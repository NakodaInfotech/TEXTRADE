<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SecurityInwardOutward
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
        Me.GRID = New System.Windows.Forms.GroupBox()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.TXTVIHICLENO = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TXTWEIGHT = New System.Windows.Forms.Label()
        Me.TXTQUALITY = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TXTMATARIAL = New System.Windows.Forms.Label()
        Me.TXTCOMPANYNAME = New System.Windows.Forms.Label()
        Me.LBLDATE = New System.Windows.Forms.Label()
        Me.WEFDATE = New System.Windows.Forms.MaskedTextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.BlendPanel1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GRID
        '
        Me.GRID.BackColor = System.Drawing.Color.Transparent
        Me.GRID.Location = New System.Drawing.Point(20, 107)
        Me.GRID.Name = "GRID"
        Me.GRID.Size = New System.Drawing.Size(601, 217)
        Me.GRID.TabIndex = 3
        Me.GRID.TabStop = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdok.Location = New System.Drawing.Point(151, 406)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 4
        Me.cmdok.Text = "&Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.Cursor = System.Windows.Forms.Cursors.Default
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDCLEAR.Location = New System.Drawing.Point(237, 406)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 5
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmddelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmddelete.Location = New System.Drawing.Point(323, 406)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 28)
        Me.cmddelete.TabIndex = 6
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdexit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdexit.Location = New System.Drawing.Point(409, 406)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 7
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTVIHICLENO)
        Me.BlendPanel1.Controls.Add(Me.TextBox6)
        Me.BlendPanel1.Controls.Add(Me.TXTWEIGHT)
        Me.BlendPanel1.Controls.Add(Me.TXTQUALITY)
        Me.BlendPanel1.Controls.Add(Me.TextBox5)
        Me.BlendPanel1.Controls.Add(Me.TXTMATARIAL)
        Me.BlendPanel1.Controls.Add(Me.TXTCOMPANYNAME)
        Me.BlendPanel1.Controls.Add(Me.LBLDATE)
        Me.BlendPanel1.Controls.Add(Me.WEFDATE)
        Me.BlendPanel1.Controls.Add(Me.GroupBox5)
        Me.BlendPanel1.Controls.Add(Me.TextBox3)
        Me.BlendPanel1.Controls.Add(Me.TextBox2)
        Me.BlendPanel1.Controls.Add(Me.TextBox1)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.GRID)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(641, 446)
        Me.BlendPanel1.TabIndex = 1
        '
        'TXTVIHICLENO
        '
        Me.TXTVIHICLENO.AutoSize = True
        Me.TXTVIHICLENO.BackColor = System.Drawing.Color.Transparent
        Me.TXTVIHICLENO.Location = New System.Drawing.Point(441, 47)
        Me.TXTVIHICLENO.Name = "TXTVIHICLENO"
        Me.TXTVIHICLENO.Size = New System.Drawing.Size(67, 13)
        Me.TXTVIHICLENO.TabIndex = 29
        Me.TXTVIHICLENO.Text = "VIHICLE NO"
        '
        'TextBox6
        '
        Me.TextBox6.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox6.Location = New System.Drawing.Point(512, 43)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(100, 20)
        Me.TextBox6.TabIndex = 28
        '
        'TXTWEIGHT
        '
        Me.TXTWEIGHT.AutoSize = True
        Me.TXTWEIGHT.BackColor = System.Drawing.Color.Transparent
        Me.TXTWEIGHT.Location = New System.Drawing.Point(228, 50)
        Me.TXTWEIGHT.Name = "TXTWEIGHT"
        Me.TXTWEIGHT.Size = New System.Drawing.Size(51, 13)
        Me.TXTWEIGHT.TabIndex = 27
        Me.TXTWEIGHT.Text = "WEIGHT"
        '
        'TXTQUALITY
        '
        Me.TXTQUALITY.AutoSize = True
        Me.TXTQUALITY.BackColor = System.Drawing.Color.Transparent
        Me.TXTQUALITY.Location = New System.Drawing.Point(227, 21)
        Me.TXTQUALITY.Name = "TXTQUALITY"
        Me.TXTQUALITY.Size = New System.Drawing.Size(53, 13)
        Me.TXTQUALITY.TabIndex = 26
        Me.TXTQUALITY.Text = "QUALITY"
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox5.Location = New System.Drawing.Point(284, 46)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(100, 20)
        Me.TextBox5.TabIndex = 25
        '
        'TXTMATARIAL
        '
        Me.TXTMATARIAL.AutoSize = True
        Me.TXTMATARIAL.BackColor = System.Drawing.Color.Transparent
        Me.TXTMATARIAL.Location = New System.Drawing.Point(39, 47)
        Me.TXTMATARIAL.Name = "TXTMATARIAL"
        Me.TXTMATARIAL.Size = New System.Drawing.Size(61, 13)
        Me.TXTMATARIAL.TabIndex = 24
        Me.TXTMATARIAL.Text = "MATARIAL"
        '
        'TXTCOMPANYNAME
        '
        Me.TXTCOMPANYNAME.AutoSize = True
        Me.TXTCOMPANYNAME.BackColor = System.Drawing.Color.Transparent
        Me.TXTCOMPANYNAME.Location = New System.Drawing.Point(10, 21)
        Me.TXTCOMPANYNAME.Name = "TXTCOMPANYNAME"
        Me.TXTCOMPANYNAME.Size = New System.Drawing.Size(91, 13)
        Me.TXTCOMPANYNAME.TabIndex = 23
        Me.TXTCOMPANYNAME.Text = "COMPANYNAME"
        '
        'LBLDATE
        '
        Me.LBLDATE.AutoSize = True
        Me.LBLDATE.BackColor = System.Drawing.Color.Transparent
        Me.LBLDATE.Location = New System.Drawing.Point(482, 21)
        Me.LBLDATE.Name = "LBLDATE"
        Me.LBLDATE.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.LBLDATE.Size = New System.Drawing.Size(36, 13)
        Me.LBLDATE.TabIndex = 4
        Me.LBLDATE.Text = "DATE"
        '
        'WEFDATE
        '
        Me.WEFDATE.AsciiOnly = True
        Me.WEFDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.WEFDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.WEFDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.WEFDATE.Location = New System.Drawing.Point(522, 16)
        Me.WEFDATE.Mask = "00/00/0000"
        Me.WEFDATE.Name = "WEFDATE"
        Me.WEFDATE.Size = New System.Drawing.Size(90, 23)
        Me.WEFDATE.TabIndex = 22
        Me.WEFDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.WEFDATE.ValidatingType = GetType(Date)
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.TextBox4)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(21, 329)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(119, 79)
        Me.GroupBox5.TabIndex = 21
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Remarks"
        '
        'TextBox4
        '
        Me.TextBox4.ForeColor = System.Drawing.Color.DimGray
        Me.TextBox4.Location = New System.Drawing.Point(5, 14)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(108, 56)
        Me.TextBox4.TabIndex = 0
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox3.Location = New System.Drawing.Point(284, 17)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(100, 20)
        Me.TextBox3.TabIndex = 11
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox2.Location = New System.Drawing.Point(104, 43)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 10
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox1.Location = New System.Drawing.Point(104, 17)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 9
        '
        'SecurityInwardOutward
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 446)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Name = "SecurityInwardOutward"
        Me.ShowIcon = False
        Me.Text = "SecurityInwardOutward"
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GRID As GroupBox
    Friend WithEvents cmdok As Button
    Friend WithEvents CMDCLEAR As Button
    Friend WithEvents cmddelete As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents WEFDATE As MaskedTextBox
    Friend WithEvents TXTCOMPANYNAME As Label
    Friend WithEvents LBLDATE As Label
    Friend WithEvents TXTMATARIAL As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents TXTQUALITY As Label
    Friend WithEvents TXTWEIGHT As Label
    Friend WithEvents TXTVIHICLENO As Label
    Friend WithEvents TextBox6 As TextBox
End Class
