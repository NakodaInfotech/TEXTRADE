Imports BL
Public Class OpeningStockGreyIssueProcess
    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean

    Private Sub OpeningStockGreyIssueProcess_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\OpeningStockGreyIssueProcess.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = ""
            PERIOD = AccFrom & " - " & AccTo

            opti.SheetName = "OpeningStockGreyIssueProcess"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "OpeningStockGreyIssueProcess", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJCLSMT As New ClsCommonMaster
            Dim DT As DataTable = OBJCLSMT.search(" ISNULL(STOCKMASTER_GREYPROCESS.SMGREYPROCESS_NO, 0) AS SRNO, STOCKMASTER_GREYPROCESS.SMGREYPROCESS_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(PURLEDGERS.Acc_cmpname, '') AS PURNAME, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(UNITMASTER.unit_name, '') AS UNIT, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(STOCKMASTER_GREYPROCESS.SMGREYPROCESS_LRNO, '0') AS LRNO, STOCKMASTER_GREYPROCESS.SMGREYPROCESS_LRDATE AS LRDATE, ISNULL(STOCKMASTER_GREYPROCESS.SMGREYPROCESS_BALENO, '0') AS BALENO, ISNULL(STOCKMASTER_GREYPROCESS.SMGREYPROCESS_MTRS, 0) AS MTRS, ISNULL(STOCKMASTER_GREYPROCESS.SMGREYPROCESS_OUTMTRS, 0) AS [OUT MTRS], ISNULL(STOCKMASTER_GREYPROCESS.SMGREYPROCESS_PCS, 0) AS PCS, ISNULL(STOCKMASTER_GREYPROCESS.SMGREYPROCESS_OUTPCS, 0) AS [OUT PCS], ISNULL(STOCKMASTER_GREYPROCESS.SMGREYPROCESS_RATE, 0) AS RATE, ISNULL(STOCKMASTER_GREYPROCESS.SMGREYPROCESS_PER, '0') AS PER, ISNULL(STOCKMASTER_GREYPROCESS.SMGREYPROCESS_AMOUNT, 0) AS AMOUNT", "", " COLORMASTER RIGHT OUTER JOIN STOCKMASTER_GREYPROCESS INNER JOIN UNITMASTER ON STOCKMASTER_GREYPROCESS.SMGREYPROCESS_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN DESIGNMASTER ON STOCKMASTER_GREYPROCESS.SMGREYPROCESS_DESIGNID = DESIGNMASTER.DESIGN_id ON COLORMASTER.COLOR_id = STOCKMASTER_GREYPROCESS.SMGREYPROCESS_COLORID LEFT OUTER JOIN ITEMMASTER ON STOCKMASTER_GREYPROCESS.SMGREYPROCESS_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON STOCKMASTER_GREYPROCESS.SMGREYPROCESS_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS PURLEDGERS ON STOCKMASTER_GREYPROCESS.SMGREYPROCESS_PURLEDGERID = PURLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON STOCKMASTER_GREYPROCESS.SMGREYPROCESS_LEDGERID = LEDGERS.Acc_id ", " AND STOCKMASTER_GREYPROCESS.SMGREYPROCESS_YEARID = " & YearId & " ORDER BY dbo.STOCKMASTER_GREYPROCESS.SMGREYPROCESS_NO")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub






End Class