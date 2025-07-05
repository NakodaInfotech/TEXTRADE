

Imports System.ComponentModel
Imports BL

Public Class DesignerMaster
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean              'Used for edit
    Public TEMPNAME As String           'Used for edit name
    Public TEMPID As Integer            'Used for edit id

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(TXTNAME.Text.Trim)
            ALPARAVAL.Add(TXTDCODE.Text.Trim)
            ALPARAVAL.Add(TXTMOBILENO.Text.Trim)
            ALPARAVAL.Add(CMBEMAIL.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            Dim OBJADD As New ClsDesignerMaster
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
            TXTNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True
            If TXTNAME.Text.Trim.Length = 0 Then
                EP.SetError(TXTNAME, "Fill Name")
                BLN = False
            Else
                If (EDIT = False) Or (EDIT = True And LCase(TEMPNAME) <> LCase(TXTNAME.Text.Trim)) Then
                    ' search duplication 
                    Dim OBJCMN As New ClsCommon
                    Dim dt As New DataTable
                    dt = OBJCMN.SEARCH(" DESIGNER_NAME ", "", " DESIGNERMASTER ", " AND DESIGNER_NAME = '" & TXTNAME.Text.Trim & "' AND  DESIGNER_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        EP.SetError(TXTNAME, "Name Already Exists")
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
            EP.Clear()
            TXTNAME.Clear()
            TXTMOBILENO.Clear()
            CMBEMAIL.Text = ""
            TXTDCODE.Clear()
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
                    dt = OBJCMN.search(" DESIGNER_NAME ", "", " DESIGNERMASTER ", " AND DESIGNER_NAME = '" & TXTNAME.Text.Trim & "' AND  DESIGNER_YEARID = " & YearId)
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

    Private Sub SalesmanMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub SalesmanMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'DESIGN MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

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
                Dim OBJSELECT As New ClsDesignerMaster
                OBJSELECT.ALPARAVAL = ALPARAVAL
                Dim dttable As DataTable = OBJSELECT.GETDESIGNERMAN()
                If dttable.Rows.Count > 0 Then
                    TXTNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    TXTDCODE.Text = dttable.Rows(0).Item("DCODE").ToString
                    TXTMOBILENO.Text = dttable.Rows(0).Item("MOBILENO").ToString
                    CMBEMAIL.Text = dttable.Rows(0).Item("EMAIL").ToString
                    TEMPNAME = dttable.Rows(0).Item("NAME").ToString
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

                Dim tempmsg As Integer = MsgBox("Delete Designer Permanently?", MsgBoxStyle.YesNo, "TEXTRADE")
                If tempmsg = vbYes Then

                    Dim OBJSalesman As New ClsDesignerMaster
                    Dim ALPARAVAL As New ArrayList

                    ALPARAVAL.Add(TEMPID)
                    ALPARAVAL.Add(YearId)

                    OBJSalesman.ALPARAVAL = ALPARAVAL
                    Dim INTRES As Integer = OBJSalesman.DELETE()
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
    Sub emailvalidate(ByRef cmb As System.Windows.Forms.ComboBox)
        Try
            Dim intresult As Integer
            If cmb.Text.Trim <> "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("DESIGNER_EMAILID", "", "DESIGNERMASTER", " and DESIGNER_EMAILID = '" & CMBEMAIL.Text.Trim & "' and DESIGNER_CMPID = " & CmpId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Email Address not present, Add New?", MsgBoxStyle.YesNo, "Office Manager")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(cmb.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objEmailmaster As New ClsEmailMaster
                        objEmailmaster.alParaval = alParaval
                        intresult = objEmailmaster.save()
                        'e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBEMAIL_Validating(sender As Object, e As CancelEventArgs) Handles CMBEMAIL.Validating
        emailvalidate(CMBEMAIL)
    End Sub
    Sub fillcmb(ByRef cmb As System.Windows.Forms.ComboBox)
        Try
            If cmb.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("DESIGNER_EMAILID", "", "DESIGNERMASTER", " and DESIGNER_CMPID = " & CmpId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "DESIGNER_EMAILID"
                    cmb.DataSource = dt
                    cmb.DisplayMember = "DESIGNER_EMAILID"
                    cmb.Text = ""
                End If
                cmb.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBEMAIL_Enter(sender As Object, e As EventArgs) Handles CMBEMAIL.Enter
        Try
            fillcmb(CMBEMAIL)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class