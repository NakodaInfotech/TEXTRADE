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
            Dim dt As DataTable = objclsCMST.search(" ISNULL(YARNSTOCKADJUSTMENT.YSA_NO, 0) AS SANO, YARNSTOCKADJUSTMENT.YSA_DATE AS DATE, GODOWNMASTER.GODOWN_name AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS YARNITEM,  ISNULL(MILLMASTER.MILL_NAME, '') AS MILL, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_PARTYLOTNO, '') AS PARTYLOTNO,  ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_PARTYCOLOR, '') AS PARTYCOLOR, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_LOTNO, '') AS LOTNO, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_DESC, '') AS [DESC], ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_BAGS, 0) AS BAGS, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_WT, 0) AS WT, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_CONES, '') AS CONES, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_LRNO, '') AS LRNO, ISNULL(RACKMASTER.RACK_NAME, '') AS RACK, ISNULL(YARNSTOCKADJUSTMENT_INDESC.SA_RATE, 0) AS RATE, ISNULL(YARNSTOCKADJUSTMENT_INDESC.SA_PER, '') AS PER, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_AMOUNT, 0) AS AMOUNT, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_BARCODE, '') AS BARCODE, ISNULL(YARNSTOCKADJUSTMENT.YSA_REMARKS, '') AS REMARKS , ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_DONE, '') AS DONE ,ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_OUTBAGS, '') AS OUTBAGS ,ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_OUTWT, '') AS OUTWT ", "", " YARNSTOCKADJUSTMENT INNER JOIN YARNSTOCKADJUSTMENT_INDESC ON YARNSTOCKADJUSTMENT.YSA_NO = YARNSTOCKADJUSTMENT_INDESC.YSA_NO AND  YARNSTOCKADJUSTMENT.YSA_yearid = YARNSTOCKADJUSTMENT_INDESC.YSA_YEARID LEFT OUTER JOIN RACKMASTER ON YARNSTOCKADJUSTMENT_INDESC.YSA_RACKID = RACKMASTER.RACK_ID LEFT OUTER JOIN COLORMASTER ON YARNSTOCKADJUSTMENT_INDESC.YSA_SHADEID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON YARNSTOCKADJUSTMENT_INDESC.YSA_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN MILLMASTER ON YARNSTOCKADJUSTMENT_INDESC.YSA_MILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN YARNQUALITYMASTER ON YARNSTOCKADJUSTMENT_INDESC.YSA_ITEMID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON YARNSTOCKADJUSTMENT.YSA_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON YARNSTOCKADJUSTMENT.YSA_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON YARNSTOCKADJUSTMENT.YSA_GODOWNID = GODOWNMASTER.GODOWN_id ", " AND dbo.YARNSTOCKADJUSTMENT.YSA_yearid=" & YearId & " order by DBO.YARNSTOCKADJUSTMENT.YSA_NO")
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
