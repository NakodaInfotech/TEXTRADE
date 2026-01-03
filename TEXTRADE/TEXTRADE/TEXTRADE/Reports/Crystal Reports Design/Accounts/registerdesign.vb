
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO
Imports BL

Public Class registerdesign

    Dim rptBr As New bankregistermonthly
    Dim rptBrd As New bankregisterdetailreport
    Dim rptcr As New cashregistermonthly
    Dim rptdb As New daybookreport
    Dim rptdbd As New daybookdetailreport

    Dim RPTPURREG As New PurchaseRegisterReport
    Dim RPTSALEREG As New SaleRegisterReport

    Dim rptlbc As New LedgerBookAcConfirmReport
    Dim RPTLBCSUMM As New LedgerBookConfirmationReport_Summ

    Dim RPTSALEREGMONTHLY As New SaleRegisterMonthlyReport
    Dim RPTPURREGMONTHLY As New PurchaseRegisterMonthlyReport

    Dim rptconr As New contraregisterreport
    Dim rptconrd As New contraregisterdetailreport
    Dim rptconrm As New contraregistermonthly
    Dim rptjvr As New jvregisterreport
    Dim rptjvrm As New jvregistermonthly

    Dim RPTEXPM As New ExpenseRegisterMonthly
    Dim RPTEXP As New ExpenseRegisterReport
    Dim RPTEXPD As New ExpenseRegisterDetailReport

    Dim rptptr As New purchasetaxreport
    Dim rptstr As New saletaxreport
    Dim rptptdr As New purchasetaxdetailreport
    Dim rptstdr As New saletaxdetailreport


    'Dim RPTLEDGERAC As New LedgerBookAcReport
    Dim RPTLEDGERAC As New LedgerBookAcReport
    Dim RPTLEDGERACRUNBAL As New LedgerBookAcRunBalReport
    Dim RPTLEDGERACSUMM As New LedgerBookAcSummReport
    Dim RPTLEDGERACDETAILS As New LedgerBookAcReportDetails
    Dim RPTLEDGERACTFORMAT As New LedgerBookAcReport_TReport
    Dim RPTGROUPMONTHLY As New GroupSummaryMonthlyReport
    Dim RPTSTATEMENT As New FFormPartyStatement
    Dim RPTSTATEMENTDTLS As New FFormPartyStatement_Details
    Dim RPTLEDGERMONTHLYTYPESUMM As New LedgerBookMonthlyTypeSummReport


    Public NEWPAGE As Boolean
    Public ADDRESS As Integer
    Public PANNO As Integer
    Public LETTERFORMAT As Integer
    Public strsearch As String
    Public FRMSTRING As String
    Public REG As String
    Public OPENING As Double
    Public OPENINGDRCR As String
    Public DRTOTAL As Double
    Public CRTOTAL As Double
    Public CLOSINGAMT As Double
    Public crdr As String
    Public CLOSINGDRCR As String
    Public PARTYNAME As String
    Public PERIOD As String
    Public FROMDATE As Date
    Public TODATE As Date
    Public CHECK As Boolean
    Public SHOWHEADER As Boolean
    Public SHOWNARRATION As Boolean = False

    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public DIRECTPRINT As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1

    Public bankname As String

    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()

            Dim tempattachment As String = ""

            If frmstring = "CashRegister" Then
                tempattachment = "CASHREGISTER"
            ElseIf frmstring = "BankRegister" Then
                tempattachment = "BANKREGISTER"
            ElseIf frmstring = "BankBookDetails" Then
                tempattachment = "BANKREGISTER"
            ElseIf frmstring = "GROUPSUMMONTHLY" Then
                tempattachment = "GROUPSUMMMARY"
            ElseIf frmstring = "DayBook" Or frmstring = "DayBookDetails" Then
                tempattachment = "DAYBOOK"
            ElseIf frmstring = "PurchaseRegister" Or frmstring = "PurchaseRegisterMonthly" Then
                tempattachment = "PURCHASEREGISTER"
            ElseIf frmstring = "JvRegister" Or frmstring = "JvRegisterDetails" Then
                tempattachment = "JOURNALREGISTER"
            ElseIf frmstring = "ContraRegister" Or frmstring = "ContraRegisterDetails" Then
                tempattachment = "CONTRAREGISTER"
            ElseIf frmstring = "SaleRegister" Or frmstring = "SaleRegisterDetails" Or frmstring = "SaleRegisterMonthly" Then
                tempattachment = "SALEREGISTER"
            ElseIf frmstring = "LedgerBook" Or frmstring = "LedgerBookRunBal" Or frmstring = "LedgerBookDetails" Or frmstring = "LedgerBookConfirm" Or frmstring = "LedgerBookConfirmSumm" Or frmstring = "LedgerBookMonthlyTypeSumm" Or frmstring = "LedgerPartySumm" Then
                tempattachment = "LEDGERBOOK"
            ElseIf frmstring = "purchasetax" Or frmstring = "purchasetaxdetail" Then
                tempattachment = "PURCHASETAXREGISTER"
            ElseIf frmstring = "saletax" Or frmstring = "saletaxdetail" Then
                tempattachment = "SALETAXREGISTER"
            End If

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = PARTYNAME
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & tempattachment & ".PDF")
            OBJWHATSAPP.FILENAME.Add(tempattachment & ".pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, FROMDATE)
        a2 = DatePart(DateInterval.Month, FROMDATE)
        a3 = DatePart(DateInterval.Year, FROMDATE)
        fromD = "(" & a3 & "," & a2 & "," & a1 & ")"

        a11 = DatePart(DateInterval.Day, TODATE)
        a12 = DatePart(DateInterval.Month, TODATE)
        a13 = DatePart(DateInterval.Year, TODATE)
        toD = "(" & a13 & "," & a12 & "," & a11 & ")"
    End Sub

    Private Sub CRPO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CRPO.Load
        Try

            If DIRECTPRINT = True Then
                PRINTDIRECTADVICE()
                Exit Sub
            End If

            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table

            getFromToDate()
            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With
            If FRMSTRING = "CashRegister" Or FRMSTRING = "BankRegister" Or FRMSTRING = "BankBookDetails" Then
                If FRMSTRING = "CashRegister" Then
                    crTables = rptcr.Database.Tables
                ElseIf FRMSTRING = "BankRegister" Then
                    crTables = rptBr.Database.Tables
                ElseIf FRMSTRING = "BankBookDetails" Then
                    crTables = rptBrd.Database.Tables
                End If
            ElseIf FRMSTRING = "DayBook" Or FRMSTRING = "DayBookDetails" Then
                If FRMSTRING = "DayBook" Then
                    crTables = rptdb.Database.Tables
                ElseIf FRMSTRING = "DayBookDetails" Then
                    crTables = rptdbd.Database.Tables
                End If

            ElseIf FRMSTRING = "PurchaseRegister" Then
                crTables = RPTPURREG.Database.Tables

            ElseIf FRMSTRING = "JvRegister" Then
                crTables = rptjvr.Database.Tables

            ElseIf FRMSTRING = "ExpenseRegister" Or FRMSTRING = "ExpenseRegisterDetails" Or FRMSTRING = "ExpenseRegisterMonthly" Then
                If FRMSTRING = "ExpenseRegister" Then
                    crTables = RPTEXP.Database.Tables
                ElseIf FRMSTRING = "ExpenseRegisterDetails" Then
                    crTables = RPTEXPD.Database.Tables
                ElseIf FRMSTRING = "ExpenseRegisterMonthly" Then
                    crTables = RPTEXPM.Database.Tables
                End If

            ElseIf FRMSTRING = "ContraRegister" Or FRMSTRING = "ContraRegisterDetails" Then
                If FRMSTRING = "ContraRegister" Then
                    crTables = rptconr.Database.Tables
                ElseIf FRMSTRING = "ContraRegisterDetails" Then
                    crTables = rptconrd.Database.Tables
                End If

            ElseIf FRMSTRING = "SaleRegister" Then
                crTables = RPTSALEREG.Database.Tables

            ElseIf FRMSTRING = "LedgerBook" Or FRMSTRING = "LedgerBookRunBal" Or FRMSTRING = "LedgerBookDetails" Or FRMSTRING = "LedgerBookConfirm" Or FRMSTRING = "LedgerBookConfirmSumm" Or FRMSTRING = "LedgerBookTFormat" Or FRMSTRING = "LedgerPartySumm" Or FRMSTRING = "LedgerBookMonthlyTypeSumm" Then
                If FRMSTRING = "LedgerBook" Then
                    crTables = RPTLEDGERAC.Database.Tables
                ElseIf FRMSTRING = "LedgerBookRunBal" Then
                    crTables = RPTLEDGERACRUNBAL.Database.Tables
                ElseIf FRMSTRING = "LedgerBookTFormat" Then
                    crTables = RPTLEDGERACTFORMAT.Database.Tables
                ElseIf FRMSTRING = "LedgerBookDetails" Then
                    crTables = RPTLEDGERACDETAILS.Database.Tables
                ElseIf FRMSTRING = "LedgerBookMonthlyTypeSumm" Then
                    crTables = RPTLEDGERMONTHLYTYPESUMM.Database.Tables
                ElseIf FRMSTRING = "LedgerBookConfirm" Then
                    crTables = rptlbc.Database.Tables
                ElseIf FRMSTRING = "LedgerBookConfirmSumm" Then
                    crTables = RPTLBCSUMM.Database.Tables
                ElseIf FRMSTRING = "LedgerPartySumm" Then
                    crTables = RPTLEDGERACSUMM.Database.Tables
                End If
            ElseIf FRMSTRING = "SaleRegisterMonthly" Or FRMSTRING = "PurchaseRegisterMonthly" Then
                If FRMSTRING = "SaleRegisterMonthly" Then
                    crTables = RPTSALEREGMONTHLY.Database.Tables
                ElseIf FRMSTRING = "PurchaseRegisterMonthly" Then
                    crTables = RPTPURREGMONTHLY.Database.Tables
                End If

            ElseIf FRMSTRING = "GROUPSUMMMONTHLY" Then
                crTables = RPTGROUPMONTHLY.Database.Tables

            ElseIf FRMSTRING = "purchasetax" Or FRMSTRING = "saletax" Or FRMSTRING = "purchasetaxdetail" Or FRMSTRING = "saletaxdetail" Then
                If FRMSTRING = "purchasetax" Then
                    crTables = rptptr.Database.Tables
                ElseIf FRMSTRING = "saletax" Then
                    crTables = rptstr.Database.Tables
                ElseIf FRMSTRING = "purchasetaxdetail" Then
                    crTables = rptptdr.Database.Tables
                ElseIf FRMSTRING = "saletaxdetail" Then
                    crTables = rptstdr.Database.Tables
                End If
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next
            '************************ END *******************

            If FRMSTRING = "CashRegister" Or FRMSTRING = "BankRegister" Or FRMSTRING = "BankBookDetails" Then
                If FRMSTRING = "CashRegister" Then
                    crParameterFieldDefinitions = rptcr.DataDefinition.ParameterFields
                ElseIf FRMSTRING = "BankRegister" Then
                    crParameterFieldDefinitions = rptBr.DataDefinition.ParameterFields
                ElseIf FRMSTRING = "BankBookDetails" Then
                    crParameterFieldDefinitions = rptBrd.DataDefinition.ParameterFields
                End If

                If FRMSTRING = "CashRegister" Then
                    crParameterDiscreteValue.Value = REG
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CASHNAME")
                ElseIf FRMSTRING = "BankRegister" Then
                    crParameterDiscreteValue.Value = REG
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@BANKNAME")
                End If

                If FRMSTRING <> "BankBookDetails" Then
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Clear()
                End If

                If FRMSTRING <> "BankBookDetails" Then
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    crParameterDiscreteValue.Value = CHECK
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CHECK")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    crParameterDiscreteValue.Value = CmpId
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CMPID")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    crParameterDiscreteValue.Value = Locationid
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@LOCATIONID")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    crParameterDiscreteValue.Value = YearId
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@YEARID")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                    crParameterDiscreteValue.Value = FROMDATE.Date
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@FROMDATE")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                    If FRMSTRING = "CashRegister" Then
                        rptcr.DataDefinition.FormulaFields("OPENING").Text = OPENING
                    ElseIf FRMSTRING = "BankRegister" Then
                        rptBr.DataDefinition.FormulaFields("OPENING").Text = OPENING
                    End If
                    crParameterDiscreteValue.Value = TODATE.Date
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@TODATE")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
                End If
                If FRMSTRING = "CashRegister" Then
                    CRPO.ReportSource = rptcr

                ElseIf FRMSTRING = "BankRegister" Then
                    CRPO.ReportSource = rptBr
                    rptBr.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    rptBr.DataDefinition.FormulaFields("NAME").Text = "'" & REG & "'"

                ElseIf FRMSTRING = "BankBookDetails" Then

                    rptBrd.DataDefinition.FormulaFields("OPENING").Text = "'" & OPENING & " " & OPENINGDRCR & "'"
                    rptBrd.DataDefinition.FormulaFields("CLOSINGBAL").Text = "'" & CLOSINGAMT & " " & CLOSINGDRCR & "'"
                    rptBrd.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

                    crParameterDiscreteValue.Value = PARTYNAME
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@NAME")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    crParameterDiscreteValue.Value = CmpId
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CMPID")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    crParameterDiscreteValue.Value = Locationid
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@LOCATIONID")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    crParameterDiscreteValue.Value = YearId
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@YEARID")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                    crParameterDiscreteValue.Value = FROMDATE.Date
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@FROMDATE")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                    crParameterDiscreteValue.Value = TODATE.Date
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@TODATE")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    CRPO.ReportSource = rptBrd
                End If
            ElseIf FRMSTRING = "DayBook" Or FRMSTRING = "DayBookDetails" Then


                If FRMSTRING = "DayBook" Then
                    strsearch = strsearch & " {@DATE} in date " & fromD & " to date " & toD
                    strsearch = strsearch & " and {REPORT_SP_DAYBOOK.CMPID}=" & CmpId & " AND {REPORT_SP_DAYBOOK.LOCATIONID}=" & Locationid & " AND {REPORT_SP_DAYBOOK.YEARID}=" & YearId

                    CRPO.SelectionFormula = strsearch

                    CRPO.ReportSource = rptdb
                ElseIf FRMSTRING = "DayBookDetails" Then
                    strsearch = strsearch & " ({@DATE} in date " & fromD & " to date " & toD & ")"
                    strsearch = strsearch & "  and {REPORT_SP_DAYBOOK_DETAILS.CMPID}=" & CmpId & " and {REPORT_SP_DAYBOOK_DETAILS.LOCATIONID}=" & Locationid & " and {REPORT_SP_DAYBOOK_DETAILS.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptdbd
                End If

                'for purchase
            ElseIf FRMSTRING = "PurchaseRegister" Then

                crParameterFieldDefinitions = RPTPURREG.DataDefinition.ParameterFields
                RPTPURREG.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

                crParameterDiscreteValue.Value = REG
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@REGNAME")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = CmpId
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CMPID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = Locationid
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@LOCATIONID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = YearId
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@YEARID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                crParameterDiscreteValue.Value = FROMDATE.Date
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@FROMDATE")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                crParameterDiscreteValue.Value = TODATE.Date
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@TODATE")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                CRPO.ReportSource = RPTPURREG


            ElseIf FRMSTRING = "GROUPSUMMMONTHLY" Then
                crParameterFieldDefinitions = RPTGROUPMONTHLY.DataDefinition.ParameterFields
                RPTGROUPMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTGROUPMONTHLY.DataDefinition.FormulaFields("OPENING").Text = OPENING

                crParameterDiscreteValue.Value = REG
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@NAME")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = CHECK
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CHECK")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = FROMDATE.Date
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@FROMDATE")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = TODATE.Date
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@TODATE")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = CmpId
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CMPID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = Locationid
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@LOCATIONID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = YearId
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@YEARID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                CRPO.ReportSource = RPTGROUPMONTHLY

            ElseIf FRMSTRING = "JvRegister" Then
                strsearch = strsearch & "({@DATE} in date " & fromD & " to date " & toD & ") and {JOURNALMASTER.JOURNAL_YEARID}=" & YearId
                If REG <> "" Then strsearch = strsearch & " And {REGISTERMASTER.REGISTER_NAME}='" & REG & "'"
                CRPO.SelectionFormula = strsearch
                rptjvr.DataDefinition.FormulaFields("PERIOD").Text = "' JOURNAL REGISTER - " & PERIOD & "'"
                CRPO.ReportSource = rptjvr


            ElseIf FRMSTRING = "ExpenseRegister" Or FRMSTRING = "ExpenseRegisterDetails" Or FRMSTRING = "ExpenseRegisterMonthly" Then
                If FRMSTRING = "ExpenseRegister" Then
                    If REG <> "" Then
                        strsearch = strsearch & " {REPORT_SP_EXPENSESUMMARY.regtype}='" & REG & "'"
                        strsearch = strsearch & " and ({@DATE} in date " & fromD & " to date " & toD & ")"
                    Else
                        strsearch = strsearch & "({@DATE} in date " & fromD & " to date " & toD & ")"
                    End If

                    strsearch = strsearch & " and {REPORT_SP_EXPENSESUMMARY.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch

                    RPTEXP.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    CRPO.ReportSource = RPTEXP
                ElseIf FRMSTRING = "ExpenseRegisterDetails" Then
                    If REG <> "" Then
                        strsearch = strsearch & " {REPORT_SP_EXPENSEDETAILS.regtype}='" & REG & "'"
                        strsearch = strsearch & " and ({@DATE} in date " & fromD & " to date " & toD & ")"
                    Else
                        strsearch = strsearch & " ({@DATE} in date " & fromD & " to date " & toD & ")"
                    End If

                    strsearch = strsearch & " and {REPORT_SP_EXPENSEDETAILS.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    RPTEXPD.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    CRPO.ReportSource = RPTEXPD

                ElseIf FRMSTRING = "ExpenseRegisterMonthly" Then

                End If


            ElseIf FRMSTRING = "ContraRegister" Or FRMSTRING = "ContraRegisterDetails" Then
                If FRMSTRING = "ContraRegister" Then
                    If REG <> "" Then
                        strsearch = strsearch & " {REPORT_SP_ContraSUMMARY.regtype}='" & REG & "'"
                        strsearch = strsearch & " and ({@DATE} in date " & fromD & " to date " & toD & ")"
                    Else
                        strsearch = strsearch & "({@DATE} in date " & fromD & " to date " & toD & ")"
                    End If

                    strsearch = strsearch & "  and {REPORT_SP_contraSUMMARY.CMPID}=" & CmpId & "  and {REPORT_SP_contraSUMMARY.LOCATIONID}=" & Locationid & "  and {REPORT_SP_contraSUMMARY.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch

                    CRPO.ReportSource = rptconr
                ElseIf FRMSTRING = "ContraRegisterDetails" Then
                    If REG <> "" Then
                        strsearch = strsearch & " {REPORT_SP_contra_DETAILS.regtype}='" & REG & "'"
                        strsearch = strsearch & " and ({@DATE} in date " & fromD & " to date " & toD & ")"
                    Else
                        strsearch = strsearch & " ({@DATE} in date " & fromD & " to date " & toD & ")"
                    End If

                    strsearch = strsearch & "  and {REPORT_SP_contra_DETAILS.CMPID}=" & CmpId & "  and {REPORT_SP_contra_DETAILS.LOCATIONID}=" & Locationid & "  and {REPORT_SP_contra_DETAILS.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptconrd
                End If


            ElseIf FRMSTRING = "SaleRegister" Then

                crParameterFieldDefinitions = RPTSALEREG.DataDefinition.ParameterFields
                RPTSALEREG.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

                crParameterDiscreteValue.Value = REG
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@REGNAME")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = CmpId
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CMPID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = Locationid
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@LOCATIONID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = YearId
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@YEARID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                crParameterDiscreteValue.Value = FROMDATE.Date
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@FROMDATE")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                crParameterDiscreteValue.Value = TODATE.Date
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@TODATE")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                CRPO.ReportSource = RPTSALEREG


            ElseIf FRMSTRING = "purchasetax" Or FRMSTRING = "saletax" Or FRMSTRING = "purchasetaxdetail" Or FRMSTRING = "saletaxdetail" Then
                If FRMSTRING = "purchasetax" Then
                    strsearch = strsearch & " {report_purchasetax.CMPID}=" & CmpId & " AND {report_purchasetax.LOCATIONID}=" & Locationid & " AND {report_purchasetax.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptptr
                ElseIf FRMSTRING = "saletax" Then
                    strsearch = strsearch & " {report_invoicetax.CMPID}=" & CmpId & " AND {report_invoicetax.LOCATIONID}=" & Locationid & " AND {report_invoicetax.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptstr
                ElseIf FRMSTRING = "purchasetaxdetail" Then
                    strsearch = strsearch & "  ({@date} in date " & fromD & " to date " & toD & ")"
                    strsearch = strsearch & " and {purchasemaster.bill_cmpid}=" & CmpId & " and {purchasemaster.bill_LOCATIONid}=" & Locationid & " and {purchasemaster.bill_YEARid}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptptdr
                ElseIf FRMSTRING = "saletaxdetail" Then
                    strsearch = strsearch & "  ({@DATE} in date " & fromD & " to date " & toD & ")"
                    strsearch = strsearch & " and {invoicemaster.invoice_cmpid}=" & CmpId & " and {invoicemaster.invoice_LOCATIONid}=" & Locationid & " and {invoicemaster.invoice_YEARid}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptstdr
                End If

                'ledger Book
            ElseIf FRMSTRING = "LedgerBook" Or FRMSTRING = "LedgerBookRunBal" Or FRMSTRING = "LedgerBookDetails" Or FRMSTRING = "LedgerBookConfirm" Or FRMSTRING = "LedgerBookConfirmSumm" Or FRMSTRING = "LedgerBookTFormat" Or FRMSTRING = "LedgerPartySumm" Or FRMSTRING = "LedgerBookMonthlyTypeSumm" Then

                If FRMSTRING = "LedgerBook" Then

                    RPTLEDGERAC.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    RPTLEDGERAC.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERAC.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERAC.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = RPTLEDGERAC
                    RPTLEDGERAC.GroupFooterSection7.SectionFormat.EnableNewPageAfter = NEWPAGE
                    RPTLEDGERAC.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                    RPTLEDGERAC.DataDefinition.FormulaFields("PANNO").Text = PANNO
                    If SHOWNARRATION = True Then RPTLEDGERAC.DataDefinition.FormulaFields("SHOWREMARKS").Text = 1

                ElseIf FRMSTRING = "LedgerBookRunBal" Then

                    RPTLEDGERACRUNBAL.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    RPTLEDGERACRUNBAL.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACRUNBAL.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACRUNBAL.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = RPTLEDGERACRUNBAL
                    RPTLEDGERACRUNBAL.GroupFooterSection7.SectionFormat.EnableNewPageAfter = NEWPAGE
                    RPTLEDGERACRUNBAL.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                    RPTLEDGERACRUNBAL.DataDefinition.FormulaFields("PANNO").Text = PANNO
                    If SHOWNARRATION = True Then RPTLEDGERACRUNBAL.DataDefinition.FormulaFields("SHOWREMARKS").Text = 1

                ElseIf FRMSTRING = "LedgerBookTFormat" Then

                    RPTLEDGERACTFORMAT.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    RPTLEDGERACTFORMAT.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACTFORMAT.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACTFORMAT.Subreports("LedgerBook_Debit").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACTFORMAT.Subreports("LedgerBook_Debit").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACTFORMAT.Subreports("LedgerBook_Credit").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACTFORMAT.Subreports("LedgerBook_Credit").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = RPTLEDGERACTFORMAT
                    RPTLEDGERACTFORMAT.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE

                ElseIf FRMSTRING = "LedgerBookDetails" Then

                    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = RPTLEDGERACDETAILS
                    RPTLEDGERACDETAILS.GroupFooterSection7.SectionFormat.EnableNewPageAfter = NEWPAGE
                    If SHOWNARRATION = True Then RPTLEDGERACDETAILS.DataDefinition.FormulaFields("SHOWREMARKS").Text = 1
                    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("PANNO").Text = PANNO


                ElseIf FRMSTRING = "LedgerBookMonthlyTypeSumm" Then

                    RPTLEDGERMONTHLYTYPESUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    RPTLEDGERMONTHLYTYPESUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERMONTHLYTYPESUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = RPTLEDGERMONTHLYTYPESUMM
                    RPTLEDGERMONTHLYTYPESUMM.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                    RPTLEDGERMONTHLYTYPESUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                    RPTLEDGERMONTHLYTYPESUMM.DataDefinition.FormulaFields("PRINTLETTER").Text = LETTERFORMAT

                ElseIf FRMSTRING = "LedgerBookConfirm" Then

                    rptlbc.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    rptlbc.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    rptlbc.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                    rptlbc.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptlbc
                    rptlbc.GroupFooterSection6.SectionFormat.EnableNewPageAfter = NEWPAGE
                    rptlbc.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                    If SHOWNARRATION = True Then rptlbc.DataDefinition.FormulaFields("SHOWREMARKS").Text = 1

                ElseIf FRMSTRING = "LedgerBookConfirmSumm" Then

                    RPTLBCSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    RPTLBCSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLBCSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = RPTLBCSUMM
                    RPTLBCSUMM.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                    RPTLBCSUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS

                ElseIf FRMSTRING = "LedgerPartySumm" Then

                    RPTLEDGERACSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    RPTLEDGERACSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(FROMDATE.Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(TODATE.Date, "MM/dd/yyyy") & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = RPTLEDGERACSUMM

                End If

            ElseIf FRMSTRING = "SaleRegisterMonthly" Or FRMSTRING = "PurchaseRegisterMonthly" Then
                If FRMSTRING = "SaleRegisterMonthly" Then
                    crParameterFieldDefinitions = RPTSALEREGMONTHLY.DataDefinition.ParameterFields
                ElseIf FRMSTRING = "PurchaseRegisterMonthly" Then
                    crParameterFieldDefinitions = RPTPURREGMONTHLY.DataDefinition.ParameterFields
                ElseIf FRMSTRING = "ContraRegisterMonthly" Then
                    crParameterFieldDefinitions = rptconrm.DataDefinition.ParameterFields
                ElseIf FRMSTRING = "JvRegisterMonthly" Then
                    crParameterFieldDefinitions = rptjvrm.DataDefinition.ParameterFields
                End If

                crParameterDiscreteValue.Value = REG
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@REGISTERNAME")
                crParameterValues = crParameterFieldDefinition.CurrentValues


                crParameterValues.Clear()
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                crParameterDiscreteValue.Value = CmpId
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CMPID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = Locationid
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@LOCATIONID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = YearId
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@YEARID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = CHECK
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CHECK")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = FROMDATE.Date
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@FROMDATE")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = TODATE.Date
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@TODATE")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                If FRMSTRING = "SaleRegisterMonthly" Then
                    CRPO.ReportSource = RPTSALEREGMONTHLY
                    RPTSALEREGMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

                ElseIf FRMSTRING = "PurchaseRegisterMonthly" Then
                    CRPO.ReportSource = RPTPURREGMONTHLY
                    RPTPURREGMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

                ElseIf FRMSTRING = "ContraRegisterMonthly" Then
                    CRPO.ReportSource = rptconrm
                ElseIf FRMSTRING = "JvRegisterMonthly" Then
                    CRPO.ReportSource = rptjvrm
                End If

            End If
            CRPO.Zoom(100)
            CRPO.Refresh()

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

            getFromToDate()
            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With


            Dim OBJ As New Object

            If FRMSTRING = "LedgerBook" Then
                OBJ = New LedgerBookAcReport
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                OBJ.GroupFooterSection7.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PANNO").Text = PANNO
                If SHOWNARRATION = True Then OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = 1

            ElseIf FRMSTRING = "LedgerBookRunBal" Then
                OBJ = New LedgerBookAcRunBalReport
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                OBJ.DATADEFINITION.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                OBJ.GroupFooterSection7.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PANNO").Text = PANNO
                If SHOWNARRATION = True Then OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = 1

            ElseIf FRMSTRING = "LedgerBookTFormat" Then
                OBJ = New LedgerBookAcReport_TReport
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                OBJ.Subreports("LedgerBook_Debit").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                OBJ.Subreports("LedgerBook_Debit").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                OBJ.Subreports("LedgerBook_Credit").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                OBJ.Subreports("LedgerBook_Credit").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                OBJ.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "LedgerBookDetails" Then

                OBJ = New LedgerBookAcReportDetails
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                OBJ.GroupFooterSection7.SectionFormat.EnableNewPageAfter = NEWPAGE
                If SHOWNARRATION = True Then OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = 1
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PANNO").Text = PANNO

            ElseIf FRMSTRING = "LedgerBookMonthlyTypeSumm" Then

                OBJ = New LedgerBookMonthlyTypeSummReport
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                OBJ.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.DataDefinition.FormulaFields("PRINTLETTER").Text = LETTERFORMAT

            ElseIf FRMSTRING = "LedgerBookConfirm" Then

                OBJ = New LedgerBookAcConfirmReport
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                OBJ.GroupFooterSection6.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                If SHOWNARRATION = True Then OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = 1

            ElseIf FRMSTRING = "LedgerBookConfirmSumm" Then
                OBJ = New LedgerBookConfirmationReport_Summ
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                OBJ.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS

            ElseIf FRMSTRING = "LedgerPartySumm" Then
                OBJ = New LedgerBookAcSummReport
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(FROMDATE.Date, "MM/dd/yyyy") & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(TODATE.Date, "MM/dd/yyyy") & "'"

            End If


            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = strsearch
            OBJ.REFRESH()

            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                If File.Exists(Application.StartupPath & "\" & PARTYNAME & "_LEDGER" & ".PDF") Then File.Delete(Application.StartupPath & "\" & PARTYNAME & "_LEDGER" & ".PDF")
                OBJ.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath & "\" & PARTYNAME & "_LEDGER" & ".PDF")
            End If
            OBJ.CLOSE()
            OBJ.DISPOSE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Try
            Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()
            Dim tempattachment As String = ""

            If frmstring = "CashRegister" Then
                tempattachment = "CASHREGISTER"
            ElseIf frmstring = "BankRegister" Then
                tempattachment = "BANKREGISTER"
            ElseIf frmstring = "BankBookDetails" Then
                tempattachment = "BANKREGISTER"
            ElseIf frmstring = "DayBook" Or frmstring = "DayBookDetails" Then
                tempattachment = "DAYBOOK"
            ElseIf frmstring = "PurchaseRegister" Or frmstring = "PurchaseRegisterMonthly" Then
                tempattachment = "PURCHASEREGISTER"
            ElseIf frmstring = "JvRegister" Or frmstring = "JvRegisterDetails" Then
                tempattachment = "JOURNALREGISTER"
            ElseIf frmstring = "ContraRegister" Or frmstring = "ContraRegisterDetails" Then
                tempattachment = "CONTRAREGISTER"
            ElseIf frmstring = "SaleRegister" Or frmstring = "SaleRegisterDetails" Or frmstring = "SaleRegisterMonthly" Then
                tempattachment = "SALEREGISTER"
            ElseIf frmstring = "LedgerBook" Or frmstring = "LedgerBookRunBal" Or frmstring = "LedgerBookDetails" Or frmstring = "LedgerBookConfirm" Or frmstring = "LedgerBookConfirmSumm" Or frmstring = "LedgerBookMonthlyTypeSumm" Or frmstring = "LedgerPartySumm" Then
                tempattachment = "LEDGERBOOK"
            ElseIf frmstring = "purchasetax" Or frmstring = "purchasetaxdetail" Then
                tempattachment = "PURCHASETAXREGISTER"
            ElseIf frmstring = "saletax" Or frmstring = "saletaxdetail" Then
                tempattachment = "SALETAXREGISTER"
            End If

            If PARTYNAME <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable = OBJCMN.search("ACC_EMAIL AS EMAILID", "", "LEDGERS", " and ACC_CMPNAME = '" & PARTYNAME & "' AND ACC_YEARID=" & YearId)
                If dt.Rows.Count > 0 Then
                    emailid = dt.Rows(0).Item(0).ToString
                End If
            End If

            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\" & tempattachment & ".PDF"
            If emailid <> "" Then objmail.cmbfirstadd.Text = emailid
            objmail.subject = tempattachment
            objmail.Show()
            objmail.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
        Windows.Forms.Cursor.Current = Cursors.Arrow
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions

            If frmstring = "CashRegister" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\CASHREGISTER.PDF"
                expo = rptcr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptcr.Export()

            ElseIf frmstring = "BankRegister" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\BANKREGISTER.PDF"
                expo = rptBr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptBr.Export()

            ElseIf frmstring = "BankBookDetails" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\BANKREGISTER.PDF"
                expo = rptBrd.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptBrd.Export()

            ElseIf frmstring = "DayBook" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\DAYBOOK.PDF"
                expo = rptdb.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptdb.Export()

            ElseIf frmstring = "DayBookDetails" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\DAYBOOK.PDF"
                expo = rptdbd.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptdbd.Export()

            ElseIf frmstring = "PurchaseRegister" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PURCHASEREGISTER.PDF"
                expo = RPTPURREG.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPURREG.Export()

            ElseIf frmstring = "PurchaseRegisterMonthly" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PURCHASEREGISTER.PDF"
                expo = RPTPURREGMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPURREGMONTHLY.Export()

            ElseIf frmstring = "JvRegister" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\JOURNALREGISTER.PDF"
                expo = rptjvr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptjvr.Export()

            ElseIf frmstring = "ContraRegister" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\CONTRAREGISTER.PDF"
                expo = rptconr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptconr.Export()

            ElseIf frmstring = "ContraRegisterDetails" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\CONTRAREGISTER.PDF"
                expo = rptconrd.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptconrd.Export()

            ElseIf frmstring = "SaleRegister" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SALEREGISTER.PDF"
                expo = RPTSALEREG.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALEREG.Export()

            ElseIf frmstring = "SaleRegisterMonthly" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SALEREGISTER.PDF"
                expo = RPTSALEREGMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALEREGMONTHLY.Export()


            ElseIf frmstring = "LedgerBook" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\LEDGERBOOK.PDF"
                expo = RPTLEDGERAC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLEDGERAC.Export()

            ElseIf frmstring = "LedgerBookRunBal" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\LEDGERBOOK.PDF"
                expo = RPTLEDGERACRUNBAL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLEDGERACRUNBAL.Export()

            ElseIf frmstring = "LedgerBookDetails" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\LEDGERBOOK.PDF"
                expo = RPTLEDGERACDETAILS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLEDGERACDETAILS.Export()

            ElseIf frmstring = "LedgerBookMonthlyTypeSumm" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\LEDGERBOOK.PDF"
                expo = RPTLEDGERMONTHLYTYPESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLEDGERMONTHLYTYPESUMM.Export()

            ElseIf frmstring = "LedgerBookConfirm" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\LEDGERBOOK.PDF"
                expo = rptlbc.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptlbc.Export()

            ElseIf frmstring = "LedgerBookConfirmSumm" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\LEDGERBOOK.PDF"
                expo = RPTLBCSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLBCSUMM.Export()

            ElseIf frmstring = "LedgerPartySumm" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\LEDGERBOOK.PDF"
                expo = RPTLEDGERACSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLEDGERACSUMM.Export()

            ElseIf frmstring = "purchasetax" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PURCHASETAXREGISTER.PDF"
                expo = rptptr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptptr.Export()

            ElseIf frmstring = "purchasetaxdetail" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PURCHASETAXREGISTER.PDF"
                expo = rptptdr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptptdr.Export()

            ElseIf frmstring = "saletax" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SALETAXREGISTER.PDF"
                expo = rptstr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptstr.Export()

            ElseIf frmstring = "saletaxdetail" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SALETAXREGISTER.PDF"
                expo = rptstdr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptstdr.Export()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TOOLLEDGER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLLEDGER.Click
        Try
            frmstring = "LedgerBookConfirm"
            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table

            getFromToDate()
            With crConnecttionInfo
                .ServerName = Servername
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With
            If frmstring = "LedgerBookConfirm" Then
                crTables = rptlbc.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next
            '************************ END *******************

            If frmstring = "LedgerBookConfirm" Then
                strsearch = ""
                strsearch = strsearch & " {REPORT_SP_LEDGERBOOK_DETAILS.name}='" & PARTYNAME & "'"
                strsearch = strsearch & "  and {REPORT_SP_LEDGERBOOK_DETAILS.CMPID}=" & CmpId & " and {REPORT_SP_LEDGERBOOK_DETAILS.LOCATIONID}=" & Locationid & " and {REPORT_SP_LEDGERBOOK_DETAILS.YEARID}=" & YearId
                CRPO.SelectionFormula = strsearch
                CRPO.ReportSource = rptlbc
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch Exp As LoadSaveReportException
            MsgBox("Incorrect path for loading report.", _
                    MsgBoxStyle.Critical, "Load Report Error")
        End Try
    End Sub

    Private Sub registerdesign_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class