
Imports BL

Public Class RollsReturnFromWarper

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPROLLRETURNNO As Integer
    Public FRMSTRING As String

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBGODOWN.Focus()
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALENDS.Text = 0
            LBLTOTALROLLS.Text = 0
            LBLTOTALWT.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDROLLS.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALENDS.Text = Format(Val(LBLTOTALENDS.Text) + Val(ROW.Cells(GENDS.Index).EditedFormattedValue), "0")
                    LBLTOTALROLLS.Text = Format(Val(LBLTOTALROLLS.Text) + Val(ROW.Cells(GROLLS.Index).EditedFormattedValue), "0")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(Gwt.Index).EditedFormattedValue), "0.000")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()

        CMDSELECTROLLS.Enabled = True
        DTROLLSRETDATE.Text = Mydate
        If USERGODOWN <> "" Then CMBGODOWN.Text = USERGODOWN Else CMBGODOWN.Text = ""
        CMBNAME.Text = ""
        TXTCHALLANNO.Clear()
        DTCHALLANDATE.Clear()
        cmbtrans.Text = ""
        TXTREMARKS.Clear()

        LBLTOTALENDS.Text = 0.0
        LBLTOTALROLLS.Text = 0.0
        LBLTOTALWT.Text = 0.0

        TXTSRNO.Clear()
        CMBYARNQUALITY.Text = ""
        CMBMILL.Text = ""
        TXTENDS.Clear()
        TXTROLLS.Clear()
        TXTWT.Clear()
        TXTNARR.Clear()


        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        GRIDROLLS.RowCount = 0
        GETMAX_ROLLSRETURN_NO()

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False


        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        If gridupload.RowCount > 0 Then TXTUPLOADSRNO.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1 Else TXTUPLOADSRNO.Text = 1
        If GRIDROLLS.RowCount > 0 Then TXTSRNO.Text = Val(GRIDROLLS.RowCount) + 1 Else TXTSRNO.Text = 1

    End Sub

    Sub GETMAX_ROLLSRETURN_NO()
        Dim DTTABLE As DataTable = getmax("ISNULL(MAX(ROLLSRET_NO),0)+ 1", "ROLLSRETURN", "AND ROLLSRET_YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTROLLSRETNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub RollsReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDSAVE_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1) Then       'for CLEAR
                TabControl1.SelectedIndex = (0)
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2) Then       'for CLEAR
                TabControl1.SelectedIndex = (1)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call toolprevious_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call toolnext_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try

    End Sub

    Sub FILLCMB()
        If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' ")
        If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
        If CMBYARNQUALITY.Text = "" Then fillYARNQUALITY(CMBYARNQUALITY, EDIT)
        If CMBMILL.Text.Trim = "" Then FILLMILL(CMBMILL, EDIT)
        If cmbtrans.Text = "" Then FILLNAME(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT' ")
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, EDIT, "AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'AND LEDGERS.ACC_TYPE= 'TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then NAMEVALIDATE(cmbtrans, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtrans.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RollsReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ROLLS RECD'")
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

                Dim dttable As New DataTable
                Dim OBJROLLSRETURN As New ClsRollsReturnFromWarper

                OBJROLLSRETURN.alParaval.Add(TEMPROLLRETURNNO)
                OBJROLLSRETURN.alParaval.Add(YearId)
                dttable = OBJROLLSRETURN.selectROLLSRETURN()

                If dttable.Rows.Count > 0 Then
                    CMBGODOWN.Focus()

                    TXTROLLSRETNO.Text = TEMPROLLRETURNNO
                    DTROLLSRETDATE.Text = dttable.Rows(0).Item("DATE")
                    CMBGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    CMBNAME.Text = dttable.Rows(0).Item("WARPER").ToString
                    TXTCHALLANNO.Text = dttable.Rows(0).Item("CHALLANNO").ToString
                    DTCHALLANDATE.Text = dttable.Rows(0).Item("CHALLANDATE")
                    cmbtrans.Text = dttable.Rows(0).Item("TRANSPORT").ToString
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDROLLS.Rows.Add(Val(ROW("SRNO")), ROW("YARNQUALITY"), ROW("MILL"), Val(ROW("ENDS")), Val(ROW("ROLLS")), Format(Val(ROW("WT")), "0.00"), ROW("NARR"), Val(ROW("FROMNO")), Val(ROW("FROMSRNO")), ROW("GRIDTYPE"), ROW("DONE"))

                        If Convert.ToBoolean(ROW("DONE")) = True Then
                            GRIDROLLS.Rows(GRIDROLLS.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If
                    Next


                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ROLLSRETURN_UPLOAD.ROLLSRET_SRNO AS GRIDSRNO, ROLLSRETURN_UPLOAD.ROLLSRET_REMARKS AS REMARKS, ROLLSRETURN_UPLOAD.ROLLSRET_NAME AS NAME, ROLLSRETURN_UPLOAD.ROLLSRET_PHOTO AS IMGPATH ", "", " ROLLSRETURN_UPLOAD ", " AND ROLLSRETURN_UPLOAD.ROLLSRET_NO = " & TEMPROLLRETURNNO & " AND ROLLSRET_YEARID = " & YearId & " ORDER BY ROLLSRETURN_UPLOAD.ROLLSRET_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If
                    TOTAL()
                    CMDSELECTROLLS.Enabled = False
                End If

            Else
                CLEAR()
                EDIT = False
                DTROLLSRETDATE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTROLLSRETDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBGODOWN.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(DTCHALLANDATE.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(LBLTOTALENDS.Text.Trim)
            alParaval.Add(LBLTOTALROLLS.Text.Trim)
            alParaval.Add(LBLTOTALWT.Text.Trim)
            alParaval.Add(TXTREMARKS.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim QUALITY As String = ""
            Dim MILLNAME As String = ""
            Dim ENDS As String = ""
            Dim ROLLS As String = ""
            Dim WT As String = ""
            Dim NARR As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim GRIDTYPE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDROLLS.Rows
                If row.Cells(gsrno.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = row.Cells(gsrno.Index).Value
                        QUALITY = row.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        ENDS = Val(row.Cells(GENDS.Index).Value)
                        ROLLS = Val(row.Cells(GROLLS.Index).Value)
                        WT = Val(row.Cells(Gwt.Index).Value)
                        If row.Cells(GNARR.Index).Value <> Nothing Then NARR = row.Cells(GNARR.Index).Value.ToString Else NARR = ""
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        GRIDTYPE = row.Cells(GTYPE.Index).Value.ToString

                    Else

                        SRNO = SRNO & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        ENDS = ENDS & "|" & Val(row.Cells(GENDS.Index).Value)
                        ROLLS = ROLLS & "|" & Val(row.Cells(GROLLS.Index).Value)
                        WT = WT & "|" & Val(row.Cells(Gwt.Index).Value)
                        If row.Cells(GNARR.Index).Value <> Nothing Then NARR = NARR & "|" & row.Cells(GNARR.Index).Value.ToString Else NARR = NARR & "|" & ""
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        GRIDTYPE = GRIDTYPE & "|" & row.Cells(GTYPE.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(QUALITY)
            alParaval.Add(MILLNAME)
            alParaval.Add(ENDS)
            alParaval.Add(ROLLS)
            alParaval.Add(WT)
            alParaval.Add(NARR)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(GRIDTYPE)


            Dim OBJROLLSRET As New ClsRollsReturnFromWarper
            OBJROLLSRET.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJROLLSRET.save()
                TEMPROLLRETURNNO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPROLLRETURNNO)
                IntResult = OBJROLLSRET.Update()
                EDIT = False
                MsgBox("Details Updated")

            End If

            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTROLLSRETDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJROLLSRET As New ClsRollsReturnFromWarper


            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPROLLRETURNNO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJROLLSRET.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJROLLSRET.SAVEUPLOAD()
                End If
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLUPLOAD()

        If GRIDUPLOADDOUBLECLICK = False Then
            gridupload.Rows.Add(Val(TXTUPLOADSRNO.Text.Trim), txtuploadremarks.Text.Trim, txtuploadname.Text.Trim, PBSOFTCOPY.Image)
            getsrno(gridupload)
        ElseIf GRIDUPLOADDOUBLECLICK = True Then

            gridupload.Item(GUSRNO.Index, TEMPUPLOADROW).Value = TXTUPLOADSRNO.Text.Trim
            gridupload.Item(GUREMARKS.Index, TEMPUPLOADROW).Value = txtuploadremarks.Text.Trim
            gridupload.Item(GUNAME.Index, TEMPUPLOADROW).Value = txtuploadname.Text.Trim
            gridupload.Item(GUIMGPATH.Index, TEMPUPLOADROW).Value = PBSOFTCOPY.Image

            GRIDUPLOADDOUBLECLICK = False

        End If
        gridupload.FirstDisplayedScrollingRowIndex = gridupload.RowCount - 1

        TXTUPLOADSRNO.Clear()
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSOFTCOPY.Image = Nothing
        TXTIMGPATH.Clear()

        txtuploadremarks.Focus()

    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If DTROLLSRETDATE.Text = "__/__/____" Then
            EP.SetError(DTROLLSRETDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTROLLSRETDATE.Text) Then
                EP.SetError(DTROLLSRETDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If DTROLLSRETDATE.Text.Trim <> "__/__/____" And DTCHALLANDATE.Text.Trim <> "__/__/____" Then
            If Convert.ToDateTime(DTROLLSRETDATE.Text).Date > Convert.ToDateTime(DTCHALLANDATE.Text).Date Then
                EP.SetError(DTCHALLANDATE, " Please Enter Proper Challan Date")
                bln = False
            End If
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Sizer Name")
            bln = False
        End If

        If cmbtrans.Text.Trim.Length = 0 Then
            EP.SetError(cmbtrans, "Please Select Transport Name")
            bln = False
        End If

        If CMBGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBGODOWN, " Please Fill Godown ")
            bln = False
        End If

        If GRIDROLLS.RowCount = 0 Then
            EP.SetError(TXTNARR, "Enter Proper Details")
            bln = False
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, entry Locked")
            bln = False
        End If

        Return bln
    End Function

    Private Sub DTROLLSRETDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTROLLSRETDATE.GotFocus
        DTROLLSRETDATE.SelectAll()
    End Sub

    Private Sub DTCHALLANDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTCHALLANDATE.GotFocus
        DTCHALLANDATE.SelectAll()
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

    Private Sub CMBNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
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

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' ", "SUNDRY CREDITORS", "ACCOUNTS", "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim <> "" Then fillGODOWN(CMBGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBYARNQUALITY.Enter
        Try
            If CMBYARNQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBYARNQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBYARNQUALITY.Validating
        Try
            If CMBYARNQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBYARNQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then FILLMILL(CMBMILL, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then MILLVALIDATE(CMBMILL, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDROLLS.RowCount = 0
LINE1:
            TEMPROLLRETURNNO = Val(TXTROLLSRETNO.Text) - 1
Line2:
            If TEMPROLLRETURNNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ROLLSRET_NO ", "", "  ROLLSRETURN", " AND ROLLSRET_NO = '" & TEMPROLLRETURNNO & "' AND ROLLSRETURN.ROLLSRET_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    RollsReturn_Load(sender, e)
                Else
                    TEMPROLLRETURNNO = Val(TEMPROLLRETURNNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDROLLS.RowCount = 0 And TEMPROLLRETURNNO > 1 Then
                TXTROLLSRETNO.Text = TEMPROLLRETURNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDROLLS.RowCount = 0
LINE1:
            TEMPROLLRETURNNO = Val(TXTROLLSRETNO.Text) + 1
            GETMAX_ROLLSRETURN_NO()
            Dim MAXNO As Integer = TXTROLLSRETNO.Text.Trim
            CLEAR()
            If Val(TXTROLLSRETNO.Text) - 1 >= TEMPROLLRETURNNO Then
                EDIT = True
                RollsReturn_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDROLLS.RowCount = 0 And TEMPROLLRETURNNO < MAXNO Then
                TXTROLLSRETNO.Text = TEMPROLLRETURNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tstxtbillno.Validated
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDROLLS.RowCount = 0
                TEMPROLLRETURNNO = Val(tstxtbillno.Text)
                If TEMPROLLRETURNNO > 0 Then
                    EDIT = True
                    RollsReturn_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridupload_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And gridupload.Item(GUSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDUPLOADDOUBLECLICK = True
                TXTUPLOADSRNO.Text = gridupload.Item(GUSRNO.Index, e.RowIndex).Value
                txtuploadremarks.Text = gridupload.Item(GUREMARKS.Index, e.RowIndex).Value
                txtuploadname.Text = gridupload.Item(GUNAME.Index, e.RowIndex).Value
                PBSOFTCOPY.Image = gridupload.Item(GUIMGPATH.Index, e.RowIndex).Value

                TEMPUPLOADROW = e.RowIndex
                txtuploadremarks.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
        Try
            If e.KeyCode = Keys.Delete And gridupload.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                If GRIDUPLOADDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                gridupload.Rows.RemoveAt(gridupload.CurrentRow.Index)
                getsrno(gridupload)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtuploadname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
        Try
            If txtuploadremarks.Text.Trim <> "" And txtuploadname.Text.Trim <> "" And PBSOFTCOPY.ImageLocation <> "" Then
                FILLUPLOAD()
            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTUPLOADSRNO_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTUPLOADSRNO.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                TXTUPLOADSRNO.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1
            Else
                TXTUPLOADSRNO.Text = 1
            End If
        End If
    End Sub

    Private Sub CMDUPLOAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDUPLOAD.Click
        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpg;*.png"
        OpenFileDialog1.ShowDialog()
        TXTIMGPATH.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTIMGPATH.Text.Trim.Length <> 0 Then PBSOFTCOPY.ImageLocation = TXTIMGPATH.Text.Trim
    End Sub

    Private Sub CMDREMOVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREMOVE.Click
        Try
            PBSOFTCOPY.Image = Nothing
            TXTIMGPATH.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDVIEW.Click
        Try
            If gridupload.SelectedRows.Count > 0 Then
                Dim objVIEW As New ViewImage
                objVIEW.pbsoftcopy.Image = PBSOFTCOPY.Image
                objVIEW.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSOFTCOPY.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDROLLS_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDROLLS.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDROLLS.Item(gsrno.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDROLLS.Item(gsrno.Index, e.RowIndex).Value
                CMBYARNQUALITY.Text = GRIDROLLS.Item(GYARNQUALITY.Index, e.RowIndex).Value
                CMBMILL.Text = GRIDROLLS.Item(GMILLNAME.Index, e.RowIndex).Value
                TXTENDS.Text = GRIDROLLS.Item(GENDS.Index, e.RowIndex).Value
                TXTROLLS.Text = GRIDROLLS.Item(GROLLS.Index, e.RowIndex).Value
                TXTWT.Text = GRIDROLLS.Item(Gwt.Index, e.RowIndex).Value
                TXTNARR.Text = GRIDROLLS.Item(GNARR.Index, e.RowIndex).Value

                TEMPROW = e.RowIndex
                CMBYARNQUALITY.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub GRIDROLLS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDROLLS.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDROLLS.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDROLLS.Rows.RemoveAt(GRIDROLLS.CurrentRow.Index)
                getsrno(GRIDROLLS)
                TXTSRNO.Text = GRIDROLLS.RowCount + 1
                TOTAL()

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call CMDSAVE_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call CMDDELETE_Click(sender, e)
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Dim IntResult As Integer
        Try
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If lbllocked.Visible = True Then
                    MsgBox("Unable to Delete, Entry Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Delete Entry?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPROLLRETURNNO)
                    alParaval.Add(YearId)

                    Dim CLSRET As New ClsRollsReturnFromWarper
                    CLSRET.alParaval = alParaval
                    IntResult = CLSRET.Delete()

                    MsgBox("Entry Deleted")
                    CLEAR()
                    EDIT = False
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDROLLS.Rows.Add(Val(TXTSRNO.Text.Trim), CMBYARNQUALITY.Text.Trim, CMBMILL.Text.Trim, Val(TXTENDS.Text.Trim), Val(TXTROLLS.Text.Trim), Format(Val(TXTWT.Text.Trim), "0.000"), TXTNARR.Text.Trim)
            Else
                GRIDROLLS.Item(gsrno.Index, TEMPROW).Value = TXTSRNO.Text.Trim
                GRIDROLLS.Item(GYARNQUALITY.Index, TEMPROW).Value = CMBYARNQUALITY.Text.Trim
                GRIDROLLS.Item(GMILLNAME.Index, TEMPROW).Value = CMBMILL.Text.Trim
                GRIDROLLS.Item(GENDS.Index, TEMPROW).Value = Val(TXTENDS.Text.Trim)
                GRIDROLLS.Item(GROLLS.Index, TEMPROW).Value = Val(TXTROLLS.Text.Trim)
                GRIDROLLS.Item(Gwt.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.000")
                GRIDROLLS.Item(GNARR.Index, TEMPROW).Value = TXTNARR.Text.Trim

                GRIDDOUBLECLICK = False
            End If
            CMBYARNQUALITY.Text = ""
            CMBMILL.Text = ""
            TXTENDS.Clear()
            TXTROLLS.Clear()
            TXTWT.Clear()
            TXTNARR.Clear()
            getsrno(GRIDROLLS)
            TOTAL()
            CMBYARNQUALITY.Focus()
            If GRIDROLLS.RowCount > 0 Then TXTSRNO.Text = Val(GRIDROLLS.RowCount) + 1 Else TXTSRNO.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub TXTNARR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARR.Validating
        If CMBYARNQUALITY.Text.Trim <> "" And CMBMILL.Text.Trim <> "" And Val(TXTENDS.Text.Trim) > 0 And Val(TXTROLLS.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 Then FILLGRID() Else MsgBox("Please Enter proper details")
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJROLLSRET As New RollsReturnFromWarperDetails
            OBJROLLSRET.MdiParent = MDIMain
            OBJROLLSRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTENDS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTENDS.KeyPress, TXTROLLS.KeyPress, TXTCHALLANNO.KeyPress, tstxtbillno.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdot3(e, sender, Me)
    End Sub

    Private Sub DTROLLSRETDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTROLLSRETDATE.Validating
        Try
            If DTROLLSRETDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTROLLSRETDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTCHALLANDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTCHALLANDATE.Validating
        Try
            If DTCHALLANDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTCHALLANDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTROLLS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTROLLS.Click
        Try
            If CMBNAME.Text.Trim = "" Then
                MsgBox("Select Name First", MsgBoxStyle.Critical)
                CMBNAME.Focus()
                Exit Sub
            End If

            Dim OBJROLLISSUE As New SelectRollIssue
            OBJROLLISSUE.SIZERNAME = CMBNAME.Text.Trim
            Dim DT As DataTable = OBJROLLISSUE.DT
            OBJROLLISSUE.ShowDialog()
            If DT.Rows.Count > 0 Then
                For Each ROW As DataRow In DT.Rows
                    GRIDROLLS.Rows.Add(0, ROW("YARNQUALITY"), ROW("MILLNAME"), Val(ROW("ENDS")), Val(ROW("ROLLS")), Format(Val(ROW("WT")), "0.000"), "", Val(ROW("ROLLISSUENO")), Val(ROW("ROLLISSUESRNO")), ROW("TYPE"), 0)
                Next
                getsrno(GRIDROLLS)
                CMDSELECTROLLS.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class