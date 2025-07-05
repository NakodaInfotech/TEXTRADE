<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PurchaseITCDetails
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
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.GRIDBILLDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDBILL = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GENTRYNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GENTRYDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMONTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVNOBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVDATEBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVNOPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVDATEPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTAXABLEAMTBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCGSTAMTBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSGSTAMTBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GIGSTAMTBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGTOTALBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTAXABLEAMTPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCGSTAMTPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSGSTAMTPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GIGSTAMTPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGTOTALPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGSTRATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.GITCRECDLASTYEAR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GITCREVERSEDLASTYEAR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BlendPanel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.GRIDBILLDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDBILL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.GRIDBILLDETAILS)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 2
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.toolStripSeparator, Me.PrintToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 777
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
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
        Me.PrintToolStripButton.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'GRIDBILLDETAILS
        '
        Me.GRIDBILLDETAILS.EmbeddedNavigator.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILLDETAILS.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.GRIDBILLDETAILS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILLDETAILS.Location = New System.Drawing.Point(14, 30)
        Me.GRIDBILLDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDBILLDETAILS.MainView = Me.GRIDBILL
        Me.GRIDBILLDETAILS.Name = "GRIDBILLDETAILS"
        Me.GRIDBILLDETAILS.Size = New System.Drawing.Size(1207, 512)
        Me.GRIDBILLDETAILS.TabIndex = 776
        Me.GRIDBILLDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDBILL})
        '
        'GRIDBILL
        '
        Me.GRIDBILL.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILL.Appearance.HeaderPanel.Options.UseFont = True
        Me.GRIDBILL.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILL.Appearance.Row.Options.UseFont = True
        Me.GRIDBILL.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GENTRYNO, Me.GENTRYDATE, Me.GMONTH, Me.GBILLNO, Me.GTYPE, Me.GGSTIN, Me.GNAME, Me.GINVNOBOOKS, Me.GINVDATEBOOKS, Me.GINVNOPORTAL, Me.GINVDATEPORTAL, Me.GTAXABLEAMTBOOKS, Me.GCGSTAMTBOOKS, Me.GSGSTAMTBOOKS, Me.GIGSTAMTBOOKS, Me.GGTOTALBOOKS, Me.GTAXABLEAMTPORTAL, Me.GCGSTAMTPORTAL, Me.GSGSTAMTPORTAL, Me.GIGSTAMTPORTAL, Me.GGTOTALPORTAL, Me.GGSTRATE, Me.GREMARKS, Me.GITCRECDLASTYEAR, Me.GITCREVERSEDLASTYEAR})
        Me.GRIDBILL.CustomizationFormBounds = New System.Drawing.Rectangle(688, 311, 208, 184)
        Me.GRIDBILL.GridControl = Me.GRIDBILLDETAILS
        Me.GRIDBILL.Name = "GRIDBILL"
        Me.GRIDBILL.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDBILL.OptionsBehavior.Editable = False
        Me.GRIDBILL.OptionsCustomization.AllowColumnMoving = False
        Me.GRIDBILL.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        Me.GRIDBILL.OptionsView.ColumnAutoWidth = False
        Me.GRIDBILL.OptionsView.ShowAutoFilterRow = True
        Me.GRIDBILL.OptionsView.ShowFooter = True
        Me.GRIDBILL.OptionsView.ShowGroupPanel = False
        '
        'GENTRYNO
        '
        Me.GENTRYNO.Caption = "Entry No"
        Me.GENTRYNO.FieldName = "ENTRYNO"
        Me.GENTRYNO.Name = "GENTRYNO"
        Me.GENTRYNO.Visible = True
        Me.GENTRYNO.VisibleIndex = 0
        '
        'GENTRYDATE
        '
        Me.GENTRYDATE.Caption = "Date"
        Me.GENTRYDATE.FieldName = "DATE"
        Me.GENTRYDATE.Name = "GENTRYDATE"
        Me.GENTRYDATE.Visible = True
        Me.GENTRYDATE.VisibleIndex = 1
        '
        'GMONTH
        '
        Me.GMONTH.Caption = "Month"
        Me.GMONTH.FieldName = "MONTH"
        Me.GMONTH.Name = "GMONTH"
        Me.GMONTH.Visible = True
        Me.GMONTH.VisibleIndex = 2
        Me.GMONTH.Width = 100
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
        'GTYPE
        '
        Me.GTYPE.Caption = "Type"
        Me.GTYPE.FieldName = "TYPE"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.OptionsColumn.AllowEdit = False
        Me.GTYPE.Visible = True
        Me.GTYPE.VisibleIndex = 4
        '
        'GGSTIN
        '
        Me.GGSTIN.Caption = "Party GSTIN"
        Me.GGSTIN.FieldName = "GSTIN"
        Me.GGSTIN.Name = "GGSTIN"
        Me.GGSTIN.OptionsColumn.AllowEdit = False
        Me.GGSTIN.Visible = True
        Me.GGSTIN.VisibleIndex = 5
        Me.GGSTIN.Width = 120
        '
        'GNAME
        '
        Me.GNAME.Caption = "Party Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.OptionsColumn.AllowEdit = False
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 6
        Me.GNAME.Width = 200
        '
        'GINVNOBOOKS
        '
        Me.GINVNOBOOKS.Caption = "Invoice No (Books)"
        Me.GINVNOBOOKS.FieldName = "INVNOBOOKS"
        Me.GINVNOBOOKS.Name = "GINVNOBOOKS"
        Me.GINVNOBOOKS.OptionsColumn.AllowEdit = False
        Me.GINVNOBOOKS.Visible = True
        Me.GINVNOBOOKS.VisibleIndex = 7
        Me.GINVNOBOOKS.Width = 120
        '
        'GINVDATEBOOKS
        '
        Me.GINVDATEBOOKS.Caption = "Inv Dt. (Books)"
        Me.GINVDATEBOOKS.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GINVDATEBOOKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GINVDATEBOOKS.FieldName = "INVDATEBOOKS"
        Me.GINVDATEBOOKS.Name = "GINVDATEBOOKS"
        Me.GINVDATEBOOKS.OptionsColumn.AllowEdit = False
        Me.GINVDATEBOOKS.Visible = True
        Me.GINVDATEBOOKS.VisibleIndex = 8
        Me.GINVDATEBOOKS.Width = 100
        '
        'GINVNOPORTAL
        '
        Me.GINVNOPORTAL.Caption = "Invoice No (Portal)"
        Me.GINVNOPORTAL.FieldName = "INVNOPORTAL"
        Me.GINVNOPORTAL.Name = "GINVNOPORTAL"
        Me.GINVNOPORTAL.OptionsColumn.AllowEdit = False
        Me.GINVNOPORTAL.Visible = True
        Me.GINVNOPORTAL.VisibleIndex = 9
        Me.GINVNOPORTAL.Width = 120
        '
        'GINVDATEPORTAL
        '
        Me.GINVDATEPORTAL.Caption = "Inv Dt. (Portal)"
        Me.GINVDATEPORTAL.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GINVDATEPORTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GINVDATEPORTAL.FieldName = "INVDATEPORTAL"
        Me.GINVDATEPORTAL.Name = "GINVDATEPORTAL"
        Me.GINVDATEPORTAL.OptionsColumn.AllowEdit = False
        Me.GINVDATEPORTAL.Visible = True
        Me.GINVDATEPORTAL.VisibleIndex = 10
        Me.GINVDATEPORTAL.Width = 100
        '
        'GTAXABLEAMTBOOKS
        '
        Me.GTAXABLEAMTBOOKS.Caption = "Taxable Amt (Books)"
        Me.GTAXABLEAMTBOOKS.DisplayFormat.FormatString = "0.00"
        Me.GTAXABLEAMTBOOKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTAXABLEAMTBOOKS.FieldName = "TAXABLEAMTBOOKS"
        Me.GTAXABLEAMTBOOKS.Name = "GTAXABLEAMTBOOKS"
        Me.GTAXABLEAMTBOOKS.OptionsColumn.AllowEdit = False
        Me.GTAXABLEAMTBOOKS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTAXABLEAMTBOOKS.Visible = True
        Me.GTAXABLEAMTBOOKS.VisibleIndex = 11
        Me.GTAXABLEAMTBOOKS.Width = 120
        '
        'GCGSTAMTBOOKS
        '
        Me.GCGSTAMTBOOKS.Caption = "CGST Amt (Books)"
        Me.GCGSTAMTBOOKS.DisplayFormat.FormatString = "0.00"
        Me.GCGSTAMTBOOKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCGSTAMTBOOKS.FieldName = "CGSTAMTBOOKS"
        Me.GCGSTAMTBOOKS.Name = "GCGSTAMTBOOKS"
        Me.GCGSTAMTBOOKS.OptionsColumn.AllowEdit = False
        Me.GCGSTAMTBOOKS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GCGSTAMTBOOKS.Visible = True
        Me.GCGSTAMTBOOKS.VisibleIndex = 12
        Me.GCGSTAMTBOOKS.Width = 120
        '
        'GSGSTAMTBOOKS
        '
        Me.GSGSTAMTBOOKS.Caption = "SGST Amt (Books)"
        Me.GSGSTAMTBOOKS.DisplayFormat.FormatString = "0.00"
        Me.GSGSTAMTBOOKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GSGSTAMTBOOKS.FieldName = "SGSTAMTBOOKS"
        Me.GSGSTAMTBOOKS.Name = "GSGSTAMTBOOKS"
        Me.GSGSTAMTBOOKS.OptionsColumn.AllowEdit = False
        Me.GSGSTAMTBOOKS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GSGSTAMTBOOKS.Visible = True
        Me.GSGSTAMTBOOKS.VisibleIndex = 13
        Me.GSGSTAMTBOOKS.Width = 120
        '
        'GIGSTAMTBOOKS
        '
        Me.GIGSTAMTBOOKS.Caption = "IGST Amt (Books)"
        Me.GIGSTAMTBOOKS.DisplayFormat.FormatString = "0.00"
        Me.GIGSTAMTBOOKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GIGSTAMTBOOKS.FieldName = "IGSTAMTBOOKS"
        Me.GIGSTAMTBOOKS.Name = "GIGSTAMTBOOKS"
        Me.GIGSTAMTBOOKS.OptionsColumn.AllowEdit = False
        Me.GIGSTAMTBOOKS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GIGSTAMTBOOKS.Visible = True
        Me.GIGSTAMTBOOKS.VisibleIndex = 14
        Me.GIGSTAMTBOOKS.Width = 120
        '
        'GGTOTALBOOKS
        '
        Me.GGTOTALBOOKS.Caption = "G Total (Books)"
        Me.GGTOTALBOOKS.DisplayFormat.FormatString = "0.00"
        Me.GGTOTALBOOKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGTOTALBOOKS.FieldName = "GRANDTOTALBOOKS"
        Me.GGTOTALBOOKS.Name = "GGTOTALBOOKS"
        Me.GGTOTALBOOKS.OptionsColumn.AllowEdit = False
        Me.GGTOTALBOOKS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GGTOTALBOOKS.Visible = True
        Me.GGTOTALBOOKS.VisibleIndex = 15
        Me.GGTOTALBOOKS.Width = 120
        '
        'GTAXABLEAMTPORTAL
        '
        Me.GTAXABLEAMTPORTAL.Caption = "Taxable Amt (Portal)"
        Me.GTAXABLEAMTPORTAL.DisplayFormat.FormatString = "0.00"
        Me.GTAXABLEAMTPORTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTAXABLEAMTPORTAL.FieldName = "TAXABLEAMTPORTAL"
        Me.GTAXABLEAMTPORTAL.Name = "GTAXABLEAMTPORTAL"
        Me.GTAXABLEAMTPORTAL.OptionsColumn.AllowEdit = False
        Me.GTAXABLEAMTPORTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTAXABLEAMTPORTAL.Visible = True
        Me.GTAXABLEAMTPORTAL.VisibleIndex = 16
        Me.GTAXABLEAMTPORTAL.Width = 120
        '
        'GCGSTAMTPORTAL
        '
        Me.GCGSTAMTPORTAL.Caption = "CGST Amt (Portal)"
        Me.GCGSTAMTPORTAL.DisplayFormat.FormatString = "0.00"
        Me.GCGSTAMTPORTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCGSTAMTPORTAL.FieldName = "CGSTAMTPORTAL"
        Me.GCGSTAMTPORTAL.Name = "GCGSTAMTPORTAL"
        Me.GCGSTAMTPORTAL.OptionsColumn.AllowEdit = False
        Me.GCGSTAMTPORTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GCGSTAMTPORTAL.Visible = True
        Me.GCGSTAMTPORTAL.VisibleIndex = 17
        Me.GCGSTAMTPORTAL.Width = 120
        '
        'GSGSTAMTPORTAL
        '
        Me.GSGSTAMTPORTAL.Caption = "SGST Amt (Portal)"
        Me.GSGSTAMTPORTAL.DisplayFormat.FormatString = "0.00"
        Me.GSGSTAMTPORTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GSGSTAMTPORTAL.FieldName = "SGSTAMTPORTAL"
        Me.GSGSTAMTPORTAL.Name = "GSGSTAMTPORTAL"
        Me.GSGSTAMTPORTAL.OptionsColumn.AllowEdit = False
        Me.GSGSTAMTPORTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GSGSTAMTPORTAL.Visible = True
        Me.GSGSTAMTPORTAL.VisibleIndex = 18
        Me.GSGSTAMTPORTAL.Width = 120
        '
        'GIGSTAMTPORTAL
        '
        Me.GIGSTAMTPORTAL.Caption = "IGST Amt (Portal)"
        Me.GIGSTAMTPORTAL.DisplayFormat.FormatString = "0.00"
        Me.GIGSTAMTPORTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GIGSTAMTPORTAL.FieldName = "IGSTAMTPORTAL"
        Me.GIGSTAMTPORTAL.Name = "GIGSTAMTPORTAL"
        Me.GIGSTAMTPORTAL.OptionsColumn.AllowEdit = False
        Me.GIGSTAMTPORTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GIGSTAMTPORTAL.Visible = True
        Me.GIGSTAMTPORTAL.VisibleIndex = 19
        Me.GIGSTAMTPORTAL.Width = 120
        '
        'GGTOTALPORTAL
        '
        Me.GGTOTALPORTAL.Caption = "G Total (Portal)"
        Me.GGTOTALPORTAL.DisplayFormat.FormatString = "0.00"
        Me.GGTOTALPORTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGTOTALPORTAL.FieldName = "GRANDTOTALPORTAL"
        Me.GGTOTALPORTAL.Name = "GGTOTALPORTAL"
        Me.GGTOTALPORTAL.OptionsColumn.AllowEdit = False
        Me.GGTOTALPORTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GGTOTALPORTAL.Visible = True
        Me.GGTOTALPORTAL.VisibleIndex = 20
        Me.GGTOTALPORTAL.Width = 120
        '
        'GGSTRATE
        '
        Me.GGSTRATE.Caption = "GST%"
        Me.GGSTRATE.DisplayFormat.FormatString = "0.00"
        Me.GGSTRATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGSTRATE.FieldName = "GSTRATE"
        Me.GGSTRATE.Name = "GGSTRATE"
        Me.GGSTRATE.OptionsColumn.AllowEdit = False
        Me.GGSTRATE.Visible = True
        Me.GGSTRATE.VisibleIndex = 21
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "GRIDREMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.Visible = True
        Me.GREMARKS.VisibleIndex = 22
        Me.GREMARKS.Width = 200
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(534, 548)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 769
        Me.CMDOK.Text = "&Ok"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(620, 548)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 4
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'GITCRECDLASTYEAR
        '
        Me.GITCRECDLASTYEAR.Caption = "ITC Recd"
        Me.GITCRECDLASTYEAR.FieldName = "ITCRECD"
        Me.GITCRECDLASTYEAR.Name = "GITCRECDLASTYEAR"
        Me.GITCRECDLASTYEAR.Visible = True
        Me.GITCRECDLASTYEAR.VisibleIndex = 23
        '
        'GITCREVERSEDLASTYEAR
        '
        Me.GITCREVERSEDLASTYEAR.Caption = "ITC Reversed"
        Me.GITCREVERSEDLASTYEAR.FieldName = "ITCREVERSED"
        Me.GITCREVERSEDLASTYEAR.Name = "GITCREVERSEDLASTYEAR"
        Me.GITCREVERSEDLASTYEAR.Visible = True
        Me.GITCREVERSEDLASTYEAR.VisibleIndex = 24
        '
        'PurchaseITCDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "PurchaseITCDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Purchase ITC Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.GRIDBILLDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDBILL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Private WithEvents GRIDBILLDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDBILL As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGSTIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVNOBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVDATEBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVNOPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVDATEPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTAXABLEAMTBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCGSTAMTBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSGSTAMTBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GIGSTAMTBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGTOTALBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTAXABLEAMTPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCGSTAMTPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSGSTAMTPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GIGSTAMTPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGTOTALPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGSTRATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMDOK As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents GENTRYNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GENTRYDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMONTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents GITCRECDLASTYEAR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GITCREVERSEDLASTYEAR As DevExpress.XtraGrid.Columns.GridColumn
End Class
