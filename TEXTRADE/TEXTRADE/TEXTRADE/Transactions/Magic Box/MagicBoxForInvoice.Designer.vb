<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MagicBoxForInvoice
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
        Me.CMDSELECTPO = New System.Windows.Forms.Button()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.tbitem = New System.Windows.Forms.TabPage()
        Me.CMBCOMM = New System.Windows.Forms.ComboBox()
        Me.CMBPER = New System.Windows.Forms.ComboBox()
        Me.TXTCOMMPER = New System.Windows.Forms.TextBox()
        Me.TXTSGSTAMT = New System.Windows.Forms.TextBox()
        Me.TXTIGSTAMT = New System.Windows.Forms.TextBox()
        Me.TXTSGSTPER = New System.Windows.Forms.TextBox()
        Me.TXTIGSTPER = New System.Windows.Forms.TextBox()
        Me.TXTCGSTPER = New System.Windows.Forms.TextBox()
        Me.TXTCGSTAMT = New System.Windows.Forms.TextBox()
        Me.TXTBALENO = New System.Windows.Forms.TextBox()
        Me.TXTPCS = New System.Windows.Forms.TextBox()
        Me.TXTMTRS = New System.Windows.Forms.TextBox()
        Me.TXTRATES = New System.Windows.Forms.TextBox()
        Me.TXTAMT = New System.Windows.Forms.TextBox()
        Me.TXTCHRGS = New System.Windows.Forms.TextBox()
        Me.TXTSUBTOTAL = New System.Windows.Forms.TextBox()
        Me.TXTLR = New System.Windows.Forms.TextBox()
        Me.CMBTRANS = New System.Windows.Forms.ComboBox()
        Me.TXTDESC = New System.Windows.Forms.TextBox()
        Me.TXTFOLD = New System.Windows.Forms.TextBox()
        Me.TXTQTY = New System.Windows.Forms.TextBox()
        Me.TXTPOTYPE = New System.Windows.Forms.TextBox()
        Me.ENTRYDATE = New System.Windows.Forms.DateTimePicker()
        Me.BILLDATE = New System.Windows.Forms.DateTimePicker()
        Me.LRDATE = New System.Windows.Forms.DateTimePicker()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.CMBSELLERS = New System.Windows.Forms.ComboBox()
        Me.TXTPOSRNO = New System.Windows.Forms.TextBox()
        Me.TXTPONO = New System.Windows.Forms.TextBox()
        Me.TXTROUNDOFF = New System.Windows.Forms.TextBox()
        Me.TXTGRANDTOTAL = New System.Windows.Forms.TextBox()
        Me.GRIDMAGICBOX = New System.Windows.Forms.DataGridView()
        Me.gsrno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBILLDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSELLERS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBUYERS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCRDAYS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPONO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPOSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPOTYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gitemname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFOLD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDESC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTRANS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLRDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBALENO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMTRS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAMT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCHARGES = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSUBTOTAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCGST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCGSTAMT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSGST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSGSTAMT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GIGST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GIGSTAMT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GROUNDOFF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGRANDTOTAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCOMPER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmbitemname = New System.Windows.Forms.ComboBox()
        Me.txtsrno = New System.Windows.Forms.TextBox()
        Me.txtcrdays = New System.Windows.Forms.TextBox()
        Me.TXTPARTYBILLNO = New System.Windows.Forms.TextBox()
        Me.CMBBUYERS = New System.Windows.Forms.ComboBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.cmdEXIT = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.tbitem.SuspendLayout()
        CType(Me.GRIDMAGICBOX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoScroll = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMDSELECTPO)
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
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 2
        '
        'CMDSELECTPO
        '
        Me.CMDSELECTPO.BackColor = System.Drawing.Color.Transparent
        Me.CMDSELECTPO.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSELECTPO.FlatAppearance.BorderSize = 0
        Me.CMDSELECTPO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSELECTPO.ForeColor = System.Drawing.Color.Black
        Me.CMDSELECTPO.Location = New System.Drawing.Point(348, 4)
        Me.CMDSELECTPO.Name = "CMDSELECTPO"
        Me.CMDSELECTPO.Size = New System.Drawing.Size(129, 28)
        Me.CMDSELECTPO.TabIndex = 1
        Me.CMDSELECTPO.Text = "Select PO"
        Me.CMDSELECTPO.UseVisualStyleBackColor = False
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
        Me.TabControl2.Location = New System.Drawing.Point(11, 12)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(3289, 603)
        Me.TabControl2.TabIndex = 0
        '
        'tbitem
        '
        Me.tbitem.AutoScroll = True
        Me.tbitem.BackColor = System.Drawing.Color.Linen
        Me.tbitem.Controls.Add(Me.CMBCOMM)
        Me.tbitem.Controls.Add(Me.CMBPER)
        Me.tbitem.Controls.Add(Me.TXTCOMMPER)
        Me.tbitem.Controls.Add(Me.TXTSGSTAMT)
        Me.tbitem.Controls.Add(Me.TXTIGSTAMT)
        Me.tbitem.Controls.Add(Me.TXTSGSTPER)
        Me.tbitem.Controls.Add(Me.TXTIGSTPER)
        Me.tbitem.Controls.Add(Me.TXTCGSTPER)
        Me.tbitem.Controls.Add(Me.TXTCGSTAMT)
        Me.tbitem.Controls.Add(Me.TXTBALENO)
        Me.tbitem.Controls.Add(Me.TXTPCS)
        Me.tbitem.Controls.Add(Me.TXTMTRS)
        Me.tbitem.Controls.Add(Me.TXTRATES)
        Me.tbitem.Controls.Add(Me.TXTAMT)
        Me.tbitem.Controls.Add(Me.TXTCHRGS)
        Me.tbitem.Controls.Add(Me.TXTSUBTOTAL)
        Me.tbitem.Controls.Add(Me.TXTLR)
        Me.tbitem.Controls.Add(Me.CMBTRANS)
        Me.tbitem.Controls.Add(Me.TXTDESC)
        Me.tbitem.Controls.Add(Me.TXTFOLD)
        Me.tbitem.Controls.Add(Me.TXTQTY)
        Me.tbitem.Controls.Add(Me.TXTPOTYPE)
        Me.tbitem.Controls.Add(Me.ENTRYDATE)
        Me.tbitem.Controls.Add(Me.BILLDATE)
        Me.tbitem.Controls.Add(Me.LRDATE)
        Me.tbitem.Controls.Add(Me.TXTREMARKS)
        Me.tbitem.Controls.Add(Me.CMBSELLERS)
        Me.tbitem.Controls.Add(Me.TXTPOSRNO)
        Me.tbitem.Controls.Add(Me.TXTPONO)
        Me.tbitem.Controls.Add(Me.TXTROUNDOFF)
        Me.tbitem.Controls.Add(Me.TXTGRANDTOTAL)
        Me.tbitem.Controls.Add(Me.GRIDMAGICBOX)
        Me.tbitem.Controls.Add(Me.cmbitemname)
        Me.tbitem.Controls.Add(Me.txtsrno)
        Me.tbitem.Controls.Add(Me.txtcrdays)
        Me.tbitem.Controls.Add(Me.TXTPARTYBILLNO)
        Me.tbitem.Controls.Add(Me.CMBBUYERS)
        Me.tbitem.Location = New System.Drawing.Point(4, 23)
        Me.tbitem.Name = "tbitem"
        Me.tbitem.Padding = New System.Windows.Forms.Padding(3)
        Me.tbitem.Size = New System.Drawing.Size(3281, 576)
        Me.tbitem.TabIndex = 0
        Me.tbitem.Text = "Magic Details"
        '
        'CMBCOMM
        '
        Me.CMBCOMM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBCOMM.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCOMM.FormattingEnabled = True
        Me.CMBCOMM.Items.AddRange(New Object() {"Mtrs", "Qty", "Kgs"})
        Me.CMBCOMM.Location = New System.Drawing.Point(2982, 4)
        Me.CMBCOMM.Name = "CMBCOMM"
        Me.CMBCOMM.Size = New System.Drawing.Size(69, 23)
        Me.CMBCOMM.TabIndex = 34
        '
        'CMBPER
        '
        Me.CMBPER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBPER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBPER.FormattingEnabled = True
        Me.CMBPER.Items.AddRange(New Object() {"Mtrs", "Qty", "Kgs"})
        Me.CMBPER.Location = New System.Drawing.Point(2082, 4)
        Me.CMBPER.Name = "CMBPER"
        Me.CMBPER.Size = New System.Drawing.Size(49, 23)
        Me.CMBPER.TabIndex = 21
        '
        'TXTCOMMPER
        '
        Me.TXTCOMMPER.BackColor = System.Drawing.Color.White
        Me.TXTCOMMPER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOMMPER.Location = New System.Drawing.Point(2912, 4)
        Me.TXTCOMMPER.Name = "TXTCOMMPER"
        Me.TXTCOMMPER.Size = New System.Drawing.Size(70, 23)
        Me.TXTCOMMPER.TabIndex = 33
        Me.TXTCOMMPER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTSGSTAMT
        '
        Me.TXTSGSTAMT.BackColor = System.Drawing.Color.Linen
        Me.TXTSGSTAMT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTSGSTAMT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSGSTAMT.Location = New System.Drawing.Point(2532, 4)
        Me.TXTSGSTAMT.Name = "TXTSGSTAMT"
        Me.TXTSGSTAMT.ReadOnly = True
        Me.TXTSGSTAMT.Size = New System.Drawing.Size(69, 23)
        Me.TXTSGSTAMT.TabIndex = 28
        Me.TXTSGSTAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTIGSTAMT
        '
        Me.TXTIGSTAMT.BackColor = System.Drawing.Color.Linen
        Me.TXTIGSTAMT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTIGSTAMT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTIGSTAMT.Location = New System.Drawing.Point(2662, 4)
        Me.TXTIGSTAMT.Name = "TXTIGSTAMT"
        Me.TXTIGSTAMT.ReadOnly = True
        Me.TXTIGSTAMT.Size = New System.Drawing.Size(69, 23)
        Me.TXTIGSTAMT.TabIndex = 30
        Me.TXTIGSTAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTSGSTPER
        '
        Me.TXTSGSTPER.BackColor = System.Drawing.Color.Linen
        Me.TXTSGSTPER.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTSGSTPER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSGSTPER.Location = New System.Drawing.Point(2471, 4)
        Me.TXTSGSTPER.Name = "TXTSGSTPER"
        Me.TXTSGSTPER.ReadOnly = True
        Me.TXTSGSTPER.Size = New System.Drawing.Size(61, 23)
        Me.TXTSGSTPER.TabIndex = 27
        Me.TXTSGSTPER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTIGSTPER
        '
        Me.TXTIGSTPER.BackColor = System.Drawing.Color.Linen
        Me.TXTIGSTPER.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTIGSTPER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTIGSTPER.Location = New System.Drawing.Point(2601, 4)
        Me.TXTIGSTPER.Name = "TXTIGSTPER"
        Me.TXTIGSTPER.ReadOnly = True
        Me.TXTIGSTPER.Size = New System.Drawing.Size(61, 23)
        Me.TXTIGSTPER.TabIndex = 29
        Me.TXTIGSTPER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTCGSTPER
        '
        Me.TXTCGSTPER.BackColor = System.Drawing.Color.Linen
        Me.TXTCGSTPER.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCGSTPER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCGSTPER.Location = New System.Drawing.Point(2342, 4)
        Me.TXTCGSTPER.Name = "TXTCGSTPER"
        Me.TXTCGSTPER.ReadOnly = True
        Me.TXTCGSTPER.Size = New System.Drawing.Size(60, 23)
        Me.TXTCGSTPER.TabIndex = 25
        Me.TXTCGSTPER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTCGSTAMT
        '
        Me.TXTCGSTAMT.BackColor = System.Drawing.Color.Linen
        Me.TXTCGSTAMT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCGSTAMT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCGSTAMT.Location = New System.Drawing.Point(2402, 4)
        Me.TXTCGSTAMT.Name = "TXTCGSTAMT"
        Me.TXTCGSTAMT.ReadOnly = True
        Me.TXTCGSTAMT.Size = New System.Drawing.Size(69, 23)
        Me.TXTCGSTAMT.TabIndex = 26
        Me.TXTCGSTAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTBALENO
        '
        Me.TXTBALENO.BackColor = System.Drawing.Color.White
        Me.TXTBALENO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTBALENO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBALENO.Location = New System.Drawing.Point(1798, 4)
        Me.TXTBALENO.Name = "TXTBALENO"
        Me.TXTBALENO.Size = New System.Drawing.Size(99, 23)
        Me.TXTBALENO.TabIndex = 17
        Me.TXTBALENO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTPCS
        '
        Me.TXTPCS.BackColor = System.Drawing.Color.White
        Me.TXTPCS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTPCS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPCS.Location = New System.Drawing.Point(1897, 4)
        Me.TXTPCS.Name = "TXTPCS"
        Me.TXTPCS.Size = New System.Drawing.Size(61, 23)
        Me.TXTPCS.TabIndex = 18
        Me.TXTPCS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTMTRS
        '
        Me.TXTMTRS.BackColor = System.Drawing.Color.White
        Me.TXTMTRS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTMTRS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTMTRS.Location = New System.Drawing.Point(1958, 4)
        Me.TXTMTRS.Name = "TXTMTRS"
        Me.TXTMTRS.Size = New System.Drawing.Size(59, 23)
        Me.TXTMTRS.TabIndex = 19
        Me.TXTMTRS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTRATES
        '
        Me.TXTRATES.BackColor = System.Drawing.Color.White
        Me.TXTRATES.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTRATES.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTRATES.Location = New System.Drawing.Point(2017, 4)
        Me.TXTRATES.Name = "TXTRATES"
        Me.TXTRATES.Size = New System.Drawing.Size(65, 23)
        Me.TXTRATES.TabIndex = 20
        Me.TXTRATES.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTAMT
        '
        Me.TXTAMT.BackColor = System.Drawing.Color.Linen
        Me.TXTAMT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTAMT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTAMT.Location = New System.Drawing.Point(2131, 4)
        Me.TXTAMT.Name = "TXTAMT"
        Me.TXTAMT.ReadOnly = True
        Me.TXTAMT.Size = New System.Drawing.Size(66, 23)
        Me.TXTAMT.TabIndex = 22
        Me.TXTAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTCHRGS
        '
        Me.TXTCHRGS.BackColor = System.Drawing.Color.Linen
        Me.TXTCHRGS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCHRGS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCHRGS.Location = New System.Drawing.Point(2197, 4)
        Me.TXTCHRGS.Name = "TXTCHRGS"
        Me.TXTCHRGS.ReadOnly = True
        Me.TXTCHRGS.Size = New System.Drawing.Size(65, 23)
        Me.TXTCHRGS.TabIndex = 23
        Me.TXTCHRGS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTSUBTOTAL
        '
        Me.TXTSUBTOTAL.BackColor = System.Drawing.Color.Linen
        Me.TXTSUBTOTAL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTSUBTOTAL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSUBTOTAL.Location = New System.Drawing.Point(2262, 4)
        Me.TXTSUBTOTAL.Name = "TXTSUBTOTAL"
        Me.TXTSUBTOTAL.ReadOnly = True
        Me.TXTSUBTOTAL.Size = New System.Drawing.Size(80, 23)
        Me.TXTSUBTOTAL.TabIndex = 24
        Me.TXTSUBTOTAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTLR
        '
        Me.TXTLR.BackColor = System.Drawing.Color.White
        Me.TXTLR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTLR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLR.Location = New System.Drawing.Point(1630, 4)
        Me.TXTLR.Name = "TXTLR"
        Me.TXTLR.Size = New System.Drawing.Size(79, 23)
        Me.TXTLR.TabIndex = 15
        Me.TXTLR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBTRANS
        '
        Me.CMBTRANS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTRANS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTRANS.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBTRANS.DropDownWidth = 400
        Me.CMBTRANS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTRANS.FormattingEnabled = True
        Me.CMBTRANS.Location = New System.Drawing.Point(1430, 4)
        Me.CMBTRANS.Name = "CMBTRANS"
        Me.CMBTRANS.Size = New System.Drawing.Size(200, 23)
        Me.CMBTRANS.TabIndex = 14
        '
        'TXTDESC
        '
        Me.TXTDESC.BackColor = System.Drawing.Color.White
        Me.TXTDESC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTDESC.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTDESC.Location = New System.Drawing.Point(1281, 4)
        Me.TXTDESC.Name = "TXTDESC"
        Me.TXTDESC.Size = New System.Drawing.Size(149, 23)
        Me.TXTDESC.TabIndex = 13
        Me.TXTDESC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTFOLD
        '
        Me.TXTFOLD.BackColor = System.Drawing.Color.White
        Me.TXTFOLD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTFOLD.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFOLD.Location = New System.Drawing.Point(1226, 4)
        Me.TXTFOLD.Name = "TXTFOLD"
        Me.TXTFOLD.Size = New System.Drawing.Size(55, 23)
        Me.TXTFOLD.TabIndex = 12
        Me.TXTFOLD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTQTY
        '
        Me.TXTQTY.BackColor = System.Drawing.Color.White
        Me.TXTQTY.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTQTY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTQTY.Location = New System.Drawing.Point(1170, 4)
        Me.TXTQTY.Name = "TXTQTY"
        Me.TXTQTY.Size = New System.Drawing.Size(56, 23)
        Me.TXTQTY.TabIndex = 11
        Me.TXTQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTPOTYPE
        '
        Me.TXTPOTYPE.BackColor = System.Drawing.Color.Linen
        Me.TXTPOTYPE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTPOTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPOTYPE.Location = New System.Drawing.Point(890, 4)
        Me.TXTPOTYPE.Name = "TXTPOTYPE"
        Me.TXTPOTYPE.ReadOnly = True
        Me.TXTPOTYPE.Size = New System.Drawing.Size(80, 23)
        Me.TXTPOTYPE.TabIndex = 8
        Me.TXTPOTYPE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ENTRYDATE
        '
        Me.ENTRYDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ENTRYDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ENTRYDATE.Location = New System.Drawing.Point(120, 4)
        Me.ENTRYDATE.Name = "ENTRYDATE"
        Me.ENTRYDATE.Size = New System.Drawing.Size(87, 23)
        Me.ENTRYDATE.TabIndex = 1
        '
        'BILLDATE
        '
        Me.BILLDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BILLDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.BILLDATE.Location = New System.Drawing.Point(33, 4)
        Me.BILLDATE.Name = "BILLDATE"
        Me.BILLDATE.Size = New System.Drawing.Size(87, 23)
        Me.BILLDATE.TabIndex = 0
        '
        'LRDATE
        '
        Me.LRDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LRDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.LRDATE.Location = New System.Drawing.Point(1709, 4)
        Me.LRDATE.Name = "LRDATE"
        Me.LRDATE.Size = New System.Drawing.Size(89, 23)
        Me.LRDATE.TabIndex = 16
        Me.LRDATE.TabStop = False
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.BackColor = System.Drawing.Color.White
        Me.TXTREMARKS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTREMARKS.Location = New System.Drawing.Point(3051, 4)
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(201, 23)
        Me.TXTREMARKS.TabIndex = 35
        '
        'CMBSELLERS
        '
        Me.CMBSELLERS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSELLERS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSELLERS.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBSELLERS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSELLERS.FormattingEnabled = True
        Me.CMBSELLERS.Items.AddRange(New Object() {""})
        Me.CMBSELLERS.Location = New System.Drawing.Point(207, 4)
        Me.CMBSELLERS.Name = "CMBSELLERS"
        Me.CMBSELLERS.Size = New System.Drawing.Size(202, 23)
        Me.CMBSELLERS.TabIndex = 2
        '
        'TXTPOSRNO
        '
        Me.TXTPOSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTPOSRNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTPOSRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPOSRNO.Location = New System.Drawing.Point(821, 4)
        Me.TXTPOSRNO.Name = "TXTPOSRNO"
        Me.TXTPOSRNO.ReadOnly = True
        Me.TXTPOSRNO.Size = New System.Drawing.Size(69, 23)
        Me.TXTPOSRNO.TabIndex = 7
        Me.TXTPOSRNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTPONO
        '
        Me.TXTPONO.BackColor = System.Drawing.Color.Linen
        Me.TXTPONO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTPONO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPONO.Location = New System.Drawing.Point(750, 4)
        Me.TXTPONO.Name = "TXTPONO"
        Me.TXTPONO.ReadOnly = True
        Me.TXTPONO.Size = New System.Drawing.Size(71, 23)
        Me.TXTPONO.TabIndex = 6
        Me.TXTPONO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTROUNDOFF
        '
        Me.TXTROUNDOFF.BackColor = System.Drawing.Color.Linen
        Me.TXTROUNDOFF.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTROUNDOFF.Location = New System.Drawing.Point(2731, 4)
        Me.TXTROUNDOFF.Name = "TXTROUNDOFF"
        Me.TXTROUNDOFF.ReadOnly = True
        Me.TXTROUNDOFF.Size = New System.Drawing.Size(81, 23)
        Me.TXTROUNDOFF.TabIndex = 31
        Me.TXTROUNDOFF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTGRANDTOTAL
        '
        Me.TXTGRANDTOTAL.BackColor = System.Drawing.Color.Linen
        Me.TXTGRANDTOTAL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTGRANDTOTAL.Location = New System.Drawing.Point(2812, 4)
        Me.TXTGRANDTOTAL.Name = "TXTGRANDTOTAL"
        Me.TXTGRANDTOTAL.ReadOnly = True
        Me.TXTGRANDTOTAL.Size = New System.Drawing.Size(100, 23)
        Me.TXTGRANDTOTAL.TabIndex = 32
        Me.TXTGRANDTOTAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.GRIDMAGICBOX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.gsrno, Me.GBILLDATE, Me.GDATE, Me.GSELLERS, Me.GBUYERS, Me.GNO, Me.GCRDAYS, Me.GPONO, Me.GPOSRNO, Me.GPOTYPE, Me.gitemname, Me.gQty, Me.GFOLD, Me.GDESC, Me.GTRANS, Me.GLRNO, Me.GLRDATE, Me.GBALENO, Me.GPCS, Me.GMTRS, Me.GRATE, Me.GPER, Me.GAMT, Me.GCHARGES, Me.GSUBTOTAL, Me.GCGST, Me.GCGSTAMT, Me.GSGST, Me.GSGSTAMT, Me.GIGST, Me.GIGSTAMT, Me.GROUNDOFF, Me.GGRANDTOTAL, Me.GCOMPER, Me.GCOM, Me.GREMARKS})
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDMAGICBOX.DefaultCellStyle = DataGridViewCellStyle17
        Me.GRIDMAGICBOX.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDMAGICBOX.Location = New System.Drawing.Point(3, 27)
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
        Me.GRIDMAGICBOX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDMAGICBOX.Size = New System.Drawing.Size(3272, 543)
        Me.GRIDMAGICBOX.TabIndex = 10
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
        'GBILLDATE
        '
        Me.GBILLDATE.HeaderText = "Bill Date"
        Me.GBILLDATE.Name = "GBILLDATE"
        Me.GBILLDATE.Width = 87
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
        'GSELLERS
        '
        Me.GSELLERS.HeaderText = "Seller's"
        Me.GSELLERS.Name = "GSELLERS"
        Me.GSELLERS.ReadOnly = True
        Me.GSELLERS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSELLERS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSELLERS.Width = 202
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
        'GNO
        '
        Me.GNO.HeaderText = "Party Bill No"
        Me.GNO.Name = "GNO"
        Me.GNO.ReadOnly = True
        Me.GNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GNO.Width = 80
        '
        'GCRDAYS
        '
        Me.GCRDAYS.HeaderText = "Cr Days"
        Me.GCRDAYS.Name = "GCRDAYS"
        Me.GCRDAYS.ReadOnly = True
        Me.GCRDAYS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCRDAYS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCRDAYS.Width = 60
        '
        'GPONO
        '
        Me.GPONO.HeaderText = "PO No"
        Me.GPONO.Name = "GPONO"
        Me.GPONO.Width = 70
        '
        'GPOSRNO
        '
        Me.GPOSRNO.HeaderText = "PO SrNo"
        Me.GPOSRNO.Name = "GPOSRNO"
        Me.GPOSRNO.Width = 70
        '
        'GPOTYPE
        '
        Me.GPOTYPE.HeaderText = "PO Type"
        Me.GPOTYPE.Name = "GPOTYPE"
        Me.GPOTYPE.Width = 80
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
        'gQty
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.NullValue = Nothing
        Me.gQty.DefaultCellStyle = DataGridViewCellStyle13
        Me.gQty.HeaderText = "Qty"
        Me.gQty.Name = "gQty"
        Me.gQty.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gQty.Width = 55
        '
        'GFOLD
        '
        Me.GFOLD.HeaderText = "Fold %"
        Me.GFOLD.Name = "GFOLD"
        Me.GFOLD.Width = 55
        '
        'GDESC
        '
        Me.GDESC.HeaderText = "Description"
        Me.GDESC.Name = "GDESC"
        Me.GDESC.Width = 150
        '
        'GTRANS
        '
        Me.GTRANS.HeaderText = "Transport"
        Me.GTRANS.Name = "GTRANS"
        Me.GTRANS.Width = 200
        '
        'GLRNO
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GLRNO.DefaultCellStyle = DataGridViewCellStyle14
        Me.GLRNO.HeaderText = "LR"
        Me.GLRNO.Name = "GLRNO"
        Me.GLRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GLRNO.Width = 80
        '
        'GLRDATE
        '
        Me.GLRDATE.HeaderText = "LR Date"
        Me.GLRDATE.Name = "GLRDATE"
        Me.GLRDATE.Width = 87
        '
        'GBALENO
        '
        Me.GBALENO.HeaderText = "Bale No"
        Me.GBALENO.Name = "GBALENO"
        '
        'GPCS
        '
        Me.GPCS.HeaderText = "Pcs"
        Me.GPCS.Name = "GPCS"
        Me.GPCS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPCS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPCS.Width = 60
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
        'GPER
        '
        Me.GPER.HeaderText = "Per"
        Me.GPER.Name = "GPER"
        Me.GPER.Width = 50
        '
        'GAMT
        '
        Me.GAMT.HeaderText = "Amount"
        Me.GAMT.Name = "GAMT"
        Me.GAMT.Width = 65
        '
        'GCHARGES
        '
        Me.GCHARGES.HeaderText = "Charges"
        Me.GCHARGES.Name = "GCHARGES"
        Me.GCHARGES.Width = 65
        '
        'GSUBTOTAL
        '
        Me.GSUBTOTAL.HeaderText = "Subtotal"
        Me.GSUBTOTAL.Name = "GSUBTOTAL"
        Me.GSUBTOTAL.Width = 80
        '
        'GCGST
        '
        Me.GCGST.HeaderText = "CGST %"
        Me.GCGST.Name = "GCGST"
        Me.GCGST.Width = 60
        '
        'GCGSTAMT
        '
        Me.GCGSTAMT.HeaderText = "CGST Amt"
        Me.GCGSTAMT.Name = "GCGSTAMT"
        Me.GCGSTAMT.Width = 70
        '
        'GSGST
        '
        Me.GSGST.HeaderText = "SGST %"
        Me.GSGST.Name = "GSGST"
        Me.GSGST.Width = 60
        '
        'GSGSTAMT
        '
        Me.GSGSTAMT.HeaderText = "SGST Amt"
        Me.GSGSTAMT.Name = "GSGSTAMT"
        Me.GSGSTAMT.Width = 70
        '
        'GIGST
        '
        Me.GIGST.HeaderText = "IGST %"
        Me.GIGST.Name = "GIGST"
        Me.GIGST.Width = 60
        '
        'GIGSTAMT
        '
        Me.GIGSTAMT.HeaderText = "IGST Amt"
        Me.GIGSTAMT.Name = "GIGSTAMT"
        Me.GIGSTAMT.Width = 70
        '
        'GROUNDOFF
        '
        Me.GROUNDOFF.HeaderText = "Round Off"
        Me.GROUNDOFF.Name = "GROUNDOFF"
        Me.GROUNDOFF.Width = 80
        '
        'GGRANDTOTAL
        '
        Me.GGRANDTOTAL.HeaderText = "Grand Total"
        Me.GGRANDTOTAL.Name = "GGRANDTOTAL"
        '
        'GCOMPER
        '
        Me.GCOMPER.HeaderText = "Comm. %"
        Me.GCOMPER.Name = "GCOMPER"
        Me.GCOMPER.Width = 70
        '
        'GCOM
        '
        Me.GCOM.HeaderText = "Comm."
        Me.GCOM.Name = "GCOM"
        Me.GCOM.Width = 70
        '
        'GREMARKS
        '
        Me.GREMARKS.HeaderText = "Remarks"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GREMARKS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GREMARKS.Width = 200
        '
        'cmbitemname
        '
        Me.cmbitemname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbitemname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbitemname.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbitemname.DropDownWidth = 400
        Me.cmbitemname.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbitemname.FormattingEnabled = True
        Me.cmbitemname.Location = New System.Drawing.Point(970, 4)
        Me.cmbitemname.Name = "cmbitemname"
        Me.cmbitemname.Size = New System.Drawing.Size(200, 23)
        Me.cmbitemname.TabIndex = 9
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
        'txtcrdays
        '
        Me.txtcrdays.BackColor = System.Drawing.Color.White
        Me.txtcrdays.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcrdays.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcrdays.Location = New System.Drawing.Point(690, 4)
        Me.txtcrdays.Name = "txtcrdays"
        Me.txtcrdays.Size = New System.Drawing.Size(60, 23)
        Me.txtcrdays.TabIndex = 5
        Me.txtcrdays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTPARTYBILLNO
        '
        Me.TXTPARTYBILLNO.BackColor = System.Drawing.Color.White
        Me.TXTPARTYBILLNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTPARTYBILLNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPARTYBILLNO.Location = New System.Drawing.Point(609, 4)
        Me.TXTPARTYBILLNO.Name = "TXTPARTYBILLNO"
        Me.TXTPARTYBILLNO.Size = New System.Drawing.Size(81, 23)
        Me.TXTPARTYBILLNO.TabIndex = 4
        Me.TXTPARTYBILLNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBBUYERS
        '
        Me.CMBBUYERS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBBUYERS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBBUYERS.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBBUYERS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBBUYERS.FormattingEnabled = True
        Me.CMBBUYERS.Items.AddRange(New Object() {""})
        Me.CMBBUYERS.Location = New System.Drawing.Point(409, 4)
        Me.CMBBUYERS.Name = "CMBBUYERS"
        Me.CMBBUYERS.Size = New System.Drawing.Size(200, 23)
        Me.CMBBUYERS.TabIndex = 3
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
        Me.cmdclear.Location = New System.Drawing.Point(682, 621)
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
        Me.cmdEXIT.Location = New System.Drawing.Point(768, 621)
        Me.cmdEXIT.Name = "cmdEXIT"
        Me.cmdEXIT.Size = New System.Drawing.Size(80, 28)
        Me.cmdEXIT.TabIndex = 3
        Me.cmdEXIT.Text = "E&xit"
        Me.cmdEXIT.UseVisualStyleBackColor = False
        '
        'MagicBoxForInvoice
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "MagicBoxForInvoice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Magic Box For Invoice"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.TabControl2.ResumeLayout(False)
        Me.tbitem.ResumeLayout(False)
        Me.tbitem.PerformLayout()
        CType(Me.GRIDMAGICBOX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents tbitem As TabPage
    Friend WithEvents BILLDATE As DateTimePicker
    Friend WithEvents LRDATE As DateTimePicker
    Friend WithEvents TXTREMARKS As TextBox
    Friend WithEvents CMBSELLERS As ComboBox
    Friend WithEvents TXTPOSRNO As TextBox
    Friend WithEvents TXTPONO As TextBox
    Friend WithEvents TXTROUNDOFF As TextBox
    Friend WithEvents TXTGRANDTOTAL As TextBox
    Friend WithEvents GRIDMAGICBOX As DataGridView
    Friend WithEvents cmbitemname As ComboBox
    Friend WithEvents txtsrno As TextBox
    Friend WithEvents txtcrdays As TextBox
    Friend WithEvents TXTPARTYBILLNO As TextBox
    Friend WithEvents CMBBUYERS As ComboBox
    Friend WithEvents cmdOK As Button
    Friend WithEvents cmdclear As Button
    Friend WithEvents cmdEXIT As Button
    Friend WithEvents ENTRYDATE As DateTimePicker
    Friend WithEvents TXTFOLD As TextBox
    Friend WithEvents TXTQTY As TextBox
    Friend WithEvents TXTPOTYPE As TextBox
    Friend WithEvents TXTLR As TextBox
    Friend WithEvents CMBTRANS As ComboBox
    Friend WithEvents TXTDESC As TextBox
    Friend WithEvents TXTBALENO As TextBox
    Friend WithEvents TXTPCS As TextBox
    Friend WithEvents TXTMTRS As TextBox
    Friend WithEvents TXTRATES As TextBox
    Friend WithEvents TXTAMT As TextBox
    Friend WithEvents TXTCHRGS As TextBox
    Friend WithEvents TXTSUBTOTAL As TextBox
    Friend WithEvents TXTSGSTAMT As TextBox
    Friend WithEvents TXTIGSTAMT As TextBox
    Friend WithEvents TXTSGSTPER As TextBox
    Friend WithEvents TXTIGSTPER As TextBox
    Friend WithEvents TXTCGSTPER As TextBox
    Friend WithEvents TXTCGSTAMT As TextBox
    Friend WithEvents TXTCOMMPER As TextBox
    Friend WithEvents CMBPER As ComboBox
    Friend WithEvents CMBCOMM As ComboBox
    Friend WithEvents CMDSELECTPO As Button
    Friend WithEvents gsrno As DataGridViewTextBoxColumn
    Friend WithEvents GBILLDATE As DataGridViewTextBoxColumn
    Friend WithEvents GDATE As DataGridViewTextBoxColumn
    Friend WithEvents GSELLERS As DataGridViewTextBoxColumn
    Friend WithEvents GBUYERS As DataGridViewTextBoxColumn
    Friend WithEvents GNO As DataGridViewTextBoxColumn
    Friend WithEvents GCRDAYS As DataGridViewTextBoxColumn
    Friend WithEvents GPONO As DataGridViewTextBoxColumn
    Friend WithEvents GPOSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GPOTYPE As DataGridViewTextBoxColumn
    Friend WithEvents gitemname As DataGridViewTextBoxColumn
    Friend WithEvents gQty As DataGridViewTextBoxColumn
    Friend WithEvents GFOLD As DataGridViewTextBoxColumn
    Friend WithEvents GDESC As DataGridViewTextBoxColumn
    Friend WithEvents GTRANS As DataGridViewTextBoxColumn
    Friend WithEvents GLRNO As DataGridViewTextBoxColumn
    Friend WithEvents GLRDATE As DataGridViewTextBoxColumn
    Friend WithEvents GBALENO As DataGridViewTextBoxColumn
    Friend WithEvents GPCS As DataGridViewTextBoxColumn
    Friend WithEvents GMTRS As DataGridViewTextBoxColumn
    Friend WithEvents GRATE As DataGridViewTextBoxColumn
    Friend WithEvents GPER As DataGridViewTextBoxColumn
    Friend WithEvents GAMT As DataGridViewTextBoxColumn
    Friend WithEvents GCHARGES As DataGridViewTextBoxColumn
    Friend WithEvents GSUBTOTAL As DataGridViewTextBoxColumn
    Friend WithEvents GCGST As DataGridViewTextBoxColumn
    Friend WithEvents GCGSTAMT As DataGridViewTextBoxColumn
    Friend WithEvents GSGST As DataGridViewTextBoxColumn
    Friend WithEvents GSGSTAMT As DataGridViewTextBoxColumn
    Friend WithEvents GIGST As DataGridViewTextBoxColumn
    Friend WithEvents GIGSTAMT As DataGridViewTextBoxColumn
    Friend WithEvents GROUNDOFF As DataGridViewTextBoxColumn
    Friend WithEvents GGRANDTOTAL As DataGridViewTextBoxColumn
    Friend WithEvents GCOMPER As DataGridViewTextBoxColumn
    Friend WithEvents GCOM As DataGridViewTextBoxColumn
    Friend WithEvents GREMARKS As DataGridViewTextBoxColumn
End Class
