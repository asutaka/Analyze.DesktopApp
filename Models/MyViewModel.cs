using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Analyze.DesktopApp.Models
{
    public class API24hVM
    {
        public int STT { get; set; }
        public string Coin { get; set; }
        public string CoinName { get; set; }
        public double PriceChange { get; set; }
        public double PriceChangePercent { get; set; }
        public float weightedAvgPrice { get; set; }
        public float prevClosePrice { get; set; }
        public float lastPrice { get; set; }
        public float openPrice { get; set; }
        public float highPrice { get; set; }
        public float lowPrice { get; set; }
        public float volume { get; set; }
        //
        public float PriceRef { get; set; }
        public float Div { get; set; }
        public float volumeMA20 { get; set; }
        public float volumeDiv { get; set; }
        //
        public int MCDX { get; set; }
    }

    public class Top30VM
    {
        public int STT { get; set; }
        public string Coin { get; set; }
        public string CoinName { get; set; }
        public int Count { get; set; }
        public double Rate { get; set; }
        public double RefValue { get; set; }
        public double Value { get; set; }
        public double BottomRecent { get; set; }
        public double RateValue { get; set; }
        public double WaveRecent { get; set; }
        public int CountTime { get; set; }
        public decimal PrevDayClosePrice { get; set; }
        public decimal PriceChangePercent { get; set; }
        public decimal WeightedAveragePrice { get; set; }
    }

    public class ListTop30VM
    {
        public List<Top30VM> lData { get; set; }
        public ConcurrentDictionary<string, int> dicTop30 { get; set; }
        public bool IsSuccess { get; set; }
    }
}