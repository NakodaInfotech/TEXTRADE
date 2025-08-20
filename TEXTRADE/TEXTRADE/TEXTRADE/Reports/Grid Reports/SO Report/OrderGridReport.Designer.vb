<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderGridReport
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.tbitem = New System.Windows.Forms.TabPage()
        Me.GRIDSO = New System.Windows.Forms.DataGridView()
        Me.GITEMNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSONO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSODATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAGENTNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNOTE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMILLNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTPCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBALPCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDAYS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GPORDERNO = New System.Windows.Forms.GroupBox()
        Me.CHKSELECTORDER = New System.Windows.Forms.CheckBox()
        Me.GRIDBILLDETAILSORDER = New DevExpress.XtraGrid.GridControl()
        Me.GRIDBILLORDER = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCHKORDER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GORDERNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPITEM = New System.Windows.Forms.GroupBox()
        Me.CHKSELECTITEM = New System.Windows.Forms.CheckBox()
        Me.GRIDBILLDETAILSITEM = New DevExpress.XtraGrid.GridControl()
        Me.GRIDBILLITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCHKITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCATEGORY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPPARTYNAME = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.RBAGENT = New System.Windows.Forms.RadioButton()
        Me.RBACCOUNT = New System.Windows.Forms.RadioButton()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGROUPNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCITYNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSTATENAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAREA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSALESMAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKSELECTALL = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtto = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtfrom = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkdate = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.CMBAGENT = New System.Windows.Forms.ComboBox()
        Me.CMBCATEGORY = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RDBALL = New System.Windows.Forms.RadioButton()
        Me.RDBPENDING = New System.Windows.Forms.RadioButton()
        Me.RDBCOMPLETE = New System.Windows.Forms.RadioButton()
        Me.RDBCLOSED = New System.Windows.Forms.RadioButton()
        Me.CMDEXPORT = New System.Windows.Forms.Button()
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.tbitem.SuspendLayout()
        CType(Me.GRIDSO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        Me.GPORDERNO.SuspendLayout()
        CType(Me.GRIDBILLDETAILSORDER, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDBILLORDER, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GPITEM.SuspendLayout()
        CType(Me.GRIDBILLDETAILSITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDBILLITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GPPARTYNAME.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TabControl2)
        Me.BlendPanel1.Controls.Add(Me.GroupBox1)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.CMBNAME)
        Me.BlendPanel1.Controls.Add(Me.CMBAGENT)
        Me.BlendPanel1.Controls.Add(Me.CMBCATEGORY)
        Me.BlendPanel1.Controls.Add(Me.Label10)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.GroupBox2)
        Me.BlendPanel1.Controls.Add(Me.CMDEXPORT)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1594, 692)
        Me.BlendPanel1.TabIndex = 0
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.tbitem)
        Me.TabControl2.Controls.Add(Me.TabPage1)
        Me.TabControl2.Location = New System.Drawing.Point(12, 94)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(1570, 560)
        Me.TabControl2.TabIndex = 770
        '
        'tbitem
        '
        Me.tbitem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.tbitem.Controls.Add(Me.GRIDSO)
        Me.tbitem.Location = New System.Drawing.Point(4, 24)
        Me.tbitem.Name = "tbitem"
        Me.tbitem.Padding = New System.Windows.Forms.Padding(3)
        Me.tbitem.Size = New System.Drawing.Size(1562, 532)
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
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDSO.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDSO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDSO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GITEMNAME, Me.GSONO, Me.GSODATE, Me.GNAME, Me.GAGENTNAME, Me.GNOTE, Me.GMILLNAME, Me.GPCS, Me.GOUTPCS, Me.GBALPCS, Me.GRATE, Me.GDAYS})
        Me.GRIDSO.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDSO.Location = New System.Drawing.Point(6, 3)
        Me.GRIDSO.MultiSelect = False
        Me.GRIDSO.Name = "GRIDSO"
        Me.GRIDSO.RowHeadersVisible = False
        Me.GRIDSO.RowHeadersWidth = 30
        Me.GRIDSO.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.GRIDSO.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.GRIDSO.RowTemplate.Height = 20
        Me.GRIDSO.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDSO.Size = New System.Drawing.Size(1550, 523)
        Me.GRIDSO.TabIndex = 0
        '
        'GITEMNAME
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GITEMNAME.DefaultCellStyle = DataGridViewCellStyle2
        Me.GITEMNAME.HeaderText = "Item Name"
        Me.GITEMNAME.Name = "GITEMNAME"
        Me.GITEMNAME.ReadOnly = True
        Me.GITEMNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GITEMNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GITEMNAME.Width = 200
        '
        'GSONO
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GSONO.DefaultCellStyle = DataGridViewCellStyle3
        Me.GSONO.HeaderText = "SO No"
        Me.GSONO.Name = "GSONO"
        Me.GSONO.ReadOnly = True
        Me.GSONO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSONO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSONO.Width = 80
        '
        'GSODATE
        '
        Me.GSODATE.HeaderText = "SO Date"
        Me.GSODATE.Name = "GSODATE"
        Me.GSODATE.ReadOnly = True
        Me.GSODATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSODATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSODATE.Width = 80
        '
        'GNAME
        '
        Me.GNAME.HeaderText = "Name"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.ReadOnly = True
        Me.GNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GNAME.Width = 250
        '
        'GAGENTNAME
        '
        Me.GAGENTNAME.HeaderText = "Agent Name"
        Me.GAGENTNAME.Name = "GAGENTNAME"
        Me.GAGENTNAME.ReadOnly = True
        Me.GAGENTNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GAGENTNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GAGENTNAME.Width = 200
        '
        'GNOTE
        '
        Me.GNOTE.HeaderText = "Note"
        Me.GNOTE.Name = "GNOTE"
        Me.GNOTE.ReadOnly = True
        Me.GNOTE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GNOTE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GNOTE.Width = 180
        '
        'GMILLNAME
        '
        Me.GMILLNAME.HeaderText = "Mill Name"
        Me.GMILLNAME.Name = "GMILLNAME"
        Me.GMILLNAME.ReadOnly = True
        Me.GMILLNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMILLNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMILLNAME.Width = 190
        '
        'GPCS
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.NullValue = Nothing
        Me.GPCS.DefaultCellStyle = DataGridViewCellStyle4
        Me.GPCS.HeaderText = "Pcs"
        Me.GPCS.Name = "GPCS"
        Me.GPCS.ReadOnly = True
        Me.GPCS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPCS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPCS.Width = 60
        '
        'GOUTPCS
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GOUTPCS.DefaultCellStyle = DataGridViewCellStyle5
        Me.GOUTPCS.HeaderText = "Del Pcs"
        Me.GOUTPCS.Name = "GOUTPCS"
        Me.GOUTPCS.ReadOnly = True
        Me.GOUTPCS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOUTPCS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOUTPCS.Width = 70
        '
        'GBALPCS
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GBALPCS.DefaultCellStyle = DataGridViewCellStyle6
        Me.GBALPCS.HeaderText = "Bal Pcs"
        Me.GBALPCS.Name = "GBALPCS"
        Me.GBALPCS.ReadOnly = True
        Me.GBALPCS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBALPCS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBALPCS.Width = 80
        '
        'GRATE
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GRATE.DefaultCellStyle = DataGridViewCellStyle7
        Me.GRATE.HeaderText = "Rate"
        Me.GRATE.Name = "GRATE"
        Me.GRATE.ReadOnly = True
        Me.GRATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GRATE.Width = 65
        '
        'GDAYS
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GDAYS.DefaultCellStyle = DataGridViewCellStyle8
        Me.GDAYS.HeaderText = "Cr Days"
        Me.GDAYS.Name = "GDAYS"
        Me.GDAYS.ReadOnly = True
        Me.GDAYS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDAYS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDAYS.Width = 75
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TabPage1.Controls.Add(Me.GPORDERNO)
        Me.TabPage1.Controls.Add(Me.GPITEM)
        Me.TabPage1.Controls.Add(Me.GPPARTYNAME)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1562, 532)
        Me.TabPage1.TabIndex = 1
        Me.TabPage1.Text = "Filters"
        '
        'GPORDERNO
        '
        Me.GPORDERNO.BackColor = System.Drawing.Color.Transparent
        Me.GPORDERNO.Controls.Add(Me.CHKSELECTORDER)
        Me.GPORDERNO.Controls.Add(Me.GRIDBILLDETAILSORDER)
        Me.GPORDERNO.Location = New System.Drawing.Point(785, 21)
        Me.GPORDERNO.Name = "GPORDERNO"
        Me.GPORDERNO.Size = New System.Drawing.Size(221, 492)
        Me.GPORDERNO.TabIndex = 762
        Me.GPORDERNO.TabStop = False
        Me.GPORDERNO.Text = "Order No"
        '
        'CHKSELECTORDER
        '
        Me.CHKSELECTORDER.AutoSize = True
        Me.CHKSELECTORDER.BackColor = System.Drawing.Color.Transparent
        Me.CHKSELECTORDER.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSELECTORDER.ForeColor = System.Drawing.Color.Black
        Me.CHKSELECTORDER.Location = New System.Drawing.Point(18, 22)
        Me.CHKSELECTORDER.Name = "CHKSELECTORDER"
        Me.CHKSELECTORDER.Size = New System.Drawing.Size(77, 18)
        Me.CHKSELECTORDER.TabIndex = 0
        Me.CHKSELECTORDER.Text = "Select All"
        Me.CHKSELECTORDER.UseVisualStyleBackColor = False
        '
        'GRIDBILLDETAILSORDER
        '
        Me.GRIDBILLDETAILSORDER.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILLDETAILSORDER.Location = New System.Drawing.Point(6, 43)
        Me.GRIDBILLDETAILSORDER.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDBILLDETAILSORDER.MainView = Me.GRIDBILLORDER
        Me.GRIDBILLDETAILSORDER.Name = "GRIDBILLDETAILSORDER"
        Me.GRIDBILLDETAILSORDER.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit5})
        Me.GRIDBILLDETAILSORDER.Size = New System.Drawing.Size(201, 422)
        Me.GRIDBILLDETAILSORDER.TabIndex = 1
        Me.GRIDBILLDETAILSORDER.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDBILLORDER})
        '
        'GRIDBILLORDER
        '
        Me.GRIDBILLORDER.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILLORDER.Appearance.Row.Options.UseFont = True
        Me.GRIDBILLORDER.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHKORDER, Me.GORDERNO})
        Me.GRIDBILLORDER.GridControl = Me.GRIDBILLDETAILSORDER
        Me.GRIDBILLORDER.Name = "GRIDBILLORDER"
        Me.GRIDBILLORDER.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDBILLORDER.OptionsView.ColumnAutoWidth = False
        Me.GRIDBILLORDER.OptionsView.ShowAutoFilterRow = True
        Me.GRIDBILLORDER.OptionsView.ShowGroupPanel = False
        '
        'GCHKORDER
        '
        Me.GCHKORDER.ColumnEdit = Me.RepositoryItemCheckEdit5
        Me.GCHKORDER.FieldName = "CHK"
        Me.GCHKORDER.Name = "GCHKORDER"
        Me.GCHKORDER.OptionsColumn.ShowCaption = False
        Me.GCHKORDER.Visible = True
        Me.GCHKORDER.VisibleIndex = 0
        Me.GCHKORDER.Width = 35
        '
        'RepositoryItemCheckEdit5
        '
        Me.RepositoryItemCheckEdit5.AutoHeight = False
        Me.RepositoryItemCheckEdit5.Name = "RepositoryItemCheckEdit5"
        Me.RepositoryItemCheckEdit5.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GORDERNO
        '
        Me.GORDERNO.Caption = "Order No"
        Me.GORDERNO.FieldName = "ORDERNO"
        Me.GORDERNO.ImageOptions.ImageIndex = 0
        Me.GORDERNO.Name = "GORDERNO"
        Me.GORDERNO.OptionsColumn.AllowEdit = False
        Me.GORDERNO.Visible = True
        Me.GORDERNO.VisibleIndex = 1
        Me.GORDERNO.Width = 120
        '
        'GPITEM
        '
        Me.GPITEM.BackColor = System.Drawing.Color.Transparent
        Me.GPITEM.Controls.Add(Me.CHKSELECTITEM)
        Me.GPITEM.Controls.Add(Me.GRIDBILLDETAILSITEM)
        Me.GPITEM.Location = New System.Drawing.Point(10, 15)
        Me.GPITEM.Name = "GPITEM"
        Me.GPITEM.Size = New System.Drawing.Size(216, 511)
        Me.GPITEM.TabIndex = 761
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
        Me.GRIDBILLDETAILSITEM.Size = New System.Drawing.Size(193, 425)
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
        Me.GridColumn1.Width = 120
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
        Me.GPPARTYNAME.Controls.Add(Me.GroupBox6)
        Me.GPPARTYNAME.Controls.Add(Me.gridbilldetails)
        Me.GPPARTYNAME.Controls.Add(Me.CHKSELECTALL)
        Me.GPPARTYNAME.Location = New System.Drawing.Point(232, 15)
        Me.GPPARTYNAME.Name = "GPPARTYNAME"
        Me.GPPARTYNAME.Size = New System.Drawing.Size(540, 498)
        Me.GPPARTYNAME.TabIndex = 760
        Me.GPPARTYNAME.TabStop = False
        Me.GPPARTYNAME.Text = "Party Name"
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox6.Controls.Add(Me.RBAGENT)
        Me.GroupBox6.Controls.Add(Me.RBACCOUNT)
        Me.GroupBox6.Location = New System.Drawing.Point(96, 6)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(153, 38)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        '
        'RBAGENT
        '
        Me.RBAGENT.AutoSize = True
        Me.RBAGENT.BackColor = System.Drawing.Color.Transparent
        Me.RBAGENT.Location = New System.Drawing.Point(80, 13)
        Me.RBAGENT.Name = "RBAGENT"
        Me.RBAGENT.Size = New System.Drawing.Size(55, 19)
        Me.RBAGENT.TabIndex = 2
        Me.RBAGENT.Text = "Agent"
        Me.RBAGENT.UseVisualStyleBackColor = False
        '
        'RBACCOUNT
        '
        Me.RBACCOUNT.AutoSize = True
        Me.RBACCOUNT.BackColor = System.Drawing.Color.Transparent
        Me.RBACCOUNT.Checked = True
        Me.RBACCOUNT.Location = New System.Drawing.Point(6, 14)
        Me.RBACCOUNT.Name = "RBACCOUNT"
        Me.RBACCOUNT.Size = New System.Drawing.Size(69, 19)
        Me.RBACCOUNT.TabIndex = 0
        Me.RBACCOUNT.TabStop = True
        Me.RBACCOUNT.Text = "Account"
        Me.RBACCOUNT.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(11, 46)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.gridbilldetails.Size = New System.Drawing.Size(523, 425)
        Me.gridbilldetails.TabIndex = 2
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHK, Me.GridColumn2, Me.GAGENT, Me.GGROUPNAME, Me.GCITYNAME, Me.GSTATENAME, Me.GAREA, Me.GSALESMAN})
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
        'GAGENT
        '
        Me.GAGENT.Caption = "Agent Name"
        Me.GAGENT.FieldName = "AGENTNAME"
        Me.GAGENT.Name = "GAGENT"
        Me.GAGENT.OptionsColumn.AllowEdit = False
        Me.GAGENT.Visible = True
        Me.GAGENT.VisibleIndex = 2
        Me.GAGENT.Width = 200
        '
        'GGROUPNAME
        '
        Me.GGROUPNAME.Caption = "Group Name"
        Me.GGROUPNAME.FieldName = "GROUPNAME"
        Me.GGROUPNAME.Name = "GGROUPNAME"
        Me.GGROUPNAME.OptionsColumn.AllowEdit = False
        Me.GGROUPNAME.Visible = True
        Me.GGROUPNAME.VisibleIndex = 3
        Me.GGROUPNAME.Width = 120
        '
        'GCITYNAME
        '
        Me.GCITYNAME.Caption = "City Name"
        Me.GCITYNAME.FieldName = "CITY"
        Me.GCITYNAME.Name = "GCITYNAME"
        Me.GCITYNAME.OptionsColumn.AllowEdit = False
        Me.GCITYNAME.Visible = True
        Me.GCITYNAME.VisibleIndex = 4
        '
        'GSTATENAME
        '
        Me.GSTATENAME.Caption = "State Name"
        Me.GSTATENAME.FieldName = "STATENAME"
        Me.GSTATENAME.Name = "GSTATENAME"
        Me.GSTATENAME.OptionsColumn.AllowEdit = False
        Me.GSTATENAME.Visible = True
        Me.GSTATENAME.VisibleIndex = 5
        Me.GSTATENAME.Width = 80
        '
        'GAREA
        '
        Me.GAREA.Caption = "Area"
        Me.GAREA.FieldName = "AREA"
        Me.GAREA.Name = "GAREA"
        Me.GAREA.OptionsColumn.AllowEdit = False
        Me.GAREA.Visible = True
        Me.GAREA.VisibleIndex = 6
        Me.GAREA.Width = 100
        '
        'GSALESMAN
        '
        Me.GSALESMAN.Caption = "Sales Person"
        Me.GSALESMAN.FieldName = "SALESMAN"
        Me.GSALESMAN.Name = "GSALESMAN"
        Me.GSALESMAN.OptionsColumn.AllowEdit = False
        Me.GSALESMAN.Visible = True
        Me.GSALESMAN.VisibleIndex = 7
        Me.GSALESMAN.Width = 200
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
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.dtto)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtfrom)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.chkdate)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(647, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 53)
        Me.GroupBox1.TabIndex = 769
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
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(22, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 14)
        Me.Label9.TabIndex = 765
        Me.Label9.Text = "Name"
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(63, 10)
        Me.CMBNAME.MaxDropDownItems = 14
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(230, 22)
        Me.CMBNAME.TabIndex = 762
        '
        'CMBAGENT
        '
        Me.CMBAGENT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBAGENT.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBAGENT.BackColor = System.Drawing.Color.White
        Me.CMBAGENT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBAGENT.FormattingEnabled = True
        Me.CMBAGENT.Location = New System.Drawing.Point(63, 38)
        Me.CMBAGENT.MaxDropDownItems = 14
        Me.CMBAGENT.Name = "CMBAGENT"
        Me.CMBAGENT.Size = New System.Drawing.Size(230, 22)
        Me.CMBAGENT.TabIndex = 763
        '
        'CMBCATEGORY
        '
        Me.CMBCATEGORY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCATEGORY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCATEGORY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCATEGORY.FormattingEnabled = True
        Me.CMBCATEGORY.Location = New System.Drawing.Point(63, 66)
        Me.CMBCATEGORY.MaxDropDownItems = 14
        Me.CMBCATEGORY.Name = "CMBCATEGORY"
        Me.CMBCATEGORY.Size = New System.Drawing.Size(85, 22)
        Me.CMBCATEGORY.TabIndex = 767
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(23, 42)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 14)
        Me.Label10.TabIndex = 766
        Me.Label10.Text = "Agent"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(7, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 14)
        Me.Label5.TabIndex = 768
        Me.Label5.Text = "Category"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.RDBALL)
        Me.GroupBox2.Controls.Add(Me.RDBPENDING)
        Me.GroupBox2.Controls.Add(Me.RDBCOMPLETE)
        Me.GroupBox2.Controls.Add(Me.RDBCLOSED)
        Me.GroupBox2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(325, 14)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(316, 46)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        '
        'RDBALL
        '
        Me.RDBALL.AutoSize = True
        Me.RDBALL.Location = New System.Drawing.Point(234, 19)
        Me.RDBALL.Name = "RDBALL"
        Me.RDBALL.Size = New System.Drawing.Size(40, 18)
        Me.RDBALL.TabIndex = 3
        Me.RDBALL.Text = "All"
        Me.RDBALL.UseVisualStyleBackColor = True
        '
        'RDBPENDING
        '
        Me.RDBPENDING.AutoSize = True
        Me.RDBPENDING.Checked = True
        Me.RDBPENDING.Location = New System.Drawing.Point(6, 19)
        Me.RDBPENDING.Name = "RDBPENDING"
        Me.RDBPENDING.Size = New System.Drawing.Size(69, 18)
        Me.RDBPENDING.TabIndex = 0
        Me.RDBPENDING.TabStop = True
        Me.RDBPENDING.Text = "Pending"
        Me.RDBPENDING.UseVisualStyleBackColor = True
        '
        'RDBCOMPLETE
        '
        Me.RDBCOMPLETE.AutoSize = True
        Me.RDBCOMPLETE.Location = New System.Drawing.Point(81, 19)
        Me.RDBCOMPLETE.Name = "RDBCOMPLETE"
        Me.RDBCOMPLETE.Size = New System.Drawing.Size(84, 18)
        Me.RDBCOMPLETE.TabIndex = 1
        Me.RDBCOMPLETE.Text = "Completed"
        Me.RDBCOMPLETE.UseVisualStyleBackColor = True
        '
        'RDBCLOSED
        '
        Me.RDBCLOSED.AutoSize = True
        Me.RDBCLOSED.Location = New System.Drawing.Point(171, 19)
        Me.RDBCLOSED.Name = "RDBCLOSED"
        Me.RDBCLOSED.Size = New System.Drawing.Size(62, 18)
        Me.RDBCLOSED.TabIndex = 2
        Me.RDBCLOSED.Text = "Closed"
        Me.RDBCLOSED.UseVisualStyleBackColor = True
        '
        'CMDEXPORT
        '
        Me.CMDEXPORT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXPORT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXPORT.FlatAppearance.BorderSize = 0
        Me.CMDEXPORT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXPORT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXPORT.Location = New System.Drawing.Point(666, 660)
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
        Me.CMDREFRESH.Location = New System.Drawing.Point(752, 660)
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
        Me.cmdexit.Location = New System.Drawing.Point(838, 660)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 3
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'OrderGridReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1594, 692)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "OrderGridReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Order Grid Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.TabControl2.ResumeLayout(False)
        Me.tbitem.ResumeLayout(False)
        CType(Me.GRIDSO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.GPORDERNO.ResumeLayout(False)
        Me.GPORDERNO.PerformLayout()
        CType(Me.GRIDBILLDETAILSORDER, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDBILLORDER, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GPITEM.ResumeLayout(False)
        Me.GPITEM.PerformLayout()
        CType(Me.GRIDBILLDETAILSITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDBILLITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GPPARTYNAME.ResumeLayout(False)
        Me.GPPARTYNAME.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents cmdexit As Button
    Friend WithEvents CMDREFRESH As Button
    Friend WithEvents GRIDSO As DataGridView
    Friend WithEvents CMDEXPORT As Button
    Friend WithEvents GITEMNAME As DataGridViewTextBoxColumn
    Friend WithEvents GSONO As DataGridViewTextBoxColumn
    Friend WithEvents GSODATE As DataGridViewTextBoxColumn
    Friend WithEvents GNAME As DataGridViewTextBoxColumn
    Friend WithEvents GAGENTNAME As DataGridViewTextBoxColumn
    Friend WithEvents GNOTE As DataGridViewTextBoxColumn
    Friend WithEvents GMILLNAME As DataGridViewTextBoxColumn
    Friend WithEvents GPCS As DataGridViewTextBoxColumn
    Friend WithEvents GOUTPCS As DataGridViewTextBoxColumn
    Friend WithEvents GBALPCS As DataGridViewTextBoxColumn
    Friend WithEvents GRATE As DataGridViewTextBoxColumn
    Friend WithEvents GDAYS As DataGridViewTextBoxColumn
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RDBALL As RadioButton
    Friend WithEvents RDBPENDING As RadioButton
    Friend WithEvents RDBCOMPLETE As RadioButton
    Friend WithEvents RDBCLOSED As RadioButton
    Friend WithEvents Label9 As Label
    Friend WithEvents CMBNAME As ComboBox
    Friend WithEvents CMBAGENT As ComboBox
    Friend WithEvents CMBCATEGORY As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dtto As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents dtfrom As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents chkdate As CheckBox
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents tbitem As TabPage
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents GPORDERNO As GroupBox
    Friend WithEvents CHKSELECTORDER As CheckBox
    Private WithEvents GRIDBILLDETAILSORDER As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDBILLORDER As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHKORDER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents GORDERNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPITEM As GroupBox
    Friend WithEvents CHKSELECTITEM As CheckBox
    Private WithEvents GRIDBILLDETAILSITEM As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDBILLITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHKITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCATEGORY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPPARTYNAME As GroupBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents RBAGENT As RadioButton
    Friend WithEvents RBACCOUNT As RadioButton
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGROUPNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCITYNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSTATENAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAREA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSALESMAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKSELECTALL As CheckBox
End Class
