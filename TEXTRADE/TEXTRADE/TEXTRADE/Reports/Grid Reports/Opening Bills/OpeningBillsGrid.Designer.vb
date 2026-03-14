<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OpeningBillsGrid
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
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.lbldrcropening = New System.Windows.Forms.Label()
        Me.txtopening = New System.Windows.Forms.TextBox()
        Me.lblopbal = New System.Windows.Forms.Label()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.CMBACCCODE = New System.Windows.Forms.ComboBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBILLTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREGISTER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GYEAR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBILLDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCRDAYS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDUEDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNARRATION = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDISPUTE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDELIVERYAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCHARGES = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTAXABLEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCGSTPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSGSTPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GIGSTPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GIGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBALANCE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GHOLDINTCALC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMBREASON = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CMBREASON, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.lbldrcropening)
        Me.BlendPanel1.Controls.Add(Me.txtopening)
        Me.BlendPanel1.Controls.Add(Me.lblopbal)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.CMBACCCODE)
        Me.BlendPanel1.Controls.Add(Me.Label33)
        Me.BlendPanel1.Controls.Add(Me.CMBNAME)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 1
        '
        'lbldrcropening
        '
        Me.lbldrcropening.AutoSize = True
        Me.lbldrcropening.BackColor = System.Drawing.Color.Transparent
        Me.lbldrcropening.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldrcropening.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbldrcropening.Location = New System.Drawing.Point(1145, 32)
        Me.lbldrcropening.Name = "lbldrcropening"
        Me.lbldrcropening.Size = New System.Drawing.Size(0, 14)
        Me.lbldrcropening.TabIndex = 654
        '
        'txtopening
        '
        Me.txtopening.BackColor = System.Drawing.Color.Linen
        Me.txtopening.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtopening.ForeColor = System.Drawing.Color.Black
        Me.txtopening.Location = New System.Drawing.Point(1043, 28)
        Me.txtopening.Name = "txtopening"
        Me.txtopening.ReadOnly = True
        Me.txtopening.Size = New System.Drawing.Size(100, 22)
        Me.txtopening.TabIndex = 653
        Me.txtopening.TabStop = False
        Me.txtopening.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblopbal
        '
        Me.lblopbal.AutoSize = True
        Me.lblopbal.BackColor = System.Drawing.Color.Transparent
        Me.lblopbal.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblopbal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblopbal.Location = New System.Drawing.Point(993, 32)
        Me.lblopbal.Name = "lblopbal"
        Me.lblopbal.Size = New System.Drawing.Size(45, 14)
        Me.lblopbal.TabIndex = 652
        Me.lblopbal.Text = "O/P Bal"
        '
        'TXTADD
        '
        Me.TXTADD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.Location = New System.Drawing.Point(505, 23)
        Me.TXTADD.MaxLength = 10
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(30, 22)
        Me.TXTADD.TabIndex = 651
        Me.TXTADD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTADD.Visible = False
        '
        'CMBACCCODE
        '
        Me.CMBACCCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBACCCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBACCCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBACCCODE.FormattingEnabled = True
        Me.CMBACCCODE.Items.AddRange(New Object() {""})
        Me.CMBACCCODE.Location = New System.Drawing.Point(887, 24)
        Me.CMBACCCODE.Name = "CMBACCCODE"
        Me.CMBACCCODE.Size = New System.Drawing.Size(74, 22)
        Me.CMBACCCODE.TabIndex = 650
        Me.CMBACCCODE.Visible = False
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(17, 32)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(89, 14)
        Me.Label33.TabIndex = 644
        Me.Label33.Text = "Party A/C Name"
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Items.AddRange(New Object() {""})
        Me.CMBNAME.Location = New System.Drawing.Point(108, 28)
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(269, 22)
        Me.CMBNAME.TabIndex = 643
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.Black
        Me.CMDREFRESH.Location = New System.Drawing.Point(602, 541)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREFRESH.TabIndex = 5
        Me.CMDREFRESH.Text = "&Refresh"
        Me.CMDREFRESH.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(12, 56)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CMBREASON})
        Me.gridbilldetails.Size = New System.Drawing.Size(1194, 479)
        Me.gridbilldetails.TabIndex = 3
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GSRNO, Me.GBILLTYPE, Me.GREGISTER, Me.GBILLNO, Me.GYEAR, Me.GBILLDATE, Me.GCRDAYS, Me.GDUEDATE, Me.GAGENT, Me.GNARRATION, Me.GDISPUTE, Me.GDELIVERYAT, Me.GPCS, Me.GMTRS, Me.GTOTALAMT, Me.GCHARGES, Me.GTAXABLEAMT, Me.GCGSTPER, Me.GCGSTAMT, Me.GSGSTPER, Me.GSGSTAMT, Me.GIGSTPER, Me.GIGSTAMT, Me.GGRANDTOTAL, Me.GBALANCE, Me.GHOLDINTCALC})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GSRNO
        '
        Me.GSRNO.Caption = "Sr No"
        Me.GSRNO.FieldName = "GRIDSRNO"
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.OptionsColumn.AllowEdit = False
        Me.GSRNO.Visible = True
        Me.GSRNO.VisibleIndex = 0
        Me.GSRNO.Width = 40
        '
        'GBILLTYPE
        '
        Me.GBILLTYPE.Caption = "Bill Type"
        Me.GBILLTYPE.FieldName = "BILLTYPE"
        Me.GBILLTYPE.Name = "GBILLTYPE"
        Me.GBILLTYPE.OptionsColumn.AllowEdit = False
        Me.GBILLTYPE.Visible = True
        Me.GBILLTYPE.VisibleIndex = 1
        Me.GBILLTYPE.Width = 100
        '
        'GREGISTER
        '
        Me.GREGISTER.Caption = "Register"
        Me.GREGISTER.FieldName = "REGISTER"
        Me.GREGISTER.Name = "GREGISTER"
        Me.GREGISTER.OptionsColumn.AllowEdit = False
        Me.GREGISTER.Visible = True
        Me.GREGISTER.VisibleIndex = 2
        Me.GREGISTER.Width = 120
        '
        'GBILLNO
        '
        Me.GBILLNO.Caption = "Bill No"
        Me.GBILLNO.FieldName = "BILLNO"
        Me.GBILLNO.Name = "GBILLNO"
        Me.GBILLNO.OptionsColumn.AllowEdit = False
        Me.GBILLNO.Visible = True
        Me.GBILLNO.VisibleIndex = 3
        '
        'GYEAR
        '
        Me.GYEAR.Caption = "Year"
        Me.GYEAR.FieldName = "YEAR"
        Me.GYEAR.Name = "GYEAR"
        Me.GYEAR.OptionsColumn.AllowEdit = False
        Me.GYEAR.Visible = True
        Me.GYEAR.VisibleIndex = 4
        '
        'GBILLDATE
        '
        Me.GBILLDATE.Caption = "Bill Date"
        Me.GBILLDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GBILLDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GBILLDATE.FieldName = "BILLDATE"
        Me.GBILLDATE.Name = "GBILLDATE"
        Me.GBILLDATE.OptionsColumn.AllowEdit = False
        Me.GBILLDATE.Visible = True
        Me.GBILLDATE.VisibleIndex = 5
        '
        'GCRDAYS
        '
        Me.GCRDAYS.Caption = "Cr Days"
        Me.GCRDAYS.FieldName = "CRDAYS"
        Me.GCRDAYS.Name = "GCRDAYS"
        Me.GCRDAYS.OptionsColumn.AllowEdit = False
        Me.GCRDAYS.Visible = True
        Me.GCRDAYS.VisibleIndex = 6
        '
        'GDUEDATE
        '
        Me.GDUEDATE.Caption = "Due Date"
        Me.GDUEDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GDUEDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GDUEDATE.FieldName = "DUEDATE"
        Me.GDUEDATE.Name = "GDUEDATE"
        Me.GDUEDATE.OptionsColumn.AllowEdit = False
        Me.GDUEDATE.Visible = True
        Me.GDUEDATE.VisibleIndex = 7
        '
        'GAGENT
        '
        Me.GAGENT.Caption = "Agent"
        Me.GAGENT.FieldName = "AGENT"
        Me.GAGENT.Name = "GAGENT"
        Me.GAGENT.OptionsColumn.AllowEdit = False
        Me.GAGENT.Visible = True
        Me.GAGENT.VisibleIndex = 8
        Me.GAGENT.Width = 200
        '
        'GNARRATION
        '
        Me.GNARRATION.Caption = "Narration"
        Me.GNARRATION.FieldName = "NARRATION"
        Me.GNARRATION.Name = "GNARRATION"
        Me.GNARRATION.OptionsColumn.AllowEdit = False
        Me.GNARRATION.Visible = True
        Me.GNARRATION.VisibleIndex = 9
        Me.GNARRATION.Width = 200
        '
        'GDISPUTE
        '
        Me.GDISPUTE.Caption = "Dispute"
        Me.GDISPUTE.FieldName = "DISPUTE"
        Me.GDISPUTE.Name = "GDISPUTE"
        Me.GDISPUTE.OptionsColumn.AllowEdit = False
        Me.GDISPUTE.Visible = True
        Me.GDISPUTE.VisibleIndex = 10
        '
        'GDELIVERYAT
        '
        Me.GDELIVERYAT.Caption = "Delivery At"
        Me.GDELIVERYAT.FieldName = "DELIVERYAT"
        Me.GDELIVERYAT.Name = "GDELIVERYAT"
        Me.GDELIVERYAT.OptionsColumn.AllowEdit = False
        Me.GDELIVERYAT.Visible = True
        Me.GDELIVERYAT.VisibleIndex = 11
        Me.GDELIVERYAT.Width = 150
        '
        'GPCS
        '
        Me.GPCS.Caption = "Pcs"
        Me.GPCS.FieldName = "PCS"
        Me.GPCS.Name = "GPCS"
        Me.GPCS.OptionsColumn.AllowEdit = False
        Me.GPCS.Visible = True
        Me.GPCS.VisibleIndex = 12
        '
        'GMTRS
        '
        Me.GMTRS.Caption = "Mtrs"
        Me.GMTRS.FieldName = "MTRS"
        Me.GMTRS.Name = "GMTRS"
        Me.GMTRS.OptionsColumn.AllowEdit = False
        Me.GMTRS.Visible = True
        Me.GMTRS.VisibleIndex = 13
        '
        'GTOTALAMT
        '
        Me.GTOTALAMT.Caption = "Total Amount"
        Me.GTOTALAMT.FieldName = "TOTALAMT"
        Me.GTOTALAMT.Name = "GTOTALAMT"
        Me.GTOTALAMT.OptionsColumn.AllowEdit = False
        Me.GTOTALAMT.Visible = True
        Me.GTOTALAMT.VisibleIndex = 14
        Me.GTOTALAMT.Width = 100
        '
        'GCHARGES
        '
        Me.GCHARGES.Caption = "Charges"
        Me.GCHARGES.FieldName = "CHARGES"
        Me.GCHARGES.Name = "GCHARGES"
        Me.GCHARGES.OptionsColumn.AllowEdit = False
        Me.GCHARGES.Visible = True
        Me.GCHARGES.VisibleIndex = 15
        Me.GCHARGES.Width = 90
        '
        'GTAXABLEAMT
        '
        Me.GTAXABLEAMT.Caption = "Taxable Amount"
        Me.GTAXABLEAMT.DisplayFormat.FormatString = "0.00"
        Me.GTAXABLEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTAXABLEAMT.FieldName = "TAXABLEAMT"
        Me.GTAXABLEAMT.Name = "GTAXABLEAMT"
        Me.GTAXABLEAMT.OptionsColumn.AllowEdit = False
        Me.GTAXABLEAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTAXABLEAMT.Visible = True
        Me.GTAXABLEAMT.VisibleIndex = 16
        Me.GTAXABLEAMT.Width = 100
        '
        'GCGSTPER
        '
        Me.GCGSTPER.Caption = "CGST %"
        Me.GCGSTPER.FieldName = "CGSTPER"
        Me.GCGSTPER.Name = "GCGSTPER"
        Me.GCGSTPER.OptionsColumn.AllowEdit = False
        Me.GCGSTPER.Visible = True
        Me.GCGSTPER.VisibleIndex = 17
        Me.GCGSTPER.Width = 60
        '
        'GCGSTAMT
        '
        Me.GCGSTAMT.Caption = "CGST Amt"
        Me.GCGSTAMT.FieldName = "CGSTAMT"
        Me.GCGSTAMT.Name = "GCGSTAMT"
        Me.GCGSTAMT.OptionsColumn.AllowEdit = False
        Me.GCGSTAMT.Visible = True
        Me.GCGSTAMT.VisibleIndex = 18
        '
        'GSGSTPER
        '
        Me.GSGSTPER.Caption = "SGST %"
        Me.GSGSTPER.FieldName = "SGSTPER"
        Me.GSGSTPER.Name = "GSGSTPER"
        Me.GSGSTPER.OptionsColumn.AllowEdit = False
        Me.GSGSTPER.Visible = True
        Me.GSGSTPER.VisibleIndex = 19
        Me.GSGSTPER.Width = 60
        '
        'GSGSTAMT
        '
        Me.GSGSTAMT.Caption = "SGST Amt"
        Me.GSGSTAMT.FieldName = "SGSTAMT"
        Me.GSGSTAMT.Name = "GSGSTAMT"
        Me.GSGSTAMT.OptionsColumn.AllowEdit = False
        Me.GSGSTAMT.Visible = True
        Me.GSGSTAMT.VisibleIndex = 20
        '
        'GIGSTPER
        '
        Me.GIGSTPER.Caption = "IGST %"
        Me.GIGSTPER.FieldName = "IGSTPER"
        Me.GIGSTPER.Name = "GIGSTPER"
        Me.GIGSTPER.OptionsColumn.AllowEdit = False
        Me.GIGSTPER.Visible = True
        Me.GIGSTPER.VisibleIndex = 21
        Me.GIGSTPER.Width = 60
        '
        'GIGSTAMT
        '
        Me.GIGSTAMT.Caption = "IGST Amt"
        Me.GIGSTAMT.FieldName = "IGSTAMT"
        Me.GIGSTAMT.Name = "GIGSTAMT"
        Me.GIGSTAMT.OptionsColumn.AllowEdit = False
        Me.GIGSTAMT.Visible = True
        Me.GIGSTAMT.VisibleIndex = 22
        '
        'GGRANDTOTAL
        '
        Me.GGRANDTOTAL.Caption = "Grand Total"
        Me.GGRANDTOTAL.DisplayFormat.FormatString = "0.00"
        Me.GGRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.GGRANDTOTAL.Name = "GGRANDTOTAL"
        Me.GGRANDTOTAL.OptionsColumn.AllowEdit = False
        Me.GGRANDTOTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GGRANDTOTAL.Visible = True
        Me.GGRANDTOTAL.VisibleIndex = 23
        Me.GGRANDTOTAL.Width = 100
        '
        'GBALANCE
        '
        Me.GBALANCE.Caption = "Bal Amount"
        Me.GBALANCE.DisplayFormat.FormatString = "0.00"
        Me.GBALANCE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GBALANCE.FieldName = "BALANCE"
        Me.GBALANCE.Name = "GBALANCE"
        Me.GBALANCE.OptionsColumn.AllowEdit = False
        Me.GBALANCE.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GBALANCE.Visible = True
        Me.GBALANCE.VisibleIndex = 24
        Me.GBALANCE.Width = 100
        '
        'GHOLDINTCALC
        '
        Me.GHOLDINTCALC.Caption = "Hold Int Calc"
        Me.GHOLDINTCALC.FieldName = "HOLDINTCALC"
        Me.GHOLDINTCALC.Name = "GHOLDINTCALC"
        Me.GHOLDINTCALC.Visible = True
        Me.GHOLDINTCALC.VisibleIndex = 25
        '
        'CMBREASON
        '
        Me.CMBREASON.AutoHeight = False
        Me.CMBREASON.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CMBREASON.Name = "CMBREASON"
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(688, 541)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 6
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripButton, Me.toolStripSeparator})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 7
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'OpeningBillsGrid
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "OpeningBillsGrid"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Opening Bills Grid"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CMBREASON, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDREFRESH As Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CMBREASON As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents cmdexit As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents GSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBILLTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GYEAR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBILLDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCRDAYS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDUEDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNARRATION As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDISPUTE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHARGES As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTAXABLEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCGSTPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSGSTPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GIGSTPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GIGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBALANCE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREGISTER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDELIVERYAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GHOLDINTCALC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Label33 As Label
    Friend WithEvents CMBNAME As ComboBox
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents CMBACCCODE As ComboBox
    Friend WithEvents lbldrcropening As Label
    Friend WithEvents txtopening As TextBox
    Friend WithEvents lblopbal As Label
End Class
