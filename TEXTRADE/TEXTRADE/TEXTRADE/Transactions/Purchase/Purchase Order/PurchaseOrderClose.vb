﻿
Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class PurchaseOrderClose

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
            DTROW = USERRIGHTS.Select("FormName = 'PURCHASE ORDER'")
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
                dt = objclsCMST.search("*", "", " (SELECT  PURCHASEORDER.PO_NO AS PONO, PURCHASEORDER.PO_DATE AS PODATE, PURCHASEORDER.PO_DUEDATE AS DUEDATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(PURCHASEORDER.PO_CRDAYS, 0) AS CRDAYS,ISNULL(PURCHASEORDER.PO_DELDAYS, 0) AS DELDAYS, ISNULL(PURCHASEORDER.PO_REFNO, '0') AS REFNO, ISNULL(PURCHASEORDER.PO_REMARKS, '') AS REMARKS, ISNULL(PURCHASEORDER.PO_DONE, 0) AS PODONE, PURCHASEORDER_DESC.PO_GRIDSRNO AS POGRIDSRNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(PURCHASEORDER_DESC.PO_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(PURCHASEORDER_DESC.PO_PDESNO, '') AS PDESNO, ISNULL(PURCHASEORDER_DESC.PO_PSHADE, '') AS PSHADE, ISNULL(PURCHASEORDER_DESC.PO_CUT, 0) AS CUT, ISNULL(PURCHASEORDER_DESC.PO_QTY, 0) AS QTY,ISNULL(PURCHASEORDER_DESC.PO_MTRS, 0) AS MTRS, ISNULL(PURCHASEORDER_DESC.PO_RATE, 0) AS RATE, ISNULL(PURCHASEORDER_DESC.PO_RECDQTY, 0) AS RECDQTY,ISNULL(PURCHASEORDER_DESC.PO_RECDMTRS, 0) AS RECDMTRS, ISNULL(PURCHASEORDER_DESC.PO_DONE, 0) AS GRIDPODONE, ISNULL(BROKERLEDGERS.Acc_cmpname, '') AS BROKER,ISNULL(PURCHASEORDER.PO_ORDERTYPE, '') AS ORDERTYPE, ISNULL(PURCHASEORDER_DESC.PO_CLOSED, 0) AS CLOSED, PURCHASEORDER_DESC.PO_QTY - PURCHASEORDER_DESC.PO_RECDQTY AS BALPCS, PURCHASEORDER_DESC.PO_MTRS - PURCHASEORDER_DESC.PO_RECDMTRS AS BALMTRS, 'PURCHASEORDER' AS TYPE FROM  PURCHASEORDER INNER JOIN PURCHASEORDER_DESC ON PURCHASEORDER.PO_NO = PURCHASEORDER_DESC.PO_NO AND PURCHASEORDER.PO_YEARID = PURCHASEORDER_DESC.PO_YEARID INNER JOIN ITEMMASTER ON PURCHASEORDER_DESC.PO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN  DESIGNMASTER ON PURCHASEORDER_DESC.PO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS AS BROKERLEDGERS ON PURCHASEORDER.PO_BROKERID = BROKERLEDGERS.Acc_id INNER JOIN LEDGERS ON PURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON PURCHASEORDER_DESC.PO_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON PURCHASEORDER_DESC.PO_COLORID = COLORMASTER.COLOR_id WHERE PURCHASEORDER_DESC.PO_CLOSED = 'FALSE' and (PURCHASEORDER_DESC.PO_MTRS-PURCHASEORDER_DESC.PO_RECDMTRS)>0 AND dbo.PURCHASEORDER.PO_yearid=" & YearId & " UNION ALL SELECT  OPENINGPURCHASEORDER.OPO_NO AS PONO, OPENINGPURCHASEORDER.OPO_DATE AS PODATE, OPENINGPURCHASEORDER.OPO_DUEDATE AS DUEDATE, LEDGERS.Acc_cmpname AS NAME,ISNULL(OPENINGPURCHASEORDER.OPO_CRDAYS, 0) AS CRDAYS, ISNULL(OPENINGPURCHASEORDER.OPO_DELDAYS, 0) AS DELDAYS, ISNULL(OPENINGPURCHASEORDER.OPO_REFNO, '0') AS REFNO,ISNULL(OPENINGPURCHASEORDER.OPO_REMARKS, '') AS REMARKS, ISNULL(OPENINGPURCHASEORDER.OPO_DONE, 0) AS PODONE, OPENINGPURCHASEORDER_DESC.OPO_GRIDSRNO AS POGRIDSRNO,ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY,ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_PDESNO, '') AS PDESNO,ISNULL(OPENINGPURCHASEORDER_DESC.OPO_PSHADE, '') AS PSHADE, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_CUT, 0) AS CUT, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_QTY, 0) AS QTY,ISNULL(OPENINGPURCHASEORDER_DESC.OPO_MTRS, 0) AS MTRS, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_RATE, 0) AS RATE, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_RECDQTY, 0) AS RECDQTY,ISNULL(OPENINGPURCHASEORDER_DESC.OPO_RECDMTRS, 0) AS RECDMTRS, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_DONE, 0) AS GRIDPODONE, ISNULL(BROKERLEDGERS.Acc_cmpname, '') AS BROKER,ISNULL(OPENINGPURCHASEORDER.OPO_ORDERTYPE, '') AS ORDERTYPE, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_CLOSED, 0) AS CLOSED,OPENINGPURCHASEORDER_DESC.OPO_QTY - OPENINGPURCHASEORDER_DESC.OPO_RECDQTY AS BALPCS,OPENINGPURCHASEORDER_DESC.OPO_MTRS - OPENINGPURCHASEORDER_DESC.OPO_RECDMTRS AS BALMTRS, 'OPENING' AS TYPE FROM OPENINGPURCHASEORDER INNER JOIN OPENINGPURCHASEORDER_DESC ON OPENINGPURCHASEORDER.OPO_NO = OPENINGPURCHASEORDER_DESC.OPO_NO AND OPENINGPURCHASEORDER.OPO_YEARID = OPENINGPURCHASEORDER_DESC.OPO_YEARID INNER JOIN ITEMMASTER ON OPENINGPURCHASEORDER_DESC.OPO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGPURCHASEORDER_DESC.OPO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS AS BROKERLEDGERS ON OPENINGPURCHASEORDER.OPO_BROKERID = BROKERLEDGERS.Acc_id INNER JOIN LEDGERS ON OPENINGPURCHASEORDER.OPO_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGPURCHASEORDER_DESC.OPO_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON OPENINGPURCHASEORDER_DESC.OPO_COLORID = COLORMASTER.COLOR_id WHERE OPENINGPURCHASEORDER_DESC.OPO_CLOSED = 'FALSE' and (OPENINGPURCHASEORDER_DESC.OPO_MTRS-OPENINGPURCHASEORDER_DESC.OPO_RECDMTRS)>0 AND dbo.OPENINGPURCHASEORDER.OPO_yearid=" & YearId & ") AS T", " ORDER BY PONO, POGRIDSRNO")
            Else
                dt = objclsCMST.search("*", "", " (SELECT PURCHASEORDER.PO_NO AS PONO, PURCHASEORDER.PO_DATE AS PODATE, PURCHASEORDER.PO_DUEDATE AS DUEDATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(PURCHASEORDER.PO_CRDAYS, 0) AS CRDAYS, ISNULL(PURCHASEORDER.PO_DELDAYS, 0) AS DELDAYS, ISNULL(PURCHASEORDER.PO_REFNO, '0') AS REFNO, ISNULL(PURCHASEORDER.PO_REMARKS, '') AS REMARKS, ISNULL(PURCHASEORDER.PO_DONE, 0) AS PODONE, PURCHASEORDER_DESC.PO_GRIDSRNO AS POGRIDSRNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(PURCHASEORDER_DESC.PO_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(PURCHASEORDER_DESC.PO_PDESNO, '') AS PDESNO, ISNULL(PURCHASEORDER_DESC.PO_PSHADE, '') AS PSHADE, ISNULL(PURCHASEORDER_DESC.PO_CUT, 0) AS CUT, ISNULL(PURCHASEORDER_DESC.PO_QTY, 0) AS QTY, ISNULL(PURCHASEORDER_DESC.PO_MTRS, 0) AS MTRS, ISNULL(PURCHASEORDER_DESC.PO_RATE, 0) AS RATE, ISNULL(PURCHASEORDER_DESC.PO_RECDQTY, 0) AS RECDQTY, ISNULL(PURCHASEORDER_DESC.PO_RECDMTRS, 0) AS RECDMTRS, ISNULL(PURCHASEORDER_DESC.PO_DONE, 0) AS GRIDPODONE, ISNULL(BROKERLEDGERS.Acc_cmpname, '') AS BROKER, ISNULL(PURCHASEORDER.PO_ORDERTYPE, '') AS ORDERTYPE, cast(0 as BIT) AS CLOSED, (PURCHASEORDER_DESC.PO_QTY-PURCHASEORDER_DESC.PO_RECDQTY) AS BALPCS, (PURCHASEORDER_DESC.PO_MTRS-PURCHASEORDER_DESC.PO_RECDMTRS) AS BALMTRS, 'PURCHASEORDER' AS TYPE FROM PURCHASEORDER INNER JOIN PURCHASEORDER_DESC ON PURCHASEORDER.PO_NO = PURCHASEORDER_DESC.PO_NO AND PURCHASEORDER.PO_YEARID = PURCHASEORDER_DESC.PO_YEARID INNER JOIN ITEMMASTER ON PURCHASEORDER_DESC.PO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON PURCHASEORDER_DESC.PO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS AS BROKERLEDGERS ON PURCHASEORDER.PO_BROKERID = BROKERLEDGERS.Acc_id INNER JOIN LEDGERS ON PURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON PURCHASEORDER_DESC.PO_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON PURCHASEORDER_DESC.PO_COLORID = COLORMASTER.COLOR_id WHERE PURCHASEORDER_DESC.PO_CLOSED = 'TRUE' and (PURCHASEORDER_DESC.PO_MTRS-PURCHASEORDER_DESC.PO_RECDMTRS)>0 AND dbo.PURCHASEORDER.PO_yearid=" & YearId & " UNION ALL SELECT OPENINGPURCHASEORDER.OPO_NO AS PONO, OPENINGPURCHASEORDER.OPO_DATE AS PODATE, OPENINGPURCHASEORDER.OPO_DUEDATE AS DUEDATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(OPENINGPURCHASEORDER.OPO_CRDAYS, 0) AS CRDAYS, ISNULL(OPENINGPURCHASEORDER.OPO_DELDAYS, 0) AS DELDAYS, ISNULL(OPENINGPURCHASEORDER.OPO_REFNO, '0') AS REFNO, ISNULL(OPENINGPURCHASEORDER.OPO_REMARKS, '') AS REMARKS, ISNULL(OPENINGPURCHASEORDER.OPO_DONE, 0) AS PODONE, OPENINGPURCHASEORDER_DESC.OPO_GRIDSRNO AS POGRIDSRNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_PDESNO, '') AS PDESNO, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_PSHADE, '') AS PSHADE, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_CUT, 0) AS CUT, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_QTY, 0) AS QTY, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_MTRS, 0) AS MTRS, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_RATE, 0) AS RATE, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_RECDQTY, 0) AS RECDQTY, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_RECDMTRS, 0) AS RECDMTRS, ISNULL(OPENINGPURCHASEORDER_DESC.OPO_DONE, 0) AS GRIDPODONE, ISNULL(BROKERLEDGERS.Acc_cmpname, '') AS BROKER, ISNULL(OPENINGPURCHASEORDER.OPO_ORDERTYPE, '') AS ORDERTYPE, cast(0 as BIT) AS CLOSED, (OPENINGPURCHASEORDER_DESC.OPO_QTY-OPENINGPURCHASEORDER_DESC.OPO_RECDQTY) AS BALPCS, (OPENINGPURCHASEORDER_DESC.OPO_MTRS-OPENINGPURCHASEORDER_DESC.OPO_RECDMTRS) AS BALMTRS, 'OPENING' AS TYPE FROM OPENINGPURCHASEORDER INNER JOIN OPENINGPURCHASEORDER_DESC ON OPENINGPURCHASEORDER.OPO_NO = OPENINGPURCHASEORDER_DESC.OPO_NO AND OPENINGPURCHASEORDER.OPO_YEARID = OPENINGPURCHASEORDER_DESC.OPO_YEARID INNER JOIN ITEMMASTER ON OPENINGPURCHASEORDER_DESC.OPO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGPURCHASEORDER_DESC.OPO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS AS BROKERLEDGERS ON OPENINGPURCHASEORDER.OPO_BROKERID = BROKERLEDGERS.Acc_id INNER JOIN LEDGERS ON OPENINGPURCHASEORDER.OPO_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGPURCHASEORDER_DESC.OPO_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON OPENINGPURCHASEORDER_DESC.OPO_COLORID = COLORMASTER.COLOR_id WHERE OPENINGPURCHASEORDER_DESC.OPO_CLOSED = 'TRUE' and (OPENINGPURCHASEORDER_DESC.OPO_MTRS-OPENINGPURCHASEORDER_DESC.OPO_RECDMTRS)>0 AND dbo.OPENINGPURCHASEORDER.OPO_yearid=" & YearId & ") AS T", " ORDER BY PONO, POGRIDSRNO")
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
                        If DTROW("TYPE") = "PURCHASEORDER" Then Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE PURCHASEORDER_DESC SET PO_CLOSED = 1 WHERE PO_NO = " & Val(DTROW("PONO")) & " AND PO_GRIDSRNO = " & Val(DTROW("POGRIDSRNO")) & " AND PO_YEARID = " & YearId, "", "") Else Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE OPENINGPURCHASEORDER_DESC SET OPO_CLOSED = 1 WHERE OPO_NO = " & Val(DTROW("PONO")) & " AND OPO_GRIDSRNO = " & Val(DTROW("POGRIDSRNO")) & " AND OPO_YEARID = " & YearId, "", "")
                    End If
                Else
                    If Convert.ToBoolean(DTROW("CLOSED")) = True Then
                        If DTROW("TYPE") = "PURCHASEORDER" Then Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE PURCHASEORDER_DESC SET PO_CLOSED = 0 WHERE PO_NO = " & Val(DTROW("PONO")) & " AND PO_GRIDSRNO = " & Val(DTROW("POGRIDSRNO")) & " AND PO_YEARID = " & YearId, "", "") Else Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE OPENINGPURCHASEORDER_DESC SET OPO_CLOSED = 0 WHERE OPO_NO = " & Val(DTROW("PONO")) & " AND OPO_GRIDSRNO = " & Val(DTROW("POGRIDSRNO")) & " AND OPO_YEARID = " & YearId, "", "")
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

            Dim PATH As String = Application.StartupPath & "\Purchase Order Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Purchase Order Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Purchase Order Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Purchase Order Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
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

    Private Sub gridbill_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbill.KeyDown
        Try
            If e.KeyCode = Keys.Space Then
                Dim dtrow As DataRow = gridbill.GetFocusedDataRow()
                dtrow("CLOSED") = Not dtrow("CLOSED")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class