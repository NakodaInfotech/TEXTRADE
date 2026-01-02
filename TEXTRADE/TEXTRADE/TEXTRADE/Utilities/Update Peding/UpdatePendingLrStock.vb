Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class UpdatePendingLrStock

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub UpdatePendingLrStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE ORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            'PENDING
            If RBPENDING.Checked = True Then
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

                    If DTROW("TYPE") = "PURCHASE" Then DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASEMASTER SET BILL_TEMPSOLD = 1 WHERE BILL_NO = " & Val(DTROW("BILLNO")) & " AND  BILL_YEARID = " & YearId, "", "")
                    If DTROW("TYPE") = "OPENINGPUR" Then DT = OBJCMN.Execute_Any_String(" UPDATE STOCKMASTER SET SM_TEMPSOLD = 1 WHERE SM_NO = " & Val(DTROW("BILLNO")) & " AND  SM_YEARID = " & YearId, "", "")

                Next
                MsgBox("Details Updated Successfully")
                FILLGRID()
                gridbill.Focus()
            End If

            'ENTERED
            If RBENTERED.Checked = True Then
                If MsgBox("You have trying to Re-Open Close Job Docket Batch, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                    If DTROW("TYPE") = "PURCHASE" Then DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASEMASTER SET BILL_TEMPSOLD = 0 WHERE BILL_NO = " & Val(DTROW("BILLNO")) & " AND  BILL_YEARID = " & YearId, "", "")
                    If DTROW("TYPE") = "OPENINGPUR" Then DT = OBJCMN.Execute_Any_String(" UPDATE STOCKMASTER SET SM_TEMPSOLD = 0 WHERE SM_NO = " & Val(DTROW("BILLNO")) & " AND  SM_YEARID = " & YearId, "", "")
                Next
                MsgBox("Details Updated Successfully")
                FILLGRID()
                gridbill.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As New DataTable
            If RBPENDING.Checked = True Then

                dt = OBJCMN.search("SELECT PURCHASEMASTER.BILL_NO AS BILLNO, PurchaseMaster.BILL_DATE As BILLDATE, RegisterMaster.register_name As REGNAME, PurchaseMaster.BILL_REGISTERID As REGID, LEDGERS.Acc_id As LEDGERID, TRANSLEDGERS.Acc_id As TRANSID, PurchaseMaster.BILL_INITIALS As BILLINITIALS, PURCHASEMASTER_DESC.BILL_gridsrno As GRIDSRNO, LEDGERS.Acc_cmpname As NAME, ISNULL(TRANSLEDGERS.ACC_CMPNAME,'') AS TRANSNAME, PURCHASEMASTER.BILL_PARTYBILLNO AS PARTYBILLNO,  PURCHASEMASTER.BILL_PARTYBILLDATE AS PARTYBILLDATE, ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME, ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE, ISNULL(PURCHASEMASTER_DESC.BILL_AQTY,0) AS AQTY, ISNULL(PURCHASEMASTER_DESC.BILL_FOLDPER,0) AS FOLDPER, PURCHASEMASTER.BILL_TOTALQTY AS TOTALQTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, PURCHASEMASTER.BILL_TOTALMTRS AS TOTALMTRS, PURCHASEMASTER.BILL_CHADTI AS WT, PURCHASEMASTER_DESC.BILL_rate AS RATE, PURCHASEMASTER_DESC.BILL_BALENO AS BALENO  ,PURCHASEMASTER.BILL_LRNO AS LRNO, ISNULL(CATEGORYMASTER.category_name, '') AS CATEGORY, PURCHASEMASTER.BILL_CMPID AS CMPID, PURCHASEMASTER.BILL_YEARID AS YEARID, 'PURCHASE' AS TYPE, PURCHASEMASTER.BILL_SOLD AS SOLD, PURCHASEMASTER.BILL_TEMPSOLD AS TEMPSOLD FROM PURCHASEMASTER INNER JOIN PURCHASEMASTER_DESC ON PURCHASEMASTER.BILL_NO = PURCHASEMASTER_DESC.BILL_NO AND PURCHASEMASTER.BILL_REGISTERID = PURCHASEMASTER_DESC.BILL_REGISTERID AND PURCHASEMASTER.BILL_YEARID = PURCHASEMASTER_DESC.BILL_yearid INNER JOIN LEDGERS ON PURCHASEMASTER.BILL_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON PURCHASEMASTER_DESC.BILL_ITEMID = ITEMMASTER.item_id INNER JOIN REGISTERMASTER ON PURCHASEMASTER.BILL_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON PURCHASEMASTER.BILL_TRANSNAMEID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN UNITMASTER ON PURCHASEMASTER_DESC.BILL_QTYUNITID = UNITMASTER.unit_id LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID  where PURCHASEMASTER.BILL_TEMPSOLD = 0 AND PURCHASEMASTER.BILL_YEARID = " & YearId & " UNION ALL  SELECT STOCKMASTER.SM_NO AS BILLNO, STOCKMASTER.SM_DATE AS BILLDATE, '' AS REGNAME, 0 AS REGID, LEDGERS.Acc_id AS LEDGERID,TRANSLEDGERS.Acc_id AS TRANSID , '' AS BILLINITIALS, STOCKMASTER.SM_GRIDSRNO AS GRIDSRNO, LEDGERS.Acc_cmpname AS NAME, ISNULL(TRANSLEDGERS.ACC_CMPNAME,'') AS TRANSNAME, STOCKMASTER.SM_PARTYCHALLANNO AS PARTYBILLNO, STOCKMASTER.SM_DATE AS PARTYBILLDATE, ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME, ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE, ISNULL(STOCKMASTER.SM_AQTY,0) AS AQTY, ISNULL(STOCKMASTER.SM_FOLDPER,0) AS FOLDPER, STOCKMASTER.SM_PCS AS TOTALQTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, STOCKMASTER.SM_MTRS AS TOTALMTRS, STOCKMASTER.SM_WT AS WT, STOCKMASTER.SM_RATE AS RATE, STOCKMASTER.SM_BALENO AS BALENO, STOCKMASTER.SM_LRNO AS LRNO , ISNULL(CATEGORYMASTER.category_name, '') AS CATEGORY, STOCKMASTER.SM_CMPID AS CMPID, STOCKMASTER.SM_YEARID AS YEARID, 'OPENINGPUR' AS TYPE, STOCKMASTER.SM_SOLD AS SOLD, STOCKMASTER.SM_TEMPSOLD AS TEMPSOLD FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON STOCKMASTER.SM_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON STOCKMASTER.SM_LEDGERIDTO = TRANSLEDGERS.Acc_id LEFT OUTER JOIN UNITMASTER ON ITEMMASTER.item_unitid = UNITMASTER.unit_id LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID  where STOCKMASTER.SM_TEMPSOLD and  STOCKMASTER.SM_YEARID = " & YearId)
            Else

                'dt = OBJCMN.search("SELECT PURCHASEMASTER.BILL_NO AS BILLNO, PurchaseMaster.BILL_DATE As BILLDATE, RegisterMaster.register_name As REGNAME, PurchaseMaster.BILL_REGISTERID As REGID, LEDGERS.Acc_id As LEDGERID, TRANSLEDGERS.Acc_id As TRANSID, PurchaseMaster.BILL_INITIALS As BILLINITIALS, PURCHASEMASTER_DESC.BILL_gridsrno As GRIDSRNO, LEDGERS.Acc_cmpname As NAME, ISNULL(TRANSLEDGERS.ACC_CMPNAME,'') AS TRANSNAME, PURCHASEMASTER.BILL_PARTYBILLNO AS PARTYBILLNO,  PURCHASEMASTER.BILL_PARTYBILLDATE AS PARTYBILLDATE, ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME, ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE, ISNULL(PURCHASEMASTER_DESC.BILL_AQTY,0) AS AQTY, ISNULL(PURCHASEMASTER_DESC.BILL_FOLDPER,0) AS FOLDPER, PURCHASEMASTER.BILL_TOTALQTY AS TOTALQTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, PURCHASEMASTER.BILL_TOTALMTRS AS TOTALMTRS, PURCHASEMASTER.BILL_CHADTI AS WT, PURCHASEMASTER_DESC.BILL_rate AS RATE, PURCHASEMASTER_DESC.BILL_BALENO AS BALENO  ,PURCHASEMASTER.BILL_LRNO AS LRNO, ISNULL(CATEGORYMASTER.category_name, '') AS CATEGORY, PURCHASEMASTER.BILL_CMPID AS CMPID, PURCHASEMASTER.BILL_YEARID AS YEARID, 'PURCHASE' AS TYPE, PURCHASEMASTER.BILL_SOLD AS SOLD, PURCHASEMASTER.BILL_TEMPSOLD AS TEMPSOLD FROM PURCHASEMASTER INNER JOIN PURCHASEMASTER_DESC ON PURCHASEMASTER.BILL_NO = PURCHASEMASTER_DESC.BILL_NO AND PURCHASEMASTER.BILL_REGISTERID = PURCHASEMASTER_DESC.BILL_REGISTERID AND PURCHASEMASTER.BILL_YEARID = PURCHASEMASTER_DESC.BILL_yearid INNER JOIN LEDGERS ON PURCHASEMASTER.BILL_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON PURCHASEMASTER_DESC.BILL_ITEMID = ITEMMASTER.item_id INNER JOIN REGISTERMASTER ON PURCHASEMASTER.BILL_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON PURCHASEMASTER.BILL_TRANSNAMEID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN UNITMASTER ON PURCHASEMASTER_DESC.BILL_QTYUNITID = UNITMASTER.unit_id LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID  where PURCHASEMASTER.BILL_TEMPSOLD = 0 AND PURCHASEMASTER.BILL_YEARID = " & YearId & " UNION ALL  SELECT STOCKMASTER.SM_NO AS BILLNO, STOCKMASTER.SM_DATE AS BILLDATE, '' AS REGNAME, 0 AS REGID, LEDGERS.Acc_id AS LEDGERID,TRANSLEDGERS.Acc_id AS TRANSID , '' AS BILLINITIALS, STOCKMASTER.SM_GRIDSRNO AS GRIDSRNO, LEDGERS.Acc_cmpname AS NAME, ISNULL(TRANSLEDGERS.ACC_CMPNAME,'') AS TRANSNAME, STOCKMASTER.SM_PARTYCHALLANNO AS PARTYBILLNO, STOCKMASTER.SM_DATE AS PARTYBILLDATE, ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME, ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE, ISNULL(STOCKMASTER.SM_AQTY,0) AS AQTY, ISNULL(STOCKMASTER.SM_FOLDPER,0) AS FOLDPER, STOCKMASTER.SM_PCS AS TOTALQTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, STOCKMASTER.SM_MTRS AS TOTALMTRS, STOCKMASTER.SM_WT AS WT, STOCKMASTER.SM_RATE AS RATE, STOCKMASTER.SM_BALENO AS BALENO, STOCKMASTER.SM_LRNO AS LRNO , ISNULL(CATEGORYMASTER.category_name, '') AS CATEGORY, STOCKMASTER.SM_CMPID AS CMPID, STOCKMASTER.SM_YEARID AS YEARID, 'OPENINGPUR' AS TYPE, STOCKMASTER.SM_SOLD AS SOLD, STOCKMASTER.SM_TEMPSOLD AS TEMPSOLD FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON STOCKMASTER.SM_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON STOCKMASTER.SM_LEDGERIDTO = TRANSLEDGERS.Acc_id LEFT OUTER JOIN UNITMASTER ON ITEMMASTER.item_unitid = UNITMASTER.unit_id LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID  where STOCKMASTER.SM_TEMPSOLD and  STOCKMASTER.SM_YEARID = " & YearId & )
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

    Private Sub UpdatePendingLrStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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



    'Private Sub CHKSELECTALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTALL.CheckedChanged
    '    Try
    '        If gridbilldetails.Visible = True Then
    '            For i As Integer = 0 To gridbill.RowCount - 1
    '                Dim dtrow As DataRow = gridbill.GetDataRow(i)
    '                dtrow("CLOSED") = CHKSELECTALL.Checked
    '            Next
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub



    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Pending Lr Stock Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Pending Lr Stock Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Pending Lr Stock Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Pending Lr Stock Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub
End Class