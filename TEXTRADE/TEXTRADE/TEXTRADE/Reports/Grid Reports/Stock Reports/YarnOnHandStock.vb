
Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO

Public Class YarnOnHandStock

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnOnHandStock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Opening_Stock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fillgrid(" AND yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim OBJCMN As New ClsCommon
            If ALLOWYARNBARCODEPRINT = True Then
                Dim dt As DataTable = OBJCMN.SEARCH(" GODOWN, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO,  SUM(ISNULL(CONES,0)) AS CONES, SUM(BAGS) AS BAGS, SUM(WT) AS WT, BARCODE , BILLNO , JOBBERNAME , RACK ", "", "  YARNBARCODESTOCK ", TEMPCONDITION & " GROUP BY GODOWN, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO , BARCODE , BILLNO , JOBBERNAME , RACK HAVING SUM(WT) > 0")
                gridbilldetails.DataSource = dt
                If dt.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If
            Else
                Dim dt As DataTable = OBJCMN.SEARCH(" GODOWN, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO,  SUM(ISNULL(CONES,0)) AS CONES, SUM(BAGS) AS BAGS, SUM(WT) AS WT ", "", "  YARNSTOCKVIEW ", TEMPCONDITION & " GROUP BY GODOWN, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO HAVING SUM(WT) > 0")
                gridbilldetails.DataSource = dt
                If dt.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Yarn Stock In Hand.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Yarn Stock In Hand"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Yarn Stock In Hand", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Yarn Stock Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid(" AND yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVELAYOUT_Click(sender As Object, e As EventArgs) Handles CMDSAVELAYOUT.Click
        Try
            Dim layoutFileName As String = $"{Me.Name}"
            Dim layoutPath As String = System.IO.Path.Combine(Application.StartupPath, layoutFileName)
            gridbill.SaveLayoutToXml(layoutPath)
            'MessageBox.Show("Layout saved as: " & layoutFileName)




            ' Prompt user for filename
            Dim userFileName As String = InputBox("Enter a name for the layout file (without extension):", "Save Layout", Me.Name)

            ' Exit if the user cancels or enters nothing
            If String.IsNullOrWhiteSpace(userFileName) Then
                MessageBox.Show("Save cancelled.")
                Exit Sub
            End If

            ' Add .xml extension and construct path
            Dim FileName As String = $"{userFileName}.xml"

            ' Save layout to file
            gridbill.SaveLayoutToXml(layoutPath)
            MessageBox.Show("Layout saved as: " & FileName)

            ' Read file content
            Dim xmlContent As String = File.ReadAllText(layoutPath)



            Dim OBJSELECTSG As New SelectCustomLayout
            OBJSELECTSG.FORMNAMES = layoutFileName
            OBJSELECTSG.FILENAME = FileName
            OBJSELECTSG.FILES = xmlContent
            OBJSELECTSG.ShowDialog()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class