using EventBus.Base.Standard;
using MongoDB.Driver;
using MS_Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GPSCore.Model{

    [DependencyInjection]
    public class GPSBLO : IGPSBLO{

        private readonly IEventBus _eventBus;

        public GPSBLO(IEventBus eventBus){
            _eventBus = eventBus;
        }

        public void SendToMongoDB(Location _Location)
        {

            //把資料送進 MongoDB                    
            IMongoClient _client = new MongoClient("mongodb://localhost");
            IMongoDatabase _database = _client.GetDatabase("NetConf2020"); //DataBase   
            IMongoCollection<CarGPS> _collection = _database.GetCollection<CarGPS>("GPSData"); //Table  Collection

            UpdateResult result = _collection.UpdateOneAsync(
                 Builders<CarGPS>.Filter.Eq(x => x.CarNo, "216-GH"),
                 Builders<CarGPS>.Update.Push<Location>(x => x.locations, _Location)
            ).Result;

            //再新增
            if (result.ModifiedCount == 0){
                CarGPS _CarGPS = new CarGPS(){ 
                     CarNo = "123-ABC",
                     Time = DateTime.UtcNow,
                     locations = new List<Location>(){ _Location}                      
                };
                _collection.InsertOneAsync(_CarGPS);
            }

            //向 Event Bus 發出一個 Event
            GPSTriggerEvent _ge = new GPSTriggerEvent(){
                 //在這邊發出你的 Event Bus 事件
            }; 

            _eventBus.Publish(_ge);

        }
    }

    public class DependencyInjectionAttribute : Attribute { }


}
