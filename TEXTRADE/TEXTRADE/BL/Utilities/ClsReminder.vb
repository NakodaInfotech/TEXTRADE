

Imports DB

Public Class ClsReminder
    Private objDBOperation As DBOperation
    Public ALPARAVAL As New ArrayList
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
        Dim DTTABLE As DataTable
        Try
            'SAVE SAMPLE BARCODE 
            Dim strCommand As String = "SP_UTILITIES_REMINDER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0


                .Add(New SqlClient.SqlParameter("@GRIDSRNO", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NARRATION", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ASSIGNUSER", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CREATEDBY", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DONE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@cmpid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@locationid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", ALPARAVAL(I)))
                I = I + 1


            End With

            DTTABLE = objDBOperation.execute(strCommand, alParameter).Tables(0)

            Return DTTABLE
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function UPDATE() As Integer
        Dim intResult As Integer
        Try
            'save SALE order
            Dim strCommand As String = "SP_UTILITIES_REMINDER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0


                .Add(New SqlClient.SqlParameter("@GRIDSRNO", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NARRATION", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ASSIGNUSER", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CREATEDBY", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DONE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@cmpid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@locationid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", ALPARAVAL(I)))
                I = I + 1


                .Add(New SqlClient.SqlParameter("@RNO", ALPARAVAL(I)))
                I = I + 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult

    End Function

    Public Function DELETE() As Integer
        Dim intResult As Integer
        Try
            'save SALE order
            Dim strCommand As String = "SP_UTILITIES_REMINDER_DELETE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@RNO", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@cmpid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@locationid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", ALPARAVAL(I)))
                I = I + 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult

    End Function

#End Region
End Class
