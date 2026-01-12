Imports BL
Imports DevExpress.XtraGrid.Views.Base
Public Class DispatchUnitWiseSummary
    Public WHERECLAUSE As String = ""

    Private Sub DispatchUnitWiseSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommon
            Dim dt As New DataTable
            Dim sql As String = "DECLARE @COLS AS NVARCHAR(MAX);
DECLARE @SQL AS NVARCHAR(MAX);

SELECT @COLS = STRING_AGG(QUOTENAME(UNIT), ',') FROM (SELECT DISTINCT UNITMASTER.UNIT_ABBR AS UNIT FROM GDN_DESC INNER JOIN UNITMASTER ON GDN_UNITID = UNIT_ID WHERE GDN_YEARID = 6) AS C;


SET @SQL = '
SELECT * FROM
(SELECT        UNITMASTER.UNIT_ABBR AS UNIT, ROUND(SUM(GDN_DESC.GDN_MTRS),2) AS MTRS
FROM            GDN INNER JOIN
                         GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID INNER JOIN
                         UNITMASTER ON GDN_DESC.GDN_UNITID = UNITMASTER.unit_id INNER JOIN
                         ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id INNER JOIN
                         COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id INNER JOIN
                         GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id INNER JOIN
                         LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id INNER JOIN
                         LEDGERS AS agentLEDGERS ON GDN.GDN_AGENTID = agentLEDGERS.Acc_id
WHERE GDN.GDN_YEARID = " & YearId & WHERECLAUSE & "
GROUP BY UNITMASTER.UNIT_ABBR
) AS SRC
PIVOT
(
	SUM(MTRS) FOR UNIT IN (' + @COLS + ')
) AS P;';

EXEC sp_executesql @sql;"
            dt = objclsCMST.Execute_Any_String(sql, "", "")
            gridbilldetails.DataSource = dt
            gridbill.PopulateColumns()
            gridbill.OptionsView.ShowFooter = True
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
            End If
            gridbill.BestFitColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(sender As Object, e As EventArgs) Handles TOOLEXCEL.Click
        Try
            Try
                Dim PATH As String = Application.StartupPath & "\Dispatch Unit Wise Rpt.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True
                opti.SheetName = "Dispatch Unit Wise Rpt"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Dispatch Unit Wise Rpt", gridbill.VisibleColumns.Count + gridbill.GroupCount)
            Catch ex As Exception
                MsgBox("Dispatch Unit Wise Rpt Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
            End Try
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
End Class