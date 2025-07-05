
Imports BL

Public Class SelectPcsNoForMatRec

    Public DYEINGNAME As String = ""
    Public LOTNO As String = ""
    Public SRNO As Integer = 0

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectPcsNoForMatRec_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectPcsNoForMatRec_Load(sender As Object, e As EventArgs) Handles Me.Load
        fillgrid("")
    End Sub

    Sub fillgrid(ByVal WHERE As String)
        Try
            Dim OBJCMN As New ClsCommon()
            Dim DT As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, BALENO, PARTYDESIGNNO AS PCSNO, BALMTRS AS PARTYMTRS ", "", " LOT_VIEW_PCSDETAILS ", " AND BALENO NOT IN (SELECT DISTINCT BALENO FROM LOT_VIEW_PCSDETAILS WHERE LOTNO= '" & LOTNO & "' AND YEARID = " & YearId & " AND BALPCS<=0) AND BALPCS > 0 AND LOTCOMPLETED = 0 AND LOTNO = '" & LOTNO & "' AND JOBBERNAME = '" & DYEINGNAME & "' AND YEARID = " & YearId & " ORDER BY GRNSRNO")
            gridbilldetails.DataSource = DT
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Dim DTROW As DataRow = gridbill.GetFocusedDataRow
            SRNO = Val(DTROW("BALENO"))
            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(sender As Object, e As EventArgs) Handles gridbilldetails.DoubleClick
        Try
            Call cmdok_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbilldetails.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Call cmdok_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class