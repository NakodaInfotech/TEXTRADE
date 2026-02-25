<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MasterTransfer
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CMBNEWCMP = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CMBOLDCMP = New System.Windows.Forms.ComboBox()
        Me.GBTRANSFERDATA = New System.Windows.Forms.GroupBox()
        Me.CHKOTHERMASTER = New System.Windows.Forms.CheckBox()
        Me.CHKDATA = New System.Windows.Forms.CheckBox()
        Me.CHKLEDGER = New System.Windows.Forms.CheckBox()
        Me.LBLUSER = New System.Windows.Forms.Label()
        Me.CMBUSER = New System.Windows.Forms.ComboBox()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.lbl = New System.Windows.Forms.Label()
        Me.BlendPanel1.SuspendLayout()
        Me.GBTRANSFERDATA.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.CMBNEWCMP)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.CMBOLDCMP)
        Me.BlendPanel1.Controls.Add(Me.GBTRANSFERDATA)
        Me.BlendPanel1.Controls.Add(Me.LBLUSER)
        Me.BlendPanel1.Controls.Add(Me.CMBUSER)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(327, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 14)
        Me.Label2.TabIndex = 629
        Me.Label2.Text = "New Company"
        Me.Label2.Visible = False
        '
        'CMBNEWCMP
        '
        Me.CMBNEWCMP.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNEWCMP.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNEWCMP.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNEWCMP.FormattingEnabled = True
        Me.CMBNEWCMP.Items.AddRange(New Object() {""})
        Me.CMBNEWCMP.Location = New System.Drawing.Point(416, 91)
        Me.CMBNEWCMP.Name = "CMBNEWCMP"
        Me.CMBNEWCMP.Size = New System.Drawing.Size(216, 22)
        Me.CMBNEWCMP.TabIndex = 628
        Me.CMBNEWCMP.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 14)
        Me.Label1.TabIndex = 627
        Me.Label1.Text = "Old Company"
        Me.Label1.Visible = False
        '
        'CMBOLDCMP
        '
        Me.CMBOLDCMP.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBOLDCMP.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBOLDCMP.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBOLDCMP.FormattingEnabled = True
        Me.CMBOLDCMP.Items.AddRange(New Object() {""})
        Me.CMBOLDCMP.Location = New System.Drawing.Point(90, 91)
        Me.CMBOLDCMP.Name = "CMBOLDCMP"
        Me.CMBOLDCMP.Size = New System.Drawing.Size(216, 22)
        Me.CMBOLDCMP.TabIndex = 626
        Me.CMBOLDCMP.Visible = False
        '
        'GBTRANSFERDATA
        '
        Me.GBTRANSFERDATA.BackColor = System.Drawing.Color.Transparent
        Me.GBTRANSFERDATA.Controls.Add(Me.CHKOTHERMASTER)
        Me.GBTRANSFERDATA.Controls.Add(Me.CHKDATA)
        Me.GBTRANSFERDATA.Controls.Add(Me.CHKLEDGER)
        Me.GBTRANSFERDATA.Location = New System.Drawing.Point(974, 72)
        Me.GBTRANSFERDATA.Name = "GBTRANSFERDATA"
        Me.GBTRANSFERDATA.Size = New System.Drawing.Size(220, 122)
        Me.GBTRANSFERDATA.TabIndex = 625
        Me.GBTRANSFERDATA.TabStop = False
        '
        'CHKOTHERMASTER
        '
        Me.CHKOTHERMASTER.AutoSize = True
        Me.CHKOTHERMASTER.Checked = True
        Me.CHKOTHERMASTER.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHKOTHERMASTER.Location = New System.Drawing.Point(6, 47)
        Me.CHKOTHERMASTER.Name = "CHKOTHERMASTER"
        Me.CHKOTHERMASTER.Size = New System.Drawing.Size(147, 19)
        Me.CHKOTHERMASTER.TabIndex = 2
        Me.CHKOTHERMASTER.Text = "Transfer Other Master"
        Me.CHKOTHERMASTER.UseVisualStyleBackColor = True
        '
        'CHKDATA
        '
        Me.CHKDATA.AutoSize = True
        Me.CHKDATA.Checked = True
        Me.CHKDATA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHKDATA.Location = New System.Drawing.Point(6, 72)
        Me.CHKDATA.Name = "CHKDATA"
        Me.CHKDATA.Size = New System.Drawing.Size(100, 19)
        Me.CHKDATA.TabIndex = 1
        Me.CHKDATA.Text = "Transfer Data"
        Me.CHKDATA.UseVisualStyleBackColor = True
        '
        'CHKLEDGER
        '
        Me.CHKLEDGER.AutoSize = True
        Me.CHKLEDGER.Checked = True
        Me.CHKLEDGER.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHKLEDGER.Location = New System.Drawing.Point(6, 22)
        Me.CHKLEDGER.Name = "CHKLEDGER"
        Me.CHKLEDGER.Size = New System.Drawing.Size(151, 19)
        Me.CHKLEDGER.TabIndex = 0
        Me.CHKLEDGER.Text = "Transfer Ledger Master"
        Me.CHKLEDGER.UseVisualStyleBackColor = True
        '
        'LBLUSER
        '
        Me.LBLUSER.AutoSize = True
        Me.LBLUSER.BackColor = System.Drawing.Color.Transparent
        Me.LBLUSER.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLUSER.Location = New System.Drawing.Point(21, 48)
        Me.LBLUSER.Name = "LBLUSER"
        Me.LBLUSER.Size = New System.Drawing.Size(67, 14)
        Me.LBLUSER.TabIndex = 624
        Me.LBLUSER.Text = "User Name"
        Me.LBLUSER.Visible = False
        '
        'CMBUSER
        '
        Me.CMBUSER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBUSER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBUSER.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBUSER.FormattingEnabled = True
        Me.CMBUSER.Items.AddRange(New Object() {""})
        Me.CMBUSER.Location = New System.Drawing.Point(90, 44)
        Me.CMBUSER.Name = "CMBUSER"
        Me.CMBUSER.Size = New System.Drawing.Size(216, 22)
        Me.CMBUSER.TabIndex = 623
        Me.CMBUSER.Visible = False
        '
        'CMDOK
        '
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(81, 350)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 184
        Me.CMDOK.Text = "&OK"
        Me.CMDOK.UseVisualStyleBackColor = True
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(167, 350)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 183
        Me.CMDEXIT.Text = "&Exit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.Black
        Me.lbl.Location = New System.Drawing.Point(20, 16)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(248, 14)
        Me.lbl.TabIndex = 182
        Me.lbl.Text = "Select Accounting Year To Transfer Data From"
        '
        'MasterTransfer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "MasterTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MasterTransfer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.GBTRANSFERDATA.ResumeLayout(False)
        Me.GBTRANSFERDATA.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents GBTRANSFERDATA As GroupBox
    Friend WithEvents CHKOTHERMASTER As CheckBox
    Friend WithEvents CHKDATA As CheckBox
    Friend WithEvents CHKLEDGER As CheckBox
    Friend WithEvents LBLUSER As Label
    Friend WithEvents CMBUSER As ComboBox
    Friend WithEvents CMDOK As Button
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents lbl As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents CMBNEWCMP As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CMBOLDCMP As ComboBox
End Class
