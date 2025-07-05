
Imports BL

Public Class ExpenseVoucherDetails

    Public FRMSTRING As String
    Dim PURCHASEREGID As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub NonPurchaseDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Alt = True Then
                showform(False, 0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, NONPURCHASE.NP_NO AS [Sr. No], NONPURCHASE.NP_DATE AS Date, LEDGERS.Acc_cmpname AS [Party Name], NONPURCHASE.NP_REFNO AS PartyBillNo, NONPURCHASE.NP_PARTYBILLDATE AS PartyBillDate, NONPURCHASE.NP_TOTALAMT AS [Total Amt.], NONPURCHASE.NP_REMARKS AS Remarks,  ISNULL(NONPURCHASE.NP_RCM, 0) AS RCM, ISNULL(NONPURCHASE.NP_MANUALGST, 0) AS MANUALGST, ISNULL(NONPURCHASE.NP_TOTALOTHERAMT, 0)  AS TOTALOTHERAMT, ISNULL(NONPURCHASE.NP_TOTALTAXABLEAMT, 0) AS TOTALTAXABLEAMT, ISNULL(NONPURCHASE.NP_TOTALCGSTAMT, 0) AS TOTALCGSTAMT, ISNULL(NONPURCHASE.NP_TOTALSGSTAMT, 0) AS TOTALSGSTAMT, ISNULL(NONPURCHASE.NP_TOTALIGSTAMT, 0) AS TOTALIGSTAMT, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(STATEMASTER.state_name, '') AS STATENAME, ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(NONPURCHASE.NP_GRANDTOTAL, 0) AS GRANDTOTAL, ISNULL(NONPURCHASE.NP_APPLYTCS, 0) AS APPLYTCS, ISNULL(NONPURCHASE.NP_TCSPER, 0) AS TCSPER, ISNULL(NONPURCHASE.NP_TCSAMT, 0) AS TCSAMT, ISNULL(NONPURCHASE.NP_AMTPAID, 0) AS PAIDAMT, ISNULL(NONPURCHASE.NP_RETURN, 0) AS [RETURN], ISNULL(NONPURCHASE.NP_BALANCE, 0) AS BALANCE, ISNULL(COSTCENTERMASTER.COSTCENTER_name, '') AS COSTCENTERNAME , ISNULL(NONPURCHASE.NP_BILLCHECKED, 0) AS BILLCHECKED, ISNULL(NONPURCHASE.NP_BILLDISPUTE, 0) AS BILLDISPUTE ,ISNULL(USERMASTER.User_Name, '') AS CREATEDBY, ISNULL(NONPURCHASE.NP_SELFINVNO,0) AS SELFINVNO, ISNULL(NONPURCHASE.NP_SELFINVPRINTINITIALS,'') AS SELFINVPRINTINITIALS", "", " NONPURCHASE INNER JOIN LEDGERS ON NONPURCHASE.NP_LEDGERID = LEDGERS.Acc_id AND NONPURCHASE.NP_CMPID = LEDGERS.Acc_cmpid AND NONPURCHASE.NP_LOCATIONID = LEDGERS.Acc_locationid AND NONPURCHASE.NP_YEARID = LEDGERS.Acc_yearid LEFT OUTER JOIN USERMASTER ON NONPURCHASE.NP_USERID = USERMASTER.User_id LEFT OUTER JOIN COSTCENTERMASTER ON NONPURCHASE.NP_COSTCENTERNAMEID = COSTCENTERMASTER.COSTCENTER_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id ", TEMPCONDITION)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal NPno As Integer)
        Try

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJNP As New ExpenseVoucher
                OBJNP.MdiParent = MDIMain
                OBJNP.EDIT = editval
                OBJNP.TEMPEXPNO = NPno
                OBJNP.TEMPREGNAME = cmbregister.Text.Trim
                OBJNP.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'EXPENSE'")
            Dim clscommon As New ClsCommon
            Dim dt As DataTable = clscommon.SEARCH(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'EXPENSE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
            If dt.Rows.Count > 0 Then cmbregister.Text = dt.Rows(0).Item(0).ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try

            If cmbregister.Text.Trim.Length > 0 Then
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.SEARCH(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'EXPENSE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    PURCHASEREGID = dt.Rows(0).Item(0)
                    FILLGRID(" and dbo.NONPURCHASE.NP_cmpid=" & CmpId & " and NONPURCHASE.NP_locationid = " & Locationid & " and NONPURCHASE.NP_yearid = " & YearId & " AND NONPURCHASE.NP_registerid = " & PURCHASEREGID & " order by dbo.NONPURCHASE.NP_no ")
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

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("Sr. No"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDETAILS.Click
        Try
            Dim OBJEXP As New ExpenseVoucherGridDetails
            OBJEXP.MdiParent = MDIMain
            OBJEXP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub gridNP_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("Sr. No"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub ExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Expense Register Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = ""
            PERIOD = AccFrom & " - " & AccTo

            opti.SheetName = "Expense Register Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Expense Register Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("Expense Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ExpenseVoucherDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "SVS" Then Me.Close()
    End Sub

    Private Sub TOOLMAIL_Click(sender As Object, e As EventArgs) Handles TOOLMAIL.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Expense Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail Expense from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(True)
                End If
            Else
                If MsgBox("Wish to Mail Selected Expense ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Expense Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Expense from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Expense ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(False, True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub


            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Expense Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Wish to Print Expense from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPDIRECT()
                End If
            Else
                If MsgBox("Wish to Print Selected Expense Note ?", MsgBoxStyle.YesNo) = vbYes Then
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
                If PrintDialog.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PrintDialog.PrinterSettings Else Exit Sub
            End If

            For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                Dim OBJJV As New NonPurchaseDesign
                OBJJV.MdiParent = MDIMain

                'PASSING DR NAME AS PARTNAME
                Dim OBJCMN As New ClsCommon
                Dim DTLEDGER As DataTable = OBJCMN.SEARCH("ISNULL(NP_SELFINVNO,0) AS SELFINVNO", "", " NONPURCHASE ", " AND NP_NO = " & Val(I) & " AND NP_YEARID = " & YearId)
                If DTLEDGER.Rows.Count > 0 Then OBJJV.NPNO = Val(DTLEDGER.Rows(0).Item("SELFINVNO"))

                OBJJV.DIRECTPRINT = True
                OBJJV.FRMSTRING = "SELFINV"
                OBJJV.DIRECTMAIL = INVOICEMAIL
                OBJJV.DIRECTWHATSAPP = WHATSAPP
                OBJJV.REGNAME = cmbregister.Text.Trim
                OBJJV.PRINTSETTING = PRINTDIALOG
                OBJJV.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJJV.STRSEARCH = "{NONPURCHASE.NP_NO}=" & Val(I) & " And {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "' AND {NONPURCHASE.NP_YEARID} = " & YearId
                OBJJV.Show()
                OBJJV.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\SELFINVOICE_" & Val(DTLEDGER.Rows(0).Item("SELFINVNO")) & ".pdf")
                FILENAME.Add("SELFINVOICE_" & Val(DTLEDGER.Rows(0).Item("SELFINVNO")) & ".pdf")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "SELFINVOICE"
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
                If PrintDialog.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PrintDialog.PrinterSettings Else Exit Sub
            End If
            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                Dim ROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

                Dim OBJJV As New NonPurchaseDesign
                OBJJV.MdiParent = MDIMain
                OBJJV.DIRECTPRINT = True
                OBJJV.FRMSTRING = "SELFINV"
                OBJJV.DIRECTMAIL = INVOICEMAIL
                OBJJV.DIRECTWHATSAPP = WHATSAPP
                OBJJV.REGNAME = cmbregister.Text.Trim
                OBJJV.PRINTSETTING = PRINTDIALOG
                OBJJV.NPNO = Val(ROW("SELFINVNO"))
                OBJJV.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJJV.STRSEARCH = "{NONPURCHASE.NP_NO} = " & Val(ROW("Sr. No")) & " And {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "' AND {NONPURCHASE.NP_YEARID} = " & YearId

                If ClientName = "NAKODAINFOTECH" Then
                    'PASSING DR NAME AS PARTNAME
                    Dim OBJCMN As New ClsCommon
                    Dim DTLEDGER As DataTable = OBJCMN.SEARCH("TOP 1 LEDGERS.ACC_CMPNAME AS PARTYNAME", "", " NONPURCHASE INNER JOIN LEDGERS ON NP_LEDGERID = LEDGERS.ACC_ID ", " AND NP_NO = " & Val(ROW("Sr. No")) & " AND NP_TYPE = 'Dr' AND NP_YEARID = " & YearId)
                    If DTLEDGER.Rows.Count > 0 Then OBJJV.PARTYNAME = DTLEDGER.Rows(0).Item("PARTYNAME")
                End If


                OBJJV.Show()
                OBJJV.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\SELFINVOICE_" & Val(ROW("SELFINVNO")) & ".pdf")
                FILENAME.Add("SELFINVOICE_" & Val(ROW("SELFINVNO")) & ".pdf")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "SELFINVOICE"
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

End Class