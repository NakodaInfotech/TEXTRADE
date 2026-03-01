
Imports BL
Imports System.ComponentModel

Public Class YarnInterGodownTransfer

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Public TEMPGODOWNNO As Integer          'used for editing
    Public EDIT As Boolean          'used for editing
    Dim TEMPROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer

    Dim PARTYCHALLANNO As String
    Dim ALLOWMANUALJONO As Boolean = False

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        EP.Clear()
        TXTGODOWNNO.Clear()

        If ALLOWMANUALJONO = True Then
            TXTGODOWNNO.ReadOnly = False
            TXTGODOWNNO.BackColor = Color.LemonChiffon
        Else
            TXTGODOWNNO.ReadOnly = True
            TXTGODOWNNO.BackColor = Color.Linen
        End If
        'CMBFROMGODOWN.Text = ""
        'CMBTOGODOWN.Text = ""

        CMBTRANSPORTNAME.Text = ""
        TXTADD.Clear()
        TXTDATE.Text = Now.Date
        tstxtbillno.Clear()
        txtremarks.Clear()
        CMDSELECTSTOCK.Enabled = True
        lbllocked.Visible = False
        PBlock.Visible = False
        LBLTOTALMTRS.Text = 0.0
        LBLTOTALPCS.Text = 0.0
        TXTREFRENCE.Clear()
        TXTISSUEBY.Clear()
        GRIDJO.RowCount = 0
        GRIDDOUBLECLICK = False
        getmaxno()

        'TXTLRNO.Clear()
        'DTLRDATE.Value = Now.Date
        LIFTINGDATE.Text = Now.Date


        If USERGODOWN <> "" Then CMBFROMGODOWN.Text = USERGODOWN Else CMBFROMGODOWN.Text = ""
        CMBTOGODOWN.Text = ""
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False
        'CMBFROMGODOWN.Focus()
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(YTRANSFER_no),0) + 1 ", " YARNINTERGODOWNTRANSFER ", " AND YTRANSFER_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTGODOWNNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True

            If CMBFROMGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBFROMGODOWN, " Please Fill Godown")
                bln = False
            End If

            If CMBTOGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBTOGODOWN, " Please Fill Godown")
                bln = False
            End If

            If CMBFROMGODOWN.Text.Trim = CMBTOGODOWN.Text.Trim Then
                EP.SetError(CMBFROMGODOWN, " From && To Godown cannot be same")
                bln = False
            End If

            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, " Inward Done, Delete Inward First")
                bln = False
            End If

            If GRIDJO.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If

            If TXTDATE.Text = "__/__/____" Then
                EP.SetError(TXTDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(TXTDATE.Text) Then
                    EP.SetError(TXTDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If

            If Val(TXTGODOWNNO.Text.Trim) = 0 Then
                EP.SetError(TXTGODOWNNO, "Enter  Godown No")
                bln = False
            End If


            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            If TXTGODOWNNO.ReadOnly = False Then
                alParaval.Add(Val(TXTGODOWNNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(Format(Convert.ToDateTime(TXTDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBFROMGODOWN.Text.Trim)
            alParaval.Add(CMBTOGODOWN.Text.Trim)
            alParaval.Add(CMBTRANSPORTNAME.Text.Trim)
            alParaval.Add(TXTREFRENCE.Text.Trim)
            alParaval.Add(TXTISSUEBY.Text.Trim)

            alParaval.Add(Val(LBLTOTALPCS.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)




            Dim gridsrno As String = ""
            Dim YARNQUALITY As String = ""
            Dim MILLNAME As String = ""
            Dim DESIGN As String = ""
            Dim PARTYLOTNO As String = ""
            Dim PARTYCOLOR As String = ""
            Dim COLOR As String = ""
            Dim LOTNO As String = ""
            Dim LRNO As String = ""
            Dim BAGS As String = ""
            Dim WEIGHT As String = ""
            Dim CONES As String = ""
            'Dim LRNO As String = ""
            Dim LRDATE As String = ""
            Dim LIFTINGDATE As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDJO.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(GSRNO.Index).Value.ToString
                        YARNQUALITY = row.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        PARTYLOTNO = row.Cells(GJOBBERLOTNO.Index).Value.ToString
                        PARTYCOLOR = row.Cells(GPCOLOR.Index).Value.ToString
                        COLOR = row.Cells(GCOLOR.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        LRNO = row.Cells(GLRNO.Index).Value.ToString
                        BAGS = row.Cells(GQTY.Index).Value.ToString
                        WEIGHT = row.Cells(GWT.Index).Value.ToString
                        CONES = row.Cells(GCONES.Index).Value.ToString
                        'LRNO = row.Cells(GLRNO.Index).Value
                        'LRDATE = Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        LIFTINGDATE = row.Cells(GLIFTINGDATE.Index).Value
                        'BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        'OUTPCS = row.Cells(GOUTPCS.Index).Value.ToString
                        'OUTMTRS = row.Cells(GOUTMTRS.Index).Value.ToString
                        'PONO = row.Cells(GPONO.Index).Value.ToString
                        'GRIDGSRNO = row.Cells(GGRIDSRNO.Index).Value.ToString
                        'FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString

                    Else
                        gridsrno = gridsrno & "|" & row.Cells(GSRNO.Index).Value.ToString
                        YARNQUALITY = YARNQUALITY & "|" & row.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        PARTYLOTNO = PARTYLOTNO & "|" & row.Cells(GJOBBERLOTNO.Index).Value.ToString
                        PARTYCOLOR = PARTYCOLOR & "|" & row.Cells(GPCOLOR.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(GCOLOR.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        LRNO = LRNO & "|" & row.Cells(GLRNO.Index).Value.ToString
                        BAGS = BAGS & "|" & row.Cells(GQTY.Index).Value
                        WEIGHT = WEIGHT & "|" & row.Cells(GWT.Index).Value
                        CONES = CONES & "|" & row.Cells(GCONES.Index).Value
                        'LRNO = LRNO & "|" & row.Cells(GLRNO.Index).Value
                        'LRDATE = LRDATE & "|" & Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        LIFTINGDATE = LIFTINGDATE & "|" & (row.Cells(GLIFTINGDATE.Index).Value)
                        'BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        'OUTPCS = OUTPCS & "|" & row.Cells(GOUTPCS.Index).Value.ToString
                        'OUTMTRS = OUTMTRS & "|" & row.Cells(GOUTMTRS.Index).Value.ToString
                        'PONO = PONO & "|" & row.Cells(GPONO.Index).Value.ToString
                        'GRIDGSRNO = GRIDGSRNO & "|" & row.Cells(GGRIDSRNO.Index).Value.ToString
                        'FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(YARNQUALITY)
            alParaval.Add(MILLNAME)
            alParaval.Add(DESIGN)
            alParaval.Add(PARTYLOTNO)
            alParaval.Add(PARTYCOLOR)
            alParaval.Add(COLOR)
            alParaval.Add(LOTNO)
            alParaval.Add(LRNO)
            alParaval.Add(BAGS)
            alParaval.Add(WEIGHT)
            alParaval.Add(CONES)
            'alParaval.Add(LRNO)
            'alParaval.Add(LRDATE)
            alParaval.Add(LIFTINGDATE)
            'alParaval.Add(BARCODE)
            'alParaval.Add(OUTPCS)
            'alParaval.Add(OUTMTRS)
            'alParaval.Add(PONO)
            'alParaval.Add(GRIDGSRNO)
            'alParaval.Add(FROMTYPE)



            Dim objCUTTING As New ClsYarnInterGodownTransfer()
            objCUTTING.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = objCUTTING.SAVE()
                MsgBox("Details Added")

                TXTGODOWNNO.Text = DTTABLE.Rows(0).Item(0)
                ' PRINTREPORT(DTTABLE.Rows(0).Item(0))

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                alParaval.Add(TEMPGODOWNNO)
                IntResult = objCUTTING.UPDATE()
                MsgBox("Details Updated")
                'PRINTREPORT(TEMPGODOWNNO)
                EDIT = False
            End If
            clear()
            TXTDATE.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub



    Private Sub YarnInterGodownTransfer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If errorvalid() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.Alt = True And (e.KeyCode = Windows.Forms.Keys.D1) Then
            TabControl1.Focus()
            TabControl1.SelectedIndex = (0)
        ElseIf e.Alt = True And (e.KeyCode = Windows.Forms.Keys.D2) Then
            TabControl1.SelectedIndex = (1)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        ElseIf e.KeyCode = Keys.F5 Then
            GRIDJO.Focus()
        ElseIf e.KeyCode = Keys.OemPipe Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
            toolprevious_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
            toolnext_Click(sender, e)
        ElseIf e.KeyCode = Keys.P And e.Alt = True Then
            Call PrintToolStripButton_Click(sender, e)
        End If
    End Sub

    Private Sub YarnInterGodownTransfer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'YARNINTER GODOWN'")
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

                Dim objJO As New ClsYarnInterGodownTransfer()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPGODOWNNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)
                objJO.alParaval = ALPARAVAL
                Dim dttable As DataTable = objJO.SELECTGODOWN()

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTGODOWNNO.Text = TEMPGODOWNNO
                        TXTGODOWNNO.ReadOnly = True
                        TXTDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBFROMGODOWN.Text = Convert.ToString(dr("FROMGODOWN").ToString)
                        CMBTOGODOWN.Text = Convert.ToString(dr("TOGODOWN").ToString)
                        CMBTRANSPORTNAME.Text = Convert.ToString(dr("TRANSPORTNAME").ToString)
                        TXTREFRENCE.Text = dr("REFRENCE")
                        TXTISSUEBY.Text = dr("ISSUE")

                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        LBLTOTALMTRS.Text = Val(dr("LBLTOTALMTRS"))
                        LBLTOTALPCS.Text = Val(dr("LBLTOTALPCS"))

                        'Item Grid


                        GRIDJO.Rows.Add(dr("GRIDSRNO").ToString, dr("YARNQUALITY").ToString, dr("MILLNAME").ToString, dr("DESIGN").ToString, dr("PARTYLOTNO").ToString, dr("PARTYCOLOR").ToString, dr("COLOR").ToString, dr("LOTNO").ToString, dr("LRNO").ToString, Format(dr("BAGS"), "0.00"), Format(dr("WT"), "0.00"), Format(dr("CONES"), "0.00"), dr("LIFTINGDATE").ToString)


                    Next
                    total()
                    GRIDJO.FirstDisplayedScrollingRowIndex = GRIDJO.RowCount - 1
                    chkchange.CheckState = CheckState.Checked
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
    Sub fillcmb()
        Try
            If CMBFROMGODOWN.Text.Trim = "" Then fillGODOWN(CMBFROMGODOWN, EDIT)
            If CMBTOGODOWN.Text.Trim = "" Then fillGODOWN(CMBTOGODOWN, EDIT)
            If CMBTRANSPORTNAME.Text.Trim = "" Then filltransname(CMBTRANSPORTNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJEMB As New YarnInterGodownTransferDetails
            OBJEMB.MdiParent = MDIMain
            OBJEMB.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORTNAME_Enter(sender As Object, e As EventArgs) Handles CMBTRANSPORTNAME.Enter
        Try
            If CMBTRANSPORTNAME.Text.Trim = "" Then FILLNAME(CMBTRANSPORTNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORTNAME_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBTRANSPORTNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'"
                OBJLEDGER.ShowDialog()
                'If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBTRANSPORTNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORTNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBTRANSPORTNAME.Validating
        Try
            If CMBTRANSPORTNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBTRANSPORTNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "SUNDRY CREDITORS")
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





    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDJO.RowCount = 0
LINE1:
            TEMPGODOWNNO = Val(TXTGODOWNNO.Text) - 1
            If TEMPGODOWNNO > 0 Then
                EDIT = True
                YarnInterGodownTransfer_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDJO.RowCount = 0 And TEMPGODOWNNO > 1 Then
                TXTGODOWNNO.Text = TEMPGODOWNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
LINE1:
            TEMPGODOWNNO = Val(TXTGODOWNNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTGODOWNNO.Text.Trim
            clear()
            If Val(TXTGODOWNNO.Text) - 1 >= TEMPGODOWNNO Then
                EDIT = True
                YarnInterGodownTransfer_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDJO.RowCount = 0 And TEMPGODOWNNO < MAXNO Then
                TXTGODOWNNO.Text = TEMPGODOWNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBFROMGODOWN_Enter(sender As Object, e As EventArgs) Handles CMBFROMGODOWN.Enter
        Try
            If CMBFROMGODOWN.Text.Trim = "" Then fillGODOWN(CMBFROMGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMGODOWN_Validating(sender As Object, e As CancelEventArgs) Handles CMBFROMGODOWN.Validating
        Try
            If CMBFROMGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBFROMGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub CMBTOGODOWN_Enter(sender As Object, e As EventArgs) Handles CMBTOGODOWN.Enter
        Try
            If CMBTOGODOWN.Text.Trim = "" Then fillGODOWN(CMBTOGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOGODOWN_Validating(sender As Object, e As CancelEventArgs) Handles CMBTOGODOWN.Validating
        Try
            If CMBTOGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBTOGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub





    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then
                'PRINTREPORT(TEMPGODOWNNO)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    'Sub PRINTREPORT(ByVal GODOWNNO As Integer)
    '    Try
    '        If MsgBox("Wish to Print?", MsgBoxStyle.YesNo) = vbYes Then
    '            Dim OBJGDN As New GDNDESIGN
    '            OBJGDN.MdiParent = MDIMain
    '            OBJGDN.FRMSTRING = "YARNGODOWNTRANSFER"
    '            OBJGDN.FORMULA = "{YARNINTERGODOWNTRANSFER.YTRANSFER_NO}=" & Val(GODOWNNO) & " and {YARNINTERGODOWNTRANSFER.YTRANSFER_yearid}=" & YearId
    '            OBJGDN.Show()
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then

                Dim TEMPMSG As Integer = MsgBox("Wish to Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                Dim OBJEMB As New ClsYarnInterGodownTransfer

                ALPARAVAL.Add(TEMPGODOWNNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)
                OBJEMB.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJEMB.Delete()
                MsgBox("Entry Deleted Succesfully")
                EDIT = False
                clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSTOCK_Click(sender As Object, e As EventArgs) Handles CMDSELECTSTOCK.Click
        Try
            If CMBFROMGODOWN.Text = "" Then
                EP.SetError(CMBFROMGODOWN, "Please Select From Godown")
                Exit Sub
            End If

            Dim DTJO As New DataTable
            Dim OBJSELECTGDN As New SelectYarnGodownTransfer
            OBJSELECTGDN.GODOWN = CMBFROMGODOWN.Text.Trim
            OBJSELECTGDN.ShowDialog()
            DTJO = OBJSELECTGDN.DT
            If DTJO.Rows.Count > 0 Then
                For Each DTROWPS As DataRow In DTJO.Rows

                    ''CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    'For Each ROW As DataGridViewRow In GRIDJO.Rows
                    '    If DTROWPS("BARCODE") <> "" And LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(DTROWPS("BARCODE")) Then GoTo LINE1
                    'Next

                    GRIDJO.Rows.Add(0, DTROWPS("YARNQUALITY"), DTROWPS("MILLNAME"), DTROWPS("DESIGNNO"), "", "", DTROWPS("COLOR"), DTROWPS("LOTNO"), DTROWPS("LRNO"), Val(DTROWPS("BAGS")), Format(Val(DTROWPS("WT")), "0.00"), Format(Val(DTROWPS("CONES")), "0.00"), DTROWPS("LIFTINGDATE"))
LINE1:
                Next
                getsrno(GRIDJO)
                'total()
                GRIDJO.FirstDisplayedScrollingRowIndex = GRIDJO.RowCount - 1
            End If
            total()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub total()
        Try
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALPCS.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDJO.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(ROW.Cells(GQTY.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.00")
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(sender As Object, e As EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub txtremarks_KeyDown(sender As Object, e As KeyEventArgs) Handles txtremarks.KeyDown
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


    Private Sub TXTDATE_GotFocus(sender As Object, e As EventArgs) Handles TXTDATE.GotFocus
        TXTDATE.SelectionStart = 0

    End Sub

    Private Sub tstxtbillno_Validating(sender As Object, e As CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDJO.RowCount = 0
                TEMPGODOWNNO = Val(tstxtbillno.Text)
                If TEMPGODOWNNO > 0 Then
                    EDIT = True
                    YarnInterGodownTransfer_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    'Private Sub TXTLRNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTLRNO.Validating
    '    Try
    '        If ClientName = "MJFABRIC" Then Exit Sub
    '        If CMBYARNQUALITY.Text.Trim <> "" And Val(TXTWT.Text.Trim) > 0 Then

    '            fillgrid()

    '        Else

    '            If CMBYARNQUALITY.Text.Trim = "" Then
    '                MsgBox("Enter  Yarn Quality", MsgBoxStyle.Critical)
    '                CMBYARNQUALITY.Focus()

    '            ElseIf Val(TXTWT.Text.Trim) = 0 Then
    '                MsgBox("Enter Weight", MsgBoxStyle.Critical)
    '                TXTWT.Focus()
    '            End If
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    ' End Sub

    Private Sub GRIDJO_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDJO.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDJO.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                'end of block
                GRIDJO.Rows.RemoveAt(GRIDJO.CurrentRow.Index)
                getsrno(GRIDJO)
                total()
            ElseIf e.KeyCode = Keys.F5 Then
                'EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDJO_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDJO.CellValidating
        Dim colNum As Integer = GRIDJO.Columns(e.ColumnIndex).Index
        If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return
        Select Case colNum

            Case GQTY.Index, GWT.Index
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                If bValid Then
                    If GRIDJO.CurrentCell.Value = Nothing Then GRIDJO.CurrentCell.Value = "0.00"
                    GRIDJO.CurrentCell.Value = Convert.ToDecimal(GRIDJO.Item(colNum, e.RowIndex).Value)
                    total()
                Else
                    MessageBox.Show("Invalid Number Entered")
                    e.Cancel = True
                    'Exit Sub
                End If


        End Select
    End Sub

    Private Sub YarnInterGodownTransfer_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If ClientName = "SWPL" Then
            GLRNO.HeaderText = "Box No"
        End If
    End Sub
End Class