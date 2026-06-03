<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BeamJobOut
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BeamJobOut))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.LBLEINVGENERATED = New System.Windows.Forms.Label()
        Me.CMDSELECTYARNISSUE = New System.Windows.Forms.Button()
        Me.LBLTAPLINE = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CMBGODOWN = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.cmbcode = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.tstxtbillno = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.LBLTOTALMTRS = New System.Windows.Forms.Label()
        Me.LBLTOTALWT = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GRIDBEAM = New System.Windows.Forms.DataGridView()
        Me.GSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBEAMNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBEAMNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMILLNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTOTALENDS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTOTALMTRS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGAMANO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSECTION = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GROLLNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBREAKAGE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGRIDREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFROMNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFROMSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFROMTYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DTBEAMJODATE = New System.Windows.Forms.MaskedTextBox()
        Me.TXTBEAMJONO = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblgrndate = New System.Windows.Forms.Label()
        Me.PBlock = New System.Windows.Forms.PictureBox()
        Me.CMDDELETE = New System.Windows.Forms.Button()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.CMDSAVE = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.lbllocked = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.tooldelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.toolprevious = New System.Windows.Forms.ToolStripButton()
        Me.toolnext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.LBLPROCESS = New System.Windows.Forms.Label()
        Me.CMBPROCESS = New System.Windows.Forms.ComboBox()
        Me.BlendPanel1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.GRIDBEAM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.LBLPROCESS)
        Me.BlendPanel1.Controls.Add(Me.CMBPROCESS)
        Me.BlendPanel1.Controls.Add(Me.LBLEINVGENERATED)
        Me.BlendPanel1.Controls.Add(Me.CMDSELECTYARNISSUE)
        Me.BlendPanel1.Controls.Add(Me.LBLTAPLINE)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.CMBGODOWN)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.CMBNAME)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.cmbcode)
        Me.BlendPanel1.Controls.Add(Me.GroupBox5)
        Me.BlendPanel1.Controls.Add(Me.tstxtbillno)
        Me.BlendPanel1.Controls.Add(Me.TabControl1)
        Me.BlendPanel1.Controls.Add(Me.DTBEAMJODATE)
        Me.BlendPanel1.Controls.Add(Me.TXTBEAMJONO)
        Me.BlendPanel1.Controls.Add(Me.Label12)
        Me.BlendPanel1.Controls.Add(Me.lblgrndate)
        Me.BlendPanel1.Controls.Add(Me.PBlock)
        Me.BlendPanel1.Controls.Add(Me.CMDDELETE)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.CMDSAVE)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.lbllocked)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1238, 581)
        Me.BlendPanel1.TabIndex = 1
        '
        'LBLEINVGENERATED
        '
        Me.LBLEINVGENERATED.AutoSize = True
        Me.LBLEINVGENERATED.BackColor = System.Drawing.Color.Transparent
        Me.LBLEINVGENERATED.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLEINVGENERATED.ForeColor = System.Drawing.Color.Black
        Me.LBLEINVGENERATED.Location = New System.Drawing.Point(3, 25)
        Me.LBLEINVGENERATED.Name = "LBLEINVGENERATED"
        Me.LBLEINVGENERATED.Size = New System.Drawing.Size(151, 29)
        Me.LBLEINVGENERATED.TabIndex = 938
        Me.LBLEINVGENERATED.Text = "Beam Job Out"
        '
        'CMDSELECTYARNISSUE
        '
        Me.CMDSELECTYARNISSUE.BackColor = System.Drawing.Color.Transparent
        Me.CMDSELECTYARNISSUE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSELECTYARNISSUE.FlatAppearance.BorderSize = 0
        Me.CMDSELECTYARNISSUE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSELECTYARNISSUE.ForeColor = System.Drawing.Color.Black
        Me.CMDSELECTYARNISSUE.Location = New System.Drawing.Point(578, 481)
        Me.CMDSELECTYARNISSUE.Name = "CMDSELECTYARNISSUE"
        Me.CMDSELECTYARNISSUE.Size = New System.Drawing.Size(123, 28)
        Me.CMDSELECTYARNISSUE.TabIndex = 3
        Me.CMDSELECTYARNISSUE.Text = "&Select Beam Stock"
        Me.CMDSELECTYARNISSUE.UseVisualStyleBackColor = False
        '
        'LBLTAPLINE
        '
        Me.LBLTAPLINE.AutoSize = True
        Me.LBLTAPLINE.BackColor = System.Drawing.Color.Transparent
        Me.LBLTAPLINE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTAPLINE.ForeColor = System.Drawing.Color.Black
        Me.LBLTAPLINE.Location = New System.Drawing.Point(970, 474)
        Me.LBLTAPLINE.Name = "LBLTAPLINE"
        Me.LBLTAPLINE.Size = New System.Drawing.Size(70, 15)
        Me.LBLTAPLINE.TabIndex = 857
        Me.LBLTAPLINE.Text = "AVGTAPLINE"
        Me.LBLTAPLINE.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(13, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 15)
        Me.Label6.TabIndex = 851
        Me.Label6.Text = "Godown Name"
        '
        'CMBGODOWN
        '
        Me.CMBGODOWN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGODOWN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGODOWN.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBGODOWN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGODOWN.FormattingEnabled = True
        Me.CMBGODOWN.Location = New System.Drawing.Point(101, 60)
        Me.CMBGODOWN.MaxDropDownItems = 14
        Me.CMBGODOWN.Name = "CMBGODOWN"
        Me.CMBGODOWN.Size = New System.Drawing.Size(206, 23)
        Me.CMBGODOWN.TabIndex = 2
        Me.CMBGODOWN.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(22, 92)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 15)
        Me.Label5.TabIndex = 820
        Me.Label5.Text = "Jobber Name"
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(101, 88)
        Me.CMBNAME.MaxDropDownItems = 14
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(206, 23)
        Me.CMBNAME.TabIndex = 2
        '
        'TXTADD
        '
        Me.TXTADD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.Location = New System.Drawing.Point(1171, 31)
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(29, 23)
        Me.TXTADD.TabIndex = 0
        Me.TXTADD.TabStop = False
        Me.TXTADD.Visible = False
        '
        'cmbcode
        '
        Me.cmbcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbcode.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcode.FormattingEnabled = True
        Me.cmbcode.Location = New System.Drawing.Point(1206, 31)
        Me.cmbcode.Name = "cmbcode"
        Me.cmbcode.Size = New System.Drawing.Size(25, 23)
        Me.cmbcode.TabIndex = 649
        Me.cmbcode.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.TXTREMARKS)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(23, 465)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(268, 89)
        Me.GroupBox5.TabIndex = 14
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Remarks"
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.ForeColor = System.Drawing.Color.Black
        Me.TXTREMARKS.Location = New System.Drawing.Point(5, 16)
        Me.TXTREMARKS.MaxLength = 200
        Me.TXTREMARKS.Multiline = True
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(257, 65)
        Me.TXTREMARKS.TabIndex = 0
        '
        'tstxtbillno
        '
        Me.tstxtbillno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstxtbillno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstxtbillno.Location = New System.Drawing.Point(241, 2)
        Me.tstxtbillno.Name = "tstxtbillno"
        Me.tstxtbillno.Size = New System.Drawing.Size(61, 22)
        Me.tstxtbillno.TabIndex = 13
        Me.tstxtbillno.TabStop = False
        Me.tstxtbillno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(27, 153)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1195, 297)
        Me.TabControl1.TabIndex = 15
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.LBLTOTALMTRS)
        Me.TabPage1.Controls.Add(Me.LBLTOTALWT)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.GRIDBEAM)
        Me.TabPage1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1187, 269)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "1. Item Details"
        '
        'LBLTOTALMTRS
        '
        Me.LBLTOTALMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALMTRS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALMTRS.Location = New System.Drawing.Point(963, 234)
        Me.LBLTOTALMTRS.Name = "LBLTOTALMTRS"
        Me.LBLTOTALMTRS.Size = New System.Drawing.Size(58, 15)
        Me.LBLTOTALMTRS.TabIndex = 647
        Me.LBLTOTALMTRS.Text = "0"
        Me.LBLTOTALMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALWT
        '
        Me.LBLTOTALWT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALWT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALWT.Location = New System.Drawing.Point(1069, 234)
        Me.LBLTOTALWT.Name = "LBLTOTALWT"
        Me.LBLTOTALWT.Size = New System.Drawing.Size(58, 15)
        Me.LBLTOTALWT.TabIndex = 646
        Me.LBLTOTALWT.Text = "0"
        Me.LBLTOTALWT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(816, 234)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 15)
        Me.Label7.TabIndex = 645
        Me.Label7.Text = "Total"
        '
        'GRIDBEAM
        '
        Me.GRIDBEAM.AllowUserToAddRows = False
        Me.GRIDBEAM.AllowUserToDeleteRows = False
        Me.GRIDBEAM.AllowUserToResizeColumns = False
        Me.GRIDBEAM.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDBEAM.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDBEAM.BackgroundColor = System.Drawing.Color.White
        Me.GRIDBEAM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDBEAM.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDBEAM.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDBEAM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDBEAM.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GBEAMNO, Me.GBEAMNAME, Me.GMILLNAME, Me.GTOTALENDS, Me.GTOTALMTRS, Me.GWT, Me.GGAMANO, Me.GSECTION, Me.GROLLNO, Me.GBREAKAGE, Me.GGRIDREMARKS, Me.GFROMNO, Me.GFROMSRNO, Me.GFROMTYPE})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDBEAM.DefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDBEAM.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDBEAM.Location = New System.Drawing.Point(3, 4)
        Me.GRIDBEAM.MultiSelect = False
        Me.GRIDBEAM.Name = "GRIDBEAM"
        Me.GRIDBEAM.RowHeadersVisible = False
        Me.GRIDBEAM.RowHeadersWidth = 30
        Me.GRIDBEAM.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDBEAM.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDBEAM.RowTemplate.Height = 20
        Me.GRIDBEAM.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDBEAM.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDBEAM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDBEAM.Size = New System.Drawing.Size(1448, 230)
        Me.GRIDBEAM.TabIndex = 0
        Me.GRIDBEAM.TabStop = False
        '
        'GSRNO
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GSRNO.DefaultCellStyle = DataGridViewCellStyle3
        Me.GSRNO.HeaderText = "Sr."
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.ReadOnly = True
        Me.GSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSRNO.Width = 30
        '
        'GBEAMNO
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GBEAMNO.DefaultCellStyle = DataGridViewCellStyle4
        Me.GBEAMNO.HeaderText = "Beam No"
        Me.GBEAMNO.Name = "GBEAMNO"
        Me.GBEAMNO.ReadOnly = True
        Me.GBEAMNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBEAMNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBEAMNO.Width = 75
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
        'GMILLNAME
        '
        Me.GMILLNAME.HeaderText = "Mill Name"
        Me.GMILLNAME.Name = "GMILLNAME"
        Me.GMILLNAME.ReadOnly = True
        Me.GMILLNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMILLNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMILLNAME.Width = 200
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
        'GWT
        '
        Me.GWT.HeaderText = "Weight"
        Me.GWT.Name = "GWT"
        Me.GWT.ReadOnly = True
        Me.GWT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GWT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
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
        '
        'GBREAKAGE
        '
        Me.GBREAKAGE.HeaderText = "Breakage"
        Me.GBREAKAGE.Name = "GBREAKAGE"
        Me.GBREAKAGE.ReadOnly = True
        Me.GBREAKAGE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBREAKAGE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GGRIDREMARKS
        '
        Me.GGRIDREMARKS.HeaderText = "Remarks"
        Me.GGRIDREMARKS.Name = "GGRIDREMARKS"
        Me.GGRIDREMARKS.ReadOnly = True
        Me.GGRIDREMARKS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGRIDREMARKS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GGRIDREMARKS.Width = 200
        '
        'GFROMNO
        '
        Me.GFROMNO.HeaderText = "From No"
        Me.GFROMNO.Name = "GFROMNO"
        Me.GFROMNO.ReadOnly = True
        Me.GFROMNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GFROMNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GFROMNO.Visible = False
        '
        'GFROMSRNO
        '
        Me.GFROMSRNO.HeaderText = "From Sr No"
        Me.GFROMSRNO.Name = "GFROMSRNO"
        Me.GFROMSRNO.ReadOnly = True
        Me.GFROMSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GFROMSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GFROMSRNO.Visible = False
        '
        'GFROMTYPE
        '
        Me.GFROMTYPE.HeaderText = "From Type"
        Me.GFROMTYPE.Name = "GFROMTYPE"
        Me.GFROMTYPE.ReadOnly = True
        Me.GFROMTYPE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GFROMTYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GFROMTYPE.Visible = False
        '
        'DTBEAMJODATE
        '
        Me.DTBEAMJODATE.AsciiOnly = True
        Me.DTBEAMJODATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.DTBEAMJODATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTBEAMJODATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTBEAMJODATE.Location = New System.Drawing.Point(1134, 88)
        Me.DTBEAMJODATE.Mask = "00/00/0000"
        Me.DTBEAMJODATE.Name = "DTBEAMJODATE"
        Me.DTBEAMJODATE.Size = New System.Drawing.Size(79, 23)
        Me.DTBEAMJODATE.TabIndex = 1
        Me.DTBEAMJODATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTBEAMJODATE.ValidatingType = GetType(Date)
        '
        'TXTBEAMJONO
        '
        Me.TXTBEAMJONO.BackColor = System.Drawing.Color.Linen
        Me.TXTBEAMJONO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBEAMJONO.Location = New System.Drawing.Point(1134, 60)
        Me.TXTBEAMJONO.Name = "TXTBEAMJONO"
        Me.TXTBEAMJONO.ReadOnly = True
        Me.TXTBEAMJONO.Size = New System.Drawing.Size(79, 23)
        Me.TXTBEAMJONO.TabIndex = 0
        Me.TXTBEAMJONO.TabStop = False
        Me.TXTBEAMJONO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(1052, 64)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(80, 15)
        Me.Label12.TabIndex = 634
        Me.Label12.Text = "Beam Rec. No"
        '
        'lblgrndate
        '
        Me.lblgrndate.AutoSize = True
        Me.lblgrndate.BackColor = System.Drawing.Color.Transparent
        Me.lblgrndate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgrndate.ForeColor = System.Drawing.Color.Black
        Me.lblgrndate.Location = New System.Drawing.Point(1100, 92)
        Me.lblgrndate.Name = "lblgrndate"
        Me.lblgrndate.Size = New System.Drawing.Size(32, 15)
        Me.lblgrndate.TabIndex = 632
        Me.lblgrndate.Text = "Date"
        '
        'PBlock
        '
        Me.PBlock.BackColor = System.Drawing.Color.Transparent
        Me.PBlock.Image = Global.TEXTRADE.My.Resources.Resources.lock_copy
        Me.PBlock.Location = New System.Drawing.Point(1052, 474)
        Me.PBlock.Name = "PBlock"
        Me.PBlock.Size = New System.Drawing.Size(60, 60)
        Me.PBlock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBlock.TabIndex = 453
        Me.PBlock.TabStop = False
        Me.PBlock.Visible = False
        '
        'CMDDELETE
        '
        Me.CMDDELETE.BackColor = System.Drawing.Color.Transparent
        Me.CMDDELETE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDDELETE.FlatAppearance.BorderSize = 0
        Me.CMDDELETE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDELETE.ForeColor = System.Drawing.Color.Black
        Me.CMDDELETE.Location = New System.Drawing.Point(642, 514)
        Me.CMDDELETE.Name = "CMDDELETE"
        Me.CMDDELETE.Size = New System.Drawing.Size(80, 28)
        Me.CMDDELETE.TabIndex = 17
        Me.CMDDELETE.Text = "&Delete"
        Me.CMDDELETE.UseVisualStyleBackColor = False
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDCLEAR.FlatAppearance.BorderSize = 0
        Me.CMDCLEAR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.Black
        Me.CMDCLEAR.Location = New System.Drawing.Point(556, 514)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 16
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
        '
        'CMDSAVE
        '
        Me.CMDSAVE.BackColor = System.Drawing.Color.Transparent
        Me.CMDSAVE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSAVE.FlatAppearance.BorderSize = 0
        Me.CMDSAVE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSAVE.ForeColor = System.Drawing.Color.Black
        Me.CMDSAVE.Location = New System.Drawing.Point(470, 514)
        Me.CMDSAVE.Name = "CMDSAVE"
        Me.CMDSAVE.Size = New System.Drawing.Size(80, 28)
        Me.CMDSAVE.TabIndex = 15
        Me.CMDSAVE.Text = "&Save"
        Me.CMDSAVE.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(728, 514)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 18
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'lbllocked
        '
        Me.lbllocked.AutoSize = True
        Me.lbllocked.BackColor = System.Drawing.Color.Transparent
        Me.lbllocked.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllocked.ForeColor = System.Drawing.Color.Red
        Me.lbllocked.Location = New System.Drawing.Point(1118, 505)
        Me.lbllocked.Name = "lbllocked"
        Me.lbllocked.Size = New System.Drawing.Size(82, 29)
        Me.lbllocked.TabIndex = 452
        Me.lbllocked.Text = "Locked"
        Me.lbllocked.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.tooldelete, Me.toolStripSeparator, Me.toolprevious, Me.toolnext, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1238, 25)
        Me.ToolStrip1.TabIndex = 868
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
        Me.toolprevious.Image = Global.TEXTRADE.My.Resources.Resources.POINT021
        Me.toolprevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolprevious.Name = "toolprevious"
        Me.toolprevious.Size = New System.Drawing.Size(73, 22)
        Me.toolprevious.Text = "Previous"
        '
        'toolnext
        '
        Me.toolnext.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'LBLPROCESS
        '
        Me.LBLPROCESS.AutoSize = True
        Me.LBLPROCESS.BackColor = System.Drawing.Color.Transparent
        Me.LBLPROCESS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLPROCESS.ForeColor = System.Drawing.Color.Black
        Me.LBLPROCESS.Location = New System.Drawing.Point(14, 121)
        Me.LBLPROCESS.Name = "LBLPROCESS"
        Me.LBLPROCESS.Size = New System.Drawing.Size(84, 15)
        Me.LBLPROCESS.TabIndex = 940
        Me.LBLPROCESS.Text = "Process Name"
        '
        'CMBPROCESS
        '
        Me.CMBPROCESS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBPROCESS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBPROCESS.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBPROCESS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBPROCESS.FormattingEnabled = True
        Me.CMBPROCESS.Location = New System.Drawing.Point(101, 117)
        Me.CMBPROCESS.MaxDropDownItems = 14
        Me.CMBPROCESS.Name = "CMBPROCESS"
        Me.CMBPROCESS.Size = New System.Drawing.Size(217, 23)
        Me.CMBPROCESS.TabIndex = 939
        '
        'BeamJobOut
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1238, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.KeyPreview = True
        Me.Name = "BeamJobOut"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Beam Job Out"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.GRIDBEAM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBlock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents LBLEINVGENERATED As Label
    Friend WithEvents CMDSELECTYARNISSUE As Button
    Friend WithEvents LBLTAPLINE As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents CMBNAME As ComboBox
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents cmbcode As ComboBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents TXTREMARKS As TextBox
    Friend WithEvents tstxtbillno As TextBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents LBLTOTALMTRS As Label
    Friend WithEvents LBLTOTALWT As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents GRIDBEAM As DataGridView
    Friend WithEvents DTBEAMJODATE As MaskedTextBox
    Friend WithEvents TXTBEAMJONO As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents lblgrndate As Label
    Friend WithEvents PBlock As PictureBox
    Friend WithEvents CMDDELETE As Button
    Friend WithEvents CMDCLEAR As Button
    Friend WithEvents CMDSAVE As Button
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents lbllocked As Label
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents SaveToolStripButton As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents tooldelete As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents toolprevious As ToolStripButton
    Friend WithEvents toolnext As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents CMBGODOWN As ComboBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents GSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GBEAMNO As DataGridViewTextBoxColumn
    Friend WithEvents GBEAMNAME As DataGridViewTextBoxColumn
    Friend WithEvents GMILLNAME As DataGridViewTextBoxColumn
    Friend WithEvents GTOTALENDS As DataGridViewTextBoxColumn
    Friend WithEvents GTOTALMTRS As DataGridViewTextBoxColumn
    Friend WithEvents GWT As DataGridViewTextBoxColumn
    Friend WithEvents GGAMANO As DataGridViewTextBoxColumn
    Friend WithEvents GSECTION As DataGridViewTextBoxColumn
    Friend WithEvents GROLLNO As DataGridViewTextBoxColumn
    Friend WithEvents GBREAKAGE As DataGridViewTextBoxColumn
    Friend WithEvents GGRIDREMARKS As DataGridViewTextBoxColumn
    Friend WithEvents GFROMNO As DataGridViewTextBoxColumn
    Friend WithEvents GFROMSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GFROMTYPE As DataGridViewTextBoxColumn
    Friend WithEvents LBLPROCESS As Label
    Friend WithEvents CMBPROCESS As ComboBox
End Class
