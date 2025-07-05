Imports System.Windows.Forms
Imports BL

Public Class PaymentDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim PAYREGID As Integer
    Dim DTWHATSAPP As New DataTable
    Dim DTMAIL As New DataTable


    Private Sub PaymentDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.N) Or (e.Alt = True And e.KeyCode = Windows.Forms.Keys.A) Then       'for AddNew 
                showform(False, 0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("  CAST(0 AS BIT) AS CHK, PAYMENTMASTER.PAYMENT_no AS SRNO, LEDGERS.Acc_cmpname AS Name, PAYMENTMASTER.PAYMENT_date AS Date, PAYMENTMASTER.PAYMENT_total AS Total, PAYMENTMASTER.PAYMENT_CHQNO AS [Chq. No.], PAYMENTMASTER.PAYMENT_registerid AS Registerid, PAYMENTMASTER.PAYMENT_remarks AS Remarks, BANKLEDGERS.Acc_cmpname AS BankName,  PAYMENTMASTER.PAYMENT_BILLREMARKS AS BILLREMARKS, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(PARTYBANKMASTER.PARTYBANK_name, '') AS PARTYBANK,  ISNULL(LEDGERS.ACC_ACCOUNTTYPE, '') AS ACCTYPE, ISNULL(LEDGERS.ACC_ACCOUNTNO, '') AS ACCOUNTNO, ISNULL(LEDGERS.ACC_IFSC, '') AS IFSCCODE, ISNULL(LEDGERS.ACC_BRANCH, '') AS BRANCH,  ISNULL(CITYMASTER.city_name, '') AS BANKCITY, ISNULL(GROUPMASTER.group_name, '') AS GROUPNAME, PAYMENTMASTER.PAYMENT_SPECIALREMARKS AS SPECIALREMARK, ISNULL(USERMASTER.User_Name, '')  AS CREATEDBY, ISNULL(AGENTLEDGERS.ACC_WHATSAPPNO, '') AS AGENTWHATSAPP, ISNULL(LEDGERS.ACC_WHATSAPPNO, '') AS PARTYWHATSAPP, ISNULL(PAYMENTMASTER.PAYMENT_initials, '') AS INITIALS,  ISNULL(LEDGERS.Acc_email, '') AS PARTYEMAILID, ISNULL(AGENTLEDGERS.Acc_email, '') AS AGENTEMAILID, ISNULL(PAYMENT_PRINT,0) AS [PRINT]", "", "  PAYMENTMASTER INNER JOIN LEDGERS ON PAYMENTMASTER.PAYMENT_ledgerid = LEDGERS.Acc_id INNER JOIN LEDGERS AS BANKLEDGERS ON PAYMENTMASTER.PAYMENT_accid = BANKLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_BANKCITYID = CITYMASTER.city_id LEFT OUTER JOIN PARTYBANKMASTER ON LEDGERS.ACC_BANKID = PARTYBANKMASTER.PARTYBANK_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.Acc_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN USERMASTER ON PAYMENTMASTER.PAYMENT_userid = USERMASTER.User_id", TEMPCONDITION)
            griddetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridpayment.FocusedRowHandle = gridpayment.RowCount - 1
                gridpayment.TopRowIndex = gridpayment.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal billno As Integer)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And gridpayment.RowCount > 0) Then
                Dim objpaymaster As New PaymentMaster
                objpaymaster.MdiParent = MDIMain
                objpaymaster.EDIT = editval
                objpaymaster.TEMPREGNAME = cmbregister.Text.Trim
                objpaymaster.TEMPPAYMENTNO = billno
                objpaymaster.Show()
                'Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridpayment.DoubleClick
        Try
            showform(True, gridpayment.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If cmbregister.Text.Trim.Length > 0 Then
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.SEARCH(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'PAYMENT' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
                If dt.Rows.Count > 0 Then
                    PAYREGID = dt.Rows(0).Item(0)
                    'cmbregister.Enabled = False
                    fillgrid(" and dbo.PaymentMaster.payment_cmpid=" & CmpId & " and dbo.PaymentMaster.payment_LOCATIONid=" & Locationid & " and dbo.PaymentMaster.payment_YEARid=" & YearId & " AND PaymentMaster.payment_registerid = " & PAYREGID & " order by dbo.PaymentMaster.payment_no ")
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PaymentDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'PAYMENT'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            DTMAIL.Columns.Add("PRINTINITIALS")
            DTMAIL.Columns.Add("Registerid")
            DTMAIL.Columns.Add("REGNAME")
            DTMAIL.Columns.Add("INITIALS")
            DTMAIL.Columns.Add("DATE")
            DTMAIL.Columns.Add("NAME")
            DTMAIL.Columns.Add("PARTYEMAILID")
            DTMAIL.Columns.Add("AGENTNAME")
            DTMAIL.Columns.Add("AGENTEMAILID")
            DTMAIL.Columns.Add("GRANDTOTAL")
            DTMAIL.Columns.Add("SUBJECT")
            DTMAIL.Columns.Add("ATTACHMENT")
            DTMAIL.Columns.Add("FILENAME")

            DTWHATSAPP.Columns.Add("PRINTINITIALS")
            DTWHATSAPP.Columns.Add("Registerid")
            DTWHATSAPP.Columns.Add("REGNAME")
            DTWHATSAPP.Columns.Add("INITIALS")
            DTWHATSAPP.Columns.Add("DATE")
            DTWHATSAPP.Columns.Add("NAME")
            DTWHATSAPP.Columns.Add("PARTYWHATSAPP")
            DTWHATSAPP.Columns.Add("AGENTNAME")
            DTWHATSAPP.Columns.Add("AGENTWHATSAPP")
            DTWHATSAPP.Columns.Add("")
            DTWHATSAPP.Columns.Add("SUBJECT")
            DTWHATSAPP.Columns.Add("ATTACHMENT")
            DTWHATSAPP.Columns.Add("FILENAME")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridpayment.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Payment Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = ""
            PERIOD = AccFrom & " - " & AccTo

            opti.SheetName = "Payment Details"
            gridpayment.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Payment Details", gridpayment.VisibleColumns.Count + gridpayment.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("Payment Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub PaymentDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "SVS" Then Me.Close()
    End Sub

    Private Sub TOOLMAIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLMAIL.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridpayment.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Payment Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail Payment from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(True)
                End If
            Else
                If MsgBox("Wish to Mail Selected Payment ?", MsgBoxStyle.YesNo) = vbYes Then
                    CMDOK.Focus()
                    SERVERPROPSELECTED(True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridpayment.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Payment Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Payment from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Payment ?", MsgBoxStyle.YesNo) = vbYes Then
                    CMDOK.Focus()
                    SERVERPROPSELECTED(False, True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHQPRINTTOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHQPRINTTOOL.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridpayment.SelectedRowsCount = 0 Then Exit Sub


            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Payment Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If


                If MsgBox("Print Chq from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim OBJPAY As New payment_advice
                    OBJPAY.MdiParent = MDIMain
                    OBJPAY.DIRECTPRINT = True
                    OBJPAY.FROMNO = Val(TXTFROM.Text.Trim)
                    OBJPAY.TONO = Val(TXTTO.Text.Trim)
                    OBJPAY.REGNAME = cmbregister.Text.Trim
                    OBJPAY.FRMSTRING = "CHQPAY"
                    OBJPAY.Show()
                    OBJPAY.Close()
                End If


                If MsgBox("Wish to Print Payment from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPDIRECT()
                End If
            Else
                If MsgBox("Wish to Print Selected Payment Note ?", MsgBoxStyle.YesNo) = vbYes Then
                    CMDOK.Focus()
                    SERVERPROPSELECTED()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub SERVERPROPDIRECT(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                Dim OBJPAY As New payment_advice
                OBJPAY.MdiParent = MDIMain
                OBJPAY.DIRECTPRINT = True
                OBJPAY.FRMSTRING = "PAYMENT"
                OBJPAY.DIRECTMAIL = INVOICEMAIL
                OBJPAY.DIRECTWHATSAPP = WHATSAPP
                OBJPAY.REGNAME = cmbregister.Text.Trim
                OBJPAY.PRINTSETTING = PRINTDIALOG
                'OBJPAY.LEDGERSNAME = "NAME"
                OBJPAY.payno = Val(I)
                OBJPAY.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJPAY.Show()
                OBJPAY.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\PAYMENT_" & I & ".pdf")
                FILENAME.Add("PAYMENT_" & I & ".pdf")

                If INVOICEMAIL = False And WHATSAPP = False Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER SET PAYMENT_PRINT = 1 FROM PAYMENTMASTER INNER JOIN REGISTERMASTER ON PAYMENT_REGISTERID = REGISTER_ID WHERE PAYMENT_NO = " & Val(I) & " and REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND PAYMENT_YEARID = " & YearId, "", "")
                End If
            Next



            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Payment"
                OBJMAIL.ShowDialog()
            End If
            'If INVOICEMAIL = True Then
            '    If DTMAIL.Rows.Count = 0 Then Exit Sub
            '    Dim OBJEMAIL As New SendMultipleMail
            '    OBJEMAIL.FORMTYPE = "Payment"
            '    OBJEMAIL.DT = DTMAIL
            '    OBJEMAIL.ShowDialog()
            '    Exit Sub
            'End If

            If WHATSAPP = True Then
                Dim OBJWHATSAPP As New SendWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.ShowDialog()
            End If
            'If WHATSAPP = True Then
            '    If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
            '    Dim OBJWHATSAPP As New SendMultipleWhatsapp
            '    OBJWHATSAPP.PATH = ALATTACHMENT
            '    OBJWHATSAPP.FILENAME = FILENAME
            '    OBJWHATSAPP.DT = DTWHATSAPP
            '    OBJWHATSAPP.ShowDialog()
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPSELECTED(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            DTMAIL.Rows.Clear()
            DTWHATSAPP.Rows.Clear()

            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            'Dim SELECTEDROWS As Int32() = gridpayment.GetSelectedRows()
            'For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
            For I As Integer = 0 To Val(gridpayment.RowCount - 1)

                Dim ROW As DataRow = gridpayment.GetDataRow(I)
                If ROW("CHK") = True Then
                    Dim OBJPAY As New payment_advice
                    OBJPAY.MdiParent = MDIMain
                    OBJPAY.DIRECTPRINT = True
                    OBJPAY.FRMSTRING = "PAYMENT"
                    OBJPAY.DIRECTMAIL = INVOICEMAIL
                    OBJPAY.DIRECTWHATSAPP = WHATSAPP
                    OBJPAY.REGNAME = cmbregister.Text.Trim
                    OBJPAY.PRINTSETTING = PRINTDIALOG
                    OBJPAY.LEDGERSNAME = ROW("NAME")
                    OBJPAY.payno = Val(ROW("SRNO"))
                    OBJPAY.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJPAY.Show()
                    OBJPAY.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\" & ROW("NAME") & "PAYMENT_" & Val(ROW("SRNO")) & ".pdf")
                    FILENAME.Add(ROW("NAME") & "PAYMENT_" & Val(ROW("SRNO")) & ".pdf")

                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" register_name, ISNULL (REGISTERMASTER.register_id, 0) AS Registerid", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'PAYMENT' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)

                    'ADDINT IN DTEMAIL
                    DTMAIL.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("Registerid"), cmbregister.Text.Trim, ROW("INITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYEMAILID"), ROW("AGENTNAME"), ROW("AGENTEMAILID"), "", UCase(CmpName) & " - Pay No. " & ROW("INITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "PAYMENT_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "PAYMENT_" & Val(ROW("SRNO")) & ".pdf")

                    'ADDINT IN WHATSAPP
                    DTWHATSAPP.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("Registerid"), cmbregister.Text.Trim, ROW("INITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("AGENTNAME"), ROW("AGENTWHATSAPP"), "", UCase(CmpName) & " - Pay No. " & ROW("INITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "PAYMENT_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "PAYMENT_" & Val(ROW("SRNO")) & ".pdf")

                    If INVOICEMAIL = False And WHATSAPP = False Then DT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER SET PAYMENT_PRINT = 1 FROM PAYMENTMASTER INNER JOIN REGISTERMASTER ON PAYMENT_REGISTERID = REGISTER_ID WHERE PAYMENT_NO = " & Val(ROW("SRNO")) & " and REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND PAYMENT_YEARID = " & YearId, "", "")

                End If
            Next

            'If INVOICEMAIL Then
            '    Dim OBJMAIL As New SendMail
            '    OBJMAIL.ALATTACHMENT = ALATTACHMENT
            '    OBJMAIL.subject = "Payment"
            '    OBJMAIL.ShowDialog()
            'End If
            If INVOICEMAIL = True Then
                If DTMAIL.Rows.Count = 0 Then Exit Sub
                Dim OBJEMAIL As New SendMultipleMail
                OBJEMAIL.FORMTYPE = "Payment"
                OBJEMAIL.DT = DTMAIL
                OBJEMAIL.ShowDialog()
                Exit Sub
            End If

            'If WHATSAPP = True Then
            '    Dim OBJWHATSAPP As New SendWhatsapp
            '    OBJWHATSAPP.PATH = ALATTACHMENT
            '    OBJWHATSAPP.FILENAME = FILENAME
            '    OBJWHATSAPP.ShowDialog()
            'End If
            If WHATSAPP = True Then
                If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
                Dim OBJWHATSAPP As New SendMultipleWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.DT = DTWHATSAPP
                OBJWHATSAPP.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTFROM.KeyPress, TXTTO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub cmbregister_Enter(sender As Object, e As EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'PAYMENT'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'PAYMENT' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class