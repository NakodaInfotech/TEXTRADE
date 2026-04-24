Imports DB


Public Class ClsYarnStockAdjustment

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
            Dim strCommand As String = "SP_TRANS_YARNSTOCKADJUSTMENT_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@RECONO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GODOWN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TRANSNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALINBAGS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALINWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALINCONES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALOUTBAGS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALOUTWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALOUTCONES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1



                'grid parameters
                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YARNQUALITY", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MILLNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESIGN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PARTYLOTNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PARTYCOLOR", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SHADE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESC", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@BAGS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CONES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RACK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PER", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AMOUNT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@BARCODE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FROMNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FROMSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FROMTYPE", alParaval(I)))
                I = I + 1



                'INGRID PARAMETERS
                .Add(New SqlClient.SqlParameter("@INGRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INYARNQUALITY", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INMILLNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INDESIGN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INPARTYLOTNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INPARTYCOLOR", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INSHADE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INLOTNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INDESC", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INBAGS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INCONES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INLRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INRACK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INRATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INPER", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INAMOUNT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INBARCODE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DONE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@OUTBAGS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@OUTWT", alParaval(I)))
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
            Dim strCommand As String = "SP_TRANS_YARNSTOCKADJUSTMENT_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@RECONO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GODOWN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TRANSNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALINBAGS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALINWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALINCONES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALOUTBAGS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALOUTWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALOUTCONES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1



                'grid parameters
                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@YARNQUALITY", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MILLNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESIGN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PARTYLOTNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PARTYCOLOR", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SHADE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LOTNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESC", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@BAGS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CONES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RACK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PER", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AMOUNT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@BARCODE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FROMNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FROMSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FROMTYPE", alParaval(I)))
                I = I + 1



                'INGRID PARAMETERS
                .Add(New SqlClient.SqlParameter("@INGRIDSRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INYARNQUALITY", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INMILLNAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INDESIGN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INPARTYLOTNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INPARTYCOLOR", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INSHADE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INLOTNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INDESC", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INBAGS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INCONES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INLRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INRACK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INRATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INPER", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INAMOUNT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INBARCODE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DONE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@OUTBAGS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@OUTWT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@SANO", alParaval(I)))
                I = I + 1
            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function SELECTYARNSTOCKADJUSTMENT(ByVal RECONO As Integer, ByVal Cmpid As Integer, ByVal LocationID As Integer, ByVal YearID As Integer) As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_SELECTYARNSTOCKADJUSTMENT_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@RECONO", RECONO))
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

    Public Function DELETE() As Integer
        Dim intResult As Integer
        Try
            Dim strCommand As String = "SP_TRANS_YARNSTOCKADJUSTMENT_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@RECONO", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@Cmpid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@LocationID", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@userID", alParaval(3)))
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(4)))

            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class



