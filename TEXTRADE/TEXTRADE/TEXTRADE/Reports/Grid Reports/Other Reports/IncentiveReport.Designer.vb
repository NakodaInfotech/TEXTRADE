<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IncentiveReport
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
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.CMDEXPORT = New System.Windows.Forms.Button()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GTAXABLEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCNAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDNAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNETTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GELIGIBILEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINCENTIVEPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINCENTIVEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.CMDEXPORT)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1134, 581)
        Me.BlendPanel1.TabIndex = 0
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.Black
        Me.CMDREFRESH.Location = New System.Drawing.Point(527, 547)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREFRESH.TabIndex = 1
        Me.CMDREFRESH.Text = "&Refresh"
        Me.CMDREFRESH.UseVisualStyleBackColor = False
        '
        'CMDEXPORT
        '
        Me.CMDEXPORT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXPORT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXPORT.FlatAppearance.BorderSize = 0
        Me.CMDEXPORT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXPORT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXPORT.Location = New System.Drawing.Point(441, 547)
        Me.CMDEXPORT.Name = "CMDEXPORT"
        Me.CMDEXPORT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXPORT.TabIndex = 0
        Me.CMDEXPORT.Text = "&Export"
        Me.CMDEXPORT.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(21, 12)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.Size = New System.Drawing.Size(1092, 529)
        Me.gridbilldetails.TabIndex = 3
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GNAME, Me.GTAXABLEAMT, Me.GCNAMT, Me.GDNAMT, Me.GNETTAMT, Me.GELIGIBILEAMT, Me.GINCENTIVEPER, Me.GINCENTIVEAMT})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GTAXABLEAMT
        '
        Me.GTAXABLEAMT.Caption = "Taxable Amt"
        Me.GTAXABLEAMT.DisplayFormat.FormatString = "0.00"
        Me.GTAXABLEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTAXABLEAMT.FieldName = "TAXABLEAMT"
        Me.GTAXABLEAMT.Name = "GTAXABLEAMT"
        Me.GTAXABLEAMT.Visible = True
        Me.GTAXABLEAMT.VisibleIndex = 1
        Me.GTAXABLEAMT.Width = 120
        '
        'GCNAMT
        '
        Me.GCNAMT.Caption = "Credit Note"
        Me.GCNAMT.DisplayFormat.FormatString = "0.00"
        Me.GCNAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCNAMT.FieldName = "CNAMT"
        Me.GCNAMT.Name = "GCNAMT"
        Me.GCNAMT.Visible = True
        Me.GCNAMT.VisibleIndex = 2
        Me.GCNAMT.Width = 120
        '
        'GDNAMT
        '
        Me.GDNAMT.Caption = "Debit Note"
        Me.GDNAMT.DisplayFormat.FormatString = "0.00"
        Me.GDNAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GDNAMT.FieldName = "DNAMT"
        Me.GDNAMT.Name = "GDNAMT"
        Me.GDNAMT.Visible = True
        Me.GDNAMT.VisibleIndex = 3
        Me.GDNAMT.Width = 120
        '
        'GNETTAMT
        '
        Me.GNETTAMT.Caption = "Nett Amount"
        Me.GNETTAMT.DisplayFormat.FormatString = "0.00"
        Me.GNETTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GNETTAMT.FieldName = "NETTAMT"
        Me.GNETTAMT.Name = "GNETTAMT"
        Me.GNETTAMT.Visible = True
        Me.GNETTAMT.VisibleIndex = 4
        Me.GNETTAMT.Width = 120
        '
        'GELIGIBILEAMT
        '
        Me.GELIGIBILEAMT.Caption = "Eligibility Amt"
        Me.GELIGIBILEAMT.DisplayFormat.FormatString = "0.00"
        Me.GELIGIBILEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GELIGIBILEAMT.FieldName = "ELIGIBILITYAMT"
        Me.GELIGIBILEAMT.Name = "GELIGIBILEAMT"
        Me.GELIGIBILEAMT.Visible = True
        Me.GELIGIBILEAMT.VisibleIndex = 5
        Me.GELIGIBILEAMT.Width = 120
        '
        'GINCENTIVEPER
        '
        Me.GINCENTIVEPER.Caption = "Incentive %"
        Me.GINCENTIVEPER.DisplayFormat.FormatString = "0.00"
        Me.GINCENTIVEPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GINCENTIVEPER.FieldName = "INCENTIVEPER"
        Me.GINCENTIVEPER.Name = "GINCENTIVEPER"
        Me.GINCENTIVEPER.Visible = True
        Me.GINCENTIVEPER.VisibleIndex = 6
        '
        'GINCENTIVEAMT
        '
        Me.GINCENTIVEAMT.Caption = "Incentive Amt"
        Me.GINCENTIVEAMT.DisplayFormat.FormatString = "0.00"
        Me.GINCENTIVEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GINCENTIVEAMT.FieldName = "INCENTIVEAMT"
        Me.GINCENTIVEAMT.Name = "GINCENTIVEAMT"
        Me.GINCENTIVEAMT.Visible = True
        Me.GINCENTIVEAMT.VisibleIndex = 7
        Me.GINCENTIVEAMT.Width = 120
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.Color.Transparent
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdcancel.FlatAppearance.BorderSize = 0
        Me.cmdcancel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.Black
        Me.cmdcancel.Location = New System.Drawing.Point(613, 547)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 2
        Me.cmdcancel.Text = "E&xit"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'GNAME
        '
        Me.GNAME.Caption = "Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 0
        Me.GNAME.Width = 250
        '
        'IncentiveReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1134, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "IncentiveReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Incentive Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDEXPORT As Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GTAXABLEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCNAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDNAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNETTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GELIGIBILEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINCENTIVEPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINCENTIVEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmdcancel As Button
    Friend WithEvents CMDREFRESH As Button
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
End Class
