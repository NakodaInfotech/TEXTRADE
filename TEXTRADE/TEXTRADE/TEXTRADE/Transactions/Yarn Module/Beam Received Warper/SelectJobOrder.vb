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
            Dim dt As DataTable = objcmn.SEARCH(" CAST(0 AS BIT) AS CHK, ALLJOBORDER.JOB_DATE AS DATE, ALLJOBORDER.JOB_NO AS JOBNO, ALLJOBORDER_DESC.JOB_SRNO AS JOBSRNO, ISNULL(ALLJOBORDER_DESC.JOB_REED, 0) AS REED, ISNULL(ALLJOBORDER_DESC.JOB_REEDSPACE, 0) AS REEDSPACE, ISNULL(ALLJOBORDER_DESC.JOB_PICKS, 0) AS PICS, ALLJOBORDER.TYPE AS FROMTYPE, ISNULL(ALLJOBORDER_DESC.JOB_REFNO, '') AS REFNO, ISNULL(ALLJOBORDER_DESC.JOB_MTRS - ALLJOBORDER_DESC.JOB_OUTMTRS, 0) AS JOBMTRS, ISNULL(ALLJOBORDER_DESC.JOB_ENDS, 0) AS ENDS, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(ALLJOBORDER_DESC.JOB_DESCRIPTION, '') AS DESCRIPTION, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR ", "", "  ALLJOBORDER INNER JOIN ALLJOBORDER_DESC ON ALLJOBORDER.JOB_NO = ALLJOBORDER_DESC.JOB_NO AND ALLJOBORDER.JOB_YEARID = ALLJOBORDER_DESC.JOB_YEARID AND ALLJOBORDER.TYPE = ALLJOBORDER_DESC.TYPE INNER JOIN ITEMMASTER ON ALLJOBORDER_DESC.JOB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON ALLJOBORDER_DESC.JOB_SHADEID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ALLJOBORDER_DESC.JOB_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS ON ALLJOBORDER.JOB_LEDGERID = LEDGERS.Acc_id ", WHERE & " AND (ALLJOBORDER_DESC.JOB_MTRS- ALLJOBORDER_DESC.JOB_OUTMTRS >0) AND  ALLJOBORDER_DESC.JOB_CLOSED= 'FALSE' AND ALLJOBORDER.JOB_YEARID = " & YearId & "   ORDER BY ALLJOBORDER.JOB_NO ")
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
                End If
            Next


            DT.Columns.Add("NAME")
            DT.Columns.Add("JOBNO")
            DT.Columns.Add("JOBSRNO")
            DT.Columns.Add("FROMTYPE")
            DT.Columns.Add("REED")
            DT.Columns.Add("REEDSPACE")
            DT.Columns.Add("PICS")
            DT.Columns.Add("REFNO")
            DT.Columns.Add("ITEMNAME")
            DT.Columns.Add("ENDS")
            DT.Columns.Add("JOBMTRS")
            DT.Columns.Add("DESCRIPTION")
            DT.Columns.Add("DESIGNNO")
            DT.Columns.Add("COLOR")




            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("NAME"), dtrow("JOBNO"), dtrow("JOBSRNO"), dtrow("FROMTYPE"), Val(dtrow("REED")), Val(dtrow("REEDSPACE")), Val(dtrow("PICS")), dtrow("REFNO"), dtrow("ITEMNAME"), dtrow("ENDS"), Val(dtrow("JOBMTRS")), dtrow("DESCRIPTION"), dtrow("DESIGNNO"), dtrow("COLOR"))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class



