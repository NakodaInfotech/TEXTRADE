
Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports BL
Imports DevExpress.XtraTreeMap.Native
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class AgencyOutstandingGridReport

    Dim FILLDONE As Boolean = True
    Public PARTYNAME As String = ""

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        FILLCMB()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub TXTDAYS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTDAYS.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTPERCENT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPERCENT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Sub FILLCMB()
        Try
            FILLNAME(CMBBUYERNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
            FILLNAME(CMBSELLERNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")

            Dim OBJCMN As New ClsCommon
            Dim DTBUYER As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.ACC_AREAID = AREAMASTER.AREA_ID ", " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            GRIDBUYERDETAILS.DataSource = DTBUYER
            If DTBUYER.Rows.Count > 0 Then GRIDBUYER.FocusedRowHandle = GRIDBUYER.RowCount - 1

            Dim DTSELLER As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.ACC_AREAID = AREAMASTER.AREA_ID ", " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            GRIDSELLERDETAILS.DataSource = DTSELLER
            If DTSELLER.Rows.Count > 0 Then GRIDSELLER.FocusedRowHandle = GRIDSELLER.RowCount - 1


            Dim dt As DataTable = OBJCMN.SEARCH("group_name", "", "GroupMaster", " and group_Yearid = " & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "Group_name"
                CMBGROUP.DisplayMember = "group_name"
            End If
            CMBGROUP.DataSource = dt
            CMBGROUP.SelectedIndex = -1

            fillCITY(CMBCITY, False)
            FILLGROUPCOMPANY(CMBGROUPOFCOMPANY)
            fillSTATE(CMBSTATE)
            fillitemname(CMBITEMNAME, " AND ITEM_FRMSTRING = 'MERCHANT'")

            dt = OBJCMN.SEARCH("CMP_NAME", "", "CMPMASTER", "")
            For Each DTROW As DataRow In dt.Rows
                LSTCMP.Items.Add(DTROW(0).ToString)
                If DTROW(0) = CmpName Then LSTCMP.SetItemChecked(LSTCMP.Items.Count - 1, True)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AgencyOutstandingGridReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#Region "BUYERWISE"

    Sub FILLGRID()

        '******************* ORIGINAL CODE ********************
        'Try
        '    GRIDOUTSTANDING.RowCount = 0
        '    GCMPNAME.Visible = False

        '    Dim TEMPNAME As String = ""
        '    Dim GTOTAL, RECDTOTAL, BALANCE, GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL, PARTYINTTOTAL As Decimal
        '    Dim WHERECLAUSE As String = " "


        '    If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
        '    If CMBSELLERNAME.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
        '    If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
        '    If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
        '    If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
        '    If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
        '    If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
        '    If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"

        '    If chkdate.CheckState = CheckState.Checked Then
        '        WHERECLAUSE = WHERECLAUSE & " AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
        '    End If
        '    Mydate = dtto.Value.Date

        '    If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
        '    If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
        '    If TXTOVERDUEDAYS.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  = '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"

        '    'GET ALL YEARID FROM SELECTED COMPANY WITH SAME STARTYEAR
        '    Dim OBJCMN As New ClsCommon
        '    Dim DT As New DataTable
        '    Dim CMPCLAUSE As String = ""
        '    Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
        '    For Each item As Object In CHECKED_CMP
        '        If CMPCLAUSE = "" Then
        '            CMPCLAUSE = "'" & item.ToString() & "'"
        '        Else
        '            CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
        '            GCMPNAME.Visible = True
        '        End If
        '    Next item



        '    DT = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
        '    CMPCLAUSE = ""
        '    For Each DTROW As DataRow In DT.Rows
        '        If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
        '    Next
        '    WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


        '    Dim DAYS As Integer = 0
        '    Dim TOTALDAYS As Integer = 0
        '    Dim RUNNINGBAL As Double = 0.0
        '    Dim BILLINTEREST As Double = 0.0
        '    Dim SRNO As Integer = 0

        '    'WE ARE PASSING YEARID FROM ABOVE CLAUSE SO NO NEED TO ENTER YEARID HERE
        '    DT = OBJCMN.Execute_Any_String(" SELECT AGENCYOUTSTANDINGREC.*, CMPMASTER.CMP_NAME AS CMPNAME FROM AGENCYOUTSTANDINGREC INNER JOIN CMPMASTER ON CMPID = CMP_ID WHERE SECONDARY = 'Sundry Debtors' AND ROUND(BALANCE,2) <> 0 " & WHERECLAUSE & " ORDER BY NAME, DATE, TYPE, BILL", "", "")
        '    If DT.Rows.Count > 0 Then
        '        TEMPNAME = ""
        '        GTOTAL = 0
        '        RECDTOTAL = 0
        '        BALANCE = 0
        '        GRANDTOTAL = 0
        '        RECDGRANDTOTAL = 0
        '        BALANCEGRANDTOTAL = 0
        '        DAYS = 0
        '        TOTALDAYS = 0
        '        RUNNINGBAL = 0.0
        '        SRNO = 0
        '        BILLINTEREST = 0
        '        PARTYINTTOTAL = 0
        '        GINTTOTAL = 0

        '        For Each ROW As DataRow In DT.Rows
        '            If TEMPNAME <> ROW("NAME") Then
        '                TEMPNAME = ROW("NAME")
        '                If GRIDOUTSTANDING.RowCount > 0 Then ADDPARTYTOTALROW(GTOTAL, RECDTOTAL, BALANCE, PARTYINTTOTAL)
        '                GTOTAL = 0
        '                RECDTOTAL = 0
        '                BALANCE = 0
        '                RUNNINGBAL = 0.0
        '                SRNO = 0
        '                PARTYINTTOTAL = 0
        '                ADDNAMEROW(ROW("NAME"), ROW("MOBILENO"), ROW("PHONENO"), ROW("CITY"))
        '            End If

        '            DAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date)
        '            TOTALDAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DATE")).Date, Mydate.Date)
        '            If Val(TXTPERCENT.Text.Trim) > 0 And Val(TXTDAYS.Text.Trim) > 0 Then BILLINTEREST = Format((Val(TXTPERCENT.Text.Trim) / Val(TXTDAYS.Text.Trim) / 100) * Val(DAYS) * Val(ROW("BALANCE")), "0")

        '            SRNO += 1
        '            RUNNINGBAL += Val(ROW("BALANCE"))
        '            GRIDOUTSTANDING.Rows.Add(ROW("SELLERNAME"), ROW("PRINTINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), ROW("ITEMNAME"), Val(ROW("TOTALPCS")), Format(Val(ROW("TOTALMTRS")), "0.00"), Format(Val(ROW("RATE")), "0.00"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), Format(Val(RUNNINGBAL), "0.00"), Val(SRNO), Val(ROW("CRDAYS")), Val(DAYS), Val(TOTALDAYS), Format(Val(ROW("CHARGES")), "0.00"), ROW("CMPNAME"), ROW("TYPE"), Val(ROW("BILL")), ROW("REGTYPE"), Val(BILLINTEREST), ROW("HOLDINTCALC"), ROW("COMPLAINT"), ROW("COMPLAINTBY"), ROW("COMPLAINTDATE"))
        '            GTOTAL += Val(ROW("GRANDTOTAL"))
        '            RECDTOTAL += Val(ROW("RECDAMT"))
        '            BALANCE += Val(ROW("BALANCE"))
        '            PARTYINTTOTAL += Val(BILLINTEREST)

        '            GRANDTOTAL += Val(ROW("GRANDTOTAL"))
        '            RECDGRANDTOTAL += Val(ROW("RECDAMT"))
        '            BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
        '            GINTTOTAL += Val(BILLINTEREST)
        '        Next
        '        'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
        '        If GRIDOUTSTANDING.RowCount > 0 Then ADDPARTYTOTALROW(GTOTAL, RECDTOTAL, BALANCE, PARTYINTTOTAL)
        '        If GRIDOUTSTANDING.RowCount > 0 Then ADDGRANDTOTALROW(GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
        '******************* END OF ORIGINAL CODE ********************

        Try
            GRIDOUTSTANDING.RowCount = 0
            GCMPNAME.Visible = False

            Dim TEMPNAME As String = ""
            Dim TEMPSELLERNAME As String = ""
            Dim GTOTAL, RECDTOTAL, BALANCE, GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL, PARTYINTTOTAL, SGTOTAL, SRECDTOTAL, SBALANCE, SGRANDTOTAL, SRECDGRANDTOTAL, SBALANCEGRANDTOTAL, SGINTTOTAL, SPARTYINTTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"

            If chkdate.CheckState = CheckState.Checked Then
                WHERECLAUSE = WHERECLAUSE & " AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
            End If
            Mydate = dtto.Value.Date

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If TXTOVERDUEDAYS.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  = '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"

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
                    GCMPNAME.Visible = True
                End If
            Next item



            DT = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            Dim DAYS As Integer = 0
            Dim TOTALDAYS As Integer = 0
            Dim RUNNINGBAL As Double = 0.0
            Dim BILLINTEREST As Double = 0.0
            Dim SRNO As Integer = 0

            'WE ARE PASSING YEARID FROM ABOVE CLAUSE SO NO NEED TO ENTER YEARID HERE
            DT = OBJCMN.Execute_Any_String(" SELECT AGENCYOUTSTANDINGREC.*, CMPMASTER.CMP_NAME AS CMPNAME FROM AGENCYOUTSTANDINGREC INNER JOIN CMPMASTER ON CMPID = CMP_ID WHERE SECONDARY = 'Sundry Debtors' AND ROUND(BALANCE,2) <> 0 " & WHERECLAUSE & " ORDER BY NAME, SELLERNAME, DATE, TYPE, BILL", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                TEMPSELLERNAME = ""
                GTOTAL = 0
                RECDTOTAL = 0
                BALANCE = 0
                SGTOTAL = 0
                SRECDTOTAL = 0
                SBALANCE = 0
                GRANDTOTAL = 0
                RECDGRANDTOTAL = 0
                BALANCEGRANDTOTAL = 0
                DAYS = 0
                TOTALDAYS = 0
                RUNNINGBAL = 0.0
                SRNO = 0
                BILLINTEREST = 0
                PARTYINTTOTAL = 0
                GINTTOTAL = 0

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("NAME") Then
                        TEMPNAME = ROW("NAME")
                        If GRIDOUTSTANDING.RowCount > 0 Then ADDPARTYTOTALROW(GTOTAL, RECDTOTAL, BALANCE, PARTYINTTOTAL)
                        GTOTAL = 0
                        RECDTOTAL = 0
                        BALANCE = 0
                        RUNNINGBAL = 0.0
                        SRNO = 0
                        PARTYINTTOTAL = 0
                        ADDNAMEROW(ROW("NAME"), ROW("MOBILENO"), ROW("PHONENO"), ROW("CITY"))
                    End If

                    If TEMPSELLERNAME <> ROW("SELLERNAME") Then
                        TEMPSELLERNAME = ROW("SELLERNAME")
                        If GRIDOUTSTANDING.RowCount > 1 Then ADDPARTYTOTALROW(SGTOTAL, SRECDTOTAL, SBALANCE, SPARTYINTTOTAL)
                        SGTOTAL = 0
                        SRECDTOTAL = 0
                        SBALANCE = 0
                        SPARTYINTTOTAL = 0
                        RUNNINGBAL = 0.0
                        SRNO = 0
                    End If


                    DAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date)
                    TOTALDAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DATE")).Date, Mydate.Date)
                    If Val(TXTPERCENT.Text.Trim) > 0 And Val(TXTDAYS.Text.Trim) > 0 Then BILLINTEREST = Format((Val(TXTPERCENT.Text.Trim) / Val(TXTDAYS.Text.Trim) / 100) * Val(DAYS) * Val(ROW("BALANCE")), "0")

                    SRNO += 1
                    RUNNINGBAL += Val(ROW("BALANCE"))
                    GRIDOUTSTANDING.Rows.Add(ROW("SELLERNAME"), ROW("PRINTINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), ROW("ITEMNAME"), Val(ROW("TOTALPCS")), Format(Val(ROW("TOTALMTRS")), "0.00"), Format(Val(ROW("RATE")), "0.00"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), Format(Val(RUNNINGBAL), "0.00"), Val(SRNO), Val(ROW("CRDAYS")), Val(DAYS), Val(TOTALDAYS), Format(Val(ROW("CHARGES")), "0.00"), ROW("CMPNAME"), ROW("TYPE"), Val(ROW("BILL")), ROW("REGTYPE"), Val(BILLINTEREST), ROW("HOLDINTCALC"), ROW("COMPLAINT"), ROW("COMPLAINTBY"), ROW("COMPLAINTDATE"))
                    GTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    BALANCE += Val(ROW("BALANCE"))
                    PARTYINTTOTAL += Val(BILLINTEREST)

                    SGTOTAL += Val(ROW("GRANDTOTAL"))
                    SRECDTOTAL += Val(ROW("RECDAMT"))
                    SBALANCE += Val(ROW("BALANCE"))
                    SPARTYINTTOTAL += Val(BILLINTEREST)

                    GRANDTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                    GINTTOTAL += Val(BILLINTEREST)
                Next

                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDOUTSTANDING.RowCount > 0 Then ADDPARTYTOTALROW(GTOTAL, RECDTOTAL, BALANCE, PARTYINTTOTAL)
                If GRIDOUTSTANDING.RowCount > 0 Then ADDGRANDTOTALROW(GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSUMMGRID()
        Try
            GRIDSUMM.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim BALANCE, BALANCEGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            DT = OBJCMN.Execute_Any_String(" SELECT NAME,SUM(BALANCE) AS BALANCE FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' " & WHERECLAUSE & " GROUP BY NAME HAVING ROUND(SUM(BALANCE),2) <> 0 order by BALANCE", "", "")
            If DT.Rows.Count > 0 Then
                BALANCE = 0
                BALANCEGRANDTOTAL = 0
                For Each ROW As DataRow In DT.Rows
                    GRIDSUMM.Rows.Add(ROW("NAME"), Format(Val(ROW("BALANCE")), "0.00"))
                    BALANCE += Val(ROW("BALANCE"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDSUMM.RowCount > 0 Then ADDSUMMTOTALROW(BALANCE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLADVGRID()
        Try
            GRIDADV.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim RECDTOTAL, RECDGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            'If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            'If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            'If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            DT = OBJCMN.Execute_Any_String(" SELECT BILLINITIALS,DATE,NAME, SELLERNAME,RECDAMT, MOBILENO, PHONENO FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' AND TYPE='RECEIPT' " & WHERECLAUSE & " ORDER BY SELLERNAME, DATE, TYPE", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                RECDTOTAL = 0
                RECDGRANDTOTAL = 0

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("NAME") Then
                        TEMPNAME = ROW("NAME")
                        If GRIDADV.RowCount > 0 Then ADDADVTOTALROW(RECDTOTAL)
                        RECDTOTAL = 0
                        ADDADVNAMEROW(ROW("NAME"), ROW("MOBILENO"), ROW("PHONENO"))
                    End If
                    GRIDADV.Rows.Add(ROW("SELLERNAME"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Val(ROW("RECDAMT")), "0.00"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                Next

                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDADV.RowCount > 0 Then ADDADVTOTALROW(RECDTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLPARTGRID()
        Try
            GRIDPART.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim GTOTAL, RECDTOTAL, BALANCE, GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"



            DT = OBJCMN.Execute_Any_String(" SELECT * FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' AND RECDAMT > 0 AND BALANCE > 0 " & WHERECLAUSE & " ORDER BY NAME, DATE, TYPE, BILL", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                GTOTAL = 0
                RECDTOTAL = 0
                BALANCE = 0
                GRANDTOTAL = 0
                RECDGRANDTOTAL = 0
                BALANCEGRANDTOTAL = 0

                GRIDOUTSTANDING.DefaultCellStyle.Font = New Drawing.Font("Verdana", 8, FontStyle.Regular)

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("NAME") Then
                        TEMPNAME = ROW("NAME")
                        If GRIDPART.RowCount > 0 Then ADDPARTPAIDTOTALROW(GTOTAL, RECDTOTAL, BALANCE)
                        GTOTAL = 0
                        RECDTOTAL = 0
                        BALANCE = 0
                        ADDPARTNAMEROW(ROW("NAME"), ROW("MOBILENO"), ROW("PHONENO"))
                    End If
                    GRIDPART.Rows.Add(ROW("SELLERNAME"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), ROW("ITEMNAME"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date))
                    GTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    BALANCE += Val(ROW("BALANCE"))

                    GRANDTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDPART.RowCount > 0 Then ADDPARTPAIDTOTALROW(GTOTAL, RECDTOTAL, BALANCE)
                If GRIDPART.RowCount > 0 Then ADDPARTGRANDTOTALROW(GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDNAMEROW(ByVal NAME, ByVal MOBILENO, ByVal PHONENO, ByVal CITYNAME)
        Try
            'PRINT NAME 
            GRIDOUTSTANDING.Rows.Add(NAME, CITYNAME, "CONTACT", MOBILENO, PHONENO)
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle.Font = New Drawing.Font("Verdana", 8, FontStyle.Bold)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDADVNAMEROW(ByVal NAME, ByVal MOBILENO, ByVal PHONENO)
        Try
            'PRINT NAME 
            GRIDADV.Rows.Add(NAME, "CONTACT NO : ", MOBILENO, PHONENO)
            GRIDADV.Rows(GRIDADV.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDPARTNAMEROW(ByVal NAME, ByVal MOBILENO, ByVal PHONENO)
        Try
            'PRINT NAME 
            GRIDPART.Rows.Add(NAME, "CONTACT NO : ", MOBILENO, PHONENO)
            GRIDPART.Rows(GRIDPART.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDPARTYTOTALROW(ByVal GTOTAL As Decimal, ByVal RECDTOTAL As Decimal, ByVal BALANCE As Decimal, PARTYINTTOTAL As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDOUTSTANDING.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDOUTSTANDING.Rows.Add("SUBTOTAL", "", "", "", "", "", "", "", Format(Val(GTOTAL), "0.00"), "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "", "", "", "", "", "", "", "", "", "", PARTYINTTOTAL)
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle = STYLE
            GRIDOUTSTANDING.Rows.Add()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDPARTPAIDTOTALROW(ByVal GTOTAL As Decimal, ByVal RECDTOTAL As Decimal, ByVal BALANCE As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDPART.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDPART.Rows.Add("SUBTOTAL", "", "", "", Format(Val(GTOTAL), "0.00"), "", "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "")
            GRIDPART.Rows(GRIDPART.RowCount - 1).DefaultCellStyle = STYLE
            GRIDPART.Rows.Add()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDSUMMTOTALROW(ByVal BALANCE As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDSUMM.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDSUMM.Rows.Add("TOTAL", Format(Val(BALANCE), "0.00"))
            GRIDSUMM.Rows(GRIDSUMM.RowCount - 1).DefaultCellStyle = STYLE
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDADVTOTALROW(ByVal BALANCE As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDADV.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDADV.Rows.Add("SUBTOTAL", "", "", Format(Val(BALANCE), "0.00"))
            GRIDADV.Rows(GRIDADV.RowCount - 1).DefaultCellStyle = STYLE
            GRIDADV.Rows.Add()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDSUMMGRANDTOTALROW(ByVal BALANCE As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDSUMM.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Orange
            GRIDSUMM.Rows.Add("GRANDTOTAL", Format(Val(BALANCE), "0.00"), "")
            GRIDSUMM.Rows(GRIDSUMM.RowCount - 1).DefaultCellStyle = STYLE
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDGRANDTOTALROW(ByVal GTOTAL As Decimal, ByVal RECDTOTAL As Decimal, ByVal BALANCE As Decimal, INTTOTAL As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDOUTSTANDING.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Orange
            GRIDOUTSTANDING.Rows.Add("GRANDTOTAL", "", "", "", "", "", "", "", Format(Val(GTOTAL), "0.00"), "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "", "", "", "", "", "", "", "", "", "", Val(INTTOTAL))
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle = STYLE
            GRIDOUTSTANDING.Rows.Add()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDPARTGRANDTOTALROW(ByVal GTOTAL As Decimal, ByVal RECDTOTAL As Decimal, ByVal BALANCE As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDPART.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Orange
            GRIDPART.Rows.Add("GRANDTOTAL", "", "", "", Format(Val(GTOTAL), "0.00"), "", "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "")
            GRIDPART.Rows(GRIDPART.RowCount - 1).DefaultCellStyle = STYLE
            GRIDPART.Rows.Add()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "SELLERWISE"

    Sub FILLSELLERNAMEGRID()
        Try
            GRIDOUTSTANDING.RowCount = 0
            GCMPNAME.Visible = False
            Dim TEMPNAME As String = ""
            Dim TEMPBUYERNAME As String = ""
            Dim GTOTAL, RECDTOTAL, BALANCE, GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL, PARTYINTTOTAL, SGTOTAL, SRECDTOTAL, SBALANCE, SGRANDTOTAL, SRECDGRANDTOTAL, SBALANCEGRANDTOTAL, SGINTTOTAL, SPARTYINTTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then
                WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
            End If
            Mydate = dtto.Value.Date

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"

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
                    GCMPNAME.Visible = True
                End If
            Next item


            DT = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"



            Dim DAYS As Integer = 0
            Dim TOTALDAYS As Integer = 0
            Dim RUNNINGBAL As Double = 0.0
            Dim SRNO As Integer = 0
            Dim BILLINTEREST As Double = 0.0


            'WE ARE PASSING YEARID FROM ABOVE CLAUSE SO NO NEED TO ENTER YEARID HERE
            DT = OBJCMN.Execute_Any_String(" SELECT AGENCYOUTSTANDINGREC.*, CMPMASTER.CMP_NAME AS CMPNAME FROM AGENCYOUTSTANDINGREC INNER JOIN CMPMASTER ON CMPID = CMP_ID WHERE SECONDARY = 'Sundry Debtors' AND ROUND(BALANCE,2) <> 0 " & WHERECLAUSE & " ORDER BY SELLERNAME, NAME, DATE, TYPE, BILL", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                TEMPBUYERNAME = ""
                GTOTAL = 0
                RECDTOTAL = 0
                BALANCE = 0
                SGTOTAL = 0
                SRECDTOTAL = 0
                SBALANCE = 0
                GRANDTOTAL = 0
                RECDGRANDTOTAL = 0
                BALANCEGRANDTOTAL = 0
                DAYS = 0
                TOTALDAYS = 0
                RUNNINGBAL = 0.0
                BILLINTEREST = 0.0
                PARTYINTTOTAL = 0.0
                GINTTOTAL = 0.0
                SRNO = 0

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("SELLERNAME") Then
                        TEMPNAME = ROW("SELLERNAME")
                        If GRIDOUTSTANDING.RowCount > 0 Then ADDPARTYTOTALROW(GTOTAL, RECDTOTAL, BALANCE, PARTYINTTOTAL)
                        GTOTAL = 0
                        RECDTOTAL = 0
                        BALANCE = 0
                        RUNNINGBAL = 0.0
                        SRNO = 0
                        PARTYINTTOTAL = 0.0
                        ADDSELLERNAMENAMEROW(ROW("SELLERNAME"))
                    End If

                    If TEMPBUYERNAME <> ROW("NAME") Then
                        TEMPBUYERNAME = ROW("NAME")
                        If GRIDOUTSTANDING.RowCount > 1 Then ADDPARTYTOTALROW(SGTOTAL, SRECDTOTAL, SBALANCE, SPARTYINTTOTAL)
                        SGTOTAL = 0
                        SRECDTOTAL = 0
                        SBALANCE = 0
                        SPARTYINTTOTAL = 0
                        RUNNINGBAL = 0.0
                        SRNO = 0
                    End If

                    DAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date)
                    TOTALDAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DATE")).Date, Mydate.Date)
                    If Val(TXTPERCENT.Text.Trim) > 0 And Val(TXTDAYS.Text.Trim) > 0 Then BILLINTEREST = Format((Val(TXTPERCENT.Text.Trim) / Val(TXTDAYS.Text.Trim) / 100) * Val(DAYS) * Val(ROW("BALANCE")), "0")

                    SRNO += 1
                    RUNNINGBAL += Val(ROW("BALANCE"))
                    'GRIDOUTSTANDING.Rows.Add(ROW("NAME"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), ROW("ITEMNAME"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date), Format(Val(ROW("CHARGES")), "0.00"), ROW("CMPNAME"))
                    GRIDOUTSTANDING.Rows.Add(ROW("NAME"), ROW("PRINTINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), ROW("ITEMNAME"), Val(ROW("TOTALPCS")), Format(Val(ROW("TOTALMTRS")), "0.00"), Format(Val(ROW("RATE")), "0.00"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), Format(Val(RUNNINGBAL), "0.00"), Val(SRNO), Val(ROW("CRDAYS")), Val(DAYS), Val(TOTALDAYS), Format(Val(ROW("CHARGES")), "0.00"), ROW("CMPNAME"), ROW("TYPE"), Val(ROW("BILL")), ROW("REGTYPE"), Val(BILLINTEREST), ROW("HOLDINTCALC"), ROW("COMPLAINT"), ROW("COMPLAINTBY"), ROW("COMPLAINTDATE"))
                    GTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    BALANCE += Val(ROW("BALANCE"))
                    PARTYINTTOTAL += Val(BILLINTEREST)

                    SGTOTAL += Val(ROW("GRANDTOTAL"))
                    SRECDTOTAL += Val(ROW("RECDAMT"))
                    SBALANCE += Val(ROW("BALANCE"))
                    SPARTYINTTOTAL += Val(BILLINTEREST)

                    GRANDTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                    GINTTOTAL += Val(BILLINTEREST)
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDOUTSTANDING.RowCount > 0 Then ADDPARTYTOTALROW(GTOTAL, RECDTOTAL, BALANCE, PARTYINTTOTAL)
                If GRIDOUTSTANDING.RowCount > 0 Then ADDGRANDTOTALROW(GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSELLERNAMESUMMGRID()
        Try
            GRIDSUMM.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim BALANCE, BALANCEGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            DT = OBJCMN.Execute_Any_String(" SELECT SELLERNAME,SUM(BALANCE) AS BALANCE FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' " & WHERECLAUSE & " GROUP BY SELLERNAME HAVING ROUND(SUM(BALANCE),2) <> 0", "", "")
            If DT.Rows.Count > 0 Then
                BALANCE = 0
                BALANCEGRANDTOTAL = 0
                For Each ROW As DataRow In DT.Rows
                    GRIDSUMM.Rows.Add(ROW("SELLERNAME"), Format(Val(ROW("BALANCE")), "0.00"))
                    BALANCE += Val(ROW("BALANCE"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDSUMM.RowCount > 0 Then ADDSUMMTOTALROW(BALANCE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSELLERNAMEADVGRID()
        Try
            GRIDADV.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim RECDTOTAL, RECDGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            'If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            'If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            'If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            DT = OBJCMN.Execute_Any_String(" SELECT BILLINITIALS,DATE,NAME, SELLERNAME,RECDAMT, MOBILENO, PHONENO FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' AND TYPE='RECEIPT' " & WHERECLAUSE & " ORDER BY SELLERNAME, DATE, TYPE", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                RECDTOTAL = 0
                RECDGRANDTOTAL = 0

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("SELLERNAME") Then
                        TEMPNAME = ROW("SELLERNAME")
                        If GRIDADV.RowCount > 0 Then ADDADVTOTALROW(RECDTOTAL)
                        RECDTOTAL = 0
                        ADDSELLERNAMEADVNAMEROW(ROW("SELLERNAME"))
                    End If
                    GRIDADV.Rows.Add(ROW("NAME"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Val(ROW("RECDAMT")), "0.00"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                Next

                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDADV.RowCount > 0 Then ADDADVTOTALROW(RECDTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSELLERNAMEPARTGRID()
        Try
            GRIDPART.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim GTOTAL, RECDTOTAL, BALANCE, GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            DT = OBJCMN.Execute_Any_String(" SELECT * FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' AND RECDAMT > 0 AND BALANCE > 0 " & WHERECLAUSE & " ORDER BY SELLERNAME, NAME, DATE, TYPE, BILL", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                GTOTAL = 0
                RECDTOTAL = 0
                BALANCE = 0
                GRANDTOTAL = 0
                RECDGRANDTOTAL = 0
                BALANCEGRANDTOTAL = 0

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("SELLERNAME") Then
                        TEMPNAME = ROW("SELLERNAME")
                        If GRIDPART.RowCount > 0 Then ADDPARTPAIDTOTALROW(GTOTAL, RECDTOTAL, BALANCE)
                        GTOTAL = 0
                        RECDTOTAL = 0
                        BALANCE = 0
                        ADDSELLERNAMEPARTNAMEROW(ROW("SELLERNAME"))
                    End If
                    GRIDPART.Rows.Add(ROW("NAME"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), ROW("ITEMNAME"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date))
                    GTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    BALANCE += Val(ROW("BALANCE"))

                    GRANDTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDPART.RowCount > 0 Then ADDPARTPAIDTOTALROW(GTOTAL, RECDTOTAL, BALANCE)
                If GRIDPART.RowCount > 0 Then ADDPARTGRANDTOTALROW(GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDSELLERNAMENAMEROW(ByVal SELLERNAME)
        Try
            'PRINT NAME 
            GRIDOUTSTANDING.Rows.Add(SELLERNAME)
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDSELLERNAMEADVNAMEROW(ByVal SELLERNAME)
        Try
            'PRINT NAME 
            GRIDADV.Rows.Add(SELLERNAME)
            GRIDADV.Rows(GRIDADV.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDSELLERNAMEPARTNAMEROW(ByVal SELLERNAME)
        Try
            'PRINT NAME 
            GRIDPART.Rows.Add(SELLERNAME)
            GRIDPART.Rows(GRIDPART.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "CITYWISE"

    Sub FILLCITYGRID()
        Try
            GRIDOUTSTANDING.RowCount = 0
            GCMPNAME.Visible = False

            Dim TEMPNAME As String = ""
            Dim GTOTAL, RECDTOTAL, BALANCE, GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL, PARTYINTTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"

            If chkdate.CheckState = CheckState.Checked Then
                WHERECLAUSE = WHERECLAUSE & " AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
            End If
            Mydate = dtto.Value.Date

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If TXTOVERDUEDAYS.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  = '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"

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
                    GCMPNAME.Visible = True
                End If
            Next item



            DT = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            Dim DAYS As Integer = 0
            Dim TOTALDAYS As Integer = 0
            Dim RUNNINGBAL As Double = 0.0
            Dim BILLINTEREST As Double = 0.0
            Dim SRNO As Integer = 0

            'WE ARE PASSING YEARID FROM ABOVE CLAUSE SO NO NEED TO ENTER YEARID HERE
            DT = OBJCMN.Execute_Any_String(" SELECT AGENCYOUTSTANDINGREC.*, CMPMASTER.CMP_NAME AS CMPNAME FROM AGENCYOUTSTANDINGREC INNER JOIN CMPMASTER ON CMPID = CMP_ID WHERE SECONDARY = 'Sundry Debtors' AND ROUND(BALANCE,2) <> 0 " & WHERECLAUSE & " ORDER BY CITY, NAME, DATE, TYPE, BILL", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                GTOTAL = 0
                RECDTOTAL = 0
                BALANCE = 0
                GRANDTOTAL = 0
                RECDGRANDTOTAL = 0
                BALANCEGRANDTOTAL = 0
                DAYS = 0
                TOTALDAYS = 0
                RUNNINGBAL = 0.0
                SRNO = 0
                BILLINTEREST = 0
                PARTYINTTOTAL = 0
                GINTTOTAL = 0

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("CITY") Then
                        TEMPNAME = ROW("CITY")
                        If GRIDOUTSTANDING.RowCount > 0 Then ADDCITYTOTALROW(GTOTAL, RECDTOTAL, BALANCE, PARTYINTTOTAL)
                        GTOTAL = 0
                        RECDTOTAL = 0
                        BALANCE = 0
                        RUNNINGBAL = 0.0
                        SRNO = 0
                        PARTYINTTOTAL = 0
                        ADDCITYNAMEROW(ROW("CITY"), "", "", "")
                    End If

                    DAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date)
                    TOTALDAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DATE")).Date, Mydate.Date)
                    If Val(TXTPERCENT.Text.Trim) > 0 And Val(TXTDAYS.Text.Trim) > 0 Then BILLINTEREST = Format((Val(TXTPERCENT.Text.Trim) / Val(TXTDAYS.Text.Trim) / 100) * Val(DAYS) * Val(ROW("BALANCE")), "0")

                    SRNO += 1
                    RUNNINGBAL += Val(ROW("BALANCE"))
                    GRIDOUTSTANDING.Rows.Add(ROW("NAME"), ROW("PRINTINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), ROW("ITEMNAME"), Val(ROW("TOTALPCS")), Format(Val(ROW("TOTALMTRS")), "0.00"), Format(Val(ROW("RATE")), "0.00"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), Format(Val(RUNNINGBAL), "0.00"), Val(SRNO), Val(ROW("CRDAYS")), Val(DAYS), Val(TOTALDAYS), Format(Val(ROW("CHARGES")), "0.00"), ROW("CMPNAME"), ROW("TYPE"), Val(ROW("BILL")), ROW("REGTYPE"), Val(BILLINTEREST), ROW("HOLDINTCALC"), ROW("COMPLAINT"), ROW("COMPLAINTBY"), ROW("COMPLAINTDATE"))
                    GTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    BALANCE += Val(ROW("BALANCE"))
                    PARTYINTTOTAL += Val(BILLINTEREST)

                    GRANDTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                    GINTTOTAL += Val(BILLINTEREST)
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDOUTSTANDING.RowCount > 0 Then ADDCITYTOTALROW(GTOTAL, RECDTOTAL, BALANCE, PARTYINTTOTAL)
                If GRIDOUTSTANDING.RowCount > 0 Then ADDCITYGRANDTOTALROW(GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCITYSUMMGRID()
        Try
            GRIDSUMM.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim BALANCE, BALANCEGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            DT = OBJCMN.Execute_Any_String(" SELECT CITY, SUM(BALANCE) AS BALANCE FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' " & WHERECLAUSE & " GROUP BY CITY HAVING ROUND(SUM(BALANCE),2) <> 0 order by BALANCE", "", "")
            If DT.Rows.Count > 0 Then
                BALANCE = 0
                BALANCEGRANDTOTAL = 0
                For Each ROW As DataRow In DT.Rows
                    GRIDSUMM.Rows.Add(ROW("CITY"), Format(Val(ROW("BALANCE")), "0.00"))
                    BALANCE += Val(ROW("BALANCE"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDSUMM.RowCount > 0 Then ADDCITYSUMMTOTALROW(BALANCE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCITYADVGRID()
        Try
            GRIDADV.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim RECDTOTAL, RECDGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            'If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            'If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            'If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            DT = OBJCMN.Execute_Any_String(" SELECT BILLINITIALS,DATE,NAME, SELLERNAME,RECDAMT, MOBILENO, PHONENO FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' AND TYPE='RECEIPT' " & WHERECLAUSE & " ORDER BY SELLERNAME, DATE, TYPE", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                RECDTOTAL = 0
                RECDGRANDTOTAL = 0

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("NAME") Then
                        TEMPNAME = ROW("NAME")
                        If GRIDADV.RowCount > 0 Then ADDADVTOTALROW(RECDTOTAL)
                        RECDTOTAL = 0
                        ADDADVNAMEROW(ROW("NAME"), ROW("MOBILENO"), ROW("PHONENO"))
                    End If
                    GRIDADV.Rows.Add(ROW("SELLERNAME"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Val(ROW("RECDAMT")), "0.00"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                Next

                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDADV.RowCount > 0 Then ADDADVTOTALROW(RECDTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCITYPARTGRID()
        Try
            GRIDPART.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim GTOTAL, RECDTOTAL, BALANCE, GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"



            DT = OBJCMN.Execute_Any_String(" SELECT * FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' AND RECDAMT > 0 AND BALANCE > 0 " & WHERECLAUSE & " ORDER BY NAME, DATE, TYPE, BILL", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                GTOTAL = 0
                RECDTOTAL = 0
                BALANCE = 0
                GRANDTOTAL = 0
                RECDGRANDTOTAL = 0
                BALANCEGRANDTOTAL = 0

                GRIDOUTSTANDING.DefaultCellStyle.Font = New Drawing.Font("Verdana", 8, FontStyle.Regular)

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("NAME") Then
                        TEMPNAME = ROW("NAME")
                        If GRIDPART.RowCount > 0 Then ADDPARTPAIDTOTALROW(GTOTAL, RECDTOTAL, BALANCE)
                        GTOTAL = 0
                        RECDTOTAL = 0
                        BALANCE = 0
                        ADDPARTNAMEROW(ROW("NAME"), ROW("MOBILENO"), ROW("PHONENO"))
                    End If
                    GRIDPART.Rows.Add(ROW("SELLERNAME"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), ROW("ITEMNAME"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date))
                    GTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    BALANCE += Val(ROW("BALANCE"))

                    GRANDTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDPART.RowCount > 0 Then ADDPARTPAIDTOTALROW(GTOTAL, RECDTOTAL, BALANCE)
                If GRIDPART.RowCount > 0 Then ADDPARTGRANDTOTALROW(GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDCITYNAMEROW(ByVal NAME, ByVal MOBILENO, ByVal PHONENO, ByVal CITYNAME)
        Try
            'PRINT NAME 
            GRIDOUTSTANDING.Rows.Add(NAME, "", "", "", MOBILENO, PHONENO)
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle.Font = New Drawing.Font("Verdana", 8, FontStyle.Bold)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDCITYTOTALROW(ByVal GTOTAL As Decimal, ByVal RECDTOTAL As Decimal, ByVal BALANCE As Decimal, PARTYINTTOTAL As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDOUTSTANDING.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDOUTSTANDING.Rows.Add("SUBTOTAL", "", "", "", "", "", "", "", "", Format(Val(GTOTAL), "0.00"), "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "", "", "", "", "", "", "", "", "", "", PARTYINTTOTAL)
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle = STYLE
            GRIDOUTSTANDING.Rows.Add()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDCITYSUMMTOTALROW(ByVal BALANCE As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDSUMM.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDSUMM.Rows.Add("TOTAL", Format(Val(BALANCE), "0.00"))
            GRIDSUMM.Rows(GRIDSUMM.RowCount - 1).DefaultCellStyle = STYLE
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDCITYSUMMGRANDTOTALROW(ByVal BALANCE As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDSUMM.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Orange
            GRIDSUMM.Rows.Add("GRANDTOTAL", Format(Val(BALANCE), "0.00"), "")
            GRIDSUMM.Rows(GRIDSUMM.RowCount - 1).DefaultCellStyle = STYLE
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDCITYGRANDTOTALROW(ByVal GTOTAL As Decimal, ByVal RECDTOTAL As Decimal, ByVal BALANCE As Decimal, INTTOTAL As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDOUTSTANDING.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Orange
            GRIDOUTSTANDING.Rows.Add("GRANDTOTAL", "", "", "", "", "", "", "", "", Format(Val(GTOTAL), "0.00"), "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "", "", "", "", "", "", "", "", "", "", Val(INTTOTAL))
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle = STYLE
            GRIDOUTSTANDING.Rows.Add()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "STATEWISE"

    Sub FILLSTATEGRID()
        Try
            GRIDOUTSTANDING.RowCount = 0
            GCMPNAME.Visible = False

            Dim TEMPNAME As String = ""
            Dim GTOTAL, RECDTOTAL, BALANCE, GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL, PARTYINTTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"

            If chkdate.CheckState = CheckState.Checked Then
                WHERECLAUSE = WHERECLAUSE & " AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
            End If
            Mydate = dtto.Value.Date

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If TXTOVERDUEDAYS.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  = '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"

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
                    GCMPNAME.Visible = True
                End If
            Next item



            DT = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            Dim DAYS As Integer = 0
            Dim TOTALDAYS As Integer = 0
            Dim RUNNINGBAL As Double = 0.0
            Dim BILLINTEREST As Double = 0.0
            Dim SRNO As Integer = 0

            'WE ARE PASSING YEARID FROM ABOVE CLAUSE SO NO NEED TO ENTER YEARID HERE
            DT = OBJCMN.Execute_Any_String(" SELECT AGENCYOUTSTANDINGREC.*, CMPMASTER.CMP_NAME AS CMPNAME FROM AGENCYOUTSTANDINGREC INNER JOIN CMPMASTER ON CMPID = CMP_ID WHERE SECONDARY = 'Sundry Debtors' AND ROUND(BALANCE,2) <> 0 " & WHERECLAUSE & " ORDER BY STATE, NAME, DATE, TYPE, BILL", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                GTOTAL = 0
                RECDTOTAL = 0
                BALANCE = 0
                GRANDTOTAL = 0
                RECDGRANDTOTAL = 0
                BALANCEGRANDTOTAL = 0
                DAYS = 0
                TOTALDAYS = 0
                RUNNINGBAL = 0.0
                SRNO = 0
                BILLINTEREST = 0
                PARTYINTTOTAL = 0
                GINTTOTAL = 0

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("STATE") Then
                        TEMPNAME = ROW("STATE")
                        If GRIDOUTSTANDING.RowCount > 0 Then ADDSTATETOTALROW(GTOTAL, RECDTOTAL, BALANCE, PARTYINTTOTAL)
                        GTOTAL = 0
                        RECDTOTAL = 0
                        BALANCE = 0
                        RUNNINGBAL = 0.0
                        SRNO = 0
                        PARTYINTTOTAL = 0
                        ADDSTATENAMEROW(ROW("STATE"), "", "", "")
                    End If

                    DAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date)
                    TOTALDAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DATE")).Date, Mydate.Date)
                    If Val(TXTPERCENT.Text.Trim) > 0 And Val(TXTDAYS.Text.Trim) > 0 Then BILLINTEREST = Format((Val(TXTPERCENT.Text.Trim) / Val(TXTDAYS.Text.Trim) / 100) * Val(DAYS) * Val(ROW("BALANCE")), "0")

                    SRNO += 1
                    RUNNINGBAL += Val(ROW("BALANCE"))
                    GRIDOUTSTANDING.Rows.Add(ROW("NAME"), ROW("PRINTINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), ROW("ITEMNAME"), Val(ROW("TOTALPCS")), Format(Val(ROW("TOTALMTRS")), "0.00"), Format(Val(ROW("RATE")), "0.00"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), Format(Val(RUNNINGBAL), "0.00"), Val(SRNO), Val(ROW("CRDAYS")), Val(DAYS), Val(TOTALDAYS), Format(Val(ROW("CHARGES")), "0.00"), ROW("CMPNAME"), ROW("TYPE"), Val(ROW("BILL")), ROW("REGTYPE"), Val(BILLINTEREST), ROW("HOLDINTCALC"), ROW("COMPLAINT"), ROW("COMPLAINTBY"), ROW("COMPLAINTDATE"))
                    GTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    BALANCE += Val(ROW("BALANCE"))
                    PARTYINTTOTAL += Val(BILLINTEREST)

                    GRANDTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                    GINTTOTAL += Val(BILLINTEREST)
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDOUTSTANDING.RowCount > 0 Then ADDSTATETOTALROW(GTOTAL, RECDTOTAL, BALANCE, PARTYINTTOTAL)
                If GRIDOUTSTANDING.RowCount > 0 Then ADDSTATEGRANDTOTALROW(GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSTATESUMMGRID()
        Try
            GRIDSUMM.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim BALANCE, BALANCEGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            DT = OBJCMN.Execute_Any_String(" SELECT STATE, SUM(BALANCE) AS BALANCE FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' " & WHERECLAUSE & " GROUP BY STATE HAVING ROUND(SUM(BALANCE),2) <> 0 order by BALANCE", "", "")
            If DT.Rows.Count > 0 Then
                BALANCE = 0
                BALANCEGRANDTOTAL = 0
                For Each ROW As DataRow In DT.Rows
                    GRIDSUMM.Rows.Add(ROW("STATE"), Format(Val(ROW("BALANCE")), "0.00"))
                    BALANCE += Val(ROW("BALANCE"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDSUMM.RowCount > 0 Then ADDSTATESUMMTOTALROW(BALANCE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSTATEADVGRID()
        Try
            GRIDADV.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim RECDTOTAL, RECDGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            'If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            'If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            'If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            DT = OBJCMN.Execute_Any_String(" SELECT BILLINITIALS,DATE,NAME, SELLERNAME,RECDAMT, MOBILENO, PHONENO FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' AND TYPE='RECEIPT' " & WHERECLAUSE & " ORDER BY SELLERNAME, DATE, TYPE", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                RECDTOTAL = 0
                RECDGRANDTOTAL = 0

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("NAME") Then
                        TEMPNAME = ROW("NAME")
                        If GRIDADV.RowCount > 0 Then ADDADVTOTALROW(RECDTOTAL)
                        RECDTOTAL = 0
                        ADDADVNAMEROW(ROW("NAME"), ROW("MOBILENO"), ROW("PHONENO"))
                    End If
                    GRIDADV.Rows.Add(ROW("SELLERNAME"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Val(ROW("RECDAMT")), "0.00"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                Next

                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDADV.RowCount > 0 Then ADDADVTOTALROW(RECDTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSTATEPARTGRID()
        Try
            GRIDPART.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim GTOTAL, RECDTOTAL, BALANCE, GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"



            DT = OBJCMN.Execute_Any_String(" SELECT * FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' AND RECDAMT > 0 AND BALANCE > 0 " & WHERECLAUSE & " ORDER BY NAME, DATE, TYPE, BILL", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                GTOTAL = 0
                RECDTOTAL = 0
                BALANCE = 0
                GRANDTOTAL = 0
                RECDGRANDTOTAL = 0
                BALANCEGRANDTOTAL = 0

                GRIDOUTSTANDING.DefaultCellStyle.Font = New Drawing.Font("Verdana", 8, FontStyle.Regular)

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("NAME") Then
                        TEMPNAME = ROW("NAME")
                        If GRIDPART.RowCount > 0 Then ADDPARTPAIDTOTALROW(GTOTAL, RECDTOTAL, BALANCE)
                        GTOTAL = 0
                        RECDTOTAL = 0
                        BALANCE = 0
                        ADDPARTNAMEROW(ROW("NAME"), ROW("MOBILENO"), ROW("PHONENO"))
                    End If
                    GRIDPART.Rows.Add(ROW("SELLERNAME"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), ROW("ITEMNAME"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date))
                    GTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    BALANCE += Val(ROW("BALANCE"))

                    GRANDTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDPART.RowCount > 0 Then ADDPARTPAIDTOTALROW(GTOTAL, RECDTOTAL, BALANCE)
                If GRIDPART.RowCount > 0 Then ADDPARTGRANDTOTALROW(GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDSTATENAMEROW(ByVal NAME, ByVal MOBILENO, ByVal PHONENO, ByVal CITYNAME)
        Try
            'PRINT NAME 
            GRIDOUTSTANDING.Rows.Add(NAME, "", "", "", MOBILENO, PHONENO)
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle.Font = New Drawing.Font("Verdana", 8, FontStyle.Bold)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDSTATETOTALROW(ByVal GTOTAL As Decimal, ByVal RECDTOTAL As Decimal, ByVal BALANCE As Decimal, PARTYINTTOTAL As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDOUTSTANDING.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDOUTSTANDING.Rows.Add("SUBTOTAL", "", "", "", "", "", "", "", "", Format(Val(GTOTAL), "0.00"), "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "", "", "", "", "", "", "", "", "", "", PARTYINTTOTAL)
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle = STYLE
            GRIDOUTSTANDING.Rows.Add()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDSTATESUMMTOTALROW(ByVal BALANCE As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDSUMM.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDSUMM.Rows.Add("TOTAL", Format(Val(BALANCE), "0.00"))
            GRIDSUMM.Rows(GRIDSUMM.RowCount - 1).DefaultCellStyle = STYLE
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDSTATESUMMGRANDTOTALROW(ByVal BALANCE As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDSUMM.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Orange
            GRIDSUMM.Rows.Add("GRANDTOTAL", Format(Val(BALANCE), "0.00"), "")
            GRIDSUMM.Rows(GRIDSUMM.RowCount - 1).DefaultCellStyle = STYLE
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDSTATEGRANDTOTALROW(ByVal GTOTAL As Decimal, ByVal RECDTOTAL As Decimal, ByVal BALANCE As Decimal, INTTOTAL As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDOUTSTANDING.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Orange
            GRIDOUTSTANDING.Rows.Add("GRANDTOTAL", "", "", "", "", "", "", "", "", Format(Val(GTOTAL), "0.00"), "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "", "", "", "", "", "", "", "", "", "", Val(INTTOTAL))
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle = STYLE
            GRIDOUTSTANDING.Rows.Add()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "MONTHWISE"

    Sub FILLMONTHGRID()
        Try
            GRIDOUTSTANDING.RowCount = 0
            GCMPNAME.Visible = False

            Dim TEMPNAME As String = ""
            Dim GTOTAL, RECDTOTAL, BALANCE, GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL, PARTYINTTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"

            If chkdate.CheckState = CheckState.Checked Then
                WHERECLAUSE = WHERECLAUSE & " AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
            End If
            Mydate = dtto.Value.Date

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If TXTOVERDUEDAYS.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  = '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"

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
                    GCMPNAME.Visible = True
                End If
            Next item



            DT = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            Dim DAYS As Integer = 0
            Dim TOTALDAYS As Integer = 0
            Dim RUNNINGBAL As Double = 0.0
            Dim BILLINTEREST As Double = 0.0
            Dim SRNO As Integer = 0

            'WE ARE PASSING YEARID FROM ABOVE CLAUSE SO NO NEED TO ENTER YEARID HERE
            DT = OBJCMN.Execute_Any_String(" SELECT AGENCYOUTSTANDINGREC.*, CMPMASTER.CMP_NAME AS CMPNAME FROM AGENCYOUTSTANDINGREC INNER JOIN CMPMASTER ON CMPID = CMP_ID WHERE SECONDARY = 'Sundry Debtors' AND ROUND(BALANCE,2) <> 0 " & WHERECLAUSE & " ORDER BY CASE WHEN MONTHNAME = 'APRIL' THEN 0 WHEN MONTHNAME = 'MAY' THEN 1 WHEN MONTHNAME = 'JUNE' THEN 2 WHEN MONTHNAME = 'JULY' THEN 3 WHEN MONTHNAME = 'AUGUST' THEN 4 WHEN MONTHNAME = 'SEPTEMBER' THEN 5 WHEN MONTHNAME = 'OCTOBER' THEN 6 WHEN MONTHNAME = 'NOVEMBER' THEN 7 WHEN MONTHNAME = 'DECEMBER' THEN 8 WHEN MONTHNAME = 'JANUARY' THEN 9 WHEN MONTHNAME = 'FEBRUARY' THEN 10 WHEN MONTHNAME = 'MARCH' THEN 11 END, NAME, DATE, TYPE, BILL", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                GTOTAL = 0
                RECDTOTAL = 0
                BALANCE = 0
                GRANDTOTAL = 0
                RECDGRANDTOTAL = 0
                BALANCEGRANDTOTAL = 0
                DAYS = 0
                TOTALDAYS = 0
                RUNNINGBAL = 0.0
                SRNO = 0
                BILLINTEREST = 0
                PARTYINTTOTAL = 0
                GINTTOTAL = 0

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("MONTHNAME") Then
                        TEMPNAME = ROW("MONTHNAME")
                        If GRIDOUTSTANDING.RowCount > 0 Then ADDMONTHTOTALROW(GTOTAL, RECDTOTAL, BALANCE, PARTYINTTOTAL)
                        GTOTAL = 0
                        RECDTOTAL = 0
                        BALANCE = 0
                        RUNNINGBAL = 0.0
                        SRNO = 0
                        PARTYINTTOTAL = 0
                        ADDMONTHNAMEROW(ROW("MONTHNAME"), "", "", "")
                    End If

                    DAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date)
                    TOTALDAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DATE")).Date, Mydate.Date)
                    If Val(TXTPERCENT.Text.Trim) > 0 And Val(TXTDAYS.Text.Trim) > 0 Then BILLINTEREST = Format((Val(TXTPERCENT.Text.Trim) / Val(TXTDAYS.Text.Trim) / 100) * Val(DAYS) * Val(ROW("BALANCE")), "0")

                    SRNO += 1
                    RUNNINGBAL += Val(ROW("BALANCE"))
                    GRIDOUTSTANDING.Rows.Add(ROW("NAME"), ROW("PRINTINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), ROW("ITEMNAME"), Val(ROW("TOTALPCS")), Format(Val(ROW("TOTALMTRS")), "0.00"), Format(Val(ROW("RATE")), "0.00"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), Format(Val(RUNNINGBAL), "0.00"), Val(SRNO), Val(ROW("CRDAYS")), Val(DAYS), Val(TOTALDAYS), Format(Val(ROW("CHARGES")), "0.00"), ROW("CMPNAME"), ROW("TYPE"), Val(ROW("BILL")), ROW("REGTYPE"), Val(BILLINTEREST), ROW("HOLDINTCALC"), ROW("COMPLAINT"), ROW("COMPLAINTBY"), ROW("COMPLAINTDATE"))
                    GTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    BALANCE += Val(ROW("BALANCE"))
                    PARTYINTTOTAL += Val(BILLINTEREST)

                    GRANDTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                    GINTTOTAL += Val(BILLINTEREST)
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDOUTSTANDING.RowCount > 0 Then ADDMONTHTOTALROW(GTOTAL, RECDTOTAL, BALANCE, PARTYINTTOTAL)
                If GRIDOUTSTANDING.RowCount > 0 Then ADDMONTHGRANDTOTALROW(GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLMONTHSUMMGRID()
        Try
            GRIDSUMM.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim BALANCE, BALANCEGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            DT = OBJCMN.Execute_Any_String(" SELECT MONTHNAME, SUM(BALANCE) AS BALANCE FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' " & WHERECLAUSE & " GROUP BY MONTHNAME HAVING ROUND(SUM(BALANCE),2) <> 0 order by BALANCE", "", "")
            If DT.Rows.Count > 0 Then
                BALANCE = 0
                BALANCEGRANDTOTAL = 0
                For Each ROW As DataRow In DT.Rows
                    GRIDSUMM.Rows.Add(ROW("MONTHNAME"), Format(Val(ROW("BALANCE")), "0.00"))
                    BALANCE += Val(ROW("BALANCE"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDSUMM.RowCount > 0 Then ADDMONTHSUMMTOTALROW(BALANCE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLMONTHADVGRID()
        Try
            GRIDADV.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim RECDTOTAL, RECDGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            'If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            'If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            'If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"


            DT = OBJCMN.Execute_Any_String(" SELECT BILLINITIALS,DATE,NAME, SELLERNAME,RECDAMT, MOBILENO, PHONENO FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' AND TYPE='RECEIPT' " & WHERECLAUSE & " ORDER BY SELLERNAME, DATE, TYPE", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                RECDTOTAL = 0
                RECDGRANDTOTAL = 0

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("NAME") Then
                        TEMPNAME = ROW("NAME")
                        If GRIDADV.RowCount > 0 Then ADDADVTOTALROW(RECDTOTAL)
                        RECDTOTAL = 0
                        ADDADVNAMEROW(ROW("NAME"), ROW("MOBILENO"), ROW("PHONENO"))
                    End If
                    GRIDADV.Rows.Add(ROW("SELLERNAME"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Val(ROW("RECDAMT")), "0.00"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                Next

                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDADV.RowCount > 0 Then ADDADVTOTALROW(RECDTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLMONTHPARTGRID()
        Try
            GRIDPART.RowCount = 0
            Dim TEMPNAME As String = ""
            Dim GTOTAL, RECDTOTAL, BALANCE, GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBBUYERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBBUYERNAME.Text.Trim & "'"
            If CMBSELLERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND SELLERNAME = '" & CMBSELLERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CHKHOLDINTCALC.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND CAST(HOLDINTCALC AS bit) = 'FALSE'"
            If chkdate.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"

            If CHKDUE.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND DUEDATE < '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"
            If CHKLASTYEAR.CheckState = CheckState.Checked Then WHERECLAUSE = WHERECLAUSE & " AND TYPE = 'OPENING'"
            If Val(TXTOVERDUEDAYS.Text.Trim) > 0 Then WHERECLAUSE = WHERECLAUSE & " AND DATEADD(DAY, " & Val(TXTOVERDUEDAYS.Text.Trim) & ", DUEDATE)  <= '" & Format(Mydate.Date, "MM/dd/yyyy") & "'"


            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In DT.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            WHERECLAUSE = WHERECLAUSE & " AND YEARID IN (" & CMPCLAUSE & ")"



            DT = OBJCMN.Execute_Any_String(" SELECT * FROM AGENCYOUTSTANDINGREC WHERE SECONDARY = 'Sundry Debtors' AND RECDAMT > 0 AND BALANCE > 0 " & WHERECLAUSE & " ORDER BY NAME, DATE, TYPE, BILL", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                GTOTAL = 0
                RECDTOTAL = 0
                BALANCE = 0
                GRANDTOTAL = 0
                RECDGRANDTOTAL = 0
                BALANCEGRANDTOTAL = 0

                GRIDOUTSTANDING.DefaultCellStyle.Font = New Drawing.Font("Verdana", 8, FontStyle.Regular)

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("NAME") Then
                        TEMPNAME = ROW("NAME")
                        If GRIDPART.RowCount > 0 Then ADDPARTPAIDTOTALROW(GTOTAL, RECDTOTAL, BALANCE)
                        GTOTAL = 0
                        RECDTOTAL = 0
                        BALANCE = 0
                        ADDPARTNAMEROW(ROW("NAME"), ROW("MOBILENO"), ROW("PHONENO"))
                    End If
                    GRIDPART.Rows.Add(ROW("SELLERNAME"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), ROW("ITEMNAME"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date))
                    GTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDTOTAL += Val(ROW("RECDAMT"))
                    BALANCE += Val(ROW("BALANCE"))

                    GRANDTOTAL += Val(ROW("GRANDTOTAL"))
                    RECDGRANDTOTAL += Val(ROW("RECDAMT"))
                    BALANCEGRANDTOTAL += Val(ROW("BALANCE"))
                Next
                'FOR LAST RECORD WE NNEED TO ADD TOTAL ALSO
                If GRIDPART.RowCount > 0 Then ADDPARTPAIDTOTALROW(GTOTAL, RECDTOTAL, BALANCE)
                If GRIDPART.RowCount > 0 Then ADDPARTGRANDTOTALROW(GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDMONTHNAMEROW(ByVal NAME, ByVal MOBILENO, ByVal PHONENO, ByVal CITYNAME)
        Try
            'PRINT NAME 
            GRIDOUTSTANDING.Rows.Add(NAME, "", "", "", MOBILENO, PHONENO)
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle.Font = New Drawing.Font("Verdana", 8, FontStyle.Bold)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDMONTHTOTALROW(ByVal GTOTAL As Decimal, ByVal RECDTOTAL As Decimal, ByVal BALANCE As Decimal, PARTYINTTOTAL As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDOUTSTANDING.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDOUTSTANDING.Rows.Add("SUBTOTAL", "", "", "", "", "", "", "", "", Format(Val(GTOTAL), "0.00"), "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "", "", "", "", "", "", "", "", "", "", PARTYINTTOTAL)
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle = STYLE
            GRIDOUTSTANDING.Rows.Add()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDMONTHSUMMTOTALROW(ByVal BALANCE As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDSUMM.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDSUMM.Rows.Add("TOTAL", Format(Val(BALANCE), "0.00"))
            GRIDSUMM.Rows(GRIDSUMM.RowCount - 1).DefaultCellStyle = STYLE
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDMONTHSUMMGRANDTOTALROW(ByVal BALANCE As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDSUMM.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Orange
            GRIDSUMM.Rows.Add("GRANDTOTAL", Format(Val(BALANCE), "0.00"), "")
            GRIDSUMM.Rows(GRIDSUMM.RowCount - 1).DefaultCellStyle = STYLE
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDMONTHGRANDTOTALROW(ByVal GTOTAL As Decimal, ByVal RECDTOTAL As Decimal, ByVal BALANCE As Decimal, INTTOTAL As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Drawing.Font(GRIDOUTSTANDING.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Orange
            GRIDOUTSTANDING.Rows.Add("GRANDTOTAL", "", "", "", "", "", "", "", "", Format(Val(GTOTAL), "0.00"), "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "", "", "", "", "", "", "", "", "", "", Val(INTTOTAL))
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle = STYLE
            GRIDOUTSTANDING.Rows.Add()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

    Private Sub AgencyOutstandingGridReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            'DONE BY GULKIT, COZ IF FILLDONE IS NOT MENTIONED HERE THEN EVERYTIME IT GOES TO SELECTEDINDEXCHANGE EVENT ON FILLING THE COMBO
            FILLDONE = True
            CMBREPORTTYPE.SelectedIndex = 0

            If PARTYNAME <> "" Then CMBBUYERNAME.Text = PARTYNAME

            dtfrom.Value = AccFrom.Date
            dtto.Value = Now.Date




            FILLGRID()
            FILLSUMMGRID()
            FILLADVGRID()
            FILLPARTGRID()
            GRIDSUMM.Columns(SBALANCE.Index).CellTemplate.ValueType = GetType(Decimal)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDSEARCH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSEARCH.Click
        Try
            Mydate = Now.Date
            If Val(TXTPERCENT.Text.Trim) > 0 And Val(TXTDAYS.Text.Trim) > 0 Then GINTAMT.Visible = True Else GINTAMT.Visible = False
            If CMBREPORTTYPE.Text = "BUYERWISE" Then

                FILLGRID()
                FILLSUMMGRID()
                FILLADVGRID()
                FILLPARTGRID()

            ElseIf CMBREPORTTYPE.Text = "SELLERWISE" Then

                FILLSELLERNAMEGRID()
                FILLSELLERNAMESUMMGRID()
                FILLSELLERNAMEADVGRID()
                FILLSELLERNAMEPARTGRID()

            ElseIf CMBREPORTTYPE.Text = "CITYWISE" Then

                FILLCITYGRID()
                FILLCITYSUMMGRID()
                FILLCITYADVGRID()
                FILLCITYPARTGRID()

            ElseIf CMBREPORTTYPE.Text = "STATEWISE" Then

                FILLSTATEGRID()
                FILLSTATESUMMGRID()
                FILLSTATEADVGRID()
                FILLSTATEPARTGRID()

            ElseIf CMBREPORTTYPE.Text = "MONTHWISE" Then

                FILLMONTHGRID()
                FILLMONTHSUMMGRID()
                FILLMONTHADVGRID()
                FILLMONTHPARTGRID()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbgroup_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGROUP.Validating
        Try
            If CMBGROUP.Text.Trim <> "" Then
                pcase(CMBGROUP)
                Dim objClsCommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objClsCommon.search("group_name", "", "groupMaster", " and group_name = '" & CMBGROUP.Text.Trim & "' and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBGROUP.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Group not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBGROUP.Text = a
                        Dim objgroupmaster As New GroupMaster
                        objgroupmaster.txtname.Text = CMBGROUP.Text.Trim()
                        objgroupmaster.ShowDialog()
                        dt = objClsCommon.search("group_name", "", "groupMaster", " and group_name = '" & CMBGROUP.Text.Trim & "' and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As DataTable
                            dt1 = CMBGROUP.DataSource
                            If CMBGROUP.DataSource <> Nothing Then
line1:
                                dt1.Rows.Add(CMBGROUP.Text)
                                CMBGROUP.Text = a
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBUYERNAME.Validating
        Try
            NAMEVALIDATE(CMBBUYERNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub CMBSELLERNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSELLERNAME.Validating
        Try
            If CMBSELLERNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBSELLERNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSELLERNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBSELLERNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='SELLERNAME' "
                OBJLEDGER.ShowDialog()
                'If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBSELLERNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbstate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSTATE.Validating
        Try
            If CMBSTATE.Text.Trim <> "" Then
                pcase(CMBSTATE)
                Dim objClsCommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objClsCommon.search("state_name", "", "StateMaster", " and state_name = '" & CMBSTATE.Text.Trim & "' and state_cmpid = " & CmpId & " and state_locationid = " & Locationid & " and state_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBSTATE.Text.Trim
                    Dim tempmsg As Integer = MsgBox("State not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBSTATE.Text = a
                        objyearmaster.savestate(CMBSTATE.Text.Trim, CmpId, Locationid, Userid, YearId, " and state_name = '" & CMBSTATE.Text.Trim & "' and state_cmpid = " & CmpId & " and state_locationid = " & Locationid & " and state_yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = CMBSTATE.DataSource
                        If CMBSTATE.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(CMBSTATE.Text)
                                CMBSTATE.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then itemvalidate(CMBITEMNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCITY As New SelectCity
                OBJCITY.FRMSTRING = "CITY"
                OBJCITY.ShowDialog()
                If OBJCITY.TEMPNAME <> "" Then CMBCITY.Text = OBJCITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSCITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCITY.Validating
        Try
            If CMBCITY.Text.Trim <> "" Then CITYVALIDATE(CMBCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKERNAME_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBUYERNAME.SelectedValueChanged, CMBSELLERNAME.SelectedValueChanged, CMBGROUP.SelectedValueChanged, CMBCITY.SelectedValueChanged, CMBSTATE.SelectedValueChanged, CMBGROUPOFCOMPANY.SelectedValueChanged, CMBITEMNAME.SelectedValueChanged
        Try
            If FILLDONE = False Then Exit Sub
            CMDSEARCH_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            CMBBUYERNAME.Text = ""
            CMBSELLERNAME.Text = ""
            CMBGROUP.Text = ""
            CMBCITY.Text = ""
            CMBGROUPOFCOMPANY.Text = ""
            CMBSTATE.Text = ""
            TXTOVERDUEDAYS.Clear()
            CMBITEMNAME.Text = ""
            CHKDUE.CheckState = CheckState.Unchecked
            CHKLASTYEAR.CheckState = CheckState.Unchecked
            chkdate.CheckState = CheckState.Unchecked

            FILLGRID()
            FILLSUMMGRID()
            FILLADVGRID()
            FILLPARTGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGROUPOFCOMPANY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGROUPOFCOMPANY.Validating
        Try
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then GROUPCOMPANYVALIDATE(CMBGROUPOFCOMPANY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPRINT_Click(sender As Object, e As EventArgs) Handles CMDPRINT.Click
        Try
            If GRIDOUTSTANDING.RowCount = 0 Then Exit Sub
            Dim PRINT As Boolean = True
            Dim WHATSAPP As Boolean = True

            'Dim filePath As String = Application.StartupPath & "\Outstanding_" & CMBNAME.Text.Trim & ".pdf"
            'If MsgBox("Wish to Print?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '    ExportDataGridViewToPdf(GRIDOUTSTANDING, filePath)
            'End If

            If MsgBox("Wish to Print?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Using sfd As New SaveFileDialog()
                    sfd.Filter = "PDF files (*.pdf)|*.pdf"
                    sfd.Title = "Save PDF File"
                    sfd.FileName = "Outstanding_" & CMBBUYERNAME.Text.Trim() & ".pdf"

                    If sfd.ShowDialog() = DialogResult.OK Then
                        ExportDataGridViewToPdf(GRIDOUTSTANDING, sfd.FileName)
                    End If
                End Using

            Else
                If MsgBox(" It Will Take Time .... Wish to Print in Excel?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    ' Dim OBJRPT As New clsReportDesigner("Outstanding Report", System.AppDomain.CurrentDomain.BaseDirectory & "Outstanding Report.xlsx", 2)
                    ExportDataGridViewToExcel(ClientName, CmpId, YearId)
                    ' Exit Sub
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExportDataGridViewToExcel(ClientName As String, CmpId As Integer, YearId As Integer)
        Dim dgv As DataGridView = GRIDOUTSTANDING

        If dgv Is Nothing OrElse dgv.Rows.Count = 0 Then
            MessageBox.Show("No data to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim xlApp As New Excel.Application()
        Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Add()
        Dim xlWorkSheet As Excel.Worksheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)

        Try
            xlApp.DisplayAlerts = False

            ' 👉 Title and Metadata
            xlWorkSheet.Cells(1, 1) = "Receivable Outstanding Report - " & ClientName
            xlWorkSheet.Range("A1").Font.Bold = True
            xlWorkSheet.Range("A1").Font.Size = 16

            xlWorkSheet.Cells(2, 1) = "Company ID: " & CmpId & "    Year ID: " & YearId
            xlWorkSheet.Cells(3, 1) = "Generated on: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm")
            xlWorkSheet.Range("A2:A3").Font.Size = 10

            Dim startRow As Integer = 5
            Dim colIndex As Integer = 0

            ' 👉 Get visible columns
            Dim visibleColumns As New List(Of DataGridViewColumn)
            For Each col As DataGridViewColumn In dgv.Columns
                If col.Visible Then
                    visibleColumns.Add(col)
                End If
            Next

            Dim rowCount As Integer = dgv.Rows.Cast(Of DataGridViewRow)().Count(Function(r) Not r.IsNewRow)
            Dim colCount As Integer = visibleColumns.Count

            ' 👉 Prepare data array
            Dim data(rowCount - 1, colCount - 1) As Object
            Dim rowColors(rowCount - 1) As Color
            Dim isGrandTotalRow(rowCount - 1) As Boolean

            ' 👉 Fill data + colors
            Dim rIndex As Integer = 0
            For Each row As DataGridViewRow In dgv.Rows
                If Not row.IsNewRow Then
                    Dim grandTotal As Boolean = False

                    For c = 0 To colCount - 1
                        Dim cellValue = row.Cells(visibleColumns(c).Index).Value
                        If cellValue IsNot Nothing Then
                            If TypeOf cellValue Is DateTime Then
                                data(rIndex, c) = CType(cellValue, DateTime).ToString("dd/MM/yyyy")
                            Else
                                data(rIndex, c) = cellValue.ToString()
                                If cellValue.ToString().Trim().ToUpper() = "GRANDTOTAL" Then
                                    grandTotal = True
                                End If
                            End If
                        Else
                            data(rIndex, c) = ""
                        End If
                    Next

                    rowColors(rIndex) = row.DefaultCellStyle.BackColor
                    isGrandTotalRow(rIndex) = grandTotal
                    rIndex += 1
                End If
            Next

            ' 👉 Headers
            For c = 0 To colCount - 1
                xlWorkSheet.Cells(startRow, c + 1) = visibleColumns(c).HeaderText
                With xlWorkSheet.Cells(startRow, c + 1)
                    .Font.Bold = True
                    .Interior.Color = RGB(220, 220, 220)
                    .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                    .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                End With
            Next

            ' 👉 Write all data at once
            Dim dataStartCell = xlWorkSheet.Cells(startRow + 1, 1)
            Dim dataEndCell = xlWorkSheet.Cells(startRow + rowCount, colCount)
            Dim writeRange = xlWorkSheet.Range(dataStartCell, dataEndCell)
            writeRange.Value = data
            writeRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous

            ' 👉 Apply formatting row-by-row
            For r = 0 To rowCount - 1
                For c = 0 To colCount - 1
                    Dim cell = xlWorkSheet.Cells(startRow + 1 + r, c + 1)
                    Dim val = data(r, c)

                    If IsNumeric(val) Then
                        cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
                    Else
                        cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
                    End If

                    If isGrandTotalRow(r) Then
                        cell.Font.Bold = True
                        cell.Interior.Color = RGB(250, 240, 230) ' Light beige for total
                    ElseIf rowColors(r) = Color.Yellow Then
                        cell.Interior.Color = RGB(255, 255, 0)
                    ElseIf rowColors(r) = Color.LightGreen Then
                        cell.Interior.Color = RGB(200, 255, 200)
                    End If
                Next
            Next

            ' 👉 Auto-fit
            xlWorkSheet.Columns.AutoFit()

            ' 👉 Show Excel
            xlApp.Visible = True

        Catch ex As Exception
            MessageBox.Show("Export failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            ' Cleanup
            xlWorkBook.Close(False)
            xlApp.Quit()

            Marshal.ReleaseComObject(xlWorkSheet)
            Marshal.ReleaseComObject(xlWorkBook)
            Marshal.ReleaseComObject(xlApp)
        End Try
    End Sub

    Sub TEMPOUTSTANDING()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPOUTSTANDING WHERE YEARID = " & YearId, "", "")

            Dim I As Integer = 1

            If TBFILTER.SelectedIndex = 0 Then
                For Each ROW As DataGridViewRow In GRIDOUTSTANDING.Rows
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(I)
                    If ROW.Cells(GNAME.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GNAME.Index).Value)
                    If ROW.Cells(GINVNO.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GINVNO.Index).Value)
                    Dim TEMP As Date
                    If Not DateTime.TryParse(ROW.Cells(GDATE.Index).Value, TEMP) Then
                        ALPARAVAL.Add(DBNull.Value)
                    Else
                        ALPARAVAL.Add(TEMP)
                    End If

                    If Not DateTime.TryParse(ROW.Cells(GDUEDATE.Index).Value, TEMP) Then
                        ALPARAVAL.Add(DBNull.Value)
                    Else
                        ALPARAVAL.Add(TEMP)
                    End If

                    If ROW.Cells(GBILLAMT.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(GBILLAMT.Index).Value))

                    'WE WILL ADD PARTYCONTACT NO IN LR NO COLUMN
                    If ROW.Cells(GDATE.Index).Value = "CONTACT" Then
                        If ROW.Cells(GDATE.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GDATE.Index).Value)
                    Else
                        If ROW.Cells(GLRNO.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GLRNO.Index).Value)
                    End If

                    If ROW.Cells(GITEMNAME.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GITEMNAME.Index).Value)
                    If ROW.Cells(GRECDAMT.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(GRECDAMT.Index).Value))
                    If ROW.Cells(GBALANCE.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(GBALANCE.Index).Value))
                    'If ROW.Cells(GDAYS.Index).Value = Nothing Then ALPARAVAL.Add(0) Else ALPARAVAL.Add(Val(ROW.Cells(GDAYS.Index).Value))
                    If ROW.Cells(GOVERDUEDAYS.Index).Value = Nothing Then ALPARAVAL.Add(0) Else ALPARAVAL.Add(Val(ROW.Cells(GOVERDUEDAYS.Index).Value))
                    If ROW.Cells(GCMPNAME.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GCMPNAME.Index).Value)
                    ALPARAVAL.Add(CmpId)
                    ALPARAVAL.Add(YearId)
                    If ROW.Cells(GCHARGES.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(GCHARGES.Index).Value))

                    Dim OBJTB As New ClsTrialBalance
                    OBJTB.alParaval = ALPARAVAL
                    Dim INT As Integer = OBJTB.SAVEOUTSTANDING()

                    I += 1
                Next

            Else
                For Each ROW As DataGridViewRow In GRIDSUMM.Rows
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(I)
                    If ROW.Cells(SNAME.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(SNAME.Index).Value)
                    ALPARAVAL.Add("")
                    ALPARAVAL.Add("")
                    ALPARAVAL.Add("")
                    ALPARAVAL.Add("")
                    ALPARAVAL.Add("")
                    ALPARAVAL.Add("")
                    ALPARAVAL.Add("")
                    If ROW.Cells(SBALANCE.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(SBALANCE.Index).Value))
                    '  ALPARAVAL.Add(0)
                    ALPARAVAL.Add("")
                    ALPARAVAL.Add(CmpId)
                    ALPARAVAL.Add(YearId)
                    ALPARAVAL.Add("")

                    Dim OBJTB As New ClsTrialBalance
                    OBJTB.alParaval = ALPARAVAL
                    Dim INT As Integer = OBJTB.SAVEOUTSTANDING()

                    I += 1
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLOSE_Click(sender As Object, e As EventArgs) Handles CMDCLOSE.Click
        Try
            GBFIND.Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDFIND_Click(sender As Object, e As EventArgs) Handles CMDFIND.Click
        Try
            TXTFIND.Clear()
            GBFIND.Visible = True
            If GRIDOUTSTANDING.RowCount > 0 Then GRIDOUTSTANDING.Rows(0).Cells(0).Selected = True
            TXTFIND.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDNEXT_Click(sender As Object, e As EventArgs) Handles CMDNEXT.Click
        Try
            Dim CNO As Integer = GRIDOUTSTANDING.CurrentCell.ColumnIndex + 1
            For ROWNO As Integer = GRIDOUTSTANDING.CurrentRow.Index To GRIDOUTSTANDING.RowCount - 1
                For COLNO As Integer = CNO To GRIDOUTSTANDING.ColumnCount - 1
                    If GRIDOUTSTANDING.Item(COLNO, ROWNO).Value <> Nothing Then
                        If LCase(GRIDOUTSTANDING.Item(COLNO, ROWNO).Value.ToString.Trim) Like LCase(TXTFIND.Text.Trim) & "*" Then
                            GRIDOUTSTANDING.Item(COLNO, ROWNO).Selected = True
                            GoTo LINE1
                        End If
                    End If
                Next
                CNO = 0
            Next
LINE1:
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AgencyOutstandingGridReport_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "MASHOK" Or ClientName = "ABHEE" Then
                GDUEDATE.Visible = False
                CHKHOLDINTCALC.Visible = True
            End If
            GINTAMT.Visible = False

            GCHARGES.Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDOUTSTANDING_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDOUTSTANDING.CellDoubleClick
        Try
            If GRIDOUTSTANDING.RowCount > 0 And e.RowIndex >= 0 Then VIEWFORM(GRIDOUTSTANDING.CurrentRow.Cells(GTYPE.Index).Value, True, GRIDOUTSTANDING.CurrentRow.Cells(GBILL.Index).Value, GRIDOUTSTANDING.CurrentRow.Cells(GREGTYPE.Index).Value)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDWHATSAPP_Click(sender As Object, e As EventArgs) Handles CMDWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub

            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            ' Prepare data for grid
            ' TEMPOUTSTANDING()

            ' Generate the PDF from DataGridView
            Dim filePath As String = Application.StartupPath & "\Outstanding_" & CMBBUYERNAME.Text.Trim & ".pdf"

            ' ✅ Replace "YourDataGridView" with the actual DataGridView object from your form
            ExportDataGridViewToPdfForWP(GRIDOUTSTANDING, filePath)

            ' Prepare WhatsApp sending form
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBBUYERNAME.Text.Trim
            OBJWHATSAPP.PATH.Add(filePath)
            OBJWHATSAPP.FILENAME.Add("Outstanding" & CMBBUYERNAME.Text.Trim & ".pdf")
            OBJWHATSAPP.ShowDialog()

            ' Delete PDF if client is SNCM
            If ClientName = "SNCM" Then
                For Each path As String In OBJWHATSAPP.PATH
                    If File.Exists(path) Then
                        File.Delete(path)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    '****** THIS FUCTION WE ARE CREATED COZ SYSTEM WILL SAVE THIS PDF IN DEBUG FOLDER BY DEFAULT IN THIS CODE SYSTEM NOT ASKING FOR USER TO SAVE WHERE HE WANT ITS SAVING BY DEFAULT IN DEBUG AND SENDING WHATSAPP FROM DEBUG ****** 
    ' DONT DELETE THIS FUCTION         ------- DONE BY CHANDRISH

    Public Sub ExportDataGridViewToPdfForWP(dgv As DataGridView, filePath As String)
        ' 👉 Changed to A3 for bigger page size
        Dim doc As New Document(PageSize.A3.Rotate(), 20, 20, 20, 20)

        Try
            PdfWriter.GetInstance(doc, New FileStream(filePath, FileMode.Create))
            doc.Open()

            ' Load Verdana font
            Dim verdanaBaseFont As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\verdana.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim verdana10 As New iTextSharp.text.Font(verdanaBaseFont, 10)
            Dim verdana10Bold As New iTextSharp.text.Font(verdanaBaseFont, 10, iTextSharp.text.Font.BOLD)
            Dim verdana16Bold As New iTextSharp.text.Font(verdanaBaseFont, 16, iTextSharp.text.Font.BOLD)

            ' Title & Date
            doc.Add(New Paragraph("Receivable Outstanding Report", verdana16Bold))
            doc.Add(New Paragraph("Generated on: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm"), verdana10))
            doc.Add(New Paragraph(" "))

            ' Collect visible columns
            Dim visibleColumns As New List(Of DataGridViewColumn)
            For Each col As DataGridViewColumn In dgv.Columns
                If col.Visible Then visibleColumns.Add(col)
            Next

            Dim table As New PdfPTable(visibleColumns.Count)
            table.WidthPercentage = 100
            table.HeaderRows = 1

            ' 👉 Custom width logic: NAME & BILL AMT are wider
            Dim columnWidths(visibleColumns.Count - 1) As Single
            Dim totalWeight As Single = 0.0F

            For i As Integer = 0 To visibleColumns.Count - 1
                Dim header As String = visibleColumns(i).HeaderText.Trim().ToUpper()
                Select Case header
                    Case "NAME"
                        columnWidths(i) = 2.5F  ' 👈 Increased
                    Case "BILL AMT"
                        columnWidths(i) = 2.0F
                    Case "RECD AMT", "BALANCE", "RUNNING BAL"
                        columnWidths(i) = 1.5F
                    Case Else
                        columnWidths(i) = 1.0F
                End Select
                totalWeight += columnWidths(i)
            Next

            ' Normalize widths to make total = 100%
            For i As Integer = 0 To columnWidths.Length - 1
                columnWidths(i) = columnWidths(i) / totalWeight * 100.0F
            Next

            table.SetWidths(columnWidths)

            ' Headers
            For Each col As DataGridViewColumn In visibleColumns
                Dim headerCell As New PdfPCell(New Phrase(col.HeaderText, verdana10Bold)) With {
                .BackgroundColor = BaseColor.LIGHT_GRAY,
                .HorizontalAlignment = Element.ALIGN_CENTER,
                .VerticalAlignment = Element.ALIGN_MIDDLE,
                .Padding = 5,
                .NoWrap = False
            }
                table.AddCell(headerCell)
            Next

            ' Data rows
            For Each row As DataGridViewRow In dgv.Rows
                If Not row.IsNewRow Then
                    Dim isGrandTotalRow As Boolean = False

                    For Each cell As DataGridViewCell In row.Cells
                        If cell.Value IsNot Nothing AndAlso cell.Value.ToString().Trim().ToUpper() = "GRANDTOTAL" Then
                            isGrandTotalRow = True
                            Exit For
                        End If
                    Next

                    For Each col As DataGridViewColumn In visibleColumns
                        Dim cell As DataGridViewCell = row.Cells(col.Index)
                        Dim value As String = ""

                        If cell.Value IsNot Nothing Then
                            If TypeOf cell.Value Is DateTime Then
                                value = CType(cell.Value, DateTime).ToString("dd/MM/yyyy")
                            Else
                                value = cell.Value.ToString()
                            End If
                        End If

                        Dim pdfCell As PdfPCell = New PdfPCell(New Phrase(value, If(isGrandTotalRow, verdana10Bold, verdana10))) With {
                        .VerticalAlignment = Element.ALIGN_MIDDLE,
                        .Padding = 4
                    }

                        ' Color logic
                        If isGrandTotalRow Then
                            pdfCell.BackgroundColor = New BaseColor(250, 240, 230)
                        ElseIf row.DefaultCellStyle.BackColor = Color.Yellow Then
                            pdfCell.BackgroundColor = BaseColor.YELLOW
                        ElseIf row.DefaultCellStyle.BackColor = Color.LightGreen Then
                            pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY
                        End If

                        ' Wrapping for specific columns
                        Dim colName As String = col.HeaderText.Trim().ToUpper()
                        Select Case colName
                            Case "NAME", "INV NO", "ITEM NAME", "MILL NAME", "PCS/BAGS", "REMARKS", "BROKER", "JOBBERNAME", "TRANSNAME", "GODOWN"
                                pdfCell.NoWrap = False
                            Case Else
                                pdfCell.NoWrap = True
                        End Select

                        ' Alignment
                        If IsNumeric(value) Then
                            pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT
                        Else
                            pdfCell.HorizontalAlignment = Element.ALIGN_LEFT
                        End If

                        table.AddCell(pdfCell)
                    Next
                End If
            Next

            doc.Add(table)

        Catch ex As Exception
            MessageBox.Show("Failed to export PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            doc.Close()
        End Try
    End Sub


    '********************************************************* END ***************************************************************




    '****** THIS FUCTION WE ARE CREATED COZ USER WANT TO SAVE PDF WHERE HE WANTS  ****** 
    Public Sub ExportDataGridViewToPdf(dgv As DataGridView, FileName As String)
        ' 👉 Use the file name passed to the function
        Dim doc As New Document(PageSize.A3.Rotate(), 20, 20, 20, 20)

        Try
            PdfWriter.GetInstance(doc, New FileStream(FileName, FileMode.Create))
            doc.Open()

            ' 👉 Fonts
            Dim verdanaBaseFont As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\verdana.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim verdana10 As New iTextSharp.text.Font(verdanaBaseFont, 10)
            Dim verdana10Bold As New iTextSharp.text.Font(verdanaBaseFont, 10, iTextSharp.text.Font.BOLD)
            Dim verdana16Bold As New iTextSharp.text.Font(verdanaBaseFont, 16, iTextSharp.text.Font.BOLD)

            ' 👉 Title and Date
            doc.Add(New Paragraph("Receivable Outstanding Report", verdana16Bold))
            doc.Add(New Paragraph("Generated on: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm"), verdana10))
            doc.Add(New Paragraph(" "))

            ' 👉 Visible columns
            Dim visibleColumns As New List(Of DataGridViewColumn)
            For Each col As DataGridViewColumn In dgv.Columns
                If col.Visible Then visibleColumns.Add(col)
            Next

            Dim table As New PdfPTable(visibleColumns.Count)
            table.WidthPercentage = 100
            table.HeaderRows = 1

            ' 👉 Column widths
            Dim columnWidths(visibleColumns.Count - 1) As Single
            Dim totalWeight As Single = 0.0F

            For i As Integer = 0 To visibleColumns.Count - 1
                Dim header As String = visibleColumns(i).HeaderText.Trim().ToUpper()
                Select Case header
                    Case "NAME"
                        columnWidths(i) = 2.5F
                    Case "BILL AMT"
                        columnWidths(i) = 2.0F
                    Case "RECD AMT", "BALANCE", "RUNNING BAL"
                        columnWidths(i) = 1.5F
                    Case Else
                        columnWidths(i) = 1.0F
                End Select
                totalWeight += columnWidths(i)
            Next

            ' 👉 Normalize column widths
            For i As Integer = 0 To columnWidths.Length - 1
                columnWidths(i) = columnWidths(i) / totalWeight * 100.0F
            Next
            table.SetWidths(columnWidths)

            ' 👉 Headers
            For Each col As DataGridViewColumn In visibleColumns
                Dim headerCell As New PdfPCell(New Phrase(col.HeaderText, verdana10Bold)) With {
                .BackgroundColor = BaseColor.LIGHT_GRAY,
                .HorizontalAlignment = Element.ALIGN_CENTER,
                .VerticalAlignment = Element.ALIGN_MIDDLE,
                .Padding = 5,
                .NoWrap = False
            }
                table.AddCell(headerCell)
            Next

            ' 👉 Data Rows
            For Each row As DataGridViewRow In dgv.Rows
                If Not row.IsNewRow Then
                    Dim isGrandTotalRow As Boolean = False

                    For Each cell As DataGridViewCell In row.Cells
                        If cell.Value IsNot Nothing AndAlso cell.Value.ToString().Trim().ToUpper() = "GRANDTOTAL" Then
                            isGrandTotalRow = True
                            Exit For
                        End If
                    Next

                    For Each col As DataGridViewColumn In visibleColumns
                        Dim cell As DataGridViewCell = row.Cells(col.Index)
                        Dim value As String = ""

                        If cell.Value IsNot Nothing Then
                            If TypeOf cell.Value Is DateTime Then
                                value = CType(cell.Value, DateTime).ToString("dd/MM/yyyy")
                            Else
                                value = cell.Value.ToString()
                            End If
                        End If

                        Dim pdfCell As New PdfPCell(New Phrase(value, If(isGrandTotalRow, verdana10Bold, verdana10))) With {
                        .VerticalAlignment = Element.ALIGN_MIDDLE,
                        .Padding = 4
                    }

                        ' 👉 Row color logic
                        If isGrandTotalRow Then
                            pdfCell.BackgroundColor = New BaseColor(250, 240, 230)
                        ElseIf row.DefaultCellStyle.BackColor = Color.Yellow Then
                            pdfCell.BackgroundColor = BaseColor.YELLOW
                        ElseIf row.DefaultCellStyle.BackColor = Color.LightGreen Then
                            pdfCell.BackgroundColor = New BaseColor(200, 255, 200)
                        End If

                        ' 👉 Wrapping logic
                        Dim colName As String = col.HeaderText.Trim().ToUpper()
                        Select Case colName
                            Case "NAME", "INV NO", "ITEM NAME", "MILL NAME", "PCS/BAGS", "REMARKS", "BROKER", "JOBBERNAME", "TRANSNAME", "GODOWN"
                                pdfCell.NoWrap = False
                            Case Else
                                pdfCell.NoWrap = True
                        End Select

                        ' 👉 Alignment
                        If IsNumeric(value) Then
                            pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT
                        Else
                            pdfCell.HorizontalAlignment = Element.ALIGN_LEFT
                        End If

                        table.AddCell(pdfCell)
                    Next
                End If
            Next

            ' 👉 Add table to document
            doc.Add(table)

            ' ✅ Success Message
            MessageBox.Show("PDF saved to: " & FileName, "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Failed to export PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            doc.Close()
        End Try
    End Sub

    Private Sub GRIDSUMM_SortCompare(sender As Object, e As DataGridViewSortCompareEventArgs) Handles GRIDSUMM.SortCompare
        Try
            If e.Column.Index = SBALANCE.Index Then
                e.SortResult = CDbl(e.CellValue1).CompareTo(CDbl(e.CellValue2))
            Else
                e.SortResult = CStr(e.CellValue1).CompareTo(CStr(e.CellValue2))
            End If
            e.Handled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDOUTSTANDING_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles GRIDOUTSTANDING.CellFormatting
        Try
            If Convert.ToBoolean(GRIDOUTSTANDING.Rows(e.RowIndex).Cells(GHOLDINTCALC.Index).Value) = True Then GRIDOUTSTANDING.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.FromArgb(213, 228, 248)
            If GRIDOUTSTANDING.Rows(e.RowIndex).Cells(GCOMPLAINT.Index).Value <> "" Then GRIDOUTSTANDING.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Orange
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class