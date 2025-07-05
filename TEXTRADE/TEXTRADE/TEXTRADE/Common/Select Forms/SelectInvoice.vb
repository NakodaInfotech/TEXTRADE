Imports BL
Public Class SelectInvoice
    Public PARTYNAME As String = ""
    Public AGENTNAME As String = ""
    Public FRMSTRING As String = ""
    Public DT1 As New DataTable
    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectInvoice_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = 100
        fillgrid()
        gridbilldetails.ForceInitialize()
        gridbill.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
    End Sub
    Sub fillgrid(Optional ByVal where As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If PARTYNAME <> "" Then
                where = where & " AND LEDGERS.Acc_cmpname='" & PARTYNAME & "'"
            Else
                where = where & " AND AGENTLEDGERS.Acc_cmpname='" & AGENTNAME & "'"
            End If

            Dim objclspreq As New ClsCommon()
            Dim DT1 As New DataTable
            'THIS CODE IS FOR SELECT CHALLAN ON GATEPASS
            If FRMSTRING = "SELECTINVOICE" Then
                DT1 = objclspreq.SEARCH(" CAST(0 AS BIT) AS CHK, INVOICEMASTER.INVOICE_NO AS INVNO, INVOICEMASTER.INVOICE_INITIALS AS INVINITIALS, INVOICEMASTER.INVOICE_DATE AS INVDATE, INVOICEMASTER.INVOICE_PRINTINITIALS AS PRINTINITIALS, INVOICEMASTER.INVOICE_LRNO AS LRNO, ISNULL(TRASLEDGERS.Acc_cmpname, '') AS TRANSPORT, INVOICEMASTER.INVOICE_LRDATE AS LRDATE, REGISTERMASTER.register_name AS REGNAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME ", "", " INVOICEMASTER LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRASLEDGERS ON INVOICEMASTER.INVOICE_TRANSID = TRASLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON INVOICEMASTER.INVOICE_YEARID = LEDGERS.Acc_yearid AND INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_YEARID = REGISTERMASTER.register_yearid AND INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id ", " AND INVOICEMASTER.INVOICE_COVERNOTEDONE = " & 0 & " AND INVOICEMASTER.INVOICE_YEARID = " & YearId & where & " ORDER BY INVOICE_NO ")
            End If
            gridbilldetails.DataSource = DT1

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            DT1.Columns.Add("INVNO")
            DT1.Columns.Add("INVINITIALS")
            DT1.Columns.Add("INVDATE")
            DT1.Columns.Add("PRINTINITIALS")
            DT1.Columns.Add("LRNO")
            DT1.Columns.Add("TRANSPORT")
            DT1.Columns.Add("REGNAME")

            Dim TEMPGDNNO As String = ""
            Dim TEMPALLOW As Boolean = False
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)

                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT1.Rows.Add(Val(dtrow("INVNO")), dtrow("REGNAME"), dtrow("INVDATE"), dtrow("INVINITIALS"), dtrow("PRINTINITIALS"), dtrow("LRNO"), dtrow("TRANSPORT"))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub chkall_CheckedChanged(sender As Object, e As EventArgs) Handles chkall.CheckedChanged
        Try
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                dtrow("CHK") = chkall.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class