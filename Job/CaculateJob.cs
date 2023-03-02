using Analyze.DesktopApp.Utils;
using Newtonsoft.Json;
using Quartz;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution]
    public class CaculateJob : IJob
    {
        private static long count = 0;
        public void Execute(IJobExecutionContext context)
        {
            if (!StaticVal.isAllowCalculate)
                return;
            count++;
            CalculateMng._dic1H = DataMng.AssignDic1h();
            CalculateMng._dicVolume = DataMng.AssignDicVolume();
            CalculateMng._dicVolumeCal = DataMng.AssignDicVolumeCalculate();
            CalculateMng._binanceTicks = StaticVal.binanceTicks;
            CalculateMng._lstCoin = StaticVal.lstCoin;

            var lstTask = new List<Task>();
            var taskMCDX = Task.Run(() =>
            {
                DataMng.AssignlMCDX(CalculateMng.MCDX());
            });
            lstTask.Add(taskMCDX);

            var taskVolumeCal = Task.Run(() =>
            {
                DataMng.AssignDicVolumeCalculate(CalculateMng.CalculateVolume());
            });
            lstTask.Add(taskVolumeCal);

            var taskTop30 = Task.Run(() =>
            {
                if (count % 3600 == 0 || !StaticVal.cryptonRank.IsSuccess)
                {
                    DataMng.AssignTop30(CalculateMng.Top30());
                }
            });
            lstTask.Add(taskTop30);

            Task.WaitAll(lstTask.ToArray());
        }
    }
}
