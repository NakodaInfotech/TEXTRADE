<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GSTR1GridReport
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
        Me.BlendPanel2 = New VbPowerPack.BlendPanel()
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.PBEXCEL = New System.Windows.Forms.PictureBox()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.TABGSTR1 = New System.Windows.Forms.TabControl()
        Me.TBB2B = New System.Windows.Forms.TabPage()
        Me.GRIDB2BDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDB2B = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GGSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPRINTINITIALS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPARTYBILLDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPLACEOFSUPPLY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREVERSECHARGE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAPPLICABLEPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVOICETYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GECOMMGSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTAXABLEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCESSAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GIGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TBB2CL = New System.Windows.Forms.TabPage()
        Me.GRIDB2CDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDB2C = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.G2INVNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2DATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2GRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2PLACEOFSUPPLY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2APPLICABLEPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2RATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2TAXABLEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2CESSAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2ECOMMGSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2CGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2SGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2IGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TBB2CS = New System.Windows.Forms.TabPage()
        Me.GRIDB2CSDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDB2CS = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.G3TYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G3PLACEOFSUPPLY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G3APPLICABLEPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G3RATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G3TAXABLEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G3CESSAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G3ECOMMGSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G3CGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G3SGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G3IGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TBCDNR = New System.Windows.Forms.TabPage()
        Me.GRIDCDNRDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDCDNR = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.G4GSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4NAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4INVNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4INVDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4CDNRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4CDNRDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4DOCTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4PLACEOFSUPPLY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4GRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4APPLICABLEPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4RATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4TAXABLEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4CESSAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4PREGST = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4CGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4SGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G4IGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TBCDNRUR = New System.Windows.Forms.TabPage()
        Me.GRIDCDNRURDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDCDNRUR = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.G5URTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5REFVOUCHERNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5REFVOUCHERDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5DOCTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5INVNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5INVDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5PLACEOFSUPPLY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5GRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5APPLICABLEPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5RATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5TAXABLEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5CESSAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5PREGST = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5CGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5SGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G5IGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TBHSN = New System.Windows.Forms.TabPage()
        Me.GRIDHSNDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDHSN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.G6HSN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6HSNDESC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6UQC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6TOTALQTY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6GRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6RATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6TAXABLEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6IGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6CGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6SGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G6CESSAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TBDOCS = New System.Windows.Forms.TabPage()
        Me.GRIDDOCSDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDDOCS = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.G7DOCUMENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G7FROM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G7TO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G7TOTALNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G7TOTALCANCELLED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TBHSNB2C = New System.Windows.Forms.TabPage()
        Me.GRIDHSNB2CDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDHSNB2C = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BlendPanel2.SuspendLayout()
        CType(Me.PBEXCEL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TABGSTR1.SuspendLayout()
        Me.TBB2B.SuspendLayout()
        CType(Me.GRIDB2BDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDB2B, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBB2CL.SuspendLayout()
        CType(Me.GRIDB2CDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDB2C, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBB2CS.SuspendLayout()
        CType(Me.GRIDB2CSDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDB2CS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBCDNR.SuspendLayout()
        CType(Me.GRIDCDNRDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDCDNR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBCDNRUR.SuspendLayout()
        CType(Me.GRIDCDNRURDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDCDNRUR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBHSN.SuspendLayout()
        CType(Me.GRIDHSNDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDHSN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBDOCS.SuspendLayout()
        CType(Me.GRIDDOCSDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDDOCS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBHSNB2C.SuspendLayout()
        CType(Me.GRIDHSNB2CDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDHSNB2C, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel2
        '
        Me.BlendPanel2.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel2.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel2.Controls.Add(Me.PBEXCEL)
        Me.BlendPanel2.Controls.Add(Me.cmdexit)
        Me.BlendPanel2.Controls.Add(Me.TABGSTR1)
        Me.BlendPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel2.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel2.Name = "BlendPanel2"
        Me.BlendPanel2.Size = New System.Drawing.Size(1284, 581)
        Me.BlendPanel2.TabIndex = 0
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDREFRESH.Location = New System.Drawing.Point(551, 550)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(88, 28)
        Me.CMDREFRESH.TabIndex = 0
        Me.CMDREFRESH.Text = "&Refresh"
        Me.CMDREFRESH.UseVisualStyleBackColor = False
        '
        'PBEXCEL
        '
        Me.PBEXCEL.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        Me.PBEXCEL.Location = New System.Drawing.Point(1177, 12)
        Me.PBEXCEL.Name = "PBEXCEL"
        Me.PBEXCEL.Size = New System.Drawing.Size(25, 25)
        Me.PBEXCEL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBEXCEL.TabIndex = 690
        Me.PBEXCEL.TabStop = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdexit.Location = New System.Drawing.Point(645, 550)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(88, 28)
        Me.cmdexit.TabIndex = 1
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'TABGSTR1
        '
        Me.TABGSTR1.Controls.Add(Me.TBB2B)
        Me.TABGSTR1.Controls.Add(Me.TBB2CL)
        Me.TABGSTR1.Controls.Add(Me.TBB2CS)
        Me.TABGSTR1.Controls.Add(Me.TBCDNR)
        Me.TABGSTR1.Controls.Add(Me.TBCDNRUR)
        Me.TABGSTR1.Controls.Add(Me.TBHSN)
        Me.TABGSTR1.Controls.Add(Me.TBHSNB2C)
        Me.TABGSTR1.Controls.Add(Me.TBDOCS)
        Me.TABGSTR1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TABGSTR1.Location = New System.Drawing.Point(14, 18)
        Me.TABGSTR1.Name = "TABGSTR1"
        Me.TABGSTR1.SelectedIndex = 0
        Me.TABGSTR1.Size = New System.Drawing.Size(1258, 526)
        Me.TABGSTR1.TabIndex = 18
        '
        'TBB2B
        '
        Me.TBB2B.AutoScroll = True
        Me.TBB2B.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TBB2B.Controls.Add(Me.GRIDB2BDETAILS)
        Me.TBB2B.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBB2B.Location = New System.Drawing.Point(4, 23)
        Me.TBB2B.Name = "TBB2B"
        Me.TBB2B.Padding = New System.Windows.Forms.Padding(3)
        Me.TBB2B.Size = New System.Drawing.Size(1250, 499)
        Me.TBB2B.TabIndex = 0
        Me.TBB2B.Text = "1. B2B"
        '
        'GRIDB2BDETAILS
        '
        Me.GRIDB2BDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDB2BDETAILS.Location = New System.Drawing.Point(3, 1)
        Me.GRIDB2BDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDB2BDETAILS.MainView = Me.GRIDB2B
        Me.GRIDB2BDETAILS.Name = "GRIDB2BDETAILS"
        Me.GRIDB2BDETAILS.Size = New System.Drawing.Size(1244, 497)
        Me.GRIDB2BDETAILS.TabIndex = 7
        Me.GRIDB2BDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDB2B})
        '
        'GRIDB2B
        '
        Me.GRIDB2B.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDB2B.Appearance.Row.Options.UseFont = True
        Me.GRIDB2B.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GGSTIN, Me.GNAME, Me.GPRINTINITIALS, Me.GPARTYBILLDATE, Me.GGRANDTOTAL, Me.GPLACEOFSUPPLY, Me.GREVERSECHARGE, Me.GAPPLICABLEPER, Me.GINVOICETYPE, Me.GECOMMGSTIN, Me.GRATE, Me.GTAXABLEAMT, Me.GCESSAMT, Me.GCGSTAMT, Me.GSGSTAMT, Me.GIGSTAMT})
        Me.GRIDB2B.GridControl = Me.GRIDB2BDETAILS
        Me.GRIDB2B.Name = "GRIDB2B"
        Me.GRIDB2B.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDB2B.OptionsBehavior.Editable = False
        Me.GRIDB2B.OptionsView.ColumnAutoWidth = False
        Me.GRIDB2B.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDB2B.OptionsView.ShowAutoFilterRow = True
        Me.GRIDB2B.OptionsView.ShowFooter = True
        Me.GRIDB2B.OptionsView.ShowGroupPanel = False
        '
        'GGSTIN
        '
        Me.GGSTIN.Caption = "GSTIN"
        Me.GGSTIN.FieldName = "GSTIN"
        Me.GGSTIN.Name = "GGSTIN"
        Me.GGSTIN.Visible = True
        Me.GGSTIN.VisibleIndex = 0
        Me.GGSTIN.Width = 120
        '
        'GNAME
        '
        Me.GNAME.Caption = "Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.ImageIndex = 0
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 1
        Me.GNAME.Width = 300
        '
        'GPRINTINITIALS
        '
        Me.GPRINTINITIALS.Caption = "Inv No"
        Me.GPRINTINITIALS.FieldName = "PRINTINITIALS"
        Me.GPRINTINITIALS.Name = "GPRINTINITIALS"
        Me.GPRINTINITIALS.Visible = True
        Me.GPRINTINITIALS.VisibleIndex = 2
        Me.GPRINTINITIALS.Width = 120
        '
        'GPARTYBILLDATE
        '
        Me.GPARTYBILLDATE.Caption = "Date"
        Me.GPARTYBILLDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GPARTYBILLDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GPARTYBILLDATE.FieldName = "DATE"
        Me.GPARTYBILLDATE.Name = "GPARTYBILLDATE"
        Me.GPARTYBILLDATE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GPARTYBILLDATE.Visible = True
        Me.GPARTYBILLDATE.VisibleIndex = 3
        Me.GPARTYBILLDATE.Width = 80
        '
        'GGRANDTOTAL
        '
        Me.GGRANDTOTAL.Caption = "Invoice Value"
        Me.GGRANDTOTAL.DisplayFormat.FormatString = "0.00"
        Me.GGRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.GGRANDTOTAL.Name = "GGRANDTOTAL"
        Me.GGRANDTOTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GGRANDTOTAL.Visible = True
        Me.GGRANDTOTAL.VisibleIndex = 4
        Me.GGRANDTOTAL.Width = 100
        '
        'GPLACEOFSUPPLY
        '
        Me.GPLACEOFSUPPLY.Caption = "Place Of Supply"
        Me.GPLACEOFSUPPLY.FieldName = "PLACEOFSUPPLY"
        Me.GPLACEOFSUPPLY.Name = "GPLACEOFSUPPLY"
        Me.GPLACEOFSUPPLY.Visible = True
        Me.GPLACEOFSUPPLY.VisibleIndex = 5
        Me.GPLACEOFSUPPLY.Width = 130
        '
        'GREVERSECHARGE
        '
        Me.GREVERSECHARGE.Caption = "Reverse Charge"
        Me.GREVERSECHARGE.FieldName = "REVERSECHARGE"
        Me.GREVERSECHARGE.Name = "GREVERSECHARGE"
        Me.GREVERSECHARGE.Visible = True
        Me.GREVERSECHARGE.VisibleIndex = 6
        '
        'GAPPLICABLEPER
        '
        Me.GAPPLICABLEPER.Caption = "Applicable %"
        Me.GAPPLICABLEPER.FieldName = "APPLICABLEPER"
        Me.GAPPLICABLEPER.Name = "GAPPLICABLEPER"
        Me.GAPPLICABLEPER.Visible = True
        Me.GAPPLICABLEPER.VisibleIndex = 7
        '
        'GINVOICETYPE
        '
        Me.GINVOICETYPE.Caption = "Invoice Type"
        Me.GINVOICETYPE.FieldName = "INVOICETYPE"
        Me.GINVOICETYPE.Name = "GINVOICETYPE"
        Me.GINVOICETYPE.Visible = True
        Me.GINVOICETYPE.VisibleIndex = 8
        '
        'GECOMMGSTIN
        '
        Me.GECOMMGSTIN.Caption = "E-Comm GSTIN"
        Me.GECOMMGSTIN.FieldName = "ECOMMGSTIN"
        Me.GECOMMGSTIN.Name = "GECOMMGSTIN"
        Me.GECOMMGSTIN.Visible = True
        Me.GECOMMGSTIN.VisibleIndex = 9
        '
        'GRATE
        '
        Me.GRATE.Caption = "Rate"
        Me.GRATE.DisplayFormat.FormatString = "0.00"
        Me.GRATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRATE.FieldName = "RATE"
        Me.GRATE.Name = "GRATE"
        Me.GRATE.Visible = True
        Me.GRATE.VisibleIndex = 10
        '
        'GTAXABLEAMT
        '
        Me.GTAXABLEAMT.Caption = "Taxable Amt."
        Me.GTAXABLEAMT.DisplayFormat.FormatString = "0.00"
        Me.GTAXABLEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTAXABLEAMT.FieldName = "TAXABLEAMT"
        Me.GTAXABLEAMT.Name = "GTAXABLEAMT"
        Me.GTAXABLEAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTAXABLEAMT.Visible = True
        Me.GTAXABLEAMT.VisibleIndex = 11
        Me.GTAXABLEAMT.Width = 120
        '
        'GCESSAMT
        '
        Me.GCESSAMT.Caption = "Cess Amt"
        Me.GCESSAMT.DisplayFormat.FormatString = "0"
        Me.GCESSAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCESSAMT.FieldName = "CESSAMT"
        Me.GCESSAMT.Name = "GCESSAMT"
        Me.GCESSAMT.Visible = True
        Me.GCESSAMT.VisibleIndex = 12
        '
        'GCGSTAMT
        '
        Me.GCGSTAMT.Caption = "CGST Amt."
        Me.GCGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.GCGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCGSTAMT.FieldName = "CGSTAMT"
        Me.GCGSTAMT.Name = "GCGSTAMT"
        Me.GCGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GCGSTAMT.Visible = True
        Me.GCGSTAMT.VisibleIndex = 13
        Me.GCGSTAMT.Width = 100
        '
        'GSGSTAMT
        '
        Me.GSGSTAMT.Caption = "SGST Amt."
        Me.GSGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.GSGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GSGSTAMT.FieldName = "SGSTAMT"
        Me.GSGSTAMT.Name = "GSGSTAMT"
        Me.GSGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GSGSTAMT.Visible = True
        Me.GSGSTAMT.VisibleIndex = 14
        Me.GSGSTAMT.Width = 100
        '
        'GIGSTAMT
        '
        Me.GIGSTAMT.Caption = "IGST Amt."
        Me.GIGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.GIGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GIGSTAMT.FieldName = "IGSTAMT"
        Me.GIGSTAMT.Name = "GIGSTAMT"
        Me.GIGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GIGSTAMT.Visible = True
        Me.GIGSTAMT.VisibleIndex = 15
        Me.GIGSTAMT.Width = 100
        '
        'TBB2CL
        '
        Me.TBB2CL.AutoScroll = True
        Me.TBB2CL.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.TBB2CL.Controls.Add(Me.GRIDB2CDETAILS)
        Me.TBB2CL.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBB2CL.Location = New System.Drawing.Point(4, 23)
        Me.TBB2CL.Name = "TBB2CL"
        Me.TBB2CL.Padding = New System.Windows.Forms.Padding(3)
        Me.TBB2CL.Size = New System.Drawing.Size(1250, 499)
        Me.TBB2CL.TabIndex = 1
        Me.TBB2CL.Text = "2. B2CL"
        '
        'GRIDB2CDETAILS
        '
        Me.GRIDB2CDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDB2CDETAILS.Location = New System.Drawing.Point(3, 1)
        Me.GRIDB2CDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDB2CDETAILS.MainView = Me.GRIDB2C
        Me.GRIDB2CDETAILS.Name = "GRIDB2CDETAILS"
        Me.GRIDB2CDETAILS.Size = New System.Drawing.Size(1244, 497)
        Me.GRIDB2CDETAILS.TabIndex = 8
        Me.GRIDB2CDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDB2C})
        '
        'GRIDB2C
        '
        Me.GRIDB2C.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDB2C.Appearance.Row.Options.UseFont = True
        Me.GRIDB2C.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.G2INVNO, Me.G2DATE, Me.G2GRANDTOTAL, Me.G2PLACEOFSUPPLY, Me.G2APPLICABLEPER, Me.G2RATE, Me.G2TAXABLEAMT, Me.G2CESSAMT, Me.G2ECOMMGSTIN, Me.G2CGSTAMT, Me.G2SGSTAMT, Me.G2IGSTAMT})
        Me.GRIDB2C.GridControl = Me.GRIDB2CDETAILS
        Me.GRIDB2C.Name = "GRIDB2C"
        Me.GRIDB2C.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDB2C.OptionsBehavior.Editable = False
        Me.GRIDB2C.OptionsView.ColumnAutoWidth = False
        Me.GRIDB2C.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDB2C.OptionsView.ShowAutoFilterRow = True
        Me.GRIDB2C.OptionsView.ShowFooter = True
        Me.GRIDB2C.OptionsView.ShowGroupPanel = False
        '
        'G2INVNO
        '
        Me.G2INVNO.Caption = "Inv No"
        Me.G2INVNO.FieldName = "PRINTINITIALS"
        Me.G2INVNO.Name = "G2INVNO"
        Me.G2INVNO.Visible = True
        Me.G2INVNO.VisibleIndex = 0
        Me.G2INVNO.Width = 150
        '
        'G2DATE
        '
        Me.G2DATE.Caption = "Date"
        Me.G2DATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.G2DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.G2DATE.FieldName = "DATE"
        Me.G2DATE.Name = "G2DATE"
        Me.G2DATE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.G2DATE.Visible = True
        Me.G2DATE.VisibleIndex = 1
        Me.G2DATE.Width = 80
        '
        'G2GRANDTOTAL
        '
        Me.G2GRANDTOTAL.Caption = "Invoice Value"
        Me.G2GRANDTOTAL.DisplayFormat.FormatString = "0.00"
        Me.G2GRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G2GRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.G2GRANDTOTAL.Name = "G2GRANDTOTAL"
        Me.G2GRANDTOTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G2GRANDTOTAL.Visible = True
        Me.G2GRANDTOTAL.VisibleIndex = 2
        Me.G2GRANDTOTAL.Width = 100
        '
        'G2PLACEOFSUPPLY
        '
        Me.G2PLACEOFSUPPLY.Caption = "Place Of Supply"
        Me.G2PLACEOFSUPPLY.FieldName = "PLACEOFSUPPLY"
        Me.G2PLACEOFSUPPLY.Name = "G2PLACEOFSUPPLY"
        Me.G2PLACEOFSUPPLY.Visible = True
        Me.G2PLACEOFSUPPLY.VisibleIndex = 3
        Me.G2PLACEOFSUPPLY.Width = 140
        '
        'G2APPLICABLEPER
        '
        Me.G2APPLICABLEPER.Caption = "Applicable %"
        Me.G2APPLICABLEPER.FieldName = "APPLICABLEPER"
        Me.G2APPLICABLEPER.Name = "G2APPLICABLEPER"
        Me.G2APPLICABLEPER.Visible = True
        Me.G2APPLICABLEPER.VisibleIndex = 4
        '
        'G2RATE
        '
        Me.G2RATE.Caption = "Rate"
        Me.G2RATE.DisplayFormat.FormatString = "0.00"
        Me.G2RATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G2RATE.FieldName = "RATE"
        Me.G2RATE.Name = "G2RATE"
        Me.G2RATE.Visible = True
        Me.G2RATE.VisibleIndex = 5
        '
        'G2TAXABLEAMT
        '
        Me.G2TAXABLEAMT.Caption = "Taxable Amt."
        Me.G2TAXABLEAMT.DisplayFormat.FormatString = "0.00"
        Me.G2TAXABLEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G2TAXABLEAMT.FieldName = "TAXABLEAMT"
        Me.G2TAXABLEAMT.Name = "G2TAXABLEAMT"
        Me.G2TAXABLEAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G2TAXABLEAMT.Visible = True
        Me.G2TAXABLEAMT.VisibleIndex = 6
        Me.G2TAXABLEAMT.Width = 120
        '
        'G2CESSAMT
        '
        Me.G2CESSAMT.Caption = "Cess Amt"
        Me.G2CESSAMT.DisplayFormat.FormatString = "0"
        Me.G2CESSAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G2CESSAMT.FieldName = "CESSAMT"
        Me.G2CESSAMT.Name = "G2CESSAMT"
        Me.G2CESSAMT.Visible = True
        Me.G2CESSAMT.VisibleIndex = 7
        '
        'G2ECOMMGSTIN
        '
        Me.G2ECOMMGSTIN.Caption = "E-Comm GSTIN"
        Me.G2ECOMMGSTIN.FieldName = "ECOMMGSTIN"
        Me.G2ECOMMGSTIN.Name = "G2ECOMMGSTIN"
        Me.G2ECOMMGSTIN.Visible = True
        Me.G2ECOMMGSTIN.VisibleIndex = 8
        '
        'G2CGSTAMT
        '
        Me.G2CGSTAMT.Caption = "CGST Amt."
        Me.G2CGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G2CGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G2CGSTAMT.FieldName = "CGSTAMT"
        Me.G2CGSTAMT.Name = "G2CGSTAMT"
        Me.G2CGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G2CGSTAMT.Visible = True
        Me.G2CGSTAMT.VisibleIndex = 9
        Me.G2CGSTAMT.Width = 100
        '
        'G2SGSTAMT
        '
        Me.G2SGSTAMT.Caption = "SGST Amt."
        Me.G2SGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G2SGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G2SGSTAMT.FieldName = "SGSTAMT"
        Me.G2SGSTAMT.Name = "G2SGSTAMT"
        Me.G2SGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G2SGSTAMT.Visible = True
        Me.G2SGSTAMT.VisibleIndex = 10
        Me.G2SGSTAMT.Width = 100
        '
        'G2IGSTAMT
        '
        Me.G2IGSTAMT.Caption = "IGST Amt."
        Me.G2IGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G2IGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G2IGSTAMT.FieldName = "IGSTAMT"
        Me.G2IGSTAMT.Name = "G2IGSTAMT"
        Me.G2IGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G2IGSTAMT.Visible = True
        Me.G2IGSTAMT.VisibleIndex = 11
        Me.G2IGSTAMT.Width = 100
        '
        'TBB2CS
        '
        Me.TBB2CS.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TBB2CS.Controls.Add(Me.GRIDB2CSDETAILS)
        Me.TBB2CS.Location = New System.Drawing.Point(4, 23)
        Me.TBB2CS.Name = "TBB2CS"
        Me.TBB2CS.Size = New System.Drawing.Size(1250, 499)
        Me.TBB2CS.TabIndex = 2
        Me.TBB2CS.Text = "3. B2CS"
        '
        'GRIDB2CSDETAILS
        '
        Me.GRIDB2CSDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDB2CSDETAILS.Location = New System.Drawing.Point(3, 1)
        Me.GRIDB2CSDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDB2CSDETAILS.MainView = Me.GRIDB2CS
        Me.GRIDB2CSDETAILS.Name = "GRIDB2CSDETAILS"
        Me.GRIDB2CSDETAILS.Size = New System.Drawing.Size(1244, 495)
        Me.GRIDB2CSDETAILS.TabIndex = 9
        Me.GRIDB2CSDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDB2CS})
        '
        'GRIDB2CS
        '
        Me.GRIDB2CS.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDB2CS.Appearance.Row.Options.UseFont = True
        Me.GRIDB2CS.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.G3TYPE, Me.G3PLACEOFSUPPLY, Me.G3APPLICABLEPER, Me.G3RATE, Me.G3TAXABLEAMT, Me.G3CESSAMT, Me.G3ECOMMGSTIN, Me.G3CGSTAMT, Me.G3SGSTAMT, Me.G3IGSTAMT})
        Me.GRIDB2CS.GridControl = Me.GRIDB2CSDETAILS
        Me.GRIDB2CS.Name = "GRIDB2CS"
        Me.GRIDB2CS.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDB2CS.OptionsBehavior.Editable = False
        Me.GRIDB2CS.OptionsView.ColumnAutoWidth = False
        Me.GRIDB2CS.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDB2CS.OptionsView.ShowAutoFilterRow = True
        Me.GRIDB2CS.OptionsView.ShowFooter = True
        Me.GRIDB2CS.OptionsView.ShowGroupPanel = False
        '
        'G3TYPE
        '
        Me.G3TYPE.Caption = "Type"
        Me.G3TYPE.FieldName = "TYPE"
        Me.G3TYPE.Name = "G3TYPE"
        Me.G3TYPE.Visible = True
        Me.G3TYPE.VisibleIndex = 0
        Me.G3TYPE.Width = 150
        '
        'G3PLACEOFSUPPLY
        '
        Me.G3PLACEOFSUPPLY.Caption = "Place Of Supply"
        Me.G3PLACEOFSUPPLY.FieldName = "PLACEOFSUPPLY"
        Me.G3PLACEOFSUPPLY.Name = "G3PLACEOFSUPPLY"
        Me.G3PLACEOFSUPPLY.Visible = True
        Me.G3PLACEOFSUPPLY.VisibleIndex = 1
        Me.G3PLACEOFSUPPLY.Width = 140
        '
        'G3APPLICABLEPER
        '
        Me.G3APPLICABLEPER.Caption = "Applicable %"
        Me.G3APPLICABLEPER.FieldName = "APPLICABLEPER"
        Me.G3APPLICABLEPER.Name = "G3APPLICABLEPER"
        Me.G3APPLICABLEPER.Visible = True
        Me.G3APPLICABLEPER.VisibleIndex = 2
        '
        'G3RATE
        '
        Me.G3RATE.Caption = "Rate"
        Me.G3RATE.DisplayFormat.FormatString = "0.00"
        Me.G3RATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G3RATE.FieldName = "RATE"
        Me.G3RATE.Name = "G3RATE"
        Me.G3RATE.Visible = True
        Me.G3RATE.VisibleIndex = 3
        '
        'G3TAXABLEAMT
        '
        Me.G3TAXABLEAMT.Caption = "Taxable Amt."
        Me.G3TAXABLEAMT.DisplayFormat.FormatString = "0.00"
        Me.G3TAXABLEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G3TAXABLEAMT.FieldName = "TAXABLEAMT"
        Me.G3TAXABLEAMT.Name = "G3TAXABLEAMT"
        Me.G3TAXABLEAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G3TAXABLEAMT.Visible = True
        Me.G3TAXABLEAMT.VisibleIndex = 4
        Me.G3TAXABLEAMT.Width = 120
        '
        'G3CESSAMT
        '
        Me.G3CESSAMT.Caption = "Cess Amt"
        Me.G3CESSAMT.DisplayFormat.FormatString = "0"
        Me.G3CESSAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G3CESSAMT.FieldName = "CESSAMT"
        Me.G3CESSAMT.Name = "G3CESSAMT"
        Me.G3CESSAMT.Visible = True
        Me.G3CESSAMT.VisibleIndex = 5
        '
        'G3ECOMMGSTIN
        '
        Me.G3ECOMMGSTIN.Caption = "E-Comm GSTIN"
        Me.G3ECOMMGSTIN.FieldName = "ECOMMGSTIN"
        Me.G3ECOMMGSTIN.Name = "G3ECOMMGSTIN"
        Me.G3ECOMMGSTIN.Visible = True
        Me.G3ECOMMGSTIN.VisibleIndex = 6
        '
        'G3CGSTAMT
        '
        Me.G3CGSTAMT.Caption = "CGST Amt."
        Me.G3CGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G3CGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G3CGSTAMT.FieldName = "CGSTAMT"
        Me.G3CGSTAMT.Name = "G3CGSTAMT"
        Me.G3CGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G3CGSTAMT.Visible = True
        Me.G3CGSTAMT.VisibleIndex = 7
        Me.G3CGSTAMT.Width = 100
        '
        'G3SGSTAMT
        '
        Me.G3SGSTAMT.Caption = "SGST Amt."
        Me.G3SGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G3SGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G3SGSTAMT.FieldName = "SGSTAMT"
        Me.G3SGSTAMT.Name = "G3SGSTAMT"
        Me.G3SGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G3SGSTAMT.Visible = True
        Me.G3SGSTAMT.VisibleIndex = 8
        Me.G3SGSTAMT.Width = 100
        '
        'G3IGSTAMT
        '
        Me.G3IGSTAMT.Caption = "IGST Amt."
        Me.G3IGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G3IGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G3IGSTAMT.FieldName = "IGSTAMT"
        Me.G3IGSTAMT.Name = "G3IGSTAMT"
        Me.G3IGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G3IGSTAMT.Visible = True
        Me.G3IGSTAMT.VisibleIndex = 9
        Me.G3IGSTAMT.Width = 100
        '
        'TBCDNR
        '
        Me.TBCDNR.AutoScroll = True
        Me.TBCDNR.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TBCDNR.Controls.Add(Me.GRIDCDNRDETAILS)
        Me.TBCDNR.Location = New System.Drawing.Point(4, 23)
        Me.TBCDNR.Name = "TBCDNR"
        Me.TBCDNR.Padding = New System.Windows.Forms.Padding(3)
        Me.TBCDNR.Size = New System.Drawing.Size(1250, 499)
        Me.TBCDNR.TabIndex = 3
        Me.TBCDNR.Text = "4. CDNR"
        '
        'GRIDCDNRDETAILS
        '
        Me.GRIDCDNRDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDCDNRDETAILS.Location = New System.Drawing.Point(3, 1)
        Me.GRIDCDNRDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDCDNRDETAILS.MainView = Me.GRIDCDNR
        Me.GRIDCDNRDETAILS.Name = "GRIDCDNRDETAILS"
        Me.GRIDCDNRDETAILS.Size = New System.Drawing.Size(1245, 496)
        Me.GRIDCDNRDETAILS.TabIndex = 8
        Me.GRIDCDNRDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDCDNR})
        '
        'GRIDCDNR
        '
        Me.GRIDCDNR.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDCDNR.Appearance.Row.Options.UseFont = True
        Me.GRIDCDNR.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.G4GSTIN, Me.G4NAME, Me.G4INVNO, Me.G4INVDATE, Me.G4CDNRNO, Me.G4CDNRDATE, Me.G4DOCTYPE, Me.G4PLACEOFSUPPLY, Me.G4GRANDTOTAL, Me.G4APPLICABLEPER, Me.G4RATE, Me.G4TAXABLEAMT, Me.G4CESSAMT, Me.G4PREGST, Me.G4CGSTAMT, Me.G4SGSTAMT, Me.G4IGSTAMT})
        Me.GRIDCDNR.GridControl = Me.GRIDCDNRDETAILS
        Me.GRIDCDNR.Name = "GRIDCDNR"
        Me.GRIDCDNR.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDCDNR.OptionsBehavior.Editable = False
        Me.GRIDCDNR.OptionsView.ColumnAutoWidth = False
        Me.GRIDCDNR.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDCDNR.OptionsView.ShowAutoFilterRow = True
        Me.GRIDCDNR.OptionsView.ShowFooter = True
        Me.GRIDCDNR.OptionsView.ShowGroupPanel = False
        '
        'G4GSTIN
        '
        Me.G4GSTIN.Caption = "GSTIN"
        Me.G4GSTIN.FieldName = "GSTIN"
        Me.G4GSTIN.Name = "G4GSTIN"
        Me.G4GSTIN.Visible = True
        Me.G4GSTIN.VisibleIndex = 0
        Me.G4GSTIN.Width = 120
        '
        'G4NAME
        '
        Me.G4NAME.Caption = "Name"
        Me.G4NAME.FieldName = "NAME"
        Me.G4NAME.ImageIndex = 0
        Me.G4NAME.Name = "G4NAME"
        Me.G4NAME.Visible = True
        Me.G4NAME.VisibleIndex = 1
        Me.G4NAME.Width = 300
        '
        'G4INVNO
        '
        Me.G4INVNO.Caption = "Inv No"
        Me.G4INVNO.FieldName = "PRINTINITIALS"
        Me.G4INVNO.Name = "G4INVNO"
        Me.G4INVNO.Visible = True
        Me.G4INVNO.VisibleIndex = 2
        Me.G4INVNO.Width = 120
        '
        'G4INVDATE
        '
        Me.G4INVDATE.Caption = "Inv Date"
        Me.G4INVDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.G4INVDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.G4INVDATE.FieldName = "INVDATE"
        Me.G4INVDATE.Name = "G4INVDATE"
        Me.G4INVDATE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.G4INVDATE.Visible = True
        Me.G4INVDATE.VisibleIndex = 3
        Me.G4INVDATE.Width = 80
        '
        'G4CDNRNO
        '
        Me.G4CDNRNO.Caption = "Ref Voucher No"
        Me.G4CDNRNO.FieldName = "CNINITIALS"
        Me.G4CDNRNO.Name = "G4CDNRNO"
        Me.G4CDNRNO.Visible = True
        Me.G4CDNRNO.VisibleIndex = 4
        Me.G4CDNRNO.Width = 120
        '
        'G4CDNRDATE
        '
        Me.G4CDNRDATE.Caption = "Ref Voucher Dt."
        Me.G4CDNRDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.G4CDNRDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.G4CDNRDATE.FieldName = "CNDATE"
        Me.G4CDNRDATE.Name = "G4CDNRDATE"
        Me.G4CDNRDATE.Visible = True
        Me.G4CDNRDATE.VisibleIndex = 5
        Me.G4CDNRDATE.Width = 80
        '
        'G4DOCTYPE
        '
        Me.G4DOCTYPE.Caption = "Doc Type"
        Me.G4DOCTYPE.FieldName = "DOCTYPE"
        Me.G4DOCTYPE.Name = "G4DOCTYPE"
        Me.G4DOCTYPE.Visible = True
        Me.G4DOCTYPE.VisibleIndex = 6
        '
        'G4PLACEOFSUPPLY
        '
        Me.G4PLACEOFSUPPLY.Caption = "Place Of Supply"
        Me.G4PLACEOFSUPPLY.FieldName = "PLACEOFSUPPLY"
        Me.G4PLACEOFSUPPLY.Name = "G4PLACEOFSUPPLY"
        Me.G4PLACEOFSUPPLY.Visible = True
        Me.G4PLACEOFSUPPLY.VisibleIndex = 7
        Me.G4PLACEOFSUPPLY.Width = 130
        '
        'G4GRANDTOTAL
        '
        Me.G4GRANDTOTAL.Caption = "Grand Total"
        Me.G4GRANDTOTAL.DisplayFormat.FormatString = "0.00"
        Me.G4GRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G4GRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.G4GRANDTOTAL.Name = "G4GRANDTOTAL"
        Me.G4GRANDTOTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G4GRANDTOTAL.Visible = True
        Me.G4GRANDTOTAL.VisibleIndex = 8
        Me.G4GRANDTOTAL.Width = 120
        '
        'G4APPLICABLEPER
        '
        Me.G4APPLICABLEPER.Caption = "Applicable %"
        Me.G4APPLICABLEPER.FieldName = "APPLICABLEPER"
        Me.G4APPLICABLEPER.Name = "G4APPLICABLEPER"
        Me.G4APPLICABLEPER.Visible = True
        Me.G4APPLICABLEPER.VisibleIndex = 9
        '
        'G4RATE
        '
        Me.G4RATE.Caption = "Rate"
        Me.G4RATE.DisplayFormat.FormatString = "0.00"
        Me.G4RATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G4RATE.FieldName = "RATE"
        Me.G4RATE.Name = "G4RATE"
        Me.G4RATE.Visible = True
        Me.G4RATE.VisibleIndex = 10
        '
        'G4TAXABLEAMT
        '
        Me.G4TAXABLEAMT.Caption = "Taxable Amt."
        Me.G4TAXABLEAMT.DisplayFormat.FormatString = "0.00"
        Me.G4TAXABLEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G4TAXABLEAMT.FieldName = "TAXABLEAMT"
        Me.G4TAXABLEAMT.Name = "G4TAXABLEAMT"
        Me.G4TAXABLEAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G4TAXABLEAMT.Visible = True
        Me.G4TAXABLEAMT.VisibleIndex = 11
        Me.G4TAXABLEAMT.Width = 120
        '
        'G4CESSAMT
        '
        Me.G4CESSAMT.Caption = "Cess Amt"
        Me.G4CESSAMT.DisplayFormat.FormatString = "0"
        Me.G4CESSAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G4CESSAMT.FieldName = "CESSAMT"
        Me.G4CESSAMT.Name = "G4CESSAMT"
        Me.G4CESSAMT.Visible = True
        Me.G4CESSAMT.VisibleIndex = 12
        '
        'G4PREGST
        '
        Me.G4PREGST.Caption = "Pre GST"
        Me.G4PREGST.FieldName = "PREGST"
        Me.G4PREGST.Name = "G4PREGST"
        Me.G4PREGST.Visible = True
        Me.G4PREGST.VisibleIndex = 13
        '
        'G4CGSTAMT
        '
        Me.G4CGSTAMT.Caption = "CGST Amt."
        Me.G4CGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G4CGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G4CGSTAMT.FieldName = "CGSTAMT"
        Me.G4CGSTAMT.Name = "G4CGSTAMT"
        Me.G4CGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G4CGSTAMT.Visible = True
        Me.G4CGSTAMT.VisibleIndex = 14
        Me.G4CGSTAMT.Width = 100
        '
        'G4SGSTAMT
        '
        Me.G4SGSTAMT.Caption = "SGST Amt."
        Me.G4SGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G4SGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G4SGSTAMT.FieldName = "SGSTAMT"
        Me.G4SGSTAMT.Name = "G4SGSTAMT"
        Me.G4SGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G4SGSTAMT.Visible = True
        Me.G4SGSTAMT.VisibleIndex = 15
        Me.G4SGSTAMT.Width = 100
        '
        'G4IGSTAMT
        '
        Me.G4IGSTAMT.Caption = "IGST Amt."
        Me.G4IGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G4IGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G4IGSTAMT.FieldName = "IGSTAMT"
        Me.G4IGSTAMT.Name = "G4IGSTAMT"
        Me.G4IGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G4IGSTAMT.Visible = True
        Me.G4IGSTAMT.VisibleIndex = 16
        Me.G4IGSTAMT.Width = 100
        '
        'TBCDNRUR
        '
        Me.TBCDNRUR.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TBCDNRUR.Controls.Add(Me.GRIDCDNRURDETAILS)
        Me.TBCDNRUR.Location = New System.Drawing.Point(4, 23)
        Me.TBCDNRUR.Name = "TBCDNRUR"
        Me.TBCDNRUR.Padding = New System.Windows.Forms.Padding(3)
        Me.TBCDNRUR.Size = New System.Drawing.Size(1250, 499)
        Me.TBCDNRUR.TabIndex = 4
        Me.TBCDNRUR.Text = "5. CDNRUR"
        '
        'GRIDCDNRURDETAILS
        '
        Me.GRIDCDNRURDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDCDNRURDETAILS.Location = New System.Drawing.Point(3, 1)
        Me.GRIDCDNRURDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDCDNRURDETAILS.MainView = Me.GRIDCDNRUR
        Me.GRIDCDNRURDETAILS.Name = "GRIDCDNRURDETAILS"
        Me.GRIDCDNRURDETAILS.Size = New System.Drawing.Size(1245, 496)
        Me.GRIDCDNRURDETAILS.TabIndex = 9
        Me.GRIDCDNRURDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDCDNRUR})
        '
        'GRIDCDNRUR
        '
        Me.GRIDCDNRUR.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDCDNRUR.Appearance.Row.Options.UseFont = True
        Me.GRIDCDNRUR.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.G5URTYPE, Me.G5REFVOUCHERNO, Me.G5REFVOUCHERDATE, Me.G5DOCTYPE, Me.G5INVNO, Me.G5INVDATE, Me.G5PLACEOFSUPPLY, Me.G5GRANDTOTAL, Me.G5APPLICABLEPER, Me.G5RATE, Me.G5TAXABLEAMT, Me.G5CESSAMT, Me.G5PREGST, Me.G5CGSTAMT, Me.G5SGSTAMT, Me.G5IGSTAMT})
        Me.GRIDCDNRUR.GridControl = Me.GRIDCDNRURDETAILS
        Me.GRIDCDNRUR.Name = "GRIDCDNRUR"
        Me.GRIDCDNRUR.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDCDNRUR.OptionsBehavior.Editable = False
        Me.GRIDCDNRUR.OptionsView.ColumnAutoWidth = False
        Me.GRIDCDNRUR.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDCDNRUR.OptionsView.ShowAutoFilterRow = True
        Me.GRIDCDNRUR.OptionsView.ShowFooter = True
        Me.GRIDCDNRUR.OptionsView.ShowGroupPanel = False
        '
        'G5URTYPE
        '
        Me.G5URTYPE.Caption = "UR Type"
        Me.G5URTYPE.FieldName = "URTYPE"
        Me.G5URTYPE.Name = "G5URTYPE"
        Me.G5URTYPE.Visible = True
        Me.G5URTYPE.VisibleIndex = 0
        Me.G5URTYPE.Width = 120
        '
        'G5REFVOUCHERNO
        '
        Me.G5REFVOUCHERNO.Caption = "Ref Voucher No"
        Me.G5REFVOUCHERNO.FieldName = "CNINITIALS"
        Me.G5REFVOUCHERNO.Name = "G5REFVOUCHERNO"
        Me.G5REFVOUCHERNO.Visible = True
        Me.G5REFVOUCHERNO.VisibleIndex = 1
        Me.G5REFVOUCHERNO.Width = 120
        '
        'G5REFVOUCHERDATE
        '
        Me.G5REFVOUCHERDATE.Caption = "Ref Voucher Dt."
        Me.G5REFVOUCHERDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.G5REFVOUCHERDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.G5REFVOUCHERDATE.FieldName = "CNDATE"
        Me.G5REFVOUCHERDATE.Name = "G5REFVOUCHERDATE"
        Me.G5REFVOUCHERDATE.Visible = True
        Me.G5REFVOUCHERDATE.VisibleIndex = 2
        Me.G5REFVOUCHERDATE.Width = 80
        '
        'G5DOCTYPE
        '
        Me.G5DOCTYPE.Caption = "Doc Type"
        Me.G5DOCTYPE.FieldName = "DOCTYPE"
        Me.G5DOCTYPE.Name = "G5DOCTYPE"
        Me.G5DOCTYPE.Visible = True
        Me.G5DOCTYPE.VisibleIndex = 3
        '
        'G5INVNO
        '
        Me.G5INVNO.Caption = "Inv No"
        Me.G5INVNO.FieldName = "PRINTINITIALS"
        Me.G5INVNO.Name = "G5INVNO"
        Me.G5INVNO.Visible = True
        Me.G5INVNO.VisibleIndex = 4
        Me.G5INVNO.Width = 120
        '
        'G5INVDATE
        '
        Me.G5INVDATE.Caption = "Inv Date"
        Me.G5INVDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.G5INVDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.G5INVDATE.FieldName = "INVDATE"
        Me.G5INVDATE.Name = "G5INVDATE"
        Me.G5INVDATE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.G5INVDATE.Visible = True
        Me.G5INVDATE.VisibleIndex = 5
        Me.G5INVDATE.Width = 80
        '
        'G5PLACEOFSUPPLY
        '
        Me.G5PLACEOFSUPPLY.Caption = "Place Of Supply"
        Me.G5PLACEOFSUPPLY.FieldName = "PLACEOFSUPPLY"
        Me.G5PLACEOFSUPPLY.Name = "G5PLACEOFSUPPLY"
        Me.G5PLACEOFSUPPLY.Visible = True
        Me.G5PLACEOFSUPPLY.VisibleIndex = 6
        Me.G5PLACEOFSUPPLY.Width = 130
        '
        'G5GRANDTOTAL
        '
        Me.G5GRANDTOTAL.Caption = "Grand Total"
        Me.G5GRANDTOTAL.DisplayFormat.FormatString = "0.00"
        Me.G5GRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G5GRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.G5GRANDTOTAL.Name = "G5GRANDTOTAL"
        Me.G5GRANDTOTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G5GRANDTOTAL.Visible = True
        Me.G5GRANDTOTAL.VisibleIndex = 7
        Me.G5GRANDTOTAL.Width = 120
        '
        'G5APPLICABLEPER
        '
        Me.G5APPLICABLEPER.Caption = "Applicable %"
        Me.G5APPLICABLEPER.FieldName = "APPLICABLEPER"
        Me.G5APPLICABLEPER.Name = "G5APPLICABLEPER"
        Me.G5APPLICABLEPER.Visible = True
        Me.G5APPLICABLEPER.VisibleIndex = 8
        '
        'G5RATE
        '
        Me.G5RATE.Caption = "Rate"
        Me.G5RATE.DisplayFormat.FormatString = "0.00"
        Me.G5RATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G5RATE.FieldName = "RATE"
        Me.G5RATE.Name = "G5RATE"
        Me.G5RATE.Visible = True
        Me.G5RATE.VisibleIndex = 9
        '
        'G5TAXABLEAMT
        '
        Me.G5TAXABLEAMT.Caption = "Taxable Amt."
        Me.G5TAXABLEAMT.DisplayFormat.FormatString = "0.00"
        Me.G5TAXABLEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G5TAXABLEAMT.FieldName = "TAXABLEAMT"
        Me.G5TAXABLEAMT.Name = "G5TAXABLEAMT"
        Me.G5TAXABLEAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G5TAXABLEAMT.Visible = True
        Me.G5TAXABLEAMT.VisibleIndex = 10
        Me.G5TAXABLEAMT.Width = 120
        '
        'G5CESSAMT
        '
        Me.G5CESSAMT.Caption = "Cess Amt"
        Me.G5CESSAMT.DisplayFormat.FormatString = "0"
        Me.G5CESSAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G5CESSAMT.FieldName = "CESSAMT"
        Me.G5CESSAMT.Name = "G5CESSAMT"
        Me.G5CESSAMT.Visible = True
        Me.G5CESSAMT.VisibleIndex = 11
        '
        'G5PREGST
        '
        Me.G5PREGST.Caption = "Pre GST"
        Me.G5PREGST.FieldName = "PREGST"
        Me.G5PREGST.Name = "G5PREGST"
        Me.G5PREGST.Visible = True
        Me.G5PREGST.VisibleIndex = 12
        '
        'G5CGSTAMT
        '
        Me.G5CGSTAMT.Caption = "CGST Amt."
        Me.G5CGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G5CGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G5CGSTAMT.FieldName = "CGSTAMT"
        Me.G5CGSTAMT.Name = "G5CGSTAMT"
        Me.G5CGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G5CGSTAMT.Visible = True
        Me.G5CGSTAMT.VisibleIndex = 13
        Me.G5CGSTAMT.Width = 100
        '
        'G5SGSTAMT
        '
        Me.G5SGSTAMT.Caption = "SGST Amt."
        Me.G5SGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G5SGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G5SGSTAMT.FieldName = "SGSTAMT"
        Me.G5SGSTAMT.Name = "G5SGSTAMT"
        Me.G5SGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G5SGSTAMT.Visible = True
        Me.G5SGSTAMT.VisibleIndex = 14
        Me.G5SGSTAMT.Width = 100
        '
        'G5IGSTAMT
        '
        Me.G5IGSTAMT.Caption = "IGST Amt."
        Me.G5IGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G5IGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G5IGSTAMT.FieldName = "IGSTAMT"
        Me.G5IGSTAMT.Name = "G5IGSTAMT"
        Me.G5IGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G5IGSTAMT.Visible = True
        Me.G5IGSTAMT.VisibleIndex = 15
        Me.G5IGSTAMT.Width = 100
        '
        'TBHSN
        '
        Me.TBHSN.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TBHSN.Controls.Add(Me.GRIDHSNDETAILS)
        Me.TBHSN.Location = New System.Drawing.Point(4, 23)
        Me.TBHSN.Name = "TBHSN"
        Me.TBHSN.Padding = New System.Windows.Forms.Padding(3)
        Me.TBHSN.Size = New System.Drawing.Size(1250, 499)
        Me.TBHSN.TabIndex = 5
        Me.TBHSN.Text = "6. HSN (B2B)"
        '
        'GRIDHSNDETAILS
        '
        Me.GRIDHSNDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDHSNDETAILS.Location = New System.Drawing.Point(3, 1)
        Me.GRIDHSNDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDHSNDETAILS.MainView = Me.GRIDHSN
        Me.GRIDHSNDETAILS.Name = "GRIDHSNDETAILS"
        Me.GRIDHSNDETAILS.Size = New System.Drawing.Size(1244, 497)
        Me.GRIDHSNDETAILS.TabIndex = 8
        Me.GRIDHSNDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDHSN})
        '
        'GRIDHSN
        '
        Me.GRIDHSN.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDHSN.Appearance.Row.Options.UseFont = True
        Me.GRIDHSN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.G6HSN, Me.G6HSNDESC, Me.G6UQC, Me.G6TOTALQTY, Me.G6GRANDTOTAL, Me.G6RATE, Me.G6TAXABLEAMT, Me.G6IGSTAMT, Me.G6CGSTAMT, Me.G6SGSTAMT, Me.G6CESSAMT})
        Me.GRIDHSN.GridControl = Me.GRIDHSNDETAILS
        Me.GRIDHSN.Name = "GRIDHSN"
        Me.GRIDHSN.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDHSN.OptionsBehavior.Editable = False
        Me.GRIDHSN.OptionsView.ColumnAutoWidth = False
        Me.GRIDHSN.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDHSN.OptionsView.ShowAutoFilterRow = True
        Me.GRIDHSN.OptionsView.ShowFooter = True
        Me.GRIDHSN.OptionsView.ShowGroupPanel = False
        '
        'G6HSN
        '
        Me.G6HSN.Caption = "HSN Code"
        Me.G6HSN.FieldName = "HSNCODE"
        Me.G6HSN.Name = "G6HSN"
        Me.G6HSN.Visible = True
        Me.G6HSN.VisibleIndex = 0
        Me.G6HSN.Width = 130
        '
        'G6HSNDESC
        '
        Me.G6HSNDESC.Caption = "HSN Desc"
        Me.G6HSNDESC.FieldName = "HSNDESC"
        Me.G6HSNDESC.ImageIndex = 0
        Me.G6HSNDESC.Name = "G6HSNDESC"
        Me.G6HSNDESC.Visible = True
        Me.G6HSNDESC.VisibleIndex = 1
        Me.G6HSNDESC.Width = 130
        '
        'G6UQC
        '
        Me.G6UQC.Caption = "UQC"
        Me.G6UQC.FieldName = "UNIT"
        Me.G6UQC.Name = "G6UQC"
        Me.G6UQC.Visible = True
        Me.G6UQC.VisibleIndex = 2
        Me.G6UQC.Width = 120
        '
        'G6TOTALQTY
        '
        Me.G6TOTALQTY.Caption = "Total Qty"
        Me.G6TOTALQTY.DisplayFormat.FormatString = "0.00"
        Me.G6TOTALQTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G6TOTALQTY.FieldName = "TOTALQTY"
        Me.G6TOTALQTY.Name = "G6TOTALQTY"
        Me.G6TOTALQTY.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G6TOTALQTY.Visible = True
        Me.G6TOTALQTY.VisibleIndex = 3
        Me.G6TOTALQTY.Width = 100
        '
        'G6GRANDTOTAL
        '
        Me.G6GRANDTOTAL.Caption = "Total Value"
        Me.G6GRANDTOTAL.DisplayFormat.FormatString = "0.00"
        Me.G6GRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G6GRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.G6GRANDTOTAL.Name = "G6GRANDTOTAL"
        Me.G6GRANDTOTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G6GRANDTOTAL.Visible = True
        Me.G6GRANDTOTAL.VisibleIndex = 4
        Me.G6GRANDTOTAL.Width = 130
        '
        'G6RATE
        '
        Me.G6RATE.Caption = "Rate"
        Me.G6RATE.DisplayFormat.FormatString = "0.00"
        Me.G6RATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G6RATE.FieldName = "RATE"
        Me.G6RATE.Name = "G6RATE"
        Me.G6RATE.Visible = True
        Me.G6RATE.VisibleIndex = 5
        '
        'G6TAXABLEAMT
        '
        Me.G6TAXABLEAMT.Caption = "Taxable Amt."
        Me.G6TAXABLEAMT.DisplayFormat.FormatString = "0.00"
        Me.G6TAXABLEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G6TAXABLEAMT.FieldName = "TAXABLEAMT"
        Me.G6TAXABLEAMT.Name = "G6TAXABLEAMT"
        Me.G6TAXABLEAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G6TAXABLEAMT.Visible = True
        Me.G6TAXABLEAMT.VisibleIndex = 6
        Me.G6TAXABLEAMT.Width = 130
        '
        'G6IGSTAMT
        '
        Me.G6IGSTAMT.Caption = "IGST Amt."
        Me.G6IGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G6IGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G6IGSTAMT.FieldName = "IGSTAMT"
        Me.G6IGSTAMT.Name = "G6IGSTAMT"
        Me.G6IGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G6IGSTAMT.Visible = True
        Me.G6IGSTAMT.VisibleIndex = 7
        Me.G6IGSTAMT.Width = 100
        '
        'G6CGSTAMT
        '
        Me.G6CGSTAMT.Caption = "CGST Amt."
        Me.G6CGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G6CGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G6CGSTAMT.FieldName = "CGSTAMT"
        Me.G6CGSTAMT.Name = "G6CGSTAMT"
        Me.G6CGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G6CGSTAMT.Visible = True
        Me.G6CGSTAMT.VisibleIndex = 8
        Me.G6CGSTAMT.Width = 100
        '
        'G6SGSTAMT
        '
        Me.G6SGSTAMT.Caption = "SGST Amt."
        Me.G6SGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.G6SGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G6SGSTAMT.FieldName = "SGSTAMT"
        Me.G6SGSTAMT.Name = "G6SGSTAMT"
        Me.G6SGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.G6SGSTAMT.Visible = True
        Me.G6SGSTAMT.VisibleIndex = 9
        Me.G6SGSTAMT.Width = 100
        '
        'G6CESSAMT
        '
        Me.G6CESSAMT.Caption = "Cess Amt"
        Me.G6CESSAMT.DisplayFormat.FormatString = "0"
        Me.G6CESSAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G6CESSAMT.FieldName = "CESSAMT"
        Me.G6CESSAMT.Name = "G6CESSAMT"
        Me.G6CESSAMT.Visible = True
        Me.G6CESSAMT.VisibleIndex = 10
        '
        'TBDOCS
        '
        Me.TBDOCS.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TBDOCS.Controls.Add(Me.GRIDDOCSDETAILS)
        Me.TBDOCS.Location = New System.Drawing.Point(4, 23)
        Me.TBDOCS.Name = "TBDOCS"
        Me.TBDOCS.Padding = New System.Windows.Forms.Padding(3)
        Me.TBDOCS.Size = New System.Drawing.Size(1250, 499)
        Me.TBDOCS.TabIndex = 6
        Me.TBDOCS.Text = "8. DOCS"
        '
        'GRIDDOCSDETAILS
        '
        Me.GRIDDOCSDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDDOCSDETAILS.Location = New System.Drawing.Point(3, 1)
        Me.GRIDDOCSDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDDOCSDETAILS.MainView = Me.GRIDDOCS
        Me.GRIDDOCSDETAILS.Name = "GRIDDOCSDETAILS"
        Me.GRIDDOCSDETAILS.Size = New System.Drawing.Size(1245, 496)
        Me.GRIDDOCSDETAILS.TabIndex = 10
        Me.GRIDDOCSDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDDOCS})
        '
        'GRIDDOCS
        '
        Me.GRIDDOCS.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDDOCS.Appearance.Row.Options.UseFont = True
        Me.GRIDDOCS.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.G7DOCUMENT, Me.G7FROM, Me.G7TO, Me.G7TOTALNO, Me.G7TOTALCANCELLED})
        Me.GRIDDOCS.GridControl = Me.GRIDDOCSDETAILS
        Me.GRIDDOCS.Name = "GRIDDOCS"
        Me.GRIDDOCS.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDDOCS.OptionsBehavior.Editable = False
        Me.GRIDDOCS.OptionsView.ColumnAutoWidth = False
        Me.GRIDDOCS.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDDOCS.OptionsView.ShowAutoFilterRow = True
        Me.GRIDDOCS.OptionsView.ShowFooter = True
        Me.GRIDDOCS.OptionsView.ShowGroupPanel = False
        '
        'G7DOCUMENT
        '
        Me.G7DOCUMENT.Caption = "Name Of Document"
        Me.G7DOCUMENT.FieldName = "DOCUMENT"
        Me.G7DOCUMENT.Name = "G7DOCUMENT"
        Me.G7DOCUMENT.Visible = True
        Me.G7DOCUMENT.VisibleIndex = 0
        Me.G7DOCUMENT.Width = 400
        '
        'G7FROM
        '
        Me.G7FROM.Caption = "Sr No From"
        Me.G7FROM.FieldName = "FROM"
        Me.G7FROM.Name = "G7FROM"
        Me.G7FROM.Visible = True
        Me.G7FROM.VisibleIndex = 1
        Me.G7FROM.Width = 140
        '
        'G7TO
        '
        Me.G7TO.Caption = "Sr No To"
        Me.G7TO.FieldName = "TO"
        Me.G7TO.Name = "G7TO"
        Me.G7TO.Visible = True
        Me.G7TO.VisibleIndex = 2
        Me.G7TO.Width = 140
        '
        'G7TOTALNO
        '
        Me.G7TOTALNO.Caption = "Total Number"
        Me.G7TOTALNO.FieldName = "TOTALNUMBER"
        Me.G7TOTALNO.Name = "G7TOTALNO"
        Me.G7TOTALNO.Visible = True
        Me.G7TOTALNO.VisibleIndex = 3
        Me.G7TOTALNO.Width = 120
        '
        'G7TOTALCANCELLED
        '
        Me.G7TOTALCANCELLED.Caption = "Total Cancelled"
        Me.G7TOTALCANCELLED.DisplayFormat.FormatString = "0"
        Me.G7TOTALCANCELLED.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G7TOTALCANCELLED.FieldName = "TOTALCANCELLED"
        Me.G7TOTALCANCELLED.Name = "G7TOTALCANCELLED"
        Me.G7TOTALCANCELLED.Visible = True
        Me.G7TOTALCANCELLED.VisibleIndex = 4
        Me.G7TOTALCANCELLED.Width = 120
        '
        'TBHSNB2C
        '
        Me.TBHSNB2C.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TBHSNB2C.Controls.Add(Me.GRIDHSNB2CDETAILS)
        Me.TBHSNB2C.Location = New System.Drawing.Point(4, 23)
        Me.TBHSNB2C.Name = "TBHSNB2C"
        Me.TBHSNB2C.Padding = New System.Windows.Forms.Padding(3)
        Me.TBHSNB2C.Size = New System.Drawing.Size(1250, 499)
        Me.TBHSNB2C.TabIndex = 7
        Me.TBHSNB2C.Text = "7. HSN (B2C)"
        '
        'GRIDHSNB2CDETAILS
        '
        Me.GRIDHSNB2CDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDHSNB2CDETAILS.Location = New System.Drawing.Point(3, 1)
        Me.GRIDHSNB2CDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDHSNB2CDETAILS.MainView = Me.GRIDHSNB2C
        Me.GRIDHSNB2CDETAILS.Name = "GRIDHSNB2CDETAILS"
        Me.GRIDHSNB2CDETAILS.Size = New System.Drawing.Size(1244, 497)
        Me.GRIDHSNB2CDETAILS.TabIndex = 9
        Me.GRIDHSNB2CDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDHSNB2C})
        '
        'GRIDHSNB2C
        '
        Me.GRIDHSNB2C.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDHSNB2C.Appearance.Row.Options.UseFont = True
        Me.GRIDHSNB2C.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11})
        Me.GRIDHSNB2C.GridControl = Me.GRIDHSNB2CDETAILS
        Me.GRIDHSNB2C.Name = "GRIDHSNB2C"
        Me.GRIDHSNB2C.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDHSNB2C.OptionsBehavior.Editable = False
        Me.GRIDHSNB2C.OptionsView.ColumnAutoWidth = False
        Me.GRIDHSNB2C.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDHSNB2C.OptionsView.ShowAutoFilterRow = True
        Me.GRIDHSNB2C.OptionsView.ShowFooter = True
        Me.GRIDHSNB2C.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "HSN Code"
        Me.GridColumn1.FieldName = "HSNCODE"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 130
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "HSN Desc"
        Me.GridColumn2.FieldName = "HSNDESC"
        Me.GridColumn2.ImageIndex = 0
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 130
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "UQC"
        Me.GridColumn3.FieldName = "UNIT"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 120
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Total Qty"
        Me.GridColumn4.DisplayFormat.FormatString = "0.00"
        Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4.FieldName = "TOTALQTY"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 100
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Total Value"
        Me.GridColumn5.DisplayFormat.FormatString = "0.00"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "GRANDTOTAL"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 130
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Rate"
        Me.GridColumn6.DisplayFormat.FormatString = "0.00"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "RATE"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Taxable Amt."
        Me.GridColumn7.DisplayFormat.FormatString = "0.00"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "TAXABLEAMT"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        Me.GridColumn7.Width = 130
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "IGST Amt."
        Me.GridColumn8.DisplayFormat.FormatString = "0.00"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn8.FieldName = "IGSTAMT"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 7
        Me.GridColumn8.Width = 100
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "CGST Amt."
        Me.GridColumn9.DisplayFormat.FormatString = "0.00"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn9.FieldName = "CGSTAMT"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 8
        Me.GridColumn9.Width = 100
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "SGST Amt."
        Me.GridColumn10.DisplayFormat.FormatString = "0.00"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn10.FieldName = "SGSTAMT"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 9
        Me.GridColumn10.Width = 100
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Cess Amt"
        Me.GridColumn11.DisplayFormat.FormatString = "0"
        Me.GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn11.FieldName = "CESSAMT"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 10
        '
        'GSTR1GridReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1284, 581)
        Me.Controls.Add(Me.BlendPanel2)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "GSTR1GridReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "GSTR_1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel2.ResumeLayout(False)
        CType(Me.PBEXCEL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TABGSTR1.ResumeLayout(False)
        Me.TBB2B.ResumeLayout(False)
        CType(Me.GRIDB2BDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDB2B, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBB2CL.ResumeLayout(False)
        CType(Me.GRIDB2CDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDB2C, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBB2CS.ResumeLayout(False)
        CType(Me.GRIDB2CSDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDB2CS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBCDNR.ResumeLayout(False)
        CType(Me.GRIDCDNRDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDCDNR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBCDNRUR.ResumeLayout(False)
        CType(Me.GRIDCDNRURDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDCDNRUR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBHSN.ResumeLayout(False)
        CType(Me.GRIDHSNDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDHSN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBDOCS.ResumeLayout(False)
        CType(Me.GRIDDOCSDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDDOCS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBHSNB2C.ResumeLayout(False)
        CType(Me.GRIDHSNB2CDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDHSNB2C, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel2 As VbPowerPack.BlendPanel
    Friend WithEvents TABGSTR1 As TabControl
    Friend WithEvents TBB2B As TabPage
    Friend WithEvents TBB2CL As TabPage
    Friend WithEvents cmdexit As Button
    Friend WithEvents PBEXCEL As PictureBox
    Private WithEvents GRIDB2BDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDB2B As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GPRINTINITIALS As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGSTIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPARTYBILLDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPLACEOFSUPPLY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREVERSECHARGE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTAXABLEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GIGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAPPLICABLEPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVOICETYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GECOMMGSTIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCESSAMT As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GRIDB2CDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDB2C As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents G2INVNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2DATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2GRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2PLACEOFSUPPLY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2APPLICABLEPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2ECOMMGSTIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2RATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2TAXABLEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2CESSAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2CGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2SGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2IGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TBB2CS As TabPage
    Private WithEvents GRIDB2CSDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDB2CS As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents G3TYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G3PLACEOFSUPPLY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G3APPLICABLEPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G3RATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G3TAXABLEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G3CESSAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G3ECOMMGSTIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G3CGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G3SGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G3IGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TBCDNR As TabPage
    Private WithEvents GRIDCDNRDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDCDNR As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents G4GSTIN As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents G4NAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4INVNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4INVDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4CDNRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4CDNRDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4DOCTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4PLACEOFSUPPLY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4GRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4APPLICABLEPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4RATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4TAXABLEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4CESSAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4PREGST As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4CGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4SGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G4IGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMDREFRESH As Button
    Friend WithEvents TBCDNRUR As TabPage
    Private WithEvents GRIDCDNRURDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDCDNRUR As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents G5URTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5INVNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5INVDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5REFVOUCHERNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5REFVOUCHERDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5DOCTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5PLACEOFSUPPLY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5GRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5APPLICABLEPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5RATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5TAXABLEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5CESSAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5PREGST As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5CGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5SGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G5IGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TBHSN As TabPage
    Private WithEvents GRIDHSNDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDHSN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents G6HSN As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents G6HSNDESC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G6UQC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G6TOTALQTY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G6GRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G6RATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G6TAXABLEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G6IGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G6CGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G6SGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G6CESSAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TBDOCS As TabPage
    Private WithEvents GRIDDOCSDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDDOCS As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents G7DOCUMENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G7FROM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G7TO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G7TOTALNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G7TOTALCANCELLED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TBHSNB2C As TabPage
    Private WithEvents GRIDHSNB2CDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDHSNB2C As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
End Class
