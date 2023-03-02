using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Models;
using Binance.Net.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacTec.TA.Library;
using static TicTacTec.TA.Library.Core;

namespace Analyze.DesktopApp.Utils
{
    public static class CalculateMng
    {
        public static ConcurrentDictionary<string, List<LocalTicketModel>> _dic1H = new ConcurrentDictionary<string, List<LocalTicketModel>>();
        public static ConcurrentDictionary<string, float> _dicVolume = new ConcurrentDictionary<string, float>();
        public static ConcurrentDictionary<string, Tuple<float, float>> _dicVolumeCal = new ConcurrentDictionary<string, Tuple<float, float>>();
        public static List<IBinanceMiniTick> _binanceTicks = new List<IBinanceMiniTick>();
        public static List<CryptonDetailDataModel> _lstCoin = new List<CryptonDetailDataModel>();

        public static List<Top30VM> Top30()
        {
            var count = 1;
            var lstResult = new List<Top30VM>();
            var qResult = new ConcurrentQueue<Top30VM>();

            var lstTask = new List<Task>();
            foreach (var item in StaticVal.lstCoin)
            {
                var task = Task.Run(() =>
                {
                    var res = CalculateCryptonRank(item.S, item.AN);
                    //lstResult.Add(res);
                    qResult.Enqueue(res);
                });
                lstTask.Add(task);
            }
            Task.WaitAll(lstTask.ToArray());
            lstResult = qResult.ToList();

            lstResult = lstResult.Where(x => x != null).OrderByDescending(x => x.Count).ThenByDescending(x => x.Rate).Take(30).ToList();
            if (lstResult != null)
            {
                lstResult.ForEach(x => x.STT = count++);
            }
            return lstResult;
        }

        public static Top30VM CalculateCryptonRank(string coin, string coinName)
        {
            try
            {
                int count = 1;
                decimal sum = 0;
                var entity = _dic1H.FirstOrDefault(x => x.Key.Equals(coin, StringComparison.InvariantCultureIgnoreCase));
                if(entity.Key == null)
                    return new Top30VM { Coin = coin, Count = count, Rate = (double)Math.Round(sum / count, 2) };
                var lSource = entity.Value;
                if (lSource == null || !lSource.Any() || lSource.Count() < 10)
                    return new Top30VM { Coin = coin, Count = count, Rate = (double)Math.Round(sum / count, 2) };

                long dtMin = 0, dtMax = 0, dtMin_Temp = 0;
                int leftMax = 0, rightMin = 0, rightMax = 0;
                decimal min = 0, max = 0, min_Temp = 0;
                foreach (var item in lSource)
                {
                    CheckMinMax();
                    if (rightMax >= 2)
                    {
                        var rate = Math.Round((max - min) * 100 / min, 0);
                        if (leftMax >= 2 && rate >= 10)
                        {
                            sum += rate;
                            count++;
                        }
                        min = min_Temp;
                        dtMin = dtMin_Temp;
                        min_Temp = 0;
                        dtMin_Temp = 0;
                        rightMin = 0;
                        rightMax = 0;
                        leftMax = 0;
                        max = 0;
                        dtMax = 0;
                        CheckMinMax();
                    }
                    else if (rightMax > 0)
                    {
                        min_Temp = (decimal)item.l;
                        dtMin_Temp = item.e;
                    }

                    void CheckMinMax()
                    {
                        if (min == 0)
                        {
                            min = (decimal)item.l;
                            dtMin = item.e;
                        }
                        if ((decimal)item.l < min)
                        {
                            rightMin = 0;
                            min = (decimal)item.l;
                            dtMin = item.e;
                        }
                        else
                        {
                            rightMin++;
                        }
                        //reset
                        if (rightMin == 0)
                        {
                            max = 0;
                            leftMax = 0;
                            rightMax = 0;
                        }
                        else
                        {
                            if (max < (decimal)item.h)
                            {
                                rightMax = 0;
                                leftMax++;
                                max = (decimal)item.h;
                                dtMax = item.e;
                            }
                            else
                            {
                                rightMax++;
                            }
                        }
                    }
                }
                var outputModel = new Top30VM { Coin = coin, CoinName = coinName, Count = count, Rate = (double)Math.Round(sum / count, 2) };
                var entityBinanceTick = _binanceTicks.FirstOrDefault(x => x.Symbol.Equals(coin, StringComparison.InvariantCultureIgnoreCase)); 
                if (entityBinanceTick != null)
                {
                    outputModel.RefValue = (double)entityBinanceTick.LastPrice;
                }
                return outputModel;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng.CalculateCryptonRank|EXCEPTION|{coin}| {ex.Message}");
                return new Top30VM { Coin = coin, CoinName = coinName, Count = 1, Rate = 0 };
            }
        }

