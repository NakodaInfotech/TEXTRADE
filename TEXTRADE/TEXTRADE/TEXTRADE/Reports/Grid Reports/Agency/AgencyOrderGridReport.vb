
Imports System.IO
Imports BL
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class AgencyOrderGridReport

    Public SOCLAUSE As String

    Public Sub New()
        InitializeComponent()
        FILLCMB()
    End Sub

    Private Sub OrderGridReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBSELLER.Text.Trim = "" Then FILLNAME(CMBSELLER, False, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            If CMBBUYER.Text.Trim = "" Then FILLNAME(CMBBUYER, False, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")

            Dim OBJCMN As New ClsCommon
            Dim DTBUYER As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.ACC_AREAID = AREAMASTER.AREA_ID ", " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            GRIDBUYERDETAILS.DataSource = DTBUYER
            If DTBUYER.Rows.Count > 0 Then GRIDBUYER.FocusedRowHandle = GRIDBUYER.RowCount - 1

            Dim DTSELLER As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.ACC_AREAID = AREAMASTER.AREA_ID ", " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            GRIDSELLERDETAILS.DataSource = DTSELLER
            If DTSELLER.Rows.Count > 0 Then GRIDSELLER.FocusedRowHandle = GRIDSELLER.RowCount - 1


            Dim DTITEM As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY ", " ", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.ITEM_CATEGORYID = CATEGORYMASTER.CATEGORY_ID", " AND ITEMMASTER.ITEM_YEARID = '" & YearId & "' ORDER BY ITEMMASTER.ITEM_NAME")
            If DTITEM.Rows.Count > 0 Then GRIDBILLITEM.FocusedRowHandle = GRIDBILLITEM.RowCount - 1
            GRIDBILLDETAILSITEM.DataSource = DTITEM


            Dim DT As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ALLAGENCYSALEORDER.ASO_NO AS ORDERNO ", " ", " ALLAGENCYSALEORDER ", " AND ALLAGENCYSALEORDER.ASO_YEARID = " & YearId & " ORDER BY ALLAGENCYSALEORDER.ASO_NO ")
            If DT.Rows.Count > 0 Then GRIDBILLORDER.FocusedRowHandle = GRIDBILLORDER.RowCount - 1
            GRIDBILLDETAILSORDER.DataSource = DT

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub FILLGRID()
        Try

            Dim BUYERNAMECLAUSE As String = ""
            Dim SELLERNAMECLAUSE As String = ""
            Dim ITEMCLAUSE As String = ""
            Dim ORDERCLAUSE As String = ""

            SOCLAUSE = " AND 1=1 "

            If CMBBUYER.Text <> "" Then SOCLAUSE = SOCLAUSE & " and BUYERLEDGERS.ACC_CMPNAME='" & CMBBUYER.Text.Trim & "'"
            If CMBSELLER.Text <> "" Then SOCLAUSE = SOCLAUSE & " and SELLERLEDGERS.ACC_CMPNAME='" & CMBSELLER.Text.Trim & "'"
            If chkdate.Checked = True Then SOCLAUSE &= " AND ALLAGENCYSALEORDER.ASO_date BETWEEN '" & Format(dtfrom.Value.Date, "YYYY-MM-dd") & "' AND '" & Format(dtto.Value.Date, "YYYY-MM-dd") & "'"


            'FOR BUYERNAME
            GRIDBUYER.ClearColumnsFilter()
            For i As Integer = 0 To GRIDBUYER.RowCount - 1
                Dim dtrow As DataRow = GRIDBUYER.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If BUYERNAMECLAUSE = "" Then
                        BUYERNAMECLAUSE = " AND (BUYERLEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                    Else
                        BUYERNAMECLAUSE = BUYERNAMECLAUSE & " OR BUYERLEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                    End If
                End If
            Next
            If BUYERNAMECLAUSE <> "" Then
                BUYERNAMECLAUSE = BUYERNAMECLAUSE & ")"
                SOCLAUSE = SOCLAUSE & BUYERNAMECLAUSE
            End If



            'FOR SELLERNAME
            GRIDSELLER.ClearColumnsFilter()
            For i As Integer = 0 To GRIDSELLER.RowCount - 1
                Dim dtrow As DataRow = GRIDSELLER.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If SELLERNAMECLAUSE = "" Then
                        SELLERNAMECLAUSE = " AND (SELLERLEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                    Else
                        SELLERNAMECLAUSE = SELLERNAMECLAUSE & " OR SELLERLEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                    End If
                End If
            Next
            If SELLERNAMECLAUSE <> "" Then
                SELLERNAMECLAUSE = SELLERNAMECLAUSE & ")"
                SOCLAUSE = SOCLAUSE & SELLERNAMECLAUSE
            End If



            'FOR ITEMNAME
            GRIDBILLITEM.ClearColumnsFilter()
            For i As Integer = 0 To GRIDBILLITEM.RowCount - 1
                Dim dtrow As DataRow = GRIDBILLITEM.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If ITEMCLAUSE = "" Then
                        ITEMCLAUSE = " AND (ITEMMASTER.ITEM_NAME = '" & dtrow("ITEMNAME") & "'"
                    Else
                        ITEMCLAUSE = ITEMCLAUSE & " OR ITEMMASTER.ITEM_NAME = '" & dtrow("ITEMNAME") & "'"
                    End If
                End If
            Next
            If ITEMCLAUSE <> "" Then
                ITEMCLAUSE = ITEMCLAUSE & ")"
                SOCLAUSE = SOCLAUSE & ITEMCLAUSE
            End If

            'FOR ORDERNO
            GRIDBILLORDER.ClearColumnsFilter()
            For i As Integer = 0 To GRIDBILLORDER.RowCount - 1
                Dim dtrow As DataRow = GRIDBILLORDER.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If ORDERCLAUSE = "" Then
                        ORDERCLAUSE = " AND (ALLAGENCYSALEORDER.ASO_NO = '" & dtrow("ORDERNO") & "'"
                    Else
                        ORDERCLAUSE = ORDERCLAUSE & " OR ALLAGENCYSALEORDER.ASO_NO = '" & dtrow("ORDERNO") & "'"
                    End If
                End If
            Next
            If ORDERCLAUSE <> "" Then
                ORDERCLAUSE = ORDERCLAUSE & ")"
                SOCLAUSE = SOCLAUSE & ORDERCLAUSE
            End If


            If RDBPENDING.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLAGENCYSALEORDER_DESC.BALANCE > 0 AND ALLAGENCYSALEORDER_DESC.ASO_CLOSED='FALSE' "
            If RDBCOMPLETE.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLAGENCYSALEORDER_DESC.BALANCE <= 0 AND ALLAGENCYSALEORDER_DESC.ASO_CLOSED='FALSE'"
            If RDBCLOSED.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLAGENCYSALEORDER_DESC.ASO_CLOSED='TRUE' "

            GRIDSO.RowCount = 0

            Dim OBJCMN As New ClsCommon
            Dim LASTITEMNAME As String = ""
            Dim TOTALPCS, TOTALDELPCS, TOTALBALPCS As Double
            Dim GTOTALPCS, GTOTALDELPCS, GTOTALBALPCS As Double
            Dim DT As DataTable = OBJCMN.SEARCH(" ITEMMASTER.item_name AS ITEMNAME, ALLAGENCYSALEORDER.Aso_no AS SONO, ALLAGENCYSALEORDER.Aso_date AS SODATE, BUYERLEDGERS.Acc_cmpname AS BUYERNAME, ISNULL(SELLERLEDGERS.Acc_cmpname,'') AS SELLERNAME, ALLAGENCYSALEORDER.Aso_NOTE AS NOTE, ALLAGENCYSALEORDER_DESC.ASO_MTRS AS PCS, (CASE WHEN '" & ClientName & "' = 'ABHEE' AND ALLAGENCYSALEORDER.ASO_ORDERON = 'PCS' THEN ALLAGENCYSALEORDER_DESC.ASO_RECDQTY ELSE ALLAGENCYSALEORDER_DESC.ASO_RECDMTRS END) AS OUTPCS, ALLAGENCYSALEORDER_DESC.BALANCE AS BALPCS, ALLAGENCYSALEORDER_DESC.ASO_RATE AS RATE, ASO_DAYS AS [DAYS]  ", "", " ALLAGENCYSALEORDER INNER JOIN ALLAGENCYSALEORDER_DESC ON ALLAGENCYSALEORDER.Aso_no = ALLAGENCYSALEORDER_DESC.ASO_NO AND ALLAGENCYSALEORDER.TYPE = ALLAGENCYSALEORDER_DESC.TYPE AND ALLAGENCYSALEORDER.ASO_YEARID = ALLAGENCYSALEORDER_DESC.ASO_YEARID INNER JOIN ITEMMASTER ON ALLAGENCYSALEORDER_DESC.ASO_ITEMID = ITEMMASTER.item_id INNER JOIN LEDGERS AS BUYERLEDGERS ON ALLAGENCYSALEORDER.Aso_ledgerid = BUYERLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS SELLERLEDGERS ON ALLAGENCYSALEORDER.Aso_PACKINGID = SELLERLEDGERS.Acc_id ", " AND ALLAGENCYSALEORDER.ASO_YEARID =" & YearId & SOCLAUSE & " ORDER BY ITEMMASTER.item_name, ALLAGENCYSALEORDER.ASO_DATE, ALLAGENCYSALEORDER.ASO_NO")


            For Each DTROW As DataRow In DT.Rows
                If LASTITEMNAME <> DTROW("ITEMNAME") Then
                    LASTITEMNAME = DTROW("ITEMNAME")
                    If GRIDSO.RowCount > 0 Then
                        GRIDSO.Rows.Add("", "", "", "", "", "TOTAL", Val(TOTALPCS), Val(TOTALDELPCS), Val(TOTALBALPCS), "", "")
                        GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.ForeColor = Color.Maroon
                        GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.Font = New Drawing.Font("Calibri", 10, FontStyle.Bold)
                        GRIDSO.Rows.Add("", "", "", "", "", "", "", "", "", "", "")

                        TOTALPCS = 0
                        TOTALDELPCS = 0
                        TOTALBALPCS = 0
                    End If
                    GRIDSO.Rows.Add(DTROW("ITEMNAME"), "", "", "", "", "", "", "", "", "", "")
                    GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.Font = New Drawing.Font("Calibri", 10, FontStyle.Bold)
                End If
                GRIDSO.Rows.Add("", Val(DTROW("SONO")), Format(DTROW("SODATE"), "dd/MM/yyyy"), DTROW("BUYERNAME"), DTROW("SELLERNAME"), DTROW("NOTE"), Val(DTROW("PCS")), Val(DTROW("OUTPCS")), Val(DTROW("BALPCS")), Format(Val(DTROW("RATE")), "0.00"), Val(DTROW("DAYS")))
                TOTALPCS += Val(DTROW("PCS"))
                GTOTALPCS += Val(DTROW("PCS"))
                TOTALDELPCS += Val(DTROW("OUTPCS"))
                GTOTALDELPCS += Val(DTROW("OUTPCS"))
                TOTALBALPCS += Val(DTROW("BALPCS"))
                GTOTALBALPCS += Val(DTROW("BALPCS"))
            Next

            'FOR TOTAL AND GRANDTOTAL ON LAST LINE
            If GRIDSO.RowCount > 0 Then
                GRIDSO.Rows.Add("", "", "", "", "", "TOTAL", Val(TOTALPCS), Val(TOTALDELPCS), Val(TOTALBALPCS), "", "")
                GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.ForeColor = Color.Maroon
                GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.Font = New Drawing.Font("Calibri", 10, FontStyle.Bold)


                GRIDSO.Rows.Add("", "", "", "", "", "GRAND TOTAL", Val(GTOTALPCS), Val(GTOTALDELPCS), Val(GTOTALBALPCS), "", "")
                GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.ForeColor = Color.DarkGreen
                GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.Font = New Drawing.Font("Calibri", 10, FontStyle.Bold)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OrderGridReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXPORT_Click(sender As Object, e As EventArgs) Handles CMDEXPORT.Click
        Try
            Dim xlapp As Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim i As Integer
            Dim j As Integer

            xlapp = New Excel.Application
            xlWorkBook = xlapp.Workbooks.Add(misValue)
            xlWorkSheet = CType(xlWorkBook.Sheets("Sheet1"), Excel.Worksheet)

            For k = 0 To GRIDSO.ColumnCount - 1
                xlWorkSheet.Cells(1, k + 1).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(1, k + 1) = GRIDSO.Columns(k).HeaderText
                xlWorkSheet.Rows.Item(1).EntireColumn.AutoFit()
            Next
            For i = 0 To GRIDSO.RowCount - 1
                For j = 0 To GRIDSO.ColumnCount - 1
                    xlWorkSheet.Cells(i + 2, j + 1).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(i + 2, j + 1) = GRIDSO(j, i).Value.ToString()
                Next
                xlWorkSheet.Rows.Item(i + 2).EntireColumn.AutoFit()
            Next


            Dim SaveFileDialog1 As New SaveFileDialog()
            SaveFileDialog1.Filter = "Execl files (*.xlsx)|*.xlsx"
            SaveFileDialog1.FilterIndex = 2
            SaveFileDialog1.RestoreDirectory = True
            If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                xlWorkSheet.SaveAs(SaveFileDialog1.FileName)
                MsgBox("Save file success")
            Else
                Return
            End If
            xlWorkBook.Close()
            xlapp.Quit()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBBUYER.Enter
        Try
            If CMBBUYER.Text.Trim = "" Then FILLNAME(CMBBUYER, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBUYER_Enter(sender As Object, e As EventArgs) Handles CMBSELLER.Enter
        Try
            If CMBSELLER.Text.Trim = "" Then FILLNAME(CMBSELLER, False, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND ACC_TYPE='ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBUYER_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBSELLER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='ACCOUNTS' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBSELLER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBBUYER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBBUYER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            For i As Integer = 0 To GRIDBUYER.RowCount - 1
                Dim dtrow As DataRow = GRIDBUYER.GetDataRow(i)
                dtrow("CHK") = CHKSELECTALL.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RDBALL_CheckedChanged(sender As Object, e As EventArgs) Handles RDBALL.CheckedChanged, RDBCLOSED.CheckedChanged, RDBCOMPLETE.CheckedChanged, RDBPENDING.CheckedChanged
        If sender IsNot Nothing AndAlso CType(sender, RadioButton).Checked Then
            Try
                FILLGRID()
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

    Private Sub GRIDSO_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSO.CellDoubleClick
        Try
            If e.RowIndex >= 0 Then
                Dim OBJSO As New AgencySaleOrder
                OBJSO.MdiParent = MDIMain
                OBJSO.TEMPSONO = GRIDSO.Rows(e.RowIndex).Cells("GSONO").Value.ToString()
                OBJSO.EDIT = True
                OBJSO.Show()
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
            Dim filePath As String = Application.StartupPath & "\Agency Order Grid" & CMBBUYER.Text.Trim & ".pdf"

            ' ✅ Replace "YourDataGridView" with the actual DataGridView object from your form
            ExportDataGridViewToPdfForWP(GRIDSO, filePath)

            ' Prepare WhatsApp sending form
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBBUYER.Text.Trim
            OBJWHATSAPP.PATH.Add(filePath)
            OBJWHATSAPP.FILENAME.Add("Agency Order Grid" & CMBBUYER.Text.Trim & ".pdf")
            OBJWHATSAPP.ShowDialog()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExportDataGridViewToPdfForWP(dgv As DataGridView, filePath As String)
        ' 👉 Changed to A3 for bigger page size
        Dim doc As New Document(PageSize.A3.Rotate(), 20, 20, 20, 20)

        Try
            PdfWriter.GetInstance(doc, New FileStream(filePath, FileMode.Create))
            doc.Open()

            ' Load Verdana font
            Dim verdanaBaseFont As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\verdana.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim verdana10 As New iTextSharp.text.Font(verdanaBaseFont, 10)
            Dim verdana10Bold As New iTextSharp.text.Font(verdanaBaseFont, 10, iTextSharp.text.Font.BOLD)
            Dim verdana16Bold As New iTextSharp.text.Font(verdanaBaseFont, 16, iTextSharp.text.Font.BOLD)

            ' Title & Date
            doc.Add(New Paragraph(" Agency Order Report", verdana16Bold))
            doc.Add(New Paragraph("Generated on: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm"), verdana10))
            doc.Add(New Paragraph(" "))

            ' Collect visible columns
            Dim visibleColumns As New List(Of DataGridViewColumn)
            For Each col As DataGridViewColumn In dgv.Columns
                If col.Visible Then visibleColumns.Add(col)
            Next

            ' Add one more column for "Remarks"
            Dim totalColumnsCount As Integer = visibleColumns.Count + 1

            Dim table As New PdfPTable(totalColumnsCount)
            table.WidthPercentage = 100 ' Use full page width
            table.HeaderRows = 1

            ' Set custom widths: 40% more for selected columns, add smaller width for Remarks
            Dim baseWidth As Single = 0.75F
            Dim wideWidth As Single = baseWidth * 1.4F ' 40% more
            Dim remarksWidth As Single = baseWidth * 1.2F ' slightly wider than base

            Dim widths(totalColumnsCount - 1) As Single

            For i As Integer = 0 To visibleColumns.Count - 1
                Select Case visibleColumns(i).HeaderText.Trim().ToUpper()
                    Case "ITEM NAME", "BUYER NAME", "SELLER NAME"
                        widths(i) = wideWidth
                    Case Else
                        widths(i) = baseWidth
                End Select
            Next

            ' Last column "Remarks"
            widths(totalColumnsCount - 1) = remarksWidth

            table.SetWidths(widths)

            ' Add header cells for existing columns
            For Each col As DataGridViewColumn In visibleColumns
                Dim headerCell As New PdfPCell(New Phrase(col.HeaderText, verdana10Bold)) With {
                .BackgroundColor = BaseColor.LIGHT_GRAY,
                .HorizontalAlignment = Element.ALIGN_CENTER,
                .VerticalAlignment = Element.ALIGN_MIDDLE,
                .Padding = 5,
                .NoWrap = False
            }
                table.AddCell(headerCell)
            Next

            ' Add header cell for new Remarks column
            Dim remarksHeaderCell As New PdfPCell(New Phrase("Remarks", verdana10Bold)) With {
            .BackgroundColor = BaseColor.LIGHT_GRAY,
            .HorizontalAlignment = Element.ALIGN_CENTER,
            .VerticalAlignment = Element.ALIGN_MIDDLE,
            .Padding = 5,
            .NoWrap = False
        }
            table.AddCell(remarksHeaderCell)

            ' Data rows
            For Each row As DataGridViewRow In dgv.Rows
                If Not row.IsNewRow Then
                    Dim isTotalRow As Boolean = False

                    ' Check if row contains "TOTAL" or "GRAND TOTAL"
                    For Each cell As DataGridViewCell In row.Cells
                        If cell.Value IsNot Nothing Then
                            Dim cellText As String = cell.Value.ToString().Trim().ToUpper()
                            If cellText = "TOTAL" OrElse cellText = "GRAND TOTAL" Then
                                isTotalRow = True
                                Exit For
                            End If
                        End If
                    Next

                    ' Add existing columns data
                    For Each col As DataGridViewColumn In visibleColumns
                        Dim cell As DataGridViewCell = row.Cells(col.Index)
                        Dim value As String = ""

                        If cell.Value IsNot Nothing Then
                            If TypeOf cell.Value Is DateTime Then
                                value = CType(cell.Value, DateTime).ToString("dd/MM/yyyy")
                            Else
                                value = cell.Value.ToString()
                            End If
                        End If

                        Dim pdfCell As New PdfPCell(New Phrase(value, If(isTotalRow, verdana10Bold, verdana10))) With {
                        .VerticalAlignment = Element.ALIGN_MIDDLE,
                        .Padding = 4
                    }

                        ' Color logic
                        If isTotalRow Then
                            pdfCell.BackgroundColor = New BaseColor(250, 240, 230) ' Light beige
                        ElseIf row.DefaultCellStyle.BackColor = Color.Yellow Then
                            pdfCell.BackgroundColor = BaseColor.YELLOW
                        ElseIf row.DefaultCellStyle.BackColor = Color.LightGreen Then
                            pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY
                        End If

                        ' Alignment
                        If IsNumeric(value) Then
                            pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT
                        Else
                            pdfCell.HorizontalAlignment = Element.ALIGN_LEFT
                        End If

                        table.AddCell(pdfCell)
                    Next

                    ' Add Remarks cell (empty or customize here)
                    Dim remarksText As String = "" ' You can set any remark per row here if you want

                    Dim remarksCell As New PdfPCell(New Phrase(remarksText, If(isTotalRow, verdana10Bold, verdana10))) With {
                    .VerticalAlignment = Element.ALIGN_MIDDLE,
                    .Padding = 4
                }
                    ' Same color logic for remarks
                    If isTotalRow Then
                        remarksCell.BackgroundColor = New BaseColor(250, 240, 230)
                    ElseIf row.DefaultCellStyle.BackColor = Color.Yellow Then
                        remarksCell.BackgroundColor = BaseColor.YELLOW
                    ElseIf row.DefaultCellStyle.BackColor = Color.LightGreen Then
                        remarksCell.BackgroundColor = BaseColor.LIGHT_GRAY
                    End If

                    remarksCell.HorizontalAlignment = Element.ALIGN_LEFT
                    table.AddCell(remarksCell)
                End If
            Next

            doc.Add(table)

        Catch ex As Exception
            MessageBox.Show("Failed to export PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            doc.Close()
        End Try
    End Sub

    Private Sub CMDSHOW_Click(sender As Object, e As EventArgs) Handles CMDSHOW.Click
        Try

            Dim BUYERNAMECLAUSE As String = ""
            Dim SELLERNAMECLAUSE As String = ""
            Dim ITEMCLAUSE As String = ""
            Dim ORDERCLAUSE As String = ""

            Dim OBJSO As New AgencyDesign
            OBJSO.MdiParent = MDIMain
            OBJSO.FRMSTRING = "ORDERDETAILS"
            OBJSO.FORMULA = "{ALLAGENCYSALEORDER.ASO_YEARID} = " & YearId


            'FOR BUYERNAME
            GRIDBUYER.ClearColumnsFilter()
            For i As Integer = 0 To GRIDBUYER.RowCount - 1
                Dim dtrow As DataRow = GRIDBUYER.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If BUYERNAMECLAUSE = "" Then
                        BUYERNAMECLAUSE = " AND ({BUYERLEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                    Else
                        BUYERNAMECLAUSE = BUYERNAMECLAUSE & " OR {BUYERLEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                    End If
                End If
            Next
            If BUYERNAMECLAUSE <> "" Then
                BUYERNAMECLAUSE = BUYERNAMECLAUSE & ")"
                OBJSO.FORMULA = OBJSO.FORMULA & BUYERNAMECLAUSE
            End If



            'FOR SELLERNAME
            GRIDSELLER.ClearColumnsFilter()
            For i As Integer = 0 To GRIDSELLER.RowCount - 1
                Dim dtrow As DataRow = GRIDSELLER.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If SELLERNAMECLAUSE = "" Then
                        SELLERNAMECLAUSE = " AND ({SELLERLEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                    Else
                        SELLERNAMECLAUSE = SELLERNAMECLAUSE & " OR {SELLERLEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                    End If
                End If
            Next
            If SELLERNAMECLAUSE <> "" Then
                SELLERNAMECLAUSE = SELLERNAMECLAUSE & ")"
                OBJSO.FORMULA = OBJSO.FORMULA & SELLERNAMECLAUSE
            End If



            'FOR ITEMNAME
            GRIDBILLITEM.ClearColumnsFilter()
            For i As Integer = 0 To GRIDBILLITEM.RowCount - 1
                Dim dtrow As DataRow = GRIDBILLITEM.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If ITEMCLAUSE = "" Then
                        ITEMCLAUSE = " AND ({ITEMMASTER.ITEM_NAME} = '" & dtrow("ITEMNAME") & "'"
                    Else
                        ITEMCLAUSE = ITEMCLAUSE & " OR {ITEMMASTER.ITEM_NAME} = '" & dtrow("ITEMNAME") & "'"
                    End If
                End If
            Next
            If ITEMCLAUSE <> "" Then
                ITEMCLAUSE = ITEMCLAUSE & ")"
                OBJSO.FORMULA = OBJSO.FORMULA & ITEMCLAUSE
            End If

            'FOR ORDERNO
            GRIDBILLORDER.ClearColumnsFilter()
            For i As Integer = 0 To GRIDBILLORDER.RowCount - 1
                Dim dtrow As DataRow = GRIDBILLORDER.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If ORDERCLAUSE = "" Then
                        ORDERCLAUSE = " AND ({ALLAGENCYSALEORDER.ASO_NO} = " & Val(dtrow("ORDERNO"))
                    Else
                        ORDERCLAUSE = ORDERCLAUSE & " OR {ALLAGENCYSALEORDER.ASO_NO} = " & Val(dtrow("ORDERNO"))
                    End If
                End If
            Next
            If ORDERCLAUSE <> "" Then
                ORDERCLAUSE = ORDERCLAUSE & ")"
                OBJSO.FORMULA = OBJSO.FORMULA & ORDERCLAUSE
            End If

            If RDBPENDING.Checked = True Then OBJSO.FORMULA = OBJSO.FORMULA & " AND {ALLAGENCYSALEORDER_DESC.BALANCE} > 0 AND {ALLAGENCYSALEORDER_DESC.ASO_CLOSED}=FALSE "
            If RDBCOMPLETE.Checked = True Then OBJSO.FORMULA = OBJSO.FORMULA & " AND {ALLAGENCYSALEORDER_DESC.BALANCE} <= 0 AND {ALLAGENCYSALEORDER_DESC.ASO_CLOSED}=FALSE"
            If RDBCLOSED.Checked = True Then OBJSO.FORMULA = OBJSO.FORMULA & " AND {ALLAGENCYSALEORDER_DESC.ASO_CLOSED}=TRUE "

            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class