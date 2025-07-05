Imports DB

Public Class ClsUpdatePendingEntry
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
        Dim DTTABLE As New DataTable
        Try
            'save SALE order
            Dim strCommand As String = "SP_UTILITIES_UPDATEPENDINGENTRY_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@DATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REMARKS", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDSRNO", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ENTRYNO", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INITIALS", ALPARAVAL(I)))
                I = I + 1
               
                .Add(New SqlClient.SqlParameter("@Type", ALPARAVAL(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@CMPID", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@USERID", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YEARID", ALPARAVAL(I)))
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
            'Update purchase order
            Dim strCommand As String = "SP_UTILITIES_UPDATEPENDINGENTRY_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@DATE", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REMARKS", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDSRNO", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ENTRYNO", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INITIALS", ALPARAVAL(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@Type", ALPARAVAL(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@CMPID", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@USERID", ALPARAVAL(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YEARID", ALPARAVAL(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@PENDINGNO", ALPARAVAL(I)))
                I = I + 1
            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function SELECTPENDINGENTRY(ByVal ENTRYNO As Integer, ByVal CMPID As Integer, ByVal YearID As Integer) As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_SELECTUPDATEPENDINGENTRY_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@PENDINGNO", ENTRYNO))
                .Add(New SqlClient.SqlParameter("@YearID", YearID))
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
            Dim strCommand As String = "SP_UTILITIES_UPDATEPENDINGENTRY_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@PENDINGNO", ALPARAVAL(0)))
                .Add(New SqlClient.SqlParameter("@YearID", ALPARAVAL(1)))

            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
