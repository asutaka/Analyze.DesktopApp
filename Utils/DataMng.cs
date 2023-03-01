using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Analyze.DesktopApp.Utils
{
    public static class DataMng
    {
        public static Dictionary<string, List<LocalTicketModel>> AssignDic1h()
        {
            Dictionary<string, List<LocalTicketModel>> val;
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

        public static Dictionary<string, float> AssignDicVolume()
        {
            Dictionary<string, float> dicVolume;
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

        public static Dictionary<string, Tuple<float, float>> AssignDicVolumeCalculate()
        {
            Dictionary<string, Tuple<float, float>> dic;
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
    }
}
