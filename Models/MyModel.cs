namespace Analyze.DesktopApp.Models
{
    public class ProfileModel
    {
        public string phone { get; set; }
        public string password { get; set; }
    }

    public class CheckUserModel : ProfileModel
    {
        public string signature { get; set; }
    }

    public class DomainModel
    {
        public string Main { get; set; }
        public string Sub1 { get; set; }
        public string Sub2 { get; set; }
        public string Sub3 { get; set; }
        public string Sub4 { get; set; }
    }

    public class SendNotifyModel
    {
        public string phone { get; set; }
        public string message { get; set; }
    }

    public class TicketModel
    {
        public string symbol { get; set; }
        public float priceChange { get; set; }
        public float priceChangePercent { get; set; }
        public float weightedAvgPrice { get; set; }
        public float prevClosePrice { get; set; }
        public float lastPrice { get; set; }
        public float lastQty { get; set; }
        public float bidPrice { get; set; }
        public float bidQty { get; set; }
        public float askPrice { get; set; }
        public float askQty { get; set; }
        public float openPrice { get; set; }
        public float highPrice { get; set; }
        public float lowPrice { get; set; }
        public float volume { get; set; }
        public float quoteVolume { get; set; }
        public long openTime { get; set; }
        public long closeTime { get; set; }
        public long firstId { get; set; }
        public long lastId { get; set; }
        public long count { get; set; }
    }
}
