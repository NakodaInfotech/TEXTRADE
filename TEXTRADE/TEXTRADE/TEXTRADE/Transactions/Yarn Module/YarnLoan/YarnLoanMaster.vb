Imports System.ComponentModel
Imports BL
Imports DevExpress.CodeParser
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraReports.UI
Imports iTextSharp
Public Class YarnLoanMaster
    'following two variables is only for used in edit mode....
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim gridDoubleClick As Boolean
    Dim tempRow As Integer

    Public edit As Boolean
    Public TEMPLOANNO As String
    Public tempMsg As Integer

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Dim IntResult As Integer
        Try
            Cursor.Current = Cursors.WaitCursor

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(txtloanno.Text.Trim)
            alParaval.Add(loandate.Value)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(cmbLoan.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(cmbGodown.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim gridsrno As String = ""
            Dim YARNQUALITY As String = ""
            Dim MILLNAME As String = ""
            Dim JOBBERLOTNO As String = ""
            Dim qty As String = ""
            Dim WT As String = ""
            Dim CONES As String = ""
            Dim LRNO As String = ""
            Dim LRDATE As String = ""
            Dim DONE As String = ""
            Dim OUTBAGS As String = ""
            Dim OUTWT As String = ""
            Dim PONO As String = ""
            Dim POGRIDSRNO As String = ""
            Dim FROMTYPE As String = ""

            Dim RACK As String = ""
            Dim BARCODE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDYARN.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = Val(row.Cells(gsrno.Index).Value)
                        YARNQUALITY = row.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        JOBBERLOTNO = row.Cells(GJOBBERLOTNO.Index).Value.ToString
                        qty = Val(row.Cells(GQTY.Index).Value)
                        WT = Val(row.Cells(GWT.Index).Value)
                        CONES = Val(row.Cells(GCONES.Index).Value)
                        LRNO = row.Cells(GLRNO.Index).Value.ToString
                        LRDATE = Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        If row.Cells(GDONE.Index).Value = True Then DONE = 1 Else DONE = 0
                        OUTBAGS = Val(row.Cells(GOUTBAGS.Index).Value)
                        OUTWT = Val(row.Cells(GOUTWT.Index).Value)
                        PONO = Val(row.Cells(GPONO.Index).Value)
                        POGRIDSRNO = Val(row.Cells(GGRIDSRNO.Index).Value)
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString
                        RACK = row.Cells(GRACK.Index).Value.ToString
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString

                    Else
                        gridsrno = gridsrno & "|" & Val(row.Cells(gsrno.Index).Value)
                        YARNQUALITY = YARNQUALITY & "|" & row.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        JOBBERLOTNO = JOBBERLOTNO & "|" & row.Cells(GJOBBERLOTNO.Index).Value.ToString
                        qty = qty & "|" & Val(row.Cells(GQTY.Index).Value)
                        WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                        CONES = CONES & "|" & Val(row.Cells(GCONES.Index).Value)
                        LRNO = LRNO & "|" & row.Cells(GLRNO.Index).Value
                        LRDATE = LRDATE & "|" & Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        If row.Cells(GDONE.Index).Value = True Then DONE = DONE & "|" & "1" Else DONE = DONE & "|" & "0"
                        OUTBAGS = OUTBAGS & "|" & Val(row.Cells(GOUTBAGS.Index).Value)
                        OUTWT = OUTWT & "|" & Val(row.Cells(GOUTWT.Index).Value)
                        PONO = PONO & "|" & Val(row.Cells(GPONO.Index).Value)
                        POGRIDSRNO = POGRIDSRNO & "|" & Val(row.Cells(GGRIDSRNO.Index).Value)
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value.ToString
                        RACK = RACK & "|" & row.Cells(GRACK.Index).Value.ToString
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(YARNQUALITY)
            alParaval.Add(MILLNAME)
            alParaval.Add(JOBBERLOTNO)
            alParaval.Add(qty)
            alParaval.Add(WT)
            alParaval.Add(CONES)
            alParaval.Add(LRNO)
            alParaval.Add(LRDATE)
            alParaval.Add(DONE)
            alParaval.Add(OUTBAGS)
            alParaval.Add(OUTWT)
            alParaval.Add(PONO)
            alParaval.Add(POGRIDSRNO)
            alParaval.Add(FROMTYPE)
            alParaval.Add(RACK)
            alParaval.Add(BARCODE)


            Dim objclsloan As New ClsYarnLoan()
            objclsloan.alParaval = alParaval

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = objclsloan.SAVE()
                MessageBox.Show("Details Added")
                txtloanno.Text = Val(DT.Rows(0).Item(0))

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPLOANNO)
                IntResult = objclsloan.Update()
                MessageBox.Show("Details Updated")
                edit = False
            End If

            'edit = False
            'Dim TEMPMSG As Integer
            'TEMPMSG = MsgBox("WISH TO PRINT", MsgBoxStyle.YesNo)

            If TEMPMSG = vbYes Then
                'Dim OBJGN As New LoanDesign
                ''OBJGN.loanNO = txtloanno.Text
                ''OBJGN.MdiParent = MDIMain
                ''OBJGN.selfor_po = "{loanMaster.loan_no}=" & txtloanno.Text & " and {loanMaster.loan_cmpid}=" & CmpId & " and {loanMaster.loan_locationid}=" & Locationid & " and {loanMaster.loan_yearid}=" & YearId
                'OBJGN.Show()
            End If
            edit = False
            clear()
            cmbname.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True
            If cmbname.Text.Trim.Length = 0 Then
                EP.SetError(cmbname, " Please Fill Company Name ")
                bln = False
            End If
            If cmbGodown.Text.Trim.Length = 0 Then
                EP.SetError(cmbGodown, " Please Fill Godown Name ")
                bln = False
            End If
            If GRIDYARN.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If
            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        Try
            clear()
            edit = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(sender As Object, e As EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then FILLNAME(cmbname, edit, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(sender As Object, e As CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then NAMEVALIDATE(cmbname, CMBCODE, e, Me, txtadd, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'  or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS') ", "Sundry Creditors")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_Enter(sender As Object, e As EventArgs) Handles CMBYARNQUALITY.Enter
        Try
            If CMBYARNQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBYARNQUALITY, edit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBYARNQUALITY.Validating
        Try
            If CMBYARNQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBYARNQUALITY, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbGodown_Validating(sender As Object, e As CancelEventArgs) Handles cmbGodown.Validating
        Try
            If cmbGodown.Text.Trim <> "" Then GODOWNVALIDATE(cmbGodown, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbGodown_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbGodown.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectGodown
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then cmbGodown.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbGodown_Enter(sender As Object, e As EventArgs) Handles cmbGodown.Enter
        Try
            If cmbGodown.Text.Trim = "" Then fillGODOWN(cmbGodown, edit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub fillcmb()
        Try
            If cmbGodown.Text.Trim = "" Then fillGODOWN(cmbGodown, edit)
            If cmbname.Text.Trim = "" Then FILLNAME(cmbname, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
            If CMBCODE.Text.Trim = "" Then fillACCCODE(CMBCODE, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")

            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT'")

            fillYARNQUALITY(CMBYARNQUALITY, edit)
            FILLMILL(CMBMILL, edit)
            FILLRACK(cmbrack)


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then NAMEVALIDATE(cmbtrans, CMBCODE, e, Me, TXTTRANSADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub cmbtrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtrans.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
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
    Private Sub txtremarks_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtremarks.KeyDown
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
    Private Sub CMBMILL_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then FILLMILL(CMBMILL, edit)
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
    Private Sub TXTCONES_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCONES.KeyPress
        numkeypress(e, TXTCONES, Me)
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdot(e, TXTWT, Me)
    End Sub
    Sub clear()
        tstxtbillno.Clear()
        cmbLoan.Text = ""
        loandate.Value = Mydate
        EP.Clear()
        txtsrno.Clear()
        txtqty.Clear()
        txtremarks.Clear()
        cmbname.Text = ""
        gridDoubleClick = False
        getmax_loan_no()
        CMBYARNQUALITY.Text = ""
        If USERGODOWN <> "" Then cmbGodown.Text = USERGODOWN Else cmbGodown.Text = ""
        txtadd.Clear()
        cmbtrans.Text = ""
        txtremarks.Clear()
        cmbrack.Text = ""
        txtsrno.Text = 1
        CMBYARNQUALITY.Text = ""
        CMBMILL.Text = ""
        TXTJOBBERLOTNO.Clear()
        TXTGRIDLRNO.Clear()
        TXTWT.Clear()
        TXTCONES.Clear()
        DTLRDATE.Value = Now.Date
        GRIDYARN.RowCount = 0
        LBLTOTALCONES.Text = 0
        LBLTOTALWT.Text = 0
        lbltotalbags.Text = 0

        cmbLoan.Enabled = True

        Label11.Visible = False
        TXTMASTERBARCODE.Visible = False


        txtsrno.Visible = True
        CMBYARNQUALITY.Visible = True
        CMBMILL.Visible = True
        TXTJOBBERLOTNO.Visible = True
        txtqty.Visible = True
        TXTWT.Visible = True
        TXTCONES.Visible = True
        TXTGRIDLRNO.Visible = True
        DTLRDATE.Visible = True
        cmbrack.Visible = True


        lbllocked.Visible = False
        PBlock.Visible = False


    End Sub

    Private Sub cmbrack_Enter(sender As Object, e As EventArgs) Handles cmbrack.Enter
        Try
            If cmbrack.Text = "" Then FILLRACK(cmbrack)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbrack_Validating(sender As Object, e As CancelEventArgs) Handles cmbrack.Validating
        Try
            If cmbrack.Text.Trim <> "" Then RACKVALIDATE(cmbrack, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub getmax_loan_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(YARNloan_no),0) + 1 ", "YARNLOAN", " AND YARNloan_cmpid=" & CmpId & " and YARNloan_LOCATIONID=" & Locationid & " and YARNloan_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            txtloanno.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub YarnLoanMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Cursor.Current = Cursors.WaitCursor

            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'YARN RECD'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            fillcmb()
            clear()

            If edit = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim objclsYARN As New ClsYarnLoan()
                Dim dttable As New DataTable

                dttable = objclsYARN.selectLoan(TEMPloanNO, CmpId, Locationid, YearId)

                If dttable.Rows.Count > 0 Then
                    For Each dr As DataRow In dttable.Rows

                        txtloanno.Text = TEMPloanNO
                        loandate.Value = Convert.ToDateTime(dr("DATE"))
                        cmbname.Text = Convert.ToString(dr("PARTYNAME"))
                        cmbLoan.Text = Convert.ToString(dr("TYPE").ToString)
                        txtremarks.Text = Convert.ToString(dr("REMARKS"))
                        cmbtrans.Text = Convert.ToString(dr("TRANSPORT"))
                        cmbGodown.Text = Convert.ToString(dr("GODOWN"))
                        GRIDYARN.Rows.Add(Val(dr("GRIDSRNO")), dr("YARNNAME"), dr("MILLNAME"), dr("LOTNO"), Format(dr("BAGS"), "0"), Format(dr("WT"), "0.00"), Format(dr("CONES"), "0.00"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), dr("DONE").ToString, Val(dr("OUTBAGS")), Val(dr("OUTWT")), dr("FROMNO").ToString, dr("FROMSRNO").ToString, dr("FROMTYPE").ToString, dr("RACK").ToString, dr("BARCODE").ToString)

                        If Val(dr("OUTWT")) > 0 Then
                            GRIDYARN.Rows(GRIDYARN.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                    Next
                    GRIDYARN.FirstDisplayedScrollingRowIndex = GRIDYARN.RowCount - 1
                End If

                chkchange.CheckState = CheckState.Checked
                total()
                cmbLoan.Enabled = False

                If cmbLoan.Text = "Loan Return to Party" Or cmbLoan.Text = "Party taking Loan" Then
                    Label11.Visible = True
                    TXTMASTERBARCODE.Visible = True

                    txtsrno.Visible = False
                    CMBYARNQUALITY.Visible = False
                    CMBMILL.Visible = False
                    TXTJOBBERLOTNO.Visible = False
                    txtqty.Visible = False
                    TXTWT.Visible = False
                    TXTCONES.Visible = False
                    TXTGRIDLRNO.Visible = False
                    DTLRDATE.Visible = False
                    cmbrack.Visible = False
                End If

            End If

            'If gridDoubleClick = False Then
            If GRIDYARN.RowCount > 0 Then
                txtsrno.Text = Val(GRIDYARN.Rows(GRIDYARN.RowCount - 1).Cells(gsrno.Index).Value) + 1
            Else
                txtsrno.Text = 1
            End If
            'End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Sub getmaxno()
        Dim DTTABLE As DataTable = getmax(" isnull(max(YARNLOAN_no),0) + 1 ", "YARNLOAN", " and YARNLOAN_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then txtloanno.Text = DTTABLE.Rows(0).Item(0)
    End Sub
    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(gsrno.Index).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try

            If edit = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If


                If lbllocked.Visible = True Or lbllocked.Visible = True Then
                    MsgBox("Yarn Loan Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete Entry?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(txtloanno.Text.Trim)
                    alParaval.Add(CmpId)
                    alParaval.Add(Locationid)
                    alParaval.Add(Userid)
                    alParaval.Add(YearId)

                    Dim Clsgrn As New ClsYarnLoan()
                    Clsgrn.alParaval = alParaval
                    IntResult = Clsgrn.Delete()
                    MsgBox("Yarn Deleted")
                    clear()
                    edit = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
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

            GRIDYARN.RowCount = 0
LINE1:
            TEMPloanNO = Val(txtloanno.Text) - 1
            If TEMPloanNO > 0 Then
                edit = True
                YarnLoanMaster_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If GRIDYARN.RowCount = 0 And TEMPloanNO > 1 Then
                txtloanno.Text = TEMPloanNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            GRIDYARN.RowCount = 0
LINE1:
            TEMPloanNO = Val(txtloanno.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = txtloanno.Text.Trim
            clear()
            If Val(txtloanno.Text) - 1 >= TEMPloanNO Then
                edit = True
                YarnLoanMaster_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If GRIDYARN.RowCount = 0 And TEMPloanNO < MAXNO Then
                txtloanno.Text = TEMPloanNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(sender As Object, e As EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete.PerformClick()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Try
            Call cmdok.PerformClick()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        GRIDYARN.Enabled = True
        If gridDoubleClick = False Then

            If gridDoubleClick = False Then
                If edit = True Then
                    'GET LAST BARCODE SRNO
                    Dim LSRNO As Integer = 0
                    Dim RSRNO As Integer = 0
                    Dim SNO As Integer = 0
                    LSRNO = InStr(GRIDYARN.Rows(GRIDYARN.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                    RSRNO = InStr(LSRNO + 1, GRIDYARN.Rows(GRIDYARN.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                    SNO = GRIDYARN.Rows(GRIDYARN.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                    If cmbLoan.Text = "Loan taken from Party" Then
                        txtbarcode.Text = "LTP-" & Val(txtloanno.Text.Trim) & "/" & SNO + 1 & "/" & YearId

                    ElseIf cmbLoan.Text = "Party Returning Loan" Then
                        txtbarcode.Text = "PRL-" & Val(txtloanno.Text.Trim) & "/" & SNO + 1 & "/" & YearId

                    Else
                        txtbarcode.Text = ""
                    End If

                Else


                    If cmbLoan.Text = "Loan taken from Party" Then
                        txtbarcode.Text = "LTP-" & Val(txtloanno.Text.Trim) & "/" & GRIDYARN.RowCount + 1 & "/" & YearId

                    ElseIf cmbLoan.Text = "Party Returning Loan" Then
                        txtbarcode.Text = "PRL-" & Val(txtloanno.Text.Trim) & "/" & GRIDYARN.RowCount + 1 & "/" & YearId

                    Else
                        txtbarcode.Text = ""
                    End If

                End If
            End If

            GRIDYARN.Rows.Add(Val(txtsrno.Text.Trim), CMBYARNQUALITY.Text.Trim, CMBMILL.Text.Trim, TXTJOBBERLOTNO.Text.Trim, Format(Val(txtqty.Text.Trim), "0"), Format(Val(TXTWT.Text.Trim), "0.00"), Format(Val(TXTCONES.Text.Trim), "0"), TXTGRIDLRNO.Text.Trim, Format(DTLRDATE.Value.Date, "dd/MM/yyyy"), 0, 0, 0, 0, 0, "", cmbrack.Text.Trim, txtbarcode.Text.Trim)


            getsrno(GRIDYARN)
        ElseIf gridDoubleClick = True Then
            GRIDYARN.Item(gsrno.Index, tempRow).Value = Val(txtsrno.Text.Trim)
            GRIDYARN.Item(GYARNQUALITY.Index, tempRow).Value = CMBYARNQUALITY.Text.Trim
            GRIDYARN.Item(GMILLNAME.Index, tempRow).Value = CMBMILL.Text.Trim
            GRIDYARN.Item(GJOBBERLOTNO.Index, tempRow).Value = TXTJOBBERLOTNO.Text.Trim
            GRIDYARN.Item(GQTY.Index, tempRow).Value = Format(Val(txtqty.Text.Trim), "0")
            GRIDYARN.Item(GWT.Index, tempRow).Value = Format(Val(TXTWT.Text.Trim), "0.000")

            GRIDYARN.Item(GCONES.Index, tempRow).Value = Format(Val(TXTCONES.Text.Trim), "0")
            GRIDYARN.Item(GLRNO.Index, tempRow).Value = TXTGRIDLRNO.Text.Trim
            GRIDYARN.Item(GLRDATE.Index, tempRow).Value = Format(DTLRDATE.Value.Date, "dd/MM/yyyy")
            GRIDYARN.Item(GRACK.Index, tempRow).Value = cmbrack.Text.Trim
            GRIDYARN.Item(GBARCODE.Index, tempRow).Value = txtbarcode.Text.Trim

            gridDoubleClick = False

        End If

        total()

        GRIDYARN.FirstDisplayedScrollingRowIndex = GRIDYARN.RowCount - 1


        If ClientName <> "VAISHALI" Then CMBYARNQUALITY.Text = ""
        CMBMILL.Text = ""
        TXTJOBBERLOTNO.Clear()
        txtqty.Clear()
        TXTWT.Clear()
        TXTCONES.Clear()
        TXTGRIDLRNO.Clear()
        DTLRDATE.Value = Now.Date
        txtbarcode.Clear()
        txtsrno.Text = Val(GRIDYARN.Rows(GRIDYARN.RowCount - 1).Cells(0).Value) + 1
        CMBYARNQUALITY.Focus()
        cmbrack.Text = ""
        txtbarcode.Clear()


        gridDoubleClick = False

        If GRIDYARN.RowCount > 0 Then
            txtsrno.Text = Val(GRIDYARN.Rows(GRIDYARN.RowCount - 1).Cells(gsrno.Index).Value) + 1
        Else
            txtsrno.Text = 1
        End If
        txtsrno.Focus()

    End Sub
    Sub total()
        lbltotalbags.Text = "0.00"
        LBLTOTALCONES.Text = "0.00"
        LBLTOTALWT.Text = "0.00"
        For Each row As DataGridViewRow In GRIDYARN.Rows
            If Val(row.Cells(GQTY.Index).Value) <> 0 Then
                lbltotalbags.Text = Val(lbltotalbags.Text) + Val(row.Cells(GQTY.Index).Value)
                LBLTOTALCONES.Text = Val(LBLTOTALCONES.Text) + Val(row.Cells(GCONES.Index).Value)
                LBLTOTALWT.Text = Val(LBLTOTALWT.Text) + Val(row.Cells(GWT.Index).Value)
            End If
        Next
    End Sub

    Private Sub cmbrack_Validated(sender As Object, e As EventArgs) Handles cmbrack.Validated
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDYARN_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDYARN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDYARN.RowCount > 0 Then


                If Convert.ToBoolean(GRIDYARN.Rows(GRIDYARN.CurrentRow.Index).Cells(GDONE.Index).Value) = True Or GRIDYARN.Rows(GRIDYARN.CurrentRow.Index).Cells(GOUTWT.Index).Value > 0 Then
                    MsgBox("Item Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If gridDoubleClick = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDYARN.Rows.RemoveAt(GRIDYARN.CurrentRow.Index)
                getsrno(GRIDYARN)
                total()
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            ElseIf e.KeyCode = Keys.F12 And GRIDYARN.RowCount > 0 Then
                'If gridgrn.CurrentRow.Cells(gitemname.Index).Value <> "" Then
                '    gridgrn.Rows.Add(CloneWithValues(gridgrn.CurrentRow))
                '    getsrno(gridgrn)
                '    total()
                'End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub EDITROW()
        Try
            If GRIDYARN.CurrentRow.Index >= 0 And GRIDYARN.Item(gsrno.Index, GRIDYARN.CurrentRow.Index).Value <> Nothing Then

                If Convert.ToBoolean(GRIDYARN.Rows(GRIDYARN.CurrentRow.Index).Cells(GDONE.Index).Value) = True Or GRIDYARN.Rows(GRIDYARN.CurrentRow.Index).Cells(GOUTWT.Index).Value > 0 Then
                    MsgBox("Item Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                gridDoubleClick = True
                txtsrno.Text = GRIDYARN.Item(gsrno.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                CMBYARNQUALITY.Text = GRIDYARN.Item(GYARNQUALITY.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                CMBMILL.Text = GRIDYARN.Item(GMILLNAME.Index, GRIDYARN.CurrentRow.Index).Value.ToString

                TXTJOBBERLOTNO.Text = GRIDYARN.Item(GJOBBERLOTNO.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                txtqty.Text = GRIDYARN.Item(GQTY.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                TXTWT.Text = GRIDYARN.Item(GWT.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                TXTCONES.Text = GRIDYARN.Item(GCONES.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                TXTGRIDLRNO.Text = GRIDYARN.Item(GLRNO.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                DTLRDATE.Text = GRIDYARN.Item(GLRDATE.Index, GRIDYARN.CurrentRow.Index).Value
                cmbrack.Text = GRIDYARN.Item(GRACK.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                txtbarcode.Text = GRIDYARN.Item(GBARCODE.Index, GRIDYARN.CurrentRow.Index).Value.ToString

                tempRow = GRIDYARN.CurrentRow.Index
                CMBYARNQUALITY.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDYARN_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDYARN.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub txtbarcode_Validated(sender As Object, e As EventArgs) Handles txtbarcode.Validated
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtbarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtbarcode.KeyDown

    End Sub

    Private Sub TXTMASTERBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTMASTERBARCODE.Validated
        Try
            If txtbarcode.Text.Trim.Length > 0 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 * ", "", "YARNBARCODESTOCK",
                " AND BARCODE = '" & txtbarcode.Text.Trim & "' AND DONE = 0 AND YEARID = " & YearId)

                If DT.Rows.Count > 0 Then
                    ' CHECK: Barcode already in GRIDYARN (DataGridView)?
                    For I As Integer = 0 To GRIDYARN.RowCount - 1
                        If GRIDYARN.Rows(I).Cells(GBARCODE.Index).Value IsNot Nothing Then
                            If LCase(GRIDYARN.Rows(I).Cells(GBARCODE.Index).Value.ToString) =
                           LCase(txtbarcode.Text.Trim) Then
                                MsgBox("Barcode already exists in grid!", MsgBoxStyle.Information)
                                GoTo LINE1
                            End If
                        End If
                    Next

                    ' ADD ROW to GRIDYARN (same format as Load event)
                    Dim dr As DataRow = DT.Rows(0)
                    GRIDYARN.Rows.Add(GRIDYARN.RowCount, dr("YARNQUALITY").ToString, dr("MILLNAME").ToString, dr("LOTNO").ToString, Format(dr("BAGS"), "0"), Format(dr("WT"), "0.00"), Format(dr("CONES"), "0.00"), dr("LRNO").ToString, Format(DTLRDATE.Value.Date, "dd/MM/yyyy"), dr("DONE").ToString, 0, 0, dr("FROMNO").ToString, dr("FROMSRNO").ToString, dr("FROMTYPE").ToString, dr("RACK").ToString, dr("BARCODE").ToString)
                    GRIDYARN.FirstDisplayedScrollingRowIndex = GRIDYARN.RowCount - 1
                    total()
LINE1:
                    txtbarcode.Clear()
                    txtbarcode.Focus()
                    getsrno(GRIDYARN)
                Else
                    MsgBox("Barcode not found or already used!", MsgBoxStyle.Exclamation)
                    txtbarcode.Clear()
                    txtbarcode.Focus()
                End If
            End If
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

            Dim OBJEMB As New YarnLoanMasterDetails
            OBJEMB.MdiParent = MDIMain
            OBJEMB.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMASTERBARCODE_KeyDown(sender As Object, e As KeyEventArgs) Handles TXTMASTERBARCODE.KeyDown
        Try
            If e.KeyCode = Keys.F1 And ALLOWBARCODEPRINT = True And ALLOWPACKINGSLIP = False Then
                If (ClientName = "MAHAVIRPOLYCOT" Or ClientName = "SNCM") And UserName <> "Admin" Then Exit Sub

                Dim OBJSTOCK As New SelectYarnStock
                ' OBJSTOCK.WHERECLAUSE = OBJSTOCK.WHERECLAUSE & " AND GODOWN = '" & CMBGODOWN.Text.Trim & "'"
                OBJSTOCK.ShowDialog()
                Dim DTBARCODE As DataTable = OBJSTOCK.DT
                For Each DTROW As DataRow In DTBARCODE.Rows
                    txtbarcode.Text = DTROW("BARCODE")
                    TXTMASTERBARCODE_Validated(sender, e)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbLoan_Validated(sender As Object, e As EventArgs) Handles cmbLoan.Validated
        Try
            cmbLoan.Enabled = False
            If cmbLoan.Text = "Loan Return to Party" Or cmbLoan.Text = "Party taking Loan" Then
                Label11.Visible = True
                TXTMASTERBARCODE.Visible = True

                txtsrno.Visible = False
                CMBYARNQUALITY.Visible = False
                CMBMILL.Visible = False
                TXTJOBBERLOTNO.Visible = False
                txtqty.Visible = False
                TXTWT.Visible = False
                TXTCONES.Visible = False
                TXTGRIDLRNO.Visible = False
                DTLRDATE.Visible = False
                cmbrack.Visible = False

            End If

        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Sub YarnLoanMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
        ElseIf e.KeyCode = Keys.Oemcomma Then
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
        ElseIf e.KeyCode = Keys.F5 Then     'grid focus
            YarnRecd.Focus()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_CLICK(sender, e)
        End If
    End Sub

    Private Sub tstxtbillno_Validating(sender As Object, e As CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDYARN.RowCount = 0
                TEMPLOANNO = Val(tstxtbillno.Text)
                If TEMPLOANNO > 0 Then
                    edit = True
                    YarnLoanMaster_Load(sender, e)
                Else
                    clear()
                    edit = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class