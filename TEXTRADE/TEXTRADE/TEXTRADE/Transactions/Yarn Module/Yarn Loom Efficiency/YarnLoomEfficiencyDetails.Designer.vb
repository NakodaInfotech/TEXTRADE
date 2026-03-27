<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class YarnLoomEfficiencyDetails
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
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GYARNQUALITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBEAMNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRPM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRECMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWEFT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPICKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEFFPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKDONE = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.TOOLEXCEL = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDEDIT = New System.Windows.Forms.Button()
        Me.CMDADD = New System.Windows.Forms.Button()
        Me.GLOOMNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAVGPICK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDEDIT)
        Me.BlendPanel1.Controls.Add(Me.CMDADD)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 17
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(16, 41)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKDONE})
        Me.gridbilldetails.Size = New System.Drawing.Size(1202, 494)
        Me.gridbilldetails.TabIndex = 321
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GSRNO, Me.GDATE, Me.GNAME, Me.GLOOMNO, Me.GYARNQUALITY, Me.GBEAMNO, Me.GRPM, Me.GPICKS, Me.GRECMTRS, Me.GWEFT, Me.GWARP, Me.GEFFPER, Me.GAVGPICK, Me.GREMARKS})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.AutoExpandAllGroups = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsCustomization.AllowRowSizing = True
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        '
        'GSRNO
        '
        Me.GSRNO.Caption = "Sr. No."
        Me.GSRNO.FieldName = "YLENO"
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.OptionsColumn.AllowEdit = False
        Me.GSRNO.Visible = True
        Me.GSRNO.VisibleIndex = 0
        Me.GSRNO.Width = 60
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
        Me.GDATE.VisibleIndex = 1
        '
        'GNAME
        '
        Me.GNAME.Caption = "Party Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.OptionsColumn.AllowEdit = False
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 3
        Me.GNAME.Width = 200
        '
        'GYARNQUALITY
        '
        Me.GYARNQUALITY.Caption = "Yarn Quality"
        Me.GYARNQUALITY.FieldName = "YARNQUALITY"
        Me.GYARNQUALITY.Name = "GYARNQUALITY"
        Me.GYARNQUALITY.OptionsColumn.AllowEdit = False
        Me.GYARNQUALITY.Visible = True
        Me.GYARNQUALITY.VisibleIndex = 4
        Me.GYARNQUALITY.Width = 250
        '
        'GBEAMNO
        '
        Me.GBEAMNO.Caption = "Beam No"
        Me.GBEAMNO.FieldName = "BEAMNO"
        Me.GBEAMNO.Name = "GBEAMNO"
        Me.GBEAMNO.OptionsColumn.AllowEdit = False
        Me.GBEAMNO.Visible = True
        Me.GBEAMNO.VisibleIndex = 5
        Me.GBEAMNO.Width = 120
        '
        'GRPM
        '
        Me.GRPM.Caption = "RPM"
        Me.GRPM.DisplayFormat.FormatString = "0.00"
        Me.GRPM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRPM.FieldName = "RPM"
        Me.GRPM.Name = "GRPM"
        Me.GRPM.OptionsColumn.AllowEdit = False
        Me.GRPM.Visible = True
        Me.GRPM.VisibleIndex = 6
        Me.GRPM.Width = 100
        '
        'GRECMTRS
        '
        Me.GRECMTRS.Caption = "Rec Mtrs"
        Me.GRECMTRS.DisplayFormat.FormatString = "0.00"
        Me.GRECMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRECMTRS.FieldName = "RECMTRS"
        Me.GRECMTRS.Name = "GRECMTRS"
        Me.GRECMTRS.OptionsColumn.AllowEdit = False
        Me.GRECMTRS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GRECMTRS.Visible = True
        Me.GRECMTRS.VisibleIndex = 7
        Me.GRECMTRS.Width = 100
        '
        'GWEFT
        '
        Me.GWEFT.Caption = "Weft"
        Me.GWEFT.DisplayFormat.FormatString = "0.00"
        Me.GWEFT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GWEFT.FieldName = "WEFT"
        Me.GWEFT.Name = "GWEFT"
        Me.GWEFT.OptionsColumn.AllowEdit = False
        Me.GWEFT.Visible = True
        Me.GWEFT.VisibleIndex = 8
        Me.GWEFT.Width = 100
        '
        'GPICKS
        '
        Me.GPICKS.Caption = "Picks"
        Me.GPICKS.DisplayFormat.FormatString = "0.00"
        Me.GPICKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPICKS.FieldName = "PICKS"
        Me.GPICKS.Name = "GPICKS"
        Me.GPICKS.OptionsColumn.AllowEdit = False
        Me.GPICKS.Visible = True
        Me.GPICKS.VisibleIndex = 9
        '
        'GWARP
        '
        Me.GWARP.Caption = "Warp"
        Me.GWARP.DisplayFormat.FormatString = "0.00"
        Me.GWARP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GWARP.FieldName = "WARP"
        Me.GWARP.Name = "GWARP"
        Me.GWARP.OptionsColumn.AllowEdit = False
        Me.GWARP.Visible = True
        Me.GWARP.VisibleIndex = 10
        Me.GWARP.Width = 100
        '
        'GEFFPER
        '
        Me.GEFFPER.Caption = "Efficiency Per"
        Me.GEFFPER.DisplayFormat.FormatString = "0.00"
        Me.GEFFPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GEFFPER.FieldName = "EFFPER"
        Me.GEFFPER.Name = "GEFFPER"
        Me.GEFFPER.OptionsColumn.AllowEdit = False
        Me.GEFFPER.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GEFFPER.Visible = True
        Me.GEFFPER.VisibleIndex = 11
        Me.GEFFPER.Width = 100
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "REMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.OptionsColumn.AllowEdit = False
        Me.GREMARKS.Visible = True
        Me.GREMARKS.VisibleIndex = 13
        Me.GREMARKS.Width = 200
        '
        'CHKDONE
        '
        Me.CHKDONE.AutoHeight = False
        Me.CHKDONE.Name = "CHKDONE"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TOOLEXCEL, Me.ToolStripSeparator2, Me.TOOLREFRESH, Me.ToolStripSeparator4})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'TOOLEXCEL
        '
        Me.TOOLEXCEL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLEXCEL.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        Me.TOOLEXCEL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLEXCEL.Name = "TOOLEXCEL"
        Me.TOOLEXCEL.Size = New System.Drawing.Size(23, 22)
        Me.TOOLEXCEL.Text = "Print"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'TOOLREFRESH
        '
        Me.TOOLREFRESH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLREFRESH.Image = Global.TEXTRADE.My.Resources.Resources.refresh1
        Me.TOOLREFRESH.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLREFRESH.Name = "TOOLREFRESH"
        Me.TOOLREFRESH.Size = New System.Drawing.Size(23, 22)
        Me.TOOLREFRESH.Text = "ToolStripButton1"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Location = New System.Drawing.Point(662, 541)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 2
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDEDIT
        '
        Me.CMDEDIT.Location = New System.Drawing.Point(577, 541)
        Me.CMDEDIT.Name = "CMDEDIT"
        Me.CMDEDIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEDIT.TabIndex = 1
        Me.CMDEDIT.Text = "&Edit"
        Me.CMDEDIT.UseVisualStyleBackColor = True
        '
        'CMDADD
        '
        Me.CMDADD.Location = New System.Drawing.Point(492, 541)
        Me.CMDADD.Name = "CMDADD"
        Me.CMDADD.Size = New System.Drawing.Size(80, 28)
        Me.CMDADD.TabIndex = 0
        Me.CMDADD.Text = "&Add New"
        Me.CMDADD.UseVisualStyleBackColor = True
        '
        'GLOOMNO
        '
        Me.GLOOMNO.Caption = "Loom No"
        Me.GLOOMNO.FieldName = "LOOMNO"
        Me.GLOOMNO.Name = "GLOOMNO"
        Me.GLOOMNO.OptionsColumn.AllowEdit = False
        Me.GLOOMNO.Visible = True
        Me.GLOOMNO.VisibleIndex = 2
        '
        'GAVGPICK
        '
        Me.GAVGPICK.Caption = "Average Pick"
        Me.GAVGPICK.DisplayFormat.FormatString = "0.00"
        Me.GAVGPICK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GAVGPICK.FieldName = "AVGPICK"
        Me.GAVGPICK.Name = "GAVGPICK"
        Me.GAVGPICK.OptionsColumn.AllowEdit = False
        Me.GAVGPICK.Visible = True
        Me.GAVGPICK.VisibleIndex = 12
        '
        'YarnLoomEfficiencyDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "YarnLoomEfficiencyDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Yarn Loom Efficiency Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GYARNQUALITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBEAMNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRPM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRECMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWEFT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPICKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEFFPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKDONE As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents TOOLEXCEL As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents TOOLREFRESH As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDEDIT As Button
    Friend WithEvents CMDADD As Button
    Friend WithEvents GLOOMNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAVGPICK As DevExpress.XtraGrid.Columns.GridColumn
End Class
