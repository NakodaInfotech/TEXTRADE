<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class YearTransfer
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.GBTRANSFERDATA = New System.Windows.Forms.GroupBox()
        Me.LBLUSER = New System.Windows.Forms.Label()
        Me.CMBUSER = New System.Windows.Forms.ComboBox()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.GRIDYEAR = New System.Windows.Forms.DataGridView()
        Me.GYEAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GYEARID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSTARTDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GENDDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbl = New System.Windows.Forms.Label()
        Me.CHKLEDGER = New System.Windows.Forms.CheckBox()
        Me.CHKDATA = New System.Windows.Forms.CheckBox()
        Me.CHKOTHERMASTER = New System.Windows.Forms.CheckBox()
        Me.BlendPanel1.SuspendLayout()
        Me.GBTRANSFERDATA.SuspendLayout()
        CType(Me.GRIDYEAR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.GBTRANSFERDATA)
        Me.BlendPanel1.Controls.Add(Me.LBLUSER)
        Me.BlendPanel1.Controls.Add(Me.CMBUSER)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.GRIDYEAR)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(721, 388)
        Me.BlendPanel1.TabIndex = 17
        '
        'GBTRANSFERDATA
        '
        Me.GBTRANSFERDATA.BackColor = System.Drawing.Color.Transparent
        Me.GBTRANSFERDATA.Controls.Add(Me.CHKOTHERMASTER)
        Me.GBTRANSFERDATA.Controls.Add(Me.CHKDATA)
        Me.GBTRANSFERDATA.Controls.Add(Me.CHKLEDGER)
        Me.GBTRANSFERDATA.Location = New System.Drawing.Point(312, 72)
        Me.GBTRANSFERDATA.Name = "GBTRANSFERDATA"
        Me.GBTRANSFERDATA.Size = New System.Drawing.Size(332, 271)
        Me.GBTRANSFERDATA.TabIndex = 625
        Me.GBTRANSFERDATA.TabStop = False
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
        'GRIDYEAR
        '
        Me.GRIDYEAR.AllowUserToAddRows = False
        Me.GRIDYEAR.AllowUserToDeleteRows = False
        Me.GRIDYEAR.AllowUserToResizeColumns = False
        Me.GRIDYEAR.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDYEAR.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDYEAR.BackgroundColor = System.Drawing.Color.White
        Me.GRIDYEAR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDYEAR.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.GRIDYEAR.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.Transparent
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDYEAR.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDYEAR.ColumnHeadersHeight = 22
        Me.GRIDYEAR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GRIDYEAR.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GYEAR, Me.GYEARID, Me.GSTARTDATE, Me.GENDDATE})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDYEAR.DefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDYEAR.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GRIDYEAR.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDYEAR.Location = New System.Drawing.Point(23, 72)
        Me.GRIDYEAR.MultiSelect = False
        Me.GRIDYEAR.Name = "GRIDYEAR"
        Me.GRIDYEAR.ReadOnly = True
        Me.GRIDYEAR.RowHeadersVisible = False
        Me.GRIDYEAR.RowHeadersWidth = 30
        Me.GRIDYEAR.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDYEAR.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.GRIDYEAR.RowTemplate.Height = 20
        Me.GRIDYEAR.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDYEAR.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDYEAR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDYEAR.Size = New System.Drawing.Size(283, 271)
        Me.GRIDYEAR.TabIndex = 18
        '
        'GYEAR
        '
        Me.GYEAR.HeaderText = "Year "
        Me.GYEAR.Name = "GYEAR"
        Me.GYEAR.ReadOnly = True
        Me.GYEAR.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GYEAR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GYEAR.Width = 250
        '
        'GYEARID
        '
        Me.GYEARID.HeaderText = "YEARID"
        Me.GYEARID.Name = "GYEARID"
        Me.GYEARID.ReadOnly = True
        Me.GYEARID.Visible = False
        '
        'GSTARTDATE
        '
        Me.GSTARTDATE.HeaderText = "STARTDATE"
        Me.GSTARTDATE.Name = "GSTARTDATE"
        Me.GSTARTDATE.ReadOnly = True
        Me.GSTARTDATE.Visible = False
        '
        'GENDDATE
        '
        Me.GENDDATE.HeaderText = "ENDDATE"
        Me.GENDDATE.Name = "GENDDATE"
        Me.GENDDATE.ReadOnly = True
        Me.GENDDATE.Visible = False
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
        'YearTransfer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(721, 388)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "YearTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Year Transfer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.GBTRANSFERDATA.ResumeLayout(False)
        Me.GBTRANSFERDATA.PerformLayout()
        CType(Me.GRIDYEAR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents GRIDYEAR As System.Windows.Forms.DataGridView
    Friend WithEvents CMDOK As System.Windows.Forms.Button
    Friend WithEvents CMDEXIT As System.Windows.Forms.Button
    Friend WithEvents LBLUSER As System.Windows.Forms.Label
    Friend WithEvents CMBUSER As System.Windows.Forms.ComboBox
    Friend WithEvents GYEAR As DataGridViewTextBoxColumn
    Friend WithEvents GYEARID As DataGridViewTextBoxColumn
    Friend WithEvents GSTARTDATE As DataGridViewTextBoxColumn
    Friend WithEvents GENDDATE As DataGridViewTextBoxColumn
    Friend WithEvents GBTRANSFERDATA As GroupBox
    Friend WithEvents CHKDATA As CheckBox
    Friend WithEvents CHKLEDGER As CheckBox
    Friend WithEvents CHKOTHERMASTER As CheckBox
End Class
