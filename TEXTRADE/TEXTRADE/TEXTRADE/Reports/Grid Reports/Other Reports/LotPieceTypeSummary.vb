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
            dt = objclsCMST.Execute_Any_String(" 
Declare @COLS NVARCHAR(MAX);
Declare @COLS_PCT NVARCHAR(MAX);
Declare @SQL NVARCHAR(MAX);

Select Case
    @COLS = STRING_AGG(QUOTENAME(PIECETYPE), ','),
    @COLS_PCT = STRING_AGG(
        'CAST((' + QUOTENAME(PIECETYPE) + '*100.0)/NULLIF(ACCEPTEDMTRS,0) AS DECIMAL(10,2)) AS '
        + QUOTENAME(PIECETYPE + '_PCT')
    , ',')
FROM (
    Select Case DISTINCT PM.PIECETYPE_name As PIECETYPE
    FROM MATERIALRECEIPT_DESC MRD
    INNER JOIN PIECETYPEMASTER PM 
        On MRD.MATREC_PIECETYPEID = PM.PIECETYPE_id
    WHERE MRD.MATREC_YEARID = " & YearId & "
) X;

Set @SQL = '
Select Case JOBBERNAME, LOTNO, ACCEPTEDMTRS, BALMTRS,
       ' + @COLS + ', ' + @COLS_PCT + '
FROM (
     SELECT  
        LV.JOBBERNAME,
        LV.LOTNO,
        LV.ACCEPTEDMTRS,
        PM.PIECETYPE_name AS PIECETYPE,
        SUM(MRD.MATREC_RECDMTRS) AS RECDMTRS,
        LV.BALMTRS
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
", "", "")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then gridbill.FocusedRowHandle = gridbill.RowCount - 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class