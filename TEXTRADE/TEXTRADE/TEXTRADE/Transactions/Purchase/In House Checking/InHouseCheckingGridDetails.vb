Imports BL
Public Class InHouseCheckingGridDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal CHECKINGNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJCHECK As New InHouseChecking
                OBJCHECK.MdiParent = MDIMain
                OBJCHECK.EDIT = editval
                OBJCHECK.TEMPCHECKINGNO = CHECKINGNO
                OBJCHECK.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InHouseCheckingGridDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("CHECKINGNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InHouseCheckingGridDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GRN CHECKING'")
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
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("INHOUSECHECKING.CHECK_NO AS CHECKINGNO, INHOUSECHECKING.CHECK_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, INHOUSECHECKING_DESC.CHECK_GREYMTRS AS GREYMTRS, INHOUSECHECKING_DESC.CHECK_RECDMTRS AS RECDMTRS, INHOUSECHECKING_DESC.CHECK_CHECKEDMTRS AS CHECKEDMTRS, PIECETYPEMASTER.PIECETYPE_name AS PIECETYPE, INHOUSECHECKING_DESC.CHECK_DIFF AS DIFF,INHOUSECHECKING_DESC.CHECK_RECDWT AS RECDWT, INHOUSECHECKING_DESC.CHECK_WT AS WT, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, INHOUSECHECKING_DESC.CHECK_BARCODE AS BARCODE, ISNULL(UNITMASTER.unit_abbr, 'Pcs') AS UNIT, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(WEAVERLEDGERS.Acc_cmpname, '') AS WEAVERNAME, ISNULL(INHOUSECHECKING.CHECK_LOTNO, '') AS LOTNO", "", "  LEDGERS AS WEAVERLEDGERS RIGHT OUTER JOIN INHOUSECHECKING INNER JOIN INHOUSECHECKING_DESC ON INHOUSECHECKING.CHECK_NO = INHOUSECHECKING_DESC.CHECK_NO AND INHOUSECHECKING.CHECK_YEARID = INHOUSECHECKING_DESC.CHECK_YEARID INNER JOIN LEDGERS ON INHOUSECHECKING.CHECK_LEDGERID = LEDGERS.Acc_id INNER JOIN PIECETYPEMASTER ON INHOUSECHECKING_DESC.CHECK_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id INNER JOIN ITEMMASTER ON INHOUSECHECKING_DESC.CHECK_ITEMID = ITEMMASTER.item_id ON WEAVERLEDGERS.Acc_id = INHOUSECHECKING.CHECK_WEAVERID LEFT OUTER JOIN  COLORMASTER ON INHOUSECHECKING_DESC.CHECK_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN SHELFMASTER AS SHELFMASTER_1 ON INHOUSECHECKING_DESC.CHECK_SHELFID = SHELFMASTER_1.SHELF_ID LEFT OUTER JOIN DESIGNMASTER ON INHOUSECHECKING_DESC.CHECK_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON INHOUSECHECKING_DESC.CHECK_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN UNITMASTER ON INHOUSECHECKING_DESC.CHECK_UNITID = UNITMASTER.unit_id  ", " AND dbo.INHOUSECHECKING.CHECK_yearid=" & YearId & " order by dbo.INHOUSECHECKING.CHECK_no")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
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

    Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("CHECKINGNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InHouseCheckingGridDetails_Click(sender As Object, e As EventArgs) Handles Me.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Checking Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Checking Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Checking Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Checking Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub InHouseCheckingGridDetails_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "YASHVI" Then
                GRECDWT.Visible = False
                GWT.Visible = False
                GQUALITY.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class