Imports BL
Imports DevExpress.XtraGrid.Views.Base

Public Class ItemDesignImageDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        FILLGRID()
    End Sub

    Sub FILLGRID()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_NO, 0) AS ITEMDESIGNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNAME, ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_SETMTRS, 0) AS [SET], ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_FILENAME,'') AS FILENAME, ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_PATH,'') AS [IMAGEPATH]", "", " ITEMDESIGNIMAGE LEFT OUTER JOIN DESIGNMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = ITEMMASTER.item_id", " AND ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = " & YearId & " ORDER BY ITEMDESIGNIMAGE.ITEMDESIGN_NO ")
            If dt.Rows.Count > 0 Then
                GRIDBILL.FocusedRowHandle = GRIDBILL.RowCount - 1
                GRIDBILL.TopRowIndex = GRIDBILL.RowCount - 15
            End If
            GRIDBILLDETAILS.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        showform(False, 0)
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

    Private Sub ItemDesignImageDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub GRIDBILL_DoubleClick(sender As Object, e As EventArgs) Handles GRIDBILL.DoubleClick
        showform(True, GRIDBILL.GetFocusedRowCellValue("ITEMDESIGNNO"))
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal IMAGENO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And GRIDBILL.RowCount > 0) Then
                Dim OBJIMAGE As New ItemDesignImage
                OBJIMAGE.MdiParent = MDIMain
                OBJIMAGE.EDIT = editval
                OBJIMAGE.ITEMDESIGNNO = IMAGENO
                OBJIMAGE.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBILL_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GRIDBILL.FocusedRowChanged
        Try
            If GRIDBILL.FocusedRowHandle < 0 Then
                PBIMAGE2.Image = Nothing
                Exit Sub
            End If
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            'GET DESIGN FROM PATH
            If CATALOGPATH <> "" Then
                PBIMAGE2.ImageLocation = GRIDBILL.GetFocusedRowCellValue("IMAGEPATH")
            Else
                DT = OBJCMN.SEARCH("ITEMDESIGNIMAGE.ITEMDESIGN_IMAGE2 AS IMAGE2", "", " ITEMDESIGNIMAGE ", " AND ITEMDESIGNIMAGE.ITEMDESIGN_NO = " & Val(GRIDBILL.GetFocusedRowCellValue("ITEMDESIGNNO")) & " AND ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = " & YearId & " ORDER BY ITEMDESIGNIMAGE.ITEMDESIGN_NO ")
                If IsDBNull(DT.Rows(0).Item("IMAGE2")) = False Then
                    PBIMAGE2.Image = Image.FromStream(New IO.MemoryStream(DirectCast(DT.Rows(0).Item("IMAGE2"), Byte())))
                Else
                    PBIMAGE2.Image = Nothing
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class