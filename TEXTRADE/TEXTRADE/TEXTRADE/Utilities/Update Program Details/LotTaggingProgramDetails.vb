Imports BL

Public Class LotTaggingProgramDetails
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid()
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

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub LotTaggingProgramDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Private Sub EXCELEXPORT_Click(sender As Object, e As EventArgs) Handles EXCELEXPORT.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Lot Tagging Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Lot Tagging Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Program Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Lot Tagging Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridBILL_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LotTaggingProgramDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GRN'")
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
            Dim WHERECLAUSE As String = ""
            Dim SELECTCLAUSE As String = ""
            Dim dt As DataTable = objclsCMST.search(" ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(LOTTAGGING_ADJDESC.LOTTAG_PMTRS, 0) AS MTRS,  ISNULL(LOTTAGGING_ADJDESC.LOTTAG_PBALMTRS, 0) AS BALMTRS, ISNULL(LOTTAGGING_ADJDESC.LOTTAG_PADJMTRS, 0) AS ADJMTRS, ISNULL(LOTTAGGING_ADJDESC.LOTTAG_PSRNO, 0) AS PSRNO,  ISNULL(LOTTAGGING.LOTTAG_NO, 0) AS SRNO, ISNULL(LOTTAGGING_ADJDESC.LOTTAG_FROMNO, 0) AS PROGRAMNO" & SELECTCLAUSE, "", "   LOTTAGGING INNER JOIN LOTTAGGING_ADJDESC ON LOTTAGGING.LOTTAG_NO = LOTTAGGING_ADJDESC.LOTTAG_NO AND LOTTAGGING.LOTTAG_YEARID = LOTTAGGING_ADJDESC.LOTTAG_YEARID LEFT OUTER JOIN COLORMASTER ON LOTTAGGING_ADJDESC.LOTTAG_PCOLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON LOTTAGGING_ADJDESC.LOTTAG_PDESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON LOTTAGGING_ADJDESC.LOTTAG_PITEMID = ITEMMASTER.item_id", WHERECLAUSE & " AND LOTTAGGING.LOTTAG_YEARID = " & YearId & " ORDER BY LOTTAGGING.LOTTAG_NO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal PROGRAMNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJPROGRAM As New LotTagging
                OBJPROGRAM.MdiParent = MDIMain
                OBJPROGRAM.EDIT = editval
                OBJPROGRAM.LOTTAGNO = PROGRAMNO
                OBJPROGRAM.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class