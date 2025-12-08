<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MagicBoxForRecPay
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
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.miniToolStrip = New System.Windows.Forms.ToolStrip()
        Me.CMDDELETE = New System.Windows.Forms.Button()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TXTENTRYNO = New System.Windows.Forms.TextBox()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.DTENTERYDATE = New System.Windows.Forms.MaskedTextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.TXTCHQAMT = New System.Windows.Forms.TextBox()
        Me.CMBACCNAME = New System.Windows.Forms.ComboBox()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.TXTCHQNO = New System.Windows.Forms.TextBox()
        Me.DTCHQDATE = New System.Windows.Forms.DateTimePicker()
        Me.TXTPARTYBANKNAME = New System.Windows.Forms.TextBox()
        Me.txtadd = New System.Windows.Forms.TextBox()
        Me.CMBACCCODE = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TXTINWORDS = New System.Windows.Forms.TextBox()
        Me.CMBPAYTYPE = New System.Windows.Forms.ComboBox()
        Me.CMBSELLERNAME = New System.Windows.Forms.ComboBox()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.CMBTDSDEDUCTEDAC = New System.Windows.Forms.ComboBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TXTREMAINING = New System.Windows.Forms.TextBox()
        Me.CHKSELECTALL = New System.Windows.Forms.CheckBox()
        Me.GRIDBILLDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDBILL = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREFNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBALAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTDS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GDAYS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTEMPBAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOMPLAINT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GADJAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GADJTDS = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDBILLDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDBILL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'miniToolStrip
        '
        Me.miniToolStrip.AccessibleName = "New item selection"
        Me.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(1280, 25)
        Me.miniToolStrip.TabIndex = 0
        '
        'CMDDELETE
        '
        Me.CMDDELETE.BackColor = System.Drawing.Color.Transparent
        Me.CMDDELETE.FlatAppearance.BorderSize = 0
        Me.CMDDELETE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDELETE.ForeColor = System.Drawing.Color.Black
        Me.CMDDELETE.Location = New System.Drawing.Point(209, 518)
        Me.CMDDELETE.Name = "CMDDELETE"
        Me.CMDDELETE.Size = New System.Drawing.Size(80, 28)
        Me.CMDDELETE.TabIndex = 16
        Me.CMDDELETE.Text = "&Delete"
        Me.CMDDELETE.UseVisualStyleBackColor = False
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(37, 518)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 14
        Me.CMDOK.Text = "&Save"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(295, 518)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 17
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(257, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 15)
        Me.Label9.TabIndex = 632
        Me.Label9.Text = "Date"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(89, 27)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(38, 15)
        Me.Label12.TabIndex = 634
        Me.Label12.Text = "Sr. No"
        '
        'TXTENTRYNO
        '
        Me.TXTENTRYNO.BackColor = System.Drawing.Color.Linen
        Me.TXTENTRYNO.Enabled = False
        Me.TXTENTRYNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTENTRYNO.Location = New System.Drawing.Point(130, 23)
        Me.TXTENTRYNO.Name = "TXTENTRYNO"
        Me.TXTENTRYNO.ReadOnly = True
        Me.TXTENTRYNO.Size = New System.Drawing.Size(94, 23)
        Me.TXTENTRYNO.TabIndex = 0
        Me.TXTENTRYNO.TabStop = False
        Me.TXTENTRYNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.FlatAppearance.BorderSize = 0
        Me.CMDCLEAR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.Black
        Me.CMDCLEAR.Location = New System.Drawing.Point(123, 518)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 15
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
        '
        'DTENTERYDATE
        '
        Me.DTENTERYDATE.AsciiOnly = True
        Me.DTENTERYDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.DTENTERYDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTENTERYDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTENTERYDATE.Location = New System.Drawing.Point(292, 23)
        Me.DTENTERYDATE.Mask = "00/00/0000"
        Me.DTENTERYDATE.Name = "DTENTERYDATE"
        Me.DTENTERYDATE.Size = New System.Drawing.Size(88, 23)
        Me.DTENTERYDATE.TabIndex = 1
        Me.DTENTERYDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTENTERYDATE.ValidatingType = GetType(Date)
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.TXTREMARKS)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(13, 283)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(367, 145)
        Me.GroupBox5.TabIndex = 13
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Remarks"
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.ForeColor = System.Drawing.Color.DimGray
        Me.TXTREMARKS.Location = New System.Drawing.Point(8, 17)
        Me.TXTREMARKS.Multiline = True
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(353, 124)
        Me.TXTREMARKS.TabIndex = 0
        '
        'TXTCHQAMT
        '
        Me.TXTCHQAMT.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTCHQAMT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCHQAMT.ForeColor = System.Drawing.Color.Black
        Me.TXTCHQAMT.Location = New System.Drawing.Point(130, 197)
        Me.TXTCHQAMT.Name = "TXTCHQAMT"
        Me.TXTCHQAMT.Size = New System.Drawing.Size(94, 23)
        Me.TXTCHQAMT.TabIndex = 8
        Me.TXTCHQAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBACCNAME
        '
        Me.CMBACCNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBACCNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBACCNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBACCNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBACCNAME.FormattingEnabled = True
        Me.CMBACCNAME.Location = New System.Drawing.Point(130, 52)
        Me.CMBACCNAME.MaxDropDownItems = 14
        Me.CMBACCNAME.Name = "CMBACCNAME"
        Me.CMBACCNAME.Size = New System.Drawing.Size(250, 23)
        Me.CMBACCNAME.TabIndex = 2
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(130, 81)
        Me.CMBNAME.MaxDropDownItems = 14
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(250, 23)
        Me.CMBNAME.TabIndex = 3
        '
        'TXTCHQNO
        '
        Me.TXTCHQNO.BackColor = System.Drawing.Color.White
        Me.TXTCHQNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCHQNO.Location = New System.Drawing.Point(130, 139)
        Me.TXTCHQNO.MaxLength = 6
        Me.TXTCHQNO.Name = "TXTCHQNO"
        Me.TXTCHQNO.Size = New System.Drawing.Size(94, 23)
        Me.TXTCHQNO.TabIndex = 5
        Me.TXTCHQNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DTCHQDATE
        '
        Me.DTCHQDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTCHQDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTCHQDATE.Location = New System.Drawing.Point(292, 139)
        Me.DTCHQDATE.Name = "DTCHQDATE"
        Me.DTCHQDATE.Size = New System.Drawing.Size(88, 23)
        Me.DTCHQDATE.TabIndex = 6
        '
        'TXTPARTYBANKNAME
        '
        Me.TXTPARTYBANKNAME.BackColor = System.Drawing.Color.White
        Me.TXTPARTYBANKNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPARTYBANKNAME.ForeColor = System.Drawing.Color.Black
        Me.TXTPARTYBANKNAME.Location = New System.Drawing.Point(130, 168)
        Me.TXTPARTYBANKNAME.Name = "TXTPARTYBANKNAME"
        Me.TXTPARTYBANKNAME.Size = New System.Drawing.Size(250, 23)
        Me.TXTPARTYBANKNAME.TabIndex = 7
        '
        'txtadd
        '
        Me.txtadd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd.ForeColor = System.Drawing.Color.DimGray
        Me.txtadd.Location = New System.Drawing.Point(1392, 7)
        Me.txtadd.Multiline = True
        Me.txtadd.Name = "txtadd"
        Me.txtadd.Size = New System.Drawing.Size(10, 31)
        Me.txtadd.TabIndex = 704
        Me.txtadd.TabStop = False
        Me.txtadd.Visible = False
        '
        'CMBACCCODE
        '
        Me.CMBACCCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBACCCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBACCCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBACCCODE.FormattingEnabled = True
        Me.CMBACCCODE.Items.AddRange(New Object() {""})
        Me.CMBACCCODE.Location = New System.Drawing.Point(1374, 11)
        Me.CMBACCCODE.Name = "CMBACCCODE"
        Me.CMBACCCODE.Size = New System.Drawing.Size(12, 22)
        Me.CMBACCCODE.TabIndex = 705
        Me.CMBACCCODE.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.TXTINWORDS)
        Me.GroupBox3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox3.Location = New System.Drawing.Point(13, 434)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(367, 78)
        Me.GroupBox3.TabIndex = 706
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "In Words"
        '
        'TXTINWORDS
        '
        Me.TXTINWORDS.BackColor = System.Drawing.Color.Linen
        Me.TXTINWORDS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTINWORDS.ForeColor = System.Drawing.Color.Black
        Me.TXTINWORDS.Location = New System.Drawing.Point(6, 17)
        Me.TXTINWORDS.Multiline = True
        Me.TXTINWORDS.Name = "TXTINWORDS"
        Me.TXTINWORDS.ReadOnly = True
        Me.TXTINWORDS.Size = New System.Drawing.Size(356, 55)
        Me.TXTINWORDS.TabIndex = 0
        Me.TXTINWORDS.TabStop = False
        '
        'CMBPAYTYPE
        '
        Me.CMBPAYTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBPAYTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBPAYTYPE.FormattingEnabled = True
        Me.CMBPAYTYPE.Items.AddRange(New Object() {"On Account", "Against Bill"})
        Me.CMBPAYTYPE.Location = New System.Drawing.Point(292, 197)
        Me.CMBPAYTYPE.MaxDropDownItems = 14
        Me.CMBPAYTYPE.Name = "CMBPAYTYPE"
        Me.CMBPAYTYPE.Size = New System.Drawing.Size(88, 23)
        Me.CMBPAYTYPE.TabIndex = 9
        '
        'CMBSELLERNAME
        '
        Me.CMBSELLERNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSELLERNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSELLERNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBSELLERNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSELLERNAME.FormattingEnabled = True
        Me.CMBSELLERNAME.Items.AddRange(New Object() {""})
        Me.CMBSELLERNAME.Location = New System.Drawing.Point(130, 110)
        Me.CMBSELLERNAME.Name = "CMBSELLERNAME"
        Me.CMBSELLERNAME.Size = New System.Drawing.Size(250, 23)
        Me.CMBSELLERNAME.TabIndex = 4
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMBTDSDEDUCTEDAC)
        Me.BlendPanel1.Controls.Add(Me.Label64)
        Me.BlendPanel1.Controls.Add(Me.Label10)
        Me.BlendPanel1.Controls.Add(Me.TXTREMAINING)
        Me.BlendPanel1.Controls.Add(Me.CHKSELECTALL)
        Me.BlendPanel1.Controls.Add(Me.GRIDBILLDETAILS)
        Me.BlendPanel1.Controls.Add(Me.Label8)
        Me.BlendPanel1.Controls.Add(Me.Label7)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.CMBSELLERNAME)
        Me.BlendPanel1.Controls.Add(Me.CMBPAYTYPE)
        Me.BlendPanel1.Controls.Add(Me.GroupBox3)
        Me.BlendPanel1.Controls.Add(Me.CMBACCCODE)
        Me.BlendPanel1.Controls.Add(Me.txtadd)
        Me.BlendPanel1.Controls.Add(Me.TXTPARTYBANKNAME)
        Me.BlendPanel1.Controls.Add(Me.DTCHQDATE)
        Me.BlendPanel1.Controls.Add(Me.TXTCHQNO)
        Me.BlendPanel1.Controls.Add(Me.CMBNAME)
        Me.BlendPanel1.Controls.Add(Me.CMBACCNAME)
        Me.BlendPanel1.Controls.Add(Me.TXTCHQAMT)
        Me.BlendPanel1.Controls.Add(Me.GroupBox5)
        Me.BlendPanel1.Controls.Add(Me.DTENTERYDATE)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.TXTENTRYNO)
        Me.BlendPanel1.Controls.Add(Me.Label12)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.CMDDELETE)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.ForeColor = System.Drawing.Color.Transparent
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1479, 661)
        Me.BlendPanel1.TabIndex = 0
        '
        'CMBTDSDEDUCTEDAC
        '
        Me.CMBTDSDEDUCTEDAC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTDSDEDUCTEDAC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTDSDEDUCTEDAC.BackColor = System.Drawing.Color.White
        Me.CMBTDSDEDUCTEDAC.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTDSDEDUCTEDAC.FormattingEnabled = True
        Me.CMBTDSDEDUCTEDAC.Items.AddRange(New Object() {""})
        Me.CMBTDSDEDUCTEDAC.Location = New System.Drawing.Point(130, 255)
        Me.CMBTDSDEDUCTEDAC.Name = "CMBTDSDEDUCTEDAC"
        Me.CMBTDSDEDUCTEDAC.Size = New System.Drawing.Size(250, 22)
        Me.CMBTDSDEDUCTEDAC.TabIndex = 11
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.BackColor = System.Drawing.Color.Transparent
        Me.Label64.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.ForeColor = System.Drawing.Color.Black
        Me.Label64.Location = New System.Drawing.Point(80, 259)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(47, 14)
        Me.Label64.TabIndex = 873
        Me.Label64.Text = "TDS A/c"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(38, 230)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 15)
        Me.Label10.TabIndex = 871
        Me.Label10.Text = "Remaining Amt"
        '
        'TXTREMAINING
        '
        Me.TXTREMAINING.BackColor = System.Drawing.Color.Linen
        Me.TXTREMAINING.Enabled = False
        Me.TXTREMAINING.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTREMAINING.Location = New System.Drawing.Point(130, 226)
        Me.TXTREMAINING.Name = "TXTREMAINING"
        Me.TXTREMAINING.ReadOnly = True
        Me.TXTREMAINING.Size = New System.Drawing.Size(94, 23)
        Me.TXTREMAINING.TabIndex = 10
        Me.TXTREMAINING.TabStop = False
        Me.TXTREMAINING.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CHKSELECTALL
        '
        Me.CHKSELECTALL.AutoSize = True
        Me.CHKSELECTALL.BackColor = System.Drawing.Color.Transparent
        Me.CHKSELECTALL.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSELECTALL.ForeColor = System.Drawing.Color.Black
        Me.CHKSELECTALL.Location = New System.Drawing.Point(427, 13)
        Me.CHKSELECTALL.Name = "CHKSELECTALL"
        Me.CHKSELECTALL.Size = New System.Drawing.Size(77, 18)
        Me.CHKSELECTALL.TabIndex = 867
        Me.CHKSELECTALL.Text = "Select All"
        Me.CHKSELECTALL.UseVisualStyleBackColor = False
        '
        'GRIDBILLDETAILS
        '
        Me.GRIDBILLDETAILS.Location = New System.Drawing.Point(393, 37)
        Me.GRIDBILLDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDBILLDETAILS.MainView = Me.GRIDBILL
        Me.GRIDBILLDETAILS.Name = "GRIDBILLDETAILS"
        Me.GRIDBILLDETAILS.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.GRIDBILLDETAILS.Size = New System.Drawing.Size(1074, 612)
        Me.GRIDBILLDETAILS.TabIndex = 12
        Me.GRIDBILLDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDBILL})
        '
        'GRIDBILL
        '
        Me.GRIDBILL.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILL.Appearance.Row.Options.UseFont = True
        Me.GRIDBILL.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHK, Me.GBILLNO, Me.GREFNO, Me.GDATE, Me.GBALAMT, Me.GTOTAL, Me.GTDS, Me.GDAYS, Me.GTEMPBAL, Me.GCOMPLAINT, Me.GADJAMT, Me.GADJTDS})
        Me.GRIDBILL.GridControl = Me.GRIDBILLDETAILS
        Me.GRIDBILL.Name = "GRIDBILL"
        Me.GRIDBILL.OptionsCustomization.AllowColumnMoving = False
        Me.GRIDBILL.OptionsCustomization.AllowGroup = False
        Me.GRIDBILL.OptionsCustomization.AllowQuickHideColumns = False
        Me.GRIDBILL.OptionsView.ColumnAutoWidth = False
        Me.GRIDBILL.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDBILL.OptionsView.ShowAutoFilterRow = True
        Me.GRIDBILL.OptionsView.ShowFooter = True
        Me.GRIDBILL.OptionsView.ShowGroupPanel = False
        '
        'GCHK
        '
        Me.GCHK.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GCHK.FieldName = "CHK"
        Me.GCHK.Name = "GCHK"
        Me.GCHK.OptionsColumn.ShowCaption = False
        Me.GCHK.Visible = True
        Me.GCHK.VisibleIndex = 0
        Me.GCHK.Width = 30
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GBILLNO
        '
        Me.GBILLNO.Caption = "Bill No."
        Me.GBILLNO.FieldName = "BILLNO"
        Me.GBILLNO.ImageOptions.ImageIndex = 0
        Me.GBILLNO.Name = "GBILLNO"
        Me.GBILLNO.OptionsColumn.AllowEdit = False
        Me.GBILLNO.Visible = True
        Me.GBILLNO.VisibleIndex = 1
        Me.GBILLNO.Width = 150
        '
        'GDATE
        '
        Me.GDATE.Caption = "Date"
        Me.GDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GDATE.FieldName = "DATE"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.OptionsColumn.AllowEdit = False
        Me.GDATE.Visible = True
        Me.GDATE.VisibleIndex = 3
        Me.GDATE.Width = 90
        '
        'GREFNO
        '
        Me.GREFNO.Caption = "Party Bil No"
        Me.GREFNO.FieldName = "REFNO"
        Me.GREFNO.Name = "GREFNO"
        Me.GREFNO.OptionsColumn.AllowEdit = False
        Me.GREFNO.Visible = True
        Me.GREFNO.VisibleIndex = 2
        Me.GREFNO.Width = 150
        '
        'GTOTAL
        '
        Me.GTOTAL.Caption = "B‎ill Amt"
        Me.GTOTAL.DisplayFormat.FormatString = "0.00"
        Me.GTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTAL.FieldName = "BILLAMT"
        Me.GTOTAL.Name = "GTOTAL"
        Me.GTOTAL.OptionsColumn.AllowEdit = False
        Me.GTOTAL.Visible = True
        Me.GTOTAL.VisibleIndex = 5
        Me.GTOTAL.Width = 100
        '
        'GBALAMT
        '
        Me.GBALAMT.Caption = "Bal Amt"
        Me.GBALAMT.DisplayFormat.FormatString = "0.00"
        Me.GBALAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GBALAMT.FieldName = "BALAMT"
        Me.GBALAMT.Name = "GBALAMT"
        Me.GBALAMT.OptionsColumn.AllowEdit = False
        Me.GBALAMT.Visible = True
        Me.GBALAMT.VisibleIndex = 4
        Me.GBALAMT.Width = 100
        '
        'GTDS
        '
        Me.GTDS.Caption = "T.D.S."
        Me.GTDS.DisplayFormat.FormatString = "0.00"
        Me.GTDS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTDS.FieldName = "TDS"
        Me.GTDS.Name = "GTDS"
        Me.GTDS.OptionsColumn.AllowEdit = False
        Me.GTDS.Visible = True
        Me.GTDS.VisibleIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(27, 172)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 15)
        Me.Label8.TabIndex = 757
        Me.Label8.Text = "Party Bank Name"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(235, 201)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 15)
        Me.Label7.TabIndex = 756
        Me.Label7.Text = "Pay Type"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(54, 201)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 15)
        Me.Label6.TabIndex = 755
        Me.Label6.Text = "Chq Amount"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(246, 143)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 15)
        Me.Label5.TabIndex = 754
        Me.Label5.Text = "Chq Dt"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(81, 143)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 15)
        Me.Label4.TabIndex = 753
        Me.Label4.Text = "Chq No"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(89, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 15)
        Me.Label3.TabIndex = 752
        Me.Label3.Text = "Seller"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(89, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 15)
        Me.Label2.TabIndex = 751
        Me.Label2.Text = "Buyer"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(59, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 15)
        Me.Label1.TabIndex = 750
        Me.Label1.Text = "Bank Name"
        '
        'GDAYS
        '
        Me.GDAYS.Caption = "Days"
        Me.GDAYS.DisplayFormat.FormatString = "0"
        Me.GDAYS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GDAYS.FieldName = "DAYS"
        Me.GDAYS.Name = "GDAYS"
        Me.GDAYS.OptionsColumn.AllowEdit = False
        Me.GDAYS.Visible = True
        Me.GDAYS.VisibleIndex = 7
        Me.GDAYS.Width = 50
        '
        'GTEMPBAL
        '
        Me.GTEMPBAL.Caption = "Temp Bal"
        Me.GTEMPBAL.DisplayFormat.FormatString = "0.00"
        Me.GTEMPBAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTEMPBAL.FieldName = "TEMPBAL"
        Me.GTEMPBAL.Name = "GTEMPBAL"
        Me.GTEMPBAL.OptionsColumn.AllowEdit = False
        Me.GTEMPBAL.Visible = True
        Me.GTEMPBAL.VisibleIndex = 8
        Me.GTEMPBAL.Width = 100
        '
        'GCOMPLAINT
        '
        Me.GCOMPLAINT.Caption = "Complaint"
        Me.GCOMPLAINT.FieldName = "COMPLAINT"
        Me.GCOMPLAINT.Name = "GCOMPLAINT"
        Me.GCOMPLAINT.OptionsColumn.AllowEdit = False
        '
        'GADJAMT
        '
        Me.GADJAMT.Caption = "Adjust Amt"
        Me.GADJAMT.DisplayFormat.FormatString = "0.00"
        Me.GADJAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GADJAMT.FieldName = "ADJAMT"
        Me.GADJAMT.Name = "GADJAMT"
        Me.GADJAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ADJAMT", "0.00")})
        Me.GADJAMT.Visible = True
        Me.GADJAMT.VisibleIndex = 9
        Me.GADJAMT.Width = 100
        '
        'GADJTDS
        '
        Me.GADJTDS.Caption = "Adjust TDS"
        Me.GADJTDS.DisplayFormat.FormatString = "0.00"
        Me.GADJTDS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GADJTDS.FieldName = "ADJTDS"
        Me.GADJTDS.Name = "GADJTDS"
        Me.GADJTDS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ADJTDS", "0.00")})
        Me.GADJTDS.Visible = True
        Me.GADJTDS.VisibleIndex = 10
        '
        'MagicBoxForRecPay
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1479, 661)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "MagicBoxForRecPay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Magic Box For Rec Pay"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDBILLDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDBILL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMBSELLERNAME As ComboBox
    Friend WithEvents CMBPAYTYPE As ComboBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents TXTINWORDS As TextBox
    Friend WithEvents CMBACCCODE As ComboBox
    Friend WithEvents txtadd As TextBox
    Friend WithEvents TXTPARTYBANKNAME As TextBox
    Friend WithEvents DTCHQDATE As DateTimePicker
    Friend WithEvents TXTCHQNO As TextBox
    Friend WithEvents CMBNAME As ComboBox
    Friend WithEvents CMBACCNAME As ComboBox
    Friend WithEvents TXTCHQAMT As TextBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents TXTREMARKS As TextBox
    Friend WithEvents DTENTERYDATE As MaskedTextBox
    Friend WithEvents CMDCLEAR As Button
    Friend WithEvents TXTENTRYNO As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDOK As Button
    Friend WithEvents CMDDELETE As Button
    Friend WithEvents miniToolStrip As ToolStrip
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents CMBTDSDEDUCTEDAC As ComboBox
    Friend WithEvents Label64 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents TXTREMAINING As TextBox
    Friend WithEvents CHKSELECTALL As CheckBox
    Friend WithEvents GRIDBILLDETAILS As DevExpress.XtraGrid.GridControl
    Friend WithEvents GRIDBILL As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREFNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBALAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTDS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDAYS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTEMPBAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOMPLAINT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GADJAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GADJTDS As DevExpress.XtraGrid.Columns.GridColumn
End Class
