Imports DB
Public Class ClsCustomLayout
    Private objDBOperation As DBOperation
    Public alParaval As New ArrayList
    Dim intResult As Integer

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

            'save itemdetails
            Dim strCommand As String = "SP_MASTER_CUSTOMLAYOUT_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@USERNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FORMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FILE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FILENAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I += 1


            End With

            DT = objDBOperation.execute(strCommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return DT

    End Function



    Public Function GETLAYOUT() As DataTable
        Try

            'save CategoryMaster
            Dim strcommand As String = "SP_MASTER_CUSTOMLAYOUT_SELECT"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@USERNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FORMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1

            End With

            Dim DT As DataTable = objDBOperation.execute(strcommand, alParameter).Tables(0)
            Return DT

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function selectLAYOUT(ByVal USERNAME As String, ByVal FORMNAME As String, ByVal Cmpid As Integer, ByVal YearID As Integer) As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_SELECTCUSTOMLAYOUT_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@USERNAME", USERNAME))
                .Add(New SqlClient.SqlParameter("@FORMNAME", FORMNAME))
                .Add(New SqlClient.SqlParameter("@CMPID", Cmpid))
                .Add(New SqlClient.SqlParameter("@YearID", YearID))
            End With
            dtTable = objDBOperation.execute(strCommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtTable
    End Function

#End Region
End Class
