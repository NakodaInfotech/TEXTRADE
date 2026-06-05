Imports System.IO
Imports System.Runtime.Remoting
Imports System.Windows.Forms
Imports BL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class AgencyReceiptDesign
    Public recno As Integer
    Public recname As String
    Public REGNAME As String
    Public FRMSTRING As String
    Public FORMULA As String
    Public PERIOD As String
    Public WHERECLAUSE As String = ""
    Public SHOWNARR As Integer = 0
    Public strsearch As String

    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1


    Dim RPTAGENCYREC As New AgencyRecReport
    Dim RPT As New AgencyRecReport_ABHEE
    Dim OBJ As New Object


    Private Sub AgencyReceiptDesign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strsearch As String
        strsearch = ""
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


            If ClientName = "ABHEE" Then
                crTables = RPT.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next


            strsearch = strsearch & "  {AGENCYRECEIPT_REPORT.RECEIPTNO} = " & recno & "  And {AGENCYRECEIPT_REPORT.REGNAME} = '" & REGNAME & "'  and {AGENCYRECEIPT_REPORT.CMPID} = " & CmpId & " and {AGENCYRECEIPT_REPORT.LOCATIONID} = " & Locationid & " and {AGENCYRECEIPT_REPORT.YEARID} = " & YearId
            crpo.SelectionFormula = strsearch

            OBJ = RPT

            If ClientName = "ABHEE" Then
                'ADD DATA IN TEMPAGENCYPAYMENTDETAILS
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPAGENCYPAYMENTDETAILS WHERE YEARID = " & YearId, "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO TEMPAGENCYPAYMENTDETAILS SELECT RECNO,  RECDATE, NAME, RECREMARKS, CHQNO, RECAMT, CMPID, YEARID, RECTYPE, RECINITIALS, RECREMARKS FROM AGENCYRECEIPTMASTER INNER JOIN AGENCYRECEIPTMASTER_DESC ON AGENCYRECEIPTMASTER.Areceipt_no= AGENCYRECEIPTMASTER_DESC.Areceipt_no AND AGENCYRECEIPTMASTER.Areceipt_registerid= AGENCYRECEIPTMASTER_DESC.Areceipt_registerid AND AGENCYRECEIPTMASTER.Areceipt_yearid= AGENCYRECEIPTMASTER_DESC.Areceipt_yearid INNER JOIN REGISTERMASTER ON AGENCYRECEIPTMASTER.Areceipt_registerid = REGISTERMASTER.REGISTER_ID INNER JOIN AGENCYOUTSTANDINGREPORT_DETAILS ON AGENCYRECEIPTMASTER_DESC.Areceipt_yearid = AGENCYOUTSTANDINGREPORT_DETAILS.YEARID AND AGENCYRECEIPTMASTER.Areceipt_ledgerid = AGENCYOUTSTANDINGREPORT_DETAILS.LEDGERID AND AGENCYRECEIPTMASTER_DESC.Areceipt_BILLINITIALS = AGENCYOUTSTANDINGREPORT_DETAILS.BILLINITIALS AND AGENCYOUTSTANDINGREPORT_DETAILS.RECINITIALS <> AGENCYRECEIPTMASTER.Areceipt_initials WHERE AGENCYRECEIPTMASTER_DESC.Areceipt_no = " & Val(recno) & " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "' And AGENCYRECEIPTMASTER_DESC.Areceipt_yearid = " & YearId, "", "")
                crpo.ReportSource = RPT
            End If


            crpo.Zoom(100)
            crpo.Refresh()

        Catch Exp As LoadSaveReportException
            MsgBox("Incorrect path for loading report.",
                    MsgBoxStyle.Critical, "Load Report Error")

        Catch Exp As Exception
            MsgBox(Exp.Message, MsgBoxStyle.Critical, "General Error")

        End Try
    End Sub

    Private Sub AgencyReceiptDesign_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub sendmailtool_Click(sender As Object, e As EventArgs) Handles sendmailtool.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()
        Dim tempattachment As String

        Dim objmail As New SendMail

        If FRMSTRING = "AGENCYREC" Then
            tempattachment = "AGENCYRECEIPTREPORT"
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
    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\AGENCYRECEIPTREPORT.pdf"

            'If FRMSTRING = "AGENCYREC" Then
            '    expo = RPTAGENCYREC.ExportOptions
            '    expo.ExportDestinationType = ExportDestinationType.DiskFile
            '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
            '    expo.DestinationOptions = oDfDopt
            '    RPTAGENCYREC.Export()
            'End If 
            expo = OBJ.ExportOptions
            OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
            expo.ExportDestinationType = ExportDestinationType.DiskFile
            expo.ExportFormatType = ExportFormatType.PortableDocFormat
            expo.DestinationOptions = oDfDopt
            OBJ.Export()
            OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\AGENCYRECEIPTREPORT.PDF")
            OBJWHATSAPP.FILENAME.Add("AGENCYRECEIPTREPORT.pdf")
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

            'strsearch = strsearch & "  {AGENCYRECEIPT_REPORT.areceipt_no}= " & recno & " AND {AGENCYRECEIPT_REPORT.REGNAME}= '" & REGNAME & "' and {LEDGERS.Acc_cmpname} = '" & recname & "' and {AGENCYRECEIPT_REPORT.CMPID} = " & CmpId & " and {AGENCYRECEIPT_REPORT.LOCATIONID} = " & Locationid & " and {AGENCYRECEIPT_REPORT.YEARID} = " & YearId
            FORMULA = "{AGENCYRECEIPT_REPORT.RECEIPTNO} = " & recno & " and {AGENCYRECEIPT_REPORT.CMPID} = " & CmpId & " and {AGENCYRECEIPT_REPORT.LOCATIONID} = " & Locationid & " and {AGENCYRECEIPT_REPORT.YEARID} = " & YearId & " and {AGENCYRECEIPT_REPORT.REGNAME} = '" & REGNAME & "'"
            crpo.SelectionFormula = FORMULA
            Dim OBJ As New Object

            'OBJ = New AgencyRecReport
            OBJ = New AgencyRecReport_ABHEE

            'If ClientName = "CHINTAN" Then RPTAGENCYREC.DataDefinition.FormulaFields("SENDMAIL").Text = "1"

            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = FORMULA

            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions
                oDfDopt.DiskFileName = Application.StartupPath & "\" & recname & "AGENCYRECEIPT_" & recno & ".pdf"

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
End Class