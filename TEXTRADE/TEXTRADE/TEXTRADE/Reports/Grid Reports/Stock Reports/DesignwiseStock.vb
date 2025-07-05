
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class DesignwiseStock

    Public FRMSTRING As String
    Public WHERECLAUSE As String = ""
    Public TEMPQUALITY As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Designwise_Stock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Designwise_Stock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fillgrid(" and yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform()
        Try
            Dim ObjColorwiseStocks As New ColorwiseStock
            ObjColorwiseStocks.MdiParent = MDIMain
            ObjColorwiseStocks.TEMPQUALITY = gridbill.GetFocusedRowCellValue("QUALITY")
            ObjColorwiseStocks.TEMPDESIGNNO = gridbill.GetFocusedRowCellValue("DESIGNNO")
            ObjColorwiseStocks.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try

            If ClientName <> "MOMAI" And ClientName <> "AXIS" And ClientName <> "AVIS" Then TEMPCONDITION = TEMPCONDITION & " AND ROUND(MTRS,0) > 0 "
            If TEMPQUALITY <> "" Then TEMPCONDITION = TEMPCONDITION & " AND QUALITY='" & TEMPQUALITY & "' "

            Dim GROUPBY As String = " GROUP BY QUALITY,DESIGNNO"
            'If TEMPQUALITY <> "" Then GROUPBY = GROUPBY & ", QUALITY"

            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable
            If TEMPQUALITY = "" Then
                dt = objclsCMST.search("CAST(0 AS BIT) AS SAMPLEMATCH, ITEMNAME, QUALITY, DESIGNNO, SUM(PCS) AS TOTALPCS, SUM(MTRS) AS TOTALMTRS, CATEGORY", "", "  BARCODESTOCK", WHERECLAUSE & TEMPCONDITION & "GROUP BY ITEMNAME, QUALITY, DESIGNNO, CATEGORY")
            Else
                dt = objclsCMST.search("CAST(0 AS BIT) AS SAMPLEMATCH, SUM(PCS) AS TOTALPCS, SUM(MTRS) AS TOTALMTRS,'" & TEMPQUALITY & "' AS QUALITY, DESIGNNO", "", "  BARCODESTOCK", TEMPCONDITION & GROUPBY)
            End If

            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Designwise Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Designwise Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Designwise Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Design Stock Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        Try
            showform()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                Dim MATCHFOUND As Boolean = False
                'GET DATA FROM SAMPLE BARCODE
                'no need for yearid clause here as we need to fetch this barcode in all acccouting year
                DT = OBJCMN.SEARCH(" SAMPLEBARCODE.SB_NO AS SBNO, SAMPLEBARCODE.SB_GRIDSRNO AS GRIDSRNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_NAME,'') AS QUALITY, ISNULL(DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(SAMPLEBARCODE.SB_REMARKS, '') AS REMARKS, SAMPLEBARCODE.SB_BARCODE AS BARCODE", "", " SAMPLEBARCODE INNER JOIN ITEMMASTER ON SAMPLEBARCODE.SB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN QUALITYMASTER ON SAMPLEBARCODE.SB_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON SAMPLEBARCODE.SB_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON SAMPLEBARCODE.SB_DESIGNID = DESIGNMASTER.DESIGN_id  ", " AND SB_BARCODE = '" & TXTBARCODE.Text.Trim & "'")
                If DT.Rows.Count > 0 Then

                    For I As Integer = 0 To gridbill.RowCount - 1
                        Dim ROW As DataRow = gridbill.GetDataRow(I)
                        If ROW("ITEMNAME") = DT.Rows(0).Item("ITEMNAME") And ROW("DESIGNNO") = DT.Rows(0).Item("DESIGNNO") Then
                            ROW("SAMPLEMATCH") = True
                            MATCHFOUND = True
                            If ClientName <> "AVIS" Then GoTo LINE1
                        End If
                    Next
                    'IF ENTRY IS NOT FOUND THEN GIVE MESSAGE
                    If MATCHFOUND = False Then MsgBox("Sample Not Present in Stock", MsgBoxStyle.Critical)


LINE1:
                    TXTBARCODE.Clear()
                    TXTBARCODE.Focus()

                    If gridbill.RowCount > 0 Then
                        gridbill.FocusedRowHandle = gridbill.RowCount - 1
                        gridbill.TopRowIndex = gridbill.RowCount - 15
                    End If

                Else
                    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                    TXTBARCODE.Clear()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("SAMPLEMATCH")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class