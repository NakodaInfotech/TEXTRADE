<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class YarnJobOrderWarpDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(YarnJobOrderWarpDetails))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.CMDSAVELAYOUT = New System.Windows.Forms.Button()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gsrno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARPGRIDSYM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARPYARNQUALITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARPDENIER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARPMILLNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARPSHADE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARPPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARPBE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARPTE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARPWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKEDIT = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.TOOLMAIL = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.lbl = New System.Windows.Forms.Label()
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMDSAVELAYOUT)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 1
        '
        'CMDSAVELAYOUT
        '
        Me.CMDSAVELAYOUT.BackColor = System.Drawing.Color.Transparent
        Me.CMDSAVELAYOUT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSAVELAYOUT.FlatAppearance.BorderSize = 0
        Me.CMDSAVELAYOUT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSAVELAYOUT.ForeColor = System.Drawing.Color.Black
        Me.CMDSAVELAYOUT.Location = New System.Drawing.Point(449, 541)
        Me.CMDSAVELAYOUT.Name = "CMDSAVELAYOUT"
        Me.CMDSAVELAYOUT.Size = New System.Drawing.Size(80, 28)
        Me.CMDSAVELAYOUT.TabIndex = 448
        Me.CMDSAVELAYOUT.Text = "Save Layout"
        Me.CMDSAVELAYOUT.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(22, 61)
        Me.gridbilldetails.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKEDIT})
        Me.gridbilldetails.Size = New System.Drawing.Size(1200, 472)
        Me.gridbilldetails.TabIndex = 0
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gsrno, Me.GWARPGRIDSYM, Me.GWARPYARNQUALITY, Me.GWARPDENIER, Me.GWARPMILLNAME, Me.GWARPSHADE, Me.GWARPPE, Me.GWARPBE, Me.GWARPTE, Me.GWARPWT})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.gridbill.OptionsSelection.MultiSelect = True
        Me.gridbill.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        '
        'gsrno
        '
        Me.gsrno.Caption = "Sr. No"
        Me.gsrno.FieldName = "JOBNO"
        Me.gsrno.ImageOptions.ImageIndex = 1
        Me.gsrno.Name = "gsrno"
        Me.gsrno.OptionsColumn.AllowEdit = False
        Me.gsrno.Visible = True
        Me.gsrno.VisibleIndex = 1
        Me.gsrno.Width = 60
        '
        'GWARPGRIDSYM
        '
        Me.GWARPGRIDSYM.Caption = "Grid Sym"
        Me.GWARPGRIDSYM.FieldName = "WARPGRIDSYM"
        Me.GWARPGRIDSYM.Name = "GWARPGRIDSYM"
        Me.GWARPGRIDSYM.OptionsColumn.AllowEdit = False
        Me.GWARPGRIDSYM.Visible = True
        Me.GWARPGRIDSYM.VisibleIndex = 2
        '
        'GWARPYARNQUALITY
        '
        Me.GWARPYARNQUALITY.Caption = "Yarn Quality"
        Me.GWARPYARNQUALITY.FieldName = "WARPYARNQUALITY"
        Me.GWARPYARNQUALITY.ImageOptions.ImageIndex = 0
        Me.GWARPYARNQUALITY.Name = "GWARPYARNQUALITY"
        Me.GWARPYARNQUALITY.OptionsColumn.AllowEdit = False
        Me.GWARPYARNQUALITY.Visible = True
        Me.GWARPYARNQUALITY.VisibleIndex = 3
        Me.GWARPYARNQUALITY.Width = 230
        '
        'GWARPDENIER
        '
        Me.GWARPDENIER.Caption = "Denier"
        Me.GWARPDENIER.FieldName = "WARPDENIER"
        Me.GWARPDENIER.Name = "GWARPDENIER"
        Me.GWARPDENIER.OptionsColumn.AllowEdit = False
        Me.GWARPDENIER.Visible = True
        Me.GWARPDENIER.VisibleIndex = 4
        Me.GWARPDENIER.Width = 120
        '
        'GWARPMILLNAME
        '
        Me.GWARPMILLNAME.Caption = "Mill Name"
        Me.GWARPMILLNAME.FieldName = "WARPMILLNAME"
        Me.GWARPMILLNAME.Name = "GWARPMILLNAME"
        Me.GWARPMILLNAME.OptionsColumn.AllowEdit = False
        Me.GWARPMILLNAME.Visible = True
        Me.GWARPMILLNAME.VisibleIndex = 5
        Me.GWARPMILLNAME.Width = 200
        '
        'GWARPSHADE
        '
        Me.GWARPSHADE.Caption = "Shade"
        Me.GWARPSHADE.FieldName = "WARPSHADE"
        Me.GWARPSHADE.Name = "GWARPSHADE"
        Me.GWARPSHADE.OptionsColumn.AllowEdit = False
        Me.GWARPSHADE.Visible = True
        Me.GWARPSHADE.VisibleIndex = 6
        Me.GWARPSHADE.Width = 120
        '
        'GWARPPE
        '
        Me.GWARPPE.Caption = "PE"
        Me.GWARPPE.FieldName = "WARPPE"
        Me.GWARPPE.Name = "GWARPPE"
        Me.GWARPPE.OptionsColumn.AllowEdit = False
        Me.GWARPPE.Visible = True
        Me.GWARPPE.VisibleIndex = 7
        Me.GWARPPE.Width = 80
        '
        'GWARPBE
        '
        Me.GWARPBE.Caption = "BE"
        Me.GWARPBE.FieldName = "GWARPBE"
        Me.GWARPBE.Name = "GWARPBE"
        Me.GWARPBE.OptionsColumn.AllowEdit = False
        Me.GWARPBE.Visible = True
        Me.GWARPBE.VisibleIndex = 8
        Me.GWARPBE.Width = 80
        '
        'GWARPTE
        '
        Me.GWARPTE.Caption = "TE"
        Me.GWARPTE.FieldName = "WARPTE"
        Me.GWARPTE.Name = "GWARPTE"
        Me.GWARPTE.OptionsColumn.AllowEdit = False
        Me.GWARPTE.Visible = True
        Me.GWARPTE.VisibleIndex = 9
        Me.GWARPTE.Width = 80
        '
        'GWARPWT
        '
        Me.GWARPWT.Caption = "WT"
        Me.GWARPWT.FieldName = "GWARPWT"
        Me.GWARPWT.Name = "GWARPWT"
        Me.GWARPWT.OptionsColumn.AllowEdit = False
        Me.GWARPWT.Visible = True
        Me.GWARPWT.VisibleIndex = 10
        Me.GWARPWT.Width = 80
        '
        'CHKEDIT
        '
        Me.CHKEDIT.AutoHeight = False
        Me.CHKEDIT.Name = "CHKEDIT"
        Me.CHKEDIT.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(535, 541)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 2
        Me.CMDOK.Text = "&Ok"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.Color.Transparent
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdcancel.FlatAppearance.BorderSize = 0
        Me.cmdcancel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.Black
        Me.cmdcancel.Location = New System.Drawing.Point(619, 540)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 3
        Me.cmdcancel.Text = "E&xit"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.toolStripSeparator, Me.PrintToolStripButton, Me.TOOLMAIL, Me.ToolStripButton2, Me.ToolStripSeparator1, Me.TOOLREFRESH})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        Me.PrintToolStripButton.Visible = False
        '
        'TOOLMAIL
        '
        Me.TOOLMAIL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLMAIL.Image = Global.TEXTRADE.My.Resources.Resources.MAIL_IMAGE
        Me.TOOLMAIL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLMAIL.Name = "TOOLMAIL"
        Me.TOOLMAIL.Size = New System.Drawing.Size(23, 22)
        Me.TOOLMAIL.Text = "Mail Invoice Directly"
        Me.TOOLMAIL.Visible = False
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl.Location = New System.Drawing.Point(20, 44)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(168, 14)
        Me.lbl.TabIndex = 251
        Me.lbl.Text = "Select an Job Order to Change"
        '
        'TOOLREFRESH
        '
        Me.TOOLREFRESH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLREFRESH.Image = Global.TEXTRADE.My.Resources.Resources.refresh1
        Me.TOOLREFRESH.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLREFRESH.Name = "TOOLREFRESH"
        Me.TOOLREFRESH.Size = New System.Drawing.Size(23, 22)
        Me.TOOLREFRESH.Text = "ToolStripButton3"
        '
        'YarnJobOrderWarpDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "YarnJobOrderWarpDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "JobOrderWarpDetails"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDSAVELAYOUT As Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents gsrno As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARPGRIDSYM As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GWARPYARNQUALITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARPDENIER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARPMILLNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARPSHADE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARPPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARPBE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARPTE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARPWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKEDIT As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CMDOK As Button
    Friend WithEvents cmdcancel As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents TOOLMAIL As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents lbl As Label
    Friend WithEvents TOOLREFRESH As ToolStripButton
End Class
