<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SampleOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SampleOrder))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.LBLWHATSAPP = New System.Windows.Forms.Label()
        Me.CMDSELECTSTOCK = New System.Windows.Forms.Button()
        Me.CHKSELF = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TXTCOPY = New System.Windows.Forms.TextBox()
        Me.CMBSAMPLETYPE = New System.Windows.Forms.ComboBox()
        Me.LBLCLOSED = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PBlock = New System.Windows.Forms.PictureBox()
        Me.lbllocked = New System.Windows.Forms.Label()
        Me.CMBAGENT = New System.Windows.Forms.ComboBox()
        Me.TOTALNOOFBOOKLET = New System.Windows.Forms.Label()
        Me.TXTNOOFBOOKLET = New System.Windows.Forms.TextBox()
        Me.CMBQUALITY = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CMBCOLOR = New System.Windows.Forms.ComboBox()
        Me.TXTBARCODE = New System.Windows.Forms.TextBox()
        Me.CMBDESIGN = New System.Windows.Forms.ComboBox()
        Me.lblbarcode = New System.Windows.Forms.Label()
        Me.TXTNARRATION = New System.Windows.Forms.TextBox()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.GRIDSO = New System.Windows.Forms.DataGridView()
        Me.gsrno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSAMPLETYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gitemname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GQUALITY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDESIGN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCOLOR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNOOFBOOKLET = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNARRATION = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCLOSED = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.CMBITEMNAME = New System.Windows.Forms.ComboBox()
        Me.txtbillno = New System.Windows.Forms.TextBox()
        Me.TXTSRNO = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.TOOLWHATSAPP = New System.Windows.Forms.ToolStripButton()
        Me.TOOLDELETE = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolPREVIOUS = New System.Windows.Forms.ToolStripButton()
        Me.Toolnext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ORDERDATE = New System.Windows.Forms.MaskedTextBox()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.cmdEXIT = New System.Windows.Forms.Button()
        Me.CMBPARTYNAME = New System.Windows.Forms.ComboBox()
        Me.TXTMODEOFSHIPMENT = New System.Windows.Forms.TextBox()
        Me.TXTORDERNO = New System.Windows.Forms.TextBox()
        Me.LBLMODEOFSHIPMENT = New System.Windows.Forms.Label()
        Me.LBLDATE = New System.Windows.Forms.Label()
        Me.LBLSRNO = New System.Windows.Forms.Label()
        Me.lblpartyname = New System.Windows.Forms.Label()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TXTSONO = New System.Windows.Forms.TextBox()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.PBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDSO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.LBLWHATSAPP)
        Me.BlendPanel1.Controls.Add(Me.TXTSONO)
        Me.BlendPanel1.Controls.Add(Me.CMDSELECTSTOCK)
        Me.BlendPanel1.Controls.Add(Me.CHKSELF)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Controls.Add(Me.TXTCOPY)
        Me.BlendPanel1.Controls.Add(Me.CMBSAMPLETYPE)
        Me.BlendPanel1.Controls.Add(Me.LBLCLOSED)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.PBlock)
        Me.BlendPanel1.Controls.Add(Me.lbllocked)
        Me.BlendPanel1.Controls.Add(Me.CMBAGENT)
        Me.BlendPanel1.Controls.Add(Me.TOTALNOOFBOOKLET)
        Me.BlendPanel1.Controls.Add(Me.TXTNOOFBOOKLET)
        Me.BlendPanel1.Controls.Add(Me.CMBQUALITY)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.CMBCOLOR)
        Me.BlendPanel1.Controls.Add(Me.TXTBARCODE)
        Me.BlendPanel1.Controls.Add(Me.CMBDESIGN)
        Me.BlendPanel1.Controls.Add(Me.lblbarcode)
        Me.BlendPanel1.Controls.Add(Me.TXTNARRATION)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.CMBCODE)
        Me.BlendPanel1.Controls.Add(Me.GRIDSO)
        Me.BlendPanel1.Controls.Add(Me.GroupBox5)
        Me.BlendPanel1.Controls.Add(Me.CMBITEMNAME)
        Me.BlendPanel1.Controls.Add(Me.txtbillno)
        Me.BlendPanel1.Controls.Add(Me.TXTSRNO)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.ORDERDATE)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.cmdclear)
        Me.BlendPanel1.Controls.Add(Me.cmdEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMBPARTYNAME)
        Me.BlendPanel1.Controls.Add(Me.TXTMODEOFSHIPMENT)
        Me.BlendPanel1.Controls.Add(Me.TXTORDERNO)
        Me.BlendPanel1.Controls.Add(Me.LBLMODEOFSHIPMENT)
        Me.BlendPanel1.Controls.Add(Me.LBLDATE)
        Me.BlendPanel1.Controls.Add(Me.LBLSRNO)
        Me.BlendPanel1.Controls.Add(Me.lblpartyname)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1155, 581)
        Me.BlendPanel1.TabIndex = 0
        '
        'LBLWHATSAPP
        '
        Me.LBLWHATSAPP.AutoSize = True
        Me.LBLWHATSAPP.BackColor = System.Drawing.Color.Transparent
        Me.LBLWHATSAPP.Font = New System.Drawing.Font("Calibri", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLWHATSAPP.ForeColor = System.Drawing.Color.Black
        Me.LBLWHATSAPP.Location = New System.Drawing.Point(760, 2)
        Me.LBLWHATSAPP.Name = "LBLWHATSAPP"
        Me.LBLWHATSAPP.Size = New System.Drawing.Size(205, 36)
        Me.LBLWHATSAPP.TabIndex = 923
        Me.LBLWHATSAPP.Text = "WhatsApp Sent"
        Me.LBLWHATSAPP.Visible = False
        '
        'CMDSELECTSTOCK
        '
        Me.CMDSELECTSTOCK.BackColor = System.Drawing.Color.Transparent
        Me.CMDSELECTSTOCK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSELECTSTOCK.FlatAppearance.BorderSize = 0
        Me.CMDSELECTSTOCK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSELECTSTOCK.ForeColor = System.Drawing.Color.Black
        Me.CMDSELECTSTOCK.Location = New System.Drawing.Point(465, 482)
        Me.CMDSELECTSTOCK.Name = "CMDSELECTSTOCK"
        Me.CMDSELECTSTOCK.Size = New System.Drawing.Size(80, 28)
        Me.CMDSELECTSTOCK.TabIndex = 749
        Me.CMDSELECTSTOCK.Text = "SelectStock"
        Me.CMDSELECTSTOCK.UseVisualStyleBackColor = False
        '
        'CHKSELF
        '
        Me.CHKSELF.AutoSize = True
        Me.CHKSELF.BackColor = System.Drawing.Color.Transparent
        Me.CHKSELF.Location = New System.Drawing.Point(692, 71)
        Me.CHKSELF.Name = "CHKSELF"
        Me.CHKSELF.Size = New System.Drawing.Size(81, 19)
        Me.CHKSELF.TabIndex = 5
        Me.CHKSELF.Text = "Self Order"
        Me.CHKSELF.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(409, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 15)
        Me.Label3.TabIndex = 748
        Me.Label3.Text = "Copy Order No"
        '
        'TXTCOPY
        '
        Me.TXTCOPY.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCOPY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOPY.Location = New System.Drawing.Point(499, 0)
        Me.TXTCOPY.Name = "TXTCOPY"
        Me.TXTCOPY.Size = New System.Drawing.Size(61, 22)
        Me.TXTCOPY.TabIndex = 747
        Me.TXTCOPY.TabStop = False
        Me.TXTCOPY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBSAMPLETYPE
        '
        Me.CMBSAMPLETYPE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSAMPLETYPE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSAMPLETYPE.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBSAMPLETYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBSAMPLETYPE.DropDownWidth = 400
        Me.CMBSAMPLETYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSAMPLETYPE.FormattingEnabled = True
        Me.CMBSAMPLETYPE.Location = New System.Drawing.Point(56, 107)
        Me.CMBSAMPLETYPE.MaxLength = 100
        Me.CMBSAMPLETYPE.Name = "CMBSAMPLETYPE"
        Me.CMBSAMPLETYPE.Size = New System.Drawing.Size(150, 23)
        Me.CMBSAMPLETYPE.TabIndex = 6
        '
        'LBLCLOSED
        '
        Me.LBLCLOSED.AutoSize = True
        Me.LBLCLOSED.BackColor = System.Drawing.Color.Transparent
        Me.LBLCLOSED.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLCLOSED.ForeColor = System.Drawing.Color.Red
        Me.LBLCLOSED.Location = New System.Drawing.Point(998, 467)
        Me.LBLCLOSED.Name = "LBLCLOSED"
        Me.LBLCLOSED.Size = New System.Drawing.Size(53, 19)
        Me.LBLCLOSED.TabIndex = 678
        Me.LBLCLOSED.Text = "Closed"
        Me.LBLCLOSED.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(491, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 15)
        Me.Label1.TabIndex = 746
        Me.Label1.Text = "Agent"
        '
        'PBlock
        '
        Me.PBlock.BackColor = System.Drawing.Color.Transparent
        Me.PBlock.Image = CType(resources.GetObject("PBlock.Image"), System.Drawing.Image)
        Me.PBlock.Location = New System.Drawing.Point(1057, 462)
        Me.PBlock.Name = "PBlock"
        Me.PBlock.Size = New System.Drawing.Size(60, 60)
        Me.PBlock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBlock.TabIndex = 677
        Me.PBlock.TabStop = False
        Me.PBlock.Visible = False
        '
        'lbllocked
        '
        Me.lbllocked.AutoSize = True
        Me.lbllocked.BackColor = System.Drawing.Color.Transparent
        Me.lbllocked.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lbllocked.ForeColor = System.Drawing.Color.Red
        Me.lbllocked.Location = New System.Drawing.Point(994, 491)
        Me.lbllocked.Name = "lbllocked"
        Me.lbllocked.Size = New System.Drawing.Size(57, 19)
        Me.lbllocked.TabIndex = 676
        Me.lbllocked.Text = "Locked"
        Me.lbllocked.Visible = False
        '
        'CMBAGENT
        '
        Me.CMBAGENT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBAGENT.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBAGENT.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CMBAGENT.FormattingEnabled = True
        Me.CMBAGENT.Location = New System.Drawing.Point(533, 40)
        Me.CMBAGENT.MaxLength = 100
        Me.CMBAGENT.Name = "CMBAGENT"
        Me.CMBAGENT.Size = New System.Drawing.Size(240, 23)
        Me.CMBAGENT.TabIndex = 3
        '
        'TOTALNOOFBOOKLET
        '
        Me.TOTALNOOFBOOKLET.BackColor = System.Drawing.Color.Transparent
        Me.TOTALNOOFBOOKLET.Location = New System.Drawing.Point(813, 443)
        Me.TOTALNOOFBOOKLET.Name = "TOTALNOOFBOOKLET"
        Me.TOTALNOOFBOOKLET.Size = New System.Drawing.Size(80, 15)
        Me.TOTALNOOFBOOKLET.TabIndex = 744
        Me.TOTALNOOFBOOKLET.Text = "0"
        Me.TOTALNOOFBOOKLET.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTNOOFBOOKLET
        '
        Me.TXTNOOFBOOKLET.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTNOOFBOOKLET.Location = New System.Drawing.Point(816, 107)
        Me.TXTNOOFBOOKLET.MaxLength = 100
        Me.TXTNOOFBOOKLET.Name = "TXTNOOFBOOKLET"
        Me.TXTNOOFBOOKLET.Size = New System.Drawing.Size(80, 23)
        Me.TXTNOOFBOOKLET.TabIndex = 11
        Me.TXTNOOFBOOKLET.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBQUALITY
        '
        Me.CMBQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBQUALITY.BackColor = System.Drawing.Color.White
        Me.CMBQUALITY.DropDownWidth = 400
        Me.CMBQUALITY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBQUALITY.FormattingEnabled = True
        Me.CMBQUALITY.Location = New System.Drawing.Point(406, 107)
        Me.CMBQUALITY.MaxLength = 100
        Me.CMBQUALITY.Name = "CMBQUALITY"
        Me.CMBQUALITY.Size = New System.Drawing.Size(160, 23)
        Me.CMBQUALITY.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(736, 443)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 15)
        Me.Label2.TabIndex = 742
        Me.Label2.Text = "Total"
        '
        'CMBCOLOR
        '
        Me.CMBCOLOR.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCOLOR.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCOLOR.BackColor = System.Drawing.Color.White
        Me.CMBCOLOR.DropDownWidth = 400
        Me.CMBCOLOR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCOLOR.FormattingEnabled = True
        Me.CMBCOLOR.Location = New System.Drawing.Point(716, 107)
        Me.CMBCOLOR.MaxLength = 100
        Me.CMBCOLOR.Name = "CMBCOLOR"
        Me.CMBCOLOR.Size = New System.Drawing.Size(100, 23)
        Me.CMBCOLOR.TabIndex = 10
        '
        'TXTBARCODE
        '
        Me.TXTBARCODE.BackColor = System.Drawing.Color.White
        Me.TXTBARCODE.Location = New System.Drawing.Point(533, 69)
        Me.TXTBARCODE.Name = "TXTBARCODE"
        Me.TXTBARCODE.Size = New System.Drawing.Size(140, 23)
        Me.TXTBARCODE.TabIndex = 4
        '
        'CMBDESIGN
        '
        Me.CMBDESIGN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBDESIGN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBDESIGN.BackColor = System.Drawing.Color.White
        Me.CMBDESIGN.DropDownWidth = 400
        Me.CMBDESIGN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBDESIGN.FormattingEnabled = True
        Me.CMBDESIGN.Location = New System.Drawing.Point(566, 107)
        Me.CMBDESIGN.MaxLength = 100
        Me.CMBDESIGN.Name = "CMBDESIGN"
        Me.CMBDESIGN.Size = New System.Drawing.Size(150, 23)
        Me.CMBDESIGN.TabIndex = 9
        '
        'lblbarcode
        '
        Me.lblbarcode.AutoSize = True
        Me.lblbarcode.BackColor = System.Drawing.Color.Transparent
        Me.lblbarcode.Location = New System.Drawing.Point(478, 73)
        Me.lblbarcode.Name = "lblbarcode"
        Me.lblbarcode.Size = New System.Drawing.Size(52, 15)
        Me.lblbarcode.TabIndex = 1
        Me.lblbarcode.Text = "Barcode"
        '
        'TXTNARRATION
        '
        Me.TXTNARRATION.Location = New System.Drawing.Point(896, 107)
        Me.TXTNARRATION.MaxLength = 100
        Me.TXTNARRATION.Name = "TXTNARRATION"
        Me.TXTNARRATION.Size = New System.Drawing.Size(200, 23)
        Me.TXTNARRATION.TabIndex = 12
        '
        'TXTADD
        '
        Me.TXTADD.Location = New System.Drawing.Point(386, 69)
        Me.TXTADD.MaxLength = 100
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(75, 23)
        Me.TXTADD.TabIndex = 739
        Me.TXTADD.Visible = False
        '
        'CMBCODE
        '
        Me.CMBCODE.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Location = New System.Drawing.Point(386, 40)
        Me.CMBCODE.MaxLength = 100
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(75, 23)
        Me.CMBCODE.TabIndex = 738
        Me.CMBCODE.Visible = False
        '
        'GRIDSO
        '
        Me.GRIDSO.AllowUserToAddRows = False
        Me.GRIDSO.AllowUserToDeleteRows = False
        Me.GRIDSO.AllowUserToResizeColumns = False
        Me.GRIDSO.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDSO.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDSO.BackgroundColor = System.Drawing.Color.White
        Me.GRIDSO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDSO.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDSO.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDSO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDSO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.gsrno, Me.GSAMPLETYPE, Me.gitemname, Me.GQUALITY, Me.GDESIGN, Me.GCOLOR, Me.GNOOFBOOKLET, Me.GNARRATION, Me.GOUTQTY, Me.GCLOSED})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSO.DefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDSO.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDSO.Location = New System.Drawing.Point(21, 130)
        Me.GRIDSO.MultiSelect = False
        Me.GRIDSO.Name = "GRIDSO"
        Me.GRIDSO.RowHeadersVisible = False
        Me.GRIDSO.RowHeadersWidth = 30
        Me.GRIDSO.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDSO.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDSO.RowTemplate.Height = 20
        Me.GRIDSO.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSO.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDSO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDSO.Size = New System.Drawing.Size(1109, 310)
        Me.GRIDSO.TabIndex = 13
        Me.GRIDSO.TabStop = False
        '
        'gsrno
        '
        Me.gsrno.HeaderText = "Sr."
        Me.gsrno.Name = "gsrno"
        Me.gsrno.ReadOnly = True
        Me.gsrno.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gsrno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gsrno.Width = 35
        '
        'GSAMPLETYPE
        '
        Me.GSAMPLETYPE.HeaderText = "Sample Type"
        Me.GSAMPLETYPE.Name = "GSAMPLETYPE"
        Me.GSAMPLETYPE.ReadOnly = True
        Me.GSAMPLETYPE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSAMPLETYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSAMPLETYPE.Width = 150
        '
        'gitemname
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.gitemname.DefaultCellStyle = DataGridViewCellStyle3
        Me.gitemname.HeaderText = "Item Name"
        Me.gitemname.Name = "gitemname"
        Me.gitemname.ReadOnly = True
        Me.gitemname.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gitemname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gitemname.Width = 200
        '
        'GQUALITY
        '
        Me.GQUALITY.HeaderText = "Quality"
        Me.GQUALITY.Name = "GQUALITY"
        Me.GQUALITY.ReadOnly = True
        Me.GQUALITY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GQUALITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GQUALITY.Width = 160
        '
        'GDESIGN
        '
        Me.GDESIGN.HeaderText = "Design"
        Me.GDESIGN.Name = "GDESIGN"
        Me.GDESIGN.ReadOnly = True
        Me.GDESIGN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDESIGN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDESIGN.Width = 150
        '
        'GCOLOR
        '
        Me.GCOLOR.HeaderText = "Shade"
        Me.GCOLOR.Name = "GCOLOR"
        Me.GCOLOR.ReadOnly = True
        Me.GCOLOR.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCOLOR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GNOOFBOOKLET
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GNOOFBOOKLET.DefaultCellStyle = DataGridViewCellStyle4
        Me.GNOOFBOOKLET.HeaderText = "No Of Booklet"
        Me.GNOOFBOOKLET.Name = "GNOOFBOOKLET"
        Me.GNOOFBOOKLET.ReadOnly = True
        Me.GNOOFBOOKLET.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNOOFBOOKLET.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GNOOFBOOKLET.Width = 80
        '
        'GNARRATION
        '
        Me.GNARRATION.HeaderText = "Narration"
        Me.GNARRATION.Name = "GNARRATION"
        Me.GNARRATION.ReadOnly = True
        Me.GNARRATION.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNARRATION.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GNARRATION.Width = 200
        '
        'GOUTQTY
        '
        Me.GOUTQTY.HeaderText = "OutQty"
        Me.GOUTQTY.Name = "GOUTQTY"
        Me.GOUTQTY.Visible = False
        '
        'GCLOSED
        '
        Me.GCLOSED.HeaderText = "Closed"
        Me.GCLOSED.Name = "GCLOSED"
        Me.GCLOSED.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.TXTREMARKS)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(25, 446)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(236, 107)
        Me.GroupBox5.TabIndex = 14
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Remarks"
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.ForeColor = System.Drawing.Color.DimGray
        Me.TXTREMARKS.Location = New System.Drawing.Point(5, 16)
        Me.TXTREMARKS.MaxLength = 200
        Me.TXTREMARKS.Multiline = True
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(225, 85)
        Me.TXTREMARKS.TabIndex = 0
        Me.TXTREMARKS.TabStop = False
        '
        'CMBITEMNAME
        '
        Me.CMBITEMNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBITEMNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBITEMNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBITEMNAME.DropDownWidth = 400
        Me.CMBITEMNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBITEMNAME.FormattingEnabled = True
        Me.CMBITEMNAME.Location = New System.Drawing.Point(206, 107)
        Me.CMBITEMNAME.MaxLength = 100
        Me.CMBITEMNAME.Name = "CMBITEMNAME"
        Me.CMBITEMNAME.Size = New System.Drawing.Size(200, 23)
        Me.CMBITEMNAME.TabIndex = 7
        '
        'txtbillno
        '
        Me.txtbillno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtbillno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbillno.Location = New System.Drawing.Point(262, 1)
        Me.txtbillno.Name = "txtbillno"
        Me.txtbillno.Size = New System.Drawing.Size(61, 22)
        Me.txtbillno.TabIndex = 19
        Me.txtbillno.TabStop = False
        Me.txtbillno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTSRNO
        '
        Me.TXTSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTSRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSRNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTSRNO.Location = New System.Drawing.Point(21, 107)
        Me.TXTSRNO.Name = "TXTSRNO"
        Me.TXTSRNO.ReadOnly = True
        Me.TXTSRNO.Size = New System.Drawing.Size(35, 23)
        Me.TXTSRNO.TabIndex = 0
        Me.TXTSRNO.TabStop = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.TOOLWHATSAPP, Me.TOOLDELETE, Me.ToolStripSeparator2, Me.ToolPREVIOUS, Me.Toolnext, Me.ToolStripSeparator3})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1155, 25)
        Me.ToolStrip1.TabIndex = 32
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
        'TOOLWHATSAPP
        '
        Me.TOOLWHATSAPP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLWHATSAPP.Image = Global.TEXTRADE.My.Resources.Resources.WHATSAPP
        Me.TOOLWHATSAPP.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLWHATSAPP.Name = "TOOLWHATSAPP"
        Me.TOOLWHATSAPP.Size = New System.Drawing.Size(23, 22)
        Me.TOOLWHATSAPP.Text = "&Whatsapp"
        '
        'TOOLDELETE
        '
        Me.TOOLDELETE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLDELETE.Image = CType(resources.GetObject("TOOLDELETE.Image"), System.Drawing.Image)
        Me.TOOLDELETE.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLDELETE.Name = "TOOLDELETE"
        Me.TOOLDELETE.Size = New System.Drawing.Size(23, 22)
        Me.TOOLDELETE.Text = "&Delete"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolPREVIOUS
        '
        Me.ToolPREVIOUS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolPREVIOUS.Image = CType(resources.GetObject("ToolPREVIOUS.Image"), System.Drawing.Image)
        Me.ToolPREVIOUS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolPREVIOUS.Name = "ToolPREVIOUS"
        Me.ToolPREVIOUS.Size = New System.Drawing.Size(73, 22)
        Me.ToolPREVIOUS.Text = "Previous"
        '
        'Toolnext
        '
        Me.Toolnext.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Toolnext.Image = CType(resources.GetObject("Toolnext.Image"), System.Drawing.Image)
        Me.Toolnext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Toolnext.Name = "Toolnext"
        Me.Toolnext.Size = New System.Drawing.Size(51, 22)
        Me.Toolnext.Text = "Next"
        Me.Toolnext.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ORDERDATE
        '
        Me.ORDERDATE.AsciiOnly = True
        Me.ORDERDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.ORDERDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.ORDERDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.ORDERDATE.Location = New System.Drawing.Point(1008, 69)
        Me.ORDERDATE.Mask = "00/00/0000"
        Me.ORDERDATE.Name = "ORDERDATE"
        Me.ORDERDATE.Size = New System.Drawing.Size(88, 23)
        Me.ORDERDATE.TabIndex = 0
        Me.ORDERDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.ORDERDATE.ValidatingType = GetType(Date)
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmddelete.FlatAppearance.BorderSize = 0
        Me.cmddelete.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddelete.ForeColor = System.Drawing.Color.Black
        Me.cmddelete.Location = New System.Drawing.Point(507, 519)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 28)
        Me.cmddelete.TabIndex = 17
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(335, 519)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 15
        Me.cmdok.Text = "&Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.Color.Transparent
        Me.cmdclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdclear.FlatAppearance.BorderSize = 0
        Me.cmdclear.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.ForeColor = System.Drawing.Color.Black
        Me.cmdclear.Location = New System.Drawing.Point(421, 519)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(80, 28)
        Me.cmdclear.TabIndex = 16
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = False
        '
        'cmdEXIT
        '
        Me.cmdEXIT.BackColor = System.Drawing.Color.Transparent
        Me.cmdEXIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdEXIT.FlatAppearance.BorderSize = 0
        Me.cmdEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEXIT.ForeColor = System.Drawing.Color.Black
        Me.cmdEXIT.Location = New System.Drawing.Point(593, 519)
        Me.cmdEXIT.Name = "cmdEXIT"
        Me.cmdEXIT.Size = New System.Drawing.Size(80, 28)
        Me.cmdEXIT.TabIndex = 18
        Me.cmdEXIT.Text = "E&xit"
        Me.cmdEXIT.UseVisualStyleBackColor = False
        '
        'CMBPARTYNAME
        '
        Me.CMBPARTYNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBPARTYNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBPARTYNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBPARTYNAME.FormattingEnabled = True
        Me.CMBPARTYNAME.Location = New System.Drawing.Point(133, 40)
        Me.CMBPARTYNAME.MaxLength = 100
        Me.CMBPARTYNAME.Name = "CMBPARTYNAME"
        Me.CMBPARTYNAME.Size = New System.Drawing.Size(240, 23)
        Me.CMBPARTYNAME.TabIndex = 1
        '
        'TXTMODEOFSHIPMENT
        '
        Me.TXTMODEOFSHIPMENT.Location = New System.Drawing.Point(133, 69)
        Me.TXTMODEOFSHIPMENT.MaxLength = 100
        Me.TXTMODEOFSHIPMENT.Name = "TXTMODEOFSHIPMENT"
        Me.TXTMODEOFSHIPMENT.Size = New System.Drawing.Size(240, 23)
        Me.TXTMODEOFSHIPMENT.TabIndex = 2
        '
        'TXTORDERNO
        '
        Me.TXTORDERNO.BackColor = System.Drawing.Color.Linen
        Me.TXTORDERNO.Location = New System.Drawing.Point(1008, 40)
        Me.TXTORDERNO.Name = "TXTORDERNO"
        Me.TXTORDERNO.ReadOnly = True
        Me.TXTORDERNO.Size = New System.Drawing.Size(88, 23)
        Me.TXTORDERNO.TabIndex = 0
        Me.TXTORDERNO.TabStop = False
        '
        'LBLMODEOFSHIPMENT
        '
        Me.LBLMODEOFSHIPMENT.AutoSize = True
        Me.LBLMODEOFSHIPMENT.BackColor = System.Drawing.Color.Transparent
        Me.LBLMODEOFSHIPMENT.Location = New System.Drawing.Point(22, 73)
        Me.LBLMODEOFSHIPMENT.Name = "LBLMODEOFSHIPMENT"
        Me.LBLMODEOFSHIPMENT.Size = New System.Drawing.Size(108, 15)
        Me.LBLMODEOFSHIPMENT.TabIndex = 6
        Me.LBLMODEOFSHIPMENT.Text = "Mode Of Shipment"
        '
        'LBLDATE
        '
        Me.LBLDATE.AutoSize = True
        Me.LBLDATE.BackColor = System.Drawing.Color.Transparent
        Me.LBLDATE.Location = New System.Drawing.Point(973, 73)
        Me.LBLDATE.Name = "LBLDATE"
        Me.LBLDATE.Size = New System.Drawing.Size(32, 15)
        Me.LBLDATE.TabIndex = 5
        Me.LBLDATE.Text = "Date"
        '
        'LBLSRNO
        '
        Me.LBLSRNO.AutoSize = True
        Me.LBLSRNO.BackColor = System.Drawing.Color.Transparent
        Me.LBLSRNO.Location = New System.Drawing.Point(969, 44)
        Me.LBLSRNO.Name = "LBLSRNO"
        Me.LBLSRNO.Size = New System.Drawing.Size(36, 15)
        Me.LBLSRNO.TabIndex = 4
        Me.LBLSRNO.Text = "Sr No"
        '
        'lblpartyname
        '
        Me.lblpartyname.AutoSize = True
        Me.lblpartyname.BackColor = System.Drawing.Color.Transparent
        Me.lblpartyname.Location = New System.Drawing.Point(19, 44)
        Me.lblpartyname.Name = "lblpartyname"
        Me.lblpartyname.Size = New System.Drawing.Size(111, 15)
        Me.lblpartyname.TabIndex = 0
        Me.lblpartyname.Text = "Party / Agent Name"
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(572, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(114, 15)
        Me.Label4.TabIndex = 964
        Me.Label4.Text = "Fetch Data From SO"
        '
        'TXTSONO
        '
        Me.TXTSONO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTSONO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSONO.Location = New System.Drawing.Point(691, 0)
        Me.TXTSONO.Name = "TXTSONO"
        Me.TXTSONO.Size = New System.Drawing.Size(63, 22)
        Me.TXTSONO.TabIndex = 963
        Me.TXTSONO.TabStop = False
        Me.TXTSONO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SampleOrder
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1155, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "SampleOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Sample Order"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.PBlock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDSO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents CMBAGENT As ComboBox
    Friend WithEvents TOTALNOOFBOOKLET As Label
    Friend WithEvents TXTNOOFBOOKLET As TextBox
    Friend WithEvents CMBQUALITY As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CMBCOLOR As ComboBox
    Friend WithEvents TXTBARCODE As TextBox
    Friend WithEvents CMBDESIGN As ComboBox
    Friend WithEvents lblbarcode As Label
    Friend WithEvents TXTNARRATION As TextBox
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents GRIDSO As DataGridView
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents TXTREMARKS As TextBox
    Friend WithEvents CMBITEMNAME As ComboBox
    Friend WithEvents txtbillno As TextBox
    Friend WithEvents TXTSRNO As TextBox
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents SaveToolStripButton As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents TOOLDELETE As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolPREVIOUS As ToolStripButton
    Friend WithEvents Toolnext As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ORDERDATE As MaskedTextBox
    Friend WithEvents cmddelete As Button
    Friend WithEvents cmdok As Button
    Friend WithEvents cmdclear As Button
    Friend WithEvents cmdEXIT As Button
    Friend WithEvents CMBPARTYNAME As ComboBox
    Friend WithEvents TXTMODEOFSHIPMENT As TextBox
    Friend WithEvents TXTORDERNO As TextBox
    Friend WithEvents LBLMODEOFSHIPMENT As Label
    Friend WithEvents LBLDATE As Label
    Friend WithEvents LBLSRNO As Label
    Friend WithEvents lblpartyname As Label
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents LBLCLOSED As Label
    Friend WithEvents PBlock As PictureBox
    Friend WithEvents lbllocked As Label
    Friend WithEvents CMBSAMPLETYPE As ComboBox
    Friend WithEvents gsrno As DataGridViewTextBoxColumn
    Friend WithEvents GSAMPLETYPE As DataGridViewTextBoxColumn
    Friend WithEvents gitemname As DataGridViewTextBoxColumn
    Friend WithEvents GQUALITY As DataGridViewTextBoxColumn
    Friend WithEvents GDESIGN As DataGridViewTextBoxColumn
    Friend WithEvents GCOLOR As DataGridViewTextBoxColumn
    Friend WithEvents GNOOFBOOKLET As DataGridViewTextBoxColumn
    Friend WithEvents GNARRATION As DataGridViewTextBoxColumn
    Friend WithEvents GOUTQTY As DataGridViewTextBoxColumn
    Friend WithEvents GCLOSED As DataGridViewTextBoxColumn
    Friend WithEvents Label3 As Label
    Friend WithEvents TXTCOPY As TextBox
    Friend WithEvents CHKSELF As CheckBox
    Friend WithEvents CMDSELECTSTOCK As Button
    Friend WithEvents TOOLWHATSAPP As ToolStripButton
    Friend WithEvents LBLWHATSAPP As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TXTSONO As TextBox
End Class
