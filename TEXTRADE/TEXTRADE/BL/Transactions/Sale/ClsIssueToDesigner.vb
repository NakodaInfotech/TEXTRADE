Imports DB

Public Class ClsIssueToDesigner

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

#Region "Functions"
        Public Function SAVE() As DataTable
            Dim DT As DataTable
            Try
            'save SALE order
            Dim strCommand As String = "SP_TRANS_SALE_ISSUETODESIGNER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@ISSNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESIGNERNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1


                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1

                'grid parameters********************************

                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ORDERNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESIGN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SHADE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ORDERSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ORDERTYPE", alParaval(I)))
                I = I + 1

            End With

            DT = objDBOperation.execute(strCommand, alParameter).Tables(0)

            Catch ex As Exception
                Throw ex
            End Try
            Return DT

        End Function

        Public Function UPDATE() As Integer
            Dim intResult As Integer
            Try
            'Update SALE order
            Dim strCommand As String = "SP_TRANS_SALE_ISSUETODESIGNER_UPDATE"
            Dim alParameter As New ArrayList
                With alParameter

                    Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@ISSNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESIGNERNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1

                'grid parameters********************************

                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ORDERNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESIGN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SHADE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ORDERSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ORDERTYPE", alParaval(I)))
                I = I + 1


                .Add(New SqlClient.SqlParameter("@TEMPISSUNO", alParaval(I)))
                I = I + 1
                End With

                intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

            Catch ex As Exception
                Throw ex
            End Try
            Return 0
        End Function

    Public Function SELECTISSNO() As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_SELECTISSUETODESIGNER_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@ISSNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Cmpid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(I)))
                I += 1
            End With
            dtTable = objDBOperation.execute(strCommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtTable
    End Function

    Public Function DELETE() As Integer
            Dim intResult As Integer
            Try
            Dim strCommand As String = "SP_TRANS_SALE_ISSUETODESIGNER_DELETE"
            Dim alParameter As New ArrayList
                With alParameter
                    Dim I As Integer = 0
                    .Add(New SqlClient.SqlParameter("@ISSNO", alParaval(I)))
                    I += 1
                    .Add(New SqlClient.SqlParameter("@Cmpid", alParaval(I)))
                    I += 1
                    .Add(New SqlClient.SqlParameter("@YearID", alParaval(I)))
                    I += 1

                End With
                intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region





    End Class







