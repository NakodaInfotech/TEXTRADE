
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports BL

Public Class receipt_advice

    Public recno As Integer
    Public recname As String
    Public REGNAME As String
    Public FRMSTRING As String = ""

    Dim OBJREC As New RecReport
    Dim OBJREC_NAKODAINFOTECH As New RecReport_NAKODAINFOTECH
    Dim RPTBANKSLIP As New Receipt_BankSlip
    Dim RPTOFFICECOPY As New Receipt_OfficeCopy

    Dim OBJRECMONTHLY As New RecMonthlyReport
    Dim OBJRECPARTYSUMM As New RecPartySummReport
    Dim OBJRECREG As New ReceiptRegisterReport


    Public WHERECLAUSE As String = ""
    Public PERIOD As String = ""
    Public SHOWNARR As Integer = 0

    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1

    Private Sub receipt_advice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control = True And e.KeyCode = Keys.P Then
            CRPO.PrintReport()
        ElseIf e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub receipt_advice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strsearch As String
        strsearch = ""

        Try

            If DIRECTPRINT = True Then
                PRINTDIRECTADVICE()
                Exit Sub
            End If


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

            If FRMSTRING = "BANKSLIP" Then
                strsearch = WHERECLAUSE
                crTables = RPTBANKSLIP.Database.Tables
            ElseIf FRMSTRING = "OFFICECOPY" Then
                strsearch = WHERECLAUSE
                crTables = RPTOFFICECOPY.Database.Tables


            ElseIf FRMSTRING = "RECMONTHLY" Then
                crTables = OBJRECMONTHLY.Database.Tables
            ElseIf FRMSTRING = "RECPARTYSUMM" Then
                crTables = OBJRECPARTYSUMM.Database.Tables
            ElseIf FRMSTRING = "RECREGISTER" Then
                crTables = OBJRECREG.Database.Tables

            Else
                strsearch = strsearch & "  {receiptmaster.receipt_no}= " & recno & " and {RECEIPT_REPORT.REGNAME}= '" & REGNAME & "' and {ledgermaster.Acc_cmpname} = '" & recname & "' and {receiptmaster.receipt_cmpid} = " & CmpId & " and {receiptmaster.receipt_LOCATIONid} = " & Locationid & " and {receiptmaster.receipt_YEARid} = " & YearId
                If ClientName = "NAKODAINFOTECH" Then
                    crTables = OBJREC_NAKODAINFOTECH.Database.Tables
                Else
                    crTables = OBJREC.Database.Tables
                    If ClientName = "CHINTAN" Then OBJREC.DataDefinition.FormulaFields("SENDMAIL").Text = "1"

                End If
            End If
            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next
            '************* END *******************


            CRPO.SelectionFormula = strsearch

            If FRMSTRING = "BANKSLIP" Then
                CRPO.ReportSource = RPTBANKSLIP
            ElseIf FRMSTRING = "OFFICECOPY" Then
                CRPO.ReportSource = RPTOFFICECOPY

            ElseIf FRMSTRING = "RECMONTHLY" Then
                CRPO.SelectionFormula = WHERECLAUSE
                CRPO.ReportSource = OBJRECMONTHLY
                OBJRECMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "RECPARTYSUMM" Then
                CRPO.SelectionFormula = WHERECLAUSE
                CRPO.ReportSource = OBJRECPARTYSUMM
                OBJRECPARTYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "RECREGISTER" Then
                CRPO.SelectionFormula = WHERECLAUSE
                CRPO.ReportSource = OBJRECREG
                OBJRECREG.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJRECREG.DataDefinition.FormulaFields("SHOWNARR").Text = SHOWNARR
                OBJRECREG.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            Else
                If ClientName = "NAKODAINFOTECH" Then
                    CRPO.ReportSource = OBJREC_NAKODAINFOTECH
                Else
                    CRPO.ReportSource = OBJREC
                    If ClientName = "CHINTAN" Then OBJREC.DataDefinition.FormulaFields("SENDMAIL").Text = "1"

                End If
            End If


            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch Exp As LoadSaveReportException
            MsgBox("Incorrect path for loading report.",
                    MsgBoxStyle.Critical, "Load Report Error")

        Catch Exp As Exception
            MsgBox(Exp.Message, MsgBoxStyle.Critical, "General Error")

        End Try
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions

            If FRMSTRING = "BANKSLIP" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\BANKSLIP.PDF"
                expo = RPTBANKSLIP.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBANKSLIP.Export()
            ElseIf FRMSTRING = "OFFICECOPY" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\OFFICECOPY.PDF"
                expo = RPTOFFICECOPY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOFFICECOPY.Export()
            Else
                If ClientName = "NAKODAINFOTECH" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\RECEIPTREPORT.PDF"
                    expo = OBJREC_NAKODAINFOTECH.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    OBJREC_NAKODAINFOTECH.Export()
                Else
                    oDfDopt.DiskFileName = Application.StartupPath & "\RECEIPTREPORT.PDF"
                    expo = OBJREC.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    OBJREC.Export()
                End If
            End If




        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Sub PRINTDIRECTADVICE()
        Try
            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

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

            strsearch = " {receiptmaster.receipt_no}= " & recno & " and {RECEIPT_REPORT.REGNAME}= '" & REGNAME & "' and {receiptmaster.receipt_YEARid} = " & YearId
            CRPO.SelectionFormula = strsearch
            Dim OBJ As New Object
            If ClientName = "NAKODAINFOTECH" Then
                OBJ = New RecReport_NAKODAINFOTECH
            Else
                OBJ = New RecReport
                If ClientName = "CHINTAN" Then OBJREC.DataDefinition.FormulaFields("SENDMAIL").Text = "1"

            End If
            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = strsearch

            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions
                oDfDopt.DiskFileName = Application.StartupPath & "\" & recname & "RECEIPT_" & recno & ".pdf"

                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                expo = OBJ.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
            End If
            OBJ.CLOSE()
            OBJ.DISPOSE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()
        Dim tempattachment As String

        Dim objmail As New SendMail

        If FRMSTRING = "BANKSLIP" Then
            tempattachment = "BANKSLIP"
        ElseIf FRMSTRING = "OFFICECOPY" Then
            tempattachment = "OFFICECOPY"
        Else
            tempattachment = "RECEIPTREPORT"
        End If
        objmail.subject = "Receipt Voucher"

        If recname <> "" Then
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable = objclscommon.search(" acc_email ", "", " LEDGERS ", " and ACC_cmpname='" & recname & "' and ACC_cmpid=" & CmpId & " and ACC_LOCATIONid=" & Locationid & " and ACC_YEARid=" & YearId)
            If dt.Rows.Count > 0 Then
                emailid = dt.Rows(0).Item(0).ToString
            End If
        End If

        Try
            'Dim objmail As New SendMail
            objmail.attachment = tempattachment
            objmail.attachment = Application.StartupPath & "\" & tempattachment & ".PDF"
            If emailid <> "" Then
                objmail.cmbfirstadd.Text = emailid
            End If
            objmail.Show()
            objmail.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
        Windows.Forms.Cursor.Current = Cursors.Arrow
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\RECEIPTREPORT.PDF")
            OBJWHATSAPP.FILENAME.Add("RECEIPTREPORT.pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class