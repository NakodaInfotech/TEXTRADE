<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LRStockValuation
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.CMBITEMNAME = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CMDWHATSAPP = New System.Windows.Forms.Button()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.tbitem = New System.Windows.Forms.TabPage()
        Me.GRIDSO = New System.Windows.Forms.DataGridView()
        Me.GSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLRTOTAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMTRS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMTRSTOTAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAMT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTOTAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRUNBAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDAYS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GINT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFINAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GARATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GPITEM = New System.Windows.Forms.GroupBox()
        Me.CHKSELECTITEM = New System.Windows.Forms.CheckBox()
        Me.GRIDBILLDETAILSITEM = New DevExpress.XtraGrid.GridControl()
        Me.GRIDBILLITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCHKITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCATEGORY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPPARTYNAME = New System.Windows.Forms.GroupBox()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCITYNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSTATENAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAREA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKSELECTALL = New System.Windows.Forms.CheckBox()
        Me.CMDEXPORT = New System.Windows.Forms.Button()
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.tbitem.SuspendLayout()
        CType(Me.GRIDSO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        Me.GPITEM.SuspendLayout()
        CType(Me.GRIDBILLDETAILSITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDBILLITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GPPARTYNAME.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.CMBNAME)
        Me.BlendPanel1.Controls.Add(Me.CMBITEMNAME)
        Me.BlendPanel1.Controls.Add(Me.Label10)
        Me.BlendPanel1.Controls.Add(Me.CMDWHATSAPP)
        Me.BlendPanel1.Controls.Add(Me.TabControl2)
        Me.BlendPanel1.Controls.Add(Me.CMDEXPORT)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1334, 674)
        Me.BlendPanel1.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(178, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 15)
        Me.Label9.TabIndex = 765
        Me.Label9.Text = "Name"
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(219, 10)
        Me.CMBNAME.MaxDropDownItems = 14
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(230, 23)
        Me.CMBNAME.TabIndex = 0
        '
        'CMBITEMNAME
        '
        Me.CMBITEMNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBITEMNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBITEMNAME.BackColor = System.Drawing.Color.White
        Me.CMBITEMNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBITEMNAME.FormattingEnabled = True
        Me.CMBITEMNAME.Location = New System.Drawing.Point(559, 10)
        Me.CMBITEMNAME.MaxDropDownItems = 14
        Me.CMBITEMNAME.Name = "CMBITEMNAME"
        Me.CMBITEMNAME.Size = New System.Drawing.Size(230, 23)
        Me.CMBITEMNAME.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(490, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(65, 15)
        Me.Label10.TabIndex = 766
        Me.Label10.Text = "Item Name"
        '
        'CMDWHATSAPP
        '
        Me.CMDWHATSAPP.BackColor = System.Drawing.Color.Transparent
        Me.CMDWHATSAPP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDWHATSAPP.FlatAppearance.BorderSize = 0
        Me.CMDWHATSAPP.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDWHATSAPP.ForeColor = System.Drawing.Color.Black
        Me.CMDWHATSAPP.Location = New System.Drawing.Point(473, 638)
        Me.CMDWHATSAPP.Name = "CMDWHATSAPP"
        Me.CMDWHATSAPP.Size = New System.Drawing.Size(80, 28)
        Me.CMDWHATSAPP.TabIndex = 22
        Me.CMDWHATSAPP.Text = "&Whatsapp"
        Me.CMDWHATSAPP.UseVisualStyleBackColor = False
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.tbitem)
        Me.TabControl2.Controls.Add(Me.TabPage1)
        Me.TabControl2.Location = New System.Drawing.Point(6, 14)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(1316, 618)
        Me.TabControl2.TabIndex = 6
        '
        'tbitem
        '
        Me.tbitem.AutoScroll = True
        Me.tbitem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.tbitem.Controls.Add(Me.GRIDSO)
        Me.tbitem.Location = New System.Drawing.Point(4, 24)
        Me.tbitem.Name = "tbitem"
        Me.tbitem.Padding = New System.Windows.Forms.Padding(3)
        Me.tbitem.Size = New System.Drawing.Size(1308, 590)
        Me.tbitem.TabIndex = 0
        Me.tbitem.Text = "Item Details"
        '
        'GRIDSO
        '
        Me.GRIDSO.AllowUserToAddRows = False
        Me.GRIDSO.AllowUserToDeleteRows = False
        Me.GRIDSO.AllowUserToResizeColumns = False
        Me.GRIDSO.AllowUserToResizeRows = False
        Me.GRIDSO.BackgroundColor = System.Drawing.Color.White
        Me.GRIDSO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDSO.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDSO.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDSO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDSO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GNAME, Me.GLR, Me.GLRTOTAL, Me.GMTRS, Me.GMTRSTOTAL, Me.GRATE, Me.GAMT, Me.GGST, Me.GTOTAL, Me.GRUNBAL, Me.GDATE, Me.GDAYS, Me.GINT, Me.GFINAL, Me.GARATE})
        Me.GRIDSO.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDSO.Location = New System.Drawing.Point(6, 3)
        Me.GRIDSO.MultiSelect = False
        Me.GRIDSO.Name = "GRIDSO"
        Me.GRIDSO.RowHeadersVisible = False
        Me.GRIDSO.RowHeadersWidth = 30
        Me.GRIDSO.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.GRIDSO.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.GRIDSO.RowTemplate.Height = 20
        Me.GRIDSO.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDSO.Size = New System.Drawing.Size(1475, 570)
        Me.GRIDSO.TabIndex = 0
        '
        'GSRNO
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GSRNO.DefaultCellStyle = DataGridViewCellStyle8
        Me.GSRNO.HeaderText = "Sr"
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.ReadOnly = True
        Me.GSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSRNO.Width = 30
        '
        'GNAME
        '
        Me.GNAME.HeaderText = "Name"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.ReadOnly = True
        Me.GNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GNAME.Width = 220
        '
        'GLR
        '
        Me.GLR.HeaderText = "LR"
        Me.GLR.Name = "GLR"
        Me.GLR.ReadOnly = True
        Me.GLR.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GLR.Width = 60
        '
        'GLRTOTAL
        '
        Me.GLRTOTAL.HeaderText = "Cum"
        Me.GLRTOTAL.Name = "GLRTOTAL"
        Me.GLRTOTAL.ReadOnly = True
        Me.GLRTOTAL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLRTOTAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GLRTOTAL.Width = 60
        '
        'GMTRS
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.NullValue = Nothing
        Me.GMTRS.DefaultCellStyle = DataGridViewCellStyle9
        Me.GMTRS.HeaderText = "Mtrs"
        Me.GMTRS.Name = "GMTRS"
        Me.GMTRS.ReadOnly = True
        Me.GMTRS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMTRS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GMTRSTOTAL
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GMTRSTOTAL.DefaultCellStyle = DataGridViewCellStyle10
        Me.GMTRSTOTAL.HeaderText = "Cum"
        Me.GMTRSTOTAL.Name = "GMTRSTOTAL"
        Me.GMTRSTOTAL.ReadOnly = True
        Me.GMTRSTOTAL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMTRSTOTAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GRATE
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GRATE.DefaultCellStyle = DataGridViewCellStyle11
        Me.GRATE.HeaderText = "Rate"
        Me.GRATE.Name = "GRATE"
        Me.GRATE.ReadOnly = True
        Me.GRATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GRATE.Width = 70
        '
        'GAMT
        '
        Me.GAMT.HeaderText = "Amount"
        Me.GAMT.Name = "GAMT"
        Me.GAMT.ReadOnly = True
        Me.GAMT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GAMT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GGST
        '
        Me.GGST.HeaderText = "GST"
        Me.GGST.Name = "GGST"
        Me.GGST.ReadOnly = True
        Me.GGST.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGST.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GGST.Width = 80
        '
        'GTOTAL
        '
        Me.GTOTAL.HeaderText = "Total Amt"
        Me.GTOTAL.Name = "GTOTAL"
        Me.GTOTAL.ReadOnly = True
        Me.GTOTAL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTOTAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GRUNBAL
        '
        Me.GRUNBAL.HeaderText = "Cum"
        Me.GRUNBAL.Name = "GRUNBAL"
        Me.GRUNBAL.ReadOnly = True
        Me.GRUNBAL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRUNBAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GDATE
        '
        Me.GDATE.HeaderText = "Date"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.ReadOnly = True
        Me.GDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDATE.Width = 80
        '
        'GDAYS
        '
        Me.GDAYS.HeaderText = "Days"
        Me.GDAYS.Name = "GDAYS"
        Me.GDAYS.ReadOnly = True
        Me.GDAYS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDAYS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDAYS.Width = 60
        '
        'GINT
        '
        Me.GINT.HeaderText = "Interest"
        Me.GINT.Name = "GINT"
        Me.GINT.ReadOnly = True
        Me.GINT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GINT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GFINAL
        '
        Me.GFINAL.HeaderText = "Final Amt"
        Me.GFINAL.Name = "GFINAL"
        Me.GFINAL.ReadOnly = True
        Me.GFINAL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GFINAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GARATE
        '
        Me.GARATE.HeaderText = "A Rate"
        Me.GARATE.Name = "GARATE"
        Me.GARATE.ReadOnly = True
        Me.GARATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GARATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GARATE.Width = 80
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.GPITEM)
        Me.TabPage1.Controls.Add(Me.GPPARTYNAME)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1308, 592)
        Me.TabPage1.TabIndex = 1
        Me.TabPage1.Text = "Filters"
        '
        'GPITEM
        '
        Me.GPITEM.BackColor = System.Drawing.Color.Transparent
        Me.GPITEM.Controls.Add(Me.CHKSELECTITEM)
        Me.GPITEM.Controls.Add(Me.GRIDBILLDETAILSITEM)
        Me.GPITEM.Location = New System.Drawing.Point(10, 15)
        Me.GPITEM.Name = "GPITEM"
        Me.GPITEM.Size = New System.Drawing.Size(464, 498)
        Me.GPITEM.TabIndex = 0
        Me.GPITEM.TabStop = False
        Me.GPITEM.Text = "Item Name"
        '
        'CHKSELECTITEM
        '
        Me.CHKSELECTITEM.AutoSize = True
        Me.CHKSELECTITEM.BackColor = System.Drawing.Color.Transparent
        Me.CHKSELECTITEM.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSELECTITEM.ForeColor = System.Drawing.Color.Black
        Me.CHKSELECTITEM.Location = New System.Drawing.Point(18, 22)
        Me.CHKSELECTITEM.Name = "CHKSELECTITEM"
        Me.CHKSELECTITEM.Size = New System.Drawing.Size(77, 18)
        Me.CHKSELECTITEM.TabIndex = 0
        Me.CHKSELECTITEM.Text = "Select All"
        Me.CHKSELECTITEM.UseVisualStyleBackColor = False
        '
        'GRIDBILLDETAILSITEM
        '
        Me.GRIDBILLDETAILSITEM.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILLDETAILSITEM.Location = New System.Drawing.Point(9, 46)
        Me.GRIDBILLDETAILSITEM.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDBILLDETAILSITEM.MainView = Me.GRIDBILLITEM
        Me.GRIDBILLDETAILSITEM.Name = "GRIDBILLDETAILSITEM"
        Me.GRIDBILLDETAILSITEM.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit4})
        Me.GRIDBILLDETAILSITEM.Size = New System.Drawing.Size(449, 425)
        Me.GRIDBILLDETAILSITEM.TabIndex = 1
        Me.GRIDBILLDETAILSITEM.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDBILLITEM})
        '
        'GRIDBILLITEM
        '
        Me.GRIDBILLITEM.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILLITEM.Appearance.Row.Options.UseFont = True
        Me.GRIDBILLITEM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHKITEM, Me.GridColumn1, Me.GCATEGORY})
        Me.GRIDBILLITEM.GridControl = Me.GRIDBILLDETAILSITEM
        Me.GRIDBILLITEM.Name = "GRIDBILLITEM"
        Me.GRIDBILLITEM.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDBILLITEM.OptionsView.ColumnAutoWidth = False
        Me.GRIDBILLITEM.OptionsView.ShowAutoFilterRow = True
        Me.GRIDBILLITEM.OptionsView.ShowGroupPanel = False
        '
        'GCHKITEM
        '
        Me.GCHKITEM.ColumnEdit = Me.RepositoryItemCheckEdit4
        Me.GCHKITEM.FieldName = "CHK"
        Me.GCHKITEM.Name = "GCHKITEM"
        Me.GCHKITEM.OptionsColumn.ShowCaption = False
        Me.GCHKITEM.Visible = True
        Me.GCHKITEM.VisibleIndex = 0
        Me.GCHKITEM.Width = 35
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        Me.RepositoryItemCheckEdit4.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Item Name"
        Me.GridColumn1.FieldName = "ITEMNAME"
        Me.GridColumn1.ImageOptions.ImageIndex = 0
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 250
        '
        'GCATEGORY
        '
        Me.GCATEGORY.Caption = "Category"
        Me.GCATEGORY.FieldName = "CATEGORY"
        Me.GCATEGORY.Name = "GCATEGORY"
        Me.GCATEGORY.Visible = True
        Me.GCATEGORY.VisibleIndex = 2
        '
        'GPPARTYNAME
        '
        Me.GPPARTYNAME.BackColor = System.Drawing.Color.Transparent
        Me.GPPARTYNAME.Controls.Add(Me.gridbilldetails)
        Me.GPPARTYNAME.Controls.Add(Me.CHKSELECTALL)
        Me.GPPARTYNAME.Location = New System.Drawing.Point(480, 15)
        Me.GPPARTYNAME.Name = "GPPARTYNAME"
        Me.GPPARTYNAME.Size = New System.Drawing.Size(615, 498)
        Me.GPPARTYNAME.TabIndex = 1
        Me.GPPARTYNAME.TabStop = False
        Me.GPPARTYNAME.Text = "Party Name"
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(11, 46)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.gridbilldetails.Size = New System.Drawing.Size(589, 425)
        Me.gridbilldetails.TabIndex = 2
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHK, Me.GridColumn2, Me.GCITYNAME, Me.GSTATENAME, Me.GAREA})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GCHK
        '
        Me.GCHK.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GCHK.FieldName = "CHK"
        Me.GCHK.Name = "GCHK"
        Me.GCHK.OptionsColumn.ShowCaption = False
        Me.GCHK.Visible = True
        Me.GCHK.VisibleIndex = 0
        Me.GCHK.Width = 35
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Name"
        Me.GridColumn2.FieldName = "NAME"
        Me.GridColumn2.ImageOptions.ImageIndex = 0
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 230
        '
        'GCITYNAME
        '
        Me.GCITYNAME.Caption = "City Name"
        Me.GCITYNAME.FieldName = "CITY"
        Me.GCITYNAME.Name = "GCITYNAME"
        Me.GCITYNAME.OptionsColumn.AllowEdit = False
        Me.GCITYNAME.Visible = True
        Me.GCITYNAME.VisibleIndex = 2
        '
        'GSTATENAME
        '
        Me.GSTATENAME.Caption = "State Name"
        Me.GSTATENAME.FieldName = "STATENAME"
        Me.GSTATENAME.Name = "GSTATENAME"
        Me.GSTATENAME.OptionsColumn.AllowEdit = False
        Me.GSTATENAME.Visible = True
        Me.GSTATENAME.VisibleIndex = 3
        Me.GSTATENAME.Width = 80
        '
        'GAREA
        '
        Me.GAREA.Caption = "Area"
        Me.GAREA.FieldName = "AREA"
        Me.GAREA.Name = "GAREA"
        Me.GAREA.OptionsColumn.AllowEdit = False
        Me.GAREA.Visible = True
        Me.GAREA.VisibleIndex = 4
        Me.GAREA.Width = 100
        '
        'CHKSELECTALL
        '
        Me.CHKSELECTALL.AutoSize = True
        Me.CHKSELECTALL.BackColor = System.Drawing.Color.Transparent
        Me.CHKSELECTALL.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSELECTALL.ForeColor = System.Drawing.Color.Black
        Me.CHKSELECTALL.Location = New System.Drawing.Point(18, 22)
        Me.CHKSELECTALL.Name = "CHKSELECTALL"
        Me.CHKSELECTALL.Size = New System.Drawing.Size(77, 18)
        Me.CHKSELECTALL.TabIndex = 0
        Me.CHKSELECTALL.Text = "Select All"
        Me.CHKSELECTALL.UseVisualStyleBackColor = False
        '
        'CMDEXPORT
        '
        Me.CMDEXPORT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXPORT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXPORT.FlatAppearance.BorderSize = 0
        Me.CMDEXPORT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXPORT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXPORT.Location = New System.Drawing.Point(559, 638)
        Me.CMDEXPORT.Name = "CMDEXPORT"
        Me.CMDEXPORT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXPORT.TabIndex = 4
        Me.CMDEXPORT.Text = "&Export"
        Me.CMDEXPORT.UseVisualStyleBackColor = False
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.Black
        Me.CMDREFRESH.Location = New System.Drawing.Point(645, 638)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREFRESH.TabIndex = 1
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
        Me.cmdexit.Location = New System.Drawing.Point(731, 638)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 3
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'LRStockValuation
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1334, 674)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "LRStockValuation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "LR Stock Valuation"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.TabControl2.ResumeLayout(False)
        Me.tbitem.ResumeLayout(False)
        CType(Me.GRIDSO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.GPITEM.ResumeLayout(False)
        Me.GPITEM.PerformLayout()
        CType(Me.GRIDBILLDETAILSITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDBILLITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GPPARTYNAME.ResumeLayout(False)
        Me.GPPARTYNAME.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDWHATSAPP As Button
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents tbitem As TabPage
    Friend WithEvents GRIDSO As DataGridView
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents GPITEM As GroupBox
    Friend WithEvents CHKSELECTITEM As CheckBox
    Private WithEvents GRIDBILLDETAILSITEM As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDBILLITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHKITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCATEGORY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPPARTYNAME As GroupBox
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCITYNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSTATENAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAREA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKSELECTALL As CheckBox
    Friend WithEvents Label9 As Label
    Friend WithEvents CMBNAME As ComboBox
    Friend WithEvents CMBITEMNAME As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents CMDEXPORT As Button
    Friend WithEvents CMDREFRESH As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents GSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GNAME As DataGridViewTextBoxColumn
    Friend WithEvents GLR As DataGridViewTextBoxColumn
    Friend WithEvents GLRTOTAL As DataGridViewTextBoxColumn
    Friend WithEvents GMTRS As DataGridViewTextBoxColumn
    Friend WithEvents GMTRSTOTAL As DataGridViewTextBoxColumn
    Friend WithEvents GRATE As DataGridViewTextBoxColumn
    Friend WithEvents GAMT As DataGridViewTextBoxColumn
    Friend WithEvents GGST As DataGridViewTextBoxColumn
    Friend WithEvents GTOTAL As DataGridViewTextBoxColumn
    Friend WithEvents GRUNBAL As DataGridViewTextBoxColumn
    Friend WithEvents GDATE As DataGridViewTextBoxColumn
    Friend WithEvents GDAYS As DataGridViewTextBoxColumn
    Friend WithEvents GINT As DataGridViewTextBoxColumn
    Friend WithEvents GFINAL As DataGridViewTextBoxColumn
    Friend WithEvents GARATE As DataGridViewTextBoxColumn
End Class
