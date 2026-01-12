
Imports BL

Public Class DesignWithoutImage

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub ExcelExport_Click(sender As Object, e As EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Design Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Design Details"
            GRIDBILL.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Design Details", GRIDBILL.VisibleColumns.Count + GRIDBILL.GroupCount)
        Catch ex As Exception
            MsgBox("Invoice Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub DesignWithoutImage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'DESIGN MASTER'")
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

    Sub FILLGRID()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim DT As New DataTable
            If ClientName = "SNCM" Then
                DT = objclsCMST.search("DISTINCT DESIGNNO, ITEMNAME", "", " BARCODESTOCK LEFT OUTER JOIN ITEMDESIGNIMAGE ON BARCODESTOCK.DESIGNID = ITEMDESIGN_DESIGNID ", " and BARCODESTOCK.DESIGNNO <> '' AND ITEMDESIGN_PATH IS NULL AND BARCODESTOCK.YEARID = " & YearId & " ORDER BY BARCODESTOCK.DESIGNNO, ITEMNAME")
            Else
                DT = objclsCMST.search("DESIGN_NO AS DESIGNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEM_ID LEFT OUTER JOIN ITEMDESIGNIMAGE ON DESIGNMASTER.DESIGN_ID = ITEMDESIGN_DESIGNID ", " AND ITEMDESIGN_PATH IS NULL AND DESIGNMASTER.DESIGN_yearid = " & YearId)
            End If
            If DT.Rows.Count > 0 Then
                    GRIDBILL.FocusedRowHandle = GRIDBILL.RowCount - 1
                    GRIDBILL.TopRowIndex = GRIDBILL.RowCount - 15
                End If
                GRIDBILLDETAILS.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class