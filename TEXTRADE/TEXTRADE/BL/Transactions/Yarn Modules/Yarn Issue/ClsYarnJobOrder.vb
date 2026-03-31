Imports DB

Public Class ClsYarnJobOrder
    Private objDBOperation As DBOperation
    Public alParaval As New ArrayList
    Dim intResult As Integer

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
    Public Function SAVE() As Integer
        Try
            Dim strCommand As String = "SP_TRANS_YARNJOBORDER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                ' Add parameters in the exact order of alParaval
                .Add(New SqlClient.SqlParameter("@JOBNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PONO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PARTYNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1


                .Add(New SqlClient.SqlParameter("@SRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DESIGN", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@OTHERITEMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REFNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REED", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PICKS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ENDS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DESCRIPTION", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@OUTMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DONE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CLOSED", alParaval(I)))
                I += 1


            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function UPDATE() As Integer
        Dim intResult As Integer

        Try
            Dim strCommand As String = "SP_TRANS_YARNJOBORDER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                ' Add parameters in the exact order of alParaval
                .Add(New SqlClient.SqlParameter("@JOBNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PONO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PARTYNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1


                .Add(New SqlClient.SqlParameter("@SRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DESIGN", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@OTHERITEMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REFNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REED", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PICKS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ENDS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DESCRIPTION", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@OUTMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DONE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CLOSED", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@TEMPJOBNO", alParaval(I))) ' Adjust if needed
                I += 1



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
            Dim strCommand As String = "SP_TRANS_YARNJOBORDER_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@JOBNO", alParaval(0))) ' Or correct index
                .Add(New SqlClient.SqlParameter("@YearId", alParaval(1)))
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SelectYarnJob(ByVal JOBNO As String, ByVal YearId As Integer) As DataTable
        Dim dtTable As DataTable

        Try
            Dim strCommand As String = "SP_SELECTYARNJOBORDER_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@JOBNO", JOBNO))
                .Add(New SqlClient.SqlParameter("@YearId", YearId))
            End With
            dtTable = objDBOperation.execute(strCommand, alParameter).Tables(0)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtTable

    End Function
#End Region
End Class
