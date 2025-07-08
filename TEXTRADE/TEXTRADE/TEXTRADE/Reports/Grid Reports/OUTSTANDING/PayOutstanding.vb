﻿
Imports BL
Imports System.Windows.Forms

Public Class PayOutstanding

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

    Private Sub RecOutstanding_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            GRIDOUTSTANDING.RowCount = 0
            GCMPNAME.Visible = False

            Dim TEMPNAME As String = ""
            Dim GTOTAL, RECDTOTAL, BALANCE, GRANDTOTAL, RECDGRANDTOTAL, BALANCEGRANDTOTAL, GINTTOTAL, PARTYINTTOTAL As Decimal
            Dim WHERECLAUSE As String = " "


            If CMBNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBNAME.Text.Trim & "'"
            If CMBBROKERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND AGENT = '" & CMBBROKERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBREGISTER.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND REGTYPE = '" & CMBREGISTER.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"

            If chkdate.CheckState = CheckState.Checked Then
                WHERECLAUSE = WHERECLAUSE & " AND DATE >= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
            End If

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
            Dim SRNO As Integer = 0
            Dim BILLINTEREST As Double = 0.0

            DT = OBJCMN.Execute_Any_String(" SELECT *, CMPMASTER.CMP_NAME AS CMPNAME FROM OUTSTANDINGPAY INNER JOIN CMPMASTER ON CMPID = CMP_ID  WHERE SECONDARY = 'Sundry CREDITORS' AND ROUND(BALANCE,2) <> 0 AND YEARID = " & YearId & WHERECLAUSE & " ORDER BY NAME, DATE, TYPE, BILL", "", "")
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
                    DAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date)
                    TOTALDAYS = DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DATE")).Date, Mydate.Date)
                    If Val(TXTPERCENT.Text.Trim) > 0 And Val(TXTDAYS.Text.Trim) > 0 Then BILLINTEREST = Format((Val(TXTPERCENT.Text.Trim) / Val(TXTDAYS.Text.Trim) / 100) * Val(DAYS) * Val(ROW("BALANCE")), "0")

                    SRNO += 1
                    RUNNINGBAL += Val(ROW("BALANCE"))
                    GRIDOUTSTANDING.Rows.Add(ROW("AGENT"), ROW("PRINTINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), ROW("ITEMNAME"), ROW("MILLNAME"), Val(ROW("TOTALPCS")), Format(Val(ROW("TOTALMTRS")), "0.00"), Format(Val(ROW("RATE")), "0.00"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), Format(Val(RUNNINGBAL), "0.00"), Val(SRNO), Val(ROW("CRDAYS")), Val(DAYS), Val(TOTALDAYS), Format(Val(ROW("CHARGES")), "0.00"), ROW("CMPNAME"), ROW("TYPE"), Val(ROW("BILL")), ROW("REGTYPE"), Val(BILLINTEREST))
                    'GRIDOUTSTANDING.Rows.Add(ROW("AGENT"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date))
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


            If CMBNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBNAME.Text.Trim & "'"
            If CMBBROKERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND AGENT = '" & CMBBROKERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBREGISTER.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND REGTYPE = '" & CMBREGISTER.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
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

            DT = OBJCMN.Execute_Any_String(" SELECT NAME,SUM(BALANCE) AS BALANCE FROM OUTSTANDINGPAY WHERE SECONDARY = 'Sundry CREDITORS' AND YEARID = " & YearId & WHERECLAUSE & " GROUP BY NAME HAVING ROUND(SUM(BALANCE),2) <> 0", "", "")
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


            If CMBNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBNAME.Text.Trim & "'"
            If CMBBROKERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND AGENT = '" & CMBBROKERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBREGISTER.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND REGTYPE = '" & CMBREGISTER.Text.Trim & "'"
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


            DT = OBJCMN.Execute_Any_String(" SELECT BILLINITIALS,DATE,NAME, AGENT,RECDAMT, MOBILENO, PHONENO FROM OUTSTANDINGPAY WHERE SECONDARY = 'Sundry CREDITORS' AND TYPE='PAYMENT' AND YEARID = " & YearId & WHERECLAUSE & " ORDER BY AGENT, DATE, TYPE", "", "")
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
                    GRIDADV.Rows.Add(ROW("AGENT"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Val(ROW("RECDAMT")), "0.00"))
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


            If CMBNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND NAME = '" & CMBNAME.Text.Trim & "'"
            If CMBBROKERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND AGENT = '" & CMBBROKERNAME.Text.Trim & "'"
            If CMBGROUP.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPNAME = '" & CMBGROUP.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITY = '" & CMBCITY.Text.Trim & "'"
            If CMBGROUPOFCOMPANY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GROUPOFCOMPANIES = '" & CMBGROUPOFCOMPANY.Text.Trim & "'"
            If CMBSTATE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND STATE = '" & CMBSTATE.Text.Trim & "'"
            If CMBREGISTER.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND REGTYPE = '" & CMBREGISTER.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
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



            DT = OBJCMN.Execute_Any_String(" SELECT * FROM OUTSTANDINGPAY WHERE SECONDARY = 'Sundry CREDITORS' AND RECDAMT > 0 AND BALANCE > 0 AND YEARID = " & YearId & WHERECLAUSE & " ORDER BY NAME, DATE, TYPE, BILL", "", "")
            If DT.Rows.Count > 0 Then
                TEMPNAME = ""
                GTOTAL = 0
                RECDTOTAL = 0
                BALANCE = 0
                GRANDTOTAL = 0
                RECDGRANDTOTAL = 0
                BALANCEGRANDTOTAL = 0

                For Each ROW As DataRow In DT.Rows
                    If TEMPNAME <> ROW("NAME") Then
                        TEMPNAME = ROW("NAME")
                        If GRIDPART.RowCount > 0 Then ADDPARTPAIDTOTALROW(GTOTAL, RECDTOTAL, BALANCE)
                        GTOTAL = 0
                        RECDTOTAL = 0
                        BALANCE = 0
                        ADDPARTNAMEROW(ROW("NAME"), ROW("MOBILENO"), ROW("PHONENO"))
                    End If
                    GRIDPART.Rows.Add(ROW("AGENT"), ROW("BILLINITIALS"), Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yy"), Format(Convert.ToDateTime(ROW("DUEDATE")).Date, "dd/MM/yy"), Format(Val(ROW("GRANDTOTAL")), "0.00"), ROW("LRNO"), Format(Val(ROW("RECDAMT")), "0.00"), Format(Val(ROW("BALANCE")), "0.00"), DateDiff(DateInterval.Day, Convert.ToDateTime(ROW("DUEDATE")).Date, Mydate.Date))
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
            GRIDOUTSTANDING.Rows.Add(NAME, "", "CONTACT NO : ", MOBILENO, PHONENO, "", "", CITYNAME)
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
            GRIDOUTSTANDING.Rows(GRIDOUTSTANDING.RowCount - 1).DefaultCellStyle.Font = New Font("Calibri", 11, FontStyle.Bold)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDADVNAMEROW(ByVal NAME, ByVal MOBILENO, ByVal PHONENO)
        Try
            'PRINT NAME 
            GRIDADV.Rows.Add(NAME, "", "CONTACT NO : ", MOBILENO, PHONENO)
            GRIDADV.Rows(GRIDADV.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDPARTNAMEROW(ByVal NAME, ByVal MOBILENO, ByVal PHONENO)
        Try
            'PRINT NAME 
            GRIDPART.Rows.Add(NAME, "", "CONTACT NO : ", MOBILENO, PHONENO)
            GRIDPART.Rows(GRIDPART.RowCount - 1).DefaultCellStyle.BackColor = Color.FromArgb(213, 228, 248)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDPARTYTOTALROW(ByVal GTOTAL As Decimal, ByVal RECDTOTAL As Decimal, ByVal BALANCE As Decimal, PARTYINTTOTAL As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Font(GRIDOUTSTANDING.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDOUTSTANDING.Rows.Add("SUBTOTAL", "", "", "", "", "", "", "", "", Format(Val(GTOTAL), "0.00"), "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "", "", "", "", "", "", "", "", "", "", PARTYINTTOTAL)
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
            STYLE.Font = New Font(GRIDPART.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDPART.Rows.Add("SUBTOTAL", "", "", "", Format(Val(GTOTAL), "0.00"), "", "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "")
            'GRIDPART.Rows.Add("SUBTOTAL", "", "", "", Format(Val(GTOTAL), "0.00"), "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "")
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
            STYLE.Font = New Font(GRIDSUMM.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Yellow
            GRIDSUMM.Rows.Add("TOTAL", Format(Val(BALANCE), "0.00"), "")
            GRIDSUMM.Rows(GRIDSUMM.RowCount - 1).DefaultCellStyle = STYLE
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDADVTOTALROW(ByVal BALANCE As Decimal)
        Try
            'PRINT NAME 
            Dim STYLE As New DataGridViewCellStyle
            STYLE.Font = New Font(GRIDADV.Font, FontStyle.Bold)
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
            STYLE.Font = New Font(GRIDSUMM.Font, FontStyle.Bold)
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
            STYLE.Font = New Font(GRIDOUTSTANDING.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Orange
            GRIDOUTSTANDING.Rows.Add("GRANDTOTAL", "", "", "", "", "", "", "", "", Format(Val(GTOTAL), "0.00"), "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "", "", "", "", "", "", "", "", "", "", Val(INTTOTAL))
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
            STYLE.Font = New Font(GRIDPART.Font, FontStyle.Bold)
            STYLE.BackColor = Color.Orange
            GRIDPART.Rows.Add("GRANDTOTAL", "", "", "", Format(Val(GTOTAL), "0.00"), "", Format(Val(RECDTOTAL), "0.00"), Format(Val(BALANCE), "0.00"), "")
            GRIDPART.Rows(GRIDPART.RowCount - 1).DefaultCellStyle = STYLE
            GRIDPART.Rows.Add()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RecOutstanding_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'DONE BY GULKIT, COZ IF FILLDONE IS NOT MENTIONED HERE THEN EVERYTIME IT GOES TO SELECTEDINDEXCHANGE EVENT ON FILLING THE COMBO
            FILLDONE = True
            CMBREPORTTYPE.SelectedIndex = 0

            If PARTYNAME <> "" Then CMBNAME.Text = PARTYNAME

            dtfrom.Value = AccFrom.Date
            dtto.Value = Now.Date

            FILLGRID()
            FILLSUMMGRID()
            FILLADVGRID()
            FILLPARTGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDSEARCH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSEARCH.Click
        If Val(TXTPERCENT.Text.Trim) > 0 And Val(TXTDAYS.Text.Trim) > 0 Then GINTAMT.Visible = True Else GINTAMT.Visible = False
        FILLGRID()
        FILLSUMMGRID()
        FILLADVGRID()
        FILLPARTGRID()
    End Sub

    Private Sub CMBGROUP_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGROUP.Enter
        Try
            If CMBGROUP.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("group_name", "", "GroupMaster", " and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId)
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

    Private Sub cmbgroup_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGROUP.Validated
        Try
            If CMBGROUP.Text.Trim <> "" Then
                'CHECK GROUP_SECONDARY
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" GROUP_SECONDARY ", "", " GROUPMASTER ", " AND GROUP_NAME = '" & CMBGROUP.Text.Trim & "' AND GROUP_CMPID = " & CmpId & " AND GROUP_LOCATIONID = " & Locationid & " AND GROUP_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBREGISTER.Text = ""
                    If DT.Rows(0).Item(0) = "Sundry Creditors" Then
                        fillregister(CMBREGISTER, " and register_type = 'PURCHASE'")
                    ElseIf DT.Rows(0).Item(0) = "Sundry Debtors" Then
                        fillregister(CMBREGISTER, " and register_type = 'SALE'")
                    Else
                        CMBREGISTER.DataSource = Nothing
                    End If
                End If
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

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry CREDITORS'", "Sundry CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBBROKERNAME.Enter
        If CMBBROKERNAME.Text.Trim = "" Then fillagentledger(CMBBROKERNAME, False, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
    End Sub

    Private Sub CMBAGENT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBROKERNAME.Validating
        Try
            If CMBBROKERNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBBROKERNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBROKERNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT' "
                OBJLEDGER.ShowDialog()
                'If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBBROKERNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBREGISTER.Enter
        Try
            If CMBREGISTER.Text.Trim = "" Then fillregister(CMBREGISTER, " and register_type = 'PURCHASE'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.SEARCH(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'PURCHASE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                CMBREGISTER.Text = dt.Rows(0).Item(0).ToString
            End If


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbstate_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSTATE.Enter
        Try
            If CMBSTATE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("state_name", "", "StateMaster", " and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "state_name"
                    CMBSTATE.DataSource = dt
                    CMBSTATE.DisplayMember = "state_name"
                    CMBSTATE.Text = ""
                End If
                CMBSTATE.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Private Sub CMBTOCITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBCITY.Enter
        Try
            If CMBCITY.Text.Trim = "" Then fillCITY(CMBCITY, False)
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

    Private Sub CMDPRINT_Click(sender As Object, e As EventArgs) Handles CMDPRINT.Click
        'Try
        '    If GRIDOUTSTANDING.RowCount = 0 Then Exit Sub


        '    If MsgBox("Wish to Print?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

        '    Dim OBJCMN As New ClsCommon
        '    Dim DT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPOUTSTANDING WHERE YEARID = " & YearId, "", "")

        '    Dim I As Integer = 1

        '    For Each ROW As DataGridViewRow In GRIDOUTSTANDING.Rows
        '        Dim ALPARAVAL As New ArrayList
        '        ALPARAVAL.Add(I)
        '        If ROW.Cells(GNAME.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GNAME.Index).Value)
        '        If ROW.Cells(GINVNO.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GINVNO.Index).Value)

        '        Dim TEMP As Date
        '        If Not DateTime.TryParse(ROW.Cells(GDATE.Index).Value, TEMP) Then
        '            ALPARAVAL.Add(DBNull.Value)
        '        Else
        '            ALPARAVAL.Add(TEMP)
        '        End If

        '        If Not DateTime.TryParse(ROW.Cells(GDUEDATE.Index).Value, TEMP) Then
        '            ALPARAVAL.Add(DBNull.Value)
        '        Else
        '            ALPARAVAL.Add(TEMP)
        '        End If

        '        If ROW.Cells(GBILLAMT.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(GBILLAMT.Index).Value))

        '        'WE WILL ADD PARTYCONTACT NO IN LR NO COLUMN
        '        If ROW.Cells(GINVNO.Index).Value = "CONTACT NO : " Then
        '            If ROW.Cells(GDATE.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GDATE.Index).Value)
        '        Else
        '            If ROW.Cells(GLRNO.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GLRNO.Index).Value)
        '        End If

        '        ALPARAVAL.Add("") 'ITEMNAME
        '        If ROW.Cells(GRECDAMT.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(GRECDAMT.Index).Value))
        '        If ROW.Cells(GBALANCE.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(GBALANCE.Index).Value))
        '        If ROW.Cells(GDAYS.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(GDAYS.Index).Value))
        '        ALPARAVAL.Add("")   'CmpName
        '        ALPARAVAL.Add(CmpId)
        '        ALPARAVAL.Add(YearId)
        '        ALPARAVAL.Add("") 'CHARGES

        '        Dim OBJTB As New ClsTrialBalance
        '        OBJTB.alParaval = ALPARAVAL
        '        Dim INT As Integer = OBJTB.SAVEOUTSTANDING()

        '        I += 1
        '    Next


        '    If MsgBox("Wish to Print in Excel?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '        Dim OBJRPT As New clsReportDesigner("Outstanding Report", System.AppDomain.CurrentDomain.BaseDirectory & "Outstanding Report.xlsx", 2)
        '        OBJRPT.OUTSTANDIGEXCEL(ClientName, CmpId, YearId)
        '        Exit Sub
        '    End If

        '    Dim OBJPL As New PLDesign
        '    OBJPL.frmstring = "OUTSTANDING"
        '    OBJPL.MdiParent = MDIMain
        '    OBJPL.strsearch = "{TEMPOUTSTANDING.YEARID} = " & YearId
        '    OBJPL.Show()

        'Catch ex As Exception
        '    Throw ex
        'End Try

        Try
            If GRIDOUTSTANDING.RowCount = 0 Then Exit Sub
            Dim PRINT As Boolean = True
            Dim WHATSAPP As Boolean = True

            If MsgBox("Wish to Print?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            TEMPOUTSTANDING()


            If MsgBox("Wish to Print in Excel?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim OBJRPT As New clsReportDesigner("Outstanding Report", System.AppDomain.CurrentDomain.BaseDirectory & "Outstanding Report.xlsx", 2)
                OBJRPT.OUTSTANDIGEXCEL(ClientName, CmpId, YearId)
                Exit Sub
            End If

            Dim OBJPL As New PLDesign
            OBJPL.frmstring = "OUTSTANDING"
            OBJPL.MdiParent = MDIMain
            OBJPL.strsearch = "{TEMPOUTSTANDING.YEARID} = " & YearId
            OBJPL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TEMPOUTSTANDING()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPOUTSTANDING WHERE YEARID = " & YearId, "", "")

            Dim I As Integer = 1

            If TabControl1.SelectedIndex = 0 Then
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
                    If ROW.Cells(GDAYS.Index).Value = Nothing Then ALPARAVAL.Add(0) Else ALPARAVAL.Add(Val(ROW.Cells(GDAYS.Index).Value))
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
                    ALPARAVAL.Add(0)
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

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            CMBNAME.Text = ""
            CMBBROKERNAME.Text = ""
            CMBGROUP.Text = ""
            CMBCITY.Text = ""
            CMBSTATE.Text = ""
            CMBREGISTER.Text = ""
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

    Sub FILLCMB()
        Try
            FILLNAME(CMBNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            FILLNAME(CMBBROKERNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'AGENT'")

            Dim OBJCMN As New ClsCommon
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
            fillregister(CMBREGISTER, " and (register_type = 'PURCHASE' or register_type = 'DEBITNOTE')")
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

    Private Sub CMDWHATSAPP_Click(sender As Object, e As EventArgs) Handles CMDWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            TEMPOUTSTANDING()
            Dim WHATSAPPNO As String = ""
            Dim OBJPL As New PLDesign
            OBJPL.frmstring = "OUTSTANDING"
            OBJPL.MdiParent = MDIMain
            OBJPL.strsearch = "{TEMPOUTSTANDING.YEARID} = " & YearId
            OBJPL.DIRECTPRINT = True
            OBJPL.PARTYNAME = CMBNAME.Text.Trim
            OBJPL.Show()
            OBJPL.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\Outstanding_" & CMBNAME.Text.Trim & ".pdf")
            OBJWHATSAPP.FILENAME.Add("Outstanding" & PARTYNAME & ".pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PayOutstanding_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            GINTAMT.Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class