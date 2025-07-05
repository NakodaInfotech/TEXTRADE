
Imports BL
Imports DevExpress.XtraGrid.Views.Grid
Public Class SampleOrderGridDetails
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SampleOrderGridDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
    Sub showform(ByVal editval As Boolean, ByVal SONO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New SampleOrder
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPSONO = SONO
                objPO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SampleOrderGridDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SAMPLE MODULE'")
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
            Dim OBJSMP As New ClsSampleOrder
            Dim DT As New DataTable
            If CHKPENDING.CheckState = CheckState.Unchecked Then
                DT = OBJSMP.selectSMP(0, CmpId, 0, YearId)
            Else
                Dim OBJCMN As New ClsCommon
                DT = OBJCMN.SEARCH("SAMPLEORDER.SO_NO AS SONO, SAMPLEORDER.SO_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, SAMPLEORDER.SO_MODE AS MODE,  SAMPLEORDER.SO_REMARKS AS REMARKS, SAMPLEORDER_DESC.SO_GRIDSRNO AS GRIDSRNO, ITEMMASTER.item_name AS ITEMNAME,  ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITYNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '')  AS COLOR, SAMPLEORDER_DESC.SO_NARRATION AS NARRATION, ISNULL(SAMPLEORDER.SO_TOTALNOOFBOOKLET, 0) AS TOTALNOOFBOOKLET,  ISNULL(SAMPLEORDER_DESC.SO_NOOFBOOKLET, 0) AS NOOFBOOKLET, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT,  ISNULL(SAMPLEORDER_DESC.SO_OUTQTY, 0) AS OUTQTY, ISNULL(SAMPLEORDER_DESC.SO_CLOSED, 0) AS CLOSED,(ISNULL(SAMPLEORDER_DESC.SO_NOOFBOOKLET, 0)-ISNULL(SAMPLEORDER_DESC.SO_OUTQTY, 0)) AS BALQTY, ISNULL(SAMPLETYPEMASTER.SAMPLETYPE_NAME,'') AS SAMPLETYPE, ISNULL(SAMPLEORDER.SO_SELF,0) AS SELF ", "", " SAMPLEORDER INNER JOIN SAMPLEORDER_DESC ON SAMPLEORDER.SO_NO = SAMPLEORDER_DESC.SO_NO AND  SAMPLEORDER.SO_YEARID = SAMPLEORDER_DESC.SO_YEARID INNER JOIN LEDGERS ON SAMPLEORDER.SO_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON SAMPLEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON SAMPLEORDER.SO_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON SAMPLEORDER_DESC.SO_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON SAMPLEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON SAMPLEORDER_DESC.SO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN SAMPLETYPEMASTER ON SAMPLEORDER_DESC.SO_SAMPLETYPEID = SAMPLETYPEMASTER.SAMPLETYPE_id  ", " AND (ISNULL(SAMPLEORDER_DESC.SO_NOOFBOOKLET, 0)-ISNULL(SAMPLEORDER_DESC.SO_OUTQTY, 0)) > 0 AND ISNULL(SAMPLEORDER_DESC.SO_CLOSED, 0) = 'FALSE' AND SAMPLEORDER.SO_YEARID = " & YearId & " ORDER BY SONO")
            End If
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
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

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SONO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Sample Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Sample Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sample Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Sample Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            FILLGRID()
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
                ElseIf Val(View.GetRowCellDisplayText(e.RowHandle, View.Columns("OUTQTY"))) > 0 Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class