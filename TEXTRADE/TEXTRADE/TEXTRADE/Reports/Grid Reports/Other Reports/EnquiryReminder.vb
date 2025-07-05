Imports BL
Public Class EnquiryReminder
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub EnquiryReminder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DTFOLLOWUPDATE.Text = Mydate
            DTFOLLOWUPDATE.Focus()
            'DTFOLLOWUPDATE.SelectAll()
            If DTFOLLOWUPDATE.Text <> "__/__/____" Then fillgrid() Else MsgBox("Please Enter Followup Date")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclsCMST.search(" ENQ_NO AS ENQNO , ENQ_DATE AS DATE, ENQ_GIVENBY AS GIVENBY, ENQ_GIVENTO AS GIVENTO, ENQ_NEXTDATE AS NEXTDATE, ISNULL(SALESENQUIRY_FOLLOWUP.ENQ_NARRATION,'') AS NARRATION", "", "  SALESENQUIRY_FOLLOWUP", " AND SALESENQUIRY_FOLLOWUP.ENQ_NEXTDATE = '" & DTFOLLOWUPDATE.Text & "'  AND SALESENQUIRY_FOLLOWUP.ENQ_YEARID = " & YearId & " ORDER BY SALESENQUIRY_FOLLOWUP.ENQ_NO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
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

    Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs) Handles gridbill.DoubleClick
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("ENQNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub showform(ByVal EDITVAL As Boolean, ByVal ENQNO As Integer)
        Try
            If ENQNO = 0 Then
                Exit Sub
            End If

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJENQNO As New SalesEnquiry
            OBJENQNO.edit = EDITVAL
            OBJENQNO.MdiParent = MDIMain
            OBJENQNO.TEMPSONO = ENQNO
            OBJENQNO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EnquiryReminder_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.Alt = True And e.KeyCode = Keys.R Then
            Call TOOLREFRESH_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.P Then
            Call PrintToolStripButton_Click(sender, e)
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles TOOLEXCELL.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\ Enquiry Followup Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim workbook As String = PATH
            If FileIO.FileSystem.FileExists(PATH) = True Then Interaction.GetObject(workbook).close(False)
            GC.Collect()

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = " Enquiry Followup Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, " Enquiry Followup Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(sender As Object, e As EventArgs) Handles CMDEDIT.Click
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("ENQNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTFOLLOWUPDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTFOLLOWUPDATE.GotFocus
        DTFOLLOWUPDATE.SelectAll()
    End Sub

    Private Sub DTFOLLOWUPDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTFOLLOWUPDATE.Validating

        Try
            If DTFOLLOWUPDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTFOLLOWUPDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub DTFOLLOWUPDATE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTFOLLOWUPDATE.Validated
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ENQUIRY'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)



            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If DTFOLLOWUPDATE.Text <> "__/__/____" Then fillgrid() Else MsgBox("Please Enter Followup Date")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class