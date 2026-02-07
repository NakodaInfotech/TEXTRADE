<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UploadExcel_MASHOK
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
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.CMDEXCEL = New System.Windows.Forms.Button()
        Me.GRIDERROR = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.EROWNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ENAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.EPARTYBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.EGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.EERROR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.EINVOICENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GRIDCOMPLETE = New DevExpress.XtraGrid.GridControl()
        Me.GRIDITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GROWNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPARTYBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVOICENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cmbregister = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LBLTYPE = New System.Windows.Forms.Label()
        Me.CMBTYPE = New System.Windows.Forms.ComboBox()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.TXTFILENAME = New System.Windows.Forms.TextBox()
        Me.CMDSELECTFILE = New System.Windows.Forms.Button()
        Me.TXTPATH = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CMDUPLOAD = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDERROR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDCOMPLETE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMDEXCEL)
        Me.BlendPanel1.Controls.Add(Me.GRIDERROR)
        Me.BlendPanel1.Controls.Add(Me.GRIDCOMPLETE)
        Me.BlendPanel1.Controls.Add(Me.cmbregister)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.LBLTYPE)
        Me.BlendPanel1.Controls.Add(Me.CMBTYPE)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.TXTFILENAME)
        Me.BlendPanel1.Controls.Add(Me.CMDSELECTFILE)
        Me.BlendPanel1.Controls.Add(Me.TXTPATH)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.CMDUPLOAD)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 0
        '
        'CMDEXCEL
        '
        Me.CMDEXCEL.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXCEL.FlatAppearance.BorderSize = 0
        Me.CMDEXCEL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXCEL.ForeColor = System.Drawing.Color.Black
        Me.CMDEXCEL.Location = New System.Drawing.Point(492, 516)
        Me.CMDEXCEL.Name = "CMDEXCEL"
        Me.CMDEXCEL.Size = New System.Drawing.Size(109, 28)
        Me.CMDEXCEL.TabIndex = 718
        Me.CMDEXCEL.Text = "Generate Excel"
        Me.CMDEXCEL.UseVisualStyleBackColor = False
        '
        'GRIDERROR
        '
        Me.GRIDERROR.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDERROR.Location = New System.Drawing.Point(523, 188)
        Me.GRIDERROR.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDERROR.MainView = Me.GridView1
        Me.GRIDERROR.Name = "GRIDERROR"
        Me.GRIDERROR.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.GRIDERROR.Size = New System.Drawing.Size(709, 297)
        Me.GRIDERROR.TabIndex = 717
        Me.GRIDERROR.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.EROWNO, Me.ENAME, Me.EPARTYBILLNO, Me.EGRANDTOTAL, Me.EERROR, Me.EINVOICENO})
        Me.GridView1.GridControl = Me.GRIDERROR
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AllowIncrementalSearch = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'EROWNO
        '
        Me.EROWNO.Caption = "Row No"
        Me.EROWNO.FieldName = "ROWNO"
        Me.EROWNO.Name = "EROWNO"
        Me.EROWNO.Visible = True
        Me.EROWNO.VisibleIndex = 0
        Me.EROWNO.Width = 40
        '
        'ENAME
        '
        Me.ENAME.Caption = "Name"
        Me.ENAME.FieldName = "NAME"
        Me.ENAME.Name = "ENAME"
        Me.ENAME.Visible = True
        Me.ENAME.VisibleIndex = 1
        Me.ENAME.Width = 200
        '
        'EPARTYBILLNO
        '
        Me.EPARTYBILLNO.Caption = "Party Bill No/So No"
        Me.EPARTYBILLNO.FieldName = "PARTYBILLNO"
        Me.EPARTYBILLNO.Name = "EPARTYBILLNO"
        Me.EPARTYBILLNO.Visible = True
        Me.EPARTYBILLNO.VisibleIndex = 2
        '
        'EGRANDTOTAL
        '
        Me.EGRANDTOTAL.Caption = "Grand Total/Total Mtrs"
        Me.EGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.EGRANDTOTAL.Name = "EGRANDTOTAL"
        Me.EGRANDTOTAL.Visible = True
        Me.EGRANDTOTAL.VisibleIndex = 3
        Me.EGRANDTOTAL.Width = 100
        '
        'EERROR
        '
        Me.EERROR.Caption = "Error"
        Me.EERROR.FieldName = "ERROR"
        Me.EERROR.Name = "EERROR"
        Me.EERROR.Visible = True
        Me.EERROR.VisibleIndex = 4
        Me.EERROR.Width = 200
        '
        'EINVOICENO
        '
        Me.EINVOICENO.Caption = "Invoice No"
        Me.EINVOICENO.FieldName = "INVOICENO"
        Me.EINVOICENO.Name = "EINVOICENO"
        Me.EINVOICENO.Visible = True
        Me.EINVOICENO.VisibleIndex = 5
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GRIDCOMPLETE
        '
        Me.GRIDCOMPLETE.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDCOMPLETE.Location = New System.Drawing.Point(26, 188)
        Me.GRIDCOMPLETE.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDCOMPLETE.MainView = Me.GRIDITEM
        Me.GRIDCOMPLETE.Name = "GRIDCOMPLETE"
        Me.GRIDCOMPLETE.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit4})
        Me.GRIDCOMPLETE.Size = New System.Drawing.Size(444, 297)
        Me.GRIDCOMPLETE.TabIndex = 716
        Me.GRIDCOMPLETE.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDITEM})
        '
        'GRIDITEM
        '
        Me.GRIDITEM.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDITEM.Appearance.Row.Options.UseFont = True
        Me.GRIDITEM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GROWNO, Me.GNAME, Me.GPARTYBILLNO, Me.GGRANDTOTAL, Me.GINVOICENO})
        Me.GRIDITEM.GridControl = Me.GRIDCOMPLETE
        Me.GRIDITEM.Name = "GRIDITEM"
        Me.GRIDITEM.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDITEM.OptionsView.ColumnAutoWidth = False
        Me.GRIDITEM.OptionsView.ShowAutoFilterRow = True
        Me.GRIDITEM.OptionsView.ShowGroupPanel = False
        '
        'GROWNO
        '
        Me.GROWNO.Caption = "Row No"
        Me.GROWNO.FieldName = "ROWNO"
        Me.GROWNO.Name = "GROWNO"
        Me.GROWNO.Visible = True
        Me.GROWNO.VisibleIndex = 0
        Me.GROWNO.Width = 40
        '
        'GNAME
        '
        Me.GNAME.Caption = "Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 1
        Me.GNAME.Width = 200
        '
        'GPARTYBILLNO
        '
        Me.GPARTYBILLNO.Caption = "Party Bill No/ So No"
        Me.GPARTYBILLNO.FieldName = "PARTYBILLNO"
        Me.GPARTYBILLNO.Name = "GPARTYBILLNO"
        Me.GPARTYBILLNO.Visible = True
        Me.GPARTYBILLNO.VisibleIndex = 2
        '
        'GGRANDTOTAL
        '
        Me.GGRANDTOTAL.Caption = "Grand Total/Total Mtrs"
        Me.GGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.GGRANDTOTAL.Name = "GGRANDTOTAL"
        Me.GGRANDTOTAL.Visible = True
        Me.GGRANDTOTAL.VisibleIndex = 3
        Me.GGRANDTOTAL.Width = 100
        '
        'GINVOICENO
        '
        Me.GINVOICENO.Caption = "Invoice No"
        Me.GINVOICENO.FieldName = "INVOICENO"
        Me.GINVOICENO.Name = "GINVOICENO"
        Me.GINVOICENO.Visible = True
        Me.GINVOICENO.VisibleIndex = 4
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        Me.RepositoryItemCheckEdit4.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'cmbregister
        '
        Me.cmbregister.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbregister.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbregister.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbregister.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbregister.FormattingEnabled = True
        Me.cmbregister.Items.AddRange(New Object() {""})
        Me.cmbregister.Location = New System.Drawing.Point(67, 41)
        Me.cmbregister.Name = "cmbregister"
        Me.cmbregister.Size = New System.Drawing.Size(280, 23)
        Me.cmbregister.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(13, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 15)
        Me.Label5.TabIndex = 715
        Me.Label5.Text = "Register"
        '
        'LBLTYPE
        '
        Me.LBLTYPE.AutoSize = True
        Me.LBLTYPE.BackColor = System.Drawing.Color.Transparent
        Me.LBLTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTYPE.ForeColor = System.Drawing.Color.Black
        Me.LBLTYPE.Location = New System.Drawing.Point(34, 16)
        Me.LBLTYPE.Name = "LBLTYPE"
        Me.LBLTYPE.Size = New System.Drawing.Size(31, 15)
        Me.LBLTYPE.TabIndex = 674
        Me.LBLTYPE.Text = "Type"
        '
        'CMBTYPE
        '
        Me.CMBTYPE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTYPE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTYPE.BackColor = System.Drawing.Color.White
        Me.CMBTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBTYPE.Enabled = False
        Me.CMBTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTYPE.FormattingEnabled = True
        Me.CMBTYPE.Items.AddRange(New Object() {"NONPURCHASE", "INVOICE"})
        Me.CMBTYPE.Location = New System.Drawing.Point(67, 12)
        Me.CMBTYPE.MaxDropDownItems = 14
        Me.CMBTYPE.Name = "CMBTYPE"
        Me.CMBTYPE.Size = New System.Drawing.Size(156, 23)
        Me.CMBTYPE.TabIndex = 0
        Me.CMBTYPE.TabStop = False
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.FlatAppearance.BorderSize = 0
        Me.CMDCLEAR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.Black
        Me.CMDCLEAR.Location = New System.Drawing.Point(192, 125)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 4
        Me.CMDCLEAR.Text = "Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
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
        'CMDSELECTFILE
        '
        Me.CMDSELECTFILE.BackColor = System.Drawing.Color.Transparent
        Me.CMDSELECTFILE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSELECTFILE.FlatAppearance.BorderSize = 0
        Me.CMDSELECTFILE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSELECTFILE.ForeColor = System.Drawing.Color.Black
        Me.CMDSELECTFILE.Location = New System.Drawing.Point(20, 125)
        Me.CMDSELECTFILE.Name = "CMDSELECTFILE"
        Me.CMDSELECTFILE.Size = New System.Drawing.Size(80, 28)
        Me.CMDSELECTFILE.TabIndex = 2
        Me.CMDSELECTFILE.Text = "Select File"
        Me.CMDSELECTFILE.UseVisualStyleBackColor = False
        '
        'TXTPATH
        '
        Me.TXTPATH.BackColor = System.Drawing.Color.Linen
        Me.TXTPATH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPATH.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTPATH.Location = New System.Drawing.Point(67, 70)
        Me.TXTPATH.Name = "TXTPATH"
        Me.TXTPATH.ReadOnly = True
        Me.TXTPATH.Size = New System.Drawing.Size(280, 23)
        Me.TXTPATH.TabIndex = 575
        Me.TXTPATH.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(33, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 15)
        Me.Label6.TabIndex = 576
        Me.Label6.Text = "Path"
        '
        'CMDUPLOAD
        '
        Me.CMDUPLOAD.BackColor = System.Drawing.Color.Transparent
        Me.CMDUPLOAD.FlatAppearance.BorderSize = 0
        Me.CMDUPLOAD.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDUPLOAD.ForeColor = System.Drawing.Color.Black
        Me.CMDUPLOAD.Location = New System.Drawing.Point(106, 125)
        Me.CMDUPLOAD.Name = "CMDUPLOAD"
        Me.CMDUPLOAD.Size = New System.Drawing.Size(80, 28)
        Me.CMDUPLOAD.TabIndex = 3
        Me.CMDUPLOAD.Text = "Upload"
        Me.CMDUPLOAD.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(278, 125)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 5
        Me.CMDEXIT.Text = "Exit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'UploadExcel_MASHOK
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "UploadExcel_MASHOK"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Upload Excel"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDERROR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDCOMPLETE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDUPLOAD As Button
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents TXTPATH As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CMDSELECTFILE As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents TXTFILENAME As TextBox
    Friend WithEvents CMDCLEAR As Button
    Friend WithEvents LBLTYPE As Label
    Friend WithEvents CMBTYPE As ComboBox
    Friend WithEvents cmbregister As ComboBox
    Friend WithEvents Label5 As Label
    Private WithEvents GRIDCOMPLETE As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GROWNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPARTYBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GRIDERROR As DevExpress.XtraGrid.GridControl
    Private WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents EROWNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ENAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EPARTYBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents EERROR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVOICENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EINVOICENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMDEXCEL As Button
End Class
