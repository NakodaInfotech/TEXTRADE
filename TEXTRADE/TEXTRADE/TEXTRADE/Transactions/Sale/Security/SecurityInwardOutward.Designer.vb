<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SecurityInwardOutward
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SecurityInwardOutward))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.TXTWT = New System.Windows.Forms.TextBox()
        Me.TXTQUANTITY = New System.Windows.Forms.TextBox()
        Me.TXTMATRERIAL = New System.Windows.Forms.TextBox()
        Me.TXTSECNO = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbname = New System.Windows.Forms.ComboBox()
        Me.tstxtbillno = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.tooldelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.toolprevious = New System.Windows.Forms.ToolStripButton()
        Me.toolnext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GBIMAGE = New System.Windows.Forms.GroupBox()
        Me.TXTUPLOADPATH = New System.Windows.Forms.TextBox()
        Me.CMDRMV = New System.Windows.Forms.Button()
        Me.TXTPHOTOIMAGEUPLOADPATH = New System.Windows.Forms.TextBox()
        Me.GRIDUPLOADDESC = New System.Windows.Forms.DataGridView()
        Me.DSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DIMGPATH = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DMAINSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DIMAGEUPLOADPATH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CMDREMOVE = New System.Windows.Forms.Button()
        Me.TXTUPLOADSRNO = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CMDVIEW = New System.Windows.Forms.Button()
        Me.gridupload = New System.Windows.Forms.DataGridView()
        Me.GGRIDUPLOADSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GIMGPATH = New System.Windows.Forms.DataGridViewImageColumn()
        Me.GQCSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GIMAGEUPLOADPATH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtuploadname = New System.Windows.Forms.TextBox()
        Me.PBIMG = New System.Windows.Forms.PictureBox()
        Me.cmdupload = New System.Windows.Forms.Button()
        Me.TXTVEHICLENO = New System.Windows.Forms.TextBox()
        Me.DTSECDATE = New System.Windows.Forms.MaskedTextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TXTQTY = New System.Windows.Forms.TextBox()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.PRINTDOC = New System.Drawing.Printing.PrintDocument()
        Me.PRINTDIALOG = New System.Windows.Forms.PrintDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.BlendPanel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GBIMAGE.SuspendLayout()
        CType(Me.GRIDUPLOADDESC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridupload, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBIMG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdok.Location = New System.Drawing.Point(159, 486)
        Me.cmdok.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(93, 32)
        Me.cmdok.TabIndex = 4
        Me.cmdok.Text = "&Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.Cursor = System.Windows.Forms.Cursors.Default
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDCLEAR.Location = New System.Drawing.Point(259, 486)
        Me.CMDCLEAR.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(93, 32)
        Me.CMDCLEAR.TabIndex = 5
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmddelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmddelete.Location = New System.Drawing.Point(360, 486)
        Me.cmddelete.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(93, 32)
        Me.cmddelete.TabIndex = 6
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdexit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdexit.Location = New System.Drawing.Point(460, 486)
        Me.cmdexit.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(93, 32)
        Me.cmdexit.TabIndex = 7
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTWT)
        Me.BlendPanel1.Controls.Add(Me.TXTQUANTITY)
        Me.BlendPanel1.Controls.Add(Me.TXTMATRERIAL)
        Me.BlendPanel1.Controls.Add(Me.TXTSECNO)
        Me.BlendPanel1.Controls.Add(Me.Label12)
        Me.BlendPanel1.Controls.Add(Me.cmbname)
        Me.BlendPanel1.Controls.Add(Me.tstxtbillno)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.GroupBox5)
        Me.BlendPanel1.Controls.Add(Me.Label7)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.GBIMAGE)
        Me.BlendPanel1.Controls.Add(Me.TXTVEHICLENO)
        Me.BlendPanel1.Controls.Add(Me.DTSECDATE)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(748, 561)
        Me.BlendPanel1.TabIndex = 1
        '
        'TXTWT
        '
        Me.TXTWT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTWT.Location = New System.Drawing.Point(390, 32)
        Me.TXTWT.MaxLength = 50
        Me.TXTWT.Name = "TXTWT"
        Me.TXTWT.Size = New System.Drawing.Size(95, 22)
        Me.TXTWT.TabIndex = 652
        Me.TXTWT.TabStop = False
        '
        'TXTQUANTITY
        '
        Me.TXTQUANTITY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTQUANTITY.Location = New System.Drawing.Point(96, 89)
        Me.TXTQUANTITY.MaxLength = 50
        Me.TXTQUANTITY.Name = "TXTQUANTITY"
        Me.TXTQUANTITY.Size = New System.Drawing.Size(95, 22)
        Me.TXTQUANTITY.TabIndex = 651
        Me.TXTQUANTITY.TabStop = False
        '
        'TXTMATRERIAL
        '
        Me.TXTMATRERIAL.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTMATRERIAL.Location = New System.Drawing.Point(96, 61)
        Me.TXTMATRERIAL.MaxLength = 50
        Me.TXTMATRERIAL.Name = "TXTMATRERIAL"
        Me.TXTMATRERIAL.Size = New System.Drawing.Size(215, 22)
        Me.TXTMATRERIAL.TabIndex = 650
        Me.TXTMATRERIAL.TabStop = False
        '
        'TXTSECNO
        '
        Me.TXTSECNO.BackColor = System.Drawing.Color.Linen
        Me.TXTSECNO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSECNO.Location = New System.Drawing.Point(552, 32)
        Me.TXTSECNO.Name = "TXTSECNO"
        Me.TXTSECNO.ReadOnly = True
        Me.TXTSECNO.Size = New System.Drawing.Size(84, 22)
        Me.TXTSECNO.TabIndex = 648
        Me.TXTSECNO.TabStop = False
        Me.TXTSECNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(512, 35)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(37, 14)
        Me.Label12.TabIndex = 649
        Me.Label12.Text = "Sr. No"
        '
        'cmbname
        '
        Me.cmbname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbname.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbname.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbname.FormattingEnabled = True
        Me.cmbname.Location = New System.Drawing.Point(96, 32)
        Me.cmbname.MaxDropDownItems = 14
        Me.cmbname.Name = "cmbname"
        Me.cmbname.Size = New System.Drawing.Size(208, 22)
        Me.cmbname.TabIndex = 647
        '
        'tstxtbillno
        '
        Me.tstxtbillno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstxtbillno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstxtbillno.Location = New System.Drawing.Point(272, 1)
        Me.tstxtbillno.Name = "tstxtbillno"
        Me.tstxtbillno.Size = New System.Drawing.Size(45, 22)
        Me.tstxtbillno.TabIndex = 646
        Me.tstxtbillno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.tooldelete, Me.toolStripSeparator, Me.toolprevious, Me.toolnext, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(748, 25)
        Me.ToolStrip1.TabIndex = 645
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
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.txtremarks)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(27, 383)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(236, 93)
        Me.GroupBox5.TabIndex = 644
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Remarks"
        '
        'txtremarks
        '
        Me.txtremarks.ForeColor = System.Drawing.Color.DimGray
        Me.txtremarks.Location = New System.Drawing.Point(5, 16)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(224, 71)
        Me.txtremarks.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(323, 64)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 14)
        Me.Label7.TabIndex = 643
        Me.Label7.Text = "Vehicle No"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(517, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 14)
        Me.Label6.TabIndex = 642
        Me.Label6.Text = "Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(66, 92)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 14)
        Me.Label4.TabIndex = 641
        Me.Label4.Text = "Qty"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(342, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 14)
        Me.Label3.TabIndex = 640
        Me.Label3.Text = "Weight"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(41, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 14)
        Me.Label2.TabIndex = 639
        Me.Label2.Text = "Material"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(4, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 14)
        Me.Label1.TabIndex = 638
        Me.Label1.Text = "Company Name"
        '
        'GBIMAGE
        '
        Me.GBIMAGE.BackColor = System.Drawing.Color.Transparent
        Me.GBIMAGE.Controls.Add(Me.TXTUPLOADPATH)
        Me.GBIMAGE.Controls.Add(Me.CMDRMV)
        Me.GBIMAGE.Controls.Add(Me.TXTPHOTOIMAGEUPLOADPATH)
        Me.GBIMAGE.Controls.Add(Me.GRIDUPLOADDESC)
        Me.GBIMAGE.Controls.Add(Me.CMDREMOVE)
        Me.GBIMAGE.Controls.Add(Me.TXTUPLOADSRNO)
        Me.GBIMAGE.Controls.Add(Me.Label5)
        Me.GBIMAGE.Controls.Add(Me.CMDVIEW)
        Me.GBIMAGE.Controls.Add(Me.gridupload)
        Me.GBIMAGE.Controls.Add(Me.txtuploadname)
        Me.GBIMAGE.Controls.Add(Me.PBIMG)
        Me.GBIMAGE.Controls.Add(Me.cmdupload)
        Me.GBIMAGE.Location = New System.Drawing.Point(14, 118)
        Me.GBIMAGE.Name = "GBIMAGE"
        Me.GBIMAGE.Size = New System.Drawing.Size(605, 255)
        Me.GBIMAGE.TabIndex = 30
        Me.GBIMAGE.TabStop = False
        Me.GBIMAGE.Text = "Images"
        '
        'TXTUPLOADPATH
        '
        Me.TXTUPLOADPATH.BackColor = System.Drawing.Color.Linen
        Me.TXTUPLOADPATH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTUPLOADPATH.ForeColor = System.Drawing.Color.Black
        Me.TXTUPLOADPATH.Location = New System.Drawing.Point(275, 21)
        Me.TXTUPLOADPATH.MaxLength = 100
        Me.TXTUPLOADPATH.Name = "TXTUPLOADPATH"
        Me.TXTUPLOADPATH.ReadOnly = True
        Me.TXTUPLOADPATH.Size = New System.Drawing.Size(99, 23)
        Me.TXTUPLOADPATH.TabIndex = 1088
        Me.TXTUPLOADPATH.TabStop = False
        Me.TXTUPLOADPATH.Visible = False
        '
        'CMDRMV
        '
        Me.CMDRMV.BackColor = System.Drawing.Color.Transparent
        Me.CMDRMV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDRMV.FlatAppearance.BorderSize = 0
        Me.CMDRMV.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDRMV.ForeColor = System.Drawing.Color.Black
        Me.CMDRMV.Location = New System.Drawing.Point(453, 145)
        Me.CMDRMV.Name = "CMDRMV"
        Me.CMDRMV.Size = New System.Drawing.Size(91, 28)
        Me.CMDRMV.TabIndex = 640
        Me.CMDRMV.Text = "Remove Entry"
        Me.CMDRMV.UseVisualStyleBackColor = False
        '
        'TXTPHOTOIMAGEUPLOADPATH
        '
        Me.TXTPHOTOIMAGEUPLOADPATH.BackColor = System.Drawing.Color.Linen
        Me.TXTPHOTOIMAGEUPLOADPATH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPHOTOIMAGEUPLOADPATH.ForeColor = System.Drawing.Color.Black
        Me.TXTPHOTOIMAGEUPLOADPATH.Location = New System.Drawing.Point(378, 21)
        Me.TXTPHOTOIMAGEUPLOADPATH.MaxLength = 100
        Me.TXTPHOTOIMAGEUPLOADPATH.Name = "TXTPHOTOIMAGEUPLOADPATH"
        Me.TXTPHOTOIMAGEUPLOADPATH.ReadOnly = True
        Me.TXTPHOTOIMAGEUPLOADPATH.Size = New System.Drawing.Size(218, 23)
        Me.TXTPHOTOIMAGEUPLOADPATH.TabIndex = 639
        Me.TXTPHOTOIMAGEUPLOADPATH.TabStop = False
        '
        'GRIDUPLOADDESC
        '
        Me.GRIDUPLOADDESC.AllowUserToAddRows = False
        Me.GRIDUPLOADDESC.AllowUserToDeleteRows = False
        Me.GRIDUPLOADDESC.AllowUserToResizeColumns = False
        Me.GRIDUPLOADDESC.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDUPLOADDESC.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDUPLOADDESC.BackgroundColor = System.Drawing.Color.White
        Me.GRIDUPLOADDESC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDUPLOADDESC.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDUPLOADDESC.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDUPLOADDESC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDUPLOADDESC.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DSRNO, Me.DNAME, Me.DIMGPATH, Me.DMAINSRNO, Me.DIMAGEUPLOADPATH})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDUPLOADDESC.DefaultCellStyle = DataGridViewCellStyle3
        Me.GRIDUPLOADDESC.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GRIDUPLOADDESC.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDUPLOADDESC.Location = New System.Drawing.Point(6, 187)
        Me.GRIDUPLOADDESC.MultiSelect = False
        Me.GRIDUPLOADDESC.Name = "GRIDUPLOADDESC"
        Me.GRIDUPLOADDESC.RowHeadersVisible = False
        Me.GRIDUPLOADDESC.RowHeadersWidth = 30
        Me.GRIDUPLOADDESC.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDUPLOADDESC.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.GRIDUPLOADDESC.RowTemplate.Height = 20
        Me.GRIDUPLOADDESC.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDUPLOADDESC.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDUPLOADDESC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDUPLOADDESC.Size = New System.Drawing.Size(314, 33)
        Me.GRIDUPLOADDESC.TabIndex = 548
        Me.GRIDUPLOADDESC.TabStop = False
        Me.GRIDUPLOADDESC.Visible = False
        '
        'DSRNO
        '
        Me.DSRNO.HeaderText = "Sr."
        Me.DSRNO.Name = "DSRNO"
        Me.DSRNO.ReadOnly = True
        Me.DSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DSRNO.Width = 30
        '
        'DNAME
        '
        Me.DNAME.HeaderText = "Name"
        Me.DNAME.Name = "DNAME"
        Me.DNAME.Width = 200
        '
        'DIMGPATH
        '
        Me.DIMGPATH.HeaderText = "ImgPath"
        Me.DIMGPATH.Name = "DIMGPATH"
        Me.DIMGPATH.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DIMGPATH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DIMGPATH.Visible = False
        '
        'DMAINSRNO
        '
        Me.DMAINSRNO.HeaderText = "MAINSRNO"
        Me.DMAINSRNO.Name = "DMAINSRNO"
        '
        'DIMAGEUPLOADPATH
        '
        Me.DIMAGEUPLOADPATH.HeaderText = "IMAGEUPLOADPATH"
        Me.DIMAGEUPLOADPATH.Name = "DIMAGEUPLOADPATH"
        Me.DIMAGEUPLOADPATH.Visible = False
        '
        'CMDREMOVE
        '
        Me.CMDREMOVE.BackColor = System.Drawing.Color.Transparent
        Me.CMDREMOVE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREMOVE.FlatAppearance.BorderSize = 0
        Me.CMDREMOVE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREMOVE.ForeColor = System.Drawing.Color.Black
        Me.CMDREMOVE.Location = New System.Drawing.Point(500, 55)
        Me.CMDREMOVE.Name = "CMDREMOVE"
        Me.CMDREMOVE.Size = New System.Drawing.Size(80, 28)
        Me.CMDREMOVE.TabIndex = 4
        Me.CMDREMOVE.Text = "&Remove"
        Me.CMDREMOVE.UseVisualStyleBackColor = False
        '
        'TXTUPLOADSRNO
        '
        Me.TXTUPLOADSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTUPLOADSRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTUPLOADSRNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTUPLOADSRNO.Location = New System.Drawing.Point(6, 21)
        Me.TXTUPLOADSRNO.Name = "TXTUPLOADSRNO"
        Me.TXTUPLOADSRNO.ReadOnly = True
        Me.TXTUPLOADSRNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTUPLOADSRNO.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(443, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(110, 14)
        Me.Label5.TabIndex = 541
        Me.Label5.Text = "Upload Soft Copies"
        '
        'CMDVIEW
        '
        Me.CMDVIEW.BackColor = System.Drawing.Color.Transparent
        Me.CMDVIEW.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDVIEW.FlatAppearance.BorderSize = 0
        Me.CMDVIEW.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDVIEW.ForeColor = System.Drawing.Color.Black
        Me.CMDVIEW.Location = New System.Drawing.Point(458, 89)
        Me.CMDVIEW.Name = "CMDVIEW"
        Me.CMDVIEW.Size = New System.Drawing.Size(80, 28)
        Me.CMDVIEW.TabIndex = 5
        Me.CMDVIEW.Text = "&View"
        Me.CMDVIEW.UseVisualStyleBackColor = False
        '
        'gridupload
        '
        Me.gridupload.AllowUserToAddRows = False
        Me.gridupload.AllowUserToDeleteRows = False
        Me.gridupload.AllowUserToResizeColumns = False
        Me.gridupload.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Black
        Me.gridupload.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.gridupload.BackgroundColor = System.Drawing.Color.White
        Me.gridupload.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.gridupload.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.gridupload.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.gridupload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridupload.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GGRIDUPLOADSRNO, Me.GNAME, Me.GIMGPATH, Me.GQCSRNO, Me.GIMAGEUPLOADPATH})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridupload.DefaultCellStyle = DataGridViewCellStyle7
        Me.gridupload.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.gridupload.GridColor = System.Drawing.SystemColors.Control
        Me.gridupload.Location = New System.Drawing.Point(6, 43)
        Me.gridupload.MultiSelect = False
        Me.gridupload.Name = "gridupload"
        Me.gridupload.RowHeadersVisible = False
        Me.gridupload.RowHeadersWidth = 30
        Me.gridupload.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White
        Me.gridupload.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.gridupload.RowTemplate.Height = 20
        Me.gridupload.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridupload.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.gridupload.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridupload.Size = New System.Drawing.Size(267, 120)
        Me.gridupload.TabIndex = 2
        Me.gridupload.TabStop = False
        '
        'GGRIDUPLOADSRNO
        '
        Me.GGRIDUPLOADSRNO.HeaderText = "Sr."
        Me.GGRIDUPLOADSRNO.Name = "GGRIDUPLOADSRNO"
        Me.GGRIDUPLOADSRNO.ReadOnly = True
        Me.GGRIDUPLOADSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGRIDUPLOADSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GGRIDUPLOADSRNO.Width = 30
        '
        'GNAME
        '
        Me.GNAME.HeaderText = "Name"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Width = 200
        '
        'GIMGPATH
        '
        Me.GIMGPATH.HeaderText = "ImgPath"
        Me.GIMGPATH.Name = "GIMGPATH"
        Me.GIMGPATH.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GIMGPATH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.GIMGPATH.Visible = False
        '
        'GQCSRNO
        '
        Me.GQCSRNO.HeaderText = "QCGRIDSRNO"
        Me.GQCSRNO.Name = "GQCSRNO"
        Me.GQCSRNO.Visible = False
        '
        'GIMAGEUPLOADPATH
        '
        Me.GIMAGEUPLOADPATH.HeaderText = "IMAGEUPLOADPATH"
        Me.GIMAGEUPLOADPATH.Name = "GIMAGEUPLOADPATH"
        Me.GIMAGEUPLOADPATH.Visible = False
        '
        'txtuploadname
        '
        Me.txtuploadname.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtuploadname.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtuploadname.Location = New System.Drawing.Point(36, 21)
        Me.txtuploadname.Name = "txtuploadname"
        Me.txtuploadname.ReadOnly = True
        Me.txtuploadname.Size = New System.Drawing.Size(200, 23)
        Me.txtuploadname.TabIndex = 1
        '
        'PBIMG
        '
        Me.PBIMG.BackColor = System.Drawing.Color.Transparent
        Me.PBIMG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBIMG.Location = New System.Drawing.Point(296, 51)
        Me.PBIMG.Name = "PBIMG"
        Me.PBIMG.Size = New System.Drawing.Size(97, 103)
        Me.PBIMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBIMG.TabIndex = 540
        Me.PBIMG.TabStop = False
        '
        'cmdupload
        '
        Me.cmdupload.BackColor = System.Drawing.Color.Transparent
        Me.cmdupload.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdupload.FlatAppearance.BorderSize = 0
        Me.cmdupload.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdupload.ForeColor = System.Drawing.Color.Black
        Me.cmdupload.Location = New System.Drawing.Point(414, 55)
        Me.cmdupload.Name = "cmdupload"
        Me.cmdupload.Size = New System.Drawing.Size(80, 28)
        Me.cmdupload.TabIndex = 3
        Me.cmdupload.Text = "&Upload"
        Me.cmdupload.UseVisualStyleBackColor = False
        '
        'TXTVEHICLENO
        '
        Me.TXTVEHICLENO.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTVEHICLENO.Location = New System.Drawing.Point(390, 60)
        Me.TXTVEHICLENO.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TXTVEHICLENO.Name = "TXTVEHICLENO"
        Me.TXTVEHICLENO.Size = New System.Drawing.Size(116, 23)
        Me.TXTVEHICLENO.TabIndex = 28
        '
        'DTSECDATE
        '
        Me.DTSECDATE.AsciiOnly = True
        Me.DTSECDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.DTSECDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.DTSECDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTSECDATE.Location = New System.Drawing.Point(552, 60)
        Me.DTSECDATE.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DTSECDATE.Mask = "00/00/0000"
        Me.DTSECDATE.Name = "DTSECDATE"
        Me.DTSECDATE.Size = New System.Drawing.Size(104, 23)
        Me.DTSECDATE.TabIndex = 22
        Me.DTSECDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTSECDATE.ValidatingType = GetType(Date)
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox1.Location = New System.Drawing.Point(121, 20)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(116, 20)
        Me.TextBox1.TabIndex = 9
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox2.Location = New System.Drawing.Point(121, 50)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(116, 20)
        Me.TextBox2.TabIndex = 10
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox3.Location = New System.Drawing.Point(331, 20)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(116, 20)
        Me.TextBox3.TabIndex = 11
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox5.Location = New System.Drawing.Point(331, 53)
        Me.TextBox5.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(116, 20)
        Me.TextBox5.TabIndex = 25
        '
        'TextBox6
        '
        Me.TextBox6.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox6.Location = New System.Drawing.Point(597, 50)
        Me.TextBox6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(116, 20)
        Me.TextBox6.TabIndex = 28
        '
        'TXTQTY
        '
        Me.TXTQTY.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTQTY.Location = New System.Drawing.Point(82, 72)
        Me.TXTQTY.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TXTQTY.Name = "TXTQTY"
        Me.TXTQTY.Size = New System.Drawing.Size(116, 20)
        Me.TXTQTY.TabIndex = 11
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'PRINTDIALOG
        '
        Me.PRINTDIALOG.AllowSelection = True
        Me.PRINTDIALOG.AllowSomePages = True
        Me.PRINTDIALOG.ShowHelp = True
        Me.PRINTDIALOG.UseEXDialog = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'SecurityInwardOutward
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(748, 561)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "SecurityInwardOutward"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Security Inward Outward"
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GBIMAGE.ResumeLayout(False)
        Me.GBIMAGE.PerformLayout()
        CType(Me.GRIDUPLOADDESC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridupload, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBIMG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdok As Button
    Friend WithEvents CMDCLEAR As Button
    Friend WithEvents cmddelete As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents DTSECDATE As MaskedTextBox
    Friend WithEvents TXTVEHICLENO As TextBox
    Friend WithEvents GBIMAGE As GroupBox
    Friend WithEvents CMDRMV As Button
    Friend WithEvents TXTPHOTOIMAGEUPLOADPATH As TextBox
    Friend WithEvents GRIDUPLOADDESC As DataGridView
    Friend WithEvents DSRNO As DataGridViewTextBoxColumn
    Friend WithEvents DNAME As DataGridViewTextBoxColumn
    Friend WithEvents DIMGPATH As DataGridViewImageColumn
    Friend WithEvents DMAINSRNO As DataGridViewTextBoxColumn
    Friend WithEvents DIMAGEUPLOADPATH As DataGridViewTextBoxColumn
    Friend WithEvents CMDREMOVE As Button
    Friend WithEvents TXTUPLOADSRNO As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents CMDVIEW As Button
    Friend WithEvents gridupload As DataGridView
    Friend WithEvents GGRIDUPLOADSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GNAME As DataGridViewTextBoxColumn
    Friend WithEvents GIMGPATH As DataGridViewImageColumn
    Friend WithEvents GQCSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GIMAGEUPLOADPATH As DataGridViewTextBoxColumn
    Friend WithEvents txtuploadname As TextBox
    Friend WithEvents PBIMG As PictureBox
    Friend WithEvents cmdupload As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TXTQTY As TextBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents txtremarks As TextBox
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents SaveToolStripButton As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents tooldelete As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents toolprevious As ToolStripButton
    Friend WithEvents toolnext As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tstxtbillno As TextBox
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents cmbname As ComboBox
    Friend WithEvents TXTSECNO As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents TXTMATRERIAL As TextBox
    Friend WithEvents TXTWT As TextBox
    Friend WithEvents TXTQUANTITY As TextBox
    Friend WithEvents TXTUPLOADPATH As TextBox
    Friend WithEvents PRINTDOC As System.Drawing.Printing.PrintDocument
    Friend WithEvents PRINTDIALOG As PrintDialog
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
