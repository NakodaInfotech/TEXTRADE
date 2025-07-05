
Imports System.ComponentModel
Imports BL

Public Class SampleBookletInward

    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPSPLNO As String
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdEXIT_Click(sender As Object, e As EventArgs) Handles cmdEXIT.Click
        Me.Close()
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If
            Dim OBJSPL As New ClsSampleBookletInward()

            OBJSPL.ALPARAVAL.Add(Format(Convert.ToDateTime(SPLDATE.Text).Date, "MM/dd/yyyy"))
            OBJSPL.ALPARAVAL.Add(CMBPARTYNAME.Text.Trim)
            OBJSPL.ALPARAVAL.Add(TXTMODEOFSHIPMENT.Text.Trim)
            OBJSPL.ALPARAVAL.Add(txtremarks.Text.Trim)

            OBJSPL.ALPARAVAL.Add(CmpId)
            OBJSPL.ALPARAVAL.Add(Userid)
            OBJSPL.ALPARAVAL.Add(YearId)

            Dim GRIDSRNO As String = ""
            Dim GRIDSAMPLETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            'Dim RATE As String = ""
            'Dim MTRS As String = ""
            'Dim AMOUNT As String = ""
            Dim NARRATION As String = ""
            Dim NOOFBOOKLET As String = ""

            For Each ROW As DataGridViewRow In GRIDSPL.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(ROW.Cells(gsrno.Index).Value)
                        GRIDSAMPLETYPE = ROW.Cells(GSAMPLETYPE.Index).Value
                        ITEMNAME = ROW.Cells(gitemname.Index).Value
                        QUALITY = ROW.Cells(GQUALITY.Index).Value
                        DESIGN = ROW.Cells(GDESIGN.Index).Value
                        COLOR = ROW.Cells(GCOLOR.Index).Value
                        NARRATION = ROW.Cells(GNARRATION.Index).Value
                        NOOFBOOKLET = Val(ROW.Cells(GNOOFBOOKLET.Index).Value)
                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(ROW.Cells(gsrno.Index).Value)
                        GRIDSAMPLETYPE = GRIDSAMPLETYPE & "|" & ROW.Cells(GSAMPLETYPE.Index).Value
                        ITEMNAME = ITEMNAME & "|" & ROW.Cells(gitemname.Index).Value
                        QUALITY = QUALITY & "|" & ROW.Cells(GQUALITY.Index).Value
                        DESIGN = DESIGN & "|" & ROW.Cells(GDESIGN.Index).Value
                        COLOR = COLOR & "|" & ROW.Cells(GCOLOR.Index).Value
                        NARRATION = NARRATION & "|" & ROW.Cells(GNARRATION.Index).Value
                        NOOFBOOKLET = NOOFBOOKLET & "|" & Val(ROW.Cells(GNOOFBOOKLET.Index).Value)

                    End If
                End If
            Next

            OBJSPL.ALPARAVAL.Add(GRIDSRNO)
            OBJSPL.ALPARAVAL.Add(GRIDSAMPLETYPE)

            OBJSPL.ALPARAVAL.Add(ITEMNAME)
            OBJSPL.ALPARAVAL.Add(QUALITY)
            OBJSPL.ALPARAVAL.Add(DESIGN)
            OBJSPL.ALPARAVAL.Add(COLOR)
            'OBJSPL.ALPARAVAL.Add(RATE)
            'OBJSPL.ALPARAVAL.Add(MTRS)
            'OBJSPL.ALPARAVAL.Add(AMOUNT)
            OBJSPL.ALPARAVAL.Add(NARRATION)
            OBJSPL.ALPARAVAL.Add(NOOFBOOKLET)
            OBJSPL.ALPARAVAL.Add(Val(TOTALNOOFBOOKLET.Text.Trim))
            OBJSPL.ALPARAVAL.Add(CMBGODOWN.Text.Trim)
            If CHKDONT.Checked = True Then
                OBJSPL.ALPARAVAL.Add(1)
            Else
                OBJSPL.ALPARAVAL.Add(0)
            End If


            If EDIT = False Then

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = OBJSPL.SAVE()
                MessageBox.Show("Details Added")
                TXTSPLNO.Text = DT.Rows(0).Item(0)
            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                OBJSPL.ALPARAVAL.Add(TEMPSPLNO)
                Dim IntResult As Integer = OBJSPL.UPDATE()
                MessageBox.Show("Details Updated")
            End If

            PRINTREPORT()

            EDIT = False
            CLEAR()
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
        If CMBGODOWN.Text.Trim = "" Then
            EP.SetError(CMBGODOWN, " Please Fill Godown ")
            bln = False
        End If
        If GRIDSPL.RowCount = 0 Then
            EP.SetError(TXTNARRATION, " Please Enter Data in grid")
            bln = False
        End If

        If SPLDATE.Text = "__/__/____" Then
            EP.SetError(SPLDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(SPLDATE.Text) Then
                EP.SetError(SPLDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        Return bln
    End Function

    Sub FILLGRID()

        If GRIDDOUBLECLICK = False Then
            GRIDSPL.Rows.Add(Val(TXTSRNO.Text.Trim), CMBSAMPLETYPE.Text.Trim, CMBITEMNAME.Text.Trim, CMBQUALITY.Text.Trim, CMBDESIGN.Text.Trim, CMBCOLOR.Text.Trim, Val(TXTNOOFBOOKLET.Text.Trim), TXTNARRATION.Text.Trim)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDSPL.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDSPL.Item(GSAMPLETYPE.Index, TEMPROW).Value = CMBSAMPLETYPE.Text.Trim
            GRIDSPL.Item(gitemname.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
            GRIDSPL.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDSPL.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDSPL.Item(GCOLOR.Index, TEMPROW).Value = CMBCOLOR.Text.Trim
            GRIDSPL.Item(GNOOFBOOKLET.Index, TEMPROW).Value = Val(TXTNOOFBOOKLET.Text.Trim)
            GRIDSPL.Item(GNARRATION.Index, TEMPROW).Value = TXTNARRATION.Text.Trim
            GRIDDOUBLECLICK = False
        End If

        GRIDSPL.FirstDisplayedScrollingRowIndex = GRIDSPL.RowCount - 1

        TXTSRNO.Text = GRIDSPL.RowCount + 1
        CMBSAMPLETYPE.Text = ""
        CMBITEMNAME.Text = ""
        CMBQUALITY.Text = ""
        CMBDESIGN.Text = ""
        CMBCOLOR.Text = ""
        TXTBARCODE.Clear()
        TXTNOOFBOOKLET.Clear()
        TXTNARRATION.Clear()

        CMBSAMPLETYPE.Focus()

        TOTAL()

    End Sub

    Sub TOTAL()
        Try
            TOTALNOOFBOOKLET.Text = 0

            For Each ROW As DataGridViewRow In GRIDSPL.Rows
                TOTALNOOFBOOKLET.Text = Format(Val(TOTALNOOFBOOKLET.Text.Trim) + Val(ROW.Cells(GNOOFBOOKLET.Index).Value), "0")
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            CMBPARTYNAME.Text = ""
            CMBSAMPLETYPE.Text = ""
            SPLDATE.Text = Now.Date
            TXTMODEOFSHIPMENT.Clear()
            TXTBARCODE.Clear()
            txtremarks.Clear()
            CMBITEMNAME.Text = ""
            CMBQUALITY.Text = ""
            CMBDESIGN.Text = ""
            CMBCOLOR.Text = ""
            TXTNOOFBOOKLET.Clear()
            TXTNARRATION.Clear()
            GRIDSPL.RowCount = 0

            txtbillno.Clear()
            EP.Clear()
            GETMAX_SPLNO()
            TOTALNOOFBOOKLET.Text = 0.0
            TXTSRNO.Text = 1
            If USERGODOWN <> "" Then CMBGODOWN.Text = USERGODOWN Else CMBGODOWN.Text = ""
            CHKDONT.CheckState = CheckState.Unchecked

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_SPLNO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(SPL_no),0) + 1 ", "SAMPLEBOOKLETINWARD", " AND SPL_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSPLNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub SampleBookletInward_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SAMPLE MODULE'")
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

                Dim OBJSMP As New ClsSampleBookletInward()
                OBJSMP.ALPARAVAL.Add(TEMPSPLNO)
                OBJSMP.ALPARAVAL.Add(YearId)
                Dim dt_po As DataTable = OBJSMP.selectSMP()
                If dt_po.Rows.Count > 0 Then
                    For Each dr As DataRow In dt_po.Rows
                        TXTSPLNO.Text = TEMPSPLNO
                        SPLDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")
                        CMBPARTYNAME.Text = Convert.ToString(dr("NAME"))
                        TXTMODEOFSHIPMENT.Text = Convert.ToString(dr("MODE"))
                        txtremarks.Text = Convert.ToString(dr("REMARKS"))
                        TOTALNOOFBOOKLET.Text = dr("TOTALNOOFBOOKLET")

                        GRIDSPL.Rows.Add(Val(dr("GRIDSRNO")), dr("SAMPLETYPE"), dr("ITEMNAME"), dr("QUALITYNAME"), dr("DESIGN").ToString, dr("COLOR").ToString, Val(dr("NOOFBOOKLET")), dr("NARRATION").ToString)
                        CMBGODOWN.Text = dr("GODOWN")
                        CHKDONT.Checked = Convert.ToBoolean(dr("DONTSHOWSTOCK"))
                    Next
                    GRIDSPL.FirstDisplayedScrollingRowIndex = GRIDSPL.RowCount - 1
                    TOTAL()
                Else
                    EDIT = False
                    CLEAR()
                End If
            End If

            TXTSRNO.Text = Val(GRIDSPL.RowCount) + 1

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillname(CMBPARTYNAME, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors')")
            fillQUALITY(CMBQUALITY, EDIT)
            FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
            FILLCOLOR(CMBCOLOR, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
            'If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
            FILLSAMPLETYPE(CMBSAMPLETYPE)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBPARTYNAME.Focus()
    End Sub

    Private Sub SampleBookletInward_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
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
                GRIDSPL.Focus()
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
    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
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

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete Sample barcode ?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPSPLNO)
                    alParaval.Add(YearId)

                    Dim clspo As New ClsSampleBookletInward()
                    clspo.ALPARAVAL = alParaval
                    Dim IntResult As Integer = clspo.Delete()
                    MsgBox("Sample Booklet Deleted")
                    CLEAR()
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
            If GRIDSPL.CurrentRow.Index >= 0 And GRIDSPL.Item(gsrno.Index, GRIDSPL.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TEMPROW = GRIDSPL.CurrentRow.Index
                TXTSRNO.Text = GRIDSPL.Item(gsrno.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                CMBSAMPLETYPE.Text = GRIDSPL.Item(GSAMPLETYPE.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Text = GRIDSPL.Item(gitemname.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDSPL.Item(GQUALITY.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDSPL.Item(GDESIGN.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                CMBCOLOR.Text = GRIDSPL.Item(GCOLOR.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                TXTNOOFBOOKLET.Text = GRIDSPL.Item(GNOOFBOOKLET.Index, GRIDSPL.CurrentRow.Index).Value
                TXTNARRATION.Text = GRIDSPL.Item(GNARRATION.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSPL_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDSPL.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSPL.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDSPL.Rows.RemoveAt(GRIDSPL.CurrentRow.Index)
                getsrno(GRIDSPL)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objpodtls As New SampleBookletInwardDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub ToolPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolPREVIOUS.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            GRIDSPL.RowCount = 0
LINE1:
            TEMPSPLNO = Val(TXTSPLNO.Text) - 1
            If TEMPSPLNO > 0 Then
                EDIT = True
                SampleBookletInward_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDSPL.RowCount = 0 And TEMPSPLNO > 1 Then
                TXTSPLNO.Text = TEMPSPLNO
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
            GRIDSPL.RowCount = 0
LINE1:
            TEMPSPLNO = Val(TXTSPLNO.Text) + 1
            GETMAX_SPLNO()
            Dim MAXNO As Integer = TXTSPLNO.Text.Trim
            CLEAR()
            If Val(TXTSPLNO.Text) - 1 >= TEMPSPLNO Then
                EDIT = True
                SampleBookletInward_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDSPL.RowCount = 0 And TEMPSPLNO < MAXNO Then
                TXTSPLNO.Text = TEMPSPLNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        'Try
        '    If MsgBox("Wish to Print Booklet Inward?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub
        '    Dim OBJSMP As New SaleOrderDesign
        '    OBJSMP.MdiParent = MDIMain
        '    OBJSMP.FRMSTRING = "SAMPLEBOOKLETINWARD"
        '    OBJSMP.FORMULA = "{SAMPLEBOOKLETINWARD.SPL_no}=" & Val(TXTSPLNO.Text.Trim) & " and {SAMPLEBOOKLETINWARD.SPL_yearid}=" & YearId
        '    OBJSMP.Show()
        'Catch ex As Exception
        '    Throw ex
        'End Try
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

    Private Sub SPLDATE_Validating(sender As Object, e As CancelEventArgs) Handles SPLDATE.Validating
        Try
            If SPLDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(SPLDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    If Not datecheck(SPLDATE.Text) Then
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
            If CMBPARTYNAME.Text.Trim <> "" Then namevalidate(CMBPARTYNAME, CMBCODE, e, Me, TXTADD, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors') ", "SUNDRY DEBTORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSPL_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSPL.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNARRATION_Validating(sender As Object, e As CancelEventArgs) Handles TXTNARRATION.Validating
        Try
            If CMBSAMPLETYPE.Text.Trim <> "" And CMBITEMNAME.Text.Trim <> "" And Val(TXTNOOFBOOKLET.Text.Trim) > 0 Then
                FILLGRID(CMBSAMPLETYPE.Text.Trim)
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillgrid(MATCHING As String)
        GRIDSPL.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDSPL.Rows.Add(Val(TXTSRNO.Text.Trim), CMBSAMPLETYPE.Text.Trim, CMBITEMNAME.Text.Trim, CMBQUALITY.Text.Trim, CMBDESIGN.Text.Trim, CMBCOLOR.Text.Trim, Format(Val(TXTNOOFBOOKLET.Text.Trim), "0.00"), Format(Val(TXTNARRATION.Text.Trim), "0.00"))
            getsrno(GRIDSPL)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDSPL.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDSPL.Item(GSAMPLETYPE.Index, TEMPROW).Value = CMBSAMPLETYPE.Text.Trim
            GRIDSPL.Item(gitemname.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
            GRIDSPL.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDSPL.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDSPL.Item(GCOLOR.Index, TEMPROW).Value = CMBCOLOR.Text.Trim
            GRIDSPL.Item(GNOOFBOOKLET.Index, TEMPROW).Value = Format(Val(TXTNOOFBOOKLET.Text.Trim), "0.00")

            GRIDSPL.Item(GNARRATION.Index, TEMPROW).Value = Format(Val(TXTNARRATION.Text.Trim), "0.00")

            GRIDDOUBLECLICK = False
        End If

        GRIDSPL.FirstDisplayedScrollingRowIndex = GRIDSPL.RowCount - 1

        TXTSRNO.Text = GRIDSPL.RowCount + 1



        If ClientName = "YASHVI" Then
            CMBITEMNAME.Text = ""
            CMBQUALITY.Text = ""
            CMBDESIGN.Text = ""
            CMBCOLOR.Text = ""
            TXTNARRATION.Clear()
            TXTNOOFBOOKLET.Clear()
            CMBSAMPLETYPE.Focus()

        End If


    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 And CMBSAMPLETYPE.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                'no need for yearid clause here as we need to fetch this barcode in all acccouting year
                Dim DT As DataTable = OBJCMN.SEARCH(" SAMPLEBARCODE.SB_NO AS SBNO, SAMPLEBARCODE.SB_GRIDSRNO AS GRIDSRNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_NAME,'') AS QUALITY, ISNULL(DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(SAMPLEBARCODE.SB_REMARKS, '') AS REMARKS, SAMPLEBARCODE.SB_BARCODE AS BARCODE,ISNULL(ITEMMASTER.ITEM_RATE,0)AS RATE", "", " SAMPLEBARCODE INNER JOIN ITEMMASTER ON SAMPLEBARCODE.SB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN QUALITYMASTER ON SAMPLEBARCODE.SB_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON SAMPLEBARCODE.SB_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON SAMPLEBARCODE.SB_DESIGNID = DESIGNMASTER.DESIGN_id  ", " AND SB_BARCODE = '" & TXTBARCODE.Text.Trim & "'")
                If DT.Rows.Count > 0 Then

                    GRIDSPL.Rows.Add(GRIDSPL.RowCount + 1, CMBSAMPLETYPE.Text.Trim, DT.Rows(0).Item("ITEMNAME"), "", DT.Rows(0).Item("DESIGN"), DT.Rows(0).Item("COLOR"), 1, "")
                    GRIDSPL.FirstDisplayedScrollingRowIndex = GRIDSPL.RowCount - 1

LINE1:
                    TXTBARCODE.Clear()
                    TXTBARCODE.Focus()
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
                GRIDSPL.RowCount = 0
                TEMPSPLNO = Val(txtbillno.Text)
                If TEMPSPLNO > 0 Then
                    EDIT = True
                    SampleBookletInward_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNOOFBOOKLET_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTNOOFBOOKLET.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub CMBPARTYNAME_Enter(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillname(CMBPARTYNAME, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors')")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(sender As Object, e As CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(sender As Object, e As EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SPLDATE_SystemColorsChanged(sender As Object, e As EventArgs) Handles SPLDATE.SystemColorsChanged

    End Sub

    Private Sub SPLDATE_GotFocus(sender As Object, e As EventArgs) Handles SPLDATE.GotFocus
        SPLDATE.SelectionStart = 0
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

    Private Sub SampleBookletInward_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "YASHVI" Then
                CMBQUALITY.TabStop = False
                CMBCOLOR.TabStop = False
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class