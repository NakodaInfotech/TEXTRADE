


Imports BL
    Imports DevExpress.XtraGrid.Views.Grid

Public Class StoresPurchaseOrderDetails

    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub StoresPurchaseOrderDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub StoresPurchaseOrderDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJPO As New ClsStoresPurchaseOrder
            Dim dt As New DataTable
            If CHKPENDING.CheckState = CheckState.Unchecked Then
                dt = OBJPO.SELECTPO(0, YearId)
            Else
                Dim OBJCMN As New ClsCommon
                dt = OBJCMN.SEARCH(" STORESPURCHASEORDER.PO_NO AS PONO, STORESPURCHASEORDER.PO_DATE AS PODATE, LEDGERS.Acc_cmpname AS NAME, STORESPURCHASEORDER.PO_REFNO AS REFNO, ISNULL(STORESPURCHASEORDER.PO_REMARKS, '') AS REMARKS, STORESPURCHASEORDER_DESC.PO_GRIDSRNO AS POGRIDSRNO, ISNULL(STOREITEMMASTER.STOREITEM_name, '') AS STORESITEMNAME, ISNULL(STORESPURCHASEORDER_DESC.PO_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(STORESPURCHASEORDER_DESC.PO_MONTH1, 0) AS MONTH1, ISNULL(STORESPURCHASEORDER_DESC.PO_MONTH2, 0) AS MONTH2, ISNULL(STORESPURCHASEORDER_DESC.PO_MONTH3, 0) AS MONTH3, ISNULL(STORESPURCHASEORDER_DESC.PO_STOCK, 0) AS STOCK, STORESPURCHASEORDER_DESC.PO_QTY AS QTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(STORESPURCHASEORDER_DESC.PO_RATE, 0) AS RATE, ISNULL(STORESPURCHASEORDER_DESC.PO_AMT, 0) AS AMT, ISNULL(STORESPURCHASEORDER_DESC.PO_RECDQTY, 0) AS RECDQTY, ISNULL(STORESPURCHASEORDER_DESC.PO_DONE, 0) AS GRIDPODONE, ISNULL(STORESPURCHASEORDER_DESC.PO_CLOSED, 0) AS CLOSED, ROUND((PO_QTY - PO_RECDQTY),2) AS BALQTY ", "", " STORESPURCHASEORDER INNER JOIN STORESPURCHASEORDER_DESC ON STORESPURCHASEORDER.PO_NO = STORESPURCHASEORDER_DESC.PO_NO AND STORESPURCHASEORDER.PO_YEARID = STORESPURCHASEORDER_DESC.PO_YEARID INNER JOIN STOREITEMMASTER ON STORESPURCHASEORDER_DESC.PO_ITEMID = STOREITEMMASTER.STOREITEM_id INNER JOIN LEDGERS ON STORESPURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN UNITMASTER ON STORESPURCHASEORDER_DESC.PO_QTYUNITID = UNITMASTER.unit_id ", " AND ISNULL(STORESPURCHASEORDER_DESC.PO_CLOSED, 0) = 'FALSE' AND ROUND((PO_QTY - PO_RECDQTY),2) > 0 AND ISNULL(STORESPURCHASEORDER_DESC.PO_CLOSED, 0) = 'FALSE' AND STORESPURCHASEORDER.PO_yearid=" & YearId & " ORDER BY STORESPURCHASEORDER.PO_NO")
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

    Sub showform(ByVal editval As Boolean, ByVal PONO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New StoresPurchaseOrder
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPPONO = PONO
                objPO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("PONO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("PONO"))
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
                    e.Appearance.BackColor = Color.LightSkyBlue
                ElseIf val(View.GetRowCellDisplayText(e.RowHandle, View.Columns("RECDQTY"))) > 0 Then
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



End Class