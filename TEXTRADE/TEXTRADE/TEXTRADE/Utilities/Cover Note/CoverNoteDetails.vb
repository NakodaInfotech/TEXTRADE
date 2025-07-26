


Imports BL
    Imports System.Windows.Forms

Public Class CoverNoteDetails

    Public EDIT As Boolean
    Dim TEMPCOVERNO As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CoverNoteDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub CoverNoteDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE ORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid(" and dbo.COVERNOTE.COVER_YEARID=" & YearId & " order by dbo.COVERNOTE.COVER_NO ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclsCMST.search("COVERNOTE.COVER_NO AS TEMPCOVERNO, COVERNOTE.COVER_COVERDATE AS COVERDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENTMASTER.Acc_cmpname, '') AS AGENT,  ISNULL(COVERNOTE.COVER_REMARKS, '') AS REMARKS, COVERNOTE_DESC.COVER_SRNO AS SRNO, ISNULL(COVERNOTE_DESC.COVER_INVNO, 0) AS INVNO, ISNULL(REGISTERMASTER.register_name, '') AS REGNAME, ISNULL(COVERNOTE_DESC.COVER_INVINITIALS, '') AS INVINITIALS, ISNULL(COVERNOTE_DESC.COVER_PRINTINITIALS, '') AS PRINTINITIALS, ISNULL(COVERNOTE_DESC.COVER_INVDATE, '') AS INVDATE, ISNULL(COVERNOTE_DESC.COVER_LRNO, '') AS LRNO, ISNULL(COVERNOTE_DESC.COVER_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(COVERNOTE_DESC.COVER_TOTALPCS, 0) AS TOTALPCS, ISNULL(COVERNOTE_DESC.COVER_GRANDTOTAL, 0) AS GRANDTOTAL, ISNULL(COVERNOTE_DESC.COVER_LRDATE, '') AS LRDATE, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRIDLEDGERS.Acc_cmpname, '') AS GRIDNAME, ISNULL(GRIDAGENTLEDGERS.Acc_cmpname, '') AS GRIDAGENTNAME, ISNULL(COVERNOTE.COVER_COURIERNAME, '') AS COURIERNAME, ISNULL(COVERNOTE.COVER_COURIERDOCKETNO, '') AS COURIERDOCKETNO, COVERNOTE.COVER_COURIERDATE AS COURIERDATE ", "", " COVERNOTE INNER JOIN COVERNOTE_DESC ON COVERNOTE.COVER_NO = COVERNOTE_DESC.COVER_NO AND COVERNOTE.COVER_YEARID = COVERNOTE_DESC.COVER_YEARID LEFT OUTER JOIN LEDGERS AS GRIDAGENTLEDGERS ON COVERNOTE_DESC.COVER_AGENTNAMEID = GRIDAGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS GRIDLEDGERS ON COVERNOTE_DESC.COVER_PARTYNAMEID = GRIDLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON COVERNOTE_DESC.COVER_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN REGISTERMASTER ON COVERNOTE_DESC.COVER_REGID = REGISTERMASTER.register_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON COVERNOTE.COVER_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN LEDGERS ON COVERNOTE.COVER_LEDGERID = LEDGERS.Acc_id  ", TEMPCONDITION)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal TEMPCOVERNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New CoverNote
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPCOVERNO = TEMPCOVERNO
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

    Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("TEMPCOVERNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid(" and dbo.COVERNOTE.COVER_YEARID=" & YearId & " order by dbo.COVERNOTE.COVER_NO ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("TEMPCOVERNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        'Try
        '    Dim DT As DataTable = gridbilldetails.DataSource
        '    If e.RowHandle >= 0 Then
        '        Dim ROW As DataRow = DT.Rows(e.RowHandle)
        '        If ROW("CLOSED") = "TRUE" Then
        '            e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Regular)
        '            e.Appearance.BackColor = Color.LightSkyBlue
        '        End If
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub


    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Cover Note Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Cover Note Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Cover Note Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
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
                    MsgBox("Enter Proper  Cover Note Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Wish to Print Cover Note from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPDIRECT()
                End If
            Else
                If MsgBox("Wish to Print Selected Cover  Note ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED()
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
                    MsgBox("Enter Proper CoverNote Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp CoverNote from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT_WHATS(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected CoverNote ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED_WHATS(False, True)
                End If
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
                    MsgBox("Enter Proper Cover Note Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail Cover Note from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(True)
                End If
            Else
                If MsgBox("Wish to Mail Selected Cover Note ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(True)
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

            Dim TEMPMSG As Integer = MsgBox("Wish to Print Party Cover Note ?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                    Dim OBJINV As New SaleInvoiceDesign
                    OBJINV.MdiParent = MDIMain
                    OBJINV.DIRECTPRINT = True
                    OBJINV.FRMSTRING = "MAINCOVERNOTE"
                    OBJINV.DIRECTMAIL = INVOICEMAIL
                    OBJINV.DIRECTWHATSAPP = WHATSAPP
                    'OBJJV.REGNAME = cmbregister.Text.Trim
                    OBJINV.PRINTSETTING = PRINTDIALOG
                    OBJINV.COVERNOTENO = Val(I)
                    OBJINV.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJINV.WHERECLAUSE = "{COVERNOTE.COVER_NO}=" & Val(I) & " and {COVERNOTE.COVER_YEARID}=" & YearId
                    OBJINV.Show()
                    OBJINV.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\COVER_" & I & ".pdf")
                    FILENAME.Add("COVER_" & I & ".pdf")
                Next

                If INVOICEMAIL Then
                    Dim OBJMAIL As New SendMail
                    OBJMAIL.ALATTACHMENT = ALATTACHMENT
                    OBJMAIL.subject = "MAINCOVERNOTE"
                    OBJMAIL.ShowDialog()
                End If

                If WHATSAPP = True Then
                    Dim OBJWHATSAPP As New SendWhatsapp
                    OBJWHATSAPP.PATH = ALATTACHMENT
                    OBJWHATSAPP.FILENAME = FILENAME
                    OBJWHATSAPP.ShowDialog()
                End If

            Else
                MsgBox("Wish to Print Agent Cover Note ?", MsgBoxStyle.YesNo)
                For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                    Dim OBJINV As New SaleInvoiceDesign
                    OBJINV.MdiParent = MDIMain
                    OBJINV.DIRECTPRINT = True
                    OBJINV.FRMSTRING = "MAINAGENTCOVERNOTE"
                    OBJINV.DIRECTMAIL = INVOICEMAIL
                    OBJINV.DIRECTWHATSAPP = WHATSAPP
                    'OBJJV.REGNAME = cmbregister.Text.Trim
                    OBJINV.PRINTSETTING = PRINTDIALOG
                    OBJINV.COVERNOTENO = Val(I)
                    OBJINV.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJINV.WHERECLAUSE = "{COVERNOTE.COVER_NO}=" & Val(I) & " and {COVERNOTE.COVER_YEARID}=" & YearId
                    OBJINV.Show()
                    OBJINV.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\COVER_" & I & ".pdf")
                    FILENAME.Add("COVER_" & I & ".pdf")
                Next

                If INVOICEMAIL Then
                    Dim OBJMAIL As New SendMail
                    OBJMAIL.ALATTACHMENT = ALATTACHMENT
                    OBJMAIL.subject = "MAINCOVERNOTE"
                    OBJMAIL.ShowDialog()
                End If

                If WHATSAPP = True Then
                    Dim OBJWHATSAPP As New SendWhatsapp
                    OBJWHATSAPP.PATH = ALATTACHMENT
                    OBJWHATSAPP.FILENAME = FILENAME
                    OBJWHATSAPP.ShowDialog()
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

            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If

            Dim TEMPMSG As Integer = MsgBox("Wish to Print Party Cover Note ?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then

                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim ROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

                    Dim OBJINV As New SaleInvoiceDesign
                    OBJINV.MdiParent = MDIMain
                    OBJINV.DIRECTPRINT = True
                    OBJINV.FRMSTRING = "MAINCOVERNOTE"
                    OBJINV.DIRECTMAIL = INVOICEMAIL
                    OBJINV.DIRECTWHATSAPP = WHATSAPP
                    OBJINV.PRINTSETTING = PRINTDIALOG
                    OBJINV.WHERECLAUSE = "{COVERNOTE.COVER_NO}=" & Val(ROW("TEMPCOVERNO")) & " and {COVERNOTE.COVER_YEARID}=" & YearId
                    OBJINV.COVERNOTENO = Val(ROW("TEMPCOVERNO"))
                    OBJINV.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJINV.Show()
                    OBJINV.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\COVERNOTE_" & Val(ROW("SRNO")) & ".pdf")
                    FILENAME.Add("COVER_" & Val(ROW("TEMPCOVERNO")) & ".pdf")
                Next
            Else
                MsgBox("Wish to Print Agent Cover Note ?", MsgBoxStyle.YesNo)
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim ROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                    Dim OBJINV As New SaleInvoiceDesign
                    OBJINV.MdiParent = MDIMain
                    OBJINV.DIRECTPRINT = True
                    OBJINV.FRMSTRING = "MAINAGENTCOVERNOTE"
                    OBJINV.DIRECTMAIL = INVOICEMAIL
                    OBJINV.DIRECTWHATSAPP = WHATSAPP
                    OBJINV.PRINTSETTING = PRINTDIALOG
                    OBJINV.WHERECLAUSE = "{COVERNOTE.COVER_NO}=" & Val(ROW("TEMPCOVERNO")) & " and {COVERNOTE.COVER_YEARID}=" & YearId
                    OBJINV.COVERNOTENO = Val(ROW("TEMPCOVERNO"))
                    OBJINV.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJINV.Show()
                    OBJINV.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\COVERNOTE_" & Val(ROW("SRNO")) & ".pdf")
                    FILENAME.Add("COVER_" & Val(ROW("TEMPCOVERNO")) & ".pdf")
                Next

                If INVOICEMAIL Then
                    Dim OBJMAIL As New SendMail
                    OBJMAIL.ALATTACHMENT = ALATTACHMENT
                    OBJMAIL.subject = "MAINAGENTCOVERNOTE"
                    OBJMAIL.ShowDialog()
                End If

                If WHATSAPP = True Then
                    Dim OBJWHATSAPP As New SendWhatsapp
                    OBJWHATSAPP.PATH = ALATTACHMENT
                    OBJWHATSAPP.FILENAME = FILENAME
                    OBJWHATSAPP.ShowDialog()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPDIRECT_WHATS(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If

            Dim TEMPMSG2 As Integer = MsgBox("Wish to Send Party CoverNote ?", MsgBoxStyle.YesNo)
            If TEMPMSG2 = vbYes Then
                For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                    'Dim WHATSAPPNO As String = ""
                    Dim OBJINV As New SaleInvoiceDesign
                    OBJINV.MdiParent = MDIMain
                    OBJINV.DIRECTPRINT = True
                    OBJINV.FRMSTRING = "MAINCOVERNOTE"
                    OBJINV.DIRECTMAIL = INVOICEMAIL
                    OBJINV.DIRECTWHATSAPP = WHATSAPP
                    'OBJJV.REGNAME = cmbregister.Text.Trim
                    OBJINV.PRINTSETTING = PRINTDIALOG
                    OBJINV.PARTYNAME = ("PARTYNAME")
                    'Dim OBJCMN As New ClsCommon
                    'Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STATE_REMARK,'') AS STATECODE, ISNULL(LEDGERS.ACC_WHATSAPPNO,'') AS WHATSAPPNO", "", " COVERNOTE INNER JOIN LEDGERS ON COVER_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID  ", " AND COVERNOTE.COVER_NO = " & Val(I) & " AND COVERNOTE.COVER_YEARID = " & YearId)
                    'If DT.Rows.Count > 0 Then WHATSAPPNO = DT.Rows(0).Item("WHATSAPPNO")
                    OBJINV.COVERNOTENO = Val(I)
                    OBJINV.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJINV.WHERECLAUSE = "{COVERNOTE.COVER_NO}=" & Val(I) & " and {COVERNOTE.COVER_YEARID}=" & YearId
                    OBJINV.Show()
                    OBJINV.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\" & ("PARTYNAME") & "COVERNOTE_" & Val(I) & ".pdf")
                    FILENAME.Add("PARTYNAME" & "COVERNOTE_" & Val(I) & ".pdf")
                Next

                If INVOICEMAIL Then
                    Dim OBJMAIL As New SendMail
                    OBJMAIL.ALATTACHMENT = ALATTACHMENT
                    OBJMAIL.subject = "MAINCOVERNOTE"
                    OBJMAIL.ShowDialog()
                End If

                If WHATSAPP = True Then
                    Dim OBJWHATSAPP As New SendWhatsapp
                    OBJWHATSAPP.PATH = ALATTACHMENT
                    OBJWHATSAPP.FILENAME = FILENAME
                    OBJWHATSAPP.ShowDialog()
                End If

            Else

                Dim TEMPMSG3 As Integer = MsgBox("Wish to Send Agent CoverNote ?", MsgBoxStyle.YesNo)
                If TEMPMSG3 = vbYes Then
                    For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                        'Dim WHATSAPPNO As String = ""
                        Dim OBJINV As New SaleInvoiceDesign
                        OBJINV.MdiParent = MDIMain
                        OBJINV.DIRECTPRINT = True
                        OBJINV.FRMSTRING = "MAINAGENTCOVERNOTE"
                        OBJINV.DIRECTMAIL = INVOICEMAIL
                        OBJINV.DIRECTWHATSAPP = WHATSAPP
                        'OBJJV.REGNAME = cmbregister.Text.Trim
                        OBJINV.PRINTSETTING = PRINTDIALOG
                        OBJINV.AGENTNAME = ("AGENT")
                        'Dim OBJCMN As New ClsCommon
                        'Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STATE_REMARK,'') AS STATECODE, ISNULL(LEDGERS.ACC_WHATSAPPNO,'') AS WHATSAPPNO", "", " COVERNOTE INNER JOIN LEDGERS ON COVER_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID  ", " AND COVERNOTE.COVER_NO = " & Val(I) & " AND COVERNOTE.COVER_YEARID = " & YearId)
                        'If DT.Rows.Count > 0 Then WHATSAPPNO = DT.Rows(0).Item("WHATSAPPNO")
                        OBJINV.COVERNOTENO = Val(I)
                        OBJINV.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                        OBJINV.WHERECLAUSE = "{COVERNOTE.COVER_NO}=" & Val(I) & " and {COVERNOTE.COVER_YEARID}=" & YearId
                        OBJINV.Show()
                        OBJINV.Close()
                        ALATTACHMENT.Add(Application.StartupPath & "\" & ("AGENT") & "AGENTCOVERNOTE_" & Val(I) & ".pdf")
                        FILENAME.Add("AGENT" & "AGENTCOVERNOTE_" & Val(I) & ".pdf")
                    Next

                    If INVOICEMAIL Then
                        Dim OBJMAIL As New SendMail
                        OBJMAIL.ALATTACHMENT = ALATTACHMENT
                        OBJMAIL.subject = "MAINCOVERNOTE"
                        OBJMAIL.ShowDialog()
                    End If

                    If WHATSAPP = True Then
                        Dim OBJWHATSAPP As New SendWhatsapp
                        OBJWHATSAPP.PATH = ALATTACHMENT
                        OBJWHATSAPP.FILENAME = FILENAME
                        OBJWHATSAPP.ShowDialog()
                    End If

                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPSELECTED_WHATS(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList

            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If

            Dim TEMPMSG2 As Integer = MsgBox("Wish to Send Party CoverNote ?", MsgBoxStyle.YesNo)
            If TEMPMSG2 = vbYes Then

                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim ROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

                    Dim OBJINV As New SaleInvoiceDesign
                    OBJINV.MdiParent = MDIMain
                    OBJINV.DIRECTPRINT = True
                    OBJINV.FRMSTRING = "MAINCOVERNOTE"
                    OBJINV.DIRECTMAIL = INVOICEMAIL
                    OBJINV.DIRECTWHATSAPP = WHATSAPP
                    OBJINV.PRINTSETTING = PRINTDIALOG
                    OBJINV.PARTYNAME = ROW("PARTYNAME")
                    OBJINV.WHERECLAUSE = "{COVERNOTE.COVER_NO}=" & Val(ROW("TEMPCOVERNO")) & " and {COVERNOTE.COVER_YEARID}=" & YearId
                    OBJINV.COVERNOTENO = Val(ROW("TEMPCOVERNO"))
                    OBJINV.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJINV.Show()
                    OBJINV.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\" & ROW("PARTYNAME") & "COVERNOTE_" & Val(ROW("TEMPCOVERNO")) & ".pdf")
                    FILENAME.Add(ROW("PARTYNAME") & "COVERNOTE_" & Val(ROW("TEMPCOVERNO")) & ".pdf")
                Next

                If INVOICEMAIL Then
                    Dim OBJMAIL As New SendMail
                    OBJMAIL.ALATTACHMENT = ALATTACHMENT
                    OBJMAIL.subject = "MAINAGENTCOVERNOTE"
                    OBJMAIL.ShowDialog()
                End If

                If WHATSAPP = True Then
                    Dim OBJWHATSAPP As New SendWhatsapp
                    OBJWHATSAPP.PATH = ALATTACHMENT
                    OBJWHATSAPP.FILENAME = FILENAME
                    OBJWHATSAPP.ShowDialog()
                End If

            Else

                MsgBox("Wish to Send Agent Cover Note ?", MsgBoxStyle.YesNo)
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim ROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                    Dim OBJINV As New SaleInvoiceDesign
                    OBJINV.MdiParent = MDIMain
                    OBJINV.DIRECTPRINT = True
                    OBJINV.FRMSTRING = "MAINAGENTCOVERNOTE"
                    OBJINV.DIRECTMAIL = INVOICEMAIL
                    OBJINV.DIRECTWHATSAPP = WHATSAPP
                    OBJINV.PRINTSETTING = PRINTDIALOG
                    OBJINV.PRINTSETTING = PRINTDIALOG
                    OBJINV.AGENTNAME = ROW("AGENT")
                    OBJINV.WHERECLAUSE = "{COVERNOTE.COVER_NO}=" & Val(ROW("TEMPCOVERNO")) & " and {COVERNOTE.COVER_YEARID}=" & YearId
                    OBJINV.COVERNOTENO = Val(ROW("TEMPCOVERNO"))
                    OBJINV.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJINV.Show()
                    OBJINV.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\" & ROW("AGENT") & "AGENTCOVERNOTE_" & Val(ROW("TEMPCOVERNO")) & ".pdf")
                    FILENAME.Add(ROW("AGENT") & "AGENTCOVERNOTE_" & Val(ROW("TEMPCOVERNO")) & ".pdf")
                Next

                If INVOICEMAIL Then
                    Dim OBJMAIL As New SendMail
                    OBJMAIL.ALATTACHMENT = ALATTACHMENT
                    OBJMAIL.subject = "MAINAGENTCOVERNOTE"
                    OBJMAIL.ShowDialog()
                End If

                If WHATSAPP = True Then
                    Dim OBJWHATSAPP As New SendWhatsapp
                    OBJWHATSAPP.PATH = ALATTACHMENT
                    OBJWHATSAPP.FILENAME = FILENAME
                    OBJWHATSAPP.ShowDialog()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class