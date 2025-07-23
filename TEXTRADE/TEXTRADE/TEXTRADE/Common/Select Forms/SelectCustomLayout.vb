Imports BL
Public Class SelectCustomLayout
    Public FORMNAMES As String = ""
    Public FILENAME As String = ""
    Public EDIT As Boolean          'used for editing
    Public FILES As String = ""
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
            Dim DTTABLE As DataTable
            Dim USERNAME As String = ""
            Dim CHK As String = ""
            Dim FORMNAME As String = ""
            Dim FILE As String = ""
            Dim FILENAMES As String = ""
            Dim alparaval As New ArrayList


            For I As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(I)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If USERNAME = "" Then

                        'CHK = dtrow("CHK")
                        USERNAME = dtrow("USERNAME")
                        FORMNAME = FORMNAMES
                        FILE = FILES
                        FILENAMES = FILENAME


                    Else

                        USERNAME = USERNAME & "|" & dtrow("USERNAME")
                        FORMNAME = FORMNAME & "|" & FORMNAMES
                        'CHK = CHK & "|" & dtrow("CHK")
                        FILE = FILE & "|" & FILES
                        FILENAMES = FILENAMES & "|" & FILENAME


                    End If
                End If
            Next
            alparaval.Add(USERNAME)
            alparaval.Add(FORMNAME)
            alparaval.Add(FILE)
            alparaval.Add(FILENAME)
            alparaval.Add(CmpId)
            alparaval.Add(YearId)

            Dim OBJMATCH As New CLSCUSTOMLAYOUT
            OBJMATCH.alParaval = alparaval

            If EDIT = False Then

                DTTABLE = OBJMATCH.SAVE()
                MessageBox.Show("Details Added")
                USERNAME = DTTABLE.Rows(0).Item(0)

            End If
            Me.Close()

        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
End Class