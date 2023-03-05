using Analyze.DesktopApp.Common;
using Analyze.DesktopApp.Models;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Analyze.DesktopApp.GUI.Child
{
    public partial class frmChart : XtraForm
    {
        private static frmChart _instance = null;
        private readonly string _fileName = "coin_follow.json";
        private CoinFollowModel _coinFollowModel;
        private frmChart()
        {
            InitializeComponent();
            //InitData();
            //tmp();
        }

        public static frmChart Instance()
        {
            _instance = (_instance == null || _instance.IsDisposed) ? new frmChart() : _instance;
            return _instance;
        }

        public void MakeShow()
        {
            InitData();
            this.Show();
        }

        private void InitData()
        {
            // Create a candlestick series.
            Series series1 = new Series("Series 1", ViewType.CandleStick);

            // Bind the series to data.
            series1.DataSource = GetDataPoints();
            series1.SetFinancialDataMembers("Argument", "Low", "High", "Open", "Close");

            // Specify that date-time arguments are expected.
            series1.ArgumentScaleType = ScaleType.DateTime;

            // Add the series to the chart.
            candlestickChart.Series.Add(series1);

            // Customize the series view settings.
            CandleStickSeriesView view = (CandleStickSeriesView)series1.View;

            view.LineThickness = 2;
            view.LevelLineLength = 0.25;

            // Specify the series reduction options.
            view.ReductionOptions.ColorMode = ReductionColorMode.OpenToCloseValue;
            view.ReductionOptions.FillMode = CandleStickFillMode.AlwaysFilled;
            view.ReductionOptions.Level = StockLevel.Close;
            view.ReductionOptions.Visible = true;

            // Set point colors.
            view.Color = Color.Green;
            view.ReductionOptions.Color = Color.Red;

            // Access the chart's diagram.
            XYDiagram diagram = (XYDiagram)candlestickChart.Diagram;

            // Exclude empty ranges from the X-axis range
            // to avoid gaps in the chart's data.
            diagram.AxisX.DateTimeScaleOptions.SkipRangesWithoutPoints = true;

            // Hide the range without points at the beginning of the y-axis.
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;

            // Hide the legend.
            candlestickChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // Add a title to the chart.
            candlestickChart.Titles.Add(new ChartTitle());
            candlestickChart.Titles[0].Text = "Candlestick Chart";

            // Add the chart to the form.
            candlestickChart.Dock = DockStyle.Fill;
            this.Controls.Add(candlestickChart);
        }

        List<DataPoint> GetDataPoints()
        {
            List<DataPoint> dataPoints = new List<DataPoint> {
            new DataPoint(DateTime.Now.AddDays(-9), 24.00, 25.00, 25.00, 24.875),
            new DataPoint(DateTime.Now.AddDays(-8), 23.625, 25.125, 24.00, 24.875),
            new DataPoint(DateTime.Now.AddDays(-7), 26.25, 28.25, 26.75, 27.00),
            new DataPoint(DateTime.Now.AddDays(-6), 26.50, 27.875, 26.875, 27.25),

            new DataPoint(DateTime.Now.AddDays(-4), 25.75, 26.875, 26.75, 26.00),
            new DataPoint(DateTime.Now.AddDays(-3), 25.75, 26.75, 26.125, 26.25),
            new DataPoint(DateTime.Now.AddDays(-2), 25.75, 26.375, 26.375, 25.875),
            new DataPoint(DateTime.Now.AddDays(-1), 24.875, 26.125, 26.00, 25.375),
            new DataPoint(DateTime.Now.AddDays(0), 25.125, 26.00, 25.625, 25.75),
            };
            return dataPoints;
        }

        public void TrendLine()
        {
            {
                // Create a stock series, add it to the chart and set its properties.
                Series series1 = new Series("Series 1", ViewType.Stock);
                candlestickChart.Series.Add(series1);
                series1.ArgumentScaleType = ScaleType.DateTime;
                series1.ValueScaleType = ScaleType.Numerical;

                // Add points to the series.
                series1.Points.Add(new SeriesPoint(new DateTime(1994, 3, 1),
                    new object[] { 4.00, 5.00, 5.00, 4.85 }));
                series1.Points.Add(new SeriesPoint(new DateTime(1994, 3, 2),
                    new object[] { 6.05, 8.05, 6.05, 7.05 }));
                series1.Points.Add(new SeriesPoint(new DateTime(1994, 3, 3),
                    new object[] { 6.25, 8.25, 6.75, 7.15 }));

                // Create and customize a trendline, 
                TrendLine trendline1 = new TrendLine("A Trend");
                trendline1.Point1.Argument = new DateTime(1994, 3, 1);
                trendline1.Point1.ValueLevel = ValueLevel.High;
                trendline1.Point2.Argument = new DateTime(1994, 3, 3);
                trendline1.Point2.ValueLevel = ValueLevel.High;
                trendline1.ExtrapolateToInfinity = false;
                trendline1.Color = Color.Red;
                trendline1.LineStyle.DashStyle = DashStyle.Dash;


                // Cast the view type of the series to the Stock view.
                StockSeriesView myView = ((StockSeriesView)series1.View);

                // Define the Y-axis range.
                myView.AxisY.WholeRange.AlwaysShowZeroLevel = false;

                // Add the trendline to the series collection of indicators.
                myView.Indicators.Add(trendline1);

                // Add the chart to the form.
                candlestickChart.Dock = DockStyle.Fill;
            }
        }
    }

    public class DataPoint
    {
        public DateTime Argument { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public DataPoint(DateTime argument, double low, double high, double open, double close)
        {
            this.Argument = argument;
            this.Low = low;
            this.High = high;
            this.Open = open;
            this.Close = close;
        }
    }
}