<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SelectBillsforITC
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
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.CMDEXPORT = New System.Windows.Forms.Button()
        Me.DTTILLDATE = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVNOBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVDATEBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVNOPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVDATEPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTAXABLEAMTBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCGSTAMTBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSGSTAMTBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GIGSTAMTBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGTOTALBOOKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTAXABLEAMTPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCGSTAMTPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSGSTAMTPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GIGSTAMTPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGTOTALPORTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRECOSTATUS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMDEXPORT)
        Me.BlendPanel1.Controls.Add(Me.DTTILLDATE)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1284, 711)
        Me.BlendPanel1.TabIndex = 0
        '
        'CMDEXPORT
        '
        Me.CMDEXPORT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXPORT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXPORT.FlatAppearance.BorderSize = 0
        Me.CMDEXPORT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXPORT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXPORT.Location = New System.Drawing.Point(643, 678)
        Me.CMDEXPORT.Name = "CMDEXPORT"
        Me.CMDEXPORT.Size = New System.Drawing.Size(80, 27)
        Me.CMDEXPORT.TabIndex = 2
        Me.CMDEXPORT.Text = "&Export"
        Me.CMDEXPORT.UseVisualStyleBackColor = False
        '
        'DTTILLDATE
        '
        Me.DTTILLDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTTILLDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTTILLDATE.Location = New System.Drawing.Point(446, 678)
        Me.DTTILLDATE.Name = "DTTILLDATE"
        Me.DTTILLDATE.Size = New System.Drawing.Size(91, 23)
        Me.DTTILLDATE.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(389, 682)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 15)
        Me.Label9.TabIndex = 767
        Me.Label9.Text = "Till Date"
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.Black
        Me.CMDREFRESH.Location = New System.Drawing.Point(557, 678)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 27)
        Me.CMDREFRESH.TabIndex = 1
        Me.CMDREFRESH.Text = "&Refresh"
        Me.CMDREFRESH.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(815, 678)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 27)
        Me.cmdexit.TabIndex = 4
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(729, 678)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 27)
        Me.CMDOK.TabIndex = 3
        Me.CMDOK.Text = "&Ok"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.EmbeddedNavigator.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.gridbilldetails.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(14, 12)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.Size = New System.Drawing.Size(1257, 660)
        Me.gridbilldetails.TabIndex = 5
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GBILLNO, Me.GTYPE, Me.GGSTIN, Me.GNAME, Me.GINVNOBOOKS, Me.GINVDATEBOOKS, Me.GINVNOPORTAL, Me.GINVDATEPORTAL, Me.GTAXABLEAMTBOOKS, Me.GCGSTAMTBOOKS, Me.GSGSTAMTBOOKS, Me.GIGSTAMTBOOKS, Me.GGTOTALBOOKS, Me.GTAXABLEAMTPORTAL, Me.GCGSTAMTPORTAL, Me.GSGSTAMTPORTAL, Me.GIGSTAMTPORTAL, Me.GGTOTALPORTAL, Me.GRECOSTATUS})
        Me.gridbill.CustomizationFormBounds = New System.Drawing.Rectangle(688, 311, 208, 184)
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AutoExpandAllGroups = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsCustomization.AllowColumnMoving = False
        Me.gridbill.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.gridbill.OptionsSelection.MultiSelect = True
        Me.gridbill.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
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
        '
        'GTYPE
        '
        Me.GTYPE.Caption = "Type"
        Me.GTYPE.FieldName = "TYPE"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.Visible = True
        Me.GTYPE.VisibleIndex = 2
        '
        'GGSTIN
        '
        Me.GGSTIN.Caption = "Party GSTIN"
        Me.GGSTIN.FieldName = "GSTIN"
        Me.GGSTIN.Name = "GGSTIN"
        Me.GGSTIN.Visible = True
        Me.GGSTIN.VisibleIndex = 3
        Me.GGSTIN.Width = 120
        '
        'GNAME
        '
        Me.GNAME.Caption = "Party Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 4
        Me.GNAME.Width = 200
        '
        'GINVNOBOOKS
        '
        Me.GINVNOBOOKS.Caption = "Invoice No (Books)"
        Me.GINVNOBOOKS.FieldName = "INVNOBOOKS"
        Me.GINVNOBOOKS.Name = "GINVNOBOOKS"
        Me.GINVNOBOOKS.Visible = True
        Me.GINVNOBOOKS.VisibleIndex = 5
        Me.GINVNOBOOKS.Width = 120
        '
        'GINVDATEBOOKS
        '
        Me.GINVDATEBOOKS.Caption = "Inv Dt. (Books)"
        Me.GINVDATEBOOKS.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GINVDATEBOOKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GINVDATEBOOKS.FieldName = "INVDATEBOOKS"
        Me.GINVDATEBOOKS.Name = "GINVDATEBOOKS"
        Me.GINVDATEBOOKS.Visible = True
        Me.GINVDATEBOOKS.VisibleIndex = 6
        Me.GINVDATEBOOKS.Width = 100
        '
        'GINVNOPORTAL
        '
        Me.GINVNOPORTAL.Caption = "Invoice No (Portal)"
        Me.GINVNOPORTAL.FieldName = "INVNOPORTAL"
        Me.GINVNOPORTAL.Name = "GINVNOPORTAL"
        Me.GINVNOPORTAL.Visible = True
        Me.GINVNOPORTAL.VisibleIndex = 7
        Me.GINVNOPORTAL.Width = 120
        '
        'GINVDATEPORTAL
        '
        Me.GINVDATEPORTAL.Caption = "Inv Dt. (Portal)"
        Me.GINVDATEPORTAL.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GINVDATEPORTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GINVDATEPORTAL.FieldName = "INVDATEPORTAL"
        Me.GINVDATEPORTAL.Name = "GINVDATEPORTAL"
        Me.GINVDATEPORTAL.Visible = True
        Me.GINVDATEPORTAL.VisibleIndex = 8
        Me.GINVDATEPORTAL.Width = 100
        '
        'GTAXABLEAMTBOOKS
        '
        Me.GTAXABLEAMTBOOKS.Caption = "Taxable Amt (Books)"
        Me.GTAXABLEAMTBOOKS.DisplayFormat.FormatString = "0.00"
        Me.GTAXABLEAMTBOOKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTAXABLEAMTBOOKS.FieldName = "TAXABLEAMTBOOKS"
        Me.GTAXABLEAMTBOOKS.Name = "GTAXABLEAMTBOOKS"
        Me.GTAXABLEAMTBOOKS.Visible = True
        Me.GTAXABLEAMTBOOKS.VisibleIndex = 9
        Me.GTAXABLEAMTBOOKS.Width = 120
        '
        'GCGSTAMTBOOKS
        '
        Me.GCGSTAMTBOOKS.Caption = "CGST Amt (Books)"
        Me.GCGSTAMTBOOKS.DisplayFormat.FormatString = "0.00"
        Me.GCGSTAMTBOOKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCGSTAMTBOOKS.FieldName = "CGSTAMTBOOKS"
        Me.GCGSTAMTBOOKS.Name = "GCGSTAMTBOOKS"
        Me.GCGSTAMTBOOKS.Visible = True
        Me.GCGSTAMTBOOKS.VisibleIndex = 10
        Me.GCGSTAMTBOOKS.Width = 120
        '
        'GSGSTAMTBOOKS
        '
        Me.GSGSTAMTBOOKS.Caption = "SGST Amt (Books)"
        Me.GSGSTAMTBOOKS.DisplayFormat.FormatString = "0.00"
        Me.GSGSTAMTBOOKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GSGSTAMTBOOKS.FieldName = "SGSTAMTBOOKS"
        Me.GSGSTAMTBOOKS.Name = "GSGSTAMTBOOKS"
        Me.GSGSTAMTBOOKS.Visible = True
        Me.GSGSTAMTBOOKS.VisibleIndex = 11
        Me.GSGSTAMTBOOKS.Width = 120
        '
        'GIGSTAMTBOOKS
        '
        Me.GIGSTAMTBOOKS.Caption = "IGST Amt (Books)"
        Me.GIGSTAMTBOOKS.DisplayFormat.FormatString = "0.00"
        Me.GIGSTAMTBOOKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GIGSTAMTBOOKS.FieldName = "IGSTAMTBOOKS"
        Me.GIGSTAMTBOOKS.Name = "GIGSTAMTBOOKS"
        Me.GIGSTAMTBOOKS.Visible = True
        Me.GIGSTAMTBOOKS.VisibleIndex = 12
        Me.GIGSTAMTBOOKS.Width = 120
        '
        'GGTOTALBOOKS
        '
        Me.GGTOTALBOOKS.Caption = "G Total (Books)"
        Me.GGTOTALBOOKS.DisplayFormat.FormatString = "0.00"
        Me.GGTOTALBOOKS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGTOTALBOOKS.FieldName = "GRANDTOTALBOOKS"
        Me.GGTOTALBOOKS.Name = "GGTOTALBOOKS"
        Me.GGTOTALBOOKS.Visible = True
        Me.GGTOTALBOOKS.VisibleIndex = 13
        Me.GGTOTALBOOKS.Width = 120
        '
        'GTAXABLEAMTPORTAL
        '
        Me.GTAXABLEAMTPORTAL.Caption = "Taxable Amt (Portal)"
        Me.GTAXABLEAMTPORTAL.DisplayFormat.FormatString = "0.00"
        Me.GTAXABLEAMTPORTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTAXABLEAMTPORTAL.FieldName = "TAXABLEAMTPORTAL"
        Me.GTAXABLEAMTPORTAL.Name = "GTAXABLEAMTPORTAL"
        Me.GTAXABLEAMTPORTAL.Visible = True
        Me.GTAXABLEAMTPORTAL.VisibleIndex = 14
        Me.GTAXABLEAMTPORTAL.Width = 120
        '
        'GCGSTAMTPORTAL
        '
        Me.GCGSTAMTPORTAL.Caption = "CGST Amt (Portal)"
        Me.GCGSTAMTPORTAL.DisplayFormat.FormatString = "0.00"
        Me.GCGSTAMTPORTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCGSTAMTPORTAL.FieldName = "CGSTAMTPORTAL"
        Me.GCGSTAMTPORTAL.Name = "GCGSTAMTPORTAL"
        Me.GCGSTAMTPORTAL.Visible = True
        Me.GCGSTAMTPORTAL.VisibleIndex = 15
        Me.GCGSTAMTPORTAL.Width = 120
        '
        'GSGSTAMTPORTAL
        '
        Me.GSGSTAMTPORTAL.Caption = "SGST Amt (Portal)"
        Me.GSGSTAMTPORTAL.DisplayFormat.FormatString = "0.00"
        Me.GSGSTAMTPORTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GSGSTAMTPORTAL.FieldName = "SGSTAMTPORTAL"
        Me.GSGSTAMTPORTAL.Name = "GSGSTAMTPORTAL"
        Me.GSGSTAMTPORTAL.Visible = True
        Me.GSGSTAMTPORTAL.VisibleIndex = 16
        Me.GSGSTAMTPORTAL.Width = 120
        '
        'GIGSTAMTPORTAL
        '
        Me.GIGSTAMTPORTAL.Caption = "IGST Amt (Portal)"
        Me.GIGSTAMTPORTAL.DisplayFormat.FormatString = "0.00"
        Me.GIGSTAMTPORTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GIGSTAMTPORTAL.FieldName = "IGSTAMTPORTAL"
        Me.GIGSTAMTPORTAL.Name = "GIGSTAMTPORTAL"
        Me.GIGSTAMTPORTAL.Visible = True
        Me.GIGSTAMTPORTAL.VisibleIndex = 17
        Me.GIGSTAMTPORTAL.Width = 120
        '
        'GGTOTALPORTAL
        '
        Me.GGTOTALPORTAL.Caption = "G Total (Portal)"
        Me.GGTOTALPORTAL.DisplayFormat.FormatString = "0.00"
        Me.GGTOTALPORTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGTOTALPORTAL.FieldName = "GRANDTOTALPORTAL"
        Me.GGTOTALPORTAL.Name = "GGTOTALPORTAL"
        Me.GGTOTALPORTAL.Visible = True
        Me.GGTOTALPORTAL.VisibleIndex = 18
        Me.GGTOTALPORTAL.Width = 120
        '
        'GRECOSTATUS
        '
        Me.GRECOSTATUS.Caption = "Reco Status"
        Me.GRECOSTATUS.FieldName = "RECOSTATUS"
        Me.GRECOSTATUS.Name = "GRECOSTATUS"
        Me.GRECOSTATUS.Visible = True
        Me.GRECOSTATUS.VisibleIndex = 19
        Me.GRECOSTATUS.Width = 120
        '
        'SelectBillsforITC
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1284, 711)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "SelectBillsforITC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Select Bills for ITC"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDREFRESH As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents CMDOK As Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGSTIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVNOBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVDATEBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVNOPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVDATEPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTAXABLEAMTBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCGSTAMTBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSGSTAMTBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GIGSTAMTBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGTOTALBOOKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTAXABLEAMTPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCGSTAMTPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSGSTAMTPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GIGSTAMTPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGTOTALPORTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRECOSTATUS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DTTILLDATE As DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents CMDEXPORT As Button
End Class
