Imports System.ComponentModel
Imports BL
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils.CommonDialogs

Public Class OpeningBeamStockAtJobber
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW, PURREGID As Integer
    Public EDIT As Boolean
    Public TEMPBEAMISSUENO As Integer
    Dim TEMPMSG As Integer
    Dim DTWHATSAPP As New DataTable
    Dim NextBeamNo As Integer
    Dim MAXNO As Integer = 0


    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        'CMBOURGODOWN.Focus()
    End Sub

    Sub CLEAR()
        LBLWHATSAPP.Visible = False
        TXTISSUENO.Clear()
        DTISSUEDATE.Text = Mydate
        cmbname.Text = ""

        TXTVEHICALNO.Clear()
        TXTEWBNO.Clear()
        TXTREMARKS.Clear()

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        TXTREMARKS.Clear()

        CMBBEAMNAME.Text = ""
        GRIDBEAMISSUE.RowCount = 0

        GETMAX_BEAMISSUE_NO()
        LBLTOTALCUT.Text = 0.0
        LBLTOTALWT.Text = 0.0
        GetLastBeamNo()


        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False
        CMDSELECTBEAMSTOCK.Enabled = True

        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()

        'If CMBROLLNO.Text = "" Then
        '    Dim strUsedRolls As String = ""
        '    For Each ROW As DataGridViewRow In GRIDBEAMISSUE.Rows
        '        If ROW.IsNewRow Then Continue For
        '        If GRIDDOUBLECLICK = True And ROW.Index = TEMPROW Then Continue For
        '        Dim cellVal As String = If(ROW.Cells(GROLLNO.Index).Value IsNot Nothing, ROW.Cells(GROLLNO.Index).Value.ToString.Trim, "")
        '        If cellVal <> "" Then strUsedRolls = strUsedRolls & "'" & cellVal & "',"
        '    Next
        '    If strUsedRolls <> "" Then strUsedRolls = " AND ITEMNAME NOT IN (" & strUsedRolls.TrimEnd(",") & ") "
        '    fillROLLITEM(CMBROLLNO, EDIT, "AND ROLLITEM = 1 " & strUsedRolls, "HAVING SUM(QTY - ISSQTY) >0")
        'End If

        gridupload.RowCount = 0

        If gridupload.RowCount > 0 Then
            TXTUPLOADSRNO.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1
        Else
            TXTUPLOADSRNO.Text = 1
        End If
    End Sub
    Sub TOTAL()
        Try
            LBLTOTALWT.Text = 0.0
            LBLTOTALCUT.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDBEAMISSUE.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALCUT.Text = Format(Val(LBLTOTALCUT.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub GETMAX_BEAMISSUE_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(OPBEAM_NO),0)+1", "OPENINGBEAMSTOCKATJOBBER", "AND OPBEAM_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            TXTISSUENO.Text = DTTABLE.Rows(0).Item(0)
        End If


    End Sub
    Public Sub GETMAXSERIES(ByVal TXTSERIES As TextBox)
        Try
            Dim DTTABLE As DataTable = getmax(" ISNULL(MAX(SERIES),0) + 1 ", " ( SELECT MAX(ROLLISSUE_SERIES) AS SERIES, ROLLISSUE_yearid AS YEARID FROM ROLLISSUE GROUP BY ROLLISSUE_yearid  UNION ALL  SELECT MAX(OPBEAM_SERIES) AS SERIES, OPBEAM_yearid AS YEARID  FROM OPENINGBEAMSTOCKATJOBBER GROUP BY OPBEAM_yearid ) AS T", " AND T.YEARID = " & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTSERIES.Text = DTTABLE.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamIssueWeaver_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown



        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNoCancel)
                    If tempmsg = vbCancel Then Exit Sub
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
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
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub FILLCMB()
        If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        fillBEAM(CMBBEAMNAME, False)
        If CMBSIZER.Text.Trim = "" Then FILLNAME(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
    End Sub

    Private Sub BeamIssueWeaver_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'BEAM ISSUE'")
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
                Dim OBJBEAMISSUE As New ClsOpeningBeamStockAtJobber

                OBJBEAMISSUE.alParaval.Add(TEMPBEAMISSUENO)
                OBJBEAMISSUE.alParaval.Add(YearId)
                dttable = OBJBEAMISSUE.selectBEAMISSUE()

                If dttable.Rows.Count > 0 Then
                    cmbname.Focus()

                    TXTISSUENO.Text = TEMPBEAMISSUENO
                    DTISSUEDATE.Text = dttable.Rows(0).Item("ISSUEDATE")
                    cmbname.Text = dttable.Rows(0).Item("NAME").ToString
                    TXTVEHICALNO.Text = dttable.Rows(0).Item("VEHICALNO").ToString
                    TXTEWBNO.Text = dttable.Rows(0).Item("EWBNO").ToString
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString
                    'If Convert.ToBoolean(dttable.Rows(0).Item("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True
                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        'GRIDBEAMISSUE.Rows.Add(Val(ROW("SRNO")), ROW("BEAMNAME"), ROW("BEAMNO"), Val(ROW("ENDS")), Val(ROW("TAPLINE")), Format(Val(ROW("MTRS")), "0.00"), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTCUT")), "0.000"), ROW("NARR"), Val(ROW("FROMNO")), Val(ROW("FROMSRNO")), ROW("TYPE"), ROW("OUTMTRS"), ROW("DONE"), ROW("SIZERNAME"), ROW("LOOMNO"), ROW("UPLOADDATE"))
                        GRIDBEAMISSUE.Rows.Add(Val(ROW("SRNO")), ROW("BEAMNAME"), ROW("BEAMNO"), Format(Val(ROW("GAMANO")), "0.00"), Format(Val(ROW("SECTION")), "0.00"), ROW("ROLLNO"), Format(Val(ROW("BEAMWT")), "0.00"), Format(Val(ROW("BREAKAGE")), "0.00"), Val(ROW("ENDS")), Val(ROW("TAPLINE")), Format(Val(ROW("MTRS")), "0.00"), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("CUTWT")), "0.000"), ROW("NARR"), ROW("SIZER"), ROW("OUTMTRS"), ROW("DONE"))

                        If Val(ROW("OUTMTRS")) > 0 Or Convert.ToBoolean(ROW("DONE")) = True Then
                            GRIDBEAMISSUE.Rows(GRIDBEAMISSUE.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        'ALLOW USER TO ADD NEW BEAMS ALSO
                        'If ROW("BEAMNAME") <> "" Then CMDSELECTBEAMSTOCK.Enabled = False
                    Next

                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" OPENINGBEAMSTOCKATJOBBER_UPLOAD.OPBEAM_SRNO AS GRIDSRNO, OPENINGBEAMSTOCKATJOBBER_UPLOAD.OPBEAM_REMARKS AS REMARKS, OPENINGBEAMSTOCKATJOBBER_UPLOAD.OPBEAM_NAME AS NAME, OPENINGBEAMSTOCKATJOBBER_UPLOAD.OPBEAM_PHOTO AS IMGPATH ", "", " OPENINGBEAMSTOCKATJOBBER_UPLOAD ", " AND OPENINGBEAMSTOCKATJOBBER_UPLOAD.OPBEAM_NO = " & TEMPBEAMISSUENO & " AND OPBEAM_YEARID = " & YearId & " ORDER BY OPENINGBEAMSTOCKATJOBBER_UPLOAD.OPBEAM_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If


                    TOTAL()
                End If
            End If
            GetLastBeamNo()
            fillROLLITEM(CMBROLLNO, EDIT, "AND ROLLITEM = 1 ", "HAVING SUM(QTY - ISSQTY) >0")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTISSUEDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(TXTVEHICALNO.Text.Trim)
            alParaval.Add(TXTEWBNO.Text.Trim)
            alParaval.Add(TXTREMARKS.Text.Trim)
            alParaval.Add(LBLTOTALCUT.Text.Trim)
            alParaval.Add(LBLTOTALWT.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim SRNO As String = ""
            Dim BEAMNAME As String = ""
            Dim BEAMNO As String = ""
            Dim GAMANO As String = ""
            Dim SECTION As String = ""
            Dim ROLLNO As String = ""
            Dim BEAMWT As String = ""
            Dim BREAKAGE As String = ""
            Dim ENDS As String = ""
            Dim TL As String = ""
            Dim CUT As String = ""
            Dim WT As String = ""
            Dim CUTWT As String = ""
            Dim NARR As String = ""
            Dim OUTMTRS As String = ""
            Dim GRIDDONE As String = ""
            Dim SIZERNAME As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDBEAMISSUE.Rows
                If row.Cells(gsrno.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = Val(row.Cells(gsrno.Index).Value)
                        BEAMNAME = row.Cells(GBEAMNAME.Index).Value.ToString
                        BEAMNO = row.Cells(GBEAMNO.Index).Value.ToString
                        GAMANO = Format(Val(row.Cells(GGAMANO.Index).Value), "0.00")
                        SECTION = Format(Val(row.Cells(GSECTION.Index).Value), "0.00")
                        ROLLNO = row.Cells(GROLLNO.Index).Value.ToString
                        BEAMWT = Format(Val(row.Cells(GBEAMWT.Index).Value), "0.00")
                        BREAKAGE = Format(Val(row.Cells(GBREAKAGE.Index).Value), "0.00")

                        ENDS = Val(row.Cells(GENDS.Index).Value)
                        TL = Val(row.Cells(GTAPLINE.Index).Value)
                        CUT = Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        WT = Format(Val(row.Cells(GWT.Index).Value), "0.00")
                        CUTWT = Format(Val(row.Cells(GCUTWT.Index).Value), "0.00")
                        If row.Cells(GNARR.Index).Value = Nothing Then NARR = "" Else NARR = row.Cells(GNARR.Index).Value.ToString
                        SIZERNAME = row.Cells(GSIZERNAME.Index).Value
                        OUTMTRS = Val(row.Cells(GOUTMTRS.Index).Value)
                        If row.Cells(GDONE.Index).Value = True Then
                            GRIDDONE = 1
                        Else
                            GRIDDONE = 0
                        End If


                    Else

                        SRNO = SRNO & "|" & row.Cells(gsrno.Index).Value
                        BEAMNAME = BEAMNAME & "|" & row.Cells(GBEAMNAME.Index).Value.ToString
                        BEAMNO = BEAMNO & "|" & row.Cells(GBEAMNO.Index).Value.ToString
                        GAMANO = GAMANO & "|" & Format(Val(row.Cells(GGAMANO.Index).Value), "0.00")
                        SECTION = SECTION & "|" & Format(Val(row.Cells(GSECTION.Index).Value), "0.00")
                        ROLLNO = ROLLNO & "|" & row.Cells(GROLLNO.Index).Value.ToString
                        BEAMWT = BEAMWT & "|" & Format(Val(row.Cells(GBEAMWT.Index).Value), "0.00")
                        BREAKAGE = BREAKAGE & "|" & Format(Val(row.Cells(GBREAKAGE.Index).Value), "0.00")

                        ENDS = ENDS & "|" & Val(row.Cells(GENDS.Index).Value)
                        TL = TL & "|" & Val(row.Cells(GTAPLINE.Index).Value)
                        CUT = CUT & "|" & Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        WT = WT & "|" & Format(Val(row.Cells(GWT.Index).Value), "0.000")
                        CUTWT = CUTWT & "|" & Format(Val(row.Cells(GCUTWT.Index).Value), "0.000")
                        If row.Cells(GNARR.Index).Value = Nothing Then NARR = NARR & "|" & "" Else NARR = NARR & "|" & row.Cells(GNARR.Index).Value
                        SIZERNAME = SIZERNAME & "|" & row.Cells(GSIZERNAME.Index).Value
                        OUTMTRS = OUTMTRS & "|" & Val(row.Cells(GOUTMTRS.Index).Value)
                        If row.Cells(GDONE.Index).Value = True Then
                            GRIDDONE = GRIDDONE & "|" & "1"
                        Else
                            GRIDDONE = GRIDDONE & "|" & "0"
                        End If

                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(BEAMNAME)
            alParaval.Add(BEAMNO)
            alParaval.Add(GAMANO)
            alParaval.Add(SECTION)
            alParaval.Add(ROLLNO)
            alParaval.Add(BEAMWT)
            alParaval.Add(BREAKAGE)
            alParaval.Add(ENDS)
            alParaval.Add(TL)
            alParaval.Add(CUT)
            alParaval.Add(WT)
            alParaval.Add(CUTWT)
            alParaval.Add(NARR)

            alParaval.Add(OUTMTRS)
            alParaval.Add(GRIDDONE)
            alParaval.Add(SIZERNAME)



            Dim OBJBEAMISSUE As New ClsOpeningBeamStockAtJobber
            OBJBEAMISSUE.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJBEAMISSUE.SAVE()
                TEMPBEAMISSUENO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPBEAMISSUENO)
                IntResult = OBJBEAMISSUE.UPDATE()
                EDIT = False
                MsgBox("Details Updated")

            End If

            PRINTREPORT()
            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTISSUEDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub SAVEUPLOAD()

        Try
            Dim OBJBEAMISSUE As New ClsOpeningBeamStockAtJobber


            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPBEAMISSUENO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJBEAMISSUE.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJBEAMISSUE.SAVEUPLOAD()
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

        If DTISSUEDATE.Text = "__/__/____" Then
            EP.SetError(DTISSUEDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTISSUEDATE.Text) Then
                EP.SetError(DTISSUEDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(cmbname, "Please Fill Jobber Name")
            bln = False
        End If

        'NOT MANDATORY COZ WE CAN GET BEAMS FROM SIZER ALSO
        'IN THAT CASE GODOWN IS NOT MANDATORY
        'If CMBOURGODOWN.Text.Trim.Length = 0 Then
        '    EP.SetError(CMBOURGODOWN, " Please Fill Our Godown Name ")
        '    bln = False
        'End If



        'DONE TEMPORARILY
        'If lbllocked.Visible = True Then
        '    EP.SetError(lbllocked, "Unable to Modify, entry Locked")
        '    bln = False
        'End If

        'If GRIDSCHEDULE.RowCount = 0 And ClientName = "SASHWINKUMAR" Then
        '    EP.SetError(cmbname, "Please Fill Schedule Details")
        '    bln = False
        'End If

        Return bln
    End Function

    Private Sub DTISSUEDATE_GotFocus(sender As Object, e As EventArgs) Handles DTISSUEDATE.GotFocus
        DTISSUEDATE.Select(0, 0)

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
    Private Sub cmbname_Enter(sender As Object, e As EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(sender As Object, e As CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then NAMEVALIDATE(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' ", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            GRIDBEAMISSUE.RowCount = 0
LINE1:
            TEMPBEAMISSUENO = Val(TXTISSUENO.Text) - 1
Line2:
            If TEMPBEAMISSUENO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" OPBEAM_NO ", "", "  OPENINGBEAMSTOCKATJOBBER", " AND OPBEAM_NO = '" & TEMPBEAMISSUENO & "' AND OPENINGBEAMSTOCKATJOBBER.OPBEAM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    BeamIssueWeaver_Load(sender, e)
                Else
                    TEMPBEAMISSUENO = Val(TEMPBEAMISSUENO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If cmbname.Text = "" And TEMPBEAMISSUENO > 1 Then
                TXTISSUENO.Text = TEMPBEAMISSUENO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            GRIDBEAMISSUE.RowCount = 0
LINE1:
            TEMPBEAMISSUENO = Val(TXTISSUENO.Text) + 1
            GETMAX_BEAMISSUE_NO()
            Dim MAXNO As Integer = TXTISSUENO.Text.Trim
            CLEAR()
            If Val(TXTISSUENO.Text) - 1 >= TEMPBEAMISSUENO Then
                EDIT = True
                BeamIssueWeaver_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDBEAMISSUE.RowCount = 0 And TEMPBEAMISSUENO < MAXNO Then
                TXTISSUENO.Text = TEMPBEAMISSUENO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstxtbillno.KeyPress
        numkeypress(e, tstxtbillno, Me)

    End Sub

    Private Sub tstxtbillno_Validated(sender As Object, e As EventArgs) Handles tstxtbillno.Validated
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDBEAMISSUE.RowCount = 0
                TEMPBEAMISSUENO = Val(tstxtbillno.Text)
                If TEMPBEAMISSUENO > 0 Then
                    EDIT = True
                    BeamIssueWeaver_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridupload_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
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

    Private Sub gridupload_KeyDown(sender As Object, e As KeyEventArgs) Handles gridupload.KeyDown
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

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
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
                    alParaval.Add(TXTISSUENO.Text.Trim)
                    alParaval.Add(YearId)

                    Dim OBJDEL As New ClsOpeningBeamStockAtJobber
                    OBJDEL.alParaval = alParaval
                    IntResult = OBJDEL.Delete()
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

    'Private Sub CMDSELECTBEAMSTOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTBEAMSTOCK.Click
    '    Try

    '        If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
    '            MsgBox("Insufficient Rights")
    '            Exit Sub
    '        End If

    '        If GRIDSCHEDULE.RowCount = 0 And ClientName = "SASHWINKUMAR" Then
    '            MsgBox("First Schedule Beams", MsgBoxStyle.Critical)
    '            Exit Sub
    '        End If


    '        'IT IS NOT MANDATE TO SELECT GODOWN HERE,
    '        'IF USER SELECTS GODOWN THEN WE WILL ADD THAT IN WHERE CLAUE OR ELSE SHOW ALL BEAMS WHICH ARE PRESENT WITH SIZER OR INHOUSE BOTH


    '        'SHOW ONLY THOSE BEAMS IN STOCK WHICH WE HAVE SELECTED IN SCHEDULING
    '        Dim WHERECLAUSE As String = ""
    '        For Each ROW As DataGridViewRow In GRIDSCHEDULE.Rows
    '            If WHERECLAUSE = "" Then
    '                WHERECLAUSE = " AND BEAMNAME IN ('" & ROW.Cells(GBEAMNAME.Index).Value & "'"
    '            Else
    '                WHERECLAUSE = WHERECLAUSE & ",'" & ROW.Cells(GBEAMNAME.Index).Value & "'"
    '            End If
    '        Next
    '        If WHERECLAUSE <> "" Then WHERECLAUSE = WHERECLAUSE & ")"
    '        WHERECLAUSE = WHERECLAUSE & " AND DATE <= '" & Format(Convert.ToDateTime(DTISSUEDATE.Text).Date, "MM/dd/yyyy") & "'"


    '        Dim OBJSELECTSTOCK As New SelectBeamStock
    '        OBJSELECTSTOCK.TEMPGODOWNNAME = CMBOURGODOWN.Text.Trim
    '        Dim DTBEAMSTOCK As DataTable = OBJSELECTSTOCK.DT
    '        OBJSELECTSTOCK.WHERECLAUSE = WHERECLAUSE
    '        OBJSELECTSTOCK.ALLOWEDBEAMS = GRIDSCHEDULE.RowCount
    '        OBJSELECTSTOCK.ShowDialog()
    '        If DTBEAMSTOCK.Rows.Count > 0 Then

    '            'CHECK IF 1ST BEAM HAS 0 IN SRNO THEN CLEAR THE GRID
    '            'NEED TO CHECK WHETHER ANY ROW IS PRESENT OR NOT ELSE IT GIVES ERROR
    '            If GRIDBEAMISSUE.RowCount <> 0 Then
    '                If Val(GRIDBEAMISSUE.Rows(0).Cells(gsrno.Index).Value) = 0 Then GRIDBEAMISSUE.RowCount = 0
    '            End If

    '            For Each ROW As DataRow In DTBEAMSTOCK.Rows
    '                GRIDBEAMISSUE.Rows.Add(0, ROW("BEAMNAME"), ROW("BEAMNO"), Val(ROW("ENDS")), Val(ROW("TAPLINE")), Format(Val(ROW("CUT")), "0.00"), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTCUT")), "0.000"), "", Val(ROW("FROMNO")), Val(ROW("FROMSRNO")), ROW("TYPE"), 0, 0, ROW("SIZERNAME"), 0, "")
    '            Next
    '            TOTAL()
    '            getsrno(GRIDBEAMISSUE)
    '            CMDSELECTBEAMSTOCK.Enabled = False
    '        End If

    '        TOTAL()

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub GRIDBEAMISSUE_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDBEAMISSUE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDBEAMISSUE.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDBEAMISSUE.Rows.RemoveAt(GRIDBEAMISSUE.CurrentRow.Index)
                getsrno(GRIDBEAMISSUE)
                TOTAL()

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    'Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
    '    Try
    '        Dim OBJBEAM As New BeamIssueDetails
    '        OBJBEAM.MdiParent = MDIMain
    '        OBJBEAM.Show()
    '    Catch EX As Exception
    '        Throw EX
    '    End Try
    'End Sub

    Private Sub DTISSUEDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTISSUEDATE.Validating
        Try
            If DTISSUEDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTISSUEDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then fillBEAM(CMBBEAMNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then BEAMVALIDATE(CMBBEAMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOOMNO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        numkeypress(e, sender, Me)
    End Sub


    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Dim DT As New DataTable
        Dim OBJCMN As New ClsCommon
        If EDIT = True Then SENDWHATSAPP(TEMPBEAMISSUENO)
        DT = OBJCMN.Execute_Any_String("UPDATE OPENINGBEAMSTOCKATJOBBER SET OPBEAM_SENDWHATSAPP = 1 WHERE OPBEAM_NO = " & TEMPBEAMISSUENO & " AND OPBEAM_YEARID = " & YearId, "", "")
        LBLWHATSAPP.Visible = True
    End Sub
    Async Sub SENDWHATSAPP(BEAMISSUEBEAMNO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJCN As New BeamIssueWeaverDesign
            OBJCN.MdiParent = MDIMain
            OBJCN.FRMSTRING = "BEAMISSUE"
            OBJCN.DIRECTMAIL = False
            OBJCN.DIRECTPRINT = True
            OBJCN.DIRECTWHATSAPP = True
            OBJCN.PARTYNAME = cmbname.Text.Trim
            OBJCN.BEAMISSUEBEAMNO = Val(BEAMISSUEBEAMNO)
            OBJCN.NOOFCOPIES = 1
            OBJCN.Show()
            OBJCN.Close()


            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = cmbname.Text.Trim
            'OBJWHATSAPP.AGENTNAME = cmbtrans.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & cmbname.Text.Trim & "_BEAM_" & Val(BEAMISSUEBEAMNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add(cmbname.Text.Trim & "BEAM_" & Val(BEAMISSUEBEAMNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub CMBLOOMNO_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If cmbname.Text.Trim <> "" And CMBLOOMNO.Text.Trim = "" Then FILLLOOM(CMBLOOMNO, cmbname.Text.Trim, EDIT)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub CMBLOOMNO_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If CMBBEAMNAME.Text.Trim <> "" And Val(CMBLOOMNO.Text.Trim) > 0 Then FILLGRID()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub CMBLOOMNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
    '    Try
    '        If cmbname.Text.Trim <> "" And CMBLOOMNO.Text.Trim <> "" Then LOOMVALIDATE(CMBLOOMNO, cmbname.Text.Trim, e, Me)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Sub PRINTREPORT()
        'Try
        '    If MsgBox("Wish To Print Report?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        '    Dim OBJYARNISSUE As New BeamIssueDesign
        '    OBJYARNISSUE.MdiParent = MDIMain
        '    If Val(LBLTOTALCUT.Text) > 0 Then OBJYARNISSUE.FRMSTRING = "BEAMISSUEBEAMNO" Else OBJYARNISSUE.FRMSTRING = "BEAMISSUE"
        '    OBJYARNISSUE.WHERECLAUSE = "{BEAMISSUEWEAVER.BEAMISSUE_NO} = " & TEMPBEAMISSUENO & " AND {BEAMISSUEWEAVER.BEAMISSUE_YEARID} = " & YearId
        '    OBJYARNISSUE.Show()
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        If EDIT = True Then PRINTREPORT()
    End Sub

    Private Sub CMBISSBEAMNAME_Enter(sender As Object, e As EventArgs) Handles CMBBEAMNAME.Enter
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then fillBEAM(CMBBEAMNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBISSBEAMNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBBEAMNAME.Validating
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then BEAMVALIDATE(CMBBEAMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillgrid()

        GRIDBEAMISSUE.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDBEAMISSUE.Rows.Add(Val(TXTGRIDSRNO.Text.Trim), CMBBEAMNAME.Text.Trim, TXTBEAMNO.Text.Trim, Val(TXTGAMANO.Text.Trim), Val(TXTSECTION.Text.Trim), CMBROLLNO.Text.Trim, Val(TXTBEAMWT.Text.Trim), Val(TXTBREAKAGE.Text.Trim), Val(TXTENDS.Text.Trim), Val(TXTTAPLINE.Text.Trim), Val(TXTCUT.Text.Trim), Val(TXTWT.Text.Trim), Val(TXTCUTWT.Text.Trim), TXTGRIDREMARKS.Text.Trim, CMBSIZER.Text.Trim, 0, 0, "", 0, 0, "", "")
            getsrno(GRIDBEAMISSUE)
            TXTBEAMNO.Text = TXTBEAMNO.Text + 1

            GRIDBEAMISSUE.FirstDisplayedScrollingRowIndex = GRIDBEAMISSUE.RowCount - 1
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDBEAMISSUE.Item(gsrno.Index, TEMPROW).Value = Val(TXTGRIDSRNO.Text.Trim)
            GRIDBEAMISSUE.Item(GBEAMNAME.Index, TEMPROW).Value = CMBBEAMNAME.Text.Trim
            GRIDBEAMISSUE.Item(GBEAMNO.Index, TEMPROW).Value = Val(TXTBEAMNO.Text.Trim)
            GRIDBEAMISSUE.Item(GGAMANO.Index, TEMPROW).Value = Val(TXTGAMANO.Text.Trim)
            GRIDBEAMISSUE.Item(GSECTION.Index, TEMPROW).Value = Val(TXTSECTION.Text.Trim)
            GRIDBEAMISSUE.Item(GROLLNO.Index, TEMPROW).Value = CMBROLLNO.Text.Trim
            GRIDBEAMISSUE.Item(GBEAMWT.Index, TEMPROW).Value = Val(TXTBEAMWT.Text.Trim)
            GRIDBEAMISSUE.Item(GBREAKAGE.Index, TEMPROW).Value = Val(TXTBREAKAGE.Text.Trim)
            GRIDBEAMISSUE.Item(GENDS.Index, TEMPROW).Value = Val(TXTENDS.Text.Trim)
            GRIDBEAMISSUE.Item(GTAPLINE.Index, TEMPROW).Value = Val(TXTTAPLINE.Text.Trim)
            GRIDBEAMISSUE.Item(GMTRS.Index, TEMPROW).Value = Val(TXTCUT.Text.Trim)
            GRIDBEAMISSUE.Item(GWT.Index, TEMPROW).Value = Val(TXTWT.Text.Trim)
            GRIDBEAMISSUE.Item(GCUTWT.Index, TEMPROW).Value = Val(TXTCUTWT.Text.Trim)

            GRIDBEAMISSUE.Item(GNARR.Index, TEMPROW).Value = TXTGRIDREMARKS.Text.Trim
            GRIDBEAMISSUE.Item(GSIZERNAME.Index, TEMPROW).Value = CMBSIZER.Text.Trim

            TXTBEAMNO.Text = MAXNO + 1

            GRIDDOUBLECLICK = False
        End If
        'TXTGRIDSRNO.Text = GRIDBEAMISSUE.RowCount + 1
        'CMBOURGODOWN.Text = ""
        CMBBEAMNAME.Text = ""
        GetLastBeamNo()

        TXTGAMANO.Clear()
        TXTSECTION.Clear()
        CMBROLLNO.DataSource = Nothing
        CMBROLLNO.Text = ""
        'If CMBROLLNO.Text = "" Then fillROLLITEM(CMBROLLNO, edit, "AND ROLLITEM = 1 ", "HAVING SUM(QTY - ISSQTY) >0")
        If CMBROLLNO.Text = "" Then
            Dim strUsedRolls As String = ""
            For Each ROW As DataGridViewRow In GRIDBEAMISSUE.Rows
                If ROW.IsNewRow Then Continue For
                If GRIDDOUBLECLICK = True And ROW.Index = TEMPROW Then Continue For
                Dim cellVal As String = If(ROW.Cells(GROLLNO.Index).Value IsNot Nothing, ROW.Cells(GROLLNO.Index).Value.ToString.Trim, "")
                If cellVal <> "" Then strUsedRolls = strUsedRolls & "'" & cellVal & "',"
            Next
            If strUsedRolls <> "" Then strUsedRolls = " AND ITEMNAME NOT IN (" & strUsedRolls.TrimEnd(",") & ") "
            fillROLLITEM(CMBROLLNO, EDIT, "AND ROLLITEM = 1 " & strUsedRolls, "HAVING SUM(QTY - ISSQTY) >0")
        End If
        TXTBEAMWT.Clear()
        TXTBREAKAGE.Clear()
        TXTENDS.Clear()
        TXTTAPLINE.Clear()
        TXTCUT.Clear()
        TXTWT.Clear()
        TXTCUTWT.Clear()
        TXTGRIDREMARKS.Clear()
        CMBSIZER.Text = ""
        'getsrno(GRIDSTOCK)
        TOTAL()

        CMBBEAMNAME.Focus()

        TXTGRIDSRNO.Text = Val(GRIDBEAMISSUE.RowCount) + 1

    End Sub
    Public Function GetGridMaxBeamNo() As Integer
        For Each r As DataGridViewRow In GRIDBEAMISSUE.Rows
            If Not r.IsNewRow Then
                If Val(r.Cells(GBEAMNO.Index).Value) > MAXNO Then
                    MAXNO = Val(r.Cells(GBEAMNO.Index).Value)
                End If
            End If
        Next
    End Function


    Sub GetLastBeamNo()
        Dim NextBeamNo As Integer
        Dim OBJCMN As New ClsCommon
        Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(MAX(BEAMNO),0)+1 AS LASTNO ", "", "BEAMSTOCKATJOBBER")
        If DT.Rows.Count > 0 Then NextBeamNo = DT.Rows(0).Item(0)
        TXTBEAMNO.Text = NextBeamNo
    End Sub



    Private Sub TXTBEAMNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTBEAMNO.Validating

        If Val(TXTBEAMNO.Text) <> 0 Then
            If GRIDBEAMISSUE.RowCount > 0 Then
                If Not CHECKBEAM() Then
                    MsgBox("Beam No already Present in Grid below")
                    TXTBEAMNO.Clear()
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If

    End Sub


    Function CHECKBEAM() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In GRIDBEAMISSUE.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Or GRIDDOUBLECLICK = False Then
                    If TXTBEAMNO.Text.Trim = ROW.Cells(GBEAMNO.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function CHECKROLL() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In GRIDBEAMISSUE.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Or GRIDDOUBLECLICK = False Then
                    If CMBROLLNO.Text.Trim = ROW.Cells(GROLLNO.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJJO As New OpeningBeamStockAtJobberDetails
            OBJJO.MdiParent = MDIMain
            OBJJO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBROLLNO_Validating(sender As Object, e As CancelEventArgs) Handles CMBROLLNO.Validating
        'If CMBROLLNO.Text <> "" Then
        If GRIDBEAMISSUE.RowCount > 0 Then
            If Not CHECKROLL() Then
                MsgBox("Roll No already Present in Grid below")
                CMBROLLNO.Text = ""
                e.Cancel = True
                Exit Sub
            End If
        End If
        'End If
    End Sub

    Private Sub CMBSIZER_Enter(sender As Object, e As EventArgs) Handles CMBSIZER.Enter
        Try
            If CMBSIZER.Text.Trim = "" Then FILLNAME(CMBSIZER, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZER_Validating(sender As Object, e As CancelEventArgs) Handles CMBSIZER.Validating
        Try
            If CMBSIZER.Text.Trim <> "" Then NAMEVALIDATE(CMBSIZER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZER_Validated(sender As Object, e As EventArgs) Handles CMBSIZER.Validated
        FILLGRID()
    End Sub

    Private Sub GRIDBEAMISSUE_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDBEAMISSUE.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub EDITROW()
        Try
            If GRIDBEAMISSUE.CurrentRow.Index >= 0 And GRIDBEAMISSUE.Item(gsrno.Index, GRIDBEAMISSUE.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True

                TXTGRIDSRNO.Text = GRIDBEAMISSUE.Item(gsrno.Index, GRIDBEAMISSUE.CurrentRow.Index).Value.ToString
                CMBBEAMNAME.Text = GRIDBEAMISSUE.Item(GBEAMNAME.Index, GRIDBEAMISSUE.CurrentRow.Index).Value.ToString
                TXTBEAMNO.Text = Val(GRIDBEAMISSUE.Item(GBEAMNO.Index, GRIDBEAMISSUE.CurrentRow.Index).Value)
                TXTGAMANO.Text = Val(GRIDBEAMISSUE.Item(GGAMANO.Index, GRIDBEAMISSUE.CurrentRow.Index).Value)
                TXTSECTION.Text = Val(GRIDBEAMISSUE.Item(GSECTION.Index, GRIDBEAMISSUE.CurrentRow.Index).Value)
                fillROLLITEM(CMBROLLNO, EDIT, "AND ROLLITEM = 1", "")
                CMBROLLNO.Text = GRIDBEAMISSUE.Item(GROLLNO.Index, GRIDBEAMISSUE.CurrentRow.Index).Value.ToString

                TXTBEAMWT.Text = Val(GRIDBEAMISSUE.Item(GBEAMWT.Index, GRIDBEAMISSUE.CurrentRow.Index).Value)
                TXTBREAKAGE.Text = Val(GRIDBEAMISSUE.Item(GBREAKAGE.Index, GRIDBEAMISSUE.CurrentRow.Index).Value)
                TXTENDS.Text = Val(GRIDBEAMISSUE.Item(GENDS.Index, GRIDBEAMISSUE.CurrentRow.Index).Value)
                TXTTAPLINE.Text = Val(GRIDBEAMISSUE.Item(GTAPLINE.Index, GRIDBEAMISSUE.CurrentRow.Index).Value)
                TXTCUT.Text = Val(GRIDBEAMISSUE.Item(GMTRS.Index, GRIDBEAMISSUE.CurrentRow.Index).Value)
                TXTWT.Text = Val(GRIDBEAMISSUE.Item(GWT.Index, GRIDBEAMISSUE.CurrentRow.Index).Value)
                TXTCUTWT.Text = Val(GRIDBEAMISSUE.Item(GCUTWT.Index, GRIDBEAMISSUE.CurrentRow.Index).Value)
                TXTGRIDREMARKS.Text = GRIDBEAMISSUE.Item(GNARR.Index, GRIDBEAMISSUE.CurrentRow.Index).Value.ToString
                CMBSIZER.Text = GRIDBEAMISSUE.Item(GSIZERNAME.Index, GRIDBEAMISSUE.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDBEAMISSUE.CurrentRow.Index
                CMBBEAMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class