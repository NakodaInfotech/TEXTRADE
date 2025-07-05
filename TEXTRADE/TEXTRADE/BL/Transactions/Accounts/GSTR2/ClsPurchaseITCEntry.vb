
Imports DB

Public Class ClsPurchaseITCEntry

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
        Dim DTTABLE As DataTable
        Try
            'save purchase order
            Dim strCommand As String = "SP_UTILITIES_PURCHASEITCENTRY_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MONTH", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALTAXABLEAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCGSTAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALSGSTAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALIGSTAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRANDTOTALBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALTAXABLEAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCGSTAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALSGSTAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALIGSTAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRANDTOTALPORTAL", alParaval(I)))
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
                .Add(New SqlClient.SqlParameter("@BILLNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TYPE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GSTIN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVNOBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVDATEBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVNOPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVDATEPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TAXABLEAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CGSTAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SGSTAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@IGSTAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GTOTALBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TAXABLEAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CGSTAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SGSTAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@IGSTAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GTOTALPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GSTRATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDREMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITCRECDLASTYEAR", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITCREVERSEDLASTYEAR", alParaval(I)))
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
            Dim strCommand As String = "SP_UTILITIES_PURCHASEITCENTRY_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@MONTH", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALTAXABLEAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCGSTAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALSGSTAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALIGSTAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRANDTOTALBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALTAXABLEAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALCGSTAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALSGSTAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALIGSTAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRANDTOTALPORTAL", alParaval(I)))
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
                .Add(New SqlClient.SqlParameter("@BILLNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TYPE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GSTIN", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVNOBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVDATEBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVNOPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INVDATEPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TAXABLEAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CGSTAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SGSTAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@IGSTAMTBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GTOTALBOOKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TAXABLEAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@CGSTAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SGSTAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@IGSTAMTPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GTOTALPORTAL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GSTRATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDREMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITCRECDLASTYEAR", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITCREVERSEDLASTYEAR", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@ENTRYNO", alParaval(I)))
                I = I + 1
            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function SELECTPURCHASEITCENTRY(ByVal RECONO As Integer, ByVal YearID As Integer) As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_SELECTPURCHASEITCENTRY_FOR_EDIT"
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
            Dim strCommand As String = "SP_UTILITIES_PURCHASEITCENTRY_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@ENTRYNO", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(1)))
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
