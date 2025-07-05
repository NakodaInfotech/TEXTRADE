<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SendMultipleWhatsapp
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TBWHATSAPP = New System.Windows.Forms.TabPage()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GPRINTINITIALS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gdate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gname = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPARTYWHATSAAPNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENTWHATSAPP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREGID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREGNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GATTACHMENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFILENAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKEDIT = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.CHKSELECTALL = New System.Windows.Forms.CheckBox()
        Me.GRIDNAMEDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDNAME = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGROUP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAREA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGROUPOFCOMPANIES = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWHATSAPP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TXTAUTOCC = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TXTOTHERNO3 = New System.Windows.Forms.TextBox()
        Me.TXTOTHERNO2 = New System.Windows.Forms.TextBox()
        Me.TXTOTHERNO1 = New System.Windows.Forms.TextBox()
        Me.TXTAGENTNO = New System.Windows.Forms.TextBox()
        Me.TXTPARTYNO = New System.Windows.Forms.TextBox()
        Me.TXTMESSAGE = New System.Windows.Forms.TextBox()
        Me.CMBOTHERNAME3 = New System.Windows.Forms.ComboBox()
        Me.CMBOTHERNAME2 = New System.Windows.Forms.ComboBox()
        Me.CMBOTHERNAME1 = New System.Windows.Forms.ComboBox()
        Me.CMBAGENTNAME = New System.Windows.Forms.ComboBox()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.RBPARTY = New System.Windows.Forms.RadioButton()
        Me.RBAGENT = New System.Windows.Forms.RadioButton()
        Me.RBBOTH = New System.Windows.Forms.RadioButton()
        Me.BlendPanel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TBWHATSAPP.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.GRIDNAMEDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDNAME, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.RBBOTH)
        Me.BlendPanel1.Controls.Add(Me.RBAGENT)
        Me.BlendPanel1.Controls.Add(Me.RBPARTY)
        Me.BlendPanel1.Controls.Add(Me.TabControl1)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 2
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TBWHATSAPP)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1222, 523)
        Me.TabControl1.TabIndex = 4
        '
        'TBWHATSAPP
        '
        Me.TBWHATSAPP.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TBWHATSAPP.Controls.Add(Me.gridbilldetails)
        Me.TBWHATSAPP.Location = New System.Drawing.Point(4, 24)
        Me.TBWHATSAPP.Name = "TBWHATSAPP"
        Me.TBWHATSAPP.Padding = New System.Windows.Forms.Padding(3)
        Me.TBWHATSAPP.Size = New System.Drawing.Size(1214, 495)
        Me.TBWHATSAPP.TabIndex = 0
        Me.TBWHATSAPP.Text = "Whatsapp"
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(3, 6)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKEDIT})
        Me.gridbilldetails.Size = New System.Drawing.Size(1210, 487)
        Me.gridbilldetails.TabIndex = 1
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GPRINTINITIALS, Me.gdate, Me.gname, Me.GPARTYWHATSAAPNO, Me.GAGENT, Me.GAGENTWHATSAPP, Me.GINVNO, Me.GREGID, Me.GREGNAME, Me.GATTACHMENT, Me.GFILENAME, Me.GGRANDTOTAL})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GPRINTINITIALS
        '
        Me.GPRINTINITIALS.Caption = "Bill No"
        Me.GPRINTINITIALS.FieldName = "PRINTINITIALS"
        Me.GPRINTINITIALS.ImageIndex = 1
        Me.GPRINTINITIALS.Name = "GPRINTINITIALS"
        Me.GPRINTINITIALS.OptionsColumn.AllowEdit = False
        Me.GPRINTINITIALS.Visible = True
        Me.GPRINTINITIALS.VisibleIndex = 0
        Me.GPRINTINITIALS.Width = 150
        '
        'gdate
        '
        Me.gdate.Caption = "Date"
        Me.gdate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.gdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.gdate.FieldName = "DATE"
        Me.gdate.Name = "gdate"
        Me.gdate.OptionsColumn.AllowEdit = False
        Me.gdate.Visible = True
        Me.gdate.VisibleIndex = 1
        '
        'gname
        '
        Me.gname.Caption = "Name"
        Me.gname.FieldName = "NAME"
        Me.gname.ImageIndex = 0
        Me.gname.Name = "gname"
        Me.gname.OptionsColumn.AllowEdit = False
        Me.gname.Visible = True
        Me.gname.VisibleIndex = 2
        Me.gname.Width = 250
        '
        'GPARTYWHATSAAPNO
        '
        Me.GPARTYWHATSAAPNO.Caption = "Party Whatsaap No"
        Me.GPARTYWHATSAAPNO.FieldName = "PARTYWHATSAPP"
        Me.GPARTYWHATSAAPNO.Name = "GPARTYWHATSAAPNO"
        Me.GPARTYWHATSAAPNO.Visible = True
        Me.GPARTYWHATSAAPNO.VisibleIndex = 3
        Me.GPARTYWHATSAAPNO.Width = 120
        '
        'GAGENT
        '
        Me.GAGENT.Caption = "Agent Name"
        Me.GAGENT.FieldName = "AGENTNAME"
        Me.GAGENT.Name = "GAGENT"
        Me.GAGENT.OptionsColumn.AllowEdit = False
        Me.GAGENT.Visible = True
        Me.GAGENT.VisibleIndex = 4
        Me.GAGENT.Width = 200
        '
        'GAGENTWHATSAPP
        '
        Me.GAGENTWHATSAPP.Caption = "Agent Whatsapp No"
        Me.GAGENTWHATSAPP.FieldName = "AGENTWHATSAPP"
        Me.GAGENTWHATSAPP.Name = "GAGENTWHATSAPP"
        Me.GAGENTWHATSAPP.Visible = True
        Me.GAGENTWHATSAPP.VisibleIndex = 5
        Me.GAGENTWHATSAPP.Width = 150
        '
        'GINVNO
        '
        Me.GINVNO.Caption = "Inv No"
        Me.GINVNO.FieldName = "INVNO"
        Me.GINVNO.Name = "GINVNO"
        '
        'GREGID
        '
        Me.GREGID.Caption = "Reg ID"
        Me.GREGID.FieldName = "REGID"
        Me.GREGID.Name = "GREGID"
        '
        'GREGNAME
        '
        Me.GREGNAME.Caption = "Reg Name"
        Me.GREGNAME.FieldName = "REGNAME"
        Me.GREGNAME.Name = "GREGNAME"
        '
        'GATTACHMENT
        '
        Me.GATTACHMENT.Caption = "Attachment"
        Me.GATTACHMENT.FieldName = "ATTACHMENT"
        Me.GATTACHMENT.Name = "GATTACHMENT"
        Me.GATTACHMENT.Visible = True
        Me.GATTACHMENT.VisibleIndex = 6
        Me.GATTACHMENT.Width = 350
        '
        'GFILENAME
        '
        Me.GFILENAME.Caption = "File Name"
        Me.GFILENAME.FieldName = "FILENAME"
        Me.GFILENAME.Name = "GFILENAME"
        '
        'GGRANDTOTAL
        '
        Me.GGRANDTOTAL.Caption = "Grand Total"
        Me.GGRANDTOTAL.DisplayFormat.FormatString = "0.00"
        Me.GGRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.GGRANDTOTAL.Name = "GGRANDTOTAL"
        Me.GGRANDTOTAL.Visible = True
        Me.GGRANDTOTAL.VisibleIndex = 7
        '
        'CHKEDIT
        '
        Me.CHKEDIT.AutoHeight = False
        Me.CHKEDIT.Name = "CHKEDIT"
        Me.CHKEDIT.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.TXTADD)
        Me.TabPage2.Controls.Add(Me.CMBCODE)
        Me.TabPage2.Controls.Add(Me.CHKSELECTALL)
        Me.TabPage2.Controls.Add(Me.GRIDNAMEDETAILS)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.TXTAUTOCC)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.TXTOTHERNO3)
        Me.TabPage2.Controls.Add(Me.TXTOTHERNO2)
        Me.TabPage2.Controls.Add(Me.TXTOTHERNO1)
        Me.TabPage2.Controls.Add(Me.TXTAGENTNO)
        Me.TabPage2.Controls.Add(Me.TXTPARTYNO)
        Me.TabPage2.Controls.Add(Me.TXTMESSAGE)
        Me.TabPage2.Controls.Add(Me.CMBOTHERNAME3)
        Me.TabPage2.Controls.Add(Me.CMBOTHERNAME2)
        Me.TabPage2.Controls.Add(Me.CMBOTHERNAME1)
        Me.TabPage2.Controls.Add(Me.CMBAGENTNAME)
        Me.TabPage2.Controls.Add(Me.CMBNAME)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1214, 497)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Multiple Numbers"
        '
        'TXTADD
        '
        Me.TXTADD.Location = New System.Drawing.Point(361, 285)
        Me.TXTADD.MaxLength = 10
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(25, 23)
        Me.TXTADD.TabIndex = 41
        Me.TXTADD.Visible = False
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Location = New System.Drawing.Point(330, 285)
        Me.CMBCODE.MaxDropDownItems = 14
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(25, 23)
        Me.CMBCODE.TabIndex = 40
        Me.CMBCODE.Visible = False
        '
        'CHKSELECTALL
        '
        Me.CHKSELECTALL.AutoSize = True
        Me.CHKSELECTALL.BackColor = System.Drawing.Color.Transparent
        Me.CHKSELECTALL.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSELECTALL.ForeColor = System.Drawing.Color.Black
        Me.CHKSELECTALL.Location = New System.Drawing.Point(519, 9)
        Me.CHKSELECTALL.Name = "CHKSELECTALL"
        Me.CHKSELECTALL.Size = New System.Drawing.Size(77, 18)
        Me.CHKSELECTALL.TabIndex = 38
        Me.CHKSELECTALL.Text = "Select All"
        Me.CHKSELECTALL.UseVisualStyleBackColor = False
        '
        'GRIDNAMEDETAILS
        '
        Me.GRIDNAMEDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDNAMEDETAILS.Location = New System.Drawing.Point(516, 30)
        Me.GRIDNAMEDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDNAMEDETAILS.MainView = Me.GRIDNAME
        Me.GRIDNAMEDETAILS.Name = "GRIDNAMEDETAILS"
        Me.GRIDNAMEDETAILS.Size = New System.Drawing.Size(697, 459)
        Me.GRIDNAMEDETAILS.TabIndex = 39
        Me.GRIDNAMEDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDNAME})
        '
        'GRIDNAME
        '
        Me.GRIDNAME.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDNAME.Appearance.Row.Options.UseFont = True
        Me.GRIDNAME.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHK, Me.GridColumn1, Me.GGROUP, Me.GAREA, Me.GCITY, Me.GGROUPOFCOMPANIES, Me.GWHATSAPP})
        Me.GRIDNAME.GridControl = Me.GRIDNAMEDETAILS
        Me.GRIDNAME.Name = "GRIDNAME"
        Me.GRIDNAME.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDNAME.OptionsView.ColumnAutoWidth = False
        Me.GRIDNAME.OptionsView.ShowAutoFilterRow = True
        Me.GRIDNAME.OptionsView.ShowGroupPanel = False
        '
        'GCHK
        '
        Me.GCHK.FieldName = "CHK"
        Me.GCHK.Name = "GCHK"
        Me.GCHK.Visible = True
        Me.GCHK.VisibleIndex = 0
        Me.GCHK.Width = 35
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Name"
        Me.GridColumn1.FieldName = "NAME"
        Me.GridColumn1.ImageIndex = 0
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 250
        '
        'GGROUP
        '
        Me.GGROUP.Caption = "Group"
        Me.GGROUP.FieldName = "GROUP"
        Me.GGROUP.Name = "GGROUP"
        Me.GGROUP.Visible = True
        Me.GGROUP.VisibleIndex = 2
        Me.GGROUP.Width = 100
        '
        'GAREA
        '
        Me.GAREA.Caption = "Area"
        Me.GAREA.FieldName = "AREA"
        Me.GAREA.Name = "GAREA"
        Me.GAREA.Visible = True
        Me.GAREA.VisibleIndex = 3
        Me.GAREA.Width = 100
        '
        'GCITY
        '
        Me.GCITY.Caption = "City"
        Me.GCITY.FieldName = "CITY"
        Me.GCITY.Name = "GCITY"
        Me.GCITY.OptionsColumn.AllowEdit = False
        Me.GCITY.Visible = True
        Me.GCITY.VisibleIndex = 4
        Me.GCITY.Width = 100
        '
        'GGROUPOFCOMPANIES
        '
        Me.GGROUPOFCOMPANIES.Caption = "Group Of Companies"
        Me.GGROUPOFCOMPANIES.FieldName = "GRPCOM"
        Me.GGROUPOFCOMPANIES.Name = "GGROUPOFCOMPANIES"
        Me.GGROUPOFCOMPANIES.Visible = True
        Me.GGROUPOFCOMPANIES.VisibleIndex = 5
        Me.GGROUPOFCOMPANIES.Width = 120
        '
        'GWHATSAPP
        '
        Me.GWHATSAPP.Caption = "WhatsApp No"
        Me.GWHATSAPP.FieldName = "WHATSAPP"
        Me.GWHATSAPP.Name = "GWHATSAPP"
        Me.GWHATSAPP.Visible = True
        Me.GWHATSAPP.VisibleIndex = 6
        Me.GWHATSAPP.Width = 80
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(29, 262)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 15)
        Me.Label8.TabIndex = 37
        Me.Label8.Text = "Auto CC"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTAUTOCC
        '
        Me.TXTAUTOCC.Location = New System.Drawing.Point(83, 259)
        Me.TXTAUTOCC.MaxLength = 10
        Me.TXTAUTOCC.Name = "TXTAUTOCC"
        Me.TXTAUTOCC.Size = New System.Drawing.Size(176, 23)
        Me.TXTAUTOCC.TabIndex = 36
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(23, 158)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 15)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Message"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTOTHERNO3
        '
        Me.TXTOTHERNO3.Location = New System.Drawing.Point(330, 126)
        Me.TXTOTHERNO3.MaxLength = 10
        Me.TXTOTHERNO3.Name = "TXTOTHERNO3"
        Me.TXTOTHERNO3.Size = New System.Drawing.Size(176, 23)
        Me.TXTOTHERNO3.TabIndex = 33
        '
        'TXTOTHERNO2
        '
        Me.TXTOTHERNO2.Location = New System.Drawing.Point(330, 96)
        Me.TXTOTHERNO2.MaxLength = 10
        Me.TXTOTHERNO2.Name = "TXTOTHERNO2"
        Me.TXTOTHERNO2.Size = New System.Drawing.Size(176, 23)
        Me.TXTOTHERNO2.TabIndex = 31
        '
        'TXTOTHERNO1
        '
        Me.TXTOTHERNO1.Location = New System.Drawing.Point(330, 66)
        Me.TXTOTHERNO1.MaxLength = 10
        Me.TXTOTHERNO1.Name = "TXTOTHERNO1"
        Me.TXTOTHERNO1.Size = New System.Drawing.Size(176, 23)
        Me.TXTOTHERNO1.TabIndex = 29
        '
        'TXTAGENTNO
        '
        Me.TXTAGENTNO.Location = New System.Drawing.Point(330, 36)
        Me.TXTAGENTNO.MaxLength = 10
        Me.TXTAGENTNO.Name = "TXTAGENTNO"
        Me.TXTAGENTNO.Size = New System.Drawing.Size(176, 23)
        Me.TXTAGENTNO.TabIndex = 27
        '
        'TXTPARTYNO
        '
        Me.TXTPARTYNO.Location = New System.Drawing.Point(330, 6)
        Me.TXTPARTYNO.MaxLength = 10
        Me.TXTPARTYNO.Name = "TXTPARTYNO"
        Me.TXTPARTYNO.Size = New System.Drawing.Size(176, 23)
        Me.TXTPARTYNO.TabIndex = 25
        '
        'TXTMESSAGE
        '
        Me.TXTMESSAGE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTMESSAGE.ForeColor = System.Drawing.Color.DimGray
        Me.TXTMESSAGE.Location = New System.Drawing.Point(83, 155)
        Me.TXTMESSAGE.Multiline = True
        Me.TXTMESSAGE.Name = "TXTMESSAGE"
        Me.TXTMESSAGE.Size = New System.Drawing.Size(423, 98)
        Me.TXTMESSAGE.TabIndex = 34
        '
        'CMBOTHERNAME3
        '
        Me.CMBOTHERNAME3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBOTHERNAME3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBOTHERNAME3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBOTHERNAME3.FormattingEnabled = True
        Me.CMBOTHERNAME3.Location = New System.Drawing.Point(83, 126)
        Me.CMBOTHERNAME3.MaxDropDownItems = 14
        Me.CMBOTHERNAME3.Name = "CMBOTHERNAME3"
        Me.CMBOTHERNAME3.Size = New System.Drawing.Size(241, 23)
        Me.CMBOTHERNAME3.TabIndex = 32
        '
        'CMBOTHERNAME2
        '
        Me.CMBOTHERNAME2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBOTHERNAME2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBOTHERNAME2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBOTHERNAME2.FormattingEnabled = True
        Me.CMBOTHERNAME2.Location = New System.Drawing.Point(83, 96)
        Me.CMBOTHERNAME2.MaxDropDownItems = 14
        Me.CMBOTHERNAME2.Name = "CMBOTHERNAME2"
        Me.CMBOTHERNAME2.Size = New System.Drawing.Size(241, 23)
        Me.CMBOTHERNAME2.TabIndex = 30
        '
        'CMBOTHERNAME1
        '
        Me.CMBOTHERNAME1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBOTHERNAME1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBOTHERNAME1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBOTHERNAME1.FormattingEnabled = True
        Me.CMBOTHERNAME1.Location = New System.Drawing.Point(83, 66)
        Me.CMBOTHERNAME1.MaxDropDownItems = 14
        Me.CMBOTHERNAME1.Name = "CMBOTHERNAME1"
        Me.CMBOTHERNAME1.Size = New System.Drawing.Size(241, 23)
        Me.CMBOTHERNAME1.TabIndex = 28
        '
        'CMBAGENTNAME
        '
        Me.CMBAGENTNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBAGENTNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBAGENTNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBAGENTNAME.FormattingEnabled = True
        Me.CMBAGENTNAME.Location = New System.Drawing.Point(83, 36)
        Me.CMBAGENTNAME.MaxDropDownItems = 14
        Me.CMBAGENTNAME.Name = "CMBAGENTNAME"
        Me.CMBAGENTNAME.Size = New System.Drawing.Size(241, 23)
        Me.CMBAGENTNAME.TabIndex = 26
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(83, 6)
        Me.CMBNAME.MaxDropDownItems = 14
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(241, 23)
        Me.CMBNAME.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 15)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Party Name"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 15)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Agent Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 15)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Other Name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 15)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Other Name"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 130)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 15)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Other Name"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(535, 541)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 2
        Me.CMDOK.Text = "&Ok"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.Color.Transparent
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdcancel.FlatAppearance.BorderSize = 0
        Me.cmdcancel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.Black
        Me.cmdcancel.Location = New System.Drawing.Point(619, 541)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 3
        Me.cmdcancel.Text = "E&xit"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'RBPARTY
        '
        Me.RBPARTY.AutoSize = True
        Me.RBPARTY.BackColor = System.Drawing.Color.Transparent
        Me.RBPARTY.Location = New System.Drawing.Point(282, 546)
        Me.RBPARTY.Name = "RBPARTY"
        Me.RBPARTY.Size = New System.Drawing.Size(83, 19)
        Me.RBPARTY.TabIndex = 5
        Me.RBPARTY.Text = "Only Party"
        Me.RBPARTY.UseVisualStyleBackColor = False
        '
        'RBAGENT
        '
        Me.RBAGENT.AutoSize = True
        Me.RBAGENT.BackColor = System.Drawing.Color.Transparent
        Me.RBAGENT.Location = New System.Drawing.Point(371, 546)
        Me.RBAGENT.Name = "RBAGENT"
        Me.RBAGENT.Size = New System.Drawing.Size(84, 19)
        Me.RBAGENT.TabIndex = 6
        Me.RBAGENT.Text = "Only Agent"
        Me.RBAGENT.UseVisualStyleBackColor = False
        '
        'RBBOTH
        '
        Me.RBBOTH.AutoSize = True
        Me.RBBOTH.BackColor = System.Drawing.Color.Transparent
        Me.RBBOTH.Checked = True
        Me.RBBOTH.Location = New System.Drawing.Point(165, 546)
        Me.RBBOTH.Name = "RBBOTH"
        Me.RBBOTH.Size = New System.Drawing.Size(111, 19)
        Me.RBBOTH.TabIndex = 7
        Me.RBBOTH.TabStop = True
        Me.RBBOTH.Text = "Party And Agent"
        Me.RBBOTH.UseVisualStyleBackColor = False
        '
        'SendMultipleWhatsapp
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "SendMultipleWhatsapp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Send Multiple Whatsapp"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TBWHATSAPP.ResumeLayout(False)
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.GRIDNAMEDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDNAME, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GPRINTINITIALS As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gdate As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gname As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPARTYWHATSAAPNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENTWHATSAPP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREGID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREGNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GATTACHMENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFILENAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKEDIT As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CMDOK As Button
    Friend WithEvents cmdcancel As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TBWHATSAPP As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label8 As Label
    Friend WithEvents TXTAUTOCC As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TXTOTHERNO3 As TextBox
    Friend WithEvents TXTOTHERNO2 As TextBox
    Friend WithEvents TXTOTHERNO1 As TextBox
    Friend WithEvents TXTAGENTNO As TextBox
    Friend WithEvents TXTPARTYNO As TextBox
    Friend WithEvents TXTMESSAGE As TextBox
    Friend WithEvents CMBOTHERNAME3 As ComboBox
    Friend WithEvents CMBOTHERNAME2 As ComboBox
    Friend WithEvents CMBOTHERNAME1 As ComboBox
    Friend WithEvents CMBAGENTNAME As ComboBox
    Friend WithEvents CMBNAME As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents CHKSELECTALL As CheckBox
    Private WithEvents GRIDNAMEDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDNAME As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGROUP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAREA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGROUPOFCOMPANIES As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWHATSAPP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents RBBOTH As RadioButton
    Friend WithEvents RBAGENT As RadioButton
    Friend WithEvents RBPARTY As RadioButton
End Class
