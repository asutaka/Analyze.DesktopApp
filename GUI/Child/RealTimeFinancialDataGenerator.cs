using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Models;
using Analyze.DesktopApp.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using TicTacTec.TA.Library;

namespace Analyze.DesktopApp.GUI.Child
{
    public class RealTimeFinancialDataGenerator
    {
        const double MinPrice = 5.0;
        const double StartPrice = 24.0;
        const int InitialDataPointsCount = 350000;
        const int MaxPointsCount = 255000;
        const int PeriodMilliseconds = 30;

        readonly FinancialDataCollection dataSource = new FinancialDataCollection();
        readonly Random random = new Random(3);
        readonly List<FinancialDataPoint> buffer = new List<FinancialDataPoint>();
        readonly object bufferSync = new object();
        FinancialDataPoint prevPoint;
        bool generatingEnabled = false;
        Thread generatingThread;

        public FinancialDataCollection DataSource
        {
            get { return dataSource; }
        }
        public DateTime LastArgument
        {
            get { return prevPoint.DateTimeStamp; }
        }

        bool firstOnlinePoint = true;
        FinancialDataPoint CreateOnlinePoint(DateTime argument, FinancialDataPoint locPrevPoint)
        {
            double priceDelta = (random.NextDouble() - 0.5) / 300d;
            double close = locPrevPoint.Close + priceDelta;
            if (close <= MinPrice)
                close = 2 * MinPrice - close;
            double open = locPrevPoint.Close;
            double high = Math.Max(open, close) + (random.NextDouble()) / 100d;
            double low = Math.Min(open, close) - (random.NextDouble()) / 100d;
            double volume;
            if (!firstOnlinePoint)
            {
                volume = locPrevPoint.Volume + random.Next(-5, 5);
            }
            else
            {
                volume = 2;
                firstOnlinePoint = false;
            }
            if (volume < 2)
                volume = 4 - volume;
            return new FinancialDataPoint(argument, open, high, low, close, volume);
        }
        FinancialDataPoint CreateHistoryPoint(DateTime argument, FinancialDataPoint locPrevPoint)
        {
            double priceDelta = (random.NextDouble() - 0.5) / 8d;
            double close = locPrevPoint.Close + priceDelta;
            if (close <= MinPrice)
                close = 2 * MinPrice - close;
            double open = locPrevPoint.Close;
            double high = Math.Max(open, close) + (random.NextDouble()) / 25d;
            double low = Math.Min(open, close) - (random.NextDouble()) / 25d;
            double volume = locPrevPoint.Volume + random.Next(-50000, 50000);
            if (volume < 10000)
                volume = 2 * 10000 - volume;
            if (volume > 200000)
                volume = 200000 - (int)(volume / 4);
            return new FinancialDataPoint(argument, open, high, low, close, volume);
        }
        void GeneratingLoop()
        {
            DateTime timeStamp = DateTime.Now;
            while (generatingEnabled)
            {
                DateTime newTimeStamp = timeStamp.AddMilliseconds(PeriodMilliseconds);
                TimeSpan span = newTimeStamp - DateTime.Now;
                if (span.Ticks > 0)
                    Thread.Sleep((int)span.TotalMilliseconds);
                timeStamp = newTimeStamp;
                AddPoint(timeStamp);
            }
        }
        FinancialDataPoint currentAggregatingPoint;
        void AddPoint(DateTime timeStamp)
        {
            FinancialDataPoint point = CreateOnlinePoint(timeStamp, prevPoint);
            if (currentAggregatingPoint.DateTimeStamp.Minute == timeStamp.Minute)
            {
                currentAggregatingPoint.Close = point.Close;
                currentAggregatingPoint.High = Math.Max(currentAggregatingPoint.High, point.High);
                currentAggregatingPoint.Low = Math.Min(currentAggregatingPoint.Low, point.Low);
                currentAggregatingPoint.Volume += point.Volume;
                lock (bufferSync)
                {
                    if (buffer.Count > 0)
                        buffer[buffer.Count - 1] = currentAggregatingPoint;
                    else
                        buffer.Add(currentAggregatingPoint);
                }
            }
            else
            {
                lock (bufferSync)
                {
                    currentAggregatingPoint = point;
                    buffer.Add(point);
                }
            }
            prevPoint = point;
        }
        bool TheSameMinute(DateTime dt1, DateTime dt2)
        {
            return (dt1 - DateTime.MinValue).TotalMinutes == (dt2 - DateTime.MinValue).TotalMinutes;
        }

