
Imports System.IO
Imports BL
Imports DevExpress.Utils.CodedUISupport
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class InHousePackingStock

    Public FRMSTRING As String = "DETAILS"

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InHousePackingStock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Opening_Stock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If ClientName = "KARAN" Then
                GLOTNO.Visible = True
                GLOTNO.VisibleIndex = GSHADE.VisibleIndex + 1
            End If

            If FRMSTRING = "SUMMARY" Then
                GBARCODE.Visible = False
                GPCS.Visible = True
                If ClientName <> "AVIS" Then
                    GSRNO.Visible = False
                    GDATE.Visible = False
                End If
            Else
                GPCS.Visible = False
            End If
            fillgrid(" AND ISSUEPACKING_DESC.ISS_yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim OBJCMN As New ClsCommon
            Dim dt As New DataTable
            If FRMSTRING = "SUMMARY" Then
                If ClientName = "AVIS" Then
                    dt = OBJCMN.SEARCH(" ISSUEPACKING_DESC.ISS_NO AS SRNO, ISSUEPACKING.ISS_DATE AS DATE, ISNULL(CONTRACT_NAME,'') AS CONTRACTOR, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, SUM(ISSUEPACKING_DESC.ISS_MTRS) AS MTRS, SUM(ROUND(ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2)) AS RECDMTRS, SUM(ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2)) AS BALMTRS, COUNT(ISSUEPACKING_DESC.ISS_BARCODE) AS PCS, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY ", "", "  ITEMMASTER INNER JOIN ISSUEPACKING_DESC ON ITEMMASTER.item_id = ISSUEPACKING_DESC.ISS_ITEMID LEFT OUTER JOIN COLORMASTER ON ISSUEPACKING_DESC.ISS_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ISSUEPACKING_DESC.ISS_DESIGNID = DESIGNMASTER.DESIGN_id INNER JOIN ISSUEPACKING ON ISSUEPACKING.ISS_NO = ISSUEPACKING_DESC.ISS_NO AND ISSUEPACKING.ISS_YEARID = ISSUEPACKING_DESC.ISS_YEARID LEFT OUTER JOIN CONTRACTMASTER ON ISS_CONTRACTID = CONTRACT_ID LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.ITEM_CATEGORYID = CATEGORYMASTER.CATEGORY_ID", TEMPCONDITION & " GROUP BY ISSUEPACKING_DESC.ISS_NO, ISSUEPACKING.ISS_DATE , ISNULL(CONTRACT_NAME,''), ITEMMASTER.item_name, ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, ''), ISNULL(CATEGORYMASTER.CATEGORY_NAME,'')  HAVING ROUND ( SUM( ISSUEPACKING_DESC.ISS_MTRS) - SUM(ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0)), 2) > 0  ")
                ElseIf ClientName = "KARAN" Then
                    dt = OBJCMN.SEARCH(" ISNULL(CONTRACT_NAME,'') AS CONTRACTOR, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, SUM(ISSUEPACKING_DESC.ISS_MTRS) AS MTRS, SUM(ROUND(ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2)) AS RECDMTRS, SUM(ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2)) AS BALMTRS, COUNT(ISSUEPACKING_DESC.ISS_BARCODE) AS PCS, ISNULL(ISS_LOTNO,'') AS LOTNO, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY ", "", "  ITEMMASTER INNER JOIN ISSUEPACKING_DESC ON ITEMMASTER.item_id = ISSUEPACKING_DESC.ISS_ITEMID LEFT OUTER JOIN COLORMASTER ON ISSUEPACKING_DESC.ISS_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ISSUEPACKING_DESC.ISS_DESIGNID = DESIGNMASTER.DESIGN_id INNER JOIN ISSUEPACKING ON ISSUEPACKING.ISS_NO = ISSUEPACKING_DESC.ISS_NO AND ISSUEPACKING.ISS_YEARID = ISSUEPACKING_DESC.ISS_YEARID LEFT OUTER JOIN CONTRACTMASTER ON ISS_CONTRACTID = CONTRACT_ID  LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.ITEM_CATEGORYID = CATEGORYMASTER.CATEGORY_ID", TEMPCONDITION & " AND ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2) > 0 GROUP BY ISNULL(CONTRACT_NAME,''), ITEMMASTER.item_name, ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, ''), ISNULL(ISS_LOTNO,''), ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') ")
                Else
                    dt = OBJCMN.SEARCH(" ISNULL(CONTRACT_NAME,'') AS CONTRACTOR, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, SUM(ISSUEPACKING_DESC.ISS_MTRS) AS MTRS, SUM(ROUND(ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2)) AS RECDMTRS, SUM(ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2)) AS BALMTRS, COUNT(ISSUEPACKING_DESC.ISS_BARCODE) AS PCS, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY ", "", "  ITEMMASTER INNER JOIN ISSUEPACKING_DESC ON ITEMMASTER.item_id = ISSUEPACKING_DESC.ISS_ITEMID LEFT OUTER JOIN COLORMASTER ON ISSUEPACKING_DESC.ISS_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ISSUEPACKING_DESC.ISS_DESIGNID = DESIGNMASTER.DESIGN_id INNER JOIN ISSUEPACKING ON ISSUEPACKING.ISS_NO = ISSUEPACKING_DESC.ISS_NO AND ISSUEPACKING.ISS_YEARID = ISSUEPACKING_DESC.ISS_YEARID LEFT OUTER JOIN CONTRACTMASTER ON ISS_CONTRACTID = CONTRACT_ID  LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.ITEM_CATEGORYID = CATEGORYMASTER.CATEGORY_ID ", TEMPCONDITION & " AND ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2) > 0 GROUP BY ISNULL(CONTRACT_NAME,''), ITEMMASTER.item_name, ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, '') , ISNULL(CATEGORYMASTER.CATEGORY_NAME,'')")
                End If
            Else
                dt = OBJCMN.SEARCH(" ISSUEPACKING_DESC.ISS_NO AS SRNO, ISSUEPACKING.ISS_DATE AS DATE, ISNULL(CONTRACT_NAME,'') AS CONTRACTOR, ISSUEPACKING_DESC.ISS_BARCODE AS BARCODE, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISSUEPACKING_DESC.ISS_MTRS AS MTRS, ROUND(ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2) AS RECDMTRS, ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2) AS BALMTRS, ISNULL(ISS_LOTNO,'') AS LOTNO, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY  ", "", "  ITEMMASTER INNER JOIN ISSUEPACKING_DESC ON ITEMMASTER.item_id = ISSUEPACKING_DESC.ISS_ITEMID LEFT OUTER JOIN COLORMASTER ON ISSUEPACKING_DESC.ISS_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ISSUEPACKING_DESC.ISS_DESIGNID = DESIGNMASTER.DESIGN_id INNER JOIN ISSUEPACKING ON ISSUEPACKING.ISS_NO = ISSUEPACKING_DESC.ISS_NO AND ISSUEPACKING.ISS_YEARID = ISSUEPACKING_DESC.ISS_YEARID LEFT OUTER JOIN CONTRACTMASTER ON ISS_CONTRACTID = CONTRACT_ID  LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.ITEM_CATEGORYID = CATEGORYMASTER.CATEGORY_ID", TEMPCONDITION & " AND ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2) > 0 ORDER BY ISSUEPACKING_DESC.ISS_NO, ISSUEPACKING_DESC.ISS_GRIDSRNO")
            End If
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\In House Packing Stock.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "In House Packing Stock"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "In House Packing Stock", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("In House Packing Stock Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs) Handles gridbill.DoubleClick
        Try
            If FRMSTRING <> "SUMMARY" Then
                Dim OBJREC As New RecFromPacking
                OBJREC.ISSUEBARCODE = gridbill.GetFocusedRowCellValue("BARCODE")
                OBJREC.MdiParent = MDIMain
                OBJREC.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid(" AND ISSUEPACKING_DESC.ISS_yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InHousePackingStock_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "KARAN" Then
                GCATEGORY.Visible = True
                GCATEGORY.VisibleIndex = GITEMNAME.VisibleIndex + 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDWHATSAPP_Click(sender As Object, e As EventArgs) Handles CMDWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub

            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            ' Prepare data for grid
            ' TEMPOUTSTANDING()

            ' Generate the PDF from DataGridView
            Dim filePath As String = Application.StartupPath & "\InHouse Packing Stock Summary.pdf"

            ' ✅ Replace "YourDataGridView" with the actual DataGridView object from your form
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(gridbilldetails.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            ExportDevExpressGridToPdf(view, filePath)

            ' Prepare WhatsApp sending form
            Dim OBJWHATSAPP As New SendWhatsapp
            'OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.PATH.Add(filePath)
            OBJWHATSAPP.FILENAME.Add("InHouse Packing Stock Summary.pdf")
            OBJWHATSAPP.ShowDialog()

            ' Delete PDF if client is SNCM
            If ClientName = "SNCM" Then
                For Each path As String In OBJWHATSAPP.PATH
                    If File.Exists(path) Then
                        File.Delete(path)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Public Sub ExportDataGridViewToPdfForWP(dgv As System.Object, filePath As String)
    '    ' 👉 Changed to A3 for bigger page size
    '    Dim doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A3.Rotate(), 20, 20, 20, 20)

    '    Try
    '        PdfWriter.GetInstance(doc, New FileStream(filePath, FileMode.Create))
    '        doc.Open()

    '        ' Load Verdana font
    '        Dim verdanaBaseFont As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\verdana.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
    '        Dim verdana10 As New iTextSharp.text.Font(verdanaBaseFont, 10)
    '        Dim verdana10Bold As New iTextSharp.text.Font(verdanaBaseFont, 10, iTextSharp.text.Font.BOLD)
    '        Dim verdana16Bold As New iTextSharp.text.Font(verdanaBaseFont, 16, iTextSharp.text.Font.BOLD)

    '        ' Title & Date
    '        doc.Add(New iTextSharp.text.Paragraph("Inhouse Packing Stock Report", verdana16Bold))
    '        doc.Add(New iTextSharp.text.Paragraph("Generated on: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm"), verdana10))
    '        doc.Add(New iTextSharp.text.Paragraph(" "))

    '        ' Collect visible columns
    '        Dim visibleColumns As New List(Of DataGridViewColumn)
    '        For Each col As DataGridViewColumn In dgv.Columns
    '            If col.Visible Then visibleColumns.Add(col)
    '        Next

    '        Dim table As New PdfPTable(visibleColumns.Count)
    '        table.WidthPercentage = 100
    '        table.HeaderRows = 1

    '        ' 👉 Custom width logic: NAME & BILL AMT are wider
    '        Dim columnWidths(visibleColumns.Count - 1) As Single
    '        Dim totalWeight As Single = 0.0F

    '        For i As Integer = 0 To visibleColumns.Count - 1
    '            Dim header As String = visibleColumns(i).HeaderText.Trim().ToUpper()
    '            Select Case header
    '                Case "CONTRACTOR", "BARCODE"
    '                    columnWidths(i) = 2.5F  ' 👈 Increased
    '                Case "ITEMNAME"
    '                    columnWidths(i) = 2.0F
    '                Case "DESIGNNO", "SHADE", "PCS"
    '                    columnWidths(i) = 1.5F
    '                Case "BALMTRS"
    '                    columnWidths(i) = 5.0F  ' 👈 Increased
    '                Case Else
    '                    columnWidths(i) = 1.0F  ' 👈 Increased
    '            End Select
    '            totalWeight += columnWidths(i)
    '        Next

    '        ' Normalize widths to make total = 100%
    '        For i As Integer = 0 To columnWidths.Length - 1
    '            columnWidths(i) = columnWidths(i) / totalWeight * 100.0F
    '        Next

    '        table.SetWidths(columnWidths)

    '        ' Headers
    '        For Each col As DataGridViewColumn In visibleColumns
    '            Dim headerCell As New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(col.HeaderText, verdana10Bold)) With {
    '             .BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY,
    '             .HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
    '             .VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,
    '             .Padding = 5,
    '             .NoWrap = False
    '          }

    '            table.AddCell(headerCell)

    '        Next


    '        ' Data rows
    '        For Each row As DataGridViewRow In dgv.Rows
    '            If Not row.IsNewRow Then
    '                Dim isGrandTotalRow As Boolean = False

    '                For Each cell As DataGridViewCell In row.Cells
    '                    If cell.Value IsNot Nothing AndAlso cell.Value.ToString().Trim().ToUpper() = "GRANDTOTAL" Then
    '                        isGrandTotalRow = True
    '                        Exit For
    '                    End If
    '                Next

    '                For Each col As DataGridViewColumn In visibleColumns
    '                    Dim cell As DataGridViewCell = row.Cells(col.Index)
    '                    Dim value As String = ""

    '                    If cell.Value IsNot Nothing Then
    '                        If TypeOf cell.Value Is DateTime Then
    '                            value = CType(cell.Value, DateTime).ToString("dd/MM/yyyy")
    '                        Else
    '                            value = cell.Value.ToString()
    '                        End If
    '                    End If

    '                    Dim pdfCell As New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(value, If(isGrandTotalRow, verdana10Bold, verdana10))) With {
    '                    .VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,
    '                    .Padding = 4
    '                }

    '                    ' Color logic
    '                    If isGrandTotalRow Then
    '                        pdfCell.BackgroundColor = New iTextSharp.text.BaseColor(250, 240, 230)

    '                    ElseIf row.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow Then
    '                        pdfCell.BackgroundColor = iTextSharp.text.BaseColor.YELLOW

    '                    ElseIf row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen Then
    '                        pdfCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY

    '                    End If

    '                    ' Wrapping for specific columns
    '                    Dim colName As String = col.HeaderText.Trim().ToUpper()
    '                    Select Case colName
    '                        Case "CONTRACTOR", "SRNO", "ITEMNAME", "DESIGNNO", "SHADE", "BARCODE", "PCS", "BALMTRS", "CATEGORY", "RECDMTRS"
    '                            pdfCell.NoWrap = False
    '                        Case Else
    '                            pdfCell.NoWrap = True
    '                    End Select

    '                    ' Alignment

    '                    If IsNumeric(value) Then
    '                        pdfCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT
    '                    Else
    '                        pdfCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
    '                    End If

    '                    table.AddCell(pdfCell)
    '                Next
    '            End If
    '        Next

    '        doc.Add(table)

    '    Catch ex As Exception
    '        MessageBox.Show("Failed to export PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        doc.Close()
    '    End Try
    'End Sub


    Public Sub ExportDevExpressGridToPdf(view As DevExpress.XtraGrid.Views.Grid.GridView, filePath As String)

        Dim doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A3.Rotate(), 20, 20, 20, 20)

        Try
            PdfWriter.GetInstance(doc, New FileStream(filePath, FileMode.Create))
            doc.Open()

            ' Fonts
            Dim verdanaBaseFont As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\verdana.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim verdana10 As New iTextSharp.text.Font(verdanaBaseFont, 10)
            Dim verdana10Bold As New iTextSharp.text.Font(verdanaBaseFont, 10, iTextSharp.text.Font.BOLD)
            Dim verdana16Bold As New iTextSharp.text.Font(verdanaBaseFont, 16, iTextSharp.text.Font.BOLD)

            ' Title
            doc.Add(New iTextSharp.text.Paragraph("Inhouse Packing Stock Summary", verdana16Bold))
            doc.Add(New iTextSharp.text.Paragraph("Generated on: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm"), verdana10))
            doc.Add(New iTextSharp.text.Paragraph(" "))

            ' 👉 Collect visible columns
            Dim visibleColumns As New List(Of DevExpress.XtraGrid.Columns.GridColumn)

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In view.Columns
                If col.Visible Then visibleColumns.Add(col)
            Next

            Dim table As New PdfPTable(visibleColumns.Count)
            table.WidthPercentage = 100
            table.HeaderRows = 1

            ' 👉 Column Width Logic
            Dim columnWidths(visibleColumns.Count - 1) As Single
            Dim totalWeight As Single = 0.0F

            For i As Integer = 0 To visibleColumns.Count - 1
                Dim header As String = visibleColumns(i).Caption.Trim().ToUpper()

                Select Case header
                    Case "CONTRACTOR", "BARCODE"
                        columnWidths(i) = 2.5F
                    Case "ITEMNAME"
                        columnWidths(i) = 2.0F
                    Case "DESIGNNO", "SHADE", "PCS"
                        columnWidths(i) = 1.5F
                    Case "BALMTRS"
                        columnWidths(i) = 5.0F
                    Case Else
                        columnWidths(i) = 1.0F
                End Select

                totalWeight += columnWidths(i)
            Next

            ' Normalize
            For i As Integer = 0 To columnWidths.Length - 1
                columnWidths(i) = columnWidths(i) / totalWeight * 100.0F
            Next

            table.SetWidths(columnWidths)

            ' 👉 Headers
            For Each col In visibleColumns
                Dim headerCell As New PdfPCell(New Phrase(col.Caption, verdana10Bold)) With {
                    .BackgroundColor = BaseColor.LIGHT_GRAY,
                    .HorizontalAlignment = Element.ALIGN_CENTER,
                    .VerticalAlignment = Element.ALIGN_MIDDLE,
                    .Padding = 5,
                    .NoWrap = False
                }
                table.AddCell(headerCell)
            Next

            ' 👉 Rows
            For i As Integer = 0 To view.RowCount - 1

                If view.IsGroupRow(i) Then Continue For

                Dim isGrandTotalRow As Boolean = False

                ' Check GRANDTOTAL
                For Each col In visibleColumns
                    Dim valObj = view.GetRowCellValue(i, col)
                    If valObj IsNot Nothing AndAlso valObj.ToString().Trim().ToUpper() = "GRANDTOTAL" Then
                        isGrandTotalRow = True
                        Exit For
                    End If
                Next

                ' Cells
                For Each col In visibleColumns

                    Dim valueObj = view.GetRowCellValue(i, col)
                    Dim value As String = ""

                    If valueObj IsNot Nothing Then
                        If TypeOf valueObj Is DateTime Then
                            value = CType(valueObj, DateTime).ToString("dd/MM/yyyy")
                        Else
                            value = valueObj.ToString()
                        End If
                    End If

                    Dim pdfCell As New PdfPCell(New Phrase(value, If(isGrandTotalRow, verdana10Bold, verdana10))) With {
                        .VerticalAlignment = Element.ALIGN_MIDDLE,
                        .Padding = 4
                    }

                    ' 👉 Color Logic
                    If isGrandTotalRow Then
                        pdfCell.BackgroundColor = New BaseColor(250, 240, 230)

                    ElseIf view.Appearance.Row.BackColor = Color.Yellow Then
                        pdfCell.BackgroundColor = BaseColor.YELLOW

                    ElseIf view.Appearance.Row.BackColor = Color.LightGreen Then
                        pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY
                    End If

                    ' 👉 Wrapping
                    Dim colName As String = col.Caption.Trim().ToUpper()

                    Select Case colName
                        Case "CONTRACTOR", "SRNO", "ITEMNAME", "DESIGNNO", "SHADE", "BARCODE", "PCS", "BALMTRS", "CATEGORY", "RECDMTRS"
                            pdfCell.NoWrap = False
                        Case Else
                            pdfCell.NoWrap = True
                    End Select

                    ' 👉 Alignment
                    If IsNumeric(value) Then
                        pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT
                    Else
                        pdfCell.HorizontalAlignment = Element.ALIGN_LEFT
                    End If

                    table.AddCell(pdfCell)
                Next
            Next

            doc.Add(table)

        Catch ex As Exception
            MessageBox.Show("Failed to export PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            doc.Close()
        End Try

    End Sub

End Class