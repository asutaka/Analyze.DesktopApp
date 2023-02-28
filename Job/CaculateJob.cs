using Quartz;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution]
    public class CaculateJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //var dic = StaticVal.dic1H;
            //var dicVolume = StaticVal.dicVolume;

            if (!StaticVal.isAllowCalculate)
                return;
            var lMCDX = CalculateMng.MCDX();
            StaticVal.lstMCDX = lMCDX;
        }
    }
}
