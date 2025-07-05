Imports DB
Public Class ClsLotTagging

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
        Dim DT As DataTable
        Try
            'save PAYMENT MASTER
            Dim strCommand As String = "SP_UTILITIES_LOTTAGGING_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@MANUALLOTTAGNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTTAGDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTTOTALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTTOTALBALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTTOTALADJMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGTOTALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGTOTALBALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGTOTALADJMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CHK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@BALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ADJMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FROMNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FROMTYPE", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@PSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PCHK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PITEMNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESIGNNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLOR", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PBALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PADJMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PFROMNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PFROMSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PFROMTYPE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PENDINGLOT", alParaval(I)))
                I = I + 1

            End With

            DT = objDBOperation.execute(strCommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return DT

    End Function

    Public Function SELECTBILL(ByVal MATCHNO As Integer, ByVal YEARID As Integer) As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_SELECT_LOTTAGGING_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@LOTTAGNO", MATCHNO))
                .Add(New SqlClient.SqlParameter("@YEARID", YEARID))
            End With
            dtTable = objDBOperation.execute(strCommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtTable
    End Function

    Public Function UPDATE() As Integer
        Dim intResult As Integer
        Try
            'update purchase Requisition
            Dim strCommand As String = "SP_UTILITIES_LOTTAGGING_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@MANUALLOTTAGNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTTAGDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTTOTALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTTOTALBALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTTOTALADJMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGTOTALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGTOTALBALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGTOTALADJMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CHK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@BALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ADJMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FROMNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FROMTYPE", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@PSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PCHK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PITEMNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESIGNNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COLOR", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PBALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PADJMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PFROMNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PFROMSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PFROMTYPE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PENDINGLOT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@LOTTAGNO", alParaval(I)))
                I = I + 1

            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function


    Public Function DELETE() As Integer
        Try
            Dim strcommand As String = "SP_UTILITIES_LOTTAGGING_DELETE"
            Dim alParameter As New ArrayList
            Dim I As Integer = 0
            With alParameter
                .Add(New SqlClient.SqlParameter("@LOTTAGNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(I)))
                I += 1
            End With
            Dim INTRES As Integer = objDBOperation.executeNonQuery(strcommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

#End Region


End Class
