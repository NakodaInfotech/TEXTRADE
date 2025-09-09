
Imports BL
Imports DevExpress.XtraGrid.Views.Base

Public Class StockRegisterChallanWise

    Public FRMSTRING As String = "SUMM"
    Public WHERECLAUSE As String = ""
    Public FROMDATE As Date = AccFrom
    Public TODATE As Date = AccTo

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub FILLGRID()
        Dim OBJCMN As New ClsCommon
        'Dim DT As DataTable = OBJCMN.search(" NAME, CHALLANNO, SUM(PCS) AS PCS, SUM(MTRS) AS MTRS, SUM(ISSPCS) AS ISSPCS, SUM(ISSMTRS) AS ISSMTRS,  ROUND(SUM(PCS)-SUM(ISSPCS),0) AS BALPCS, ROUND(SUM(MTRS)-SUM(ISSMTRS),2) AS BALMTRS ", "", " STOCKREGISTER", " AND YEARID = " & YearId & " AND CHALLANNO <> '' GROUP BY NAME, CHALLANNO HAVING ROUND(SUM(PCS)-SUM(ISSPCS),2) > 0")
        Dim DT As New DataTable
        If FRMSTRING = "SUMM" Then
            If ClientName = "AARYA" Then
                DT = OBJCMN.SEARCH(" STOCKREGISTER.NAME, STOCKREGISTER.CHALLANNO, STOCKREGISTER.LOTNO, SUM(STOCKREGISTER.PCS) AS PCS, SUM(STOCKREGISTER.MTRS) AS MTRS, SUM(STOCKREGISTER.ISSPCS) AS ISSPCS, SUM(STOCKREGISTER.ISSMTRS) AS ISSMTRS,  ROUND(SUM(STOCKREGISTER.PCS)-SUM(STOCKREGISTER.ISSPCS),0) AS BALPCS, ROUND(SUM(STOCKREGISTER.MTRS)-SUM(STOCKREGISTER.ISSMTRS),2) AS BALMTRS, SREG.DATE AS DATE  ", "", " STOCKREGISTER CROSS APPLY (SELECT TOP 1 * FROM STOCKREGISTER SREG WHERE STOCKREGISTER.NAME = SREG.NAME AND STOCKREGISTER.CHALLANNO = SREG.CHALLANNO AND STOCKREGISTER.YEARID = SREG.YEARID AND SREG.PCS > 0 ORDER BY DATE ) AS SREG ", " AND STOCKREGISTER.YEARID = " & YearId & " AND STOCKREGISTER.CHALLANNO <> '' GROUP BY STOCKREGISTER.NAME, STOCKREGISTER.CHALLANNO, STOCKREGISTER.LOTNO, SREG.DATE HAVING ROUND(SUM(STOCKREGISTER.MTRS)-SUM(STOCKREGISTER.ISSMTRS),2) > 0 ORDER BY STOCKREGISTER.NAME, STOCKREGISTER.CHALLANNO")
            Else
                DT = OBJCMN.SEARCH(" STOCKREGISTER.NAME, STOCKREGISTER.CHALLANNO, SUM(STOCKREGISTER.PCS) AS PCS, SUM(STOCKREGISTER.MTRS) AS MTRS, SUM(STOCKREGISTER.ISSPCS) AS ISSPCS, SUM(STOCKREGISTER.ISSMTRS) AS ISSMTRS,  ROUND(SUM(STOCKREGISTER.PCS)-SUM(STOCKREGISTER.ISSPCS),0) AS BALPCS, ROUND(SUM(STOCKREGISTER.MTRS)-SUM(STOCKREGISTER.ISSMTRS),2) AS BALMTRS, SREG.DATE AS DATE  ", "", " STOCKREGISTER CROSS APPLY (SELECT TOP 1 * FROM STOCKREGISTER SREG WHERE STOCKREGISTER.NAME = SREG.NAME AND STOCKREGISTER.CHALLANNO = SREG.CHALLANNO AND STOCKREGISTER.YEARID = SREG.YEARID AND SREG.PCS > 0 ORDER BY DATE ) AS SREG ", " AND STOCKREGISTER.YEARID = " & YearId & " AND STOCKREGISTER.CHALLANNO <> '' GROUP BY STOCKREGISTER.NAME, STOCKREGISTER.CHALLANNO, SREG.DATE HAVING ROUND(SUM(STOCKREGISTER.MTRS)-SUM(STOCKREGISTER.ISSMTRS),2) > 0")
            End If
        Else
            DT = OBJCMN.SEARCH(" STOCKREGISTER.NAME, STOCKREGISTER.CHALLANNO, STOCKREGISTER.LOTNO, SUM(STOCKREGISTER.PCS) AS PCS, SUM(STOCKREGISTER.MTRS) AS MTRS, SUM(STOCKREGISTER.ISSPCS) AS ISSPCS, SUM(STOCKREGISTER.ISSMTRS) AS ISSMTRS, STOCKREGISTER.NO AS ENTRYNO, STOCKREGISTER.ENTRYTYPE , STOCKREGISTER.DATE AS DATE  ", "", " STOCKREGISTER ", WHERECLAUSE & " AND STOCKREGISTER.YEARID = " & YearId & " AND STOCKREGISTER.CHALLANNO <> '' GROUP BY STOCKREGISTER.NAME, STOCKREGISTER.CHALLANNO, STOCKREGISTER.LOTNO, STOCKREGISTER.NO, STOCKREGISTER.ENTRYTYPE , STOCKREGISTER.DATE ORDER BY STOCKREGISTER.DATE, STOCKREGISTER.ENTRYTYPE, STOCKREGISTER.NO")
        End If

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
            If FRMSTRING = "DETAILS" Then
                Me.Text = "Stock REgister Challan Wise Details"
                GENTRYNO.Visible = True
                GENTRYNO.VisibleIndex = GBALPCS.VisibleIndex

                GENTRYTYPE.Visible = True
                GENTRYTYPE.VisibleIndex = GENTRYNO.VisibleIndex + 1

                GBALPCS.Visible = False
                GBALMTRS.Visible = False

            End If
            FILLGRID()
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
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StockRegisterChallanWise_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName <> "AARYA" Then GLOTNO.Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            If FRMSTRING = "DETAILS" Then Exit Sub
            Dim OBJSTOCKREG As New StockRegisterChallanWise
            OBJSTOCKREG.FRMSTRING = "DETAILS"
            OBJSTOCKREG.WHERECLAUSE = " AND STOCKREGISTER.CHALLANNO = '" & gridregister.GetFocusedRowCellValue("CHALLANNO") & "' AND STOCKREGISTER.NAME = '" & gridregister.GetFocusedRowCellValue("NAME") & "'"
            OBJSTOCKREG.MdiParent = MDIMain
            OBJSTOCKREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class