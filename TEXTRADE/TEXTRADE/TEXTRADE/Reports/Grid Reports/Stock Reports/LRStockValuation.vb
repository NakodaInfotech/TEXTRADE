
Imports System.ComponentModel
Imports System.IO
Imports BL
Imports iTextSharp.text.pdf

Public Class LRStockValuation

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
            FILLNAME(CMBNAME, False, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, GROUPMASTER.GROUP_NAME AS GROUPNAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA, ISNULL(SALESMANMASTER.SALESMAN_NAME,'') AS SALESMAN ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.ACC_AREAID = AREAMASTER.AREA_ID LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.ACC_ID  ", " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then gridbill.FocusedRowHandle = gridbill.RowCount - 1

            Dim DTITEM As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY ", " ", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.ITEM_CATEGORYID = CATEGORYMASTER.CATEGORY_ID", " AND ITEMMASTER.ITEM_YEARID = '" & YearId & "' ORDER BY ITEMMASTER.ITEM_NAME")
            If DTITEM.Rows.Count > 0 Then GRIDBILLITEM.FocusedRowHandle = GRIDBILLITEM.RowCount - 1
            GRIDBILLDETAILSITEM.DataSource = DTITEM

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

            Dim WHERECLAUSE As String = " AND PURCHASELRSTOCK.SOLD = 0 AND PURCHASELRSTOCK.YEARID = " & YearId
            Dim NAMECLAUSE As String = ""
            Dim ITEMCLAUSE As String = ""

            If CMBNAME.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " and PURCHASELRSTOCK.NAME ='" & CMBNAME.Text.Trim & "'"
            If CMBITEMNAME.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " and PURCHASELRSTOCK.ITEMNAME ='" & CMBITEMNAME.Text.Trim & "'"


            'FOR NAME
            gridbill.ClearColumnsFilter()
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If NAMECLAUSE = "" Then
                        NAMECLAUSE = " AND (PURCHASELRSTOCK.NAME = '" & dtrow("NAME") & "'"
                    Else
                        NAMECLAUSE = NAMECLAUSE & " OR PURCHASELRSTOCK.NAME = '" & dtrow("NAME") & "'"
                    End If
                End If
            Next
            If NAMECLAUSE <> "" Then
                NAMECLAUSE = NAMECLAUSE & ")"
                WHERECLAUSE = WHERECLAUSE & NAMECLAUSE
            End If


            'FOR ITEMNAME
            GRIDBILLITEM.ClearColumnsFilter()
            For i As Integer = 0 To GRIDBILLITEM.RowCount - 1
                Dim dtrow As DataRow = GRIDBILLITEM.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If ITEMCLAUSE = "" Then
                        ITEMCLAUSE = " AND (PURCHASELRSTOCK.ITEMNAME = '" & dtrow("ITEMNAME") & "'"
                    Else
                        ITEMCLAUSE = ITEMCLAUSE & " OR PURCHASELRSTOCK.ITEMNAME = '" & dtrow("ITEMNAME") & "'"
                    End If
                End If
            Next
            If ITEMCLAUSE <> "" Then
                ITEMCLAUSE = ITEMCLAUSE & ")"
                WHERECLAUSE = WHERECLAUSE & ITEMCLAUSE
            End If



            GRIDSTOCK.RowCount = 0
            Dim OBJCMN As New ClsCommon
            Dim LASTITEMNAME As String = ""
            Dim TOTALLR, TOTALMTRS, TOTALAMT, TOTALGST, TOTALTAXABLE, TOTALINT, TOTALFINAL As Double
            Dim GTOTALLR, GTOTALMTRS, GTOTALAMT, GTOTALGST, GTOTALTAXABLE, GTOTALINT, GTOTALFINAL As Double
            Dim DT As DataTable = OBJCMN.SEARCH("  ITEMNAME, NAME, COUNT(LRNO) AS LR, SUM(TOTALMTRS) AS MTRS, RATE, ROUND(RATE*SUM(TOTALMTRS),2) AS AMT, ROUND(RATE*SUM(TOTALMTRS)*0.05,2) AS GST, ROUND((RATE*SUM(TOTALMTRS)) + (RATE*SUM(TOTALMTRS)*0.05),2) AS AMTWITHGST , BILLDATE AS [DATE], DATEDIFF(D,BILLDATE, GETDATE()) AS DAYS ", "", " PURCHASELRSTOCK ", WHERECLAUSE & " GROUP BY ITEMNAME, NAME, RATE, BILLDATE ORDER BY ITEMNAME, NAME ")

            'For Each DTROW As DataRow In DT.Rows
            '    If LASTITEMNAME <> DTROW("ITEMNAME") Then
            '        LASTITEMNAME = DTROW("ITEMNAME")
            '        If GRIDSTOCK.RowCount > 0 Then
            '            GRIDSTOCK.Rows.Add("", "", "", "", "", "TOTAL", "", Val(TOTALPCS), Val(TOTALDELPCS), Val(TOTALBALPCS), "", "")
            '            GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.ForeColor = Color.Maroon
            '            GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.Font = New Font("Calibri", 10, FontStyle.Bold)
            '            GRIDSTOCK.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "")

            '            TOTALPCS = 0
            '            TOTALDELPCS = 0
            '            TOTALBALPCS = 0
            '        End If
            '        GRIDSTOCK.Rows.Add(DTROW("ITEMNAME"), "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            '        GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.Font = New Font("Calibri", 10, FontStyle.Bold)
            '    End If
            '    GRIDSTOCK.Rows.Add(DTROW("NAME"), Val(DTROW("LR")), 0, Val(DTROW("MTRS")), 0, Format(Val(DTROW("RATE")), "0.00"), Format(Val(DTROW("AMT")), "0.00"), Format(Val(DTROW("GST")), "0.00"), Format(Val(DTROW("AMTWITHGST")), "0.00"), 0, Format(DTROW("DATE"), "dd/MM/yyyy"), Val(DTROW("DAYS")), Format(((Val(DTROW("AMT")) * (Val(TXTINTPER.Text.Trim) / 100)) / 30) * Val(DTROW("DAYS")), "0.00"), Format(Val(DTROW("AMT")) + (((Val(DTROW("AMT")) * (Val(TXTINTPER.Text.Trim) / 100)) / 30) * Val(DTROW("DAYS"))), "0.00"), "")
            '    TOTALPCS += Val(DTROW("PCS"))
            '    GTOTALPCS += Val(DTROW("PCS"))
            '    TOTALDELPCS += Val(DTROW("OUTPCS"))
            '    GTOTALDELPCS += Val(DTROW("OUTPCS"))
            '    TOTALBALPCS += Val(DTROW("BALPCS"))
            '    GTOTALBALPCS += Val(DTROW("BALPCS"))
            'Next

            ''FOR TOTAL AND GRANDTOTAL ON LAST LINE
            'If GRIDSTOCK.RowCount > 0 Then
            '    GRIDSTOCK.Rows.Add("", "", "", "", "", "TOTAL", "", Val(TOTALPCS), Val(TOTALDELPCS), Val(TOTALBALPCS), "", "")
            '    GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.ForeColor = Color.Maroon
            '    GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.Font = New Font("Calibri", 10, FontStyle.Bold)


            '    GRIDSTOCK.Rows.Add("", "", "", "", "", "GRAND TOTAL", "", Val(GTOTALPCS), Val(GTOTALDELPCS), Val(GTOTALBALPCS), "", "")
            '    GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.ForeColor = Color.DarkGreen
            '    GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.Font = New Font("Calibri", 10, FontStyle.Bold)
            'End If

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

            For k = 0 To GRIDSTOCK.ColumnCount - 1
                xlWorkSheet.Cells(1, k + 1).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(1, k + 1) = GRIDSTOCK.Columns(k).HeaderText
                xlWorkSheet.Rows.Item(1).EntireColumn.AutoFit()
            Next
            For i = 0 To GRIDSTOCK.RowCount - 1
                For j = 0 To GRIDSTOCK.ColumnCount - 1
                    xlWorkSheet.Cells(i + 2, j + 1).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(i + 2, j + 1) = GRIDSTOCK(j, i).Value.ToString()
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

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(sender As Object, e As EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                dtrow("CHK") = CHKSELECTALL.Checked
            Next
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
            Dim filePath As String = Application.StartupPath & "\Pending Order_" & CMBNAME.Text.Trim & ".pdf"

            ' ✅ Replace "YourDataGridView" with the actual DataGridView object from your form
            ExportDataGridViewToPdfForWP(GRIDSTOCK, filePath)

            ' Prepare WhatsApp sending form
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.PATH.Add(filePath)
            OBJWHATSAPP.FILENAME.Add("Pending Order" & CMBNAME.Text.Trim & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExportDataGridViewToPdfForWP(dgv As DataGridView, filePath As String)
        ' 👉 Changed to A3 for bigger page size
        Dim doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A3.Rotate(), 20, 20, 20, 20)

        Try
            PdfWriter.GetInstance(doc, New FileStream(filePath, FileMode.Create))
            doc.Open()

            ' Load Verdana font
            Dim verdanaBaseFont As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\verdana.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim verdana10 As New iTextSharp.text.Font(verdanaBaseFont, 10)
            Dim verdana10Bold As New iTextSharp.text.Font(verdanaBaseFont, 10, iTextSharp.text.Font.BOLD)
            Dim verdana16Bold As New iTextSharp.text.Font(verdanaBaseFont, 16, iTextSharp.text.Font.BOLD)

            ' Title & Date
            doc.Add(New iTextSharp.text.Paragraph("Order Grid Report", verdana16Bold))
            doc.Add(New iTextSharp.text.Paragraph("Generated on: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm"), verdana10))
            doc.Add(New iTextSharp.text.Paragraph(" "))

            ' Collect visible columns
            Dim visibleColumns As New List(Of DataGridViewColumn)
            For Each col As DataGridViewColumn In dgv.Columns
                If col.Visible Then visibleColumns.Add(col)
            Next

            Dim table As New PdfPTable(visibleColumns.Count)
            table.WidthPercentage = 100
            table.HeaderRows = 1

            ' 👉 Custom width logic: NAME & BILL AMT are wider
            Dim columnWidths(visibleColumns.Count - 1) As Single
            Dim totalWeight As Single = 0.0F

            For i As Integer = 0 To visibleColumns.Count - 1
                Dim header As String = visibleColumns(i).HeaderText.Trim().ToUpper()
                Select Case header
                    Case "NAME", "AGENT NAME"
                        columnWidths(i) = 2.5F  ' 👈 Increased
                    Case "BILL AMT"
                        columnWidths(i) = 2.0F
                    Case "RECD AMT", "BALANCE", "RUNNING BAL"
                        columnWidths(i) = 1.5F
                    Case "NOTE"
                        columnWidths(i) = 5.0F  ' 👈 Increased
                    Case Else
                        columnWidths(i) = 1.0F  ' 👈 Increased
                End Select
                totalWeight += columnWidths(i)
            Next

            ' Normalize widths to make total = 100%
            For i As Integer = 0 To columnWidths.Length - 1
                columnWidths(i) = columnWidths(i) / totalWeight * 100.0F
            Next

            table.SetWidths(columnWidths)

            ' Headers
            For Each col As DataGridViewColumn In visibleColumns
                Dim headerCell As New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(col.HeaderText, verdana10Bold)) With {
                 .BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY,
                 .HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
                 .VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,
                 .Padding = 5,
                 .NoWrap = False
              }

                table.AddCell(headerCell)

            Next


            ' Data rows
            For Each row As DataGridViewRow In dgv.Rows
                If Not row.IsNewRow Then
                    Dim isGrandTotalRow As Boolean = False

                    For Each cell As DataGridViewCell In row.Cells
                        If cell.Value IsNot Nothing AndAlso cell.Value.ToString().Trim().ToUpper() = "GRANDTOTAL" Then
                            isGrandTotalRow = True
                            Exit For
                        End If
                    Next

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

                        Dim pdfCell As New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(value, If(isGrandTotalRow, verdana10Bold, verdana10))) With {
                        .VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,
                        .Padding = 4
                    }

                        ' Color logic
                        If isGrandTotalRow Then
                            pdfCell.BackgroundColor = New iTextSharp.text.BaseColor(250, 240, 230)

                        ElseIf row.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow Then
                            pdfCell.BackgroundColor = iTextSharp.text.BaseColor.YELLOW

                        ElseIf row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen Then
                            pdfCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY

                        End If

                        ' Wrapping for specific columns
                        Dim colName As String = col.HeaderText.Trim().ToUpper()
                        Select Case colName
                            Case "NAME", "INV NO", "ITEM NAME", "MILL NAME", "PCS/BAGS", "REMARKS", "BROKER", "JOBBERNAME", "TRANSNAME", "GODOWN"
                                pdfCell.NoWrap = False
                            Case Else
                                pdfCell.NoWrap = True
                        End Select

                        ' Alignment

                        If IsNumeric(value) Then
                            pdfCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT
                        Else
                            pdfCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                        End If

                        table.AddCell(pdfCell)
                    Next
                End If
            Next

            doc.Add(table)

        Catch ex As Exception
            MessageBox.Show("Failed to export PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            doc.Close()
        End Try
    End Sub

End Class