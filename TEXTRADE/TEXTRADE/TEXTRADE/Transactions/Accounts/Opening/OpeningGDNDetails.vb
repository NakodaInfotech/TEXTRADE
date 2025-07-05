Imports BL
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.XtraGrid.Views.Grid


Public Class OpeningGDNDetails

    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

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
            FILLCHALLANTYPE(CMBTYPE)
            If ClientName <> "SAFFRON" And ClientName <> "SAFFRONOFF" Then fillgrid(" and dbo.OPENINGgdn.OPENINGgdn_yearid=" & YearId & " ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, OPENINGGDN.OPENINGGDN_TYPENO AS TYPECHALLANNO, OPENINGGDN.OPENINGGDN_NO AS SRNO, ISNULL(OPENINGGDN.OPENINGGDN_TRANSREFNO, '') AS CHNO, OPENINGGDN.OPENINGGDN_date AS DATE, LEDGERS.Acc_cmpname AS CMPNAME, ISNULL(JOBBERLEDGERS.Acc_cmpname,'') AS JOBBERNAME, ISNULL(OPENINGGDN.OPENINGGDN_CONSIGNEE, '') AS CONSIGNEE, ISNULL(AGENTLEDGERS.Acc_cmpname,'') AS AGENT, OPENINGGDN.OPENINGGDN_SONO AS SONO, OPENINGGDN.OPENINGGDN_MULTISONO AS MULTISONO, OPENINGGDN.OPENINGGDN_SODATE AS SODATE, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, SUM(OPENINGGDN_DESC.OPENINGGDN_PCS) AS PCS, SUM(OPENINGGDN_DESC.OPENINGGDN_MTRS) AS MTRS, OPENINGGDN.OPENINGGDN_TOTALBALES AS TOTALBALES, OPENINGGDN.OPENINGGDN_BALENOFROM AS BALENOFROM, OPENINGGDN_DESC.OPENINGGDN_BALENO AS BALENO, ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS PACKING, ISNULL(CATEGORYMASTER.category_name, '') AS CATEGORY, ISNULL(OPENINGGDN.OPENINGGDN_TRANSREMARKS,'') AS PARTYPONO, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO,'') AS GRIDPARTYPONO, ISNULL(OPENINGGDN.OPENINGGDN_HOLDFORAPPROVAL,0) AS HOLDFORAPPROVAL, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME  ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id INNER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID INNER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON OPENINGGDN_DESC.OPENINGGDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS JOBBERLEDGERS ON OPENINGGDN.OPENINGGDN_JOBBERID = JOBBERLEDGERS.Acc_id LEFT OUTER JOIN CHALLANTYPEMASTER ON CHALLANTYPEMASTER.CHALLANTYPE_ID = OPENINGGDN.OPENINGGDN_TYPEID LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON OPENINGGDN.OPENINGGDN_DISPATCHTOID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYID = CITYMASTER.CITY_ID", TEMPCONDITION & " GROUP BY OPENINGGDN.OPENINGGDN_NO, ISNULL(OPENINGGDN.OPENINGGDN_TRANSREFNO, ''), OPENINGGDN.OPENINGGDN_date, LEDGERS.Acc_cmpname, ISNULL(OPENINGGDN.OPENINGGDN_CONSIGNEE, ''), OPENINGGDN.OPENINGGDN_SONO, OPENINGGDN.OPENINGGDN_SODATE, ITEMMASTER.item_name,  ISNULL(DESIGNMASTER.DESIGN_NO, ''), OPENINGGDN.OPENINGGDN_TOTALBALES, OPENINGGDN_DESC.OPENINGGDN_BALENO, OPENINGGDN.OPENINGGDN_TYPENO, ISNULL(PACKINGLEDGERS.Acc_cmpname, ''), ISNULL(CATEGORYMASTER.category_name, ''),  ISNULL(OPENINGGDN.OPENINGGDN_TRANSREMARKS, ''), ISNULL(OPENINGGDN.OPENINGGDN_HOLDFORAPPROVAL, 0), OPENINGGDN.OPENINGGDN_BALENOFROM, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, ''), ISNULL(COLORMASTER.COLOR_name, ''), ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(JOBBERLEDGERS.Acc_cmpname, ''), ISNULL(AGENTLEDGERS.Acc_cmpname, ''), ISNULL(CITYMASTER.CITY_NAME,''), OPENINGGDN.OPENINGGDN_MULTISONO ORDER BY OPENINGGDN.OPENINGGDN_NO")
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
                Dim objgdn As New OpeningGDN
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


            ''IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            'If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
            '    If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
            '        MsgBox("Enter Proper Challan Nos", MsgBoxStyle.Critical)
            '        Exit Sub
            '    End If
            '    If MsgBox("Wish to Print Challan from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
            '        If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings
            '        serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim))
            '    End If
            'Else
            '    If MsgBox("Wish to Print Selected Challan ?", MsgBoxStyle.YesNo) = vbYes Then
            '        If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings
            '        cmdok.Focus()
            '        SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim))
            '    End If
            'End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub serverprop(ByVal fromno As Integer, ByVal tono As Integer, Optional ByVal NOOFCOPIES As Integer = 1, Optional ByVal FRMSTRING As String = "PRINT")
        Try
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList

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
                ElseIf ClientName = "SKF" Then
                    OBJ = New GDNReport_SKF
                ElseIf ClientName = "SVS" Then
                    OBJ = New GDNReport_SVS
                ElseIf ClientName = "AVIS" Then
                    OBJ = New GDNReport_AVIS
                ElseIf ClientName = "NTC" Then
                    OBJ = New GDNReport_NTC
                ElseIf ClientName = "PARAS" Or ClientName = "MARKIN" Then
                    OBJ = New GDNReport_PARASMARKIN
                    If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                Else
                    OBJ = New GDNReport_COMMON
                    If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                    If CHKHIDEPCS.Checked = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                    If FRMSTRING <> "PRINT" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    If ClientName = "ALENCOT" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                End If

                crTables = OBJ.Database.Tables
                For Each crTable In crTables
                    crtableLogonInfo = crTable.LogOnInfo
                    crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                    crTable.ApplyLogOnInfo(crtableLogonInfo)
                Next

                OBJ.RecordSelectionFormula = "{OPENINGGDN.OPENINGGDN_no}=" & Val(I) & " and {OPENINGGDN.OPENINGGDN_yearid}=" & YearId

                If FRMSTRING = "PRINT" Then
                    OBJ.PrintOptions.PrinterName = PRINTDIALOG.PrinterSettings.PrinterName
                    If ClientName <> "AVIS" Then OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize Else OBJ.PrintOptions.PaperSize = PaperSize.PaperA5
                    OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                Else
                    oDfDopt.DiskFileName = Application.StartupPath & "\OPENINGGDN_" & I & ".pdf"
                    expo = OBJ.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    OBJ.Export()
                    ALATTACHMENT.Add(oDfDopt.DiskFileName)
                    FILENAME.Add("GDN_" & I & ".pdf")
                End If
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
                    ElseIf ClientName = "SKF" Then
                        OBJ = New GDNReport_SKF
                    ElseIf ClientName = "SVS" Then
                        OBJ = New GDNReport_SVS
                    ElseIf ClientName = "AVIS" Then
                        OBJ = New GDNReport_AVIS
                    ElseIf ClientName = "NTC" Then
                        OBJ = New GDNReport_NTC
                    ElseIf ClientName = "PARAS" Or ClientName = "MARKIN" Then
                        OBJ = New GDNReport_PARASMARKIN
                        If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                    Else
                        OBJ = New GDNReport_COMMON
                        If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                        If CHKHIDEPCS.Checked = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                        If FRMSTRING <> "PRINT" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                        If ClientName = "ALENCOT" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    End If

                    crTables = OBJ.Database.Tables
                    For Each crTable In crTables
                        crtableLogonInfo = crTable.LogOnInfo
                        crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                        crTable.ApplyLogOnInfo(crtableLogonInfo)
                    Next

                    OBJ.RecordSelectionFormula = "{OPENINGGDN.OPENINGGDN_no}=" & Val(ROW("SRNO")) & " and {OPENINGGDN.OPENINGGDN_yearid}=" & YearId

                    If FRMSTRING = "PRINT" Then
                        OBJ.PrintOptions.PrinterName = PRINTDIALOG.PrinterSettings.PrinterName
                        If ClientName <> "AVIS" Then OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize Else OBJ.PrintOptions.PaperSize = PaperSize.PaperA5
                        OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                    Else
                        oDfDopt.DiskFileName = Application.StartupPath & "\OPENINGGDN_" & ROW("SRNO") & ".pdf"
                        expo = OBJ.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        OBJ.Export()
                        ALATTACHMENT.Add(oDfDopt.DiskFileName)
                        FILENAME.Add("GDN_" & ROW("SRNO") & ".pdf")
                    End If

                End If
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

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Opening Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Opening Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Opening Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Opening Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
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

            If ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Then
                GCITYNAME.Visible = True
                GCITYNAME.Width = 120
                GCITYNAME.VisibleIndex = GPACKING.VisibleIndex + 1
                GMULTISONO.Visible = True
                GSONO.Visible = False
                GMULTISONO.VisibleIndex = GCITYNAME.VisibleIndex + 1
            End If

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