
Imports BL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Views.Grid

Public Class SalesEnquiryDetails
    Public EDIT As Boolean
    Dim temppreqno As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public TEMPSONO As String
    Dim PARTYSOREPORT As Boolean = False
    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SalesEnquiryDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Private Sub SalesEnquiryDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE ORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid(" and dbo.SALESENQUIRY.ENQ_yearid=" & YearId & " order by dbo.SALESENQUIRY.ENQ_no, SALESENQUIRY_DESC.ENQ_GRIDSRNO ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, ISNULL(SALESENQUIRY.ENQ_no, 0) AS ENQNO, SALESENQUIRY.ENQ_date AS DATE, ISNULL(SALESENQUIRY_DESC.ENQ_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(SALESENQUIRY_DESC.ENQ_QTY, 0) AS QTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(SALESENQUIRY_DESC.ENQ_CUT, 0) AS CUT, ISNULL(SALESENQUIRY_DESC.ENQ_MTRS, 0) AS MTRS, ISNULL(SALESENQUIRY_DESC.ENQ_RATE, 0) AS RATE, ISNULL(SALESENQUIRY_DESC.ENQ_AMOUNT, 0) AS AMOUNT, ISNULL(SALESENQUIRY_DESC.ENQ_CLOSED, 0) AS CLOSED, SALESENQUIRY_DESC.ENQ_CLOSEDDATE AS CLOSEDDATE, ISNULL(SALESENQUIRY_DESC.ENQ_CLOSEDREASON, '') AS REASON, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(TRANSPORTER.Acc_cmpname, '') AS TRANSPORTER, ISNULL(SALESENQUIRY.ENQ_TOTALQTY, 0) AS TOTALQTY, ISNULL(SALESENQUIRY_DESC.ENQ_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(SALESENQUIRY.ENQ_remarks, '') AS REMARKS ", "", "  SALESENQUIRY INNER JOIN SALESENQUIRY_DESC ON SALESENQUIRY.ENQ_no = SALESENQUIRY_DESC.ENQ_NO AND SALESENQUIRY.ENQ_YEARID = SALESENQUIRY_DESC.ENQ_YEARID LEFT OUTER JOIN LEDGERS AS TRANSPORTER ON SALESENQUIRY.ENQ_transid = TRANSPORTER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON SALESENQUIRY.ENQ_Agentid = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON SALESENQUIRY.ENQ_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN UNITMASTER ON SALESENQUIRY_DESC.ENQ_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN COLORMASTER ON SALESENQUIRY_DESC.ENQ_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON SALESENQUIRY_DESC.ENQ_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON SALESENQUIRY_DESC.ENQ_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON SALESENQUIRY_DESC.ENQ_ITEMID = ITEMMASTER.item_id ", TEMPCONDITION)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub showform(ByVal editval As Boolean, ByVal ENQNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New SalesEnquiry
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                'objPO.TEMPENQNO = ENQNO
                objPO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
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

    Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("ENQNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(sender As Object, e As EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Sale Enquiry Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Sale Enquiry Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sale Enquiry Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Sale Enquiry Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub
    'Private Sub TOOLMAIL_Click(sender As Object, e As EventArgs) Handles TOOLMAIL.Click
    '    Try
    '        If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
    '            If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
    '                MsgBox("Enter Proper Sale Enquiry Nos", MsgBoxStyle.Critical)
    '                Exit Sub
    '            Else
    '                If MsgBox("Wish to Mail Sale Enquiry from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
    '                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "MAIL")
    '            End If
    '        Else
    '            If MsgBox("Wish to Mail Selected Sale Enquiry ?", MsgBoxStyle.YesNo) = vbYes Then
    '                cmdok.Focus()
    '                SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "MAIL")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SONO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("CLOSED")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightSkyBlue
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDETAILS.Click
        Try
            Dim OBJPROD As New SalesEnquiryGridDetails
            OBJPROD.MdiParent = MDIMain
            OBJPROD.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    'Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
    '    Try
    '        Dim DT As New DataTable
    '        Dim OBJCMN As New ClsCommon


    '        If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
    '            If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
    '                MsgBox("Enter Proper Sale Enquiry Nos", MsgBoxStyle.Critical)
    '                Exit Sub
    '            Else
    '                If MsgBox("Wish to Whatsapp Sale Enquiry from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
    '                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "WHATSAPP")
    '            End If
    '        Else
    '            If MsgBox("Wish to Whatsapp Selected Sale Enquiry ?", MsgBoxStyle.YesNo) = vbYes Then
    '                cmdok.Focus()
    '                SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "WHATSAPP")
    '            End If
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
    '    Try
    '        If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
    '            If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
    '                MsgBox("Enter Proper Sale Enquiry Nos", MsgBoxStyle.Critical)
    '                Exit Sub
    '            End If
    '            If MsgBox("Wish to Print Sale Enquiry from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
    '                If PrintDialog.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PrintDialog.PrinterSettings
    '                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim))
    '            End If
    '        Else
    '            If MsgBox("Wish to Print Selected Sale Enquiry ?", MsgBoxStyle.YesNo) = vbYes Then
    '                If PrintDialog.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PrintDialog.PrinterSettings
    '                cmdok.Focus()
    '                SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid(" and dbo.SALESENQUIRY.ENQ_yearid=" & YearId & " order by dbo.SALESENQUIRY.ENQ_no, SALESENQUIRY_DESC.ENQ_GRIDSRNO ")
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


                'UPDATING SALE ORDER FOR WHATSAPP DONE

                '   If FRMSTRING = "WHATSAPP" Then DT = OBJCMN.Execute_Any_String("UPDATE SALEORDER SET SO_SENDWHATSAPP = 1 WHERE SO_NO = " & I & " AND SO_YEARID = " & YearId, "", "")



                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions

                Dim OBJ As New Object



                'If ClientName = "AVIS" Or ClientName = "SUPRIYA" Or ClientName = "SHREEVALLABH" Or ClientName = "RAJDEEP" Then
                '    OBJ = New SOReport_AVISSUP
                'ElseIf ClientName = "SAFFRON" Then
                '    OBJ = New SOReport_SAFFRON
                'ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                '    OBJ = New SOReport_CC
                'ElseIf ClientName = "MAHAVIR" Then
                '    OBJ = New SOReport_MAHAVIR
                'ElseIf ClientName = "YASHVI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "KRISHNA" Or ClientName = "KRFABRICS" Or ClientName = "SHREENAKODA" Then
                '    OBJ = New SOReport_YASHVIYUMILONE
                'ElseIf ClientName = "SBA" Then
                '    OBJ = New SOReport_SBA
                'ElseIf ClientName = "KENCOT" Then
                '    OBJ = New SOReport_KENCOT
                'ElseIf ClientName = "MAHAVIRPOLYCOT" And PARTYSOREPORT = True Then
                '    OBJ = New SOReport_MAHAVIRPOLYCOT
                'Else
                '    OBJ = New SOReport
                'End If

                crTables = OBJ.Database.Tables
                For Each crTable In crTables
                    crtableLogonInfo = crTable.LogOnInfo
                    crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                    crTable.ApplyLogOnInfo(crtableLogonInfo)
                Next

                OBJ.RecordSelectionFormula = "{SALESENQUIRY.ENQ_no}=" & Val(I) & " and {SALESENQUIRY.ENQ_yearid}=" & YearId

                If FRMSTRING = "PRINT" Then
                    OBJ.PrintOptions.PrinterName = PRINTDIALOG.PrinterSettings.PrinterName
                    OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                    OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                Else
                    oDfDopt.DiskFileName = Application.StartupPath & "\ENQ_" & I & ".pdf"
                    expo = OBJ.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    OBJ.Export()
                    ALATTACHMENT.Add(oDfDopt.DiskFileName)
                    FILENAME.Add("ENQ_" & I & ".pdf")
                End If
            Next

            If FRMSTRING = "MAIL" Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Sale Order"
                OBJMAIL.ShowDialog()
            End If

            If FRMSTRING = "WHATSAPP" Then
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


                    'If ClientName = "AVIS" Or ClientName = "SUPRIYA" Or ClientName = "SHREEVALLABH" Or ClientName = "RAJDEEP" Then
                    '    OBJ = New SOReport_AVISSUP
                    'ElseIf ClientName = "SAFFRON" Then
                    '    OBJ = New SOReport_SAFFRON
                    'ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    '    OBJ = New SOReport_CC
                    'ElseIf ClientName = "MAHAVIR" Then
                    '    OBJ = New SOReport_MAHAVIR
                    'ElseIf ClientName = "YASHVI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "KRISHNA" Or ClientName = "KRFABRICS" Or ClientName = "SHREENAKODA" Then
                    '    OBJ = New SOReport_YASHVIYUMILONE
                    'ElseIf ClientName = "SBA" Then
                    '    OBJ = New SOReport_SBA
                    'ElseIf ClientName = "KENCOT" Then
                    '    OBJ = New SOReport_KENCOT
                    'ElseIf ClientName = "MAHAVIRPOLYCOT" And PARTYSOREPORT = True Then
                    '    OBJ = New SOReport_MAHAVIRPOLYCOT
                    'Else
                    '    OBJ = New SOReport
                    'End If


                    crTables = OBJ.Database.Tables
                    For Each crTable In crTables
                        crtableLogonInfo = crTable.LogOnInfo
                        crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                        crTable.ApplyLogOnInfo(crtableLogonInfo)
                    Next

                    OBJ.RecordSelectionFormula = "{SALESENQUIRY.ENQ_no}=" & Val(ROW("ENQNO")) & " and {SALESENQUIRY.ENQ_yearid}=" & YearId

                    If FRMSTRING = "PRINT" Then
                        OBJ.PrintOptions.PrinterName = PrintDialog.PrinterSettings.PrinterName
                        OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                        OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                    Else
                        oDfDopt.DiskFileName = Application.StartupPath & "\" & ROW("NAME") & "ENQ_" & ROW("ENQNO") & ".pdf"
                        expo = OBJ.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        OBJ.Export()
                        ALATTACHMENT.Add(oDfDopt.DiskFileName)
                        FILENAME.Add(ROW("NAME") & "ENQ_" & ROW("ENQNO") & ".pdf")
                    End If

                End If
            Next

            If FRMSTRING = "MAIL" Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Sale Order"
                OBJMAIL.ShowDialog()
            End If

            If FRMSTRING = "WHATSAPP" Then
                Dim OBJWHATSAPP As New SendWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class