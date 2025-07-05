

Imports BL
    Imports System.Windows.Forms

Public Class AgencyReceiptDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim PAYREGID As Integer

    Private Sub AgencyReceiptDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            Dim dt As DataTable = objclsCMST.search(" AGENCYRECEIPTMASTER.Areceipt_no AS SRNO, LEDGERS.Acc_cmpname AS Name, AGENCYRECEIPTMASTER.Areceipt_date AS Date, AGENCYRECEIPTMASTER.Areceipt_total AS Total, AGENCYRECEIPTMASTER.Areceipt_CHQNO AS [Chq. No.],AGENCYRECEIPTMASTER.Areceipt_registerid AS Registerid, AGENCYRECEIPTMASTER.Areceipt_remarks AS Remarks, BANKLEDGERS.Acc_cmpname AS BankName, ISNULL(AGENCYRECEIPTMASTER.Areceipt_CHECKPDC, 0) AS CHECKPDC, AGENCYRECEIPTMASTER.Areceipt_BILLREMARKS AS BILLREMARKS, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(GROUPMASTER.group_name, '') AS GROUPNAME, ISNULL(AGENCYRECEIPTMASTER.Areceipt_CHQDATE, GETDATE()) AS CHQDATE, AGENCYRECEIPTMASTER.Areceipt_SPECIALREMARKS AS SPECIALREMARK ", "", " AGENCYRECEIPTMASTER LEFT OUTER JOIN LEDGERS ON AGENCYRECEIPTMASTER.Areceipt_ledgerid = LEDGERS.Acc_id INNER JOIN LEDGERS AS BANKLEDGERS ON AGENCYRECEIPTMASTER.Areceipt_accid = BANKLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.Acc_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id", TEMPCONDITION)
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
                Dim OBJREC As New AgencyReceipt
                OBJREC.MdiParent = MDIMain
                OBJREC.EDIT = editval
                OBJREC.TEMPREGNAME = cmbregister.Text.Trim
                OBJREC.TEMPARECEIPTNO = billno
                OBJREC.Show()
                'Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
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

    Private Sub gridRECEIPT_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridpayment.DoubleClick
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
                dt = clscommon.SEARCH(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'RECEIPT' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
                If dt.Rows.Count > 0 Then
                    PAYREGID = dt.Rows(0).Item(0)
                    'cmbregister.Enabled = False
                    fillgrid(" and dbo.AGENCYRECEIPTMASTER.ARECEIPT_cmpid=" & CmpId & " and dbo.AGENCYRECEIPTMASTER.ARECEIPT_LOCATIONid=" & Locationid & " and dbo.AGENCYRECEIPTMASTER.ARECEIPT_YEARid=" & YearId & " AND AGENCYRECEIPTMASTER.ARECEIPT_registerid = " & PAYREGID & " order by dbo.AGENCYRECEIPTMASTER.ARECEIPT_no ")
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridpayment.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AgencyReceiptDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Receipt Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = ""
            PERIOD = AccFrom & " - " & AccTo

            opti.SheetName = "Receipt Details"
            gridpayment.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Receipt Details", gridpayment.VisibleColumns.Count + gridpayment.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("Receipt Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ReceiptDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "SVS" Then Me.Close()
    End Sub

    Private Sub TOOLMAIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridpayment.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Receipt Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail Receipt from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(True)
                End If
            Else
                If MsgBox("Wish to Mail Selected Receipt ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs)
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridpayment.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Receipt Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Receipt from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Receipt ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(False, True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHQPRINTTOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHQPRINTTOOL.Click
        'Try
        '    If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridpayment.SelectedRowsCount = 0 Then Exit Sub




        '    'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
        '    If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
        '        If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
        '            MsgBox("Enter Proper Receipt Nos", MsgBoxStyle.Critical)
        '            Exit Sub
        '        End If

        '        'FOR PRINTING BANKSLIP
        '        If MsgBox("Wish To Print Bank Slip", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '            Dim OBJREC As New receipt_advice
        '            OBJREC.FRMSTRING = "BANKSLIP"
        '            OBJREC.WHERECLAUSE = " {receiptmaster.receipt_no} >= " & Val(TXTFROM.Text.Trim) & " AND {receiptmaster.receipt_no} <= " & Val(TXTTO.Text.Trim) & " and {RECEIPT_REPORT.REGNAME}= '" & cmbregister.Text.Trim & "' and {receiptmaster.receipt_YEARid} = " & YearId
        '            OBJREC.MdiParent = MDIMain
        '            OBJREC.Show()
        '        End If


        '        'FOR PRINTING OFFICE COPY
        '        If MsgBox("Wish To Print Office Copy", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '            Dim OBJREC As New receipt_advice
        '            OBJREC.FRMSTRING = "OFFICECOPY"
        '            OBJREC.WHERECLAUSE = " {receiptmaster.receipt_no} >= " & Val(TXTFROM.Text.Trim) & " AND {receiptmaster.receipt_no} <= " & Val(TXTTO.Text.Trim) & " and {RECEIPT_REPORT.REGNAME}= '" & cmbregister.Text.Trim & "' and {receiptmaster.receipt_YEARid} = " & YearId
        '            OBJREC.MdiParent = MDIMain
        '            OBJREC.Show()
        '        End If

        '        If ClientName <> "MAHAVIRPOLYCOT" Then
        '            If MsgBox("Wish to Print Receipt from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
        '                SERVERPROPDIRECT()
        '            End If
        '        End If
        '    Else

        '            'FOR PRINTING BANKSLIP
        '            If MsgBox("Wish To Print Bank Slip", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

        '            Dim RECCLAUSE As String = ""
        '            Dim SELECTEDROWS As Int32() = gridpayment.GetSelectedRows()
        '            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
        '                Dim ROW As DataRow = gridpayment.GetDataRow(SELECTEDROWS(I))
        '                If RECCLAUSE = "" Then
        '                    RECCLAUSE = " AND {receiptmaster.receipt_no} IN [" & Val(ROW("SRNO"))
        '                Else
        '                    RECCLAUSE = RECCLAUSE & "," & Val(ROW("SRNO"))
        '                End If
        '            Next
        '            If RECCLAUSE <> "" Then RECCLAUSE = RECCLAUSE & "]"


        '            Dim OBJREC As New receipt_advice
        '            OBJREC.FRMSTRING = "BANKSLIP"
        '            OBJREC.WHERECLAUSE = " {REGISTERMASTER.REGISTER_NAME}= '" & cmbregister.Text.Trim & "' and {receiptmaster.receipt_YEARid} = " & YearId & RECCLAUSE
        '            OBJREC.MdiParent = MDIMain
        '            OBJREC.Show()
        '        End If


        '        'FOR PRINTING OFFICECOPY
        '        If MsgBox("Wish To Print Office Copy", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

        '            Dim RECCLAUSE As String = ""
        '            Dim SELECTEDROWS As Int32() = gridpayment.GetSelectedRows()
        '            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
        '                Dim ROW As DataRow = gridpayment.GetDataRow(SELECTEDROWS(I))
        '                If RECCLAUSE = "" Then
        '                    RECCLAUSE = " AND {receiptmaster.receipt_no} IN [" & Val(ROW("SRNO"))
        '                Else
        '                    RECCLAUSE = RECCLAUSE & "," & Val(ROW("SRNO"))
        '                End If
        '            Next
        '            If RECCLAUSE <> "" Then RECCLAUSE = RECCLAUSE & "]"


        '            Dim OBJREC As New receipt_advice
        '            OBJREC.FRMSTRING = "OFFICECOPY"
        '            OBJREC.WHERECLAUSE = " {REGISTERMASTER.REGISTER_NAME}= '" & cmbregister.Text.Trim & "' and {receiptmaster.receipt_YEARid} = " & YearId & RECCLAUSE
        '            OBJREC.MdiParent = MDIMain
        '            OBJREC.Show()
        '        End If


        '        If ClientName <> "MAHAVIRPOLYCOT" Then
        '            If MsgBox("Wish to Print Selected Receipt Note ?", MsgBoxStyle.YesNo) = vbYes Then
        '                SERVERPROPSELECTED()
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try

    End Sub

    Sub SERVERPROPDIRECT(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        'Try
        '    Dim ALATTACHMENT As New ArrayList
        '    Dim FILENAME As New ArrayList
        '    If INVOICEMAIL = False And WHATSAPP = False Then
        '        If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
        '    End If
        '    For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
        '        Dim OBJREC As New receipt_advice
        '        OBJREC.MdiParent = MDIMain
        '        OBJREC.DIRECTPRINT = True
        '        OBJREC.FRMSTRING = "RECEIPT"
        '        OBJREC.DIRECTMAIL = INVOICEMAIL
        '        OBJREC.DIRECTWHATSAPP = WHATSAPP
        '        OBJREC.REGNAME = cmbregister.Text.Trim
        '        OBJREC.PRINTSETTING = PRINTDIALOG
        '        OBJREC.recno = Val(I)
        '        OBJREC.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
        '        OBJREC.Show()
        '        OBJREC.Close()
        '        ALATTACHMENT.Add(Application.StartupPath & "\RECEIPT_" & I & ".pdf")
        '        FILENAME.Add("RECEIPT_" & I & ".pdf")
        '    Next

        '    If INVOICEMAIL Then
        '        Dim OBJMAIL As New SendMail
        '        OBJMAIL.ALATTACHMENT = ALATTACHMENT
        '        OBJMAIL.subject = "RECEIPT"
        '        OBJMAIL.ShowDialog()
        '    End If

        '    If WHATSAPP = True Then
        '        Dim OBJWHATSAPP As New SendWhatsapp
        '        OBJWHATSAPP.PATH = ALATTACHMENT
        '        OBJWHATSAPP.FILENAME = FILENAME
        '        OBJWHATSAPP.ShowDialog()
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Sub SERVERPROPSELECTED(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        'Try

        '    Dim ALATTACHMENT As New ArrayList
        '    Dim FILENAME As New ArrayList

        '    If INVOICEMAIL = False And WHATSAPP = False Then
        '        If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
        '    End If
        '    Dim SELECTEDROWS As Int32() = gridpayment.GetSelectedRows()
        '    For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
        '        Dim ROW As DataRow = gridpayment.GetDataRow(SELECTEDROWS(I))

        '        Dim OBJREC As New receipt_advice
        '        OBJREC.MdiParent = MDIMain
        '        OBJREC.DIRECTPRINT = True
        '        OBJREC.FRMSTRING = "RECEIPT"
        '        OBJREC.DIRECTMAIL = INVOICEMAIL
        '        OBJREC.DIRECTWHATSAPP = WHATSAPP
        '        OBJREC.REGNAME = cmbregister.Text.Trim
        '        OBJREC.PRINTSETTING = PRINTDIALOG
        '        OBJREC.recno = Val(ROW("SRNO"))
        '        OBJREC.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
        '        OBJREC.Show()
        '        OBJREC.Close()
        '        ALATTACHMENT.Add(Application.StartupPath & "\RECEIPT_" & Val(ROW("SRNO")) & ".pdf")
        '        FILENAME.Add("RECEIPT_" & Val(ROW("SRNO")) & ".pdf")
        '    Next

        '    If INVOICEMAIL Then
        '        Dim OBJMAIL As New SendMail
        '        OBJMAIL.ALATTACHMENT = ALATTACHMENT
        '        OBJMAIL.subject = "RECEIPT"
        '        OBJMAIL.ShowDialog()
        '    End If

        '    If WHATSAPP = True Then
        '        Dim OBJWHATSAPP As New SendWhatsapp
        '        OBJWHATSAPP.PATH = ALATTACHMENT
        '        OBJWHATSAPP.FILENAME = FILENAME
        '        OBJWHATSAPP.ShowDialog()
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub TXTFROM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTFROM.KeyPress, TXTTO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub cmbregister_Enter(sender As Object, e As EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " And register_type = 'RECEIPT'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.SEARCH(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'RECEIPT' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class