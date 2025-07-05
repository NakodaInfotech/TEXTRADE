<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UploadGodName
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
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.CMDUPDATE = New System.Windows.Forms.Button()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.TXTGODNAME = New System.Windows.Forms.TextBox()
        Me.LBLGODNAME = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(621, 75)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 6
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'CMDUPDATE
        '
        Me.CMDUPDATE.BackColor = System.Drawing.Color.Transparent
        Me.CMDUPDATE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDUPDATE.FlatAppearance.BorderSize = 0
        Me.CMDUPDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDUPDATE.ForeColor = System.Drawing.Color.Black
        Me.CMDUPDATE.Location = New System.Drawing.Point(449, 75)
        Me.CMDUPDATE.Name = "CMDUPDATE"
        Me.CMDUPDATE.Size = New System.Drawing.Size(80, 28)
        Me.CMDUPDATE.TabIndex = 5
        Me.CMDUPDATE.Text = "&Update"
        Me.CMDUPDATE.UseVisualStyleBackColor = False
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.Color.Transparent
        Me.cmdclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdclear.FlatAppearance.BorderSize = 0
        Me.cmdclear.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.ForeColor = System.Drawing.Color.Black
        Me.cmdclear.Location = New System.Drawing.Point(535, 75)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(80, 28)
        Me.cmdclear.TabIndex = 15
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = False
        '
        'TXTGODNAME
        '
        Me.TXTGODNAME.BackColor = System.Drawing.Color.White
        Me.TXTGODNAME.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTGODNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTGODNAME.Location = New System.Drawing.Point(90, 30)
        Me.TXTGODNAME.MaxLength = 2000
        Me.TXTGODNAME.Name = "TXTGODNAME"
        Me.TXTGODNAME.Size = New System.Drawing.Size(943, 22)
        Me.TXTGODNAME.TabIndex = 424
        Me.TXTGODNAME.TabStop = False
        '
        'LBLGODNAME
        '
        Me.LBLGODNAME.AutoSize = True
        Me.LBLGODNAME.BackColor = System.Drawing.Color.Transparent
        Me.LBLGODNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLGODNAME.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LBLGODNAME.Location = New System.Drawing.Point(20, 33)
        Me.LBLGODNAME.Name = "LBLGODNAME"
        Me.LBLGODNAME.Size = New System.Drawing.Size(64, 14)
        Me.LBLGODNAME.TabIndex = 425
        Me.LBLGODNAME.Text = "God Name"
        '
        'UploadGodName
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1092, 142)
        Me.Controls.Add(Me.TXTGODNAME)
        Me.Controls.Add(Me.LBLGODNAME)
        Me.Controls.Add(Me.cmdclear)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.CMDUPDATE)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "UploadGodName"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "UploadGodName"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdexit As Button
    Friend WithEvents CMDUPDATE As Button
    Friend WithEvents cmdclear As Button
    Friend WithEvents TXTGODNAME As TextBox
    Friend WithEvents LBLGODNAME As Label
End Class
