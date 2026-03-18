Imports BL


Public Class SelectJobOrder

    Public SIZERNAME As String = ""
    Public DT As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectJobOrder_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectJobOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FILLGRID(" ")
    End Sub

    Sub FILLGRID(ByVal WHERE As String)
        Try
            Cursor.Current = Cursors.WaitCursor

            If SIZERNAME <> "" Then WHERE = WHERE & " AND LEDGERS.ACC_CMPNAME = '" & SIZERNAME & "'"

            Dim objcmn As New ClsCommon
            Dim dt As DataTable = objcmn.SEARCH(" CAST(0 AS BIT) AS CHK, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ALLJOBORDER.JOB_NO AS JOBNO, ALLJOBORDER.JOB_DATE AS DATE, ISNULL(ALLJOBORDER.JOB_REED, 0) AS REED, ISNULL(ALLJOBORDER.JOB_REEDSPACE, 0) AS REEDSPACE, ISNULL(ALLJOBORDER.JOB_PICKS, 0) AS PICS, ALLJOBORDER.TYPE AS FROMTYPE, ISNULL(ALLJOBORDER.JOB_REFNO, '') AS REFNO, ISNULL(ALLJOBORDER.JOB_TOTALMTRS - ALLJOBORDER.JOB_OUTMTRS, 0) AS MTRS, ITEMMASTER.item_name AS ITEMNAME  ", "", "  ALLJOBORDER INNER  JOIN ITEMMASTER ON ALLJOBORDER.JOB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS ON ALLJOBORDER.JOB_LEDGERID = LEDGERS.Acc_id  ", WHERE & " AND (ALLJOBORDER.JOB_TOTALMTRS- ALLJOBORDER.JOB_OUTMTRS >0)AND  ALLJOBORDER.JOB_CLOSE = 0 AND ALLJOBORDER.JOB_YEARID = " & YearId)
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

            DT.Columns.Add("NAME")
            DT.Columns.Add("JOBNO")
            DT.Columns.Add("REED")
            DT.Columns.Add("REEDSPACE")
            DT.Columns.Add("PICS")
            DT.Columns.Add("FROMTYPE")
            DT.Columns.Add("REFNO")
            DT.Columns.Add("ITEMNAME")


            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("NAME"), dtrow("JOBNO"), Val(dtrow("REED")), Val(dtrow("REEDSPACE")), Val(dtrow("PICS")), dtrow("FROMTYPE"), dtrow("REFNO"), dtrow("ITEMNAME"))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class



