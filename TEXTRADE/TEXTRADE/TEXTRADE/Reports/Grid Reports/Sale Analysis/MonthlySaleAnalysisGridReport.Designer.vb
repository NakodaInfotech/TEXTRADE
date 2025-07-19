<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MonthlySaleAnalysisGridReport
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel2 = New VbPowerPack.BlendPanel()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.CMBCITY = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CMBSUBGROUP = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CMBBROKERNAME = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CMDWHATSAPP = New System.Windows.Forms.Button()
        Me.CMDPRINT = New System.Windows.Forms.Button()
        Me.CMBITEMNAME = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CMDSEARCH = New System.Windows.Forms.Button()
        Me.CMBREGISTER = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtto = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtfrom = New System.Windows.Forms.DateTimePicker()
        Me.chkdate = New System.Windows.Forms.CheckBox()
        Me.CMBREPORTTYPE = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TBREPORT = New System.Windows.Forms.TabPage()
        Me.CMBGRIDITEM = New System.Windows.Forms.ComboBox()
        Me.CMBGRIDAGENT = New System.Windows.Forms.ComboBox()
        Me.CMBGRIDPARTY = New System.Windows.Forms.ComboBox()
        Me.GRIDREPORT = New System.Windows.Forms.DataGridView()
        Me.GNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAPRIL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMAY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GJUNE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GJULY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAUGUST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSEPTEMBER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOCTOBER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNOVEMBER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDECEMBER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GJANUARY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFEBRUARY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMARCH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTOTAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TBFILTERS = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CHKSELECTAGENT = New System.Windows.Forms.CheckBox()
        Me.GRIDAGENTDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDAGENT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GAGENTCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GAGENTNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPITEMNAME = New System.Windows.Forms.GroupBox()
        Me.CHKSELECTITEM = New System.Windows.Forms.CheckBox()
        Me.GRIDITEMDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GITEMCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GITEMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPPARTYNAME = New System.Windows.Forms.GroupBox()
        Me.CHKSELECTPARTY = New System.Windows.Forms.CheckBox()
        Me.gridpartydetails = New DevExpress.XtraGrid.GridControl()
        Me.Gridparty = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GPARTYCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GPARTYNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMBPRINTVALUE = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.txtDeliveryadd = New System.Windows.Forms.TextBox()
        Me.cmbacccode = New System.Windows.Forms.ComboBox()
        Me.txtadd = New System.Windows.Forms.TextBox()
        Me.TXTTEMP = New System.Windows.Forms.TextBox()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.CMBGRIDPARTY1 = New System.Windows.Forms.ComboBox()
        Me.CMBGRIDITEM1 = New System.Windows.Forms.ComboBox()
        Me.CMBGRIDAGENT1 = New System.Windows.Forms.ComboBox()
        Me.BlendPanel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TBREPORT.SuspendLayout()
        CType(Me.GRIDREPORT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBFILTERS.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GRIDAGENTDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDAGENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GPITEMNAME.SuspendLayout()
        CType(Me.GRIDITEMDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GPPARTYNAME.SuspendLayout()
        CType(Me.gridpartydetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gridparty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel2
        '
        Me.BlendPanel2.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel2.Controls.Add(Me.cmdexit)
        Me.BlendPanel2.Controls.Add(Me.CMBCITY)
        Me.BlendPanel2.Controls.Add(Me.Label4)
        Me.BlendPanel2.Controls.Add(Me.CMBSUBGROUP)
        Me.BlendPanel2.Controls.Add(Me.Label1)
        Me.BlendPanel2.Controls.Add(Me.CMBBROKERNAME)
        Me.BlendPanel2.Controls.Add(Me.Label5)
        Me.BlendPanel2.Controls.Add(Me.CMDWHATSAPP)
        Me.BlendPanel2.Controls.Add(Me.CMDPRINT)
        Me.BlendPanel2.Controls.Add(Me.CMBITEMNAME)
        Me.BlendPanel2.Controls.Add(Me.Label11)
        Me.BlendPanel2.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel2.Controls.Add(Me.CMBNAME)
        Me.BlendPanel2.Controls.Add(Me.Label9)
        Me.BlendPanel2.Controls.Add(Me.CMDSEARCH)
        Me.BlendPanel2.Controls.Add(Me.CMBREGISTER)
        Me.BlendPanel2.Controls.Add(Me.Label3)
        Me.BlendPanel2.Controls.Add(Me.dtto)
        Me.BlendPanel2.Controls.Add(Me.Label7)
        Me.BlendPanel2.Controls.Add(Me.dtfrom)
        Me.BlendPanel2.Controls.Add(Me.chkdate)
        Me.BlendPanel2.Controls.Add(Me.CMBREPORTTYPE)
        Me.BlendPanel2.Controls.Add(Me.Label10)
        Me.BlendPanel2.Controls.Add(Me.TabControl1)
        Me.BlendPanel2.Controls.Add(Me.CMBPRINTVALUE)
        Me.BlendPanel2.Controls.Add(Me.Label6)
        Me.BlendPanel2.Controls.Add(Me.CMBCODE)
        Me.BlendPanel2.Controls.Add(Me.txtDeliveryadd)
        Me.BlendPanel2.Controls.Add(Me.cmbacccode)
        Me.BlendPanel2.Controls.Add(Me.txtadd)
        Me.BlendPanel2.Controls.Add(Me.TXTTEMP)
        Me.BlendPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel2.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel2.Name = "BlendPanel2"
        Me.BlendPanel2.Size = New System.Drawing.Size(1444, 672)
        Me.BlendPanel2.TabIndex = 0
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(1262, 37)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 788
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'CMBCITY
        '
        Me.CMBCITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCITY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCITY.FormattingEnabled = True
        Me.CMBCITY.Location = New System.Drawing.Point(798, 14)
        Me.CMBCITY.MaxDropDownItems = 14
        Me.CMBCITY.Name = "CMBCITY"
        Me.CMBCITY.Size = New System.Drawing.Size(140, 23)
        Me.CMBCITY.TabIndex = 786
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(735, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 15)
        Me.Label4.TabIndex = 787
        Me.Label4.Text = "City Name"
        '
        'CMBSUBGROUP
        '
        Me.CMBSUBGROUP.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSUBGROUP.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSUBGROUP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBSUBGROUP.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSUBGROUP.FormattingEnabled = True
        Me.CMBSUBGROUP.Items.AddRange(New Object() {"", "PARTYWISE", "AGENTWISE", "ITEMWISE"})
        Me.CMBSUBGROUP.Location = New System.Drawing.Point(88, 42)
        Me.CMBSUBGROUP.MaxDropDownItems = 14
        Me.CMBSUBGROUP.Name = "CMBSUBGROUP"
        Me.CMBSUBGROUP.Size = New System.Drawing.Size(237, 23)
        Me.CMBSUBGROUP.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(22, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 15)
        Me.Label1.TabIndex = 785
        Me.Label1.Text = "Sub Group"
        '
        'CMBBROKERNAME
        '
        Me.CMBBROKERNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBBROKERNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBBROKERNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBBROKERNAME.FormattingEnabled = True
        Me.CMBBROKERNAME.Location = New System.Drawing.Point(415, 42)
        Me.CMBBROKERNAME.MaxDropDownItems = 14
        Me.CMBBROKERNAME.Name = "CMBBROKERNAME"
        Me.CMBBROKERNAME.Size = New System.Drawing.Size(287, 23)
        Me.CMBBROKERNAME.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(335, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 15)
        Me.Label5.TabIndex = 783
        Me.Label5.Text = "Broker Name"
        '
        'CMDWHATSAPP
        '
        Me.CMDWHATSAPP.BackColor = System.Drawing.Color.Transparent
        Me.CMDWHATSAPP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDWHATSAPP.FlatAppearance.BorderSize = 0
        Me.CMDWHATSAPP.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDWHATSAPP.ForeColor = System.Drawing.Color.Black
        Me.CMDWHATSAPP.Location = New System.Drawing.Point(1262, 68)
        Me.CMDWHATSAPP.Name = "CMDWHATSAPP"
        Me.CMDWHATSAPP.Size = New System.Drawing.Size(80, 28)
        Me.CMDWHATSAPP.TabIndex = 13
        Me.CMDWHATSAPP.Text = "&Whatsapp"
        Me.CMDWHATSAPP.UseVisualStyleBackColor = False
        '
        'CMDPRINT
        '
        Me.CMDPRINT.BackColor = System.Drawing.Color.Transparent
        Me.CMDPRINT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDPRINT.FlatAppearance.BorderSize = 0
        Me.CMDPRINT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDPRINT.ForeColor = System.Drawing.Color.Black
        Me.CMDPRINT.Location = New System.Drawing.Point(1176, 68)
        Me.CMDPRINT.Name = "CMDPRINT"
        Me.CMDPRINT.Size = New System.Drawing.Size(80, 28)
        Me.CMDPRINT.TabIndex = 12
        Me.CMDPRINT.Text = "&Print"
        Me.CMDPRINT.UseVisualStyleBackColor = False
        '
        'CMBITEMNAME
        '
        Me.CMBITEMNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBITEMNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBITEMNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBITEMNAME.FormattingEnabled = True
        Me.CMBITEMNAME.Location = New System.Drawing.Point(415, 71)
        Me.CMBITEMNAME.MaxDropDownItems = 14
        Me.CMBITEMNAME.Name = "CMBITEMNAME"
        Me.CMBITEMNAME.Size = New System.Drawing.Size(287, 23)
        Me.CMBITEMNAME.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(347, 75)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(65, 15)
        Me.Label11.TabIndex = 781
        Me.Label11.Text = "Item Name"
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDCLEAR.FlatAppearance.BorderSize = 0
        Me.CMDCLEAR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.Black
        Me.CMDCLEAR.Location = New System.Drawing.Point(1176, 39)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 11
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(415, 13)
        Me.CMBNAME.MaxDropDownItems = 14
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(287, 23)
        Me.CMBNAME.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(374, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 15)
        Me.Label9.TabIndex = 780
        Me.Label9.Text = "Name"
        '
        'CMDSEARCH
        '
        Me.CMDSEARCH.BackColor = System.Drawing.Color.Transparent
        Me.CMDSEARCH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSEARCH.FlatAppearance.BorderSize = 0
        Me.CMDSEARCH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSEARCH.ForeColor = System.Drawing.Color.Black
        Me.CMDSEARCH.Location = New System.Drawing.Point(1176, 10)
        Me.CMDSEARCH.Name = "CMDSEARCH"
        Me.CMDSEARCH.Size = New System.Drawing.Size(80, 28)
        Me.CMDSEARCH.TabIndex = 10
        Me.CMDSEARCH.Text = "&Display"
        Me.CMDSEARCH.UseVisualStyleBackColor = False
        '
        'CMBREGISTER
        '
        Me.CMBREGISTER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBREGISTER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBREGISTER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBREGISTER.FormattingEnabled = True
        Me.CMBREGISTER.Location = New System.Drawing.Point(88, 71)
        Me.CMBREGISTER.MaxDropDownItems = 14
        Me.CMBREGISTER.Name = "CMBREGISTER"
        Me.CMBREGISTER.Size = New System.Drawing.Size(237, 23)
        Me.CMBREGISTER.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(35, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 15)
        Me.Label3.TabIndex = 773
        Me.Label3.Text = "Register"
        '
        'dtto
        '
        Me.dtto.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtto.Location = New System.Drawing.Point(1080, 42)
        Me.dtto.Name = "dtto"
        Me.dtto.Size = New System.Drawing.Size(90, 23)
        Me.dtto.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(1051, 46)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 15)
        Me.Label7.TabIndex = 771
        Me.Label7.Text = "To :"
        '
        'dtfrom
        '
        Me.dtfrom.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtfrom.Location = New System.Drawing.Point(1080, 13)
        Me.dtfrom.Name = "dtfrom"
        Me.dtfrom.Size = New System.Drawing.Size(90, 23)
        Me.dtfrom.TabIndex = 7
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.BackColor = System.Drawing.Color.Transparent
        Me.chkdate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdate.ForeColor = System.Drawing.Color.Black
        Me.chkdate.Location = New System.Drawing.Point(1025, 15)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(51, 19)
        Me.chkdate.TabIndex = 6
        Me.chkdate.Text = "Date"
        Me.chkdate.UseVisualStyleBackColor = False
        '
        'CMBREPORTTYPE
        '
        Me.CMBREPORTTYPE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBREPORTTYPE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBREPORTTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBREPORTTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBREPORTTYPE.FormattingEnabled = True
        Me.CMBREPORTTYPE.Items.AddRange(New Object() {"PARTYWISE", "AGENTWISE", "CITYWISE", "ITEMWISE", "CATEGORYWISE"})
        Me.CMBREPORTTYPE.Location = New System.Drawing.Point(88, 13)
        Me.CMBREPORTTYPE.MaxDropDownItems = 14
        Me.CMBREPORTTYPE.Name = "CMBREPORTTYPE"
        Me.CMBREPORTTYPE.Size = New System.Drawing.Size(237, 23)
        Me.CMBREPORTTYPE.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(16, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 15)
        Me.Label10.TabIndex = 767
        Me.Label10.Text = "Report Type"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TBREPORT)
        Me.TabControl1.Controls.Add(Me.TBFILTERS)
        Me.TabControl1.Location = New System.Drawing.Point(0, 99)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1448, 561)
        Me.TabControl1.TabIndex = 14
        '
        'TBREPORT
        '
        Me.TBREPORT.AutoScroll = True
        Me.TBREPORT.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TBREPORT.Controls.Add(Me.CMBGRIDAGENT1)
        Me.TBREPORT.Controls.Add(Me.CMBGRIDITEM1)
        Me.TBREPORT.Controls.Add(Me.CMBGRIDPARTY1)
        Me.TBREPORT.Controls.Add(Me.CMBGRIDITEM)
        Me.TBREPORT.Controls.Add(Me.CMBGRIDAGENT)
        Me.TBREPORT.Controls.Add(Me.CMBGRIDPARTY)
        Me.TBREPORT.Controls.Add(Me.GRIDREPORT)
        Me.TBREPORT.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.TBREPORT.Location = New System.Drawing.Point(4, 24)
        Me.TBREPORT.Name = "TBREPORT"
        Me.TBREPORT.Padding = New System.Windows.Forms.Padding(3)
        Me.TBREPORT.Size = New System.Drawing.Size(1440, 533)
        Me.TBREPORT.TabIndex = 0
        Me.TBREPORT.Text = "Report"
        '
        'CMBGRIDITEM
        '
        Me.CMBGRIDITEM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGRIDITEM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGRIDITEM.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGRIDITEM.FormattingEnabled = True
        Me.CMBGRIDITEM.Location = New System.Drawing.Point(2, 3)
        Me.CMBGRIDITEM.MaxDropDownItems = 14
        Me.CMBGRIDITEM.Name = "CMBGRIDITEM"
        Me.CMBGRIDITEM.Size = New System.Drawing.Size(172, 23)
        Me.CMBGRIDITEM.TabIndex = 791
        Me.CMBGRIDITEM.Visible = False
        '
        'CMBGRIDAGENT
        '
        Me.CMBGRIDAGENT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGRIDAGENT.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGRIDAGENT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGRIDAGENT.FormattingEnabled = True
        Me.CMBGRIDAGENT.Location = New System.Drawing.Point(1, 3)
        Me.CMBGRIDAGENT.MaxDropDownItems = 14
        Me.CMBGRIDAGENT.Name = "CMBGRIDAGENT"
        Me.CMBGRIDAGENT.Size = New System.Drawing.Size(172, 23)
        Me.CMBGRIDAGENT.TabIndex = 790
        Me.CMBGRIDAGENT.Visible = False
        '
        'CMBGRIDPARTY
        '
        Me.CMBGRIDPARTY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGRIDPARTY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGRIDPARTY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGRIDPARTY.FormattingEnabled = True
        Me.CMBGRIDPARTY.Location = New System.Drawing.Point(2, 3)
        Me.CMBGRIDPARTY.MaxDropDownItems = 14
        Me.CMBGRIDPARTY.Name = "CMBGRIDPARTY"
        Me.CMBGRIDPARTY.Size = New System.Drawing.Size(172, 23)
        Me.CMBGRIDPARTY.TabIndex = 789
        Me.CMBGRIDPARTY.Visible = False
        '
        'GRIDREPORT
        '
        Me.GRIDREPORT.AllowUserToAddRows = False
        Me.GRIDREPORT.AllowUserToDeleteRows = False
        Me.GRIDREPORT.AllowUserToResizeColumns = False
        Me.GRIDREPORT.AllowUserToResizeRows = False
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDREPORT.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDREPORT.BackgroundColor = System.Drawing.Color.White
        Me.GRIDREPORT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDREPORT.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 7.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDREPORT.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDREPORT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDREPORT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GNAME, Me.GAPRIL, Me.GMAY, Me.GJUNE, Me.GJULY, Me.GAUGUST, Me.GSEPTEMBER, Me.GOCTOBER, Me.GNOVEMBER, Me.GDECEMBER, Me.GJANUARY, Me.GFEBRUARY, Me.GMARCH, Me.GTOTAL})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Verdana", 7.0!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDREPORT.DefaultCellStyle = DataGridViewCellStyle4
        Me.GRIDREPORT.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDREPORT.Location = New System.Drawing.Point(2, 25)
        Me.GRIDREPORT.MultiSelect = False
        Me.GRIDREPORT.Name = "GRIDREPORT"
        Me.GRIDREPORT.ReadOnly = True
        Me.GRIDREPORT.RowHeadersVisible = False
        Me.GRIDREPORT.RowHeadersWidth = 30
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDREPORT.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDREPORT.RowTemplate.Height = 20
        Me.GRIDREPORT.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDREPORT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDREPORT.Size = New System.Drawing.Size(1437, 500)
        Me.GRIDREPORT.TabIndex = 686
        Me.GRIDREPORT.TabStop = False
        '
        'GNAME
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GNAME.DefaultCellStyle = DataGridViewCellStyle3
        Me.GNAME.HeaderText = "Name"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.ReadOnly = True
        Me.GNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNAME.Width = 170
        '
        'GAPRIL
        '
        Me.GAPRIL.HeaderText = "April"
        Me.GAPRIL.Name = "GAPRIL"
        Me.GAPRIL.ReadOnly = True
        Me.GAPRIL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GAPRIL.Width = 95
        '
        'GMAY
        '
        Me.GMAY.HeaderText = "May"
        Me.GMAY.Name = "GMAY"
        Me.GMAY.ReadOnly = True
        Me.GMAY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMAY.Width = 95
        '
        'GJUNE
        '
        Me.GJUNE.HeaderText = "June"
        Me.GJUNE.Name = "GJUNE"
        Me.GJUNE.ReadOnly = True
        Me.GJUNE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GJUNE.Width = 95
        '
        'GJULY
        '
        Me.GJULY.HeaderText = "July"
        Me.GJULY.Name = "GJULY"
        Me.GJULY.ReadOnly = True
        Me.GJULY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GJULY.Width = 95
        '
        'GAUGUST
        '
        Me.GAUGUST.HeaderText = "August"
        Me.GAUGUST.Name = "GAUGUST"
        Me.GAUGUST.ReadOnly = True
        Me.GAUGUST.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GAUGUST.Width = 95
        '
        'GSEPTEMBER
        '
        Me.GSEPTEMBER.HeaderText = "September"
        Me.GSEPTEMBER.Name = "GSEPTEMBER"
        Me.GSEPTEMBER.ReadOnly = True
        Me.GSEPTEMBER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSEPTEMBER.Width = 95
        '
        'GOCTOBER
        '
        Me.GOCTOBER.HeaderText = "October"
        Me.GOCTOBER.Name = "GOCTOBER"
        Me.GOCTOBER.ReadOnly = True
        Me.GOCTOBER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOCTOBER.Width = 95
        '
        'GNOVEMBER
        '
        Me.GNOVEMBER.HeaderText = "November"
        Me.GNOVEMBER.Name = "GNOVEMBER"
        Me.GNOVEMBER.ReadOnly = True
        Me.GNOVEMBER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNOVEMBER.Width = 95
        '
        'GDECEMBER
        '
        Me.GDECEMBER.HeaderText = "December"
        Me.GDECEMBER.Name = "GDECEMBER"
        Me.GDECEMBER.ReadOnly = True
        Me.GDECEMBER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDECEMBER.Width = 95
        '
        'GJANUARY
        '
        Me.GJANUARY.HeaderText = "January"
        Me.GJANUARY.Name = "GJANUARY"
        Me.GJANUARY.ReadOnly = True
        Me.GJANUARY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GJANUARY.Width = 95
        '
        'GFEBRUARY
        '
        Me.GFEBRUARY.HeaderText = "February"
        Me.GFEBRUARY.Name = "GFEBRUARY"
        Me.GFEBRUARY.ReadOnly = True
        Me.GFEBRUARY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GFEBRUARY.Width = 95
        '
        'GMARCH
        '
        Me.GMARCH.HeaderText = "March"
        Me.GMARCH.Name = "GMARCH"
        Me.GMARCH.ReadOnly = True
        Me.GMARCH.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMARCH.Width = 95
        '
        'GTOTAL
        '
        Me.GTOTAL.HeaderText = "Total"
        Me.GTOTAL.Name = "GTOTAL"
        Me.GTOTAL.ReadOnly = True
        Me.GTOTAL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'TBFILTERS
        '
        Me.TBFILTERS.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TBFILTERS.Controls.Add(Me.GroupBox1)
        Me.TBFILTERS.Controls.Add(Me.GPITEMNAME)
        Me.TBFILTERS.Controls.Add(Me.GPPARTYNAME)
        Me.TBFILTERS.Location = New System.Drawing.Point(4, 22)
        Me.TBFILTERS.Name = "TBFILTERS"
        Me.TBFILTERS.Padding = New System.Windows.Forms.Padding(3)
        Me.TBFILTERS.Size = New System.Drawing.Size(1440, 535)
        Me.TBFILTERS.TabIndex = 1
        Me.TBFILTERS.Text = "Filters"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.CHKSELECTAGENT)
        Me.GroupBox1.Controls.Add(Me.GRIDAGENTDETAILS)
        Me.GroupBox1.Location = New System.Drawing.Point(890, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(452, 552)
        Me.GroupBox1.TabIndex = 760
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Agent Name"
        '
        'CHKSELECTAGENT
        '
        Me.CHKSELECTAGENT.AutoSize = True
        Me.CHKSELECTAGENT.BackColor = System.Drawing.Color.Transparent
        Me.CHKSELECTAGENT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSELECTAGENT.ForeColor = System.Drawing.Color.Black
        Me.CHKSELECTAGENT.Location = New System.Drawing.Point(83, 0)
        Me.CHKSELECTAGENT.Name = "CHKSELECTAGENT"
        Me.CHKSELECTAGENT.Size = New System.Drawing.Size(77, 18)
        Me.CHKSELECTAGENT.TabIndex = 0
        Me.CHKSELECTAGENT.Text = "Select All"
        Me.CHKSELECTAGENT.UseVisualStyleBackColor = False
        '
        'GRIDAGENTDETAILS
        '
        Me.GRIDAGENTDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDAGENTDETAILS.Location = New System.Drawing.Point(6, 20)
        Me.GRIDAGENTDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDAGENTDETAILS.MainView = Me.GRIDAGENT
        Me.GRIDAGENTDETAILS.Name = "GRIDAGENTDETAILS"
        Me.GRIDAGENTDETAILS.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.GRIDAGENTDETAILS.Size = New System.Drawing.Size(435, 501)
        Me.GRIDAGENTDETAILS.TabIndex = 1
        Me.GRIDAGENTDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDAGENT})
        '
        'GRIDAGENT
        '
        Me.GRIDAGENT.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDAGENT.Appearance.Row.Options.UseFont = True
        Me.GRIDAGENT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GAGENTCHK, Me.GAGENTNAME})
        Me.GRIDAGENT.GridControl = Me.GRIDAGENTDETAILS
        Me.GRIDAGENT.Name = "GRIDAGENT"
        Me.GRIDAGENT.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDAGENT.OptionsView.ColumnAutoWidth = False
        Me.GRIDAGENT.OptionsView.ShowAutoFilterRow = True
        Me.GRIDAGENT.OptionsView.ShowGroupPanel = False
        '
        'GAGENTCHK
        '
        Me.GAGENTCHK.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.GAGENTCHK.FieldName = "CHK"
        Me.GAGENTCHK.Name = "GAGENTCHK"
        Me.GAGENTCHK.OptionsColumn.ShowCaption = False
        Me.GAGENTCHK.Visible = True
        Me.GAGENTCHK.VisibleIndex = 0
        Me.GAGENTCHK.Width = 35
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
        Me.GAGENTNAME.FieldName = "NAME"
        Me.GAGENTNAME.ImageIndex = 0
        Me.GAGENTNAME.Name = "GAGENTNAME"
        Me.GAGENTNAME.OptionsColumn.AllowEdit = False
        Me.GAGENTNAME.Visible = True
        Me.GAGENTNAME.VisibleIndex = 1
        Me.GAGENTNAME.Width = 350
        '
        'GPITEMNAME
        '
        Me.GPITEMNAME.BackColor = System.Drawing.Color.Transparent
        Me.GPITEMNAME.Controls.Add(Me.CHKSELECTITEM)
        Me.GPITEMNAME.Controls.Add(Me.GRIDITEMDETAILS)
        Me.GPITEMNAME.Location = New System.Drawing.Point(6, 6)
        Me.GPITEMNAME.Name = "GPITEMNAME"
        Me.GPITEMNAME.Size = New System.Drawing.Size(351, 552)
        Me.GPITEMNAME.TabIndex = 760
        Me.GPITEMNAME.TabStop = False
        Me.GPITEMNAME.Text = "Item Name"
        '
        'CHKSELECTITEM
        '
        Me.CHKSELECTITEM.AutoSize = True
        Me.CHKSELECTITEM.BackColor = System.Drawing.Color.Transparent
        Me.CHKSELECTITEM.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSELECTITEM.ForeColor = System.Drawing.Color.Black
        Me.CHKSELECTITEM.Location = New System.Drawing.Point(86, 0)
        Me.CHKSELECTITEM.Name = "CHKSELECTITEM"
        Me.CHKSELECTITEM.Size = New System.Drawing.Size(77, 18)
        Me.CHKSELECTITEM.TabIndex = 0
        Me.CHKSELECTITEM.Text = "Select All"
        Me.CHKSELECTITEM.UseVisualStyleBackColor = False
        '
        'GRIDITEMDETAILS
        '
        Me.GRIDITEMDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDITEMDETAILS.Location = New System.Drawing.Point(6, 20)
        Me.GRIDITEMDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDITEMDETAILS.MainView = Me.GRIDITEM
        Me.GRIDITEMDETAILS.Name = "GRIDITEMDETAILS"
        Me.GRIDITEMDETAILS.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit4})
        Me.GRIDITEMDETAILS.Size = New System.Drawing.Size(337, 501)
        Me.GRIDITEMDETAILS.TabIndex = 1
        Me.GRIDITEMDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDITEM})
        '
        'GRIDITEM
        '
        Me.GRIDITEM.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDITEM.Appearance.Row.Options.UseFont = True
        Me.GRIDITEM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GITEMCHK, Me.GITEMNAME})
        Me.GRIDITEM.GridControl = Me.GRIDITEMDETAILS
        Me.GRIDITEM.Name = "GRIDITEM"
        Me.GRIDITEM.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDITEM.OptionsView.ColumnAutoWidth = False
        Me.GRIDITEM.OptionsView.ShowAutoFilterRow = True
        Me.GRIDITEM.OptionsView.ShowGroupPanel = False
        '
        'GITEMCHK
        '
        Me.GITEMCHK.ColumnEdit = Me.RepositoryItemCheckEdit4
        Me.GITEMCHK.FieldName = "CHK"
        Me.GITEMCHK.Name = "GITEMCHK"
        Me.GITEMCHK.OptionsColumn.ShowCaption = False
        Me.GITEMCHK.Visible = True
        Me.GITEMCHK.VisibleIndex = 0
        Me.GITEMCHK.Width = 35
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        Me.RepositoryItemCheckEdit4.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GITEMNAME
        '
        Me.GITEMNAME.Caption = "Item Name"
        Me.GITEMNAME.FieldName = "NAME"
        Me.GITEMNAME.ImageIndex = 0
        Me.GITEMNAME.Name = "GITEMNAME"
        Me.GITEMNAME.OptionsColumn.AllowEdit = False
        Me.GITEMNAME.Visible = True
        Me.GITEMNAME.VisibleIndex = 1
        Me.GITEMNAME.Width = 250
        '
        'GPPARTYNAME
        '
        Me.GPPARTYNAME.BackColor = System.Drawing.Color.Transparent
        Me.GPPARTYNAME.Controls.Add(Me.CHKSELECTPARTY)
        Me.GPPARTYNAME.Controls.Add(Me.gridpartydetails)
        Me.GPPARTYNAME.Location = New System.Drawing.Point(370, 6)
        Me.GPPARTYNAME.Name = "GPPARTYNAME"
        Me.GPPARTYNAME.Size = New System.Drawing.Size(495, 552)
        Me.GPPARTYNAME.TabIndex = 759
        Me.GPPARTYNAME.TabStop = False
        Me.GPPARTYNAME.Text = "Party Name"
        '
        'CHKSELECTPARTY
        '
        Me.CHKSELECTPARTY.AutoSize = True
        Me.CHKSELECTPARTY.BackColor = System.Drawing.Color.Transparent
        Me.CHKSELECTPARTY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSELECTPARTY.ForeColor = System.Drawing.Color.Black
        Me.CHKSELECTPARTY.Location = New System.Drawing.Point(81, 0)
        Me.CHKSELECTPARTY.Name = "CHKSELECTPARTY"
        Me.CHKSELECTPARTY.Size = New System.Drawing.Size(77, 18)
        Me.CHKSELECTPARTY.TabIndex = 0
        Me.CHKSELECTPARTY.Text = "Select All"
        Me.CHKSELECTPARTY.UseVisualStyleBackColor = False
        '
        'gridpartydetails
        '
        Me.gridpartydetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridpartydetails.Location = New System.Drawing.Point(6, 20)
        Me.gridpartydetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridpartydetails.MainView = Me.Gridparty
        Me.gridpartydetails.Name = "gridpartydetails"
        Me.gridpartydetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.gridpartydetails.Size = New System.Drawing.Size(483, 501)
        Me.gridpartydetails.TabIndex = 1
        Me.gridpartydetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.Gridparty})
        '
        'Gridparty
        '
        Me.Gridparty.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Gridparty.Appearance.Row.Options.UseFont = True
        Me.Gridparty.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GPARTYCHK, Me.GPARTYNAME})
        Me.Gridparty.GridControl = Me.gridpartydetails
        Me.Gridparty.Name = "Gridparty"
        Me.Gridparty.OptionsBehavior.AllowIncrementalSearch = True
        Me.Gridparty.OptionsView.ColumnAutoWidth = False
        Me.Gridparty.OptionsView.ShowAutoFilterRow = True
        Me.Gridparty.OptionsView.ShowGroupPanel = False
        '
        'GPARTYCHK
        '
        Me.GPARTYCHK.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GPARTYCHK.FieldName = "CHK"
        Me.GPARTYCHK.Name = "GPARTYCHK"
        Me.GPARTYCHK.OptionsColumn.ShowCaption = False
        Me.GPARTYCHK.Visible = True
        Me.GPARTYCHK.VisibleIndex = 0
        Me.GPARTYCHK.Width = 35
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GPARTYNAME
        '
        Me.GPARTYNAME.Caption = "Party Name"
        Me.GPARTYNAME.FieldName = "NAME"
        Me.GPARTYNAME.ImageIndex = 0
        Me.GPARTYNAME.Name = "GPARTYNAME"
        Me.GPARTYNAME.OptionsColumn.AllowEdit = False
        Me.GPARTYNAME.Visible = True
        Me.GPARTYNAME.VisibleIndex = 1
        Me.GPARTYNAME.Width = 400
        '
        'CMBPRINTVALUE
        '
        Me.CMBPRINTVALUE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBPRINTVALUE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBPRINTVALUE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBPRINTVALUE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBPRINTVALUE.FormattingEnabled = True
        Me.CMBPRINTVALUE.Items.AddRange(New Object() {"Qty Only", "Taka Only", "Amt Only", "Qty And Amt", "Avg Mtrs", "Avg Rate"})
        Me.CMBPRINTVALUE.Location = New System.Drawing.Point(798, 43)
        Me.CMBPRINTVALUE.MaxDropDownItems = 14
        Me.CMBPRINTVALUE.Name = "CMBPRINTVALUE"
        Me.CMBPRINTVALUE.Size = New System.Drawing.Size(140, 23)
        Me.CMBPRINTVALUE.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(728, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 14)
        Me.Label6.TabIndex = 764
        Me.Label6.Text = "Print Value"
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Items.AddRange(New Object() {"C/R", "O/R"})
        Me.CMBCODE.Location = New System.Drawing.Point(1315, 22)
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(28, 22)
        Me.CMBCODE.TabIndex = 738
        Me.CMBCODE.Visible = False
        '
        'txtDeliveryadd
        '
        Me.txtDeliveryadd.BackColor = System.Drawing.Color.White
        Me.txtDeliveryadd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDeliveryadd.Enabled = False
        Me.txtDeliveryadd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryadd.Location = New System.Drawing.Point(1315, 10)
        Me.txtDeliveryadd.Name = "txtDeliveryadd"
        Me.txtDeliveryadd.ReadOnly = True
        Me.txtDeliveryadd.Size = New System.Drawing.Size(34, 22)
        Me.txtDeliveryadd.TabIndex = 737
        Me.txtDeliveryadd.TabStop = False
        Me.txtDeliveryadd.Visible = False
        '
        'cmbacccode
        '
        Me.cmbacccode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbacccode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbacccode.BackColor = System.Drawing.Color.White
        Me.cmbacccode.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbacccode.FormattingEnabled = True
        Me.cmbacccode.Location = New System.Drawing.Point(1388, 22)
        Me.cmbacccode.MaxDropDownItems = 14
        Me.cmbacccode.Name = "cmbacccode"
        Me.cmbacccode.Size = New System.Drawing.Size(69, 22)
        Me.cmbacccode.TabIndex = 650
        Me.cmbacccode.Visible = False
        '
        'txtadd
        '
        Me.txtadd.Location = New System.Drawing.Point(1388, 23)
        Me.txtadd.Name = "txtadd"
        Me.txtadd.Size = New System.Drawing.Size(69, 23)
        Me.txtadd.TabIndex = 649
        Me.txtadd.Visible = False
        '
        'TXTTEMP
        '
        Me.TXTTEMP.Location = New System.Drawing.Point(1385, 25)
        Me.TXTTEMP.Name = "TXTTEMP"
        Me.TXTTEMP.Size = New System.Drawing.Size(69, 23)
        Me.TXTTEMP.TabIndex = 646
        Me.TXTTEMP.Visible = False
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'CMBGRIDPARTY1
        '
        Me.CMBGRIDPARTY1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGRIDPARTY1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGRIDPARTY1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGRIDPARTY1.FormattingEnabled = True
        Me.CMBGRIDPARTY1.Location = New System.Drawing.Point(173, 3)
        Me.CMBGRIDPARTY1.MaxDropDownItems = 14
        Me.CMBGRIDPARTY1.Name = "CMBGRIDPARTY1"
        Me.CMBGRIDPARTY1.Size = New System.Drawing.Size(172, 23)
        Me.CMBGRIDPARTY1.TabIndex = 792
        Me.CMBGRIDPARTY1.Visible = False
        '
        'CMBGRIDITEM1
        '
        Me.CMBGRIDITEM1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGRIDITEM1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGRIDITEM1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGRIDITEM1.FormattingEnabled = True
        Me.CMBGRIDITEM1.Location = New System.Drawing.Point(173, 3)
        Me.CMBGRIDITEM1.MaxDropDownItems = 14
        Me.CMBGRIDITEM1.Name = "CMBGRIDITEM1"
        Me.CMBGRIDITEM1.Size = New System.Drawing.Size(172, 23)
        Me.CMBGRIDITEM1.TabIndex = 793
        Me.CMBGRIDITEM1.Visible = False
        '
        'CMBGRIDAGENT1
        '
        Me.CMBGRIDAGENT1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGRIDAGENT1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGRIDAGENT1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGRIDAGENT1.FormattingEnabled = True
        Me.CMBGRIDAGENT1.Location = New System.Drawing.Point(173, 3)
        Me.CMBGRIDAGENT1.MaxDropDownItems = 14
        Me.CMBGRIDAGENT1.Name = "CMBGRIDAGENT1"
        Me.CMBGRIDAGENT1.Size = New System.Drawing.Size(172, 23)
        Me.CMBGRIDAGENT1.TabIndex = 794
        Me.CMBGRIDAGENT1.Visible = False
        '
        'MonthlySaleAnalysisGridReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1444, 672)
        Me.Controls.Add(Me.BlendPanel2)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "MonthlySaleAnalysisGridReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Monthly Sale Analysis"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel2.ResumeLayout(False)
        Me.BlendPanel2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TBREPORT.ResumeLayout(False)
        CType(Me.GRIDREPORT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBFILTERS.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GRIDAGENTDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDAGENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GPITEMNAME.ResumeLayout(False)
        Me.GPITEMNAME.PerformLayout()
        CType(Me.GRIDITEMDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GPPARTYNAME.ResumeLayout(False)
        Me.GPPARTYNAME.PerformLayout()
        CType(Me.gridpartydetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gridparty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel2 As VbPowerPack.BlendPanel
    Friend WithEvents GPITEMNAME As GroupBox
    Friend WithEvents CHKSELECTITEM As CheckBox
    Private WithEvents GRIDITEMDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GITEMCHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents GITEMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPPARTYNAME As GroupBox
    Friend WithEvents CHKSELECTPARTY As CheckBox
    Private WithEvents gridpartydetails As DevExpress.XtraGrid.GridControl
    Private WithEvents Gridparty As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GPARTYCHK As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GPARTYNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents txtDeliveryadd As TextBox
    Friend WithEvents cmbacccode As ComboBox
    Friend WithEvents txtadd As TextBox
    Friend WithEvents TXTTEMP As TextBox
    Friend WithEvents CMBPRINTVALUE As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TBREPORT As TabPage
    Friend WithEvents TBFILTERS As TabPage
    Friend WithEvents CMBREPORTTYPE As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents dtto As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents dtfrom As DateTimePicker
    Friend WithEvents chkdate As CheckBox
    Friend WithEvents CMBREGISTER As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents CMDWHATSAPP As Button
    Friend WithEvents CMDPRINT As Button
    Friend WithEvents CMBITEMNAME As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents CMDCLEAR As Button
    Friend WithEvents CMBNAME As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents CMDSEARCH As Button
    Friend WithEvents CMBSUBGROUP As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CMBBROKERNAME As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents GRIDREPORT As DataGridView
    Friend WithEvents CMBCITY As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cmdexit As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CHKSELECTAGENT As CheckBox
    Private WithEvents GRIDAGENTDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDAGENT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GAGENTCHK As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GAGENTNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GNAME As DataGridViewTextBoxColumn
    Friend WithEvents GAPRIL As DataGridViewTextBoxColumn
    Friend WithEvents GMAY As DataGridViewTextBoxColumn
    Friend WithEvents GJUNE As DataGridViewTextBoxColumn
    Friend WithEvents GJULY As DataGridViewTextBoxColumn
    Friend WithEvents GAUGUST As DataGridViewTextBoxColumn
    Friend WithEvents GSEPTEMBER As DataGridViewTextBoxColumn
    Friend WithEvents GOCTOBER As DataGridViewTextBoxColumn
    Friend WithEvents GNOVEMBER As DataGridViewTextBoxColumn
    Friend WithEvents GDECEMBER As DataGridViewTextBoxColumn
    Friend WithEvents GJANUARY As DataGridViewTextBoxColumn
    Friend WithEvents GFEBRUARY As DataGridViewTextBoxColumn
    Friend WithEvents GMARCH As DataGridViewTextBoxColumn
    Friend WithEvents GTOTAL As DataGridViewTextBoxColumn
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents CMBGRIDPARTY As ComboBox
    Friend WithEvents CMBGRIDITEM As ComboBox
    Friend WithEvents CMBGRIDAGENT As ComboBox
    Friend WithEvents CMBGRIDAGENT1 As ComboBox
    Friend WithEvents CMBGRIDITEM1 As ComboBox
    Friend WithEvents CMBGRIDPARTY1 As ComboBox
End Class
