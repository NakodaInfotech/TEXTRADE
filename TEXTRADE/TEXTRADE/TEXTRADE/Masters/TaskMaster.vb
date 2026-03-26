Imports System.ComponentModel
Imports System.Net
Imports BL
Imports DevExpress.XtraScheduler.Drawing

Public Class TaskMaster
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Public frmString As String       'Used for form Category or GRade
    Public TempName As String        'Used for tempname while edit mode
    Public TempID As Integer         'Used for tempname while edit mode
    Public EDIT As Boolean           'Used for edit
    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If CMBTASKNAME.Text.Trim.Length = 0 Then
            Ep.SetError(CMBTASKNAME, "Fill Task Name")
            bln = False
        End If
        Return bln
    End Function

    Sub clear()
        CMBTASKNAME.Text = ""
        txtremarks.Clear()
    End Sub

    Private Sub CMDDELETE_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Try
            If EDIT = False Then Exit Sub
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If MsgBox("Wish to Delete?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable

                DT = OBJCMN.Execute_Any_String("DELETE FROM TASKMASTER WHERE TASK_name = '" & TempName & "' AND TASK_YEARID= " & YearId, "", "")
                MsgBox("Entry Deleted Successfully")
                EDIT = False
                clear()

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub TaskMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt = True And e.KeyCode = Windows.Forms.Keys.S Then       'for Saving
            Call cmdok_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D Then       'for Saving
            Call CMDDELETE_Click(sender, e)
        ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub TaskMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim dttable As New DataTable
            Dim objCommon As New ClsCommonMaster

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
            USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)
            Me.Text = "Task Master"
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If EDIT = True Then dttable = objCommon.search(" TASK_name, TASK_REMARKS", "", "TaskMaster", " and Task_id = " & TempID & " and Task_cmpid = " & CmpId & " and Task_locationid = " & Locationid & " and Task_yearid = " & YearId)


            CMBTASKNAME.Text = TempName

            If dttable.Rows.Count > 0 Then
                CMBTASKNAME.Text = dttable.Rows(0).Item(0).ToString
                CMBTASKTYPE.Text = dttable.Rows(0).Item(0).ToString
                txtremarks.Text = dttable.Rows(0).Item(1).ToString
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try

            Ep.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(UCase(CMBTASKNAME.Text.Trim))
            alParaval.Add(UCase(CMBTASKTYPE.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim objclscategorymaster As New ClsTaskMaster
            objclscategorymaster.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objclscategorymaster.save()
                MsgBox("Details Added")
            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TempID)
                IntResult = objclscategorymaster.Update()
                MsgBox("Details Updated")
                EDIT = False

            End If

            clear()
            CMBTASKNAME.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTASKNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBTASKNAME.Validating
        Try
            If CMBTASKNAME.Text.Trim <> "" Then
                'for search
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                If (EDIT = False) Or (EDIT = True And LCase(TempName) <> LCase(CMBTASKNAME.Text.Trim)) Then
                    dt = objclscommon.search("TASK_name", "", "TaskMaster", " and Task_name = '" & CMBTASKNAME.Text.Trim & "' and Task_cmpid =" & CmpId & " and Task_Locationid =" & Locationid & " and Task_Yearid =" & YearId)
                    If dt.Rows.Count > 0 Then
                            MsgBox("Task Name Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                            e.Cancel = True
                        End If

                    End If

                    uppercase(CMBTASKNAME)
                End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Dim objclscommon As New ClsCommonMaster
        Dim dt As DataTable

        dt = objclscommon.search("TASK_name", "", "TaskMaster", " and TASK_cmpid =" & CmpId & " and TASK_Locationid =" & Locationid & " and TASK_Yearid =" & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "TASK_name"
            CMBTASKNAME.DisplayMember = "TASK_name"
            CMBTASKNAME.Text = ""
        End If
        CMBTASKNAME.DataSource = dt
        CMBTASKNAME.SelectedIndex = -1
    End Sub

    Private Sub CMBTASKNAME_Enter(sender As Object, e As EventArgs) Handles CMBTASKNAME.Enter
        Try
            If CMBTASKNAME.Text.Trim = "" Then FILLTASK(CMBTASKNAME, EDIT, "")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class