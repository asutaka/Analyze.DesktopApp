using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Job.ScheduleJob;
using Analyze.DesktopApp.Models;
using Analyze.DesktopApp.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyze.DesktopApp.GUI.Child
{
    public partial class frm24H : XtraForm
    {
        public static List<API24hVM> _lst24H = new List<API24hVM>();
        public static List<API24hVM> _lstAdapter = new List<API24hVM>();
        private ScheduleMember jobValue = null;
        private frm24H()
        {
            InitializeComponent();
            var settings = Program.Configuration.GetSection("Job").Get<JobModel>();
            jobValue = new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<API24hScheduleJob>(), settings.DefaultJob, nameof(API24hScheduleJob));
            InitData();
        }

        private static frm24H _instance = null;
        public static frm24H Instance()
        {
            _instance = _instance ?? new frm24H();
            return _instance;
        }

        public void InitData()
        {
            if (!this.Visible)
            {
                jobValue.Pause();
            }
            if (!this.IsHandleCreated)
                return;
            this.Invoke((MethodInvoker)delegate
            {
                grid.BeginUpdate();
                grid.DataSource = _lst24H;
                grid.EndUpdate();
            });
            
        }

        private void frmTop30_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                jobValue.Pause();
            }
            else
            {
                if (!jobValue.IsStarted())
                {
                    jobValue.Start();
                }
                else
                {
                    jobValue.Resume();
                }
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var settings = Program.Configuration.GetSection("ViewWeb").Get<ViewWebModel>();
                DXMouseEventArgs ea = e as DXMouseEventArgs;
                GridHitInfo info = gridView1.CalcHitInfo(ea.Location);
                if (info.InRow || info.InRowCell)
                {
                    var cellValue = gridView1.GetRowCellValue(info.RowHandle, "Coin").ToString();
                    ProcessStartInfo sInfo = new ProcessStartInfo($"{settings.Single}/{cellValue.Replace("USDT", "_USDT")}");
                    Process.Start(sInfo);
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"frm24H.gridView1_DoubleClick|EXCEPTION| {ex.Message}");
            }
        }

        private void frm24H_Load(object sender, EventArgs e)
        {
            var bkgr = new BackgroundWorker();
            bkgr.DoWork += bkgrConfig_DoWork;
            bkgr.RunWorkerCompleted += bkgrConfig_RunWorkerCompleted;
            bkgr.RunWorkerAsync();
        }

        private void bkgrConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            var settings = Program.Configuration.GetSection("API").Get<APIModel>();
            var content = StaticClass.GetWebContent(settings.API24hr).GetAwaiter().GetResult();
            if (!string.IsNullOrWhiteSpace(content))
            {
                _lstAdapter = JsonConvert.DeserializeObject<IEnumerable<TicketModel>>(content)
                            .Where(x => x.symbol.EndsWith("USDT"))
                            .OrderByDescending(x => x.priceChangePercent)
                            .ToList()
                            .To<List<API24hVM>>();
                int index = 1;
                foreach (var item in _lstAdapter)
                {
                    var entityCoin = StaticVal.lstCoin.FirstOrDefault(x => x.S == item.Coin);
                    item.STT = index++;
                    item.CoinName = entityCoin == null ? string.Empty : entityCoin.AN;
                    item.PriceRef = item.lastPrice;
                }
            }
        }
        private void bkgrConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                _lst24H = _lstAdapter.Take(50).ToList();
                grid.BeginUpdate();
                grid.DataSource = _lst24H;
                grid.EndUpdate();
            });
        }
    }

    [DisallowConcurrentExecution] /*impt: no multiple instances executed concurrently*/
    public class API24hScheduleJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                //if (StaticVal.IsRealTimeAction)
                //    return;
                var lstTask = new List<Task>();
                foreach (var item in frm24H._lstAdapter)
                {
                    var task = Task.Run(() =>
                    {
                        var coin = item.Coin;
                        var entityBinanceTick =  StaticVal.GetCoinBinanceTick(coin);
                        if (entityBinanceTick == null)
                        {
                            return;
                        }
                        item.lastPrice = (float)entityBinanceTick.LastPrice;
                        item.Div = (float)Math.Round(((-1 + item.lastPrice / item.PriceRef) * 100), 1);
                        item.PriceChange = Math.Round(item.lastPrice - item.prevClosePrice, 2);
                        item.PriceChangePercent = Math.Round(((-1 + item.lastPrice / item.prevClosePrice) * 100), 1);
                    });
                    lstTask.Add(task);
                }
                Task.WaitAll(lstTask.ToArray());
                //if (StaticVal.IsRealTimeAction)
                //    return;
                frm24H._lst24H = frm24H._lstAdapter.OrderByDescending(x => x.PriceChangePercent).Take(50).ToList();

                frm24H.Instance().InitData();
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"Top30ScheduleJob.Execute|EXCEPTION| {ex.Message}");
            }
        }
    }
}