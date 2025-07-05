
Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class StoresPurchaseOrderClose

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub PurchaseOrderClose_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Space And e.Control = True Then
                'SELECT ALL DATA
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("CLOSED") = Not Convert.ToBoolean(dtrow("CLOSED"))
                Next
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PurchaseOrderClose_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'STORES'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid("")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As New DataTable
            If RBPENDING.Checked = True Then
                dt = objclsCMST.search("*", "", " (SELECT STORESPURCHASEORDER.PO_NO AS PONO, STORESPURCHASEORDER.PO_DATE AS PODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(STORESPURCHASEORDER.PO_REMARKS, '') AS REMARKS, STORESPURCHASEORDER_DESC.PO_GRIDSRNO AS POGRIDSRNO, ISNULL(STOREITEMMASTER.STOREITEM_name, '') AS STOREITEMNAME, ISNULL(STORESPURCHASEORDER_DESC.PO_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(STORESPURCHASEORDER_DESC.PO_QTY, 0) AS QTY, ISNULL(STORESPURCHASEORDER_DESC.PO_RATE, 0) AS RATE, ISNULL(STORESPURCHASEORDER_DESC.PO_RECDQTY, 0) AS RECDQTY, ISNULL(STORESPURCHASEORDER_DESC.PO_DONE, 0) AS GRIDPODONE, ISNULL(STORESPURCHASEORDER_DESC.PO_CLOSED, 0) AS CLOSED, (STORESPURCHASEORDER_DESC.PO_QTY - STORESPURCHASEORDER_DESC.PO_RECDQTY) AS BALQTY, 'PURCHASEORDER' AS TYPE FROM  STORESPURCHASEORDER INNER JOIN STORESPURCHASEORDER_DESC ON STORESPURCHASEORDER.PO_NO = STORESPURCHASEORDER_DESC.PO_NO AND STORESPURCHASEORDER.PO_YEARID = STORESPURCHASEORDER_DESC.PO_YEARID INNER JOIN STOREITEMMASTER ON STORESPURCHASEORDER_DESC.PO_ITEMID = STOREITEMMASTER.STOREITEM_id INNER JOIN LEDGERS ON STORESPURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id WHERE STORESPURCHASEORDER_DESC.PO_CLOSED = 'FALSE' and (STORESPURCHASEORDER_DESC.PO_QTY-STORESPURCHASEORDER_DESC.PO_RECDQTY)>0 AND dbo.STORESPURCHASEORDER.PO_yearid=" & YearId & " UNION ALL SELECT  OPENINGSTORESPURCHASEORDER.OPO_NO AS PONO, OPENINGSTORESPURCHASEORDER.OPO_DATE AS PODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(OPENINGSTORESPURCHASEORDER.OPO_REMARKS, '') AS REMARKS, OPENINGSTORESPURCHASEORDER_DESC.OPO_GRIDSRNO AS POGRIDSRNO, ISNULL(STOREITEMMASTER.STOREITEM_name, '') AS ITEMNAME, ISNULL(OPENINGSTORESPURCHASEORDER_DESC.OPO_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(OPENINGSTORESPURCHASEORDER_DESC.OPO_QTY, 0) AS QTY, ISNULL(OPENINGSTORESPURCHASEORDER_DESC.OPO_RATE, 0) AS RATE, ISNULL(OPENINGSTORESPURCHASEORDER_DESC.OPO_RECDQTY, 0) AS RECDQTY, ISNULL(OPENINGSTORESPURCHASEORDER_DESC.OPO_DONE, 0) AS GRIDPODONE, ISNULL(OPENINGSTORESPURCHASEORDER_DESC.OPO_CLOSED, 0) AS CLOSED, (OPENINGSTORESPURCHASEORDER_DESC.OPO_QTY - OPENINGSTORESPURCHASEORDER_DESC.OPO_RECDQTY) AS BALQTY,'OPENING' AS TYPE FROM OPENINGSTORESPURCHASEORDER INNER JOIN OPENINGSTORESPURCHASEORDER_DESC ON OPENINGSTORESPURCHASEORDER.OPO_NO = OPENINGSTORESPURCHASEORDER_DESC.OPO_NO AND OPENINGSTORESPURCHASEORDER.OPO_YEARID = OPENINGSTORESPURCHASEORDER_DESC.OPO_YEARID INNER JOIN STOREITEMMASTER ON OPENINGSTORESPURCHASEORDER_DESC.OPO_ITEMID = STOREITEMMASTER.STOREITEM_id INNER JOIN LEDGERS ON OPENINGSTORESPURCHASEORDER.OPO_LEDGERID = LEDGERS.Acc_id WHERE OPENINGSTORESPURCHASEORDER_DESC.OPO_CLOSED = 'FALSE' and (OPENINGSTORESPURCHASEORDER_DESC.OPO_QTY-OPENINGSTORESPURCHASEORDER_DESC.OPO_RECDQTY)>0 AND dbo.OPENINGSTORESPURCHASEORDER.OPO_yearid=" & YearId & ") AS T", " ORDER BY PONO, POGRIDSRNO")
            Else
                dt = objclsCMST.search("*", "", " (SELECT STORESPURCHASEORDER.PO_NO AS PONO, STORESPURCHASEORDER.PO_DATE AS PODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(STORESPURCHASEORDER.PO_REMARKS, '') AS REMARKS, STORESPURCHASEORDER_DESC.PO_GRIDSRNO AS POGRIDSRNO, ISNULL(STOREITEMMASTER.STOREITEM_name, '') AS STOREITEMNAME, ISNULL(STORESPURCHASEORDER_DESC.PO_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(STORESPURCHASEORDER_DESC.PO_QTY, 0) AS QTY, ISNULL(STORESPURCHASEORDER_DESC.PO_RATE, 0) AS RATE, ISNULL(STORESPURCHASEORDER_DESC.PO_RECDQTY, 0) AS RECDQTY, ISNULL(STORESPURCHASEORDER_DESC.PO_DONE, 0) AS GRIDPODONE, ISNULL(STORESPURCHASEORDER_DESC.PO_CLOSED, 0) AS CLOSED, (STORESPURCHASEORDER_DESC.PO_QTY - STORESPURCHASEORDER_DESC.PO_RECDQTY) AS BALQTY, 'PURCHASEORDER' AS TYPE FROM  STORESPURCHASEORDER INNER JOIN STORESPURCHASEORDER_DESC ON STORESPURCHASEORDER.PO_NO = STORESPURCHASEORDER_DESC.PO_NO AND STORESPURCHASEORDER.PO_YEARID = STORESPURCHASEORDER_DESC.PO_YEARID INNER JOIN STOREITEMMASTER ON STORESPURCHASEORDER_DESC.PO_ITEMID = STOREITEMMASTER.STOREITEM_id INNER JOIN LEDGERS ON STORESPURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id WHERE STORESPURCHASEORDER_DESC.PO_CLOSED = 'TRUE' and (STORESPURCHASEORDER_DESC.PO_QTY-STORESPURCHASEORDER_DESC.PO_RECDQTY)>0 AND dbo.STORESPURCHASEORDER.PO_yearid=" & YearId & " UNION ALL SELECT  OPENINGSTORESPURCHASEORDER.OPO_NO AS PONO, OPENINGSTORESPURCHASEORDER.OPO_DATE AS PODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(OPENINGSTORESPURCHASEORDER.OPO_REMARKS, '') AS REMARKS, OPENINGSTORESPURCHASEORDER_DESC.OPO_GRIDSRNO AS POGRIDSRNO, ISNULL(STOREITEMMASTER.STOREITEM_name, '') AS ITEMNAME, ISNULL(OPENINGSTORESPURCHASEORDER_DESC.OPO_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(OPENINGSTORESPURCHASEORDER_DESC.OPO_QTY, 0) AS QTY, ISNULL(OPENINGSTORESPURCHASEORDER_DESC.OPO_RATE, 0) AS RATE, ISNULL(OPENINGSTORESPURCHASEORDER_DESC.OPO_RECDQTY, 0) AS RECDQTY, ISNULL(OPENINGSTORESPURCHASEORDER_DESC.OPO_DONE, 0) AS GRIDPODONE, ISNULL(OPENINGSTORESPURCHASEORDER_DESC.OPO_CLOSED, 0) AS CLOSED, (OPENINGSTORESPURCHASEORDER_DESC.OPO_QTY - OPENINGSTORESPURCHASEORDER_DESC.OPO_RECDQTY) AS BALQTY,'OPENING' AS TYPE FROM OPENINGSTORESPURCHASEORDER INNER JOIN OPENINGSTORESPURCHASEORDER_DESC ON OPENINGSTORESPURCHASEORDER.OPO_NO = OPENINGSTORESPURCHASEORDER_DESC.OPO_NO AND OPENINGSTORESPURCHASEORDER.OPO_YEARID = OPENINGSTORESPURCHASEORDER_DESC.OPO_YEARID INNER JOIN STOREITEMMASTER ON OPENINGSTORESPURCHASEORDER_DESC.OPO_ITEMID = STOREITEMMASTER.STOREITEM_id INNER JOIN LEDGERS ON OPENINGSTORESPURCHASEORDER.OPO_LEDGERID = LEDGERS.Acc_id WHERE OPENINGSTORESPURCHASEORDER_DESC.OPO_CLOSED = 'TRUE' and (OPENINGSTORESPURCHASEORDER_DESC.OPO_QTY-OPENINGSTORESPURCHASEORDER_DESC.OPO_RECDQTY)>0 AND dbo.OPENINGSTORESPURCHASEORDER.OPO_yearid=" & YearId & ") AS T", " ORDER BY PONO, POGRIDSRNO")
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
                If MsgBox("You have trying to Re-Open Closed Orders, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            For I As Integer = 0 To gridbill.RowCount - 1
                Dim DTROW As DataRow = gridbill.GetDataRow(I)
                If RBPENDING.Checked = True Then
                    If Convert.ToBoolean(DTROW("CLOSED")) = True Then
                        If DTROW("TYPE") = "PURCHASEORDER" Then Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE STORESPURCHASEORDER_DESC SET PO_CLOSED = 1 WHERE PO_NO = " & Val(DTROW("PONO")) & " AND PO_GRIDSRNO = " & Val(DTROW("POGRIDSRNO")) & " AND PO_YEARID = " & YearId, "", "") Else Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE OPENINGSTORESPURCHASEORDER_DESC SET OPO_CLOSED = 1 WHERE OPO_NO = " & Val(DTROW("PONO")) & " AND OPO_GRIDSRNO = " & Val(DTROW("POGRIDSRNO")) & " AND OPO_YEARID = " & YearId, "", "")
                    End If
                Else
                    If Convert.ToBoolean(DTROW("CLOSED")) = True Then
                        If DTROW("TYPE") = "PURCHASEORDER" Then Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE STORESPURCHASEORDER_DESC SET PO_CLOSED = 0 WHERE PO_NO = " & Val(DTROW("PONO")) & " AND PO_GRIDSRNO = " & Val(DTROW("POGRIDSRNO")) & " AND PO_YEARID = " & YearId, "", "") Else Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE OPENINGSTORESPURCHASEORDER_DESC SET OPO_CLOSED = 0 WHERE OPO_NO = " & Val(DTROW("PONO")) & " AND OPO_GRIDSRNO = " & Val(DTROW("POGRIDSRNO")) & " AND OPO_YEARID = " & YearId, "", "")
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
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("CLOSED")) = "Checked" Then
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

            Dim PATH As String = Application.StartupPath & "\Stores Purchase Order Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Stores Purchase Order Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Stores Purchase Order Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Stores Purchase Order Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            If gridbilldetails.Visible = True Then
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("CLOSED") = CHKSELECTALL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class