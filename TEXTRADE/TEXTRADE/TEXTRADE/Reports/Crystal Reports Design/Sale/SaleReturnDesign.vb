
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class SaleReturnDesign

    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public PERIOD As String

    Public SALRETNO As Integer = 0
    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public NOOFCOPIES As Integer = 1
    Public PRINTSETTING As Object = Nothing
    Public DIRECTWHATSAPP As Boolean = False
    Public IGSTFORMAT As Boolean = False


    Dim tempattachment As String
    Dim RPTPARTYDTLS As New SaleRetPartyWiseDetails
    Dim RPTPARTYSUMM As New SaleRetPartyWiseSummary
    Dim RPTAGENTDTLS As New SaleRetAgentWiseDetails
    Dim RPTAGENTSUMM As New SaleRetAgentWiseSummary
    Dim RPTGODOWNDTLS As New SaleRetGodownWiseDetails
    Dim RPTGODOWNSUMM As New SaleRetGodownWiseSummary
    Dim RPTITEMDTLS As New SaleRetItemWiseDetails
    Dim RPTITEMSUMM As New SaleRetItemWiseSummary
    Dim RPTQUALITYDTLS As New SaleRetQualityWiseDetails
    Dim RPTQUALITYSUMM As New SaleRetQualityWiseSummary
    Dim RPTDESIGNDTLS As New SaleRetDesignWiseDetails
    Dim RPTDESIGNSUMM As New SaleRetDesignWiseSummary
    Dim RPTSHADEDTLS As New SaleRetShadeWiseDetails
    Dim RPTSHADESUMM As New SaleRetShadeWiseSummary
    Dim RPTTRANSDTLS As New SaleRetTransWiseDetails
    Dim RPTTRANSSUMM As New SaleRetTransWiseSummary
    Dim RPTMONTHLY As New SaleRetMonthWise
    Dim RPTSALRETREGISTERINDETAIL As New SaleReturnInDetailsReport


    Dim RPTCNREGISTERINDETAIL As New CreditNoteRegisterInDetailsReport


    Dim RPTSALERET_SVS As New SaleReturnReport_SVS
    Dim RPTSALERET_POONAM As New SaleReturnReport_POONAM
    Dim RPTSALERET As New SaleReturnReport
    Dim RPTSALERETCHALLAN As New SaleReturnChallanReport
    Dim RPTSALERET_BRILLANTO As New SaleReturnReport_BRILLANTO

    Private Sub SaleReturnDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Cursor.Current = Cursors.WaitCursor


            If DIRECTPRINT = True Then
                PRINTDIRECTLYTOPRINTER()
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

            If FRMSTRING = "PARTYWISEDTLS" Then crTables = RPTPARTYDTLS.Database.Tables
            If FRMSTRING = "PARTYWISESUMM" Then crTables = RPTPARTYSUMM.Database.Tables

            If FRMSTRING = "AGENTWISEDTLS" Then crTables = RPTAGENTDTLS.Database.Tables
            If FRMSTRING = "AGENTWISESUMM" Then crTables = RPTAGENTSUMM.Database.Tables

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

            If FRMSTRING = "TRANSWISEDTLS" Then crTables = RPTTRANSDTLS.Database.Tables
            If FRMSTRING = "TRANSWISESUMM" Then crTables = RPTTRANSSUMM.Database.Tables
            If FRMSTRING = "SALERETURNCHALLAN" Then crTables = RPTSALERETCHALLAN.Database.Tables
            If FRMSTRING = "MONTHLY" Then crTables = RPTMONTHLY.Database.Tables
            If FRMSTRING = "SALRETREGISTERINDETAIL" Then crTables = RPTSALRETREGISTERINDETAIL.Database.Tables
            If FRMSTRING = "CNREGISTERINDETAIL" Then crTables = RPTCNREGISTERINDETAIL.Database.Tables


            If FRMSTRING = "SALERETURN" Then
                If ClientName = "SVS" Then
                    crTables = RPTSALERET_SVS.Database.Tables
                ElseIf ClientName = "POONAM" Then
                    crTables = RPTSALERET_POONAM.Database.Tables
                ElseIf ClientName = "BRILLANTO" Then
                    crTables = RPTSALERET_BRILLANTO.Database.Tables
                Else
                    crTables = RPTSALERET.Database.Tables

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
            ElseIf FRMSTRING = "AGENTWISEDTLS" Then
                crpo.ReportSource = RPTAGENTDTLS
                RPTAGENTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "AGENTWISESUMM" Then
                crpo.ReportSource = RPTAGENTSUMM
                RPTAGENTSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
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
            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                crpo.ReportSource = RPTTRANSDTLS
                RPTTRANSDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                crpo.ReportSource = RPTTRANSSUMM
                RPTTRANSSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "MONTHLY" Then
                crpo.ReportSource = RPTMONTHLY
            ElseIf FRMSTRING = "SALERETURNCHALLAN" Then
                crpo.ReportSource = RPTSALERETCHALLAN
            ElseIf FRMSTRING = "SALRETREGISTERINDETAIL" Then
                crpo.ReportSource = RPTSALRETREGISTERINDETAIL
                RPTSALRETREGISTERINDETAIL.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "CNREGISTERINDETAIL" Then
                crpo.ReportSource = RPTCNREGISTERINDETAIL
                RPTCNREGISTERINDETAIL.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SALERETURN" Then
                If ClientName = "SVS" Then
                    crpo.ReportSource = RPTSALERET_SVS
                ElseIf ClientName = "POONAM" Then
                    crpo.ReportSource = RPTSALERET_POONAM
                ElseIf ClientName = "BRILLANTO" Then
                    crpo.ReportSource = RPTSALERET_BRILLANTO
                Else
                    crpo.ReportSource = RPTSALERET
                    If ClientName = "VINAYAK" Or ClientName = "SKF" Or ClientName = "CHINTAN" Then RPTSALERET.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                End If
            End If
            crpo.Zoom(100)
            crpo.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Sub SaleReturnDesign_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            Dim TEMPATTACHMENT As String = "SaleRet"
            Dim objmail As New SendMail
            objmail.attachment = TEMPATTACHMENT
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
            oDfDopt.DiskFileName = Application.StartupPath & "\SaleRet.pdf"

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
            ElseIf FRMSTRING = "AGENTWISEDTLS" Then
                expo = RPTAGENTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTAGENTDTLS.Export()
            ElseIf FRMSTRING = "AGENTWISESUMM" Then
                expo = RPTAGENTSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTAGENTSUMM.Export()
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
            ElseIf FRMSTRING = "MONTHLY" Then
                expo = RPTMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMONTHLY.Export()
            ElseIf FRMSTRING = "SALRETREGISTERINDETAIL" Then
                expo = RPTSALRETREGISTERINDETAIL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALRETREGISTERINDETAIL.Export()
            ElseIf FRMSTRING = "CNREGISTERINDETAIL" Then
                expo = RPTCNREGISTERINDETAIL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCNREGISTERINDETAIL.Export()
            ElseIf FRMSTRING = "SALERETURNCHALLAN" Then
                RPTSALERETCHALLAN.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                expo = RPTSALERETCHALLAN.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETCHALLAN.Export()
                RPTSALERETCHALLAN.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
            ElseIf FRMSTRING = "SALERETURN" Then
                If ClientName = "SVS" Then
                    expo = RPTSALERET_SVS.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSALERET_SVS.Export()
                ElseIf ClientName = "POONAM" Then
                    expo = RPTSALERET_POONAM.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSALERET_POONAM.Export()
                ElseIf ClientName = "BRILLANTO" Then
                    expo = RPTSALERET_BRILLANTO.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSALERET_BRILLANTO.Export()
                Else
                    RPTSALERET.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    expo = RPTSALERET.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSALERET.Export()
                    RPTSALERET.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString)
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
            If FRMSTRING = "SALERETURN" Then
                strsearch = "{SALERETURN.SALRET_NO}=" & Val(SALRETNO) & " and {SALERETURN.SALRET_yearid}=" & YearId
                If ClientName = "SVS" Then
                    OBJ = New SaleReturnReport_SVS
                ElseIf ClientName = "POONAM" Then
                    OBJ = New SaleReturnReport_POONAM
                ElseIf ClientName = "BRILLANTO" Then
                    OBJ = New SaleReturnReport_BRILLANTO
                Else
                    OBJ = New SaleReturnReport
                    If ClientName = "VINAYAK" Or ClientName = "SKF" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                End If
            ElseIf FRMSTRING = "SALERETURNCHALLAN" Then
                strsearch = "{SALERETURNCHALLAN.SRCH_NO}=" & Val(SALRETNO) & " and {SALERETURNCHALLAN.SRCH_yearid}=" & YearId
                OBJ = New SaleReturnChallanReport
            End If
            crpo.SelectionFormula = strsearch