        internal void GenerateInitialData()
        {
            DateTime baseDate = DateTime.Now.AddMinutes(-InitialDataPointsCount).Date;
            if (baseDate.DayOfWeek == DayOfWeek.Saturday || baseDate.DayOfWeek == DayOfWeek.Sunday)
                baseDate = baseDate.AddDays(baseDate.DayOfWeek == DayOfWeek.Saturday ? 2 : 1);
            prevPoint = new FinancialDataPoint(baseDate, StartPrice, StartPrice + 0.002, StartPrice - 0.002, StartPrice + 0.001, 100000);
            dataSource.Add(prevPoint);
            DateTime argument = baseDate;
            while (argument < DateTime.Now.AddMinutes(-1))
            {
                argument = argument.AddMinutes(1);
                if (argument.DayOfWeek == DayOfWeek.Saturday)
                    argument = argument.AddDays(2);
                FinancialDataPoint point = CreateHistoryPoint(argument, prevPoint);
                prevPoint = point;
                dataSource.Add(point);
            }
            currentAggregatingPoint = prevPoint;
            currentAggregatingPoint.Volume = (int)(DateTime.Now.Second / 60d * currentAggregatingPoint.Volume);
        }
        internal void UpdateDataSource()
        {
            List<FinancialDataPoint> tempBuffer;
            lock (bufferSync)
            {
                tempBuffer = new List<FinancialDataPoint>(buffer);
                buffer.Clear();
            }
            if (tempBuffer.Count == 0)
                return;
            if (TheSameMinute(tempBuffer[0].DateTimeStamp, dataSource[dataSource.Count - 1].DateTimeStamp))
            {
                dataSource[dataSource.Count - 1] = tempBuffer[0];
            }
            else
            {
                dataSource.Add(tempBuffer[0]);
            }
            if (tempBuffer.Count > 1)
                dataSource.AddRange(tempBuffer.GetRange(1, tempBuffer.Count - 1));
            int overflow = dataSource.Count - MaxPointsCount;
            if (overflow > 0)
            {
                dataSource.RemoveRangeAt(0, overflow);
            }
        }
        internal void Start()
        {
            if (generatingThread == null)
                generatingThread = new Thread(new ThreadStart(GeneratingLoop));
            generatingEnabled = true;
            generatingThread.Start();
        }
        internal void Stop()
        {
            generatingEnabled = false;
            if (generatingThread != null)
                generatingThread.Join();
            generatingThread = null;
        }
        //phunv
        public double _AVG = 0;
        private static List<FinancialDataPoint> lResult = new List<FinancialDataPoint>();
        internal void InitialData(string symbol)
        {
            lResult.Clear();
            var settings = Program.Configuration.GetSection("API").Get<APIModel>();
            try
            {
                var content = WebClass.GetWebContent(string.Format(settings.History, symbol.ToUpper())).GetAwaiter().GetResult();
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var arr = JArray.Parse(content);
                    lResult.AddRange(arr.Select(x => 
                    new FinancialDataPoint((((long)x[0]) / 1000).UnixTimeStampToDateTime(), 
                                            double.Parse(x[1].ToString()), 
                                            double.Parse(x[2].ToString()), 
                                            double.Parse(x[3].ToString()), 
                                            double.Parse(x[4].ToString()), 
                                            double.Parse(x[5].ToString())))
                                                .ToList());
                    count = lResult.Count();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"RealTimeFinancialDataGenerator.InitialData|EXCEPTION| {ex.Message}");
            }
            var index = 1;
            foreach (var item in lResult)
            {
                item.Volume = MCDX(lResult.Take(index++))*150000;  
            }
            _AVG = lResult.Sum(x => x.Close) / lResult.Count();
            prevPoint = lResult.Last();
            index = 100;
            _lstCalculate = lResult.Take(index).ToList();
            dataSource.AddRange(_lstCalculate);
            currentAggregatingPoint = prevPoint;
        }

