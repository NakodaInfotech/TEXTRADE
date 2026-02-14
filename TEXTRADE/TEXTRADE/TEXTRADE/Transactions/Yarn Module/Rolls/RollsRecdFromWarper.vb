Imports System.ComponentModel
Imports BL
Public Class RollsRecdFromWarper

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPROLLSRECDNO As Integer
    Dim TEMPMSG As Integer
    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub
    Sub TOTAL()
        Try
            LBLTOTALCONES.Text = 0
            LBLTOTALGROSSWT.Text = 0.0
            LBLTOTALNETTWT.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDROLLS.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALCONES.Text = Format(Val(LBLTOTALCONES.Text) + Val(ROW.Cells(GCONES.Index).EditedFormattedValue), "0")
                    LBLTOTALGROSSWT.Text = Format(Val(LBLTOTALGROSSWT.Text) + Val(ROW.Cells(GGROSSWT.Index).EditedFormattedValue), "0.000")
                    LBLTOTALNETTWT.Text = Format(Val(LBLTOTALNETTWT.Text) + Val(ROW.Cells(GNETTWT.Index).EditedFormattedValue), "0.000")
                End If
            Next
            TXTUSEDFRESH.Text = Val(LBLTOTALCONES.Text.Trim)
            TXTUSEDFRESHWT.Text = Val(LBLTOTALGROSSWT.Text.Trim)
            TXTUSEDFRESHNETT.Text = Val(LBLTOTALNETTWT.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()

        TXTROLLSRECDNO.Clear()
        ROLLSRECDDATE.Text = Mydate
        CMBNAME.Text = ""
        CMBOURGODOWN.Text = "" 'GETDEFAULTGODOWN()
        TXTCHALLANNO.Clear()
        DTCHALLANDATE.Text = Mydate
        TXTWARPINGNO.Clear()
        TXTPROGRAMSRNO.Clear()
        TXTPROGRAMNO.Clear()
        If ClientName = "SASHWINKUMAR" Then PROGRAMDATE.Clear()
        TXTENDS.Clear()
        TXTTOTALENDS.Clear()
        TXTCUT.Clear()
        TXTCOUNT.Clear()
        TXTLONGATION.Clear()
        TXTLENGTH.Clear()
        TXTTL.Clear()
        TXTROLLS.Clear()
        TXTWT.Clear()
        TXTBEAMCHALLANNO.Clear()
        TXTBEAMNAME.Clear()
        TXTBEAMFROM.Clear()
        TXTBEAMTO.Clear()
        TXTBEAMREMARKS.Clear()

        LBLTOTALCONES.Text = 0.0
        LBLTOTALGROSSWT.Text = 0.0
        LBLTOTALNETTWT.Text = 0.0

        TXTUSEDFRESH.Clear()
        TXTUSEDFRESHWT.Clear()
        TXTUSEDFRESHNETT.Clear()
        TXTUSEDWINDING.Clear()
        TXTUSEDWINDINGWT.Clear()
        TXTUSEDWINDINGNETT.Clear()
        TXTUSEDFIRKA.Clear()
        TXTUSEDFIRKAWT.Clear()
        TXTUSEDFIRKANETT.Clear()

        TXTRETFRESH.Clear()
        TXTRETFRESHWT.Clear()
        TXTRETFRESHNETT.Clear()
        TXTRETWINDING.Clear()
        TXTRETWINDINGWT.Clear()
        TXTRETWINDINGNETT.Clear()
        TXTRETFIRKA.Clear()
        TXTRETFIRKAWT.Clear()
        TXTRETFIRKANETT.Clear()

        TXTREMARKS.Clear()
        TXTSRNO.Clear()
        CMBQUALITY.Text = ""
        CMBMILL.Text = ""
        TXTLOTNO.Clear()
        TXTCONES.Clear()
        TXTGROSSWT.Clear()
        TXTNETTWT.Clear()

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False
        GRIDROLLS.RowCount = 0

        tstxtbillno.Clear()

        GETMAX_ROLLSRECD_NO()

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        TXTSRNO.Text = 1
        TXTUPLOADSRNO.Text = 1

        If ClientName = "SASHWINKUMAR" Then CMBWINDINGMILL.Text = "" Else CMBWINDINGMILL.Text = "WINDING"

    End Sub

    Sub GETMAX_ROLLSRECD_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(ROLLRECD_NO),0)+1", "ROLLRECEIVED", "AND ROLLRECD_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTROLLSRECDNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub
    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub RollsRecdFromWarper_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ROLLS RECD'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            CLEAR()
            fillGODOWN(CMBOURGODOWN, EDIT)

            If EDIT = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable
                Dim OBJROLLSREC As New ClsRollsRecdFromWarper

                OBJROLLSREC.alParaval.Add(TEMPROLLSRECDNO)
                OBJROLLSREC.alParaval.Add(YearId)
                dttable = OBJROLLSREC.SELECTROLLS()

                If dttable.Rows.Count > 0 Then

                    TXTROLLSRECDNO.Text = TEMPROLLSRECDNO
                    ROLLSRECDDATE.Text = dttable.Rows(0).Item("DATE")
                    CMBNAME.Text = dttable.Rows(0).Item("WARPER").ToString
                    CMBOURGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    TXTCHALLANNO.Text = dttable.Rows(0).Item("CHALLANNO").ToString
                    DTCHALLANDATE.Text = dttable.Rows(0).Item("CHALLANDATE")
                    TXTWARPINGNO.Text = dttable.Rows(0).Item("WARPINGNO")
                    TXTPROGRAMSRNO.Text = dttable.Rows(0).Item("PROGRAMSRNO")
                    TXTPROGRAMNO.Text = dttable.Rows(0).Item("PROGRAMNO")
                    PROGRAMDATE.Text = dttable.Rows(0).Item("PROGRAMDATE")
                    TXTENDS.Text = Val(dttable.Rows(0).Item("ENDS"))
                    TXTTOTALENDS.Text = Val(dttable.Rows(0).Item("TOTALENDS"))
                    TXTLENGTH.Text = Val(dttable.Rows(0).Item("LENGTH"))
                    TXTCUT.Text = Val(dttable.Rows(0).Item("CUT"))
                    TXTCOUNT.Text = Format(Val(dttable.Rows(0).Item("COUNT")), "0.00")
                    TXTLONGATION.Text = Format(Val(dttable.Rows(0).Item("LONGATION")), "0.00")
                    TXTTL.Text = Val(dttable.Rows(0).Item("TAPLINE"))

                    TXTROLLS.Text = Val(dttable.Rows(0).Item("TOTALROLLS"))
                    TXTWT.Text = Val(dttable.Rows(0).Item("TOTALWT"))

                    TXTOUTROLLS.Text = Val(dttable.Rows(0).Item("OUTROLLS"))
                    TXTOUTWT.Text = Val(dttable.Rows(0).Item("OUTWT"))

                    If Val(dttable.Rows(0).Item("OUTROLLS")) > 0 Or Val(dttable.Rows(0).Item("OUTWT")) > 0 Then
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If

                    TXTBEAMCHALLANNO.Text = dttable.Rows(0).Item("BEAMCHALLANNO")
                    TXTBEAMREMARKS.Text = dttable.Rows(0).Item("BEAMREMARKS")
                    TXTBEAMNAME.Text = dttable.Rows(0).Item("BEAMNAME")
                    TXTBEAMFROM.Text = dttable.Rows(0).Item("BEAMFROM")
                    TXTBEAMTO.Text = dttable.Rows(0).Item("BEAMTO")


                    TXTUSEDFRESH.Text = Val(dttable.Rows(0).Item("FRESH"))
                    TXTUSEDFRESHWT.Text = Val(dttable.Rows(0).Item("FRESHWT"))
                    TXTUSEDFRESHNETT.Text = Val(dttable.Rows(0).Item("FRESHNETT"))
                    TXTUSEDWINDING.Text = Val(dttable.Rows(0).Item("WINDING"))
                    TXTUSEDWINDINGWT.Text = Val(dttable.Rows(0).Item("WINDINGWT"))
                    TXTUSEDWINDINGNETT.Text = Val(dttable.Rows(0).Item("WINDINGNETT"))
                    TXTUSEDFIRKA.Text = Val(dttable.Rows(0).Item("FIRKA"))
                    TXTUSEDFIRKAWT.Text = Val(dttable.Rows(0).Item("FIRKAWT"))
                    TXTUSEDFIRKANETT.Text = Val(dttable.Rows(0).Item("FIRKANETT"))

                    TXTRETFRESH.Text = Val(dttable.Rows(0).Item("RETFRESH"))
                    TXTRETFRESHWT.Text = Val(dttable.Rows(0).Item("RETFRESHWT"))
                    TXTRETFRESHNETT.Text = Val(dttable.Rows(0).Item("RETFRESHNETT"))
                    TXTRETWINDING.Text = Val(dttable.Rows(0).Item("RETWINDING"))
                    TXTRETWINDINGWT.Text = Val(dttable.Rows(0).Item("RETWINDINGWT"))
                    TXTRETWINDINGNETT.Text = Val(dttable.Rows(0).Item("RETWINDINGNETT"))
                    TXTRETFIRKA.Text = Val(dttable.Rows(0).Item("RETFIRKA"))
                    TXTRETFIRKAWT.Text = Val(dttable.Rows(0).Item("RETFIRKAWT"))
                    TXTRETFIRKANETT.Text = Val(dttable.Rows(0).Item("RETFIRKANETT"))

                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString

                    CMBWINDINGMILL.Text = dttable.Rows(0).Item("WINDINGMILL").ToString


                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDROLLS.Rows.Add(Val(ROW("SRNO")), ROW("QUALITY"), ROW("MILLNAME"), ROW("LOTNO"), Val(ROW("CONES")), Format(Val(ROW("GROSSWT")), "0.000"), Format(Val(ROW("NETTWT")), "0.000"))
                    Next


                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ROLLRECEIVED_UPLOAD.ROLLRECD_SRNO AS GRIDSRNO, ROLLRECEIVED_UPLOAD.ROLLRECD_REMARKS AS REMARKS, ROLLRECEIVED_UPLOAD.ROLLRECD_NAME AS NAME, ROLLRECEIVED_UPLOAD.ROLLRECD_PHOTO AS IMGPATH ", "", " ROLLRECEIVED_UPLOAD ", " AND ROLLRECEIVED_UPLOAD.ROLLRECD_NO = " & TEMPROLLSRECDNO & " AND ROLLRECD_YEARID = " & YearId & " ORDER BY ROLLRECEIVED_UPLOAD.ROLLRECD_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If
                    TOTAL()
                    CMBNAME.Focus()
                End If
            Else
                CLEAR()
                EDIT = False
                ROLLSRECDDATE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVE_Click(sender As Object, e As EventArgs) Handles CMDSAVE.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(ROLLSRECDDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(DTCHALLANDATE.Text.Trim)
            alParaval.Add(Val(TXTWARPINGNO.Text.Trim))
            alParaval.Add(Val(TXTPROGRAMNO.Text.Trim))
            alParaval.Add(Val(TXTPROGRAMSRNO.Text.Trim))
            alParaval.Add(Format(Convert.ToDateTime(PROGRAMDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(Val(TXTENDS.Text.Trim))
            alParaval.Add(Val(TXTTOTALENDS.Text.Trim))
            alParaval.Add(Val(TXTLENGTH.Text.Trim))
            alParaval.Add(Val(TXTCUT.Text.Trim))
            alParaval.Add(Val(TXTCOUNT.Text.Trim))
            alParaval.Add(Val(TXTLONGATION.Text.Trim))
            alParaval.Add(Val(TXTTL.Text.Trim))

            alParaval.Add(Val(TXTROLLS.Text))
            alParaval.Add(Format(Val(TXTWT.Text), "0.000"))

            alParaval.Add(Val(LBLTOTALCONES.Text))
            alParaval.Add(Format(Val(LBLTOTALGROSSWT.Text.Trim), "0.000"))
            alParaval.Add(Format(Val(LBLTOTALNETTWT.Text.Trim), "0.000"))

            alParaval.Add(Val(TXTUSEDFRESH.Text.Trim))
            alParaval.Add(Val(TXTUSEDFRESHWT.Text.Trim))
            alParaval.Add(Val(TXTUSEDFRESHNETT.Text.Trim))
            alParaval.Add(Val(TXTUSEDWINDING.Text.Trim))
            alParaval.Add(Val(TXTUSEDWINDINGWT.Text.Trim))
            alParaval.Add(Val(TXTUSEDWINDINGNETT.Text.Trim))
            alParaval.Add(Val(TXTUSEDFIRKA.Text.Trim))
            alParaval.Add(Val(TXTUSEDFIRKAWT.Text.Trim))
            alParaval.Add(Val(TXTUSEDFIRKANETT.Text.Trim))

            alParaval.Add(Val(TXTRETFRESH.Text.Trim))
            alParaval.Add(Val(TXTRETFRESHWT.Text.Trim))
            alParaval.Add(Val(TXTRETFRESHNETT.Text.Trim))
            alParaval.Add(Val(TXTRETWINDING.Text.Trim))
            alParaval.Add(Val(TXTRETWINDINGWT.Text.Trim))
            alParaval.Add(Val(TXTRETWINDINGNETT.Text.Trim))
            alParaval.Add(Val(TXTRETFIRKA.Text.Trim))
            alParaval.Add(Val(TXTRETFIRKAWT.Text.Trim))
            alParaval.Add(Val(TXTRETFIRKANETT.Text.Trim))

            alParaval.Add(TXTREMARKS.Text.Trim)
            alParaval.Add(CMBWINDINGMILL.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim QUALITY As String = ""
            Dim MILLNAME As String = ""
            Dim LOTNO As String = ""
            Dim CONES As String = ""
            Dim GROSSWT As String = ""
            Dim NETTWT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDROLLS.Rows
                If row.Cells(gsrno.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = row.Cells(gsrno.Index).Value
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        LOTNO = Val(row.Cells(GLOTNO.Index).Value)
                        CONES = Val(row.Cells(GCONES.Index).Value)
                        GROSSWT = Format(Val(row.Cells(GGROSSWT.Index).Value), "0.000")
                        NETTWT = Format(Val(row.Cells(GNETTWT.Index).Value), "0.000")
                    Else
                        SRNO = SRNO & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        CONES = CONES & "|" & Val(row.Cells(GCONES.Index).Value)
                        GROSSWT = GROSSWT & "|" & Format(Val(row.Cells(GGROSSWT.Index).Value), "0.000")
                        NETTWT = NETTWT & "|" & row.Cells(GNETTWT.Index).Value.ToString
                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(QUALITY)
            alParaval.Add(MILLNAME)
            alParaval.Add(LOTNO)
            alParaval.Add(CONES)
            alParaval.Add(GROSSWT)
            alParaval.Add(NETTWT)


            Dim OBJROLLSREC As New ClsRollsRecdFromWarper
            OBJROLLSREC.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJROLLSREC.SAVE()
                TEMPROLLSRECDNO = DT.Rows(0).Item(0)
                TXTROLLSRECDNO.Text = TEMPROLLSRECDNO
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPROLLSRECDNO)
                IntResult = OBJROLLSREC.UPDATE()
                EDIT = False
                MsgBox("Details Updated")

            End If

            If lbllocked.Visible = False Then
                If MsgBox("Issue Rolls Directly to Sizer?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim OBJSIZER As New DirectIssueSizer
                    OBJSIZER.ShowDialog()
                    If OBJSIZER.cmbname.Text.Trim = "" Then GoTo LINE1
                    DIRECTISSUESIZER(OBJSIZER.cmbname.Text.Trim)
                End If
            End If

LINE1:
            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            'CLEAR()
            'Show NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            ROLLSRECDDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If ROLLSRECDDATE.Text = "__/__/____" Then
            EP.SetError(ROLLSRECDDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(ROLLSRECDDATE.Text) Then
                EP.SetError(ROLLSRECDDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If DTCHALLANDATE.Text = "__/__/____" Then
            EP.SetError(DTCHALLANDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTCHALLANDATE.Text) Then
                EP.SetError(DTCHALLANDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If ROLLSRECDDATE.Text.Trim <> "__/__/____" And DTCHALLANDATE.Text.Trim <> "__/__/____" Then
            If Convert.ToDateTime(ROLLSRECDDATE.Text).Date < Convert.ToDateTime(DTCHALLANDATE.Text).Date Then
                EP.SetError(DTCHALLANDATE, " Please Enter Proper Challan Date")
                bln = False
            End If
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Jobber Name")
            bln = False
        End If

        If CMBWINDINGMILL.Text.Trim.Length = 0 And ClientName <> "SASHWINKUMAR" And (Val(TXTUSEDWINDING.Text.Trim) > 0 Or Val(TXTUSEDFIRKA.Text.Trim) > 0 Or Val(TXTRETWINDING.Text.Trim) > 0 Or Val(TXTRETFIRKA.Text.Trim) > 0) Then
            EP.SetError(CMBWINDINGMILL, "Please Fill Mill Name")
            bln = False
        End If

        If Val(TXTROLLS.Text.Trim) = 0 Then
            EP.SetError(CMBNAME, "Please Enter Rolls")
            bln = False
        End If

        If Val(TXTENDS.Text.Trim) = 0 Then
            EP.SetError(TXTENDS, "Please Enter Ends")
            bln = False
        End If

        If Val(TXTLENGTH.Text.Trim) = 0 Then
            EP.SetError(TXTLENGTH, "Please Enter Length")
            bln = False
        End If

        If CMBOURGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBOURGODOWN, " Please Fill Godown ")
            bln = False
        End If

        If TXTPROGRAMNO.Text.Trim.Length = 0 And ClientName = "SASHWINKUMAR" Then
            EP.SetError(TXTPROGRAMNO, " Please Select Program")
            bln = False
        End If

        If GRIDROLLS.RowCount = 0 Then
            EP.SetError(TXTNETTWT, "Enter Proper Details")
            bln = False
        End If

        For Each row As DataGridViewRow In GRIDROLLS.Rows
            If Val(row.Cells(GGROSSWT.Index).Value) = 0 Then
                EP.SetError(TXTNETTWT, "Gross Wt Cannot be 0")
                bln = False
            End If

            If Val(row.Cells(GNETTWT.Index).Value) = 0 Then
                EP.SetError(TXTNETTWT, "Nett Wt Cannot be 0")
                bln = False
            End If
        Next


        'WARPER CAN USE EXTRA CONES, IF MATERIAL IS SHORT THEN HE WILL USE EXTRA CONES
        'If ClientName <> "JASHOK" Then
        '    If (Val(TXTUSEDFRESH.Text.Trim) + Val(TXTUSEDWINDING.Text.Trim) + Val(TXTUSEDFIRKA.Text.Trim)) <> Val(TXTENDS.Text.Trim) Then
        '        EP.SetError(TXTENDS, "Ends Does not Match with Used Details")
        '        bln = False
        '    End If
        'End If

        If (Val(TXTUSEDFRESH.Text.Trim) + Val(TXTUSEDWINDING.Text.Trim) + Val(TXTUSEDFIRKA.Text.Trim)) <> (Val(TXTRETFRESH.Text.Trim) + Val(TXTRETWINDING.Text.Trim) + Val(TXTRETFIRKA.Text.Trim)) Then
            EP.SetError(TXTENDS, "Used Details Does not Match with Return Details")
            bln = False
        End If


        If Format(((Val(TXTUSEDFRESHWT.Text.Trim) + Val(TXTUSEDWINDINGNETT.Text.Trim) + Val(TXTUSEDFIRKANETT.Text.Trim)) - (Val(TXTRETFRESHNETT.Text.Trim) + Val(TXTRETWINDINGNETT.Text.Trim) + Val(TXTRETFIRKANETT.Text.Trim))), "0.000") <> Format(Val(TXTWT.Text.Trim), "0.000") Then
            EP.SetError(TXTENDS, "Roll Wt Does not Match with Entered Details")
            bln = False
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, Entry Locked")
            bln = False
        End If

        Return bln
    End Function

    Sub CALCWT()
        Try
            TXTWT.Text = Format((Val(TXTUSEDFRESHWT.Text.Trim) + Val(TXTUSEDWINDINGNETT.Text.Trim) + Val(TXTUSEDFIRKANETT.Text.Trim)) - (Val(TXTRETFRESHNETT.Text.Trim) + Val(TXTRETWINDINGNETT.Text.Trim) + Val(TXTRETFIRKANETT.Text.Trim)), "0.000")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RollsRecdFromWarper_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNoCancel)
                    If tempmsg = vbCancel Then Exit Sub
                    If tempmsg = vbYes Then CMDSAVE_Click(sender, e)
                End If
                Me.Close()

            ElseIf e.Alt = True And e.KeyCode = Keys.D1 Then
                TabControl1.SelectedIndex = 0
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolprevious_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.F5 Then
                GRIDROLLS.Focus()
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub FILLCMB()
        If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' ")
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT)
        If CMBQUALITY.Text = "" Then fillYARNQUALITY(CMBQUALITY, EDIT)
        If CMBMILL.Text = "" Then FILLNAME(CMBMILL, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' ")
        If CMBWINDINGMILL.Text = "" Then FILLNAME(CMBWINDINGMILL, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
    End Sub

    Sub DIRECTISSUESIZER(ByVal SIZERNAME As String)
        Try
            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(Format(Convert.ToDateTime(ROLLSRECDDATE.Text.Trim).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBOURGODOWN.Text.Trim)
            ALPARAVAL.Add(SIZERNAME)
            ALPARAVAL.Add("")   'TRANSPORT
            ALPARAVAL.Add("")   'VEHICLE NO


            ALPARAVAL.Add(Val(TXTENDS.Text.Trim))  ''
            ALPARAVAL.Add(Val(TXTTOTALENDS.Text.Trim))  ''
            ALPARAVAL.Add(Val(TXTLENGTH.Text.Trim))  ''


            ALPARAVAL.Add(Val(TXTROLLS.Text.Trim))
            ALPARAVAL.Add(Val(TXTWT.Text.Trim))
            ALPARAVAL.Add("")   'REMARKS

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim ROWNO As Integer = 0
            For Each row As Windows.Forms.DataGridViewRow In GRIDROLLS.Rows
                'GET ONLY ONE RECORD WITH MAX CONES
                If row.Index > 0 Then
                    If Val(row.Cells(GCONES.Index).Value) > Val(GRIDROLLS.Rows(ROWNO).Cells(GCONES.Index).Value) Then ROWNO = row.Index
                End If
            Next

            ALPARAVAL.Add("1")
            ALPARAVAL.Add(GRIDROLLS.Rows(ROWNO).Cells(GQUALITY.Index).Value)
            ALPARAVAL.Add(GRIDROLLS.Rows(ROWNO).Cells(GMILLNAME.Index).Value)
            ALPARAVAL.Add(Val(TXTTOTALENDS.Text.Trim))
            ALPARAVAL.Add(Val(TXTROLLS.Text.Trim))
            ALPARAVAL.Add(Format(Val(TXTWT.Text.Trim), "0.000"))
            ALPARAVAL.Add("")   'NARRATION
            ALPARAVAL.Add(Val(TXTROLLSRECDNO.Text.Trim))
            ALPARAVAL.Add("1")
            ALPARAVAL.Add("ROLLRECD")

            Dim OBJROLLISSUE As New ClsRollIssueToSizer
            OBJROLLISSUE.alParaval = ALPARAVAL
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim DT As DataTable = OBJROLLISSUE.save()
            MsgBox("Rolls Issued To Sizer Added")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJROLLSREC As New ClsRollsRecdFromWarper
            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPROLLSRECDNO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJROLLSREC.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJROLLSREC.SAVEUPLOAD()
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
    Private Sub ROLLSRECDDATE_GotFocus(sender As Object, e As EventArgs) Handles ROLLSRECDDATE.GotFocus
        ROLLSRECDDATE.Select(0, 0)
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

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WARPER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then fillGODOWN(CMBOURGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBOURGODOWN.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'True'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBOURGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBOURGODOWN.Validating
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBOURGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDROLLS.RowCount = 0
LINE1:
            TEMPROLLSRECDNO = Val(TXTROLLSRECDNO.Text) - 1
Line2:
            If TEMPROLLSRECDNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ROLLRECD_NO ", "", "  ROLLRECEIVED", " AND ROLLRECD_NO = '" & TEMPROLLSRECDNO & "' AND ROLLRECEIVED.ROLLRECD_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    RollsRecdFromWarper_Load(sender, e)
                Else
                    TEMPROLLSRECDNO = Val(TEMPROLLSRECDNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDROLLS.RowCount = 0 And TEMPROLLSRECDNO > 1 Then
                TXTROLLSRECDNO.Text = TEMPROLLSRECDNO
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
            TEMPROLLSRECDNO = Val(TXTROLLSRECDNO.Text) + 1
            GETMAX_ROLLSRECD_NO()
            Dim MAXNO As Integer = TXTROLLSRECDNO.Text.Trim
            CLEAR()
            If Val(TXTROLLSRECDNO.Text) - 1 >= TEMPROLLSRECDNO Then
                EDIT = True
                RollsRecdFromWarper_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDROLLS.RowCount = 0 And TEMPROLLSRECDNO < MAXNO Then
                TXTROLLSRECDNO.Text = TEMPROLLSRECDNO
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
                GRIDROLLS.RowCount = 0
                TEMPROLLSRECDNO = Val(tstxtbillno.Text)
                If TEMPROLLSRECDNO > 0 Then
                    EDIT = True
                    RollsRecdFromWarper_Load(sender, e)
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
                CMBQUALITY.Text = GRIDROLLS.Item(GQUALITY.Index, e.RowIndex).Value
                CMBMILL.Text = GRIDROLLS.Item(GMILLNAME.Index, e.RowIndex).Value
                TXTLOTNO.Text = GRIDROLLS.Item(GLOTNO.Index, e.RowIndex).Value
                TXTCONES.Text = Val(GRIDROLLS.Item(GCONES.Index, e.RowIndex).Value)
                TXTGROSSWT.Text = Val(GRIDROLLS.Item(GGROSSWT.Index, e.RowIndex).Value)
                TXTNETTWT.Text = Val(GRIDROLLS.Item(GNETTWT.Index, e.RowIndex).Value)

                TEMPROW = e.RowIndex
                CMBQUALITY.Focus()

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
                TEMPMSG = MsgBox("Delete Entry?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPROLLSRECDNO)
                    alParaval.Add(YearId)

                    Dim CLSRECD As New ClsRollsRecdFromWarper
                    CLSRECD.alParaval = alParaval
                    IntResult = CLSRECD.Delete()

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

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDROLLS.Rows.Add(Val(TXTSRNO.Text.Trim), CMBQUALITY.Text.Trim, CMBMILL.Text.Trim, TXTLOTNO.Text.Trim, Val(TXTCONES.Text.Trim), Format(Val(TXTGROSSWT.Text.Trim), "0.000"), Format(Val(TXTNETTWT.Text.Trim), "0.000"))
            Else
                GRIDROLLS.Item(gsrno.Index, TEMPROW).Value = TXTSRNO.Text.Trim
                GRIDROLLS.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
                GRIDROLLS.Item(GMILLNAME.Index, TEMPROW).Value = CMBMILL.Text.Trim
                GRIDROLLS.Item(GLOTNO.Index, TEMPROW).Value = TXTLOTNO.Text.Trim
                GRIDROLLS.Item(GCONES.Index, TEMPROW).Value = Val(TXTCONES.Text.Trim)
                GRIDROLLS.Item(GGROSSWT.Index, TEMPROW).Value = Format(Val(TXTGROSSWT.Text.Trim), "0.000")
                GRIDROLLS.Item(GNETTWT.Index, TEMPROW).Value = Format(Val(TXTNETTWT.Text.Trim), "0.000")

                GRIDDOUBLECLICK = False
            End If
            TXTSRNO.Clear()
            CMBQUALITY.Text = ""
            CMBMILL.Text = ""
            TXTLOTNO.Clear()
            TXTCONES.Clear()
            TXTGROSSWT.Clear()
            TXTNETTWT.Clear()
            getsrno(GRIDROLLS)
            TOTAL()
            CMBQUALITY.Focus()
            If GRIDROLLS.RowCount > 0 Then TXTSRNO.Text = Val(GRIDROLLS.RowCount) + 1 Else TXTSRNO.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNETTWT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNETTWT.Validating
        If Val(TXTPROGRAMNO.Text.Trim) = 0 And ClientName = "SASHWINKUMAR" Then
            MsgBox("Select Program First", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If CMBQUALITY.Text.Trim <> "" And CMBMILL.Text.Trim <> "" And Val(TXTNETTWT.Text.Trim) > 0 And Val(TXTGROSSWT.Text.Trim) > 0 And Val(TXTCONES.Text.Trim) > 0 Then FILLGRID() Else MsgBox("Please Enter proper details")
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJROLLS As New SelectQuality
                OBJROLLS.ShowDialog()
                If OBJROLLS.TEMPNAME <> "" Then CMBQUALITY.Text = OBJROLLS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            'Dim OBJROLLS As New RollsRecdDetails
            'OBJROLLS.MdiParent = MDIMain
            'OBJROLLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTROLLS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTROLLS.KeyPress
        numkeypress(e, TXTROLLS, Me)
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTNETTWT.KeyPress
        numdot3(e, TXTROLLS, Me)
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then FILLNAME(CMBMILL, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBMILL.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILL.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then NAMEVALIDATE(CMBMILL, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' ", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "MILL")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTCHALLANDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTCHALLANDATE.GotFocus
        DTCHALLANDATE.Select(0, 0)
    End Sub

    Private Sub CMDSELECTPROGRAM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If CMBNAME.Text.Trim = "" Then
                MsgBox("Select Warper First", MsgBoxStyle.Critical)
                CMBNAME.Focus()
                Exit Sub
            End If

            Dim OBJPROG As New SelectYarnProgram
            Dim DT As DataTable = OBJPROG.DT
            OBJPROG.WARPERNAME = CMBNAME.Text.Trim
            OBJPROG.ShowDialog()
            If DT.Rows.Count > 0 Then
                TXTPROGRAMNO.Text = Val(DT.Rows(0).Item("PROGRAMNO"))
                TXTPROGRAMSRNO.Text = Val(DT.Rows(0).Item("PROGRAMSRNO"))
                PROGRAMDATE.Text = DT.Rows(0).Item("PROGRAMDATE")
                TXTENDS.Text = Val(DT.Rows(0).Item("ENDS"))
                TXTTOTALENDS.Text = Val(DT.Rows(0).Item("TOTALENDS"))
                TXTLENGTH.Text = Val(DT.Rows(0).Item("LENGTH"))
                CMBQUALITY.Text = DT.Rows(0).Item("QUALITY")
                CMBMILL.Text = DT.Rows(0).Item("MILLNAME")
                TXTROLLS.Text = Val(DT.Rows(0).Item("ROLLS"))
                TXTLOTNO.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ROLLSRECDDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ROLLSRECDDATE.Validating
        Try
            If ROLLSRECDDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(ROLLSRECDDATE.Text, TEMP) Then
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

    Private Sub TXTUSEDFRESH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTUSEDWINDING.KeyPress, TXTUSEDFRESH.KeyPress, TXTUSEDFIRKA.KeyPress, TXTRETWINDING.KeyPress, TXTRETFRESH.KeyPress, TXTRETFIRKA.KeyPress, TXTWARPINGNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTBALFRESHWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTUSEDWINDINGWT.KeyPress, TXTUSEDFRESHWT.KeyPress, TXTUSEDFIRKAWT.KeyPress, TXTRETWINDINGWT.KeyPress, TXTRETFRESHWT.KeyPress, TXTRETFIRKAWT.KeyPress, TXTUSEDFRESHNETT.KeyPress, TXTUSEDWINDINGNETT.KeyPress, TXTUSEDFIRKANETT.KeyPress, TXTRETFRESHNETT.KeyPress, TXTRETWINDINGNETT.KeyPress, TXTRETFIRKANETT.KeyPress, TXTGROSSWT.KeyPress, TXTNETTWT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTUSEDFRESHWT_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTUSEDWINDINGWT.Validated, TXTUSEDFRESHWT.Validated, TXTUSEDFIRKAWT.Validated, TXTRETWINDINGWT.Validated, TXTRETFRESHWT.Validated, TXTRETFIRKAWT.Validated, TXTUSEDFRESHNETT.Validated, TXTUSEDWINDINGNETT.Validated, TXTUSEDFIRKANETT.Validated, TXTRETFRESHNETT.Validated, TXTRETWINDINGNETT.Validated, TXTRETFIRKANETT.Validated
        CALCWT()
    End Sub



    Private Sub RollsRecdFromWarper_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'If ALLOWMFG = False Then Exit Sub

            If ClientName <> "SASHWINKUMAR" Then
                TXTROLLS.ReadOnly = False
                TXTROLLS.TabStop = True
                TXTROLLS.BackColor = Color.LemonChiffon

                TXTTOTALENDS.ReadOnly = False
                TXTTOTALENDS.TabStop = True
                TXTTOTALENDS.BackColor = Color.LemonChiffon

                TXTLENGTH.ReadOnly = False
                TXTLENGTH.TabStop = True
                TXTLENGTH.BackColor = Color.LemonChiffon

                PROGRAMDATE.Text = Mydate

                LBLWINDINGMILL.Visible = True
                CMBWINDINGMILL.Visible = True


                TXTUSEDFIRKA.TabStop = False
                TXTUSEDFIRKAWT.TabStop = False
                TXTUSEDFIRKANETT.TabStop = False

                TXTRETFRESH.TabStop = False
                TXTRETFRESHWT.TabStop = False
                TXTRETFRESHNETT.TabStop = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CALC()
        If ClientName <> "SASHWINKUMAR" Then TXTENDS.Text = Format(Val(TXTTOTALENDS.Text.Trim) / Val(TXTROLLS.Text.Trim))
    End Sub

    Private Sub TXTTOTALENDS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTTOTALENDS.Validating, TXTROLLS.Validating
        CALC()
    End Sub

    Private Sub TXTTOTALENDS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTOTALENDS.KeyPress, TXTROLLS.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTLENGTH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTLENGTH.KeyPress
        numkeypress(e, TXTLENGTH, Me)
    End Sub
End Class