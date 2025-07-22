
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class PLDesign

    Dim RPTPL As New ProfitLossReport
    Dim RPTBS As New BalanceSheetReport

    Dim RPTINTSUMM As New InterestSummReport
    Dim RPTINTDTLS As New InterestDetailsReport
    Dim RPTINTBILLDTLS As New InterestBillWiseReport
    Dim RPTOUTSTANDING As New GridOutstandingPrintReport
    Dim RPTSALEANALYSIS As New GridSaleAnalysisPrintReport
    Dim RPTPURANALYSIS As New GridPurAnalysisPrintReport

    Public frmstring As String
    Public SHOWNARR As Integer = 0
    Public FROMDATE As Date
    Public TODATE As Date
    Public DUEDATE As Date
    Public CHANGEDUEDATE As Boolean
    Public strsearch As String
    Public PERIOD As String
    Public PARTYNAME As String
    Public SIDEDAYS As Integer
    Public CALCDAYS As Integer
    Public INTPER As Double
    Public TDSPER As Double
    Public DIRECTPRINT As Boolean = False
    Public OVERRIDEDUEDATE As Integer = 1

    Private Sub PLDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PLdesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If DIRECTPRINT = True Then
                PRINTDIRECTADVICE()
                Exit Sub
            End If
            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table


            With crConnecttionInfo
                .ServerName = Servername
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            If frmstring = "BALANCESHEET" Then
                crTables = RPTBS.Database.Tables
            ElseIf frmstring = "INTERESTSUMM" Then
                crTables = RPTINTSUMM.Database.Tables
            ElseIf frmstring = "INTERESTDTLS" Then
                crTables = RPTINTDTLS.Database.Tables
            ElseIf frmstring = "INTERESTBILLDTLS" Then
                crTables = RPTINTBILLDTLS.Database.Tables
            ElseIf frmstring = "OUTSTANDING" Then
                crTables = RPTOUTSTANDING.Database.Tables
            ElseIf frmstring = "SALEANALYSIS" Then
                crTables = RPTSALEANALYSIS.Database.Tables
            ElseIf frmstring = "PURANALYSIS" Then
                crTables = RPTPURANALYSIS.Database.Tables
            Else
                crTables = RPTPL.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next
            '************************ END *******************

            CRPO.SelectionFormula = strsearch
            If frmstring = "BALANCESHEET" Then
                CRPO.ReportSource = RPTBS
                RPTBS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf frmstring = "INTERESTSUMM" Then
                CRPO.ReportSource = RPTINTSUMM
            ElseIf frmstring = "INTERESTDTLS" Then
                CRPO.ReportSource = RPTINTDTLS
                RPTINTDTLS.DataDefinition.FormulaFields("SHOWNARR").Text = SHOWNARR
                RPTINTDTLS.Subreports("INTERESTDTLS_TERMSUMM").DataDefinition.FormulaFields("INTPER").Text = INTPER
                RPTINTDTLS.Subreports("INTERESTDTLS_TERMSUMM").DataDefinition.FormulaFields("TEMPDAYS").Text = CALCDAYS
            ElseIf frmstring = "INTERESTBILLDTLS" Then
                CRPO.ReportSource = RPTINTBILLDTLS
                RPTINTBILLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTINTBILLDTLS.DataDefinition.FormulaFields("SIDEDAYS").Text = SIDEDAYS
                RPTINTBILLDTLS.DataDefinition.FormulaFields("INTPER").Text = INTPER
                RPTINTBILLDTLS.DataDefinition.FormulaFields("TDSPER").Text = TDSPER
                RPTINTBILLDTLS.DataDefinition.FormulaFields("CALCDAYS").Text = CALCDAYS
                RPTINTBILLDTLS.DataDefinition.FormulaFields("OVERRIDEDUEDATE").Text = OVERRIDEDUEDATE
                RPTINTBILLDTLS.DataDefinition.FormulaFields("TILLDATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                If CHANGEDUEDATE = True Then RPTINTBILLDTLS.DataDefinition.FormulaFields("DUEDATE").Text = "#" & Format(Convert.ToDateTime(DUEDATE).Date, "MM/dd/yyyy") & "#"
            ElseIf frmstring = "OUTSTANDING" Then
                CRPO.ReportSource = RPTOUTSTANDING
            ElseIf frmstring = "SALEANALYSIS" Then
                CRPO.ReportSource = RPTSALEANALYSIS
            ElseIf frmstring = "PURANALYSIS" Then
                CRPO.ReportSource = RPTPURANALYSIS
            Else
                CRPO.ReportSource = RPTPL
                RPTPL.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
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

            If frmstring = "OUTSTANDING" Then
                OBJ = New GridOutstandingPrintReport
                crTables = RPTOUTSTANDING.Database.Tables
            ElseIf frmstring = "SALEANALYSIS" Then
                OBJ = New GridSaleAnalysisPrintReport
                crTables = RPTSALEANALYSIS.Database.Tables
            ElseIf frmstring = "PURANALYSIS" Then
                OBJ = New GridPurAnalysisPrintReport
                crTables = RPTSALEANALYSIS.Database.Tables
            End If


            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = strsearch

            'If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
            '    OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
            '    OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            'Else

            If frmstring = "OUTSTANDING" Then
                CRPO.ReportSource = RPTOUTSTANDING
            ElseIf frmstring = "SALEANALYSIS" Then
                CRPO.ReportSource = RPTSALEANALYSIS
            ElseIf frmstring = "PURANALYSIS" Then
                CRPO.ReportSource = RPTPURANALYSIS
            End If

            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions


            Dim TEMPATTACHMENT As String = ""
            If frmstring = "OUTSTANDING" Then
                TEMPATTACHMENT = "Outstanding_" & PARTYNAME
            ElseIf frmstring = "SALEANALYSIS" Then
                TEMPATTACHMENT = "SaleAnalysis_" & PARTYNAME
            ElseIf frmstring = "PURANALYSIS" Then
                TEMPATTACHMENT = "PurAnalysis_" & PARTYNAME
            End If
            CRPO.Zoom(100)
            CRPO.Refresh()

            oDfDopt.DiskFileName = Application.StartupPath & "\" & TEMPATTACHMENT & ".pdf"

            'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
            If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
            'OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
            expo = OBJ.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
            'OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()
        Dim tempattachment As String

        If frmstring = "BALANCESHEET" Then
            tempattachment = "BALANCESHEET"
        ElseIf frmstring = "INTERESTSUMM" Then
            tempattachment = "INTERESTSUMM"
        ElseIf frmstring = "INTERESTDTLS" Then
            tempattachment = "INTERESTDTLS"
        ElseIf frmstring = "INTERESTBILLDTLS" Then
            tempattachment = "INTERESTBILLDTLS"
        ElseIf frmstring = "OUTSTANDING" Then
            tempattachment = "OUTSTANDING"
        ElseIf frmstring = "SALEANALYSIS" Then
            tempattachment = "SALEANALYSIS"
        ElseIf frmstring = "PURANALYSIS" Then
            tempattachment = "PURANALYSIS"
        Else
            tempattachment = "PROFITLOSS"
        End If

        Try
            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\" & tempattachment & ".PDF"
            If emailid <> "" Then
                objmail.cmbfirstadd.Text = emailid
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

            Dim oDfDopt As New DiskFileDestinationOptions
            Dim expo As ExportOptions
            If frmstring = "BALANCESHEET" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\BALANCESHEET.PDF"
                expo = RPTBS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBS.Export()
            ElseIf frmstring = "INTERESTSUMM" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\INTERESTSUMM.PDF"
                expo = RPTINTSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINTSUMM.Export()
            ElseIf frmstring = "INTERESTDTLS" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\INTERESTDTLS.PDF"
                expo = RPTINTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINTDTLS.Export()
            ElseIf frmstring = "INTERESTBILLDTLS" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\INTERESTBILLDTLS.PDF"
                expo = RPTINTBILLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINTBILLDTLS.Export()
            ElseIf frmstring = "OUTSTANDING" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\OUTSTANDING.PDF"
                expo = RPTOUTSTANDING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDING.Export()
            ElseIf frmstring = "SALEANALYSIS" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SALEANALYSIS.PDF"
                expo = RPTSALEANALYSIS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALEANALYSIS.Export()
            ElseIf frmstring = "PURANALYSIS" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PURANALYSIS.PDF"
                expo = RPTPURANALYSIS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPURANALYSIS.Export()
            Else
                oDfDopt.DiskFileName = Application.StartupPath & "\PROFITLOSS.PDF"
                expo = RPTPL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPL.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()
            Dim tempattachment As String

            If frmstring = "BALANCESHEET" Then
                tempattachment = "BALANCESHEET"
            ElseIf frmstring = "INTERESTSUMM" Then
                tempattachment = "INTERESTSUMM"
            ElseIf frmstring = "INTERESTDTLS" Then
                tempattachment = "INTERESTDTLS"
            ElseIf frmstring = "INTERESTBILLDTLS" Then
                tempattachment = "INTERESTBILLDTLS"
            ElseIf frmstring = "OUTSTANDING" Then
                tempattachment = "OUTSTANDING"
            ElseIf frmstring = "SALEANALYSIS" Then
                tempattachment = "SALEANALYSIS"
            ElseIf frmstring = "PURANALYSIS" Then
                tempattachment = "PURANALYSIS"
            Else
                tempattachment = "PROFITLOSS"
            End If

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & tempattachment & ".PDF")
            OBJWHATSAPP.FILENAME.Add(tempattachment & ".pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class