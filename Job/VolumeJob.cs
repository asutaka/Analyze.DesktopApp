using Analyze.DesktopApp.Models;
using Analyze.DesktopApp.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Linq;
using static TicTacTec.TA.Library.Core;

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
                                double MA20 = 0;
                                double percent = 0;
                                var entityDicData = StaticVal.dic1H.FirstOrDefault(x => x.Key.Equals(item.name, StringComparison.InvariantCultureIgnoreCase));
                                if(entityDicData.Key != null)
                                {
                                    var count = entityDicData.Value.Count();
                                    if (count >= 20)
                                    {
                                        var list = entityDicData.Value.Select(x => (double)x.v).ToList();
                                        list.Add(item.v);
                                        MA20 = CalculateMng.MA(list.ToArray(), MAType.Sma, 20, count + 1);
                                        if(MA20 > 0)
                                        {
                                            percent = Math.Round(item.v * 100 / MA20, 2);
                                        }
                                    }
                                }
                                StaticVal.dicVolume[entityDic.Key] = new Tuple<float, float, float>(item.v, (float)MA20, (float)percent);
                            }
                        }
                    }
                }
            }
        }
    }
}
