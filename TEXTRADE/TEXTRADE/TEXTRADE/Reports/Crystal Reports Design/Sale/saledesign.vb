
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
Imports DevExpress.CodeParser

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

    Dim OBJ As New Object

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
            ElseIf FRMSTRING = "QUOTATION" Then
                tempattachment = "QUOTATION"
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




            If FRMSTRING = "PARTYWISEDTLS" Then
                OBJ = New InvoicePartyWiseDetails
            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                OBJ = New InvoicePartyWiseSummary
            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                OBJ = New InvoiceAgentWiseDetails
            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                OBJ = New InvoiceAgentWiseSummary
            ElseIf FRMSTRING = "ITEMWISEDTLS" Then
                OBJ = New InvoiceItemWiseDetails
            ElseIf FRMSTRING = "ITEMWISESUMM" Then
                OBJ = New InvoiceItemWiseSummary
            ElseIf FRMSTRING = "CATEGORYWISESUMM" Then
                OBJ = New InvoiceCategoryWiseSummary
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                OBJ = New InvoiceQualityWiseDetails
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                OBJ = New InvoiceQualityWiseSummary
            ElseIf FRMSTRING = "DESIGNWISEDTLS" Then
                OBJ = New InvoiceDesignWiseDetails
            ElseIf FRMSTRING = "DESIGNWISESUMM" Then
                OBJ = New InvoiceDesignWiseSummary
            ElseIf FRMSTRING = "SHADEWISEDTLS" Then
                OBJ = New InvoiceColorWiseDetails
            ElseIf FRMSTRING = "SHADEWISESUMM" Then
                OBJ = New InvoiceColorWiseSummary
            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                OBJ = New InvoiceTransWiseDetails
            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                OBJ = New InvoiceTransWiseSummary
            ElseIf FRMSTRING = "EXPBUYER" Then
                OBJ = New InvoiceReport_Export_Buyer
                Me.Text = "Buyer Invoice"
            ElseIf FRMSTRING = "EXPCUSTOM" Then
                OBJ = New InvoiceReport_Export_Custom
                Me.Text = "Custom Invoice"
            ElseIf FRMSTRING = "EXPGST" Then
                OBJ = New InvoiceReport_Export_GST
                Me.Text = "GST Invoice"
            ElseIf FRMSTRING = "YARNDO" Then
                OBJ = New InvoiceReport_YARNDO
                Me.Text = "Yarn DO"
            ElseIf FRMSTRING = "PROFORMAINVOICE" Then
                OBJ = New ProformaInvoiceReport_TOTALLEFT
            ElseIf FRMSTRING = "QUOTATION" Then
                OBJ = New ProformaReport_LAXMI
            ElseIf FRMSTRING = "INVOICE" Then

                'CODE DONE BY GULKIT
                If INVOICETRANS = True Then
                    If TRANSPORTCOPYA4 Then
                        OBJ = New InvoiceReport_TOTALTRANSA4
                    Else
                        OBJ = New InvoiceReport_TOTALTRANS
                    End If
                    GoTo SKIPINVOICE
                End If


                'CODE DONE BY GULKIT
                If INVOICERETAIL = True Then
                    OBJ = New InvoiceReport_Retail
                    GoTo SKIPINVOICE
                End If


                If ClientName = "SKF" Then
                    OBJ = New InvoiceReport_SKF
                ElseIf ClientName = "AVIS" Then
                    OBJ = New InvoiceReport_AVIS
                ElseIf ClientName = "SAFFRONOFF" Or ClientName = "SAFFRON" Then
                    OBJ = New InvoiceReport_SAFFRON
                ElseIf ClientName = "PURPLE" Then
                    OBJ = New InvoiceReport_PURPLE
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    OBJ = New InvoiceReport_CC
                ElseIf ClientName = "SVS" Then
                    OBJ = New InvoiceReport_SVS
                ElseIf ClientName = "MANINATH" Then
                    OBJ = New InvoiceReport_MANINATH
                ElseIf ClientName = "JURON" Or ClientName = "MOHAN" Then
                    If INVOICESCREENTYPE = "LINE GST" Then OBJ = New InvoiceReport Else OBJ = New InvoiceReport_TOTAL
                ElseIf ClientName = "MOMAI" Then
                    OBJ = New InvoiceReport_MOMAI
                ElseIf ClientName = "MAHAVIR" Then
                    OBJ = New InvoiceReport_MAHAVIR
                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                    OBJ = New InvoiceReport_KOTHARI
                ElseIf ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "MONOGRAM" Then
                    OBJ = New InvoiceReport_BARKHASHUBHI
                ElseIf ClientName = "DRDRAPES" Then
                    OBJ = New InvoiceReport_DRDRAPES
                ElseIf ClientName = "SAKARIA" Or ClientName = "NVAHAN" Then
                    OBJ = New InvoiceReport_SAKNVAHAN
                ElseIf ClientName = "CHANDRA" Then
                    OBJ = New InvoiceReport_CHANDRA
                ElseIf ClientName = "DETLINE" Then
                    OBJ = New InvoiceReport_DETLINE
                ElseIf ClientName = "KDFAB" Then
                    OBJ = New InvoiceReport_KDFAB
                ElseIf ClientName = "SBA" Or ClientName = "SOFTAS" Then
                    OBJ = New InvoiceReport_SBASOFTAS
                ElseIf ClientName = "POONAM" Or ClientName = "KREEVE" Then
                    OBJ = New InvoiceReport_POONAMKREEVE
                ElseIf ClientName = "GELATO" Then
                    If IGSTFORMAT = False Then
                        OBJ = New InvoiceReport_GELATO
                    Else
                        OBJ = New InvoiceReport_GELATOIGST
                    End If
                ElseIf ClientName = "SANGHVI" Then
                    OBJ = New InvoiceReport_SANGHVI
                ElseIf ClientName = "SUPRIYA" Then
                    OBJ = New InvoiceReport_SUPRIYA
                ElseIf ClientName = "SIMPLEX" Then
                    OBJ = New InvoiceReport_SIMPLEX
                ElseIf ClientName = "SNCM" Then
                    OBJ = New InvoiceReport_SNCM
                ElseIf ClientName = "LAXMI" Then
                    OBJ = New InvoiceReport_LAXMI
                ElseIf ClientName = "NAKODAINFOTECH" Then
                    OBJ = New InvoiceReport_NI
                ElseIf ClientName = "ABHEE" Then
                    OBJ = New InvoiceReport_ABHEE
                Else
                    OBJ = New InvoiceReport_TOTALLEFT
                End If

            End If







