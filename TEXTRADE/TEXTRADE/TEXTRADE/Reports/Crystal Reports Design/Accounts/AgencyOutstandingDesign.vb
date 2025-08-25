
Imports BL
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO

Public Class AgencyOutstandingDesign

    Dim RPTOUTSTANDINGBUYER As New AgencyOutstandingReport_BuyerDetails
    Dim RPTOUTSTANDINGSELLER As New AgencyOutstandingReport_BuyerDetails

    'NEWLY ADDED
    Public REPORTNAME As String
    Public DAYS As String
    Public TODATE As Date
    Public ADDRESS As Integer
    Public NEWPAGE As Boolean
    Public FRMSTRING As String
    Public PARTYNAME As String = ""
    Public AGENTNAME As String = ""
    Public SELLERNAME As String = ""
    Public selfor_ss As String
    Public PERIOD As String
    Public INTEREST As Double
    Public INTDAYS As Integer
    Public SHOWPRINTDATE As Integer
    Public SHOWREMARKS As Integer
    Public SHOWDETAILS As Integer
    Public MULTICMP As Integer

    Public REGNAME As String
    Public BILLNO As Integer
    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1

    Private Sub AgencyOutstandingDesign_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If DIRECTPRINT = True Then
                PRINTDIRECTADVICE()
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor

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


            If FRMSTRING = "BUYEROUTSTANDINGRECDTLS" Then crTables = RPTOUTSTANDINGBUYER.Database.Tables
            If FRMSTRING = "SELLEROUTSTANDINGRECDTLS" Then crTables = RPTOUTSTANDINGSELLER.Database.Tables


            'If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then crTables = RPTOUTSTANDINGALLSUMMREC.Database.Tables
            'If FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then crTables = RPTOUTSTANDINGALLSUMMPAY.Database.Tables
            'If FRMSTRING = "OUTSTANDINGALLDTLS" Then crTables = RPTOUTSTANDINGALLDTLS.Database.Tables

            'If FRMSTRING = "OUTSTANDINGPAYSUMM" Then crTables = RPTOUTSTANDINGPAYSUMM.Database.Tables
            'If FRMSTRING = "OUTSTANDINGRECSUMM" Then crTables = RPTOUTSTANDINGRECSUMM.Database.Tables
            'If FRMSTRING = "OUTSTANDINGPAYDTLS" Then crTables = RPTOUTSTANDINGPAYDTLS.Database.Tables

            'If FRMSTRING = "BROKEROUTSTANDINGPAYSUMM" Then crTables = RPTBROKEROUTSTANDINGPAYSUMM.Database.Tables
            'If FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then crTables = RPTBROKEROUTSTANDINGRECSUMM.Database.Tables
            'If FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then crTables = RPTBROKEROUTSTANDINGPAYDTLS.Database.Tables
            'If FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Then crTables = RPTBROKEROUTSTANDINGRECDTLS.Database.Tables

            'If FRMSTRING = "INTOUTSTANDINGREC" Then crTables = RPTINTOUTSTANDINGREC.Database.Tables
            'If FRMSTRING = "INTOUTSTANDINGPAY" Then crTables = RPTINTOUTSTANDINGPAY.Database.Tables

            'If FRMSTRING = "OUTSTANDINGRECRUNBALDTLS" Then crTables = RPTOUTSTANDINGRECRUNBAL.Database.Tables
            'If FRMSTRING = "OUTSTANDINGPAYRUNBALDTLS" Then crTables = RPTOUTSTANDINGPAYRUNBAL.Database.Tables

            'If FRMSTRING = "RECINVENTORYOUTSTANDING" Then crTables = RPTRECITEMOUTSTANDING.Database.Tables
            'If FRMSTRING = "PAYINVENTORYOUTSTANDING" Then crTables = RPTPAYITEMOUTSTANDING.Database.Tables

            'If FRMSTRING = "RECBROKERINVENTORYOUTSTANDING" Then crTables = RPTRECBROKERITEMOUTSTANDING.Database.Tables
            'If FRMSTRING = "PAYBROKERINVENTORYOUTSTANDING" Then crTables = RPTPAYBROKERITEMOUTSTANDING.Database.Tables

            'If FRMSTRING = "RECBROKERINVENTORYOUTSTANDINGRUNBAL" Then crTables = RPTRECBROKERITEMOUTSTANDINGRUNBAL.Database.Tables
            'If FRMSTRING = "PAYBROKERINVENTORYOUTSTANDINGRUNBAL" Then crTables = RPTPAYBROKERITEMOUTSTANDINGRUNBAL.Database.Tables

            'If FRMSTRING = "ALLBILLOUTSTANDINGREC" Then crTables = RPTALLOUTSTANDINGREC.Database.Tables
            'If FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then crTables = RPTALLOUTSTANDINGPAY.Database.Tables

            'If FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then crTables = RPTONLYOUTSTANDINGREC.Database.Tables
            'If FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then crTables = RPTONLYOUTSTANDINGPAY.Database.Tables

            'If FRMSTRING = "REMINDERLETTERREC" Then crTables = RPTREMINDERLETTERREC.Database.Tables
            'If FRMSTRING = "REMINDERLETTERPAY" Then crTables = RPTREMINDERLETTERPAY.Database.Tables



            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            If FRMSTRING = "BUYEROUTSTANDINGRECDTLS" Then

                CRPO.ReportSource = RPTOUTSTANDINGBUYER
                RPTOUTSTANDINGBUYER.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGBUYER.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                RPTOUTSTANDINGBUYER.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGBUYER.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGBUYER.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTOUTSTANDINGBUYER.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                RPTOUTSTANDINGBUYER.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 0
                RPTOUTSTANDINGBUYER.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTOUTSTANDINGBUYER.DataDefinition.FormulaFields("MULTICMP").Text = MULTICMP
                RPTOUTSTANDINGBUYER.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

            End If

            CRPO.SelectionFormula = selfor_ss

            If FRMSTRING = "BUYEROUTSTANDINGRECDTLS" Then
                CRPO.ReportSource = RPTOUTSTANDINGBUYER
            End If
            '************************ END *******************

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Try
            Dim emailid As String = ""
            Dim emailid1 As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Transfer()
            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\Agency Outstanding Report.PDF"

            If PARTYNAME <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable = OBJCMN.SEARCH("ACC_EMAIL AS EMAILID", "", "LEDGERS", " and ACC_CMPNAME = '" & PARTYNAME & "' AND ACC_YEARID=" & YearId)
                If dt.Rows.Count > 0 Then
                    emailid = dt.Rows(0).Item(0).ToString
                End If
            End If

            If AGENTNAME <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable = OBJCMN.SEARCH("ACC_EMAIL AS EMAILID", "", "LEDGERS", " and ACC_CMPNAME = '" & AGENTNAME & "' AND ACC_YEARID=" & YearId)
                If dt.Rows.Count > 0 Then
                    emailid1 = dt.Rows(0).Item(0).ToString
                End If
            End If


            If emailid <> "" Then objmail.cmbfirstadd.Text = emailid
            If emailid1 <> "" Then objmail.cmbsecondadd.Text = emailid1
            objmail.subject = "OUTSTANDING"
            objmail.Show()
            objmail.BringToFront()
            Windows.Forms.Cursor.Current = Cursors.Arrow
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\Agency Outstanding Report.pdf"


            If FRMSTRING = "BUYEROUTSTANDINGRECDTLS" Then
                expo = RPTOUTSTANDINGBUYER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGBUYER.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub AgencyOutstandingDesign_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If File.Exists(Application.StartupPath & "\" & PARTYNAME & "Agency Outstanding Report" & ".PDF") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\" & PARTYNAME & "Agency Outstanding Report" & ".PDF")
            Transfer()
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = PARTYNAME
            OBJWHATSAPP.AGENTNAME = AGENTNAME

            If File.Exists(Application.StartupPath & "\Agency Outstanding Report" & ".PDF") And PARTYNAME <> "" Then My.Computer.FileSystem.RenameFile(Application.StartupPath & "\Agency Outstanding Report" & ".PDF", PARTYNAME & "Agency Outstanding Report" & ".PDF")

            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & PARTYNAME & "Agency Outstanding Report" & ".PDF")
            OBJWHATSAPP.FILENAME.Add(PARTYNAME & "Agency Outstanding Report.pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
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

            Dim OBJ As New Object
            If FRMSTRING = "BUYEROUTSTANDINGRECDTLS" Then
                OBJ = New AgencyOutstandingReport_BuyerDetails
                OBJ.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                OBJ.DataDefinition.FormulaFields("TODATE").Text = "#" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "#"
                OBJ.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                OBJ.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                OBJ.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                OBJ.DataDefinition.FormulaFields("SHOWREMARKS").Text = SHOWREMARKS
                OBJ.DataDefinition.FormulaFields("SHOWITEMDTLS").Text = 0
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                OBJ.DataDefinition.FormulaFields("MULTICMP").Text = MULTICMP
                OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
            End If


            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = selfor_ss
            OBJ.REFRESH()


            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                If File.Exists(Application.StartupPath & "\" & PARTYNAME & "_AGENCYOUTSTANDING" & ".PDF") Then File.Delete(Application.StartupPath & "\" & PARTYNAME & "_AGENCYOUTSTANDING" & ".PDF")
                OBJ.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath & "\" & PARTYNAME & "_AGENCYOUTSTANDING" & ".PDF")


                'Dim expo As New ExportOptions
                'Dim oDfDopt As New DiskFileDestinationOptions
                'oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_OUTSTANDING" & ".PDF"

                ''CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                'If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
                'expo = OBJ.ExportOptions
                'expo.ExportDestinationType = ExportDestinationType.DiskFile
                'expo.ExportFormatType = ExportFormatType.PortableDocFormat
                'expo.DestinationOptions = oDfDopt
                'OBJ.Export()
            End If
            OBJ.CLOSE()
            OBJ.DISPOSE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class