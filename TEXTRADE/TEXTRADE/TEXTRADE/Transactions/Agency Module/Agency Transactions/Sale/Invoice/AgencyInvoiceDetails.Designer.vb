﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AgencyInvoiceDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AgencyInvoiceDetails))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.TOOLMAIL = New System.Windows.Forms.ToolStripButton()
        Me.TOOLWHATSAPP = New System.Windows.Forms.ToolStripButton()
        Me.TOOLCMBINVCOPY = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLGRIDDETAILS = New System.Windows.Forms.ToolStripButton()
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.CHKBLANKPAPER = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CHKMULTI = New System.Windows.Forms.CheckBox()
        Me.TXTCOPIES = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TXTTO = New System.Windows.Forms.TextBox()
        Me.TXTFROM = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gsrno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gdate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREFNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCHALLANNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCHALLANDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gname = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSTATENAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSTATECODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GADD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSHIPPEDTO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEWAYBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTRANSPORT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLRDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBALES = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDISC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSPDISCAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCHARGES = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALTAXABLEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALCGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALSGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALIGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALWITHGST = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAPPLYTCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTCSPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTCSAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRECDAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRETURNAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBALANCE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDISPUTED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBILLCHECKED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSUPPLIERNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRECDDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GIRNNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GACKNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GACKDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDOCKETNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDOCKETDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOURIER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCHGSDTLS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFROMCITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOCITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GUSERNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCREATEDDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPONO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPARTYMAIL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPARTYWHATSAPP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENTMAIL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENTWHATSAPP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPRINTINITIALS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINITIALS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBROKERAGEPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBROKERAGE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDISCPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDISCOUNT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCDPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCASHDISC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSPECIALREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOSTCENTERNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCREATEDBY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREFERREDBY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTRADINGACC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMONTHNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOMM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOMMTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKEDIT = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.lbl = New System.Windows.Forms.Label()
        Me.PRINTDOC = New System.Drawing.Printing.PrintDocument()
        Me.PRINTDIALOG = New System.Windows.Forms.PrintDialog()
        Me.BlendPanel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.CHKBLANKPAPER)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.CHKMULTI)
        Me.BlendPanel1.Controls.Add(Me.TXTCOPIES)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.Label10)
        Me.BlendPanel1.Controls.Add(Me.TXTTO)
        Me.BlendPanel1.Controls.Add(Me.TXTFROM)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Controls.Add(Me.Label21)
        Me.BlendPanel1.Controls.Add(Me.Label22)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.toolStripSeparator, Me.PrintToolStripButton, Me.TOOLMAIL, Me.TOOLWHATSAPP, Me.TOOLCMBINVCOPY, Me.ToolStripButton2, Me.ToolStripSeparator1, Me.TOOLGRIDDETAILS, Me.TOOLREFRESH, Me.ToolStripSeparator2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 806
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(59, 22)
        Me.ToolStripButton1.Text = "Add New"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'TOOLMAIL
        '
        Me.TOOLMAIL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLMAIL.Image = Global.TEXTRADE.My.Resources.Resources.MAIL_IMAGE
        Me.TOOLMAIL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLMAIL.Name = "TOOLMAIL"
        Me.TOOLMAIL.Size = New System.Drawing.Size(23, 22)
        Me.TOOLMAIL.Text = "Mail Invoice Directly"
        '
        'TOOLWHATSAPP
        '
        Me.TOOLWHATSAPP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLWHATSAPP.Image = Global.TEXTRADE.My.Resources.Resources.WHATSAPP
        Me.TOOLWHATSAPP.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLWHATSAPP.Name = "TOOLWHATSAPP"
        Me.TOOLWHATSAPP.Size = New System.Drawing.Size(23, 22)
        Me.TOOLWHATSAPP.Text = "&Whatsapp"
        '
        'TOOLCMBINVCOPY
        '
        Me.TOOLCMBINVCOPY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TOOLCMBINVCOPY.Name = "TOOLCMBINVCOPY"
        Me.TOOLCMBINVCOPY.Size = New System.Drawing.Size(130, 25)
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "&Print"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'TOOLGRIDDETAILS
        '
        Me.TOOLGRIDDETAILS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TOOLGRIDDETAILS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLGRIDDETAILS.Name = "TOOLGRIDDETAILS"
        Me.TOOLGRIDDETAILS.Size = New System.Drawing.Size(77, 22)
        Me.TOOLGRIDDETAILS.Text = "Grid Details"
        '
        'TOOLREFRESH
        '
        Me.TOOLREFRESH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLREFRESH.Image = Global.TEXTRADE.My.Resources.Resources.refresh1
        Me.TOOLREFRESH.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLREFRESH.Name = "TOOLREFRESH"
        Me.TOOLREFRESH.Size = New System.Drawing.Size(23, 22)
        Me.TOOLREFRESH.Text = "&Refresh"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'CHKBLANKPAPER
        '
        Me.CHKBLANKPAPER.AutoSize = True
        Me.CHKBLANKPAPER.BackColor = System.Drawing.Color.White
        Me.CHKBLANKPAPER.Location = New System.Drawing.Point(842, 4)
        Me.CHKBLANKPAPER.Name = "CHKBLANKPAPER"
        Me.CHKBLANKPAPER.Size = New System.Drawing.Size(99, 19)
        Me.CHKBLANKPAPER.TabIndex = 805
        Me.CHKBLANKPAPER.Text = "Blank Header"
        Me.CHKBLANKPAPER.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(754, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 14)
        Me.Label4.TabIndex = 797
        Me.Label4.Text = "Copies"
        '
        'CHKMULTI
        '
        Me.CHKMULTI.AutoSize = True
        Me.CHKMULTI.BackColor = System.Drawing.Color.White
        Me.CHKMULTI.Location = New System.Drawing.Point(464, 4)
        Me.CHKMULTI.Name = "CHKMULTI"
        Me.CHKMULTI.Size = New System.Drawing.Size(102, 19)
        Me.CHKMULTI.TabIndex = 4
        Me.CHKMULTI.Text = "Print Multiple"
        Me.CHKMULTI.UseVisualStyleBackColor = False
        '
        'TXTCOPIES
        '
        Me.TXTCOPIES.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCOPIES.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOPIES.Location = New System.Drawing.Point(799, 2)
        Me.TXTCOPIES.Name = "TXTCOPIES"
        Me.TXTCOPIES.Size = New System.Drawing.Size(29, 22)
        Me.TXTCOPIES.TabIndex = 796
        Me.TXTCOPIES.Text = "1"
        Me.TXTCOPIES.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(664, 6)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(19, 14)
        Me.Label9.TabIndex = 787
        Me.Label9.Text = "To"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(566, 6)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 14)
        Me.Label10.TabIndex = 786
        Me.Label10.Text = "From"
        '
        'TXTTO
        '
        Me.TXTTO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTTO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTO.Location = New System.Drawing.Point(685, 2)
        Me.TXTTO.Name = "TXTTO"
        Me.TXTTO.Size = New System.Drawing.Size(57, 22)
        Me.TXTTO.TabIndex = 6
        Me.TXTTO.TabStop = False
        Me.TXTTO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTFROM
        '
        Me.TXTFROM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTFROM.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFROM.Location = New System.Drawing.Point(601, 2)
        Me.TXTFROM.Name = "TXTFROM"
        Me.TXTFROM.Size = New System.Drawing.Size(57, 22)
        Me.TXTFROM.TabIndex = 5
        Me.TXTFROM.TabStop = False
        Me.TXTFROM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(228, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 14)
        Me.Label2.TabIndex = 447
        Me.Label2.Text = "Disputed"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.LightGreen
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(291, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(18, 17)
        Me.Label3.TabIndex = 446
        Me.Label3.Text = "   "
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Red
        Me.Label21.Location = New System.Drawing.Point(312, 39)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(52, 14)
        Me.Label21.TabIndex = 445
        Me.Label21.Text = "Checked"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Location = New System.Drawing.Point(206, 38)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(18, 17)
        Me.Label22.TabIndex = 444
        Me.Label22.Text = "   "
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(22, 61)
        Me.gridbilldetails.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKEDIT})
        Me.gridbilldetails.Size = New System.Drawing.Size(1200, 472)
        Me.gridbilldetails.TabIndex = 0
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHK, Me.gsrno, Me.gdate, Me.GREFNO, Me.GCHALLANNO, Me.GCHALLANDATE, Me.gname, Me.GGSTIN, Me.GCITY, Me.GSTATENAME, Me.GSTATECODE, Me.GADD, Me.GAGENT, Me.GSHIPPEDTO, Me.GEWAYBILLNO, Me.GTRANSPORT, Me.GLRNO, Me.GLRDATE, Me.GSO, Me.GBALES, Me.GPCS, Me.GMTRS, Me.GTOTALAMT, Me.GDISC, Me.GSPDISCAMT, Me.GCHARGES, Me.GTOTALTAXABLEAMT, Me.GTOTALCGSTAMT, Me.GTOTALSGSTAMT, Me.GTOTALIGSTAMT, Me.GTOTALWITHGST, Me.GAPPLYTCS, Me.GTCSPER, Me.GTCSAMT, Me.GGRANDTOTAL, Me.GRECDAMT, Me.GRETURNAMT, Me.GBALANCE, Me.GREMARKS, Me.GDISPUTED, Me.GBILLCHECKED, Me.GSUPPLIERNAME, Me.GRECDDATE, Me.GIRNNO, Me.GACKNO, Me.GACKDATE, Me.GDOCKETNO, Me.GDOCKETDATE, Me.GCOURIER, Me.GCHGSDTLS, Me.GFROMCITY, Me.GTOCITY, Me.GUSERNAME, Me.GCREATEDDATE, Me.GPONO, Me.GPARTYMAIL, Me.GPARTYWHATSAPP, Me.GAGENTMAIL, Me.GAGENTWHATSAPP, Me.GPRINTINITIALS, Me.GINITIALS, Me.GBROKERAGEPER, Me.GBROKERAGE, Me.GDISCPER, Me.GDISCOUNT, Me.GCDPER, Me.GCASHDISC, Me.GSPECIALREMARKS, Me.GCOSTCENTERNAME, Me.GCREATEDBY, Me.GREFERREDBY, Me.GTRADINGACC, Me.GMONTHNAME, Me.GCOMM, Me.GCOMMTYPE})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        '
        'GCHK
        '
        Me.GCHK.FieldName = "CHK"
        Me.GCHK.Name = "GCHK"
        Me.GCHK.Visible = True
        Me.GCHK.VisibleIndex = 0
        Me.GCHK.Width = 30
        '
        'gsrno
        '
        Me.gsrno.Caption = "Sr. No"
        Me.gsrno.FieldName = "SRNO"
        Me.gsrno.ImageIndex = 1
        Me.gsrno.Name = "gsrno"
        Me.gsrno.OptionsColumn.AllowEdit = False
        Me.gsrno.Visible = True
        Me.gsrno.VisibleIndex = 1
        Me.gsrno.Width = 60
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
        Me.gdate.VisibleIndex = 2
        '
        'GREFNO
        '
        Me.GREFNO.Caption = "Ref No"
        Me.GREFNO.FieldName = "REFNO"
        Me.GREFNO.Name = "GREFNO"
        Me.GREFNO.OptionsColumn.AllowEdit = False
        Me.GREFNO.Visible = True
        Me.GREFNO.VisibleIndex = 3
        '
        'GCHALLANNO
        '
        Me.GCHALLANNO.Caption = "Challan No."
        Me.GCHALLANNO.FieldName = "CHALLANNO"
        Me.GCHALLANNO.Name = "GCHALLANNO"
        Me.GCHALLANNO.OptionsColumn.AllowEdit = False
        Me.GCHALLANNO.Visible = True
        Me.GCHALLANNO.VisibleIndex = 4
        '
        'GCHALLANDATE
        '
        Me.GCHALLANDATE.Caption = "Ch. Date"
        Me.GCHALLANDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GCHALLANDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GCHALLANDATE.FieldName = "CHALLANDATE"
        Me.GCHALLANDATE.Name = "GCHALLANDATE"
        Me.GCHALLANDATE.OptionsColumn.AllowEdit = False
        Me.GCHALLANDATE.Visible = True
        Me.GCHALLANDATE.VisibleIndex = 5
        '
        'gname
        '
        Me.gname.Caption = "Buyer's Name"
        Me.gname.FieldName = "NAME"
        Me.gname.ImageIndex = 0
        Me.gname.Name = "gname"
        Me.gname.OptionsColumn.AllowEdit = False
        Me.gname.Visible = True
        Me.gname.VisibleIndex = 6
        Me.gname.Width = 230
        '
        'GGSTIN
        '
        Me.GGSTIN.Caption = "GSTIN"
        Me.GGSTIN.FieldName = "GSTIN"
        Me.GGSTIN.Name = "GGSTIN"
        Me.GGSTIN.OptionsColumn.AllowEdit = False
        Me.GGSTIN.Visible = True
        Me.GGSTIN.VisibleIndex = 9
        Me.GGSTIN.Width = 100
        '
        'GCITY
        '
        Me.GCITY.Caption = "City"
        Me.GCITY.FieldName = "CITY"
        Me.GCITY.Name = "GCITY"
        Me.GCITY.OptionsColumn.AllowEdit = False
        Me.GCITY.Visible = True
        Me.GCITY.VisibleIndex = 10
        Me.GCITY.Width = 100
        '
        'GSTATENAME
        '
        Me.GSTATENAME.Caption = "State"
        Me.GSTATENAME.FieldName = "STATENAME"
        Me.GSTATENAME.Name = "GSTATENAME"
        Me.GSTATENAME.OptionsColumn.AllowEdit = False
        Me.GSTATENAME.Visible = True
        Me.GSTATENAME.VisibleIndex = 11
        '
        'GSTATECODE
        '
        Me.GSTATECODE.Caption = "Code"
        Me.GSTATECODE.FieldName = "STATECODE"
        Me.GSTATECODE.Name = "GSTATECODE"
        Me.GSTATECODE.OptionsColumn.AllowEdit = False
        Me.GSTATECODE.Visible = True
        Me.GSTATECODE.VisibleIndex = 12
        Me.GSTATECODE.Width = 40
        '
        'GADD
        '
        Me.GADD.Caption = "Add."
        Me.GADD.FieldName = "ADDRESS"
        Me.GADD.Name = "GADD"
        Me.GADD.OptionsColumn.AllowEdit = False
        Me.GADD.Visible = True
        Me.GADD.VisibleIndex = 13
        Me.GADD.Width = 150
        '
        'GAGENT
        '
        Me.GAGENT.Caption = "Agent Name"
        Me.GAGENT.FieldName = "AGENTNAME"
        Me.GAGENT.Name = "GAGENT"
        Me.GAGENT.OptionsColumn.AllowEdit = False
        Me.GAGENT.Visible = True
        Me.GAGENT.VisibleIndex = 14
        Me.GAGENT.Width = 150
        '
        'GSHIPPEDTO
        '
        Me.GSHIPPEDTO.Caption = "Shipped To"
        Me.GSHIPPEDTO.FieldName = "PACKING"
        Me.GSHIPPEDTO.Name = "GSHIPPEDTO"
        Me.GSHIPPEDTO.OptionsColumn.AllowEdit = False
        Me.GSHIPPEDTO.Visible = True
        Me.GSHIPPEDTO.VisibleIndex = 15
        Me.GSHIPPEDTO.Width = 100
        '
        'GEWAYBILLNO
        '
        Me.GEWAYBILLNO.Caption = "E-Way Bill No."
        Me.GEWAYBILLNO.FieldName = "EWAYBILLNO"
        Me.GEWAYBILLNO.Name = "GEWAYBILLNO"
        Me.GEWAYBILLNO.OptionsColumn.AllowEdit = False
        Me.GEWAYBILLNO.Visible = True
        Me.GEWAYBILLNO.VisibleIndex = 16
        '
        'GTRANSPORT
        '
        Me.GTRANSPORT.Caption = "Transport"
        Me.GTRANSPORT.FieldName = "TRANSNAME"
        Me.GTRANSPORT.Name = "GTRANSPORT"
        Me.GTRANSPORT.OptionsColumn.AllowEdit = False
        Me.GTRANSPORT.Visible = True
        Me.GTRANSPORT.VisibleIndex = 17
        Me.GTRANSPORT.Width = 150
        '
        'GLRNO
        '
        Me.GLRNO.Caption = "LR No"
        Me.GLRNO.FieldName = "LRNO"
        Me.GLRNO.Name = "GLRNO"
        Me.GLRNO.OptionsColumn.AllowEdit = False
        Me.GLRNO.Visible = True
        Me.GLRNO.VisibleIndex = 18
        '
        'GLRDATE
        '
        Me.GLRDATE.Caption = "LR Date"
        Me.GLRDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GLRDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GLRDATE.FieldName = "LRDATE"
        Me.GLRDATE.Name = "GLRDATE"
        Me.GLRDATE.OptionsColumn.AllowEdit = False
        Me.GLRDATE.Visible = True
        Me.GLRDATE.VisibleIndex = 19
        '
        'GSO
        '
        Me.GSO.Caption = "SO No."
        Me.GSO.FieldName = "SONO"
        Me.GSO.Name = "GSO"
        Me.GSO.OptionsColumn.AllowEdit = False
        Me.GSO.Visible = True
        Me.GSO.VisibleIndex = 20
        '
        'GBALES
        '
        Me.GBALES.Caption = "Bales"
        Me.GBALES.DisplayFormat.FormatString = "0"
        Me.GBALES.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GBALES.FieldName = "NOOFBALES"
        Me.GBALES.Name = "GBALES"
        Me.GBALES.OptionsColumn.AllowEdit = False
        Me.GBALES.Visible = True
        Me.GBALES.VisibleIndex = 21
        '
        'GPCS
        '
        Me.GPCS.Caption = "Total Pcs"
        Me.GPCS.DisplayFormat.FormatString = "0.00"
        Me.GPCS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPCS.FieldName = "TOTALPCS"
        Me.GPCS.Name = "GPCS"
        Me.GPCS.OptionsColumn.AllowEdit = False
        Me.GPCS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GPCS.Visible = True
        Me.GPCS.VisibleIndex = 22
        Me.GPCS.Width = 85
        '
        'GMTRS
        '
        Me.GMTRS.Caption = "Total Mtrs"
        Me.GMTRS.DisplayFormat.FormatString = "0.00"
        Me.GMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GMTRS.FieldName = "TOTALMTRS"
        Me.GMTRS.Name = "GMTRS"
        Me.GMTRS.OptionsColumn.AllowEdit = False
        Me.GMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GMTRS.Visible = True
        Me.GMTRS.VisibleIndex = 23
        Me.GMTRS.Width = 85
        '
        'GTOTALAMT
        '
        Me.GTOTALAMT.Caption = "Total Amt"
        Me.GTOTALAMT.DisplayFormat.FormatString = "0.00"
        Me.GTOTALAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALAMT.FieldName = "TOTALAMT"
        Me.GTOTALAMT.Name = "GTOTALAMT"
        Me.GTOTALAMT.OptionsColumn.AllowEdit = False
        Me.GTOTALAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALAMT.Visible = True
        Me.GTOTALAMT.VisibleIndex = 24
        '
        'GDISC
        '
        Me.GDISC.Caption = "Total Disc Amt."
        Me.GDISC.FieldName = "TOTALDISC"
        Me.GDISC.Name = "GDISC"
        Me.GDISC.OptionsColumn.AllowEdit = False
        Me.GDISC.Visible = True
        Me.GDISC.VisibleIndex = 26
        Me.GDISC.Width = 80
        '
        'GSPDISCAMT
        '
        Me.GSPDISCAMT.Caption = "Total Sp Disc Amt."
        Me.GSPDISCAMT.FieldName = "TOTALSPDISCAMT"
        Me.GSPDISCAMT.Name = "GSPDISCAMT"
        Me.GSPDISCAMT.OptionsColumn.AllowEdit = False
        Me.GSPDISCAMT.Visible = True
        Me.GSPDISCAMT.VisibleIndex = 25
        '
        'GCHARGES
        '
        Me.GCHARGES.Caption = "Charges"
        Me.GCHARGES.FieldName = "CHARGES"
        Me.GCHARGES.Name = "GCHARGES"
        Me.GCHARGES.OptionsColumn.AllowEdit = False
        Me.GCHARGES.Visible = True
        Me.GCHARGES.VisibleIndex = 27
        '
        'GTOTALTAXABLEAMT
        '
        Me.GTOTALTAXABLEAMT.Caption = "Total Taxable Amt."
        Me.GTOTALTAXABLEAMT.DisplayFormat.FormatString = "0.00"
        Me.GTOTALTAXABLEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALTAXABLEAMT.FieldName = "TOTALTAXABLEAMT"
        Me.GTOTALTAXABLEAMT.Name = "GTOTALTAXABLEAMT"
        Me.GTOTALTAXABLEAMT.OptionsColumn.AllowEdit = False
        Me.GTOTALTAXABLEAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALTAXABLEAMT.Visible = True
        Me.GTOTALTAXABLEAMT.VisibleIndex = 28
        '
        'GTOTALCGSTAMT
        '
        Me.GTOTALCGSTAMT.Caption = "Total CGST Amt."
        Me.GTOTALCGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.GTOTALCGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALCGSTAMT.FieldName = "TOTALCGSTAMT"
        Me.GTOTALCGSTAMT.Name = "GTOTALCGSTAMT"
        Me.GTOTALCGSTAMT.OptionsColumn.AllowEdit = False
        Me.GTOTALCGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALCGSTAMT.Visible = True
        Me.GTOTALCGSTAMT.VisibleIndex = 29
        '
        'GTOTALSGSTAMT
        '
        Me.GTOTALSGSTAMT.Caption = "Total SGST Amt."
        Me.GTOTALSGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.GTOTALSGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALSGSTAMT.FieldName = "TOTALSGSTAMT"
        Me.GTOTALSGSTAMT.Name = "GTOTALSGSTAMT"
        Me.GTOTALSGSTAMT.OptionsColumn.AllowEdit = False
        Me.GTOTALSGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALSGSTAMT.Visible = True
        Me.GTOTALSGSTAMT.VisibleIndex = 30
        '
        'GTOTALIGSTAMT
        '
        Me.GTOTALIGSTAMT.Caption = "Total IGST Amt."
        Me.GTOTALIGSTAMT.FieldName = "TOTALIGSTAMT"
        Me.GTOTALIGSTAMT.Name = "GTOTALIGSTAMT"
        Me.GTOTALIGSTAMT.OptionsColumn.AllowEdit = False
        Me.GTOTALIGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALIGSTAMT.Visible = True
        Me.GTOTALIGSTAMT.VisibleIndex = 31
        '
        'GTOTALWITHGST
        '
        Me.GTOTALWITHGST.Caption = "Total With GST"
        Me.GTOTALWITHGST.DisplayFormat.FormatString = "0.00"
        Me.GTOTALWITHGST.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALWITHGST.FieldName = "TOTALWITHGST"
        Me.GTOTALWITHGST.Name = "GTOTALWITHGST"
        Me.GTOTALWITHGST.OptionsColumn.AllowEdit = False
        Me.GTOTALWITHGST.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        '
        'GAPPLYTCS
        '
        Me.GAPPLYTCS.Caption = "TCS"
        Me.GAPPLYTCS.FieldName = "APPLYTCS"
        Me.GAPPLYTCS.Name = "GAPPLYTCS"
        Me.GAPPLYTCS.OptionsColumn.AllowEdit = False
        Me.GAPPLYTCS.Visible = True
        Me.GAPPLYTCS.VisibleIndex = 32
        '
        'GTCSPER
        '
        Me.GTCSPER.Caption = "TCS %"
        Me.GTCSPER.DisplayFormat.FormatString = "0.000"
        Me.GTCSPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTCSPER.FieldName = "TCSPER"
        Me.GTCSPER.Name = "GTCSPER"
        Me.GTCSPER.OptionsColumn.AllowEdit = False
        Me.GTCSPER.Visible = True
        Me.GTCSPER.VisibleIndex = 33
        '
        'GTCSAMT
        '
        Me.GTCSAMT.Caption = "TCS Amt"
        Me.GTCSAMT.DisplayFormat.FormatString = "0.00"
        Me.GTCSAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTCSAMT.FieldName = "TCSAMT"
        Me.GTCSAMT.Name = "GTCSAMT"
        Me.GTCSAMT.OptionsColumn.AllowEdit = False
        Me.GTCSAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTCSAMT.Visible = True
        Me.GTCSAMT.VisibleIndex = 34
        '
        'GGRANDTOTAL
        '
        Me.GGRANDTOTAL.Caption = "Grand Total"
        Me.GGRANDTOTAL.DisplayFormat.FormatString = "0.00"
        Me.GGRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.GGRANDTOTAL.Name = "GGRANDTOTAL"
        Me.GGRANDTOTAL.OptionsColumn.AllowEdit = False
        Me.GGRANDTOTAL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GGRANDTOTAL.Visible = True
        Me.GGRANDTOTAL.VisibleIndex = 35
        Me.GGRANDTOTAL.Width = 85
        '
        'GRECDAMT
        '
        Me.GRECDAMT.Caption = "Recd Amt"
        Me.GRECDAMT.DisplayFormat.FormatString = "0.00"
        Me.GRECDAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRECDAMT.FieldName = "RECDAMT"
        Me.GRECDAMT.Name = "GRECDAMT"
        Me.GRECDAMT.OptionsColumn.AllowEdit = False
        Me.GRECDAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GRECDAMT.Visible = True
        Me.GRECDAMT.VisibleIndex = 36
        '
        'GRETURNAMT
        '
        Me.GRETURNAMT.Caption = "Sale Ret Amt"
        Me.GRETURNAMT.DisplayFormat.FormatString = "0.00"
        Me.GRETURNAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRETURNAMT.FieldName = "RETURNAMT"
        Me.GRETURNAMT.Name = "GRETURNAMT"
        Me.GRETURNAMT.OptionsColumn.AllowEdit = False
        Me.GRETURNAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GRETURNAMT.Visible = True
        Me.GRETURNAMT.VisibleIndex = 37
        '
        'GBALANCE
        '
        Me.GBALANCE.Caption = "Bal Amt"
        Me.GBALANCE.DisplayFormat.FormatString = "0.00"
        Me.GBALANCE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GBALANCE.FieldName = "BALANCEAMT"
        Me.GBALANCE.Name = "GBALANCE"
        Me.GBALANCE.OptionsColumn.AllowEdit = False
        Me.GBALANCE.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GBALANCE.Visible = True
        Me.GBALANCE.VisibleIndex = 38
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "REMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.OptionsColumn.AllowEdit = False
        Me.GREMARKS.Visible = True
        Me.GREMARKS.VisibleIndex = 39
        Me.GREMARKS.Width = 150
        '
        'GDISPUTED
        '
        Me.GDISPUTED.Caption = "Disputed"
        Me.GDISPUTED.FieldName = "DISPUTED"
        Me.GDISPUTED.Name = "GDISPUTED"
        Me.GDISPUTED.OptionsColumn.AllowEdit = False
        Me.GDISPUTED.Visible = True
        Me.GDISPUTED.VisibleIndex = 40
        '
        'GBILLCHECKED
        '
        Me.GBILLCHECKED.Caption = "Checked"
        Me.GBILLCHECKED.FieldName = "CHECKED"
        Me.GBILLCHECKED.Name = "GBILLCHECKED"
        Me.GBILLCHECKED.OptionsColumn.AllowEdit = False
        Me.GBILLCHECKED.Visible = True
        Me.GBILLCHECKED.VisibleIndex = 41
        '
        'GSUPPLIERNAME
        '
        Me.GSUPPLIERNAME.Caption = "Seller Name"
        Me.GSUPPLIERNAME.FieldName = "PURNAME"
        Me.GSUPPLIERNAME.Name = "GSUPPLIERNAME"
        Me.GSUPPLIERNAME.OptionsColumn.AllowEdit = False
        Me.GSUPPLIERNAME.Visible = True
        Me.GSUPPLIERNAME.VisibleIndex = 7
        Me.GSUPPLIERNAME.Width = 230
        '
        'GRECDDATE
        '
        Me.GRECDDATE.Caption = "Recd Date"
        Me.GRECDDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GRECDDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GRECDDATE.FieldName = "RECDATE"
        Me.GRECDDATE.Name = "GRECDDATE"
        Me.GRECDDATE.OptionsColumn.AllowEdit = False
        '
        'GIRNNO
        '
        Me.GIRNNO.Caption = "IRN No"
        Me.GIRNNO.FieldName = "IRNNO"
        Me.GIRNNO.Name = "GIRNNO"
        Me.GIRNNO.OptionsColumn.AllowEdit = False
        Me.GIRNNO.Visible = True
        Me.GIRNNO.VisibleIndex = 42
        Me.GIRNNO.Width = 120
        '
        'GACKNO
        '
        Me.GACKNO.Caption = "Ack No"
        Me.GACKNO.FieldName = "ACKNO"
        Me.GACKNO.Name = "GACKNO"
        Me.GACKNO.OptionsColumn.AllowEdit = False
        Me.GACKNO.Visible = True
        Me.GACKNO.VisibleIndex = 45
        '
        'GACKDATE
        '
        Me.GACKDATE.Caption = "Ack date"
        Me.GACKDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GACKDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GACKDATE.FieldName = "ACKDATE"
        Me.GACKDATE.Name = "GACKDATE"
        Me.GACKDATE.OptionsColumn.AllowEdit = False
        Me.GACKDATE.Visible = True
        Me.GACKDATE.VisibleIndex = 46
        '
        'GDOCKETNO
        '
        Me.GDOCKETNO.Caption = "Docket No"
        Me.GDOCKETNO.FieldName = "DOCKETNO"
        Me.GDOCKETNO.Name = "GDOCKETNO"
        Me.GDOCKETNO.OptionsColumn.AllowEdit = False
        Me.GDOCKETNO.Visible = True
        Me.GDOCKETNO.VisibleIndex = 43
        '
        'GDOCKETDATE
        '
        Me.GDOCKETDATE.Caption = "Docket Date"
        Me.GDOCKETDATE.FieldName = "DOCKETDATE"
        Me.GDOCKETDATE.Name = "GDOCKETDATE"
        Me.GDOCKETDATE.OptionsColumn.AllowEdit = False
        Me.GDOCKETDATE.Visible = True
        Me.GDOCKETDATE.VisibleIndex = 44
        '
        'GCOURIER
        '
        Me.GCOURIER.Caption = "Courier"
        Me.GCOURIER.FieldName = "COURIER"
        Me.GCOURIER.Name = "GCOURIER"
        Me.GCOURIER.OptionsColumn.AllowEdit = False
        Me.GCOURIER.Visible = True
        Me.GCOURIER.VisibleIndex = 47
        '
        'GCHGSDTLS
        '
        Me.GCHGSDTLS.Caption = "Charges Details"
        Me.GCHGSDTLS.FieldName = "CHGSDTLS"
        Me.GCHGSDTLS.Name = "GCHGSDTLS"
        Me.GCHGSDTLS.OptionsColumn.AllowEdit = False
        Me.GCHGSDTLS.Visible = True
        Me.GCHGSDTLS.VisibleIndex = 48
        Me.GCHGSDTLS.Width = 200
        '
        'GFROMCITY
        '
        Me.GFROMCITY.Caption = "From City"
        Me.GFROMCITY.FieldName = "FROMCITY"
        Me.GFROMCITY.Name = "GFROMCITY"
        Me.GFROMCITY.OptionsColumn.AllowEdit = False
        Me.GFROMCITY.Visible = True
        Me.GFROMCITY.VisibleIndex = 49
        '
        'GTOCITY
        '
        Me.GTOCITY.Caption = "To City"
        Me.GTOCITY.FieldName = "TOCITY"
        Me.GTOCITY.Name = "GTOCITY"
        Me.GTOCITY.OptionsColumn.AllowEdit = False
        Me.GTOCITY.Visible = True
        Me.GTOCITY.VisibleIndex = 50
        '
        'GUSERNAME
        '
        Me.GUSERNAME.Caption = "User Name"
        Me.GUSERNAME.FieldName = "USERNAME"
        Me.GUSERNAME.Name = "GUSERNAME"
        Me.GUSERNAME.OptionsColumn.AllowEdit = False
        Me.GUSERNAME.Visible = True
        Me.GUSERNAME.VisibleIndex = 51
        '
        'GCREATEDDATE
        '
        Me.GCREATEDDATE.Caption = "Created Dt"
        Me.GCREATEDDATE.DisplayFormat.FormatString = "dd/MM/yyyy  hh:mm:ss"
        Me.GCREATEDDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GCREATEDDATE.FieldName = "CREATEDDATE"
        Me.GCREATEDDATE.Name = "GCREATEDDATE"
        Me.GCREATEDDATE.OptionsColumn.AllowEdit = False
        Me.GCREATEDDATE.Visible = True
        Me.GCREATEDDATE.VisibleIndex = 52
        '
        'GPONO
        '
        Me.GPONO.Caption = "Party Bill No"
        Me.GPONO.FieldName = "PARTYPONO"
        Me.GPONO.Name = "GPONO"
        Me.GPONO.OptionsColumn.AllowEdit = False
        Me.GPONO.Visible = True
        Me.GPONO.VisibleIndex = 8
        '
        'GPARTYMAIL
        '
        Me.GPARTYMAIL.Caption = "Party Mail"
        Me.GPARTYMAIL.FieldName = "PARTYMAIL"
        Me.GPARTYMAIL.Name = "GPARTYMAIL"
        Me.GPARTYMAIL.OptionsColumn.AllowEdit = False
        Me.GPARTYMAIL.Visible = True
        Me.GPARTYMAIL.VisibleIndex = 53
        '
        'GPARTYWHATSAPP
        '
        Me.GPARTYWHATSAPP.Caption = "PARTYWHATSAPP"
        Me.GPARTYWHATSAPP.FieldName = "PARTYWHATSAPP"
        Me.GPARTYWHATSAPP.Name = "GPARTYWHATSAPP"
        Me.GPARTYWHATSAPP.OptionsColumn.AllowEdit = False
        Me.GPARTYWHATSAPP.Visible = True
        Me.GPARTYWHATSAPP.VisibleIndex = 54
        '
        'GAGENTMAIL
        '
        Me.GAGENTMAIL.Caption = "Agent Mail"
        Me.GAGENTMAIL.FieldName = "AGENTMAIL"
        Me.GAGENTMAIL.Name = "GAGENTMAIL"
        Me.GAGENTMAIL.OptionsColumn.AllowEdit = False
        Me.GAGENTMAIL.Visible = True
        Me.GAGENTMAIL.VisibleIndex = 55
        '
        'GAGENTWHATSAPP
        '
        Me.GAGENTWHATSAPP.Caption = "Agent WhatsApp"
        Me.GAGENTWHATSAPP.FieldName = "AGENTWHATSAPP"
        Me.GAGENTWHATSAPP.Name = "GAGENTWHATSAPP"
        Me.GAGENTWHATSAPP.OptionsColumn.AllowEdit = False
        Me.GAGENTWHATSAPP.Visible = True
        Me.GAGENTWHATSAPP.VisibleIndex = 56
        '
        'GPRINTINITIALS
        '
        Me.GPRINTINITIALS.Caption = "Print Initials"
        Me.GPRINTINITIALS.FieldName = "PRINTINITIALS"
        Me.GPRINTINITIALS.Name = "GPRINTINITIALS"
        Me.GPRINTINITIALS.OptionsColumn.AllowEdit = False
        '
        'GINITIALS
        '
        Me.GINITIALS.Caption = "INITIALS"
        Me.GINITIALS.FieldName = "INITIALS"
        Me.GINITIALS.Name = "GINITIALS"
        Me.GINITIALS.OptionsColumn.AllowEdit = False
        '
        'GBROKERAGEPER
        '
        Me.GBROKERAGEPER.Caption = "Brok %"
        Me.GBROKERAGEPER.DisplayFormat.FormatString = "0.00"
        Me.GBROKERAGEPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GBROKERAGEPER.FieldName = "BROKERAGEPER"
        Me.GBROKERAGEPER.Name = "GBROKERAGEPER"
        Me.GBROKERAGEPER.OptionsColumn.AllowEdit = False
        Me.GBROKERAGEPER.Visible = True
        Me.GBROKERAGEPER.VisibleIndex = 57
        '
        'GBROKERAGE
        '
        Me.GBROKERAGE.Caption = "Brokerage"
        Me.GBROKERAGE.DisplayFormat.FormatString = "0.00"
        Me.GBROKERAGE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GBROKERAGE.FieldName = "BROKERAGE"
        Me.GBROKERAGE.Name = "GBROKERAGE"
        Me.GBROKERAGE.OptionsColumn.AllowEdit = False
        Me.GBROKERAGE.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GBROKERAGE.Visible = True
        Me.GBROKERAGE.VisibleIndex = 58
        '
        'GDISCPER
        '
        Me.GDISCPER.Caption = "Disc %"
        Me.GDISCPER.DisplayFormat.FormatString = "0.00"
        Me.GDISCPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GDISCPER.FieldName = "DISCOUNTPER"
        Me.GDISCPER.Name = "GDISCPER"
        Me.GDISCPER.OptionsColumn.AllowEdit = False
        Me.GDISCPER.Visible = True
        Me.GDISCPER.VisibleIndex = 59
        '
        'GDISCOUNT
        '
        Me.GDISCOUNT.Caption = "Disc"
        Me.GDISCOUNT.DisplayFormat.FormatString = "0.00"
        Me.GDISCOUNT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GDISCOUNT.FieldName = "DISCOUNT"
        Me.GDISCOUNT.Name = "GDISCOUNT"
        Me.GDISCOUNT.OptionsColumn.AllowEdit = False
        Me.GDISCOUNT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GDISCOUNT.Visible = True
        Me.GDISCOUNT.VisibleIndex = 60
        '
        'GCDPER
        '
        Me.GCDPER.Caption = "C.D. %"
        Me.GCDPER.DisplayFormat.FormatString = "0.00"
        Me.GCDPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCDPER.FieldName = "CDPER"
        Me.GCDPER.Name = "GCDPER"
        Me.GCDPER.OptionsColumn.AllowEdit = False
        Me.GCDPER.Visible = True
        Me.GCDPER.VisibleIndex = 61
        '
        'GCASHDISC
        '
        Me.GCASHDISC.Caption = "Cash Disc"
        Me.GCASHDISC.DisplayFormat.FormatString = "0.00"
        Me.GCASHDISC.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCASHDISC.FieldName = "CASHDISC"
        Me.GCASHDISC.Name = "GCASHDISC"
        Me.GCASHDISC.OptionsColumn.AllowEdit = False
        Me.GCASHDISC.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GCASHDISC.Visible = True
        Me.GCASHDISC.VisibleIndex = 62
        '
        'GSPECIALREMARKS
        '
        Me.GSPECIALREMARKS.Caption = "Special Remark"
        Me.GSPECIALREMARKS.FieldName = "SPECIALREMARK"
        Me.GSPECIALREMARKS.Name = "GSPECIALREMARKS"
        Me.GSPECIALREMARKS.OptionsColumn.AllowEdit = False
        Me.GSPECIALREMARKS.Visible = True
        Me.GSPECIALREMARKS.VisibleIndex = 63
        '
        'GCOSTCENTERNAME
        '
        Me.GCOSTCENTERNAME.Caption = "Cost Center Name"
        Me.GCOSTCENTERNAME.FieldName = "COSTCENTERNAME"
        Me.GCOSTCENTERNAME.Name = "GCOSTCENTERNAME"
        Me.GCOSTCENTERNAME.OptionsColumn.AllowEdit = False
        Me.GCOSTCENTERNAME.Visible = True
        Me.GCOSTCENTERNAME.VisibleIndex = 64
        '
        'GCREATEDBY
        '
        Me.GCREATEDBY.Caption = "Created By"
        Me.GCREATEDBY.FieldName = "CREATEDBY"
        Me.GCREATEDBY.Name = "GCREATEDBY"
        Me.GCREATEDBY.OptionsColumn.AllowEdit = False
        Me.GCREATEDBY.Visible = True
        Me.GCREATEDBY.VisibleIndex = 65
        '
        'GREFERREDBY
        '
        Me.GREFERREDBY.Caption = "Referred by"
        Me.GREFERREDBY.FieldName = "REFERREDBY"
        Me.GREFERREDBY.Name = "GREFERREDBY"
        Me.GREFERREDBY.OptionsColumn.AllowEdit = False
        Me.GREFERREDBY.Visible = True
        Me.GREFERREDBY.VisibleIndex = 66
        '
        'GTRADINGACC
        '
        Me.GTRADINGACC.Caption = "Trading Acc"
        Me.GTRADINGACC.FieldName = "TRADINGACC"
        Me.GTRADINGACC.Name = "GTRADINGACC"
        Me.GTRADINGACC.OptionsColumn.AllowEdit = False
        Me.GTRADINGACC.Visible = True
        Me.GTRADINGACC.VisibleIndex = 67
        '
        'GMONTHNAME
        '
        Me.GMONTHNAME.Caption = "Month Name"
        Me.GMONTHNAME.FieldName = "MONTHNAME"
        Me.GMONTHNAME.Name = "GMONTHNAME"
        Me.GMONTHNAME.OptionsColumn.AllowEdit = False
        Me.GMONTHNAME.Visible = True
        Me.GMONTHNAME.VisibleIndex = 68
        Me.GMONTHNAME.Width = 100
        '
        'GCOMM
        '
        Me.GCOMM.Caption = "Comm %"
        Me.GCOMM.FieldName = "COMM"
        Me.GCOMM.Name = "GCOMM"
        Me.GCOMM.OptionsColumn.AllowEdit = False
        Me.GCOMM.Visible = True
        Me.GCOMM.VisibleIndex = 69
        '
        'GCOMMTYPE
        '
        Me.GCOMMTYPE.Caption = "Comm Type"
        Me.GCOMMTYPE.FieldName = "COMMTPYE"
        Me.GCOMMTYPE.Name = "GCOMMTYPE"
        Me.GCOMMTYPE.OptionsColumn.AllowEdit = False
        Me.GCOMMTYPE.Visible = True
        Me.GCOMMTYPE.VisibleIndex = 70
        '
        'CHKEDIT
        '
        Me.CHKEDIT.AutoHeight = False
        Me.CHKEDIT.Name = "CHKEDIT"
        Me.CHKEDIT.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
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
        Me.cmdcancel.Location = New System.Drawing.Point(619, 540)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 3
        Me.cmdcancel.Text = "E&xit"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl.Location = New System.Drawing.Point(20, 44)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(156, 14)
        Me.lbl.TabIndex = 251
        Me.lbl.Text = "Select an Invoice to Change"
        '
        'PRINTDIALOG
        '
        Me.PRINTDIALOG.AllowSelection = True
        Me.PRINTDIALOG.AllowSomePages = True
        Me.PRINTDIALOG.ShowHelp = True
        Me.PRINTDIALOG.UseEXDialog = True
        '
        'AgencyInvoiceDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "AgencyInvoiceDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Agency Invoice Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CHKBLANKPAPER As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents CHKMULTI As CheckBox
    Friend WithEvents TXTCOPIES As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents TXTTO As TextBox
    Friend WithEvents TXTFROM As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label22 As Label
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gsrno As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gdate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREFNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHALLANNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHALLANDATE As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gname As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGSTIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSTATENAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSTATECODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GADD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSHIPPEDTO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEWAYBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTRANSPORT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLRDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBALES As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDISC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSPDISCAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHARGES As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALTAXABLEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALCGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALSGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALIGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALWITHGST As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAPPLYTCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTCSPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTCSAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRECDAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRETURNAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBALANCE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDISPUTED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBILLCHECKED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSUPPLIERNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRECDDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GIRNNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GACKNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GACKDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDOCKETNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDOCKETDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOURIER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHGSDTLS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFROMCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GUSERNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCREATEDDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPONO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPARTYMAIL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPARTYWHATSAPP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENTMAIL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENTWHATSAPP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPRINTINITIALS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINITIALS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBROKERAGEPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBROKERAGE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDISCPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDISCOUNT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCDPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCASHDISC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSPECIALREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOSTCENTERNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCREATEDBY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREFERREDBY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTRADINGACC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMONTHNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKEDIT As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CMDOK As Button
    Friend WithEvents cmdcancel As Button
    Friend WithEvents lbl As Label
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents TOOLMAIL As ToolStripButton
    Friend WithEvents TOOLWHATSAPP As ToolStripButton
    Friend WithEvents TOOLCMBINVCOPY As ToolStripComboBox
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TOOLGRIDDETAILS As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents PRINTDOC As Drawing.Printing.PrintDocument
    Friend WithEvents PRINTDIALOG As PrintDialog
    Friend WithEvents GCOMM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOMMTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TOOLREFRESH As ToolStripButton
End Class
