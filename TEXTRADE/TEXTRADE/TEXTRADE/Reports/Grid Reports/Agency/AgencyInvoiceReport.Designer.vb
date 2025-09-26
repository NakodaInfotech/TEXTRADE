<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AgencyInvoiceReport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AgencyInvoiceReport))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gsrno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gdate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBUYERNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELLERNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSHIPPEDTO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTRANSPORT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDISC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSPDISCAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCHARGES = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALTAXABLEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDISCPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDISCOUNT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCDPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCASHDISC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCREATEDBY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREFERREDBY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTRADINGACC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMONTHNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKEDIT = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.TOOLMAIL = New System.Windows.Forms.ToolStripButton()
        Me.TOOLWHATSAPP = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.lbl = New System.Windows.Forms.Label()
        Me.GBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GQUALITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFOLD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GVPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOMNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDRCR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtto = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtfrom = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkdate = New System.Windows.Forms.CheckBox()
        Me.cmdshow = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.cmdshow)
        Me.BlendPanel1.Controls.Add(Me.GroupBox1)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1440, 670)
        Me.BlendPanel1.TabIndex = 1
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(22, 99)
        Me.gridbilldetails.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKEDIT})
        Me.gridbilldetails.Size = New System.Drawing.Size(1200, 472)
        Me.gridbilldetails.TabIndex = 0
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHK, Me.gsrno, Me.GBILLNO, Me.GPO, Me.gdate, Me.GBUYERNAME, Me.GGD, Me.GSELLERNAME, Me.GTRANSPORT, Me.GLRNO, Me.GQUALITY, Me.GPCS, Me.GMTRS, Me.GFOLD, Me.GRATE, Me.GVPER, Me.GBPER, Me.GAMT, Me.GPDATE, Me.GRNO, Me.GDRCR, Me.GCOMNO, Me.GCID, Me.GDISC, Me.GSPDISCAMT, Me.GCHARGES, Me.GTOTALTAXABLEAMT, Me.GREMARKS, Me.GDISCPER, Me.GDISCOUNT, Me.GCDPER, Me.GCASHDISC, Me.GSHIPPEDTO, Me.GCREATEDBY, Me.GREFERREDBY, Me.GTRADINGACC, Me.GMONTHNAME})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        '
        'GCHK
        '
        Me.GCHK.FieldName = "CHK"
        Me.GCHK.Name = "GCHK"
        Me.GCHK.Visible = True
        Me.GCHK.VisibleIndex = 0
        Me.GCHK.Width = 30
        '
        'gsrno
        '
        Me.gsrno.Caption = "Sr. No"
        Me.gsrno.FieldName = "SRNO"
        Me.gsrno.ImageOptions.ImageIndex = 1
        Me.gsrno.Name = "gsrno"
        Me.gsrno.OptionsColumn.AllowEdit = False
        Me.gsrno.Visible = True
        Me.gsrno.VisibleIndex = 1
        Me.gsrno.Width = 60
        '
        'gdate
        '
        Me.gdate.Caption = "Date"
        Me.gdate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.gdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.gdate.FieldName = "DATE"
        Me.gdate.Name = "gdate"
        Me.gdate.OptionsColumn.AllowEdit = False
        Me.gdate.Visible = True
        Me.gdate.VisibleIndex = 3
        '
        'GBUYERNAME
        '
        Me.GBUYERNAME.Caption = "Buyer Name"
        Me.GBUYERNAME.FieldName = "BUYERNAME"
        Me.GBUYERNAME.ImageOptions.ImageIndex = 0
        Me.GBUYERNAME.Name = "GBUYERNAME"
        Me.GBUYERNAME.OptionsColumn.AllowEdit = False
        Me.GBUYERNAME.Visible = True
        Me.GBUYERNAME.VisibleIndex = 4
        Me.GBUYERNAME.Width = 150
        '
        'GSELLERNAME
        '
        Me.GSELLERNAME.Caption = "Seller Name"
        Me.GSELLERNAME.FieldName = "SELLERNAME"
        Me.GSELLERNAME.Name = "GSELLERNAME"
        Me.GSELLERNAME.OptionsColumn.AllowEdit = False
        Me.GSELLERNAME.Visible = True
        Me.GSELLERNAME.VisibleIndex = 6
        Me.GSELLERNAME.Width = 150
        '
        'GSHIPPEDTO
        '
        Me.GSHIPPEDTO.Caption = "Shipped To"
        Me.GSHIPPEDTO.FieldName = "PACKING"
        Me.GSHIPPEDTO.Name = "GSHIPPEDTO"
        Me.GSHIPPEDTO.OptionsColumn.AllowEdit = False
        Me.GSHIPPEDTO.Width = 100
        '
        'GTRANSPORT
        '
        Me.GTRANSPORT.Caption = "Transport"
        Me.GTRANSPORT.FieldName = "TRANSNAME"
        Me.GTRANSPORT.Name = "GTRANSPORT"
        Me.GTRANSPORT.OptionsColumn.AllowEdit = False
        Me.GTRANSPORT.Visible = True
        Me.GTRANSPORT.VisibleIndex = 7
        Me.GTRANSPORT.Width = 150
        '
        'GLRNO
        '
        Me.GLRNO.Caption = "LR No"
        Me.GLRNO.FieldName = "LRNO"
        Me.GLRNO.Name = "GLRNO"
        Me.GLRNO.OptionsColumn.AllowEdit = False
        Me.GLRNO.Visible = True
        Me.GLRNO.VisibleIndex = 8
        '
        'GPO
        '
        Me.GPO.Caption = "PO No."
        Me.GPO.FieldName = "PONO"
        Me.GPO.Name = "GPO"
        Me.GPO.OptionsColumn.AllowEdit = False
        Me.GPO.Visible = True
        Me.GPO.VisibleIndex = 10
        '
        'GPCS
        '
        Me.GPCS.Caption = "Pcs"
        Me.GPCS.DisplayFormat.FormatString = "0.00"
        Me.GPCS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPCS.FieldName = "PCS"
        Me.GPCS.Name = "GPCS"
        Me.GPCS.OptionsColumn.AllowEdit = False
        Me.GPCS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GPCS.Visible = True
        Me.GPCS.VisibleIndex = 11
        Me.GPCS.Width = 85
        '
        'GMTRS
        '
        Me.GMTRS.Caption = "Mtrs"
        Me.GMTRS.DisplayFormat.FormatString = "0.00"
        Me.GMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GMTRS.FieldName = "MTRS"
        Me.GMTRS.Name = "GMTRS"
        Me.GMTRS.OptionsColumn.AllowEdit = False
        Me.GMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GMTRS.Visible = True
        Me.GMTRS.VisibleIndex = 16
        Me.GMTRS.Width = 85
        '
        'GAMT
        '
        Me.GAMT.Caption = "Amount"
        Me.GAMT.DisplayFormat.FormatString = "0.00"
        Me.GAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GAMT.FieldName = "AMT"
        Me.GAMT.Name = "GAMT"
        Me.GAMT.OptionsColumn.AllowEdit = False
        Me.GAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GAMT.Visible = True
        Me.GAMT.VisibleIndex = 17
        '
        'GDISC
        '
        Me.GDISC.Caption = "Total Disc Amt."
        Me.GDISC.FieldName = "TOTALDISC"
        Me.GDISC.Name = "GDISC"
        Me.GDISC.OptionsColumn.AllowEdit = False
        Me.GDISC.Width = 80
        '
        'GSPDISCAMT
        '
        Me.GSPDISCAMT.Caption = "Total Sp Disc Amt."
        Me.GSPDISCAMT.FieldName = "TOTALSPDISCAMT"
        Me.GSPDISCAMT.Name = "GSPDISCAMT"
        Me.GSPDISCAMT.OptionsColumn.AllowEdit = False
        '
        'GCHARGES
        '
        Me.GCHARGES.Caption = "Charges"
        Me.GCHARGES.FieldName = "CHARGES"
        Me.GCHARGES.Name = "GCHARGES"
        Me.GCHARGES.OptionsColumn.AllowEdit = False
        '
        'GTOTALTAXABLEAMT
        '
        Me.GTOTALTAXABLEAMT.Caption = "Total Taxable Amt."
        Me.GTOTALTAXABLEAMT.DisplayFormat.FormatString = "0.00"
        Me.GTOTALTAXABLEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALTAXABLEAMT.FieldName = "TOTALTAXABLEAMT"
        Me.GTOTALTAXABLEAMT.Name = "GTOTALTAXABLEAMT"
        Me.GTOTALTAXABLEAMT.OptionsColumn.AllowEdit = False
        Me.GTOTALTAXABLEAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "REMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.OptionsColumn.AllowEdit = False
        Me.GREMARKS.Width = 150
        '
        'GDISCPER
        '
        Me.GDISCPER.Caption = "Disc %"
        Me.GDISCPER.DisplayFormat.FormatString = "0.00"
        Me.GDISCPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GDISCPER.FieldName = "DISCOUNTPER"
        Me.GDISCPER.Name = "GDISCPER"
        Me.GDISCPER.OptionsColumn.AllowEdit = False
        '
        'GDISCOUNT
        '
        Me.GDISCOUNT.Caption = "Disc"
        Me.GDISCOUNT.DisplayFormat.FormatString = "0.00"
        Me.GDISCOUNT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GDISCOUNT.FieldName = "DISCOUNT"
        Me.GDISCOUNT.Name = "GDISCOUNT"
        Me.GDISCOUNT.OptionsColumn.AllowEdit = False
        Me.GDISCOUNT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        '
        'GCDPER
        '
        Me.GCDPER.Caption = "C.D. %"
        Me.GCDPER.DisplayFormat.FormatString = "0.00"
        Me.GCDPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCDPER.FieldName = "CDPER"
        Me.GCDPER.Name = "GCDPER"
        Me.GCDPER.OptionsColumn.AllowEdit = False
        '
        'GCASHDISC
        '
        Me.GCASHDISC.Caption = "Cash Disc"
        Me.GCASHDISC.DisplayFormat.FormatString = "0.00"
        Me.GCASHDISC.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCASHDISC.FieldName = "CASHDISC"
        Me.GCASHDISC.Name = "GCASHDISC"
        Me.GCASHDISC.OptionsColumn.AllowEdit = False
        Me.GCASHDISC.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        '
        'GCREATEDBY
        '
        Me.GCREATEDBY.Caption = "Created By"
        Me.GCREATEDBY.FieldName = "CREATEDBY"
        Me.GCREATEDBY.Name = "GCREATEDBY"
        Me.GCREATEDBY.OptionsColumn.AllowEdit = False
        '
        'GREFERREDBY
        '
        Me.GREFERREDBY.Caption = "Referred by"
        Me.GREFERREDBY.FieldName = "REFERREDBY"
        Me.GREFERREDBY.Name = "GREFERREDBY"
        Me.GREFERREDBY.OptionsColumn.AllowEdit = False
        '
        'GTRADINGACC
        '
        Me.GTRADINGACC.Caption = "Trading Acc"
        Me.GTRADINGACC.FieldName = "TRADINGACC"
        Me.GTRADINGACC.Name = "GTRADINGACC"
        Me.GTRADINGACC.OptionsColumn.AllowEdit = False
        '
        'GMONTHNAME
        '
        Me.GMONTHNAME.Caption = "Month Name"
        Me.GMONTHNAME.FieldName = "MONTHNAME"
        Me.GMONTHNAME.Name = "GMONTHNAME"
        Me.GMONTHNAME.OptionsColumn.AllowEdit = False
        Me.GMONTHNAME.Width = 100
        '
        'CHKEDIT
        '
        Me.CHKEDIT.AutoHeight = False
        Me.CHKEDIT.Name = "CHKEDIT"
        Me.CHKEDIT.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(535, 579)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 2
        Me.CMDOK.Text = "&Ok"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.Color.Transparent
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdcancel.FlatAppearance.BorderSize = 0
        Me.cmdcancel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.Black
        Me.cmdcancel.Location = New System.Drawing.Point(619, 578)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 3
        Me.cmdcancel.Text = "E&xit"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.toolStripSeparator, Me.PrintToolStripButton, Me.TOOLMAIL, Me.TOOLWHATSAPP, Me.ToolStripButton2, Me.ToolStripSeparator1, Me.ToolStripSeparator2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1440, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(59, 22)
        Me.ToolStripButton1.Text = "Add New"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'TOOLMAIL
        '
        Me.TOOLMAIL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLMAIL.Image = Global.TEXTRADE.My.Resources.Resources.MAIL_IMAGE
        Me.TOOLMAIL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLMAIL.Name = "TOOLMAIL"
        Me.TOOLMAIL.Size = New System.Drawing.Size(23, 22)
        Me.TOOLMAIL.Text = "Mail Invoice Directly"
        '
        'TOOLWHATSAPP
        '
        Me.TOOLWHATSAPP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLWHATSAPP.Image = Global.TEXTRADE.My.Resources.Resources.WHATSAPP
        Me.TOOLWHATSAPP.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLWHATSAPP.Name = "TOOLWHATSAPP"
        Me.TOOLWHATSAPP.Size = New System.Drawing.Size(23, 22)
        Me.TOOLWHATSAPP.Text = "&Whatsapp"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "&Print"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl.Location = New System.Drawing.Point(20, 82)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(156, 14)
        Me.lbl.TabIndex = 251
        Me.lbl.Text = "Select an Invoice to Change"
        '
        'GBILLNO
        '
        Me.GBILLNO.Caption = "Bill No"
        Me.GBILLNO.FieldName = "BILLNO"
        Me.GBILLNO.Name = "GBILLNO"
        Me.GBILLNO.Visible = True
        Me.GBILLNO.VisibleIndex = 2
        '
        'GGD
        '
        Me.GGD.Caption = "GD"
        Me.GGD.FieldName = "GD"
        Me.GGD.Name = "GGD"
        Me.GGD.Visible = True
        Me.GGD.VisibleIndex = 5
        '
        'GQUALITY
        '
        Me.GQUALITY.Caption = "Quality"
        Me.GQUALITY.FieldName = "QUALITY"
        Me.GQUALITY.Name = "GQUALITY"
        Me.GQUALITY.Visible = True
        Me.GQUALITY.VisibleIndex = 9
        '
        'GFOLD
        '
        Me.GFOLD.Caption = "Fold"
        Me.GFOLD.FieldName = "FOLD"
        Me.GFOLD.Name = "GFOLD"
        Me.GFOLD.Visible = True
        Me.GFOLD.VisibleIndex = 13
        '
        'GRATE
        '
        Me.GRATE.Caption = "Rate"
        Me.GRATE.FieldName = "RATE"
        Me.GRATE.Name = "GRATE"
        Me.GRATE.Visible = True
        Me.GRATE.VisibleIndex = 12
        '
        'GBPER
        '
        Me.GBPER.Caption = "B %"
        Me.GBPER.FieldName = "BPER"
        Me.GBPER.Name = "GBPER"
        Me.GBPER.Visible = True
        Me.GBPER.VisibleIndex = 15
        '
        'GVPER
        '
        Me.GVPER.Caption = "V %"
        Me.GVPER.FieldName = "VPER"
        Me.GVPER.Name = "GVPER"
        Me.GVPER.Visible = True
        Me.GVPER.VisibleIndex = 14
        '
        'GCID
        '
        Me.GCID.Caption = "CID"
        Me.GCID.FieldName = "CID"
        Me.GCID.Name = "GCID"
        Me.GCID.Visible = True
        Me.GCID.VisibleIndex = 22
        '
        'GCOMNO
        '
        Me.GCOMNO.Caption = "Com No"
        Me.GCOMNO.FieldName = "COMNO"
        Me.GCOMNO.Name = "GCOMNO"
        Me.GCOMNO.Visible = True
        Me.GCOMNO.VisibleIndex = 21
        '
        'GDRCR
        '
        Me.GDRCR.Caption = "Dr/Cr"
        Me.GDRCR.FieldName = "DRCR"
        Me.GDRCR.Name = "GDRCR"
        Me.GDRCR.Visible = True
        Me.GDRCR.VisibleIndex = 20
        '
        'GRNO
        '
        Me.GRNO.Caption = "R. No"
        Me.GRNO.FieldName = "RNO"
        Me.GRNO.Name = "GRNO"
        Me.GRNO.Visible = True
        Me.GRNO.VisibleIndex = 19
        '
        'GPDATE
        '
        Me.GPDATE.Caption = "P. Date"
        Me.GPDATE.FieldName = "PDATE"
        Me.GPDATE.Name = "GPDATE"
        Me.GPDATE.Visible = True
        Me.GPDATE.VisibleIndex = 18
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.dtto)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtfrom)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.chkdate)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(22, 26)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 53)
        Me.GroupBox1.TabIndex = 252
        Me.GroupBox1.TabStop = False
        '
        'dtto
        '
        Me.dtto.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.dtto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtto.Location = New System.Drawing.Point(189, 20)
        Me.dtto.Name = "dtto"
        Me.dtto.Size = New System.Drawing.Size(83, 22)
        Me.dtto.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(161, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "To :"
        '
        'dtfrom
        '
        Me.dtfrom.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.dtfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtfrom.Location = New System.Drawing.Point(50, 20)
        Me.dtfrom.Name = "dtfrom"
        Me.dtfrom.Size = New System.Drawing.Size(83, 22)
        Me.dtfrom.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(9, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 14)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "From :"
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.BackColor = System.Drawing.Color.Transparent
        Me.chkdate.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdate.ForeColor = System.Drawing.Color.Black
        Me.chkdate.Location = New System.Drawing.Point(6, 0)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(52, 18)
        Me.chkdate.TabIndex = 0
        Me.chkdate.Text = "Date"
        Me.chkdate.UseVisualStyleBackColor = False
        '
        'cmdshow
        '
        Me.cmdshow.BackColor = System.Drawing.Color.Transparent
        Me.cmdshow.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdshow.FlatAppearance.BorderSize = 0
        Me.cmdshow.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdshow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdshow.Location = New System.Drawing.Point(343, 44)
        Me.cmdshow.Name = "cmdshow"
        Me.cmdshow.Size = New System.Drawing.Size(88, 28)
        Me.cmdshow.TabIndex = 253
        Me.cmdshow.Text = "&Show Details"
        Me.cmdshow.UseVisualStyleBackColor = False
        '
        'AgencyInvoiceReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1440, 670)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "AgencyInvoiceReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Agency Invoice Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gsrno As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPO As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gdate As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GBUYERNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELLERNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTRANSPORT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GQUALITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFOLD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GVPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDRCR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOMNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDISC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSPDISCAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHARGES As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALTAXABLEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDISCPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDISCOUNT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCDPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCASHDISC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSHIPPEDTO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCREATEDBY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREFERREDBY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTRADINGACC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMONTHNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKEDIT As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CMDOK As Button
    Friend WithEvents cmdcancel As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents TOOLMAIL As ToolStripButton
    Friend WithEvents TOOLWHATSAPP As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents lbl As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dtto As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents dtfrom As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents chkdate As CheckBox
    Friend WithEvents cmdshow As Button
End Class
