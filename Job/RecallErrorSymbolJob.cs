using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Models;
using Analyze.DesktopApp.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using System;
using System.Linq;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution]
    public class RecallErrorSymbolJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var settings = Program.Configuration.GetSection("API").Get<APIModel>();
                var lError = StaticVal.lstError.ToList();
                foreach (var item in lError)
                {
                    try
                    {
                        var content = WebClass.GetWebContent10s(string.Format(settings.History, item)).GetAwaiter().GetResult();
                        if (!string.IsNullOrWhiteSpace(content))
                        {
                            var time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            var arr = JArray.Parse(content);
                            StaticVal.dic1H.Add(item, arr.Select(x => new LocalTicketModel
                            {
                                name = item.ToLower(),
                                e = (long)x[0],
                                o = float.Parse(x[1].ToString()),
                                h = float.Parse(x[2].ToString()),
                                l = float.Parse(x[3].ToString()),
                                c = float.Parse(x[4].ToString()),
                                v = float.Parse(x[5].ToString()),
                                q = float.Parse(x[7].ToString()),
                                state = true,
                                ut = time
                            }).ToList());

                            if(arr.Count() > 0)
                            {
                                StaticVal.dic1H[item].Remove(StaticVal.dic1H[item].Last());
                            }

                            StaticVal.lstError.Remove(item.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        NLogLogger.PublishException(ex, $"RecallErrorSymbolJob.Execute|EXCEPTION|INPUT: {JsonConvert.SerializeObject(item)}| {ex.Message}");
                    }
                }
            }
            catch(Exception exm)
            {
                NLogLogger.PublishException(exm, $"RecallErrorSymbolJob.Execute|EXCEPTION| {exm.Message}");
            }

            try
            {
                if (StaticVal.lstError.Count() < 10)
                {
                    //stop job
                    StaticVal.jobError.Pause();
                    LogM.Stop();
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"RecallErrorSymbolJob.Execute|EXCEPTION(Cannot end Job)| {ex.Message}");
            }
        }
    }
}
