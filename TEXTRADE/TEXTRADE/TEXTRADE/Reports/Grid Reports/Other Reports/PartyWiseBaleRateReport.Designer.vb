<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PartyWiseBaleRateReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PartyWiseBaleRateReport))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDEDIT = New System.Windows.Forms.Button()
        Me.DTPARTYBILLDATE = New System.Windows.Forms.MaskedTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPARTYNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTRANSPORT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNOOFBALES = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAMOUNT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.TOOLEXCELL = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        resources.ApplyResources(Me.BlendPanel1, "BlendPanel1")
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDEDIT)
        Me.BlendPanel1.Controls.Add(Me.DTPARTYBILLDATE)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Name = "BlendPanel1"
        '
        'CMDEXIT
        '
        resources.ApplyResources(Me.CMDEXIT, "CMDEXIT")
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDEDIT
        '
        resources.ApplyResources(Me.CMDEDIT, "CMDEDIT")
        Me.CMDEDIT.Name = "CMDEDIT"
        Me.CMDEDIT.UseVisualStyleBackColor = True
        '
        'DTPARTYBILLDATE
        '
        Me.DTPARTYBILLDATE.AsciiOnly = True
        Me.DTPARTYBILLDATE.BackColor = System.Drawing.Color.LemonChiffon
        resources.ApplyResources(Me.DTPARTYBILLDATE, "DTPARTYBILLDATE")
        Me.DTPARTYBILLDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTPARTYBILLDATE.Name = "DTPARTYBILLDATE"
        Me.DTPARTYBILLDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTPARTYBILLDATE.ValidatingType = GetType(Date)
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Name = "Label9"
        '
        'gridbilldetails
        '
        resources.ApplyResources(Me.gridbilldetails, "gridbilldetails")
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill, Me.GridView1})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = CType(resources.GetObject("gridbill.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = CType(resources.GetObject("gridbill.Appearance.Row.Font"), System.Drawing.Font)
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GDATE, Me.GPARTYNAME, Me.GTRANSPORT, Me.GRATE, Me.GNOOFBALES, Me.GAMOUNT})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.AutoExpandAllGroups = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsCustomization.AllowRowSizing = True
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        '
        'GDATE
        '
        resources.ApplyResources(Me.GDATE, "GDATE")
        Me.GDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GDATE.FieldName = "DATE"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.OptionsColumn.AllowEdit = False
        '
        'GPARTYNAME
        '
        resources.ApplyResources(Me.GPARTYNAME, "GPARTYNAME")
        Me.GPARTYNAME.FieldName = "PARTYNAME"
        Me.GPARTYNAME.Name = "GPARTYNAME"
        Me.GPARTYNAME.OptionsColumn.AllowEdit = False
        '
        'GTRANSPORT
        '
        resources.ApplyResources(Me.GTRANSPORT, "GTRANSPORT")
        Me.GTRANSPORT.FieldName = "TRANSPORT"
        Me.GTRANSPORT.Name = "GTRANSPORT"
        Me.GTRANSPORT.OptionsColumn.AllowEdit = False
        '
        'GRATE
        '
        resources.ApplyResources(Me.GRATE, "GRATE")
        Me.GRATE.FieldName = "RATE"
        Me.GRATE.Name = "GRATE"
        '
        'GNOOFBALES
        '
        resources.ApplyResources(Me.GNOOFBALES, "GNOOFBALES")
        Me.GNOOFBALES.FieldName = "NOOFBALES"
        Me.GNOOFBALES.Name = "GNOOFBALES"
        '
        'GAMOUNT
        '
        resources.ApplyResources(Me.GAMOUNT, "GAMOUNT")
        Me.GAMOUNT.FieldName = "AMOUNT"
        Me.GAMOUNT.Name = "GAMOUNT"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.gridbilldetails
        Me.GridView1.Name = "GridView1"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TOOLEXCELL, Me.toolStripSeparator, Me.TOOLREFRESH})
        resources.ApplyResources(Me.ToolStrip1, "ToolStrip1")
        Me.ToolStrip1.Name = "ToolStrip1"
        '
        'TOOLEXCELL
        '
        Me.TOOLEXCELL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLEXCELL.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        resources.ApplyResources(Me.TOOLEXCELL, "TOOLEXCELL")
        Me.TOOLEXCELL.Name = "TOOLEXCELL"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        resources.ApplyResources(Me.toolStripSeparator, "toolStripSeparator")
        '
        'TOOLREFRESH
        '
        Me.TOOLREFRESH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.TOOLREFRESH, "TOOLREFRESH")
        Me.TOOLREFRESH.Name = "TOOLREFRESH"
        '
        'PartyWiseBaleRateReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.BlendPanel1)
        Me.KeyPreview = True
        Me.Name = "PartyWiseBaleRateReport"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDEDIT As Button
    Friend WithEvents DTPARTYBILLDATE As MaskedTextBox
    Friend WithEvents Label9 As Label
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPARTYNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTRANSPORT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents TOOLEXCELL As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents TOOLREFRESH As ToolStripButton
    Friend WithEvents GRATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNOOFBALES As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAMOUNT As DevExpress.XtraGrid.Columns.GridColumn
End Class
