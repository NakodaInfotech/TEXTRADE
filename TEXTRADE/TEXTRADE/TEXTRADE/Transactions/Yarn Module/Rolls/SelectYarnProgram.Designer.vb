<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectYarnProgram
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
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKEDIT = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GPROGRAMNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPROGRAMSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GENDS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GROLLS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLENGTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALENDS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GQUALITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMILLNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGIVENBY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.APPROXDATE = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.APPROXDATE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.APPROXDATE.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1010, 610)
        Me.BlendPanel1.TabIndex = 3
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(18, 22)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKEDIT, Me.APPROXDATE})
        Me.gridbilldetails.Size = New System.Drawing.Size(981, 548)
        Me.gridbilldetails.TabIndex = 651
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHK, Me.GPROGRAMNO, Me.GPROGRAMSRNO, Me.GDATE, Me.GENDS, Me.GROLLS, Me.GLENGTH, Me.GTOTALENDS, Me.GQUALITY, Me.GMILLNAME, Me.GGIVENBY})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GCHK
        '
        Me.GCHK.ColumnEdit = Me.CHKEDIT
        Me.GCHK.FieldName = "CHK"
        Me.GCHK.Name = "GCHK"
        Me.GCHK.OptionsColumn.ShowCaption = False
        Me.GCHK.Visible = True
        Me.GCHK.VisibleIndex = 0
        Me.GCHK.Width = 41
        '
        'CHKEDIT
        '
        Me.CHKEDIT.AutoHeight = False
        Me.CHKEDIT.Name = "CHKEDIT"
        Me.CHKEDIT.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GPROGRAMNO
        '
        Me.GPROGRAMNO.Caption = "Program No."
        Me.GPROGRAMNO.FieldName = "PROGRAMNO"
        Me.GPROGRAMNO.Name = "GPROGRAMNO"
        Me.GPROGRAMNO.OptionsColumn.AllowEdit = False
        Me.GPROGRAMNO.Width = 85
        '
        'GPROGRAMSRNO
        '
        Me.GPROGRAMSRNO.Caption = "Program No"
        Me.GPROGRAMSRNO.FieldName = "PROGRAMSRNO"
        Me.GPROGRAMSRNO.Name = "GPROGRAMSRNO"
        Me.GPROGRAMSRNO.Visible = True
        Me.GPROGRAMSRNO.VisibleIndex = 1
        Me.GPROGRAMSRNO.Width = 85
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
        Me.GDATE.VisibleIndex = 2
        '
        'GENDS
        '
        Me.GENDS.Caption = "Ends"
        Me.GENDS.DisplayFormat.FormatString = "0"
        Me.GENDS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GENDS.FieldName = "ENDS"
        Me.GENDS.Name = "GENDS"
        Me.GENDS.OptionsColumn.AllowEdit = False
        Me.GENDS.Visible = True
        Me.GENDS.VisibleIndex = 3
        Me.GENDS.Width = 60
        '
        'GROLLS
        '
        Me.GROLLS.Caption = "Rolls"
        Me.GROLLS.DisplayFormat.FormatString = "0"
        Me.GROLLS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GROLLS.FieldName = "ROLLS"
        Me.GROLLS.Name = "GROLLS"
        Me.GROLLS.OptionsColumn.AllowEdit = False
        Me.GROLLS.Visible = True
        Me.GROLLS.VisibleIndex = 4
        Me.GROLLS.Width = 60
        '
        'GLENGTH
        '
        Me.GLENGTH.Caption = "Length"
        Me.GLENGTH.DisplayFormat.FormatString = "0"
        Me.GLENGTH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GLENGTH.FieldName = "LENGTH"
        Me.GLENGTH.Name = "GLENGTH"
        Me.GLENGTH.OptionsColumn.AllowEdit = False
        Me.GLENGTH.Visible = True
        Me.GLENGTH.VisibleIndex = 5
        Me.GLENGTH.Width = 60
        '
        'GTOTALENDS
        '
        Me.GTOTALENDS.Caption = "Total Ends"
        Me.GTOTALENDS.DisplayFormat.FormatString = "0"
        Me.GTOTALENDS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALENDS.FieldName = "TOTALENDS"
        Me.GTOTALENDS.Name = "GTOTALENDS"
        Me.GTOTALENDS.OptionsColumn.AllowEdit = False
        Me.GTOTALENDS.Visible = True
        Me.GTOTALENDS.VisibleIndex = 6
        Me.GTOTALENDS.Width = 80
        '
        'GQUALITY
        '
        Me.GQUALITY.Caption = "Quality "
        Me.GQUALITY.FieldName = "QUALITY"
        Me.GQUALITY.Name = "GQUALITY"
        Me.GQUALITY.OptionsColumn.AllowEdit = False
        Me.GQUALITY.Visible = True
        Me.GQUALITY.VisibleIndex = 7
        Me.GQUALITY.Width = 150
        '
        'GMILLNAME
        '
        Me.GMILLNAME.Caption = "Mill Name"
        Me.GMILLNAME.FieldName = "MILLNAME"
        Me.GMILLNAME.Name = "GMILLNAME"
        Me.GMILLNAME.Visible = True
        Me.GMILLNAME.VisibleIndex = 8
        Me.GMILLNAME.Width = 200
        '
        'GGIVENBY
        '
        Me.GGIVENBY.Caption = "Given By"
        Me.GGIVENBY.FieldName = "GIVENBY"
        Me.GGIVENBY.Name = "GGIVENBY"
        Me.GGIVENBY.OptionsColumn.AllowEdit = False
        Me.GGIVENBY.Visible = True
        Me.GGIVENBY.VisibleIndex = 9
        Me.GGIVENBY.Width = 120
        '
        'APPROXDATE
        '
        Me.APPROXDATE.AutoHeight = False
        Me.APPROXDATE.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.APPROXDATE.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.APPROXDATE.Name = "APPROXDATE"
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(511, 576)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 550
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(425, 576)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 549
        Me.cmdok.Text = "&Ok"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'SelectYarnProgram
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1010, 610)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "SelectYarnProgram"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Select Yarn Program"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.APPROXDATE.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.APPROXDATE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKEDIT As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GPROGRAMNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPROGRAMSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GENDS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GROLLS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLENGTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALENDS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GQUALITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMILLNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGIVENBY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents APPROXDATE As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents cmdexit As Button
    Friend WithEvents cmdok As Button
End Class
