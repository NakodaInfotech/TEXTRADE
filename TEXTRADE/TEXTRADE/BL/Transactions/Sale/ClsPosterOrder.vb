Imports DB
Public Class ClsPosterOrder
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
        Try
            'save SALE order
            Dim strCommand As String = "SP_TRANS_POSTERORDER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@SRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DELIVERYAT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PARTYNAME", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@COL1", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COL2", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@COL3", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COL4", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COL5", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTAL12X18", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTAL12X24", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTAL8X10", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTAL6X8", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTAL4X6", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCOLUMN1", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCOLUMN2", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCOLUMN3", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCOLUMN4", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCOLUMN5", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PRINTTYPE", alParaval(I)))
                I = I + 1


                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(I)))
                I = I + 1

                'grid parameters
                .Add(New SqlClient.SqlParameter("@gridsrno", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESIGN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLOR", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESC", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@D12X18", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@D12X24", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@D8X10", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@D6X8", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@D4X6", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLUMN1", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLUMN2", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLUMN3", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLUMN4", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLUMN5", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DONE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CLOSED", alParaval(I)))
                I = I + 1


            End With

            Dim DT As DataTable = objDBOperation.execute(strCommand, alParameter).Tables(0)
            Return DT
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function UPDATE() As Integer
        Dim intResult As Integer
        Try
            'Update SALE order
            Dim strCommand As String = "SP_TRANS_POSTERORDER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@SRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DELIVERYAT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PARTYNAME", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@COL1", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COL2", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@COL3", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COL4", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COL5", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTAL12X18", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTAL12X24", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTAL8X10", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTAL6X8", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTAL4X6", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCOLUMN1", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCOLUMN2", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCOLUMN3", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCOLUMN4", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCOLUMN5", alParaval(I)))
                I = I + 1


                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PRINTTYPE", alParaval(I)))
                I = I + 1



                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(I)))
                I = I + 1

                'grid parameters
                .Add(New SqlClient.SqlParameter("@gridsrno", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESIGN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLOR", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESC", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@D12X18", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@D12X24", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@D8X10", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@D6X8", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@D4X6", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLUMN1", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLUMN2", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLUMN3", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLUMN4", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLUMN5", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DONE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CLOSED", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@TEMPPONO", alParaval(I)))
                I = I + 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function selectSo(ByVal SRNO As Integer, ByVal Cmpid As Integer, ByVal LocationID As Integer, ByVal YearID As Integer) As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_SELECTPOSTERORDER_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@SRno", SRNO))
                .Add(New SqlClient.SqlParameter("@CmpID", Cmpid))
                .Add(New SqlClient.SqlParameter("@YearID", YearID))
            End With
            dtTable = objDBOperation.execute(strCommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtTable
    End Function

    Public Function Delete() As Integer
        Dim intResult As Integer
        Try
            Dim strCommand As String = "SP_TRANS_POSTERORDER_DELETE"
            Dim alParameter As New ArrayList
            Dim I As Integer = 0
            With alParameter
                .Add(New SqlClient.SqlParameter("@SRno", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Cmpid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@UserId", alParaval(I)))
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
