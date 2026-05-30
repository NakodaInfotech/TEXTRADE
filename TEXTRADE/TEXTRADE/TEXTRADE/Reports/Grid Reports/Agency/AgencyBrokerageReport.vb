Imports System.IO
Imports BL
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class AgencyBrokerageReport

    Private Const COL_DATE As Integer = 0
    Private Const COL_BILLNO As Integer = 1
    Private Const COL_GROSSAMT As Integer = 2
    Private Const COL_INVAMT As Integer = 3
    Private Const COL_BROKRATE As Integer = 4
    Private Const COL_BROKAMT As Integer = 5

    Public Sub New()
        InitializeComponent()
        FILLCMB()
    End Sub
    Private Sub AgencyBrokerageReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Default date range = today
            dtfrom.Value = Today
            dtto.Value = Today
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLCMB()
        Try
            If CMBSELLER.Text.Trim = "" Then
                FILLNAME(CMBSELLER, False, " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If
            If CMBBUYER.Text.Trim = "" Then
                FILLNAME(CMBBUYER, False, " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub FILLGRID()
        Try
            Dim CLAUSE As String = " AND 1=1 "

            ' ── Buyer filter ──────────────────────────────────────────────────
            If CMBBUYER.Text.Trim <> "" Then
                CLAUSE &= " AND BUYERS.Acc_cmpname = '" & CMBBUYER.Text.Trim & "'"
            End If

            ' ── Seller filter ─────────────────────────────────────────────────
            If CMBSELLER.Text.Trim <> "" Then
                CLAUSE &= " AND SELLERS.Acc_cmpname = '" & CMBSELLER.Text.Trim & "'"
            End If

            ' ── Date filter ───────────────────────────────────────────────────
            If chkdate.Checked Then
                CLAUSE &= " AND AGENCYINVOICEMASTER.AINVOICE_DATE BETWEEN '" &
                          Format(dtfrom.Value.Date, "yyyy-MM-dd") & "' AND '" &
                          Format(dtto.Value.Date, "yyyy-MM-dd") & "'"
            End If

            ' ── Fetch data ────────────────────────────────────────────────────
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" BUYERS.Acc_cmpname AS BUYERNAME, SELLERS.Acc_cmpname AS SELLERNAME,  AGENCYINVOICEMASTER.AINVOICE_DATE AS IDATE, AGENCYINVOICEMASTER.AINVOICE_NO AS BILLNO, AGENCYINVOICEMASTER.AINVOICE_SUBTOTAL AS GROSSAMT,  AGENCYINVOICEMASTER.AINVOICE_GRANDTOTAL AS INVAMT,  ISNULL(AGENCYINVOICEMASTER.AINVOICE_COMM,0) AS BROKRATE,  ((AGENCYINVOICEMASTER.AINVOICE_SUBTOTAL * ISNULL(AGENCYINVOICEMASTER.AINVOICE_COMM,0)) / 100) AS BROKAMT ", "", " AGENCYINVOICEMASTER  INNER JOIN LEDGERS AS BUYERS  ON AGENCYINVOICEMASTER.AINVOICE_LEDGERID    = BUYERS.Acc_id  LEFT  JOIN LEDGERS AS SELLERS ON AGENCYINVOICEMASTER.AINVOICE_PURLEDGERID = SELLERS.Acc_id ", " AND AGENCYINVOICEMASTER.AINVOICE_YEARID = " & YearId & CLAUSE & " ORDER BY SELLERS.Acc_cmpname, BUYERS.Acc_cmpname, AGENCYINVOICEMASTER.AINVOICE_DATE, AGENCYINVOICEMASTER.AINVOICE_NO")

            ' ── Populate grid ─────────────────────────────────────────────────
            GRIDRPT.Rows.Clear()

            Dim lastSeller As String = ""
            Dim lastBuyer As String = ""

            Dim sellerGross, sellerInv, sellerBrok As Double
            Dim buyerGross, buyerInv, buyerBrok As Double
            Dim grandGross, grandInv, grandBrok As Double

            For Each ROW As DataRow In DT.Rows
                Dim curSeller As String = If(IsDBNull(ROW("SELLERNAME")), "", ROW("SELLERNAME").ToString())
                Dim curBuyer As String = ROW("BUYERNAME").ToString()

                ' ── Seller break (MAIN GROUP) ─────────────────────────────────────
                If curSeller <> lastSeller Then

                    ' Close previous buyer subtotal
                    If lastBuyer <> "" Then
                        AddBuyerSubtotal(buyerGross, buyerInv, buyerBrok)
                        buyerGross = 0 : buyerInv = 0 : buyerBrok = 0
                    End If

                    ' Close previous seller subtotal
                    If lastSeller <> "" Then
                        AddSellerSubtotal(sellerGross, sellerInv, sellerBrok)
                        AddBlankRow()
                        sellerGross = 0 : sellerInv = 0 : sellerBrok = 0
                    End If

                    ' Seller header row (MAIN)
                    AddSellerHeader(curSeller)
                    lastSeller = curSeller
                    lastBuyer = ""
                End If

                ' ── Buyer break (SUB GROUP) ───────────────────────────────────────
                If curBuyer <> lastBuyer Then

                    ' Close previous buyer subtotal
                    If lastBuyer <> "" Then
                        AddBuyerSubtotal(buyerGross, buyerInv, buyerBrok)
                        buyerGross = 0 : buyerInv = 0 : buyerBrok = 0
                    End If

                    ' Buyer header row (SUB)
                    AddBuyerHeader(curBuyer)
                    lastBuyer = curBuyer
                End If

                ' ── Data row ──────────────────────────────────────────────────────
                Dim gross As Double = Val(ROW("GROSSAMT"))
                Dim inv As Double = Val(ROW("INVAMT"))
                Dim brokRate As Double = Val(ROW("BROKRATE"))
                Dim brokAmt As Double = Val(ROW("BROKAMT"))

                GRIDRPT.Rows.Add(
                Format(CDate(ROW("IDATE")), "dd/MM/yyyy"),
                ROW("BILLNO").ToString(),
                Format(gross, "0.00"),
                Format(inv, "0.00"),
                Format(brokRate, "0.00"),
                Format(brokAmt, "0.00"))

                buyerGross += gross : buyerInv += inv : buyerBrok += brokAmt
                sellerGross += gross : sellerInv += inv : sellerBrok += brokAmt
                grandGross += gross : grandInv += inv : grandBrok += brokAmt
            Next

            ' ── Close last buyer / seller ─────────────────────────────────────────
            If lastBuyer <> "" Then
                AddBuyerSubtotal(buyerGross, buyerInv, buyerBrok)
            End If
            If lastSeller <> "" Then
                AddSellerSubtotal(sellerGross, sellerInv, sellerBrok)
                AddBlankRow()
            End If

            ' ── Grand total row ───────────────────────────────────────────────
            If GRIDRPT.RowCount > 0 Then
                Dim gi As Integer = GRIDRPT.Rows.Add(
                    "GRAND TOTAL", "", Format(grandGross, "0.00"),
                    Format(grandInv, "0.00"), "", Format(grandBrok, "0.00"))
                With GRIDRPT.Rows(gi).DefaultCellStyle
                    .ForeColor = Color.White
                    .BackColor = Color.DarkSlateBlue
                    .Font = New Drawing.Font("Calibri", 10, FontStyle.Bold)
                End With
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub AddSellerHeader(sellerName As String)
        Dim ri As Integer = GRIDRPT.Rows.Add("  Seller : " & sellerName, "", "", "", "", "")
        With GRIDRPT.Rows(ri)
            .Tag = "SELLERHDR"
            .Height = 22
            For Each cell As DataGridViewCell In .Cells
                cell.Style.BackColor = Color.FromArgb(173, 216, 230)   ' blue — main group
                cell.Style.ForeColor = Color.DarkBlue
                cell.Style.Font = New Drawing.Font("Calibri", 10, FontStyle.Bold)
                cell.Style.SelectionBackColor = Color.FromArgb(173, 216, 230)
                cell.Style.SelectionForeColor = Color.DarkBlue
            Next
            .Cells(0).Value = "  Seller : " & sellerName
        End With
    End Sub

    Private Sub AddBuyerHeader(buyerName As String)
        Dim ri As Integer = GRIDRPT.Rows.Add("      Buyer : " & buyerName, "", "", "", "", "")
        With GRIDRPT.Rows(ri)
            .Tag = "BUYERHDR"
            .Height = 20
            For Each cell As DataGridViewCell In .Cells
                cell.Style.BackColor = Color.FromArgb(220, 235, 245)   ' light — sub group
                cell.Style.ForeColor = Color.DimGray
                cell.Style.Font = New Drawing.Font("Calibri", 9, FontStyle.Bold)
                cell.Style.SelectionBackColor = Color.FromArgb(220, 235, 245)
                cell.Style.SelectionForeColor = Color.DimGray
            Next
            .Cells(0).Value = "      Buyer : " & buyerName
        End With
    End Sub

    Private Sub AddSellerSubtotal(gross As Double, inv As Double, brok As Double)
        Dim ri As Integer = GRIDRPT.Rows.Add(
            "   Seller Total :", "", Format(gross, "0.00"),
            Format(inv, "0.00"), "", Format(brok, "0.00"))
        With GRIDRPT.Rows(ri).DefaultCellStyle
            .ForeColor = Color.Maroon
            .Font = New Drawing.Font("Calibri", 9, FontStyle.Bold)
        End With
        GRIDRPT.Rows(ri).Tag = "SELLERTOTAL"
    End Sub

    Private Sub AddBuyerSubtotal(gross As Double, inv As Double, brok As Double)
        Dim ri As Integer = GRIDRPT.Rows.Add(
            "Buyer Total :", "", Format(gross, "0.00"),
            Format(inv, "0.00"), "", Format(brok, "0.00"))
        With GRIDRPT.Rows(ri).DefaultCellStyle
            .ForeColor = Color.DarkBlue
            .BackColor = Color.FromArgb(220, 235, 255)
            .Font = New Drawing.Font("Calibri", 10, FontStyle.Bold)
        End With
        GRIDRPT.Rows(ri).Tag = "BUYERTOTAL"
    End Sub

    Private Sub AddBlankRow()
        GRIDRPT.Rows.Add("", "", "", "", "", "")
        GRIDRPT.Rows(GRIDRPT.RowCount - 1).Tag = "BLANK"
    End Sub

    ' ───────────────────────────────────────────────────────────────────────────
    '  BUTTON EVENTS
    ' ───────────────────────────────────────────────────────────────────────────
    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub AgencyBrokerageReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    ' ── EXPORT TO EXCEL ───────────────────────────────────────────────────────
    Private Sub CMDEXPORT_Click(sender As Object, e As EventArgs) Handles CMDEXPORT.Click
        Try
            Dim xlapp As Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            xlapp = New Excel.Application
            xlWorkBook = xlapp.Workbooks.Add(misValue)
            xlWorkSheet = CType(xlWorkBook.Sheets("Sheet1"), Excel.Worksheet)

            ' Header row
            For k = 0 To GRIDRPT.ColumnCount - 1
                xlWorkSheet.Cells(1, k + 1) = GRIDRPT.Columns(k).HeaderText
                xlWorkSheet.Cells(1, k + 1).Font.Bold = True
            Next

            ' Data rows
            For i As Integer = 0 To GRIDRPT.RowCount - 1
                For j As Integer = 0 To GRIDRPT.ColumnCount - 1
                    Dim v As Object = GRIDRPT(j, i).Value
                    xlWorkSheet.Cells(i + 2, j + 1) = If(v Is Nothing, "", v.ToString())
                Next
                xlWorkSheet.Rows.Item(i + 2).EntireColumn.AutoFit()
            Next

            xlWorkSheet.Rows.Item(1).EntireColumn.AutoFit()

            Dim sfd As New SaveFileDialog()
            sfd.Filter = "Excel files (*.xlsx)|*.xlsx"
            sfd.FileName = "Brokerage Report " & Format(Today, "ddMMMyyyy")
            If sfd.ShowDialog() = DialogResult.OK Then
                xlWorkSheet.SaveAs(sfd.FileName)
                MsgBox("File saved successfully.", MsgBoxStyle.Information)
            End If
            xlWorkBook.Close()
            xlapp.Quit()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ' ── PRINT (PDF) ───────────────────────────────────────────────────────────
    Private Sub CMDPRINT_Click(sender As Object, e As EventArgs) Handles CMDPRINT.Click
        Try
            Dim filePath As String = Application.StartupPath & "\Brokerage Report.pdf"
            ExportToPdf(filePath)
            Process.Start(filePath)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ' ── WHATSAPP ──────────────────────────────────────────────────────────────
    Private Sub CMDWHATSAPP_Click(sender As Object, e As EventArgs) Handles CMDWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If
            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim filePath As String = Application.StartupPath & "\Brokerage Report.pdf"
            ExportToPdf(filePath)

            Dim OBJWA As New SendWhatsapp
            OBJWA.PARTYNAME = CMBBUYER.Text.Trim
            OBJWA.PATH.Add(filePath)
            OBJWA.FILENAME.Add("Brokerage Report " & CMBBUYER.Text.Trim & ".pdf")
            OBJWA.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ' ───────────────────────────────────────────────────────────────────────────
    '  PDF EXPORT  (iTextSharp)
    ' ───────────────────────────────────────────────────────────────────────────
    Public Sub ExportToPdf(filePath As String)
        Dim doc As New Document(PageSize.A4.Rotate(), 20, 20, 30, 20)
        Try
            PdfWriter.GetInstance(doc, New FileStream(filePath, FileMode.Create))
            doc.Open()

            Dim baseFont As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim fNormal As New iTextSharp.text.Font(baseFont, 7)
            Dim fBold As New iTextSharp.text.Font(baseFont, 7, iTextSharp.text.Font.BOLD)
            Dim fTitle As New iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD)
            Dim fSub As New iTextSharp.text.Font(baseFont, 8)

            ' ── Report header ─────────────────────────────────────────────────
            doc.Add(New Paragraph("ABHEE", fTitle) With {.Alignment = Element.ALIGN_CENTER})
            Dim subTitle As String = "Brokerage Sales Report"
            If chkdate.Checked Then
                subTitle &= "  |  " & Format(dtfrom.Value, "dd/MM/yyyy") & " To " & Format(dtto.Value, "dd/MM/yyyy")
            End If
            doc.Add(New Paragraph(subTitle, fSub) With {.Alignment = Element.ALIGN_CENTER})
            doc.Add(New Paragraph("Generated: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fSub) With {.Alignment = Element.ALIGN_RIGHT})
            doc.Add(New Paragraph(" "))

            ' ── Table  (6 columns) ────────────────────────────────────────────
            Dim tbl As New PdfPTable(6)
            tbl.WidthPercentage = 100
            tbl.HeaderRows = 1
            tbl.SetWidths(New Single() {16, 14, 14, 14, 10, 14})

            Dim headers() As String = {"Date", "Bill No", "Gross Amt", "Inv Amt", "Brok %", "Brok Amt"}
            For Each h As String In headers
                tbl.AddCell(New PdfPCell(New Phrase(h, fBold)) With {
                    .BackgroundColor = New BaseColor(200, 200, 200),
                    .HorizontalAlignment = Element.ALIGN_CENTER,
                    .Padding = 4
                })
            Next

            ' ── Rows ──────────────────────────────────────────────────────────
            For Each row As DataGridViewRow In GRIDRPT.Rows
                If row.IsNewRow Then Continue For

                Dim tag As String = If(row.Tag IsNot Nothing, row.Tag.ToString(), "")
                Dim isBuyerHdr As Boolean = (tag = "BUYERHDR")
                Dim isSellerHdr As Boolean = (tag = "SELLERHDR")
                Dim isBuyerTotal As Boolean = (tag = "BUYERTOTAL")
                Dim isSellerTotal As Boolean = (tag = "SELLERTOTAL")
                Dim isGrand As Boolean = (tag = "" AndAlso
                    row.Cells(COL_DATE).Value IsNot Nothing AndAlso
                    row.Cells(COL_DATE).Value.ToString().StartsWith("GRAND"))
                Dim isBlank As Boolean = (tag = "BLANK")

                If isBlank Then
                    ' Skip blank separator rows in PDF (saves space)
                    Continue For
                End If

                Dim bgColor As BaseColor = BaseColor.WHITE
                If isBuyerHdr Then bgColor = New BaseColor(173, 216, 230)
                If isSellerHdr Then bgColor = New BaseColor(240, 240, 240)
                If isBuyerTotal Then bgColor = New BaseColor(220, 235, 255)
                If isSellerTotal Then bgColor = New BaseColor(255, 245, 240)
                If isGrand Then bgColor = New BaseColor(50, 50, 120)

                Dim fRow As iTextSharp.text.Font = fNormal
                If isBuyerHdr OrElse isBuyerTotal OrElse isGrand Then fRow = fBold

                For colIdx As Integer = 0 To 5
                    Dim cellVal As String = ""
                    If row.Cells(colIdx).Value IsNot Nothing Then cellVal = row.Cells(colIdx).Value.ToString()

                    ' For buyer/seller headers, span all cols via first cell only
                    If (isBuyerHdr OrElse isSellerHdr) AndAlso colIdx = 0 Then
                        Dim spanCell As New PdfPCell(New Phrase(cellVal, fRow)) With {
                            .Colspan = 6,
                            .BackgroundColor = bgColor,
                            .Padding = 4
                        }
                        If isGrand Then spanCell.HorizontalAlignment = Element.ALIGN_RIGHT
                        tbl.AddCell(spanCell)
                        Exit For   ' skip remaining cols — spanned
                    ElseIf (isBuyerHdr OrElse isSellerHdr) AndAlso colIdx > 0 Then
                        Continue For   ' already spanned
                    End If

                    Dim pCell As New PdfPCell(New Phrase(cellVal, If(isGrand, New iTextSharp.text.Font(baseFont, 7, iTextSharp.text.Font.BOLD, BaseColor.WHITE), fRow))) With {
                        .BackgroundColor = bgColor,
                        .Padding = 4
                    }
                    pCell.HorizontalAlignment = If(colIdx >= 2, Element.ALIGN_RIGHT, Element.ALIGN_LEFT)
                    tbl.AddCell(pCell)
                Next
            Next

            doc.Add(tbl)

        Catch ex As Exception
            MessageBox.Show("PDF export failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            doc.Close()
        End Try
    End Sub

    ' ───────────────────────────────────────────────────────────────────────────
    '  COMBO ENTER / KEYDOWN  (same pattern as AgencyOrderGridReport)
    ' ───────────────────────────────────────────────────────────────────────────
    Private Sub CMBBUYER_Enter(sender As Object, e As EventArgs) Handles CMBBUYER.Enter
        Try
            If CMBBUYER.Text.Trim = "" Then
                FILLNAME(CMBBUYER, False, " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELLER_Enter(sender As Object, e As EventArgs) Handles CMBSELLER.Enter
        Try
            If CMBSELLER.Text.Trim = "" Then
                FILLNAME(CMBSELLER, False, " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBUYER_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBBUYER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma OrElse e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.F1 Then
                Dim OBJ As New SelectLedger
                OBJ.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJ.ShowDialog()
                If OBJ.TEMPNAME <> "" Then CMBBUYER.Text = OBJ.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELLER_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBSELLER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma OrElse e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.F1 Then
                Dim OBJ As New SelectLedger
                OBJ.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJ.ShowDialog()
                If OBJ.TEMPNAME <> "" Then CMBSELLER.Text = OBJ.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ' ── Date checkbox toggle ──────────────────────────────────────────────────
    Private Sub CHKDATE_CheckedChanged(sender As Object, e As EventArgs) Handles chkdate.CheckedChanged
        dtfrom.Enabled = chkdate.Checked
        dtto.Enabled = chkdate.Checked
    End Sub
    Private Sub GRIDRPT_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GRIDRPT.RowPostPaint
        Try
            If e.RowIndex < 0 OrElse e.RowIndex >= GRIDRPT.RowCount Then Exit Sub

            Dim tag As String = If(GRIDRPT.Rows(e.RowIndex).Tag IsNot Nothing,
                               GRIDRPT.Rows(e.RowIndex).Tag.ToString(), "")

            If tag <> "BUYERHDR" AndAlso tag <> "SELLERHDR" Then Exit Sub

            Dim cellText As String = ""
            If GRIDRPT.Rows(e.RowIndex).Cells(0).Value IsNot Nothing Then
                cellText = GRIDRPT.Rows(e.RowIndex).Cells(0).Value.ToString().Trim()
            End If

            Dim bgColor As Color = If(tag = "SELLERHDR",
                          Color.FromArgb(173, 216, 230),
                          Color.FromArgb(220, 235, 245))
            Dim fgColor As Color = If(tag = "SELLERHDR", Color.DarkBlue, Color.DimGray)
            Dim fontSize As Single = If(tag = "SELLERHDR", 11.0F, 11.0F)

            ' Full row rectangle from row header width to last column
            Dim rowRect As New RectangleF(
            e.RowBounds.Left,
            e.RowBounds.Top,
            e.RowBounds.Width,
            e.RowBounds.Height)

            ' Fill entire row background
            e.Graphics.FillRectangle(New SolidBrush(bgColor), rowRect)

            ' Draw bottom border
            e.Graphics.DrawLine(
            New Pen(Color.LightSteelBlue),
            rowRect.Left, rowRect.Bottom - 1,
            rowRect.Right, rowRect.Bottom - 1)

            ' Draw text across full row
            Dim drawFont As New Drawing.Font("Calibri", fontSize, FontStyle.Bold)
            Dim sf As New StringFormat() With {
            .Alignment = StringAlignment.Near,
            .LineAlignment = StringAlignment.Center
        }
            Dim textRect As New RectangleF(
            rowRect.Left + 10, rowRect.Top,
            rowRect.Width - 10, rowRect.Height)

            e.Graphics.DrawString(cellText, drawFont, New SolidBrush(fgColor), textRect, sf)

            drawFont.Dispose()
            sf.Dispose()

        Catch ex As Exception
            ' silent
        End Try
    End Sub
End Class