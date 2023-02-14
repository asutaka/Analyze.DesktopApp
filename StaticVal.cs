using Analyze.DesktopApp.Models;
using Binance.Net.Interfaces;
using System.Collections.Generic;

namespace Analyze.DesktopApp
{
    public class StaticVal
    {
        //Local Data
        public static List<IBinanceMiniTick> binanceTicks = new List<IBinanceMiniTick>();
        //Data Coin
        public static Dictionary<string, IEnumerable<MiniTicketModel>> dic1H = new Dictionary<string, IEnumerable<MiniTicketModel>>();
    }
}
