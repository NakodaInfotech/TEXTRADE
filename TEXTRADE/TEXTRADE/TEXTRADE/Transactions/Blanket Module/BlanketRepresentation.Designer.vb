<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BlanketRepresentation
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BlanketRepresentation))
        Me.BLENDPANEL1 = New VbPowerPack.BlendPanel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.txtadd = New System.Windows.Forms.TextBox()
        Me.CMBBLANKETNAME = New System.Windows.Forms.ComboBox()
        Me.GRIDBLANKETDESIGN = New System.Windows.Forms.DataGridView()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.DTDATE = New System.Windows.Forms.MaskedTextBox()
        Me.TXTSRNO = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GRIDBLANKET = New System.Windows.Forms.DataGridView()
        Me.ESRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EBLANKET = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CMBSALESMAN = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TXTBLANKETNO = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.CMBAGENTNAME = New System.Windows.Forms.ComboBox()
        Me.lbl = New System.Windows.Forms.Label()
        Me.CMDDELETE = New System.Windows.Forms.Button()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMBPARTYNAME = New System.Windows.Forms.ComboBox()
        Me.lblsrno = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.toolprevious = New System.Windows.Forms.ToolStripButton()
        Me.toolnext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Ep = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDESIGN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAPPROVAL = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.GMAINSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BLENDPANEL1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.GRIDBLANKETDESIGN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDBLANKET, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.Ep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BLENDPANEL1
        '
        Me.BLENDPANEL1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BLENDPANEL1.Controls.Add(Me.GroupBox5)
        Me.BLENDPANEL1.Controls.Add(Me.txtadd)
        Me.BLENDPANEL1.Controls.Add(Me.CMBBLANKETNAME)
        Me.BLENDPANEL1.Controls.Add(Me.GRIDBLANKETDESIGN)
        Me.BLENDPANEL1.Controls.Add(Me.CMBCODE)
        Me.BLENDPANEL1.Controls.Add(Me.CMDCLEAR)
        Me.BLENDPANEL1.Controls.Add(Me.DTDATE)
        Me.BLENDPANEL1.Controls.Add(Me.TXTSRNO)
        Me.BLENDPANEL1.Controls.Add(Me.Label2)
        Me.BLENDPANEL1.Controls.Add(Me.GRIDBLANKET)
        Me.BLENDPANEL1.Controls.Add(Me.CMBSALESMAN)
        Me.BLENDPANEL1.Controls.Add(Me.Label19)
        Me.BLENDPANEL1.Controls.Add(Me.TXTBLANKETNO)
        Me.BLENDPANEL1.Controls.Add(Me.Label24)
        Me.BLENDPANEL1.Controls.Add(Me.CMBAGENTNAME)
        Me.BLENDPANEL1.Controls.Add(Me.lbl)
        Me.BLENDPANEL1.Controls.Add(Me.CMDDELETE)
        Me.BLENDPANEL1.Controls.Add(Me.CMDOK)
        Me.BLENDPANEL1.Controls.Add(Me.CMDEXIT)
        Me.BLENDPANEL1.Controls.Add(Me.CMBPARTYNAME)
        Me.BLENDPANEL1.Controls.Add(Me.lblsrno)
        Me.BLENDPANEL1.Controls.Add(Me.Label3)
        Me.BLENDPANEL1.Controls.Add(Me.ToolStrip1)
        Me.BLENDPANEL1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BLENDPANEL1.Location = New System.Drawing.Point(0, 0)
        Me.BLENDPANEL1.Name = "BLENDPANEL1"
        Me.BLENDPANEL1.Size = New System.Drawing.Size(1040, 640)
        Me.BLENDPANEL1.TabIndex = 0
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.TXTREMARKS)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(22, 510)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(305, 118)
        Me.GroupBox5.TabIndex = 11
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Remarks"
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.ForeColor = System.Drawing.Color.DimGray
        Me.TXTREMARKS.Location = New System.Drawing.Point(6, 16)
        Me.TXTREMARKS.MaxLength = 200
        Me.TXTREMARKS.Multiline = True
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(293, 96)
        Me.TXTREMARKS.TabIndex = 0
        Me.TXTREMARKS.TabStop = False
        '
        'txtadd
        '
        Me.txtadd.BackColor = System.Drawing.Color.White
        Me.txtadd.ForeColor = System.Drawing.Color.DimGray
        Me.txtadd.Location = New System.Drawing.Point(12, 252)
        Me.txtadd.Multiline = True
        Me.txtadd.Name = "txtadd"
        Me.txtadd.ReadOnly = True
        Me.txtadd.Size = New System.Drawing.Size(23, 19)
        Me.txtadd.TabIndex = 979
        Me.txtadd.Visible = False
        '
        'CMBBLANKETNAME
        '
        Me.CMBBLANKETNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBBLANKETNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBBLANKETNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBBLANKETNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBBLANKETNAME.FormattingEnabled = True
        Me.CMBBLANKETNAME.Location = New System.Drawing.Point(111, 186)
        Me.CMBBLANKETNAME.Name = "CMBBLANKETNAME"
        Me.CMBBLANKETNAME.Size = New System.Drawing.Size(150, 23)
        Me.CMBBLANKETNAME.TabIndex = 4
        '
        'GRIDBLANKETDESIGN
        '
        Me.GRIDBLANKETDESIGN.AllowUserToAddRows = False
        Me.GRIDBLANKETDESIGN.AllowUserToDeleteRows = False
        Me.GRIDBLANKETDESIGN.AllowUserToResizeColumns = False
        Me.GRIDBLANKETDESIGN.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDBLANKETDESIGN.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDBLANKETDESIGN.BackgroundColor = System.Drawing.Color.White
        Me.GRIDBLANKETDESIGN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDBLANKETDESIGN.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDBLANKETDESIGN.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDBLANKETDESIGN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDBLANKETDESIGN.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GDESIGN, Me.GREMARKS, Me.GAPPROVAL, Me.GMAINSRNO})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDBLANKETDESIGN.DefaultCellStyle = DataGridViewCellStyle3
        Me.GRIDBLANKETDESIGN.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDBLANKETDESIGN.Location = New System.Drawing.Point(333, 38)
        Me.GRIDBLANKETDESIGN.MultiSelect = False
        Me.GRIDBLANKETDESIGN.Name = "GRIDBLANKETDESIGN"
        Me.GRIDBLANKETDESIGN.RowHeadersVisible = False
        Me.GRIDBLANKETDESIGN.RowHeadersWidth = 30
        Me.GRIDBLANKETDESIGN.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDBLANKETDESIGN.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.GRIDBLANKETDESIGN.RowTemplate.Height = 20
        Me.GRIDBLANKETDESIGN.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDBLANKETDESIGN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDBLANKETDESIGN.Size = New System.Drawing.Size(683, 540)
        Me.GRIDBLANKETDESIGN.TabIndex = 10
        Me.GRIDBLANKETDESIGN.TabStop = False
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.BackColor = System.Drawing.Color.White
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Location = New System.Drawing.Point(12, 276)
        Me.CMBCODE.MaxDropDownItems = 14
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(23, 22)
        Me.CMBCODE.TabIndex = 978
        Me.CMBCODE.Visible = False
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDCLEAR.FlatAppearance.BorderSize = 0
        Me.CMDCLEAR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.Black
        Me.CMDCLEAR.Location = New System.Drawing.Point(438, 600)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 13
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
        '
        'DTDATE
        '
        Me.DTDATE.AsciiOnly = True
        Me.DTDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.DTDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTDATE.Location = New System.Drawing.Point(224, 70)
        Me.DTDATE.Mask = "00/00/0000"
        Me.DTDATE.Name = "DTDATE"
        Me.DTDATE.Size = New System.Drawing.Size(84, 23)
        Me.DTDATE.TabIndex = 975
        Me.DTDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTDATE.ValidatingType = GetType(Date)
        '
        'TXTSRNO
        '
        Me.TXTSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTSRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSRNO.Location = New System.Drawing.Point(81, 186)
        Me.TXTSRNO.Name = "TXTSRNO"
        Me.TXTSRNO.ReadOnly = True
        Me.TXTSRNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTSRNO.TabIndex = 3
        Me.TXTSRNO.TabStop = False
        Me.TXTSRNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(188, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 15)
        Me.Label2.TabIndex = 976
        Me.Label2.Text = "Date"
        '
        'GRIDBLANKET
        '
        Me.GRIDBLANKET.AllowUserToAddRows = False
        Me.GRIDBLANKET.AllowUserToDeleteRows = False
        Me.GRIDBLANKET.AllowUserToResizeColumns = False
        Me.GRIDBLANKET.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GRIDBLANKET.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDBLANKET.BackgroundColor = System.Drawing.Color.White
        Me.GRIDBLANKET.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDBLANKET.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDBLANKET.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDBLANKET.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDBLANKET.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ESRNO, Me.EBLANKET})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDBLANKET.DefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDBLANKET.GridColor = System.Drawing.SystemColors.ControlText
        Me.GRIDBLANKET.Location = New System.Drawing.Point(81, 208)
        Me.GRIDBLANKET.Margin = New System.Windows.Forms.Padding(2)
        Me.GRIDBLANKET.MultiSelect = False
        Me.GRIDBLANKET.Name = "GRIDBLANKET"
        Me.GRIDBLANKET.ReadOnly = True
        Me.GRIDBLANKET.RowHeadersVisible = False
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDBLANKET.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.GRIDBLANKET.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDBLANKET.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDBLANKET.Size = New System.Drawing.Size(215, 297)
        Me.GRIDBLANKET.TabIndex = 5
        '
        'ESRNO
        '
        Me.ESRNO.HeaderText = "Sr"
        Me.ESRNO.Name = "ESRNO"
        Me.ESRNO.ReadOnly = True
        Me.ESRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ESRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ESRNO.Width = 30
        '
        'EBLANKET
        '
        Me.EBLANKET.HeaderText = "Blanket Name"
        Me.EBLANKET.Name = "EBLANKET"
        Me.EBLANKET.ReadOnly = True
        Me.EBLANKET.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.EBLANKET.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.EBLANKET.Width = 150
        '
        'CMBSALESMAN
        '
        Me.CMBSALESMAN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSALESMAN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSALESMAN.BackColor = System.Drawing.SystemColors.Window
        Me.CMBSALESMAN.FormattingEnabled = True
        Me.CMBSALESMAN.Location = New System.Drawing.Point(81, 157)
        Me.CMBSALESMAN.Name = "CMBSALESMAN"
        Me.CMBSALESMAN.Size = New System.Drawing.Size(227, 23)
        Me.CMBSALESMAN.TabIndex = 2
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(19, 160)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(60, 15)
        Me.Label19.TabIndex = 680
        Me.Label19.Text = "Salesman"
        '
        'TXTBLANKETNO
        '
        Me.TXTBLANKETNO.BackColor = System.Drawing.Color.Linen
        Me.TXTBLANKETNO.Location = New System.Drawing.Point(81, 70)
        Me.TXTBLANKETNO.Name = "TXTBLANKETNO"
        Me.TXTBLANKETNO.ReadOnly = True
        Me.TXTBLANKETNO.Size = New System.Drawing.Size(84, 23)
        Me.TXTBLANKETNO.TabIndex = 8
        Me.TXTBLANKETNO.TabStop = False
        Me.TXTBLANKETNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(8, 130)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(71, 15)
        Me.Label24.TabIndex = 672
        Me.Label24.Text = "Agent Name"
        '
        'CMBAGENTNAME
        '
        Me.CMBAGENTNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBAGENTNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBAGENTNAME.BackColor = System.Drawing.SystemColors.Window
        Me.CMBAGENTNAME.FormattingEnabled = True
        Me.CMBAGENTNAME.Location = New System.Drawing.Point(81, 127)
        Me.CMBAGENTNAME.Name = "CMBAGENTNAME"
        Me.CMBAGENTNAME.Size = New System.Drawing.Size(227, 23)
        Me.CMBAGENTNAME.TabIndex = 1
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl.Location = New System.Drawing.Point(12, 28)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(188, 19)
        Me.lbl.TabIndex = 312
        Me.lbl.Text = "Blanket Represantation"
        '
        'CMDDELETE
        '
        Me.CMDDELETE.BackColor = System.Drawing.Color.Transparent
        Me.CMDDELETE.FlatAppearance.BorderSize = 0
        Me.CMDDELETE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDELETE.ForeColor = System.Drawing.Color.Black
        Me.CMDDELETE.Location = New System.Drawing.Point(522, 600)
        Me.CMDDELETE.Name = "CMDDELETE"
        Me.CMDDELETE.Size = New System.Drawing.Size(80, 28)
        Me.CMDDELETE.TabIndex = 14
        Me.CMDDELETE.Text = "&Delete"
        Me.CMDDELETE.UseVisualStyleBackColor = False
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(352, 600)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 12
        Me.CMDOK.Text = "&Save"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(608, 600)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 15
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'CMBPARTYNAME
        '
        Me.CMBPARTYNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBPARTYNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBPARTYNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBPARTYNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBPARTYNAME.FormattingEnabled = True
        Me.CMBPARTYNAME.Location = New System.Drawing.Point(81, 99)
        Me.CMBPARTYNAME.MaxDropDownItems = 14
        Me.CMBPARTYNAME.Name = "CMBPARTYNAME"
        Me.CMBPARTYNAME.Size = New System.Drawing.Size(227, 22)
        Me.CMBPARTYNAME.TabIndex = 0
        '
        'lblsrno
        '
        Me.lblsrno.AutoSize = True
        Me.lblsrno.BackColor = System.Drawing.Color.Transparent
        Me.lblsrno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsrno.Location = New System.Drawing.Point(35, 74)
        Me.lblsrno.Name = "lblsrno"
        Me.lblsrno.Size = New System.Drawing.Size(40, 14)
        Me.lblsrno.TabIndex = 314
        Me.lblsrno.Text = "Sr. No."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(40, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 14)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Name"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.ToolStripButton4, Me.toolStripSeparator, Me.toolprevious, Me.toolnext, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1040, 25)
        Me.ToolStrip1.TabIndex = 977
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
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton4.Text = "&Delete"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'toolprevious
        '
        Me.toolprevious.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toolprevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolprevious.Name = "toolprevious"
        Me.toolprevious.Size = New System.Drawing.Size(57, 22)
        Me.toolprevious.Text = "Previous"
        '
        'toolnext
        '
        Me.toolnext.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toolnext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolnext.Name = "toolnext"
        Me.toolnext.Size = New System.Drawing.Size(35, 22)
        Me.toolnext.Text = "Next"
        Me.toolnext.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'Ep
        '
        Me.Ep.BlinkRate = 0
        Me.Ep.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.Ep.ContainerControl = Me
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
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
        'GDESIGN
        '
        Me.GDESIGN.HeaderText = "Design"
        Me.GDESIGN.Name = "GDESIGN"
        Me.GDESIGN.ReadOnly = True
        Me.GDESIGN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDESIGN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDESIGN.Width = 200
        '
        'GREMARKS
        '
        Me.GREMARKS.HeaderText = "Remarks"
        Me.GREMARKS.MaxInputLength = 200
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GREMARKS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GREMARKS.Width = 300
        '
        'GAPPROVAL
        '
        Me.GAPPROVAL.HeaderText = "Approval"
        Me.GAPPROVAL.Name = "GAPPROVAL"
        Me.GAPPROVAL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GAPPROVAL.Width = 113
        '
        'GMAINSRNO
        '
        Me.GMAINSRNO.HeaderText = "MAINSRNO"
        Me.GMAINSRNO.Name = "GMAINSRNO"
        Me.GMAINSRNO.ReadOnly = True
        Me.GMAINSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMAINSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMAINSRNO.Visible = False
        '
        'BlanketRepresentation
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1040, 640)
        Me.Controls.Add(Me.BLENDPANEL1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "BlanketRepresentation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Blanket Representation"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BLENDPANEL1.ResumeLayout(False)
        Me.BLENDPANEL1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.GRIDBLANKETDESIGN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDBLANKET, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.Ep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BLENDPANEL1 As VbPowerPack.BlendPanel
    Friend WithEvents CMBSALESMAN As ComboBox
    Friend WithEvents Label19 As Label
    Friend WithEvents CMBBLANKETNAME As ComboBox
    Friend WithEvents GRIDBLANKETDESIGN As DataGridView
    Friend WithEvents GWEFTQUALITY As DataGridViewTextBoxColumn
    Friend WithEvents GSHADE As DataGridViewTextBoxColumn
    Friend WithEvents GPICK As DataGridViewTextBoxColumn
    Friend WithEvents GWTPER As DataGridViewTextBoxColumn
    Friend WithEvents GWEFTSRNO As DataGridViewTextBoxColumn
    Friend WithEvents TXTSRNO As TextBox
    Friend WithEvents GRIDBLANKET As DataGridView
    Friend WithEvents TXTBLANKETNO As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents CMBAGENTNAME As ComboBox
    Friend WithEvents lbl As Label
    Friend WithEvents CMDDELETE As Button
    Friend WithEvents CMDOK As Button
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMBPARTYNAME As ComboBox
    Friend WithEvents lblsrno As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Ep As ErrorProvider
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents DTDATE As MaskedTextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CMDCLEAR As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents TXTREMARKS As TextBox
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents SaveToolStripButton As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents ToolStripButton4 As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents toolprevious As ToolStripButton
    Friend WithEvents toolnext As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents txtadd As TextBox
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents ESRNO As DataGridViewTextBoxColumn
    Friend WithEvents EBLANKET As DataGridViewTextBoxColumn
    Friend WithEvents GSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GDESIGN As DataGridViewTextBoxColumn
    Friend WithEvents GREMARKS As DataGridViewTextBoxColumn
    Friend WithEvents GAPPROVAL As DataGridViewComboBoxColumn
    Friend WithEvents GMAINSRNO As DataGridViewTextBoxColumn
End Class
