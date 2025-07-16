
Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class PurchaseOrderDetails

    Public EDIT As Boolean
    Dim temppreqno As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub PurchaseOrderDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub PurchaseOrderDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'PURCHASE ORDER'")
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
            'CREATED A QUERY FOR CLIENT ABHEE TO SEE PENDING PUCHASE ORDER
            If ClientName = "ABHEE" And CHKPENDING.Checked = True Then
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK,PURCHASEORDER.PO_NO AS PONO, PURCHASEORDER.PO_DATE AS PODATE, PURCHASEORDER.PO_DUEDATE AS DUEDATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(PURCHASEORDER.PO_CRDAYS, 0) AS CRDAYS, ISNULL(PURCHASEORDER.PO_DELDAYS, 0) AS DELDAYS, ISNULL(PURCHASEORDER.PO_REFNO, '') AS REFNO, ISNULL(PURCHASEORDER.PO_DISCOUNT, 0) AS DISCOUNT, ISNULL(PURCHASEORDER.PO_QUOTNO, 0) AS QUOTNO, PURCHASEORDER.PO_QUOTDATE AS QUOTDATE, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(PURCHASEORDER.PO_REMARKS, '') AS REMARKS, ISNULL(PURCHASEORDER.PO_DONE, 0) AS PODONE, PURCHASEORDER_DESC.PO_GRIDSRNO AS POGRIDSRNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(PURCHASEORDER_DESC.PO_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(PURCHASEORDER_DESC.PO_REED, 0) AS REED, ISNULL(PURCHASEORDER_DESC.PO_PICK, 0) AS PICK, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLORNAME, ISNULL(PURCHASEORDER_DESC.PO_PDESNO, '') AS PDESNO, ISNULL(PURCHASEORDER_DESC.PO_PSHADE, '') AS PSHADE, ISNULL(PURCHASEORDER_DESC.PO_CUT, 0) AS CUT, ISNULL(PURCHASEORDER_DESC.PO_QTY, 0) AS POQTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(PURCHASEORDER_DESC.PO_MTRS, 0) AS MTRS, ISNULL(PURCHASEORDER_DESC.PO_RATE, 0) AS RATE, ISNULL(PURCHASEORDER_DESC.PO_MTRS, 0) * ISNULL(PURCHASEORDER_DESC.PO_RATE, 0) AS AMT, ISNULL(PURCHASEORDER_DESC.PO_QUOTNO, 0) AS GRIDQUOTNO, PURCHASEORDER_DESC.PO_QUOTGRIDSRNO AS QUOTGRIDSRNO, ISNULL(PURCHASEORDER_DESC.PO_RECDQTY, 0) AS RECDQTY, ISNULL(PURCHASEORDER_DESC.PO_RECDMTRS, 0) AS RECDMTRS, ISNULL(PURCHASEORDER_DESC.PO_DONE, 0) AS GRIDPODONE, ISNULL(PURCHASEORDER.PO_AMENDNO, '') AS AMDNO, ISNULL(PURCHASEORDER.PO_AMENDDONE, 0) AS AMDDONE, ISNULL(PURCHASEORDER.PO_DISPER, 0) AS PO_DISPER, ISNULL(PURCHASEORDER.PO_DISAMT, 0) AS PO_DISAMT, ISNULL(PURCHASEORDER.PO_PFPER, 0) AS PO_PFPER, ISNULL(PURCHASEORDER.PO_PFAMT, 0) AS PO_PFAMT, ISNULL(PURCHASEORDER.PO_TESTCHGS, 0) AS PO_TESTCHGS, ISNULL(PURCHASEORDER.PO_NETT, 0) AS PO_NETT, ISNULL(PURCHASEORDER.PO_SUBTOTAL, 0) AS PO_SUBTOTAL, '' AS TAXNAME, ISNULL(PURCHASEORDER.PO_TAXAMT, 0) AS PO_TAXAMT, '' AS ADDTAXNAME, ISNULL(PURCHASEORDER.PO_ADDTAXAMT, 0) AS PO_ADDTAXAMT, ISNULL(PURCHASEORDER.PO_FRPER, 0) AS PO_FRPER, ISNULL(PURCHASEORDER.PO_FREIGHT, 0) AS PO_FREIGHT, '' AS OCTROINAME, ISNULL(PURCHASEORDER.PO_OCTROIAMT, 0) AS PO_OCTROIAMT, ISNULL(PURCHASEORDER.PO_INSAMT, 0) AS PO_INSAMT, ISNULL(PURCHASEORDER.PO_ROUNDOFF, 0) AS PO_ROUNDOFF, ISNULL(PURCHASEORDER.PO_GRANDTOTAL, 0) AS PO_GRANDTOTAL, ISNULL(PURCHASEORDER_DESC.PO_count, 0) AS COUNT, ISNULL(PURCHASEORDER_DESC.PO_WIDTH, 0) AS WIDTH, ISNULL(PURCHASEORDER_DESC.PO_WT, 0) AS WT, ISNULL(PURCHASEORDER_DESC.PO_PER, '') AS PER, ISNULL(TOLEDGERS.Acc_cmpname, '') AS TONAME, ISNULL(BROKERLEDGERS.Acc_cmpname, '') AS BROKER, ISNULL(PURCHASEORDER.PO_VERIFIED, 0) AS VERIFIED, ISNULL(PURCHASEORDER.PO_SMSSEND, 0) AS SMSSEND, ISNULL(LEDGERS.Acc_mobile, '') AS MOBILENO, ISNULL(PURCHASEORDER.PO_ORDERTYPE, '') AS ORDERTYPE, ISNULL(PURCHASEORDER_DESC.PO_CLOSED, 0) AS CLOSED, CITYMASTER.city_name AS CITYNAME, ISNULL(PURCHASEORDER.PO_SENDWHATSAPP, 0) AS SENDWHATSAPP , ISNULL(PURCHASEORDER_DESC.PO_QTY- PURCHASEORDER_DESC.PO_RECDQTY, 0) AS BALQTY,ISNULL(PURCHASEORDER.PO_ORDERON,'') AS ORDERON ", "", " CITYMASTER RIGHT OUTER JOIN LEDGERS ON CITYMASTER.city_id = LEDGERS.Acc_cityid RIGHT OUTER JOIN PURCHASEORDER INNER JOIN  PURCHASEORDER_DESC ON PURCHASEORDER.PO_NO = PURCHASEORDER_DESC.PO_NO AND PURCHASEORDER.PO_YEARID = PURCHASEORDER_DESC.PO_YEARID INNER JOIN ITEMMASTER ON PURCHASEORDER_DESC.PO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN  DESIGNMASTER ON PURCHASEORDER_DESC.PO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON PURCHASEORDER.PO_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN UNITMASTER ON PURCHASEORDER_DESC.PO_QTYUNITID = UNITMASTER.unit_id LEFT OUTER JOIN LEDGERS AS BROKERLEDGERS ON PURCHASEORDER.PO_BROKERID = BROKERLEDGERS.Acc_id LEFT OUTER JOIN  LEDGERS AS TOLEDGERS ON PURCHASEORDER_DESC.PO_TOLEDGERID = TOLEDGERS.Acc_id ON LEDGERS.Acc_id = PURCHASEORDER.PO_LEDGERID LEFT OUTER JOIN QUALITYMASTER ON PURCHASEORDER_DESC.PO_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN  COLORMASTER ON PURCHASEORDER_DESC.PO_COLORID = COLORMASTER.COLOR_id  ", " AND (PURCHASEORDER.PO_YEARID = " & YearId & ") AND ISNULL(PURCHASEORDER_DESC.PO_CLOSED,0) = 'FALSE' ORDER BY dbo.PURCHASEORDER.PO_NO, dbo.PURCHASEORDER_DESC.PO_GRIDSRNO")
                gridbilldetails.DataSource = dt
                If dt.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If
            Else
                Dim OBJPO As New ClsPurchaseOrder
                Dim dt As DataTable = OBJPO.selectpo(0, CmpId, 0, YearId)
                gridbilldetails.DataSource = dt
                If dt.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal PONO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New PurchaseOrder
                objPO.MdiParent = MDIMain
                objPO.edit = editval
                objPO.tempono = PONO
                objPO.Show()
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
            showform(True, gridbill.GetFocusedRowCellValue("PONO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLMAIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLMAIL.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper PO Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail PO from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(True)
                End If
            Else
                If MsgBox("Wish to Mail Selected PO ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper PO Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp PO from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected PO ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(False, True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub


            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper PO Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Wish to Print PO from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPDIRECT()
                End If
            Else
                If MsgBox("Wish to Print Selected PO Note ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPDIRECT(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try

            Dim PRINTPARTYDESIGN As Boolean = False
            If ClientName = "MAHAVIRPOLYCOT" And MsgBox("Print PO With Party Design?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then PRINTPARTYDESIGN = True Else PRINTPARTYDESIGN = False
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                Dim OBJPO As New PurchaseOrderDesign
                OBJPO.MdiParent = MDIMain
                OBJPO.DIRECTPRINT = True
                OBJPO.FRMSTRING = "POREPORT"
                OBJPO.DIRECTMAIL = INVOICEMAIL
                OBJPO.DIRECTWHATSAPP = WHATSAPP
                OBJPO.PRINTSETTING = PRINTDIALOG
                OBJPO.PRINTPARTYDESIGN = PRINTPARTYDESIGN
                OBJPO.FORMULA = "{PURCHASEORDER.PO_NO}=" & Val(I) & " and {PURCHASEORDER.PO_yearid}=" & YearId
                OBJPO.PONO = Val(I)
                OBJPO.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJPO.Show()
                OBJPO.Close()

                ALATTACHMENT.Add(Application.StartupPath & "\POREPORT_" & I & ".pdf")
                FILENAME.Add("POREPORT_" & I & ".pdf")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "POREPORT"
                OBJMAIL.ShowDialog()
            End If

            If WHATSAPP = True Then
                Dim OBJWHATSAPP As New SendWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPSELECTED(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            Dim PRINTPARTYDESIGN As Boolean = False
            If ClientName = "MAHAVIRPOLYCOT" And MsgBox("Print PO With Party Design?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then PRINTPARTYDESIGN = True Else PRINTPARTYDESIGN = False

            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                Dim ROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

                Dim OBJPO As New PurchaseOrderDesign
                OBJPO.MdiParent = MDIMain
                OBJPO.DIRECTPRINT = True
                OBJPO.FRMSTRING = "POREPORT"
                OBJPO.DIRECTMAIL = INVOICEMAIL
                OBJPO.DIRECTWHATSAPP = WHATSAPP
                OBJPO.PRINTSETTING = PRINTDIALOG
                OBJPO.PRINTPARTYDESIGN = PRINTPARTYDESIGN
                OBJPO.FORMULA = "{PURCHASEORDER.PO_NO}=" & Val(ROW("PONO")) & " and {PURCHASEORDER.PO_yearid}=" & YearId
                OBJPO.PONO = Val(ROW("PONO"))
                OBJPO.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJPO.Show()
                OBJPO.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\POREPORT_" & Val(ROW("PONO")) & ".pdf")
                FILENAME.Add("POREPORT_" & Val(ROW("PONO")) & ".pdf")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "POREPORT"
                OBJMAIL.ShowDialog()
            End If

            If WHATSAPP = True Then
                Dim OBJWHATSAPP As New SendWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTFROM.KeyPress, TXTTO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("PONO"))
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
                    e.Appearance.BackColor = Color.Yellow
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("DONE")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightGreen
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXPORT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXPORT.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\PO Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = ""
            PERIOD = AccFrom & " - " & AccTo

            opti.SheetName = "PO Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "PO Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("PO Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub PurchaseOrderDetails_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "ABHEE" Then
                GORDERNO.Visible = False
                GQUALITY.Visible = False
                GCOLOR.Visible = False
                GPDESNO.Visible = False
                GPSHADE.Visible = False
                GAMT.Visible = False
                GRECDQTY.Visible = False
                GTONAME.Visible = False
                GORDERTYPE.Visible = False
                GBROKER.Visible = False
                GDONE.Visible = False
                GTRANSNAME.Visible = False
                CHKPENDING.Visible = True
                GDESIGNNO.Caption = "P Quality"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKPENDING_Click(sender As Object, e As EventArgs) Handles CHKPENDING.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class