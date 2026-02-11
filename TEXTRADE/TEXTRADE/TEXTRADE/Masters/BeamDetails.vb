Imports BL

Public Class BeamDetails
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT


    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal id As Integer)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim objBEAM As New BeamMaster
            objBEAM.EDIT = editval
            objBEAM.MdiParent = MDIMain
            objBEAM.TEMPBEAMID = id
            objBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub BeamDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'BEAM MASTER'")
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
        Try
            Dim OBJCMN As New ClsCommonMaster
            'Dim dttable As DataTable = OBJCMN.search(" ISNULL(BEAMMASTER.BEAM_ID, 0) AS BEAMID, ISNULL(BEAMMASTER.BEAM_NAME, '') AS BEAMNAME, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(BEAMMASTER.BEAM_ENDS, 0)  AS ENDS, ISNULL(BEAMMASTER.BEAM_TAPLINE, 0) AS TAPLINE, ISNULL(BEAMMASTER.BEAM_WTMTRS, 0) AS WTMTRS, ISNULL(BEAMMASTER.BEAM_WTTL, 0) AS WTTL, ISNULL(HSNMASTER.HSN_CODE, '')  AS HSNCODE, ISNULL(BEAMMASTER_DESC.BEAM_SRNO, 0) AS GRIDSRNO, ISNULL(GRIDQUALITYMASTER.QUALITY_NAME, '') AS GRIDQUALITY, ISNULL(COLORMASTER.COLOR_NAME, '') AS SHADE,  ISNULL(BEAMMASTER_DESC.BEAM_GRIDENDS, 0) AS GRIDENDS, ISNULL(BEAMMASTER_DESC.BEAM_GRIDWT, 0) AS GRIDWT, ISNULL(BEAMMASTER.BEAM_TOTALENDS, 0) AS TOTALENDS,  ISNULL(BEAMMASTER.BEAM_TOTALWT, 0) AS TOTALWT ", "", " QUALITYMASTER AS GRIDQUALITYMASTER RIGHT OUTER JOIN BEAMMASTER_DESC ON GRIDQUALITYMASTER.QUALITY_ID = BEAMMASTER_DESC.BEAM_GRIDQUALITYID LEFT OUTER JOIN COLORMASTER ON BEAMMASTER_DESC.BEAM_SHADEID = COLORMASTER.COLOR_ID RIGHT OUTER JOIN BEAMMASTER ON BEAMMASTER_DESC.BEAM_ID = BEAMMASTER.BEAM_ID LEFT OUTER JOIN QUALITYMASTER AS QUALITYMASTER ON BEAMMASTER.BEAM_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN HSNMASTER ON BEAMMASTER.BEAM_HSNCODEID = HSNMASTER.HSN_ID ", " and BEAMMASTER.BEAM_ID = ' & TEMPBEAMID & ' and BEAMMASTER.BEAM_yearid = " & YearId)
            Dim dttable As DataTable = OBJCMN.search(" BEAMMASTER.BEAM_NAME AS BEAMNAME, HSNMASTER.HSN_CODE AS HSNCODE, YARNQUALITYMASTER.YARN_NAME AS QUALITY, COLORMASTER.COLOR_name AS SHADE, BEAMMASTER.BEAM_WT AS WTMTRS,  BEAMMASTER.BEAM_ID AS BEAMID, BEAMMASTER.BEAM_YEARID , ISNULL(BEAMMASTER.BEAM_TAPLINE,0)  AS TAPLINE  , ISNULL(BEAMMASTER_DESC.BEAM_GRIDENDS,0)  AS ENDS  ", "", " BEAMMASTER INNER JOIN BEAMMASTER_DESC ON BEAMMASTER.BEAM_ID = BEAMMASTER_DESC.BEAM_ID AND BEAMMASTER.BEAM_YEARID = BEAMMASTER_DESC.BEAM_YEARID INNER JOIN HSNMASTER ON BEAMMASTER.BEAM_HSNCODEID = HSNMASTER.HSN_ID INNER JOIN YARNQUALITYMASTER ON BEAMMASTER_DESC.BEAM_GRIDQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN COLORMASTER ON BEAMMASTER_DESC.BEAM_SHADEID = COLORMASTER.COLOR_id   ", "  and BEAMMASTER.BEAM_yearid = " & YearId)
            gridbilldetails.DataSource = dttable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("BEAMID"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("BEAMID"))
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


    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Beam Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim workbook As String = PATH
            If FileIO.FileSystem.FileExists(PATH) = True Then Interaction.GetObject(workbook).close(False)
            GC.Collect()

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Beam Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Beam Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

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

    'Private Sub BeamDetails_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    '    Try
    '        If MULTIYARN = True Then
    '            GQUALITY.Visible = False
    '            GENDS.Visible = False
    '            GWTMTRS.Visible = False

    '            GTOTALENDS.Visible = True
    '            GTOTALWT.Visible = True
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
End Class

