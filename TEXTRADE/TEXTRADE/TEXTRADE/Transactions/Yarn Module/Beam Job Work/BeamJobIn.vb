
Imports System.ComponentModel
Imports BL
Imports DevExpress.Diagram.Core.Native
Imports DevExpress.XtraGrid.Views.Base
Public Class BeamJobIn
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPBEAMJONO, tempzalanirollno As Integer
    Dim TEMPMSG As Integer
    Dim NextBeamNo As Integer
    Dim MAXNO As Integer = 0

    Dim TEMPBEAMNO As Integer

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDSAVE_Click(sender As Object, e As EventArgs) Handles CMDSAVE.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTBEAMJODATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBGODOWN.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(Val(LBLTOTALMTRS.Text.Trim))
            alParaval.Add(Val(LBLTOTALBEAMWT.Text.Trim))
            alParaval.Add(TXTREMARKS.Text.Trim)

            'alParaval.Add(Val(LBLTAPLINE.Text.Trim))
            alParaval.Add(CMBJONO.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)




            Dim SRNO As String = ""
            Dim BEAMNO As String = ""
            Dim BEAMNAME As String = ""
            Dim MILLNAME As String = ""
            Dim TOTALENDS As String = ""
            Dim TOTALMTRS As String = ""
            Dim WT As String = ""
            Dim GAMANO As String = ""
            Dim SECTION As String = ""
            Dim ROLLNO As String = ""
            Dim BREAKAGE As String = ""
            Dim GRIDREMARKS As String = ""
            Dim GRIDDONE As String = ""
            Dim OUTWT As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDBEAM.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = row.Cells(GSRNO.Index).Value
                        BEAMNO = Val(row.Cells(GBEAMNO.Index).Value)
                        BEAMNAME = row.Cells(GBEAMNAME.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        TOTALENDS = Val(row.Cells(GTOTALENDS.Index).Value)
                        TOTALMTRS = Val(row.Cells(GTOTALMTRS.Index).Value)
                        WT = Val(row.Cells(GWT.Index).Value)
                        GAMANO = Val(row.Cells(GGAMANO.Index).Value)
                        SECTION = Val(row.Cells(GSECTION.Index).Value)
                        ROLLNO = Val(row.Cells(GROLLNO.Index).Value)
                        BREAKAGE = Val(row.Cells(GBREAKAGE.Index).Value)
                        GRIDREMARKS = row.Cells(GGRIDREMARKS.Index).Value.ToString
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString

                        GRIDDONE = Val(row.Cells(GGRIDDONE.Index).Value)
                        OUTWT = Val(row.Cells(GOUTWT.Index).Value)


                    Else

                        SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                        BEAMNO = BEAMNO & "|" & Val(row.Cells(GBEAMNO.Index).Value)
                        BEAMNAME = BEAMNAME & "|" & row.Cells(GBEAMNAME.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        TOTALENDS = TOTALENDS & "|" & Val(row.Cells(GTOTALENDS.Index).Value)
                        TOTALMTRS = TOTALMTRS & "|" & Val(row.Cells(GTOTALMTRS.Index).Value)
                        WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                        GAMANO = GAMANO & "|" & Val(row.Cells(GGAMANO.Index).Value)
                        SECTION = SECTION & "|" & Val(row.Cells(GSECTION.Index).Value)
                        ROLLNO = ROLLNO & "|" & Val(row.Cells(GROLLNO.Index).Value)
                        BREAKAGE = BREAKAGE & "|" & Val(row.Cells(GBREAKAGE.Index).Value)
                        GRIDREMARKS = GRIDREMARKS & "|" & row.Cells(GGRIDREMARKS.Index).Value.ToString
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value.ToString

                        GRIDDONE = GRIDDONE & "|" & Val(row.Cells(GGRIDDONE.Index).Value)
                        OUTWT = OUTWT & "|" & Val(row.Cells(GOUTWT.Index).Value)


                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(BEAMNO)
            alParaval.Add(BEAMNAME)
            alParaval.Add(MILLNAME)
            alParaval.Add(TOTALENDS)
            alParaval.Add(TOTALMTRS)
            alParaval.Add(WT)
            alParaval.Add(GAMANO)
            alParaval.Add(SECTION)
            alParaval.Add(ROLLNO)
            alParaval.Add(BREAKAGE)
            alParaval.Add(GRIDREMARKS)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)
            alParaval.Add(GRIDDONE)
            alParaval.Add(OUTWT)


            Dim OBJBEAMREC As New ClsBeamJobIn
            OBJBEAMREC.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJBEAMREC.SAVE()
                TEMPBEAMJONO = DT.Rows(0).Item(0)


                MsgBox("Details Added")

                'If ClientName = "SWPL" Then
                '    'NOW NO NEED TO GENERATE INVOICE IN ABHEE
                '    GENERATECONSUMPTION()
                'End If


            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPBEAMJONO)
                IntResult = OBJBEAMREC.UPDATE()
                EDIT = False
                MsgBox("Details Updated")

            End If

            'If lbllocked.Visible = False Then
            '    If MsgBox("Issue Beam Directly to Weaver?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '        Dim OBJWEAVER As New DirectIssueWeaver
            '        OBJWEAVER.ShowDialog()
            '        If OBJWEAVER.cmbname.Text.Trim = "" Then GoTo LINE1
            ' DIRECTISSUEWEAVER(OBJWEAVER.cmbname.Text.Trim)
            '    End If
            'End If

