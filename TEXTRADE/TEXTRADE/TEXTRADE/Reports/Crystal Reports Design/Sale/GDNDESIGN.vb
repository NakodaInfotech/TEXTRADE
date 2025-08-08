
Imports BL
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Windows.Forms
Imports System.IO

Public Class GDNDESIGN

    Public FORMULA As String
    Public JOBNO As Integer
    Public DTBATCH As DataTable
    Public BLANKPAPER As Boolean = False

    Dim RPTJO_SVS As New JobOutReport_SVS
    Dim RPTJO_SAFFRON As New JOReport_SAFFRON
    Dim RPTJO As New JOReport_COMMON
    Dim RPTJO_MARKIN As New JOReport_MARKIN
    Dim RPTJO_KARAN As New JOReport_KARAN
    Dim RPTJO_CC As New JOReport_CC
    Dim RPTJO_BARKHASHUBHI As New JOReport_BARKHASHUBHI
    Dim RPTJO_BRILLANTO As New JOReport_BRILLANTO
    Dim RPTJO_MOHAN As New JOReport_MOHAN
    Dim RPTJO_PARAS As New JOReport_PARAS
    Dim RPTJO_CUTTING As New JOReportCutting
    Dim RPTJO_GARMENT As New JOReport_GARMENT
    Dim RPTJO_SNCM As New JOReport_SNCM
    Dim RPTJOTAKA_SNCM As New JOReportTakaWise_SNCM
    Dim RPTJO_A5 As New JOReport_A5
    Dim RPTJOBANNER As New JobOutBannerPrintReport



    Dim RPTJI As New JobInReport_COMMON
    Dim RPTJI_SVS As New JobInReport_SVS
    Dim RPTJI_AXIS As New JIReport_AXIS
    Dim RPTJI_SNCM As New JobInReport_SNCM

    Dim RPTGDN As New GDNReport_COMMON
    Dim RPTGDN_BALAJI As New GDNReport_BALAJI
    Dim RPTGDN_PARASMARKIN As New GDNReport_PARASMARKIN
    Dim RPTGDN_CC As New GDNReport_CC
    Dim RPTGDN_KCRAYON As New GDNReport_KCRAYON
    Dim RPTGDN_SVS As New GDNReport_SVS
    Dim RPTGDN_SKF As New GDNReport_SKF
    Dim RPTGDN_SANGHVI As New GDNReport_SANGHVI
    Dim RPTGDN_KDFAB As New GDNReport_KDFAB
    Dim RPTGDN_SAFFRON As New GDNReport_SAFFRON
    Dim RPTGDN_MANINATH As New GDNReport_MANINATH
    Dim RPTGDN_A5 As New GDNReport_A5
    Dim RPTGDN_SHREENAKODA As New GDNReport_SHREENAKODA
    Dim RPTGDN_BRILLANTO As New GDNReport_BRILLANTO
    Dim RPTGDN_AKASHDEEP As New GDNReport_AKASHDEEP
    Dim RPTGDN_SBA As New GDNReport_SBA
    Dim RPTGDN_DRDRAPES As New GDNReport_DRDRAPES
    Dim RPTGDN_AVIS As New GDNReport_AVIS
    Dim RPTGDN_SUPRIYA As New GDNReport_SUPRIYA
    Dim RPTGDN_MOMAI As New GDNReport_MOMAI
    Dim RPTGDN_NTC As New GDNReport_NTC
    Dim RPTGDN_MNIKHILHRITI As New GDNReport_MNIKHILHRITI
    Dim RPTGDN_SOFTAS As New GDNReport_SOFTAS
    Dim RPTGDN_SUPEEMA As New GDNReport_SUPEEMA
    Dim RPTGDN_SNCM As New GDNReport_SNCM
    Dim RPTGDN_GARMENT As New GDNReport_GARMENT
    Dim RPTGDN_REALCORP As New GDNReport_REALCORP
    Dim RPTGDN_AARYA As New GDNReport_AARYA


    Dim RPTTRANSGDN As New GDNTransReport
    Dim RPTTRANSGDN_MOHAN As New GDNTransReport_MOHAN
    Dim RPTTRANSGDN_KDFAB As New GDNTransReport_KDFAB
    Dim RPTTRANSGDN_SAFFRON As New GDNTransReport_SAFFRON
    Dim RPTTRANSGDN_AKASHDEEPSBA As New GDNTransReport_MANIBHADRASBA

    Dim RPTGODOWNTRANSFER As New GodownTransferReport
    Dim RPTBANNER As New GDNBannerPrintReport


    Dim RPTPS As New PackingSlipReport
    Dim RPTPSA4 As New PackingSlipReportA4
    Dim RPTPS_MARKIN As New PackingSlipReport_MARKIN

    Dim RPTGATEPASS As New GatePassReport
    Dim RPTGATEPASS_AVIS As New GatePassReport_AVIS
    Dim RPTGPPACKINGSLIP As New GatePassPackingSlipReport
    Dim RPTGPEXPPACKINGSLIP As New GatePassExpPackingSlipReport


    Dim rptdispatchpartywise As New DispatchReportPartywise
    Dim rptdispatchpartydetails As New DispatchReportPartywiseDetailed
    Dim rptdispatchdesignwise As New DispatchReportDesignwise
    Dim rptdispatchdesigndetails As New DispatchReportDesignwiseDetailed

    Dim RPTPENDINGDTLC As New PendingDetailsReport
    Dim RPTDAILYACT As New DailyActivityReport

    Dim RPTPROFORMA As New ProformaReport


    Dim tempattachment As String
    Dim ConInfo As New CrystalDecisions.Shared.TableLogOnInfo
    Dim expo As New ExportOptions
    ' Dim oDfDopt As New DiskFileDestinationOptions
    Public vendorname As String
    Public agentname As String
    'NEWLY ADDED

    Public FRMSTRING As String
    Public FROMDATE As String
    Public TODATE As String
    Public OPENINGDATE As String
    Public selfor_ss As String
    Public PERIOD As String
    Public HIDEPCSDETAILS As Boolean = False
    Public WHITELABEL As Boolean = False
    Public GSTRPT As Boolean = False
    Public PRINTINYARDS As Boolean = True
    Public JONO As Integer = 0
    Public PRINTRATE As Boolean = False
    Public PARTYCHANGEADD As String

    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1

    Private Sub GDNDESIGN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub GRNDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            Dim OBJCMN As New ClsCommon
            Dim DTNEW As New DataTable

            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            If FRMSTRING = "PARTYWISE" Then
                crpo.ReportSource = rptdispatchpartywise
                crTables = rptdispatchpartywise.Database.Tables
            ElseIf FRMSTRING = "PARTYWISEDETAILS" Then
                crpo.ReportSource = rptdispatchpartydetails
                crTables = rptdispatchpartydetails.Database.Tables
            ElseIf FRMSTRING = "DESIGNWISE" Then
                crpo.ReportSource = rptdispatchdesignwise
                crTables = rptdispatchdesignwise.Database.Tables
            ElseIf FRMSTRING = "DESIGNWISEDETAILS" Then
                crpo.ReportSource = rptdispatchdesigndetails
                crTables = rptdispatchdesigndetails.Database.Tables

            ElseIf FRMSTRING = "JOBOUT" Then

                If JOBOUTA5 = True Then
                    crTables = RPTJO_A5.Database.Tables
                ElseIf ClientName = "SVS" Then
                    crTables = RPTJO_SVS.Database.Tables
                ElseIf ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                    crTables = RPTJO_SAFFRON.Database.Tables
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    crTables = RPTJO_CC.Database.Tables
                ElseIf ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                    crTables = RPTJO_BARKHASHUBHI.Database.Tables
                ElseIf ClientName = "MARKIN" Then
                    crTables = RPTJO_MARKIN.Database.Tables
                ElseIf ClientName = "BRILLANTO" Then
                    crTables = RPTJO_BRILLANTO.Database.Tables
                ElseIf ClientName = "MOHAN" Then
                    crTables = RPTJO_MOHAN.Database.Tables
                ElseIf ClientName = "KARAN" Then
                    crTables = RPTJO_KARAN.Database.Tables
                ElseIf ClientName = "PARAS" Then
                    crTables = RPTJO_PARAS.Database.Tables
                ElseIf ClientName = "SNCM" Then
                    crTables = RPTJO_SNCM.Database.Tables
                Else
                    crTables = RPTJO.Database.Tables
                End If

            ElseIf FRMSTRING = "JOTAKADETAILS" Then
                crTables = RPTJOTAKA_SNCM.Database.Tables

            ElseIf FRMSTRING = "JOBOUTBANNER" Then
                crTables = RPTJOBANNER.Database.Tables

            ElseIf FRMSTRING = "JOBOUTGARMENT" Then
                crTables = RPTJO_GARMENT.Database.Tables

            ElseIf FRMSTRING = "GDNBANNER" Then
                crTables = RPTBANNER.Database.Tables


            ElseIf FRMSTRING = "JOBCUTTING" Then
                crTables = RPTJO_CUTTING.Database.Tables

            ElseIf FRMSTRING = "JOBIN" Then
                If ClientName = "AXIS" Then
                    crTables = RPTJI_AXIS.Database.Tables
                ElseIf ClientName = "SVS" Then
                    crTables = RPTJI_SVS.Database.Tables
                Else
                    crTables = RPTJI.Database.Tables
                End If
            ElseIf FRMSTRING = "JOBINPS" Then
                crTables = RPTJI_SNCM.Database.Tables

            ElseIf FRMSTRING = "PACKINGSLIP" Then
                If ClientName = "MARKIN" Then
                    crTables = RPTPS_MARKIN.Database.Tables
                ElseIf ClientName = "KOTHARI" Then
                    crTables = RPTPSA4.Database.Tables
                Else
                    crTables = RPTPS.Database.Tables
                End If

            ElseIf FRMSTRING = "GATEPASS" Then
                If ClientName = "AVIS" Or ClientName = "RADHA" Then
                    crTables = RPTGATEPASS_AVIS.Database.Tables
                Else
                    crTables = RPTGATEPASS.Database.Tables
                End If
            ElseIf FRMSTRING = "GPPACKINGSLIP" Then
                crTables = RPTGPPACKINGSLIP.Database.Tables
            ElseIf FRMSTRING = "GPEXPPACKINGSLIP" Then
                crTables = RPTGPEXPPACKINGSLIP.Database.Tables

            ElseIf FRMSTRING = "GDNGARMENT" Then
                crTables = RPTGDN_GARMENT.Database.Tables

            ElseIf FRMSTRING = "GDN" Then
                If ClientName = "SVS" Then
                    crTables = RPTGDN_SVS.Database.Tables
                ElseIf ClientName = "BALAJI" Or ClientName = "NAYRA" Then
                    crTables = RPTGDN_BALAJI.Database.Tables
                ElseIf ClientName = "SOFTAS" Then
                    crTables = RPTGDN_SOFTAS.Database.Tables
                ElseIf ClientName = "SKF" Then
                    crTables = RPTGDN_SKF.Database.Tables
                ElseIf ClientName = "KDFAB" Then
                    crTables = RPTGDN_KDFAB.Database.Tables
                ElseIf ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                    crTables = RPTGDN_SAFFRON.Database.Tables
                ElseIf ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                    crTables = RPTGDN_SANGHVI.Database.Tables
                ElseIf ClientName = "MANINATH" Then
                    crTables = RPTGDN_MANINATH.Database.Tables
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    crTables = RPTGDN_CC.Database.Tables
                ElseIf ClientName = "KCRAYON" Then
                    crTables = RPTGDN_KCRAYON.Database.Tables
                ElseIf ClientName = "MAFATLAL" Then
                    crTables = RPTGDN_A5.Database.Tables
                ElseIf ClientName = "SUPRIYA" Then
                    crTables = RPTGDN_SUPRIYA.Database.Tables
                ElseIf ClientName = "SUPEEMA" Then
                    crTables = RPTGDN_SUPEEMA.Database.Tables
                ElseIf ClientName = "BRILLANTO" Then
                    crTables = RPTGDN_BRILLANTO.Database.Tables
                ElseIf ClientName = "AKASHDEEP" Then
                    crTables = RPTGDN_AKASHDEEP.Database.Tables
                ElseIf ClientName = "SBA" Then
                    crTables = RPTGDN_SBA.Database.Tables
                ElseIf ClientName = "DRDRAPES" Then
                    crTables = RPTGDN_DRDRAPES.Database.Tables
                ElseIf ClientName = "AVIS" Then
                    crTables = RPTGDN_AVIS.Database.Tables
                ElseIf ClientName = "PARAS" Or ClientName = "MARKIN" Then
                    crTables = RPTGDN_PARASMARKIN.Database.Tables
                ElseIf ClientName = "MOMAI" Then
                    crTables = RPTGDN_MOMAI.Database.Tables
                ElseIf ClientName = "NTC" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "KUNAL" Or ClientName = "SURYODAYA" Or ClientName = "SSC" Or ClientName = "VALIANT" Then
                    crTables = RPTGDN_NTC.Database.Tables
                ElseIf ClientName = "MNIKHIL" Or ClientName = "HRITI" Then
                    crTables = RPTGDN_MNIKHILHRITI.Database.Tables
                ElseIf ClientName = "SHREENAKODA" Then
                    crTables = RPTGDN_SHREENAKODA.Database.Tables
                ElseIf ClientName = "SNCM" Then
                    crTables = RPTGDN_SNCM.Database.Tables
                ElseIf ClientName = "REALCORPORATION" Then
                    crTables = RPTGDN_REALCORP.Database.Tables
                ElseIf ClientName = "BARKHA" Then
                    crTables = RPTGDN_A5.Database.Tables
                ElseIf ClientName = "AARYA" Then
                    crTables = RPTGDN_AARYA.Database.Tables
                Else
                    crTables = RPTGDN.Database.Tables
                End If


            ElseIf FRMSTRING = "PROFORMA" Then
                crTables = RPTPROFORMA.Database.Tables

            ElseIf FRMSTRING = "GODOWNTRANSFER" Then
                crTables = RPTGODOWNTRANSFER.Database.Tables


            ElseIf FRMSTRING = "TRANSGDN" Then
                If ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                    crTables = RPTTRANSGDN_SAFFRON.Database.Tables
                ElseIf ClientName = "KDFAB" Then
                    crTables = RPTTRANSGDN_KDFAB.Database.Tables
                ElseIf ClientName = "MOHAN" Then
                    crTables = RPTTRANSGDN_MOHAN.Database.Tables
                ElseIf ClientName = "AKASHDEEP" Or ClientName = "SBA" Then
                    crTables = RPTTRANSGDN_AKASHDEEPSBA.Database.Tables
                Else
                    crTables = RPTTRANSGDN.Database.Tables
                End If

            ElseIf FRMSTRING = "PENDINGDETAILS" Then
                crpo.ReportSource = RPTPENDINGDTLC
                crTables = RPTPENDINGDTLC.Database.Tables

            ElseIf FRMSTRING = "DAILYACTIVITY" Then
                crpo.ReportSource = RPTDAILYACT
                crTables = RPTDAILYACT.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            If FRMSTRING = "GDN" Or FRMSTRING = "GDNGARMENT" Or FRMSTRING = "PROFORMA" Or FRMSTRING = "TRANSGDN" Or FRMSTRING = "JOBOUT" Or FRMSTRING = "JOTAKADETAILS" Or FRMSTRING = "JOBOUTBANNER" Or FRMSTRING = "JOBCUTTING" Or FRMSTRING = "JOBIN" Or FRMSTRING = "JOBINPS" Or FRMSTRING = "PACKINGSLIP" Or FRMSTRING = "GATEPASS" Or FRMSTRING = "GPPACKINGSLIP" Or FRMSTRING = "GPEXPPACKINGSLIP" Or FRMSTRING = "GODOWNTRANSFER" Or FRMSTRING = "GDNBANNER" Or FRMSTRING = "JOBOUTGARMENT" Or FRMSTRING = "EMBPRODUCTION" Then crpo.SelectionFormula = FORMULA
            If FRMSTRING = "PARTYWISE" Or FRMSTRING = "PARTYWISEDETAILS" Or FRMSTRING = "DESIGNWISE" Or FRMSTRING = "DESIGNWISEDETAILS" Or FRMSTRING = "PENDINGDETAILS" Or FRMSTRING = "DAILYACTIVITY" Then crpo.SelectionFormula = selfor_ss


            If FRMSTRING = "JOBOUT" Then
                If JOBOUTA5 = True Then
                    crpo.ReportSource = RPTJO_A5
                    If HIDEPCSDETAILS = True Then RPTJO_A5.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else RPTJO_A5.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                ElseIf ClientName = "SVS" Then
                    crpo.ReportSource = RPTJO_SVS
                ElseIf ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                    crpo.ReportSource = RPTJO_SAFFRON
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    crpo.ReportSource = RPTJO_CC
                ElseIf ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                    crpo.ReportSource = RPTJO_BARKHASHUBHI
                    If GSTRPT = True Then RPTJO_BARKHASHUBHI.DataDefinition.FormulaFields("GSTRPT").Text = 1 Else RPTJO_BARKHASHUBHI.DataDefinition.FormulaFields("GSTRPT").Text = 0
                ElseIf ClientName = "MARKIN" Then
                    If GSTRPT = True Then RPTJO_MARKIN.DataDefinition.FormulaFields("GSTRPT").Text = 1 Else RPTJO_MARKIN.DataDefinition.FormulaFields("GSTRPT").Text = 0
                    crpo.ReportSource = RPTJO_MARKIN
                ElseIf ClientName = "BRILLANTO" Then
                    crpo.ReportSource = RPTJO_BRILLANTO
                ElseIf ClientName = "MOHAN" Then
                    crpo.ReportSource = RPTJO_MOHAN
                ElseIf ClientName = "KARAN" Then
                    crpo.ReportSource = RPTJO_KARAN
                ElseIf ClientName = "PARAS" Then
                    crpo.ReportSource = RPTJO_PARAS
                ElseIf ClientName = "SNCM" Then
                    crpo.ReportSource = RPTJO_SNCM
                Else
                    crpo.ReportSource = RPTJO
                    If GSTRPT = True Then RPTJO.DataDefinition.FormulaFields("GSTRPT").Text = 1 Else RPTJO.DataDefinition.FormulaFields("GSTRPT").Text = 0
                    If HIDEPCSDETAILS = True Then RPTJO.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else RPTJO.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                    If (ClientName = "ANOX" Or ClientName = "SURYODAYA") AndAlso MsgBox("Print Images?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then RPTJO.DataDefinition.FormulaFields("SHOWIMAGE").Text = "1"
                End If

            ElseIf FRMSTRING = "JOTAKADETAILS" Then
                crpo.ReportSource = RPTJOTAKA_SNCM

            ElseIf FRMSTRING = "JOBOUTBANNER" Then
                crpo.ReportSource = RPTJOBANNER

            ElseIf FRMSTRING = "JOBOUTGARMENT" Then
                crpo.ReportSource = RPTJO_GARMENT

            ElseIf FRMSTRING = "GDNBANNER" Then
                crpo.ReportSource = RPTBANNER

            ElseIf FRMSTRING = "JOBCUTTING" Then
                crpo.ReportSource = RPTJO_CUTTING

            ElseIf FRMSTRING = "JOBIN" Then
                If ClientName = "AXIS" Then
                    crpo.ReportSource = RPTJI_AXIS
                ElseIf ClientName = "SVS" Then
                    crpo.ReportSource = RPTJI_SVS
                Else
                    crpo.ReportSource = RPTJI
                End If
            ElseIf FRMSTRING = "JOBINPS" Then
                crpo.ReportSource = RPTJI_SNCM
                If BLANKPAPER = True Then RPTJI_SNCM.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTJI_SNCM.DataDefinition.FormulaFields("WHITELABEL").Text = 0

            ElseIf FRMSTRING = "PARTYWISE" Then
                crpo.ReportSource = rptdispatchpartywise
            ElseIf FRMSTRING = "PARTYWISEDETAILS" Then
                crpo.ReportSource = rptdispatchpartydetails
            ElseIf FRMSTRING = "DESIGNWISE" Then
                crpo.ReportSource = rptdispatchdesignwise
            ElseIf FRMSTRING = "DESIGNWISEDETAILS" Then
                crpo.ReportSource = rptdispatchdesigndetails

            ElseIf FRMSTRING = "PACKINGSLIP" Then
                If ClientName = "MARKIN" Then
                    crpo.ReportSource = RPTPS_MARKIN
                    If WHITELABEL = True Then RPTPS_MARKIN.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                ElseIf ClientName = "KOTHARI" Then
                    crpo.ReportSource = RPTPSA4
                    If WHITELABEL = True Then RPTPSA4.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                Else
                    crpo.ReportSource = RPTPS
                    If WHITELABEL = True Then RPTPS.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                End If
            ElseIf FRMSTRING = "GATEPASS" Then
                If ClientName = "AVIS" Or ClientName = "RADHA" Then
                    crpo.ReportSource = RPTGATEPASS_AVIS
                Else
                    RPTGATEPASS.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    crpo.ReportSource = RPTGATEPASS
                End If
            ElseIf FRMSTRING = "GPPACKINGSLIP" Then
                crpo.ReportSource = RPTGPPACKINGSLIP
            ElseIf FRMSTRING = "GPEXPPACKINGSLIP" Then
                crpo.ReportSource = RPTGPEXPPACKINGSLIP

            ElseIf FRMSTRING = "GDNGARMENT" Then
                If WHITELABEL = True Then RPTGDN_GARMENT.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTGDN_GARMENT.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                If HIDEPCSDETAILS = True Then RPTGDN_GARMENT.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else RPTGDN_GARMENT.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                If PRINTRATE = True Then RPTGDN_GARMENT.DataDefinition.FormulaFields("PRINTRATE").Text = 1 Else RPTGDN_GARMENT.DataDefinition.FormulaFields("PRINTRATE").Text = 0
                If ClientName = "ALENCOT" Or ClientName = "MANSI" Or ClientName = "CHINTAN" Then RPTGDN_GARMENT.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                RPTGDN_GARMENT.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                crpo.ReportSource = RPTGDN_GARMENT

            ElseIf FRMSTRING = "GDN" Then
                If ClientName = "SVS" Then
                    crpo.ReportSource = RPTGDN_SVS
                ElseIf ClientName = "BALAJI" Or ClientName = "NAYRA" Then
                    If WHITELABEL = True Then RPTGDN_BALAJI.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTGDN_BALAJI.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    If HIDEPCSDETAILS = True Then RPTGDN_BALAJI.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else RPTGDN_BALAJI.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                    RPTGDN_BALAJI.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    RPTGDN_BALAJI.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    crpo.ReportSource = RPTGDN_BALAJI
                ElseIf ClientName = "SOFTAS" Then
                    If WHITELABEL = True Then RPTGDN_SOFTAS.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTGDN_SOFTAS.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    If HIDEPCSDETAILS = True Then RPTGDN_SOFTAS.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else RPTGDN_SOFTAS.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                    RPTGDN_SOFTAS.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    crpo.ReportSource = RPTGDN_SOFTAS
                ElseIf ClientName = "SKF" Then
                    crpo.ReportSource = RPTGDN_SKF
                ElseIf ClientName = "KDFAB" Then
                    crpo.ReportSource = RPTGDN_KDFAB
                ElseIf ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                    crpo.ReportSource = RPTGDN_SAFFRON
                ElseIf ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                    crpo.ReportSource = RPTGDN_SANGHVI
                ElseIf ClientName = "MANINATH" Then
                    crpo.ReportSource = RPTGDN_MANINATH
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    crpo.ReportSource = RPTGDN_CC
                ElseIf ClientName = "MAFATLAL" Then
                    crpo.ReportSource = RPTGDN_A5
                    If HIDEPCSDETAILS = True Then RPTGDN_A5.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else RPTGDN_A5.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                ElseIf ClientName = "SUPRIYA" Then
                    crpo.ReportSource = RPTGDN_SUPRIYA
                ElseIf ClientName = "SUPEEMA" Then
                    crpo.ReportSource = RPTGDN_SUPEEMA
                ElseIf ClientName = "BRILLANTO" Then
                    crpo.ReportSource = RPTGDN_BRILLANTO
                ElseIf ClientName = "AKASHDEEP" Then
                    crpo.ReportSource = RPTGDN_AKASHDEEP
                ElseIf ClientName = "SBA" Then
                    crpo.ReportSource = RPTGDN_SBA
                ElseIf ClientName = "DRDRAPES" Then
                    crpo.ReportSource = RPTGDN_DRDRAPES
                    If WHITELABEL = True Then RPTGDN_DRDRAPES.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                ElseIf ClientName = "KCRAYON" Then
                    crpo.ReportSource = RPTGDN_KCRAYON
                    If HIDEPCSDETAILS = True Then RPTGDN_KCRAYON.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1
                ElseIf ClientName = "AVIS" Then
                    If WHITELABEL = True Then RPTGDN_AVIS.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                    If HIDEPCSDETAILS = True Then RPTGDN_AVIS.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1
                    crpo.ReportSource = RPTGDN_AVIS
                ElseIf ClientName = "PARAS" Or ClientName = "MARKIN" Then
                    If WHITELABEL = True Then RPTGDN_PARASMARKIN.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTGDN_PARASMARKIN.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    If HIDEPCSDETAILS = True Then RPTGDN_PARASMARKIN.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else RPTGDN_PARASMARKIN.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                    crpo.ReportSource = RPTGDN_PARASMARKIN
                ElseIf ClientName = "MOMAI" Then
                    crpo.ReportSource = RPTGDN_MOMAI
                ElseIf ClientName = "NTC" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "KUNAL" Or ClientName = "SURYODAYA" Or ClientName = "SSC" Or ClientName = "VALIANT" Then
                    If WHITELABEL = True Then RPTGDN_NTC.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                    If HIDEPCSDETAILS = True Then RPTGDN_NTC.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1
                    If PRINTRATE = True Then RPTGDN_NTC.DataDefinition.FormulaFields("PRINTRATE").Text = 1 Else RPTGDN_NTC.DataDefinition.FormulaFields("PRINTRATE").Text = 0
                    crpo.ReportSource = RPTGDN_NTC
                ElseIf ClientName = "MNIKHIL" Or ClientName = "HRITI" Then
                    crpo.ReportSource = RPTGDN_MNIKHILHRITI
                ElseIf ClientName = "SHREENAKODA" Then
                    If WHITELABEL = True Then RPTGDN_SHREENAKODA.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                    If HIDEPCSDETAILS = True Then RPTGDN_SHREENAKODA.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1
                    crpo.ReportSource = RPTGDN_SHREENAKODA
                ElseIf ClientName = "SNCM" Then
                    crpo.ReportSource = RPTGDN_SNCM
                    If PARTYCHANGEADD <> "" Then RPTGDN.DataDefinition.FormulaFields("SHIPPINGADD").Text = "'" & PARTYCHANGEADD & "'"
                    If PRINTRATE = True Then RPTGDN_SNCM.DataDefinition.FormulaFields("PRINTRATE").Text = 1 Else RPTGDN_SNCM.DataDefinition.FormulaFields("PRINTRATE").Text = 0
                ElseIf ClientName = "REALCORPORATION" Then
                    crpo.ReportSource = RPTGDN_REALCORP
                ElseIf ClientName = "AARYA" Then
                    crpo.ReportSource = RPTGDN_AARYA
                ElseIf ClientName = "BARKHA" Then
                    crpo.ReportSource = RPTGDN_A5
                Else
                    If PRINTINYARDS = True Then
                        RPTGDN.DataDefinition.FormulaFields("PRINTINYARDS").Text = 1
                        RPTGDN.Subreports(0).DataDefinition.FormulaFields("PRINTINYARDS").Text = 1
                    Else
                        RPTGDN.DataDefinition.FormulaFields("PRINTINYARDS").Text = 0
                        RPTGDN.Subreports(0).DataDefinition.FormulaFields("PRINTINYARDS").Text = 0
                    End If
                    If PARTYCHANGEADD <> "" Then RPTGDN.DataDefinition.FormulaFields("SHIPPINGADD").Text = "'" & PARTYCHANGEADD & "'"
                    If WHITELABEL = True Then RPTGDN.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTGDN.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    If HIDEPCSDETAILS = True Then RPTGDN.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else RPTGDN.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                    If PRINTRATE = True Then RPTGDN.DataDefinition.FormulaFields("PRINTRATE").Text = 1 Else RPTGDN.DataDefinition.FormulaFields("PRINTRATE").Text = 0
                    If ClientName = "ALENCOT" Or ClientName = "MANSI" Or ClientName = "SIMPLEX" Or ClientName = "CHINTAN" Or ClientName = "SIDDHPOLYCOT" Then RPTGDN.DataDefinition.FormulaFields("SENDMAIL").Text = "1"

                    RPTGDN.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    crpo.ReportSource = RPTGDN
                End If


            ElseIf FRMSTRING = "PROFORMA" Then
                crpo.ReportSource = RPTPROFORMA

            ElseIf FRMSTRING = "GODOWNTRANSFER" Then
                crpo.ReportSource = RPTGODOWNTRANSFER


            ElseIf FRMSTRING = "TRANSGDN" Then
                If ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                    crpo.ReportSource = RPTTRANSGDN_SAFFRON
                ElseIf ClientName = "KDFAB" Then
                    crpo.ReportSource = RPTTRANSGDN_KDFAB
                ElseIf ClientName = "MOHAN" Then
                    crpo.ReportSource = RPTTRANSGDN_MOHAN
                ElseIf ClientName = "AKASHDEEP" Or ClientName = "SBA" Then
                    crpo.ReportSource = RPTTRANSGDN_AKASHDEEPSBA
                Else
                    crpo.ReportSource = RPTTRANSGDN
                End If

            ElseIf FRMSTRING = "PENDINGDETAILS" Then
                crpo.ReportSource = RPTPENDINGDTLC

            ElseIf FRMSTRING = "DAILYACTIVITY" Then
                crpo.ReportSource = RPTDAILYACT
                RPTDAILYACT.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            End If

            '************************ END *******************
            crpo.Zoom(100)
            crpo.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Try
            Dim emailid As String = ""
            Dim EMAILID1 As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()


            If vendorname <> "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" acc_email ", "", " LEDGERS ", " and ACC_cmpname='" & vendorname & "' and ACC_cmpid=" & CmpId & " and ACC_LOCATIONid=" & Locationid & " and ACC_YEARid=" & YearId)
                If dt.Rows.Count > 0 Then
                    emailid = dt.Rows(0).Item(0).ToString
                End If
            End If

            If agentname <> "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" acc_email ", "", " LEDGERS ", " and ACC_cmpname='" & agentname & "' and ACC_cmpid=" & CmpId & " and ACC_LOCATIONid=" & Locationid & " and ACC_YEARid=" & YearId)
                If dt.Rows.Count > 0 Then
                    EMAILID1 = dt.Rows(0).Item(0).ToString
                End If
            End If
            Dim objmail As New SendMail

            'tempattachment = "GDN"
            If FRMSTRING = "JOBIN" Then
                tempattachment = "JOBIN"
                objmail.subject = "Job In"
            ElseIf FRMSTRING = "JOBINPS" Then
                tempattachment = "JOBINPS"
                objmail.subject = "Job In Packing Slip"
            ElseIf FRMSTRING = "JOBOUT" Or FRMSTRING = "JOBCUTTING" Or FRMSTRING = "JOBOUTGARMENT" Or FRMSTRING = "EMBPRODUCTION" Then
                tempattachment = "JOBOUT"
                objmail.subject = "Job Challan"
            ElseIf FRMSTRING = "GDN" Or FRMSTRING = "GDNGARMENT" Then
                tempattachment = "GDN"
                objmail.subject = "Challan"
            ElseIf FRMSTRING = "TRANSGDN" Then
                tempattachment = "TRANSGDN"
                objmail.subject = "Transport Challan"
            ElseIf FRMSTRING = "PROFORMA" Then
                tempattachment = "PROFORMA"
                objmail.subject = "Proforma"
            ElseIf FRMSTRING = "GODOWNTRANSFER" Then
                tempattachment = "GODOWNTRANSFER"
                objmail.subject = "Godown Transfer Voucher"
            ElseIf FRMSTRING = "PENDINGDETAILS" Then
                tempattachment = "PENDINGDETAILS"
                objmail.subject = "PENDINGDETAILS"
            ElseIf FRMSTRING = "DAILYACTIVITY" Then
                tempattachment = "DAILYACTIVITY"
                objmail.subject = "DAILYACTIVITY"
            ElseIf FRMSTRING = "GATEPASS" Then
                tempattachment = "GATEPASS"
                objmail.subject = "GATEPASS"
            ElseIf FRMSTRING = "GPPACKINGSLIP" Or FRMSTRING = "GPEXPPACKINGSLIP" Then
                tempattachment = "PACKINGSLIP"
                objmail.subject = "PACKINGSLIP"
            End If

            objmail.attachment = tempattachment
            objmail.attachment = Application.StartupPath & "\" & tempattachment & ".PDF"

            If emailid <> "" Then
                objmail.cmbfirstadd.Text = emailid
            End If

            If EMAILID1 <> "" Then
                objmail.cmbsecondadd.Text = EMAILID1
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
            Dim oDfDopt As New DiskFileDestinationOptions

            If FRMSTRING = "GDN" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\GDN.PDF"

                If ClientName = "SVS" Then

                    expo = RPTGDN_SVS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_SVS.Export()

                ElseIf ClientName = "BALAJI" Then

                    expo = RPTGDN_BALAJI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_BALAJI.Export()

                ElseIf ClientName = "SOFTAS" Then

                    expo = RPTGDN_SOFTAS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_SOFTAS.Export()

                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then

                    expo = RPTGDN_CC.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_CC.Export()

                ElseIf ClientName = "MAFATLAL" Then

                    expo = RPTGDN_A5.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_A5.Export()

                ElseIf ClientName = "BRILLANTO" Then

                    expo = RPTGDN_BRILLANTO.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_BRILLANTO.Export()

                ElseIf ClientName = "AKASHDEEP" Then

                    expo = RPTGDN_AKASHDEEP.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_AKASHDEEP.Export()

                ElseIf ClientName = "SBA" Then

                    expo = RPTGDN_SBA.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_SBA.Export()

                ElseIf ClientName = "DRDRAPES" Then

                    expo = RPTGDN_DRDRAPES.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_DRDRAPES.Export()

                ElseIf ClientName = "SKF" Then
                    expo = RPTGDN_SKF.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_SKF.Export()

                ElseIf ClientName = "KDFAB" Then
                    expo = RPTGDN_KDFAB.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_KDFAB.Export()

                ElseIf ClientName = "SAFFRON" Then
                    expo = RPTGDN_SAFFRON.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_SAFFRON.Export()

                ElseIf ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                    expo = RPTGDN_SANGHVI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_SANGHVI.Export()

                ElseIf ClientName = "MANINATH" Then
                    expo = RPTGDN_MANINATH.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_MANINATH.Export()

                ElseIf ClientName = "KCRAYON" Then
                    expo = RPTGDN_KCRAYON.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_KCRAYON.Export()

                ElseIf ClientName = "AVIS" Then
                    expo = RPTGDN_AVIS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_AVIS.Export()

                ElseIf ClientName = "SUPRIYA" Then
                    expo = RPTGDN_SUPRIYA.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_SUPRIYA.Export()

                ElseIf ClientName = "SUPEEMA" Then
                    expo = RPTGDN_SUPEEMA.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_SUPEEMA.Export()

                ElseIf ClientName = "PARAS" Or ClientName = "MARKIN" Then
                    expo = RPTGDN_PARASMARKIN.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_PARASMARKIN.Export()

                ElseIf ClientName = "MOMAI" Then
                    expo = RPTGDN_MOMAI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_MOMAI.Export()

                ElseIf ClientName = "NTC" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "KUNAL" Or ClientName = "SURYODAYA" Or ClientName = "SSC" Or ClientName = "VALIANT" Then
                    expo = RPTGDN_NTC.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_NTC.Export()

                ElseIf ClientName = "MNIKHIL" Or ClientName = "HRITI" Then
                    expo = RPTGDN_MNIKHILHRITI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_MNIKHILHRITI.Export()

                ElseIf ClientName = "SHREENAKODA" Then
                    expo = RPTGDN_SHREENAKODA.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_SHREENAKODA.Export()

                ElseIf ClientName = "SNCM" Then
                    RPTGDN_SNCM.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    expo = RPTGDN_SNCM.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_SNCM.Export()
                    RPTGDN_SNCM.DataDefinition.FormulaFields("SENDMAIL").Text = "0"

                ElseIf ClientName = "REALCORPORATION" Then
                    RPTGDN_REALCORP.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    expo = RPTGDN_REALCORP.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_REALCORP.Export()
                    RPTGDN_REALCORP.DataDefinition.FormulaFields("SENDMAIL").Text = "0"

                ElseIf ClientName = "AARYA" Then
                    RPTGDN_AARYA.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    expo = RPTGDN_AARYA.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_AARYA.Export()
                    RPTGDN_AARYA.DataDefinition.FormulaFields("SENDMAIL").Text = "0"

                ElseIf ClientName = "BARKHA" Then
                    RPTGDN_A5.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    expo = RPTGDN_A5.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN_A5.Export()
                    RPTGDN_A5.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                Else
                    RPTGDN.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    RPTGDN.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    expo = RPTGDN.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGDN.Export()
                    RPTGDN.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                End If

            ElseIf FRMSTRING = "GDNGARMENT" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\GDN.PDF"
                RPTGDN_GARMENT.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                RPTGDN_GARMENT.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                expo = RPTGDN_GARMENT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGDN_GARMENT.Export()
                RPTGDN_GARMENT.DataDefinition.FormulaFields("SENDMAIL").Text = "0"

            ElseIf FRMSTRING = "GDNBANNER" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\GDNBANNER.PDF"
                expo = RPTBANNER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBANNER.Export()



            ElseIf FRMSTRING = "TRANSGDN" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\TRANSGDN.PDF"

                If ClientName = "MOHAN" Then
                    expo = RPTTRANSGDN_MOHAN.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTTRANSGDN_MOHAN.Export()
                End If

            ElseIf FRMSTRING = "PROFORMA" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PROFORMA.PDF"
                expo = RPTPROFORMA.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPROFORMA.Export()

            ElseIf FRMSTRING = "GODOWNTRANSFER" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\GODOWNTRANSFER.PDF"
                expo = RPTGODOWNTRANSFER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGODOWNTRANSFER.Export()

            ElseIf FRMSTRING = "GATEPASS" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\GATEPASS.PDF"
                If ClientName = "AVIS" Or ClientName = "RADHA" Then
                    expo = RPTGATEPASS_AVIS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGATEPASS_AVIS.Export()
                Else
                    expo = RPTGATEPASS.ExportOptions
                    RPTGATEPASS.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGATEPASS.Export()
                End If

            ElseIf FRMSTRING = "GPPACKINGSLIP" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PACKINGSLIP.PDF"
                expo = RPTGPPACKINGSLIP.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGPPACKINGSLIP.Export()
            ElseIf FRMSTRING = "GPEXPPACKINGSLIP" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PACKINGSLIP.PDF"
                expo = RPTGPEXPPACKINGSLIP.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGPEXPPACKINGSLIP.Export()


            ElseIf FRMSTRING = "JOBIN" Then
                If ClientName = "SVS" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\JOBIN.pdf"
                    expo = RPTJI_SVS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJI_SVS.Export()
                ElseIf ClientName = "AXIS" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\JOBIN.pdf"
                    expo = RPTJI_AXIS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJI_AXIS.Export()
                Else
                    oDfDopt.DiskFileName = Application.StartupPath & "\JOBIN.pdf"
                    expo = RPTJI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJI.Export()
                End If
            ElseIf FRMSTRING = "JOBINPS" Then
                If ClientName = "SNCM" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\JOBINPS.pdf"
                    expo = RPTJI_SNCM.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJI_SNCM.Export()
                End If

            ElseIf FRMSTRING = "JOBCUTTING" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\JOBOUT.pdf"
                expo = RPTJO_CUTTING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJO_CUTTING.Export()

            ElseIf FRMSTRING = "JOBOUTGARMENT" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\JOBOUT.pdf"
                expo = RPTJO_GARMENT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJO_GARMENT.Export()

            ElseIf FRMSTRING = "JOBOUT" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\JOBOUT.pdf"
                If JOBOUTA5 = True Then
                    expo = RPTJO_A5.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJO_A5.Export()
                ElseIf ClientName = "SVS" Then
                    expo = RPTJO_SVS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJO_SVS.Export()
                ElseIf ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                    expo = RPTJO_SAFFRON.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJO_SAFFRON.Export()
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    expo = RPTJO_CC.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJO_CC.Export()
                ElseIf ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                    expo = RPTJO_BARKHASHUBHI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJO_BARKHASHUBHI.Export()
                ElseIf ClientName = "MARKIN" Then
                    expo = RPTJO_MARKIN.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJO_MARKIN.Export()
                ElseIf ClientName = "BRILLANTO" Then
                    expo = RPTJO_BRILLANTO.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJO_BRILLANTO.Export()
                ElseIf ClientName = "MOHAN" Then
                    expo = RPTJO_MOHAN.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJO_MOHAN.Export()
                ElseIf ClientName = "KARAN" Then
                    expo = RPTJO_KARAN.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJO_KARAN.Export()
                ElseIf ClientName = "PARAS" Then
                    expo = RPTJO_PARAS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJO_PARAS.Export()
                ElseIf ClientName = "SNCM" Then
                    expo = RPTJO_SNCM.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJO_SNCM.Export()
                Else
                    expo = RPTJO.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTJO.Export()
                End If

            ElseIf FRMSTRING = "JOBOUTBANNER" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\JOBOUTBANNER.PDF"
                expo = RPTJOBANNER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJOBANNER.Export()

            ElseIf FRMSTRING = "PENDINGDETAILS" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PENDINGDETAILS.PDF"
                expo = RPTPENDINGDTLC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPENDINGDTLC.Export()

            ElseIf FRMSTRING = "DAILYACTIVITY" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\DAILYACTIVITY.PDF"
                expo = RPTDAILYACT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDAILYACT.Export()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
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


            Dim OBJ As New Object

            If FRMSTRING = "JOBOUT" Then

                If JOBOUTA5 = True Then
                    OBJ = New JOReport_A5
                ElseIf ClientName = "SVS" Then
                    OBJ = New JobOutReport_SVS
                ElseIf ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                    OBJ = New JOReport_SAFFRON
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    OBJ = New JOReport_CC
                ElseIf ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                    OBJ = New JOReport_BARKHASHUBHI
                ElseIf ClientName = "MARKIN" Then
                    OBJ = New JOReport_MARKIN
                ElseIf ClientName = "BRILLANTO" Then
                    OBJ = New JOReport_BRILLANTO
                ElseIf ClientName = "MOHAN" Then
                    OBJ = New JOReport_MOHAN
                ElseIf ClientName = "KARAN" Then
                    OBJ = New JOReport_KARAN
                ElseIf ClientName = "PARAS" Then
                    OBJ = New JOReport_PARAS
                ElseIf ClientName = "SNCM" Then
                    OBJ = New JOReport_SNCM
                Else
                    OBJ = New JOReport_COMMON
                    If ClientName = "ANOX" AndAlso MsgBox("Print Images?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJ.DataDefinition.FormulaFields("SHOWIMAGE").Text = "1"
                End If

            ElseIf FRMSTRING = "EMBPRODUCTION" Then
                OBJ = New EmbProdReport

            ElseIf FRMSTRING = "JOBCUTTING" Then
                OBJ = New JOReportCutting

            ElseIf FRMSTRING = "JOBOUTGARMENT" Then
                OBJ = New JOReport_GARMENT

            ElseIf FRMSTRING = "JOBIN" Then
                If ClientName = "AXIS" Then
                    OBJ = New JIReport_AXIS
                ElseIf ClientName = "SVS" Then
                    OBJ = New JobInReport_SVS
                ElseIf ClientName = "SNCM" And FRMSTRING = "JOBINPS" Then
                    OBJ = New JobInReport_SNCM
                Else
                    OBJ = New JobInReport_COMMON
                End If

            ElseIf FRMSTRING = "PACKINGSLIP" Then
                If ClientName = "MARKIN" Then
                    OBJ = New PackingSlipReport_MARKIN
                Else
                    OBJ = New PackingSlipReport
                End If

            ElseIf FRMSTRING = "GDNGARMENT" Then
                OBJ = New GDNReport_GARMENT
                If WHITELABEL = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                If HIDEPCSDETAILS = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                If ClientName = "ALENCOT" Or ClientName = "MANSI" Or ClientName = "CHINTAN" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            ElseIf FRMSTRING = "GDN" Then
                If ClientName = "SVS" Then
                    OBJ = New GDNReport_SVS
                ElseIf ClientName = "BALAJI" Then
                    OBJ = New GDNReport_BALAJI
                    If WHITELABEL = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    If HIDEPCSDETAILS = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                    OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                ElseIf ClientName = "SOFTAS" Then
                    OBJ = New GDNReport_SOFTAS
                ElseIf ClientName = "SKF" Then
                    OBJ = New GDNReport_SKF
                ElseIf ClientName = "KDFAB" Then
                    OBJ = New GDNReport_KDFAB
                ElseIf ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                    OBJ = New GDNReport_SAFFRON
                ElseIf ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                    OBJ = New GDNReport_SANGHVI
                ElseIf ClientName = "MANINATH" Then
                    OBJ = New GDNReport_MANINATH
                ElseIf ClientName = "CC" Or ClientName = "C3" Then
                    OBJ = New GDNReport_CC
                ElseIf ClientName = "MAFATLAL" Then
                    OBJ = New GDNReport_A5
                ElseIf ClientName = "SUPRIYA" Then
                    OBJ = New GDNReport_SUPRIYA
                ElseIf ClientName = "SUPEEMA" Then
                    OBJ = New GDNReport_SUPEEMA
                ElseIf ClientName = "BRILLANTO" Then
                    OBJ = New GDNReport_BRILLANTO
                ElseIf ClientName = "AKASHDEEP" Then
                    OBJ = New GDNReport_AKASHDEEP
                ElseIf ClientName = "SBA" Then
                    OBJ = New GDNReport_SBA
                ElseIf ClientName = "DRDRAPES" Then
                    OBJ = New GDNReport_DRDRAPES
                    If WHITELABEL = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                ElseIf ClientName = "KCRAYON" Then
                    OBJ = New GDNReport_KCRAYON
                    If HIDEPCSDETAILS = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1
                ElseIf ClientName = "AVIS" Then
                    OBJ = New GDNReport_AVIS
                    If WHITELABEL = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                    If HIDEPCSDETAILS = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1
                ElseIf ClientName = "PARAS" Or ClientName = "MARKIN" Then
                    OBJ = New GDNReport_PARASMARKIN
                    If WHITELABEL = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    If HIDEPCSDETAILS = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                ElseIf ClientName = "MOMAI" Then
                    OBJ = New GDNReport_MOMAI
                ElseIf ClientName = "NTC" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "KUNAL" Or ClientName = "SURYODAYA" Or ClientName = "SSC" Or ClientName = "VALIANT" Then
                    OBJ = New GDNReport_NTC
                    If WHITELABEL = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                    If HIDEPCSDETAILS = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1
                ElseIf ClientName = "MNIKHIL" Or ClientName = "HRITI" Then
                    OBJ = New GDNReport_MNIKHILHRITI
                ElseIf ClientName = "SHREENAKODA" Then
                    OBJ = New GDNReport_SHREENAKODA
                    If WHITELABEL = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                    If HIDEPCSDETAILS = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1
                ElseIf ClientName = "SNCM" Then
                    OBJ = New GDNReport_SNCM
                    If PARTYCHANGEADD <> "" Then OBJ.DataDefinition.FormulaFields("SHIPPINGADD").Text = "'" & PARTYCHANGEADD & "'"
                    If PRINTRATE = True Then OBJ.DataDefinition.FormulaFields("PRINTRATE").Text = 1 Else OBJ.DataDefinition.FormulaFields("PRINTRATE").Text = 0
                ElseIf ClientName = "REALCORPORATION" Then
                    OBJ = New GDNReport_REALCORP
                ElseIf ClientName = "AARYA" Then
                    OBJ = New GDNReport_AARYA
                ElseIf ClientName = "BARKHA" Then
                    OBJ = New GDNReport_A5
                Else
                    OBJ = New GDNReport_COMMON
                    If PRINTINYARDS = True Then
                        OBJ.DataDefinition.FormulaFields("PRINTINYARDS").Text = 1
                        OBJ.Subreports(0).DataDefinition.FormulaFields("PRINTINYARDS").Text = 1
                    Else
                        OBJ.DataDefinition.FormulaFields("PRINTINYARDS").Text = 0
                        OBJ.Subreports(0).DataDefinition.FormulaFields("PRINTINYARDS").Text = 0
                    End If
                    If PARTYCHANGEADD <> "" Then RPTGDN.DataDefinition.FormulaFields("SHIPPINGADD").Text = "'" & PARTYCHANGEADD & "'"
                    If WHITELABEL = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 0
                    If HIDEPCSDETAILS = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                    If ClientName = "ALENCOT" Or ClientName = "MANSI" Or ClientName = "CHINTAN" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                End If


            ElseIf FRMSTRING = "GATEPASS" Then
                If ClientName = "AVIS" Or ClientName = "RADHA" Then
                    OBJ = New GatePassReport_AVIS
                Else
                    OBJ = New GatePassReport
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                End If
            ElseIf FRMSTRING = "GPPACKINGSLIP" Then
                OBJ = New GatePassPackingSlipReport
            ElseIf FRMSTRING = "GPEXPPACKINGSLIP" Then
                OBJ = New GatePassExpPackingSlipReport

            End If


            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = FORMULA

            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions


                Dim TEMPATTACHMENT As String = ""
                If FRMSTRING = "JOBIN" Then
                    TEMPATTACHMENT = "JOBIN"
                    'ElseIf FRMSTRING = "JOBINPS" Then
                    '    TEMPATTACHMENT = "JOBINPS"
                ElseIf FRMSTRING = "JOBOUT" Or FRMSTRING = "JOBCUTTING" Or FRMSTRING = "EMBPRODUCTION" Then
                    TEMPATTACHMENT = "JOBOUT"
                ElseIf FRMSTRING = "GDN" Or FRMSTRING = "GDNGARMENT" Then
                    TEMPATTACHMENT = vendorname & "GDN"
                ElseIf FRMSTRING = "TRANSGDN" Then
                    TEMPATTACHMENT = "TRANSGDN"
                ElseIf FRMSTRING = "PROFORMA" Then
                    TEMPATTACHMENT = "PROFORMA"
                ElseIf FRMSTRING = "GODOWNTRANSFER" Then
                    TEMPATTACHMENT = "GODOWNTRANSFER"
                ElseIf FRMSTRING = "PENDINGDETAILS" Then
                    TEMPATTACHMENT = "PENDINGDETAILS"
                ElseIf FRMSTRING = "DAILYACTIVITY" Then
                    TEMPATTACHMENT = "DAILYACTIVITY"
                ElseIf FRMSTRING = "GATEPASS" Then
                    TEMPATTACHMENT = "GATEPASS"
                ElseIf FRMSTRING = "GPPACKINGSLIP" Or FRMSTRING = "GPEXPPACKINGSLIP" Then
                    TEMPATTACHMENT = "PACKINGSLIP"
                End If


                oDfDopt.DiskFileName = Application.StartupPath & "\" & TEMPATTACHMENT & "_" & JONO & ".pdf"

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
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If Val(TXTCOPIES.Text.Trim) <= 0 Then
                MsgBox("No of Copies cannot be zero", MsgBoxStyle.Critical)
                Exit Sub
            Else
                If FRMSTRING = "GDN" Then RPTGDN.PrintToPrinter(Val(TXTCOPIES.Text.Trim), True, 0, 0)
                If FRMSTRING = "GDNGARMENT" Then RPTGDN_GARMENT.PrintToPrinter(Val(TXTCOPIES.Text.Trim), True, 0, 0)
                If FRMSTRING = "PROFORMA" Then RPTPROFORMA.PrintToPrinter(Val(TXTCOPIES.Text.Trim), True, 0, 0)
                If FRMSTRING = "TRANSGDN" Then RPTTRANSGDN.PrintToPrinter(Val(TXTCOPIES.Text.Trim), True, 0, 0)
                If FRMSTRING = "JOBOUT" Then RPTJO.PrintToPrinter(Val(TXTCOPIES.Text.Trim), True, 0, 0) Else RPTJO_SAFFRON.PrintToPrinter(Val(TXTCOPIES.Text.Trim), True, 0, 0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCOPIES_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCOPIES.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub GDNDESIGN_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            If ClientName = "DAKSH" Or ClientName = "SHALIBHADRA" Then
                PrintToolStripButton.Visible = True
                TXTCOPIES.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()

            Dim TEMPATTACHMENT As String = ""
            If FRMSTRING = "JOBIN" Then
                TEMPATTACHMENT = "JOBIN"
            ElseIf FRMSTRING = "JOBOUT" Or FRMSTRING = "JOBCUTTING" Or FRMSTRING = "JOBOUTGARMENT" Or FRMSTRING = "EMBPRODUCTION" Then
                TEMPATTACHMENT = "JOBOUT"
            ElseIf FRMSTRING = "GDN" Or FRMSTRING = "GDNGARMENT" Then
                TEMPATTACHMENT = "GDN"
            ElseIf FRMSTRING = "TRANSGDN" Then
                TEMPATTACHMENT = "TRANSGDN"
            ElseIf FRMSTRING = "PROFORMA" Then
                TEMPATTACHMENT = "PROFORMA"
            ElseIf FRMSTRING = "GODOWNTRANSFER" Then
                TEMPATTACHMENT = "GODOWNTRANSFER"
            ElseIf FRMSTRING = "PENDINGDETAILS" Then
                TEMPATTACHMENT = "PENDINGDETAILS"
            ElseIf FRMSTRING = "DAILYACTIVITY" Then
                TEMPATTACHMENT = "DAILYACTIVITY"
            ElseIf FRMSTRING = "GATEPASS" Then
                TEMPATTACHMENT = "GATEPASS"
            ElseIf FRMSTRING = "GPPACKINGSLIP" Or FRMSTRING = "GPEXPPACKINGSLIP" Then
                TEMPATTACHMENT = "PACKINGSLIP"
            ElseIf FRMSTRING = "GDNBANNER" Then
                TEMPATTACHMENT = "GDNBANNER"
            End If

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & TEMPATTACHMENT & ".PDF")
            OBJWHATSAPP.FILENAME.Add(TEMPATTACHMENT & ".pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class