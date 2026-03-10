Imports DB

Public Class ClsJobOrder
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
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DESIGNNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REED", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REEDSPACE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PICKS", alParaval(I)))
                I += 1



                ' Reference and names
                .Add(New SqlClient.SqlParameter("@REFNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I += 1

                '.Add(New SqlClient.SqlParameter("@ORDERNO", alParaval(I)))
                'I += 1

                ' Other details
                .Add(New SqlClient.SqlParameter("@TOTALMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALENDS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I += 1
                'warp gridmatching data serializations
                .Add(New SqlClient.SqlParameter("@WARPGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPGRIDSYM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPYARNQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPDENIER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPMILLNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPCONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPCOST", alParaval(I)))
                I += 1

                ' Weft Grid data serialization
                .Add(New SqlClient.SqlParameter("@WEFTGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTGRIDSYM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTYARNQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTDENIER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTMILLNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTCONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTCOST", alParaval(I)))
                I += 1

                ' Company and user details
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOCATIONID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1
                ' Additional flags or reserved parameter
                .Add(New SqlClient.SqlParameter("@TRANSFER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@OUTMTRS", alParaval(I)))
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
                .Add(New SqlClient.SqlParameter("@JOBNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DESIGNNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REED", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REEDSPACE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PICKS", alParaval(I)))
                I += 1



                ' Reference and names
                .Add(New SqlClient.SqlParameter("@REFNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I += 1

                '.Add(New SqlClient.SqlParameter("@ORDERNO", alParaval(I)))
                'I += 1

                ' Other details
                .Add(New SqlClient.SqlParameter("@TOTALMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALENDS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I += 1
                'warp gridmatching data serializations
                .Add(New SqlClient.SqlParameter("@WARPGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPGRIDSYM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPYARNQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPDENIER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPMILLNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPCONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPCOST", alParaval(I)))
                I += 1

                ' Weft Grid data serialization
                .Add(New SqlClient.SqlParameter("@WEFTGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTGRIDSYM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTYARNQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTDENIER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTMILLNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTCONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTCOST", alParaval(I)))
                I += 1

                ' Company and user details
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOCATIONID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1
                ' Additional flags or reserved parameter
                .Add(New SqlClient.SqlParameter("@TRANSFER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@OUTMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TEMPDESIGNNO", alParaval(I))) ' Adjust if needed
                I += 1



            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function Delete() As Integer
        Dim intResult As Integer
        Try
            Dim strCommand As String = "SP_TRANS_YARNJOBORDER_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@JOBNO", alParaval(0))) ' Or correct index
                .Add(New SqlClient.SqlParameter("@CmpId", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@LocationId", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@YearId", alParaval(3)))
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
            'Dim DT As DataTable = objDBOperation.execute(strCommand, alParameter).Tables(0)
            'Return DT
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function SelectDesignCard(ByVal designNo As String, ByVal Itemname As String, ByVal cmpId As Integer, ByVal locationId As Integer, ByVal yearId As Integer) As DataTable
    '    Try
    '        Dim strCommand As String = "SP_SELECT_DESIGN_CARD_FOR_EDIT"
    '        Dim alParameter As New ArrayList
    '        With alParameter
    '            .Add(New SqlClient.SqlParameter("@DesignNo", designNo))
    '            .Add(New SqlClient.SqlParameter("@Itemname", Itemname))
    '            .Add(New SqlClient.SqlParameter("@CmpId", cmpId))
    '            .Add(New SqlClient.SqlParameter("@LocationId", locationId))
    '            .Add(New SqlClient.SqlParameter("@YearId", yearId))
    '        End With
    '        Dim dtTable As DataTable = objDBOperation.execute(strCommand, alParameter).Tables(0)
    '        Return dtTable
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    'Public Function SelectYarnJob(ByVal CARDNO As String, ByVal yearId As Integer) As DataTable
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
