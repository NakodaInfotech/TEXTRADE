<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CutPackFilter
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
        Me.BlendPanel2 = New VbPowerPack.BlendPanel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TXTJONO = New System.Windows.Forms.TextBox()
        Me.CMBITEMNAME = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RDBCONTRACTOR = New System.Windows.Forms.RadioButton()
        Me.RDBLOT = New System.Windows.Forms.RadioButton()
        Me.RDBITEM = New System.Windows.Forms.RadioButton()
        Me.CHKDATE = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DTTO = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DTFROM = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CMBCONTRACTOR = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CMDSHOW = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.BlendPanel2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel2
        '
        Me.BlendPanel2.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel2.Controls.Add(Me.Label13)
        Me.BlendPanel2.Controls.Add(Me.TXTJONO)
        Me.BlendPanel2.Controls.Add(Me.CMBITEMNAME)
        Me.BlendPanel2.Controls.Add(Me.Label5)
        Me.BlendPanel2.Controls.Add(Me.GroupBox3)
        Me.BlendPanel2.Controls.Add(Me.CHKDATE)
        Me.BlendPanel2.Controls.Add(Me.GroupBox1)
        Me.BlendPanel2.Controls.Add(Me.CMBCONTRACTOR)
        Me.BlendPanel2.Controls.Add(Me.Label9)
        Me.BlendPanel2.Controls.Add(Me.CMDSHOW)
        Me.BlendPanel2.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel2.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel2.Name = "BlendPanel2"
        Me.BlendPanel2.Size = New System.Drawing.Size(363, 310)
        Me.BlendPanel2.TabIndex = 1
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(42, 72)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(41, 15)
        Me.Label13.TabIndex = 693
        Me.Label13.Text = "Lot No"
        '
        'TXTJONO
        '
        Me.TXTJONO.BackColor = System.Drawing.Color.White
        Me.TXTJONO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTJONO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTJONO.ForeColor = System.Drawing.Color.Black
        Me.TXTJONO.Location = New System.Drawing.Point(85, 68)
        Me.TXTJONO.Name = "TXTJONO"
        Me.TXTJONO.Size = New System.Drawing.Size(90, 23)
        Me.TXTJONO.TabIndex = 9
        '
        'CMBITEMNAME
        '
        Me.CMBITEMNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBITEMNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBITEMNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBITEMNAME.FormattingEnabled = True
        Me.CMBITEMNAME.Location = New System.Drawing.Point(85, 40)
        Me.CMBITEMNAME.MaxDropDownItems = 14
        Me.CMBITEMNAME.Name = "CMBITEMNAME"
        Me.CMBITEMNAME.Size = New System.Drawing.Size(230, 22)
        Me.CMBITEMNAME.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(16, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 14)
        Me.Label5.TabIndex = 740
        Me.Label5.Text = "Item Name"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.RDBCONTRACTOR)
        Me.GroupBox3.Controls.Add(Me.RDBLOT)
        Me.GroupBox3.Controls.Add(Me.RDBITEM)
        Me.GroupBox3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(24, 97)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(327, 78)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        '
        'RDBCONTRACTOR
        '
        Me.RDBCONTRACTOR.AutoSize = True
        Me.RDBCONTRACTOR.Location = New System.Drawing.Point(148, 21)
        Me.RDBCONTRACTOR.Name = "RDBCONTRACTOR"
        Me.RDBCONTRACTOR.Size = New System.Drawing.Size(111, 18)
        Me.RDBCONTRACTOR.TabIndex = 0
        Me.RDBCONTRACTOR.Text = "Contractor Wise"
        Me.RDBCONTRACTOR.UseVisualStyleBackColor = True
        '
        'RDBLOT
        '
        Me.RDBLOT.AutoSize = True
        Me.RDBLOT.Location = New System.Drawing.Point(10, 45)
        Me.RDBLOT.Name = "RDBLOT"
        Me.RDBLOT.Size = New System.Drawing.Size(72, 18)
        Me.RDBLOT.TabIndex = 5
        Me.RDBLOT.Text = "Lot Wise"
        Me.RDBLOT.UseVisualStyleBackColor = True
        '
        'RDBITEM
        '
        Me.RDBITEM.AutoSize = True
        Me.RDBITEM.Checked = True
        Me.RDBITEM.Location = New System.Drawing.Point(10, 21)
        Me.RDBITEM.Name = "RDBITEM"
        Me.RDBITEM.Size = New System.Drawing.Size(81, 18)
        Me.RDBITEM.TabIndex = 2
        Me.RDBITEM.TabStop = True
        Me.RDBITEM.Text = "Item Wise"
        Me.RDBITEM.UseVisualStyleBackColor = True
        '
        'CHKDATE
        '
        Me.CHKDATE.AutoSize = True
        Me.CHKDATE.BackColor = System.Drawing.Color.Transparent
        Me.CHKDATE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKDATE.ForeColor = System.Drawing.Color.Black
        Me.CHKDATE.Location = New System.Drawing.Point(30, 185)
        Me.CHKDATE.Name = "CHKDATE"
        Me.CHKDATE.Size = New System.Drawing.Size(85, 18)
        Me.CHKDATE.TabIndex = 12
        Me.CHKDATE.Text = "Issue Date"
        Me.CHKDATE.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.DTTO)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.DTFROM)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(22, 188)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 57)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        '
        'DTTO
        '
        Me.DTTO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTTO.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTTO.Location = New System.Drawing.Point(189, 22)
        Me.DTTO.Name = "DTTO"
        Me.DTTO.Size = New System.Drawing.Size(88, 23)
        Me.DTTO.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(161, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 15)
        Me.Label1.TabIndex = 106
        Me.Label1.Text = "To :"
        '
        'DTFROM
        '
        Me.DTFROM.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTFROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTFROM.Location = New System.Drawing.Point(50, 22)
        Me.DTFROM.Name = "DTFROM"
        Me.DTFROM.Size = New System.Drawing.Size(88, 23)
        Me.DTFROM.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(9, 26)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 15)
        Me.Label7.TabIndex = 104
        Me.Label7.Text = "From :"
        '
        'CMBCONTRACTOR
        '
        Me.CMBCONTRACTOR.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCONTRACTOR.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCONTRACTOR.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCONTRACTOR.FormattingEnabled = True
        Me.CMBCONTRACTOR.Location = New System.Drawing.Point(85, 12)
        Me.CMBCONTRACTOR.MaxDropDownItems = 14
        Me.CMBCONTRACTOR.Name = "CMBCONTRACTOR"
        Me.CMBCONTRACTOR.Size = New System.Drawing.Size(230, 22)
        Me.CMBCONTRACTOR.TabIndex = 0
        Me.CMBCONTRACTOR.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(21, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 14)
        Me.Label9.TabIndex = 419
        Me.Label9.Text = "Contractor"
        Me.Label9.Visible = False
        '
        'CMDSHOW
        '
        Me.CMDSHOW.BackColor = System.Drawing.Color.Transparent
        Me.CMDSHOW.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSHOW.FlatAppearance.BorderSize = 0
        Me.CMDSHOW.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSHOW.ForeColor = System.Drawing.Color.Black
        Me.CMDSHOW.Location = New System.Drawing.Point(85, 251)
        Me.CMDSHOW.Name = "CMDSHOW"
        Me.CMDSHOW.Size = New System.Drawing.Size(83, 28)
        Me.CMDSHOW.TabIndex = 14
        Me.CMDSHOW.Text = "&Show Report"
        Me.CMDSHOW.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(173, 251)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 15
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'CutPackFilter
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(363, 310)
        Me.Controls.Add(Me.BlendPanel2)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "CutPackFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Cut Pack Filter"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel2.ResumeLayout(False)
        Me.BlendPanel2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel2 As VbPowerPack.BlendPanel
    Friend WithEvents Label13 As Label
    Friend WithEvents TXTJONO As TextBox
    Friend WithEvents CMBITEMNAME As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents RDBCONTRACTOR As RadioButton
    Friend WithEvents RDBLOT As RadioButton
    Friend WithEvents RDBITEM As RadioButton
    Friend WithEvents CHKDATE As CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DTTO As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents DTFROM As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents CMBCONTRACTOR As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents CMDSHOW As Button
    Friend WithEvents CMDEXIT As Button
End Class
