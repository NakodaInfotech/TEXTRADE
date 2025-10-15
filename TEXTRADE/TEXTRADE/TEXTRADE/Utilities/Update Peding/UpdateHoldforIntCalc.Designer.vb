<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UpdateHoldforIntCalc
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UpdateHoldforIntCalc))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.RBPENDING = New System.Windows.Forms.RadioButton()
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.RBENTERED = New System.Windows.Forms.RadioButton()
        Me.CMDSAVE = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GENTRYTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GENTRYNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDELIVERYAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.GREGNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        resources.ApplyResources(Me.BlendPanel1, "BlendPanel1")
        Me.BlendPanel1.Controls.Add(Me.RBPENDING)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.RBENTERED)
        Me.BlendPanel1.Controls.Add(Me.CMDSAVE)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Name = "BlendPanel1"
        '
        'RBPENDING
        '
        resources.ApplyResources(Me.RBPENDING, "RBPENDING")
        Me.RBPENDING.BackColor = System.Drawing.Color.Transparent
        Me.RBPENDING.Checked = True
        Me.RBPENDING.Name = "RBPENDING"
        Me.RBPENDING.TabStop = True
        Me.RBPENDING.UseVisualStyleBackColor = False
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        resources.ApplyResources(Me.CMDREFRESH, "CMDREFRESH")
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.Black
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.UseVisualStyleBackColor = False
        '
        'RBENTERED
        '
        resources.ApplyResources(Me.RBENTERED, "RBENTERED")
        Me.RBENTERED.BackColor = System.Drawing.Color.Transparent
        Me.RBENTERED.Name = "RBENTERED"
        Me.RBENTERED.UseVisualStyleBackColor = False
        '
        'CMDSAVE
        '
        Me.CMDSAVE.BackColor = System.Drawing.Color.Transparent
        Me.CMDSAVE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSAVE.FlatAppearance.BorderSize = 0
        resources.ApplyResources(Me.CMDSAVE, "CMDSAVE")
        Me.CMDSAVE.ForeColor = System.Drawing.Color.Black
        Me.CMDSAVE.Name = "CMDSAVE"
        Me.CMDSAVE.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        resources.ApplyResources(Me.cmdexit, "cmdexit")
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        resources.ApplyResources(Me.gridbilldetails, "gridbilldetails")
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GENTRYTYPE, Me.GENTRYNO, Me.GDATE, Me.GNAME, Me.GDELIVERYAT, Me.GTOTALPCS, Me.GTOTALMTRS, Me.GGRANDTOTAL, Me.GREGNAME})
        Me.gridbill.CustomizationFormBounds = New System.Drawing.Rectangle(688, 311, 208, 184)
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AutoExpandAllGroups = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsSelection.CheckBoxSelectorColumnWidth = 40
        Me.gridbill.OptionsSelection.MultiSelect = True
        Me.gridbill.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.gridbill.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.[True]
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GENTRYTYPE
        '
        resources.ApplyResources(Me.GENTRYTYPE, "GENTRYTYPE")
        Me.GENTRYTYPE.FieldName = "ENTRYTYPE"
        Me.GENTRYTYPE.Name = "GENTRYTYPE"
        '
        'GENTRYNO
        '
        resources.ApplyResources(Me.GENTRYNO, "GENTRYNO")
        Me.GENTRYNO.FieldName = "ENTRYNO"
        Me.GENTRYNO.Name = "GENTRYNO"
        '
        'GDATE
        '
        resources.ApplyResources(Me.GDATE, "GDATE")
        Me.GDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GDATE.FieldName = "DATE"
        Me.GDATE.Name = "GDATE"
        '
        'GNAME
        '
        resources.ApplyResources(Me.GNAME, "GNAME")
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        '
        'GDELIVERYAT
        '
        resources.ApplyResources(Me.GDELIVERYAT, "GDELIVERYAT")
        Me.GDELIVERYAT.FieldName = "DELIVERYAT"
        Me.GDELIVERYAT.Name = "GDELIVERYAT"
        '
        'GTOTALPCS
        '
        resources.ApplyResources(Me.GTOTALPCS, "GTOTALPCS")
        Me.GTOTALPCS.DisplayFormat.FormatString = "0"
        Me.GTOTALPCS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALPCS.FieldName = "TOTALPCS"
        Me.GTOTALPCS.Name = "GTOTALPCS"
        '
        'GTOTALMTRS
        '
        resources.ApplyResources(Me.GTOTALMTRS, "GTOTALMTRS")
        Me.GTOTALMTRS.DisplayFormat.FormatString = "0.00"
        Me.GTOTALMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALMTRS.FieldName = "TOTALMTRS"
        Me.GTOTALMTRS.Name = "GTOTALMTRS"
        '
        'GGRANDTOTAL
        '
        resources.ApplyResources(Me.GGRANDTOTAL, "GGRANDTOTAL")
        Me.GGRANDTOTAL.DisplayFormat.FormatString = "0.00"
        Me.GGRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.GGRANDTOTAL.Name = "GGRANDTOTAL"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripButton})
        resources.ApplyResources(Me.ToolStrip1, "ToolStrip1")
        Me.ToolStrip1.Name = "ToolStrip1"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        resources.ApplyResources(Me.PrintToolStripButton, "PrintToolStripButton")
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        '
        'GREGNAME
        '
        resources.ApplyResources(Me.GREGNAME, "GREGNAME")
        Me.GREGNAME.FieldName = "REGNAME"
        Me.GREGNAME.Name = "GREGNAME"
        '
        'UpdateHoldforIntCalc
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.BlendPanel1)
        Me.KeyPreview = True
        Me.Name = "UpdateHoldforIntCalc"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDREFRESH As Button
    Friend WithEvents RBENTERED As RadioButton
    Friend WithEvents CMDSAVE As Button
    Friend WithEvents cmdexit As Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GENTRYNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDELIVERYAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents RBPENDING As RadioButton
    Friend WithEvents GENTRYTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREGNAME As DevExpress.XtraGrid.Columns.GridColumn
End Class
