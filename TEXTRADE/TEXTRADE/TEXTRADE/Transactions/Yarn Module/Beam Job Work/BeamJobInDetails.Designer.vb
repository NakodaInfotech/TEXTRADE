<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BeamJobInDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BeamJobInDetails))
        Me.cmdok = New System.Windows.Forms.Button()
        Me.miniToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLGRIDDETAILS = New System.Windows.Forms.ToolStripLabel()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.gsrno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TXTBEAMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMBNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMBGODOWN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TXTBEAMJONO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMBJONO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TXTMILLNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DTBEAMJODATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TXTTOTALMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TXTWEIGHT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TXTGAMANO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TXTSECTION = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TXTROLLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TXTBREAKAGE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TXTREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.CMDEDIT = New System.Windows.Forms.Button()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BlendPanel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(533, 550)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 2
        Me.cmdok.Text = "&Ok"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'miniToolStrip
        '
        Me.miniToolStrip.AccessibleName = "New item selection"
        Me.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.Location = New System.Drawing.Point(216, 3)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(1234, 25)
        Me.miniToolStrip.TabIndex = 255
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
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
        'TOOLREFRESH
        '
        Me.TOOLREFRESH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLREFRESH.Image = Global.TEXTRADE.My.Resources.Resources.refresh1
        Me.TOOLREFRESH.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLREFRESH.Name = "TOOLREFRESH"
        Me.TOOLREFRESH.Size = New System.Drawing.Size(23, 22)
        Me.TOOLREFRESH.Text = "&Refresh"
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
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'TOOLGRIDDETAILS
        '
        Me.TOOLGRIDDETAILS.Name = "TOOLGRIDDETAILS"
        Me.TOOLGRIDDETAILS.Size = New System.Drawing.Size(67, 22)
        Me.TOOLGRIDDETAILS.Text = "Grid Details"
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(621, 550)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 3
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(14, 69)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemTextEdit1})
        Me.gridbilldetails.Size = New System.Drawing.Size(1207, 475)
        Me.gridbilldetails.TabIndex = 1
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill, Me.GridView1})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHK, Me.gsrno, Me.TXTBEAMNAME, Me.CMBNAME, Me.CMBGODOWN, Me.TXTBEAMJONO, Me.CMBJONO, Me.TXTMILLNAME, Me.DTBEAMJODATE, Me.TXTTOTALMTRS, Me.TXTWEIGHT, Me.TXTGAMANO, Me.TXTSECTION, Me.TXTROLLNO, Me.TXTBREAKAGE, Me.TXTREMARKS})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        '
        'GCHK
        '
        Me.GCHK.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GCHK.FieldName = "CHK"
        Me.GCHK.Name = "GCHK"
        Me.GCHK.Visible = True
        Me.GCHK.VisibleIndex = 0
        Me.GCHK.Width = 30
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = ""
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'gsrno
        '
        Me.gsrno.Caption = "Sr. No"
        Me.gsrno.FieldName = "SRNO"
        Me.gsrno.Name = "gsrno"
        Me.gsrno.OptionsColumn.AllowEdit = False
        Me.gsrno.Visible = True
        Me.gsrno.VisibleIndex = 1
        '
        'TXTBEAMNAME
        '
        Me.TXTBEAMNAME.Caption = "Beam Name"
        Me.TXTBEAMNAME.FieldName = "TXTBEAMNAME"
        Me.TXTBEAMNAME.Name = "TXTBEAMNAME"
        Me.TXTBEAMNAME.OptionsColumn.AllowEdit = False
        Me.TXTBEAMNAME.Visible = True
        Me.TXTBEAMNAME.VisibleIndex = 2
        '
        'CMBNAME
        '
        Me.CMBNAME.Caption = "Name"
        Me.CMBNAME.FieldName = "CMBNAME"
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.OptionsColumn.AllowEdit = False
        Me.CMBNAME.Visible = True
        Me.CMBNAME.VisibleIndex = 3
        '
        'CMBGODOWN
        '
        Me.CMBGODOWN.Caption = "Godown"
        Me.CMBGODOWN.FieldName = "CMBGODOWN"
        Me.CMBGODOWN.Name = "CMBGODOWN"
        Me.CMBGODOWN.OptionsColumn.AllowEdit = False
        Me.CMBGODOWN.Visible = True
        Me.CMBGODOWN.VisibleIndex = 4
        '
        'TXTBEAMJONO
        '
        Me.TXTBEAMJONO.Caption = "Beam Jo No"
        Me.TXTBEAMJONO.FieldName = "TXTBEAMJONO"
        Me.TXTBEAMJONO.Name = "TXTBEAMJONO"
        Me.TXTBEAMJONO.OptionsColumn.AllowEdit = False
        Me.TXTBEAMJONO.Visible = True
        Me.TXTBEAMJONO.VisibleIndex = 5
        '
        'CMBJONO
        '
        Me.CMBJONO.Caption = "Jo No"
        Me.CMBJONO.FieldName = "CMBJONO"
        Me.CMBJONO.Name = "CMBJONO"
        Me.CMBJONO.OptionsColumn.AllowEdit = False
        Me.CMBJONO.Visible = True
        Me.CMBJONO.VisibleIndex = 6
        '
        'TXTMILLNAME
        '
        Me.TXTMILLNAME.Caption = "Mill Name"
        Me.TXTMILLNAME.FieldName = "TXTMILLNAME"
        Me.TXTMILLNAME.Name = "TXTMILLNAME"
        Me.TXTMILLNAME.OptionsColumn.AllowEdit = False
        Me.TXTMILLNAME.Visible = True
        Me.TXTMILLNAME.VisibleIndex = 7
        '
        'DTBEAMJODATE
        '
        Me.DTBEAMJODATE.Caption = "Beam Jo Date"
        Me.DTBEAMJODATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.DTBEAMJODATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DTBEAMJODATE.FieldName = "DTBEAMJODATE"
        Me.DTBEAMJODATE.Name = "DTBEAMJODATE"
        Me.DTBEAMJODATE.OptionsColumn.AllowEdit = False
        Me.DTBEAMJODATE.Visible = True
        Me.DTBEAMJODATE.VisibleIndex = 8
        Me.DTBEAMJODATE.Width = 127
        '
        'TXTTOTALMTRS
        '
        Me.TXTTOTALMTRS.Caption = "Total Mtrs"
        Me.TXTTOTALMTRS.FieldName = "TXTTOTALMTRS"
        Me.TXTTOTALMTRS.ImageOptions.ImageIndex = 0
        Me.TXTTOTALMTRS.Name = "TXTTOTALMTRS"
        Me.TXTTOTALMTRS.OptionsColumn.AllowEdit = False
        Me.TXTTOTALMTRS.Visible = True
        Me.TXTTOTALMTRS.VisibleIndex = 9
        Me.TXTTOTALMTRS.Width = 200
        '
        'TXTWEIGHT
        '
        Me.TXTWEIGHT.Caption = "Weight"
        Me.TXTWEIGHT.FieldName = "TXTWEIGHT"
        Me.TXTWEIGHT.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
        Me.TXTWEIGHT.Name = "TXTWEIGHT"
        Me.TXTWEIGHT.OptionsColumn.AllowEdit = False
        Me.TXTWEIGHT.Visible = True
        Me.TXTWEIGHT.VisibleIndex = 10
        Me.TXTWEIGHT.Width = 100
        '
        'TXTGAMANO
        '
        Me.TXTGAMANO.Caption = "Gama No"
        Me.TXTGAMANO.FieldName = "TXTGAMANO"
        Me.TXTGAMANO.Name = "TXTGAMANO"
        Me.TXTGAMANO.OptionsColumn.AllowEdit = False
        Me.TXTGAMANO.Visible = True
        Me.TXTGAMANO.VisibleIndex = 11
        Me.TXTGAMANO.Width = 60
        '
        'TXTSECTION
        '
        Me.TXTSECTION.Caption = "Section "
        Me.TXTSECTION.FieldName = "TXTSECTION"
        Me.TXTSECTION.Name = "TXTSECTION"
        Me.TXTSECTION.OptionsColumn.AllowEdit = False
        Me.TXTSECTION.Visible = True
        Me.TXTSECTION.VisibleIndex = 12
        Me.TXTSECTION.Width = 120
        '
        'TXTROLLNO
        '
        Me.TXTROLLNO.Caption = "Roll No"
        Me.TXTROLLNO.DisplayFormat.FormatString = "0"
        Me.TXTROLLNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TXTROLLNO.FieldName = "TXTROLLNO"
        Me.TXTROLLNO.Name = "TXTROLLNO"
        Me.TXTROLLNO.OptionsColumn.AllowEdit = False
        Me.TXTROLLNO.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.TXTROLLNO.Visible = True
        Me.TXTROLLNO.VisibleIndex = 13
        '
        'TXTBREAKAGE
        '
        Me.TXTBREAKAGE.Caption = "Breakage"
        Me.TXTBREAKAGE.DisplayFormat.FormatString = "0.00"
        Me.TXTBREAKAGE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TXTBREAKAGE.FieldName = "TXTBREAKAGE"
        Me.TXTBREAKAGE.Name = "TXTBREAKAGE"
        Me.TXTBREAKAGE.OptionsColumn.AllowEdit = False
        Me.TXTBREAKAGE.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.TXTBREAKAGE.Visible = True
        Me.TXTBREAKAGE.VisibleIndex = 14
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.Caption = "Remarks"
        Me.TXTREMARKS.FieldName = "TXTREMARKS"
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.OptionsColumn.AllowEdit = False
        Me.TXTREMARKS.Visible = True
        Me.TXTREMARKS.VisibleIndex = 15
        Me.TXTREMARKS.Width = 150
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.gridbilldetails
        Me.GridView1.Name = "GridView1"
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMDEDIT)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.toolStripSeparator, Me.TOOLREFRESH, Me.ToolStripButton2, Me.PrintToolStripButton, Me.ToolStripSeparator1, Me.TOOLGRIDDETAILS})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 255
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'CMDEDIT
        '
        Me.CMDEDIT.Location = New System.Drawing.Point(707, 550)
        Me.CMDEDIT.Name = "CMDEDIT"
        Me.CMDEDIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEDIT.TabIndex = 256
        Me.CMDEDIT.Text = "&Edit"
        Me.CMDEDIT.UseVisualStyleBackColor = True
        '
        'BeamJobInDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Name = "BeamJobInDetails"
        Me.Text = "BeamJobInDetails"
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdok As Button
    Friend WithEvents miniToolStrip As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents TOOLREFRESH As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TOOLGRIDDETAILS As ToolStripLabel
    Friend WithEvents cmdexit As Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents TXTBEAMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gsrno As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TXTMILLNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TXTTOTALMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TXTWEIGHT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TXTGAMANO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TXTSECTION As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DTBEAMJODATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TXTROLLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TXTBREAKAGE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TXTREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents CMBGODOWN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMBNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TXTBEAMJONO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMBJONO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMDEDIT As Button
End Class
