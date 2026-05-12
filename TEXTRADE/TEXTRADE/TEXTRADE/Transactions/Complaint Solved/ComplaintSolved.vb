Imports BL
Imports System.ComponentModel
Public Class ComplaintSolved
    Dim IntResult As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public TEMPENTRYNO As String
    Public PARTYNAME As String = ""

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub GET_MAX_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(COMP_NO),0) + 1 ", " COMPLAINTSOLVED ", " and COMP_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Sub FILLCMB()
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' or  GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If


            Dim alParaval As New ArrayList

            If TXTNO.ReadOnly = False Then
                alParaval.Add(Val(TXTNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(SKDATE.Text).Date, "MM/dd/yyyy"))


            'alParaval.Add(Val(LBLTOTALPCS.Text.Trim))
            'alParaval.Add(Val(LBLTOTALMTRS.Text.Trim))
            'alParaval.Add(Val(LBLTOTALRECDPCS.Text.Trim))
            'alParaval.Add(Val(LBLTOTALRECDMTRS.Text.Trim))
            'alParaval.Add(Val(LBLTOTALBALPCS.Text.Trim))
            'alParaval.Add(Val(LBLTOTALBALMTRS.Text.Trim))
            'alParaval.Add(Val(LBLTOTALSMPMTRS.Text.Trim))
            'alParaval.Add(Val(LBLTOTALSHRINKAGE.Text.Trim))
            'alParaval.Add(Val(LBLAVGSHRINKAGE.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)
            'alParaval.Add(TXTLRNO.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim srno As String = ""
            Dim COMPLAINT As String = ""
            Dim COMPLAINTDATE As String = ""
            Dim COMPLAINTBY As String = ""
            Dim BILLINITIALS As String = ""
            Dim BILLNO As String = ""
            Dim REGISTER As String = ""
            'Dim FROMNO As String = ""
            'Dim FROMSRNO As String = ""

            Dim FROMTYPE As String = ""



            For Each row As Windows.Forms.DataGridViewRow In GRIDSHRINKAGE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If srno = "" Then
                        srno = row.Cells(GSRNO.Index).Value.ToString
                        COMPLAINT = row.Cells(GCOMP.Index).Value.ToString
                        COMPLAINTDATE = row.Cells(GCOMPDATE.Index).Value.ToString
                        COMPLAINTBY = row.Cells(GCOMPBY.Index).Value.ToString
                        BILLINITIALS = row.Cells(GBILLINITIALS.Index).Value.ToString
                        BILLNO = Val(row.Cells(GBILLNO.Index).Value)
                        REGISTER = row.Cells(GREGISTER.Index).Value.ToString
                        'FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        'FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)

                        FROMTYPE = row.Cells(GTYPE.Index).Value.ToString

                    Else

                        srno = srno & "|" & row.Cells(GSRNO.Index).Value
                        COMPLAINT = COMPLAINT & "|" & row.Cells(GCOMP.Index).Value
                        COMPLAINTDATE = COMPLAINTDATE & "|" & row.Cells(GCOMPDATE.Index).Value
                        COMPLAINTBY = COMPLAINTBY & "|" & row.Cells(GCOMPBY.Index).Value
                        BILLINITIALS = BILLINITIALS & "|" & row.Cells(GBILLINITIALS.Index).Value

                        BILLNO = BILLNO & "|" & Val(row.Cells(GBILLNO.Index).Value)
                        REGISTER = REGISTER & "|" & row.Cells(GREGISTER.Index).Value
                        'FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        'FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)

                        FROMTYPE = FROMTYPE & "|" & row.Cells(GTYPE.Index).Value


                    End If
                End If



            Next

            alParaval.Add(srno)
            alParaval.Add(COMPLAINT)
            alParaval.Add(COMPLAINTDATE)
            alParaval.Add(COMPLAINTBY)
            alParaval.Add(BILLINITIALS)
            alParaval.Add(BILLNO)
            alParaval.Add(REGISTER)
            'alParaval.Add(FROMNO)
            'alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)





            Dim OBJCLSPROFORMA As New ClsComplaintSolved()
            OBJCLSPROFORMA.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTT As DataTable = OBJCLSPROFORMA.SAVE()
                TXTNO.Text = DTT.Rows(0).Item(0)
                MsgBox("Details Added")

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPENTRYNO)
                Dim IntResult As Integer = OBJCLSPROFORMA.UPDATE()
                MsgBox("Details Updated")

            End If

            'PRINTREPORT(Val(TXTENTRYNO.Text.Trim))
            EDIT = False
            CLEAR()
            CMBNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True
        'If CMBDYEINGNAME.Text.Trim = "" And CMBNAME.Text.Trim <> "" Then CMBDYEINGNAME.Text = CMBNAME.Text.Trim

        Dim OBJCMN As New ClsCommon
        Dim DT As New DataTable
        If Val(TXTNO.Text.Trim) = 0 Then
            EP.SetError(TXTNO, "Enter Invoice No")
            bln = False
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, " Please Enter Name ")
            bln = False
        End If

        If GRIDSHRINKAGE.RowCount = 0 Then
            EP.SetError(CMBNAME, " Please Select Complaint")
            bln = False
        End If


        'CHECKING IF LOTNO = 0
        For Each ROW As DataGridViewRow In GRIDSHRINKAGE.Rows
            ROW.DefaultCellStyle.BackColor = Color.Empty
            If ROW.Cells(GCOMP.Index).Value = "0" Or ROW.Cells(GCOMP.Index).Value = "" Then
                EP.SetError(CMBNAME, " Please Select Entries with Complaint")
                ROW.DefaultCellStyle.BackColor = Color.Green
                bln = False
            End If
        Next


        If SKDATE.Text = "__/__/____" Then
            EP.SetError(SKDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(SKDATE.Text) Then
                EP.SetError(SKDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If
        Return bln

    End Function

    Sub CLEAR()

        EP.Clear()
        TXTNO.Clear()
        SKDATE.Text = Now.Date

        CMBNAME.Text = ""
        TXTNO.Clear()

        txtremarks.Clear()
        TXTLRNO.Clear()

        txtsrno.Text = 1
        TXTCOMPLAINTDATE.Text = Now.Date
        TXTCOMPLAINT.Clear()
        TXTCOMPLAINTBY.Clear()
        TXTBILLINITIALS.Clear()
        TXTBILLNO.Clear()
        CMBREGISTER.Text = ""
        TXTFROMTYPE.Clear()

        GRIDSHRINKAGE.RowCount = 0

        'LBLTOTALPCS.Text = 0.0
        'LBLTOTALMTRS.Text = 0.0
        'LBLTOTALRECDPCS.Text = 0.0
        'LBLTOTALRECDMTRS.Text = 0.0
        'LBLTOTALBALPCS.Text = 0
        'LBLTOTALBALMTRS.Text = 0
        'LBLTOTALSMPMTRS.Text = 0.0
        'LBLTOTALSHRINKAGE.Text = 0.0
        'LBLAVGSHRINKAGE.Text = 0.0
        GET_MAX_NO()
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If MsgBox("Wish to Delete Complaint Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                'DONE BY GULKIT
                'BEFORE UPDATING REVERSE THE ENTRY IN SCHEDULEMASTER_DESC
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPENTRYNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)


                Dim OBJPRO As New ClsComplaintSolved
                OBJPRO.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJPRO.DELETE
                MsgBox("Complaint Solved Deleted Sucessfully")

                CLEAR()
                EDIT = False
                CMBNAME.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDYEINGNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If ClientName = "VINTAGEINDIA" Then
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMBDYEINGNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If ClientName = "VINTAGEINDIA" Then
                If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors')", "Sundry debtors", "ACCOUNTS")
            Else
                If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors')", "Sundry debtors", "ACCOUNTS")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROFORMA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow

            'DTROW = USERRIGHTS.Select("FormName = 'COMPLAINT SOLVED'")

            'USERADD = DTROW(0).Item(1)
            'USEREDIT = DTROW(0).Item(2)
            'USERVIEW = DTROW(0).Item(3)
            'USERDELETE = DTROW(0).Item(4)
            Cursor.Current = Cursors.WaitCursor
            FILLCMB()
            CLEAR()
            If EDIT = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJCMN As New ClsCommon
                Dim OBJCLSPROFORMA As New ClsComplaintSolved()
                Dim dttable As New DataTable

                dttable = OBJCLSPROFORMA.selectSHRINKAGE(TEMPENTRYNO, CmpId, YearId)
                If dttable.Rows.Count > 0 Then
                    For Each dr As DataRow In dttable.Rows

                        TXTNO.Text = TEMPENTRYNO
                        TXTNO.ReadOnly = True
                        SKDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                        txtremarks.Text = Convert.ToString(dr("REMARKS").ToString)
                        'TXTLRNO.Text = Convert.ToString(dr("LRNO").ToString)
                        GRIDSHRINKAGE.Rows.Add(dr("COMPSRNO").ToString, dr("COMPLAINT"), dr("COMPLAINTDATE").ToString, dr("COMPLAINTBY").ToString, dr("BILLINITIALS").ToString, Val(dr("BILLNO")), dr("REGNAME").ToString, dr("FROMTYPE").ToString)

                    Next

                    'TOTAL()
                    GRIDSHRINKAGE.FirstDisplayedScrollingRowIndex = GRIDSHRINKAGE.RowCount - 1
                Else
                    EDIT = False
                    CLEAR()
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        GRIDSHRINKAGE.Enabled = True
        If GRIDDOUBLECLICK = False Then
            GRIDSHRINKAGE.Rows.Add(Val(TXTNO.Text.Trim), TXTCOMPLAINT.Text.Trim, TXTCOMPLAINTDATE.Text.Trim, TXTCOMPLAINTBY.Text.Trim, TXTBILLINITIALS.Text.Trim, TXTBILLNO.Text.Trim, CMBREGISTER.Text.Trim, 0, 0, TXTFROMTYPE.Text.Trim)
            getsrno(GRIDSHRINKAGE)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDSHRINKAGE.Item(GSRNO.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDSHRINKAGE.Item(GCOMP.Index, TEMPROW).Value = TXTCOMPLAINT.Text.Trim
            GRIDSHRINKAGE.Item(GCOMPDATE.Index, TEMPROW).Value = TXTCOMPLAINTDATE.Text.Trim
            GRIDSHRINKAGE.Item(GCOMPBY.Index, TEMPROW).Value = TXTCOMPLAINTBY.Text.Trim
            GRIDSHRINKAGE.Item(GBILLINITIALS.Index, TEMPROW).Value = TXTBILLINITIALS.Text.Trim
            GRIDSHRINKAGE.Item(GBILLNO.Index, TEMPROW).Value = TXTBILLNO.Text.Trim
            GRIDSHRINKAGE.Item(GREGISTER.Index, TEMPROW).Value = CMBREGISTER.Text.Trim
            GRIDSHRINKAGE.Item(GTYPE.Index, TEMPROW).Value = TXTFROMTYPE.Text.Trim


            GRIDDOUBLECLICK = False

        End If

        'CALC()
        'total()

        GRIDSHRINKAGE.FirstDisplayedScrollingRowIndex = GRIDSHRINKAGE.RowCount - 1

        TXTCOMPLAINT.Text = ""
        TXTCOMPLAINTDATE.Text = ""
            CMBREGISTER.Text = ""
            TXTCOMPLAINTBY.Clear()
            TXTBILLINITIALS.Clear()
            TXTBILLNO.Clear()
            TXTFROMTYPE.Clear()
        txtsrno.Text = Val(GRIDSHRINKAGE.RowCount) + 1

    End Sub

    'Sub TOTAL()
    '    Try

    '        LBLTOTALPCS.Text = 0.0
    '        LBLTOTALMTRS.Text = 0.0
    '        LBLTOTALRECDPCS.Text = 0.0
    '        LBLTOTALRECDMTRS.Text = 0.0
    '        LBLTOTALBALPCS.Text = 0.0
    '        LBLTOTALBALMTRS.Text = 0.0
    '        LBLTOTALSMPMTRS.Text = 0.0
    '        LBLTOTALSHRINKAGE.Text = 0.0
    '        LBLAVGSHRINKAGE.Text = 0.0

    '        If GRIDSHRINKAGE.RowCount > 0 Then
    '            For Each row As DataGridViewRow In GRIDSHRINKAGE.Rows
    '                LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(row.Cells(Gpcs.Index).EditedFormattedValue), "0.00")
    '                LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(row.Cells(Gmtrs.Index).EditedFormattedValue), "0.00")
    '                LBLTOTALRECDPCS.Text = Format(Val(LBLTOTALRECDPCS.Text) + Val(row.Cells(GRECDPCS.Index).EditedFormattedValue), "0.00")
    '                LBLTOTALRECDMTRS.Text = Format(Val(LBLTOTALRECDMTRS.Text) + Val(row.Cells(GRECDMTRS.Index).EditedFormattedValue), "0.00")
    '                LBLTOTALBALPCS.Text = Format(Val(LBLTOTALBALPCS.Text) + Val(row.Cells(GBALPCS.Index).EditedFormattedValue), "0.00")
    '                LBLTOTALBALMTRS.Text = Format(Val(LBLTOTALBALMTRS.Text) + Val(row.Cells(GBALMTRS.Index).EditedFormattedValue), "0.00")
    '                LBLTOTALSMPMTRS.Text = Format(Val(LBLTOTALSMPMTRS.Text) + Val(row.Cells(GSMPMTRS.Index).EditedFormattedValue), "0.00")
    '                row.Cells(GShrinkage.Index).Value = Format(Val(row.Cells(GBALMTRS.Index).EditedFormattedValue) - Val(row.Cells(GSMPMTRS.Index).EditedFormattedValue), "0.00")
    '                row.Cells(GSHRINKAGEPER.Index).Value = Format((Val(row.Cells(GShrinkage.Index).EditedFormattedValue) / Val(row.Cells(Gmtrs.Index).EditedFormattedValue)) * 100, "0.00")
    '                LBLTOTALSHRINKAGE.Text = Format(Val(LBLTOTALSHRINKAGE.Text) + Val(row.Cells(GShrinkage.Index).EditedFormattedValue), "0.00")
    '            Next
    '            LBLAVGSHRINKAGE.Text = Format((Val(LBLTOTALSHRINKAGE.Text) / Val(LBLTOTALMTRS.Text)) * 100, "0.00")
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub toolPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLPRIVIOUS.Click
        Try
            GRIDSHRINKAGE.RowCount = 0
LINE1:
            TEMPENTRYNO = Val(TXTNO.Text) - 1
            If TEMPENTRYNO > 0 Then
                EDIT = True
                PROFORMA_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDSHRINKAGE.RowCount = 0 And TEMPENTRYNO > 1 Then
                TXTNO.Text = TEMPENTRYNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PROFORMA_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemQuotes Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.F5 Then
                GRIDSHRINKAGE.Focus()
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolPREVIOUS_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_CLICK(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objpodtls As New ComplaintSolvedDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_CLICK(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDSHRINKAGE.RowCount = 0
LINE1:
            TEMPENTRYNO = Val(TXTNO.Text) + 1
            GET_MAX_NO()
            Dim MAXNO As Integer = TXTNO.Text.Trim
            CLEAR()
            If Val(TXTNO.Text) - 1 >= TEMPENTRYNO Then
                EDIT = True
                PROFORMA_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDSHRINKAGE.RowCount = 0 And TEMPENTRYNO < MAXNO Then
                TXTNO.Text = TEMPENTRYNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTLOT_Click(sender As Object, e As EventArgs) Handles CMDSELECTCOMPLAINT.Click
        Try
            If EDIT = True And UserName <> "Admin" Then
                MsgBox("Not allowed in Edit Mode", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If CMBNAME.Text.Trim = "" Then
                MsgBox("Please Select Name First", MsgBoxStyle.Critical)
                CMBNAME.Focus()
                Exit Sub
            End If


            'Dim OBJLOT As New SelectComplaint
            'OBJLOT.JOBBERNAME = CMBNAME.Text.Trim
            'OBJLOT.ShowDialog()

            ' Collect BILLINITIALS already added in the grid
            Dim existingBillInitials As New List(Of String)
            For Each row As DataGridViewRow In GRIDSHRINKAGE.Rows
                If row.Cells(GBILLINITIALS.Index).Value IsNot Nothing AndAlso
       row.Cells(GBILLINITIALS.Index).Value.ToString.Trim <> "" Then
                    existingBillInitials.Add("'" & row.Cells(GBILLINITIALS.Index).Value.ToString.Trim.Replace("'", "''") & "'")
                End If
            Next

            Dim OBJLOT As New SelectComplaint
            OBJLOT.JOBBERNAME = CMBNAME.Text.Trim

            ' Exclude already selected BILLINITIALS
            If existingBillInitials.Count > 0 Then
                OBJLOT.WCLAUSE = " AND COMPLAINTREGISTERVIEW.BILLINITIALS NOT IN (" & String.Join(",", existingBillInitials) & ")"
            End If

            OBJLOT.ShowDialog()

            Dim DTTABLE As DataTable = OBJLOT.DT
            If DTTABLE.Rows.Count > 0 Then
                For Each DTROW As DataRow In DTTABLE.Rows
                    'TXTLRNO.Text = DTROW("LRNO")

                    'THEY NEED LRNO IN GRID
                    'If ClientName = "AVIS" Then DTROW("CHALLANNO") = DTROW("LRNO")
                    Dim RAWDATE As String = DTROW("COMPLAINTDATE").ToString().Trim()
                    Dim COMPLAINTDATE As String = ""

                    If RAWDATE <> "" AndAlso RAWDATE <> "__/__/____" Then
                        COMPLAINTDATE = Format(Convert.ToDateTime(RAWDATE).Date, "dd/MM/yyyy")
                    End If
                    GRIDSHRINKAGE.Rows.Add(0, DTROW("COMPLAINT"), COMPLAINTDATE, DTROW("COMPLAINTBY"), DTROW("BILLINITIALS"), Val(DTROW("BILLNO")), DTROW("REGISTER"), DTROW("TYPE"))
                Next
                getsrno(GRIDSHRINKAGE)
                'TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTSHRINKAGE_Validating(sender As Object, e As CancelEventArgs) Handles TXTFROMTYPE.Validating
        Try
            If CMBREGISTER.Text.Trim <> "" Then
                FILLGRID()
                'TOTAL()
            Else
                MsgBox("Enter Proper Details")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDSHRINKAGE.CurrentRow.Index >= 0 And GRIDSHRINKAGE.Item(GSRNO.Index, GRIDSHRINKAGE.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True

                txtsrno.Text = Val(GRIDSHRINKAGE.Item(GSRNO.Index, GRIDSHRINKAGE.CurrentRow.Index).Value)
                TXTCOMPLAINT.Text = GRIDSHRINKAGE.Item(GCOMP.Index, GRIDSHRINKAGE.CurrentRow.Index).Value.ToString
                TXTCOMPLAINTDATE.Text = GRIDSHRINKAGE.Item(GCOMPDATE.Index, GRIDSHRINKAGE.CurrentRow.Index).Value.ToString
                TXTCOMPLAINTBY.Text = GRIDSHRINKAGE.Item(GCOMPBY.Index, GRIDSHRINKAGE.CurrentRow.Index).Value.ToString
                TXTBILLINITIALS.Text = GRIDSHRINKAGE.Item(GBILLINITIALS.Index, GRIDSHRINKAGE.CurrentRow.Index).ToString
                TXTBILLNO.Text = Val(GRIDSHRINKAGE.Item(GBILLNO.Index, GRIDSHRINKAGE.CurrentRow.Index).Value)
                CMBREGISTER.Text = GRIDSHRINKAGE.Item(GREGISTER.Index, GRIDSHRINKAGE.CurrentRow.Index).Value.ToString
                TXTFROMTYPE.Text = GRIDSHRINKAGE.Item(GTYPE.Index, GRIDSHRINKAGE.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDSHRINKAGE.CurrentRow.Index
                CMBREGISTER.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDPROFORMA_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDSHRINKAGE.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCOSTSHEET_Click(sender As Object, e As EventArgs) Handles CMDCOSTSHEET.Click
        Try
            If EDIT = False Then Exit Sub
            Dim OBJRPT As New clsReportDesigner("Cost Sheet", System.AppDomain.CurrentDomain.BaseDirectory & "Cost Sheet.xlsx", 2)
            OBJRPT.COSTSHEET_EXCEL(CmpId, YearId, Val(TEMPENTRYNO), Val(LBLAVGSHRINKAGE.Text), CMBNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
    End Sub

    'Private Sub GRIDSHRINKAGE_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDSHRINKAGE.CellValidating
    '    Try
    '        ''  CODE FOR NUMERIC CHECK ONLY
    '        Dim colNum As Integer = GRIDSHRINKAGE.Columns(e.ColumnIndex).Index
    '        If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

    '        Select Case colNum

    '            Case GSMPMTRS.Index
    '                Dim dDebit As Decimal
    '                Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

    '                If bValid Then
    '                    If GRIDSHRINKAGE.CurrentCell.Value = Nothing Then GRIDSHRINKAGE.CurrentCell.Value = "0.00"
    '                    GRIDSHRINKAGE.CurrentCell.Value = Convert.ToDecimal(GRIDSHRINKAGE.Item(colNum, e.RowIndex).Value)
    '                    TOTAL()
    '                Else
    '                    MessageBox.Show("Invalid Number Entered")
    '                    e.Cancel = True
    '                    Exit Sub
    '                End If
    '        End Select
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub GRIDSHRINKAGE_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDSHRINKAGE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSHRINKAGE.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbMERCHANT.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDSHRINKAGE.Rows.RemoveAt(GRIDSHRINKAGE.CurrentRow.Index)
                'TOTAL()
                getsrno(GRIDSHRINKAGE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validated(sender As Object, e As EventArgs) Handles tstxtbillno.Validated
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDSHRINKAGE.RowCount = 0
                TEMPENTRYNO = Val(tstxtbillno.Text)
                If TEMPENTRYNO > 0 Then
                    EDIT = True
                    PROFORMA_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ShrinkageEntry_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'If ClientName = "AVIS" Then GCHALLANNO.HeaderText = "LR No"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtremarks_KeyDown(sender As Object, e As KeyEventArgs) Handles txtremarks.KeyDown
        Try
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
End Class