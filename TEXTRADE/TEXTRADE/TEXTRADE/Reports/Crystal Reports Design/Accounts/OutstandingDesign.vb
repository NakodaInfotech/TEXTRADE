
Imports BL
Imports DB
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports System.Data.SqlClient

Public Class OutstandingDesign

    Dim RPTOUTSTANDINGALLSUMMREC As New OutstandingReport_All_Summary_Rec
    Dim RPTOUTSTANDINGALLSUMMPAY As New OutstandingReport_All_Summary_Pay
    Dim RPTOUTSTANDINGALLDTLS As New OutstandingReport_All_Details

    Dim RPTOUTSTANDINGPAYSUMM As New OutstandingReport_Summary_Pay
    Dim RPTOUTSTANDINGRECSUMM As New OutstandingReport_Summary_Rec
    Dim RPTOUTSTANDINGPAYDTLS As New OutstandingReport_Details_Pay
    Dim RPTOUTSTANDINGRECDTLS As New OutstandingReport_Details_Rec

    Dim RPTBROKEROUTSTANDINGPAYSUMM As New OutstandingReport_Broker_Summary_Pay
    Dim RPTBROKEROUTSTANDINGRECSUMM As New OutstandingReport_Broker_Summary_Rec
    Dim RPTBROKEROUTSTANDINGPAYDTLS As New OutstandingReport_Broker_Details_Pay
    Dim RPTBROKEROUTSTANDINGRECDTLS As New OutstandingReport_Broker_Details_Rec
    Dim RPTBROKEROUTSTANDINGRECDTLSSHORT As New OutstandingReport_Broker_Details_Rec

    Dim RPTINTOUTSTANDINGREC As New OutstandingInterestReport_Rec
    Dim RPTINTOUTSTANDINGPAY As New OutstandingInterestReport_Pay

    Dim RPTOUTSTANDINGRECRUNBAL As New OutstandingReport_RunBal_Rec
    Dim RPTOUTSTANDINGPAYRUNBAL As New OutstandingReport_RunBal_Pay


    Dim RPTRECITEMOUTSTANDING As New OutstandingReport_Inventory_Details_Rec
    Dim RPTPAYITEMOUTSTANDING As New OutstandingReport_Inventory_Details_Pay


    Dim RPTRECBROKERITEMOUTSTANDING As New OutstandingReport_Broker_Inventory_Details_Rec
    Dim RPTPAYBROKERITEMOUTSTANDING As New OutstandingReport_Broker_Inventory_Details_Pay

    Dim RPTRECBROKERITEMOUTSTANDINGRUNBAL As New OutstandingReport_Broker_Inventory_RunBal_Details_Rec
    Dim RPTPAYBROKERITEMOUTSTANDINGRUNBAL As New OutstandingReport_Broker_Inventory_Details_Pay


    Dim RPTALLOUTSTANDINGREC As New OutstandingReport_AllBills_Summary_Rec
    Dim RPTALLOUTSTANDINGPAY As New OutstandingReport_AllBills_Summary_Pay

    Dim RPTONLYOUTSTANDINGREC As New OutstandingReport_AllBills_Summary_Rec
    Dim RPTONLYOUTSTANDINGPAY As New OutstandingReport_AllBills_Summary_Pay

    Dim RPTREMINDERLETTERREC As New OutstandingReport_Letter_Rec
    Dim RPTREMINDERLETTERPAY As New OutstandingReport_Letter_Pay

    Dim RPTOLDNEWREC As New MonthlyOldNew_Rec
    Dim RPTOLDNEWPAY As New MonthlyOldNew_Pay

    'NEWLY ADDED
    Public REPORTNAME As String
    Public DAYS As String
    Public TODATE As Date
    Public ADDRESS As Integer
    Public NEWPAGE As Boolean
    Public FRMSTRING As String
    Public PARTYNAME As String = ""
    Public AGENTNAME As String = ""
    Public selfor_ss As String
    Public PERIOD As String
    Public INTEREST As Double
    Public INTDAYS As Integer
    Public SHOWPRINTDATE As Integer
    Public SHOWREMARKS As Integer
    Public SHOWDETAILS As Integer
    Public MULTICMP As Integer

    Public REGNAME As String
    Public BILLNO As Integer
    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1

    Private Sub OutstandingDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If DIRECTPRINT = True Then
                PRINTDIRECTADVICE()
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor

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


            If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then crTables = RPTOUTSTANDINGALLSUMMREC.Database.Tables
            If FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then crTables = RPTOUTSTANDINGALLSUMMPAY.Database.Tables
            If FRMSTRING = "OUTSTANDINGALLDTLS" Then crTables = RPTOUTSTANDINGALLDTLS.Database.Tables

            If FRMSTRING = "OUTSTANDINGPAYSUMM" Then crTables = RPTOUTSTANDINGPAYSUMM.Database.Tables
            If FRMSTRING = "OUTSTANDINGRECSUMM" Then crTables = RPTOUTSTANDINGRECSUMM.Database.Tables
            If FRMSTRING = "OUTSTANDINGPAYDTLS" Then crTables = RPTOUTSTANDINGPAYDTLS.Database.Tables
            If FRMSTRING = "OUTSTANDINGRECDTLS" Then crTables = RPTOUTSTANDINGRECDTLS.Database.Tables

            If FRMSTRING = "BROKEROUTSTANDINGPAYSUMM" Then crTables = RPTBROKEROUTSTANDINGPAYSUMM.Database.Tables
            If FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then crTables = RPTBROKEROUTSTANDINGRECSUMM.Database.Tables
            If FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then crTables = RPTBROKEROUTSTANDINGPAYDTLS.Database.Tables
            If FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Then crTables = RPTBROKEROUTSTANDINGRECDTLS.Database.Tables

            If FRMSTRING = "INTOUTSTANDINGREC" Then crTables = RPTINTOUTSTANDINGREC.Database.Tables
            If FRMSTRING = "INTOUTSTANDINGPAY" Then crTables = RPTINTOUTSTANDINGPAY.Database.Tables

            If FRMSTRING = "OUTSTANDINGRECRUNBALDTLS" Then crTables = RPTOUTSTANDINGRECRUNBAL.Database.Tables
            If FRMSTRING = "OUTSTANDINGPAYRUNBALDTLS" Then crTables = RPTOUTSTANDINGPAYRUNBAL.Database.Tables

            If FRMSTRING = "RECINVENTORYOUTSTANDING" Then crTables = RPTRECITEMOUTSTANDING.Database.Tables
            If FRMSTRING = "PAYINVENTORYOUTSTANDING" Then crTables = RPTPAYITEMOUTSTANDING.Database.Tables

            If FRMSTRING = "RECBROKERINVENTORYOUTSTANDING" Then crTables = RPTRECBROKERITEMOUTSTANDING.Database.Tables
            If FRMSTRING = "PAYBROKERINVENTORYOUTSTANDING" Then crTables = RPTPAYBROKERITEMOUTSTANDING.Database.Tables

            If FRMSTRING = "RECBROKERINVENTORYOUTSTANDINGRUNBAL" Then crTables = RPTRECBROKERITEMOUTSTANDINGRUNBAL.Database.Tables
            If FRMSTRING = "PAYBROKERINVENTORYOUTSTANDINGRUNBAL" Then crTables = RPTPAYBROKERITEMOUTSTANDINGRUNBAL.Database.Tables

            If FRMSTRING = "ALLBILLOUTSTANDINGREC" Then crTables = RPTALLOUTSTANDINGREC.Database.Tables
            If FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then crTables = RPTALLOUTSTANDINGPAY.Database.Tables

            If FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then crTables = RPTONLYOUTSTANDINGREC.Database.Tables
            If FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then crTables = RPTONLYOUTSTANDINGPAY.Database.Tables

            If FRMSTRING = "REMINDERLETTERREC" Then crTables = RPTREMINDERLETTERREC.Database.Tables
            If FRMSTRING = "REMINDERLETTERPAY" Then crTables = RPTREMINDERLETTERPAY.Database.Tables


            If FRMSTRING = "OLDNEWREC" Then crTables = RPTOLDNEWREC.Database.Tables
            If FRMSTRING = "OLDNEWPAY" Then crTables = RPTOLDNEWPAY.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then

                CRPO.ReportSource = RPTOUTSTANDINGALLSUMMREC
                RPTOUTSTANDINGALLSUMMREC.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGALLSUMMREC.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGALLSUMMREC.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTOUTSTANDINGALLSUMMREC.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then

                CRPO.ReportSource = RPTOUTSTANDINGALLSUMMPAY
                RPTOUTSTANDINGALLSUMMPAY.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGALLSUMMPAY.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGALLSUMMPAY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTOUTSTANDINGALLSUMMPAY.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                crTables = RPTOUTSTANDINGALLSUMMPAY.Database.Tables

            ElseIf FRMSTRING = "OUTSTANDINGALLDTLS" Then

                CRPO.ReportSource = RPTOUTSTANDINGALLDTLS
                RPTOUTSTANDINGALLDTLS.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGALLDTLS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGALLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTOUTSTANDINGALLDTLS.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTOUTSTANDINGALLDTLS.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTOUTSTANDINGALLDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "OUTSTANDINGPAYSUMM" Then

                CRPO.ReportSource = RPTOUTSTANDINGPAYSUMM
                RPTOUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTOUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGPAYSUMM.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTOUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "OUTSTANDINGRECSUMM" Then

                CRPO.ReportSource = RPTOUTSTANDINGRECSUMM
                RPTOUTSTANDINGRECSUMM.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGRECSUMM.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTOUTSTANDINGRECSUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGRECSUMM.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGRECSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTOUTSTANDINGRECSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "OUTSTANDINGPAYDTLS" Then

                CRPO.ReportSource = RPTOUTSTANDINGPAYDTLS
                RPTOUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTOUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGPAYDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTOUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTOUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 0
                RPTOUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTOUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("MULTICMP").Text = MULTICMP
                RPTOUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "OUTSTANDINGRECDTLS" Then

                CRPO.ReportSource = RPTOUTSTANDINGRECDTLS
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGRECDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 0
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("MULTICMP").Text = MULTICMP
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYSUMM" Then

                CRPO.ReportSource = RPTBROKEROUTSTANDINGPAYSUMM
                RPTBROKEROUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTBROKEROUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTBROKEROUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTBROKEROUTSTANDINGPAYSUMM.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTBROKEROUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTBROKEROUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then

                CRPO.ReportSource = RPTBROKEROUTSTANDINGRECSUMM
                RPTBROKEROUTSTANDINGRECSUMM.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTBROKEROUTSTANDINGRECSUMM.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTBROKEROUTSTANDINGRECSUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTBROKEROUTSTANDINGRECSUMM.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTBROKEROUTSTANDINGRECSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTBROKEROUTSTANDINGRECSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then

                CRPO.ReportSource = RPTBROKEROUTSTANDINGPAYDTLS
                RPTBROKEROUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTBROKEROUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTBROKEROUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTBROKEROUTSTANDINGPAYDTLS.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTBROKEROUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTBROKEROUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTBROKEROUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("MULTICMP").Text = MULTICMP
                RPTBROKEROUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTBROKEROUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Then

                CRPO.ReportSource = RPTBROKEROUTSTANDINGRECDTLS
                RPTBROKEROUTSTANDINGRECDTLS.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTBROKEROUTSTANDINGRECDTLS.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTBROKEROUTSTANDINGRECDTLS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTBROKEROUTSTANDINGRECDTLS.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTBROKEROUTSTANDINGRECDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTBROKEROUTSTANDINGRECDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTBROKEROUTSTANDINGRECDTLS.DataDefinition.FormulaFields("MULTICMP").Text = MULTICMP
                RPTBROKEROUTSTANDINGRECDTLS.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTBROKEROUTSTANDINGRECDTLS.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "INTOUTSTANDINGREC" Then

                CRPO.ReportSource = RPTINTOUTSTANDINGREC
                RPTINTOUTSTANDINGREC.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTINTOUTSTANDINGREC.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTINTOUTSTANDINGREC.DataDefinition.FormulaFields("INTDAYS").Text = INTDAYS
                RPTINTOUTSTANDINGREC.DataDefinition.FormulaFields("INTEREST").Text = INTEREST
                RPTINTOUTSTANDINGREC.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTINTOUTSTANDINGREC.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTINTOUTSTANDINGREC.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "INTOUTSTANDINGPAY" Then

                CRPO.ReportSource = RPTINTOUTSTANDINGPAY
                RPTINTOUTSTANDINGPAY.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTINTOUTSTANDINGPAY.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTINTOUTSTANDINGPAY.DataDefinition.FormulaFields("INTDAYS").Text = INTDAYS
                RPTINTOUTSTANDINGPAY.DataDefinition.FormulaFields("INTREST").Text = INTEREST
                RPTINTOUTSTANDINGPAY.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTINTOUTSTANDINGPAY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTINTOUTSTANDINGPAY.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "OUTSTANDINGRECRUNBALDTLS" Then

                CRPO.ReportSource = RPTOUTSTANDINGRECRUNBAL
                RPTOUTSTANDINGRECRUNBAL.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGRECRUNBAL.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTOUTSTANDINGRECRUNBAL.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGRECRUNBAL.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGRECRUNBAL.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTOUTSTANDINGRECRUNBAL.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTOUTSTANDINGRECRUNBAL.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTOUTSTANDINGRECRUNBAL.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "OUTSTANDINGPAYRUNBALDTLS" Then

                CRPO.ReportSource = RPTOUTSTANDINGPAYRUNBAL
                RPTOUTSTANDINGPAYRUNBAL.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGPAYRUNBAL.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTOUTSTANDINGPAYRUNBAL.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGPAYRUNBAL.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGPAYRUNBAL.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTOUTSTANDINGPAYRUNBAL.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTOUTSTANDINGPAYRUNBAL.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTOUTSTANDINGPAYRUNBAL.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "RECINVENTORYOUTSTANDING" Then

                CRPO.ReportSource = RPTRECITEMOUTSTANDING
                RPTRECITEMOUTSTANDING.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTRECITEMOUTSTANDING.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTRECITEMOUTSTANDING.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTRECITEMOUTSTANDING.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTRECITEMOUTSTANDING.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTRECITEMOUTSTANDING.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTRECITEMOUTSTANDING.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 1
                RPTRECITEMOUTSTANDING.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTRECITEMOUTSTANDING.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTRECITEMOUTSTANDING.Subreports(0).DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "PAYINVENTORYOUTSTANDING" Then

                CRPO.ReportSource = RPTPAYITEMOUTSTANDING
                RPTPAYITEMOUTSTANDING.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTPAYITEMOUTSTANDING.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTPAYITEMOUTSTANDING.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTPAYITEMOUTSTANDING.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTPAYITEMOUTSTANDING.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTPAYITEMOUTSTANDING.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTPAYITEMOUTSTANDING.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 1
                RPTPAYITEMOUTSTANDING.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTPAYITEMOUTSTANDING.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "RECBROKERINVENTORYOUTSTANDING" Then

                CRPO.ReportSource = RPTRECBROKERITEMOUTSTANDING
                RPTRECBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTRECBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTRECBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTRECBROKERITEMOUTSTANDING.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTRECBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTRECBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTRECBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 1
                RPTRECBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTRECBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "RECBROKERINVENTORYOUTSTANDINGRUNBAL" Then

                CRPO.ReportSource = RPTRECBROKERITEMOUTSTANDINGRUNBAL
                RPTRECBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTRECBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTRECBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTRECBROKERITEMOUTSTANDINGRUNBAL.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTRECBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTRECBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTRECBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTRECBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "PAYBROKERINVENTORYOUTSTANDING" Then

                CRPO.ReportSource = RPTPAYBROKERITEMOUTSTANDING
                RPTPAYBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTPAYBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTPAYBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTPAYBROKERITEMOUTSTANDING.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTPAYBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTPAYBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTPAYBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 1
                RPTPAYBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTPAYBROKERITEMOUTSTANDING.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "PAYBROKERINVENTORYOUTSTANDINGRUNBAL" Then

                CRPO.ReportSource = RPTPAYBROKERITEMOUTSTANDINGRUNBAL
                RPTPAYBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTPAYBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTPAYBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTPAYBROKERITEMOUTSTANDINGRUNBAL.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTPAYBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTPAYBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTPAYBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 1
                RPTPAYBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTPAYBROKERITEMOUTSTANDINGRUNBAL.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGREC" Then

                CRPO.ReportSource = RPTALLOUTSTANDINGREC
                RPTALLOUTSTANDINGREC.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                RPTALLOUTSTANDINGREC.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTALLOUTSTANDINGREC.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTALLOUTSTANDINGREC.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTALLOUTSTANDINGREC.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then

                CRPO.ReportSource = RPTALLOUTSTANDINGPAY
                RPTALLOUTSTANDINGPAY.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                RPTALLOUTSTANDINGPAY.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTALLOUTSTANDINGPAY.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTALLOUTSTANDINGPAY.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTALLOUTSTANDINGPAY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then

                CRPO.ReportSource = RPTONLYOUTSTANDINGREC
                RPTONLYOUTSTANDINGREC.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                RPTONLYOUTSTANDINGREC.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTONLYOUTSTANDINGREC.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTONLYOUTSTANDINGREC.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTONLYOUTSTANDINGREC.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTONLYOUTSTANDINGREC.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then

                CRPO.ReportSource = RPTONLYOUTSTANDINGPAY
                RPTONLYOUTSTANDINGPAY.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                RPTONLYOUTSTANDINGPAY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTONLYOUTSTANDINGPAY.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTONLYOUTSTANDINGPAY.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTONLYOUTSTANDINGPAY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTONLYOUTSTANDINGPAY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "REMINDERLETTERREC" Then

                CRPO.ReportSource = RPTREMINDERLETTERREC
                RPTREMINDERLETTERREC.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTREMINDERLETTERREC.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTREMINDERLETTERREC.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTREMINDERLETTERREC.GroupFooterSection7.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTREMINDERLETTERREC.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTREMINDERLETTERREC.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTREMINDERLETTERREC.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 0
                RPTREMINDERLETTERREC.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTREMINDERLETTERREC.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "REMINDERLETTERPAY" Then

                CRPO.ReportSource = RPTREMINDERLETTERPAY
                RPTREMINDERLETTERPAY.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTREMINDERLETTERPAY.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTREMINDERLETTERPAY.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTREMINDERLETTERPAY.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTREMINDERLETTERPAY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTREMINDERLETTERPAY.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTREMINDERLETTERPAY.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 0
                RPTREMINDERLETTERPAY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTREMINDERLETTERPAY.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "OLDNEWPAY" Then

                CRPO.ReportSource = RPTOLDNEWPAY
                RPTOLDNEWPAY.DataDefinition.FormulaFields("SHOWDETAILS").Text = SHOWDETAILS
                RPTOLDNEWPAY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE

            ElseIf FRMSTRING = "OLDNEWREC" Then

                CRPO.ReportSource = RPTOLDNEWREC
                RPTOLDNEWREC.DataDefinition.FormulaFields("SHOWDETAILS").Text = SHOWDETAILS
                RPTOLDNEWREC.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
            End If

            CRPO.SelectionFormula = selfor_ss

            If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then
                CRPO.ReportSource = RPTOUTSTANDINGALLSUMMREC
            ElseIf FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then
                CRPO.ReportSource = RPTOUTSTANDINGALLSUMMPAY
            ElseIf FRMSTRING = "OUTSTANDINGALLDTLS" Then
                CRPO.ReportSource = RPTOUTSTANDINGALLDTLS
            ElseIf FRMSTRING = "OUTSTANDINGPAYSUMM" Then
                CRPO.ReportSource = RPTOUTSTANDINGPAYSUMM
            ElseIf FRMSTRING = "OUTSTANDINGRECSUMM" Then
                CRPO.ReportSource = RPTOUTSTANDINGRECSUMM
            ElseIf FRMSTRING = "OUTSTANDINGPAYDTLS" Then
                CRPO.ReportSource = RPTOUTSTANDINGPAYDTLS
            ElseIf FRMSTRING = "OUTSTANDINGRECDTLS" Then
                CRPO.ReportSource = RPTOUTSTANDINGRECDTLS
            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYSUMM" Then
                CRPO.ReportSource = RPTBROKEROUTSTANDINGPAYSUMM
            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then
                CRPO.ReportSource = RPTBROKEROUTSTANDINGRECSUMM
            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then
                CRPO.ReportSource = RPTBROKEROUTSTANDINGPAYDTLS
            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Then
                CRPO.ReportSource = RPTBROKEROUTSTANDINGRECDTLS
            ElseIf FRMSTRING = "INTOUTSTANDINGREC" Then
                CRPO.ReportSource = RPTINTOUTSTANDINGREC
            ElseIf FRMSTRING = "INTOUTSTANDINGPAY" Then
                CRPO.ReportSource = RPTINTOUTSTANDINGPAY
            ElseIf FRMSTRING = "OUTSTANDINGRECRUNBALDTLS" Then
                CRPO.ReportSource = RPTOUTSTANDINGRECRUNBAL
            ElseIf FRMSTRING = "OUTSTANDINGPAYRUNBALDTLS" Then
                CRPO.ReportSource = RPTOUTSTANDINGPAYRUNBAL
            ElseIf FRMSTRING = "RECINVENTORYOUTSTANDING" Then
                CRPO.ReportSource = RPTRECITEMOUTSTANDING
            ElseIf FRMSTRING = "PAYINVENTORYOUTSTANDING" Then
                CRPO.ReportSource = RPTPAYITEMOUTSTANDING
            ElseIf FRMSTRING = "RECBROKERINVENTORYOUTSTANDING" Then
                CRPO.ReportSource = RPTRECBROKERITEMOUTSTANDING
            ElseIf FRMSTRING = "PAYBROKERINVENTORYOUTSTANDING" Then
                CRPO.ReportSource = RPTPAYBROKERITEMOUTSTANDING
            ElseIf FRMSTRING = "RECBROKERINVENTORYOUTSTANDINGRUNBAL" Then
                CRPO.ReportSource = RPTRECBROKERITEMOUTSTANDINGRUNBAL
            ElseIf FRMSTRING = "PAYBROKERINVENTORYOUTSTANDINGRUNBAL" Then
                CRPO.ReportSource = RPTPAYBROKERITEMOUTSTANDINGRUNBAL
            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGREC" Then
                CRPO.ReportSource = RPTALLOUTSTANDINGREC
            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then
                CRPO.ReportSource = RPTALLOUTSTANDINGPAY
            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then
                CRPO.ReportSource = RPTONLYOUTSTANDINGREC
            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then
                CRPO.ReportSource = RPTONLYOUTSTANDINGPAY
            ElseIf FRMSTRING = "REMINDERLETTERREC" Then
                CRPO.ReportSource = RPTREMINDERLETTERREC
            ElseIf FRMSTRING = "REMINDERLETTERPAY" Then
                CRPO.ReportSource = RPTREMINDERLETTERPAY
            ElseIf FRMSTRING = "OLDNEWREC" Then
                CRPO.ReportSource = RPTOLDNEWREC
            ElseIf FRMSTRING = "OLDNEWPAY" Then
                CRPO.ReportSource = RPTOLDNEWPAY
            End If
            '************************ END *******************

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Try
            Dim emailid As String = ""
            Dim emailid1 As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Transfer()
            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\Outstanding Report.PDF"

            If PARTYNAME <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable = OBJCMN.search("ACC_EMAIL AS EMAILID", "", "LEDGERS", " and ACC_CMPNAME = '" & PARTYNAME & "' AND ACC_YEARID=" & YearId)
                If dt.Rows.Count > 0 Then
                    emailid = dt.Rows(0).Item(0).ToString
                End If
            End If

            If AGENTNAME <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable = OBJCMN.search("ACC_EMAIL AS EMAILID", "", "LEDGERS", " and ACC_CMPNAME = '" & AGENTNAME & "' AND ACC_YEARID=" & YearId)
                If dt.Rows.Count > 0 Then
                    emailid1 = dt.Rows(0).Item(0).ToString
                End If
            End If


            If emailid <> "" Then objmail.cmbfirstadd.Text = emailid
            If emailid1 <> "" Then objmail.cmbsecondadd.Text = emailid1
            objmail.subject = "OUTSTANDING"
            objmail.Show()
            objmail.BringToFront()
            Windows.Forms.Cursor.Current = Cursors.Arrow
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\Outstanding Report.pdf"


            If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then
                expo = RPTOUTSTANDINGALLSUMMREC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGALLSUMMREC.Export()
            ElseIf FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then
                expo = RPTOUTSTANDINGALLSUMMPAY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGALLSUMMPAY.Export()
            ElseIf FRMSTRING = "OUTSTANDINGALLDTLS" Then
                expo = RPTOUTSTANDINGALLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGALLDTLS.Export()
            ElseIf FRMSTRING = "OUTSTANDINGPAYSUMM" Then
                expo = RPTOUTSTANDINGPAYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGPAYSUMM.Export()
            ElseIf FRMSTRING = "OUTSTANDINGRECSUMM" Then
                expo = RPTOUTSTANDINGRECSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGRECSUMM.Export()
            ElseIf FRMSTRING = "OUTSTANDINGPAYDTLS" Then
                expo = RPTOUTSTANDINGPAYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGPAYDTLS.Export()
            ElseIf FRMSTRING = "OUTSTANDINGRECDTLS" Then
                expo = RPTOUTSTANDINGRECDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGRECDTLS.Export()
            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYSUMM" Then
                expo = RPTBROKEROUTSTANDINGPAYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBROKEROUTSTANDINGPAYSUMM.Export()
            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then
                expo = RPTBROKEROUTSTANDINGRECSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBROKEROUTSTANDINGRECSUMM.Export()
            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then
                expo = RPTBROKEROUTSTANDINGPAYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBROKEROUTSTANDINGPAYDTLS.Export()
            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Then
                expo = RPTBROKEROUTSTANDINGRECDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBROKEROUTSTANDINGRECDTLS.Export()
            ElseIf FRMSTRING = "INTOUTSTANDINGREC" Then
                expo = RPTINTOUTSTANDINGREC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINTOUTSTANDINGREC.Export()
            ElseIf FRMSTRING = "INTOUTSTANDINGPAY" Then
                expo = RPTINTOUTSTANDINGPAY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINTOUTSTANDINGPAY.Export()
            ElseIf FRMSTRING = "OUTSTANDINGRECRUNBALDTLS" Then
                expo = RPTOUTSTANDINGRECRUNBAL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGRECRUNBAL.Export()
            ElseIf FRMSTRING = "OUTSTANDINGPAYRUNBALDTLS" Then
                expo = RPTOUTSTANDINGPAYRUNBAL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGPAYRUNBAL.Export()
            ElseIf FRMSTRING = "RECINVENTORYOUTSTANDING" Then
                expo = RPTRECITEMOUTSTANDING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTRECITEMOUTSTANDING.Export()
            ElseIf FRMSTRING = "PAYINVENTORYOUTSTANDING" Then
                expo = RPTPAYITEMOUTSTANDING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPAYITEMOUTSTANDING.Export()
            ElseIf FRMSTRING = "RECBROKERINVENTORYOUTSTANDING" Then
                expo = RPTRECBROKERITEMOUTSTANDING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTRECBROKERITEMOUTSTANDING.Export()
            ElseIf FRMSTRING = "PAYBROKERINVENTORYOUTSTANDING" Then
                expo = RPTPAYBROKERITEMOUTSTANDING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPAYBROKERITEMOUTSTANDING.Export()
            ElseIf FRMSTRING = "RECBROKERINVENTORYOUTSTANDINGRUNBAL" Then
                expo = RPTRECBROKERITEMOUTSTANDINGRUNBAL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTRECBROKERITEMOUTSTANDINGRUNBAL.Export()
            ElseIf FRMSTRING = "PAYBROKERINVENTORYOUTSTANDINGRUNBAL" Then
                expo = RPTPAYBROKERITEMOUTSTANDINGRUNBAL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPAYBROKERITEMOUTSTANDINGRUNBAL.Export()
            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGREC" Then
                expo = RPTALLOUTSTANDINGREC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTALLOUTSTANDINGREC.Export()
            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then
                expo = RPTALLOUTSTANDINGPAY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTALLOUTSTANDINGPAY.Export()
            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then
                expo = RPTONLYOUTSTANDINGREC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTONLYOUTSTANDINGREC.Export()
            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then
                expo = RPTONLYOUTSTANDINGPAY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTONLYOUTSTANDINGPAY.Export()
            ElseIf FRMSTRING = "REMINDERLETTERREC" Then
                expo = RPTREMINDERLETTERREC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTREMINDERLETTERREC.Export()
            ElseIf FRMSTRING = "REMINDERLETTERPAY" Then
                expo = RPTREMINDERLETTERPAY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTREMINDERLETTERPAY.Export()
            ElseIf FRMSTRING = "OLDNEWREC" Then
                expo = RPTOLDNEWREC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOLDNEWREC.Export()
            ElseIf FRMSTRING = "OLDNEWPAY" Then
                expo = RPTOLDNEWPAY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOLDNEWPAY.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub OutstandingDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If File.Exists(Application.StartupPath & "\" & PARTYNAME & "Outstanding Report" & ".PDF") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\" & PARTYNAME & "Outstanding Report" & ".PDF")
            Transfer()
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = PARTYNAME
            OBJWHATSAPP.AGENTNAME = AGENTNAME

            If File.Exists(Application.StartupPath & "\Outstanding Report" & ".PDF") And PARTYNAME <> "" Then My.Computer.FileSystem.RenameFile(Application.StartupPath & "\Outstanding Report" & ".PDF", PARTYNAME & "Outstanding Report" & ".PDF")

            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & PARTYNAME & "Outstanding Report" & ".PDF")
            OBJWHATSAPP.FILENAME.Add(PARTYNAME & "Outstanding Report.pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTDIRECTADVICE()
        Try

            'Dim CONN As New DBConnection
            'Dim DA As New SqlDataAdapter("SELECT        OUTSTANDINGREPORT_DETAILS.DATE, OUTSTANDINGREPORT_DETAILS.NAME, OUTSTANDINGREPORT_DETAILS.TYPE, OUTSTANDINGREPORT_DETAILS.BILLINITIALS, OUTSTANDINGREPORT_DETAILS.GRANDTOTAL, 
            '                         LEDGERS.Acc_add, OUTSTANDINGREPORT_DETAILS.RECINITIALS, OUTSTANDINGREPORT_DETAILS.RECDATE, OUTSTANDINGREPORT_DETAILS.RECAMT, OUTSTANDINGREPORT_DETAILS.AGENT, 
            '                         OUTSTANDINGREPORT_DETAILS.DUEDATE, OUTSTANDINGREPORT_DETAILS.BILL, OUTSTANDINGREPORT_DETAILS.GROUPNAME, OUTSTANDINGREPORT_DETAILS.CITY, OUTSTANDINGREPORT_DETAILS.MOBILENO, 
            '                         OUTSTANDINGREPORT_DETAILS.YEARID, LEDGERS.Acc_crdays, LEDGERS.Acc_exciseno, AGENTLEDGERS.Acc_mobile, CMPMASTER.CMP_BANKNAME, CMPMASTER.CMP_BANKACNO, CMPMASTER.CMP_IFSCCODE, 
            '                         CMPMASTER.CMP_UPI, CMPMASTER.cmp_displayedname, YEARMASTER.year_id, OUTSTANDINGREPORT_DETAILS.CD, LEDGERS.ACC_DISC, LEDGERS.ACC_CDPER, CMPMASTER.cmp_plano, 
            '                         OUTSTANDINGREPORT_DETAILS.REMARKS
            'FROM            OUTSTANDINGREPORT_DETAILS INNER JOIN
            '                         CMPMASTER AS CMPMASTER ON OUTSTANDINGREPORT_DETAILS.CMPID = CMPMASTER.cmp_id INNER JOIN
            '                         YEARMASTER AS YEARMASTER ON OUTSTANDINGREPORT_DETAILS.YEARID = YEARMASTER.year_id INNER JOIN
            '                         LEDGERS AS LEDGERS ON OUTSTANDINGREPORT_DETAILS.LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN
            '                         LEDGERS AS AGENTLEDGERS ON OUTSTANDINGREPORT_DETAILS.AGENTID = AGENTLEDGERS.Acc_id
            'WHERE        OUTSTANDINGREPORT_DETAILS.YEARID = 6 AND 
            'OUTSTANDINGREPORT_DETAILS.LEDGERID = 85719
            'ORDER BY OUTSTANDINGREPORT_DETAILS.NAME, YEARMASTER.year_id, OUTSTANDINGREPORT_DETAILS.AGENT, OUTSTANDINGREPORT_DETAILS.DATE, OUTSTANDINGREPORT_DETAILS.BILL, 
            '                         OUTSTANDINGREPORT_DETAILS.BILLINITIALS", DBConnection.ConnectionString)

            'Dim DSA As New DataSet()
            'DA.Fill(DSA)
            'Dim TEMPRPT As New ReportDocument
            'TEMPRPT.Load("D:\VSS\VSS\TEXTRADE\TEXTRADE\TEXTRADE\Reports\Crystal Reports\Outstanding Reports\OutstandingReport_Details_Rec.rpt")

            'Dim crtableLogonInfo As New TableLogOnInfo
            'Dim crConnecttionInfo As New ConnectionInfo
            'Dim crTables As Tables
            'Dim crTable As Table

            'With crConnecttionInfo
            '    .ServerName = SERVERNAME
            '    .DatabaseName = DatabaseName
            '    .UserID = DBUSERNAME
            '    .Password = Dbpassword
            '    .IntegratedSecurity = Dbsecurity
            'End With
            'crTables = TEMPRPT.Database.Tables

            'For Each crTable In crTables
            '    crtableLogonInfo = crTable.LogOnInfo
            '    crtableLogonInfo.ConnectionInfo = crConnecttionInfo
            '    crTable.ApplyLogOnInfo(crtableLogonInfo)
            'Next
            'TEMPRPT.SetDataSource(DSA)

            'TEMPRPT.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath & "\" & PARTYNAME & "_OUTSTANDING" & ".PDF")








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

            'strsearch = "  {OUTSTANDINGREPORT_DETAILS.NAME} = '" & ROW("NAME") & "' AND {OUTSTANDINGREPORT_DETAILS.yearid} = " & YearId

            Dim OBJ As New Object
            If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then
                OBJ = New OutstandingReport_All_Summary_Rec
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then
                OBJ = New OutstandingReport_All_Summary_Pay
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE


            ElseIf FRMSTRING = "OUTSTANDINGALLDTLS" Then
                OBJ = New OutstandingReport_All_Details
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "OUTSTANDINGPAYSUMM" Then
                OBJ = New OutstandingReport_Summary_Pay
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "OUTSTANDINGRECSUMM" Then
                OBJ = New OutstandingReport_Summary_Rec
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "OUTSTANDINGPAYDTLS" Then
                OBJ = New OutstandingReport_Details_Pay
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 0
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("MULTICMP").Text = MULTICMP
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "OUTSTANDINGRECDTLS" Then
                OBJ = New OutstandingReport_Details_Rec
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 0
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("MULTICMP").Text = MULTICMP
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYSUMM" Then
                OBJ = New OutstandingReport_Broker_Summary_Pay
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then
                OBJ = New OutstandingReport_Broker_Summary_Rec
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then
                OBJ = New OutstandingReport_Broker_Details_Pay
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("MULTICMP").Text = MULTICMP
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Then
                OBJ = New OutstandingReport_Broker_Details_Rec
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("MULTICMP").Text = MULTICMP
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "INTOUTSTANDINGREC" Then
                OBJ = New OutstandingInterestReport_Rec
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("INTDAYS").Text = INTDAYS
                OBJ.DataDefinition.FormulaFields("INTEREST").Text = INTEREST
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "INTOUTSTANDINGPAY" Then
                OBJ = New OutstandingInterestReport_Pay
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("INTDAYS").Text = INTDAYS
                OBJ.DataDefinition.FormulaFields("INTREST").Text = INTEREST
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "OUTSTANDINGRECRUNBALDTLS" Then
                OBJ = New OutstandingReport_RunBal_Rec
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "OUTSTANDINGPAYRUNBALDTLS" Then
                OBJ = New OutstandingReport_RunBal_Pay
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "RECINVENTORYOUTSTANDING" Then
                OBJ = New OutstandingReport_Inventory_Details_Rec
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 1
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                OBJ.Subreports(0).DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "PAYINVENTORYOUTSTANDING" Then
                OBJ = New OutstandingReport_Inventory_Details_Pay
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 1
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "RECBROKERINVENTORYOUTSTANDING" Then
                OBJ = New OutstandingReport_Broker_Inventory_Details_Rec
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 1
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "PAYBROKERINVENTORYOUTSTANDING" Then
                OBJ = New OutstandingReport_Broker_Inventory_Details_Pay
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 1
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "RECBROKERINVENTORYOUTSTANDINGRUNBAL" Then
                OBJ = New OutstandingReport_Broker_Inventory_RunBal_Details_Rec
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "PAYBROKERINVENTORYOUTSTANDINGRUNBAL" Then
                OBJ = New OutstandingReport_Broker_Inventory_RunBal_Details_Rec
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGREC" Then
                OBJ = New OutstandingReport_AllBills_Summary_Rec
                OBJ.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then
                OBJ = New OutstandingReport_AllBills_Summary_Pay
                OBJ.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then
                OBJ = New OutstandingReport_AllBills_Summary_Rec
                OBJ.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then
                OBJ = New OutstandingReport_AllBills_Summary_Pay
                OBJ.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "REMINDERLETTERREC" Then
                OBJ = New OutstandingReport_Letter_Rec
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection7.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 0
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "REMINDERLETTERPAY" Then
                OBJ = New OutstandingReport_Letter_Pay
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection7.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 0
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "OLDNEWREC" Then
                OBJ = New MonthlyOldNew_Rec
                OBJ.DataDefinition.FormulaFields("SHOWDETAILS").Text = SHOWDETAILS
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE

            ElseIf FRMSTRING = "OLDNEWPAY" Then
                OBJ = New MonthlyOldNew_Pay
                OBJ.DataDefinition.FormulaFields("SHOWDETAILS").Text = SHOWDETAILS
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE

            End If


            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = selfor_ss
            OBJ.REFRESH()
            'Dim TEMPRPT As New ReportDocument
            'TEMPRPT.Load(OBJ)




            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                If File.Exists(Application.StartupPath & "\" & PARTYNAME & "_OUTSTANDING" & ".PDF") Then File.Delete(Application.StartupPath & "\" & PARTYNAME & "_OUTSTANDING" & ".PDF")
                OBJ.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath & "\" & PARTYNAME & "_OUTSTANDING" & ".PDF")


                'Dim expo As New ExportOptions
                'Dim oDfDopt As New DiskFileDestinationOptions
                'oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_OUTSTANDING" & ".PDF"

                ''CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                'If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
                'expo = OBJ.ExportOptions
                'expo.ExportDestinationType = ExportDestinationType.DiskFile
                'expo.ExportFormatType = ExportFormatType.PortableDocFormat
                'expo.DestinationOptions = oDfDopt
                'OBJ.Export()
            End If
            OBJ.CLOSE()
            OBJ.DISPOSE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class