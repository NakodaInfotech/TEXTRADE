﻿
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class ProgramDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub ProgramDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub ProgramDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim WHERECLAUSE As String = ""
            Dim SELECTCLAUSE As String = ""
            'If ClientName = "AVIS" Then SELECTCLAUSE = " ,CASE WHEN PROGRAMMASTER.PROGRAM_CARDRECDATE <> '' AND PROGRAMMASTER_DESC.PROGRAM_PROGISSDATE <> '' THEN DATEDIFF(DAY, CONVERT(date, PROGRAMMASTER.PROGRAM_CARDRECDATE,103 ), CONVERT(date, PROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,103 ))  ELSE 0 END AS CARDRECPRGISSDAYS, CASE WHEN PROGRAMMASTER_DESC.PROGRAM_PROGISSDATE <> '' THEN DATEDIFF(DAY, CONVERT(date, PROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,103 ), CAST(GETDATE() AS DATE))  ELSE 0 END AS PRGISSDAYS, CASE WHEN PROGRAMMASTER_DESC.PROGRAM_PRODCUTTING <> '' THEN DATEDIFF(DAY, CONVERT(date, PROGRAMMASTER_DESC.PROGRAM_PRODCUTTING,103 ), CAST(GETDATE() AS DATE))  ELSE 0 END AS PRODCUTTDAYS, CASE WHEN PROGRAMMASTER_DESC.PROGRAM_FINISHCUTTING <> '' THEN DATEDIFF(DAY, CONVERT(date, PROGRAMMASTER_DESC.PROGRAM_FINISHCUTTING,103 ), CAST(GETDATE() AS DATE))  ELSE 0 END AS FINISHCUTTDAYS  "
            'If CHKPENDING.Checked = True Then WHERECLAUSE = " AND ISNULL(PROGRAMMASTER_DESC.PROGRAM_STATUS,'') = '' AND (ROUND(((ISNULL(PROGRAMMASTER_DESC.PROGRAM_PCS,0) - ISNULL(PROGRAMMASTER_DESC.PROGRAM_RECDPCS,0)) / ISNULL(PROGRAMMASTER_DESC.PROGRAM_PCS,0))*100,2)) >= 90"
            If CHKPENDING.Checked = True Then WHERECLAUSE = " AND ISNULL(PROGRAMMASTER_DESC.PROGRAM_STATUS,'') = '' AND ROUND(ISNULL(PROGRAMMASTER_DESC.PROGRAM_PCS, 0) - ISNULL(PROGRAMMASTER_DESC.PROGRAM_RECDPCS, 0), 2) > 0 "
            Dim dt As DataTable = objclsCMST.search("PROGRAMMASTER.PROGRAM_NO AS SRNO, PROGRAMMASTER.PROGRAM_DATE AS DATE, ISNULL(PROGRAMMASTER.PROGRAM_CARDRECDATE, '') AS CARDRECDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(DESIGNMASTER.DESIGN_CADNO, '') AS CADNO, ISNULL(ITEMMASTER.ITEM_GREYWIDTH, '') AS GREYWIDTH, ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(PROGRAMMASTER_DESC.PROGRAM_URGENT, 0) AS URGENT, ISNULL(PROGRAMMASTER_DESC.PROGRAM_PCS, 0) AS PCS, ISNULL(PROGRAMMASTER_DESC.PROGRAM_LOTNO, 0) AS LOTNO, ISNULL(PROGRAMMASTER_DESC.PROGRAM_PROGISSDATE, '') AS PROGISSDATE, ISNULL(PROGRAMMASTER_DESC.PROGRAM_STATUS, '') AS STATUS, ISNULL(PROGRAMMASTER_DESC.PROGRAM_PRODCUTTING, '') AS PRODCUTTING, ISNULL(PROGRAMMASTER_DESC.PROGRAM_FINISHCUTTING, '') AS FINISHCUTTING, ISNULL(PROGRAMMASTER_DESC.PROGRAM_INWARDDATE, '') AS INWARDDATE, ISNULL(PROGRAMMASTER_DESC.PROGRAM_RECDPCS, 0) AS RECDPCS,  ROUND(ISNULL(PROGRAMMASTER_DESC.PROGRAM_PCS, 0) - ISNULL(PROGRAMMASTER_DESC.PROGRAM_RECDPCS, 0), 2) AS BALPCS, ISNULL(PROGRAMMASTER_DESC.PROGRAM_BARCODE, '') AS BARCODE, ISNULL(PROGRAMMASTER_DESC.PROGRAM_RATE, 0) AS RATE, ISNULL(PROGRAMMASTER.PROGRAM_PROCESSNAME, '') AS PROCESSNAME, ISNULL(PROGRAMMASTER.PROGRAM_FINISHTYPE, '') AS FINISHTYPE, ISNULL(PROGRAMMASTER.PROGRAM_FOLD, '') AS FOLD, ISNULL(PROGRAMMASTER.PROGRAM_REMARKS, '') AS REMARKS, ISNULL(PROGRAMMASTER_DESC.PROGRAM_KHADI, 0) AS KHADI, ISNULL(PROGRAMMASTER_DESC.PROGRAM_ABOVETOSCREEN, 0) AS ABOVETWOSCREEN, ISNULL(PROGRAMMASTER_DESC.PROGRAM_MISCRATE, 0) AS MISCRATE, ISNULL(PROGRAMMASTER_DESC.PROGRAM_RATETYPE, '') AS RATETYPE" & SELECTCLAUSE, "", "     PROGRAMMASTER INNER JOIN  PROGRAMMASTER_DESC ON PROGRAMMASTER.PROGRAM_NO = PROGRAMMASTER_DESC.PROGRAM_NO AND PROGRAMMASTER.PROGRAM_YEARID = PROGRAMMASTER_DESC.PROGRAM_YEARID INNER JOIN ITEMMASTER ON PROGRAMMASTER_DESC.PROGRAM_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN  LEDGERS ON PROGRAMMASTER.PROGRAM_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN  COLORMASTER ON PROGRAMMASTER_DESC.PROGRAM_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON PROGRAMMASTER_DESC.PROGRAM_DESIGNID = DESIGNMASTER.DESIGN_id", WHERECLAUSE & " AND PROGRAMMASTER.PROGRAM_YEARID = " & YearId & " ORDER BY PROGRAMMASTER.PROGRAM_NO, PROGRAMMASTER_DESC.PROGRAM_GRIDSRNO")
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
                Dim OBJPROGRAM As New ProgramMaster
                OBJPROGRAM.MdiParent = MDIMain
                OBJPROGRAM.EDIT = editval
                OBJPROGRAM.PROGRAMNO = PROGRAMNO
                OBJPROGRAM.Show()
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

    Private Sub gridBILL_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
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

    Private Sub PrintToolStripButton_Click_1(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
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
            Dim ALATTACHMENT As New ArrayList
            If INVOICEMAIL = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                Dim OBJPROGRAM As New ProgramDesign
                OBJPROGRAM.MdiParent = MDIMain
                OBJPROGRAM.DIRECTPRINT = True
                OBJPROGRAM.DIRECTMAIL = INVOICEMAIL
                OBJPROGRAM.PRINTSETTING = PRINTDIALOG
                OBJPROGRAM.PROGRAMNO = Val(I)
                OBJPROGRAM.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJPROGRAM.Show()
                OBJPROGRAM.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\PROGRAM_" & I & ".pdf")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Program"
                OBJMAIL.ShowDialog()
            End If
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

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If Val(View.GetRowCellDisplayText(e.RowHandle, View.Columns("RECDPCS"))) > 0 Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("PRODCUTTING")) <> "" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightGreen
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EXCELEXPORT.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Program Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Program Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Program Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Porgram Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ProgramDetails_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName <> "AVIS" And ClientName <> "SUPEEMA" Then
                GCARDRECDATE.Visible = False
                GCADNO.Visible = False
                GGREYWIDTH.Visible = False
                GPROGISSDATE.Visible = False
                GPRODCUTTING.Visible = False
                GFINISHCUTTING.Visible = False
                GINWARDDATE.Visible = False
            Else
                GPCS.Caption = "Mtrs"
                GRECDPCS.Caption = "Recd Mtrs"
                GBALPCS.Caption = "Bal Mtrs"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class