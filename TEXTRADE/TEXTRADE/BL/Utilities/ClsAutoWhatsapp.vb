Imports DB
Imports System.Data

Public Class ClsAUTOWHATSAPP
    Private objDBOperation As DBOperation
    Public alParaval As New ArrayList

#Region "Constructor"
    Public Sub New()
        Try
            objDBOperation = New DBOperation()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Functions"

    Public Function SAVE() As DataTable
        'Dim INTRESULT As Integer
        Dim DTTABLE As DataTable

        Try
            'save OPENING BILLS
            Dim strCommand As String = "SP_UTILITIES_AUTOWHATSAPP_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@TYPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MONDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TUESDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEDNESDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@THURSDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FRIDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SATURDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SUNDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TIME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@AGENTNAME", alParaval(I)))
                I += 1

            End With

            DTTABLE = objDBOperation.execute(strCommand, alParameter).Tables(0)

            Return DTTABLE

        Catch ex As Exception
            Throw ex
        End Try
        ' Return 0

    End Function
    Public Function UPDATE() As Integer
        Dim intResult As Integer
        Try
            'save SALE order
            Dim strCommand As String = "SP_UTILITIES_AUTOWHATSAPP_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0


                .Add(New SqlClient.SqlParameter("@TYPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MONDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TUESDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEDNESDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@THURSDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FRIDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SATURDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SUNDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TIME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1



                .Add(New SqlClient.SqlParameter("@TYPE", alParaval(I)))
                I += 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult

    End Function


    Public Function UPDATELEDGERS() As Integer
        Dim intResult As Integer
        Try
            'save SALE order
            Dim strCommand As String = "SP_UTILITIES_AUTOWHATSAPP_UPDATELEDGERS"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0


                .Add(New SqlClient.SqlParameter("@TYPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@AGENTNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1



                .Add(New SqlClient.SqlParameter("@TYPE", alParaval(I)))
                I += 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult

    End Function

#End Region
End Class
