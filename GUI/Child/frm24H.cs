﻿using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Job;
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
using System.Windows.Forms;

namespace Analyze.DesktopApp.GUI.Child
{
    public partial class frm24H : XtraForm
    {
        public static List<API24hVM> _lst24H = new List<API24hVM>();
       
        private frm24H()
        {
            InitializeComponent();
            InitData();
        }

        private static frm24H _instance = null;
        public static frm24H Instance()
        {
            _instance = (_instance == null || _instance.IsDisposed) ? new frm24H() : _instance;
            return _instance;
        }

        public void InitData()
        {
            if (!this.IsHandleCreated)
                return;
            this.Invoke((MethodInvoker)delegate
            {
                grid.BeginUpdate();
                grid.DataSource = _lst24H;
                grid.EndUpdate();
            });
            
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
            var content = WebClass.GetWebContent(settings.API24hr).GetAwaiter().GetResult();
            if (!string.IsNullOrWhiteSpace(content))
            {
                StaticVal.lst24H = JsonConvert.DeserializeObject<IEnumerable<TicketModel>>(content)
                            .Where(x => x.symbol.EndsWith("USDT"))
                            .OrderByDescending(x => x.priceChangePercent)
                            .ToList()
                            .To<List<API24hVM>>();
                int index = 1;
                foreach (var item in StaticVal.lst24H)
                {
                    var entityCoin = StaticVal.lstCoin.FirstOrDefault(x => x.S == item.Coin);
                    item.STT = index++;
                    item.CoinName = entityCoin == null ? string.Empty : entityCoin.AN;
                    item.PriceRef = item.lastPrice;
                    item.volume = 0;
                }
                var settingsJob = Program.Configuration.GetSection("Job").Get<JobModel>();
                new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<API24hScheduleJob>(), settingsJob.DefaultJob, nameof(API24hScheduleJob)).Start();
            }
        }
        private void bkgrConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                _lst24H = StaticVal.lst24H.Take(50).ToList();
                grid.BeginUpdate();
                grid.DataSource = _lst24H;
                grid.EndUpdate();
                frmMain.Instance().barBtn24h.Enabled = true;
                frmMain.Instance().barBtnAdd.Enabled = true;
                frmMain.Instance().barBtnMyList.Enabled = true;
                StaticVal.frm24HReady = true;
            });
        }
    }

    
}