using Analyze.DesktopApp.Models;
using Analyze.DesktopApp.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution]
    public class SyncDataJob : IJob
    {
        private static bool hasUpdate = false;
        public void Execute(IJobExecutionContext context)
        {
            var time = DateTime.Now;
            if(time.Minute == 5 && !hasUpdate)
            {
                //cập nhật bằng tay
                hasUpdate = true;
                return;
            }
            if(time.Minute > 5 && hasUpdate)
            {
                hasUpdate = false;
                return;
            }    
            if (time.Minute < 55 || time.Minute > 4)
                return;
            if (hasUpdate)
                return;
            
            var settings = Program.Configuration.GetSection("Domain").Get<DomainModel>();
            var content1 = StaticClass.GetWebContent10s($"{settings.Sub1}/mirror").GetAwaiter().GetResult();
            
            if (!string.IsNullOrWhiteSpace(content1))
            {
                var res = JsonConvert.DeserializeObject<List_LocalTicketModel>(content1);
                //check data
                if (!res.data.Any())
                    return;
                var firstRes = res.data.First();
                var entityDic = StaticVal.dic1H.FirstOrDefault(x => x.Key.Equals(firstRes.name.ToUpper()));
                if (entityDic.Key == null)
                    return;
                var entityData = entityDic.Value.Last();
                if (firstRes.e < entityData.e)
                    return;
                //Update
                StaticVal.isAllowCalculate = false;
                Thread.Sleep();


                foreach (var item in res1.data)
                {
                    var entityDic = StaticVal.dic1H.FirstOrDefault(x => x.Key.Equals(item.name.ToUpper()));
                    if(entityDic.Key != null)
                    {
                        var entityData = entityDic.Value.Last();
                        if (item.e < entityData.e)
                            return;


                    }    
                }


                //StaticVal.dic1H.Add(item, arr.Select(x => new LocalTicketModel
                //{
                //    name = item.ToLower(),
                //    e = (long)x[0],
                //    o = float.Parse(x[1].ToString()),
                //    h = float.Parse(x[2].ToString()),
                //    l = float.Parse(x[3].ToString()),
                //    c = float.Parse(x[4].ToString()),
                //    v = float.Parse(x[5].ToString()),
                //    q = float.Parse(x[7].ToString()),
                //    state = true,
                //    ut = time
                //}));

                //StaticVal.lstError.Remove(item.ToString());
            }
            var content2 = StaticClass.GetWebContent10s($"{settings.Sub2}/mirror").GetAwaiter().GetResult();
            var content3 = StaticClass.GetWebContent10s($"{settings.Sub3}/mirror").GetAwaiter().GetResult();

            //var lMCDX = CalculateMng.MCDX();
            //StaticVal.lstMCDX = lMCDX;
        }
    }
}
