<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemDesignImage
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
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.CMDUPDATE = New System.Windows.Forms.Button()
        Me.TXTSETMTRS = New System.Windows.Forms.TextBox()
        Me.LBLSETMTRS = New System.Windows.Forms.Label()
        Me.CMDCOMPRESS = New System.Windows.Forms.Button()
        Me.TXTITEMNO = New System.Windows.Forms.TextBox()
        Me.LBLSRNO = New System.Windows.Forms.Label()
        Me.TXTPHOTOIMGPATH2 = New System.Windows.Forms.TextBox()
        Me.TXTPHOTOIMGPATH1 = New System.Windows.Forms.TextBox()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.CMDDELETE = New System.Windows.Forms.Button()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDPHOTOREMOVE2 = New System.Windows.Forms.Button()
        Me.CMDPHOTOUPLOAD2 = New System.Windows.Forms.Button()
        Me.CMDPHOTOVIEW2 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PBIMAGE2 = New System.Windows.Forms.PictureBox()
        Me.CMDPHOTOREMOVE1 = New System.Windows.Forms.Button()
        Me.CMDPHOTOUPLOAD1 = New System.Windows.Forms.Button()
        Me.CMDPHOTOVIEW1 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PBIMAGE1 = New System.Windows.Forms.PictureBox()
        Me.CMBDESIGNNAME = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CMBITEMNAME = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.CMDDOWNLOAD = New System.Windows.Forms.Button()
        Me.TXTFILENAME = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TBUPLOAD = New System.Windows.Forms.TabPage()
        Me.TBPATH = New System.Windows.Forms.TabPage()
        Me.PBIMAGEPATH = New System.Windows.Forms.PictureBox()
        Me.CMDVIEWPATH = New System.Windows.Forms.Button()
        Me.CMDUPLOADPATH = New System.Windows.Forms.Button()
        Me.CMDREMOVEPATH = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TXTPHOTOIMAGEUPLOADPATH = New System.Windows.Forms.TextBox()
        Me.TXTUPLOADPATH = New System.Windows.Forms.TextBox()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.PBIMAGE2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBIMAGE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TBUPLOAD.SuspendLayout()
        Me.TBPATH.SuspendLayout()
        CType(Me.PBIMAGEPATH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTPHOTOIMAGEUPLOADPATH)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.TXTFILENAME)
        Me.BlendPanel1.Controls.Add(Me.TabControl1)
        Me.BlendPanel1.Controls.Add(Me.CMDDOWNLOAD)
        Me.BlendPanel1.Controls.Add(Me.CMDUPDATE)
        Me.BlendPanel1.Controls.Add(Me.TXTSETMTRS)
        Me.BlendPanel1.Controls.Add(Me.LBLSETMTRS)
        Me.BlendPanel1.Controls.Add(Me.TXTITEMNO)
        Me.BlendPanel1.Controls.Add(Me.LBLSRNO)
        Me.BlendPanel1.Controls.Add(Me.TXTPHOTOIMGPATH2)
        Me.BlendPanel1.Controls.Add(Me.TXTPHOTOIMGPATH1)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.CMDDELETE)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMBDESIGNNAME)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.CMBITEMNAME)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(622, 435)
        Me.BlendPanel1.TabIndex = 0
        '
        'CMDUPDATE
        '
        Me.CMDUPDATE.BackColor = System.Drawing.Color.Transparent
        Me.CMDUPDATE.FlatAppearance.BorderSize = 0
        Me.CMDUPDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDUPDATE.ForeColor = System.Drawing.Color.Black
        Me.CMDUPDATE.Location = New System.Drawing.Point(542, 356)
        Me.CMDUPDATE.Name = "CMDUPDATE"
        Me.CMDUPDATE.Size = New System.Drawing.Size(80, 28)
        Me.CMDUPDATE.TabIndex = 636
        Me.CMDUPDATE.Text = "UPDATE"
        Me.CMDUPDATE.UseVisualStyleBackColor = False
        Me.CMDUPDATE.Visible = False
        '
        'TXTSETMTRS
        '
        Me.TXTSETMTRS.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTSETMTRS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSETMTRS.Location = New System.Drawing.Point(464, 51)
        Me.TXTSETMTRS.Name = "TXTSETMTRS"
        Me.TXTSETMTRS.Size = New System.Drawing.Size(77, 23)
        Me.TXTSETMTRS.TabIndex = 2
        Me.TXTSETMTRS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLSETMTRS
        '
        Me.LBLSETMTRS.AutoSize = True
        Me.LBLSETMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLSETMTRS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLSETMTRS.Location = New System.Drawing.Point(408, 55)
        Me.LBLSETMTRS.Name = "LBLSETMTRS"
        Me.LBLSETMTRS.Size = New System.Drawing.Size(51, 14)
        Me.LBLSETMTRS.TabIndex = 635
        Me.LBLSETMTRS.Text = "Set Mtrs"
        '
        'CMDCOMPRESS
        '
        Me.CMDCOMPRESS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDCOMPRESS.Location = New System.Drawing.Point(243, 157)
        Me.CMDCOMPRESS.Name = "CMDCOMPRESS"
        Me.CMDCOMPRESS.Size = New System.Drawing.Size(80, 28)
        Me.CMDCOMPRESS.TabIndex = 633
        Me.CMDCOMPRESS.Text = "&Compress"
        Me.CMDCOMPRESS.UseVisualStyleBackColor = True
        '
        'TXTITEMNO
        '
        Me.TXTITEMNO.BackColor = System.Drawing.Color.Linen
        Me.TXTITEMNO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTITEMNO.Location = New System.Drawing.Point(464, 23)
        Me.TXTITEMNO.Name = "TXTITEMNO"
        Me.TXTITEMNO.ReadOnly = True
        Me.TXTITEMNO.Size = New System.Drawing.Size(77, 22)
        Me.TXTITEMNO.TabIndex = 631
        Me.TXTITEMNO.TabStop = False
        Me.TXTITEMNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLSRNO
        '
        Me.LBLSRNO.AutoSize = True
        Me.LBLSRNO.BackColor = System.Drawing.Color.Transparent
        Me.LBLSRNO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLSRNO.ForeColor = System.Drawing.Color.Black
        Me.LBLSRNO.Location = New System.Drawing.Point(422, 27)
        Me.LBLSRNO.Name = "LBLSRNO"
        Me.LBLSRNO.Size = New System.Drawing.Size(37, 14)
        Me.LBLSRNO.TabIndex = 632
        Me.LBLSRNO.Text = "Sr. No"
        Me.LBLSRNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTPHOTOIMGPATH2
        '
        Me.TXTPHOTOIMGPATH2.BackColor = System.Drawing.Color.White
        Me.TXTPHOTOIMGPATH2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPHOTOIMGPATH2.ForeColor = System.Drawing.Color.Black
        Me.TXTPHOTOIMGPATH2.Location = New System.Drawing.Point(577, 27)
        Me.TXTPHOTOIMGPATH2.Name = "TXTPHOTOIMGPATH2"
        Me.TXTPHOTOIMGPATH2.Size = New System.Drawing.Size(22, 23)
        Me.TXTPHOTOIMGPATH2.TabIndex = 490
        Me.TXTPHOTOIMGPATH2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTPHOTOIMGPATH2.Visible = False
        '
        'TXTPHOTOIMGPATH1
        '
        Me.TXTPHOTOIMGPATH1.BackColor = System.Drawing.Color.White
        Me.TXTPHOTOIMGPATH1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPHOTOIMGPATH1.ForeColor = System.Drawing.Color.Black
        Me.TXTPHOTOIMGPATH1.Location = New System.Drawing.Point(577, 12)
        Me.TXTPHOTOIMGPATH1.Name = "TXTPHOTOIMGPATH1"
        Me.TXTPHOTOIMGPATH1.Size = New System.Drawing.Size(22, 23)
        Me.TXTPHOTOIMGPATH1.TabIndex = 489
        Me.TXTPHOTOIMGPATH1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTPHOTOIMGPATH1.Visible = False
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDCLEAR.FlatAppearance.BorderSize = 0
        Me.CMDCLEAR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.Black
        Me.CMDCLEAR.Location = New System.Drawing.Point(228, 356)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 10
        Me.CMDCLEAR.Text = "Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
        '
        'CMDDELETE
        '
        Me.CMDDELETE.BackColor = System.Drawing.Color.Transparent
        Me.CMDDELETE.FlatAppearance.BorderSize = 0
        Me.CMDDELETE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDELETE.ForeColor = System.Drawing.Color.Black
        Me.CMDDELETE.Location = New System.Drawing.Point(314, 356)
        Me.CMDDELETE.Name = "CMDDELETE"
        Me.CMDDELETE.Size = New System.Drawing.Size(80, 28)
        Me.CMDDELETE.TabIndex = 11
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
        Me.CMDOK.Location = New System.Drawing.Point(142, 356)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 9
        Me.CMDOK.Text = "&Save"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(400, 356)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 12
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'CMDPHOTOREMOVE2
        '
        Me.CMDPHOTOREMOVE2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDPHOTOREMOVE2.Location = New System.Drawing.Point(394, 89)
        Me.CMDPHOTOREMOVE2.Name = "CMDPHOTOREMOVE2"
        Me.CMDPHOTOREMOVE2.Size = New System.Drawing.Size(80, 28)
        Me.CMDPHOTOREMOVE2.TabIndex = 7
        Me.CMDPHOTOREMOVE2.Text = "Remove"
        Me.CMDPHOTOREMOVE2.UseVisualStyleBackColor = True
        '
        'CMDPHOTOUPLOAD2
        '
        Me.CMDPHOTOUPLOAD2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDPHOTOUPLOAD2.Location = New System.Drawing.Point(394, 55)
        Me.CMDPHOTOUPLOAD2.Name = "CMDPHOTOUPLOAD2"
        Me.CMDPHOTOUPLOAD2.Size = New System.Drawing.Size(80, 28)
        Me.CMDPHOTOUPLOAD2.TabIndex = 6
        Me.CMDPHOTOUPLOAD2.Text = "Upload"
        Me.CMDPHOTOUPLOAD2.UseVisualStyleBackColor = True
        '
        'CMDPHOTOVIEW2
        '
        Me.CMDPHOTOVIEW2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDPHOTOVIEW2.Location = New System.Drawing.Point(394, 123)
        Me.CMDPHOTOVIEW2.Name = "CMDPHOTOVIEW2"
        Me.CMDPHOTOVIEW2.Size = New System.Drawing.Size(80, 28)
        Me.CMDPHOTOVIEW2.TabIndex = 8
        Me.CMDPHOTOVIEW2.Text = "View"
        Me.CMDPHOTOVIEW2.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(391, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 15)
        Me.Label4.TabIndex = 484
        Me.Label4.Text = "Image2"
        '
        'PBIMAGE2
        '
        Me.PBIMAGE2.BackColor = System.Drawing.Color.White
        Me.PBIMAGE2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBIMAGE2.ErrorImage = Nothing
        Me.PBIMAGE2.InitialImage = Nothing
        Me.PBIMAGE2.Location = New System.Drawing.Point(243, 6)
        Me.PBIMAGE2.Name = "PBIMAGE2"
        Me.PBIMAGE2.Size = New System.Drawing.Size(142, 145)
        Me.PBIMAGE2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBIMAGE2.TabIndex = 483
        Me.PBIMAGE2.TabStop = False
        '
        'CMDPHOTOREMOVE1
        '
        Me.CMDPHOTOREMOVE1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDPHOTOREMOVE1.Location = New System.Drawing.Point(157, 89)
        Me.CMDPHOTOREMOVE1.Name = "CMDPHOTOREMOVE1"
        Me.CMDPHOTOREMOVE1.Size = New System.Drawing.Size(80, 28)
        Me.CMDPHOTOREMOVE1.TabIndex = 4
        Me.CMDPHOTOREMOVE1.Text = "Remove"
        Me.CMDPHOTOREMOVE1.UseVisualStyleBackColor = True
        '
        'CMDPHOTOUPLOAD1
        '
        Me.CMDPHOTOUPLOAD1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDPHOTOUPLOAD1.Location = New System.Drawing.Point(157, 55)
        Me.CMDPHOTOUPLOAD1.Name = "CMDPHOTOUPLOAD1"
        Me.CMDPHOTOUPLOAD1.Size = New System.Drawing.Size(80, 28)
        Me.CMDPHOTOUPLOAD1.TabIndex = 3
        Me.CMDPHOTOUPLOAD1.Text = "Upload"
        Me.CMDPHOTOUPLOAD1.UseVisualStyleBackColor = True
        '
        'CMDPHOTOVIEW1
        '
        Me.CMDPHOTOVIEW1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDPHOTOVIEW1.Location = New System.Drawing.Point(157, 123)
        Me.CMDPHOTOVIEW1.Name = "CMDPHOTOVIEW1"
        Me.CMDPHOTOVIEW1.Size = New System.Drawing.Size(80, 28)
        Me.CMDPHOTOVIEW1.TabIndex = 5
        Me.CMDPHOTOVIEW1.Text = "View"
        Me.CMDPHOTOVIEW1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(154, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 15)
        Me.Label2.TabIndex = 479
        Me.Label2.Text = "Image1"
        '
        'PBIMAGE1
        '
        Me.PBIMAGE1.BackColor = System.Drawing.Color.White
        Me.PBIMAGE1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBIMAGE1.ErrorImage = Nothing
        Me.PBIMAGE1.InitialImage = Nothing
        Me.PBIMAGE1.Location = New System.Drawing.Point(6, 6)
        Me.PBIMAGE1.Name = "PBIMAGE1"
        Me.PBIMAGE1.Size = New System.Drawing.Size(142, 145)
        Me.PBIMAGE1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBIMAGE1.TabIndex = 478
        Me.PBIMAGE1.TabStop = False
        '
        'CMBDESIGNNAME
        '
        Me.CMBDESIGNNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBDESIGNNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBDESIGNNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBDESIGNNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBDESIGNNAME.FormattingEnabled = True
        Me.CMBDESIGNNAME.Location = New System.Drawing.Point(104, 51)
        Me.CMBDESIGNNAME.MaxDropDownItems = 14
        Me.CMBDESIGNNAME.Name = "CMBDESIGNNAME"
        Me.CMBDESIGNNAME.Size = New System.Drawing.Size(294, 22)
        Me.CMBDESIGNNAME.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 14)
        Me.Label1.TabIndex = 312
        Me.Label1.Text = "Design Name"
        '
        'CMBITEMNAME
        '
        Me.CMBITEMNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBITEMNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBITEMNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBITEMNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBITEMNAME.FormattingEnabled = True
        Me.CMBITEMNAME.Location = New System.Drawing.Point(104, 23)
        Me.CMBITEMNAME.MaxDropDownItems = 14
        Me.CMBITEMNAME.Name = "CMBITEMNAME"
        Me.CMBITEMNAME.Size = New System.Drawing.Size(294, 22)
        Me.CMBITEMNAME.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(33, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 14)
        Me.Label3.TabIndex = 310
        Me.Label3.Text = "Item Name"
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
        'CMDDOWNLOAD
        '
        Me.CMDDOWNLOAD.BackColor = System.Drawing.Color.Transparent
        Me.CMDDOWNLOAD.FlatAppearance.BorderSize = 0
        Me.CMDDOWNLOAD.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDOWNLOAD.ForeColor = System.Drawing.Color.Black
        Me.CMDDOWNLOAD.Location = New System.Drawing.Point(542, 390)
        Me.CMDDOWNLOAD.Name = "CMDDOWNLOAD"
        Me.CMDDOWNLOAD.Size = New System.Drawing.Size(87, 28)
        Me.CMDDOWNLOAD.TabIndex = 637
        Me.CMDDOWNLOAD.Text = "DOWNLOAD"
        Me.CMDDOWNLOAD.UseVisualStyleBackColor = False
        Me.CMDDOWNLOAD.Visible = False
        '
        'TXTFILENAME
        '
        Me.TXTFILENAME.BackColor = System.Drawing.Color.Linen
        Me.TXTFILENAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFILENAME.ForeColor = System.Drawing.Color.Black
        Me.TXTFILENAME.Location = New System.Drawing.Point(104, 79)
        Me.TXTFILENAME.MaxLength = 100
        Me.TXTFILENAME.Name = "TXTFILENAME"
        Me.TXTFILENAME.ReadOnly = True
        Me.TXTFILENAME.Size = New System.Drawing.Size(294, 23)
        Me.TXTFILENAME.TabIndex = 638
        Me.TXTFILENAME.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TBUPLOAD)
        Me.TabControl1.Controls.Add(Me.TBPATH)
        Me.TabControl1.Location = New System.Drawing.Point(12, 125)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(607, 225)
        Me.TabControl1.TabIndex = 639
        '
        'TBUPLOAD
        '
        Me.TBUPLOAD.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TBUPLOAD.Controls.Add(Me.PBIMAGE1)
        Me.TBUPLOAD.Controls.Add(Me.Label2)
        Me.TBUPLOAD.Controls.Add(Me.CMDPHOTOVIEW1)
        Me.TBUPLOAD.Controls.Add(Me.CMDPHOTOUPLOAD1)
        Me.TBUPLOAD.Controls.Add(Me.CMDPHOTOREMOVE1)
        Me.TBUPLOAD.Controls.Add(Me.PBIMAGE2)
        Me.TBUPLOAD.Controls.Add(Me.CMDCOMPRESS)
        Me.TBUPLOAD.Controls.Add(Me.Label4)
        Me.TBUPLOAD.Controls.Add(Me.CMDPHOTOVIEW2)
        Me.TBUPLOAD.Controls.Add(Me.CMDPHOTOUPLOAD2)
        Me.TBUPLOAD.Controls.Add(Me.CMDPHOTOREMOVE2)
        Me.TBUPLOAD.Location = New System.Drawing.Point(4, 24)
        Me.TBUPLOAD.Name = "TBUPLOAD"
        Me.TBUPLOAD.Padding = New System.Windows.Forms.Padding(3)
        Me.TBUPLOAD.Size = New System.Drawing.Size(599, 197)
        Me.TBUPLOAD.TabIndex = 0
        Me.TBUPLOAD.Text = "1. Upload Images"
        '
        'TBPATH
        '
        Me.TBPATH.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TBPATH.Controls.Add(Me.TXTUPLOADPATH)
        Me.TBPATH.Controls.Add(Me.PBIMAGEPATH)
        Me.TBPATH.Controls.Add(Me.CMDVIEWPATH)
        Me.TBPATH.Controls.Add(Me.CMDUPLOADPATH)
        Me.TBPATH.Controls.Add(Me.CMDREMOVEPATH)
        Me.TBPATH.Location = New System.Drawing.Point(4, 24)
        Me.TBPATH.Name = "TBPATH"
        Me.TBPATH.Padding = New System.Windows.Forms.Padding(3)
        Me.TBPATH.Size = New System.Drawing.Size(599, 197)
        Me.TBPATH.TabIndex = 1
        Me.TBPATH.Text = "2. Upload Path"
        '
        'PBIMAGEPATH
        '
        Me.PBIMAGEPATH.BackColor = System.Drawing.Color.White
        Me.PBIMAGEPATH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBIMAGEPATH.ErrorImage = Nothing
        Me.PBIMAGEPATH.InitialImage = Nothing
        Me.PBIMAGEPATH.Location = New System.Drawing.Point(7, 6)
        Me.PBIMAGEPATH.Name = "PBIMAGEPATH"
        Me.PBIMAGEPATH.Size = New System.Drawing.Size(142, 145)
        Me.PBIMAGEPATH.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBIMAGEPATH.TabIndex = 483
        Me.PBIMAGEPATH.TabStop = False
        '
        'CMDVIEWPATH
        '
        Me.CMDVIEWPATH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDVIEWPATH.Location = New System.Drawing.Point(158, 123)
        Me.CMDVIEWPATH.Name = "CMDVIEWPATH"
        Me.CMDVIEWPATH.Size = New System.Drawing.Size(80, 28)
        Me.CMDVIEWPATH.TabIndex = 482
        Me.CMDVIEWPATH.Text = "View"
        Me.CMDVIEWPATH.UseVisualStyleBackColor = True
        '
        'CMDUPLOADPATH
        '
        Me.CMDUPLOADPATH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDUPLOADPATH.Location = New System.Drawing.Point(158, 55)
        Me.CMDUPLOADPATH.Name = "CMDUPLOADPATH"
        Me.CMDUPLOADPATH.Size = New System.Drawing.Size(80, 28)
        Me.CMDUPLOADPATH.TabIndex = 480
        Me.CMDUPLOADPATH.Text = "Upload"
        Me.CMDUPLOADPATH.UseVisualStyleBackColor = True
        '
        'CMDREMOVEPATH
        '
        Me.CMDREMOVEPATH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREMOVEPATH.Location = New System.Drawing.Point(158, 89)
        Me.CMDREMOVEPATH.Name = "CMDREMOVEPATH"
        Me.CMDREMOVEPATH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREMOVEPATH.TabIndex = 481
        Me.CMDREMOVEPATH.Text = "Remove"
        Me.CMDREMOVEPATH.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(39, 83)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 15)
        Me.Label6.TabIndex = 639
        Me.Label6.Text = "File Name"
        '
        'TXTPHOTOIMAGEUPLOADPATH
        '
        Me.TXTPHOTOIMAGEUPLOADPATH.BackColor = System.Drawing.Color.White
        Me.TXTPHOTOIMAGEUPLOADPATH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPHOTOIMAGEUPLOADPATH.ForeColor = System.Drawing.Color.Black
        Me.TXTPHOTOIMAGEUPLOADPATH.Location = New System.Drawing.Point(577, 50)
        Me.TXTPHOTOIMAGEUPLOADPATH.Name = "TXTPHOTOIMAGEUPLOADPATH"
        Me.TXTPHOTOIMAGEUPLOADPATH.Size = New System.Drawing.Size(22, 23)
        Me.TXTPHOTOIMAGEUPLOADPATH.TabIndex = 640
        Me.TXTPHOTOIMAGEUPLOADPATH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTPHOTOIMAGEUPLOADPATH.Visible = False
        '
        'TXTUPLOADPATH
        '
        Me.TXTUPLOADPATH.BackColor = System.Drawing.Color.Linen
        Me.TXTUPLOADPATH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTUPLOADPATH.ForeColor = System.Drawing.Color.Black
        Me.TXTUPLOADPATH.Location = New System.Drawing.Point(289, 19)
        Me.TXTUPLOADPATH.MaxLength = 100
        Me.TXTUPLOADPATH.Name = "TXTUPLOADPATH"
        Me.TXTUPLOADPATH.ReadOnly = True
        Me.TXTUPLOADPATH.Size = New System.Drawing.Size(294, 23)
        Me.TXTUPLOADPATH.TabIndex = 641
        Me.TXTUPLOADPATH.TabStop = False
        '
        'ItemDesignImage
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(622, 435)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "ItemDesignImage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Item Design Image"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.PBIMAGE2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBIMAGE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TBUPLOAD.ResumeLayout(False)
        Me.TBUPLOAD.PerformLayout()
        Me.TBPATH.ResumeLayout(False)
        Me.TBPATH.PerformLayout()
        CType(Me.PBIMAGEPATH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMBITEMNAME As ComboBox
    Friend WithEvents CMBDESIGNNAME As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents CMDPHOTOREMOVE2 As Button
    Friend WithEvents CMDPHOTOUPLOAD2 As Button
    Friend WithEvents CMDPHOTOVIEW2 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents PBIMAGE2 As PictureBox
    Friend WithEvents CMDPHOTOREMOVE1 As Button
    Friend WithEvents CMDPHOTOUPLOAD1 As Button
    Friend WithEvents CMDPHOTOVIEW1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents PBIMAGE1 As PictureBox
    Friend WithEvents CMDDELETE As Button
    Friend WithEvents CMDOK As Button
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDCLEAR As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents TXTPHOTOIMGPATH2 As TextBox
    Friend WithEvents TXTPHOTOIMGPATH1 As TextBox
    Friend WithEvents TXTITEMNO As TextBox
    Friend WithEvents LBLSRNO As Label
    Friend WithEvents CMDCOMPRESS As Button
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents TXTSETMTRS As TextBox
    Friend WithEvents LBLSETMTRS As Label
    Friend WithEvents CMDUPDATE As Button
    Friend WithEvents CMDDOWNLOAD As Button
    Friend WithEvents TXTFILENAME As TextBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TBUPLOAD As TabPage
    Friend WithEvents TBPATH As TabPage
    Friend WithEvents Label6 As Label
    Friend WithEvents PBIMAGEPATH As PictureBox
    Friend WithEvents CMDVIEWPATH As Button
    Friend WithEvents CMDUPLOADPATH As Button
    Friend WithEvents CMDREMOVEPATH As Button
    Friend WithEvents TXTPHOTOIMAGEUPLOADPATH As TextBox
    Friend WithEvents TXTUPLOADPATH As TextBox
End Class
