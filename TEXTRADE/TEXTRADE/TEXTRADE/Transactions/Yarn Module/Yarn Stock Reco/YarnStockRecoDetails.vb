Imports BL
Imports DevExpress.XtraGrid.Views.Grid


Public Class YarnStockRecoDetails

    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public TEMPRECONO As Integer

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub YarnStockRecoDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub YarnStockRecoDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
            dt = OBJCMN.SEARCH(" YARNSTOCKADJUSTMENT.YSA_NO AS RECONO, YARNSTOCKADJUSTMENT.YSA_DATE AS DATE, GODOWNMASTER.GODOWN_name AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME,  ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(YARNSTOCKADJUSTMENT.YSA_TOTALINBAGS, '') AS TOTALINBAGS, ISNULL(YARNSTOCKADJUSTMENT.YSA_TOTALINWT, '') AS TOTALINWT,   ISNULL(YARNSTOCKADJUSTMENT.YSA_TOTALINCONES, '') AS TOTALINCONES, ISNULL(YARNSTOCKADJUSTMENT.YSA_TOTALOUTBAGS, '') AS TOTALOUTBAGS, ISNULL(YARNSTOCKADJUSTMENT.YSA_TOTALOUTWT, '')  AS TOTALOUTWT, ISNULL(YARNSTOCKADJUSTMENT.YSA_TOTALOUTCONES, '') AS TOTALOUTCONES, ISNULL(YARNSTOCKADJUSTMENT.YSA_REMARKS, '') AS REMARKS ", "", " YARNSTOCKADJUSTMENT LEFT OUTER JOIN LEDGERS ON YARNSTOCKADJUSTMENT.YSA_yearid = LEDGERS.Acc_yearid AND YARNSTOCKADJUSTMENT.YSA_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON YARNSTOCKADJUSTMENT.YSA_yearid = TRANSLEDGERS.Acc_yearid AND YARNSTOCKADJUSTMENT.YSA_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON YARNSTOCKADJUSTMENT.YSA_yearid = GODOWNMASTER.GODOWN_yearid AND YARNSTOCKADJUSTMENT.YSA_GODOWNID = GODOWNMASTER.GODOWN_id ", "  AND (YARNSTOCKADJUSTMENT.YSA_yearid= " & YearId & ") ORDER BY dbo.YARNSTOCKADJUSTMENT.YSA_NO")
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
            Dim OBJSTCK As New YarnStockOutDetails
            OBJSTCK.MdiParent = MDIMain
            OBJSTCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLSTOCKIN_Click_1(sender As Object, e As EventArgs) Handles TOOLSTOCKIN.Click
        Try
            Dim OBJSTOCK As New YarnStockInDetails
            OBJSTOCK.MdiParent = MDIMain
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        FILLGRID()
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Yarn Stock Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Yarn Stock Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Yarn Stock Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Yarn Stock Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
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


End Class