        public static List<CoinFollowDetailModel> MCDX()
        {
            var lstResult = new List<CoinFollowDetailModel>();
            try
            {
                var lstTask = new List<Task>();
                foreach (var item in _lstCoin)
                {
                    var task = Task.Run(() =>
                    {
                        var val = MCDX(item.S);
                        if (val.Item1)
                        {
                            lstResult.Add(new CoinFollowDetailModel
                            {
                                Symbol = item.S,
                                Value = val.Item2,
                            });
                        }
                    });
                    lstTask.Add(task);
                }
                Task.WaitAll(lstTask.ToArray());
                lstResult = lstResult.OrderByDescending(x => x.Value).ToList();
                (bool, double) MCDX(string coin)
                {
                    var data = _dic1H.FirstOrDefault(x => x.Key.Equals(coin, StringComparison.InvariantCultureIgnoreCase));
                    if (data.Key == null || !data.Value.Any())
                        return (false, 0);
                    var valTemp1H = _binanceTicks.FirstOrDefault(x => x.Symbol.Equals(coin, StringComparison.InvariantCultureIgnoreCase));
                    if (valTemp1H == null)
                        return (false, 0);

                    var settings = Program.Configuration.GetSection("Calculate").Get<CalculateModel>();

                    var arrClose = data.Value.Select(x => (double)x.c).ToArray();
                    arrClose = arrClose.Concat(new double[] { (double)valTemp1H.LastPrice }).ToArray();
                    var count = arrClose.Count();
                    if (count < 50)
                        return (false, 0);

                    double[] output1 = new double[1000];
                    double[] output2 = new double[1000];
                    Rsi(0, count - 1, arrClose, 50, out int outBegIdx1, out int outNBElement1, output1);
                    Rsi(0, count - 1, arrClose, 40, out int outBegIdx2, out int outNBElement2, output2);
                    var rsi50 = output1[count - 51];
                    var rsi40 = output2[count - 41];
                    var banker_rsi = 1.5 * (rsi50 - 50);
                    if (banker_rsi > 20)
                        banker_rsi = 20;
                    if (banker_rsi < 0)
                        banker_rsi = 0;
                    var signal = settings.MCDX;
                    return (banker_rsi >= signal, banker_rsi);
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng.MCDX|EXCEPTION| {ex.Message}");
            }

            return lstResult;
        }

        public static double ADX(double[] arrHigh, double[] arrLow, double[] arrClose, int period, int count)
        {
            try
            {
                var output = new double[1000];
                Core.Adx(0, count - 1, arrHigh, arrLow, arrClose, period, out var outBegIdx, out var outNBElement, output);
                return output[count - period];
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng.ADX|EXCEPTION| {ex.Message}");
            }
            return 0;
        }
        public static double MA(double[] arrInput, Core.MAType type, int period, int count)
        {
            try
            {
                var output = new double[1000];
                Core.MovingAverage(0, count - 1, arrInput, period, Core.MAType.Sma, out var outBegIdx, out var outNBElement, output);
                return output[count - period];
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng.MA|EXCEPTION| {ex.Message}");
            }
            return 0;
        }
        public static double MACD(double[] arrInput, int high, int low, int signal, int count)
        {
            try
            {
                var output = new double[1000];
                Core.Macd(0, count - 1, arrInput, low, high, signal, out var outBegIdx, out var outNbElement, new double[1000], new double[1000], output);
                return output[count - 1];
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng.MACD|EXCEPTION| {ex.Message}");
            }
            return 0;
        }
        public static IEnumerable<double> MACD(double[] arrInput, int high, int low, int signal, int count, int take)
        {
            try
            {
                var output = new double[1000];
                Core.Macd(0, count - 1, arrInput, low, high, signal, out var outBegIdx, out var outNbElement, new double[1000], new double[1000], output);
                return output.Skip(count - (take + 1)).Take(take);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng.MACD|EXCEPTION| {ex.Message}");
            }
            return null;
        }

        public static double RSI(double[] arrInput, int period, int count)
        {
            try
            {
                var output = new double[1000];
                Core.Rsi(0, count - 1, arrInput, period, out var outBegIdx, out var outNBElement, output);
                return output[count - period];
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng.RSI|EXCEPTION| {ex.Message}");
            }
            return 0;
        }

        public static ConcurrentDictionary<string, Tuple<float, float>> CalculateVolume()
        {
            foreach (var item in _dicVolume)
            {
                if (item.Value <= 0)
                    continue;
                var entityDicData = _dic1H.FirstOrDefault(x => x.Key.Equals(item.Key, StringComparison.InvariantCultureIgnoreCase));
                if (entityDicData.Key != null)
                {
                    var count = entityDicData.Value.Count();
                    if (count >= 20)
                    {
                        var entityCal = _dicVolumeCal.FirstOrDefault(x => x.Key == item.Key);
                        if (entityCal.Key != null)
                        {
                            var list = entityDicData.Value.Select(x => (double)x.v).ToList();
                            list.Add(item.Value);
                            double MA20 = 0;
                            double percent = 0;
                            MA20 = MA(list.ToArray(), MAType.Sma, 20, count + 1);
                            if (MA20 > 0)
                            {
                                percent = Math.Round(item.Value * 100 / MA20, 2);
                            }
                            _dicVolumeCal[entityCal.Key] = new Tuple<float, float>((float)MA20, (float)percent);
                        }
                    }
                }
            }
            return _dicVolumeCal;
        }
    }
}
