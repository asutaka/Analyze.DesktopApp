using Analyze.DesktopApp.Models;
using Binance.Net.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Analyze.DesktopApp
{
    public class StaticVal
    {
        //Local Data
        public static List<IBinanceMiniTick> binanceTicks = new List<IBinanceMiniTick>();
        public static List<CryptonDetailDataModel> lstCoin = new List<CryptonDetailDataModel>();
        public static List<CryptonDetailDataModel> lstCoinFilter = new List<CryptonDetailDataModel>();
        public static List<API24hVM> lst24H = new List<API24hVM>();
        //Data Coin
        public static Dictionary<string, IEnumerable<LocalTicketModel>> dic1H = new Dictionary<string, IEnumerable<LocalTicketModel>>();

        //State Form
        public static bool frm24HReady = false;
        public static bool frmMyList = false;
        //Function
        public static IBinanceMiniTick GetCoinBinanceTick(string coin)
        {
            if (binanceTicks == null)
            {
                return null;
            }
            var entity = binanceTicks.FirstOrDefault(x => x.Symbol == coin);
            return entity;
        }
    }
}
