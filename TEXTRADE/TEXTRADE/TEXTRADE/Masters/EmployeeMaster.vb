
Imports BL
Imports System.IO
Imports System.ComponentModel

Public Class EmployeeMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDEDDOUBLECLICK, GRIDEARDOUBLECLICK As Boolean
    Dim TEMPDEDROW, TEMPEARROW As Integer
    Public EDIT As Boolean
    Public TEMPEMPNAME As String
    Dim TEMPEMPID As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
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

    Private Sub EmployeeMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If errorvalid() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.Alt = True And e.KeyCode = Keys.D1 Then
            TabControl1.SelectedIndex = 0
        ElseIf e.Alt = True And e.KeyCode = Keys.D2 Then
            TabControl1.SelectedIndex = 1
        End If
    End Sub

    Sub FILLEMPNAME()
        Try
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable = OBJCMN.search("EMP_NAME", "", " EMPLOYEEMASTER ", " and EMP_yearid = " & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "EMP_NAME"
                CMBEMPNAME.DataSource = dt
                CMBEMPNAME.DisplayMember = "EMP_NAME"
                CMBEMPNAME.Text = ""
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLCMB()

        Dim OBJCMN As New ClsCommon
        Dim DT As New DataTable

        DT = OBJCMN.search("DESIGNATION_name", "", "DESIGNATIONMASTER", " and DESIGNATION_Yearid =" & YearId)
        If DT.Rows.Count > 0 Then
            DT.DefaultView.Sort = "DESIGNATION_name"
            CMBDESIGNATION.DataSource = DT
            CMBDESIGNATION.DisplayMember = "DESIGNATION_name"
            CMBDESIGNATION.Text = ""
        End If

        DT = OBJCMN.search("DEPARTMENT_name", "", "DEPARTMENTMASTER", " and DEPARTMENT_Yearid =" & YearId)
        If DT.Rows.Count > 0 Then
            DT.DefaultView.Sort = "DEPARTMENT_name"
            CMBDEPARTMENT.DataSource = DT
            CMBDEPARTMENT.DisplayMember = "DEPARTMENT_name"
            CMBDEPARTMENT.Text = ""
        End If


        DT = OBJCMN.search("area_name", "", "AreaMaster", " and area_Yearid =" & YearId)
        If DT.Rows.Count > 0 Then
            DT.DefaultView.Sort = "area_name"
            CMBAREA.DataSource = DT
            CMBAREA.DisplayMember = "area_name"
            CMBAREA.Text = ""
        End If

        DT = OBJCMN.search("city_name", "", "CityMaster", " and city_yearid = " & YearId)
        If DT.Rows.Count > 0 Then
            DT.DefaultView.Sort = "City_name"
            CMBCITY.DataSource = DT
            CMBCITY.DisplayMember = "city_name"
            CMBCITY.Text = ""
        End If

        DT = OBJCMN.search("country_name", "", "CountryMaster", " and country_Yearid = " & YearId)
        If DT.Rows.Count > 0 Then
            DT.DefaultView.Sort = "country_name"
            CMBCOUNTRY.DataSource = DT
            CMBCOUNTRY.DisplayMember = "country_name"
            CMBCOUNTRY.Text = ""
        End If

        DT = OBJCMN.search("state_name", "", "StateMaster", " and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_yearid = " & YearId)
        If DT.Rows.Count > 0 Then
            DT.DefaultView.Sort = "state_name"
            CMBSTATE.DataSource = DT
            CMBSTATE.DisplayMember = "state_name"
            CMBSTATE.Text = ""
        End If

        If CMBLEDGERNAME.Text.Trim = "" Then fillledger(CMBLEDGERNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors') ")
        If CMBLOANLEDGERNAME.Text.Trim = "" Then fillledger(CMBLOANLEDGERNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors') ")
        If CMBDEDUCTION.Text.Trim = "" Then fillledger(CMBDEDUCTION, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income'  OR GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances') ")
        If CMBEARNINGS.Text.Trim = "" Then fillledger(CMBEARNINGS, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income'  OR GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances') ")

        If CMBPARTYBANK.Text = "" Then FILLBANK(CMBPARTYBANK)
        If CMBBANKCITY.Text.Trim = "" Then fillCITY(CMBBANKCITY, EDIT)

    End Sub

    Private Sub EmployeeMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'EMPLOYEE MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)



            FILLCMB()
            FILLEMPNAME()
            CLEAR()

            If EDIT = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJEMP As New ClsEmployeeMaster

                OBJEMP.alParaval.Add(TEMPEMPNAME)
                OBJEMP.alParaval.Add(YearId)

                Dim dttable As DataTable = OBJEMP.GETEMPLOYEE()

                If dttable.Rows.Count > 0 Then
                    TEMPEMPID = Val(dttable.Rows(0).Item("EMPID"))
                    CMBEMPNAME.Text = dttable.Rows(0).Item("EMPNAME").ToString
                    CMBLEDGERNAME.Text = dttable.Rows(0).Item("LEDGERNAME").ToString
                    CMBLOANLEDGERNAME.Text = dttable.Rows(0).Item("LOANLEDGERNAME").ToString
                    TXTSALARY.Text = Val(dttable.Rows(0).Item("SALARY"))

                    CMBDEPARTMENT.Text = dttable.Rows(0).Item("DEPARTMENT").ToString
                    CMBDESIGNATION.Text = dttable.Rows(0).Item("DESIGNATION").ToString

                    CMBAREA.Text = dttable.Rows(0).Item("AREA").ToString
                    CMBCITY.Text = dttable.Rows(0).Item("CITY").ToString
                    TXTPINCODE.Text = dttable.Rows(0).Item("PINCODE").ToString
                    CMBSTATE.Text = dttable.Rows(0).Item("STATE").ToString
                    CMBCOUNTRY.Text = dttable.Rows(0).Item("COUNTRY").ToString

                    TXTRESINO.Text = dttable.Rows(0).Item("RESINO").ToString
                    TXTWHATSAPPNO.Text = dttable.Rows(0).Item("WHATSAPPNO").ToString
                    TXTALTNO.Text = dttable.Rows(0).Item("ALTNO").ToString
                    TXTMOBILENO.Text = dttable.Rows(0).Item("MOBILENO").ToString

                    CMBEMAIL.Text = dttable.Rows(0).Item("EMAIL").ToString

                    TXTPANNO.Text = dttable.Rows(0).Item("PANNO").ToString
                    TXTPFNO.Text = dttable.Rows(0).Item("PFNO").ToString

                    CMBPARTYBANK.Text = dttable.Rows(0).Item("PARTYBANK").ToString
                    TXTACCTYPE.Text = dttable.Rows(0).Item("ACTYPE").ToString
                    TXTACCNO.Text = dttable.Rows(0).Item("ACCNO").ToString
                    TXTIFSC.Text = dttable.Rows(0).Item("IFSCCODE").ToString
                    TXTBRANCH.Text = dttable.Rows(0).Item("BRANCH").ToString
                    CMBBANKCITY.Text = dttable.Rows(0).Item("BANKCITY").ToString
                    TXTUPI.Text = dttable.Rows(0).Item("UPI").ToString

                    TXTADDRESS.Text = dttable.Rows(0).Item("ADDRESS").ToString
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString

                    If IsDBNull(dttable.Rows(0).Item("PHOTO")) = False Then
                        PBIMG.Image = Image.FromStream(New IO.MemoryStream(DirectCast(dttable.Rows(0).Item("PHOTO"), Byte())))
                    Else
                        PBIMG.Image = Nothing
                    End If

                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" EMPLOYEEMASTER_DEDUCTION.EMP_SRNO AS SRNO, LEDGERS.Acc_cmpname AS DEDUCTION, EMPLOYEEMASTER_DEDUCTION.EMP_AMT AS DEDAMT ", "", " EMPLOYEEMASTER_DEDUCTION INNER JOIN LEDGERS ON EMPLOYEEMASTER_DEDUCTION.EMP_DEDID = LEDGERS.Acc_id ", " AND EMP_ID = " & TEMPEMPID & " AND EMP_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        For Each DTDED As DataRow In DT.Rows
                            GRIDDED.Rows.Add(DTDED("SRNO"), DTDED("DEDUCTION"), Format(Val(DTDED("DEDAMT")), "0.00"))
                        Next
                    End If

                    DT = OBJCMN.search(" EMPLOYEEMASTER_EARNINGS.EMP_SRNO AS SRNO, LEDGERS.Acc_cmpname AS EARNINGS, EMPLOYEEMASTER_EARNINGS.EMP_AMT AS EARAMT ", "", " EMPLOYEEMASTER_EARNINGS INNER JOIN LEDGERS ON EMPLOYEEMASTER_EARNINGS.EMP_EARID = LEDGERS.Acc_id ", " AND EMP_ID = " & TEMPEMPID & " AND EMP_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        For Each DTEAR As DataRow In DT.Rows
                            GRIDEAR.Rows.Add(DTEAR("SRNO"), DTEAR("EARNINGS"), Format(Val(DTEAR("EARAMT")), "0.00"))
                        Next
                    End If

                    TOTAL()
                End If

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            TXTTOTALEARNINGS.Clear()
            TXTTOTALDEDUCTION.Clear()
            TXTSALARY.Clear()

            For Each ROW As DataGridViewRow In GRIDEAR.Rows
                TXTTOTALEARNINGS.Text = Format(Val(TXTTOTALEARNINGS.Text.Trim) + Val(ROW.Cells(GEARAMT.Index).Value), "0.00")
            Next

            For Each ROW As DataGridViewRow In GRIDDED.Rows
                TXTTOTALDEDUCTION.Text = Format(Val(TXTTOTALDEDUCTION.Text.Trim) + Val(ROW.Cells(GDEDAMT.Index).Value), "0.00")
            Next

            TXTSALARY.Text = Format(Val(TXTTOTALEARNINGS.Text.Trim) - Val(TXTTOTALDEDUCTION.Text.Trim), "0.00")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList
            alParaval.Add(CMBEMPNAME.Text.Trim)
            alParaval.Add(CMBLEDGERNAME.Text.Trim)
            alParaval.Add(CMBLOANLEDGERNAME.Text.Trim)
            alParaval.Add(Val(TXTSALARY.Text.Trim))

            alParaval.Add(CMBDEPARTMENT.Text.Trim)
            alParaval.Add(CMBDESIGNATION.Text.Trim)

            alParaval.Add(CMBAREA.Text.Trim)
            alParaval.Add(CMBCITY.Text.Trim)
            alParaval.Add(TXTPINCODE.Text.Trim)
            alParaval.Add(CMBSTATE.Text.Trim)
            alParaval.Add(CMBCOUNTRY.Text.Trim)
            alParaval.Add(TXTRESINO.Text.Trim)
            alParaval.Add(TXTWHATSAPPNO.Text.Trim)
            alParaval.Add(TXTALTNO.Text.Trim)
            alParaval.Add(TXTMOBILENO.Text.Trim)
            alParaval.Add(CMBEMAIL.Text.Trim)
            alParaval.Add(TXTPANNO.Text.Trim)
            alParaval.Add(TXTPFNO.Text.Trim)

            alParaval.Add(CMBPARTYBANK.Text.Trim)
            alParaval.Add(TXTACCTYPE.Text.Trim)
            alParaval.Add(TXTACCNO.Text.Trim)
            alParaval.Add(TXTIFSC.Text.Trim)
            alParaval.Add(TXTBRANCH.Text.Trim)
            alParaval.Add(CMBBANKCITY.Text.Trim)
            alParaval.Add(TXTUPI.Text.Trim)

            alParaval.Add(TXTADDRESS.Text.Trim)
            alParaval.Add(TXTREMARKS.Text.Trim)

            If PBIMG.Image IsNot Nothing Then
                Dim MS As New IO.MemoryStream
                PBIMG.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                alParaval.Add(MS.ToArray)
            Else
                alParaval.Add(DBNull.Value)
            End If


            Dim EARGRIDSRNO As String = ""
            Dim EARNINGS As String = ""
            Dim EARAMT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDEAR.Rows
                If row.Cells(GEARSRNO.Index).Value <> Nothing Then
                    If EARGRIDSRNO = "" Then
                        EARGRIDSRNO = Val(row.Cells(GEARSRNO.Index).Value)
                        EARNINGS = row.Cells(GEARNINGS.Index).Value
                        EARAMT = Val(row.Cells(GEARAMT.Index).Value)
                    Else
                        EARGRIDSRNO = EARGRIDSRNO & "|" & Val(row.Cells(GEARSRNO.Index).Value)
                        EARNINGS = EARNINGS & "|" & row.Cells(GEARNINGS.Index).Value
                        EARAMT = EARAMT & "|" & Val(row.Cells(GEARAMT.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(EARGRIDSRNO)
            alParaval.Add(EARNINGS)
            alParaval.Add(EARAMT)




            Dim DEDGRIDSRNO As String = ""
            Dim DEDUCTION As String = ""
            Dim DEDAMT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDDED.Rows
                If row.Cells(GDEDSRNO.Index).Value <> Nothing Then
                    If DEDGRIDSRNO = "" Then
                        DEDGRIDSRNO = Val(row.Cells(GDEDSRNO.Index).Value)
                        DEDUCTION = row.Cells(GDEDUCTION.Index).Value
                        DEDAMT = Val(row.Cells(GDEDAMT.Index).Value)
                    Else
                        DEDGRIDSRNO = DEDGRIDSRNO & "|" & Val(row.Cells(GDEDSRNO.Index).Value)
                        DEDUCTION = DEDUCTION & "|" & row.Cells(GDEDUCTION.Index).Value
                        DEDAMT = DEDAMT & "|" & Val(row.Cells(GDEDAMT.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(DEDGRIDSRNO)
            alParaval.Add(DEDUCTION)
            alParaval.Add(DEDAMT)


            alParaval.Add(Val(TXTTOTALEARNINGS.Text.Trim))
            alParaval.Add(Val(TXTTOTALDEDUCTION.Text.Trim))


            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim OBJEMP As New ClsEmployeeMaster
            OBJEMP.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = OBJEMP.SAVE()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPEMPID)
                IntResult = OBJEMP.UPDATE()
                EDIT = False
                MsgBox("Details Updated")
            End If

            CLEAR()
            CMBEMPNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub CLEAR()

        CMBEMPNAME.Text = ""
        CMBLEDGERNAME.Text = ""
        CMBLOANLEDGERNAME.Text = ""
        TXTSALARY.Clear()

        CMBDEPARTMENT.Text = ""
        CMBDESIGNATION.Text = ""
        CMBAREA.Text = ""
        CMBCITY.Text = ""
        TXTPINCODE.Clear()
        CMBSTATE.Text = ""
        CMBCOUNTRY.Text = ""

        TXTRESINO.Clear()
        TXTWHATSAPPNO.Clear()
        TXTALTNO.Clear()
        TXTMOBILENO.Clear()
        CMBEMAIL.Text = ""
        TXTPANNO.Clear()
        TXTPFNO.Clear()

        CMBPARTYBANK.Text = ""
        TXTACCTYPE.Clear()
        TXTACCNO.Clear()
        TXTIFSC.Clear()
        TXTBRANCH.Clear()
        CMBBANKCITY.Text = ""
        TXTUPI.Clear()

        TXTADDRESS.Clear()
        TXTREMARKS.Clear()

        TXTDEDSRNO.Clear()
        CMBDEDUCTION.Text = ""
        TXTDEDAMT.Clear()
        GRIDDED.RowCount = 0
        TXTTOTALDEDUCTION.Clear()

        TXTEARSRNO.Clear()
        CMBEARNINGS.Text = ""
        TXTEARAMT.Clear()
        GRIDEAR.RowCount = 0
        TXTTOTALEARNINGS.Clear()

        TXTFILENAME.Clear()
        TXTIMGPATH.Clear()
        TXTOURLOCATION.Clear()
        PBIMG.ImageLocation = ""

        GRIDDEDDOUBLECLICK = False
        GRIDEARDOUBLECLICK = False

    End Sub

    Sub FILLDED()

        If GRIDDEDDOUBLECLICK = False Then
            GRIDDED.Rows.Add(Val(TXTDEDSRNO.Text.Trim), CMBDEDUCTION.Text.Trim, Val(TXTDEDAMT.Text.Trim))
            getsrno(GRIDDED)
        ElseIf GRIDDEDDOUBLECLICK = True Then

            GRIDDED.Item(GDEDSRNO.Index, TEMPDEDROW).Value = TXTDEDSRNO.Text.Trim
            GRIDDED.Item(GDEDUCTION.Index, TEMPDEDROW).Value = CMBDEDUCTION.Text.Trim
            GRIDDED.Item(GDEDAMT.Index, TEMPDEDROW).Value = Val(TXTDEDAMT.Text.Trim)

            GRIDDEDDOUBLECLICK = False

        End If
        TOTAL()
        GRIDDED.FirstDisplayedScrollingRowIndex = GRIDDED.RowCount - 1

        CMBDEDUCTION.Text = ""
        TXTDEDAMT.Clear()

        TXTDEDSRNO.Text = GRIDDED.RowCount + 1
        CMBDEDUCTION.Focus()

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

        CMBEARNINGS.Text = ""
        TXTEARAMT.Clear()
        TXTEARSRNO.Text = GRIDEAR.RowCount + 1
        CMBEARNINGS.Focus()

    End Sub

    Function CHECKDUPLICATE() As Boolean
        Try
            Dim BLN As Boolean = True
            uppercase(CMBEMPNAME)
            Dim OBJCMN As New ClsCommon
            If (EDIT = False) Or (EDIT = True And LCase(CMBEMPNAME.Text.Trim) <> LCase(TEMPEMPNAME)) Then
                Dim dt As DataTable = OBJCMN.search("EMP_NAME", "", " EMPLOYEEMASTER", " AND EMP_NAME = '" & CMBEMPNAME.Text.Trim & "' AND EMPLOYEEMASTER.EMP_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Employee Name Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                    BLN = False
                End If
            End If

            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If CMBEMPNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBEMPNAME, "Fill Employee Name")
            bln = False
        End If

        If CMBLEDGERNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBLEDGERNAME, "Select Ledger Name")
            bln = False
        End If

        If CMBSTATE.Text.Trim.Length = 0 Then
            EP.SetError(CMBSTATE, "Select State")
            bln = False
        End If

        If Val(TXTSALARY.Text.Trim) = 0 Then
            EP.SetError(TXTSALARY, "Enter Salary Details")
            bln = False
        End If

        If CMBDEPARTMENT.Text.Trim.Length = 0 Then
            EP.SetError(CMBDEPARTMENT, "Select Department")
            bln = False
        End If

        If CMBDESIGNATION.Text.Trim.Length = 0 Then
            EP.SetError(CMBDESIGNATION, "Select Designation")
            bln = False
        End If

        If Not CHECKDUPLICATE() Then
            EP.SetError(CMBEMPNAME, "Employee Name Already Exists")
            bln = False
        End If

        Return bln
    End Function

    Private Sub cmbcity_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCITY.Enter
        Try
            If CMBCITY.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("city_name", "", "CityMaster", " and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "City_name"
                    CMBCITY.DataSource = dt
                    CMBCITY.DisplayMember = "city_name"
                    CMBCITY.Text = ""
                End If
                CMBCITY.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcity_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCITY.Validating
        Try
            If CMBCITY.Text.Trim <> "" Then
                pcase(CMBCITY)
                Dim objclscommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objclscommon.search("city_name", "", "CityMaster", " and city_name = '" & CMBCITY.Text.Trim & "' and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBCITY.Text.Trim
                    Dim tempmsg As Integer = MsgBox("City not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        CMBCITY.Text = a
                        objyearmaster.savecity(CMBCITY.Text.Trim, CmpId, Locationid, Userid, YearId, " and city_name = '" & CMBCITY.Text.Trim & "' and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = CMBCITY.DataSource
                        If CMBCITY.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(CMBCITY.Text)
                                CMBCITY.Text = a
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

    Private Sub cmbstate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSTATE.Validating
        Try
            If CMBSTATE.Text.Trim <> "" Then

                pcase(CMBSTATE)
                Dim objClsCommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objClsCommon.search("state_name", "", "StateMaster", " and state_name = '" & CMBSTATE.Text.Trim & "' and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBSTATE.Text.Trim
                    Dim tempmsg As Integer = MsgBox("State not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        CMBSTATE.Text = a
                        objyearmaster.savestate(CMBSTATE.Text.Trim, CmpId, Locationid, Userid, YearId, " and state_name = '" & CMBSTATE.Text.Trim & "' and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_Yearid = " & YearId)
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

    Private Sub cmbcountry_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCOUNTRY.Enter
        Try
            If CMBCOUNTRY.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("country_name", "", "CountryMaster", " and country_cmpid = " & CmpId & " and country_Locationid = " & Locationid & " and country_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "country_name"
                    CMBCOUNTRY.DataSource = dt
                    CMBCOUNTRY.DisplayMember = "country_name"
                    CMBCOUNTRY.Text = ""
                End If
                CMBCOUNTRY.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcountry_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCOUNTRY.Validating
        Try
            If CMBCOUNTRY.Text.Trim <> "" Then
                pcase(CMBCOUNTRY)
                Dim objClsCommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objClsCommon.search("Country_name", "", "CountryMaster", " and Country_name = '" & CMBCOUNTRY.Text.Trim & "' and country_cmpid = " & CmpId & " and country_Locationid = " & Locationid & " and country_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBCOUNTRY.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Country not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        CMBCOUNTRY.Text = a
                        objyearmaster.savecountry(CMBCOUNTRY.Text.Trim, CmpId, Locationid, Userid, YearId, " and Country_name = '" & CMBCOUNTRY.Text.Trim & "' and country_cmpid = " & CmpId & " and country_Locationid = " & Locationid & " and country_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = CMBCOUNTRY.DataSource
                        If CMBCOUNTRY.DataSource <> Nothing Then
                            If dt1.Rows.Count > 0 Then
Line1:
                                dt1.Rows.Add(CMBCOUNTRY.Text)
                                CMBCOUNTRY.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo Line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbarea_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBAREA.Enter
        Try
            If CMBAREA.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("area_name", "", "AreaMaster", " and area_cmpid = " & CmpId & " and area_Locationid = " & Locationid & " and area_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "area_name"
                    CMBAREA.DataSource = dt
                    CMBAREA.DisplayMember = "area_name"
                    CMBAREA.Text = ""
                End If
                CMBAREA.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbarea_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBAREA.Validating
        Try
            If CMBAREA.Text.Trim <> "" Then
                pcase(CMBAREA)
                Dim objClsCommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objClsCommon.search("area_name", "", "areaMaster", " and area_name = '" & CMBAREA.Text.Trim & "' and area_cmpid = " & CmpId & " and area_Locationid = " & Locationid & " and area_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBAREA.Text.Trim
                    Dim tempmsg As Integer = MsgBox("area not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        CMBAREA.Text = a
                        objyearmaster.savearea(CMBAREA.Text.Trim, CmpId, Locationid, Userid, YearId, " and area_name = '" & CMBAREA.Text.Trim & "' and area_cmpid = " & CmpId & " and area_Locationid = " & Locationid & " and area_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = CMBAREA.DataSource
                        If CMBAREA.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(CMBAREA.Text)
                                CMBAREA.Text = a
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

    Private Sub CMBEMPNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBEMPNAME.Enter
        Try
            If CMBEMPNAME.Text.Trim = "" Then
                FILLEMPNAME()
                CMBEMPNAME.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBEMPNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBEMPNAME.KeyDown
        If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
        If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
    End Sub

    Private Sub CMBEMPNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBEMPNAME.Validating
        Try
            If CMBEMPNAME.Text.Trim <> "" Then
                If Not CHECKDUPLICATE() Then
                    EP.SetError(CMBEMPNAME, "Employee Name Already Exists")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try
            If EDIT = True Then

                If MsgBox("Wish To Delete the Employee?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim ALPARAVAL As New ArrayList

                ALPARAVAL.Add(TEMPEMPNAME)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(YearId)

                Dim OBJACC As New ClsEmployeeMaster
                OBJACC.alParaval = ALPARAVAL
                Dim DT As DataTable = OBJACC.DELETE
                If DT.Rows.Count > 0 Then
                    MsgBox(DT.Rows(0).Item(0))
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYBANK_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPARTYBANK.Enter
        Try
            If CMBPARTYBANK.Text.Trim = "" Then FILLBANK(CMBPARTYBANK)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYBANK_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPARTYBANK.Validating
        Try
            If CMBPARTYBANK.Text.Trim <> "" Then PARTYBANKvalidate(CMBPARTYBANK, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBANKCITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBANKCITY.Enter
        Try
            If CMBBANKCITY.Text.Trim = "" Then fillCITY(CMBBANKCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBANKCITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBANKCITY.Validating
        Try
            If CMBBANKCITY.Text.Trim <> "" Then CITYVALIDATE(CMBBANKCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPLOADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDUPLOAD.Click
        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png"
        OpenFileDialog1.ShowDialog()
        TXTIMGPATH.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTIMGPATH.Text.Trim.Length <> 0 Then PBIMG.ImageLocation = TXTIMGPATH.Text.Trim
    End Sub

    Private Sub CMDREMOVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREMOVE.Click
        Try
            PBIMG.Image = Nothing
            TXTIMGPATH.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDVIEW.Click
        Try
            If TXTIMGPATH.Text.Trim <> "" Then
                If Path.GetExtension(TXTIMGPATH.Text.Trim) = ".pdf" Then
                    System.Diagnostics.Process.Start(TXTIMGPATH.Text.Trim)
                Else
                    Dim objVIEW As New ViewImage
                    objVIEW.pbsoftcopy.Image = PBIMG.Image
                    objVIEW.ShowDialog()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDDED_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDDED.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub
            If e.RowIndex >= 0 And GRIDDED.Item(GDEDSRNO.Index, e.RowIndex).Value <> Nothing Then
                GRIDDEDDOUBLECLICK = True
                TXTDEDSRNO.Text = GRIDDED.Item(GDEDSRNO.Index, e.RowIndex).Value
                CMBDEDUCTION.Text = GRIDDED.Item(GDEDUCTION.Index, e.RowIndex).Value
                TXTDEDAMT.Text = Val(GRIDDED.Item(GDEDAMT.Index, e.RowIndex).Value)
                TEMPDEDROW = e.RowIndex
                CMBDEDUCTION.Focus()
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
                TXTEARAMT.Text = Val(GRIDEAR.Item(GEARAMT.Index, e.RowIndex).Value)
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
                If GRIDDEDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
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
                If GRIDEARDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDEAR.Rows.RemoveAt(GRIDEAR.CurrentRow.Index)
                getsrno(GRIDEAR)
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTDEDAMT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTDEDAMT.Validating
        Try
            If CMBDEDUCTION.Text.Trim <> "" And Val(TXTDEDAMT.Text.Trim) > 0 Then
                FILLDED()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTEARAMT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTEARAMT.Validating
        Try
            If CMBEARNINGS.Text.Trim <> "" And Val(TXTEARAMT.Text.Trim) > 0 Then
                FILLEAR()
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

    Private Sub CMBDEPARTMENT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDEPARTMENT.Enter
        Try
            If CMBDEPARTMENT.Text.Trim = "" Then filldepartment(CMBDEPARTMENT, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDEPARTMENT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDEPARTMENT.Validating
        Try
            If CMBDEPARTMENT.Text.Trim <> "" Then DEPARTMENTVALIDATE(CMBDEPARTMENT, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNATION_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDESIGNATION.Enter
        Try
            If CMBDESIGNATION.Text.Trim = "" Then filldesignation(CMBDESIGNATION)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            EDIT = False
            CMBEMPNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNATION_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGNATION.Validating
        Try
            If CMBDESIGNATION.Text.Trim <> "" Then designationvalidate(CMBDESIGNATION, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLEDGERNAME_Enter(sender As Object, e As EventArgs) Handles CMBLEDGERNAME.Enter
        Try
            If CMBLEDGERNAME.Text.Trim = "" Then fillledger(CMBLEDGERNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors') ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOANLEDGERNAME_Enter(sender As Object, e As EventArgs) Handles CMBLOANLEDGERNAME.Enter
        Try
            If CMBLOANLEDGERNAME.Text.Trim = "" Then fillledger(CMBLOANLEDGERNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'Loans' or GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances' or GROUPMASTER.GROUP_SECONDARY = 'Secured Loans'  or GROUPMASTER.GROUP_SECONDARY = 'Unsecured Loans') ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLEDGERNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBLEDGERNAME.Validating
        Try
            If CMBLEDGERNAME.Text.Trim <> "" Then namevalidate(CMBLEDGERNAME, CMBACCCODE, e, Me, TXTTEMPADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors')", "Sundry Creditors")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOANLEDGERNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBLOANLEDGERNAME.Validating
        Try
            If CMBLOANLEDGERNAME.Text.Trim <> "" Then namevalidate(CMBLOANLEDGERNAME, CMBACCCODE, e, Me, TXTTEMPADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'Loans' or GROUPMASTER.GROUP_SECONDARY = 'Loans & Advances' or GROUPMASTER.GROUP_SECONDARY = 'Secured Loans'  or GROUPMASTER.GROUP_SECONDARY = 'Unsecured Loans')", "Unsecured Loans")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class