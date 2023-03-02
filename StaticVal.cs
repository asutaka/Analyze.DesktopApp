using Analyze.DesktopApp.Job;
using Analyze.DesktopApp.Job.ScheduleJob;
using Analyze.DesktopApp.Models;
using Binance.Net.Interfaces;
using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Concurrent;
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
        public static List<Top30VM> lstCryptonRank = new List<Top30VM>();
        //Data Coin
        public static ConcurrentDictionary<string, List<LocalTicketModel>> dic1H = new ConcurrentDictionary<string, List<LocalTicketModel>>();
        public static ConcurrentDictionary<string, float> dicVolume = new ConcurrentDictionary<string, float>();
        public static ConcurrentDictionary<string, Tuple<float, float>> dicVolumeCalculate = new ConcurrentDictionary<string, Tuple<float, float>>();

        //Job
        public static ScheduleMember jobError = new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<RecallErrorSymbolJob>(), Program.Configuration.GetSection("Job").Get<JobModel>().DefaultJob, nameof(RecallErrorSymbolJob));

        //State Form
        public static bool frm24HReady = false;
        public static bool frmMyListReady = false;
        public static bool isAllowCalculate = false;
        //Value Storage
        public static int TimeSynData = 2;
    }
}