SKIPINVOICE:
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
                oDfDopt.DiskFileName = Application.StartupPath & "\SALRET_" & SALRETNO & ".pdf"

                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)

                expo = OBJ.ExportOptions
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\SaleRet.PDF")
            OBJWHATSAPP.FILENAME.Add("SaleRet.pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try

        'Try
        '    If ALLOWWHATSAPP = False Then Exit Sub
        '    ' If ALLOWDIGITALSIGN = True AndAlso MsgBox("Send Pdf with Digital Signature?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then PDFWITHDIGITALSIGN = True
        '    Transfer()

        '    'If FRMSTRING = "" Then
        '    '    tempattachment = "SALEDETAILS"
        '    If FRMSTRING = "SALERETURN" Then
        '        tempattachment = "SALERETURN"
        '        'ElseIf FRMSTRING = "PROFORMAINVOICE" Then
        '        '    tempattachment = "PROFORMA"
        '        'Else
        '        '    tempattachment = "SALESUMMARY"
        '    End If

        '    Dim OBJWHATSAPP As New SendWhatsapp
        '    'OBJWHATSAPP.PARTYNAME = PARTYNAME
        '    'OBJWHATSAPP.AGENTNAME = AGENTNAME
        '    ' If PDFWITHDIGITALSIGN = True Then
        '    'OBJWHATSAPP.PATH.Add(Application.StartupPath & "\INVOICEPDF\INVOICE_" & Val(INVNO) & "-" & Val(AccFrom.Year) & ".pdf")
        '    '    OBJWHATSAPP.FILENAME.Add("INVOICE_" & Val(INVNO) & "-" & Val(AccFrom.Year) & ".pdf")
        '    'Else
        '    OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & tempattachment & ".PDF")
        '        OBJWHATSAPP.FILENAME.Add(tempattachment & ".pdf")
        '    'End If
        '    OBJWHATSAPP.ShowDialog()
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub
End Class