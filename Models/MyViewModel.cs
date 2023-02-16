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
    }
}