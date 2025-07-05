
Imports BL
Imports System.ComponentModel

Public Class BlanketRepresentation

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPBLANKETDETROW As Integer
    Public EDIT As Boolean
    Public TEMPBLANKETREPNO As String
    Dim DT_BLANKETDETAILS As New DataTable

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Sub getmax_BLANKET_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(BLANKET_NO),0) + 1 ", "BLANKETREPRESENT   ", " AND BLANKET_cmpid=" & CmpId & "  and BLANKET_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTBLANKETNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try
            Ep.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            If TXTBLANKETNO.ReadOnly = False Then
                alParaval.Add(Val(TXTBLANKETNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(Format(Convert.ToDateTime(DTDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBPARTYNAME.Text.Trim)
            alParaval.Add(CMBAGENTNAME.Text.Trim)
            alParaval.Add(CMBSALESMAN.Text.Trim)
            alParaval.Add(TXTREMARKS.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)



            Dim BLANKETSRNO As String = ""
            Dim BLANKETNAME As String = ""
            For Each row As Windows.Forms.DataGridViewRow In GRIDBLANKET.Rows
                If row.Cells(0).Value <> Nothing Then
                    If BLANKETSRNO = "" Then
                        BLANKETSRNO = Val(row.Cells(ESRNO.Index).Value)
                        BLANKETNAME = row.Cells(EBLANKET.Index).Value.ToString
                    Else
                        BLANKETSRNO = BLANKETSRNO & "|" & Val(row.Cells(ESRNO.Index).Value)
                        BLANKETNAME = BLANKETNAME & "|" & row.Cells(EBLANKET.Index).Value.ToString
                    End If
                End If
            Next


            alParaval.Add(BLANKETSRNO)
            alParaval.Add(BLANKETNAME)



            Dim SRNO As String = ""
            Dim DESIGN As String = ""
            Dim REMARKS As String = ""
            Dim APPROVAL As String = ""
            Dim MAINSRNO As String = ""

            For i As Integer = 0 To DT_BLANKETDETAILS.Rows.Count - 1
                If DT_BLANKETDETAILS.Rows(i).Item(0) <> Nothing Then
                    If SRNO = "" Then
                        SRNO = Val(DT_BLANKETDETAILS.Rows(i).Item("SRNO"))
                        DESIGN = DT_BLANKETDETAILS.Rows(i).Item("DESIGN")
                        REMARKS = DT_BLANKETDETAILS.Rows(i).Item("REMARKS")
                        APPROVAL = DT_BLANKETDETAILS.Rows(i).Item("APPROVAL")
                        MAINSRNO = Val(DT_BLANKETDETAILS.Rows(i).Item("MAINSRNO"))
                    Else
                        SRNO = SRNO & "|" & Val(DT_BLANKETDETAILS.Rows(i).Item("SRNO"))
                        DESIGN = DESIGN & "|" & DT_BLANKETDETAILS.Rows(i).Item("DESIGN")
                        REMARKS = REMARKS & "|" & DT_BLANKETDETAILS.Rows(i).Item("REMARKS")
                        APPROVAL = APPROVAL & "|" & DT_BLANKETDETAILS.Rows(i).Item("APPROVAL")
                        MAINSRNO = MAINSRNO & "|" & Val(DT_BLANKETDETAILS.Rows(i).Item("MAINSRNO"))
                    End If
                End If
            Next


            alParaval.Add(SRNO)
            alParaval.Add(DESIGN)
            alParaval.Add(REMARKS)
            alParaval.Add(APPROVAL)
            alParaval.Add(MAINSRNO)



            Dim objclsItemMaster As New ClsBlanketRepresentation
            objclsItemMaster.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = objclsItemMaster.SAVE()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPBLANKETREPNO)
                Dim IntResult As Integer = objclsItemMaster.UPDATE()
                MsgBox("Details Updated")

            End If
            EDIT = False
            CLEAR()
            CMBPARTYNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True

        If CMBPARTYNAME.Text.Trim.Length = 0 Then
            Ep.SetError(CMBPARTYNAME, "Fill Party Name")
            bln = False
        End If

        If CMBAGENTNAME.Text.Trim.Length = 0 And CMBSALESMAN.Text.Trim.Length = 0 Then
            Ep.SetError(CMBAGENTNAME, "Enter Agent / Salesman")
            bln = False
        End If

        If GRIDBLANKET.RowCount = 0 Then
            Ep.SetError(CMBBLANKETNAME, "Enter Blanket Details")
            bln = False
        End If

        Return bln
    End Function

    Sub CLEAR()
        Try
            Ep.Clear()
            TXTBLANKETNO.Clear()
            DTDATE.Text = Now.Date

            CMBPARTYNAME.Text = ""
            CMBAGENTNAME.Text = ""
            CMBSALESMAN.Text = ""
            TXTREMARKS.Clear()

            getmax_BLANKET_no()
            GRIDBLANKET.RowCount = 0

            TXTSRNO.Text = 1
            CMBBLANKETNAME.Text = ""

            DT_BLANKETDETAILS.Reset()
            DT_BLANKETDETAILS.Columns.Add("SRNO")
            DT_BLANKETDETAILS.Columns.Add("DESIGN")
            DT_BLANKETDETAILS.Columns.Add("REMARKS")
            DT_BLANKETDETAILS.Columns.Add("APPROVAL")
            DT_BLANKETDETAILS.Columns.Add("MAINSRNO")

            GRIDDOUBLECLICK = False
            GRIDBLANKETDESIGN.RowCount = 0



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBPARTYNAME.Text.Trim = "" Then FILLNAME(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            If CMBAGENTNAME.Text.Trim = "" Then FILLNAME(CMBAGENTNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
            If CMBSALESMAN.Text.Trim = "" Then FILLNAME(CMBSALESMAN, EDIT, "")
            If CMBBLANKETNAME.Text.Trim = "" Then FILLBLANKET(CMBBLANKETNAME)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSALESMAN_Enter(sender As Object, e As EventArgs) Handles CMBSALESMAN.Enter
        Try
            If CMBSALESMAN.Text.Trim = "" Then FILLSALESMAN(CMBSALESMAN)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENTNAME_Enter(sender As Object, e As EventArgs) Handles CMBAGENTNAME.Enter
        Try
            If CMBAGENTNAME.Text.Trim = "" Then FILLNAME(CMBAGENTNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Enter(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then FILLNAME(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBLANKETNAME_Enter(sender As Object, e As EventArgs) Handles CMBBLANKETNAME.Enter
        Try
            If CMBBLANKETNAME.Text.Trim = "" Then FILLBLANKET(CMBBLANKETNAME)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBPARTYNAME, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors'", "Sundry Debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBAGENTNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBAGENTNAME.Validating
        Try
            If CMBAGENTNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBAGENTNAME, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSALESMAN_Validating(sender As Object, e As CancelEventArgs) Handles CMBSALESMAN.Validating
        Try
            If CMBSALESMAN.Text.Trim <> "" Then SALESMANVALIDATE(CMBSALESMAN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBLANKETNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBBLANKETNAME.Validating
        Try
            If CMBBLANKETNAME.Text.Trim <> "" Then BLANKETVALIDATE(CMBBLANKETNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BlanketRepresentation_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BlanketRepresentation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow

            DTROW = USERRIGHTS.Select("FormName = 'DESIGN MASTER'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            FILLCMB()
            CLEAR()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJCLSBLANKET As New ClsBlanketRepresentation
                OBJCLSBLANKET.alParaval.Add(TEMPBLANKETREPNO)
                OBJCLSBLANKET.alParaval.Add(CmpId)
                OBJCLSBLANKET.alParaval.Add(YearId)
                Dim DTTABLE As DataTable = OBJCLSBLANKET.SELECTBLANKETREPRESENT()

                If DTTABLE.Rows.Count > 0 Then
                    For Each dr As DataRow In DTTABLE.Rows

                        TXTBLANKETNO.Text = TEMPBLANKETREPNO
                        DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")
                        CMBPARTYNAME.Text = Convert.ToString(dr("PARTYNAME"))
                        CMBAGENTNAME.Text = Convert.ToString(dr("AGENTNAME"))
                        CMBSALESMAN.Text = Convert.ToString(dr("SALESMAN"))
                        TXTREMARKS.Text = dr("REMARKS")

                        GRIDBLANKET.Rows.Add(Val(dr("BLANKETSRNO")), dr("BLANKETNAME"))
                    Next

                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" BLANKETREPRESENT_DESIGNDESC.BLANKET_SRNO AS SRNO, DESIGNMASTER.DESIGN_NO AS DESIGNNO, BLANKETREPRESENT_DESIGNDESC.BLANKET_GRIDREMARKS AS GRIDREMARKS, BLANKETREPRESENT_DESIGNDESC.BLANKET_APPROVAL AS APPROVAL,  BLANKETREPRESENT_DESIGNDESC.BLANKET_MAINSRNO AS MAINSRNO ", "", " BLANKETREPRESENT_DESIGNDESC INNER JOIN BLANKETREPRESENT ON BLANKETREPRESENT_DESIGNDESC.BLANKET_NO = BLANKETREPRESENT.BLANKET_NO AND BLANKETREPRESENT_DESIGNDESC.BLANKET_YEARID = BLANKETREPRESENT.BLANKET_YEARID INNER JOIN DESIGNMASTER ON BLANKETREPRESENT_DESIGNDESC.BLANKET_DESIGNID = DESIGNMASTER.DESIGN_id ", " AND BLANKETREPRESENT.BLANKET_NO = " & TEMPBLANKETREPNO & " AND BLANKETREPRESENT_DESIGNDESC.BLANKET_YEARID=" & YearId & " ORDER BY  BLANKETREPRESENT_DESIGNDESC.BLANKET_SRNO ")
                    For Each DR As DataRow In DT.Rows
                        DT_BLANKETDETAILS.Rows.Add(Val(DR("SRNO")), DR("DESIGNNO"), DR("GRIDREMARKS"), DR("APPROVAL"), Val(DR("MAINSRNO")))
                    Next

                    CMBPARTYNAME.Focus()
                Else
                    EDIT = False
                    CLEAR()
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If EDIT = False Then Exit Sub

            If MsgBox("Delete Entry ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim OBJCLSBLANKET As New ClsBlanketRepresentation
            OBJCLSBLANKET.alParaval.Add(Val(TXTBLANKETNO.Text.Trim))
            OBJCLSBLANKET.alParaval.Add(YearId)
            Dim IntResult As Integer = OBJCLSBLANKET.DELETE()
            MsgBox("Entry Deleted Successfully")
            EDIT = False
            CLEAR()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBLANKETNAME_Validated(sender As Object, e As EventArgs) Handles CMBBLANKETNAME.Validated
        Try
            If CMBBLANKETNAME.Text.Trim <> "" Then
                If Not CHECKBLANKET() Then
                    MsgBox("Blanket already Present in Grid below ")
                    Exit Sub
                End If

                FILLBLANKETGRID()
                CMBBLANKETNAME.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CHECKBLANKET() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In GRIDBLANKET.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Or GRIDDOUBLECLICK = False Then
                    If CMBBLANKETNAME.Text.Trim = ROW.Cells(EBLANKET.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub FILLBLANKETGRID()
        Try
            'If GRIDDOUBLECLICK = False Then
            '    GRIDBLANKET.Rows.Add(Val(TXTSRNO.Text), CMBBLANKETNAME.Text.Trim)
            '    GETSRNO(GRIDBLANKET)
            '    GRIDBLANKETDESIGN.RowCount = 0
            '    FILLGRIDBLANKETDESIGN(CMBBLANKETNAME.Text.Trim, GRIDBLANKET.RowCount)

            'ElseIf GRIDDOUBLECLICK = True Then
            '    GRIDBLANKET.Item(ESRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text)
            '    GRIDBLANKET.Item(EBLANKET.Index, TEMPROW).Value = CMBBLANKETNAME.Text.Trim
            '    GRIDDOUBLECLICK = False
            'End If

            GRIDBLANKET.Rows.Add(Val(TXTSRNO.Text), CMBBLANKETNAME.Text.Trim)
            GETSRNO(GRIDBLANKET)
            GRIDBLANKETDESIGN.RowCount = 0
            FILLGRIDBLANKETDESIGN(CMBBLANKETNAME.Text.Trim, GRIDBLANKET.RowCount)
            GRIDBLANKET.FirstDisplayedScrollingRowIndex = GRIDBLANKET.RowCount - 1

            GRIDBLANKET.Rows(GRIDBLANKET.RowCount - 1).Selected = True
            GRIDBLANKET.CurrentCell = GRIDBLANKET.Item(0, GRIDBLANKET.RowCount - 1)
            TXTSRNO.Text = GRIDBLANKET.RowCount + 1

            CMBBLANKETNAME.Text = ""

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRIDBLANKETDESIGN(BLANKETNAME As String, TMAINSRNO As Integer)
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN", "", " BLANKETMASTER_DESIGN INNER JOIN DESIGNMASTER ON BLANKETMASTER_DESIGN.BLANKET_DESIGNID = DESIGNMASTER.DESIGN_id INNER JOIN BLANKETMASTER ON BLANKETMASTER_DESIGN.BLANKET_ID = BLANKETMASTER.BLANKET_ID ", " AND BLANKETMASTER.BLANKET_NAME = '" & BLANKETNAME & "' And BLANKETMASTER.BLANKET_YEARID = " & YearId)
            Dim I As Integer = 1
            For Each DTR As DataRow In DT.Rows
                GRIDBLANKETDESIGN.Rows.Add(I, DTR("DESIGN"), "", "", TMAINSRNO)
                DT_BLANKETDETAILS.Rows.Add(I, DTR("DESIGN"), "", "", TMAINSRNO)
                I += 1
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBLANKETDESIGN_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDBLANKETDESIGN.CellValidating

    End Sub

    Sub UPDATEDT(TMAINSRNO As Integer)
        Try
            'FIRST DELETE ALL ROWS OF MAINSRNO THEN REINSERT
LINE1:
            For Each ROW As DataRow In DT_BLANKETDETAILS.Rows
                If Val(ROW("MAINSRNO")) = TMAINSRNO Then
                    ROW.Delete()
                    GoTo LINE1
                End If
            Next

            For Each ROW As DataGridViewRow In GRIDBLANKETDESIGN.Rows
                DT_BLANKETDETAILS.Rows.Add(Val(ROW.Cells(GSRNO.Index).Value), ROW.Cells(GDESIGN.Index).Value, ROW.Cells(GREMARKS.Index).EditedFormattedValue, ROW.Cells(GAPPROVAL.Index).Value, TMAINSRNO)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBLANKET_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDBLANKET.CellClick
        Try
            If GRIDBLANKET.Rows.Count > 0 Then
                GRIDBLANKETDESIGN.RowCount = 0
                For Each ROW As DataRow In DT_BLANKETDETAILS.Rows
                    If Val(ROW("MAINSRNO")) = Val(GRIDBLANKET.CurrentRow.Cells(ESRNO.Index).Value) Then
                        GRIDBLANKETDESIGN.Rows.Add(Val(ROW("SRNO")), ROW("DESIGN"), ROW("REMARKS"), ROW("APPROVAL"), Val(ROW("MAINSRNO")))
                    End If
                Next
                GETSRNO(GRIDBLANKETDESIGN)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBLANKET_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDBLANKET.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
LINE1:
                For Each ROW As DataRow In DT_BLANKETDETAILS.Rows
                    If Val(GRIDBLANKET.CurrentRow.Cells(ESRNO.Index).Value) = Val(ROW("MAINSRNO")) Then
                        ROW.Delete()
                        GoTo LINE1
                    End If
                Next


                For Each ROW As DataRow In DT_BLANKETDETAILS.Rows
                    If Val(GRIDBLANKET.CurrentRow.Cells(ESRNO.Index).Value) < Val(ROW("MAINSRNO")) Then
                        ROW("MAINSRNO") = Val(ROW("MAINSRNO")) - 1
                    End If
                Next
                GRIDBLANKET.Rows.RemoveAt(GRIDBLANKET.CurrentRow.Index)
                GRIDBLANKETDESIGN.RowCount = 0
                GETSRNO(GRIDBLANKET)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETSRNO(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            GRIDBLANKET.RowCount = 0
LINE1:
            TEMPBLANKETREPNO = Val(TXTBLANKETNO.Text) - 1
            If TEMPBLANKETREPNO > 0 Then
                EDIT = True
                BlanketRepresentation_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDBLANKET.RowCount = 0 And TEMPBLANKETREPNO > 1 Then
                TXTBLANKETNO.Text = TEMPBLANKETREPNO
                GoTo LINE1
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            GRIDBLANKET.RowCount = 0
LINE1:
            TEMPBLANKETREPNO = Val(TXTBLANKETNO.Text) + 1
            getmax_BLANKET_no()
            Dim MAXNO As Integer = TXTBLANKETNO.Text.Trim
            CLEAR()
            If Val(TXTBLANKETNO.Text) - 1 >= TEMPBLANKETREPNO Then
                EDIT = True
                BlanketRepresentation_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDBLANKET.RowCount = 0 And TEMPBLANKETREPNO < MAXNO Then
                TXTBLANKETNO.Text = TEMPBLANKETREPNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        DTDATE.Focus()
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim obj As New BlanketRepresentationDetails
            obj.MdiParent = MDIMain
            obj.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BlanketRepresentation_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            GAPPROVAL.Items.Clear()
            GAPPROVAL.Items.Add("")
            GAPPROVAL.Items.Add("APPROVED")
            GAPPROVAL.Items.Add("REJECTED")
            GAPPROVAL.Items.Add("INTRESTED")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBLANKETDESIGN_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDBLANKETDESIGN.CellValueChanged
        Try
            If GRIDBLANKETDESIGN.RowCount = 0 Then Return
            Dim colNum As Integer = GRIDBLANKETDESIGN.Columns(e.ColumnIndex).Index
            Select Case colNum
                Case GREMARKS.Index, GAPPROVAL.Index
                    'UPDATE RECORDS IN DT
                    UPDATEDT(Val(GRIDBLANKET.SelectedRows(0).Cells(ESRNO.Index).Value))
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Sub EDITROW()
    '    Try
    '        If GRIDBLANKET.CurrentRow.Index >= 0 And GRIDBLANKET.Item(GSRNO.Index, GRIDBLANKET.CurrentRow.Index).Value <> Nothing Then
    '            GRIDDOUBLECLICK = True
    '            TXTSRNO.Text = GRIDBLANKET.Item(ESRNO.Index, GRIDBLANKET.CurrentRow.Index).Value.ToString
    '            CMBBLANKETNAME.Text = GRIDBLANKET.Item(EBLANKET.Index, GRIDBLANKET.CurrentRow.Index).Value.ToString


    '            TEMPROW = GRIDBLANKET.CurrentRow.Index
    '            TXTSRNO.Focus()
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub GRIDBLANKET_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDBLANKET.CellDoubleClick
    '    Try
    '        If e.RowIndex >= 0 And GRIDBLANKET.Item(EBLANKET.Index, e.RowIndex).Value <> Nothing Then
    '            GRIDDOUBLECLICK = True
    '            TEMPROW = e.RowIndex
    '            TXTGSRNO.Text = GRIDBLANKET.Item(ESRNO.Index, e.RowIndex).Value
    '            CMBBLANKETNAME.Text = GRIDBLANKET.Item(EBLANKET.Index, e.RowIndex).Value
    '            CMBBLANKETNAME.Focus()
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

End Class