
Imports BL

Public Class CutPackFilter

    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Public Sub New()
        InitializeComponent()
        FILLCMB()
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBCONTRACTOR.Text.Trim = "" Then FILLCONTRACT(CMBCONTRACTOR)
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, DTFROM.Value)
        a2 = DatePart(DateInterval.Month, DTFROM.Value)
        a3 = DatePart(DateInterval.Year, DTFROM.Value)
        fromD = "(" & a3 & "," & a2 & "," & a1 & ")"

        a11 = DatePart(DateInterval.Day, DTTO.Value)
        a12 = DatePart(DateInterval.Month, DTTO.Value)
        a13 = DatePart(DateInterval.Year, DTTO.Value)
        toD = "(" & a13 & "," & a12 & "," & a11 & ")"
    End Sub

    Private Sub cmdshow_Click(sender As Object, e As EventArgs) Handles CMDSHOW.Click
        Try
            Dim OBJCUTPACK As New JobOutDesign
            OBJCUTPACK.MdiParent = MDIMain
            OBJCUTPACK.WHERECLAUSE = "{CUTPACKVIEW.YEARID}=" & YearId

            If CHKDATE.Checked = True Then
                getFromToDate()
                OBJCUTPACK.PERIOD = Format(DTFROM.Value, "dd/MM/yyyy") & " - " & Format(DTTO.Value, "dd/MM/yyyy")
                OBJCUTPACK.WHERECLAUSE = OBJCUTPACK.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
            Else
                OBJCUTPACK.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If

            If CMBCONTRACTOR.Text <> "" Then OBJCUTPACK.WHERECLAUSE = OBJCUTPACK.WHERECLAUSE & " and {CUTPACKVIEW.CONTRACTOR}='" & CMBCONTRACTOR.Text.Trim & "'"
            If CMBITEMNAME.Text <> "" Then OBJCUTPACK.WHERECLAUSE = OBJCUTPACK.WHERECLAUSE & " and {CUTPACKVIEW.ITEMNAME}='" & CMBITEMNAME.Text.Trim & "'"

            If RDBITEM.Checked = True Then
                OBJCUTPACK.FRMSTRING = "CUTPACKITEM"
                OBJCUTPACK.PERIOD = "CUTPACK - ITEMWISE " & OBJCUTPACK.PERIOD

            ElseIf RDBLOT.Checked = True Then
                OBJCUTPACK.FRMSTRING = "CUTPACKLOT"
                OBJCUTPACK.PERIOD = "CUTPACK - LOTWISE " & OBJCUTPACK.PERIOD

            ElseIf RDBCONTRACTOR.Checked = True Then
                OBJCUTPACK.FRMSTRING = "CUTPACKCONTRACTOR"
                OBJCUTPACK.PERIOD = "CUTPACK - CONTRACTORWISE " & OBJCUTPACK.PERIOD

            End If
            OBJCUTPACK.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CutPackFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Private Sub CMBCONTRACTOR_Enter(sender As Object, e As EventArgs) Handles CMBCONTRACTOR.Enter
        Try
            If CMBCONTRACTOR.Text.Trim = "" Then FILLCONTRACT(CMBCONTRACTOR)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class