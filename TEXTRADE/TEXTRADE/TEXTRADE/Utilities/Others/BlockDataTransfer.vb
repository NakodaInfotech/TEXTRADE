Imports BL

Public Class BlockDataTransfer

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean

    Private Sub CMDUPDATE_Click(sender As Object, e As EventArgs) Handles CMDUPDATE.Click
        Dim IntResult As Integer
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList


            If CHKLEDGER.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            If CHKOTHERMASTER.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            If CHKDATA.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            If CHKSTOCK.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            alParaval.Add(CmpId)
            alParaval.Add(YearId)

            Dim OBJCONFIG As New ClsBlockDataTransfer
            OBJCONFIG.alParaval = alParaval
            IntResult = OBJCONFIG.SAVE()
            MsgBox("Details Added")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If CHKDATA.CheckState = False And CHKLEDGER.CheckState = False And CHKOTHERMASTER.CheckState = False And CHKSTOCK.CheckState = False Then
            EP.SetError(CMDUPDATE, "Select Any Of The Checkbox")
            bln = False
        End If
        Return bln
    End Function

    Sub CLEAR()
        CHKLEDGER.CheckState = CheckState.Unchecked
        CHKOTHERMASTER.CheckState = CheckState.Unchecked
        CHKDATA.CheckState = CheckState.Unchecked
        CHKSTOCK.CheckState = CheckState.Unchecked

    End Sub

    Private Sub BlockDataTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable = OBJCMN.SEARCH(" BLOCKDATA.BLOCK_LEDGER ,BLOCKDATA.BLOCK_OTHER, BLOCKDATA.BLOCK_DATA ,BLOCKDATA.BLOCK_STOCK ", "", " BLOCKDATA", "  AND BLOCKDATA.BLOCK_YEARID=" & YearId & "")
            If dt.Rows.Count > 0 Then
                CHKLEDGER.Checked = dt.Rows(0).Item(0)
                CHKOTHERMASTER.Checked = dt.Rows(0).Item(1)
                CHKDATA.Checked = dt.Rows(0).Item(2)
                CHKSTOCK.Checked = dt.Rows(0).Item(3)

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDDELETE_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Try
            If MsgBox("Wish to Delete Blocked Data?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            'DONE BY GULKIT
            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(YearId)
            Dim OBJPRO As New ClsBlockDataTransfer
            OBJPRO.alParaval = ALPARAVAL
            Dim INTRES As Integer = OBJPRO.DELETE
            MsgBox("Blocked Data Deleted")
            CLEAR()
            EDIT = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class