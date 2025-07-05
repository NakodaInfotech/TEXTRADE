<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SendMultipleMail
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
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.multiplemail = New System.Windows.Forms.TabPage()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GPRINTINITIALS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gdate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gname = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPARTYMAIL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENTMAIL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSUBJECT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREGID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREGNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GATTACHMENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFILENAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMAILBODY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKEDIT = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.MailBody = New System.Windows.Forms.TabPage()
        Me.cmbfifthadd = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbfourthadd = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbthirdadd = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbsecondadd = New System.Windows.Forms.ComboBox()
        Me.TXTMAILBODY = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbfirstadd = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.multiplemail.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MailBody.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label7)
        Me.BlendPanel1.Controls.Add(Me.TabControl1)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(221, 29)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Send Multiple E-Mail"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.multiplemail)
        Me.TabControl1.Controls.Add(Me.MailBody)
        Me.TabControl1.Location = New System.Drawing.Point(17, 65)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1169, 459)
        Me.TabControl1.TabIndex = 9
        '
        'multiplemail
        '
        Me.multiplemail.Controls.Add(Me.gridbilldetails)
        Me.multiplemail.Location = New System.Drawing.Point(4, 24)
        Me.multiplemail.Name = "multiplemail"
        Me.multiplemail.Padding = New System.Windows.Forms.Padding(3)
        Me.multiplemail.Size = New System.Drawing.Size(1161, 431)
        Me.multiplemail.TabIndex = 0
        Me.multiplemail.Text = "Multiple Mail"
        Me.multiplemail.UseVisualStyleBackColor = True
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(3, 3)
        Me.gridbilldetails.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKEDIT})
        Me.gridbilldetails.Size = New System.Drawing.Size(1155, 425)
        Me.gridbilldetails.TabIndex = 1
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GPRINTINITIALS, Me.gdate, Me.gname, Me.GPARTYMAIL, Me.GAGENT, Me.GAGENTMAIL, Me.GSUBJECT, Me.GINVNO, Me.GREGID, Me.GREGNAME, Me.GATTACHMENT, Me.GFILENAME, Me.GMAILBODY, Me.GGRANDTOTAL})
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
        Me.gname.Width = 230
        '
        'GPARTYMAIL
        '
        Me.GPARTYMAIL.Caption = "Party E-Mail Id"
        Me.GPARTYMAIL.FieldName = "PARTYEMAILID"
        Me.GPARTYMAIL.Name = "GPARTYMAIL"
        Me.GPARTYMAIL.Visible = True
        Me.GPARTYMAIL.VisibleIndex = 3
        Me.GPARTYMAIL.Width = 300
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
        'GAGENTMAIL
        '
        Me.GAGENTMAIL.Caption = "Agent E-Mail Id"
        Me.GAGENTMAIL.FieldName = "AGENTEMAILID"
        Me.GAGENTMAIL.Name = "GAGENTMAIL"
        Me.GAGENTMAIL.Visible = True
        Me.GAGENTMAIL.VisibleIndex = 5
        Me.GAGENTMAIL.Width = 300
        '
        'GSUBJECT
        '
        Me.GSUBJECT.Caption = "Subject"
        Me.GSUBJECT.FieldName = "SUBJECT"
        Me.GSUBJECT.Name = "GSUBJECT"
        Me.GSUBJECT.Visible = True
        Me.GSUBJECT.VisibleIndex = 6
        Me.GSUBJECT.Width = 200
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
        Me.GATTACHMENT.VisibleIndex = 7
        Me.GATTACHMENT.Width = 350
        '
        'GFILENAME
        '
        Me.GFILENAME.Caption = "File Name"
        Me.GFILENAME.FieldName = "FILENAME"
        Me.GFILENAME.Name = "GFILENAME"
        '
        'GMAILBODY
        '
        Me.GMAILBODY.Caption = "Mail Body"
        Me.GMAILBODY.FieldName = "MAILBODY"
        Me.GMAILBODY.Name = "GMAILBODY"
        '
        'GGRANDTOTAL
        '
        Me.GGRANDTOTAL.Caption = "Grand Total"
        Me.GGRANDTOTAL.DisplayFormat.FormatString = "0.00"
        Me.GGRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.GGRANDTOTAL.Name = "GGRANDTOTAL"
        Me.GGRANDTOTAL.Visible = True
        Me.GGRANDTOTAL.VisibleIndex = 8
        '
        'CHKEDIT
        '
        Me.CHKEDIT.AutoHeight = False
        Me.CHKEDIT.Name = "CHKEDIT"
        Me.CHKEDIT.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'MailBody
        '
        Me.MailBody.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.MailBody.Controls.Add(Me.cmbfifthadd)
        Me.MailBody.Controls.Add(Me.Label6)
        Me.MailBody.Controls.Add(Me.cmbfourthadd)
        Me.MailBody.Controls.Add(Me.Label5)
        Me.MailBody.Controls.Add(Me.cmbthirdadd)
        Me.MailBody.Controls.Add(Me.Label4)
        Me.MailBody.Controls.Add(Me.Label3)
        Me.MailBody.Controls.Add(Me.cmbsecondadd)
        Me.MailBody.Controls.Add(Me.TXTMAILBODY)
        Me.MailBody.Controls.Add(Me.Label2)
        Me.MailBody.Controls.Add(Me.cmbfirstadd)
        Me.MailBody.Controls.Add(Me.Label1)
        Me.MailBody.Location = New System.Drawing.Point(4, 24)
        Me.MailBody.Name = "MailBody"
        Me.MailBody.Padding = New System.Windows.Forms.Padding(3)
        Me.MailBody.Size = New System.Drawing.Size(1161, 431)
        Me.MailBody.TabIndex = 1
        Me.MailBody.Text = "E-mail/Mail Body"
        '
        'cmbfifthadd
        '
        Me.cmbfifthadd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbfifthadd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbfifthadd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbfifthadd.FormattingEnabled = True
        Me.cmbfifthadd.Location = New System.Drawing.Point(150, 183)
        Me.cmbfifthadd.MaxDropDownItems = 14
        Me.cmbfifthadd.Name = "cmbfifthadd"
        Me.cmbfifthadd.Size = New System.Drawing.Size(306, 22)
        Me.cmbfifthadd.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(32, 186)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 14)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Fifth Email Address"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbfourthadd
        '
        Me.cmbfourthadd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbfourthadd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbfourthadd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbfourthadd.FormattingEnabled = True
        Me.cmbfourthadd.Location = New System.Drawing.Point(150, 155)
        Me.cmbfourthadd.MaxDropDownItems = 14
        Me.cmbfourthadd.Name = "cmbfourthadd"
        Me.cmbfourthadd.Size = New System.Drawing.Size(306, 22)
        Me.cmbfourthadd.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 158)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 14)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Fourth Email Address"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbthirdadd
        '
        Me.cmbthirdadd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbthirdadd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbthirdadd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbthirdadd.FormattingEnabled = True
        Me.cmbthirdadd.Location = New System.Drawing.Point(150, 127)
        Me.cmbthirdadd.MaxDropDownItems = 14
        Me.cmbthirdadd.Name = "cmbthirdadd"
        Me.cmbthirdadd.Size = New System.Drawing.Size(306, 22)
        Me.cmbthirdadd.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(28, 130)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 14)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Third Email Address"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(291, 42)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "E-Mail / Mail Body"
        '
        'cmbsecondadd
        '
        Me.cmbsecondadd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbsecondadd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbsecondadd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsecondadd.FormattingEnabled = True
        Me.cmbsecondadd.Location = New System.Drawing.Point(150, 99)
        Me.cmbsecondadd.MaxDropDownItems = 14
        Me.cmbsecondadd.Name = "cmbsecondadd"
        Me.cmbsecondadd.Size = New System.Drawing.Size(306, 22)
        Me.cmbsecondadd.TabIndex = 7
        '
        'TXTMAILBODY
        '
        Me.TXTMAILBODY.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTMAILBODY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTMAILBODY.Location = New System.Drawing.Point(13, 211)
        Me.TXTMAILBODY.Multiline = True
        Me.TXTMAILBODY.Name = "TXTMAILBODY"
        Me.TXTMAILBODY.Size = New System.Drawing.Size(443, 195)
        Me.TXTMAILBODY.TabIndex = 8
        Me.TXTMAILBODY.TabStop = False
        Me.TXTMAILBODY.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(127, 14)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Second Email Address"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbfirstadd
        '
        Me.cmbfirstadd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbfirstadd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbfirstadd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbfirstadd.FormattingEnabled = True
        Me.cmbfirstadd.Location = New System.Drawing.Point(150, 71)
        Me.cmbfirstadd.MaxDropDownItems = 14
        Me.cmbfirstadd.Name = "cmbfirstadd"
        Me.cmbfirstadd.Size = New System.Drawing.Size(306, 22)
        Me.cmbfirstadd.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 14)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "First Email Address"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        'SendMultipleMail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "SendMultipleMail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Send Multiple Mail"
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.multiplemail.ResumeLayout(False)
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MailBody.ResumeLayout(False)
        Me.MailBody.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDOK As Button
    Friend WithEvents cmdcancel As Button
    Friend WithEvents cmbfirstadd As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbsecondadd As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TXTMAILBODY As TextBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents multiplemail As TabPage
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GPRINTINITIALS As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gdate As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gname As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPARTYMAIL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENTMAIL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSUBJECT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREGID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREGNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GATTACHMENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFILENAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMAILBODY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKEDIT As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents MailBody As TabPage
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbfifthadd As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbfourthadd As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbthirdadd As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
End Class
