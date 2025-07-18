Imports BL
Public Class SelectCustomLayout
    Public FORMNAME As String = ""
    Public FILE As String = ""
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

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try

            'Dim USERNAME As String = ""
            'Dim CHK As String = ""
            'Dim FORMNAME As String = FORMNAME
            'Dim FILE As String = FILE


            'For I As Integer = 0 To gridbill.RowCount - 1
            '    Dim dtrow As DataRow = gridbill.GetDataRow(I)
            '    If Convert.ToBoolean(dtrow("CHK")) = True Then
            '        If Name = "" Then

            '            CHK = dtrow("CHK")
            '            USERNAME = dtrow("USERNAME")
            '            USERNAME = dtrow("USERNAME")


            '        Else

            '            GRIDSRNO = GRIDSRNO & "|" & Val(dtrow("SRNO"))
            '            CHK = CHK & "|" & dtrow("CHK")
            '            LRNO = LRNO & "|" & Val(dtrow("LRNO"))
            '            LOTNO = LOTNO & "|" & dtrow("LOTNO")
            '            LOTDATE = LOTDATE & "|" & Format(dtrow("DATE"), "MM/dd/yyyy")
            '            ITEMNAME = ITEMNAME & "|" & dtrow("ITEMNAME")
            '            TOTALMTRS = TOTALMTRS & "|" & Val(dtrow("TOTALMTRS"))
            '            BALMTRS = BALMTRS & "|" & Val(dtrow("BALMTRS"))
            '            ADJMTRS = ADJMTRS & "|" & Val(dtrow("ADJMTRS"))
            '            FROMNO = FROMNO & "|" & Val(dtrow("FROMNO"))
            '            FROMTYPE = FROMTYPE & "|" & dtrow("FROMTYPE")

            '        End If
            '    End If
            'Next
            'alparaval.Add(GRIDSRNO)
            'alparaval.Add(CHK)
            'alparaval.Add(LRNO)
        Catch ex As Exception
            Throw ex

        End Try
    End Sub
End Class