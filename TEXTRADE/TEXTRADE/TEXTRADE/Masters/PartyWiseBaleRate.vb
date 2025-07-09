Imports System.ComponentModel
Imports BL

Public Class PartyWiseBaleRate

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean          'used for editing

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        Dim OBJCMN As New ClsCommon
        Dim DT As New DataTable

        'FOR RAJKRIPA PARTY NAME IS NOT MANDATE
        If CMBNAME.Text.Trim <> "" Then
            DT = OBJCMN.SEARCH(" ISNULL(PARTYWISEBALERATE.PAR_NO,0) AS ID, ISNULL(LEDGERS.ACC_CMPNAME, '') AS NAME, ISNULL(TRANSLEDGERS.ACC_CMPNAME, '') AS TRANSPORT ", "", " PARTYWISEBALERATE LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON PARTYWISEBALERATE.PAR_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON PARTYWISEBALERATE.PAR_LEDGERID = LEDGERS.Acc_id ", " AND ledgers.acc_cmpname = '" & CMBNAME.Text.Trim & "' AND TRANSLEDGERS.acc_cmpname = '" & CMBTRANSPORT.Text.Trim & "'  AND PAR_YEARID = " & YearId & " AND PAR_CMPID = " & CmpId)
            If DT.Rows.Count > 0 Then
                If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And Val(TXTNO.Text) <> Val(DT.Rows(0).Item(0)) And CMBNAME.Text.Trim <> DT.Rows(0).Item("NAME") And CMBTRANSPORT.Text.Trim <> DT.Rows(0).Item("TRANSPORT")) Then
                    EP.SetError(CMBNAME, "Bale Rate for This party & Item Already Exist in Grid below")
                    bln = False
                End If
            End If

            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, "Select Party")
                bln = False
            End If
        End If


        If CMBTRANSPORT.Text.Trim.Length = 0 Then
            EP.SetError(CMBTRANSPORT, "Select Transport")
            bln = False
        End If

        Return bln
    End Function
    Sub FILLGRID()
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(PARTYWISEBALERATE.PAR_NO, 0) AS ID, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(PARTYWISEBALERATE.PAR_RATE, 0) AS RATE", "", " PARTYWISEBALERATE LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON PARTYWISEBALERATE.PAR_TRANSID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON PARTYWISEBALERATE.PAR_LEDGERID = LEDGERS.Acc_id ", " AND PAR_YEARID = " & YearId & " AND PAR_CMPID = " & CmpId)

            gridbilldetails.DataSource = DT
            CMBNAME.Text = ""
            CMBTRANSPORT.Text = ""
            TXTRATE.Clear()
            TXTNO.Clear()
            EP.Clear()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub




    Private Sub gridbilldetails_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbilldetails.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.OemQuotes Or e.KeyCode = Keys.OemPipe Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub



    Sub FILLCMB()
        Try
            FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
            FILLNAME(CMBTRANSPORT, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CLEAR()
        Try
            EP.Clear()
            TXTNO.Clear()
            CMBNAME.Text = ""
            CMBTRANSPORT.Text = ""
            TXTRATE.Clear()
            gridbilldetails.DataSource = Nothing

            GRIDDOUBLECLICK = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub PartyWiseBaleRate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ACCOUNTS MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            Dim OBJCMN As New ClsCommon
            Dim dttable As New DataTable

            FILLCMB()
            CLEAR()
            FILLGRID()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub TXTRATE_Validated(sender As Object, e As EventArgs) Handles TXTRATE.Validated
        Try
            If CMBNAME.Text.Trim <> "" And TXTRATE.Text.Trim.Length > 0 Then
                'If ClientName = "RAJKRIPA" And Val(TXTRATE.Text.Trim) = 0 Then Exit Sub
                'If ClientName <> "RAJKRIPA" And CMBNAME.Text.Trim = "" Then Exit Sub
                If Not errorvalid() Then
                    MsgBox("Bale Rate for This Party & Item Already Exist in Grid below", MsgBoxStyle.Critical)
                    CMBNAME.Focus()
                    Exit Sub
                End If
                SAVE()
            End If
            CMBNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTTRANSADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'", "SUNDRY DEBTORS", "ACCOUNTS", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub CMBTRANSPORT_Enter(sender As Object, e As EventArgs) Handles CMBTRANSPORT.Enter
        Try
            FILLNAME(CMBTRANSPORT, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORT_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBTRANSPORT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'"
                OBJLEDGER.ShowDialog()
                'If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBTRANSPORT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub CMBTRANSPORT_Validating(sender As Object, e As CancelEventArgs) Handles CMBTRANSPORT.Validating
        Try
            If CMBTRANSPORT.Text.Trim <> "" Then NAMEVALIDATE(CMBTRANSPORT, CMBCODE, e, Me, TXTTRANSADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        FILLGRID()
        CMBNAME.Focus()
    End Sub
    Sub SAVE()
        Try

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList
            Dim OBJCONFIG As New ClsPartyItemWiseChart

            ALPARAVAL.Add(Val(TXTNO.Text.Trim))
            ALPARAVAL.Add(CMBNAME.Text.Trim)
            ALPARAVAL.Add(CMBTRANSPORT.Text.Trim)
            ALPARAVAL.Add(Val(TXTRATE.Text.Trim))
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            OBJCONFIG.alParaval = ALPARAVAL

            Dim INT As Integer = OBJCONFIG.SAVE()
            FILLGRID()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMDDELETE_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If GRIDDOUBLECLICK = True Then
                MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                Exit Sub
            End If

            Dim TEMPMSG As Integer = MsgBox("Wish To Delete?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbNo Then Exit Sub


            'DELETE FROM TABLE
            Dim OBJSM As New ClsPartyItemWiseChart
            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(gridbill.GetFocusedRowCellValue("ID"))
            ALPARAVAL.Add(Userid)


            OBJSM.alParaval = ALPARAVAL
            Dim INTRES As Integer = OBJSM.DELETE()
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs) Handles gridbill.DoubleClick
        EDITROW()
    End Sub
    Sub EDITROW()
        Try
            If gridbill.GetFocusedRowCellValue("ID") > 0 Then
                GRIDDOUBLECLICK = True
                TXTNO.Text = gridbill.GetFocusedRowCellValue("ID")
                CMBNAME.Text = gridbill.GetFocusedRowCellValue("NAME")
                CMBTRANSPORT.Text = gridbill.GetFocusedRowCellValue("ITEM")
                TXTRATE.Text = Val(gridbill.GetFocusedRowCellValue("RATE"))
                CMBNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbill.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Call CMDDELETE_Click(sender, e)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Function CHECKDUPLICATE() As Boolean
    '    'Try
    '    '    Dim bln As Boolean = True
    '    '    For Each row As DataGridViewRow In gridbilldetails.rows
    '    '        If (GRIDDOUBLECLICK = True And TEMPROW <> row.Index) Or GRIDDOUBLECLICK = False Then
    '    '            If TXTPOLICYNO.Text.Trim = row.Cells(GPOLICYNO.Index).Value Then bln = False
    '    '        End If
    '    '    Next
    '    '    Return bln
    '    'Catch ex As Exception
    '    '    Throw ex
    '    'End Try
    '    Dim bln As Boolean = True

    '    Dim OBJCMN As New ClsCommon
    '    Dim DT As New DataTable
    '    If CMBNAME.Text.Trim <> "" Then
    '        DT = OBJCMN.SEARCH(" ISNULL(PARTYWISEBALERATE.PAR_NO,0) AS ID, ISNULL(LEDGERS.ACC_CMPNAME, '') AS NAME, ISNULL(TRANSLEDGERS.ACC_CMPNAME, '') AS TRANSPORT ", "", " PARTYWISEBALERATE LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON PARTYWISEBALERATE.PAR_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON PARTYWISEBALERATE.PAR_LEDGERID = LEDGERS.Acc_id ", " AND ledgers.acc_cmpname = '" & CMBNAME.Text.Trim & "' AND TRANSLEDGERS.acc_cmpname = '" & CMBTRANSPORT.Text.Trim & "'  AND PAR_YEARID = " & YearId & " AND PAR_CMPID = " & CmpId)
    '        If DT.Rows.Count > 0 Then
    '            If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And Val(TXTNO.Text) <> Val(DT.Rows(0).Item(0)) And CMBNAME.Text.Trim <> DT.Rows(0).Item("NAME") And CMBTRANSPORT.Text.Trim <> DT.Rows(0).Item("TRANSPORT")) Then
    '                EP.SetError(CMBNAME, "Bale Rate for This party & Item Already Exist in Grid below")
    '                bln = False
    '            End If
    '        End If
    '    End If
    'End Function
End Class