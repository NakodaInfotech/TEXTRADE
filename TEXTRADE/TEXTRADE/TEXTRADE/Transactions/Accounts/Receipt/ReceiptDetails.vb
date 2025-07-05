
Imports BL
Imports System.Windows.Forms

Public Class ReceiptDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim PAYREGID As Integer
    Dim DTMAIL As New DataTable
    Dim DTWHATSAPP As New DataTable
    'Public multi As Boolean = False


    Private Sub ReceiptDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Sub FILLGRID(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            'Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, RECEIPTMASTER.receipt_no AS SRNO, LEDGERS.Acc_cmpname AS Name, RECEIPTMASTER.receipt_date AS Date, RECEIPTMASTER.receipt_total AS Total, RECEIPTMASTER.RECEIPT_CHQNO AS [Chq. No.],RECEIPTMASTER.receipt_registerid AS Registerid, RECEIPTMASTER.receipt_remarks AS Remarks, BANKLEDGERS.Acc_cmpname AS BankName, ISNULL(RECEIPTMASTER.RECEIPT_CHECKPDC, 0) AS CHECKPDC, RECEIPTMASTER.RECEIPT_BILLREMARKS AS BILLREMARKS, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(GROUPMASTER.group_name, '') AS GROUPNAME, ISNULL(RECEIPTMASTER.RECEIPT_CHQDATE, GETDATE()) AS CHQDATE, RECEIPTMASTER.RECEIPT_SPECIALREMARKS AS SPECIALREMARK , ISNULL(USERMASTER.User_Name,'') AS CREATEDBY ", "", "  RECEIPTMASTER LEFT OUTER JOIN LEDGERS ON RECEIPTMASTER.receipt_ledgerid = LEDGERS.Acc_id INNER JOIN LEDGERS AS BANKLEDGERS ON RECEIPTMASTER.receipt_accid = BANKLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.Acc_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN USERMASTER ON RECEIPTMASTER.receipt_userid = USERMASTER.User_id", TEMPCONDITION)
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, RECEIPTMASTER.receipt_no AS SRNO, LEDGERS.Acc_cmpname AS Name, RECEIPTMASTER.receipt_date AS Date, RECEIPTMASTER.receipt_total AS Total, RECEIPTMASTER.RECEIPT_CHQNO AS [Chq. No.], RECEIPTMASTER.receipt_registerid AS Registerid, RECEIPTMASTER.receipt_remarks AS Remarks, BANKLEDGERS.Acc_cmpname AS BankName, ISNULL(RECEIPTMASTER.RECEIPT_CHECKPDC, 0) AS CHECKPDC, RECEIPTMASTER.RECEIPT_BILLREMARKS AS BILLREMARKS, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(GROUPMASTER.group_name, '') AS GROUPNAME, ISNULL(RECEIPTMASTER.RECEIPT_CHQDATE, GETDATE()) AS CHQDATE, RECEIPTMASTER.RECEIPT_SPECIALREMARKS AS SPECIALREMARK, ISNULL(USERMASTER.User_Name, '') AS CREATEDBY, ISNULL(AGENTLEDGERS.ACC_WHATSAPPNO, '') AS AGENTWHATSAPP, ISNULL(LEDGERS.ACC_WHATSAPPNO, '') AS PARTYWHATSAPP, ISNULL(RECEIPTMASTER.receipt_initials, '') AS INITIALS, ISNULL(AGENTLEDGERS.Acc_email, '') AS AGENTEMAILID, ISNULL(LEDGERS.Acc_email, '') AS PARTYEMAILID, ISNULL(RECEIPTMASTER.RECEIPT_BANKNAME, '') AS PARTYBANKNAME , (CASE WHEN RECEIPTMASTER.RECEIPT_RECODATE = '' THEN NULL ELSE CAST(RECEIPTMASTER.RECEIPT_RECODATE AS DATE) END) AS RECODATE, ISNULL(RECEIPT_PRINT,0) AS [PRINT] ", "", "  RECEIPTMASTER LEFT OUTER JOIN LEDGERS ON RECEIPTMASTER.receipt_ledgerid = LEDGERS.Acc_id INNER JOIN LEDGERS AS BANKLEDGERS ON RECEIPTMASTER.receipt_accid = BANKLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.Acc_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN USERMASTER ON RECEIPTMASTER.receipt_userid = USERMASTER.User_id", TEMPCONDITION)

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
                Dim OBJREC As New Receipt
                OBJREC.MdiParent = MDIMain
                OBJREC.edit = editval
                OBJREC.TEMPREGNAME = cmbregister.Text.Trim
                OBJREC.TEMPRECEIPTNO = billno
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
                dt = clscommon.search(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'RECEIPT' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
                If dt.Rows.Count > 0 Then
                    PAYREGID = dt.Rows(0).Item(0)
                    'cmbregister.Enabled = False
                    fillgrid(" and dbo.RECEIPTMASTER.RECEIPT_cmpid=" & CmpId & " and dbo.RECEIPTMASTER.RECEIPT_LOCATIONid=" & Locationid & " and dbo.RECEIPTMASTER.RECEIPT_YEARid=" & YearId & " AND RECEIPTMASTER.RECEIPT_registerid = " & PAYREGID & " order by dbo.RECEIPTMASTER.RECEIPT_no ")
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

    Private Sub ReceiptDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'RECEIPT'")
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
        If ClientName = "AVIS" Then
            GPARTYBANK.Visible = False
        End If
    End Sub

    Private Sub TOOLMAIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLMAIL.Click
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
                    MsgBox("Enter Proper Receipt Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Receipt from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Receipt ?", MsgBoxStyle.YesNo) = vbYes Then
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
            CMDOK.Focus()
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridpayment.SelectedRowsCount = 0 Then Exit Sub




            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Receipt Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                'FOR PRINTING BANKSLIP
                If MsgBox("Wish To Print Bank Slip", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim OBJREC As New receipt_advice
                    OBJREC.FRMSTRING = "BANKSLIP"
                    OBJREC.WHERECLAUSE = " {receiptmaster.receipt_no} >= " & Val(TXTFROM.Text.Trim) & " AND {receiptmaster.receipt_no} <= " & Val(TXTTO.Text.Trim) & " and {REGISTERMASTER.REGISTER_NAME}= '" & cmbregister.Text.Trim & "' and {receiptmaster.receipt_YEARid} = " & YearId
                    OBJREC.MdiParent = MDIMain
                    OBJREC.Show()
                End If


                'FOR PRINTING OFFICE COPY
                If MsgBox("Wish To Print Office Copy", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim OBJREC As New receipt_advice
                    OBJREC.FRMSTRING = "OFFICECOPY"
                    OBJREC.WHERECLAUSE = " {receiptmaster.receipt_no} >= " & Val(TXTFROM.Text.Trim) & " AND {receiptmaster.receipt_no} <= " & Val(TXTTO.Text.Trim) & " and {REGISTERMASTER.REGISTER_NAME}= '" & cmbregister.Text.Trim & "' and {receiptmaster.receipt_YEARid} = " & YearId
                    OBJREC.MdiParent = MDIMain
                    OBJREC.Show()
                End If

                If ClientName <> "MAHAVIRPOLYCOT" Then
                    If MsgBox("Wish to Print Receipt from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                        SERVERPROPDIRECT()
                    End If
                End If
            Else

                    'FOR PRINTING BANKSLIP
                    If MsgBox("Wish To Print Bank Slip", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    Dim RECCLAUSE As String = ""
                    For I As Integer = 0 To Val(gridpayment.RowCount - 1)
                        Dim ROW As DataRow = gridpayment.GetDataRow(I)
                        If ROW("CHK") = True Then
                            If RECCLAUSE = "" Then
                                RECCLAUSE = " AND {receiptmaster.receipt_no} IN [" & Val(ROW("SRNO"))
                            Else
                                RECCLAUSE = RECCLAUSE & "," & Val(ROW("SRNO"))
                            End If
                        End If
                    Next
                    If RECCLAUSE <> "" Then RECCLAUSE = RECCLAUSE & "]"


                    Dim OBJREC As New receipt_advice
                    OBJREC.FRMSTRING = "BANKSLIP"
                    OBJREC.WHERECLAUSE = " {REGISTERMASTER.REGISTER_NAME}= '" & cmbregister.Text.Trim & "' and {receiptmaster.receipt_YEARid} = " & YearId & RECCLAUSE
                    OBJREC.MdiParent = MDIMain
                    OBJREC.Show()
                End If


                'FOR PRINTING OFFICECOPY
                If MsgBox("Wish To Print Office Copy", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    Dim RECCLAUSE As String = ""
                    For I As Integer = 0 To Val(gridpayment.RowCount - 1)
                        Dim ROW As DataRow = gridpayment.GetDataRow(I)
                        If ROW("CHK") = True Then
                            If RECCLAUSE = "" Then
                                RECCLAUSE = " AND {receiptmaster.receipt_no} IN [" & Val(ROW("SRNO"))
                            Else
                                RECCLAUSE = RECCLAUSE & "," & Val(ROW("SRNO"))
                            End If
                        End If
                    Next
                    If RECCLAUSE <> "" Then RECCLAUSE = RECCLAUSE & "]"


                    Dim OBJREC As New receipt_advice
                    OBJREC.FRMSTRING = "OFFICECOPY"
                    OBJREC.WHERECLAUSE = " {REGISTERMASTER.REGISTER_NAME}= '" & cmbregister.Text.Trim & "' and {receiptmaster.receipt_YEARid} = " & YearId & RECCLAUSE
                    OBJREC.MdiParent = MDIMain
                    OBJREC.Show()
                End If


                If ClientName <> "MAHAVIRPOLYCOT" Then
                    If MsgBox("Wish to Print Selected Receipt Note ?", MsgBoxStyle.YesNo) = vbYes Then
                        SERVERPROPSELECTED()
                    End If
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
                Dim OBJREC As New receipt_advice
                OBJREC.MdiParent = MDIMain
                OBJREC.DIRECTPRINT = True
                OBJREC.FRMSTRING = "RECEIPT"
                OBJREC.DIRECTMAIL = INVOICEMAIL
                OBJREC.DIRECTWHATSAPP = WHATSAPP
                OBJREC.REGNAME = cmbregister.Text.Trim
                OBJREC.PRINTSETTING = PRINTDIALOG
                OBJREC.recno = Val(I)
                OBJREC.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJREC.Show()
                OBJREC.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\RECEIPT_" & I & ".pdf")
                FILENAME.Add("RECEIPT_" & I & ".pdf")

                If INVOICEMAIL = False And WHATSAPP = False Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER SET RECEIPT_PRINT = 1 FROM RECEIPTMASTER INNER JOIN REGISTERMASTER ON RECEIPT_REGISTERID = REGISTER_ID WHERE RECEIPT_NO = " & Val(I) & " and REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND RECEIPT_YEARID = " & YearId, "", "")
                End If
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "RECEIPT"
                OBJMAIL.ShowDialog()
            End If

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
                    Dim OBJREC As New receipt_advice
                    OBJREC.MdiParent = MDIMain
                    OBJREC.DIRECTPRINT = True
                    OBJREC.FRMSTRING = "RECEIPT"
                    OBJREC.DIRECTMAIL = INVOICEMAIL
                    OBJREC.DIRECTWHATSAPP = WHATSAPP
                    OBJREC.REGNAME = cmbregister.Text.Trim
                    OBJREC.PRINTSETTING = PRINTDIALOG
                    OBJREC.recname = ROW("NAME")
                    OBJREC.recno = Val(ROW("SRNO"))
                    OBJREC.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJREC.Show()
                    OBJREC.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\" & ROW("NAME") & "\RECEIPT_" & Val(ROW("SRNO")) & ".pdf")
                    FILENAME.Add(ROW("NAME") & "RECEIPT_" & Val(ROW("SRNO")) & ".pdf")



                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" register_name,ISNULL (REGISTERMASTER.register_id, 0) AS Registerid ", "", " RegisterMaster ", " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' and register_YEARid = " & YearId)
                    'ADDINT IN DTEMAIL
                    DTMAIL.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("Registerid"), cmbregister.Text.Trim, ROW("INITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYEMAILID"), ROW("AGENTNAME"), ROW("AGENTEMAILID"), "", UCase(CmpName) & " - REC No. " & ROW("INITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "RECEIPT_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "RECEIPT_" & Val(ROW("SRNO")) & ".pdf")
                    'ADDING IN WHATSAPP

                    DTWHATSAPP.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("Registerid"), cmbregister.Text.Trim, ROW("INITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("AGENTNAME"), ROW("AGENTWHATSAPP"), "", UCase(CmpName) & " - Rec No. " & ROW("INITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "RECEIPT_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "RECEIPT_" & Val(ROW("SRNO")) & ".pdf")

                    If INVOICEMAIL = False And WHATSAPP = False Then DT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER SET RECEIPT_PRINT = 1 FROM RECEIPTMASTER INNER JOIN REGISTERMASTER ON RECEIPT_REGISTERID = REGISTER_ID WHERE RECEIPT_NO = " & Val(ROW("SRNO")) & " and REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND RECEIPT_YEARID = " & YearId, "", "")
                End If

            Next

            'If INVOICEMAIL Then
            '    Dim OBJMAIL As New SendMail
            '    OBJMAIL.ALATTACHMENT = ALATTACHMENT
            '    OBJMAIL.subject = "RECEIPT"
            '    OBJMAIL.ShowDialog()
            'End If
            If INVOICEMAIL = True Then
                If DTMAIL.Rows.Count = 0 Then Exit Sub
                Dim OBJEMAIL As New SendMultipleMail
                OBJEMAIL.FORMTYPE = "INVOICE"
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
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " And register_type = 'RECEIPT'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'RECEIPT' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub CHKMULTI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKMULTI.CheckedChanged
    '    Try
    '        If CHKMULTI.Checked = True Then
    '            multi = True
    '            TXTFROM.Focus()
    '        Else
    '            multi = False
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
End Class