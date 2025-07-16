<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CoverNote
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CoverNote))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.GRIDCOVER = New System.Windows.Forms.DataGridView()
        Me.GSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GINVNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREGNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GINVINITIALS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPRINTINITIALS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPARTYNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAGENTNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GINVDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLRDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTRANSPORT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTOTALMTRS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTOTALPCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGRANDTOTAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.LBLNAME = New System.Windows.Forms.Label()
        Me.LBLCATEGORY = New System.Windows.Forms.Label()
        Me.CMBAGENT = New System.Windows.Forms.ComboBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.CMDSELECTINV = New System.Windows.Forms.Button()
        Me.DTCOVERDATE = New System.Windows.Forms.DateTimePicker()
        Me.TXTCOVERNO = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
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
        Me.cmdok = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.LBL = New System.Windows.Forms.Label()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.PRINTDOC = New System.Drawing.Printing.PrintDocument()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TXTCOURIERNAME = New System.Windows.Forms.TextBox()
        Me.LBLCOURIERDOCKETNO = New System.Windows.Forms.Label()
        Me.TXTCOURIERDOCKETNO = New System.Windows.Forms.TextBox()
        Me.COURIERDATE = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDCOVER, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.COURIERDATE)
        Me.BlendPanel1.Controls.Add(Me.Label14)
        Me.BlendPanel1.Controls.Add(Me.TXTCOURIERNAME)
        Me.BlendPanel1.Controls.Add(Me.LBLCOURIERDOCKETNO)
        Me.BlendPanel1.Controls.Add(Me.TXTCOURIERDOCKETNO)
        Me.BlendPanel1.Controls.Add(Me.CMBCODE)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.GRIDCOVER)
        Me.BlendPanel1.Controls.Add(Me.CMBNAME)
        Me.BlendPanel1.Controls.Add(Me.LBLNAME)
        Me.BlendPanel1.Controls.Add(Me.LBLCATEGORY)
        Me.BlendPanel1.Controls.Add(Me.CMBAGENT)
        Me.BlendPanel1.Controls.Add(Me.Label27)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.cmdclear)
        Me.BlendPanel1.Controls.Add(Me.GroupBox1)
        Me.BlendPanel1.Controls.Add(Me.GroupBox5)
        Me.BlendPanel1.Controls.Add(Me.CMDSELECTINV)
        Me.BlendPanel1.Controls.Add(Me.DTCOVERDATE)
        Me.BlendPanel1.Controls.Add(Me.TXTCOVERNO)
        Me.BlendPanel1.Controls.Add(Me.Label12)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.tstxtbillno)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.LBL)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 0
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.Enabled = False
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Location = New System.Drawing.Point(1069, 506)
        Me.CMBCODE.MaxDropDownItems = 14
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(58, 23)
        Me.CMBCODE.TabIndex = 922
        Me.CMBCODE.Visible = False
        '
        'TXTADD
        '
        Me.TXTADD.BackColor = System.Drawing.Color.White
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.ForeColor = System.Drawing.Color.DimGray
        Me.TXTADD.Location = New System.Drawing.Point(1129, 506)
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.ReadOnly = True
        Me.TXTADD.Size = New System.Drawing.Size(23, 23)
        Me.TXTADD.TabIndex = 921
        Me.TXTADD.TabStop = False
        Me.TXTADD.Visible = False
        '
        'GRIDCOVER
        '
        Me.GRIDCOVER.AllowUserToAddRows = False
        Me.GRIDCOVER.AllowUserToDeleteRows = False
        Me.GRIDCOVER.AllowUserToResizeColumns = False
        Me.GRIDCOVER.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDCOVER.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDCOVER.BackgroundColor = System.Drawing.Color.White
        Me.GRIDCOVER.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDCOVER.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDCOVER.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDCOVER.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDCOVER.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GINVNO, Me.GREGNAME, Me.GINVINITIALS, Me.GPRINTINITIALS, Me.GPARTYNAME, Me.GAGENTNAME, Me.GINVDATE, Me.GLRNO, Me.GLRDATE, Me.GTRANSPORT, Me.GTOTALMTRS, Me.GTOTALPCS, Me.GGRANDTOTAL})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDCOVER.DefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDCOVER.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDCOVER.Location = New System.Drawing.Point(17, 143)
        Me.GRIDCOVER.MultiSelect = False
        Me.GRIDCOVER.Name = "GRIDCOVER"
        Me.GRIDCOVER.RowHeadersVisible = False
        Me.GRIDCOVER.RowHeadersWidth = 30
        Me.GRIDCOVER.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDCOVER.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.GRIDCOVER.RowTemplate.Height = 20
        Me.GRIDCOVER.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDCOVER.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDCOVER.Size = New System.Drawing.Size(1205, 285)
        Me.GRIDCOVER.TabIndex = 10
        Me.GRIDCOVER.TabStop = False
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
        'GINVNO
        '
        Me.GINVNO.HeaderText = "Inv No"
        Me.GINVNO.Name = "GINVNO"
        Me.GINVNO.ReadOnly = True
        Me.GINVNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GINVNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GINVNO.Visible = False
        '
        'GREGNAME
        '
        Me.GREGNAME.HeaderText = "Reg Name"
        Me.GREGNAME.Name = "GREGNAME"
        Me.GREGNAME.ReadOnly = True
        Me.GREGNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GREGNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GREGNAME.Visible = False
        Me.GREGNAME.Width = 150
        '
        'GINVINITIALS
        '
        Me.GINVINITIALS.HeaderText = "Inv Initials"
        Me.GINVINITIALS.Name = "GINVINITIALS"
        Me.GINVINITIALS.ReadOnly = True
        Me.GINVINITIALS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GINVINITIALS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GINVINITIALS.Visible = False
        Me.GINVINITIALS.Width = 150
        '
        'GPRINTINITIALS
        '
        Me.GPRINTINITIALS.HeaderText = "Bill No"
        Me.GPRINTINITIALS.Name = "GPRINTINITIALS"
        Me.GPRINTINITIALS.ReadOnly = True
        Me.GPRINTINITIALS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPRINTINITIALS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPRINTINITIALS.Width = 120
        '
        'GPARTYNAME
        '
        Me.GPARTYNAME.HeaderText = "Party Name"
        Me.GPARTYNAME.Name = "GPARTYNAME"
        Me.GPARTYNAME.ReadOnly = True
        Me.GPARTYNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPARTYNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPARTYNAME.Width = 200
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
        'GINVDATE
        '
        Me.GINVDATE.HeaderText = "Inv Date"
        Me.GINVDATE.Name = "GINVDATE"
        Me.GINVDATE.ReadOnly = True
        Me.GINVDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GINVDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GINVDATE.Width = 80
        '
        'GLRNO
        '
        Me.GLRNO.HeaderText = "LR No"
        Me.GLRNO.Name = "GLRNO"
        Me.GLRNO.ReadOnly = True
        Me.GLRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GLRNO.Width = 120
        '
        'GLRDATE
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GLRDATE.DefaultCellStyle = DataGridViewCellStyle3
        Me.GLRDATE.HeaderText = "LR Date"
        Me.GLRDATE.Name = "GLRDATE"
        Me.GLRDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLRDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GLRDATE.Width = 80
        '
        'GTRANSPORT
        '
        Me.GTRANSPORT.HeaderText = "Transport"
        Me.GTRANSPORT.Name = "GTRANSPORT"
        Me.GTRANSPORT.ReadOnly = True
        Me.GTRANSPORT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTRANSPORT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GTRANSPORT.Width = 200
        '
        'GTOTALMTRS
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GTOTALMTRS.DefaultCellStyle = DataGridViewCellStyle4
        Me.GTOTALMTRS.HeaderText = "Mtrs"
        Me.GTOTALMTRS.Name = "GTOTALMTRS"
        Me.GTOTALMTRS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTOTALMTRS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GTOTALMTRS.Width = 80
        '
        'GTOTALPCS
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GTOTALPCS.DefaultCellStyle = DataGridViewCellStyle5
        Me.GTOTALPCS.HeaderText = "Pcs"
        Me.GTOTALPCS.Name = "GTOTALPCS"
        Me.GTOTALPCS.ReadOnly = True
        Me.GTOTALPCS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTOTALPCS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GTOTALPCS.Width = 60
        '
        'GGRANDTOTAL
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GGRANDTOTAL.DefaultCellStyle = DataGridViewCellStyle6
        Me.GGRANDTOTAL.HeaderText = "Grand Total"
        Me.GGRANDTOTAL.Name = "GGRANDTOTAL"
        Me.GGRANDTOTAL.ReadOnly = True
        Me.GGRANDTOTAL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGRANDTOTAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GGRANDTOTAL.Width = 80
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(101, 61)
        Me.CMBNAME.MaxDropDownItems = 14
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(339, 23)
        Me.CMBNAME.TabIndex = 0
        '
        'LBLNAME
        '
        Me.LBLNAME.AutoSize = True
        Me.LBLNAME.BackColor = System.Drawing.Color.Transparent
        Me.LBLNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLNAME.Location = New System.Drawing.Point(27, 93)
        Me.LBLNAME.Name = "LBLNAME"
        Me.LBLNAME.Size = New System.Drawing.Size(71, 15)
        Me.LBLNAME.TabIndex = 920
        Me.LBLNAME.Text = "Agent Name"
        '
        'LBLCATEGORY
        '
        Me.LBLCATEGORY.AutoSize = True
        Me.LBLCATEGORY.BackColor = System.Drawing.Color.Transparent
        Me.LBLCATEGORY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLCATEGORY.ForeColor = System.Drawing.Color.Red
        Me.LBLCATEGORY.Location = New System.Drawing.Point(166, 143)
        Me.LBLCATEGORY.Name = "LBLCATEGORY"
        Me.LBLCATEGORY.Size = New System.Drawing.Size(0, 15)
        Me.LBLCATEGORY.TabIndex = 908
        Me.LBLCATEGORY.Visible = False
        '
        'CMBAGENT
        '
        Me.CMBAGENT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBAGENT.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBAGENT.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBAGENT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBAGENT.FormattingEnabled = True
        Me.CMBAGENT.Location = New System.Drawing.Point(101, 90)
        Me.CMBAGENT.MaxDropDownItems = 14
        Me.CMBAGENT.Name = "CMBAGENT"
        Me.CMBAGENT.Size = New System.Drawing.Size(339, 23)
        Me.CMBAGENT.TabIndex = 1
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(25, 64)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(70, 15)
        Me.Label27.TabIndex = 765
        Me.Label27.Text = "Party Name"
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmddelete.FlatAppearance.BorderSize = 0
        Me.cmddelete.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddelete.ForeColor = System.Drawing.Color.Black
        Me.cmddelete.Location = New System.Drawing.Point(590, 501)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 28)
        Me.cmddelete.TabIndex = 5
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
        Me.cmdclear.Location = New System.Drawing.Point(719, 467)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(80, 28)
        Me.cmdclear.TabIndex = 4
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.txtremarks)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(22, 433)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(276, 93)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Remarks"
        '
        'txtremarks
        '
        Me.txtremarks.ForeColor = System.Drawing.Color.DimGray
        Me.txtremarks.Location = New System.Drawing.Point(6, 18)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(261, 65)
        Me.txtremarks.TabIndex = 0
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(22, 434)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(276, 93)
        Me.GroupBox5.TabIndex = 6
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Remarks"
        '
        'CMDSELECTINV
        '
        Me.CMDSELECTINV.BackColor = System.Drawing.Color.Transparent
        Me.CMDSELECTINV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSELECTINV.FlatAppearance.BorderSize = 0
        Me.CMDSELECTINV.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSELECTINV.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDSELECTINV.Location = New System.Drawing.Point(547, 467)
        Me.CMDSELECTINV.Name = "CMDSELECTINV"
        Me.CMDSELECTINV.Size = New System.Drawing.Size(80, 28)
        Me.CMDSELECTINV.TabIndex = 2
        Me.CMDSELECTINV.Text = "Select Inv"
        Me.CMDSELECTINV.UseVisualStyleBackColor = False
        '
        'DTCOVERDATE
        '
        Me.DTCOVERDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTCOVERDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTCOVERDATE.Location = New System.Drawing.Point(1131, 85)
        Me.DTCOVERDATE.Name = "DTCOVERDATE"
        Me.DTCOVERDATE.Size = New System.Drawing.Size(91, 23)
        Me.DTCOVERDATE.TabIndex = 0
        '
        'TXTCOVERNO
        '
        Me.TXTCOVERNO.BackColor = System.Drawing.Color.Linen
        Me.TXTCOVERNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOVERNO.Location = New System.Drawing.Point(1131, 56)
        Me.TXTCOVERNO.Name = "TXTCOVERNO"
        Me.TXTCOVERNO.ReadOnly = True
        Me.TXTCOVERNO.Size = New System.Drawing.Size(91, 23)
        Me.TXTCOVERNO.TabIndex = 633
        Me.TXTCOVERNO.TabStop = False
        Me.TXTCOVERNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(1089, 60)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(38, 15)
        Me.Label12.TabIndex = 634
        Me.Label12.Text = "Sr. No"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(1096, 89)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 15)
        Me.Label9.TabIndex = 632
        Me.Label9.Text = "Date"
        '
        'tstxtbillno
        '
        Me.tstxtbillno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstxtbillno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstxtbillno.Location = New System.Drawing.Point(242, 1)
        Me.tstxtbillno.Name = "tstxtbillno"
        Me.tstxtbillno.Size = New System.Drawing.Size(56, 22)
        Me.tstxtbillno.TabIndex = 9
        Me.tstxtbillno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.tooldelete, Me.toolStripSeparator, Me.toolprevious, Me.toolnext, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 611
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
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(633, 467)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 3
        Me.cmdok.Text = "&Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(676, 501)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 6
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'LBL
        '
        Me.LBL.AutoSize = True
        Me.LBL.BackColor = System.Drawing.Color.Transparent
        Me.LBL.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LBL.Location = New System.Drawing.Point(4, 31)
        Me.LBL.Name = "LBL"
        Me.LBL.Size = New System.Drawing.Size(110, 26)
        Me.LBL.TabIndex = 430
        Me.LBL.Text = "Cover Note"
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(559, 42)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 15)
        Me.Label14.TabIndex = 926
        Me.Label14.Text = "Courier Name"
        '
        'TXTCOURIERNAME
        '
        Me.TXTCOURIERNAME.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCOURIERNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOURIERNAME.Location = New System.Drawing.Point(643, 39)
        Me.TXTCOURIERNAME.MaxLength = 50
        Me.TXTCOURIERNAME.Name = "TXTCOURIERNAME"
        Me.TXTCOURIERNAME.Size = New System.Drawing.Size(344, 23)
        Me.TXTCOURIERNAME.TabIndex = 923
        '
        'LBLCOURIERDOCKETNO
        '
        Me.LBLCOURIERDOCKETNO.AutoSize = True
        Me.LBLCOURIERDOCKETNO.BackColor = System.Drawing.Color.Transparent
        Me.LBLCOURIERDOCKETNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLCOURIERDOCKETNO.Location = New System.Drawing.Point(533, 70)
        Me.LBLCOURIERDOCKETNO.Name = "LBLCOURIERDOCKETNO"
        Me.LBLCOURIERDOCKETNO.Size = New System.Drawing.Size(108, 15)
        Me.LBLCOURIERDOCKETNO.TabIndex = 925
        Me.LBLCOURIERDOCKETNO.Text = "Courier docket No."
        '
        'TXTCOURIERDOCKETNO
        '
        Me.TXTCOURIERDOCKETNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCOURIERDOCKETNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOURIERDOCKETNO.Location = New System.Drawing.Point(643, 67)
        Me.TXTCOURIERDOCKETNO.MaxLength = 20
        Me.TXTCOURIERDOCKETNO.Name = "TXTCOURIERDOCKETNO"
        Me.TXTCOURIERDOCKETNO.Size = New System.Drawing.Size(247, 23)
        Me.TXTCOURIERDOCKETNO.TabIndex = 924
        '
        'COURIERDATE
        '
        Me.COURIERDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.COURIERDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.COURIERDATE.Location = New System.Drawing.Point(643, 96)
        Me.COURIERDATE.Name = "COURIERDATE"
        Me.COURIERDATE.Size = New System.Drawing.Size(91, 23)
        Me.COURIERDATE.TabIndex = 927
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(565, 99)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 15)
        Me.Label1.TabIndex = 928
        Me.Label1.Text = "Courier Date"
        '
        'CoverNote
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "CoverNote"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Cover Note"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDCOVER, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMBNAME As ComboBox
    Friend WithEvents LBLNAME As Label
    Friend WithEvents LBLCATEGORY As Label
    Friend WithEvents CMBAGENT As ComboBox
    Friend WithEvents Label27 As Label
    Friend WithEvents cmddelete As Button
    Friend WithEvents cmdclear As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtremarks As TextBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents CMDSELECTINV As Button
    Friend WithEvents DTCOVERDATE As DateTimePicker
    Friend WithEvents TXTCOVERNO As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label9 As Label
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
    Friend WithEvents cmdok As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents LBL As Label
    Friend WithEvents GRIDCOVER As DataGridView
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents PRINTDOC As Drawing.Printing.PrintDocument
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents GSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GINVNO As DataGridViewTextBoxColumn
    Friend WithEvents GREGNAME As DataGridViewTextBoxColumn
    Friend WithEvents GINVINITIALS As DataGridViewTextBoxColumn
    Friend WithEvents GPRINTINITIALS As DataGridViewTextBoxColumn
    Friend WithEvents GPARTYNAME As DataGridViewTextBoxColumn
    Friend WithEvents GAGENTNAME As DataGridViewTextBoxColumn
    Friend WithEvents GINVDATE As DataGridViewTextBoxColumn
    Friend WithEvents GLRNO As DataGridViewTextBoxColumn
    Friend WithEvents GLRDATE As DataGridViewTextBoxColumn
    Friend WithEvents GTRANSPORT As DataGridViewTextBoxColumn
    Friend WithEvents GTOTALMTRS As DataGridViewTextBoxColumn
    Friend WithEvents GTOTALPCS As DataGridViewTextBoxColumn
    Friend WithEvents GGRANDTOTAL As DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents COURIERDATE As DateTimePicker
    Friend WithEvents Label14 As Label
    Friend WithEvents TXTCOURIERNAME As TextBox
    Friend WithEvents LBLCOURIERDOCKETNO As Label
    Friend WithEvents TXTCOURIERDOCKETNO As TextBox
End Class
