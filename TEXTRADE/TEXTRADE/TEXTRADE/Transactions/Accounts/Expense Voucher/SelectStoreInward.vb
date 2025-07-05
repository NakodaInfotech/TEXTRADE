
Imports BL

Public Class SelectStoreInward

    Public PARTYNAME As String = ""
    Public DT As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectStoreInward_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectStoreInward_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid()
    End Sub

    Sub fillgrid()
        Try
            Dim objclspreq As New ClsCommon()
            Dim DT As DataTable = objclspreq.SEARCH("CAST (0 AS BIT) AS CHK, STOREINWARD.STORE_NO AS INWARDNO, STOREINWARD.STORE_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, STOREITEMMASTER.STOREITEM_NAME AS STOREITEMNAME, SUM(STOREINWARD_DESC.STORE_QTY) AS QTY, ISNULL(STOREINWARD_DESC.STORE_RATE, 0) AS RATE, ISNULL(DEBITLEDGERS.Acc_cmpname, '') AS DEBITLEDGER, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(STOREINWARD_DESC.STORE_NO, 0) AS FROMNO, ISNULL(STOREINWARD_DESC.STORE_GRIDSRNO, 0) AS FROMSRNO  ", "", " STOREINWARD INNER JOIN LEDGERS ON STOREINWARD.STORE_LEDGERID = LEDGERS.Acc_id INNER JOIN STOREITEMMASTER INNER JOIN STOREINWARD_DESC ON STOREITEMMASTER.STOREITEM_ID = STOREINWARD_DESC.STORE_ITEMID ON STOREINWARD.STORE_NO = STOREINWARD_DESC.STORE_NO AND STOREINWARD.STORE_YEARID = STOREINWARD_DESC.STORE_YEARID LEFT OUTER JOIN HSNMASTER ON STOREITEMMASTER.STOREITEM_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN LEDGERS AS DEBITLEDGERS ON STOREITEMMASTER.STOREITEM_DEBITLEDGERID = DEBITLEDGERS.Acc_id ", " AND LEDGERS.ACC_CMPNAME = '" & PARTYNAME & "' AND STOREINWARD.STORE_DONE = 'FALSE' AND STOREINWARD.STORE_YEARID = " & YearId & " GROUP BY STOREINWARD.STORE_NO, STOREINWARD.STORE_DATE, LEDGERS.Acc_cmpname, STOREITEMMASTER.STOREITEM_NAME, ISNULL(STOREINWARD_DESC.STORE_RATE, 0), ISNULL(DEBITLEDGERS.Acc_cmpname, ''), ISNULL(HSNMASTER.HSN_CODE, ''),ISNULL(STOREINWARD_DESC.STORE_NO, 0), ISNULL(STOREINWARD_DESC.STORE_GRIDSRNO, 0) ORDER BY STOREINWARD.STORE_NO")
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
            DT.Columns.Add("INWARDNO")
            DT.Columns.Add("INWARDDATE")
            DT.Columns.Add("STOREITEMNAME")
            DT.Columns.Add("QTY")
            DT.Columns.Add("RATE")
            DT.Columns.Add("DEBITLEDGER")
            DT.Columns.Add("HSNCODE")
            DT.Columns.Add("FROMNO")
            DT.Columns.Add("FROMSRNO")


            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(Val(dtrow("INWARDNO")), dtrow("DATE"), dtrow("STOREITEMNAME"), Val(dtrow("QTY")), Val(dtrow("RATE")), dtrow("DEBITLEDGER"), dtrow("HSNCODE"), Val(dtrow("FROMNO")), Val(dtrow("FROMSRNO")))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            If gridbilldetails.Visible = True Then
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("CHK") = CHKSELECTALL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class