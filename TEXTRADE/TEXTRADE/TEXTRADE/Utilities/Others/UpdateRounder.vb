Imports BL
Imports System.Windows.Forms
Imports System.IO
Imports System.ComponentModel
Public Class UpdateRounder
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub UpdateRounder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'JOB OUT'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor



        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub CMBROUNDER_Enter(sender As Object, e As EventArgs) Handles CMBROUNDER.Enter
        Try
            If CMBROUNDER.Text.Trim = "" Then FILLCONTRACT(CMBROUNDER)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLGRID()
        Dim OBJCMN As New ClsCommon
        Dim DT As New DataTable
        DT = OBJCMN.SEARCH("ACC_CMPNAME")
    End Sub
End Class