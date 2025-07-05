<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PurchaseITCEntry
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PurchaseITCEntry))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.GRIDBILLDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDBILL = New DevExpress.XtraGrid.Views.Grid.GridView()
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
        Me.GITCRECDLASTYEAR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GITCREVERSEDLASTYEAR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LBLDYEINGTYPE = New System.Windows.Forms.Label()
        Me.CMBMONTH = New System.Windows.Forms.ComboBox()
        Me.CMDSELECTBILLS = New System.Windows.Forms.Button()
        Me.tstxtbillno = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.tooldelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.toolprevious = New System.Windows.Forms.ToolStripButton()
        Me.toolnext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.DTENTRYDATE = New System.Windows.Forms.DateTimePicker()
        Me.TXTENTRYNO = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CMDSAVE = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDBILLDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDBILL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.GRIDBILLDETAILS)
        Me.BlendPanel1.Controls.Add(Me.LBLDYEINGTYPE)
        Me.BlendPanel1.Controls.Add(Me.CMBMONTH)
        Me.BlendPanel1.Controls.Add(Me.CMDSELECTBILLS)
        Me.BlendPanel1.Controls.Add(Me.tstxtbillno)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.GroupBox1)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.cmdclear)
        Me.BlendPanel1.Controls.Add(Me.DTENTRYDATE)
        Me.BlendPanel1.Controls.Add(Me.TXTENTRYNO)
        Me.BlendPanel1.Controls.Add(Me.Label12)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.CMDSAVE)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 0
        '
        'GRIDBILLDETAILS
        '
        Me.GRIDBILLDETAILS.EmbeddedNavigator.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILLDETAILS.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.GRIDBILLDETAILS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILLDETAILS.Location = New System.Drawing.Point(15, 96)
        Me.GRIDBILLDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDBILLDETAILS.MainView = Me.GRIDBILL
        Me.GRIDBILLDETAILS.Name = "GRIDBILLDETAILS"
        Me.GRIDBILLDETAILS.Size = New System.Drawing.Size(1207, 446)
        Me.GRIDBILLDETAILS.TabIndex = 9
        Me.GRIDBILLDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDBILL})
        '
        'GRIDBILL
        '
        Me.GRIDBILL.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILL.Appearance.HeaderPanel.Options.UseFont = True
        Me.GRIDBILL.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILL.Appearance.Row.Options.UseFont = True
        Me.GRIDBILL.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GBILLNO, Me.GTYPE, Me.GGSTIN, Me.GNAME, Me.GINVNOBOOKS, Me.GINVDATEBOOKS, Me.GINVNOPORTAL, Me.GINVDATEPORTAL, Me.GTAXABLEAMTBOOKS, Me.GCGSTAMTBOOKS, Me.GSGSTAMTBOOKS, Me.GIGSTAMTBOOKS, Me.GGTOTALBOOKS, Me.GTAXABLEAMTPORTAL, Me.GCGSTAMTPORTAL, Me.GSGSTAMTPORTAL, Me.GIGSTAMTPORTAL, Me.GGTOTALPORTAL, Me.GGSTRATE, Me.GREMARKS, Me.GITCRECDLASTYEAR, Me.GITCREVERSEDLASTYEAR})
        Me.GRIDBILL.CustomizationFormBounds = New System.Drawing.Rectangle(688, 311, 208, 184)
        Me.GRIDBILL.GridControl = Me.GRIDBILLDETAILS
        Me.GRIDBILL.Name = "GRIDBILL"
        Me.GRIDBILL.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDBILL.OptionsCustomization.AllowColumnMoving = False
        Me.GRIDBILL.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        Me.GRIDBILL.OptionsView.ColumnAutoWidth = False
        Me.GRIDBILL.OptionsView.ShowAutoFilterRow = True
        Me.GRIDBILL.OptionsView.ShowFooter = True
        Me.GRIDBILL.OptionsView.ShowGroupPanel = False
        '
        'GBILLNO
        '
        Me.GBILLNO.Caption = "Bill No"
        Me.GBILLNO.FieldName = "BILLNO"
        Me.GBILLNO.Name = "GBILLNO"
        Me.GBILLNO.OptionsColumn.AllowEdit = False
        Me.GBILLNO.Visible = True
        Me.GBILLNO.VisibleIndex = 0
        '
        'GTYPE
        '
        Me.GTYPE.Caption = "Type"
        Me.GTYPE.FieldName = "TYPE"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.OptionsColumn.AllowEdit = False
        Me.GTYPE.Visible = True
        Me.GTYPE.VisibleIndex = 1
        '
        'GGSTIN
        '
        Me.GGSTIN.Caption = "Party GSTIN"
        Me.GGSTIN.FieldName = "GSTIN"
        Me.GGSTIN.Name = "GGSTIN"
        Me.GGSTIN.OptionsColumn.AllowEdit = False
        Me.GGSTIN.Visible = True
        Me.GGSTIN.VisibleIndex = 2
        Me.GGSTIN.Width = 120
        '
        'GNAME
        '
        Me.GNAME.Caption = "Party Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.OptionsColumn.AllowEdit = False
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 3
        Me.GNAME.Width = 200
        '
        'GINVNOBOOKS
        '
        Me.GINVNOBOOKS.Caption = "Invoice No (Books)"
        Me.GINVNOBOOKS.FieldName = "INVNOBOOKS"
        Me.GINVNOBOOKS.Name = "GINVNOBOOKS"
        Me.GINVNOBOOKS.OptionsColumn.AllowEdit = False
        Me.GINVNOBOOKS.Visible = True
        Me.GINVNOBOOKS.VisibleIndex = 4
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
        Me.GINVDATEBOOKS.VisibleIndex = 5
        Me.GINVDATEBOOKS.Width = 100
        '
        'GINVNOPORTAL
        '
        Me.GINVNOPORTAL.Caption = "Invoice No (Portal)"
        Me.GINVNOPORTAL.FieldName = "INVNOPORTAL"
        Me.GINVNOPORTAL.Name = "GINVNOPORTAL"
        Me.GINVNOPORTAL.OptionsColumn.AllowEdit = False
        Me.GINVNOPORTAL.Visible = True
        Me.GINVNOPORTAL.VisibleIndex = 6
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
        Me.GINVDATEPORTAL.VisibleIndex = 7
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
        Me.GTAXABLEAMTBOOKS.VisibleIndex = 8
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
        Me.GCGSTAMTBOOKS.VisibleIndex = 9
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
        Me.GSGSTAMTBOOKS.VisibleIndex = 10
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
        Me.GIGSTAMTBOOKS.VisibleIndex = 11
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
        Me.GGTOTALBOOKS.VisibleIndex = 12
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
        Me.GTAXABLEAMTPORTAL.VisibleIndex = 13
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
        Me.GCGSTAMTPORTAL.VisibleIndex = 14
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
        Me.GSGSTAMTPORTAL.VisibleIndex = 15
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
        Me.GIGSTAMTPORTAL.VisibleIndex = 16
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
        Me.GGTOTALPORTAL.VisibleIndex = 17
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
        Me.GGSTRATE.VisibleIndex = 18
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "GRIDREMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.Visible = True
        Me.GREMARKS.VisibleIndex = 19
        Me.GREMARKS.Width = 200
        '
        'GITCRECDLASTYEAR
        '
        Me.GITCRECDLASTYEAR.Caption = "ITC Recd LY"
        Me.GITCRECDLASTYEAR.FieldName = "ITCRECD"
        Me.GITCRECDLASTYEAR.Name = "GITCRECDLASTYEAR"
        Me.GITCRECDLASTYEAR.Visible = True
        Me.GITCRECDLASTYEAR.VisibleIndex = 20
        '
        'GITCREVERSEDLASTYEAR
        '
        Me.GITCREVERSEDLASTYEAR.Caption = "ITC Reversed LY"
        Me.GITCREVERSEDLASTYEAR.FieldName = "ITCREVERSED"
        Me.GITCREVERSEDLASTYEAR.Name = "GITCREVERSEDLASTYEAR"
        Me.GITCREVERSEDLASTYEAR.Visible = True
        Me.GITCREVERSEDLASTYEAR.VisibleIndex = 21
        '
        'LBLDYEINGTYPE
        '
        Me.LBLDYEINGTYPE.AutoSize = True
        Me.LBLDYEINGTYPE.BackColor = System.Drawing.Color.Transparent
        Me.LBLDYEINGTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLDYEINGTYPE.ForeColor = System.Drawing.Color.Black
        Me.LBLDYEINGTYPE.Location = New System.Drawing.Point(25, 42)
        Me.LBLDYEINGTYPE.Name = "LBLDYEINGTYPE"
        Me.LBLDYEINGTYPE.Size = New System.Drawing.Size(43, 15)
        Me.LBLDYEINGTYPE.TabIndex = 775
        Me.LBLDYEINGTYPE.Text = "Month"
        '
        'CMBMONTH
        '
        Me.CMBMONTH.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBMONTH.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBMONTH.BackColor = System.Drawing.Color.White
        Me.CMBMONTH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBMONTH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBMONTH.FormattingEnabled = True
        Me.CMBMONTH.Items.AddRange(New Object() {"APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER", "JANUARY", "FEBRUARY", "MARCH"})
        Me.CMBMONTH.Location = New System.Drawing.Point(72, 39)
        Me.CMBMONTH.MaxDropDownItems = 14
        Me.CMBMONTH.Name = "CMBMONTH"
        Me.CMBMONTH.Size = New System.Drawing.Size(131, 23)
        Me.CMBMONTH.TabIndex = 2
        '
        'CMDSELECTBILLS
        '
        Me.CMDSELECTBILLS.BackColor = System.Drawing.Color.Transparent
        Me.CMDSELECTBILLS.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSELECTBILLS.FlatAppearance.BorderSize = 0
        Me.CMDSELECTBILLS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSELECTBILLS.ForeColor = System.Drawing.Color.Black
        Me.CMDSELECTBILLS.Location = New System.Drawing.Point(405, 548)
        Me.CMDSELECTBILLS.Name = "CMDSELECTBILLS"
        Me.CMDSELECTBILLS.Size = New System.Drawing.Size(80, 28)
        Me.CMDSELECTBILLS.TabIndex = 4
        Me.CMDSELECTBILLS.Text = "Select &Bills"
        Me.CMDSELECTBILLS.UseVisualStyleBackColor = False
        '
        'tstxtbillno
        '
        Me.tstxtbillno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstxtbillno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstxtbillno.Location = New System.Drawing.Point(241, 1)
        Me.tstxtbillno.Name = "tstxtbillno"
        Me.tstxtbillno.Size = New System.Drawing.Size(56, 22)
        Me.tstxtbillno.TabIndex = 772
        Me.tstxtbillno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.tooldelete, Me.toolStripSeparator, Me.toolprevious, Me.toolnext, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 771
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.OpenToolStripButton.Text = "&Open"
        '
        'SaveToolStripButton
        '
        Me.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"), System.Drawing.Image)
        Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.SaveToolStripButton.Text = "&Save"
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
        'tooldelete
        '
        Me.tooldelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tooldelete.Image = CType(resources.GetObject("tooldelete.Image"), System.Drawing.Image)
        Me.tooldelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tooldelete.Name = "tooldelete"
        Me.tooldelete.Size = New System.Drawing.Size(23, 22)
        Me.tooldelete.Text = "&Delete"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'toolprevious
        '
        Me.toolprevious.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toolprevious.Image = Global.TEXTRADE.My.Resources.Resources.POINT021
        Me.toolprevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolprevious.Name = "toolprevious"
        Me.toolprevious.Size = New System.Drawing.Size(73, 22)
        Me.toolprevious.Text = "Previous"
        '
        'toolnext
        '
        Me.toolnext.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toolnext.Image = Global.TEXTRADE.My.Resources.Resources.POINT04
        Me.toolnext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolnext.Name = "toolnext"
        Me.toolnext.Size = New System.Drawing.Size(51, 22)
        Me.toolnext.Text = "Next"
        Me.toolnext.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.txtremarks)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(560, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(276, 68)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Remarks"
        '
        'txtremarks
        '
        Me.txtremarks.ForeColor = System.Drawing.Color.DimGray
        Me.txtremarks.Location = New System.Drawing.Point(8, 18)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(261, 44)
        Me.txtremarks.TabIndex = 0
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmddelete.FlatAppearance.BorderSize = 0
        Me.cmddelete.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddelete.ForeColor = System.Drawing.Color.Black
        Me.cmddelete.Location = New System.Drawing.Point(663, 548)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 28)
        Me.cmddelete.TabIndex = 7
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.Color.Transparent
        Me.cmdclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdclear.FlatAppearance.BorderSize = 0
        Me.cmdclear.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.ForeColor = System.Drawing.Color.Black
        Me.cmdclear.Location = New System.Drawing.Point(577, 548)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(80, 28)
        Me.cmdclear.TabIndex = 6
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = False
        '
        'DTENTRYDATE
        '
        Me.DTENTRYDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTENTRYDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTENTRYDATE.Location = New System.Drawing.Point(1043, 64)
        Me.DTENTRYDATE.Name = "DTENTRYDATE"
        Me.DTENTRYDATE.Size = New System.Drawing.Size(91, 23)
        Me.DTENTRYDATE.TabIndex = 1
        '
        'TXTENTRYNO
        '
        Me.TXTENTRYNO.BackColor = System.Drawing.Color.Linen
        Me.TXTENTRYNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTENTRYNO.Location = New System.Drawing.Point(1043, 35)
        Me.TXTENTRYNO.Name = "TXTENTRYNO"
        Me.TXTENTRYNO.ReadOnly = True
        Me.TXTENTRYNO.Size = New System.Drawing.Size(91, 23)
        Me.TXTENTRYNO.TabIndex = 0
        Me.TXTENTRYNO.TabStop = False
        Me.TXTENTRYNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(1001, 39)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(38, 15)
        Me.Label12.TabIndex = 767
        Me.Label12.Text = "Sr. No"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(1008, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 15)
        Me.Label9.TabIndex = 765
        Me.Label9.Text = "Date"
        '
        'CMDSAVE
        '
        Me.CMDSAVE.BackColor = System.Drawing.Color.Transparent
        Me.CMDSAVE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSAVE.FlatAppearance.BorderSize = 0
        Me.CMDSAVE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSAVE.ForeColor = System.Drawing.Color.Black
        Me.CMDSAVE.Location = New System.Drawing.Point(491, 548)
        Me.CMDSAVE.Name = "CMDSAVE"
        Me.CMDSAVE.Size = New System.Drawing.Size(80, 28)
        Me.CMDSAVE.TabIndex = 5
        Me.CMDSAVE.Text = "&Save"
        Me.CMDSAVE.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(749, 548)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 8
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'PurchaseITCEntry
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "PurchaseITCEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Purchase ITC Entry"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDBILLDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDBILL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDSELECTBILLS As Button
    Friend WithEvents tstxtbillno As TextBox
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents SaveToolStripButton As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents tooldelete As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents toolprevious As ToolStripButton
    Friend WithEvents toolnext As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtremarks As TextBox
    Friend WithEvents cmddelete As Button
    Friend WithEvents cmdclear As Button
    Friend WithEvents DTENTRYDATE As DateTimePicker
    Friend WithEvents TXTENTRYNO As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents CMDSAVE As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents LBLDYEINGTYPE As Label
    Friend WithEvents CMBMONTH As ComboBox
    Friend WithEvents EP As ErrorProvider
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
    Friend WithEvents GITCRECDLASTYEAR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GITCREVERSEDLASTYEAR As DevExpress.XtraGrid.Columns.GridColumn
End Class
