
Imports BL

Public Class RackCapacityGridReport

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub RackCapacityGridReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub RackCapacityGridReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" RACK_NAME AS RACK, CAST(RACK_REMARKS AS INT) AS CAPACITY, COUNT(BARCODESTOCK.RACK) AS RACKPCS, CAST(RACK_REMARKS AS INT)- COUNT(BARCODESTOCK.RACK) AS BALANCE , ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY ", "", " RACKMASTER LEFT OUTER JOIN BARCODESTOCK ON RACK_NAME = BARCODESTOCK.RACK AND RACK_YEARID = BARCODESTOCK.YEARID LEFT OUTER JOIN CATEGORYMASTER ON RACK_CATEGORYID = CATEGORYMASTER.CATEGORY_ID", " and RACK_YEARID = " & YearId & "  GROUP BY RACK_NAME, RACK_REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') ")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Rack Capacity Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Rack Capacity Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Rack Capacity Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Rack Capacity Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            gridbill.ClearColumnsFilter()
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs) Handles gridbill.DoubleClick
        Try
            Dim OBJSTOCK As New GodownwiseDetails
            OBJSTOCK.MdiParent = MDIMain
            OBJSTOCK.WHERECLAUSE = " AND RACK = '" & gridbill.GetFocusedRowCellValue("RACK") & "'"
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class