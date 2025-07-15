<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MagicBox
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.tbitem = New System.Windows.Forms.TabPage()
        Me.ORDERDATE = New System.Windows.Forms.DateTimePicker()
        Me.duedate = New System.Windows.Forms.DateTimePicker()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.CMBSELLERS = New System.Windows.Forms.ComboBox()
        Me.TXTORDERNO = New System.Windows.Forms.TextBox()
        Me.TXTNO = New System.Windows.Forms.TextBox()
        Me.TXTDISCOUNT = New System.Windows.Forms.TextBox()
        Me.CMBDESIGN = New System.Windows.Forms.ComboBox()
        Me.TXTCUT = New System.Windows.Forms.TextBox()
        Me.TXTRATE = New System.Windows.Forms.TextBox()
        Me.GRIDSO = New System.Windows.Forms.DataGridView()
        Me.cmbitemname = New System.Windows.Forms.ComboBox()
        Me.cmbqtyunit = New System.Windows.Forms.ComboBox()
        Me.txtsrno = New System.Windows.Forms.TextBox()
        Me.TXTDELPERIOD = New System.Windows.Forms.TextBox()
        Me.TXTMTRS = New System.Windows.Forms.TextBox()
        Me.txtQTY = New System.Windows.Forms.TextBox()
        Me.TXTCRDAYS = New System.Windows.Forms.TextBox()
        Me.CMBBUYERS = New System.Windows.Forms.ComboBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.cmdEXIT = New System.Windows.Forms.Button()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.gsrno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBUYERS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSELLERS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCRDAYS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDISCOUNT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDELPERIOD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDUEDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GORDERNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gitemname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDESIGN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gqtyunit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gcut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMTRS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BlendPanel1.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.tbitem.SuspendLayout()
        CType(Me.GRIDSO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.CMBCODE)
        Me.BlendPanel1.Controls.Add(Me.TabControl2)
        Me.BlendPanel1.Controls.Add(Me.cmdOK)
        Me.BlendPanel1.Controls.Add(Me.cmdclear)
        Me.BlendPanel1.Controls.Add(Me.cmdEXIT)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1484, 661)
        Me.BlendPanel1.TabIndex = 1
        '
        'TXTADD
        '
        Me.TXTADD.BackColor = System.Drawing.Color.White
        Me.TXTADD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.Location = New System.Drawing.Point(924, 639)
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(12, 22)
        Me.TXTADD.TabIndex = 22
        Me.TXTADD.Visible = False
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Items.AddRange(New Object() {""})
        Me.CMBCODE.Location = New System.Drawing.Point(908, 639)
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(10, 22)
        Me.CMBCODE.TabIndex = 21
        Me.CMBCODE.Visible = False
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.tbitem)
        Me.TabControl2.Location = New System.Drawing.Point(16, 12)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(1453, 603)
        Me.TabControl2.TabIndex = 0
        '
        'tbitem
        '
        Me.tbitem.AutoScroll = True
        Me.tbitem.BackColor = System.Drawing.Color.Linen
        Me.tbitem.Controls.Add(Me.ORDERDATE)
        Me.tbitem.Controls.Add(Me.duedate)
        Me.tbitem.Controls.Add(Me.TXTREMARKS)
        Me.tbitem.Controls.Add(Me.CMBSELLERS)
        Me.tbitem.Controls.Add(Me.TXTORDERNO)
        Me.tbitem.Controls.Add(Me.TXTNO)
        Me.tbitem.Controls.Add(Me.TXTDISCOUNT)
        Me.tbitem.Controls.Add(Me.CMBDESIGN)
        Me.tbitem.Controls.Add(Me.TXTCUT)
        Me.tbitem.Controls.Add(Me.TXTRATE)
        Me.tbitem.Controls.Add(Me.GRIDSO)
        Me.tbitem.Controls.Add(Me.cmbitemname)
        Me.tbitem.Controls.Add(Me.cmbqtyunit)
        Me.tbitem.Controls.Add(Me.txtsrno)
        Me.tbitem.Controls.Add(Me.TXTDELPERIOD)
        Me.tbitem.Controls.Add(Me.TXTMTRS)
        Me.tbitem.Controls.Add(Me.txtQTY)
        Me.tbitem.Controls.Add(Me.TXTCRDAYS)
        Me.tbitem.Controls.Add(Me.CMBBUYERS)
        Me.tbitem.Location = New System.Drawing.Point(4, 23)
        Me.tbitem.Name = "tbitem"
        Me.tbitem.Padding = New System.Windows.Forms.Padding(3)
        Me.tbitem.Size = New System.Drawing.Size(1445, 576)
        Me.tbitem.TabIndex = 0
        Me.tbitem.Text = "Magic Details"
        '
        'ORDERDATE
        '
        Me.ORDERDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ORDERDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ORDERDATE.Location = New System.Drawing.Point(76, 4)
        Me.ORDERDATE.Name = "ORDERDATE"
        Me.ORDERDATE.Size = New System.Drawing.Size(87, 23)
        Me.ORDERDATE.TabIndex = 2
        '
        'duedate
        '
        Me.duedate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.duedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.duedate.Location = New System.Drawing.Point(713, 4)
        Me.duedate.Name = "duedate"
        Me.duedate.Size = New System.Drawing.Size(89, 23)
        Me.duedate.TabIndex = 8
        Me.duedate.TabStop = False
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.BackColor = System.Drawing.Color.White
        Me.TXTREMARKS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTREMARKS.Location = New System.Drawing.Point(1477, 4)
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(201, 23)
        Me.TXTREMARKS.TabIndex = 17
        '
        'CMBSELLERS
        '
        Me.CMBSELLERS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSELLERS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSELLERS.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBSELLERS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSELLERS.FormattingEnabled = True
        Me.CMBSELLERS.Items.AddRange(New Object() {""})
        Me.CMBSELLERS.Location = New System.Drawing.Point(365, 4)
        Me.CMBSELLERS.Name = "CMBSELLERS"
        Me.CMBSELLERS.Size = New System.Drawing.Size(198, 23)
        Me.CMBSELLERS.TabIndex = 4
        '
        'TXTORDERNO
        '
        Me.TXTORDERNO.BackColor = System.Drawing.Color.White
        Me.TXTORDERNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTORDERNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTORDERNO.Location = New System.Drawing.Point(802, 4)
        Me.TXTORDERNO.Name = "TXTORDERNO"
        Me.TXTORDERNO.Size = New System.Drawing.Size(70, 23)
        Me.TXTORDERNO.TabIndex = 9
        Me.TXTORDERNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTNO
        '
        Me.TXTNO.BackColor = System.Drawing.Color.Linen
        Me.TXTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTNO.Location = New System.Drawing.Point(33, 4)
        Me.TXTNO.Name = "TXTNO"
        Me.TXTNO.ReadOnly = True
        Me.TXTNO.Size = New System.Drawing.Size(43, 23)
        Me.TXTNO.TabIndex = 1
        Me.TXTNO.TabStop = False
        '
        'TXTDISCOUNT
        '
        Me.TXTDISCOUNT.BackColor = System.Drawing.Color.White
        Me.TXTDISCOUNT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTDISCOUNT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTDISCOUNT.Location = New System.Drawing.Point(613, 4)
        Me.TXTDISCOUNT.Name = "TXTDISCOUNT"
        Me.TXTDISCOUNT.Size = New System.Drawing.Size(51, 23)
        Me.TXTDISCOUNT.TabIndex = 6
        Me.TXTDISCOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBDESIGN
        '
        Me.CMBDESIGN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBDESIGN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBDESIGN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBDESIGN.FormattingEnabled = True
        Me.CMBDESIGN.Location = New System.Drawing.Point(1072, 4)
        Me.CMBDESIGN.Name = "CMBDESIGN"
        Me.CMBDESIGN.Size = New System.Drawing.Size(120, 23)
        Me.CMBDESIGN.TabIndex = 11
        '
        'TXTCUT
        '
        Me.TXTCUT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCUT.Location = New System.Drawing.Point(1307, 4)
        Me.TXTCUT.Name = "TXTCUT"
        Me.TXTCUT.Size = New System.Drawing.Size(45, 23)
        Me.TXTCUT.TabIndex = 14
        Me.TXTCUT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTRATE
        '
        Me.TXTRATE.BackColor = System.Drawing.Color.White
        Me.TXTRATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTRATE.Location = New System.Drawing.Point(1412, 4)
        Me.TXTRATE.Name = "TXTRATE"
        Me.TXTRATE.Size = New System.Drawing.Size(65, 23)
        Me.TXTRATE.TabIndex = 16
        Me.TXTRATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDSO.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDSO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDSO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.gsrno, Me.GNO, Me.GDATE, Me.GBUYERS, Me.GSELLERS, Me.GCRDAYS, Me.GDISCOUNT, Me.GDELPERIOD, Me.GDUEDATE, Me.GORDERNO, Me.gitemname, Me.GDESIGN, Me.gQty, Me.gqtyunit, Me.gcut, Me.GMTRS, Me.GRATE, Me.GREMARKS})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSO.DefaultCellStyle = DataGridViewCellStyle8
        Me.GRIDSO.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDSO.Location = New System.Drawing.Point(3, 27)
        Me.GRIDSO.MultiSelect = False
        Me.GRIDSO.Name = "GRIDSO"
        Me.GRIDSO.RowHeadersVisible = False
        Me.GRIDSO.RowHeadersWidth = 30
        Me.GRIDSO.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDSO.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.GRIDSO.RowTemplate.Height = 20
        Me.GRIDSO.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSO.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDSO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDSO.Size = New System.Drawing.Size(1702, 529)
        Me.GRIDSO.TabIndex = 14
        Me.GRIDSO.TabStop = False
        '
        'cmbitemname
        '
        Me.cmbitemname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbitemname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbitemname.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbitemname.DropDownWidth = 400
        Me.cmbitemname.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbitemname.FormattingEnabled = True
        Me.cmbitemname.Location = New System.Drawing.Point(872, 4)
        Me.cmbitemname.Name = "cmbitemname"
        Me.cmbitemname.Size = New System.Drawing.Size(200, 23)
        Me.cmbitemname.TabIndex = 10
        '
        'cmbqtyunit
        '
        Me.cmbqtyunit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbqtyunit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbqtyunit.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbqtyunit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbqtyunit.FormattingEnabled = True
        Me.cmbqtyunit.Location = New System.Drawing.Point(1247, 4)
        Me.cmbqtyunit.Name = "cmbqtyunit"
        Me.cmbqtyunit.Size = New System.Drawing.Size(60, 23)
        Me.cmbqtyunit.TabIndex = 13
        '
        'txtsrno
        '
        Me.txtsrno.BackColor = System.Drawing.Color.Linen
        Me.txtsrno.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrno.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtsrno.Location = New System.Drawing.Point(3, 4)
        Me.txtsrno.Name = "txtsrno"
        Me.txtsrno.ReadOnly = True
        Me.txtsrno.Size = New System.Drawing.Size(30, 23)
        Me.txtsrno.TabIndex = 0
        Me.txtsrno.TabStop = False
        '
        'TXTDELPERIOD
        '
        Me.TXTDELPERIOD.BackColor = System.Drawing.Color.White
        Me.TXTDELPERIOD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTDELPERIOD.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTDELPERIOD.Location = New System.Drawing.Point(664, 4)
        Me.TXTDELPERIOD.Name = "TXTDELPERIOD"
        Me.TXTDELPERIOD.Size = New System.Drawing.Size(49, 23)
        Me.TXTDELPERIOD.TabIndex = 7
        Me.TXTDELPERIOD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTMTRS
        '
        Me.TXTMTRS.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTMTRS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTMTRS.Location = New System.Drawing.Point(1352, 4)
        Me.TXTMTRS.Name = "TXTMTRS"
        Me.TXTMTRS.Size = New System.Drawing.Size(60, 23)
        Me.TXTMTRS.TabIndex = 15
        Me.TXTMTRS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtQTY
        '
        Me.txtQTY.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtQTY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQTY.Location = New System.Drawing.Point(1192, 4)
        Me.txtQTY.Name = "txtQTY"
        Me.txtQTY.Size = New System.Drawing.Size(55, 23)
        Me.txtQTY.TabIndex = 12
        Me.txtQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTCRDAYS
        '
        Me.TXTCRDAYS.BackColor = System.Drawing.Color.White
        Me.TXTCRDAYS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCRDAYS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCRDAYS.Location = New System.Drawing.Point(563, 4)
        Me.TXTCRDAYS.Name = "TXTCRDAYS"
        Me.TXTCRDAYS.Size = New System.Drawing.Size(50, 23)
        Me.TXTCRDAYS.TabIndex = 5
        Me.TXTCRDAYS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBBUYERS
        '
        Me.CMBBUYERS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBBUYERS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBBUYERS.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBBUYERS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBBUYERS.FormattingEnabled = True
        Me.CMBBUYERS.Items.AddRange(New Object() {""})
        Me.CMBBUYERS.Location = New System.Drawing.Point(163, 4)
        Me.CMBBUYERS.Name = "CMBBUYERS"
        Me.CMBBUYERS.Size = New System.Drawing.Size(202, 23)
        Me.CMBBUYERS.TabIndex = 3
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.Color.Transparent
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdOK.FlatAppearance.BorderSize = 0
        Me.cmdOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOK.ForeColor = System.Drawing.Color.Black
        Me.cmdOK.Location = New System.Drawing.Point(616, 621)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(80, 28)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "&Save"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.Color.Transparent
        Me.cmdclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdclear.FlatAppearance.BorderSize = 0
        Me.cmdclear.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.ForeColor = System.Drawing.Color.Black
        Me.cmdclear.Location = New System.Drawing.Point(702, 621)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(80, 28)
        Me.cmdclear.TabIndex = 2
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
        Me.cmdEXIT.Location = New System.Drawing.Point(788, 621)
        Me.cmdEXIT.Name = "cmdEXIT"
        Me.cmdEXIT.Size = New System.Drawing.Size(80, 28)
        Me.cmdEXIT.TabIndex = 3
        Me.cmdEXIT.Text = "E&xit"
        Me.cmdEXIT.UseVisualStyleBackColor = False
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
        'gsrno
        '
        Me.gsrno.HeaderText = "Sr."
        Me.gsrno.Name = "gsrno"
        Me.gsrno.ReadOnly = True
        Me.gsrno.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gsrno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gsrno.Width = 30
        '
        'GNO
        '
        Me.GNO.HeaderText = "No"
        Me.GNO.Name = "GNO"
        Me.GNO.ReadOnly = True
        Me.GNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GNO.Width = 43
        '
        'GDATE
        '
        Me.GDATE.HeaderText = "Date"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.ReadOnly = True
        Me.GDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDATE.Width = 87
        '
        'GBUYERS
        '
        Me.GBUYERS.HeaderText = "Buyer's"
        Me.GBUYERS.Name = "GBUYERS"
        Me.GBUYERS.ReadOnly = True
        Me.GBUYERS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBUYERS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBUYERS.Width = 200
        '
        'GSELLERS
        '
        Me.GSELLERS.HeaderText = "Seller's"
        Me.GSELLERS.Name = "GSELLERS"
        Me.GSELLERS.ReadOnly = True
        Me.GSELLERS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSELLERS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSELLERS.Width = 200
        '
        'GCRDAYS
        '
        Me.GCRDAYS.HeaderText = "Cr Days"
        Me.GCRDAYS.Name = "GCRDAYS"
        Me.GCRDAYS.ReadOnly = True
        Me.GCRDAYS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCRDAYS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCRDAYS.Width = 50
        '
        'GDISCOUNT
        '
        Me.GDISCOUNT.HeaderText = "Disc %"
        Me.GDISCOUNT.Name = "GDISCOUNT"
        Me.GDISCOUNT.ReadOnly = True
        Me.GDISCOUNT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDISCOUNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDISCOUNT.Width = 50
        '
        'GDELPERIOD
        '
        Me.GDELPERIOD.HeaderText = "Del Period"
        Me.GDELPERIOD.Name = "GDELPERIOD"
        Me.GDELPERIOD.ReadOnly = True
        Me.GDELPERIOD.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDELPERIOD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDELPERIOD.Width = 50
        '
        'GDUEDATE
        '
        Me.GDUEDATE.HeaderText = "Due Date"
        Me.GDUEDATE.Name = "GDUEDATE"
        Me.GDUEDATE.ReadOnly = True
        Me.GDUEDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDUEDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDUEDATE.Width = 89
        '
        'GORDERNO
        '
        Me.GORDERNO.HeaderText = "Order No."
        Me.GORDERNO.Name = "GORDERNO"
        Me.GORDERNO.ReadOnly = True
        Me.GORDERNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GORDERNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GORDERNO.Width = 70
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
        'GDESIGN
        '
        Me.GDESIGN.HeaderText = "Design"
        Me.GDESIGN.Name = "GDESIGN"
        Me.GDESIGN.ReadOnly = True
        Me.GDESIGN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDESIGN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDESIGN.Width = 120
        '
        'gQty
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.NullValue = Nothing
        Me.gQty.DefaultCellStyle = DataGridViewCellStyle4
        Me.gQty.HeaderText = "Qty/Pcs"
        Me.gQty.Name = "gQty"
        Me.gQty.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gQty.Width = 55
        '
        'gqtyunit
        '
        Me.gqtyunit.HeaderText = "Qty Unit"
        Me.gqtyunit.Name = "gqtyunit"
        Me.gqtyunit.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gqtyunit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gqtyunit.Width = 60
        '
        'gcut
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.gcut.DefaultCellStyle = DataGridViewCellStyle5
        Me.gcut.HeaderText = "Cut"
        Me.gcut.Name = "gcut"
        Me.gcut.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gcut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gcut.Width = 45
        '
        'GMTRS
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GMTRS.DefaultCellStyle = DataGridViewCellStyle6
        Me.GMTRS.HeaderText = "Mtrs."
        Me.GMTRS.Name = "GMTRS"
        Me.GMTRS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMTRS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMTRS.Width = 60
        '
        'GRATE
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GRATE.DefaultCellStyle = DataGridViewCellStyle7
        Me.GRATE.HeaderText = "Rate"
        Me.GRATE.Name = "GRATE"
        Me.GRATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GRATE.Width = 65
        '
        'GREMARKS
        '
        Me.GREMARKS.HeaderText = "Remarks"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GREMARKS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GREMARKS.Width = 200
        '
        'MagicBox
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1484, 661)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "MagicBox"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Magic Box"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.TabControl2.ResumeLayout(False)
        Me.tbitem.ResumeLayout(False)
        Me.tbitem.PerformLayout()
        CType(Me.GRIDSO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents TXTDISCOUNT As TextBox
    Friend WithEvents TXTDELPERIOD As TextBox
    Friend WithEvents TXTCRDAYS As TextBox
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents tbitem As TabPage
    Friend WithEvents CMBDESIGN As ComboBox
    Friend WithEvents TXTCUT As TextBox
    Friend WithEvents TXTRATE As TextBox
    Friend WithEvents GRIDSO As DataGridView
    Friend WithEvents cmbitemname As ComboBox
    Friend WithEvents cmbqtyunit As ComboBox
    Friend WithEvents txtsrno As TextBox
    Friend WithEvents TXTMTRS As TextBox
    Friend WithEvents txtQTY As TextBox
    Friend WithEvents cmdOK As Button
    Friend WithEvents cmdclear As Button
    Friend WithEvents CMBBUYERS As ComboBox
    Friend WithEvents cmdEXIT As Button
    Friend WithEvents CMBSELLERS As ComboBox
    Friend WithEvents TXTORDERNO As TextBox
    Friend WithEvents TXTNO As TextBox
    Friend WithEvents TXTREMARKS As TextBox
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents duedate As DateTimePicker
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents ORDERDATE As DateTimePicker
    Friend WithEvents gsrno As DataGridViewTextBoxColumn
    Friend WithEvents GNO As DataGridViewTextBoxColumn
    Friend WithEvents GDATE As DataGridViewTextBoxColumn
    Friend WithEvents GBUYERS As DataGridViewTextBoxColumn
    Friend WithEvents GSELLERS As DataGridViewTextBoxColumn
    Friend WithEvents GCRDAYS As DataGridViewTextBoxColumn
    Friend WithEvents GDISCOUNT As DataGridViewTextBoxColumn
    Friend WithEvents GDELPERIOD As DataGridViewTextBoxColumn
    Friend WithEvents GDUEDATE As DataGridViewTextBoxColumn
    Friend WithEvents GORDERNO As DataGridViewTextBoxColumn
    Friend WithEvents gitemname As DataGridViewTextBoxColumn
    Friend WithEvents GDESIGN As DataGridViewTextBoxColumn
    Friend WithEvents gQty As DataGridViewTextBoxColumn
    Friend WithEvents gqtyunit As DataGridViewTextBoxColumn
    Friend WithEvents gcut As DataGridViewTextBoxColumn
    Friend WithEvents GMTRS As DataGridViewTextBoxColumn
    Friend WithEvents GRATE As DataGridViewTextBoxColumn
    Friend WithEvents GREMARKS As DataGridViewTextBoxColumn
End Class
