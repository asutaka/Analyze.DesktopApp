using System;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraCharts;
   

namespace Analyze.DesktopApp.GUI.Child
{

    public partial class FinancialChartingDemo : Form {
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
                    fibbRetrBarCheckItem.Checked || removeBarCheckItem.Checked || addTextAnnotationBarItem.Checked ||
                    addImageAnnotationBarItem.Checked;
            }
        }

        public FinancialChartingDemo() {
            InitializeComponent();
            //AutoMergeRibbon = true;
            chart.BeginInit();
            this.dataGenerator = new RealTimeFinancialDataGenerator();
            this.dataGenerator.GenerateInitialData();
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
                dataGenerator.UpdateDataSource();
            CustomAxisLabel currentValueLabel = AxisY.CustomLabels[0];
            if (PriceSeries.Points.Count > 0) {
                FinancialDataCollection dataSource = this.dataGenerator.DataSource;
                double currentClose = dataSource[dataSource.Count - 1].Close;
                currentValueLabel.AxisValue = currentClose;
                currentValueLabel.Name = string.Format("{0:0.0000}", currentClose);
            }
        }
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
