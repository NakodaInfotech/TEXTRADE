
Imports System.ComponentModel
Imports BL
Imports DevExpress.Diagram.Core.Native

Public Class BeamRecdWarper

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPBEAMRECDNO, tempzalanirollno As Integer
    Dim TEMPMSG As Integer
    Dim NextBeamNo As Integer
    Dim MAXNO As Integer = 0

    Dim TEMPBEAMNO As Integer

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
            Dim TEMPWARPWT As Double
            Dim TEMPSELWT As Double
            LBLTOTALJOBMTRS.Text = 0.0
            TXTTOTALMTRS.Text = 0.0
            LBLTAPLINE.Text = 0.0
            LBLTOTALBEAMMTRS.Text = 0.0

            'Dim TOTALTAPLINE As Double
            For Each ROW As DataGridViewRow In GRIDBEAM.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then

                    LBLTOTALJOBMTRS.Text = Format(Val(LBLTOTALJOBMTRS.Text) + Val(ROW.Cells(GJOBMTRS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALBEAMMTRS.Text = Format(Val(LBLTOTALBEAMMTRS.Text) + Val(ROW.Cells(GBEAMMTRS.Index).EditedFormattedValue), "0.00")
                End If
            Next
            TXTTOTALMTRS.Text = Format(Val(LBLTOTALBEAMMTRS.Text), "0.00")

            'If ClientName = "SWPL" Then
            If Val(TXTBEAMWT.Text.Trim) = 0 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(DESIGNCARD.DESIGN_TOTALWARPWT, 0) AS TOTALWARPWT, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEWT, 0) AS TOTALSELWT, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME ", "", " DESIGNCARD INNER JOIN ITEMMASTER ON DESIGNCARD.DESIGN_ITEMID = ITEMMASTER.item_id ", " AND ITEMMASTER.item_name = '" & GRIDBEAM.Item(GITEMNAME.Index, GRIDBEAM.CurrentRow.Index).Value & "' AND DESIGNCARD.DESIGN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWARPWT = DT.Rows(0).Item("TOTALWARPWT")
                    TEMPSELWT = DT.Rows(0).Item("TOTALSELWT")
                End If
                TXTBEAMWT.Text = Format(Val(TEMPWARPWT + TEMPSELWT) * Val(GRIDBEAM.Item(GBEAMMTRS.Index, GRIDBEAM.CurrentRow.Index).EditedFormattedValue), "0.00")
            End If
            'End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()

        CMDSELECTYARNISSUE.Enabled = True
        TXTBEAMRECDNO.Clear()
        DTBEAMRECDDATE.Text = Mydate
        CMBOURGODOWN.Text = USERGODOWN
        CMBNAME.Text = ""
        CMBMILLNAME.Text = ""

        TXTCHALLANNO.Clear()
        DTCHALLANDATE.Text = Mydate
        LBLTAPLINE.Text = 0.0
        TXTTOTALMTRS.Clear()
        TXTBEAMNAME.Clear()
        TXTREMARKS.Clear()
        'TXTBEAMNO.Clear()
        TXTENDS.Clear()
        TXTTOTALMTRS.Clear()
        TXTGAMANO.Clear()
        TXTBEAMWT.Clear()
        CMBROLLNO.Text = ""
        TXTSECTION.Clear()

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        TXTREMARKS.Clear()


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

        TXTUPLOADSRNO.Text = 1

        GetLastBeamNo()
        'TXTBEAMNO.Text = NextBeamNo

        fillROLLITEM(CMBROLLNO, EDIT, "AND ROLLITEM = 1 ", "HAVING SUM(QTY - ISSQTY) >0")
        CMBROLLNO.Enabled = True

        LBLTOTALJOBMTRS.Text = 0.0
        LBLTAPLINE.Text = 0.0
        LBLTOTALBEAMMTRS.Text = 0.0




    End Sub

    Sub GETMAX_BEAMRECD_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(BEAMREC_NO),0)+1", "BEAMRECEIVEDWARPER", "AND BEAMREC_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTBEAMRECDNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub BeamRecdWarper_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        If CMBMILLNAME.Text = "" Then FILLNAME(CMBMILLNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT)
        fillROLLITEM(CMBROLLNO, EDIT, "AND ROLLITEM = 1 ", "HAVING SUM(QTY - ISSQTY) >0")
    End Sub

    Private Sub BeamRecdWarper_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                Dim OBJBEAMREC As New ClsBeamReceivedWarper

                OBJBEAMREC.alParaval.Add(TEMPBEAMRECDNO)
                OBJBEAMREC.alParaval.Add(YearId)
                dttable = OBJBEAMREC.selectBEAM()

                If dttable.Rows.Count > 0 Then
                    CMBNAME.Focus()

                    TXTBEAMRECDNO.Text = TEMPBEAMRECDNO
                    DTBEAMRECDDATE.Text = dttable.Rows(0).Item("DATE")
                    CMBOURGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    CMBMILLNAME.Text = dttable.Rows(0).Item("MILLNAME").ToString
                    TXTBEAMNAME.Text = dttable.Rows(0).Item("BEAMNAME").ToString
                    TXTBEAMNO.Text = dttable.Rows(0).Item("BEAMNO").ToString
                    TXTENDS.Text = dttable.Rows(0).Item("ENDS").ToString
                    TXTGAMANO.Text = dttable.Rows(0).Item("GAMANO").ToString
                    TXTSECTION.Text = dttable.Rows(0).Item("SECTION").ToString

                    TXTBEAMWT.Text = dttable.Rows(0).Item("BEAMWT").ToString
                    TXTBREAKAGE.Text = dttable.Rows(0).Item("BREAKAGE").ToString

                    TXTCHALLANNO.Text = dttable.Rows(0).Item("CHALLANNO").ToString
                    DTCHALLANDATE.Text = dttable.Rows(0).Item("CHALLANDATE")
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString

                    CMBROLLNO.Text = dttable.Rows(0).Item("ROLLNO").ToString



                    CMDSELECTYARNISSUE.Enabled = False

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDBEAM.Rows.Add(Val(ROW("SRNO")), Val(ROW("JOBNO")), Val(ROW("JOBSRNO")), ROW("ITEMNAME"), Val(ROW("REED")), Val(ROW("REEDSPACE")), Val(ROW("PICS")), ROW("DESCRIPTION"), Val(ROW("GRIDENDS")), ROW("REFNO"), Val(ROW("JOBMTRS")), Val(ROW("BEAMMTRS")), ROW("FROMTYPE"), Val(ROW("GRIDDONE")), Val(ROW("OUTMTRS")))

                        If Convert.ToBoolean(ROW("GRIDDONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                            GRIDBEAM.Rows(GRIDBEAM.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                        End If

                        If Val(ROW("OUTMTRS")) > 0 Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                            GRIDBEAM.Rows(GRIDBEAM.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                        End If
                    Next


                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" BEAMRECEIVEDWARPER_UPLOAD.BEAMREC_SRNO AS GRIDSRNO, BEAMRECEIVEDWARPER_UPLOAD.BEAMREC_REMARKS AS REMARKS, BEAMRECEIVEDWARPER_UPLOAD.BEAMREC_NAME AS NAME, BEAMRECEIVEDWARPER_UPLOAD.BEAMREC_PHOTO AS IMGPATH ", "", " BEAMRECEIVEDWARPER_UPLOAD ", " AND BEAMRECEIVEDWARPER_UPLOAD.BEAMREC_NO = " & TEMPBEAMRECDNO & " AND BEAMREC_YEARID = " & YearId & " ORDER BY BEAMRECEIVEDWARPER_UPLOAD.BEAMREC_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If
                    TOTAL()
                End If

                CMBROLLNO.Enabled = False
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
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBMILLNAME.Text.Trim)
            alParaval.Add(TXTBEAMNAME.Text.Trim)
            alParaval.Add(TXTBEAMNO.Text.Trim)
            alParaval.Add(Val(TXTTOTALMTRS.Text.Trim))
            alParaval.Add(Val(TXTENDS.Text.Trim))
            alParaval.Add(Val(TXTGAMANO.Text.Trim))
            alParaval.Add(Val(TXTSECTION.Text.Trim))
            alParaval.Add(CMBROLLNO.Text.Trim)
            alParaval.Add(Val(TXTBEAMWT.Text.Trim))
            alParaval.Add(Val(TXTBREAKAGE.Text.Trim))
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(DTCHALLANDATE.Text.Trim)
            alParaval.Add(Val(LBLTOTALJOBMTRS.Text.Trim))
            alParaval.Add(Val(LBLTOTALBEAMMTRS.Text.Trim))
            alParaval.Add(Val(LBLTAPLINE.Text.Trim))
            alParaval.Add(TXTREMARKS.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)




            Dim SRNO As String = ""
            Dim JOBNO As String = ""
            Dim JOBSRNO As String = ""
            Dim ITEMNAME As String = ""
            Dim REED As String = ""
            Dim REEDSPACE As String = ""
            Dim PICS As String = ""
            Dim DESC As String = ""
            Dim GRIDENDS As String = ""
            Dim REFNO As String = ""
            Dim JOBMTRS As String = ""
            Dim BEAMMTRS As String = ""
            Dim FROMTYPE As String = ""
            Dim GRIDDONE As String = ""
            Dim OUTMTRS As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDBEAM.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = row.Cells(GSRNO.Index).Value
                        JOBNO = Val(row.Cells(GJOBNO.Index).Value)
                        JOBSRNO = Val(row.Cells(GJOBSRNO.Index).Value)
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        REED = Val(row.Cells(GREED.Index).Value)
                        REEDSPACE = Val(row.Cells(GREEDSPACE.Index).Value)
                        PICS = Val(row.Cells(GPICS.Index).Value)
                        DESC = row.Cells(GDESC.Index).Value.ToString
                        GRIDENDS = Val(row.Cells(GGRIDENDS.Index).Value)
                        REFNO = row.Cells(GREFNO.Index).Value.ToString
                        JOBMTRS = Val(row.Cells(GJOBMTRS.Index).Value)
                        BEAMMTRS = Val(row.Cells(GBEAMMTRS.Index).Value)
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString
                        If row.Cells(GDONE.Index).Value = True Then
                            GRIDDONE = 1
                        Else
                            GRIDDONE = 0
                        End If
                        OUTMTRS = Val(row.Cells(GOUTMTRS.Index).Value)


                    Else

                        SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                        JOBNO = JOBNO & "|" & Val(row.Cells(GJOBNO.Index).Value)
                        JOBSRNO = JOBSRNO & "|" & Val(row.Cells(GJOBSRNO.Index).Value)
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value.ToString
                        REED = REED & "|" & row.Cells(GREED.Index).Value
                        REEDSPACE = REEDSPACE & "|" & row.Cells(GREEDSPACE.Index).Value
                        PICS = PICS & "|" & row.Cells(GPICS.Index).Value
                        DESC = DESC & "|" & row.Cells(GDESC.Index).Value.ToString
                        GRIDENDS = GRIDENDS & "|" & row.Cells(GGRIDENDS.Index).Value
                        REFNO = REFNO & "|" & row.Cells(GREFNO.Index).Value.ToString
                        JOBMTRS = JOBMTRS & "|" & row.Cells(GJOBMTRS.Index).Value
                        BEAMMTRS = BEAMMTRS & "|" & row.Cells(GBEAMMTRS.Index).Value
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value.ToString
                        If row.Cells(GDONE.Index).Value = True Then
                            GRIDDONE = GRIDDONE & "|" & "1"
                        Else
                            GRIDDONE = GRIDDONE & "|" & "0"
                        End If
                        OUTMTRS = OUTMTRS & "|" & row.Cells(GOUTMTRS.Index).Value


                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(JOBNO)
            alParaval.Add(JOBSRNO)
            alParaval.Add(ITEMNAME)
            alParaval.Add(REED)
            alParaval.Add(REEDSPACE)
            alParaval.Add(PICS)
            alParaval.Add(DESC)
            alParaval.Add(GRIDENDS)
            alParaval.Add(REFNO)
            alParaval.Add(JOBMTRS)
            alParaval.Add(BEAMMTRS)
            alParaval.Add(FROMTYPE)
            alParaval.Add(GRIDDONE)
            alParaval.Add(OUTMTRS)



            Dim OBJBEAMREC As New ClsBeamReceivedWarper
            OBJBEAMREC.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJBEAMREC.SAVE()
                TEMPBEAMRECDNO = DT.Rows(0).Item(0)


                MsgBox("Details Added")

                If ClientName = "SWPL" Then
                    'NOW NO NEED TO GENERATE INVOICE IN ABHEE
                    GENERATECONSUMPTION()
                End If


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

            'If lbllocked.Visible = False Then
            '    If MsgBox("Issue Beam Directly to Weaver?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '        Dim OBJWEAVER As New DirectIssueWeaver
            '        OBJWEAVER.ShowDialog()
            '        If OBJWEAVER.cmbname.Text.Trim = "" Then GoTo LINE1
            ' DIRECTISSUEWEAVER(OBJWEAVER.cmbname.Text.Trim)
            '    End If
            'End If

LINE1:
            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTBEAMRECDDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub DIRECTISSUEWEAVER(ByVal WEAVERNAME As String)
        'Try
        '    Dim ALPARAVAL As New ArrayList
        '    ALPARAVAL.Add(Format(Convert.ToDateTime(DTBEAMRECDDATE.Text.Trim).Date, "MM/dd/yyyy"))
        '    ALPARAVAL.Add(CMBOURGODOWN.Text.Trim)
        '    ALPARAVAL.Add(WEAVERNAME)
        '    ALPARAVAL.Add("")   'TRANSPORT
        '    ALPARAVAL.Add("")   'VEHICLE NO
        '    ALPARAVAL.Add("")   'EWBNO
        '    ALPARAVAL.Add("")   'REMARKS

        '    ALPARAVAL.Add(Val(TXTTOTALCUT.Text.Trim))
        '    ALPARAVAL.Add(Val(TXTTOTALWT.Text.Trim))

        '    ALPARAVAL.Add(CmpId)
        '    ALPARAVAL.Add(Userid)
        '    ALPARAVAL.Add(YearId)

        '    Dim SRNO As String = ""
        '    Dim BEAMNAME As String = ""
        '    Dim BEAMNO As String = ""
        '    Dim ENDS As String = ""
        '    Dim TL As String = ""
        '    Dim CUT As String = ""
        '    Dim WT As String = ""
        '    Dim WTCUT As String = ""
        '    Dim NARR As String = ""
        '    Dim FROMNO As String = ""
        '    Dim FROMSRNO As String = ""
        '    Dim FROMTYPE As String = ""
        '    Dim OUTCUT As String = ""
        '    Dim GRIDDONE As String = ""
        '    Dim NAME As String = ""
        '    Dim LOOMNO As String = ""
        '    Dim UPLOADDATE As String = ""

        '    For Each row As Windows.Forms.DataGridViewRow In GRIDBEAM.Rows
        '        If SRNO = "" Then
        '            SRNO = Val(row.Cells(GSRNO.Index).Value)
        '            BEAMNAME = row.Cells(GBEAMNAME.Index).Value
        '            BEAMNO = row.Cells(GBEAMNO.Index).Value
        '            ENDS = row.Cells(GENDS.Index).Value
        '            TL = row.Cells(GTAPLINE.Index).Value
        '            CUT = Val(row.Cells(GCUT.Index).Value)
        '            WT = Val(row.Cells(GWT.Index).Value)
        '            WTCUT = Val(row.Cells(GWTCUT.Index).Value)
        '            NARR = row.Cells(GNARR.Index).Value
        '            FROMNO = Val(TXTBEAMRECDNO.Text.Trim)
        '            FROMSRNO = Val(row.Cells(GSRNO.Index).Value)
        '            FROMTYPE = "BEAMWARPER"
        '            OUTCUT = "0"
        '            GRIDDONE = "0"
        '            NAME = CMBNAME.Text.Trim
        '            LOOMNO = "0"
        '            UPLOADDATE = ""
        '        Else
        '            SRNO = SRNO & "|" & Val(row.Cells(GSRNO.Index).Value)
        '            BEAMNAME = BEAMNAME & "|" & row.Cells(GBEAMNAME.Index).Value
        '            BEAMNO = BEAMNO & "|" & row.Cells(GBEAMNO.Index).Value
        '            ENDS = ENDS & "|" & row.Cells(GENDS.Index).Value
        '            TL = TL & "|" & row.Cells(GTAPLINE.Index).Value
        '            CUT = CUT & "|" & Val(row.Cells(GCUT.Index).Value)
        '            WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
        '            WTCUT = WTCUT & "|" & Val(row.Cells(GWTCUT.Index).Value)
        '            NARR = NARR & "|" & row.Cells(GNARR.Index).Value
        '            FROMNO = FROMNO & "|" & Val(TXTBEAMRECDNO.Text.Trim)
        '            FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GSRNO.Index).Value)
        '            FROMTYPE = FROMTYPE & "|" & "BEAMWARPER"
        '            OUTCUT = OUTCUT & "|" & "0"
        '            GRIDDONE = GRIDDONE & "|" & "0"
        '            NAME = NAME & "|" & CMBNAME.Text.Trim
        '            LOOMNO = LOOMNO & "|" & "0"
        '            UPLOADDATE = UPLOADDATE & "|" & ""
        '        End If
        '    Next

        '    'SCHEDULE GRID DATA SAME AS MAIN GRID DATA, LOOM NO ALWAYS 0
        '    ALPARAVAL.Add(SRNO)
        '    ALPARAVAL.Add(BEAMNAME)
        '    ALPARAVAL.Add(LOOMNO)


        '    ALPARAVAL.Add(SRNO)
        '    ALPARAVAL.Add(BEAMNAME)
        '    ALPARAVAL.Add(BEAMNO)
        '    ALPARAVAL.Add(ENDS)
        '    ALPARAVAL.Add(TL)
        '    ALPARAVAL.Add(CUT)
        '    ALPARAVAL.Add(WT)
        '    ALPARAVAL.Add(WTCUT)
        '    ALPARAVAL.Add(NARR)
        '    ALPARAVAL.Add(FROMNO)
        '    ALPARAVAL.Add(FROMSRNO)
        '    ALPARAVAL.Add(FROMTYPE)
        '    ALPARAVAL.Add(OUTCUT)
        '    ALPARAVAL.Add(GRIDDONE)
        '    ALPARAVAL.Add(NAME)
        '    ALPARAVAL.Add(LOOMNO)
        '    ALPARAVAL.Add(UPLOADDATE)


        '    Dim OBJBEAMISSUE As New ClsBeamIssueToWeaver
        '    OBJBEAMISSUE.alParaval = ALPARAVAL
        '    If USERADD = False Then
        '        MsgBox("Insufficient Rights")
        '        Exit Sub
        '    End If
        '    Dim DT As DataTable = OBJBEAMISSUE.SAVE()
        '    MsgBox("Beam Issue To Weaver Added")

        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJBEAMREC As New ClsBeamReceivedWarper


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


    Sub GENERATECONSUMPTION()

        Try

            Cursor.Current = Cursors.WaitCursor
                Dim alParaval As New ArrayList

                alParaval.Add(Format(Convert.ToDateTime(DTBEAMRECDDATE.Text).Date, "MM/dd/yyyy"))
                alParaval.Add(CMBOURGODOWN.Text.Trim)
                alParaval.Add("")   'DEPARTMENT
                alParaval.Add("")   'ISSUETO
                alParaval.Add("")   ' CHALLANNO
                alParaval.Add(1)     ' TOTALQTY
                alParaval.Add("")    ' REMARKS
                alParaval.Add(CmpId)
                alParaval.Add(Userid)
                alParaval.Add(YearId)

                alParaval.Add(1)    'GRIDSRNO
                alParaval.Add(CMBROLLNO.Text.Trim)
                alParaval.Add(TXTBEAMNO.Text.Trim)   'DESCRIPTION
                alParaval.Add(1)   ' QTY

                Dim TEMPUNIT As String = ""
                Dim OBJCMN As New ClsCommon
                Dim TEMPDT As DataTable = OBJCMN.SEARCH(" STOREITEMMASTER.STOREITEM_NAME AS ITEMNAME, UNITMASTER.unit_abbr AS UNIT ", "", " STOREITEMMASTER INNER JOIN UNITMASTER ON STOREITEMMASTER.STOREITEM_UNITID = UNITMASTER.unit_id ", " AND STOREITEMMASTER.STOREITEM_NAME =  '" & CMBROLLNO.Text & "' ")
                If TEMPDT.Rows.Count > 0 Then
                    TEMPUNIT = TEMPDT.Rows(0).Item("UNIT")
                End If

                alParaval.Add(TEMPUNIT)
                alParaval.Add("")  ' MACHINE
                alParaval.Add("") ' TAKEN BY 

                Dim OBJCONSUME As New ClsStoreConsumption
                OBJCONSUME.alParaval = alParaval
                Dim DTTABLE As DataTable = OBJCONSUME.SAVE()


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try


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

        If CMBOURGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBOURGODOWN, " Please Fill Godown ")
            bln = False
        End If


        If CMBROLLNO.Text.Trim.Length = 0 Then
            EP.SetError(CMBROLLNO, " Please Fill Roll No ")
            bln = False
        End If


        If Val(TXTBEAMWT.Text.Trim) = 0 Then
            EP.SetError(TXTBEAMWT, " Please Fill Beam Wt ")
            bln = False
        End If


        For Each row As DataGridViewRow In GRIDBEAM.Rows
            If Val(row.Cells(GBEAMMTRS.Index).Value) = 0 Then
                EP.SetError(CMBOURGODOWN, "Beam Mtrs Cannot be 0 or Less")
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




        If TXTBEAMNO.Text.Trim.Length = 0 Then
            EP.SetError(TXTBEAMNO, "Fill Beam No")
            bln = False
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

    Private Sub CMBMILLNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMILLNAME.Enter
        Try
            If CMBMILLNAME.Text = "" Then FILLNAME(CMBMILLNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBMILLNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILLNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILLNAME.Validating
        Try
            If CMBMILLNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBMILLNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
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
                OBJGODOWN.SEARCH = " And GODOWN_ISOUR = 'True'"
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
                Dim DT As DataTable = OBJCMN.SEARCH(" BEAMREC_NO ", "", "  BEAMRECEIVEDWARPER", " AND BEAMREC_NO = '" & TEMPBEAMRECDNO & "' AND BEAMRECEIVEDWARPER.BEAMREC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    BeamRecdWarper_Load(sender, e)
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
                BeamRecdWarper_Load(sender, e)
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
                    BeamRecdWarper_Load(sender, e)
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

                    Dim OBJDEL As New ClsBeamReceivedWarper
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




    Sub CALC()
        'Try
        '    'GET WT AUTO FROM BEAMMASTER AND MULTIPLY IT BY CUT
        '    If CMBBEAMNAME.Text.Trim <> "" And Val(TXTBEAMWT.Text.Trim) = 0 Then
        '        Dim OBJCMN As New ClsCommon
        '        Dim DT As DataTable = OBJCMN.SEARCH(" BEAM_TOTALWT AS BEAMWT, BEAM_TAPLINE AS TAPLINE", "", " BEAMMASTER ", " AND BEAM_NAME = '" & CMBBEAMNAME.Text.Trim & "' AND BEAM_YEARID = " & YearId)
        '        If DT.Rows.Count > 0 Then
        '            If Val(TXTTOTALMTRS.Text.Trim) <> Val(DT.Rows(0).Item("TAPLINE")) Then
        '                TXTBEAMWT.Text = Format(Val(TXTGAMANO.Text.Trim) * ((Val(TXTTOTALMTRS.Text.Trim) * Val(DT.Rows(0).Item("BEAMWT"))) / Val(DT.Rows(0).Item("TAPLINE"))), "0.000")
        '            Else
        '                TXTBEAMWT.Text = Format(Val(TXTGAMANO.Text.Trim) * Val(DT.Rows(0).Item("BEAMWT")), "0.000")
        '            End If
        '        End If
        '    End If
        '    ' TXTROLLNO.Text = Format(Val(TXTBEAMWT.Text) / Val(TXTGAMANO.Text.Trim), "0.000")
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub



    Private Sub TXTCUT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTGAMANO.Validating, TXTBEAMWT.Validating, TXTTOTALMTRS.Validating
        Try
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub





    Private Sub CMBBEAMNAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'If CMBBEAMNAME.Text.Trim <> "" Then
            '    Dim OBJCMN As New ClsCommon
            '    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(BEAM_TAPLINE, 0) AS TAPLINE, ISNULL(BEAM_TOTALENDS, 0) AS TOTALENDS", "", "BEAMMASTER", "AND BEAMMASTER.BEAM_NAME = '" & CMBBEAMNAME.Text.Trim & "' AND BEAM_YEARID = " & YearId)
            '    If DT.Rows.Count > 0 Then
            '        TXTENDS.Text = DT.Rows(0).Item("TOTALENDS")
            '        'TXTMTRS.Text = DT.Rows(0).Item("TAPLINE")
            '    End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJBEAM As New BeamRecdWarperDetails
            OBJBEAM.MdiParent = MDIMain
            OBJBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTROLLISSUE_Click(sender As Object, e As EventArgs) Handles CMDSELECTYARNISSUE.Click
        Try
            If CMBNAME.Text.Trim = "" Then
                MsgBox("Select Sizer Name First", MsgBoxStyle.Critical)
                CMBNAME.Focus()
                Exit Sub
            End If

            Dim OBJYARNISSUE As New SelectJobOrder
            Dim DT As DataTable = OBJYARNISSUE.DT
            OBJYARNISSUE.SIZERNAME = CMBNAME.Text.Trim
            OBJYARNISSUE.ShowDialog()

            If GRIDBEAM.Rows.Count = 0 Then TEMPBEAMNO = 0

            If DT.Rows.Count > 0 Then

                TXTENDS.Text = DT.Rows(0).Item("ENDS")

                ''  GETTING DISTINCT ITEMNAME  IN TEXTBOX
                Dim DV As DataView = DT.DefaultView
                Dim NEWDT As DataTable = DV.ToTable(True, "ITEMNAME")
                For Each DTR As DataRow In NEWDT.Rows
                    If TXTBEAMNAME.Text.Trim = "" Then
                        TXTBEAMNAME.Text = DTR("ITEMNAME").ToString
                    Else
                        TXTBEAMNAME.Text = TXTBEAMNAME.Text & "/" & DTR("ITEMNAME").ToString
                    End If
                Next

                For Each dr As DataRow In DT.Rows
                    GRIDBEAM.Rows.Add(0, Val(dr("JOBNO")), Val(dr("JOBSRNO")), dr("ITEMNAME"), dr("REED"), dr("REEDSPACE"), dr("PICS"), dr("DESCRIPTION"), dr("ENDS"), dr("REFNO"), Format(Val(dr("JOBMTRS")), "0"), 0, dr("FROMTYPE"), 0, 0)
                Next


                GRIDBEAM.FirstDisplayedScrollingRowIndex = GRIDBEAM.RowCount - 1
                getsrno(GRIDBEAM)

                CMDSELECTYARNISSUE.Enabled = False
                TOTAL()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTGAMANO.KeyPress, TXTBEAMWT.KeyPress, TXTSECTION.KeyPress, TXTTOTALMTRS.KeyPress, TXTBEAMWT.KeyPress, TXTBREAKAGE.KeyPress
        numdotkeypress(e, sender, Me)
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

    Private Sub BeamRecdWarper_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'If ALLOWMFG = False Then Exit Sub
    End Sub

    Private Sub DTCHALLANDATE_GotFocus(sender As Object, e As EventArgs) Handles DTCHALLANDATE.GotFocus
        DTCHALLANDATE.Select(0, 0)
    End Sub

    Private Sub TXTTAPLINE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTENDS.KeyPress, TXTBEAMNO.KeyPress
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub GetLastBeamNo()
        Dim NextBeamNo As Integer
        Dim OBJCMN As New ClsCommon
        Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(MAX(BEAMNO),0)+1 AS LASTNO ", "", "BEAMSTOCK")
        If DT.Rows.Count > 0 Then NextBeamNo = DT.Rows(0).Item(0)
        TXTBEAMNO.Text = NextBeamNo
    End Sub

    'Private Sub TXTBEAMNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTBEAMNO.Validating

    '    If Val(TXTBEAMNO.Text) <> 0 Then
    '        If GRIDBEAM.RowCount > 0 Then
    '            If Not CHECKBEAM() Then
    '                MsgBox("Beam No already Present in Grid below")
    '                TXTBEAMNO.Clear()
    '                e.Cancel = True
    '                Exit Sub
    '            End If
    '        End If
    '    End If

    'End Sub

    'Function CHECKBEAM() As Boolean
    '    Try
    '        Dim bln As Boolean = True
    '        For Each ROW As DataGridViewRow In GRIDBEAM.Rows
    '            If (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Or GRIDDOUBLECLICK = False Then
    '                If TXTBEAMNO.Text.Trim = ROW.Cells(GBEAMNO.Index).Value Then bln = False
    '            End If
    '        Next
    '        Return bln
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function




    Sub getmax_BEAMNO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(BEAMREC_BEAMNO),0) + 1 ", "BEAMRECEIVEDWARPER ", "  AND BEAMREC_CMPID=" & CmpId)
        If DTTABLE.Rows.Count > 0 Then tempzalanirollno = DTTABLE.Rows(0).Item(0)
    End Sub



    Private Sub GRIDBEAM_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDBEAM.CellValidating
        Dim colNum As Integer = GRIDBEAM.Columns(e.ColumnIndex).Index
        If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return
        Select Case colNum

            Case GBEAMMTRS.Index
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

    Private Sub CMBROLLNO_Validated(sender As Object, e As EventArgs) Handles CMBROLLNO.Validated
        Try
            If CMBROLLNO.Text.Trim <> "" Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ITEMNAME ", "", "STORESTOCKREGISTER ", "  AND ITEMNAME =  '" & CMBROLLNO.Text & "' AND ROLLITEM = 1 AND YEARID =" & YearId)
                If DT.Rows.Count = 0 Then
                    MsgBox("Roll Stock Not Present !!!!")
                    CMBROLLNO.Text = ""
                    CMBROLLNO.Focus()
                End If

            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBEAMNO_Validated(sender As Object, e As EventArgs) Handles TXTBEAMNO.Validated
        Try
            If TXTBEAMNO.Text.Trim <> "" Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("BEAMNO ", "", "BEAMSTOCK ", "  AND BEAMNO =  '" & TXTBEAMNO.Text & "'  ")
                If DT.Rows.Count > 0 Then
                    MsgBox(" Beam No Already Present In Stock !!!!!")
                    TXTBEAMNO.Clear()
                    TXTBEAMNO.Focus()
                End If
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class