using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Models;
using Analyze.DesktopApp.Utils;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Analyze.DesktopApp.GUI
{
    public partial class frmLogin : XtraForm
    {
        private readonly string _fileName = "profile.json";
        private frmLogin()
        {
            InitializeComponent();
            InitData();
        }

        private static frmLogin _instance = null;
        public static frmLogin Instance()
        {
            _instance = (_instance == null || _instance.IsDisposed) ? new frmLogin() : _instance;
            return _instance;
        }

        private void InitData()
        {
            Startup.Instance();
            var model = new ProfileModel().LoadJsonFile(_fileName);
            if (model != null)
            {
                txtPhone.Text = model.phone;
                txtPassword.Text = model.password;
                var bkgr = new BackgroundWorker();
                bkgr.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    CheckUser();
                };
                bkgr.RunWorkerAsync();
            }
        }

        private void CheckUser()
        {
            var phone = txtPhone.Text.Trim();
            var password = Security.HMACSHA256Hash(txtPassword.Text.Trim());
            var model = new CheckUserModel { phone = phone, password = password, signature = Security.HMACSHA256Hash($"{phone}{password}") };
            var res = WebClass.PostCheckUser(model).GetAwaiter().GetResult();
            if(string.IsNullOrWhiteSpace(res))
            {
                MessageBox.Show($"Lỗi không kiểm tra được Tài Khoản", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var resModel = JsonConvert.DeserializeObject<ResponseModel>(res);
            if(resModel.code <= 0)
            {
                MessageBox.Show(resModel.msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //show Main
            this.Invoke((MethodInvoker)delegate
            {
                Thread.Sleep(200);
                _instance.Hide();
                frmMain.Instance().Show();
            });
        }

        private void frmLogin_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            /*kill all running process
            * https://stackoverflow.com/questions/8507978/exiting-a-c-sharp-winforms-application
            */
            Process.GetCurrentProcess().Kill();
            Application.Exit();
            Environment.Exit(0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(chkSavePassword.Checked)
            {
                new ProfileModel
                {
                    phone = txtPhone.Text.Trim(),
                    password = txtPassword.Text.Trim()
                }.UpdateJson(_fileName);
            }
            CheckUser();
        }
    }
}