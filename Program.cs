using Analyze.DesktopApp.GUI;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace Analyze.DesktopApp
{
    static class Program
    {
        public static IConfiguration Configuration;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(frmLogin.Instance());
        }
    }
}
