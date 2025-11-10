Imports System.ComponentModel
Imports BL
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class SelectAdjustBills
    Public CMPNAME As String = ""
    Public AMOUNT As Integer
    Public BILLNO As String = ""
    Public REFNO As String = ""
    Public BILLDATE As Date = Now.Date
    Public FRMSTRING As String = ""
    Public BILLINTWHERECLAUSE As String = ""
    Public BILLINTPRINTWHERECLAUSE As String = ""
    Public DTBILLS As New DataTable
    Public Property RemAmount As String
    Public TDSDEDUCTEDAC As String = ""
    Public TDSDEDUCTEDAMT As Decimal


    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Dim totalAdjustAmt As Decimal = Convert.ToDecimal(gridrec.Columns("ADJUSTAMT").SummaryItem.SummaryValue)
            Dim totalTDSAmt As Decimal = Convert.ToDecimal(gridrec.Columns("TDS").SummaryItem.SummaryValue)
            Dim billAmt As Decimal = 0
            Decimal.TryParse(TXTBILLAMT.Text, billAmt)

            If totalAdjustAmt > billAmt Then
                MessageBox.Show("Total Adjust Amt cannot be greater than Bill Amt.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            ElseIf totalAdjustAmt < billAmt Then
                MessageBox.Show("Total Adjust Amt Is less than Bill Amt. Remaining Amount will be OnAccount.", "", MessageBoxButtons.OK)
                Dim remamount1 As Decimal = billAmt - totalAdjustAmt
                RemAmount = remamount1
            End If
            If totalTDSAmt > 0 Then

            End If
            Dim totalSelectedAdjustAmt As Decimal = 0
            For i As Integer = 0 To gridrec.RowCount - 1
                If Convert.ToBoolean(gridrec.GetRowCellValue(i, "CHK")) Then
                    Dim amt As Decimal = 0
                    Decimal.TryParse(gridrec.GetRowCellValue(i, "ADJUSTAMT").ToString(), amt)
                    totalSelectedAdjustAmt += amt
                End If
            Next
            DTBILLS.Columns.Add("BILLNO")
            DTBILLS.Columns.Add("REFNO")
            DTBILLS.Columns.Add("BILLDATE")
            DTBILLS.Columns.Add("ADJUSTAMT")
            DTBILLS.Columns.Add(CMBTDSDEDUCTEDAC.Text.Trim)
            DTBILLS.Columns.Add("TDS")


            For i As Integer = 0 To gridrec.RowCount - 1
                Dim dtrow As DataRow = gridrec.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DTBILLS.Rows.Add(dtrow("SRNO"), dtrow("REFNO"), dtrow("DATE"), dtrow("ADJUSTAMT"), CMBTDSDEDUCTEDAC.Text.Trim, dtrow("TDS"))
                End If
            Next
            ' assign your remamount string value here

            ' BILLNO = gridrec.GetFocusedRowCellValue("SRNO")
            ' REFNO = gridrec.GetFocusedRowCellValue("REFNO")
            ' BILLDATE = gridrec.GetFocusedRowCellValue("DATE")
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SelectBills_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.E And e.Alt = True Then
                cmdok_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLCMB()
        If CMBTDSDEDUCTEDAC.Text = "" Then FILLNAME(CMBTDSDEDUCTEDAC, "FALSE", " AND LEDGERS.ACC_TDSAC = 1")
    End Sub
    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim OBJCMN As New ClsCommon
            Dim dt As New DataTable
            If FRMSTRING = "BILLINTEREST" Then
                dt = OBJCMN.SEARCH(" *, CAST (0 AS BIT) AS CHK ", "", " (SELECT OPENINGBILL.BILL_INITIALS AS SRNO, LEDGERS.Acc_cmpname AS NAME, OPENINGBILL.BILL_AMT AS BILLAMT, OPENINGBILL.BILL_BALANCE AS BALAMT, 'OPENING' AS TYPE, OPENINGBILL.BILL_NARRATION AS REFNO, OPENINGBILL.BILL_NO AS BILLNO, OPENINGBILL.BILL_DATE AS DATE, OPENINGBILL.BILL_CMPID AS CMPID, OPENINGBILL.BILL_LOCATIONID AS LOCATIONID, OPENINGBILL.BILL_YEARID AS YEARID, ISNULL(OPENINGBILL.BILL_DISPUTE,0) AS DISPUTE ,0.00 AS ADJUSTAMT,0 AS TDS FROM OPENINGBILL INNER JOIN LEDGERS ON OPENINGBILL.BILL_LEDGERID = LEDGERS.Acc_id  UNION ALL  SELECT INVOICEMASTER.INVOICE_INITIALS AS SRNO, LEDGERS.Acc_cmpname AS NAME, INVOICEMASTER.INVOICE_GRANDTOTAL AS BILLAMT, INVOICEMASTER.INVOICE_BALANCE AS BALAMT, 'INVOICE' AS TYPE, INVOICEMASTER.INVOICE_REFNO AS REFNO, INVOICEMASTER.INVOICE_NO AS BILLNO, INVOICEMASTER.INVOICE_DATE AS DATE, INVOICEMASTER.INVOICE_CMPID AS CMPID, INVOICEMASTER.INVOICE_LOCATIONID AS LOCATIONID, INVOICEMASTER.INVOICE_YEARID AS YEARID, ISNULL(INVOICEMASTER.INVOICE_DISPUTE,0) AS DISPUTE,0.00 AS ADJUSTAMT,0.00 AS TDS FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id  UNION ALL SELECT PURCHASEMASTER.BILL_INITIALS AS SRNO, LEDGERS.Acc_cmpname AS NAME, (CASE WHEN ISNULL(BILL_RCM,0) = 'FALSE' THEN PURCHASEMASTER.BILL_GRANDTOTAL ELSE (CASE WHEN ISNULL(BILL_SCREENTYPE,'LINE GST') = 'LINE GST' THEN BILL_TOTALTAXABLEAMT ELSE BILL_SUBTOTAL END) END)  AS BILLAMT, PURCHASEMASTER.BILL_BALANCE AS BALAMT, 'PURCHASE' AS TYPE, PURCHASEMASTER.BILL_PARTYBILLNO AS REFNO, PURCHASEMASTER.BILL_NO AS BILLNO, PURCHASEMASTER.BILL_PARTYBILLDATE AS DATE, PURCHASEMASTER.BILL_CMPID AS CMPID, PURCHASEMASTER.BILL_LOCATIONID AS LOCATIONID, PURCHASEMASTER.BILL_YEARID AS YEARID, ISNULL(PURCHASEMASTER.BILL_DISPUTE,0) AS DISPUTE ,0.00 AS ADJUSTAMT,0.00 AS TDS FROM PURCHASEMASTER INNER JOIN LEDGERS ON PURCHASEMASTER.BILL_LEDGERID = LEDGERS.Acc_id UNION ALL SELECT NONPURCHASE.NP_INITIALS AS SRNO, LEDGERS.Acc_cmpname AS NAME, (CASE WHEN ISNULL(NP_RCM,0) = 'TRUE' THEN NP_TOTALTAXABLEAMT ELSE NONPURCHASE.NP_GRANDTOTAL END) AS BILLAMT, NONPURCHASE.NP_BALANCE AS BALAMT, 'NONPURCHASE' AS TYPE, NONPURCHASE.NP_REFNO AS REFNO, NONPURCHASE.NP_NO AS BILLNO, NONPURCHASE.NP_PARTYBILLDATE AS DATE, NONPURCHASE.NP_CMPID AS CMPID, NONPURCHASE.NP_LOCATIONID AS LOCATIONID, NONPURCHASE.NP_YEARID AS YEARID, CAST (0 AS BIT) AS DISPUTE ,0.00 AS ADJUSTAMT,0.00 AS TDS FROM NONPURCHASE INNER JOIN LEDGERS ON NONPURCHASE.NP_LEDGERID = LEDGERS.Acc_id  UNION ALL SELECT CREDITNOTEMASTER.CN_initials AS SRNO, LEDGERS.Acc_cmpname AS NAME, (CASE WHEN ISNULL(CN_RCM,0) = 'TRUE' THEN CN_SUBTOTAL ELSE CREDITNOTEMASTER.CN_GTOTAL END) AS BILLAMT, CREDITNOTEMASTER.CN_BALANCE AS BALAMT, 'CREDITNOTE' AS TYPE, CREDITNOTEMASTER.CN_PARTYREFNO AS REFNO, CREDITNOTEMASTER.CN_NO AS BILLNO, CREDITNOTEMASTER.CN_date AS DATE, CREDITNOTEMASTER.CN_CMPID AS CMPID, CREDITNOTEMASTER.CN_LOCATIONID AS LOCATIONID, CREDITNOTEMASTER.CN_YEARID AS YEARID, CAST (0 AS BIT) AS DISPUTE ,0.00 AS ADJUSTAMT,0.00 AS TDS FROM CREDITNOTEMASTER INNER JOIN LEDGERS ON CREDITNOTEMASTER.CN_LEDGERID = LEDGERS.Acc_id WHERE CN_date >= '07/01/2017' UNION ALL SELECT DEBITNOTEMASTER.DN_initials AS SRNO, LEDGERS.Acc_cmpname AS NAME, DEBITNOTEMASTER.DN_GTOTAL AS BILLAMT, DEBITNOTEMASTER.DN_BALANCE AS BALAMT, 'DEBITNOTE' AS TYPE, DEBITNOTEMASTER.DN_initials AS REFNO, DEBITNOTEMASTER.DN_NO AS BILLNO, DEBITNOTEMASTER.DN_date AS DATE, DEBITNOTEMASTER.DN_CMPID AS CMPID, DEBITNOTEMASTER.DN_LOCATIONID AS LOCATIONID, DEBITNOTEMASTER.DN_YEARID AS YEARID, CAST (0 AS BIT) AS DISPUTE ,0.00 AS ADJUSTAMT,0.00 AS TDS FROM DEBITNOTEMASTER INNER JOIN LEDGERS ON DEBITNOTEMASTER.DN_LEDGERID = LEDGERS.Acc_id WHERE DN_date >= '07/01/2017') AS T ", TEMPCONDITION & " AND T.YEARID =" & YearId & " ORDER BY  T.DATE, T.BILLNO ")
            Else
                dt = OBJCMN.SEARCH(" *, CAST (0 AS BIT) AS CHK ", "", " (SELECT AGENCYOPENINGBILL.ABILL_INITIALS AS SRNO, LEDGERS.Acc_cmpname AS NAME, AGENCYOPENINGBILL.ABILL_AMT AS BILLAMT, AGENCYOPENINGBILL.ABILL_BALANCE AS BALAMT, 'OPENING' AS TYPE, AGENCYOPENINGBILL.ABILL_NARRATION AS REFNO, AGENCYOPENINGBILL.ABILL_NO AS BILLNO, AGENCYOPENINGBILL.ABILL_DATE AS DATE, AGENCYOPENINGBILL.ABILL_CMPID AS CMPID, AGENCYOPENINGBILL.ABILL_LOCATIONID AS LOCATIONID, AGENCYOPENINGBILL.ABILL_YEARID AS YEARID, ISNULL(AGENCYOPENINGBILL.ABILL_DISPUTE,0) AS DISPUTE ,0.00 AS ADJUSTAMT,0 AS TDS FROM AGENCYOPENINGBILL INNER JOIN LEDGERS ON AGENCYOPENINGBILL.ABILL_LEDGERID = LEDGERS.Acc_id  WHERE(AGENCYOPENINGBILL.ABILL_BALANCE > 0) UNION  ALL  SELECT AGENCYINVOICEMASTER.AINVOICE_INITIALS AS SRNO, LEDGERS.Acc_cmpname AS NAME, AGENCYINVOICEMASTER.AINVOICE_GRANDTOTAL AS BILLAMT, AGENCYINVOICEMASTER.AINVOICE_BALANCE AS BALAMT, 'INVOICE' AS TYPE, AGENCYINVOICEMASTER.AINVOICE_REFNO AS REFNO, AGENCYINVOICEMASTER.AINVOICE_NO AS BILLNO, AGENCYINVOICEMASTER.AINVOICE_DATE AS DATE, AGENCYINVOICEMASTER.AINVOICE_CMPID AS CMPID, AGENCYINVOICEMASTER.AINVOICE_LOCATIONID AS LOCATIONID, AGENCYINVOICEMASTER.AINVOICE_YEARID AS YEARID, ISNULL(AGENCYINVOICEMASTER.AINVOICE_DISPUTE,0) AS DISPUTE,0.00 AS ADJUSTAMT,0.00 AS TDS FROM AGENCYINVOICEMASTER INNER JOIN LEDGERS ON AGENCYINVOICEMASTER.AINVOICE_LEDGERID = LEDGERS.Acc_id WHERE AGENCYINVOICEMASTER.AINVOICE_BALANCE > 0 UNION ALL SELECT NONPURCHASE.NP_INITIALS AS SRNO, LEDGERS.Acc_cmpname AS NAME, (CASE WHEN ISNULL(NP_RCM,0) = 'TRUE' THEN NP_TOTALTAXABLEAMT ELSE NONPURCHASE.NP_GRANDTOTAL END) AS BILLAMT, NONPURCHASE.NP_BALANCE AS BALAMT, 'NONPURCHASE' AS TYPE, NONPURCHASE.NP_REFNO AS REFNO, NONPURCHASE.NP_NO AS BILLNO, NONPURCHASE.NP_PARTYBILLDATE AS DATE, NONPURCHASE.NP_CMPID AS CMPID, NONPURCHASE.NP_LOCATIONID AS LOCATIONID, NONPURCHASE.NP_YEARID AS YEARID, CAST (0 AS BIT) AS DISPUTE ,0.00 AS ADJUSTAMT,0.00 AS TDS FROM NONPURCHASE INNER JOIN LEDGERS ON NONPURCHASE.NP_LEDGERID = LEDGERS.Acc_id  WHERE NONPURCHASE.NP_BALANCE > 0 UNION ALL SELECT AGENCYCREDITNOTEMASTER.ACN_initials AS SRNO, LEDGERS.Acc_cmpname AS NAME, (CASE WHEN ISNULL(ACN_RCM,0) = 'TRUE' THEN ACN_SUBTOTAL ELSE AGENCYCREDITNOTEMASTER.ACN_GTOTAL END) AS BILLAMT, AGENCYCREDITNOTEMASTER.ACN_BALANCE AS BALAMT, 'CREDITNOTE' AS TYPE, AGENCYCREDITNOTEMASTER.ACN_PARTYREFNO AS REFNO, AGENCYCREDITNOTEMASTER.ACN_NO AS BILLNO, AGENCYCREDITNOTEMASTER.ACN_date AS DATE, AGENCYCREDITNOTEMASTER.ACN_CMPID AS CMPID, AGENCYCREDITNOTEMASTER.ACN_LOCATIONID AS LOCATIONID, AGENCYCREDITNOTEMASTER.ACN_YEARID AS YEARID, CAST (0 AS BIT) AS DISPUTE,0.00 AS ADJUSTAMT ,0.00 AS TDS FROM AGENCYCREDITNOTEMASTER INNER JOIN LEDGERS ON AGENCYCREDITNOTEMASTER.ACN_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN AGENCYCREDITNOTEMASTER_BILLDESC ON AGENCYCREDITNOTEMASTER.ACN_NO = AGENCYCREDITNOTEMASTER_BILLDESC.ACN_NO  WHERE AGENCYCREDITNOTEMASTER.ACN_BALANCE > 0 AND ACN_date >= '07/01/2017'AND ACN_BILLNO = '' AND AGENCYCREDITNOTEMASTER_BILLDESC.ACN_NO IS NULL UNION ALL SELECT AGENCYCREDITNOTEMASTER.ACN_initials AS SRNO, LEDGERS.Acc_cmpname AS NAME, AGENCYCREDITNOTEMASTER_BILLDESC.ACN_AMT AS BILLAMT, AGENCYCREDITNOTEMASTER_BILLDESC.ACN_BALANCE AS BALAMT, 'CREDITNOTE' AS TYPE, AGENCYCREDITNOTEMASTER.ACN_PARTYREFNO AS REFNO, AGENCYCREDITNOTEMASTER.ACN_NO AS BILLNO, AGENCYCREDITNOTEMASTER.ACN_date AS DATE, AGENCYCREDITNOTEMASTER.ACN_CMPID AS CMPID, AGENCYCREDITNOTEMASTER.ACN_LOCATIONID AS LOCATIONID, AGENCYCREDITNOTEMASTER.ACN_YEARID AS YEARID, CAST (0 AS BIT) AS DISPUTE ,0.00 AS ADJUSTAMT,0.00 AS TDS FROM AGENCYCREDITNOTEMASTER INNER JOIN LEDGERS ON AGENCYCREDITNOTEMASTER.ACN_LEDGERID = LEDGERS.Acc_id INNER JOIN AGENCYCREDITNOTEMASTER_BILLDESC ON AGENCYCREDITNOTEMASTER.ACN_NO = AGENCYCREDITNOTEMASTER_BILLDESC.ACN_NO  WHERE AGENCYCREDITNOTEMASTER_BILLDESC.ACN_BALANCE > 0 AND AGENCYCREDITNOTEMASTER_BILLDESC.ACN_PAYTYPE = 'New Ref' UNION ALL SELECT 'SR-' + CAST(AGENCYSALERETURN.ASALRET_NO AS VARCHAR(50)) AS SRNO, LEDGERS.Acc_cmpname AS NAME, AGENCYSALERETURN_BILLDESC.ASALRET_AMT AS BILLAMT, AGENCYSALERETURN_BILLDESC.ASALRET_BALANCE AS BALAMT, 'AGENCYSALERETURN' AS TYPE, AGENCYSALERETURN.ASALRET_PARTYREFNO AS REFNO, AGENCYSALERETURN.ASALRET_NO AS BILLNO, AGENCYSALERETURN.ASALRET_date AS DATE, AGENCYSALERETURN.ASALRET_CMPID AS CMPID, AGENCYSALERETURN.ASALRET_LOCATIONID AS LOCATIONID, AGENCYSALERETURN.ASALRET_YEARID AS YEARID, CAST (0 AS BIT) AS DISPUTE,0.00 AS ADJUSTAMT,0.00 AS TDS FROM AGENCYSALERETURN INNER JOIN LEDGERS ON AGENCYSALERETURN.ASALRET_LEDGERID = LEDGERS.Acc_id INNER JOIN AGENCYSALERETURN_BILLDESC ON AGENCYSALERETURN.ASALRET_NO = AGENCYSALERETURN_BILLDESC.ASALRET_NO AND AGENCYSALERETURN.ASALRET_YEARID = AGENCYSALERETURN_BILLDESC.ASALRET_YEARID WHERE AGENCYSALERETURN_BILLDESC.ASALRET_BALANCE > 0 AND AGENCYSALERETURN_BILLDESC.ASALRET_PAYTYPE = 'New Ref'  ) AS T  ", TEMPCONDITION & " AND T.BALAMT > 0 AND T.YEARID =" & YearId & " ORDER BY T.TYPE, T.BILLNO")
            End If
            griddetails.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SelectBills_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Receipt.SELECTEDBILLNO = ""
            Dim CONDITION As String = ""
            If CMPNAME <> "" Then CONDITION = " AND T.NAME = '" & CMPNAME & "' "
            If TXTBILLAMT.Text = "" Then TXTBILLAMT.Text = AMOUNT
            If FRMSTRING = "BILLINTEREST" Then
                GCHK.Visible = True
                GCHK.VisibleIndex = 0
            End If

            fillgrid(CONDITION)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CHKSELECTALL_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            If griddetails.Visible = True Then
                For i As Integer = 0 To gridrec.RowCount - 1
                    Dim dtrow As DataRow = gridrec.GetDataRow(i)
                    dtrow("CHK") = CHKSELECTALL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub gridrec_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gridrec.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("DISPUTE")) = "Checked" Then
                    e.Appearance.BackColor = Color.LightGreen
                End If
                'If View.GetRowCellDisplayText(e.RowHandle, View.Columns("OUTPCS")) > 0 Or View.GetRowCellDisplayText(e.RowHandle, View.Columns("OUTMTRS")) > 0 Then
                '    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                '    e.Appearance.BackColor = Color.Yellow
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub gridrec_CellValueChanging(sender As Object, e As CellValueChangedEventArgs) Handles gridrec.CellValueChanging
        Try
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            If e.Column.FieldName = "CHK" Then
                ' Only run when CHK column changes
                'Dim chkVal = view.GetRowCellValue(e.RowHandle, "CHK")
                'If chkVal = True Then
                view.PostEditor() ' Commit edit

                ' Get BALAMT and TDS values
                Dim balAmtVal = view.GetRowCellValue(e.RowHandle, "BALAMT")
                Dim tdsVal = view.GetRowCellValue(e.RowHandle, "TDS")

                ' Convert to decimal safely
                Dim balAmt As Decimal = 0D
                Dim tds As Decimal = 0D
                If balAmtVal IsNot DBNull.Value Then Decimal.TryParse(balAmtVal.ToString(), balAmt)
                If tdsVal IsNot DBNull.Value Then Decimal.TryParse(tdsVal.ToString(), tds)

                ' BALAMT + TDS
                Dim totalAmt As Decimal = balAmt + tds
                'If e.Column.FieldName = "CHK" Then
                '    ' Apply logic based on checkbox
                '    If Convert.ToBoolean(e.Value) = True Then
                view.SetRowCellValue(e.RowHandle, "ADJUSTAMT", totalAmt)
                '    Else
                '        view.SetRowCellValue(e.RowHandle, "ADJUSTAMT", 0)
                '    End If
                'End If
                view.RefreshRow(e.RowHandle)
            End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridrec_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles gridrec.CellValueChanged
        Try
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            If e.Column.FieldName = "TDS" Then
                Dim chkVal = view.GetRowCellValue(e.RowHandle, "CHK")
                If chkVal = True Then
                    ' Only run when CHK column changes
                    'Dim chkVal = view.GetRowCellValue(e.RowHandle, "CHK")
                    'If chkVal = True Then
                    view.PostEditor() ' Commit edit

                    ' Get BALAMT and TDS values
                    Dim balAmtVal = view.GetRowCellValue(e.RowHandle, "BALAMT")
                    Dim tdsVal = view.GetRowCellValue(e.RowHandle, "TDS")

                    ' Convert to decimal safely
                    Dim balAmt As Decimal = 0D
                    Dim tds As Decimal = 0D
                    If balAmtVal IsNot DBNull.Value Then Decimal.TryParse(balAmtVal.ToString(), balAmt)
                    If tdsVal IsNot DBNull.Value Then Decimal.TryParse(tdsVal.ToString(), tds)

                    ' BALAMT + TDS
                    Dim totalAmt As Decimal = balAmt + tds
                    'If e.Column.FieldName = "CHK" Then
                    '    ' Apply logic based on checkbox
                    '    If Convert.ToBoolean(e.Value) = True Then
                    view.SetRowCellValue(e.RowHandle, "ADJUSTAMT", totalAmt)
                    '    Else
                    '        view.SetRowCellValue(e.RowHandle, "ADJUSTAMT", 0)
                    '    End If
                    'End If
                    view.RefreshRow(e.RowHandle)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTDSDEDUCTEDAC_Enter(sender As Object, e As EventArgs) Handles CMBTDSDEDUCTEDAC.Enter
        Try
            If CMBTDSDEDUCTEDAC.Text.Trim = "" Then FILLNAME(CMBTDSDEDUCTEDAC, "FALSE", " AND LEDGERS.ACC_TDSAC = 1")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTDSDEDUCTEDAC_Validating(sender As Object, e As CancelEventArgs) Handles CMBTDSDEDUCTEDAC.Validating
        Try
            If CMBTDSDEDUCTEDAC.Text.Trim <> "" Then NAMEVALIDATE(CMBTDSDEDUCTEDAC, CMBTDSDEDUCTEDAC, e, Me, txtadd, " AND LEDGERS.ACC_TDSAC = 1")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class