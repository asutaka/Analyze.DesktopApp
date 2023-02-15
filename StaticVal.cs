using Analyze.DesktopApp.Models;
using Binance.Net.Interfaces;
using System.Collections.Generic;

namespace Analyze.DesktopApp
{
    public class StaticVal
    {
        //Local Data
        public static List<IBinanceMiniTick> binanceTicks = new List<IBinanceMiniTick>();
        public static List<CryptonDetailDataModel> lstCoin = new List<CryptonDetailDataModel>();
        public static List<CryptonDetailDataModel> lstCoinFilter = new List<CryptonDetailDataModel>();
        //Data Coin
        public static Dictionary<string, IEnumerable<MiniTicketModel>> dic1H = new Dictionary<string, IEnumerable<MiniTicketModel>>();
    }
}
