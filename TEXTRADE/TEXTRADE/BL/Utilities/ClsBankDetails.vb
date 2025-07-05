Imports DB

Public Class ClsBankDetails
    Private objDBOperation As DBOperation
    Public alParaval As New ArrayList
    Public frmstring As String        'Used from Displaying Customer, Vendor, Employee Master

#Region "Constructor"
    Public Sub New()
        Try
            objDBOperation = New DBOperation
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Function"

    Public Function save() As Integer
        Dim intResult As Integer
        Dim strcommand As String = ""

        Try

            'save CategoryMaster
            strcommand = "SP_MASTER_CMPBANKDTLS_SAVE"

            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@BANKNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ACCNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@IFSC", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@UPI", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BANKNAME1", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ACCNO1", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@IFSC1", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@UPI1", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BANKNAME2", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ACCNO2", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@IFSC2", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@UPI2", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I += 1

            End With

            intResult = objDBOperation.executeNonQuery(strcommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function update() As Integer

        Dim intResult As Integer
        Dim strcommand As String = ""

        Try

            'Update AccountsMaster
            'If frmstring = "ACCOUNTS" Then
            strcommand = "SP_MASTER_CMPBANKDTLS_UPDATE"
            ' End If

            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@BANKNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ACCNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@IFSC", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@UPI", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BANKNAME1", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ACCNO1", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@IFSC1", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@UPI1", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BANKNAME2", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ACCNO2", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@IFSC2", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@UPI2", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I += 1
            End With

            intResult = objDBOperation.executeNonQuery(strcommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function GETCMPBANKDTLS(ByVal Cmpid As Integer) As DataTable
        Dim DT As DataTable
        Try

            'save CategoryMaster
            Dim strcommand As String = "SP_MASTER_SELECT_CMPBANKDTLS"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@cmpid", Cmpid))

            End With

            DT = objDBOperation.execute(strcommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return DT

    End Function

#End Region
End Class
