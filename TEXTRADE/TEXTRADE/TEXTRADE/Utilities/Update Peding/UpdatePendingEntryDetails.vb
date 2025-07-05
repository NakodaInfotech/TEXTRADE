
Imports BL

Public Class UpdatePendingEntryDetails

    Public EDIT As Boolean          'used for editing

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("UPDATENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("UPDATENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(sender As Object, e As EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Challan Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "UpdatePendingEntryDetails"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "UpdatePendingEntryDetails", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub UpdatePendingEntryDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FILLGRID()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJPENDING As New ClsUpdatePendingEntry
            Dim dt As DataTable = OBJPENDING.SELECTPENDINGENTRY(0, CmpId, YearId)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal PENDINGNO As Integer)
        Try
            Dim OBJPENDING As New UpdatePendingEntry
            OBJPENDING.MdiParent = MDIMain
            OBJPENDING.EDIT = editval
            OBJPENDING.TEMPPENDINGNO = PENDINGNO
            OBJPENDING.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class