        internal void InitialDataFast(string symbol)
        {
            lResult.Clear();
            var settings = Program.Configuration.GetSection("API").Get<APIModel>();
            try
            {
                var content = WebClass.GetWebContent(string.Format(settings.History, symbol.ToUpper())).GetAwaiter().GetResult();
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var arr = JArray.Parse(content);
                    lResult.AddRange(arr.Select(x =>
                    new FinancialDataPoint((((long)x[0]) / 1000).UnixTimeStampToDateTime(),
                                            double.Parse(x[1].ToString()),
                                            double.Parse(x[2].ToString()),
                                            double.Parse(x[3].ToString()),
                                            double.Parse(x[4].ToString()),
                                            double.Parse(x[5].ToString())))
                                                .ToList());
                    count = lResult.Count();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"RealTimeFinancialDataGenerator.InitialData|EXCEPTION| {ex.Message}");
            }
            var index = 1;
            foreach (var item in lResult)
            {
                item.Volume = MCDX(lResult.Take(index++)) * 150000;
            }
            _AVG = lResult.Sum(x => x.Close) / lResult.Count();
            prevPoint = lResult.Last();
            index = count;
            _lstCalculate = lResult.ToList();
            dataSource.AddRange(_lstCalculate);
            currentAggregatingPoint = prevPoint;
        }

        internal void InitialDataAll(string symbol)
        {
            lResult.Clear();
            var settings = Program.Configuration.GetSection("API").Get<APIModel>();
            try
            {
                var contentM = WebClass.GetWebContent(string.Format(settings.HistoryM, symbol.ToUpper())).GetAwaiter().GetResult();
                if (!string.IsNullOrWhiteSpace(contentM))
                {
                    var arrM = JArray.Parse(contentM);
                    var time = arrM[0][0];
                    string content = string.Empty;
                    JArray arr = null;
                    int countArr = 0;
                    LogM.Start();
                    do
                    {
                        content = WebClass.GetWebContent(string.Format(settings.HistoryTime, symbol.ToUpper(), time)).GetAwaiter().GetResult();
                        if (!string.IsNullOrWhiteSpace(content))
                        {
                            arr = JArray.Parse(content);
                            countArr = arr.Count();
                            lResult.AddRange(arr.Select(x =>
                               new FinancialDataPoint((((long)x[0]) / 1000).UnixTimeStampToDateTime(),
                                                       double.Parse(x[1].ToString()),
                                                       double.Parse(x[2].ToString()),
                                                       double.Parse(x[3].ToString()),
                                                       double.Parse(x[4].ToString()),
                                                       double.Parse(x[5].ToString())))
                                                           .ToList());
                            time = (countArr > 0) ? arr[countArr - 1][0] : 0;
                        }
                    }
                    while (!string.IsNullOrWhiteSpace(content) && countArr >= 500);
                    count = lResult.Count();
                    LogM.Log($"count: {count}");
                    LogM.Stop();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"RealTimeFinancialDataGenerator.InitialData|EXCEPTION| {ex.Message}");
            }
            _AVG = lResult.Sum(x => x.Close) / lResult.Count();
            prevPoint = lResult.Last();
            index = 100;
            _lstCalculate = lResult.Take(index).ToList();
            dataSource.AddRange(_lstCalculate);
            currentAggregatingPoint = prevPoint;
        }

        internal void InitialDataFastAll(string symbol, int max = 0)
        {
            lResult.Clear();
            var settings = Program.Configuration.GetSection("API").Get<APIModel>();
            try
            {
                var contentM = WebClass.GetWebContent(string.Format(settings.HistoryM, symbol.ToUpper())).GetAwaiter().GetResult();
                if (!string.IsNullOrWhiteSpace(contentM))
                {
                    var arrM = JArray.Parse(contentM);
                    var time = arrM[0][0];
                    string content = string.Empty;
                    JArray arr = null;
                    int countArr = 0;
                    LogM.Start();
                    do
                    {
                        content = WebClass.GetWebContent(string.Format(settings.HistoryTime, symbol.ToUpper(), time)).GetAwaiter().GetResult();
                        if (!string.IsNullOrWhiteSpace(content))
                        {
                            arr = JArray.Parse(content);
                            countArr = arr.Count();
                            lResult.AddRange(arr.Select(x =>
                               new FinancialDataPoint((((long)x[0]) / 1000).UnixTimeStampToDateTime(),
                                                       double.Parse(x[1].ToString()),
                                                       double.Parse(x[2].ToString()),
                                                       double.Parse(x[3].ToString()),
                                                       double.Parse(x[4].ToString()),
                                                       double.Parse(x[5].ToString())))
                                                           .ToList());
                            time = (countArr > 0) ? arr[countArr - 1][0] : 0;
                            if (max > 0 && lResult.Count() >= max)
                                break;
                        }
                    }
                    while (!string.IsNullOrWhiteSpace(content) && countArr >= 500);
                    count = lResult.Count();
                    LogM.Log($"count: {count}");
                    LogM.Stop();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"RealTimeFinancialDataGenerator.InitialData|EXCEPTION| {ex.Message}");
            }
            _AVG = lResult.Sum(x => x.Close) / lResult.Count();
            prevPoint = lResult.Last();
            index = count;
            _lstCalculate = lResult.ToList();
            dataSource.AddRange(_lstCalculate);
            currentAggregatingPoint = prevPoint;
        }

        public double MCDX(IEnumerable<FinancialDataPoint> data)
        {
            var arrClose = data.Select(x => (double)x.Close).ToArray();
            var count = arrClose.Count();
            if (count <= 50)
                return 0;

            double[] output1 = new double[1000];
            double[] output2 = new double[1000];
            Core.Rsi(0, count - 1, arrClose, 50, out int outBegIdx1, out int outNBElement1, output1);
            Core.Rsi(0, count - 1, arrClose, 40, out int outBegIdx2, out int outNBElement2, output2);
            var rsi50 = output1[count - 51];
            var rsi40 = output2[count - 41];
            var banker_rsi = 1.5 * (rsi50 - 50);
            if (banker_rsi > 20)
                banker_rsi = 20;
            if (banker_rsi < 0)
                banker_rsi = 0;
            return banker_rsi;
        }

        int count = 0;
        public int index = 0;
        bool isComplete = false;
        public List<FinancialDataPoint> _lstCalculate = new List<FinancialDataPoint>();
        internal void UpdateSource()
        {
            if(isComplete)
            {
                return;
            }
            if(index >= count)
            {
                isComplete = true;
                return;
            }
            var item = lResult.Skip(index++).Take(1).First();
            _lstCalculate.Add(item);
            dataSource.Add(item);
        }
    }

    public class FinancialDataCollection : ObservableCollection<FinancialDataPoint>
    {
        public void AddRange(List<FinancialDataPoint> list)
        {
            for (int i = 0; i < list.Count; i++)
                Items.Add(list[i]);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, list, Items.Count - list.Count));
        }
        public void RemoveRangeAt(int startingIndex, int count)
        {
            var removedItems = new List<FinancialDataPoint>(count);
            for (int i = 0; i < count; i++)
            {
                removedItems.Add(Items[startingIndex]);
                Items.RemoveAt(startingIndex);
            }
            if (count > 0)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItems, startingIndex));
        }
    }

    public class FinancialDataPoint
    {
        public DateTime DateTimeStamp { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public bool IsEmpty { get { return DateTimeStamp.Equals(new DateTime()); } }
        public double MCDX { get; set; }

        public FinancialDataPoint() { }
        public FinancialDataPoint(DateTime date, double open, double high, double low, double close, double volume)
        {
            this.DateTimeStamp = date;
            this.Low = low;
            this.High = high;
            this.Open = open;
            this.Close = close;
            this.Volume = volume;
        }
    }
}
