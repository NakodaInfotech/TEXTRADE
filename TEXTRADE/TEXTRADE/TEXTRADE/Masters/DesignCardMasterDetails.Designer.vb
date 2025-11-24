<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DesignCardMasterDetails
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
        Me.miniToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ExcelExport = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDEDIT = New System.Windows.Forms.Button()
        Me.CMDADDNEW = New System.Windows.Forms.Button()
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.BlendPanel2 = New VbPowerPack.BlendPanel()
        Me.GRIDBILLDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDBILL = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GITEMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDESIGNNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCARDNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREEDSPACE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDENTS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARPTTL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWEFTTTL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPICKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMAINRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALDENTSMAIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGSM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTHREADPERDENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALDENTSSEL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWEAVE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFEPI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFINISHWIDTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALDENTS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSHAFTS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALDESIGNWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFINISHPPI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALFINISHWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELVEDGEL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELDENTSL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPHOTOGRAPH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELVEDGER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELDENTSR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELENDSL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELTENDSL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELENDSR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELTENDSR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELTENDSLR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSORTREFNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPARTYNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GORDERNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GORDERDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENTNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGREYFIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDELIVERYAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDELDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPIECEMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNOOFPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLOOM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBEAMMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLOOMPROD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRPM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOVERFACTOR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEFFICIENCY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGREYDELIVERYAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGREYDELDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEPD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEPI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELSIZE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELEPDENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSELTENDS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALENDS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGWIDTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFWIDTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRIEGEWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBLEND = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFINISHMETHOD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GQUALITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GQUALITYTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARPWES = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWASTAGE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSHRINKAGE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWPP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWEAVINGCOSTMTR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRIEGEFABCOST = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFINISHFABCOST = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPRODDAY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPIECEL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGLM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALMAINENDS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALDENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALDENTSREPEAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BlendPanel2.SuspendLayout()
        CType(Me.GRIDBILLDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDBILL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'miniToolStrip
        '
        Me.miniToolStrip.AccessibleName = "New item selection"
        Me.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.Location = New System.Drawing.Point(103, 3)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(706, 25)
        Me.miniToolStrip.TabIndex = 318
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
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(439, 541)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 322
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'CMDEDIT
        '
        Me.CMDEDIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEDIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEDIT.FlatAppearance.BorderSize = 0
        Me.CMDEDIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEDIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEDIT.Location = New System.Drawing.Point(353, 541)
        Me.CMDEDIT.Name = "CMDEDIT"
        Me.CMDEDIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEDIT.TabIndex = 323
        Me.CMDEDIT.Text = "&Edit"
        Me.CMDEDIT.UseVisualStyleBackColor = False
        '
        'CMDADDNEW
        '
        Me.CMDADDNEW.BackColor = System.Drawing.Color.Transparent
        Me.CMDADDNEW.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDADDNEW.FlatAppearance.BorderSize = 0
        Me.CMDADDNEW.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDADDNEW.ForeColor = System.Drawing.Color.Black
        Me.CMDADDNEW.Location = New System.Drawing.Point(181, 541)
        Me.CMDADDNEW.Name = "CMDADDNEW"
        Me.CMDADDNEW.Size = New System.Drawing.Size(80, 28)
        Me.CMDADDNEW.TabIndex = 324
        Me.CMDADDNEW.Text = "&Add New"
        Me.CMDADDNEW.UseVisualStyleBackColor = False
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.Black
        Me.CMDREFRESH.Location = New System.Drawing.Point(267, 541)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREFRESH.TabIndex = 325
        Me.CMDREFRESH.Text = "&Refresh"
        Me.CMDREFRESH.UseVisualStyleBackColor = False
        '
        'BlendPanel2
        '
        Me.BlendPanel2.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel2.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel2.Controls.Add(Me.CMDADDNEW)
        Me.BlendPanel2.Controls.Add(Me.CMDEDIT)
        Me.BlendPanel2.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel2.Controls.Add(Me.GRIDBILLDETAILS)
        Me.BlendPanel2.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel2.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel2.Name = "BlendPanel2"
        Me.BlendPanel2.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel2.TabIndex = 7
        '
        'GRIDBILLDETAILS
        '
        Me.GRIDBILLDETAILS.Location = New System.Drawing.Point(14, 33)
        Me.GRIDBILLDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDBILLDETAILS.MainView = Me.GRIDBILL
        Me.GRIDBILLDETAILS.Name = "GRIDBILLDETAILS"
        Me.GRIDBILLDETAILS.Size = New System.Drawing.Size(1195, 502)
        Me.GRIDBILLDETAILS.TabIndex = 315
        Me.GRIDBILLDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDBILL})
        '
        'GRIDBILL
        '
        Me.GRIDBILL.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILL.Appearance.Row.Options.UseFont = True
        Me.GRIDBILL.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GITEMNAME, Me.GDESIGNNO, Me.GCARDNO, Me.GDATE, Me.GREED, Me.GREEDSPACE, Me.GDENTS, Me.GWARPTTL, Me.GWEFTTTL, Me.GPICKS, Me.GMAINRS, Me.GTOTALDENTSMAIN, Me.GGSM, Me.GTHREADPERDENT, Me.GTOTALDENTSSEL, Me.GWEAVE, Me.GFEPI, Me.GFINISHWIDTH, Me.GTOTALDENTS, Me.GSHAFTS, Me.GTOTALDESIGNWT, Me.GFINISHPPI, Me.GTOTALFINISHWT, Me.GSELVEDGEL, Me.GSELDENTSL, Me.GPHOTOGRAPH, Me.GSELVEDGER, Me.GSELDENTSR, Me.GSELENDSL, Me.GSELTENDSL, Me.GSELENDSR, Me.GSELTENDSR, Me.GSELTENDSLR, Me.GSORTREFNO, Me.GPARTYNAME, Me.GORDERNO, Me.GORDERDATE, Me.GAGENTNAME, Me.GGREYFIN, Me.GDELIVERYAT, Me.GDELDATE, Me.GPIECEMTRS, Me.GNOOFPCS, Me.GLOOM, Me.GBEAMMTRS, Me.GLOOMPROD, Me.GRPM, Me.GCOVERFACTOR, Me.GEFFICIENCY, Me.GGREYDELIVERYAT, Me.GGREYDELDATE, Me.GEPD, Me.GEPI, Me.GSELSIZE, Me.GSELEPDENT, Me.GSELTENDS, Me.GTOTALENDS, Me.GGWIDTH, Me.GFWIDTH, Me.GGRIEGEWT, Me.GBLEND, Me.GFINISHMETHOD, Me.GQUALITY, Me.GQUALITYTYPE, Me.GWARPWES, Me.GWASTAGE, Me.GSHRINKAGE, Me.GWPP, Me.GWEAVINGCOSTMTR, Me.GGRIEGEFABCOST, Me.GFINISHFABCOST, Me.GPRODDAY, Me.GPIECEL, Me.GGLM, Me.GTOTALMAINENDS, Me.GTOTALDENT, Me.GTOTALDENTSREPEAT})
        Me.GRIDBILL.GridControl = Me.GRIDBILLDETAILS
        Me.GRIDBILL.Name = "GRIDBILL"
        Me.GRIDBILL.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDBILL.OptionsBehavior.Editable = False
        Me.GRIDBILL.OptionsView.ColumnAutoWidth = False
        Me.GRIDBILL.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDBILL.OptionsView.ShowAutoFilterRow = True
        Me.GRIDBILL.OptionsView.ShowGroupPanel = False
        '
        'GITEMNAME
        '
        Me.GITEMNAME.Caption = "Quality Name"
        Me.GITEMNAME.FieldName = "ITEMNAME"
        Me.GITEMNAME.Name = "GITEMNAME"
        Me.GITEMNAME.OptionsColumn.AllowEdit = False
        Me.GITEMNAME.Visible = True
        Me.GITEMNAME.VisibleIndex = 0
        Me.GITEMNAME.Width = 150
        '
        'GDESIGNNO
        '
        Me.GDESIGNNO.Caption = "Design No"
        Me.GDESIGNNO.FieldName = "DESIGNNO"
        Me.GDESIGNNO.Name = "GDESIGNNO"
        Me.GDESIGNNO.OptionsColumn.AllowEdit = False
        Me.GDESIGNNO.Visible = True
        Me.GDESIGNNO.VisibleIndex = 1
        Me.GDESIGNNO.Width = 150
        '
        'GCARDNO
        '
        Me.GCARDNO.Caption = "Card No"
        Me.GCARDNO.FieldName = "CARDNO"
        Me.GCARDNO.Name = "GCARDNO"
        Me.GCARDNO.OptionsColumn.AllowEdit = False
        Me.GCARDNO.Visible = True
        Me.GCARDNO.VisibleIndex = 2
        Me.GCARDNO.Width = 80
        '
        'GDATE
        '
        Me.GDATE.Caption = "Date"
        Me.GDATE.FieldName = "DATE"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.OptionsColumn.AllowEdit = False
        Me.GDATE.Visible = True
        Me.GDATE.VisibleIndex = 3
        Me.GDATE.Width = 80
        '
        'GREED
        '
        Me.GREED.Caption = "Reed"
        Me.GREED.FieldName = "REED"
        Me.GREED.Name = "GREED"
        Me.GREED.OptionsColumn.AllowEdit = False
        Me.GREED.Visible = True
        Me.GREED.VisibleIndex = 4
        '
        'GREEDSPACE
        '
        Me.GREEDSPACE.Caption = "Reed Space"
        Me.GREEDSPACE.FieldName = "REEDSPACE"
        Me.GREEDSPACE.Name = "GREEDSPACE"
        Me.GREEDSPACE.OptionsColumn.AllowEdit = False
        Me.GREEDSPACE.Visible = True
        Me.GREEDSPACE.VisibleIndex = 5
        '
        'GDENTS
        '
        Me.GDENTS.Caption = "Dents / In"
        Me.GDENTS.FieldName = "DENTS"
        Me.GDENTS.Name = "GDENTS"
        Me.GDENTS.OptionsColumn.AllowEdit = False
        Me.GDENTS.Visible = True
        Me.GDENTS.VisibleIndex = 6
        '
        'GWARPTTL
        '
        Me.GWARPTTL.Caption = "Warp TL"
        Me.GWARPTTL.FieldName = "WARPTTL"
        Me.GWARPTTL.Name = "GWARPTTL"
        Me.GWARPTTL.OptionsColumn.AllowEdit = False
        Me.GWARPTTL.Visible = True
        Me.GWARPTTL.VisibleIndex = 7
        '
        'GWEFTTTL
        '
        Me.GWEFTTTL.Caption = "Weft TL"
        Me.GWEFTTTL.FieldName = "WEFTTTL"
        Me.GWEFTTTL.Name = "GWEFTTTL"
        Me.GWEFTTTL.OptionsColumn.AllowEdit = False
        Me.GWEFTTTL.Visible = True
        Me.GWEFTTTL.VisibleIndex = 8
        '
        'GPICKS
        '
        Me.GPICKS.Caption = "Picks(On Loom)"
        Me.GPICKS.FieldName = "PICKS"
        Me.GPICKS.Name = "GPICKS"
        Me.GPICKS.OptionsColumn.AllowEdit = False
        Me.GPICKS.Visible = True
        Me.GPICKS.VisibleIndex = 9
        '
        'GMAINRS
        '
        Me.GMAINRS.Caption = "Main R.S."
        Me.GMAINRS.FieldName = "MAINRS"
        Me.GMAINRS.Name = "GMAINRS"
        Me.GMAINRS.OptionsColumn.AllowEdit = False
        Me.GMAINRS.Visible = True
        Me.GMAINRS.VisibleIndex = 10
        '
        'GTOTALDENTSMAIN
        '
        Me.GTOTALDENTSMAIN.Caption = "Total Dents (Main)"
        Me.GTOTALDENTSMAIN.FieldName = "TOTALDENTSMAIN"
        Me.GTOTALDENTSMAIN.Name = "GTOTALDENTSMAIN"
        Me.GTOTALDENTSMAIN.OptionsColumn.AllowEdit = False
        Me.GTOTALDENTSMAIN.Visible = True
        Me.GTOTALDENTSMAIN.VisibleIndex = 11
        Me.GTOTALDENTSMAIN.Width = 110
        '
        'GGSM
        '
        Me.GGSM.Caption = "GSM"
        Me.GGSM.FieldName = "GSM"
        Me.GGSM.Name = "GGSM"
        Me.GGSM.OptionsColumn.AllowEdit = False
        Me.GGSM.Visible = True
        Me.GGSM.VisibleIndex = 12
        '
        'GTHREADPERDENT
        '
        Me.GTHREADPERDENT.Caption = "Thread Per Dent"
        Me.GTHREADPERDENT.FieldName = "THREADPERDENT"
        Me.GTHREADPERDENT.Name = "GTHREADPERDENT"
        Me.GTHREADPERDENT.OptionsColumn.AllowEdit = False
        Me.GTHREADPERDENT.Visible = True
        Me.GTHREADPERDENT.VisibleIndex = 13
        '
        'GTOTALDENTSSEL
        '
        Me.GTOTALDENTSSEL.Caption = "Total Dents (Sel)"
        Me.GTOTALDENTSSEL.FieldName = "TOTALDENTSSEL"
        Me.GTOTALDENTSSEL.Name = "GTOTALDENTSSEL"
        Me.GTOTALDENTSSEL.OptionsColumn.AllowEdit = False
        Me.GTOTALDENTSSEL.Visible = True
        Me.GTOTALDENTSSEL.VisibleIndex = 14
        '
        'GWEAVE
        '
        Me.GWEAVE.Caption = "Weave"
        Me.GWEAVE.FieldName = "WEAVE"
        Me.GWEAVE.Name = "GWEAVE"
        Me.GWEAVE.OptionsColumn.AllowEdit = False
        Me.GWEAVE.Visible = True
        Me.GWEAVE.VisibleIndex = 15
        '
        'GFEPI
        '
        Me.GFEPI.Caption = "Finish EPI"
        Me.GFEPI.FieldName = "FEPI"
        Me.GFEPI.Name = "GFEPI"
        Me.GFEPI.OptionsColumn.AllowEdit = False
        Me.GFEPI.Visible = True
        Me.GFEPI.VisibleIndex = 16
        '
        'GFINISHWIDTH
        '
        Me.GFINISHWIDTH.Caption = "Finish Width"
        Me.GFINISHWIDTH.FieldName = "FINISHWIDTH"
        Me.GFINISHWIDTH.Name = "GFINISHWIDTH"
        Me.GFINISHWIDTH.OptionsColumn.AllowEdit = False
        Me.GFINISHWIDTH.Visible = True
        Me.GFINISHWIDTH.VisibleIndex = 17
        '
        'GTOTALDENTS
        '
        Me.GTOTALDENTS.Caption = "Total Dents (Body)"
        Me.GTOTALDENTS.FieldName = "TOTALDENTS"
        Me.GTOTALDENTS.Name = "GTOTALDENTS"
        Me.GTOTALDENTS.OptionsColumn.AllowEdit = False
        Me.GTOTALDENTS.Visible = True
        Me.GTOTALDENTS.VisibleIndex = 18
        '
        'GSHAFTS
        '
        Me.GSHAFTS.Caption = "Shafts"
        Me.GSHAFTS.FieldName = "SHAFTS"
        Me.GSHAFTS.Name = "GSHAFTS"
        Me.GSHAFTS.OptionsColumn.AllowEdit = False
        Me.GSHAFTS.Visible = True
        Me.GSHAFTS.VisibleIndex = 19
        '
        'GTOTALDESIGNWT
        '
        Me.GTOTALDESIGNWT.Caption = "Total Design Wt"
        Me.GTOTALDESIGNWT.FieldName = "TOTALDESIGNWT"
        Me.GTOTALDESIGNWT.Name = "GTOTALDESIGNWT"
        Me.GTOTALDESIGNWT.OptionsColumn.AllowEdit = False
        Me.GTOTALDESIGNWT.Visible = True
        Me.GTOTALDESIGNWT.VisibleIndex = 20
        '
        'GFINISHPPI
        '
        Me.GFINISHPPI.Caption = "Finish PPI"
        Me.GFINISHPPI.FieldName = "FINISHPPI"
        Me.GFINISHPPI.Name = "GFINISHPPI"
        Me.GFINISHPPI.OptionsColumn.AllowEdit = False
        Me.GFINISHPPI.Visible = True
        Me.GFINISHPPI.VisibleIndex = 21
        '
        'GTOTALFINISHWT
        '
        Me.GTOTALFINISHWT.Caption = "Total Finish Wt"
        Me.GTOTALFINISHWT.FieldName = "TOTALFINISHWT"
        Me.GTOTALFINISHWT.Name = "GTOTALFINISHWT"
        Me.GTOTALFINISHWT.OptionsColumn.AllowEdit = False
        Me.GTOTALFINISHWT.Visible = True
        Me.GTOTALFINISHWT.VisibleIndex = 22
        '
        'GSELVEDGEL
        '
        Me.GSELVEDGEL.Caption = "Selvedge (L)"
        Me.GSELVEDGEL.FieldName = "SELVEDGEL"
        Me.GSELVEDGEL.Name = "GSELVEDGEL"
        Me.GSELVEDGEL.OptionsColumn.AllowEdit = False
        Me.GSELVEDGEL.Visible = True
        Me.GSELVEDGEL.VisibleIndex = 23
        '
        'GSELDENTSL
        '
        Me.GSELDENTSL.Caption = "Sel Dents (L)"
        Me.GSELDENTSL.FieldName = "SELDENTSL"
        Me.GSELDENTSL.Name = "GSELDENTSL"
        Me.GSELDENTSL.OptionsColumn.AllowEdit = False
        Me.GSELDENTSL.Visible = True
        Me.GSELDENTSL.VisibleIndex = 24
        '
        'GPHOTOGRAPH
        '
        Me.GPHOTOGRAPH.Caption = "Photograph"
        Me.GPHOTOGRAPH.FieldName = "PHOTOGRAPH"
        Me.GPHOTOGRAPH.Name = "GPHOTOGRAPH"
        Me.GPHOTOGRAPH.OptionsColumn.AllowEdit = False
        Me.GPHOTOGRAPH.Visible = True
        Me.GPHOTOGRAPH.VisibleIndex = 25
        '
        'GSELVEDGER
        '
        Me.GSELVEDGER.Caption = "Selvedge (R)"
        Me.GSELVEDGER.FieldName = "SELVEDGER"
        Me.GSELVEDGER.Name = "GSELVEDGER"
        Me.GSELVEDGER.OptionsColumn.AllowEdit = False
        Me.GSELVEDGER.Visible = True
        Me.GSELVEDGER.VisibleIndex = 26
        '
        'GSELDENTSR
        '
        Me.GSELDENTSR.Caption = "Sel Dents (R)"
        Me.GSELDENTSR.FieldName = "SELDENTSR"
        Me.GSELDENTSR.Name = "GSELDENTSR"
        Me.GSELDENTSR.OptionsColumn.AllowEdit = False
        Me.GSELDENTSR.Visible = True
        Me.GSELDENTSR.VisibleIndex = 27
        '
        'GSELENDSL
        '
        Me.GSELENDSL.Caption = "Sel Ends (L)"
        Me.GSELENDSL.FieldName = "SELENDSL"
        Me.GSELENDSL.Name = "GSELENDSL"
        Me.GSELENDSL.OptionsColumn.AllowEdit = False
        Me.GSELENDSL.Visible = True
        Me.GSELENDSL.VisibleIndex = 28
        '
        'GSELTENDSL
        '
        Me.GSELTENDSL.Caption = "Sel T. Ends (L)"
        Me.GSELTENDSL.FieldName = "SELTENDSL"
        Me.GSELTENDSL.Name = "GSELTENDSL"
        Me.GSELTENDSL.OptionsColumn.AllowEdit = False
        Me.GSELTENDSL.Visible = True
        Me.GSELTENDSL.VisibleIndex = 29
        '
        'GSELENDSR
        '
        Me.GSELENDSR.Caption = "Sel Ends (R)"
        Me.GSELENDSR.FieldName = "SELENDSR"
        Me.GSELENDSR.Name = "GSELENDSR"
        Me.GSELENDSR.OptionsColumn.AllowEdit = False
        Me.GSELENDSR.Visible = True
        Me.GSELENDSR.VisibleIndex = 30
        '
        'GSELTENDSR
        '
        Me.GSELTENDSR.Caption = "Sel T. Ends (R)"
        Me.GSELTENDSR.FieldName = "SELTENDSR"
        Me.GSELTENDSR.Name = "GSELTENDSR"
        Me.GSELTENDSR.OptionsColumn.AllowEdit = False
        Me.GSELTENDSR.Visible = True
        Me.GSELTENDSR.VisibleIndex = 31
        '
        'GSELTENDSLR
        '
        Me.GSELTENDSLR.Caption = "Sel T. Ends (L+R)"
        Me.GSELTENDSLR.FieldName = "SELTENDSLR"
        Me.GSELTENDSLR.Name = "GSELTENDSLR"
        Me.GSELTENDSLR.OptionsColumn.AllowEdit = False
        Me.GSELTENDSLR.Visible = True
        Me.GSELTENDSLR.VisibleIndex = 32
        '
        'GSORTREFNO
        '
        Me.GSORTREFNO.Caption = "Sort /Ref No"
        Me.GSORTREFNO.FieldName = "SORTREFNO"
        Me.GSORTREFNO.Name = "GSORTREFNO"
        Me.GSORTREFNO.OptionsColumn.AllowEdit = False
        Me.GSORTREFNO.Visible = True
        Me.GSORTREFNO.VisibleIndex = 33
        '
        'GPARTYNAME
        '
        Me.GPARTYNAME.Caption = "Party Name"
        Me.GPARTYNAME.FieldName = "PARTYNAME"
        Me.GPARTYNAME.Name = "GPARTYNAME"
        Me.GPARTYNAME.OptionsColumn.AllowEdit = False
        Me.GPARTYNAME.Visible = True
        Me.GPARTYNAME.VisibleIndex = 34
        '
        'GORDERNO
        '
        Me.GORDERNO.Caption = "Order No"
        Me.GORDERNO.FieldName = "ORDERNO"
        Me.GORDERNO.Name = "GORDERNO"
        Me.GORDERNO.OptionsColumn.AllowEdit = False
        Me.GORDERNO.Visible = True
        Me.GORDERNO.VisibleIndex = 35
        '
        'GORDERDATE
        '
        Me.GORDERDATE.Caption = "Order Date "
        Me.GORDERDATE.FieldName = "ORDERDATE"
        Me.GORDERDATE.Name = "GORDERDATE"
        Me.GORDERDATE.OptionsColumn.AllowEdit = False
        Me.GORDERDATE.Visible = True
        Me.GORDERDATE.VisibleIndex = 36
        '
        'GAGENTNAME
        '
        Me.GAGENTNAME.Caption = "Agent Name "
        Me.GAGENTNAME.FieldName = "AGENTNAME"
        Me.GAGENTNAME.Name = "GAGENTNAME"
        Me.GAGENTNAME.OptionsColumn.AllowEdit = False
        Me.GAGENTNAME.Visible = True
        Me.GAGENTNAME.VisibleIndex = 37
        '
        'GGREYFIN
        '
        Me.GGREYFIN.Caption = "Grey/Fin"
        Me.GGREYFIN.FieldName = "GREYFIN"
        Me.GGREYFIN.Name = "GGREYFIN"
        Me.GGREYFIN.OptionsColumn.AllowEdit = False
        Me.GGREYFIN.Visible = True
        Me.GGREYFIN.VisibleIndex = 38
        '
        'GDELIVERYAT
        '
        Me.GDELIVERYAT.Caption = "Delivery At"
        Me.GDELIVERYAT.FieldName = "DELIVERYAT"
        Me.GDELIVERYAT.Name = "GDELIVERYAT"
        Me.GDELIVERYAT.OptionsColumn.AllowEdit = False
        Me.GDELIVERYAT.Visible = True
        Me.GDELIVERYAT.VisibleIndex = 39
        '
        'GDELDATE
        '
        Me.GDELDATE.Caption = "Del Date"
        Me.GDELDATE.FieldName = "DELDATE"
        Me.GDELDATE.Name = "GDELDATE"
        Me.GDELDATE.OptionsColumn.AllowEdit = False
        Me.GDELDATE.Visible = True
        Me.GDELDATE.VisibleIndex = 40
        '
        'GPIECEMTRS
        '
        Me.GPIECEMTRS.Caption = "Piece Mtrs"
        Me.GPIECEMTRS.FieldName = "PIECEMTRS"
        Me.GPIECEMTRS.Name = "GPIECEMTRS"
        Me.GPIECEMTRS.OptionsColumn.AllowEdit = False
        Me.GPIECEMTRS.Visible = True
        Me.GPIECEMTRS.VisibleIndex = 41
        '
        'GNOOFPCS
        '
        Me.GNOOFPCS.Caption = "No of Pcs"
        Me.GNOOFPCS.FieldName = "NOOFPCS"
        Me.GNOOFPCS.Name = "GNOOFPCS"
        Me.GNOOFPCS.OptionsColumn.AllowEdit = False
        Me.GNOOFPCS.Visible = True
        Me.GNOOFPCS.VisibleIndex = 42
        '
        'GLOOM
        '
        Me.GLOOM.Caption = "Loom"
        Me.GLOOM.FieldName = "LOOM"
        Me.GLOOM.Name = "GLOOM"
        Me.GLOOM.OptionsColumn.AllowEdit = False
        Me.GLOOM.Visible = True
        Me.GLOOM.VisibleIndex = 43
        '
        'GBEAMMTRS
        '
        Me.GBEAMMTRS.Caption = "Beam Mtrs"
        Me.GBEAMMTRS.FieldName = "BEAMMTRS"
        Me.GBEAMMTRS.Name = "GBEAMMTRS"
        Me.GBEAMMTRS.OptionsColumn.AllowEdit = False
        Me.GBEAMMTRS.Visible = True
        Me.GBEAMMTRS.VisibleIndex = 44
        '
        'GLOOMPROD
        '
        Me.GLOOMPROD.Caption = "Loom Prod"
        Me.GLOOMPROD.FieldName = "LOOMPROD"
        Me.GLOOMPROD.Name = "GLOOMPROD"
        Me.GLOOMPROD.OptionsColumn.AllowEdit = False
        Me.GLOOMPROD.Visible = True
        Me.GLOOMPROD.VisibleIndex = 45
        '
        'GRPM
        '
        Me.GRPM.Caption = "RPM"
        Me.GRPM.FieldName = "RPM"
        Me.GRPM.Name = "GRPM"
        Me.GRPM.OptionsColumn.AllowEdit = False
        Me.GRPM.Visible = True
        Me.GRPM.VisibleIndex = 46
        '
        'GCOVERFACTOR
        '
        Me.GCOVERFACTOR.Caption = "Cover Factor"
        Me.GCOVERFACTOR.FieldName = "COVERFACTOR"
        Me.GCOVERFACTOR.Name = "GCOVERFACTOR"
        Me.GCOVERFACTOR.OptionsColumn.AllowEdit = False
        Me.GCOVERFACTOR.Visible = True
        Me.GCOVERFACTOR.VisibleIndex = 47
        '
        'GEFFICIENCY
        '
        Me.GEFFICIENCY.Caption = "Efficiency"
        Me.GEFFICIENCY.FieldName = "EFFICIENCY"
        Me.GEFFICIENCY.Name = "GEFFICIENCY"
        Me.GEFFICIENCY.OptionsColumn.AllowEdit = False
        Me.GEFFICIENCY.Visible = True
        Me.GEFFICIENCY.VisibleIndex = 48
        '
        'GGREYDELIVERYAT
        '
        Me.GGREYDELIVERYAT.Caption = "Grey Delivery At"
        Me.GGREYDELIVERYAT.FieldName = "GRYEDELIVERYAT"
        Me.GGREYDELIVERYAT.Name = "GGREYDELIVERYAT"
        Me.GGREYDELIVERYAT.OptionsColumn.AllowEdit = False
        Me.GGREYDELIVERYAT.Visible = True
        Me.GGREYDELIVERYAT.VisibleIndex = 49
        '
        'GGREYDELDATE
        '
        Me.GGREYDELDATE.Caption = "Grey Del Date"
        Me.GGREYDELDATE.FieldName = "GREYDELDATE"
        Me.GGREYDELDATE.Name = "GGREYDELDATE"
        Me.GGREYDELDATE.OptionsColumn.AllowEdit = False
        Me.GGREYDELDATE.Visible = True
        Me.GGREYDELDATE.VisibleIndex = 50
        '
        'GEPD
        '
        Me.GEPD.Caption = "E.P.D."
        Me.GEPD.FieldName = "EPD"
        Me.GEPD.Name = "GEPD"
        Me.GEPD.OptionsColumn.AllowEdit = False
        Me.GEPD.Visible = True
        Me.GEPD.VisibleIndex = 51
        '
        'GEPI
        '
        Me.GEPI.Caption = "E.P.I."
        Me.GEPI.FieldName = "EPI"
        Me.GEPI.Name = "GEPI"
        Me.GEPI.OptionsColumn.AllowEdit = False
        Me.GEPI.Visible = True
        Me.GEPI.VisibleIndex = 52
        '
        'GSELSIZE
        '
        Me.GSELSIZE.Caption = "Sel Size"
        Me.GSELSIZE.FieldName = "SELSIZE"
        Me.GSELSIZE.Name = "GSELSIZE"
        Me.GSELSIZE.OptionsColumn.AllowEdit = False
        Me.GSELSIZE.Visible = True
        Me.GSELSIZE.VisibleIndex = 53
        '
        'GSELEPDENT
        '
        Me.GSELEPDENT.Caption = "Sel E. P. Dent"
        Me.GSELEPDENT.FieldName = "SELEPDENT"
        Me.GSELEPDENT.Name = "GSELEPDENT"
        Me.GSELEPDENT.OptionsColumn.AllowEdit = False
        Me.GSELEPDENT.Visible = True
        Me.GSELEPDENT.VisibleIndex = 54
        '
        'GSELTENDS
        '
        Me.GSELTENDS.Caption = "Sel t. Ends"
        Me.GSELTENDS.FieldName = "SELTENDS"
        Me.GSELTENDS.Name = "GSELTENDS"
        Me.GSELTENDS.OptionsColumn.AllowEdit = False
        Me.GSELTENDS.Visible = True
        Me.GSELTENDS.VisibleIndex = 55
        '
        'GTOTALENDS
        '
        Me.GTOTALENDS.Caption = "Total Ends"
        Me.GTOTALENDS.FieldName = "TOTALENDS"
        Me.GTOTALENDS.Name = "GTOTALENDS"
        Me.GTOTALENDS.OptionsColumn.AllowEdit = False
        Me.GTOTALENDS.Visible = True
        Me.GTOTALENDS.VisibleIndex = 56
        '
        'GGWIDTH
        '
        Me.GGWIDTH.Caption = "G. Width"
        Me.GGWIDTH.FieldName = "GWIDTH"
        Me.GGWIDTH.Name = "GGWIDTH"
        Me.GGWIDTH.OptionsColumn.AllowEdit = False
        Me.GGWIDTH.Visible = True
        Me.GGWIDTH.VisibleIndex = 57
        '
        'GFWIDTH
        '
        Me.GFWIDTH.Caption = "F. Width"
        Me.GFWIDTH.FieldName = "FWIDTH"
        Me.GFWIDTH.Name = "GFWIDTH"
        Me.GFWIDTH.OptionsColumn.AllowEdit = False
        Me.GFWIDTH.Visible = True
        Me.GFWIDTH.VisibleIndex = 58
        '
        'GGRIEGEWT
        '
        Me.GGRIEGEWT.Caption = "Griege Wt"
        Me.GGRIEGEWT.FieldName = "GRIEGEWT"
        Me.GGRIEGEWT.Name = "GGRIEGEWT"
        Me.GGRIEGEWT.OptionsColumn.AllowEdit = False
        Me.GGRIEGEWT.Visible = True
        Me.GGRIEGEWT.VisibleIndex = 59
        '
        'GBLEND
        '
        Me.GBLEND.Caption = "Blend %"
        Me.GBLEND.FieldName = "BLEND"
        Me.GBLEND.Name = "GBLEND"
        Me.GBLEND.OptionsColumn.AllowEdit = False
        Me.GBLEND.Visible = True
        Me.GBLEND.VisibleIndex = 60
        '
        'GFINISHMETHOD
        '
        Me.GFINISHMETHOD.Caption = "finish method"
        Me.GFINISHMETHOD.FieldName = "FINISHMETHOD"
        Me.GFINISHMETHOD.Name = "GFINISHMETHOD"
        Me.GFINISHMETHOD.OptionsColumn.AllowEdit = False
        Me.GFINISHMETHOD.Visible = True
        Me.GFINISHMETHOD.VisibleIndex = 61
        '
        'GQUALITY
        '
        Me.GQUALITY.Caption = "Qualities"
        Me.GQUALITY.FieldName = "QUALITY"
        Me.GQUALITY.Name = "GQUALITY"
        Me.GQUALITY.OptionsColumn.AllowEdit = False
        Me.GQUALITY.Visible = True
        Me.GQUALITY.VisibleIndex = 62
        '
        'GQUALITYTYPE
        '
        Me.GQUALITYTYPE.Caption = "Quality Type"
        Me.GQUALITYTYPE.FieldName = "QUALITYTYPE"
        Me.GQUALITYTYPE.Name = "GQUALITYTYPE"
        Me.GQUALITYTYPE.OptionsColumn.AllowEdit = False
        Me.GQUALITYTYPE.Visible = True
        Me.GQUALITYTYPE.VisibleIndex = 63
        '
        'GWARPWES
        '
        Me.GWARPWES.Caption = "warp wes%"
        Me.GWARPWES.FieldName = "WARPWES"
        Me.GWARPWES.Name = "GWARPWES"
        Me.GWARPWES.OptionsColumn.AllowEdit = False
        Me.GWARPWES.Visible = True
        Me.GWARPWES.VisibleIndex = 64
        '
        'GWASTAGE
        '
        Me.GWASTAGE.Caption = "Wastage %"
        Me.GWASTAGE.FieldName = "WASTAGE"
        Me.GWASTAGE.Name = "GWASTAGE"
        Me.GWASTAGE.OptionsColumn.AllowEdit = False
        Me.GWASTAGE.Visible = True
        Me.GWASTAGE.VisibleIndex = 65
        '
        'GSHRINKAGE
        '
        Me.GSHRINKAGE.Caption = "Shrinkage %"
        Me.GSHRINKAGE.FieldName = "SHRINKAGE"
        Me.GSHRINKAGE.Name = "GSHRINKAGE"
        Me.GSHRINKAGE.OptionsColumn.AllowEdit = False
        Me.GSHRINKAGE.Visible = True
        Me.GSHRINKAGE.VisibleIndex = 66
        '
        'GWPP
        '
        Me.GWPP.Caption = "W.P.P."
        Me.GWPP.FieldName = "WPP"
        Me.GWPP.Name = "GWPP"
        Me.GWPP.OptionsColumn.AllowEdit = False
        Me.GWPP.Visible = True
        Me.GWPP.VisibleIndex = 67
        '
        'GWEAVINGCOSTMTR
        '
        Me.GWEAVINGCOSTMTR.Caption = "Weaving Cost /mtr"
        Me.GWEAVINGCOSTMTR.FieldName = "WEAVINGCOSTMTR"
        Me.GWEAVINGCOSTMTR.Name = "GWEAVINGCOSTMTR"
        Me.GWEAVINGCOSTMTR.OptionsColumn.AllowEdit = False
        Me.GWEAVINGCOSTMTR.Visible = True
        Me.GWEAVINGCOSTMTR.VisibleIndex = 68
        '
        'GGRIEGEFABCOST
        '
        Me.GGRIEGEFABCOST.Caption = "Griege fab. cost"
        Me.GGRIEGEFABCOST.FieldName = "GRIEGEFABCOST"
        Me.GGRIEGEFABCOST.Name = "GGRIEGEFABCOST"
        Me.GGRIEGEFABCOST.OptionsColumn.AllowEdit = False
        Me.GGRIEGEFABCOST.Visible = True
        Me.GGRIEGEFABCOST.VisibleIndex = 69
        '
        'GFINISHFABCOST
        '
        Me.GFINISHFABCOST.Caption = "finish fab. cost"
        Me.GFINISHFABCOST.FieldName = "FINISHFABCOST"
        Me.GFINISHFABCOST.Name = "GFINISHFABCOST"
        Me.GFINISHFABCOST.OptionsColumn.AllowEdit = False
        Me.GFINISHFABCOST.Visible = True
        Me.GFINISHFABCOST.VisibleIndex = 70
        '
        'GPRODDAY
        '
        Me.GPRODDAY.Caption = "Prod /Day"
        Me.GPRODDAY.FieldName = "PRODDAY"
        Me.GPRODDAY.Name = "GPRODDAY"
        Me.GPRODDAY.OptionsColumn.AllowEdit = False
        Me.GPRODDAY.Visible = True
        Me.GPRODDAY.VisibleIndex = 71
        '
        'GPIECEL
        '
        Me.GPIECEL.Caption = "Piece L"
        Me.GPIECEL.Name = "GPIECEL"
        Me.GPIECEL.Visible = True
        Me.GPIECEL.VisibleIndex = 72
        '
        'GGLM
        '
        Me.GGLM.Caption = "GLM"
        Me.GGLM.FieldName = "GLM"
        Me.GGLM.Name = "GGLM"
        Me.GGLM.OptionsColumn.AllowEdit = False
        Me.GGLM.Visible = True
        Me.GGLM.VisibleIndex = 73
        '
        'GTOTALMAINENDS
        '
        Me.GTOTALMAINENDS.Caption = "Total Main Ends"
        Me.GTOTALMAINENDS.FieldName = "TOTALMAINENDS"
        Me.GTOTALMAINENDS.Name = "GTOTALMAINENDS"
        Me.GTOTALMAINENDS.OptionsColumn.AllowEdit = False
        Me.GTOTALMAINENDS.Visible = True
        Me.GTOTALMAINENDS.VisibleIndex = 74
        '
        'GTOTALDENT
        '
        Me.GTOTALDENT.Caption = "Total Dents"
        Me.GTOTALDENT.FieldName = "TOTALDENT"
        Me.GTOTALDENT.Name = "GTOTALDENT"
        Me.GTOTALDENT.OptionsColumn.AllowEdit = False
        Me.GTOTALDENT.Visible = True
        Me.GTOTALDENT.VisibleIndex = 75
        '
        'GTOTALDENTSREPEAT
        '
        Me.GTOTALDENTSREPEAT.Caption = "Total Dents Repeat"
        Me.GTOTALDENTSREPEAT.FieldName = "TOTALDENTSREPEAT"
        Me.GTOTALDENTSREPEAT.Name = "GTOTALDENTSREPEAT"
        Me.GTOTALDENTSREPEAT.OptionsColumn.AllowEdit = False
        Me.GTOTALDENTSREPEAT.Visible = True
        Me.GTOTALDENTSREPEAT.VisibleIndex = 76
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.toolStripSeparator, Me.ExcelExport, Me.ToolStripSeparator2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 318
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'DesignCardMasterDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel2)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "DesignCardMasterDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Design Card Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel2.ResumeLayout(False)
        Me.BlendPanel2.PerformLayout()
        CType(Me.GRIDBILLDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDBILL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents miniToolStrip As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents ExcelExport As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDEDIT As Button
    Friend WithEvents CMDADDNEW As Button
    Friend WithEvents CMDREFRESH As Button
    Friend WithEvents BlendPanel2 As VbPowerPack.BlendPanel
    Friend WithEvents GRIDBILLDETAILS As DevExpress.XtraGrid.GridControl
    Friend WithEvents GRIDBILL As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents GITEMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDESIGNNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREEDSPACE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDENTS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARPTTL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWEFTTTL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPICKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMAINRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALDENTSMAIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGSM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTHREADPERDENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALDENTSSEL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWEAVE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFEPI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFINISHWIDTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALDENTS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSHAFTS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALDESIGNWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFINISHPPI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALFINISHWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELVEDGEL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELDENTSL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPHOTOGRAPH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELVEDGER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELDENTSR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELENDSL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELTENDSL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELENDSR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELTENDSR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELTENDSLR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSORTREFNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPARTYNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GORDERNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GORDERDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENTNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGREYFIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDELIVERYAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDELDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPIECEMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNOOFPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLOOM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBEAMMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLOOMPROD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRPM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOVERFACTOR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEFFICIENCY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGREYDELIVERYAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGREYDELDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCARDNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEPD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEPI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELSIZE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELEPDENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSELTENDS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALENDS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGWIDTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFWIDTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRIEGEWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBLEND As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFINISHMETHOD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GQUALITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GQUALITYTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARPWES As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWASTAGE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSHRINKAGE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWPP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWEAVINGCOSTMTR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRIEGEFABCOST As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFINISHFABCOST As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPRODDAY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPIECEL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGLM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALMAINENDS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALDENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALDENTSREPEAT As DevExpress.XtraGrid.Columns.GridColumn
End Class
