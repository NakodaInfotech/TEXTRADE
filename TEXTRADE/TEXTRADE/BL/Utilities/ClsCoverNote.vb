Imports DB
Public Class ClsCoverNote
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
            Dim strCommand As String = "SP_UTILITIES_COVERNOTE_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                '.Add(New SqlClient.SqlParameter("@COVERNO", alParaval(I)))
                'I = I + 1
                .Add(New SqlClient.SqlParameter("@COVERDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AGENT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1


                'grid parameters
                .Add(New SqlClient.SqlParameter("@SRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REGNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVINITIALS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PRINTINITIALS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PARTYNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AGENTNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LRDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TRANSPORT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALPCS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRANDTOTAL", alParaval(I)))
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
            Dim strCommand As String = "SP_UTILITIES_COVERNOTE_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                '.Add(New SqlClient.SqlParameter("@COVERNO", alParaval(I)))
                'I = I + 1
                .Add(New SqlClient.SqlParameter("@COVERDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AGENT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1


                'grid parameters
                .Add(New SqlClient.SqlParameter("@SRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REGNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVINITIALS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PRINTINITIALS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PARTYNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AGENTNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LRDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TRANSPORT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALPCS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRANDTOTAL", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@TEMPCOVERNO", alParaval(I)))
                I = I + 1


            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    'Public Function SELECTNOTE(ByVal COVERNO As Integer, ByVal Cmpid As Integer, ByVal YearID As Integer) As DataTable
    '    Dim dtTable As DataTable
    '    Try

    '        Dim strCommand As String = "SP_SELECTCOVERNOTE_FOR_EDIT"
    '        Dim alParameter As New ArrayList
    '        With alParameter
    '            .Add(New SqlClient.SqlParameter("@COVERNO", COVERNO))
    '            .Add(New SqlClient.SqlParameter("@CmpID", Cmpid))
    '            .Add(New SqlClient.SqlParameter("@YearID", YearID))

    '        End With
    '        dtTable = objDBOperation.execute(strCommand, alParameter).Tables(0)

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    '    Return dtTable
    'End Function

    'Public Function DELETE() As Integer
    '    Dim intResult As Integer
    '    Try
    '        Dim strCommand As String = "SP_UTILITIES_COVERNOTE_DELETE"
    '        Dim alParameter As New ArrayList
    '        With alParameter
    '            .Add(New SqlClient.SqlParameter("@COVERNO", alParaval(0)))
    '            .Add(New SqlClient.SqlParameter("@Cmpid", alParaval(1)))
    '            .Add(New SqlClient.SqlParameter("@UserId", alParaval(2)))
    '            .Add(New SqlClient.SqlParameter("@YearID", alParaval(3)))


    '        End With
    '        intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Function SELECTNOTE() As DataTable
        Dim DT As DataTable
        Try

            Dim strCommand As String = "SP_SELECTCOVERNOTE_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@TEMPCOVERNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I = I + 1
            End With
            DT = objDBOperation.execute(strCommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return DT
    End Function

    Public Function DELETE() As Integer
        Dim intResult As Integer
        Try
            Dim strCommand As String = "SP_UTILITIES_COVERNOTE_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@COVERNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I = I + 1
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
