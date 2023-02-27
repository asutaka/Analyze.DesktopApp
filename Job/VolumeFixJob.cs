using Analyze.DesktopApp.Common;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Linq;
using System.Threading;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution]
    public class VolumeFixJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                LogM.Log("Start Fix Volume");
                if (StaticVal.isAllowCalculate)
                {
                    StaticVal.isAllowCalculate = false;
                    Thread.Sleep(2000);
                }
                var _dic = StaticVal.dicVolumeFix;
                foreach (var item in StaticVal.dic1H)
                {
                    try
                    {
                        float sumVolume = 0;
                        var count = item.Value.Count();
                        if(count < 23)
                        {
                            sumVolume = item.Value.Sum(x => x.v);
                        }
                        else
                        {
                            sumVolume = item.Value.Skip(count - 23).Take(23).Sum(x => x.v);
                        }

                        var entity = _dic.FirstOrDefault(x => x.Key == item.Key);
                        if (entity.Key == null)
                        {
                            _dic.Add(item.Key, sumVolume);
                        }
                        else
                        {
                            _dic[item.Key] = sumVolume;
                        }
                    }
                    catch (Exception ex)
                    {
                        NLogLogger.PublishException(ex, $"VolumeFixJob.Execute|EXCEPTION|INPUT: {JsonConvert.SerializeObject(item)}| {ex.Message}");
                    }
                }
                StaticVal.dicVolumeFix = _dic;
            }
            catch (Exception exm)
            {
                NLogLogger.PublishException(exm, $"VolumeFixJob.Execute|EXCEPTION| {exm.Message}");
            }

            try
            {
                StaticVal.jobVolumeFix.Pause();
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"VolumeFixJob.Execute|EXCEPTION(Cannot end Job)| {ex.Message}");
            }
            StaticVal.isAllowCalculate = true;
            LogM.Log("End Fix Volume");
        }
    }
}
