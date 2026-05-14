Imports System.ComponentModel
Imports BL
Public Class AgencyInvoiceFilter
    Dim edit As Boolean
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String
    Public FRMSTRING As String

    Public Sub New()
        Try
            ' This call is required by the designer.
            InitializeComponent()
            FILLCMB()
            ' Add any initialization after the InitializeComponent() call.
        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub AgencyInvoiceFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Sub FILLCMB()
        Try
            If FRMSTRING = "SO" Then
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If

            If CMBAGENT.Text.Trim = "" Then FILLNAME(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry CREDITORS' AND ACC_TYPE='AGENT'")
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, edit, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE='TRANSPORT'")
            If CMBDISPATCHEDTO.Text.Trim = "" Then filljobbername(CMBDISPATCHEDTO, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            If CMBUNIT.Text.Trim = "" Then fillunit(CMBUNIT)
            If CMBCITYNAME.Text.Trim = "" Then fillCITY(CMBCITYNAME, False)

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AgencyInvoiceFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, dtfrom.Value)
        a2 = DatePart(DateInterval.Month, dtfrom.Value)
        a3 = DatePart(DateInterval.Year, dtfrom.Value)
        fromD = "(" & a3 & "," & a2 & "," & a1 & ")"

        a11 = DatePart(DateInterval.Day, dtto.Value)
        a12 = DatePart(DateInterval.Month, dtto.Value)
        a13 = DatePart(DateInterval.Year, dtto.Value)
        toD = "(" & a13 & "," & a12 & "," & a11 & ")"
    End Sub

    Private Sub cmdshow_Click(sender As Object, e As EventArgs) Handles cmdshow.Click
        Try
            Dim OBJGRN As New SaleInvoiceDesign
            OBJGRN.MdiParent = MDIMain
            OBJGRN.WHERECLAUSE = " {AGENCYINVOICEMASTER.AINVOICE_yearid}=" & YearId
            If RDBDATEWISE.Checked = True Then
                OBJGRN.FRMSTRING = "REGDATEWISE"
            ElseIf RBSELLERCOMM.Checked = True Then
                OBJGRN.FRMSTRING = "SELLERCOMM"
            End If

            If chkdate.Checked = True Then
                getFromToDate()
                OBJGRN.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
            Else
                OBJGRN.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If
            OBJGRN.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class