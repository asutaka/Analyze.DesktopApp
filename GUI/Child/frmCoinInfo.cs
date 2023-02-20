using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Models;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Analyze.DesktopApp.GUI.Child
{
    public partial class frmCoinInfo : XtraForm
    {
        private static frmCoinInfo _instance = null;
        private readonly string _fileName = "coin_follow.json";
        private CoinFollowModel _coinFollowModel;
        private frmCoinInfo()
        {
            InitializeComponent();
        }

        public static frmCoinInfo Instance()
        {
            _instance = (_instance == null || _instance.IsDisposed) ? new frmCoinInfo() : _instance;
            return _instance;
        }

        public void MakeShow()
        {
            InitData();
            this.Show();
        }

        private void InitData()
        {
            _coinFollowModel = new CoinFollowModel().LoadJsonFile(_fileName);
            if (_coinFollowModel == null)
            {
                _coinFollowModel = new CoinFollowModel { lData = new List<CoinFollowDetailModel>() };
            }

            cmbCoin.Properties.ValueMember = "S";
            cmbCoin.Properties.DisplayMember = "S";
            cmbCoin.Properties.DataSource = StaticVal.lstCoin;
            
            txtValue.Text = string.Empty;
        }

        private bool CheckData()
        {
            var check = double.TryParse(txtValue.Text, out var val);
            if (!check)
            {
                Security.MesError("Giá trị Coin không hợp lệ!");
                return false;
            }

            if (val <= 0)
            {
                Security.MesError("Giá trị Coin không hợp lệ!");
                return false;
            }

            return true;
        }

        private void btnOkAndSave_Click(object sender, EventArgs e)
        {
            if (!CheckData())
                return;
            var entity = _coinFollowModel.lData.FirstOrDefault(x => x.Symbol.Equals(cmbCoin.EditValue.ToString()));
            if (entity != null)
                _coinFollowModel.lData.Remove(entity);
            _coinFollowModel.lData.Add(new CoinFollowDetailModel { Symbol = cmbCoin.EditValue.ToString(), Value = double.Parse(txtValue.Text) });
            _coinFollowModel.UpdateJson(_fileName);
            Security.MesSuccess();
            frmMyList.Instance().InitList();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCoin_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbCoin.EditValue == null
               || string.IsNullOrWhiteSpace(cmbCoin.EditValue.ToString()))
                return;
            var entity = StaticVal.lst24H.FirstOrDefault(x => x.Coin.Equals(cmbCoin.EditValue.ToString()));
            if (entity != null)
                txtValue.Text = entity.lastPrice.ToString(); 
        }
    }
}