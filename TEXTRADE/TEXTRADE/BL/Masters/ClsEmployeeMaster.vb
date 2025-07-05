
Imports DB

Public Class ClsEmployeeMaster

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

    Public Function SAVE() As Integer
        Dim intResult As Integer
        Dim strcommand As String = ""

        Try

            'save CategoryMaster
            strcommand = "SP_MASTER_EMPLOYEEMASTER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@EMPNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LEDGERNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOANLEDGERNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SALARY", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@DEPARTMENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DESIGNATION", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@AREA", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PINCODE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@STATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@COUNTRY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RESINO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WHATSAPPNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ALTNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MOBILENO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@EMAIL", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PANNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PFNO", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BANK", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ACCTYPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ACCOUNTNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@IFSC", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BRANCH", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BANKCITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@UPI", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@ADDRESS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PHOTO", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@EARGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@EARNINGS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@EARAMT", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@DEDGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DEDUCTION", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DEDAMT", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@TOTALEARNINGS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALDEDUCTION", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1

            End With

            intResult = objDBOperation.executeNonQuery(strcommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function UPDATE() As Integer

        Dim intResult As Integer
        Dim strcommand As String = ""

        Try

            'Update AccountsMaster
            strcommand = "SP_MASTER_EMPLOYEEMASTER_UPDATE"

            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@EMPNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LEDGERNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOANLEDGERNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SALARY", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@DEPARTMENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DESIGNATION", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@AREA", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PINCODE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@STATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@COUNTRY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RESINO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WHATSAPPNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ALTNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MOBILENO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@EMAIL", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PANNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PFNO", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@BANK", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ACCTYPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ACCOUNTNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@IFSC", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BRANCH", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BANKCITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@UPI", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@ADDRESS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REMARKS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PHOTO", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@EARGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@EARNINGS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@EARAMT", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@DEDGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DEDUCTION", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DEDAMT", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@TOTALEARNINGS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALDEDUCTION", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@EMPID", alParaval(I)))
                I += 1

            End With

            intResult = objDBOperation.executeNonQuery(strcommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function DELETE() As DataTable
        Dim DTTABLE As DataTable
        Dim strcommand As String = ""

        Try

            'save CategoryMaster
            strcommand = "SP_MASTER_EMPLOYEEMASTER_DELETE"

            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@EMPNAME", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(2)))

            End With

            DTTABLE = objDBOperation.execute(strcommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return DTTABLE

    End Function

    Public Function GETEMPLOYEE() As DataTable
        Dim dtTable As DataTable
        Dim strcommand As String = ""
        Try
            strcommand = "SP_MASTER_SELECT_EMPLOYEE"

            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(1)))
            End With
            dtTable = objDBOperation.execute(strcommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtTable
    End Function

#End Region

End Class
