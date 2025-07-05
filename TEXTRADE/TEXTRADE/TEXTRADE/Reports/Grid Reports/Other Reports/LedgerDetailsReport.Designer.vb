<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LedgerDetailsReport
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
        Me.cmdadd = New System.Windows.Forms.Button()
        Me.cmdedit = New System.Windows.Forms.Button()
        Me.CMDSAVE = New System.Windows.Forms.Button()
        Me.CMDPRINT = New System.Windows.Forms.Button()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCMPNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GADD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGROUP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCSTNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GVATNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GOPBAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDRCR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAREA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDISTRICT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPINCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSTATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOUNTRY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRESINO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GALTNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPHONE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMOBILENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFAX = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWEBSITE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEMAIL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPANNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSECONDARY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTERMS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPARTYBANK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GACCOUNTTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GACCOUNTNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GIFSCCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBRANCH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBANKCITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTRANSPORT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINTPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCRDAYS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCRLIMIT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTDSPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCONTACT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDELIVERYAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDELIVERYPINCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSHIPPINGADD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEXMILLDISC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDISC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCASHDISC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GADD1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GADD2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GKMS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBROKERAGE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWHATSAPPNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGROUPOFCOMPANIES = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTANNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMSMENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBLOCKED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GUSER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTALLYLEDGER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTDSSECTION = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTDSCOMPANY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOMMISSION = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.cmdadd)
        Me.BlendPanel1.Controls.Add(Me.cmdedit)
        Me.BlendPanel1.Controls.Add(Me.CMDSAVE)
        Me.BlendPanel1.Controls.Add(Me.CMDPRINT)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 582)
        Me.BlendPanel1.TabIndex = 5
        '
        'cmdadd
        '
        Me.cmdadd.BackColor = System.Drawing.Color.Transparent
        Me.cmdadd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdadd.FlatAppearance.BorderSize = 0
        Me.cmdadd.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.ForeColor = System.Drawing.Color.Black
        Me.cmdadd.Location = New System.Drawing.Point(463, 547)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Size = New System.Drawing.Size(80, 28)
        Me.cmdadd.TabIndex = 504
        Me.cmdadd.Text = "&Add New"
        Me.cmdadd.UseVisualStyleBackColor = False
        '
        'cmdedit
        '
        Me.cmdedit.BackColor = System.Drawing.Color.Transparent
        Me.cmdedit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdedit.FlatAppearance.BorderSize = 0
        Me.cmdedit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdedit.ForeColor = System.Drawing.Color.Black
        Me.cmdedit.Location = New System.Drawing.Point(635, 547)
        Me.cmdedit.Name = "cmdedit"
        Me.cmdedit.Size = New System.Drawing.Size(80, 28)
        Me.cmdedit.TabIndex = 503
        Me.cmdedit.Text = "&Edit"
        Me.cmdedit.UseVisualStyleBackColor = False
        '
        'CMDSAVE
        '
        Me.CMDSAVE.Location = New System.Drawing.Point(549, 547)
        Me.CMDSAVE.Name = "CMDSAVE"
        Me.CMDSAVE.Size = New System.Drawing.Size(80, 28)
        Me.CMDSAVE.TabIndex = 502
        Me.CMDSAVE.Text = "&Refresh"
        Me.CMDSAVE.UseVisualStyleBackColor = True
        '
        'CMDPRINT
        '
        Me.CMDPRINT.BackColor = System.Drawing.Color.Transparent
        Me.CMDPRINT.BackgroundImage = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        Me.CMDPRINT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CMDPRINT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDPRINT.FlatAppearance.BorderSize = 0
        Me.CMDPRINT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMDPRINT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDPRINT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDPRINT.Location = New System.Drawing.Point(433, 551)
        Me.CMDPRINT.Name = "CMDPRINT"
        Me.CMDPRINT.Size = New System.Drawing.Size(25, 21)
        Me.CMDPRINT.TabIndex = 501
        Me.CMDPRINT.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(16, 14)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.Size = New System.Drawing.Size(1206, 526)
        Me.gridbilldetails.TabIndex = 494
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCODE, Me.GCMPNAME, Me.GADD, Me.GNAME, Me.GGSTIN, Me.GGROUP, Me.GCSTNO, Me.GVATNO, Me.GOPBAL, Me.GDRCR, Me.GAREA, Me.GDISTRICT, Me.GCITY, Me.GPINCODE, Me.GSTATE, Me.GCOUNTRY, Me.GRESINO, Me.GALTNO, Me.GPHONE, Me.GMOBILENO, Me.GFAX, Me.GWEBSITE, Me.GEMAIL, Me.GPANNO, Me.GTYPE, Me.GSECONDARY, Me.GAGENT, Me.GTERMS, Me.GPARTYBANK, Me.GACCOUNTTYPE, Me.GACCOUNTNO, Me.GIFSCCODE, Me.GBRANCH, Me.GBANKCITY, Me.GREMARKS, Me.GTRANSPORT, Me.GINTPER, Me.GCRDAYS, Me.GCRLIMIT, Me.GTDSPER, Me.GCONTACT, Me.GDELIVERYAT, Me.GDELIVERYPINCODE, Me.GSHIPPINGADD, Me.GEXMILLDISC, Me.GDISC, Me.GCASHDISC, Me.GADD1, Me.GADD2, Me.GKMS, Me.GBROKERAGE, Me.GWHATSAPPNO, Me.GRD, Me.GGROUPOFCOMPANIES, Me.GTANNO, Me.GMSMENO, Me.GBLOCKED, Me.GUSER, Me.GTALLYLEDGER, Me.GTDSSECTION, Me.GTDSCOMPANY, Me.GCOMMISSION})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GCODE
        '
        Me.GCODE.Caption = "Code"
        Me.GCODE.FieldName = "CODE"
        Me.GCODE.Name = "GCODE"
        Me.GCODE.Visible = True
        Me.GCODE.VisibleIndex = 0
        Me.GCODE.Width = 100
        '
        'GCMPNAME
        '
        Me.GCMPNAME.Caption = "Name"
        Me.GCMPNAME.FieldName = "CMPNAME"
        Me.GCMPNAME.Name = "GCMPNAME"
        Me.GCMPNAME.Visible = True
        Me.GCMPNAME.VisibleIndex = 1
        Me.GCMPNAME.Width = 150
        '
        'GADD
        '
        Me.GADD.Caption = "Address"
        Me.GADD.FieldName = "ADD"
        Me.GADD.Name = "GADD"
        Me.GADD.Visible = True
        Me.GADD.VisibleIndex = 4
        Me.GADD.Width = 200
        '
        'GNAME
        '
        Me.GNAME.Caption = "Contact Person"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 39
        Me.GNAME.Width = 150
        '
        'GGSTIN
        '
        Me.GGSTIN.Caption = "GSTIN"
        Me.GGSTIN.FieldName = "GSTIN"
        Me.GGSTIN.Name = "GGSTIN"
        Me.GGSTIN.Visible = True
        Me.GGSTIN.VisibleIndex = 11
        '
        'GGROUP
        '
        Me.GGROUP.Caption = "Group Name"
        Me.GGROUP.FieldName = "GROUPNAME"
        Me.GGROUP.Name = "GGROUP"
        Me.GGROUP.Visible = True
        Me.GGROUP.VisibleIndex = 12
        Me.GGROUP.Width = 100
        '
        'GCSTNO
        '
        Me.GCSTNO.Caption = "CST No"
        Me.GCSTNO.FieldName = "CSTNO"
        Me.GCSTNO.Name = "GCSTNO"
        Me.GCSTNO.Width = 90
        '
        'GVATNO
        '
        Me.GVATNO.Caption = "VAT No"
        Me.GVATNO.FieldName = "VATNO"
        Me.GVATNO.Name = "GVATNO"
        Me.GVATNO.Width = 90
        '
        'GOPBAL
        '
        Me.GOPBAL.Caption = "O/P Bal"
        Me.GOPBAL.FieldName = "OPBAL"
        Me.GOPBAL.Name = "GOPBAL"
        '
        'GDRCR
        '
        Me.GDRCR.Caption = "Dr/Cr"
        Me.GDRCR.FieldName = "DRCR"
        Me.GDRCR.Name = "GDRCR"
        Me.GDRCR.Width = 40
        '
        'GAREA
        '
        Me.GAREA.Caption = "Area"
        Me.GAREA.FieldName = "AREA"
        Me.GAREA.Name = "GAREA"
        Me.GAREA.Visible = True
        Me.GAREA.VisibleIndex = 37
        '
        'GDISTRICT
        '
        Me.GDISTRICT.Caption = "District"
        Me.GDISTRICT.FieldName = "DISTRICT"
        Me.GDISTRICT.Name = "GDISTRICT"
        Me.GDISTRICT.Visible = True
        Me.GDISTRICT.VisibleIndex = 41
        Me.GDISTRICT.Width = 100
        '
        'GCITY
        '
        Me.GCITY.Caption = "City"
        Me.GCITY.FieldName = "CITY"
        Me.GCITY.Name = "GCITY"
        Me.GCITY.Visible = True
        Me.GCITY.VisibleIndex = 5
        Me.GCITY.Width = 90
        '
        'GPINCODE
        '
        Me.GPINCODE.Caption = "Pin Code"
        Me.GPINCODE.FieldName = "ZIPCODE"
        Me.GPINCODE.Name = "GPINCODE"
        Me.GPINCODE.Visible = True
        Me.GPINCODE.VisibleIndex = 6
        '
        'GSTATE
        '
        Me.GSTATE.Caption = "State"
        Me.GSTATE.FieldName = "STATE"
        Me.GSTATE.Name = "GSTATE"
        Me.GSTATE.Visible = True
        Me.GSTATE.VisibleIndex = 7
        Me.GSTATE.Width = 90
        '
        'GCOUNTRY
        '
        Me.GCOUNTRY.Caption = "Country"
        Me.GCOUNTRY.FieldName = "COUNTRY"
        Me.GCOUNTRY.Name = "GCOUNTRY"
        Me.GCOUNTRY.Visible = True
        Me.GCOUNTRY.VisibleIndex = 8
        '
        'GRESINO
        '
        Me.GRESINO.Caption = "Resi No"
        Me.GRESINO.FieldName = "RESINO"
        Me.GRESINO.Name = "GRESINO"
        Me.GRESINO.Visible = True
        Me.GRESINO.VisibleIndex = 26
        Me.GRESINO.Width = 90
        '
        'GALTNO
        '
        Me.GALTNO.Caption = "Alt No"
        Me.GALTNO.FieldName = "ALTNO"
        Me.GALTNO.Name = "GALTNO"
        Me.GALTNO.Visible = True
        Me.GALTNO.VisibleIndex = 27
        '
        'GPHONE
        '
        Me.GPHONE.Caption = "Phone No"
        Me.GPHONE.FieldName = "PHONENO"
        Me.GPHONE.Name = "GPHONE"
        Me.GPHONE.Visible = True
        Me.GPHONE.VisibleIndex = 9
        Me.GPHONE.Width = 90
        '
        'GMOBILENO
        '
        Me.GMOBILENO.Caption = "Mobile No"
        Me.GMOBILENO.FieldName = "MOBILE"
        Me.GMOBILENO.Name = "GMOBILENO"
        Me.GMOBILENO.Visible = True
        Me.GMOBILENO.VisibleIndex = 10
        Me.GMOBILENO.Width = 90
        '
        'GFAX
        '
        Me.GFAX.Caption = "Fax"
        Me.GFAX.FieldName = "FAX"
        Me.GFAX.Name = "GFAX"
        Me.GFAX.Visible = True
        Me.GFAX.VisibleIndex = 28
        Me.GFAX.Width = 90
        '
        'GWEBSITE
        '
        Me.GWEBSITE.Caption = "Website"
        Me.GWEBSITE.FieldName = "WEBSITE"
        Me.GWEBSITE.Name = "GWEBSITE"
        Me.GWEBSITE.Visible = True
        Me.GWEBSITE.VisibleIndex = 29
        Me.GWEBSITE.Width = 150
        '
        'GEMAIL
        '
        Me.GEMAIL.Caption = "Email"
        Me.GEMAIL.FieldName = "EMAIL"
        Me.GEMAIL.Name = "GEMAIL"
        Me.GEMAIL.Visible = True
        Me.GEMAIL.VisibleIndex = 16
        Me.GEMAIL.Width = 150
        '
        'GPANNO
        '
        Me.GPANNO.Caption = "PAN No"
        Me.GPANNO.FieldName = "PANNO"
        Me.GPANNO.Name = "GPANNO"
        Me.GPANNO.Visible = True
        Me.GPANNO.VisibleIndex = 13
        Me.GPANNO.Width = 100
        '
        'GTYPE
        '
        Me.GTYPE.Caption = "Type"
        Me.GTYPE.FieldName = "TYPE"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.Visible = True
        Me.GTYPE.VisibleIndex = 38
        Me.GTYPE.Width = 90
        '
        'GSECONDARY
        '
        Me.GSECONDARY.Caption = "Secondary"
        Me.GSECONDARY.FieldName = "SECONDARY"
        Me.GSECONDARY.Name = "GSECONDARY"
        Me.GSECONDARY.Visible = True
        Me.GSECONDARY.VisibleIndex = 40
        Me.GSECONDARY.Width = 100
        '
        'GAGENT
        '
        Me.GAGENT.Caption = "Broker"
        Me.GAGENT.FieldName = "AGENTNAME"
        Me.GAGENT.Name = "GAGENT"
        Me.GAGENT.Visible = True
        Me.GAGENT.VisibleIndex = 14
        Me.GAGENT.Width = 200
        '
        'GTERMS
        '
        Me.GTERMS.Caption = "Term"
        Me.GTERMS.FieldName = "TERM"
        Me.GTERMS.Name = "GTERMS"
        Me.GTERMS.Visible = True
        Me.GTERMS.VisibleIndex = 42
        '
        'GPARTYBANK
        '
        Me.GPARTYBANK.Caption = "Bank Name"
        Me.GPARTYBANK.FieldName = "PARTYBANK"
        Me.GPARTYBANK.Name = "GPARTYBANK"
        Me.GPARTYBANK.Visible = True
        Me.GPARTYBANK.VisibleIndex = 43
        '
        'GACCOUNTTYPE
        '
        Me.GACCOUNTTYPE.Caption = "A/C Type"
        Me.GACCOUNTTYPE.FieldName = "ACCOUNTTYPE"
        Me.GACCOUNTTYPE.Name = "GACCOUNTTYPE"
        Me.GACCOUNTTYPE.Visible = True
        Me.GACCOUNTTYPE.VisibleIndex = 56
        '
        'GACCOUNTNO
        '
        Me.GACCOUNTNO.Caption = "A/C No"
        Me.GACCOUNTNO.FieldName = "ACCOUNTNO"
        Me.GACCOUNTNO.Name = "GACCOUNTNO"
        Me.GACCOUNTNO.Visible = True
        Me.GACCOUNTNO.VisibleIndex = 57
        '
        'GIFSCCODE
        '
        Me.GIFSCCODE.Caption = "IFSC Code"
        Me.GIFSCCODE.FieldName = "IFSCCODE"
        Me.GIFSCCODE.Name = "GIFSCCODE"
        Me.GIFSCCODE.Visible = True
        Me.GIFSCCODE.VisibleIndex = 44
        '
        'GBRANCH
        '
        Me.GBRANCH.Caption = "Branch"
        Me.GBRANCH.FieldName = "BRANCH"
        Me.GBRANCH.Name = "GBRANCH"
        Me.GBRANCH.Visible = True
        Me.GBRANCH.VisibleIndex = 45
        '
        'GBANKCITY
        '
        Me.GBANKCITY.Caption = "Bank City"
        Me.GBANKCITY.FieldName = "BANKCITY"
        Me.GBANKCITY.Name = "GBANKCITY"
        Me.GBANKCITY.Visible = True
        Me.GBANKCITY.VisibleIndex = 46
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "REMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.Visible = True
        Me.GREMARKS.VisibleIndex = 30
        '
        'GTRANSPORT
        '
        Me.GTRANSPORT.Caption = "Transport"
        Me.GTRANSPORT.FieldName = "TRANSPORT"
        Me.GTRANSPORT.Name = "GTRANSPORT"
        Me.GTRANSPORT.Visible = True
        Me.GTRANSPORT.VisibleIndex = 15
        '
        'GINTPER
        '
        Me.GINTPER.Caption = "Int %"
        Me.GINTPER.DisplayFormat.FormatString = "0.00"
        Me.GINTPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GINTPER.FieldName = "INTPER"
        Me.GINTPER.Name = "GINTPER"
        Me.GINTPER.Visible = True
        Me.GINTPER.VisibleIndex = 31
        '
        'GCRDAYS
        '
        Me.GCRDAYS.Caption = "Cr Days"
        Me.GCRDAYS.DisplayFormat.FormatString = "0"
        Me.GCRDAYS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCRDAYS.FieldName = "CRDAYS"
        Me.GCRDAYS.Name = "GCRDAYS"
        Me.GCRDAYS.Visible = True
        Me.GCRDAYS.VisibleIndex = 17
        '
        'GCRLIMIT
        '
        Me.GCRLIMIT.Caption = "Cr Limit"
        Me.GCRLIMIT.DisplayFormat.FormatString = "0.00"
        Me.GCRLIMIT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCRLIMIT.FieldName = "CRLIMIT"
        Me.GCRLIMIT.Name = "GCRLIMIT"
        Me.GCRLIMIT.Visible = True
        Me.GCRLIMIT.VisibleIndex = 47
        '
        'GTDSPER
        '
        Me.GTDSPER.Caption = "TDS %"
        Me.GTDSPER.DisplayFormat.FormatString = "0.00"
        Me.GTDSPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTDSPER.FieldName = "TDSPER"
        Me.GTDSPER.Name = "GTDSPER"
        Me.GTDSPER.Visible = True
        Me.GTDSPER.VisibleIndex = 19
        '
        'GCONTACT
        '
        Me.GCONTACT.Caption = "Contact Person"
        Me.GCONTACT.FieldName = "CONTACT"
        Me.GCONTACT.Name = "GCONTACT"
        Me.GCONTACT.Visible = True
        Me.GCONTACT.VisibleIndex = 18
        Me.GCONTACT.Width = 100
        '
        'GDELIVERYAT
        '
        Me.GDELIVERYAT.Caption = "Delivery At"
        Me.GDELIVERYAT.FieldName = "DELIVERYAT"
        Me.GDELIVERYAT.Name = "GDELIVERYAT"
        Me.GDELIVERYAT.Visible = True
        Me.GDELIVERYAT.VisibleIndex = 32
        Me.GDELIVERYAT.Width = 120
        '
        'GDELIVERYPINCODE
        '
        Me.GDELIVERYPINCODE.Caption = "Delivery Pin"
        Me.GDELIVERYPINCODE.FieldName = "DELIVERYPINCODE"
        Me.GDELIVERYPINCODE.Name = "GDELIVERYPINCODE"
        Me.GDELIVERYPINCODE.Visible = True
        Me.GDELIVERYPINCODE.VisibleIndex = 33
        '
        'GSHIPPINGADD
        '
        Me.GSHIPPINGADD.Caption = "Shipping Add"
        Me.GSHIPPINGADD.FieldName = "SHIPPINGADD"
        Me.GSHIPPINGADD.Name = "GSHIPPINGADD"
        Me.GSHIPPINGADD.Visible = True
        Me.GSHIPPINGADD.VisibleIndex = 25
        Me.GSHIPPINGADD.Width = 200
        '
        'GEXMILLDISC
        '
        Me.GEXMILLDISC.Caption = "Exmill Disc"
        Me.GEXMILLDISC.FieldName = "EXMILLDISC"
        Me.GEXMILLDISC.Name = "GEXMILLDISC"
        Me.GEXMILLDISC.Visible = True
        Me.GEXMILLDISC.VisibleIndex = 48
        '
        'GDISC
        '
        Me.GDISC.Caption = "Discount"
        Me.GDISC.FieldName = "DISC"
        Me.GDISC.Name = "GDISC"
        Me.GDISC.Visible = True
        Me.GDISC.VisibleIndex = 22
        '
        'GCASHDISC
        '
        Me.GCASHDISC.Caption = "Cash Disc"
        Me.GCASHDISC.FieldName = "CDPER"
        Me.GCASHDISC.Name = "GCASHDISC"
        Me.GCASHDISC.Visible = True
        Me.GCASHDISC.VisibleIndex = 23
        '
        'GADD1
        '
        Me.GADD1.Caption = "Add 1"
        Me.GADD1.FieldName = "ADD1"
        Me.GADD1.Name = "GADD1"
        Me.GADD1.Visible = True
        Me.GADD1.VisibleIndex = 2
        Me.GADD1.Width = 200
        '
        'GADD2
        '
        Me.GADD2.Caption = "Add 2"
        Me.GADD2.FieldName = "ADD2"
        Me.GADD2.Name = "GADD2"
        Me.GADD2.Visible = True
        Me.GADD2.VisibleIndex = 3
        Me.GADD2.Width = 200
        '
        'GKMS
        '
        Me.GKMS.Caption = "Kms"
        Me.GKMS.FieldName = "KMS"
        Me.GKMS.Name = "GKMS"
        Me.GKMS.Visible = True
        Me.GKMS.VisibleIndex = 34
        '
        'GBROKERAGE
        '
        Me.GBROKERAGE.Caption = "Brok %"
        Me.GBROKERAGE.DisplayFormat.FormatString = "0.00"
        Me.GBROKERAGE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GBROKERAGE.FieldName = "BROKERAGE"
        Me.GBROKERAGE.Name = "GBROKERAGE"
        Me.GBROKERAGE.Visible = True
        Me.GBROKERAGE.VisibleIndex = 35
        '
        'GWHATSAPPNO
        '
        Me.GWHATSAPPNO.Caption = "WhatsApp No"
        Me.GWHATSAPPNO.FieldName = "WHATSAPPNO"
        Me.GWHATSAPPNO.Name = "GWHATSAPPNO"
        Me.GWHATSAPPNO.Visible = True
        Me.GWHATSAPPNO.VisibleIndex = 36
        Me.GWHATSAPPNO.Width = 100
        '
        'GRD
        '
        Me.GRD.Caption = "Rate Diff"
        Me.GRD.DisplayFormat.FormatString = "0.00"
        Me.GRD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRD.FieldName = "RATEDIFF"
        Me.GRD.Name = "GRD"
        Me.GRD.Visible = True
        Me.GRD.VisibleIndex = 49
        '
        'GGROUPOFCOMPANIES
        '
        Me.GGROUPOFCOMPANIES.Caption = "Group of Companies"
        Me.GGROUPOFCOMPANIES.FieldName = "GROUPOFCOMPANIES"
        Me.GGROUPOFCOMPANIES.Name = "GGROUPOFCOMPANIES"
        Me.GGROUPOFCOMPANIES.Visible = True
        Me.GGROUPOFCOMPANIES.VisibleIndex = 50
        '
        'GTANNO
        '
        Me.GTANNO.Caption = "TAN No"
        Me.GTANNO.FieldName = "TANNO"
        Me.GTANNO.Name = "GTANNO"
        Me.GTANNO.Visible = True
        Me.GTANNO.VisibleIndex = 51
        Me.GTANNO.Width = 100
        '
        'GMSMENO
        '
        Me.GMSMENO.Caption = "MSME No"
        Me.GMSMENO.FieldName = "MSMENO"
        Me.GMSMENO.Name = "GMSMENO"
        Me.GMSMENO.Visible = True
        Me.GMSMENO.VisibleIndex = 52
        Me.GMSMENO.Width = 200
        '
        'GBLOCKED
        '
        Me.GBLOCKED.Caption = "Blocked"
        Me.GBLOCKED.FieldName = "BLOCKED"
        Me.GBLOCKED.Name = "GBLOCKED"
        Me.GBLOCKED.Visible = True
        Me.GBLOCKED.VisibleIndex = 53
        '
        'GUSER
        '
        Me.GUSER.Caption = "User Name"
        Me.GUSER.FieldName = "USERNAME"
        Me.GUSER.Name = "GUSER"
        Me.GUSER.Visible = True
        Me.GUSER.VisibleIndex = 54
        Me.GUSER.Width = 120
        '
        'GTALLYLEDGER
        '
        Me.GTALLYLEDGER.Caption = "Tally Ledger Name"
        Me.GTALLYLEDGER.FieldName = "TALLYLEDGER"
        Me.GTALLYLEDGER.Name = "GTALLYLEDGER"
        Me.GTALLYLEDGER.Visible = True
        Me.GTALLYLEDGER.VisibleIndex = 55
        Me.GTALLYLEDGER.Width = 200
        '
        'GTDSSECTION
        '
        Me.GTDSSECTION.Caption = "Tds Section"
        Me.GTDSSECTION.FieldName = "TDSSECTION"
        Me.GTDSSECTION.Name = "GTDSSECTION"
        Me.GTDSSECTION.Visible = True
        Me.GTDSSECTION.VisibleIndex = 20
        '
        'GTDSCOMPANY
        '
        Me.GTDSCOMPANY.Caption = "Cmp Non Cmp"
        Me.GTDSCOMPANY.FieldName = "TDSCOMPANY"
        Me.GTDSCOMPANY.Name = "GTDSCOMPANY"
        Me.GTDSCOMPANY.Visible = True
        Me.GTDSCOMPANY.VisibleIndex = 21
        '
        'GCOMMISSION
        '
        Me.GCOMMISSION.Caption = "Commission"
        Me.GCOMMISSION.FieldName = "COMMISSION"
        Me.GCOMMISSION.Name = "GCOMMISSION"
        Me.GCOMMISSION.Visible = True
        Me.GCOMMISSION.VisibleIndex = 24
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.Color.Transparent
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdcancel.FlatAppearance.BorderSize = 0
        Me.cmdcancel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.Black
        Me.cmdcancel.Location = New System.Drawing.Point(721, 547)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 316
        Me.cmdcancel.Text = "&Exit"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'LedgerDetailsReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 582)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "LedgerDetailsReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Ledger Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDPRINT As System.Windows.Forms.Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmdcancel As System.Windows.Forms.Button
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCMPNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGROUP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GOPBAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDRCR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAREA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSTATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOUNTRY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRESINO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GALTNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPHONE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMOBILENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFAX As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWEBSITE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEMAIL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPANNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GADD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSECONDARY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMDSAVE As System.Windows.Forms.Button
    Friend WithEvents GTANNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCSTNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GVATNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGSTIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTERMS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmdedit As Button
    Friend WithEvents cmdadd As Button
    Friend WithEvents GPARTYBANK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GACCOUNTTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GACCOUNTNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GIFSCCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBRANCH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBANKCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTRANSPORT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINTPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCRDAYS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTDSPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCONTACT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDELIVERYAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDELIVERYPINCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSHIPPINGADD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPINCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDISC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCASHDISC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GADD1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GADD2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GKMS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCRLIMIT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBROKERAGE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWHATSAPPNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDISTRICT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGROUPOFCOMPANIES As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMSMENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBLOCKED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GUSER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTALLYLEDGER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEXMILLDISC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTDSSECTION As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTDSCOMPANY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOMMISSION As DevExpress.XtraGrid.Columns.GridColumn
End Class
