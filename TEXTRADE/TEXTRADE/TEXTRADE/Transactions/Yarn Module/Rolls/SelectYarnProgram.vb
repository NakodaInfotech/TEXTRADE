Imports BL

Public Class SelectYarnProgram
    Public DT As New DataTable
    Public WARPERNAME As String = ""

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectStock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FILLGRID(" ")
    End Sub

    Sub FILLGRID(ByVal WHERE As String)
        Try
            Cursor.Current = Cursors.WaitCursor

            If WARPERNAME <> "" Then WHERE = WHERE & " AND WARPERLEDGERS.ACC_CMPNAME = '" & WARPERNAME & "'"

            Dim objcmn As New ClsCommon
            Dim dt As DataTable
            dt = objcmn.SEARCH("CAST(0 AS BIT) AS CHK, PROGRAM_NO AS PROGRAMNO,PROGRAM_SRNO AS PROGRAMSRNO, PROGRAM_DATE AS DATE, PROGRAM_ENDS AS ENDS, PROGRAM_ROLLS AS ROLLS, PROGRAM_LENGTH AS LENGTH, PROGRAM_TOTALENDS AS TOTALENDS, QUALITY_NAME AS QUALITY, MILLLEDGERS.ACC_CMPNAME AS MILLNAME, WARPERLEDGERS.ACC_CMPNAME AS WARPERNAME, PROGRAM_GIVENBY AS GIVENBY", "", " PROGRAMMASTER INNER JOIN QUALITYMASTER ON PROGRAM_QUALITYID = QUALITY_ID INNER JOIN LEDGERS AS WARPERLEDGERS ON PROGRAM_LEDGERID = WARPERLEDGERS.ACC_ID INNER JOIN LEDGERS AS MILLLEDGERS ON PROGRAM_MILLID = MILLLEDGERS.ACC_ID ", WHERE & " AND PROGRAM_DONE = 'FALSE'")
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
                MsgBox("You Can Select Only One Program")
                Exit Sub
            End If

            DT.Columns.Add("PROGRAMNO")
            DT.Columns.Add("PROGRAMSRNO")
            DT.Columns.Add("PROGRAMDATE")
            DT.Columns.Add("ENDS")
            DT.Columns.Add("TOTALENDS")
            DT.Columns.Add("LENGTH")
            DT.Columns.Add("QUALITY")
            DT.Columns.Add("MILLNAME")
            DT.Columns.Add("ROLLS")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("PROGRAMNO"), dtrow("PROGRAMSRNO"), dtrow("DATE"), Val(dtrow("ENDS")), Val(dtrow("TOTALENDS")), Val(dtrow("LENGTH")), dtrow("QUALITY"), dtrow("MILLNAME"), Val(dtrow("ROLLS")))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class