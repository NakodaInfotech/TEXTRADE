
Imports BL
Imports System.Windows.Forms
Imports System.IO
Imports System.ComponentModel

Public Class BeamRecdFromSizer


    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
        Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
        Dim TEMPROW, TEMPUPLOADROW As Integer
        Public EDIT As Boolean
        Public TEMPBEAMRECDNO As Integer
        Dim TEMPMSG As Integer

        Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
            Me.Close()
        End Sub

        Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
            CLEAR()
            EDIT = False
            CMBNAME.Focus()
        End Sub

        Sub TOTAL()
            Try
                TXTBEAMTOTAL.Text = 0.0
                TXTBEAMDIFF.Text = 0.0
                TXTCUTTOTAL.Text = 0.0
                TXTCUTDIFF.Text = 0.0
                TXTWTTOTAL.Text = 0.0
                TXTWTDIFF.Text = 0.0
                LBLTAPLINE.Text = 0.0

                Dim TOTALTAPLINE As Double
                For Each ROW As DataGridViewRow In GRIDBEAM.Rows
                    If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                        TXTBEAMTOTAL.Text = Format(Val(TXTBEAMTOTAL.Text) + 1, "0")
                        TXTCUTTOTAL.Text = Format(Val(TXTCUTTOTAL.Text) + Val(ROW.Cells(GCUT.Index).EditedFormattedValue), "0.00")
                        TXTWTTOTAL.Text = Format(Val(TXTWTTOTAL.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                        TOTALTAPLINE += Val(ROW.Cells(GTAPLINE.Index).Value)
                    End If
                Next

                TXTBEAMDIFF.Text = Format(Val(TXTBEAMSUMM.Text.Trim) - Val(TXTBEAMTOTAL.Text.Trim), "0")
                TXTCUTDIFF.Text = Format(Val(TXTCUTSUMM.Text.Trim) - Val(TXTCUTTOTAL.Text.Trim), "0.00")
                TXTWTDIFF.Text = Format(Val(TXTWTSUMM.Text.Trim) - Val(TXTWTTOTAL.Text.Trim), "0.000")
                LBLTAPLINE.Text = Format(Val(TOTALTAPLINE / (GRIDBEAM.RowCount)), "0.00")

            'GET ACTUAL CUT TO BE RECD WITH RESPECT TO PROGRAM NO
            'If Val(TXTPROGRAMNO.Text.Trim) > 0 Then
            '    Dim OBJCMN As New ClsCommon
            '    Dim DT As DataTable = OBJCMN.search("PROGRAM_LENGTH AS LENGTH", "", "PROGRAMMASTER", " AND PROGRAM_SRNO = " & Val(TXTPROGRAMNO.Text.Trim) & " AND PROGRAM_YEARID = " & YearId)
            '    If DT.Rows.Count > 0 Then TXTACTUALCUT.Text = Format((Val(DT.Rows(0).Item(0)) / Val(LBLTAPLINE.Text)) * 1.02, "0.00")
            'Else
            '    TXTACTUALCUT.Text = Format((Val(TXTLENGTH.Text.Trim) / Val(LBLTAPLINE.Text)) * 1.02, "0.00")
            'End If


            'COUNT CALC
            If GRIDBEAM.RowCount > 0 Then TXTCOUNT.Text = Format(((Val(GRIDBEAM.Rows(0).Cells(GENDS.Index).Value) * Val(LBLTAPLINE.Text.Trim)) / 1850) / (Val(TXTWTSUMM.Text.Trim) / Val(TXTCUTSUMM.Text.Trim)), "0.00")


                'CALC LONGATION
                If GRIDBEAM.RowCount > 0 Then TXTLONGATION.Text = Format(((((Val(GRIDBEAM.Rows(0).Cells(GENDS.Index).Value) * Val(LBLTAPLINE.Text.Trim)) / 1850) / (Val(TXTDENIER.Text.Trim))) * Val(TXTCUTSUMM.Text.Trim)) - Val(TXTWTSUMM.Text.Trim), "0.00")


            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Sub CLEAR()

            TXTBEAMRECDNO.Clear()
            DTBEAMRECDDATE.Text = Mydate
            TXTPROGRAMNO.Clear()
        CMBOURGODOWN.Text = USERGODOWN
        CMBNAME.Text = ""
            CMBMILLNAME.Text = ""
            TXTCHALLANNO.Clear()
            DTCHALLANDATE.Text = Mydate
            TXTROLLISSUENO.Clear()
            TXTROLLISSUESRNO.Clear()
            TXTTYPE.Clear()
            TXTACTUALCUT.Clear()
            LBLTAPLINE.Text = 0.0
            TXTBEAMSUMM.Clear()
            TXTBEAMTOTAL.Clear()
            TXTBEAMDIFF.Clear()
            TXTWTSUMM.Clear()
            TXTWTTOTAL.Clear()
            TXTWTDIFF.Clear()
            TXTCUTSUMM.Clear()
            TXTCUTTOTAL.Clear()
            TXTCUTDIFF.Clear()
            TXTREMARKS.Clear()
            TXTSRNO.Clear()
            TXTBEAMNO.Clear()
            CMBBEAMNAME.Text = ""
            TXTENDS.Clear()
            TXTTAPLINE.Clear()
            TXTCUT.Clear()
            TXTWT.Clear()
            TXTWTCUT.Clear()
            TXTNARR.Clear()
            TXTLENGTH.Clear()

            EP.Clear()
            lbllocked.Visible = False
            PBlock.Visible = False
            CMDSELECTROLLISSUE.Enabled = True

            TXTREMARKS.Clear()

            TXTROLLENDS.Clear()
            TXTCOUNT.Clear()
            TXTLONGATION.Clear()
            TXTDENIER.Clear()


            GRIDBEAM.RowCount = 0

            GETMAX_BEAMRECD_NO()

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

        End Sub

        Sub GETMAX_BEAMRECD_NO()
            Dim DTTABLE As New DataTable
            DTTABLE = getmax("ISNULL(MAX(BEAMREC_NO),0)+1", "BEAMRECEIVED", "AND BEAMREC_YEARID=" & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTBEAMRECDNO.Text = DTTABLE.Rows(0).Item(0)
        End Sub

        Private Sub BeamReceivedFromSizer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                GRIDBEAM.Focus()
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
        If CMBMILLNAME.Text = "" Then FILLMILL(CMBMILLNAME, EDIT)
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT)
        If CMBBEAMNAME.Text = "" Then fillBEAM(CMBBEAMNAME, EDIT)
        End Sub

        Private Sub BeamReceivedFromSizer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'BEAM RECD'")
            USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)

                Cursor.Current = Cursors.WaitCursor

                FILLCMB()
                CLEAR()
            CMBOURGODOWN.Text = USERGODOWN

            If EDIT = True Then
                    If USEREDIT = False And USERVIEW = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim dttable As New DataTable
                    Dim OBJBEAMREC As New ClsBeamReceived

                    OBJBEAMREC.alParaval.Add(TEMPBEAMRECDNO)
                    OBJBEAMREC.alParaval.Add(YearId)
                    dttable = OBJBEAMREC.selectBEAM()

                    If dttable.Rows.Count > 0 Then
                        CMBNAME.Focus()

                        TXTBEAMRECDNO.Text = TEMPBEAMRECDNO
                        DTBEAMRECDDATE.Text = dttable.Rows(0).Item("DATE")
                        TXTPROGRAMNO.Text = Val(dttable.Rows(0).Item("PROGRAMNO"))
                        TXTLENGTH.Text = Val(dttable.Rows(0).Item("LENGTH"))

                        CMBOURGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                        CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString
                        CMBMILLNAME.Text = dttable.Rows(0).Item("MILLNAME").ToString
                        TXTCHALLANNO.Text = dttable.Rows(0).Item("CHALLANNO").ToString
                        DTCHALLANDATE.Text = dttable.Rows(0).Item("CHALLANDATE")

                        TXTROLLISSUENO.Text = Val(dttable.Rows(0).Item("ROLLISSUENO"))
                        TXTROLLISSUESRNO.Text = Val(dttable.Rows(0).Item("ROLLISSUESRNO"))
                        TXTTYPE.Text = dttable.Rows(0).Item("TYPE")

                        TXTBEAMSUMM.Text = dttable.Rows(0).Item("TOTALBEAM").ToString
                        TXTCUTSUMM.Text = dttable.Rows(0).Item("TOTALCUT").ToString
                        TXTWTSUMM.Text = dttable.Rows(0).Item("TOTALWT")
                        TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString


                        TXTROLLENDS.Text = Val(dttable.Rows(0).Item("ROLLENDS"))
                        TXTDENIER.Text = Val(dttable.Rows(0).Item("DENIER"))
                        TXTCOUNT.Text = Val(dttable.Rows(0).Item("COUNT"))
                        TXTLONGATION.Text = Val(dttable.Rows(0).Item("LONGATION"))


                        'ITEM GRID
                        For Each ROW As DataRow In dttable.Rows
                            GRIDBEAM.Rows.Add(Val(ROW("SRNO")), ROW("BEAMNO"), ROW("BEAMNAME"), Val(ROW("ENDS")), Val(ROW("TAPLINE")), Format(Val(ROW("CUT")), "0.00"), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTCUT")), "0.000"), ROW("NARR"), ROW("GRIDDONE"))
                            If Convert.ToBoolean(ROW("GRIDDONE")) = True Then
                                lbllocked.Visible = True
                                PBlock.Visible = True
                                GRIDBEAM.Rows(GRIDBEAM.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            End If
                        Next


                        'UPLOAD(GRID)
                        Dim OBJCMN As New ClsCommon
                        Dim DT As DataTable = OBJCMN.search(" BEAMRECEIVED_UPLOAD.BEAMREC_SRNO AS GRIDSRNO, BEAMRECEIVED_UPLOAD.BEAMREC_REMARKS AS REMARKS, BEAMRECEIVED_UPLOAD.BEAMREC_NAME AS NAME, BEAMRECEIVED_UPLOAD.BEAMREC_PHOTO AS IMGPATH ", "", " BEAMRECEIVED_UPLOAD ", " AND BEAMRECEIVED_UPLOAD.BEAMREC_NO = " & TEMPBEAMRECDNO & " AND BEAMREC_YEARID = " & YearId & " ORDER BY BEAMRECEIVED_UPLOAD.BEAMREC_SRNO")
                        If DT.Rows.Count > 0 Then
                            For Each DTR As DataRow In DT.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If
                        TOTAL()
                        CMDSELECTROLLISSUE.Enabled = False
                    End If
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

                alParaval.Add(Format(Convert.ToDateTime(DTBEAMRECDDATE.Text.Trim).Date, "MM/dd/yyyy"))
                alParaval.Add(Val(TXTPROGRAMNO.Text.Trim))
                alParaval.Add(CMBOURGODOWN.Text.Trim)
                alParaval.Add(CMBNAME.Text.Trim)
                alParaval.Add(CMBMILLNAME.Text.Trim)
                alParaval.Add(TXTLENGTH.Text.Trim)
                alParaval.Add(TXTCHALLANNO.Text.Trim)
                alParaval.Add(DTCHALLANDATE.Text.Trim)
                alParaval.Add(Val(TXTROLLISSUENO.Text.Trim))
                alParaval.Add(Val(TXTROLLISSUESRNO.Text.Trim))
                alParaval.Add(TXTTYPE.Text.Trim)
                alParaval.Add(Val(TXTBEAMSUMM.Text.Trim))
                alParaval.Add(Val(TXTCUTSUMM.Text.Trim))
                alParaval.Add(Val(TXTWTSUMM.Text.Trim))
                alParaval.Add(Val(LBLTAPLINE.Text.Trim))
                alParaval.Add(TXTREMARKS.Text.Trim)

                'ADDING FROMTO BEAM NO
                'GET FIRST AND LAST ROW OF GRID
                alParaval.Add("From " & GRIDBEAM.Rows(0).Cells(GBEAMNO.Index).Value.ToString & " To " & GRIDBEAM.Rows(GRIDBEAM.RowCount - 1).Cells(GBEAMNO.Index).Value.ToString)



                alParaval.Add(Val(TXTROLLENDS.Text.Trim))
                alParaval.Add(Val(TXTDENIER.Text.Trim))
                alParaval.Add(Val(TXTCOUNT.Text.Trim))
                alParaval.Add(Val(TXTLONGATION.Text.Trim))


                alParaval.Add(CmpId)
                alParaval.Add(Userid)
                alParaval.Add(YearId)

                Dim SRNO As String = ""
                Dim BEAMNO As String = ""
                Dim BEAMNAME As String = ""
                Dim ENDS As String = ""
                Dim TL As String = ""
                Dim CUT As String = ""
                Dim WT As String = ""
                Dim WTCUT As String = ""
                Dim NARR As String = ""
                Dim GRIDDONE As String = ""

                For Each row As Windows.Forms.DataGridViewRow In GRIDBEAM.Rows
                    If row.Cells(GSRNO.Index).Value <> Nothing Then
                        If SRNO = "" Then
                            SRNO = row.Cells(GSRNO.Index).Value
                            BEAMNO = row.Cells(GBEAMNO.Index).Value.ToString
                            BEAMNAME = row.Cells(GBEAMNAME.Index).Value.ToString
                            ENDS = Val(row.Cells(GENDS.Index).Value)
                            TL = Val(row.Cells(GTAPLINE.Index).Value)
                            CUT = Val(row.Cells(GCUT.Index).Value)
                            WT = Val(row.Cells(GWT.Index).Value)
                            WTCUT = Val(row.Cells(GWTCUT.Index).Value)
                            NARR = row.Cells(GNARR.Index).Value.ToString
                            If row.Cells(GDONE.Index).Value = True Then
                                GRIDDONE = 1
                            Else
                                GRIDDONE = 0
                            End If

                        Else

                            SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                            BEAMNO = BEAMNO & "|" & row.Cells(GBEAMNO.Index).Value.ToString
                            BEAMNAME = BEAMNAME & "|" & row.Cells(GBEAMNAME.Index).Value.ToString
                            ENDS = ENDS & "|" & Val(row.Cells(GENDS.Index).Value)
                            TL = TL & "|" & Val(row.Cells(GTAPLINE.Index).Value)
                            CUT = CUT & "|" & Val(row.Cells(GCUT.Index).Value)
                            WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                            WTCUT = WTCUT & "|" & Val(row.Cells(GWTCUT.Index).Value)
                            NARR = NARR & "|" & row.Cells(GNARR.Index).Value
                            If row.Cells(GDONE.Index).Value = True Then
                                GRIDDONE = GRIDDONE & "|" & "1"
                            Else
                                GRIDDONE = GRIDDONE & "|" & "0"
                            End If

                        End If
                    End If
                Next

                alParaval.Add(SRNO)
                alParaval.Add(BEAMNO)
                alParaval.Add(BEAMNAME)
                alParaval.Add(ENDS)
                alParaval.Add(TL)
                alParaval.Add(CUT)
                alParaval.Add(WT)
                alParaval.Add(WTCUT)
                alParaval.Add(NARR)
                alParaval.Add(GRIDDONE)


                Dim OBJBEAMREC As New ClsBeamReceived
                OBJBEAMREC.alParaval = alParaval

                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    Dim DT As DataTable = OBJBEAMREC.SAVE()
                    TEMPBEAMRECDNO = DT.Rows(0).Item(0)
                    MsgBox("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPBEAMRECDNO)
                    IntResult = OBJBEAMREC.UPDATE()
                    EDIT = False
                    MsgBox("Details Updated")

                End If

                If gridupload.RowCount > 0 Then SAVEUPLOAD()

                'CLEAR()
                'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
                Call toolnext_Click(sender, e)
                DTBEAMRECDDATE.Focus()

            Catch ex As Exception
                If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
            End Try
        End Sub

        Sub SAVEUPLOAD()

            Try
                Dim OBJBEAMREC As New ClsBeamReceived


                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPBEAMRECDNO)
                        ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                        PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                        PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                        ALPARAVAL.Add(MS.ToArray)
                        ALPARAVAL.Add(YearId)

                        OBJBEAMREC.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJBEAMREC.SAVEUPLOAD()
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


            If DTBEAMRECDDATE.Text = "__/__/____" Then
                EP.SetError(DTBEAMRECDDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(DTBEAMRECDDATE.Text) Then
                    EP.SetError(DTBEAMRECDDATE, "Date not in Accounting Year")
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

            If DTBEAMRECDDATE.Text.Trim <> "__/__/____" And DTCHALLANDATE.Text.Trim <> "__/__/____" Then
                If Convert.ToDateTime(DTBEAMRECDDATE.Text).Date > Convert.ToDateTime(DTCHALLANDATE.Text).Date Then
                    EP.SetError(DTCHALLANDATE, " Please Enter Proper Challan Date")
                    bln = False
                End If
            End If

            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, "Please Fill Jobber Name")
                bln = False
            End If

            If CMBMILLNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBMILLNAME, " Please Fill Mill Name ")
                bln = False
            End If

            If CMBOURGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBOURGODOWN, " Please Fill Godown ")
                bln = False
            End If

            If TXTBEAMSUMM.Text.Trim.Length = 0 Then
                EP.SetError(LBLBEAM, " Please fill proper Beam Summary")
                bln = False
            End If

            If TXTCUTSUMM.Text.Trim.Length = 0 Then
                EP.SetError(LBLBEAM, " Please fill proper Beam Summary")
                bln = False
            End If

            If TXTWTSUMM.Text.Trim.Length = 0 Then
                EP.SetError(LBLBEAM, " Please fill proper Beam Summary")
                bln = False
            End If

            If GRIDBEAM.RowCount = 0 Then
                EP.SetError(TXTNARR, "Enter Proper Details")
                bln = False
            End If

            If Val(TXTROLLISSUENO.Text.Trim) = 0 Then
                EP.SetError(TXTROLLISSUENO, "Select Roll Issue Entry")
                bln = False
            End If

            For Each row As DataGridViewRow In GRIDBEAM.Rows
                If Val(row.Cells(GWT.Index).Value) = 0 Then
                    EP.SetError(TXTNARR, "Wt Cannot be 0 or Less")
                    bln = False
                End If

                If Val(row.Cells(GCUT.Index).Value) = 0 Then
                    EP.SetError(TXTNARR, "Cut Cannot be 0 or Less")
                    bln = False
                End If
            Next


            'DONE TEMPORARILY
            'If lbllocked.Visible = True Then
            '    EP.SetError(lbllocked, "Unable to Modify, Entry Locked")
            '    bln = False
            'End If

            If Val(TXTBEAMDIFF.Text.Trim) > 0 Or Val(TXTCUTDIFF.Text.Trim) > 0 Or Val(TXTWTDIFF.Text.Trim) > 0 Or Val(TXTBEAMDIFF.Text.Trim) < 0 Or Val(TXTCUTDIFF.Text.Trim) < 0 Or Val(TXTWTDIFF.Text.Trim) < 0 Then
                If MsgBox("Beam/Cut/Wt. All Difference Should be 0, Wish To Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    EP.SetError(TXTWTDIFF, "Beam/Cut/Wt. All Difference Should be 0")
                    bln = False
                End If
            End If

            Return bln
        End Function

        Private Sub DTBEAMRECDDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTBEAMRECDDATE.GotFocus
            DTBEAMRECDDATE.Select(0, 0)
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

        Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
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
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub CMBMILLNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMILLNAME.Enter
            Try
            If CMBMILLNAME.Text = "" Then FILLMILL(CMBMILLNAME, EDIT)
        Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub CMBMILLNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBMILLNAME.KeyDown
            Try
                If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

                If e.KeyCode = Keys.F1 Then
                    Dim OBJLEDGER As New SelectLedger
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'"
                    OBJLEDGER.ShowDialog()
                    If OBJLEDGER.TEMPNAME <> "" Then CMBMILLNAME.Text = OBJLEDGER.TEMPNAME
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub CMBMILLNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILLNAME.Validating
            Try
            If CMBMILLNAME.Text.Trim <> "" Then MILLVALIDATE(CMBMILLNAME, e, Me)
        Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
            Try
            If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT)
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
            GRIDBEAM.RowCount = 0
LINE1:
            TEMPBEAMRECDNO = Val(TXTBEAMRECDNO.Text) - 1
Line2:
            If TEMPBEAMRECDNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" BEAMREC_NO ", "", "  BEAMRECEIVED", " AND BEAMREC_NO = '" & TEMPBEAMRECDNO & "' AND BEAMRECEIVED.BEAMREC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    BeamReceivedFromSizer_Load(sender, e)
                Else
                    TEMPBEAMRECDNO = Val(TEMPBEAMRECDNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDBEAM.RowCount = 0 And TEMPBEAMRECDNO > 1 Then
                TXTBEAMRECDNO.Text = TEMPBEAMRECDNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDBEAM.RowCount = 0
LINE1:
            TEMPBEAMRECDNO = Val(TXTBEAMRECDNO.Text) + 1
            GETMAX_BEAMRECD_NO()
            Dim MAXNO As Integer = TXTBEAMRECDNO.Text.Trim
            CLEAR()
            If Val(TXTBEAMRECDNO.Text) - 1 >= TEMPBEAMRECDNO Then
                EDIT = True
                BeamReceivedFromSizer_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDBEAM.RowCount = 0 And TEMPBEAMRECDNO < MAXNO Then
                TXTBEAMRECDNO.Text = TEMPBEAMRECDNO
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
                TEMPBEAMRECDNO = Val(tstxtbillno.Text)
                If TEMPBEAMRECDNO > 0 Then
                    EDIT = True
                    BeamReceivedFromSizer_Load(sender, e)
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

    Private Sub GRIDBEAM_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDBEAM.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDBEAM.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

                If Convert.ToBoolean(GRIDBEAM.Item(GDONE.Index, e.RowIndex).Value) = True Then
                    MsgBox("Beam Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDBEAM.Item(GSRNO.Index, e.RowIndex).Value
                TXTBEAMNO.Text = GRIDBEAM.Item(GBEAMNO.Index, e.RowIndex).Value
                CMBBEAMNAME.Text = GRIDBEAM.Item(GBEAMNAME.Index, e.RowIndex).Value
                TXTENDS.Text = GRIDBEAM.Item(GENDS.Index, e.RowIndex).Value
                TXTTAPLINE.Text = GRIDBEAM.Item(GTAPLINE.Index, e.RowIndex).Value
                TXTCUT.Text = GRIDBEAM.Item(GCUT.Index, e.RowIndex).Value
                TXTWT.Text = Format(Val(GRIDBEAM.Item(GWT.Index, e.RowIndex).Value), "0.000")
                TXTWTCUT.Text = GRIDBEAM.Item(GWTCUT.Index, e.RowIndex).Value
                TXTNARR.Text = GRIDBEAM.Item(GNARR.Index, e.RowIndex).Value

                TEMPROW = e.RowIndex
                TXTBEAMNO.Focus()

            End If
        Catch ex As Exception
            Throw ex
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

                If Convert.ToBoolean(GRIDBEAM.CurrentRow.Cells(GDONE.Index).Value) = True Then
                    MsgBox("Beam Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                GRIDBEAM.Rows.RemoveAt(GRIDBEAM.CurrentRow.Index)
                getsrno(GRIDBEAM)
                TXTSRNO.Text = GRIDBEAM.RowCount + 1
                TOTAL()

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call CMDSAVE_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call CMDDELETE_Click(sender, e)
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
                    alParaval.Add(TEMPBEAMRECDNO)
                    alParaval.Add(YearId)

                    Dim OBJDEL As New ClsBeamReceived
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

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDBEAM.Rows.Add(Val(TXTSRNO.Text.Trim), TXTBEAMNO.Text.Trim, CMBBEAMNAME.Text.Trim, Val(TXTENDS.Text.Trim), Val(TXTTAPLINE.Text.Trim), Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTWT.Text.Trim), "0.000"), Format(Val(TXTWTCUT.Text.Trim), "0.000"), TXTNARR.Text.Trim)
            Else
                GRIDBEAM.Item(GSRNO.Index, TEMPROW).Value = TXTSRNO.Text.Trim
                GRIDBEAM.Item(GBEAMNO.Index, TEMPROW).Value = TXTBEAMNO.Text.Trim
                GRIDBEAM.Item(GBEAMNAME.Index, TEMPROW).Value = CMBBEAMNAME.Text.Trim
                GRIDBEAM.Item(GENDS.Index, TEMPROW).Value = Val(TXTENDS.Text.Trim)
                GRIDBEAM.Item(GTAPLINE.Index, TEMPROW).Value = Format(Val(TXTTAPLINE.Text.Trim))
                GRIDBEAM.Item(GCUT.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
                GRIDBEAM.Item(GWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.000")
                GRIDBEAM.Item(GWTCUT.Index, TEMPROW).Value = Format(Val(TXTWTCUT.Text.Trim), "0.000")
                GRIDBEAM.Item(GNARR.Index, TEMPROW).Value = TXTNARR.Text.Trim

                GRIDDOUBLECLICK = False
            End If
            TXTBEAMNO.Text = Val(TXTBEAMNO.Text.Trim) + 1
            TXTNARR.Clear()
            getsrno(GRIDBEAM)
            TOTAL()
            TXTBEAMNO.Focus()
            If GRIDBEAM.RowCount > 0 Then TXTSRNO.Text = Val(GRIDBEAM.RowCount) + 1 Else TXTSRNO.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub calc()
        TXTWTCUT.Text = Val(TXTWTSUMM.Text) / Val(TXTCUTSUMM.Text.Trim)
        TXTWT.Text = Format(Val(TXTWTCUT.Text) * Val(TXTCUT.Text.Trim), "0.000")
        TXTWTCUT.Text = Format(Val(TXTWTCUT.Text.Trim), "0.000")
    End Sub

    Private Sub TXTNARR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARR.Validating
        If TXTBEAMNO.Text.Trim <> "" And CMBBEAMNAME.Text.Trim <> "" And Val(TXTCUT.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 And Val(TXTTAPLINE.Text.Trim) > 0 Then FILLGRID() Else MsgBox("Please Enter proper details")
    End Sub

    Private Sub TXTCUT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCUT.Validating
        calc()
    End Sub

    Private Sub TXTWT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTWT.Validating
        If Val(TXTWT.Text.Trim) = 0 Then calc()
    End Sub

    Private Sub TXTBEAM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTBEAMSUMM.KeyPress
        numkeypress(e, TXTBEAMSUMM, Me)
    End Sub

    Private Sub CMBBEAMNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBEAMNAME.Enter
        Try
            If CMBBEAMNAME.Text.Trim = "" Then fillBEAM(CMBBEAMNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBEAMNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJBEAM As New SelectBeam
                OBJBEAM.ShowDialog()
                If OBJBEAM.TEMPNAME <> "" Then CMBBEAMNAME.Text = OBJBEAM.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBBEAMNAME.Validated
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(BEAM_TOTALENDS, 0) AS ENDS, ISNULL(BEAM_TAPLINE, 0) AS TAPLINE", "", "BEAMMASTER", "AND BEAMMASTER.BEAM_NAME = '" & CMBBEAMNAME.Text.Trim & "' AND BEAM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTENDS.Text = DT.Rows(0).Item("ENDS")
                    If Val(TXTTAPLINE.Text.Trim) = 0 Then TXTTAPLINE.Text = DT.Rows(0).Item("TAPLINE")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBEAMNAME.Validating
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then BEAMVALIDATE(CMBBEAMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJBEAM As New BeamRecdFromSizerDetails
            OBJBEAM.MdiParent = MDIMain
            OBJBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBEAMSUMM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTBEAMSUMM.Validated
            TOTAL()
        End Sub

        Private Sub TXTCUTSUMM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTCUTSUMM.Validated
            TOTAL()
        End Sub

        Private Sub TXTWTSUMM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTWTSUMM.Validated
            TOTAL()
        End Sub

        Private Sub TXTCUT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCUT.KeyPress
            numdotkeypress(e, TXTCUT, Me)
        End Sub

        Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
            numdot3(e, TXTWT, Me)
        End Sub

        Private Sub DTBEAMRECDDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTBEAMRECDDATE.Validating
            Try
                If DTBEAMRECDDATE.Text.Trim <> "__/__/____" Then
                    'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                    Dim TEMP As DateTime
                    If Not DateTime.TryParse(DTBEAMRECDDATE.Text, TEMP) Then
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

    Private Sub CMDSELECTROLLISSUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTROLLISSUE.Click
        Try
            If CMBNAME.Text.Trim = "" And ClientName = "SASHWINKUMAR" Then
                MsgBox("Select Sizer Name First", MsgBoxStyle.Critical)
                CMBNAME.Focus()
                Exit Sub
            End If

            Dim OBJROLLISSUE As New SelectRollIssue
            OBJROLLISSUE.SIZERNAME = CMBNAME.Text.Trim
            Dim DT As DataTable = OBJROLLISSUE.DT
            OBJROLLISSUE.ShowDialog()
            If DT.Rows.Count > 0 Then
                CMBNAME.Text = DT.Rows(0).Item("SIZERNAME")
                TXTROLLISSUENO.Text = DT.Rows(0).Item("ROLLISSUENO")
                TXTROLLISSUESRNO.Text = DT.Rows(0).Item("ROLLISSUESRNO")
                TXTTYPE.Text = DT.Rows(0).Item("TYPE")
                CMBMILLNAME.Text = DT.Rows(0).Item("MILLNAME")
                TXTLENGTH.Text = Val(DT.Rows(0).Item("LENGTH"))
                TXTWTSUMM.Text = Val(DT.Rows(0).Item("WT"))
                TXTROLLENDS.Text = Val(DT.Rows(0).Item("TOTALENDS"))
                TXTDENIER.Text = Val(DT.Rows(0).Item("DENIER"))
                TXTPROGRAMNO.Text = Val(DT.Rows(0).Item("PROGRAMNO"))
                CMDSELECTROLLISSUE.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

        Private Sub BeamRecd_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'If ALLOWMFG = False Then Exit Sub
    End Sub

        Private Sub DTCHALLANDATE_GotFocus(sender As Object, e As EventArgs) Handles DTCHALLANDATE.GotFocus
            DTCHALLANDATE.Select(0, 0)
        End Sub

        Private Sub TXTBEAMNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTBEAMNO.Validating
            Try
                If TXTBEAMNO.Text.Trim <> "" Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search("BEAMRECEIVED_DESC.BEAMREC_NO AS BEAMRECNO", "", " BEAMRECEIVED_DESC INNER JOIN BEAMRECEIVED ON BEAMRECEIVED_DESC.BEAMREC_NO = BEAMRECEIVED.BEAMREC_NO AND BEAMRECEIVED_DESC.BEAMREC_YEARID = BEAMRECEIVED.BEAMREC_YEARID INNER JOIN LEDGERS ON BEAMRECEIVED.BEAMREC_LEDGERID = LEDGERS.Acc_id ", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND BEAMRECEIVED_DESC.BEAMREC_BEAMNO = '" & TXTBEAMNO.Text.Trim & "' AND BEAMRECEIVED_DESC.BEAMREC_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        If EDIT = False Or (EDIT = True And Val(DT.Rows(0).Item("BEAMRECNO")) <> Val(TXTBEAMRECDNO.Text.Trim)) Then
                            If MsgBox("Duplicate Beam No, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then e.Cancel = True
                        End If
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
    End Class
