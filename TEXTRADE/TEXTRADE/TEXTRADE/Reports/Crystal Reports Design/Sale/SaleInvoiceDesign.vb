
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports BL
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO



Public Class SaleInvoiceDesign

    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public SELECTEDRATE As String
    Public PLREMARKS As String
    Public PLDESC As String
    Public PERIOD As String
    Public LESSPER As Double
    Public COMMISSIONONRECDAMT As Boolean
    Public FROMDATE As Date
    Public TODATE As Date
    Public RECDAMT As Decimal
    Public COMMPER As Double
    Public PENDINGSO As String
    Public NEWPAGE As Boolean
    Public BALERATE As Double = 0.0
    Public ROLLRATE As Double = 0.0
    Public COVERNOTENO As Integer = 1




    Dim RPTPARTYDTLS As New InvoicePartyWiseDetails
    Dim RPTPARTYSUMM As New InvoicePartyWiseSummary
    Dim RPTPARTYPERCENT As New InvoicePartyWiseSummPercent
    Dim RPTJOBBERDTLS As New InvoiceAgentWiseDetails
    Dim RPTJOBBERSUMM As New InvoiceAgentWiseSummary
    Dim RPTITEMDTLS As New InvoiceItemWiseDetails
    Dim RPTITEMSUMM As New InvoiceItemWiseSummary
    Dim RPTCATEGORYSUMM As New InvoiceCategoryWiseSummary
    Dim RPTITEMPERCENT As New InvoiceItemWiseSummPercent
    Dim RPTQUALITYDTLS As New InvoiceQualityWiseDetails
    Dim RPTQUALITYSUMM As New InvoiceQualityWiseSummary
    Dim RPTDESIGNDTLS As New InvoiceDesignWiseDetails
    Dim RPTDESIGNSUMM As New InvoiceDesignWiseSummary
    Dim RPTSHADEDTLS As New InvoiceColorWiseDetails
    Dim RPTSHADESUMM As New InvoiceColorWiseSummary
    Dim RPTTRANSDTLS As New InvoiceTransWiseDetails
    Dim RPTTRANSSUMM As New InvoiceTransWiseSummary
    Dim RPTMONTHLY As New InvoiceMonthWise
    Dim RPTMONTHLYWITHRET As New InvoiceMonthWiseWithReturn
    Dim RPTDOCDTLS As New InvoiceDocumentWiseDetails
    Dim RPTREFDTLS As New InvoiceRefNoWiseDetails
    Dim RPTCOMM As New InvoiceAgentCommReport
    Dim RPTCOMMSUMM As New InvoiceAgentCommSummReport
    Dim RPTCOMMOP As New InvoiceAgentCommOpeningReport
    Dim RPTCOMMOPSUMM As New InvoiceAgentCommSummOpeningReport
    Dim RPTTERMWISE As New InvoiceTermWiseDetails
    Dim RPTENTRYWISE As New InvoiceEntryWiseReport
    Dim RPTENTRYWISESUMM As New InvoiceEntryWiseSummReport
    Dim RPTPARTYENTRYWISE As New InvoicePartyEntryWiseReport
    Dim RPTSALEREGISTERINDETAIL As New InvoiceRegisterInDetailsReport
    Dim RPTREGISTERENTRYWISE As New InvoiceRegisterEntryWiseReport
    Dim RPTREGISTERENTRYITEMWISE As New InvoiceRegisterEntryItemWiseReport
    Dim RPTAGENTENTRYWISE As New InvoiceAgentPartyEntryWiseReport
    Dim RPTLOCALTRANS As New InvoiceLocalTransChgsReport
    Dim RPTTRANSWTCALC As New InvoiceTransWtCalcReport
    Dim RPTMONTHLYPURSALE As New InvoiceMonthlyPurSale



    Dim RPTYOYSALE As New InvoiceYOYSaleReport
    Dim RPTYOYPARTYSALE As New InvoiceYOYPartySaleReport
    Dim RPTYOYITEMSALE As New InvoiceYOYItemSaleReport
    Dim RPTYOYAGENTSALE As New InvoiceYOYAgentSaleReport



    Dim RPTSOSTATUS As New SOStatusReport
    Dim RPTSOSTATUSRACK As New SOStatusWithRackReport
    Dim RPTSOSTATUSDTLS As New SOStatusDetailsReport
    Dim RPTSOSTATUSDATE As New SOStatusDateWiseReport
    Dim RPTSOCUTDTLS As New SOCutWiseDetails
    Dim RPTORDERSTOCK As New SOVsStockReport
    Dim RPTSOSTATUSITEM As New SOItemStatusReport
    Dim RPTSOSTATUSIMG As New SOStatusWithImgReport
    Dim RPTSODISPPER As New SODispatchPercentReport
    Dim RPTSOSTATUSITEMSMALL As New SOItemStatusSmallReport

    Dim RPTYARNSOSTATUSDTLS As New YarnSOStatusDetailsReport


    Dim RPTPOSTATUS As New POStatusReport
    Dim RPTPOSTATUSDTLS As New POStatusDetailsReport
    Dim RPTPOSTATUSDATE As New POStatusDateWiseReport
    Dim RPTPOCUTDTLS As New POCutWiseDetails
    Dim RPTPOSTATUSIMG As New POStatuswithImgReport
    Dim RPTPOSTATUSITEM As New POItemStatusReport

    Dim RPTYARNPOSTATUSDTLS As New YarnPOStatusDetailsReport


    Dim RPTPENDING As New GRNPendingCheck

    Dim RPTCOVERNOTE As New CoverNoteReport
    Dim RPTCOVERNOTEAGENT As New CoverNoteAgentReport
    Dim RPTPRICELIST As New ItemPriceListReport
    Dim RPTPRICELIST_MAHAVIRPOLYCOT As New ItemPriceListReport_MAHAVIRPOLYCOT

    Dim RPTCOVERNOTEMAIN As New CoverNoteReport_SNCM
    Dim RPTCOVERNOTEAGENTMAIN As New CoverNoteAgentReport_SNCM

    Dim RPTLABEL As New StickerLabelReport
    Dim RPT100X50 As New AddressPrint100X50
    Dim RPT100X75 As New AddressPrint100X75

    Public POSOFRMSTRING As String

    Public FORMULA As String
    Public PARTYNAME As String
    Public AGENTNAME As String

    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1

    Private Sub SaleInvoiceDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


            If DIRECTPRINT = True Then
                PRINTDIRECTLYTOPRINTER()
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor

            If POSOFRMSTRING = "PO" Then Me.Text = "Purchase Order"
            If POSOFRMSTRING = "SO" Then Me.Text = "Sale Order"

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

            If FRMSTRING = "PARTYWISEDTLS" Then crTables = RPTPARTYDTLS.Database.Tables
            If FRMSTRING = "PARTYWISESUMM" Then crTables = RPTPARTYSUMM.Database.Tables
            If FRMSTRING = "PARTYPERCENT" Then crTables = RPTPARTYPERCENT.Database.Tables

            If FRMSTRING = "JOBBERWISEDTLS" Then crTables = RPTJOBBERDTLS.Database.Tables
            If FRMSTRING = "JOBBERWISESUMM" Then crTables = RPTJOBBERSUMM.Database.Tables

            If FRMSTRING = "ITEMWISEDTLS" Then crTables = RPTITEMDTLS.Database.Tables
            If FRMSTRING = "ITEMWISESUMM" Then crTables = RPTITEMSUMM.Database.Tables
            If FRMSTRING = "CATEGORYWISESUMM" Then crTables = RPTCATEGORYSUMM.Database.Tables
            If FRMSTRING = "ITEMPERCENT" Then crTables = RPTITEMPERCENT.Database.Tables

            If FRMSTRING = "QUALITYWISEDTLS" Then crTables = RPTQUALITYDTLS.Database.Tables
            If FRMSTRING = "QUALITYWISESUMM" Then crTables = RPTQUALITYSUMM.Database.Tables

            If FRMSTRING = "DESIGNWISEDTLS" Then crTables = RPTDESIGNDTLS.Database.Tables
            If FRMSTRING = "DESIGNWISESUMM" Then crTables = RPTDESIGNSUMM.Database.Tables

            If FRMSTRING = "SHADEWISEDTLS" Then crTables = RPTSHADEDTLS.Database.Tables
            If FRMSTRING = "SHADEWISESUMM" Then crTables = RPTSHADESUMM.Database.Tables

            If FRMSTRING = "TRANSWISEDTLS" Then crTables = RPTTRANSDTLS.Database.Tables
            If FRMSTRING = "TRANSWISESUMM" Then crTables = RPTTRANSSUMM.Database.Tables
            If FRMSTRING = "MONTHLYPURSALE" Then crTables = RPTMONTHLYPURSALE.Database.Tables

            If FRMSTRING = "SOSTATUS" Then crTables = RPTSOSTATUS.Database.Tables
            If FRMSTRING = "SOSTATUSDTLS" Then crTables = RPTSOSTATUSDTLS.Database.Tables
            If FRMSTRING = "SOSTATUSDATE" Then crTables = RPTSOSTATUSDATE.Database.Tables
            If FRMSTRING = "CUTWISEDTLS" Then crTables = RPTSOCUTDTLS.Database.Tables
            If FRMSTRING = "SOSTATUSRACK" Then crTables = RPTSOSTATUSRACK.Database.Tables
            If FRMSTRING = "SOSTATUSITEM" Then crTables = RPTSOSTATUSITEM.Database.Tables
            If FRMSTRING = "SOSTATUSITEMSMALL" Then crTables = RPTSOSTATUSITEMSMALL.Database.Tables
            If FRMSTRING = "SOSTATUSIMG" Then crTables = RPTSOSTATUSIMG.Database.Tables
            If FRMSTRING = "SODISPPER" Then crTables = RPTSODISPPER.Database.Tables
            If FRMSTRING = "ORDERVSSTOCK" Then crTables = RPTORDERSTOCK.Database.Tables

            If FRMSTRING = "YARNSOSTATUSDTLS" Then crTables = RPTYARNSOSTATUSDTLS.Database.Tables


            If FRMSTRING = "POSTATUS" Then crTables = RPTPOSTATUS.Database.Tables
            If FRMSTRING = "POSTATUSDTLS" Then crTables = RPTPOSTATUSDTLS.Database.Tables
            If FRMSTRING = "POSTATUSDATE" Then crTables = RPTPOSTATUSDATE.Database.Tables
            If FRMSTRING = "POSTATUSIMG" Then crTables = RPTPOSTATUSIMG.Database.Tables
            If FRMSTRING = "POCUTWISEDTLS" Then crTables = RPTPOCUTDTLS.Database.Tables
            If FRMSTRING = "POSTATUSITEM" Then crTables = RPTPOSTATUSITEM.Database.Tables

            If FRMSTRING = "YARNPOSTATUSDTLS" Then crTables = RPTYARNPOSTATUSDTLS.Database.Tables


            If FRMSTRING = "YOYSALE" Then crTables = RPTYOYSALE.Database.Tables
            If FRMSTRING = "YOYPARTYSALE" Then crTables = RPTYOYPARTYSALE.Database.Tables
            If FRMSTRING = "YOYITEMSALE" Then crTables = RPTYOYITEMSALE.Database.Tables
            If FRMSTRING = "YOYAGENTSALE" Then crTables = RPTYOYAGENTSALE.Database.Tables


            'OLD CODE
            'If FRMSTRING = "POPARTYWISEDTLS" Then crTables = RPTPOPARTYDTLS.Database.Tables
            'If FRMSTRING = "POPARTYWISESUMM" Then crTables = RPTPOPARTYSUMM.Database.Tables
            'If FRMSTRING = "POJOBBERWISEDTLS" Then crTables = RPTPOJOBBERDTLS.Database.Tables
            'If FRMSTRING = "POJOBBERWISESUMM" Then crTables = RPTPOJOBBERSUMM.Database.Tables

            If FRMSTRING = "DOCUMENT" Then crTables = RPTDOCDTLS.Database.Tables
            If FRMSTRING = "REFFNO" Then crTables = RPTREFDTLS.Database.Tables

            If FRMSTRING = "COMMISSION" Then crTables = RPTCOMM.Database.Tables
            If FRMSTRING = "COMMSUMM" Then crTables = RPTCOMMSUMM.Database.Tables
            If FRMSTRING = "COMMISSIONOP" Then crTables = RPTCOMMOP.Database.Tables
            If FRMSTRING = "COMMOPSUMM" Then crTables = RPTCOMMOPSUMM.Database.Tables

            If FRMSTRING = "COVERNOTE" Then crTables = RPTCOVERNOTE.Database.Tables
            If FRMSTRING = "AGENTCOVERNOTE" Then crTables = RPTCOVERNOTEAGENT.Database.Tables

            If FRMSTRING = "MAINCOVERNOTE" Then crTables = RPTCOVERNOTEMAIN.Database.Tables
            If FRMSTRING = "MAINAGENTCOVERNOTE" Then crTables = RPTCOVERNOTEAGENTMAIN.Database.Tables

            If FRMSTRING = "LABEL" Then crTables = RPTLABEL.Database.Tables
            If FRMSTRING = "100X50" Then crTables = RPT100X50.Database.Tables
            If FRMSTRING = "100X75" Then crTables = RPT100X75.Database.Tables

            If FRMSTRING = "ENTRYWISE" Then crTables = RPTENTRYWISE.Database.Tables
            If FRMSTRING = "ENTRYWISESUMM" Then crTables = RPTENTRYWISESUMM.Database.Tables
            If FRMSTRING = "PARTYENTRYWISE" Then crTables = RPTPARTYENTRYWISE.Database.Tables
            If FRMSTRING = "REGISTERENTRYWISE" Then crTables = RPTREGISTERENTRYWISE.Database.Tables
            If FRMSTRING = "SALEREGISTERINDETAIL" Then crTables = RPTSALEREGISTERINDETAIL.Database.Tables
            If FRMSTRING = "REGISTERENTRYITEMWISE" Then crTables = RPTREGISTERENTRYITEMWISE.Database.Tables
            If FRMSTRING = "AGENTENTRYWISE" Then crTables = RPTAGENTENTRYWISE.Database.Tables
            If FRMSTRING = "TERMWISE" Then crTables = RPTTERMWISE.Database.Tables

            If FRMSTRING = "MONTHLY" Then crTables = RPTMONTHLY.Database.Tables
            If FRMSTRING = "MONTHLYWITHRET" Then crTables = RPTMONTHLYWITHRET.Database.Tables
            If FRMSTRING = "PENDING" Then crTables = RPTPENDING.Database.Tables

            If FRMSTRING = "PRICELIST" Then
                If ClientName = "MAHAVIRPOLYCOT" Then
                    crTables = RPTPRICELIST_MAHAVIRPOLYCOT.Database.Tables
                Else
                    crTables = RPTPRICELIST.Database.Tables
                End If
            End If

            If FRMSTRING = "LOCALTRANS" Then crTables = RPTLOCALTRANS.Database.Tables
            If FRMSTRING = "TRANSWTCALC" Then crTables = RPTTRANSWTCALC.Database.Tables
            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next


            crpo.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "PARTYWISEDTLS" Then
                crpo.ReportSource = RPTPARTYDTLS
                RPTPARTYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "PARTYPERCENT" Then
                crpo.ReportSource = RPTPARTYPERCENT
                RPTPARTYPERCENT.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                crpo.ReportSource = RPTPARTYSUMM
                RPTPARTYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                crpo.ReportSource = RPTJOBBERDTLS
                RPTJOBBERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                crpo.ReportSource = RPTJOBBERSUMM
                RPTJOBBERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "ITEMWISEDTLS" Then
                crpo.ReportSource = RPTITEMDTLS
                RPTITEMDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "ITEMWISESUMM" Then
                crpo.ReportSource = RPTITEMSUMM
                RPTITEMSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "CATEGORYWISESUMM" Then
                crpo.ReportSource = RPTCATEGORYSUMM
                RPTCATEGORYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "ITEMPERCENT" Then
                crpo.ReportSource = RPTITEMPERCENT
                RPTITEMPERCENT.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                crpo.ReportSource = RPTQUALITYDTLS
                RPTQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                crpo.ReportSource = RPTQUALITYSUMM
                RPTQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "DESIGNWISEDTLS" Then
                crpo.ReportSource = RPTDESIGNDTLS
                RPTDESIGNDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "DESIGNWISESUMM" Then
                crpo.ReportSource = RPTDESIGNSUMM
                RPTDESIGNSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SHADEWISEDTLS" Then
                crpo.ReportSource = RPTSHADEDTLS
                RPTSHADEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SHADEWISESUMM" Then
                crpo.ReportSource = RPTSHADESUMM
                RPTSHADESUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                crpo.ReportSource = RPTTRANSDTLS
                RPTTRANSDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                crpo.ReportSource = RPTTRANSSUMM
                RPTTRANSSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "MONTHLYPURSALE" Then
                crpo.ReportSource = RPTMONTHLYPURSALE
                RPTMONTHLYPURSALE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "MONTHLY" Then
                crpo.ReportSource = RPTMONTHLY
                RPTMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "MONTHLYWITHRET" Then
                crpo.ReportSource = RPTMONTHLYWITHRET
                RPTMONTHLYWITHRET.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "ENTRYWISE" Then
                crpo.ReportSource = RPTENTRYWISE
                RPTENTRYWISE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTENTRYWISE.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
            ElseIf FRMSTRING = "ENTRYWISESUMM" Then
                crpo.ReportSource = RPTENTRYWISESUMM
                RPTENTRYWISESUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTENTRYWISESUMM.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
            ElseIf FRMSTRING = "PARTYENTRYWISE" Then
                crpo.ReportSource = RPTPARTYENTRYWISE
                RPTPARTYENTRYWISE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTPARTYENTRYWISE.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
            ElseIf FRMSTRING = "SALEREGISTERINDETAIL" Then
                crpo.ReportSource = RPTSALEREGISTERINDETAIL
                RPTSALEREGISTERINDETAIL.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "REGISTERENTRYWISE" Then
                crpo.ReportSource = RPTREGISTERENTRYWISE
                RPTREGISTERENTRYWISE.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTREGISTERENTRYWISE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "REGISTERENTRYITEMWISE" Then
                crpo.ReportSource = RPTREGISTERENTRYITEMWISE
                RPTREGISTERENTRYITEMWISE.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTREGISTERENTRYITEMWISE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "AGENTENTRYWISE" Then
                crpo.ReportSource = RPTAGENTENTRYWISE
                RPTAGENTENTRYWISE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTAGENTENTRYWISE.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTAGENTENTRYWISE.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
            ElseIf FRMSTRING = "TERMWISE" Then
                crpo.ReportSource = RPTTERMWISE
                RPTTERMWISE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "PENDING" Then
                crpo.ReportSource = RPTPENDING
                RPTPENDING.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"


            ElseIf FRMSTRING = "YOYSALE" Then
                crpo.ReportSource = RPTYOYSALE
                RPTYOYSALE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "YOYPARTYSALE" Then
                crpo.ReportSource = RPTYOYPARTYSALE
                RPTYOYPARTYSALE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "YOYITEMSALE" Then
                crpo.ReportSource = RPTYOYITEMSALE
                RPTYOYITEMSALE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "YOYAGENTSALE" Then
                crpo.ReportSource = RPTYOYAGENTSALE
                RPTYOYAGENTSALE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"



            ElseIf FRMSTRING = "SOSTATUS" Then
                crpo.ReportSource = RPTSOSTATUS
                RPTSOSTATUS.DataDefinition.FormulaFields("TYPE").Text = "'" & PENDINGSO & "'"
                RPTSOSTATUS.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
            ElseIf FRMSTRING = "SOSTATUSDTLS" Then
                crpo.ReportSource = RPTSOSTATUSDTLS
                RPTSOSTATUSDTLS.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTSOSTATUSDTLS.DataDefinition.FormulaFields("SALEORDERONMTRS").Text = SALEORDERONMTRS
            ElseIf FRMSTRING = "SOSTATUSDATE" Then
                crpo.ReportSource = RPTSOSTATUSDATE
                RPTSOSTATUSDATE.DataDefinition.FormulaFields("TYPE").Text = "'" & PENDINGSO & "'"
            ElseIf FRMSTRING = "CUTWISEDTLS" Then
                crpo.ReportSource = RPTSOCUTDTLS
                RPTSOCUTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTSOCUTDTLS.Subreports(0).DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
            ElseIf FRMSTRING = "SOSTATUSRACK" Then
                'RPTSOSTATUSRACK.Subreports(0).RecordSelectionFormula = "{BARCODESTOCK.UNIT} = 'LUMP'"
                crpo.ReportSource = RPTSOSTATUSRACK
                RPTSOSTATUSRACK.DataDefinition.FormulaFields("TYPE").Text = "'" & PENDINGSO & "'"
                RPTSOSTATUSRACK.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTSOSTATUSRACK.Subreports(0).DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                Dim OBJCMN As New ClsCommon
                Dim TEMPUNIT As DataTable = OBJCMN.SEARCH(" DISTINCT SUBSTRING ((SELECT ',""' + UNIT_ABBR + '""' from DEFAULTSTOCKUNIT for XML PATH('')),2,20000)AS UNIT ", "", " DEFAULTSTOCKUNIT ", "")
                'If TEMPUNIT.Rows.Count > 0 Then RPTSOSTATUSRACK.Subreports(0).DataDefinition.FormulaFields("UNITS").Text = "'" & TEMPUNIT.Rows(0).Item("UNIT") & "'"
            ElseIf FRMSTRING = "SOSTATUSITEM" Then
                crpo.ReportSource = RPTSOSTATUSITEM
                RPTSOSTATUSITEM.DataDefinition.FormulaFields("TYPE").Text = "'" & PENDINGSO & "'"
                RPTSOSTATUSITEM.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
            ElseIf FRMSTRING = "SOSTATUSITEMSMALL" Then
                crpo.ReportSource = RPTSOSTATUSITEMSMALL
                RPTSOSTATUSITEMSMALL.DataDefinition.FormulaFields("TYPE").Text = "'" & PENDINGSO & "'"
                RPTSOSTATUSITEMSMALL.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
            ElseIf FRMSTRING = "SOSTATUSIMG" Then
                crpo.ReportSource = RPTSOSTATUSIMG
                RPTSOSTATUSIMG.DataDefinition.FormulaFields("TYPE").Text = "'" & PENDINGSO & "'"
                RPTSOSTATUSIMG.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
            ElseIf FRMSTRING = "SODISPPER" Then
                crpo.ReportSource = RPTSODISPPER
                RPTSODISPPER.DataDefinition.FormulaFields("TYPE").Text = "'" & PENDINGSO & "'"
                RPTSODISPPER.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
            ElseIf FRMSTRING = "ORDERVSSTOCK" Then
                crpo.ReportSource = RPTORDERSTOCK


            ElseIf FRMSTRING = "YARNSOSTATUSDTLS" Then
                crpo.ReportSource = RPTYARNSOSTATUSDTLS
                RPTYARNSOSTATUSDTLS.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTYARNSOSTATUSDTLS.DataDefinition.FormulaFields("SALEORDERONMTRS").Text = SALEORDERONMTRS



            ElseIf FRMSTRING = "POSTATUS" Then
                crpo.ReportSource = RPTPOSTATUS
            ElseIf FRMSTRING = "POSTATUSDTLS" Then
                crpo.ReportSource = RPTPOSTATUSDTLS
            ElseIf FRMSTRING = "POSTATUSDATE" Then
                crpo.ReportSource = RPTPOSTATUSDATE
            ElseIf FRMSTRING = "POCUTWISEDTLS" Then
                crpo.ReportSource = RPTPOCUTDTLS
                RPTPOCUTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "POSTATUSIMG" Then
                crpo.ReportSource = RPTPOSTATUSIMG
            ElseIf FRMSTRING = "POSTATUSITEM" Then
                crpo.ReportSource = RPTPOSTATUSITEM
                RPTPOSTATUSITEM.DataDefinition.FormulaFields("TYPE").Text = "'" & PENDINGSO & "'"
                RPTPOSTATUSITEM.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"


            ElseIf FRMSTRING = "YARNPOSTATUSDTLS" Then
                crpo.ReportSource = RPTYARNPOSTATUSDTLS



            ElseIf FRMSTRING = "DOCUMENT" Then
                crpo.ReportSource = RPTDOCDTLS
                RPTDOCDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "REFFNO" Then
                crpo.ReportSource = RPTREFDTLS
                RPTREFDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "COMMISSION" Then
                crpo.ReportSource = RPTCOMM
                RPTCOMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTCOMM.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTCOMM.DataDefinition.FormulaFields("COMMPER").Text = COMMPER
                If COMMISSIONONRECDAMT = True Then
                    RPTCOMM.DataDefinition.FormulaFields("CALCON").Text = "'" & RECDAMT & "'"
                End If
            ElseIf FRMSTRING = "COMMSUMM" Then
                crpo.ReportSource = RPTCOMMSUMM
                RPTCOMMSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTCOMMSUMM.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTCOMMSUMM.DataDefinition.FormulaFields("COMMPER").Text = COMMPER
                If COMMISSIONONRECDAMT = True Then
                    RPTCOMMSUMM.DataDefinition.FormulaFields("CALCON").Text = "'" & RECDAMT & "'"
                End If
            ElseIf FRMSTRING = "COMMISSIONOP" Then
                crpo.ReportSource = RPTCOMMOP
                RPTCOMMOP.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTCOMMOP.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTCOMMOP.DataDefinition.FormulaFields("COMMPER").Text = COMMPER
                If COMMISSIONONRECDAMT = True Then
                    RPTCOMMOP.DataDefinition.FormulaFields("CALCON").Text = "'" & RECDAMT & "'"
                End If
            ElseIf FRMSTRING = "COMMOPSUMM" Then
                crpo.ReportSource = RPTCOMMOPSUMM
                RPTCOMMOPSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTCOMMOPSUMM.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTCOMMOPSUMM.DataDefinition.FormulaFields("COMMPER").Text = COMMPER
                If COMMISSIONONRECDAMT = True Then
                    RPTCOMMOPSUMM.DataDefinition.FormulaFields("CALCON").Text = "'" & RECDAMT & "'"
                End If



            ElseIf FRMSTRING = "COVERNOTE" Then

                If ClientName = "RAJKRIPA" Then
                    RPTCOVERNOTE.PrintOptions.PaperSize = PaperSize.PaperA4
                    RPTCOVERNOTE.PrintOptions.PaperOrientation = PaperOrientation.Portrait
                End If
                If Val(COVERNOTENO) > 0 Then RPTCOVERNOTE.DataDefinition.FormulaFields("COVERNOTENO").Text = Val(COVERNOTENO)
                crpo.ReportSource = RPTCOVERNOTE

            ElseIf FRMSTRING = "AGENTCOVERNOTE" Then
                If Val(COVERNOTENO) > 0 Then RPTCOVERNOTEAGENT.DataDefinition.FormulaFields("COVERNOTENO").Text = Val(COVERNOTENO)
                crpo.ReportSource = RPTCOVERNOTEAGENT

            ElseIf FRMSTRING = "MAINCOVERNOTE" Then
                crpo.ReportSource = RPTCOVERNOTEMAIN
            ElseIf FRMSTRING = "MAINAGENTCOVERNOTE" Then
                crpo.ReportSource = RPTCOVERNOTEAGENTMAIN

            ElseIf FRMSTRING = "LABEL" Then
                crpo.ReportSource = RPTLABEL

            ElseIf FRMSTRING = "100X50" Then
                crpo.ReportSource = RPT100X50

            ElseIf FRMSTRING = "100X75" Then
                crpo.ReportSource = RPT100X75

            ElseIf FRMSTRING = "PRICELIST" Then
                If ClientName = "MAHAVIRPOLYCOT" Then
                    crpo.ReportSource = RPTPRICELIST_MAHAVIRPOLYCOT
                    RPTPRICELIST_MAHAVIRPOLYCOT.DataDefinition.FormulaFields("HEADER").Text = "'" & PERIOD.Replace(vbCrLf, "___") & "'"
                    RPTPRICELIST_MAHAVIRPOLYCOT.DataDefinition.FormulaFields("REMARKS").Text = "'" & PLREMARKS.Replace(vbCrLf, "___") & "'"
                    RPTPRICELIST_MAHAVIRPOLYCOT.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    RPTPRICELIST_MAHAVIRPOLYCOT.DataDefinition.FormulaFields("LESSPER").Text = Val(LESSPER)
                Else
                    crpo.ReportSource = RPTPRICELIST
                    RPTPRICELIST.DataDefinition.FormulaFields("HEADER").Text = "'" & PERIOD & "'"
                    RPTPRICELIST.DataDefinition.FormulaFields("HEADERDESC").Text = "'" & PLDESC & "'"
                    RPTPRICELIST.DataDefinition.FormulaFields("SELECTEDRATE").Text = "'" & SELECTEDRATE & "'"
                    RPTPRICELIST.DataDefinition.FormulaFields("REMARKS").Text = "'" & PLREMARKS & "'"
                    RPTPRICELIST.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                End If

            ElseIf FRMSTRING = "LOCALTRANS" Then
                crpo.ReportSource = RPTLOCALTRANS
                RPTLOCALTRANS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTLOCALTRANS.DataDefinition.FormulaFields("BALERATE").Text = BALERATE
                RPTLOCALTRANS.DataDefinition.FormulaFields("ROLLRATE").Text = ROLLRATE
            ElseIf FRMSTRING = "TRANSWTCALC" Then
                crpo.ReportSource = RPTTRANSWTCALC
                RPTTRANSWTCALC.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTTRANSWTCALC.DataDefinition.FormulaFields("BALERATE").Text = BALERATE
                RPTTRANSWTCALC.DataDefinition.FormulaFields("ROLLRATE").Text = ROLLRATE




            End If

            crpo.Zoom(100)
            crpo.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub


    Sub PRINTDIRECTLYTOPRINTER()
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


            Dim OBJ As New Object

            If FRMSTRING = "MAINCOVERNOTE" Then
                OBJ = New CoverNoteReport_SNCM
            ElseIf FRMSTRING = "MAINAGENTCOVERNOTE" Then
                OBJ = New CoverNoteAgentReport_SNCM
            End If



