using EventBus.Base.Standard;
using MS_Events;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MS_Redis
{
    public class GPSTriggerEventHandler : IIntegrationEventHandler<GPSTriggerEvent>
    {

        public async Task Handle(GPSTriggerEvent @event)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("連到你的Redis的連線字串");
            IDatabase db = redis.GetDatabase(0); ///取得那一個資料庫                    
                db.StringSet("中正區", @event.CarNo); ///設定Key與Value
            redis.Dispose(); ///釋放連線

        }
    }







}
