Imports BL

Public Class YarnStockInDetails



    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

        Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
            Me.Close()
        End Sub

    Private Sub YarnStockInDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub YarnStockInDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        FILLGRID()
    End Sub

    Sub FILLGRID()
            Try
                Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" STOCKADJUSTMENT.SA_no AS SANO, STOCKADJUSTMENT.SA_date AS DATE, ISNULL(STOCKADJUSTMENT_INDESC.SA_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(ITEMMASTER.ITEM_NAME, '') AS ITEMNAME,  ISNULL(STOCKADJUSTMENT_INDESC.SA_LOTNO, '') AS LOTNO, ISNULL(STOCKADJUSTMENT_INDESC.SA_BARCODE, '') AS BARCODE, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(STOCKADJUSTMENT_INDESC.SA_REELNO, '') AS REELNO, ISNULL(STOCKADJUSTMENT_INDESC.SA_GSM, 0) AS GSM, ISNULL(STOCKADJUSTMENT_INDESC.SA_GSMDETAILS, '') AS GSMDETAILS, ISNULL(STOCKADJUSTMENT_INDESC.SA_SIZE, 0) AS SIZE, ISNULL(STOCKADJUSTMENT_INDESC.SA_QTY, 0) AS QTY ", "", "STOCKADJUSTMENT INNER JOIN STOCKADJUSTMENT_INDESC ON STOCKADJUSTMENT.SA_no = STOCKADJUSTMENT_INDESC.SA_NO AND STOCKADJUSTMENT.SA_yearid = STOCKADJUSTMENT_INDESC.SA_YEARID LEFT OUTER JOIN UNITMASTER ON STOCKADJUSTMENT_INDESC.SA_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN ITEMMASTER AS ITEMMASTER ON STOCKADJUSTMENT_INDESC.SA_ITEMID = ITEMMASTER.item_id ", " AND dbo.YARNSTOCKADJUSTMENT.YSA_yearid=" & YearId & " order by DBO.YARNSTOCKADJUSTMENT.YSA_NO")
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
                Dim objSTOCK As New YarnStockReco
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

            Dim PATH As String = Application.StartupPath & "\Yarn Adjustment In Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True
            opti.SheetName = "Yarn Adjustment In Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Yarn Adjustment In Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Yarn Adjustment In Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
        End Sub

    End Class
