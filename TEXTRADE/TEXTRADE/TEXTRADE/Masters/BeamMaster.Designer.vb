<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BeamMaster
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.GPGRID = New System.Windows.Forms.GroupBox()
        Me.LBLTOTAL = New System.Windows.Forms.Label()
        Me.TXTTOTALWT = New System.Windows.Forms.TextBox()
        Me.TXTTOTALENDS = New System.Windows.Forms.TextBox()
        Me.GRIDBEAM = New System.Windows.Forms.DataGridView()
        Me.TXTGRIDWT = New System.Windows.Forms.TextBox()
        Me.TXTGRIDENDS = New System.Windows.Forms.TextBox()
        Me.CMBSHADE = New System.Windows.Forms.ComboBox()
        Me.CMBGRIDQUALITY = New System.Windows.Forms.ComboBox()
        Me.TXTSRNO = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TXTWTTL = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TXTTL = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TXTHSNCODE = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TXTBEAMDESC = New System.Windows.Forms.TextBox()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GSRNO = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.GYARNQUALITY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSHADE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GENDS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GWTPER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BlendPanel1.SuspendLayout()
        Me.GPGRID.SuspendLayout()
        CType(Me.GRIDBEAM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.GPGRID)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.TXTWTTL)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Controls.Add(Me.TXTTL)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.TXTHSNCODE)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.TXTBEAMDESC)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(641, 446)
        Me.BlendPanel1.TabIndex = 1
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdexit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdexit.Location = New System.Drawing.Point(409, 406)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 9
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmddelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmddelete.Location = New System.Drawing.Point(323, 406)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 28)
        Me.cmddelete.TabIndex = 8
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.Cursor = System.Windows.Forms.Cursors.Default
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDCLEAR.Location = New System.Drawing.Point(237, 406)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 354
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdok.Location = New System.Drawing.Point(151, 406)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 6
        Me.cmdok.Text = "&Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'GPGRID
        '
        Me.GPGRID.BackColor = System.Drawing.Color.Transparent
        Me.GPGRID.Controls.Add(Me.LBLTOTAL)
        Me.GPGRID.Controls.Add(Me.TXTTOTALWT)
        Me.GPGRID.Controls.Add(Me.TXTTOTALENDS)
        Me.GPGRID.Controls.Add(Me.GRIDBEAM)
        Me.GPGRID.Controls.Add(Me.TXTGRIDWT)
        Me.GPGRID.Controls.Add(Me.TXTGRIDENDS)
        Me.GPGRID.Controls.Add(Me.CMBSHADE)
        Me.GPGRID.Controls.Add(Me.CMBGRIDQUALITY)
        Me.GPGRID.Controls.Add(Me.TXTSRNO)
        Me.GPGRID.Location = New System.Drawing.Point(20, 107)
        Me.GPGRID.Name = "GPGRID"
        Me.GPGRID.Size = New System.Drawing.Size(601, 217)
        Me.GPGRID.TabIndex = 5
        Me.GPGRID.TabStop = False
        '
        'LBLTOTAL
        '
        Me.LBLTOTAL.AutoSize = True
        Me.LBLTOTAL.Location = New System.Drawing.Point(349, 190)
        Me.LBLTOTAL.Name = "LBLTOTAL"
        Me.LBLTOTAL.Size = New System.Drawing.Size(34, 15)
        Me.LBLTOTAL.TabIndex = 845
        Me.LBLTOTAL.Text = "Total"
        '
        'TXTTOTALWT
        '
        Me.TXTTOTALWT.BackColor = System.Drawing.Color.Linen
        Me.TXTTOTALWT.Location = New System.Drawing.Point(464, 186)
        Me.TXTTOTALWT.Name = "TXTTOTALWT"
        Me.TXTTOTALWT.ReadOnly = True
        Me.TXTTOTALWT.Size = New System.Drawing.Size(100, 23)
        Me.TXTTOTALWT.TabIndex = 844
        Me.TXTTOTALWT.TabStop = False
        Me.TXTTOTALWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTTOTALENDS
        '
        Me.TXTTOTALENDS.BackColor = System.Drawing.Color.Linen
        Me.TXTTOTALENDS.Location = New System.Drawing.Point(384, 186)
        Me.TXTTOTALENDS.Name = "TXTTOTALENDS"
        Me.TXTTOTALENDS.ReadOnly = True
        Me.TXTTOTALENDS.Size = New System.Drawing.Size(80, 23)
        Me.TXTTOTALENDS.TabIndex = 843
        Me.TXTTOTALENDS.TabStop = False
        Me.TXTTOTALENDS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GRIDBEAM
        '
        Me.GRIDBEAM.AllowUserToAddRows = False
        Me.GRIDBEAM.AllowUserToDeleteRows = False
        Me.GRIDBEAM.AllowUserToResizeColumns = False
        Me.GRIDBEAM.AllowUserToResizeRows = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDBEAM.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDBEAM.BackgroundColor = System.Drawing.Color.White
        Me.GRIDBEAM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDBEAM.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDBEAM.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDBEAM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDBEAM.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GYARNQUALITY, Me.GSHADE, Me.GENDS, Me.GWTPER})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDBEAM.DefaultCellStyle = DataGridViewCellStyle9
        Me.GRIDBEAM.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDBEAM.Location = New System.Drawing.Point(3, 33)
        Me.GRIDBEAM.MultiSelect = False
        Me.GRIDBEAM.Name = "GRIDBEAM"
        Me.GRIDBEAM.ReadOnly = True
        Me.GRIDBEAM.RowHeadersVisible = False
        Me.GRIDBEAM.RowHeadersWidth = 30
        Me.GRIDBEAM.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDBEAM.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.GRIDBEAM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDBEAM.Size = New System.Drawing.Size(593, 150)
        Me.GRIDBEAM.TabIndex = 4
        Me.GRIDBEAM.TabStop = False
        '
        'TXTGRIDWT
        '
        Me.TXTGRIDWT.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTGRIDWT.Location = New System.Drawing.Point(464, 10)
        Me.TXTGRIDWT.Name = "TXTGRIDWT"
        Me.TXTGRIDWT.Size = New System.Drawing.Size(100, 23)
        Me.TXTGRIDWT.TabIndex = 3
        Me.TXTGRIDWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTGRIDENDS
        '
        Me.TXTGRIDENDS.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTGRIDENDS.Location = New System.Drawing.Point(384, 10)
        Me.TXTGRIDENDS.Name = "TXTGRIDENDS"
        Me.TXTGRIDENDS.Size = New System.Drawing.Size(80, 23)
        Me.TXTGRIDENDS.TabIndex = 2
        Me.TXTGRIDENDS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBSHADE
        '
        Me.CMBSHADE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSHADE.BackColor = System.Drawing.SystemColors.Window
        Me.CMBSHADE.DropDownWidth = 150
        Me.CMBSHADE.FormattingEnabled = True
        Me.CMBSHADE.Location = New System.Drawing.Point(234, 10)
        Me.CMBSHADE.Name = "CMBSHADE"
        Me.CMBSHADE.Size = New System.Drawing.Size(150, 23)
        Me.CMBSHADE.TabIndex = 1
        '
        'CMBGRIDQUALITY
        '
        Me.CMBGRIDQUALITY.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBGRIDQUALITY.DropDownWidth = 200
        Me.CMBGRIDQUALITY.FormattingEnabled = True
        Me.CMBGRIDQUALITY.Location = New System.Drawing.Point(34, 10)
        Me.CMBGRIDQUALITY.Name = "CMBGRIDQUALITY"
        Me.CMBGRIDQUALITY.Size = New System.Drawing.Size(200, 23)
        Me.CMBGRIDQUALITY.TabIndex = 0
        '
        'TXTSRNO
        '
        Me.TXTSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTSRNO.Location = New System.Drawing.Point(4, 10)
        Me.TXTSRNO.Name = "TXTSRNO"
        Me.TXTSRNO.ReadOnly = True
        Me.TXTSRNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTSRNO.TabIndex = 840
        Me.TXTSRNO.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(115, 75)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(159, 15)
        Me.Label5.TabIndex = 696
        Me.Label5.Text = "Press 'F1' To Select HSN/SAC"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(284, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 15)
        Me.Label4.TabIndex = 341
        Me.Label4.Text = "Wt./TL"
        '
        'TXTWTTL
        '
        Me.TXTWTTL.BackColor = System.Drawing.Color.White
        Me.TXTWTTL.Location = New System.Drawing.Point(327, 46)
        Me.TXTWTTL.Name = "TXTWTTL"
        Me.TXTWTTL.Size = New System.Drawing.Size(46, 23)
        Me.TXTWTTL.TabIndex = 3
        Me.TXTWTTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(204, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(18, 15)
        Me.Label3.TabIndex = 335
        Me.Label3.Text = "TL"
        '
        'TXTTL
        '
        Me.TXTTL.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTTL.Location = New System.Drawing.Point(224, 46)
        Me.TXTTL.Name = "TXTTL"
        Me.TXTTL.Size = New System.Drawing.Size(46, 23)
        Me.TXTTL.TabIndex = 2
        Me.TXTTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(27, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 14)
        Me.Label2.TabIndex = 352
        Me.Label2.Text = "HSN / SAC Code"
        '
        'TXTHSNCODE
        '
        Me.TXTHSNCODE.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTHSNCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTHSNCODE.Location = New System.Drawing.Point(118, 46)
        Me.TXTHSNCODE.Name = "TXTHSNCODE"
        Me.TXTHSNCODE.Size = New System.Drawing.Size(68, 22)
        Me.TXTHSNCODE.TabIndex = 1
        Me.TXTHSNCODE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(44, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 15)
        Me.Label1.TabIndex = 310
        Me.Label1.Text = "Beam Name"
        '
        'TXTBEAMDESC
        '
        Me.TXTBEAMDESC.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTBEAMDESC.Location = New System.Drawing.Point(118, 17)
        Me.TXTBEAMDESC.Name = "TXTBEAMDESC"
        Me.TXTBEAMDESC.Size = New System.Drawing.Size(255, 23)
        Me.TXTBEAMDESC.TabIndex = 0
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'GSRNO
        '
        Me.GSRNO.DefaultCellStyle = DataGridViewCellStyle8
        Me.GSRNO.HeaderText = "Sr."
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.ReadOnly = True
        Me.GSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSRNO.Width = 30
        '
        'GYARNQUALITY
        '
        Me.GYARNQUALITY.HeaderText = "Yarn Quality"
        Me.GYARNQUALITY.Name = "GYARNQUALITY"
        Me.GYARNQUALITY.ReadOnly = True
        Me.GYARNQUALITY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GYARNQUALITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GYARNQUALITY.Width = 200
        '
        'GSHADE
        '
        Me.GSHADE.HeaderText = "Shade"
        Me.GSHADE.Name = "GSHADE"
        Me.GSHADE.ReadOnly = True
        Me.GSHADE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSHADE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSHADE.Width = 150
        '
        'GENDS
        '
        Me.GENDS.HeaderText = "Ends"
        Me.GENDS.Name = "GENDS"
        Me.GENDS.ReadOnly = True
        Me.GENDS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GENDS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GENDS.Width = 80
        '
        'GWTPER
        '
        Me.GWTPER.HeaderText = "Wt/100 Mtrs"
        Me.GWTPER.Name = "GWTPER"
        Me.GWTPER.ReadOnly = True
        Me.GWTPER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GWTPER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'BeamMaster
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(641, 446)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "BeamMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Beam Master"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.GPGRID.ResumeLayout(False)
        Me.GPGRID.PerformLayout()
        CType(Me.GRIDBEAM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents TXTBEAMDESC As TextBox
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents Label1 As Label
    Friend WithEvents TXTHSNCODE As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TXTTL As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TXTWTTL As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents GPGRID As GroupBox
    Friend WithEvents TXTSRNO As TextBox
    Friend WithEvents CMBSHADE As ComboBox
    Friend WithEvents CMBGRIDQUALITY As ComboBox
    Friend WithEvents TXTGRIDENDS As TextBox
    Friend WithEvents TXTGRIDWT As TextBox
    Friend WithEvents GRIDBEAM As DataGridView
    Friend WithEvents TXTTOTALENDS As TextBox
    Friend WithEvents TXTTOTALWT As TextBox
    Friend WithEvents CMDCLEAR As Button
    Friend WithEvents cmdok As Button
    Friend WithEvents cmddelete As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents LBLTOTAL As Label
    Friend WithEvents GSRNO As DataGridViewButtonColumn
    Friend WithEvents GYARNQUALITY As DataGridViewTextBoxColumn
    Friend WithEvents GSHADE As DataGridViewTextBoxColumn
    Friend WithEvents GENDS As DataGridViewTextBoxColumn
    Friend WithEvents GWTPER As DataGridViewTextBoxColumn
End Class
