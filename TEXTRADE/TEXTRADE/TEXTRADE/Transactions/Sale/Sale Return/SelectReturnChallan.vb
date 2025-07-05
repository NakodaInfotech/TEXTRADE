﻿
Imports BL

Public Class SelectReturnChallan

    Public PARTYNAME As String = ""
    Public FRMSTRING As String = ""
    Public DT1 As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectReturnChallan_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectReturnChallan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Top = 100
        fillgrid()
        gridbilldetails.ForceInitialize()
        gridbill.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
    End Sub

    Sub fillgrid(Optional ByVal where As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If PARTYNAME <> "" Then where = where & " AND LEDGERS.Acc_cmpname='" & PARTYNAME & "'"
            Dim objclspreq As New ClsCommon()
            Dim DT1 As New DataTable
            If FRMSTRING = "PURRETURN" Then
                DT1 = objclspreq.search(" DISTINCT CAST (0 AS BIT) AS CHK, PURCHASERETURNCHALLAN.PRCH_NO AS SRCHNO, PURCHASERETURNCHALLAN.PRCH_date AS DATE, ISNULL(PRCH_CHALLANNO,'') AS PARTYCHALLANNO, LEDGERS.Acc_cmpname AS NAME, ISNULL(ITEM_NAME, '') AS ITEMNAME, ISNULL(DESIGN_NO, '') AS DESIGNNO, ISNULL(COLOR_NAME, '') AS SHADE, SUM(ISNULL(PURCHASERETURNCHALLAN_DESC.PRCH_QTY, 0)) AS PCS, SUM(ISNULL(PURCHASERETURNCHALLAN_DESC.PRCH_MTRS, 0)) AS MTRS, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR), '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN, ISNULL(PURCHASERETURNCHALLAN.PRCH_NOOFBALES, 0) AS NOOFBALES, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(PURCHASERETURNCHALLAN.PRCH_LRNO, '') AS LRNO, ISNULL(PURCHASERETURNCHALLAN.PRCH_LRDATE, '') AS LRDATE ", "", "  PURCHASERETURNCHALLAN INNER JOIN PURCHASERETURNCHALLAN_DESC ON PURCHASERETURNCHALLAN.PRCH_NO = PURCHASERETURNCHALLAN_DESC.PRCH_NO AND PURCHASERETURNCHALLAN.PRCH_YEARID = PURCHASERETURNCHALLAN_DESC.PRCH_YEARID LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON PURCHASERETURNCHALLAN.PRCH_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON PURCHASERETURNCHALLAN.PRCH_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN LEDGERS ON PURCHASERETURNCHALLAN.PRCH_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN ITEMMASTER ON PRCH_ITEMID = ITEM_ID LEFT OUTER JOIN DESIGNMASTER ON PRCH_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON PRCH_COLORID = COLOR_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID ", " AND PURCHASERETURNCHALLAN.PRCH_DONE = 0 AND PURCHASERETURNCHALLAN.PRCH_YEARID = " & YearId & where & " GROUP BY PURCHASERETURNCHALLAN.PRCH_NO, PURCHASERETURNCHALLAN.PRCH_date, ISNULL(PRCH_CHALLANNO,''), LEDGERS.Acc_cmpname, ISNULL(ITEM_NAME, '') , ISNULL(DESIGN_NO, ''), ISNULL(COLOR_NAME, '') , ISNULL(GODOWNMASTER.GODOWN_name, ''), ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR), ''), ISNULL(LEDGERS.ACC_GSTIN,''),  ISNULL(PURCHASERETURNCHALLAN.PRCH_NOOFBALES, 0), ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(PURCHASERETURNCHALLAN.PRCH_LRNO, ''), ISNULL(PURCHASERETURNCHALLAN.PRCH_LRDATE, '') order by PURCHASERETURNCHALLAN.PRCH_NO ")
            Else
                DT1 = objclspreq.search(" DISTINCT CAST (0 AS BIT) AS CHK, SALERETURNCHALLAN.SRCH_NO AS SRCHNO, SALERETURNCHALLAN.SRCH_date AS DATE, ISNULL(SRCH_CHALLANNO,'') AS PARTYCHALLANNO, LEDGERS.Acc_cmpname AS NAME, ISNULL(ITEM_NAME, '') AS ITEMNAME, ISNULL(DESIGN_NO, '') AS DESIGNNO, ISNULL(COLOR_NAME, '') AS SHADE, SUM(ISNULL(SALERETURNCHALLAN_DESC.SRCH_QTY, 0)) AS PCS, SUM(ISNULL(SALERETURNCHALLAN_DESC.SRCH_MTRS, 0)) AS MTRS, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR), '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN, ISNULL(SALERETURNCHALLAN.SRCH_NOOFBALES, 0) AS NOOFBALES, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(SALERETURNCHALLAN.SRCH_LRNO, '') AS LRNO, SALERETURNCHALLAN.SRCH_LRDATE AS LRDATE ", "", "  SALERETURNCHALLAN INNER JOIN SALERETURNCHALLAN_DESC ON SALERETURNCHALLAN.SRCH_NO = SALERETURNCHALLAN_DESC.SRCH_NO AND SALERETURNCHALLAN.SRCH_YEARID = SALERETURNCHALLAN_DESC.SRCH_YEARID LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON SALERETURNCHALLAN.SRCH_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON SALERETURNCHALLAN.SRCH_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN LEDGERS ON SALERETURNCHALLAN.SRCH_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN ITEMMASTER ON SRCH_ITEMID = ITEM_ID LEFT OUTER JOIN DESIGNMASTER ON SRCH_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON SRCH_COLORID = COLOR_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID ", "  AND SALERETURNCHALLAN.SRCH_DONE = 0 AND SALERETURNCHALLAN.SRCH_YEARID = " & YearId & where & " GROUP BY SALERETURNCHALLAN.SRCH_NO, SALERETURNCHALLAN.SRCH_date, ISNULL(SRCH_CHALLANNO,''), LEDGERS.Acc_cmpname, ISNULL(ITEM_NAME, '') , ISNULL(DESIGN_NO, ''), ISNULL(COLOR_NAME, '') , ISNULL(GODOWNMASTER.GODOWN_name, ''), ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR), ''), ISNULL(LEDGERS.ACC_GSTIN,''),  ISNULL(SALERETURNCHALLAN.SRCH_NOOFBALES, 0), ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(SALERETURNCHALLAN.SRCH_LRNO, ''), SALERETURNCHALLAN.SRCH_LRDATE order by SALERETURNCHALLAN.SRCH_NO ")
            End If
            gridbilldetails.DataSource = DT1
            If DT1.Rows.Count > 0 Then
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
            If FRMSTRING = "PURRETURN" Then DT1.Columns.Add("PRCHNO") Else DT1.Columns.Add("SRCHNO")
            DT1.Columns.Add("DATE")
            DT1.Columns.Add("CHALLANNO")
            DT1.Columns.Add("NAME")
            DT1.Columns.Add("PCS")
            DT1.Columns.Add("MTRS")
            DT1.Columns.Add("GODOWN")
            DT1.Columns.Add("GSTIN")
            DT1.Columns.Add("STATECODE")
            DT1.Columns.Add("NOOFBALES")
            DT1.Columns.Add("TRANSNAME")
            DT1.Columns.Add("LRNO")
            DT1.Columns.Add("LRDATE")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT1.Rows.Add(dtrow("SRCHNO"), dtrow("DATE"), dtrow("PARTYCHALLANNO"), dtrow("NAME"), Val(dtrow("PCS")), Val(dtrow("MTRS")), dtrow("GODOWN"), dtrow("GSTIN"), dtrow("STATECODE"), dtrow("NOOFBALES"), dtrow("TRANSNAME"), dtrow("LRNO"), dtrow("LRDATE"))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CHKALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkall.CheckedChanged
        Try
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                dtrow("CHK") = chkall.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class