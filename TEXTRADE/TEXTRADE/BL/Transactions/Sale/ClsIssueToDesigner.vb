Imports DB

Public Class ClsIssueToDesigner

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
            Dim strCommand As String = "SP_TRANS_SALE_ISSUETODESIGNER_SAVE"
            Dim alParameter As New ArrayList
                With alParameter

                    Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@ISSNO", alParaval(I)))
                I = I + 1
                    .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTALQTY", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTALREJECTEDQTY", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTALSHIPPINGQTY", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTALUNITPRICE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTALBOXLOADED", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTALAMOUNT", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@GRIDTOTAL", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                    I = I + 1


                    .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                    I = I + 1

                    'grid parameters********************************

                    .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TYPE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@GRIDDATE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@SCARNO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@SCARDATE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@LEDGER", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@ITEMCODE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@PAPERGSM", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@ADIYAPO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@PONO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@BATCHNO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@ORDERDATE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@RECEIVEDATE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@ORDERQTY", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@DATEOFOCCURRENCE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@REASONFORSCAR", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@SCARSENTDATE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@SCARSENTTO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@REJECTEDQTY", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@CUSTOMERNO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@ITEMNUMBER", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@DESC", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@SHIPPING", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@UNITPRICE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@PERSONINVOLVED", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@BOXLOADED", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@NOTE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@AMOUNT", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTAL", alParaval(I)))
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
            Dim strCommand As String = "SP_TRANS_SALE_ISSUETODESIGNER_UPDATE"
            Dim alParameter As New ArrayList
                With alParameter

                    Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@ISSNO", alParaval(I)))
                I = I + 1
                    .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTALQTY", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTALREJECTEDQTY", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTALSHIPPINGQTY", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTALUNITPRICE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTALBOXLOADED", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTALAMOUNT", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@GRIDTOTAL", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                    I = I + 1


                    .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                    I = I + 1


                    'grid parameters********************************

                    .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TYPE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@GRIDDATE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@SCARNO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@SCARDATE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@LEDGER", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@ITEMCODE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@PAPERGSM", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@ADIYAPO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@PONO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@BATCHNO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@ORDERDATE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@RECEIVEDATE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@ORDERQTY", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@DATEOFOCCURRENCE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@REASONFORSCAR", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@SCARSENTDATE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@SCARSENTTO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@REJECTEDQTY", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@CUSTOMERNO", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@ITEMNUMBER", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@DESC", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@SHIPPING", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@UNITPRICE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@PERSONINVOLVED", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@BOXLOADED", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@NOTE", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@AMOUNT", alParaval(I)))
                    I = I + 1
                    .Add(New SqlClient.SqlParameter("@TOTAL", alParaval(I)))
                    I = I + 1


                    .Add(New SqlClient.SqlParameter("@TEMPMANUALNO", alParaval(I)))
                    I = I + 1
                End With

                intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

            Catch ex As Exception
                Throw ex
            End Try
            Return 0
        End Function

    Public Function SELECTISSNO() As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_SELECTISSUETODESIGNER_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@MANUALNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Cmpid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(I)))
                I += 1
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
            Dim strCommand As String = "SP_TRANS_SALE_ISSUETODESIGNER_DELETE"
            Dim alParameter As New ArrayList
                With alParameter
                    Dim I As Integer = 0
                    .Add(New SqlClient.SqlParameter("@ISSNO", alParaval(I)))
                    I += 1
                    .Add(New SqlClient.SqlParameter("@Cmpid", alParaval(I)))
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







