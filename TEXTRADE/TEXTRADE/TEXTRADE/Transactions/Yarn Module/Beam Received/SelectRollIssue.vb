
Imports BL

Public Class SelectRollIssue

    Public SIZERNAME As String = ""
    Public DT As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectRollIssue_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectRollIssue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FILLGRID(" ")
    End Sub

    Sub FILLGRID(ByVal WHERE As String)
        Try
            Cursor.Current = Cursors.WaitCursor

            If SIZERNAME <> "" Then WHERE = WHERE & " AND SIZERNAME = '" & SIZERNAME & "'"

            Dim objcmn As New ClsCommon
            Dim dt As DataTable
            dt = objcmn.SEARCH(" CAST(0 AS BIT) AS CHK, [NO] ,[DATE] ,[SRNO],[TYPE],[YARNQUALITY],[MILLNAME],[ROLLS],[ENDS],[TOTALENDS],[LENGTH],[SIZERNAME], WT, DENIER ", "", " [ROLLSTOCK_SIZER] ", WHERE & " AND YEARID = " & YearId)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Dim COUNT As Integer = 0
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    COUNT = COUNT + 1
                End If
            Next
            If COUNT > 1 Then
                MsgBox("You Can Select Only One Entry")
                Exit Sub
            End If

            DT.Columns.Add("SIZERNAME")
            DT.Columns.Add("ROLLISSUENO")
            DT.Columns.Add("ROLLISSUESRNO")
            DT.Columns.Add("TYPE")
            DT.Columns.Add("MILLNAME")
            DT.Columns.Add("WT")
            DT.Columns.Add("YARNQUALITY")
            DT.Columns.Add("TOTALENDS")
            DT.Columns.Add("ROLLS")
            DT.Columns.Add("LENGTH")
            DT.Columns.Add("DENIER")


            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("SIZERNAME"), dtrow("NO"), dtrow("SRNO"), dtrow("TYPE"), dtrow("MILLNAME"), Val(dtrow("WT")), dtrow("YARNQUALITY"), Val(dtrow("TOTALENDS")), Val(dtrow("ROLLS")), Val(dtrow("LENGTH")), Val(dtrow("DENIER")))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class