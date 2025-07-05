
Imports BL

Public Class ChallanTypeMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Public EDIT As Boolean
    Public TEMPCHALLANTYPE As String
    Dim TEMPINITIALS As String
    Public TEMPTYPEID As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            Ep.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(TXTCHALLANTYPE.Text.Trim)
            alParaval.Add(TXTINITIALS.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim OBJTYPE As New ClsChallanTypeMaster
            OBJTYPE.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = OBJTYPE.SAVE()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPTYPEID)
                IntResult = OBJTYPE.UPDATE()
                MsgBox("Details Updated")

            End If
            EDIT = False


            clear()
            TXTCHALLANTYPE.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If TXTCHALLANTYPE.Text.Trim.Length = 0 Then
            EP.SetError(TXTCHALLANTYPE, "Fill Challan Type")
            bln = False
        End If

        If TXTINITIALS.Text.Trim.Length = 0 Then
            EP.SetError(TXTINITIALS, "Fill Challan Initials")
            bln = False
        End If
        Return bln
    End Function

    Sub clear()
        TXTCHALLANTYPE.Clear()
        TXTINITIALS.Clear()
        EDIT = False
    End Sub

    Private Sub TXTCHALLANTYPE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHALLANTYPE.Validating
        Try
            If (EDIT = False) Or (EDIT = True And TEMPCHALLANTYPE <> TXTCHALLANTYPE.Text.Trim) Then
                'for search
                Dim objclscommon As New ClsCommon
                Dim dt As DataTable = objclscommon.search("CHALLANTYPE_NAME", "", "CHALLANTYPEMASTER", " and CHALLANTYPE_NAME = '" & TXTCHALLANTYPE.Text.Trim & "' and CHALLANTYPE_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Challan Type Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GroupMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then               'FOR EXIT
            Me.Close()
        End If
    End Sub

    Private Sub GroupMaster_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GDN'")
        USERADD = DTROW(0).Item(1)
        USEREDIT = DTROW(0).Item(2)
        USERVIEW = DTROW(0).Item(3)
        USERDELETE = DTROW(0).Item(4)

        If EDIT = True Then

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim objCommon As New ClsCommon
            Dim dttable As DataTable = objCommon.search("CHALLANTYPE_ID AS CHALLANID, CHALLANTYPE_name AS CHALLANTYPE, CHALLANTYPE_INITIALS AS INITIALS", "", "CHALLANTYPEMASTER ", " AND CHALLANTYPE_NAME = '" & TEMPCHALLANTYPE & "' and CHALLANTYPE_yearid = " & YearId)
            If dttable.Rows.Count > 0 Then
                TEMPTYPEID = dttable.Rows(0).Item("CHALLANID")
                TXTCHALLANTYPE.Text = dttable.Rows(0).Item("CHALLANTYPE")
                TEMPCHALLANTYPE = dttable.Rows(0).Item("CHALLANTYPE")
                TXTINITIALS.Text = dttable.Rows(0).Item("INITIALS")
                TEMPINITIALS = dttable.Rows(0).Item("INITIALS")
            End If
        End If
    End Sub

    Private Sub TXTINITIALS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTINITIALS.Validating
        Try
            If (EDIT = False) Or (EDIT = True And TEMPINITIALS <> TXTINITIALS.Text.Trim) Then
                'for search
                Dim objclscommon As New ClsCommon
                Dim dt As DataTable = objclscommon.search("CHALLANTYPE_INITIALS", "", "CHALLANTYPEMASTER", " and CHALLANTYPE_INITIALS = '" & TXTINITIALS.Text.Trim & "' and CHALLANTYPE_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Initials Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class