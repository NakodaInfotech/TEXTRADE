
Imports BL
Imports System.Windows.Forms
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class BeamIssueDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub StoreInwardDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Windows.Forms.Keys.Enter Then
                CMDEDIT_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.R Then
                Call TOOLREFRESH_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                Call TOOLEXCEL_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub StoreInwardDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'JOB OUT'")
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
            Dim OBJSTORE As New ClsBeamIssue
            OBJSTORE.alParaval.Add(0)
            OBJSTORE.alParaval.Add(YearId)
            Dim DT As DataTable = OBJSTORE.SELECTISSUE
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal ISSUENO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJSTORES As New BeamIssue
                OBJSTORES.MdiParent = MDIMain
                OBJSTORES.EDIT = editval
                OBJSTORES.TEMPBEAMISSUENO = ISSUENO
                OBJSTORES.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("BEAMISSUENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("BEAMISSUENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Beam Issue Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Beam Issue  Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Beam Issue  Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Beam Issue Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

    Private Sub CMDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDADD.Click
        Try
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTFROM.Validated
        If TXTFROM.Text.Trim <> "" Then TXTTO.Focus()
    End Sub

    Private Sub TXTCOPIES_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCOPIES.Validating
        If Val(TXTCOPIES.Text.Trim) <= 0 Then TXTCOPIES.Text = 1
    End Sub

    Private Sub TOOLREFRESH_Click_1(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTFROM.KeyPress, TXTTO.KeyPress, TXTCOPIES.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0 Then Exit Sub

            If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                MsgBox("Enter Proper Beam Issue Nos", MsgBoxStyle.Critical)
                Exit Sub
            Else
                If MsgBox("Wish to Print Beam Issue from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub serverprop(ByVal fromno As Integer, ByVal tono As Integer, Optional ByVal NOOFCOPIES As Integer = 1)

        For I As Integer = fromno To tono
            Dim OBJINV As New BeamIssueDesign
            Dim RPTBEAMISSUE As New BeamIssueReport

            '**************** SET SERVER ************************
            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table


            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With




            crTables = RPTBEAMISSUE.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next


            OBJINV.MdiParent = MDIMain

            RPTBEAMISSUE.PrintOptions.PaperSize = PaperSize.PaperA5
            RPTBEAMISSUE.RecordSelectionFormula = "{BEAMISSUEMASTER.BEAMISS_NO}=" & Val(I) & " and {BEAMISSUEMASTER.BEAMISS_yearid}=" & YearId
            RPTBEAMISSUE.PrintToPrinter(NOOFCOPIES, True, 0, 0)

            'OBJINV.MdiParent = MDIMain

            'If ClientName = "SVS" Then
            '    RPTGDN_SVS.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
            '    RPTGDN_SVS.RecordSelectionFormula = "{GDN.GDN_no}=" & Val(I) & " and {GDN.GDN_yearid}=" & YearId
            '    RPTGDN_SVS.PrintToPrinter(NOOFCOPIES, True, 0, 0)
            'ElseIf ClientName = "SAFFRON" Then
            '    RPTGDN_SAFFRON.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
            '    RPTGDN_SAFFRON.RecordSelectionFormula = "{GDN.GDN_no}=" & Val(I) & " and {GDN.GDN_yearid}=" & YearId
            '    RPTGDN_SAFFRON.PrintToPrinter(NOOFCOPIES, True, 0, 0)
            'ElseIf ClientName = "SKF" Then
            '    RPTGDN_SKF.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
            '    RPTGDN_SKF.RecordSelectionFormula = "{GDN.GDN_no}=" & Val(I) & " and {GDN.GDN_yearid}=" & YearId
            '    RPTGDN_SKF.PrintToPrinter(NOOFCOPIES, True, 0, 0)
            'ElseIf ClientName = "KDFAB" Then
            '    RPTGDN_KDFAB.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
            '    RPTGDN_KDFAB.RecordSelectionFormula = "{GDN.GDN_no}=" & Val(I) & " and {GDN.GDN_yearid}=" & YearId
            '    RPTGDN_KDFAB.PrintToPrinter(NOOFCOPIES, True, 0, 0)
            'ElseIf ClientName = "SANGHVI" Then
            '    RPTGDN_SANGHVI.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
            '    RPTGDN_SANGHVI.RecordSelectionFormula = "{GDN.GDN_no}=" & Val(I) & " and {GDN.GDN_yearid}=" & YearId
            '    RPTGDN_SANGHVI.PrintToPrinter(NOOFCOPIES, True, 0, 0)
            'Else
            '    RPTGDN.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
            '    RPTGDN.RecordSelectionFormula = "{GDN.GDN_no}=" & Val(I) & " and {GDN.GDN_yearid}=" & YearId
            '    RPTGDN.PrintToPrinter(NOOFCOPIES, True, 0, 0)
            'End If

        Next
    End Sub

End Class