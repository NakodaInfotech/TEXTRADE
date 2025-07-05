
Imports BL

Public Class SelectGreyTransport

    Public TRANSNAME As String = ""
    Public DT As New DataTable
    Public GREYRECNO As Integer = 0

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectGreyTransport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        Try
            Cursor.Current = Cursors.WaitCursor

            Dim where As String = " AND ROUND(MTRS,2) > 0 "
            If GREYRECNO <> 0 Then where = where & " AND GREYRECNO = " & GREYRECNO
            If TRANSNAME <> "" Then where = where & " AND TRANSNAME = '" & TRANSNAME & "'"

            Dim objclspreq As New ClsCommon()
            Dim dt As DataTable = objclspreq.search("*", "", " GREYSTOCKTRANSPORT ", " AND YEARID = " & YearId & where & " ORDER BY DATE, GREYRECNO ")

            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try

            'Dim COUNT As Integer = 0
            'For i As Integer = 0 To gridbill.RowCount - 1
            '    Dim dtrow As DataRow = gridbill.GetDataRow(i)
            '    If Convert.ToBoolean(dtrow("CHK")) = True Then
            '        COUNT = COUNT + 1
            '    End If
            'Next
            'If COUNT > 1 Then
            '    MsgBox("You Can Select Only One Entry")
            '    Exit Sub
            'End If

            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()

            If Val(SELECTEDROWS.Length) > 1 And ClientName <> "OWAIS" Then
                MsgBox("You Can Select Only One Entry")
                Exit Sub
            End If

            DT.Columns.Add("GREYRECNO")
            DT.Columns.Add("NAME")
            DT.Columns.Add("ITEMNAME")
            DT.Columns.Add("DESIGNNO")
            DT.Columns.Add("COLOR")
            DT.Columns.Add("PCS")
            DT.Columns.Add("MTRS")
            DT.Columns.Add("RATE")
            DT.Columns.Add("TRANSPORT")
            DT.Columns.Add("LRNO")
            DT.Columns.Add("LRDATE")
            DT.Columns.Add("TYPE")
            DT.Columns.Add("GRIDSRNO")
            DT.Columns.Add("BALENO")
            DT.Columns.Add("UNIT")
            DT.Columns.Add("REMARKS")
            DT.Columns.Add("AGENT")
            DT.Columns.Add("CRDAYS")



            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                Dim dtrow As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                DT.Rows.Add(dtrow("GREYRECNO"), dtrow("NAME"), dtrow("ITEMNAME"), dtrow("DESIGNNO"), dtrow("COLOR"), Val(dtrow("PCS")), Val(dtrow("BALMTRS")), Val(dtrow("RATE")), dtrow("TRANSNAME"), dtrow("LRNO"), Convert.ToDateTime(dtrow("LRDATE")).Date, dtrow("TYPE"), Val(dtrow("GRIDSRNO")), dtrow("BALENO"), dtrow("UNIT"), dtrow("REMARKS"), dtrow("AGENT"), dtrow("CRDAYS"))
            Next
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectGreyTransport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class