Imports BL
Imports DevExpress.XtraGrid.Views.Base
Imports SystemWindows.Forms
Public Class DesignWithoutImage
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean
    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub ExcelExport_Click(sender As Object, e As EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Design Without Image.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim PERIOD As String = AccFrom & " - " & AccTo
            opti.SheetName = "Design Without Image"
            GRIDBILL.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Design Without Image", GRIDBILL.VisibleColumns.Count + GRIDBILL.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("Design Without Image Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
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

            ' FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class