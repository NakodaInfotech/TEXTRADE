Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports System.ComponentModel
Public Class UploadGodName
    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        Try
            CLEAR()
            TXTGODNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CLEAR()
        Try
            TXTGODNAME.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UploadGodName_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            CLEAR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPDATE_Click(sender As Object, e As EventArgs) Handles CMDUPDATE.Click
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            DT = OBJCMN.Execute_Any_String(" UPDATE VERSION SET VERSION.VERSION_GODNAME = '" & TXTGODNAME.Text.Trim & "'", "", "")
            MsgBox("GodName Updated Successfully")
            CLEAR()
            TXTGODNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class