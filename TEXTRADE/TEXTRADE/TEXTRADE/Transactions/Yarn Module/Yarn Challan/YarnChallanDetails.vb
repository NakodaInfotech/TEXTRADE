Imports BL
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.XtraGrid.Views.Grid


Public Class YarnChallanDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim DTMAIL As New DataTable
    Dim DTWHATSAPP As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub GRNDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub GRNDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GDN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid(" and dbo.YarnChallan.YARN_yearid=" & YearId & " order by dbo.YarnChallan.YARN_no ")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim OBJDEPT As New ClsYarnChallan
            OBJDEPT.alParaval.Add(0)
            OBJDEPT.alParaval.Add(CmpId)
            OBJDEPT.alParaval.Add(Locationid)
            OBJDEPT.alParaval.Add(YearId)
            Dim DT As DataTable = OBJDEPT.SELECTYARN(0, CmpId, Locationid, YearId)
            gridbilldetails.DataSource = DT
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal SRNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objGRN As New YarnChallan
                objGRN.MdiParent = MDIMain
                objGRN.EDIT = editval
                objGRN.TEMPYARNNO = SRNO
                objGRN.Show()
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

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid(" CAST(0 AS BIT) AS CHK, and dbo.YarnChallan.YARN_yearid=" & YearId & " order by dbo.YarnChallan.YARN_no ")
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
                    SERVERPROP(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "MAIL")
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


    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Challan Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Challan from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROP(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "WHATSAPP")
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

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("YARNNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("YARNNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_Click(sender As Object, e As EventArgs) Handles gridbilldetails.Click

    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Yarn Challan Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Yarn Challan Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Yarn Received Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Yarn Challan Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Sub SERVERPROP(ByVal fromno As Integer, ByVal tono As Integer, Optional ByVal NOOFCOPIES As Integer = 1, Optional ByVal FRMSTRING As String = "PRINT")
        Try
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList

            Dim GARMENTCHALLAN As Boolean = False
            If (ClientName = "MANSI" Or ClientName = "CHINTAN") AndAlso MsgBox("Print Challan for Garments?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then GARMENTCHALLAN = True

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
                ElseIf ClientName = "SHEETAL" Or ClientName = "MILUXE" Then
                    OBJ = New GDNReport_SHEETAL
                    OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    If CHKTHIRDPARTY.Checked = True Then OBJ.DataDefinition.FormulaFields("THIRDPARTY").Text = "1"
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    OBJ = New GDNReport_CC
                ElseIf ClientName = "DRDRAPES" Then
                    OBJ = New GDNReport_DRDRAPES
                ElseIf ClientName = "KCRAYON" Then
                    OBJ = New GDNReport_KCRAYON
                ElseIf ClientName = "KDFAB" Then
                    OBJ = New GDNReport_KDFAB
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
                ElseIf ClientName = "AARYA" Then
                    OBJ = New GDNReport_AARYA
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
                    ElseIf ClientName = "SHEETAL" Or ClientName = "MILUXE" Then
                        OBJ = New GDNReport_SHEETAL
                        OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                        OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                        OBJ = New GDNReport_CC
                    ElseIf ClientName = "DRDRAPES" Then
                        OBJ = New GDNReport_DRDRAPES
                    ElseIf ClientName = "KCRAYON" Then
                        OBJ = New GDNReport_KCRAYON
                    ElseIf ClientName = "KDFAB" Then
                        OBJ = New GDNReport_KDFAB
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
                    ElseIf ClientName = "AARYA" Then
                        OBJ = New GDNReport_AARYA
                    ElseIf ClientName = "NTC" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "KUNAL" Or ClientName = "SURYODAYA" Or ClientName = "SSC" Or ClientName = "VALIANT" Then
                        OBJ = New GDNReport_NTC
                    ElseIf ClientName = "PARAS" Or ClientName = "MARKIN" Then
                        OBJ = New GDNReport_PARASMARKIN
                        If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                    Else

                        If GARMENTCHALLAN = True Then
                            OBJ = New YarnChallanReport
                            If CHKWHITELABEL.Checked = True Then OBJ.DataDefinition.FormulaFields("WHITELABEL").Text = 1
                            If CHKHIDEPCS.Checked = True Then OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 1 Else OBJ.DataDefinition.FormulaFields("HIDEPCSDETAILS").Text = 0
                            If FRMSTRING <> "PRINT" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                            If ClientName = "ALENCOT" Or ClientName = "MANSI" Or ClientName = "CHINTAN" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                            OBJ.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"

                        Else
                            OBJ = New YarnChallanReport
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

                    OBJ.RecordSelectionFormula = "{YarnChallan.YARNNO}=" & Val(ROW("YARNNO")) & " and {YarnChallan_.yarn_yearid}=" & YearId

                    If FRMSTRING = "PRINT" Then
                        OBJ.PrintOptions.PrinterName = PRINTDIALOG.PrinterSettings.PrinterName
                        If ClientName <> "AVIS" Then OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize Else OBJ.PrintOptions.PaperSize = PaperSize.PaperA5
                        OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                    Else
                        oDfDopt.DiskFileName = Application.StartupPath & "\" & ROW("YARN_LEDGERID") & "YarnChallan_" & ROW("YARNNO") & ".pdf"
                        expo = OBJ.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        OBJ.Export()
                        ALATTACHMENT.Add(oDfDopt.DiskFileName)
                        FILENAME.Add(ROW("YARN_LEDGERID") & "GDN_" & ROW("YARNNO") & ".pdf")

                        'ADDINT IN DTEMAIL
                        DTMAIL.Rows.Add(ROW("YARNNO"), 0, "", ROW("YARNNO"), ROW("DATE"), ROW("YARN_LEDGERID"), ROW("YARN_GODOWNID"), ROW("YARN_REFNO"), ROW("YARN_SONO"), 0, UCase(CmpName) & " - Challan No. " & ROW("YARNNO") & " Dated " & ROW("DATE"), oDfDopt.DiskFileName, ROW("YARN_LEDGERID") & "YarnChallan_" & ROW("YARNNO") & ".pdf")

                        'ADDING IN DTWHATSAPP
                        DTWHATSAPP.Rows.Add(ROW("YARNNO"), 0, "", ROW("YARNNO"), ROW("DATE"), ROW("YARN_LEDGERID"), ROW("YARN_GODOWNID"), ROW("YARN_REFNO"), ROW("YARN_SONO"), 0, UCase(CmpName) & " - Challan No. " & ROW("YARNNO") & " Dated " & ROW("DATE"), oDfDopt.DiskFileName, ROW("YARN_LEDGERID") & "YarnChallan_" & ROW("YARNNO") & ".pdf")


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
End Class