SKIPINVOICE:
            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = FORMULA
            OBJ.REFRESH()

            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions
                oDfDopt.DiskFileName = Application.StartupPath & "\COVERNOTE_" & COVERNOTENO & ".pdf"
                If File.Exists(oDfDopt.DiskFileName) Then FILE.Delete(oDfDopt.DiskFileName)
                expo = OBJ.ExportOptions
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub SaleInvoiceDesign_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click

        Dim emailid As String = ""
        Dim emailid1 As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()
        Dim tempattachment As String

        Dim objmail As New SendMail


        tempattachment = "MAINCOVERNOTE"
        objmail.subject = "Cover Note"

        Try
            ' Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()
            ' Dim TEMPATTACHMENT As String = "REPORT"
            ' Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\" & TEMPATTACHMENT & ".PDF"
            If emailid <> "" Then
                objmail.cmbfirstadd.Text = emailid
            End If
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
            oDfDopt.DiskFileName = Application.StartupPath & "\REPORT.pdf"

            If FRMSTRING = "PARTYWISEDTLS" Then
                expo = RPTPARTYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPARTYDTLS.Export()
            ElseIf FRMSTRING = "PARTYPERCENT" Then
                expo = RPTPARTYPERCENT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPARTYPERCENT.Export()
            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                expo = RPTPARTYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPARTYSUMM.Export()
            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                expo = RPTJOBBERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJOBBERDTLS.Export()
            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                expo = RPTJOBBERSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJOBBERSUMM.Export()
            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                expo = RPTTRANSDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTTRANSDTLS.Export()
            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                expo = RPTTRANSSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTTRANSSUMM.Export()
            ElseIf FRMSTRING = "MONTHLYPURSALE" Then
                expo = RPTMONTHLYPURSALE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMONTHLYPURSALE.Export()

            ElseIf FRMSTRING = "ITEMWISEDTLS" Then
                expo = RPTITEMDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMDTLS.Export()
            ElseIf FRMSTRING = "ITEMWISESUMM" Then
                expo = RPTITEMSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMSUMM.Export()
            ElseIf FRMSTRING = "CATEGORYWISESUMM" Then
                expo = RPTCATEGORYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMSUMM.Export()
            ElseIf FRMSTRING = "ITEMPERCENT" Then
                expo = RPTITEMPERCENT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMPERCENT.Export()
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                expo = RPTQUALITYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTQUALITYDTLS.Export()
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                expo = RPTQUALITYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTQUALITYSUMM.Export()
            ElseIf FRMSTRING = "DESIGNWISEDTLS" Then
                expo = RPTDESIGNDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDESIGNDTLS.Export()
            ElseIf FRMSTRING = "DESIGNWISESUMM" Then
                expo = RPTDESIGNSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDESIGNSUMM.Export()
            ElseIf FRMSTRING = "SHADEWISEDTLS" Then
                expo = RPTSHADEDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSHADEDTLS.Export()
            ElseIf FRMSTRING = "SHADEWISESUMM" Then
                expo = RPTSHADESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSHADESUMM.Export()
            ElseIf FRMSTRING = "MONTHLY" Then
                expo = RPTMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMONTHLY.Export()
            ElseIf FRMSTRING = "MONTHLYWITHRET" Then
                expo = RPTMONTHLYWITHRET.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMONTHLYWITHRET.Export()
            ElseIf FRMSTRING = "ENTRYWISE" Then
                expo = RPTENTRYWISE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTENTRYWISE.Export()
            ElseIf FRMSTRING = "ENTRYWISESUMM" Then
                expo = RPTENTRYWISESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTENTRYWISESUMM.Export()
            ElseIf FRMSTRING = "PARTYENTRYWISE" Then
                expo = RPTPARTYENTRYWISE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPARTYENTRYWISE.Export()
            ElseIf FRMSTRING = "SALEREGISTERINDETAIL" Then
                expo = RPTSALEREGISTERINDETAIL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALEREGISTERINDETAIL.Export()
            ElseIf FRMSTRING = "REGISTERENTRYWISE" Then
                expo = RPTREGISTERENTRYWISE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTREGISTERENTRYWISE.Export()
            ElseIf FRMSTRING = "REGISTERENTRYITEMWISE" Then
                expo = RPTREGISTERENTRYITEMWISE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTREGISTERENTRYITEMWISE.Export()
            ElseIf FRMSTRING = "AGENTENTRYWISE" Then
                expo = RPTAGENTENTRYWISE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTAGENTENTRYWISE.Export()
            ElseIf FRMSTRING = "TERMWISE" Then
                expo = RPTTERMWISE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTTERMWISE.Export()
            ElseIf FRMSTRING = "PENDING" Then
                expo = RPTPENDING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPENDING.Export()


            ElseIf FRMSTRING = "YOYSALE" Then
                expo = RPTYOYSALE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYOYSALE.Export()
            ElseIf FRMSTRING = "YOYPARTYSALE" Then
                expo = RPTYOYPARTYSALE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYOYPARTYSALE.Export()
            ElseIf FRMSTRING = "YOYITEMSALE" Then
                expo = RPTYOYITEMSALE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYOYITEMSALE.Export()
            ElseIf FRMSTRING = "YOYAGENTSALE" Then
                expo = RPTYOYAGENTSALE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYOYAGENTSALE.Export()



            ElseIf FRMSTRING = "SOSTATUS" Then
                expo = RPTSOSTATUS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSOSTATUS.Export()
            ElseIf FRMSTRING = "SOSTATUSDTLS" Then
                expo = RPTSOSTATUSDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSOSTATUSDTLS.Export()
            ElseIf FRMSTRING = "SOSTATUSDATE" Then
                expo = RPTSOSTATUSDATE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSOSTATUSDATE.Export()
            ElseIf FRMSTRING = "CUTWISEDTLS" Then
                expo = RPTSOCUTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSOCUTDTLS.Export()
            ElseIf FRMSTRING = "SOSTATUSRACK" Then
                expo = RPTSOSTATUSRACK.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSOSTATUSRACK.Export()
            ElseIf FRMSTRING = "SOSTATUSITEM" Then
                expo = RPTSOSTATUSITEM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSOSTATUSITEM.Export()
            ElseIf FRMSTRING = "SOSTATUSITEMSMALL" Then
                expo = RPTSOSTATUSITEMSMALL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSOSTATUSITEMSMALL.Export()
            ElseIf FRMSTRING = "SOSTATUSIMG" Then
                expo = RPTSOSTATUSIMG.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSOSTATUSIMG.Export()
            ElseIf FRMSTRING = "SODISPPER" Then
                expo = RPTSODISPPER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSODISPPER.Export()
            ElseIf FRMSTRING = "ORDERVSSTOCK" Then
                expo = RPTORDERSTOCK.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTORDERSTOCK.Export()


            ElseIf FRMSTRING = "YARNSOSTATUSDTLS" Then
                expo = RPTYARNSOSTATUSDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYARNSOSTATUSDTLS.Export()



            ElseIf FRMSTRING = "POSTATUS" Then
                expo = RPTPOSTATUS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPOSTATUS.Export()
            ElseIf FRMSTRING = "POSTATUSDTLS" Then
                expo = RPTPOSTATUSDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPOSTATUSDTLS.Export()
            ElseIf FRMSTRING = "POSTATUSDATE" Then
                expo = RPTPOSTATUSDATE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPOSTATUSDATE.Export()
            ElseIf FRMSTRING = "POCUTWISEDTLS" Then
                expo = RPTPOCUTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPOCUTDTLS.Export()
            ElseIf FRMSTRING = "POSTATUSIMG" Then
                expo = RPTPOSTATUSIMG.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPOSTATUSIMG.Export()
            ElseIf FRMSTRING = "POSTATUSITEM" Then
                expo = RPTPOSTATUSITEM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPOSTATUSITEM.Export()


            ElseIf FRMSTRING = "YARNPOSTATUSDTLS" Then
                expo = RPTYARNPOSTATUSDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYARNPOSTATUSDTLS.Export()


            ElseIf FRMSTRING = "DOCUMENT" Then
                expo = RPTDOCDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDOCDTLS.Export()
            ElseIf FRMSTRING = "REFFNO" Then
                expo = RPTREFDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTREFDTLS.Export()

            ElseIf FRMSTRING = "COMMISSION" Then
                expo = RPTCOMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCOMM.Export()
            ElseIf FRMSTRING = "COMMSUMM" Then
                expo = RPTCOMMSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCOMMSUMM.Export()
            ElseIf FRMSTRING = "COMMISSIONOP" Then
                expo = RPTCOMMOP.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCOMMOP.Export()
            ElseIf FRMSTRING = "COMMOPSUMM" Then
                expo = RPTCOMMOPSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCOMMOPSUMM.Export()


            ElseIf FRMSTRING = "PRICELIST" Then
                If ClientName = "MAHAVIRPOLYCOT" Then
                    expo = RPTPRICELIST_MAHAVIRPOLYCOT.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTPRICELIST_MAHAVIRPOLYCOT.Export()
                Else
                    expo = RPTPRICELIST.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTPRICELIST.Export()
                End If

            ElseIf FRMSTRING = "LOCALTRANS" Then
                expo = RPTLOCALTRANS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLOCALTRANS.Export()

            ElseIf FRMSTRING = "TRANSWTCALC" Then
                expo = RPTTRANSWTCALC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTTRANSWTCALC.Export()


            ElseIf FRMSTRING = "COVERNOTE" Then
                expo = RPTCOVERNOTE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCOVERNOTE.Export()

            ElseIf FRMSTRING = "AGENTCOVERNOTE" Then
                expo = RPTCOVERNOTEAGENT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCOVERNOTEAGENT.Export()


            ElseIf FRMSTRING = "MAINCOVERNOTE" Then
                expo = RPTCOVERNOTEMAIN.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCOVERNOTEMAIN.Export()

            ElseIf FRMSTRING = "MAINAGENTCOVERNOTE" Then
                expo = RPTCOVERNOTEAGENTMAIN.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCOVERNOTEAGENTMAIN.Export()


            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\REPORT.PDF")
            OBJWHATSAPP.FILENAME.Add("REPORT.pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class