using Analyze.DesktopApp.Utils;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TicTacTec.TA.Library.Core;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution]
    public class CaculateJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            if (!StaticVal.isAllowCalculate)
                return;
            
            var lstTask = new List<Task>();
            var task = Task.Run(() =>
            {
                CalculateMng.MCDX();
            });
            lstTask.Add(task);

            var dic1H = DataMng.AssignDic1h();
            var dicVolume = DataMng.AssignDicVolume();
            var dicVolumeCal = DataMng.AssignDicVolumeCalculate();
            var task1 = Task.Run(() =>
            {
                CalculateVolume();
            });
            lstTask.Add(task1);

            Task.WaitAll(lstTask.ToArray());
            StaticVal.dicVolumeCalculate = dicVolumeCal;

            void CalculateVolume()
            {
                foreach (var item in dicVolume)
                {
                    if (item.Value <= 0)
                        continue;
                    var entityDicData = dic1H.FirstOrDefault(x => x.Key.Equals(item.Key, StringComparison.InvariantCultureIgnoreCase));
                    if(entityDicData.Key != null)
                    {
                        var count = entityDicData.Value.Count();
                        if (count >= 20)
                        {
                            var entityCal = dicVolumeCal.FirstOrDefault(x => x.Key == item.Key);
                            if (entityCal.Key != null)
                            {
                                var list = entityDicData.Value.Select(x => (double)x.v).ToList();
                                list.Add(item.Value);
                                double MA20 = 0;
                                double percent = 0;
                                MA20 = CalculateMng.MA(list.ToArray(), MAType.Sma, 20, count + 1);
                                if (MA20 > 0)
                                {
                                    percent = Math.Round(item.Value * 100 / MA20, 2);
                                }
                                dicVolumeCal[entityCal.Key] = new Tuple<float, float>((float)MA20, (float)percent);
                            }
                        }
                    }
                }
            }
        }
    }
}
