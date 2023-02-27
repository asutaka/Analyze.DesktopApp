using Analyze.DesktopApp.Common;
using Binance.Net.Clients;
using Binance.Net.Objects;
using Quartz;
using System;
using System.Linq;
using System.Threading;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution]
    public class SubcribeJob : IJob
    {
        private static BinanceSocketClient _socket;
        public void Execute(IJobExecutionContext context)
        {
            //Console.WriteLine($"access Job: {SocketInstance().IncomingKbps}; Time: {DateTime.Now}");
            if (SocketInstance().IncomingKbps == 0)
            {
                NLogLogger.LogInfo($"retry Job: {DateTime.Now}");
                SocketInstance().UnsubscribeAllAsync();
                Thread.Sleep(2000);
                var binanceTick = StaticVal.binanceTicks;
                var isLock = false;
                var subscribeResult = SocketInstance().SpotStreams.SubscribeToAllMiniTickerUpdatesAsync(data => {
                    if (!isLock)
                    {
                        isLock = true;
                        var lData = data.Data.Where(x => x.Symbol.EndsWith("USDT")).ToList();
                        var lExists = binanceTick.Where(x => lData.Any(y => y.Symbol.Equals(x.Symbol, StringComparison.InvariantCultureIgnoreCase)));
                        if (lExists != null && lExists.Any())
                        {
                            binanceTick = binanceTick.Except(lExists).ToList();
                        }
                        binanceTick.AddRange(lData);
                        ////for test
                        //var tmp = lData.FirstOrDefault(x => x.Symbol.Equals("1INCHUSDT", StringComparison.InvariantCultureIgnoreCase));
                        //if (tmp != null)
                        //{
                        //    var entity = StaticVal.dic1H.FirstOrDefault(x => x.Key.Equals("1INCHUSDT", StringComparison.InvariantCultureIgnoreCase));
                        //    if(entity.Key != null)
                        //    {
                        //        var count = entity.Value.Count();
                        //        var sum1 = entity.Value.Skip(count - 23).Sum(x => x.v);
                        //        //LogM.Log($"sum1: {sum1}");
                        //        //var sum1A = entity.Value.Skip(count - 24).Sum(x => x.v);
                        //        //LogM.Log($"sum1A: {sum1A}");
                        //        LogM.Log($"Socket: {tmp.Volume}");
                        //        LogM.Log($"Last: {entity.Value.Last().v}");
                        //        LogM.Log($"Count-1: {entity.Value[count-1].v}");
                        //        var tmp2 = tmp.Volume - (decimal)sum1;
                        //        LogM.Log($"tmp2: {tmp2}");
                        //    }
                            
                        //}

                        StaticVal.binanceTicks = binanceTick;
                        isLock = false;
                    }
                }).GetAwaiter().GetResult();

                subscribeResult.Data.ConnectionLost += () => NLogLogger.LogInfo("Connection lost"); 
                subscribeResult.Data.ConnectionRestored += (t) => NLogLogger.LogInfo("Connection restored"); 
            }
        }

        public static BinanceSocketClient SocketInstance()
        {
            _socket = _socket ?? new BinanceSocketClient(new BinanceSocketClientOptions() { });
            return _socket;
        }
    }
}
