Imports BL
Imports System.Windows.Forms
Imports System.IO
Imports System.ComponentModel
Public Class UpdateRounder
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdEXIT_Click(sender As Object, e As EventArgs) Handles cmdEXIT.Click
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles CMDUPDATE.Click
        For I As Integer = 0 To Val(GRIDITEM.RowCount - 1)
            Dim ROW As DataRow = GRIDITEM.GetDataRow(I)
            If ROW("CHK") = True Then
                Dim dttable As New DataTable
                Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.Execute_Any_String("UPDATE ACCOUNTSMASTER SET ACC_ROUNDERID = (SELECT CONTRACT_ID FROM CONTRACTMASTER WHERE CONTRACT_YEARID = 15 AND CONTRACT_NAME = '" & CMBROUNDER.Text.Trim & "') WHERE ACCOUNTSMASTER.Acc_id =  " & Val(ROW("ACCID")) & " AND Acc_yearid = " & YearId, "", "")
            End If
        Next
    End Sub

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
        DT = OBJCMN.SEARCH("CAST(0 AS BIT) AS CHK,ISNULL(LEDGERS.Acc_id, 0) as ACCID ,ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(CONTRACTMASTER.CONTRACT_NAME, '') AS ROUNDER ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid LEFT OUTER JOIN CONTRACTMASTER ON  CONTRACTMASTER.CONTRACT_ID = LEDGERS.ACC_ROUNDERID AND CONTRACTMASTER.CONTRACT_YEARID = LEDGERS.Acc_yearid ", " AND Acc_yearid = " & YearId & " AND group_secondary = 'Sundry Creditors' AND Acc_TYPE = 'ACCOUNTS' ")

    End Sub
End Class