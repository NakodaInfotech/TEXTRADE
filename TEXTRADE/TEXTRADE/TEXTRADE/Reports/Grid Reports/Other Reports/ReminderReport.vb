

Imports BL
    Imports System.Windows.Forms
    Imports DevExpress.XtraGrid.Views.Grid

Public Class ReminderReport

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub ReminderReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Space And e.Control = True Then
                'SELECT ALL DATA
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("DONE") = Not Convert.ToBoolean(dtrow("DONE"))
                Next
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ReminderReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillgrid("")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As New DataTable
            Dim WHERECLAUSE As String = ""
            If UserName <> "Admin" Then WHERECLAUSE = " AND USERMASTER.USER_NAME = '" & UserName & "'"

            If RBPENDING.Checked = True Then
                dt = objclsCMST.search(" ISNULL(REMINDER.R_NO, 0) As NO, Reminder.R_GRIDSRNO As SRNO, Reminder.R_DATE As Date, ISNULL(Reminder.R_NARRATION, '') AS NARRATION, ISNULL(USERMASTER.User_Name, '') AS ASSIGNUSER, ISNULL(REMINDER.R_CREATEDBY, '') AS CREATEDBY, ISNULL(REMINDER.R_DONE, 0) AS DONE ", "", " REMINDER LEFT OUTER JOIN USERMASTER ON REMINDER.R_ASSIGNUSERID = USERMASTER.User_id ", " AND REMINDER.R_YEARID =" & YearId & WHERECLAUSE & " AND REMINDER.R_DONE = 0 ORDER BY Reminder.R_DATE")
            Else
                dt = objclsCMST.search(" ISNULL(REMINDER.R_NO, 0) As NO, Reminder.R_GRIDSRNO As SRNO, Reminder.R_DATE As Date, ISNULL(Reminder.R_NARRATION, '') AS NARRATION, ISNULL(USERMASTER.User_Name, '') AS ASSIGNUSER, ISNULL(REMINDER.R_CREATEDBY, '') AS CREATEDBY, CAST(0 AS BIT) AS DONE ", "", " REMINDER LEFT OUTER JOIN USERMASTER ON REMINDER.R_ASSIGNUSERID = USERMASTER.User_id ", " AND REMINDER.R_YEARID =" & YearId & WHERECLAUSE & " AND REMINDER.R_DONE = 1 ORDER BY Reminder.R_DATE")
            End If
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            If RBENTERED.Checked = True Then
                If MsgBox("You have trying to Re-Open Closed Reminders, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            For I As Integer = 0 To gridbill.RowCount - 1
                Dim DTROW As DataRow = gridbill.GetDataRow(I)
                If RBPENDING.Checked = True Then
                    If Convert.ToBoolean(DTROW("DONE")) = True Then
                        Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE REMINDER SET R_DONE = 1 WHERE R_NO = " & Val(DTROW("NO")) & " And R_YEARID = " & YearId, "", "")
                    End If
                Else
                    If Convert.ToBoolean(DTROW("DONE")) = True Then
                        Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE REMINDER SET R_DONE = 0 WHERE R_NO = " & Val(DTROW("NO")) & " And R_YEARID = " & YearId, "", "")
                    End If
                End If
            Next
            MsgBox("Entries Updated")
            fillgrid("")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid("")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("DONE")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Reminder Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Reminder Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Reminder Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Reminder Details Excel File Is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            If gridbilldetails.Visible = True Then
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("DONE") = CHKSELECTALL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbill.KeyDown
        Try
            If e.KeyCode = Keys.Space Then
                Dim dtrow As DataRow = gridbill.GetFocusedDataRow()
                dtrow("DONE") = Not dtrow("DONE")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class