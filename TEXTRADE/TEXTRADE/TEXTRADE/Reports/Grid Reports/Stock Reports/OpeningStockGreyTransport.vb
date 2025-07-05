Imports BL
Public Class OpeningStockGreyTransport

    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Opening Stock Grey Transport Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Opening Stock Grey Transport Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Opening Stock Grey Transport Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Opening Stock Grey Transport Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJCLSMT As New ClsCommonMaster
            Dim DT As DataTable = OBJCLSMT.search("  ISNULL(STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_GRIDSRNO, 0) AS SRNO, ISNULL(STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_NO, 0) AS NO, STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORTER, ISNULL(STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_LRNO, '') AS LRNO, STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_LRDATE AS LRDATE, ISNULL(STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_BALENO, '0') AS BALENO, ISNULL(STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_PCS, 0) AS PCS, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(UNITMASTER.unit_name, '') AS UNIT, ISNULL(STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_MTRS, 0) AS MTRS, ISNULL(STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_OUTPCS, 0) AS OUTPCS, ISNULL(STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_OUTMTRS, 0) AS OUTMTRS, ISNULL(STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_RATE, 0) AS RATE, ISNULL(STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_PER, '') AS PER, ISNULL(STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_AMOUNT, 0) AS AMOUNT, ISNULL(STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_CRDAYS, 0) AS CRDAYS, ISNULL(AGENTLEDGER.Acc_cmpname, '') AS AGENT, ISNULL(ITEMMASTER.item_name, '') AS ITEM", "", " STOCKMASTER_GREYTRANSPORT LEFT OUTER JOIN ITEMMASTER ON STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGER ON STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_AGENTID = AGENTLEDGER.Acc_id LEFT OUTER JOIN UNITMASTER ON STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN COLORMASTER ON STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_LEDGERID = LEDGERS.Acc_id ", " AND dbo.STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_YEARID = " & YearId & " ORDER BY dbo.STOCKMASTER_GREYTRANSPORT.SMGREYTRANS_NO ")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub OpeningStockGreyTransport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")

            End If
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class