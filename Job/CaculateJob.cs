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
        public void Execute(IJobExecutionContext context)
        {
            if (!StaticVal.isAllowCalculate)
                return;
            CalculateMng._dic1H = DataMng.AssignDic1h();
            CalculateMng._dicVolume = DataMng.AssignDicVolume();
            CalculateMng._dicVolumeCal = DataMng.AssignDicVolumeCalculate();
            CalculateMng._binanceTicks = StaticVal.binanceTicks;
            CalculateMng._lstCoin = StaticVal.lstCoin;

            var lstTask = new List<Task>();
            var task = Task.Run(() =>
            {
                DataMng.AssignlMCDX(CalculateMng.MCDX());
            });
            lstTask.Add(task);

            var task1 = Task.Run(() =>
            {
                DataMng.AssignDicVolumeCalculate(CalculateMng.CalculateVolume());
            });
            lstTask.Add(task1);

            Task.WaitAll(lstTask.ToArray());
            
            
            //LogM.Start();
            //var tmp = CalculateMng.Top30();
            //LogM.Log(JsonConvert.SerializeObject(tmp.Select(x => new { x.Coin, x.Count })));
            //LogM.Stop();
            ////var tmp1 = 1;
        }
    }
}
