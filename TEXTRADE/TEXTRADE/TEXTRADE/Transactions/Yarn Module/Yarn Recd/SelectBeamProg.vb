
Imports BL

Public Class SelectBeamProg

    Public PARTYNAME As String = ""
    Public PROGNO As Integer = 0
    Public DT As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectBeamProg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectBeamProg_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid()
    End Sub

    Sub fillgrid()
        Try
            Cursor.Current = Cursors.WaitCursor

            Dim WHERECLAUSE As String = ""
            If PROGNO <> 0 Then WHERECLAUSE = WHERECLAUSE & " AND BEAMPROGRAM.BEAMPRM_NO = " & PROGNO
            If PARTYNAME <> "" Then WHERECLAUSE = WHERECLAUSE & " AND LEDGERS.ACC_CMPNAME = '" & PARTYNAME & "'"
            Dim OBJCMN As New ClsCommon()
            Dim dt As DataTable = OBJCMN.search(" LEDGERS.Acc_cmpname AS NAME, BEAMPROGRAM_DESC.BEAMPRM_NO AS PROGNO, BEAMPROGRAM.BEAMPRM_DATE AS DATE, YARNQUALITYMASTER.YARN_NAME AS YARNQUALITY, ISNULL(MILLMASTER.MILL_NAME, '') AS MILLNAME, BEAMPROGRAM_DESC.BEAMPRM_LOTNO AS LOTNO, ISNULL(YARNQUALITYMASTER.YARN_DENIER, 0) AS DENIER, ISNULL(BEAMPROGRAM_DESC.BEAMPRM_REED, 0) AS REED, ISNULL(BEAMPROGRAM_DESC.BEAMPRM_TL, 0) AS TL, ISNULL(BEAMPROGRAM_DESC.BEAMPRM_REEDSPACE, 0) AS REEDSPACE, BEAMPROGRAM_DESC.BEAMPRM_WT AS WT, ISNULL(BEAMPROGRAM_DESC.BEAMPRM_REMARKS, '') AS GRIDREMARKS, ISNULL(BEAMPROGRAM.BEAMPRM_TOTALWT, 0) AS TOTALWT, ISNULL(BEAMPROGRAM.BEAMPRM_REMARK, '') AS REMARKS, BEAMPROGRAM_DESC.BEAMPRM_SRNO AS GRIDSRNO, 'BEAMPROGRAM' AS [TYPE] ", "", " BEAMPROGRAM INNER JOIN LEDGERS ON BEAMPROGRAM.BEAMPRM_LEDGERID = LEDGERS.Acc_id INNER JOIN BEAMPROGRAM_DESC ON BEAMPROGRAM.BEAMPRM_NO = BEAMPROGRAM_DESC.BEAMPRM_NO AND BEAMPROGRAM.BEAMPRM_YEARID = BEAMPROGRAM_DESC.BEAMPRM_YEARID INNER JOIN YARNQUALITYMASTER ON BEAMPROGRAM_DESC.BEAMPRM_YARNQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN MILLMASTER ON BEAMPROGRAM_DESC.BEAMPRM_MILLID = MILLMASTER.MILL_ID ", WHERECLAUSE & " AND BEAMPROGRAM.BEAMPRM_CLOSED='False' AND BEAMPROGRAM.BEAMPRM_YEARID = " & YearId & " ORDER BY BEAMPROGRAM_DESC.BEAMPRM_NO")
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

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            DT.Columns.Add("PROGNO")
            DT.Columns.Add("TYPE")

            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            If SELECTEDROWS.Count > 1 Then
                MsgBox("You Can Select Only One Program at a Time", MsgBoxStyle.Critical)
                Exit Sub
            End If


            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                Dim dtrow As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                DT.Rows.Add(dtrow("PROGNO"), dtrow("TYPE"))
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

End Class