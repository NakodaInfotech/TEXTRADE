
Imports BL
Imports System.Windows.Forms

Public Class YarnJobOrderDetails
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub JobOrderDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.Alt = True And e.KeyCode = Keys.R Then
                Call TOOLREFRESH_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                Call TOOLEXCEL_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub JobOrderDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'YARN JOBORDER'")
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
            Dim OBJSTORE As New ClsYarnJobOrder
            OBJSTORE.alParaval.Add(0)
            OBJSTORE.alParaval.Add(YearId)
            'Dim DT As DataTable = OBJSTORE.SelectYarnJob
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("CAST(0 AS BIT) AS CHK, YARNJOBORDER.YJOB_NO AS YJOB_NO, ISNULL(YARNJOBORDER_DESC.YJOB_REFNO, '') AS REFNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(YARNJOBORDER.YJOB_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, YARNJOBORDER.YJOB_DATE AS DATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(YARNJOBORDER_DESC.YJOB_REED, 0) AS REED, ISNULL(YARNJOBORDER_DESC.YJOB_REEDSPACE, 0) AS REEDSPACE, ISNULL(YARNJOBORDER_DESC.YJOB_PICKS, 0) AS PICKS, ISNULL(YARNJOBORDER_DESC.YJOB_ENDS, 0) AS TOTALENDS, ISNULL(YARNJOBORDER.YJOB_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(YARNJOBORDER_DESC.YJOB_OUTMTRS, 0) AS OUTMTRS, ISNULL(YARNJOBORDER_DESC.YJOB_DONE, 0) AS DONE, ISNULL(YARNJOBORDER.YJOB_REMARKS, '') AS REMARKS, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO  ", "", " YARNJOBORDER INNER JOIN YARNJOBORDER_DESC ON YARNJOBORDER.YJOB_NO = YARNJOBORDER_DESC.YJOB_NO LEFT OUTER JOIN DESIGNMASTER ON YARNJOBORDER_DESC.YJOB_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS ON YARNJOBORDER.YJOB_YEARID = LEDGERS.Acc_yearid AND YARNJOBORDER.YJOB_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN ITEMMASTER ON YARNJOBORDER_DESC.YJOB_YEARID = ITEMMASTER.item_yearid AND YARNJOBORDER_DESC.YJOB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON YARNJOBORDER_DESC.YJOB_YEARID = COLORMASTER.COLOR_yearid AND YARNJOBORDER_DESC.YJOB_SHADEID = COLORMASTER.COLOR_id   ", " AND  (YARNJOBORDER.YJOB_YEARID  = '" & YearId & "') ORDER BY YJOB_NO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal INWARDNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJSTORES As New YarnJobOrder
                OBJSTORES.MdiParent = MDIMain
                OBJSTORES.EDIT = editval
                OBJSTORES.TEMPJONO = INWARDNO
                OBJSTORES.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("YJOB_NO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("YJOB_NO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs)
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Stores Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Yarn Job Order Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Yarn Job Order Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Yarn Job Order Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
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

    Private Sub CMDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDADD.Click
        Try
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class