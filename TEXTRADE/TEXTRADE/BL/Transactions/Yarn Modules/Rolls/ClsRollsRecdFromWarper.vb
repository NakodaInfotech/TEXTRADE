Imports DB

Public Class ClsRollsRecdFromWarper

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
        Dim DTTABLE As DataTable
        Try
            'save purchase order
            Dim strCommand As String = "SP_TRANS_ROLLSRECEIVED_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@ROLLRECDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GODOWN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CHALLANNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CHALLANDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WARPINGNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGRAMNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGRAMSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGRAMDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ENDS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALENDS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LENGTH", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CUT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COUNT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LONGATION", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TAPLINE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALROLLS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALWT", alParaval(I)))
                I = I + 1


                .Add(New SqlClient.SqlParameter("@TOTALCONES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALGROSSWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALNETTWT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@FRESH", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FRESHWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FRESHNETT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WINDING", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WINDINGWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WINDINGNETT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FIRKA", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FIRKAWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FIRKANETT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@RETFRESH", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETFRESHWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETFRESHNETT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETWINDING", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETWINDINGWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETWINDINGNETT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETFIRKA", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETFIRKAWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETFIRKANETT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WINDINGMILL", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1


                'grid parameters


                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@QUALITY", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MILLNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CONES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GROSSWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NETTWT", alParaval(I)))
                I = I + 1

            End With

            DTTABLE = objDBOperation.execute(strCommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return DTTABLE

    End Function

    Public Function UPDATE() As Integer
        Dim intResult As Integer
        Try
            'Update purchase order
            Dim strCommand As String = "SP_TRANS_ROLLSRECEIVED_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@ROLLRECDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GODOWN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CHALLANNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CHALLANDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WARPINGNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGRAMNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGRAMSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PROGRAMDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ENDS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALENDS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LENGTH", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CUT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@COUNT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LONGATION", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TAPLINE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALROLLS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALWT", alParaval(I)))
                I = I + 1


                .Add(New SqlClient.SqlParameter("@TOTALCONES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALGROSSWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALNETTWT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@FRESH", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FRESHWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FRESHNETT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WINDING", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WINDINGWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WINDINGNETT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FIRKA", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FIRKAWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FIRKANETT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@RETFRESH", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETFRESHWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETFRESHNETT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETWINDING", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETWINDINGWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETWINDINGNETT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETFIRKA", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETFIRKAWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RETFIRKANETT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WINDINGMILL", alParaval(I)))
                I = I + 1


                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1


                'grid parameters


                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@QUALITY", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MILLNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CONES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GROSSWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NETTWT", alParaval(I)))
                I = I + 1


                .Add(New SqlClient.SqlParameter("@ROLLSRECNO", alParaval(I)))
                I = I + 1
            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function SELECTROLLS() As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_SELECTROLLSRECEIVED_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@ROLLSRECNO", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(1)))
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
            Dim strCommand As String = "SP_TRANS_ROLLSRECEIVED_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@ROLLSRECNO", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(1)))
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SAVEUPLOAD() As Integer
        Dim INTRESULT As Integer
        Try
            Dim STRCOMMAND As String = "SP_TRANS_ROLLSRECEIVED_SAVEUPLOAD"
            Dim ALPARAMATER As New ArrayList
            With ALPARAMATER
                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@ROLLSRECNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@IMGPATH", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I = I + 1
            End With

            INTRESULT = objDBOperation.executeNonQuery(STRCOMMAND, ALPARAMATER)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
