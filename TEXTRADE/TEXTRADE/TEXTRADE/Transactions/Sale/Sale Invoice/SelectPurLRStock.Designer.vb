<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectPurLRStock
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
        Me.CHK = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GENTRYNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINITIALS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPARTYBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTRANSNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GITEMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREGID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLEDGERID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTRANSID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GUNIT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GHSNCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAQTY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFOLDPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKSELECT = New System.Windows.Forms.CheckBox()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.CHKSELECT)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1284, 581)
        Me.BlendPanel1.TabIndex = 1
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(12, 28)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHK})
        Me.gridbilldetails.Size = New System.Drawing.Size(1260, 507)
        Me.gridbilldetails.TabIndex = 808
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHK, Me.GENTRYNO, Me.GDATE, Me.GINITIALS, Me.GNAME, Me.GPARTYBILLNO, Me.GTRANSNAME, Me.GLRNO, Me.GITEMNAME, Me.GPCS, Me.GMTRS, Me.GWT, Me.GTYPE, Me.GREGID, Me.GLEDGERID, Me.GTRANSID, Me.GUNIT, Me.GHSNCODE, Me.GAQTY, Me.GFOLDPER})
        Me.gridbill.CustomizationFormBounds = New System.Drawing.Rectangle(688, 311, 208, 184)
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AutoExpandAllGroups = True
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GCHK
        '
        Me.GCHK.ColumnEdit = Me.CHK
        Me.GCHK.FieldName = "CHK"
        Me.GCHK.Name = "GCHK"
        Me.GCHK.Visible = True
        Me.GCHK.VisibleIndex = 0
        Me.GCHK.Width = 40
        '
        'CHK
        '
        Me.CHK.AutoHeight = False
        Me.CHK.Name = "CHK"
        Me.CHK.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GENTRYNO
        '
        Me.GENTRYNO.Caption = "Entry No"
        Me.GENTRYNO.FieldName = "BILLNO"
        Me.GENTRYNO.Name = "GENTRYNO"
        Me.GENTRYNO.OptionsColumn.AllowEdit = False
        '
        'GDATE
        '
        Me.GDATE.Caption = "Date"
        Me.GDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GDATE.FieldName = "BILLDATE"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.OptionsColumn.AllowEdit = False
        Me.GDATE.Visible = True
        Me.GDATE.VisibleIndex = 1
        Me.GDATE.Width = 85
        '
        'GINITIALS
        '
        Me.GINITIALS.Caption = "Initials"
        Me.GINITIALS.FieldName = "BILLINITIALS"
        Me.GINITIALS.Name = "GINITIALS"
        Me.GINITIALS.OptionsColumn.AllowEdit = False
        Me.GINITIALS.Visible = True
        Me.GINITIALS.VisibleIndex = 2
        Me.GINITIALS.Width = 100
        '
        'GNAME
        '
        Me.GNAME.Caption = "Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.OptionsColumn.AllowEdit = False
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 3
        Me.GNAME.Width = 250
        '
        'GPARTYBILLNO
        '
        Me.GPARTYBILLNO.Caption = "Party Bill No"
        Me.GPARTYBILLNO.FieldName = "PARTYBILLNO"
        Me.GPARTYBILLNO.Name = "GPARTYBILLNO"
        Me.GPARTYBILLNO.OptionsColumn.AllowEdit = False
        Me.GPARTYBILLNO.Visible = True
        Me.GPARTYBILLNO.VisibleIndex = 4
        Me.GPARTYBILLNO.Width = 120
        '
        'GTRANSNAME
        '
        Me.GTRANSNAME.Caption = "Transport Name"
        Me.GTRANSNAME.FieldName = "TRANSNAME"
        Me.GTRANSNAME.Name = "GTRANSNAME"
        Me.GTRANSNAME.OptionsColumn.AllowEdit = False
        Me.GTRANSNAME.Visible = True
        Me.GTRANSNAME.VisibleIndex = 6
        Me.GTRANSNAME.Width = 200
        '
        'GLRNO
        '
        Me.GLRNO.Caption = "LR No"
        Me.GLRNO.FieldName = "LRNO"
        Me.GLRNO.Name = "GLRNO"
        Me.GLRNO.OptionsColumn.AllowEdit = False
        Me.GLRNO.Visible = True
        Me.GLRNO.VisibleIndex = 5
        Me.GLRNO.Width = 120
        '
        'GITEMNAME
        '
        Me.GITEMNAME.Caption = "Item Name"
        Me.GITEMNAME.FieldName = "ITEMNAME"
        Me.GITEMNAME.Name = "GITEMNAME"
        Me.GITEMNAME.OptionsColumn.AllowEdit = False
        Me.GITEMNAME.Visible = True
        Me.GITEMNAME.VisibleIndex = 7
        Me.GITEMNAME.Width = 180
        '
        'GPCS
        '
        Me.GPCS.Caption = "Pcs"
        Me.GPCS.DisplayFormat.FormatString = "0"
        Me.GPCS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPCS.FieldName = "TOTALQTY"
        Me.GPCS.Name = "GPCS"
        Me.GPCS.OptionsColumn.AllowEdit = False
        Me.GPCS.Visible = True
        Me.GPCS.VisibleIndex = 8
        Me.GPCS.Width = 60
        '
        'GMTRS
        '
        Me.GMTRS.Caption = "Mtrs"
        Me.GMTRS.DisplayFormat.FormatString = "0.00"
        Me.GMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GMTRS.FieldName = "TOTALMTRS"
        Me.GMTRS.Name = "GMTRS"
        Me.GMTRS.OptionsColumn.AllowEdit = False
        Me.GMTRS.Visible = True
        Me.GMTRS.VisibleIndex = 9
        Me.GMTRS.Width = 80
        '
        'GWT
        '
        Me.GWT.Caption = "Wt"
        Me.GWT.DisplayFormat.FormatString = "0.00"
        Me.GWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GWT.FieldName = "WT"
        Me.GWT.Name = "GWT"
        Me.GWT.OptionsColumn.AllowEdit = False
        Me.GWT.Visible = True
        Me.GWT.VisibleIndex = 10
        '
        'GTYPE
        '
        Me.GTYPE.Caption = "Type"
        Me.GTYPE.FieldName = "TYPE"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.OptionsColumn.AllowEdit = False
        Me.GTYPE.Visible = True
        Me.GTYPE.VisibleIndex = 11
        '
        'GREGID
        '
        Me.GREGID.Caption = "REGID"
        Me.GREGID.FieldName = "REGID"
        Me.GREGID.Name = "GREGID"
        Me.GREGID.OptionsColumn.AllowEdit = False
        '
        'GLEDGERID
        '
        Me.GLEDGERID.Caption = "LEDGERID"
        Me.GLEDGERID.FieldName = "LEDGERID"
        Me.GLEDGERID.Name = "GLEDGERID"
        Me.GLEDGERID.OptionsColumn.AllowEdit = False
        '
        'GTRANSID
        '
        Me.GTRANSID.Caption = "TRANSID"
        Me.GTRANSID.FieldName = "TRANSID"
        Me.GTRANSID.Name = "GTRANSID"
        Me.GTRANSID.OptionsColumn.AllowEdit = False
        '
        'GUNIT
        '
        Me.GUNIT.Caption = "Unit"
        Me.GUNIT.FieldName = "UNIT"
        Me.GUNIT.Name = "GUNIT"
        Me.GUNIT.OptionsColumn.AllowEdit = False
        '
        'GHSNCODE
        '
        Me.GHSNCODE.Caption = "HSN"
        Me.GHSNCODE.FieldName = "HSNCODE"
        Me.GHSNCODE.Name = "GHSNCODE"
        Me.GHSNCODE.OptionsColumn.AllowEdit = False
        '
        'GAQTY
        '
        Me.GAQTY.Caption = "Qty"
        Me.GAQTY.DisplayFormat.FormatString = "0.00"
        Me.GAQTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GAQTY.FieldName = "AQTY"
        Me.GAQTY.Name = "GAQTY"
        Me.GAQTY.OptionsColumn.AllowEdit = False
        '
        'GFOLDPER
        '
        Me.GFOLDPER.Caption = "Fold %"
        Me.GFOLDPER.DisplayFormat.FormatString = "0.00"
        Me.GFOLDPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GFOLDPER.FieldName = "FOLDPER"
        Me.GFOLDPER.Name = "GFOLDPER"
        Me.GFOLDPER.OptionsColumn.AllowEdit = False
        '
        'CHKSELECT
        '
        Me.CHKSELECT.AutoSize = True
        Me.CHKSELECT.BackColor = System.Drawing.Color.Transparent
        Me.CHKSELECT.Location = New System.Drawing.Point(31, 6)
        Me.CHKSELECT.Name = "CHKSELECT"
        Me.CHKSELECT.Size = New System.Drawing.Size(76, 19)
        Me.CHKSELECT.TabIndex = 805
        Me.CHKSELECT.Text = "&Select All"
        Me.CHKSELECT.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Location = New System.Drawing.Point(645, 541)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 2
        Me.CMDEXIT.Text = "&Exit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDOK
        '
        Me.CMDOK.Location = New System.Drawing.Point(559, 541)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 1
        Me.CMDOK.Text = "&OK"
        Me.CMDOK.UseVisualStyleBackColor = True
        '
        'SelectPurLRStock
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1284, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "SelectPurLRStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Select LR Stock"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDOK As Button
    Friend WithEvents CHKSELECT As CheckBox
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GENTRYNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINITIALS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPARTYBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREGID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLEDGERID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTRANSID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHK As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GTRANSNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GITEMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GUNIT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GHSNCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAQTY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFOLDPER As DevExpress.XtraGrid.Columns.GridColumn
End Class
