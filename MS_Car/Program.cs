using Microsoft.AspNetCore.SignalR.Client;
using MS_Events;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace MS_Car
{
    public class Program{

        static void Main(string[] args){

            //載入模擬GPS的檔案
            string text = File.ReadAllText(@"car2.json");
            CarGPS item = JsonConvert.DeserializeObject<CarGPS>(text);

            //連結SignalR GPS資料
            var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:6001/IMHub")
            .ConfigureLogging(logging =>
            {
            })
            .Build();

            connection.StartAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }
            }).Wait();



            //每1秒鐘送出GPS訊號
            item.locations.ForEach(i =>
            {
                Console.WriteLine("經度 = {0},緯度 = {1}", i.Lon, i.Lat);
                Thread.Sleep(1000); //Delay 1秒                
                connection.InvokeAsync("onReciveGPS", i).Wait();
            });



            Console.ReadLine();
        }
    }
}
