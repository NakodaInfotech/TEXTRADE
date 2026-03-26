Imports DB

Public Class ClsYarnLoomEfficiency

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
            Dim strCommand As String = "SP_TRANS_YARNLOOMEFFICIENCY_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@EFFNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ROUNDER", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@totalRECMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@totalWEFT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@remarks", alParaval(I)))
                I = I + 1

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



                'grid parameters
                .Add(New SqlClient.SqlParameter("@gridsrno", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOOMNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YARNQUALITY", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@BEAMNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RPM", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PICKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RECMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WEFT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WARP", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@EFFPER", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AVGPICK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDREMARKS", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@DONE", alParaval(I)))
                I = I + 1


            End With

            DT = objDBOperation.execute(strCommand, alParameter).Tables(0)

            Return DT
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function UPDATE() As Integer
        Dim intResult As Integer
        Try
            'Update SALE order
            Dim strCommand As String = "SP_TRANS_YARNLOOMEFFICIENCY_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@EFFNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ROUNDER", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@totalRECMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@totalWEFT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@remarks", alParaval(I)))
                I = I + 1

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



                'grid parameters
                .Add(New SqlClient.SqlParameter("@gridsrno", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOOMNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YARNQUALITY", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@BEAMNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RPM", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PICKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RECMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WEFT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WARP", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@EFFPER", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AVGPICK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDREMARKS", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@DONE", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@YLENO", alParaval(I)))
                I = I + 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function SELECTLOAN(ByVal TEMPloanNO As Integer, ByVal Cmpid As Integer, ByVal LocationID As Integer, ByVal YearID As Integer) As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_SELECTYARNLOOMEFFICIENCY_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@YLENO", TEMPloanNO))
                .Add(New SqlClient.SqlParameter("@CmpID", Cmpid))
                .Add(New SqlClient.SqlParameter("@LocationID", LocationID))
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
            Dim strCommand As String = "SP_TRANS_YARNLOOMEFFFICIENCY_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@YLENO", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@Cmpid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@LocationID", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(3)))
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(4)))


            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region



End Class
