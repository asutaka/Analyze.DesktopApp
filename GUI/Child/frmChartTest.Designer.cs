
namespace Analyze.DesktopApp.GUI.Child
{
    partial class frmChartTest
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.XYDiagramPane xyDiagramPane1 = new DevExpress.XtraCharts.XYDiagramPane();
            DevExpress.XtraCharts.XYDiagramPane xyDiagramPane2 = new DevExpress.XtraCharts.XYDiagramPane();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.CandleStickSeriesView candleStickSeriesView1 = new DevExpress.XtraCharts.CandleStickSeriesView();
            DevExpress.XtraCharts.BollingerBands bollingerBands1 = new DevExpress.XtraCharts.BollingerBands();
            DevExpress.XtraCharts.MovingAverageConvergenceDivergence movingAverageConvergenceDivergence1 = new DevExpress.XtraCharts.MovingAverageConvergenceDivergence();
            this.candlestickChart = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagramPane1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagramPane2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(candleStickSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(bollingerBands1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(movingAverageConvergenceDivergence1)).BeginInit();
            this.SuspendLayout();
            // 
            // candlestickChart
            // 
            this.candlestickChart.CrosshairOptions.GroupHeaderPattern = "{A:d} {A:t}";
            this.candlestickChart.CrosshairOptions.ShowArgumentLabels = true;
            this.candlestickChart.CrosshairOptions.ShowOnlyInFocusedPane = false;
            this.candlestickChart.CrosshairOptions.ShowOutOfRangePoints = true;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1;0";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1;0";
            xyDiagramPane1.Name = "Pane 1";
            xyDiagramPane1.PaneID = 0;
            xyDiagramPane2.Name = "Pane 2";
            xyDiagramPane2.PaneID = 1;
            xyDiagram1.Panes.AddRange(new DevExpress.XtraCharts.XYDiagramPane[] {
            xyDiagramPane1,
            xyDiagramPane2});
            this.candlestickChart.Diagram = xyDiagram1;
            this.candlestickChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.candlestickChart.Legend.Name = "Default Legend";
            this.candlestickChart.Location = new System.Drawing.Point(0, 0);
            this.candlestickChart.Name = "candlestickChart";
            series1.Name = "Volume";
            series2.Name = "Price";
            bollingerBands1.Name = "Bollinger Bands 1";
            movingAverageConvergenceDivergence1.Name = "Moving Average Convergence/Divergence 1";
            movingAverageConvergenceDivergence1.PaneName = "Pane 1";
            candleStickSeriesView1.Indicators.AddRange(new DevExpress.XtraCharts.Indicator[] {
            bollingerBands1,
            movingAverageConvergenceDivergence1});
            series2.View = candleStickSeriesView1;
            this.candlestickChart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
            this.candlestickChart.Size = new System.Drawing.Size(1488, 449);
            this.candlestickChart.TabIndex = 0;
            // 
            // frmChartTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1488, 449);
            this.Controls.Add(this.candlestickChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LookAndFeel.SkinName = "McSkin";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.Name = "frmChartTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm Coin";
            ((System.ComponentModel.ISupportInitialize)(xyDiagramPane1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagramPane2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(bollingerBands1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(movingAverageConvergenceDivergence1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(candleStickSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl candlestickChart;
    }
}