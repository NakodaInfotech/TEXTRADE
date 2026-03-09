<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OpeningStockYarn
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.TXTTO = New System.Windows.Forms.TextBox()
        Me.LBLTO = New System.Windows.Forms.Label()
        Me.TXTBARCODE = New System.Windows.Forms.TextBox()
        Me.LBLFROM = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TXTGRIDREMARKS = New System.Windows.Forms.TextBox()
        Me.TXTAMOUNT = New System.Windows.Forms.TextBox()
        Me.TXTRATE = New System.Windows.Forms.TextBox()
        Me.CMBPER = New System.Windows.Forms.ComboBox()
        Me.CMBRACK = New System.Windows.Forms.ComboBox()
        Me.TXTBILLNO = New System.Windows.Forms.TextBox()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.txtsrno = New System.Windows.Forms.TextBox()
        Me.TXTCONES = New System.Windows.Forms.TextBox()
        Me.TXTBAGS = New System.Windows.Forms.TextBox()
        Me.CMBYARNQUALITY = New System.Windows.Forms.ComboBox()
        Me.TXTLRNO = New System.Windows.Forms.TextBox()
        Me.gridstock = New System.Windows.Forms.DataGridView()
        Me.gsrno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLOTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GYARNQUALITY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMILL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDESIGN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSHADE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPROCESS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLRDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBILLNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gtoname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGODOWN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBAGS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCONES = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRACK = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGRIDREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAMOUNT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBARCODE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTBAGS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDONE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CMBSHADE = New System.Windows.Forms.ComboBox()
        Me.TXTLOTNO = New System.Windows.Forms.TextBox()
        Me.CMBDESIGN = New System.Windows.Forms.ComboBox()
        Me.CMBPROCESS = New System.Windows.Forms.ComboBox()
        Me.CMBMILL = New System.Windows.Forms.ComboBox()
        Me.TXTWT = New System.Windows.Forms.TextBox()
        Me.CMBTONAME = New System.Windows.Forms.ComboBox()
        Me.CMBGODOWN = New System.Windows.Forms.ComboBox()
        Me.TXTFROM = New System.Windows.Forms.TextBox()
        Me.LRDATE = New System.Windows.Forms.DateTimePicker()
        Me.TXTNO = New System.Windows.Forms.TextBox()
        Me.cmbdyeing = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.txtadd = New System.Windows.Forms.TextBox()
        Me.cmbtype = New System.Windows.Forms.ComboBox()
        Me.openingdate = New System.Windows.Forms.DateTimePicker()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.BlendPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.gridstock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTTO)
        Me.BlendPanel1.Controls.Add(Me.LBLTO)
        Me.BlendPanel1.Controls.Add(Me.TXTBARCODE)
        Me.BlendPanel1.Controls.Add(Me.LBLFROM)
        Me.BlendPanel1.Controls.Add(Me.Panel1)
        Me.BlendPanel1.Controls.Add(Me.TXTFROM)
        Me.BlendPanel1.Controls.Add(Me.LRDATE)
        Me.BlendPanel1.Controls.Add(Me.TXTNO)
        Me.BlendPanel1.Controls.Add(Me.cmbdyeing)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.CMBCODE)
        Me.BlendPanel1.Controls.Add(Me.txtadd)
        Me.BlendPanel1.Controls.Add(Me.cmbtype)
        Me.BlendPanel1.Controls.Add(Me.openingdate)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1274, 571)
        Me.BlendPanel1.TabIndex = 0
        '
        'TXTTO
        '
        Me.TXTTO.BackColor = System.Drawing.Color.White
        Me.TXTTO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTTO.Location = New System.Drawing.Point(993, 16)
        Me.TXTTO.Name = "TXTTO"
        Me.TXTTO.Size = New System.Drawing.Size(41, 23)
        Me.TXTTO.TabIndex = 724
        Me.TXTTO.Text = " "
        Me.TXTTO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLTO
        '
        Me.LBLTO.AutoSize = True
        Me.LBLTO.BackColor = System.Drawing.Color.Transparent
        Me.LBLTO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTO.ForeColor = System.Drawing.Color.Black
        Me.LBLTO.Location = New System.Drawing.Point(973, 20)
        Me.LBLTO.Name = "LBLTO"
        Me.LBLTO.Size = New System.Drawing.Size(19, 15)
        Me.LBLTO.TabIndex = 723
        Me.LBLTO.Text = "To"
        '
        'TXTBARCODE
        '
        Me.TXTBARCODE.BackColor = System.Drawing.Color.White
        Me.TXTBARCODE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBARCODE.Location = New System.Drawing.Point(1107, 16)
        Me.TXTBARCODE.Name = "TXTBARCODE"
        Me.TXTBARCODE.Size = New System.Drawing.Size(111, 23)
        Me.TXTBARCODE.TabIndex = 714
        Me.TXTBARCODE.Visible = False
        '
        'LBLFROM
        '
        Me.LBLFROM.AutoSize = True
        Me.LBLFROM.BackColor = System.Drawing.Color.Transparent
        Me.LBLFROM.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLFROM.ForeColor = System.Drawing.Color.Black
        Me.LBLFROM.Location = New System.Drawing.Point(814, 20)
        Me.LBLFROM.Name = "LBLFROM"
        Me.LBLFROM.Size = New System.Drawing.Size(113, 15)
        Me.LBLFROM.TabIndex = 722
        Me.LBLFROM.Text = "Print Barcode From"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.TXTGRIDREMARKS)
        Me.Panel1.Controls.Add(Me.TXTAMOUNT)
        Me.Panel1.Controls.Add(Me.TXTRATE)
        Me.Panel1.Controls.Add(Me.CMBPER)
        Me.Panel1.Controls.Add(Me.CMBRACK)
        Me.Panel1.Controls.Add(Me.TXTBILLNO)
        Me.Panel1.Controls.Add(Me.TXTREMARKS)
        Me.Panel1.Controls.Add(Me.txtsrno)
        Me.Panel1.Controls.Add(Me.TXTCONES)
        Me.Panel1.Controls.Add(Me.TXTBAGS)
        Me.Panel1.Controls.Add(Me.CMBYARNQUALITY)
        Me.Panel1.Controls.Add(Me.TXTLRNO)
        Me.Panel1.Controls.Add(Me.gridstock)
        Me.Panel1.Controls.Add(Me.CMBSHADE)
        Me.Panel1.Controls.Add(Me.TXTLOTNO)
        Me.Panel1.Controls.Add(Me.CMBDESIGN)
        Me.Panel1.Controls.Add(Me.CMBPROCESS)
        Me.Panel1.Controls.Add(Me.CMBMILL)
        Me.Panel1.Controls.Add(Me.TXTWT)
        Me.Panel1.Controls.Add(Me.CMBTONAME)
        Me.Panel1.Controls.Add(Me.CMBGODOWN)
        Me.Panel1.Location = New System.Drawing.Point(12, 41)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1250, 493)
        Me.Panel1.TabIndex = 1
        '
        'TXTGRIDREMARKS
        '
        Me.TXTGRIDREMARKS.BackColor = System.Drawing.Color.White
        Me.TXTGRIDREMARKS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTGRIDREMARKS.Location = New System.Drawing.Point(1533, 5)
        Me.TXTGRIDREMARKS.Name = "TXTGRIDREMARKS"
        Me.TXTGRIDREMARKS.Size = New System.Drawing.Size(120, 22)
        Me.TXTGRIDREMARKS.TabIndex = 36
        '
        'TXTAMOUNT
        '
        Me.TXTAMOUNT.BackColor = System.Drawing.Color.Linen
        Me.TXTAMOUNT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTAMOUNT.Location = New System.Drawing.Point(1793, 5)
        Me.TXTAMOUNT.Name = "TXTAMOUNT"
        Me.TXTAMOUNT.ReadOnly = True
        Me.TXTAMOUNT.Size = New System.Drawing.Size(80, 22)
        Me.TXTAMOUNT.TabIndex = 35
        Me.TXTAMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTRATE
        '
        Me.TXTRATE.BackColor = System.Drawing.Color.White
        Me.TXTRATE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTRATE.Location = New System.Drawing.Point(1713, 5)
        Me.TXTRATE.Name = "TXTRATE"
        Me.TXTRATE.Size = New System.Drawing.Size(80, 22)
        Me.TXTRATE.TabIndex = 34
        Me.TXTRATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBPER
        '
        Me.CMBPER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBPER.FormattingEnabled = True
        Me.CMBPER.Items.AddRange(New Object() {"Bags", "Wt"})
        Me.CMBPER.Location = New System.Drawing.Point(1653, 4)
        Me.CMBPER.Name = "CMBPER"
        Me.CMBPER.Size = New System.Drawing.Size(60, 23)
        Me.CMBPER.TabIndex = 33
        '
        'CMBRACK
        '
        Me.CMBRACK.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBRACK.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBRACK.DropDownWidth = 400
        Me.CMBRACK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBRACK.FormattingEnabled = True
        Me.CMBRACK.Location = New System.Drawing.Point(1453, 4)
        Me.CMBRACK.Name = "CMBRACK"
        Me.CMBRACK.Size = New System.Drawing.Size(80, 23)
        Me.CMBRACK.TabIndex = 14
        '
        'TXTBILLNO
        '
        Me.TXTBILLNO.BackColor = System.Drawing.Color.White
        Me.TXTBILLNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBILLNO.Location = New System.Drawing.Point(913, 4)
        Me.TXTBILLNO.Name = "TXTBILLNO"
        Me.TXTBILLNO.Size = New System.Drawing.Size(80, 23)
        Me.TXTBILLNO.TabIndex = 8
        Me.TXTBILLNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.BackColor = System.Drawing.Color.White
        Me.TXTREMARKS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTREMARKS.Location = New System.Drawing.Point(683, 4)
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(150, 23)
        Me.TXTREMARKS.TabIndex = 6
        Me.TXTREMARKS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtsrno
        '
        Me.txtsrno.BackColor = System.Drawing.Color.Linen
        Me.txtsrno.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrno.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtsrno.Location = New System.Drawing.Point(3, 4)
        Me.txtsrno.Name = "txtsrno"
        Me.txtsrno.ReadOnly = True
        Me.txtsrno.Size = New System.Drawing.Size(40, 23)
        Me.txtsrno.TabIndex = 0
        Me.txtsrno.TabStop = False
        Me.txtsrno.Text = " "
        '
        'TXTCONES
        '
        Me.TXTCONES.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTCONES.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCONES.Location = New System.Drawing.Point(1393, 4)
        Me.TXTCONES.Name = "TXTCONES"
        Me.TXTCONES.Size = New System.Drawing.Size(60, 23)
        Me.TXTCONES.TabIndex = 13
        Me.TXTCONES.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTBAGS
        '
        Me.TXTBAGS.BackColor = System.Drawing.Color.White
        Me.TXTBAGS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBAGS.Location = New System.Drawing.Point(1273, 4)
        Me.TXTBAGS.Name = "TXTBAGS"
        Me.TXTBAGS.Size = New System.Drawing.Size(60, 23)
        Me.TXTBAGS.TabIndex = 11
        Me.TXTBAGS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBYARNQUALITY
        '
        Me.CMBYARNQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBYARNQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBYARNQUALITY.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBYARNQUALITY.DropDownWidth = 400
        Me.CMBYARNQUALITY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBYARNQUALITY.FormattingEnabled = True
        Me.CMBYARNQUALITY.Location = New System.Drawing.Point(103, 4)
        Me.CMBYARNQUALITY.Name = "CMBYARNQUALITY"
        Me.CMBYARNQUALITY.Size = New System.Drawing.Size(160, 23)
        Me.CMBYARNQUALITY.TabIndex = 1
        '
        'TXTLRNO
        '
        Me.TXTLRNO.BackColor = System.Drawing.Color.White
        Me.TXTLRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLRNO.Location = New System.Drawing.Point(833, 4)
        Me.TXTLRNO.Name = "TXTLRNO"
        Me.TXTLRNO.Size = New System.Drawing.Size(80, 23)
        Me.TXTLRNO.TabIndex = 7
        Me.TXTLRNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'gridstock
        '
        Me.gridstock.AllowUserToAddRows = False
        Me.gridstock.AllowUserToDeleteRows = False
        Me.gridstock.AllowUserToResizeColumns = False
        Me.gridstock.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.gridstock.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.gridstock.BackgroundColor = System.Drawing.Color.White
        Me.gridstock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.gridstock.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.gridstock.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.gridstock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridstock.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.gsrno, Me.GNO, Me.GLOTNO, Me.GYARNQUALITY, Me.GMILL, Me.GDESIGN, Me.GSHADE, Me.GPROCESS, Me.GREMARKS, Me.GLRNO, Me.GLRDATE, Me.GBILLNO, Me.gtoname, Me.GGODOWN, Me.GBAGS, Me.GWT, Me.GCONES, Me.GRACK, Me.GGRIDREMARKS, Me.GPER, Me.GRATE, Me.GAMOUNT, Me.GBARCODE, Me.GOUTWT, Me.GOUTBAGS, Me.GDONE})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridstock.DefaultCellStyle = DataGridViewCellStyle5
        Me.gridstock.GridColor = System.Drawing.SystemColors.Control
        Me.gridstock.Location = New System.Drawing.Point(3, 27)
        Me.gridstock.MultiSelect = False
        Me.gridstock.Name = "gridstock"
        Me.gridstock.ReadOnly = True
        Me.gridstock.RowHeadersVisible = False
        Me.gridstock.RowHeadersWidth = 30
        Me.gridstock.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White
        Me.gridstock.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.gridstock.RowTemplate.Height = 20
        Me.gridstock.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridstock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.gridstock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridstock.Size = New System.Drawing.Size(1946, 448)
        Me.gridstock.TabIndex = 14
        Me.gridstock.TabStop = False
        '
        'gsrno
        '
        Me.gsrno.HeaderText = "Sr."
        Me.gsrno.Name = "gsrno"
        Me.gsrno.ReadOnly = True
        Me.gsrno.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gsrno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gsrno.Width = 40
        '
        'GNO
        '
        Me.GNO.HeaderText = "NO"
        Me.GNO.Name = "GNO"
        Me.GNO.ReadOnly = True
        Me.GNO.Visible = False
        '
        'GLOTNO
        '
        Me.GLOTNO.HeaderText = "Lot No."
        Me.GLOTNO.Name = "GLOTNO"
        Me.GLOTNO.ReadOnly = True
        Me.GLOTNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLOTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GLOTNO.Width = 60
        '
        'GYARNQUALITY
        '
        Me.GYARNQUALITY.HeaderText = "Yarn Quality"
        Me.GYARNQUALITY.Name = "GYARNQUALITY"
        Me.GYARNQUALITY.ReadOnly = True
        Me.GYARNQUALITY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GYARNQUALITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GYARNQUALITY.Width = 160
        '
        'GMILL
        '
        Me.GMILL.HeaderText = "Mill Name"
        Me.GMILL.Name = "GMILL"
        Me.GMILL.ReadOnly = True
        Me.GMILL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMILL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMILL.Width = 160
        '
        'GDESIGN
        '
        Me.GDESIGN.HeaderText = "Design"
        Me.GDESIGN.Name = "GDESIGN"
        Me.GDESIGN.ReadOnly = True
        Me.GDESIGN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDESIGN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDESIGN.Width = 80
        '
        'GSHADE
        '
        Me.GSHADE.HeaderText = "Shade"
        Me.GSHADE.Name = "GSHADE"
        Me.GSHADE.ReadOnly = True
        Me.GSHADE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSHADE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSHADE.Width = 80
        '
        'GPROCESS
        '
        Me.GPROCESS.HeaderText = "Process"
        Me.GPROCESS.Name = "GPROCESS"
        Me.GPROCESS.ReadOnly = True
        Me.GPROCESS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPROCESS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GREMARKS
        '
        Me.GREMARKS.HeaderText = "Remarks"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.ReadOnly = True
        Me.GREMARKS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GREMARKS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GREMARKS.Width = 150
        '
        'GLRNO
        '
        Me.GLRNO.HeaderText = "LR No"
        Me.GLRNO.Name = "GLRNO"
        Me.GLRNO.ReadOnly = True
        Me.GLRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GLRNO.Width = 80
        '
        'GLRDATE
        '
        Me.GLRDATE.HeaderText = "LR Date"
        Me.GLRDATE.Name = "GLRDATE"
        Me.GLRDATE.ReadOnly = True
        Me.GLRDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLRDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GLRDATE.Visible = False
        Me.GLRDATE.Width = 80
        '
        'GBILLNO
        '
        Me.GBILLNO.HeaderText = "Bill No"
        Me.GBILLNO.Name = "GBILLNO"
        Me.GBILLNO.ReadOnly = True
        Me.GBILLNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBILLNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBILLNO.Width = 80
        '
        'gtoname
        '
        Me.gtoname.HeaderText = "Jobber Name"
        Me.gtoname.Name = "gtoname"
        Me.gtoname.ReadOnly = True
        Me.gtoname.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gtoname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gtoname.Width = 160
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
        'GBAGS
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GBAGS.DefaultCellStyle = DataGridViewCellStyle3
        Me.GBAGS.HeaderText = "Bags"
        Me.GBAGS.Name = "GBAGS"
        Me.GBAGS.ReadOnly = True
        Me.GBAGS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBAGS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBAGS.Width = 60
        '
        'GWT
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GWT.DefaultCellStyle = DataGridViewCellStyle4
        Me.GWT.HeaderText = "Wt"
        Me.GWT.Name = "GWT"
        Me.GWT.ReadOnly = True
        Me.GWT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GWT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GWT.Width = 60
        '
        'GCONES
        '
        Me.GCONES.HeaderText = "Cones"
        Me.GCONES.Name = "GCONES"
        Me.GCONES.ReadOnly = True
        Me.GCONES.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCONES.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCONES.Width = 60
        '
        'GRACK
        '
        Me.GRACK.HeaderText = "Rack"
        Me.GRACK.Name = "GRACK"
        Me.GRACK.ReadOnly = True
        Me.GRACK.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRACK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GRACK.Width = 80
        '
        'GGRIDREMARKS
        '
        Me.GGRIDREMARKS.HeaderText = "Remarks"
        Me.GGRIDREMARKS.Name = "GGRIDREMARKS"
        Me.GGRIDREMARKS.ReadOnly = True
        Me.GGRIDREMARKS.Width = 120
        '
        'GPER
        '
        Me.GPER.HeaderText = "Per"
        Me.GPER.Name = "GPER"
        Me.GPER.ReadOnly = True
        Me.GPER.Width = 60
        '
        'GRATE
        '
        Me.GRATE.HeaderText = "Rate"
        Me.GRATE.Name = "GRATE"
        Me.GRATE.ReadOnly = True
        Me.GRATE.Width = 80
        '
        'GAMOUNT
        '
        Me.GAMOUNT.HeaderText = "Amount"
        Me.GAMOUNT.Name = "GAMOUNT"
        Me.GAMOUNT.ReadOnly = True
        Me.GAMOUNT.Width = 80
        '
        'GBARCODE
        '
        Me.GBARCODE.HeaderText = "Barcode"
        Me.GBARCODE.Name = "GBARCODE"
        Me.GBARCODE.ReadOnly = True
        Me.GBARCODE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBARCODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBARCODE.Width = 120
        '
        'GOUTWT
        '
        Me.GOUTWT.HeaderText = "Out Wt"
        Me.GOUTWT.Name = "GOUTWT"
        Me.GOUTWT.ReadOnly = True
        Me.GOUTWT.Visible = False
        '
        'GOUTBAGS
        '
        Me.GOUTBAGS.HeaderText = "Out Bags"
        Me.GOUTBAGS.Name = "GOUTBAGS"
        Me.GOUTBAGS.ReadOnly = True
        Me.GOUTBAGS.Visible = False
        '
        'GDONE
        '
        Me.GDONE.HeaderText = "Done"
        Me.GDONE.Name = "GDONE"
        Me.GDONE.ReadOnly = True
        Me.GDONE.Visible = False
        '
        'CMBSHADE
        '
        Me.CMBSHADE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSHADE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSHADE.DropDownWidth = 400
        Me.CMBSHADE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSHADE.FormattingEnabled = True
        Me.CMBSHADE.Location = New System.Drawing.Point(503, 4)
        Me.CMBSHADE.Name = "CMBSHADE"
        Me.CMBSHADE.Size = New System.Drawing.Size(80, 23)
        Me.CMBSHADE.TabIndex = 4
        '
        'TXTLOTNO
        '
        Me.TXTLOTNO.BackColor = System.Drawing.Color.White
        Me.TXTLOTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLOTNO.Location = New System.Drawing.Point(43, 4)
        Me.TXTLOTNO.Name = "TXTLOTNO"
        Me.TXTLOTNO.Size = New System.Drawing.Size(60, 23)
        Me.TXTLOTNO.TabIndex = 0
        Me.TXTLOTNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBDESIGN
        '
        Me.CMBDESIGN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBDESIGN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBDESIGN.DropDownWidth = 400
        Me.CMBDESIGN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBDESIGN.FormattingEnabled = True
        Me.CMBDESIGN.Location = New System.Drawing.Point(423, 4)
        Me.CMBDESIGN.Name = "CMBDESIGN"
        Me.CMBDESIGN.Size = New System.Drawing.Size(80, 23)
        Me.CMBDESIGN.TabIndex = 3
        '
        'CMBPROCESS
        '
        Me.CMBPROCESS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBPROCESS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBPROCESS.DropDownWidth = 400
        Me.CMBPROCESS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBPROCESS.FormattingEnabled = True
        Me.CMBPROCESS.Location = New System.Drawing.Point(583, 4)
        Me.CMBPROCESS.Name = "CMBPROCESS"
        Me.CMBPROCESS.Size = New System.Drawing.Size(100, 23)
        Me.CMBPROCESS.TabIndex = 5
        '
        'CMBMILL
        '
        Me.CMBMILL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBMILL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBMILL.BackColor = System.Drawing.Color.White
        Me.CMBMILL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBMILL.FormattingEnabled = True
        Me.CMBMILL.Location = New System.Drawing.Point(263, 4)
        Me.CMBMILL.Name = "CMBMILL"
        Me.CMBMILL.Size = New System.Drawing.Size(160, 23)
        Me.CMBMILL.TabIndex = 2
        '
        'TXTWT
        '
        Me.TXTWT.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTWT.Location = New System.Drawing.Point(1333, 4)
        Me.TXTWT.Name = "TXTWT"
        Me.TXTWT.Size = New System.Drawing.Size(60, 23)
        Me.TXTWT.TabIndex = 12
        Me.TXTWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBTONAME
        '
        Me.CMBTONAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTONAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTONAME.DropDownWidth = 400
        Me.CMBTONAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTONAME.FormattingEnabled = True
        Me.CMBTONAME.Location = New System.Drawing.Point(993, 4)
        Me.CMBTONAME.Name = "CMBTONAME"
        Me.CMBTONAME.Size = New System.Drawing.Size(160, 23)
        Me.CMBTONAME.TabIndex = 9
        '
        'CMBGODOWN
        '
        Me.CMBGODOWN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGODOWN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGODOWN.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBGODOWN.DropDownWidth = 400
        Me.CMBGODOWN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGODOWN.FormattingEnabled = True
        Me.CMBGODOWN.Location = New System.Drawing.Point(1153, 4)
        Me.CMBGODOWN.Name = "CMBGODOWN"
        Me.CMBGODOWN.Size = New System.Drawing.Size(120, 23)
        Me.CMBGODOWN.TabIndex = 10
        '
        'TXTFROM
        '
        Me.TXTFROM.BackColor = System.Drawing.Color.White
        Me.TXTFROM.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFROM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTFROM.Location = New System.Drawing.Point(929, 16)
        Me.TXTFROM.Name = "TXTFROM"
        Me.TXTFROM.Size = New System.Drawing.Size(41, 23)
        Me.TXTFROM.TabIndex = 721
        Me.TXTFROM.Text = " "
        Me.TXTFROM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LRDATE
        '
        Me.LRDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LRDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.LRDATE.Location = New System.Drawing.Point(715, 16)
        Me.LRDATE.Name = "LRDATE"
        Me.LRDATE.Size = New System.Drawing.Size(80, 23)
        Me.LRDATE.TabIndex = 6
        Me.LRDATE.Value = New Date(2013, 4, 1, 8, 54, 0, 0)
        Me.LRDATE.Visible = False
        '
        'TXTNO
        '
        Me.TXTNO.BackColor = System.Drawing.Color.White
        Me.TXTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTNO.Location = New System.Drawing.Point(289, 16)
        Me.TXTNO.Name = "TXTNO"
        Me.TXTNO.ReadOnly = True
        Me.TXTNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTNO.TabIndex = 715
        Me.TXTNO.Text = " "
        Me.TXTNO.Visible = False
        '
        'cmbdyeing
        '
        Me.cmbdyeing.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbdyeing.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbdyeing.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbdyeing.FormattingEnabled = True
        Me.cmbdyeing.Location = New System.Drawing.Point(530, 16)
        Me.cmbdyeing.Name = "cmbdyeing"
        Me.cmbdyeing.Size = New System.Drawing.Size(80, 23)
        Me.cmbdyeing.TabIndex = 12
        Me.cmbdyeing.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(12, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 15)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Date :"
        Me.Label6.Visible = False
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.DropDownWidth = 400
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Location = New System.Drawing.Point(436, 16)
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(40, 23)
        Me.CMBCODE.TabIndex = 20
        Me.CMBCODE.Visible = False
        '
        'txtadd
        '
        Me.txtadd.BackColor = System.Drawing.Color.White
        Me.txtadd.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtadd.Location = New System.Drawing.Point(636, 16)
        Me.txtadd.Name = "txtadd"
        Me.txtadd.ReadOnly = True
        Me.txtadd.Size = New System.Drawing.Size(30, 23)
        Me.txtadd.TabIndex = 15
        Me.txtadd.Text = " "
        Me.txtadd.Visible = False
        '
        'cmbtype
        '
        Me.cmbtype.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbtype.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtype.DropDownWidth = 400
        Me.cmbtype.Enabled = False
        Me.cmbtype.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtype.FormattingEnabled = True
        Me.cmbtype.Items.AddRange(New Object() {"GODOWNSTOCKYARN", "JOBBERSTOCKYARN"})
        Me.cmbtype.Location = New System.Drawing.Point(150, 16)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(99, 23)
        Me.cmbtype.TabIndex = 0
        '
        'openingdate
        '
        Me.openingdate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.openingdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.openingdate.Location = New System.Drawing.Point(55, 16)
        Me.openingdate.Name = "openingdate"
        Me.openingdate.Size = New System.Drawing.Size(88, 23)
        Me.openingdate.TabIndex = 0
        Me.openingdate.Value = New Date(2013, 4, 1, 8, 54, 0, 0)
        Me.openingdate.Visible = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(597, 537)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 15
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(1533, 5)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(120, 22)
        Me.TextBox1.TabIndex = 36
        '
        'OpeningStockYarn
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1274, 571)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "OpeningStockYarn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Opening Stock Yarn"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.gridstock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents TXTNO As System.Windows.Forms.TextBox
    Friend WithEvents cmbdyeing As System.Windows.Forms.ComboBox
    Friend WithEvents CMBPROCESS As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CMBCODE As System.Windows.Forms.ComboBox
    Friend WithEvents CMBMILL As System.Windows.Forms.ComboBox
    Friend WithEvents CMBTONAME As System.Windows.Forms.ComboBox
    Friend WithEvents CMBGODOWN As System.Windows.Forms.ComboBox
    Friend WithEvents TXTWT As System.Windows.Forms.TextBox
    Friend WithEvents TXTLOTNO As System.Windows.Forms.TextBox
    Friend WithEvents gridstock As System.Windows.Forms.DataGridView
    Friend WithEvents CMBYARNQUALITY As System.Windows.Forms.ComboBox
    Friend WithEvents TXTBAGS As System.Windows.Forms.TextBox
    Friend WithEvents txtsrno As System.Windows.Forms.TextBox
    Friend WithEvents txtadd As System.Windows.Forms.TextBox
    Friend WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents openingdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents CMBSHADE As System.Windows.Forms.ComboBox
    Friend WithEvents CMBDESIGN As System.Windows.Forms.ComboBox
    Friend WithEvents LRDATE As System.Windows.Forms.DateTimePicker
    Friend WithEvents TXTLRNO As System.Windows.Forms.TextBox
    Friend WithEvents TXTCONES As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TXTBARCODE As TextBox
    Friend WithEvents TXTTO As TextBox
    Friend WithEvents LBLTO As Label
    Friend WithEvents LBLFROM As Label
    Friend WithEvents TXTFROM As TextBox
    Friend WithEvents CMBRACK As ComboBox
    Friend WithEvents TXTBILLNO As TextBox
    Friend WithEvents TXTREMARKS As TextBox
    Friend WithEvents gsrno As DataGridViewTextBoxColumn
    Friend WithEvents GNO As DataGridViewTextBoxColumn
    Friend WithEvents GLOTNO As DataGridViewTextBoxColumn
    Friend WithEvents GYARNQUALITY As DataGridViewTextBoxColumn
    Friend WithEvents GMILL As DataGridViewTextBoxColumn
    Friend WithEvents GDESIGN As DataGridViewTextBoxColumn
    Friend WithEvents GSHADE As DataGridViewTextBoxColumn
    Friend WithEvents GPROCESS As DataGridViewTextBoxColumn
    Friend WithEvents GREMARKS As DataGridViewTextBoxColumn
    Friend WithEvents GLRNO As DataGridViewTextBoxColumn
    Friend WithEvents GLRDATE As DataGridViewTextBoxColumn
    Friend WithEvents GBILLNO As DataGridViewTextBoxColumn
    Friend WithEvents gtoname As DataGridViewTextBoxColumn
    Friend WithEvents GGODOWN As DataGridViewTextBoxColumn
    Friend WithEvents GBAGS As DataGridViewTextBoxColumn
    Friend WithEvents GWT As DataGridViewTextBoxColumn
    Friend WithEvents GCONES As DataGridViewTextBoxColumn
    Friend WithEvents GRACK As DataGridViewTextBoxColumn
    Friend WithEvents GGRIDREMARKS As DataGridViewTextBoxColumn
    Friend WithEvents GPER As DataGridViewTextBoxColumn
    Friend WithEvents GRATE As DataGridViewTextBoxColumn
    Friend WithEvents GAMOUNT As DataGridViewTextBoxColumn
    Friend WithEvents GBARCODE As DataGridViewTextBoxColumn
    Friend WithEvents GOUTWT As DataGridViewTextBoxColumn
    Friend WithEvents GOUTBAGS As DataGridViewTextBoxColumn
    Friend WithEvents GDONE As DataGridViewTextBoxColumn
    Friend WithEvents TXTAMOUNT As TextBox
    Friend WithEvents TXTRATE As TextBox
    Friend WithEvents CMBPER As ComboBox
    Friend WithEvents TXTGRIDREMARKS As TextBox
    Friend WithEvents TextBox1 As TextBox
End Class