LINE1:

            CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTBEAMJODATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            GRIDBEAM.RowCount = 0
LINE1:
            TEMPBEAMJONO = Val(TXTBEAMJONO.Text) - 1
Line2:
            If TEMPBEAMJONO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" BJI_NO ", "", "  BEAMJOBIN", " AND BJI_NO = '" & TEMPBEAMJONO & "' AND BEAMJOBIN.BJI_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    BeamJobOut_Load(sender, e)
                Else
                    TEMPBEAMJONO = Val(TEMPBEAMJONO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDBEAM.RowCount = 0 And TEMPBEAMJONO > 1 Then
                TXTBEAMJONO.Text = TEMPBEAMJONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            GRIDBEAM.RowCount = 0
LINE1:
            TEMPBEAMJONO = Val(TXTBEAMJONO.Text) + 1
            GETMAX_BEAMJO_NO()
            Dim MAXNO As Integer = TXTBEAMJONO.Text.Trim
            CLEAR()
            If Val(TXTBEAMJONO.Text) - 1 >= TEMPBEAMJONO Then
                EDIT = True
                BeamJobOut_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDBEAM.RowCount = 0 And TEMPBEAMJONO < MAXNO Then
                TXTBEAMJONO.Text = TEMPBEAMJONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tstxtbillno.KeyPress
        numkeypress(e, tstxtbillno, Me)
    End Sub

    Private Sub tstxtbillno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tstxtbillno.Validated
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDBEAM.RowCount = 0
                TEMPBEAMJONO = Val(tstxtbillno.Text)
                If TEMPBEAMJONO > 0 Then
                    EDIT = True
                    BeamJobOut_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDBEAM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDBEAM.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDBEAM.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                'If Convert.ToBoolean(GRIDBEAM.CurrentRow.Cells(GDONE.Index).Value) = True Then
                '    MsgBox("Beam Locked", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If

                GRIDBEAM.Rows.RemoveAt(GRIDBEAM.CurrentRow.Index)
                getsrno(GRIDBEAM)

                TOTAL()


            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call CMDSAVE_Click(sender, e)
    End Sub
    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call CMDDELETE_Click(sender, e)
    End Sub
    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Sub CLEAR()

        CMDSELECTYARNISSUE.Enabled = True
        TXTBEAMJONO.Clear()
        DTBEAMJODATE.Text = Mydate
        CMBGODOWN.Text = USERGODOWN
        CMBNAME.Text = ""

        LBLTAPLINE.Text = 0.0
        TXTREMARKS.Clear()
        'TXTBEAMNO.Clear()

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False
        CMBJONO.Text = ""
        TXTREMARKS.Clear()
        CMBNAME.Enabled = True

        GRIDBEAM.RowCount = 0

        GETMAX_BEAMJO_NO()

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        TabControl1.SelectedIndex = 0

        'PBSOFTCOPY.Image = Nothing
        'TXTUPLOADSRNO.Clear()
        'txtuploadname.Clear()
        'txtuploadremarks.Clear()
        'TXTIMGPATH.Clear()
        'gridupload.RowCount = 0

        'TXTUPLOADSRNO.Text = 1

        'GetLastBeamNo()
        'TXTBEAMNO.Text = NextBeamNo

        'FILLROLLITEM(CMBROLLNO, EDIT, "AND ROLLITEM = 1 ", "HAVING SUM(QTY - ISSQTY) >0")
        'CMBROLLNO.Enabled = True

        LBLTOTALMTRS.Text = 0.0
        LBLTAPLINE.Text = 0.0
        LBLTOTALBEAMWT.Text = 0.0

    End Sub

    Sub GETMAX_BEAMJO_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(BJI_NO),0)+1", "BEAMJOBIN", "AND BJI_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTBEAMJONO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub BeamJobOut_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1) Then       'for scheduling
                TabControl1.SelectedIndex = (0)
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2) Then       'for ITEM DETAILS
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
        If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If DTBEAMJODATE.Text = "__/__/____" Then
            EP.SetError(DTBEAMJODATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTBEAMJODATE.Text) Then
                EP.SetError(DTBEAMJODATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Jobber Name")
            bln = False
        End If

        If CMBGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBGODOWN, " Please Fill Godown ")
            bln = False
        End If

        If lbllocked.Visible = True And UserName <> "Admin" Then
            EP.SetError(lbllocked, "Item Used, Item Locked")
            bln = False
        End If

        For Each row As DataGridViewRow In GRIDBEAM.Rows
            If Val(row.Cells(GWT.Index).Value) = 0 Then
                EP.SetError(GRIDBEAM, "Beam Wt Cannot be 0 or Less")
                bln = False
            End If
        Next

        'For Each row As DataGridViewRow In GRIDBEAM.Rows
        '    If Val(row.Cells(GBEAMMTRS.Index).Value) > Val(row.Cells(GJOBMTRS.Index).Value) Then
        '        EP.SetError(CMBOURGODOWN, "Beam Mtrs Cannot be Greater Than Job Mtrs")
        '        row.DefaultCellStyle.BackColor = Color.LightGreen
        '        bln = False
        '    End If
        'Next

        Return bln
    End Function

    Private Sub DTBEAMJODATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTBEAMJODATE.GotFocus
        DTBEAMJODATE.Select(0, 0)
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
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' )"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "ACCOUNTS", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
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

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
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

                TEMPMSG = MsgBox("Delete Entry?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPBEAMJONO)
                    alParaval.Add(YearId)

                    Dim OBJDEL As New ClsBeamJobIn
                    OBJDEL.alParaval = alParaval
                    Dim IntResult As Integer = OBJDEL.Delete()
                    MsgBox("Entry Deleted")
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

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJBEAM As New BeamJobOutDetails
            OBJBEAM.MdiParent = MDIMain
            OBJBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamJobOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'BEAM RECD'")
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
                Dim OBJBEAMREC As New ClsBeamJobIn

                OBJBEAMREC.alParaval.Add(TEMPBEAMJONO)
                OBJBEAMREC.alParaval.Add(YearId)
                dttable = OBJBEAMREC.selectBEAM()

                If dttable.Rows.Count > 0 Then
                    CMBNAME.Focus()

                    TXTBEAMJONO.Text = TEMPBEAMJONO
                    DTBEAMJODATE.Text = dttable.Rows(0).Item("DATE")
                    CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    CMBGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    LBLTOTALMTRS.Text = dttable.Rows(0).Item("TOTALMTRS")
                    LBLTOTALBEAMWT.Text = dttable.Rows(0).Item("TOTALWT")
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString
                    CMBJONO.Text = dttable.Rows(0).Item("JONO")



                    CMDSELECTYARNISSUE.Enabled = False

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDBEAM.Rows.Add(Val(ROW("GRIDSRNO")), Val(ROW("BEAMNO")), ROW("BEAMNAME"), ROW("MILLNAME"), Val(ROW("GRIDTOTALENDS")), Val(ROW("GRIDTOTALMTRS")), ROW("BEAMWT"), Val(ROW("GAMANO")), Val(ROW("SECTION")), Val(ROW("ROLLNO")), Val(ROW("BREAKAGE")), ROW("GRIDREMARKS"), ROW("FROMNO"), ROW("FROMSRNO"), ROW("FROMTYPE"), ROW("GRIDDONE"), Val(ROW("OUTWT")))

                        If Convert.ToBoolean(ROW("GRIDDONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                            GRIDBEAM.Rows(GRIDBEAM.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                        End If

                        If Val(ROW("OUTWT")) > 0 Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                            GRIDBEAM.Rows(GRIDBEAM.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                        End If
                    Next


                    '        'UPLOAD(GRID)
                    '        Dim OBJCMN As New ClsCommon
                    '        Dim DT As DataTable = OBJCMN.SEARCH(" BEAMRECEIVEDWARPER_UPLOAD.BEAMREC_SRNO AS GRIDSRNO, BEAMRECEIVEDWARPER_UPLOAD.BEAMREC_REMARKS AS REMARKS, BEAMRECEIVEDWARPER_UPLOAD.BEAMREC_NAME AS NAME, BEAMRECEIVEDWARPER_UPLOAD.BEAMREC_PHOTO AS IMGPATH ", "", " BEAMRECEIVEDWARPER_UPLOAD ", " AND BEAMRECEIVEDWARPER_UPLOAD.BEAMREC_NO = " & TEMPBEAMRECDNO & " AND BEAMREC_YEARID = " & YearId & " ORDER BY BEAMRECEIVEDWARPER_UPLOAD.BEAMREC_SRNO")
                    '        If DT.Rows.Count > 0 Then
                    '            For Each DTR As DataRow In DT.Rows
                    '                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                    '            Next
                    '        End If
                    '        TOTAL()
                End If

                '    CMBROLLNO.Enabled = False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTYARNISSUE_Click(sender As Object, e As EventArgs) Handles CMDSELECTYARNISSUE.Click
        Try

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            'If GRIDSCHEDULE.RowCount = 0 And ClientName = "SASHWINKUMAR" Then
            '    MsgBox("First Schedule Beams", MsgBoxStyle.Critical)
            '    Exit Sub
            'End If


            'IT IS NOT MANDATE TO SELECT GODOWN HERE,
            'IF USER SELECTS GODOWN THEN WE WILL ADD THAT IN WHERE CLAUE OR ELSE SHOW ALL BEAMS WHICH ARE PRESENT WITH SIZER OR INHOUSE BOTH


            'SHOW ONLY THOSE BEAMS IN STOCK WHICH WE HAVE SELECTED IN SCHEDULING
            Dim WHERECLAUSE As String = ""
            For Each ROW As DataGridViewRow In GRIDBEAM.Rows
                If WHERECLAUSE = "" Then
                    WHERECLAUSE = " AND BEAMNAME IN ('" & ROW.Cells(GBEAMNAME.Index).Value & "'"
                Else
                    WHERECLAUSE = WHERECLAUSE & ",'" & ROW.Cells(GBEAMNAME.Index).Value & "'"
                End If
            Next
            If WHERECLAUSE <> "" Then WHERECLAUSE = WHERECLAUSE & ")"
            'WHERECLAUSE = WHERECLAUSE & " AND DATE <= '" & Format(Convert.ToDateTime(DTISSUEDATE.Text).Date, "MM/dd/yyyy") & "'"


            Dim OBJSELECTSTOCK As New SelectBeamStock
            OBJSELECTSTOCK.TEMPGODOWNNAME = CMBGODOWN.Text.Trim
            Dim DTBEAMSTOCK As DataTable = OBJSELECTSTOCK.DT
            OBJSELECTSTOCK.WHERECLAUSE = WHERECLAUSE & " AND GODOWN = '" & CMBGODOWN.Text.Trim & "'"
            'OBJSELECTSTOCK.ALLOWEDBEAMS = GRIDSCHEDULE.RowCount
            OBJSELECTSTOCK.ShowDialog()
            If DTBEAMSTOCK.Rows.Count > 0 Then

                'CHECK IF 1ST BEAM HAS 0 IN SRNO THEN CLEAR THE GRID
                'NEED TO CHECK WHETHER ANY ROW IS PRESENT OR NOT ELSE IT GIVES ERROR
                If GRIDBEAM.RowCount <> 0 Then
                    If Val(GRIDBEAM.Rows(0).Cells(GSRNO.Index).Value) = 0 Then GRIDBEAM.RowCount = 0
                End If


                For Each ROW As DataRow In DTBEAMSTOCK.Rows
                    Dim WTMTRS As Double = Val(ROW("WT"))
                    If ClientName = "SWPL" Then WTMTRS = Val(ROW("MTRS"))
                    GRIDBEAM.Rows.Add(0, ROW("BEAMNAME"), ROW("BEAMNO"), Val(ROW("ENDS")), Val(ROW("TAPLINE")), Format(Val(ROW("CUT")), "0.00"), Format(Val(WTMTRS), "0.000"), Format(Val(ROW("WTCUT")), "0.000"), "", Val(ROW("FROMNO")), Val(ROW("FROMSRNO")), ROW("TYPE"), 0, 0, ROW("SIZERNAME"), 0, "")
                Next
                TOTAL()
                getsrno(GRIDBEAM)
                CMDSELECTYARNISSUE.Enabled = False
            End If

            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTBEAMRECDDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTBEAMJODATE.Validating
        Try
            If DTBEAMJODATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTBEAMJODATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click_1(sender As Object, e As EventArgs) Handles tooldelete.Click
        Call CMDDELETE_Click(sender, e)
    End Sub

    Sub getmax_BEAMNO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(BJI_NO),0) + 1 ", "BEAMJOBIN ", "  AND BJI_CMPID=" & CmpId)
        If DTTABLE.Rows.Count > 0 Then tempzalanirollno = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub GRIDBEAM_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDBEAM.CellValidating
        Dim colNum As Integer = GRIDBEAM.Columns(e.ColumnIndex).Index
        If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return
        Select Case colNum

            Case GTOTALMTRS.Index
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                If bValid Then
                    If GRIDBEAM.CurrentCell.Value = Nothing Then GRIDBEAM.CurrentCell.Value = "0.00"
                    GRIDBEAM.CurrentCell.Value = Convert.ToDecimal(GRIDBEAM.Item(colNum, e.RowIndex).Value)
                    TOTAL()
                Else
                    MessageBox.Show("Invalid Number Entered")
                    e.Cancel = True
                    'Exit Sub
                End If
        End Select
    End Sub

    Sub TOTAL()
        Try
            Dim TEMPWARPWT As Double
            Dim TEMPSELWT As Double
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALBEAMWT.Text = 0.0

            'Dim TOTALTAPLINE As Double
            For Each ROW As DataGridViewRow In GRIDBEAM.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then

                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GTOTALMTRS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALBEAMWT.Text = Format(Val(LBLTOTALBEAMWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.00")
                End If
            Next
            'TXTTOTALMTRS.Text = Format(Val(LBLTOTALBEAMMTRS.Text), "0.00")

            'If ClientName = "SWPL" Then
            'If Val(TXTBEAMWT.Text.Trim) = 0 Then
            '    Dim OBJCMN As New ClsCommon
            '    Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(DESIGNCARD.DESIGN_TOTALWARPWT, 0) AS TOTALWARPWT, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEWT, 0) AS TOTALSELWT, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME ", "", " DESIGNCARD INNER JOIN ITEMMASTER ON DESIGNCARD.DESIGN_ITEMID = ITEMMASTER.item_id ", " AND ITEMMASTER.item_name = '" & GRIDBEAM.Item(GITEMNAME.Index, GRIDBEAM.CurrentRow.Index).Value & "' AND DESIGNCARD.DESIGN_YEARID = " & YearId)
            '    If DT.Rows.Count > 0 Then
            '        TEMPWARPWT = DT.Rows(0).Item("TOTALWARPWT")
            '        TEMPSELWT = DT.Rows(0).Item("TOTALSELWT")
            '    End If
            '    TXTBEAMWT.Text = Format(Val(TEMPWARPWT + TEMPSELWT) * Val(GRIDBEAM.Item(GBEAMMTRS.Index, GRIDBEAM.CurrentRow.Index).EditedFormattedValue), "0.00")
            'End If
            'End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTREMARKS_KeyDown(sender As Object, e As KeyEventArgs) Handles TXTREMARKS.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then TXTREMARKS.Text = OBJREMARKS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(sender As Object, e As EventArgs) Handles CMBNAME.Validated
        Try
            If EDIT = False And CMBNAME.Text.Trim <> "" Then
                CMBJONO.Items.Clear()
                'FILL JOBOUT NO
                'IF USER HAS NOT WRITTEN BILLNO THEN IT WONT BE SHOWN HERE
                'IF USER HAS WRITTEN LOTNO THEN IT WONT BE SHOWN HERE
                Dim OBJCMN As New ClsCommon

                ''WE HAVE CHANGED THE CODE FOR OPENING BY GULKIT, COZ WHEN WE TRANSFER STOCK FROM LAST YEAR WE WILL NEED JOBOUT LOTNO IN THIS YEAR'S OPENING
                ''AND IF WE KEEP LOTNO BLANK THEN IT WONT BE FETCHED IN JOBIN
                ''Dim DT As DataTable = OBJCMN.search(" JONO ", "", " (SELECT JOBOUT.JO_no AS JONO FROM JOBOUT INNER JOIN LEDGERS ON JOBOUT.JO_ledgerid = LEDGERS.Acc_id WHERE LEDGERS.Acc_CMPNAME='" & cmbname.Text.Trim & "' AND ROUND((JOBOUT.JO_TOTALMTRS - JOBOUT.JO_RECDMTRS),2) > 0 AND JOBOUT.JO_CLOSE=0 AND JOBOUT.JO_YEARID = " & YearId & " UNION ALL SELECT DISTINCT SM_BILLNO AS JONO FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO= LEDGERS.Acc_id WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ROUND((SM_MTRS - SM_OUTMTRS),2) > 0 AND SM_BILLNO <> 0 AND (SM_LOTNO = '' or SM_LOTNO = 0) AND SM_YEARID = " & YearId & ") AS T", "")


                ''THIS CODE IS FOR MTRS, IF CLIENT IS ON LOTSTATUS ON PCS THEN GIVES ISSUE
                ''Dim DT As DataTable = OBJCMN.SEARCH(" JONO ", "", " (SELECT JOBOUT.JO_no AS JONO FROM JOBOUT INNER JOIN LEDGERS ON JOBOUT.JO_ledgerid = LEDGERS.Acc_id WHERE LEDGERS.Acc_CMPNAME='" & cmbname.Text.Trim & "' AND ROUND((JOBOUT.JO_TOTALMTRS - JOBOUT.JO_RECDMTRS),2) > 0 AND JOBOUT.JO_CLOSE=0 AND ISNULL(JOBOUT.JO_LOTCOMPLETED,0)=0 AND JOBOUT.JO_YEARID = " & YearId & " UNION ALL SELECT DISTINCT SM_BILLNO AS JONO FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO= LEDGERS.Acc_id WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ROUND((SM_MTRS - SM_OUTMTRS),2) > 0 AND SM_BILLNO <> 0 AND ISNULL(SM_LOTCOMPLETED,0)=0 AND SM_YEARID = " & YearId & " AND (ISNULL(SM_DYEINGJOB,'')= '' OR ISNULL(SM_DYEINGJOB,'') = 'JOB')) AS T", "")
                Dim DT As New DataTable
                'If LOTSTATUSONMTRS = False Then
                '    DT = OBJCMN.SEARCH(" JONO ", "", " LOT_VIEW ", " AND JOBBERNAME ='" & CMBNAME.Text.Trim & "' AND BALPCS > 0 AND ISNULL(LOTCOMPLETED,0)=0 AND ISNULL(DYEINGJOB,'') = 'JOB' AND YEARID = " & YearId)
                'Else
                'DT = OBJCMN.SEARCH(" JONO ", "", " (SELECT BEAMJOBOUT.BJO_no AS JONO FROM BEAMJOBOUT INNER JOIN LEDGERS ON BEAMJOBOUT.BJO_ledgerid = LEDGERS.Acc_id WHERE LEDGERS.Acc_CMPNAME='" & CMBNAME.Text.Trim & "' AND ROUND((JOBOUT.JO_TOTALMTRS - JOBOUT.JO_RECDMTRS),2) > 0 AND JOBOUT.JO_CLOSE=0 AND ISNULL(JOBOUT.JO_LOTCOMPLETED,0)=0 AND JOBOUT.JO_YEARID = " & YearId & " UNION ALL SELECT DISTINCT SM_BILLNO AS JONO FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO= LEDGERS.Acc_id WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ROUND((SM_MTRS - SM_OUTMTRS),2) > 0 AND SM_BILLNO <> 0 AND ISNULL(SM_LOTCOMPLETED,0)=0 AND SM_YEARID = " & YearId & " AND (ISNULL(SM_DYEINGJOB,'')= '' OR ISNULL(SM_DYEINGJOB,'') = 'JOB')) AS T", "")
                DT = OBJCMN.SEARCH(" JONO ", "", " (SELECT DISTINCT BEAMJOBOUT.BJO_no AS JONO FROM BEAMJOBOUT INNER JOIN LEDGERS ON BEAMJOBOUT.BJO_ledgerid = LEDGERS.Acc_id INNER JOIN BEAMJOBOUT_DESC ON BEAMJOBOUT.BJO_NO = BEAMJOBOUT_DESC.BJO_NO AND BEAMJOBOUT.BJO_YEARID = BEAMJOBOUT_DESC.BJO_YEARID  WHERE LEDGERS.Acc_CMPNAME='" & CMBNAME.Text.Trim & "' AND ROUND((BEAMJOBOUT_DESC.BJO_BEAMWT - BEAMJOBOUT_DESC.BJO_OUTWT),2) > 0   AND BEAMJOBOUT.BJO_YEARID = " & YearId & " ) AS T", "")

                'End If
                If DT.Rows.Count > 0 Then
                    For Each DTROW As DataRow In DT.Rows
                        CMBJONO.Items.Add(DTROW("JONO"))
                    Next
                    CMBNAME.Enabled = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJONO_Validated(sender As Object, e As EventArgs) Handles CMBJONO.Validated
        Try
            If Val(CMBJONO.Text.Trim) = 0 Then Exit Sub

            GRIDBEAM.RowCount = 0

            Dim OBJCMN As New ClsCommon
            'Dim DT As DataTable = OBJCMN.SEARCH("BJO_GRIDSRNO AS GRIDSRNO, BJO_BEAMNO AS BEAMNO, BJO_BEAMNAME AS BEAMNAME, BJO_TOTALENDS AS GRIDTOTALENDS, BJO_TOTALMTRS AS GRIDTOTALMTRS, BJO_BEAMWT AS BEAMWT, BJO_GAMANO AS GAMANO, BJO_SECTION AS SECTION, BJO_BREAKAGE AS BREAKAGE, ISNULL(MILLMASTER.MILL_NAME, '') AS MILLNAME, BEAMJOBOUT_DESC.BJO_ROLLID AS ROLLNO,BEAMJOBOUT_DESC.BJO_REMARKS AS GRIDREMARKS , BEAMJOBOUT_DESC.BJO_NO AS JONO , BEAMJOBOUT_DESC.BJO_GRIDSRNO AS JOSRNO, 'BEAMJOBOUT' AS FROMTYPE", "", "BEAMJOBOUT_DESC LEFT OUTER JOIN MILLMASTER ON BEAMJOBOUT_DESC.BJO_MILLID = MILLMASTER.MILL_ID AND BEAMJOBOUT_DESC.BJO_YEARID = MILLMASTER.MILL_YEARID", " AND BJO_NO = " & Val(CMBJONO.Text.Trim) & " AND BJO_YEARID = " & YearId & " AND BJO_CMPID = " & CmpId)
            Dim DT As DataTable = OBJCMN.SEARCH("BJO_GRIDSRNO AS GRIDSRNO, BJO_BEAMNO AS BEAMNO, BJO_BEAMNAME AS BEAMNAME, BJO_TOTALENDS AS GRIDTOTALENDS, BJO_TOTALMTRS AS GRIDTOTALMTRS, (BJO_BEAMWT - BJO_OUTWT) AS BEAMWT, BJO_GAMANO AS GAMANO, BJO_SECTION AS SECTION, BJO_BREAKAGE AS BREAKAGE, ISNULL(MILLMASTER.MILL_NAME, '') AS MILLNAME, BEAMJOBOUT_DESC.BJO_ROLLID AS ROLLNO,BEAMJOBOUT_DESC.BJO_REMARKS AS GRIDREMARKS , BEAMJOBOUT_DESC.BJO_NO AS JONO , BEAMJOBOUT_DESC.BJO_GRIDSRNO AS JOSRNO, 'BEAMJOBOUT' AS FROMTYPE", "", "BEAMJOBOUT_DESC LEFT OUTER JOIN MILLMASTER ON BEAMJOBOUT_DESC.BJO_MILLID = MILLMASTER.MILL_ID AND BEAMJOBOUT_DESC.BJO_YEARID = MILLMASTER.MILL_YEARID", " AND BJO_NO = " & Val(CMBJONO.Text.Trim) & " AND BJO_YEARID = " & YearId & " AND BJO_CMPID = " & CmpId & " AND ROUND((BJO_BEAMWT - BJO_OUTWT),2) > 0")
            If DT.Rows.Count > 0 Then
                For Each DR As DataRow In DT.Rows
                    GRIDBEAM.Rows.Add(0, Val(DR("BEAMNO")), DR("BEAMNAME"), DR("MILLNAME"), Val(DR("GRIDTOTALENDS")), Format(Val(DR("GRIDTOTALMTRS")), "0.00"), Format(Val(DR("BEAMWT")), "0.00"), Val(DR("GAMANO")), Val(DR("SECTION")), Val(DR("ROLLNO")), Format(Val(DR("BREAKAGE")), "0.00"), DR("GRIDREMARKS").ToString, Val(DR("JONO")), Val(DR("JOSRNO")), DR("FROMTYPE"))
                Next
                getsrno(GRIDBEAM)
                TOTAL()

            Else
                MsgBox("No Details Found For This Job No", MsgBoxStyle.Information)
                CMBJONO.Text = ""
                CMBJONO.Focus()
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class