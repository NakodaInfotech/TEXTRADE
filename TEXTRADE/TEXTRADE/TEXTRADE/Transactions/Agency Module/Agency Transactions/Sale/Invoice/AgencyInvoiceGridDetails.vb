Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class AgencyInvoiceGridDetails


    Dim SALEREGID As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public multi As Boolean = False
    Public fromno, tono As Integer
    Public PARTYNAME As String

    Private Sub AgencyInvoiceGridDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

            Dim dt As DataTable = OBJCMN.SEARCH(" AGENCYINVOICEMASTER.AINVOICE_NO AS INVOICENO, AGENCYINVOICEMASTER.AINVOICE_DATE AS INVOICEDATE, ISNULL(AGENCYINVOICEMASTER.AINVOICE_LRNO, '') AS LRNO, 
                         ISNULL(AGENCYINVOICEMASTER.AINVOICE_GDNNO, '0') AS CHALLANNO, AGENCYINVOICEMASTER.AINVOICE_GDNDATE AS CHALLANDATE, ISNULL(AGENCYINVOICEMASTER.AINVOICE_REFNO, 0) AS REFNO, 
                         LEDGERS.Acc_cmpname AS NAME, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(LEDGERS.Acc_add, '') AS ADDRESS, ISNULL(AGENCYINVOICEMASTER.AINVOICE_EWAYBILLNO, '') AS EWAYBILLNO, 
                         ISNULL(AGENCYINVOICEMASTER.AINVOICE_IRNNO, '') AS IRNNO, AGENCYINVOICEMASTER.AINVOICE_LRDATE AS LRDATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(HSNMASTER.HSN_CODE, '') 
                         AS HSNCODE, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, 
                         ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_BALENO, '') AS BALENO, ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_PCS, 0) AS PCS, ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_MTRS, 0) AS MTRS, 
                         ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_PER, '') AS PER, ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_RATE, 0) AS RATE, ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_AMOUNT, 0) AS AMT, 
                         ISNULL(AGENT.Acc_cmpname, '') AS AGENT, ISNULL(PACKINGLEDERS.Acc_cmpname, '') AS PACKING, ISNULL(PACKINGLEDERS.Acc_add, '') AS PACKINGADDRESS, ISNULL(TRANSLEDGER.Acc_cmpname, '') AS TRANSNAME, 
                         ISNULL(TRANSLEDGER.ACC_GSTIN, '') AS TRANSGSTIN, ISNULL(AGENCYINVOICEMASTER.AINVOICE_PONO, '0') AS PONO, ISNULL(AGENCYINVOICEMASTER.AINVOICE_BALENOFROM, 0) AS BALENOFROM, 
                         ISNULL(AGENCYINVOICEMASTER.AINVOICE_AMOUNT, 0) AS AMOUNT, ISNULL(AGENCYINVOICEMASTER.AINVOICE_TOTALDISCAMT, 0) AS TOTALDISCAMT, ISNULL(AGENCYINVOICEMASTER.AINVOICE_TOTALSPDISCAMT, 0) 
                         AS TOTALSPDISCAMT, ISNULL(AGENCYINVOICEMASTER.AINVOICE_CHARGES, 0) AS CHARGES, ISNULL(AGENCYINVOICEMASTER.AINVOICE_TOTALTAXABLEAMT, 0) AS TOTALTAXABLEAMT, 
                         ISNULL(AGENCYINVOICEMASTER.AINVOICE_TOTALCGSTAMT, 0) AS TOTALCGSTAMT, ISNULL(AGENCYINVOICEMASTER.AINVOICE_TOTALSGSTAMT, 0) AS TOTALSGSTAMT, 
                         ISNULL(AGENCYINVOICEMASTER.AINVOICE_TOTALIGSTAMT, 0) AS TOTALIGSTAMT, ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_CGSTAMT, 0) AS CGSTAMT, 
                         ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_SGSTPER, 0) AS SGSTPER, ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_SGSTAMT, 0) AS SGSTAMT, ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_IGSTPER, 0) 
                         AS IGSTPER, ISNULL(AGENCYINVOICEMASTER.AINVOICE_GRANDTOTAL, 0) AS GRANDTOTAL, ISNULL(AGENCYINVOICEMASTER.AINVOICE_AMTREC, 0) AS AMTREC, ISNULL(AGENCYINVOICEMASTER.AINVOICE_EXTRAAMT, 0) 
                         AS EXTRAAMT, ISNULL(AGENCYINVOICEMASTER.AINVOICE_RETURN, 0) AS [RETURN], ISNULL(AGENCYINVOICEMASTER.AINVOICE_BALANCE, 0) AS BALANCE, ISNULL(AGENCYINVOICEMASTER.AINVOICE_REMARKS, '') 
                         AS REMARKS, ISNULL(AGENCYINVOICEMASTER.AINVOICE_CHECKED, 0) AS BILLCHECKED, ISNULL(AGENCYINVOICEMASTER.AINVOICE_DISPUTE, 0) AS BILLDISPUTE, ISNULL(PURLEDGERS.Acc_cmpname, '') AS PURNAME, 
                         ISNULL(STATEMASTER.state_name, '') AS STATENAME, ISNULL(CITYMASTER.city_name, '') AS CITYNAME, ISNULL(AGENCYINVOICEMASTER.AINVOICE_INITIALS, '') AS INITIALS, 
                         ISNULL(AGENCYINVOICEMASTER.AINVOICE_PARTYPONO, 0) AS PARTYPONO, ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_PRINTDESC, '') AS DESCRIPTION, ISNULL(AGENCYINVOICEMASTER.AINVOICE_TRADINGACC, 
                         0) AS TRADING, UPPER(DATENAME(MONTH, AGENCYINVOICEMASTER.AINVOICE_DATE)) AS MONTHNAME, ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_QTY, 0) AS QTY, 
                         ISNULL(AGENCYINVOICEMASTER_DESC.AINVOICE_FOLDPER, 0) AS FOLDPER   ", "", " AGENCYINVOICEMASTER INNER JOIN
                         LEDGERS ON AGENCYINVOICEMASTER.AINVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN
                         AGENCYINVOICEMASTER_DESC ON AGENCYINVOICEMASTER.AINVOICE_NO = AGENCYINVOICEMASTER_DESC.AINVOICE_NO AND 
                         AGENCYINVOICEMASTER.AINVOICE_YEARID = AGENCYINVOICEMASTER_DESC.AINVOICE_YEARID LEFT OUTER JOIN
                         CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN
                         LEDGERS AS PURLEDGERS ON AGENCYINVOICEMASTER.AINVOICE_PURLEDGERID = PURLEDGERS.Acc_id LEFT OUTER JOIN
                         HSNMASTER ON AGENCYINVOICEMASTER_DESC.AINVOICE_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN
                         STATEMASTER ON STATEMASTER.state_id = LEDGERS.Acc_stateid LEFT OUTER JOIN
                         LEDGERS AS PACKINGLEDERS ON AGENCYINVOICEMASTER.AINVOICE_PACKINGID = PACKINGLEDERS.Acc_id LEFT OUTER JOIN
                         COLORMASTER ON AGENCYINVOICEMASTER_DESC.AINVOICE_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN
                         DESIGNMASTER ON AGENCYINVOICEMASTER_DESC.AINVOICE_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN
                         QUALITYMASTER ON AGENCYINVOICEMASTER_DESC.AINVOICE_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN
                         ITEMMASTER ON ITEMMASTER.item_id = AGENCYINVOICEMASTER_DESC.AINVOICE_ITEMID LEFT OUTER JOIN
                         LEDGERS AS TRANSLEDGER ON AGENCYINVOICEMASTER.AINVOICE_TRANSID = TRANSLEDGER.Acc_id LEFT OUTER JOIN
                         LEDGERS AS AGENT ON AGENCYINVOICEMASTER.AINVOICE_AGENTID = AGENT.Acc_id ", " AND AGENCYINVOICEMASTER.AINVOICE_YEARID = " & YearId & " ORDER BY AGENCYINVOICEMASTER.AINVOICE_NO, AGENCYINVOICEMASTER_DESC.AINVOICE_SRNO")
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
                Dim OBJBILL As New AgencyInvoice
                OBJBILL.MdiParent = MDIMain
                OBJBILL.EDIT = editval
                OBJBILL.TEMPINVOICENO = billno
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

    Private Sub gridAINVOICE_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("INVOICENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AgencyInvoiceGridDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE INVOICE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            fillgrid(" and dbo.AGENCYINVOICEMASTER.AINVOICE_yearid=" & YearId)


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

    Private Sub AgencyInvoiceGridDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
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

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class


