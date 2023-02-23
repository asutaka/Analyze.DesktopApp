using Quartz;

namespace Analyze.DesktopApp.Job
{
    [DisallowConcurrentExecution]
    public class CaculateJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            if (!StaticVal.isAllowCalculate)
                return;
            var lMCDX = CalculateMng.MCDX();
            StaticVal.lstMCDX = lMCDX;
        }
    }
}
