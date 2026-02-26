Imports BL
Imports DevExpress.XtraGrid.Views.Grid


Public Class StoreStockRecoDetails

    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public TEMPRECONO As Integer

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub StoreStockRecoDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'STORESTOCKRECO'")
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



    Private Sub StoreStockRecoDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Sub FILLGRID()
        Try
            'Dim OBJSTOCKRECO As New ClsStoreStockAdjustment
            Dim dt As New DataTable
            'dt = OBJSTOCKRECO.SELECTSTORESTOCKADJUSTMENT(TEMPRECONO, CmpId, Locationid, YearId)
            Dim OBJCMN As New ClsCommon
            dt = OBJCMN.SEARCH(" STORESTOCKADJUSTMENT.SA_NO AS SANO, STORESTOCKADJUSTMENT.SA_DATE AS DATE, GODOWNMASTER.GODOWN_name AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(STORESTOCKADJUSTMENT.SA_REMARKS, '') AS REMARKS, STORESTOCKADJUSTMENT.SA_TOTALINQTY AS TOTALINQTY, STORESTOCKADJUSTMENT.SA_TOTALOUTQTY AS TOTALOUTQTY, STOREITEMMASTER.STOREITEM_NAME AS ITEMNAME, UNITMASTER.unit_name AS UNIT, STORESTOCKADJUSTMENT_DESC.SA_DESC AS GDESC, STORESTOCKADJUSTMENT_DESC.SA_QTY AS QTY,  STORESTOCKADJUSTMENT_DESC.SA_RATE AS RATE, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(STORESTOCKADJUSTMENT.SA_CHALLANNO, '') AS CHALLANNO, STORESTOCKADJUSTMENT.SA_CHALLANDATE AS CHALLANDATE, ISNULL(STORESTOCKADJUSTMENT_DESC.SA_GRIDSRNO, 0) AS GRIDSRNO ", "", " STORESTOCKADJUSTMENT LEFT OUTER JOIN STORESTOCKADJUSTMENT_DESC INNER JOIN STOREITEMMASTER ON STOREITEMMASTER.STOREITEM_ID = STORESTOCKADJUSTMENT_DESC.SA_ITEMID ON STORESTOCKADJUSTMENT.SA_NO = STORESTOCKADJUSTMENT_DESC.SA_NO AND  STORESTOCKADJUSTMENT.SA_yearid = STORESTOCKADJUSTMENT_DESC.SA_YEARID INNER JOIN LEDGERS AS TRANSLEDGERS ON STORESTOCKADJUSTMENT.SA_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON STORESTOCKADJUSTMENT.SA_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN UNITMASTER ON STORESTOCKADJUSTMENT_DESC.SA_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN GODOWNMASTER ON STORESTOCKADJUSTMENT.SA_GODOWNID = GODOWNMASTER.GODOWN_id ", "  AND (STORESTOCKADJUSTMENT.SA_yearid= " & YearId & ") ORDER BY dbo.STORESTOCKADJUSTMENT.SA_NO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Sub showform(ByVal editval As Boolean, ByVal SANO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New StoreStockReco
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPRECONO = SANO
                objPO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs)
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

    Private Sub gridbilldetails_DoubleClick(sender As Object, e As EventArgs) Handles gridbilldetails.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SANO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLSTOCKOUT_Click(sender As Object, e As EventArgs) Handles TOOLSTOCKOUT.Click
        Try
            Dim OBJSTCK As New StoreStockRecoOutDetails
            OBJSTCK.MdiParent = MDIMain
            OBJSTCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLSTOCKIN_Click_1(sender As Object, e As EventArgs) Handles TOOLSTOCKIN.Click
        Try
            Dim OBJSTOCK As New StoreStockRecoInDetails
            OBJSTOCK.MdiParent = MDIMain
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs)
        Try
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
            showform(True, gridbill.GetFocusedRowCellValue("SANO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs)
        Try

            Dim PATH As String = Application.StartupPath & "\Store Stock Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Store Stock Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Store Stock Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Store Stock Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub


End Class