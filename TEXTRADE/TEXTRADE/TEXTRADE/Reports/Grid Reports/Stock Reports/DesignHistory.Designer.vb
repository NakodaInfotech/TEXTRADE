<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DesignHistory
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GRIDSTOCKDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDSTOCK = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.SPIECETYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SUNIT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GRIDORDERDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDORDER = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ONAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.OORDERNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ODUEDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.OUNIT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.OPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.OMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.GRIDGDNDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDGDN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CCHALLANNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCHALLANDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CINVNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRIDDESIGN = New System.Windows.Forms.DataGridView()
        Me.GDETAILS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.GRIDSTOCKDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDSTOCK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.GRIDORDERDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDORDER, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.GRIDGDNDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDGDN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDDESIGN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TabControl1)
        Me.BlendPanel1.Controls.Add(Me.GRIDDESIGN)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1286, 581)
        Me.BlendPanel1.TabIndex = 11
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(722, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(552, 530)
        Me.TabControl1.TabIndex = 452
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.GRIDSTOCKDETAILS)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(544, 502)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Stock Details"
        '
        'GRIDSTOCKDETAILS
        '
        Me.GRIDSTOCKDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDSTOCKDETAILS.Location = New System.Drawing.Point(6, 6)
        Me.GRIDSTOCKDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDSTOCKDETAILS.MainView = Me.GRIDSTOCK
        Me.GRIDSTOCKDETAILS.Name = "GRIDSTOCKDETAILS"
        Me.GRIDSTOCKDETAILS.Size = New System.Drawing.Size(532, 490)
        Me.GRIDSTOCKDETAILS.TabIndex = 450
        Me.GRIDSTOCKDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDSTOCK})
        '
        'GRIDSTOCK
        '
        Me.GRIDSTOCK.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDSTOCK.Appearance.HeaderPanel.Options.UseFont = True
        Me.GRIDSTOCK.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDSTOCK.Appearance.Row.Options.UseFont = True
        Me.GRIDSTOCK.Appearance.Row.Options.UseTextOptions = True
        Me.GRIDSTOCK.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GRIDSTOCK.Appearance.ViewCaption.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDSTOCK.Appearance.ViewCaption.Options.UseFont = True
        Me.GRIDSTOCK.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.SPIECETYPE, Me.SUNIT, Me.SPCS, Me.SMTRS})
        Me.GRIDSTOCK.GridControl = Me.GRIDSTOCKDETAILS
        Me.GRIDSTOCK.Name = "GRIDSTOCK"
        Me.GRIDSTOCK.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDSTOCK.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDSTOCK.OptionsBehavior.Editable = False
        Me.GRIDSTOCK.OptionsMenu.EnableColumnMenu = False
        Me.GRIDSTOCK.OptionsView.ColumnAutoWidth = False
        Me.GRIDSTOCK.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDSTOCK.OptionsView.ShowAutoFilterRow = True
        Me.GRIDSTOCK.OptionsView.ShowFooter = True
        Me.GRIDSTOCK.OptionsView.ShowGroupPanel = False
        '
        'SPIECETYPE
        '
        Me.SPIECETYPE.AppearanceCell.BackColor = System.Drawing.Color.Linen
        Me.SPIECETYPE.AppearanceCell.Options.UseBackColor = True
        Me.SPIECETYPE.Caption = "Piece Type"
        Me.SPIECETYPE.FieldName = "PIECETYPE"
        Me.SPIECETYPE.Name = "SPIECETYPE"
        Me.SPIECETYPE.Visible = True
        Me.SPIECETYPE.VisibleIndex = 0
        Me.SPIECETYPE.Width = 200
        '
        'SUNIT
        '
        Me.SUNIT.AppearanceCell.BackColor = System.Drawing.Color.Linen
        Me.SUNIT.AppearanceCell.Options.UseBackColor = True
        Me.SUNIT.Caption = "Unit"
        Me.SUNIT.FieldName = "UNIT"
        Me.SUNIT.Name = "SUNIT"
        Me.SUNIT.Visible = True
        Me.SUNIT.VisibleIndex = 1
        Me.SUNIT.Width = 100
        '
        'SPCS
        '
        Me.SPCS.AppearanceCell.BackColor = System.Drawing.Color.Linen
        Me.SPCS.AppearanceCell.Options.UseBackColor = True
        Me.SPCS.Caption = "Pcs"
        Me.SPCS.DisplayFormat.FormatString = "0"
        Me.SPCS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SPCS.FieldName = "PCS"
        Me.SPCS.Name = "SPCS"
        Me.SPCS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.SPCS.Visible = True
        Me.SPCS.VisibleIndex = 2
        '
        'SMTRS
        '
        Me.SMTRS.AppearanceCell.BackColor = System.Drawing.Color.Linen
        Me.SMTRS.AppearanceCell.Options.UseBackColor = True
        Me.SMTRS.Caption = "Mtrs"
        Me.SMTRS.DisplayFormat.FormatString = "0.00"
        Me.SMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SMTRS.FieldName = "MTRS"
        Me.SMTRS.Name = "SMTRS"
        Me.SMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.SMTRS.Visible = True
        Me.SMTRS.VisibleIndex = 3
        Me.SMTRS.Width = 100
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.GRIDORDERDETAILS)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(544, 502)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Sale Order Details"
        '
        'GRIDORDERDETAILS
        '
        Me.GRIDORDERDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDORDERDETAILS.Location = New System.Drawing.Point(6, 6)
        Me.GRIDORDERDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDORDERDETAILS.MainView = Me.GRIDORDER
        Me.GRIDORDERDETAILS.Name = "GRIDORDERDETAILS"
        Me.GRIDORDERDETAILS.Size = New System.Drawing.Size(532, 490)
        Me.GRIDORDERDETAILS.TabIndex = 451
        Me.GRIDORDERDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDORDER})
        '
        'GRIDORDER
        '
        Me.GRIDORDER.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDORDER.Appearance.HeaderPanel.Options.UseFont = True
        Me.GRIDORDER.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDORDER.Appearance.Row.Options.UseFont = True
        Me.GRIDORDER.Appearance.Row.Options.UseTextOptions = True
        Me.GRIDORDER.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GRIDORDER.Appearance.ViewCaption.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDORDER.Appearance.ViewCaption.Options.UseFont = True
        Me.GRIDORDER.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ONAME, Me.OORDERNO, Me.ODUEDATE, Me.OUNIT, Me.OPCS, Me.OMTRS})
        Me.GRIDORDER.GridControl = Me.GRIDORDERDETAILS
        Me.GRIDORDER.Name = "GRIDORDER"
        Me.GRIDORDER.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDORDER.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDORDER.OptionsBehavior.Editable = False
        Me.GRIDORDER.OptionsMenu.EnableColumnMenu = False
        Me.GRIDORDER.OptionsView.ColumnAutoWidth = False
        Me.GRIDORDER.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDORDER.OptionsView.ShowAutoFilterRow = True
        Me.GRIDORDER.OptionsView.ShowFooter = True
        Me.GRIDORDER.OptionsView.ShowGroupPanel = False
        '
        'ONAME
        '
        Me.ONAME.AppearanceCell.BackColor = System.Drawing.Color.LemonChiffon
        Me.ONAME.AppearanceCell.Options.UseBackColor = True
        Me.ONAME.Caption = "Name"
        Me.ONAME.FieldName = "NAME"
        Me.ONAME.Name = "ONAME"
        Me.ONAME.Visible = True
        Me.ONAME.VisibleIndex = 0
        Me.ONAME.Width = 185
        '
        'OORDERNO
        '
        Me.OORDERNO.AppearanceCell.BackColor = System.Drawing.Color.LemonChiffon
        Me.OORDERNO.AppearanceCell.Options.UseBackColor = True
        Me.OORDERNO.Caption = "Order No"
        Me.OORDERNO.FieldName = "ORDERNO"
        Me.OORDERNO.Name = "OORDERNO"
        Me.OORDERNO.Visible = True
        Me.OORDERNO.VisibleIndex = 1
        Me.OORDERNO.Width = 65
        '
        'ODUEDATE
        '
        Me.ODUEDATE.AppearanceCell.BackColor = System.Drawing.Color.LemonChiffon
        Me.ODUEDATE.AppearanceCell.Options.UseBackColor = True
        Me.ODUEDATE.Caption = "Due Date"
        Me.ODUEDATE.FieldName = "DUEDATE"
        Me.ODUEDATE.Name = "ODUEDATE"
        Me.ODUEDATE.Visible = True
        Me.ODUEDATE.VisibleIndex = 2
        '
        'OUNIT
        '
        Me.OUNIT.AppearanceCell.BackColor = System.Drawing.Color.LemonChiffon
        Me.OUNIT.AppearanceCell.Options.UseBackColor = True
        Me.OUNIT.Caption = "Unit"
        Me.OUNIT.FieldName = "UNIT"
        Me.OUNIT.Name = "OUNIT"
        Me.OUNIT.Visible = True
        Me.OUNIT.VisibleIndex = 3
        Me.OUNIT.Width = 60
        '
        'OPCS
        '
        Me.OPCS.AppearanceCell.BackColor = System.Drawing.Color.LemonChiffon
        Me.OPCS.AppearanceCell.Options.UseBackColor = True
        Me.OPCS.Caption = "Pcs"
        Me.OPCS.DisplayFormat.FormatString = "0"
        Me.OPCS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.OPCS.FieldName = "PCS"
        Me.OPCS.Name = "OPCS"
        Me.OPCS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.OPCS.Visible = True
        Me.OPCS.VisibleIndex = 4
        Me.OPCS.Width = 40
        '
        'OMTRS
        '
        Me.OMTRS.AppearanceCell.BackColor = System.Drawing.Color.LemonChiffon
        Me.OMTRS.AppearanceCell.Options.UseBackColor = True
        Me.OMTRS.Caption = "Mtrs"
        Me.OMTRS.DisplayFormat.FormatString = "0.00"
        Me.OMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.OMTRS.FieldName = "MTRS"
        Me.OMTRS.Name = "OMTRS"
        Me.OMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.OMTRS.Visible = True
        Me.OMTRS.VisibleIndex = 5
        Me.OMTRS.Width = 60
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.GRIDGDNDETAILS)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(544, 502)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Challan Details"
        '
        'GRIDGDNDETAILS
        '
        Me.GRIDGDNDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDGDNDETAILS.Location = New System.Drawing.Point(6, 6)
        Me.GRIDGDNDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDGDNDETAILS.MainView = Me.GRIDGDN
        Me.GRIDGDNDETAILS.Name = "GRIDGDNDETAILS"
        Me.GRIDGDNDETAILS.Size = New System.Drawing.Size(535, 490)
        Me.GRIDGDNDETAILS.TabIndex = 451
        Me.GRIDGDNDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDGDN})
        '
        'GRIDGDN
        '
        Me.GRIDGDN.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDGDN.Appearance.HeaderPanel.Options.UseFont = True
        Me.GRIDGDN.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDGDN.Appearance.Row.Options.UseFont = True
        Me.GRIDGDN.Appearance.Row.Options.UseTextOptions = True
        Me.GRIDGDN.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GRIDGDN.Appearance.ViewCaption.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDGDN.Appearance.ViewCaption.Options.UseFont = True
        Me.GRIDGDN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CNAME, Me.CMTRS, Me.CCHALLANNO, Me.GCHALLANDATE, Me.CINVNO})
        Me.GRIDGDN.GridControl = Me.GRIDGDNDETAILS
        Me.GRIDGDN.Name = "GRIDGDN"
        Me.GRIDGDN.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDGDN.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDGDN.OptionsBehavior.Editable = False
        Me.GRIDGDN.OptionsMenu.EnableColumnMenu = False
        Me.GRIDGDN.OptionsView.ColumnAutoWidth = False
        Me.GRIDGDN.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDGDN.OptionsView.ShowAutoFilterRow = True
        Me.GRIDGDN.OptionsView.ShowFooter = True
        Me.GRIDGDN.OptionsView.ShowGroupPanel = False
        '
        'CNAME
        '
        Me.CNAME.AppearanceCell.BackColor = System.Drawing.Color.LightGreen
        Me.CNAME.AppearanceCell.Options.UseBackColor = True
        Me.CNAME.Caption = "Party Name"
        Me.CNAME.FieldName = "NAME"
        Me.CNAME.Name = "CNAME"
        Me.CNAME.Visible = True
        Me.CNAME.VisibleIndex = 0
        Me.CNAME.Width = 190
        '
        'CMTRS
        '
        Me.CMTRS.AppearanceCell.BackColor = System.Drawing.Color.LightGreen
        Me.CMTRS.AppearanceCell.Options.UseBackColor = True
        Me.CMTRS.Caption = "Mtrs"
        Me.CMTRS.DisplayFormat.FormatString = "0.00"
        Me.CMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CMTRS.FieldName = "MTRS"
        Me.CMTRS.Name = "CMTRS"
        Me.CMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.CMTRS.Visible = True
        Me.CMTRS.VisibleIndex = 1
        '
        'CCHALLANNO
        '
        Me.CCHALLANNO.AppearanceCell.BackColor = System.Drawing.Color.LightGreen
        Me.CCHALLANNO.AppearanceCell.Options.UseBackColor = True
        Me.CCHALLANNO.Caption = "Ch No"
        Me.CCHALLANNO.FieldName = "CHALLANNO"
        Me.CCHALLANNO.Name = "CCHALLANNO"
        Me.CCHALLANNO.Visible = True
        Me.CCHALLANNO.VisibleIndex = 2
        '
        'GCHALLANDATE
        '
        Me.GCHALLANDATE.AppearanceCell.BackColor = System.Drawing.Color.LightGreen
        Me.GCHALLANDATE.AppearanceCell.Options.UseBackColor = True
        Me.GCHALLANDATE.Caption = "Ch Dt"
        Me.GCHALLANDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GCHALLANDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GCHALLANDATE.FieldName = "GDNDATE"
        Me.GCHALLANDATE.Name = "GCHALLANDATE"
        Me.GCHALLANDATE.Visible = True
        Me.GCHALLANDATE.VisibleIndex = 3
        '
        'CINVNO
        '
        Me.CINVNO.AppearanceCell.BackColor = System.Drawing.Color.LightGreen
        Me.CINVNO.AppearanceCell.Options.UseBackColor = True
        Me.CINVNO.Caption = "Inv No"
        Me.CINVNO.FieldName = "INVNO"
        Me.CINVNO.Name = "CINVNO"
        Me.CINVNO.Visible = True
        Me.CINVNO.VisibleIndex = 4
        '
        'GRIDDESIGN
        '
        Me.GRIDDESIGN.AllowUserToAddRows = False
        Me.GRIDDESIGN.AllowUserToDeleteRows = False
        Me.GRIDDESIGN.AllowUserToResizeColumns = False
        Me.GRIDDESIGN.AllowUserToResizeRows = False
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.Empty
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDDESIGN.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle9
        Me.GRIDDESIGN.BackgroundColor = System.Drawing.Color.White
        Me.GRIDDESIGN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDDESIGN.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDDESIGN.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.GRIDDESIGN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDDESIGN.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GDETAILS})
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDDESIGN.DefaultCellStyle = DataGridViewCellStyle11
        Me.GRIDDESIGN.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDDESIGN.Location = New System.Drawing.Point(12, 12)
        Me.GRIDDESIGN.MultiSelect = False
        Me.GRIDDESIGN.Name = "GRIDDESIGN"
        Me.GRIDDESIGN.ReadOnly = True
        Me.GRIDDESIGN.RowHeadersVisible = False
        Me.GRIDDESIGN.RowHeadersWidth = 30
        Me.GRIDDESIGN.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDDESIGN.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.GRIDDESIGN.RowTemplate.Height = 20
        Me.GRIDDESIGN.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDDESIGN.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDDESIGN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDDESIGN.Size = New System.Drawing.Size(695, 530)
        Me.GRIDDESIGN.TabIndex = 260
        Me.GRIDDESIGN.TabStop = False
        '
        'GDETAILS
        '
        Me.GDETAILS.HeaderText = "."
        Me.GDETAILS.Name = "GDETAILS"
        Me.GDETAILS.ReadOnly = True
        Me.GDETAILS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDETAILS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDETAILS.Width = 250
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.Black
        Me.CMDREFRESH.Location = New System.Drawing.Point(560, 548)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREFRESH.TabIndex = 259
        Me.CMDREFRESH.Text = "&Refresh"
        Me.CMDREFRESH.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(646, 548)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 2
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'DesignHistory
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1286, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "DesignHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Design History"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.GRIDSTOCKDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDSTOCK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.GRIDORDERDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDORDER, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.GRIDGDNDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDGDN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDDESIGN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDREFRESH As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents GRIDDESIGN As DataGridView
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Private WithEvents GRIDSTOCKDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDSTOCK As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SPIECETYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SUNIT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TabPage2 As TabPage
    Private WithEvents GRIDORDERDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDORDER As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ONAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents OORDERNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ODUEDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents OUNIT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents OPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents OMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TabPage3 As TabPage
    Private WithEvents GRIDGDNDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDGDN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CCHALLANNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHALLANDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CINVNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDETAILS As DataGridViewTextBoxColumn
End Class
