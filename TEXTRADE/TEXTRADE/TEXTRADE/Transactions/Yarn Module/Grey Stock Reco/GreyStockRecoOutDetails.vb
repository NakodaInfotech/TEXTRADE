Imports BL
Public Class GreyStockRecoOutDetails
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub StockRecoOutDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub StockRecoOutDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DTROW() As DataRow
        DTROW = USERRIGHTS.Select("FormName = 'GREYSTOCK ADJUSTMENT'")
        USERADD = DTROW(0).Item(1)
        USEREDIT = DTROW(0).Item(2)
        USERVIEW = DTROW(0).Item(3)
        USERDELETE = DTROW(0).Item(4)

        If USEREDIT = False And USERVIEW = False Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        FILLGRID()
    End Sub

    Sub FILLGRID()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" GREYSTOCKADJUSTMENT.GREYSA_no AS SANO, GREYSTOCKADJUSTMENT.GREYSA_date AS DATE, ISNULL(GREYSTOCKADJUSTMENT_DESC.GREYSA_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(GREYSTOCKADJUSTMENT_DESC.GREYSA_LOTNO, '') AS LOTNO, ISNULL(GREYSTOCKADJUSTMENT_DESC.GREYSA_PCS, 0) AS PCS, ISNULL(GREYSTOCKADJUSTMENT_DESC.GREYSA_MTRS, 0) AS MTRS, ISNULL(GREYSTOCKADJUSTMENT_DESC.GREYSA_BARCODE, '') AS BARCODE, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(GREYSTOCKADJUSTMENT.GREYSA_CHALLANNO, 0) AS CHALLANNO, ISNULL(GREYSTOCKADJUSTMENT_DESC.GREYSA_RATE, 0) AS RATE, ISNULL(GREYSTOCKADJUSTMENT_DESC.GREYSA_PER, '') AS PER, ISNULL(GREYSTOCKADJUSTMENT_DESC.GREYSA_AMOUNT, 0) AS AMOUNT", "", "PIECETYPEMASTER RIGHT OUTER JOIN GREYSTOCKADJUSTMENT LEFT OUTER JOIN GREYSTOCKADJUSTMENT_DESC LEFT OUTER JOIN UNITMASTER ON GREYSTOCKADJUSTMENT_DESC.GREYSA_UNITID = UNITMASTER.unit_id ON GREYSTOCKADJUSTMENT.GREYSA_no = GREYSTOCKADJUSTMENT_DESC.GREYSA_NO AND GREYSTOCKADJUSTMENT.GREYSA_yearid = GREYSTOCKADJUSTMENT_DESC.GREYSA_YEARID LEFT OUTER JOIN COLORMASTER ON GREYSTOCKADJUSTMENT_DESC.GREYSA_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GREYSTOCKADJUSTMENT_DESC.GREYSA_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON GREYSTOCKADJUSTMENT_DESC.GREYSA_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GREYSTOCKADJUSTMENT_DESC.GREYSA_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GREYSTOCKADJUSTMENT.GREYSA_TRANSID = TRANSLEDGER.Acc_id ON PIECETYPEMASTER.PIECETYPE_id = GREYSTOCKADJUSTMENT_DESC.GREYSA_PIECETYPEID LEFT OUTER JOIN GODOWNMASTER ON GREYSTOCKADJUSTMENT.GREYSA_GODOWNID = GODOWNMASTER.GODOWN_id ", " AND dbo.GREYSTOCKADJUSTMENT.GREYSA_yearid=" & YearId & " order by dbo.GREYSTOCKADJUSTMENT.GREYSA_no ")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal RECONO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objSTOCK As New GreyStockReco
                objSTOCK.MdiParent = MDIMain
                objSTOCK.EDIT = editval
                objSTOCK.TEMPRECONO = RECONO
                objSTOCK.Show()
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

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SANO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SANO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Grey Stock Adjustment Out Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Grey Stock Adjustment Out Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Grey Stock Adjustment Out Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Grey Stock Adjustment Out Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub
End Class