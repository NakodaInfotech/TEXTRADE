
Imports BL
Imports DevExpress.XtraGrid.Views.Grid
Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.pdf

Public Class InvoiceDetails
    Dim SALEREGID As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public multi As Boolean = False
    Public fromno, tono As Integer
    Public PARTYNAME As String
    Public TEMPINVOICENO, TEMPREGNAME As String
    Dim DTMAIL As New DataTable
    Dim DTWHATSAPP As New DataTable

    Private Sub InvoiceDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

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

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objclsCMST As New ClsCommonMaster
            'Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, INVOICEMASTER.INVOICE_NO AS SRNO, INVOICEMASTER.INVOICE_DATE AS DATE, ISNULL(INVOICEMASTER.INVOICE_REFNO, '') AS REFNO, ISNULL(LEDGER.Acc_cmpname, '') AS NAME, ISNULL(LEDGER.Acc_add, '') AS ADDRESS, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(LEDGERS.Acc_cmpname, '') AS AGENTNAME,  ISNULL(INVOICE_ACKNO, '') AS ACKNO, INVOICE_ACKDATE AS ACKDATE, ISNULL(LEDGER.Acc_email, '') AS PARTYMAIL, ISNULL(LEDGER.ACC_WHATSAPPNO, '') AS PARTYWHATSAPP, ISNULL(LEDGERS.Acc_email, '')  AS AGENTMAIL, ISNULL(LEDGERS.ACC_WHATSAPPNO, '') AS AGENTWHATSAPP, ISNULL(TRANSNAME.Acc_cmpname, '') AS TRANSNAME, ISNULL(INVOICEMASTER.INVOICE_TOTALPCS, 0) AS TOTALPCS, ISNULL(INVOICEMASTER.INVOICE_TOTALMTRS, 0) AS TOTALMTRS,ISNULL(INVOICEMASTER.INVOICE_TOTALDISCAMT, 0) AS TOTALDISC,ISNULL(INVOICEMASTER.INVOICE_TOTALSPDISCAMT, 0) AS TOTALSPDISCAMT ,ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL, INVOICEMASTER.INVOICE_DISPUTE AS DISPUTED, INVOICEMASTER.INVOICE_CHECKED AS CHECKED, INVOICEMASTER.INVOICE_REMARKS AS REMARKS, ISNULL(LEDGER.ACC_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.state_name, '') AS STATENAME, ISNULL(STATEMASTER.state_remark, '') AS STATECODE,ISNULL(INVOICEMASTER.INVOICE_TOTALAMT, 0) AS TOTALAMT , CASE WHEN ISNULL(INVOICEMASTER.INVOICE_SCREENTYPE, 'LINE GST') = 'LINE GST' THEN ISNULL(INVOICEMASTER.INVOICE_TOTALTAXABLEAMT, 0) ELSE ISNULL(INVOICEMASTER.INVOICE_SUBTOTAL, 0) END AS TOTALTAXABLEAMT,ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL ,ISNULL(INVOICEMASTER.INVOICE_RETURN, 0) AS RETURNAMT ,ISNULL(INVOICEMASTER.INVOICE_BALANCE, 0) AS BALANCEAMT ,ISNULL(INVOICEMASTER.INVOICE_AMTREC, 0) + ISNULL(INVOICEMASTER.INVOICE_EXTRAAMT, 0)   AS RECDAMT,  ISNULL(INVOICEMASTER.INVOICE_TOTALCGSTAMT, 0) AS TOTALCGSTAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALSGSTAMT, 0) AS TOTALSGSTAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALIGSTAMT, 0) AS TOTALIGSTAMT, ISNULL(INVOICEMASTER.INVOICE_GDNNO, '') AS CHALLANNO, ISNULL(INVOICEMASTER.INVOICE_GDNDATE, GETDATE()) AS CHALLANDATE,ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS PACKING,ISNULL(INVOICEMASTER.INVOICE_EWAYBILLNO, '') AS EWAYBILLNO, ISNULL(TRANSNAME.ACC_CMPNAME,'') AS TRANSNAME, ISNULL(INVOICE_LRNO,'') AS LRNO,INVOICEMASTER.INVOICE_LRDATE AS LRDATE, ISNULL(INVOICE_BALENOFROM,0) AS NOOFBALES, ISNULL(PURLEDGERS.ACC_CMPNAME,'') AS PURNAME , INVOICEMASTER.INVOICE_PONO AS SONO, ISNULL(INVOICEMASTER.INVOICE_CHARGES, 0) AS CHARGES, RECDATE.RECDATE, ISNULL(LOCALTRANSLEDGERS.ACC_CMPNAME,'') AS LOCALTRANSNAME, ISNULL(INVOICEMASTER.INVOICE_APPLYTCS,0) AS APPLYTCS, ISNULL(INVOICEMASTER.INVOICE_TCSPER,0) AS TCSPER, ISNULL(INVOICEMASTER.INVOICE_TCSAMT,0) AS TCSAMT, ISNULL(INVOICE_IRNNO,'') AS IRNNO, ISNULL(INVOICE_DOCKETNO,'') AS DOCKETNO, ISNULL(INVOICE_DOCKETDATE, GETDATE()) AS DOCKETDATE, ISNULL(INVOICE_COURIER,'') AS COURIER, ISNULL(FROMCITYMASTER.city_name, '') AS FROMCITY, ISNULL(TOCITYMASTER.city_name, '') AS TOCITY, ISNULL(USER_NAME,'') AS USERNAME, ISNULL(INVOICEMASTER.INVOICE_INITIALS,'') AS INITIALS, ISNULL(INVOICEMASTER.INVOICE_PRINTINITIALS,'') AS PRINTINITIALS, INVOICE_CREATED AS CREATEDDATE , ISNULL(STUFF((SELECT ', ' + CHGSLEDGERS.ACC_CMPNAME + '  ' + CAST(INVOICE_PER AS VARCHAR(10)) [text()] FROM INVOICEMASTER_CHGS  AS A INNER JOIN LEDGERS AS CHGSLEDGERS ON A.INVOICE_CHARGESID = CHGSLEDGERS.ACC_ID WHERE A.INVOICE_no = INVOICEMASTER.INVOICE_NO AND A.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND A.INVOICE_yearid = INVOICEMASTER.INVOICE_YEARID  FOR XML PATH(''), TYPE) .value('.','NVARCHAR(MAX)'),1,2,' '),'') AS CHGSDTLS, ISNULL(INVOICEMASTER.INVOICE_PARTYPONO, 0) AS PARTYPONO  ", " ", " INVOICEMASTER INNER JOIN LEDGERS AS LEDGER ON INVOICEMASTER.INVOICE_LEDGERID = LEDGER.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON INVOICEMASTER.INVOICE_PACKINGID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS LOCALTRANSLEDGERS ON INVOICEMASTER.INVOICE_LOCALTRANSID = LOCALTRANSLEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON LEDGER.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS ON INVOICEMASTER.INVOICE_AGENTID = LEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGER.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSNAME ON INVOICEMASTER.INVOICE_TRANSID = TRANSNAME.Acc_id LEFT OUTER JOIN LEDGERS AS PURLEDGERS ON INVOICE_PURLEDGERID = PURLEDGERS.ACC_ID LEFT OUTER JOIN (SELECT INVOICEMASTER.INVOICE_INITIALS, REC.receipt_date AS RECDATE, INVOICE_YEARID FROM INVOICEMASTER  CROSS APPLY (SELECT TOP 1 RECEIPTMASTER.receipt_date FROM RECEIPTMASTER_DESC INNER JOIN RECEIPTMASTER ON RECEIPTMASTER_DESC.receipt_no = RECEIPTMASTER.RECEIPT_NO AND RECEIPTMASTER_DESC.receipt_registerid = RECEIPTMASTER.receipt_registerid AND RECEIPTMASTER_DESC.receipt_yearid = RECEIPTMASTER.receipt_yearid WHERE RECEIPTMASTER.RECEIPT_YEARID = " & YearId & " AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " AND receipt_paytype = 'Against Bill' AND INVOICEMASTER.INVOICE_INITIALS = RECEIPT_BILLINITIALS AND INVOICEMASTER.INVOICE_YEARID = RECEIPTMASTER.receipt_yearid ) AS REC) AS RECDATE  ON INVOICEMASTER.INVOICE_INITIALS = RECDATE.INVOICE_INITIALS AND INVOICEMASTER.INVOICE_YEARID = RECDATE.INVOICE_YEARID LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON INVOICEMASTER.INVOICE_TOCITYID = TOCITYMASTER.city_id LEFT OUTER JOIN CITYMASTER AS FROMCITYMASTER ON INVOICEMASTER.INVOICE_TRANSCITYID = FROMCITYMASTER.city_id LEFT OUTER JOIN USERMASTER ON INVOICE_USERID = USER_ID", " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND (INVOICEMASTER.INVOICE_YEARID = '" & YearId & "') ORDER BY INVOICEMASTER.INVOICE_NO")
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, INVOICEMASTER.INVOICE_NO AS SRNO, INVOICEMASTER.INVOICE_DATE AS DATE, ISNULL(INVOICEMASTER.INVOICE_REFNO, '') AS REFNO, ISNULL(LEDGER.Acc_cmpname, '') AS NAME, ISNULL(LEDGER.Acc_add, '') AS ADDRESS, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(LEDGERS.Acc_cmpname, '') AS AGENTNAME,  ISNULL(INVOICE_ACKNO, '') AS ACKNO, INVOICE_ACKDATE AS ACKDATE, ISNULL(LEDGER.Acc_email, '') AS PARTYMAIL, ISNULL(LEDGER.ACC_WHATSAPPNO, '') AS PARTYWHATSAPP, ISNULL(LEDGERS.Acc_email, '')  AS AGENTMAIL, ISNULL(LEDGERS.ACC_WHATSAPPNO, '') AS AGENTWHATSAPP, ISNULL(TRANSNAME.Acc_cmpname, '') AS TRANSNAME, ISNULL(INVOICEMASTER.INVOICE_TOTALPCS, 0) AS TOTALPCS, ISNULL(INVOICEMASTER.INVOICE_TOTALMTRS, 0) AS TOTALMTRS,ISNULL(INVOICEMASTER.INVOICE_TOTALDISCAMT, 0) AS TOTALDISC,ISNULL(INVOICEMASTER.INVOICE_TOTALSPDISCAMT, 0) AS TOTALSPDISCAMT ,ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL, INVOICEMASTER.INVOICE_DISPUTE AS DISPUTED, INVOICEMASTER.INVOICE_CHECKED AS CHECKED, INVOICEMASTER.INVOICE_REMARKS AS REMARKS, ISNULL(LEDGER.ACC_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.state_name, '') AS STATENAME, ISNULL(STATEMASTER.state_remark, '') AS STATECODE,ISNULL(INVOICEMASTER.INVOICE_TOTALAMT, 0) AS TOTALAMT , CASE WHEN ISNULL(INVOICEMASTER.INVOICE_SCREENTYPE, 'LINE GST') = 'LINE GST' THEN ISNULL(INVOICEMASTER.INVOICE_TOTALTAXABLEAMT, 0) ELSE ISNULL(INVOICEMASTER.INVOICE_SUBTOTAL, 0) END AS TOTALTAXABLEAMT,ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL ,ISNULL(INVOICEMASTER.INVOICE_RETURN, 0) AS RETURNAMT ,ISNULL(INVOICEMASTER.INVOICE_BALANCE, 0) AS BALANCEAMT ,ISNULL(INVOICEMASTER.INVOICE_AMTREC, 0) + ISNULL(INVOICEMASTER.INVOICE_EXTRAAMT, 0)   AS RECDAMT,  ISNULL(INVOICEMASTER.INVOICE_TOTALCGSTAMT, 0) AS TOTALCGSTAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALSGSTAMT, 0) AS TOTALSGSTAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALIGSTAMT, 0) AS TOTALIGSTAMT, ISNULL(INVOICEMASTER.INVOICE_GDNNO, '') AS CHALLANNO, ISNULL(INVOICEMASTER.INVOICE_GDNDATE, GETDATE()) AS CHALLANDATE,ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS PACKING,ISNULL(INVOICEMASTER.INVOICE_EWAYBILLNO, '') AS EWAYBILLNO, ISNULL(TRANSNAME.ACC_CMPNAME,'') AS TRANSNAME, ISNULL(INVOICE_LRNO,'') AS LRNO,INVOICEMASTER.INVOICE_LRDATE AS LRDATE, ISNULL(INVOICE_BALENOFROM,0) AS NOOFBALES, ISNULL(PURLEDGERS.ACC_CMPNAME,'') AS PURNAME , INVOICEMASTER.INVOICE_PONO AS SONO, ISNULL(INVOICEMASTER.INVOICE_CHARGES, 0) AS CHARGES, RECDATE.RECDATE, ISNULL(LOCALTRANSLEDGERS.ACC_CMPNAME,'') AS LOCALTRANSNAME, ISNULL(INVOICEMASTER.INVOICE_APPLYTCS,0) AS APPLYTCS, ISNULL(INVOICEMASTER.INVOICE_TCSPER,0) AS TCSPER, ISNULL(INVOICEMASTER.INVOICE_TCSAMT,0) AS TCSAMT, ISNULL(INVOICE_IRNNO,'') AS IRNNO, ISNULL(INVOICE_DOCKETNO,'') AS DOCKETNO, ISNULL(INVOICE_DOCKETDATE, GETDATE()) AS DOCKETDATE, ISNULL(INVOICE_COURIER,'') AS COURIER, ISNULL(FROMCITYMASTER.city_name, '') AS FROMCITY, ISNULL(TOCITYMASTER.city_name, '') AS TOCITY, ISNULL(USER_NAME,'') AS USERNAME, ISNULL(INVOICEMASTER.INVOICE_INITIALS,'') AS INITIALS, ISNULL(INVOICEMASTER.INVOICE_PRINTINITIALS,'') AS PRINTINITIALS, INVOICE_CREATED AS CREATEDDATE, ISNULL(COSTCENTERMASTER.COSTCENTER_NAME,'') AS COSTCENTERNAME, ISNULL(STUFF((SELECT ', ' + CHGSLEDGERS.ACC_CMPNAME + '  ' + CAST(INVOICE_PER AS VARCHAR(10)) [text()] FROM INVOICEMASTER_CHGS  AS A INNER JOIN LEDGERS AS CHGSLEDGERS ON A.INVOICE_CHARGESID = CHGSLEDGERS.ACC_ID WHERE A.INVOICE_no = INVOICEMASTER.INVOICE_NO AND A.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND A.INVOICE_yearid = INVOICEMASTER.INVOICE_YEARID  FOR XML PATH(''), TYPE) .value('.','NVARCHAR(MAX)'),1,2,' '),'') AS CHGSDTLS, ISNULL(INVOICEMASTER.INVOICE_PARTYPONO, 0) AS PARTYPONO, ISNULL((SELECT TOP 1 INVOICE_PER FROM INVOICEMASTER_CHGS  AS A INNER JOIN LEDGERS AS CHGSLEDGERS ON A.INVOICE_CHARGESID = CHGSLEDGERS.ACC_ID WHERE A.INVOICE_no = INVOICEMASTER.INVOICE_NO AND A.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND A.INVOICE_yearid = INVOICEMASTER.INVOICE_YEARID AND (CHGSLEDGERS.ACC_CMPNAME = 'BROKERAGE' OR CHGSLEDGERS.ACC_CMPNAME = 'SPECIAL DISCOUNT')),0) AS BROKERAGEPER, ISNULL((SELECT SUM(INVOICE_AMT) AS BAMT FROM INVOICEMASTER_CHGS  AS A INNER JOIN LEDGERS AS CHGSLEDGERS ON A.INVOICE_CHARGESID = CHGSLEDGERS.ACC_ID WHERE A.INVOICE_no = INVOICEMASTER.INVOICE_NO AND A.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND A.INVOICE_yearid = INVOICEMASTER.INVOICE_YEARID AND (CHGSLEDGERS.ACC_CMPNAME = 'BROKERAGE' OR CHGSLEDGERS.ACC_CMPNAME = 'SPECIAL DISCOUNT')),0) AS BROKERAGE,  ISNULL((SELECT TOP 1 INVOICE_PER FROM INVOICEMASTER_CHGS  AS A INNER JOIN LEDGERS AS CHGSLEDGERS ON A.INVOICE_CHARGESID = CHGSLEDGERS.ACC_ID WHERE A.INVOICE_no = INVOICEMASTER.INVOICE_NO AND A.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND A.INVOICE_yearid = INVOICEMASTER.INVOICE_YEARID AND CHGSLEDGERS.ACC_CMPNAME = 'DISCOUNT GIVEN'),0) AS DISCOUNTPER,  ISNULL((SELECT SUM(INVOICE_AMT) AS DAMT FROM INVOICEMASTER_CHGS  AS A INNER JOIN LEDGERS AS CHGSLEDGERS ON A.INVOICE_CHARGESID = CHGSLEDGERS.ACC_ID WHERE A.INVOICE_no = INVOICEMASTER.INVOICE_NO AND A.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND A.INVOICE_yearid = INVOICEMASTER.INVOICE_YEARID AND CHGSLEDGERS.ACC_CMPNAME = 'DISCOUNT GIVEN'),0) AS DISCOUNT,  ISNULL((SELECT TOP 1 INVOICE_PER AS FLOAT FROM INVOICEMASTER_CHGS  AS A INNER JOIN LEDGERS AS CHGSLEDGERS ON A.INVOICE_CHARGESID = CHGSLEDGERS.ACC_ID WHERE A.INVOICE_no = INVOICEMASTER.INVOICE_NO AND A.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND A.INVOICE_yearid = INVOICEMASTER.INVOICE_YEARID AND CHGSLEDGERS.ACC_CMPNAME = 'CASH DISCOUNT'),0) AS CDPER,  ISNULL((SELECT SUM(INVOICE_AMT) AS CDAMT FROM INVOICEMASTER_CHGS  AS A INNER JOIN LEDGERS AS CHGSLEDGERS ON A.INVOICE_CHARGESID = CHGSLEDGERS.ACC_ID WHERE A.INVOICE_no = INVOICEMASTER.INVOICE_NO AND A.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND A.INVOICE_yearid = INVOICEMASTER.INVOICE_YEARID AND CHGSLEDGERS.ACC_CMPNAME = 'CASH DISCOUNT'),0) AS CASHDISC ,ISNULL(INVOICEMASTER.INVOICE_SPECIALREMARKS,'') AS SPECIALREMARK ,ISNULL(USERMASTER.User_Name, '') AS CREATEDBY , ISNULL(REFERREDBYLEDGERS.Acc_cmpname, '') AS REFERREDBY , UPPER(DATENAME(MONTH,INVOICE_DATE)) AS [MONTHNAME] ,ISNULL(INVOICEMASTER.INVOICE_TRADINGACC, 0) AS TRADINGACC ", "", " INVOICEMASTER INNER JOIN LEDGERS AS LEDGER ON INVOICEMASTER.INVOICE_LEDGERID = LEDGER.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON INVOICEMASTER.INVOICE_PACKINGID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS LOCALTRANSLEDGERS ON INVOICEMASTER.INVOICE_LOCALTRANSID = LOCALTRANSLEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON LEDGER.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS ON INVOICEMASTER.INVOICE_AGENTID = LEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGER.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSNAME ON INVOICEMASTER.INVOICE_TRANSID = TRANSNAME.Acc_id LEFT OUTER JOIN LEDGERS AS PURLEDGERS ON INVOICE_PURLEDGERID = PURLEDGERS.ACC_ID LEFT OUTER JOIN (SELECT INVOICEMASTER.INVOICE_INITIALS, REC.receipt_date AS RECDATE, INVOICE_YEARID FROM INVOICEMASTER  CROSS APPLY (SELECT TOP 1 RECEIPTMASTER.receipt_date FROM RECEIPTMASTER_DESC INNER JOIN RECEIPTMASTER ON RECEIPTMASTER_DESC.receipt_no = RECEIPTMASTER.RECEIPT_NO AND RECEIPTMASTER_DESC.receipt_registerid = RECEIPTMASTER.receipt_registerid AND RECEIPTMASTER_DESC.receipt_yearid = RECEIPTMASTER.receipt_yearid WHERE RECEIPTMASTER.RECEIPT_YEARID = " & YearId & " AND INVOICEMASTER.INVOICE_YEARID =" & YearId & " AND receipt_paytype = 'Against Bill' AND INVOICEMASTER.INVOICE_INITIALS = RECEIPT_BILLINITIALS AND INVOICEMASTER.INVOICE_YEARID = RECEIPTMASTER.receipt_yearid ) AS REC) AS RECDATE  ON INVOICEMASTER.INVOICE_INITIALS = RECDATE.INVOICE_INITIALS AND INVOICEMASTER.INVOICE_YEARID = RECDATE.INVOICE_YEARID LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON INVOICEMASTER.INVOICE_TOCITYID = TOCITYMASTER.city_id LEFT OUTER JOIN CITYMASTER AS FROMCITYMASTER ON INVOICEMASTER.INVOICE_TRANSCITYID = FROMCITYMASTER.city_id LEFT OUTER JOIN USERMASTER ON INVOICE_USERID = USER_ID LEFT OUTER JOIN COSTCENTERMASTER ON INVOICEMASTER.INVOICE_COSTCENTERNAMEID = COSTCENTERMASTER.COSTCENTER_ID LEFT OUTER JOIN LEDGERS AS REFERREDBYLEDGERS ON INVOICEMASTER.INVOICE_REFERREDBYID = REFERREDBYLEDGERS.Acc_id ", " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND (INVOICEMASTER.INVOICE_YEARID = '" & YearId & "') ORDER BY INVOICEMASTER.INVOICE_NO")
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
                Dim OBJBILL As New InvoiceMaster
                OBJBILL.MdiParent = MDIMain
                OBJBILL.EDIT = editval
                OBJBILL.TEMPINVOICENO = billno
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
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'SALE'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If cmbregister.Text.Trim.Length > 0 Then
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    SALEREGID = dt.Rows(0).Item(0)
                    fillgrid(" and INVOICEMASTER.INVOICE_yearid = " & YearId & " AND INVOICEMASTER.INVOICE_registerid = " & SALEREGID & " order by dbo.INVOICEMASTER.INVOICE_no ")
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridINVOICE_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub


            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Invoice Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Wish to Print Invoice from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPDIRECT()
                End If
            Else
                If MsgBox("Wish to Print Selected Invoice ?", MsgBoxStyle.YesNo) = vbYes Then
                    CMDOK.Focus()
                    SERVERPROPSELECTED()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub SERVERPROPDIRECT(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList

            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                Dim OBJINVOICE As New saledesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.DIRECTPRINT = True
                OBJINVOICE.FRMSTRING = "INVOICE"
                OBJINVOICE.DIRECTMAIL = INVOICEMAIL
                OBJINVOICE.DIRECTWHATSAPP = WHATSAPP

                OBJINVOICE.INVOICECOPYNAME = TOOLCMBINVCOPY.Text.Trim
                If (ClientName = "SOFTAS" Or ClientName = "MANS") And TOOLCMBINVCOPY.Text = "OFFICE COPY" Then OBJINVOICE.INVOICECOPYNAME = "AGENT COPY"
                If (ClientName = "RMANILAL" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "TARUN" Or ClientName = "SHANTI" Or ClientName = "KUNAL" Or ClientName = "VALIANT") And TOOLCMBINVCOPY.Text = "DUPLICATE COPY" Then OBJINVOICE.INVOICECOPYNAME = "AGENT COPY"
                If ClientName = "ALENCOT" And TOOLCMBINVCOPY.Text = "DUPLICATE COPY" Then OBJINVOICE.INVOICECOPYNAME = "REVISED COPY"
                If ClientName = "GELATO" And TOOLCMBINVCOPY.Text = "TRANSPORT COPY" Then OBJINVOICE.INVOICECOPYNAME = "DUPLICATE FOR TRANSPORT"

                If TOOLCMBINVCOPY.Text = "TRANSPORT COPY" Then OBJINVOICE.INVOICETRANS = True
                If TOOLCMBINVCOPY.Text = "RETAIL COPY (A5)" Then OBJINVOICE.INVOICERETAIL = True
                If TOOLCMBINVCOPY.Text = "YARN DO" Then OBJINVOICE.FRMSTRING = "YARNDO"


                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(STATE_REMARK,'') AS STATECODE", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID ", " AND INVOICEMASTER.INVOICE_NO = " & Val(I) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STATECODE") <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                OBJINVOICE.registername = cmbregister.Text.Trim
                OBJINVOICE.PRINTSETTING = PRINTDIALOG
                OBJINVOICE.INVNO = Val(I)
                OBJINVOICE.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJINVOICE.BLANKPAPER = CHKBLANKPAPER.Checked
                OBJINVOICE.Show()
                OBJINVOICE.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\INVOICE_" & I & ".pdf")
                FILENAME.Add("INVOICE_" & I & ".pdf")
                DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_SENDWHATSAPP = 1 FROM InvoiceMaster INNER JOIN REGISTERMASTER On INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id WHERE INVOICE_NO = " & I & " AND REGISTER_NAME '" & cmbregister.Text.Trim & "'  AND INVOICE_YEARID = " & YearId, "", "")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Invoice"
                OBJMAIL.ShowDialog()
            End If



            If WHATSAPP = True Then
                Dim OBJWHATSAPP As New SendWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPSELECTED(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            DTMAIL.Rows.Clear()
            DTWHATSAPP.Rows.Clear()


            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            'Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(gridbill.RowCount - 1)
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                If ROW("CHK") = True Then
                    Dim OBJINVOICE As New saledesign
                    OBJINVOICE.MdiParent = MDIMain
                    OBJINVOICE.DIRECTPRINT = True
                    OBJINVOICE.FRMSTRING = "INVOICE"
                    OBJINVOICE.DIRECTMAIL = INVOICEMAIL
                    OBJINVOICE.DIRECTWHATSAPP = WHATSAPP
                    If TOOLCMBINVCOPY.Text = "TRANSPORT COPY" Then OBJINVOICE.INVOICETRANS = True
                    If TOOLCMBINVCOPY.Text = "RETAIL COPY (A5)" Then OBJINVOICE.INVOICERETAIL = True
                    If TOOLCMBINVCOPY.Text = "YARN DO" Then OBJINVOICE.FRMSTRING = "YARNDO"

                    OBJINVOICE.INVOICECOPYNAME = TOOLCMBINVCOPY.Text.Trim
                    If (ClientName = "SOFTAS" Or ClientName = "MANS") And TOOLCMBINVCOPY.Text = "OFFICE COPY" Then OBJINVOICE.INVOICECOPYNAME = "AGENT COPY"
                    If (ClientName = "RMANILAL" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "TARUN" Or ClientName = "SHANTI" Or ClientName = "KUNAL" Or ClientName = "VALIANT") And TOOLCMBINVCOPY.Text = "DUPLICATE COPY" Then OBJINVOICE.INVOICECOPYNAME = "AGENT COPY"
                    If ClientName = "ALENCOT" And TOOLCMBINVCOPY.Text = "DUPLICATE COPY" Then OBJINVOICE.INVOICECOPYNAME = "REVISED COPY"
                    If ClientName = "GELATO" And TOOLCMBINVCOPY.Text = "TRANSPORT COPY" Then OBJINVOICE.INVOICECOPYNAME = "DUPLICATE FOR TRANSPORT"

                    OBJINVOICE.PARTYNAME = ROW("NAME")
                    OBJINVOICE.AGENTNAME = ROW("AGENTNAME")
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STATE_REMARK,'') AS STATECODE, ISNULL(REGISTERMASTER.REGISTER_ID,0) AS REGID", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID ", " AND INVOICEMASTER.INVOICE_NO = " & Val(ROW("SRNO")) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                    If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STATECODE") <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                    OBJINVOICE.registername = cmbregister.Text.Trim
                    OBJINVOICE.PRINTSETTING = PRINTDIALOG
                    OBJINVOICE.INVNO = Val(ROW("SRNO"))
                    OBJINVOICE.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJINVOICE.BLANKPAPER = CHKBLANKPAPER.Checked
                    OBJINVOICE.Show()
                    OBJINVOICE.Close()

                    If TOOLCMBINVCOPY.Text = "YARN DO" Then
                        ALATTACHMENT.Add(Application.StartupPath & "\" & ROW("NAME") & "YARNDO_" & Val(ROW("SRNO")) & ".pdf")
                        FILENAME.Add(ROW("NAME") & "YARNDO_" & Val(ROW("SRNO")) & ".pdf")
                    Else
                        ALATTACHMENT.Add(Application.StartupPath & "\" & ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf")
                        FILENAME.Add(ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf")
                    End If


                    'ADDINT IN DTEMAIL
                    DTMAIL.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("REGID"), cmbregister.Text.Trim, ROW("PRINTINITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYMAIL"), ROW("AGENTNAME"), ROW("AGENTMAIL"), Val(ROW("GRANDTOTAL")), UCase(CmpName) & " - Invoice No. " & ROW("PRINTINITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf")

                    'ADDING IN DTWHATSAPP
                    If ClientName = "MAHAVIRPOLYCOT" Then ROW("AGENTWHATSAPP") = ""
                    DTWHATSAPP.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("REGID"), cmbregister.Text.Trim, ROW("PRINTINITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("AGENTNAME"), ROW("AGENTWHATSAPP"), Val(ROW("GRANDTOTAL")), UCase(CmpName) & " - Invoice No. " & ROW("PRINTINITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf")

                    DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_SENDWHATSAPP = 1 FROM InvoiceMaster INNER JOIN REGISTERMASTER On INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id WHERE INVOICE_NO = " & Val(ROW("SRNO")) & " AND REGISTER_NAME '" & cmbregister.Text.Trim & "'  AND INVOICE_YEARID = " & YearId, "", "")

                End If
            Next

            If INVOICEMAIL Then
                If DTMAIL.Rows.Count = 0 Then Exit Sub
                Dim OBJEMAIL As New SendMultipleMail
                OBJEMAIL.FORMTYPE = "INVOICE"
                OBJEMAIL.DT = DTMAIL
                OBJEMAIL.ShowDialog()
                Exit Sub
            End If

            'If INVOICEMAIL Then
            '    Dim OBJMAIL As New SendMail
            '    OBJMAIL.ALATTACHMENT = ALATTACHMENT
            '    OBJMAIL.subject = "Invoice"
            '    OBJMAIL.ShowDialog()
            'End If

            If WHATSAPP = True Then
                If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
                Dim OBJWHATSAPP As New SendMultipleWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.DT = DTWHATSAPP
                OBJWHATSAPP.ShowDialog()
            End If


            'FOR MERGING MULTIPLE PDF
            'Dim pdfReaderList As List(Of PdfReader) = New List(Of PdfReader)()
            'For i As Integer = 0 To ALATTACHMENT.Count - 1
            '    Dim pdfReader As PdfReader = New PdfReader(ALATTACHMENT(i).ToString)
            '    pdfReaderList.Add(pdfReader)
            'Next

            'Dim document As Document = New Document(PageSize.A4, 0, 0, 0, 0)
            'Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream("D:  \OutPut.pdf", FileMode.Create))
            'document.Open()
            'For Each reader As PdfReader In pdfReaderList
            '    For i As Integer = 1 To reader.NumberOfPages
            '        Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)
            '        document.Add(iTextSharp.text.Image.GetInstance(page))
            '    Next
            'Next
            'document.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InvoiceDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE INVOICE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            DTMAIL.Columns.Add("INVNO")
            DTMAIL.Columns.Add("REGID")
            DTMAIL.Columns.Add("REGNAME")
            DTMAIL.Columns.Add("PRINTINITIALS")
            DTMAIL.Columns.Add("DATE")
            DTMAIL.Columns.Add("NAME")
            DTMAIL.Columns.Add("PARTYEMAILID")
            DTMAIL.Columns.Add("AGENTNAME")
            DTMAIL.Columns.Add("AGENTEMAILID")
            DTMAIL.Columns.Add("GRANDTOTAL")
            DTMAIL.Columns.Add("SUBJECT")
            DTMAIL.Columns.Add("ATTACHMENT")
            DTMAIL.Columns.Add("FILENAME")

            DTWHATSAPP.Columns.Add("INVNO")
            DTWHATSAPP.Columns.Add("REGID")
            DTWHATSAPP.Columns.Add("REGNAME")
            DTWHATSAPP.Columns.Add("PRINTINITIALS")
            DTWHATSAPP.Columns.Add("DATE")
            DTWHATSAPP.Columns.Add("NAME")
            DTWHATSAPP.Columns.Add("PARTYWHATSAPP")
            DTWHATSAPP.Columns.Add("AGENTNAME")
            DTWHATSAPP.Columns.Add("AGENTWHATSAPP")
            DTWHATSAPP.Columns.Add("GRANDTOTAL")
            DTWHATSAPP.Columns.Add("SUBJECT")
            DTWHATSAPP.Columns.Add("ATTACHMENT")
            DTWHATSAPP.Columns.Add("FILENAME")


            fillregister(cmbregister, " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
            ' TOOLCMBINVCOPY.SelectedIndex = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("CHECKED")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightGreen
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("DISPUTED")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKMULTI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKMULTI.CheckedChanged
        Try
            If CHKMULTI.Checked = True Then
                multi = True
                TXTFROM.Focus()
            Else
                multi = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDETAILS.Click
        Try
            Dim OBJINV As New InvoiceGridDetails
            OBJINV.MdiParent = MDIMain
            OBJINV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Invoice Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Invoice from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Invoice ?", MsgBoxStyle.YesNo) = vbYes Then
                    CMDOK.Focus()
                    SERVERPROPSELECTED(False, True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTFROM.Validated
        If TXTFROM.Text.Trim <> "" Then TXTTO.Focus()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Sale Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Sale Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sale Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Invoice Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TOOLMAIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLMAIL.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Invoice Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail Invoice from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(True)
                End If
            Else
                If MsgBox("Wish to Mail Selected Invoice ?", MsgBoxStyle.YesNo) = vbYes Then
                    CMDOK.Focus()
                    SERVERPROPSELECTED(True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InvoiceDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "INDRAPUJAIMPEX" Or ClientName = "SOFTAS" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "PARTOBA" Then CHKBLANKPAPER.CheckState = CheckState.Checked
            If ClientName = "KDFAB" Then
                GRECDDATE.Visible = True
                GRECDDATE.VisibleIndex = GRECDAMT.VisibleIndex + 1
            End If

            If ClientName = "VALIANT" Then
                GEWAYBILLNO.VisibleIndex = gname.VisibleIndex + 1
                GACKNO.VisibleIndex = GEWAYBILLNO.VisibleIndex + 1
            End If

            If ClientName = "SAKARIA" Or ClientName = "NVAHAN" Then
                GSUPPLIERNAME.Visible = True
                GSUPPLIERNAME.VisibleIndex = 11
            End If
            If ClientName = "GELATO" Then
                GTRANSPORT.VisibleIndex = 8
                GREMARKS.VisibleIndex = 9
            End If


            TOOLCMBINVCOPY.Items.Add("CUSTOMER COPY")
            TOOLCMBINVCOPY.Items.Add("TRANSPORT COPY")
            If ClientName = "MASHOK" Then TOOLCMBINVCOPY.Items.Add("YARN DO") Else TOOLCMBINVCOPY.Items.Add("OFFICE COPY")
            If ClientName = "MSANCHITKUMAR" Then TOOLCMBINVCOPY.Items.Add("AGENT COPY") Else TOOLCMBINVCOPY.Items.Add("DUPLICATE COPY")
            TOOLCMBINVCOPY.Items.Add("RETAIL COPY (A5)")


            If ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                GFROMCITY.VisibleIndex = GCITY.VisibleIndex + 1
                GTOCITY.VisibleIndex = GFROMCITY.VisibleIndex + 1
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class