<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CoverNoteDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CoverNoteDetails))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gsrno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gdate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREGNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVINITIALS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPRINTINITIALS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRIDNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRIDAGENTNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTRANSPORT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLRDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOURIERNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOURIERDOCKETNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOURIERDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.lbl = New System.Windows.Forms.Label()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1303, 581)
        Me.BlendPanel1.TabIndex = 7
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(12, 34)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.Size = New System.Drawing.Size(1279, 472)
        Me.gridbilldetails.TabIndex = 256
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gsrno, Me.gdate, Me.GNAME, Me.GAGENT, Me.GINVNO, Me.GREGNAME, Me.GINVINITIALS, Me.GPRINTINITIALS, Me.GGRIDNAME, Me.GGRIDAGENTNAME, Me.GINVDATE, Me.GLRNO, Me.GTRANSPORT, Me.GLRDATE, Me.GTOTALMTRS, Me.GTOTALPCS, Me.GGRANDTOTAL, Me.GCOURIERNAME, Me.GCOURIERDOCKETNO, Me.GCOURIERDATE, Me.GREMARKS})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsCustomization.AllowColumnMoving = False
        Me.gridbill.OptionsCustomization.AllowGroup = False
        Me.gridbill.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.gridbill.OptionsSelection.MultiSelect = True
        Me.gridbill.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'gsrno
        '
        Me.gsrno.Caption = "Sr. No"
        Me.gsrno.FieldName = "TEMPCOVERNO"
        Me.gsrno.Name = "gsrno"
        Me.gsrno.Visible = True
        Me.gsrno.VisibleIndex = 1
        Me.gsrno.Width = 50
        '
        'gdate
        '
        Me.gdate.Caption = "Date"
        Me.gdate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.gdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.gdate.FieldName = "COVERDATE"
        Me.gdate.Name = "gdate"
        Me.gdate.Visible = True
        Me.gdate.VisibleIndex = 2
        Me.gdate.Width = 80
        '
        'GNAME
        '
        Me.GNAME.Caption = "Name"
        Me.GNAME.FieldName = "PARTYNAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 3
        Me.GNAME.Width = 220
        '
        'GAGENT
        '
        Me.GAGENT.Caption = "Agent"
        Me.GAGENT.FieldName = "AGENT"
        Me.GAGENT.Name = "GAGENT"
        Me.GAGENT.Visible = True
        Me.GAGENT.VisibleIndex = 4
        Me.GAGENT.Width = 220
        '
        'GINVNO
        '
        Me.GINVNO.Caption = "Inv No"
        Me.GINVNO.FieldName = "INVNO"
        Me.GINVNO.Name = "GINVNO"
        Me.GINVNO.Visible = True
        Me.GINVNO.VisibleIndex = 5
        '
        'GREGNAME
        '
        Me.GREGNAME.Caption = "Register Name"
        Me.GREGNAME.FieldName = "REGNAME"
        Me.GREGNAME.Name = "GREGNAME"
        Me.GREGNAME.Visible = True
        Me.GREGNAME.VisibleIndex = 6
        Me.GREGNAME.Width = 150
        '
        'GINVINITIALS
        '
        Me.GINVINITIALS.Caption = "Inv Initials"
        Me.GINVINITIALS.FieldName = "INVINITIALS"
        Me.GINVINITIALS.Name = "GINVINITIALS"
        Me.GINVINITIALS.Visible = True
        Me.GINVINITIALS.VisibleIndex = 7
        '
        'GPRINTINITIALS
        '
        Me.GPRINTINITIALS.Caption = "Print Initials"
        Me.GPRINTINITIALS.FieldName = "PRINTINITIALS"
        Me.GPRINTINITIALS.Name = "GPRINTINITIALS"
        Me.GPRINTINITIALS.Visible = True
        Me.GPRINTINITIALS.VisibleIndex = 8
        '
        'GGRIDNAME
        '
        Me.GGRIDNAME.Caption = "Grid Party Name"
        Me.GGRIDNAME.FieldName = "GRIDNAME"
        Me.GGRIDNAME.Name = "GGRIDNAME"
        Me.GGRIDNAME.OptionsColumn.AllowEdit = False
        Me.GGRIDNAME.Visible = True
        Me.GGRIDNAME.VisibleIndex = 9
        Me.GGRIDNAME.Width = 200
        '
        'GGRIDAGENTNAME
        '
        Me.GGRIDAGENTNAME.Caption = "Grid Agent Name"
        Me.GGRIDAGENTNAME.FieldName = "GRIDAGENTNAME"
        Me.GGRIDAGENTNAME.Name = "GGRIDAGENTNAME"
        Me.GGRIDAGENTNAME.OptionsColumn.AllowEdit = False
        Me.GGRIDAGENTNAME.Visible = True
        Me.GGRIDAGENTNAME.VisibleIndex = 10
        Me.GGRIDAGENTNAME.Width = 200
        '
        'GINVDATE
        '
        Me.GINVDATE.Caption = "Inv Date"
        Me.GINVDATE.FieldName = "INVDATE"
        Me.GINVDATE.Name = "GINVDATE"
        Me.GINVDATE.Visible = True
        Me.GINVDATE.VisibleIndex = 11
        '
        'GLRNO
        '
        Me.GLRNO.Caption = "LR No"
        Me.GLRNO.FieldName = "LRNO"
        Me.GLRNO.Name = "GLRNO"
        Me.GLRNO.Visible = True
        Me.GLRNO.VisibleIndex = 12
        '
        'GTRANSPORT
        '
        Me.GTRANSPORT.Caption = "Transport"
        Me.GTRANSPORT.FieldName = "TRANSPORT"
        Me.GTRANSPORT.Name = "GTRANSPORT"
        Me.GTRANSPORT.Visible = True
        Me.GTRANSPORT.VisibleIndex = 13
        Me.GTRANSPORT.Width = 150
        '
        'GLRDATE
        '
        Me.GLRDATE.Caption = "LR Date"
        Me.GLRDATE.FieldName = "LRDATE"
        Me.GLRDATE.Name = "GLRDATE"
        Me.GLRDATE.Visible = True
        Me.GLRDATE.VisibleIndex = 14
        '
        'GTOTALMTRS
        '
        Me.GTOTALMTRS.Caption = "Total Mtrs"
        Me.GTOTALMTRS.FieldName = "TOTALMTRS"
        Me.GTOTALMTRS.Name = "GTOTALMTRS"
        Me.GTOTALMTRS.Visible = True
        Me.GTOTALMTRS.VisibleIndex = 15
        Me.GTOTALMTRS.Width = 80
        '
        'GTOTALPCS
        '
        Me.GTOTALPCS.Caption = "Total Pcs"
        Me.GTOTALPCS.FieldName = "TOTALPCS"
        Me.GTOTALPCS.ImageIndex = 0
        Me.GTOTALPCS.Name = "GTOTALPCS"
        Me.GTOTALPCS.Visible = True
        Me.GTOTALPCS.VisibleIndex = 16
        Me.GTOTALPCS.Width = 80
        '
        'GGRANDTOTAL
        '
        Me.GGRANDTOTAL.Caption = "Grand Total"
        Me.GGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.GGRANDTOTAL.Name = "GGRANDTOTAL"
        Me.GGRANDTOTAL.Visible = True
        Me.GGRANDTOTAL.VisibleIndex = 17
        '
        'GCOURIERNAME
        '
        Me.GCOURIERNAME.Caption = "Courier Name"
        Me.GCOURIERNAME.FieldName = "COURIERNAME"
        Me.GCOURIERNAME.Name = "GCOURIERNAME"
        Me.GCOURIERNAME.OptionsColumn.AllowEdit = False
        Me.GCOURIERNAME.Visible = True
        Me.GCOURIERNAME.VisibleIndex = 18
        Me.GCOURIERNAME.Width = 220
        '
        'GCOURIERDOCKETNO
        '
        Me.GCOURIERDOCKETNO.Caption = "Courier Docket No"
        Me.GCOURIERDOCKETNO.FieldName = "COURIERDOCKETNO"
        Me.GCOURIERDOCKETNO.Name = "GCOURIERDOCKETNO"
        Me.GCOURIERDOCKETNO.OptionsColumn.AllowEdit = False
        Me.GCOURIERDOCKETNO.Visible = True
        Me.GCOURIERDOCKETNO.VisibleIndex = 19
        Me.GCOURIERDOCKETNO.Width = 150
        '
        'GCOURIERDATE
        '
        Me.GCOURIERDATE.Caption = "Courier Date"
        Me.GCOURIERDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GCOURIERDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GCOURIERDATE.FieldName = "COURIERDATE"
        Me.GCOURIERDATE.Name = "GCOURIERDATE"
        Me.GCOURIERDATE.OptionsColumn.AllowEdit = False
        Me.GCOURIERDATE.Visible = True
        Me.GCOURIERDATE.VisibleIndex = 20
        Me.GCOURIERDATE.Width = 80
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "REMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.Visible = True
        Me.GREMARKS.VisibleIndex = 21
        Me.GREMARKS.Width = 240
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(645, 528)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 4
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.toolStripSeparator, Me.TOOLREFRESH, Me.PrintToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1303, 25)
        Me.ToolStrip1.TabIndex = 255
        Me.ToolStrip1.Text = "ToolStrip1"
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
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl.Location = New System.Drawing.Point(19, 34)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(130, 14)
        Me.lbl.TabIndex = 251
        Me.lbl.Text = "Select a S.O. to Change"
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(563, 528)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 3
        Me.cmdok.Text = "&Ok"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'CoverNoteDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1303, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "CoverNoteDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Cover Note Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents gsrno As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gdate As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GTOTALPCS As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GTOTALMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmdexit As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents TOOLREFRESH As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents lbl As Label
    Friend WithEvents cmdok As Button
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREGNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVINITIALS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPRINTINITIALS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLRDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTRANSPORT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRIDNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRIDAGENTNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOURIERNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOURIERDOCKETNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOURIERDATE As DevExpress.XtraGrid.Columns.GridColumn
End Class
