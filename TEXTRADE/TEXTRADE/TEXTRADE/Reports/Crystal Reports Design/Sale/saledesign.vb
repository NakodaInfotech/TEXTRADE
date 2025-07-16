
Imports BL
Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO
Imports DevExpress.Pdf
Imports DevExpress.XtraEditors
Imports TEXTRADE.DevExpressTest.Docs.Demos
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography

Public Class saledesign


    Private Class CertItem
        Private privateName As String
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Set(ByVal value As String)
                privateName = value
            End Set
        End Property

        Private privateCert As X509Certificate2

        Public Property Cert() As X509Certificate2
            Get
                Return privateCert
            End Get
            Set(ByVal value As X509Certificate2)
                privateCert = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Private Shared Function SelectCertificates() As X509Certificate2Collection
        Dim store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
        store.Open(OpenFlags.ReadOnly Or OpenFlags.OpenExistingOnly)
        Dim collection As X509Certificate2Collection = store.Certificates
        collection = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, True)
        collection = collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, True)
        Return collection
    End Function

    Dim RPTPROFORMAINVOICE As New ProformaInvoiceReport_TOTALLEFT


    Dim RPTINVOICE_BUYER As New InvoiceReport_Export_Buyer
    Dim RPTINVOICE_CUSTOM As New InvoiceReport_Export_Custom
    Dim RPTINVOICE_EXPGST As New InvoiceReport_Export_GST

    Dim RPTINVOICE_LINE As New InvoiceReport
    Dim RPTINVOICE_SAFFRON As New InvoiceReport_SAFFRON
    Dim RPTINVOICE_PURPLE As New InvoiceReport_PURPLE
    Dim RPTINV_TOTAL As New InvoiceReport_TOTAL
    Dim RPTINVOICE_CC As New InvoiceReport_CC
    Dim RPTINVOICE_RETAIL As New InvoiceReport_Retail
    Dim RPTINVOICE_SVS As New InvoiceReport_SVS
    Dim RPTINVOICE_SHALIBHADRA As New InvoiceReport_SHALIBHADRA
    Dim RPTINVOICE_SKF As New InvoiceReport_SKF
    Dim RPTINVOICE_MANINATH As New InvoiceReport_MANINATH
    Dim RPTINVOICE_SANGHVI As New InvoiceReport_SANGHVI
    Dim RPTINVOICE_KDFAB As New InvoiceReport_KDFAB

    Dim RPTINVOICE_GELATO As New InvoiceReport_GELATO
    Dim RPTINVOICE_GELATOIGST As New InvoiceReport_GELATOIGST

    Dim RPTINVOICE_KOTHARI As New InvoiceReport_KOTHARI
    Dim RPTINVOICE_MAHAVIR As New InvoiceReport_MAHAVIR
    'Dim RPTINVOICE_KCRAYON As New InvoiceReport_KCRAYON
    Dim RPTINVOICE_POONAMKREEVE As New InvoiceReport_POONAMKREEVE
    Dim RPTINVOICE_POONAMKREEVEIGST As New InvoiceReport_POONAMKREEVEIGST
    Dim RPTINVOICE_BARKHASHUBHI As New InvoiceReport_BARKHASHUBHI
    Dim RPTINVOICE_DRDRAPES As New InvoiceReport_DRDRAPES
    Dim RPTINVOICE_SAKNVAHAN As New InvoiceReport_SAKNVAHAN
    Dim RPTINVOICE_CHANDRA As New InvoiceReport_CHANDRA
    Dim RPTINVOICE_DETLINE As New InvoiceReport_DETLINE
    Dim RPTINVOICE_TOTALTRANS As New InvoiceReport_TOTALTRANS
    Dim RPTINVOICE_TOTALTRANSA4 As New InvoiceReport_TOTALTRANSA4
    Dim RPTINVOICE_TOTALLEFT As New InvoiceReport_TOTALLEFT
    Dim RPTINVOICE_MITESHBHAI As New InvoiceReport_MITESHBHAI
    Dim RPTINVOICE_SBASOFTAS As New InvoiceReport_SBASOFTAS
    Dim RPTINVOICE_AVIS As New InvoiceReport_AVIS
    Dim RPTINVOICE_MOMAI As New InvoiceReport_MOMAI
    Dim RPTINVOICE_SIMPLEX As New InvoiceReport_SIMPLEX
    Dim RPTINVOICE_SNCM As New InvoiceReport_SNCM
    Dim RPTINVOICE_ABHEE As New InvoiceReport_ABHEE

    Dim RPTINVOICE_SUPRIYA As New InvoiceReport_SUPRIYA
    Dim RPTINVOICE_YARNDO As New InvoiceReport_YARNDO


    Dim RPTINVOICE_NAKODAINFOTECH As New InvoiceReport_NI


    Dim RPTPARTYDTLS As New InvoicePartyWiseDetails
    Dim RPTPARTYSUMM As New InvoicePartyWiseSummary
    Dim RPTAGENTDTLS As New InvoiceAgentWiseDetails
    Dim RPTAGENTSUMM As New InvoiceAgentWiseSummary
    Dim RPTITEMDTLS As New InvoiceItemWiseDetails
    Dim RPTITEMSUMM As New InvoiceItemWiseSummary
    Dim RPTCATEGORYSUMM As New InvoiceCategoryWiseSummary
    Dim RPTQUALITYDTLS As New InvoiceQualityWiseDetails
    Dim RPTQUALITYSUMM As New InvoiceQualityWiseSummary
    Dim RPTDESIGNDTLS As New InvoiceDesignWiseDetails
    Dim RPTDESIGNSUMM As New InvoiceDesignWiseSummary
    Dim RPTSHADEDTLS As New InvoiceColorWiseDetails
    Dim RPTSHADESUMM As New InvoiceColorWiseSummary
    Dim RPTTRANSDTLS As New InvoiceTransWiseDetails
    Dim RPTTRANSSUMM As New InvoiceTransWiseSummary

    Public WHERECLAUSE As String
    Public IGSTFORMAT As Boolean = False
    Public BLANKPAPER As Boolean = False
    Public PERIOD As String
    Public strsumm As String
    Public FRMSTRING As String
    Public registername As String
    Public FROMDATE As Date
    Public TODATE As Date
    Public strsearch As String
    Public PARTYNAME As String
    Public AGENTNAME As String
    Public INVOICECOPYNAME As String
    Public INVOICETRANS As Boolean
    Public INVOICERETAIL As Boolean
    Public INVNO As Integer
    Public COMM As Double
    Public PRINTSETTING As Object = Nothing
    Public PARTYCHANGEADD As String = ""

    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String
    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Dim tempattachment As String
    Public NOOFCOPIES As Integer = 1
    Public PDFWITHDIGITALSIGN As Boolean = False

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If ALLOWDIGITALSIGN = True AndAlso MsgBox("Send Pdf with Digital Signature?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then PDFWITHDIGITALSIGN = True
            Transfer()

            If FRMSTRING = "" Then
                tempattachment = "SALEDETAILS"
            ElseIf FRMSTRING = "INVOICE" Then
                tempattachment = "INVOICE"
            ElseIf FRMSTRING = "PROFORMAINVOICE" Then
                tempattachment = "PROFORMA"
            ElseIf FRMSTRING = "YARNDO" Then
                tempattachment = "YARNDO"
            Else
                tempattachment = "SALESUMMARY"
            End If

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = PARTYNAME
            OBJWHATSAPP.AGENTNAME = AGENTNAME
            If PDFWITHDIGITALSIGN = True Then
                OBJWHATSAPP.PATH.Add(Application.StartupPath & "\INVOICEPDF\INVOICE_" & Val(INVNO) & "-" & Val(AccFrom.Year) & ".pdf")
                OBJWHATSAPP.FILENAME.Add("INVOICE_" & Val(INVNO) & "-" & Val(AccFrom.Year) & ".pdf")
            Else
                OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & tempattachment & ".PDF")
                OBJWHATSAPP.FILENAME.Add(tempattachment & ".pdf")
            End If
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

    Private Sub saledesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If DIRECTPRINT = True Then
                PRINTDIRECTLYTOPRINTER()
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


            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With


            If FRMSTRING = "PARTYWISEDTLS" Then crTables = RPTPARTYDTLS.Database.Tables
            If FRMSTRING = "PARTYWISESUMM" Then crTables = RPTPARTYSUMM.Database.Tables

            If FRMSTRING = "JOBBERWISEDTLS" Then crTables = RPTAGENTDTLS.Database.Tables
            If FRMSTRING = "JOBBERWISESUMM" Then crTables = RPTAGENTSUMM.Database.Tables

            If FRMSTRING = "ITEMWISEDTLS" Then crTables = RPTITEMDTLS.Database.Tables
            If FRMSTRING = "ITEMWISESUMM" Then crTables = RPTITEMSUMM.Database.Tables
            If FRMSTRING = "CATEGORYWISESUMM" Then crTables = RPTCATEGORYSUMM.Database.Tables

            If FRMSTRING = "QUALITYWISEDTLS" Then crTables = RPTQUALITYDTLS.Database.Tables
            If FRMSTRING = "QUALITYWISESUMM" Then crTables = RPTQUALITYSUMM.Database.Tables

            If FRMSTRING = "DESIGNWISEDTLS" Then crTables = RPTDESIGNDTLS.Database.Tables
            If FRMSTRING = "DESIGNWISESUMM" Then crTables = RPTDESIGNSUMM.Database.Tables

            If FRMSTRING = "SHADEWISEDTLS" Then crTables = RPTSHADEDTLS.Database.Tables
            If FRMSTRING = "SHADEWISESUMM" Then crTables = RPTSHADESUMM.Database.Tables

            If FRMSTRING = "TRANSWISEDTLS" Then crTables = RPTTRANSDTLS.Database.Tables
            If FRMSTRING = "TRANSWISESUMM" Then crTables = RPTTRANSSUMM.Database.Tables


            If FRMSTRING = "EXPBUYER" Then
                crTables = RPTINVOICE_BUYER.Database.Tables
                Me.Text = "Buyer Invoice"
            End If
            If FRMSTRING = "EXPCUSTOM" Then
                crTables = RPTINVOICE_CUSTOM.Database.Tables
                Me.Text = "Custom Invoice"
            End If
            If FRMSTRING = "EXPGST" Then
                crTables = RPTINVOICE_EXPGST.Database.Tables
                Me.Text = "GST Invoice"
            End If

            If FRMSTRING = "YARNDO" Then
                crTables = RPTINVOICE_YARNDO.Database.Tables
                Me.Text = "Yarn DO"
            End If

            If FRMSTRING = "PROFORMAINVOICE" Then crTables = RPTPROFORMAINVOICE.Database.Tables


            If FRMSTRING = "INVOICE" Then

                'CODE DONE BY GULKIT
                If INVOICETRANS = True Then
                    If TRANSPORTCOPYA4 Then
                        crTables = RPTINVOICE_TOTALTRANSA4.Database.Tables
                    Else
                        crTables = RPTINVOICE_TOTALTRANS.Database.Tables
                    End If
                    GoTo SKIPINVOICE
                End If


                'CODE DONE BY GULKIT
                If INVOICERETAIL = True Then
                    crTables = RPTINVOICE_RETAIL.Database.Tables
                    GoTo SKIPINVOICE
                End If


                If ClientName = "SKF" Then
                    crTables = RPTINVOICE_SKF.Database.Tables
                ElseIf ClientName = "AVIS" Then
                    crTables = RPTINVOICE_AVIS.Database.Tables
                ElseIf ClientName = "SAFFRONOFF" Then
                    crTables = RPTINVOICE_SAFFRON.Database.Tables
                ElseIf ClientName = "SAFFRON" Then
                    crTables = RPTINVOICE_SAFFRON.Database.Tables
                ElseIf ClientName = "PURPLE" Then
                    crTables = RPTINVOICE_PURPLE.Database.Tables
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    crTables = RPTINVOICE_CC.Database.Tables
                ElseIf ClientName = "SVS" Then
                    crTables = RPTINVOICE_SVS.Database.Tables
                ElseIf ClientName = "MANINATH" Then
                    crTables = RPTINVOICE_MANINATH.Database.Tables
                ElseIf ClientName = "JURON" Or ClientName = "MOHAN" Then
                    If INVOICESCREENTYPE = "LINE GST" Then crTables = RPTINVOICE_LINE.Database.Tables Else crTables = RPTINV_TOTAL.Database.Tables
                ElseIf ClientName = "MOMAI" Then
                    crTables = RPTINVOICE_MOMAI.Database.Tables
                ElseIf ClientName = "MAHAVIR" Then
                    crTables = RPTINVOICE_MAHAVIR.Database.Tables
                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                    crTables = RPTINVOICE_KOTHARI.Database.Tables
                ElseIf ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "MONOGRAM" Then
                    crTables = RPTINVOICE_BARKHASHUBHI.Database.Tables
                ElseIf ClientName = "DRDRAPES" Then
                    crTables = RPTINVOICE_DRDRAPES.Database.Tables
                ElseIf ClientName = "SAKARIA" Or ClientName = "NVAHAN" Then
                    crTables = RPTINVOICE_SAKNVAHAN.Database.Tables
                ElseIf ClientName = "CHANDRA" Then
                    crTables = RPTINVOICE_CHANDRA.Database.Tables
                ElseIf ClientName = "DETLINE" Then
                    crTables = RPTINVOICE_DETLINE.Database.Tables
                ElseIf ClientName = "KDFAB" Then
                    crTables = RPTINVOICE_KDFAB.Database.Tables
                ElseIf ClientName = "MITESHBHAI" Then
                    crTables = RPTINVOICE_MITESHBHAI.Database.Tables
                ElseIf ClientName = "SBA" Or ClientName = "SOFTAS" Then
                    crTables = RPTINVOICE_SBASOFTAS.Database.Tables
                ElseIf ClientName = "POONAM" Or ClientName = "KREEVE" Then
                    crTables = RPTINVOICE_POONAMKREEVE.Database.Tables
                    'If IGSTFORMAT = False Then
                    '    crTables = RPTINVOICE_POONAMKREEVE.Database.Tables
                    'Else
                    '    crTables = RPTINVOICE_POONAMKREEVEIGST.Database.Tables
                    'End If
                ElseIf ClientName = "GELATO" Then
                    If IGSTFORMAT = False Then
                        crTables = RPTINVOICE_GELATO.Database.Tables
                    Else
                        crTables = RPTINVOICE_GELATOIGST.Database.Tables
                    End If
                ElseIf ClientName = "SANGHVI" Then
                    crTables = RPTINVOICE_SANGHVI.Database.Tables
                ElseIf ClientName = "SUPRIYA" Then
                    crTables = RPTINVOICE_SUPRIYA.Database.Tables
                ElseIf ClientName = "SIMPLEX" Then
                    crTables = RPTINVOICE_SIMPLEX.Database.Tables
                ElseIf ClientName = "SNCM" Then
                    crTables = RPTINVOICE_SNCM.Database.Tables
                ElseIf ClientName = "NAKODAINFOTECH" Then
                    crTables = RPTINVOICE_NAKODAINFOTECH.Database.Tables
                ElseIf ClientName = "ABHEE" Then
                    crTables = RPTINVOICE_ABHEE.Database.Tables
                Else
                    crTables = RPTINVOICE_TOTALLEFT.Database.Tables
                End If


            End If

