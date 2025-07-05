<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LotTagging
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LotTagging))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.PRINTTOOLSTRIP = New System.Windows.Forms.ToolStripButton()
        Me.TOOLDELETE = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLPREVIOUS = New System.Windows.Forms.ToolStripButton()
        Me.TOOLNEXT = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.CHKPENDINGLOT = New System.Windows.Forms.CheckBox()
        Me.GRIDPROGRAMDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDPROGRAM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PGRIDSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PFROMNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPROGDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PITEMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PDESIGN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSHADE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PBALMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PADJMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PFROMSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PFROMTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GRIDLOTDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDLOT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GGRIDSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLOTNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLOTDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GITEMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBALMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GADJMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFROMNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFROMTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKDONE = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LBLTOTALBALMTRS = New System.Windows.Forms.Label()
        Me.LBLTOTALLOTBALMTRS = New System.Windows.Forms.Label()
        Me.LBLTOTALADJMTRS = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LBLTOTALMTRS = New System.Windows.Forms.Label()
        Me.LBLTOTALLOTADJMTRS = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.LBLTOTALLOTMTRS = New System.Windows.Forms.Label()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.LOTTAGDATE = New System.Windows.Forms.DateTimePicker()
        Me.TXTLOTTAGNO = New System.Windows.Forms.TextBox()
        Me.LBLSRNO = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.CMDDELETE = New System.Windows.Forms.Button()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.tstxtbillno = New System.Windows.Forms.TextBox()
        Me.PSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ToolStrip1.SuspendLayout()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDPROGRAMDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDPROGRAM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDLOTDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDLOT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.ToolStripButton2, Me.PRINTTOOLSTRIP, Me.TOOLDELETE, Me.ToolStripSeparator2, Me.TOOLPREVIOUS, Me.TOOLNEXT, Me.ToolStripSeparator3})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1143, 25)
        Me.ToolStrip1.TabIndex = 614
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.OpenToolStripButton.Text = "&Open"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "&Save"
        '
        'PRINTTOOLSTRIP
        '
        Me.PRINTTOOLSTRIP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PRINTTOOLSTRIP.Image = CType(resources.GetObject("PRINTTOOLSTRIP.Image"), System.Drawing.Image)
        Me.PRINTTOOLSTRIP.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PRINTTOOLSTRIP.Name = "PRINTTOOLSTRIP"
        Me.PRINTTOOLSTRIP.Size = New System.Drawing.Size(23, 22)
        Me.PRINTTOOLSTRIP.Text = "&Print"
        '
        'TOOLDELETE
        '
        Me.TOOLDELETE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLDELETE.Image = CType(resources.GetObject("TOOLDELETE.Image"), System.Drawing.Image)
        Me.TOOLDELETE.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLDELETE.Name = "TOOLDELETE"
        Me.TOOLDELETE.Size = New System.Drawing.Size(23, 22)
        Me.TOOLDELETE.Text = "&Delete"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'TOOLPREVIOUS
        '
        Me.TOOLPREVIOUS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TOOLPREVIOUS.Image = Global.TEXTRADE.My.Resources.Resources.POINT021
        Me.TOOLPREVIOUS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLPREVIOUS.Name = "TOOLPREVIOUS"
        Me.TOOLPREVIOUS.Size = New System.Drawing.Size(73, 22)
        Me.TOOLPREVIOUS.Text = "Previous"
        '
        'TOOLNEXT
        '
        Me.TOOLNEXT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TOOLNEXT.Image = Global.TEXTRADE.My.Resources.Resources.POINT04
        Me.TOOLNEXT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLNEXT.Name = "TOOLNEXT"
        Me.TOOLNEXT.Size = New System.Drawing.Size(51, 22)
        Me.TOOLNEXT.Text = "Next"
        Me.TOOLNEXT.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CHKPENDINGLOT)
        Me.BlendPanel1.Controls.Add(Me.GRIDPROGRAMDETAILS)
        Me.BlendPanel1.Controls.Add(Me.GRIDLOTDETAILS)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALBALMTRS)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALLOTBALMTRS)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALADJMTRS)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALMTRS)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALLOTADJMTRS)
        Me.BlendPanel1.Controls.Add(Me.Label10)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALLOTMTRS)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.Label7)
        Me.BlendPanel1.Controls.Add(Me.CMBNAME)
        Me.BlendPanel1.Controls.Add(Me.LOTTAGDATE)
        Me.BlendPanel1.Controls.Add(Me.TXTLOTTAGNO)
        Me.BlendPanel1.Controls.Add(Me.LBLSRNO)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.CMDDELETE)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 25)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1143, 642)
        Me.BlendPanel1.TabIndex = 615
        '
        'CHKPENDINGLOT
        '
        Me.CHKPENDINGLOT.AutoSize = True
        Me.CHKPENDINGLOT.BackColor = System.Drawing.Color.Transparent
        Me.CHKPENDINGLOT.Location = New System.Drawing.Point(466, 9)
        Me.CHKPENDINGLOT.Name = "CHKPENDINGLOT"
        Me.CHKPENDINGLOT.Size = New System.Drawing.Size(193, 19)
        Me.CHKPENDINGLOT.TabIndex = 805
        Me.CHKPENDINGLOT.Text = "Lock Pending Lots For Program"
        Me.CHKPENDINGLOT.UseVisualStyleBackColor = False
        '
        'GRIDPROGRAMDETAILS
        '
        Me.GRIDPROGRAMDETAILS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDPROGRAMDETAILS.Location = New System.Drawing.Point(30, 305)
        Me.GRIDPROGRAMDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDPROGRAMDETAILS.MainView = Me.GRIDPROGRAM
        Me.GRIDPROGRAMDETAILS.Name = "GRIDPROGRAMDETAILS"
        Me.GRIDPROGRAMDETAILS.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemTextEdit2})
        Me.GRIDPROGRAMDETAILS.Size = New System.Drawing.Size(1079, 294)
        Me.GRIDPROGRAMDETAILS.TabIndex = 764
        Me.GRIDPROGRAMDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDPROGRAM, Me.GridView3})
        '
        'GRIDPROGRAM
        '
        Me.GRIDPROGRAM.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDPROGRAM.Appearance.HeaderPanel.Options.UseFont = True
        Me.GRIDPROGRAM.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDPROGRAM.Appearance.Row.Options.UseFont = True
        Me.GRIDPROGRAM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.PGRIDSRNO, Me.PCHK, Me.PFROMNO, Me.GPROGDATE, Me.PITEMNAME, Me.PDESIGN, Me.GSHADE, Me.PMTRS, Me.PBALMTRS, Me.PADJMTRS, Me.PFROMSRNO, Me.PFROMTYPE})
        Me.GRIDPROGRAM.CustomizationFormBounds = New System.Drawing.Rectangle(688, 311, 208, 184)
        Me.GRIDPROGRAM.GridControl = Me.GRIDPROGRAMDETAILS
        Me.GRIDPROGRAM.Name = "GRIDPROGRAM"
        Me.GRIDPROGRAM.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDPROGRAM.OptionsCustomization.AllowColumnMoving = False
        Me.GRIDPROGRAM.OptionsCustomization.AllowQuickHideColumns = False
        Me.GRIDPROGRAM.OptionsView.ColumnAutoWidth = False
        Me.GRIDPROGRAM.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDPROGRAM.OptionsView.ShowAutoFilterRow = True
        Me.GRIDPROGRAM.OptionsView.ShowGroupPanel = False
        '
        'PGRIDSRNO
        '
        Me.PGRIDSRNO.Caption = "Sr.No"
        Me.PGRIDSRNO.DisplayFormat.FormatString = "0"
        Me.PGRIDSRNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PGRIDSRNO.FieldName = "SRNO"
        Me.PGRIDSRNO.Name = "PGRIDSRNO"
        Me.PGRIDSRNO.OptionsColumn.AllowEdit = False
        Me.PGRIDSRNO.Visible = True
        Me.PGRIDSRNO.VisibleIndex = 0
        Me.PGRIDSRNO.Width = 50
        '
        'PCHK
        '
        Me.PCHK.Caption = "Chk"
        Me.PCHK.FieldName = "CHK"
        Me.PCHK.Name = "PCHK"
        Me.PCHK.Visible = True
        Me.PCHK.VisibleIndex = 1
        Me.PCHK.Width = 30
        '
        'PFROMNO
        '
        Me.PFROMNO.Caption = "Prog. No"
        Me.PFROMNO.DisplayFormat.FormatString = "0"
        Me.PFROMNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PFROMNO.FieldName = "FROMNO"
        Me.PFROMNO.Name = "PFROMNO"
        Me.PFROMNO.OptionsColumn.AllowEdit = False
        Me.PFROMNO.Visible = True
        Me.PFROMNO.VisibleIndex = 2
        Me.PFROMNO.Width = 80
        '
        'GPROGDATE
        '
        Me.GPROGDATE.Caption = "Prog Date"
        Me.GPROGDATE.FieldName = "PROGDATE"
        Me.GPROGDATE.Name = "GPROGDATE"
        Me.GPROGDATE.OptionsColumn.AllowEdit = False
        Me.GPROGDATE.Visible = True
        Me.GPROGDATE.VisibleIndex = 3
        Me.GPROGDATE.Width = 100
        '
        'PITEMNAME
        '
        Me.PITEMNAME.Caption = "Item Name"
        Me.PITEMNAME.FieldName = "ITEMNAME"
        Me.PITEMNAME.Name = "PITEMNAME"
        Me.PITEMNAME.OptionsColumn.AllowEdit = False
        Me.PITEMNAME.Visible = True
        Me.PITEMNAME.VisibleIndex = 4
        Me.PITEMNAME.Width = 200
        '
        'PDESIGN
        '
        Me.PDESIGN.Caption = "Design"
        Me.PDESIGN.FieldName = "DESIGN"
        Me.PDESIGN.Name = "PDESIGN"
        Me.PDESIGN.OptionsColumn.AllowEdit = False
        Me.PDESIGN.Visible = True
        Me.PDESIGN.VisibleIndex = 5
        Me.PDESIGN.Width = 150
        '
        'GSHADE
        '
        Me.GSHADE.Caption = "Shade"
        Me.GSHADE.FieldName = "SHADE"
        Me.GSHADE.Name = "GSHADE"
        Me.GSHADE.OptionsColumn.AllowEdit = False
        Me.GSHADE.Visible = True
        Me.GSHADE.VisibleIndex = 6
        Me.GSHADE.Width = 150
        '
        'PMTRS
        '
        Me.PMTRS.Caption = "Mtrs"
        Me.PMTRS.DisplayFormat.FormatString = "0.00"
        Me.PMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PMTRS.FieldName = "MTRS"
        Me.PMTRS.Name = "PMTRS"
        Me.PMTRS.OptionsColumn.AllowEdit = False
        Me.PMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.PMTRS.Visible = True
        Me.PMTRS.VisibleIndex = 7
        Me.PMTRS.Width = 100
        '
        'PBALMTRS
        '
        Me.PBALMTRS.Caption = "Bal Mtrs"
        Me.PBALMTRS.DisplayFormat.FormatString = "0.00"
        Me.PBALMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PBALMTRS.FieldName = "BALMTRS"
        Me.PBALMTRS.Name = "PBALMTRS"
        Me.PBALMTRS.OptionsColumn.AllowEdit = False
        Me.PBALMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.PBALMTRS.Visible = True
        Me.PBALMTRS.VisibleIndex = 8
        Me.PBALMTRS.Width = 100
        '
        'PADJMTRS
        '
        Me.PADJMTRS.Caption = "Adj Mtrs"
        Me.PADJMTRS.DisplayFormat.FormatString = "0.00"
        Me.PADJMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PADJMTRS.FieldName = "ADJMTRS"
        Me.PADJMTRS.Name = "PADJMTRS"
        Me.PADJMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.PADJMTRS.Visible = True
        Me.PADJMTRS.VisibleIndex = 9
        Me.PADJMTRS.Width = 100
        '
        'PFROMSRNO
        '
        Me.PFROMSRNO.Caption = "From Sr.NO"
        Me.PFROMSRNO.DisplayFormat.FormatString = "0"
        Me.PFROMSRNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PFROMSRNO.FieldName = "FROMSRNO"
        Me.PFROMSRNO.Name = "PFROMSRNO"
        Me.PFROMSRNO.OptionsColumn.AllowEdit = False
        '
        'PFROMTYPE
        '
        Me.PFROMTYPE.Caption = "From Ttype"
        Me.PFROMTYPE.FieldName = "FROMTYPE"
        Me.PFROMTYPE.Name = "PFROMTYPE"
        Me.PFROMTYPE.OptionsColumn.AllowEdit = False
        Me.PFROMTYPE.Width = 100
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'RepositoryItemTextEdit2
        '
        Me.RepositoryItemTextEdit2.AutoHeight = False
        Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
        '
        'GridView3
        '
        Me.GridView3.GridControl = Me.GRIDPROGRAMDETAILS
        Me.GridView3.Name = "GridView3"
        '
        'GRIDLOTDETAILS
        '
        Me.GRIDLOTDETAILS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDLOTDETAILS.Location = New System.Drawing.Point(30, 34)
        Me.GRIDLOTDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDLOTDETAILS.MainView = Me.GRIDLOT
        Me.GRIDLOTDETAILS.Name = "GRIDLOTDETAILS"
        Me.GRIDLOTDETAILS.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKDONE, Me.RepositoryItemTextEdit1})
        Me.GRIDLOTDETAILS.Size = New System.Drawing.Size(1079, 237)
        Me.GRIDLOTDETAILS.TabIndex = 763
        Me.GRIDLOTDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDLOT, Me.GridView1})
        '
        'GRIDLOT
        '
        Me.GRIDLOT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDLOT.Appearance.HeaderPanel.Options.UseFont = True
        Me.GRIDLOT.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDLOT.Appearance.Row.Options.UseFont = True
        Me.GRIDLOT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GGRIDSRNO, Me.GCHK, Me.GLRNO, Me.GLOTNO, Me.GLOTDATE, Me.GITEMNAME, Me.GMTRS, Me.GBALMTRS, Me.GADJMTRS, Me.GFROMNO, Me.GFROMTYPE})
        Me.GRIDLOT.CustomizationFormBounds = New System.Drawing.Rectangle(688, 311, 208, 184)
        Me.GRIDLOT.GridControl = Me.GRIDLOTDETAILS
        Me.GRIDLOT.Name = "GRIDLOT"
        Me.GRIDLOT.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDLOT.OptionsCustomization.AllowColumnMoving = False
        Me.GRIDLOT.OptionsCustomization.AllowQuickHideColumns = False
        Me.GRIDLOT.OptionsView.ColumnAutoWidth = False
        Me.GRIDLOT.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDLOT.OptionsView.ShowAutoFilterRow = True
        Me.GRIDLOT.OptionsView.ShowGroupPanel = False
        '
        'GGRIDSRNO
        '
        Me.GGRIDSRNO.Caption = "Sr.No"
        Me.GGRIDSRNO.DisplayFormat.FormatString = "0"
        Me.GGRIDSRNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGRIDSRNO.FieldName = "SRNO"
        Me.GGRIDSRNO.Name = "GGRIDSRNO"
        Me.GGRIDSRNO.OptionsColumn.AllowEdit = False
        Me.GGRIDSRNO.Visible = True
        Me.GGRIDSRNO.VisibleIndex = 0
        Me.GGRIDSRNO.Width = 50
        '
        'GCHK
        '
        Me.GCHK.Caption = "Chk"
        Me.GCHK.FieldName = "CHK"
        Me.GCHK.Name = "GCHK"
        Me.GCHK.Visible = True
        Me.GCHK.VisibleIndex = 1
        Me.GCHK.Width = 30
        '
        'GLRNO
        '
        Me.GLRNO.Caption = "Lr.No"
        Me.GLRNO.FieldName = "LRNO"
        Me.GLRNO.Name = "GLRNO"
        Me.GLRNO.OptionsColumn.AllowEdit = False
        Me.GLRNO.Visible = True
        Me.GLRNO.VisibleIndex = 2
        Me.GLRNO.Width = 155
        '
        'GLOTNO
        '
        Me.GLOTNO.Caption = "Lot No"
        Me.GLOTNO.FieldName = "LOTNO"
        Me.GLOTNO.Name = "GLOTNO"
        Me.GLOTNO.OptionsColumn.AllowEdit = False
        Me.GLOTNO.Visible = True
        Me.GLOTNO.VisibleIndex = 3
        Me.GLOTNO.Width = 120
        '
        'GLOTDATE
        '
        Me.GLOTDATE.Caption = "Lot Date"
        Me.GLOTDATE.FieldName = "DATE"
        Me.GLOTDATE.Name = "GLOTDATE"
        Me.GLOTDATE.OptionsColumn.AllowEdit = False
        Me.GLOTDATE.Visible = True
        Me.GLOTDATE.VisibleIndex = 4
        Me.GLOTDATE.Width = 100
        '
        'GITEMNAME
        '
        Me.GITEMNAME.Caption = "Item Name"
        Me.GITEMNAME.FieldName = "ITEMNAME"
        Me.GITEMNAME.Name = "GITEMNAME"
        Me.GITEMNAME.OptionsColumn.AllowEdit = False
        Me.GITEMNAME.Visible = True
        Me.GITEMNAME.VisibleIndex = 5
        Me.GITEMNAME.Width = 270
        '
        'GMTRS
        '
        Me.GMTRS.Caption = "Mtrs"
        Me.GMTRS.DisplayFormat.FormatString = "0.00"
        Me.GMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GMTRS.FieldName = "TOTALMTRS"
        Me.GMTRS.Name = "GMTRS"
        Me.GMTRS.OptionsColumn.AllowEdit = False
        Me.GMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GMTRS.Visible = True
        Me.GMTRS.VisibleIndex = 6
        Me.GMTRS.Width = 100
        '
        'GBALMTRS
        '
        Me.GBALMTRS.Caption = "Bal Mtrs"
        Me.GBALMTRS.DisplayFormat.FormatString = "0.00"
        Me.GBALMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GBALMTRS.FieldName = "BALMTRS"
        Me.GBALMTRS.Name = "GBALMTRS"
        Me.GBALMTRS.OptionsColumn.AllowEdit = False
        Me.GBALMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GBALMTRS.Visible = True
        Me.GBALMTRS.VisibleIndex = 7
        Me.GBALMTRS.Width = 100
        '
        'GADJMTRS
        '
        Me.GADJMTRS.Caption = "Adj Mtrs"
        Me.GADJMTRS.DisplayFormat.FormatString = "0.00"
        Me.GADJMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GADJMTRS.FieldName = "ADJMTRS"
        Me.GADJMTRS.Name = "GADJMTRS"
        Me.GADJMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GADJMTRS.Visible = True
        Me.GADJMTRS.VisibleIndex = 8
        Me.GADJMTRS.Width = 100
        '
        'GFROMNO
        '
        Me.GFROMNO.Caption = "From No"
        Me.GFROMNO.DisplayFormat.FormatString = "0"
        Me.GFROMNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GFROMNO.FieldName = "FROMNO"
        Me.GFROMNO.Name = "GFROMNO"
        Me.GFROMNO.OptionsColumn.AllowEdit = False
        Me.GFROMNO.Width = 70
        '
        'GFROMTYPE
        '
        Me.GFROMTYPE.Caption = "From Ttype"
        Me.GFROMTYPE.FieldName = "FROMTYPE"
        Me.GFROMTYPE.Name = "GFROMTYPE"
        Me.GFROMTYPE.OptionsColumn.AllowEdit = False
        Me.GFROMTYPE.Width = 100
        '
        'CHKDONE
        '
        Me.CHKDONE.AutoHeight = False
        Me.CHKDONE.Name = "CHKDONE"
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GRIDLOTDETAILS
        Me.GridView1.Name = "GridView1"
        '
        'LBLTOTALBALMTRS
        '
        Me.LBLTOTALBALMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALBALMTRS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALBALMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALBALMTRS.Location = New System.Drawing.Point(910, 612)
        Me.LBLTOTALBALMTRS.Name = "LBLTOTALBALMTRS"
        Me.LBLTOTALBALMTRS.Size = New System.Drawing.Size(74, 15)
        Me.LBLTOTALBALMTRS.TabIndex = 691
        Me.LBLTOTALBALMTRS.Text = "0"
        Me.LBLTOTALBALMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALLOTBALMTRS
        '
        Me.LBLTOTALLOTBALMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALLOTBALMTRS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALLOTBALMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALLOTBALMTRS.Location = New System.Drawing.Point(848, 275)
        Me.LBLTOTALLOTBALMTRS.Name = "LBLTOTALLOTBALMTRS"
        Me.LBLTOTALLOTBALMTRS.Size = New System.Drawing.Size(86, 15)
        Me.LBLTOTALLOTBALMTRS.TabIndex = 690
        Me.LBLTOTALLOTBALMTRS.Text = "0"
        Me.LBLTOTALLOTBALMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALADJMTRS
        '
        Me.LBLTOTALADJMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALADJMTRS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALADJMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALADJMTRS.Location = New System.Drawing.Point(1022, 612)
        Me.LBLTOTALADJMTRS.Name = "LBLTOTALADJMTRS"
        Me.LBLTOTALADJMTRS.Size = New System.Drawing.Size(74, 15)
        Me.LBLTOTALADJMTRS.TabIndex = 689
        Me.LBLTOTALADJMTRS.Text = "0"
        Me.LBLTOTALADJMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(758, 612)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 14)
        Me.Label5.TabIndex = 687
        Me.Label5.Text = "Total"
        '
        'LBLTOTALMTRS
        '
        Me.LBLTOTALMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALMTRS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALMTRS.Location = New System.Drawing.Point(808, 612)
        Me.LBLTOTALMTRS.Name = "LBLTOTALMTRS"
        Me.LBLTOTALMTRS.Size = New System.Drawing.Size(74, 15)
        Me.LBLTOTALMTRS.TabIndex = 686
        Me.LBLTOTALMTRS.Text = "0"
        Me.LBLTOTALMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALLOTADJMTRS
        '
        Me.LBLTOTALLOTADJMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALLOTADJMTRS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALLOTADJMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALLOTADJMTRS.Location = New System.Drawing.Point(946, 275)
        Me.LBLTOTALLOTADJMTRS.Name = "LBLTOTALLOTADJMTRS"
        Me.LBLTOTALLOTADJMTRS.Size = New System.Drawing.Size(86, 15)
        Me.LBLTOTALLOTADJMTRS.TabIndex = 685
        Me.LBLTOTALLOTADJMTRS.Text = "0"
        Me.LBLTOTALLOTADJMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(716, 274)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 14)
        Me.Label10.TabIndex = 682
        Me.Label10.Text = "Total"
        '
        'LBLTOTALLOTMTRS
        '
        Me.LBLTOTALLOTMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALLOTMTRS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALLOTMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALLOTMTRS.Location = New System.Drawing.Point(750, 275)
        Me.LBLTOTALLOTMTRS.Name = "LBLTOTALLOTMTRS"
        Me.LBLTOTALLOTMTRS.Size = New System.Drawing.Size(86, 15)
        Me.LBLTOTALLOTMTRS.TabIndex = 681
        Me.LBLTOTALLOTMTRS.Text = "0"
        Me.LBLTOTALLOTMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.FlatAppearance.BorderSize = 0
        Me.CMDCLEAR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.Black
        Me.CMDCLEAR.Location = New System.Drawing.Point(216, 605)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 8
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(32, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 14)
        Me.Label7.TabIndex = 658
        Me.Label7.Text = "Dyeing Name"
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(113, 7)
        Me.CMBNAME.MaxDropDownItems = 14
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(323, 22)
        Me.CMBNAME.TabIndex = 1
        '
        'LOTTAGDATE
        '
        Me.LOTTAGDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LOTTAGDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.LOTTAGDATE.Location = New System.Drawing.Point(987, 7)
        Me.LOTTAGDATE.Name = "LOTTAGDATE"
        Me.LOTTAGDATE.Size = New System.Drawing.Size(87, 23)
        Me.LOTTAGDATE.TabIndex = 0
        Me.LOTTAGDATE.TabStop = False
        '
        'TXTLOTTAGNO
        '
        Me.TXTLOTTAGNO.BackColor = System.Drawing.Color.Linen
        Me.TXTLOTTAGNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLOTTAGNO.Location = New System.Drawing.Point(847, 7)
        Me.TXTLOTTAGNO.Name = "TXTLOTTAGNO"
        Me.TXTLOTTAGNO.ReadOnly = True
        Me.TXTLOTTAGNO.Size = New System.Drawing.Size(87, 23)
        Me.TXTLOTTAGNO.TabIndex = 0
        Me.TXTLOTTAGNO.TabStop = False
        Me.TXTLOTTAGNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLSRNO
        '
        Me.LBLSRNO.BackColor = System.Drawing.Color.Transparent
        Me.LBLSRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLSRNO.ForeColor = System.Drawing.Color.Black
        Me.LBLSRNO.Location = New System.Drawing.Point(743, 11)
        Me.LBLSRNO.Name = "LBLSRNO"
        Me.LBLSRNO.Size = New System.Drawing.Size(101, 15)
        Me.LBLSRNO.TabIndex = 634
        Me.LBLSRNO.Text = "Sr. No"
        Me.LBLSRNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(952, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 15)
        Me.Label9.TabIndex = 632
        Me.Label9.Text = "Date"
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(389, 605)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 10
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(129, 605)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 7
        Me.CMDOK.Text = "&Save"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'CMDDELETE
        '
        Me.CMDDELETE.BackColor = System.Drawing.Color.Transparent
        Me.CMDDELETE.FlatAppearance.BorderSize = 0
        Me.CMDDELETE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDELETE.ForeColor = System.Drawing.Color.Black
        Me.CMDDELETE.Location = New System.Drawing.Point(303, 605)
        Me.CMDDELETE.Name = "CMDDELETE"
        Me.CMDDELETE.Size = New System.Drawing.Size(80, 28)
        Me.CMDDELETE.TabIndex = 9
        Me.CMDDELETE.Text = "&Delete"
        Me.CMDDELETE.UseVisualStyleBackColor = False
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'tstxtbillno
        '
        Me.tstxtbillno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstxtbillno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstxtbillno.Location = New System.Drawing.Point(240, 1)
        Me.tstxtbillno.Name = "tstxtbillno"
        Me.tstxtbillno.Size = New System.Drawing.Size(56, 22)
        Me.tstxtbillno.TabIndex = 616
        Me.tstxtbillno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'PSRNO
        '
        Me.PSRNO.Caption = "Sr.No"
        Me.PSRNO.FieldName = "SRNO"
        Me.PSRNO.Name = "PSRNO"
        Me.PSRNO.OptionsColumn.AllowEdit = False
        Me.PSRNO.Width = 50
        '
        'GSRNO
        '
        Me.GSRNO.Caption = "Sr.No"
        Me.GSRNO.FieldName = "SRNO"
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.OptionsColumn.AllowEdit = False
        Me.GSRNO.Width = 50
        '
        'LotTagging
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1143, 667)
        Me.Controls.Add(Me.tstxtbillno)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "LotTagging"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Lot Tagging"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDPROGRAMDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDPROGRAM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDLOTDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDLOT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents PRINTTOOLSTRIP As ToolStripButton
    Friend WithEvents TOOLDELETE As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents TOOLPREVIOUS As ToolStripButton
    Friend WithEvents TOOLNEXT As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents Label10 As Label
    Friend WithEvents LBLTOTALLOTMTRS As Label
    Friend WithEvents CMDCLEAR As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents CMBNAME As ComboBox
    Friend WithEvents LOTTAGDATE As DateTimePicker
    Friend WithEvents TXTLOTTAGNO As TextBox
    Friend WithEvents LBLSRNO As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDOK As Button
    Friend WithEvents CMDDELETE As Button
    Friend WithEvents LBLTOTALADJMTRS As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents LBLTOTALMTRS As Label
    Friend WithEvents LBLTOTALLOTADJMTRS As Label
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents LBLTOTALBALMTRS As Label
    Friend WithEvents LBLTOTALLOTBALMTRS As Label
    Friend WithEvents tstxtbillno As TextBox
    Private WithEvents GRIDLOTDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDLOT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GLRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLOTNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLOTDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GITEMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBALMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GADJMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFROMNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFROMTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKDONE As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GRIDPROGRAMDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDPROGRAM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PITEMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PDESIGN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSHADE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PBALMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PADJMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PFROMNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PFROMSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PFROMTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PGRIDSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRIDSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents PCHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKPENDINGLOT As CheckBox
    Friend WithEvents GPROGDATE As DevExpress.XtraGrid.Columns.GridColumn
End Class
