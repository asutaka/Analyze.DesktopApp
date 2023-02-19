﻿using Analyze.DesktopApp.Models;
using Binance.Net.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        //Function
        public static IBinanceMiniTick GetCoinBinanceTick(string coin)
        {
            if (binanceTicks == null)
                Thread.Sleep(100);
            var entity = binanceTicks.FirstOrDefault(x => x.Symbol == coin);
            return entity;
        }
    }
}
