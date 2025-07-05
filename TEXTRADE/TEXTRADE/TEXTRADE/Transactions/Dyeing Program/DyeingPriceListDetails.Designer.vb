<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DyeingPriceListDetails
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
        Me.gSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gdate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPROCESS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWHITE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCREAM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLIGHT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMEDIUM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDARK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEXTRADARK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRAINBOW = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEXTRACHGS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPROCIANWHITE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPROCIANDYDE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDISCHARGE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GKHADI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GABOVETOSCREEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMISCRATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBLOCKED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton()
        Me.TOOLEXCEL = New System.Windows.Forms.ToolStripButton()
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
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 611)
        Me.BlendPanel1.TabIndex = 3
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(11, 34)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.Size = New System.Drawing.Size(1211, 531)
        Me.gridbilldetails.TabIndex = 1
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gSRNO, Me.gdate, Me.GNAME, Me.GTYPE, Me.GPROCESS, Me.GWHITE, Me.GCREAM, Me.GLIGHT, Me.GMEDIUM, Me.GDARK, Me.GEXTRADARK, Me.GRAINBOW, Me.GEXTRACHGS, Me.GPROCIANWHITE, Me.GPROCIANDYDE, Me.GDISCHARGE, Me.GKHADI, Me.GABOVETOSCREEN, Me.GMISCRATE, Me.GBLOCKED})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'gSRNO
        '
        Me.gSRNO.Caption = "Sr.No"
        Me.gSRNO.FieldName = "TEMPDPLNO"
        Me.gSRNO.Name = "gSRNO"
        Me.gSRNO.Visible = True
        Me.gSRNO.VisibleIndex = 0
        Me.gSRNO.Width = 60
        '
        'gdate
        '
        Me.gdate.Caption = "Date"
        Me.gdate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.gdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.gdate.FieldName = "DATE"
        Me.gdate.Name = "gdate"
        Me.gdate.Visible = True
        Me.gdate.VisibleIndex = 1
        '
        'GNAME
        '
        Me.GNAME.Caption = "Dyeing Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 2
        Me.GNAME.Width = 200
        '
        'GTYPE
        '
        Me.GTYPE.Caption = "Type"
        Me.GTYPE.FieldName = "TYPE"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.Visible = True
        Me.GTYPE.VisibleIndex = 3
        '
        'GPROCESS
        '
        Me.GPROCESS.Caption = "Process"
        Me.GPROCESS.FieldName = "PROCESS"
        Me.GPROCESS.Name = "GPROCESS"
        Me.GPROCESS.Width = 160
        '
        'GWHITE
        '
        Me.GWHITE.Caption = "White"
        Me.GWHITE.FieldName = "WHITE"
        Me.GWHITE.Name = "GWHITE"
        Me.GWHITE.Visible = True
        Me.GWHITE.VisibleIndex = 4
        '
        'GCREAM
        '
        Me.GCREAM.Caption = "Cream"
        Me.GCREAM.FieldName = "CREAM"
        Me.GCREAM.Name = "GCREAM"
        Me.GCREAM.Visible = True
        Me.GCREAM.VisibleIndex = 5
        '
        'GLIGHT
        '
        Me.GLIGHT.Caption = "Light"
        Me.GLIGHT.FieldName = "LIGHT"
        Me.GLIGHT.Name = "GLIGHT"
        Me.GLIGHT.Visible = True
        Me.GLIGHT.VisibleIndex = 6
        '
        'GMEDIUM
        '
        Me.GMEDIUM.Caption = "Medium"
        Me.GMEDIUM.FieldName = "MEDIUM"
        Me.GMEDIUM.Name = "GMEDIUM"
        Me.GMEDIUM.Visible = True
        Me.GMEDIUM.VisibleIndex = 7
        '
        'GDARK
        '
        Me.GDARK.Caption = "Dark"
        Me.GDARK.FieldName = "DARK"
        Me.GDARK.Name = "GDARK"
        Me.GDARK.Visible = True
        Me.GDARK.VisibleIndex = 8
        '
        'GEXTRADARK
        '
        Me.GEXTRADARK.Caption = "ExtraDark"
        Me.GEXTRADARK.FieldName = "EXTRADARK"
        Me.GEXTRADARK.Name = "GEXTRADARK"
        Me.GEXTRADARK.Visible = True
        Me.GEXTRADARK.VisibleIndex = 9
        '
        'GRAINBOW
        '
        Me.GRAINBOW.Caption = "Rainbow"
        Me.GRAINBOW.FieldName = "RAINBOW"
        Me.GRAINBOW.Name = "GRAINBOW"
        Me.GRAINBOW.Visible = True
        Me.GRAINBOW.VisibleIndex = 10
        '
        'GEXTRACHGS
        '
        Me.GEXTRACHGS.Caption = "Extra Chgs"
        Me.GEXTRACHGS.FieldName = "EXTRACHGS"
        Me.GEXTRACHGS.Name = "GEXTRACHGS"
        Me.GEXTRACHGS.Visible = True
        Me.GEXTRACHGS.VisibleIndex = 11
        '
        'GPROCIANWHITE
        '
        Me.GPROCIANWHITE.Caption = "Procianwhite"
        Me.GPROCIANWHITE.FieldName = "PROCWHITE"
        Me.GPROCIANWHITE.Name = "GPROCIANWHITE"
        Me.GPROCIANWHITE.Visible = True
        Me.GPROCIANWHITE.VisibleIndex = 12
        '
        'GPROCIANDYDE
        '
        Me.GPROCIANDYDE.Caption = "Prociandyde"
        Me.GPROCIANDYDE.FieldName = "PROCDYED"
        Me.GPROCIANDYDE.Name = "GPROCIANDYDE"
        Me.GPROCIANDYDE.Visible = True
        Me.GPROCIANDYDE.VisibleIndex = 13
        '
        'GDISCHARGE
        '
        Me.GDISCHARGE.Caption = "Discharge"
        Me.GDISCHARGE.FieldName = "DISCHARGE"
        Me.GDISCHARGE.Name = "GDISCHARGE"
        Me.GDISCHARGE.Visible = True
        Me.GDISCHARGE.VisibleIndex = 14
        '
        'GKHADI
        '
        Me.GKHADI.Caption = "Khadi"
        Me.GKHADI.FieldName = "KHADI"
        Me.GKHADI.Name = "GKHADI"
        Me.GKHADI.Visible = True
        Me.GKHADI.VisibleIndex = 15
        '
        'GABOVETOSCREEN
        '
        Me.GABOVETOSCREEN.Caption = "Above To Screen"
        Me.GABOVETOSCREEN.FieldName = "ABOVETWOSCREEN"
        Me.GABOVETOSCREEN.Name = "GABOVETOSCREEN"
        Me.GABOVETOSCREEN.Visible = True
        Me.GABOVETOSCREEN.VisibleIndex = 16
        '
        'GMISCRATE
        '
        Me.GMISCRATE.Caption = "Misc Rate"
        Me.GMISCRATE.FieldName = "MISCRATE"
        Me.GMISCRATE.Name = "GMISCRATE"
        Me.GMISCRATE.Visible = True
        Me.GMISCRATE.VisibleIndex = 17
        '
        'GBLOCKED
        '
        Me.GBLOCKED.Caption = "Blocked"
        Me.GBLOCKED.FieldName = "BLOCK"
        Me.GBLOCKED.Name = "GBLOCKED"
        Me.GBLOCKED.Visible = True
        Me.GBLOCKED.VisibleIndex = 18
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(620, 571)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 3
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.toolStripSeparator, Me.TOOLREFRESH, Me.TOOLEXCEL})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
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
        'TOOLEXCEL
        '
        Me.TOOLEXCEL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLEXCEL.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        Me.TOOLEXCEL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLEXCEL.Name = "TOOLEXCEL"
        Me.TOOLEXCEL.Size = New System.Drawing.Size(23, 22)
        Me.TOOLEXCEL.Text = "&Print"
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(534, 571)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 2
        Me.cmdok.Text = "&Ok"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'DyeingPriceListDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 611)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "DyeingPriceListDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Dyeing Price List Details"
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
    Private WithEvents gSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gdate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWHITE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCREAM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLIGHT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMEDIUM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmdexit As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents TOOLREFRESH As ToolStripButton
    Friend WithEvents TOOLEXCEL As ToolStripButton
    Friend WithEvents cmdok As Button
    Friend WithEvents GDARK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEXTRADARK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRAINBOW As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEXTRACHGS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPROCIANWHITE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPROCIANDYDE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDISCHARGE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GKHADI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GABOVETOSCREEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMISCRATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBLOCKED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPROCESS As DevExpress.XtraGrid.Columns.GridColumn
End Class
