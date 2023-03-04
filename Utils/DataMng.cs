using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Analyze.DesktopApp.Utils
{
    public static class DataMng
    {
        public static ConcurrentDictionary<string, List<LocalTicketModel>> AssignDic1h()
        {
            ConcurrentDictionary<string, List<LocalTicketModel>> val;
            try
            {
                val = StaticVal.dic1H;
            }
            catch (Exception exD)
            {
                NLogLogger.PublishException(exD, $"DataMng.AssignDic1h|EXCEPTION(dic1H)| {exD.Message}");
                Thread.Sleep(100);
                val = StaticVal.dic1H;
            }
            return val;
        }

        public static void AssignDic1h(ConcurrentDictionary<string, List<LocalTicketModel>> val)
        {
            StaticVal.dic1H = val;
        }

        public static List<CoinFollowDetailModel> AssignlMCDX()
        {
            List<CoinFollowDetailModel> lMCDX;
            try
            {
                lMCDX = StaticVal.lstMCDX;
            }
            catch (Exception exM)
            {
                NLogLogger.PublishException(exM, $"DataMng.AssignlMCDX|EXCEPTION(lMCDX)| {exM.Message}");
                Thread.Sleep(50);
                lMCDX = StaticVal.lstMCDX;
            }
            return lMCDX;
        }

        public static void AssignlMCDX(List<CoinFollowDetailModel> val)
        {
            StaticVal.lstMCDX = val;
        }

        public static ConcurrentDictionary<string, float> AssignDicVolume()
        {
            ConcurrentDictionary<string, float> dicVolume;
            try
            {
                dicVolume = StaticVal.dicVolume;
            }
            catch (Exception exD)
            {
                NLogLogger.PublishException(exD, $"DataMng.AssignDicVolume|EXCEPTION(dicVolume)| {exD.Message}");
                Thread.Sleep(100);
                dicVolume = StaticVal.dicVolume;
            }
            return dicVolume;
        }

        public static void AssignDicVolume(ConcurrentDictionary<string, float> val)
        {
            StaticVal.dicVolume = val;
        }

        public static ConcurrentDictionary<string, Tuple<float, float>> AssignDicVolumeCalculate()
        {
            ConcurrentDictionary<string, Tuple<float, float>> dic;
            try
            {
                dic = StaticVal.dicVolumeCalculate;
            }
            catch (Exception exD)
            {
                NLogLogger.PublishException(exD, $"DataMng.AssignDicVolumeCalculate|EXCEPTION(dicVolumeCalculate)| {exD.Message}");
                Thread.Sleep(100);
                dic = StaticVal.dicVolumeCalculate;
            }
            return dic;
        }

        public static void AssignDicVolumeCalculate(ConcurrentDictionary<string, Tuple<float, float>> val)
        {
            StaticVal.dicVolumeCalculate = val;
        }

        public static void AssignTop30(List<Top30VM> val)
        {
            StaticVal.cryptonRank.lData = val;
            StaticVal.cryptonRank.IsSuccess = (val.Count() > 0 && val.First().Count > 5);
            if(StaticVal.cryptonRank.IsSuccess)
            {
                int i = 1;
                foreach (var item in StaticVal.cryptonRank.lData)
                {
                    StaticVal.cryptonRank.dicTop30.TryAdd(item.Coin, i++);
                }
            }
        }

        public static List<Top30VM> AssignTop30()
        {
            List<Top30VM> lTop30;
            try
            {
                lTop30 = StaticVal.cryptonRank.lData;
            }
            catch (Exception exM)
            {
                NLogLogger.PublishException(exM, $"DataMng.AssignTop30|EXCEPTION(lTop30)| {exM.Message}");
                Thread.Sleep(50);
                lTop30 = StaticVal.cryptonRank.lData;
            }
            return lTop30;
        }
    }
}
