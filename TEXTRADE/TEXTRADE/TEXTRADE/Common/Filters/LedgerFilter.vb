
Imports System.IO
Imports BL

Public Class LedgerFilter

    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String
    Public FRMSTRING As String
    Dim DTMAIL As New DataTable
    Dim DTWHATSAPP As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub FILLGROUP(ByVal WHERECLAUSE As String)
        Try
            If CMBGROUP.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("group_name", "", "GroupMaster", " and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId & WHERECLAUSE)
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

    Sub FILLCMB()
        Try
            If cmbname.Text.Trim = "" Then FILLNAME(cmbname, False, "")
            If CMBGROUP.Text.Trim = "" Then FILLGROUP("")
            If CMBGROUPOFCOMPANIES.Text.Trim = "" Then FILLGROUPCOMPANY(CMBGROUPOFCOMPANIES)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGROUPOFCOMPANIES_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGROUPOFCOMPANIES.Enter
        Try
            If CMBGROUPOFCOMPANIES.Text.Trim = "" Then FILLGROUPCOMPANY(CMBGROUPOFCOMPANIES)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGROUPOFCOMPANIES_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGROUPOFCOMPANIES.Validating
        Try
            If CMBGROUPOFCOMPANIES.Text.Trim <> "" Then GROUPCOMPANYVALIDATE(CMBGROUPOFCOMPANIES, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub LedgerFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgerFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
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


            FILLCMB()
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

    Private Sub RBACCOUNT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBACCOUNT.CheckedChanged
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST (0 AS BIT) AS CHK,LEDGERS.Acc_cmpname AS NAME, GROUPMASTER.group_secondary AS UNDER, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(LEDGERS.ACC_WHATSAPPNO,'') AS PARTYWHATSAPP, ISNULL(AGENTLEDGERS.ACC_WHATSAPPNO,'') AS AGENTWHATSAPP  ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.ACC_ID ", " AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
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
                        Call cmdshow_Click(sender, e, True, False, "", dtrow("NAME"), "")
                        ALATTACHMENT.Add(Application.StartupPath & "\" & dtrow("NAME") & "_LEDGER" & ".PDF")
                        FILENAME.Add(dtrow("NAME") & "_LEDGER.pdf")
                        DTWHATSAPP.Rows.Add(dtrow("NAME"), dtrow("PARTYWHATSAPP"), dtrow("AGENTNAME"), dtrow("AGENTWHATSAPP"), UCase(CmpName) & " - LEDGER ", Application.StartupPath & "\" & dtrow("NAME") & "_LEDGER.pdf", dtrow("NAME") & "_LEDGER.pdf")
                    End If
                Next
            End If


            If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
            Dim OBJWHATSAPP As New SendMultipleWhatsapp
            OBJWHATSAPP.PATH = ALATTACHMENT
            OBJWHATSAPP.FILENAME = FILENAME
            OBJWHATSAPP.DT = DTWHATSAPP
            OBJWHATSAPP.ShowDialog()

            If ClientName = "SNCM" Then
                For Each filePath As String In OBJWHATSAPP.PATH
                    If File.Exists(filePath) Then
                        File.Delete(filePath)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBGROUP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBGROUP.CheckedChanged
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST (0 AS BIT) AS CHK, group_name AS NAME, group_under AS UNDER, group_secondary AS CITY ", " ", " GROUPMASTER ", " AND (GROUPMASTER.GROUP_CMPID = '" & CmpId & "') AND (GROUPMASTER.GROUP_LOCATIONID = '" & Locationid & "') AND (GROUPMASTER.GROUP_YEARID = '" & YearId & "') ORDER BY group_name")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBSELECTED_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSELECTED.CheckedChanged
        gridbilldetails.Visible = True
    End Sub

    Private Sub RBALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBALL.CheckedChanged
        gridbilldetails.Visible = False
    End Sub

    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional SENDWHATSAPP As Boolean = False, Optional SENDMAIL As Boolean = False, Optional GROUPNAME As String = "", Optional PARTYNAME As String = "", Optional AGENTNAME As String = "") Handles cmdshow.Click
        Try
            Dim objreg As New registerdesign
            objreg.PARTYNAME = cmbname.Text.Trim
            objreg.DIRECTMAIL = SENDMAIL
            objreg.DIRECTWHATSAPP = SENDWHATSAPP
            If SENDWHATSAPP = True Or SENDMAIL = True Then objreg.DIRECTPRINT = True

            If RBPARTYSTATEMENT.Checked = True Or RBPARTYSTATEMENTDTLS.Checked = True Then

                'GET OPENING BALANCE 
                'getting opening balances
                Dim OPENING As Double
                Dim ALPARAVAL As New ArrayList
                Dim OBJCOMMON As New ClsCommonMaster
                Dim DT As New DataTable
                Dim WHERECLAUSE As String = ""
                Dim PURWHERECLAUSE As String = ""

                If chkdate.CheckState = CheckState.Checked Then
                    DT = OBJCOMMON.search(" SUM(DR)-SUM(CR)", "", " Register_Grouped", " and name = '" & cmbname.Text.Trim & "' and acc_billdate <'" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "' and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and YEARID = " & YearId)
                    objreg.FROMDATE = dtfrom.Value.Date
                    objreg.TODATE = dtto.Value.Date
                    objreg.PERIOD = Format(dtfrom.Value.Date, "dd/MM/yyyy") & " - " & Format(dtto.Value.Date, "dd/MM/yyyy")
                ElseIf chkdate.CheckState = CheckState.Unchecked Then
                    DT = OBJCOMMON.search(" SUM(DR)-SUM(CR)", "", " Register_Grouped", " and name = '" & cmbname.Text.Trim & "' and acc_billdate <'" & Format(AccFrom, "MM/dd/yyyy") & "' and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and YEARID = " & YearId)
                    WHERECLAUSE = " AND INVOICE_DATE >= '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND INVOICE_DATE <= '" & Format(AccTo.Date, "MM/dd/yyyy") & "'"
                    objreg.FROMDATE = AccFrom
                    objreg.TODATE = AccTo
                    objreg.PERIOD = Format(AccFrom.Date, "dd/MM/yyyy") & " - " & Format(AccTo.Date, "dd/MM/yyyy")
                End If

                If DT.Rows.Count > 0 Then OPENING = Val(DT.Rows(0).Item(0).ToString)

                'THIS CODE IS WRITTEN COZ ABOVE CODE DOES NT RETRIEVE OPBAL IF DATE IS FROM 1ST DAY OF ACCOUNTING YEAR
                'DONT DELETE THIS CODE IT IS CHECKED AND WORKING FINE
                If Val(OPENING) = 0 Then
                    DT = OBJCOMMON.search("ACC_OPBAL, ACC_DRCR", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then OPENING = Val(DT.Rows(0).Item(0).ToString)
                End If



                objreg.SHOWHEADER = CHKHEADER.Checked
                objreg.OPENING = OPENING
                If RBPARTYSTATEMENT.Checked = True Then objreg.FRMSTRING = "PARTYSTATEMENT" Else objreg.FRMSTRING = "PARTYSTATEMENTDTLS"
                objreg.strsearch = " {REGISTER_GROUPED.Yearid} = " & YearId & " AND {REGISTER_GROUPED.name} = '" & cmbname.Text.Trim & "'"
                objreg.MdiParent = MDIMain
                objreg.Show()

                Exit Sub
            End If



            Dim tempmsg As Integer = vbNo

            If chkdate.Checked = True Then
                objreg.FROMDATE = dtfrom.Value.Date
                objreg.TODATE = dtto.Value.Date
                objreg.PERIOD = "LEDGER BOOK (" & Format(dtfrom.Value.Date, "dd/MM/yyyy") & " - " & Format(dtto.Value.Date, "dd/MM/yyyy") & ")"
            Else
                objreg.FROMDATE = AccFrom
                objreg.TODATE = AccTo
                objreg.PERIOD = "LEDGER BOOK (" & Format(AccFrom.Date, "dd/MM/yyyy") & " - " & Format(AccTo.Date, "dd/MM/yyyy") & ")"
            End If

            If ClientName = "SAKARIA" And CMBGROUPOFCOMPANIES.Text.Trim <> "" Then objreg.PERIOD = CMBGROUPOFCOMPANIES.Text.Trim & " - " & objreg.PERIOD

            If RBSUMMARY.Checked = True Then
                objreg.FRMSTRING = "LedgerBook"
            ElseIf RBDETAILS.Checked = True Then
                objreg.FRMSTRING = "LedgerBookDetails"
            ElseIf RBPARTYMONTHLY.Checked = True Then
                objreg.FRMSTRING = "LedgerBookMonthlyTypeSumm"
                If MsgBox("Print Letter Format", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then objreg.LETTERFORMAT = 1 Else objreg.LETTERFORMAT = 0
            ElseIf RBTFORMAT.Checked = True Then
                objreg.FRMSTRING = "LedgerBookTFormat"
            ElseIf RBCONFIRMATION.Checked = True Then
                objreg.FRMSTRING = "LedgerBookConfirm"
            ElseIf RBCONFIRMATIONSUMM.Checked = True Then
                objreg.FRMSTRING = "LedgerBookConfirmSumm"
            ElseIf RBPARTYSUMM.Checked = True Then
                objreg.FRMSTRING = "LedgerPartySumm"
            ElseIf RBSUMMRUNBAL.Checked = True Then
                objreg.FRMSTRING = "LedgerBookRunBal"
            End If

            'objreg.strsearch = "{LEDGERBOOK.ACC_Type} <> 'OPENING' AND {LEDGERBOOK.Yearid} = " & YearId
            objreg.strsearch = "{LEDGERBOOK.Yearid} = " & YearId
            If cmbname.Text.Trim <> "" Then objreg.strsearch = objreg.strsearch & " AND {LEDGERBOOK.Name} = '" & cmbname.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then
                objreg.strsearch = objreg.strsearch & " AND {LEDGERBOOK.GROUP_Name} = '" & CMBGROUP.Text.Trim & "'"
                objreg.PERIOD = CMBGROUP.Text.Trim & " - " & objreg.PERIOD
            End If
            If CMBGROUPOFCOMPANIES.Text.Trim <> "" Then objreg.strsearch = objreg.strsearch & " AND {LEDGERBOOK.GROUPOFCOMPANIES} = '" & CMBGROUPOFCOMPANIES.Text.Trim & "'"
            getFromToDate()

            objreg.CLOSINGDRCR = lbldrcrclosing.Text

            If TXTAMT.Text <> "" And CMBSIGN.Text <> "" Then
                If CMBSIGN.Text = "=" Then objreg.strsearch = objreg.strsearch & " and ({LEDGERBOOK.DR} =" & Val(TXTAMT.Text.Trim) & " OR {LEDGERBOOK.CR} =" & Val(TXTAMT.Text.Trim) & ") "
                If CMBSIGN.Text = ">" Then objreg.strsearch = objreg.strsearch & " and ({LEDGERBOOK.DR} >" & Val(TXTAMT.Text.Trim) & " OR {LEDGERBOOK.CR} >" & Val(TXTAMT.Text.Trim) & ") "
                If CMBSIGN.Text = "<" Then objreg.strsearch = objreg.strsearch & " and ({LEDGERBOOK.DR} <" & Val(TXTAMT.Text.Trim) & " OR {LEDGERBOOK.CR} <" & Val(TXTAMT.Text.Trim) & ")"
                If CMBSIGN.Text = ">=" Then objreg.strsearch = objreg.strsearch & " and ({LEDGERBOOK.DR} >=" & Val(TXTAMT.Text.Trim) & " OR {LEDGERBOOK.CR} >=" & Val(TXTAMT.Text.Trim) & ") "
                If CMBSIGN.Text = "<=" Then objreg.strsearch = objreg.strsearch & " and ({LEDGERBOOK.DR} <=" & Val(TXTAMT.Text.Trim) & " OR {LEDGERBOOK.CR} <=" & Val(TXTAMT.Text.Trim) & ") "
                If CMBSIGN.Text = "<>" Then objreg.strsearch = objreg.strsearch & " and ({LEDGERBOOK.DR} <>" & Val(TXTAMT.Text.Trim) & " AND {LEDGERBOOK.CR} <>" & Val(TXTAMT.Text.Trim) & ") "
            End If

            If RBSELECTED.Checked = True And SENDMAIL = False And SENDWHATSAPP = False Then
                gridbill.ClearColumnsFilter()
                Dim NAMECLAUSE As String = ""
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then
                            If RBACCOUNT.Checked = True Then NAMECLAUSE = " AND ({LEDGERBOOK.Name} = '" & dtrow("NAME") & "'" Else NAMECLAUSE = " AND ({LEDGERBOOK.GROUP_Name} = '" & dtrow("NAME") & "'"
                        Else
                            If RBACCOUNT.Checked = True Then NAMECLAUSE = NAMECLAUSE & " OR {LEDGERBOOK.Name} = '" & dtrow("NAME") & "'" Else NAMECLAUSE = NAMECLAUSE & " OR {LEDGERBOOK.GROUP_Name} = '" & dtrow("NAME") & "'"
                        End If
                    End If
                Next
                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                    objreg.strsearch = objreg.strsearch & NAMECLAUSE
                End If
            Else
                'WHEN WHATSAPP OR MAIL IS SELECTED
                If PARTYNAME <> "" Then objreg.strsearch = objreg.strsearch & " AND ({LEDGERBOOK.Name} = '" & PARTYNAME & "') "
                objreg.PARTYNAME = PARTYNAME
            End If


            objreg.NEWPAGE = CHKGROUPONNEWPG.CheckState
            objreg.SHOWNARRATION = CHKREMARKS.Checked
            If CHKADDRESS.Checked = True Then objreg.ADDRESS = 1 Else objreg.ADDRESS = 0
            If CHKPANNO.Checked = True Then objreg.PANNO = 1 Else objreg.PANNO = 0

            If cmbname.Text.Trim <> "" Then objreg.PARTYNAME = cmbname.Text.Trim
            objreg.MdiParent = MDIMain
            objreg.Show()
            If SENDMAIL = True Or SENDWHATSAPP = True Then objreg.Close()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgerFilter_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "KCRAYON" Or ClientName = "SAKARIA" Then CHKREMARKS.CheckState = CheckState.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = ""
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class