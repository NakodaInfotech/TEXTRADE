<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectItemSampleOrder
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
        Me.CMDSHOWSTOCK = New System.Windows.Forms.Button()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GDESIGNNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALBOOKLET = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPENDINGBOOKET = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GORDERBOOKLET = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label7)
        Me.BlendPanel1.Controls.Add(Me.CMDSHOWSTOCK)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(601, 485)
        Me.BlendPanel1.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(21, 440)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(261, 28)
        Me.Label7.TabIndex = 563
        Me.Label7.Text = "Press Space to Copy Data from Previous Line" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Press Delete to Remove Data from Cur" &
    "rent Line"
        '
        'CMDSHOWSTOCK
        '
        Me.CMDSHOWSTOCK.Location = New System.Drawing.Point(475, 440)
        Me.CMDSHOWSTOCK.Name = "CMDSHOWSTOCK"
        Me.CMDSHOWSTOCK.Size = New System.Drawing.Size(80, 28)
        Me.CMDSHOWSTOCK.TabIndex = 3
        Me.CMDSHOWSTOCK.Text = "&Show Stock"
        Me.CMDSHOWSTOCK.UseVisualStyleBackColor = True
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(15, 12)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.Size = New System.Drawing.Size(559, 422)
        Me.gridbilldetails.TabIndex = 0
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GDESIGNNO, Me.GTOTALBOOKLET, Me.GPENDINGBOOKET, Me.GORDERBOOKLET})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsCustomization.AllowColumnMoving = False
        Me.gridbill.OptionsCustomization.AllowGroup = False
        Me.gridbill.OptionsCustomization.AllowQuickHideColumns = False
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GDESIGNNO
        '
        Me.GDESIGNNO.Caption = "Design No"
        Me.GDESIGNNO.FieldName = "DESIGNNO"
        Me.GDESIGNNO.Name = "GDESIGNNO"
        Me.GDESIGNNO.OptionsColumn.AllowEdit = False
        Me.GDESIGNNO.OptionsColumn.ReadOnly = True
        Me.GDESIGNNO.OptionsColumn.TabStop = False
        Me.GDESIGNNO.Visible = True
        Me.GDESIGNNO.VisibleIndex = 0
        Me.GDESIGNNO.Width = 200
        '
        'GTOTALBOOKLET
        '
        Me.GTOTALBOOKLET.Caption = "Total Booklet"
        Me.GTOTALBOOKLET.DisplayFormat.FormatString = "0"
        Me.GTOTALBOOKLET.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALBOOKLET.FieldName = "TOTALBOOKLET"
        Me.GTOTALBOOKLET.Name = "GTOTALBOOKLET"
        Me.GTOTALBOOKLET.OptionsColumn.AllowEdit = False
        Me.GTOTALBOOKLET.OptionsColumn.ReadOnly = True
        Me.GTOTALBOOKLET.OptionsColumn.TabStop = False
        Me.GTOTALBOOKLET.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALBOOKLET.Visible = True
        Me.GTOTALBOOKLET.VisibleIndex = 1
        Me.GTOTALBOOKLET.Width = 100
        '
        'GPENDINGBOOKET
        '
        Me.GPENDINGBOOKET.Caption = "Pending Booklet"
        Me.GPENDINGBOOKET.FieldName = "PENDINGBOOKLET"
        Me.GPENDINGBOOKET.Name = "GPENDINGBOOKET"
        Me.GPENDINGBOOKET.OptionsColumn.AllowEdit = False
        Me.GPENDINGBOOKET.OptionsColumn.ReadOnly = True
        Me.GPENDINGBOOKET.OptionsColumn.TabStop = False
        Me.GPENDINGBOOKET.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GPENDINGBOOKET.Visible = True
        Me.GPENDINGBOOKET.VisibleIndex = 2
        Me.GPENDINGBOOKET.Width = 100
        '
        'GORDERBOOKLET
        '
        Me.GORDERBOOKLET.Caption = "Order Booklet"
        Me.GORDERBOOKLET.DisplayFormat.FormatString = "0"
        Me.GORDERBOOKLET.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GORDERBOOKLET.FieldName = "ORDERBOOKLET"
        Me.GORDERBOOKLET.Name = "GORDERBOOKLET"
        Me.GORDERBOOKLET.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GORDERBOOKLET.Visible = True
        Me.GORDERBOOKLET.VisibleIndex = 3
        Me.GORDERBOOKLET.Width = 100
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Location = New System.Drawing.Point(389, 440)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 2
        Me.CMDEXIT.Text = "&Exit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDOK
        '
        Me.CMDOK.Location = New System.Drawing.Point(303, 440)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 1
        Me.CMDOK.Text = "&OK"
        Me.CMDOK.UseVisualStyleBackColor = True
        '
        'SelectItemSampleOrder
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(601, 485)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "SelectItemSampleOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Item Sample Order"
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents Label7 As Label
    Friend WithEvents CMDSHOWSTOCK As Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GDESIGNNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALBOOKLET As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPENDINGBOOKET As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GORDERBOOKLET As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDOK As Button
End Class