SKIPINVOICE:

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next


            '************************ END *******************
            getFromToDate()

            If FRMSTRING <> "INVOICE" And FRMSTRING <> "EXPBUYER" And FRMSTRING <> "EXPCUSTOM" And FRMSTRING <> "EXPGST" And FRMSTRING <> "PROFORMAINVOICE" And FRMSTRING <> "YARNDO" Then
                crParameterDiscreteValue.Value = CmpId
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CMPID")
                crParameterValues = crParameterFieldDefinition.CurrentValues

                crParameterValues.Clear()
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
            End If

            CRPO.SelectionFormula = strsearch

            If FRMSTRING = "INVOICE" Then

                'CODE DONE BY GULKIT
                If INVOICETRANS = True Then
                    If TRANSPORTCOPYA4 Then
                        CRPO.ReportSource = RPTINVOICE_TOTALTRANSA4
                        If ClientName = "VSTRADERS" Or ClientName = "SIMPLEX" Or ClientName = "CHINTAN" Or ClientName = "VINAYAK" Or ClientName = "SUCCESS" Or ClientName = "SIDDHPOLYCOT" Then RPTINVOICE_TOTALTRANSA4.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                        RPTINVOICE_TOTALTRANSA4.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                        RPTINVOICE_TOTALTRANSA4.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                    Else
                        CRPO.ReportSource = RPTINVOICE_TOTALTRANS
                    End If
                    CRPO.Zoom(100)
                    CRPO.Refresh()
                    Exit Sub
                End If


                'CODE DONE BY GULKIT
                If INVOICERETAIL = True Then
                    CRPO.ReportSource = RPTINVOICE_RETAIL
                    If BLANKPAPER = True Then RPTINVOICE_RETAIL.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_RETAIL.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    CRPO.Zoom(100)
                    CRPO.Refresh()
                    Exit Sub
                End If

                CRPO.Refresh()

                If ClientName = "SKF" Then
                    CRPO.ReportSource = RPTINVOICE_SKF
                ElseIf ClientName = "AVIS" Then
                    CRPO.ReportSource = RPTINVOICE_AVIS
                    RPTINVOICE_AVIS.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If PARTYCHANGEADD <> "" Then RPTINVOICE_AVIS.DataDefinition.FormulaFields("PARTYCHANGEADD").Text = "'" & PARTYCHANGEADD & "'"
                    If BLANKPAPER = True Then RPTINVOICE_AVIS.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_AVIS.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                ElseIf ClientName = "SAFFRONOFF" Then
                    CRPO.ReportSource = RPTINVOICE_SAFFRON
                ElseIf ClientName = "SAFFRON" Then
                    CRPO.ReportSource = RPTINVOICE_SAFFRON
                ElseIf ClientName = "PURPLE" Then
                    CRPO.ReportSource = RPTINVOICE_PURPLE
                ElseIf ClientName = "SVS" Then
                    CRPO.ReportSource = RPTINVOICE_SVS
                ElseIf ClientName = "CC" Or ClientName = "C3" Then
                    CRPO.ReportSource = RPTINVOICE_CC
                    If ClientName = "C3" Then RPTINVOICE_CC.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                ElseIf ClientName = "MANINATH" Then
                    CRPO.ReportSource = RPTINVOICE_MANINATH
                ElseIf ClientName = "JURON" Or ClientName = "MOHAN" Then
                    If INVOICESCREENTYPE = "LINE GST" Then CRPO.ReportSource = RPTINVOICE_LINE Else CRPO.ReportSource = RPTINV_TOTAL
                ElseIf ClientName = "MOMAI" Then
                    CRPO.ReportSource = RPTINVOICE_MOMAI
                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                    CRPO.ReportSource = RPTINVOICE_KOTHARI
                    If BLANKPAPER = True Then RPTINVOICE_KOTHARI.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_KOTHARI.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                ElseIf ClientName = "MAHAVIR" Then
                    CRPO.ReportSource = RPTINVOICE_MAHAVIR
                ElseIf ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "MONOGRAM" Then
                    CRPO.ReportSource = RPTINVOICE_BARKHASHUBHI
                    RPTINVOICE_BARKHASHUBHI.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    RPTINVOICE_BARKHASHUBHI.DataDefinition.FormulaFields("GODNAMETOP").Text = "'" & GODNAME & "'"
                ElseIf ClientName = "DRDRAPES" Then
                    CRPO.ReportSource = RPTINVOICE_DRDRAPES
                    RPTINVOICE_DRDRAPES.DataDefinition.FormulaFields("DIGITALSIGN").Text = 1
                ElseIf ClientName = "SAKARIA" Or ClientName = "NVAHAN" Then
                    CRPO.ReportSource = RPTINVOICE_SAKNVAHAN
                    RPTINVOICE_SAKNVAHAN.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                ElseIf ClientName = "CHANDRA" Then
                    CRPO.ReportSource = RPTINVOICE_CHANDRA
                ElseIf ClientName = "DETLINE" Then
                    CRPO.ReportSource = RPTINVOICE_DETLINE
                ElseIf ClientName = "MITESHBHAI" Then
                    CRPO.ReportSource = RPTINVOICE_MITESHBHAI
                ElseIf ClientName = "SBA" Or ClientName = "SOFTAS" Then
                    CRPO.ReportSource = RPTINVOICE_SBASOFTAS
                    RPTINVOICE_SBASOFTAS.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then RPTINVOICE_SBASOFTAS.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_SBASOFTAS.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                ElseIf ClientName = "KDFAB" Then
                    CRPO.ReportSource = RPTINVOICE_KDFAB
                ElseIf ClientName = "POONAM" Or ClientName = "KREEVE" Then
                    CRPO.ReportSource = RPTINVOICE_POONAMKREEVE
                    'If IGSTFORMAT = False Then
                    '    CRPO.ReportSource = RPTINVOICE_POONAMKREEVE
                    'Else
                    '    CRPO.ReportSource = RPTINVOICE_POONAMKREEVEIGST
                    'End If
                ElseIf ClientName = "GELATO" Then
                    If IGSTFORMAT = False Then
                        CRPO.ReportSource = RPTINVOICE_GELATO
                        RPTINVOICE_GELATO.DataDefinition.FormulaFields("INVOICETYPE").Text = "'" & INVOICECOPYNAME & "'"
                    Else
                        CRPO.ReportSource = RPTINVOICE_GELATOIGST
                        RPTINVOICE_GELATOIGST.DataDefinition.FormulaFields("INVOICETYPE").Text = "'" & INVOICECOPYNAME & "'"
                    End If
                ElseIf ClientName = "SANGHVI" Then
                    CRPO.ReportSource = RPTINVOICE_SANGHVI
                ElseIf ClientName = "SUPRIYA" Then
                    CRPO.ReportSource = RPTINVOICE_SUPRIYA
                    RPTINVOICE_SUPRIYA.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then RPTINVOICE_SUPRIYA.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_SUPRIYA.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    RPTINVOICE_SUPRIYA.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    RPTINVOICE_SUPRIYA.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                ElseIf ClientName = "SIMPLEX" Then
                    CRPO.ReportSource = RPTINVOICE_SIMPLEX
                    RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                    If INVTOPHEADER = True Then RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("TOPHEADER").Text = 1 Else RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("TOPHEADER").Text = 0
                    If INVCENTREHEADER = True Then RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("CENTREHEADER").Text = 1 Else RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("CENTREHEADER").Text = 0
                    If INVSHOWSRNO = True Then RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("SHOWSRNO").Text = 1 Else RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("SHOWSRNO").Text = 0
                    If INVSHOWITEMDESIGN = True Then RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 1 Else RPTINVOICE_SIMPLEX.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 0
                ElseIf ClientName = "NAKODAINFOTECH" Then
                    CRPO.ReportSource = RPTINVOICE_NAKODAINFOTECH
                    RPTINVOICE_NAKODAINFOTECH.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then RPTINVOICE_NAKODAINFOTECH.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_NAKODAINFOTECH.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    RPTINVOICE_NAKODAINFOTECH.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    RPTINVOICE_NAKODAINFOTECH.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                ElseIf ClientName = "SNCM" Then
                    CRPO.ReportSource = RPTINVOICE_SNCM
                    RPTINVOICE_SNCM.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                ElseIf ClientName = "ABHEE" Then
                    CRPO.ReportSource = RPTINVOICE_ABHEE
                    RPTINVOICE_ABHEE.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then RPTINVOICE_ABHEE.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_ABHEE.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    If SHOWSIGNONINVOICE = True Then RPTINVOICE_ABHEE.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    RPTINVOICE_ABHEE.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    RPTINVOICE_ABHEE.DataDefinition.FormulaFields("GODNAMETOP").Text = "'" & GODNAME & "'"
                    RPTINVOICE_ABHEE.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                    If INVTOPHEADER = True Then RPTINVOICE_ABHEE.DataDefinition.FormulaFields("TOPHEADER").Text = 1 Else RPTINVOICE_ABHEE.DataDefinition.FormulaFields("TOPHEADER").Text = 0
                    If INVCENTREHEADER = True Then RPTINVOICE_ABHEE.DataDefinition.FormulaFields("CENTREHEADER").Text = 1 Else RPTINVOICE_ABHEE.DataDefinition.FormulaFields("CENTREHEADER").Text = 0
                    If INVSHOWSRNO = True Then RPTINVOICE_ABHEE.DataDefinition.FormulaFields("SHOWSRNO").Text = 1 Else RPTINVOICE_ABHEE.DataDefinition.FormulaFields("SHOWSRNO").Text = 0
                    If INVSHOWITEMDESIGN = True Then RPTINVOICE_ABHEE.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 1 Else RPTINVOICE_ABHEE.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 0

                Else
                    CRPO.ReportSource = RPTINVOICE_TOTALLEFT
                    RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    If SHOWSIGNONINVOICE = True Then RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("GODNAMETOP").Text = "'" & GODNAME & "'"
                    RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                    If INVTOPHEADER = True Then RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("TOPHEADER").Text = 1 Else RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("TOPHEADER").Text = 0
                    If INVCENTREHEADER = True Then RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("CENTREHEADER").Text = 1 Else RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("CENTREHEADER").Text = 0
                    If INVSHOWSRNO = True Then RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("SHOWSRNO").Text = 1 Else RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("SHOWSRNO").Text = 0
                    If INVSHOWITEMDESIGN = True Then RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 1 Else RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 0
                End If

            ElseIf FRMSTRING = "PROFORMAINVOICE" Then
                CRPO.ReportSource = RPTPROFORMAINVOICE
                RPTPROFORMAINVOICE.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                If BLANKPAPER = True Then RPTPROFORMAINVOICE.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTPROFORMAINVOICE.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                If ClientName = "ALENCOT" Then RPTPROFORMAINVOICE.DataDefinition.FormulaFields("SENDMAIL").Text = "1"

            ElseIf FRMSTRING = "EXPBUYER" Then
                CRPO.ReportSource = RPTINVOICE_BUYER
                RPTINVOICE_BUYER.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                If BLANKPAPER = True Then RPTINVOICE_BUYER.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_BUYER.DataDefinition.FormulaFields("WHITELABEL").Text = 0
            ElseIf FRMSTRING = "EXPCUSTOM" Then
                CRPO.ReportSource = RPTINVOICE_CUSTOM
                RPTINVOICE_CUSTOM.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                If BLANKPAPER = True Then RPTINVOICE_CUSTOM.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_CUSTOM.DataDefinition.FormulaFields("WHITELABEL").Text = 0
            ElseIf FRMSTRING = "EXPGST" Then
                CRPO.ReportSource = RPTINVOICE_EXPGST
                RPTINVOICE_EXPGST.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                If BLANKPAPER = True Then RPTINVOICE_EXPGST.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_EXPGST.DataDefinition.FormulaFields("WHITELABEL").Text = 0


            ElseIf FRMSTRING = "YARNDO" Then
                CRPO.ReportSource = RPTINVOICE_YARNDO
                If BLANKPAPER = True Then RPTINVOICE_YARNDO.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_YARNDO.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                RPTINVOICE_YARNDO.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"


            ElseIf FRMSTRING = "PARTYWISEDTLS" Then
                CRPO.ReportSource = RPTPARTYDTLS
                RPTPARTYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                CRPO.ReportSource = RPTPARTYSUMM
                RPTPARTYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If INVOICESCREENTYPE = "LINE GST" Then RPTPARTYSUMM.DataDefinition.FormulaFields("LINEGST").Text = 1 Else RPTPARTYSUMM.DataDefinition.FormulaFields("LINEGST").Text = 0
            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                CRPO.ReportSource = RPTAGENTDTLS
                RPTAGENTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                CRPO.ReportSource = RPTAGENTDTLS
                RPTAGENTSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "ITEMWISEDTLS" Then
                CRPO.ReportSource = RPTITEMDTLS
                RPTITEMDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "ITEMWISESUMM" Then
                CRPO.ReportSource = RPTITEMSUMM
                RPTITEMSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "CATEGORYWISESUMM" Then
                CRPO.ReportSource = RPTCATEGORYSUMM
                RPTCATEGORYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                CRPO.ReportSource = RPTQUALITYDTLS
                RPTQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                CRPO.ReportSource = RPTQUALITYSUMM
                RPTQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "DESIGNWISEDTLS" Then
                CRPO.ReportSource = RPTDESIGNDTLS
                RPTDESIGNDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "DESIGNWISESUMM" Then
                CRPO.ReportSource = RPTDESIGNSUMM
                RPTDESIGNSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SHADEWISEDTLS" Then
                CRPO.ReportSource = RPTSHADEDTLS
                RPTSHADEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SHADEWISESUMM" Then
                CRPO.ReportSource = RPTSHADESUMM
                RPTSHADESUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                CRPO.ReportSource = RPTTRANSDTLS
                RPTTRANSDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                CRPO.ReportSource = RPTTRANSSUMM
                RPTTRANSSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch Exp As LoadSaveReportException
            MsgBox("Incorrect path for loading report.",
                    MsgBoxStyle.Critical, "Load Report Error")

        Catch Exp As Exception
            MsgBox(Exp.Message, MsgBoxStyle.Critical, "General Error")

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
            If FRMSTRING = "INVOICE" Then

                strsearch = strsearch & " {INVOICEMASTER.INVOICE_no}= " & INVNO & " AND {REGISTERMASTER.REGISTER_NAME} = '" & registername & "' AND {INVOICEMASTER.INVOICE_cmpid} = " & CmpId & " AND {INVOICEMASTER.INVOICE_locationid} = " & Locationid & " AND {INVOICEMASTER.INVOICE_yearid} = " & YearId

                If INVOICETRANS = True Then
                    If TRANSPORTCOPYA4 Then
                        OBJ = New InvoiceReport_TOTALTRANSA4
                        If ClientName = "VSTRADERS" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                        OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                        OBJ.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                    Else
                        OBJ = New InvoiceReport_TOTALTRANS
                    End If
                    GoTo SKIPINVOICE
                End If


                If INVOICERETAIL = True Then
                    OBJ = New InvoiceReport_Retail
                    GoTo SKIPINVOICE
                End If


                'FOR COMMON REPORTS
                If ClientName = "AVIS" Then
                    OBJ = New InvoiceReport_AVIS
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                ElseIf ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "MONOGRAM" Then
                    OBJ = New InvoiceReport_BARKHASHUBHI
                    OBJ.DataDefinition.FormulaFields("GODNAMETOP").Text = "'" & GODNAME & "'"
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    OBJ = New InvoiceReport_CC
                ElseIf ClientName = "CHANDRA" Then
                    OBJ = New InvoiceReport_CHANDRA
                ElseIf ClientName = "DETLINE" Then
                    OBJ = New InvoiceReport_DETLINE
                ElseIf ClientName = "DRDRAPES" Then
                    OBJ = New InvoiceReport_DRDRAPES
                    OBJ.DataDefinition.FormulaFields("DIGITALSIGN").Text = 1
                ElseIf ClientName = "JURON" Or ClientName = "MOHAN" Then
                    If INVOICESCREENTYPE = "LINE GST" Then OBJ = New InvoiceReport Else OBJ = New InvoiceReport_TOTAL
                ElseIf ClientName = "KDFAB" Then
                    OBJ = New InvoiceReport_KDFAB
                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                    OBJ = New InvoiceReport_KOTHARI
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                ElseIf ClientName = "MAHAVIR" Then
                    OBJ = New InvoiceReport_MAHAVIR
                ElseIf ClientName = "MANINATH" Then
                    OBJ = New InvoiceReport_MANINATH
                ElseIf ClientName = "MOMAI" Then
                    OBJ = New InvoiceReport_MOMAI
                ElseIf ClientName = "MITESHBHAI" Then
                    OBJ = New InvoiceReport_MITESHBHAI
                ElseIf ClientName = "POONAM" Or ClientName = "KREEVE" Then
                    OBJ = New InvoiceReport_POONAMKREEVE
                    'If IGSTFORMAT = False Then OBJ = New InvoiceReport_POONAMKREEVE Else OBJ = New InvoiceReport_POONAMKREEVEIGST
                ElseIf ClientName = "GELATO" Then
                    If IGSTFORMAT = False Then OBJ = New InvoiceReport_GELATO Else OBJ = New InvoiceReport_GELATOIGST
                ElseIf ClientName = "PURPLE" Then
                    OBJ = New InvoiceReport_PURPLE
                ElseIf ClientName = "SAFFRONOFF" Or ClientName = "SAFFRON" Then
                    OBJ = New InvoiceReport_SAFFRON
                ElseIf ClientName = "SAKARIA" Or ClientName = "NVAHAN" Then
                    OBJ = New InvoiceReport_SAKNVAHAN
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                ElseIf ClientName = "SANGHVI" Then
                    OBJ = New InvoiceReport_SANGHVI
                ElseIf ClientName = "SBA" Or ClientName = "SOFTAS" Then
                    OBJ = New InvoiceReport_SBASOFTAS
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                ElseIf ClientName = "SKF" Then
                    OBJ = New InvoiceReport_SKF
                ElseIf ClientName = "SVS" Then
                    OBJ = New InvoiceReport_SVS
                ElseIf ClientName = "SUPRIYA" Then
                    OBJ = New InvoiceReport_SUPRIYA
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    OBJ.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                ElseIf ClientName = "SIMPLEX" Then
                    OBJ = New InvoiceReport_SIMPLEX
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    OBJ.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                    If INVTOPHEADER = True Then OBJ.DataDefinition.FormulaFields("TOPHEADER").Text = 1 Else OBJ.DataDefinition.FormulaFields("TOPHEADER").Text = 0
                    If INVCENTREHEADER = True Then OBJ.DataDefinition.FormulaFields("CENTREHEADER").Text = 1 Else OBJ.DataDefinition.FormulaFields("CENTREHEADER").Text = 0
                    If INVSHOWSRNO = True Then OBJ.DataDefinition.FormulaFields("SHOWSRNO").Text = 1 Else OBJ.DataDefinition.FormulaFields("SHOWSRNO").Text = 0
                    If INVSHOWITEMDESIGN = True Then OBJ.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 1 Else OBJ.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 0
                ElseIf ClientName = "SNCM" Then
                    OBJ = New InvoiceReport_SNCM
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                ElseIf ClientName = "NAKODAINFOTECH" Then
                    OBJ = New InvoiceReport_NI
                    If SHOWSIGNONINVOICE = True Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    OBJ.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                    If INVTOPHEADER = True Then OBJ.DataDefinition.FormulaFields("TOPHEADER").Text = 1 Else OBJ.DataDefinition.FormulaFields("TOPHEADER").Text = 0
                    If INVCENTREHEADER = True Then OBJ.DataDefinition.FormulaFields("CENTREHEADER").Text = 1 Else OBJ.DataDefinition.FormulaFields("CENTREHEADER").Text = 0
                    If INVSHOWSRNO = True Then OBJ.DataDefinition.FormulaFields("SHOWSRNO").Text = 1 Else OBJ.DataDefinition.FormulaFields("SHOWSRNO").Text = 0
                    If INVSHOWITEMDESIGN = True Then OBJ.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 1 Else OBJ.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 0
                ElseIf ClientName = "ABHEE" Then
                    OBJ = New InvoiceReport_ABHEE
                    If SHOWSIGNONINVOICE = True Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    OBJ.DataDefinition.FormulaFields("GODNAMETOP").Text = "'" & GODNAME & "'"
                    OBJ.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                    If INVTOPHEADER = True Then OBJ.DataDefinition.FormulaFields("TOPHEADER").Text = 1 Else OBJ.DataDefinition.FormulaFields("TOPHEADER").Text = 0
                    If INVCENTREHEADER = True Then OBJ.DataDefinition.FormulaFields("CENTREHEADER").Text = 1 Else OBJ.DataDefinition.FormulaFields("CENTREHEADER").Text = 0
                    If INVSHOWSRNO = True Then OBJ.DataDefinition.FormulaFields("SHOWSRNO").Text = 1 Else OBJ.DataDefinition.FormulaFields("SHOWSRNO").Text = 0
                    If INVSHOWITEMDESIGN = True Then OBJ.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 1 Else OBJ.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 0
                Else
                    OBJ = New InvoiceReport_TOTALLEFT
                    If SHOWSIGNONINVOICE = True Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    OBJ.DataDefinition.FormulaFields("GODNAMETOP").Text = "'" & GODNAME & "'"
                    OBJ.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                    If INVTOPHEADER = True Then OBJ.DataDefinition.FormulaFields("TOPHEADER").Text = 1 Else OBJ.DataDefinition.FormulaFields("TOPHEADER").Text = 0
                    If INVCENTREHEADER = True Then OBJ.DataDefinition.FormulaFields("CENTREHEADER").Text = 1 Else OBJ.DataDefinition.FormulaFields("CENTREHEADER").Text = 0
                    If INVSHOWSRNO = True Then OBJ.DataDefinition.FormulaFields("SHOWSRNO").Text = 1 Else OBJ.DataDefinition.FormulaFields("SHOWSRNO").Text = 0
                    If INVSHOWITEMDESIGN = True Then OBJ.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 1 Else OBJ.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 0
                End If

            ElseIf FRMSTRING = "PROFORMAINVOICE" Then
                strsearch = strsearch & "{PROFORMAINVOICEMASTER.INVOICE_no}=" & Val(INVNO) & " and {REGISTERMASTER.REGISTER_NAME} = '" & registername & "' and {PROFORMAINVOICEMASTER.INVOICE_yearid}=" & YearId
                OBJ = New ProformaInvoiceReport_TOTALLEFT

            ElseIf FRMSTRING = "YARNDO" Then
                OBJ = New InvoiceReport_YARNDO
                strsearch = strsearch & " {INVOICEMASTER.INVOICE_no}= " & INVNO & " AND {REGISTERMASTER.REGISTER_NAME} = '" & registername & "' AND {INVOICEMASTER.INVOICE_cmpid} = " & CmpId & " AND {INVOICEMASTER.INVOICE_locationid} = " & Locationid & " AND {INVOICEMASTER.INVOICE_yearid} = " & YearId
                If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            End If

