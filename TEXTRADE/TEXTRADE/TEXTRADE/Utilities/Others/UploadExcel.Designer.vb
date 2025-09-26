<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UploadExcel
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
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.CMDSELECTFILE = New System.Windows.Forms.Button()
        Me.TXTB2BPATH = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmdupload = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.TXTFILENAME = New System.Windows.Forms.TextBox()
        Me.BlendPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTFILENAME)
        Me.BlendPanel1.Controls.Add(Me.CMDSELECTFILE)
        Me.BlendPanel1.Controls.Add(Me.TXTB2BPATH)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.cmdupload)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(484, 276)
        Me.BlendPanel1.TabIndex = 2
        '
        'CMDSELECTFILE
        '
        Me.CMDSELECTFILE.BackColor = System.Drawing.Color.Transparent
        Me.CMDSELECTFILE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSELECTFILE.FlatAppearance.BorderSize = 0
        Me.CMDSELECTFILE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSELECTFILE.ForeColor = System.Drawing.Color.Black
        Me.CMDSELECTFILE.Location = New System.Drawing.Point(86, 87)
        Me.CMDSELECTFILE.Name = "CMDSELECTFILE"
        Me.CMDSELECTFILE.Size = New System.Drawing.Size(80, 28)
        Me.CMDSELECTFILE.TabIndex = 577
        Me.CMDSELECTFILE.Text = "Select File"
        Me.CMDSELECTFILE.UseVisualStyleBackColor = False
        '
        'TXTB2BPATH
        '
        Me.TXTB2BPATH.BackColor = System.Drawing.Color.Linen
        Me.TXTB2BPATH.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTB2BPATH.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTB2BPATH.Location = New System.Drawing.Point(86, 42)
        Me.TXTB2BPATH.Name = "TXTB2BPATH"
        Me.TXTB2BPATH.ReadOnly = True
        Me.TXTB2BPATH.Size = New System.Drawing.Size(280, 22)
        Me.TXTB2BPATH.TabIndex = 575
        Me.TXTB2BPATH.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(48, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 15)
        Me.Label6.TabIndex = 576
        Me.Label6.Text = "Path"
        '
        'cmdupload
        '
        Me.cmdupload.BackColor = System.Drawing.Color.Transparent
        Me.cmdupload.FlatAppearance.BorderSize = 0
        Me.cmdupload.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdupload.ForeColor = System.Drawing.Color.Black
        Me.cmdupload.Location = New System.Drawing.Point(187, 87)
        Me.cmdupload.Name = "cmdupload"
        Me.cmdupload.Size = New System.Drawing.Size(80, 28)
        Me.cmdupload.TabIndex = 16
        Me.cmdupload.Text = "Upload"
        Me.cmdupload.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(286, 87)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 18
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'TXTFILENAME
        '
        Me.TXTFILENAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFILENAME.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTFILENAME.Location = New System.Drawing.Point(3, 3)
        Me.TXTFILENAME.Multiline = True
        Me.TXTFILENAME.Name = "TXTFILENAME"
        Me.TXTFILENAME.Size = New System.Drawing.Size(10, 22)
        Me.TXTFILENAME.TabIndex = 573
        Me.TXTFILENAME.Visible = False
        '
        'UploadExcel
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(484, 276)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "UploadExcel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "UploadExcel"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents cmdupload As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents TXTB2BPATH As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CMDSELECTFILE As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents TXTFILENAME As TextBox
End Class
