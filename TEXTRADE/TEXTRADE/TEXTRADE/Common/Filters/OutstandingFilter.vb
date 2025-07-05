
Imports BL

Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.XtraGrid.Views.Grid

Public Class OutstandingFilter
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String
    Dim DTMAIL As New DataTable
    Dim DTWHATSAPP As New DataTable

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub FILLGROUP(ByVal WHERECLAUSE As String)
        Try
            If CMBGROUP.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("group_name", "", "GroupMaster", " and group_Yearid = " & YearId & WHERECLAUSE)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "Group_name"
                    CMBGROUP.DataSource = dt
                    CMBGROUP.DisplayMember = "group_name"
                    CMBGROUP.Text = ""
                End If
                CMBGROUP.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OutstandingFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ACCOUNT REPORTS'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            DTMAIL.Columns.Add("NAME")
            DTMAIL.Columns.Add("PARTYEMAILID")
            DTMAIL.Columns.Add("AGENTNAME")
            DTMAIL.Columns.Add("AGENTEMAILID")
            DTMAIL.Columns.Add("SUBJECT")
            DTMAIL.Columns.Add("ATTACHMENT")
            DTMAIL.Columns.Add("FILENAME")



            DTWHATSAPP.Columns.Add("NAME")
            DTWHATSAPP.Columns.Add("PARTYWHATSAPP")
            DTWHATSAPP.Columns.Add("AGENTNAME")
            DTWHATSAPP.Columns.Add("AGENTWHATSAPP")
            DTWHATSAPP.Columns.Add("SUBJECT")
            DTWHATSAPP.Columns.Add("ATTACHMENT")
            DTWHATSAPP.Columns.Add("FILENAME")

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If


            If FRMSTRING = "PAYOUTSTANDING" Then RBPAYABLE.Checked = True Else RBREC.Checked = True

            FILLCMB(" AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            FILLNAME(CMBBROKERNAME, False, " and LEDGERS.ACC_TYPE = 'AGENT'")
            FILLGROUP(" AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")


            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable = OBJCMN.SEARCH("CMP_NAME", "", "CMPMASTER", "")
            For Each DROW As DataRow In dt.Rows
                LSTCMP.Items.Add(DROW(0).ToString)
                If DROW(0) = CmpName Then LSTCMP.SetItemChecked(LSTCMP.Items.Count - 1, True)
            Next



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OutstandingFilter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.S) Then
                cmdshow_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, dtfrom.Value)
        a2 = DatePart(DateInterval.Month, dtfrom.Value)
        a3 = DatePart(DateInterval.Year, dtfrom.Value)
        fromD = "(" & a3 & "," & a2 & "," & a1 & ")"

        a11 = DatePart(DateInterval.Day, dtto.Value)
        a12 = DatePart(DateInterval.Month, dtto.Value)
        a13 = DatePart(DateInterval.Year, dtto.Value)
        toD = "(" & a13 & "," & a12 & "," & a11 & ")"
    End Sub

    Sub FILLCMB(ByVal WHERECLAUSE As String)
        Try
            FILLNAME(CMBPARTYNAME, False, WHERECLAUSE)
            If RBREC.Checked = True Then fillregister(cmbregister, " and register_type = 'SALE'") Else fillregister(cmbregister, " and register_type = 'PURCHASE'")

            If ClientName = "AVIS" Then
                If CMBDELIVERYAT.Text.Trim = "" Then FILLNAME(CMBDELIVERYAT, False, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')  AND GROUP_NAME = 'HASTE DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBDELIVERYAT.Text.Trim = "" Then FILLNAME(CMBDELIVERYAT, False, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')   AND ACC_TYPE = 'ACCOUNTS'")
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional SENDWHATSAPP As Boolean = False, Optional SENDMAIL As Boolean = False, Optional GROUPNAME As String = "", Optional PARTYNAME As String = "", Optional AGENTNAME As String = "") Handles cmdshow.Click
        Try

            If RBAGEING.Checked = True And SENDWHATSAPP = False And SENDMAIL = False Then
                Dim OBJAGEING As New AgeingReport
                OBJAGEING.MdiParent = MDIMain
                OBJAGEING.RBREC.Checked = RBREC.Checked
                OBJAGEING.RBPAYABLE.Checked = RBPAYABLE.Checked
                If CMBBROKERNAME.Text.Trim <> "" Then OBJAGEING.AGENTNAME = CMBBROKERNAME.Text.Trim

                gridbill.ClearColumnsFilter()
                Dim NAMECLAUSE As String = ""
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then
                            If RBGROUP.Checked = True Then
                                NAMECLAUSE = " AND (GROUPNAME = '" & dtrow("NAME") & "'"
                            ElseIf RBACCOUNT.Checked = True Then
                                NAMECLAUSE = " AND (NAME = '" & dtrow("NAME") & "'"
                            ElseIf RBAGENT.Checked = True Then
                                NAMECLAUSE = " AND (AGENT = '" & dtrow("NAME") & "'"
                            End If
                        Else
                            If RBGROUP.Checked = True Then
                                NAMECLAUSE = NAMECLAUSE & " OR GROUPNAME = '" & dtrow("NAME") & "'"
                            ElseIf RBACCOUNT.Checked = True Then
                                NAMECLAUSE = NAMECLAUSE & " OR NAME = '" & dtrow("NAME") & "'"
                            ElseIf RBAGENT.Checked = True Then
                                NAMECLAUSE = NAMECLAUSE & " OR AGENT = '" & dtrow("NAME") & "'"
                            End If
                        End If
                    End If
                Next
                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                    OBJAGEING.NAMECLAUSE = NAMECLAUSE
                End If

                If CHKMSME.CheckState = CheckState.Checked Then OBJAGEING.NAMECLAUSE = OBJAGEING.NAMECLAUSE & " AND MSMENO <> ''"


                If RBREC.Checked = True Then OBJAGEING.FILLGRIDREC() Else OBJAGEING.FILLGRIDPAY()
                OBJAGEING.Show()
                Exit Sub
            End If




            Dim OBJOUTSTAND As New OutstandingDesign
            If ClientName <> "SUPEEMA" Then OBJOUTSTAND.MdiParent = MDIMain

            OBJOUTSTAND.DIRECTMAIL = SENDMAIL
            OBJOUTSTAND.DIRECTWHATSAPP = SENDWHATSAPP
            If SENDWHATSAPP = True Or SENDMAIL = True Then OBJOUTSTAND.DIRECTPRINT = True

            'GET ALL YEARID FROM SELECTED COMPANY WITH SAME STARTYEAR
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item
            DT = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            'OBJOUTSTAND.selfor_ss = " {@YEARID} in [" & CMPCLAUSE & "]"
            If RBOUTSTANDINGBILLS.Checked = True Then OBJOUTSTAND.selfor_ss = " {OUTSTANDINGREPORT_ALL.YEARID} in [" & CMPCLAUSE & "]" Else OBJOUTSTAND.selfor_ss = " {OUTSTANDINGREPORT_DETAILS.YEARID} in [" & CMPCLAUSE & "]"


            ''IF CHK IS TRUE THEN GET ALL YEARID UDER THAT COMPANY
            'Dim TEMPCONDITION As String = ""
            'Dim OBJCMN As New ClsCommon
            'If CHKALLCMP.Checked = True Then
            '    Dim DTYEAR As DataTable = OBJCMN.Execute_Any_String("SELECT YEAR_ID AS YEARID FROM YEARMASTER WHERE YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' ", "", "")
            '    For Each DTROW As DataRow In DTYEAR.Rows
            '        If TEMPCONDITION = "" Then
            '            TEMPCONDITION = Val(DTROW("YEARID"))
            '        Else
            '            TEMPCONDITION = TEMPCONDITION & "," & Val(DTROW("YEARID"))
            '        End If
            '    Next
            'End If
            'If TEMPCONDITION = "" Then TEMPCONDITION = YearId
            'OBJOUTSTAND.selfor_ss = " {@YEARID} in [" & TEMPCONDITION & "]"


            OBJOUTSTAND.PARTYNAME = CMBPARTYNAME.Text.Trim
            OBJOUTSTAND.AGENTNAME = CMBBROKERNAME.Text.Trim
            If LSTCMP.CheckedItems.Count > 1 Then OBJOUTSTAND.MULTICMP = 1 Else OBJOUTSTAND.MULTICMP = 0

            If chkdate.Checked = True Then
                getFromToDate()
                OBJOUTSTAND.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                OBJOUTSTAND.TODATE = Format(dtto.Value.Date, "dd/MM/yyyy")
                If ClientName = "SKF" Or ClientName = "ALENCOT" Or ClientName = "DETLINE" Or ClientName = "SHREENAKODA" Then
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DATE} in date " & fromD & " to date " & toD & ""
                Else
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DATE} <= #" & Format(dtto.Value.Date, "MM/dd/yyyy") & "#"
                End If
            Else
                OBJOUTSTAND.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
                OBJOUTSTAND.TODATE = Format(AccTo.Date, "dd/MM/yyyy")
            End If

            If RBALL.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Checked Then
                    If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "OUTSTANDINGALLSUMMREC" Else OBJOUTSTAND.FRMSTRING = "OUTSTANDINGALLSUMMPAY"
                Else
                    OBJOUTSTAND.FRMSTRING = "OUTSTANDINGALLDTLS"
                End If
            ElseIf RBOUTSTANDING.Checked = True Then
                If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and (({@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim) & " AND {OUTSTANDINGREPORT_DETAILS.TYPE} <> 'RECEIPT') OR ({OUTSTANDINGREPORT_DETAILS.TYPE} = 'RECEIPT'))"
                If Val(TXTOVERDUEDAYSLESS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} <= " & Val(TXTOVERDUEDAYSLESS.Text.Trim)
                If CMBBROKERNAME.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.AGENT} = '" & CMBBROKERNAME.Text.Trim & "'"
                If CHKSUMMARY.CheckState = CheckState.Checked Then
                    If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "OUTSTANDINGRECSUMM" Else OBJOUTSTAND.FRMSTRING = "OUTSTANDINGPAYSUMM"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_ALL.BALANCE}>0"
                Else
                    If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "OUTSTANDINGRECDTLS" Else OBJOUTSTAND.FRMSTRING = "OUTSTANDINGPAYDTLS"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.BALANCE}>0"
                    If CMBDELIVERYAT.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.DELIVERYAT}= '" & CMBDELIVERYAT.Text.Trim & "'"
                    If CHKPDC.CheckState = CheckState.Checked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.CHKPDC}= FALSE"
                    If CHKPARTPAYMENT.CheckState = CheckState.Checked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.GRANDTOTAL}>0 and {OUTSTANDINGREPORT_DETAILS.RECAMT}>0"
                    If cmbregister.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.REGTYPE}= '" & cmbregister.Text.Trim & "'"
                End If
            ElseIf RBINT.Checked = True Then
                If RBREC.Checked = True Then
                    If Val(TXTDAYS.Text.Trim) = 0 Or Val(TXTINTEREST.Text.Trim) = 0 Then
                        MsgBox("Enter Proper Days / Interest", MsgBoxStyle.Critical)
                        TXTINTEREST.Focus()
                        Exit Sub
                    End If
                    If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim)
                    If Val(TXTOVERDUEDAYSLESS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} <= " & Val(TXTOVERDUEDAYSLESS.Text.Trim)
                    OBJOUTSTAND.FRMSTRING = "INTOUTSTANDINGREC"
                Else
                    If Val(TXTDAYS.Text.Trim) = 0 Or Val(TXTINTEREST.Text.Trim) = 0 Then
                        MsgBox("Enter Proper Days / Interest", MsgBoxStyle.Critical)
                        TXTINTEREST.Focus()
                        Exit Sub
                    End If
                    If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim)
                    If Val(TXTOVERDUEDAYSLESS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} <= " & Val(TXTOVERDUEDAYSLESS.Text.Trim)
                    OBJOUTSTAND.FRMSTRING = "INTOUTSTANDINGPAY"
                End If

            ElseIf RBINVENTORY.Checked = True Then
                If CMBDELIVERYAT.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.DELIVERYAT}= '" & CMBDELIVERYAT.Text.Trim & "'"
                If CMBBROKERNAME.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.AGENT} = '" & CMBBROKERNAME.Text.Trim & "'"
                If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim)
                If Val(TXTOVERDUEDAYSLESS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} <= " & Val(TXTOVERDUEDAYSLESS.Text.Trim)
                If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "RECINVENTORYOUTSTANDING" Else OBJOUTSTAND.FRMSTRING = "PAYINVENTORYOUTSTANDING"
                OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.BALANCE}>0"
                If CHKPDC.CheckState = CheckState.Checked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.CHKPDC}= FALSE"
                If CHKPARTPAYMENT.CheckState = CheckState.Checked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.GRANDTOTAL}>0 and {OUTSTANDINGREPORT_DETAILS.RECAMT}>0"
                If cmbregister.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.REGTYPE}= '" & cmbregister.Text.Trim & "'"

            ElseIf RBALLBILLS.Checked = True Then
                If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "ALLBILLOUTSTANDINGREC" Else OBJOUTSTAND.FRMSTRING = "ALLBILLOUTSTANDINGPAY"
                OBJOUTSTAND.REPORTNAME = "Outstanding Report (All Bills)"
                OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and ({OUTSTANDINGREPORT_ALL.TYPE}='SALE' OR {OUTSTANDINGREPORT_ALL.TYPE}='PURCHASE' OR {OUTSTANDINGREPORT_ALL.TYPE}='OPENING' OR {OUTSTANDINGREPORT_ALL.TYPE}='DIFF IN OPEN' OR {OUTSTANDINGREPORT_ALL.TYPE}='PAYMENT' OR {OUTSTANDINGREPORT_ALL.TYPE}='RECEIPT')"
                If cmbregister.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_ALL.REGTYPE}= '" & cmbregister.Text.Trim & "'"

            ElseIf RBOUTSTANDINGBILLS.Checked = True Then
                OBJOUTSTAND.REPORTNAME = "Outstanding Report (Only Outstanding Bills)"
                If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim)
                If Val(TXTOVERDUEDAYSLESS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} <= " & Val(TXTOVERDUEDAYSLESS.Text.Trim)
                If cmbregister.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_ALL.REGTYPE}= '" & cmbregister.Text.Trim & "'"
                If RBREC.Checked = True Then
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and ({OUTSTANDINGREPORT_ALL.TYPE}='SALE' OR {OUTSTANDINGREPORT_ALL.TYPE}='PURCHASE' OR {OUTSTANDINGREPORT_ALL.TYPE}='OPENING' OR {OUTSTANDINGREPORT_ALL.TYPE}='DIFF IN OPEN') AND {OUTSTANDINGREPORT_ALL.DEBIT} - {OUTSTANDINGREPORT_ALL.CREDIT}>0 "
                    OBJOUTSTAND.FRMSTRING = "ONLYBILLOUTSTANDINGREC"
                ElseIf RBPAYABLE.Checked = True Then
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and ({OUTSTANDINGREPORT_ALL.TYPE}='SALE' OR {OUTSTANDINGREPORT_ALL.TYPE}='PURCHASE' OR {OUTSTANDINGREPORT_ALL.TYPE}='OPENING' OR {OUTSTANDINGREPORT_ALL.TYPE}='DIFF IN OPEN') AND {OUTSTANDINGREPORT_ALL.CREDIT} - {OUTSTANDINGREPORT_ALL.DEBIT}>0 "
                    OBJOUTSTAND.FRMSTRING = "ONLYBILLOUTSTANDINGPAY"
                End If

            ElseIf RBREMINDER.Checked = True Then
                If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "REMINDERLETTERREC" Else OBJOUTSTAND.FRMSTRING = "REMINDERLETTERPAY"
                OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.BALANCE}>0"
                If CMBDELIVERYAT.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.DELIVERYAT}= '" & CMBDELIVERYAT.Text.Trim & "'"
                If CMBBROKERNAME.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.AGENT} = '" & CMBBROKERNAME.Text.Trim & "'"
                If cmbregister.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.REGTYPE}= '" & cmbregister.Text.Trim & "'"
                If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim)
                If Val(TXTOVERDUEDAYSLESS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} <= " & Val(TXTOVERDUEDAYSLESS.Text.Trim)

            ElseIf RBBROKEROUTSTANDING.Checked = True Then

                If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and (({@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim) & " AND {OUTSTANDINGREPORT_DETAILS.TYPE} <> 'RECEIPT') OR ({OUTSTANDINGREPORT_DETAILS.TYPE} = 'RECEIPT'))"
                'If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim)
                If Val(TXTOVERDUEDAYSLESS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} <= " & Val(TXTOVERDUEDAYSLESS.Text.Trim)
                If CHKSUMMARY.CheckState = CheckState.Checked Then
                    If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Else OBJOUTSTAND.FRMSTRING = "BROKEROUTSTANDINGPAYSUMM"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_ALL.BALANCE}>0"
                    If CMBBROKERNAME.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_ALL.AGENT} = '" & CMBBROKERNAME.Text.Trim & "'" Else OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_ALL.AGENT} <> '' "
                Else
                    If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Else OBJOUTSTAND.FRMSTRING = "BROKEROUTSTANDINGPAYDTLS"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.BALANCE}>0"
                    If CHKPDC.CheckState = CheckState.Checked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.CHKPDC}= FALSE"
                    If CMBDELIVERYAT.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.DELIVERYAT}= '" & CMBDELIVERYAT.Text.Trim & "'"
                    If CMBBROKERNAME.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.AGENT} = '" & CMBBROKERNAME.Text.Trim & "'" Else OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.AGENT} <> '' "
                    If cmbregister.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.REGTYPE}= '" & cmbregister.Text.Trim & "'"
                End If

            ElseIf RBBROKERINVENTORY.Checked = True Then
                If CMBDELIVERYAT.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.DELIVERYAT}= '" & CMBDELIVERYAT.Text.Trim & "'"
                If CMBBROKERNAME.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.AGENT} = '" & CMBBROKERNAME.Text.Trim & "'"
                If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim)
                If Val(TXTOVERDUEDAYSLESS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} <= " & Val(TXTOVERDUEDAYSLESS.Text.Trim)
                If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "RECBROKERINVENTORYOUTSTANDING" Else OBJOUTSTAND.FRMSTRING = "PAYBROKERINVENTORYOUTSTANDING"
                OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.BALANCE}>0"
                If CHKPDC.CheckState = CheckState.Checked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.CHKPDC}= FALSE"
                If CHKPARTPAYMENT.CheckState = CheckState.Checked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.GRANDTOTAL}>0 and {OUTSTANDINGREPORT_DETAILS.RECAMT}>0"
                If cmbregister.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.REGTYPE}= '" & cmbregister.Text.Trim & "'"


            ElseIf RBBROKERINVENTORYRUNBAL.Checked = True Then
                If CMBDELIVERYAT.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.DELIVERYAT}= '" & CMBDELIVERYAT.Text.Trim & "'"
                If CMBBROKERNAME.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.AGENT} = '" & CMBBROKERNAME.Text.Trim & "'"
                If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim)
                If Val(TXTOVERDUEDAYSLESS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} <= " & Val(TXTOVERDUEDAYSLESS.Text.Trim)
                If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "RECBROKERINVENTORYOUTSTANDINGRUNBAL" Else OBJOUTSTAND.FRMSTRING = "PAYBROKERINVENTORYOUTSTANDINGRUNBAL"
                OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.BALANCE}>0"
                If CHKPDC.CheckState = CheckState.Checked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.CHKPDC}= FALSE"
                If CHKPARTPAYMENT.CheckState = CheckState.Checked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.GRANDTOTAL}>0 and {OUTSTANDINGREPORT_DETAILS.RECAMT}>0"
                If cmbregister.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.REGTYPE}= '" & cmbregister.Text.Trim & "'"


            ElseIf RBOLDNEWREPORT.Checked = True Then

                If RBREC.Checked = True Then
                    If CHKPARTPAYMENT.CheckState = CheckState.Unchecked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {RECEIPT_REPORT.PAYTYPE}='Against Bill'"
                    OBJOUTSTAND.FRMSTRING = "OLDNEWREC"
                    If CHKSUMMARY.CheckState = CheckState.Checked Then OBJOUTSTAND.SHOWDETAILS = 0 Else OBJOUTSTAND.SHOWDETAILS = 1
                Else
                    If CHKPARTPAYMENT.CheckState = CheckState.Unchecked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {PAYMENT_REPORT.PAYTYPE}='Against Bill'"
                    OBJOUTSTAND.FRMSTRING = "OLDNEWPAY"
                    If CHKSUMMARY.CheckState = CheckState.Checked Then OBJOUTSTAND.SHOWDETAILS = 0 Else OBJOUTSTAND.SHOWDETAILS = 1
                End If

            ElseIf RBOUTSTANDINGRUNBAL.Checked = True Then
                If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and (({@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim) & " AND {OUTSTANDINGREPORT_DETAILS.TYPE} <> 'RECEIPT') OR ({OUTSTANDINGREPORT_DETAILS.TYPE} = 'RECEIPT'))"
                'If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim)
                If Val(TXTOVERDUEDAYSLESS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} <= " & Val(TXTOVERDUEDAYSLESS.Text.Trim)
                If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "OUTSTANDINGRECRUNBALDTLS" Else OBJOUTSTAND.FRMSTRING = "OUTSTANDINGPAYRUNBALDTLS"
                OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.BALANCE}>0"
                If CHKPDC.CheckState = CheckState.Checked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.CHKPDC}= FALSE"
                If CHKPARTPAYMENT.CheckState = CheckState.Checked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.GRANDTOTAL}>0 and {OUTSTANDINGREPORT_DETAILS.RECAMT}>0"
                If CMBDELIVERYAT.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.DELIVERYAT}= '" & CMBDELIVERYAT.Text.Trim & "'"
                If CMBBROKERNAME.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.AGENT} = '" & CMBBROKERNAME.Text.Trim & "'"
                If cmbregister.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.REGTYPE}= '" & cmbregister.Text.Trim & "'"

            ElseIf RBINT.Checked = True Then
                If RBREC.Checked = True Then
                    If Val(TXTDAYS.Text.Trim) = 0 Or Val(TXTINTEREST.Text.Trim) = 0 Then
                        MsgBox("Enter Proper Days / Interest", MsgBoxStyle.Critical)
                        TXTINTEREST.Focus()
                        Exit Sub
                    End If
                    If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim)
                    If Val(TXTOVERDUEDAYSLESS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} <= " & Val(TXTOVERDUEDAYSLESS.Text.Trim)
                    OBJOUTSTAND.FRMSTRING = "INTOUTSTANDINGREC"
                Else
                    If Val(TXTDAYS.Text.Trim) = 0 Or Val(TXTINTEREST.Text.Trim) = 0 Then
                        MsgBox("Enter Proper Days / Interest", MsgBoxStyle.Critical)
                        TXTINTEREST.Focus()
                        Exit Sub
                    End If
                    If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} >= " & Val(TXTOVERDUEDAYS.Text.Trim)
                    If Val(TXTOVERDUEDAYSLESS.Text.Trim) > 0 Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DAYS} <= " & Val(TXTOVERDUEDAYSLESS.Text.Trim)
                    OBJOUTSTAND.FRMSTRING = "INTOUTSTANDINGPAY"
                End If
            End If

            If RBREC.Checked = True Or RBREMINDER.Checked = True Then
                If CMBGROUP.Text.Trim = "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@SECONDARY}='SUNDRY DEBTORS'" Else OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@PARTYGROUP}='" & CMBGROUP.Text.Trim & "'"
            Else
                If CMBGROUP.Text.Trim = "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@SECONDARY}='SUNDRY CREDITORS'" Else OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@PARTYGROUP}='" & CMBGROUP.Text.Trim & "'"
            End If

            If CMBPARTYNAME.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@NAME}='" & CMBPARTYNAME.Text.Trim & "'"
            If CHKMSME.CheckState = CheckState.Checked Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.MSMENO}<>''"

            If RBSELECTED.Checked = True And SENDMAIL = False And SENDWHATSAPP = False Then
                gridbill.ClearColumnsFilter()
                Dim NAMECLAUSE As String = ""
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then
                            If RBGROUP.Checked = True Then
                                NAMECLAUSE = " AND ({@PARTYGROUP} = '" & dtrow("NAME") & "'"
                            ElseIf RBACCOUNT.Checked = True Then
                                NAMECLAUSE = " AND ({@NAME} = '" & dtrow("NAME") & "'"
                            ElseIf RBAGENT.Checked = True Then
                                NAMECLAUSE = " AND ({@AGENT} = '" & dtrow("NAME") & "'"
                            End If
                        Else
                            If RBGROUP.Checked = True Then
                                NAMECLAUSE = NAMECLAUSE & " OR {@PARTYGROUP} = '" & dtrow("NAME") & "'"
                            ElseIf RBACCOUNT.Checked = True Then
                                NAMECLAUSE = NAMECLAUSE & " OR {@NAME} = '" & dtrow("NAME") & "'"
                            ElseIf RBAGENT.Checked = True Then
                                NAMECLAUSE = NAMECLAUSE & " OR {@AGENT} = '" & dtrow("NAME") & "'"
                            End If
                        End If
                    End If
                Next
                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & NAMECLAUSE
                End If
            Else
                'WHEN WHATSAPP OR MAIL IS SELECTED
                If GROUPNAME <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " AND ({@PARTYGROUP} = '" & GROUPNAME & "')"
                If PARTYNAME <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " AND ({@NAME} = '" & PARTYNAME & "')"
                If AGENTNAME <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " AND ({@AGENT} = '" & AGENTNAME & "')"
                OBJOUTSTAND.PARTYNAME = PARTYNAME
            End If


            If RBINT.Checked = True Then
                GBILL.ClearColumnsFilter()
                Dim BILLCLAUSE As String = ""
                For i As Integer = 0 To GBILL.RowCount - 1
                    Dim dtrow As DataRow = GBILL.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If BILLCLAUSE = "" Then BILLCLAUSE = " AND ({@BILL} = '" & dtrow("BILLINITIALS") & "'" Else BILLCLAUSE = BILLCLAUSE & " OR {@BILL} = '" & dtrow("BILLINITIALS") & "'"
                    End If
                Next
                If BILLCLAUSE <> "" Then
                    BILLCLAUSE = BILLCLAUSE & ")"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & BILLCLAUSE
                End If
            End If

            If RBBILLDATE.Checked = True Then OBJOUTSTAND.DAYS = "BILLDATE" Else OBJOUTSTAND.DAYS = "DUEDATE"
            If CHKADDRESS.Checked = True Then OBJOUTSTAND.ADDRESS = 1 Else OBJOUTSTAND.ADDRESS = 0
            If CHKPRINTDATE.Checked = True Then OBJOUTSTAND.SHOWPRINTDATE = 1 Else OBJOUTSTAND.SHOWPRINTDATE = 0
            If CHKNARRATION.Checked = True Then OBJOUTSTAND.SHOWREMARKS = 1 Else OBJOUTSTAND.SHOWREMARKS = 0
            If CHKGROUPONNEWPG.Checked = True Then OBJOUTSTAND.NEWPAGE = CHKGROUPONNEWPG.Checked
            OBJOUTSTAND.INTEREST = Val(TXTINTEREST.Text.Trim)
            OBJOUTSTAND.INTDAYS = Val(TXTDAYS.Text.Trim)

            OBJOUTSTAND.Show()
            If SENDMAIL = True Or SENDWHATSAPP = True Then OBJOUTSTAND.Close()


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub RBREC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBREC.Click
        Try
            CMBPARTYNAME.Text = ""
            CMBGROUP.Text = ""
            fillcmb(" AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            FILLGROUP(" AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBPAYABLE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBPAYABLE.Click
        Try
            CMBPARTYNAME.Text = ""
            CMBGROUP.Text = ""
            fillcmb(" AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'")
            FILLGROUP(" AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTINTEREST_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTINTEREST.KeyPress
        numdotkeypress(e, TXTINTEREST, Me)
    End Sub

    Private Sub TXTDAYS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTDAYS.KeyPress
        numkeypress(e, TXTDAYS, Me)
    End Sub

    Sub FILLGRID()
        Try
            If RBSELECTED.Checked = False Then Exit Sub
            Dim WHERECLAUSE As String = ""
            Dim GRIDWHERECLAUSE As String = ""

            If RBREC.Checked = True Then
                WHERECLAUSE = " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors'"
                GRIDWHERECLAUSE = " AND SECONDARY = 'Sundry Debtors'"
            Else
                WHERECLAUSE = " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'"
                GRIDWHERECLAUSE = " AND SECONDARY = 'Sundry Creditors'"
            End If
            Dim objclsCMST As New ClsCommonMaster


            If ClientName = "SUPRIYA" And Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND LEDGERS.ACC_CMPNAME IN (select DISTINCT NAME from OUTSTANDINGREPORT_DETAILS INNER JOIN REGISTERMASTER ON REGTYPE = REGISTER_NAME AND YEARID = REGISTER_YEARID WHERE YEARID = " & YearId & " AND REGISTER_TYPE IN ('SALE', 'PURCHASE') AND BALANCE > 0 AND DATEDIFF(DAY, DATE, GETDATE()) > " & Val(TXTOVERDUEDAYS.Text.Trim) & ")"


            Dim dt As New DataTable
            If RBACCOUNT.Checked = True Then
                'dt = objclsCMST.search(" CAST (0 AS BIT) AS CHK,LEDGERS.Acc_cmpname AS NAME, GROUPMASTER.group_secondary AS UNDER, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(AREAMASTER.AREA_NAME,'') AS AREA, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(SALESMAN_NAME,'') AS SALESMAN, ISNULL(GROUPOFCOMPANIESMASTER.GOC_NAME,'') AS GOC  ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN AREAMASTER ON LEDGERS.Acc_AREAid = AREAMASTER.AREA_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_STATEid = STATEMASTER.STATE_id LEFT OUTER JOIN SALESMANMASTER ON ACC_SALESMANID = SALESMAN_ID LEFT OUTER JOIN GROUPOFCOMPANIESMASTER ON LEDGERS.ACC_GOCID = GROUPOFCOMPANIESMASTER.GOC_ID", WHERECLAUSE & " AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
                dt = objclsCMST.search("  CAST(0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, ISNULL(LEDGERS.ACC_WHATSAPPNO, 0) AS PARTYWHATSAPP, GROUPMASTER.group_secondary AS UNDER, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(AREAMASTER.area_name, '') AS AREA, ISNULL(STATEMASTER.state_name, '') AS STATENAME, ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN, ISNULL(GROUPOFCOMPANIESMASTER.GOC_NAME, '') AS GOC, ISNULL(AGENTMASTER.Acc_cmpname, '') AS AGENT, ISNULL(AGENTMASTER.ACC_WHATSAPPNO, '') AS AGENTWHATSAPP, ISNULL(LEDGERS.Acc_email, '') AS PARTYEMAILID , ISNULL(AGENTMASTER.Acc_email, '') AS AGENTEMAILID  ", " ", " LEDGERS INNER JOIN (SELECT DISTINCT LEDGERID FROM OUTSTANDINGREPORT_DETAILS WHERE YEARID = " & YearId & "  AND BALANCE > 0 " & GRIDWHERECLAUSE & ") AS OUT ON LEDGERS.ACC_ID = OUT.LEDGERID INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON LEDGERS.ACC_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN AREAMASTER ON LEDGERS.Acc_areaid = AREAMASTER.area_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN GROUPOFCOMPANIESMASTER ON LEDGERS.ACC_GOCID = GROUPOFCOMPANIESMASTER.GOC_ID  ", WHERECLAUSE & " AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_YEARID = " & YearId & ") ORDER BY LEDGERS.Acc_cmpname")

            ElseIf RBAGENT.Checked = True Then
                dt = objclsCMST.search("  CAST (0 AS BIT) AS CHK,LEDGERS.Acc_cmpname AS NAME,'' AS AGENT ,ISNULL(LEDGERS.ACC_WHATSAPPNO,0) AS PARTYWHATSAPP ,ISNULL(LEDGERS.Acc_email,0) AS PARTYEMAIL, GROUPMASTER.group_secondary AS UNDER, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(AREAMASTER.AREA_NAME,'') AS AREA, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME , '' AS AGENTWHATSAPP , '' AS AGENTEMAILID  ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN AREAMASTER ON LEDGERS.Acc_AREAid = AREAMASTER.AREA_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_STATEid = STATEMASTER.STATE_id ", " AND LEDGERS.ACC_TYPE = 'AGENT' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            Else
                dt = objclsCMST.search(" CAST (0 AS BIT) AS CHK, group_name AS NAME, group_under AS UNDER, group_secondary AS CITY ", " ", " GROUPMASTER ", " AND (GROUPMASTER.GROUP_CMPID = '" & CmpId & "') AND (GROUPMASTER.GROUP_LOCATIONID = '" & Locationid & "') AND (GROUPMASTER.GROUP_YEARID = '" & YearId & "') ORDER BY group_name")
            End If
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLBILL()
        Try
            Dim WHERECLAUSE As String = ""
            If RBREC.Checked = True Then WHERECLAUSE = " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors'" Else WHERECLAUSE = " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'"
            Dim objclsCMST As New ClsCommonMaster

            Dim dt As New DataTable
            If RBREC.Checked = True Then
                dt = objclsCMST.search(" CAST (0 AS BIT) AS CHK, ISNULL(INVOICEMASTER.INVOICE_INITIALS,'') AS BILLINITIALS, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL,0) AS GRANDTOTAL ", " ", "LEDGERS INNER JOIN INVOICEMASTER ON LEDGERS.Acc_id = INVOICEMASTER.INVOICE_LEDGERID INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id ", WHERECLAUSE & " AND LEDGERS.Acc_cmpname= '" & CMBPARTYNAME.Text.Trim & "' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            Else
                dt = objclsCMST.search(" CAST (0 AS BIT) AS CHK, ISNULL(PURCHASEMASTER.BILL_INITIALS, '') AS BILLINITIALS, ISNULL(PURCHASEMASTER.BILL_GRANDTOTAL, 0) AS GRANDTOTAL ", " ", " LEDGERS INNER JOIN PURCHASEMASTER ON LEDGERS.Acc_id = PURCHASEMASTER.BILL_LEDGERID INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id ", WHERECLAUSE & " AND LEDGERS.Acc_cmpname= '" & CMBPARTYNAME.Text.Trim & "' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            End If
            GBILLDETAILS.DataSource = dt
            If dt.Rows.Count > 0 Then
                GBILL.FocusedRowHandle = GBILL.RowCount - 1
                GBILL.TopRowIndex = GBILL.RowCount - 15
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBACCOUNT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBACCOUNT.CheckedChanged
        FILLGRID()
    End Sub

    Private Sub RBGROUP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBGROUP.CheckedChanged
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTBILL_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTBILL.CheckedChanged
        Try
            If GBILLDETAILS.Visible = True Then
                For i As Integer = 0 To GBILL.RowCount - 1
                    Dim dtrow As DataRow = GBILL.GetDataRow(i)
                    dtrow("CHK") = CHKSELECTBILL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBSELECTED_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSELECTED.CheckedChanged
        gridbilldetails.Visible = True
        FILLGRID()
    End Sub


    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        gridbilldetails.Visible = False
    End Sub

    Private Sub CMBPARTYNAME_Validated(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Validated
        Try
            FILLBILL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            If gridbilldetails.Visible = True Then
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("CHK") = CHKSELECTALL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBPAYABLE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBPAYABLE.CheckedChanged
        FILLGRID()
    End Sub

    Private Sub RBREC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBREC.CheckedChanged
        FILLGRID()
    End Sub

    Private Sub CMBBROKERNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBROKERNAME.Enter
        Try
            If CMBBROKERNAME.Text.Trim = "" Then fillname(CMBBROKERNAME, False, " AND LEDGERS.ACC_TYPE = 'AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKERNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBROKERNAME.Validating
        Try
            If CMBBROKERNAME.Text.Trim <> "" Then namevalidate(CMBBROKERNAME, cmbacccode, e, Me, txtadd, " AND LEDGERS.ACC_TYPE = 'AGENT' ", "SUNDRY CREDITORS", "AGENT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OutstandingFilter_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        RBBILLDATE.Checked = True
        If ClientName <> "SUPRIYA" Then chkdate.CheckState = CheckState.Checked

        If ClientName = "SHREENAKODA" Then dtfrom.Value = "01/04/2000" Else dtfrom.Value = AccFrom

        If ClientName = "AVIS" Or ClientName = "SUPEEMA" Then RBOUTSTANDINGRUNBAL.Checked = True
        If ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then RBINVENTORY.Checked = True

        If ClientName = "ALENCOT" Then
            RBOUTSTANDINGRUNBAL.Checked = True
            chkdate.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub CMBPARTYNAME_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBPARTYNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If RBREC.Checked = True Then OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'" Else OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBPARTYNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDWHATSAPP_Click(sender As Object, e As EventArgs) Handles CMDWHATSAPP.Click
        Try
            If MsgBox("Send WhatsApp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            DTMAIL.Rows.Clear()
            DTWHATSAPP.Rows.Clear()

            If RBSELECTED.Checked = True Then
                gridbill.ClearColumnsFilter()
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If RBGROUP.Checked = True Then
                            Call cmdshow_Click(sender, e, True, False, dtrow("NAME"), "", "")
                        ElseIf RBACCOUNT.Checked = True Then
                            Call cmdshow_Click(sender, e, True, False, "", dtrow("NAME"), "")
                        ElseIf RBAGENT.Checked = True Then
                            Call cmdshow_Click(sender, e, True, False, "", "", dtrow("NAME"))
                        End If
                        ALATTACHMENT.Add(Application.StartupPath & "\" & dtrow("NAME") & "_OUTSTANDING" & ".PDF")
                        FILENAME.Add(dtrow("NAME") & "_OUTSTANDING.pdf")
                        'DTMAIL.Rows.Add(ROW("NAME"), ROW("PARTYEMAIL"), ROW("AGENT"), ROW("AGENTEMAIL"), UCase(CmpName) & " - OUTSTANDING ", Application.StartupPath & "\" & ROW("NAME") & "_OUTSTANDING.pdf", ROW("NAME") & "_OUTSTANDING.pdf")
                        DTWHATSAPP.Rows.Add(dtrow("NAME"), dtrow("PARTYWHATSAPP"), dtrow("AGENT"), dtrow("AGENTWHATSAPP"), UCase(CmpName) & " - OUTSTANDING ", Application.StartupPath & "\" & dtrow("NAME") & "_OUTSTANDING.pdf", dtrow("NAME") & "_OUTSTANDING.pdf")
                    End If
                Next
            End If





            'ADDINT IN DTEMAIL
            'DTMAIL.Rows.Add(ROW("SRNO"), 0, "", ROW("SRNO"), ROW("DATE"), ROW("CMPNAME"), ROW("PARTYEMAIL"), ROW("AGENT"), ROW("AGENTEMAIL"), 0, UCase(CmpName) & " - Challan No. " & ROW("SRNO") & " Dated " & ROW("DATE"), oDfDopt.DiskFileName, ROW("CMPNAME") & "GDN_" & ROW("SRNO") & ".pdf")

            'ADDING IN DTWHATSAPP
            'DTWHATSAPP.Rows.Add(0, "", ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("AGENT"), ROW("AGENTWHATSAPP"), 0, 0, "", UCase(CmpName) & " - OUSTSTANDING No. ", ROW("NAME") & "_OUSTSTANDING.pdf", 0)
            'DTWHATSAPP.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("REGID"), cmbregister.Text.Trim, ROW("PRINTINITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("AGENTNAME"), ROW("AGENTWHATSAPP"), Val(ROW("GRANDTOTAL")), UCase(CmpName) & " - Invoice No. " & ROW("PRINTINITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf")


            'If INVOICEMAIL = True Then
            '    If DTMAIL.Rows.Count = 0 Then Exit Sub
            '    Dim OBJEMAIL As New SendMultipleMail
            '    OBJEMAIL.FORMTYPE = "EMAIL"
            '    OBJEMAIL.DT = DTMAIL
            '    OBJEMAIL.ShowDialog()
            '    Exit Sub
            'End If


            If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
            Dim OBJWHATSAPP As New SendMultipleWhatsapp
            OBJWHATSAPP.PATH = ALATTACHMENT
            OBJWHATSAPP.FILENAME = FILENAME
            OBJWHATSAPP.DT = DTWHATSAPP
            OBJWHATSAPP.ShowDialog()


            'If MsgBox("Wish to Whats'app ?", MsgBoxStyle.YesNo) = vbYes Then
            '    If RBREC.Checked = True Then FRMSTRING = "OUTSTANDINGRECDTLS" Else FRMSTRING = "OUTSTANDINGPAYDTLS"
            '    SERVERPROPSELECTED(False, 1, "", True)
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDMAIL_Click(sender As Object, e As EventArgs) Handles CMDMAIL.Click
        Try
            If MsgBox("Send Mail ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            DTMAIL.Rows.Clear()
            DTWHATSAPP.Rows.Clear()

            If RBSELECTED.Checked = True Then
                gridbill.ClearColumnsFilter()
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If RBGROUP.Checked = True Then
                            Call cmdshow_Click(sender, e, False, True, dtrow("NAME"), "", "")
                        ElseIf RBACCOUNT.Checked = True Then
                            Call cmdshow_Click(sender, e, False, True, "", dtrow("NAME"), "")
                        ElseIf RBAGENT.Checked = True Then
                            Call cmdshow_Click(sender, e, False, True, "", "", dtrow("NAME"))
                        End If
                        ALATTACHMENT.Add(Application.StartupPath & "\" & dtrow("NAME") & "_OUTSTANDING" & ".PDF")
                        FILENAME.Add(dtrow("NAME") & "_OUTSTANDING.pdf")
                        DTMAIL.Rows.Add(dtrow("NAME"), dtrow("PARTYEMAILID"), dtrow("AGENT"), dtrow("AGENTEMAILID"), UCase(CmpName) & " - OUTSTANDING ", Application.StartupPath & "\" & dtrow("NAME") & "_OUTSTANDING.pdf", dtrow("NAME") & "_OUTSTANDING.pdf")
                    End If
                Next
            End If


            If DTMAIL.Rows.Count = 0 Then Exit Sub
            Dim OBJEMAIL As New SendMultipleMail
            OBJEMAIL.FORMTYPE = "Outstanding"
            OBJEMAIL.DT = DTMAIL
            OBJEMAIL.ShowDialog()
            Exit Sub



            'If MsgBox("Wish to Whats'app ?", MsgBoxStyle.YesNo) = vbYes Then
            '    If RBREC.Checked = True Then FRMSTRING = "OUTSTANDINGRECDTLS" Else FRMSTRING = "OUTSTANDINGPAYDTLS"
            '    SERVERPROPSELECTED(False, 1, "", True)
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPSELECTED(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal NOOFCOPIES As Integer = 1, Optional ByVal FRMSTRING As String = "PRINT", Optional ByVal WHATSAPP As Boolean = False)
        Try
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            DTMAIL.Rows.Clear()
            DTWHATSAPP.Rows.Clear()

            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If

            For I As Integer = 0 To gridbill.RowCount - 1
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                If ROW("CHK") = True Then
                    '**************** SET SERVER ************************
                    Dim crParameterFieldDefinitions As ParameterFieldDefinitions
                    Dim crParameterFieldDefinition As ParameterFieldDefinition
                    Dim crParameterValues As New ParameterValues
                    Dim crParameterDiscreteValue As New ParameterDiscreteValue

                    Dim crtableLogonInfo As New TableLogOnInfo
                    Dim crConnecttionInfo As New ConnectionInfo
                    Dim crTables As Tables
                    Dim crTable As Table

                    With crConnecttionInfo
                        .ServerName = SERVERNAME
                        .DatabaseName = DatabaseName
                        .UserID = DBUSERNAME
                        .Password = Dbpassword
                        .IntegratedSecurity = Dbsecurity
                    End With

                    Dim expo As New ExportOptions
                    Dim oDfDopt As New DiskFileDestinationOptions

                    Dim OBJ As New Object
                    If FRMSTRING = "OUTSTANDINGPAYDTLS" Then
                        strsearch = "  {OUTSTANDINGREPORT_DETAILS.NAME} = '" & ROW("NAME") & "' AND {OUTSTANDINGREPORT_DETAILS.yearid} = " & YearId
                        OBJ = New OutstandingReport_Details_Pay
                    Else
                        FRMSTRING = "OUTSTANDINGRECDTLS"
                        strsearch = "  {OUTSTANDINGREPORT_DETAILS.NAME} = '" & ROW("NAME") & "' AND {OUTSTANDINGREPORT_DETAILS.yearid} = " & YearId
                        OBJ = New OutstandingReport_Details_Rec
                    End If

                    crTables = OBJ.Database.Tables
                    For Each crTable In crTables
                        crtableLogonInfo = crTable.LogOnInfo
                        crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                        crTable.ApplyLogOnInfo(crtableLogonInfo)
                    Next

                    OBJ.RecordSelectionFormula = strsearch

                    If FRMSTRING = "PRINT" Then
                        OBJ.PrintOptions.PrinterName = PRINTDIALOG.PrinterSettings.PrinterName
                        If ClientName <> "AVIS" Then OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize Else OBJ.PrintOptions.PaperSize = PaperSize.PaperA5
                        OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                    Else
                        oDfDopt.DiskFileName = Application.StartupPath & "\" & ROW("NAME") & "_OUTSTANDING.pdf"
                        expo = OBJ.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        OBJ.Export()
                        ALATTACHMENT.Add(oDfDopt.DiskFileName)
                        FILENAME.Add(ROW("NAME") & "_OUTSTANDING.pdf")

                        'ADDINT IN DTEMAIL
                        'DTMAIL.Rows.Add(ROW("SRNO"), 0, "", ROW("SRNO"), ROW("DATE"), ROW("CMPNAME"), ROW("PARTYEMAIL"), ROW("AGENT"), ROW("AGENTEMAIL"), 0, UCase(CmpName) & " - Challan No. " & ROW("SRNO") & " Dated " & ROW("DATE"), oDfDopt.DiskFileName, ROW("CMPNAME") & "GDN_" & ROW("SRNO") & ".pdf")
                        DTMAIL.Rows.Add(ROW("NAME"), ROW("PARTYEMAIL"), ROW("AGENT"), ROW("AGENTEMAIL"), UCase(CmpName) & " - OUTSTANDING ", Application.StartupPath & "\" & ROW("NAME") & "_OUTSTANDING.pdf", ROW("NAME") & "_OUTSTANDING.pdf")

                        'ADDING IN DTWHATSAPP
                        'DTWHATSAPP.Rows.Add(0, "", ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("AGENT"), ROW("AGENTWHATSAPP"), 0, 0, "", UCase(CmpName) & " - OUSTSTANDING No. ", ROW("NAME") & "_OUSTSTANDING.pdf", 0)
                        DTWHATSAPP.Rows.Add(ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("AGENT"), ROW("AGENTWHATSAPP"), UCase(CmpName) & " - OUTSTANDING ", Application.StartupPath & "\" & ROW("NAME") & "_OUTSTANDING.pdf", ROW("NAME") & "_OUTSTANDING.pdf")
                            'DTWHATSAPP.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("REGID"), cmbregister.Text.Trim, ROW("PRINTINITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("AGENTNAME"), ROW("AGENTWHATSAPP"), Val(ROW("GRANDTOTAL")), UCase(CmpName) & " - Invoice No. " & ROW("PRINTINITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf")


                        End If

                    End If
            Next

            If INVOICEMAIL = True Then
                If DTMAIL.Rows.Count = 0 Then Exit Sub
                Dim OBJEMAIL As New SendMultipleMail
                OBJEMAIL.FORMTYPE = "EMAIL"
                OBJEMAIL.DT = DTMAIL
                OBJEMAIL.ShowDialog()
                Exit Sub
            End If



            If WHATSAPP = True Then
                If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
                Dim OBJWHATSAPP As New SendMultipleWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.DT = DTWHATSAPP
                OBJWHATSAPP.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class