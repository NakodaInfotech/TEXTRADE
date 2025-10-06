Imports BL
Imports System.Data.OleDb
Imports System.Data
Imports System.Linq
Imports System.Data.DataSetExtensions
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Public Class UploadExcel
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim frm As New ExpenseVoucher()

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        CLEAR()
    End Sub
    Sub CLEAR()
        TXTFILENAME.Clear()
    End Sub

    Private Sub CMDSELECFILE_Click(sender As Object, e As EventArgs) Handles CMDSELECTFILE.Click
        Try
            OpenFileDialog1.Filter = "Excel (*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
            OpenFileDialog1.ShowDialog()

            OpenFileDialog1.AddExtension = True
            TXTFILENAME.Text = OpenFileDialog1.SafeFileName
            TXTB2BPATH.Text = OpenFileDialog1.FileName
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdupload_Click(sender As Object, e As EventArgs) Handles cmdupload.Click
        If TXTB2BPATH.Text.Trim = "" Then
            MessageBox.Show("Please select Excel file first.")
            Exit Sub
        End If

        ' 1. Read Excel into DataTable
        Dim oExcel As Excel.Application = Nothing
        Dim oBook As Excel.Workbook = Nothing
        Dim oSheet As Excel.Worksheet = Nothing
        Dim dt As New DataTable()
        Dim OBJCMN As New ClsCommon


        Try
            ' 1. Open Excel
            oExcel = New Excel.Application()
            oBook = oExcel.Workbooks.Open(TXTB2BPATH.Text.Trim)
            oSheet = CType(oBook.Sheets(1), Excel.Worksheet) ' Use Sheet1 or change as needed

            ' 2. Read Header Row
            Dim colIndex As Integer = 1
            While oSheet.Cells(1, colIndex).Value IsNot Nothing
                dt.Columns.Add(oSheet.Cells(1, colIndex).Value.ToString())
                colIndex += 1
            End While

            ' 3. Read Data Rows
            Dim rowIndex As Integer = 2
            While oSheet.Cells(rowIndex, 1).Value IsNot Nothing
                Dim row As DataRow = dt.NewRow()
                For i As Integer = 1 To dt.Columns.Count
                    row(i - 1) = Convert.ToString(oSheet.Cells(rowIndex, i).Value)
                Next
                dt.Rows.Add(row)
                rowIndex += 1
            End While
            If dt.Rows.Count = 0 Then
                MessageBox.Show("Excel file is empty.")
                Exit Sub
            End If
            ' Check CMBTYPE to decide save destination
            If CMBTYPE.Text.Trim = "NONPURCHASE" Then
                ' Create dictionary: key = Party Bill No, value = List of rows
                Dim invoices As New Dictionary(Of String, List(Of DataRow))()

                For Each row As DataRow In dt.Rows
                    Dim key As String = row("party bill no").ToString().Trim()
                    If Not invoices.ContainsKey(key) Then
                        invoices(key) = New List(Of DataRow)()
                    End If
                    invoices(key).Add(row)
                Next

                ' Process each invoice group
                Dim successCount As Integer = 0
                Dim errorCount As Integer = 0
                For Each kvp In invoices
                    Dim rows As List(Of DataRow) = kvp.Value
                    Dim dr As DataRow = rows(0)   ' first row = header

                    Dim frm As New ExpenseVoucher()
                    frm.CMBNAME.Text = dr("name").ToString()
                    frm.RunCmbNameValidation()   ' 🔹 Ensures correct GST split
                    If String.IsNullOrEmpty(frm.NPDATE.Text) Or frm.NPDATE.Text = "__/__/____" Then
                        frm.NPDATE.Text = DateTime.Now.ToString("dd/MM/yyyy")
                    End If
                    frm.TXTPARTYBILLNO.Text = dr("party bill no").ToString()
                    frm.PARTYBILLDATE.Text = dr("party bill date").ToString()
                    'frm.PARTYBILLDATE.Text = Format(Convert.ToDateTime(dr("bill date")), "dd/MM/yyyy")
                    frm.txtremarks.Text = dr("OTHER REF (REMARKS)").ToString()
                    'frm.TXTSACCODE.Text = dr("SAC CODE").ToString()

                    frm.GRIDEXPENSE.Rows.Clear()
                    Dim sr As Integer = 1
                    'For Each r As DataRow In rows
                    '    If r("ITEM NAME").ToString().Trim() <> "" Then
                    '        frm.GRIDEXPENSE.Rows.Add(sr,
                    '                  "WEAVING CHARGES",           ' Debit To (fixed)
                    '                  dr("SAC CODE").ToString(),
                    '                  r("ITEM NAME").ToString(),   ' NOTE
                    '                  Val(r("QTY")),               ' QTY 
                    '                  Val(r("RATE")),              ' RATE
                    '                  Val(r("AMOUNT")),            ' AMOUNT
                    '                  0,                           ' OTHERAMT
                    '                  0,                           ' TAXABLE AMT
                    '                  0,                           ' CGST %
                    '                  0,                           ' CGST AMT
                    '                  0,                           ' SGST %
                    '                  0,                           ' SGST AMT
                    '                  0,                           ' IGST %
                    '                  0,                           ' IGST 
                    '                  Val(r("AMOUNT"))           ' GRID TOTAL
                    '         )
                    '        sr += 1
                    '    End If
                    'Next
                    ' Before adding items in rows, fetch once:
                    Dim otherAmt As Decimal = 0
                    If dr.Table.Columns.Contains("OTHER AMT") AndAlso dr("OTHER AMT").ToString().Trim() <> "" Then
                        otherAmt = Val(dr("OTHER AMT"))
                    End If
                    For Each r As DataRow In rows
                        ' -----------------------
                        ' Handle first ITEM NAME (main item)
                        ' -----------------------
                        If r.Table.Columns.Contains("ITEM NAME") AndAlso r("ITEM NAME").ToString().Trim() <> "" Then
                            frm.GRIDEXPENSE.Rows.Add(sr,
            "WEAVING CHARGES",
            dr("SAC CODE").ToString(),
            r("ITEM NAME").ToString(),
            Val(r("QTY")),
            Val(r("RATE")),
            Val(r("AMOUNT")),
            otherAmt,  ' --- set this for all
            0, 0, 0, 0, 0, 0, 0, 0,
            Val(r("AMOUNT"))
        )

                            Dim lastRow As DataGridViewRow = frm.GRIDEXPENSE.Rows(frm.GRIDEXPENSE.Rows.Count - 1)

                            ' Populate form fields for CALC()
                            frm.TXTQTY.Text = Val(r("QTY")).ToString()
                            frm.TXTRATE.Text = Val(r("RATE")).ToString()
                            frm.TXTTAXABLEAMT.Text = Val(r("AMOUNT")).ToString()
                            frm.CMBHSNCODE.Text = dr("SAC CODE").ToString()

                            frm.GETHSNCODE()
                            frm.CALC()

                            ' Copy GST values back
                            lastRow.Cells("GCGSTPER").Value = frm.TXTCGSTPER.Text
                            lastRow.Cells("GCGSTAMT").Value = frm.TXTCGSTAMT.Text
                            lastRow.Cells("GSGSTPER").Value = frm.TXTSGSTPER.Text
                            lastRow.Cells("GSGSTAMT").Value = frm.TXTSGSTAMT.Text
                            lastRow.Cells("GIGSTPER").Value = frm.TXTIGSTPER.Text
                            lastRow.Cells("GIGSTAMT").Value = frm.TXTIGSTAMT.Text
                            lastRow.Cells("GTAXABLEAMT").Value = frm.TXTTAXABLEAMT.Text
                            lastRow.Cells("GGRIDTOTAL").Value = frm.TXTGRIDTOTAL.Text

                            sr += 1
                        End If

                        ' -----------------------
                        ' Handle ITEM NAME 1, ITEMNAME 2, ITEM NAME 3
                        ' -----------------------
                        For i As Integer = 1 To 3
                            Dim itemColOptions = {$"ITEM NAME {i}", $"ITEMNAME {i}", $"ITEMNAME{i}"}
                            Dim qtyColOptions = {$"QTY {i}", $"QTY{i}"}
                            Dim rateColOptions = {$"RATE {i}", $"RATE{i}"}
                            Dim amountColOptions = {$"AMOUNT {i}", $"AMOUNT{i}"}

                            Dim itemCol = itemColOptions.FirstOrDefault(Function(c) r.Table.Columns.Contains(c))
                            Dim qtyCol = qtyColOptions.FirstOrDefault(Function(c) r.Table.Columns.Contains(c))
                            Dim rateCol = rateColOptions.FirstOrDefault(Function(c) r.Table.Columns.Contains(c))
                            Dim amountCol = amountColOptions.FirstOrDefault(Function(c) r.Table.Columns.Contains(c))

                            If Not String.IsNullOrEmpty(itemCol) AndAlso r(itemCol).ToString().Trim() <> "" Then
                                frm.GRIDEXPENSE.Rows.Add(sr,
                "WEAVING CHARGES",
                dr("SAC CODE").ToString(),
                r(itemCol).ToString(),
                Val(r(qtyCol)),
                Val(r(rateCol)),
                Val(r(amountCol)),
                otherAmt,  ' --- set this for all
                0, 0, 0, 0, 0, 0, 0, 0,
                Val(r(amountCol))
            )

                                Dim lastRow As DataGridViewRow = frm.GRIDEXPENSE.Rows(frm.GRIDEXPENSE.Rows.Count - 1)

                                ' Populate form fields for CALC()
                                frm.TXTQTY.Text = Val(r(qtyCol)).ToString()
                                frm.TXTRATE.Text = Val(r(rateCol)).ToString()
                                frm.TXTTAXABLEAMT.Text = Val(r(amountCol)).ToString()
                                frm.CMBHSNCODE.Text = dr("SAC CODE").ToString()

                                frm.GETHSNCODE()
                                frm.CALC()

                                ' Copy GST values back
                                lastRow.Cells("GCGSTPER").Value = frm.TXTCGSTPER.Text
                                lastRow.Cells("GCGSTAMT").Value = frm.TXTCGSTAMT.Text
                                lastRow.Cells("GSGSTPER").Value = frm.TXTSGSTPER.Text
                                lastRow.Cells("GSGSTAMT").Value = frm.TXTSGSTAMT.Text
                                lastRow.Cells("GIGSTPER").Value = frm.TXTIGSTPER.Text
                                lastRow.Cells("GIGSTAMT").Value = frm.TXTIGSTAMT.Text
                                lastRow.Cells("GTAXABLEAMT").Value = frm.TXTTAXABLEAMT.Text
                                lastRow.Cells("GGRIDTOTAL").Value = frm.TXTGRIDTOTAL.Text

                                sr += 1
                            End If
                        Next
                    Next
                    ' 🔹 Refresh totals after all rows
                    frm.TOTAL()
                    ' Set Register Name
                    frm.CMBREGISTER.Text = "NON-PURCHASE REGISTER" ' or get from Excel if dynamic
                    frm.CanUserAdd = True
                    frm.TXTNPNO.Text = getmaxno().ToString()
                    ' Save invoice
                    If frm.SaveInvoice(True) Then
                        successCount += 1
                    Else
                        errorCount += 1
                        MessageBox.Show("Error uploading voucher for Party Bill No: " & frm.TXTPARTYBILLNO.Text, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Next

                MessageBox.Show(successCount & " vouchers uploaded successfully. " & errorCount & " failed.", "Upload Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf CMBTYPE.Text.Trim = "INVOICE" Then
                ' New InvoiceMaster upload logic
                Dim successCount As Integer = 0
                Dim errorCount As Integer = 0

                For Each row As DataRow In dt.Rows
                    Dim invoiceNo As String = row("CHALLAN NO").ToString().Trim()
                    Dim sono As String = row("SO NO").ToString().Trim()

                    ' Check for duplicate invoice
                    Dim dtCheck As DataTable = OBJCMN.SEARCH("INVOICE_NO", "INVOICEMASTER", "AND INVOICE_NO = '" & invoiceNo & "' AND INVOICE_YEARID = " & YearId)
                    If dtCheck.Rows.Count > 0 Then
                        MessageBox.Show("Duplicate Invoice No: " & invoiceNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        errorCount += 1
                        Continue For
                    End If

                    ' Fetch SONO details
                    Dim dtSONO As DataTable = OBJCMN.SEARCH("DELIVERYTO, AGENTNAME, CRDAYS, PARTYNAME, DISCOUNT", "SALEORDER", "AND SONO = '" & sono & "' AND SOYEARID = " & YearId)
                    If dtSONO.Rows.Count = 0 Then
                        MessageBox.Show("SONO " & sono & " not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        errorCount += 1
                        Continue For
                    End If

                    Try
                        Dim frmInv As New InvoiceMaster()
                        frmInv.TXTINVOICENO.Text = invoiceNo
                        frmInv.TXTSONO.Text = sono
                        frmInv.CMBPACKING.Text = dtSONO.Rows(0)("DELIVERYTO").ToString()
                        frmInv.CMBAGENT.Text = dtSONO.Rows(0)("AGENTNAME").ToString()
                        frmInv.TXTCRDAYS.Text = dtSONO.Rows(0)("CRDAYS").ToString()
                        frmInv.cmbname.Text = dtSONO.Rows(0)("PARTYNAME").ToString()
                        frmInv.CMBCHARGES.Text = dtSONO.Rows(0)("DISCOUNT").ToString()
                        ' Map other delivery/transport/LR/etc details from Excel columns if needed here

                        ' Fill grid of frmInv using dt row if needed (customize as per grid requirements)

                        'If frmInv.SaveInvoice() Then
                        '    successCount += 1
                        'Else
                        '    errorCount += 1
                        'End If
                    Catch ex As Exception
                        MessageBox.Show("Error saving invoice no: " & invoiceNo & vbCrLf & ex.Message)
                        errorCount += 1
                    End Try
                Next
                MessageBox.Show(successCount & " invoices saved successfully, " & errorCount & " errors.", "Invoice Upload Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Select CMBTYPE as either NONPURCHASE or INVOICE.")
            End If
        Catch ex As Exception
            ' MessageBox.Show("Error: " & ex.Message)
        Finally
            ' 5. Clean up COM objects
            If oBook IsNot Nothing Then oBook.Close(False)
            If oExcel IsNot Nothing Then oExcel.Quit()

            If oSheet IsNot Nothing Then Marshal.ReleaseComObject(oSheet)
            If oBook IsNot Nothing Then Marshal.ReleaseComObject(oBook)
            If oExcel IsNot Nothing Then Marshal.ReleaseComObject(oExcel)

            oSheet = Nothing
            oBook = Nothing
            oExcel = Nothing

            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub
    Private Function getmaxno() As Integer
        Dim DTTABLE As New DataTable
        Dim nextNo As Integer = 1

        Try
            Dim registerName As String = "NON-PURCHASE REGISTER"  ' or use dynamic name if needed

            DTTABLE = getmax("ISNULL(MAX(NP_NO), 0) + 1",
                             "NONPURCHASE INNER JOIN REGISTERMASTER ON REGISTER_ID = NP_REGISTERID",
                             "AND REGISTERMASTER.REGISTER_NAME = '" & registerName & "' AND REGISTER_TYPE = 'EXPENSE' AND NP_YEARID = " & YearId)

            If DTTABLE.Rows.Count > 0 Then
                nextNo = Convert.ToInt32(DTTABLE.Rows(0).Item(0))
            End If

        Catch ex As Exception
            MessageBox.Show("Error getting max voucher no: " & ex.Message)
        End Try

        Return nextNo
    End Function

End Class