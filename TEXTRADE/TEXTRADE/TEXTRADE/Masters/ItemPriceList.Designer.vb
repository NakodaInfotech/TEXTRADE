<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemPriceList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemPriceList))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TXTRATE15 = New System.Windows.Forms.TextBox()
        Me.TXTRATE14 = New System.Windows.Forms.TextBox()
        Me.TXTRATE13 = New System.Windows.Forms.TextBox()
        Me.TXTRATE12 = New System.Windows.Forms.TextBox()
        Me.TXTRATE11 = New System.Windows.Forms.TextBox()
        Me.GRIDBILLDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDBILL = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRATE15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TXTRATE5 = New System.Windows.Forms.TextBox()
        Me.TXTRATE1 = New System.Windows.Forms.TextBox()
        Me.TXTRATE10 = New System.Windows.Forms.TextBox()
        Me.TXTRATE2 = New System.Windows.Forms.TextBox()
        Me.TXTRATE9 = New System.Windows.Forms.TextBox()
        Me.TXTRATE3 = New System.Windows.Forms.TextBox()
        Me.TXTRATE8 = New System.Windows.Forms.TextBox()
        Me.TXTRATE4 = New System.Windows.Forms.TextBox()
        Me.TXTRATE7 = New System.Windows.Forms.TextBox()
        Me.TXTRATE6 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CMBRATEFROM = New System.Windows.Forms.ComboBox()
        Me.GPITEMNAME = New System.Windows.Forms.GroupBox()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.CHKSELECTITEM = New System.Windows.Forms.CheckBox()
        Me.GRIDITEMDETAILS = New DevExpress.XtraGrid.GridControl()
        Me.GRIDITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GITEMCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GITEMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TXTRATEPER = New System.Windows.Forms.TextBox()
        Me.CMBRATETYPE = New System.Windows.Forms.ComboBox()
        Me.cmbcategory = New System.Windows.Forms.ComboBox()
        Me.lblcategory = New System.Windows.Forms.Label()
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ExcelExport = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.BlendPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.GRIDBILLDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDBILL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GPITEMNAME.SuspendLayout()
        CType(Me.GRIDITEMDETAILS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Panel1)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.CMBRATEFROM)
        Me.BlendPanel1.Controls.Add(Me.GPITEMNAME)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.TXTRATEPER)
        Me.BlendPanel1.Controls.Add(Me.CMBRATETYPE)
        Me.BlendPanel1.Controls.Add(Me.cmbcategory)
        Me.BlendPanel1.Controls.Add(Me.lblcategory)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1369, 601)
        Me.BlendPanel1.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.TXTRATE15)
        Me.Panel1.Controls.Add(Me.TXTRATE14)
        Me.Panel1.Controls.Add(Me.TXTRATE13)
        Me.Panel1.Controls.Add(Me.TXTRATE12)
        Me.Panel1.Controls.Add(Me.TXTRATE11)
        Me.Panel1.Controls.Add(Me.GRIDBILLDETAILS)
        Me.Panel1.Controls.Add(Me.TXTRATE5)
        Me.Panel1.Controls.Add(Me.TXTRATE1)
        Me.Panel1.Controls.Add(Me.TXTRATE10)
        Me.Panel1.Controls.Add(Me.TXTRATE2)
        Me.Panel1.Controls.Add(Me.TXTRATE9)
        Me.Panel1.Controls.Add(Me.TXTRATE3)
        Me.Panel1.Controls.Add(Me.TXTRATE8)
        Me.Panel1.Controls.Add(Me.TXTRATE4)
        Me.Panel1.Controls.Add(Me.TXTRATE7)
        Me.Panel1.Controls.Add(Me.TXTRATE6)
        Me.Panel1.Location = New System.Drawing.Point(12, 59)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1065, 459)
        Me.Panel1.TabIndex = 772
        '
        'TXTRATE15
        '
        Me.TXTRATE15.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE15.Enabled = False
        Me.TXTRATE15.Location = New System.Drawing.Point(1371, 3)
        Me.TXTRATE15.Name = "TXTRATE15"
        Me.TXTRATE15.ReadOnly = True
        Me.TXTRATE15.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE15.TabIndex = 790
        Me.TXTRATE15.TabStop = False
        Me.TXTRATE15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE14
        '
        Me.TXTRATE14.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE14.Enabled = False
        Me.TXTRATE14.Location = New System.Drawing.Point(1286, 3)
        Me.TXTRATE14.Name = "TXTRATE14"
        Me.TXTRATE14.ReadOnly = True
        Me.TXTRATE14.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE14.TabIndex = 789
        Me.TXTRATE14.TabStop = False
        Me.TXTRATE14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE13
        '
        Me.TXTRATE13.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE13.Enabled = False
        Me.TXTRATE13.Location = New System.Drawing.Point(1201, 3)
        Me.TXTRATE13.Name = "TXTRATE13"
        Me.TXTRATE13.ReadOnly = True
        Me.TXTRATE13.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE13.TabIndex = 788
        Me.TXTRATE13.TabStop = False
        Me.TXTRATE13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE12
        '
        Me.TXTRATE12.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE12.Enabled = False
        Me.TXTRATE12.Location = New System.Drawing.Point(1116, 3)
        Me.TXTRATE12.Name = "TXTRATE12"
        Me.TXTRATE12.ReadOnly = True
        Me.TXTRATE12.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE12.TabIndex = 787
        Me.TXTRATE12.TabStop = False
        Me.TXTRATE12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE11
        '
        Me.TXTRATE11.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE11.Enabled = False
        Me.TXTRATE11.Location = New System.Drawing.Point(1031, 3)
        Me.TXTRATE11.Name = "TXTRATE11"
        Me.TXTRATE11.ReadOnly = True
        Me.TXTRATE11.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE11.TabIndex = 786
        Me.TXTRATE11.TabStop = False
        Me.TXTRATE11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GRIDBILLDETAILS
        '
        Me.GRIDBILLDETAILS.Location = New System.Drawing.Point(3, 25)
        Me.GRIDBILLDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDBILLDETAILS.MainView = Me.GRIDBILL
        Me.GRIDBILLDETAILS.Name = "GRIDBILLDETAILS"
        Me.GRIDBILLDETAILS.Size = New System.Drawing.Size(1488, 417)
        Me.GRIDBILLDETAILS.TabIndex = 775
        Me.GRIDBILLDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDBILL, Me.GridView1})
        '
        'GRIDBILL
        '
        Me.GRIDBILL.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILL.Appearance.Row.Options.UseFont = True
        Me.GRIDBILL.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GNAME, Me.GRATE1, Me.GRATE2, Me.GRATE3, Me.GRATE4, Me.GRATE5, Me.GRATE6, Me.GRATE7, Me.GRATE8, Me.GRATE9, Me.GRATE10, Me.GRATE11, Me.GRATE12, Me.GRATE13, Me.GRATE14, Me.GRATE15})
        Me.GRIDBILL.GridControl = Me.GRIDBILLDETAILS
        Me.GRIDBILL.Name = "GRIDBILL"
        Me.GRIDBILL.OptionsBehavior.AutoExpandAllGroups = True
        Me.GRIDBILL.OptionsCustomization.AllowColumnMoving = False
        Me.GRIDBILL.OptionsCustomization.AllowGroup = False
        Me.GRIDBILL.OptionsCustomization.AllowQuickHideColumns = False
        Me.GRIDBILL.OptionsView.ColumnAutoWidth = False
        Me.GRIDBILL.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GRIDBILL.OptionsView.ShowAutoFilterRow = True
        Me.GRIDBILL.OptionsView.ShowGroupPanel = False
        '
        'GNAME
        '
        Me.GNAME.Caption = "Item Name"
        Me.GNAME.FieldName = "ITEMNAME"
        Me.GNAME.ImageIndex = 0
        Me.GNAME.Name = "GNAME"
        Me.GNAME.OptionsColumn.AllowEdit = False
        Me.GNAME.OptionsColumn.AllowFocus = False
        Me.GNAME.OptionsColumn.ReadOnly = True
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 0
        Me.GNAME.Width = 160
        '
        'GRATE1
        '
        Me.GRATE1.Caption = "RATE1"
        Me.GRATE1.FieldName = "RATE1"
        Me.GRATE1.Name = "GRATE1"
        Me.GRATE1.Visible = True
        Me.GRATE1.VisibleIndex = 1
        Me.GRATE1.Width = 85
        '
        'GRATE2
        '
        Me.GRATE2.Caption = "RATE2"
        Me.GRATE2.FieldName = "RATE2"
        Me.GRATE2.Name = "GRATE2"
        Me.GRATE2.Visible = True
        Me.GRATE2.VisibleIndex = 2
        Me.GRATE2.Width = 85
        '
        'GRATE3
        '
        Me.GRATE3.Caption = "RATE3"
        Me.GRATE3.FieldName = "RATE3"
        Me.GRATE3.Name = "GRATE3"
        Me.GRATE3.Visible = True
        Me.GRATE3.VisibleIndex = 3
        Me.GRATE3.Width = 85
        '
        'GRATE4
        '
        Me.GRATE4.Caption = "RATE4"
        Me.GRATE4.FieldName = "RATE4"
        Me.GRATE4.Name = "GRATE4"
        Me.GRATE4.Visible = True
        Me.GRATE4.VisibleIndex = 4
        Me.GRATE4.Width = 85
        '
        'GRATE5
        '
        Me.GRATE5.Caption = "RATE5"
        Me.GRATE5.FieldName = "RATE5"
        Me.GRATE5.Name = "GRATE5"
        Me.GRATE5.Visible = True
        Me.GRATE5.VisibleIndex = 5
        Me.GRATE5.Width = 85
        '
        'GRATE6
        '
        Me.GRATE6.Caption = "RATE6"
        Me.GRATE6.FieldName = "RATE6"
        Me.GRATE6.Name = "GRATE6"
        Me.GRATE6.Visible = True
        Me.GRATE6.VisibleIndex = 6
        Me.GRATE6.Width = 85
        '
        'GRATE7
        '
        Me.GRATE7.Caption = "RATE7"
        Me.GRATE7.FieldName = "RATE7"
        Me.GRATE7.Name = "GRATE7"
        Me.GRATE7.Visible = True
        Me.GRATE7.VisibleIndex = 7
        Me.GRATE7.Width = 85
        '
        'GRATE8
        '
        Me.GRATE8.Caption = "RATE8"
        Me.GRATE8.FieldName = "RATE8"
        Me.GRATE8.Name = "GRATE8"
        Me.GRATE8.Visible = True
        Me.GRATE8.VisibleIndex = 8
        Me.GRATE8.Width = 85
        '
        'GRATE9
        '
        Me.GRATE9.Caption = "RATE9"
        Me.GRATE9.FieldName = "RATE9"
        Me.GRATE9.Name = "GRATE9"
        Me.GRATE9.Visible = True
        Me.GRATE9.VisibleIndex = 9
        Me.GRATE9.Width = 85
        '
        'GRATE10
        '
        Me.GRATE10.Caption = "RATE10"
        Me.GRATE10.FieldName = "RATE10"
        Me.GRATE10.Name = "GRATE10"
        Me.GRATE10.Visible = True
        Me.GRATE10.VisibleIndex = 10
        Me.GRATE10.Width = 85
        '
        'GRATE11
        '
        Me.GRATE11.Caption = "RATE11"
        Me.GRATE11.FieldName = "RATE11"
        Me.GRATE11.Name = "GRATE11"
        Me.GRATE11.Visible = True
        Me.GRATE11.VisibleIndex = 11
        Me.GRATE11.Width = 85
        '
        'GRATE12
        '
        Me.GRATE12.Caption = "RATE12"
        Me.GRATE12.FieldName = "RATE12"
        Me.GRATE12.Name = "GRATE12"
        Me.GRATE12.Visible = True
        Me.GRATE12.VisibleIndex = 12
        Me.GRATE12.Width = 85
        '
        'GRATE13
        '
        Me.GRATE13.Caption = "RATE13"
        Me.GRATE13.FieldName = "RATE13"
        Me.GRATE13.Name = "GRATE13"
        Me.GRATE13.Visible = True
        Me.GRATE13.VisibleIndex = 13
        Me.GRATE13.Width = 85
        '
        'GRATE14
        '
        Me.GRATE14.Caption = "RATE14"
        Me.GRATE14.FieldName = "RATE14"
        Me.GRATE14.Name = "GRATE14"
        Me.GRATE14.Visible = True
        Me.GRATE14.VisibleIndex = 14
        Me.GRATE14.Width = 85
        '
        'GRATE15
        '
        Me.GRATE15.Caption = "RATE15"
        Me.GRATE15.FieldName = "RATE15"
        Me.GRATE15.Name = "GRATE15"
        Me.GRATE15.Visible = True
        Me.GRATE15.VisibleIndex = 15
        Me.GRATE15.Width = 85
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GRIDBILLDETAILS
        Me.GridView1.Name = "GridView1"
        '
        'TXTRATE5
        '
        Me.TXTRATE5.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE5.Enabled = False
        Me.TXTRATE5.Location = New System.Drawing.Point(521, 3)
        Me.TXTRATE5.Name = "TXTRATE5"
        Me.TXTRATE5.ReadOnly = True
        Me.TXTRATE5.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE5.TabIndex = 780
        Me.TXTRATE5.TabStop = False
        Me.TXTRATE5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE1
        '
        Me.TXTRATE1.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE1.Enabled = False
        Me.TXTRATE1.Location = New System.Drawing.Point(181, 3)
        Me.TXTRATE1.Name = "TXTRATE1"
        Me.TXTRATE1.ReadOnly = True
        Me.TXTRATE1.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE1.TabIndex = 776
        Me.TXTRATE1.TabStop = False
        Me.TXTRATE1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE10
        '
        Me.TXTRATE10.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE10.Enabled = False
        Me.TXTRATE10.Location = New System.Drawing.Point(946, 3)
        Me.TXTRATE10.Name = "TXTRATE10"
        Me.TXTRATE10.ReadOnly = True
        Me.TXTRATE10.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE10.TabIndex = 785
        Me.TXTRATE10.TabStop = False
        Me.TXTRATE10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE2
        '
        Me.TXTRATE2.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE2.Enabled = False
        Me.TXTRATE2.Location = New System.Drawing.Point(266, 3)
        Me.TXTRATE2.Name = "TXTRATE2"
        Me.TXTRATE2.ReadOnly = True
        Me.TXTRATE2.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE2.TabIndex = 777
        Me.TXTRATE2.TabStop = False
        Me.TXTRATE2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE9
        '
        Me.TXTRATE9.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE9.Enabled = False
        Me.TXTRATE9.Location = New System.Drawing.Point(861, 3)
        Me.TXTRATE9.Name = "TXTRATE9"
        Me.TXTRATE9.ReadOnly = True
        Me.TXTRATE9.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE9.TabIndex = 784
        Me.TXTRATE9.TabStop = False
        Me.TXTRATE9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE3
        '
        Me.TXTRATE3.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE3.Enabled = False
        Me.TXTRATE3.Location = New System.Drawing.Point(351, 3)
        Me.TXTRATE3.Name = "TXTRATE3"
        Me.TXTRATE3.ReadOnly = True
        Me.TXTRATE3.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE3.TabIndex = 778
        Me.TXTRATE3.TabStop = False
        Me.TXTRATE3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE8
        '
        Me.TXTRATE8.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE8.Enabled = False
        Me.TXTRATE8.Location = New System.Drawing.Point(776, 3)
        Me.TXTRATE8.Name = "TXTRATE8"
        Me.TXTRATE8.ReadOnly = True
        Me.TXTRATE8.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE8.TabIndex = 783
        Me.TXTRATE8.TabStop = False
        Me.TXTRATE8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE4
        '
        Me.TXTRATE4.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE4.Enabled = False
        Me.TXTRATE4.Location = New System.Drawing.Point(436, 3)
        Me.TXTRATE4.Name = "TXTRATE4"
        Me.TXTRATE4.ReadOnly = True
        Me.TXTRATE4.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE4.TabIndex = 779
        Me.TXTRATE4.TabStop = False
        Me.TXTRATE4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE7
        '
        Me.TXTRATE7.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE7.Enabled = False
        Me.TXTRATE7.Location = New System.Drawing.Point(691, 3)
        Me.TXTRATE7.Name = "TXTRATE7"
        Me.TXTRATE7.ReadOnly = True
        Me.TXTRATE7.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE7.TabIndex = 782
        Me.TXTRATE7.TabStop = False
        Me.TXTRATE7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TXTRATE6
        '
        Me.TXTRATE6.BackColor = System.Drawing.Color.Linen
        Me.TXTRATE6.Enabled = False
        Me.TXTRATE6.Location = New System.Drawing.Point(606, 3)
        Me.TXTRATE6.Name = "TXTRATE6"
        Me.TXTRATE6.ReadOnly = True
        Me.TXTRATE6.Size = New System.Drawing.Size(85, 23)
        Me.TXTRATE6.TabIndex = 781
        Me.TXTRATE6.TabStop = False
        Me.TXTRATE6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(517, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 14)
        Me.Label2.TabIndex = 771
        Me.Label2.Text = "Rate From"
        '
        'CMBRATEFROM
        '
        Me.CMBRATEFROM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBRATEFROM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBRATEFROM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBRATEFROM.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBRATEFROM.FormattingEnabled = True
        Me.CMBRATEFROM.Items.AddRange(New Object() {"RATE1", "RATE2", "RATE3", "RATE4", "RATE5", "RATE6", "RATE7", "RATE8", "RATE9", "RATE10", "RATE11", "RATE12", "RATE13", "RATE14", "RATE15"})
        Me.CMBRATEFROM.Location = New System.Drawing.Point(581, 32)
        Me.CMBRATEFROM.MaxDropDownItems = 14
        Me.CMBRATEFROM.Name = "CMBRATEFROM"
        Me.CMBRATEFROM.Size = New System.Drawing.Size(76, 22)
        Me.CMBRATEFROM.TabIndex = 770
        '
        'GPITEMNAME
        '
        Me.GPITEMNAME.BackColor = System.Drawing.Color.Transparent
        Me.GPITEMNAME.Controls.Add(Me.CMBCODE)
        Me.GPITEMNAME.Controls.Add(Me.CHKSELECTITEM)
        Me.GPITEMNAME.Controls.Add(Me.GRIDITEMDETAILS)
        Me.GPITEMNAME.Location = New System.Drawing.Point(1093, 40)
        Me.GPITEMNAME.Name = "GPITEMNAME"
        Me.GPITEMNAME.Size = New System.Drawing.Size(264, 500)
        Me.GPITEMNAME.TabIndex = 759
        Me.GPITEMNAME.TabStop = False
        Me.GPITEMNAME.Text = "Item Name"
        Me.GPITEMNAME.Visible = False
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Location = New System.Drawing.Point(158, -6)
        Me.CMBCODE.MaxDropDownItems = 14
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(35, 22)
        Me.CMBCODE.TabIndex = 767
        Me.CMBCODE.Visible = False
        '
        'CHKSELECTITEM
        '
        Me.CHKSELECTITEM.AutoSize = True
        Me.CHKSELECTITEM.BackColor = System.Drawing.Color.Transparent
        Me.CHKSELECTITEM.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSELECTITEM.ForeColor = System.Drawing.Color.Black
        Me.CHKSELECTITEM.Location = New System.Drawing.Point(34, 22)
        Me.CHKSELECTITEM.Name = "CHKSELECTITEM"
        Me.CHKSELECTITEM.Size = New System.Drawing.Size(77, 18)
        Me.CHKSELECTITEM.TabIndex = 0
        Me.CHKSELECTITEM.Text = "Select All"
        Me.CHKSELECTITEM.UseVisualStyleBackColor = False
        '
        'GRIDITEMDETAILS
        '
        Me.GRIDITEMDETAILS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDITEMDETAILS.Location = New System.Drawing.Point(6, 20)
        Me.GRIDITEMDETAILS.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDITEMDETAILS.MainView = Me.GRIDITEM
        Me.GRIDITEMDETAILS.Name = "GRIDITEMDETAILS"
        Me.GRIDITEMDETAILS.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit4})
        Me.GRIDITEMDETAILS.Size = New System.Drawing.Size(246, 474)
        Me.GRIDITEMDETAILS.TabIndex = 1
        Me.GRIDITEMDETAILS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDITEM})
        '
        'GRIDITEM
        '
        Me.GRIDITEM.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDITEM.Appearance.Row.Options.UseFont = True
        Me.GRIDITEM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GITEMCHK, Me.GITEMNAME})
        Me.GRIDITEM.GridControl = Me.GRIDITEMDETAILS
        Me.GRIDITEM.Name = "GRIDITEM"
        Me.GRIDITEM.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDITEM.OptionsView.ColumnAutoWidth = False
        Me.GRIDITEM.OptionsView.ShowAutoFilterRow = True
        Me.GRIDITEM.OptionsView.ShowGroupPanel = False
        '
        'GITEMCHK
        '
        Me.GITEMCHK.ColumnEdit = Me.RepositoryItemCheckEdit4
        Me.GITEMCHK.FieldName = "CHK"
        Me.GITEMCHK.Name = "GITEMCHK"
        Me.GITEMCHK.OptionsColumn.ShowCaption = False
        Me.GITEMCHK.Visible = True
        Me.GITEMCHK.VisibleIndex = 0
        Me.GITEMCHK.Width = 35
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        Me.RepositoryItemCheckEdit4.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GITEMNAME
        '
        Me.GITEMNAME.Caption = "Item Name"
        Me.GITEMNAME.FieldName = "ITEMNAME"
        Me.GITEMNAME.Name = "GITEMNAME"
        Me.GITEMNAME.Visible = True
        Me.GITEMNAME.VisibleIndex = 1
        Me.GITEMNAME.Width = 200
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(319, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 14)
        Me.Label1.TabIndex = 330
        Me.Label1.Text = "Rate"
        '
        'TXTRATEPER
        '
        Me.TXTRATEPER.Location = New System.Drawing.Point(431, 32)
        Me.TXTRATEPER.Name = "TXTRATEPER"
        Me.TXTRATEPER.Size = New System.Drawing.Size(74, 23)
        Me.TXTRATEPER.TabIndex = 329
        '
        'CMBRATETYPE
        '
        Me.CMBRATETYPE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBRATETYPE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBRATETYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBRATETYPE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBRATETYPE.FormattingEnabled = True
        Me.CMBRATETYPE.Items.AddRange(New Object() {"", "RATE2", "RATE3", "RATE4", "RATE5", "RATE6", "RATE7", "RATE8", "RATE9", "RATE10", "RATE11", "RATE12", "RATE13", "RATE14", "RATE15"})
        Me.CMBRATETYPE.Location = New System.Drawing.Point(353, 32)
        Me.CMBRATETYPE.MaxDropDownItems = 14
        Me.CMBRATETYPE.Name = "CMBRATETYPE"
        Me.CMBRATETYPE.Size = New System.Drawing.Size(76, 22)
        Me.CMBRATETYPE.TabIndex = 328
        '
        'cmbcategory
        '
        Me.cmbcategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbcategory.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcategory.FormattingEnabled = True
        Me.cmbcategory.Location = New System.Drawing.Point(71, 32)
        Me.cmbcategory.MaxDropDownItems = 14
        Me.cmbcategory.Name = "cmbcategory"
        Me.cmbcategory.Size = New System.Drawing.Size(207, 22)
        Me.cmbcategory.TabIndex = 326
        '
        'lblcategory
        '
        Me.lblcategory.AutoSize = True
        Me.lblcategory.BackColor = System.Drawing.Color.Transparent
        Me.lblcategory.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcategory.Location = New System.Drawing.Point(15, 36)
        Me.lblcategory.Name = "lblcategory"
        Me.lblcategory.Size = New System.Drawing.Size(53, 14)
        Me.lblcategory.TabIndex = 327
        Me.lblcategory.Text = "Category"
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.Black
        Me.CMDREFRESH.Location = New System.Drawing.Point(618, 532)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREFRESH.TabIndex = 325
        Me.CMDREFRESH.Text = "&Refresh"
        Me.CMDREFRESH.UseVisualStyleBackColor = False
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(532, 532)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 323
        Me.CMDOK.Text = "&Save"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(704, 532)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 322
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripButton, Me.toolStripSeparator, Me.ExcelExport, Me.ToolStripSeparator2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1369, 25)
        Me.ToolStrip1.TabIndex = 318
        Me.ToolStrip1.Text = "ToolStrip1"
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
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'ExcelExport
        '
        Me.ExcelExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ExcelExport.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        Me.ExcelExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ExcelExport.Name = "ExcelExport"
        Me.ExcelExport.Size = New System.Drawing.Size(23, 22)
        Me.ExcelExport.Text = "&Export to Excel"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ItemPriceList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1369, 601)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "ItemPriceList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Item Price List"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GRIDBILLDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDBILL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GPITEMNAME.ResumeLayout(False)
        Me.GPITEMNAME.PerformLayout()
        CType(Me.GRIDITEMDETAILS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDREFRESH As System.Windows.Forms.Button
    Friend WithEvents CMDOK As System.Windows.Forms.Button
    Friend WithEvents CMDEXIT As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExcelExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmbcategory As System.Windows.Forms.ComboBox
    Friend WithEvents lblcategory As System.Windows.Forms.Label
    Friend WithEvents TXTRATEPER As System.Windows.Forms.TextBox
    Friend WithEvents CMBRATETYPE As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents GPITEMNAME As GroupBox
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents CHKSELECTITEM As CheckBox
    Private WithEvents GRIDITEMDETAILS As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GITEMCHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GITEMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Label2 As Label
    Friend WithEvents CMBRATEFROM As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TXTRATE15 As TextBox
    Friend WithEvents TXTRATE14 As TextBox
    Friend WithEvents TXTRATE13 As TextBox
    Friend WithEvents TXTRATE12 As TextBox
    Friend WithEvents TXTRATE11 As TextBox
    Friend WithEvents GRIDBILLDETAILS As DevExpress.XtraGrid.GridControl
    Friend WithEvents GRIDBILL As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRATE15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TXTRATE5 As TextBox
    Friend WithEvents TXTRATE1 As TextBox
    Friend WithEvents TXTRATE10 As TextBox
    Friend WithEvents TXTRATE2 As TextBox
    Friend WithEvents TXTRATE9 As TextBox
    Friend WithEvents TXTRATE3 As TextBox
    Friend WithEvents TXTRATE8 As TextBox
    Friend WithEvents TXTRATE4 As TextBox
    Friend WithEvents TXTRATE7 As TextBox
    Friend WithEvents TXTRATE6 As TextBox
End Class
