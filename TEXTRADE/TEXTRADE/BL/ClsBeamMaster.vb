Imports DB
Imports System.Data.SqlClient

Public Class ClsBeamMaster

    Private objDBOperation As DBOperation
    Public alparaval As New ArrayList

    Public Sub New()
        objDBOperation = New DBOperation()
    End Sub

#Region "SAVE"

    Public Function SAVE() As Integer
        Try
            Dim strCommand As String = "SP_MASTER_BEAMMASTER_SAVE"
            Dim alParameter As New ArrayList
            Dim I As Integer = 0

            alParameter.Add(New SqlParameter("@BEAM_NAME", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@HSNCODE", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@BEAM_TL", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@WT", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@TOTALENDS", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@TOTALWT", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@CMPID", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@USERID", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@YEARID", alparaval(I)))
            I += 1

            'grid value
            alParameter.Add(New SqlParameter("@SRNO", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@GRIDQUALITY", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@SHADE", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@GRIDENDS", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@GRIDWT", alparaval(I)))
            I += 1

            Return objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region

#Region "UPDATE"

    Public Function UPDATE() As Integer
        Try
            Dim strCommand As String = "SP_MASTER_BEAMMASTER_UPDATE"
            Dim alParameter As New ArrayList
            Dim I As Integer = 0

            alParameter.Add(New SqlParameter("@BEAM_NAME", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@HSNCODE", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@BEAM_TL", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@WT", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@TOTALENDS", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@TOTALWT", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@CMPID", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@USERID", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@YEARID", alparaval(I)))
            I += 1

            'grid value
            alParameter.Add(New SqlParameter("@SRNO", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@GRIDQUALITY", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@SHADE", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@GRIDENDS", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@GRIDWT", alparaval(I)))
            I += 1

            Return objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region

#Region "DELETE"

    Public Function DELETE() As DataTable
        Try
            Dim strCommand As String = "SP_MASTER_BEAMMASTER_DELETE"
            Dim alParameter As New ArrayList
            Dim I As Integer = 0

            alParameter.Add(New SqlParameter("@BEAMID", alparaval(I)))
            I += 1
            alParameter.Add(New SqlParameter("@YEARID", alparaval(I)))
            I += 1

            Return objDBOperation.execute(strCommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region

End Class
