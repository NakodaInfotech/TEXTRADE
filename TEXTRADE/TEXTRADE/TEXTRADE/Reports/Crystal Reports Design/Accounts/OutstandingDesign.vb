
Imports BL
Imports DB
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports System.Data.SqlClient

Public Class OutstandingDesign

    Dim OBJ As New Object

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

            If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then
                OBJ = New OutstandingReport_All_Summary_Rec
            ElseIf FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then
                OBJ = New OutstandingReport_All_Summary_Pay
            ElseIf FRMSTRING = "OUTSTANDINGALLDTLS" Then
                OBJ = New OutstandingReport_All_Details
            ElseIf FRMSTRING = "OUTSTANDINGPAYSUMM" Then
                OBJ = New OutstandingReport_Summary_Pay
            ElseIf FRMSTRING = "OUTSTANDINGRECSUMM" Then
                OBJ = New OutstandingReport_Summary_Rec
            ElseIf FRMSTRING = "OUTSTANDINGPAYDTLS" Then
                OBJ = New OutstandingReport_Details_Pay
            ElseIf FRMSTRING = "OUTSTANDINGRECDTLS" Then
                OBJ = New OutstandingReport_Details_Rec
            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYSUMM" Then
                OBJ = New OutstandingReport_Broker_Summary_Pay
            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then
                OBJ = New OutstandingReport_Broker_Summary_Rec
            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then
                OBJ = New OutstandingReport_Broker_Details_Pay
            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Then
                OBJ = New OutstandingReport_Broker_Details_Rec
            ElseIf FRMSTRING = "INTOUTSTANDINGREC" Then
                OBJ = New OutstandingInterestReport_Rec
            ElseIf FRMSTRING = "INTOUTSTANDINGPAY" Then
                OBJ = New OutstandingInterestReport_Pay
            ElseIf FRMSTRING = "OUTSTANDINGRECRUNBALDTLS" Then
                OBJ = New OutstandingReport_RunBal_Rec
            ElseIf FRMSTRING = "OUTSTANDINGPAYRUNBALDTLS" Then
                OBJ = New OutstandingReport_RunBal_Pay
            ElseIf FRMSTRING = "RECINVENTORYOUTSTANDING" Then
                OBJ = New OutstandingReport_Inventory_Details_Rec
            ElseIf FRMSTRING = "PAYINVENTORYOUTSTANDING" Then
                OBJ = New OutstandingReport_Inventory_Details_Pay
            ElseIf FRMSTRING = "RECBROKERINVENTORYOUTSTANDING" Then
                OBJ = New OutstandingReport_Broker_Inventory_Details_Rec
            ElseIf FRMSTRING = "PAYBROKERINVENTORYOUTSTANDING" Then
                OBJ = New OutstandingReport_Broker_Inventory_Details_Pay
            ElseIf FRMSTRING = "RECBROKERINVENTORYOUTSTANDINGRUNBAL" Then
                OBJ = New OutstandingReport_Broker_Inventory_RunBal_Details_Rec
            ElseIf FRMSTRING = "PAYBROKERINVENTORYOUTSTANDINGRUNBAL" Then
                OBJ = New OutstandingReport_Broker_Inventory_Details_Pay
            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGREC" Then
                OBJ = New OutstandingReport_AllBills_Summary_Rec
            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then
                OBJ = New OutstandingReport_AllBills_Summary_Pay
            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then
                OBJ = New OutstandingReport_AllBills_Summary_Rec
            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then
                OBJ = New OutstandingReport_AllBills_Summary_Pay
            ElseIf FRMSTRING = "REMINDERLETTERREC" Then
                OBJ = New OutstandingReport_Letter_Rec
            ElseIf FRMSTRING = "REMINDERLETTERPAY" Then
                OBJ = New OutstandingReport_Letter_Pay
            ElseIf FRMSTRING = "OLDNEWREC" Then
                OBJ = New MonthlyOldNew_Rec
            ElseIf FRMSTRING = "OLDNEWPAY" Then
                OBJ = New CachedMonthlyOldNew_Pay
            End If







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


            crTables = OBJ.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.ReportSource = OBJ

            If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                crTables = OBJ.Database.Tables

            ElseIf FRMSTRING = "OUTSTANDINGALLDTLS" Then
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "OUTSTANDINGPAYSUMM" Then
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "OUTSTANDINGRECSUMM" Then
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "OUTSTANDINGPAYDTLS" Then
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
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then
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
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("INTDAYS").Text = INTDAYS
                OBJ.DataDefinition.FormulaFields("INTEREST").Text = INTEREST
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "INTOUTSTANDINGPAY" Then
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("INTDAYS").Text = INTDAYS
                OBJ.DataDefinition.FormulaFields("INTREST").Text = INTEREST
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "OUTSTANDINGRECRUNBALDTLS" Then
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "OUTSTANDINGPAYRUNBALDTLS" Then
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "RECINVENTORYOUTSTANDING" Then
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
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "PAYBROKERINVENTORYOUTSTANDING" Then
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 1
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "PAYBROKERINVENTORYOUTSTANDINGRUNBAL" Then
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 1
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGREC" Then
                OBJ.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then
                OBJ.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then
                OBJ.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then
                OBJ.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "REMINDERLETTERREC" Then
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
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 0
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "OLDNEWPAY" Then
                OBJ.DataDefinition.FormulaFields("SHOWDETAILS").Text = SHOWDETAILS
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE

            ElseIf FRMSTRING = "OLDNEWREC" Then
                OBJ.DataDefinition.FormulaFields("SHOWDETAILS").Text = SHOWDETAILS
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
            End If

            CRPO.SelectionFormula = selfor_ss
            CRPO.ReportSource = OBJ

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
            objmail.attachment = Application.StartupPath & "\" & PARTYNAME & "Outstanding Report.PDF"

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
            oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "Outstanding Report.pdf"
            expo = OBJ.ExportOptions
            expo.ExportDestinationType = ExportDestinationType.DiskFile
            expo.ExportFormatType = ExportFormatType.PortableDocFormat
            expo.DestinationOptions = oDfDopt
            OBJ.Export()
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

            'If File.Exists(Application.StartupPath & "\Outstanding Report" & ".PDF") And PARTYNAME <> "" Then My.Computer.FileSystem.RenameFile(Application.StartupPath & "\Outstanding Report" & ".PDF", PARTYNAME & "Outstanding Report" & ".PDF")

            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & PARTYNAME & "Outstanding Report" & ".PDF")
            OBJWHATSAPP.FILENAME.Add(PARTYNAME & "Outstanding Report.pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
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

    Private Sub OutstandingDesign_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        OBJ.CLOSE()
        OBJ.DISPOSE()
    End Sub
End Class