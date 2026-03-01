Imports BL
Imports DevExpress.XtraGrid.Views.Grid


Public Class RollsRecdFromWarperDetails


    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT


    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
            Me.Close()
        End Sub

        Sub showform(ByVal EDITVAL As Boolean, ByVal ROLLRECDNO As Integer)
            Try
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim OBJROLLREC As New RollsRecdFromWarper
                OBJROLLREC.EDIT = EDITVAL
                OBJROLLREC.MdiParent = MDIMain
                OBJROLLREC.TEMPROLLSRECDNO = ROLLRECDNO
                OBJROLLREC.Show()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Private Sub RollsRecdFromWarperDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ROLLS RECD'")
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

        Private Sub RollsRecdFromWarperDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.Alt = True And e.KeyCode = Keys.R Then
                Call TOOLREFRESH_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                Call TOOLEXCEL_Click(sender, e)
            ElseIf e.KeyCode = Keys.OemQuotes Then
                e.SuppressKeyPress = True
            End If
        End Sub
        Sub fillgrid()
            Try
                Dim OBJROLL As New ClsRollsRecdFromWarper
                OBJROLL.alParaval.Add(0)
                OBJROLL.alParaval.Add(YearId)
                Dim dttable As DataTable = OBJROLL.SELECTROLLS()
                gridbilldetails.DataSource = dttable
                If dttable.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
            Try
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                showform(True, gridbill.GetFocusedRowCellValue("ROLLRECDNO"))
            Catch ex As Exception
                Throw ex
            End Try
        End Sub


        Private Sub TOOLREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
            Try
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                fillgrid()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
            Try
                Dim PATH As String = "" = ""
                If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
                PATH = Application.StartupPath & "\Rolls Received Details.XLS"

                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                Dim workbook As String = PATH
                If FileIO.FileSystem.FileExists(PATH) = True Then Interaction.GetObject(workbook).close(False)
                GC.Collect()

                Dim PERIOD As String = AccFrom & " - " & AccTo

                opti.SheetName = "Rolls Received Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Rolls Received Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
            Try
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                showform(True, gridbill.GetFocusedRowCellValue("ROLLRECDNO"))
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub CMDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDADD.Click
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

        Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
            Try
                If e.RowHandle >= 0 Then
                    Dim View As GridView = sender
                    If Val(View.GetRowCellDisplayText(e.RowHandle, View.Columns("OUTROLLS"))) > 0 Or Val(View.GetRowCellDisplayText(e.RowHandle, View.Columns("OUTWT"))) > 0 Then
                        e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                        e.Appearance.BackColor = Color.Yellow
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        'Private Sub TXTFROM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTFROM.Validated
        '    If TXTFROM.Text.Trim <> "" Then TXTTO.Focus()
        'End Sub

        'Private Sub TXTCOPIES_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCOPIES.Validating
        '    If Val(TXTCOPIES.Text.Trim) <= 0 Then TXTCOPIES.Text = 1
        'End Sub

        'Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        '    Try
        '        If Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Then Exit Sub
        '        If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
        '            MsgBox("Enter Proper Program Nos", MsgBoxStyle.Critical)
        '            Exit Sub
        '        End If
        '        If MsgBox("Wish to Print Program from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        '        If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings
        '        TXTCOPIES.Text = Val(PRINTDIALOG.PrinterSettings.Copies)
        '        serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), "", "PROGRAMREPORT", Val(TXTCOPIES.Text.Trim), PRINTDIALOG)

        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Sub
    End Class

