
Imports BL

Public Class LogsUpdate

    Sub FILLGRID()
        Try
            Dim Objcls As New ClsCommonMaster
            Dim dt As DataTable = Objcls.search(" CAST(0 AS BIT) AS CHK, UPDATE_LOGS.UPDATE_ID AS ID, UPDATE_LOGS.UPDATE_TABLE AS [TABLE], UPDATE_LOGS.UPDATE_DATE AS DATE, UPDATE_LOGS.UPDATE_REMARKS AS REMARKS, USERMASTER.User_Name AS [USER]", " ", "  UPDATE_LOGS INNER JOIN USERMASTER ON UPDATE_LOGS.UPDATE_USERID = USERMASTER.User_id ", " AND UPDATE_LOGS.UPDATE_YEARID = " & YearId & " ORDER BY UPDATE_LOGS.UPDATE_DATE")
            griddetails.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LogsUpdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub UpdateDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdrefresh.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Update Logs.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Update Logs"
            griddetails.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Update Logs", GRIDBILL.VisibleColumns.Count + GRIDBILL.GroupCount)
        Catch ex As Exception
            MsgBox("Update Logs Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub griddetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles griddetails.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Delete And UserName = "Admin" Then
                If MsgBox("Delete Log " & GRIDBILL.GetFocusedRowCellValue("TABLE"), MsgBoxStyle.YesNo) = vbYes Then
                    Dim OBJOP As New ClsLogsUpdate
                    OBJOP.alParaval.Add(GRIDBILL.GetFocusedRowCellValue("ID").ToString)
                    OBJOP.alParaval.Add(YearId)
                    Dim INTRES As Integer = OBJOP.DELETE
                    FILLGRID()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub fromdate_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fromdate.Validated
        Try
            If Todate.Value.Date < fromdate.Value.Date Then
                MsgBox("Invalid Date")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Todate_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Todate.Validated
        Try
            If fromdate.Value.Date > Todate.Value.Date Then
                MsgBox("Invalid Date")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddel.Click
        Try
            If CHKFROM.CheckState = CheckState.Checked AndAlso fromdate.Value.Date <= Todate.Value.Date Then
                If MsgBox("Delete Logs ", MsgBoxStyle.YesNo) = vbYes Then
                    Dim OBJOP As New ClsLogsUpdate
                    OBJOP.alParaval.Add(fromdate.Value.Date)
                    OBJOP.alParaval.Add(Todate.Value.Date)
                    OBJOP.alParaval.Add(YearId)
                    Dim INTRES As Integer = OBJOP.DELETE_DATE
                    FILLGRID()
                    Exit Sub
                End If
            End If

            'Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            Dim OBJCMN As New ClsCommon
            If MsgBox("Delete Logs ", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            For I As Integer = 0 To GRIDBILL.RowCount - 1
                Dim ROW As DataRow = GRIDBILL.GetDataRow(I)
                If ROW("CHK") = True Then OBJCMN.Execute_Any_String("DELETE FROM UPDATE_LOGS WHERE UPDATE_ID = " & Val(ROW("ID")), "", "")
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LogsUpdate_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "DILIPNEW" Then
                cmddel.Enabled = False
            Else
                If UserName <> "Admin" Then cmddel.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class