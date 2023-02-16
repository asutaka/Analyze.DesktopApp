using System.Collections.Generic;

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

    #region AppSetting
    public class DomainModel
    {
        public string Main { get; set; }
        public string Sub1 { get; set; }
        public string Sub2 { get; set; }
        public string Sub3 { get; set; }
        public string Sub4 { get; set; }
        public string Sub5 { get; set; }
        public string Sub6 { get; set; }
        public string Sub7 { get; set; }
        public string Sub8 { get; set; }
    } 

    public class ViewWebModel
    {
        public string Single { get; set; }
    }

    public class APIModel
    {
        public string API24hr { get; set; }
        public string Coin { get; set; }
    }

    public class JobModel
    {
        public string DefaultJob { get; set; }
        public string SubcribeJob { get; set; }
    }

    public class CalculateModel
    {
        public int MCDX { get; set; }
    }
    #endregion

    public class SendNotifyModel
    {
        public string phone { get; set; }
        public string message { get; set; }
    }

    public class MiniTicketModel
    {
        public string Symbol { get; set; }
        public decimal LastPrice { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal Volume { get; set; }
        public decimal QuoteVolume { get; set; }
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

    public class LocalTicketModel
    {
        public string name { get; set; }
        public long e { get; set; }
        public float c { get; set; }
        public float o { get; set; }
        public float h { get; set; }
        public float l { get; set; }
        public float v { get; set; }
        public float q { get; set; }
        public long ut { get; set; }
        public bool state { get; set; }
    }

    public class List_LocalTicketModel
    {
        public IEnumerable<LocalTicketModel> data { get; set; }
    }

    public class CryptonDataModel
    {
        public List<CryptonDetailDataModel> Data { get; set; }
    }

    public class CryptonDetailDataModel
    {
        public string S { get; set; }
        public string AN { get; set; }
    }
}
