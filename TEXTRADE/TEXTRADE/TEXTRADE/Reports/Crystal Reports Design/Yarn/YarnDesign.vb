
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared


Public Class YarnDesign

    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PERIOD As String = ""
    Public FROMDATE As Date
    Public TODATE As Date

    Dim RPTYARNISSUEKNITTING As New YarnIssueKnittingReport
    Dim RPTYARNISSUE As New YarnIssueReport
    Dim RPTYARNISSUE_A5 As New YarnIssueReportA5
    Dim RPTYARNISSUE_VAISHALI As New YarnIssueReport__VAISHALI
    Dim RPTYARNDYEINGPROG As New YarnDyeingProgramReport

    Dim RPTCHALLAN As New YarnChallanReport
    Dim RPTCHALLAN_VAISHALI As New YarnChallanReport_VAISHALI


    Private Sub StockDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub BeamIssueDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "YARNISSUEKNITTING" Then
                crTables = RPTYARNISSUEKNITTING.Database.Tables

            ElseIf FRMSTRING = "YARNISSUE" Then
                If ClientName = "VAISHALI" Then
                    crTables = RPTYARNISSUE_VAISHALI.Database.Tables
                Else
                    If YARNISSUEA5 = True Then crTables = RPTYARNISSUE_A5.Database.Tables Else crTables = RPTYARNISSUE.Database.Tables
                End If

            ElseIf FRMSTRING = "YARNCHALLAN" Then
                If ClientName = "VAISHALI" Then
                    crTables = RPTCHALLAN_VAISHALI.Database.Tables
                Else
                    crTables = RPTCHALLAN.Database.Tables
                End If

            ElseIf FRMSTRING = "YARNDYEINGPROGRAM" Then
                crTables = RPTYARNDYEINGPROG.Database.Tables

            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            crpo.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "YARNISSUEKNITTING" Then
                crpo.ReportSource = RPTYARNISSUEKNITTING

            ElseIf FRMSTRING = "YARNISSUE" Then
                If ClientName = "VAISHALI" Then
                    crpo.ReportSource = RPTYARNISSUE_VAISHALI
                Else
                    If YARNISSUEA5 = True Then crpo.ReportSource = RPTYARNISSUE_A5 Else crpo.ReportSource = RPTYARNISSUE
                End If

            ElseIf FRMSTRING = "YARNCHALLAN" Then
                If ClientName = "VAISHALI" Then
                    crpo.ReportSource = RPTCHALLAN_VAISHALI
                Else
                    crpo.ReportSource = RPTCHALLAN
                End If

            ElseIf FRMSTRING = "YARNDYEINGPROGRAM" Then
                crpo.ReportSource = RPTYARNDYEINGPROG

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
            Dim TEMPATTACHMENT As String = "YARN ISSUE"
            Dim objmail As New SendMail
            objmail.attachment = TEMPATTACHMENT
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Yarn Issue.pdf"

            If FRMSTRING = "YARNISSUEKNITTING" Then
                expo = RPTYARNISSUEKNITTING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYARNISSUEKNITTING.Export()

            ElseIf FRMSTRING = "YARNISSUE" Then
                If ClientName = "VAISHALI" Then
                    expo = RPTYARNISSUE_VAISHALI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTYARNISSUE_VAISHALI.Export()
                Else
                    If YARNISSUEA5 = True Then
                        expo = RPTYARNISSUE_A5.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTYARNISSUE_A5.Export()
                    Else
                        expo = RPTYARNISSUE.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTYARNISSUE.Export()
                    End If
                End If

            ElseIf FRMSTRING = "YARNCHALLAN" Then
                If ClientName = "VAISHALI" Then
                    expo = RPTCHALLAN_VAISHALI.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTCHALLAN_VAISHALI.Export()
                Else
                    expo = RPTCHALLAN.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTCHALLAN.Export()
                End If

            ElseIf FRMSTRING = "YARNDYEINGPROGRAM" Then
                expo = RPTYARNDYEINGPROG.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYARNDYEINGPROG.Export()
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
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\YARN ISSUE.PDF")
            OBJWHATSAPP.FILENAME.Add("YARN ISSUE.pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class