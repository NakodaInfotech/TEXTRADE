<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TallyDataExportSalesInv
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
        Me.dtto = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtfrom = New System.Windows.Forms.DateTimePicker()
        Me.chkdate = New System.Windows.Forms.CheckBox()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GINVOICENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GITEMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GHSN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GQTY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GUNITPRICE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTAXVALUE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTRANS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPOSTAGECHRGS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLOADCHRGS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCGST = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSGST = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GISGT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGSTRATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSALESACCOUNT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCGSTLEDGER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSGSTLEDGER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GIGSTLEDGER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOUNTRY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSTATENAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GVOUCHERTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GUNITS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.TOOLEXCEL = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CMDSHOW = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.dtto)
        Me.BlendPanel1.Controls.Add(Me.Label7)
        Me.BlendPanel1.Controls.Add(Me.dtfrom)
        Me.BlendPanel1.Controls.Add(Me.chkdate)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.CMDSHOW)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 1
        '
        'dtto
        '
        Me.dtto.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtto.Location = New System.Drawing.Point(211, 28)
        Me.dtto.Name = "dtto"
        Me.dtto.Size = New System.Drawing.Size(87, 23)
        Me.dtto.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(184, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 15)
        Me.Label7.TabIndex = 260
        Me.Label7.Text = "To :"
        '
        'dtfrom
        '
        Me.dtfrom.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtfrom.Location = New System.Drawing.Point(90, 28)
        Me.dtfrom.Name = "dtfrom"
        Me.dtfrom.Size = New System.Drawing.Size(87, 23)
        Me.dtfrom.TabIndex = 1
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.BackColor = System.Drawing.Color.Transparent
        Me.chkdate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdate.ForeColor = System.Drawing.Color.Black
        Me.chkdate.Location = New System.Drawing.Point(32, 30)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(51, 19)
        Me.chkdate.TabIndex = 0
        Me.chkdate.Text = "Date"
        Me.chkdate.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(18, 59)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.Size = New System.Drawing.Size(1204, 478)
        Me.gridbilldetails.TabIndex = 4
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GINVOICENO, Me.GDATE, Me.GNAME, Me.GITEMNAME, Me.GHSN, Me.GQTY, Me.GUNITPRICE, Me.GTAXVALUE, Me.GTRANS, Me.GPOSTAGECHRGS, Me.GLOADCHRGS, Me.GCGST, Me.GSGST, Me.GISGT, Me.GINVTOTAL, Me.GGSTRATE, Me.GSALESACCOUNT, Me.GCGSTLEDGER, Me.GSGSTLEDGER, Me.GIGSTLEDGER, Me.GCOUNTRY, Me.GSTATENAME, Me.GVOUCHERTYPE, Me.GUNITS, Me.GGSTIN})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GINVOICENO
        '
        Me.GINVOICENO.Caption = "Invoice No"
        Me.GINVOICENO.FieldName = "INITIALS"
        Me.GINVOICENO.Name = "GINVOICENO"
        Me.GINVOICENO.Visible = True
        Me.GINVOICENO.VisibleIndex = 0
        Me.GINVOICENO.Width = 70
        '
        'GDATE
        '
        Me.GDATE.Caption = "Invoice Date"
        Me.GDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GDATE.FieldName = "DATE"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.Visible = True
        Me.GDATE.VisibleIndex = 1
        Me.GDATE.Width = 80
        '
        'GNAME
        '
        Me.GNAME.Caption = "Party Name"
        Me.GNAME.FieldName = "TALLYLEDGERNAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 2
        Me.GNAME.Width = 130
        '
        'GITEMNAME
        '
        Me.GITEMNAME.Caption = "Item"
        Me.GITEMNAME.FieldName = "TALLYITEMNAME"
        Me.GITEMNAME.Name = "GITEMNAME"
        Me.GITEMNAME.Visible = True
        Me.GITEMNAME.VisibleIndex = 3
        Me.GITEMNAME.Width = 130
        '
        'GHSN
        '
        Me.GHSN.Caption = "HSN Code"
        Me.GHSN.FieldName = "HSNCODE"
        Me.GHSN.Name = "GHSN"
        Me.GHSN.Visible = True
        Me.GHSN.VisibleIndex = 4
        Me.GHSN.Width = 70
        '
        'GQTY
        '
        Me.GQTY.Caption = "Quantity"
        Me.GQTY.DisplayFormat.FormatString = "0.00"
        Me.GQTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GQTY.FieldName = "QTY"
        Me.GQTY.Name = "GQTY"
        Me.GQTY.Visible = True
        Me.GQTY.VisibleIndex = 5
        Me.GQTY.Width = 60
        '
        'GUNITPRICE
        '
        Me.GUNITPRICE.Caption = "Unit Price"
        Me.GUNITPRICE.DisplayFormat.FormatString = "0.000"
        Me.GUNITPRICE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GUNITPRICE.FieldName = "RATE"
        Me.GUNITPRICE.Name = "GUNITPRICE"
        Me.GUNITPRICE.Visible = True
        Me.GUNITPRICE.VisibleIndex = 6
        Me.GUNITPRICE.Width = 60
        '
        'GTAXVALUE
        '
        Me.GTAXVALUE.Caption = "Taxable Value"
        Me.GTAXVALUE.DisplayFormat.FormatString = "0.00"
        Me.GTAXVALUE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTAXVALUE.FieldName = "TAXABLEAMT"
        Me.GTAXVALUE.Name = "GTAXVALUE"
        Me.GTAXVALUE.Visible = True
        Me.GTAXVALUE.VisibleIndex = 7
        '
        'GTRANS
        '
        Me.GTRANS.Caption = "Transportation Charges"
        Me.GTRANS.DisplayFormat.FormatString = "0.00"
        Me.GTRANS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTRANS.FieldName = "TRANSCHARGES"
        Me.GTRANS.Name = "GTRANS"
        Me.GTRANS.Visible = True
        Me.GTRANS.VisibleIndex = 8
        '
        'GPOSTAGECHRGS
        '
        Me.GPOSTAGECHRGS.Caption = "Postage Charges"
        Me.GPOSTAGECHRGS.DisplayFormat.FormatString = "0.00"
        Me.GPOSTAGECHRGS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPOSTAGECHRGS.FieldName = "POSTCHARGES"
        Me.GPOSTAGECHRGS.Name = "GPOSTAGECHRGS"
        Me.GPOSTAGECHRGS.Visible = True
        Me.GPOSTAGECHRGS.VisibleIndex = 9
        '
        'GLOADCHRGS
        '
        Me.GLOADCHRGS.Caption = "Loading Charges"
        Me.GLOADCHRGS.DisplayFormat.FormatString = "0.00"
        Me.GLOADCHRGS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GLOADCHRGS.FieldName = "LOADCHRGS"
        Me.GLOADCHRGS.Name = "GLOADCHRGS"
        Me.GLOADCHRGS.Visible = True
        Me.GLOADCHRGS.VisibleIndex = 10
        '
        'GCGST
        '
        Me.GCGST.Caption = "CGST"
        Me.GCGST.DisplayFormat.FormatString = "0.00"
        Me.GCGST.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCGST.FieldName = "CGSTAMT"
        Me.GCGST.Name = "GCGST"
        Me.GCGST.Visible = True
        Me.GCGST.VisibleIndex = 11
        '
        'GSGST
        '
        Me.GSGST.Caption = "SGST"
        Me.GSGST.DisplayFormat.FormatString = "0.00"
        Me.GSGST.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GSGST.FieldName = "SGSTAMT"
        Me.GSGST.Name = "GSGST"
        Me.GSGST.Visible = True
        Me.GSGST.VisibleIndex = 12
        '
        'GISGT
        '
        Me.GISGT.Caption = "IGST"
        Me.GISGT.DisplayFormat.FormatString = "0.00"
        Me.GISGT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GISGT.FieldName = "IGSTAMT"
        Me.GISGT.Name = "GISGT"
        Me.GISGT.Visible = True
        Me.GISGT.VisibleIndex = 13
        '
        'GINVTOTAL
        '
        Me.GINVTOTAL.Caption = "Invoice Total"
        Me.GINVTOTAL.DisplayFormat.FormatString = "0.00"
        Me.GINVTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GINVTOTAL.FieldName = "GRANDTOTAL"
        Me.GINVTOTAL.Name = "GINVTOTAL"
        Me.GINVTOTAL.Visible = True
        Me.GINVTOTAL.VisibleIndex = 14
        '
        'GGSTRATE
        '
        Me.GGSTRATE.Caption = "GST Rate %"
        Me.GGSTRATE.DisplayFormat.FormatString = "0.00"
        Me.GGSTRATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGSTRATE.FieldName = "HSNRATE"
        Me.GGSTRATE.Name = "GGSTRATE"
        Me.GGSTRATE.Visible = True
        Me.GGSTRATE.VisibleIndex = 15
        '
        'GSALESACCOUNT
        '
        Me.GSALESACCOUNT.Caption = "Sales Account"
        Me.GSALESACCOUNT.FieldName = "SALESAC"
        Me.GSALESACCOUNT.Name = "GSALESACCOUNT"
        Me.GSALESACCOUNT.Visible = True
        Me.GSALESACCOUNT.VisibleIndex = 16
        '
        'GCGSTLEDGER
        '
        Me.GCGSTLEDGER.Caption = "CGST Ledger"
        Me.GCGSTLEDGER.FieldName = "CGSTLEDGER"
        Me.GCGSTLEDGER.Name = "GCGSTLEDGER"
        Me.GCGSTLEDGER.Visible = True
        Me.GCGSTLEDGER.VisibleIndex = 17
        '
        'GSGSTLEDGER
        '
        Me.GSGSTLEDGER.Caption = "SGST Ledger"
        Me.GSGSTLEDGER.FieldName = "SGSTLEDGER"
        Me.GSGSTLEDGER.Name = "GSGSTLEDGER"
        Me.GSGSTLEDGER.Visible = True
        Me.GSGSTLEDGER.VisibleIndex = 18
        '
        'GIGSTLEDGER
        '
        Me.GIGSTLEDGER.Caption = "IGST Ledger"
        Me.GIGSTLEDGER.FieldName = "IGSTLEDGER"
        Me.GIGSTLEDGER.Name = "GIGSTLEDGER"
        Me.GIGSTLEDGER.Visible = True
        Me.GIGSTLEDGER.VisibleIndex = 19
        '
        'GCOUNTRY
        '
        Me.GCOUNTRY.Caption = "Country"
        Me.GCOUNTRY.FieldName = "COUNTRY"
        Me.GCOUNTRY.Name = "GCOUNTRY"
        Me.GCOUNTRY.Visible = True
        Me.GCOUNTRY.VisibleIndex = 20
        '
        'GSTATENAME
        '
        Me.GSTATENAME.Caption = "State"
        Me.GSTATENAME.FieldName = "STATENAME"
        Me.GSTATENAME.Name = "GSTATENAME"
        Me.GSTATENAME.Visible = True
        Me.GSTATENAME.VisibleIndex = 21
        '
        'GVOUCHERTYPE
        '
        Me.GVOUCHERTYPE.Caption = "Voucher Type"
        Me.GVOUCHERTYPE.FieldName = "VOUCHERTYPE"
        Me.GVOUCHERTYPE.Name = "GVOUCHERTYPE"
        Me.GVOUCHERTYPE.Visible = True
        Me.GVOUCHERTYPE.VisibleIndex = 22
        '
        'GUNITS
        '
        Me.GUNITS.Caption = "Units"
        Me.GUNITS.FieldName = "UNIT"
        Me.GUNITS.Name = "GUNITS"
        Me.GUNITS.Visible = True
        Me.GUNITS.VisibleIndex = 23
        '
        'GGSTIN
        '
        Me.GGSTIN.Caption = "GSTIN"
        Me.GGSTIN.FieldName = "GSTIN"
        Me.GGSTIN.Name = "GGSTIN"
        Me.GGSTIN.Visible = True
        Me.GGSTIN.VisibleIndex = 24
        Me.GGSTIN.Width = 120
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(577, 543)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 5
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TOOLEXCEL, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 255
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'TOOLEXCEL
        '
        Me.TOOLEXCEL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLEXCEL.Image = Global.TEXTRADE.My.Resources.Resources.Excel_icon
        Me.TOOLEXCEL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLEXCEL.Name = "TOOLEXCEL"
        Me.TOOLEXCEL.Size = New System.Drawing.Size(23, 22)
        Me.TOOLEXCEL.Text = "&Excel"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'CMDSHOW
        '
        Me.CMDSHOW.BackColor = System.Drawing.Color.Transparent
        Me.CMDSHOW.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSHOW.FlatAppearance.BorderSize = 0
        Me.CMDSHOW.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSHOW.ForeColor = System.Drawing.Color.Black
        Me.CMDSHOW.Location = New System.Drawing.Point(304, 28)
        Me.CMDSHOW.Name = "CMDSHOW"
        Me.CMDSHOW.Size = New System.Drawing.Size(80, 28)
        Me.CMDSHOW.TabIndex = 3
        Me.CMDSHOW.Text = "&Show Data"
        Me.CMDSHOW.UseVisualStyleBackColor = False
        '
        'TallyDataExportSalesInv
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "TallyDataExportSalesInv"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Tally Data Export Sales (Inventory)"
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
    Friend WithEvents dtto As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents dtfrom As DateTimePicker
    Friend WithEvents chkdate As CheckBox
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GINVOICENO As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGSTIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCGST As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSGST As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GISGT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmdexit As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents TOOLEXCEL As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents CMDSHOW As Button
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GITEMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GHSN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GQTY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GUNITPRICE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTAXVALUE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTRANS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPOSTAGECHRGS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLOADCHRGS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINVTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGSTRATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSALESACCOUNT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCGSTLEDGER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSGSTLEDGER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GIGSTLEDGER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOUNTRY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSTATENAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GVOUCHERTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GUNITS As DevExpress.XtraGrid.Columns.GridColumn
End Class
