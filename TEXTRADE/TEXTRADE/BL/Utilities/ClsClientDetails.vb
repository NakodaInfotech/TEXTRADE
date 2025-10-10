Imports DB

Public Class ClsClientDetails
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
            'SAVE CLIENT DETAILS 
            Dim strCommand As String = "SP_UTILITIES_CLIENTDETAILS_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0


                .Add(New SqlClient.SqlParameter("@GRIDSRNO", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PARTYNAME", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CLIENTNAME", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROJECTNAME", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AMCDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@EWAYDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@EINVOICEDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WHATSAPPDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOCATION", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MOBILEDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MOBILELIC", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@cmpid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@locationid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@transfer", ALPARAVAL(I)))
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
            Dim strCommand As String = "SP_UTILITIES_CLIENTDETAILS_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0


                .Add(New SqlClient.SqlParameter("@GRIDSRNO", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PARTYNAME", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CLIENTNAME", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROJECTNAME", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AMCDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@EWAYDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@EINVOICEDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WHATSAPPDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOCATION", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MOBILEDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MOBILELIC", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@cmpid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@locationid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@transfer", ALPARAVAL(I)))
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
            Dim strCommand As String = "SP_UTILITIES_CLIENTDETAILS_DELETE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@SBNO", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDSRNO", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PARTYNAME", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CLIENTNAME", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROJECTNAME", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AMCDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@EWAYDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@EINVOICEDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WHATSAPPDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOCATION", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MOBILEDATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MOBILELIC", ALPARAVAL(I)))
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
