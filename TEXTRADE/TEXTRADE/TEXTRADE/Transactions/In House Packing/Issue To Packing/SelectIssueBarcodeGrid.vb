Imports BL

Public Class SelectIssueBarcodeGrid


    Public WHERECLAUSE As String = ""
        Public DTBARCODE As New DataTable

        Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
            Try
                Me.Close()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    Private Sub SelectIssueBarcodeGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then
                gridbill.Focus()
                gridbill.FocusedColumn = gridbill.Columns("BALENO")
                gridbill.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
                gridbill.ShowEditor()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectIssueBarcodeGrid_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("CAST(0 AS BIT) AS CHK , ISSUEPACKING_DESC.ISS_BARCODE AS BARCODE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(ISSUEPACKING_DESC.ISS_BALENO, '') AS BALENO, ISNULL(ISSUEPACKING_DESC.ISS_PCS, 0) AS TOTALPCS, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(ISSUEPACKING_DESC.ISS_MTRS, 0) AS TOTALMTRS ", "", " ISSUEPACKING_DESC INNER JOIN ITEMMASTER ON ISSUEPACKING_DESC.ISS_ITEMID = ITEMMASTER.item_id INNER JOIN UNITMASTER ON ISSUEPACKING_DESC.ISS_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN COLORMASTER ON ISSUEPACKING_DESC.ISS_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ISSUEPACKING_DESC.ISS_DESIGNID = DESIGNMASTER.DESIGN_id ", " AND ROUND(ISS_MTRS-ISS_OUTMTRS,2) > 0  AND ISS_YEARID = " & YearId)
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If

        Catch ex As Exception
            Throw ex
            End Try
        End Sub

        Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try

            DTBARCODE.Columns.Add("BARCODE")

            gridbill.ClearColumnsFilter()

            Dim COUNT As Integer = 0
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DTBARCODE.Rows.Add(dtrow("BARCODE"))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            Throw ex
            End Try
        End Sub

    Private Sub SelectIssueBarcodeGrid_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            gridbill.Focus()
            gridbill.FocusedColumn = gridbill.Columns("BALENO")
            gridbill.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            gridbill.ShowEditor()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbill.KeyDown
            Try
                If gridbill.FocusedRowHandle >= 0 AndAlso (e.KeyCode = Keys.Space Or e.KeyCode = Keys.Enter) Then
                    Dim DTROW As DataRow = gridbill.GetFocusedDataRow
                    DTROW("CHK") = 1
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
    End Class
