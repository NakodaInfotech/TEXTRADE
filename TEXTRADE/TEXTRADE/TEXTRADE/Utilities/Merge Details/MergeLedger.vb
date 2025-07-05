
Imports BL

Public Class MergeLedger

    Sub CLEAR()
        Try
            EP.Clear()
            GETMAXNO()
            CMBNAME.Text = ""
            CMBMERGENAME.Text = ""
            LBLNAMEGROUP.Text = ""
            LBLMERGEGROUP.Text = ""
            ENTRYDATE.Text = Now.Date
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAXNO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(ML_no),0) + 1 ", " MERGELEDGER ", " and ML_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSRNO.Text = Val(DTTABLE.Rows(0).Item(0))
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, " and ledgers.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.ACC_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBACCCODE, e, Me, txtadd, " and ledgers.acc_cmpname = '" & CMBNAME.Text.Trim & "' and ledgers.acc_cmpid = " & CmpId & " and ledgers.acc_LOCATIONid = " & Locationid & " and ledgers.acc_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbmergename_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMERGENAME.Enter, CMBMERGENAME.Enter
        Try
            If CMBMERGENAME.Text.Trim = "" Then fillname(CMBMERGENAME, False, " and ledgers.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.ACC_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbmergename_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMERGENAME.Validating
        Try
            If CMBMERGENAME.Text.Trim <> "" Then namevalidate(CMBMERGENAME, CMBMERGECODE, e, Me, txtadd, " and ledgers.acc_cmpname = '" & CMBMERGENAME.Text.Trim & "' and ledgers.acc_cmpid = " & CmpId & " and ledgers.acc_LOCATIONid = " & Locationid & " and ledgers.ACC_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True

            If CMBNAME.Text.Trim = "" Then
                EP.SetError(CMBNAME, "Select Ledger")
                BLN = False
            End If

            If CMBMERGENAME.Text.Trim = "" Then
                EP.SetError(CMBMERGENAME, "Select Ledger")
                BLN = False
            End If

            If CMBMERGENAME.Text.Trim = CMBNAME.Text.Trim Then
                EP.SetError(CMBNAME, "Both the Name cannot be same")
                BLN = False
            End If


            If ENTRYDATE.Text = "__/__/____" Then
                EP.SetError(ENTRYDATE, " Please Enter Proper Date")
                BLN = False
            Else
                If Not datecheck(ENTRYDATE.Text) Then
                    EP.SetError(ENTRYDATE, "Date not in Accounting Year")
                    BLN = False
                End If
            End If

            Return BLN
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub ENTRYDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ENTRYDATE.GotFocus
        ENTRYDATE.SelectionStart = 0
    End Sub

    Private Sub ENTRYDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ENTRYDATE.Validating
        Try
            If ENTRYDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(ENTRYDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            If MsgBox("Please Take a Backup Before Merging, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            If MsgBox("Merge Ledger " & CMBNAME.Text.Trim & " with " & CMBMERGENAME.Text.Trim, MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If MsgBox("Are You Sure, All Transactions will be merged in the name of " & CMBMERGENAME.Text.Trim & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim OBJMERGER As New ClsMergeLedger
            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(Format(Convert.ToDateTime(ENTRYDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBNAME.Text.Trim)
            ALPARAVAL.Add(CMBMERGENAME.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJMERGER.alParaval = ALPARAVAL
            Dim INTRESULT As Integer = OBJMERGER.SAVE()
            MsgBox("Ledger Merged Successfully")
            CLEAR()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            CMBNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJMERGE As New MergeLedgerDetails
            OBJMERGE.MdiParent = MDIMain
            OBJMERGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub cmbname_Validated(sender As Object, e As EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.Execute_Any_String(" SELECT ISNULL(GROUPMASTER.GROUP_NAME,'') AS GROUPNAME FROM LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.ACC_GROUPID = GROUPMASTER.GROUP_ID WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId, "", "")
                If DT.Rows.Count > 0 Then LBLNAMEGROUP.Text = DT.Rows(0).Item("GROUPNAME")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbmergename_Validated(sender As Object, e As EventArgs) Handles CMBMERGENAME.Validated
        Try
            If CMBMERGENAME.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.Execute_Any_String(" SELECT ISNULL(GROUPMASTER.GROUP_NAME,'') AS GROUPNAME FROM LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.ACC_GROUPID = GROUPMASTER.GROUP_ID WHERE LEDGERS.ACC_CMPNAME = '" & CMBMERGENAME.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId, "", "")
                If DT.Rows.Count > 0 Then LBLMERGEGROUP.Text = DT.Rows(0).Item("GROUPNAME")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MergeLedger_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            CLEAR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class