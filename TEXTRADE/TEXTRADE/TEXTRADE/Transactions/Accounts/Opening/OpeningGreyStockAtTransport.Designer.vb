<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OpeningGreyStockAtTransport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OpeningGreyStockAtTransport))
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
        Me.TXTYARDS = New System.Windows.Forms.TextBox()
        Me.TXTSEARCHBARCODE = New System.Windows.Forms.TextBox()
        Me.LBLBARCODE = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TXTTO = New System.Windows.Forms.TextBox()
        Me.LBLTO = New System.Windows.Forms.Label()
        Me.LBLFROM = New System.Windows.Forms.Label()
        Me.TXTFROM = New System.Windows.Forms.TextBox()
        Me.CHKPRINT = New System.Windows.Forms.CheckBox()
        Me.TXTNO = New System.Windows.Forms.TextBox()
        Me.cmbdyeing = New System.Windows.Forms.ComboBox()
        Me.CMBPROCESS = New System.Windows.Forms.ComboBox()
        Me.TXTBARCODE = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TXTLRNO = New System.Windows.Forms.TextBox()
        Me.TXTBALENO = New System.Windows.Forms.TextBox()
        Me.CMBPER = New System.Windows.Forms.ComboBox()
        Me.TXTAMOUNT = New System.Windows.Forms.TextBox()
        Me.TXTRATE = New System.Windows.Forms.TextBox()
        Me.CMBDESIGN = New System.Windows.Forms.ComboBox()
        Me.gridstock = New System.Windows.Forms.DataGridView()
        Me.txtpcs = New System.Windows.Forms.TextBox()
        Me.cmbname = New System.Windows.Forms.ComboBox()
        Me.cmbcolor = New System.Windows.Forms.ComboBox()
        Me.txtMtrs = New System.Windows.Forms.TextBox()
        Me.cmbunit = New System.Windows.Forms.ComboBox()
        Me.txtsrno = New System.Windows.Forms.TextBox()
        Me.cmbmerchant = New System.Windows.Forms.ComboBox()
        Me.txtadd = New System.Windows.Forms.TextBox()
        Me.cmbtype = New System.Windows.Forms.ComboBox()
        Me.openingdate = New System.Windows.Forms.DateTimePicker()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.CMBTRANS = New System.Windows.Forms.ComboBox()
        Me.GDNDATE = New System.Windows.Forms.MaskedTextBox()
        Me.CMBAGENT = New System.Windows.Forms.ComboBox()
        Me.TXTCRDAYS = New System.Windows.Forms.TextBox()
        Me.gsrno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTRANS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLRDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMERCHANT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDESIGN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gcolor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBALENO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Gpcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Gunit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gMtrs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAMOUNT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAGENT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCRDAYS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gBarcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gdone = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.gOutpcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.goutmtrs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPROGRAMDONE = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.BlendPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.gridstock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTYARDS)
        Me.BlendPanel1.Controls.Add(Me.TXTSEARCHBARCODE)
        Me.BlendPanel1.Controls.Add(Me.LBLBARCODE)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.TXTTO)
        Me.BlendPanel1.Controls.Add(Me.LBLTO)
        Me.BlendPanel1.Controls.Add(Me.LBLFROM)
        Me.BlendPanel1.Controls.Add(Me.TXTFROM)
        Me.BlendPanel1.Controls.Add(Me.CHKPRINT)
        Me.BlendPanel1.Controls.Add(Me.TXTNO)
        Me.BlendPanel1.Controls.Add(Me.cmbdyeing)
        Me.BlendPanel1.Controls.Add(Me.CMBPROCESS)
        Me.BlendPanel1.Controls.Add(Me.TXTBARCODE)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.CMBCODE)
        Me.BlendPanel1.Controls.Add(Me.Panel1)
        Me.BlendPanel1.Controls.Add(Me.txtadd)
        Me.BlendPanel1.Controls.Add(Me.cmbtype)
        Me.BlendPanel1.Controls.Add(Me.openingdate)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 599)
        Me.BlendPanel1.TabIndex = 1
        '
        'TXTYARDS
        '
        Me.TXTYARDS.BackColor = System.Drawing.Color.White
        Me.TXTYARDS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTYARDS.Location = New System.Drawing.Point(1162, 16)
        Me.TXTYARDS.Name = "TXTYARDS"
        Me.TXTYARDS.Size = New System.Drawing.Size(60, 22)
        Me.TXTYARDS.TabIndex = 0
        Me.TXTYARDS.TabStop = False
        Me.TXTYARDS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTYARDS.Visible = False
        '
        'TXTSEARCHBARCODE
        '
        Me.TXTSEARCHBARCODE.BackColor = System.Drawing.Color.White
        Me.TXTSEARCHBARCODE.ForeColor = System.Drawing.Color.DimGray
        Me.TXTSEARCHBARCODE.Location = New System.Drawing.Point(930, 15)
        Me.TXTSEARCHBARCODE.Name = "TXTSEARCHBARCODE"
        Me.TXTSEARCHBARCODE.Size = New System.Drawing.Size(122, 22)
        Me.TXTSEARCHBARCODE.TabIndex = 759
        '
        'LBLBARCODE
        '
        Me.LBLBARCODE.AutoSize = True
        Me.LBLBARCODE.BackColor = System.Drawing.Color.Transparent
        Me.LBLBARCODE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLBARCODE.ForeColor = System.Drawing.Color.Black
        Me.LBLBARCODE.Location = New System.Drawing.Point(837, 20)
        Me.LBLBARCODE.Name = "LBLBARCODE"
        Me.LBLBARCODE.Size = New System.Drawing.Size(92, 15)
        Me.LBLBARCODE.TabIndex = 722
        Me.LBLBARCODE.Text = "Search Barcode"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(12, 530)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(696, 60)
        Me.Label1.TabIndex = 721
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'TXTTO
        '
        Me.TXTTO.BackColor = System.Drawing.Color.White
        Me.TXTTO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTTO.Location = New System.Drawing.Point(683, 16)
        Me.TXTTO.Name = "TXTTO"
        Me.TXTTO.Size = New System.Drawing.Size(41, 23)
        Me.TXTTO.TabIndex = 720
        Me.TXTTO.Text = " "
        Me.TXTTO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLTO
        '
        Me.LBLTO.AutoSize = True
        Me.LBLTO.BackColor = System.Drawing.Color.Transparent
        Me.LBLTO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTO.ForeColor = System.Drawing.Color.Black
        Me.LBLTO.Location = New System.Drawing.Point(663, 20)
        Me.LBLTO.Name = "LBLTO"
        Me.LBLTO.Size = New System.Drawing.Size(19, 15)
        Me.LBLTO.TabIndex = 719
        Me.LBLTO.Text = "To"
        '
        'LBLFROM
        '
        Me.LBLFROM.AutoSize = True
        Me.LBLFROM.BackColor = System.Drawing.Color.Transparent
        Me.LBLFROM.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLFROM.ForeColor = System.Drawing.Color.Black
        Me.LBLFROM.Location = New System.Drawing.Point(507, 20)
        Me.LBLFROM.Name = "LBLFROM"
        Me.LBLFROM.Size = New System.Drawing.Size(113, 15)
        Me.LBLFROM.TabIndex = 718
        Me.LBLFROM.Text = "Print Barcode From"
        '
        'TXTFROM
        '
        Me.TXTFROM.BackColor = System.Drawing.Color.White
        Me.TXTFROM.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFROM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTFROM.Location = New System.Drawing.Point(619, 16)
        Me.TXTFROM.Name = "TXTFROM"
        Me.TXTFROM.Size = New System.Drawing.Size(41, 23)
        Me.TXTFROM.TabIndex = 717
        Me.TXTFROM.Text = " "
        Me.TXTFROM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CHKPRINT
        '
        Me.CHKPRINT.AutoSize = True
        Me.CHKPRINT.BackColor = System.Drawing.Color.Transparent
        Me.CHKPRINT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKPRINT.Location = New System.Drawing.Point(270, 18)
        Me.CHKPRINT.Name = "CHKPRINT"
        Me.CHKPRINT.Size = New System.Drawing.Size(134, 19)
        Me.CHKPRINT.TabIndex = 716
        Me.CHKPRINT.Text = "Print Each Bar Code"
        Me.CHKPRINT.UseVisualStyleBackColor = False
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
        Me.cmbdyeing.FormattingEnabled = True
        Me.cmbdyeing.Location = New System.Drawing.Point(739, 17)
        Me.cmbdyeing.Name = "cmbdyeing"
        Me.cmbdyeing.Size = New System.Drawing.Size(80, 22)
        Me.cmbdyeing.TabIndex = 12
        Me.cmbdyeing.Visible = False
        '
        'CMBPROCESS
        '
        Me.CMBPROCESS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBPROCESS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBPROCESS.DropDownWidth = 400
        Me.CMBPROCESS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBPROCESS.FormattingEnabled = True
        Me.CMBPROCESS.Location = New System.Drawing.Point(840, 13)
        Me.CMBPROCESS.Name = "CMBPROCESS"
        Me.CMBPROCESS.Size = New System.Drawing.Size(40, 22)
        Me.CMBPROCESS.TabIndex = 714
        Me.CMBPROCESS.Visible = False
        '
        'TXTBARCODE
        '
        Me.TXTBARCODE.BackColor = System.Drawing.Color.White
        Me.TXTBARCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBARCODE.Location = New System.Drawing.Point(881, 16)
        Me.TXTBARCODE.Name = "TXTBARCODE"
        Me.TXTBARCODE.Size = New System.Drawing.Size(111, 22)
        Me.TXTBARCODE.TabIndex = 713
        Me.TXTBARCODE.Visible = False
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
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.Linen
        Me.Panel1.Controls.Add(Me.TXTCRDAYS)
        Me.Panel1.Controls.Add(Me.CMBAGENT)
        Me.Panel1.Controls.Add(Me.GDNDATE)
        Me.Panel1.Controls.Add(Me.CMBTRANS)
        Me.Panel1.Controls.Add(Me.TXTLRNO)
        Me.Panel1.Controls.Add(Me.TXTBALENO)
        Me.Panel1.Controls.Add(Me.CMBPER)
        Me.Panel1.Controls.Add(Me.TXTAMOUNT)
        Me.Panel1.Controls.Add(Me.TXTRATE)
        Me.Panel1.Controls.Add(Me.CMBDESIGN)
        Me.Panel1.Controls.Add(Me.gridstock)
        Me.Panel1.Controls.Add(Me.txtpcs)
        Me.Panel1.Controls.Add(Me.cmbname)
        Me.Panel1.Controls.Add(Me.cmbcolor)
        Me.Panel1.Controls.Add(Me.txtMtrs)
        Me.Panel1.Controls.Add(Me.cmbunit)
        Me.Panel1.Controls.Add(Me.txtsrno)
        Me.Panel1.Controls.Add(Me.cmbmerchant)
        Me.Panel1.Location = New System.Drawing.Point(13, 41)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1209, 482)
        Me.Panel1.TabIndex = 0
        '
        'TXTLRNO
        '
        Me.TXTLRNO.BackColor = System.Drawing.Color.White
        Me.TXTLRNO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLRNO.Location = New System.Drawing.Point(443, 0)
        Me.TXTLRNO.Name = "TXTLRNO"
        Me.TXTLRNO.Size = New System.Drawing.Size(100, 22)
        Me.TXTLRNO.TabIndex = 29
        '
        'TXTBALENO
        '
        Me.TXTBALENO.BackColor = System.Drawing.Color.White
        Me.TXTBALENO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBALENO.Location = New System.Drawing.Point(958, 0)
        Me.TXTBALENO.Name = "TXTBALENO"
        Me.TXTBALENO.Size = New System.Drawing.Size(100, 22)
        Me.TXTBALENO.TabIndex = 24
        '
        'CMBPER
        '
        Me.CMBPER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBPER.FormattingEnabled = True
        Me.CMBPER.Items.AddRange(New Object() {"Mtrs", "Qty"})
        Me.CMBPER.Location = New System.Drawing.Point(1338, 0)
        Me.CMBPER.Name = "CMBPER"
        Me.CMBPER.Size = New System.Drawing.Size(60, 22)
        Me.CMBPER.TabIndex = 16
        '
        'TXTAMOUNT
        '
        Me.TXTAMOUNT.BackColor = System.Drawing.Color.Linen
        Me.TXTAMOUNT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTAMOUNT.Location = New System.Drawing.Point(1398, 0)
        Me.TXTAMOUNT.Name = "TXTAMOUNT"
        Me.TXTAMOUNT.ReadOnly = True
        Me.TXTAMOUNT.Size = New System.Drawing.Size(80, 22)
        Me.TXTAMOUNT.TabIndex = 20
        Me.TXTAMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTRATE
        '
        Me.TXTRATE.BackColor = System.Drawing.Color.White
        Me.TXTRATE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTRATE.Location = New System.Drawing.Point(1258, 0)
        Me.TXTRATE.Name = "TXTRATE"
        Me.TXTRATE.Size = New System.Drawing.Size(80, 22)
        Me.TXTRATE.TabIndex = 19
        Me.TXTRATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBDESIGN
        '
        Me.CMBDESIGN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBDESIGN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBDESIGN.BackColor = System.Drawing.Color.White
        Me.CMBDESIGN.FormattingEnabled = True
        Me.CMBDESIGN.Location = New System.Drawing.Point(797, 0)
        Me.CMBDESIGN.Name = "CMBDESIGN"
        Me.CMBDESIGN.Size = New System.Drawing.Size(81, 22)
        Me.CMBDESIGN.TabIndex = 5
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
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.gridstock.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.gridstock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridstock.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.gsrno, Me.GNO, Me.GNAME, Me.GTRANS, Me.GLRNO, Me.GLRDATE, Me.GMERCHANT, Me.GDESIGN, Me.gcolor, Me.GBALENO, Me.Gpcs, Me.Gunit, Me.gMtrs, Me.GRATE, Me.GPer, Me.GAMOUNT, Me.GAGENT, Me.GCRDAYS, Me.gBarcode, Me.gdone, Me.gOutpcs, Me.goutmtrs, Me.GPROGRAMDONE})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridstock.DefaultCellStyle = DataGridViewCellStyle8
        Me.gridstock.GridColor = System.Drawing.SystemColors.Control
        Me.gridstock.Location = New System.Drawing.Point(3, 22)
        Me.gridstock.MultiSelect = False
        Me.gridstock.Name = "gridstock"
        Me.gridstock.ReadOnly = True
        Me.gridstock.RowHeadersVisible = False
        Me.gridstock.RowHeadersWidth = 30
        Me.gridstock.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White
        Me.gridstock.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.gridstock.RowTemplate.Height = 20
        Me.gridstock.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridstock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.gridstock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridstock.Size = New System.Drawing.Size(1926, 439)
        Me.gridstock.TabIndex = 28
        Me.gridstock.TabStop = False
        '
        'txtpcs
        '
        Me.txtpcs.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtpcs.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpcs.Location = New System.Drawing.Point(1058, 0)
        Me.txtpcs.Name = "txtpcs"
        Me.txtpcs.Size = New System.Drawing.Size(60, 22)
        Me.txtpcs.TabIndex = 14
        Me.txtpcs.Text = "1"
        Me.txtpcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbname
        '
        Me.cmbname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbname.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbname.DropDownWidth = 400
        Me.cmbname.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbname.FormattingEnabled = True
        Me.cmbname.Location = New System.Drawing.Point(43, 0)
        Me.cmbname.Name = "cmbname"
        Me.cmbname.Size = New System.Drawing.Size(200, 22)
        Me.cmbname.TabIndex = 7
        '
        'cmbcolor
        '
        Me.cmbcolor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcolor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbcolor.FormattingEnabled = True
        Me.cmbcolor.Location = New System.Drawing.Point(878, 0)
        Me.cmbcolor.Name = "cmbcolor"
        Me.cmbcolor.Size = New System.Drawing.Size(80, 22)
        Me.cmbcolor.TabIndex = 6
        '
        'txtMtrs
        '
        Me.txtMtrs.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtMtrs.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMtrs.Location = New System.Drawing.Point(1178, 0)
        Me.txtMtrs.Name = "txtMtrs"
        Me.txtMtrs.Size = New System.Drawing.Size(80, 22)
        Me.txtMtrs.TabIndex = 15
        Me.txtMtrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbunit
        '
        Me.cmbunit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbunit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbunit.FormattingEnabled = True
        Me.cmbunit.Location = New System.Drawing.Point(1118, 0)
        Me.cmbunit.Name = "cmbunit"
        Me.cmbunit.Size = New System.Drawing.Size(60, 22)
        Me.cmbunit.TabIndex = 13
        '
        'txtsrno
        '
        Me.txtsrno.BackColor = System.Drawing.Color.Linen
        Me.txtsrno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrno.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtsrno.Location = New System.Drawing.Point(3, 0)
        Me.txtsrno.Name = "txtsrno"
        Me.txtsrno.ReadOnly = True
        Me.txtsrno.Size = New System.Drawing.Size(40, 22)
        Me.txtsrno.TabIndex = 0
        Me.txtsrno.TabStop = False
        Me.txtsrno.Text = " "
        '
        'cmbmerchant
        '
        Me.cmbmerchant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbmerchant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbmerchant.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbmerchant.DropDownWidth = 400
        Me.cmbmerchant.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbmerchant.FormattingEnabled = True
        Me.cmbmerchant.Location = New System.Drawing.Point(627, 0)
        Me.cmbmerchant.Name = "cmbmerchant"
        Me.cmbmerchant.Size = New System.Drawing.Size(170, 22)
        Me.cmbmerchant.TabIndex = 3
        '
        'txtadd
        '
        Me.txtadd.BackColor = System.Drawing.Color.White
        Me.txtadd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtadd.Location = New System.Drawing.Point(845, 13)
        Me.txtadd.Name = "txtadd"
        Me.txtadd.ReadOnly = True
        Me.txtadd.Size = New System.Drawing.Size(30, 22)
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
        Me.cmbtype.Items.AddRange(New Object() {"INHOUSE", "JOBBERSTOCK"})
        Me.cmbtype.Location = New System.Drawing.Point(150, 16)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(99, 23)
        Me.cmbtype.TabIndex = 1
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
        Me.cmdexit.Location = New System.Drawing.Point(1035, 530)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 1
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'CMBTRANS
        '
        Me.CMBTRANS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTRANS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTRANS.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBTRANS.DropDownWidth = 400
        Me.CMBTRANS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTRANS.FormattingEnabled = True
        Me.CMBTRANS.Location = New System.Drawing.Point(243, 0)
        Me.CMBTRANS.Name = "CMBTRANS"
        Me.CMBTRANS.Size = New System.Drawing.Size(200, 22)
        Me.CMBTRANS.TabIndex = 30
        '
        'GDNDATE
        '
        Me.GDNDATE.AsciiOnly = True
        Me.GDNDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.GDNDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.GDNDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.GDNDATE.Location = New System.Drawing.Point(543, 0)
        Me.GDNDATE.Mask = "00/00/0000"
        Me.GDNDATE.Name = "GDNDATE"
        Me.GDNDATE.Size = New System.Drawing.Size(84, 23)
        Me.GDNDATE.TabIndex = 31
        Me.GDNDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.GDNDATE.ValidatingType = GetType(Date)
        '
        'CMBAGENT
        '
        Me.CMBAGENT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBAGENT.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBAGENT.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBAGENT.DropDownWidth = 400
        Me.CMBAGENT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBAGENT.FormattingEnabled = True
        Me.CMBAGENT.Location = New System.Drawing.Point(1478, 0)
        Me.CMBAGENT.Name = "CMBAGENT"
        Me.CMBAGENT.Size = New System.Drawing.Size(200, 22)
        Me.CMBAGENT.TabIndex = 32
        '
        'TXTCRDAYS
        '
        Me.TXTCRDAYS.BackColor = System.Drawing.Color.White
        Me.TXTCRDAYS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCRDAYS.Location = New System.Drawing.Point(1678, 0)
        Me.TXTCRDAYS.Name = "TXTCRDAYS"
        Me.TXTCRDAYS.Size = New System.Drawing.Size(100, 22)
        Me.TXTCRDAYS.TabIndex = 33
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
        'GNAME
        '
        Me.GNAME.HeaderText = "Party Name"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.ReadOnly = True
        Me.GNAME.Width = 200
        '
        'GTRANS
        '
        Me.GTRANS.HeaderText = "Transport"
        Me.GTRANS.Name = "GTRANS"
        Me.GTRANS.ReadOnly = True
        Me.GTRANS.Width = 200
        '
        'GLRNO
        '
        Me.GLRNO.HeaderText = "LR No"
        Me.GLRNO.Name = "GLRNO"
        Me.GLRNO.ReadOnly = True
        '
        'GLRDATE
        '
        Me.GLRDATE.HeaderText = "LR Date"
        Me.GLRDATE.Name = "GLRDATE"
        Me.GLRDATE.ReadOnly = True
        Me.GLRDATE.Width = 84
        '
        'GMERCHANT
        '
        Me.GMERCHANT.HeaderText = "Item Name"
        Me.GMERCHANT.Name = "GMERCHANT"
        Me.GMERCHANT.ReadOnly = True
        Me.GMERCHANT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMERCHANT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMERCHANT.Width = 170
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
        'gcolor
        '
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.NullValue = Nothing
        Me.gcolor.DefaultCellStyle = DataGridViewCellStyle3
        Me.gcolor.HeaderText = "Shade"
        Me.gcolor.Name = "gcolor"
        Me.gcolor.ReadOnly = True
        Me.gcolor.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gcolor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gcolor.Width = 80
        '
        'GBALENO
        '
        Me.GBALENO.HeaderText = "Bale No"
        Me.GBALENO.Name = "GBALENO"
        Me.GBALENO.ReadOnly = True
        Me.GBALENO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBALENO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Gpcs
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 8.25!)
        Me.Gpcs.DefaultCellStyle = DataGridViewCellStyle4
        Me.Gpcs.HeaderText = "Pcs"
        Me.Gpcs.Name = "Gpcs"
        Me.Gpcs.ReadOnly = True
        Me.Gpcs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Gpcs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Gpcs.Width = 60
        '
        'Gunit
        '
        Me.Gunit.HeaderText = "Unit"
        Me.Gunit.Name = "Gunit"
        Me.Gunit.ReadOnly = True
        Me.Gunit.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Gunit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Gunit.Width = 60
        '
        'gMtrs
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.gMtrs.DefaultCellStyle = DataGridViewCellStyle5
        Me.gMtrs.HeaderText = "Mtrs"
        Me.gMtrs.Name = "gMtrs"
        Me.gMtrs.ReadOnly = True
        Me.gMtrs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gMtrs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gMtrs.Width = 80
        '
        'GRATE
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GRATE.DefaultCellStyle = DataGridViewCellStyle6
        Me.GRATE.HeaderText = "Rate"
        Me.GRATE.Name = "GRATE"
        Me.GRATE.ReadOnly = True
        Me.GRATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GRATE.Width = 80
        '
        'GPer
        '
        Me.GPer.HeaderText = "Per"
        Me.GPer.Name = "GPer"
        Me.GPer.ReadOnly = True
        Me.GPer.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GPer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPer.Width = 60
        '
        'GAMOUNT
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GAMOUNT.DefaultCellStyle = DataGridViewCellStyle7
        Me.GAMOUNT.HeaderText = "Amount"
        Me.GAMOUNT.Name = "GAMOUNT"
        Me.GAMOUNT.ReadOnly = True
        Me.GAMOUNT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GAMOUNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GAMOUNT.Width = 80
        '
        'GAGENT
        '
        Me.GAGENT.HeaderText = "Agent"
        Me.GAGENT.Name = "GAGENT"
        Me.GAGENT.ReadOnly = True
        Me.GAGENT.Width = 200
        '
        'GCRDAYS
        '
        Me.GCRDAYS.HeaderText = "Cr Days"
        Me.GCRDAYS.Name = "GCRDAYS"
        Me.GCRDAYS.ReadOnly = True
        '
        'gBarcode
        '
        Me.gBarcode.HeaderText = "Barcode"
        Me.gBarcode.Name = "gBarcode"
        Me.gBarcode.ReadOnly = True
        Me.gBarcode.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gBarcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gBarcode.Width = 135
        '
        'gdone
        '
        Me.gdone.HeaderText = "done"
        Me.gdone.Name = "gdone"
        Me.gdone.ReadOnly = True
        Me.gdone.Visible = False
        '
        'gOutpcs
        '
        Me.gOutpcs.HeaderText = "Outpcs"
        Me.gOutpcs.Name = "gOutpcs"
        Me.gOutpcs.ReadOnly = True
        Me.gOutpcs.Visible = False
        '
        'goutmtrs
        '
        Me.goutmtrs.HeaderText = "Out Mtrs"
        Me.goutmtrs.Name = "goutmtrs"
        Me.goutmtrs.ReadOnly = True
        Me.goutmtrs.Visible = False
        '
        'GPROGRAMDONE
        '
        Me.GPROGRAMDONE.HeaderText = "Program Done"
        Me.GPROGRAMDONE.Name = "GPROGRAMDONE"
        Me.GPROGRAMDONE.ReadOnly = True
        Me.GPROGRAMDONE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GPROGRAMDONE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.GPROGRAMDONE.Visible = False
        '
        'OpeningGreyStockAtTransport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 599)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "OpeningGreyStockAtTransport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "OpeningGreyStockAtTransport"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.gridstock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents TXTYARDS As TextBox
    Friend WithEvents TXTSEARCHBARCODE As TextBox
    Friend WithEvents LBLBARCODE As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TXTTO As TextBox
    Friend WithEvents LBLTO As Label
    Friend WithEvents LBLFROM As Label
    Friend WithEvents TXTFROM As TextBox
    Friend WithEvents CHKPRINT As CheckBox
    Friend WithEvents TXTNO As TextBox
    Friend WithEvents cmbdyeing As ComboBox
    Friend WithEvents CMBPROCESS As ComboBox
    Friend WithEvents TXTBARCODE As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TXTLRNO As TextBox
    Friend WithEvents TXTBALENO As TextBox
    Friend WithEvents CMBPER As ComboBox
    Friend WithEvents TXTAMOUNT As TextBox
    Friend WithEvents TXTRATE As TextBox
    Friend WithEvents CMBDESIGN As ComboBox
    Friend WithEvents gridstock As DataGridView
    Friend WithEvents txtpcs As TextBox
    Friend WithEvents cmbname As ComboBox
    Friend WithEvents cmbcolor As ComboBox
    Friend WithEvents txtMtrs As TextBox
    Friend WithEvents cmbunit As ComboBox
    Friend WithEvents txtsrno As TextBox
    Friend WithEvents cmbmerchant As ComboBox
    Friend WithEvents txtadd As TextBox
    Friend WithEvents cmbtype As ComboBox
    Friend WithEvents openingdate As DateTimePicker
    Friend WithEvents cmdexit As Button
    Friend WithEvents CMBTRANS As ComboBox
    Friend WithEvents GDNDATE As MaskedTextBox
    Friend WithEvents CMBAGENT As ComboBox
    Friend WithEvents TXTCRDAYS As TextBox
    Friend WithEvents gsrno As DataGridViewTextBoxColumn
    Friend WithEvents GNO As DataGridViewTextBoxColumn
    Friend WithEvents GNAME As DataGridViewTextBoxColumn
    Friend WithEvents GTRANS As DataGridViewTextBoxColumn
    Friend WithEvents GLRNO As DataGridViewTextBoxColumn
    Friend WithEvents GLRDATE As DataGridViewTextBoxColumn
    Friend WithEvents GMERCHANT As DataGridViewTextBoxColumn
    Friend WithEvents GDESIGN As DataGridViewTextBoxColumn
    Friend WithEvents gcolor As DataGridViewTextBoxColumn
    Friend WithEvents GBALENO As DataGridViewTextBoxColumn
    Friend WithEvents Gpcs As DataGridViewTextBoxColumn
    Friend WithEvents Gunit As DataGridViewTextBoxColumn
    Friend WithEvents gMtrs As DataGridViewTextBoxColumn
    Friend WithEvents GRATE As DataGridViewTextBoxColumn
    Friend WithEvents GPer As DataGridViewTextBoxColumn
    Friend WithEvents GAMOUNT As DataGridViewTextBoxColumn
    Friend WithEvents GAGENT As DataGridViewTextBoxColumn
    Friend WithEvents GCRDAYS As DataGridViewTextBoxColumn
    Friend WithEvents gBarcode As DataGridViewTextBoxColumn
    Friend WithEvents gdone As DataGridViewCheckBoxColumn
    Friend WithEvents gOutpcs As DataGridViewTextBoxColumn
    Friend WithEvents goutmtrs As DataGridViewTextBoxColumn
    Friend WithEvents GPROGRAMDONE As DataGridViewCheckBoxColumn
End Class