SKIPINVOICE:
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
                'Dim expo As New ExportOptions
                Dim PATH As String = ""
                If FRMSTRING = "INVOICE" Then
                    PATH = Application.StartupPath & "\" & PARTYNAME & "INVOICE_" & INVNO & ".pdf"
                ElseIf FRMSTRING = "YARNDO" Then
                    PATH = Application.StartupPath & "\" & PARTYNAME & "YARNDO_" & INVNO & ".pdf"
                Else
                    PATH = Application.StartupPath & "\" & PARTYNAME & "INVOICE_" & INVNO & ".pdf"
                End If

                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                If File.Exists(PATH) Then File.Delete(PATH)

                'expo = OBJ.ExportOptions
                If PDFWITHDIGITALSIGN = True Then OBJ.DataDefinition.FormulaFields("DIGITALSIGN").Text = 1
                If ClientName <> "KCRAYON" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                'expo.ExportDestinationType = ExportDestinationType.DiskFile
                'expo.ExportFormatType = ExportFormatType.PortableDocFormat
                'expo.DestinationOptions = oDfDopt

                OBJ.ExportToDisk(ExportFormatType.PortableDocFormat, PATH)

                If ClientName <> "KCRAYON" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 0
                If PDFWITHDIGITALSIGN = True Then OBJ.DataDefinition.FormulaFields("DIGITALSIGN").Text = 0


                'FOR DIGITAL SIGN ON PFD GENERATED
                If PDFWITHDIGITALSIGN = True Then
                    For Each cert As X509Certificate2 In SelectCertificates()
                        lbCerts.Items.Add(New CertItem() With {.Name = cert.Subject, .Cert = cert})
                    Next cert
                    lbCerts.SelectedIndex = 0


                    Dim documentProcessor As New PdfDocumentProcessor()
                    Dim fileHelper As New PdfFileHelper(documentProcessor, pdfViewer)
                    PATH = Application.StartupPath & "\" & PARTYNAME & "INVOICE_" & INVNO & ".pdf"
                    documentProcessor.LoadDocument(path)
                    pdfViewer.LoadDocument(path)

                    If Directory.Exists(Application.StartupPath & " \INVOICEPDF") = False Then Directory.CreateDirectory(Application.StartupPath & "\INVOICEPDF")

                    Dim fileName As String = Application.StartupPath & "\INVOICEPDF\" & PARTYNAME & "INVOICE_" & Val(INVNO) & "-" & Val(AccFrom.Year) & ".pdf" 'fileHelper.SaveFileDialog() 'Application.StartupPath & "\INVOICE.pdf"
                    If (Not String.IsNullOrEmpty(fileName)) Then
                        documentProcessor.Document.Creator = "Nakoda Infotech"
                        documentProcessor.Document.Producer = "Nakoda Infotech"
                        Dim signature As New PdfSignature((CType(lbCerts.SelectedItem, CertItem)).Cert) With {.Location = "", .ContactInfo = "", .Reason = ""}
                        Try
                            documentProcessor.SaveDocument(fileName, New PdfSaveOptions() With {.Signature = signature})
                        Catch exception As CryptographicException
                            XtraMessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End If
                    documentProcessor.Dispose()
                End If
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
            Dim emailid1 As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            'FIRST DELETE THE EXISTING PDF AND THEN RECREATE
            If FRMSTRING = "" Then
                tempattachment = "SALEDETAILS"
            ElseIf FRMSTRING = "INVOICE" Then
                tempattachment = "INVOICE"
            ElseIf FRMSTRING = "PROFORMAINVOICE" Then
                tempattachment = "PROFORMA"
            ElseIf FRMSTRING = "YARNDO" Then
                tempattachment = "YARNDO"
            Else
                tempattachment = "SALESUMMARY"
            End If
            If File.Exists(Application.StartupPath & "\" & tempattachment & ".PDF") Then System.IO.File.Delete(Application.StartupPath & "\" & tempattachment & ".PDF")
            '**********************************************************


            Transfer()

            If PARTYNAME <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable = OBJCMN.SEARCH("ACC_EMAIL As EMAILID", "", "LEDGERS", " And ACC_CMPNAME = '" & PARTYNAME & "' AND ACC_YEARID=" & YearId)
                If dt.Rows.Count > 0 Then
                    emailid = dt.Rows(0).Item(0).ToString
                End If
            End If

            If AGENTNAME <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable = OBJCMN.SEARCH("ACC_EMAIL AS EMAILID", "", "LEDGERS", " and ACC_CMPNAME = '" & AGENTNAME & "' AND ACC_YEARID=" & YearId)
                If dt.Rows.Count > 0 Then
                    emailid1 = dt.Rows(0).Item(0).ToString
                End If
            End If


            Dim objmail As New SendMail

            If FRMSTRING = "" Then
                objmail.subject = "Invoice Details"
            ElseIf FRMSTRING = "INVOICE" Then
                objmail.subject = "Invoice"
            ElseIf FRMSTRING = "YARNDO" Then
                objmail.subject = "Yarn DO"
            ElseIf FRMSTRING = "PROFORMAINVOICE" Then
                objmail.subject = "Proforma Invoice"
            Else
                objmail.subject = "Invoice Summary"
            End If


            objmail.attachment = tempattachment
            objmail.attachment = Application.StartupPath & "\" & tempattachment & ".PDF"

            If emailid <> "" Then
                objmail.cmbfirstadd.Text = emailid
            End If
            If emailid1 <> "" Then
                objmail.cmbsecondadd.Text = emailid1
            End If
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

            If FRMSTRING = "" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SALEDETAILS.PDF"
                '   expo = rpts.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                '   rpts.Export()
            ElseIf FRMSTRING = "INVOICE" Then


                If INVOICETRANS = True Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    If TRANSPORTCOPYA4 Then
                        expo = RPTINVOICE_TOTALTRANSA4.ExportOptions
                        RPTINVOICE_TOTALTRANSA4.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTINVOICE_TOTALTRANSA4.Export()
                        RPTINVOICE_TOTALTRANSA4.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                    Else
                        expo = RPTINVOICE_TOTALTRANS.ExportOptions
                        RPTINVOICE_TOTALTRANS.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTINVOICE_TOTALTRANS.Export()
                        RPTINVOICE_TOTALTRANS.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                    End If
                    Exit Sub
                End If


                If INVOICERETAIL = True Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_RETAIL.ExportOptions
                    If BLANKPAPER = True Then RPTINVOICE_RETAIL.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTINVOICE_RETAIL.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    RPTINVOICE_RETAIL.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_RETAIL.Export()
                    RPTINVOICE_RETAIL.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                    Exit Sub
                End If


                If ClientName = "SAFFRON" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_SAFFRON.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_SAFFRON.Export()
                ElseIf ClientName = "SAFFRONOFF" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_SAFFRON.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_SAFFRON.Export()
                ElseIf ClientName = "MITESHBHAI" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_MITESHBHAI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_MITESHBHAI.Export()
                    RPTINVOICE_SAFFRON.Export()
                ElseIf ClientName = "SBA" Or ClientName = "SOFTAS" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_SBASOFTAS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_SBASOFTAS.Export()
                ElseIf ClientName = "PURPLE" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_PURPLE.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_PURPLE.Export()
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    RPTINVOICE_CC.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    expo = RPTINVOICE_CC.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_CC.Export()
                    RPTINVOICE_CC.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                ElseIf ClientName = "SVS" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_SVS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_SVS.Export()
                ElseIf ClientName = "AVIS" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    RPTINVOICE_AVIS.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    expo = RPTINVOICE_AVIS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_AVIS.Export()
                    RPTINVOICE_AVIS.DataDefinition.FormulaFields("SENDMAIL").Text = "0"

                ElseIf ClientName = "SKF" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    RPTINVOICE_SKF.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    expo = RPTINVOICE_SKF.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_SKF.Export()
                    RPTINVOICE_SKF.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                ElseIf ClientName = "MANINATH" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_MANINATH.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_MANINATH.Export()
                    'TEMPORARY
                    'ElseIf ClientName = "MANIBHADRA" Then
                    '    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    '    expo = RPTINVOICE_MANIBHADRA.ExportOptions
                    '    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    '    expo.DestinationOptions = oDfDopt
                    '    RPTINVOICE_MANIBHADRA.Export()
                ElseIf ClientName = "SANGHVI" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    RPTINVOICE_SANGHVI.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    expo = RPTINVOICE_SANGHVI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_SANGHVI.Export()
                    RPTINVOICE_SANGHVI.DataDefinition.FormulaFields("SENDMAIL").Text = "0"

                ElseIf ClientName = "KDFAB" Then

                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    RPTINVOICE_KDFAB.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    expo = RPTINVOICE_KDFAB.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_KDFAB.Export()
                    RPTINVOICE_KDFAB.DataDefinition.FormulaFields("SENDMAIL").Text = "1"

                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                    RPTINVOICE_KOTHARI.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_KOTHARI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_KOTHARI.Export()
                    RPTINVOICE_KOTHARI.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                ElseIf ClientName = "MAHAVIR" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_MAHAVIR.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_MAHAVIR.Export()
                ElseIf ClientName = "JURON" Or ClientName = "MOHAN" Then
                    If INVOICESCREENTYPE = "LINE GST" Then
                        oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                        expo = RPTINVOICE_LINE.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTINVOICE_LINE.Export()
                    Else
                        oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                        expo = RPTINV_TOTAL.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTINV_TOTAL.Export()
                    End If
                ElseIf ClientName = "MOMAI" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_MOMAI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_MOMAI.Export()

                ElseIf ClientName = "POONAM" Or ClientName = "KREEVE" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_POONAMKREEVE.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_POONAMKREEVE.Export()

                ElseIf ClientName = "GELATO" Then
                    If IGSTFORMAT = False Then
                        oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                        RPTINVOICE_GELATO.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                        expo = RPTINVOICE_GELATO.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTINVOICE_GELATO.Export()
                        RPTINVOICE_GELATO.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                    Else
                        oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                        RPTINVOICE_GELATOIGST.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                        expo = RPTINVOICE_GELATOIGST.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTINVOICE_GELATOIGST.Export()
                        RPTINVOICE_GELATOIGST.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                    End If

                ElseIf ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "MONOGRAM" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_BARKHASHUBHI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_BARKHASHUBHI.Export()

                ElseIf ClientName = "DRDRAPES" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    RPTINVOICE_DRDRAPES.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                    If PDFWITHDIGITALSIGN = True Then RPTINVOICE_DRDRAPES.DataDefinition.FormulaFields("DIGITALSIGN").Text = 1
                    expo = RPTINVOICE_DRDRAPES.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_DRDRAPES.Export()
                    RPTINVOICE_DRDRAPES.DataDefinition.FormulaFields("SENDMAIL").Text = 0
                    If PDFWITHDIGITALSIGN = True Then RPTINVOICE_DRDRAPES.DataDefinition.FormulaFields("DIGITALSIGN").Text = 0


                ElseIf ClientName = "SAKARIA" Or ClientName = "NVAHAN" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    RPTINVOICE_SAKNVAHAN.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                    expo = RPTINVOICE_SAKNVAHAN.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_SAKNVAHAN.Export()
                    RPTINVOICE_SAKNVAHAN.DataDefinition.FormulaFields("SENDMAIL").Text = 0

                ElseIf ClientName = "CHANDRA" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_CHANDRA.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_CHANDRA.Export()

                ElseIf ClientName = "DETLINE" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_DETLINE.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_DETLINE.Export()

                ElseIf ClientName = "SUPRIYA" Then
                    RPTINVOICE_SUPRIYA.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_SUPRIYA.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_SUPRIYA.Export()
                    RPTINVOICE_SUPRIYA.DataDefinition.FormulaFields("SENDMAIL").Text = "0"

                ElseIf ClientName = "SNCM" Then
                    RPTINVOICE_SNCM.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_SNCM.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_SNCM.Export()
                    RPTINVOICE_SNCM.DataDefinition.FormulaFields("SENDMAIL").Text = "0"

                ElseIf ClientName = "NAKODAINFOTECH" Then
                    RPTINVOICE_NAKODAINFOTECH.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_NAKODAINFOTECH.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_NAKODAINFOTECH.Export()
                    RPTINVOICE_NAKODAINFOTECH.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                ElseIf ClientName = "ABHEE" Then
                    RPTINVOICE_ABHEE.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_ABHEE.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_ABHEE.Export()
                    RPTINVOICE_ABHEE.DataDefinition.FormulaFields("SENDMAIL").Text = "0"

                Else
                    RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                    expo = RPTINVOICE_TOTALLEFT.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_TOTALLEFT.Export()
                    RPTINVOICE_TOTALLEFT.DataDefinition.FormulaFields("SENDMAIL").Text = "0"

                End If


                'FOR DIGITAL SIGN ON PFD GENERATED
                If PDFWITHDIGITALSIGN = True Then
                    For Each cert As X509Certificate2 In SelectCertificates()
                        lbCerts.Items.Add(New CertItem() With {.Name = cert.Subject, .Cert = cert})
                    Next cert
                    lbCerts.SelectedIndex = 0


                    Dim documentProcessor As New PdfDocumentProcessor()
                    Dim fileHelper As New PdfFileHelper(documentProcessor, pdfViewer)
                    Dim path As String = Application.StartupPath & "\INVOICE.PDF"
                    documentProcessor.LoadDocument(path)
                    pdfViewer.LoadDocument(path)

                    If Directory.Exists(Application.StartupPath & "\INVOICEPDF") = False Then Directory.CreateDirectory(Application.StartupPath & "\INVOICEPDF")

                    Dim fileName As String = Application.StartupPath & "\INVOICEPDF\INVOICE_" & Val(INVNO) & "-" & Val(AccFrom.Year) & ".pdf" 'fileHelper.SaveFileDialog() 'Application.StartupPath & "\INVOICE.pdf"
                    If (Not String.IsNullOrEmpty(fileName)) Then
                        documentProcessor.Document.Creator = "Nakoda Infotech"
                        documentProcessor.Document.Producer = "Nakoda Infotech"
                        Dim signature As New PdfSignature((CType(lbCerts.SelectedItem, CertItem)).Cert) With {.Location = "", .ContactInfo = "", .Reason = ""}
                        Try
                            documentProcessor.SaveDocument(fileName, New PdfSaveOptions() With {.Signature = signature})
                        Catch exception As CryptographicException
                            XtraMessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End If
                    documentProcessor.Dispose()
                End If




            ElseIf FRMSTRING = "PROFORMAINVOICE" Then
                RPTPROFORMAINVOICE.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                oDfDopt.DiskFileName = Application.StartupPath & "\PROFORMA.PDF"
                expo = RPTPROFORMAINVOICE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPROFORMAINVOICE.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                RPTPROFORMAINVOICE.Export()

            ElseIf FRMSTRING = "EXPBUYER" Then
                expo = RPTINVOICE_BUYER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINVOICE_BUYER.Export()

            ElseIf FRMSTRING = "EXPCUSTOM" Then
                expo = RPTINVOICE_CUSTOM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINVOICE_CUSTOM.Export()

            ElseIf FRMSTRING = "EXPGST" Then
                expo = RPTINVOICE_EXPGST.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINVOICE_EXPGST.Export()

            ElseIf FRMSTRING = "YARNDO" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\YARNDO.PDF"
                expo = RPTINVOICE_YARNDO.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINVOICE_YARNDO.Export()

            Else
                oDfDopt.DiskFileName = Application.StartupPath & "\SALESUMMARY.PDF"
                '  expo = rptssum.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                'rptssum.Export()
            End If

            If FRMSTRING = "PARTYWISEDTLS" Then
                expo = RPTPARTYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPARTYDTLS.Export()
            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                expo = RPTPARTYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPARTYSUMM.Export()
            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                expo = RPTAGENTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTAGENTDTLS.Export()
            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                expo = RPTAGENTSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTAGENTSUMM.Export()
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
                RPTCATEGORYSUMM.Export()
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
                'ElseIf FRMSTRING = "MONTHLY" Then
                '    expo = RPTMONTHLY.ExportOptions
                '    expo.ExportDestinationType = ExportDestinationType.DiskFile
                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                '    expo.DestinationOptions = oDfDopt
                '    RPTMONTHLY.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub


End Class