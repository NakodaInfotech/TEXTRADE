
Imports BL
Imports System.Windows.Forms
Public Class StoresLoan
    'following two variables is only for used in edit mode....
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim gridDoubleClick As Boolean
    Dim tempRow As Integer

    Public edit As Boolean
    Public TEMPloanNO As String
    Public tempMsg As Integer

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
            If cmbname.Text.Trim <> "" Then NAMEVALIDATE(cmbname, CMBCODE, e, Me, txtadd, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'  or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS') ", "Sundry Creditors")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitemname.Enter
        Try
            If cmbitemname.Text.Trim = "" Then fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'ITEM' AND (MATERIALTYPEMASTER.MATERIAL_NAME='Stores & Supplies - Production' or   MATERIALTYPEMASTER.MATERIAL_NAME='Pakaging Material')")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbitemname.Validating
        Try
            If cmbitemname.Text.Trim <> "" Then itemvalidate(cmbitemname, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'ITEM' and (MATERIALTYPEMASTER.MATERIAL_NAME='Stores & Supplies - Production' or   MATERIALTYPEMASTER.MATERIAL_NAME='Pakaging Material')", "ITEM")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


    Private Sub cmbqtyunit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbqtyunit.GotFocus
        Try
            If cmbqtyunit.Text.Trim = "" Then fillunit(cmbqtyunit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbqtyunit.Validating
        Try
            If cmbqtyunit.Text.Trim <> "" Then unitvalidate(cmbqtyunit, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub clear()

        tstxtbillno.Clear()
        cmbLoan.Text = ""
        loandate.Value = Mydate

        EP.Clear()
        txtsrno.Clear()
        cmbitemname.Text = ""
        txtgridremarks.Clear()
        txtqty.Clear()
        cmbqtyunit.Text = ""

        txtremarks.Clear()
        cmbname.Text = ""
        gridloan.RowCount = 0
        lbltotalqty.Text = 0.0

        lbllocked.Visible = False
        PBlock.Visible = False
        gridDoubleClick = False



        getmax_loan_no() 'this function is for to get max value from the Purchase loanuisition table

        If gridloan.RowCount > 0 Then
            txtsrno.Text = Val(gridloan.Rows(gridloan.RowCount - 1).Cells(gsrno.Index).Value) + 1
        Else
            txtsrno.Text = 1
        End If

    End Sub

    Sub getmax_loan_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(loan_no),0) + 1 ", "loanMaster", " AND loan_cmpid=" & CmpId & " and loan_LOCATIONID=" & Locationid & " and loan_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            txtloanno.Text = DTTABLE.Rows(0).Item(0)
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
            gridloan.Rows.Add(Val(txtsrno.Text.Trim), cmbitemname.Text.Trim, txtgridremarks.Text.Trim, Val(txtqty.Text.Trim), cmbqtyunit.Text.Trim, Val(txtamt.Text.Trim))
            getsrno(gridloan)
        ElseIf gridDoubleClick = True Then
            gridloan.Item(gsrno.Index, tempRow).Value = Val(txtsrno.Text.Trim)
            gridloan.Item(gitemname.Index, tempRow).Value = cmbitemname.Text.Trim
            gridloan.Item(gdesc.Index, tempRow).Value = txtgridremarks.Text.Trim
            gridloan.Item(gQty.Index, tempRow).Value = Val(txtqty.Text.Trim)
            gridloan.Item(gqtyunit.Index, tempRow).Value = cmbqtyunit.Text.Trim
            gridloan.Item(gAmt.Index, tempRow).Value = txtamt.Text.Trim

            gridDoubleClick = False

        End If

        gridloan.FirstDisplayedScrollingRowIndex = gridloan.RowCount - 1

        txtsrno.Clear()
        cmbitemname.Text = ""
        txtgridremarks.Clear()
        txtqty.Clear()
        txtamt.Clear()
        cmbqtyunit.Text = ""
        If gridloan.RowCount > 0 Then
            txtsrno.Text = Val(gridloan.Rows(gridloan.RowCount - 1).Cells(gsrno.Index).Value) + 1
        Else
            txtsrno.Text = 1
        End If
        txtsrno.Focus()

    End Sub

    Sub qtytotal()
        lbltotalqty.Text = "0.00"
        LBLTOTALAMT.Text = "0.00"
        For Each row As DataGridViewRow In gridloan.Rows
            If Val(row.Cells(gQty.Index).Value) <> 0 Then
                lbltotalqty.Text = Val(lbltotalqty.Text) + Val(row.Cells(gQty.Index).Value)
                LBLTOTALAMT.Text = Format(Val(LBLTOTALAMT.Text) + Val(row.Cells(gAmt.Index).Value), "0.00")
            End If
        Next
    End Sub

    Private Sub gridloan_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridloan.CellDoubleClick

        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.RowIndex >= 0 And gridloan.Item(gsrno.Index, e.RowIndex).Value <> Nothing Then
            If Convert.ToBoolean(gridloan.Item(GDONE.Index, e.RowIndex).Value) = False Then
                gridDoubleClick = True
                txtsrno.Text = gridloan.Item(gsrno.Index, e.RowIndex).Value
                cmbitemname.Text = gridloan.Item(gitemname.Index, e.RowIndex).Value
                txtgridremarks.Text = gridloan.Item(gdesc.Index, e.RowIndex).Value
                txtqty.Text = gridloan.Item(gQty.Index, e.RowIndex).Value
                txtamt.Text = gridloan.Item(gAmt.Index, e.RowIndex).Value
                cmbqtyunit.Text = gridloan.Item(gqtyunit.Index, e.RowIndex).Value
                tempRow = e.RowIndex
                txtsrno.Focus()
            Else
                MsgBox("Item Locked. First Delete Entry From Quotation")
            End If
        End If
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

            alParaval.Add(loandate.Value)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(cmbLoan.Text.Trim)
            alParaval.Add(lbltotalqty.Text.Trim)
            alParaval.Add(LBLTOTALAMT.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim gridsrno As String = ""
            Dim ITEMNAME As String = ""
            Dim gridremarks As String = ""
            Dim qty As String = ""
            Dim qtyunit As String = ""
            Dim AMT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In gridloan.Rows
                If row.Cells(gsrno.Index).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value
                        ITEMNAME = row.Cells(gitemname.Index).Value.ToString
                        gridremarks = row.Cells(gdesc.Index).Value.ToString
                        qty = Val(row.Cells(gQty.Index).Value)
                        qtyunit = row.Cells(gqtyunit.Index).Value
                        AMT = row.Cells(gAmt.Index).Value

                    Else
                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        ITEMNAME = ITEMNAME & "|" & row.Cells(gitemname.Index).Value.ToString
                        gridremarks = gridremarks & "|" & row.Cells(gdesc.Index).Value.ToString
                        qty = qty & "|" & Val(row.Cells(gQty.Index).Value)
                        qtyunit = qtyunit & "|" & row.Cells(gqtyunit.Index).Value
                        AMT = AMT & "|" & row.Cells(gAmt.Index).Value

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(ITEMNAME)
            alParaval.Add(gridremarks)
            alParaval.Add(qty)
            alParaval.Add(qtyunit)
            alParaval.Add(AMT)


            Dim objclsloan As New ClsLoan
            objclsloan.alParaval = alParaval

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objclsloan.save()
                MessageBox.Show("Details Added")
            Else
                alParaval.Add(TEMPloanNO)
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objclsloan.Update()
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
            EP.SetError(cmbname, "Enter loan. by")
            bln = False
        End If

        If cmbLoan.Text.Trim.Length = 0 Then
            EP.SetError(cmbLoan, "Enter Loan")
            bln = False
        End If

        If gridloan.RowCount = 0 Then
            EP.SetError(txtqty, "Enter Item Details")
            bln = False
        End If

        'If chkchange.CheckState = CheckState.Unchecked Then
        '    EP.SetError(txtqty, "Enter Item Details")
        '    bln = False
        'End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Quotation Raised, Delete Quotation First")
            bln = False
        End If

        If Not datecheck(loandate.Value) Then bln = False
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
        End If
    End Sub

    Private Sub Purchaseloanuisition_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If AscW(e.KeyChar) <> 33 Then
            chkchange.CheckState = CheckState.Checked
        End If
    End Sub

    Private Sub Loanmaster_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Cursor.Current = Cursors.WaitCursor

            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'STORES'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)



            fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'ITEM'")
            fillunit(cmbqtyunit)
            clear()

            If edit = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim ALPARAVAL As New ArrayList
                Dim objclsloan As New ClsStoresLoan

                ALPARAVAL.Add(TEMPloanNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)

                objclsloan.alParaval = ALPARAVAL
                Dim dt As DataTable = objclsloan.selectLoan()

                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows

                        txtloanno.Text = TEMPloanNO
                        loandate.Value = Convert.ToDateTime(dr("loanDATE"))
                        cmbname.Text = Convert.ToString(dr("NAME"))
                        cmbLoan.Text = Convert.ToString(dr("Loan").ToString)
                        txtremarks.Text = Convert.ToString(dr("remarks"))
                        gridloan.Rows.Add(dr("gridsrno").ToString, dr("ITEMNAME").ToString, dr("DESC").ToString, Val(dr("QTY")), dr("QTYUNIT").ToString, dr("amt").ToString)

                    Next
                    gridloan.FirstDisplayedScrollingRowIndex = gridloan.RowCount - 1
                End If

                chkchange.CheckState = CheckState.Checked
                qtytotal()
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

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtqty.KeyPress
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
                Dim TEMPMSG As Integer = MsgBox("Delete loan?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then

                    Dim ALPARAVAL As New ArrayList
                    Dim OBJLOAN As New ClsLoan

                    ALPARAVAL.Add(Val(txtloanno.Text.Trim))
                    ALPARAVAL.Add(CmpId)
                    ALPARAVAL.Add(Locationid)
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

    'Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
    '    Try
    '        If USEREDIT = False And USERVIEW = False Then
    '            MsgBox("Insufficient Rights")
    '            Exit Sub
    '        End If

    '        Dim objprdetails As New LoanDetail
    '        objprdetails.MdiParent = MDIMain
    '        objprdetails.Show()
    '        objprdetails.BringToFront()
    '        Me.Close()
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        TEMPloanNO = Val(txtloanno.Text) - 1
        clear()
        If TEMPloanNO > 0 Then
            edit = True
            Loanmaster_load(sender, e)
        Else
            clear()
            edit = False
        End If
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        TEMPloanNO = Val(txtloanno.Text) + 1
        getmax_loan_no()
        clear()
        If Val(txtloanno.Text) - 1 >= TEMPloanNO Then
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

    Private Sub loandate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles loandate.Validating
        If Not datecheck(loandate.Value) Then
            MsgBox("Date Not in Current Accounting Year")
            e.Cancel = True
        End If
    End Sub


    Private Sub gridpurchaseloan_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridloan.KeyDown
        Try
            If e.KeyCode = Keys.Delete And gridloan.RowCount > 0 Then


                gridloan.Rows.RemoveAt(gridloan.CurrentRow.Index)
                qtytotal()
                getsrno(gridloan)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub


    Private Sub cmbitemname_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbitemname.KeyPress
        commakeypress(e, cmbitemname, Me)
    End Sub

    Private Sub txtgridremarks_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtgridremarks.KeyPress
        commakeypress(e, txtgridremarks, Me)
    End Sub

    Private Sub cmbqtyunit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbqtyunit.KeyPress
        commakeypress(e, cmbqtyunit, Me)
    End Sub

    Private Sub tstxtbillno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tstxtbillno.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        TEMPloanNO = Val(tstxtbillno.Text)
        clear()
        If TEMPloanNO > 0 Then
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

    Private Sub txtamt_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtamt.Validated
        Try
            If cmbitemname.Text.Trim <> "" And cmbqtyunit.Text.Trim <> "" And Val(txtqty.Text.Trim) > 0 Then
                fillgrid()
                qtytotal()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class