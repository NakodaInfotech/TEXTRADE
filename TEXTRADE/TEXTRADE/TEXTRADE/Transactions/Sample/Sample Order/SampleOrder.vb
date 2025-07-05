


Imports System.ComponentModel
    Imports BL

Public Class SampleOrder

    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPSONO As String
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdEXIT_Click(sender As Object, e As EventArgs) Handles cmdEXIT.Click
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If
            Dim OBJSO As New ClsSampleOrder()


            OBJSO.ALPARAVAL.Add(Format(Convert.ToDateTime(ORDERDATE.Text).Date, "MM/dd/yyyy"))
            OBJSO.ALPARAVAL.Add(CMBPARTYNAME.Text.Trim)
            OBJSO.ALPARAVAL.Add(TXTMODEOFSHIPMENT.Text.Trim)
            OBJSO.ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            OBJSO.ALPARAVAL.Add(CHKSELF.CheckState)
            OBJSO.ALPARAVAL.Add(CmpId)
            OBJSO.ALPARAVAL.Add(Userid)
            OBJSO.ALPARAVAL.Add(YearId)


            Dim GRIDSRNO As String = ""
            Dim SAMPLETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim NARRATION As String = ""
            Dim NOOFBOOKLET As String = ""
            Dim OUTQTY As String = ""
            Dim CLOSED As String = ""

            For Each ROW As DataGridViewRow In GRIDSO.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(ROW.Cells(gsrno.Index).Value)
                        SAMPLETYPE = ROW.Cells(GSAMPLETYPE.Index).Value
                        ITEMNAME = ROW.Cells(gitemname.Index).Value
                        QUALITY = ROW.Cells(GQUALITY.Index).Value
                        DESIGN = ROW.Cells(GDESIGN.Index).Value
                        COLOR = ROW.Cells(GCOLOR.Index).Value
                        NARRATION = ROW.Cells(GNARRATION.Index).Value
                        NOOFBOOKLET = Val(ROW.Cells(GNOOFBOOKLET.Index).Value)
                        OUTQTY = Val(ROW.Cells(GOUTQTY.Index).Value)
                        If Convert.ToBoolean(ROW.Cells(GCLOSED.Index).Value) = True Then CLOSED = 1 Else CLOSED = 0
                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(ROW.Cells(gsrno.Index).Value)
                        SAMPLETYPE = SAMPLETYPE & "|" & ROW.Cells(GSAMPLETYPE.Index).Value
                        ITEMNAME = ITEMNAME & "|" & ROW.Cells(gitemname.Index).Value
                        QUALITY = QUALITY & "|" & ROW.Cells(GQUALITY.Index).Value
                        DESIGN = DESIGN & "|" & ROW.Cells(GDESIGN.Index).Value
                        COLOR = COLOR & "|" & ROW.Cells(GCOLOR.Index).Value
                        NARRATION = NARRATION & "|" & ROW.Cells(GNARRATION.Index).Value
                        NOOFBOOKLET = NOOFBOOKLET & "|" & Val(ROW.Cells(GNOOFBOOKLET.Index).Value)
                        OUTQTY = OUTQTY & "|" & Val(ROW.Cells(GOUTQTY.Index).Value)
                        If Convert.ToBoolean(ROW.Cells(GCLOSED.Index).Value) = True Then CLOSED = CLOSED & "|" & 1 Else CLOSED = CLOSED & "|" & 0
                    End If
                End If
            Next

            OBJSO.ALPARAVAL.Add(GRIDSRNO)
            OBJSO.ALPARAVAL.Add(SAMPLETYPE)
            OBJSO.ALPARAVAL.Add(ITEMNAME)
            OBJSO.ALPARAVAL.Add(QUALITY)
            OBJSO.ALPARAVAL.Add(DESIGN)
            OBJSO.ALPARAVAL.Add(COLOR)
            OBJSO.ALPARAVAL.Add(NARRATION)
            OBJSO.ALPARAVAL.Add(NOOFBOOKLET)
            OBJSO.ALPARAVAL.Add(OUTQTY)
            OBJSO.ALPARAVAL.Add(CLOSED)


            OBJSO.ALPARAVAL.Add(Val(TOTALNOOFBOOKLET.Text.Trim))
            OBJSO.ALPARAVAL.Add(CMBAGENT.Text.Trim)


            If EDIT = False Then

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = OBJSO.SAVE()
                MessageBox.Show("Details Added")
                TXTORDERNO.Text = DT.Rows(0).Item(0)
            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                OBJSO.ALPARAVAL.Add(TEMPSONO)
                Dim IntResult As Integer = OBJSO.UPDATE()
                MessageBox.Show("Details Updated")
            End If
            PRINTREPORT()
            EDIT = False
            clear()
            CMBPARTYNAME.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True
        If CMBPARTYNAME.Text.Trim = "" Then
            EP.SetError(CMBPARTYNAME, " Please Fill Party Name ")
            bln = False
        End If

        If GRIDSO.RowCount = 0 Then
            EP.SetError(TXTNARRATION, " Please Enter Data in grid")
            bln = False
        End If

        If ORDERDATE.Text = "__/__/____" Then
            EP.SetError(ORDERDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(ORDERDATE.Text) Then
                EP.SetError(ORDERDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If PBlock.Visible = True Then
            EP.SetError(PBlock, " Order Locked")
            bln = False
        End If

        Return bln
    End Function

    Sub FILLGRID()

        If GRIDDOUBLECLICK = False Then
            GRIDSO.Rows.Add(Val(TXTSRNO.Text.Trim), CMBSAMPLETYPE.Text.Trim, CMBITEMNAME.Text.Trim, CMBQUALITY.Text.Trim, CMBDESIGN.Text.Trim, CMBCOLOR.Text.Trim, Val(TXTNOOFBOOKLET.Text.Trim), TXTNARRATION.Text.Trim, 0, 0)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDSO.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDSO.Item(GSAMPLETYPE.Index, TEMPROW).Value = CMBSAMPLETYPE.Text.Trim
            GRIDSO.Item(gitemname.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
            GRIDSO.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDSO.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDSO.Item(GCOLOR.Index, TEMPROW).Value = CMBCOLOR.Text.Trim
            GRIDSO.Item(GNOOFBOOKLET.Index, TEMPROW).Value = Val(TXTNOOFBOOKLET.Text.Trim)
            GRIDSO.Item(GNARRATION.Index, TEMPROW).Value = TXTNARRATION.Text.Trim
            GRIDDOUBLECLICK = False
        End If

        GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1

        TXTSRNO.Text = GRIDSO.RowCount + 1
        CMBSAMPLETYPE.Text = ""
        CMBITEMNAME.Text = ""
        CMBQUALITY.Text = ""
        CMBDESIGN.Text = ""
        CMBCOLOR.Text = ""
        TXTBARCODE.Clear()
        TXTNOOFBOOKLET.Clear()

        TXTNARRATION.Clear()

        TXTBARCODE.Focus()
        TOTAL()

    End Sub

    Sub TOTAL()
        Try
            TOTALNOOFBOOKLET.Text = 0

            For Each ROW As DataGridViewRow In GRIDSO.Rows
                TOTALNOOFBOOKLET.Text = Format(Val(TOTALNOOFBOOKLET.Text.Trim) + Val(ROW.Cells(GNOOFBOOKLET.Index).Value), "0")
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            CMBPARTYNAME.Text = ""
            CMBPARTYNAME.Enabled = True
            ORDERDATE.Text = Now.Date
            TXTMODEOFSHIPMENT.Clear()
            TXTBARCODE.Clear()
            TXTREMARKS.Clear()
            CMBSAMPLETYPE.Text = ""
            CMBITEMNAME.Text = ""
            CMBQUALITY.Text = ""
            CMBDESIGN.Text = ""
            CMBCOLOR.Text = ""
            TXTNOOFBOOKLET.Clear()
            TXTNARRATION.Clear()
            GRIDSO.RowCount = 0
            txtbillno.Clear()
            TXTCOPY.Clear()
            EP.Clear()
            GETMAX_ORDERNO()
            TOTALNOOFBOOKLET.Text = 0.0
            TXTSRNO.Text = 1
            CMBAGENT.Text = ""
            CHKSELF.CheckState = CheckState.Unchecked
            LBLCLOSED.Visible = False
            lbllocked.Visible = False
            PBlock.Visible = False
            GRIDDOUBLECLICK = False
            LBLWHATSAPP.Visible = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_ORDERNO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(SO_no),0) + 1 ", "SAMPLEORDER", " AND SO_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTORDERNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub SampleOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SAMPLE MODULE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            fillcmb()
            clear()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objclsSMP As New ClsSampleOrder()
                Dim dt_po As DataTable = objclsSMP.selectSMP(TEMPSONO, CmpId, Locationid, YearId)

                If dt_po.Rows.Count > 0 Then
                    For Each dr As DataRow In dt_po.Rows
                        TXTORDERNO.Text = TEMPSONO
                        ORDERDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")
                        CMBPARTYNAME.Text = Convert.ToString(dr("NAME"))
                        TXTMODEOFSHIPMENT.Text = Convert.ToString(dr("MODE"))
                        TXTREMARKS.Text = Convert.ToString(dr("REMARKS"))
                        TOTALNOOFBOOKLET.Text = dr("TOTALNOOFBOOKLET")
                        CMBAGENT.Text = dr("AGENT")


                        GRIDSO.Rows.Add(Val(dr("GRIDSRNO")), dr("SAMPLETYPE"), dr("ITEMNAME"), dr("QUALITYNAME"), dr("DESIGN").ToString, dr("COLOR").ToString, Val(dr("NOOFBOOKLET")), dr("NARRATION").ToString, Val(dr("OUTQTY")), dr("CLOSED"))

                        If Val(dr("OUTQTY")) > 0 Then
                            GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
                            lbllocked.Visible = True
                            PBlock.Visible = True
                            CMBPARTYNAME.Enabled = False
                        End If

                        If Convert.ToBoolean(dr("CLOSED")) = True Then
                            GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            LBLCLOSED.Visible = True
                            PBlock.Visible = True
                        End If

                        CHKSELF.Checked = Convert.ToBoolean(dr("SELF"))
                        If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True
                    Next
                    GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1
                    TOTAL()
                Else
                    EDIT = False
                    clear()
                End If
            End If

            TXTSRNO.Text = Val(GRIDSO.RowCount) + 1

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillcmb()
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillname(CMBPARTYNAME, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' and LEDGERS.ACC_TYPE = 'AGENT'))")
            fillQUALITY(CMBQUALITY, EDIT)
            FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
            FILLCOLOR(CMBCOLOR, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
            FILLSAMPLETYPE(CMBSAMPLETYPE)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False
        CMBPARTYNAME.Focus()
    End Sub

    Private Sub SampleOrder_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdOK_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                txtbillno.Focus()
                txtbillno.SelectAll()
            ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'for grid foucs
                GRIDSO.Focus()
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                ToolPREVIOUS_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                Toolnext_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete Sample barcode ?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPSONO)
                    alParaval.Add(YearId)

                    Dim clspo As New ClsSampleOrder()
                    clspo.ALPARAVAL = alParaval
                    Dim IntResult As Integer = clspo.Delete()
                    MsgBox("sample barcode Deleted")
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

    Sub EDITROW()
        Try
            If GRIDSO.CurrentRow.Index >= 0 And GRIDSO.Item(gsrno.Index, GRIDSO.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TEMPROW = GRIDSO.CurrentRow.Index
                TXTSRNO.Text = GRIDSO.Item(gsrno.Index, GRIDSO.CurrentRow.Index).Value.ToString
                CMBSAMPLETYPE.Text = GRIDSO.Item(GSAMPLETYPE.Index, GRIDSO.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Text = GRIDSO.Item(gitemname.Index, GRIDSO.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDSO.Item(GQUALITY.Index, GRIDSO.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDSO.Item(GDESIGN.Index, GRIDSO.CurrentRow.Index).Value.ToString
                CMBCOLOR.Text = GRIDSO.Item(GCOLOR.Index, GRIDSO.CurrentRow.Index).Value.ToString
                TXTNOOFBOOKLET.Text = GRIDSO.Item(GNOOFBOOKLET.Index, GRIDSO.CurrentRow.Index).Value
                TXTNARRATION.Text = GRIDSO.Item(GNARRATION.Index, GRIDSO.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSPL_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDSO.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSO.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDSO.Rows.RemoveAt(GRIDSO.CurrentRow.Index)
                getsrno(GRIDSO)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
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


            Dim objpodtls As New SampleOrderDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdOK_Click(sender, e)
    End Sub

    Private Sub ToolPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolPREVIOUS.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            GRIDSO.RowCount = 0
LINE1:
            TEMPSONO = Val(TXTORDERNO.Text) - 1
            If TEMPSONO > 0 Then
                EDIT = True
                SampleOrder_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDSO.RowCount = 0 And TEMPSONO > 1 Then
                TXTORDERNO.Text = TEMPSONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            GRIDSO.RowCount = 0
LINE1:
            TEMPSONO = Val(TXTORDERNO.Text) + 1
            GETMAX_ORDERNO()
            Dim MAXNO As Integer = TXTORDERNO.Text.Trim
            clear()
            If Val(TXTORDERNO.Text) - 1 >= TEMPSONO Then
                EDIT = True
                SampleOrder_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDSO.RowCount = 0 And TEMPSONO < MAXNO Then
                TXTORDERNO.Text = TEMPSONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If MsgBox("Wish to Print Sample Order?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub
            Dim OBJSMP As New SampleOrderDesign
            OBJSMP.MdiParent = MDIMain
            OBJSMP.FRMSTRING = "SAMPLEORDER"
            OBJSMP.FORMULA = "{SAMPLEORDER.SO_no}=" & Val(TXTORDERNO.Text.Trim) & " and {SAMPLEORDER.SO_yearid}=" & YearId
            OBJSMP.TEMPNO = Val(TXTORDERNO.Text.Trim)
            OBJSMP.PARTYNAME = CMBPARTYNAME.Text.Trim
            OBJSMP.AGENTNAME = CMBAGENT.Text.Trim
            OBJSMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then itemvalidate(CMBITEMNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDESIGN.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJD As New SelectDesign
                OBJD.ShowDialog()
                If OBJD.TEMPNAME <> "" Then CMBDESIGN.Text = OBJD.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCOLOR.Enter
        Try
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCOLOR.KeyDown
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

    Private Sub CMBCOLOR_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCOLOR.Validating
        Try
            If CMBCOLOR.Text.Trim <> "" Then COLORVALIDATE(CMBCOLOR, e, Me, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ORDERDATE_Validating(sender As Object, e As CancelEventArgs) Handles ORDERDATE.Validating
        Try
            If ORDERDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(ORDERDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    If Not datecheck(ORDERDATE.Text) Then
                        MsgBox("Date not in Accounting Year")
                        e.Cancel = True

                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then namevalidate(CMBPARTYNAME, CMBCODE, e, Me, TXTADD, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' and LEDGERS.ACC_TYPE = 'AGENT')) ", "SUNDRY DEBTORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSPL_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSO.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNARRATION_Validating(sender As Object, e As CancelEventArgs) Handles TXTNARRATION.Validating
        Try
            If CMBSAMPLETYPE.Text.Trim <> "" And CMBITEMNAME.Text.Trim <> "" And Val(TXTNOOFBOOKLET.Text.Trim) > 0 Then
                fillgrid()
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 And CMBSAMPLETYPE.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                'no need for yearid clause here as we need to fetch this barcode in all acccouting year
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITYNAME ", "", "  SAMPLEBARCODE INNER JOIN ITEMMASTER ON SAMPLEBARCODE.SB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN QUALITYMASTER ON SAMPLEBARCODE.SB_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON SAMPLEBARCODE.SB_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON SAMPLEBARCODE.SB_DESIGNID = DESIGNMASTER.DESIGN_id ", " AND SB_BARCODE = '" & TXTBARCODE.Text.Trim & "'")
                If DT.Rows.Count > 0 Then
                    GRIDSO.Rows.Add(GRIDSO.RowCount + 1, CMBSAMPLETYPE.Text.Trim, DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITYNAME"), DT.Rows(0).Item("DESIGN"), DT.Rows(0).Item("SHADE"), 1, "")
                    GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1

                    TXTBARCODE.Clear()
                    TXTBARCODE.Focus()
                    TOTAL()
                Else
                    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                    TXTBARCODE.Clear()
                End If
            Else
                If CMBSAMPLETYPE.Text.Trim = "" And TXTBARCODE.Text.Trim <> "" Then MsgBox("Enter Sample Type", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(sender As Object, e As EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtbillno_Validating(sender As Object, e As CancelEventArgs) Handles txtbillno.Validating
        Try
            If Val(txtbillno.Text.Trim) > 0 Then
                GRIDSO.RowCount = 0
                TEMPSONO = Val(txtbillno.Text)
                If TEMPSONO > 0 Then
                    EDIT = True
                    SampleOrder_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNOOFBOOKLET_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTNOOFBOOKLET.KeyPress, txtbillno.KeyPress, TXTCOPY.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub CMBPARTYNAME_Validated(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Validated
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENT ", "", " LEDGERS LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.Acc_id  ", " and LEDGERS.acc_cmpname = '" & CMBPARTYNAME.Text.Trim & "' and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    If CMBAGENT.Text.Trim = "" Then CMBAGENT.Text = DT.Rows(0).Item("AGENT")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(sender As Object, e As EventArgs) Handles CMBAGENT.Enter
        If CMBAGENT.Text.Trim = "" Then fillledger(CMBAGENT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
    End Sub

    Private Sub CMBAGENT_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBAGENT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBAGENT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Validating(sender As Object, e As CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then namevalidate(CMBAGENT, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Enter(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillname(CMBPARTYNAME, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' and LEDGERS.ACC_TYPE = 'AGENT'))")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validated(sender As Object, e As EventArgs) Handles CMBITEMNAME.Validated
        Try
            If CMBITEMNAME.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DTRATE As New DataTable
                Dim DT As New DataTable
                If ClientName = "MAHAVIRPOLYCOT" Then
                    DTRATE = OBJCMN.SEARCH("ISNULL(ITEMMASTER.ITEM_RATE, 0) AS RATE ", "", "  ITEMMASTER", "AND ITEMMASTER.item_name = '" & CMBITEMNAME.Text.Trim & "' AND ITEMMASTER.item_yearid = " & YearId)
                ElseIf ClientName = "YASHVI" Then
                    DT = OBJCMN.SEARCH(" ISNULL(LEDGERS.ACC_PRICELISTCOLUMN, '') AS COLNAME", "", " LEDGERS ", " AND ledgers.acc_cmpname = '" & CMBPARTYNAME.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                    If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("COLNAME") <> "" Then DTRATE = OBJCMN.SEARCH(DT.Rows(0).Item("COLNAME") & " AS RATE", "", "ITEMPRICELIST INNER JOIN ITEMMASTER ON ITEMPRICELIST.ITEMID = ITEMMASTER.item_id ", " AND ITEMMASTER.ITEM_NAME = '" & CMBITEMNAME.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                End If

                'OPEN THIS BOX IF SHADES ARE PRESENT FOR SELECTED DESIGN
                Dim DTITEM As New DataTable
                DT = OBJCMN.SEARCH(" DESIGNMASTER.DESIGN_NO", "", " DESIGNMASTER INNER JOIN ITEMMASTER ON DESIGNMASTER.DESIGN_ITEMID = ITEMMASTER.ITEM_ID ", " AND ITEMMASTER.ITEM_NAME = '" & CMBITEMNAME.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                If FETCHITEMWISEDESIGN = True And CMBSAMPLETYPE.Text.Trim <> "" And DT.Rows.Count > 0 And ClientName = "YASHVI" Then
                    Dim OBJ As New SelectItemSampleOrder
                    OBJ.ITEMNAME = CMBITEMNAME.Text.Trim
                    OBJ.ShowDialog()
                    DTITEM = OBJ.DT
                End If
                If DTITEM.Rows.Count > 0 Then
                    For Each DTROWPS As DataRow In DTITEM.Rows
                        GRIDSO.Rows.Add(0, CMBSAMPLETYPE.Text.Trim, CMBITEMNAME.Text.Trim, "", DTROWPS("DESIGNNO"), "", Format(Val(DTROWPS("ORDERBOOKLET")), "0"), "", 0, 0)
                    Next
                    GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1
                    GETSRNO(GRIDSO)

                    TOTAL()
                    CMBITEMNAME.Text = ""
                    CMBDESIGN.Text = ""
                    CMBITEMNAME.Focus()
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ORDERDATE_GotFocus(sender As Object, e As EventArgs) Handles ORDERDATE.GotFocus
        ORDERDATE.SelectionStart = 0
    End Sub

    Private Sub CMDSELECTSTOCK_Click(sender As Object, e As EventArgs) Handles CMDSELECTSTOCK.Click
        Try

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If CMBSAMPLETYPE.Text.Trim = "" Then
                MsgBox("Please Select Sample Type First", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim OBJSELECTSG As New SelectStockGrid
            OBJSELECTSG.ShowDialog()
            Dim DTTABLE As DataTable = OBJSELECTSG.DT1

            For Each dr As DataRow In DTTABLE.Rows
                GRIDSO.Rows.Add(0, CMBSAMPLETYPE.Text.Trim, dr("ITEMNAME"), "", dr("DESIGNNO"), "", 1, "", 0, 0)
            Next

            GETSRNO(GRIDSO)
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If EDIT = False Then Exit Sub
            SENDWHATSAPP(TEMPSONO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(SONO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJSO As New SampleOrderDesign
            OBJSO.MdiParent = MDIMain
            OBJSO.DIRECTPRINT = True
            OBJSO.FRMSTRING = "SAMPLEORDER"
            OBJSO.DIRECTMAIL = True
            OBJSO.PARTYNAME = CMBPARTYNAME.Text.Trim
            OBJSO.AGENTNAME = CMBAGENT.Text.Trim
            OBJSO.FORMULA = "{sampleOrder.so_no}=" & Val(SONO) & " and {sampleOrder.SO_yearid}=" & YearId
            OBJSO.TEMPNO = SONO
            OBJSO.NOOFCOPIES = 1
            OBJSO.Show()
            OBJSO.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBPARTYNAME.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBAGENT.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\SAMPLEORDER_" & Val(SONO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add("SAMPLEORDER_" & Val(SONO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE SAMPLEORDER SET SO_SENDWHATSAPP = 1 WHERE SO_NO = " & SONO & " AND SO_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSAMPLETYPE_Enter(sender As Object, e As EventArgs) Handles CMBSAMPLETYPE.Enter
        Try
            If CMBSAMPLETYPE.Text.Trim = "" Then FILLSAMPLETYPE(CMBSAMPLETYPE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSAMPLETYPE_Validating(sender As Object, e As CancelEventArgs) Handles CMBSAMPLETYPE.Validating
        Try
            If CMBSAMPLETYPE.Text.Trim <> "" Then SAMPLETYPEVALIDATE(CMBSAMPLETYPE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCOPY_Validated(sender As Object, e As EventArgs) Handles TXTCOPY.Validated
        Try
            If Val(TXTCOPY.Text.Trim) > 0 And EDIT = False Then
                If MsgBox("Copy Sample Order No " & Val(TXTCOPY.Text.Trim) & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim OBJSAMPLE As New ClsSampleOrder
                Dim DT As DataTable = OBJSAMPLE.selectSMP(Val(TXTCOPY.Text.Trim), CmpId, 0, YearId)
                If DT.Rows.Count > 0 Then
                    For Each dr As DataRow In DT.Rows
                        ORDERDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")
                        CMBPARTYNAME.Text = Convert.ToString(dr("NAME"))
                        TXTMODEOFSHIPMENT.Text = Convert.ToString(dr("MODE"))
                        TXTREMARKS.Text = Convert.ToString(dr("REMARKS"))
                        TOTALNOOFBOOKLET.Text = dr("TOTALNOOFBOOKLET")
                        CMBAGENT.Text = dr("AGENT")

                        GRIDSO.Rows.Add(Val(dr("GRIDSRNO")), dr("SAMPLETYPE"), dr("ITEMNAME"), dr("QUALITYNAME"), dr("DESIGN").ToString, dr("COLOR").ToString, Val(dr("NOOFBOOKLET")), dr("NARRATION").ToString, 0, 0)
                    Next
                    GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1
                    TOTAL()
                Else
                    MsgBox("Invalid Sample Order No", MsgBoxStyle.Critical)
                    TXTCOPY.Clear()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTSONO_Validated(sender As Object, e As EventArgs) Handles TXTSONO.Validated
        Try
            If CMBSAMPLETYPE.Text.Trim = "" Then
                MsgBox("Select Sample Type First", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Val(TXTSONO.Text.Trim) > 0 And EDIT = False Then
                If MsgBox("Fetch Data From SO No " & Val(TXTSONO.Text.Trim) & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR", "", " SALEORDER INNER JOIN SALEORDER_DESC ON SALEORDER.so_no = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID LEFT OUTER JOIN DESIGNMASTER ON DESIGNMASTER.DESIGN_id = SALEORDER_DESC.SO_DESIGNID LEFT OUTER JOIN ITEMMASTER AS ITEMMASTER ON SALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER AS COLORMASTER ON SALEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_id ", " AND SALEORDER.SO_NO = " & Val(TXTSONO.Text.Trim) & " AND SALEORDER.SO_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows
                        GRIDSO.Rows.Add(0, CMBSAMPLETYPE.Text.Trim, DR("ITEM"), "", DR("DESIGN"), DR("COLOR"), 1, "", 0, 0)
                    Next
                    GETSRNO(GRIDSO)
                    TOTAL()
                Else
                    MsgBox("Invalid SO No", MsgBoxStyle.Critical)
                    TXTSONO.Clear()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class


