<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemMonthlyStockStatement
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.GRIDREPORT = New System.Windows.Forms.DataGridView()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.TOOLWHATSAPP = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.GMONTHNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOPENING = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFINISHINWARD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPURRETURN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDYEINGREC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GISSPACKING = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRECPACKING = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GJOBOUT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GJOBIN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSTOCKOUT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSTOCKIN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GINVOICE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSALERETURN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRUNNINGBAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDREPORT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.GRIDREPORT)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1405, 442)
        Me.BlendPanel1.TabIndex = 9
        '
        'GRIDREPORT
        '
        Me.GRIDREPORT.AllowUserToAddRows = False
        Me.GRIDREPORT.AllowUserToDeleteRows = False
        Me.GRIDREPORT.AllowUserToResizeColumns = False
        Me.GRIDREPORT.AllowUserToResizeRows = False
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDREPORT.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDREPORT.BackgroundColor = System.Drawing.Color.White
        Me.GRIDREPORT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDREPORT.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDREPORT.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDREPORT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDREPORT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GMONTHNAME, Me.GOPENING, Me.GFINISHINWARD, Me.GPURRETURN, Me.GDYEINGREC, Me.GISSPACKING, Me.GRECPACKING, Me.GJOBOUT, Me.GJOBIN, Me.GSTOCKOUT, Me.GSTOCKIN, Me.GINVOICE, Me.GSALERETURN, Me.GRUNNINGBAL})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDREPORT.DefaultCellStyle = DataGridViewCellStyle4
        Me.GRIDREPORT.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDREPORT.Location = New System.Drawing.Point(12, 39)
        Me.GRIDREPORT.MultiSelect = False
        Me.GRIDREPORT.Name = "GRIDREPORT"
        Me.GRIDREPORT.ReadOnly = True
        Me.GRIDREPORT.RowHeadersVisible = False
        Me.GRIDREPORT.RowHeadersWidth = 30
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDREPORT.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDREPORT.RowTemplate.Height = 20
        Me.GRIDREPORT.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDREPORT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDREPORT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDREPORT.Size = New System.Drawing.Size(1376, 358)
        Me.GRIDREPORT.TabIndex = 687
        Me.GRIDREPORT.TabStop = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdexit.Location = New System.Drawing.Point(717, 403)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 2
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDREFRESH.Location = New System.Drawing.Point(632, 403)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREFRESH.TabIndex = 1
        Me.CMDREFRESH.Text = "&Refresh"
        Me.CMDREFRESH.UseVisualStyleBackColor = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripButton, Me.TOOLWHATSAPP, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1405, 25)
        Me.ToolStrip1.TabIndex = 255
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'TOOLWHATSAPP
        '
        Me.TOOLWHATSAPP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLWHATSAPP.Image = Global.TEXTRADE.My.Resources.Resources.WHATSAPP
        Me.TOOLWHATSAPP.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLWHATSAPP.Name = "TOOLWHATSAPP"
        Me.TOOLWHATSAPP.Size = New System.Drawing.Size(23, 22)
        Me.TOOLWHATSAPP.Text = "&Whatsapp"
        Me.TOOLWHATSAPP.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'GMONTHNAME
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GMONTHNAME.DefaultCellStyle = DataGridViewCellStyle3
        Me.GMONTHNAME.HeaderText = "Month Name"
        Me.GMONTHNAME.Name = "GMONTHNAME"
        Me.GMONTHNAME.ReadOnly = True
        Me.GMONTHNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMONTHNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GOPENING
        '
        Me.GOPENING.HeaderText = "Opening"
        Me.GOPENING.Name = "GOPENING"
        Me.GOPENING.ReadOnly = True
        Me.GOPENING.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPENING.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPENING.Width = 95
        '
        'GFINISHINWARD
        '
        Me.GFINISHINWARD.HeaderText = "Finish Inward"
        Me.GFINISHINWARD.Name = "GFINISHINWARD"
        Me.GFINISHINWARD.ReadOnly = True
        Me.GFINISHINWARD.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GFINISHINWARD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GFINISHINWARD.Width = 95
        '
        'GPURRETURN
        '
        Me.GPURRETURN.HeaderText = "Pur Return"
        Me.GPURRETURN.Name = "GPURRETURN"
        Me.GPURRETURN.ReadOnly = True
        Me.GPURRETURN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPURRETURN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPURRETURN.Width = 95
        '
        'GDYEINGREC
        '
        Me.GDYEINGREC.HeaderText = "Dyeing Rec"
        Me.GDYEINGREC.Name = "GDYEINGREC"
        Me.GDYEINGREC.ReadOnly = True
        Me.GDYEINGREC.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDYEINGREC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDYEINGREC.Width = 95
        '
        'GISSPACKING
        '
        Me.GISSPACKING.HeaderText = "Iss Packing"
        Me.GISSPACKING.Name = "GISSPACKING"
        Me.GISSPACKING.ReadOnly = True
        Me.GISSPACKING.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GISSPACKING.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GISSPACKING.Width = 95
        '
        'GRECPACKING
        '
        Me.GRECPACKING.HeaderText = "Rec Packing"
        Me.GRECPACKING.Name = "GRECPACKING"
        Me.GRECPACKING.ReadOnly = True
        Me.GRECPACKING.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRECPACKING.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GRECPACKING.Width = 95
        '
        'GJOBOUT
        '
        Me.GJOBOUT.HeaderText = "Job Out"
        Me.GJOBOUT.Name = "GJOBOUT"
        Me.GJOBOUT.ReadOnly = True
        Me.GJOBOUT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GJOBOUT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GJOBOUT.Width = 95
        '
        'GJOBIN
        '
        Me.GJOBIN.HeaderText = "Job In"
        Me.GJOBIN.Name = "GJOBIN"
        Me.GJOBIN.ReadOnly = True
        Me.GJOBIN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GJOBIN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GJOBIN.Width = 95
        '
        'GSTOCKOUT
        '
        Me.GSTOCKOUT.HeaderText = "Stock Out"
        Me.GSTOCKOUT.Name = "GSTOCKOUT"
        Me.GSTOCKOUT.ReadOnly = True
        Me.GSTOCKOUT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSTOCKOUT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSTOCKOUT.Width = 95
        '
        'GSTOCKIN
        '
        Me.GSTOCKIN.HeaderText = "Stock In"
        Me.GSTOCKIN.Name = "GSTOCKIN"
        Me.GSTOCKIN.ReadOnly = True
        Me.GSTOCKIN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSTOCKIN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSTOCKIN.Width = 95
        '
        'GINVOICE
        '
        Me.GINVOICE.HeaderText = "Sale Inv"
        Me.GINVOICE.Name = "GINVOICE"
        Me.GINVOICE.ReadOnly = True
        Me.GINVOICE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GINVOICE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GINVOICE.Width = 95
        '
        'GSALERETURN
        '
        Me.GSALERETURN.HeaderText = "Sale Ret"
        Me.GSALERETURN.Name = "GSALERETURN"
        Me.GSALERETURN.ReadOnly = True
        Me.GSALERETURN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSALERETURN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSALERETURN.Width = 95
        '
        'GRUNNINGBAL
        '
        Me.GRUNNINGBAL.HeaderText = "Run Bal"
        Me.GRUNNINGBAL.Name = "GRUNNINGBAL"
        Me.GRUNNINGBAL.ReadOnly = True
        Me.GRUNNINGBAL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRUNNINGBAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ItemMonthlyStockStatement
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1405, 442)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "ItemMonthlyStockStatement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Item Monthly Stock Statement"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDREPORT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents cmdexit As Button
    Friend WithEvents CMDREFRESH As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents TOOLWHATSAPP As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents GRIDREPORT As DataGridView
    Friend WithEvents GMONTHNAME As DataGridViewTextBoxColumn
    Friend WithEvents GOPENING As DataGridViewTextBoxColumn
    Friend WithEvents GFINISHINWARD As DataGridViewTextBoxColumn
    Friend WithEvents GPURRETURN As DataGridViewTextBoxColumn
    Friend WithEvents GDYEINGREC As DataGridViewTextBoxColumn
    Friend WithEvents GISSPACKING As DataGridViewTextBoxColumn
    Friend WithEvents GRECPACKING As DataGridViewTextBoxColumn
    Friend WithEvents GJOBOUT As DataGridViewTextBoxColumn
    Friend WithEvents GJOBIN As DataGridViewTextBoxColumn
    Friend WithEvents GSTOCKOUT As DataGridViewTextBoxColumn
    Friend WithEvents GSTOCKIN As DataGridViewTextBoxColumn
    Friend WithEvents GINVOICE As DataGridViewTextBoxColumn
    Friend WithEvents GSALERETURN As DataGridViewTextBoxColumn
    Friend WithEvents GRUNNINGBAL As DataGridViewTextBoxColumn
End Class
