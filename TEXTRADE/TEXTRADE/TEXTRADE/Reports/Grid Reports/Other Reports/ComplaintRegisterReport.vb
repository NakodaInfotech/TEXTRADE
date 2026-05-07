
Imports BL

Public Class ComplaintRegisterReport

    Private Sub ComplaintRegisterReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("*", "", "COMPLAINTREGISTERVIEW", " AND YEARID = " & YearId & "  ORDER BY DATE, TYPE, BILLNO")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then gridbill.FocusedRowHandle = gridbill.RowCount - 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(sender As Object, e As EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDPRINT_Click(sender As Object, e As EventArgs) Handles CMDPRINT.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Complaint Register.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Complaint Register"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Complaint Register", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("Complaint Register Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ComplaintRegisterReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDWHATSAPP_Click(sender As Object, e As EventArgs) Handles CMDWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub

            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            ' Prepare data for grid
            ' TEMPOUTSTANDING()

            ' Generate the PDF from DataGridView
            Dim filePath As String = Application.StartupPath & "\ComplaintRegister.pdf"

            'Enable text wrapping
            gridbill.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            gridbill.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

            'Auto adjust row height
            gridbill.OptionsView.RowAutoHeight = True
            gridbill.OptionsPrint.AutoWidth = False
            'gridbill.BestFitColumns()
            gridbill.OptionsPrint.PrintHeader = True

            'Printing system
            Dim ps As New PrintingSystem()

            Dim link As New PrintableComponentLink(ps)

            link.Component = gridbilldetails

            'Landscape
            link.Landscape = True

            'Paper size
            link.PaperKind = System.Drawing.Printing.PaperKind.A4

            'Fit all columns in one page width
            link.PrintingSystem.Document.AutoFitToPagesWidth = 1

            'Narrow margins
            link.Margins = New System.Drawing.Printing.Margins(1, 1, 1, 1)

            'Create document
            link.CreateDocument()

            'Export
            link.ExportToPdf(filePath)

            MessageBox.Show("PDF Exported Successfully")

            ' Prepare WhatsApp sending form
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PATH.Add(filePath)
            OBJWHATSAPP.FILENAME.Add("ComplaintRegister.pdf")
            OBJWHATSAPP.ShowDialog()


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class