using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Models;
using Analyze.DesktopApp.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution]
    public class VolumeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var dicVolume = DataMng.AssignDicVolume();
                var settings = Program.Configuration.GetSection("Domain").Get<DomainModel>();
                var content1 = WebClass.GetWebContent10s($"{settings.Sub1}/current").GetAwaiter().GetResult();
                UpdateData(content1);
                var content2 = WebClass.GetWebContent10s($"{settings.Sub2}/current").GetAwaiter().GetResult();
                UpdateData(content2);
                var content3 = WebClass.GetWebContent10s($"{settings.Sub3}/current").GetAwaiter().GetResult();
                UpdateData(content3);
                StaticVal.dicVolume = dicVolume;

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
                                var entityDic = dicVolume.FirstOrDefault(x => x.Key.Equals(item.name, StringComparison.InvariantCultureIgnoreCase));
                                if (entityDic.Key != null)
                                {
                                    dicVolume[entityDic.Key] = item.v;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"VolumeJob.Execute|EXCEPTION| {ex.Message}");
            }
        }
    }
}
