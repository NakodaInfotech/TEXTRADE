
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class InHouseDesign

    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Dim RPTCHECKREPORT As New InHouseCheckingReport
    Dim RPTCHECKREPORT_SNCM As New InHouseCheckingReport_SNCM
    Dim RPTCHECKREPORT_RAJKRIPA As New InHouseCheckingReport_RAJKRIPA
    Dim RPTCHECKREPORT_VINTAGEINDIA As New InHouseCheckingReport_VINTAGEINDIA

    Private Sub crpo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles crpo.Load
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

            If ClientName = "RAJKRIPA" Then
                crTables = RPTCHECKREPORT_RAJKRIPA.Database.Tables
            ElseIf ClientName = "VINTAGEINDIA" Then
                crTables = RPTCHECKREPORT_VINTAGEINDIA.Database.Tables
            ElseIf ClientName = "SNCM" Then
                crTables = RPTCHECKREPORT_SNCM.Database.Tables
            Else
                crTables = RPTCHECKREPORT.Database.Tables
            End If





            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            crpo.SelectionFormula = WHERECLAUSE

            If ClientName = "RAJKRIPA" Then
                crpo.ReportSource = RPTCHECKREPORT_RAJKRIPA
            ElseIf ClientName = "VINTAGEINDIA" Then
                crpo.ReportSource = RPTCHECKREPORT_VINTAGEINDIA
            ElseIf ClientName = "SNCM" Then
                crpo.ReportSource = RPTCHECKREPORT_SNCM
            Else
                crpo.ReportSource = RPTCHECKREPORT
            End If

            crpo.Zoom(100)
            crpo.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub InHouseDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\INHOUSECHECKING.PDF")
            OBJWHATSAPP.FILENAME.Add("INHOUSECHECKING.pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\INHOUSECHECKING.PDF"

            If ClientName = "RAJKRIPA" Then
                expo = RPTCHECKREPORT_RAJKRIPA.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCHECKREPORT_RAJKRIPA.Export()

            ElseIf ClientName = "VINTAGEINDIA" Then
                expo = RPTCHECKREPORT_RAJKRIPA.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCHECKREPORT_VINTAGEINDIA.Export()

            ElseIf ClientName = "SNCM" Then
                expo = RPTCHECKREPORT_SNCM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCHECKREPORT_SNCM.Export()
            Else
                expo = RPTCHECKREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCHECKREPORT.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

End Class