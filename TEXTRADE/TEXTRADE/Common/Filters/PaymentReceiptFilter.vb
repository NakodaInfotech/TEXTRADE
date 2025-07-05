
Imports BL

Public Class PaymentReceiptFilter

    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
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

            If FRMSTRING = "PAYMENT" Then

                'FOR PARTYNAME
                gridbill.ClearColumnsFilter()
                Dim NAMECLAUSE As String = ""
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then
                            NAMECLAUSE = " AND ({LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                        Else
                            NAMECLAUSE = NAMECLAUSE & " OR {LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                        End If
                    End If
                Next
                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                End If


                Dim OBJPAY As New payment_advice
                OBJPAY.MdiParent = MDIMain
                OBJPAY.WHERECLAUSE = " {PAYMENTMASTER.PAYMENT_YEARID} = " & YearId
                If chkdate.Checked = True Then
                    getFromToDate()
                    OBJPAY.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                    OBJPAY.WHERECLAUSE = OBJPAY.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
                Else
                    OBJPAY.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
                End If

                If NAMECLAUSE <> "" Then OBJPAY.WHERECLAUSE = OBJPAY.WHERECLAUSE & NAMECLAUSE
                If CMBNAME.Text.Trim <> "" Then OBJPAY.WHERECLAUSE = OBJPAY.WHERECLAUSE & " AND {ACCLEDGERS.ACC_CMPNAME} = '" & CMBNAME.Text.Trim & "'"
                If CMBREGISTER.Text.Trim <> "" Then OBJPAY.WHERECLAUSE = OBJPAY.WHERECLAUSE & " AND {REGISTERMASTER.REGISTER_NAME} = '" & CMBREGISTER.Text.Trim & "'"
                If CMBGROUPNAME.Text.Trim <> "" Then OBJPAY.WHERECLAUSE = OBJPAY.WHERECLAUSE & " AND {GROUPMASTER.GROUP_NAME} = '" & CMBGROUPNAME.Text.Trim & "'"

                If RDBMONTHLY.Checked = True Then
                    OBJPAY.FRMSTRING = "PAYMONTHLY"
                    OBJPAY.PERIOD = "PAYMENT MONTHLY REPORT " & OBJPAY.PERIOD
                ElseIf RDBPARTYWISESUMM.Checked = True Then
                    OBJPAY.FRMSTRING = "PAYPARTYSUMM"
                    OBJPAY.PERIOD = "PARTY WISE SUMMARY REPORT " & OBJPAY.PERIOD
                ElseIf RDBREGISTER.Checked = True Then
                    OBJPAY.FRMSTRING = "PAYREGISTER"
                    OBJPAY.PERIOD = "PAYMENT REGISTER " & OBJPAY.PERIOD
                    OBJPAY.WHERECLAUSE = OBJPAY.WHERECLAUSE.Replace("{LEDGERS.ACC_CMPNAME}", "{PAYMENT_REPORT.NAME}")
                    OBJPAY.WHERECLAUSE = OBJPAY.WHERECLAUSE.Replace("REGISTERMASTER.REGISTER_NAME", "PAYMENT_REPORT.REGNAME")
                    OBJPAY.WHERECLAUSE = OBJPAY.WHERECLAUSE.Replace("GROUPMASTER.GROUP_NAME", "PAYMENT_REPORT.PARTYGROUP")
                    OBJPAY.WHERECLAUSE = OBJPAY.WHERECLAUSE.Replace("ACCLEDGERS.ACC_CMPNAME", "PAYMENT_REPORT.ACCNAME")
                    OBJPAY.WHERECLAUSE = OBJPAY.WHERECLAUSE.Replace("PAYMENTMASTER.PAYMENT_YEARID", "PAYMENT_REPORT.YEARID")
                End If

                OBJPAY.SHOWNARR = CHKNARR.CheckState
                OBJPAY.Show()

            Else


                'FOR PARTYNAME
                gridbill.ClearColumnsFilter()
                Dim NAMECLAUSE As String = ""
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then
                            NAMECLAUSE = " AND ({LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                        Else
                            NAMECLAUSE = NAMECLAUSE & " OR {LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                        End If
                    End If
                Next
                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                End If

                Dim OBJREC As New receipt_advice
                OBJREC.MdiParent = MDIMain
                OBJREC.WHERECLAUSE = " {RECEIPTMASTER.RECEIPT_YEARID} = " & YearId
                If chkdate.Checked = True Then
                    getFromToDate()
                    OBJREC.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                    OBJREC.WHERECLAUSE = OBJREC.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
                Else
                    OBJREC.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
                End If

                If NAMECLAUSE <> "" Then OBJREC.WHERECLAUSE = OBJREC.WHERECLAUSE & NAMECLAUSE
                If CMBNAME.Text.Trim <> "" Then OBJREC.WHERECLAUSE = OBJREC.WHERECLAUSE & " AND {ACCLEDGERS.ACC_CMPNAME} = '" & CMBNAME.Text.Trim & "'"
                If CMBREGISTER.Text.Trim <> "" Then OBJREC.WHERECLAUSE = OBJREC.WHERECLAUSE & " AND {REGISTERMASTER.REGISTER_NAME} = '" & CMBREGISTER.Text.Trim & "'"
                If CMBGROUPNAME.Text.Trim <> "" Then OBJREC.WHERECLAUSE = OBJREC.WHERECLAUSE & " AND {GROUPMASTER.GROUP_NAME} = '" & CMBGROUPNAME.Text.Trim & "'"

                If RDBMONTHLY.Checked = True Then
                    OBJREC.FRMSTRING = "RECMONTHLY"
                    OBJREC.PERIOD = "RECEIPT MONTHLY REPORT " & OBJREC.PERIOD
                ElseIf RDBPARTYWISESUMM.Checked = True Then
                    OBJREC.FRMSTRING = "RECPARTYSUMM"
                    OBJREC.PERIOD = "PARTY WISE SUMMARY REPORT " & OBJREC.PERIOD
                ElseIf RDBREGISTER.Checked = True Then
                    OBJREC.FRMSTRING = "RECREGISTER"
                    OBJREC.PERIOD = "RECEIPT REGISTER " & OBJREC.PERIOD
                    OBJREC.WHERECLAUSE = OBJREC.WHERECLAUSE.Replace("LEDGERS.ACC_CMPNAME", "RECEIPT_REPORT.NAME")
                    OBJREC.WHERECLAUSE = OBJREC.WHERECLAUSE.Replace("REGISTERMASTER.REGISTER_NAME", "RECEIPT_REPORT.REGNAME")
                    OBJREC.WHERECLAUSE = OBJREC.WHERECLAUSE.Replace("GROUPMASTER.GROUP_NAME", "RECEIPT_REPORT.PARTYGROUP")
                    OBJREC.WHERECLAUSE = OBJREC.WHERECLAUSE.Replace("ACCLEDGERS.ACC_CMPNAME", "RECEIPT_REPORT.ACCNAME")
                    OBJREC.WHERECLAUSE = OBJREC.WHERECLAUSE.Replace("RECEIPTMASTER.RECEIPT_YEARID", "RECEIPT_REPORT.YEARID")
                End If

                OBJREC.SHOWNARR = CHKNARR.CheckState
                OBJREC.Show()

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, GROUPMASTER.GROUP_SECONDARY AS GROUPNAME ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id ", " AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                dtrow("CHK") = CHKSELECTALL.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PaymentReceiptFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PaymentReceiptFilter_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If FRMSTRING = "PAYMENT" Then
                Me.Text = "Payment Filter"
                fillregister(cmbregister, " and register_type = 'PAYMENT'")
            Else
                Me.Text = "Receipt Filter"
                fillregister(cmbregister, " and register_type = 'RECEIPT'")
            End If

            FILLGRID()

            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, " and (groupmaster.group_secondary = 'BANK A/C' OR groupmaster.group_secondary = 'BANK OD A/C' OR groupmaster.group_secondary = 'CASH IN HAND') and acc_YEARid = " & YearId)
            If CMBGROUPNAME.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("GROUP_NAME AS GROUPNAME", "", "GROUPMASTER", " AND GROUP_YEARID = " & YearId)
                DT.DefaultView.Sort = "GROUPNAME"
                CMBGROUPNAME.DataSource = DT
                CMBGROUPNAME.DisplayMember = "GROUPNAME"
                CMBGROUPNAME.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class


