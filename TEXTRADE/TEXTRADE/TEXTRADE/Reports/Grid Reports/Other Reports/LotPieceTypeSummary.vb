Imports BL

Public Class LotPieceTypeSummary
    Public LOTNO As String

    Private Sub LotPieceTypeSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            Dim sql As String = "DECLARE @COLS NVARCHAR(MAX); DECLARE @COLS_PCT NVARCHAR(MAX); DECLARE @SQL NVARCHAR(MAX); 
SELECT @COLS = STRING_AGG(QUOTENAME(PIECETYPE), ','), @COLS_PCT = STRING_AGG( 'CAST((' + QUOTENAME(PIECETYPE) + '*100.0)/NULLIF(ACCEPTEDMTRS,0) AS DECIMAL(10,2)) AS '
        + QUOTENAME(PIECETYPE + '_PCT') , ',') FROM (SELECT DISTINCT PM.PIECETYPE_name AS PIECETYPE FROM MATERIALRECEIPT_DESC MRD INNER JOIN PIECETYPEMASTER PM  
        ON MRD.MATREC_PIECETYPEID = PM.PIECETYPE_id WHERE MRD.MATREC_YEARID = " & YearId & " ) X; 
        SET @SQL = ' SELECT JOBBERNAME, LOTNO, ACCEPTEDMTRS, BALMTRS, BALMTRS_PCT,' + @COLS + ', ' + @COLS_PCT + ' FROM (
    SELECT  
        LV.JOBBERNAME,
        LV.LOTNO,
        LV.ACCEPTEDMTRS,
        PM.PIECETYPE_name AS PIECETYPE,
        SUM(MRD.MATREC_RECDMTRS) AS RECDMTRS,
        LV.BALMTRS,
		CAST((BALMTRS * 100.0) / NULLIF(ACCEPTEDMTRS,0) AS DECIMAL(10,2)) AS BALMTRS_PCT
    FROM MATERIALRECEIPT MR
    INNER JOIN MATERIALRECEIPT_DESC MRD
        ON MR.MATREC_NO = MRD.MATREC_NO
        AND MR.MATREC_YEARID = MRD.MATREC_YEARID
    INNER JOIN PIECETYPEMASTER PM
        ON MRD.MATREC_PIECETYPEID = PM.PIECETYPE_id
    INNER JOIN (
        SELECT 
            JOBBERNAME,
            LOTNO,
            YEARID,
            JOBBERLEDGERID,
            SUM(ACCEPTEDMTRS) AS ACCEPTEDMTRS,
            SUM(BALMTRS) AS BALMTRS
        FROM LOT_VIEW
        GROUP BY JOBBERNAME, LOTNO, YEARID, JOBBERLEDGERID
    ) LV
        ON MR.MATREC_ledgerid = LV.JOBBERLEDGERID
        AND MRD.MATREC_GRIDLOTNO = LV.LOTNO
        AND MR.MATREC_yearid = LV.YEARID
    WHERE LV.YEARID = " & YearId & "
    GROUP BY 
        LV.JOBBERNAME,
        LV.LOTNO,
        LV.ACCEPTEDMTRS,
        LV.BALMTRS,
        PM.PIECETYPE_name
) SRC
PIVOT (
    SUM(RECDMTRS) FOR PIECETYPE IN (' + @COLS + ')
) P';

EXEC sp_executesql @SQL;
"
            dt = objclsCMST.Execute_Any_String(sql, "", "")
            gridbilldetails.DataSource = dt
            gridbill.PopulateColumns()
            gridbill.OptionsView.ShowFooter = True
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In gridbill.Columns
                If Not col.FieldName.EndsWith("_PCT") Then
                    Dim pctColName As String = col.FieldName & "_PCT"
                    ' Agar uska % column exist karta hai
                    If gridbill.Columns(pctColName) IsNot Nothing Then
                        gridbill.Columns(pctColName).VisibleIndex = col.VisibleIndex + 1
                    End If
                End If
            Next
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In gridbill.Columns
                If col.SummaryItem IsNot Nothing _
           AndAlso col.SummaryItem.SummaryValue IsNot Nothing _
           AndAlso IsNumeric(col.SummaryItem.SummaryValue) _
           AndAlso Convert.ToDecimal(col.SummaryItem.SummaryValue) = 0 Then
                    col.Visible = False
                End If
            Next
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPRINT_Click(sender As Object, e As EventArgs) Handles CMDPRINT.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Lot Piece Type Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Lot Piece Type Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Lot Piece Type Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("Lot Piece Type Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub cmdcancel_Click(sender As Object, e As EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class