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
        Me.CHKLEDGER = New System.Windows.Forms.CheckBox()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.lbl = New System.Windows.Forms.Label()
        Me.CHKOTHERMASTER = New System.Windows.Forms.CheckBox()
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
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(653, 364)
        Me.BlendPanel1.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(324, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 14)
        Me.Label2.TabIndex = 629
        Me.Label2.Text = "New Company"
        '
        'CMBNEWCMP
        '
        Me.CMBNEWCMP.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNEWCMP.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNEWCMP.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNEWCMP.FormattingEnabled = True
        Me.CMBNEWCMP.Items.AddRange(New Object() {""})
        Me.CMBNEWCMP.Location = New System.Drawing.Point(413, 44)
        Me.CMBNEWCMP.Name = "CMBNEWCMP"
        Me.CMBNEWCMP.Size = New System.Drawing.Size(216, 22)
        Me.CMBNEWCMP.TabIndex = 628
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 14)
        Me.Label1.TabIndex = 627
        Me.Label1.Text = "Old Company"
        '
        'CMBOLDCMP
        '
        Me.CMBOLDCMP.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBOLDCMP.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBOLDCMP.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBOLDCMP.FormattingEnabled = True
        Me.CMBOLDCMP.Items.AddRange(New Object() {""})
        Me.CMBOLDCMP.Location = New System.Drawing.Point(87, 44)
        Me.CMBOLDCMP.Name = "CMBOLDCMP"
        Me.CMBOLDCMP.Size = New System.Drawing.Size(216, 22)
        Me.CMBOLDCMP.TabIndex = 626
        '
        'GBTRANSFERDATA
        '
        Me.GBTRANSFERDATA.BackColor = System.Drawing.Color.Transparent
        Me.GBTRANSFERDATA.Controls.Add(Me.CHKOTHERMASTER)
        Me.GBTRANSFERDATA.Controls.Add(Me.CHKLEDGER)
        Me.GBTRANSFERDATA.Location = New System.Drawing.Point(45, 95)
        Me.GBTRANSFERDATA.Name = "GBTRANSFERDATA"
        Me.GBTRANSFERDATA.Size = New System.Drawing.Size(220, 122)
        Me.GBTRANSFERDATA.TabIndex = 625
        Me.GBTRANSFERDATA.TabStop = False
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
        'CMDOK
        '
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(241, 245)
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
        Me.CMDEXIT.Location = New System.Drawing.Point(327, 245)
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
        Me.lbl.Size = New System.Drawing.Size(213, 14)
        Me.lbl.TabIndex = 182
        Me.lbl.Text = "Select Comapny To Transfer Data From"
        '
        'CHKOTHERMASTER
        '
        Me.CHKOTHERMASTER.AutoSize = True
        Me.CHKOTHERMASTER.Checked = True
        Me.CHKOTHERMASTER.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHKOTHERMASTER.Location = New System.Drawing.Point(6, 47)
        Me.CHKOTHERMASTER.Name = "CHKOTHERMASTER"
        Me.CHKOTHERMASTER.Size = New System.Drawing.Size(147, 19)
        Me.CHKOTHERMASTER.TabIndex = 3
        Me.CHKOTHERMASTER.Text = "Transfer Other Master"
        Me.CHKOTHERMASTER.UseVisualStyleBackColor = True
        '
        'MasterTransfer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(653, 364)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "MasterTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MasterTransfer"
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.GBTRANSFERDATA.ResumeLayout(False)
        Me.GBTRANSFERDATA.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents GBTRANSFERDATA As GroupBox
    Friend WithEvents CHKLEDGER As CheckBox
    Friend WithEvents CMDOK As Button
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents lbl As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents CMBNEWCMP As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CMBOLDCMP As ComboBox
    Friend WithEvents CHKOTHERMASTER As CheckBox
End Class
