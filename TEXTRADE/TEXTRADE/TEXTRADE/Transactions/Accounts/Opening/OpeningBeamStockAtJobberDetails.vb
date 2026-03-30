Imports BL
Imports System.Windows.Forms
Public Class OpeningBeamStockAtJobberDetails
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
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'BEAM ISSUE'")
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
            Dim OBJSTORE As New ClsOpeningBeamStockAtJobber
            OBJSTORE.alParaval.Add(0)
            OBJSTORE.alParaval.Add(YearId)
            'Dim DT As DataTable = OBJSTORE.SelectYarnJob
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, ISNULL(OPENINGBEAMSTOCKATJOBBER.OPBEAM_NO, 0) AS BEAMISSUENO, OPENINGBEAMSTOCKATJOBBER.OPBEAM_DATE AS ISSUEDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(OPENINGBEAMSTOCKATJOBBER.OPBEAM_VEHICALNO, '') AS VEHICALNO, ISNULL(OPENINGBEAMSTOCKATJOBBER.OPBEAM_EWBNO, '') AS EWBNO, ISNULL(OPENINGBEAMSTOCKATJOBBER.OPBEAM_REMARKS, '') AS REMARKS, ISNULL(OPENINGBEAMSTOCKATJOBBER.OPBEAM_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(OPENINGBEAMSTOCKATJOBBER.OPBEAM_TOTALWT, 0) AS TOTALWT,  ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_GRIDSRNO, 0) AS SRNO, ISNULL(BEAMMASTER.BEAM_NAME, '') AS BEAMNAME, ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_BEAMNO, '0')  AS BEAMNO, ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_ENDS, 0) AS ENDS, ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_TAPLINE, 0) AS TAPLINE,  ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_MTRS, 0) AS MTRS, ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_WT, 0) AS WT, ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_WTCUT, 0)  AS CUTWT, ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_NARR, '') AS NARR, ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_DONE, 0) AS DONE, ISNULL(SIZERLEDGER.Acc_cmpname, '') AS SIZER,  ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_OUTMTRS, 0) AS OUTMTRS, ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_GAMANO, 0) AS GAMANO,  ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_SECTION, 0) AS SECTION, ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_BEAMWT, 0) AS BEAMWT,  ISNULL(OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_BREAKAGE, 0) AS BREAKAGE, ISNULL(STOREITEMMASTER.STOREITEM_NAME, '') AS ROLLNO   ", "", " OPENINGBEAMSTOCKATJOBBER INNER JOIN OPENINGBEAMSTOCKATJOBBER_DESC ON OPENINGBEAMSTOCKATJOBBER.OPBEAM_NO = OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_NO AND  OPENINGBEAMSTOCKATJOBBER.OPBEAM_YEARID = OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_YEARID LEFT OUTER JOIN STOREITEMMASTER ON OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_ROLLNO = STOREITEMMASTER.STOREITEM_ID LEFT OUTER JOIN LEDGERS AS SIZERLEDGER ON OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_SIZERID = SIZERLEDGER.Acc_id LEFT OUTER JOIN BEAMMASTER ON OPENINGBEAMSTOCKATJOBBER_DESC.OPBEAM_BEAMID = BEAMMASTER.BEAM_ID LEFT OUTER JOIN LEDGERS ON OPENINGBEAMSTOCKATJOBBER.OPBEAM_LEDGERID = LEDGERS.Acc_id   ", " AND  (OPENINGBEAMSTOCKATJOBBER.OPBEAM_YEARID  = '" & YearId & "') ORDER BY BEAMISSUENO")
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
                Dim OBJSTORES As New OpeningBeamStockAtJobber
                OBJSTORES.MdiParent = MDIMain
                OBJSTORES.EDIT = editval
                OBJSTORES.TEMPBEAMISSUENO = INWARDNO
                OBJSTORES.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("BEAMISSUENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("BEAMISSUENO"))
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

            Dim PATH As String = Application.StartupPath & "\Opening Beam Stock At Jobber Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Opening Beam Stock At Jobber Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Opening Beam Stock At Jobber Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Opening Beam Stock At Jobber Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
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