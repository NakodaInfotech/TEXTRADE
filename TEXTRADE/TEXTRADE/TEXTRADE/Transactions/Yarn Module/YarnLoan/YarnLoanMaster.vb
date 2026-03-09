Imports System.ComponentModel
Imports BL
Imports DevExpress.CodeParser
Imports DevExpress.XtraReports.UI
Imports iTextSharp
Public Class YarnLoanMaster
    'following two variables is only for used in edit mode....
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim gridDoubleClick As Boolean
    Dim tempRow As Integer

    Public edit As Boolean
    Public TEMPloanNO As String
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
            Dim PONO As String = ""
            Dim POGRIDSRNO As String = ""
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
                        PONO = Val(row.Cells(GPONO.Index).Value)
                        POGRIDSRNO = Val(row.Cells(GGRIDSRNO.Index).Value)
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
                        PONO = PONO & "|" & Val(row.Cells(GPONO.Index).Value)
                        POGRIDSRNO = POGRIDSRNO & "|" & Val(row.Cells(GGRIDSRNO.Index).Value)
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
            alParaval.Add(PONO)
            alParaval.Add(POGRIDSRNO)
            alParaval.Add(RACK)
            alParaval.Add(BARCODE)


            Dim objclsloan As New ClsStoresLoan
            objclsloan.alParaval = alParaval

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objclsloan.save()
                MessageBox.Show("Details Added")
            Else
                alParaval.Add(TEMPloanNO)
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objclsloan.Update()
                MsgBox("Details Updated")
            End If
            edit = False
            Dim TEMPMSG As Integer
            TEMPMSG = MsgBox("WISH TO PRINT", MsgBoxStyle.YesNo)

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
            If cmbrack.Text = "" Then RACKVALIDATE(cmbrack, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub getmax_loan_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(STORloan_no),0) + 1 ", "STORESLOAN", " AND STORloan_cmpid=" & CmpId & " and STORloan_LOCATIONID=" & Locationid & " and STORloan_YEARID=" & YearId)
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
                        GRIDYARN.Rows.Add(Val(dr("GRIDSRNO")), dr("YARNNAME"), dr("MILLNAME"), dr("LOTNO"), Format(dr("BAGS"), "0"), Format(dr("WT"), "0.00"), Format(dr("CONES"), "0.00"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), dr("DONE").ToString, dr("FROMNO").ToString, dr("FROMSRNO").ToString, dr("RACK").ToString, dr("BARCODE").ToString)
                    Next
                    GRIDYARN.FirstDisplayedScrollingRowIndex = GRIDYARN.RowCount - 1
                End If

                chkchange.CheckState = CheckState.Checked
                total()
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

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(gsrno.Index).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub fillgrid()
        GRIDYARN.Enabled = True
        If gridDoubleClick = False Then
            GRIDYARN.Rows.Add(Val(txtsrno.Text.Trim), CMBYARNQUALITY.Text.Trim, CMBMILL.Text.Trim, TXTJOBBERLOTNO.Text.Trim, Format(Val(txtqty.Text.Trim), "0.00"), Format(Val(TXTWT.Text.Trim), "0.00"), Format(Val(TXTCONES.Text.Trim), "0.00"), TXTGRIDLRNO.Text.Trim, Format(DTLRDATE.Value.Date, "dd/MM/yyyy"), 0, 0, 0, 0, 0, cmbrack.Text.Trim, txtbarcode.Text.Trim)
            getsrno(GRIDYARN)
        ElseIf gridDoubleClick = True Then
            GRIDYARN.Item(gsrno.Index, tempRow).Value = Val(txtsrno.Text.Trim)
            GRIDYARN.Item(GYARNQUALITY.Index, tempRow).Value = CMBYARNQUALITY.Text.Trim
            GRIDYARN.Item(GMILLNAME.Index, tempRow).Value = CMBMILL.Text.Trim
            GRIDYARN.Item(GJOBBERLOTNO.Index, tempRow).Value = TXTJOBBERLOTNO.Text.Trim
            GRIDYARN.Item(GQTY.Index, tempRow).Value = Format(Val(txtqty.Text.Trim), "0.00")
            GRIDYARN.Item(GWT.Index, tempRow).Value = Format(Val(TXTWT.Text.Trim), "0.00")

            GRIDYARN.Item(GCONES.Index, tempRow).Value = Format(Val(TXTCONES.Text.Trim), "0.00")
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
End Class