using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Analyze.DesktopApp.Utils;
using DevExpress.Utils;
using DevExpress.XtraCharts;
   

namespace Analyze.DesktopApp.GUI.Child
{

    public partial class FinancialChartingCustom : Form {
        const int InitialPointCountOnScreen = 90;
        const int MaxZoomPointCount = 300;

        RealTimeFinancialDataGenerator dataGenerator;
        object selectedObject = null;
        DefaultBoolean crosshairEnabled;

        XYDiagram XYDiagram {
            get { return (XYDiagram)this.chart.Diagram; }
        }
        AxisX AxisX {
            get { return XYDiagram.AxisX; }
        }
        AxisY AxisY {
            get { return XYDiagram.AxisY; }
        }
        SecondaryAxisY VolumeAxisY {
            get { return XYDiagram.SecondaryAxesY["VolumeAxisY"]; }
        }
        Series PriceSeries {
            get { return this.chart.Series["Price"]; }
        }
        Series VolumeSeries {
            get { return this.chart.Series["Volume"]; }
        }
        BarSeriesView VolumeSeriesView {
            get { return (BarSeriesView)VolumeSeries.View; }
        }
        bool IsToolbarInteractionEnabled {
            get {
                return trendLineBarCheckItem.Checked || fibbArcBarCheckItem.Checked || fibbFansBarCheckItem.Checked ||
                    fibbRetrBarCheckItem.Checked || removeBarCheckItem.Checked;
            }
        }

        public FinancialChartingCustom() {
            InitializeComponent();
            //AutoMergeRibbon = true;
            chart.BeginInit();
            this.dataGenerator = new RealTimeFinancialDataGenerator();
            this.dataGenerator.InitialDataFastAll("GMTUSDT");
            InitChartControl();
            SetVisualRangesAndGridOptions();
            chart.EndInit();
            this.dataGenerator.Start();
            this.timer.Enabled = true;
        }

        void InitChartControl() {
            chart.DataSource = this.dataGenerator.DataSource;
            PriceSeries.SetFinancialDataMembers("DateTimeStamp", "Low", "High", "Open", "Close");
            VolumeSeries.SetDataMembers("DateTimeStamp", "Volume");
            SetCustomLabelColor();
            selectAxisMeasureUnitBarItem1.EditValue = selectAxisMeasureUnitRepositoryItemComboBox1.Items[1];
        }
        void SetVisualRangesAndGridOptions() {
            TimeSpan visibleInterval;
            int multiplier = AxisX.DateTimeScaleOptions.MeasureUnitMultiplier;
            double timeIntervalToShow = InitialPointCountOnScreen * multiplier;
            switch (AxisX.DateTimeScaleOptions.MeasureUnit) {
                case DateTimeMeasureUnit.Minute:
                    visibleInterval = TimeSpan.FromMinutes(timeIntervalToShow);
                    VolumeAxisY.WholeRange.SideMarginsValue = 1000000 * multiplier;
                    break;
                case DateTimeMeasureUnit.Hour:
                    visibleInterval = TimeSpan.FromHours(timeIntervalToShow);
                    VolumeAxisY.WholeRange.SideMarginsValue = 60000000 * multiplier;
                    break;
                case DateTimeMeasureUnit.Day:
                    visibleInterval = TimeSpan.FromDays(timeIntervalToShow);
                    VolumeAxisY.WholeRange.SideMarginsValue = 1440000000 * multiplier;
                    break;
                case DateTimeMeasureUnit.Week:
                    visibleInterval = TimeSpan.FromDays(timeIntervalToShow * 7);
                    VolumeAxisY.WholeRange.SideMarginsValue = 7 * 1440000000d * multiplier;
                    break;
                default:
                    throw new NotSupportedException("This measure unit is not supported.");
            }
            AxisX.VisualRange.SetMinMaxValues(this.dataGenerator.LastArgument - visibleInterval, this.dataGenerator.LastArgument);
        }
        void SetCustomLabelColor() {
            AxisY.CustomLabels[0].BackColor = chart.GetPaletteEntries(2)[1].Color;
        }

        void timer_Tick(object sender, EventArgs e) {
            if (dataGenerator != null)
            {
                dataGenerator.UpdateSource();
                MACD();
            }
            CustomAxisLabel currentValueLabel = AxisY.CustomLabels[0];
            if (PriceSeries.Points.Count > 0)
            {
                FinancialDataCollection dataSource = this.dataGenerator.DataSource;
                double currentClose = dataSource[dataSource.Count - 1].Close;
                currentValueLabel.AxisValue = currentClose;
                currentValueLabel.Name = string.Format("{0:0.0000}", currentClose);
            }
        }

