
Imports BL
Imports System.Runtime.InteropServices

Public Class UploadExcel_MASHOK

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim frm As New ExpenseVoucher()

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        CLEAR()
    End Sub

    Sub CLEAR()
        TXTFILENAME.Clear()
    End Sub

    Private Sub UploadExcelNonPurchase_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub CMDSELECFILE_Click(sender As Object, e As EventArgs) Handles CMDSELECTFILE.Click
        Try
            OpenFileDialog1.Filter = "Excel (*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
            OpenFileDialog1.ShowDialog()

            OpenFileDialog1.AddExtension = True
            TXTFILENAME.Text = OpenFileDialog1.SafeFileName
            TXTPATH.Text = OpenFileDialog1.FileName
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdupload_Click(sender As Object, e As EventArgs) Handles CMDUPLOAD.Click

        If TXTPATH.Text.Trim = "" Then
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
            oBook = oExcel.Workbooks.Open(TXTPATH.Text.Trim)
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
                'Dim invoices As New Dictionary(Of String, List(Of DataRow))()

                'For Each row As DataRow In dt.Rows
                '    Dim key As String = row("party bill no").ToString().Trim()
                '    If Not invoices.ContainsKey(key) Then
                '        invoices(key) = New List(Of DataRow)()
                '    End If
                '    invoices(key).Add(row)
                'Next

                '' Process each invoice group
                'Dim successCount As Integer = 0
                'Dim errorCount As Integer = 0
                'For Each kvp In invoices
                '    Dim rows As List(Of DataRow) = kvp.Value
                '    Dim dr As DataRow = rows(0)   ' first row = header

                '    Dim frm As New ExpenseVoucher()
                '    frm.CMBNAME.Text = dr("name").ToString()
                '    frm.RunCmbNameValidation()   ' 🔹 Ensures correct GST split
                '    If String.IsNullOrEmpty(frm.NPDATE.Text) Or frm.NPDATE.Text = "__/__/____" Then
                '        frm.NPDATE.Text = DateTime.Now.ToString("dd/MM/yyyy")
                '    End If
                '    frm.TXTPARTYBILLNO.Text = dr("party bill no").ToString()
                '    frm.PARTYBILLDATE.Text = dr("party bill date").ToString()
                '    'frm.PARTYBILLDATE.Text = Format(Convert.ToDateTime(dr("bill date")), "dd/MM/yyyy")
                '    frm.txtremarks.Text = dr("OTHER REF (REMARKS)").ToString()
                '    'frm.TXTSACCODE.Text = dr("SAC CODE").ToString()

                '    frm.GRIDEXPENSE.Rows.Clear()
                '    Dim sr As Integer = 1
                '    ' Before adding items in rows, fetch once:
                '    Dim otherAmt As Decimal = 0
                '    If dr.Table.Columns.Contains("OTHER AMT") AndAlso dr("OTHER AMT").ToString().Trim() <> "" Then
                '        otherAmt = Val(dr("OTHER AMT"))
                '    End If
                '    For Each r As DataRow In rows
                '        ' -----------------------
                '        ' Handle first ITEM NAME (main item)
                '        ' -----------------------
                '        If r.Table.Columns.Contains("ITEM NAME") AndAlso r("ITEM NAME").ToString().Trim() <> "" Then
                '            frm.GRIDEXPENSE.Rows.Add(sr, "WEAVING CHARGES", dr("SAC CODE").ToString(), r("ITEM NAME").ToString(), Val(r("QTY")), Val(r("RATE")), Val(r("AMOUNT")), otherAmt, 0, 0, 0, 0, 0, 0, 0, 0, Val(r("AMOUNT")))

                '            Dim lastRow As DataGridViewRow = frm.GRIDEXPENSE.Rows(frm.GRIDEXPENSE.Rows.Count - 1)

                '            ' Populate form fields for CALC()
                '            frm.TXTQTY.Text = Val(r("QTY")).ToString()
                '            frm.TXTRATE.Text = Val(r("RATE")).ToString()
                '            frm.TXTTAXABLEAMT.Text = Val(r("AMOUNT")).ToString()
                '            frm.CMBHSNCODE.Text = dr("SAC CODE").ToString()

                '            frm.GETHSNCODE()
                '            frm.CALC()

                '            ' Copy GST values back
                '            lastRow.Cells("GCGSTPER").Value = frm.TXTCGSTPER.Text
                '            lastRow.Cells("GCGSTAMT").Value = frm.TXTCGSTAMT.Text
                '            lastRow.Cells("GSGSTPER").Value = frm.TXTSGSTPER.Text
                '            lastRow.Cells("GSGSTAMT").Value = frm.TXTSGSTAMT.Text
                '            lastRow.Cells("GIGSTPER").Value = frm.TXTIGSTPER.Text
                '            lastRow.Cells("GIGSTAMT").Value = frm.TXTIGSTAMT.Text
                '            lastRow.Cells("GTAXABLEAMT").Value = frm.TXTTAXABLEAMT.Text
                '            lastRow.Cells("GGRIDTOTAL").Value = frm.TXTGRIDTOTAL.Text

                '            sr += 1
                '        End If

                '        ' -----------------------
                '        ' Handle ITEM NAME 1, ITEMNAME 2, ITEM NAME 3
                '        ' -----------------------
                '        For i As Integer = 1 To 3
                '            Dim itemColOptions = {$"ITEM NAME {i}", $"ITEMNAME {i}", $"ITEMNAME{i}"}
                '            Dim qtyColOptions = {$"QTY {i}", $"QTY{i}"}
                '            Dim rateColOptions = {$"RATE {i}", $"RATE{i}"}
                '            Dim amountColOptions = {$"AMOUNT {i}", $"AMOUNT{i}"}

                '            Dim itemCol = itemColOptions.FirstOrDefault(Function(c) r.Table.Columns.Contains(c))
                '            Dim qtyCol = qtyColOptions.FirstOrDefault(Function(c) r.Table.Columns.Contains(c))
                '            Dim rateCol = rateColOptions.FirstOrDefault(Function(c) r.Table.Columns.Contains(c))
                '            Dim amountCol = amountColOptions.FirstOrDefault(Function(c) r.Table.Columns.Contains(c))

                '            If Not String.IsNullOrEmpty(itemCol) AndAlso r(itemCol).ToString().Trim() <> "" Then
                '                frm.GRIDEXPENSE.Rows.Add(sr, "WEAVING CHARGES", dr("SAC CODE").ToString(), r(itemCol).ToString(), Val(r(qtyCol)), Val(r(rateCol)), Val(r(amountCol)), 0, 0, 0, 0, 0, 0, 0, 0, 0, Val(r(amountCol)))

                '                Dim lastRow As DataGridViewRow = frm.GRIDEXPENSE.Rows(frm.GRIDEXPENSE.Rows.Count - 1)

                '                ' Populate form fields for CALC()
                '                frm.TXTQTY.Text = Val(r(qtyCol)).ToString()
                '                frm.TXTRATE.Text = Val(r(rateCol)).ToString()
                '                frm.TXTTAXABLEAMT.Text = Val(r(amountCol)).ToString()
                '                frm.CMBHSNCODE.Text = dr("SAC CODE").ToString()

                '                frm.GETHSNCODE()
                '                frm.CALC()

                '                ' Copy GST values back
                '                lastRow.Cells("GCGSTPER").Value = frm.TXTCGSTPER.Text
                '                lastRow.Cells("GCGSTAMT").Value = frm.TXTCGSTAMT.Text
                '                lastRow.Cells("GSGSTPER").Value = frm.TXTSGSTPER.Text
                '                lastRow.Cells("GSGSTAMT").Value = frm.TXTSGSTAMT.Text
                '                lastRow.Cells("GIGSTPER").Value = frm.TXTIGSTPER.Text
                '                lastRow.Cells("GIGSTAMT").Value = frm.TXTIGSTAMT.Text
                '                lastRow.Cells("GTAXABLEAMT").Value = frm.TXTTAXABLEAMT.Text
                '                lastRow.Cells("GGRIDTOTAL").Value = frm.TXTGRIDTOTAL.Text

                '                sr += 1
                '            End If
                '        Next
                '    Next

                '    ' 🔹 Refresh totals after all rows
                '    frm.TOTAL()
                '    ' Set Register Name
                '    frm.CMBREGISTER.Text = "NON-PURCHASE REGISTER" ' or get from Excel if dynamic
                '    frm.CanUserAdd = True
                '    frm.TXTNPNO.Text = GETMAXNO().ToString()
                '    ' Save invoice
                '    If frm.SaveInvoice(True) Then
                '        successCount += 1
                '    Else
                '        errorCount += 1
                '        MessageBox.Show("Error uploading voucher for Party Bill No: " & frm.TXTPARTYBILLNO.Text, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    End If
                'Next
                Dim successCount As Integer = 0
                Dim errorCount As Integer = 0

                ' Normalize column existence helpers
                'Dim functionCols = Function(name As String) dt.Columns.Contains(name)
                Dim failedRows As New List(Of String)
                Dim processedPartyBillNos As New HashSet(Of String)() ' Track processed party bill numbers
                Dim duplicateBillNos As New List(Of String)()
                For Each dr As DataRow In dt.Rows
                    Dim partyBillNo As String = dr("party bill no").ToString().Trim()

                    '' Check if party bill no is already in the set of processed numbers
                    'If processedPartyBillNos.Contains(partyBillNo) Then
                    '    Continue For ' Skip this row if it's already processed
                    'End If

                    '' Add the current party bill number to the set of processed numbers
                    'processedPartyBillNos.Add(partyBillNo)
                    Try
                        Dim partyName As String = dr("name").ToString().Trim()
                        If Not PartyExists(partyName) Then
                            failedRows.Add("Row " & (dt.Rows.IndexOf(dr) + 2) & " ('" & partyName & "')")
                            'Increment error count
                            errorCount += 1
                            Continue For
                        End If
                        If IsPartyBillNoAlreadySavedInDB(partyBillNo) Then
                            duplicateBillNos.Add("Row " & (dt.Rows.IndexOf(dr) + 2) & " - Party Bill No: " & partyBillNo & " (already in DB)")
                            ' Increment error count
                            errorCount += 1
                            Continue For
                        End If

                        ' Skip empty lines (no item and no amount)
                        'Dim hasMainItem As Boolean = functionCols("ITEM NAME") AndAlso dr("ITEM NAME").ToString().Trim() <> ""
                        'Dim hasAnyAmount As Boolean = functionCols("AMOUNT") AndAlso dr("AMOUNT").ToString().Trim() <> ""
                        'If Not hasMainItem AndAlso Not hasAnyAmount Then Continue For

                        Dim frm As New ExpenseVoucher()
                        frm.IsBulkUpload = True

                        ' Basic header fields
                        frm.CMBREGISTER.Text = "NON-PURCHASE REGISTER"
                        frm.CanUserAdd = True

                        ' Generate next NP no (if applicable in this form)
                        frm.TXTNPNO.Text = GETMAXNO().ToString()

                        frm.CMBNAME.Text = dr("name").ToString().Trim()
                        frm.RunCmbNameValidation()   ' sets GST split context etc.

                        ' Dates: party bill and voucher date
                        Dim billDate As String = dr("party bill date").ToString().Trim()
                        If String.IsNullOrWhiteSpace(billDate) Then
                            frm.PARTYBILLDATE.Text = DateTime.Now.ToString("dd/MM/yyyy")
                        Else
                            frm.PARTYBILLDATE.Text = billDate
                        End If
                        If String.IsNullOrEmpty(frm.NPDATE.Text) OrElse frm.NPDATE.Text = "__/__/____" Then
                            frm.NPDATE.Text = DateTime.Now.ToString("dd/MM/yyyy")
                        End If
                        frm.TXTPARTYBILLNO.Text = dr("party bill no").ToString().Trim()

                        If dt.Columns.Contains("OTHER REF (REMARKS)") Then
                            frm.txtremarks.Text = dr("OTHER REF (REMARKS)").ToString().Trim()
                        End If
                        frm.CHKMANUAL.Checked = True



                        ' Prepare grid
                        frm.GRIDEXPENSE.Rows.Clear()
                        Dim sr As Integer = 1

                        Dim sacCode As String = dr("SAC CODE").ToString().Trim()
                        Dim otherAmt As Decimal = If(dt.Columns.Contains("OTHER AMT"), Val(dr("OTHER AMT")), 0D)

                        ' ==== Loop through 1 to 4 item sets ====
                        Dim itemSets = New List(Of Integer) From {0, 1, 2, 3}
                        For Each i In itemSets
                            Dim itemCol As String = If(i = 0, "ITEM NAME", $"ITEM NAME {i}")
                            Dim qtyCol As String = If(i = 0, "QTY", $"QTY {i}")
                            Dim rateCol As String = If(i = 0, "RATE", $"RATE {i}")
                            Dim amtCol As String = If(i = 0, "AMOUNT", $"AMOUNT {i}")

                            'If dt.Columns.Contains(itemCol) AndAlso Not String.IsNullOrWhiteSpace(dr(itemCol).ToString()) Then
                            'Dim itemName As String = dr(itemCol).ToString().Trim()
                            'Dim qty As Decimal = If(IsNumeric(dr(qtyCol)), Val(dr(qtyCol)), 0)
                            'Dim rate As Decimal = If(IsNumeric(dr(rateCol)), Val(dr(rateCol)), 0)
                            'Dim amt As Decimal = If(IsNumeric(dr(amtCol)), Val(dr(amtCol)), 0)
                            '' Add row to expense grid
                            'frm.GRIDEXPENSE.Rows.Add(sr, "WEAVING CHARGES", sacCode, itemName, qty, rate, amt,
                            '             If(i = 0, otherAmt, 0), 0, 0, 0, 0, 0, 0, 0, 0, amt)
                            If Not dt.Columns.Contains(itemCol) Then Continue For
                            Dim itemName As String = dr(itemCol).ToString().Trim()
                            If String.IsNullOrWhiteSpace(itemName) Then Continue For
                            Dim qty As Decimal = If(dt.Columns.Contains(qtyCol), Val(dr(qtyCol)), 0)
                            Dim rate As Decimal = If(dt.Columns.Contains(rateCol), Val(dr(rateCol)), 0)
                            Dim amt As Decimal = If(dt.Columns.Contains(amtCol), Val(dr(amtCol)), 0)

                            Dim gridRowIndex As Integer = frm.GRIDEXPENSE.Rows.Add()
                            Dim gridRow As DataGridViewRow = frm.GRIDEXPENSE.Rows(gridRowIndex)
                            gridRow.Cells("srno").Value = sr
                            gridRow.Cells("GDRNAME").Value = "WEAVING CHARGES"
                            gridRow.Cells("GHSNCODE").Value = sacCode
                            gridRow.Cells("GNOTE").Value = itemName
                            gridRow.Cells("GQTY").Value = qty
                            gridRow.Cells("GRATE").Value = rate
                            gridRow.Cells("gAMT").Value = amt
                            gridRow.Cells("GOTHERAMT").Value = If(i = 0, otherAmt, 0)
                            ' Select last added row to populate GST
                            'Dim lastRow As DataGridViewRow = frm.GRIDEXPENSE.Rows(frm.GRIDEXPENSE.Rows.Count - 1)

                            frm.TXTQTY.Text = qty.ToString()
                            frm.TXTRATE.Text = rate.ToString()
                            frm.TXTTAXABLEAMT.Text = amt.ToString()
                            frm.CMBHSNCODE.Text = sacCode

                            frm.GETHSNCODE()
                            'frm.CALC()
                            'lastRow.Cells("GCGSTPER").Value = frm.TXTCGSTPER.Text
                            '    lastRow.Cells("GCGSTAMT").Value = frm.TXTCGSTAMT.Text
                            '    lastRow.Cells("GSGSTPER").Value = frm.TXTSGSTPER.Text
                            '    lastRow.Cells("GSGSTAMT").Value = frm.TXTSGSTAMT.Text
                            '    lastRow.Cells("GIGSTPER").Value = frm.TXTIGSTPER.Text
                            '    lastRow.Cells("GIGSTAMT").Value = frm.TXTIGSTAMT.Text
                            '    lastRow.Cells("GTAXABLEAMT").Value = frm.TXTTAXABLEAMT.Text
                            '    lastRow.Cells("GGRIDTOTAL").Value = frm.TXTGRIDTOTAL.Text
                            ' Add to grid


                            gridRow.Cells("GCGSTPER").Value = frm.TXTCGSTPER.Text
                            gridRow.Cells("GCGSTAMT").Value = frm.TXTCGSTAMT.Text
                            gridRow.Cells("GSGSTPER").Value = frm.TXTSGSTPER.Text
                            gridRow.Cells("GSGSTAMT").Value = frm.TXTSGSTAMT.Text
                            gridRow.Cells("GIGSTPER").Value = frm.TXTIGSTPER.Text
                            gridRow.Cells("GIGSTAMT").Value = frm.TXTIGSTAMT.Text
                            gridRow.Cells("GTAXABLEAMT").Value = frm.TXTTAXABLEAMT.Text
                            gridRow.Cells("GGRIDTOTAL").Value = frm.TXTGRIDTOTAL.Text
                            sr += 1
                            'End If
                        Next

                        frm.TOTAL()

                        If frm.SaveInvoice(False) Then
                            successCount += 1
                        Else
                            errorCount += 1
                        End If

                    Catch ex As Exception
                        errorCount += 1
                        ' Optional: Log error - MsgBox(ex.Message)
                    End Try
                Next
                If failedRows.Count > 0 Then
                    MessageBox.Show("The following rows were not saved because party name not found:" & vbCrLf & String.Join(vbCrLf, failedRows), "Party Name Not Present", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                If duplicateBillNos.Count > 0 Then
                    MessageBox.Show("The following Excel rows were skipped because their Party Bill No already exists in the system:" &
                    vbCrLf & vbCrLf & String.Join(vbCrLf, duplicateBillNos),
                    "Duplicate Entries Skipped", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

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
                    Dim dtSONO As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS DELIVERYTO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(SALEORDER_DESC.SO_GRIDREMARKS, '') AS [DESC], ISNULL(SALEORDER_DESC.SO_QTY,  0) AS PCS, ISNULL(SALEORDER_DESC.SO_CUT, 0) AS CUT, ISNULL(SALEORDER_DESC.SO_RATE, 0) AS RATE, ISNULL(SALEORDER_DESC.SO_PER, '') AS PER, ISNULL(SALEORDER_DESC.SO_AMOUNT, 0) AS AMOUNT, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(SALEORDER.SO_CD, 0) AS DISCOUNT, ISNULL(SALEORDER.SO_DAYS, 0) AS CRDAYS", "SALEORDER INNER JOIN SALEORDER_DESC ON SALEORDER.so_no = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON SALEORDER.so_Agentid = AGENTLEDGERS.Acc_id LEFT OUTER JOIN                          COLORMASTER ON SALEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON SALEORDER_DESC.SO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON SALEORDER_DESC.SO_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON SALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON SALEORDER.SO_transid = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON SALEORDER.SO_PACKINGID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON SALEORDER.so_ledgerid = LEDGERS.Acc_id ", "AND SO_NO = '" & sono & "' AND SO_YEARID = " & YearId)
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
            Throw ex
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

    Private Function GETMAXNO() As Integer
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
            Throw ex
        End Try

        Return nextNo
    End Function
    ' Helper function: Checks if party name exists in LEDGERS for current year
    Private Function PartyExists(partyName As String) As Boolean
        Dim OBJCMN As New ClsCommon()
        Dim dtParty As DataTable = OBJCMN.SEARCH("ACC_CMPNAME", "", "LEDGERS", "And ACC_CMPNAME = '" & partyName.Replace("'", "''") & "' AND ACC_YEARID = " & YearId)
        Return dtParty.Rows.Count > 0
    End Function
    Private Function IsPartyBillNoAlreadySavedInDB(partyBillNo As String) As Boolean
        Dim OBJCMN As New ClsCommon()
        Dim dtExist As DataTable = OBJCMN.SEARCH("NP_REFNO", "", "NONPURCHASE", "AND NP_REFNO = " & partyBillNo.Replace("'", "''") & " AND NP_YEARID = " & YearId)
        Return dtExist.Rows.Count > 0
    End Function
End Class