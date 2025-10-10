<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ClientDetails
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
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.TXTNO = New System.Windows.Forms.TextBox()
        Me.TXTMOBILELIC = New System.Windows.Forms.TextBox()
        Me.TXTLOCATION = New System.Windows.Forms.TextBox()
        Me.DTMOBILEDATE = New System.Windows.Forms.MaskedTextBox()
        Me.DTWHATSAPPDATE = New System.Windows.Forms.MaskedTextBox()
        Me.DTEINVOICEDATE = New System.Windows.Forms.MaskedTextBox()
        Me.DTAMCDATE = New System.Windows.Forms.MaskedTextBox()
        Me.DTEWAYDATE = New System.Windows.Forms.MaskedTextBox()
        Me.CMBCLIENTNAME = New System.Windows.Forms.ComboBox()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPARTYNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCLIENTNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPROJECTNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAMCDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEWAYDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVOICEDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWHATSAPPDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLOCATION = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMOBILEDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMOBILELICENSE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHK = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CMBPROJECTNAME = New System.Windows.Forms.ComboBox()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.CMBPARTYNAME = New System.Windows.Forms.ComboBox()
        Me.txtsrno = New System.Windows.Forms.TextBox()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTNO)
        Me.BlendPanel1.Controls.Add(Me.TXTMOBILELIC)
        Me.BlendPanel1.Controls.Add(Me.TXTLOCATION)
        Me.BlendPanel1.Controls.Add(Me.DTMOBILEDATE)
        Me.BlendPanel1.Controls.Add(Me.DTWHATSAPPDATE)
        Me.BlendPanel1.Controls.Add(Me.DTEINVOICEDATE)
        Me.BlendPanel1.Controls.Add(Me.DTAMCDATE)
        Me.BlendPanel1.Controls.Add(Me.DTEWAYDATE)
        Me.BlendPanel1.Controls.Add(Me.CMBCLIENTNAME)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.CMBPROJECTNAME)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.CMBPARTYNAME)
        Me.BlendPanel1.Controls.Add(Me.txtsrno)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1393, 591)
        Me.BlendPanel1.TabIndex = 1
        '
        'TXTNO
        '
        Me.TXTNO.BackColor = System.Drawing.Color.White
        Me.TXTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTNO.Location = New System.Drawing.Point(1348, 3)
        Me.TXTNO.Name = "TXTNO"
        Me.TXTNO.ReadOnly = True
        Me.TXTNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTNO.TabIndex = 934
        Me.TXTNO.Text = " "
        Me.TXTNO.Visible = False
        '
        'TXTMOBILELIC
        '
        Me.TXTMOBILELIC.Location = New System.Drawing.Point(1248, 33)
        Me.TXTMOBILELIC.Name = "TXTMOBILELIC"
        Me.TXTMOBILELIC.Size = New System.Drawing.Size(76, 20)
        Me.TXTMOBILELIC.TabIndex = 933
        '
        'TXTLOCATION
        '
        Me.TXTLOCATION.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTLOCATION.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLOCATION.Location = New System.Drawing.Point(1072, 31)
        Me.TXTLOCATION.Name = "TXTLOCATION"
        Me.TXTLOCATION.Size = New System.Drawing.Size(89, 22)
        Me.TXTLOCATION.TabIndex = 932
        '
        'DTMOBILEDATE
        '
        Me.DTMOBILEDATE.AsciiOnly = True
        Me.DTMOBILEDATE.BackColor = System.Drawing.SystemColors.Window
        Me.DTMOBILEDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.DTMOBILEDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTMOBILEDATE.Location = New System.Drawing.Point(1162, 30)
        Me.DTMOBILEDATE.Mask = "00/00/0000"
        Me.DTMOBILEDATE.Name = "DTMOBILEDATE"
        Me.DTMOBILEDATE.Size = New System.Drawing.Size(82, 23)
        Me.DTMOBILEDATE.TabIndex = 931
        Me.DTMOBILEDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTMOBILEDATE.ValidatingType = GetType(Date)
        '
        'DTWHATSAPPDATE
        '
        Me.DTWHATSAPPDATE.AsciiOnly = True
        Me.DTWHATSAPPDATE.BackColor = System.Drawing.SystemColors.Window
        Me.DTWHATSAPPDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.DTWHATSAPPDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTWHATSAPPDATE.Location = New System.Drawing.Point(978, 30)
        Me.DTWHATSAPPDATE.Mask = "00/00/0000"
        Me.DTWHATSAPPDATE.Name = "DTWHATSAPPDATE"
        Me.DTWHATSAPPDATE.Size = New System.Drawing.Size(89, 23)
        Me.DTWHATSAPPDATE.TabIndex = 929
        Me.DTWHATSAPPDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTWHATSAPPDATE.ValidatingType = GetType(Date)
        '
        'DTEINVOICEDATE
        '
        Me.DTEINVOICEDATE.AsciiOnly = True
        Me.DTEINVOICEDATE.BackColor = System.Drawing.SystemColors.Window
        Me.DTEINVOICEDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.DTEINVOICEDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTEINVOICEDATE.Location = New System.Drawing.Point(881, 30)
        Me.DTEINVOICEDATE.Mask = "00/00/0000"
        Me.DTEINVOICEDATE.Name = "DTEINVOICEDATE"
        Me.DTEINVOICEDATE.Size = New System.Drawing.Size(89, 23)
        Me.DTEINVOICEDATE.TabIndex = 928
        Me.DTEINVOICEDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTEINVOICEDATE.ValidatingType = GetType(Date)
        '
        'DTAMCDATE
        '
        Me.DTAMCDATE.AsciiOnly = True
        Me.DTAMCDATE.BackColor = System.Drawing.SystemColors.Window
        Me.DTAMCDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.DTAMCDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTAMCDATE.Location = New System.Drawing.Point(673, 30)
        Me.DTAMCDATE.Mask = "00/00/0000"
        Me.DTAMCDATE.Name = "DTAMCDATE"
        Me.DTAMCDATE.Size = New System.Drawing.Size(89, 23)
        Me.DTAMCDATE.TabIndex = 927
        Me.DTAMCDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTAMCDATE.ValidatingType = GetType(Date)
        '
        'DTEWAYDATE
        '
        Me.DTEWAYDATE.AsciiOnly = True
        Me.DTEWAYDATE.BackColor = System.Drawing.SystemColors.Window
        Me.DTEWAYDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.DTEWAYDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTEWAYDATE.Location = New System.Drawing.Point(777, 30)
        Me.DTEWAYDATE.Mask = "00/00/0000"
        Me.DTEWAYDATE.Name = "DTEWAYDATE"
        Me.DTEWAYDATE.Size = New System.Drawing.Size(89, 23)
        Me.DTEWAYDATE.TabIndex = 926
        Me.DTEWAYDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTEWAYDATE.ValidatingType = GetType(Date)
        '
        'CMBCLIENTNAME
        '
        Me.CMBCLIENTNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCLIENTNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCLIENTNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBCLIENTNAME.FormattingEnabled = True
        Me.CMBCLIENTNAME.Location = New System.Drawing.Point(324, 32)
        Me.CMBCLIENTNAME.Name = "CMBCLIENTNAME"
        Me.CMBCLIENTNAME.Size = New System.Drawing.Size(200, 21)
        Me.CMBCLIENTNAME.TabIndex = 1
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(11, 53)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHK})
        Me.gridbilldetails.Size = New System.Drawing.Size(1367, 498)
        Me.gridbilldetails.TabIndex = 772
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GSRNO, Me.GNO, Me.GPARTYNAME, Me.GCLIENTNAME, Me.GPROJECTNAME, Me.GAMCDATE, Me.GEWAYDATE, Me.GINVOICEDATE, Me.GWHATSAPPDATE, Me.GLOCATION, Me.GMOBILEDATE, Me.GMOBILELICENSE})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsCustomization.AllowQuickHideColumns = False
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GSRNO
        '
        Me.GSRNO.Caption = "Sr No"
        Me.GSRNO.FieldName = "SRNO"
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.OptionsColumn.AllowEdit = False
        Me.GSRNO.Visible = True
        Me.GSRNO.VisibleIndex = 0
        Me.GSRNO.Width = 40
        '
        'GNO
        '
        Me.GNO.Caption = "NO"
        Me.GNO.FieldName = "NO"
        Me.GNO.Name = "GNO"
        Me.GNO.OptionsColumn.AllowEdit = False
        Me.GNO.Width = 60
        '
        'GPARTYNAME
        '
        Me.GPARTYNAME.Caption = "Party Name"
        Me.GPARTYNAME.FieldName = "PARTYNAME"
        Me.GPARTYNAME.Name = "GPARTYNAME"
        Me.GPARTYNAME.OptionsColumn.AllowEdit = False
        Me.GPARTYNAME.Visible = True
        Me.GPARTYNAME.VisibleIndex = 1
        Me.GPARTYNAME.Width = 250
        '
        'GCLIENTNAME
        '
        Me.GCLIENTNAME.Caption = "Client Name"
        Me.GCLIENTNAME.FieldName = "CLIENTNAME"
        Me.GCLIENTNAME.Name = "GCLIENTNAME"
        Me.GCLIENTNAME.OptionsColumn.AllowEdit = False
        Me.GCLIENTNAME.Visible = True
        Me.GCLIENTNAME.VisibleIndex = 2
        Me.GCLIENTNAME.Width = 200
        '
        'GPROJECTNAME
        '
        Me.GPROJECTNAME.Caption = "Project Name "
        Me.GPROJECTNAME.FieldName = "PROJECTNAME"
        Me.GPROJECTNAME.Name = "GPROJECTNAME"
        Me.GPROJECTNAME.OptionsColumn.AllowEdit = False
        Me.GPROJECTNAME.Visible = True
        Me.GPROJECTNAME.VisibleIndex = 3
        Me.GPROJECTNAME.Width = 150
        '
        'GAMCDATE
        '
        Me.GAMCDATE.Caption = "Amc Date"
        Me.GAMCDATE.FieldName = "AMCDATE"
        Me.GAMCDATE.Name = "GAMCDATE"
        Me.GAMCDATE.OptionsColumn.AllowEdit = False
        Me.GAMCDATE.Visible = True
        Me.GAMCDATE.VisibleIndex = 4
        Me.GAMCDATE.Width = 100
        '
        'GEWAYDATE
        '
        Me.GEWAYDATE.Caption = "Eway Date "
        Me.GEWAYDATE.FieldName = "EWAYDATE"
        Me.GEWAYDATE.Name = "GEWAYDATE"
        Me.GEWAYDATE.OptionsColumn.AllowEdit = False
        Me.GEWAYDATE.Visible = True
        Me.GEWAYDATE.VisibleIndex = 5
        Me.GEWAYDATE.Width = 100
        '
        'GINVOICEDATE
        '
        Me.GINVOICEDATE.Caption = "Einvoice Date"
        Me.GINVOICEDATE.FieldName = "EINVOICEDATE"
        Me.GINVOICEDATE.Name = "GINVOICEDATE"
        Me.GINVOICEDATE.OptionsColumn.AllowEdit = False
        Me.GINVOICEDATE.Visible = True
        Me.GINVOICEDATE.VisibleIndex = 6
        Me.GINVOICEDATE.Width = 100
        '
        'GWHATSAPPDATE
        '
        Me.GWHATSAPPDATE.Caption = "Whatsapp Date"
        Me.GWHATSAPPDATE.FieldName = "WHATSAPPDATE "
        Me.GWHATSAPPDATE.Name = "GWHATSAPPDATE"
        Me.GWHATSAPPDATE.OptionsColumn.AllowEdit = False
        Me.GWHATSAPPDATE.Visible = True
        Me.GWHATSAPPDATE.VisibleIndex = 7
        Me.GWHATSAPPDATE.Width = 100
        '
        'GLOCATION
        '
        Me.GLOCATION.Caption = "Location"
        Me.GLOCATION.FieldName = "LOCATION"
        Me.GLOCATION.Name = "GLOCATION"
        Me.GLOCATION.OptionsColumn.AllowEdit = False
        Me.GLOCATION.Visible = True
        Me.GLOCATION.VisibleIndex = 8
        Me.GLOCATION.Width = 85
        '
        'GMOBILEDATE
        '
        Me.GMOBILEDATE.Caption = "Mobile Date"
        Me.GMOBILEDATE.FieldName = "MOBILEDATE"
        Me.GMOBILEDATE.Name = "GMOBILEDATE"
        Me.GMOBILEDATE.OptionsColumn.AllowEdit = False
        Me.GMOBILEDATE.Visible = True
        Me.GMOBILEDATE.VisibleIndex = 9
        Me.GMOBILEDATE.Width = 85
        '
        'GMOBILELICENSE
        '
        Me.GMOBILELICENSE.Caption = "Mobile License"
        Me.GMOBILELICENSE.FieldName = "MOBILELICENSE"
        Me.GMOBILELICENSE.Name = "GMOBILELICENSE"
        Me.GMOBILELICENSE.OptionsColumn.AllowEdit = False
        Me.GMOBILELICENSE.Visible = True
        Me.GMOBILELICENSE.VisibleIndex = 10
        Me.GMOBILELICENSE.Width = 85
        '
        'CHK
        '
        Me.CHK.AutoHeight = False
        Me.CHK.Name = "CHK"
        '
        'CMBPROJECTNAME
        '
        Me.CMBPROJECTNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBPROJECTNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBPROJECTNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBPROJECTNAME.FormattingEnabled = True
        Me.CMBPROJECTNAME.Location = New System.Drawing.Point(524, 32)
        Me.CMBPROJECTNAME.Name = "CMBPROJECTNAME"
        Me.CMBPROJECTNAME.Size = New System.Drawing.Size(125, 21)
        Me.CMBPROJECTNAME.TabIndex = 2
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(577, 557)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 5
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'CMBPARTYNAME
        '
        Me.CMBPARTYNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBPARTYNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBPARTYNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBPARTYNAME.DropDownWidth = 400
        Me.CMBPARTYNAME.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.CMBPARTYNAME.FormattingEnabled = True
        Me.CMBPARTYNAME.Location = New System.Drawing.Point(74, 31)
        Me.CMBPARTYNAME.Name = "CMBPARTYNAME"
        Me.CMBPARTYNAME.Size = New System.Drawing.Size(250, 23)
        Me.CMBPARTYNAME.TabIndex = 0
        '
        'txtsrno
        '
        Me.txtsrno.BackColor = System.Drawing.Color.Linen
        Me.txtsrno.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrno.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtsrno.Location = New System.Drawing.Point(34, 31)
        Me.txtsrno.Name = "txtsrno"
        Me.txtsrno.ReadOnly = True
        Me.txtsrno.Size = New System.Drawing.Size(40, 23)
        Me.txtsrno.TabIndex = 0
        Me.txtsrno.TabStop = False
        Me.txtsrno.Text = " "
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'ClientDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1393, 591)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Name = "ClientDetails"
        Me.Text = "ClientDetails"
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMBCLIENTNAME As ComboBox
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CHK As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPARTYNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCLIENTNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPROJECTNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAMCDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEWAYDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVOICEDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWHATSAPPDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLOCATION As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMOBILEDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMOBILELICENSE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMBPROJECTNAME As ComboBox
    Friend WithEvents cmdexit As Button
    Friend WithEvents CMBPARTYNAME As ComboBox
    Friend WithEvents txtsrno As TextBox
    Friend WithEvents DTAMCDATE As MaskedTextBox
    Friend WithEvents DTEWAYDATE As MaskedTextBox
    Friend WithEvents DTEINVOICEDATE As MaskedTextBox
    Friend WithEvents DTWHATSAPPDATE As MaskedTextBox
    Friend WithEvents DTMOBILEDATE As MaskedTextBox
    Friend WithEvents TXTLOCATION As TextBox
    Friend WithEvents TXTMOBILELIC As TextBox
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents TXTNO As TextBox
End Class
