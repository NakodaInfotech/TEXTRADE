<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OpeningBeamStock
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
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.TXTNO = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.openingdate = New System.Windows.Forms.DateTimePicker()
        Me.LBLEINVGENERATED = New System.Windows.Forms.Label()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.cmbcode = New System.Windows.Forms.ComboBox()
        Me.TXTOPROLLSSTOCKNO = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LBLTOTALBEAMWT = New System.Windows.Forms.Label()
        Me.LBLTOTALROLLNO = New System.Windows.Forms.Label()
        Me.CMBROLLNO = New System.Windows.Forms.ComboBox()
        Me.txtsrno = New System.Windows.Forms.TextBox()
        Me.TXTBREAKAGE = New System.Windows.Forms.TextBox()
        Me.TXTBEAMWT = New System.Windows.Forms.TextBox()
        Me.LBLTOTALMTRS = New System.Windows.Forms.Label()
        Me.TXTSECTION = New System.Windows.Forms.TextBox()
        Me.LBLTOTAL = New System.Windows.Forms.Label()
        Me.TXTGAMANO = New System.Windows.Forms.TextBox()
        Me.TXTTOTALMTRS = New System.Windows.Forms.TextBox()
        Me.TXTBEAMNO = New System.Windows.Forms.TextBox()
        Me.CMBBEAMNAME = New System.Windows.Forms.ComboBox()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.CMBMILL = New System.Windows.Forms.ComboBox()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.TXTTOTALENDS = New System.Windows.Forms.TextBox()
        Me.CMBOURGODOWN = New System.Windows.Forms.ComboBox()
        Me.GRIDSTOCK = New System.Windows.Forms.DataGridView()
        Me.GGRIDSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBEAMSTOCKNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGODOWN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMILL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBEAMNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBEAMNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTOTALENDS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTOTALMTRS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGAMANO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSECTION = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GROLLNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBEAMWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBREAKAGE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTMTRS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdexit = New System.Windows.Forms.Button()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BlendPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.GRIDSTOCK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTNO)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.openingdate)
        Me.BlendPanel1.Controls.Add(Me.LBLEINVGENERATED)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.cmbcode)
        Me.BlendPanel1.Controls.Add(Me.TXTOPROLLSSTOCKNO)
        Me.BlendPanel1.Controls.Add(Me.Panel1)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 613)
        Me.BlendPanel1.TabIndex = 1
        '
        'TXTNO
        '
        Me.TXTNO.BackColor = System.Drawing.Color.White
        Me.TXTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTNO.Location = New System.Drawing.Point(511, 15)
        Me.TXTNO.Name = "TXTNO"
        Me.TXTNO.ReadOnly = True
        Me.TXTNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTNO.TabIndex = 942
        Me.TXTNO.Text = " "
        Me.TXTNO.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(1010, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 15)
        Me.Label6.TabIndex = 941
        Me.Label6.Text = "Date :"
        Me.Label6.Visible = False
        '
        'openingdate
        '
        Me.openingdate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.openingdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.openingdate.Location = New System.Drawing.Point(1053, 19)
        Me.openingdate.Name = "openingdate"
        Me.openingdate.Size = New System.Drawing.Size(88, 23)
        Me.openingdate.TabIndex = 940
        Me.openingdate.Value = New Date(2013, 4, 1, 8, 54, 0, 0)
        Me.openingdate.Visible = False
        '
        'LBLEINVGENERATED
        '
        Me.LBLEINVGENERATED.AutoSize = True
        Me.LBLEINVGENERATED.BackColor = System.Drawing.Color.Transparent
        Me.LBLEINVGENERATED.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLEINVGENERATED.ForeColor = System.Drawing.Color.Black
        Me.LBLEINVGENERATED.Location = New System.Drawing.Point(12, 9)
        Me.LBLEINVGENERATED.Name = "LBLEINVGENERATED"
        Me.LBLEINVGENERATED.Size = New System.Drawing.Size(218, 29)
        Me.LBLEINVGENERATED.TabIndex = 939
        Me.LBLEINVGENERATED.Text = "Opening Beam Stock"
        '
        'TXTADD
        '
        Me.TXTADD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.Location = New System.Drawing.Point(1184, 6)
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(29, 21)
        Me.TXTADD.TabIndex = 807
        Me.TXTADD.TabStop = False
        Me.TXTADD.Visible = False
        '
        'cmbcode
        '
        Me.cmbcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbcode.FormattingEnabled = True
        Me.cmbcode.Location = New System.Drawing.Point(1219, 3)
        Me.cmbcode.Name = "cmbcode"
        Me.cmbcode.Size = New System.Drawing.Size(25, 23)
        Me.cmbcode.TabIndex = 717
        Me.cmbcode.Visible = False
        '
        'TXTOPROLLSSTOCKNO
        '
        Me.TXTOPROLLSSTOCKNO.BackColor = System.Drawing.Color.White
        Me.TXTOPROLLSSTOCKNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTOPROLLSSTOCKNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTOPROLLSSTOCKNO.Location = New System.Drawing.Point(1250, 3)
        Me.TXTOPROLLSSTOCKNO.Name = "TXTOPROLLSSTOCKNO"
        Me.TXTOPROLLSSTOCKNO.ReadOnly = True
        Me.TXTOPROLLSSTOCKNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTOPROLLSSTOCKNO.TabIndex = 715
        Me.TXTOPROLLSSTOCKNO.Text = " "
        Me.TXTOPROLLSSTOCKNO.Visible = False
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.Controls.Add(Me.LBLTOTALBEAMWT)
        Me.Panel1.Controls.Add(Me.LBLTOTALROLLNO)
        Me.Panel1.Controls.Add(Me.CMBROLLNO)
        Me.Panel1.Controls.Add(Me.txtsrno)
        Me.Panel1.Controls.Add(Me.TXTBREAKAGE)
        Me.Panel1.Controls.Add(Me.TXTBEAMWT)
        Me.Panel1.Controls.Add(Me.LBLTOTALMTRS)
        Me.Panel1.Controls.Add(Me.TXTSECTION)
        Me.Panel1.Controls.Add(Me.LBLTOTAL)
        Me.Panel1.Controls.Add(Me.TXTGAMANO)
        Me.Panel1.Controls.Add(Me.TXTTOTALMTRS)
        Me.Panel1.Controls.Add(Me.TXTBEAMNO)
        Me.Panel1.Controls.Add(Me.CMBBEAMNAME)
        Me.Panel1.Controls.Add(Me.CMBNAME)
        Me.Panel1.Controls.Add(Me.CMBMILL)
        Me.Panel1.Controls.Add(Me.TXTREMARKS)
        Me.Panel1.Controls.Add(Me.TXTTOTALENDS)
        Me.Panel1.Controls.Add(Me.CMBOURGODOWN)
        Me.Panel1.Controls.Add(Me.GRIDSTOCK)
        Me.Panel1.Location = New System.Drawing.Point(6, 48)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1297, 525)
        Me.Panel1.TabIndex = 0
        '
        'LBLTOTALBEAMWT
        '
        Me.LBLTOTALBEAMWT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALBEAMWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALBEAMWT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALBEAMWT.Location = New System.Drawing.Point(1430, 487)
        Me.LBLTOTALBEAMWT.Name = "LBLTOTALBEAMWT"
        Me.LBLTOTALBEAMWT.Size = New System.Drawing.Size(65, 15)
        Me.LBLTOTALBEAMWT.TabIndex = 834
        Me.LBLTOTALBEAMWT.Text = "0"
        Me.LBLTOTALBEAMWT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALROLLNO
        '
        Me.LBLTOTALROLLNO.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALROLLNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALROLLNO.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALROLLNO.Location = New System.Drawing.Point(1329, 487)
        Me.LBLTOTALROLLNO.Name = "LBLTOTALROLLNO"
        Me.LBLTOTALROLLNO.Size = New System.Drawing.Size(65, 15)
        Me.LBLTOTALROLLNO.TabIndex = 833
        Me.LBLTOTALROLLNO.Text = "0"
        Me.LBLTOTALROLLNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CMBROLLNO
        '
        Me.CMBROLLNO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBROLLNO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBROLLNO.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBROLLNO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBROLLNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBROLLNO.FormattingEnabled = True
        Me.CMBROLLNO.Items.AddRange(New Object() {""})
        Me.CMBROLLNO.Location = New System.Drawing.Point(1265, 3)
        Me.CMBROLLNO.MaxDropDownItems = 14
        Me.CMBROLLNO.Name = "CMBROLLNO"
        Me.CMBROLLNO.Size = New System.Drawing.Size(129, 23)
        Me.CMBROLLNO.TabIndex = 9
        '
        'txtsrno
        '
        Me.txtsrno.BackColor = System.Drawing.Color.Linen
        Me.txtsrno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrno.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtsrno.Location = New System.Drawing.Point(4, 4)
        Me.txtsrno.Name = "txtsrno"
        Me.txtsrno.ReadOnly = True
        Me.txtsrno.Size = New System.Drawing.Size(40, 22)
        Me.txtsrno.TabIndex = 18
        Me.txtsrno.TabStop = False
        Me.txtsrno.Text = " "
        '
        'TXTBREAKAGE
        '
        Me.TXTBREAKAGE.BackColor = System.Drawing.Color.White
        Me.TXTBREAKAGE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBREAKAGE.Location = New System.Drawing.Point(1495, 3)
        Me.TXTBREAKAGE.MaxLength = 200
        Me.TXTBREAKAGE.Name = "TXTBREAKAGE"
        Me.TXTBREAKAGE.Size = New System.Drawing.Size(100, 23)
        Me.TXTBREAKAGE.TabIndex = 11
        Me.TXTBREAKAGE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTBEAMWT
        '
        Me.TXTBEAMWT.BackColor = System.Drawing.Color.White
        Me.TXTBEAMWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBEAMWT.Location = New System.Drawing.Point(1394, 3)
        Me.TXTBEAMWT.MaxLength = 200
        Me.TXTBEAMWT.Name = "TXTBEAMWT"
        Me.TXTBEAMWT.Size = New System.Drawing.Size(101, 23)
        Me.TXTBEAMWT.TabIndex = 10
        Me.TXTBEAMWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLTOTALMTRS
        '
        Me.LBLTOTALMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALMTRS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALMTRS.Location = New System.Drawing.Point(949, 487)
        Me.LBLTOTALMTRS.Name = "LBLTOTALMTRS"
        Me.LBLTOTALMTRS.Size = New System.Drawing.Size(65, 15)
        Me.LBLTOTALMTRS.TabIndex = 832
        Me.LBLTOTALMTRS.Text = "0"
        Me.LBLTOTALMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTSECTION
        '
        Me.TXTSECTION.BackColor = System.Drawing.Color.White
        Me.TXTSECTION.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSECTION.Location = New System.Drawing.Point(1165, 3)
        Me.TXTSECTION.MaxLength = 200
        Me.TXTSECTION.Name = "TXTSECTION"
        Me.TXTSECTION.Size = New System.Drawing.Size(100, 23)
        Me.TXTSECTION.TabIndex = 8
        Me.TXTSECTION.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLTOTAL
        '
        Me.LBLTOTAL.AutoSize = True
        Me.LBLTOTAL.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTAL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTAL.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTAL.Location = New System.Drawing.Point(912, 487)
        Me.LBLTOTAL.Name = "LBLTOTAL"
        Me.LBLTOTAL.Size = New System.Drawing.Size(33, 15)
        Me.LBLTOTAL.TabIndex = 831
        Me.LBLTOTAL.Text = "Total"
        '
        'TXTGAMANO
        '
        Me.TXTGAMANO.BackColor = System.Drawing.Color.White
        Me.TXTGAMANO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTGAMANO.Location = New System.Drawing.Point(1064, 3)
        Me.TXTGAMANO.MaxLength = 200
        Me.TXTGAMANO.Name = "TXTGAMANO"
        Me.TXTGAMANO.Size = New System.Drawing.Size(101, 23)
        Me.TXTGAMANO.TabIndex = 7
        Me.TXTGAMANO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTTOTALMTRS
        '
        Me.TXTTOTALMTRS.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTTOTALMTRS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTOTALMTRS.Location = New System.Drawing.Point(964, 3)
        Me.TXTTOTALMTRS.Name = "TXTTOTALMTRS"
        Me.TXTTOTALMTRS.Size = New System.Drawing.Size(100, 23)
        Me.TXTTOTALMTRS.TabIndex = 6
        Me.TXTTOTALMTRS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTBEAMNO
        '
        Me.TXTBEAMNO.BackColor = System.Drawing.Color.White
        Me.TXTBEAMNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBEAMNO.Location = New System.Drawing.Point(564, 3)
        Me.TXTBEAMNO.Name = "TXTBEAMNO"
        Me.TXTBEAMNO.Size = New System.Drawing.Size(100, 23)
        Me.TXTBEAMNO.TabIndex = 3
        Me.TXTBEAMNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBBEAMNAME
        '
        Me.CMBBEAMNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBBEAMNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBBEAMNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBBEAMNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBBEAMNAME.FormattingEnabled = True
        Me.CMBBEAMNAME.Location = New System.Drawing.Point(664, 3)
        Me.CMBBEAMNAME.Name = "CMBBEAMNAME"
        Me.CMBBEAMNAME.Size = New System.Drawing.Size(200, 23)
        Me.CMBBEAMNAME.TabIndex = 4
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(164, 3)
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(200, 23)
        Me.CMBNAME.TabIndex = 1
        '
        'CMBMILL
        '
        Me.CMBMILL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBMILL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBMILL.BackColor = System.Drawing.Color.White
        Me.CMBMILL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBMILL.FormattingEnabled = True
        Me.CMBMILL.Location = New System.Drawing.Point(364, 3)
        Me.CMBMILL.Name = "CMBMILL"
        Me.CMBMILL.Size = New System.Drawing.Size(200, 23)
        Me.CMBMILL.TabIndex = 2
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.BackColor = System.Drawing.Color.White
        Me.TXTREMARKS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTREMARKS.Location = New System.Drawing.Point(1595, 3)
        Me.TXTREMARKS.MaxLength = 200
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(200, 23)
        Me.TXTREMARKS.TabIndex = 12
        '
        'TXTTOTALENDS
        '
        Me.TXTTOTALENDS.BackColor = System.Drawing.Color.White
        Me.TXTTOTALENDS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTOTALENDS.Location = New System.Drawing.Point(864, 3)
        Me.TXTTOTALENDS.Name = "TXTTOTALENDS"
        Me.TXTTOTALENDS.Size = New System.Drawing.Size(100, 23)
        Me.TXTTOTALENDS.TabIndex = 5
        Me.TXTTOTALENDS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBOURGODOWN
        '
        Me.CMBOURGODOWN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBOURGODOWN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBOURGODOWN.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBOURGODOWN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBOURGODOWN.FormattingEnabled = True
        Me.CMBOURGODOWN.Location = New System.Drawing.Point(44, 3)
        Me.CMBOURGODOWN.Name = "CMBOURGODOWN"
        Me.CMBOURGODOWN.Size = New System.Drawing.Size(120, 23)
        Me.CMBOURGODOWN.TabIndex = 0
        '
        'GRIDSTOCK
        '
        Me.GRIDSTOCK.AllowUserToAddRows = False
        Me.GRIDSTOCK.AllowUserToDeleteRows = False
        Me.GRIDSTOCK.AllowUserToResizeColumns = False
        Me.GRIDSTOCK.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDSTOCK.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDSTOCK.BackgroundColor = System.Drawing.Color.White
        Me.GRIDSTOCK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDSTOCK.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDSTOCK.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDSTOCK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDSTOCK.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GGRIDSRNO, Me.GBEAMSTOCKNO, Me.GGODOWN, Me.GNAME, Me.GMILL, Me.GBEAMNO, Me.GBEAMNAME, Me.GTOTALENDS, Me.GTOTALMTRS, Me.GGAMANO, Me.GSECTION, Me.GROLLNO, Me.GBEAMWT, Me.GBREAKAGE, Me.GREMARKS, Me.GOUTMTRS, Me.GOUTWT})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSTOCK.DefaultCellStyle = DataGridViewCellStyle3
        Me.GRIDSTOCK.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDSTOCK.Location = New System.Drawing.Point(3, 26)
        Me.GRIDSTOCK.MultiSelect = False
        Me.GRIDSTOCK.Name = "GRIDSTOCK"
        Me.GRIDSTOCK.ReadOnly = True
        Me.GRIDSTOCK.RowHeadersVisible = False
        Me.GRIDSTOCK.RowHeadersWidth = 30
        Me.GRIDSTOCK.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDSTOCK.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.GRIDSTOCK.RowTemplate.Height = 20
        Me.GRIDSTOCK.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSTOCK.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.GRIDSTOCK.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDSTOCK.Size = New System.Drawing.Size(1932, 458)
        Me.GRIDSTOCK.TabIndex = 9
        Me.GRIDSTOCK.TabStop = False
        '
        'GGRIDSRNO
        '
        Me.GGRIDSRNO.HeaderText = "Sr."
        Me.GGRIDSRNO.Name = "GGRIDSRNO"
        Me.GGRIDSRNO.ReadOnly = True
        Me.GGRIDSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGRIDSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GGRIDSRNO.Width = 40
        '
        'GBEAMSTOCKNO
        '
        Me.GBEAMSTOCKNO.HeaderText = "NO"
        Me.GBEAMSTOCKNO.Name = "GBEAMSTOCKNO"
        Me.GBEAMSTOCKNO.ReadOnly = True
        Me.GBEAMSTOCKNO.Visible = False
        '
        'GGODOWN
        '
        Me.GGODOWN.HeaderText = "Godown"
        Me.GGODOWN.Name = "GGODOWN"
        Me.GGODOWN.ReadOnly = True
        Me.GGODOWN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGODOWN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GGODOWN.Width = 120
        '
        'GNAME
        '
        Me.GNAME.HeaderText = "Warper Name"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.ReadOnly = True
        Me.GNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GNAME.Width = 200
        '
        'GMILL
        '
        Me.GMILL.HeaderText = "Mill Name"
        Me.GMILL.Name = "GMILL"
        Me.GMILL.ReadOnly = True
        Me.GMILL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMILL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMILL.Width = 200
        '
        'GBEAMNO
        '
        Me.GBEAMNO.HeaderText = "Beam No"
        Me.GBEAMNO.Name = "GBEAMNO"
        Me.GBEAMNO.ReadOnly = True
        Me.GBEAMNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBEAMNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GBEAMNAME
        '
        Me.GBEAMNAME.HeaderText = "Beam Name"
        Me.GBEAMNAME.Name = "GBEAMNAME"
        Me.GBEAMNAME.ReadOnly = True
        Me.GBEAMNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBEAMNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBEAMNAME.Width = 200
        '
        'GTOTALENDS
        '
        Me.GTOTALENDS.HeaderText = "Total Ends"
        Me.GTOTALENDS.Name = "GTOTALENDS"
        Me.GTOTALENDS.ReadOnly = True
        Me.GTOTALENDS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTOTALENDS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GTOTALMTRS
        '
        Me.GTOTALMTRS.HeaderText = "Total Mtrs"
        Me.GTOTALMTRS.Name = "GTOTALMTRS"
        Me.GTOTALMTRS.ReadOnly = True
        Me.GTOTALMTRS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTOTALMTRS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GGAMANO
        '
        Me.GGAMANO.HeaderText = "Gama No"
        Me.GGAMANO.Name = "GGAMANO"
        Me.GGAMANO.ReadOnly = True
        Me.GGAMANO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGAMANO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GSECTION
        '
        Me.GSECTION.HeaderText = "Section"
        Me.GSECTION.Name = "GSECTION"
        Me.GSECTION.ReadOnly = True
        Me.GSECTION.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSECTION.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GROLLNO
        '
        Me.GROLLNO.HeaderText = "Roll No"
        Me.GROLLNO.Name = "GROLLNO"
        Me.GROLLNO.ReadOnly = True
        Me.GROLLNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GROLLNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GROLLNO.Width = 130
        '
        'GBEAMWT
        '
        Me.GBEAMWT.HeaderText = "Beam Wt"
        Me.GBEAMWT.Name = "GBEAMWT"
        Me.GBEAMWT.ReadOnly = True
        Me.GBEAMWT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBEAMWT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GBREAKAGE
        '
        Me.GBREAKAGE.HeaderText = "Breakage"
        Me.GBREAKAGE.Name = "GBREAKAGE"
        Me.GBREAKAGE.ReadOnly = True
        Me.GBREAKAGE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBREAKAGE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GREMARKS
        '
        Me.GREMARKS.HeaderText = "Remarks"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.ReadOnly = True
        Me.GREMARKS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GREMARKS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GREMARKS.Width = 200
        '
        'GOUTMTRS
        '
        Me.GOUTMTRS.HeaderText = "OUTMTRS"
        Me.GOUTMTRS.Name = "GOUTMTRS"
        Me.GOUTMTRS.ReadOnly = True
        Me.GOUTMTRS.Visible = False
        '
        'GOUTWT
        '
        Me.GOUTWT.HeaderText = "OUTWT"
        Me.GOUTWT.Name = "GOUTWT"
        Me.GOUTWT.ReadOnly = True
        Me.GOUTWT.Visible = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(606, 579)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 1
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'OpeningBeamStock
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 613)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "OpeningBeamStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "OpeningBeamStock"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GRIDSTOCK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents cmbcode As ComboBox
    Friend WithEvents LBLTOTALMTRS As Label
    Friend WithEvents LBLTOTAL As Label
    Friend WithEvents TXTOPROLLSSTOCKNO As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CMBNAME As ComboBox
    Friend WithEvents CMBMILL As ComboBox
    Friend WithEvents TXTREMARKS As TextBox
    Friend WithEvents TXTTOTALENDS As TextBox
    Friend WithEvents CMBOURGODOWN As ComboBox
    Friend WithEvents GRIDSTOCK As DataGridView
    Friend WithEvents cmdexit As Button
    Friend WithEvents TXTGAMANO As TextBox
    Friend WithEvents TXTTOTALMTRS As TextBox
    Friend WithEvents TXTBEAMNO As TextBox
    Friend WithEvents CMBBEAMNAME As ComboBox
    Friend WithEvents TXTBEAMWT As TextBox
    Friend WithEvents TXTSECTION As TextBox
    Friend WithEvents TXTBREAKAGE As TextBox
    Friend WithEvents txtsrno As TextBox
    Friend WithEvents CMBROLLNO As ComboBox
    Friend WithEvents LBLEINVGENERATED As Label
    Friend WithEvents LBLTOTALROLLNO As Label
    Friend WithEvents LBLTOTALBEAMWT As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents openingdate As DateTimePicker
    Friend WithEvents TXTNO As TextBox
    Friend WithEvents GGRIDSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GBEAMSTOCKNO As DataGridViewTextBoxColumn
    Friend WithEvents GGODOWN As DataGridViewTextBoxColumn
    Friend WithEvents GNAME As DataGridViewTextBoxColumn
    Friend WithEvents GMILL As DataGridViewTextBoxColumn
    Friend WithEvents GBEAMNO As DataGridViewTextBoxColumn
    Friend WithEvents GBEAMNAME As DataGridViewTextBoxColumn
    Friend WithEvents GTOTALENDS As DataGridViewTextBoxColumn
    Friend WithEvents GTOTALMTRS As DataGridViewTextBoxColumn
    Friend WithEvents GGAMANO As DataGridViewTextBoxColumn
    Friend WithEvents GSECTION As DataGridViewTextBoxColumn
    Friend WithEvents GROLLNO As DataGridViewTextBoxColumn
    Friend WithEvents GBEAMWT As DataGridViewTextBoxColumn
    Friend WithEvents GBREAKAGE As DataGridViewTextBoxColumn
    Friend WithEvents GREMARKS As DataGridViewTextBoxColumn
    Friend WithEvents GOUTMTRS As DataGridViewTextBoxColumn
    Friend WithEvents GOUTWT As DataGridViewTextBoxColumn
End Class
