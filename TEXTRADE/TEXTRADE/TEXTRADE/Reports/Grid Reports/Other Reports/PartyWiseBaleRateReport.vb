Imports System.ComponentModel
Imports BL

Public Class PartyWiseBaleRateReport
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String
    Public TEMPNAME As String = ""
    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, dtfrom.Value.Date)
        a2 = DatePart(DateInterval.Month, dtfrom.Value.Date)
        a3 = DatePart(DateInterval.Year, dtfrom.Value.Date)
        fromD = "(" & a3 & "," & a2 & "," & a1 & ")"

        a11 = DatePart(DateInterval.Day, dtto.Value.Date)
        a12 = DatePart(DateInterval.Month, dtto.Value.Date)
        a13 = DatePart(DateInterval.Year, dtto.Value.Date)
        toD = "(" & a13 & "," & a12 & "," & a11 & ")"
    End Sub
    Private Sub PartyWiseBaleRateReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLNAME(cmbname, False, " and ledgers.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.ACC_YEARID = " & YearId)
            If cmbname.Text.Trim = "" Then cmbname.Text = TEMPNAME
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Sub fillgrid()
    '    Try
    '        Dim objclsCMST As New ClsCommonMaster
    '        Dim dt As DataTable
    '        dt = objclsCMST.search(" PURCHASEMASTER.BILL_DATE AS BILLDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(PURCHASEMASTER.BILL_NOOFBALES, 0) AS NOOFBALES ", "", "  PARTYWISEBALERATE INNER JOIN PURCHASEMASTER ON PARTYWISEBALERATE.PAR_LEDGERID = PURCHASEMASTER.BILL_LEDGERID AND PARTYWISEBALERATE.PAR_TRANSID = PURCHASEMASTER.BILL_TRANSNAMEID LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON PARTYWISEBALERATE.PAR_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON PARTYWISEBALERATE.PAR_LEDGERID = LEDGERS.Acc_id", " AND SALESENQUIRY_FOLLOWUP.ENQ_NEXTDATE = '" & DTFROMDATE.Text & "'  AND SALESENQUIRY_FOLLOWUP.ENQ_YEARID = " & YearId & " ORDER BY SALESENQUIRY_FOLLOWUP.ENQ_NO")
    '        gridbilldetails.DataSource = dt
    '        If dt.Rows.Count > 0 Then
    '            gridbill.FocusedRowHandle = gridbill.RowCount - 1
    '            gridbill.TopRowIndex = gridbill.RowCount - 15
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub


    'Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs)
    '    Try
    '        If USEREDIT = False And USERVIEW = False Then
    '            MsgBox("Insufficient Rights")
    '            Exit Sub
    '        End If
    '        fillgrid()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs)
    '    Try
    '        If USEREDIT = False Then
    '            MsgBox("Insufficient Rights")
    '            Exit Sub
    '        End If
    '        showform(True, gridbill.GetFocusedRowCellValue("ENQNO"))
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Sub showform(ByVal EDITVAL As Boolean, ByVal ENQNO As Integer)
    '    Try
    '        If ENQNO = 0 Then
    '            Exit Sub
    '        End If

    '        If USEREDIT = False And USERVIEW = False Then
    '            MsgBox("Insufficient Rights")
    '            Exit Sub
    '        End If
    '        Dim OBJENQNO As New SalesEnquiry
    '        OBJENQNO.EDIT = EDITVAL
    '        OBJENQNO.MdiParent = MDIMain
    '        OBJENQNO.TEMPSONO = ENQNO
    '        OBJENQNO.Show()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub PartyWiseBaleRateReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
            'ElseIf e.Alt = True And e.KeyCode = Keys.R Then
            '    Call TOOLREFRESH_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.P Then
            'Call ExcelExport_Click(sender, e)
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    'Private Sub TOOLEXCELL_Click(sender As Object, e As EventArgs)
    '    Try
    '        Dim PATH As String = "" = ""
    '        If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
    '        PATH = Application.StartupPath & "\ Enquiry Followup Details.XLS"

    '        Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
    '        opti.ShowGridLines = True

    '        Dim workbook As String = PATH
    '        If FileIO.FileSystem.FileExists(PATH) = True Then Interaction.GetObject(workbook).close(False)
    '        GC.Collect()

    '        Dim PERIOD As String = AccFrom & " - " & AccTo

    '        opti.SheetName = " Enquiry Followup Details"
    '        gridbill.ExportToXls(PATH, opti)
    '        EXCELCMPHEADER(PATH, " Enquiry Followup Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub CMDEDIT_Click(sender As Object, e As EventArgs)
    '    Try
    '        If USEREDIT = False Then
    '            MsgBox("Insufficient Rights")
    '            Exit Sub
    '        End If
    '        showform(True, gridbill.GetFocusedRowCellValue("ENQNO"))
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub DTPARTYBILLDATE_Click(sender As Object, e As EventArgs)
    '    DTFROMDATE.SelectAll()
    'End Sub

    'Private Sub DTPARTYBILLDATE_Validating(sender As Object, e As CancelEventArgs)
    '    Try
    '        If DTFROMDATE.Text.Trim <> "__/__/____" Then
    '            'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
    '            Dim TEMP As DateTime
    '            If Not DateTime.TryParse(DTFROMDATE.Text, TEMP) Then
    '                MsgBox("Enter Proper Date")
    '                e.Cancel = True
    '                Exit Sub
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub DTPARTYBILLDATE_Validated(sender As Object, e As EventArgs)
    '    Try

    '        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ENQUIRY'")
    '        USERADD = DTROW(0).Item(1)
    '        USEREDIT = DTROW(0).Item(2)
    '        USERVIEW = DTROW(0).Item(3)
    '        USERDELETE = DTROW(0).Item(4)



    '        If USEREDIT = False And USERVIEW = False Then
    '            MsgBox("Insufficient Rights")
    '            Exit Sub
    '        End If

    '        If DTFROMDATE.Text <> "__/__/____" Then fillgrid() Else MsgBox("Please Enter Followup Date")
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdshowdetails_Click(sender As Object, e As EventArgs) Handles cmdshowdetails.Click
        fillgrid()
    End Sub
    Sub fillgrid()

        'txttotal.Text = "0.00"

        'Dim dt As New DataTable()
        'Dim ALPARAVAL As New ArrayList

        'Dim objregister As New ClsLedgerBook

        'ALPARAVAL.Add(cmbname.Text.Trim)
        'If chkdate.Checked = True Then
        '    ALPARAVAL.Add(dtfrom.Value.Date)
        '    ALPARAVAL.Add(dtto.Value.Date)
        'Else
        '    ALPARAVAL.Add(AccFrom)
        '    'COZ DATE WONT BE IN ACCOUNTING YEAR
        '    If CmpName = "TRANSFER DATA" Then
        '        ALPARAVAL.Add(Now.Date)
        '    Else
        '        ALPARAVAL.Add(AccTo)
        '    End If
        'End If

        'ALPARAVAL.Add(CmpId)
        'ALPARAVAL.Add(Locationid)
        'ALPARAVAL.Add(YearId)

        'objregister.alParaval = ALPARAVAL

        'If chksubdetails.CheckState = CheckState.Checked Then
        '    dt = objregister.GETSUBDETAILS
        'ElseIf chkdetails.Checked = True Then
        '    dt = objregister.getDETAILS
        'ElseIf chkdetails.Checked = False Then
        '    dt = objregister.getSUMMARY
        'End If

        'griddetails.DataSource = dt

        ''getting opening balances
        'Dim OBJCOMMON As New ClsCommonMaster
        'If chkdate.CheckState = CheckState.Unchecked Then
        '    dt = OBJCOMMON.search(" SUM(DR)-SUM(CR)", "", " Register_Grouped", " and name = '" & cmbname.Text.Trim & "' and acc_billdate <'" & Format(AccFrom, "MM/dd/yyyy") & "' and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and YEARID = " & YearId)
        'ElseIf chkdate.CheckState = CheckState.Checked Then
        '    dt = OBJCOMMON.search(" SUM(DR)-SUM(CR)", "", " Register_Grouped", " and name = '" & cmbname.Text.Trim & "' and acc_billdate <'" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "' and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and YEARID = " & YearId)
        'End If

        'If dt.Rows.Count > 0 Then
        '    If Val(dt.Rows(0).Item(0).ToString) < 0 Then
        '        txtopening.Text = Val(dt.Rows(0).Item(0).ToString) * (-1)
        '        lbldrcropening.Text = "Cr"
        '    Else
        '        txtopening.Text = Val(dt.Rows(0).Item(0).ToString)
        '        lbldrcropening.Text = "Dr"
        '    End If
        'End If

        ''THIS CODE IS WRITTEN COZ ABOVE CODE DOES NT RETRIEVE OPBAL IF DATE IS FROM 1ST DAY OF ACCOUNTING YEAR
        ''DONT DELETE THIS CODE IT IS CHECKED AND WORKING FINE
        'If (Val(txtopening.Text.Trim) = 0 And chkdate.CheckState = CheckState.Unchecked) Or (Val(txtopening.Text.Trim) = 0 And chkdate.CheckState = CheckState.Checked And dtfrom.Value.Date = Convert.ToDateTime("01/04/" & Year(AccFrom)).Date) Then
        '    dt = OBJCOMMON.search("ACC_OPBAL, ACC_DRCR", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId)
        '    If dt.Rows.Count > 0 Then
        '        txtopening.Text = Val(dt.Rows(0).Item(0).ToString)
        '        If dt.Rows(0).Item(1).ToString = "Dr." Then
        '            lbldrcropening.Text = "Dr"
        '        Else
        '            lbldrcropening.Text = "Cr"
        '        End If
        '    End If
        'End If

        'total()
        'txtopening.Text = Format(Val(txtopening.Text), "0.00")
    End Sub

    Private Sub cmbname_Enter(sender As Object, e As EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then FILLNAME(cmbname, False, " and ledgers.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.ACC_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and ledgers.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.ACC_YEARID = " & YearId
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBACCCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(sender As Object, e As CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then NAMEVALIDATE(cmbname, CMBACCCODE, e, Me, txtadd, " and ledgers.acc_cmpname = '" & cmbname.Text.Trim & "'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkdate_CheckedChanged(sender As Object, e As EventArgs) Handles chkdate.CheckedChanged
        Try
            dtfrom.Enabled = chkdate.CheckState
            dtto.Enabled = chkdate.CheckState
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class