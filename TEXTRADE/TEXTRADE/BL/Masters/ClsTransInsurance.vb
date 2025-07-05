Imports DB
Imports System.Data

Public Class ClsTransInsurance
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
            Dim strCommand As String = "SP_MASTER_TRANSINSURANCE_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(I)))

                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INSURERNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@POLICYNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LEDGER", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PERCENT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CALCON", alParaval(I)))
                I = I + 1


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
            Dim strCommand As String = "SP_MASTER_TRANSINSURANCE_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(I)))

                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INSURERNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@POLICYNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LEDGER", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PERCENT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CALCON", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@TINO", alParaval(I)))
                I = I + 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult

    End Function
    Public Function DELETE() As Integer
        Dim INTRESULT As Integer
        Try
            'save OPENING BILLS
            Dim strCommand As String = "SP_MASTER_TRANSINSURANCE_DELETE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@TINO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1

            End With

            INTRESULT = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function


#End Region
End Class
