<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ComplaintSolved
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ComplaintSolved))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TXTLRNO = New System.Windows.Forms.TextBox()
        Me.CMDCOSTSHEET = New System.Windows.Forms.Button()
        Me.SKDATE = New System.Windows.Forms.MaskedTextBox()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.CMDSELECTCOMPLAINT = New System.Windows.Forms.Button()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.TXTNO = New System.Windows.Forms.TextBox()
        Me.LBLSRNO = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.CMBREGISTER = New System.Windows.Forms.ComboBox()
        Me.TXTBILLNO = New System.Windows.Forms.TextBox()
        Me.GRIDSHRINKAGE = New System.Windows.Forms.DataGridView()
        Me.LBLTOTALRECDPCS = New System.Windows.Forms.Label()
        Me.LBLTOTALRECDMTRS = New System.Windows.Forms.Label()
        Me.TXTFROMTYPE = New System.Windows.Forms.TextBox()
        Me.LBLTOTALSMPMTRS = New System.Windows.Forms.Label()
        Me.LBLTOTALSHRINKAGE = New System.Windows.Forms.Label()
        Me.TXTCOMPLAINT = New System.Windows.Forms.TextBox()
        Me.LBLAVGSHRINKAGE = New System.Windows.Forms.Label()
        Me.LBLTOTALBALMTRS = New System.Windows.Forms.Label()
        Me.TXTCOMPLAINTDATE = New System.Windows.Forms.MaskedTextBox()
        Me.TXTBILLINITIALS = New System.Windows.Forms.TextBox()
        Me.TXTCOMPLAINTBY = New System.Windows.Forms.TextBox()
        Me.txtsrno = New System.Windows.Forms.TextBox()
        Me.LBLTOTALPCS = New System.Windows.Forms.Label()
        Me.LBLTOTALBALPCS = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.LBLTOTALMTRS = New System.Windows.Forms.Label()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.tstxtbillno = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.tooldelete = New System.Windows.Forms.ToolStripButton()
        Me.TOOLSMS = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLPRIVIOUS = New System.Windows.Forms.ToolStripButton()
        Me.toolnext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCOMP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCOMPDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCOMPBY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBILLINITIALS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBILLNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREGISTER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFROMNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFROMSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BlendPanel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.GRIDSHRINKAGE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.TXTLRNO)
        Me.BlendPanel1.Controls.Add(Me.CMDCOSTSHEET)
        Me.BlendPanel1.Controls.Add(Me.SKDATE)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.CMDSELECTCOMPLAINT)
        Me.BlendPanel1.Controls.Add(Me.CMBCODE)
        Me.BlendPanel1.Controls.Add(Me.TXTNO)
        Me.BlendPanel1.Controls.Add(Me.LBLSRNO)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.TabControl1)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.GroupBox5)
        Me.BlendPanel1.Controls.Add(Me.cmdclear)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.tstxtbillno)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.CMBNAME)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(665, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 14)
        Me.Label1.TabIndex = 810
        Me.Label1.Text = "LR No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTLRNO
        '
        Me.TXTLRNO.BackColor = System.Drawing.Color.White
        Me.TXTLRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLRNO.Location = New System.Drawing.Point(721, 36)
        Me.TXTLRNO.Name = "TXTLRNO"
        Me.TXTLRNO.Size = New System.Drawing.Size(80, 23)
        Me.TXTLRNO.TabIndex = 809
        Me.TXTLRNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMDCOSTSHEET
        '
        Me.CMDCOSTSHEET.BackColor = System.Drawing.Color.Transparent
        Me.CMDCOSTSHEET.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDCOSTSHEET.FlatAppearance.BorderSize = 0
        Me.CMDCOSTSHEET.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDCOSTSHEET.ForeColor = System.Drawing.Color.Black
        Me.CMDCOSTSHEET.Location = New System.Drawing.Point(983, 491)
        Me.CMDCOSTSHEET.Name = "CMDCOSTSHEET"
        Me.CMDCOSTSHEET.Size = New System.Drawing.Size(80, 28)
        Me.CMDCOSTSHEET.TabIndex = 738
        Me.CMDCOSTSHEET.TabStop = False
        Me.CMDCOSTSHEET.Text = "Cost Sheet"
        Me.CMDCOSTSHEET.UseVisualStyleBackColor = False
        '
        'SKDATE
        '
        Me.SKDATE.AsciiOnly = True
        Me.SKDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.SKDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.SKDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.SKDATE.Location = New System.Drawing.Point(868, 65)
        Me.SKDATE.Mask = "00/00/0000"
        Me.SKDATE.Name = "SKDATE"
        Me.SKDATE.Size = New System.Drawing.Size(84, 23)
        Me.SKDATE.TabIndex = 1
        Me.SKDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.SKDATE.ValidatingType = GetType(Date)
        '
        'TXTADD
        '
        Me.TXTADD.BackColor = System.Drawing.Color.White
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.ForeColor = System.Drawing.Color.DimGray
        Me.TXTADD.Location = New System.Drawing.Point(968, 49)
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.ReadOnly = True
        Me.TXTADD.Size = New System.Drawing.Size(23, 22)
        Me.TXTADD.TabIndex = 737
        Me.TXTADD.TabStop = False
        Me.TXTADD.Visible = False
        '
        'CMDSELECTCOMPLAINT
        '
        Me.CMDSELECTCOMPLAINT.BackColor = System.Drawing.Color.Transparent
        Me.CMDSELECTCOMPLAINT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSELECTCOMPLAINT.FlatAppearance.BorderSize = 0
        Me.CMDSELECTCOMPLAINT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSELECTCOMPLAINT.ForeColor = System.Drawing.Color.Black
        Me.CMDSELECTCOMPLAINT.Location = New System.Drawing.Point(413, 457)
        Me.CMDSELECTCOMPLAINT.Name = "CMDSELECTCOMPLAINT"
        Me.CMDSELECTCOMPLAINT.Size = New System.Drawing.Size(107, 28)
        Me.CMDSELECTCOMPLAINT.TabIndex = 3
        Me.CMDSELECTCOMPLAINT.Text = "Select Complaint"
        Me.CMDSELECTCOMPLAINT.UseVisualStyleBackColor = False
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Items.AddRange(New Object() {"C/R", "O/R"})
        Me.CMBCODE.Location = New System.Drawing.Point(1010, 51)
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(51, 22)
        Me.CMBCODE.TabIndex = 721
        Me.CMBCODE.Visible = False
        '
        'TXTNO
        '
        Me.TXTNO.BackColor = System.Drawing.Color.Linen
        Me.TXTNO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTNO.Location = New System.Drawing.Point(868, 37)
        Me.TXTNO.Name = "TXTNO"
        Me.TXTNO.ReadOnly = True
        Me.TXTNO.Size = New System.Drawing.Size(84, 22)
        Me.TXTNO.TabIndex = 0
        Me.TXTNO.TabStop = False
        Me.TXTNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLSRNO
        '
        Me.LBLSRNO.BackColor = System.Drawing.Color.Transparent
        Me.LBLSRNO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLSRNO.ForeColor = System.Drawing.Color.Black
        Me.LBLSRNO.Location = New System.Drawing.Point(816, 41)
        Me.LBLSRNO.Name = "LBLSRNO"
        Me.LBLSRNO.Size = New System.Drawing.Size(50, 14)
        Me.LBLSRNO.TabIndex = 630
        Me.LBLSRNO.Text = "Sr. No"
        Me.LBLSRNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(833, 69)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(33, 14)
        Me.Label9.TabIndex = 622
        Me.Label9.Text = "Date"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(13, 94)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1159, 338)
        Me.TabControl1.TabIndex = 17
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.CMBREGISTER)
        Me.TabPage1.Controls.Add(Me.TXTBILLNO)
        Me.TabPage1.Controls.Add(Me.GRIDSHRINKAGE)
        Me.TabPage1.Controls.Add(Me.LBLTOTALRECDPCS)
        Me.TabPage1.Controls.Add(Me.LBLTOTALRECDMTRS)
        Me.TabPage1.Controls.Add(Me.TXTFROMTYPE)
        Me.TabPage1.Controls.Add(Me.LBLTOTALSMPMTRS)
        Me.TabPage1.Controls.Add(Me.LBLTOTALSHRINKAGE)
        Me.TabPage1.Controls.Add(Me.TXTCOMPLAINT)
        Me.TabPage1.Controls.Add(Me.LBLAVGSHRINKAGE)
        Me.TabPage1.Controls.Add(Me.LBLTOTALBALMTRS)
        Me.TabPage1.Controls.Add(Me.TXTCOMPLAINTDATE)
        Me.TabPage1.Controls.Add(Me.TXTBILLINITIALS)
        Me.TabPage1.Controls.Add(Me.TXTCOMPLAINTBY)
        Me.TabPage1.Controls.Add(Me.txtsrno)
        Me.TabPage1.Controls.Add(Me.LBLTOTALPCS)
        Me.TabPage1.Controls.Add(Me.LBLTOTALBALPCS)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.LBLTOTALMTRS)
        Me.TabPage1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1151, 310)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "1. Item Details"
        '
        'CMBREGISTER
        '
        Me.CMBREGISTER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBREGISTER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBREGISTER.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBREGISTER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBREGISTER.FormattingEnabled = True
        Me.CMBREGISTER.Location = New System.Drawing.Point(732, 1)
        Me.CMBREGISTER.Name = "CMBREGISTER"
        Me.CMBREGISTER.Size = New System.Drawing.Size(150, 23)
        Me.CMBREGISTER.TabIndex = 811
        Me.CMBREGISTER.Visible = False
        '
        'TXTBILLNO
        '
        Me.TXTBILLNO.BackColor = System.Drawing.Color.White
        Me.TXTBILLNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBILLNO.Location = New System.Drawing.Point(648, 1)
        Me.TXTBILLNO.Name = "TXTBILLNO"
        Me.TXTBILLNO.Size = New System.Drawing.Size(84, 23)
        Me.TXTBILLNO.TabIndex = 810
        Me.TXTBILLNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTBILLNO.Visible = False
        '
        'GRIDSHRINKAGE
        '
        Me.GRIDSHRINKAGE.AllowUserToAddRows = False
        Me.GRIDSHRINKAGE.AllowUserToDeleteRows = False
        Me.GRIDSHRINKAGE.AllowUserToResizeColumns = False
        Me.GRIDSHRINKAGE.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDSHRINKAGE.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDSHRINKAGE.BackgroundColor = System.Drawing.Color.White
        Me.GRIDSHRINKAGE.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDSHRINKAGE.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDSHRINKAGE.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDSHRINKAGE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDSHRINKAGE.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GCOMP, Me.GCOMPDATE, Me.GCOMPBY, Me.GBILLINITIALS, Me.GBILLNO, Me.GREGISTER, Me.GTYPE, Me.GFROMNO, Me.GFROMSRNO})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSHRINKAGE.DefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDSHRINKAGE.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDSHRINKAGE.Location = New System.Drawing.Point(2, 23)
        Me.GRIDSHRINKAGE.MultiSelect = False
        Me.GRIDSHRINKAGE.Name = "GRIDSHRINKAGE"
        Me.GRIDSHRINKAGE.RowHeadersVisible = False
        Me.GRIDSHRINKAGE.RowHeadersWidth = 30
        Me.GRIDSHRINKAGE.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDSHRINKAGE.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDSHRINKAGE.RowTemplate.Height = 20
        Me.GRIDSHRINKAGE.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSHRINKAGE.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDSHRINKAGE.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDSHRINKAGE.Size = New System.Drawing.Size(1143, 258)
        Me.GRIDSHRINKAGE.TabIndex = 11
        Me.GRIDSHRINKAGE.TabStop = False
        '
        'LBLTOTALRECDPCS
        '
        Me.LBLTOTALRECDPCS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALRECDPCS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALRECDPCS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALRECDPCS.Location = New System.Drawing.Point(454, 284)
        Me.LBLTOTALRECDPCS.Name = "LBLTOTALRECDPCS"
        Me.LBLTOTALRECDPCS.Size = New System.Drawing.Size(74, 14)
        Me.LBLTOTALRECDPCS.TabIndex = 808
        Me.LBLTOTALRECDPCS.Text = "0"
        Me.LBLTOTALRECDPCS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALRECDMTRS
        '
        Me.LBLTOTALRECDMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALRECDMTRS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALRECDMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALRECDMTRS.Location = New System.Drawing.Point(547, 284)
        Me.LBLTOTALRECDMTRS.Name = "LBLTOTALRECDMTRS"
        Me.LBLTOTALRECDMTRS.Size = New System.Drawing.Size(61, 14)
        Me.LBLTOTALRECDMTRS.TabIndex = 807
        Me.LBLTOTALRECDMTRS.Text = "0"
        Me.LBLTOTALRECDMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTFROMTYPE
        '
        Me.TXTFROMTYPE.BackColor = System.Drawing.Color.White
        Me.TXTFROMTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFROMTYPE.Location = New System.Drawing.Point(882, 1)
        Me.TXTFROMTYPE.Name = "TXTFROMTYPE"
        Me.TXTFROMTYPE.Size = New System.Drawing.Size(150, 23)
        Me.TXTFROMTYPE.TabIndex = 6
        Me.TXTFROMTYPE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTFROMTYPE.Visible = False
        '
        'LBLTOTALSMPMTRS
        '
        Me.LBLTOTALSMPMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALSMPMTRS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALSMPMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALSMPMTRS.Location = New System.Drawing.Point(781, 284)
        Me.LBLTOTALSMPMTRS.Name = "LBLTOTALSMPMTRS"
        Me.LBLTOTALSMPMTRS.Size = New System.Drawing.Size(67, 14)
        Me.LBLTOTALSMPMTRS.TabIndex = 806
        Me.LBLTOTALSMPMTRS.Text = "0.00"
        Me.LBLTOTALSMPMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALSHRINKAGE
        '
        Me.LBLTOTALSHRINKAGE.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALSHRINKAGE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALSHRINKAGE.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALSHRINKAGE.Location = New System.Drawing.Point(879, 284)
        Me.LBLTOTALSHRINKAGE.Name = "LBLTOTALSHRINKAGE"
        Me.LBLTOTALSHRINKAGE.Size = New System.Drawing.Size(56, 14)
        Me.LBLTOTALSHRINKAGE.TabIndex = 805
        Me.LBLTOTALSHRINKAGE.Text = "0.00"
        Me.LBLTOTALSHRINKAGE.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTCOMPLAINT
        '
        Me.TXTCOMPLAINT.BackColor = System.Drawing.Color.White
        Me.TXTCOMPLAINT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOMPLAINT.Location = New System.Drawing.Point(33, 1)
        Me.TXTCOMPLAINT.Name = "TXTCOMPLAINT"
        Me.TXTCOMPLAINT.Size = New System.Drawing.Size(277, 23)
        Me.TXTCOMPLAINT.TabIndex = 1
        Me.TXTCOMPLAINT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTCOMPLAINT.Visible = False
        '
        'LBLAVGSHRINKAGE
        '
        Me.LBLAVGSHRINKAGE.BackColor = System.Drawing.Color.Transparent
        Me.LBLAVGSHRINKAGE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLAVGSHRINKAGE.ForeColor = System.Drawing.Color.Black
        Me.LBLAVGSHRINKAGE.Location = New System.Drawing.Point(945, 284)
        Me.LBLAVGSHRINKAGE.Name = "LBLAVGSHRINKAGE"
        Me.LBLAVGSHRINKAGE.Size = New System.Drawing.Size(56, 14)
        Me.LBLAVGSHRINKAGE.TabIndex = 804
        Me.LBLAVGSHRINKAGE.Text = "0.00"
        Me.LBLAVGSHRINKAGE.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALBALMTRS
        '
        Me.LBLTOTALBALMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALBALMTRS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALBALMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALBALMTRS.Location = New System.Drawing.Point(701, 284)
        Me.LBLTOTALBALMTRS.Name = "LBLTOTALBALMTRS"
        Me.LBLTOTALBALMTRS.Size = New System.Drawing.Size(67, 14)
        Me.LBLTOTALBALMTRS.TabIndex = 803
        Me.LBLTOTALBALMTRS.Text = "0.00"
        Me.LBLTOTALBALMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTCOMPLAINTDATE
        '
        Me.TXTCOMPLAINTDATE.AsciiOnly = True
        Me.TXTCOMPLAINTDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTCOMPLAINTDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOMPLAINTDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.TXTCOMPLAINTDATE.Location = New System.Drawing.Point(310, 1)
        Me.TXTCOMPLAINTDATE.Mask = "00/00/0000"
        Me.TXTCOMPLAINTDATE.Name = "TXTCOMPLAINTDATE"
        Me.TXTCOMPLAINTDATE.Size = New System.Drawing.Size(85, 23)
        Me.TXTCOMPLAINTDATE.TabIndex = 2
        Me.TXTCOMPLAINTDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.TXTCOMPLAINTDATE.ValidatingType = GetType(Date)
        Me.TXTCOMPLAINTDATE.Visible = False
        '
        'TXTBILLINITIALS
        '
        Me.TXTBILLINITIALS.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTBILLINITIALS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBILLINITIALS.Location = New System.Drawing.Point(528, 1)
        Me.TXTBILLINITIALS.Name = "TXTBILLINITIALS"
        Me.TXTBILLINITIALS.Size = New System.Drawing.Size(120, 23)
        Me.TXTBILLINITIALS.TabIndex = 5
        Me.TXTBILLINITIALS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTBILLINITIALS.Visible = False
        '
        'TXTCOMPLAINTBY
        '
        Me.TXTCOMPLAINTBY.BackColor = System.Drawing.Color.White
        Me.TXTCOMPLAINTBY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOMPLAINTBY.Location = New System.Drawing.Point(395, 1)
        Me.TXTCOMPLAINTBY.Name = "TXTCOMPLAINTBY"
        Me.TXTCOMPLAINTBY.Size = New System.Drawing.Size(133, 23)
        Me.TXTCOMPLAINTBY.TabIndex = 4
        Me.TXTCOMPLAINTBY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTCOMPLAINTBY.Visible = False
        '
        'txtsrno
        '
        Me.txtsrno.BackColor = System.Drawing.Color.Linen
        Me.txtsrno.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrno.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtsrno.Location = New System.Drawing.Point(3, 1)
        Me.txtsrno.Name = "txtsrno"
        Me.txtsrno.ReadOnly = True
        Me.txtsrno.Size = New System.Drawing.Size(30, 23)
        Me.txtsrno.TabIndex = 0
        Me.txtsrno.TabStop = False
        Me.txtsrno.Visible = False
        '
        'LBLTOTALPCS
        '
        Me.LBLTOTALPCS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALPCS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALPCS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALPCS.Location = New System.Drawing.Point(294, 284)
        Me.LBLTOTALPCS.Name = "LBLTOTALPCS"
        Me.LBLTOTALPCS.Size = New System.Drawing.Size(74, 14)
        Me.LBLTOTALPCS.TabIndex = 686
        Me.LBLTOTALPCS.Text = "0"
        Me.LBLTOTALPCS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALBALPCS
        '
        Me.LBLTOTALBALPCS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALBALPCS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALBALPCS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALBALPCS.Location = New System.Drawing.Point(611, 284)
        Me.LBLTOTALBALPCS.Name = "LBLTOTALBALPCS"
        Me.LBLTOTALBALPCS.Size = New System.Drawing.Size(77, 14)
        Me.LBLTOTALBALPCS.TabIndex = 684
        Me.LBLTOTALBALPCS.Text = "0.00"
        Me.LBLTOTALBALPCS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(226, 284)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 14)
        Me.Label10.TabIndex = 683
        Me.Label10.Text = "Total"
        '
        'LBLTOTALMTRS
        '
        Me.LBLTOTALMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALMTRS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALMTRS.Location = New System.Drawing.Point(387, 284)
        Me.LBLTOTALMTRS.Name = "LBLTOTALMTRS"
        Me.LBLTOTALMTRS.Size = New System.Drawing.Size(61, 14)
        Me.LBLTOTALMTRS.TabIndex = 685
        Me.LBLTOTALMTRS.Text = "0"
        Me.LBLTOTALMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmddelete.FlatAppearance.BorderSize = 0
        Me.cmddelete.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddelete.ForeColor = System.Drawing.Color.Black
        Me.cmddelete.Location = New System.Drawing.Point(502, 491)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 28)
        Me.cmddelete.TabIndex = 6
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.txtremarks)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(23, 440)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(230, 79)
        Me.GroupBox5.TabIndex = 24
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Remarks"
        '
        'txtremarks
        '
        Me.txtremarks.ForeColor = System.Drawing.Color.DimGray
        Me.txtremarks.Location = New System.Drawing.Point(7, 16)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(217, 57)
        Me.txtremarks.TabIndex = 0
        Me.txtremarks.TabStop = False
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.Color.Transparent
        Me.cmdclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdclear.FlatAppearance.BorderSize = 0
        Me.cmdclear.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.ForeColor = System.Drawing.Color.Black
        Me.cmdclear.Location = New System.Drawing.Point(415, 491)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(80, 28)
        Me.cmdclear.TabIndex = 5
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(539, 457)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 4
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
        Me.cmdexit.Location = New System.Drawing.Point(589, 491)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 7
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'tstxtbillno
        '
        Me.tstxtbillno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstxtbillno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstxtbillno.Location = New System.Drawing.Point(255, 1)
        Me.tstxtbillno.Name = "tstxtbillno"
        Me.tstxtbillno.Size = New System.Drawing.Size(66, 22)
        Me.tstxtbillno.TabIndex = 25
        Me.tstxtbillno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(34, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 14)
        Me.Label6.TabIndex = 434
        Me.Label6.Text = "Party Name"
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(104, 38)
        Me.CMBNAME.MaxDropDownItems = 14
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(234, 22)
        Me.CMBNAME.TabIndex = 2
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.tooldelete, Me.TOOLSMS, Me.toolStripSeparator, Me.TOOLPRIVIOUS, Me.toolnext, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 610
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
        'TOOLSMS
        '
        Me.TOOLSMS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLSMS.Image = Global.TEXTRADE.My.Resources.Resources.SMS2
        Me.TOOLSMS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLSMS.Name = "TOOLSMS"
        Me.TOOLSMS.Size = New System.Drawing.Size(23, 22)
        Me.TOOLSMS.Text = "&SMS"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'TOOLPRIVIOUS
        '
        Me.TOOLPRIVIOUS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TOOLPRIVIOUS.Image = Global.TEXTRADE.My.Resources.Resources.POINT021
        Me.TOOLPRIVIOUS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLPRIVIOUS.Name = "TOOLPRIVIOUS"
        Me.TOOLPRIVIOUS.Size = New System.Drawing.Size(73, 22)
        Me.TOOLPRIVIOUS.Text = "Previous"
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
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'GSRNO
        '
        Me.GSRNO.HeaderText = "Sr."
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.ReadOnly = True
        Me.GSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSRNO.Width = 30
        '
        'GCOMP
        '
        Me.GCOMP.HeaderText = "Complaint"
        Me.GCOMP.Name = "GCOMP"
        Me.GCOMP.ReadOnly = True
        Me.GCOMP.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCOMP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCOMP.Width = 277
        '
        'GCOMPDATE
        '
        Me.GCOMPDATE.HeaderText = "Date"
        Me.GCOMPDATE.Name = "GCOMPDATE"
        Me.GCOMPDATE.ReadOnly = True
        Me.GCOMPDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCOMPDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCOMPDATE.Width = 85
        '
        'GCOMPBY
        '
        Me.GCOMPBY.HeaderText = "Complaint By"
        Me.GCOMPBY.Name = "GCOMPBY"
        Me.GCOMPBY.ReadOnly = True
        Me.GCOMPBY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCOMPBY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCOMPBY.Width = 133
        '
        'GBILLINITIALS
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GBILLINITIALS.DefaultCellStyle = DataGridViewCellStyle3
        Me.GBILLINITIALS.HeaderText = "Bill Initials"
        Me.GBILLINITIALS.Name = "GBILLINITIALS"
        Me.GBILLINITIALS.Width = 120
        '
        'GBILLNO
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GBILLNO.DefaultCellStyle = DataGridViewCellStyle4
        Me.GBILLNO.HeaderText = "Bill No"
        Me.GBILLNO.Name = "GBILLNO"
        Me.GBILLNO.ReadOnly = True
        Me.GBILLNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBILLNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBILLNO.Width = 84
        '
        'GREGISTER
        '
        Me.GREGISTER.HeaderText = "Register"
        Me.GREGISTER.Name = "GREGISTER"
        Me.GREGISTER.Width = 150
        '
        'GTYPE
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GTYPE.DefaultCellStyle = DataGridViewCellStyle5
        Me.GTYPE.HeaderText = "Type"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.ReadOnly = True
        Me.GTYPE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GTYPE.Width = 150
        '
        'GFROMNO
        '
        Me.GFROMNO.HeaderText = "From No"
        Me.GFROMNO.Name = "GFROMNO"
        Me.GFROMNO.ReadOnly = True
        Me.GFROMNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GFROMNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GFROMNO.Visible = False
        Me.GFROMNO.Width = 80
        '
        'GFROMSRNO
        '
        Me.GFROMSRNO.HeaderText = "From Sr No"
        Me.GFROMSRNO.Name = "GFROMSRNO"
        Me.GFROMSRNO.ReadOnly = True
        Me.GFROMSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GFROMSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GFROMSRNO.Visible = False
        Me.GFROMSRNO.Width = 80
        '
        'ComplaintSolved
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "ComplaintSolved"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Update Complaint"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.GRIDSHRINKAGE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents TXTLRNO As TextBox
    Friend WithEvents CMDCOSTSHEET As Button
    Friend WithEvents SKDATE As MaskedTextBox
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents CMDSELECTCOMPLAINT As Button
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents TXTNO As TextBox
    Friend WithEvents LBLSRNO As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents GRIDSHRINKAGE As DataGridView
    Friend WithEvents LBLTOTALRECDPCS As Label
    Friend WithEvents LBLTOTALRECDMTRS As Label
    Friend WithEvents TXTFROMTYPE As TextBox
    Friend WithEvents LBLTOTALSMPMTRS As Label
    Friend WithEvents LBLTOTALSHRINKAGE As Label
    Friend WithEvents TXTCOMPLAINT As TextBox
    Friend WithEvents LBLAVGSHRINKAGE As Label
    Friend WithEvents LBLTOTALBALMTRS As Label
    Friend WithEvents TXTCOMPLAINTDATE As MaskedTextBox
    Friend WithEvents TXTBILLINITIALS As TextBox
    Friend WithEvents TXTCOMPLAINTBY As TextBox
    Friend WithEvents txtsrno As TextBox
    Friend WithEvents LBLTOTALPCS As Label
    Friend WithEvents LBLTOTALBALPCS As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents LBLTOTALMTRS As Label
    Friend WithEvents cmddelete As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents txtremarks As TextBox
    Friend WithEvents cmdclear As Button
    Friend WithEvents cmdok As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents tstxtbillno As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CMBNAME As ComboBox
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents SaveToolStripButton As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents tooldelete As ToolStripButton
    Friend WithEvents TOOLSMS As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents TOOLPRIVIOUS As ToolStripButton
    Friend WithEvents toolnext As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents TXTBILLNO As TextBox
    Friend WithEvents CMBREGISTER As ComboBox
    Friend WithEvents GSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GCOMP As DataGridViewTextBoxColumn
    Friend WithEvents GCOMPDATE As DataGridViewTextBoxColumn
    Friend WithEvents GCOMPBY As DataGridViewTextBoxColumn
    Friend WithEvents GBILLINITIALS As DataGridViewTextBoxColumn
    Friend WithEvents GBILLNO As DataGridViewTextBoxColumn
    Friend WithEvents GREGISTER As DataGridViewTextBoxColumn
    Friend WithEvents GTYPE As DataGridViewTextBoxColumn
    Friend WithEvents GFROMNO As DataGridViewTextBoxColumn
    Friend WithEvents GFROMSRNO As DataGridViewTextBoxColumn
End Class
