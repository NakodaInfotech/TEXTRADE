Imports DB

Public Class ClsUpdateYarnRackShelf
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
            Dim strCommand As String = "SP_UTILITIES_UPDATEYARNRACKSHELF_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RACK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SHELF", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALPCS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I = I + 1


                'grid parameters
                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@YARNQUALITY", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@DESIGN", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@COLOR", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@GODOWN", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BAGS", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@CONES", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@WT", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@CATEGORY", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@LRNO", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@MILLNAME", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BILLNO", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BARCODE", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@JOBBERNAME", alParaval(I)))
                I += 1
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
            Dim strCommand As String = "SP_UTILITIES_UPDATEYARNRACKSHELF_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RACK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SHELF", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALPCS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALMTRS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I = I + 1


                'grid parameters
                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@YARNQUALITY", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@DESIGN", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@COLOR", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@GODOWN", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BAGS", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@CONES", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@WT", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@CATEGORY", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@LRNO", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@MILLNAME", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BILLNO", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BARCODE", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@JOBBERNAME", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@ENTRYNO", alParaval(I)))
                I = I + 1
            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function SELECTUPDATEYARNRACKSHELF(ByVal RECONO As Integer, ByVal YearID As Integer) As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_SELECTUPDATEYARNRACKSHELF_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@ENTRYNO", RECONO))
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
            Dim strCommand As String = "SP_UTILITIES_UPDATEYARNRACKSHELF_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@ENTRYNO", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(3)))
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
