Imports BL
Imports DevExpress.CodeParser

Public Class SelectStoreStock

    Dim tempindex, i As Integer
    Dim SELECTIONFORMULA As String = ""
    Public DT As New DataTable
    Public GODOWN As String = ""


    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
            Me.Close()
        End Sub

    Private Sub SelectStoreStock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectStoreStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FILLGRID(" ")
    End Sub

    Sub FILLGRID(ByVal WHERE As String)
        Try

            Cursor.Current = Cursors.WaitCursor
            If GODOWN <> "" Then WHERE = WHERE & " AND GODOWN = '" & GODOWN & "'"

            Dim objcmn As New ClsCommon
            Dim dt As DataTable
            dt = objcmn.SEARCH(" CAST(0 AS BIT) AS CHK,ITEMNAME , SUM(QTY) AS QTY ,UNIT ", "", "STORESTOCKREGISTER", " " & WHERE & " AND  CMPID = " & CmpId & " AND YEARID = " & YearId & " GROUP BY  ITEMNAME , UNIT  HAVING SUM(QTY)> 0")
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

        Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try


            DT.Columns.Add("ITEMNAME")
            DT.Columns.Add("QTY")
            DT.Columns.Add("UNIT")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("ITEMNAME"), Val(dtrow("QTY")), dtrow("UNIT"))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            Throw ex
            End Try
        End Sub

    End Class
