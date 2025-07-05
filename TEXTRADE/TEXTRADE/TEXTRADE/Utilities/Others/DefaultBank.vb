
Imports System.ComponentModel
Imports BL

Public Class DefaultBank

    Public frmString As String       'Used for form Category or GRade
    Public TempName As String        'Used for tempname while edit mode
    Public TempID As Integer         'Used for tempname while edit mode
    Public edit As Boolean
    Public TEMPDEFAULT As Boolean
    Dim regabbr, reginitial As String

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click

        Try
            Dim objcmn As New ClsCommon
            Dim DT As New DataTable
            DT = objcmn.SEARCH("DTYPE_REGID AS REGTYPE", "", "DEFAULTBANK INNER JOIN REGISTERMASTER ON DTYPE_REGID = REGISTER_ID ", " AND REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' AND DTYPE_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                DT = objcmn.Execute_Any_String(" UPDATE DEFAULTBANK SET DTYPE_BANKID = (SELECT ACC_ID FROM LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID WHERE Acc_CMPNAME = '" & CMBLEDGER.Text.Trim & "' AND ACC_YEARID = " & YearId & ") FROM DEFAULTBANK INNER JOIN REGISTERMASTER ON DTYPE_REGID = REGISTER_ID WHERE REGISTER_NAME = '" & CMBREGISTER.Text & "' AND REGISTER_YEARID = " & YearId, "", "")
            Else
                'DT = objcmn.Execute_Any_String(" INSERT INTO DEFAULTBANK VALUES((SELECT REGISTER_ID FROM REGISTERMASTER WHERE REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' AND REGISTER_YEARID = " & YearId & "),(SELECT ACC_ID FROM LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID WHERE ACC_NAME = '" & CMBLEDGER.Text.Trim & "' AND ACC_YEARID = " & YearId & ")," & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = objcmn.Execute_Any_String("INSERT INTO DEFAULTBANK VALUES((SELECT REGISTER_ID FROM REGISTERMASTER WHERE REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' AND REGISTER_YEARID = " & YearId & "),(SELECT ACC_ID FROM LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID WHERE Acc_CMPNAME = '" & CMBLEDGER.Text.Trim & "' AND ACC_YEARID = " & YearId & ")," & CmpId & "," & Userid & "," & YearId & ")", "", "")
            End If
            MsgBox("Updated Successfully!")
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub DefaultBank_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt = True And e.KeyCode = Windows.Forms.Keys.S Then       'for Saving
            Call cmdok_Click(sender, e)
        ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub CMBREGISTER_Enter(sender As Object, e As EventArgs) Handles CMBREGISTER.Enter
        Try
            If CMBREGISTER.Text.Trim = "" Then fillregister(CMBREGISTER, " and (register_type = 'PAYMENT' OR register_type = 'RECEIPT')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBREGISTER_Validating(sender As Object, e As CancelEventArgs) Handles CMBREGISTER.Validating
        'Try
        '    If CMBREGISTER.Text.Trim.Length > 0 And edit = False Then
        '        CMBREGISTER.Text = UCase(CMBREGISTER.Text)
        '        Dim clscommon As New ClsCommon
        '        Dim dt As DataTable
        '        dt = clscommon.SEARCH(" register_abbr, register_initials, register_id", "", " RegisterMaster", " AND register_name = 'PAYMENT' or register_name = 'RECEIPT'  and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
        '        If dt.Rows.Count > 0 Then
        '            regabbr = dt.Rows(0).Item(0).ToString
        '            reginitial = dt.Rows(0).Item(1).ToString
        '            TempID = dt.Rows(0).Item(2)
        '            'cmbregister.Enabled = False
        '        Else
        '            MsgBox("Register Not Present, Add New from Master Module")
        '            e.Cancel = True
        '        End If
        '    End If
        'Catch ex As Exception
        '    If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        'End Try
        Try
            If CMBREGISTER.Text = "PAYMENT" Then
                fillledger(CMBLEDGER, edit, " and (groupmaster.group_secondary = 'BANK A/C' OR groupmaster.group_secondary = 'BANK OD A/C') and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
            End If

            If CMBREGISTER.Text = "RECEIPT" Then
                fillledger(CMBLEDGER, edit, " and (groupmaster.group_secondary = 'BANK A/C' OR groupmaster.group_secondary = 'BANK OD A/C') and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)

            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLEDGER_Validating(sender As Object, e As CancelEventArgs) Handles CMBLEDGER.Validating
        Try
            If CMBREGISTER.Text = "PAYMENT" Then
                fillledger(CMBLEDGER, edit, " and (groupmaster.group_secondary = 'BANK A/C' OR groupmaster.group_secondary = 'BANK OD A/C' ) and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
            ElseIf CMBREGISTER.Text = "RECEIPT" Then
                fillledger(CMBLEDGER, edit, " and (groupmaster.group_secondary = 'BANK A/C' OR groupmaster.group_secondary = 'BANK OD A/C' ) and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class