        #region Calculate
        private bool flag = false;
        private double val = 0;
        private DateTime dt;
        private List<double> lstRate = new List<double>();
        private void EMA5_12()
        {
            var EMA5 = CalculateMng.MA(dataGenerator._lstCalculate.Select(x => x.Close).ToArray(), TicTacTec.TA.Library.Core.MAType.Ema, 5, dataGenerator.index);
            var EMA12 = CalculateMng.MA(dataGenerator._lstCalculate.Select(x => x.Close).ToArray(), TicTacTec.TA.Library.Core.MAType.Ema, 12, dataGenerator.index);
            if (EMA5 >= EMA12 && flag)
            {
                flag = false;
                var last = dataGenerator._lstCalculate.Last();
                val = last.Close;
                dt = last.DateTimeStamp;
            }

            if (EMA5 < EMA12)
            {
                flag = true;
                if (val > 0)
                {
                    var cur = dataGenerator._lstCalculate.Last();
                    var rate = (1 - val / cur.Close) * 100;
                    var divTime = (cur.DateTimeStamp - dt).TotalHours;
                    lstRate.Add(rate);
                    LogM.Log($"START: {dt}; Value: {val}| END: {cur.DateTimeStamp}; Value: {cur.Close}|HOUR: {divTime}| Rate: {rate}%");
                    if (lstRate.Count() > 0)
                    {
                        LogM.Log($"AVG: {lstRate.Sum() / lstRate.Count()}");
                    }
                    val = 0;
                }
            }
        }
        private void EMA5_12Advance()
        {
            var EMA5 = CalculateMng.MA(dataGenerator._lstCalculate.Select(x => x.Close).ToArray(), TicTacTec.TA.Library.Core.MAType.Ema, 5, dataGenerator.index);
            var EMA12 = CalculateMng.MA(dataGenerator._lstCalculate.Select(x => x.Close).ToArray(), TicTacTec.TA.Library.Core.MAType.Ema, 12, dataGenerator.index);
            var RSI = CalculateMng.RSI(dataGenerator._lstCalculate.Select(x => x.Close).ToArray(), 14, dataGenerator.index);
            if (EMA5 >= EMA12 && RSI >= 50 && flag)
            {
                flag = false;
                var last = dataGenerator._lstCalculate.Last();
                val = last.Close;
                dt = last.DateTimeStamp;
            }

            if (EMA5 < EMA12)
            {
                flag = true;
                if (val > 0)
                {
                    var cur = dataGenerator._lstCalculate.Last();
                    var rate = (1 - val / cur.Close) * 100;
                    var divTime = (cur.DateTimeStamp - dt).TotalHours;
                    lstRate.Add(rate);
                    LogM.Log($"START: {dt}; Value: {val}| END: {cur.DateTimeStamp}; Value: {cur.Close}|HOUR: {divTime}| Rate: {rate}%");
                    if (lstRate.Count() > 0)
                    {
                        LogM.Log($"AVG: {lstRate.Sum() / lstRate.Count()}");
                    }
                    val = 0;
                }
            }
        }
        private void EMA5_10()
        {
            var EMA5 = CalculateMng.MA(dataGenerator._lstCalculate.Select(x => x.Close).ToArray(), TicTacTec.TA.Library.Core.MAType.Ema, 5, dataGenerator.index);
            var EMA12 = CalculateMng.MA(dataGenerator._lstCalculate.Select(x => x.Close).ToArray(), TicTacTec.TA.Library.Core.MAType.Ema, 10, dataGenerator.index);
            if (EMA5 >= EMA12 && flag)
            {
                flag = false;
                var last = dataGenerator._lstCalculate.Last();
                val = last.Close;
                dt = last.DateTimeStamp;
            }

            if (EMA5 < EMA12)
            {
                flag = true;
                if (val > 0)
                {
                    var cur = dataGenerator._lstCalculate.Last();
                    var rate = (1 - val / cur.Close) * 100;
                    var divTime = (cur.DateTimeStamp - dt).TotalHours;
                    lstRate.Add(rate);
                    LogM.Log($"START: {dt}; Value: {val}| END: {cur.DateTimeStamp}; Value: {cur.Close}|HOUR: {divTime}| Rate: {rate}%");
                    if (lstRate.Count() > 0)
                    {
                        LogM.Log($"AVG: {lstRate.Sum() / lstRate.Count()}");
                    }
                    val = 0;
                }
            }
        }
        private void MA5_10()
        {
            var EMA5 = CalculateMng.MA(dataGenerator._lstCalculate.Select(x => x.Close).ToArray(), TicTacTec.TA.Library.Core.MAType.Sma, 5, dataGenerator.index);
            var EMA12 = CalculateMng.MA(dataGenerator._lstCalculate.Select(x => x.Close).ToArray(), TicTacTec.TA.Library.Core.MAType.Sma, 10, dataGenerator.index);
            if (EMA5 >= EMA12 && flag)
            {
                flag = false;
                var last = dataGenerator._lstCalculate.Last();
                val = last.Close;
                dt = last.DateTimeStamp;
            }

            if (EMA5 < EMA12)
            {
                flag = true;
                if (val > 0)
                {
                    var cur = dataGenerator._lstCalculate.Last();
                    var rate = (1 - val / cur.Close) * 100;
                    var divTime = (cur.DateTimeStamp - dt).TotalHours;
                    lstRate.Add(rate);
                    LogM.Log($"START: {dt}; Value: {val}| END: {cur.DateTimeStamp}; Value: {cur.Close}|HOUR: {divTime}| Rate: {rate}%");
                    if (lstRate.Count() > 0)
                    {
                        LogM.Log($"AVG: {lstRate.Sum() / lstRate.Count()}");
                    }
                    val = 0;
                }
            }
        }
        private void MACD()
        {
            var MACD = CalculateMng.MACD(dataGenerator._lstCalculate.Select(x => x.Close).ToArray(), 12, 26, 9, dataGenerator.index);
            if (MACD >= 0 && flag)
            {
                flag = false;
                var last = dataGenerator._lstCalculate.Last();
                val = last.Close;
                dt = last.DateTimeStamp;
            }

            if (MACD < 0)
            {
                flag = true;
                if (val > 0)
                {
                    var cur = dataGenerator._lstCalculate.Last();
                    var rate = (1 - val / cur.Close) * 100;
                    var divTime = (cur.DateTimeStamp - dt).TotalHours;
                    lstRate.Add(rate);
                    LogM.Log($"START: {dt}; Value: {val}| END: {cur.DateTimeStamp}; Value: {cur.Close}|HOUR: {divTime}| Rate: {rate}%");
                    if (lstRate.Count() > 0)
                    {
                        LogM.Log($"AVG: {lstRate.Sum() / lstRate.Count()}");
                    }
                    val = 0;
                }
            }
        }
        #endregion


        void selectChartMeasureUnitRepositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e) {  
            SetVisualRangesAndGridOptions();
        }
        void chart_BeforeZoom(object sender, ChartBeforeZoomEventArgs e) {
            if (!(e.Axis is AxisX))
                return;
            double rangeLengthInMeasureUnits = e.NewRange.Max - e.NewRange.Min;
            if (rangeLengthInMeasureUnits > MaxZoomPointCount)
                e.Cancel = true;
        }
        void chart_Zoom(object sender, ChartZoomEventArgs e) {
            double rangeLengthInMeasureUnits = e.NewXRange.Max - e.NewXRange.Min;
            if (rangeLengthInMeasureUnits > 1.2 * InitialPointCountOnScreen)
                VolumeSeriesView.BarWidth = 1;
            else
                VolumeSeriesView.BarWidth = 0.6;
        }
        void chart_BoundDataChanged(object sender, EventArgs e) {
            chart.SetObjectSelection(this.selectedObject);
            crosshairEnabled = chart.CrosshairEnabled;
        }
        void chart_MouseUp(object sender, MouseEventArgs e) {
            if (IsToolbarInteractionEnabled)
                chart.CrosshairEnabled = crosshairEnabled;
        }
        void chartCommandBarCheckItem_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            bool isChecked = IsToolbarInteractionEnabled;
            timer.Enabled = !isChecked;
            chart.CrosshairEnabled = IsToolbarInteractionEnabled ? DefaultBoolean.False : crosshairEnabled;
        }
        void BeforePopup(object sender, EventArgs e) {
            timer.Enabled = false;
        }
        void BeforePopup(object sender, System.ComponentModel.CancelEventArgs e) {
            BeforePopup(sender, (EventArgs)e);
        }
        void CloseUp(object sender, EventArgs e) {
            if (!IsToolbarInteractionEnabled)
                timer.Enabled = true;
        }
        void CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e) {
            CloseUp(sender, (EventArgs)e);
        }
    }
}
