using Analyze.DesktopApp.Models;
using Analyze.DesktopApp.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Linq;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution]
    public class VolumeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var settings = Program.Configuration.GetSection("Domain").Get<DomainModel>();
            var content1 = StaticClass.GetWebContent10s($"{settings.Sub1}/current").GetAwaiter().GetResult();
            UpdateData(content1);
            var content2 = StaticClass.GetWebContent10s($"{settings.Sub2}/current").GetAwaiter().GetResult();
            UpdateData(content2);
            var content3 = StaticClass.GetWebContent10s($"{settings.Sub3}/current").GetAwaiter().GetResult();
            UpdateData(content3);

            void UpdateData(string content)
            {
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var response = JsonConvert.DeserializeObject<List_LocalTicketModel>(content);
                    //check data
                    if (response.data.Any())
                    {
                        foreach (var item in response.data)
                        {
                            var entityDic = StaticVal.dicVolume.FirstOrDefault(x => x.Key.Equals(item.name, StringComparison.InvariantCultureIgnoreCase));
                            if (entityDic.Key != null)
                            {
                                StaticVal.dicVolume[entityDic.Key] = item.v;
                            }
                        }
                    }
                }
            }
        }
    }
}
