Imports System.ComponentModel
Imports BL

Public Class PartyWiseBaleRateReport
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

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
            'FILLNAME(cmbname, False, " and ledgers.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.ACC_YEARID = " & YearId)
            'If cmbname.Text.Trim = "" Then cmbname.Text = TEMPNAME
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

        Try
            Dim dt As New DataTable()
            Dim ALPARAVAL As New ArrayList
            Dim objclsCMST As New ClsCommonMaster
            If chkdate.Checked = True Then
                ALPARAVAL.Add(dtfrom.Value.Date)
                ALPARAVAL.Add(dtto.Value.Date)
            Else
                ALPARAVAL.Add(AccFrom)
                'COZ DATE WONT BE IN ACCOUNTING YEAR
                'If CmpName = "TRANSFER DATA" Then
                '    ALPARAVAL.Add(Now.Date)
                'Else
                ALPARAVAL.Add(AccTo)
                'End If
            End If
            If chkdate.CheckState = CheckState.Unchecked Then
                'Dim dt As DataTable = objclsCMST.search(" ISNULL(LEDGERS.Acc_cmpname, '') AS CMPNAME, ISNULL(LEDGERS.Acc_name, '') AS NAME, ISNULL(LEDGERS.Acc_code, '') AS CODE, ISNULL(GROUPMASTER.group_name, '') AS GROUPNAME,  ISNULL(GROUPMASTER.group_secondary, '') AS SECONDARY, ISNULL(LEDGERS.Acc_TYPE, '') AS TYPE, ISNULL(LEDGERS.Acc_opbal, 0) AS OPBAL, ISNULL(LEDGERS.Acc_drcr, '') AS DRCR, ISNULL(AREAMASTER.area_name, '') AS AREA, ISNULL(DISTRICTMASTER.DISTRICT_name, '') AS DISTRICT, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(STATEMASTER.state_name, '') AS STATE, ISNULL(COUNTRYMASTER.country_name, '')  AS COUNTRY, ISNULL(LEDGERS.Acc_resino, '') AS RESINO, ISNULL(LEDGERS.Acc_altno, '') AS ALTNO, ISNULL(LEDGERS.Acc_phone, '') AS PHONENO, ISNULL(LEDGERS.Acc_mobile, '') AS MOBILE, ISNULL(LEDGERS.Acc_fax,'') AS FAX, ISNULL(LEDGERS.Acc_website, '') AS WEBSITE, ISNULL(LEDGERS.Acc_email, '') AS EMAIL, ISNULL(LEDGERS.Acc_panno, '') AS PANNO, ISNULL(LEDGERS.Acc_add, '') AS [ADD], ISNULL(LEDGERS.Acc_tinno, '')  AS TANNO, ISNULL(LEDGERS.Acc_cstno, '') AS CSTNO, ISNULL(LEDGERS.Acc_vatno, '') AS VATNO, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME,ISNULL(TERMMASTER.TERM_NAME, '') AS TERM, ISNULL(PARTYBANKMASTER.PARTYBANK_name, '') AS PARTYBANK, ISNULL(LEDGERS.ACC_ACCOUNTTYPE, '') AS ACCOUNTTYPE, ISNULL(LEDGERS.ACC_ACCOUNTNO, '')  AS ACCOUNTNO, ISNULL(LEDGERS.ACC_IFSC, '') AS IFSCCODE, ISNULL(LEDGERS.ACC_BRANCH, '') AS BRANCH, ISNULL(BANKCITY.city_name, '') AS BANKCITY, ISNULL(LEDGERS.Acc_remarks, '') AS REMARKS,  ISNULL(LEDGERS.ACC_INTPER, 0) AS INTPER, ISNULL(LEDGERS.Acc_crdays, 0) AS CRDAYS, ISNULL(LEDGERS.Acc_crlimit, 0) AS CRLIMIT, ISNULL(ACCOUNTSMASTER_TDS.ACC_TDSPER, 0) AS TDSPER,  ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(LEDGERS.Acc_name, '') AS CONTACT, ISNULL(DELIVERYCITY.city_name, '') AS DELIVERYAT, ISNULL(LEDGERS.Acc_zipcode, '') AS ZIPCODE,  ISNULL(LEDGERS.ACC_DELIVERYPINCODE, '') AS DELIVERYPINCODE, ISNULL(CAST(LEDGERS.Acc_shippingadd AS VARCHAR(500)), '') AS SHIPPINGADD, ISNULL(LEDGERS.ACC_KMS, 0) AS KMS,   ISNULL(LEDGERS.ACC_CDPER, 0) AS CDPER, ISNULL(LEDGERS.ACC_DISC, 0) AS DISC, ISNULL(LEDGERS.ACC_AGENTCOMM, 0) AS BROKERAGE, ISNULL(LEDGERS.ACC_WHATSAPPNO, '') AS WHATSAPPNO,   ISNULL(LEDGERS.ACC_RD, 0) AS RATEDIFF, ISNULL(GROUPOFCOMPANIESMASTER.GOC_NAME, '') AS GROUPOFCOMPANIES, ISNULL(LEDGERS.Acc_MSMENO, '') AS MSMENO, ISNULL(LEDGERS.Acc_RANGE, '') AS TALLYLEDGER, ISNULL(LEDGERS.ACC_BLOCKED, 0) AS BLOCKED, USER_NAME AS USERNAME,  ISNULL(LEDGERS.ACC_EXMILLLESS, 0) AS EXMILLDISC ", "", "   LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN  CITYMASTER AS BANKCITY ON LEDGERS.ACC_BANKCITYID = BANKCITY.city_id LEFT OUTER JOIN GROUPOFCOMPANIESMASTER ON LEDGERS.ACC_GOCID = GROUPOFCOMPANIESMASTER.GOC_ID LEFT OUTER JOIN  PARTYBANKMASTER ON LEDGERS.ACC_BANKID = PARTYBANKMASTER.PARTYBANK_id LEFT OUTER JOIN  CITYMASTER AS CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN  CITYMASTER AS DELIVERYCITY ON LEDGERS.ACC_DELIVERYATID = DELIVERYCITY.city_id LEFT OUTER JOIN AREAMASTER ON LEDGERS.Acc_areaid = AREAMASTER.area_id LEFT OUTER JOIN  DISTRICTMASTER ON LEDGERS.ACC_DISTRICTID = DISTRICTMASTER.DISTRICT_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN COUNTRYMASTER ON LEDGERS.Acc_countryid = COUNTRYMASTER.country_id LEFT OUTER JOIN  LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN TERMMASTER ON LEDGERS.ACC_TERMID = TERMMASTER.TERM_ID LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON LEDGERS.ACC_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN ACCOUNTSMASTER_TDS ON LEDGERS.Acc_id = ACCOUNTSMASTER_TDS.ACC_ID LEFT OUTER JOIN USERMASTER ON LEDGERS.ACC_USERID = USER_ID", WHERECLAUSE & " AND LEDGERS.ACC_YEARID = " & YearId & " ORDER BY LEDGERS.ACC_CMPNAME")
                dt = objclsCMST.search(" LEDGERS.ACC_CMPNAME AS NAME, TRANSLEDGER.ACC_CMPNAME AS TRANSNAME, PAR_RATE AS BALERATE, SUM(BILL_NOOFBALES) AS TOTALBALES, PAR_RATE*SUM(BILL_NOOFBALES) AS AMT ", "", "    PURCHASEMASTER INNER JOIN PARTYWISEBALERATE ON BILL_LEDGERID = PARTYWISEBALERATE.PAR_LEDGERID AND BILL_TRANSNAMEID = PAR_TRANSID INNER JOIN LEDGERS ON BILL_LEDGERID = LEDGERS.ACC_ID INNER JOIN LEDGERS AS TRANSLEDGER ON BILL_TRANSNAMEID = TRANSLEDGER.ACC_ID", " and BILL_PARTYBILLDATE >'" & Format(AccFrom, "MM/dd/yyyy") & "'   GROUP BY LEDGERS.ACC_CMPNAME , TRANSLEDGER.ACC_CMPNAME , PAR_RATE")
            ElseIf chkdate.CheckState = CheckState.Checked Then
                dt = objclsCMST.search(" LEDGERS.ACC_CMPNAME AS NAME, TRANSLEDGER.ACC_CMPNAME AS TRANSNAME, PAR_RATE AS BALERATE, SUM(BILL_NOOFBALES) AS TOTALBALES, PAR_RATE*SUM(BILL_NOOFBALES) AS AMT ", "", "    PURCHASEMASTER INNER JOIN PARTYWISEBALERATE ON BILL_LEDGERID = PARTYWISEBALERATE.PAR_LEDGERID AND BILL_TRANSNAMEID = PAR_TRANSID INNER JOIN LEDGERS ON BILL_LEDGERID = LEDGERS.ACC_ID INNER JOIN LEDGERS AS TRANSLEDGER ON BILL_TRANSNAMEID = TRANSLEDGER.ACC_ID", " and BILL_PARTYBILLDATE >'" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "' AND  BILL_PARTYBILLDATE < '" & Format(dtto.Value.Date, "MM/dd/yyyy") & "' GROUP BY LEDGERS.ACC_CMPNAME , TRANSLEDGER.ACC_CMPNAME , PAR_RATE")
            End If
            griddetails.DataSource = dt
            If dt.Rows.Count > 0 Then gridregister.FocusedRowHandle = gridregister.RowCount - 1
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

    Private Sub gridregister_DoubleClick(sender As Object, e As EventArgs) Handles gridregister.DoubleClick
        Try
            showform(True, gridregister.GetFocusedRowCellValue("PARNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            showform(True, gridregister.GetFocusedRowCellValue("PARNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub showform(ByVal EDITVAL As Boolean, ByVal PARNO As Integer)
        Try
            If PARNO = 0 Then
                Exit Sub
            End If

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJENQNO As New PartyWiseBaleRate
            OBJENQNO.EDIT = EDITVAL
            OBJENQNO.MdiParent = MDIMain
            OBJENQNO.TEMPPARNO = PARNO
            OBJENQNO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\ Party Wise Bale Report.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim workbook As String = PATH
            If FileIO.FileSystem.FileExists(PATH) = True Then Interaction.GetObject(workbook).close(False)
            GC.Collect()

            Dim PERIOD As String = ""
            If chkdate.Checked = False Then
                PERIOD = AccFrom & " - " & AccTo
            Else
                PERIOD = dtfrom.Value.Date & " - " & dtto.Value.Date
            End If

            opti.SheetName = " Party Wise Bale Report"
            gridregister.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, " Party Wise Bale Report", gridregister.VisibleColumns.Count + gridregister.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class