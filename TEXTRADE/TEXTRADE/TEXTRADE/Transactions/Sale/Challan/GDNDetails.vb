﻿
Imports BL
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.XtraGrid.Views.Grid

Public Class GDNDetails

    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim DTMAIL As New DataTable
    Dim DTWHATSAPP As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SALEOrderDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub SALEOrderDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GDN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            DTMAIL.Columns.Add("INVNO")
            DTMAIL.Columns.Add("REGID")
            DTMAIL.Columns.Add("REGNAME")
            DTMAIL.Columns.Add("PRINTINITIALS")
            DTMAIL.Columns.Add("DATE")
            DTMAIL.Columns.Add("NAME")
            DTMAIL.Columns.Add("PARTYEMAILID")
            DTMAIL.Columns.Add("AGENTNAME")
            DTMAIL.Columns.Add("AGENTEMAILID")
            DTMAIL.Columns.Add("GRANDTOTAL")
            DTMAIL.Columns.Add("SUBJECT")
            DTMAIL.Columns.Add("ATTACHMENT")
            DTMAIL.Columns.Add("FILENAME")

            DTWHATSAPP.Columns.Add("INVNO")
            DTWHATSAPP.Columns.Add("REGID")
            DTWHATSAPP.Columns.Add("REGNAME")
            DTWHATSAPP.Columns.Add("PRINTINITIALS")
            DTWHATSAPP.Columns.Add("DATE")
            DTWHATSAPP.Columns.Add("NAME")
            DTWHATSAPP.Columns.Add("PARTYWHATSAPP")
            DTWHATSAPP.Columns.Add("AGENTNAME")
            DTWHATSAPP.Columns.Add("AGENTWHATSAPP")
            DTWHATSAPP.Columns.Add("GRANDTOTAL")
            DTWHATSAPP.Columns.Add("SUBJECT")
            DTWHATSAPP.Columns.Add("ATTACHMENT")
            DTWHATSAPP.Columns.Add("FILENAME")


            FILLCHALLANTYPE(CMBTYPE)
            If ClientName <> "SAFFRON" And ClientName <> "SAFFRONOFF" Then fillgrid(" and dbo.gdn.gdn_yearid=" & YearId & " ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            'WE NEED ONLY GDN DATA HERE, WE HAVE ADDED A NEW GRID DETAILS REPORT FOR DESC DATA
            'Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, GDN.GDN_TYPENO AS TYPECHALLANNO, GDN.GDN_NO AS SRNO, ISNULL(GDN.GDN_TRANSREFNO, '') AS CHNO, GDN.GDN_date AS DATE, LEDGERS.Acc_cmpname AS CMPNAME, ISNULL(JOBBERLEDGERS.Acc_cmpname,'') AS JOBBERNAME, ISNULL(GDN.GDN_CONSIGNEE, '') AS CONSIGNEE, ISNULL(AGENTLEDGERS.Acc_cmpname,'') AS AGENT, GDN.GDN_SONO AS SONO, GDN.GDN_MULTISONO AS MULTISONO, GDN.GDN_SODATE AS SODATE, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, SUM(GDN_DESC.GDN_PCS) AS PCS, SUM(GDN_DESC.GDN_MTRS) AS MTRS, GDN.GDN_TOTALBALES AS TOTALBALES, GDN.GDN_BALENOFROM AS BALENOFROM, GDN_DESC.GDN_BALENO AS BALENO, ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS PACKING, ISNULL(CATEGORYMASTER.category_name, '') AS CATEGORY, ISNULL(GDN.GDN_TRANSREMARKS,'') AS PARTYPONO, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO,'') AS GRIDPARTYPONO, ISNULL(GDN.GDN_HOLDFORAPPROVAL,0) AS HOLDFORAPPROVAL, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME  ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id INNER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID INNER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON GDN.GDN_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS JOBBERLEDGERS ON GDN.GDN_JOBBERID = JOBBERLEDGERS.Acc_id LEFT OUTER JOIN CHALLANTYPEMASTER ON CHALLANTYPEMASTER.CHALLANTYPE_ID = GDN.GDN_TYPEID LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON GDN.GDN_DISPATCHTOID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON GDN.GDN_transid = TRANSLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYID = CITYMASTER.CITY_ID", TEMPCONDITION & " GROUP BY GDN.GDN_NO, ISNULL(GDN.GDN_TRANSREFNO, ''), GDN.GDN_date, LEDGERS.Acc_cmpname, ISNULL(GDN.GDN_CONSIGNEE, ''), GDN.GDN_SONO, GDN.GDN_SODATE, ITEMMASTER.item_name,  ISNULL(DESIGNMASTER.DESIGN_NO, ''), GDN.GDN_TOTALBALES, GDN_DESC.GDN_BALENO, GDN.GDN_TYPENO, ISNULL(PACKINGLEDGERS.Acc_cmpname, ''), ISNULL(CATEGORYMASTER.category_name, ''),  ISNULL(GDN.GDN_TRANSREMARKS, ''), ISNULL(GDN.GDN_HOLDFORAPPROVAL, 0), GDN.GDN_BALENOFROM, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, ''), ISNULL(COLORMASTER.COLOR_name, ''), ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(JOBBERLEDGERS.Acc_cmpname, ''), ISNULL(AGENTLEDGERS.Acc_cmpname, ''), ISNULL(CITYMASTER.CITY_NAME,''), GDN.GDN_MULTISONO ORDER BY GDN.GDN_NO")
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, GDN.GDN_TYPENO AS TYPECHALLANNO, GDN.GDN_NO AS SRNO, ISNULL(GDN.GDN_TRANSREFNO, '') AS CHNO, GDN.GDN_date AS DATE, LEDGERS.Acc_cmpname AS CMPNAME, ISNULL(JOBBERLEDGERS.Acc_cmpname, '') AS JOBBERNAME, ISNULL(GDN.GDN_CONSIGNEE, '') AS CONSIGNEE, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT, GDN.GDN_SONO AS SONO, GDN.GDN_MULTISONO AS MULTISONO, GDN.GDN_SODATE AS SODATE, GDN.GDN_TOTALPCS AS PCS, GDN.GDN_TOTALMTRS AS MTRS, GDN.GDN_TOTALBALES AS TOTALBALES, GDN.GDN_BALENOFROM AS BALENOFROM , ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS PACKING, ISNULL(GDN.GDN_TRANSREMARKS, '') AS PARTYPONO, ISNULL(GDN.GDN_HOLDFORAPPROVAL, 0) AS HOLDFORAPPROVAL, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(CITYMASTER.city_name, '') AS CITYNAME, ISNULL(LEDGERS.ACC_WHATSAPPNO, '0') AS PARTYWHATSAPP, ISNULL(LEDGERS.Acc_email, '') AS PARTYEMAIL, ISNULL(AGENTLEDGERS.Acc_email, '') AS AGENTEMAIL, ISNULL(AGENTLEDGERS.ACC_WHATSAPPNO, '0') AS AGENTWHATSAPP, ISNULL(USERMASTER.User_Name, '') AS USERNAME", "", "   GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN USERMASTER ON GDN.GDN_USERID = USERMASTER.User_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON GDN.GDN_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS JOBBERLEDGERS ON GDN.GDN_JOBBERID = JOBBERLEDGERS.Acc_id LEFT OUTER JOIN CHALLANTYPEMASTER ON CHALLANTYPEMASTER.CHALLANTYPE_ID = GDN.GDN_TYPEID LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON GDN.GDN_DISPATCHTOID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON GDN.GDN_transid = TRANSLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id", TEMPCONDITION & " ORDER BY GDN.GDN_NO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal gdnno As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objgdn As New GDN
                objgdn.MdiParent = MDIMain
                objgdn.EDIT = editval
                objgdn.TEMPGDNNO = gdnno
                objgdn.Show()
                'Me.Close()
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

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
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

    Private Sub TXTFROM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTFROM.Validated
        If TXTFROM.Text.Trim <> "" Then TXTTO.Focus()
    End Sub

    Private Sub TXTCOPIES_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCOPIES.Validating
        If Val(TXTCOPIES.Text.Trim) <= 0 Then TXTCOPIES.Text = 1
    End Sub

    Private Sub TXTFROM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTFROM.KeyPress, TXTTO.KeyPress, TXTCOPIES.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            'If Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0 Then Exit Sub

            'If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
            '    MsgBox("Enter Propoer Challan Nos", MsgBoxStyle.Critical)
            '    Exit Sub
            'Else
            '    If MsgBox("Wish to Print Challan from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            '    serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim))
            'End If

            'If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) Then  Exit Sub

            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Challan Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Wish to Print Challan from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings
                    serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim))
                End If
            Else
                If MsgBox("Wish to Print Selected Challan ?", MsgBoxStyle.YesNo) = vbYes Then
                    If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings
                    cmdok.Focus()
                    SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim))
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROP(ByVal fromno As Integer, ByVal tono As Integer, Optional ByVal NOOFCOPIES As Integer = 1, Optional ByVal FRMSTRING As String = "PRINT")
        Try
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList

            Dim GARMENTCHALLAN As Boolean = False
            If (ClientName = "MANSI"  Or ClientName = "CHINTAN") AndAlso MsgBox("Print Challan for Garments?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then GARMENTCHALLAN = True

            For I As Integer = fromno To tono

                '**************** SET SERVER ************************
                Dim crParameterFieldDefinitions As ParameterFieldDefinitions
                Dim crParameterFieldDefinition As ParameterFieldDefinition
                Dim crParameterValues As New ParameterValues
                Dim crParameterDiscreteValue As New ParameterDiscreteValue

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

                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions

                Dim OBJ As New Object
                If ClientName = "MAFATLAL" Then
                    OBJ = New GDNReport_A5
                ElseIf ClientName = "BALAJI" Or ClientName = "NAYRA" Then
                    OBJ = New GDNReport_BALAJI
                    If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                    If CHKHIDEPCS.Checked = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                    OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                ElseIf ClientName = "SOFTAS" Then
                    OBJ = New GDNReport_SOFTAS
                ElseIf ClientName = "BRILLANTO" Then
                    OBJ = New GDNReport_BRILLANTO
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    OBJ = New GDNReport_CC
                ElseIf ClientName = "DRDRAPES" Then
                    OBJ = New GDNReport_DRDRAPES
                ElseIf ClientName = "KCRAYON" Then
                    OBJ = New GDNReport_KCRAYON
                ElseIf ClientName = "KDFAB" Then
                    OBJ = New GDNReport_KDFAB
                ElseIf ClientName = "AKASHDEEP" Then
                    OBJ = New GDNReport_AKASHDEEP
                ElseIf ClientName = "MANINATH" Then
                    OBJ = New GDNReport_MANINATH
                ElseIf ClientName = "MOMAI" Then
                    OBJ = New GDNReport_MOMAI
                ElseIf ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                    OBJ = New GDNReport_SANGHVI
                ElseIf ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                    OBJ = New GDNReport_SAFFRON
                ElseIf ClientName = "SBA" Then
                    OBJ = New GDNReport_SBA
                ElseIf ClientName = "SUPEEMA" Then
                    OBJ = New GDNReport_SUPEEMA
                ElseIf ClientName = "SKF" Then
                    OBJ = New GDNReport_SKF
                ElseIf ClientName = "SVS" Then
                    OBJ = New GDNReport_SVS
                ElseIf ClientName = "AVIS" Then
                    OBJ = New GDNReport_AVIS
                ElseIf ClientName = "SHREENAKODA" Then
                    OBJ = New GDNReport_SHREENAKODA
                ElseIf ClientName = "SNCM" Then
                    OBJ = New GDNReport_SNCM
                ElseIf ClientName = "REALCORPORATION" Then
                    OBJ = New GDNReport_REALCORP
                ElseIf ClientName = "NTC" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "KUNAL" Or ClientName = "SURYODAYA" Or ClientName = "SSC" Or ClientName = "VALIANT" Then
                    OBJ = New GDNReport_NTC
                ElseIf ClientName = "PARAS" Or ClientName = "MARKIN" Then
                    OBJ = New GDNReport_PARASMARKIN
                    If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                Else

                    If GARMENTCHALLAN = True Then
                        OBJ = New GDNReport_GARMENT
                        If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                        If CHKHIDEPCS.Checked = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                        If FRMSTRING <> "PRINT" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                        If ClientName = "ALENCOT" Or ClientName = "MANSI" Or ClientName = "CHINTAN" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"

                        OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

                    Else
                        OBJ = New GDNReport_COMMON
                        If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                        If CHKHIDEPCS.Checked = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                        If ClientName = "MYCOT" Or ClientName = "SHUBHI" Then OBJ.DataDefinition.FormulaFields("PRINTRATE").Text = 1 Else OBJ.DataDefinition.FormulaFields("PRINTRATE").Text = 0
                        If FRMSTRING <> "PRINT" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                        If ClientName = "ALENCOT" Or ClientName = "MANSI" Or ClientName = "CHINTAN" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"

                        OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

                        If ClientName = "SUPRIYA" AndAlso MsgBox("Print Images?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJ.DataDefinition.FormulaFields("SHOWIMAGE").Text = "1"
                    End If

                End If

                crTables = OBJ.Database.Tables
                For Each crTable In crTables
                    crtableLogonInfo = crTable.LogOnInfo
                    crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                    crTable.ApplyLogOnInfo(crtableLogonInfo)
                Next

                OBJ.RecordSelectionFormula = "{GDN.GDN_no}=" & Val(I) & " and {GDN.GDN_yearid}=" & YearId

                If FRMSTRING = "PRINT" Then
                    OBJ.PrintOptions.PrinterName = PRINTDIALOG.PrinterSettings.PrinterName
                    If ClientName <> "AVIS" Then OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize Else OBJ.PrintOptions.PaperSize = PaperSize.PaperA5
                    OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                Else
                    oDfDopt.DiskFileName = Application.StartupPath & "\GDN_" & I & ".pdf"
                    expo = OBJ.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    OBJ.Export()
                    ALATTACHMENT.Add(oDfDopt.DiskFileName)
                    FILENAME.Add("GDN_" & I & ".pdf")
                End If

                OBJ.CLOSE()
                OBJ.DISPOSE()
            Next

            If FRMSTRING = "MAIL" Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Challan"
                OBJMAIL.ShowDialog()
            End If

            If FRMSTRING = "WHATSAPP" = True Then
                Dim OBJWHATSAPP As New SendWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.ShowDialog()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPSELECTED(ByVal fromno As Integer, ByVal tono As Integer, Optional ByVal NOOFCOPIES As Integer = 1, Optional ByVal FRMSTRING As String = "PRINT")
        Try

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            DTMAIL.Rows.Clear()
            DTWHATSAPP.Rows.Clear()
            Dim GARMENTCHALLAN As Boolean = False
            If (ClientName = "MANSI" Or ClientName = "CHINTAN") AndAlso MsgBox("Print Challan For Garments?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then GARMENTCHALLAN = True


            'Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To gridbill.RowCount - 1
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                If ROW("CHK") = True Then
                    '**************** SET SERVER ************************
                    Dim crParameterFieldDefinitions As ParameterFieldDefinitions
                    Dim crParameterFieldDefinition As ParameterFieldDefinition
                    Dim crParameterValues As New ParameterValues
                    Dim crParameterDiscreteValue As New ParameterDiscreteValue

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

                    Dim expo As New ExportOptions
                    Dim oDfDopt As New DiskFileDestinationOptions


                    Dim OBJ As New Object
                    If ClientName = "MAFATLAL" Then
                        OBJ = New GDNReport_A5
                    ElseIf ClientName = "BALAJI" Or ClientName = "NAYRA" Then
                        OBJ = New GDNReport_BALAJI
                        If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                        If CHKHIDEPCS.Checked = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                        OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                        OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    ElseIf ClientName = "SOFTAS" Then
                        OBJ = New GDNReport_SOFTAS
                    ElseIf ClientName = "BRILLANTO" Then
                        OBJ = New GDNReport_BRILLANTO
                    ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                        OBJ = New GDNReport_CC
                    ElseIf ClientName = "DRDRAPES" Then
                        OBJ = New GDNReport_DRDRAPES
                    ElseIf ClientName = "KCRAYON" Then
                        OBJ = New GDNReport_KCRAYON
                    ElseIf ClientName = "KDFAB" Then
                        OBJ = New GDNReport_KDFAB
                    ElseIf ClientName = "AKASHDEEP" Then
                        OBJ = New GDNReport_AKASHDEEP
                    ElseIf ClientName = "MANINATH" Then
                        OBJ = New GDNReport_MANINATH
                    ElseIf ClientName = "MOMAI" Then
                        OBJ = New GDNReport_MOMAI
                    ElseIf ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                        OBJ = New GDNReport_SANGHVI
                    ElseIf ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                        OBJ = New GDNReport_SAFFRON
                    ElseIf ClientName = "SBA" Then
                        OBJ = New GDNReport_SBA
                    ElseIf ClientName = "SUPEEMA" Then
                        OBJ = New GDNReport_SUPEEMA
                    ElseIf ClientName = "SKF" Then
                        OBJ = New GDNReport_SKF
                    ElseIf ClientName = "SVS" Then
                        OBJ = New GDNReport_SVS
                    ElseIf ClientName = "AVIS" Then
                        OBJ = New GDNReport_AVIS
                    ElseIf ClientName = "SHREENAKODA" Then
                        OBJ = New GDNReport_SHREENAKODA
                    ElseIf ClientName = "SNCM" Then
                        OBJ = New GDNReport_SNCM
                    ElseIf ClientName = "REALCORPORATION" Then
                        OBJ = New GDNReport_REALCORP
                    ElseIf ClientName = "NTC" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "KUNAL" Or ClientName = "SURYODAYA" Or ClientName = "SSC" Or ClientName = "VALIANT" Then
                        OBJ = New GDNReport_NTC
                    ElseIf ClientName = "PARAS" Or ClientName = "MARKIN" Then
                        OBJ = New GDNReport_PARASMARKIN
                        If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                    Else

                        If GARMENTCHALLAN = True Then
                            OBJ = New GDNReport_GARMENT
                            If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                            If CHKHIDEPCS.Checked = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                            If FRMSTRING <> "PRINT" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                            If ClientName = "ALENCOT" Or ClientName = "MANSI" Or ClientName = "CHINTAN" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                            OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

                        Else
                            OBJ = New GDNReport_COMMON
                            If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                            If CHKHIDEPCS.Checked = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                            If ClientName = "MYCOT" Or ClientName = "SHUBHI" Then OBJ.DataDefinition.FormulaFields("PRINTRATE").Text = 1 Else OBJ.DataDefinition.FormulaFields("PRINTRATE").Text = 0
                            If FRMSTRING <> "PRINT" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                            If ClientName = "ALENCOT" Or ClientName = "MANSI" Or ClientName = "CHINTAN" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                            OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                            If ClientName = "SUPRIYA" AndAlso MsgBox("Print Images?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJ.DataDefinition.FormulaFields("SHOWIMAGE").Text = "1"
                        End If
                    End If

                    crTables = OBJ.Database.Tables
                    For Each crTable In crTables
                        crtableLogonInfo = crTable.LogOnInfo
                        crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                        crTable.ApplyLogOnInfo(crtableLogonInfo)
                    Next

                    OBJ.RecordSelectionFormula = "{GDN.GDN_no}=" & Val(ROW("SRNO")) & " and {GDN.GDN_yearid}=" & YearId

                    If FRMSTRING = "PRINT" Then
                        OBJ.PrintOptions.PrinterName = PRINTDIALOG.PrinterSettings.PrinterName
                        If ClientName <> "AVIS" Then OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize Else OBJ.PrintOptions.PaperSize = PaperSize.PaperA5
                        OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                    Else
                        oDfDopt.DiskFileName = Application.StartupPath & "\" & ROW("CMPNAME") & "GDN_" & ROW("SRNO") & ".pdf"
                        expo = OBJ.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        OBJ.Export()
                        ALATTACHMENT.Add(oDfDopt.DiskFileName)
                        FILENAME.Add(ROW("CMPNAME") & "GDN_" & ROW("SRNO") & ".pdf")

                        'ADDINT IN DTEMAIL
                        DTMAIL.Rows.Add(ROW("SRNO"), 0, "", ROW("SRNO"), ROW("DATE"), ROW("CMPNAME"), ROW("PARTYEMAIL"), ROW("AGENT"), ROW("AGENTEMAIL"), 0, UCase(CmpName) & " - Challan No. " & ROW("SRNO") & " Dated " & ROW("DATE"), oDfDopt.DiskFileName, ROW("CMPNAME") & "GDN_" & ROW("SRNO") & ".pdf")

                        'ADDING IN DTWHATSAPP
                        DTWHATSAPP.Rows.Add(ROW("SRNO"), 0, "", ROW("SRNO"), ROW("DATE"), ROW("CMPNAME"), ROW("PARTYWHATSAPP"), ROW("AGENT"), ROW("AGENTWHATSAPP"), 0, UCase(CmpName) & " - Challan No. " & ROW("SRNO") & " Dated " & ROW("DATE"), oDfDopt.DiskFileName, ROW("CMPNAME") & "GDN_" & ROW("SRNO") & ".pdf")


                    End If
                    OBJ.CLOSE()
                    OBJ.DISPOSE()
                End If
            Next

            If FRMSTRING = "MAIL" Then
                If DTMAIL.Rows.Count = 0 Then Exit Sub
                Dim OBJEMAIL As New SendMultipleMail
                OBJEMAIL.FORMTYPE = "CHALLAN"
                OBJEMAIL.DT = DTMAIL
                OBJEMAIL.ShowDialog()
                Exit Sub

                'Dim OBJMAIL As New SendMail
                'OBJMAIL.ALATTACHMENT = ALATTACHMENT
                'OBJMAIL.subject = "Challan"
                'OBJMAIL.ShowDialog()
            End If

            If FRMSTRING = "WHATSAPP" = True Then
                If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
                Dim OBJWHATSAPP As New SendMultipleWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.DT = DTWHATSAPP
                OBJWHATSAPP.ShowDialog()
                'Dim OBJWHATSAPP As New SendWhatsapp
                'OBJWHATSAPP.PATH = ALATTACHMENT
                'OBJWHATSAPP.FILENAME = FILENAME
                'OBJWHATSAPP.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Challan Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Challan Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Challan Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Challan Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub GDNDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                CMBTYPE.Visible = True
                LBLTYPE.Visible = True
                GTYPECHALLANNO.Visible = True
                GTYPECHALLANNO.VisibleIndex = 0
                gsrno.Visible = False
                GCHALLANNO.Caption = "Ref No."
                CMBTYPE.Focus()
            End If

            If ClientName = "DJIMPEX" Then
                GCHALLANNO.Caption = "Gross Wt"
                GBALENOFROM.Caption = "Container No"
            End If

            If ClientName = "SONU" Then
                GCHALLANNO.Caption = "Bale Type"
            End If

            If ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "VINAYAK" Then
                GCITYNAME.Visible = True
                GCITYNAME.Width = 120
                GCITYNAME.VisibleIndex = GPACKING.VisibleIndex + 1
                GMULTISONO.Visible = True
                GSONO.Visible = False
                GMULTISONO.VisibleIndex = GCITYNAME.VisibleIndex + 1
            End If

            If ClientName = "RADHA" Then GCHALLANNO.Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDETAILS.Click
        Try
            Dim OBJDTLS As New GDNGridDetails
            OBJDTLS.MdiParent = MDIMain
            OBJDTLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Challan Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Challan from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "WHATSAPP")
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Challan ?", MsgBoxStyle.YesNo) = vbYes Then
                    cmdok.Focus()
                    SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "WHATSAPP")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid(" AND CHALLANTYPE_NAME = '" & CMBTYPE.Text.Trim & "' and dbo.gdn.gdn_yearid=" & YearId & " ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTYPE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTYPE.Validated
        Try
            If CMBTYPE.Text <> "" And (ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF") Then fillgrid(" AND CHALLANTYPE_NAME = '" & CMBTYPE.Text.Trim & "' and dbo.gdn.gdn_yearid=" & YearId & " ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLMAIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLMAIL.Click
        Try
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Challan Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail Challan from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "MAIL")
                End If
            Else
                If MsgBox("Wish to Mail Selected Challan ?", MsgBoxStyle.YesNo) = vbYes Then
                    cmdok.Focus()
                    SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "MAIL")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("HOLDFORAPPROVAL")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class