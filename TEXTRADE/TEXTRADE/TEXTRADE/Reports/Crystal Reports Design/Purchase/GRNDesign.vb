
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class GRNDesign

    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public PERIOD As String
    Public PENDINGCOMPLETED As String
    Public GRNNO As Integer
    Public HIDEDETAILS As Boolean   'FOR LOTREGISTER
    Public PRINTTRANS As Boolean = False   ' TO PRINT TRANSPORT NAME 

    Dim RPTGRN As New GRNReport
    Dim RPTBARCODE As New GRNReport_CC
    Dim RPTGRNCUTTING As New GRNReportCutting
    Dim RPTWHOLESALEBARCODE As New GRNReport_CC_Wholesale
    Dim RPTGRNSVS As New GRNSVS
    Dim RPTGRN_AXIS As New GRN_AXIS
    Dim RPTGRNDYEING As New GRNDYEING_BRILLANTO
    Dim RPTGRNFINISH As New GRNFinishReport

    Dim RPTLETTER As New GREYISSUELetter

    Dim RPTPARTYDTLS As New GRNPartyWiseDetails
    Dim RPTPARTYSUMM As New GRNPartyWiseSummary
    Dim RPTJOBBERDTLS As New GRNJobberWiseDetails
    Dim RPTJOBBERSUMM As New GRNJobberWiseSummary
    Dim RPTITEMDTLS As New GRNItemWiseDetails
    Dim RPTITEMSUMM As New GRNItemWiseSummary
    Dim RPTQUALITYDTLS As New GRNQualityWiseDetails
    Dim RPTQUALITYSUMM As New GRNQualityWiseSummary
    Dim RPTDESIGNDTLS As New GRNDesignWiseDetails
    Dim RPTDESIGNSUMM As New GRNDesignWiseSummary
    Dim RPTSHADEDTLS As New GRNShadeWiseDetails
    Dim RPTSHADESUMM As New GRNShadeWiseSummary
    Dim RPTMONTHLY As New GRNMonthWise
    Dim RPTPENDING As New GRNPendingCheck
    Dim RPTPARTYITEMDTLS As New GRNPartyItemWiseDetails


    Dim RPTLOTREPORTSUMMARY As New LotStatus_Summary
    Dim RPTLOTREGISTER As New LotStatus_Register
    Dim RPTLOTREPORT As New LotStatus_Details
    Dim RPTLOTPCSDETAILS As New LotStatus_PcsDetails
    Dim RPTLOTVALUE As New LotStatus_SummValue
    Dim RPTLOTTAGGING As New LotTaggingReport

    'Dim RPTLOTDETAIL As New LotStatus_LEDGERS

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



            If FRMSTRING = "BARCODE" Then crTables = RPTBARCODE.Database.Tables
            If FRMSTRING = "WHOLESALEBARCODE" Then crTables = RPTWHOLESALEBARCODE.Database.Tables

            If FRMSTRING = "GRN" Then
                If ClientName = "SVS" Then
                    crTables = RPTGRNSVS.Database.Tables
                ElseIf ClientName = "BRILLANTO" Then
                    crTables = RPTGRNDYEING.Database.Tables
                ElseIf ClientName = "AXIS" Then
                    crTables = RPTGRN_AXIS.Database.Tables
                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                    crTables = RPTGRNCUTTING.Database.Tables
                Else
                    crTables = RPTGRN.Database.Tables
                End If
            End If

            If FRMSTRING = "FINISHGRN" Then crTables = RPTGRNFINISH.Database.Tables

            If FRMSTRING = "PARTYWISEDTLS" Then crTables = RPTPARTYDTLS.Database.Tables
            If FRMSTRING = "PARTYWISESUMM" Then crTables = RPTPARTYSUMM.Database.Tables

            If FRMSTRING = "JOBBERWISEDTLS" Then crTables = RPTJOBBERDTLS.Database.Tables
            If FRMSTRING = "JOBBERWISESUMM" Then crTables = RPTJOBBERSUMM.Database.Tables

            If FRMSTRING = "ITEMWISEDTLS" Then crTables = RPTITEMDTLS.Database.Tables
            If FRMSTRING = "ITEMWISESUMM" Then crTables = RPTITEMSUMM.Database.Tables

            If FRMSTRING = "QUALITYWISEDTLS" Then crTables = RPTQUALITYDTLS.Database.Tables
            If FRMSTRING = "QUALITYWISESUMM" Then crTables = RPTQUALITYSUMM.Database.Tables

            If FRMSTRING = "DESIGNWISEDTLS" Then crTables = RPTDESIGNDTLS.Database.Tables
            If FRMSTRING = "DESIGNWISESUMM" Then crTables = RPTDESIGNSUMM.Database.Tables

            If FRMSTRING = "SHADEWISEDTLS" Then crTables = RPTSHADEDTLS.Database.Tables
            If FRMSTRING = "SHADEWISESUMM" Then crTables = RPTSHADESUMM.Database.Tables

            If FRMSTRING = "MONTHLY" Then crTables = RPTMONTHLY.Database.Tables
            If FRMSTRING = "PENDING" Then crTables = RPTPENDING.Database.Tables
            If FRMSTRING = "PARTYITEMWISE" Then crTables = RPTPARTYITEMDTLS.Database.Tables

            If FRMSTRING = "LETTER" Then crTables = RPTLETTER.Database.Tables

            If FRMSTRING = "LOTSUMM" Then crTables = RPTLOTREPORTSUMMARY.Database.Tables
            If FRMSTRING = "LOTREGISTER" Then crTables = RPTLOTREGISTER.Database.Tables
            If FRMSTRING = "LOTDETAILS" Then crTables = RPTLOTREPORT.Database.Tables
            If FRMSTRING = "LOTPCSDETAILS" Then crTables = RPTLOTPCSDETAILS.Database.Tables
            If FRMSTRING = "LOTVALUE" Then crTables = RPTLOTVALUE.Database.Tables
            If FRMSTRING = "LOTTAGGING" Then crTables = RPTLOTTAGGING.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            crpo.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "PARTYWISEDTLS" Then
                crpo.ReportSource = RPTPARTYDTLS
                RPTPARTYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "BARCODE" Then
                crpo.ReportSource = RPTBARCODE
            ElseIf FRMSTRING = "GRN" Then
                If ClientName = "SVS" Then
                    crpo.ReportSource = RPTGRNSVS
                ElseIf ClientName = "BRILLANTO" Then
                    crpo.ReportSource = RPTGRNDYEING
                ElseIf ClientName = "AXIS" Then
                    crpo.ReportSource = RPTGRN_AXIS
                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                    crpo.ReportSource = RPTGRNCUTTING
                Else
                    crpo.ReportSource = RPTGRN
                End If
            ElseIf FRMSTRING = "FINISHGRN" Then
                crpo.ReportSource = RPTGRNFINISH
            ElseIf FRMSTRING = "WHOLESALEBARCODE" Then
                crpo.ReportSource = RPTWHOLESALEBARCODE
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
            ElseIf FRMSTRING = "MONTHLY" Then
                crpo.ReportSource = RPTMONTHLY
                RPTMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "PENDING" Then
                crpo.ReportSource = RPTPENDING
                RPTPENDING.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "PARTYITEMWISE" Then
                crpo.ReportSource = RPTPARTYITEMDTLS
                RPTPARTYITEMDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "LOTSUMM" Then
                crpo.ReportSource = RPTLOTREPORTSUMMARY
                RPTLOTREPORTSUMMARY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "LOTREGISTER" Then
                crpo.ReportSource = RPTLOTREGISTER
                If HIDEDETAILS = True Then RPTLOTREGISTER.DataDefinition.FormulaFields("HIDEDETAILS").Text = 1
                RPTLOTREGISTER.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "LOTDETAILS" Then
                crpo.ReportSource = RPTLOTREPORT
                RPTLOTREPORT.DataDefinition.FormulaFields("PENDING").Text = "'" & PENDINGCOMPLETED & "'"
                RPTLOTREPORT.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "LOTPCSDETAILS" Then
                crpo.ReportSource = RPTLOTPCSDETAILS
                RPTLOTPCSDETAILS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "LOTVALUE" Then
                crpo.ReportSource = RPTLOTVALUE
                RPTLOTVALUE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "LOTTAGGING" Then
                crpo.ReportSource = RPTLOTTAGGING
                RPTLOTTAGGING.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "LETTER" Then
                If PRINTTRANS = True Then RPTLETTER.DataDefinition.FormulaFields("SHOWPURNAME").Text = 1 Else RPTLETTER.DataDefinition.FormulaFields("SHOWPURNAME").Text = 0
                crpo.ReportSource = RPTLETTER
            End If

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
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()
            Dim TEMPATTACHMENT As String = "GRN"
            Dim objmail As New SendMail
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

            If FRMSTRING = "GRN" Then
                If ClientName = "SVS" Then
                    OBJ = New GRNSVS
                ElseIf ClientName = "BRILLANTO" Then
                    OBJ = New GRNDYEING_BRILLANTO
                ElseIf ClientName = "AXIS" Then
                    OBJ = New GRN_AXIS
                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                    OBJ = New GRNReportCutting
                Else
                    OBJ = New GRNReport
                End If
            ElseIf FRMSTRING = "FINISHGRN" Then
                OBJ = New GRNFinishReport
            End If


            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = WHERECLAUSE

            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions
                oDfDopt.DiskFileName = Application.StartupPath & "\GRN_" & GRNNO & ".pdf"

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

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\GRN.pdf"

            If FRMSTRING = "PARTYWISEDTLS" Then
                expo = RPTPARTYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPARTYDTLS.Export()

            ElseIf FRMSTRING = "PARTYITEMWISE" Then
                expo = RPTPARTYITEMDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPARTYITEMDTLS.Export()

            ElseIf FRMSTRING = "BARCODE" Then
                expo = RPTBARCODE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBARCODE.Export()

            ElseIf FRMSTRING = "LETTER" Then
                expo = RPTLETTER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLETTER.Export()

            ElseIf FRMSTRING = "GRN" Then
                If ClientName = "SVS" Then
                    expo = RPTGRNSVS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGRNSVS.Export()
                ElseIf ClientName = "BRILLANTO" Then
                    expo = RPTGRNDYEING.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGRNDYEING.Export()
                ElseIf ClientName = "AXIS" Then
                    expo = RPTGRN_AXIS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGRN_AXIS.Export()
                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                    expo = RPTGRNCUTTING.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGRNCUTTING.Export()
                Else
                    expo = RPTGRN.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTGRN.Export()
                End If

            ElseIf FRMSTRING = "FINISHGRN" Then
                expo = RPTGRNFINISH.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGRNFINISH.Export()

            ElseIf FRMSTRING = "WHOLESALEBARCODE" Then
                expo = RPTWHOLESALEBARCODE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWHOLESALEBARCODE.Export()

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
            ElseIf FRMSTRING = "PENDING" Then
                expo = RPTPENDING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPENDING.Export()

            ElseIf FRMSTRING = "LOTSUMM" Then
                expo = RPTLOTREPORTSUMMARY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLOTREPORTSUMMARY.Export()
            ElseIf FRMSTRING = "LOTDETAILS" Then
                expo = RPTLOTREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLOTREPORT.Export()
            ElseIf FRMSTRING = "LOTPCSDETAILS" Then
                expo = RPTLOTPCSDETAILS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLOTPCSDETAILS.Export()
            ElseIf FRMSTRING = "LOTVALUE" Then
                expo = RPTLOTVALUE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLOTVALUE.Export()
            ElseIf FRMSTRING = "LOTTAGGING" Then
                expo = RPTLOTTAGGING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLOTTAGGING.Export()

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
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\GRN.PDF")
            OBJWHATSAPP.FILENAME.Add("GRN.pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class