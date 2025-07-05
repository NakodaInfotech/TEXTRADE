<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BlockDataTransfer
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
        Me.CHKSTOCK = New System.Windows.Forms.CheckBox()
        Me.CHKOTHERMASTER = New System.Windows.Forms.CheckBox()
        Me.CMDUPDATE = New System.Windows.Forms.Button()
        Me.CHKDATA = New System.Windows.Forms.CheckBox()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CHKLEDGER = New System.Windows.Forms.CheckBox()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.CMDDELETE = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMDDELETE)
        Me.BlendPanel1.Controls.Add(Me.CHKSTOCK)
        Me.BlendPanel1.Controls.Add(Me.CHKOTHERMASTER)
        Me.BlendPanel1.Controls.Add(Me.CMDUPDATE)
        Me.BlendPanel1.Controls.Add(Me.CHKDATA)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CHKLEDGER)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(288, 222)
        Me.BlendPanel1.TabIndex = 18
        '
        'CHKSTOCK
        '
        Me.CHKSTOCK.AutoSize = True
        Me.CHKSTOCK.BackColor = System.Drawing.Color.Transparent
        Me.CHKSTOCK.Checked = True
        Me.CHKSTOCK.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHKSTOCK.Location = New System.Drawing.Point(30, 100)
        Me.CHKSTOCK.Name = "CHKSTOCK"
        Me.CHKSTOCK.Size = New System.Drawing.Size(136, 19)
        Me.CHKSTOCK.TabIndex = 3
        Me.CHKSTOCK.Text = "Block Stock Transfer"
        Me.CHKSTOCK.UseVisualStyleBackColor = False
        '
        'CHKOTHERMASTER
        '
        Me.CHKOTHERMASTER.AutoSize = True
        Me.CHKOTHERMASTER.BackColor = System.Drawing.Color.Transparent
        Me.CHKOTHERMASTER.Checked = True
        Me.CHKOTHERMASTER.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHKOTHERMASTER.Location = New System.Drawing.Point(30, 50)
        Me.CHKOTHERMASTER.Name = "CHKOTHERMASTER"
        Me.CHKOTHERMASTER.Size = New System.Drawing.Size(180, 19)
        Me.CHKOTHERMASTER.TabIndex = 2
        Me.CHKOTHERMASTER.Text = "Block Other Master Transfer"
        Me.CHKOTHERMASTER.UseVisualStyleBackColor = False
        '
        'CMDUPDATE
        '
        Me.CMDUPDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDUPDATE.ForeColor = System.Drawing.Color.Black
        Me.CMDUPDATE.Location = New System.Drawing.Point(61, 148)
        Me.CMDUPDATE.Name = "CMDUPDATE"
        Me.CMDUPDATE.Size = New System.Drawing.Size(80, 28)
        Me.CMDUPDATE.TabIndex = 184
        Me.CMDUPDATE.Text = "&Update"
        Me.CMDUPDATE.UseVisualStyleBackColor = True
        '
        'CHKDATA
        '
        Me.CHKDATA.AutoSize = True
        Me.CHKDATA.BackColor = System.Drawing.Color.Transparent
        Me.CHKDATA.Checked = True
        Me.CHKDATA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHKDATA.Location = New System.Drawing.Point(30, 75)
        Me.CHKDATA.Name = "CHKDATA"
        Me.CHKDATA.Size = New System.Drawing.Size(133, 19)
        Me.CHKDATA.TabIndex = 1
        Me.CHKDATA.Text = "Block Data Transfer"
        Me.CHKDATA.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(147, 148)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 183
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CHKLEDGER
        '
        Me.CHKLEDGER.AutoSize = True
        Me.CHKLEDGER.BackColor = System.Drawing.Color.Transparent
        Me.CHKLEDGER.Checked = True
        Me.CHKLEDGER.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHKLEDGER.Location = New System.Drawing.Point(30, 25)
        Me.CHKLEDGER.Name = "CHKLEDGER"
        Me.CHKLEDGER.Size = New System.Drawing.Size(142, 19)
        Me.CHKLEDGER.TabIndex = 0
        Me.CHKLEDGER.Text = "Block Ledger Transfer"
        Me.CHKLEDGER.UseVisualStyleBackColor = False
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'CMDDELETE
        '
        Me.CMDDELETE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDELETE.ForeColor = System.Drawing.Color.Black
        Me.CMDDELETE.Location = New System.Drawing.Point(102, 182)
        Me.CMDDELETE.Name = "CMDDELETE"
        Me.CMDDELETE.Size = New System.Drawing.Size(80, 28)
        Me.CMDDELETE.TabIndex = 185
        Me.CMDDELETE.Text = "&Delete"
        Me.CMDDELETE.UseVisualStyleBackColor = True
        '
        'BlockDataTransfer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(288, 222)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "BlockDataTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Block Data Transfer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CHKSTOCK As CheckBox
    Friend WithEvents CHKOTHERMASTER As CheckBox
    Friend WithEvents CHKDATA As CheckBox
    Friend WithEvents CHKLEDGER As CheckBox
    Friend WithEvents CMDUPDATE As Button
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents CMDDELETE As Button
End Class
