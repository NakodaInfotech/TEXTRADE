Imports BL
Imports DevExpress.XtraGrid.Views.Base

Public Class LotPieceTypeSummary
    Public LOTNO As String
    Public WHERECLAUSE As String

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
        SET @SQL = ' SELECT JOBBERNAME AS [JOBBER NAME], GREYITEMNAME AS [GREY ITEM NAME], RECDITEMNAME AS [RECD ITEM NAME], LOTNO, ACCEPTEDMTRS AS MTRS, BALMTRS AS SHRINK, BALMTRS_PCT,' + @COLS + ', ' + @COLS_PCT + ' FROM (
    SELECT  
        LV.JOBBERNAME,
        LV.GREYITEMNAME,
		RITEM.item_name AS RECDITEMNAME,
        LV.LOTNO,
        LV.ACCEPTEDMTRS,
        PM.PIECETYPE_name AS PIECETYPE,
        SUM(MRD.MATREC_RECDMTRS) AS RECDMTRS,
        LV.BALMTRS,
		CAST((BALMTRS * 100.0) / NULLIF(ACCEPTEDMTRS,0) AS DECIMAL(10,2)) AS BALMTRS_PCT,
        MR.MATREC_DATE AS RECDATE
    FROM MATERIALRECEIPT MR
    INNER JOIN MATERIALRECEIPT_DESC MRD
        ON MR.MATREC_NO = MRD.MATREC_NO
        AND MR.MATREC_YEARID = MRD.MATREC_YEARID
    
    CROSS APPLY
	(SELECT TOP 1 ITEM_NAME FROM ITEMMASTER AS RITEMMASTER INNER JOIN MATERIALRECEIPT_DESC INNER JOIN MATERIALRECEIPT ON MATERIALRECEIPT_DESC.MATREC_NO = MATERIALRECEIPT.MATREC_NO AND 
	MATERIALRECEIPT_DESC.MATREC_YEARID = MATERIALRECEIPT.MATREC_yearid ON MATERIALRECEIPT_DESC.MATREC_ITEMID = RITEMMASTER.ITEM_ID WHERE MR.MATREC_ledgerid = MATERIALRECEIPT.MATREC_ledgerid
        AND MRD.MATREC_GRIDLOTNO = MATERIALRECEIPT_DESC.MATREC_GRIDLOTNO
        AND MR.MATREC_yearid = MATERIALRECEIPT.MATREC_yearid ) AS RITEM

    INNER JOIN PIECETYPEMASTER PM
        ON MRD.MATREC_PIECETYPEID = PM.PIECETYPE_id
    INNER JOIN (
        SELECT 
            JOBBERNAME,
            ITEMNAME AS GREYITEMNAME,
            LOTNO,
            YEARID,
            JOBBERLEDGERID,
            SUM(ACCEPTEDMTRS) AS ACCEPTEDMTRS,
            SUM(BALMTRS) AS BALMTRS,GROUPNAME,QUALITY,CATEGORYNAME,PROGRAMDONE,DYEINGJOB,CHALLANNO,TOTALPCS,RECDPCS,LOTCOMPLETED
        FROM LOT_VIEW
        GROUP BY JOBBERNAME, ITEMNAME, LOTNO, YEARID, JOBBERLEDGERID,GROUPNAME,QUALITY,CATEGORYNAME,PROGRAMDONE,DYEINGJOB,CHALLANNO,TOTALPCS,RECDPCS,LOTCOMPLETED
    ) LV
        ON MR.MATREC_ledgerid = LV.JOBBERLEDGERID
        AND MRD.MATREC_GRIDLOTNO = LV.LOTNO
        AND MR.MATREC_yearid = LV.YEARID
    WHERE  1 = 1 " & WHERECLAUSE & "
    GROUP BY 
        LV.JOBBERNAME,
        LV.GREYITEMNAME,
		RITEM.item_name ,
        LV.LOTNO,
        LV.ACCEPTEDMTRS,
        LV.BALMTRS,
        PM.PIECETYPE_name
, MR.MATREC_DATE

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
                If col.SummaryItem IsNot Nothing AndAlso col.SummaryItem.SummaryValue IsNot Nothing AndAlso IsNumeric(col.SummaryItem.SummaryValue) AndAlso Convert.ToDecimal(col.SummaryItem.SummaryValue) = 0 Then
                    col.Visible = False
                End If
                If col.Name.Contains("_PCT") Then col.Caption = "%"
            Next
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
            End If
            gridbill.BestFitColumns()
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

    Private Sub gridbill_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles gridbill.CustomColumnDisplayText
        Try
            If e.Column.VisibleIndex >= 4 AndAlso IsNumeric(e.Value) Then
                e.DisplayText = Convert.ToDecimal(e.Value).ToString("0.00")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class