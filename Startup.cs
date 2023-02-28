using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Job;
using Analyze.DesktopApp.Job.ScheduleJob;
using Analyze.DesktopApp.Models;
using Analyze.DesktopApp.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Linq;

namespace Analyze.DesktopApp
{
    public class Startup
    {
        private Startup()
        {
            InitData();
        }
        private static Startup _instance = null;
        public static Startup Instance()
        {
            _instance = _instance ?? new Startup();
            return _instance;
        }
        private void InitData()
        {
            try
            {
                var settings = Program.Configuration.GetSection("Job").Get<JobModel>();
                new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<SubcribeJob>(), settings.SubcribeJob, nameof(SubcribeJob)).Start();
                new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<API24hScheduleJob>(), settings.DefaultJob, nameof(API24hScheduleJob)).Start();

                var settingsAPI = Program.Configuration.GetSection("API").Get<APIModel>();
                var content = StaticClass.GetWebContent(settingsAPI.Coin).GetAwaiter().GetResult();
                if (!string.IsNullOrWhiteSpace(content))
                {
                    StaticVal.lstCoin = JsonConvert.DeserializeObject<CryptonDataModel>(content).Data
                                .Where(x => x.S.EndsWith("USDT"))
                                .OrderBy(x => x.S).ToList();
                    foreach (var item in StaticVal.lstCoin)
                    {
                        StaticVal.dicVolume.Add(item.S, new Tuple<float, float, float>(0, 0, 0));
                    }
                    new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<VolumeJob>(), Program.Configuration.GetSection("Job").Get<JobModel>().VolumeJob, nameof(VolumeJob)).Start();
                }

                var settingDomain = Program.Configuration.GetSection("Domain").Get<DomainModel>();
                var contentTime = StaticClass.GetWebContent10s($"{settingDomain.Sub1}/time").GetAwaiter().GetResult();
                if (!string.IsNullOrWhiteSpace(contentTime))
                {
                    var timeStart = DateTime.Now;
                    var model = JsonConvert.DeserializeObject<TimeModel>(contentTime);
                    var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dateTime = dateTime.AddMilliseconds(model.Data).ToLocalTime();
                    StaticVal.TimeSynData = Math.Abs((int)(2 + ((timeStart - dateTime).TotalSeconds) % 60));
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"Startup.InitData|EXCEPTION| {ex.Message}");
            }
        }
    }
}
