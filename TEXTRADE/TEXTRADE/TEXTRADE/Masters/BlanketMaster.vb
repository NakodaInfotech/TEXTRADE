Imports BL
Imports System.IO
Imports System.ComponentModel

Public Class BlanketMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE, GRIDDESIGNDOUBLECLICK As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean              'Used for edit
    Public TEMPNAME As String           'Used for edit name
    Public TEMPID As Integer            'Used for edit id
    Dim TEMPROW As Integer

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            Ep.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(TXTNAME.Text.Trim)
            ALPARAVAL.Add(CMBPARTYNAME.Text.Trim)
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            'FOR GRID

            Dim DESIGN As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDDESIGN.Rows
                If row.Cells(0).Value <> Nothing Then
                    If DESIGN = "" Then
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                    Else
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                    End If
                End If
            Next

            ALPARAVAL.Add(DESIGN)

            Dim OBJADD As New ClsBlanketMaster
            OBJADD.ALPARAVAL = ALPARAVAL
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = OBJADD.SAVE()
                MsgBox("Details Added")

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                ALPARAVAL.Add(TEMPID)

                IntResult = OBJADD.UPDATE()
                MsgBox("Details Updated")
                EDIT = False
            End If
            CLEAR()
            CMBPARTYNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True
            If TXTNAME.Text.Trim.Length = 0 Then
                Ep.SetError(TXTNAME, "Fill Name")
                BLN = False
            Else
                If (EDIT = False) Or (EDIT = True And LCase(TEMPNAME) <> LCase(TXTNAME.Text.Trim)) Then
                    ' search duplication 
                    Dim OBJCMN As New ClsCommon
                    Dim dt As New DataTable
                    dt = OBJCMN.SEARCH(" BLANKET_NAME ", "", " BLANKETMASTER ", " AND BLANKET_NAME = '" & TXTNAME.Text.Trim & "' AND  BLANKET_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        Ep.SetError(TXTNAME, "Name Already Exists")
                        BLN = False
                    End If
                    uppercase(TXTNAME)
                End If
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub CLEAR()
        Try
            Ep.Clear()
            TXTNAME.Clear()
            TXTREMARKS.Clear()
            CMBPARTYNAME.Text = ""
            CMBDESIGN.Text = ""
            GRIDDESIGN.RowCount = 0
            GRIDDESIGNDOUBLECLICK = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNAME.Validating
        Try
            If (EDIT = False) Or (EDIT = True And LCase(TEMPNAME) <> LCase(TXTNAME.Text.Trim)) Then
                ' search duplication 
                If TXTNAME.Text.Trim <> Nothing Then
                    Dim OBJCMN As New ClsCommonMaster
                    Dim dt As New DataTable
                    dt = OBJCMN.search("BLANKET_NAME ", "", " BLANKETMASTER ", " AND BLANKET_NAME = '" & TXTNAME.Text.Trim & "' AND  BLANKET_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Name Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                        e.Cancel = True
                    End If
                    uppercase(TXTNAME)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BlanketMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then               'FOR EXIT
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.OemQuotes Then
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BlanketMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'DESIGN MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            FILLCMB()
            CLEAR()
            TXTNAME.Text = TEMPNAME

            If EDIT = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPID)
                ALPARAVAL.Add(YearId)
                Dim OBJSELECT As New ClsBlanketMaster
                OBJSELECT.ALPARAVAL = ALPARAVAL
                Dim dttable As DataTable = OBJSELECT.GETBLANKET()
                If dttable.Rows.Count > 0 Then
                    TXTNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    TEMPNAME = dttable.Rows(0).Item("NAME").ToString

                    CMBPARTYNAME.Text = dttable.Rows(0).Item("PARTYNAME").ToString
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString
                End If
                'GRID
                Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.SEARCH(" BLANKETMASTER.BLANKET_ID AS TEMPID, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN ", "", "  BLANKETMASTER INNER JOIN BLANKETMASTER_DESIGN ON BLANKETMASTER.BLANKET_ID = BLANKETMASTER_DESIGN.BLANKET_ID AND BLANKETMASTER.BLANKET_YEARID = BLANKETMASTER_DESIGN.BLANKET_YEARID LEFT OUTER JOIN DESIGNMASTER ON BLANKETMASTER_DESIGN.BLANKET_DESIGNID = DESIGNMASTER.DESIGN_id ", " AND BLANKETMASTER_DESIGN.BLANKET_ID = " & TEMPID & " AND BLANKETMASTER_DESIGN.BLANKET_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR1 As DataRow In dttable.Rows
                        GRIDDESIGN.Rows.Add(DTR1("DESIGN"))
                    Next
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim tempmsg As Integer = MsgBox("Delete Blanket Permanently?", MsgBoxStyle.YesNo, "TEXTRADE")
                If tempmsg = vbYes Then

                    Dim OBJBLANKET As New ClsBlanketMaster
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPID)
                    ALPARAVAL.Add(YearId)
                    OBJBLANKET.ALPARAVAL = ALPARAVAL
                    Dim INTRES As Integer = OBJBLANKET.DELETE()
                    EDIT = False
                    CLEAR()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            EDIT = False
            TXTNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(sender As Object, e As EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, CMBDESIGN.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLDESIGNGRID()
        Try
            If GRIDDESIGNDOUBLECLICK = False Then
                GRIDDESIGN.Rows.Add(CMBDESIGN.Text.Trim)
            ElseIf GRIDDESIGNDOUBLECLICK = True Then
                GRIDDESIGN.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
                GRIDDESIGNDOUBLECLICK = False
            End If
            GRIDDESIGN.FirstDisplayedScrollingRowIndex = GRIDDESIGN.RowCount - 1
            CMBDESIGN.Text = ""
            CMBDESIGN.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDDESIGN.CurrentRow.Index >= 0 And GRIDDESIGN.Item(GDESIGN.Index, GRIDDESIGN.CurrentRow.Index).Value <> Nothing Then
                GRIDDESIGNDOUBLECLICK = True
                CMBDESIGN.Text = GRIDDESIGN.Item(GDESIGN.Index, GRIDDESIGN.CurrentRow.Index).Value
                TEMPROW = GRIDDESIGN.CurrentRow.Index
                CMBDESIGN.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validated(sender As Object, e As EventArgs) Handles CMBDESIGN.Validated
        Try
            If CMBDESIGN.Text.Trim = "" Then Exit Sub
            If CMBPARTYNAME.Text.Trim = "" Then
                Ep.Clear()

                ' search duplication 
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable = OBJCMN.SEARCH(" ISNULL(BLANKETMASTER.BLANKET_NAME, '') AS NAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN", "", " BLANKETMASTER INNER JOIN BLANKETMASTER_DESIGN ON BLANKETMASTER.BLANKET_ID = BLANKETMASTER_DESIGN.BLANKET_ID AND BLANKETMASTER.BLANKET_YEARID = BLANKETMASTER_DESIGN.BLANKET_YEARID LEFT OUTER JOIN DESIGNMASTER ON BLANKETMASTER_DESIGN.BLANKET_DESIGNID = DESIGNMASTER.DESIGN_id ", " AND ISNULL(BLANKETMASTER.BLANKET_PARTYNAMEID,0) = 0 AND DESIGNMASTER.DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' AND  BLANKETMASTER.BLANKET_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    Ep.SetError(CMBDESIGN, "DESIGN Already Exists in " & dt.Rows(0).Item("NAME") & " Blanket ")
                    Exit Sub
                End If
            End If
            FILLDESIGNGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        If CMBPARTYNAME.Text.Trim = "" Then FILLNAME(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        FILLDESIGN(CMBDESIGN, CMBDESIGN.Text.Trim)
    End Sub

    Private Sub GRIDDESIGN_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDDESIGN.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub CMBDESIGN_Validating(sender As Object, e As CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me)
            For Each ROW As DataGridViewRow In GRIDDESIGN.Rows
                If GRIDDESIGNDOUBLECLICK = False Then
                    If LCase(CMBDESIGN.Text.Trim) = LCase(ROW.Cells(GDESIGN.Index).Value) Then
                        Ep.SetError(CMBDESIGN, "Design is already present in Grid ")
                        e.Cancel = True
                        Exit Sub
                    End If
                ElseIf GRIDDESIGNDOUBLECLICK = True Then
                    If CMBDESIGN.Text.Trim = ROW.Cells(GDESIGN.Index).Value And TEMPROW <> ROW.Index Then
                        Ep.SetError(CMBDESIGN, "Design is already present in Grid ")
                        e.Cancel = True
                        Exit Sub
                    End If
                End If

            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDDESIGN_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDDESIGN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDDESIGN.RowCount > 0 Then
                If GRIDDESIGNDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDDESIGN.Rows.RemoveAt(GRIDDESIGN.CurrentRow.Index)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Enter(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then FILLNAME(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            NAMEVALIDATE(CMBPARTYNAME, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class