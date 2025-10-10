
Imports DB

Public Class ClsNarrationMaster

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

    Public Function save() As Integer
        Dim intResult As Integer

        Try

            'save NARRATIONMaster
            Dim strCommand As String = "SP_MASTER_NARRATIONMASTER_SAVE"
            Dim alParameter As New ArrayList
            Dim narrationParam As New SqlClient.SqlParameter("@NARRATION", SqlDbType.NVarChar, 50)
            narrationParam.Value = alParaval(0)
            alParameter.Add(narrationParam)

            alParameter.Add(New SqlClient.SqlParameter("@remarks", alParaval(1)))

            alParameter.Add(New SqlClient.SqlParameter("@cmpid", alParaval(2)))
            alParameter.Add(New SqlClient.SqlParameter("@locationid", alParaval(3)))
            alParameter.Add(New SqlClient.SqlParameter("@userid", alParaval(4)))
            alParameter.Add(New SqlClient.SqlParameter("@yearid", alParaval(5)))
            alParameter.Add(New SqlClient.SqlParameter("@transfer", alParaval(6)))


            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function Update() As Integer
        Dim intResult As Integer

        Try

            'save NARRATIONMaster
            Dim strCommand As String = "SP_MASTER_NARRATIONMASTER_UPDATE"
            Dim alParameter As New ArrayList
            Dim narrationParam As New SqlClient.SqlParameter("@NARRATION", SqlDbType.NVarChar, 50)
            narrationParam.Value = alParaval(0)
            alParameter.Add(narrationParam)

            alParameter.Add(New SqlClient.SqlParameter("@remarks", alParaval(1)))

            alParameter.Add(New SqlClient.SqlParameter("@cmpid", alParaval(2)))
            alParameter.Add(New SqlClient.SqlParameter("@locationid", alParaval(3)))
            alParameter.Add(New SqlClient.SqlParameter("@userid", alParaval(4)))
            alParameter.Add(New SqlClient.SqlParameter("@yearid", alParaval(5)))
            alParameter.Add(New SqlClient.SqlParameter("@transfer", alParaval(6)))

            alParameter.Add(New SqlClient.SqlParameter("@NARRATIONID", alParaval(7)))


            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

#End Region

End Class
