using EventBus.Base.Standard;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using MS_Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GPSCore.Hub
{
    public class IMHub : Hub<IChatClient>{

        private IGPSBLO iGPSBLO;

        private readonly IEventBus _eventBus;

        //public IMHub(IGPSBLO _iGPSBLO) {
        //    iGPSBLO = _iGPSBLO;
        //}

        public IMHub(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        /// <summary>
        /// 測試連線
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            try
            {
                await Clients.All.ServerMessage("連線成功");
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }


        public async Task onReciveGPS(Location _Location) {
            try{
                  
                //把資料送進 MongoDB                    
                IMongoClient _client = new MongoClient("mongodb://localhost");               
                IMongoDatabase _database = _client.GetDatabase("NetConf2020"); //DataBase   
                IMongoCollection<CarGPS> _collection = _database.GetCollection<CarGPS>("GPSData"); //Table  Collection
              
                UpdateResult result = _collection.UpdateOneAsync(
                     Builders<CarGPS>.Filter.Eq(x => x.CarNo, "123-GH"),
                     Builders<CarGPS>.Update.Push<Location>(x => x.locations, _Location) //每次第一筆更新不到很正常
                ).Result;

                //再新增
                if (result.ModifiedCount == 0)
                {
                    CarGPS _CarGPS = new CarGPS()
                    {
                        GsmNo = "0900123456",
                        CarNo = "123-GH",
                        Time = DateTime.UtcNow,
                        locations = new List<Location>() { _Location }
                    };
                    _collection.InsertOneAsync(_CarGPS);
                }

                //向 Event Bus 發出一個 Event
                GPSTriggerEvent _ge = new GPSTriggerEvent()
                {
                     Event_id = Guid.NewGuid().ToString(),
                     EventName = "車輛GPS更新",
                     timestamp = DateTime.Now,
                     version = "v1.0",
                      CarNo = "123-ABC",
                      location = _Location
                };

                _eventBus.Publish(_ge);
              

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }



    }
}
