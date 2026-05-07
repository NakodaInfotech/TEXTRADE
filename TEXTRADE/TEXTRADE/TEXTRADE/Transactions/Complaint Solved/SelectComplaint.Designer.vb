<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectComplaint
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
        Me.CMDEXCEL = New System.Windows.Forms.Button()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOMPLAINT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOMPLAINTBY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOMPLAINTDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBUYERNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREGISTER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBILLINITIALS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.GSELLERNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMDEXCEL)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 3
        '
        'CMDEXCEL
        '
        Me.CMDEXCEL.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXCEL.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXCEL.FlatAppearance.BorderSize = 0
        Me.CMDEXCEL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXCEL.ForeColor = System.Drawing.Color.Black
        Me.CMDEXCEL.Location = New System.Drawing.Point(688, 545)
        Me.CMDEXCEL.Name = "CMDEXCEL"
        Me.CMDEXCEL.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXCEL.TabIndex = 5
        Me.CMDEXCEL.Text = "Excel"
        Me.CMDEXCEL.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.gridbilldetails.Location = New System.Drawing.Point(15, 12)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.Size = New System.Drawing.Size(1255, 527)
        Me.gridbilldetails.TabIndex = 4
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GBILLNO, Me.GCOMPLAINT, Me.GCOMPLAINTDATE, Me.GCOMPLAINTBY, Me.GBUYERNAME, Me.GSELLERNAME, Me.GBILLINITIALS, Me.GREGISTER, Me.GTYPE})
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
        'GBILLNO
        '
        Me.GBILLNO.Caption = "Bill No"
        Me.GBILLNO.FieldName = "BILLNO"
        Me.GBILLNO.Name = "GBILLNO"
        Me.GBILLNO.Visible = True
        Me.GBILLNO.VisibleIndex = 1
        Me.GBILLNO.Width = 60
        '
        'GCOMPLAINT
        '
        Me.GCOMPLAINT.Caption = "Complaint"
        Me.GCOMPLAINT.FieldName = "COMPLAINT"
        Me.GCOMPLAINT.Name = "GCOMPLAINT"
        Me.GCOMPLAINT.Visible = True
        Me.GCOMPLAINT.VisibleIndex = 2
        Me.GCOMPLAINT.Width = 200
        '
        'GCOMPLAINTBY
        '
        Me.GCOMPLAINTBY.Caption = "Complaint By"
        Me.GCOMPLAINTBY.FieldName = "COMPLAINTBY"
        Me.GCOMPLAINTBY.Name = "GCOMPLAINTBY"
        Me.GCOMPLAINTBY.Visible = True
        Me.GCOMPLAINTBY.VisibleIndex = 3
        Me.GCOMPLAINTBY.Width = 120
        '
        'GCOMPLAINTDATE
        '
        Me.GCOMPLAINTDATE.Caption = "Complaint Date"
        Me.GCOMPLAINTDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GCOMPLAINTDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GCOMPLAINTDATE.FieldName = "COMPLAINTDATE"
        Me.GCOMPLAINTDATE.Name = "GCOMPLAINTDATE"
        Me.GCOMPLAINTDATE.Visible = True
        Me.GCOMPLAINTDATE.VisibleIndex = 5
        '
        'GBUYERNAME
        '
        Me.GBUYERNAME.Caption = "Buyer Name"
        Me.GBUYERNAME.FieldName = "BUYERNAME"
        Me.GBUYERNAME.Name = "GBUYERNAME"
        Me.GBUYERNAME.Visible = True
        Me.GBUYERNAME.VisibleIndex = 6
        Me.GBUYERNAME.Width = 200
        '
        'GREGISTER
        '
        Me.GREGISTER.Caption = "Register"
        Me.GREGISTER.FieldName = "REGISTER"
        Me.GREGISTER.Name = "GREGISTER"
        Me.GREGISTER.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GREGISTER.Visible = True
        Me.GREGISTER.VisibleIndex = 7
        Me.GREGISTER.Width = 150
        '
        'GBILLINITIALS
        '
        Me.GBILLINITIALS.Caption = "Bill Initials"
        Me.GBILLINITIALS.FieldName = "BILLINITIALS"
        Me.GBILLINITIALS.Name = "GBILLINITIALS"
        Me.GBILLINITIALS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GBILLINITIALS.Visible = True
        Me.GBILLINITIALS.VisibleIndex = 7
        Me.GBILLINITIALS.Width = 120
        '
        'GTYPE
        '
        Me.GTYPE.Caption = "Type"
        Me.GTYPE.FieldName = "TYPE"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.Visible = True
        Me.GTYPE.VisibleIndex = 9
        Me.GTYPE.Width = 150
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(516, 545)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 2
        Me.CMDOK.Text = "&Ok"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(602, 545)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 3
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'GSELLERNAME
        '
        Me.GSELLERNAME.Caption = "Seller Name"
        Me.GSELLERNAME.FieldName = "SELLERNAME"
        Me.GSELLERNAME.Name = "GSELLERNAME"
        Me.GSELLERNAME.Visible = True
        Me.GSELLERNAME.VisibleIndex = 4
        Me.GSELLERNAME.Width = 200
        '
        'SelectComplaint
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "SelectComplaint"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Select Complaint"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDEXCEL As Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOMPLAINT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOMPLAINTBY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOMPLAINTDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBUYERNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREGISTER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBILLINITIALS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMDOK As Button
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents GSELLERNAME As DevExpress.XtraGrid.Columns.GridColumn
End Class
