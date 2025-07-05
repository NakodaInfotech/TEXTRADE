﻿
Imports BL
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Windows.Forms
Imports System.IO

Public Class SampleOrderDesign

    Public FORMULA As String
    Public PENDINGSO As String

    Dim RPTSMP As New SampleNoteReport
    Dim RPTSMPPRICELIST As New SamplePriceListReport
    Dim RPTSMPORDER As New SampleOrderReport
    Dim RPTSOSTATUS As New SampleOrderStatusReport
    Dim RPTSOSTATUSDTLS As New SampleOrderStatusDetailsReport
    Dim RPTSOSTATUSDATE As New SampleOrderStatusDateWiseReport

    Dim tempattachment As String
    Dim ConInfo As New CrystalDecisions.Shared.TableLogOnInfo
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Public TEMPNO As String
    Public FRMSTRING As String
    Public FROMDATE As String
    Public TODATE As String
    Public PERIOD As String
    Public PARTYNAME As String
    Public AGENTNAME As String
    Public SHIPTONAME As String

    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1

    Private Sub SampleOrderDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SampleOrderDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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


            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            If FRMSTRING = "SAMPLENOTE" Then
                crTables = RPTSMP.Database.Tables
            ElseIf FRMSTRING = "SAMPLEPRICELIST" Then
                crTables = RPTSMPPRICELIST.Database.Tables
            ElseIf FRMSTRING = "SAMPLEORDER" Then
                crTables = RPTSMPORDER.Database.Tables
            ElseIf FRMSTRING = "SOSTATUS" Then
                crTables = RPTSOSTATUS.Database.Tables
            ElseIf FRMSTRING = "SOSTATUSDTLS" Then
                crTables = RPTSOSTATUSDTLS.Database.Tables
            ElseIf FRMSTRING = "SOSTATUSDATE" Then
                crTables = RPTSOSTATUSDATE.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            crpo.SelectionFormula = FORMULA

            If FRMSTRING = "SAMPLENOTE" Then
                crpo.ReportSource = RPTSMP
            ElseIf FRMSTRING = "SAMPLEPRICELIST" Then
                crpo.ReportSource = RPTSMPPRICELIST
            ElseIf FRMSTRING = "SAMPLEORDER" Then
                crpo.ReportSource = RPTSMPORDER
            ElseIf FRMSTRING = "SOSTATUS" Then
                crpo.ReportSource = RPTSOSTATUS
            ElseIf FRMSTRING = "SOSTATUSDTLS" Then
                crpo.ReportSource = RPTSOSTATUSDTLS
            ElseIf FRMSTRING = "SOSTATUSDATE" Then
                crpo.ReportSource = RPTSOSTATUSDATE
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

        Dim emailid As String = ""
        Dim emailid1 As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
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

        Dim tempattachment As String

        Dim objmail As New SendMail

        If FRMSTRING = "SAMPLENOTE" Then
            tempattachment = "SAMPLENOTE"
            objmail.subject = "Sample Note"
        ElseIf FRMSTRING = "SAMPLEPRICELIST" Then
            tempattachment = "SAMPLEPRICELIST"
            objmail.subject = "Sample Price List"
        Else
            tempattachment = "SAMPLEORDER"
            objmail.subject = "Sample Order"
        End If

        Try
            'Dim objmail As New SendMail
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

            If FRMSTRING = "SAMPLENOTE" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SAMPLENOTE.PDF"
                expo = RPTSMP.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSMP.Export()
            ElseIf FRMSTRING = "SAMPLEPRICELIST" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SAMPLEPRICELIST.PDF"
                expo = RPTSMPPRICELIST.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSMPPRICELIST.Export()
            ElseIf FRMSTRING = "SAMPLEORDER" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SAMPLEORDER.PDF"
                expo = RPTSMPORDER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSMPORDER.Export()
            ElseIf FRMSTRING = "SOSTATUS" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SAMPLEORDER.PDF"
                expo = RPTSOSTATUS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSOSTATUS.Export()
            ElseIf FRMSTRING = "SOSTATUSDTLS" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SAMPLEORDER.PDF"
                expo = RPTSOSTATUSDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSOSTATUSDTLS.Export()
            ElseIf FRMSTRING = "SOSTATUSDATE" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SAMPLEORDER.PDF"
                expo = RPTSOSTATUSDATE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSOSTATUSDATE.Export()
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

            If FRMSTRING = "SAMPLENOTE" Then
                OBJ = New SampleNoteReport
            ElseIf FRMSTRING = "SAMPLEPRICELIST" Then
                OBJ = New SamplePriceListReport
            ElseIf FRMSTRING = "SAMPLEORDER" Then
                OBJ = New SampleOrderReport
            ElseIf FRMSTRING = "SOSTATUS" Then
                OBJ = New SampleOrderStatusReport
            ElseIf FRMSTRING = "SOSTATUSDTLS" Then
                OBJ = New SampleOrderStatusDetailsReport
            ElseIf FRMSTRING = "SOSTATUSDATE" Then
                OBJ = New SampleOrderStatusDateWiseReport
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
                If FRMSTRING = "SAMPLENOTE" Then
                    TEMPATTACHMENT = "SAMPLENOTE"
                ElseIf FRMSTRING = "SAMPLEPRICELIST" Then
                    TEMPATTACHMENT = "SAMPLEPRICELIST"
                Else
                    TEMPATTACHMENT = "SAMPLEORDER"
                End If


                oDfDopt.DiskFileName = Application.StartupPath & "\" & TEMPATTACHMENT & "_" & TEMPNO & ".pdf"

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

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()

            If FRMSTRING = "SAMPLENOTE" Then
                tempattachment = "SAMPLENOTE"
            ElseIf FRMSTRING = "SAMPLEPRICELIST" Then
                tempattachment = "SAMPLEPRICELIST"
            Else
                tempattachment = "SAMPLEORDER"
            End If

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = PARTYNAME
            OBJWHATSAPP.AGENTNAME = AGENTNAME
            If PARTYNAME <> SHIPTONAME Then OBJWHATSAPP.OTHERNAME1 = SHIPTONAME
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & tempattachment & ".PDF")
            OBJWHATSAPP.FILENAME.Add(tempattachment & ".pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class