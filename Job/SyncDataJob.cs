using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Models;
using Analyze.DesktopApp.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution]
    public class SyncDataJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            LogM.Log("Start Sync");
            if (StaticVal.isAllowCalculate)
            {
                StaticVal.isAllowCalculate = false;
                Thread.Sleep(2000);
            }
            var dic1H = DataMng.AssignDic1h();

            var settings = Program.Configuration.GetSection("Domain").Get<DomainModel>();
            var content1 = WebClass.GetWebContent10s($"{settings.Sub1}/mirror").GetAwaiter().GetResult();
            UpdateData(content1);
            var content2 = WebClass.GetWebContent10s($"{settings.Sub2}/mirror").GetAwaiter().GetResult();
            UpdateData(content2);
            var content3 = WebClass.GetWebContent10s($"{settings.Sub3}/mirror").GetAwaiter().GetResult();
            UpdateData(content3);
            StaticVal.dic1H = dic1H;
            StaticVal.isAllowCalculate = true;
            LogM.Log("End Sync");

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
                            var entityDic = dic1H.FirstOrDefault(x => x.Key.Equals(item.name, StringComparison.InvariantCultureIgnoreCase));
                            if (entityDic.Key != null)
                            {
                                var entityData = entityDic.Value.Last();
                                if (item.e < entityData.e)
                                    continue;
                                if (item.e == entityData.e)
                                {
                                    entityDic.Value.Remove(entityData);
                                }
                                entityDic.Value.Add(new LocalTicketModel
                                {
                                    name = item.name,
                                    e = item.e,
                                    c = item.c,
                                    o = item.o,
                                    h = item.h,
                                    l = item.l,
                                    v = item.v,
                                    q = item.q,
                                    ut = item.ut,
                                    state = item.state
                                });
                                dic1H[entityDic.Key] = entityDic.Value;
                            }
                        }
                    }
                }
            }
            
        }
    }
}
