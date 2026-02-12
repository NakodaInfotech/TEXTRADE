
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Public Class BeamIssueWeaverDesign

    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PERIOD As String = ""


    'Dim RPTBEAMISSUEDTLS As New BeamIssueDtlsReport 'error
    Dim RPTBEAMISSUE As New BeamIssueReport
    'Dim RPTBEAMISSUEBEAMNO As New BeamIssueBeamNoReport 'error

    Public PARTYNAME As String
    Public AGENTNAME As String
    Public BEAMISSUEBEAMNO As Integer
    Public REGNAME As String
    Public DIRECTPRINT As Boolean = False
    Public NOOFCOPIES As Integer = 1
    Public PRINTSETTING As Object = Nothing
    Public INVTYPE As String
    Dim tempattachment As String
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False



    Private Sub BeamIssueDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamIssueDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If DIRECTPRINT = True Then
                PRINTDIRECTLYTOPRINTER()
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

            If FRMSTRING = "BEAMISSUE" Then
                crTables = RPTBEAMISSUE.Database.Tables
            ElseIf FRMSTRING = "BEAMISSUEBEAMNO" Then
                'crTables = RPTBEAMISSUEBEAMNO.Database.Tables ' error
            ElseIf FRMSTRING = "BEAMISSUEDTLS" Then
                'crTables = RPTBEAMISSUEDTLS.Database.Tables 'error
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "BEAMISSUE" Then
                CRPO.ReportSource = RPTBEAMISSUE
            ElseIf FRMSTRING = "BEAMISSUEBEAMNO" Then
                'CRPO.ReportSource = RPTBEAMISSUEBEAMNO
            ElseIf FRMSTRING = "BEAMISSUEDTLS" Then
                ' RPTBEAMISSUEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM ISSUE DETAILS - " & PERIOD & "'"
                'CRPO.ReportSource = RPTBEAMISSUEDTLS
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()

            If FRMSTRING = "BEAMISSUE" Then
                tempattachment = "BEAMISSUE"

                'ElseIf FRMSTRING = "DEBIT" Or FRMSTRING = "PROFORMADEBIT" Then
                '    tempattachment = "DEBIT NOTE"

            End If
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = PARTYNAME
            OBJWHATSAPP.AGENTNAME = AGENTNAME
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & tempattachment & ".PDF")
            OBJWHATSAPP.FILENAME.Add(tempattachment & ".pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
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



            If FRMSTRING = "BEAMISSUE" Then
                strsearch = "  {BEAMISSUETOWEAVER.BEAMISSUE_NO} = " & BEAMISSUEBEAMNO & " AND {BEAMISSUETOWEAVER.BEAMISSUE_yearid} = " & YearId
                'CRPO.SelectionFormula = strsearch
            End If


            Dim OBJ As New Object
            If FRMSTRING = "BEAMISSUE" Then
                'OBJ = New BeamIssueDtlsReport
                'ElseIf FRMSTRING = "BEAMISSUEBEAMNO" Then
                '    OBJ = New BeamIssueBeamNoReport
                'ElseIf FRMSTRING = "BEAMISSUEDTLS" Then
                '    OBJ = New BeamIssueDtlsReport
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

            If DIRECTWHATSAPP = False Then
                OBJ.RecordSelectionFormula = strsearch
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            End If


            'If DIRECTMAIL = False Then
            '    OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
            '    OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            'Else
            '    Dim expo As New ExportOptions
            '    Dim oDfDopt As New DiskFileDestinationOptions
            '    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE_" & INVNO & ".pdf"
            '    expo = OBJ.ExportOptions
            '    If ClientName = "SAKARIA" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 1
            '    expo.ExportDestinationType = ExportDestinationType.DiskFile
            '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
            '    expo.DestinationOptions = oDfDopt
            '    OBJ.Export()
            'End If

            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions
                If FRMSTRING = "BEAMISSUE" Then
                    OBJ.RecordSelectionFormula = strsearch
                    oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_BEAMISSUE_" & BEAMISSUEBEAMNO & ".pdf"
                End If


                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                'If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
                'OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                expo = OBJ.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SendMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendMail.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()

        Try
            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\Beam Issue Details.PDF"
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
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\Beam IssuE Details.PDF"

            If FRMSTRING = "BEAMISSUE" Then
                expo = RPTBEAMISSUE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMISSUE.Export()
                'ElseIf FRMSTRING = "BEAMISSUEBEAMNO" Then
                '    expo = RPTBEAMISSUEBEAMNO.ExportOptions
                '    expo.ExportDestinationType = ExportDestinationType.DiskFile
                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                '    expo.DestinationOptions = oDfDopt
                '    RPTBEAMISSUEBEAMNO.Export()
                'ElseIf FRMSTRING = "BEAMISSUEDTLS" Then
                '    expo = RPTBEAMISSUEDTLS.ExportOptions
                '    expo.ExportDestinationType = ExportDestinationType.DiskFile
                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                '    expo.DestinationOptions = oDfDopt
                '    RPTBEAMISSUEDTLS.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub


End Class