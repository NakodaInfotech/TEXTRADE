Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class YarnStockOutDetails
    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public TEMPRECONO As Integer

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub


    Private Sub YarnStockOutDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'YARNSTOCKRECO'")
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

    Private Sub YarnStockOutDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
            dt = OBJCMN.SEARCH(" ISNULL(YARNSTOCKADJUSTMENT.YSA_NO, 0) AS SANO, YARNSTOCKADJUSTMENT.YSA_DATE AS DATE, GODOWNMASTER.GODOWN_name AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS YARNITEM,  ISNULL(MILLMASTER.MILL_NAME, '') AS MILL, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_PARTYLOTNO, '') AS PARTYLOTNO,  ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_PARTYCOLOR, '') AS PARTYCOLOR, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_LOTNO, '') AS LOTNO, ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_DESC, '') AS [DESC], ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_BAGS, 0) AS BAGS, ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_WT, 0) AS WT, ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_CONES, '') AS CONES, ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_LRNO, '') AS LRNO, ISNULL(RACKMASTER.RACK_NAME, '') AS RACK, ISNULL(YARNSTOCKADJUSTMENT_DESC.SA_RATE, 0) AS RATE, ISNULL(YARNSTOCKADJUSTMENT_DESC.SA_PER, '') AS PER, ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_AMOUNT, 0) AS AMOUNT, ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_BARCODE, '') AS BARCODE, ISNULL(YARNSTOCKADJUSTMENT.YSA_REMARKS, '') AS REMARKS , ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_FROMNO, '') AS FROMNO ,ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_FROMSRNO, '') AS FROMSRNO ,ISNULL(YARNSTOCKADJUSTMENT_DESC.YSA_FROMTYPE, '') AS FROMTYPE  ", "", " YARNSTOCKADJUSTMENT INNER JOIN YARNSTOCKADJUSTMENT_DESC ON YARNSTOCKADJUSTMENT.YSA_NO = YARNSTOCKADJUSTMENT_DESC.YSA_NO AND  YARNSTOCKADJUSTMENT.YSA_yearid = YARNSTOCKADJUSTMENT_DESC.YSA_YEARID LEFT OUTER JOIN RACKMASTER ON YARNSTOCKADJUSTMENT_DESC.YSA_RACKID = RACKMASTER.RACK_ID LEFT OUTER JOIN COLORMASTER ON YARNSTOCKADJUSTMENT_DESC.YSA_SHADEID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON YARNSTOCKADJUSTMENT_DESC.YSA_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN MILLMASTER ON YARNSTOCKADJUSTMENT_DESC.YSA_MILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN YARNQUALITYMASTER ON YARNSTOCKADJUSTMENT_DESC.YSA_ITEMID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON YARNSTOCKADJUSTMENT.YSA_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON YARNSTOCKADJUSTMENT.YSA_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON YARNSTOCKADJUSTMENT.YSA_GODOWNID = GODOWNMASTER.GODOWN_id  ", " AND dbo.YARNSTOCKADJUSTMENT.YSA_yearid=" & YearId & " order by DBO.YARNSTOCKADJUSTMENT.YSA_NO")
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
                Dim objPO As New YarnStockReco
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPRECONO = SANO
                objPO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
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

    Private Sub gridbilldetails_DoubleClick(sender As Object, e As EventArgs)
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SANO"))
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

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Yarn Stock Out Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Yarn Stock Out Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Yarn Stock Out Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Yarn Stock Out Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub


End Class