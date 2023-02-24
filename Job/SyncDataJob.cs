using Analyze.DesktopApp.Models;
using Analyze.DesktopApp.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quartz;
using System.Linq;
using System.Threading;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution]
    public class SyncDataJob : IJob
    {
        private static bool hasUpdate = false;
        public void Execute(IJobExecutionContext context)
        {
            var settings = Program.Configuration.GetSection("Domain").Get<DomainModel>();
            var content1 = StaticClass.GetWebContent10s($"{settings.Sub1}/mirror").GetAwaiter().GetResult();
            UpdateData(content1);
            var content2 = StaticClass.GetWebContent10s($"{settings.Sub2}/mirror").GetAwaiter().GetResult();
            UpdateData(content2);
            var content3 = StaticClass.GetWebContent10s($"{settings.Sub3}/mirror").GetAwaiter().GetResult();
            UpdateData(content3);
            StaticVal.isAllowCalculate = true;

            void UpdateData(string content)
            {
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var response = JsonConvert.DeserializeObject<List_LocalTicketModel>(content);
                    //check data
                    if (response.data.Any())
                    {
                        if (StaticVal.isAllowCalculate)
                        {
                            StaticVal.isAllowCalculate = false;
                            Thread.Sleep(2000);
                        }
                        foreach (var item in response.data)
                        {
                            var entityDic = StaticVal.dic1H.FirstOrDefault(x => x.Key.Equals(item.name.ToUpper()));
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
                            }
                            StaticVal.dic1H[entityDic.Key] = entityDic.Value;
                        }
                    }
                }
            }
            
        }
    }
}
