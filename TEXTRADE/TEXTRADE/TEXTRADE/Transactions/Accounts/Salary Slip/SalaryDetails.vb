
Imports BL

Public Class SalaryDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT


    Private Sub SalaryDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Alt = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("ISNULL(SALARYMASTER.SAL_NO, 0) AS SRNO, SALARYMASTER.SAL_DATE AS SALDATE, ISNULL(EMPLOYEEMASTER.EMP_NAME, '') AS EMPNAME, ISNULL(SALARYMASTER.SAL_MONTH, '') AS MONTH, ISNULL(SALARYMASTER.SAL_WORKDAYS, 0) AS WORKDAYS, ISNULL(SALARYMASTER.SAL_PAYDAYS, 0) AS PAYDAYS, ISNULL(SALARYMASTER.SAL_LEAVE, 0) AS LEAVE, ISNULL(SALARYMASTER.SAL_TOTALDED, 0) AS TOTALDED, ISNULL(SALARYMASTER.SAL_TOTALEAR, 0) AS TOTALEAR, ISNULL(SALARYMASTER.SAL_NETTPAY, 0) AS NETTPAY, ISNULL(SALARYMASTER.SAL_REMARKS, '') AS REMARKS, ISNULL(SALARYMASTER.SAL_INWORDS, '') AS INWORDS, ISNULL(LEDGERS.Acc_cmpname, '') AS LEDGERNAME, ISNULL(LOANLEDGERS.Acc_cmpname, '') AS LOANLEDGERNAME, ISNULL(SALARYMASTER.SAL_ADVANCETAKEN, 0) AS ADVANCETAKEN, ISNULL(SALARYMASTER.SAL_LOANEMI, 0) AS LOANEMI", "", "SALARYMASTER INNER JOIN EMPLOYEEMASTER ON SALARYMASTER.SAL_EMPID = EMPLOYEEMASTER.EMP_ID INNER JOIN LEDGERS ON SALARYMASTER.SAL_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS LOANLEDGERS ON SALARYMASTER.SAL_LOANLEDGERID = LOANLEDGERS.Acc_id", " AND SAL_YEARID = " & YearId & " ORDER BY SALARYMASTER.SAL_NO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal SALNO As Integer)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJSAL As New SalaryMaster
                OBJSAL.MdiParent = MDIMain
                OBJSAL.edit = editval
                OBJSAL.TEMPSALNO = SALNO
                OBJSAL.Show()
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

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        fillgrid()
    End Sub

    Private Sub CHQPRINTTOOL_Click(sender As Object, e As EventArgs) Handles CHQPRINTTOOL.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub


            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper SALNO Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Wish to Print SALNO from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPDIRECT()
                End If
            Else
                If MsgBox("Wish to Print Selected SALARY Note ?", MsgBoxStyle.YesNo) = vbYes Then
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
            'Dim GSTRPT As Boolean = False
            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            'If MsgBox("Wish to Send Whatsapp of SALARY With GST...?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then GSTRPT = True
            For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                Dim OBJJO As New SalarySlipDesign
                OBJJO.MdiParent = MDIMain
                OBJJO.DIRECTPRINT = True
                If ClientName = "MANSI" Then
                    If MsgBox("Print SALARY for EMPLOYEE", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJJO.FRMSTRING = "SALARY" Else OBJJO.FRMSTRING = "SALARY"
                Else
                    OBJJO.FRMSTRING = "SALARY"
                End If
                OBJJO.DIRECTMAIL = INVOICEMAIL
                OBJJO.DIRECTWHATSAPP = WHATSAPP
                'OBJJO.GSTRPT = GSTRPT
                OBJJO.PRINTSETTING = PRINTDIALOG
                OBJJO.MONTHNAME = "{SALARYMASTER.SAL_NO}=" & Val(I) & " and {SALARYMASTER.SAL_yearid}=" & YearId
                OBJJO.SALNO = Val(I)
                OBJJO.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJJO.Show()
                OBJJO.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\SALARY_" & I & ".pdf")
                FILENAME.Add("SALARY_" & I & ".pdf")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "SALARY"
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
            'Dim GSTRPT As Boolean = False

            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            'If MsgBox("Wish to Send Whatsapp of SALARY With GST...?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then GSTRPT = True
            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                Dim ROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

                Dim OBJJO As New SalarySlipDesign
                OBJJO.MdiParent = MDIMain
                OBJJO.DIRECTPRINT = True
                If ClientName = "MANSI" Then
                    If MsgBox("Print SALARY for EMPLOYEE", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJJO.FRMSTRING = "SALARY" Else OBJJO.FRMSTRING = "SALARY"
                Else
                    OBJJO.FRMSTRING = "SALARY"
                End If
                OBJJO.DIRECTMAIL = INVOICEMAIL
                OBJJO.DIRECTWHATSAPP = WHATSAPP
                'OBJJO.GSTRPT = GSTRPT
                OBJJO.PRINTSETTING = PRINTDIALOG
                OBJJO.WHERECLAUSE = "{SALARYMASTER.SAL_NO}=" & Val(ROW("SRNO")) & " and {SALARYMASTER.SAL_yearid}=" & YearId
                OBJJO.PARTYNAME = ROW("EMPNAME")
                OBJJO.MONTHNAME = ROW("MONTH")
                OBJJO.SALNO = Val(ROW("SRNO"))
                OBJJO.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJJO.Show()
                OBJJO.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\SALARY_" & Val(ROW("SRNO")) & ".pdf")
                FILENAME.Add("SALARY_" & Val(ROW("SRNO")) & ".pdf")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "SALARY"
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


    Private Sub SalaryDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'PAYROLL'")
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

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Salary Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Salary Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Salary Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SalaryDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "SVS" Then Me.Close()
    End Sub
End Class