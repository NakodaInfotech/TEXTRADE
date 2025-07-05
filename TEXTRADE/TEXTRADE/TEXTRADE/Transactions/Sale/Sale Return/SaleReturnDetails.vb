
Imports BL
Imports System.Windows.Forms

Public Class SaleReturnDetails
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim DTMAIL As New DataTable
    Dim DTWHATSAPP As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub MaterialReceiptDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                cmdok_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub MaterialReceiptDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE RETURN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            DTMAIL.Columns.Add("SRNO")
            DTMAIL.Columns.Add("REGID")
            DTMAIL.Columns.Add("REGNAME")
            DTMAIL.Columns.Add("PRINTINITIALS")
            DTMAIL.Columns.Add("DATE")
            DTMAIL.Columns.Add("NAME")
            DTMAIL.Columns.Add("PARTYEMAILID")
            DTMAIL.Columns.Add("AGENTNAME")
            DTMAIL.Columns.Add("AGENTEMAILID")
            DTMAIL.Columns.Add("GRANDTOTAL")
            DTMAIL.Columns.Add("SUBJECT")
            DTMAIL.Columns.Add("ATTACHMENT")
            DTMAIL.Columns.Add("FILENAME")



            DTWHATSAPP.Columns.Add("SRNO")
            DTWHATSAPP.Columns.Add("REGID")
            DTWHATSAPP.Columns.Add("REGNAME")
            DTWHATSAPP.Columns.Add("PRINTINITIALS")
            DTWHATSAPP.Columns.Add("DATE")
            DTWHATSAPP.Columns.Add("NAME")
            DTWHATSAPP.Columns.Add("PARTYWHATSAPP")
            DTWHATSAPP.Columns.Add("AGENTNAME")
            DTWHATSAPP.Columns.Add("AGENTWHATSAPP")
            DTWHATSAPP.Columns.Add("GRANDTOTAL")
            DTWHATSAPP.Columns.Add("SUBJECT")
            DTWHATSAPP.Columns.Add("ATTACHMENT")
            DTWHATSAPP.Columns.Add("FILENAME")

            fillgrid(" and SALERETURN.SALRET_yearid=" & YearId & " order by SALERETURN.SALRET_no ")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("CAST(0 AS BIT) AS CHK ,SALERETURN.SALRET_NO AS SRNO, SALERETURN.SALRET_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(SALERETURN.SALRET_EWAYBILLNO, '') AS EWAYBILLNO, ISNULL(SALERETURN.SALRET_CHALLANNO, '') AS CHALLANNO, SALERETURN.SALRET_CHALLANDATE AS CHALLANDATE,(CASE WHEN ISNULL(SALERETURN.SALRET_INVOICENO, '') <> '' THEN ISNULL(SALERETURN.SALRET_INVOICENO, '') ELSE ISNULL(STUFF((SELECT ', ' + SALRET_BILLINITIALS  [text()] FROM SALERETURN_BILLDESC AS A WHERE A.SALRET_NO = SALERETURN.SALRET_NO AND A.SALRET_YEARID = SALERETURN.SALRET_YEARID FOR XML PATH(''), TYPE) .value('.','NVARCHAR(MAX)'),1,2,' '),'') END) AS INVOICENO, ISNULL(SALERETURN.SALRET_INVOICEDATE, GETDATE()) AS INVOICEDATE, ISNULL(SALERETURN.SALRET_LRNO, '') AS LRNO, (CASE WHEN ISNULL(SALERETURN.SALRET_LRNO, '') = '' THEN NULL ELSE SALRET_LRDATE END) AS LRDATE,ISNULL(agentLEDGERS.Acc_cmpname, '') AS AGENT, SALERETURN.SALRET_TOTALQTY AS TOTALPCS, SALERETURN.SALRET_TOTALMTRS AS TOTALMTRS, ISNULL(SALERETURN.SALRET_SUBTOTAL, 0) AS TAXABLEAMT, ISNULL(SALERETURN.SALRET_TOTALCGSTAMT, 0) AS CGSTAMT, ISNULL(SALERETURN.SALRET_TOTALSGSTAMT, 0) AS SGSTAMT, ISNULL(SALERETURN.SALRET_TOTALIGSTAMT, 0) AS IGSTAMT, ISNULL(SALERETURN.SALRET_GRANDTOTAL, 0) AS GTOTAL, ISNULL(SALERETURN.SALRET_remarks, '') AS REMARKS, ISNULL(SALERETURN.SALRET_PARTYREFNO, '') AS PARTYREFNO, ISNULL(DEBITLEDGERS.Acc_cmpname, '') AS DEBITNAME, ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS DELIVERYAT, (CASE WHEN ISNULL(SALERETURN.SALRET_ACTUALINVNO,'') <> '' THEN ISNULL(SALERETURN.SALRET_ACTUALINVNO,'') ELSE ISNULL(STUFF((SELECT ', ' + SALRET_BILLINITIALS  [text()] FROM SALERETURN_BILLDESC AS A WHERE A.SALRET_NO = SALERETURN.SALRET_NO AND A.SALRET_YEARID = SALERETURN.SALRET_YEARID FOR XML PATH(''), TYPE) .value('.','NVARCHAR(MAX)'),1,2,' '),'') END) AS ACTUALINVNO, ISNULL(SALERETURN.SALRET_ACTUALINVDATE, GETDATE()) AS ACTUALINVDATE, ISNULL(SALERETURN.SALRET_IRNNO, '') AS IRNNO, ISNULL(SALERETURN.SALRET_ACKNO, '') AS ACKNO, SALERETURN.SALRET_ACKDATE AS ACKDATE, ISNULL(LEDGERS.ACC_WHATSAPPNO, '0') AS PARTYWHATSAPP, ISNULL(LEDGERS.Acc_email, '') AS PARTYEMAIL, ISNULL(agentLEDGERS.Acc_email, '') AS AGENTEMAIL, ISNULL(agentLEDGERS.ACC_WHATSAPPNO, '0') AS AGENTWHATSAPP, ISNULL(COSTCENTERMASTER.COSTCENTER_name, '') AS COSTCENTERNAME,ISNULL(USERMASTER.User_Name, '') AS CREATEDBY ", "", "SALERETURN INNER JOIN LEDGERS ON SALERETURN.SALRET_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN COSTCENTERMASTER ON SALERETURN.SALRET_COSTCENTERNAMEID = COSTCENTERMASTER.COSTCENTER_id LEFT OUTER JOIN LEDGERS AS agentLEDGERS ON SALERETURN.SALRET_AGENTID = agentLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS DEBITLEDGERS ON SALERETURN.SALRET_DEBITLEDGERID = DEBITLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON SALERETURN.SALRET_DELIVERYATID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN USERMASTER ON SALERETURN .SALRET_userid = USERMASTER.User_id ", TEMPCONDITION)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal SALRETNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJSALRET As New SaleReturn
                OBJSALRET.MdiParent = MDIMain
                OBJSALRET.EDIT = editval
                OBJSALRET.TEMPSALRETNO = SALRETNO
                OBJSALRET.Show()
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

    Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDETAILS.Click
        Try
            Dim OBJSR As New SaleReturnGridDetails
            OBJSR.MdiParent = MDIMain
            OBJSR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub


            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Entry Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Wish to Print Entries from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPDIRECT()
                End If
            Else
                If MsgBox("Wish to Print Selected Entries ?", MsgBoxStyle.YesNo) = vbYes Then
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

            If INVOICEMAIL = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                Dim OBJSALRET As New SaleReturnDesign
                OBJSALRET.MdiParent = MDIMain
                OBJSALRET.DIRECTPRINT = True
                OBJSALRET.FRMSTRING = "SALERETURN"
                OBJSALRET.DIRECTMAIL = INVOICEMAIL
                OBJSALRET.PRINTSETTING = PRINTDIALOG
                OBJSALRET.SALRETNO = Val(I)
                OBJSALRET.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJSALRET.Show()
                OBJSALRET.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\SALRET_" & I & ".pdf")
                FILENAME.Add("SALRET_" & Val(I) & ".pdf")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Sale Return/Credit Note"
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

    'Sub SERVERPROPSELECTED(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
    '    Try

    '        Dim ALATTACHMENT As New ArrayList
    '        Dim FILENAME As New ArrayList
    '        DTMAIL.Rows.Clear()

    '        If INVOICEMAIL = False Then
    '            If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
    '        End If
    '        Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
    '        For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
    '            Dim ROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

    '            Dim OBJSALRET As New SaleReturnDesign
    '            OBJSALRET.MdiParent = MDIMain
    '            OBJSALRET.DIRECTPRINT = True
    '            OBJSALRET.FRMSTRING = "SALERETURN"
    '            OBJSALRET.DIRECTMAIL = INVOICEMAIL
    '            OBJSALRET.PRINTSETTING = PRINTDIALOG
    '            OBJSALRET.SALRETNO = Val(ROW("SRNO"))
    '            OBJSALRET.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
    '            OBJSALRET.Show()
    '            OBJSALRET.Close()
    '            ALATTACHMENT.Add(Application.StartupPath & "\SALRET_" & Val(ROW("SRNO")) & ".pdf")
    '            FILENAME.Add("SALRET_" & Val(ROW("SRNO")) & ".pdf")

    '            'ADDINT IN DTEMAIL
    '            DTMAIL.Rows.Add(ROW("SRNO"), 0, "", ROW("SRNO"), ROW("DATE"), ROW("NAME"), ROW("PARTYEMAIL"), ROW("AGENT"), ROW("AGENTEMAIL"), Val(ROW("GTOTAL")), UCase(CmpName) & " - Sale Return No. " & ROW("SRNO") & " Dated " & ROW("DATE"), Application.StartupPath & "\SALRET_" & Val(ROW("SRNO")) & ".pdf", "SALRET_" & Val(ROW("SRNO")) & ".pdf")

    '        Next

    '        If INVOICEMAIL Then
    '            If DTMAIL.Rows.Count = 0 Then Exit Sub
    '            Dim OBJEMAIL As New SendMultipleMail
    '            OBJEMAIL.FORMTYPE = "SALE RETURN"
    '            OBJEMAIL.DT = DTMAIL
    '            OBJEMAIL.ShowDialog()
    '            Exit Sub

    '            'Dim OBJMAIL As New SendMail
    '            'OBJMAIL.ALATTACHMENT = ALATTACHMENT
    '            'OBJMAIL.subject = "Sale Return/Credit Note"
    '            'OBJMAIL.ShowDialog()
    '        End If

    '        If WHATSAPP = True Then
    '            Dim OBJWHATSAPP As New SendWhatsapp
    '            OBJWHATSAPP.PATH = ALATTACHMENT
    '            OBJWHATSAPP.FILENAME = FILENAME
    '            OBJWHATSAPP.ShowDialog()
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Sub SERVERPROPSELECTED(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            DTMAIL.Rows.Clear()
            DTWHATSAPP.Rows.Clear()


            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            'Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(gridbill.RowCount - 1)
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                If ROW("CHK") = True Then
                    Dim OBJSALRET As New SaleReturnDesign
                    OBJSALRET.MdiParent = MDIMain
                    OBJSALRET.DIRECTPRINT = True
                    OBJSALRET.FRMSTRING = "SALERETURN"
                    OBJSALRET.DIRECTMAIL = INVOICEMAIL
                    OBJSALRET.DIRECTWHATSAPP = WHATSAPP

                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STATE_REMARK,'') AS STATECODE ", "", " SALERETURN INNER JOIN LEDGERS ON SALRET_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID  ", " AND SALERETURN.SALRET_NO = " & Val(ROW("SRNO")) & "  AND SALERETURN.SALRET_YEARID = " & YearId)
                    If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STATECODE") <> CMPSTATECODE Then OBJSALRET.IGSTFORMAT = True
                    OBJSALRET.PRINTSETTING = PRINTDIALOG
                    OBJSALRET.SALRETNO = Val(ROW("SRNO"))
                    OBJSALRET.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJSALRET.Show()
                    OBJSALRET.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\SALRET_" & Val(ROW("SRNO")) & ".pdf")
                    FILENAME.Add("SALRET_" & Val(ROW("SRNO")) & ".pdf")


                    DTMAIL.Rows.Add(Val(ROW("SRNO")), 0, "", "", ROW("DATE"), ROW("NAME"), ROW("PARTYEMAIL"), ROW("AGENT"), ROW("AGENTEMAIL"), Val(ROW("GTOTAL")), UCase(CmpName) & " - Sale Return No. " & ROW("SRNO") & " Dated " & ROW("DATE"), Application.StartupPath & "\SALRET_" & Val(ROW("SRNO")) & ".pdf", "SALRET_" & Val(ROW("SRNO")) & ".pdf")

                    'ADDING IN DTWHATSAPP
                    DTWHATSAPP.Rows.Add(Val(ROW("SRNO")), 0, "", "", ROW("DATE"), ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("AGENT"), ROW("AGENTWHATSAPP"), Val(ROW("GTOTAL")), UCase(CmpName) & " - Sale Return No. " & ROW("SRNO") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & "SALRET_" & Val(ROW("SRNO")) & ".pdf", "SALRET_" & Val(ROW("SRNO")) & ".pdf")

                    DT = OBJCMN.Execute_Any_String("UPDATE SALERETURN SET SALERET_SENDWHATSAPP = 1 FROM SALERETURN  WHERE SALRET_NO = " & Val(ROW("SRNO")) & " AND SALRET_YEARID = " & YearId, "", "")

                End If
            Next

            If INVOICEMAIL Then
                If DTMAIL.Rows.Count = 0 Then Exit Sub
                Dim OBJEMAIL As New SendMultipleMail
                OBJEMAIL.FORMTYPE = "INVOICE"
                OBJEMAIL.DT = DTMAIL
                OBJEMAIL.ShowDialog()
                Exit Sub
            End If

            'If INVOICEMAIL Then
            '    Dim OBJMAIL As New SendMail
            '    OBJMAIL.ALATTACHMENT = ALATTACHMENT
            '    OBJMAIL.subject = "Invoice"
            '    OBJMAIL.ShowDialog()
            'End If

            If WHATSAPP = True Then
                If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
                Dim OBJWHATSAPP As New SendMultipleWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.DT = DTWHATSAPP
                OBJWHATSAPP.ShowDialog()
            End If

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

    Private Sub TOOLMAIL_Click(sender As Object, e As EventArgs) Handles TOOLMAIL.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Entry Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail Entries from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(True)
                End If
            Else
                If MsgBox("Wish to Mail Selected Entries ?", MsgBoxStyle.YesNo) = vbYes Then
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
                    MsgBox("Enter Proper Entry Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Entries from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Entries ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(False, True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid(" and SALERETURN.SALRET_yearid=" & YearId & " order by SALERETURN.SALRET_no ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Sale Return Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Sale Return Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sale Return Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Sale Return Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub
End Class