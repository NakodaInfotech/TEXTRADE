
Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class StockOnHandWithPacking

    Public FRMSTRING, TEMPDESIGNNO, TEMPCOLOR, TEMPGODOWN, TEMPITEMNAME As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StockOnHandWithPacking_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid(" AND ROUND(MTRS,0) > 0 AND yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StockOnHandWithPacking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fillgrid(" AND ROUND(MTRS,0) > 0 AND yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim WHERECLAUSE As String = ""
            Dim OBJCMN As New ClsCommon
            Dim DTUNIT As DataTable = OBJCMN.search("UNIT_ABBR", "", "DEFAULTSTOCKUNIT", "")
            If DTUNIT.Rows.Count > 0 Then TEMPCONDITION = TEMPCONDITION & " AND UNIT IN (SELECT UNIT_ABBR FROM DEFAULTSTOCKUNIT)"
            If TEMPITEMNAME <> "" Then WHERECLAUSE = " AND T.ITEMNAME = '" & TEMPITEMNAME & "'"
            Dim dt As DataTable = OBJCMN.Execute_Any_String("SELECT * FROM (SELECT SUM(PCS) AS TOTALPCS, SUM(MTRS) AS TOTALMTRS, DESIGNNO, ITEMNAME, QUALITY, COLOR ,GODOWN, LOTNO, BALENO, CHALLANNO, PIECETYPE, BARCODE, UNIT, ITEMCODE, CATEGORY,PURRATE, SALERATE, DESIGNRATE,RACK,SHELF FROM  BARCODESTOCK WHERE 1 = 1 " & TEMPCONDITION & " GROUP BY DESIGNNO, ITEMNAME, QUALITY, LOTNO, BALENO, CHALLANNO, COLOR ,GODOWN, PIECETYPE, BARCODE, UNIT, ITEMCODE, CATEGORY, PURRATE,RACK,SHELF, SALERATE, DESIGNRATE  UNION ALL SELECT        ROUND(ISSUEPACKING_DESC.ISS_PCS - ISNULL(ISSUEPACKING_DESC.ISS_OUTPCS, 0), 2) AS BALPCS, ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2)  AS BALMTRS, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ITEMMASTER.item_name AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY,  ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, GODOWNMASTER.GODOWN_name AS GODOWN, ISNULL(ISSUEPACKING_DESC.ISS_LOTNO,'') AS LOTNO, ISNULL(ISSUEPACKING_DESC.ISS_BALENO,'') AS BALENO, ISNULL(ISSUEPACKING.ISS_CHALLANNO,'') AS CHALLANNO, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '')  AS PIECETYPE, ISSUEPACKING_DESC.ISS_BARCODE AS BARCODE, 'ISSUETOPACK' AS UNIT, ISNULL(ITEMMASTER.item_code, '') AS ITEMCODE, ISNULL(CATEGORYMASTER.category_name, '') AS CATEGORY, 0 AS PURRATE, 0 AS SALERATE, 0 AS DESIGNRATE, '' AS RACK, '' AS SHELF FROM ITEMMASTER INNER JOIN ISSUEPACKING_DESC ON ITEMMASTER.item_id = ISSUEPACKING_DESC.ISS_ITEMID INNER JOIN ISSUEPACKING ON ISSUEPACKING_DESC.ISS_NO = ISSUEPACKING.ISS_no AND ISSUEPACKING_DESC.ISS_YEARID = ISSUEPACKING.ISS_yearid INNER JOIN GODOWNMASTER ON ISSUEPACKING.ISS_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN PIECETYPEMASTER ON ISSUEPACKING_DESC.ISS_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN QUALITYMASTER ON ISSUEPACKING_DESC.ISS_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON ISSUEPACKING_DESC.ISS_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ISSUEPACKING_DESC.ISS_DESIGNID = DESIGNMASTER.DESIGN_id WHERE (ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2) > 0) AND ISSUEPACKING.ISS_YEARID = " & YearId & ") AS T WHERE 1=1  ", "", WHERECLAUSE & " ")
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
            PATH = Application.StartupPath & "\Stock with Packing.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Stock with Packing"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Stock with Packing", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Stock with Packing Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub StockOnHandWithPacking_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "SVS" Then GITEMNAME.Visible = False
        If ClientName = "SAFFRON" Then
            GITEMCODE.Visible = True
            GITEMCODE.VisibleIndex = 0
            GCATEGORY.Visible = True
            GCATEGORY.VisibleIndex = 2
        End If

        If ClientName = "SOFTAS" Then
            GQUALITY.VisibleIndex = GTOTALMTRS.VisibleIndex + 1
            GRACK.VisibleIndex = GQUALITY.VisibleIndex + 1
        End If

        If ClientName = "KARAN" Then
            GCATEGORY.Visible = True
            GCATEGORY.VisibleIndex = GITEMNAME.VisibleIndex + 1
        End If
    End Sub

End Class