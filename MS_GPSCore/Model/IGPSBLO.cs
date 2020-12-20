using MS_Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GPSCore
{
    public interface IGPSBLO{
        public void SendToMongoDB(Location _Location);
    }
}
