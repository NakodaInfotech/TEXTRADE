
Imports BL

Public Class SelectPurLRStock

    Public ITEMNAME As String = ""
    Public DT As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectPurLRStock_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectPurLRStock_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim WHERE As String = ""
            If ITEMNAME <> "" Then
                WHERE = WHERE & " AND ITEMNAME = '" & ITEMNAME & "'"
            End If

            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("CAST(0 AS BIT) AS CHK, PURCHASELRSTOCK.*", "", " PURCHASELRSTOCK ", WHERE & " AND PURCHASELRSTOCK.YEARID=" & YearId & " AND PURCHASELRSTOCK.SOLD = 0 ORDER BY TYPE, BILLNO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            DT.Columns.Add("PURNAME")
            DT.Columns.Add("PARTYBILLNO")
            DT.Columns.Add("LRNO")
            DT.Columns.Add("TRANSPORT")
            DT.Columns.Add("ITEMNAME")
            DT.Columns.Add("HSNCODE")
            DT.Columns.Add("AQTY")
            DT.Columns.Add("FOLDPER")
            DT.Columns.Add("QTY")
            DT.Columns.Add("MTRS")
            DT.Columns.Add("UNIT")
            DT.Columns.Add("WT")
            DT.Columns.Add("FROMNO")
            DT.Columns.Add("TYPE")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim DTROW As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(DTROW("NAME"), DTROW("PARTYBILLNO"), DTROW("LRNO"), DTROW("TRANSNAME"), DTROW("ITEMNAME"), DTROW("HSNCODE"), Val(DTROW("AQTY")), Val(DTROW("FOLDPER")), Val(DTROW("TOTALQTY")), Val(DTROW("TOTALMTRS")), DTROW("UNIT"), Val(DTROW("WT")), Val(DTROW("BILLNO")), DTROW("TYPE"))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CHKSELECT_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECT.CheckedChanged
        Try
            If gridbilldetails.Visible = True Then
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("CHK") = CHKSELECT.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class