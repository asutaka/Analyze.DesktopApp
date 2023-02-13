using Newtonsoft.Json;

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
}
