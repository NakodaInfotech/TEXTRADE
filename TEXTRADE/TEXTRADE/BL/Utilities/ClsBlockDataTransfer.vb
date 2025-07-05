Imports DB
Public Class ClsBlockDataTransfer

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

    Public Function SAVE() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_MASTER_BLOCKDATATRANSFER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@LEDGER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@OTHER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DATA", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@STOCK", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1


            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function
    Public Function DELETE() As Integer
        Dim intResult As Integer
        Try
            Dim strCommand As String = "SP_MASTER_BLOCKDATATRANSFER_DELETE"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(1)))


            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


End Class
