using MS_Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GPSCore.Hub
{
    public interface IChatClient
    {


        Task hello(string _string);

        /// 測試連線
        Task ServerMessage(string _msg);

        /// <summary>
        /// 接收GPS位置
        /// </summary>
        /// <param name="_msg"></param>
        /// <returns></returns>
        Task onReciveGPS(Location _Location);

    }
}
