using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.GUI.Child;
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
                var lstTask = new List<Task>();
                foreach (var item in StaticVal.lst24H)
                {
                    var task = Task.Run(() =>
                    {
                        var coin = item.Coin;
                        var entityBinanceTick = StaticVal.GetCoinBinanceTick(coin);
                        if (entityBinanceTick == null)
                        {
                            return;
                        }
                        item.lastPrice = (float)entityBinanceTick.LastPrice;
                        item.Div = (float)Math.Round(((-1 + item.lastPrice / item.PriceRef) * 100), 1);
                        //item.PriceChange = Math.Round(item.lastPrice - item.prevClosePrice, 2);
                        item.PriceChangePercent = Math.Round(((-1 + item.lastPrice / item.prevClosePrice) * 100), 1);
                        var entityVol = StaticVal.dicVolumeFix.FirstOrDefault(x => x.Key.Equals(coin, StringComparison.InvariantCultureIgnoreCase));
                        item.volume = entityVol.Key != null ? (float)entityBinanceTick.Volume - entityVol.Value : 0;

                        var entityMCDX = StaticVal.lstMCDX.FirstOrDefault(x => x.Symbol.Equals(coin, StringComparison.InvariantCultureIgnoreCase));
                        if(entityMCDX != null)
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
