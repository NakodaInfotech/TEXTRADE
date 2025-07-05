
Imports BL

Public Class StoresReOrderLevelReport

    Private Sub StoresReOrderLevelReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StoresReOrderLevelReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" T.ITEMNAME, T.QTY, ROUND(ISNULL(SUM(QTY)-SUM(ISSQTY),0),2) AS STOCKQTY ", "", " (SELECT STOREITEMMASTER.STOREITEM_NAME AS ITEMNAME, STORESREORDERLEVEL.REORDER_QTY AS QTY, STORESREORDER_YEARID AS YEARID FROM STORESREORDERLEVEL INNER JOIN STOREITEMMASTER ON STORESREORDERLEVEL.REORDER_ITEMID = STOREITEMMASTER.item_id) AS T LEFT OUTER JOIN STORESSTOCKREGISTER ON T.ITEMNAME = STORESSTOCKREGISTER.ITEMNAME AND T.YEARID= STORESSTOCKREGISTER.YEARID WHERE T.YEARID = " & YearId & " GROUP BY T.ITEMNAME")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXPORT_Click(sender As Object, e As EventArgs) Handles CMDEXPORT.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Stores ReOrder Report.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Stores ReOrder Report"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Stores ReOrder Report", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Stores ReOrder Report Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

End Class