<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EmployeeDetails
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
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.CMDADDNEW = New System.Windows.Forms.Button()
        Me.CMDEDIT = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.GRIDBILLDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDBILL = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GEMPID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEMPNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLEDGERNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLOANLEDGERNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSALARY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDEPARTMENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDESIGNATION = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAREA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPINCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRESINO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWHATSAPPNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GALTNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMOBILENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEMAIL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPANNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPFNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBANK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GACTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GACCNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GIFSCCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBRANCH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GUPI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GADDRESS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ExcelExport = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDBILLDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDBILL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.CMDADDNEW)
        Me.BlendPanel1.Controls.Add(Me.CMDEDIT)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.GRIDBILLDETAILS)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1254, 581)
        Me.BlendPanel1.TabIndex = 5
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.Black
        Me.CMDREFRESH.Location = New System.Drawing.Point(536, 541)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREFRESH.TabIndex = 325
        Me.CMDREFRESH.Text = "&Refresh"
        Me.CMDREFRESH.UseVisualStyleBackColor = False
        '
        'CMDADDNEW
        '
        Me.CMDADDNEW.BackColor = System.Drawing.Color.Transparent
        Me.CMDADDNEW.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDADDNEW.FlatAppearance.BorderSize = 0
        Me.CMDADDNEW.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDADDNEW.ForeColor = System.Drawing.Color.Black
        Me.CMDADDNEW.Location = New System.Drawing.Point(450, 541)
        Me.CMDADDNEW.Name = "CMDADDNEW"
        Me.CMDADDNEW.Size = New System.Drawing.Size(80, 28)
        Me.CMDADDNEW.TabIndex = 324
        Me.CMDADDNEW.Text = "&Add New"
        Me.CMDADDNEW.UseVisualStyleBackColor = False
        '
        'CMDEDIT
        '
        Me.CMDEDIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEDIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEDIT.FlatAppearance.BorderSize = 0
        Me.CMDEDIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEDIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEDIT.Location = New System.Drawing.Point(622, 541)
        Me.CMDEDIT.Name = "CMDEDIT"
        Me.CMDEDIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEDIT.TabIndex = 323
        Me.CMDEDIT.Text = "&Edit"
        Me.CMDEDIT.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(708, 541)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 322
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'GRIDBILLDETAILS
        '
        Me.GRIDBILLDETAILS.Location = New System.Drawing.Point(18, 45)
        Me.GRIDBILLDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDBILLDETAILS.MainView = Me.GRIDBILL
        Me.GRIDBILLDETAILS.Name = "GRIDBILLDETAILS"
        Me.GRIDBILLDETAILS.Size = New System.Drawing.Size(1223, 490)
        Me.GRIDBILLDETAILS.TabIndex = 315
        Me.GRIDBILLDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDBILL})
        '
        'GRIDBILL
        '
        Me.GRIDBILL.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILL.Appearance.Row.Options.UseFont = True
        Me.GRIDBILL.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GEMPID, Me.GEMPNAME, Me.GLEDGERNAME, Me.GLOANLEDGERNAME, Me.GSALARY, Me.GDEPARTMENT, Me.GDESIGNATION, Me.GAREA, Me.GCITY, Me.GPINCODE, Me.GRESINO, Me.GWHATSAPPNO, Me.GALTNO, Me.GMOBILENO, Me.GEMAIL, Me.GPANNO, Me.GPFNO, Me.GBANK, Me.GACTYPE, Me.GACCNO, Me.GIFSCCODE, Me.GBRANCH, Me.GUPI, Me.GADDRESS, Me.GREMARKS})
        Me.GRIDBILL.GridControl = Me.GRIDBILLDETAILS
        Me.GRIDBILL.Name = "GRIDBILL"
        Me.GRIDBILL.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDBILL.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDBILL.OptionsBehavior.Editable = False
        Me.GRIDBILL.OptionsCustomization.AllowGroup = False
        Me.GRIDBILL.OptionsView.ColumnAutoWidth = False
        Me.GRIDBILL.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDBILL.OptionsView.ShowAutoFilterRow = True
        Me.GRIDBILL.OptionsView.ShowGroupPanel = False
        '
        'GEMPID
        '
        Me.GEMPID.Caption = "EMPID"
        Me.GEMPID.FieldName = "EMPID"
        Me.GEMPID.ImageIndex = 1
        Me.GEMPID.Name = "GEMPID"
        Me.GEMPID.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        '
        'GEMPNAME
        '
        Me.GEMPNAME.Caption = "Emp Name"
        Me.GEMPNAME.FieldName = "EMPNAME"
        Me.GEMPNAME.Name = "GEMPNAME"
        Me.GEMPNAME.Visible = True
        Me.GEMPNAME.VisibleIndex = 0
        Me.GEMPNAME.Width = 200
        '
        'GLEDGERNAME
        '
        Me.GLEDGERNAME.Caption = "Ledger Name"
        Me.GLEDGERNAME.FieldName = "LEDGERNAME"
        Me.GLEDGERNAME.Name = "GLEDGERNAME"
        Me.GLEDGERNAME.Visible = True
        Me.GLEDGERNAME.VisibleIndex = 1
        Me.GLEDGERNAME.Width = 200
        '
        'GLOANLEDGERNAME
        '
        Me.GLOANLEDGERNAME.Caption = "Loan Ledger Name"
        Me.GLOANLEDGERNAME.FieldName = "LOANLEDGERNAME"
        Me.GLOANLEDGERNAME.Name = "GLOANLEDGERNAME"
        Me.GLOANLEDGERNAME.Visible = True
        Me.GLOANLEDGERNAME.VisibleIndex = 2
        Me.GLOANLEDGERNAME.Width = 200
        '
        'GSALARY
        '
        Me.GSALARY.Caption = "Salary"
        Me.GSALARY.DisplayFormat.FormatString = "0.00"
        Me.GSALARY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GSALARY.FieldName = "SALARY"
        Me.GSALARY.Name = "GSALARY"
        Me.GSALARY.Visible = True
        Me.GSALARY.VisibleIndex = 3
        Me.GSALARY.Width = 80
        '
        'GDEPARTMENT
        '
        Me.GDEPARTMENT.Caption = "Department"
        Me.GDEPARTMENT.FieldName = "DEPARTMENT"
        Me.GDEPARTMENT.Name = "GDEPARTMENT"
        Me.GDEPARTMENT.Visible = True
        Me.GDEPARTMENT.VisibleIndex = 4
        Me.GDEPARTMENT.Width = 120
        '
        'GDESIGNATION
        '
        Me.GDESIGNATION.Caption = "Designation"
        Me.GDESIGNATION.FieldName = "DESIGNATION"
        Me.GDESIGNATION.Name = "GDESIGNATION"
        Me.GDESIGNATION.Visible = True
        Me.GDESIGNATION.VisibleIndex = 5
        Me.GDESIGNATION.Width = 110
        '
        'GAREA
        '
        Me.GAREA.Caption = "Area"
        Me.GAREA.FieldName = "AREA"
        Me.GAREA.Name = "GAREA"
        Me.GAREA.Visible = True
        Me.GAREA.VisibleIndex = 6
        Me.GAREA.Width = 100
        '
        'GCITY
        '
        Me.GCITY.Caption = "City"
        Me.GCITY.FieldName = "CITY"
        Me.GCITY.Name = "GCITY"
        Me.GCITY.Visible = True
        Me.GCITY.VisibleIndex = 7
        Me.GCITY.Width = 100
        '
        'GPINCODE
        '
        Me.GPINCODE.Caption = "Pin Code"
        Me.GPINCODE.FieldName = "PINCODE"
        Me.GPINCODE.Name = "GPINCODE"
        Me.GPINCODE.Visible = True
        Me.GPINCODE.VisibleIndex = 8
        '
        'GRESINO
        '
        Me.GRESINO.Caption = "Resi No"
        Me.GRESINO.FieldName = "RESINO"
        Me.GRESINO.Name = "GRESINO"
        Me.GRESINO.Visible = True
        Me.GRESINO.VisibleIndex = 9
        Me.GRESINO.Width = 100
        '
        'GWHATSAPPNO
        '
        Me.GWHATSAPPNO.Caption = "Whatsapp"
        Me.GWHATSAPPNO.FieldName = "WHATSAPPNO"
        Me.GWHATSAPPNO.Name = "GWHATSAPPNO"
        Me.GWHATSAPPNO.Visible = True
        Me.GWHATSAPPNO.VisibleIndex = 10
        Me.GWHATSAPPNO.Width = 100
        '
        'GALTNO
        '
        Me.GALTNO.Caption = "Alt No"
        Me.GALTNO.FieldName = "ALTNO"
        Me.GALTNO.Name = "GALTNO"
        Me.GALTNO.Visible = True
        Me.GALTNO.VisibleIndex = 11
        Me.GALTNO.Width = 100
        '
        'GMOBILENO
        '
        Me.GMOBILENO.Caption = "Mobile No"
        Me.GMOBILENO.FieldName = "MOBILENO"
        Me.GMOBILENO.Name = "GMOBILENO"
        Me.GMOBILENO.Visible = True
        Me.GMOBILENO.VisibleIndex = 12
        Me.GMOBILENO.Width = 100
        '
        'GEMAIL
        '
        Me.GEMAIL.Caption = "Email Id"
        Me.GEMAIL.FieldName = "EMAIL"
        Me.GEMAIL.Name = "GEMAIL"
        Me.GEMAIL.Visible = True
        Me.GEMAIL.VisibleIndex = 13
        Me.GEMAIL.Width = 100
        '
        'GPANNO
        '
        Me.GPANNO.Caption = "PAN No"
        Me.GPANNO.FieldName = "PANNO"
        Me.GPANNO.Name = "GPANNO"
        Me.GPANNO.Visible = True
        Me.GPANNO.VisibleIndex = 14
        '
        'GPFNO
        '
        Me.GPFNO.Caption = "PF No"
        Me.GPFNO.FieldName = "PFNO"
        Me.GPFNO.Name = "GPFNO"
        Me.GPFNO.Visible = True
        Me.GPFNO.VisibleIndex = 15
        Me.GPFNO.Width = 100
        '
        'GBANK
        '
        Me.GBANK.Caption = "Bank Name"
        Me.GBANK.FieldName = "PARTYBANK"
        Me.GBANK.Name = "GBANK"
        Me.GBANK.Visible = True
        Me.GBANK.VisibleIndex = 16
        Me.GBANK.Width = 150
        '
        'GACTYPE
        '
        Me.GACTYPE.Caption = "A/C Type"
        Me.GACTYPE.FieldName = "ACTYPE"
        Me.GACTYPE.Name = "GACTYPE"
        Me.GACTYPE.Visible = True
        Me.GACTYPE.VisibleIndex = 17
        '
        'GACCNO
        '
        Me.GACCNO.Caption = "A/C No"
        Me.GACCNO.FieldName = "ACCNO"
        Me.GACCNO.Name = "GACCNO"
        Me.GACCNO.Visible = True
        Me.GACCNO.VisibleIndex = 18
        Me.GACCNO.Width = 100
        '
        'GIFSCCODE
        '
        Me.GIFSCCODE.Caption = "IFSC Code"
        Me.GIFSCCODE.FieldName = "IFSCCODE"
        Me.GIFSCCODE.Name = "GIFSCCODE"
        Me.GIFSCCODE.Visible = True
        Me.GIFSCCODE.VisibleIndex = 19
        '
        'GBRANCH
        '
        Me.GBRANCH.Caption = "Branch"
        Me.GBRANCH.FieldName = "BRANCH"
        Me.GBRANCH.Name = "GBRANCH"
        Me.GBRANCH.Visible = True
        Me.GBRANCH.VisibleIndex = 20
        Me.GBRANCH.Width = 100
        '
        'GUPI
        '
        Me.GUPI.Caption = "UPI"
        Me.GUPI.FieldName = "UPI"
        Me.GUPI.Name = "GUPI"
        Me.GUPI.Visible = True
        Me.GUPI.VisibleIndex = 21
        Me.GUPI.Width = 120
        '
        'GADDRESS
        '
        Me.GADDRESS.Caption = "Address"
        Me.GADDRESS.FieldName = "ADDRESS"
        Me.GADDRESS.Name = "GADDRESS"
        Me.GADDRESS.Visible = True
        Me.GADDRESS.VisibleIndex = 22
        Me.GADDRESS.Width = 200
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "REMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.Visible = True
        Me.GREMARKS.VisibleIndex = 23
        Me.GREMARKS.Width = 200
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelExport, Me.ToolStripSeparator2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1254, 25)
        Me.ToolStrip1.TabIndex = 318
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ExcelExport
        '
        Me.ExcelExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ExcelExport.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        Me.ExcelExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ExcelExport.Name = "ExcelExport"
        Me.ExcelExport.Size = New System.Drawing.Size(23, 22)
        Me.ExcelExport.Text = "&Export to Excel"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'EmployeeDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1254, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "EmployeeDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Employee Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDBILLDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDBILL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDREFRESH As Button
    Friend WithEvents CMDADDNEW As Button
    Friend WithEvents CMDEDIT As Button
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents GRIDBILLDETAILS As DevExpress.XtraGrid.GridControl
    Friend WithEvents GRIDBILL As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GEMPID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ExcelExport As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents GEMPNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLEDGERNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLOANLEDGERNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSALARY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDEPARTMENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDESIGNATION As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAREA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPINCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRESINO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWHATSAPPNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GALTNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMOBILENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEMAIL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPANNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPFNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBANK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GACTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GACCNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GIFSCCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBRANCH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GUPI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GADDRESS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREMARKS As DevExpress.XtraGrid.Columns.GridColumn
End Class
