Imports BL
Public Class SelectCustomLayout
    Private Sub SelectCustomLayout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLGRID()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            'Dim DTUSERS As DataTable = OBJCMN.SEARCH("USER_NAME", "", "USERMASTER", "AND User_yearid=" & YearId & " ")

            DT = OBJCMN.SEARCH(" CAST(0 AS BIT) AS CHK,USER_NAME AS USERNAME", "", "USERMASTER", " AND User_yearid=" & YearId)

            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class