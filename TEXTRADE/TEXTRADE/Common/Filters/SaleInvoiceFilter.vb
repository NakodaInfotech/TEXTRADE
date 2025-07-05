
Imports System.ComponentModel
Imports BL

Public Class SaleInvoiceFilter

    Dim edit As Boolean
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHIPTONAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSHIPTONAME.Enter
        Try
            If CMBSHIPTONAME.Text.Trim = "" Then fillname(CMBSHIPTONAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, edit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJQ As New SelectQuality
                OBJQ.FRMSTRING = "QUALITY"
                OBJQ.ShowDialog()
                If OBJQ.TEMPNAME <> "" Then CMBQUALITY.Text = OBJQ.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Sub FILLCMB()
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            If CMBSHIPTONAME.Text.Trim = "" Then FILLNAME(CMBSHIPTONAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            If CMBSUPPNAME.Text.Trim = "" Then FILLNAME(CMBSUPPNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'")
            If CMBAGENT.Text.Trim = "" Then FILLNAME(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
            If CMBLOCALTRANS.Text.Trim = "" Then FILLNAME(CMBLOCALTRANS, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, edit)
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
            If CMBCATEGORY.Text.Trim = "" Then fillCATEGORY(CMBCATEGORY, False)
            If CMBTERM.Text.Trim = "" Then fillTERM(CMBTERM, False)
            fillregister(cmbregister, " and register_type = 'SALE'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaleInvoiceFilter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub SaleInvoiceFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fillcmb()


            CMBSUPPNAME.Text = ""
            CMBLOCALTRANS.Text = ""

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, GROUPMASTER.GROUP_NAME AS GROUPNAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA, ISNULL(SALESMANMASTER.SALESMAN_NAME,'') AS SALESMAN, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.ACC_AREAID = AREAMASTER.AREA_ID LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.ACC_ID", " AND (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR LEDGERS.ACC_TYPE = 'AGENT') AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If


            Dim DT1 As DataTable = OBJCMN.search(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME", " ", " LEDGERS ", " AND  LEDGERS.ACC_TYPE = 'AGENT'  AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            GRIDBILLDETAILSAGENT.DataSource = DT1
            If DT1.Rows.Count > 0 Then
                GRIDBILLAGENT.FocusedRowHandle = GRIDBILLAGENT.RowCount - 1
                GRIDBILLAGENT.TopRowIndex = GRIDBILLAGENT.RowCount - 15
            End If

            Dim DT2 As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK,  ISNULL(INVOICE_NO, 0) AS INVOICENO, ISNULL(LEDGERS.ACC_CMPNAME,'') AS NAME, ISNULL(TERMMASTER.TERM_NAME,'') AS TERMS", " ", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN TERMMASTER ON INVOICEMASTER.INVOICE_TERMID = TERMMASTER.TERM_ID", " AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER.INVOICE_NO")
            GRIDINVOICEDETAILS.DataSource = DT2
            If DT2.Rows.Count > 0 Then
                GRIDINVOICE.FocusedRowHandle = GRIDINVOICE.RowCount - 1
                GRIDINVOICE.TopRowIndex = GRIDINVOICE.RowCount - 15
            End If

            ' Dim OBJCMN As New ClsCommon
            Dim dt3 As DataTable = OBJCMN.SEARCH("CMP_NAME", "", "CMPMASTER", "")
            For Each DROW As DataRow In dt3.Rows
                LSTCMP.Items.Add(DROW(0).ToString)
                If DROW(0) = CmpName Then LSTCMP.SetItemChecked(LSTCMP.Items.Count - 1, True)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdshow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try

            If RBINVGDNDIFF.Checked = True Then
                Dim OBJINV As New InvoiceGDNMtrsDiffReport
                OBJINV.MdiParent = MDIMain
                OBJINV.Show()
                Exit Sub
            End If

            If RBITEMRATE.Checked = True Then
                Dim OBJRATE As New PurchaseItemRateReport
                If CMBNAME.Text <> "" Then OBJRATE.WHERECLAUSE = OBJRATE.WHERECLAUSE & " And LEDGERS.ACC_CMPNAME ='" & CMBNAME.Text.Trim & "'"
                If CMBITEMNAME.Text <> "" Then OBJRATE.WHERECLAUSE = OBJRATE.WHERECLAUSE & " and ITEMMASTER.ITEM_NAME='" & CMBITEMNAME.Text.Trim & "'"
                If cmbregister.Text <> "" Then OBJRATE.WHERECLAUSE = OBJRATE.WHERECLAUSE & " and REGISTERMASTER.REGISTER_NAME='" & cmbregister.Text.Trim & "'"
                OBJRATE.MdiParent = MDIMain
                OBJRATE.FRMSTRING = "SALE"
                OBJRATE.Show()
                Exit Sub
            End If


            Dim OBJSALE As New SaleInvoiceDesign
            OBJSALE.MdiParent = MDIMain



            'IF CHK IS TRUE THEN GET ALL YEARID UDER THAT COMPANY
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




            If RBMONTHLYPURSALE.Checked = True Then

                OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " {COMMAND.YEARID} in [" & CMPCLAUSE & "]"
                'OBJSALE.WHERECLAUSE = " {COMMAND.YEARID}=" & YearId

                If chkdate.Checked = True Then
                    getFromToDate()
                    OBJSALE.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                    OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
                Else
                    OBJSALE.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
                End If
                OBJSALE.FRMSTRING = "MONTHLYPURSALE"
                OBJSALE.PERIOD = " PURCHASE & SALE MONTHLY SUMMARY - " & OBJSALE.PERIOD
                OBJSALE.Show()
                Exit Sub
            End If





            Dim NAMECLAUSE As String = ""

            'IF LABEL PRINT IS SELECTED THEN PRINT ONLYN THOSE LEDGERS WHOSE INVOICES ARE CREATED WITHIN THAT DATE
            'ELSE PRINT ALL LEDGERS (SUNDRY DEBTORS ONLY)
            '100X75 BARCODE PRINTER IS ADDED
            If RDBLABEL.Checked = True Then

                If ClientName = "SUPRIYA" Or ClientName = "INDRAPUJAFABRICS" Then
                    OBJSALE.FRMSTRING = "LABEL"
                Else
                    OBJSALE.FRMSTRING = "100X75"
                End If

                OBJSALE.WHERECLAUSE = " {LEDGERS.ACC_YEARID} = " & YearId
                Dim TEMPCONDITION As String = " AND INVOICE_YEARID = " & YearId
                If chkdate.CheckState = CheckState.Checked Then TEMPCONDITION = TEMPCONDITION & " AND INVOICE_DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "' AND INVOICE_DATE <= '" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
                If Val(TXTFROM.Text.Trim) > 0 Then TEMPCONDITION = TEMPCONDITION & " AND INVOICEMASTER.INVOICE_NO >= " & Val(TXTFROM.Text.Trim)
                If Val(TXTTO.Text.Trim) > 0 Then TEMPCONDITION = TEMPCONDITION & " AND INVOICEMASTER.INVOICE_NO <= " & Val(TXTTO.Text.Trim)

                If chkdate.CheckState = CheckState.Checked Or Val(TXTFROM.Text.Trim) > 0 Or Val(TXTTO.Text.Trim) > 0 Then
                    DT = OBJCMN.SEARCH("DISTINCT LEDGERS.ACC_CMPNAME AS NAME", "", " INVOICEMASTER INNER JOIN LEDGERS ON ACC_ID = INVOICE_LEDGERID ", TEMPCONDITION)
                    For Each DTROW As DataRow In DT.Rows
                        If NAMECLAUSE = "" Then NAMECLAUSE = " AND ({LEDGERS.ACC_CMPNAME} = '" & DTROW("NAME") & "'" Else NAMECLAUSE = NAMECLAUSE & " OR {LEDGERS.ACC_CMPNAME} = '" & DTROW("NAME") & "'"
                    Next
                End If

                'FOR PARTYNAME
                gridbill.ClearColumnsFilter()
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then NAMECLAUSE = " AND ({LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'" Else NAMECLAUSE = NAMECLAUSE & " OR {LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                    End If
                Next

                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                    OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & NAMECLAUSE
                End If

                OBJSALE.Show()
                Exit Sub
            End If


            If RBENVELOPE.Checked = True Then
                Dim OBJENV As New payment_advice
                OBJENV.FRMSTRING = "ENVELOPE"
                OBJENV.MdiParent = MDIMain
                OBJENV.WHERECLAUSE = " {LEDGERS.ACC_YEARID} = " & YearId

                'FOR PARTYNAME
                gridbill.ClearColumnsFilter()
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then NAMECLAUSE = " AND ({LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'" Else NAMECLAUSE = NAMECLAUSE & " OR {LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                    End If
                Next



                'FOR PARTYNAME
                GRIDINVOICE.ClearColumnsFilter()
                For i As Integer = 0 To GRIDINVOICE.RowCount - 1
                    Dim dtrow As DataRow = GRIDINVOICE.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then NAMECLAUSE = " AND ({LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'" Else NAMECLAUSE = NAMECLAUSE & " OR {LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                    End If
                Next


                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                    OBJENV.WHERECLAUSE = OBJENV.WHERECLAUSE & NAMECLAUSE
                End If

                OBJENV.Show()
                Exit Sub
            End If



            'NO NEED FOR YEARID CLAUSE FOR THSES 3 REPORT, ONLY CMPID CLAUSE 
            getFromToDate()
            If RBYOYSALE.Checked = True Or RBYOYPARTYSALE.Checked = True Or RBYOYITEMSALE.Checked = True Or RBYOYAGENTSALE.Checked = True Then
                OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " {INVOICEMASTER.INVOICE_CMPID} = " & CmpId
            Else
                OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " {INVOICEMASTER.INVOICE_YEARID} IN [" & CMPCLAUSE & "]"
            End If
            'OBJSALE.WHERECLAUSE = "{INVOICEMASTER.INVOICE_yearid}=" & YearId



            'FOR PARTYNAME
            gridbill.ClearColumnsFilter()
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If NAMECLAUSE = "" Then NAMECLAUSE = " AND ({LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'" Else NAMECLAUSE = NAMECLAUSE & " OR {LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                End If
            Next

            If NAMECLAUSE <> "" Then
                NAMECLAUSE = NAMECLAUSE & ")"
                OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & NAMECLAUSE
            End If



            'FOR MULTIPLE INVOICES
            NAMECLAUSE = ""
            GRIDINVOICE.ClearColumnsFilter()
            For i As Integer = 0 To GRIDINVOICE.RowCount - 1
                Dim dtrow As DataRow = GRIDINVOICE.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If NAMECLAUSE = "" Then NAMECLAUSE = " AND ({INVOICEMASTER.INVOICE_NO} = " & Val(dtrow("INVOICENO")) Else NAMECLAUSE = NAMECLAUSE & " OR {INVOICEMASTER.INVOICE_NO} = " & Val(dtrow("INVOICENO"))
                End If
            Next

            If NAMECLAUSE <> "" Then
                NAMECLAUSE = NAMECLAUSE & ")"
                OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & NAMECLAUSE
            End If


            If Val(TXTFROM.Text.Trim) > 0 Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " AND {INVOICEMASTER.INVOICE_NO} >= " & Val(TXTFROM.Text.Trim)



            If Val(TXTFROM.Text.Trim) > 0 Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " AND {INVOICEMASTER.INVOICE_NO} >= " & Val(TXTFROM.Text.Trim)
            If Val(TXTTO.Text.Trim) > 0 Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " AND {INVOICEMASTER.INVOICE_NO} <= " & Val(TXTTO.Text.Trim)

            If chkdate.Checked = True Then
                OBJSALE.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
            Else
                OBJSALE.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If

            If RDBPARTY.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSALE.FRMSTRING = "PARTYWISEDTLS" Else OBJSALE.FRMSTRING = "PARTYWISESUMM"

            ElseIf RDPARTYPERCENT.Checked = True Then
                OBJSALE.FRMSTRING = "PARTYPERCENT"

            ElseIf RDBAGENT.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSALE.FRMSTRING = "JOBBERWISEDTLS" Else OBJSALE.FRMSTRING = "JOBBERWISESUMM"

            ElseIf RDBTRANS.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSALE.FRMSTRING = "TRANSWISEDTLS" Else OBJSALE.FRMSTRING = "TRANSWISESUMM"

            ElseIf RDITEM.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSALE.FRMSTRING = "ITEMWISEDTLS" Else OBJSALE.FRMSTRING = "ITEMWISESUMM"

            ElseIf RDITEMPERCENT.Checked = True Then
                OBJSALE.FRMSTRING = "ITEMPERCENT"

            ElseIf RDQUALITY.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSALE.FRMSTRING = "QUALITYWISEDTLS" Else OBJSALE.FRMSTRING = "QUALITYWISESUMM"

            ElseIf RDBDESIGN.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSALE.FRMSTRING = "DESIGNWISEDTLS" Else OBJSALE.FRMSTRING = "DESIGNWISESUMM"

            ElseIf RDSHADE.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSALE.FRMSTRING = "SHADEWISEDTLS" Else OBJSALE.FRMSTRING = "SHADEWISESUMM"

            ElseIf RDBMONTHLY.Checked = True Then
                OBJSALE.FRMSTRING = "MONTHLY"
                OBJSALE.PERIOD = "MONTHLY SALE REPORT - " & OBJSALE.PERIOD

            ElseIf RDBMONTHLYRETURN.Checked = True Then
                OBJSALE.FRMSTRING = "MONTHLYWITHRET"
                OBJSALE.PERIOD = "MONTHLY SALE INVOICE - SALE RETURN SUMMARY - " & OBJSALE.PERIOD

            ElseIf RDBDOC.Checked = True Then
                OBJSALE.FRMSTRING = "DOCUMENT"

            ElseIf RDBREFF.Checked = True Then
                OBJSALE.FRMSTRING = "REFFNO"

            ElseIf RDBENTRYWISE.Checked = True Then
                OBJSALE.FRMSTRING = "ENTRYWISE"
                If CMBSUPPNAME.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {PURLEDGERS.ACC_CMPNAME}='" & CMBSUPPNAME.Text.Trim & "'"

            ElseIf RDBPARTYENTRYWISE.Checked = True Then
                OBJSALE.FRMSTRING = "PARTYENTRYWISE"
                If CMBSUPPNAME.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {PURLEDGERS.ACC_CMPNAME}='" & CMBSUPPNAME.Text.Trim & "'"

            ElseIf RDBAGENTENTRYWISE.Checked = True Then
                OBJSALE.FRMSTRING = "AGENTENTRYWISE"
                If CMBSUPPNAME.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {PURLEDGERS.ACC_CMPNAME}='" & CMBSUPPNAME.Text.Trim & "'"

            ElseIf RBREGISTERENTRY.Checked = True Then
                OBJSALE.FRMSTRING = "REGISTERENTRYWISE"
                If CMBSUPPNAME.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {PURLEDGERS.ACC_CMPNAME}='" & CMBSUPPNAME.Text.Trim & "'"

            ElseIf RDBSALEREGISTERINDETAIL.Checked = True Then
                OBJSALE.FRMSTRING = "SALEREGISTERINDETAIL"
                OBJSALE.PERIOD = "SALE REGISTER IN DETAIL - " & OBJSALE.PERIOD

            ElseIf RBREGISTERENTRYITEM.Checked = True Then
                OBJSALE.FRMSTRING = "REGISTERENTRYITEMWISE"
                If CMBSUPPNAME.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {PURLEDGERS.ACC_CMPNAME}='" & CMBSUPPNAME.Text.Trim & "'"

            ElseIf RDBTERM.Checked = True Then
                OBJSALE.FRMSTRING = "TERMWISE"
                If CMBTERM.Text.Trim <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {TERMMASTER.TERM_NAME}='" & CMBTERM.Text.Trim & "'"
                If CMBSUPPNAME.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {PURLEDGERS.ACC_CMPNAME}='" & CMBSUPPNAME.Text.Trim & "'"

            ElseIf RDBCOVERNOTE.Checked = True Then
                OBJSALE.FRMSTRING = "COVERNOTE"
                If ClientName = "MAHAVIRPOLYCOT" Or ClientName = "MOHATUL" Then OBJSALE.COVERNOTENO = InputBox("Enter Cover Note No", "TEXTRADE", 1)

            ElseIf RDBAGENTCOVERNOTE.Checked = True Then
                OBJSALE.FRMSTRING = "AGENTCOVERNOTE"
                If ClientName = "MAHAVIRPOLYCOT" Or ClientName = "MOHATUL" Then OBJSALE.COVERNOTENO = InputBox("Enter Agent Cover Note No", "TEXTRADE", 1)

            ElseIf RDBLOCALTRANS.Checked = True Then
                OBJSALE.FRMSTRING = "LOCALTRANS"
                OBJSALE.BALERATE = Val(TXTBALERATE.Text.Trim)
                OBJSALE.ROLLRATE = Val(TXTROLLRATE.Text.Trim)
                If CMBLOCALTRANS.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {TRANSLEDGERS.ACC_CMPNAME}='" & CMBLOCALTRANS.Text.Trim & "'"

            ElseIf RDBTRANSWTCALC.Checked = True Then
                OBJSALE.FRMSTRING = "TRANSWTCALC"
                OBJSALE.BALERATE = Val(TXTBALERATE.Text.Trim)
                OBJSALE.ROLLRATE = Val(TXTROLLRATE.Text.Trim)

            ElseIf RBYOYSALE.Checked = True Then
                OBJSALE.FRMSTRING = "YOYSALE"
                OBJSALE.PERIOD = "YEAR WISE COMPARISON SALE REPORT (GRANDTOTAL) - " & OBJSALE.PERIOD

            ElseIf RBYOYPARTYSALE.Checked = True Then
                OBJSALE.FRMSTRING = "YOYPARTYSALE"
                OBJSALE.PERIOD = "YEAR WISE COMPARISON SALE REPORT (PARTY) - " & OBJSALE.PERIOD

            ElseIf RBYOYITEMSALE.Checked = True Then
                OBJSALE.FRMSTRING = "YOYITEMSALE"
                OBJSALE.PERIOD = "YEAR WISE COMPARISON SALE REPORT (ITEM) - " & OBJSALE.PERIOD

            ElseIf RBYOYAGENTSALE.Checked = True Then
                OBJSALE.FRMSTRING = "YOYAGENTSALE"
                OBJSALE.PERIOD = "YEAR WISE COMPARISON SALE REPORT (AGENT) - " & OBJSALE.PERIOD
            End If

            If TXTAMT.Text <> "" And CMBSIGN.Text <> "" Then
                If CMBSIGN.Text = "=" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {INVOICEMASTER.INVOICE_GRANDTOTAL}=" & TXTAMT.Text.Trim & ""
                If CMBSIGN.Text = ">" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {INVOICEMASTER.INVOICE_GRANDTOTAL}>" & TXTAMT.Text.Trim & ""
                If CMBSIGN.Text = "<" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {INVOICEMASTER.INVOICE_GRANDTOTAL}<" & TXTAMT.Text.Trim & ""
            End If

            'ALL FILTERS
            If CMBNAME.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {LEDGERS.ACC_CMPNAME}='" & CMBNAME.Text.Trim & "'"
            If CMBSHIPTONAME.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {SHIPTOLEDGERS.ACC_CMPNAME}='" & CMBSHIPTONAME.Text.Trim & "'"
            If CMBAGENT.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {BROKERLEDGERS.ACC_CMPNAME}='" & CMBAGENT.Text.Trim & "'"
            If CMBITEMNAME.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {ITEMMASTER.ITEM_NAME}='" & CMBITEMNAME.Text.Trim & "'"
            If CMBQUALITY.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {QUALITYMASTER.QUALITY_NAME}='" & CMBQUALITY.Text.Trim & "'"
            If CMBSHADE.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {COLORMASTER.COLOR_NAME}='" & CMBSHADE.Text.Trim & "'"
            If cmbtrans.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {TRANSLEDGERS.ACC_CMPNAME}='" & cmbtrans.Text.Trim & "'"
            If cmbregister.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {REGISTERMASTER.REGISTER_NAME}='" & cmbregister.Text.Trim & "'"
            If CMBCATEGORY.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {CATEGORYMASTER.CATEGORY_NAME}='" & CMBCATEGORY.Text.Trim & "'"

            If RDBCOMMISSION.Checked = True Then
                OBJSALE.WHERECLAUSE = " {AGENTCOMMVIEW.YEARID} IN [" & CMPCLAUSE & "]"
                'OBJSALE.WHERECLAUSE = " {AGENTCOMMVIEW.YEARID}= " & YearId

                If CMBAGENT.Text.Trim <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " AND {AGENTCOMMVIEW.AGENT}='" & CMBAGENT.Text.Trim & "'"
                Dim ADDJOURNAL As String = ""
                If CHKJOURNAL.Checked = True Then ADDJOURNAL = ",'JOURNAL'"

                OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " And {AGENTCOMMVIEW.TYPE} IN ['SALE','SALE RETURN','CREDITNOTE','DEBITNOTE'" & ADDJOURNAL & "]"
                If MsgBox("Show Commission on Recd Amount?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJSALE.COMMISSIONONRECDAMT = True

                If chkdate.Checked = True Then
                    getFromToDate()
                    OBJSALE.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                    OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
                Else
                    OBJSALE.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
                End If
                If CMBNAME.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {AGENTCOMMVIEW.NAME}='" & CMBNAME.Text.Trim & "'"
                If CHKSUMMARY.Checked = True Then OBJSALE.FRMSTRING = "COMMSUMM" Else OBJSALE.FRMSTRING = "COMMISSION"
                OBJSALE.COMMPER = Val(TXTAGENTCOMM.Text.Trim)

                'FOR PARTYNAME
                NAMECLAUSE = ""
                gridbill.ClearColumnsFilter()
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then NAMECLAUSE = " AND ({AGENTCOMMVIEW.NAME} = '" & dtrow("NAME") & "'" Else NAMECLAUSE = NAMECLAUSE & " OR {AGENTCOMMVIEW.NAME} = '" & dtrow("NAME") & "'"
                    End If
                Next

                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                    OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & NAMECLAUSE
                End If


                'FOR AGENTNAME
                Dim AGENTCLAUSE As String = ""
                GRIDBILLAGENT.ClearColumnsFilter()
                For i As Integer = 0 To GRIDBILLAGENT.RowCount - 1
                    Dim dtrow As DataRow = GRIDBILLAGENT.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If AGENTCLAUSE = "" Then AGENTCLAUSE = " AND ({AGENTCOMMVIEW.AGENT} = '" & dtrow("NAME") & "'" Else AGENTCLAUSE = AGENTCLAUSE & " OR {AGENTCOMMVIEW.AGENT} = '" & dtrow("NAME") & "'"
                    End If
                Next

                If AGENTCLAUSE <> "" Then
                    AGENTCLAUSE = AGENTCLAUSE & ")"
                    OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & AGENTCLAUSE
                End If
            End If


            If RDBCOMMOPENING.Checked = True Then
                If CMBAGENT.Text.Trim = "" Then
                    OBJSALE.WHERECLAUSE = " {AGENTCOMMVIEWOPENING.YEARID} IN [" & CMPCLAUSE & "] And {AGENTCOMMVIEWOPENING.TYPE} IN ['SALE','SALE RETURN','CREDITNOTE','DEBITNOTE']"
                    'OBJSALE.WHERECLAUSE = " {AGENTCOMMVIEWOPENING.YEARID}= " & YearId & " And {AGENTCOMMVIEWOPENING.TYPE} IN ['SALE','SALE RETURN','CREDITNOTE','DEBITNOTE']"

                    If MsgBox("Show Commission on Recd Amount?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJSALE.COMMISSIONONRECDAMT = True
                Else
                    OBJSALE.WHERECLAUSE = " {AGENTCOMMVIEWOPENING.YEARID} IN [" & CMPCLAUSE & "] AND {AGENTCOMMVIEWOPENING.AGENT}='" & CMBAGENT.Text.Trim & "'  And {AGENTCOMMVIEWOPENING.TYPE} IN ['SALE','SALE RETURN','CREDIT','DEBIT']"
                    'OBJSALE.WHERECLAUSE = " {AGENTCOMMVIEWOPENING.YEARID}= " & YearId & " AND {AGENTCOMMVIEWOPENING.AGENT}='" & CMBAGENT.Text.Trim & "'  And {AGENTCOMMVIEWOPENING.TYPE} IN ['SALE','SALE RETURN','CREDIT','DEBIT']"
                    If MsgBox("Show Commission on Recd Amount?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJSALE.COMMISSIONONRECDAMT = True
                End If

                If chkdate.Checked = True Then
                    getFromToDate()
                    OBJSALE.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                    OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
                Else
                    OBJSALE.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
                End If
                If CMBNAME.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {AGENTCOMMVIEWOPENING.NAME}='" & CMBNAME.Text.Trim & "'"
                If CHKSUMMARY.Checked = True Then OBJSALE.FRMSTRING = "COMMOPSUMM" Else OBJSALE.FRMSTRING = "COMMISSIONOP"
                OBJSALE.COMMPER = Val(TXTAGENTCOMM.Text.Trim)

                'FOR PARTYNAME
                gridbill.ClearColumnsFilter()
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then NAMECLAUSE = " AND ({AGENTCOMMVIEWOPENING.NAME} = '" & dtrow("NAME") & "'" Else NAMECLAUSE = NAMECLAUSE & " OR {AGENTCOMMVIEWOPENING.NAME} = '" & dtrow("NAME") & "'"
                    End If
                Next

                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                    OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & NAMECLAUSE
                End If


                'FOR AGENTNAME
                Dim AGENTCLAUSE As String = ""
                GRIDBILLAGENT.ClearColumnsFilter()
                For i As Integer = 0 To GRIDBILLAGENT.RowCount - 1
                    Dim dtrow As DataRow = GRIDBILLAGENT.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If AGENTCLAUSE = "" Then AGENTCLAUSE = " AND ({AGENTCOMMVIEWOPENING.AGENT} = '" & dtrow("NAME") & "'" Else AGENTCLAUSE = AGENTCLAUSE & " OR {AGENTCOMMVIEWOPENING.AGENT} = '" & dtrow("NAME") & "'"
                    End If
                Next

                If AGENTCLAUSE <> "" Then
                    AGENTCLAUSE = AGENTCLAUSE & ")"
                    OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & AGENTCLAUSE
                End If
            End If

            If CHKGROUPONNEWPG.Checked = True Then OBJSALE.NEWPAGE = CHKGROUPONNEWPG.Checked
            OBJSALE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDESIGN.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJD As New SelectDesign
                OBJD.ShowDialog()
                If OBJD.TEMPNAME <> "" Then CMBDESIGN.Text = OBJD.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBSHADE.Enter
        Try
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSHADE.Validating
        Try
            If CMBSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBSHADE, e, Me, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBSHADE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCOLOR As New SelectShade
                OBJCOLOR.ShowDialog()
                If OBJCOLOR.TEMPNAME <> "" Then CMBSHADE.Text = OBJCOLOR.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBER_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then fillledger(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBAGENT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBAGENT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBITEMNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "MERCHANT"
                OBJItem.STRSEARCH = " and ITEM_cmpid = " & CmpId & " and ITEM_LOCATIONid = " & Locationid & " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then CMBITEMNAME.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then itemvalidate(CMBITEMNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, edit, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTALL.CheckedChanged
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

    Private Sub CHKSELECTALLAG_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTALLAG.CheckedChanged
        Try
            If GRIDBILLDETAILSAGENT.Visible = True Then
                For i As Integer = 0 To GRIDBILLAGENT.RowCount - 1
                    Dim dtrow As DataRow = GRIDBILLAGENT.GetDataRow(i)
                    dtrow("CHK") = CHKSELECTALLAG.Checked
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKALL_CheckedChanged(sender As Object, e As EventArgs) Handles CHKALL.CheckedChanged
        Try
            For i As Integer = 0 To GRIDINVOICE.RowCount - 1
                Dim dtrow As DataRow = GRIDINVOICE.GetDataRow(i)
                dtrow("CHK") = CHKALL.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTAMT.KeyPress
        Try
            numkeypress(e, TXTAMT, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleInvoiceFilter_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "SAKARIA" Or ClientName = "NVAHAN" Then
                LBLSUPPNAME.Visible = True
                CMBSUPPNAME.Visible = True
            End If

            If ClientName = "KOCHAR" Then
                LBLBALERATE.Visible = True
                LBLROLLRATE.Visible = True
                TXTBALERATE.Visible = True
                TXTROLLRATE.Visible = True
                RDBLOCALTRANS.Visible = True
                LBLLOCALTRANS.Visible = True
                CMBLOCALTRANS.Visible = True
                RDBTRANSWTCALC.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCATEGORY_Enter(sender As Object, e As EventArgs) Handles CMBCATEGORY.Enter
        Try
            If CMBCATEGORY.Text.Trim = "" Then fillCATEGORY(CMBCATEGORY, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCATEGORY_Validating(sender As Object, e As CancelEventArgs) Handles CMBCATEGORY.Validating
        Try
            If CMBCATEGORY.Text.Trim <> "" Then CATEGORYVALIDATE(CMBCATEGORY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTFROM.KeyPress, TXTTO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTBALERATE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTBALERATE.KeyPress, TXTROLLRATE.KeyPress, TXTAGENTCOMM.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

End Class