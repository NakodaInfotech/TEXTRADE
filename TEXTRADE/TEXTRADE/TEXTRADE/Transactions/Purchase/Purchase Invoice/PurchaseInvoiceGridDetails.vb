Imports System.IO
Imports BL
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting
Imports System.Diagnostics


Public Class PurchaseInvoiceGridDetails

    Dim PURCHASEREGID As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub PurchaseInvoiceGridDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Alt = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJPUR As New ClsPurchaseMaster
            OBJPUR.alParaval.Add(0)
            OBJPUR.alParaval.Add(cmbregister.Text.Trim)
            OBJPUR.alParaval.Add(CmpId)
            OBJPUR.alParaval.Add(0)
            OBJPUR.alParaval.Add(YearId)

            Dim dt As DataTable = OBJPUR.selectpurchase()
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal billno As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJBILL As New PurchaseMaster
                OBJBILL.MdiParent = MDIMain
                OBJBILL.EDIT = editval
                OBJBILL.TEMPBILLNO = billno
                OBJBILL.TEMPREGNAME = cmbregister.Text.Trim
                OBJBILL.Show()
                '  Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'PURCHASE'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'PURCHASE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'PURCHASE INVOICE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If cmbregister.Text.Trim.Length > 0 Then
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'PURCHASE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    PURCHASEREGID = dt.Rows(0).Item(0)
                    fillgrid()
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("BILLNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("BILLNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVELAYOUT_Click(sender As Object, e As EventArgs) Handles CMDSAVELAYOUT.Click
        Try
            Dim layoutFileName As String = $"{Me.Name}"
            Dim layoutPath As String = System.IO.Path.Combine(Application.StartupPath, layoutFileName)
            gridbill.SaveLayoutToXml(layoutPath)
            'MessageBox.Show("Layout saved as: " & layoutFileName)




            ' Prompt user for filename
            Dim userFileName As String = InputBox("Enter a name for the layout file (without extension):", "Save Layout", Me.Name)

            ' Exit if the user cancels or enters nothing
            If String.IsNullOrWhiteSpace(userFileName) Then
                MessageBox.Show("Save cancelled.")
                Exit Sub
            End If

            ' Add .xml extension and construct path
            Dim FileName As String = $"{userFileName}.xml"

            ' Save layout to file
            gridbill.SaveLayoutToXml(layoutPath)
            MessageBox.Show("Layout saved as: " & FileName)

            ' Read file content
            Dim xmlContent As String = File.ReadAllText(layoutPath)



            Dim OBJSELECTSG As New SelectCustomLayout
            OBJSELECTSG.FORMNAMES = layoutFileName
            OBJSELECTSG.FILENAME = FileName
            OBJSELECTSG.FILES = xmlContent
            OBJSELECTSG.ShowDialog()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdprint_Click(sender As Object, e As EventArgs) Handles CMDPDF.Click
        Try
            Dim filePath As String = Application.StartupPath & "\PurchaseInvoiceDetails.pdf"
            gridbill.OptionsPrint.AutoWidth = False   ' keeps column widths
            gridbill.OptionsPrint.UsePrintStyles = True

            ' PDF export options
            Dim pdfOptions As New PdfExportOptions()

            ' Printing system to control page layout
            Dim ps As New PrintingSystem()
            Dim link As New PrintableComponentLink(ps)

            link.Component = gridbilldetails
            link.Landscape = True                      ' LANDSCAPE
            link.PaperKind = System.Drawing.Printing.PaperKind.A4
            link.Margins = New Printing.Margins(20, 20, 20, 20)

            ' ✅ Correct grid reference
            gridbill.Columns("NAME").Width = 165
            gridbill.Columns("ITEMNAME").Width = 150
            gridbill.Columns("TRANSNAME").Width = 160
            gridbill.Columns("COMPLAINT").Width = 170
            gridbill.Columns("PARTYBILLDATE").Width = 70
            gridbill.Columns("LRNO").Width = 70

            AddHandler link.CreateReportHeaderArea,
                Sub(headerSender As Object, headerArgs As CreateAreaEventArgs)

                    headerArgs.Graph.StringFormat = New BrickStringFormat(StringAlignment.Center)
                    headerArgs.Graph.Font = New Font("Arial", 20, FontStyle.Bold)
                    headerArgs.Graph.ForeColor = Color.Black
                    headerArgs.Graph.BackColor = Color.Transparent
                    headerArgs.Graph.DrawString(CmpName, Color.Black, New RectangleF(0, 0, headerArgs.Graph.ClientPageSize.Width, 35), BorderSide.None)
                End Sub



            ' Create document and export
            link.CreateDocument()
            link.ExportToPdf(filePath)

            ' Open PDF
            Process.Start(New ProcessStartInfo(filePath) With {.UseShellExecute = True})
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Purchase Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Purchase Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Purchase Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Purchase Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("BILLCHECKED")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightGreen
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("BILLDISPUTE")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("BILLRETURN")) > 0 Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Plum
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        If ClientName = "SVS" Then Me.Close()
        If ClientName = "DAKSH" Or ClientName = "SHALIBHADRA" Then
            GSPDISCAMT.Visible = True
            GDISAMT.Visible = True
        End If
        If ClientName = "NVAHAN" Then
            GFOOTERDISCPER.Visible = True
            GFOOTERDISCPER.VisibleIndex = GMTRS.VisibleIndex + 1
            GFOOTERDISCAMT.Visible = True
            GFOOTERDISCAMT.VisibleIndex = GFOOTERDISCPER.VisibleIndex + 1
        End If
        If ClientName = "ABHEE" Then
            GDESIGNNO.Visible = False
            GCOLOR.Visible = False
            GAQTY.Visible = True
            GFOLDPER.Visible = True
            GAQTY.VisibleIndex = 14
            GFOLDPER.VisibleIndex = 15
        End If

    End Sub

End Class