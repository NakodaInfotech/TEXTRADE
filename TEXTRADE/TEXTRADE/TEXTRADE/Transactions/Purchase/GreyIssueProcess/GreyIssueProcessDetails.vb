Imports BL


Public Class GreyIssueProcessDetails
    Public FRMSTRING As String
    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean

    Private Sub TOOLADDNEW_Click(sender As Object, e As EventArgs) Handles TOOLADDNEW.Click
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

    Private Sub TOOLEXCEL_Click(sender As Object, e As EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\GREYISSUEPROCESS Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = ""
            PERIOD = AccFrom & " - " & AccTo

            opti.SheetName = "GREYISSUEPROCESS Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "GREYISSUEPROCESS Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("GREYISSUEPROCESS Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TOOLMAIL_Click(sender As Object, e As EventArgs) Handles TOOLMAIL.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper GRN Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail GRN from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(True)
                End If
            Else
                If MsgBox("Wish to Mail Selected GRN ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub GreyIssueProcessDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
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
                Dim OBJGRN As New GRNDesign
                OBJGRN.MdiParent = MDIMain
                OBJGRN.DIRECTPRINT = True

                OBJGRN.DIRECTMAIL = INVOICEMAIL
                OBJGRN.DIRECTWHATSAPP = WHATSAPP
                OBJGRN.PRINTSETTING = PRINTDIALOG
                OBJGRN.WHERECLAUSE = "{GRN.GRN_no}=" & Val(I) & "'  and {GRN.GRN_yearid}=" & YearId
                OBJGRN.GRNNO = Val(I)
                OBJGRN.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJGRN.Show()
                OBJGRN.Close()

                ALATTACHMENT.Add(Application.StartupPath & "\GRN_" & I & ".pdf")
                FILENAME.Add("GRN_" & I & ".pdf")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "GRN"
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
            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                Dim ROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

                Dim OBJGRN As New GRNDesign
                OBJGRN.MdiParent = MDIMain
                OBJGRN.DIRECTPRINT = True

                OBJGRN.DIRECTMAIL = INVOICEMAIL
                OBJGRN.DIRECTWHATSAPP = WHATSAPP
                OBJGRN.PRINTSETTING = PRINTDIALOG
                OBJGRN.WHERECLAUSE = "{GRN.GRN_no}=" & Val(ROW("SRNO")) & "'  and {GRN.GRN_yearid}=" & YearId
                OBJGRN.GRNNO = Val(ROW("SRNO"))
                OBJGRN.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJGRN.Show()
                OBJGRN.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\GRN_" & Val(ROW("SRNO")) & ".pdf")
                FILENAME.Add("GRN_" & Val(ROW("SRNO")) & ".pdf")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "GRN"
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

    Sub FILLGRID()
        Try
            Dim OBJCLSMT As New ClsCommonMaster
            Dim DT As DataTable = OBJCLSMT.search("GREYISSUEPROCESS.GREYISS_NO AS SRNO, GREYISSUEPROCESS.GREYISS_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(PURLEDGERS.Acc_cmpname, '') AS PURNAME, ISNULL(GREYISSUEPROCESS.GREYISS_CHALLANNO, '') AS CHALLANNO, GREYISSUEPROCESS.GREYISS_CHALLANDATE AS CHALLANDATE, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(GREYISSUEPROCESS.GREYISS_LRNO, '') AS LRNO, GREYISSUEPROCESS.GREYISS_LRDATE AS LRDATE, ISNULL(GREYISSUEPROCESS.GREYISS_TOTALBALES, 0) AS TOTALBALES, ISNULL(GREYISSUEPROCESS.GREYISS_TOTALQTY, 0) AS TOTALQTY, ISNULL(GREYISSUEPROCESS.GREYISS_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(GREYISSUEPROCESS.GREYISS_TOTALAMOUNT, 0) AS TOTALAMOUNT, ISNULL(GREYISSUEPROCESS.GREYISS_REMARKS, '') AS REMARKS, ISNULL(GREYISSUEPROCESS_DESC.GREYISS_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(GREYISSUEPROCESS_DESC.GREYISS_BALENO, '') AS BALENO, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(GREYISSUEPROCESS_DESC.GREYISS_QTY, 0) AS QTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(GREYISSUEPROCESS_DESC.GREYISS_MTRS, 0) AS MTRS, ISNULL(GREYISSUEPROCESS_DESC.GREYISS_OUTMTRS, 0) AS OUTMTRS, ROUND(ISNULL(GREYISSUEPROCESS_DESC.GREYISS_MTRS, 0) - ISNULL(GREYISSUEPROCESS_DESC.GREYISS_OUTMTRS, 0), 2) AS BALMTRS, ISNULL(GREYISSUEPROCESS_DESC.GREYISS_RATE, 0) AS RATE, ISNULL(GREYISSUEPROCESS_DESC.GREYISS_AMOUNT, 0) AS AMOUNT, ISNULL(GREYISSUEPROCESS_DESC.GREYISS_OUTPCS, 0) AS OUTPCS, ISNULL(GREYISSUEPROCESS_DESC.GREYISS_OUTMTRS, 0) AS OUTMTRS, ISNULL(GREYISSUEPROCESS_DESC.GREYISS_FROMNO, 0) AS FROMNO, ISNULL(GREYISSUEPROCESS_DESC.GREYISS_FROMSRNO, 0) AS FROMSRNO, ISNULL(GREYISSUEPROCESS_DESC.GREYISS_FROMTYPE, '') AS FROMTYPE, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT,  ISNULL(GREYISSUEPROCESS.GREYISS_CRDAYS, 0) AS CRDAYS, ISNULL(GREYISSUEPROCESS.GREYISS_REFLOTNO,'') AS REFLOTNO", "", "  GREYISSUEPROCESS INNER JOIN GREYISSUEPROCESS_DESC ON GREYISSUEPROCESS.GREYISS_NO = GREYISSUEPROCESS_DESC.GREYISS_NO AND GREYISSUEPROCESS.GREYISS_YEARID = GREYISSUEPROCESS_DESC.GREYISS_YEARID INNER JOIN LEDGERS ON GREYISSUEPROCESS.GREYISS_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON GREYISSUEPROCESS_DESC.GREYISS_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON GREYISSUEPROCESS.GREYISS_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN UNITMASTER ON GREYISSUEPROCESS_DESC.GREYISS_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN COLORMASTER ON GREYISSUEPROCESS_DESC.GREYISS_SHADEID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GREYISSUEPROCESS_DESC.GREYISS_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON GREYISSUEPROCESS_DESC.GREYISS_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON TRANSLEDGERS.Acc_id = GREYISSUEPROCESS.GREYISS_TRANSID LEFT OUTER JOIN LEDGERS AS PURLEDGERS ON GREYISSUEPROCESS.GREYISS_PURNAMEID = PURLEDGERS.Acc_id", " AND dbo.GREYISSUEPROCESS.GREYISS_YEARID = " & YearId & " ORDER BY dbo.GREYISSUEPROCESS.GREYISS_NO, GREYISS_GRIDSRNO ")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()

            If SELECTEDROWS.Length <= 0 Then Exit Sub
            Dim OBJGDN As New GRNDesign
            Dim SRNOCLAUSE As String = ""
            If MsgBox("Wish to Print Mill Letter ?", MsgBoxStyle.YesNo) = vbYes Then
                OBJGDN.MdiParent = MDIMain
                OBJGDN.FRMSTRING = "LETTER"
                OBJGDN.WHERECLAUSE = " {GREYISSUEPROCESS.GREYISS_YEARID}=" & YearId

                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim ROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                    If SRNOCLAUSE = "" Then
                        SRNOCLAUSE = " And ({GREYISSUEPROCESS.GREYISS_NO} = " & Val(ROW("SRNO"))
                    Else
                        SRNOCLAUSE = SRNOCLAUSE & " OR {GREYISSUEPROCESS.GREYISS_NO} = " & Val(ROW("SRNO"))
                    End If
                Next
                If SRNOCLAUSE <> "" Then
                    SRNOCLAUSE = SRNOCLAUSE & ")"
                    OBJGDN.WHERECLAUSE = OBJGDN.WHERECLAUSE & SRNOCLAUSE
                End If
                OBJGDN.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyIssueProcessDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal SRNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJGREYREC As New GreyIssueProcess
                OBJGREYREC.MdiParent = MDIMain
                OBJGREYREC.EDIT = editval
                OBJGREYREC.TEMPGRNNO = SRNO
                OBJGREYREC.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTFROM.KeyPress, TXTTO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyIssueProcessDetails_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "SNCM" Then Me.Text = "Grey Rec in Godown Details"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class