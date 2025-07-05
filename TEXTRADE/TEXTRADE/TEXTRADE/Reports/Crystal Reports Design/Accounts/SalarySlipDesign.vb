
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports System.IO
Imports BL
Imports System.Windows.Forms.Form
Imports CrystalDecisions.Shared

Public Class SalarySlipDesign

    Public SALNO As Integer
    Public MONTHNAME As String = ""
    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PARTYNAME As String = ""

    Dim RPTSAL As New SalarySlipReport
    Dim RPTSTATEMENT As New SalaryStatementReport

    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1

    Private Sub SalarySlipDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SalarySlipDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            If FRMSTRING = "SALARY" Then
                crTables = RPTSAL.Database.Tables
            ElseIf FRMSTRING = "SALSTATEMENT" Then
                crTables = RPTSTATEMENT.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next
            '************* END *******************


            CRPO.SelectionFormula = WHERECLAUSE
            If FRMSTRING = "SALARY" Then
                CRPO.ReportSource = RPTSAL
            ElseIf FRMSTRING = "SALSTATEMENT" Then
                CRPO.ReportSource = RPTSTATEMENT
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

            If FRMSTRING = "SALARY" Then
                OBJ = New SalarySlipReport
            ElseIf FRMSTRING = "SALSTATEMENT" Then
                OBJ = New SalaryStatementReport
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


                Dim TEMPATTACHMENT As String = ""
                If FRMSTRING = "SALARY" Then TEMPATTACHMENT = MONTHNAME & "_SAL_" & Val(SALNO) Else TEMPATTACHMENT = "Salary Statement"


                oDfDopt.DiskFileName = Application.StartupPath & "\" & TEMPATTACHMENT & ".pdf"

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

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()

        Dim tempattachment As String = ""
        If FRMSTRING = "SALARY" Then tempattachment = MONTHNAME & "_SAL_" & Val(SALNO) Else tempattachment = "Salary Statement"
        Try
            Dim objmail As New SendMail
            objmail.subject = "SALARY SLIP FOR THE MONTH OF " & MONTHNAME
            objmail.attachment = Application.StartupPath & "\" & tempattachment & ".PDF"
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable = OBJCMN.search("ACC_EMAIL AS EMAILID", "", "LEDGERS", " and ACC_CMPNAME = '" & PARTYNAME & "' AND ACC_YEARID=" & YearId)
            If dt.Rows.Count > 0 Then
                objmail.cmbfirstadd.Text = dt.Rows(0).Item(0).ToString
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
            If FRMSTRING = "SALARY" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\" & MONTHNAME & "_SAL_" & Val(SALNO) & ".PDF"
                RPTSAL.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                expo = RPTSAL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSAL.Export()
                RPTSAL.DataDefinition.FormulaFields("SENDMAIL").Text = "0"

            ElseIf FRMSTRING = "SALSTATEMENT" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\Salary Statement.PDF"
                expo = RPTSTATEMENT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSTATEMENT.Export()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class