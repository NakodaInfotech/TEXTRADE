Imports BL
Imports System.Data.OleDb
Imports System.Data
Imports System.Linq
Imports System.Data.DataSetExtensions
Public Class UploadExcel

    Private Sub CMDSELECFILE_Click(sender As Object, e As EventArgs) Handles CMDSELECTFILE.Click
        Try
            OpenFileDialog1.Filter = "Excel (*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
            OpenFileDialog1.ShowDialog()

            OpenFileDialog1.AddExtension = True
            TXTFILENAME.Text = OpenFileDialog1.SafeFileName
            TXTB2BPATH.Text = OpenFileDialog1.FileName
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdupload_Click(sender As Object, e As EventArgs) Handles cmdupload.Click
        If TXTB2BPATH.Text.Trim = "" Then
            MessageBox.Show("Please select Excel file first.")
            Exit Sub
        End If

        Try
            ' 1. Read Excel into DataTable
            Dim dt As New DataTable()
            Dim connStr As String =
                "Provider=Microsoft.ACE.OLEDB.12.0;" &
                "Data Source=" & TXTB2BPATH.Text.Trim & ";" &
                "Extended Properties='Excel 12.0 Xml;HDR=YES;'"

            Using conn As New OleDbConnection(connStr)
                conn.Open()
                Dim schema As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                Dim sheetName As String = schema.Rows(0)("TABLE_NAME").ToString()
                Dim da As New OleDbDataAdapter("SELECT * FROM [" & sheetName & "]", conn)
                da.Fill(dt)
            End Using

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Excel file is empty.")
                Exit Sub
            End If

            ' 2. Open Purchase Invoice form
            'Dim frm As New PurchaseMaster()

            '' 3. Fill header from first row
            'Dim dr As DataRow = dt.Rows(0)
            'frm.cmbname.Text = dr("name").ToString()
            'frm.TXTPARTYBILLNO.Text = dr("party bill no").ToString()
            'frm.DTPARTYBILLDATE.Text = dr("bill date").ToString()
            'frm.TXTREFNO.Text = dr("OTHER REF(REM)").ToString()
            'frm.TXTSACCODE.Text = dr("SAC CODE").ToString()

            '' 4. Fill items into GRIDBILL
            'frm.GRIDBILL.Rows.Clear()
            'Dim sr As Integer = 1
            'For Each row As DataRow In dt.Rows
            '    If row("ITEM NAME").ToString().Trim() <> "" Then
            '        frm.GRIDBILL.Rows.Add(
            '            sr,
            '            row("ITEM NAME").ToString(),
            '            "", "", "", "", 0, 0, "",
            '            "", "", Val(row("QTY")),
            '            "PCS", 0, 0, 0,
            '            Val(row("RATE")),
            '            "Qty", Val(row("AMOUNT")),
            '            0, 0, 0, 0, 0,
            '            Val(row("TAXABLE A")),
            '            0, 0, 0, 0, 0, 0,
            '            Val(row("AMOUNT")),
            '            0, 0, "N", False, 0, 0
            '        )
            '        sr += 1
            '    End If
            'Next

            '' 5. Save into database
            'frm.SaveInvoice()
            ' Create dictionary: key = Party Bill No, value = List of rows
            Dim invoices As New Dictionary(Of String, List(Of DataRow))()

            For Each row As DataRow In dt.Rows
                Dim key As String = row("party bill no").ToString().Trim()
                If Not invoices.ContainsKey(key) Then
                    invoices(key) = New List(Of DataRow)()
                End If
                invoices(key).Add(row)
            Next

            ' Process each invoice group
            For Each kvp In invoices
                Dim rows As List(Of DataRow) = kvp.Value
                Dim dr As DataRow = rows(0)   ' first row = header

                Dim frm As New PurchaseMaster()
                frm.cmbname.Text = dr("name").ToString()
                frm.TXTPARTYBILLNO.Text = dr("party bill no").ToString()
                frm.DTPARTYBILLDATE.Text = dr("bill date").ToString()
                frm.TXTREFNO.Text = dr("OTHER REF(REM)").ToString()
                frm.TXTSACCODE.Text = dr("SAC CODE").ToString()

                frm.GRIDBILL.Rows.Clear()
                Dim sr As Integer = 1
                For Each r As DataRow In rows
                    If r("ITEM NAME").ToString().Trim() <> "" Then
                        frm.GRIDBILL.Rows.Add(sr,
                                  r("ITEM NAME").ToString(),
                                  "", "", "", "", 0, 0, "",
                                  "", "", Val(r("QTY")),
                                  "PCS", 0, 0, 0,
                                  Val(r("RATE")), "Qty", Val(r("AMOUNT")),
                                  0, 0, 0, 0, 0,
                                  Val(r("TAXABLE A")),
                                  0, 0, 0, 0, 0, 0,
                                  Val(r("AMOUNT")),
                                  0, 0, "N", False, 0, 0)
                        sr += 1
                    End If
                Next

                ' Save invoice
                frm.SaveInvoice()
            Next

            MessageBox.Show("Data uploaded into Purchase Invoice successfully!")

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
End Class