SKIPINVOICE:


            '''''************* START OF OG CODE
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


            crTables = OBJ.Database.Tables
            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next


            '************************ END *******************
            getFromToDate()
            CRPO.SelectionFormula = strsearch
            CRPO.ReportSource = OBJ

            If FRMSTRING <> "INVOICE" And FRMSTRING <> "EXPBUYER" And FRMSTRING <> "EXPCUSTOM" And FRMSTRING <> "EXPGST" And FRMSTRING <> "PROFORMAINVOICE" And FRMSTRING <> "QUOTATION" And FRMSTRING <> "YARNDO" Then
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


            If FRMSTRING = "INVOICE" Then

                'CODE DONE BY GULKIT
                If INVOICETRANS = True Then
                    If TRANSPORTCOPYA4 Then
                        If ClientName = "VSTRADERS" Or ClientName = "SIMPLEX" Or ClientName = "CHINTAN" Or ClientName = "VINAYAK" Or ClientName = "SUCCESS" Or ClientName = "SIDDHPOLYCOT" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                        OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                        OBJ.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                    End If
                    CRPO.Zoom(100)
                    CRPO.Refresh()
                    Exit Sub
                End If


                'CODE DONE BY GULKIT
                If INVOICERETAIL = True Then
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    CRPO.Zoom(100)
                    CRPO.Refresh()
                    Exit Sub
                End If


                If ClientName = "AVIS" Then
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If PARTYCHANGEADD <> "" Then OBJ.DataDefinition.FormulaFields("PARTYCHANGEADD").Text = "'" & PARTYCHANGEADD & "'"
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                ElseIf ClientName = "CC" Or ClientName = "C3" Then
                    OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                ElseIf ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "MONOGRAM" Then
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    OBJ.DataDefinition.FormulaFields("GODNAMETOP").Text = "'" & GODNAME & "'"
                ElseIf ClientName = "DRDRAPES" Then
                    OBJ.DataDefinition.FormulaFields("DIGITALSIGN").Text = 1
                ElseIf ClientName = "SAKARIA" Or ClientName = "NVAHAN" Then
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                ElseIf ClientName = "SBA" Or ClientName = "SOFTAS" Then
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                ElseIf ClientName = "GELATO" Then
                    If IGSTFORMAT = False Then OBJ.DataDefinition.FormulaFields("INVOICETYPE").Text = "'" & INVOICECOPYNAME & "'" Else OBJ.DataDefinition.FormulaFields("INVOICETYPE").Text = "'" & INVOICECOPYNAME & "'"
                ElseIf ClientName = "SUPRIYA" Then
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    OBJ.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                ElseIf ClientName = "SIMPLEX" Then
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    OBJ.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                    If INVTOPHEADER = True Then OBJ.DataDefinition.FormulaFields("TOPHEADER").Text = 1 Else OBJ.DataDefinition.FormulaFields("TOPHEADER").Text = 0
                    If INVCENTREHEADER = True Then OBJ.DataDefinition.FormulaFields("CENTREHEADER").Text = 1 Else OBJ.DataDefinition.FormulaFields("CENTREHEADER").Text = 0
                    If INVSHOWSRNO = True Then OBJ.DataDefinition.FormulaFields("SHOWSRNO").Text = 1 Else OBJ.DataDefinition.FormulaFields("SHOWSRNO").Text = 0
                    If INVSHOWITEMDESIGN = True Then OBJ.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 1 Else OBJ.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 0
                ElseIf ClientName = "NAKODAINFOTECH" Then
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    OBJ.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                ElseIf ClientName = "SNCM" Then
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                ElseIf ClientName = "ABHEE" Then

                    'FETCH SO DETAILS AND PASS IN PRINT FORMAT
                    Dim OBJCMN As New ClsCommon
                    Dim DTSO As DataTable = OBJCMN.SEARCH(" ISNULL(SO_MTRS,0) AS SOQTY, (CASE WHEN SO_ORDERON = 'PCS' THEN ISNULL(SUM(INVOICE_PCS),0) ELSE ISNULL(SUM(INVOICE_MTRS),0) END) AS ISSQTY ", "", " INVOICEMASTER_SODETAILS INNER JOIN ALLSALEORDER_DESC ON INVOICE_FROMNO = ALLSALEORDER_DESC.SO_NO AND INVOICE_FROMSRNO = ALLSALEORDER_DESC.SO_GRIDSRNO AND INVOICE_FROMTYPE = ALLSALEORDER_DESC.TYPE INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTERMASTER.REGISTER_ID  ", " AND INVOICEMASTER_SODETAILS.INVOICE_NO <= " & Val(INVNO) & " AND REGISTERMASTER.REGISTER_NAME = '" & registername & "' AND INVOICEMASTER_SODETAILS.INVOICE_YEARID = " & YearId & " AND ALLSALEORDER_DESC.SO_NO = (SELECT INVOICEMASTER_SODETAILS.INVOICE_FROMNO FROM INVOICEMASTER_SODETAILS WHERE INVOICEMASTER_SODETAILS.INVOICE_NO = " & Val(INVNO) & " AND REGISTERMASTER.REGISTER_NAME = '" & registername & "' AND INVOICEMASTER_SODETAILS.INVOICE_YEARID = " & YearId & ") GROUP BY SO_QTY, SO_MTRS, SO_ORDERON")
                    If DTSO.Rows.Count > 0 Then
                        OBJ.DataDefinition.FormulaFields("SOQTY").Text = Val(DTSO.Rows(0).Item("SOQTY"))
                        OBJ.DataDefinition.FormulaFields("ISSQTY").Text = Val(DTSO.Rows(0).Item("ISSQTY"))
                        OBJ.DataDefinition.FormulaFields("BALQTY").Text = Val(DTSO.Rows(0).Item("SOQTY")) - Val(DTSO.Rows(0).Item("ISSQTY"))
                    End If
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If INVOICECOPYNAME = "OFFICE COPY" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"

                ElseIf ClientName <> "SANGHVI" Then
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    If SHOWSIGNONINVOICE = True Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    OBJ.DataDefinition.FormulaFields("GODNAMETOP").Text = "'" & GODNAME & "'"
                    OBJ.DataDefinition.FormulaFields("ALLOWEINVOICE").Text = ALLOWEINVOICE
                    If INVTOPHEADER = True Then OBJ.DataDefinition.FormulaFields("TOPHEADER").Text = 1 Else OBJ.DataDefinition.FormulaFields("TOPHEADER").Text = 0
                    If INVCENTREHEADER = True Then OBJ.DataDefinition.FormulaFields("CENTREHEADER").Text = 1 Else OBJ.DataDefinition.FormulaFields("CENTREHEADER").Text = 0
                    If INVSHOWSRNO = True Then OBJ.DataDefinition.FormulaFields("SHOWSRNO").Text = 1 Else OBJ.DataDefinition.FormulaFields("SHOWSRNO").Text = 0
                    If INVSHOWITEMDESIGN = True Then OBJ.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 1 Else OBJ.DataDefinition.FormulaFields("SHOWITEMDESIGN").Text = 0
                End If

            ElseIf FRMSTRING = "PROFORMAINVOICE" Then
                OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                If ClientName = "ALENCOT" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
            ElseIf FRMSTRING = "EXPBUYER" Then
                OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
            ElseIf FRMSTRING = "EXPCUSTOM" Then
                OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
            ElseIf FRMSTRING = "EXPGST" Then
                OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
            ElseIf FRMSTRING = "YARNDO" Then
                If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
            ElseIf FRMSTRING = "PARTYWISEDTLS" Then
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If INVOICESCREENTYPE = "LINE GST" Then OBJ.DataDefinition.FormulaFields("LINEGST").Text = 1 Else OBJ.DataDefinition.FormulaFields("LINEGST").Text = 0
            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "JOBBERWISESUMM" Or FRMSTRING = "ITEMWISEDTLS" Or FRMSTRING = "ITEMWISESUMM" Or FRMSTRING = "CATEGORYWISESUMM" Or FRMSTRING = "QUALITYWISEDTLS" Or FRMSTRING = "QUALITYWISESUMM" Or FRMSTRING = "DESIGNWISEDTLS" Or FRMSTRING = "DESIGNWISESUMM" Or FRMSTRING = "SHADEWISEDTLS" Or FRMSTRING = "SHADEWISESUMM" Or FRMSTRING = "TRANSWISEDTLS" Or FRMSTRING = "TRANSWISESUMM" Then
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()
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
                ElseIf ClientName = "LAXMI" Then
                    OBJ = New InvoiceReport_LAXMI
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

                    'FETCH SO DETAILS AND PASS IN PRINT FORMAT
                    Dim OBJCMN As New ClsCommon
                    Dim DTSO As DataTable = OBJCMN.SEARCH(" ISNULL(SO_MTRS,0) AS SOQTY, (CASE WHEN SO_ORDERON = 'PCS' THEN ISNULL(SUM(INVOICE_PCS),0) ELSE ISNULL(SUM(INVOICE_MTRS),0) END) AS ISSQTY ", "", " INVOICEMASTER_SODETAILS INNER JOIN ALLSALEORDER_DESC ON INVOICE_FROMNO = ALLSALEORDER_DESC.SO_NO AND INVOICE_FROMSRNO = ALLSALEORDER_DESC.SO_GRIDSRNO AND INVOICE_FROMTYPE = ALLSALEORDER_DESC.TYPE INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTERMASTER.REGISTER_ID  ", " AND INVOICEMASTER_SODETAILS.INVOICE_NO <= " & Val(INVNO) & " AND REGISTERMASTER.REGISTER_NAME = '" & registername & "' AND INVOICEMASTER_SODETAILS.INVOICE_YEARID = " & YearId & " AND ALLSALEORDER_DESC.SO_NO = (SELECT INVOICEMASTER_SODETAILS.INVOICE_FROMNO FROM INVOICEMASTER_SODETAILS WHERE INVOICEMASTER_SODETAILS.INVOICE_NO = " & Val(INVNO) & " AND REGISTERMASTER.REGISTER_NAME = '" & registername & "' AND INVOICEMASTER_SODETAILS.INVOICE_YEARID = " & YearId & ") GROUP BY SO_QTY, SO_MTRS, SO_ORDERON")
                    If DTSO.Rows.Count > 0 Then
                        OBJ.DataDefinition.FormulaFields("SOQTY").Text = Val(DTSO.Rows(0).Item("SOQTY"))
                        OBJ.DataDefinition.FormulaFields("ISSQTY").Text = Val(DTSO.Rows(0).Item("ISSQTY"))
                        OBJ.DataDefinition.FormulaFields("BALQTY").Text = Val(DTSO.Rows(0).Item("SOQTY")) - Val(DTSO.Rows(0).Item("ISSQTY"))
                    End If
                    OBJ.DataDefinition.FormulaFields("INVOICECOPYNAME").Text = "'" & INVOICECOPYNAME & "'"
                    If DIRECTMAIL = True Or DIRECTWHATSAPP = True Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 1
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
            ElseIf FRMSTRING = "QUOTATION" Then
                strsearch = strsearch & "{PROFORMAINVOICEMASTER.INVOICE_no}=" & Val(INVNO) & " and {REGISTERMASTER.REGISTER_NAME} = '" & registername & "' and {PROFORMAINVOICEMASTER.INVOICE_yearid}=" & YearId
                OBJ = New ProformaReport_LAXMI
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
                    documentProcessor.LoadDocument(PATH)
                    pdfViewer.LoadDocument(PATH)

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
            ElseIf FRMSTRING = "QUOTATION" Then
                tempattachment = "QUOTATION"
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
            ElseIf FRMSTRING = "QUOTATION" Then
                objmail.subject = "Quotation"
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
            If FRMSTRING = "INVOICE" Or FRMSTRING = "EXPBUYER" Or FRMSTRING = "EXPCUSTOM" Or FRMSTRING = "EXPGST" Then

                oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE.PDF"
                expo = OBJ.ExportOptions

                If INVOICERETAIL = True Then
                    If BLANKPAPER = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                End If
                If ClientName = "DRDRAPES" And PDFWITHDIGITALSIGN = True Then OBJ.DataDefinition.FormulaFields("DIGITALSIGN").Text = 1

                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                If ClientName = "DRDRAPES" And PDFWITHDIGITALSIGN = True Then OBJ.DataDefinition.FormulaFields("DIGITALSIGN").Text = 0


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


            Else
                If FRMSTRING = "PROFORMAINVOICE" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\PROFORMA.PDF"
                ElseIf FRMSTRING = "QUOTATION" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\QUOTATION.PDF"
                ElseIf FRMSTRING = "YARNDO" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\YARNDO.PDF"
                Else
                    oDfDopt.DiskFileName = Application.StartupPath & "\SALEREPOORT.PDF"
                End If

                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                expo = OBJ.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                OBJ.Export()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub saledesign_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If DIRECTPRINT = False Then
                OBJ.CLOSE
                OBJ.DISPOSE
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class