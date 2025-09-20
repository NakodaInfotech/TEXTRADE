<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MagicBoxForRecPay
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MagicBoxForRecPay))
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.miniToolStrip = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.tooldelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.toolprevious = New System.Windows.Forms.ToolStripButton()
        Me.toolnext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CMDDELETE = New System.Windows.Forms.Button()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TXTENTERYNO = New System.Windows.Forms.TextBox()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.LBLTOTALAMT = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DTENTERYDATE = New System.Windows.Forms.MaskedTextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.txtamt = New System.Windows.Forms.TextBox()
        Me.cmbaccname = New System.Windows.Forms.ComboBox()
        Me.cmbname = New System.Windows.Forms.ComboBox()
        Me.TXTCHQNO = New System.Windows.Forms.TextBox()
        Me.DTCHQDATE = New System.Windows.Forms.DateTimePicker()
        Me.TXTBANKNAME = New System.Windows.Forms.TextBox()
        Me.txtsrno = New System.Windows.Forms.TextBox()
        Me.tstxtbillno = New System.Windows.Forms.TextBox()
        Me.txtadd = New System.Windows.Forms.TextBox()
        Me.CMBACCCODE = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtinwords = New System.Windows.Forms.TextBox()
        Me.cmbpaytype = New System.Windows.Forms.ComboBox()
        Me.CMBBUYER = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LBLPACKING = New System.Windows.Forms.Label()
        Me.TXTBILLNO = New System.Windows.Forms.TextBox()
        Me.TXTADJAMOUNT = New System.Windows.Forms.TextBox()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.GRIDISSUE = New System.Windows.Forms.DataGridView()
        Me.txtremamount = New System.Windows.Forms.TextBox()
        Me.GSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GACCNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPARTYNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSELLERNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCHQNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCHQDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCHQAMT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPAYTYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBANKNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBILLNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAMOUNT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gremamt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.BlendPanel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.GRIDISSUE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'miniToolStrip
        '
        Me.miniToolStrip.AccessibleName = "New item selection"
        Me.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(1280, 25)
        Me.miniToolStrip.TabIndex = 0
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
        Me.toolprevious.ForeColor = System.Drawing.SystemColors.WindowText
        Me.toolprevious.Image = Global.TEXTRADE.My.Resources.Resources.POINT021
        Me.toolprevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolprevious.Name = "toolprevious"
        Me.toolprevious.Size = New System.Drawing.Size(73, 22)
        Me.toolprevious.Text = "Previous"
        '
        'toolnext
        '
        Me.toolnext.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toolnext.ForeColor = System.Drawing.SystemColors.WindowText
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
        'CMDDELETE
        '
        Me.CMDDELETE.BackColor = System.Drawing.Color.Transparent
        Me.CMDDELETE.FlatAppearance.BorderSize = 0
        Me.CMDDELETE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDELETE.ForeColor = System.Drawing.Color.Black
        Me.CMDDELETE.Location = New System.Drawing.Point(751, 509)
        Me.CMDDELETE.Name = "CMDDELETE"
        Me.CMDDELETE.Size = New System.Drawing.Size(80, 28)
        Me.CMDDELETE.TabIndex = 15
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
        Me.CMDOK.Location = New System.Drawing.Point(579, 509)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 13
        Me.CMDOK.Text = "&Save"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(837, 509)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 16
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(784, 36)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 15)
        Me.Label9.TabIndex = 632
        Me.Label9.Text = "Date"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(626, 37)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(38, 15)
        Me.Label12.TabIndex = 634
        Me.Label12.Text = "Sr. No"
        '
        'TXTENTERYNO
        '
        Me.TXTENTERYNO.BackColor = System.Drawing.Color.Linen
        Me.TXTENTERYNO.Enabled = False
        Me.TXTENTERYNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTENTERYNO.Location = New System.Drawing.Point(665, 33)
        Me.TXTENTERYNO.Name = "TXTENTERYNO"
        Me.TXTENTERYNO.ReadOnly = True
        Me.TXTENTERYNO.Size = New System.Drawing.Size(87, 23)
        Me.TXTENTERYNO.TabIndex = 633
        Me.TXTENTERYNO.TabStop = False
        Me.TXTENTERYNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.FlatAppearance.BorderSize = 0
        Me.CMDCLEAR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.Black
        Me.CMDCLEAR.Location = New System.Drawing.Point(665, 509)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 14
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
        '
        'LBLTOTALAMT
        '
        Me.LBLTOTALAMT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALAMT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALAMT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALAMT.Location = New System.Drawing.Point(611, 485)
        Me.LBLTOTALAMT.Name = "LBLTOTALAMT"
        Me.LBLTOTALAMT.Size = New System.Drawing.Size(96, 15)
        Me.LBLTOTALAMT.TabIndex = 681
        Me.LBLTOTALAMT.Text = "0"
        Me.LBLTOTALAMT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(542, 486)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 14)
        Me.Label10.TabIndex = 682
        Me.Label10.Text = "Total"
        '
        'DTENTERYDATE
        '
        Me.DTENTERYDATE.AsciiOnly = True
        Me.DTENTERYDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.DTENTERYDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTENTERYDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTENTERYDATE.Location = New System.Drawing.Point(817, 32)
        Me.DTENTERYDATE.Mask = "00/00/0000"
        Me.DTENTERYDATE.Name = "DTENTERYDATE"
        Me.DTENTERYDATE.Size = New System.Drawing.Size(88, 23)
        Me.DTENTERYDATE.TabIndex = 1
        Me.DTENTERYDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTENTERYDATE.ValidatingType = GetType(Date)
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.txtremarks)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(30, 486)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(382, 54)
        Me.GroupBox5.TabIndex = 9
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Remarks"
        '
        'txtremarks
        '
        Me.txtremarks.ForeColor = System.Drawing.Color.DimGray
        Me.txtremarks.Location = New System.Drawing.Point(8, 17)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(368, 31)
        Me.txtremarks.TabIndex = 0
        '
        'txtamt
        '
        Me.txtamt.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtamt.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtamt.ForeColor = System.Drawing.Color.Black
        Me.txtamt.Location = New System.Drawing.Point(808, 90)
        Me.txtamt.Name = "txtamt"
        Me.txtamt.Size = New System.Drawing.Size(102, 22)
        Me.txtamt.TabIndex = 9
        Me.txtamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbaccname
        '
        Me.cmbaccname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbaccname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbaccname.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbaccname.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbaccname.FormattingEnabled = True
        Me.cmbaccname.Location = New System.Drawing.Point(47, 90)
        Me.cmbaccname.MaxDropDownItems = 14
        Me.cmbaccname.Name = "cmbaccname"
        Me.cmbaccname.Size = New System.Drawing.Size(201, 22)
        Me.cmbaccname.TabIndex = 5
        '
        'cmbname
        '
        Me.cmbname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbname.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbname.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbname.FormattingEnabled = True
        Me.cmbname.Location = New System.Drawing.Point(448, 90)
        Me.cmbname.MaxDropDownItems = 14
        Me.cmbname.Name = "cmbname"
        Me.cmbname.Size = New System.Drawing.Size(200, 22)
        Me.cmbname.TabIndex = 6
        '
        'TXTCHQNO
        '
        Me.TXTCHQNO.BackColor = System.Drawing.Color.White
        Me.TXTCHQNO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCHQNO.Location = New System.Drawing.Point(648, 90)
        Me.TXTCHQNO.MaxLength = 6
        Me.TXTCHQNO.Name = "TXTCHQNO"
        Me.TXTCHQNO.Size = New System.Drawing.Size(82, 22)
        Me.TXTCHQNO.TabIndex = 7
        Me.TXTCHQNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DTCHQDATE
        '
        Me.DTCHQDATE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTCHQDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTCHQDATE.Location = New System.Drawing.Point(729, 90)
        Me.DTCHQDATE.Name = "DTCHQDATE"
        Me.DTCHQDATE.Size = New System.Drawing.Size(80, 22)
        Me.DTCHQDATE.TabIndex = 8
        '
        'TXTBANKNAME
        '
        Me.TXTBANKNAME.BackColor = System.Drawing.Color.White
        Me.TXTBANKNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBANKNAME.ForeColor = System.Drawing.Color.Black
        Me.TXTBANKNAME.Location = New System.Drawing.Point(1000, 90)
        Me.TXTBANKNAME.Name = "TXTBANKNAME"
        Me.TXTBANKNAME.Size = New System.Drawing.Size(199, 22)
        Me.TXTBANKNAME.TabIndex = 11
        '
        'txtsrno
        '
        Me.txtsrno.BackColor = System.Drawing.Color.Linen
        Me.txtsrno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrno.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtsrno.Location = New System.Drawing.Point(10, 90)
        Me.txtsrno.Name = "txtsrno"
        Me.txtsrno.ReadOnly = True
        Me.txtsrno.Size = New System.Drawing.Size(39, 22)
        Me.txtsrno.TabIndex = 4
        Me.txtsrno.TabStop = False
        '
        'tstxtbillno
        '
        Me.tstxtbillno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstxtbillno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstxtbillno.Location = New System.Drawing.Point(241, 1)
        Me.tstxtbillno.Name = "tstxtbillno"
        Me.tstxtbillno.Size = New System.Drawing.Size(74, 22)
        Me.tstxtbillno.TabIndex = 14
        Me.tstxtbillno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtadd
        '
        Me.txtadd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd.ForeColor = System.Drawing.Color.DimGray
        Me.txtadd.Location = New System.Drawing.Point(1059, 39)
        Me.txtadd.Multiline = True
        Me.txtadd.Name = "txtadd"
        Me.txtadd.Size = New System.Drawing.Size(10, 31)
        Me.txtadd.TabIndex = 704
        Me.txtadd.TabStop = False
        Me.txtadd.Visible = False
        '
        'CMBACCCODE
        '
        Me.CMBACCCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBACCCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBACCCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBACCCODE.FormattingEnabled = True
        Me.CMBACCCODE.Items.AddRange(New Object() {""})
        Me.CMBACCCODE.Location = New System.Drawing.Point(1041, 44)
        Me.CMBACCCODE.Name = "CMBACCCODE"
        Me.CMBACCCODE.Size = New System.Drawing.Size(12, 22)
        Me.CMBACCCODE.TabIndex = 705
        Me.CMBACCCODE.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.txtinwords)
        Me.GroupBox3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox3.Location = New System.Drawing.Point(30, 548)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(563, 45)
        Me.GroupBox3.TabIndex = 706
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "In Words"
        '
        'txtinwords
        '
        Me.txtinwords.BackColor = System.Drawing.Color.Linen
        Me.txtinwords.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtinwords.ForeColor = System.Drawing.Color.Black
        Me.txtinwords.Location = New System.Drawing.Point(6, 17)
        Me.txtinwords.Multiline = True
        Me.txtinwords.Name = "txtinwords"
        Me.txtinwords.ReadOnly = True
        Me.txtinwords.Size = New System.Drawing.Size(553, 22)
        Me.txtinwords.TabIndex = 0
        Me.txtinwords.TabStop = False
        '
        'cmbpaytype
        '
        Me.cmbpaytype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbpaytype.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbpaytype.FormattingEnabled = True
        Me.cmbpaytype.Items.AddRange(New Object() {"On Account", "New Ref.", "Advance", "Against Bill"})
        Me.cmbpaytype.Location = New System.Drawing.Point(910, 90)
        Me.cmbpaytype.MaxDropDownItems = 14
        Me.cmbpaytype.Name = "cmbpaytype"
        Me.cmbpaytype.Size = New System.Drawing.Size(90, 22)
        Me.cmbpaytype.TabIndex = 10
        '
        'CMBBUYER
        '
        Me.CMBBUYER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBBUYER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBBUYER.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBBUYER.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBBUYER.FormattingEnabled = True
        Me.CMBBUYER.Items.AddRange(New Object() {""})
        Me.CMBBUYER.Location = New System.Drawing.Point(248, 90)
        Me.CMBBUYER.Name = "CMBBUYER"
        Me.CMBBUYER.Size = New System.Drawing.Size(200, 22)
        Me.CMBBUYER.TabIndex = 3
        Me.CMBBUYER.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(9, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 14)
        Me.Label2.TabIndex = 743
        Me.Label2.Text = "Buyer's Name"
        Me.Label2.Visible = False
        '
        'LBLPACKING
        '
        Me.LBLPACKING.BackColor = System.Drawing.Color.Transparent
        Me.LBLPACKING.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLPACKING.ForeColor = System.Drawing.Color.Black
        Me.LBLPACKING.Location = New System.Drawing.Point(9, 36)
        Me.LBLPACKING.Name = "LBLPACKING"
        Me.LBLPACKING.Size = New System.Drawing.Size(81, 14)
        Me.LBLPACKING.TabIndex = 744
        Me.LBLPACKING.Text = "Seller Name"
        Me.LBLPACKING.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTBILLNO
        '
        Me.TXTBILLNO.BackColor = System.Drawing.Color.Linen
        Me.TXTBILLNO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBILLNO.ForeColor = System.Drawing.Color.Black
        Me.TXTBILLNO.Location = New System.Drawing.Point(998, 90)
        Me.TXTBILLNO.Name = "TXTBILLNO"
        Me.TXTBILLNO.ReadOnly = True
        Me.TXTBILLNO.Size = New System.Drawing.Size(101, 22)
        Me.TXTBILLNO.TabIndex = 745
        Me.TXTBILLNO.Visible = False
        '
        'TXTADJAMOUNT
        '
        Me.TXTADJAMOUNT.BackColor = System.Drawing.Color.Linen
        Me.TXTADJAMOUNT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADJAMOUNT.ForeColor = System.Drawing.Color.Black
        Me.TXTADJAMOUNT.Location = New System.Drawing.Point(1098, 90)
        Me.TXTADJAMOUNT.Name = "TXTADJAMOUNT"
        Me.TXTADJAMOUNT.ReadOnly = True
        Me.TXTADJAMOUNT.Size = New System.Drawing.Size(151, 22)
        Me.TXTADJAMOUNT.TabIndex = 746
        Me.TXTADJAMOUNT.Visible = False
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.txtremamount)
        Me.BlendPanel1.Controls.Add(Me.TXTADJAMOUNT)
        Me.BlendPanel1.Controls.Add(Me.TXTBILLNO)
        Me.BlendPanel1.Controls.Add(Me.LBLPACKING)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.CMBBUYER)
        Me.BlendPanel1.Controls.Add(Me.cmbpaytype)
        Me.BlendPanel1.Controls.Add(Me.GroupBox3)
        Me.BlendPanel1.Controls.Add(Me.CMBACCCODE)
        Me.BlendPanel1.Controls.Add(Me.txtadd)
        Me.BlendPanel1.Controls.Add(Me.GRIDISSUE)
        Me.BlendPanel1.Controls.Add(Me.tstxtbillno)
        Me.BlendPanel1.Controls.Add(Me.txtsrno)
        Me.BlendPanel1.Controls.Add(Me.TXTBANKNAME)
        Me.BlendPanel1.Controls.Add(Me.DTCHQDATE)
        Me.BlendPanel1.Controls.Add(Me.TXTCHQNO)
        Me.BlendPanel1.Controls.Add(Me.cmbname)
        Me.BlendPanel1.Controls.Add(Me.cmbaccname)
        Me.BlendPanel1.Controls.Add(Me.txtamt)
        Me.BlendPanel1.Controls.Add(Me.GroupBox5)
        Me.BlendPanel1.Controls.Add(Me.DTENTERYDATE)
        Me.BlendPanel1.Controls.Add(Me.Label10)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALAMT)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.TXTENTERYNO)
        Me.BlendPanel1.Controls.Add(Me.Label12)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.CMDDELETE)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.ForeColor = System.Drawing.Color.Transparent
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1398, 606)
        Me.BlendPanel1.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.tooldelete, Me.toolStripSeparator, Me.toolprevious, Me.toolnext, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1398, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'GRIDISSUE
        '
        Me.GRIDISSUE.AllowUserToAddRows = False
        Me.GRIDISSUE.AllowUserToDeleteRows = False
        Me.GRIDISSUE.AllowUserToResizeColumns = False
        Me.GRIDISSUE.AllowUserToResizeRows = False
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black
        Me.GRIDISSUE.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle9
        Me.GRIDISSUE.BackgroundColor = System.Drawing.Color.White
        Me.GRIDISSUE.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDISSUE.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDISSUE.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.GRIDISSUE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDISSUE.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GACCNAME, Me.GPARTYNAME, Me.GSELLERNAME, Me.GCHQNO, Me.GCHQDATE, Me.GCHQAMT, Me.GPAYTYPE, Me.GBANKNAME, Me.GBILLNO, Me.GAMOUNT, Me.gremamt})
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.Color.Transparent
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDISSUE.DefaultCellStyle = DataGridViewCellStyle15
        Me.GRIDISSUE.GridColor = System.Drawing.SystemColors.ControlText
        Me.GRIDISSUE.Location = New System.Drawing.Point(8, 111)
        Me.GRIDISSUE.Margin = New System.Windows.Forms.Padding(2)
        Me.GRIDISSUE.Name = "GRIDISSUE"
        Me.GRIDISSUE.ReadOnly = True
        Me.GRIDISSUE.RowHeadersVisible = False
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDISSUE.RowsDefaultCellStyle = DataGridViewCellStyle16
        Me.GRIDISSUE.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDISSUE.Size = New System.Drawing.Size(1369, 367)
        Me.GRIDISSUE.TabIndex = 12
        Me.GRIDISSUE.TabStop = False
        '
        'txtremamount
        '
        Me.txtremamount.BackColor = System.Drawing.Color.Linen
        Me.txtremamount.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremamount.ForeColor = System.Drawing.Color.Black
        Me.txtremamount.Location = New System.Drawing.Point(1249, 90)
        Me.txtremamount.Name = "txtremamount"
        Me.txtremamount.ReadOnly = True
        Me.txtremamount.Size = New System.Drawing.Size(102, 22)
        Me.txtremamount.TabIndex = 747
        Me.txtremamount.Visible = False
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
        'GACCNAME
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GACCNAME.DefaultCellStyle = DataGridViewCellStyle11
        Me.GACCNAME.HeaderText = "Bank Name"
        Me.GACCNAME.Name = "GACCNAME"
        Me.GACCNAME.ReadOnly = True
        Me.GACCNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GACCNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GACCNAME.Width = 200
        '
        'GPARTYNAME
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GPARTYNAME.DefaultCellStyle = DataGridViewCellStyle12
        Me.GPARTYNAME.HeaderText = "Buyer Name"
        Me.GPARTYNAME.Name = "GPARTYNAME"
        Me.GPARTYNAME.ReadOnly = True
        Me.GPARTYNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPARTYNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPARTYNAME.Width = 200
        '
        'GSELLERNAME
        '
        Me.GSELLERNAME.HeaderText = "Seller Name"
        Me.GSELLERNAME.Name = "GSELLERNAME"
        Me.GSELLERNAME.ReadOnly = True
        Me.GSELLERNAME.Width = 200
        '
        'GCHQNO
        '
        Me.GCHQNO.HeaderText = "Chq No"
        Me.GCHQNO.Name = "GCHQNO"
        Me.GCHQNO.ReadOnly = True
        Me.GCHQNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCHQNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCHQNO.Width = 80
        '
        'GCHQDATE
        '
        Me.GCHQDATE.HeaderText = "Chq Date"
        Me.GCHQDATE.Name = "GCHQDATE"
        Me.GCHQDATE.ReadOnly = True
        Me.GCHQDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCHQDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCHQDATE.Width = 80
        '
        'GCHQAMT
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GCHQAMT.DefaultCellStyle = DataGridViewCellStyle13
        Me.GCHQAMT.HeaderText = "Amt."
        Me.GCHQAMT.Name = "GCHQAMT"
        Me.GCHQAMT.ReadOnly = True
        Me.GCHQAMT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCHQAMT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GPAYTYPE
        '
        Me.GPAYTYPE.HeaderText = "Pay Type"
        Me.GPAYTYPE.Name = "GPAYTYPE"
        Me.GPAYTYPE.ReadOnly = True
        Me.GPAYTYPE.Width = 90
        '
        'GBANKNAME
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GBANKNAME.DefaultCellStyle = DataGridViewCellStyle14
        Me.GBANKNAME.HeaderText = "Bank Name"
        Me.GBANKNAME.Name = "GBANKNAME"
        Me.GBANKNAME.ReadOnly = True
        Me.GBANKNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBANKNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBANKNAME.Width = 200
        '
        'GBILLNO
        '
        Me.GBILLNO.HeaderText = "Bill No"
        Me.GBILLNO.Name = "GBILLNO"
        Me.GBILLNO.ReadOnly = True
        Me.GBILLNO.Visible = False
        '
        'GAMOUNT
        '
        Me.GAMOUNT.HeaderText = "Total Amount"
        Me.GAMOUNT.Name = "GAMOUNT"
        Me.GAMOUNT.ReadOnly = True
        Me.GAMOUNT.Visible = False
        Me.GAMOUNT.Width = 150
        '
        'gremamt
        '
        Me.gremamt.HeaderText = "Rem Amt"
        Me.gremamt.Name = "gremamt"
        Me.gremamt.ReadOnly = True
        Me.gremamt.Visible = False
        '
        'MagicBoxForRecPay
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1398, 606)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "MagicBoxForRecPay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Magic Box For Rec Pay"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.GRIDISSUE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents txtremamount As TextBox
    Friend WithEvents TXTADJAMOUNT As TextBox
    Friend WithEvents TXTBILLNO As TextBox
    Friend WithEvents LBLPACKING As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents CMBBUYER As ComboBox
    Friend WithEvents cmbpaytype As ComboBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents txtinwords As TextBox
    Friend WithEvents CMBACCCODE As ComboBox
    Friend WithEvents txtadd As TextBox
    Friend WithEvents GRIDISSUE As DataGridView
    Friend WithEvents tstxtbillno As TextBox
    Friend WithEvents txtsrno As TextBox
    Friend WithEvents TXTBANKNAME As TextBox
    Friend WithEvents DTCHQDATE As DateTimePicker
    Friend WithEvents TXTCHQNO As TextBox
    Friend WithEvents cmbname As ComboBox
    Friend WithEvents cmbaccname As ComboBox
    Friend WithEvents txtamt As TextBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents txtremarks As TextBox
    Friend WithEvents DTENTERYDATE As MaskedTextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents LBLTOTALAMT As Label
    Friend WithEvents CMDCLEAR As Button
    Friend WithEvents TXTENTERYNO As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDOK As Button
    Friend WithEvents CMDDELETE As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents SaveToolStripButton As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents tooldelete As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents toolprevious As ToolStripButton
    Friend WithEvents toolnext As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents miniToolStrip As ToolStrip
    Friend WithEvents GSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GACCNAME As DataGridViewTextBoxColumn
    Friend WithEvents GPARTYNAME As DataGridViewTextBoxColumn
    Friend WithEvents GSELLERNAME As DataGridViewTextBoxColumn
    Friend WithEvents GCHQNO As DataGridViewTextBoxColumn
    Friend WithEvents GCHQDATE As DataGridViewTextBoxColumn
    Friend WithEvents GCHQAMT As DataGridViewTextBoxColumn
    Friend WithEvents GPAYTYPE As DataGridViewTextBoxColumn
    Friend WithEvents GBANKNAME As DataGridViewTextBoxColumn
    Friend WithEvents GBILLNO As DataGridViewTextBoxColumn
    Friend WithEvents GAMOUNT As DataGridViewTextBoxColumn
    Friend WithEvents gremamt As DataGridViewTextBoxColumn
End Class
