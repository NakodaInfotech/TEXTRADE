Imports DB

Public Class ClsOpeningBeamStock
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

    Public Function SAVE() As DataTable
        Try
            Dim strcommand As String = "SP_MASTER_STOCKMASTERBEAM_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TYPE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GODOWN", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MILL", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BEAMNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BEAMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALENDS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GAMANO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SECTION", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ROLLNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BEAMWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BREAKAGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1

            End With

            Dim DT As DataTable = objDBOperation.execute(strcommand, alParameter).Tables(0)
            Return DT
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UPDATE() As Integer

        Dim intResult As Integer
        Dim strcommand As String = ""

        Try

            'Update AccountsMaster
            strcommand = "SP_MASTER_STOCKMASTERBEAM_UPDATE"

            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TYPE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GODOWN", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MILL", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BEAMNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BEAMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALENDS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GAMANO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SECTION", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ROLLNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BEAMWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BREAKAGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@OPBEAMSTOCKNO", alParaval(I)))
                I += 1
            End With

            intResult = objDBOperation.executeNonQuery(strcommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function DELETE() As DataTable
        Dim DTTABLE As DataTable
        Dim strcommand As String = ""

        Try

            'save CategoryMaster
            strcommand = "SP_MASTER_STOCKMASTERBEAM_DELETE"

            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@TEMP_NO", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(1)))


            End With

            DTTABLE = objDBOperation.execute(strcommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return DTTABLE

    End Function

    Public Function GETSTOCKBEAM() As DataTable
        Dim dtTable As DataTable
        Dim strcommand As String = ""
        Try
            strcommand = "SP_SELECTOPENINGSTOCK_BEAM_FOR_EDIT"

            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@OPBEAMSTOCKNO", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(1)))
            End With
            dtTable = objDBOperation.execute(strcommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtTable
    End Function

#End Region

End Class
