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
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.GRIDMAGICBOX = New System.Windows.Forms.DataGridView()
        Me.gsrno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBUYERS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSELLERS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gorderon = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCRDAYS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDISCOUNT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDELPERIOD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDUEDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GORDERNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gitemname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDESCRIPTION = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gqtyunit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gcut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMTRS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.TXTRATE = New System.Windows.Forms.TextBox()
        Me.TXTCUT = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TXTMTRS = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.TXTDESCRIPTION = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TXTORDERNO = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbqtyunit = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtQTY = New System.Windows.Forms.TextBox()
        Me.cmbitemname = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.duedate = New System.Windows.Forms.DateTimePicker()
        Me.CMBORDERON = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TXTDISCOUNT = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TXTDELPERIOD = New System.Windows.Forms.TextBox()
        Me.ORDERDATE = New System.Windows.Forms.MaskedTextBox()
        Me.CMBSELLERS = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TXTCRDAYS = New System.Windows.Forms.TextBox()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.TXTNO = New System.Windows.Forms.TextBox()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.cmdEXIT = New System.Windows.Forms.Button()
        Me.txtsrno = New System.Windows.Forms.TextBox()
        Me.CMBBUYERS = New System.Windows.Forms.ComboBox()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDMAGICBOX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.GRIDMAGICBOX)
        Me.BlendPanel1.Controls.Add(Me.TXTREMARKS)
        Me.BlendPanel1.Controls.Add(Me.TXTRATE)
        Me.BlendPanel1.Controls.Add(Me.TXTCUT)
        Me.BlendPanel1.Controls.Add(Me.Label17)
        Me.BlendPanel1.Controls.Add(Me.TXTMTRS)
        Me.BlendPanel1.Controls.Add(Me.Label18)
        Me.BlendPanel1.Controls.Add(Me.Label19)
        Me.BlendPanel1.Controls.Add(Me.Label20)
        Me.BlendPanel1.Controls.Add(Me.TXTDESCRIPTION)
        Me.BlendPanel1.Controls.Add(Me.Label11)
        Me.BlendPanel1.Controls.Add(Me.TXTORDERNO)
        Me.BlendPanel1.Controls.Add(Me.Label12)
        Me.BlendPanel1.Controls.Add(Me.cmbqtyunit)
        Me.BlendPanel1.Controls.Add(Me.Label13)
        Me.BlendPanel1.Controls.Add(Me.Label14)
        Me.BlendPanel1.Controls.Add(Me.txtQTY)
        Me.BlendPanel1.Controls.Add(Me.cmbitemname)
        Me.BlendPanel1.Controls.Add(Me.Label15)
        Me.BlendPanel1.Controls.Add(Me.duedate)
        Me.BlendPanel1.Controls.Add(Me.CMBORDERON)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.Label7)
        Me.BlendPanel1.Controls.Add(Me.TXTDISCOUNT)
        Me.BlendPanel1.Controls.Add(Me.Label8)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.Label10)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.TXTDELPERIOD)
        Me.BlendPanel1.Controls.Add(Me.ORDERDATE)
        Me.BlendPanel1.Controls.Add(Me.CMBSELLERS)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.TXTCRDAYS)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.CMBCODE)
        Me.BlendPanel1.Controls.Add(Me.cmdOK)
        Me.BlendPanel1.Controls.Add(Me.TXTNO)
        Me.BlendPanel1.Controls.Add(Me.cmdclear)
        Me.BlendPanel1.Controls.Add(Me.cmdEXIT)
        Me.BlendPanel1.Controls.Add(Me.txtsrno)
        Me.BlendPanel1.Controls.Add(Me.CMBBUYERS)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1444, 661)
        Me.BlendPanel1.TabIndex = 0
        '
        'GRIDMAGICBOX
        '
        Me.GRIDMAGICBOX.AllowUserToAddRows = False
        Me.GRIDMAGICBOX.AllowUserToDeleteRows = False
        Me.GRIDMAGICBOX.AllowUserToResizeColumns = False
        Me.GRIDMAGICBOX.AllowUserToResizeRows = False
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDMAGICBOX.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        Me.GRIDMAGICBOX.BackgroundColor = System.Drawing.Color.White
        Me.GRIDMAGICBOX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDMAGICBOX.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDMAGICBOX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.GRIDMAGICBOX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDMAGICBOX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.gsrno, Me.GNO, Me.GDATE, Me.GBUYERS, Me.GSELLERS, Me.gorderon, Me.GCRDAYS, Me.GDISCOUNT, Me.GDELPERIOD, Me.GDUEDATE, Me.GORDERNO, Me.gitemname, Me.GDESCRIPTION, Me.gQty, Me.gqtyunit, Me.gcut, Me.GMTRS, Me.GRATE, Me.GREMARKS})
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDMAGICBOX.DefaultCellStyle = DataGridViewCellStyle17
        Me.GRIDMAGICBOX.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDMAGICBOX.Location = New System.Drawing.Point(12, 171)
        Me.GRIDMAGICBOX.MultiSelect = False
        Me.GRIDMAGICBOX.Name = "GRIDMAGICBOX"
        Me.GRIDMAGICBOX.RowHeadersVisible = False
        Me.GRIDMAGICBOX.RowHeadersWidth = 30
        Me.GRIDMAGICBOX.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDMAGICBOX.RowsDefaultCellStyle = DataGridViewCellStyle18
        Me.GRIDMAGICBOX.RowTemplate.Height = 20
        Me.GRIDMAGICBOX.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDMAGICBOX.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDMAGICBOX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDMAGICBOX.Size = New System.Drawing.Size(1420, 444)
        Me.GRIDMAGICBOX.TabIndex = 22
        Me.GRIDMAGICBOX.TabStop = False
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
        'gorderon
        '
        Me.gorderon.HeaderText = "Order On"
        Me.gorderon.Name = "gorderon"
        Me.gorderon.Width = 80
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
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.gitemname.DefaultCellStyle = DataGridViewCellStyle12
        Me.gitemname.HeaderText = "Item Name"
        Me.gitemname.Name = "gitemname"
        Me.gitemname.ReadOnly = True
        Me.gitemname.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gitemname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gitemname.Width = 200
        '
        'GDESCRIPTION
        '
        Me.GDESCRIPTION.HeaderText = "Design"
        Me.GDESCRIPTION.Name = "GDESCRIPTION"
        Me.GDESCRIPTION.ReadOnly = True
        Me.GDESCRIPTION.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDESCRIPTION.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDESCRIPTION.Width = 120
        '
        'gQty
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.NullValue = Nothing
        Me.gQty.DefaultCellStyle = DataGridViewCellStyle13
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
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.gcut.DefaultCellStyle = DataGridViewCellStyle14
        Me.gcut.HeaderText = "LR"
        Me.gcut.Name = "gcut"
        Me.gcut.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gcut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gcut.Width = 45
        '
        'GMTRS
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GMTRS.DefaultCellStyle = DataGridViewCellStyle15
        Me.GMTRS.HeaderText = "Mtrs."
        Me.GMTRS.Name = "GMTRS"
        Me.GMTRS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMTRS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMTRS.Width = 60
        '
        'GRATE
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GRATE.DefaultCellStyle = DataGridViewCellStyle16
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
        'TXTREMARKS
        '
        Me.TXTREMARKS.BackColor = System.Drawing.Color.White
        Me.TXTREMARKS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTREMARKS.Location = New System.Drawing.Point(1002, 103)
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(201, 23)
        Me.TXTREMARKS.TabIndex = 18
        '
        'TXTRATE
        '
        Me.TXTRATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTRATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTRATE.Location = New System.Drawing.Point(1002, 74)
        Me.TXTRATE.Name = "TXTRATE"
        Me.TXTRATE.Size = New System.Drawing.Size(89, 23)
        Me.TXTRATE.TabIndex = 17
        Me.TXTRATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTCUT
        '
        Me.TXTCUT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCUT.Location = New System.Drawing.Point(1002, 16)
        Me.TXTCUT.MaxLength = 3
        Me.TXTCUT.Name = "TXTCUT"
        Me.TXTCUT.Size = New System.Drawing.Size(89, 23)
        Me.TXTCUT.TabIndex = 15
        Me.TXTCUT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(945, 107)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(54, 15)
        Me.Label17.TabIndex = 42
        Me.Label17.Text = "Remarks"
        '
        'TXTMTRS
        '
        Me.TXTMTRS.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTMTRS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTMTRS.Location = New System.Drawing.Point(1002, 45)
        Me.TXTMTRS.Name = "TXTMTRS"
        Me.TXTMTRS.Size = New System.Drawing.Size(89, 23)
        Me.TXTMTRS.TabIndex = 16
        Me.TXTMTRS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(968, 78)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(31, 15)
        Me.Label18.TabIndex = 41
        Me.Label18.Text = "Rate"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(966, 49)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(33, 15)
        Me.Label19.TabIndex = 40
        Me.Label19.Text = "Mtrs"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(939, 20)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(60, 15)
        Me.Label20.TabIndex = 39
        Me.Label20.Text = "No of LR's"
        '
        'TXTDESCRIPTION
        '
        Me.TXTDESCRIPTION.BackColor = System.Drawing.Color.White
        Me.TXTDESCRIPTION.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTDESCRIPTION.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTDESCRIPTION.Location = New System.Drawing.Point(626, 74)
        Me.TXTDESCRIPTION.Name = "TXTDESCRIPTION"
        Me.TXTDESCRIPTION.Size = New System.Drawing.Size(200, 23)
        Me.TXTDESCRIPTION.TabIndex = 12
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(593, 136)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(30, 15)
        Me.Label11.TabIndex = 37
        Me.Label11.Text = "Unit"
        '
        'TXTORDERNO
        '
        Me.TXTORDERNO.BackColor = System.Drawing.Color.White
        Me.TXTORDERNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTORDERNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTORDERNO.Location = New System.Drawing.Point(626, 16)
        Me.TXTORDERNO.Name = "TXTORDERNO"
        Me.TXTORDERNO.Size = New System.Drawing.Size(89, 23)
        Me.TXTORDERNO.TabIndex = 10
        Me.TXTORDERNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(573, 107)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(50, 15)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "Qty/Pcs"
        '
        'cmbqtyunit
        '
        Me.cmbqtyunit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbqtyunit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbqtyunit.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbqtyunit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbqtyunit.FormattingEnabled = True
        Me.cmbqtyunit.Location = New System.Drawing.Point(626, 132)
        Me.cmbqtyunit.Name = "cmbqtyunit"
        Me.cmbqtyunit.Size = New System.Drawing.Size(89, 23)
        Me.cmbqtyunit.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(553, 78)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(70, 15)
        Me.Label13.TabIndex = 35
        Me.Label13.Text = "Description"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(558, 49)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(65, 15)
        Me.Label14.TabIndex = 34
        Me.Label14.Text = "Item Name"
        '
        'txtQTY
        '
        Me.txtQTY.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtQTY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQTY.Location = New System.Drawing.Point(626, 103)
        Me.txtQTY.Name = "txtQTY"
        Me.txtQTY.Size = New System.Drawing.Size(89, 23)
        Me.txtQTY.TabIndex = 13
        Me.txtQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbitemname
        '
        Me.cmbitemname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbitemname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbitemname.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbitemname.DropDownWidth = 400
        Me.cmbitemname.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbitemname.FormattingEnabled = True
        Me.cmbitemname.Location = New System.Drawing.Point(626, 45)
        Me.cmbitemname.Name = "cmbitemname"
        Me.cmbitemname.Size = New System.Drawing.Size(200, 23)
        Me.cmbitemname.TabIndex = 11
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(534, 20)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(89, 15)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = "Party Order No"
        '
        'duedate
        '
        Me.duedate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.duedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.duedate.Location = New System.Drawing.Point(402, 132)
        Me.duedate.Name = "duedate"
        Me.duedate.Size = New System.Drawing.Size(89, 23)
        Me.duedate.TabIndex = 9
        Me.duedate.TabStop = False
        '
        'CMBORDERON
        '
        Me.CMBORDERON.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBORDERON.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBORDERON.BackColor = System.Drawing.Color.White
        Me.CMBORDERON.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBORDERON.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBORDERON.FormattingEnabled = True
        Me.CMBORDERON.Items.AddRange(New Object() {"PCS", "MTRS"})
        Me.CMBORDERON.Location = New System.Drawing.Point(402, 16)
        Me.CMBORDERON.MaxDropDownItems = 14
        Me.CMBORDERON.Name = "CMBORDERON"
        Me.CMBORDERON.Size = New System.Drawing.Size(89, 22)
        Me.CMBORDERON.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(346, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 15)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Del Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(344, 107)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 15)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "Del Days"
        '
        'TXTDISCOUNT
        '
        Me.TXTDISCOUNT.BackColor = System.Drawing.Color.White
        Me.TXTDISCOUNT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTDISCOUNT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTDISCOUNT.Location = New System.Drawing.Point(402, 74)
        Me.TXTDISCOUNT.Name = "TXTDISCOUNT"
        Me.TXTDISCOUNT.Size = New System.Drawing.Size(89, 23)
        Me.TXTDISCOUNT.TabIndex = 7
        Me.TXTDISCOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(356, 78)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 15)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "Disc %"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(350, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 15)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "Cr Days"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(341, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 15)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "Order On"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(46, 136)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 15)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Seller"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(46, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 15)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "Buyer"
        '
        'TXTDELPERIOD
        '
        Me.TXTDELPERIOD.BackColor = System.Drawing.Color.White
        Me.TXTDELPERIOD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTDELPERIOD.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTDELPERIOD.Location = New System.Drawing.Point(402, 103)
        Me.TXTDELPERIOD.Name = "TXTDELPERIOD"
        Me.TXTDELPERIOD.Size = New System.Drawing.Size(89, 23)
        Me.TXTDELPERIOD.TabIndex = 8
        Me.TXTDELPERIOD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ORDERDATE
        '
        Me.ORDERDATE.AsciiOnly = True
        Me.ORDERDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.ORDERDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.ORDERDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.ORDERDATE.Location = New System.Drawing.Point(87, 74)
        Me.ORDERDATE.Mask = "00/00/0000"
        Me.ORDERDATE.Name = "ORDERDATE"
        Me.ORDERDATE.Size = New System.Drawing.Size(89, 23)
        Me.ORDERDATE.TabIndex = 2
        Me.ORDERDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.ORDERDATE.ValidatingType = GetType(Date)
        '
        'CMBSELLERS
        '
        Me.CMBSELLERS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSELLERS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSELLERS.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBSELLERS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSELLERS.FormattingEnabled = True
        Me.CMBSELLERS.Items.AddRange(New Object() {""})
        Me.CMBSELLERS.Location = New System.Drawing.Point(87, 132)
        Me.CMBSELLERS.Name = "CMBSELLERS"
        Me.CMBSELLERS.Size = New System.Drawing.Size(202, 23)
        Me.CMBSELLERS.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(52, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 15)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(27, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 15)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Order No"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(48, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 15)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Sr No"
        '
        'TXTCRDAYS
        '
        Me.TXTCRDAYS.BackColor = System.Drawing.Color.White
        Me.TXTCRDAYS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCRDAYS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCRDAYS.Location = New System.Drawing.Point(402, 45)
        Me.TXTCRDAYS.MaxLength = 3
        Me.TXTCRDAYS.Name = "TXTCRDAYS"
        Me.TXTCRDAYS.Size = New System.Drawing.Size(89, 23)
        Me.TXTCRDAYS.TabIndex = 6
        Me.TXTCRDAYS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.Color.Transparent
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdOK.FlatAppearance.BorderSize = 0
        Me.cmdOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOK.ForeColor = System.Drawing.Color.Black
        Me.cmdOK.Location = New System.Drawing.Point(596, 621)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(80, 28)
        Me.cmdOK.TabIndex = 19
        Me.cmdOK.Text = "&Save"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'TXTNO
        '
        Me.TXTNO.BackColor = System.Drawing.Color.Linen
        Me.TXTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTNO.Location = New System.Drawing.Point(87, 45)
        Me.TXTNO.Name = "TXTNO"
        Me.TXTNO.Size = New System.Drawing.Size(89, 23)
        Me.TXTNO.TabIndex = 1
        Me.TXTNO.TabStop = False
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.Color.Transparent
        Me.cmdclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdclear.FlatAppearance.BorderSize = 0
        Me.cmdclear.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.ForeColor = System.Drawing.Color.Black
        Me.cmdclear.Location = New System.Drawing.Point(682, 621)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(80, 28)
        Me.cmdclear.TabIndex = 20
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
        Me.cmdEXIT.Location = New System.Drawing.Point(768, 621)
        Me.cmdEXIT.Name = "cmdEXIT"
        Me.cmdEXIT.Size = New System.Drawing.Size(80, 28)
        Me.cmdEXIT.TabIndex = 21
        Me.cmdEXIT.Text = "E&xit"
        Me.cmdEXIT.UseVisualStyleBackColor = False
        '
        'txtsrno
        '
        Me.txtsrno.BackColor = System.Drawing.Color.Linen
        Me.txtsrno.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrno.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtsrno.Location = New System.Drawing.Point(87, 16)
        Me.txtsrno.Name = "txtsrno"
        Me.txtsrno.ReadOnly = True
        Me.txtsrno.Size = New System.Drawing.Size(89, 23)
        Me.txtsrno.TabIndex = 0
        Me.txtsrno.TabStop = False
        '
        'CMBBUYERS
        '
        Me.CMBBUYERS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBBUYERS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBBUYERS.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBBUYERS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBBUYERS.FormattingEnabled = True
        Me.CMBBUYERS.Items.AddRange(New Object() {""})
        Me.CMBBUYERS.Location = New System.Drawing.Point(87, 103)
        Me.CMBBUYERS.Name = "CMBBUYERS"
        Me.CMBBUYERS.Size = New System.Drawing.Size(202, 23)
        Me.CMBBUYERS.TabIndex = 3
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
        'MagicBox
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1444, 661)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "MagicBox"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Magic Box"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDMAGICBOX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents TXTDISCOUNT As TextBox
    Friend WithEvents TXTDELPERIOD As TextBox
    Friend WithEvents TXTCRDAYS As TextBox
    Friend WithEvents TXTCUT As TextBox
    Friend WithEvents TXTRATE As TextBox
    Friend WithEvents GRIDMAGICBOX As DataGridView
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
    Friend WithEvents CMBORDERON As ComboBox
    Friend WithEvents ORDERDATE As MaskedTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents TXTDESCRIPTION As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents gsrno As DataGridViewTextBoxColumn
    Friend WithEvents GNO As DataGridViewTextBoxColumn
    Friend WithEvents GDATE As DataGridViewTextBoxColumn
    Friend WithEvents GBUYERS As DataGridViewTextBoxColumn
    Friend WithEvents GSELLERS As DataGridViewTextBoxColumn
    Friend WithEvents gorderon As DataGridViewTextBoxColumn
    Friend WithEvents GCRDAYS As DataGridViewTextBoxColumn
    Friend WithEvents GDISCOUNT As DataGridViewTextBoxColumn
    Friend WithEvents GDELPERIOD As DataGridViewTextBoxColumn
    Friend WithEvents GDUEDATE As DataGridViewTextBoxColumn
    Friend WithEvents GORDERNO As DataGridViewTextBoxColumn
    Friend WithEvents gitemname As DataGridViewTextBoxColumn
    Friend WithEvents GDESCRIPTION As DataGridViewTextBoxColumn
    Friend WithEvents gQty As DataGridViewTextBoxColumn
    Friend WithEvents gqtyunit As DataGridViewTextBoxColumn
    Friend WithEvents gcut As DataGridViewTextBoxColumn
    Friend WithEvents GMTRS As DataGridViewTextBoxColumn
    Friend WithEvents GRATE As DataGridViewTextBoxColumn
    Friend WithEvents GREMARKS As DataGridViewTextBoxColumn
End Class
