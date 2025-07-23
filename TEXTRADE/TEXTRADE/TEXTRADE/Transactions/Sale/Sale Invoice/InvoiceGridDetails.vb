
Imports System.IO
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class InvoiceGridDetails

    Dim SALEREGID As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public multi As Boolean = False
    Public fromno, tono As Integer
    Public PARTYNAME As String

    Private Sub InvoiceGridDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Alt = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon

            Dim dt As DataTable = OBJCMN.SEARCH(" INVOICEMASTER.INVOICE_NO AS INVOICENO, INVOICEMASTER.INVOICE_DATE AS INVOICEDATE, ISNULL(INVOICEMASTER.INVOICE_LRNO, '') AS LRNO, ISNULL(INVOICEMASTER.INVOICE_GDNNO, '0') AS CHALLANNO, 
                         INVOICEMASTER.INVOICE_GDNDATE AS CHALLANDATE, ISNULL(INVOICEMASTER.INVOICE_REFNO, 0) AS REFNO, LEDGERS.Acc_cmpname AS NAME, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(LEDGERS.Acc_add, '') 
                         AS ADDRESS, ISNULL(INVOICEMASTER.INVOICE_EWAYBILLNO, '') AS EWAYBILLNO, ISNULL(INVOICEMASTER.INVOICE_IRNNO, '') AS IRNNO, INVOICEMASTER.INVOICE_LRDATE AS LRDATE, ISNULL(ITEMMASTER.item_name, 
                         '') AS ITEMNAME, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') 
                         AS COLOR, ISNULL(INVOICEMASTER_DESC.INVOICE_BALENO, '') AS BALENO, ISNULL(INVOICEMASTER_DESC.INVOICE_PCS, 0) AS PCS, ISNULL(INVOICEMASTER_DESC.INVOICE_MTRS, 0) AS MTRS, 
                         ISNULL(INVOICEMASTER_DESC.INVOICE_PER, '') AS PER, ISNULL(INVOICEMASTER_DESC.INVOICE_RATE, 0) AS RATE, ISNULL(INVOICEMASTER_DESC.INVOICE_AMOUNT, 0) AS AMT, ISNULL(AGENT.Acc_cmpname, '') 
                         AS AGENT, ISNULL(PACKINGLEDERS.Acc_cmpname, '') AS PACKING, ISNULL(PACKINGLEDERS.Acc_add, '') AS PACKINGADDRESS, ISNULL(TRANSLEDGER.Acc_cmpname, '') AS TRANSNAME, 
                         ISNULL(TRANSLEDGER.ACC_GSTIN, '') AS TRANSGSTIN, ISNULL(INVOICEMASTER.INVOICE_PONO, '0') AS PONO, ISNULL(INVOICEMASTER.INVOICE_BALENOFROM, 0) AS BALENOFROM, 
                         ISNULL(INVOICEMASTER.INVOICE_AMOUNT, 0) AS AMOUNT, ISNULL(INVOICEMASTER.INVOICE_TOTALDISCAMT, 0) AS TOTALDISCAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALSPDISCAMT, 0) AS TOTALSPDISCAMT, 
                         ISNULL(INVOICEMASTER.INVOICE_CHARGES, 0) AS CHARGES, ISNULL(INVOICEMASTER.INVOICE_SUBTOTAL, 0) AS TOTALTAXABLEAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALCGSTAMT, 0) AS TOTALCGSTAMT, 
                         ISNULL(INVOICEMASTER.INVOICE_TOTALSGSTAMT, 0) AS TOTALSGSTAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALIGSTAMT, 0) AS TOTALIGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_CGSTAMT, 0) AS CGSTAMT, 
                         ISNULL(INVOICEMASTER_DESC.INVOICE_SGSTPER, 0) AS SGSTPER, ISNULL(INVOICEMASTER_DESC.INVOICE_SGSTAMT, 0) AS SGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_IGSTPER, 0) AS IGSTPER, 
                         ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL, ISNULL(INVOICEMASTER.INVOICE_AMTREC, 0) AS AMTREC, ISNULL(INVOICEMASTER.INVOICE_EXTRAAMT, 0) AS EXTRAAMT, 
                         ISNULL(INVOICEMASTER.INVOICE_RETURN, 0) AS [RETURN], ISNULL(INVOICEMASTER.INVOICE_BALANCE, 0) AS BALANCE, ISNULL(INVOICEMASTER.INVOICE_REMARKS, '') AS REMARKS, 
                         ISNULL(INVOICEMASTER.INVOICE_CHECKED, 0) AS BILLCHECKED, ISNULL(INVOICEMASTER.INVOICE_DISPUTE, 0) AS BILLDISPUTE, ISNULL(PURLEDGERS.Acc_cmpname, '') AS PURNAME, ISNULL(STATEMASTER.state_name,
                          '') AS STATENAME, ISNULL(CITYMASTER.city_name, '') AS CITYNAME, REGISTERMASTER.register_name AS REGNAME, ISNULL(INVOICEMASTER.INVOICE_INITIALS, '') AS INITIALS, 
                         ISNULL(INVOICEMASTER.INVOICE_PARTYPONO, 0) AS PARTYPONO, ISNULL(INVOICEMASTER_DESC.INVOICE_PRINTDESC, '') AS DESCRIPTION, ISNULL(INVOICEMASTER.INVOICE_TRADINGACC, 0) AS TRADING, 
                         UPPER(DATENAME(MONTH, INVOICEMASTER.INVOICE_DATE)) AS MONTHNAME, ISNULL(INVOICEMASTER_DESC.INVOICE_QTY, 0) AS QTY, ISNULL(INVOICEMASTER_DESC.INVOICE_FOLDPER, 0) AS FOLDPER, 
                         ISNULL(INVOICEMASTER_DESC.INVOICE_WT, 0) AS WT, ISNULL(INVOICEMASTER_DESC.INVOICE_PARTYBILLNO, '') AS PARTYBILLNO, ISNULL(INVOICEMASTER_DESC.INVOICE_GRIDLRNO, '') AS GRIDLRNO, 
                         ISNULL(GRIDTRANSLEDGERS.Acc_cmpname, '') AS GRIDTRANSE, ISNULL(GRIDPURLEDGERS.Acc_cmpname, '') AS GRIDPURNAME   ", "", " INVOICEMASTER INNER JOIN
                         LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN
                         INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND 
                         INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN
                         REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN
                         LEDGERS AS GRIDPURLEDGERS ON INVOICEMASTER_DESC.INVOICE_GRIDPURLEDGERID = GRIDPURLEDGERS.Acc_id LEFT OUTER JOIN
                         LEDGERS AS GRIDTRANSLEDGERS ON INVOICEMASTER_DESC.INVOICE_GRIDTRANSID = GRIDTRANSLEDGERS.Acc_id LEFT OUTER JOIN
                         CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN
                         LEDGERS AS PURLEDGERS ON INVOICEMASTER.INVOICE_PURLEDGERID = PURLEDGERS.Acc_id LEFT OUTER JOIN
                         HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN
                         STATEMASTER ON STATEMASTER.state_id = LEDGERS.Acc_stateid LEFT OUTER JOIN
                         LEDGERS AS PACKINGLEDERS ON INVOICEMASTER.INVOICE_PACKINGID = PACKINGLEDERS.Acc_id LEFT OUTER JOIN
                         COLORMASTER ON INVOICEMASTER_DESC.INVOICE_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN
                         DESIGNMASTER ON INVOICEMASTER_DESC.INVOICE_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN
                         QUALITYMASTER ON INVOICEMASTER_DESC.INVOICE_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN
                         ITEMMASTER ON ITEMMASTER.item_id = INVOICEMASTER_DESC.INVOICE_ITEMID LEFT OUTER JOIN
                         LEDGERS AS TRANSLEDGER ON INVOICEMASTER.INVOICE_TRANSID = TRANSLEDGER.Acc_id LEFT OUTER JOIN
                         LEDGERS AS AGENT ON INVOICEMASTER.INVOICE_AGENTID = AGENT.Acc_id ", " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER.INVOICE_NO, INVOICEMASTER_DESC.INVOICE_SRNO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal billno As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJBILL As New InvoiceMaster
                OBJBILL.MdiParent = MDIMain
                OBJBILL.EDIT = editval
                OBJBILL.TEMPINVOICENO = billno
                OBJBILL.TEMPREGNAME = cmbregister.Text.Trim
                OBJBILL.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'SALE'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.SEARCH(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If cmbregister.Text.Trim.Length > 0 Then
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.SEARCH(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    SALEREGID = dt.Rows(0).Item(0)
                    fillgrid(" and INVOICEMASTER.INVOICE_yearid = " & YearId & " AND INVOICEMASTER.INVOICE_registerid = " & SALEREGID & " order by dbo.INVOICEMASTER.INVOICE_no ")
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("INVOICENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridINVOICE_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("INVOICENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVELAYOUT_Click(sender As Object, e As EventArgs) Handles CMDSAVELAYOUT.Click
        Try
            Dim layoutFileName As String = $"{Me.Name}"
            Dim layoutPath As String = System.IO.Path.Combine(Application.StartupPath, layoutFileName)
            gridbill.SaveLayoutToXml(layoutPath)
            MessageBox.Show("Layout saved as: " & layoutFileName)




            ' Prompt user for filename
            Dim userFileName As String = InputBox("Enter a name for the layout file (without extension):", "Save Layout", Me.Name)

            ' Exit if the user cancels or enters nothing
            If String.IsNullOrWhiteSpace(userFileName) Then
                MessageBox.Show("Save cancelled.")
                Exit Sub
            End If

            ' Add .xml extension and construct path
            Dim FileName As String = $"{userFileName}.xml"

            ' Save layout to file
            gridbill.SaveLayoutToXml(layoutPath)
            MessageBox.Show("Layout saved as: " & layoutFileName)

            ' Read file content
            Dim xmlContent As String = File.ReadAllText(layoutPath)



            Dim OBJSELECTSG As New SelectCustomLayout
            OBJSELECTSG.FORMNAMES = layoutFileName
            OBJSELECTSG.FILENAME = FileName
            OBJSELECTSG.FILES = xmlContent
            OBJSELECTSG.ShowDialog()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InvoiceGridDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE INVOICE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
            fillregister(cmbregister, " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
            If MsgBox("Want to Load Layout ? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim layoutFileName As String = $"{Me.Name}.xml"
                Dim OBJPO As New ClsCustomLayout
                Dim dt As DataTable = OBJPO.selectLAYOUT(UserName, layoutFileName, CmpId, YearId)
                If dt.Rows.Count > 0 AndAlso dt.Columns.Contains("CL_LayoutContent") Then
                    Dim layoutXml As String = dt.Rows(0)("CL_LayoutContent").ToString()

                    If Not String.IsNullOrWhiteSpace(layoutXml) Then
                        Using ms As New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(layoutXml))
                            gridbill.RestoreLayoutFromStream(ms)
                        End Using
                    End If
                Else
                    MessageBox.Show("Layout file not found: " & layoutFileName)
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("BILLCHECKED")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightGreen
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("BILLDISPUTE")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Sale Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Sale Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sale Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Invoice Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub InvoiceGridDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "KDFAB" Then
                GRECDDATE.Visible = True
                GRECDDATE.VisibleIndex = GRECDAMT.VisibleIndex + 1
            End If
            If ClientName = "SAKARIA" Or ClientName = "NVAHAN" Then
                GSUPPLIERNAME.Visible = True
                GSUPPLIERNAME.VisibleIndex = 11
            End If
            If ClientName = "ABHEE" Then
                GDESIGNNO.Visible = False
                GSHADE.Visible = False
                GQTY.Visible = True
                GFOLDPER.Visible = True
                GQTY.VisibleIndex = 15
                GFOLDPER.VisibleIndex = 16
                GGRIDWT.Visible = True
                GGRIDLRNO.Visible = True
                GGRIDPURNAME.Visible = True
                GGRIDPARTYBILLNO.Visible = True
                GGRIDTRANSPORT.Visible = True



            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class