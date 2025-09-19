<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HomePage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim Series1 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim SideBySideBarSeriesLabel1 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel()
        Dim Series2 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim SideBySideBarSeriesLabel2 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel()
        Dim SideBySideBarSeriesLabel3 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel()
        Me.BlendPanel2 = New VbPowerPack.BlendPanel()
        Me.GBPARTYPERFORMANCE = New System.Windows.Forms.GroupBox()
        Me.RBITEM = New System.Windows.Forms.RadioButton()
        Me.RBPARTY = New System.Windows.Forms.RadioButton()
        Me.GRIDPERFORMANCEDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDPERFORMANCE = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GPPNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPPGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPPPERCENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBMONTHLYSALE = New System.Windows.Forms.GroupBox()
        Me.GRIDMONTHLYSALEDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDMONTHLYSALE = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GMMONTHNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMTAXABLE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBOVERDUE = New System.Windows.Forms.GroupBox()
        Me.GRIDOVERDUEDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDOVERDUE = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GONAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENTNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GOCITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GOINITIALS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GODATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GOBALANCE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GODUEDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GOBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREGNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOCITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDAYS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBSTOCK = New System.Windows.Forms.GroupBox()
        Me.GRIDSTOCKDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDSTOCK = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GITEMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDESIGN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCATEGORY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGODOWN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBPURORDER = New System.Windows.Forms.GroupBox()
        Me.GRIDPODETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDPO = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GPONAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPOITEMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPOMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBSALEORDER = New System.Windows.Forms.GroupBox()
        Me.GRIDSODETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDSO = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GSONAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSOITEMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSODESIGN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSOCOLOR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSOPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBSALE = New System.Windows.Forms.GroupBox()
        Me.PURSALECHART = New DevExpress.XtraCharts.ChartControl()
        Me.GBPAYOUTSTANDING = New System.Windows.Forms.GroupBox()
        Me.GRIDPAYDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDPAY = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GPNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPBALANCE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBRECOUTSTANDING = New System.Windows.Forms.GroupBox()
        Me.GRIDRECDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDREC = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRBALANCE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BlendPanel2.SuspendLayout()
        Me.GBPARTYPERFORMANCE.SuspendLayout()
        CType(Me.GRIDPERFORMANCEDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDPERFORMANCE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBMONTHLYSALE.SuspendLayout()
        CType(Me.GRIDMONTHLYSALEDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDMONTHLYSALE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBOVERDUE.SuspendLayout()
        CType(Me.GRIDOVERDUEDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDOVERDUE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBSTOCK.SuspendLayout()
        CType(Me.GRIDSTOCKDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDSTOCK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBPURORDER.SuspendLayout()
        CType(Me.GRIDPODETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDPO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBSALEORDER.SuspendLayout()
        CType(Me.GRIDSODETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDSO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBSALE.SuspendLayout()
        CType(Me.PURSALECHART, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBPAYOUTSTANDING.SuspendLayout()
        CType(Me.GRIDPAYDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDPAY, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBRECOUTSTANDING.SuspendLayout()
        CType(Me.GRIDRECDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDREC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel2
        '
        Me.BlendPanel2.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel2.Controls.Add(Me.GBPARTYPERFORMANCE)
        Me.BlendPanel2.Controls.Add(Me.GBMONTHLYSALE)
        Me.BlendPanel2.Controls.Add(Me.GBOVERDUE)
        Me.BlendPanel2.Controls.Add(Me.GBSTOCK)
        Me.BlendPanel2.Controls.Add(Me.GBPURORDER)
        Me.BlendPanel2.Controls.Add(Me.GBSALEORDER)
        Me.BlendPanel2.Controls.Add(Me.GBSALE)
        Me.BlendPanel2.Controls.Add(Me.GBPAYOUTSTANDING)
        Me.BlendPanel2.Controls.Add(Me.GBRECOUTSTANDING)
        Me.BlendPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel2.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel2.Name = "BlendPanel2"
        Me.BlendPanel2.Size = New System.Drawing.Size(1234, 661)
        Me.BlendPanel2.TabIndex = 13
        '
        'GBPARTYPERFORMANCE
        '
        Me.GBPARTYPERFORMANCE.BackColor = System.Drawing.Color.Transparent
        Me.GBPARTYPERFORMANCE.Controls.Add(Me.RBITEM)
        Me.GBPARTYPERFORMANCE.Controls.Add(Me.RBPARTY)
        Me.GBPARTYPERFORMANCE.Controls.Add(Me.GRIDPERFORMANCEDETAILS)
        Me.GBPARTYPERFORMANCE.Location = New System.Drawing.Point(407, 3)
        Me.GBPARTYPERFORMANCE.Name = "GBPARTYPERFORMANCE"
        Me.GBPARTYPERFORMANCE.Size = New System.Drawing.Size(494, 342)
        Me.GBPARTYPERFORMANCE.TabIndex = 21
        Me.GBPARTYPERFORMANCE.TabStop = False
        Me.GBPARTYPERFORMANCE.Text = "Performace"
        '
        'RBITEM
        '
        Me.RBITEM.AutoSize = True
        Me.RBITEM.Location = New System.Drawing.Point(191, 2)
        Me.RBITEM.Name = "RBITEM"
        Me.RBITEM.Size = New System.Drawing.Size(50, 18)
        Me.RBITEM.TabIndex = 7
        Me.RBITEM.Text = "Item"
        Me.RBITEM.UseVisualStyleBackColor = False
        '
        'RBPARTY
        '
        Me.RBPARTY.AutoSize = True
        Me.RBPARTY.Checked = True
        Me.RBPARTY.Location = New System.Drawing.Point(134, 2)
        Me.RBPARTY.Name = "RBPARTY"
        Me.RBPARTY.Size = New System.Drawing.Size(51, 18)
        Me.RBPARTY.TabIndex = 6
        Me.RBPARTY.TabStop = True
        Me.RBPARTY.Text = "Party"
        Me.RBPARTY.UseVisualStyleBackColor = True
        '
        'GRIDPERFORMANCEDETAILS
        '
        Me.GRIDPERFORMANCEDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDPERFORMANCEDETAILS.Location = New System.Drawing.Point(2, 21)
        Me.GRIDPERFORMANCEDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDPERFORMANCEDETAILS.MainView = Me.GRIDPERFORMANCE
        Me.GRIDPERFORMANCEDETAILS.Name = "GRIDPERFORMANCEDETAILS"
        Me.GRIDPERFORMANCEDETAILS.Size = New System.Drawing.Size(486, 318)
        Me.GRIDPERFORMANCEDETAILS.TabIndex = 5
        Me.GRIDPERFORMANCEDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDPERFORMANCE})
        '
        'GRIDPERFORMANCE
        '
        Me.GRIDPERFORMANCE.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GRIDPERFORMANCE.Appearance.Empty.Options.UseBackColor = True
        Me.GRIDPERFORMANCE.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GRIDPERFORMANCE.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDPERFORMANCE.Appearance.Row.Options.UseBackColor = True
        Me.GRIDPERFORMANCE.Appearance.Row.Options.UseFont = True
        Me.GRIDPERFORMANCE.Appearance.ViewCaption.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDPERFORMANCE.Appearance.ViewCaption.Options.UseFont = True
        Me.GRIDPERFORMANCE.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GPPNAME, Me.GPPGRANDTOTAL, Me.GPPPERCENT})
        Me.GRIDPERFORMANCE.GridControl = Me.GRIDPERFORMANCEDETAILS
        Me.GRIDPERFORMANCE.Name = "GRIDPERFORMANCE"
        Me.GRIDPERFORMANCE.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDPERFORMANCE.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDPERFORMANCE.OptionsBehavior.Editable = False
        Me.GRIDPERFORMANCE.OptionsView.ColumnAutoWidth = False
        Me.GRIDPERFORMANCE.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDPERFORMANCE.OptionsView.ShowFooter = True
        Me.GRIDPERFORMANCE.OptionsView.ShowGroupPanel = False
        '
        'GPPNAME
        '
        Me.GPPNAME.Caption = "Name"
        Me.GPPNAME.FieldName = "NAME"
        Me.GPPNAME.ImageOptions.ImageIndex = 0
        Me.GPPNAME.Name = "GPPNAME"
        Me.GPPNAME.Visible = True
        Me.GPPNAME.VisibleIndex = 0
        Me.GPPNAME.Width = 260
        '
        'GPPGRANDTOTAL
        '
        Me.GPPGRANDTOTAL.Caption = "Total Amt"
        Me.GPPGRANDTOTAL.DisplayFormat.FormatString = "0.00"
        Me.GPPGRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPPGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.GPPGRANDTOTAL.Name = "GPPGRANDTOTAL"
        Me.GPPGRANDTOTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GPPGRANDTOTAL.Visible = True
        Me.GPPGRANDTOTAL.VisibleIndex = 1
        Me.GPPGRANDTOTAL.Width = 120
        '
        'GPPPERCENT
        '
        Me.GPPPERCENT.Caption = "%"
        Me.GPPPERCENT.DisplayFormat.FormatString = "0.00"
        Me.GPPPERCENT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPPPERCENT.FieldName = "PER"
        Me.GPPPERCENT.Name = "GPPPERCENT"
        Me.GPPPERCENT.Visible = True
        Me.GPPPERCENT.VisibleIndex = 2
        Me.GPPPERCENT.Width = 55
        '
        'GBMONTHLYSALE
        '
        Me.GBMONTHLYSALE.BackColor = System.Drawing.Color.Transparent
        Me.GBMONTHLYSALE.Controls.Add(Me.GRIDMONTHLYSALEDETAILS)
        Me.GBMONTHLYSALE.Location = New System.Drawing.Point(14, 3)
        Me.GBMONTHLYSALE.Name = "GBMONTHLYSALE"
        Me.GBMONTHLYSALE.Size = New System.Drawing.Size(387, 342)
        Me.GBMONTHLYSALE.TabIndex = 20
        Me.GBMONTHLYSALE.TabStop = False
        Me.GBMONTHLYSALE.Text = "Monthly Sale"
        '
        'GRIDMONTHLYSALEDETAILS
        '
        Me.GRIDMONTHLYSALEDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDMONTHLYSALEDETAILS.Location = New System.Drawing.Point(2, 21)
        Me.GRIDMONTHLYSALEDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDMONTHLYSALEDETAILS.MainView = Me.GRIDMONTHLYSALE
        Me.GRIDMONTHLYSALEDETAILS.Name = "GRIDMONTHLYSALEDETAILS"
        Me.GRIDMONTHLYSALEDETAILS.Size = New System.Drawing.Size(382, 318)
        Me.GRIDMONTHLYSALEDETAILS.TabIndex = 5
        Me.GRIDMONTHLYSALEDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDMONTHLYSALE})
        '
        'GRIDMONTHLYSALE
        '
        Me.GRIDMONTHLYSALE.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.GRIDMONTHLYSALE.Appearance.Empty.Options.UseBackColor = True
        Me.GRIDMONTHLYSALE.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.GRIDMONTHLYSALE.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDMONTHLYSALE.Appearance.Row.Options.UseBackColor = True
        Me.GRIDMONTHLYSALE.Appearance.Row.Options.UseFont = True
        Me.GRIDMONTHLYSALE.Appearance.ViewCaption.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDMONTHLYSALE.Appearance.ViewCaption.Options.UseFont = True
        Me.GRIDMONTHLYSALE.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GMMONTHNAME, Me.GMTAXABLE, Me.GMGRANDTOTAL, Me.GMPER})
        Me.GRIDMONTHLYSALE.GridControl = Me.GRIDMONTHLYSALEDETAILS
        Me.GRIDMONTHLYSALE.Name = "GRIDMONTHLYSALE"
        Me.GRIDMONTHLYSALE.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDMONTHLYSALE.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDMONTHLYSALE.OptionsBehavior.Editable = False
        Me.GRIDMONTHLYSALE.OptionsView.ColumnAutoWidth = False
        Me.GRIDMONTHLYSALE.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDMONTHLYSALE.OptionsView.ShowFooter = True
        Me.GRIDMONTHLYSALE.OptionsView.ShowGroupPanel = False
        '
        'GMMONTHNAME
        '
        Me.GMMONTHNAME.Caption = "Name"
        Me.GMMONTHNAME.FieldName = "MONTHNAME"
        Me.GMMONTHNAME.ImageOptions.ImageIndex = 0
        Me.GMMONTHNAME.Name = "GMMONTHNAME"
        Me.GMMONTHNAME.Visible = True
        Me.GMMONTHNAME.VisibleIndex = 0
        Me.GMMONTHNAME.Width = 120
        '
        'GMTAXABLE
        '
        Me.GMTAXABLE.AppearanceCell.Options.UseTextOptions = True
        Me.GMTAXABLE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GMTAXABLE.Caption = "Taxable Amt"
        Me.GMTAXABLE.DisplayFormat.FormatString = "0.00"
        Me.GMTAXABLE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GMTAXABLE.FieldName = "TAXABLEAMT"
        Me.GMTAXABLE.ImageOptions.ImageIndex = 1
        Me.GMTAXABLE.Name = "GMTAXABLE"
        Me.GMTAXABLE.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GMTAXABLE.Visible = True
        Me.GMTAXABLE.VisibleIndex = 1
        Me.GMTAXABLE.Width = 90
        '
        'GMGRANDTOTAL
        '
        Me.GMGRANDTOTAL.Caption = "Total Amt"
        Me.GMGRANDTOTAL.DisplayFormat.FormatString = "0.00"
        Me.GMGRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GMGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.GMGRANDTOTAL.Name = "GMGRANDTOTAL"
        Me.GMGRANDTOTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GMGRANDTOTAL.Visible = True
        Me.GMGRANDTOTAL.VisibleIndex = 2
        Me.GMGRANDTOTAL.Width = 90
        '
        'GMPER
        '
        Me.GMPER.Caption = "%"
        Me.GMPER.DisplayFormat.FormatString = "0.00"
        Me.GMPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GMPER.FieldName = "PER"
        Me.GMPER.Name = "GMPER"
        Me.GMPER.Visible = True
        Me.GMPER.VisibleIndex = 3
        Me.GMPER.Width = 55
        '
        'GBOVERDUE
        '
        Me.GBOVERDUE.BackColor = System.Drawing.Color.Transparent
        Me.GBOVERDUE.Controls.Add(Me.GRIDOVERDUEDETAILS)
        Me.GBOVERDUE.Location = New System.Drawing.Point(12, 3)
        Me.GBOVERDUE.Name = "GBOVERDUE"
        Me.GBOVERDUE.Size = New System.Drawing.Size(872, 290)
        Me.GBOVERDUE.TabIndex = 19
        Me.GBOVERDUE.TabStop = False
        Me.GBOVERDUE.Text = "Over Due Bills"
        '
        'GRIDOVERDUEDETAILS
        '
        Me.GRIDOVERDUEDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDOVERDUEDETAILS.Location = New System.Drawing.Point(8, 21)
        Me.GRIDOVERDUEDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDOVERDUEDETAILS.MainView = Me.GRIDOVERDUE
        Me.GRIDOVERDUEDETAILS.Name = "GRIDOVERDUEDETAILS"
        Me.GRIDOVERDUEDETAILS.Size = New System.Drawing.Size(858, 262)
        Me.GRIDOVERDUEDETAILS.TabIndex = 7
        Me.GRIDOVERDUEDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDOVERDUE})
        '
        'GRIDOVERDUE
        '
        Me.GRIDOVERDUE.Appearance.Empty.BackColor = System.Drawing.Color.Linen
        Me.GRIDOVERDUE.Appearance.Empty.Options.UseBackColor = True
        Me.GRIDOVERDUE.Appearance.Row.BackColor = System.Drawing.Color.Linen
        Me.GRIDOVERDUE.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDOVERDUE.Appearance.Row.Options.UseBackColor = True
        Me.GRIDOVERDUE.Appearance.Row.Options.UseFont = True
        Me.GRIDOVERDUE.Appearance.ViewCaption.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDOVERDUE.Appearance.ViewCaption.Options.UseFont = True
        Me.GRIDOVERDUE.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GONAME, Me.GAGENTNAME, Me.GOCITY, Me.GOINITIALS, Me.GODATE, Me.GOBALANCE, Me.GODUEDATE, Me.GOBILLNO, Me.GREGNAME, Me.GTYPE, Me.GTOCITY, Me.GDAYS})
        Me.GRIDOVERDUE.GridControl = Me.GRIDOVERDUEDETAILS
        Me.GRIDOVERDUE.Name = "GRIDOVERDUE"
        Me.GRIDOVERDUE.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDOVERDUE.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDOVERDUE.OptionsBehavior.Editable = False
        Me.GRIDOVERDUE.OptionsView.ColumnAutoWidth = False
        Me.GRIDOVERDUE.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDOVERDUE.OptionsView.ShowAutoFilterRow = True
        Me.GRIDOVERDUE.OptionsView.ShowFooter = True
        Me.GRIDOVERDUE.OptionsView.ShowGroupPanel = False
        '
        'GONAME
        '
        Me.GONAME.Caption = "Name"
        Me.GONAME.FieldName = "NAME"
        Me.GONAME.Name = "GONAME"
        Me.GONAME.Visible = True
        Me.GONAME.VisibleIndex = 0
        Me.GONAME.Width = 200
        '
        'GAGENTNAME
        '
        Me.GAGENTNAME.Caption = "Agent Name"
        Me.GAGENTNAME.FieldName = "AGENTNAME"
        Me.GAGENTNAME.Name = "GAGENTNAME"
        Me.GAGENTNAME.Visible = True
        Me.GAGENTNAME.VisibleIndex = 1
        Me.GAGENTNAME.Width = 150
        '
        'GOCITY
        '
        Me.GOCITY.Caption = "City Name"
        Me.GOCITY.FieldName = "CITYNAME"
        Me.GOCITY.Name = "GOCITY"
        Me.GOCITY.Visible = True
        Me.GOCITY.VisibleIndex = 2
        Me.GOCITY.Width = 100
        '
        'GOINITIALS
        '
        Me.GOINITIALS.Caption = "Bill Initials"
        Me.GOINITIALS.FieldName = "INITIALS"
        Me.GOINITIALS.Name = "GOINITIALS"
        Me.GOINITIALS.Visible = True
        Me.GOINITIALS.VisibleIndex = 3
        Me.GOINITIALS.Width = 100
        '
        'GODATE
        '
        Me.GODATE.Caption = "Date"
        Me.GODATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GODATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GODATE.FieldName = "DATE"
        Me.GODATE.Name = "GODATE"
        Me.GODATE.Visible = True
        Me.GODATE.VisibleIndex = 4
        Me.GODATE.Width = 80
        '
        'GOBALANCE
        '
        Me.GOBALANCE.Caption = "Balance"
        Me.GOBALANCE.DisplayFormat.FormatString = "0.00"
        Me.GOBALANCE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GOBALANCE.FieldName = "BALANCE"
        Me.GOBALANCE.Name = "GOBALANCE"
        Me.GOBALANCE.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GOBALANCE.Visible = True
        Me.GOBALANCE.VisibleIndex = 5
        Me.GOBALANCE.Width = 100
        '
        'GODUEDATE
        '
        Me.GODUEDATE.Caption = "Due Date"
        Me.GODUEDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GODUEDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GODUEDATE.FieldName = "DUEDATE"
        Me.GODUEDATE.Name = "GODUEDATE"
        Me.GODUEDATE.Width = 80
        '
        'GOBILLNO
        '
        Me.GOBILLNO.Caption = "Bill No"
        Me.GOBILLNO.DisplayFormat.FormatString = "0"
        Me.GOBILLNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GOBILLNO.FieldName = "BILLNO"
        Me.GOBILLNO.Name = "GOBILLNO"
        '
        'GREGNAME
        '
        Me.GREGNAME.Caption = "REGNAME"
        Me.GREGNAME.FieldName = "REGNAME"
        Me.GREGNAME.Name = "GREGNAME"
        '
        'GTYPE
        '
        Me.GTYPE.Caption = "TYPE"
        Me.GTYPE.FieldName = "TYPE"
        Me.GTYPE.Name = "GTYPE"
        '
        'GTOCITY
        '
        Me.GTOCITY.Caption = "City Name"
        Me.GTOCITY.FieldName = "TOCITY"
        Me.GTOCITY.Name = "GTOCITY"
        Me.GTOCITY.Width = 100
        '
        'GDAYS
        '
        Me.GDAYS.Caption = "Days"
        Me.GDAYS.FieldName = "OVERDUEDAYS"
        Me.GDAYS.Name = "GDAYS"
        Me.GDAYS.Visible = True
        Me.GDAYS.VisibleIndex = 6
        '
        'GBSTOCK
        '
        Me.GBSTOCK.BackColor = System.Drawing.Color.Transparent
        Me.GBSTOCK.Controls.Add(Me.GRIDSTOCKDETAILS)
        Me.GBSTOCK.Location = New System.Drawing.Point(607, 359)
        Me.GBSTOCK.Name = "GBSTOCK"
        Me.GBSTOCK.Size = New System.Drawing.Size(277, 290)
        Me.GBSTOCK.TabIndex = 18
        Me.GBSTOCK.TabStop = False
        Me.GBSTOCK.Text = "Stock Details"
        '
        'GRIDSTOCKDETAILS
        '
        Me.GRIDSTOCKDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDSTOCKDETAILS.Location = New System.Drawing.Point(6, 21)
        Me.GRIDSTOCKDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDSTOCKDETAILS.MainView = Me.GRIDSTOCK
        Me.GRIDSTOCKDETAILS.Name = "GRIDSTOCKDETAILS"
        Me.GRIDSTOCKDETAILS.Size = New System.Drawing.Size(265, 262)
        Me.GRIDSTOCKDETAILS.TabIndex = 6
        Me.GRIDSTOCKDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDSTOCK})
        '
        'GRIDSTOCK
        '
        Me.GRIDSTOCK.Appearance.Empty.BackColor = System.Drawing.Color.LemonChiffon
        Me.GRIDSTOCK.Appearance.Empty.Options.UseBackColor = True
        Me.GRIDSTOCK.Appearance.Row.BackColor = System.Drawing.Color.LemonChiffon
        Me.GRIDSTOCK.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDSTOCK.Appearance.Row.Options.UseBackColor = True
        Me.GRIDSTOCK.Appearance.Row.Options.UseFont = True
        Me.GRIDSTOCK.Appearance.ViewCaption.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDSTOCK.Appearance.ViewCaption.Options.UseFont = True
        Me.GRIDSTOCK.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GITEMNAME, Me.GDESIGN, Me.GCATEGORY, Me.GPCS, Me.GMTRS, Me.GGODOWN})
        Me.GRIDSTOCK.GridControl = Me.GRIDSTOCKDETAILS
        Me.GRIDSTOCK.Name = "GRIDSTOCK"
        Me.GRIDSTOCK.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDSTOCK.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDSTOCK.OptionsBehavior.Editable = False
        Me.GRIDSTOCK.OptionsView.ColumnAutoWidth = False
        Me.GRIDSTOCK.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDSTOCK.OptionsView.ShowAutoFilterRow = True
        Me.GRIDSTOCK.OptionsView.ShowGroupPanel = False
        '
        'GITEMNAME
        '
        Me.GITEMNAME.Caption = "Item Name"
        Me.GITEMNAME.FieldName = "ITEMNAME"
        Me.GITEMNAME.ImageOptions.ImageIndex = 0
        Me.GITEMNAME.Name = "GITEMNAME"
        Me.GITEMNAME.Visible = True
        Me.GITEMNAME.VisibleIndex = 0
        Me.GITEMNAME.Width = 100
        '
        'GDESIGN
        '
        Me.GDESIGN.Caption = "Design No"
        Me.GDESIGN.FieldName = "DESIGNNO"
        Me.GDESIGN.Name = "GDESIGN"
        '
        'GCATEGORY
        '
        Me.GCATEGORY.Caption = "Category"
        Me.GCATEGORY.FieldName = "CATEGORY"
        Me.GCATEGORY.Name = "GCATEGORY"
        '
        'GPCS
        '
        Me.GPCS.AppearanceCell.Options.UseTextOptions = True
        Me.GPCS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GPCS.Caption = "Pcs"
        Me.GPCS.DisplayFormat.FormatString = "0"
        Me.GPCS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPCS.FieldName = "PCS"
        Me.GPCS.ImageOptions.ImageIndex = 1
        Me.GPCS.Name = "GPCS"
        Me.GPCS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GPCS.Visible = True
        Me.GPCS.VisibleIndex = 1
        Me.GPCS.Width = 60
        '
        'GMTRS
        '
        Me.GMTRS.Caption = "Mtrs"
        Me.GMTRS.DisplayFormat.FormatString = "0.00"
        Me.GMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GMTRS.FieldName = "MTRS"
        Me.GMTRS.Name = "GMTRS"
        Me.GMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GMTRS.Visible = True
        Me.GMTRS.VisibleIndex = 2
        Me.GMTRS.Width = 60
        '
        'GGODOWN
        '
        Me.GGODOWN.Caption = "Godown"
        Me.GGODOWN.FieldName = "GODOWN"
        Me.GGODOWN.Name = "GGODOWN"
        '
        'GBPURORDER
        '
        Me.GBPURORDER.BackColor = System.Drawing.Color.Transparent
        Me.GBPURORDER.Controls.Add(Me.GRIDPODETAILS)
        Me.GBPURORDER.Location = New System.Drawing.Point(315, 359)
        Me.GBPURORDER.Name = "GBPURORDER"
        Me.GBPURORDER.Size = New System.Drawing.Size(277, 290)
        Me.GBPURORDER.TabIndex = 17
        Me.GBPURORDER.TabStop = False
        Me.GBPURORDER.Text = "Pending Purchase Order"
        '
        'GRIDPODETAILS
        '
        Me.GRIDPODETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDPODETAILS.Location = New System.Drawing.Point(6, 22)
        Me.GRIDPODETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDPODETAILS.MainView = Me.GRIDPO
        Me.GRIDPODETAILS.Name = "GRIDPODETAILS"
        Me.GRIDPODETAILS.Size = New System.Drawing.Size(265, 262)
        Me.GRIDPODETAILS.TabIndex = 6
        Me.GRIDPODETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDPO})
        '
        'GRIDPO
        '
        Me.GRIDPO.Appearance.Empty.BackColor = System.Drawing.Color.Linen
        Me.GRIDPO.Appearance.Empty.Options.UseBackColor = True
        Me.GRIDPO.Appearance.Row.BackColor = System.Drawing.Color.Linen
        Me.GRIDPO.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDPO.Appearance.Row.Options.UseBackColor = True
        Me.GRIDPO.Appearance.Row.Options.UseFont = True
        Me.GRIDPO.Appearance.ViewCaption.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDPO.Appearance.ViewCaption.Options.UseFont = True
        Me.GRIDPO.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GPONAME, Me.GPOITEMNAME, Me.GPOMTRS})
        Me.GRIDPO.GridControl = Me.GRIDPODETAILS
        Me.GRIDPO.Name = "GRIDPO"
        Me.GRIDPO.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDPO.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDPO.OptionsBehavior.Editable = False
        Me.GRIDPO.OptionsView.ColumnAutoWidth = False
        Me.GRIDPO.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDPO.OptionsView.ShowAutoFilterRow = True
        Me.GRIDPO.OptionsView.ShowGroupPanel = False
        '
        'GPONAME
        '
        Me.GPONAME.Caption = "Name"
        Me.GPONAME.FieldName = "NAME"
        Me.GPONAME.ImageOptions.ImageIndex = 0
        Me.GPONAME.Name = "GPONAME"
        Me.GPONAME.Visible = True
        Me.GPONAME.VisibleIndex = 0
        Me.GPONAME.Width = 200
        '
        'GPOITEMNAME
        '
        Me.GPOITEMNAME.Caption = "Item Name"
        Me.GPOITEMNAME.FieldName = "ITEMNAME"
        Me.GPOITEMNAME.Name = "GPOITEMNAME"
        Me.GPOITEMNAME.Visible = True
        Me.GPOITEMNAME.VisibleIndex = 1
        Me.GPOITEMNAME.Width = 100
        '
        'GPOMTRS
        '
        Me.GPOMTRS.AppearanceCell.Options.UseTextOptions = True
        Me.GPOMTRS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GPOMTRS.Caption = "Mtrs"
        Me.GPOMTRS.DisplayFormat.FormatString = "0.00"
        Me.GPOMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPOMTRS.FieldName = "MTRS"
        Me.GPOMTRS.ImageOptions.ImageIndex = 1
        Me.GPOMTRS.Name = "GPOMTRS"
        Me.GPOMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GPOMTRS.Visible = True
        Me.GPOMTRS.VisibleIndex = 2
        Me.GPOMTRS.Width = 70
        '
        'GBSALEORDER
        '
        Me.GBSALEORDER.BackColor = System.Drawing.Color.Transparent
        Me.GBSALEORDER.Controls.Add(Me.GRIDSODETAILS)
        Me.GBSALEORDER.Location = New System.Drawing.Point(14, 359)
        Me.GBSALEORDER.Name = "GBSALEORDER"
        Me.GBSALEORDER.Size = New System.Drawing.Size(277, 290)
        Me.GBSALEORDER.TabIndex = 16
        Me.GBSALEORDER.TabStop = False
        Me.GBSALEORDER.Text = "Pending Sale Order"
        '
        'GRIDSODETAILS
        '
        Me.GRIDSODETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDSODETAILS.Location = New System.Drawing.Point(6, 22)
        Me.GRIDSODETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDSODETAILS.MainView = Me.GRIDSO
        Me.GRIDSODETAILS.Name = "GRIDSODETAILS"
        Me.GRIDSODETAILS.Size = New System.Drawing.Size(265, 262)
        Me.GRIDSODETAILS.TabIndex = 6
        Me.GRIDSODETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDSO})
        '
        'GRIDSO
        '
        Me.GRIDSO.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GRIDSO.Appearance.Empty.Options.UseBackColor = True
        Me.GRIDSO.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GRIDSO.Appearance.Row.BorderColor = System.Drawing.Color.Black
        Me.GRIDSO.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDSO.Appearance.Row.Options.UseBackColor = True
        Me.GRIDSO.Appearance.Row.Options.UseBorderColor = True
        Me.GRIDSO.Appearance.Row.Options.UseFont = True
        Me.GRIDSO.Appearance.ViewCaption.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDSO.Appearance.ViewCaption.Options.UseFont = True
        Me.GRIDSO.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GSONAME, Me.GSOITEMNAME, Me.GSODESIGN, Me.GSOCOLOR, Me.GSOPCS})
        Me.GRIDSO.GridControl = Me.GRIDSODETAILS
        Me.GRIDSO.Name = "GRIDSO"
        Me.GRIDSO.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDSO.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDSO.OptionsBehavior.Editable = False
        Me.GRIDSO.OptionsView.ColumnAutoWidth = False
        Me.GRIDSO.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDSO.OptionsView.ShowAutoFilterRow = True
        Me.GRIDSO.OptionsView.ShowGroupPanel = False
        '
        'GSONAME
        '
        Me.GSONAME.Caption = "Name"
        Me.GSONAME.FieldName = "NAME"
        Me.GSONAME.ImageOptions.ImageIndex = 0
        Me.GSONAME.Name = "GSONAME"
        Me.GSONAME.Visible = True
        Me.GSONAME.VisibleIndex = 0
        Me.GSONAME.Width = 200
        '
        'GSOITEMNAME
        '
        Me.GSOITEMNAME.Caption = "Item Name"
        Me.GSOITEMNAME.FieldName = "ITEMNAME"
        Me.GSOITEMNAME.Name = "GSOITEMNAME"
        Me.GSOITEMNAME.Visible = True
        Me.GSOITEMNAME.VisibleIndex = 1
        Me.GSOITEMNAME.Width = 100
        '
        'GSODESIGN
        '
        Me.GSODESIGN.Caption = "Design No"
        Me.GSODESIGN.FieldName = "DESIGNNO"
        Me.GSODESIGN.Name = "GSODESIGN"
        '
        'GSOCOLOR
        '
        Me.GSOCOLOR.Caption = "Shade"
        Me.GSOCOLOR.FieldName = "COLOR"
        Me.GSOCOLOR.Name = "GSOCOLOR"
        '
        'GSOPCS
        '
        Me.GSOPCS.AppearanceCell.Options.UseTextOptions = True
        Me.GSOPCS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GSOPCS.Caption = "Pcs"
        Me.GSOPCS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GSOPCS.FieldName = "PCS"
        Me.GSOPCS.ImageOptions.ImageIndex = 1
        Me.GSOPCS.Name = "GSOPCS"
        Me.GSOPCS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GSOPCS.Visible = True
        Me.GSOPCS.VisibleIndex = 2
        Me.GSOPCS.Width = 70
        '
        'GBSALE
        '
        Me.GBSALE.BackColor = System.Drawing.Color.Transparent
        Me.GBSALE.Controls.Add(Me.PURSALECHART)
        Me.GBSALE.Location = New System.Drawing.Point(12, 3)
        Me.GBSALE.Name = "GBSALE"
        Me.GBSALE.Size = New System.Drawing.Size(872, 290)
        Me.GBSALE.TabIndex = 15
        Me.GBSALE.TabStop = False
        Me.GBSALE.Text = "Month Wise Sales Turn Over"
        '
        'PURSALECHART
        '
        Me.PURSALECHART.BackColor = System.Drawing.Color.Beige
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        Me.PURSALECHART.Diagram = XyDiagram1
        Me.PURSALECHART.Legend.Visibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.PURSALECHART.Location = New System.Drawing.Point(8, 21)
        Me.PURSALECHART.Name = "PURSALECHART"
        SideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.[True]
        Series1.Label = SideBySideBarSeriesLabel1
        Series1.Name = "PURCHASE"
        Series1.SeriesID = 0
        SideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.[True]
        Series2.Label = SideBySideBarSeriesLabel2
        Series2.Name = "SALE"
        Series2.SeriesID = 1
        Me.PURSALECHART.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series1, Series2}
        SideBySideBarSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.[True]
        Me.PURSALECHART.SeriesTemplate.Label = SideBySideBarSeriesLabel3
        Me.PURSALECHART.Size = New System.Drawing.Size(858, 262)
        Me.PURSALECHART.TabIndex = 0
        '
        'GBPAYOUTSTANDING
        '
        Me.GBPAYOUTSTANDING.BackColor = System.Drawing.Color.Transparent
        Me.GBPAYOUTSTANDING.Controls.Add(Me.GRIDPAYDETAILS)
        Me.GBPAYOUTSTANDING.Location = New System.Drawing.Point(909, 359)
        Me.GBPAYOUTSTANDING.Name = "GBPAYOUTSTANDING"
        Me.GBPAYOUTSTANDING.Size = New System.Drawing.Size(315, 290)
        Me.GBPAYOUTSTANDING.TabIndex = 15
        Me.GBPAYOUTSTANDING.TabStop = False
        Me.GBPAYOUTSTANDING.Text = "Pay Outstanding"
        '
        'GRIDPAYDETAILS
        '
        Me.GRIDPAYDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDPAYDETAILS.Location = New System.Drawing.Point(2, 22)
        Me.GRIDPAYDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDPAYDETAILS.MainView = Me.GRIDPAY
        Me.GRIDPAYDETAILS.Name = "GRIDPAYDETAILS"
        Me.GRIDPAYDETAILS.Size = New System.Drawing.Size(313, 262)
        Me.GRIDPAYDETAILS.TabIndex = 6
        Me.GRIDPAYDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDPAY})
        '
        'GRIDPAY
        '
        Me.GRIDPAY.Appearance.Empty.BackColor = System.Drawing.Color.LavenderBlush
        Me.GRIDPAY.Appearance.Empty.Options.UseBackColor = True
        Me.GRIDPAY.Appearance.Row.BackColor = System.Drawing.Color.LavenderBlush
        Me.GRIDPAY.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDPAY.Appearance.Row.Options.UseBackColor = True
        Me.GRIDPAY.Appearance.Row.Options.UseFont = True
        Me.GRIDPAY.Appearance.ViewCaption.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDPAY.Appearance.ViewCaption.Options.UseFont = True
        Me.GRIDPAY.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GPNAME, Me.GPBALANCE})
        Me.GRIDPAY.GridControl = Me.GRIDPAYDETAILS
        Me.GRIDPAY.Name = "GRIDPAY"
        Me.GRIDPAY.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDPAY.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDPAY.OptionsBehavior.Editable = False
        Me.GRIDPAY.OptionsView.ColumnAutoWidth = False
        Me.GRIDPAY.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDPAY.OptionsView.ShowAutoFilterRow = True
        Me.GRIDPAY.OptionsView.ShowGroupPanel = False
        '
        'GPNAME
        '
        Me.GPNAME.Caption = "Name"
        Me.GPNAME.FieldName = "NAME"
        Me.GPNAME.ImageOptions.ImageIndex = 0
        Me.GPNAME.Name = "GPNAME"
        Me.GPNAME.Visible = True
        Me.GPNAME.VisibleIndex = 0
        Me.GPNAME.Width = 200
        '
        'GPBALANCE
        '
        Me.GPBALANCE.AppearanceCell.Options.UseTextOptions = True
        Me.GPBALANCE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GPBALANCE.Caption = "Amount"
        Me.GPBALANCE.DisplayFormat.FormatString = "0.00"
        Me.GPBALANCE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPBALANCE.FieldName = "BALANCE"
        Me.GPBALANCE.ImageOptions.ImageIndex = 1
        Me.GPBALANCE.Name = "GPBALANCE"
        Me.GPBALANCE.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GPBALANCE.Visible = True
        Me.GPBALANCE.VisibleIndex = 1
        Me.GPBALANCE.Width = 70
        '
        'GBRECOUTSTANDING
        '
        Me.GBRECOUTSTANDING.BackColor = System.Drawing.Color.Transparent
        Me.GBRECOUTSTANDING.Controls.Add(Me.GRIDRECDETAILS)
        Me.GBRECOUTSTANDING.Location = New System.Drawing.Point(907, 3)
        Me.GBRECOUTSTANDING.Name = "GBRECOUTSTANDING"
        Me.GBRECOUTSTANDING.Size = New System.Drawing.Size(314, 342)
        Me.GBRECOUTSTANDING.TabIndex = 14
        Me.GBRECOUTSTANDING.TabStop = False
        Me.GBRECOUTSTANDING.Text = "Rec Outstanding"
        '
        'GRIDRECDETAILS
        '
        Me.GRIDRECDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDRECDETAILS.Location = New System.Drawing.Point(2, 21)
        Me.GRIDRECDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDRECDETAILS.MainView = Me.GRIDREC
        Me.GRIDRECDETAILS.Name = "GRIDRECDETAILS"
        Me.GRIDRECDETAILS.Size = New System.Drawing.Size(313, 318)
        Me.GRIDRECDETAILS.TabIndex = 5
        Me.GRIDRECDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDREC})
        '
        'GRIDREC
        '
        Me.GRIDREC.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GRIDREC.Appearance.Empty.Options.UseBackColor = True
        Me.GRIDREC.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.GRIDREC.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDREC.Appearance.Row.Options.UseBackColor = True
        Me.GRIDREC.Appearance.Row.Options.UseFont = True
        Me.GRIDREC.Appearance.ViewCaption.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDREC.Appearance.ViewCaption.Options.UseFont = True
        Me.GRIDREC.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GNAME, Me.GRBALANCE})
        Me.GRIDREC.GridControl = Me.GRIDRECDETAILS
        Me.GRIDREC.Name = "GRIDREC"
        Me.GRIDREC.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDREC.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDREC.OptionsBehavior.Editable = False
        Me.GRIDREC.OptionsView.ColumnAutoWidth = False
        Me.GRIDREC.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDREC.OptionsView.ShowAutoFilterRow = True
        Me.GRIDREC.OptionsView.ShowGroupPanel = False
        '
        'GNAME
        '
        Me.GNAME.Caption = "Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.ImageOptions.ImageIndex = 0
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 0
        Me.GNAME.Width = 200
        '
        'GRBALANCE
        '
        Me.GRBALANCE.AppearanceCell.Options.UseTextOptions = True
        Me.GRBALANCE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GRBALANCE.Caption = "Amount"
        Me.GRBALANCE.DisplayFormat.FormatString = "0.00"
        Me.GRBALANCE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRBALANCE.FieldName = "BALANCE"
        Me.GRBALANCE.ImageOptions.ImageIndex = 1
        Me.GRBALANCE.Name = "GRBALANCE"
        Me.GRBALANCE.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GRBALANCE.Visible = True
        Me.GRBALANCE.VisibleIndex = 1
        Me.GRBALANCE.Width = 70
        '
        'HomePage
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 661)
        Me.Controls.Add(Me.BlendPanel2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "HomePage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Home"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel2.ResumeLayout(False)
        Me.GBPARTYPERFORMANCE.ResumeLayout(False)
        Me.GBPARTYPERFORMANCE.PerformLayout()
        CType(Me.GRIDPERFORMANCEDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDPERFORMANCE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBMONTHLYSALE.ResumeLayout(False)
        CType(Me.GRIDMONTHLYSALEDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDMONTHLYSALE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBOVERDUE.ResumeLayout(False)
        CType(Me.GRIDOVERDUEDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDOVERDUE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBSTOCK.ResumeLayout(False)
        CType(Me.GRIDSTOCKDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDSTOCK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBPURORDER.ResumeLayout(False)
        CType(Me.GRIDPODETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBSALEORDER.ResumeLayout(False)
        CType(Me.GRIDSODETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDSO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBSALE.ResumeLayout(False)
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PURSALECHART, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBPAYOUTSTANDING.ResumeLayout(False)
        CType(Me.GRIDPAYDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDPAY, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBRECOUTSTANDING.ResumeLayout(False)
        CType(Me.GRIDRECDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDREC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel2 As VbPowerPack.BlendPanel
    Friend WithEvents GBPAYOUTSTANDING As System.Windows.Forms.GroupBox
    Private WithEvents GRIDPAYDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDPAY As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GPNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPBALANCE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBRECOUTSTANDING As System.Windows.Forms.GroupBox
    Private WithEvents GRIDRECDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDREC As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRBALANCE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBSTOCK As System.Windows.Forms.GroupBox
    Private WithEvents GRIDSTOCKDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDSTOCK As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GITEMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBPURORDER As System.Windows.Forms.GroupBox
    Private WithEvents GRIDPODETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDPO As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GPONAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPOMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBSALEORDER As System.Windows.Forms.GroupBox
    Private WithEvents GRIDSODETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDSO As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GSONAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSOPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBSALE As System.Windows.Forms.GroupBox
    Friend WithEvents PURSALECHART As DevExpress.XtraCharts.ChartControl
    Friend WithEvents GPOITEMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSOITEMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBOVERDUE As GroupBox
    Private WithEvents GRIDOVERDUEDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDOVERDUE As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GONAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GOINITIALS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GODATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GOBALANCE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GODUEDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GOBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GOCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENTNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREGNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDESIGN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSODESIGN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSOCOLOR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCATEGORY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGODOWN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDAYS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBMONTHLYSALE As GroupBox
    Private WithEvents GRIDMONTHLYSALEDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDMONTHLYSALE As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GMMONTHNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMTAXABLE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBPARTYPERFORMANCE As GroupBox
    Private WithEvents GRIDPERFORMANCEDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDPERFORMANCE As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GPPNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPPGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPPPERCENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RBITEM As RadioButton
    Friend WithEvents RBPARTY As RadioButton
End Class
