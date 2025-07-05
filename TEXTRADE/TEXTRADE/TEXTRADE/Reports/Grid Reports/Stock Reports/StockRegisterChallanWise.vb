
Imports BL

Public Class StockRegisterChallanWise

    Public WHERECLAUSE As String = ""
    Public FROMDATE As Date = AccFrom
    Public TODATE As Date = AccTo

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub fillgrid()
        Dim OBJCMN As New ClsCommon
        'Dim DT As DataTable = OBJCMN.search(" NAME, CHALLANNO, SUM(PCS) AS PCS, SUM(MTRS) AS MTRS, SUM(ISSPCS) AS ISSPCS, SUM(ISSMTRS) AS ISSMTRS,  ROUND(SUM(PCS)-SUM(ISSPCS),0) AS BALPCS, ROUND(SUM(MTRS)-SUM(ISSMTRS),2) AS BALMTRS ", "", " STOCKREGISTER", " AND YEARID = " & YearId & " AND CHALLANNO <> '' GROUP BY NAME, CHALLANNO HAVING ROUND(SUM(PCS)-SUM(ISSPCS),2) > 0")
        Dim DT As DataTable = OBJCMN.search(" STOCKREGISTER.NAME, STOCKREGISTER.CHALLANNO, SUM(STOCKREGISTER.PCS) AS PCS, SUM(STOCKREGISTER.MTRS) AS MTRS, SUM(STOCKREGISTER.ISSPCS) AS ISSPCS, SUM(STOCKREGISTER.ISSMTRS) AS ISSMTRS,  ROUND(SUM(STOCKREGISTER.PCS)-SUM(STOCKREGISTER.ISSPCS),0) AS BALPCS, ROUND(SUM(STOCKREGISTER.MTRS)-SUM(STOCKREGISTER.ISSMTRS),2) AS BALMTRS, SREG.DATE AS DATE  ", "", " STOCKREGISTER CROSS APPLY (SELECT TOP 1 * FROM STOCKREGISTER SREG WHERE STOCKREGISTER.NAME = SREG.NAME AND STOCKREGISTER.CHALLANNO = SREG.CHALLANNO AND STOCKREGISTER.YEARID = SREG.YEARID AND SREG.PCS > 0 ORDER BY DATE ) AS SREG ", " AND STOCKREGISTER.YEARID = " & YearId & " AND STOCKREGISTER.CHALLANNO <> '' GROUP BY STOCKREGISTER.NAME, STOCKREGISTER.CHALLANNO, SREG.DATE HAVING ROUND(SUM(STOCKREGISTER.MTRS)-SUM(STOCKREGISTER.ISSMTRS),2) > 0")
        griddetails.DataSource = DT
    End Sub

    Private Sub StockRegisterChallanWise_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StockRegisterChallanWise_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(sender As Object, e As EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Stock Register.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Stock Register"
            gridregister.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Stock Register", gridregister.VisibleColumns.Count + gridregister.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Stock Register Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class