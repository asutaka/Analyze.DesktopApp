﻿using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.GUI.Child;
using Analyze.DesktopApp.Models;
using Analyze.DesktopApp.Utils;
using DevExpress.XtraTab;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace Analyze.DesktopApp.GUI
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //private WaitFunc _frmWaitForm = new WaitFunc();
        private BackgroundWorker _bkgr;
        //private bool _checkConnection;
        private frmMain()
        {
            InitializeComponent();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("McSkin");
            ribbon.Enabled = false;
            //StaticValues.IsAccessMain = true;
            //_bkgr = new BackgroundWorker();
            //_bkgr.DoWork += bkgrConfig_DoWork;
            //_bkgr.RunWorkerCompleted += bkgrConfig_RunWorkerCompleted;
            //_bkgr.RunWorkerAsync();
            //StaticValues.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticValues.ScheduleMngObj.GetScheduler(), JobBuilder.Create<TradeListNotifyJob>(), StaticValues.Scron_TradeList_Noti, nameof(TradeListNotifyJob)));
            //StaticValues.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticValues.ScheduleMngObj.GetScheduler(), JobBuilder.Create<FollowListJob>(), StaticValues.followList.Cron, nameof(FollowListJob)));
        }

        private static frmMain _instance = null;
        public static frmMain Instance()
        {
            _instance = (_instance == null || _instance.IsDisposed) ? new frmMain() : _instance;
            return _instance;
        }

        private void bkgrConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tabControl.AddTab(frm24H.Instance());
            });

            var settings = Program.Configuration.GetSection("Domain").Get<DomainModel>();
            //var dt = DateTime.Now;
            var lstTask = new List<Task>();
            //1
            lstTask.Add(Task.Run(() =>
            {
                GetData(settings.Sub1, ConfigVal._lstSub1);
            }));
            //2
            lstTask.Add(Task.Run(() =>
            {
                GetData(settings.Sub2, ConfigVal._lstSub2);
            }));
            //3
            lstTask.Add(Task.Run(() =>
            {
                GetData(settings.Sub3, ConfigVal._lstSub3);
            }));
            //4
            lstTask.Add(Task.Run(() =>
            {
                GetData(settings.Sub4, ConfigVal._lstSub4);
            }));
            //5
            lstTask.Add(Task.Run(() =>
            {
                GetData(settings.Sub5, ConfigVal._lstSub5);
            }));
            //6
            lstTask.Add(Task.Run(() =>
            {
                GetData(settings.Sub6, ConfigVal._lstSub6);
            }));
            //7
            lstTask.Add(Task.Run(() =>
            {
                GetData(settings.Sub7, ConfigVal._lstSub7);
            }));
            //8
            lstTask.Add(Task.Run(() =>
            {
                GetData(settings.Sub8, ConfigVal._lstSub8);
            }));
            Task.WaitAll(lstTask.ToArray());
            //TimeSpan span = DateTime.Now - dt;
        }
        List<string> _lstCoinError = new List<string>();
        private void GetData(string url, List<string> _lst)
        {
            int index = 1;
            foreach (var item in _lst)
            {
                try
                {
                    var coin = StaticVal.lstCoin.FirstOrDefault(x => x.S == item.ToUpper());
                    if (coin != null)
                    {
                        var content = StaticClass.GetWebContent($"{url}/symbol/{index++}").GetAwaiter().GetResult();
                        if (!string.IsNullOrWhiteSpace(content))
                        {
                            StaticVal.dic1H.Add(coin.S, JsonConvert.DeserializeObject<List_LocalTicketModel>(content).data);
                        }
                        else
                        {
                            _lstCoinError.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _lstCoinError.Add(item);
                    NLogLogger.PublishException(ex, $"frmMain.GetData|EXCEPTION|INPUT: {JsonConvert.SerializeObject(item)}| {ex.Message}");
                }
            }
        }

        private void bkgrConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _bkgr.DoWork -= bkgrConfig_DoWork;
            _bkgr.RunWorkerCompleted -= bkgrConfig_RunWorkerCompleted;
            _bkgr.DoWork += bkgrAnalyze_DoWork;
            _bkgr.RunWorkerCompleted += bkgrAnalyze_RunWorkerCompleted;
            _bkgr.RunWorkerAsync();
        }

        private void bkgrAnalyze_DoWork(object sender, DoWorkEventArgs e)
        {
            //dtStartCalculate = DateTime.Now;
            //_frmWaitForm.Show("Phân tích dữ liệu");
            //StaticValues.lstCryptonRank = CalculateMng.Top30();
            //Thread.Sleep(200);
            //_frmWaitForm.Close();
        }
        private void bkgrAnalyze_RunWorkerCompleted(object sender1, RunWorkerCompletedEventArgs e1)
        {
            ribbon.Enabled = true;
            _bkgr.DoWork -= bkgrAnalyze_DoWork;
            _bkgr.RunWorkerCompleted -= bkgrAnalyze_RunWorkerCompleted;
            //_bkgr.DoWork += bkgrPrepareRealTime_DoWork;
            //_bkgr.RunWorkerCompleted += bkgrPrepareRealTime_RunWorkerCompleted;
            //_bkgr.RunWorkerAsync();
        }

        //private void bkgrPrepareRealTime_DoWork(object sender1, DoWorkEventArgs e1)
        //{
        //    //15M
        //    if (StaticValues.advanceModel1.LstInterval.Contains((int)enumTimeZone.ThirteenMinute)
        //        || StaticValues.advanceModel2.LstInterval.Contains((int)enumTimeZone.ThirteenMinute)
        //        || StaticValues.advanceModel3.LstInterval.Contains((int)enumTimeZone.ThirteenMinute)
        //        || StaticValues.advanceModel4.LstInterval.Contains((int)enumTimeZone.ThirteenMinute))
        //    {
        //        var wrkr = new BackgroundWorker();
        //        wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
        //            var lstTask = new List<Task>();
        //            foreach (var item in StaticValues.lstCoinFilter)
        //            {
        //                var task = Task.Run(() =>
        //                {
        //                    StaticValues.dicDatasource15M.Add(item.S, SeedData.LoadDatasource(item.S, (int)enumInterval.ThirteenMinute));
        //                });
        //                lstTask.Add(task);
        //            }
        //            Task.WaitAll(lstTask.ToArray());
        //        };
        //        wrkr.RunWorkerAsync();
        //    }
        //    //4H
        //    if (StaticValues.advanceModel1.LstInterval.Contains((int)enumTimeZone.FourHour)
        //      || StaticValues.advanceModel2.LstInterval.Contains((int)enumTimeZone.FourHour)
        //      || StaticValues.advanceModel3.LstInterval.Contains((int)enumTimeZone.FourHour)
        //      || StaticValues.advanceModel4.LstInterval.Contains((int)enumTimeZone.FourHour))
        //    {
        //        var wrkr = new BackgroundWorker();
        //        wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
        //            var lstTask = new List<Task>();
        //            foreach (var item in StaticValues.lstCoinFilter)
        //            {
        //                var task = Task.Run(() =>
        //                {
        //                    StaticValues.dicDatasource4H.Add(item.S, SeedData.LoadDatasource(item.S, (int)enumInterval.FourHour));
        //                });
        //                lstTask.Add(task);
        //            }
        //            Task.WaitAll(lstTask.ToArray());
        //        };
        //        wrkr.RunWorkerAsync();
        //    }
        //    //1D
        //    if (StaticValues.advanceModel1.LstInterval.Contains((int)enumTimeZone.OneDay)
        //      || StaticValues.advanceModel2.LstInterval.Contains((int)enumTimeZone.OneDay)
        //      || StaticValues.advanceModel3.LstInterval.Contains((int)enumTimeZone.OneDay)
        //      || StaticValues.advanceModel4.LstInterval.Contains((int)enumTimeZone.OneDay))
        //    {
        //        var wrkr = new BackgroundWorker();
        //        wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
        //            var lstTask = new List<Task>();
        //            foreach (var item in StaticValues.lstCoinFilter)
        //            {
        //                var task = Task.Run(() =>
        //                {
        //                    StaticValues.dicDatasource1D.Add(item.S, SeedData.LoadDatasource(item.S, (int)enumInterval.OneDay));
        //                });
        //                lstTask.Add(task);
        //            }
        //            Task.WaitAll(lstTask.ToArray());
        //        };
        //        wrkr.RunWorkerAsync();
        //    }
        //    //1W
        //    if (StaticValues.advanceModel1.LstInterval.Contains((int)enumTimeZone.OneWeek)
        //      || StaticValues.advanceModel2.LstInterval.Contains((int)enumTimeZone.OneWeek)
        //      || StaticValues.advanceModel3.LstInterval.Contains((int)enumTimeZone.OneWeek)
        //      || StaticValues.advanceModel4.LstInterval.Contains((int)enumTimeZone.OneWeek))
        //    {
        //        var wrkr = new BackgroundWorker();
        //        wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
        //            var lstTask = new List<Task>();
        //            foreach (var item in StaticValues.lstCoinFilter)
        //            {
        //                var task = Task.Run(() =>
        //                {
        //                    StaticValues.dicDatasource1W.Add(item.S, SeedData.LoadDatasource(item.S, (int)enumInterval.OneWeek));
        //                });
        //                lstTask.Add(task);
        //            }
        //            Task.WaitAll(lstTask.ToArray());
        //        };
        //        wrkr.RunWorkerAsync();
        //    }
        //    //1Month
        //    if (StaticValues.advanceModel1.LstInterval.Contains((int)enumTimeZone.OneMonth)
        //      || StaticValues.advanceModel2.LstInterval.Contains((int)enumTimeZone.OneMonth)
        //      || StaticValues.advanceModel3.LstInterval.Contains((int)enumTimeZone.OneMonth)
        //      || StaticValues.advanceModel4.LstInterval.Contains((int)enumTimeZone.OneMonth))
        //    {
        //        var wrkr = new BackgroundWorker();
        //        wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
        //            var lstTask = new List<Task>();
        //            foreach (var item in StaticValues.lstCoinFilter)
        //            {
        //                var task = Task.Run(() =>
        //                {
        //                    StaticValues.dicDatasource1Month.Add(item.S, SeedData.LoadDatasource(item.S, (int)enumInterval.OneMonth));
        //                });
        //                lstTask.Add(task);
        //            }
        //            Task.WaitAll(lstTask.ToArray());
        //        };
        //        wrkr.RunWorkerAsync();
        //    }
        //}

        //private void bkgrPrepareRealTime_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    this.Invoke((MethodInvoker)delegate
        //    {
        //        tabControl.AddTab(frmTop30.Instance());
        //    });
        //    try
        //    {
        //        foreach (var process in Process.GetProcessesByName(ConstantValue.serviceName))
        //        {
        //            process.Kill();
        //        }
        //        Process.Start($"{Directory.GetCurrentDirectory()}\\service\\{ConstantValue.serviceName}.exe");
        //    }
        //    catch(Exception ex)
        //    {
        //        NLogLogger.PublishException(ex, $"frmMain: {ex.Message}");
        //    }
        //}

        private void barBtnInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tabControl.AddTab(frmMyList.Instance());
            });
        }

        private void barBtnTop30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.Invoke((MethodInvoker)delegate
            //{
            //    tabControl.AddTab(frmTop30.Instance());
            //});
        }

        private void barBtnListTrade_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.Invoke((MethodInvoker)delegate
            //{
            //    tabControl.AddTab(frmTradeList.Instance());
            //});
        }

        private void barBtnListFollow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var tmp = StaticValues.ScheduleMngObj.GetSchedules().ElementAt(0);
            //if (tmp.IsStarted())
            //{
            //    tmp.Pause();
            //    tmp.Resume();
            //}
            //else
            //{
            //    tmp.Start();
            //}
            //this.Invoke((MethodInvoker)delegate
            //{
            //    tabControl.AddTab(frmFollowList.Instance());
            //});
        }

        private void barBtnBlackList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.Invoke((MethodInvoker)delegate
            //{
            //    tabControl.AddTab(frmBlackList.Instance());
            //});
        }

        private void barBtnConfigFx_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.Invoke((MethodInvoker)delegate
            //{
            //    tabControl.AddTab(frmConfigFx.Instance());
            //});
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*kill all running process
            * https://stackoverflow.com/questions/8507978/exiting-a-c-sharp-winforms-application
            */
            //try
            //{
            //    foreach (var process in Process.GetProcessesByName(ConstantValue.serviceName))
            //    {
            //        process.Kill();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    NLogLogger.PublishException(ex, $"frmMain: {ex.Message}");
            //}
            Process.GetCurrentProcess().Kill();
            Application.Exit();
            Environment.Exit(0);
        }

        private void tabControl_CloseButtonClick(object sender, EventArgs e)
        {
            //if (tabControl.TabPages.Count == 1)
            //    return;
            var EArg = (DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs)e;
            string name = EArg.Page.Text;//Get the text of the closed tab
            foreach (XtraTabPage page in tabControl.TabPages)//Traverse to get the same text as the closed tab
            {
                if (page.Text == name)
                {
                    tabControl.TabPages.Remove(page);
                    return;
                }
            }
        }

        private void barBtnRealTime_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.Invoke((MethodInvoker)delegate
            //{
            //    tabControl.AddTab(frmRealTime.Instance());
            //});
        }

        private void barBtnStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barBtnStart.Enabled = false;
            barBtnStop.Enabled = true;

            //StaticValues.ScheduleMngObj.StartAllJob();
        }

        private void barBtnStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barBtnStart.Enabled = true;
            barBtnStop.Enabled = false;

            //StaticValues.ScheduleMngObj.StopAllJob();
        }

        private void barBtnMCDX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.Invoke((MethodInvoker)delegate
            //{
            //    tabControl.AddTab(frmMCDX.Instance());
            //});
        }

        private void barBtnSupport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barBtnSupport.Enabled = false;
            //ConstantValue.strSupport.CreateFile("Support", true);
            Thread.Sleep(1000);
            barBtnSupport.Enabled = true;
        }

        private void barBtn24h_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tabControl.AddTab(frm24H.Instance());
            });
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _bkgr = new BackgroundWorker();
            _bkgr.DoWork += bkgrConfig_DoWork;
            _bkgr.RunWorkerCompleted += bkgrConfig_RunWorkerCompleted;
            _bkgr.RunWorkerAsync();
        }

        private void barBtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCoinInfo.Instance().MakeShow();
        }
    }
}