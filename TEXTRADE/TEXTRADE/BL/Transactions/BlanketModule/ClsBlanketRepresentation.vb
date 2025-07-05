Imports DB

Public Class ClsBlanketRepresentation
    Private objDBOperation As DBOperation
    Public alParaval As New ArrayList
    Dim intResult As Integer

#Region "Constructor"
    Public Sub New()
        Try
            objDBOperation = New DBOperation
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Functions"

    Public Function SAVE() As DataTable

        Try

            'save itemdetails
            Dim strCommand As String = "SP_TRANS_BLANKETREPRESNT_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@BLANKETNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PARTYNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@AGENTNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SALESMAN", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BLANKETSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BLANKETNAME", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BLANKETDETSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BLANKETDESIGN", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BLANKETGRIDREMARKS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BLANKETAPPROVAL", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MAINSRNO", alParaval(I)))
                I += 1



            End With

            Dim DT As DataTable = objDBOperation.execute(strCommand, alParameter).Tables(0)
            Return DT

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function UPDATE() As Integer
        Dim strcommand As String = ""
        Try
            'Update AccountsMaster
            strcommand = "SP_TRANS_BLANKETREPRESNT_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@BLANKETNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PARTYNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@AGENTNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SALESMAN", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BLANKETSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BLANKETNAME", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BLANKETDETSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BLANKETDESIGN", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BLANKETGRIDREMARKS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BLANKETAPPROVAL", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MAINSRNO", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@TEMPBLANKETREPNO", alParaval(I)))
                I = I + 1

            End With

            intResult = objDBOperation.executeNonQuery(strcommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function SELECTBLANKETREPRESENT() As DataTable
        Try
            Dim strCommand As String = "SP_SELECTBLANKETREPRESENT_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@BLANKETNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Cmpid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(I)))
                I += 1
            End With
            Dim dtTable As DataTable = objDBOperation.execute(strCommand, alParameter).Tables(0)
            Return dtTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DELETE() As Integer
        Try
            Dim strCommand As String = "SP_TRANS_BLANKETREPRESNT_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@BLANKETNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(1)))
                I += 1
            End With
            Dim intResult As Integer = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class
