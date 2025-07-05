
Imports BL

Public Class TallyDataExportSalesInv

    Public FRMSTRING As String = "Sales"

    Private Sub TallyDataExportSales_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            gridbilldetails.DataSource = Nothing

            Dim WHERECLAUSE As String = " AND VOUCHERTYPE = '" & FRMSTRING & "'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " And Date >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String(" SELECT * FROM TALLYEXPORTSALESTOCKVIEW WHERE YEARID = " & YearId & WHERECLAUSE & " ORDER BY DATE, INVOICENO", "", "")
            gridbilldetails.DataSource = DT

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSHOW_Click(sender As Object, e As EventArgs) Handles CMDSHOW.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub TOOLEXCEL_Click(sender As Object, e As EventArgs) Handles TOOLEXCEL.Click
        Try

            Dim PATH As String = ""
            If FRMSTRING = "Sales" Then PATH = Application.StartupPath & "\Tally Data.XLS" Else PATH = Application.StartupPath & "\Tally Data SR.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Tally Data"
            gridbill.ExportToXls(PATH, opti)

            Dim OBJEXCEL As New Excel.Application
            Dim OBJBOOK As Excel.Workbook = OBJEXCEL.Workbooks.Open(PATH)
            Dim OBJSHEET As Excel.Worksheet = OBJBOOK.Sheets(1)
            OBJEXCEL.Visible = True
            OBJEXCEL.AlertBeforeOverwriting = False
            OBJEXCEL.DisplayAlerts = False
            OBJSHEET.SaveAs(PATH)

        Catch ex As Exception
            MsgBox("Tally Data Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub
End Class