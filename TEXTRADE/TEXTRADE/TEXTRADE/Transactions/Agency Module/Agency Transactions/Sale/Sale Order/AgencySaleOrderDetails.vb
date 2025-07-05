
Imports BL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Views.Grid

Public Class AgencySaleOrderDetails

    Public EDIT As Boolean
    Dim temppreqno As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public TEMPSONO As String
    Dim PARTYSOREPORT As Boolean = False

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub AgencySaleOrderDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub AgencySaleOrderDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            FILLGRID(" and dbo.AGENCYSALEORDER.ASO_yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            If CHKPENDING.Checked = True And ClientName = "AVIS" Then TEMPCONDITION = TEMPCONDITION & " AND ISNULL(AGENCYSALEORDER_DESC.ASO_CLOSED,0) = 'FALSE' AND (ROUND(((ISNULL(AGENCYSALEORDER_DESC.ASO_MTRS,0) - ISNULL(AGENCYSALEORDER_DESC.ASO_RECDMTRS,0)) / ISNULL(AGENCYSALEORDER_DESC.ASO_MTRS,0))*100,2)) >= 90"
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, AGENCYSALEORDER.ASO_no AS SONO, AGENCYSALEORDER.ASO_date AS SODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, AGENCYSALEORDER.ASO_pono AS PARTYPONO, AGENCYSALEORDER.ASO_DUEDATE AS DELDATE, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,   ISNULL(AGENCYSALEORDER_DESC.ASO_PARTYPONO, '') AS GRIDPARTYPONO, ISNULL(AGENCYSALEORDER_DESC.ASO_GRIDREMARKS, '') AS GRIDREMARKS, AGENCYSALEORDER_DESC.ASO_QTY AS PCS, AGENCYSALEORDER_DESC.ASO_CUT AS CUT,  AGENCYSALEORDER_DESC.ASO_MTRS AS MTRS, AGENCYSALEORDER_DESC.ASO_RATE AS RATE, AGENCYSALEORDER_DESC.ASO_QTY - AGENCYSALEORDER_DESC.ASO_RECDQTY AS BALPCS,  AGENCYSALEORDER_DESC.ASO_MTRS - ISNULL(AGENCYSALEORDER_DESC.ASO_RECDMTRS, 0) AS BALMTRS, AGENCYSALEORDER.ASO_remarks AS REMARKS, ISNULL(AGENCYSALEORDER_DESC.ASO_CLOSED, 0) AS CLOSED,  ISNULL(AGENCYSALEORDER.ASO_FORWARD, '') AS FORWARD, ISNULL(AGENCYSALEORDER_DESC.ASO_CLOSEDDATE, GETDATE()) AS CLOSEDDATE, ISNULL(AGENCYSALEORDER_DESC.ASO_CLOSEDREASON, '') AS REASON,  AGENCYSALEORDER_DESC.ASO_AMOUNT AS AMOUNT, ISNULL(USERMASTER.User_Name, '') AS USERNAME, ISNULL(AGENCYSALEORDER.ASO_REFNO, '') AS REFNO, ISNULL(LEDGERS.ACC_WHATSAPPNO, '0') AS PARTYWHATSAAP,   ISNULL(LEDGERS.Acc_email, '') AS PARTYEMAIL, ISNULL(AGENTLEDGERS.Acc_email, '') AS AGENTEMAIL, ISNULL(AGENTLEDGERS.ACC_WHATSAPPNO, '0') AS AGENTWHATSAAP, ISNULL(AGENCYSALEORDER_DESC.ASO_PER, '')   AS PER , ISNULL(SALESMANMASTER.SALESMAN_NAME,'') AS SALESMAN , ISNULL(SHIPTOLEDGERS.Acc_cmpname,'') AS SHIPTO , ISNULL(HASTELEDGERS.Acc_cmpname,'') AS HASTE, COALESCE(PARENTDESIGNMASTER.DESIGN_NO, DESIGNMASTER.DESIGN_NO) AS PARENTDESIGNNO ", "", "AGENCYSALEORDER_DESC INNER JOIN AGENCYSALEORDER ON AGENCYSALEORDER_DESC.ASO_NO = AGENCYSALEORDER.ASO_no AND AGENCYSALEORDER_DESC.ASO_YEARID = AGENCYSALEORDER.ASO_YEARID INNER JOIN  LEDGERS ON AGENCYSALEORDER.ASO_ledgerid = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON AGENCYSALEORDER_DESC.ASO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS SHIPTOLEDGERS ON AGENCYSALEORDER.ASO_PACKINGID = SHIPTOLEDGERS.Acc_id AND AGENCYSALEORDER.ASO_YEARID = SHIPTOLEDGERS.Acc_yearid LEFT OUTER JOIN LEDGERS AS HASTELEDGERS ON AGENCYSALEORDER.ASO_HASTEID = HASTELEDGERS.Acc_id AND AGENCYSALEORDER.ASO_YEARID = HASTELEDGERS.Acc_yearid LEFT OUTER JOIN SALESMANMASTER ON AGENCYSALEORDER.ASO_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN COLORMASTER ON AGENCYSALEORDER_DESC.ASO_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON AGENCYSALEORDER_DESC.ASO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN DESIGNMASTER AS PARENTDESIGNMASTER ON DESIGNMASTER.DESIGN_PARENTDESIGNID = PARENTDESIGNMASTER.DESIGN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON AGENCYSALEORDER.ASO_Agentid = AGENTLEDGERS.Acc_id LEFT OUTER JOIN USERMASTER ON AGENCYSALEORDER.ASO_USERID = USERMASTER.User_id  ", TEMPCONDITION & " order by dbo.AGENCYSALEORDER.ASO_no, AGENCYSALEORDER_DESC.ASO_GRIDSRNO")
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
            gridbilldetails.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal SOno As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New AgencySaleOrder
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPSONO = SOno
                objPO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            showform(True, gridbill.GetFocusedRowCellValue("SONO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(sender As Object, e As EventArgs)
        Try
            Dim PATH As String = Application.StartupPath & "\Sale Order Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Sale Order Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sale Order Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Sale Order Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TOOLMAIL_Click(sender As Object, e As EventArgs)
        Try
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Sale Order Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail Sale Order from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "MAIL")
                End If
            Else
                If MsgBox("Wish to Mail Selected Sale Orders ?", MsgBoxStyle.YesNo) = vbYes Then
                    cmdok.Focus()
                    SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "MAIL")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SONO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
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

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs)
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon


            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Sale Order Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Sale Order from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "WHATSAPP")
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Sale Orders ?", MsgBoxStyle.YesNo) = vbYes Then
                    cmdok.Focus()
                    SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "WHATSAPP")
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Sale Order Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Wish to Print Sale Order from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings
                    serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim))
                End If
            Else
                If MsgBox("Wish to Print Selected Sale Orders ?", MsgBoxStyle.YesNo) = vbYes Then
                    If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings
                    cmdok.Focus()
                    SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim))
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs)
        Try
            FILLGRID(" and dbo.AGENCYSALEORDER.ASO_yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
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



    'Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs)
    '    Try
    '        Dim OBJINV As New ProgramStatusDetails
    '        OBJINV.MdiParent = MDIMain
    '        OBJINV.Show()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Sub serverprop(ByVal fromno As Integer, ByVal tono As Integer, Optional ByVal NOOFCOPIES As Integer = 1, Optional ByVal FRMSTRING As String = "PRINT")
        Try
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            If ClientName = "MAHAVIRPOLYCOT" AndAlso MsgBox("Wish to Print Party SO Report?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then PARTYSOREPORT = True

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

                '   If FRMSTRING = "WHATSAPP" Then DT = OBJCMN.Execute_Any_String("UPDATE AGENCYSALEORDER SET ASO_SENDWHATSAPP = 1 WHERE ASO_NO = " & I & " AND ASO_YEARID = " & YearId, "", "")



                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions

                Dim OBJ As New Object



                If ClientName = "AVIS" Or ClientName = "SUPRIYA" Or ClientName = "SHREEVALLABH" Or ClientName = "RAJDEEP" Then
                    OBJ = New SOReport_AVISSUP
                ElseIf ClientName = "SAFFRON" Then
                    OBJ = New SOReport_SAFFRON
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    OBJ = New SOReport_CC
                ElseIf ClientName = "MAHAVIR" Then
                    OBJ = New SOReport_MAHAVIR
                ElseIf ClientName = "YASHVI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "KRISHNA" Or ClientName = "KRFABRICS" Or ClientName = "SHREENAKODA" Then
                    OBJ = New SOReport_YASHVIYUMILONE
                ElseIf ClientName = "SBA" Then
                    OBJ = New SOReport_SBA
                ElseIf ClientName = "KENCOT" Then
                    OBJ = New SOReport_KENCOT
                ElseIf ClientName = "MAHAVIRPOLYCOT" And PARTYSOREPORT = True Then
                    OBJ = New SOReport_MAHAVIRPOLYCOT
                Else
                    OBJ = New SOReport
                End If

                crTables = OBJ.Database.Tables
                For Each crTable In crTables
                    crtableLogonInfo = crTable.LogOnInfo
                    crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                    crTable.ApplyLogOnInfo(crtableLogonInfo)
                Next

                OBJ.RecordSelectionFormula = "{AGENCYSALEORDER.ASO_no}=" & Val(I) & " and {AGENCYSALEORDER.ASO_yearid}=" & YearId

                If FRMSTRING = "PRINT" Then
                    OBJ.PrintOptions.PrinterName = PrintDialog.PrinterSettings.PrinterName
                    OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                    OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                Else
                    oDfDopt.DiskFileName = Application.StartupPath & "\ASO_" & I & ".pdf"
                    expo = OBJ.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    OBJ.Export()
                    ALATTACHMENT.Add(oDfDopt.DiskFileName)
                    FILENAME.Add("ASO_" & I & ".pdf")
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
            If ClientName = "MAHAVIRPOLYCOT" AndAlso MsgBox("Wish to Print Party SO Report?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then PARTYSOREPORT = True

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


                    If ClientName = "AVIS" Or ClientName = "SUPRIYA" Or ClientName = "SHREEVALLABH" Or ClientName = "RAJDEEP" Then
                        OBJ = New SOReport_AVISSUP
                    ElseIf ClientName = "SAFFRON" Then
                        OBJ = New SOReport_SAFFRON
                    ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                        OBJ = New SOReport_CC
                    ElseIf ClientName = "MAHAVIR" Then
                        OBJ = New SOReport_MAHAVIR
                    ElseIf ClientName = "YASHVI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "KRISHNA" Or ClientName = "KRFABRICS" Or ClientName = "SHREENAKODA" Then
                        OBJ = New SOReport_YASHVIYUMILONE
                    ElseIf ClientName = "SBA" Then
                        OBJ = New SOReport_SBA
                    ElseIf ClientName = "KENCOT" Then
                        OBJ = New SOReport_KENCOT
                    ElseIf ClientName = "MAHAVIRPOLYCOT" And PARTYSOREPORT = True Then
                        OBJ = New SOReport_MAHAVIRPOLYCOT
                    Else
                        OBJ = New SOReport
                    End If


                    crTables = OBJ.Database.Tables
                    For Each crTable In crTables
                        crtableLogonInfo = crTable.LogOnInfo
                        crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                        crTable.ApplyLogOnInfo(crtableLogonInfo)
                    Next

                    OBJ.RecordSelectionFormula = "{AGENCYSALEORDER.ASO_no}=" & Val(ROW("SONO")) & " and {saleOrder.ASO_yearid}=" & YearId

                    If FRMSTRING = "PRINT" Then
                        OBJ.PrintOptions.PrinterName = PrintDialog.PrinterSettings.PrinterName
                        OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                        OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                    Else
                        oDfDopt.DiskFileName = Application.StartupPath & "\" & ROW("NAME") & "ASO_" & ROW("SONO") & ".pdf"
                        expo = OBJ.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        OBJ.Export()
                        ALATTACHMENT.Add(oDfDopt.DiskFileName)
                        FILENAME.Add(ROW("NAME") & "ASO_" & ROW("SONO") & ".pdf")
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

    Private Sub AgencySaleOrderDetails_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "INDRANI" Then
                GSONO.Caption = "Sr No."
                GREFNO.Caption = "SO No"
            End If
            If ClientName = "AVIS" Then CHKPENDING.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class

