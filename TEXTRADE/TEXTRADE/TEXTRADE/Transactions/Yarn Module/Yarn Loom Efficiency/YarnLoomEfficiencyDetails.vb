Imports BL
Imports System.Windows.Forms
Public Class YarnLoomEfficiencyDetails
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
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'YARN LOOMEFFICIENCY'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            FILLGRID()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJSTORE As New ClsYarnLoomEfficiency
            OBJSTORE.alParaval.Add(0)
            OBJSTORE.alParaval.Add(YearId)
            'Dim DT As DataTable = OBJSTORE.SelectYarnJob
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, ISNULL(YARNLOOMEFFICIENCY.YLE_no, 0) AS YLENO, YARNLOOMEFFICIENCY.YLE_date AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(CONTRACTMASTER.CONTRACT_NAME, '') AS ROUNDER, ISNULL(YARNLOOMEFFICIENCY.YLE_TOTALRECMTRS, 0) AS TOTALRECMTRS, ISNULL(YARNLOOMEFFICIENCY.YLE_TOTALWEFT, 0) AS TOTALWEFT, ISNULL(YARNLOOMEFFICIENCY.YLE_remarks, '') AS REMARKS, ISNULL(LOOMMASTER_DESC.LOOM_NO, '') AS LOOMNO, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS YARNQUALITY, ISNULL(YARNLOOMEFFICIENCY_DESC.YLE_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(YARNLOOMEFFICIENCY_DESC.YLE_BEAMNO, 0) AS BEAMNO, ISNULL(YARNLOOMEFFICIENCY_DESC.YLE_RPM, 0) AS RPM, ISNULL(YARNLOOMEFFICIENCY_DESC.YLE_PICKS, 0) AS PICKS, ISNULL(YARNLOOMEFFICIENCY_DESC.YLE_RECMTRS, 0) AS RECMTRS, ISNULL(YARNLOOMEFFICIENCY_DESC.YLE_WEFT, 0) AS WEFT, ISNULL(YARNLOOMEFFICIENCY_DESC.YLE_WARP, 0) AS WARP, ISNULL(YARNLOOMEFFICIENCY_DESC.YLE_EFFPER, 0) AS EFFPER, ISNULL(YARNLOOMEFFICIENCY_DESC.YLE_AVGPICK, 0) AS AVGPICK, ISNULL(YARNLOOMEFFICIENCY_DESC.YLE_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(YARNLOOMEFFICIENCY_DESC.YLE_DONE, 0) AS DONE  ", "", " YARNLOOMEFFICIENCY INNER JOIN YARNLOOMEFFICIENCY_DESC ON YARNLOOMEFFICIENCY.YLE_no = YARNLOOMEFFICIENCY_DESC.YLE_no AND YARNLOOMEFFICIENCY.YLE_yearid = YARNLOOMEFFICIENCY_DESC.YLE_yearid LEFT OUTER JOIN YARNQUALITYMASTER ON YARNLOOMEFFICIENCY_DESC.YLE_YARNQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN LOOMMASTER_DESC ON YARNLOOMEFFICIENCY_DESC.YLE_LOOMNO = LOOMMASTER_DESC.LOOM_NO LEFT OUTER JOIN CONTRACTMASTER ON YARNLOOMEFFICIENCY.YLE_ROUNDERID = CONTRACTMASTER.CONTRACT_ID LEFT OUTER JOIN LEDGERS ON YARNLOOMEFFICIENCY.YLE_ledgerid = LEDGERS.Acc_id  ", " AND  (YARNLOOMEFFICIENCY.YLE_YEARID  = '" & YearId & "') ORDER BY YLENO")
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
                Dim OBJSTORES As New YarnLoomEfficiency
                OBJSTORES.MdiParent = MDIMain
                OBJSTORES.EDIT = editval
                OBJSTORES.TEMPYLENO = INWARDNO
                OBJSTORES.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("YLENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("YLENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs)
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Stores Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Yarn Loom Efficiency Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Yarn Loom Efficiency Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Loom Efficiency Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
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

    Private Sub CMDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDADD.Click
        Try
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class