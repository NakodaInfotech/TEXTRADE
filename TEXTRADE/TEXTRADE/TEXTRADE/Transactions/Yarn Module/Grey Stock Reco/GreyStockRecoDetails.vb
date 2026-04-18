Imports BL

Public Class GreyStockRecoDetails
    Public EDIT As Boolean
    Public TYPE As String
    Dim TEMPPONO As Integer
    Public Where As String = ""
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub StockAdjustmentDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub StockAdjustmentDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            Dim dt As DataTable = objclsCMST.search("GREYSTOCKADJUSTMENT.GREYSA_no AS SRNO, GREYSTOCKADJUSTMENT.GREYSA_date AS DATE, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(TRANSNAME.Acc_cmpname, '') AS TRANSNAME, ISNULL(GREYSTOCKADJUSTMENT.GREYSA_TOTALPCS, 0) AS TOTALPCS, ISNULL(GREYSTOCKADJUSTMENT.GREYSA_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(GREYSTOCKADJUSTMENT.GREYSA_TOTALINPCS, 0) AS TOTALINPCS, ISNULL(GREYSTOCKADJUSTMENT.GREYSA_TOTALINMTRS, 0) AS TOTALINMTRS, ISNULL(GREYSTOCKADJUSTMENT.GREYSA_TOTALMTRS, 0) - ISNULL(GREYSTOCKADJUSTMENT.GREYSA_TOTALINMTRS, 0) AS DIFF, ISNULL(GREYSTOCKADJUSTMENT.GREYSA_remarks, '') AS REMARKS, ISNULL(GREYSTOCKADJUSTMENT.GREYSA_CHALLANNO, 0) AS CHALLANNO, ISNULL(GREYSTOCKADJUSTMENT.GREYSA_TOTALAMOUNT, 0) AS TOTALAMOUNT, ISNULL(GREYSTOCKADJUSTMENT.GREYSA_TOTALINAMOUNT, 0) AS TOTALINAMOUNT, ISNULL(CONTRACTMASTER.CONTRACT_NAME, '') AS CONTRACTOR, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME ", "", " GREYSTOCKADJUSTMENT LEFT OUTER JOIN LEDGERS ON GREYSTOCKADJUSTMENT.GREYSA_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN CONTRACTMASTER ON GREYSTOCKADJUSTMENT.GREYSA_CONTRACTID = CONTRACTMASTER.CONTRACT_ID LEFT OUTER JOIN GODOWNMASTER ON GREYSTOCKADJUSTMENT.GREYSA_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN LEDGERS AS TRANSNAME ON GREYSTOCKADJUSTMENT.GREYSA_TRANSID = TRANSNAME.Acc_id ", Where & " AND dbo.GREYSTOCKADJUSTMENT.GREYSA_yearid=" & YearId & " order by dbo.GREYSTOCKADJUSTMENT.GREYSA_no ")
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

    Private Sub TOOLSTOCKOUT_Click(sender As Object, e As EventArgs) Handles TOOLSTOCKOUT.Click
        Try
            Dim OBJSTCK As New GreyStockRecoOutDetails
            OBJSTCK.MdiParent = MDIMain
            OBJSTCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLSTOCKIN_Click(sender As Object, e As EventArgs) Handles TOOLSTOCKIN.Click
        Try
            Dim OBJSTOCK As New GreyStockRecoInDetails
            OBJSTOCK.MdiParent = MDIMain
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
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

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Grey Stock Adjustment Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Grey Stock Adjustment Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Grey Stock Adjustment Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Grey Stock Adjustment Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub StockRecoDetails_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "AMAN" Or ClientName = "AARYA" Or ClientName = "VALIANT" Then
                GNAME.Visible = True
                GNAME.VisibleIndex = GCHALLANNO.VisibleIndex + 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class