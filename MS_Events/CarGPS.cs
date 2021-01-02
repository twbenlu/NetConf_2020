using System;
using System.Collections.Generic;
using System.Text;

namespace MS_Events
{
    public class CarGPS{
        public string _id { get; set; }
        public string CarNo { get; set; }
        public DateTime Time { get; set; }
        public List<Location> locations { get; set; }
    }

    public class Location
    {
        public string _id { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Addr { get; set; }
        public string Time { get; set; }
    }


}
