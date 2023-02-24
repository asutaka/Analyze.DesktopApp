﻿using Analyze.DesktopApp.GUI;
using Analyze.DesktopApp.Job;
using Analyze.DesktopApp.Job.ScheduleJob;
using Analyze.DesktopApp.Models;
using Binance.Net.Interfaces;
using Microsoft.Extensions.Configuration;
using Quartz;
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
        public static List<string> lstError = new List<string>();
        public static List<CoinFollowDetailModel> lstMCDX = new List<CoinFollowDetailModel>();
        //Data Coin
        public static Dictionary<string, IEnumerable<LocalTicketModel>> dic1H = new Dictionary<string, IEnumerable<LocalTicketModel>>();
        //Job
        public static ScheduleMember jobError = new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<RecallErrorSymbolJob>(), Program.Configuration.GetSection("Job").Get<JobModel>().DefaultJob, nameof(RecallErrorSymbolJob));

        //State Form
        public static bool frm24HReady = false;
        public static bool frmMyListReady = false;
        public static bool isAllowCalculate = true;
        //Value Storage
        public static int TimeSynData = 2;

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
