Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class DesignCardDesign


    Public FRMSTRING As String
    Public FORMULA As String
    Public PERIOD As String

    Dim RPTDESIGNCARD As New DesignCardRep


    Private Sub DesignCardDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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

            If FRMSTRING = "DESIGNCARD" Then crTables = RPTDESIGNCARD.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            crpo.SelectionFormula = FORMULA

            If FRMSTRING = "DESIGNCARD" Then
                crpo.ReportSource = RPTDESIGNCARD
            End If

            crpo.Zoom(100)
            crpo.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Sub DesignCardDesign_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            Dim TEMPATTACHMENT As String = "DESIGNCARD.pdf"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\DESIGNCARD.pdf"

            If FRMSTRING = "DESIGNCARD" Then
                expo = RPTDESIGNCARD.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDESIGNCARD.Export()
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
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\DESIGNCARD.PDF")
            OBJWHATSAPP.FILENAME.Add("DESIGNCARD.pdf")
            OBJWHATSAPP.ShowDialog()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
    End Class

