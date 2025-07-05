
Imports BL

Public Class IncentiveReport


    Private Sub IncentiveReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" SUM(TAXABLEAMT) AS TAXABLEAMT, SUM(CNAMT) AS CNAMT, SUM(DNAMT) AS DNAMT, SUM(NETTAMT) AS NETTAMT, NAME, ROUND(SUM(INCENTIVES_DESC.INC_AMOUNT),2) AS ELIGIBILITYAMT, SUM(INCENTIVES_DESC.INC_INCENTIVE) AS INCENTIVEPER, (CASE WHEN SUM(NETTAMT) >= (ROUND(SUM(INCENTIVES_DESC.INC_AMOUNT),2)) THEN ROUND((SUM(NETTAMT)*SUM(INC_INCENTIVE))/100,2) ELSE 0 END) AS INCENTIVEAMT ", "", " (SELECT ROUND(SUM(TAXABLEAMT),2) AS TAXABLEAMT, ROUND(SUM(CNAMT),2) AS CNAMT, ROUND(SUM(DNAMT),2) AS DNAMT, ROUND(SUM(TAXABLEAMT),2)-ROUND(SUM(CNAMT),2)+ROUND(SUM(DNAMT),2) AS NETTAMT, NAME, LEDGERID, YEARID FROM (SELECT ISNULL(SUM(TAXABLEAMT),0) AS TAXABLEAMT, 0 AS CNAMT, 0 AS DNAMT, NAME, LEDGERID, YEARID FROM HSNSUMMARY INNER JOIN INCENTIVES ON HSNSUMMARY.LEDGERID = INC_LEDGERID WHERE ENTRYTYPE = 'INVOICE' AND YEARID = " & YearId & " GROUP BY NAME, LEDGERID, YEARID UNION ALL SELECT 0 AS TAXABLEAMT, ISNULL(SUM(TAXABLEAMT),0) AS CNAMT, 0 AS DNAMT, NAME, LEDGERID, YEARID FROM HSNSUMMARY INNER JOIN INCENTIVES ON HSNSUMMARY.LEDGERID = INC_LEDGERID WHERE (ENTRYTYPE = 'CREDITNOTE' OR ENTRYTYPE = 'SALERETURN') AND YEARID = " & YearId & " GROUP BY NAME, LEDGERID, YEARID UNION ALL SELECT 0 AS TAXABLEAMT, 0 AS CNAMT, ISNULL(SUM(TAXABLEAMT),0) AS DNAMT, NAME, LEDGERID, YEARID FROM HSNSUMMARY INNER JOIN INCENTIVES ON HSNSUMMARY.LEDGERID = INC_LEDGERID WHERE ENTRYTYPE = 'DEBITNOTE' AND YEARID = " & YearId & " GROUP BY NAME, LEDGERID, YEARID) AS T GROUP BY NAME, LEDGERID, YEARID ) AS G INNER JOIN INCENTIVES ON INCENTIVES.INC_LEDGERID = G.LEDGERID AND INCENTIVES.INC_YEARID = G.YEARID INNER JOIN INCENTIVES_DESC ON INCENTIVES.INC_NO = INCENTIVES_DESC.INC_NO AND INCENTIVES.INC_YEARID = INCENTIVES_DESC.INC_YEARID ", " GROUP BY NAME, YEARID ")
            gridbilldetails.DataSource = DT
            If dt.Rows.Count > 0 Then gridbill.FocusedRowHandle = gridbill.RowCount - 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(sender As Object, e As EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDEXPORT_Click(sender As Object, e As EventArgs) Handles CMDEXPORT.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Lot Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Lot Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Lot Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("Lot Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub IncentiveReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class