<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReprintYarn
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.CMDPRINTPS = New System.Windows.Forms.Button()
        Me.lblbaleno = New System.Windows.Forms.Label()
        Me.TXTBALENO = New System.Windows.Forms.TextBox()
        Me.CMDSELECTSTOCK = New System.Windows.Forms.Button()
        Me.GRIDREPRINT = New System.Windows.Forms.DataGridView()
        Me.CHKBARCODE = New System.Windows.Forms.CheckBox()
        Me.cmdprint = New System.Windows.Forms.Button()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtbarcode = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtcopies = New System.Windows.Forms.TextBox()
        Me.PRINTDIALOG = New System.Windows.Forms.PrintDialog()
        Me.PRINTDOC = New System.Drawing.Printing.PrintDocument()
        Me.GFROMTYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFROMSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFROMNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GYARNDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSHELF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRACK = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBARCODE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGRIDREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GJOBBERLOTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCOLOR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDESIGN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMILLNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GYARNQUALITY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.GRIDREPRINT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CMDPRINTPS
        '
        Me.CMDPRINTPS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDPRINTPS.ForeColor = System.Drawing.Color.Black
        Me.CMDPRINTPS.Location = New System.Drawing.Point(750, 541)
        Me.CMDPRINTPS.Name = "CMDPRINTPS"
        Me.CMDPRINTPS.Size = New System.Drawing.Size(80, 28)
        Me.CMDPRINTPS.TabIndex = 702
        Me.CMDPRINTPS.Text = "Print PS"
        Me.CMDPRINTPS.UseVisualStyleBackColor = True
        Me.CMDPRINTPS.Visible = False
        '
        'lblbaleno
        '
        Me.lblbaleno.AutoSize = True
        Me.lblbaleno.BackColor = System.Drawing.Color.Transparent
        Me.lblbaleno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbaleno.Location = New System.Drawing.Point(494, 16)
        Me.lblbaleno.Name = "lblbaleno"
        Me.lblbaleno.Size = New System.Drawing.Size(50, 14)
        Me.lblbaleno.TabIndex = 701
        Me.lblbaleno.Text = "Bale No"
        Me.lblbaleno.Visible = False
        '
        'TXTBALENO
        '
        Me.TXTBALENO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTBALENO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBALENO.Location = New System.Drawing.Point(546, 12)
        Me.TXTBALENO.Name = "TXTBALENO"
        Me.TXTBALENO.Size = New System.Drawing.Size(88, 22)
        Me.TXTBALENO.TabIndex = 700
        Me.TXTBALENO.Visible = False
        '
        'CMDSELECTSTOCK
        '
        Me.CMDSELECTSTOCK.BackColor = System.Drawing.Color.Transparent
        Me.CMDSELECTSTOCK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSELECTSTOCK.FlatAppearance.BorderSize = 0
        Me.CMDSELECTSTOCK.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSELECTSTOCK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDSELECTSTOCK.Location = New System.Drawing.Point(491, 542)
        Me.CMDSELECTSTOCK.Name = "CMDSELECTSTOCK"
        Me.CMDSELECTSTOCK.Size = New System.Drawing.Size(81, 27)
        Me.CMDSELECTSTOCK.TabIndex = 699
        Me.CMDSELECTSTOCK.Text = "Select Stock"
        Me.CMDSELECTSTOCK.UseVisualStyleBackColor = False
        '
        'GRIDREPRINT
        '
        Me.GRIDREPRINT.AllowUserToAddRows = False
        Me.GRIDREPRINT.AllowUserToDeleteRows = False
        Me.GRIDREPRINT.AllowUserToResizeColumns = False
        Me.GRIDREPRINT.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDREPRINT.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDREPRINT.BackgroundColor = System.Drawing.Color.White
        Me.GRIDREPRINT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDREPRINT.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDREPRINT.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDREPRINT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDREPRINT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GYARNQUALITY, Me.GMILLNAME, Me.GDESIGN, Me.GCOLOR, Me.GJOBBERLOTNO, Me.GLRNO, Me.GGRIDREMARKS, Me.GWT, Me.GQTY, Me.GBARCODE, Me.GRACK, Me.GSHELF, Me.GYARNDATE, Me.GFROMNO, Me.GFROMSRNO, Me.GFROMTYPE})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDREPRINT.DefaultCellStyle = DataGridViewCellStyle3
        Me.GRIDREPRINT.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDREPRINT.Location = New System.Drawing.Point(18, 40)
        Me.GRIDREPRINT.MultiSelect = False
        Me.GRIDREPRINT.Name = "GRIDREPRINT"
        Me.GRIDREPRINT.RowHeadersVisible = False
        Me.GRIDREPRINT.RowHeadersWidth = 30
        Me.GRIDREPRINT.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDREPRINT.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.GRIDREPRINT.RowTemplate.Height = 20
        Me.GRIDREPRINT.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDREPRINT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDREPRINT.Size = New System.Drawing.Size(1199, 495)
        Me.GRIDREPRINT.TabIndex = 698
        Me.GRIDREPRINT.TabStop = False
        '
        'CHKBARCODE
        '
        Me.CHKBARCODE.AutoSize = True
        Me.CHKBARCODE.BackColor = System.Drawing.Color.Transparent
        Me.CHKBARCODE.Location = New System.Drawing.Point(332, 14)
        Me.CHKBARCODE.Name = "CHKBARCODE"
        Me.CHKBARCODE.Size = New System.Drawing.Size(136, 19)
        Me.CHKBARCODE.TabIndex = 693
        Me.CHKBARCODE.Text = "Whole Sale Barcode"
        Me.CHKBARCODE.UseVisualStyleBackColor = False
        Me.CHKBARCODE.Visible = False
        '
        'cmdprint
        '
        Me.cmdprint.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdprint.ForeColor = System.Drawing.Color.Black
        Me.cmdprint.Location = New System.Drawing.Point(578, 541)
        Me.cmdprint.Name = "cmdprint"
        Me.cmdprint.Size = New System.Drawing.Size(80, 28)
        Me.cmdprint.TabIndex = 694
        Me.cmdprint.Text = "&Print"
        Me.cmdprint.UseVisualStyleBackColor = True
        '
        'cmdcancel
        '
        Me.cmdcancel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.Black
        Me.cmdcancel.Location = New System.Drawing.Point(664, 541)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 695
        Me.cmdcancel.Text = "E&xit"
        Me.cmdcancel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(30, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 14)
        Me.Label2.TabIndex = 697
        Me.Label2.Text = "Barcode"
        '
        'txtbarcode
        '
        Me.txtbarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtbarcode.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbarcode.Location = New System.Drawing.Point(82, 12)
        Me.txtbarcode.Name = "txtbarcode"
        Me.txtbarcode.Size = New System.Drawing.Size(129, 22)
        Me.txtbarcode.TabIndex = 691
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(241, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 14)
        Me.Label9.TabIndex = 696
        Me.Label9.Text = "Copies"
        '
        'txtcopies
        '
        Me.txtcopies.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcopies.Location = New System.Drawing.Point(282, 12)
        Me.txtcopies.Name = "txtcopies"
        Me.txtcopies.Size = New System.Drawing.Size(44, 22)
        Me.txtcopies.TabIndex = 692
        Me.txtcopies.Text = "1"
        Me.txtcopies.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'PRINTDIALOG
        '
        Me.PRINTDIALOG.AllowSelection = True
        Me.PRINTDIALOG.AllowSomePages = True
        Me.PRINTDIALOG.ShowHelp = True
        Me.PRINTDIALOG.UseEXDialog = True
        '
        'GFROMTYPE
        '
        Me.GFROMTYPE.HeaderText = "FROMTYPE"
        Me.GFROMTYPE.Name = "GFROMTYPE"
        '
        'GFROMSRNO
        '
        Me.GFROMSRNO.HeaderText = "FROMSRNO"
        Me.GFROMSRNO.Name = "GFROMSRNO"
        '
        'GFROMNO
        '
        Me.GFROMNO.HeaderText = "FROMNO"
        Me.GFROMNO.Name = "GFROMNO"
        '
        'GYARNDATE
        '
        Me.GYARNDATE.HeaderText = "Yarn Date"
        Me.GYARNDATE.Name = "GYARNDATE"
        Me.GYARNDATE.ReadOnly = True
        Me.GYARNDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GYARNDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GSHELF
        '
        Me.GSHELF.HeaderText = "Shelf"
        Me.GSHELF.Name = "GSHELF"
        Me.GSHELF.ReadOnly = True
        Me.GSHELF.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSHELF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GRACK
        '
        Me.GRACK.HeaderText = "Rack"
        Me.GRACK.Name = "GRACK"
        Me.GRACK.ReadOnly = True
        Me.GRACK.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRACK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GBARCODE
        '
        Me.GBARCODE.HeaderText = "Barcode"
        Me.GBARCODE.Name = "GBARCODE"
        Me.GBARCODE.ReadOnly = True
        Me.GBARCODE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBARCODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBARCODE.Width = 110
        '
        'GQTY
        '
        Me.GQTY.HeaderText = "Qty"
        Me.GQTY.Name = "GQTY"
        Me.GQTY.ReadOnly = True
        Me.GQTY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GQTY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GQTY.Width = 60
        '
        'GWT
        '
        Me.GWT.HeaderText = "Wt"
        Me.GWT.Name = "GWT"
        Me.GWT.ReadOnly = True
        Me.GWT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GWT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GWT.Width = 80
        '
        'GGRIDREMARKS
        '
        Me.GGRIDREMARKS.HeaderText = "Description"
        Me.GGRIDREMARKS.Name = "GGRIDREMARKS"
        Me.GGRIDREMARKS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGRIDREMARKS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GGRIDREMARKS.Width = 130
        '
        'GLRNO
        '
        Me.GLRNO.HeaderText = "Lr No"
        Me.GLRNO.Name = "GLRNO"
        Me.GLRNO.ReadOnly = True
        Me.GLRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GJOBBERLOTNO
        '
        Me.GJOBBERLOTNO.HeaderText = "Jobber Lot No"
        Me.GJOBBERLOTNO.Name = "GJOBBERLOTNO"
        Me.GJOBBERLOTNO.ReadOnly = True
        Me.GJOBBERLOTNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GJOBBERLOTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GCOLOR
        '
        Me.GCOLOR.HeaderText = "Shade"
        Me.GCOLOR.Name = "GCOLOR"
        Me.GCOLOR.ReadOnly = True
        Me.GCOLOR.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCOLOR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCOLOR.Width = 150
        '
        'GDESIGN
        '
        Me.GDESIGN.HeaderText = "Design"
        Me.GDESIGN.Name = "GDESIGN"
        Me.GDESIGN.ReadOnly = True
        Me.GDESIGN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDESIGN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDESIGN.Width = 150
        '
        'GMILLNAME
        '
        Me.GMILLNAME.HeaderText = "Mill Name"
        Me.GMILLNAME.Name = "GMILLNAME"
        Me.GMILLNAME.ReadOnly = True
        Me.GMILLNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMILLNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMILLNAME.Width = 130
        '
        'GYARNQUALITY
        '
        Me.GYARNQUALITY.HeaderText = "Yarn Quality"
        Me.GYARNQUALITY.Name = "GYARNQUALITY"
        Me.GYARNQUALITY.ReadOnly = True
        Me.GYARNQUALITY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GYARNQUALITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GYARNQUALITY.Width = 180
        '
        'GSRNO
        '
        Me.GSRNO.HeaderText = "Sr."
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.ReadOnly = True
        Me.GSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSRNO.Width = 30
        '
        'ReprintYarn
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.CMDPRINTPS)
        Me.Controls.Add(Me.lblbaleno)
        Me.Controls.Add(Me.TXTBALENO)
        Me.Controls.Add(Me.CMDSELECTSTOCK)
        Me.Controls.Add(Me.GRIDREPRINT)
        Me.Controls.Add(Me.CHKBARCODE)
        Me.Controls.Add(Me.cmdprint)
        Me.Controls.Add(Me.cmdcancel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtbarcode)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtcopies)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "ReprintYarn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Reprint Yarn"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GRIDREPRINT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CMDPRINTPS As Button
    Friend WithEvents lblbaleno As Label
    Friend WithEvents TXTBALENO As TextBox
    Friend WithEvents CMDSELECTSTOCK As Button
    Friend WithEvents GRIDREPRINT As DataGridView
    Friend WithEvents CHKBARCODE As CheckBox
    Friend WithEvents cmdprint As Button
    Friend WithEvents cmdcancel As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtbarcode As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtcopies As TextBox
    Friend WithEvents PRINTDIALOG As PrintDialog
    Friend WithEvents PRINTDOC As System.Drawing.Printing.PrintDocument
    Friend WithEvents GSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GYARNQUALITY As DataGridViewTextBoxColumn
    Friend WithEvents GMILLNAME As DataGridViewTextBoxColumn
    Friend WithEvents GDESIGN As DataGridViewTextBoxColumn
    Friend WithEvents GCOLOR As DataGridViewTextBoxColumn
    Friend WithEvents GJOBBERLOTNO As DataGridViewTextBoxColumn
    Friend WithEvents GLRNO As DataGridViewTextBoxColumn
    Friend WithEvents GGRIDREMARKS As DataGridViewTextBoxColumn
    Friend WithEvents GWT As DataGridViewTextBoxColumn
    Friend WithEvents GQTY As DataGridViewTextBoxColumn
    Friend WithEvents GBARCODE As DataGridViewTextBoxColumn
    Friend WithEvents GRACK As DataGridViewTextBoxColumn
    Friend WithEvents GSHELF As DataGridViewTextBoxColumn
    Friend WithEvents GYARNDATE As DataGridViewTextBoxColumn
    Friend WithEvents GFROMNO As DataGridViewTextBoxColumn
    Friend WithEvents GFROMSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GFROMTYPE As DataGridViewTextBoxColumn
End Class
