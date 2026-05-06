
Imports System.IO
Imports BL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class payment_advice

    Public payno As Integer
    Public payname As String
    Public REGNAME As String
    Public FRMSTRING As String
    Public LEDGERSNAME As String
    Public NEFTRTGSNORMAL As String = "PARTY"
    Public FROMNO, TONO As Integer
    Public WHERECLAUSE As String = ""
    Public PERIOD As String = ""
    Public SHOWNARR As Integer = 0


    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1


    Dim OBJPAY As New Paymentreport
    Dim OBJPAY_A5 As New PaymentreportA5

    Dim OBJPAY_ABHEE As New Paymentreport_ABHEE

    Dim OBJPAY_OLD As New Paymentreport_OLD
    Dim OBJPAY_SUPEEMA As New Paymentreport_SUPEEMA

    Dim OBJPAYMONTHLY As New PayMonthlyReport
    Dim OBJPAYPARTYSUMM As New PayPartySummReport
    Dim OBJPAYREG As New PaymentRegisterReport

    Dim OBJCHQPAY As New ChqPayment
    Dim OBJCHQPAY_BOB As New ChqPayment_BOB
    Dim OBJCHQPAY_UNION As New ChqPayment_UNION
    Dim OBJCHQPAY_INDUS As New ChqPayment_INDUS
    Dim OBJCHQPAY_KOTAK As New ChqPayment_KOTAK
    Dim OBJCHQPAY_DENA As New ChqPayment_DENA
    Dim OBJCHQPAY_PNB As New ChqPayment_PNB
    Dim OBJCHQPAY_CORP As New ChqPayment_CORPORATION
    Dim OBJCHQPAY_HDFC As New ChqPayment_HDFC
    Dim OBJCHQPAY_CITIBANK As New ChqPayment_CITIBANK
    Dim OBJCHQPAY_TCOT As New ChqPaymentHDFC_TCOT
    Dim OBJCHQPAY_IDBI As New ChqPayment_IDBI
    Dim OBJCHQPAY_SYNDICATE As New ChqPayment_SYNDICATE
    Dim OBJCHQPAY_CANARA As New ChqPayment_Canara
    Dim OBJCHQPAY_ICICI As New ChqPayment_ICICI
    Dim OBJCHQPAY_STANDARD As New ChqPayment_STANDARDCHAR
    Dim OBJCHQPAY_MAHESH As New ChqPayment_MAHESH
    Dim OBJCHQPAY_COSMOS As New ChqPayment_COSMOS
    Dim OBJCHQPAY_CITYUNION As New ChqPayment_CITYUNION
    Dim OBJCHQPAY_HDFCKOTAKABHEE As New ChqPayment_HDFCKOTAK_ABHEE

    Dim OBJCHQPAYBACK As New ChqPaymentBackReport


    Dim OBJENVELOPE As New EnvelopeReport
    Dim OBJENVELOPE_SMALL As New EnvelopeReport_SMALL
    Dim OBJACCTREE As New AccountsTree

    Private Sub payment_advice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control = True And e.KeyCode = Keys.P Then
            CRPO.PrintReport()
        ElseIf e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub payment_advice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strsearch As String
        strsearch = ""

        Try
            If DIRECTPRINT = True Then
                If FRMSTRING = "CHQPRINT" Then
                    PRINTDIRECTLYTOPRINTER()
                ElseIf FRMSTRING = "CHQPRINTBACK" Then
                    PRINTCHQBACKDIRECTLYTOPRINTER()
                Else
                    PRINTDIRECTADVICE()
                End If
                Exit Sub
            End If


            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table


            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            If FRMSTRING = "CHQPRINT" Then
                If BANKFORCHQPRINT = "DENA" Then
                    crTables = OBJCHQPAY_DENA.Database.Tables
                ElseIf BANKFORCHQPRINT = "PNB" Then
                    crTables = OBJCHQPAY_PNB.Database.Tables
                ElseIf BANKFORCHQPRINT = "HDFC" Then
                    If ClientName = "ABHEE" Then crTables = OBJCHQPAY_HDFCKOTAKABHEE.Database.Tables Else crTables = OBJCHQPAY_HDFC.Database.Tables
                ElseIf BANKFORCHQPRINT = "CITIBANK" Then
                    crTables = OBJCHQPAY_CITIBANK.Database.Tables
                ElseIf BANKFORCHQPRINT = "UNION" Then
                    crTables = OBJCHQPAY_UNION.Database.Tables
                ElseIf BANKFORCHQPRINT = "KOTAK" Then
                    crTables = OBJCHQPAY_KOTAK.Database.Tables
                ElseIf BANKFORCHQPRINT = "SYNDICATE" Then
                    crTables = OBJCHQPAY_SYNDICATE.Database.Tables
                ElseIf BANKFORCHQPRINT = "IDBI" Then
                    crTables = OBJCHQPAY_IDBI.Database.Tables
                ElseIf BANKFORCHQPRINT = "CANARA" Then
                    crTables = OBJCHQPAY_CANARA.Database.Tables
                ElseIf BANKFORCHQPRINT = "ICICI" Then
                    crTables = OBJCHQPAY_ICICI.Database.Tables
                ElseIf BANKFORCHQPRINT = "STANDARD" Then
                    crTables = OBJCHQPAY_STANDARD.Database.Tables
                ElseIf BANKFORCHQPRINT = "MAHESH" Then
                    crTables = OBJCHQPAY_MAHESH.Database.Tables
                ElseIf BANKFORCHQPRINT = "BOB" Then
                    crTables = OBJCHQPAY_BOB.Database.Tables
                ElseIf BANKFORCHQPRINT = "INDUS" Then
                    crTables = OBJCHQPAY_INDUS.Database.Tables
                ElseIf BANKFORCHQPRINT = "COSMOS" Then
                    crTables = OBJCHQPAY_COSMOS.Database.Tables
                ElseIf BANKFORCHQPRINT = "CITYUNION" Then
                    crTables = OBJCHQPAY_CITYUNION.Database.Tables
                Else
                    crTables = OBJCHQPAY.Database.Tables
                End If

            ElseIf FRMSTRING = "CHQPRINTBACK" Then
                crTables = OBJCHQPAYBACK.Database.Tables

            ElseIf FRMSTRING = "PAYMONTHLY" Then
                crTables = OBJPAYMONTHLY.Database.Tables
            ElseIf FRMSTRING = "PAYPARTYSUMM" Then
                crTables = OBJPAYPARTYSUMM.Database.Tables
            ElseIf FRMSTRING = "PAYREGISTER" Then
                crTables = OBJPAYREG.Database.Tables

            ElseIf FRMSTRING = "ACCOUNTSTREE" Then
                crTables = OBJACCTREE.Database.Tables
            ElseIf FRMSTRING = "ENVELOPE" Then
                If ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Then
                    crTables = OBJENVELOPE_SMALL.Database.Tables
                Else
                    crTables = OBJENVELOPE.Database.Tables
                End If
            Else
                If ClientName = "SUPEEMA" Then
                    crTables = OBJPAY_SUPEEMA.Database.Tables
                ElseIf ClientName = "VALIANT" Then
                    crTables = OBJPAY_A5.Database.Tables
                ElseIf ClientName = "ABHEE" Then
                    crTables = OBJPAY_ABHEE.Database.Tables
                Else
                    crTables = OBJPAY.Database.Tables
                    If ClientName = "CHINTAN" Or ClientName = "MILUXE" Then OBJPAY.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                End If
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next
            '************* END *******************

            If FRMSTRING = "CHQPRINT" Then
                strsearch = strsearch & "  {PAYMENTMASTER.PAYMENT_NO}= " & payno & " and {REGISTERMASTER.REGISTER_NAME} = '" & REGNAME & "' and {PAYMENTMASTER.PAYMENT_CMPID} = " & CmpId & " and {PAYMENTMASTER.PAYMENT_LOCATIONID} = " & Locationid & " and {PAYMENTMASTER.PAYMENT_YEARID} = " & YearId
                CRPO.SelectionFormula = strsearch


                If BANKFORCHQPRINT = "DENA" Then
                    CRPO.ReportSource = OBJCHQPAY_DENA
                    OBJCHQPAY_DENA.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "PNB" Then
                    CRPO.ReportSource = OBJCHQPAY_PNB
                    OBJCHQPAY_PNB.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "HDFC" Then
                    If ClientName = "ABHEE" Then
                        CRPO.ReportSource = OBJCHQPAY_HDFCKOTAKABHEE
                        OBJCHQPAY_HDFCKOTAKABHEE.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                    Else
                        CRPO.ReportSource = OBJCHQPAY_HDFC
                        OBJCHQPAY_HDFC.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                    End If
                ElseIf BANKFORCHQPRINT = "CITIBANK" Then
                    CRPO.ReportSource = OBJCHQPAY_CITIBANK
                    OBJCHQPAY_CITIBANK.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "UNION" Then
                    CRPO.ReportSource = OBJCHQPAY_UNION
                    OBJCHQPAY_UNION.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "KOTAK" Then
                    CRPO.ReportSource = OBJCHQPAY_KOTAK
                    OBJCHQPAY_KOTAK.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "SYNDICATE" Then
                    CRPO.ReportSource = OBJCHQPAY_SYNDICATE
                    OBJCHQPAY_SYNDICATE.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "IDBI" Then
                    CRPO.ReportSource = OBJCHQPAY_IDBI
                    OBJCHQPAY_IDBI.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "CANARA" Then
                    CRPO.ReportSource = OBJCHQPAY_CANARA
                    OBJCHQPAY_CANARA.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "ICICI" Then
                    CRPO.ReportSource = OBJCHQPAY_ICICI
                    OBJCHQPAY_ICICI.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "STANDARD" Then
                    CRPO.ReportSource = OBJCHQPAY_STANDARD
                    OBJCHQPAY_STANDARD.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "MAHESH" Then
                    CRPO.ReportSource = OBJCHQPAY_MAHESH
                    OBJCHQPAY_MAHESH.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "BOB" Then
                    CRPO.ReportSource = OBJCHQPAY_BOB
                    OBJCHQPAY_BOB.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "INDUS" Then
                    CRPO.ReportSource = OBJCHQPAY_INDUS
                    OBJCHQPAY_INDUS.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "COSMOS" Then
                    CRPO.ReportSource = OBJCHQPAY_COSMOS
                    OBJCHQPAY_COSMOS.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                ElseIf BANKFORCHQPRINT = "CITYUNION" Then
                    CRPO.ReportSource = OBJCHQPAY_CITYUNION
                    OBJCHQPAY_CITYUNION.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                Else
                    CRPO.ReportSource = OBJCHQPAY
                    OBJCHQPAY.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
                End If

            ElseIf FRMSTRING = "CHQPRINTBACK" Then
                strsearch = strsearch & "  {PAYMENT_REPORT.PAYMENTNO}= " & payno & " and {PAYMENT_REPORT.REGNAME} = '" & REGNAME & "' and {PAYMENT_REPORT.YEARID} = " & YearId
                CRPO.SelectionFormula = strsearch
                CRPO.ReportSource = OBJCHQPAYBACK


            ElseIf FRMSTRING = "PAYMONTHLY" Then
                CRPO.SelectionFormula = WHERECLAUSE
                CRPO.ReportSource = OBJPAYMONTHLY
                OBJPAYMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "PAYPARTYSUMM" Then
                CRPO.SelectionFormula = WHERECLAUSE
                CRPO.ReportSource = OBJPAYPARTYSUMM
                OBJPAYPARTYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "PAYREGISTER" Then
                CRPO.SelectionFormula = WHERECLAUSE
                CRPO.ReportSource = OBJPAYREG
                OBJPAYREG.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJPAYREG.DataDefinition.FormulaFields("SHOWNARR").Text = SHOWNARR
                OBJPAYREG.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"


            ElseIf FRMSTRING = "ACCOUNTSTREE" Then
                CRPO.SelectionFormula = WHERECLAUSE
                CRPO.ReportSource = OBJACCTREE

            ElseIf FRMSTRING = "ENVELOPE" Then
                CRPO.SelectionFormula = WHERECLAUSE
                If ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Then
                    CRPO.ReportSource = OBJENVELOPE_SMALL
                Else
                    If ClientName = "MVIKASKUMAR" Or ClientName = "RATAN" Or ClientName = "AXIS" Then OBJENVELOPE.DataDefinition.FormulaFields("SHOWOURADD").Text = 1
                    CRPO.ReportSource = OBJENVELOPE
                End If

            Else
                strsearch = strsearch & "  {PAYMENT_REPORT.PAYMENTNO}= " & payno & " AND {PAYMENT_REPORT.REGNAME}= '" & REGNAME & "' and {LEDGERS.Acc_cmpname} = '" & payname & "' and {PAYMENT_REPORT.CMPID} = " & CmpId & " and {PAYMENT_REPORT.LOCATIONID} = " & Locationid & " and {PAYMENT_REPORT.YEARID} = " & YearId
                CRPO.SelectionFormula = strsearch
                If ClientName = "SUPEEMA" Then
                    'ADD DATA IN TEMPPAYMENTDETAILS
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPPAYMENTDETAILS WHERE YEARID = " & YearId, "", "")
                    DT = OBJCMN.Execute_Any_String("INSERT INTO TEMPPAYMENTDETAILS SELECT PAYMENTMASTER.PAYMENT_NO, PAYMENT_DATE, LEDGERS.Acc_cmpname, ACCLEDGERS.Acc_cmpname, PAYMENT_CHQNO, PAYMENTMASTER_DESC.PAYMENT_amt, PAYMENTMASTER.PAYMENT_cmpid, PAYMENTMASTER.PAYMENT_yearid,'','', '' AS RECREMARKS  FROM PAYMENTMASTER_DESC INNER JOIN PAYMENTMASTER ON PAYMENTMASTER_DESC.PAYMENT_no =PAYMENTMASTER.PAYMENT_no AND PAYMENTMASTER_DESC.PAYMENT_registerid =PAYMENTMASTER.PAYMENT_registerid AND PAYMENTMASTER_DESC.PAYMENT_yearid =PAYMENTMASTER.PAYMENT_yearid INNER JOIN LEDGERS ON PAYMENTMASTER.PAYMENT_ledgerid = LEDGERS.ACC_ID INNER JOIN LEDGERS AS ACCLEDGERS ON PAYMENTMASTER.PAYMENT_accid = ACCLEDGERS.ACC_ID WHERE PAYMENTMASTER.PAYMENT_YEARID =" & YearId & " AND PAYMENT_BILLINITIALS In (Select PAYMENT_BILLINITIALS FROM PAYMENTMASTER_DESC INNER JOIN REGISTERMASTER ON PAYMENTMASTER_DESC.PAYMENT_REGISTERID = REGISTERMASTER.REGISTER_ID WHERE PAYMENTMASTER_DESC.PAYMENT_NO = " & Val(payno) & " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "' And PAYMENTMASTER_DESC.PAYMENT_yearid = " & YearId & ")", "", "")

                    CRPO.ReportSource = OBJPAY_SUPEEMA
                ElseIf ClientName = "VALIANT" Then
                    CRPO.ReportSource = OBJPAY_A5
                ElseIf ClientName = "ABHEE" Then

                    'ADD DATA IN TEMPPAYMENTDETAILS
                    Dim TEMPOLDYEARID As Integer = 0
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPPAYMENTDETAILS WHERE YEARID = " & YearId, "", "")

                    'GET LAST YEARID
                    DT = OBJCMN.SEARCH(" TOP 1 YEAR_ID AS OLDYEARID ", "", " YEARMASTER ", " AND year_cmpid = " & CmpId & " AND year_startdate < '" & AccFrom.Date & "' ORDER BY year_startdate DESC")
                    If DT.Rows.Count > 0 Then TEMPOLDYEARID = Val(DT.Rows(0).Item("OLDYEARID"))

                    'OGQUERY, THIS IS TAKING TIME SO TRIED TO OPTIMIZE
                    'DT = OBJCMN.Execute_Any_String("INSERT INTO TEMPPAYMENTDETAILS SELECT RECNO,  RECDATE, NAME, RECREMARKS, CHQNO, RECAMT, CMPID, YEARID, RECTYPE, RECINITIALS, RECREMARKS FROM PAYMENTMASTER INNER JOIN PAYMENTMASTER_DESC ON PAYMENTMASTER.PAYMENT_no= PAYMENTMASTER_DESC.PAYMENT_NO AND PAYMENTMASTER.PAYMENT_registerid= PAYMENTMASTER_DESC.PAYMENT_registerid AND PAYMENTMASTER.PAYMENT_yearid= PAYMENTMASTER_DESC.PAYMENT_YEARID INNER JOIN REGISTERMASTER ON PAYMENTMASTER.PAYMENT_REGISTERID = REGISTERMASTER.REGISTER_ID INNER JOIN OUTSTANDINGREPORT_DETAILS ON PAYMENTMASTER_DESC.PAYMENT_YEARID = OUTSTANDINGREPORT_DETAILS.YEARID AND PAYMENTMASTER.PAYMENT_LEDGERID = OUTSTANDINGREPORT_DETAILS.LEDGERID AND PAYMENTMASTER_DESC.PAYMENT_BILLINITIALS = OUTSTANDINGREPORT_DETAILS.BILLINITIALS AND OUTSTANDINGREPORT_DETAILS.RECINITIALS <> PAYMENTMASTER.PAYMENT_initials LEFT OUTER JOIN JOURNALMASTER ON  OUTSTANDINGREPORT_DETAILS.RECINITIALS = JOURNALMASTER.journal_initials AND JOURNALMASTER.journal_yearid = OUTSTANDINGREPORT_DETAILS.YEARID AND journal_ledgerid <> OUTSTANDINGREPORT_DETAILS.LEDGERID AND journal_credit = OUTSTANDINGREPORT_DETAILS.RECAMT LEFT OUTER JOIN LEDGERS AS JVLEDGERS ON JOURNALMASTER.journal_ledgerid = JVLEDGERS.ACC_ID WHERE (JVLEDGERS.Acc_cmpname IS NULL OR JVLEDGERS.ACC_TDSAC = 'FALSE' ) AND PAYMENTMASTER_DESC.PAYMENT_NO = " & Val(payno) & " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "' And PAYMENTMASTER_DESC.PAYMENT_yearid = " & YearId, "", "")
                    DT = OBJCMN.Execute_Any_String("INSERT INTO TEMPPAYMENTDETAILS SELECT RECNO,  RECDATE, NAME, RECREMARKS, CHQNO, RECAMT, CMPID, YEARID, RECTYPE, RECINITIALS, RECREMARKS FROM PAYMENTMASTER INNER JOIN PAYMENTMASTER_DESC ON PAYMENTMASTER.PAYMENT_no= PAYMENTMASTER_DESC.PAYMENT_NO AND PAYMENTMASTER.PAYMENT_registerid= PAYMENTMASTER_DESC.PAYMENT_registerid AND PAYMENTMASTER.PAYMENT_yearid= PAYMENTMASTER_DESC.PAYMENT_YEARID INNER JOIN REGISTERMASTER ON PAYMENTMASTER.PAYMENT_REGISTERID = REGISTERMASTER.REGISTER_ID INNER JOIN OUTSTANDINGREPORT_DETAILS ON PAYMENTMASTER_DESC.PAYMENT_YEARID = OUTSTANDINGREPORT_DETAILS.YEARID AND PAYMENTMASTER.PAYMENT_LEDGERID = OUTSTANDINGREPORT_DETAILS.LEDGERID AND PAYMENTMASTER_DESC.PAYMENT_BILLINITIALS = OUTSTANDINGREPORT_DETAILS.BILLINITIALS AND OUTSTANDINGREPORT_DETAILS.RECNO <> PAYMENTMASTER.PAYMENT_NO LEFT OUTER JOIN JOURNALMASTER ON  OUTSTANDINGREPORT_DETAILS.RECINITIALS = JOURNALMASTER.journal_initials AND JOURNALMASTER.journal_yearid = OUTSTANDINGREPORT_DETAILS.YEARID AND journal_ledgerid <> OUTSTANDINGREPORT_DETAILS.LEDGERID AND journal_credit = OUTSTANDINGREPORT_DETAILS.RECAMT LEFT OUTER JOIN LEDGERS AS JVLEDGERS ON JOURNALMASTER.journal_ledgerid = JVLEDGERS.ACC_ID WHERE (JVLEDGERS.Acc_cmpname IS NULL OR JVLEDGERS.ACC_TDSAC = 'FALSE' ) AND PAYMENTMASTER_DESC.PAYMENT_NO = " & Val(payno) & " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "' And PAYMENTMASTER_DESC.PAYMENT_yearid = " & YearId, "", "")

                    'EXECUTE THIS QUERY ONLY IF THE ENTRY CONTAINS OLD YEAR INVOICES
                    Dim DTCHECK As DataTable = OBJCMN.SEARCH("PAYMENT_NO", "", " PAYMENTMASTER_DESC INNER JOIN REGISTERMASTER ON PAYMENT_registerid = REGISTER_ID ", " AND PAYMENTMASTER_DESC.PAYMENT_NO = " & Val(payno) & " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "' And PAYMENTMASTER_DESC.PAYMENT_yearid = " & YearId & " AND CHARINDEX('/', PAYMENTMASTER_DESC.PAYMENT_BILLINITIALS) > 0 ")
                    If DTCHECK.Rows.Count > 0 Then DT = OBJCMN.Execute_Any_String("INSERT INTO TEMPPAYMENTDETAILS SELECT RECNO,  RECDATE, NAME, RECREMARKS, CHQNO, RECAMT, CMPID, " & YearId & ", RECTYPE, RECINITIALS, RECREMARKS FROM PAYMENTMASTER INNER JOIN PAYMENTMASTER_DESC ON PAYMENTMASTER.PAYMENT_no= PAYMENTMASTER_DESC.PAYMENT_NO AND PAYMENTMASTER.PAYMENT_registerid= PAYMENTMASTER_DESC.PAYMENT_registerid AND PAYMENTMASTER.PAYMENT_yearid= PAYMENTMASTER_DESC.PAYMENT_YEARID INNER JOIN REGISTERMASTER ON PAYMENTMASTER.PAYMENT_REGISTERID = REGISTERMASTER.REGISTER_ID INNER JOIN LEDGERS AS PARTYLEDGERS ON PAYMENTMASTER.PAYMENT_ledgerid = PARTYLEDGERS.ACC_ID INNER JOIN OUTSTANDINGREPORT_DETAILS ON OUTSTANDINGREPORT_DETAILS.YEARID = " & TEMPOLDYEARID & " AND OUTSTANDINGREPORT_DETAILS.NAME = PARTYLEDGERS.ACC_CMPNAME AND LEFT(PAYMENTMASTER_DESC.PAYMENT_BILLINITIALS, CHARINDEX('/', PAYMENTMASTER_DESC.PAYMENT_BILLINITIALS) - 1) = OUTSTANDINGREPORT_DETAILS.BILLINITIALS AND OUTSTANDINGREPORT_DETAILS.RECNO <> PAYMENTMASTER.PAYMENT_NO LEFT OUTER JOIN JOURNALMASTER ON  OUTSTANDINGREPORT_DETAILS.RECINITIALS = JOURNALMASTER.journal_initials AND JOURNALMASTER.journal_yearid = " & TEMPOLDYEARID & " AND journal_ledgerid <> OUTSTANDINGREPORT_DETAILS.LEDGERID AND journal_credit = OUTSTANDINGREPORT_DETAILS.RECAMT LEFT OUTER JOIN LEDGERS AS JVLEDGERS ON JOURNALMASTER.journal_ledgerid = JVLEDGERS.ACC_ID WHERE CHARINDEX('/',PAYMENTMASTER_DESC.PAYMENT_BILLINITIALS) > 0 AND RECNO IS NOT NULL AND (JVLEDGERS.Acc_cmpname IS NULL OR JVLEDGERS.ACC_TDSAC = 'FALSE' ) AND PAYMENTMASTER_DESC.PAYMENT_NO = " & Val(payno) & " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "' And PAYMENTMASTER_DESC.PAYMENT_yearid = " & YearId, "", "")

                    CRPO.ReportSource = OBJPAY_ABHEE
                    Else
                        CRPO.ReportSource = OBJPAY
                End If
            End If


            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch Exp As LoadSaveReportException
            MsgBox("Incorrect path For loading report.",
                    MsgBoxStyle.Critical, "Load Report Error")
        Catch Exp As Exception
            MsgBox(Exp.Message, MsgBoxStyle.Critical, "General Error")

        End Try
    End Sub

    Sub PRINTDIRECTLYTOPRINTER()

        For I As Integer = FROMNO To TONO

            Dim OBJ As Object
            If BANKFORCHQPRINT = "DENA" Then
                OBJ = New ChqPayment_DENA
            ElseIf BANKFORCHQPRINT = "PNB" Then
                OBJ = New ChqPayment_PNB
            ElseIf BANKFORCHQPRINT = "HDFC" Then
                If ClientName = "ABHEE" Then OBJ = New ChqPayment_HDFCKOTAK_ABHEE Else OBJ = New ChqPayment_HDFC
            ElseIf BANKFORCHQPRINT = "INDUS" Then
                OBJ = New ChqPayment_INDUS
            ElseIf BANKFORCHQPRINT = "CITIBANK" Then
                OBJ = New ChqPayment_CITIBANK
            ElseIf BANKFORCHQPRINT = "UNION" Then
                OBJ = New ChqPayment_UNION
            ElseIf BANKFORCHQPRINT = "KOTAK" Then
                OBJ = New ChqPayment_KOTAK
            ElseIf BANKFORCHQPRINT = "SYNDICATE" Then
                OBJ = New ChqPayment_SYNDICATE
            ElseIf BANKFORCHQPRINT = "IDBI" Then
                OBJ = New ChqPayment_IDBI
            ElseIf BANKFORCHQPRINT = "CANARA" Then
                OBJ = New ChqPayment_Canara
            ElseIf BANKFORCHQPRINT = "ICICI" Then
                OBJ = New ChqPayment_ICICI
            ElseIf BANKFORCHQPRINT = "STANDARD" Then
                OBJ = New ChqPayment_STANDARDCHAR
            ElseIf BANKFORCHQPRINT = "MAHESH" Then
                OBJ = New ChqPayment_MAHESH
            ElseIf BANKFORCHQPRINT = "BOB" Then
                OBJ = New ChqPayment_BOB
            ElseIf BANKFORCHQPRINT = "COSMOS" Then
                OBJ = New ChqPayment_COSMOS
            ElseIf BANKFORCHQPRINT = "CITYUNION" Then
                OBJ = New ChqPayment_CITYUNION
            Else
                OBJ = New ChqPayment
            End If


            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table

            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            crTables = OBJ.Database.Tables
            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.DataDefinition.FormulaFields("NEFTRTGSPARTY").Text = "'" & NEFTRTGSNORMAL & "'"
            OBJ.RecordSelectionFormula = " {PAYMENTMASTER.PAYMENT_NO}= " & I & " And {REGISTERMASTER.REGISTER_NAME} = '" & REGNAME & "' and {PAYMENTMASTER.PAYMENT_YEARID} = " & YearId
            OBJ.PrintToPrinter(1, True, 0, 0)


            OBJ.CLOSE()
            OBJ.DISPOSE()

        Next
    End Sub

    Sub PRINTCHQBACKDIRECTLYTOPRINTER()

        For I As Integer = FROMNO To TONO

            Dim OBJ As Object = New ChqPaymentBackReport

            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table

            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            crTables = OBJ.Database.Tables
            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = " {PAYMENT_REPORT.PAYMENTNO}= " & I & " And {PAYMENT_REPORT.REGNAME} = '" & REGNAME & "' and {PAYMENT_REPORT.YEARID} = " & YearId
            OBJ.PrintToPrinter(1, True, 0, 0)

            OBJ.CLOSE()
            OBJ.DISPOSE()
        Next
    End Sub

    Sub PRINTDIRECTADVICE()
        Try
            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table


            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With
            If FRMSTRING = "ENVELOPE" Then
                'CRPO.SelectionFormula = WHERECLAUSE

                ''CRPO.ReportSource = OBJENVELOPE
                'Dim OBJ As New Object

                'OBJ = New EnvelopeReport
                'crTables = OBJ.Database.Tables

                'For Each crTable In crTables
                '    crtableLogonInfo = crTable.LogOnInfo
                '    crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                '    crTable.ApplyLogOnInfo(crtableLogonInfo)
                'Next
                strsearch = WHERECLAUSE

            Else
                strsearch = "  {PAYMENT_REPORT.PAYMENTNO}= " & payno & " AND {PAYMENT_REPORT.REGNAME}= '" & REGNAME & "' and {PAYMENT_REPORT.YEARID} = " & YearId
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            CRPO.SelectionFormula = strsearch

            Dim OBJ As New Object
            If ClientName = "SUPEEMA" Then

                'ADD DATA IN TEMPPAYMENTDETAILS
                DT = OBJCMN.Execute_Any_String("DELETE FROM TEMPPAYMENTDETAILS WHERE YEARID = " & YearId, "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO TEMPPAYMENTDETAILS SELECT PAYMENTMASTER.PAYMENT_NO, PAYMENT_DATE, LEDGERS.Acc_cmpname, ACCLEDGERS.Acc_cmpname, PAYMENT_CHQNO, PAYMENTMASTER_DESC.PAYMENT_amt, PAYMENTMASTER.PAYMENT_cmpid, PAYMENTMASTER.PAYMENT_yearid  FROM PAYMENTMASTER_DESC INNER JOIN PAYMENTMASTER ON PAYMENTMASTER_DESC.PAYMENT_no =PAYMENTMASTER.PAYMENT_no AND PAYMENTMASTER_DESC.PAYMENT_registerid =PAYMENTMASTER.PAYMENT_registerid AND PAYMENTMASTER_DESC.PAYMENT_yearid =PAYMENTMASTER.PAYMENT_yearid INNER JOIN LEDGERS ON PAYMENTMASTER.PAYMENT_ledgerid = LEDGERS.ACC_ID INNER JOIN LEDGERS AS ACCLEDGERS ON PAYMENTMASTER.PAYMENT_accid = ACCLEDGERS.ACC_ID WHERE PAYMENTMASTER.PAYMENT_YEARID =" & YearId & " AND PAYMENT_BILLINITIALS In (Select PAYMENT_BILLINITIALS FROM PAYMENTMASTER_DESC INNER JOIN REGISTERMASTER ON PAYMENTMASTER_DESC.PAYMENT_REGISTERID = REGISTERMASTER.REGISTER_ID WHERE PAYMENTMASTER_DESC.PAYMENT_NO = " & Val(payno) & " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "' And PAYMENTMASTER_DESC.PAYMENT_yearid = " & YearId & ")", "", "")


                OBJ = New Paymentreport_SUPEEMA
            ElseIf ClientName = "VALIANT" Then
                OBJ = New PaymentreportA5
            ElseIf ClientName = "ABHEE" Then

                'ADD DATA IN TEMPPAYMENTDETAILS
                Dim TEMPOLDYEARID As Integer = 0
                DT = OBJCMN.Execute_Any_String("DELETE FROM TEMPPAYMENTDETAILS WHERE YEARID = " & YearId, "", "")

                'GET LAST YEARID
                DT = OBJCMN.SEARCH(" TOP 1 YEAR_ID AS OLDYEARID ", "", " YEARMASTER ", " AND year_cmpid = " & CmpId & " AND year_startdate < '" & AccFrom.Date & "' ORDER BY year_startdate DESC")
                If DT.Rows.Count > 0 Then TEMPOLDYEARID = Val(DT.Rows(0).Item("OLDYEARID"))

                'OGQUERY, THIS IS TAKING TIME SO TRIED TO OPTIMIZE
                'DT = OBJCMN.Execute_Any_String("INSERT INTO TEMPPAYMENTDETAILS SELECT RECNO,  RECDATE, NAME, RECREMARKS, CHQNO, RECAMT, CMPID, YEARID, RECTYPE, RECINITIALS, RECREMARKS FROM PAYMENTMASTER INNER JOIN PAYMENTMASTER_DESC ON PAYMENTMASTER.PAYMENT_no= PAYMENTMASTER_DESC.PAYMENT_NO AND PAYMENTMASTER.PAYMENT_registerid= PAYMENTMASTER_DESC.PAYMENT_registerid AND PAYMENTMASTER.PAYMENT_yearid= PAYMENTMASTER_DESC.PAYMENT_YEARID INNER JOIN REGISTERMASTER ON PAYMENTMASTER.PAYMENT_REGISTERID = REGISTERMASTER.REGISTER_ID INNER JOIN OUTSTANDINGREPORT_DETAILS ON PAYMENTMASTER_DESC.PAYMENT_YEARID = OUTSTANDINGREPORT_DETAILS.YEARID AND PAYMENTMASTER.PAYMENT_LEDGERID = OUTSTANDINGREPORT_DETAILS.LEDGERID AND PAYMENTMASTER_DESC.PAYMENT_BILLINITIALS = OUTSTANDINGREPORT_DETAILS.BILLINITIALS AND OUTSTANDINGREPORT_DETAILS.RECINITIALS <> PAYMENTMASTER.PAYMENT_initials LEFT OUTER JOIN JOURNALMASTER ON  OUTSTANDINGREPORT_DETAILS.RECINITIALS = JOURNALMASTER.journal_initials AND JOURNALMASTER.journal_yearid = OUTSTANDINGREPORT_DETAILS.YEARID AND journal_ledgerid <> OUTSTANDINGREPORT_DETAILS.LEDGERID AND journal_credit = OUTSTANDINGREPORT_DETAILS.RECAMT LEFT OUTER JOIN LEDGERS AS JVLEDGERS ON JOURNALMASTER.journal_ledgerid = JVLEDGERS.ACC_ID WHERE (JVLEDGERS.Acc_cmpname IS NULL OR JVLEDGERS.ACC_TDSAC = 'FALSE' ) AND PAYMENTMASTER_DESC.PAYMENT_NO = " & Val(payno) & " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "' And PAYMENTMASTER_DESC.PAYMENT_yearid = " & YearId, "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO TEMPPAYMENTDETAILS SELECT RECNO,  RECDATE, NAME, RECREMARKS, CHQNO, RECAMT, CMPID, YEARID, RECTYPE, RECINITIALS, RECREMARKS FROM PAYMENTMASTER INNER JOIN PAYMENTMASTER_DESC ON PAYMENTMASTER.PAYMENT_no= PAYMENTMASTER_DESC.PAYMENT_NO AND PAYMENTMASTER.PAYMENT_registerid= PAYMENTMASTER_DESC.PAYMENT_registerid AND PAYMENTMASTER.PAYMENT_yearid= PAYMENTMASTER_DESC.PAYMENT_YEARID INNER JOIN REGISTERMASTER ON PAYMENTMASTER.PAYMENT_REGISTERID = REGISTERMASTER.REGISTER_ID INNER JOIN OUTSTANDINGREPORT_DETAILS ON PAYMENTMASTER_DESC.PAYMENT_YEARID = OUTSTANDINGREPORT_DETAILS.YEARID AND PAYMENTMASTER.PAYMENT_LEDGERID = OUTSTANDINGREPORT_DETAILS.LEDGERID AND PAYMENTMASTER_DESC.PAYMENT_BILLINITIALS = OUTSTANDINGREPORT_DETAILS.BILLINITIALS AND OUTSTANDINGREPORT_DETAILS.RECNO <> PAYMENTMASTER.PAYMENT_NO LEFT OUTER JOIN JOURNALMASTER ON  OUTSTANDINGREPORT_DETAILS.RECINITIALS = JOURNALMASTER.journal_initials AND JOURNALMASTER.journal_yearid = OUTSTANDINGREPORT_DETAILS.YEARID AND journal_ledgerid <> OUTSTANDINGREPORT_DETAILS.LEDGERID AND journal_credit = OUTSTANDINGREPORT_DETAILS.RECAMT LEFT OUTER JOIN LEDGERS AS JVLEDGERS ON JOURNALMASTER.journal_ledgerid = JVLEDGERS.ACC_ID WHERE (JVLEDGERS.Acc_cmpname IS NULL OR JVLEDGERS.ACC_TDSAC = 'FALSE' ) AND PAYMENTMASTER_DESC.PAYMENT_NO = " & Val(payno) & " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "' And PAYMENTMASTER_DESC.PAYMENT_yearid = " & YearId, "", "")

                'EXECUTE THIS QUERY ONLY IF THE ENTRY CONTAINS OLD YEAR INVOICES
                Dim DTCHECK As DataTable = OBJCMN.SEARCH("PAYMENT_NO", "", " PAYMENTMASTER_DESC INNER JOIN REGISTERMASTER ON PAYMENT_registerid = REGISTER_ID ", " AND PAYMENTMASTER_DESC.PAYMENT_NO = " & Val(payno) & " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "' And PAYMENTMASTER_DESC.PAYMENT_yearid = " & YearId & " AND CHARINDEX('/', PAYMENTMASTER_DESC.PAYMENT_BILLINITIALS) > 0 ")
                If DTCHECK.Rows.Count > 0 Then DT = OBJCMN.Execute_Any_String("INSERT INTO TEMPPAYMENTDETAILS SELECT RECNO,  RECDATE, NAME, RECREMARKS, CHQNO, RECAMT, CMPID, " & YearId & ", RECTYPE, RECINITIALS, RECREMARKS FROM PAYMENTMASTER INNER JOIN PAYMENTMASTER_DESC ON PAYMENTMASTER.PAYMENT_no= PAYMENTMASTER_DESC.PAYMENT_NO AND PAYMENTMASTER.PAYMENT_registerid= PAYMENTMASTER_DESC.PAYMENT_registerid AND PAYMENTMASTER.PAYMENT_yearid= PAYMENTMASTER_DESC.PAYMENT_YEARID INNER JOIN REGISTERMASTER ON PAYMENTMASTER.PAYMENT_REGISTERID = REGISTERMASTER.REGISTER_ID INNER JOIN LEDGERS AS PARTYLEDGERS ON PAYMENTMASTER.PAYMENT_ledgerid = PARTYLEDGERS.ACC_ID INNER JOIN OUTSTANDINGREPORT_DETAILS ON OUTSTANDINGREPORT_DETAILS.YEARID = " & TEMPOLDYEARID & " AND OUTSTANDINGREPORT_DETAILS.NAME = PARTYLEDGERS.ACC_CMPNAME AND LEFT(PAYMENTMASTER_DESC.PAYMENT_BILLINITIALS, CHARINDEX('/', PAYMENTMASTER_DESC.PAYMENT_BILLINITIALS) - 1) = OUTSTANDINGREPORT_DETAILS.BILLINITIALS AND OUTSTANDINGREPORT_DETAILS.RECNO <> PAYMENTMASTER.PAYMENT_NO LEFT OUTER JOIN JOURNALMASTER ON  OUTSTANDINGREPORT_DETAILS.RECINITIALS = JOURNALMASTER.journal_initials AND JOURNALMASTER.journal_yearid = " & TEMPOLDYEARID & " AND journal_ledgerid <> OUTSTANDINGREPORT_DETAILS.LEDGERID AND journal_credit = OUTSTANDINGREPORT_DETAILS.RECAMT LEFT OUTER JOIN LEDGERS AS JVLEDGERS ON JOURNALMASTER.journal_ledgerid = JVLEDGERS.ACC_ID WHERE CHARINDEX('/',PAYMENTMASTER_DESC.PAYMENT_BILLINITIALS) > 0 AND RECNO IS NOT NULL AND (JVLEDGERS.Acc_cmpname IS NULL OR JVLEDGERS.ACC_TDSAC = 'FALSE' ) AND PAYMENTMASTER_DESC.PAYMENT_NO = " & Val(payno) & " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "' And PAYMENTMASTER_DESC.PAYMENT_yearid = " & YearId, "", "")



                OBJ = New Paymentreport_ABHEE
            ElseIf ClientName = "MAHAVIRPOLYCOT" And FRMSTRING = "ENVELOPE" Then
                OBJ = New EnvelopeReport
            Else
                OBJ = New Paymentreport
                If ClientName = "CHINTAN" Or ClientName = "MILUXE" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
            End If

            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = strsearch


            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions
                oDfDopt.DiskFileName = Application.StartupPath & "\" & LEDGERSNAME & "PAYMENT_" & payno & ".pdf"

                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                expo = OBJ.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
            End If

            OBJ.CLOSE()
            OBJ.DISPOSE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()
        Dim tempattachment As String

        Dim objmail As New SendMail

        tempattachment = "PAYMENTREPORT"
        objmail.subject = "Payment Voucher"

        If payname <> "" Then
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable = objclscommon.search(" acc_email ", "", " LEDGERS ", " and ACC_cmpname='" & payname & "' and ACC_cmpid=" & CmpId & " and ACC_LOCATIONid=" & Locationid & " and ACC_YEARid=" & YearId)
            If dt.Rows.Count > 0 Then
                emailid = dt.Rows(0).Item(0).ToString
            End If
        End If

        Try
            'Dim objmail As New SendMail
            objmail.attachment = tempattachment
            objmail.attachment = Application.StartupPath & "\" & tempattachment & ".PDF"
            If emailid <> "" Then
                objmail.cmbfirstadd.Text = emailid
            End If
            objmail.Show()
            objmail.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
        Windows.Forms.Cursor.Current = Cursors.Arrow
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\PAYMENTREPORT.PDF")
            OBJWHATSAPP.FILENAME.Add("PAYMENTREPORT.pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions

            oDfDopt.DiskFileName = Application.StartupPath & "\PAYMENTREPORT.PDF"
            If ClientName = "SUPEEMA" Then
                expo = OBJPAY_SUPEEMA.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJPAY_SUPEEMA.Export()

            ElseIf ClientName = "VALIANT" Then
                expo = OBJPAY_A5.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJPAY_A5.Export()

            ElseIf ClientName = "ABHEE" Then
                expo = OBJPAY_ABHEE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJPAY_ABHEE.Export()
            Else
                expo = OBJPAY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJPAY.Export()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class