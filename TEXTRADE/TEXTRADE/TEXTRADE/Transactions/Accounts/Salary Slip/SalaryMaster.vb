
Imports System.ComponentModel
Imports BL

Public Class SalaryMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Public TEMPSALNO As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim GRIDEARDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Dim TEMPEARROW As Integer
    Dim TEMPJVNO As Integer = 0

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub FILLCMB()
        Try
            FILLEMP(CMBEMPNAME, EDIT)
            fillledger(CMBEARNINGS, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income'  OR GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances')")
            fillledger(CMBDEDUCTION, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income'  OR GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances') ")
            fillledger(CMBNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') ")
            fillledger(CMBLOANNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'Loans' or GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances' or GROUPMASTER.GROUP_SECONDARY = 'Secured Loans'  or GROUPMASTER.GROUP_SECONDARY = 'Unsecured Loans') ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False
        CMBEMPNAME.Focus()
    End Sub

    Sub clear()

        EP.Clear()
        tstxtbillno.Clear()

        TXTSALNO.Clear()
        'SALDATE.Text = Now.Date
        'Dim dt As DataTable
        'Dim OBJAL As New ClsSalaryMaster()
        'dt = OBJAL.selectbill_edit()
        'SALDATE.Text = Convert.ToDateTime(dt.Rows(0).Item("SALDATE")).Date

        CMBEMPNAME.Text = ""
        CMBEMPNAME.Enabled = True
        'CMBMONTH.SelectedIndex = 0
        TXTWORKDAYS.Clear()
        TXTPAYDAYS.Clear()
        TXTLEAVE.Clear()

        CMBNAME.Text = ""
        CMBLOANNAME.Text = ""
        TXTLEDGERBAL.Clear()
        TXTLOANBAL.Clear()
        TXTADVTAKEN.Clear()

        TXTTOTALDED.Clear()
        TXTTOTALEAR.Clear()
        TXTNETTPAY.Clear()

        TXTEARSRNO.Clear()
        CMBEARNINGS.Text = ""
        TXTEARAMT.Clear()
        GRIDEAR.RowCount = 0

        TXTDEDSRNO.Clear()
        CMBDEDUCTION.Text = ""
        TXTDEDAMT.Clear()
        GRIDDED.RowCount = 0

        txtremarks.Clear()
        txtinwords.Clear()

        GETMAXNO_SALNO()

        lbllocked.Visible = False
        PBlock.Visible = False
        TXTLOANEMI.Clear()


    End Sub

    Sub GETMAXNO_SALNO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(SAL_NO),0) + 1 ", "SALARYMASTER", " AND SAL_cmpid=" & CmpId & " AND SAL_YEARid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            TXTSALNO.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub SalaryMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.F2 Then
                tstxtbillno.Focus()
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolprevious_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_Click(sender, e)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJSAL As New SalaryDetails
            OBJSAL.MdiParent = MDIMain
            OBJSAL.Show()
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub SalaryMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'PAYROLL'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            clear()
            FILLCMB()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dt As New DataTable
                Dim OBJSAL As New ClsSalaryMaster()
                Dim ALPARAVAL As New ArrayList

                ALPARAVAL.Add(TEMPSALNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(YearId)

                OBJSAL.alParaval = ALPARAVAL
                dt = OBJSAL.selectbill_edit()

                If dt.Rows.Count > 0 Then

                    TXTSALNO.Text = TEMPSALNO
                    SALDATE.Text = Convert.ToDateTime(dt.Rows(0).Item("SALDATE")).Date

                    CMBEMPNAME.Text = dt.Rows(0).Item("EMPNAME")
                    CMBEMPNAME.Enabled = False

                    CMBMONTH.Text = dt.Rows(0).Item("MONTH")

                    TXTWORKDAYS.Text = dt.Rows(0).Item("WORKDAYS")
                    TXTPAYDAYS.Text = dt.Rows(0).Item("PAYDAYS")
                    TXTLEAVE.Text = dt.Rows(0).Item("LEAVE")
                    TXTTOTALDED.Text = dt.Rows(0).Item("TOTALDED")
                    TXTTOTALEAR.Text = dt.Rows(0).Item("TOTALEAR")
                    TXTNETTPAY.Text = dt.Rows(0).Item("NETPAY")

                    txtremarks.Text = Convert.ToString(dt.Rows(0).Item("REMARKS"))
                    CMBNAME.Text = dt.Rows(0).Item("NAME")
                    CMBLOANNAME.Text = dt.Rows(0).Item("LOANNAME")
                    TXTADVTAKEN.Text = dt.Rows(0).Item("ADVANCETAKEN")
                    TXTLOANEMI.Text = dt.Rows(0).Item("LOANEMI")

                    Dim OBJCMN As New ClsCommon
                    dt = OBJCMN.search(" SALARYMASTER_DEDUCTION.SAL_SRNO AS SRNO, LEDGERS.Acc_cmpname AS DEDUCTION, SALARYMASTER_DEDUCTION.SAL_AMT AS DEDAMT ", "", " SALARYMASTER_DEDUCTION INNER JOIN LEDGERS ON SALARYMASTER_DEDUCTION.SAL_DEDID = LEDGERS.Acc_id ", " AND SAL_NO = " & TEMPSALNO & " AND SAL_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        For Each DTDED As DataRow In dt.Rows
                            GRIDDED.Rows.Add(DTDED("SRNO"), DTDED("DEDUCTION"), Format(Val(DTDED("DEDAMT")), "0.00"))
                        Next
                    End If

                    dt = OBJCMN.search(" SALARYMASTER_EARNINGS.SAL_SRNO AS SRNO, LEDGERS.Acc_cmpname AS EARNINGS, SALARYMASTER_EARNINGS.SAL_AMT AS EARAMT ", "", " SALARYMASTER_EARNINGS INNER JOIN LEDGERS ON SALARYMASTER_EARNINGS.SAL_EARID = LEDGERS.Acc_id ", " AND SAL_NO = " & TEMPSALNO & " AND SAL_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        For Each DTEAR As DataRow In dt.Rows
                            GRIDEAR.Rows.Add(DTEAR("SRNO"), DTEAR("EARNINGS"), Format(Val(DTEAR("EARAMT")), "0.00"))
                        Next
                    End If

                    TOTAL()
                Else
                    EDIT = True
                    clear()
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            DELETE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            'used to assign gridsrno in receiptgrid
            getsrno(GRIDDED)
            getsrno(GRIDEAR)

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(SALDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBEMPNAME.Text.Trim)
            alParaval.Add(CMBMONTH.Text.Trim)
            alParaval.Add(Val(TXTWORKDAYS.Text.Trim))
            alParaval.Add(Val(TXTPAYDAYS.Text.Trim))
            alParaval.Add(Val(TXTLEAVE.Text.Trim))

            alParaval.Add(Val(TXTTOTALDED.Text.Trim))
            alParaval.Add(Val(TXTTOTALEAR.Text.Trim))
            alParaval.Add(Val(TXTNETTPAY.Text.Trim))



            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(txtinwords.Text.Trim)


            Dim DEDGRIDSRNO As String = ""
            Dim DEDUCTION As String = ""
            Dim DEDAMT As String = ""

            Dim EARGRIDSRNO As String = ""
            Dim EARNINGS As String = ""
            Dim EARAMT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDDED.Rows
                If row.Cells(GDEDSRNO.Index).Value <> Nothing Then
                    If DEDGRIDSRNO = "" Then

                        DEDGRIDSRNO = row.Cells(GDEDSRNO.Index).Value.ToString
                        DEDUCTION = row.Cells(GDEDUCTION.Index).Value
                        DEDAMT = row.Cells(GDEDAMT.Index).Value.ToString

                    Else

                        DEDGRIDSRNO = DEDGRIDSRNO & "|" & row.Cells(GDEDSRNO.Index).Value.ToString
                        DEDUCTION = DEDUCTION & "|" & row.Cells(GDEDUCTION.Index).Value
                        DEDAMT = DEDAMT & "|" & row.Cells(GDEDAMT.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(DEDGRIDSRNO)
            alParaval.Add(DEDUCTION)
            alParaval.Add(DEDAMT)



            For Each row As Windows.Forms.DataGridViewRow In GRIDEAR.Rows
                If row.Cells(GEARSRNO.Index).Value <> Nothing Then
                    If EARGRIDSRNO = "" Then

                        EARGRIDSRNO = row.Cells(GEARSRNO.Index).Value.ToString
                        EARNINGS = row.Cells(GEARNINGS.Index).Value
                        EARAMT = row.Cells(GEARAMT.Index).Value.ToString

                    Else

                        EARGRIDSRNO = EARGRIDSRNO & "|" & row.Cells(GEARSRNO.Index).Value.ToString
                        EARNINGS = EARNINGS & "|" & row.Cells(GEARNINGS.Index).Value
                        EARAMT = EARAMT & "|" & row.Cells(GEARAMT.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(EARGRIDSRNO)
            alParaval.Add(EARNINGS)
            alParaval.Add(EARAMT)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBLOANNAME.Text.Trim)
            alParaval.Add(TXTADVTAKEN.Text.Trim)

            alParaval.Add(TXTLOANEMI.Text.Trim)

            Dim OBJSAL As New ClsSalaryMaster
            OBJSAL.alParaval = alParaval
            Dim DT As DataTable
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                DT = OBJSAL.save()
                MessageBox.Show("Details Added")
                TXTSALNO.Text = Val(DT.Rows(0).Item(0))
                AUTOJVLEDGER()
                If GRIDDED.RowCount > 0 Then AUTOJVDEDUCTION()
                If Val(TXTLOANEMI.Text.Trim) > 0 Then AUTOJVLOAN()
                PRINTREPORT(DT.Rows(0).Item(0))
            Else
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPSALNO)
                Dim INT As Integer = OBJSAL.update()
                MsgBox("Details Updated")
                PRINTREPORT(TEMPSALNO)
            End If

            clear()
            EDIT = False
            CMBEMPNAME.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AUTOJVLEDGER()
        Try
            TEMPJVNO = 0

            'save entry in journal
            Dim alParaval As New ArrayList
            alParaval.Add(0)
            alParaval.Add("JOURNAL REGISTER")
            alParaval.Add(Format(Convert.ToDateTime(SALDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(Val(TXTTOTALEAR.Text.Trim))    'TOTALDR
            alParaval.Add(Val(TXTTOTALEAR.Text.Trim))    'TOTALCR
            alParaval.Add("Against Bill No " & TXTSALNO.Text.Trim & " Bill Dt. " & SALDATE.Text)   'FOR REMARKS
            alParaval.Add("Against Bill No " & TXTSALNO.Text.Trim & " Bill Dt. " & SALDATE.Text)   'FOR BILLREMARKS
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim type As String = ""
            Dim name As String = ""
            Dim paytype As String = ""
            Dim refno As String = ""
            Dim debit As String = ""
            Dim credit As String = ""
            Dim gridsrno As String = ""

            For I As Integer = 0 To 1
                If type = "" Then
                    type = "Dr"
                    name = GRIDEAR.Rows(0).Cells(GEARNINGS.Index).Value
                    paytype = "On Account"
                    refno = ""
                    debit = Val(TXTTOTALEAR.Text.Trim)
                    credit = 0
                    gridsrno = 1
                Else
                    type = type & "|" & "Cr"
                    name = name & "|" & CMBNAME.Text.Trim
                    paytype = paytype & "|" & "New Ref."

                    'GET THE LATEST JOURNALNO
                    Dim OBJCMN As New ClsCommon
                    Dim DTJV As DataTable = OBJCMN.search("isnull(MAX(JOURNAL_NO), 0) + 1 AS JVNO", "", " JOURNALMASTER INNER JOIN REGISTERMASTER ON JOURNAL_REGISTERID = REGISTERMASTER.REGISTER_ID ", " AND REGISTERMASTER.REGISTER_NAME = 'JOURNAL REGISTER' AND JOURNALMASTER.JOURNAL_YEARID = " & YearId)

                    refno = refno & "|JV-" & Val(DTJV.Rows(0).Item("JVNO"))
                    debit = debit & "|" & 0
                    credit = credit & "|" & Val(TXTTOTALEAR.Text.Trim)
                    gridsrno = gridsrno & "|" & 2
                End If
            Next

            alParaval.Add(type)
            alParaval.Add(name)
            alParaval.Add(paytype)
            alParaval.Add(refno)
            alParaval.Add(debit)
            alParaval.Add(credit)
            alParaval.Add(gridsrno)
            alParaval.Add("")   'SPECIAL REMARKS
            alParaval.Add(TXTSALNO.Text.Trim)   'PARTYBILLNO
            alParaval.Add("")   'COSTCENTER

            Dim objclsjvmaster As New ClsJournalMaster()
            objclsjvmaster.alParaval = alParaval
            Dim DT As DataTable = objclsjvmaster.save()

            TEMPJVNO = Val(DT.Rows(0).Item(0))
            ACCOUNTSENTRYLEDGER(DT.Rows(0).Item(0))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ACCOUNTSENTRYLEDGER(ByVal JVNO As Integer)
        Try
            Dim OBJJV As New ClsJournalMaster
            Dim INTRESULT As Integer
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBNAME.Text.Trim)    'CRLEDGER
            ALPARAVAL.Add(Val(TXTTOTALEAR.Text.Trim))    'CRAMT
            ALPARAVAL.Add(GRIDEAR.Rows(0).Cells(GEARNINGS.Index).Value)    'DRLEDGER

            ALPARAVAL.Add(JVNO)            'JOURNAL NO
            ALPARAVAL.Add(Format(Convert.ToDateTime(SALDATE.Text).Date, "MM/dd/yyyy"))            'JOURNAL DATE
            ALPARAVAL.Add("")        'REMARKS
            ALPARAVAL.Add("JOURNAL REGISTER")        'REGISTER
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)
            ALPARAVAL.Add(Val(TXTSALNO.Text.Trim))   'partybillno

            OBJJV.alParaval = ALPARAVAL
            INTRESULT = OBJJV.ACCOUNTS()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AUTOJVDEDUCTION()
        Try

            'save entry in journal
            Dim alParaval As New ArrayList
            alParaval.Add(0)
            alParaval.Add("JOURNAL REGISTER")
            alParaval.Add(Format(Convert.ToDateTime(SALDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(Val(TXTTOTALDED.Text.Trim))    'TOTALDR
            alParaval.Add(Val(TXTTOTALDED.Text.Trim))    'TOTALCR
            alParaval.Add("Against Bill No " & TXTSALNO.Text.Trim & " Bill Dt. " & SALDATE.Text)   'FOR REMARKS
            alParaval.Add("Against Bill No " & TXTSALNO.Text.Trim & " Bill Dt. " & SALDATE.Text)   'FOR BILLREMARKS
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim type As String = ""
            Dim name As String = ""
            Dim paytype As String = ""
            Dim refno As String = ""
            Dim debit As String = ""
            Dim credit As String = ""
            Dim gridsrno As String = ""

            For I As Integer = 0 To GRIDDED.RowCount
                If type = "" Then
                    type = "Dr"
                    name = CMBNAME.Text.Trim
                    paytype = "Against Bill"
                    refno = "JV-" & Val(TEMPJVNO)
                    debit = Val(TXTTOTALDED.Text.Trim)
                    credit = 0
                    gridsrno = 1
                Else
                    type = type & "|" & "Cr"
                    name = name & "|" & GRIDDED.Rows(I - 1).Cells(GDEDUCTION.Index).Value
                    paytype = paytype & "|" & "On Account"
                    refno = refno & "|" & ""
                    debit = debit & "|" & 0
                    credit = credit & "|" & Val(GRIDDED.Rows(I - 1).Cells(GDEDAMT.Index).Value)
                    gridsrno = gridsrno & "|" & I + 1
                End If
            Next

            alParaval.Add(type)
            alParaval.Add(name)
            alParaval.Add(paytype)
            alParaval.Add(refno)
            alParaval.Add(debit)
            alParaval.Add(credit)
            alParaval.Add(gridsrno)
            alParaval.Add("")   'SPECIAL REMARKS
            alParaval.Add(TXTSALNO.Text.Trim)   'PARTYBILLNO
            alParaval.Add("")   'COSTCENTER

            Dim objclsjvmaster As New ClsJournalMaster()
            objclsjvmaster.alParaval = alParaval
            Dim DT As DataTable = objclsjvmaster.save()

            ACCOUNTSENTRYDEDUCTION(DT.Rows(0).Item(0))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ACCOUNTSENTRYDEDUCTION(ByVal JVNO As Integer)
        Try
            Dim OBJJV As New ClsJournalMaster
            Dim INTRESULT As Integer
            Dim ALPARAVAL As New ArrayList

            For I As Integer = 0 To GRIDDED.RowCount - 1
                ALPARAVAL.Clear()
                ALPARAVAL.Add(GRIDDED.Rows(I).Cells(GDEDUCTION.Index).Value)    'CRLEDGER
                ALPARAVAL.Add(Val(GRIDDED.Rows(I).Cells(GDEDAMT.Index).Value))    'DEDAMT
                ALPARAVAL.Add(CMBNAME.Text.Trim)    'DRLEDGER

                ALPARAVAL.Add(JVNO)            'JOURNAL NO
                ALPARAVAL.Add(Format(Convert.ToDateTime(SALDATE.Text).Date, "MM/dd/yyyy"))            'JOURNAL DATE
                ALPARAVAL.Add("")        'REMARKS
                ALPARAVAL.Add("JOURNAL REGISTER")        'REGISTER
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                ALPARAVAL.Add(Val(TXTSALNO.Text.Trim))   'partybillno

                OBJJV.alParaval = ALPARAVAL
                INTRESULT = OBJJV.ACCOUNTS()
            Next



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AUTOJVLOAN()
        Try

            'save entry in journal
            Dim alParaval As New ArrayList
            alParaval.Add(0)
            alParaval.Add("JOURNAL REGISTER")
            alParaval.Add(Format(Convert.ToDateTime(SALDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(Val(TXTLOANEMI.Text.Trim))    'TOTALDR
            alParaval.Add(Val(TXTLOANEMI.Text.Trim))    'TOTALCR
            alParaval.Add("Against Bill No " & TXTSALNO.Text.Trim & " Bill Dt. " & SALDATE.Text)   'FOR REMARKS
            alParaval.Add("Against Bill No " & TXTSALNO.Text.Trim & " Bill Dt. " & SALDATE.Text)   'FOR BILLREMARKS
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim type As String = ""
            Dim name As String = ""
            Dim paytype As String = ""
            Dim refno As String = ""
            Dim debit As String = ""
            Dim credit As String = ""
            Dim gridsrno As String = ""

            For I As Integer = 0 To 1
                If type = "" Then
                    type = "Dr"
                    name = CMBNAME.Text.Trim
                    paytype = "Against Bill"
                    refno = "JV-" & Val(TEMPJVNO)
                    debit = Val(TXTLOANEMI.Text.Trim)
                    credit = 0
                    gridsrno = 1
                Else
                    type = type & "|" & "Cr"
                    name = name & "|" & CMBLOANNAME.Text.Trim
                    paytype = paytype & "|" & "On Account"
                    refno = refno & "|" & ""
                    debit = debit & "|" & 0
                    credit = credit & "|" & Val(TXTLOANEMI.Text.Trim)
                    gridsrno = gridsrno & "|" & 2
                End If
            Next

            alParaval.Add(type)
            alParaval.Add(name)
            alParaval.Add(paytype)
            alParaval.Add(refno)
            alParaval.Add(debit)
            alParaval.Add(credit)
            alParaval.Add(gridsrno)
            alParaval.Add("")   'SPECIAL REMARKS
            alParaval.Add(TXTSALNO.Text.Trim)   'PARTYBILLNO
            alParaval.Add("")   'COSTCENTER

            Dim objclsjvmaster As New ClsJournalMaster()
            objclsjvmaster.alParaval = alParaval
            Dim DT As DataTable = objclsjvmaster.save()

            ACCOUNTSENTRYLOAN(DT.Rows(0).Item(0))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ACCOUNTSENTRYLOAN(ByVal JVNO As Integer)
        Try
            Dim OBJJV As New ClsJournalMaster
            Dim INTRESULT As Integer
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBLOANNAME.Text.Trim)    'CRLEDGER
            ALPARAVAL.Add(Val(TXTLOANEMI.Text.Trim))    'CRAMT
            ALPARAVAL.Add(CMBNAME.Text.Trim)    'DRLEDGER

            ALPARAVAL.Add(JVNO)            'JOURNAL NO
            ALPARAVAL.Add(Format(Convert.ToDateTime(SALDATE.Text).Date, "MM/dd/yyyy"))            'JOURNAL DATE
            ALPARAVAL.Add("")        'REMARKS
            ALPARAVAL.Add("JOURNAL REGISTER")        'REGISTER
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)
            ALPARAVAL.Add(Val(TXTSALNO.Text.Trim))   'partybillno

            OBJJV.alParaval = ALPARAVAL
            INTRESULT = OBJJV.ACCOUNTS()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If EDIT = False Then
            'CHECKING WHETHER SALARY OF THE EMPLOYUEE FOR THE MONTH IS PRESENT OR NOT
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" SAL_NO", "", " SALARYMASTER INNER JOIN EMPLOYEEMASTER ON SALARYMASTER.SAL_EMPID = EMPLOYEEMASTER.EMP_id AND SALARYMASTER.SAL_CMPID = EMPLOYEEMASTER.EMP_cmpid AND SALARYMASTER.SAL_YEARID = EMPLOYEEMASTER.EMP_yearid ", " AND EMP_NAME ='" & CMBEMPNAME.Text.Trim & "' AND SAL_MONTH = '" & CMBMONTH.Text.Trim & "' AND SAL_CMPID = " & CmpId & " AND SAL_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                EP.SetError(CMBMONTH, "Salary for selected Month Already Present")
                bln = False
            End If
        End If


        If TXTWORKDAYS.Text.Trim.Length = 0 Then
            EP.SetError(TXTWORKDAYS, "Select Month")
            bln = False
        End If

        If CMBMONTH.Text.Trim.Length = 0 Then
            EP.SetError(CMBMONTH, "Select Month")
            bln = False
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Salary Paid, Entry Locked")
            bln = False
        End If

        If CMBEMPNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBEMPNAME, "Select Employee's Name")
            bln = False
        End If

        If GRIDEAR.RowCount = 0 Then
            EP.SetError(TXTEARAMT, "Select Earnings")
            bln = False
        End If

        If TXTNETTPAY.Text.Trim.Length = 0 Then
            EP.SetError(TXTNETTPAY, "Enter Salary Amount")
            bln = False
        End If


        If SALDATE.Text = "__/__/____" Then
            EP.SetError(SALDATE, "Enter Proper Date")
            bln = False
        ElseIf Not datecheck(SALDATE.Text) Then
            EP.SetError(SALDATE, "Date Not in Current Accounting Year")
            bln = False
        End If

        Return bln

    End Function

    Private Sub CMBEMPNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBEMPNAME.Enter
        Try
            If CMBEMPNAME.Text.Trim = "" Then FILLEMP(CMBEMPNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBEMPNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETBALANCE()
        Try
            Dim USERACCOUNTSADD, USERACCOUNTSEDIT, USERACCOUNTSVIEW, USERACCOUNTSDELETE As Boolean
            Dim DTACCOUNTSROW() As DataRow
            DTACCOUNTSROW = USERRIGHTS.Select("FormName = 'ACCOUNT REPORTS'")
            USERACCOUNTSADD = DTACCOUNTSROW(0).Item(1)
            USERACCOUNTSEDIT = DTACCOUNTSROW(0).Item(2)
            USERACCOUNTSVIEW = DTACCOUNTSROW(0).Item(3)
            USERACCOUNTSDELETE = DTACCOUNTSROW(0).Item(4)


            TXTLEDGERBAL.Text = "0.00"
            TXTLOANBAL.Text = "0.00"
            If USERACCOUNTSVIEW = False Then
                TXTLEDGERBAL.Visible = False
                TXTLOANBAL.Visible = False
            End If


            'LEDGER BALANCE
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("(CASE WHEN DR > 0 THEN 'Dr'  ELSE 'Cr' END) AS SALEBAL, (CASE WHEN DR > 0 THEN DR ELSE CR END) AS BALANCE ", "", "  TRIALBALANCE ", " AND NAME = '" & CMBNAME.Text.Trim & "' AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then TXTLEDGERBAL.Text = Convert.ToString(Val(DT.Rows(0).Item("BALANCE"))) & "  " & DT.Rows(0).Item("SALEBAL")


            'LOAN LEDGER BALANCE
            DT = OBJCMN.search("(CASE WHEN DR > 0 THEN 'Dr'  ELSE 'Cr' END) AS SALEBAL, (CASE WHEN DR > 0 THEN DR ELSE CR END) AS BALANCE ", "", "  TRIALBALANCE ", " AND NAME = '" & CMBLOANNAME.Text.Trim & "' AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then TXTLOANBAL.Text = Convert.ToString(Val(DT.Rows(0).Item("BALANCE"))) & "  " & DT.Rows(0).Item("SALEBAL")


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBEMPNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBEMPNAME.Validated
        Try
            If CMBEMPNAME.Text.Trim <> "" And EDIT = False  Then
                GRIDDED.RowCount = 0
                GRIDEAR.RowCount = 0

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" ISNULL(LEDGERS.ACC_CMPNAME,'') AS LEDGERNAME, ISNULL(LOANLEDGERS.ACC_CMPNAME,'') AS LOANLEDGERNAME, ISNULL(EMPLOYEEMASTER.EMP_REMARKS, '') AS REMARKS ", "", " EMPLOYEEMASTER LEFT OUTER JOIN LEDGERS ON EMPLOYEEMASTER.EMP_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS LOANLEDGERS ON EMPLOYEEMASTER.EMP_LOANLEDGERID = LOANLEDGERS.Acc_id ", " AND EMPLOYEEMASTER.EMP_NAME = '" & CMBEMPNAME.Text.Trim & "' AND EMPLOYEEMASTER.EMP_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBNAME.Text = DT.Rows(0).Item("LEDGERNAME")
                    CMBLOANNAME.Text = DT.Rows(0).Item("LOANLEDGERNAME")
                    txtremarks.Text = DT.Rows(0).Item("REMARKS")
                End If


                DT = OBJCMN.search(" EMPLOYEEMASTER_DEDUCTION.EMP_SRNO AS SRNO, LEDGERS.Acc_cmpname AS DEDUCTION, EMPLOYEEMASTER_DEDUCTION.EMP_AMT AS DEDAMT ", "", " EMPLOYEEMASTER_DEDUCTION INNER JOIN LEDGERS ON EMPLOYEEMASTER_DEDUCTION.EMP_DEDID = LEDGERS.Acc_id INNER JOIN EMPLOYEEMASTER ON EMPLOYEEMASTER.EMP_ID = EMPLOYEEMASTER_DEDUCTION.EMP_ID ", " AND EMPLOYEEMASTER.EMP_NAME = '" & CMBEMPNAME.Text.Trim & "' AND EMPLOYEEMASTER.EMP_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    For Each DTDED As DataRow In DT.Rows
                        GRIDDED.Rows.Add(DTDED("SRNO"), DTDED("DEDUCTION"), Format(Val(DTDED("DEDAMT")), "0.00"))
                    Next
                End If

                DT = OBJCMN.search(" EMPLOYEEMASTER_EARNINGS.EMP_SRNO AS SRNO, LEDGERS.Acc_cmpname AS EARNINGS, EMPLOYEEMASTER_EARNINGS.EMP_AMT AS EARAMT ", "", " EMPLOYEEMASTER_EARNINGS INNER JOIN LEDGERS ON EMPLOYEEMASTER_EARNINGS.EMP_EARID = LEDGERS.Acc_id INNER JOIN EMPLOYEEMASTER ON EMPLOYEEMASTER.EMP_ID = EMPLOYEEMASTER_EARNINGS.EMP_ID ", " AND EMPLOYEEMASTER.EMP_NAME = '" & CMBEMPNAME.Text.Trim & "' AND EMPLOYEEMASTER.EMP_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    For Each DTEAR As DataRow In DT.Rows
                        GRIDEAR.Rows.Add(DTEAR("SRNO"), DTEAR("EARNINGS"), Format(Val(DTEAR("EARAMT")), "0.00"))
                    Next
                End If
                TOTAL()
                GETBALANCE()

                CMBEMPNAME.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
LINE1:
            TEMPSALNO = Val(TXTSALNO.Text) + 1
            GETMAXNO_SALNO()
            Dim MAXNO As Integer = TXTSALNO.Text.Trim
            clear()
            If Val(TXTSALNO.Text) - 1 >= TEMPSALNO Then
                EDIT = True
                SalaryMaster_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If TEMPSALNO < MAXNO And GRIDEAR.RowCount = 0 Then
                TXTSALNO.Text = TEMPSALNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try

            GRIDDED.RowCount = 0
            GRIDEAR.RowCount = 0
LINE1:
            TEMPSALNO = Val(TXTSALNO.Text) - 1
            If TEMPSALNO > 0 Then
                EDIT = True
                SalaryMaster_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDEAR.RowCount = 0 And GRIDDED.RowCount = 0 And TEMPSALNO > 1 Then
                TXTSALNO.Text = TEMPSALNO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripdelete.Click
        DELETE()
    End Sub

    Sub DELETE()
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If MsgBox("Wish to Delete Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(TEMPSALNO)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(YearId)

            Dim OBJSALARY As New ClsSalaryMaster
            OBJSALARY.alParaval = ALPARAVAL
            Dim INTRES As Integer = OBJSALARY.Delete
            MsgBox("Salary Deleted")

            clear()
            EDIT = False
            CMBNAME.Focus()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If tstxtbillno.Text.Trim.Length = 0 Then Exit Sub
            TEMPSALNO = Val(tstxtbillno.Text)
            If TEMPSALNO > 0 Then
                EDIT = True
                SalaryMaster_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDEDUCTION_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDEDUCTION.Enter
        Try
            If CMBDEDUCTION.Text.Trim = "" Then fillledger(CMBDEDUCTION, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income'  OR GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances') ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDEDUCTION_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDEDUCTION.Validating
        Try
            If CMBDEDUCTION.Text.Trim <> "" Then ledgervalidate(CMBDEDUCTION, CMBACCCODE, e, Me, TXTDEDADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income'  OR GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances') ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBEARNINGS_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBEARNINGS.Enter
        Try
            If CMBEARNINGS.Text.Trim = "" Then fillledger(CMBEARNINGS, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income'  OR GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances') ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBEARNINGS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBEARNINGS.Validating
        Try
            If CMBEARNINGS.Text.Trim <> "" Then ledgervalidate(CMBEARNINGS, CMBACCCODE, e, Me, TXTDEDADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income'  OR GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances') ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDDED_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDDED.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDDED.Item(GDEDSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTDEDSRNO.Text = GRIDDED.Item(GDEDSRNO.Index, e.RowIndex).Value
                CMBDEDUCTION.Text = GRIDDED.Item(GDEDUCTION.Index, e.RowIndex).Value
                TXTDEDAMT.Text = GRIDDED.Item(GDEDAMT.Index, e.RowIndex).Value.ToString
                TEMPROW = e.RowIndex
                TXTDEDSRNO.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GRIDEAR_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDEAR.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDEAR.Item(GEARSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDEARDOUBLECLICK = True
                TXTEARSRNO.Text = GRIDEAR.Item(GEARSRNO.Index, e.RowIndex).Value
                CMBEARNINGS.Text = GRIDEAR.Item(GEARNINGS.Index, e.RowIndex).Value
                TXTEARAMT.Text = GRIDEAR.Item(GEARAMT.Index, e.RowIndex).Value.ToString
                TEMPEARROW = e.RowIndex
                TXTEARSRNO.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDDED_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDDED.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDDED.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDDED.Rows.RemoveAt(GRIDDED.CurrentRow.Index)
                getsrno(GRIDDED)
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDEAR_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDEAR.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDEAR.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                If GRIDEARDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDEAR.Rows.RemoveAt(GRIDEAR.CurrentRow.Index)
                getsrno(GRIDEAR)
                TOTAL()
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            ElseIf e.KeyCode = Keys.F12 And GRIDEAR.RowCount > 0 Then
                If GRIDEAR.CurrentRow.Cells(GEARNINGS.Index).Value <> "" Then GRIDEAR.Rows.Add(CloneWithValues(GRIDEAR.CurrentRow))
            ElseIf e.KeyCode = Keys.F11 And GRIDEAR.RowCount > 0 Then
                If GRIDEAR.CurrentRow.Cells(GEARNINGS.Index).Value <> "" And GRIDEAR.CurrentRow.Index > 0 Then
                    GRIDEAR.Item(GEARAMT.Index, GRIDEAR.CurrentRow.Index).Value = GRIDEAR.Item(GEARAMT.Index, GRIDEAR.CurrentRow.Index - 1).Value
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function CloneWithValues(ByVal row As DataGridViewRow) As DataGridViewRow
        CloneWithValues = CType(row.Clone(), DataGridViewRow)
        For index As Int32 = 0 To row.Cells.Count - 1
            CloneWithValues.Cells(index).Value = row.Cells(index).Value
        Next
    End Function
    Sub EDITROW()
        Try
            If GRIDEAR.CurrentRow.Index >= 0 And GRIDEAR.Item(GEARSRNO.Index, GRIDEAR.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TXTEARSRNO.Text = GRIDEAR.Item(GEARSRNO.Index, GRIDEAR.CurrentRow.Index).Value.ToString
                CMBEARNINGS.Text = GRIDEAR.Item(GEARNINGS.Index, GRIDEAR.CurrentRow.Index).Value.ToString
                TXTEARAMT.Text = GRIDEAR.Item(GEARAMT.Index, GRIDEAR.CurrentRow.Index).Value.ToString


                TEMPROW = GRIDEAR.CurrentRow.Index
                TXTEARSRNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTDEDAMT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTDEDAMT.Validating
        Try
            If CMBDEDUCTION.Text.Trim <> "" And Val(TXTDEDAMT.Text.Trim) > 0 Then
                FILLDED()
            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTEARAMT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTEARAMT.Validating
        Try
            If CMBEARNINGS.Text.Trim <> "" And Val(TXTEARAMT.Text.Trim) > 0 Then
                FILLEAR()
            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTDEDSRNO_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTDEDSRNO.GotFocus
        If GRIDDOUBLECLICK = False Then
            If GRIDDED.RowCount > 0 Then
                TXTDEDSRNO.Text = Val(GRIDDED.Rows(GRIDDED.RowCount - 1).Cells(0).Value) + 1
            Else
                TXTDEDSRNO.Text = 1
            End If
        End If
    End Sub

    Private Sub TXTEARSRNO_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTEARSRNO.GotFocus
        If GRIDEARDOUBLECLICK = False Then
            If GRIDEAR.RowCount > 0 Then
                TXTEARSRNO.Text = Val(GRIDEAR.Rows(GRIDEAR.RowCount - 1).Cells(0).Value) + 1
            Else
                TXTEARSRNO.Text = 1
            End If
        End If
    End Sub

    Sub GETDATE()
        Try
            If CMBMONTH.Text.Trim = "January" Then
                dtfrom.Value = "01/01/" & Year(AccFrom)
            ElseIf CMBMONTH.Text.Trim = "February" Then
                dtfrom.Value = "01/02/" & Year(AccFrom)
            ElseIf CMBMONTH.Text.Trim = "March" Then
                dtfrom.Value = "01/03/" & Year(AccFrom)
            ElseIf CMBMONTH.Text.Trim = "April" Then
                dtfrom.Value = "01/04/" & Year(AccFrom)
            ElseIf CMBMONTH.Text.Trim = "May" Then
                dtfrom.Value = "01/05/" & Year(AccFrom)
            ElseIf CMBMONTH.Text.Trim = "June" Then
                dtfrom.Value = "01/06/" & Year(AccFrom)
            ElseIf CMBMONTH.Text.Trim = "July" Then
                dtfrom.Value = "01/07/" & Year(AccFrom)
            ElseIf CMBMONTH.Text.Trim = "August" Then
                dtfrom.Value = "01/08/" & Year(AccFrom)
            ElseIf CMBMONTH.Text.Trim = "September" Then
                dtfrom.Value = "01/09/" & Year(AccFrom)
            ElseIf CMBMONTH.Text.Trim = "October" Then
                dtfrom.Value = "01/10/" & Year(AccFrom)
            ElseIf CMBMONTH.Text.Trim = "November" Then
                dtfrom.Value = "01/11/" & Year(AccFrom)
            ElseIf CMBMONTH.Text.Trim = "December" Then
                dtfrom.Value = "01/12/" & Year(AccFrom)
            End If
            dtto.Value = dtfrom.Value.AddMonths(1)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            If CMBMONTH.Text.Trim = "" Then CMBMONTH.SelectedIndex = 0
            GETDATE()
            TXTWORKDAYS.Clear()
            TXTPAYDAYS.Clear()

            TXTTOTALDED.Clear()
            TXTTOTALEAR.Clear()
            TXTNETTPAY.Clear()

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable

            'GET WORKDAYS FROM ATTENDENCE
            DTDAY.Value = dtfrom.Value
            For I As Integer = 1 To DateDiff(DateInterval.Day, dtfrom.Value.Date, dtto.Value.Date)
                'If DTDAY.Value.DayOfWeek <> DayOfWeek.Sunday Then TXTWORKDAYS.Text = Val(TXTWORKDAYS.Text) + 1
                TXTWORKDAYS.Text = Val(TXTWORKDAYS.Text) + 1
                DTDAY.Value = DateAdd(DateInterval.Day, I, dtfrom.Value.Date)
            Next
            TXTPAYDAYS.Text = Val(TXTWORKDAYS.Text.Trim) - Val(TXTLEAVE.Text.Trim)

            For Each ROW As DataGridViewRow In GRIDDED.Rows
                TXTTOTALDED.Text = Format(Val(TXTTOTALDED.Text.Trim) + Val(ROW.Cells(GDEDAMT.Index).EditedFormattedValue), "0.00")
            Next

            For Each ROW As DataGridViewRow In GRIDEAR.Rows
                TXTTOTALEAR.Text = Format(Val(TXTTOTALEAR.Text.Trim) + Val(ROW.Cells(GEARAMT.Index).EditedFormattedValue), "0.00")
            Next

            TXTNETTPAY.Text = Val(TXTTOTALEAR.Text.Trim) - Val(TXTTOTALDED.Text.Trim) - Val(TXTADVTAKEN.Text.Trim) - Val(TXTLOANEMI.Text.Trim)
            If Val(TXTNETTPAY.Text.Trim) > 0 Then txtinwords.Text = CurrencyToWord(Val(TXTNETTPAY.Text.Trim))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLDED()

        If GRIDDOUBLECLICK = False Then
            GRIDDED.Rows.Add(Val(TXTDEDSRNO.Text.Trim), CMBDEDUCTION.Text.Trim, Val(TXTDEDAMT.Text.Trim))
            getsrno(GRIDDED)
        ElseIf GRIDDOUBLECLICK = True Then

            GRIDDED.Item(GDEDSRNO.Index, TEMPROW).Value = TXTDEDSRNO.Text.Trim
            GRIDDED.Item(GDEDUCTION.Index, TEMPROW).Value = CMBDEDUCTION.Text.Trim
            GRIDDED.Item(GDEDAMT.Index, TEMPROW).Value = Val(TXTDEDAMT.Text.Trim)

            GRIDDOUBLECLICK = False

        End If
        TOTAL()
        GRIDDED.FirstDisplayedScrollingRowIndex = GRIDDED.RowCount - 1

        TXTDEDSRNO.Clear()
        CMBDEDUCTION.Text = ""
        TXTDEDAMT.Clear()
        TXTDEDSRNO.Focus()

    End Sub

    Sub FILLEAR()

        If GRIDEARDOUBLECLICK = False Then
            GRIDEAR.Rows.Add(Val(TXTEARSRNO.Text.Trim), CMBEARNINGS.Text.Trim, Val(TXTEARAMT.Text.Trim))
            getsrno(GRIDEAR)
        ElseIf GRIDEARDOUBLECLICK = True Then

            GRIDEAR.Item(GEARSRNO.Index, TEMPEARROW).Value = TXTEARSRNO.Text.Trim
            GRIDEAR.Item(GEARNINGS.Index, TEMPEARROW).Value = CMBEARNINGS.Text.Trim
            GRIDEAR.Item(GEARAMT.Index, TEMPEARROW).Value = Val(TXTEARAMT.Text.Trim)

            GRIDEARDOUBLECLICK = False

        End If
        TOTAL()
        GRIDEAR.FirstDisplayedScrollingRowIndex = GRIDEAR.RowCount - 1

        TXTEARSRNO.Clear()
        CMBEARNINGS.Text = ""
        TXTEARAMT.Clear()
        TXTEARSRNO.Focus()

    End Sub

    Private Sub CMBMONTH_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMONTH.Validated
        TOTAL()
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPSALNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(ByVal SALNO As Integer)
        Try
            Dim TEMPMSG As Integer = MsgBox("Print Salary Slip?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbNo Then Exit Sub
            Dim OBJSAL As New SalarySlipDesign
            OBJSAL.MdiParent = MDIMain
            OBJSAL.SALNO = SALNO
            OBJSAL.WHERECLAUSE = "{SALARYMASTER.SAL_NO} =" & Val(SALNO) & " AND {SALARYMASTER.SAL_YEARID} = " & YearId
            OBJSAL.PARTYNAME = CMBNAME.Text.Trim
            OBJSAL.MONTHNAME = CMBMONTH.Text.Trim
            OBJSAL.FRMSTRING = "SALARY"
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBEMPNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBEMPNAME.Validating
        Try
            If CMBEMPNAME.Text.Trim <> "" Then EMPVALIDATE(CMBEMPNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillledger(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBACCCODE, e, Me, TXTDEDADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOANNAME_Enter(sender As Object, e As EventArgs) Handles CMBLOANNAME.Enter
        Try
            If CMBLOANNAME.Text.Trim = "" Then fillledger(CMBLOANNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'Loans' or GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances' or GROUPMASTER.GROUP_SECONDARY = 'Secured Loans'  or GROUPMASTER.GROUP_SECONDARY = 'Unsecured Loans')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOANNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBLOANNAME.Validating
        Try
            If CMBLOANNAME.Text.Trim <> "" Then namevalidate(CMBLOANNAME, CMBACCCODE, e, Me, TXTDEDADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'Loans' or GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances' or GROUPMASTER.GROUP_SECONDARY = 'Secured Loans'  or GROUPMASTER.GROUP_SECONDARY = 'Unsecured Loans')", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTDEDAMT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTDEDAMT.KeyPress, TXTEARAMT.KeyPress, TXTADVTAKEN.KeyPress, TXTLOANEMI.KeyPress, TXTLEAVE.KeyPress, TXTWORKDAYS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstxtbillno.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SENDWHATSAPP(TEMPSALNO)
            DT = OBJCMN.Execute_Any_String("UPDATE SALARYMASTER SET SAL_SENDWHATSAPP = 1 WHERE SAL_NO = " & TEMPSALNO & " AND SAL_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(SALNO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJSAL As New SalarySlipDesign
            OBJSAL.MdiParent = MDIMain
            OBJSAL.DIRECTPRINT = True
            OBJSAL.FRMSTRING = "SALARY"
            OBJSAL.MONTHNAME = CMBMONTH.Text.Trim
            OBJSAL.PARTYNAME = CMBNAME.Text.Trim
            OBJSAL.SALNO = Val(SALNO)
            OBJSAL.DIRECTMAIL = True
            OBJSAL.WHERECLAUSE = "{SALARYMASTER.SAL_NO}=" & Val(SALNO) & " and {SALARYMASTER.SAL_yearid}=" & YearId
            OBJSAL.NOOFCOPIES = 1
            OBJSAL.Show()
            OBJSAL.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & CMBMONTH.Text.Trim & "_SAL_" & Val(SALNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add(CMBMONTH.Text.Trim & "_SAL_" & Val(SALNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTADVTAKEN_Validated(sender As Object, e As EventArgs) Handles TXTADVTAKEN.Validated, TXTWORKDAYS.Validated, TXTLOANEMI.Validated, TXTLEAVE.Validated
        TOTAL()
    End Sub

    Private Sub CMBNAME_Validated(sender As Object, e As EventArgs) Handles CMBNAME.Validated, CMBLOANNAME.Validated
        Try
            GETBALANCE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALDATE_Validating(sender As Object, e As CancelEventArgs) Handles SALDATE.Validating
        Try
            If SALDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(SALDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDEAR_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDEAR.CellValidating
        Try
            Dim colNum As Integer = GRIDEAR.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return
            Select Case colNum

                Case GEARAMT.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDEAR.CurrentCell.Value = Nothing Then GRIDEAR.CurrentCell.Value = "0.00"
                        GRIDEAR.CurrentCell.Value = Convert.ToDecimal(GRIDEAR.Item(colNum, e.RowIndex).Value)
                        '' everything is good

                        TOTAL()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If

            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDDED_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDDED.CellValidating
        Try
            Dim colNum As Integer = GRIDDED.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return
            Select Case colNum

                Case GDEDAMT.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDDED.CurrentCell.Value = Nothing Then GRIDDED.CurrentCell.Value = "0.00"
                        GRIDDED.CurrentCell.Value = Convert.ToDecimal(GRIDDED.Item(colNum, e.RowIndex).Value)
                        '' everything is good

                        TOTAL()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If

            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class