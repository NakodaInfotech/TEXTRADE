Imports System.ComponentModel
Imports BL


Public Class IncentiveTagging

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE, GRIDDOUBLECLICK As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean          'used for editing
    Public TEMPINCNO As String
    Public TEMPMSG As Integer
    Dim TEMPROW As Integer


    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click

        Try
            Cursor.Current = Cursors.WaitCursor

            Dim IntResult As Integer


            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            If txtincentiveno.ReadOnly = False Then
                alParaval.Add(Val(txtincentiveno.Text.Trim))
            Else
                alParaval.Add(0)
            End If


            alParaval.Add(Format(Convert.ToDateTime(DTDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbname.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim gridsrno As String = ""
            Dim AMOUNT As String = ""
            Dim INCENTIVE As String = ""
            Dim GRIDREMARKS As String = ""

            For Each row As Windows.Forms.DataGridViewRow In gridinc.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = Val(row.Cells(gsrno.Index).Value)
                        AMOUNT = Val(row.Cells(GAMOUNT.Index).Value)
                        INCENTIVE = Val(row.Cells(GINCENTIVE.Index).Value)
                        GRIDREMARKS = row.Cells(GREMARKS.Index).Value.ToString

                    Else

                        gridsrno = gridsrno & "|" & Val(row.Cells(gsrno.Index).Value)
                        AMOUNT = AMOUNT & "|" & Val(row.Cells(GAMOUNT.Index).Value)
                        INCENTIVE = INCENTIVE & "|" & Val(row.Cells(GINCENTIVE.Index).Value)
                        GRIDREMARKS = GRIDREMARKS & "|" & row.Cells(GREMARKS.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(AMOUNT)
            alParaval.Add(INCENTIVE)
            alParaval.Add(GRIDREMARKS)


            Dim objclsPurord As New ClsIncentiveTagging()
            objclsPurord.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = objclsPurord.SAVE()
                MessageBox.Show("Details Added")
                txtincentiveno.Text = DT.Rows(0).Item(0)
            Else


                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPINCNO)
                IntResult = objclsPurord.UPDATE()
                MessageBox.Show("Details Updated")
                EDIT = False
            End If

            clear()
            cmbname.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        clear()
    End Sub

    Private Sub IncentiveTagging_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow

            DTROW = USERRIGHTS.Select("FormName = 'ACCOUNTS MASTER'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            clear()



            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJCMN As New ClsCommon
                Dim OBJCLSINCENTIVE As New ClsIncentiveTagging
                Dim dttable As New DataTable

                dttable = OBJCLSINCENTIVE.SELECTIT(TEMPINCNO, CmpId, YearId)
                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        txtincentiveno.Text = TEMPINCNO
                        txtincentiveno.ReadOnly = True
                        DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")
                        cmbname.Text = Convert.ToString(dr("NAME"))


                        gridinc.Rows.Add(dr("GRIDSRNO"), dr("AMOUNT"), dr("INCENTIVE"), dr("GRIDREMARKS"))


                    Next


                    gridinc.FirstDisplayedScrollingRowIndex = gridinc.RowCount - 1
                Else
                    EDIT = False
                    clear()
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Sub EDITROW()
        Try
            If gridinc.CurrentRow.Index >= 0 And gridinc.Item(gsrno.Index, gridinc.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TXTSRNO.Text = gridinc.Item(gsrno.Index, gridinc.CurrentRow.Index).Value.ToString
                TXTAMOUNT.Text = gridinc.Item(GAMOUNT.Index, gridinc.CurrentRow.Index).Value.ToString
                TXTINCENTIVE.Text = gridinc.Item(GINCENTIVE.Index, gridinc.CurrentRow.Index).Value.ToString
                TXTREMARKS.Text = gridinc.Item(GREMARKS.Index, gridinc.CurrentRow.Index).Value.ToString


                TEMPROW = gridinc.CurrentRow.Index
                TXTAMOUNT.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            gridinc.RowCount = 0
LINE1:
            TEMPINCNO = Val(txtincentiveno.Text) - 1
            If TEMPINCNO > 0 Then
                EDIT = True
                IncentiveTagging_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If gridinc.RowCount = 0 And TEMPINCNO > 1 Then
                txtincentiveno.Text = TEMPINCNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            gridinc.RowCount = 0
LINE1:
            TEMPINCNO = Val(txtincentiveno.Text) + 1
            GETMAXNO()
            Dim MAXNO As Integer = txtincentiveno.Text.Trim
            clear()



            If Val(txtincentiveno.Text) - 1 >= TEMPINCNO Then
                EDIT = True
                IncentiveTagging_Load(sender, e)
            Else
                clear()
                EDIT = False
                TEMPROW = 0
            End If
            If gridinc.RowCount = 0 And TEMPINCNO < MAXNO Then
                txtincentiveno.Text = TEMPINCNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub
    Sub GETMAXNO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(INC_no),0) + 1 ", " INCENTIVES ", " and INC_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then txtincentiveno.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objpodtls As New IncentiveTaggingDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub clear()
        EP.Clear()
        txtincentiveno.ReadOnly = True
        TXTAMOUNT.Clear()
        TXTINCENTIVE.Clear()
        TXTREMARKS.Clear()
        cmbname.Text = ""
        gridinc.RowCount = 0
        TXTSRNO.Clear()
        getmax_INCENTIVE_NO()
        TXTSRNO.Text = 1
        tstxtbillno.Clear()
        DTDATE.Text = Now.Date
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click

        Dim IntResult As Integer
        Try

            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                TEMPMSG = MsgBox("Delete Incentives?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(txtincentiveno.Text.Trim)
                    alParaval.Add(Userid)
                    alParaval.Add(CmpId)
                    alParaval.Add(YearId)

                    Dim ClsINCTAG As New ClsIncentiveTagging()
                    ClsINCTAG.alParaval = alParaval
                    IntResult = ClsINCTAG.Delete()
                    MsgBox("Incentives Deleted")
                    clear()
                    EDIT = False
                End If
            Else
                MsgBox("Delete Is only In Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub



    Private Function ERRORVALID() As Boolean

        Dim bln As Boolean = True
        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(cmbname, " Please Fill Party Name ")
            bln = False
        End If

        If DTDATE.Text = "__/__/____" Then
            EP.SetError(DTDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTDATE.Text) Then
                EP.SetError(DTDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If gridinc.RowCount = 0 Then
            EP.SetError(gridinc, " Please Enter Proper Details ")
            bln = False
        End If



        Return bln
    End Function

    Private Sub TXTREMARKS_Validating(sender As Object, e As CancelEventArgs) Handles TXTREMARKS.Validating
        Try
            If (Val(TXTAMOUNT.Text.Trim) > 0) And (Val(TXTINCENTIVE.Text.Trim) > 0) Then
                Fillgrid()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub Fillgrid()
        Try
            If GRIDDOUBLECLICK = False Then
                gridinc.Rows.Add(Val(TXTSRNO.Text.Trim), Val(TXTAMOUNT.Text.Trim), Val(TXTINCENTIVE.Text.Trim), (TXTREMARKS.Text.Trim))
                getsrno(gridinc)
            ElseIf GRIDDOUBLECLICK = True Then
                gridinc.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
                gridinc.Item(GAMOUNT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text.Trim), "0.00")
                gridinc.Item(GINCENTIVE.Index, TEMPROW).Value = Format(Val(TXTINCENTIVE.Text.Trim), "0.00")
                gridinc.Item(GREMARKS.Index, TEMPROW).Value = (TXTREMARKS.Text.Trim)

                GRIDDOUBLECLICK = False

            End If


            gridinc.FirstDisplayedScrollingRowIndex = gridinc.RowCount - 1

            TXTSRNO.Text = gridinc.RowCount + 1
            TXTAMOUNT.Text = ""
            TXTINCENTIVE.Clear()
            TXTREMARKS.Clear()
            TXTSRNO.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub



    Private Sub txtincentiveno_Validating(sender As Object, e As CancelEventArgs) Handles txtincentiveno.Validating
        Try
            If Val(txtincentiveno.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(INCENTIVES.INC_NO,0)  AS INCNO", "", " INCENTIVES ", "  AND INCENTIVES.INC_NO=" & Val(txtincentiveno.Text.Trim) & " AND INCENTIVES.INC_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Incentive No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtincentiveno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtincentiveno.KeyPress, TXTINCENTIVE.KeyPress, TXTAMOUNT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub DTDATE_GotFocus(sender As Object, e As EventArgs) Handles DTDATE.GotFocus
        DTDATE.SelectionStart = 0
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            Call cmdok_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Sub getmax_INCENTIVE_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(INC_NO),0) + 1 ", "INCENTIVES ", "  AND INC_cmpid=" & CmpId & " and INC_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then txtincentiveno.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub tstxtbillno_Validating(sender As Object, e As CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                gridinc.RowCount = 0
                TEMPINCNO = Val(tstxtbillno.Text)
                If TEMPINCNO > 0 Then
                    EDIT = True
                    IncentiveTagging_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub IncentiveTagging_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()

            ElseIf e.KeyCode = Keys.OemQuotes Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.F5 Then
                gridinc.Focus()
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolprevious_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)

            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridinc_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles gridinc.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validated(sender As Object, e As EventArgs) Handles cmbname.Validated
        Try
            'CHECK WHETHER THIS PARTY'S PRICE LIST IS ALREADY PRESENT OR NOT, ON FRESH MODE ONLU
            If EDIT = False And cmbname.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 inc_NO AS INCNO", "", "incentives INNER JOIN LEDGERS ON inc_LEDGERID = LEDGERS.ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND INC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    MsgBox("Incentive for This Company Already Present, Please Modify the Existing Incentive", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class