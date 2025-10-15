
Imports System.Runtime.InteropServices
Imports BL

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
        TXTPATH.Clear()
    End Sub

    Private Sub UploadExcelNonPurchase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If CMBTYPE.Text.Trim = "NONPURCHASE" Then
                fillregister(cmbregister, " and register_type = 'EXPENSE'")
            Else
                fillregister(cmbregister, " and register_type = 'SALE'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
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

        If cmbregister.Text.Trim = "" Then
            MessageBox.Show("Please select Register first.")
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
            Debug.Print("Opening Excel")
            oExcel = New Excel.Application()
            oBook = oExcel.Workbooks.Open(TXTPATH.Text.Trim)
            oSheet = CType(oBook.Sheets(1), Excel.Worksheet) ' Use Sheet1 or change as needed

            ' 2. Read Header Row
            Debug.Print("Reading header")
            Dim colIndex As Integer = 1
            While oSheet.Cells(1, colIndex).Value IsNot Nothing
                dt.Columns.Add(oSheet.Cells(1, colIndex).Value.ToString())
                colIndex += 1
            End While

            ' 3. Read Data Rows
            Debug.Print("Reading data rows")
            Dim rowIndex As Integer = 2
            'While oSheet.Cells(rowIndex, 1).Value IsNot Nothing
            '    Dim row As DataRow = dt.NewRow()
            '    For i As Integer = 1 To dt.Columns.Count
            '        row(i - 1) = Convert.ToString(oSheet.Cells(rowIndex, i).Value)
            '    Next
            '    dt.Rows.Add(row)
            '    rowIndex += 1
            'End While
            While oSheet.Cells(rowIndex, 1).Value IsNot Nothing
                Dim row As DataRow = dt.NewRow()
                For i As Integer = 1 To dt.Columns.Count
                    row(i - 1) = Convert.ToString(oSheet.Cells(rowIndex, i).Value)
                Next
                If CMBTYPE.Text.Trim = "NONPURCHASE" Then

                    ' Check if the 'name' field is blank during reading
                    If String.IsNullOrWhiteSpace(row("name").ToString()) Then
                        MessageBox.Show("Party name cannot be blank at Row " & rowIndex, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        'Exit Sub ' Exit the method if name is blank
                    End If
                Else
                    If String.IsNullOrWhiteSpace(row("CHALLAN NO").ToString()) Then
                        MessageBox.Show("Challan No cannot be blank at Row " & rowIndex, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        'Exit Sub ' Exit the method if name is blank
                    End If
                End If
                dt.Rows.Add(row)
                rowIndex += 1
            End While
            Debug.Print("Finished reading rows. Total rows = " & dt.Rows.Count)
            If dt.Rows.Count = 0 Then
                MessageBox.Show("Excel file is empty.")
                Debug.Print("Exiting because dt empty")
                Exit Sub
            End If
            If CMBTYPE.Text.Trim = "NONPURCHASE" Then
                ' Validate that "name" field is not blank
                Dim blankedfailedRows As New List(Of String)()
                For Each dr As DataRow In dt.Rows
                    If String.IsNullOrWhiteSpace(dr("name").ToString()) Then
                        blankedfailedRows.Add("Row " & (dt.Rows.IndexOf(dr) + 2) & " ('" & dr("name").ToString() & "') - Party name cannot be blank.")
                    End If
                Next

                ' If there are any invalid rows, show the validation message
                If blankedfailedRows.Count > 0 Then
                    MessageBox.Show("The following rows were skipped because the party name is blank:" & vbCrLf & String.Join(vbCrLf, blankedfailedRows), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    'Exit Sub
                End If
            Else
                ' Validate that "Challan No" field is not blank
                Dim blankedfailedRows As New List(Of String)()
                For Each dr As DataRow In dt.Rows
                    If String.IsNullOrWhiteSpace(dr("CHALLAN NO").ToString()) Then
                        blankedfailedRows.Add("Row " & (dt.Rows.IndexOf(dr) + 2) & " ('" & dr("CHALLAN NO").ToString() & "') - Challan No cannot be blank.")
                    End If
                Next

                ' If there are any invalid rows, show the validation message
                If blankedfailedRows.Count > 0 Then
                    MessageBox.Show("The following rows were skipped because the party name is blank:" & vbCrLf & String.Join(vbCrLf, blankedfailedRows), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    'Exit Sub
                End If
            End If
            ' Check CMBTYPE to decide save destination
            If CMBTYPE.Text.Trim = "NONPURCHASE" Then
                Debug.Print("Entering NONPURCHASE branch")

                Dim successCount As Integer = 0
                Dim errorCount As Integer = 0

                ' Normalize column existence helpers
                'Dim functionCols = Function(name As String) dt.Columns.Contains(name)
                Dim failedRows As New List(Of String)
                Dim processedPartyBillNos As New HashSet(Of String)() ' Track processed party bill numbers
                Dim duplicateBillNos As New List(Of String)()
                Dim missingHSNRows As New List(Of String) ' To store missing HSN rows
                For Each dr As DataRow In dt.Rows
                    Debug.Print("Processing Excel row index in dt: " & dt.Rows.IndexOf(dr))
                    Dim partyBillNo As String = dr("party bill no").ToString().Trim()
                    Debug.Print("PartyBillNo = '" & partyBillNo & "'")

                    Try
                        Dim partyName As String = dr("name").ToString().Trim()
                        If Not PartyExists(partyName) Then
                            failedRows.Add("Row " & (dt.Rows.IndexOf(dr) + 2) & " ('" & partyName & "')")
                            'Increment error count
                            errorCount += 1
                            Continue For
                        End If

                        Dim sacCode As String = dr("SAC CODE").ToString().Trim()
                        If Not HSNExists(sacCode) Then
                            ' Add to missing list: Excel row number (start at 2 for first data row)
                            missingHSNRows.Add("Row " & (dt.Rows.IndexOf(dr) + 2).ToString() & " (HSN: '" & sacCode & "')")
                            errorCount += 1 ' Increment error count as in your logic
                            Continue For ' Skip this entry, do not save
                        End If

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

                        'Dim sacCode As String = dr("SAC CODE").ToString().Trim()
                        Dim otherAmt As Decimal = If(dt.Columns.Contains("OTHER AMT"), Val(dr("OTHER AMT")), 0D)
                        Dim taxableamt As Decimal = If(dt.Columns.Contains("Taxable AMT"), Val(dr("Taxable AMT")), 0D)
                        Dim grandtotal As Decimal = If(dt.Columns.Contains("GRAND TOTAL"), Val(dr("GRAND TOTAL")), 0D)

                        ' ==== Loop through 1 to 4 item sets ====
                        Dim itemSets = New List(Of Integer) From {0, 1, 2, 3}
                        For Each i In itemSets
                            Dim itemCol As String = If(i = 0, "ITEM NAME", $"ITEM NAME {i}")
                            Dim qtyCol As String = If(i = 0, "QTY", $"QTY {i}")
                            Dim rateCol As String = If(i = 0, "RATE", $"RATE {i}")
                            Dim amtCol As String = If(i = 0, "AMOUNT", $"AMOUNT {i}")

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
                            gridRow.Cells("GTAXABLEAMT").Value = taxableamt
                            gridRow.Cells("GGRIDTOTAL").Value = grandtotal

                            ' Select last added row to populate GST
                            'Dim lastRow As DataGridViewRow = frm.GRIDEXPENSE.Rows(frm.GRIDEXPENSE.Rows.Count - 1)

                            frm.TXTQTY.Text = qty.ToString()
                            frm.TXTRATE.Text = rate.ToString()
                            frm.TXTTAXABLEAMT.Text = amt.ToString()
                            frm.CMBHSNCODE.Text = sacCode

                            frm.GETHSNCODE()

                            gridRow.Cells("GCGSTPER").Value = frm.TXTCGSTPER.Text
                            gridRow.Cells("GCGSTAMT").Value = frm.TXTCGSTAMT.Text
                            gridRow.Cells("GSGSTPER").Value = frm.TXTSGSTPER.Text
                            gridRow.Cells("GSGSTAMT").Value = frm.TXTSGSTAMT.Text
                            gridRow.Cells("GIGSTPER").Value = frm.TXTIGSTPER.Text
                            gridRow.Cells("GIGSTAMT").Value = frm.TXTIGSTAMT.Text
                            sr += 1
                        Next

                        frm.TOTAL()
                        Debug.Print("Passed duplicate check, now doing save")
                        frm.IsBulkUploadtds = True
                        If frm.SaveInvoice(False) Then
                            successCount += 1
                            If frm.CHKTDS.CheckState = CheckState.Checked Then
                                Dim OBJTDS As New DeductTDS()
                                OBJTDS.AutoDeductTDS(frm.TXTNPNO.Text.Trim(), frm.CMBREGISTER.Text.Trim())
                            End If
                        Else
                            errorCount += 1
                        End If

                    Catch exRow As Exception
                        Debug.Print("Exception in row: " & exRow.Message)
                        ' Handle error in individual row, continue to next
                        duplicateBillNos.Add("Row " & (dt.Rows.IndexOf(dr) + 2) & " - Error: " & exRow.Message)
                        errorCount += 1
                        Continue For
                    End Try
                Next
                Debug.Print("After loop, preparing to show messages")
                If failedRows.Count > 0 Then
                    MessageBox.Show("The following rows were not saved because party name not found:" & vbCrLf & String.Join(vbCrLf, failedRows), "Party Name Not Present", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                If duplicateBillNos.Count > 0 Then
                    Debug.Print("duplicateBillNos count = " & duplicateBillNos.Count)
                    MessageBox.Show("The following Excel rows were skipped because their Party Bill No already exists in the system:" &
                    vbCrLf & vbCrLf & String.Join(vbCrLf, duplicateBillNos),
                    "Duplicate Entries Skipped", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                If missingHSNRows.Count > 0 Then
                    MessageBox.Show("The following Excel rows were not saved because their HSN/SAC code was not found in the database:" &
                     vbCrLf & String.Join(vbCrLf, missingHSNRows), "Missing HSN/SAC Codes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

                MessageBox.Show(successCount & " vouchers uploaded successfully. " & errorCount & " failed.", "Upload Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)


            ElseIf CMBTYPE.Text.Trim = "INVOICE" Then

                Debug.Print("Entering INVOICE branch")

                Dim successCount As Integer = 0
                Dim errorCount As Integer = 0
                Dim duplicateList As New List(Of String)
                Dim failedRows As New List(Of String)

                For Each dr As DataRow In dt.Rows

                    Dim INVOICENO As String = dr("CHALLAN NO").ToString().Trim()
                    Dim INVOICEDATE As Date = Convert.ToDateTime(dr("DATE")).Date
                    Dim SONO As String = dr("SO NO").ToString().Trim()
                    Dim TRANSPORT As String = dr("TRANSPORT").ToString().Trim()
                    Dim LRNO As String = dr("LR NO").ToString().Trim()
                    Dim LRDATE As String = dr("LR DATE").ToString().Trim()
                    Dim TOTALPCS As String = dr("TOTAL PCS").ToString().Trim()
                    Dim TOTALMTRS As String = dr("TOTAL MTRS").ToString().Trim()
                    Dim BALEFROM As String = dr("BALE NO FROM").ToString().Trim()
                    Dim BALETO As String = dr("BALE NO TO").ToString().Trim()


                    ' Check for duplicate invoice
                    'Dim invNo As String = row("CHALLAN NO").ToString().Trim()
                    Dim checkDuplicate As DataTable = OBJCMN.SEARCH("INVOICE_NO", "", " INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTERMASTER.REGISTER_ID", " AND INVOICE_NO = " & Val(INVOICENO) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId)
                    If checkDuplicate.Rows.Count > 0 Then
                        duplicateList.Add("Row " & (dt.Rows.IndexOf(dr) + 2).ToString() & " (Invoice: " & INVOICENO & ")")
                        Continue For  ' skip this record, don't add to invoice
                    End If

                    If Not PartyExists(TRANSPORT) Then
                        failedRows.Add("Row " & (dt.Rows.IndexOf(dr) + 2) & " ('" & TRANSPORT & "')")
                        'Increment error count
                        errorCount += 1
                        Continue For
                    End If

                    ' Fetch SONO details
                    Dim dtSONO As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ALLSALEORDER.SO_DATE AS SODATE, ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS DELIVERYTO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(ALLSALEORDER_DESC.SO_GRIDREMARKS, '') AS [DESC], ISNULL(ALLSALEORDER_DESC.SO_QTY,  0) AS PCS, ISNULL(ALLSALEORDER_DESC.SO_CUT, 0) AS CUT, ISNULL(ALLSALEORDER_DESC.SO_RATE, 0) AS RATE, ISNULL(ALLSALEORDER_DESC.SO_PER, '') AS PER, ISNULL(ALLSALEORDER_DESC.SO_AMOUNT, 0) AS AMOUNT, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(ALLSALEORDER.SO_CD, 0) AS DISCOUNT, ISNULL(ALLSALEORDER.SO_DAYS, 0) AS CRDAYS, ISNULL(ALLSALEORDER_DESC.SO_MTRS, 0) AS MTRS, ISNULL(ALLSALEORDER_DESC.SO_RATE, 0) AS RATE, ISNULL(ALLSALEORDER_DESC.SO_GRIDSRNO, 0) AS SOSRNO, ISNULL(ALLSALEORDER_DESC.SO_GRIDREMARKS, '') AS GRIDDESC, ISNULL(ALLSALEORDER.so_pono, '') AS PARTYPONO , ISNULL(ALLSALEORDER.SO_ORDERON, '') AS ORDERON, ALLSALEORDER.TYPE, ISNULL(SO_DISCDEALER, 0) AS DISCPER, ISNULL(SO_CD, 0) AS CDPER, ISNULL(SO_DISCRATE,0) AS RATEDIFF, ISNULL(SO_INT,0) AS AGENTCOMM, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME,ISNULL(ALLSALEORDER.so_remarks,'') AS REMARKS ", "", "ALLSALEORDER INNER JOIN ALLSALEORDER_DESC ON ALLSALEORDER.so_no = ALLSALEORDER_DESC.SO_NO AND ALLSALEORDER.SO_YEARID = ALLSALEORDER_DESC.SO_YEARID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON ALLSALEORDER.so_Agentid = AGENTLEDGERS.Acc_id LEFT OUTER JOIN COLORMASTER ON ALLSALEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ALLSALEORDER_DESC.SO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON ALLSALEORDER_DESC.SO_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON ALLSALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON ALLSALEORDER.SO_transid = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON ALLSALEORDER.SO_PACKINGID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON ALLSALEORDER.so_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON ALLSALEORDER.so_cityid = CITYMASTER.CITY_ID ", " AND ALLSALEORDER.SO_NO = " & Val(SONO) & " AND ALLSALEORDER.SO_YEARID = " & YearId)
                    If dtSONO.Rows.Count = 0 Then
                        MessageBox.Show("SONO " & SONO & " not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        errorCount += 1
                        Continue For
                    End If

                    Try
                        Dim frmInv As New InvoiceMaster()
                        frmInv.RunLoad()
                        frmInv.IsBulkUpload = True
                        frmInv.CanUserAdd = True


                        frmInv.TXTINVOICENO.Text = INVOICENO
                        frmInv.cmbregister.Text = cmbregister.Text.Trim
                        frmInv.cmbname.Text = dtSONO.Rows(0)("PARTYNAME").ToString()
                        frmInv.RunCmbNameValidation(frmInv.cmbname, EventArgs.Empty)

                        frmInv.TXTBALENOFROM.Text = BALEFROM
                        frmInv.TXTBALENOTO.Text = BALETO

                        frmInv.INVOICEDATE.Text = Format(Convert.ToDateTime(INVOICEDATE).Date, "dd/MM/yyyy")
                        frmInv.CHALLANDATE.Text = Format(Convert.ToDateTime(INVOICEDATE).Date, "dd/MM/yyyy")
                        frmInv.GPDATE.Text = Format(Convert.ToDateTime(INVOICEDATE).Date, "dd/MM/yyyy")
                        If String.IsNullOrEmpty(frmInv.DTDOCKETDATE.Text) OrElse frmInv.DTDOCKETDATE.Text = "__/__/____" Then
                            frmInv.DTDOCKETDATE.Text = DateTime.Now.ToString("dd/MM/yyyy")
                        End If


                        frmInv.CMBITEM.Text = dtSONO.Rows(0)("ITEMNAME").ToString()
                        frmInv.GETHSNCODE()


                        frmInv.TXTSONO.Text = SONO
                        frmInv.TXTMULTISONO.Text = SONO
                        frmInv.sodate.Text = dtSONO.Rows(0)("SODATE").ToString()
                        frmInv.txtpartypono.Text = dtSONO.Rows(0)("PARTYPONO")
                        frmInv.CMBAGENT.Text = dtSONO.Rows(0)("AGENT").ToString()
                        frmInv.cmbtrans.Text = TRANSPORT
                        frmInv.CMBTOCITY.Text = dtSONO.Rows(0)("CITYNAME").ToString()
                        frmInv.CMBPACKING.Text = dtSONO.Rows(0)("DELIVERYTO").ToString()
                        frmInv.txtremarks.Text = dtSONO.Rows(0)("REMARKS").ToString()

                        frmInv.TXTCRDAYS.Text = dtSONO.Rows(0)("CRDAYS").ToString()
                        frmInv.duedate.Value = Convert.ToDateTime(frmInv.INVOICEDATE.Text).Date.AddDays(Val(frmInv.TXTCRDAYS.Text.Trim))

                        frmInv.txtlrno.Text = LRNO
                        frmInv.LRDATE.Text = LRDATE
                        frmInv.CMBFROMCITY.Text = CMPCITYNAME


                        'ADDING IN EXTRACHGS
                        For Each DTROW As DataGridViewRow In frmInv.GRIDCHGS.Rows
                            If DTROW.Cells(frmInv.ECHARGES.Index).Value = "RATE DIFFERENCE" Then GoTo NEXTLINE
                        Next
                        If Val(dtSONO.Rows(0)("RATEDIFF")) > 0 Then frmInv.GRIDCHGS.Rows.Add(frmInv.GRIDCHGS.RowCount + 1, "RATE DIFFERENCE", Val(dtSONO.Rows(0)("RATEDIFF")) * -1, 0, 0)

                        For Each DTROW As DataGridViewRow In frmInv.GRIDCHGS.Rows
                            If DTROW.Cells(frmInv.ECHARGES.Index).Value = "DISCOUNT GIVEN" Then GoTo NEXTLINE
                        Next
                        If Val(dtSONO.Rows(0)("DISCPER")) > 0 Then frmInv.GRIDCHGS.Rows.Add(frmInv.GRIDCHGS.RowCount + 1, "DISCOUNT GIVEN", Val(dtSONO.Rows(0)("DISCPER")) * -1, 0, 0)

                        For Each DTROW As DataGridViewRow In frmInv.GRIDCHGS.Rows
                            If DTROW.Cells(frmInv.ECHARGES.Index).Value = "CASH DISCOUNT" Then GoTo NEXTLINE
                        Next
                        If Val(dtSONO.Rows(0)("CDPER")) > 0 Then frmInv.GRIDCHGS.Rows.Add(frmInv.GRIDCHGS.RowCount + 1, "CASH DISCOUNT", Val(dtSONO.Rows(0)("CDPER")) * -1, 0, 0)

                        'INITIALLY IT WAS WITH RESPECT TO THE ABOVE MENTIONED CLIENT, THEN CHANGED WITH RESPECT TO AUTOBROKERAGE
                        If AUTOBROKERAGE = True Then
                            For Each DTROW As DataGridViewRow In frmInv.GRIDCHGS.Rows
                                If DTROW.Cells(frmInv.ECHARGES.Index).Value = "BROKERAGE" Then GoTo NEXTLINE
                            Next
                            If Val(dtSONO.Rows(0)("AGENTCOMM")) > 0 Then frmInv.GRIDCHGS.Rows.Add(frmInv.GRIDCHGS.RowCount + 1, "BROKERAGE", Val(dtSONO.Rows(0)("AGENTCOMM")) * -1, 0, 0)
                        End If

NEXTLINE:

                        frmInv.GRIDORDER.Rows.Clear()
                        Dim srb As Integer = 1
                        For Each r As DataRow In dtSONO.Rows
                            Dim idx As Integer = frmInv.GRIDORDER.Rows.Add()
                            Dim g As DataGridViewRow = frmInv.GRIDORDER.Rows(idx)
                            g.Cells("OSRNO").Value = srb
                            g.Cells("OITEMNAME").Value = r("ITEMNAME").ToString()
                            g.Cells("ODESIGN").Value = r("DESIGNNO").ToString()
                            g.Cells("OCOLOR").Value = r("COLOR").ToString()
                            g.Cells("OPCS").Value = Val(r("PCS"))
                            g.Cells("OMTRS").Value = Val(r("MTRS"))
                            g.Cells("OFROMNO").Value = SONO
                            g.Cells("OFROMSRNO").Value = Val(r("SOSRNO"))
                            g.Cells("OFROMTYPE").Value = r("TYPE")
                            g.Cells("OGDNQTY").Value = 0
                            g.Cells("OGDNMTRS").Value = 0
                            g.Cells("ORATE").Value = Val(r("RATE"))
                            g.Cells("OPARTYPONO").Value = r("PARTYPONO").ToString()
                            g.Cells("OORDERON").Value = r("ORDERON").ToString()
                            srb += 1
                        Next


                        frmInv.lbltotalpcs.Text = TOTALPCS
                        frmInv.lbltotalmtrs.Text = TOTALMTRS


                        frmInv.GRIDINVOICE.Rows.Clear()
                        Dim sr As Integer = 1
                        For Each r As DataRow In dtSONO.Rows
                            Dim idx As Integer = frmInv.GRIDINVOICE.Rows.Add()
                            Dim g As DataGridViewRow = frmInv.GRIDINVOICE.Rows(idx)
                            g.Cells("GSRNO").Value = sr
                            g.Cells("GITEMNAME").Value = r("ITEMNAME").ToString()

                            Dim dtHSN As DataTable = OBJCMN.SEARCH("HSNMASTER.HSN_CODE", "", "HSNMASTER INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.ITEM_YEARID", "AND ITEMMASTER.ITEM_NAME = '" & r("ITEMNAME").Replace("'", "''") & "' AND HSNMASTER.HSN_YEARID = " & YearId)
                            If dtHSN.Rows.Count > 0 Then g.Cells("GHSNCODE").Value = dtHSN.Rows(0)("HSN_CODE").ToString()

                            g.Cells("GQUALITY").Value = r("QUALITY").ToString()
                            g.Cells("GDESIGN").Value = r("DESIGNNO").ToString()
                            g.Cells("GSHADE").Value = r("COLOR").ToString()
                            g.Cells("GQTY").Value = 0
                            g.Cells("GFOLDPER").Value = 0
                            g.Cells("GDESCRIPTION").Value = ""
                            g.Cells("GBALENO").Value = ""
                            g.Cells("GPCS").Value = TOTALPCS
                            g.Cells("GCUT").Value = 0
                            g.Cells("GMTRS").Value = TOTALMTRS
                            g.Cells("GRATE").Value = Val(r("RATE"))
                            g.Cells("GPER").Value = r("PER").ToString()
                            If r("PER") = "Pcs" Then g.Cells("GAMT").Value = Format(Val(r("RATE")) * Val(TOTALPCS), "0.00") Else g.Cells("GAMT").Value = Format(Val(r("RATE")) * Val(TOTALMTRS), "0.00")
                            g.Cells("GLRNO").Value = lrNo
                            g.Cells("GTRANS").Value = transport
                            g.Cells("GDISCPER").Value = 0
                            g.Cells("GDISCAMT").Value = 0
                            g.Cells("GSPDISCPER").Value = 0
                            g.Cells("GSPDISCAMT").Value = 0
                            g.Cells("GOTHERAMT").Value = 0
                            g.Cells("GTAXABLEAMT").Value = 0
                            g.Cells("GCGSTPER").Value = 0
                            g.Cells("GCGSTAMT").Value = 0
                            g.Cells("GSGSTPER").Value = 0
                            g.Cells("GSGSTAMT").Value = 0
                            g.Cells("GIGSTPER").Value = 0
                            g.Cells("GIGSTAMT").Value = 0
                            g.Cells("GGRIDTOTAL").Value = 0
                            g.Cells("GBARCODE").Value = ""
                            g.Cells("GFROMNO").Value = 0
                            g.Cells("GFROMSRNO").Value = 0
                            g.Cells("GFROMTYPE").Value = 0
                            g.Cells("GDONE").Value = 0
                            g.Cells("GPARTYPONO").Value = 0
                            g.Cells("GUNIT").Value = 0
                            g.Cells("GSONO").Value = sono
                            g.Cells("GSOSRNO").Value = Val(r("SOSRNO"))
                            g.Cells("GWT").Value = 0
                            g.Cells("GGRIDPURPARTY").Value = 0
                            g.Cells("GPURPARTYBILLNO").Value = 0

                            sr += 1
                        Next



                        'frmInv.GRIDCHGS.Rows.Clear()
                        'Dim src As Integer = 1
                        'For Each s As DataRow In dtSOCD.Rows
                        '    Dim idx As Integer = frmInv.GRIDCHGS.Rows.Add()
                        '    Dim g As DataGridViewRow = frmInv.GRIDCHGS.Rows(idx)
                        '    g.Cells("ESRNO").Value = src
                        '    g.Cells("ECHARGES").Value = "CASH DISCOUNT"
                        '    g.Cells("EPER").Value = -Math.Abs(Val(s("DISCOUNT")))
                        '    'g.Cells("EMT").Value = Val(s("AMT"))
                        '    src += 1
                        'Next

                        ' ==== 5️⃣ Calculate and Save ====
                        frmInv.CALC()
                        frmInv.TOTAL()

                        If frmInv.SaveInvoice(False) Then
                            successCount += 1
                            Debug.Print("Invoice " & invoiceNo & " created from SO " & sono)
                        Else
                            errorCount += 1
                        End If

                    Catch ex As Exception
                        MessageBox.Show("Error saving invoice no: " & invoiceNo & vbCrLf & ex.Message)
                        errorCount += 1
                    End Try
                Next
                MessageBox.Show(successCount & " invoices saved successfully, " & errorCount & " errors.", "Invoice Upload Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If duplicateList.Count > 0 Then
                    Dim msg As String = "⚠️ Duplicate invoices found and skipped:" & vbCrLf & String.Join(vbCrLf, duplicateList)
                    MsgBox(msg, vbExclamation, "Duplicate Invoices")
                Else
                    MsgBox("✅ All invoices uploaded successfully.", vbInformation)
                End If
                If failedRows.Count > 0 Then
                    MessageBox.Show("The following rows were not saved because party name not found:" & vbCrLf & String.Join(vbCrLf, failedRows), "Party Name Not Present", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                Debug.Print("Neither NONPURCHASE nor INVOICE, invalid CMBTYPE")
                MessageBox.Show("Select CMBTYPE as either NONPURCHASE or INVOICE.")
            End If

        Catch ex As Exception
            Debug.Print("Outer exception: " & ex.Message)
            MessageBox.Show("Error in upload: " & ex.Message)
            Throw ex
        Finally
            Debug.Print("Finally block, doing cleanup")
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
            Debug.Print("Exit cmdupload_Click")
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
        Dim dtExist As DataTable = OBJCMN.SEARCH("NP_REFNO", "", "NONPURCHASE", "AND NP_REFNO = '" & partyBillNo.Replace("'", "''") & "' AND NP_YEARID = " & YearId)
        Return dtExist.Rows.Count > 0
    End Function

    Private Function HSNExists(hsnCode As String) As Boolean
        Dim OBJCMN As New ClsCommon()
        Dim dtHSN As DataTable = OBJCMN.SEARCH("HSN_CODE", "", "HSNMASTER", "AND HSN_CODE = '" & hsnCode.Replace("'", "''") & "' AND HSN_YEARID = " & YearId)
        Return dtHSN.Rows.Count > 0
    End Function

End Class