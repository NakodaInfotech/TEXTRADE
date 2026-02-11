Imports DB

Public Class ClsLoomMaster
    Private objDBOperation As DBOperation
    Public alParaval As New ArrayList


#Region "Constructor"
    Public Sub New()
        Try
            objDBOperation = New DBOperation
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Function"

    Public Function SAVE() As Integer
        Dim intResult As Integer

        Try

            Dim strCommand As String = "SP_MASTER_LOOMMASTER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALLOOMS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOOMNO", alParaval(I)))
                I += 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function


    Public Function UPDATE() As Integer
        Dim intResult As Integer

        Try

            Dim strCommand As String = "SP_MASTER_LOOMMASTER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALLOOMS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOOMNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOOMID", alParaval(I)))
                I += 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function


    Public Function DELETE() As Integer
        Try

            Dim strcommand As String = ""
            strcommand = "SP_MASTER_LOOM_DELETE"

            Dim alParameter As New ArrayList

            With alParameter
                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@LOOMID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1
            End With

            Dim INTES As Integer = objDBOperation.executeNonQuery(strcommand, alParameter)

            Return 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    'Select Name
    Public Function GETLOOM() As DataTable
        Try
            Dim dtTable As DataTable
            Dim strcommand As String = ""
            strcommand = "SP_SELECTLOOM_FOR_EDIT"

            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@LOOMID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1
            End With
            dtTable = objDBOperation.execute(strcommand, alParameter).Tables(0)

            Return dtTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function




#End Region


End Class
