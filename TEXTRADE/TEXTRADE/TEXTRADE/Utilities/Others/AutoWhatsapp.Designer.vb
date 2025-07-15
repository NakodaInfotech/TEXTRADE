<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AutoWhatsapp
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.LBLTYPE1 = New System.Windows.Forms.Label()
        Me.LBLTYPE = New System.Windows.Forms.Label()
        Me.TXTTYPE2 = New System.Windows.Forms.TextBox()
        Me.TXTTYPE1 = New System.Windows.Forms.TextBox()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GACHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GAGENTNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GACITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPPANEL = New System.Windows.Forms.Panel()
        Me.TXTTIME = New System.Windows.Forms.TextBox()
        Me.TXTSRNO = New System.Windows.Forms.TextBox()
        Me.CHKSUNDAY = New System.Windows.Forms.CheckBox()
        Me.CHKSATURDAY = New System.Windows.Forms.CheckBox()
        Me.CHKFRIDAY = New System.Windows.Forms.CheckBox()
        Me.GRIDAUTOWA = New System.Windows.Forms.DataGridView()
        Me.GSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMON = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GTUE = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GWED = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GTHU = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GFRI = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GSAT = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GSUN = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GTIME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CHKTHURSDAY = New System.Windows.Forms.CheckBox()
        Me.CHKWEDNESDAY = New System.Windows.Forms.CheckBox()
        Me.CHKTUESDAY = New System.Windows.Forms.CheckBox()
        Me.CMBTYPE = New System.Windows.Forms.ComboBox()
        Me.CHKMONDAY = New System.Windows.Forms.CheckBox()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDSAVE = New System.Windows.Forms.Button()
        Me.EP = New System.Windows.Forms.ErrorProvider()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GPPANEL.SuspendLayout()
        CType(Me.GRIDAUTOWA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.LBLTYPE1)
        Me.BlendPanel1.Controls.Add(Me.LBLTYPE)
        Me.BlendPanel1.Controls.Add(Me.TXTTYPE2)
        Me.BlendPanel1.Controls.Add(Me.TXTTYPE1)
        Me.BlendPanel1.Controls.Add(Me.GridControl1)
        Me.BlendPanel1.Controls.Add(Me.GPPANEL)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDSAVE)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1604, 581)
        Me.BlendPanel1.TabIndex = 1
        '
        'LBLTYPE1
        '
        Me.LBLTYPE1.AutoSize = True
        Me.LBLTYPE1.BackColor = System.Drawing.Color.Transparent
        Me.LBLTYPE1.Location = New System.Drawing.Point(1291, 48)
        Me.LBLTYPE1.Name = "LBLTYPE1"
        Me.LBLTYPE1.Size = New System.Drawing.Size(31, 15)
        Me.LBLTYPE1.TabIndex = 15
        Me.LBLTYPE1.Text = "Type"
        '
        'LBLTYPE
        '
        Me.LBLTYPE.AutoSize = True
        Me.LBLTYPE.BackColor = System.Drawing.Color.Transparent
        Me.LBLTYPE.Location = New System.Drawing.Point(966, 48)
        Me.LBLTYPE.Name = "LBLTYPE"
        Me.LBLTYPE.Size = New System.Drawing.Size(31, 15)
        Me.LBLTYPE.TabIndex = 14
        Me.LBLTYPE.Text = "Type"
        '
        'TXTTYPE2
        '
        Me.TXTTYPE2.BackColor = System.Drawing.Color.Linen
        Me.TXTTYPE2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTYPE2.Location = New System.Drawing.Point(1328, 45)
        Me.TXTTYPE2.Name = "TXTTYPE2"
        Me.TXTTYPE2.ReadOnly = True
        Me.TXTTYPE2.Size = New System.Drawing.Size(264, 23)
        Me.TXTTYPE2.TabIndex = 13
        Me.TXTTYPE2.TabStop = False
        '
        'TXTTYPE1
        '
        Me.TXTTYPE1.BackColor = System.Drawing.Color.Linen
        Me.TXTTYPE1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTYPE1.Location = New System.Drawing.Point(1003, 45)
        Me.TXTTYPE1.Name = "TXTTYPE1"
        Me.TXTTYPE1.ReadOnly = True
        Me.TXTTYPE1.Size = New System.Drawing.Size(264, 23)
        Me.TXTTYPE1.TabIndex = 12
        Me.TXTTYPE1.TabStop = False
        '
        'GridControl1
        '
        Me.GridControl1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridControl1.Location = New System.Drawing.Point(1275, 74)
        Me.GridControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.GridControl1.Size = New System.Drawing.Size(317, 439)
        Me.GridControl1.TabIndex = 4
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GACHK, Me.GAGENTNAME, Me.GACITY})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AllowIncrementalSearch = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GACHK
        '
        Me.GACHK.Caption = "AGENTCHK"
        Me.GACHK.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.GACHK.FieldName = "AGENTCHK"
        Me.GACHK.Name = "GACHK"
        Me.GACHK.OptionsColumn.ShowCaption = False
        Me.GACHK.Visible = True
        Me.GACHK.VisibleIndex = 0
        Me.GACHK.Width = 40
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GAGENTNAME
        '
        Me.GAGENTNAME.Caption = "Agent Name"
        Me.GAGENTNAME.FieldName = "AGENTNAME"
        Me.GAGENTNAME.ImageIndex = 0
        Me.GAGENTNAME.Name = "GAGENTNAME"
        Me.GAGENTNAME.OptionsColumn.AllowEdit = False
        Me.GAGENTNAME.Visible = True
        Me.GAGENTNAME.VisibleIndex = 1
        Me.GAGENTNAME.Width = 200
        '
        'GACITY
        '
        Me.GACITY.Caption = "City"
        Me.GACITY.FieldName = "AGENTCITY"
        Me.GACITY.Name = "GACITY"
        Me.GACITY.Visible = True
        Me.GACITY.VisibleIndex = 2
        Me.GACITY.Width = 60
        '
        'GPPANEL
        '
        Me.GPPANEL.AutoScroll = True
        Me.GPPANEL.BackColor = System.Drawing.Color.Transparent
        Me.GPPANEL.Controls.Add(Me.TXTTIME)
        Me.GPPANEL.Controls.Add(Me.TXTSRNO)
        Me.GPPANEL.Controls.Add(Me.CHKSUNDAY)
        Me.GPPANEL.Controls.Add(Me.CHKSATURDAY)
        Me.GPPANEL.Controls.Add(Me.CHKFRIDAY)
        Me.GPPANEL.Controls.Add(Me.GRIDAUTOWA)
        Me.GPPANEL.Controls.Add(Me.CHKTHURSDAY)
        Me.GPPANEL.Controls.Add(Me.CHKWEDNESDAY)
        Me.GPPANEL.Controls.Add(Me.CHKTUESDAY)
        Me.GPPANEL.Controls.Add(Me.CMBTYPE)
        Me.GPPANEL.Controls.Add(Me.CHKMONDAY)
        Me.GPPANEL.Location = New System.Drawing.Point(12, 36)
        Me.GPPANEL.Name = "GPPANEL"
        Me.GPPANEL.Size = New System.Drawing.Size(929, 330)
        Me.GPPANEL.TabIndex = 0
        '
        'TXTTIME
        '
        Me.TXTTIME.BackColor = System.Drawing.Color.White
        Me.TXTTIME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTIME.Location = New System.Drawing.Point(812, 15)
        Me.TXTTIME.Name = "TXTTIME"
        Me.TXTTIME.Size = New System.Drawing.Size(83, 23)
        Me.TXTTIME.TabIndex = 9
        '
        'TXTSRNO
        '
        Me.TXTSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTSRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSRNO.Location = New System.Drawing.Point(3, 15)
        Me.TXTSRNO.Name = "TXTSRNO"
        Me.TXTSRNO.ReadOnly = True
        Me.TXTSRNO.Size = New System.Drawing.Size(40, 23)
        Me.TXTSRNO.TabIndex = 0
        Me.TXTSRNO.TabStop = False
        '
        'CHKSUNDAY
        '
        Me.CHKSUNDAY.AutoSize = True
        Me.CHKSUNDAY.BackColor = System.Drawing.Color.Transparent
        Me.CHKSUNDAY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSUNDAY.Location = New System.Drawing.Point(739, 17)
        Me.CHKSUNDAY.Name = "CHKSUNDAY"
        Me.CHKSUNDAY.Size = New System.Drawing.Size(66, 19)
        Me.CHKSUNDAY.TabIndex = 8
        Me.CHKSUNDAY.Text = "Sunday"
        Me.CHKSUNDAY.UseVisualStyleBackColor = False
        '
        'CHKSATURDAY
        '
        Me.CHKSATURDAY.AutoSize = True
        Me.CHKSATURDAY.BackColor = System.Drawing.Color.Transparent
        Me.CHKSATURDAY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSATURDAY.Location = New System.Drawing.Point(650, 17)
        Me.CHKSATURDAY.Name = "CHKSATURDAY"
        Me.CHKSATURDAY.Size = New System.Drawing.Size(75, 19)
        Me.CHKSATURDAY.TabIndex = 7
        Me.CHKSATURDAY.Text = "Saturday"
        Me.CHKSATURDAY.UseVisualStyleBackColor = False
        '
        'CHKFRIDAY
        '
        Me.CHKFRIDAY.AutoSize = True
        Me.CHKFRIDAY.BackColor = System.Drawing.Color.Transparent
        Me.CHKFRIDAY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKFRIDAY.Location = New System.Drawing.Point(571, 16)
        Me.CHKFRIDAY.Name = "CHKFRIDAY"
        Me.CHKFRIDAY.Size = New System.Drawing.Size(61, 19)
        Me.CHKFRIDAY.TabIndex = 6
        Me.CHKFRIDAY.Text = "Friday"
        Me.CHKFRIDAY.UseVisualStyleBackColor = False
        '
        'GRIDAUTOWA
        '
        Me.GRIDAUTOWA.AllowUserToAddRows = False
        Me.GRIDAUTOWA.AllowUserToDeleteRows = False
        Me.GRIDAUTOWA.AllowUserToResizeColumns = False
        Me.GRIDAUTOWA.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDAUTOWA.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDAUTOWA.BackgroundColor = System.Drawing.Color.White
        Me.GRIDAUTOWA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDAUTOWA.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDAUTOWA.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDAUTOWA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDAUTOWA.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GTYPE, Me.GMON, Me.GTUE, Me.GWED, Me.GTHU, Me.GFRI, Me.GSAT, Me.GSUN, Me.GTIME})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDAUTOWA.DefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDAUTOWA.GridColor = System.Drawing.SystemColors.ControlText
        Me.GRIDAUTOWA.Location = New System.Drawing.Point(3, 38)
        Me.GRIDAUTOWA.Margin = New System.Windows.Forms.Padding(2)
        Me.GRIDAUTOWA.MultiSelect = False
        Me.GRIDAUTOWA.Name = "GRIDAUTOWA"
        Me.GRIDAUTOWA.RowHeadersVisible = False
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDAUTOWA.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.GRIDAUTOWA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDAUTOWA.Size = New System.Drawing.Size(916, 285)
        Me.GRIDAUTOWA.TabIndex = 11
        Me.GRIDAUTOWA.TabStop = False
        '
        'GSRNO
        '
        Me.GSRNO.HeaderText = "Sr."
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.ReadOnly = True
        Me.GSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSRNO.Width = 40
        '
        'GTYPE
        '
        Me.GTYPE.HeaderText = "Type"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.ReadOnly = True
        Me.GTYPE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GTYPE.Width = 175
        '
        'GMON
        '
        Me.GMON.HeaderText = "Monday"
        Me.GMON.Name = "GMON"
        Me.GMON.ReadOnly = True
        Me.GMON.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMON.Width = 85
        '
        'GTUE
        '
        Me.GTUE.HeaderText = "Tuesday"
        Me.GTUE.Name = "GTUE"
        Me.GTUE.ReadOnly = True
        Me.GTUE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTUE.Width = 85
        '
        'GWED
        '
        Me.GWED.HeaderText = "Wednesday"
        Me.GWED.Name = "GWED"
        Me.GWED.ReadOnly = True
        Me.GWED.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GWED.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.GWED.Width = 85
        '
        'GTHU
        '
        Me.GTHU.HeaderText = "Thursday"
        Me.GTHU.Name = "GTHU"
        Me.GTHU.ReadOnly = True
        Me.GTHU.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTHU.Width = 85
        '
        'GFRI
        '
        Me.GFRI.HeaderText = "Friday"
        Me.GFRI.Name = "GFRI"
        Me.GFRI.ReadOnly = True
        Me.GFRI.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GFRI.Width = 85
        '
        'GSAT
        '
        Me.GSAT.HeaderText = "Saturday"
        Me.GSAT.Name = "GSAT"
        Me.GSAT.ReadOnly = True
        Me.GSAT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSAT.Width = 85
        '
        'GSUN
        '
        Me.GSUN.HeaderText = "Sunday"
        Me.GSUN.Name = "GSUN"
        Me.GSUN.ReadOnly = True
        Me.GSUN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSUN.Width = 85
        '
        'GTIME
        '
        Me.GTIME.HeaderText = "Time"
        Me.GTIME.Name = "GTIME"
        Me.GTIME.ReadOnly = True
        Me.GTIME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTIME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GTIME.Width = 80
        '
        'CHKTHURSDAY
        '
        Me.CHKTHURSDAY.AutoSize = True
        Me.CHKTHURSDAY.BackColor = System.Drawing.Color.Transparent
        Me.CHKTHURSDAY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKTHURSDAY.Location = New System.Drawing.Point(478, 17)
        Me.CHKTHURSDAY.Name = "CHKTHURSDAY"
        Me.CHKTHURSDAY.Size = New System.Drawing.Size(77, 19)
        Me.CHKTHURSDAY.TabIndex = 5
        Me.CHKTHURSDAY.Text = "Thursday"
        Me.CHKTHURSDAY.UseVisualStyleBackColor = False
        '
        'CHKWEDNESDAY
        '
        Me.CHKWEDNESDAY.AutoSize = True
        Me.CHKWEDNESDAY.BackColor = System.Drawing.Color.Transparent
        Me.CHKWEDNESDAY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKWEDNESDAY.Location = New System.Drawing.Point(387, 17)
        Me.CHKWEDNESDAY.Name = "CHKWEDNESDAY"
        Me.CHKWEDNESDAY.Size = New System.Drawing.Size(90, 19)
        Me.CHKWEDNESDAY.TabIndex = 4
        Me.CHKWEDNESDAY.Text = "Wednesday"
        Me.CHKWEDNESDAY.UseVisualStyleBackColor = False
        '
        'CHKTUESDAY
        '
        Me.CHKTUESDAY.AutoSize = True
        Me.CHKTUESDAY.BackColor = System.Drawing.Color.Transparent
        Me.CHKTUESDAY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKTUESDAY.Location = New System.Drawing.Point(302, 17)
        Me.CHKTUESDAY.Name = "CHKTUESDAY"
        Me.CHKTUESDAY.Size = New System.Drawing.Size(70, 19)
        Me.CHKTUESDAY.TabIndex = 3
        Me.CHKTUESDAY.Text = "Tuesday"
        Me.CHKTUESDAY.UseVisualStyleBackColor = False
        '
        'CMBTYPE
        '
        Me.CMBTYPE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTYPE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBTYPE.DropDownWidth = 400
        Me.CMBTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTYPE.FormattingEnabled = True
        Me.CMBTYPE.Items.AddRange(New Object() {"SALEORDER", "CHALLAN", "INVOICE", "OUTSTANDING"})
        Me.CMBTYPE.Location = New System.Drawing.Point(43, 15)
        Me.CMBTYPE.MaxLength = 100
        Me.CMBTYPE.Name = "CMBTYPE"
        Me.CMBTYPE.Size = New System.Drawing.Size(175, 23)
        Me.CMBTYPE.TabIndex = 1
        '
        'CHKMONDAY
        '
        Me.CHKMONDAY.AutoSize = True
        Me.CHKMONDAY.BackColor = System.Drawing.Color.Transparent
        Me.CHKMONDAY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKMONDAY.Location = New System.Drawing.Point(225, 17)
        Me.CHKMONDAY.Name = "CHKMONDAY"
        Me.CHKMONDAY.Size = New System.Drawing.Size(71, 19)
        Me.CHKMONDAY.TabIndex = 2
        Me.CHKMONDAY.Text = "Monday"
        Me.CHKMONDAY.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(947, 74)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.gridbilldetails.Size = New System.Drawing.Size(320, 439)
        Me.gridbilldetails.TabIndex = 1
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHK, Me.GNAME, Me.GCITY})
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
        Me.GCHK.Width = 40
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GNAME
        '
        Me.GNAME.Caption = "Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.ImageIndex = 0
        Me.GNAME.Name = "GNAME"
        Me.GNAME.OptionsColumn.AllowEdit = False
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 1
        Me.GNAME.Width = 200
        '
        'GCITY
        '
        Me.GCITY.Caption = "City"
        Me.GCITY.FieldName = "CITY"
        Me.GCITY.Name = "GCITY"
        Me.GCITY.Visible = True
        Me.GCITY.VisibleIndex = 2
        Me.GCITY.Width = 60
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Location = New System.Drawing.Point(650, 391)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 3
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDSAVE
        '
        Me.CMDSAVE.Location = New System.Drawing.Point(564, 391)
        Me.CMDSAVE.Name = "CMDSAVE"
        Me.CMDSAVE.Size = New System.Drawing.Size(80, 28)
        Me.CMDSAVE.TabIndex = 2
        Me.CMDSAVE.Text = "&Save"
        Me.CMDSAVE.UseVisualStyleBackColor = True
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'AutoWhatsapp
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1604, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "AutoWhatsapp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "AutoWhatsapp"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GPPANEL.ResumeLayout(False)
        Me.GPPANEL.PerformLayout()
        CType(Me.GRIDAUTOWA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CHKSUNDAY As CheckBox
    Friend WithEvents CHKSATURDAY As CheckBox
    Friend WithEvents CHKFRIDAY As CheckBox
    Friend WithEvents CHKTHURSDAY As CheckBox
    Friend WithEvents CHKWEDNESDAY As CheckBox
    Friend WithEvents CHKTUESDAY As CheckBox
    Friend WithEvents CHKMONDAY As CheckBox
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMBTYPE As ComboBox
    Friend WithEvents CMDSAVE As Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents GPPANEL As Panel
    Friend WithEvents TXTTIME As TextBox
    Friend WithEvents TXTSRNO As TextBox
    Friend WithEvents GRIDAUTOWA As DataGridView
    Friend WithEvents GSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GTYPE As DataGridViewTextBoxColumn
    Friend WithEvents GMON As DataGridViewCheckBoxColumn
    Friend WithEvents GTUE As DataGridViewCheckBoxColumn
    Friend WithEvents GWED As DataGridViewCheckBoxColumn
    Friend WithEvents GTHU As DataGridViewCheckBoxColumn
    Friend WithEvents GFRI As DataGridViewCheckBoxColumn
    Friend WithEvents GSAT As DataGridViewCheckBoxColumn
    Friend WithEvents GSUN As DataGridViewCheckBoxColumn
    Friend WithEvents GTIME As DataGridViewTextBoxColumn
    Friend WithEvents LBLTYPE1 As Label
    Friend WithEvents LBLTYPE As Label
    Friend WithEvents TXTTYPE2 As TextBox
    Friend WithEvents TXTTYPE1 As TextBox
    Private WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Private WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GACHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents GAGENTNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GACITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCITY As DevExpress.XtraGrid.Columns.GridColumn
End Class
