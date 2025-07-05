
Imports BL
Imports DevExpress.XtraGrid.Views.Grid
Public Class LotTaggingDetails
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub LotTaggingDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
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

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid()
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
                    MsgBox("Enter Proper Serial Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Wish to Print Entry from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPDIRECT()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub SERVERPROPDIRECT(Optional ByVal INVOICEMAIL As Boolean = False)
        Try
            'Dim ALATTACHMENT As New ArrayList
            'If INVOICEMAIL = False Then
            '    If PrintDialog.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PrintDialog.PrinterSettings Else Exit Sub
            'End If
            'For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
            '    Dim OBJPROGRAM As New ProgramDesign
            '    OBJPROGRAM.MdiParent = MDIMain
            '    OBJPROGRAM.DIRECTPRINT = True
            '    OBJPROGRAM.DIRECTMAIL = INVOICEMAIL
            '    OBJPROGRAM.PRINTSETTING = PrintDialog
            '    OBJPROGRAM.PROGRAMNO = Val(I)
            '    OBJPROGRAM.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
            '    OBJPROGRAM.Show()
            '    OBJPROGRAM.Close()
            '    ALATTACHMENT.Add(Application.StartupPath & "\LotTagging_" & I & ".pdf")
            'Next

            'If INVOICEMAIL Then
            '    Dim OBJMAIL As New SendMail
            '    OBJMAIL.ALATTACHMENT = ALATTACHMENT
            '    OBJMAIL.subject = "Lot_Tagging"
            '    OBJMAIL.ShowDialog()
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EXCELEXPORT_Click(sender As Object, e As EventArgs) Handles EXCELEXPORT.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Lot Tagging Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Lot Tagging Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Lottag Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Lot Tagging Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub gridBILL_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDLOTDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDLOTDETAILS.Click
        Try
            Dim OBJINV As New LotTaggingLotDetails
            OBJINV.MdiParent = MDIMain
            OBJINV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDPROGDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDPROGDETAILS.Click
        Try
            Dim OBJINV As New LotTaggingProgramDetails
            OBJINV.MdiParent = MDIMain
            OBJINV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LotTaggingDetails_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim WHERECLAUSE As String = ""
            Dim SELECTCLAUSE As String = ""
            'If ClientName = "AVIS" Then SELECTCLAUSE = " ,CASE WHEN PROGRAMMASTER.PROGRAM_CARDRECDATE <> '' AND PROGRAMMASTER_DESC.PROGRAM_PROGISSDATE <> '' THEN DATEDIFF(DAY, CONVERT(date, PROGRAMMASTER.PROGRAM_CARDRECDATE,103 ), CONVERT(date, PROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,103 ))  ELSE 0 END AS CARDRECPRGISSDAYS, CASE WHEN PROGRAMMASTER_DESC.PROGRAM_PROGISSDATE <> '' THEN DATEDIFF(DAY, CONVERT(date, PROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,103 ), CAST(GETDATE() AS DATE))  ELSE 0 END AS PRGISSDAYS, CASE WHEN PROGRAMMASTER_DESC.PROGRAM_PRODCUTTING <> '' THEN DATEDIFF(DAY, CONVERT(date, PROGRAMMASTER_DESC.PROGRAM_PRODCUTTING,103 ), CAST(GETDATE() AS DATE))  ELSE 0 END AS PRODCUTTDAYS, CASE WHEN PROGRAMMASTER_DESC.PROGRAM_FINISHCUTTING <> '' THEN DATEDIFF(DAY, CONVERT(date, PROGRAMMASTER_DESC.PROGRAM_FINISHCUTTING,103 ), CAST(GETDATE() AS DATE))  ELSE 0 END AS FINISHCUTTDAYS  "
            If CHKPENDING.Checked = True Then WHERECLAUSE = "" ' AND ISNULL(PROGRAMMASTER_DESC.PROGRAM_STATUS,'') = '' AND (ROUND(((ISNULL(PROGRAMMASTER_DESC.PROGRAM_PCS,0) - ISNULL(PROGRAMMASTER_DESC.PROGRAM_RECDPCS,0)) / ISNULL(PROGRAMMASTER_DESC.PROGRAM_PCS,0))*100,2)) >= 90"
            'Dim dt As DataTable = objclsCMST.search("ISNULL(LOTTAGGING.LOTTAG_NO, 0) AS SRNO, LOTTAGGING.LOTTAG_DATE AS DATE, ISNULL(LOTTAGGING_DESC.LOTTAG_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(LOTTAGGING_DESC.LOTTAG_LRNO, '') AS LRNO, ISNULL(LOTTAGGING_DESC.LOTTAG_LOTNO, '') AS LOTNO, LOTTAGGING_DESC.LOTTAG_LOTDATE AS DATE, ISNULL(LOTTAGGING_DESC.LOTTAG_MTRS, 0) AS MTRS, ISNULL(LOTTAGGING_DESC.LOTTAG_BALMTRS, 0) AS BALMTRS, ISNULL(LOTTAGGING_DESC.LOTTAG_ADJMTRS, 0) AS ADJMTRS, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS ITEMNAME" & SELECTCLAUSE, "", "   LOTTAGGING INNER JOIN LOTTAGGING_DESC ON LOTTAGGING.LOTTAG_YEARID = LOTTAGGING_DESC.LOTTAG_YEARID AND LOTTAGGING.LOTTAG_NO = LOTTAGGING_DESC.LOTTAG_NO LEFT OUTER JOIN ITEMMASTER ON LOTTAGGING_DESC.LOTTAG_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS ON LOTTAGGING.LOTTAG_LEDGERID = LEDGERS.Acc_id", WHERECLAUSE & " AND LOTTAGGING.LOTTAG_YEARID = " & YearId & " ORDER BY LOTTAGGING.LOTTAG_NO, LOTTAGGING_DESC.LOTTAG_GRIDSRNO")
            Dim dt As DataTable = objclsCMST.search("ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, LOTTAGGING.LOTTAG_NO AS SRNO, LOTTAGGING.LOTTAG_DATE AS DATE, ISNULL(LOTTAGGING.LOTTAG_LBLLOTTOTALMTRS, 0) AS LOTTOTALMTRS,  ISNULL(LOTTAGGING.LOTTAG_LBLLOTTOTALBALMTRS, 0) AS LOTTOTALBALMTRS, ISNULL(LOTTAGGING.LOTTAG_LBLLOTTOTALADJMTRS, 0) AS LOTTOTALADJMTRS, ISNULL(LOTTAGGING.LOTTAG_LBLPROGTOTALMTRS, 0)  AS PROGTOTALMTRS, ISNULL(LOTTAGGING.LOTTAG_LBLPROGTOTALBALMTRS, 0) AS PROGTOTALBALMTRS, ISNULL(LOTTAGGING.LOTTAG_LBLPROGTOTALADJMTRS, 0) AS PROGTOTALADJMTRS" & SELECTCLAUSE, "", "  LOTTAGGING LEFT OUTER JOIN LEDGERS ON LOTTAGGING.LOTTAG_LEDGERID = LEDGERS.Acc_id", WHERECLAUSE & " AND LOTTAGGING.LOTTAG_YEARID = " & YearId & " ORDER BY LOTTAGGING.LOTTAG_NO")

            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal PROGRAMNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJPROGRAM As New LotTagging
                OBJPROGRAM.MdiParent = MDIMain
                OBJPROGRAM.EDIT = editval
                OBJPROGRAM.LOTTAGNO = PROGRAMNO
                OBJPROGRAM.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class