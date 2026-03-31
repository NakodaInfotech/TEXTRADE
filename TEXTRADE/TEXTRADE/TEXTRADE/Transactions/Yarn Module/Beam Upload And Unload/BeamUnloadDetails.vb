Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class BeamUnloadDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Sub showform(ByVal EDITVAL As Boolean, ByVal GREYNO As Integer)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJBEAMREC As New BeamUnload
            OBJBEAMREC.EDIT = EDITVAL
            OBJBEAMREC.MdiParent = MDIMain
            OBJBEAMREC.TEMPGREYNO = GREYNO
            OBJBEAMREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamUnloadDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.Alt = True And e.KeyCode = Keys.R Then
            Call TOOLREFRESH_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.P Then
            Call TOOLEXCEL_Click(sender, e)
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub


    Private Sub BeamUnloadDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GRN'")
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

    Sub fillgrid()
        'Try
        '    Dim OBJBEAM As New ClsBeamReceivedWarper
        '    OBJBEAM.alParaval.Add(0)
        '    OBJBEAM.alParaval.Add(YearId)
        '    Dim dttable As DataTable = OBJBEAM.selectBEAM()
        '    gridbilldetails.DataSource = dttable
        '    If dttable.Rows.Count > 0 Then
        '        gridbill.FocusedRowHandle = gridbill.RowCount - 1
        '        gridbill.TopRowIndex = gridbill.RowCount - 15
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try

        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("BEAMRECEIVEDWARPER.BEAMREC_NO AS BEAMRECNO, BEAMRECEIVEDWARPER.BEAMREC_DATE AS DATE, GODOWNMASTER.GODOWN_name AS GODOWN, LEDGERS.Acc_cmpname AS NAME, ISNULL(BEAMRECEIVEDWARPER.BEAMREC_BEAMNAME, '') AS BEAMNAME, BEAMRECEIVEDWARPER.BEAMREC_BEAMNO AS BEAMNO, ISNULL(BEAMRECEIVEDWARPER.BEAMREC_TOTALJOBMTRS, 0) AS TOTALJOBMTRS, ISNULL(BEAMRECEIVEDWARPER.BEAMREC_TOTALBEAMMTRS, 0) AS TOTALBEAMMTRS, ISNULL(BEAMRECEIVEDWARPER.BEAMREC_ENDS, 0) AS ENDS, ISNULL(BEAMRECEIVEDWARPER.BEAMREC_SECTION, 0) AS SECTION, ISNULL(BEAMRECEIVEDWARPER.BEAMREC_BEAMWT, 0) AS BEAMWT, ISNULL(BEAMRECEIVEDWARPER.BEAMREC_BREAKAGE, 0) AS BREAKAGE, ISNULL(BEAMRECEIVEDWARPER.BEAMREC_CHALLANNO, '') AS CHALLANNO, ISNULL(BEAMRECEIVEDWARPER.BEAMREC_REMARKS, '') AS REMARKS, ISNULL(BEAMRECEIVEDWARPER.BEAMREC_GAMANO, 0) AS GAMANO, ISNULL(STOREITEMMASTER.STOREITEM_NAME, '') AS ROLLNO ", "", "BEAMRECEIVEDWARPER INNER JOIN GODOWNMASTER ON BEAMRECEIVEDWARPER.BEAMREC_GODOWNID = GODOWNMASTER.GODOWN_id INNER JOIN LEDGERS ON BEAMRECEIVEDWARPER.BEAMREC_LEDGERID = LEDGERS.Acc_id INNER JOIN STOREITEMMASTER ON BEAMRECEIVEDWARPER.BEAMREC_ROLLID = STOREITEMMASTER.STOREITEM_ID ", " AND BEAMRECEIVEDWARPER.BEAMREC_YEARID =" & YearId & " ORDER BY BEAMRECEIVEDWARPER.BEAMREC_NO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("GREYNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs) Handles gridbill.DoubleClick
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("GREYNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDADD.Click
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

    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Beam Upload Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim workbook As String = PATH
            If FileIO.FileSystem.FileExists(PATH) = True Then Interaction.GetObject(workbook).close(False)
            GC.Collect()

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Beam Upload Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Beam Upload Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("GRIDDONE")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class