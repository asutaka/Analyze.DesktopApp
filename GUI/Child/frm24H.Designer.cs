
namespace Analyze.DesktopApp.GUI.Child
{
    partial class frm24H
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet1 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet1 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon1 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon2 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon3 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon4 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRule3ColorScale formatConditionRule3ColorScale1 = new DevExpress.XtraEditors.FormatConditionRule3ColorScale();
            this.MCDX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Div = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CoinName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.volumeMA20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PriceChangePercent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.volume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.STT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Coin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lastPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.prevClosePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PriceChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PriceRef = new DevExpress.XtraGrid.Columns.GridColumn();
            this.volumeDiv = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // MCDX
            // 
            this.MCDX.Caption = "MCDX";
            this.MCDX.FieldName = "MCDX";
            this.MCDX.MaxWidth = 50;
            this.MCDX.MinWidth = 50;
            this.MCDX.Name = "MCDX";
            this.MCDX.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.MCDX.Width = 50;
            // 
            // Div
            // 
            this.Div.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Div.AppearanceCell.Options.UseBackColor = true;
            this.Div.AppearanceCell.Options.UseTextOptions = true;
            this.Div.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Div.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Div.AppearanceHeader.Options.UseTextOptions = true;
            this.Div.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Div.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Div.Caption = "Gia tăng Lg(%)";
            this.Div.DisplayFormat.FormatString = "\"#,##0.0\"";
            this.Div.FieldName = "Div";
            this.Div.MaxWidth = 85;
            this.Div.MinWidth = 85;
            this.Div.Name = "Div";
            this.Div.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.Div.Visible = true;
            this.Div.VisibleIndex = 7;
            this.Div.Width = 85;
            // 
            // CoinName
            // 
            this.CoinName.AppearanceCell.Options.UseTextOptions = true;
            this.CoinName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CoinName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CoinName.AppearanceHeader.Options.UseTextOptions = true;
            this.CoinName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CoinName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CoinName.Caption = "Tên";
            this.CoinName.FieldName = "CoinName";
            this.CoinName.Name = "CoinName";
            this.CoinName.OptionsColumn.AllowEdit = false;
            this.CoinName.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.CoinName.Visible = true;
            this.CoinName.VisibleIndex = 2;
            this.CoinName.Width = 20;
            // 
            // volumeMA20
            // 
            this.volumeMA20.AppearanceCell.Options.UseTextOptions = true;
            this.volumeMA20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.volumeMA20.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.volumeMA20.AppearanceHeader.Options.UseTextOptions = true;
            this.volumeMA20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.volumeMA20.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.volumeMA20.Caption = "Khối lượng TB";
            this.volumeMA20.DisplayFormat.FormatString = "\"#,##0.0\"";
            this.volumeMA20.FieldName = "weightedAvgPrice";
            this.volumeMA20.MaxWidth = 120;
            this.volumeMA20.MinWidth = 120;
            this.volumeMA20.Name = "volumeMA20";
            this.volumeMA20.Visible = true;
            this.volumeMA20.VisibleIndex = 9;
            this.volumeMA20.Width = 120;
            // 
            // PriceChangePercent
            // 
            this.PriceChangePercent.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.PriceChangePercent.AppearanceCell.Options.UseBackColor = true;
            this.PriceChangePercent.AppearanceCell.Options.UseTextOptions = true;
            this.PriceChangePercent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PriceChangePercent.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PriceChangePercent.AppearanceHeader.Options.UseTextOptions = true;
            this.PriceChangePercent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PriceChangePercent.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PriceChangePercent.Caption = "Gia tăng(%)";
            this.PriceChangePercent.FieldName = "PriceChangePercent";
            this.PriceChangePercent.MaxWidth = 85;
            this.PriceChangePercent.MinWidth = 85;
            this.PriceChangePercent.Name = "PriceChangePercent";
            this.PriceChangePercent.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.PriceChangePercent.Visible = true;
            this.PriceChangePercent.VisibleIndex = 5;
            this.PriceChangePercent.Width = 85;
            // 
            // volume
            // 
            this.volume.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.volume.AppearanceCell.Options.UseBackColor = true;
            this.volume.AppearanceCell.Options.UseTextOptions = true;
            this.volume.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.volume.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.volume.AppearanceHeader.Options.UseTextOptions = true;
            this.volume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.volume.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.volume.Caption = "Khối lượng";
            this.volume.DisplayFormat.FormatString = "\"#,##0\"";
            this.volume.FieldName = "volume";
            this.volume.MaxWidth = 120;
            this.volume.MinWidth = 120;
            this.volume.Name = "volume";
            this.volume.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.volume.Visible = true;
            this.volume.VisibleIndex = 8;
            this.volume.Width = 120;
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.MainView = this.gridView1;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(795, 495);
            this.grid.TabIndex = 0;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.STT,
            this.Coin,
            this.CoinName,
            this.lastPrice,
            this.prevClosePrice,
            this.PriceChange,
            this.PriceChangePercent,
            this.PriceRef,
            this.Div,
            this.volume,
            this.volumeMA20,
            this.volumeDiv,
            this.MCDX});
            gridFormatRule1.Column = this.MCDX;
            gridFormatRule1.ColumnApplyTo = this.MCDX;
            gridFormatRule1.Name = "FormatRate";
            formatConditionRuleIconSet1.AllowAnimation = DevExpress.Utils.DefaultBoolean.True;
            formatConditionIconSet1.CategoryName = "Directional";
            formatConditionIconSetIcon1.PredefinedName = "Arrows4_1.png";
            formatConditionIconSetIcon1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            formatConditionIconSetIcon1.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon2.PredefinedName = "Arrows4_2.png";
            formatConditionIconSetIcon2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            formatConditionIconSetIcon2.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon3.PredefinedName = "Arrows4_3.png";
            formatConditionIconSetIcon3.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            formatConditionIconSetIcon4.PredefinedName = "Arrows4_4.png";
            formatConditionIconSetIcon4.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon1);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon2);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon3);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon4);
            formatConditionIconSet1.Name = "Arrows4Colored";
            formatConditionIconSet1.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet1.IconSet = formatConditionIconSet1;
            gridFormatRule1.Rule = formatConditionRuleIconSet1;
            gridFormatRule2.Column = this.Div;
            gridFormatRule2.ColumnApplyTo = this.CoinName;
            gridFormatRule2.Name = "Format1";
            formatConditionRule3ColorScale1.AllowAnimation = DevExpress.Utils.DefaultBoolean.True;
            formatConditionRule3ColorScale1.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            formatConditionRule3ColorScale1.MaximumColor = System.Drawing.Color.Green;
            formatConditionRule3ColorScale1.MaximumType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRule3ColorScale1.MiddleColor = System.Drawing.Color.White;
            formatConditionRule3ColorScale1.MiddleType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRule3ColorScale1.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            formatConditionRule3ColorScale1.MinimumColor = System.Drawing.Color.Red;
            formatConditionRule3ColorScale1.MinimumType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRule3ColorScale1.PredefinedName = "Green, White, Red";
            gridFormatRule2.Rule = formatConditionRule3ColorScale1;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.GridControl = this.grid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // STT
            // 
            this.STT.AppearanceCell.Options.UseTextOptions = true;
            this.STT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.STT.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.STT.Caption = "STT";
            this.STT.FieldName = "STT";
            this.STT.MaxWidth = 45;
            this.STT.MinWidth = 45;
            this.STT.Name = "STT";
            this.STT.OptionsColumn.AllowEdit = false;
            this.STT.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.STT.Visible = true;
            this.STT.VisibleIndex = 0;
            this.STT.Width = 45;
            // 
            // Coin
            // 
            this.Coin.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.Coin.AppearanceCell.ForeColor = System.Drawing.Color.Green;
            this.Coin.AppearanceCell.Options.UseFont = true;
            this.Coin.AppearanceCell.Options.UseForeColor = true;
            this.Coin.AppearanceCell.Options.UseTextOptions = true;
            this.Coin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Coin.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Coin.AppearanceHeader.Options.UseTextOptions = true;
            this.Coin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Coin.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Coin.Caption = "Coin";
            this.Coin.FieldName = "Coin";
            this.Coin.MaxWidth = 90;
            this.Coin.MinWidth = 90;
            this.Coin.Name = "Coin";
            this.Coin.OptionsColumn.AllowEdit = false;
            this.Coin.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.Coin.Visible = true;
            this.Coin.VisibleIndex = 1;
            this.Coin.Width = 90;
            // 
            // lastPrice
            // 
            this.lastPrice.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lastPrice.AppearanceCell.Options.UseBackColor = true;
            this.lastPrice.AppearanceCell.Options.UseTextOptions = true;
            this.lastPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lastPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lastPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.lastPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lastPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lastPrice.Caption = "Giá hiện tại";
            this.lastPrice.DisplayFormat.FormatString = "#,##0.0";
            this.lastPrice.FieldName = "lastPrice";
            this.lastPrice.MaxWidth = 75;
            this.lastPrice.MinWidth = 75;
            this.lastPrice.Name = "lastPrice";
            this.lastPrice.OptionsColumn.AllowEdit = false;
            this.lastPrice.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.lastPrice.Visible = true;
            this.lastPrice.VisibleIndex = 3;
            // 
            // prevClosePrice
            // 
            this.prevClosePrice.AppearanceCell.Options.UseTextOptions = true;
            this.prevClosePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.prevClosePrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.prevClosePrice.AppearanceHeader.Options.UseTextOptions = true;
            this.prevClosePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.prevClosePrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.prevClosePrice.Caption = "Giá 24H";
            this.prevClosePrice.FieldName = "prevClosePrice";
            this.prevClosePrice.MaxWidth = 75;
            this.prevClosePrice.MinWidth = 75;
            this.prevClosePrice.Name = "prevClosePrice";
            this.prevClosePrice.Visible = true;
            this.prevClosePrice.VisibleIndex = 4;
            // 
            // PriceChange
            // 
            this.PriceChange.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.PriceChange.AppearanceCell.Options.UseBackColor = true;
            this.PriceChange.AppearanceCell.Options.UseTextOptions = true;
            this.PriceChange.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.PriceChange.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PriceChange.AppearanceHeader.Options.UseTextOptions = true;
            this.PriceChange.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PriceChange.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PriceChange.Caption = "Thay đổi 24H";
            this.PriceChange.DisplayFormat.FormatString = "\"#,##0.0\"";
            this.PriceChange.FieldName = "PriceChange";
            this.PriceChange.MaxWidth = 80;
            this.PriceChange.MinWidth = 80;
            this.PriceChange.Name = "PriceChange";
            this.PriceChange.Width = 80;
            // 
            // PriceRef
            // 
            this.PriceRef.AppearanceCell.Options.UseTextOptions = true;
            this.PriceRef.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.PriceRef.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PriceRef.AppearanceHeader.Options.UseTextOptions = true;
            this.PriceRef.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PriceRef.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PriceRef.Caption = "Giá Login";
            this.PriceRef.DisplayFormat.FormatString = "\"#,##0.0\"";
            this.PriceRef.FieldName = "PriceRef";
            this.PriceRef.MaxWidth = 75;
            this.PriceRef.MinWidth = 75;
            this.PriceRef.Name = "PriceRef";
            this.PriceRef.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.PriceRef.Visible = true;
            this.PriceRef.VisibleIndex = 6;
            // 
            // volumeDiv
            // 
            this.volumeDiv.Caption = "Tỉ lệ(%)";
            this.volumeDiv.FieldName = "volumeDiv";
            this.volumeDiv.MaxWidth = 85;
            this.volumeDiv.MinWidth = 85;
            this.volumeDiv.Name = "volumeDiv";
            this.volumeDiv.Visible = true;
            this.volumeDiv.VisibleIndex = 10;
            this.volumeDiv.Width = 85;
            // 
            // frm24H
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 495);
            this.Controls.Add(this.grid);
            this.LookAndFeel.SkinName = "McSkin";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "frm24H";
            this.Text = "24H";
            this.Load += new System.EventHandler(this.frm24H_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn STT;
        private DevExpress.XtraGrid.Columns.GridColumn Coin;
        private DevExpress.XtraGrid.Columns.GridColumn CoinName;
        private DevExpress.XtraGrid.Columns.GridColumn lastPrice;
        private DevExpress.XtraGrid.Columns.GridColumn PriceChangePercent;
        private DevExpress.XtraGrid.Columns.GridColumn volume;
        private DevExpress.XtraGrid.Columns.GridColumn PriceChange;
        private DevExpress.XtraGrid.Columns.GridColumn volumeMA20;
        private DevExpress.XtraGrid.Columns.GridColumn prevClosePrice;
        private DevExpress.XtraGrid.Columns.GridColumn PriceRef;
        private DevExpress.XtraGrid.Columns.GridColumn Div;
        private DevExpress.XtraGrid.Columns.GridColumn MCDX;
        private DevExpress.XtraGrid.Columns.GridColumn volumeDiv;
    }
}