Imports System.ComponentModel
Imports System.Windows.Forms
Imports BL
Imports iTextSharp.text.pdf
Public Class YarnLoomEfficiency
    'following two variables Is only For used In edit mode....
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim gridDoubleClick As Boolean
    Dim tempRow As Integer

    Public edit As Boolean
    Public TEMPYLENO As String
    Public tempMsg As Integer
    Dim dtLoom As DataTable
    Private Sub cmbrounder_Enter(sender As Object, e As EventArgs) Handles cmbrounder.Enter
        Try
            If cmbrounder.Text.Trim = "" Then
                FILLCONTRACT(cmbrounder)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then FILLNAME(cmbname, edit, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then NAMEVALIDATE(cmbname, CMBCODE, e, Me, TXTADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'  or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS') ", "Sundry Creditors")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBEAMNAME.Enter, CMBYARNQ.Enter
        Try
            If CMBBEAMNAME.Text.Trim = "" Then fillBEAM(CMBBEAMNAME, edit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBEAMNAME.Validating, CMBYARNQ.Validating
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then BEAMVALIDATE(CMBBEAMNAME, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


    Sub clear()

        tstxtbillno.Clear()
        EFFDATE.Text = Now.Date


        EP.Clear()
        txtsrno.Clear()
        CMBBEAMNAME.Text = ""
        txtgridremarks.Clear()

        txtremarks.Clear()
        cmbname.Text = ""
        cmbrounder.Text = ""
        CMBLOOM.Text = ""
        CMBBEAMNAME.Text = ""
        TXTBEAMNO.Clear()
        TXTRPM.Clear()
        TXTPICKS.Clear()
        TXTRECMTRS.Clear()
        TXTWEFT.Clear()
        TXTWARP.Clear()
        TXTEFFPER.Clear()
        TXTAVGPICK.Clear()
        txtgridremarks.Clear()
        gridloan.RowCount = 0
        LBLTOTALRECMTRS.Text = 0.0


        gridDoubleClick = False
        'txtadd.Clear()


        getmax_loan_no() 'this function is for to get max value from the Purchase loanuisition table

        If gridloan.RowCount > 0 Then
            txtsrno.Text = Val(gridloan.Rows(gridloan.RowCount - 1).Cells(gsrno.Index).Value) + 1
        Else
            txtsrno.Text = 1
        End If

    End Sub

    Sub getmax_loan_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(YLE_no),0) + 1 ", "YARNLOOMEFFICIENCY", " AND YLE_cmpid=" & CmpId & " and YLE_LOCATIONID=" & Locationid & " and YLE_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            txteffno.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(gsrno.Index).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()
        gridloan.Enabled = True
        If gridDoubleClick = False Then
            gridloan.Rows.Add(Val(txtsrno.Text.Trim), CMBLOOM.Text.Trim, CMBBEAMNAME.Text.Trim, Val(TXTBEAMNO.Text.Trim), Val(TXTRPM.Text.Trim), Format(Val(TXTPICKS.Text.Trim), "0.00"), Format(Val(TXTRECMTRS.Text.Trim), "0.00"), Format(Val(TXTWEFT.Text.Trim), "0.00"), Format(Val(TXTWARP.Text.Trim), "0.00"), Format(Val(TXTEFFPER.Text.Trim), "0.00"), Format(Val(TXTAVGPICK.Text.Trim), "0.00"), txtgridremarks.Text.Trim)
            getsrno(gridloan)
        ElseIf gridDoubleClick = True Then
            gridloan.Item(gsrno.Index, tempRow).Value = Val(txtsrno.Text.Trim)
            gridloan.Item(GLOOM.Index, tempRow).Value = CMBLOOM.Text.Trim
            gridloan.Item(GBEAMNAME.Index, tempRow).Value = CMBBEAMNAME.Text.Trim
            gridloan.Item(GBEAMNO.Index, tempRow).Value = Val(TXTBEAMNO.Text.Trim)

            gridloan.Item(GRPM.Index, tempRow).Value = Format(Val(TXTRPM.Text.Trim), "0.00")
            gridloan.Item(GPICKS.Index, tempRow).Value = Format(Val(TXTPICKS.Text.Trim), "0.00")
            gridloan.Item(GRECMTRS.Index, tempRow).Value = Format(Val(TXTRECMTRS.Text.Trim), "0.00")
            gridloan.Item(GWEFT.Index, tempRow).Value = Format(Val(TXTWEFT.Text.Trim), "0.00")
            gridloan.Item(GWARP.Index, tempRow).Value = Format(Val(TXTWARP.Text.Trim), "0.00")
            gridloan.Item(GEFFPER.Index, tempRow).Value = Format(Val(TXTEFFPER.Text.Trim), "0.00")
            gridloan.Item(GAVGPICK.Index, tempRow).Value = Format(Val(TXTAVGPICK.Text.Trim), "0.00")
            gridloan.Item(GGRIDREMARKS.Index, tempRow).Value = txtgridremarks.Text.Trim


            gridDoubleClick = False

        End If

        gridloan.FirstDisplayedScrollingRowIndex = gridloan.RowCount - 1

        txtsrno.Clear()
        CMBLOOM.Text = ""
        CMBBEAMNAME.Text = ""
        TXTBEAMNO.Clear()
        TXTRPM.Clear()
        TXTPICKS.Clear()
        TXTRECMTRS.Clear()
        TXTWEFT.Clear()
        TXTWARP.Clear()
        TXTEFFPER.Clear()
        TXTAVGPICK.Clear()
        txtgridremarks.Clear()

        If gridloan.RowCount > 0 Then
            txtsrno.Text = Val(gridloan.Rows(gridloan.RowCount - 1).Cells(gsrno.Index).Value) + 1
        Else
            txtsrno.Text = 1
        End If
        txtsrno.Focus()

    End Sub

    Sub total()
        LBLTOTALRECMTRS.Text = "0.00"
        LBLTOTALWEFT.Text = "0.00"
        For Each row As DataGridViewRow In gridloan.Rows
            If Val(row.Cells(GRECMTRS.Index).Value) <> 0 Then
                LBLTOTALRECMTRS.Text = Format(Val(LBLTOTALRECMTRS.Text) + Val(row.Cells(GRECMTRS.Index).Value), "0.00")
                LBLTOTALWEFT.Text = Format(Val(LBLTOTALWEFT.Text) + Val(row.Cells(GWEFT.Index).Value), "0.00")
            End If
        Next
    End Sub

    Private Sub gridloan_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridloan.CellDoubleClick

        EDITROW()
    End Sub
    Sub EDITROW()
        Try

            If (Convert.ToBoolean(gridloan.CurrentRow.Cells(GDONE.Index).Value) = True And UserName <> "Admin") Then
                MessageBox.Show("Row Locked, You Cannot Edit This Row")
                Exit Sub
            End If

            If gridloan.CurrentRow.Index >= 0 And gridloan.Item(gsrno.Index, gridloan.CurrentRow.Index).Value <> Nothing Then
                gridDoubleClick = True
                txtsrno.Text = gridloan.Item(gsrno.Index, gridloan.CurrentRow.Index).Value.ToString
                CMBLOOM.Text = gridloan.Item(GLOOM.Index, gridloan.CurrentRow.Index).Value.ToString
                CMBBEAMNAME.Text = gridloan.Item(GBEAMNAME.Index, gridloan.CurrentRow.Index).Value.ToString
                TXTBEAMNO.Text = gridloan.Item(GBEAMNO.Index, gridloan.CurrentRow.Index).Value.ToString
                TXTRPM.Text = gridloan.Item(GRPM.Index, gridloan.CurrentRow.Index).Value.ToString
                TXTPICKS.Text = gridloan.Item(GPICKS.Index, gridloan.CurrentRow.Index).Value.ToString
                TXTRECMTRS.Text = gridloan.Item(GRECMTRS.Index, gridloan.CurrentRow.Index).Value.ToString
                TXTWEFT.Text = gridloan.Item(GWEFT.Index, gridloan.CurrentRow.Index).Value.ToString
                TXTWARP.Text = gridloan.Item(GWARP.Index, gridloan.CurrentRow.Index).Value.ToString
                TXTEFFPER.Text = gridloan.Item(GEFFPER.Index, gridloan.CurrentRow.Index).Value.ToString
                TXTAVGPICK.Text = gridloan.Item(GAVGPICK.Index, gridloan.CurrentRow.Index).Value.ToString
                txtgridremarks.Text = gridloan.Item(GGRIDREMARKS.Index, gridloan.CurrentRow.Index).Value.ToString

                tempRow = gridloan.CurrentRow.Index
                CMBLOOM.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        edit = False
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Dim IntResult As Integer
        Try
            Cursor.Current = Cursors.WaitCursor

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            If txteffno.ReadOnly = False Then
                alParaval.Add(Val(txteffno.Text.Trim))
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(Format(Convert.ToDateTime(EFFDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(cmbrounder.Text.Trim)
            alParaval.Add(LBLTOTALRECMTRS.Text.Trim)
            alParaval.Add(LBLTOTALWEFT.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim gridsrno As String = ""
            Dim LOOMNO As String = ""

            Dim BEAMNAME As String = ""
            Dim BEAMNO As String = ""
            Dim RPM As String = ""
            Dim PICKS As String = ""
            Dim RECMTRS As String = ""
            Dim WEFT As String = ""
            Dim WARP As String = ""
            Dim EFFICIENCYPER As String = ""
            Dim AVGPICK As String = ""
            Dim gridremarks As String = ""
            Dim DONE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In gridloan.Rows
                If row.Cells(gsrno.Index).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value
                        LOOMNO = row.Cells(GLOOM.Index).Value.ToString
                        BEAMNAME = row.Cells(GBEAMNAME.Index).Value.ToString
                        BEAMNO = Val(row.Cells(GBEAMNO.Index).Value)
                        RPM = Val(row.Cells(GRPM.Index).Value)
                        PICKS = Val(row.Cells(GPICKS.Index).Value)
                        RECMTRS = Val(row.Cells(GRECMTRS.Index).Value)
                        WEFT = Val(row.Cells(GWEFT.Index).Value)
                        WARP = Val(row.Cells(GWARP.Index).Value)
                        EFFICIENCYPER = Val(row.Cells(GEFFPER.Index).Value)
                        AVGPICK = Val(row.Cells(GAVGPICK.Index).Value)
                        gridremarks = row.Cells(GGRIDREMARKS.Index).Value.ToString
                        If IsDBNull(row.Cells(GDONE.Index).Value) OrElse row.Cells(GDONE.Index).Value Is Nothing Then
                            DONE = "0"
                        Else
                            DONE = row.Cells(GDONE.Index).Value
                        End If

                    Else
                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        LOOMNO = LOOMNO & "|" & row.Cells(GLOOM.Index).Value.ToString
                        BEAMNAME = BEAMNAME & "|" & row.Cells(GBEAMNAME.Index).Value.ToString
                        BEAMNO = BEAMNO & "|" & Val(row.Cells(GBEAMNO.Index).Value)
                        RPM = RPM & "|" & Val(row.Cells(GRPM.Index).Value)
                        PICKS = PICKS & "|" & Val(row.Cells(GPICKS.Index).Value)
                        RECMTRS = RECMTRS & "|" & Val(row.Cells(GRECMTRS.Index).Value)
                        WEFT = WEFT & "|" & Val(row.Cells(GWEFT.Index).Value)
                        WARP = WARP & "|" & Val(row.Cells(GWARP.Index).Value)
                        EFFICIENCYPER = EFFICIENCYPER & "|" & Val(row.Cells(GEFFPER.Index).Value)
                        AVGPICK = AVGPICK & "|" & Val(row.Cells(GAVGPICK.Index).Value)
                        gridremarks = gridremarks & "|" & row.Cells(GGRIDREMARKS.Index).Value.ToString
                        If IsDBNull(row.Cells(GDONE.Index).Value) OrElse row.Cells(GDONE.Index).Value Is Nothing Then
                            DONE = DONE & "|0"
                        Else
                            DONE = DONE & "|" & row.Cells(GDONE.Index).Value
                        End If


                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(LOOMNO)

            alParaval.Add(BEAMNAME)
            alParaval.Add(BEAMNO)
            alParaval.Add(RPM)
            alParaval.Add(PICKS)
            alParaval.Add(RECMTRS)
            alParaval.Add(WEFT)
            alParaval.Add(WARP)
            alParaval.Add(EFFICIENCYPER)
            alParaval.Add(AVGPICK)
            alParaval.Add(gridremarks)
            alParaval.Add(DONE)




            Dim objclsloan As New ClsYarnLoomEfficiency
            objclsloan.alParaval = alParaval

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                'IntResult = objclsloan.save()
                Dim DTT As DataTable = objclsloan.SAVE()
                txteffno.Text = DTT.Rows(0).Item(0)
                MessageBox.Show("Details Added")
            Else
                alParaval.Add(TEMPYLENO)
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objclsloan.UPDATE()
                MsgBox("Details Updated")
            End If
            edit = False
            Dim TEMPMSG As Integer
            TEMPMSG = MsgBox("WISH TO PRINT", MsgBoxStyle.YesNo)

            If TEMPMSG = vbYes Then
                'Dim OBJGN As New LoanDesign
                ''OBJGN.loanNO = txtloanno.Text
                ''OBJGN.MdiParent = MDIMain
                ''OBJGN.selfor_po = "{loanMaster.loan_no}=" & txtloanno.Text & " and {loanMaster.loan_cmpid}=" & CmpId & " and {loanMaster.loan_locationid}=" & Locationid & " and {loanMaster.loan_yearid}=" & YearId
                'OBJGN.Show()
            End If
            edit = False
            clear()
            cmbname.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(cmbname, "Enter Weaver Name")
            bln = False
        End If

        If cmbrounder.Text.Trim.Length = 0 Then
            EP.SetError(cmbrounder, "Enter Rounder")
            bln = False
        End If

        'If gridloan.RowCount = 0 Then
        '    EP.SetError(txtqty, "Enter Item Details")
        '    bln = False
        'End If


        'If chkchange.CheckState = CheckState.Unchecked Then
        '    EP.SetError(txtqty, "Enter Item Details")
        '    bln = False
        'End If



        If EFFDATE.Text = "__/__/____" Then
            EP.SetError(EFFDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(EFFDATE.Text) Then
                EP.SetError(EFFDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If
        Return bln
    End Function

    Private Sub Purchaseloanuisition_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If errorvalid() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
            toolprevious_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
            toolnext_Click(sender, e)
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        End If
    End Sub

    Private Sub Purchaseloanuisition_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If AscW(e.KeyChar) <> 33 Then
            chkchange.CheckState = CheckState.Checked
        End If
    End Sub

    Private Sub Loanmaster_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Dim DTROW() As DataRow

            DTROW = USERRIGHTS.Select("FormName = 'YARN LOOMEFFICIENCY'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor



            clear()
            If edit = True Then
                SHOWDATA()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub
    Sub SHOWDATA()
        Try

            clear()

            If edit = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim ALPARAVAL As New ArrayList
                Dim objclsloan As New ClsYarnLoomEfficiency

                ALPARAVAL.Add(TEMPYLENO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)

                objclsloan.alParaval = ALPARAVAL
                Dim dt As DataTable = objclsloan.SELECTLOAN(TEMPYLENO, CmpId, Locationid, YearId)

                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows

                        txteffno.Text = TEMPYLENO
                        EFFDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")
                        cmbname.Text = Convert.ToString(dr("NAME"))
                        'cmbname_Validated(Nothing, Nothing)  ' ← ADD THIS LINE
                        cmbrounder.Text = Convert.ToString(dr("ROUNDER").ToString)
                        txtremarks.Text = Convert.ToString(dr("remarks"))
                        gridloan.Rows.Add(dr("gridsrno").ToString, dr("LOOMNO").ToString, dr("BEAMNAME").ToString, Val(dr("BEAMNO")), Format(Val(dr("RPM")), "0.00"), Format(Val(dr("PICKS")), "0.00"), Format(Val(dr("RECMTRS")), "0.00"), Format(Val(dr("WEFT")), "0.00"), Format(Val(dr("WARP")), "0.00"), Format(Val(dr("EFFPER")), "0.00"), Format(Val(dr("AVGPICK")), "0.00"), dr("GRIDREMARKS").ToString)

                    Next
                    gridloan.FirstDisplayedScrollingRowIndex = gridloan.RowCount - 1
                End If

                chkchange.CheckState = CheckState.Checked
                total()
            End If

            'If gridDoubleClick = False Then
            If gridloan.RowCount > 0 Then
                txtsrno.Text = Val(gridloan.Rows(gridloan.RowCount - 1).Cells(gsrno.Index).Value) + 1
            Else
                txtsrno.Text = 1
            End If
            'End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub txtsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsrno.GotFocus
        If gridDoubleClick = False Then
            If gridloan.RowCount > 0 Then
                txtsrno.Text = Val(gridloan.Rows(gridloan.RowCount - 1).Cells(gsrno.Index).Value) + 1
            Else
                txtsrno.Text = 1
            End If
        End If
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If edit = True Then
                Dim BLN As Boolean = True
                Dim TEMPMSG As Integer = MsgBox("Delete Yarn Loom Efficiency?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then

                    Dim ALPARAVAL As New ArrayList
                    Dim OBJLOAN As New ClsYarnLoomEfficiency

                    ALPARAVAL.Add(Val(txteffno.Text.Trim))
                    ALPARAVAL.Add(CmpId)
                    ALPARAVAL.Add(Locationid)
                    ALPARAVAL.Add(Userid)
                    ALPARAVAL.Add(YearId)

                    OBJLOAN.alParaval = ALPARAVAL
                    Dim IntResult As Integer = OBJLOAN.Delete()
                    MsgBox("Entry Deleted")
                    clear()
                    edit = False


                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objprdetails As New YarnLoomEfficiencyDetails
            objprdetails.MdiParent = MDIMain
            objprdetails.Show()
            objprdetails.BringToFront()
            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        TEMPYLENO = Val(txteffno.Text) - 1
        clear()
        If TEMPYLENO > 0 Then
            edit = True
            Loanmaster_load(sender, e)
        Else
            clear()
            edit = False
        End If
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        TEMPYLENO = Val(txteffno.Text) + 1
        getmax_loan_no()
        clear()
        If Val(txteffno.Text) - 1 >= TEMPYLENO Then
            edit = True
            Loanmaster_load(sender, e)
        Else
            clear()
            edit = False
        End If
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub loandate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If EFFDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(EFFDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub gridpurchaseloan_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridloan.KeyDown
        Try
            If e.KeyCode = Keys.Delete And gridloan.RowCount > 0 Then


                gridloan.Rows.RemoveAt(gridloan.CurrentRow.Index)
                total()
                getsrno(gridloan)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub


    Private Sub cmbitemname_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CMBBEAMNAME.KeyPress, CMBYARNQ.KeyPress
        commakeypress(e, CMBBEAMNAME, Me)
    End Sub

    Private Sub txtgridremarks_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtgridremarks.KeyPress
        commakeypress(e, txtgridremarks, Me)
    End Sub
    Private Sub tstxtbillno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tstxtbillno.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        TEMPYLENO = Val(tstxtbillno.Text)
        clear()
        If TEMPYLENO > 0 Then
            edit = True
            Loanmaster_load(sender, e)
        Else
            clear()
            edit = False
        End If
    End Sub



    'Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
    '    Try
    '        If edit = True Then
    '            Dim OBJGN As New LoanDesign
    '            OBJGN.loanNO = TEMPloanNO
    '            OBJGN.MdiParent = MDIMain
    '            OBJGN.selfor_po = "{loanMaster.loan_no}=" & TEMPloanNO & " and {loanMaster.loan_cmpid}=" & CmpId & " and {loanMaster.loan_locationid}=" & Locationid & " and {loanMaster.loan_yearid}=" & YearId
    '            OBJGN.Show()
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub txtgridremarks_Validated(sender As Object, e As EventArgs) Handles txtgridremarks.Validated
        Try
            If CMBLOOM.Text.Trim <> "" And CMBBEAMNAME.Text.Trim <> "" And Val(TXTRECMTRS.Text.Trim) > 0 Then

                'For Each row As DataGridViewRow In gridloan.Rows
                '    If row.Cells(GLOOM.Index).Value.ToString.Trim = CMBLOOM.Text.Trim Then
                '        If gridDoubleClick = False OrElse row.Index <> tempRow Then  ' ← CHANGE THIS LINE
                '            MsgBox("Loom No " & CMBLOOM.Text.Trim & " already exists in grid!")
                '            CMBLOOM.Focus()
                '            Exit Sub
                '        End If
                '    End If
                'Next
                CHECKLOOM()

                fillgrid()
                total()
                EP.Clear()
            Else
                EP.SetError(CMBBEAMNAME, "Please enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOOM_Enter(sender As Object, e As EventArgs) Handles CMBLOOM.Enter
        Try
            If cmbname.Text.Trim = "" Then FILLNAME(cmbname, edit, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validated(sender As Object, e As EventArgs) Handles cmbname.Validated
        Try
            Dim objclsCMST As New ClsCommonMaster

            ' 🔥 FIX: assign to dtLoom (NOT dt)
            dtLoom = objclsCMST.search(" LOOM_NO, BEAM_NAME, BEAM_NO ", "", "BEAMLOOMSTATUS",
            " AND WEAVER_NAME = '" & cmbname.Text.Trim & "' AND LOOM_STATUS = 'OCCUPIED' ORDER BY LOOM_NO ")

            If dtLoom IsNot Nothing AndAlso dtLoom.Rows.Count > 0 Then
                CMBLOOM.DataSource = dtLoom
                CMBLOOM.DisplayMember = "LOOM_NO"
                CMBLOOM.ValueMember = "LOOM_NO"
                CMBLOOM.ValueMember = "LOOM_NO"

                CMBLOOM.SelectedIndex = -1
            Else
                CMBLOOM.DataSource = Nothing
                CMBBEAMNAME.Text = ""
                TXTBEAMNO.Clear()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub CMBLOOM_Validated(sender As Object, e As EventArgs) Handles CMBLOOM.Validated
        Try
            If dtLoom IsNot Nothing Then
                For Each dr As DataRow In dtLoom.Rows
                    If dr("LOOM_NO").ToString.Trim = CMBLOOM.Text.Trim Then
                        CMBBEAMNAME.Text = dr("BEAM_NAME").ToString
                        TXTBEAMNO.Text = dr("BEAM_NO").ToString
                        Exit Sub
                    End If
                Next
            End If

            '' If not found
            'TXTBEAMNO.Clear()
            If gridDoubleClick = False Then   ' ← ADD THIS CHECK
                CMBBEAMNAME.Text = ""
                TXTBEAMNO.Clear()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TXTBEAMNO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTBEAMNO.KeyPress
        numkeypress(e, TXTBEAMNO, Me)
    End Sub

    Private Sub TXTRECMTRS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTRECMTRS.KeyPress, TXTRPM.KeyPress, TXTPICKS.KeyPress, TXTWEFT.KeyPress, TXTWARP.KeyPress, TXTEFFPER.KeyPress, TXTAVGPICK.KeyPress
        numdotkeypress(e, TXTRECMTRS, Me)
    End Sub
    Function CHECKLOOM() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In gridloan.Rows
                If (gridDoubleClick = True And tempRow <> ROW.Index) Or gridDoubleClick = False Then
                    If CMBLOOM.Text.Trim = ROW.Cells(GLOOM.Index).Value.ToString Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub CMBLOOM_Validating(sender As Object, e As CancelEventArgs) Handles CMBLOOM.Validating
        Try
            If gridloan.RowCount > 0 Then
                If Not CHECKLOOM() Then
                    MsgBox("Loom No already Present in Grid below")
                    CMBLOOM.Text = ""
                    e.Cancel = True
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub txtremarks_KeyDown(sender As Object, e As KeyEventArgs) Handles txtremarks.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then txtremarks.Text = OBJREMARKS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class