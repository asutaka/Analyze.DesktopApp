
namespace Analyze.DesktopApp.GUI.Child
{
    partial class frmChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.candlestickChart = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickChart)).BeginInit();
            this.SuspendLayout();
            // 
            // candlestickChart
            // 
            this.candlestickChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.candlestickChart.Legend.Name = "Default Legend";
            this.candlestickChart.Location = new System.Drawing.Point(0, 0);
            this.candlestickChart.Name = "candlestickChart";
            this.candlestickChart.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.candlestickChart.Size = new System.Drawing.Size(794, 449);
            this.candlestickChart.TabIndex = 0;
            // 
            // frmChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 449);
            this.Controls.Add(this.candlestickChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LookAndFeel.SkinName = "McSkin";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.Name = "frmChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm Coin";
            ((System.ComponentModel.ISupportInitialize)(this.candlestickChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl candlestickChart;
    }
}