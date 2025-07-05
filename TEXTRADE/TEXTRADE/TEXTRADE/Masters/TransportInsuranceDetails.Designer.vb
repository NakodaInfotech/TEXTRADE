<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TransportInsuranceDetails
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.TXTNO = New System.Windows.Forms.TextBox()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.CMBCHARGES = New System.Windows.Forms.ComboBox()
        Me.CMBCALC = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.WEFDATE = New System.Windows.Forms.MaskedTextBox()
        Me.TXTSRNO = New System.Windows.Forms.TextBox()
        Me.TXTPER = New System.Windows.Forms.TextBox()
        Me.TXTPOLICYNO = New System.Windows.Forms.TextBox()
        Me.TXTINSURER = New System.Windows.Forms.TextBox()
        Me.GRIDINSURANCE = New System.Windows.Forms.DataGridView()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GINSURER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPOLICYNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLEDGER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCALCON = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDINSURANCE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTNO)
        Me.BlendPanel1.Controls.Add(Me.CMBCODE)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.CMBCHARGES)
        Me.BlendPanel1.Controls.Add(Me.CMBCALC)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.WEFDATE)
        Me.BlendPanel1.Controls.Add(Me.TXTSRNO)
        Me.BlendPanel1.Controls.Add(Me.TXTPER)
        Me.BlendPanel1.Controls.Add(Me.TXTPOLICYNO)
        Me.BlendPanel1.Controls.Add(Me.TXTINSURER)
        Me.BlendPanel1.Controls.Add(Me.GRIDINSURANCE)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(921, 444)
        Me.BlendPanel1.TabIndex = 0
        '
        'TXTNO
        '
        Me.TXTNO.BackColor = System.Drawing.Color.White
        Me.TXTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTNO.Location = New System.Drawing.Point(489, 20)
        Me.TXTNO.Name = "TXTNO"
        Me.TXTNO.ReadOnly = True
        Me.TXTNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTNO.TabIndex = 741
        Me.TXTNO.Text = " "
        Me.TXTNO.Visible = False
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.Enabled = False
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Location = New System.Drawing.Point(882, 61)
        Me.CMBCODE.MaxDropDownItems = 14
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(11, 22)
        Me.CMBCODE.TabIndex = 740
        Me.CMBCODE.Visible = False
        '
        'TXTADD
        '
        Me.TXTADD.BackColor = System.Drawing.Color.White
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.ForeColor = System.Drawing.Color.DimGray
        Me.TXTADD.Location = New System.Drawing.Point(899, 61)
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.ReadOnly = True
        Me.TXTADD.Size = New System.Drawing.Size(10, 22)
        Me.TXTADD.TabIndex = 739
        Me.TXTADD.TabStop = False
        Me.TXTADD.Visible = False
        '
        'CMBCHARGES
        '
        Me.CMBCHARGES.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCHARGES.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCHARGES.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBCHARGES.FormattingEnabled = True
        Me.CMBCHARGES.Location = New System.Drawing.Point(512, 60)
        Me.CMBCHARGES.Name = "CMBCHARGES"
        Me.CMBCHARGES.Size = New System.Drawing.Size(200, 23)
        Me.CMBCHARGES.TabIndex = 4
        '
        'CMBCALC
        '
        Me.CMBCALC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCALC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCALC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBCALC.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCALC.FormattingEnabled = True
        Me.CMBCALC.Items.AddRange(New Object() {"GROSS", "NETT", "QTY", "MTRS"})
        Me.CMBCALC.Location = New System.Drawing.Point(792, 60)
        Me.CMBCALC.MaxDropDownItems = 14
        Me.CMBCALC.Name = "CMBCALC"
        Me.CMBCALC.Size = New System.Drawing.Size(80, 23)
        Me.CMBCALC.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(32, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(339, 25)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "Transport Insurance Details"
        '
        'WEFDATE
        '
        Me.WEFDATE.AsciiOnly = True
        Me.WEFDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.WEFDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.WEFDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.WEFDATE.Location = New System.Drawing.Point(62, 60)
        Me.WEFDATE.Mask = "00/00/0000"
        Me.WEFDATE.Name = "WEFDATE"
        Me.WEFDATE.Size = New System.Drawing.Size(100, 23)
        Me.WEFDATE.TabIndex = 1
        Me.WEFDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.WEFDATE.ValidatingType = GetType(Date)
        '
        'TXTSRNO
        '
        Me.TXTSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTSRNO.Location = New System.Drawing.Point(20, 60)
        Me.TXTSRNO.Name = "TXTSRNO"
        Me.TXTSRNO.Size = New System.Drawing.Size(42, 23)
        Me.TXTSRNO.TabIndex = 0
        Me.TXTSRNO.TabStop = False
        Me.TXTSRNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTPER
        '
        Me.TXTPER.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTPER.Location = New System.Drawing.Point(712, 60)
        Me.TXTPER.Name = "TXTPER"
        Me.TXTPER.Size = New System.Drawing.Size(80, 23)
        Me.TXTPER.TabIndex = 5
        '
        'TXTPOLICYNO
        '
        Me.TXTPOLICYNO.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTPOLICYNO.Location = New System.Drawing.Point(362, 60)
        Me.TXTPOLICYNO.Name = "TXTPOLICYNO"
        Me.TXTPOLICYNO.Size = New System.Drawing.Size(150, 23)
        Me.TXTPOLICYNO.TabIndex = 3
        '
        'TXTINSURER
        '
        Me.TXTINSURER.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTINSURER.Location = New System.Drawing.Point(162, 60)
        Me.TXTINSURER.Name = "TXTINSURER"
        Me.TXTINSURER.Size = New System.Drawing.Size(200, 23)
        Me.TXTINSURER.TabIndex = 2
        '
        'GRIDINSURANCE
        '
        Me.GRIDINSURANCE.AllowUserToAddRows = False
        Me.GRIDINSURANCE.AllowUserToDeleteRows = False
        Me.GRIDINSURANCE.AllowUserToResizeColumns = False
        Me.GRIDINSURANCE.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GRIDINSURANCE.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDINSURANCE.BackgroundColor = System.Drawing.Color.White
        Me.GRIDINSURANCE.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDINSURANCE.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDINSURANCE.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDINSURANCE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDINSURANCE.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GNO, Me.GDATE, Me.GINSURER, Me.GPOLICYNO, Me.GLEDGER, Me.GPER, Me.GCALCON})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDINSURANCE.DefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDINSURANCE.GridColor = System.Drawing.SystemColors.ControlText
        Me.GRIDINSURANCE.Location = New System.Drawing.Point(20, 83)
        Me.GRIDINSURANCE.Margin = New System.Windows.Forms.Padding(2)
        Me.GRIDINSURANCE.MultiSelect = False
        Me.GRIDINSURANCE.Name = "GRIDINSURANCE"
        Me.GRIDINSURANCE.ReadOnly = True
        Me.GRIDINSURANCE.RowHeadersVisible = False
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDINSURANCE.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDINSURANCE.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDINSURANCE.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDINSURANCE.Size = New System.Drawing.Size(880, 312)
        Me.GRIDINSURANCE.TabIndex = 27
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Location = New System.Drawing.Point(420, 400)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 10
        Me.CMDEXIT.Text = "&Exit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'GSRNO
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GSRNO.DefaultCellStyle = DataGridViewCellStyle3
        Me.GSRNO.HeaderText = "Sr No"
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.ReadOnly = True
        Me.GSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSRNO.Width = 40
        '
        'GNO
        '
        Me.GNO.HeaderText = "TINO"
        Me.GNO.Name = "GNO"
        Me.GNO.ReadOnly = True
        Me.GNO.Visible = False
        '
        'GDATE
        '
        Me.GDATE.HeaderText = "W.E.F. Date"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.ReadOnly = True
        Me.GDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GINSURER
        '
        Me.GINSURER.HeaderText = "Insurer Name"
        Me.GINSURER.Name = "GINSURER"
        Me.GINSURER.ReadOnly = True
        Me.GINSURER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GINSURER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GINSURER.Width = 200
        '
        'GPOLICYNO
        '
        Me.GPOLICYNO.HeaderText = "Policy No."
        Me.GPOLICYNO.Name = "GPOLICYNO"
        Me.GPOLICYNO.ReadOnly = True
        Me.GPOLICYNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPOLICYNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPOLICYNO.Width = 150
        '
        'GLEDGER
        '
        Me.GLEDGER.HeaderText = "Charges"
        Me.GLEDGER.Name = "GLEDGER"
        Me.GLEDGER.ReadOnly = True
        Me.GLEDGER.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GLEDGER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GLEDGER.Width = 200
        '
        'GPER
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GPER.DefaultCellStyle = DataGridViewCellStyle4
        Me.GPER.HeaderText = "Per."
        Me.GPER.Name = "GPER"
        Me.GPER.ReadOnly = True
        Me.GPER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPER.Width = 80
        '
        'GCALCON
        '
        Me.GCALCON.HeaderText = "Calc On"
        Me.GCALCON.Name = "GCALCON"
        Me.GCALCON.ReadOnly = True
        Me.GCALCON.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GCALCON.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCALCON.Width = 80
        '
        'TransportInsuranceDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(921, 444)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "TransportInsuranceDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Transport Insurance Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDINSURANCE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents WEFDATE As MaskedTextBox
    Friend WithEvents TXTSRNO As TextBox
    Friend WithEvents TXTPER As TextBox
    Friend WithEvents TXTPOLICYNO As TextBox
    Friend WithEvents TXTINSURER As TextBox
    Friend WithEvents GRIDINSURANCE As DataGridView
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents CMBCALC As ComboBox
    Friend WithEvents CMBCHARGES As ComboBox
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents TXTNO As TextBox
    Friend WithEvents GSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GNO As DataGridViewTextBoxColumn
    Friend WithEvents GDATE As DataGridViewTextBoxColumn
    Friend WithEvents GINSURER As DataGridViewTextBoxColumn
    Friend WithEvents GPOLICYNO As DataGridViewTextBoxColumn
    Friend WithEvents GLEDGER As DataGridViewTextBoxColumn
    Friend WithEvents GPER As DataGridViewTextBoxColumn
    Friend WithEvents GCALCON As DataGridViewTextBoxColumn
End Class
