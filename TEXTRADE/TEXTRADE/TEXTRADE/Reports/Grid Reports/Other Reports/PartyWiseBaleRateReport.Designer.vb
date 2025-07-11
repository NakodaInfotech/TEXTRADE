<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PartyWiseBaleRateReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PartyWiseBaleRateReport))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.CMBOUTSTANDING = New System.Windows.Forms.Button()
        Me.griddetails = New DevExpress.XtraGrid.GridControl()
        Me.gridregister = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gname = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gtype = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gbillinitials = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREFNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gdr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRUNNINGBAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gbillno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.greg = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCHQNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gremarks = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GUSER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMBACCCODE = New System.Windows.Forms.ComboBox()
        Me.txtadd = New System.Windows.Forms.TextBox()
        Me.cmdshowdetails = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.txtcrtotal = New System.Windows.Forms.TextBox()
        Me.txtdrtotal = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbldrcropening = New System.Windows.Forms.Label()
        Me.lbldrcrclosing = New System.Windows.Forms.Label()
        Me.txttotal = New System.Windows.Forms.TextBox()
        Me.lbln = New System.Windows.Forms.Label()
        Me.txttempbillno = New System.Windows.Forms.TextBox()
        Me.chkdate = New System.Windows.Forms.CheckBox()
        Me.dtto = New System.Windows.Forms.DateTimePicker()
        Me.lblto = New System.Windows.Forms.Label()
        Me.dtfrom = New System.Windows.Forms.DateTimePicker()
        Me.lblfrom = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExcelExport = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLDAILY = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLMONTHLY = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblname = New System.Windows.Forms.Label()
        Me.cmbname = New System.Windows.Forms.ComboBox()
        Me.lbl = New System.Windows.Forms.Label()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.griddetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridregister, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        resources.ApplyResources(Me.BlendPanel1, "BlendPanel1")
        Me.BlendPanel1.Controls.Add(Me.CMBOUTSTANDING)
        Me.BlendPanel1.Controls.Add(Me.griddetails)
        Me.BlendPanel1.Controls.Add(Me.CMBACCCODE)
        Me.BlendPanel1.Controls.Add(Me.txtadd)
        Me.BlendPanel1.Controls.Add(Me.cmdshowdetails)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.txtcrtotal)
        Me.BlendPanel1.Controls.Add(Me.txtdrtotal)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.lbldrcropening)
        Me.BlendPanel1.Controls.Add(Me.lbldrcrclosing)
        Me.BlendPanel1.Controls.Add(Me.txttotal)
        Me.BlendPanel1.Controls.Add(Me.lbln)
        Me.BlendPanel1.Controls.Add(Me.txttempbillno)
        Me.BlendPanel1.Controls.Add(Me.chkdate)
        Me.BlendPanel1.Controls.Add(Me.dtto)
        Me.BlendPanel1.Controls.Add(Me.lblto)
        Me.BlendPanel1.Controls.Add(Me.dtfrom)
        Me.BlendPanel1.Controls.Add(Me.lblfrom)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.lblname)
        Me.BlendPanel1.Controls.Add(Me.cmbname)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Name = "BlendPanel1"
        '
        'CMBOUTSTANDING
        '
        Me.CMBOUTSTANDING.BackColor = System.Drawing.Color.Transparent
        Me.CMBOUTSTANDING.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMBOUTSTANDING.FlatAppearance.BorderSize = 0
        resources.ApplyResources(Me.CMBOUTSTANDING, "CMBOUTSTANDING")
        Me.CMBOUTSTANDING.ForeColor = System.Drawing.Color.Black
        Me.CMBOUTSTANDING.Name = "CMBOUTSTANDING"
        Me.CMBOUTSTANDING.UseVisualStyleBackColor = False
        '
        'griddetails
        '
        resources.ApplyResources(Me.griddetails, "griddetails")
        Me.griddetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.griddetails.MainView = Me.gridregister
        Me.griddetails.Name = "griddetails"
        Me.griddetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridregister})
        '
        'gridregister
        '
        Me.gridregister.Appearance.HeaderPanel.Font = CType(resources.GetObject("gridregister.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.gridregister.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridregister.Appearance.Row.Font = CType(resources.GetObject("gridregister.Appearance.Row.Font"), System.Drawing.Font)
        Me.gridregister.Appearance.Row.Options.UseFont = True
        Me.gridregister.Appearance.Row.Options.UseTextOptions = True
        Me.gridregister.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gridregister.Appearance.ViewCaption.Font = CType(resources.GetObject("gridregister.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.gridregister.Appearance.ViewCaption.Options.UseFont = True
        Me.gridregister.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gDate, Me.gname, Me.gtype, Me.gbillinitials, Me.GREFNO, Me.gdr, Me.gcr, Me.GRUNNINGBAL, Me.gbillno, Me.greg, Me.GCHQNO, Me.gremarks, Me.GUSER})
        Me.gridregister.GridControl = Me.griddetails
        Me.gridregister.Name = "gridregister"
        Me.gridregister.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridregister.OptionsBehavior.AutoExpandAllGroups = True
        Me.gridregister.OptionsBehavior.Editable = False
        Me.gridregister.OptionsView.ColumnAutoWidth = False
        Me.gridregister.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridregister.OptionsView.ShowAutoFilterRow = True
        Me.gridregister.OptionsView.ShowFooter = True
        Me.gridregister.OptionsView.ShowGroupedColumns = True
        '
        'gDate
        '
        resources.ApplyResources(Me.gDate, "gDate")
        Me.gDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.gDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.gDate.FieldName = "Date"
        Me.gDate.Name = "gDate"
        Me.gDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.gDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        '
        'gname
        '
        resources.ApplyResources(Me.gname, "gname")
        Me.gname.FieldName = "Name"
        Me.gname.Name = "gname"
        Me.gname.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        '
        'gtype
        '
        resources.ApplyResources(Me.gtype, "gtype")
        Me.gtype.FieldName = "Type"
        Me.gtype.Name = "gtype"
        Me.gtype.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        '
        'gbillinitials
        '
        resources.ApplyResources(Me.gbillinitials, "gbillinitials")
        Me.gbillinitials.FieldName = "Bill No"
        Me.gbillinitials.Name = "gbillinitials"
        '
        'GREFNO
        '
        resources.ApplyResources(Me.GREFNO, "GREFNO")
        Me.GREFNO.FieldName = "REFNO"
        Me.GREFNO.Name = "GREFNO"
        '
        'gdr
        '
        resources.ApplyResources(Me.gdr, "gdr")
        Me.gdr.DisplayFormat.FormatString = "0.00"
        Me.gdr.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gdr.FieldName = "Debit"
        Me.gdr.Name = "gdr"
        Me.gdr.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.gdr.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("gdr.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("gdr.Summary1"), resources.GetString("gdr.Summary2"))})
        '
        'gcr
        '
        resources.ApplyResources(Me.gcr, "gcr")
        Me.gcr.DisplayFormat.FormatString = "0.00"
        Me.gcr.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gcr.FieldName = "Credit"
        Me.gcr.Name = "gcr"
        Me.gcr.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.gcr.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("gcr.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("gcr.Summary1"), resources.GetString("gcr.Summary2"))})
        '
        'GRUNNINGBAL
        '
        resources.ApplyResources(Me.GRUNNINGBAL, "GRUNNINGBAL")
        Me.GRUNNINGBAL.DisplayFormat.FormatString = "0.00"
        Me.GRUNNINGBAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRUNNINGBAL.FieldName = "RUNNINGBAL"
        Me.GRUNNINGBAL.Name = "GRUNNINGBAL"
        '
        'gbillno
        '
        resources.ApplyResources(Me.gbillno, "gbillno")
        Me.gbillno.FieldName = "BILL"
        Me.gbillno.Name = "gbillno"
        '
        'greg
        '
        resources.ApplyResources(Me.greg, "greg")
        Me.greg.FieldName = "REGTYPE"
        Me.greg.Name = "greg"
        '
        'GCHQNO
        '
        resources.ApplyResources(Me.GCHQNO, "GCHQNO")
        Me.GCHQNO.FieldName = "CHQNO"
        Me.GCHQNO.Name = "GCHQNO"
        '
        'gremarks
        '
        resources.ApplyResources(Me.gremarks, "gremarks")
        Me.gremarks.FieldName = "REMARKS"
        Me.gremarks.Name = "gremarks"
        Me.gremarks.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        '
        'GUSER
        '
        resources.ApplyResources(Me.GUSER, "GUSER")
        Me.GUSER.FieldName = "USERNAME"
        Me.GUSER.Name = "GUSER"
        '
        'CMBACCCODE
        '
        Me.CMBACCCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBACCCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        resources.ApplyResources(Me.CMBACCCODE, "CMBACCCODE")
        Me.CMBACCCODE.FormattingEnabled = True
        Me.CMBACCCODE.Items.AddRange(New Object() {resources.GetString("CMBACCCODE.Items")})
        Me.CMBACCCODE.Name = "CMBACCCODE"
        '
        'txtadd
        '
        resources.ApplyResources(Me.txtadd, "txtadd")
        Me.txtadd.Name = "txtadd"
        '
        'cmdshowdetails
        '
        Me.cmdshowdetails.BackColor = System.Drawing.Color.Transparent
        Me.cmdshowdetails.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdshowdetails.FlatAppearance.BorderSize = 0
        resources.ApplyResources(Me.cmdshowdetails, "cmdshowdetails")
        Me.cmdshowdetails.ForeColor = System.Drawing.Color.Black
        Me.cmdshowdetails.Name = "cmdshowdetails"
        Me.cmdshowdetails.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        resources.ApplyResources(Me.cmdok, "cmdok")
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Name = "cmdok"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        resources.ApplyResources(Me.cmdexit, "cmdexit")
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'txtcrtotal
        '
        Me.txtcrtotal.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.txtcrtotal, "txtcrtotal")
        Me.txtcrtotal.ForeColor = System.Drawing.Color.Black
        Me.txtcrtotal.Name = "txtcrtotal"
        Me.txtcrtotal.ReadOnly = True
        Me.txtcrtotal.TabStop = False
        '
        'txtdrtotal
        '
        Me.txtdrtotal.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.txtdrtotal, "txtdrtotal")
        Me.txtdrtotal.ForeColor = System.Drawing.Color.Black
        Me.txtdrtotal.Name = "txtdrtotal"
        Me.txtdrtotal.ReadOnly = True
        Me.txtdrtotal.TabStop = False
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label2.Name = "Label2"
        '
        'lbldrcropening
        '
        resources.ApplyResources(Me.lbldrcropening, "lbldrcropening")
        Me.lbldrcropening.BackColor = System.Drawing.Color.Transparent
        Me.lbldrcropening.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbldrcropening.Name = "lbldrcropening"
        '
        'lbldrcrclosing
        '
        resources.ApplyResources(Me.lbldrcrclosing, "lbldrcrclosing")
        Me.lbldrcrclosing.BackColor = System.Drawing.Color.Transparent
        Me.lbldrcrclosing.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbldrcrclosing.Name = "lbldrcrclosing"
        '
        'txttotal
        '
        Me.txttotal.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.txttotal, "txttotal")
        Me.txttotal.ForeColor = System.Drawing.Color.Black
        Me.txttotal.Name = "txttotal"
        Me.txttotal.ReadOnly = True
        Me.txttotal.TabStop = False
        '
        'lbln
        '
        resources.ApplyResources(Me.lbln, "lbln")
        Me.lbln.BackColor = System.Drawing.Color.Transparent
        Me.lbln.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbln.Name = "lbln"
        '
        'txttempbillno
        '
        resources.ApplyResources(Me.txttempbillno, "txttempbillno")
        Me.txttempbillno.Name = "txttempbillno"
        '
        'chkdate
        '
        resources.ApplyResources(Me.chkdate, "chkdate")
        Me.chkdate.BackColor = System.Drawing.Color.Transparent
        Me.chkdate.Name = "chkdate"
        Me.chkdate.UseVisualStyleBackColor = False
        '
        'dtto
        '
        resources.ApplyResources(Me.dtto, "dtto")
        Me.dtto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtto.Name = "dtto"
        '
        'lblto
        '
        resources.ApplyResources(Me.lblto, "lblto")
        Me.lblto.BackColor = System.Drawing.Color.Transparent
        Me.lblto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblto.Name = "lblto"
        '
        'dtfrom
        '
        resources.ApplyResources(Me.dtfrom, "dtfrom")
        Me.dtfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtfrom.Name = "dtfrom"
        '
        'lblfrom
        '
        resources.ApplyResources(Me.lblfrom, "lblfrom")
        Me.lblfrom.BackColor = System.Drawing.Color.Transparent
        Me.lblfrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblfrom.Name = "lblfrom"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripButton, Me.ToolStripSeparator2, Me.ExcelExport, Me.ToolStripSeparator1, Me.TOOLDAILY, Me.ToolStripSeparator4, Me.TOOLMONTHLY, Me.ToolStripSeparator5})
        resources.ApplyResources(Me.ToolStrip1, "ToolStrip1")
        Me.ToolStrip1.Name = "ToolStrip1"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.PrintToolStripButton, "PrintToolStripButton")
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'ExcelExport
        '
        Me.ExcelExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ExcelExport.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        resources.ApplyResources(Me.ExcelExport, "ExcelExport")
        Me.ExcelExport.Name = "ExcelExport"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'TOOLDAILY
        '
        Me.TOOLDAILY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        resources.ApplyResources(Me.TOOLDAILY, "TOOLDAILY")
        Me.TOOLDAILY.Name = "TOOLDAILY"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        '
        'TOOLMONTHLY
        '
        Me.TOOLMONTHLY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        resources.ApplyResources(Me.TOOLMONTHLY, "TOOLMONTHLY")
        Me.TOOLMONTHLY.Name = "TOOLMONTHLY"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        resources.ApplyResources(Me.ToolStripSeparator5, "ToolStripSeparator5")
        '
        'lblname
        '
        resources.ApplyResources(Me.lblname, "lblname")
        Me.lblname.BackColor = System.Drawing.Color.Transparent
        Me.lblname.ForeColor = System.Drawing.Color.Black
        Me.lblname.Name = "lblname"
        '
        'cmbname
        '
        Me.cmbname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        resources.ApplyResources(Me.cmbname, "cmbname")
        Me.cmbname.FormattingEnabled = True
        Me.cmbname.Name = "cmbname"
        '
        'lbl
        '
        resources.ApplyResources(Me.lbl, "lbl")
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl.Name = "lbl"
        '
        'PartyWiseBaleRateReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.BlendPanel1)
        Me.KeyPreview = True
        Me.Name = "PartyWiseBaleRateReport"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.griddetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridregister, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMBOUTSTANDING As Button
    Private WithEvents griddetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridregister As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents gDate As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gname As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gtype As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gbillinitials As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREFNO As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gdr As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gcr As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRUNNINGBAL As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gbillno As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents greg As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHQNO As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gremarks As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GUSER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMBACCCODE As ComboBox
    Friend WithEvents txtadd As TextBox
    Friend WithEvents cmdshowdetails As Button
    Friend WithEvents cmdok As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents txtcrtotal As TextBox
    Friend WithEvents txtdrtotal As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lbldrcropening As Label
    Friend WithEvents lbldrcrclosing As Label
    Friend WithEvents txttotal As TextBox
    Friend WithEvents lbln As Label
    Friend WithEvents txttempbillno As TextBox
    Friend WithEvents chkdate As CheckBox
    Friend WithEvents dtto As DateTimePicker
    Friend WithEvents lblto As Label
    Friend WithEvents dtfrom As DateTimePicker
    Friend WithEvents lblfrom As Label
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ExcelExport As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TOOLDAILY As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents TOOLMONTHLY As ToolStripButton
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents lblname As Label
    Friend WithEvents cmbname As ComboBox
    Friend WithEvents lbl As Label
End Class
