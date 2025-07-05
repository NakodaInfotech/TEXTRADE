

Imports BL

Public Class AgencyCreditNoteDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim JVREGID As Integer

    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" AGENCYCREDITNOTEMASTER.ACN_no AS SRNO, AGENCYCREDITNOTEMASTER.ACN_date AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT,  (CASE WHEN ISNULL(AGENCYCREDITNOTEMASTER.ACN_BILLNO,'') <> '' THEN ISNULL(AGENCYCREDITNOTEMASTER.ACN_BILLNO,'') ELSE ISNULL(STUFF((SELECT ', ' + ACN_BILLINITIALS  [text()] FROM AGENCYCREDITNOTEMASTER_BILLDESC AS A WHERE A.ACN_NO = AGENCYCREDITNOTEMASTER.ACN_NO AND A.ACN_YEARID = AGENCYCREDITNOTEMASTER.ACN_YEARID FOR XML PATH(''), TYPE) .value('.','NVARCHAR(MAX)'),1,2,' '),'') END) AS BILLNO, DEBITLEDGERS.Acc_cmpname AS DEBITNAME, AGENCYCREDITNOTEMASTER.ACN_GTOTAL AS AMT, AGENCYCREDITNOTEMASTER.ACN_remarks AS REMARKS, ISNULL(STATEMASTER.state_name, '') AS STATENAME, ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(HSNMASTER.HSN_ITEMDESC, '') AS HSNITEMDESC, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(AGENCYCREDITNOTEMASTER.ACN_TOTALAMT, 0) AS BILLAMT, ISNULL(AGENCYCREDITNOTEMASTER.ACN_CHARGES, 0) AS CHGS, ISNULL(AGENCYCREDITNOTEMASTER.ACN_SUBTOTAL, 0) AS SUBTOTAL, ISNULL(AGENCYCREDITNOTEMASTER.ACN_CGSTAMT, 0) AS TOTALCGSTAMT, ISNULL(AGENCYCREDITNOTEMASTER.ACN_SGSTAMT, 0) AS TOTALSGSTAMT,  ISNULL(AGENCYCREDITNOTEMASTER.ACN_IGSTAMT, 0) AS TOTALIGSTAMT, ISNULL(AGENCYCREDITNOTEMASTER.ACN_ROUNDOFF, 0) AS ROUNDOFF, ISNULL(AGENCYCREDITNOTEMASTER.ACN_PARTYREFNO, '') AS PARTYREFNO, ISNULL(AGENCYCREDITNOTEMASTER.ACN_RCM, 0) AS RCM, ISNULL(AGENCYCREDITNOTEMASTER.ACN_NOGSTR1, 0) AS NOGSTR1, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '') AS DELIVERYAT, ISNULL(GROUPMASTER.group_name, '') AS GROUPNAME, ISNULL(AGENCYCREDITNOTEMASTER.ACN_IRNNO, '') AS IRNNO, ISNULL(AGENCYCREDITNOTEMASTER.ACN_ACKNO, '') AS CNACKNO, AGENCYCREDITNOTEMASTER.ACN_ACKDATE AS CNACKDATE, ISNULL(LEDGERS.ACC_WHATSAPPNO, '0') AS PARTYWHATSAAP, ISNULL(LEDGERS.Acc_email, '') AS PARTYEMAIL, ISNULL(AGENTLEDGERS.Acc_email, '') AS AGENTEMAIL, ISNULL(AGENTLEDGERS.ACC_WHATSAPPNO, '0') AS AGENTWHATSAAP, AGENCYCREDITNOTEMASTER.ACN_SPECIALREMARKS AS SPECIALREMARK, ISNULL(COSTCENTERMASTER.COSTCENTER_name, '') AS COSTCENTERNAME , ISNULL(USERMASTER.User_Name,'') AS CREATEDBY ", "", "  AGENCYCREDITNOTEMASTER INNER JOIN LEDGERS ON AGENCYCREDITNOTEMASTER.ACN_LEDGERID = LEDGERS.Acc_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON AGENCYCREDITNOTEMASTER.ACN_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON AGENCYCREDITNOTEMASTER.ACN_DELIVERYATID = DELIVERYLEDGERS.Acc_id INNER JOIN LEDGERS AS DEBITLEDGERS ON AGENCYCREDITNOTEMASTER.ACN_DEBITLEDGERID = DEBITLEDGERS.Acc_id LEFT OUTER JOIN COSTCENTERMASTER ON AGENCYCREDITNOTEMASTER.ACN_COSTCENTERNAMEID = COSTCENTERMASTER.COSTCENTER_id LEFT OUTER JOIN HSNMASTER ON AGENCYCREDITNOTEMASTER.ACN_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN USERMASTER ON AGENCYCREDITNOTEMASTER.ACN_userid = USERMASTER.User_id", " AND ACN_YEARID = " & YearId & " order by dbo.AGENCYCREDITNOTEMASTER.ACN_NO")
            griddetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridCN.FocusedRowHandle = gridCN.RowCount - 1
                gridCN.TopRowIndex = gridCN.RowCount - 15
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
            If (editval = False) Or (editval = True And gridCN.RowCount > 0) Then
                Dim objCN As New AgencyCreditNote
                objCN.MdiParent = MDIMain
                objCN.edit = editval
                objCN.TEMPCNNO = billno
                'objCN.TEMPREGNAME = cmbregister.Text.Trim
                objCN.Show()
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

    Private Sub gridjournal_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridCN.DoubleClick
        Try
            showform(True, gridCN.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
    '    Try
    '        If USEREDIT = False And USERVIEW = False Then
    '            MsgBox("Insufficient Rights")
    '            Exit Sub
    '        End If
    '        If cmbregister.Text.Trim.Length > 0 Then
    '            cmbregister.Text = UCase(cmbregister.Text)
    '            Dim clscommon As New ClsCommon
    '            Dim dt As DataTable
    '            dt = clscommon.SEARCH(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'CREDITNOTE' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
    '            If dt.Rows.Count > 0 Then
    '                JVREGID = dt.Rows(0).Item(0)
    '                fillgrid(" and REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' ")
    '            Else
    '                MsgBox("Register Not Present, Add New from Master Module")
    '                e.Cancel = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub CreditNoteDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.N) Or (e.Alt = True And e.KeyCode = Windows.Forms.Keys.A) Then       'for AddNew 
                showform(False, 0)
            ElseIf e.KeyCode = Keys.E And e.Alt = True Then
                CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreditNoteDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'CREDIT NOTE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridCN.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Credit Note Register.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = ""
            PERIOD = AccFrom & " - " & AccTo

            opti.SheetName = "Credit Note Register"
            gridCN.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Credit Note Register", gridCN.VisibleColumns.Count + gridCN.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("Credit Note Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CreditNoteDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "SVS" Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTFROM.Validated
        If TXTFROM.Text.Trim <> "" Then TXTTO.Focus()
    End Sub

    Private Sub TOOLMAIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLMAIL.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridCN.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Credit Note Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail Credit Note from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(True)
                End If
            Else
                If MsgBox("Wish to Mail Selected Credit Note ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridCN.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Credit Note Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Credit Note from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Credit Note ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(False, True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridCN.SelectedRowsCount = 0 Then Exit Sub


            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Credit Note Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Wish to Print Credit Note from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPDIRECT()
                End If
            Else
                If MsgBox("Wish to Print Selected Credit Note ?", MsgBoxStyle.YesNo) = vbYes Then
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
                Dim OBJCN As New CrDrNoteDesign
                OBJCN.MdiParent = MDIMain
                OBJCN.DIRECTPRINT = True
                OBJCN.FRMSTRING = "CREDIT"
                OBJCN.DIRECTMAIL = INVOICEMAIL
                OBJCN.DIRECTWHATSAPP = WHATSAPP
                'OBJCN.REGNAME = cmbregister.Text.Trim
                OBJCN.PRINTSETTING = PRINTDIALOG
                OBJCN.BILLNO = Val(I)
                OBJCN.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJCN.Show()
                OBJCN.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\ACN_" & I & ".pdf")
                FILENAME.Add("ACN_" & I & ".pdf")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Credit Note"
                OBJMAIL.ShowDialog()
            End If

            If WHATSAPP = True Then
                Dim OBJWHATSAPP As New SendWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPSELECTED(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList

            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            Dim SELECTEDROWS As Int32() = gridCN.GetSelectedRows()
            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                Dim ROW As DataRow = gridCN.GetDataRow(SELECTEDROWS(I))

                Dim OBJCN As New CrDrNoteDesign
                OBJCN.MdiParent = MDIMain
                OBJCN.DIRECTPRINT = True
                OBJCN.FRMSTRING = "CREDIT"
                OBJCN.DIRECTMAIL = INVOICEMAIL
                OBJCN.DIRECTWHATSAPP = WHATSAPP
                'OBJCN.REGNAME = cmbregister.Text.Trim
                OBJCN.PRINTSETTING = PRINTDIALOG
                OBJCN.BILLNO = Val(ROW("SRNO"))
                OBJCN.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJCN.Show()
                OBJCN.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\ACN_" & Val(ROW("SRNO")) & ".pdf")
                FILENAME.Add("ACN_" & Val(ROW("SRNO")) & ".pdf")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Credit Note"
                OBJMAIL.ShowDialog()
            End If

            If WHATSAPP = True Then
                Dim OBJWHATSAPP As New SendWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub cmbregister_Enter(sender As Object, e As EventArgs)
    '    Try
    '        If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'CREDITNOTE'")

    '        Dim clscommon As New ClsCommon
    '        Dim dt As DataTable
    '        dt = clscommon.SEARCH(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'CREDITNOTE' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
    '        If dt.Rows.Count > 0 Then
    '            cmbregister.Text = dt.Rows(0).Item(0).ToString
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

End Class