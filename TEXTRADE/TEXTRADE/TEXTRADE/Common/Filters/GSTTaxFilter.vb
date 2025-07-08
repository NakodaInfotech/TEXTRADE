
Imports System.ComponentModel
Imports BL

Public Class GSTTaxFilter


    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GSTTaxFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.OemQuotes Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.S) Then
                cmdshow_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()

            If RBGSTR2MATCHREPORT.Checked = True Then
                Dim OBJGSTR2 As New GSTR2MatchGridReport
                OBJGSTR2.MdiParent = MDIMain
                If chkdate.CheckState = CheckState.Checked Then OBJGSTR2.WHERECLAUSE = OBJGSTR2.WHERECLAUSE & " AND T.INVDATEBOOKS >= '" & Format(Convert.ToDateTime(dtfrom.Text), "MM/dd/yyyy") & "' AND T.INVDATEBOOKS <= '" & Format(Convert.ToDateTime(dtto.Text), "MM/dd/yyyy") & "'" Else OBJGSTR2.WHERECLAUSE = OBJGSTR2.WHERECLAUSE & " AND T.INVDATEBOOKS >= '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND T.INVDATEBOOKS <= '" & Format(AccTo.Date, "MM/dd/yyyy") & "'"
                OBJGSTR2.Show()
                Exit Sub
            End If


            If RBRCMSELFINVOICE.Checked = True Then
                Dim OBJRCM As New RCMSelfInvoiceGSTDetails
                OBJRCM.MdiParent = MDIMain
                If chkdate.CheckState = CheckState.Checked Then OBJRCM.WHERECLAUSE = OBJRCM.WHERECLAUSE & " AND NONPURCHASE.NP_PARTYBILLDATE >= '" & Format(Convert.ToDateTime(dtfrom.Text), "MM/dd/yyyy") & "' AND NONPURCHASE.NP_PARTYBILLDATE <= '" & Format(Convert.ToDateTime(dtto.Text), "MM/dd/yyyy") & "'" Else OBJRCM.WHERECLAUSE = OBJRCM.WHERECLAUSE & " AND NONPURCHASE.NP_PARTYBILLDATE >= '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND NONPURCHASE.NP_PARTYBILLDATE <= '" & Format(AccTo.Date, "MM/dd/yyyy") & "'"
                OBJRCM.Show()
                Exit Sub
            End If


            If RBGSTSUMMARY.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Summary.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTSUMMARY_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTSUMMARY_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Summary.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTSUMMARY_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTSUMMARY_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Summary.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.subject = "GST SUMMARY"
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTSALEDETAILS.Checked = True Then

                If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    Dim OBJGST As New LineGSTHSNReport
                    OBJGST.MdiParent = MDIMain
                    OBJGST.Show()
                Else
                    If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        Dim OBJRPT As New clsReportDesigner("GST Sale Details", System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale Details.xlsx", 2)
                        If chkdate.CheckState = CheckState.Checked Then
                            OBJRPT.GSTSALEDETAILS_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim, ClientName)
                        Else
                            OBJRPT.GSTSALEDETAILS_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim, ClientName)
                        End If
                        Exit Sub
                    Else
                        Dim OBJRPT As New clsReportDesigner("GST Sale Details", System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale Details.xlsx", 0)
                        If chkdate.CheckState = CheckState.Checked Then
                            OBJRPT.GSTSALEDETAILS_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim, ClientName)
                        Else
                            OBJRPT.GSTSALEDETAILS_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim, ClientName)
                        End If

                        'MAIL EXCEL AS ATTACHMENTS
                        Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale Details.xlsx"
                        Dim objmail As New SendMail
                        objmail.attachment = TEMPATTACHMENT
                        objmail.subject = "GST SALE DETAILS"
                        objmail.Show()
                        objmail.BringToFront()
                        Windows.Forms.Cursor.Current = Cursors.Arrow

                        Exit Sub
                    End If
                End If

            ElseIf RBGSTPURCHASEDETAILS.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Purchase Details", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase Details.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTPURCHASEDETAILS_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTPURCHASEDETAILS_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Purchase Details", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase Details.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTPURCHASEDETAILS_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTPURCHASEDETAILS_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase Details.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.subject = "GST PURCHASE DETAILS"
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTCNDETAILS.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Sale Return Or Credit Note  Details", System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale Return Or Credit Note Details.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTCNDETAILS_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTCNDETAILS_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Sale Return Or Credit Note Details", System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale Return Or Credit Note Details.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTCNDETAILS_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTCNDETAILS_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale Return Or Credit Note Details.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.subject = "GST CN DETAILS"
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTDNDETAILS.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Purchase Return Or Debit Note Details", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase Return Or Debit Note Details.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTDNDETAILS_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTDNDETAILS_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Purchase Return Or Debit Note Details", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase Return Or Debit Note Details.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTDNDETAILS_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTDNDETAILS_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase Return Or Debit Note Details.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.subject = "GST DN DETAILS"
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTPARTYSALESUMM.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Sale Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale Summary.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTSALESUMM_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim, CMBNAME.Text.Trim)
                    Else
                        OBJRPT.GSTSALESUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim, CMBNAME.Text.Trim)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Sale Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale Summary.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTSALESUMM_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim, CMBNAME.Text.Trim)
                    Else
                        OBJRPT.GSTSALESUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim, CMBNAME.Text.Trim)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale Summary.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.subject = "GST SUMMARY"
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTPARTYPURSUMM.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Purchase Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase Summary.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTPURSUMM_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim, CMBNAME.Text.Trim)
                    Else
                        OBJRPT.GSTPURSUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim, CMBNAME.Text.Trim)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Purchase Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase Summary.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTPURSUMM_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim, CMBNAME.Text.Trim)
                    Else
                        OBJRPT.GSTPURSUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim, CMBNAME.Text.Trim)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase Summary.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.subject = "GST SUMMARY"
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTSTATEPURSUMM.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Purchase State Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase State Summary.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTPURSTATESUMM_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBSTATE.Text.Trim)
                    Else
                        OBJRPT.GSTPURSTATESUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBSTATE.Text.Trim)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Purchase State Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase State Summary.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTPURSTATESUMM_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBSTATE.Text.Trim)
                    Else
                        OBJRPT.GSTPURSTATESUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBSTATE.Text.Trim)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase State Summary.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.subject = "GST SUMMARY"
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTSTATESALESUMM.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Sale State Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale State Summary.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTSALESTATESUMM_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBSTATE.Text.Trim)
                    Else
                        OBJRPT.GSTSALESTATESUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBSTATE.Text.Trim)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Sale State Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale State Summary.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTSALESTATESUMM_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBSTATE.Text.Trim)
                    Else
                        OBJRPT.GSTSALESTATESUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBSTATE.Text.Trim)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale State Summary.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.subject = "GST SUMMARY"
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTPURHSNSUMM.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Purchase HSN Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase HSN Summary.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTPURHSNSUMM_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), INVOICESCREENTYPE, CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTPURHSNSUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, INVOICESCREENTYPE, CMBREGISTER.Text.Trim)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Purchase HSN Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase HSN Summary.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTPURHSNSUMM_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), INVOICESCREENTYPE, CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTPURHSNSUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, INVOICESCREENTYPE, CMBREGISTER.Text.Trim)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase HSN Summary.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.subject = "GST HSN SUMMARY"
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTHSNDETAILS.Checked = True Then

                Dim OBJHSN As New HSNWiseDetails
                OBJHSN.FRMSTRING = "SALE"
                If chkdate.CheckState = CheckState.Checked Then OBJHSN.WHERECLAUSE = OBJHSN.WHERECLAUSE & " And HSNSUMMARY.Date >='" & Format(Convert.ToDateTime(dtfrom.Text), "MM/dd/yyyy") & "' AND HSNSUMMARY.DATE <= '" & Format(Convert.ToDateTime(dtto.Text), "MM/dd/yyyy") & "'"
                OBJHSN.MdiParent = MDIMain
                OBJHSN.Show()

            ElseIf RBGSTHSNPURDETAILS.Checked = True Then

                Dim OBJHSN As New HSNWiseDetails
                OBJHSN.FRMSTRING = "PURCHASE"
                If chkdate.CheckState = CheckState.Checked Then OBJHSN.WHERECLAUSE = OBJHSN.WHERECLAUSE & " AND HSNPURSUMMARY.DATE >='" & Format(Convert.ToDateTime(dtfrom.Text), "MM/dd/yyyy") & "' AND HSNPURSUMMARY.DATE <= '" & Format(Convert.ToDateTime(dtto.Text), "MM/dd/yyyy") & "'"
                OBJHSN.MdiParent = MDIMain
                OBJHSN.Show()

            ElseIf RBGSTR1.Checked = True Then
                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then

                    'WE HAVE TO CREATE 1 WORKBOOK AND ALL OTHER FILES AS SHEETS
                    Dim OBJGSTR1 As New clsReportDesigner("GSTR1", System.AppDomain.CurrentDomain.BaseDirectory & "GSTR1.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then OBJGSTR1.GSTGSTR1_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), ClientName, CMBREGISTER.Text.Trim) Else OBJGSTR1.GSTGSTR1_EXCEL(CmpId, YearId, AccFrom, AccTo, ClientName, CMBREGISTER.Text.Trim)


                    'Dim OBJRPT As New clsReportDesigner("B2B", System.AppDomain.CurrentDomain.BaseDirectory & "B2B.xlsx", 2)
                    'Dim OBJRPTB2CL As New clsReportDesigner("B2CL", System.AppDomain.CurrentDomain.BaseDirectory & "B2CL.xlsx", 2)
                    'Dim OBJRPTB2CS As New clsReportDesigner("B2CS", System.AppDomain.CurrentDomain.BaseDirectory & "B2CS.xlsx", 2)
                    'Dim OBJRPTCDNR As New clsReportDesigner("CDNR", System.AppDomain.CurrentDomain.BaseDirectory & "CDNR.xlsx", 2)
                    'Dim OBJRPTCDNUR As New clsReportDesigner("CDNUR", System.AppDomain.CurrentDomain.BaseDirectory & "CDNUR.xlsx", 2)
                    'Dim OBJRPTHSNB2B As New clsReportDesigner("HSNB2B", System.AppDomain.CurrentDomain.BaseDirectory & "HSNB2B.xlsx", 2)
                    'Dim OBJRPTHSNB2C As New clsReportDesigner("HSNB2C", System.AppDomain.CurrentDomain.BaseDirectory & "HSNB2C.xlsx", 2)
                    'Dim OBJRPTDOCS As New clsReportDesigner("DOCS", System.AppDomain.CurrentDomain.BaseDirectory & "DOCS.xlsx", 2)
                    'If chkdate.CheckState = CheckState.Checked Then
                    '    OBJRPT.GSTB2B_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    '    OBJRPTB2CL.GSTB2CL_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    '    OBJRPTB2CS.GSTB2CS_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    '    OBJRPTCDNR.GSTCDNR_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    '    OBJRPTCDNUR.GSTCDNUR_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    '    OBJRPTHSNB2B.GSTHSNB2B_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), INVOICESCREENTYPE, ClientName, CMBREGISTER.Text.Trim)
                    '    OBJRPTHSNB2C.GSTHSNB2C_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), INVOICESCREENTYPE, ClientName, CMBREGISTER.Text.Trim)
                    '    OBJRPTDOCS.GSTDOCS_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), ClientName)
                    'Else
                    '    OBJRPT.GSTB2B_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    '    OBJRPTB2CL.GSTB2CL_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    '    OBJRPTB2CS.GSTB2CS_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    '    OBJRPTCDNR.GSTCDNR_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    '    OBJRPTCDNUR.GSTCDNUR_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    '    OBJRPTHSNB2B.GSTHSNB2B_EXCEL(CmpId, YearId, AccFrom, AccTo, INVOICESCREENTYPE, ClientName, CMBREGISTER.Text.Trim)
                    '    OBJRPTHSNB2C.GSTHSNB2C_EXCEL(CmpId, YearId, AccFrom, AccTo, INVOICESCREENTYPE, ClientName, CMBREGISTER.Text.Trim)
                    '    OBJRPTDOCS.GSTDOCS_EXCEL(CmpId, YearId, AccFrom, AccTo, ClientName)
                    'End If
                    Exit Sub
                Else

                    'WE HAVE TO CREATE 1 WORKBOOK AND ALL OTHER FILES AS SHEETS
                    Dim OBJGSTR1 As New clsReportDesigner("GSTR1", System.AppDomain.CurrentDomain.BaseDirectory & "GSTR1.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then OBJGSTR1.GSTGSTR1_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), ClientName, CMBREGISTER.Text.Trim) Else OBJGSTR1.GSTGSTR1_EXCEL(CmpId, YearId, AccFrom, AccTo, ClientName, CMBREGISTER.Text.Trim)

                    'Dim OBJRPT As New clsReportDesigner("B2B", System.AppDomain.CurrentDomain.BaseDirectory & "B2B.xlsx", 0)
                    'Dim OBJRPTB2CL As New clsReportDesigner("B2CL", System.AppDomain.CurrentDomain.BaseDirectory & "B2CL.xlsx", 0)
                    'Dim OBJRPTB2CS As New clsReportDesigner("B2CS", System.AppDomain.CurrentDomain.BaseDirectory & "B2CS.xlsx", 0)
                    'Dim OBJRPTCDNR As New clsReportDesigner("CDNR", System.AppDomain.CurrentDomain.BaseDirectory & "CDNR.xlsx", 0)
                    'Dim OBJRPTCDNUR As New clsReportDesigner("CDNUR", System.AppDomain.CurrentDomain.BaseDirectory & "CDNUR.xlsx", 0)
                    'Dim OBJRPTHSNB2B As New clsReportDesigner("HSNB2B", System.AppDomain.CurrentDomain.BaseDirectory & "HSNB2B.xlsx", 0)
                    'Dim OBJRPTHSNB2C As New clsReportDesigner("HSNB2C", System.AppDomain.CurrentDomain.BaseDirectory & "HSNB2C.xlsx", 0)
                    'Dim OBJRPTDOCS As New clsReportDesigner("DOCS", System.AppDomain.CurrentDomain.BaseDirectory & "DOCS.xlsx", 0)
                    'If chkdate.CheckState = CheckState.Checked Then
                    '    OBJRPT.GSTB2B_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    '    OBJRPTB2CL.GSTB2CL_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    '    OBJRPTB2CS.GSTB2CS_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    '    OBJRPTCDNR.GSTCDNR_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    '    OBJRPTCDNUR.GSTCDNUR_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), CMBREGISTER.Text.Trim)
                    '    OBJRPTHSNB2B.GSTHSNB2B_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), INVOICESCREENTYPE, ClientName, CMBREGISTER.Text.Trim)
                    '    OBJRPTHSNB2C.GSTHSNB2C_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), INVOICESCREENTYPE, ClientName, CMBREGISTER.Text.Trim)
                    '    OBJRPTDOCS.GSTDOCS_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), ClientName)
                    'Else
                    '    OBJRPT.GSTB2B_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    '    OBJRPTB2CL.GSTB2CL_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    '    OBJRPTB2CS.GSTB2CS_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    '    OBJRPTCDNR.GSTCDNR_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    '    OBJRPTCDNUR.GSTCDNUR_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                    '    OBJRPTHSNB2B.GSTHSNB2B_EXCEL(CmpId, YearId, AccFrom, AccTo, INVOICESCREENTYPE, ClientName, CMBREGISTER.Text.Trim)
                    '    OBJRPTHSNB2C.GSTHSNB2C_EXCEL(CmpId, YearId, AccFrom, AccTo, INVOICESCREENTYPE, ClientName, CMBREGISTER.Text.Trim)
                    '    OBJRPTDOCS.GSTDOCS_EXCEL(CmpId, YearId, AccFrom, AccTo, ClientName)
                    'End If
                    'MAIL EXCEL AS ATTACHMENTS

                    Dim objmail As New SendMail
                    objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "GSTR1.xlsx")
                    'objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "B2B.xlsx")
                    'objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "B2CL.xlsx")
                    'objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "B2CS.xlsx")
                    'objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "CDNR.xlsx")
                    'objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "CDNUR.xlsx")
                    'objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "HSNB2B.xlsx")
                    'objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "HSNB2C.xlsx")
                    'objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "DOCS.xlsx")

                    ' objmail.attachment6 = System.AppDomain.CurrentDomain.BaseDirectory & "DOCS.xlsx"
                    objmail.subject = "GSTR1 Details Of " & CmpName
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBITC4.Checked = True Then

                Dim OBJITC As New ITC4GridReport
                OBJITC.MdiParent = MDIMain
                If chkdate.CheckState = True Then OBJITC.WHERECLAUSE = OBJITC.WHERECLAUSE & " AND CHALLANDATE >= '" & Format(Convert.ToDateTime(dtfrom.Text), "MM/dd/yyyy") & "' AND CHALLANDATE <= '" & Format(Convert.ToDateTime(dtto.Text), "MM/dd/yyyy") & "'"
                OBJITC.Show()

            ElseIf RBGSTR1GRID.Checked = True Then
                Dim OBJGSTR1 As New GSTR1GridReport
                OBJGSTR1.MdiParent = MDIMain

                If chkdate.Checked = True Then
                    OBJGSTR1.WHERECLAUSE = OBJGSTR1.WHERECLAUSE & " AND INVOICEMASTER.INVOICE_DATE >= '" & Format(Convert.ToDateTime(dtfrom.Text), "MM/dd/yyyy") & "' AND INVOICEMASTER.INVOICE_DATE <= '" & Format(Convert.ToDateTime(dtto.Text), "MM/dd/yyyy") & "'"
                    OBJGSTR1.FROMDATE = Convert.ToDateTime(dtfrom.Text)
                    OBJGSTR1.TODATE = Convert.ToDateTime(dtto.Text)
                End If
                OBJGSTR1.Show()

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GSTTaxFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fillregister(CMBREGISTER, " AND (REGISTER_TYPE ='SALE' OR REGISTER_TYPE = 'PURCHASE')")
            dtfrom.Text = AccFrom
            dtto.Text = AccTo

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, False, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then cmbacccode.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, cmbacccode, e, Me, txtadd, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')", "SUNDRY DEBTORS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbstate_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSTATE.Enter
        Try
            If CMBSTATE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("state_name", "", "StateMaster", " and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "state_name"
                    CMBSTATE.DataSource = dt
                    CMBSTATE.DisplayMember = "state_name"
                    CMBSTATE.Text = ""
                End If
                CMBSTATE.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbstate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSTATE.Validating
        Try
            If CMBSTATE.Text.Trim <> "" Then
                pcase(CMBSTATE)
                Dim objClsCommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objClsCommon.search("state_name", "", "StateMaster", " and state_name = '" & CMBSTATE.Text.Trim & "' and state_cmpid = " & CmpId & " and state_locationid = " & Locationid & " and state_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBSTATE.Text.Trim
                    Dim tempmsg As Integer = MsgBox("State not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBSTATE.Text = a
                        objyearmaster.savestate(CMBSTATE.Text.Trim, CmpId, Locationid, Userid, YearId, " and state_name = '" & CMBSTATE.Text.Trim & "' and state_cmpid = " & CmpId & " and state_locationid = " & Locationid & " and state_yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = CMBSTATE.DataSource
                        If CMBSTATE.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(CMBSTATE.Text)
                                CMBSTATE.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub dtfrom_Validating(sender As Object, e As CancelEventArgs) Handles dtfrom.Validating
        If dtfrom.Text.Trim <> "__/__/____" Then
            'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
            Dim TEMP As DateTime
            If Not DateTime.TryParse(dtfrom.Text, TEMP) Then
                MsgBox("Enter Proper Date")
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub

    Private Sub dtto_Validating(sender As Object, e As CancelEventArgs) Handles dtto.Validating
        If dtto.Text.Trim <> "__/__/____" Then
            'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
            Dim TEMP As DateTime
            If Not DateTime.TryParse(dtto.Text, TEMP) Then
                MsgBox("Enter Proper Date")
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub

End Class