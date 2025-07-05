

Imports System.ComponentModel
Imports BL

Public Class StoresPurchaseOrder

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPPONO As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Sub GETMAX_PO_NO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(PO_no),0) + 1 ", "STORESPURCHASEORDER", " and PO_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTPONO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()
        CMBNAME.Text = ""
        CMBNAME.Enabled = True

        PODATE.Text = Now.Date

        'GET LAST 3 MONTHS NAME
        GMONTH1.HeaderText = MonthName(Now.Date.AddMonths(-3).Month)
        GMONTH2.HeaderText = MonthName(Now.Date.AddMonths(-2).Month)
        GMONTH3.HeaderText = MonthName(Now.Date.AddMonths(-1).Month)


        TXTREFNO.Clear()
        CHKAUTOFILL.CheckState = CheckState.Unchecked

        TXTSRNO.Text = 1
        CMBSTORESITEMNAME.Text = ""
        TXTGRIDREMARKS.Clear()
        TXTMONTH1.Text = ""
        TXTMONTH2.Clear()
        TXTMONTH3.Clear()
        TXTSTOCK.Clear()
        TXTQTY.Clear()
        CMBQTYUNIT.Text = ""
        TXTRATE.Clear()
        TXTAMOUNT.Clear()

        EP.Clear()

        lbllocked.Visible = False
        LBLCLOSED.Visible = False
        PBlock.Visible = False
        txtremarks.Clear()

        txtinwords.Clear()
        txtadd.Clear()
        LBLTOTALAMT.Text = "0.00"
        LBLTOTALQTY.Text = "0.00"

        GRIDPO.RowCount = 0
        GRIDPUR.RowCount = 0

        GETMAX_PO_NO()
        GRIDDOUBLECLICK = False

    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Private Sub StoresPurchaseOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemQuotes Or e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.F5 Then     'grid focus
                GRIDPO.Focus()
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call TOOLPREVIOUS_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call TOOLNEXT_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call TOOLPRINT_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            FILLSTOREITEMNAME(CMBSTORESITEMNAME)
            fillunit(CMBQTYUNIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub StoresPurchaseOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'STORES'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            FILLCMB()
            CLEAR()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objclsPO As New ClsStoresPurchaseOrder()
                Dim dt_po As DataTable = objclsPO.SELECTPO(TEMPPONO, YearId)
                For Each dr As DataRow In dt_po.Rows

                    TXTPONO.Text = dr("PONO")
                    PODATE.Text = Format(Convert.ToDateTime(dr("PODATE")), "dd/MM/yyyy")
                    CMBNAME.Text = Convert.ToString(dr("NAME"))
                    TXTREFNO.Text = Convert.ToString(dr("REFNO"))
                    txtremarks.Text = Convert.ToString(dr("REMARKS"))

                    GRIDPO.Rows.Add(dr("POGRIDSRNO").ToString, dr("STORESITEMNAME").ToString, dr("GRIDREMARKS").ToString, Val(dr("MONTH1")), Val(dr("MONTH2")), Val(dr("MONTH3")), Val(dr("STOCK")), Format(Val(dr("QTY")), "0.00"), dr("UNIT").ToString, Format(Val(dr("RATE")), "0.00"), Format(Val(dr("AMT")), "0.00"), Val(dr("RECDQTY")), dr("GRIDPODONE"), dr("CLOSED"))

                    If Val(dr("RECDQTY")) > 0 Then
                        GRIDPO.Rows(GRIDPO.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
                        lbllocked.Visible = True
                        PBlock.Visible = True
                        CMBNAME.Enabled = False
                    End If

                    If Convert.ToBoolean(dr("CLOSED")) = True Then
                        GRIDPO.Rows(GRIDPO.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                        lbllocked.Visible = True
                        LBLCLOSED.Visible = True
                        PBlock.Visible = True
                    End If

                Next
                GRIDPO.FirstDisplayedScrollingRowIndex = GRIDPO.RowCount - 1
                TOTAL()
            Else
                EDIT = False
                CLEAR()
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click

        Try
            Cursor.Current = Cursors.WaitCursor

            Dim IntResult As Integer

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(PODATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(TXTREFNO.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(Val(LBLTOTALQTY.Text.Trim))
            alParaval.Add(Val(LBLTOTALAMT.Text.Trim))

            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim gridsrno As String = ""
            Dim STORESITEMNAME As String = ""
            Dim gridremarks As String = ""
            Dim MONTH1 As String = ""
            Dim MONTH2 As String = ""
            Dim MONTH3 As String = ""
            Dim STOCK As String = ""
            Dim qty As String = ""
            Dim qtyunit As String = ""
            Dim rate As String = ""
            Dim amount As String = ""
            Dim recdqty As String = ""      'Qty recd in GRN
            Dim GRNDONE As String = ""      'WHETHER GRN IS DONE FOR THIS LINE
            Dim CLOSED As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDPO.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        STORESITEMNAME = row.Cells(gstoreitemname.Index).Value.ToString
                        gridremarks = row.Cells(gdesc.Index).Value.ToString
                        MONTH1 = Val(row.Cells(GMONTH1.Index).Value)
                        MONTH2 = Val(row.Cells(GMONTH2.Index).Value)
                        MONTH3 = Val(row.Cells(GMONTH3.Index).Value)
                        STOCK = Val(row.Cells(GSTOCK.Index).Value)
                        qty = row.Cells(gQty.Index).Value.ToString
                        qtyunit = row.Cells(gqtyunit.Index).Value.ToString
                        rate = row.Cells(grate.Index).Value
                        amount = row.Cells(gamt.Index).Value
                        recdqty = row.Cells(grecdqty.Index).Value
                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then GRNDONE = 1 Else GRNDONE = 0
                        If Convert.ToBoolean(row.Cells(GCLOSED.Index).Value) = True Then CLOSED = 1 Else CLOSED = 0

                    Else

                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        STORESITEMNAME = STORESITEMNAME & "|" & row.Cells(gstoreitemname.Index).Value
                        gridremarks = gridremarks & "|" & row.Cells(gdesc.Index).Value.ToString
                        MONTH1 = MONTH1 & "|" & Val(row.Cells(GMONTH1.Index).Value)
                        MONTH2 = MONTH2 & "|" & Val(row.Cells(GMONTH2.Index).Value)
                        MONTH3 = MONTH3 & "|" & Val(row.Cells(GMONTH3.Index).Value)
                        STOCK = STOCK & "|" & Val(row.Cells(GSTOCK.Index).Value)
                        qty = qty & "|" & row.Cells(gQty.Index).Value
                        qtyunit = qtyunit & "|" & row.Cells(gqtyunit.Index).Value
                        rate = rate & "|" & row.Cells(grate.Index).Value
                        amount = amount & "|" & row.Cells(gamt.Index).Value
                        recdqty = recdqty & "|" & row.Cells(grecdqty.Index).Value
                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then GRNDONE = GRNDONE & "|" & 1 Else GRNDONE = GRNDONE & "|" & 0
                        If Convert.ToBoolean(row.Cells(GCLOSED.Index).Value) = True Then CLOSED = CLOSED & "|" & 1 Else CLOSED = CLOSED & "|" & 0

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(STORESITEMNAME)
            alParaval.Add(gridremarks)
            alParaval.Add(MONTH1)
            alParaval.Add(MONTH2)
            alParaval.Add(MONTH3)
            alParaval.Add(STOCK)
            alParaval.Add(qty)
            alParaval.Add(qtyunit)
            alParaval.Add(rate)
            alParaval.Add(amount)
            alParaval.Add(recdqty)
            alParaval.Add(GRNDONE)
            alParaval.Add(CLOSED)

            alParaval.Add(txtinwords.Text.Trim)

            Dim objclsPurord As New ClsStoresPurchaseOrder()
            objclsPurord.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = objclsPurord.SAVE()
                MessageBox.Show("Details Added")
                TXTPONO.Text = DT.Rows(0).Item(0)
            Else
                alParaval.Add(TEMPPONO)

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                IntResult = objclsPurord.UPDATE()
                MessageBox.Show("Details Updated")
                EDIT = False
            End If

            PRINTREPORT()

            CLEAR()
            CMBNAME.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Try

            Dim bln As Boolean = True
            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, " Please Fill Company Name ")
                bln = False
            End If

            If lbllocked.Visible = True Or LBLCLOSED.Visible = True Then
                EP.SetError(lbllocked, "Entry Locked")
                bln = False
            End If
            If Convert.ToDateTime(PODATE.Text).Date < STOREPOBLOCKDATE.Date Then
                EP.SetError(PODATE, "Date is Blocked, Please make entries after " & Format(STOREPOBLOCKDATE.Date, "dd/MM/yyyy"))
                bln = False
            End If


LINE1:
            'WE HAVE TO REMOVE ALL 0 ITEMS WHICH COMES AUTO FROM AUTOFILL
            For Each ROW As DataGridViewRow In GRIDPO.Rows
                If Val(ROW.Cells(gQty.Index).Value) <= 0 Then
                    GRIDPO.Rows.RemoveAt(ROW.Index)
                    GoTo LINE1
                End If
            Next
            GETSRNO(GRIDPO)


            If PODATE.Text = "__/__/____" Then
                EP.SetError(PODATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(PODATE.Text) Then
                    EP.SetError(PODATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If
            TOTAL()
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub TOTAL()
        Try
            LBLTOTALQTY.Text = "0.00"
            LBLTOTALAMT.Text = "0.00"

            For Each row As DataGridViewRow In GRIDPO.Rows
                If Val(row.Cells(gQty.Index).EditedFormattedValue) <> 0 Then LBLTOTALQTY.Text = Format(Val(LBLTOTALQTY.Text) + Val(row.Cells(gQty.Index).Value), "0.00")
                If Val(row.Cells(grate.Index).EditedFormattedValue) <> 0 Then row.Cells(gamt.Index).Value = Format(Val(row.Cells(grate.Index).EditedFormattedValue) * Val(row.Cells(gQty.Index).EditedFormattedValue), "0.00")
                If Val(row.Cells(gamt.Index).EditedFormattedValue) <> 0 Then LBLTOTALAMT.Text = Val(LBLTOTALAMT.Text) + Val(row.Cells(gamt.Index).EditedFormattedValue)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.GotFocus
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'  AND LEDGERS.ACC_TYPE='ACCOUNTS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTSRNO.GotFocus
        If GRIDDOUBLECLICK = False Then TXTSRNO.Text = GRIDPO.RowCount + 1
    End Sub

    Private Sub cmbstoresitemname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSTORESITEMNAME.Enter
        Try
            If CMBSTORESITEMNAME.Text.Trim = "" Then FILLSTOREITEMNAME(CMBSTORESITEMNAME)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbstoresitemname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSTORESITEMNAME.Validating
        Try
            If CMBSTORESITEMNAME.Text.Trim <> "" Then STOREITEMVALIDATE(CMBSTORESITEMNAME, e, Me, CMBQTYUNIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSTORESITEMNAME_Validated(sender As Object, e As EventArgs) Handles CMBSTORESITEMNAME.Validated
        Try
            If CMBSTORESITEMNAME.Text.Trim <> "" Then
                GETCONSUMPTION(CMBSTORESITEMNAME.Text.Trim)
                GETSTOCK(CMBSTORESITEMNAME.Text.Trim)
                GETPURCHASE(CMBSTORESITEMNAME.Text.Trim)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETSRNO(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        GRIDPO.Enabled = True
        If GRIDDOUBLECLICK = False Then
            GRIDPO.Rows.Add(Val(TXTSRNO.Text.Trim), CMBSTORESITEMNAME.Text.Trim, TXTGRIDREMARKS.Text.Trim, Val(TXTMONTH1.Text.Trim), Val(TXTMONTH2.Text.Trim), Val(TXTMONTH3.Text.Trim), Val(TXTSTOCK.Text.Trim), Val(TXTQTY.Text.Trim), CMBQTYUNIT.Text.Trim, Val(TXTRATE.Text.Trim), Val(TXTAMOUNT.Text.Trim), 0, 0, 0)
            GETSRNO(GRIDPO)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDPO.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDPO.Item(gstoreitemname.Index, TEMPROW).Value = CMBSTORESITEMNAME.Text.Trim
            GRIDPO.Item(gdesc.Index, TEMPROW).Value = TXTGRIDREMARKS.Text.Trim
            GRIDPO.Item(GMONTH1.Index, TEMPROW).Value = Val(TXTMONTH1.Text.Trim)
            GRIDPO.Item(GMONTH2.Index, TEMPROW).Value = Val(TXTMONTH2.Text.Trim)
            GRIDPO.Item(GMONTH3.Index, TEMPROW).Value = Val(TXTMONTH3.Text.Trim)
            GRIDPO.Item(GSTOCK.Index, TEMPROW).Value = Val(TXTSTOCK.Text.Trim)
            GRIDPO.Item(gQty.Index, TEMPROW).Value = Val(TXTQTY.Text.Trim)
            GRIDPO.Item(gqtyunit.Index, TEMPROW).Value = CMBQTYUNIT.Text.Trim
            GRIDPO.Item(grate.Index, TEMPROW).Value = Val(TXTRATE.Text.Trim)
            GRIDPO.Item(gamt.Index, TEMPROW).Value = Val(TXTAMOUNT.Text.Trim)
            GRIDDOUBLECLICK = False
        End If

        GRIDPO.FirstDisplayedScrollingRowIndex = GRIDPO.RowCount - 1

        TXTSRNO.Text = GRIDPO.RowCount + 1
        CMBSTORESITEMNAME.Text = ""
        TXTGRIDREMARKS.Clear()
        TXTMONTH1.Clear()
        TXTMONTH2.Clear()
        TXTMONTH3.Clear()
        TXTSTOCK.Clear()
        TXTQTY.Clear()
        CMBQTYUNIT.Text = ""
        TXTRATE.Clear()
        TXTAMOUNT.Clear()

        CMBSTORESITEMNAME.Focus()

    End Sub

    Private Sub gridPO_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDPO.CellDoubleClick
        If e.RowIndex >= 0 And GRIDPO.Item(gsrno.Index, e.RowIndex).Value <> Nothing Then
            EDITROW()
        End If
    End Sub

    Private Sub txtrate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTRATE.Validated
        CALC()
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress, TXTRATE.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PODATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles PODATE.GotFocus
        PODATE.SelectionStart = 0
    End Sub

    Private Sub PODATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PODATE.Validating
        Try
            If PODATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(PODATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    'GET LAST 3 MONTHS NAME
                    GMONTH1.HeaderText = MonthName(Convert.ToDateTime(PODATE.Text).Date.AddMonths(-3).Month)
                    GMONTH2.HeaderText = MonthName(Convert.ToDateTime(PODATE.Text).Date.AddMonths(-2).Month)
                    GMONTH3.HeaderText = MonthName(Convert.ToDateTime(PODATE.Text).Date.AddMonths(-1).Month)
                End If
            Else
                If Not datecheck(PODATE.Text) Then
                    MsgBox("Date not in Accounting Year")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Dim IntResult As Integer
        Try

            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Or LBLCLOSED.Visible = True Then
                    MsgBox("PO Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete Purchase Order ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim alParaval As New ArrayList
                alParaval.Add(TEMPPONO)
                alParaval.Add(YearId)

                Dim clspo As New ClsPurchaseOrder()
                clspo.alParaval = alParaval
                IntResult = clspo.Delete()
                MsgBox("Purchase Order Deleted")
                CLEAR()
                EDIT = False

            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    'Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
    '    Try
    '        If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'  AND ISNULL(LEDGERS.ACC_TYPE,'ACCOUNTS') = 'ACCOUNTS' ", "Sundry Creditors", "ACCOUNTS")
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub

    Private Sub cmbqtyunit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQTYUNIT.GotFocus
        Try
            If CMBQTYUNIT.Text.Trim = "" Then fillunit(CMBQTYUNIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQTYUNIT.Validating
        Try
            If CMBQTYUNIT.Text.Trim <> "" Then unitvalidate(CMBQTYUNIT, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridPO_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDPO.CellValidating
        ''  CODE FOR NUMERIC CHECK ONLY
        Dim colNum As Integer = GRIDPO.Columns(e.ColumnIndex).Index
        If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

        Select Case colNum

            Case grate.Index, gQty.Index
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                If bValid Then
                    If GRIDPO.CurrentCell.Value = Nothing Then GRIDPO.CurrentCell.Value = "0.00"
                    GRIDPO.CurrentCell.Value = Convert.ToDecimal(GRIDPO.Item(colNum, e.RowIndex).Value)
                    '' everything is good
                    GRIDPO.Rows(e.RowIndex).Cells(gamt.Index).Value = Format(Val(GRIDPO.Rows(e.RowIndex).Cells(grate.Index).EditedFormattedValue) * Val(GRIDPO.Rows(e.RowIndex).Cells(gQty.Index).EditedFormattedValue), "0.00")
                Else
                    MessageBox.Show("Invalid Number Entered")
                    e.Cancel = True
                End If
                TOTAL()

        End Select
    End Sub

    Sub EDITROW()
        Try
            If GRIDPO.CurrentRow.Index >= 0 And GRIDPO.Item(gsrno.Index, GRIDPO.CurrentRow.Index).Value <> Nothing Then

                If Convert.ToBoolean(GRIDPO.CurrentRow.Cells(GDONE.Index).Value) = True Or Convert.ToBoolean(GRIDPO.CurrentRow.Cells(GCLOSED.Index).Value) = True Then
                    MsgBox("Item Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = Val(GRIDPO.Item(gsrno.Index, GRIDPO.CurrentRow.Index).Value)
                CMBSTORESITEMNAME.Text = GRIDPO.Item(gstoreitemname.Index, GRIDPO.CurrentRow.Index).Value.ToString
                TXTGRIDREMARKS.Text = GRIDPO.Item(gdesc.Index, GRIDPO.CurrentRow.Index).Value.ToString
                TXTMONTH1.Text = Val(GRIDPO.Item(GMONTH1.Index, GRIDPO.CurrentRow.Index).Value)
                TXTMONTH2.Text = Val(GRIDPO.Item(GMONTH2.Index, GRIDPO.CurrentRow.Index).Value)
                TXTMONTH3.Text = Val(GRIDPO.Item(GMONTH3.Index, GRIDPO.CurrentRow.Index).Value)
                TXTSTOCK.Text = Val(GRIDPO.Item(GSTOCK.Index, GRIDPO.CurrentRow.Index).Value)
                TXTQTY.Text = Val(GRIDPO.Item(gQty.Index, GRIDPO.CurrentRow.Index).Value)
                CMBQTYUNIT.Text = GRIDPO.Item(gqtyunit.Index, GRIDPO.CurrentRow.Index).Value.ToString
                TXTRATE.Text = Val(GRIDPO.Item(grate.Index, GRIDPO.CurrentRow.Index).Value)
                TXTAMOUNT.Text = Val(GRIDPO.Item(gamt.Index, GRIDPO.CurrentRow.Index).Value)

                TEMPROW = GRIDPO.CurrentRow.Index
                CMBSTORESITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridPO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDPO.KeyDown

        Try
            If e.KeyCode = Keys.Delete And GRIDPO.RowCount > 0 Then

                If Convert.ToBoolean(GRIDPO.Rows(GRIDPO.CurrentRow.Index).Cells(GDONE.Index).Value) = True Or Convert.ToBoolean(GRIDPO.Rows(GRIDPO.CurrentRow.Index).Cells(GCLOSED.Index).Value) = True Then
                    MsgBox("Item Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                GRIDPO.Rows.RemoveAt(GRIDPO.CurrentRow.Index)
                TOTAL()
                GETSRNO(GRIDPO)
                TOTAL()

            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDPO.RowCount = 0
                TEMPPONO = Val(tstxtbillno.Text)
                If TEMPPONO > 0 Then
                    EDIT = True
                    StoresPurchaseOrder_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSeparator1.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objpodtls As New PurchaseOrderDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSeparator4.Click
        Call cmdok_Click(sender, e)
    End Sub

    Sub PRINTREPORT()
        Try
            If MsgBox("Wish to Print Order?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJPO As New PurchaseOrderDesign
            OBJPO.MdiParent = MDIMain
            OBJPO.FRMSTRING = "STORESPOREPORT"
            OBJPO.FORMULA = "{STORESPURCHASEORDER.PO_NO}=" & Val(TXTPONO.Text) & " and {STORESPURCHASEORDER.PO_yearid}=" & YearId
            OBJPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Try
            If EDIT = True Then
                PRINTREPORT()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TOOLPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLPREVIOUS.Click
        Try
            GRIDPO.RowCount = 0
LINE1:
            TEMPPONO = Val(TXTPONO.Text) - 1
            If TEMPPONO > 0 Then
                EDIT = True
                StoresPurchaseOrder_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDPO.RowCount = 0 And TEMPPONO > 1 Then
                TXTPONO.Text = TEMPPONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TOOLNEXT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLNEXT.Click
        Try
            GRIDPO.RowCount = 0
LINE1:
            TEMPPONO = Val(TXTPONO.Text) + 1
            GETMAX_PO_NO()
            Dim MAXNO As Integer = TXTPONO.Text.Trim
            CLEAR()
            If Val(TXTPONO.Text) - 1 >= TEMPPONO Then
                EDIT = True
                StoresPurchaseOrder_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDPO.RowCount = 0 And TEMPPONO < MAXNO Then
                TXTPONO.Text = TEMPPONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtqty_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTQTY.Validating
        CALC()
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then cmbcode.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            TXTAMOUNT.Text = Format(Val(TXTQTY.Text) * Val(TXTRATE.Text), "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbstoresitemname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBSTORESITEMNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "ITEM"
                OBJItem.STRSEARCH = " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then CMBSTORESITEMNAME.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtremarks_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtremarks.KeyDown
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

    Private Sub TOOLPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLPRINT.Click
        Try
            If EDIT = True Then PRINTREPORT()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objINVDTLS As New StoresPurchaseOrderDetails
            objINVDTLS.MdiParent = MDIMain
            objINVDTLS.Show()
            objINVDTLS.BringToFront()
            ' Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub TXTAMOUNT_Validated(sender As Object, e As EventArgs) Handles TXTAMOUNT.Validated
        Try
            If CMBSTORESITEMNAME.Text.Trim <> "" And Val(TXTQTY.Text.Trim) > 0 And CMBQTYUNIT.Text.Trim <> "" Then
                FILLGRID()
                TOTAL()
            Else
                MsgBox("Enter Proper Details")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(sender As Object, e As EventArgs) Handles CMBNAME.Validated
        Try
            'FETCH ALL ITEMNAMES WITH RESPECT TO SELECTED PARTY
            If CMBNAME.Text.Trim <> "" And CHKAUTOFILL.CheckState = CheckState.Checked Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(STOREITEM_NAME,'') AS STORESITEMNAME, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT", "", " STOREITEMMASTER INNER JOIN LEDGERS ON STOREITEM_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN UNITMASTER ON STOREITEM_UNITID = UNITMASTER.UNIT_ID ", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND STOREITEMMASTER.STOREITEM_YEARID = " & YearId & " ORDER BY STOREITEMMASTER.STOREITEM_NAME")
                For Each DTROW As DataRow In DT.Rows
                    GETCONSUMPTION(DTROW("STORESITEMNAME"))
                    GETSTOCK(DTROW("STORESITEMNAME"))
                    GRIDPO.Rows.Add(0, DTROW("STORESITEMNAME"), "", Val(TXTMONTH1.Text.Trim), Val(TXTMONTH2.Text.Trim), Val(TXTMONTH3.Text.Trim), Val(TXTSTOCK.Text.Trim), 0, DTROW("UNIT"), 0, 0, 0, 0, 0)
                    TXTMONTH1.Clear()
                    TXTMONTH2.Clear()
                    TXTMONTH3.Clear()
                    TXTSTOCK.Clear()
                Next
                GETSRNO(GRIDPO)
                CMBNAME.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETCONSUMPTION(STORESITEMNAME As String)
        Try
            'GET LAST 3 MONTHIS CONSUMPTION WITH RESPECT TO ITEMNAME AND IRRESPECTIVE TO YEARID
            'DONT ADD YEARID CLAUSE
            Dim MONTH1, MONTH2, MONTH3 As Integer
            MONTH1 = Convert.ToDateTime(PODATE.Text).Date.AddMonths(-3).Month
            MONTH2 = Convert.ToDateTime(PODATE.Text).Date.AddMonths(-2).Month
            MONTH3 = Convert.ToDateTime(PODATE.Text).Date.AddMonths(-1).Month

            Dim TEMPDATE As Date = (Convert.ToDateTime(PODATE.Text).Date.AddMonths(-3))
            Dim STARTDATE As New Date(TEMPDATE.Year, MONTH1, 1)
            Dim ENDDATE As New Date(TEMPDATE.Year, MONTH1, TEMPDATE.DaysInMonth(TEMPDATE.Year, TEMPDATE.Month))


            Dim OBJCMN As New ClsCommon
            'Dim DT As DataTable = OBJCMN.search("ISNULL(SUM(CONSUME_QTY),0) AS QTY", "", " CONSUMPTION_DESC INNER JOIN CONSUMPTION ON CONSUMPTION.CONSUME_NO = CONSUMPTION_DESC.CONSUME_no AND CONSUMPTION.CONSUME_YEARID = CONSUMPTION_DESC.CONSUME_yearid INNER JOIN ITEMMASTER ON CONSUMPTION_DESC.CONSUME_ITEMID = ITEM_ID  ", " AND ITEMMASTER.ITEM_NAME = '" & ITEMNAME & "' AND CONSUME_DATE>= '" & Format(STARTDATE.Date, "MM/dd/yyyy") & "' AND CONSUME_DATE <= '" & Format(ENDDATE.Date, "MM/dd/yyyy") & "'")
            Dim DT As DataTable = OBJCMN.search("ISNULL(SUM(ISSQTY),0) AS QTY", "", " STORESTOCKREGISTER ", " AND ITEMNAME = '" & STORESITEMNAME & "' AND DATE>= '" & Format(STARTDATE.Date, "MM/dd/yyyy") & "' AND DATE <= '" & Format(ENDDATE.Date, "MM/dd/yyyy") & "' AND ISSREC= 'ITEM ISSUE'")
            If DT.Rows.Count > 0 Then TXTMONTH1.Text = Val(DT.Rows(0).Item("QTY"))


            TEMPDATE = (Convert.ToDateTime(PODATE.Text).Date.AddMonths(-2))
            STARTDATE = New Date(TEMPDATE.Year, MONTH2, 1)
            ENDDATE = New Date(TEMPDATE.Year, MONTH2, TEMPDATE.DaysInMonth(TEMPDATE.Year, TEMPDATE.Month))

            'DT = OBJCMN.search("ISNULL(SUM(CONSUME_QTY),0) AS QTY", "", " CONSUMPTION_DESC INNER JOIN CONSUMPTION ON CONSUMPTION.CONSUME_NO = CONSUMPTION_DESC.CONSUME_no AND CONSUMPTION.CONSUME_YEARID = CONSUMPTION_DESC.CONSUME_yearid INNER JOIN ITEMMASTER ON CONSUMPTION_DESC.CONSUME_ITEMID = ITEM_ID  ", " AND ITEMMASTER.ITEM_NAME = '" & ITEMNAME & "' AND CONSUME_DATE>= '" & Format(STARTDATE.Date, "MM/dd/yyyy") & "' AND CONSUME_DATE <= '" & Format(ENDDATE.Date, "MM/dd/yyyy") & "'")
            DT = OBJCMN.search("ISNULL(SUM(ISSQTY),0) AS QTY", "", " STORESTOCKREGISTER ", " AND ITEMNAME = '" & STORESITEMNAME & "' AND DATE>= '" & Format(STARTDATE.Date, "MM/dd/yyyy") & "' AND DATE <= '" & Format(ENDDATE.Date, "MM/dd/yyyy") & "' AND ISSREC= 'ITEM ISSUE'")
            If DT.Rows.Count > 0 Then TXTMONTH2.Text = Val(DT.Rows(0).Item("QTY"))


            TEMPDATE = (Convert.ToDateTime(PODATE.Text).Date.AddMonths(-1))
            STARTDATE = New Date(TEMPDATE.Year, MONTH3, 1)
            ENDDATE = New Date(TEMPDATE.Year, MONTH3, TEMPDATE.DaysInMonth(TEMPDATE.Year, TEMPDATE.Month))

            'DT = OBJCMN.search("ISNULL(SUM(CONSUME_QTY),0) AS QTY", "", " CONSUMPTION_DESC INNER JOIN CONSUMPTION ON CONSUMPTION.CONSUME_NO = CONSUMPTION_DESC.CONSUME_no AND CONSUMPTION.CONSUME_YEARID = CONSUMPTION_DESC.CONSUME_yearid INNER JOIN ITEMMASTER ON CONSUMPTION_DESC.CONSUME_ITEMID = ITEM_ID  ", " AND ITEMMASTER.ITEM_NAME = '" & ITEMNAME & "' AND CONSUME_DATE>= '" & Format(STARTDATE.Date, "MM/dd/yyyy") & "' AND CONSUME_DATE <= '" & Format(ENDDATE.Date, "MM/dd/yyyy") & "'")
            DT = OBJCMN.search("ISNULL(SUM(ISSQTY),0) AS QTY", "", " STORESTOCKREGISTER ", " AND ITEMNAME = '" & STORESITEMNAME & "' AND DATE>= '" & Format(STARTDATE.Date, "MM/dd/yyyy") & "' AND DATE <= '" & Format(ENDDATE.Date, "MM/dd/yyyy") & "' and ISSREC= 'ITEM ISSUE'")
            If DT.Rows.Count > 0 Then TXTMONTH3.Text = Val(DT.Rows(0).Item("QTY"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETSTOCK(STORESITEMNAME As String)
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" ROUND(ISNULL(SUM(QTY),0)-ISNULL(SUM(ISSQTY),0),2) AS BALANCE ", "", " STORESTOCKREGISTER ", " AND ITEMNAME = '" & STORESITEMNAME & "' AND YEARID =" & YearId)
            If DT.Rows.Count > 0 Then TXTSTOCK.Text = Val(DT.Rows(0).Item("BALANCE"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETPURCHASE(STORESITEMNAME As String)
        Try
            GRIDPUR.RowCount = 0
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" TOP 5 ISNULL(STORESPURCHASEORDER.PO_NO,0) AS BILLNO, STORESPURCHASEORDER.PO_DATE AS BILLDATE, ISNULL(LEDGERS.ACC_CMPNAME,'') AS NAME, ISNULL(STORESPURCHASEORDER_DESC.PO_QTY,0) AS QTY, ISNULL(STORESPURCHASEORDER_DESC.PO_RATE,0) AS RATE, ISNULL(STORESPURCHASEORDER_DESC.PO_AMT,0) AS AMT ", "", " STORESPURCHASEORDER INNER JOIN STORESPURCHASEORDER_DESC ON STORESPURCHASEORDER.PO_NO = STORESPURCHASEORDER_DESC.PO_NO AND STORESPURCHASEORDER.PO_YEARID = STORESPURCHASEORDER_DESC.PO_YEARID INNER JOIN LEDGERS ON STORESPURCHASEORDER.PO_LEDGERID = ACC_ID INNER JOIN STOREITEMMASTER ON PO_ITEMID  = STOREITEM_ID ", " AND STOREITEMMASTER.STOREITEM_NAME = '" & STORESITEMNAME & "' ORDER BY STORESPURCHASEORDER.PO_DATE DESC  ")
            For Each DTROW As DataRow In DT.Rows
                GRIDPUR.Rows.Add(0, Format(Convert.ToDateTime(DTROW("BILLDATE")).Date, "dd/MM/yyyy"), DTROW("NAME"), Format(Val(DTROW("QTY")), "0.00"), Format(Val(DTROW("RATE")), "0.00"), Format(Val(DTROW("AMT")), "0.00"))
            Next
            GETSRNO(GRIDPUR)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SENDWHATSAPP(TEMPPONO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(PONO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJPO As New PurchaseOrderDesign
            OBJPO.MdiParent = MDIMain
            OBJPO.DIRECTPRINT = True
            OBJPO.FRMSTRING = "STORESPOREPORT"
            OBJPO.DIRECTMAIL = True
            OBJPO.PARTYNAME = CMBNAME.Text.Trim
            OBJPO.FORMULA = " {STORESPURCHASEORDER.PO_NO}=" & Val(TXTPONO.Text) & " and {STORESPURCHASEORDER.PO_yearid}=" & YearId
            OBJPO.PONO = PONO
            OBJPO.NOOFCOPIES = 1
            OBJPO.Show()
            OBJPO.Close()


            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\POREPORT_" & Val(PONO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add("POREPORT_" & Val(PONO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDPO_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDPO.RowEnter
        Try
            If e.RowIndex >= 0 AndAlso GRIDPO.SelectedRows.Count > 0 Then GETPURCHASE(GRIDPO.Rows(e.RowIndex).Cells(gstoreitemname.Index).EditedFormattedValue)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class