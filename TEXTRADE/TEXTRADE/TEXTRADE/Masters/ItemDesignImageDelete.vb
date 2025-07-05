
Imports BL

Public Class ItemDesignImageDelete

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub ExcelExport_Click(sender As Object, e As EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Item Design Image Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim PERIOD As String = AccFrom & " - " & AccTo
            opti.SheetName = "Item Design Image Details"
            GRIDBILL.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Item Design Image Details", GRIDBILL.VisibleColumns.Count + GRIDBILL.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("Item Design Image Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMDDELETE_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Try
            If MsgBox("Wish to Delete Selected Images?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If MsgBox("Are you Sure?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            For I As Integer = 0 To GRIDBILL.RowCount - 1
                Dim ROW As DataRow = GRIDBILL.GetDataRow(I)
                If ROW("CHK") = True Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM ITEMDESIGNIMAGE WHERE ITEMDESIGNIMAGE.ITEMDESIGN_NO = " & Val(ROW("ITEMDESIGNNO")) & " AND ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = " & YearId, "", "")
                End If
            Next
            Call CMDREFRESH_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        FILLGRID()
    End Sub

    Sub FILLGRID()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_NO, 0) AS ITEMDESIGNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNAME ", "", " ITEMDESIGNIMAGE LEFT OUTER JOIN DESIGNMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = ITEMMASTER.item_id", " AND ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = " & YearId & " ORDER BY ITEMMASTER.ITEM_NAME, DESIGNMASTER.DESIGN_NO ")
            GRIDBILLDETAILS.DataSource = dt
            If dt.Rows.Count > 0 Then
                GRIDBILL.FocusedRowHandle = GRIDBILL.RowCount - 1
                GRIDBILL.TopRowIndex = GRIDBILL.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ItemDesignImageDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'DESIGN MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class