<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class YarnQualityMaster
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CMBGREYQUALITY = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CMBMILLNAME = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TXTSHADENO = New System.Windows.Forms.TextBox()
        Me.LBLCOUNT = New System.Windows.Forms.Label()
        Me.txtcount = New System.Windows.Forms.TextBox()
        Me.CMBHSNCODE = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TXTRATE = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TXTDENIER = New System.Windows.Forms.TextBox()
        Me.GPSTORES = New System.Windows.Forms.GroupBox()
        Me.TXTSTOREQTY = New System.Windows.Forms.TextBox()
        Me.CMBSTOREITEM = New System.Windows.Forms.ComboBox()
        Me.TXTSSRNO = New System.Windows.Forms.TextBox()
        Me.GRIDSTORES = New System.Windows.Forms.DataGridView()
        Me.SSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SSTOREITEM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CMBCATEGORY = New System.Windows.Forms.ComboBox()
        Me.lblcategory = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TXTBOXWT = New System.Windows.Forms.TextBox()
        Me.GRPCOMPOSITION = New System.Windows.Forms.GroupBox()
        Me.TXTTOTALPER = New System.Windows.Forms.TextBox()
        Me.CMBYARNQUALITY = New System.Windows.Forms.ComboBox()
        Me.TXTPER = New System.Windows.Forms.TextBox()
        Me.GRIDCOMP = New System.Windows.Forms.DataGridView()
        Me.GYARNQUALITY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.LBLCODE = New System.Windows.Forms.Label()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.chkchange = New System.Windows.Forms.CheckBox()
        Me.lblgroup = New System.Windows.Forms.Label()
        Me.txtname = New System.Windows.Forms.TextBox()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.Ep = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.BlendPanel1.SuspendLayout()
        Me.GPSTORES.SuspendLayout()
        CType(Me.GRIDSTORES, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GRPCOMPOSITION.SuspendLayout()
        CType(Me.GRIDCOMP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.CMBGREYQUALITY)
        Me.BlendPanel1.Controls.Add(Me.Label8)
        Me.BlendPanel1.Controls.Add(Me.CMBMILLNAME)
        Me.BlendPanel1.Controls.Add(Me.Label7)
        Me.BlendPanel1.Controls.Add(Me.TXTSHADENO)
        Me.BlendPanel1.Controls.Add(Me.LBLCOUNT)
        Me.BlendPanel1.Controls.Add(Me.txtcount)
        Me.BlendPanel1.Controls.Add(Me.CMBHSNCODE)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.TXTRATE)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.TXTDENIER)
        Me.BlendPanel1.Controls.Add(Me.GPSTORES)
        Me.BlendPanel1.Controls.Add(Me.CMBCATEGORY)
        Me.BlendPanel1.Controls.Add(Me.lblcategory)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.TXTBOXWT)
        Me.BlendPanel1.Controls.Add(Me.GRPCOMPOSITION)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.LBLCODE)
        Me.BlendPanel1.Controls.Add(Me.txtremarks)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.chkchange)
        Me.BlendPanel1.Controls.Add(Me.lblgroup)
        Me.BlendPanel1.Controls.Add(Me.txtname)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(965, 447)
        Me.BlendPanel1.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(47, 69)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 15)
        Me.Label6.TabIndex = 672
        Me.Label6.Text = "Effect Into"
        '
        'CMBGREYQUALITY
        '
        Me.CMBGREYQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGREYQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGREYQUALITY.FormattingEnabled = True
        Me.CMBGREYQUALITY.Location = New System.Drawing.Point(110, 65)
        Me.CMBGREYQUALITY.Name = "CMBGREYQUALITY"
        Me.CMBGREYQUALITY.Size = New System.Drawing.Size(233, 23)
        Me.CMBGREYQUALITY.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(40, 126)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 14)
        Me.Label8.TabIndex = 365
        Me.Label8.Text = "Mill Name"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CMBMILLNAME
        '
        Me.CMBMILLNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBMILLNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBMILLNAME.FormattingEnabled = True
        Me.CMBMILLNAME.Location = New System.Drawing.Point(110, 122)
        Me.CMBMILLNAME.Name = "CMBMILLNAME"
        Me.CMBMILLNAME.Size = New System.Drawing.Size(233, 23)
        Me.CMBMILLNAME.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(213, 98)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 14)
        Me.Label7.TabIndex = 363
        Me.Label7.Text = "Shade No"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTSHADENO
        '
        Me.TXTSHADENO.BackColor = System.Drawing.Color.White
        Me.TXTSHADENO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSHADENO.Location = New System.Drawing.Point(276, 94)
        Me.TXTSHADENO.Name = "TXTSHADENO"
        Me.TXTSHADENO.Size = New System.Drawing.Size(67, 22)
        Me.TXTSHADENO.TabIndex = 4
        Me.TXTSHADENO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLCOUNT
        '
        Me.LBLCOUNT.BackColor = System.Drawing.Color.Transparent
        Me.LBLCOUNT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLCOUNT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LBLCOUNT.Location = New System.Drawing.Point(58, 11)
        Me.LBLCOUNT.Name = "LBLCOUNT"
        Me.LBLCOUNT.Size = New System.Drawing.Size(49, 22)
        Me.LBLCOUNT.TabIndex = 361
        Me.LBLCOUNT.Text = "Count"
        Me.LBLCOUNT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LBLCOUNT.Visible = False
        '
        'txtcount
        '
        Me.txtcount.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtcount.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcount.Location = New System.Drawing.Point(110, 12)
        Me.txtcount.Name = "txtcount"
        Me.txtcount.Size = New System.Drawing.Size(233, 22)
        Me.txtcount.TabIndex = 0
        Me.txtcount.Visible = False
        '
        'CMBHSNCODE
        '
        Me.CMBHSNCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBHSNCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBHSNCODE.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBHSNCODE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBHSNCODE.FormattingEnabled = True
        Me.CMBHSNCODE.Location = New System.Drawing.Point(270, 288)
        Me.CMBHSNCODE.MaxDropDownItems = 14
        Me.CMBHSNCODE.Name = "CMBHSNCODE"
        Me.CMBHSNCODE.Size = New System.Drawing.Size(73, 23)
        Me.CMBHSNCODE.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(47, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 14)
        Me.Label5.TabIndex = 359
        Me.Label5.Text = "Yarn Rate"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTRATE
        '
        Me.TXTRATE.BackColor = System.Drawing.Color.White
        Me.TXTRATE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTRATE.Location = New System.Drawing.Point(110, 151)
        Me.TXTRATE.Name = "TXTRATE"
        Me.TXTRATE.Size = New System.Drawing.Size(67, 22)
        Me.TXTRATE.TabIndex = 6
        Me.TXTRATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(27, 424)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(163, 14)
        Me.Label4.TabIndex = 357
        Me.Label4.Text = "Press 'F1' To Select HSN Code"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(209, 292)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 14)
        Me.Label3.TabIndex = 356
        Me.Label3.Text = "HSN Code"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(69, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 14)
        Me.Label2.TabIndex = 313
        Me.Label2.Text = "Count"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTDENIER
        '
        Me.TXTDENIER.BackColor = System.Drawing.Color.White
        Me.TXTDENIER.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTDENIER.Location = New System.Drawing.Point(110, 94)
        Me.TXTDENIER.Name = "TXTDENIER"
        Me.TXTDENIER.Size = New System.Drawing.Size(67, 22)
        Me.TXTDENIER.TabIndex = 3
        Me.TXTDENIER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GPSTORES
        '
        Me.GPSTORES.BackColor = System.Drawing.Color.Transparent
        Me.GPSTORES.Controls.Add(Me.TXTSTOREQTY)
        Me.GPSTORES.Controls.Add(Me.CMBSTOREITEM)
        Me.GPSTORES.Controls.Add(Me.TXTSSRNO)
        Me.GPSTORES.Controls.Add(Me.GRIDSTORES)
        Me.GPSTORES.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GPSTORES.ForeColor = System.Drawing.Color.Black
        Me.GPSTORES.Location = New System.Drawing.Point(653, 29)
        Me.GPSTORES.Name = "GPSTORES"
        Me.GPSTORES.Size = New System.Drawing.Size(292, 206)
        Me.GPSTORES.TabIndex = 12
        Me.GPSTORES.TabStop = False
        Me.GPSTORES.Text = "Stores Details"
        '
        'TXTSTOREQTY
        '
        Me.TXTSTOREQTY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSTOREQTY.Location = New System.Drawing.Point(211, 21)
        Me.TXTSTOREQTY.Name = "TXTSTOREQTY"
        Me.TXTSTOREQTY.Size = New System.Drawing.Size(40, 22)
        Me.TXTSTOREQTY.TabIndex = 2
        Me.TXTSTOREQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBSTOREITEM
        '
        Me.CMBSTOREITEM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSTOREITEM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSTOREITEM.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBSTOREITEM.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSTOREITEM.FormattingEnabled = True
        Me.CMBSTOREITEM.Location = New System.Drawing.Point(51, 21)
        Me.CMBSTOREITEM.MaxDropDownItems = 14
        Me.CMBSTOREITEM.Name = "CMBSTOREITEM"
        Me.CMBSTOREITEM.Size = New System.Drawing.Size(160, 22)
        Me.CMBSTOREITEM.TabIndex = 1
        '
        'TXTSSRNO
        '
        Me.TXTSSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTSSRNO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSSRNO.Location = New System.Drawing.Point(11, 21)
        Me.TXTSSRNO.Name = "TXTSSRNO"
        Me.TXTSSRNO.ReadOnly = True
        Me.TXTSSRNO.Size = New System.Drawing.Size(40, 22)
        Me.TXTSSRNO.TabIndex = 0
        Me.TXTSSRNO.TabStop = False
        Me.TXTSSRNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GRIDSTORES
        '
        Me.GRIDSTORES.AllowUserToAddRows = False
        Me.GRIDSTORES.AllowUserToDeleteRows = False
        Me.GRIDSTORES.AllowUserToResizeColumns = False
        Me.GRIDSTORES.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GRIDSTORES.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDSTORES.BackgroundColor = System.Drawing.Color.White
        Me.GRIDSTORES.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDSTORES.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDSTORES.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDSTORES.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDSTORES.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SSRNO, Me.SSTOREITEM, Me.SQTY})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSTORES.DefaultCellStyle = DataGridViewCellStyle3
        Me.GRIDSTORES.GridColor = System.Drawing.SystemColors.ControlText
        Me.GRIDSTORES.Location = New System.Drawing.Point(11, 43)
        Me.GRIDSTORES.Margin = New System.Windows.Forms.Padding(2)
        Me.GRIDSTORES.MultiSelect = False
        Me.GRIDSTORES.Name = "GRIDSTORES"
        Me.GRIDSTORES.ReadOnly = True
        Me.GRIDSTORES.RowHeadersVisible = False
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDSTORES.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.GRIDSTORES.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDSTORES.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDSTORES.Size = New System.Drawing.Size(270, 158)
        Me.GRIDSTORES.TabIndex = 3
        '
        'SSRNO
        '
        Me.SSRNO.HeaderText = "Sr"
        Me.SSRNO.Name = "SSRNO"
        Me.SSRNO.ReadOnly = True
        Me.SSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.SSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SSRNO.Width = 40
        '
        'SSTOREITEM
        '
        Me.SSTOREITEM.HeaderText = "Store Item Name"
        Me.SSTOREITEM.Name = "SSTOREITEM"
        Me.SSTOREITEM.ReadOnly = True
        Me.SSTOREITEM.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.SSTOREITEM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SSTOREITEM.Width = 160
        '
        'SQTY
        '
        Me.SQTY.HeaderText = "Qty"
        Me.SQTY.Name = "SQTY"
        Me.SQTY.ReadOnly = True
        Me.SQTY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.SQTY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SQTY.Width = 40
        '
        'CMBCATEGORY
        '
        Me.CMBCATEGORY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCATEGORY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCATEGORY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCATEGORY.FormattingEnabled = True
        Me.CMBCATEGORY.Location = New System.Drawing.Point(110, 180)
        Me.CMBCATEGORY.MaxDropDownItems = 14
        Me.CMBCATEGORY.Name = "CMBCATEGORY"
        Me.CMBCATEGORY.Size = New System.Drawing.Size(233, 22)
        Me.CMBCATEGORY.TabIndex = 7
        '
        'lblcategory
        '
        Me.lblcategory.AutoSize = True
        Me.lblcategory.BackColor = System.Drawing.Color.Transparent
        Me.lblcategory.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcategory.Location = New System.Drawing.Point(52, 184)
        Me.lblcategory.Name = "lblcategory"
        Me.lblcategory.Size = New System.Drawing.Size(53, 14)
        Me.lblcategory.TabIndex = 311
        Me.lblcategory.Text = "Category"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(13, 292)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 14)
        Me.Label1.TabIndex = 157
        Me.Label1.Text = "Wt Per Box/Tube"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTBOXWT
        '
        Me.TXTBOXWT.BackColor = System.Drawing.Color.White
        Me.TXTBOXWT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBOXWT.Location = New System.Drawing.Point(110, 288)
        Me.TXTBOXWT.Name = "TXTBOXWT"
        Me.TXTBOXWT.Size = New System.Drawing.Size(73, 22)
        Me.TXTBOXWT.TabIndex = 9
        Me.TXTBOXWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GRPCOMPOSITION
        '
        Me.GRPCOMPOSITION.BackColor = System.Drawing.Color.Transparent
        Me.GRPCOMPOSITION.Controls.Add(Me.TXTTOTALPER)
        Me.GRPCOMPOSITION.Controls.Add(Me.CMBYARNQUALITY)
        Me.GRPCOMPOSITION.Controls.Add(Me.TXTPER)
        Me.GRPCOMPOSITION.Controls.Add(Me.GRIDCOMP)
        Me.GRPCOMPOSITION.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRPCOMPOSITION.ForeColor = System.Drawing.Color.Black
        Me.GRPCOMPOSITION.Location = New System.Drawing.Point(355, 29)
        Me.GRPCOMPOSITION.Name = "GRPCOMPOSITION"
        Me.GRPCOMPOSITION.Size = New System.Drawing.Size(292, 173)
        Me.GRPCOMPOSITION.TabIndex = 11
        Me.GRPCOMPOSITION.TabStop = False
        Me.GRPCOMPOSITION.Text = "Blend Composition"
        '
        'TXTTOTALPER
        '
        Me.TXTTOTALPER.BackColor = System.Drawing.Color.Linen
        Me.TXTTOTALPER.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTOTALPER.Location = New System.Drawing.Point(211, 146)
        Me.TXTTOTALPER.Name = "TXTTOTALPER"
        Me.TXTTOTALPER.ReadOnly = True
        Me.TXTTOTALPER.Size = New System.Drawing.Size(40, 22)
        Me.TXTTOTALPER.TabIndex = 355
        Me.TXTTOTALPER.TabStop = False
        Me.TXTTOTALPER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBYARNQUALITY
        '
        Me.CMBYARNQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBYARNQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBYARNQUALITY.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBYARNQUALITY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBYARNQUALITY.FormattingEnabled = True
        Me.CMBYARNQUALITY.Location = New System.Drawing.Point(11, 21)
        Me.CMBYARNQUALITY.MaxDropDownItems = 14
        Me.CMBYARNQUALITY.Name = "CMBYARNQUALITY"
        Me.CMBYARNQUALITY.Size = New System.Drawing.Size(200, 22)
        Me.CMBYARNQUALITY.TabIndex = 0
        '
        'TXTPER
        '
        Me.TXTPER.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTPER.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPER.Location = New System.Drawing.Point(211, 21)
        Me.TXTPER.Name = "TXTPER"
        Me.TXTPER.Size = New System.Drawing.Size(40, 22)
        Me.TXTPER.TabIndex = 1
        Me.TXTPER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GRIDCOMP
        '
        Me.GRIDCOMP.AllowUserToAddRows = False
        Me.GRIDCOMP.AllowUserToDeleteRows = False
        Me.GRIDCOMP.AllowUserToResizeColumns = False
        Me.GRIDCOMP.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GRIDCOMP.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDCOMP.BackgroundColor = System.Drawing.Color.White
        Me.GRIDCOMP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDCOMP.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDCOMP.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDCOMP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDCOMP.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GYARNQUALITY, Me.GPER})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDCOMP.DefaultCellStyle = DataGridViewCellStyle8
        Me.GRIDCOMP.GridColor = System.Drawing.SystemColors.ControlText
        Me.GRIDCOMP.Location = New System.Drawing.Point(11, 43)
        Me.GRIDCOMP.Margin = New System.Windows.Forms.Padding(2)
        Me.GRIDCOMP.MultiSelect = False
        Me.GRIDCOMP.Name = "GRIDCOMP"
        Me.GRIDCOMP.ReadOnly = True
        Me.GRIDCOMP.RowHeadersVisible = False
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDCOMP.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.GRIDCOMP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDCOMP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDCOMP.Size = New System.Drawing.Size(270, 100)
        Me.GRIDCOMP.TabIndex = 2
        '
        'GYARNQUALITY
        '
        Me.GYARNQUALITY.HeaderText = "Yarn Quality"
        Me.GYARNQUALITY.Name = "GYARNQUALITY"
        Me.GYARNQUALITY.ReadOnly = True
        Me.GYARNQUALITY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GYARNQUALITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GYARNQUALITY.Width = 200
        '
        'GPER
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GPER.DefaultCellStyle = DataGridViewCellStyle7
        Me.GPER.HeaderText = "%"
        Me.GPER.Name = "GPER"
        Me.GPER.ReadOnly = True
        Me.GPER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPER.Width = 40
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.Location = New System.Drawing.Point(251, 384)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 14
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = True
        '
        'LBLCODE
        '
        Me.LBLCODE.BackColor = System.Drawing.Color.Transparent
        Me.LBLCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLCODE.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LBLCODE.Location = New System.Drawing.Point(27, 209)
        Me.LBLCODE.Name = "LBLCODE"
        Me.LBLCODE.Size = New System.Drawing.Size(79, 22)
        Me.LBLCODE.TabIndex = 154
        Me.LBLCODE.Text = "Remark"
        Me.LBLCODE.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtremarks
        '
        Me.txtremarks.ForeColor = System.Drawing.Color.DimGray
        Me.txtremarks.Location = New System.Drawing.Point(110, 209)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(233, 69)
        Me.txtremarks.TabIndex = 8
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.FlatAppearance.BorderSize = 0
        Me.cmddelete.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddelete.ForeColor = System.Drawing.Color.Black
        Me.cmddelete.Location = New System.Drawing.Point(337, 384)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 28)
        Me.cmddelete.TabIndex = 15
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(165, 384)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 13
        Me.cmdok.Text = "Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'chkchange
        '
        Me.chkchange.AutoSize = True
        Me.chkchange.Location = New System.Drawing.Point(12, 39)
        Me.chkchange.Name = "chkchange"
        Me.chkchange.Size = New System.Drawing.Size(15, 14)
        Me.chkchange.TabIndex = 152
        Me.chkchange.UseVisualStyleBackColor = True
        Me.chkchange.Visible = False
        '
        'lblgroup
        '
        Me.lblgroup.BackColor = System.Drawing.Color.Transparent
        Me.lblgroup.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblgroup.Location = New System.Drawing.Point(20, 38)
        Me.lblgroup.Name = "lblgroup"
        Me.lblgroup.Size = New System.Drawing.Size(90, 22)
        Me.lblgroup.TabIndex = 149
        Me.lblgroup.Text = "Quality  Name"
        Me.lblgroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtname
        '
        Me.txtname.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtname.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname.Location = New System.Drawing.Point(110, 39)
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(233, 22)
        Me.txtname.TabIndex = 1
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(423, 384)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 16
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'Ep
        '
        Me.Ep.BlinkRate = 0
        Me.Ep.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.Ep.ContainerControl = Me
        '
        'YarnQualityMaster
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(965, 447)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "YarnQualityMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Yarn Quality Master"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.GPSTORES.ResumeLayout(False)
        Me.GPSTORES.PerformLayout()
        CType(Me.GRIDSTORES, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GRPCOMPOSITION.ResumeLayout(False)
        Me.GRPCOMPOSITION.PerformLayout()
        CType(Me.GRIDCOMP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDCLEAR As System.Windows.Forms.Button
    Friend WithEvents LBLCODE As System.Windows.Forms.Label
    Friend WithEvents txtremarks As System.Windows.Forms.TextBox
    Friend WithEvents cmddelete As System.Windows.Forms.Button
    Friend WithEvents cmdok As System.Windows.Forms.Button
    Friend WithEvents chkchange As System.Windows.Forms.CheckBox
    Friend WithEvents lblgroup As System.Windows.Forms.Label
    Friend WithEvents txtname As System.Windows.Forms.TextBox
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents Ep As System.Windows.Forms.ErrorProvider
    Friend WithEvents GRPCOMPOSITION As System.Windows.Forms.GroupBox
    Friend WithEvents TXTTOTALPER As System.Windows.Forms.TextBox
    Friend WithEvents CMBYARNQUALITY As System.Windows.Forms.ComboBox
    Friend WithEvents TXTPER As System.Windows.Forms.TextBox
    Friend WithEvents GRIDCOMP As System.Windows.Forms.DataGridView
    Friend WithEvents GYARNQUALITY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GPER As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents TXTBOXWT As TextBox
    Friend WithEvents CMBCATEGORY As ComboBox
    Friend WithEvents lblcategory As Label
    Friend WithEvents GPSTORES As GroupBox
    Friend WithEvents TXTSTOREQTY As TextBox
    Friend WithEvents CMBSTOREITEM As ComboBox
    Friend WithEvents TXTSSRNO As TextBox
    Friend WithEvents GRIDSTORES As DataGridView
    Friend WithEvents SSRNO As DataGridViewTextBoxColumn
    Friend WithEvents SSTOREITEM As DataGridViewTextBoxColumn
    Friend WithEvents SQTY As DataGridViewTextBoxColumn
    Friend WithEvents Label2 As Label
    Friend WithEvents TXTDENIER As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TXTRATE As TextBox
    Friend WithEvents CMBHSNCODE As ComboBox
    Friend WithEvents LBLCOUNT As Label
    Friend WithEvents txtcount As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TXTSHADENO As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents CMBMILLNAME As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CMBGREYQUALITY As ComboBox
End Class
