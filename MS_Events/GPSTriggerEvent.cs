using EventBus.Base.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS_Events{

    public class GPSTriggerEvent : IntegrationEvent{
        public string Event_id { get; set; }
        public string EventName { get; set; } //這個Event名稱
        public string version { get; set; } //目前Event的版本
        public DateTime timestamp { get; set; } //紀錄Event發生時間        

        public string CarNo { get; set; }             
        public Location location { get; set; } //

    }



}
