using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Models;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Analyze.DesktopApp.GUI.Child
{
    public partial class frmCoinInfo : XtraForm
    {
        private readonly string _fileName = "coin_follow.json";
        private readonly string _symbol;
        private double _currentValue;
        private CoinFollowModel _coinFollowModel;
        public frmCoinInfo()
        {
            InitializeComponent();
        }

        public frmCoinInfo(string symbol, double currentValue)
        {
            InitializeComponent();
            _symbol = symbol;
            _currentValue = currentValue;
            InitData();
        }

        private void InitData()
        {
            _coinFollowModel = new CoinFollowModel().LoadJsonFile(_fileName);
            if (_coinFollowModel == null)
            {
                _coinFollowModel = new CoinFollowModel { lData = new List<CoinFollowDetailModel>() };
            }
            txtCoin.Text = _symbol;
            txtValue.Text = _currentValue.ToString();//.ToString("#,##0.#########");
        }

        private bool CheckData()
        {
            var check = double.TryParse(txtValue.Text, out var val);
            if(!check)
            {
                MessageBox.Show($"Giá trị Coin không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if(val <= 0)
            {
                MessageBox.Show($"Giá trị Coin không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnOkAndSave_Click(object sender, EventArgs e)
        {
            if (!CheckData())
                return;
            var entity = _coinFollowModel.lData.FirstOrDefault(x => x.Symbol.Equals(txtCoin.Text.Trim()));
            if (entity != null)
                _coinFollowModel.lData.Remove(entity);
            _coinFollowModel.lData.Add(new CoinFollowDetailModel { Symbol = txtCoin.Text.Trim(), Value = double.Parse(txtValue.Text) });
            _coinFollowModel.UpdateJson(_fileName);
            MessageBox.Show("Đã lưu dữ liệu!");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}