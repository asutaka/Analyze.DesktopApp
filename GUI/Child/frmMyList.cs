using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Models;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Analyze.DesktopApp.GUI.Child
{
    public partial class frmMyList : XtraForm
    {
        private readonly string _fileName = "coin_follow.json";
        public static List<API24hVM> _lst24H = new List<API24hVM>();
        private CoinFollowModel _coinFollowModel;

        private frmMyList()
        {
            InitializeComponent();
        }

        private static frmMyList _instance = null;
        public static frmMyList Instance()
        {
            _instance = (_instance == null || _instance.IsDisposed) ? new frmMyList() : _instance;
            return _instance;
        }

        public void InitData()
        {
            if (!this.IsHandleCreated)
                return;
            this.Invoke((MethodInvoker)delegate
            {
                var lData = StaticVal.lst24H.Where(x => _coinFollowModel.lData.Any(y => y.Symbol.Equals(x.Coin)));
                foreach (var item in lData)
                {
                    int rowHandle = gridView1.LocateByValue("Coin", item.Coin);
                    gridView1.SetRowCellValue(rowHandle, "PriceChange", item.PriceChange);
                    gridView1.SetRowCellValue(rowHandle, "PriceChangePercent", item.PriceChangePercent);
                    gridView1.SetRowCellValue(rowHandle, "lastPrice", item.lastPrice);
                    gridView1.SetRowCellValue(rowHandle, "Div", item.Div);
                }
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
            InitList();
        }

        public void InitList()
        {
            StaticVal.frmMyListReady = false;
            Thread.Sleep(1000);
            _coinFollowModel = new CoinFollowModel().LoadJsonFile(_fileName);
            if (_coinFollowModel != null)
            {
                _lst24H = StaticVal.lst24H.Where(x => _coinFollowModel.lData.Any(y => y.Symbol.Equals(x.Coin))).ToList();
            }
            var STT = 1;
            foreach (var item in _lst24H)
            {
                var entity = _coinFollowModel.lData.FirstOrDefault(x => x.Symbol.Equals(item.Coin));
                item.PriceRef = entity != null ? (float)entity.Value : 0;
                item.STT = STT++;
            }
            StaticVal.frmMyListReady = true;
            grid.BeginUpdate();
            grid.DataSource = _lst24H;
            grid.EndUpdate();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                int[] selRows = ((GridView)grid.MainView).GetSelectedRows();
                if (selRows.Length <= 0)
                    return;
                var entityRow = (API24hVM)(((GridView)grid.MainView).GetRow(selRows[0]));
                if (MessageBox.Show($"Xóa coin {entityRow.Coin} khỏi danh sách Follow?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var entity = _coinFollowModel.lData.Find(x => x.Symbol.Equals(entityRow.Coin));
                    if(entity != null)
                    {
                        _coinFollowModel.lData.Remove(entity);
                        _coinFollowModel.UpdateJson(_fileName);
                        InitList();
                    }

                    Security.MesSuccess();
                }
            }
        }
    }
}