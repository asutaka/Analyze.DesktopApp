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
            CalculateMng._dic1H = DataMng.AssignDic1h();
            CalculateMng._dicVolume = DataMng.AssignDicVolume();
            CalculateMng._dicVolumeCal = DataMng.AssignDicVolumeCalculate();
            CalculateMng._binanceTicks = StaticVal.binanceTicks;
            CalculateMng._lstCoin = StaticVal.lstCoin;

            var lstTask = new List<Task>();
            var task = Task.Run(() =>
            {
                CalculateMng.MCDX();
            });
            lstTask.Add(task);

            var task1 = Task.Run(() =>
            {
                CalculateMng.CalculateVolume();
            });
            lstTask.Add(task1);

            Task.WaitAll(lstTask.ToArray());
        }
    }
}
