
Imports BL

Public Class SelectRolls

    Public DT As New DataTable
    Public TEMPGODOWNNAME As String

    Sub fillgrid(ByVal WHERE As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon()
            Dim DT As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK , NO, PROGRAMNO, SRNO, TYPE, MILLNAME,YARNQUALITY, ISNULL(ENDS, 0) AS ENDS, ROLLS, WT,  TOTALENDS, LENGTH", "", "ROLLSTOCK", " AND ROLLSTOCK.GODOWN='" & TEMPGODOWNNAME & "' AND ROLLSTOCK.YEARID = " & YearId)
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
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


            DT.Columns.Add("YARNQUALITY")
            DT.Columns.Add("MILLNAME")
            DT.Columns.Add("ENDS")
            DT.Columns.Add("ROLLS")
            DT.Columns.Add("WT")
            DT.Columns.Add("NO")
            DT.Columns.Add("SRNO")
            DT.Columns.Add("TYPE")
            DT.Columns.Add("PROGRAMNO")
            'DT.Columns.Add("PROGRAMSRNO")
            DT.Columns.Add("TOTALENDS")
            DT.Columns.Add("LENGTH")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("YARNQUALITY"), dtrow("MILLNAME"), dtrow("ENDS"), Val(dtrow("ROLLS")), Val(dtrow("WT")), dtrow("NO"), dtrow("SRNO"), dtrow("TYPE"), Val("PROGRAMNO"), Val(dtrow("TOTALENDS")), Val(dtrow("LENGTH")))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectRolls_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectRolls_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid("")
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
End Class