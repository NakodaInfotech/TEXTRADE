
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class YarnPurchaseOrderDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim DTMAIL As New DataTable
    Dim DTWHATSAPP As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub YarnPurchaseOrderDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub YarnPurchaseOrderDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'PURCHASE ORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            DTMAIL.Columns.Add("INVNO")
            DTMAIL.Columns.Add("REGID")
            DTMAIL.Columns.Add("REGNAME")
            DTMAIL.Columns.Add("PRINTINITIALS")
            DTMAIL.Columns.Add("PODATE")
            DTMAIL.Columns.Add("NAME")
            DTMAIL.Columns.Add("PARTYEMAILID")
            DTMAIL.Columns.Add("AGENTNAME")
            DTMAIL.Columns.Add("AGENTEMAILID")
            DTMAIL.Columns.Add("GRANDTOTAL")
            DTMAIL.Columns.Add("SUBJECT")
            DTMAIL.Columns.Add("ATTACHMENT")
            DTMAIL.Columns.Add("FILENAME")

            DTWHATSAPP.Columns.Add("INVNO")
            DTWHATSAPP.Columns.Add("REGID")
            DTWHATSAPP.Columns.Add("REGNAME")
            DTWHATSAPP.Columns.Add("PRINTINITIALS")
            DTWHATSAPP.Columns.Add("PODATE")
            DTWHATSAPP.Columns.Add("NAME")
            DTWHATSAPP.Columns.Add("PARTYWHATSAPP")
            DTWHATSAPP.Columns.Add("AGENTNAME")
            DTWHATSAPP.Columns.Add("AGENTWHATSAPP")
            DTWHATSAPP.Columns.Add("GRANDTOTAL")
            DTWHATSAPP.Columns.Add("SUBJECT")
            DTWHATSAPP.Columns.Add("ATTACHMENT")
            DTWHATSAPP.Columns.Add("FILENAME")

            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            'Dim OBJPO As New ClsYarnPurchaseOrder
            'Dim dt As DataTable = OBJPO.SELECTPO(0, CmpId, 0, YearId)
            'gridbilldetails.DataSource = dt
            'If dt.Rows.Count > 0 Then
            '    gridbill.FocusedRowHandle = gridbill.RowCount - 1
            '    gridbill.TopRowIndex = gridbill.RowCount - 15
            'End If

            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, CAST(0 AS BIT) AS CHK,ISNULL(YARNPURCHASEORDER.YPO_NO, 0) AS PONO, YARNPURCHASEORDER.YPO_DATE AS PODATE, YARNPURCHASEORDER.YPO_DUEDATE AS DUEDATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(YARNPURCHASEORDER.YPO_CRDAYS, 0) AS CRDAYS, ISNULL(YARNPURCHASEORDER.YPO_DELDAYS, 0) AS DELDAYS, ISNULL(YARNPURCHASEORDER.YPO_REFNO, '') AS REFNO, ISNULL(YARNPURCHASEORDER.YPO_DISCOUNT, 0) AS DISCOUNT, ISNULL(YARNPURCHASEORDER.YPO_REMARKS, '') AS REMARKS, ISNULL(YARNPURCHASEORDER.YPO_ORDERTYPE, '') AS ORDERTYPE, ISNULL(YARNPURCHASEORDER.YPO_LBLTOTALAMT, 0) AS LBLTOTALPCS, ISNULL(YARNPURCHASEORDER.YPO_INWORDS, '') AS INWORDS, ISNULL(BROKERLEDGERS.Acc_cmpname, '') AS BROKER, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(YARNPURCHASEORDER_DESC.YPO_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(YARNPURCHASEORDER_DESC.YPO_DESC, '') AS [DESC], ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(MILLMASTER.MILL_NAME, '') AS MILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(YARNPURCHASEORDER_DESC.YPO_PCOLOR, '')AS PSHADE, ISNULL(YARNPURCHASEORDER_DESC.YPO_BAGS, 0) AS BAGS, ISNULL(YARNPURCHASEORDER_DESC.YPO_WT, 0) AS WT, ISNULL(YARNPURCHASEORDER_DESC.YPO_CONES, 0) CONES, YARNQUALITYMASTER.YARN_NAME AS YARNQUALITY, ISNULL(LEDGERS.Acc_mobile, '') AS MOBILENO, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(YARNPURCHASEORDER_DESC.YPO_RATE, 0) AS RATE, ISNULL(YARNPURCHASEORDER_DESC.YPO_AMT, 0) AS AMT, ISNULL(YARNPURCHASEORDER_DESC.YPO_RECDBAGS, 0) AS RECDBAGS, ISNULL(YARNPURCHASEORDER_DESC.YPO_RECDWT, 0) AS RECDWT, ISNULL(YARNPURCHASEORDER_DESC.YPO_DONE, 0) AS DONE, ISNULL(YARNPURCHASEORDER_DESC.YPO_CLOSED, 0) AS CLOSED ,ISNULL(YARNPURCHASEORDER_DESC.YPO_BAGS -YARNPURCHASEORDER_DESC.YPO_RECDBAGS, 0) as BALBAG , ISNULL(YARNPURCHASEORDER_DESC.YPO_WT - YARNPURCHASEORDER_DESC.YPO_RECDWT, 0) AS BALWT,ISNULL(LEDGERS.Acc_email, '') AS PARTYMAIL, ISNULL(LEDGERS.ACC_WHATSAPPNO, '') AS PARTYWHATSAPP ", "", " YARNPURCHASEORDER INNER JOIN YARNPURCHASEORDER_DESC ON YARNPURCHASEORDER.YPO_NO = YARNPURCHASEORDER_DESC.YPO_NO AND YARNPURCHASEORDER.YPO_YEARID = YARNPURCHASEORDER_DESC.YPO_YEARID LEFT OUTER JOIN DESIGNMASTER ON YARNPURCHASEORDER_DESC.YPO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON YARNPURCHASEORDER_DESC.YPO_SHADEID = COLORMASTER.COLOR_id LEFT OUTER JOIN MILLMASTER ON YARNPURCHASEORDER_DESC.YPO_MILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN YARNQUALITYMASTER ON YARNPURCHASEORDER_DESC.YPO_YARNQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN LEDGERS AS BROKERLEDGERS ON YARNPURCHASEORDER.YPO_BROKERID = BROKERLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON YARNPURCHASEORDER.YPO_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON YARNPURCHASEORDER.YPO_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN UNITMASTER ON YARNPURCHASEORDER_DESC.YPO_UNITID = UNITMASTER.unit_id ", " AND YARNPURCHASEORDER.YPO_YEARID = " & YearId & " ORDER BY YARNPURCHASEORDER_DESC.YPO_GRIDSRNO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal PONO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New YarnPurchaseOrder
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.tempono = PONO
                objPO.Show()
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

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("PONO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("PONO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("CLOSED")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("DONE")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightGreen
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Purchase Order Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Purchase Order Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Purchase Order Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Yarn PO Details Excel File Is Open, Please Close the File first Then Try To Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub


            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Yarn PO Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Wish To Print Yarn PO from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPDIRECT()
                End If
            Else
                If MsgBox("Wish To Print Selected Yarn PO ?", MsgBoxStyle.YesNo) = vbYes Then
                    cmdok.Focus()
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
                Dim OBJINVOICE As New PurchaseOrderDesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.DIRECTPRINT = True
                OBJINVOICE.FRMSTRING = "YARNPOREPORT"
                OBJINVOICE.DIRECTMAIL = INVOICEMAIL
                OBJINVOICE.DIRECTWHATSAPP = WHATSAPP

                'OBJINVOICE.INVOICECOPYNAME = TOOLCMBINVCOPY.Text.Trim
                'If (ClientName = "SOFTAS" Or ClientName = "MANS") And TOOLCMBINVCOPY.Text = "OFFICE COPY" Then OBJINVOICE.INVOICECOPYNAME = "AGENT COPY"
                'If (ClientName = "RMANILAL" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "TARUN" Or ClientName = "SHANTI" Or ClientName = "KUNAL" Or ClientName = "VALIANT") And TOOLCMBINVCOPY.Text = "DUPLICATE COPY" Then OBJINVOICE.INVOICECOPYNAME = "AGENT COPY"
                'If ClientName = "ALENCOT" And TOOLCMBINVCOPY.Text = "DUPLICATE COPY" Then OBJINVOICE.INVOICECOPYNAME = "REVISED COPY"
                'If ClientName = "GELATO" And TOOLCMBINVCOPY.Text = "TRANSPORT COPY" Then OBJINVOICE.INVOICECOPYNAME = "DUPLICATE For TRANSPORT"

                'If TOOLCMBINVCOPY.Text = "TRANSPORT COPY" Then OBJINVOICE.INVOICETRANS = True
                'If TOOLCMBINVCOPY.Text = "RETAIL COPY (A5)" Then OBJINVOICE.INVOICERETAIL = True
                'If TOOLCMBINVCOPY.Text = "YARN Do" Then OBJINVOICE.FRMSTRING = "YARNDO"


                'Dim OBJCMN As New ClsCommon
                'Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STATE_REMARK,'') AS STATECODE", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID ", " AND INVOICEMASTER.INVOICE_NO = " & Val(I) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                'If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STATECODE") <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                'OBJINVOICE.registername = cmbregister.Text.Trim
                OBJINVOICE.PRINTSETTING = PrintDialog
                OBJINVOICE.PONO = Val(I)
                OBJINVOICE.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                'OBJINVOICE.BLANKPAPER = CHKBLANKPAPER.Checked
                OBJINVOICE.Show()
                OBJINVOICE.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\POREPORT_" & I & ".pdf")
                FILENAME.Add("POREPORT_" & I & ".pdf")
                'DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_SENDWHATSAPP = 1, INVOICE_PRINT = 1 FROM InvoiceMaster INNER JOIN REGISTERMASTER On INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id WHERE INVOICE_NO = " & I & " AND REGISTER_NAME '" & cmbregister.Text.Trim & "'  AND INVOICE_YEARID = " & YearId, "", "")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Invoice"
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

    Private Sub TOOLMAIL_Click(sender As Object, e As EventArgs) Handles TOOLMAIL.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Yarn PO  Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail Yarn PO  from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(True)
                End If
            Else
                If MsgBox("Wish to Mail Selected Yarn PO  ?", MsgBoxStyle.YesNo) = vbYes Then
                    cmdok.Focus()
                    SERVERPROPSELECTED(True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Yarn PO Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Yarn PO  from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Yarn PO  ?", MsgBoxStyle.YesNo) = vbYes Then
                    cmdok.Focus()
                    SERVERPROPSELECTED(False, True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPSELECTED(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            Dim partyFiles As New Dictionary(Of String, List(Of String)) 'mobile → pdf list
            Dim partyRows As New Dictionary(Of String, DataRow) 'mobile → store party info
            DTMAIL.Rows.Clear()
            DTWHATSAPP.Rows.Clear()


            If INVOICEMAIL = False And WHATSAPP = False Then
                If PrintDialog.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PrintDialog.PrinterSettings Else Exit Sub
            End If
            'Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(gridbill.RowCount - 1)
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                If ROW("CHK") = True Then
                    Dim OBJINVOICE As New PurchaseOrderDesign
                    OBJINVOICE.MdiParent = MDIMain
                    OBJINVOICE.DIRECTPRINT = True
                    OBJINVOICE.FRMSTRING = "YARNPOREPORT"
                    OBJINVOICE.DIRECTMAIL = INVOICEMAIL
                    OBJINVOICE.DIRECTWHATSAPP = WHATSAPP
                    'If TOOLCMBINVCOPY.Text = "TRANSPORT COPY" Then OBJINVOICE.INVOICETRANS = True
                    'If TOOLCMBINVCOPY.Text = "RETAIL COPY (A5)" Then OBJINVOICE.INVOICERETAIL = True
                    'If TOOLCMBINVCOPY.Text = "YARN DO" Then OBJINVOICE.FRMSTRING = "YARNDO"

                    'OBJINVOICE.INVOICECOPYNAME = TOOLCMBINVCOPY.Text.Trim
                    'If (ClientName = "SOFTAS" Or ClientName = "MANS") And TOOLCMBINVCOPY.Text = "OFFICE COPY" Then OBJINVOICE.INVOICECOPYNAME = "AGENT COPY"
                    'If (ClientName = "RMANILAL" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "TARUN" Or ClientName = "SHANTI" Or ClientName = "KUNAL" Or ClientName = "VALIANT") And TOOLCMBINVCOPY.Text = "DUPLICATE COPY" Then OBJINVOICE.INVOICECOPYNAME = "AGENT COPY"
                    'If ClientName = "ALENCOT" And TOOLCMBINVCOPY.Text = "DUPLICATE COPY" Then OBJINVOICE.INVOICECOPYNAME = "REVISED COPY"
                    'If ClientName = "GELATO" And TOOLCMBINVCOPY.Text = "TRANSPORT COPY" Then OBJINVOICE.INVOICECOPYNAME = "DUPLICATE FOR TRANSPORT"

                    OBJINVOICE.PARTYNAME = ROW("NAME")
                    'OBJINVOICE.AGENTNAME = ROW("AGENTNAME")
                    'Dim OBJCMN As New ClsCommon
                    'Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STATE_REMARK,'') AS STATECODE, ISNULL(REGISTERMASTER.REGISTER_ID,0) AS REGID", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID ", " AND INVOICEMASTER.INVOICE_NO = " & Val(ROW("SRNO")) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                    'If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STATECODE") <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                    'OBJINVOICE.registername = cmbregister.Text.Trim
                    OBJINVOICE.PRINTSETTING = PrintDialog
                    OBJINVOICE.PONO = Val(ROW("PONO"))
                    OBJINVOICE.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    'OBJINVOICE.BLANKPAPER = CHKBLANKPAPER.Checked
                    OBJINVOICE.Show()
                    OBJINVOICE.Close()


                    ALATTACHMENT.Add(Application.StartupPath & "\" & ROW("NAME") & "POREPORT_" & Val(ROW("PONO")) & ".pdf")
                    FILENAME.Add(ROW("NAME") & "POREPORT_" & Val(ROW("PONO")) & ".pdf")
                    Dim invoicePath As String = ALATTACHMENT(ALATTACHMENT.Count - 1).ToString

                    'ADDINT IN DTEMAIL
                    DTMAIL.Rows.Add(ROW("PONO"), ROW("PONO"), "", "", ROW("PODATE"), ROW("NAME"), ROW("PARTYMAIL"), "", "", 0, UCase(CmpName) & " - POREPORT. " & ROW("PONO") & " Dated " & ROW("PODATE"), Application.StartupPath & "\" & ROW("NAME") & "POREPORT_" & Val(ROW("PONO")) & ".pdf", ROW("NAME") & "POREPORT_" & Val(ROW("PONO")) & ".pdf")

                    'ADDING IN DTWHATSAPP
                    'If ClientName = "MAHAVIRPOLYCOT" Then ROW("AGENTWHATSAPP") = ""
                    'If CHKMERGEDPDF.CheckState = False Then

                    'DTWHATSAPP.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("REGID"), cmbregister.Text.Trim, ROW("PRINTINITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("AGENTNAME"), ROW("AGENTWHATSAPP"), Val(ROW("GRANDTOTAL")), UCase(CmpName) & " - Invoice No. " & ROW("PRINTINITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf")

                End If
                'DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_SENDWHATSAPP = 1, INVOICE_PRINT = 1 FROM InvoiceMaster INNER JOIN REGISTERMASTER On INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id WHERE INVOICE_NO = " & Val(ROW("SRNO")) & " AND REGISTER_NAME '" & cmbregister.Text.Trim & "'  AND INVOICE_YEARID = " & YearId, "", "")

            Next
            'If CHKMERGEDPDF.CheckState = True Then
            '    If INVOICEMAIL Then
            '        If ALATTACHMENT.Count = 0 Then Exit Sub

            '        'Convert ArrayList to List(Of String)
            '        Dim pdfFiles As New List(Of String)
            '        For Each f As String In ALATTACHMENT
            '            pdfFiles.Add(f)
            '        Next

            '        'Merged output file
            '        Dim mergedPath As String = Application.StartupPath & "\MERGED_INVOICES.pdf"

            '        'Merge PDFs
            '        MergePDFFiles(pdfFiles, mergedPath)

            '        'Clear old list and send only merged file
            '        ALATTACHMENT.Clear()
            '        FILENAME.Clear()

            '        ALATTACHMENT.Add(mergedPath)
            '        FILENAME.Add("Invoices.pdf")

            '        Dim OBJEMAIL As New SendMultipleMail
            '        OBJEMAIL.FORMTYPE = "INVOICE"
            '        OBJEMAIL.DT = DTMAIL
            '        OBJEMAIL.ShowDialog()
            '    End If
            'Else
            If INVOICEMAIL Then
                If DTMAIL.Rows.Count = 0 Then Exit Sub
                Dim OBJEMAIL As New SendMultipleMail
                OBJEMAIL.FORMTYPE = "INVOICE"
                OBJEMAIL.DT = DTMAIL
                OBJEMAIL.ShowDialog()
                Exit Sub
            End If
            'End If
            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Invoice"
                OBJMAIL.ShowDialog()
            End If
            'If CHKMERGEDPDF.Checked AndAlso WHATSAPP Then
            '    If WHATSAPP = True Then
            '        If ALATTACHMENT.Count = 0 Then Exit Sub

            '        Dim pdfFiles As New List(Of String)
            '        For Each f As String In ALATTACHMENT
            '            pdfFiles.Add(f)
            '        Next

            '        'Dim mergedPath As String = Application.StartupPath & "\MERGED_INVOICES.pdf"
            '        'MergePDFFiles(pdfFiles, mergedPath)

            '        'Send only one row
            '        DTWHATSAPP.Rows.Clear()
            '        DTWHATSAPP.Rows.Add(0, 0, cmbregister.Text.Trim, "", Now.Date,
            '            "Multiple Invoices", "", "", "", 0,
            '            "Invoices Attached", mergedPath, "Invoices.pdf")

            '        ALATTACHMENT.Clear()
            '        FILENAME.Clear()
            '        ALATTACHMENT.Add(mergedPath)
            '        FILENAME.Add("Invoices.pdf")


            '        Dim OBJWHATSAPP As New SendMultipleWhatsapp
            '        OBJWHATSAPP.PATH = ALATTACHMENT
            '        OBJWHATSAPP.FILENAME = FILENAME
            '        OBJWHATSAPP.DT = DTWHATSAPP
            '        OBJWHATSAPP.ShowDialog()
            '    End If
            'Else


            If WHATSAPP = True Then
                If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
                Dim OBJWHATSAPP As New SendMultipleWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.DT = DTWHATSAPP
                OBJWHATSAPP.ShowDialog()
            End If
            'End If


            'FOR MERGING MULTIPLE PDF
            'Dim pdfReaderList As List(Of PdfReader) = New List(Of PdfReader)()
            'For i As Integer = 0 To ALATTACHMENT.Count - 1
            '    Dim pdfReader As PdfReader = New PdfReader(ALATTACHMENT(i).ToString)
            '    pdfReaderList.Add(pdfReader)
            'Next

            'Dim document As Document = New Document(PageSize.A4, 0, 0, 0, 0)
            'Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream("D:  \OutPut.pdf", FileMode.Create))
            'document.Open()
            'For Each reader As PdfReader In pdfReaderList
            '    For i As Integer = 1 To reader.NumberOfPages
            '        Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)
            '        document.Add(iTextSharp.text.Image.GetInstance(page))
            '    Next
            'Next
            'document.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class