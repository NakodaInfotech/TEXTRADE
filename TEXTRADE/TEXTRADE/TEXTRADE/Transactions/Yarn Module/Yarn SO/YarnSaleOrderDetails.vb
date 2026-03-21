
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class YarnSaleOrderDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub YarnSaleOrderDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub YarnSaleOrderDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(YARNSALEORDER.YSO_NO, 0) AS SONO, YARNSALEORDER.YSO_DATE AS SODATE, YARNSALEORDER.YSO_DUEDATE AS DUEDATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(YARNSALEORDER.YSO_CRDAYS, 0) AS CRDAYS, ISNULL(YARNSALEORDER.YSO_DELDAYS, 0) AS DELDAYS, ISNULL(YARNSALEORDER.YSO_REFNO, '') AS REFNO, ISNULL(YARNSALEORDER.YSO_DISCOUNT, 0) AS DISCOUNT, ISNULL(YARNSALEORDER.YSO_REMARKS, '') AS REMARKS, ISNULL(YARNSALEORDER.YSO_LBLTOTALAMT, 0) AS LBLTOTALPCS, ISNULL(YARNSALEORDER.YSO_INWORDS, '') AS INWORDS, ISNULL(BROKERLEDGERS.Acc_cmpname, '') AS BROKER, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(YARNSALEORDER_DESC.YSO_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(YARNSALEORDER_DESC.YSO_DESC, '') AS [DESC], ISNULL(UNITMASTER.unit_name, '') AS UNIT, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(YARNSALEORDER_DESC.YSO_BAGS, 0) AS BAGS, ISNULL(YARNSALEORDER_DESC.YSO_WT, 0) AS WT, YARNQUALITYMASTER.YARN_NAME AS YARNQUALITY, ISNULL(LEDGERS.Acc_mobile, '') AS MOBILENO, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(YARNSALEORDER_DESC.YSO_RATE, 0) AS RATE, ISNULL(YARNSALEORDER_DESC.YSO_AMT, 0) AS AMT, ISNULL(YARNSALEORDER_DESC.YSO_RECDBAGS, 0) AS RECDBAGS, ISNULL(YARNSALEORDER_DESC.YSO_RECDWT, 0) AS RECDWT, ISNULL(YARNSALEORDER_DESC.YSO_DONE, 0) AS DONE, ISNULL(YARNSALEORDER_DESC.YSO_CLOSED, 0) AS CLOSED, ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS PACKING, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(MILLMASTER.MILL_NAME, '') AS MILLNAME, ISNULL(YARNSALEORDER.YSO_CD, 0) AS CD, ISNULL(YARNSALEORDER.YSO_INT, 0) AS INT, ISNULL(YARNSALEORDER.YSO_NOTE, '') AS NOTE, ISNULL(YARNSALEORDER_DESC.YSO_BAGS-YARNSALEORDER_DESC.YSO_RECDBAGS, 0) AS BALBAGS", "", " YARNSALEORDER INNER JOIN YARNSALEORDER_DESC ON YARNSALEORDER.YSO_NO = YARNSALEORDER_DESC.YSO_NO AND YARNSALEORDER.YSO_YEARID = YARNSALEORDER_DESC.YSO_YEARID LEFT OUTER JOIN MILLMASTER ON YARNSALEORDER_DESC.YSO_MILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN CITYMASTER ON YARNSALEORDER.YSO_CITYID = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON YARNSALEORDER.YSO_PACKINGID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON YARNSALEORDER_DESC.YSO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON YARNSALEORDER_DESC.YSO_SHADEID = COLORMASTER.COLOR_id LEFT OUTER JOIN YARNQUALITYMASTER ON YARNSALEORDER_DESC.YSO_YARNQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN LEDGERS AS BROKERLEDGERS ON YARNSALEORDER.YSO_BROKERID = BROKERLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON YARNSALEORDER.YSO_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON YARNSALEORDER.YSO_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN UNITMASTER ON YARNSALEORDER_DESC.YSO_UNITID = UNITMASTER.unit_id ", " AND YARNSALEORDER.YSO_YEARID = " & YearId & " ORDER BY dbo.YARNSALEORDER.YSO_NO, dbo.YARNSALEORDER_DESC.YSO_GRIDSRNO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal SONO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New YarnSaleOrder
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPSONO = SONO
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
            showform(True, gridbill.GetFocusedRowCellValue("SONO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SONO"))
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
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("DONE")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightGreen
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Sale Order Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Sale Order Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Purchase Order Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Yarn SO Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

End Class