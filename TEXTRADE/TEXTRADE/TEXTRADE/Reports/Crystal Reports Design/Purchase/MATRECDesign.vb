Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class MATRECDesign
    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public PERIOD As String
    Public BLANKPAPER As Boolean = False

    Dim RPTPARTYDTLS As New MatRecPartyWiseDetails
    Dim RPTPARTYSUMM As New MatRecPartyWiseSummary
    Dim RPTGODOWNDTLS As New MatRecGodownWiseDetails
    Dim RPTGODOWNSUMM As New MatRecGodownWiseSummary
    Dim RPTITEMDTLS As New MatRecItemWiseDetails
    Dim RPTITEMSUMM As New MatRecItemWiseSummary
    Dim RPTQUALITYDTLS As New MatRecQualityWiseDetails
    Dim RPTQUALITYSUMM As New MatRecQualityWiseSummary
    Dim RPTDESIGNDTLS As New MatRecDesignWiseDetails
    Dim RPTDESIGNSUMM As New MatRecDesignWiseSummary
    Dim RPTSHADEDTLS As New MatRecShadeWiseDetails
    Dim RPTSHADESUMM As New MatRecShadeWiseSummary
    Dim RPTLOTDTLS As New MatRecLOTWiseDetails
    Dim RPTLOTSUMM As New MatRecLOTWiseSummary
    Dim RPTLOTDIFF As New MatrRecDiffReport
    Dim RPTMATRECMONTHWISE As New MatRecMonthWise
    Dim RPTENTRYDESIGNSHADE As New MatRecEntryDesignShadeWiseSumm

    Dim RPTMATREC As New MatRecReport
    Dim RPTMATRECCUTTING As New MatRecReportCutting
    Dim RPTMATREC_BRILLANTO As New MatRecReport_BRILLANTO
    Dim RPTMATRECPS As New MatRecReport_SNCM

    Dim RPTISSUEPACK As New IssueToPackingReport
    Dim RPTISSUEPACK_KARAN As New IssueToPackingReport_KARAN
    Dim RPTISSUEPACK_MAHAVIRPOLYCOT As New IssueToPackingReport_MAHAVIRPOLYCOT

    Dim RPTRECPACK As New RecPackReport
    Dim RPTRECPACKCONTRACTOR As New RecPackContractorReport


    Private Sub MATRECDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
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

            If FRMSTRING = "PARTYWISEDTLS" Then crTables = RPTPARTYDTLS.Database.Tables
            If FRMSTRING = "PARTYWISESUMM" Then crTables = RPTPARTYSUMM.Database.Tables

            If FRMSTRING = "GODOWNWISEDTLS" Then crTables = RPTGODOWNDTLS.Database.Tables
            If FRMSTRING = "GODOWNWISESUMM" Then crTables = RPTGODOWNSUMM.Database.Tables

            If FRMSTRING = "ITEMWISEDTLS" Then crTables = RPTITEMDTLS.Database.Tables
            If FRMSTRING = "ITEMWISESUMM" Then crTables = RPTITEMSUMM.Database.Tables

            If FRMSTRING = "QUALITYWISEDTLS" Then crTables = RPTQUALITYDTLS.Database.Tables
            If FRMSTRING = "QUALITYWISESUMM" Then crTables = RPTQUALITYSUMM.Database.Tables

            If FRMSTRING = "DESIGNWISEDTLS" Then crTables = RPTDESIGNDTLS.Database.Tables
            If FRMSTRING = "DESIGNWISESUMM" Then crTables = RPTDESIGNSUMM.Database.Tables

            If FRMSTRING = "SHADEWISEDTLS" Then crTables = RPTSHADEDTLS.Database.Tables
            If FRMSTRING = "SHADEWISESUMM" Then crTables = RPTSHADESUMM.Database.Tables

            If FRMSTRING = "LOTWISEDTLS" Then crTables = RPTLOTDTLS.Database.Tables
            If FRMSTRING = "LOTWISESUMM" Then crTables = RPTLOTSUMM.Database.Tables
            If FRMSTRING = "LOTWISEDIFF" Then crTables = RPTLOTDIFF.Database.Tables
            If FRMSTRING = "ENTRYDESIGNSHADEWISE" Then crTables = RPTENTRYDESIGNSHADE.Database.Tables

            If FRMSTRING = "MONTHLY" Then crTables = RPTMATRECMONTHWISE.Database.Tables

            If FRMSTRING = "RECPACK" Then crTables = RPTRECPACK.Database.Tables
            If FRMSTRING = "CONTRACTORWISE" Then crTables = RPTRECPACKCONTRACTOR.Database.Tables



            If FRMSTRING = "MATREC" Then
                If ClientName = "BRILLANTO" Then
                    crTables = RPTMATREC_BRILLANTO.Database.Tables
                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "PARAS" Or ClientName = "DJIMPEX" Or ClientName = "SOFTAS" Then
                    crTables = RPTMATRECCUTTING.Database.Tables
                Else
                    crTables = RPTMATREC.Database.Tables
                End If
            End If

            If FRMSTRING = "MATRECPS" Then crTables = RPTMATRECPS.Database.Tables


            If FRMSTRING = "ISSUETOPACK" Then
                If ClientName = "KARAN" Then
                    crTables = RPTISSUEPACK_KARAN.Database.Tables
                ElseIf ClientName = "MAHAVIRPOLYCOT" Then
                    crTables = RPTISSUEPACK_MAHAVIRPOLYCOT.Database.Tables
                Else
                    crTables = RPTISSUEPACK.Database.Tables
                End If
            End If



            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            crpo.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "PARTYWISEDTLS" Then
                crpo.ReportSource = RPTPARTYDTLS
                RPTPARTYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                crpo.ReportSource = RPTPARTYSUMM
                RPTPARTYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                crpo.ReportSource = RPTGODOWNDTLS
                RPTGODOWNDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                crpo.ReportSource = RPTGODOWNSUMM
                RPTGODOWNSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
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
            ElseIf FRMSTRING = "LOTWISEDTLS" Then
                crpo.ReportSource = RPTLOTDTLS
                RPTLOTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "LOTWISESUMM" Then
                crpo.ReportSource = RPTLOTSUMM
                RPTLOTSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "LOTWISEDIFF" Then
                crpo.ReportSource = RPTLOTDIFF
                RPTLOTDIFF.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "MONTHLY" Then
                crpo.ReportSource = RPTMATRECMONTHWISE
                RPTMATRECMONTHWISE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "ENTRYDESIGNSHADEWISE" Then
                crpo.ReportSource = RPTENTRYDESIGNSHADE
                RPTENTRYDESIGNSHADE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"

            ElseIf FRMSTRING = "MATREC" Then
                If ClientName = "BRILLANTO" Then
                    crpo.ReportSource = RPTMATREC_BRILLANTO
                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "PARAS" Or ClientName = "DJIMPEX" Or ClientName = "SOFTAS" Then
                    crpo.ReportSource = RPTMATRECCUTTING
                Else
                    crpo.ReportSource = RPTMATREC
                End If

            ElseIf FRMSTRING = "MATRECPS" Then
                crpo.ReportSource = RPTMATRECPS
                If BLANKPAPER = True Then RPTMATRECPS.DataDefinition.FormulaFields("WHITELABEL").Text = 1 Else RPTMATRECPS.DataDefinition.FormulaFields("WHITELABEL").Text = 0

            ElseIf FRMSTRING = "ISSUETOPACK" Then
                If ClientName = "KARAN" Then
                    crpo.ReportSource = RPTISSUEPACK_KARAN
                ElseIf ClientName = "MAHAVIRPOLYCOT" Then
                    crpo.ReportSource = RPTISSUEPACK_MAHAVIRPOLYCOT
                Else
                    crpo.ReportSource = RPTISSUEPACK
                End If

            ElseIf FRMSTRING = "RECPACK" Then
                crpo.ReportSource = RPTRECPACK
            ElseIf FRMSTRING = "CONTRACTORWISE" Then
                crpo.ReportSource = RPTRECPACKCONTRACTOR
                RPTRECPACKCONTRACTOR.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTRECPACKCONTRACTOR.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            End If

            crpo.Zoom(100)
            crpo.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub MATRECDesign_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Try
            Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()
            Dim TEMPATTACHMENT As String = "MATREC"
            If FRMSTRING = "MATRECPS" Then TEMPATTACHMENT = "PACKINGSLIP"

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

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\MATREC.pdf"

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
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                expo = RPTGODOWNDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGODOWNDTLS.Export()
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                expo = RPTGODOWNSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGODOWNSUMM.Export()
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
            ElseIf FRMSTRING = "LOTWISEDTLS" Then
                expo = RPTLOTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLOTDTLS.Export()
            ElseIf FRMSTRING = "LOTWISESUMM" Then
                expo = RPTLOTSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLOTSUMM.Export()
            ElseIf FRMSTRING = "LOTWISEDIFF" Then
                expo = RPTLOTDIFF.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLOTDIFF.Export()
            ElseIf FRMSTRING = "MONTHLY" Then
                expo = RPTMATRECMONTHWISE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMATRECMONTHWISE.Export()
            ElseIf FRMSTRING = "ENTRYDESIGNSHADEWISE" Then
                expo = RPTENTRYDESIGNSHADE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTENTRYDESIGNSHADE.Export()

            ElseIf FRMSTRING = "MATREC" Then
                If ClientName = "BRILLANTO" Then
                    expo = RPTMATREC_BRILLANTO.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTMATREC_BRILLANTO.Export()
                ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "PARAS" Or ClientName = "DJIMPEX" Or ClientName = "SOFTAS" Then
                    expo = RPTMATRECCUTTING.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTMATRECCUTTING.Export()
                Else
                    expo = RPTMATREC.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTMATREC.Export()
                End If

            ElseIf FRMSTRING = "MATRECPS" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PACKINGSLIP.pdf"
                expo = RPTMATRECPS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMATRECPS.Export()

            ElseIf FRMSTRING = "ISSUETOPACK" Then
                If ClientName = "KARAN" Then
                    expo = RPTISSUEPACK_KARAN.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTISSUEPACK_KARAN.Export()
                ElseIf ClientName = "MAHAVIRPOLYCOT" Then
                    expo = RPTISSUEPACK_MAHAVIRPOLYCOT.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTISSUEPACK_MAHAVIRPOLYCOT.Export()
                Else
                    expo = RPTISSUEPACK.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTISSUEPACK.Export()
                End If
            ElseIf FRMSTRING = "CONTRACTORWISE" Then
                expo = RPTRECPACKCONTRACTOR.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTRECPACKCONTRACTOR.Export()
            ElseIf FRMSTRING = "RECPACK" Then
                expo = RPTRECPACK.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTRECPACK.Export()
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
            If FRMSTRING = "MATRECPS" Then
                OBJWHATSAPP.PATH.Add(Application.StartupPath & "\PACKINGSLIP.PDF")
                OBJWHATSAPP.FILENAME.Add("PACKINGSLIP.pdf")
            Else
                OBJWHATSAPP.PATH.Add(Application.StartupPath & "\MATREC.PDF")
                OBJWHATSAPP.FILENAME.Add("MATREC.pdf")
            End If
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class