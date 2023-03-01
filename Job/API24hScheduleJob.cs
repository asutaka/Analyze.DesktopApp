﻿using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.GUI.Child;
using Analyze.DesktopApp.Utils;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution] /*impt: no multiple instances executed concurrently*/
    public class API24hScheduleJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                //if (StaticVal.IsRealTimeAction)
                //    return;
                var binanceTick = StaticVal.binanceTicks;
                var lMCDX = DataMng.AssignlMCDX();
                var dicVolume = DataMng.AssignDicVolume();
                var dicVolumeCal = DataMng.AssignDicVolumeCalculate();

                var lstTask = new List<Task>();
                foreach (var item in StaticVal.lst24H)
                {
                    if (binanceTick == null)
                        return;
                    var task = Task.Run(() =>
                    {
                        var coin = item.Coin;
                        var entityBinanceTick = binanceTick.FirstOrDefault(x => x.Symbol.Equals(coin, StringComparison.InvariantCultureIgnoreCase));
                        if (entityBinanceTick == null)
                        {
                            return;
                        }
                        item.lastPrice = (float)entityBinanceTick.LastPrice;
                        item.Div = (float)Math.Round(((-1 + item.lastPrice / item.PriceRef) * 100), 1);
                        item.PriceChangePercent = Math.Round(((-1 + item.lastPrice / item.prevClosePrice) * 100), 1);
                        var entityVol = dicVolume.FirstOrDefault(x => x.Key.Equals(coin, StringComparison.InvariantCultureIgnoreCase));
                        item.volume = entityVol.Key != null ? entityVol.Value : 0;
                        var entityVolCal = dicVolumeCal.FirstOrDefault(x => x.Key.Equals(coin, StringComparison.InvariantCultureIgnoreCase));
                        item.volumeMA20 = entityVol.Key != null ? entityVolCal.Value.Item1 : 0;
                        item.volumeDiv = entityVol.Key != null ? entityVolCal.Value.Item2 : 0;

                        var entityMCDX = lMCDX.FirstOrDefault(x => x.Symbol.Equals(coin, StringComparison.InvariantCultureIgnoreCase));
                        if (entityMCDX != null)
                        {
                            item.MCDX = entityMCDX.Value >= 18 ? 2 : 1;
                        }
                    });
                    lstTask.Add(task);
                }
                Task.WaitAll(lstTask.ToArray());
                //if (StaticVal.IsRealTimeAction)
                //    return;
                if (StaticVal.frm24HReady)
                {
                    frm24H._lst24H = StaticVal.lst24H.OrderByDescending(x => x.PriceChangePercent).ToList();
                    frm24H.Instance().InitData();
                }

                if (StaticVal.frmMyListReady)
                {
                    frmMyList.Instance().InitData();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"API24hScheduleJob.Execute|EXCEPTION| {ex.Message}");
            }
        }
    }
}
