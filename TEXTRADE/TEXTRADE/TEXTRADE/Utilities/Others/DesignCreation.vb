
Imports BL
Imports System.IO
Imports System.ComponentModel

Public Class DesignCreation

    Public EDIT As Boolean              'Used for edit
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim DESIGNNO As String

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBDESIGNERNAME.Focus()
    End Sub

    Sub CLEAR()
        Try
            CMBDESIGNERNAME.Text = ""
            TXTDESIGNCODE.Clear()
            TXTFROM.Clear()
            TXTTO.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Ep.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If
            Dim IntResult As Integer
            Dim BLN As Boolean = True

            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) < Val(TXTTO.Text.Trim) Then
                    If MsgBox("Wish to Save Designs from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                        DESIGNNO = "VI-" & TXTDESIGNCODE.Text.Trim & "_" & I
                        TXTDESIGNNO.Text = DESIGNNO
                        If Not CHECKDUPLICATE() Then
                            Exit Sub
                        End If

                        Dim alParaval As New ArrayList

                        alParaval.Add(UCase(DESIGNNO))
                        alParaval.Add("")
                        alParaval.Add("")
                        alParaval.Add(Val(0))
                        alParaval.Add(Val(0))
                        alParaval.Add(Val(0))
                        alParaval.Add(0)

                        alParaval.Add(Val(0))
                        alParaval.Add(Val(0))
                        alParaval.Add(Val(0))
                        alParaval.Add(Val(0))
                        alParaval.Add(Val(0))
                        alParaval.Add(Val(0))
                        alParaval.Add("")
                        alParaval.Add(0)

                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        alParaval.Add(DBNull.Value)

                        Dim gridsrno As String = ""
                        Dim BASE As String = ""
                        Dim PRINT As String = ""
                        Dim COLOR As String = ""
                        Dim BLOCKED As String = ""

                        alParaval.Add(gridsrno)
                        alParaval.Add(BASE)
                        alParaval.Add(PRINT)
                        alParaval.Add(COLOR)
                        alParaval.Add(BLOCKED)

                        alParaval.Add("")
                        alParaval.Add("")
                        alParaval.Add("")
                        alParaval.Add(CMBDESIGNERNAME.Text.Trim)

                        Dim objDESIGN As New ClsDesignMaster
                        objDESIGN.alParaval = alParaval


                        If EDIT = False Then
                            If USERADD = False Then
                                MsgBox("Insufficient Rights")
                                Exit Sub
                            End If
                            IntResult = objDESIGN.SAVE()
                        End If
                    Next
                End If
            Else
                MsgBox("Enter Sr No ", MsgBoxStyle.Critical)
            End If
            EDIT = False

            CLEAR()
            EDIT = False
            CMBDESIGNERNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CHECKDUPLICATE() As Boolean
        Try
            Dim BLN As Boolean = True
            If TXTDESIGNNO.Text.Trim <> "" Then
                pcase(TXTDESIGNNO)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("DESIGN_NO", "", " DESIGNMASTER ", " And DESIGN_NO = '" & TXTDESIGNNO.Text.Trim & "' and DESIGN_cmpid = " & CmpId & " AND DESIGN_LOCATIONID = " & Locationid & " AND DESIGN_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Design No " & TXTDESIGNNO.Text.Trim & " Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                    BLN = False
                End If
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True
        If CMBDESIGNERNAME.Text.Trim.Length = 0 Then
            Ep.SetError(CMBDESIGNERNAME, "Fill Designer Name")
            bln = False
        End If

        If Val(TXTFROM.Text.Trim) = 0 Then
            Ep.SetError(TXTFROM, "Enter From Design No")
            bln = False
        End If

        If Val(TXTTO.Text.Trim) = 0 Then
            Ep.SetError(TXTTO, "Enter To Design No")
            bln = False
        End If
        Return bln
    End Function

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMBDESIGNERNAME_Enter(sender As Object, e As EventArgs) Handles CMBDESIGNERNAME.Enter
        If CMBDESIGNERNAME.Text = "" Then fillDESIGNER(CMBDESIGNERNAME, "")
    End Sub

    Private Sub DesignCreation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'DESIGN MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillDESIGNER(CMBDESIGNERNAME, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNERNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBDESIGNERNAME.Validating
        Try
            If CMBDESIGNERNAME.Text.Trim <> "" Then DESIGNERVALIDATE(CMBDESIGNERNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTFROM.KeyPress, TXTTO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub CMBDESIGNERNAME_Validated(sender As Object, e As EventArgs) Handles CMBDESIGNERNAME.Validated
        Try
            If CMBDESIGNERNAME.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNER_DCODE, '') AS DESIGNCODE ", "", "DESIGNERMASTER ", " and DESIGNERMASTER.DESIGNER_name = '" & CMBDESIGNERNAME.Text.Trim & "' and DESIGNERMASTER.DESIGNER_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    If TXTDESIGNCODE.Text.Trim = "" Then TXTDESIGNCODE.Text = DT.Rows(0).Item("DESIGNCODE")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class