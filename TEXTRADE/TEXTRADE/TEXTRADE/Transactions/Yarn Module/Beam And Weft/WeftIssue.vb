
Imports BL
Imports System.IO

Public Class WeftIssue

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Public TEMPISSUENO As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Dim IntResult As Integer
    Dim TEMPMSG As Integer

    Sub clear()

        tstxtbillno.Clear()
        EP.Clear()
        CMBNAME.Enabled = True
        CMBNAME.Text = ""
        INWARDDATE.Text = Now.Date
        tstxtbillno.Clear()
        CMBTRANS.Text = ""
        TXTPARTYCHALLANNO.Clear()
        TXTAPPROXVALUE.Clear()
        txtsrno.Text = 1
        CMBYARNQUALITY.Text = ""
        CMBMILLNAME.Text = ""
        TXTCARTOONNO.Clear()
        TXTCONES.Clear()
        TXTLOTNO.Clear()
        TXTWT.Clear()
        cmbcolor.Text = ""
        lbltotalCARTOON.Text = 0
        LBLTOTALCONES.Text = 0
        LBLTOTALWT.Text = 0

        gridgrn.RowCount = 0
        getmax_BILL_no()

    End Sub

    Sub getmax_BILL_no()
        Dim DTTABLE As DataTable = getmax(" isnull(max(WEFTISS_NO),0) + 1 ", "  WEFTISSUEMASTER ", " AND WEFTISS_YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTINWARDNO.Text = DTTABLE.Rows(0).Item(0)
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

    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, " Please Fill Party Name ")
                bln = False
            End If

            If gridgrn.RowCount = 0 Then
                EP.SetError(CMBNAME, "Fill Item Details")
                bln = False
            End If

            If Val(TXTWT.Text.Trim) > 0 Then
                EP.SetError(TXTWT, "Enter Nett Wt.")
                bln = False
            End If

            If INWARDDATE.Text = "__/__/____" Then
                EP.SetError(INWARDDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(INWARDDATE.Text) Then
                    EP.SetError(INWARDDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub WEFTISSUE_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call toolprevious_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call toolnext_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.F5 Then
                gridgrn.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Sub FILLCMB()
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        If CMBMILLNAME.Text.Trim = "" Then fillmill(CMBMILLNAME, EDIT)
        If CMBYARNQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBYARNQUALITY, EDIT)
        FILLCOLOR(cmbcolor, "", "")
    End Sub

    Sub total()
        Try
            LBLTOTALCONES.Text = 0.0
            LBLTOTALWT.Text = 0.0

            For Each ROW As DataGridViewRow In gridgrn.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    'lbltotalqty.Text = Format(Val(lbltotalqty.Text) + Val(ROW.Cells(gQty.Index).EditedFormattedValue), "0.00")
                    'If ROW.Cells(gcut.Index).EditedFormattedValue > 0 Then ROW.Cells(GMTRS.Index).Value = Val(ROW.Cells(gQty.Index).EditedFormattedValue) * Val(ROW.Cells(gcut.Index).EditedFormattedValue)
                    LBLTOTALCONES.Text = Format(Val(LBLTOTALCONES.Text) + Val(ROW.Cells(GCONES.Index).EditedFormattedValue), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GNETWT.Index).EditedFormattedValue), "0.00")
                End If
            Next
            CARTOONCOUNT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CARTOONCOUNT()
        Try
            lbltotalCARTOON.Text = 0
            Dim dic As New Dictionary(Of String, Integer)()
            Dim cellValue As String
            For i = 0 To gridgrn.Rows.Count - 1
                If Not gridgrn.Rows(i).IsNewRow Then
                    cellValue = gridgrn(GCARTOONNO.Index, i).EditedFormattedValue.ToString()
                    If cellValue <> "" Then
                        If Not dic.ContainsKey(cellValue) Then
                            dic.Add(cellValue, 1)
                        Else
                            dic(cellValue) += 1
                        End If
                    End If
                End If
            Next
            lbltotalCARTOON.Text = Val(dic.Count)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS'", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' ")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTRANS.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBTRANS.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then namevalidate(CMBTRANS, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT' ", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Private Sub CMBYARNQUALITY_enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBYARNQUALITY.Enter
        Try
            If CMBYARNQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBYARNQUALITY, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBYARNQUALITY.Validating
        Try
            If CMBYARNQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBYARNQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBmilname_enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILLNAME.Enter
        Try
            If CMBMILLNAME.Text.Trim = "" Then fillmill(CMBMILLNAME, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILLNAME.Validating
        Try
            If CMBMILLNAME.Text.Trim <> "" Then MILLVALIDATE(CMBMILLNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcolor.Enter
        Try
            If cmbcolor.Text.Trim = "" Then FILLCOLOR(cmbcolor, "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbcolor.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCOLOR As New SelectShade
                OBJCOLOR.ShowDialog()
                If OBJCOLOR.TEMPNAME <> "" Then CMBCOLOR.Text = OBJCOLOR.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcolor.Validating
        Try
            If cmbcolor.Text.Trim <> "" Then COLORVALIDATE(cmbcolor, e, Me, "", "")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            gridgrn.RowCount = 0
LINE1:
            TEMPISSUENO = Val(TXTINWARDNO.Text) - 1
            If TEMPISSUENO > 0 Then
                EDIT = True
                WeftIssue_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If gridgrn.RowCount = 0 And TEMPISSUENO > 1 Then
                TXTINWARDNO.Text = TEMPISSUENO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            gridgrn.RowCount = 0
LINE1:
            TEMPISSUENO = Val(TXTINWARDNO.Text) + 1
            getmax_BILL_no()
            Dim MAXNO As Integer = TXTINWARDNO.Text.Trim
            clear()
            If Val(TXTINWARDNO.Text) - 1 >= TEMPISSUENO Then
                EDIT = True
                WeftIssue_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If gridgrn.RowCount = 0 And TEMPISSUENO < MAXNO Then
                TXTINWARDNO.Text = TEMPISSUENO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            alParaval.Add(Format(Convert.ToDateTime(INWARDDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBTRANS.Text.Trim)
            alParaval.Add(TXTPARTYCHALLANNO.Text.Trim)
            alParaval.Add(Val(TXTAPPROXVALUE.Text.Trim))
            alParaval.Add(Val(lbltotalCARTOON.Text.Trim))
            alParaval.Add(Val(LBLTOTALCONES.Text.Trim))
            alParaval.Add(Val(LBLTOTALWT.Text.Trim))

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim GRIDSRNO As String = ""
            Dim YARNQUALITY As String = ""
            Dim CARTOONNO As String = ""
            Dim CONES As String = ""
            Dim LOTNO As String = ""
            Dim NETWT As String = ""
            Dim COLOR As String = ""
            Dim MILLNAME As String = ""


            For Each row As Windows.Forms.DataGridViewRow In gridgrn.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(row.Cells(gsrno.Index).Value)
                        CARTOONNO = row.Cells(GCARTOONNO.Index).Value.ToString
                        YARNQUALITY = row.Cells(GYARNQUALITY.Index).Value.ToString
                        CONES = Val(row.Cells(GCONES.Index).Value)
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        NETWT = Val(row.Cells(GNETWT.Index).Value)
                        COLOR = row.Cells(gcolor.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                    Else
                        GRIDSRNO = GRIDSRNO & "|" & row.Cells(gsrno.Index).Value
                        CARTOONNO = CARTOONNO & "|" & row.Cells(GCARTOONNO.Index).Value.ToString
                        YARNQUALITY = YARNQUALITY & "|" & row.Cells(GYARNQUALITY.Index).Value.ToString
                        CONES = CONES & "|" & row.Cells(GCONES.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        NETWT = NETWT & "|" & row.Cells(GNETWT.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(CARTOONNO)
            alParaval.Add(YARNQUALITY)
            alParaval.Add(CONES)
            alParaval.Add(LOTNO)
            alParaval.Add(NETWT)
            alParaval.Add(COLOR)
            alParaval.Add(MILLNAME)

            Dim OBJINWARD As New ClsWeftIssue
            OBJINWARD.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJINWARD.SAVE()
                TEMPISSUENO = DTTABLE.Rows(0).Item(0)
                MessageBox.Show("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPISSUENO)
                IntResult = OBJINWARD.UPDATE()
                MessageBox.Show("Details Updated")
                EDIT = False
            End If

            PRINTREPORT(TEMPISSUENO)
            clear()
            INWARDDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try


    End Sub

    Private Sub WeftIssue_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'JOB OUT'")
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

                Dim OBJINWARD As New ClsWeftIssue
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPISSUENO)
                ALPARAVAL.Add(YearId)
                OBJINWARD.alParaval = ALPARAVAL
                Dim dttable As DataTable = OBJINWARD.SELECTISSUE()

                If dttable.Rows.Count > 0 Then
                    For Each dr As DataRow In dttable.Rows

                        TXTINWARDNO.Text = TEMPISSUENO
                        INWARDDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")

                        CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                        CMBTRANS.Text = dr("TRANSNAME").ToString
                        TXTPARTYCHALLANNO.Text = dr("PARTYCHALLANNO").ToString
                        TXTAPPROXVALUE.Text = Val(dr("APPROXVALUE"))
                        lbltotalCARTOON.Text = Val(dr("TOTALCARTOON"))
                        LBLTOTALCONES.Text = Val(dr("TOTALCONES"))
                        LBLTOTALWT.Text = Val(dr("TOTALWT"))

                        gridgrn.Rows.Add(dr("GRIDSRNO").ToString, dr("CARTOONNO"), dr("YARNQUALITY"), Val(dr("CONES")), dr("LOTNO").ToString, Val(dr("NETWT")), dr("COLOR"), dr("MILLNAME"))
                    Next
                    total()
                    gridgrn.FirstDisplayedScrollingRowIndex = gridgrn.RowCount - 1
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

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                gridgrn.RowCount = 0
                TEMPISSUENO = Val(tstxtbillno.Text)
                If TEMPISSUENO > 0 Then
                    EDIT = True
                    WeftIssue_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
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

            Dim objpodtls As New WeftIssueDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Public Function CloneWithValues(ByVal row As DataGridViewRow) As DataGridViewRow
        CloneWithValues = CType(row.Clone(), DataGridViewRow)
        For index As Int32 = 0 To row.Cells.Count - 1
            CloneWithValues.Cells(index).Value = row.Cells(index).Value
        Next
    End Function

    Private Sub gridgrn_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridgrn.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub gridgrn_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles gridgrn.CellValidating
        Try
            Dim colNum As Integer = gridgrn.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GCONES.Index, GNETWT.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If gridgrn.CurrentCell.Value = Nothing Then gridgrn.CurrentCell.Value = "0.00"
                        gridgrn.CurrentCell.Value = Convert.ToDecimal(gridgrn.Item(colNum, e.RowIndex).Value)
                        total()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If
                Case GCARTOONNO.Index
                    CARTOONCOUNT()

            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridgrn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridgrn.KeyDown
        Try
            If e.KeyCode = Keys.Delete And gridgrn.RowCount > 0 Then gridgrn.Rows.RemoveAt(gridgrn.CurrentRow.Index) ''Removing Row From Grid
            'If GRIDGDN.CurrentRow.Index > 0 Then If e.KeyCode = Keys.F12 Then If GRIDGDN.Rows(GRIDGDN.CurrentRow.Index - 1).Cells(GLRNO.Index).Value <> "" Then GRIDGDN.Rows(GRIDGDN.CurrentRow.Index).Cells(GLRNO.Index).Value = GRIDGDN.Rows(GRIDGDN.CurrentRow.Index - 1).Cells(GLRNO.Index).Value
            If e.KeyCode = Keys.F12 And gridgrn.RowCount > 0 Then
                'THIS IS DONE FOR DAKSH, COZ WHEN WE FETCH DATA FROM PACKING WE DONT HAVE BARCODE AND WE NEED TO RUN THIS CODE
                'If GRIDGDN.CurrentRow.Cells(GBARCODE.Index).Value <> "" Then GRIDGDN.Rows.Insert(GRIDGDN.CurrentRow.Index, CloneWithValues(GRIDGDN.CurrentRow))
                gridgrn.Rows.Insert(gridgrn.CurrentRow.Index, CloneWithValues(gridgrn.CurrentRow))
                gridgrn.Rows(gridgrn.RowCount - 1).Selected = True
            End If

            getsrno(gridgrn)
            total()
            If e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If gridgrn.CurrentRow.Index >= 0 And gridgrn.Item(gsrno.Index, gridgrn.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                txtsrno.Text = gridgrn.Item(gsrno.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTCARTOONNO.Text = gridgrn.Item(GCARTOONNO.Index, gridgrn.CurrentRow.Index).Value.ToString
                CMBYARNQUALITY.Text = gridgrn.Item(GYARNQUALITY.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTCONES.Text = gridgrn.Item(GCONES.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTLOTNO.Text = gridgrn.Item(GLOTNO.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTWT.Text = gridgrn.Item(GNETWT.Index, gridgrn.CurrentRow.Index).Value.ToString
                cmbcolor.Text = gridgrn.Item(gcolor.Index, gridgrn.CurrentRow.Index).Value.ToString
                CMBMILLNAME.Text = gridgrn.Item(GMILLNAME.Index, gridgrn.CurrentRow.Index).Value.ToString

                TEMPROW = gridgrn.CurrentRow.Index
                txtsrno.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try

            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                TEMPMSG = MsgBox("Delete Issue?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTINWARDNO.Text.Trim)
                    'alParaval.Add(CmpId)
                    alParaval.Add(YearId)

                    Dim Clsgrn As New ClsWeftIssue
                    Clsgrn.alParaval = alParaval
                    IntResult = Clsgrn.Delete()
                    MsgBox("Issue Deleted")
                    clear()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTCONES_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCONES.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Sub FILLGRID()

        gridgrn.Enabled = True
        If GRIDDOUBLECLICK = False Then
            gridgrn.Rows.Add(Val(txtsrno.Text.Trim), TXTCARTOONNO.Text.Trim, CMBYARNQUALITY.Text.Trim, Val(TXTCONES.Text.Trim), TXTLOTNO.Text.Trim, Val(TXTWT.Text.Trim), cmbcolor.Text.Trim, CMBMILLNAME.Text.Trim)
            getsrno(gridgrn)
        ElseIf GRIDDOUBLECLICK = True Then
            gridgrn.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            gridgrn.Item(GCARTOONNO.Index, TEMPROW).Value = TXTCARTOONNO.Text.Trim
            gridgrn.Item(GYARNQUALITY.Index, TEMPROW).Value = CMBYARNQUALITY.Text.Trim
            gridgrn.Item(GCONES.Index, TEMPROW).Value = Format(Val(TXTCONES.Text.Trim))
            gridgrn.Item(GLOTNO.Index, TEMPROW).Value = TXTLOTNO.Text.Trim
            gridgrn.Item(GNETWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.00")
            gridgrn.Item(gcolor.Index, TEMPROW).Value = cmbcolor.Text.Trim
            gridgrn.Item(GMILLNAME.Index, TEMPROW).Value = CMBMILLNAME.Text.Trim

            GRIDDOUBLECLICK = False

        End If
        total()

        gridgrn.FirstDisplayedScrollingRowIndex = gridgrn.RowCount - 1


        txtsrno.Clear()
        TXTCARTOONNO.Clear()
        TXTCONES.Clear()
        TXTLOTNO.Clear()
        TXTWT.Clear()
        cmbcolor.Text = ""
        CMBYARNQUALITY.Text = ""
        CMBMILLNAME.Text = ""

        TXTCARTOONNO.Focus()

        txtsrno.Text = Val(gridgrn.Rows(gridgrn.RowCount - 1).Cells(0).Value) + 1
        'If gridgrn.RowCount > 0 Then
        '    txtsrno.Text = Val(gridgrn.Rows(gridgrn.RowCount - 1).Cells(0).Value) + 1
        'Else
        '    txtsrno.Text = 1
        'End If

    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress, TXTAPPROXVALUE.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPISSUENO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(ByVal WEFTISSNO As Integer)
        Try
            TEMPMSG = MsgBox("Wish to Print Weft Issue?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                Dim OBJPUR As New WeftIssueDesign
                OBJPUR.MdiParent = MDIMain
                OBJPUR.WHERECLAUSE = "{WEFTISSUEMASTER.WEFTISS_NO}=" & Val(WEFTISSNO) & " and {WEFTISSUEMASTER.WEFTISS_YEARID}=" & YearId
                OBJPUR.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_Validated(sender As Object, e As EventArgs) Handles CMBMILLNAME.Validated
        If CMBYARNQUALITY.Text.Trim <> "" And Val(TXTWT.Text.Trim) <> 0 Then
            FILLGRID()
        Else
            MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
            Exit Sub
        End If
    End Sub
End Class