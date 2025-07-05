
Imports BL
Imports System.Windows.Forms
Public Class AgencySaleReturnGridDetails
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub AgencySaleReturnGridDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                cmdok_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AgencySaleReturnGridDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid(" and AGENCYSALERETURN.ASALRET_yearid=" & YearId & " order by AGENCYSALERETURN.ASALRET_no ")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("  AGENCYSALERETURN.ASALRET_NO AS SRNO, AGENCYSALERETURN.ASALRET_DATE AS DATE, AGENCYSALERETURN.ASALRET_CHALLANDATE AS CHALLANDATE, ISNULL(AGENCYSALERETURN.ASALRET_CHALLANNO, '') AS CHALLANNO, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(AGENCYSALERETURN.ASALRET_EWAYBILLNO, '') AS EWAYBILLNO, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENCYSALERETURN.ASALRET_GRANDTOTAL, 0) AS GTOTAL, ISNULL(AGENCYSALERETURN.ASALRET_remarks, '') AS REMARKS, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE,  ISNULL(AGENCYSALERETURN_DESC.ASALRET_QTY, 0) AS PCS, ISNULL(AGENCYSALERETURN_DESC.ASALRET_MTRS, 0) AS MTRS, ISNULL(AGENCYSALERETURN.ASALRET_INVOICENO, '') AS INVOICENO, ISNULL(AGENCYSALERETURN.ASALRET_INVOICEDATE, GETDATE()) AS INVOICEDATE, ISNULL(AGENCYSALERETURN_DESC.ASALRET_AMT, 0) AS AMT, ISNULL(AGENCYSALERETURN.ASALRET_TOTALCGSTAMT, 0) AS CGSTAMT, ISNULL(AGENCYSALERETURN.ASALRET_TOTALSGSTAMT, 0) AS SGSTAMT,  ISNULL(AGENCYSALERETURN.ASALRET_SGSTPER, 0) AS SGSTPER, ISNULL(AGENCYSALERETURN.ASALRET_CGSTPER, 0) AS CGSTPER, ISNULL(AGENCYSALERETURN.ASALRET_IGSTPER, 0) AS IGSTPER, ISNULL(AGENCYSALERETURN.ASALRET_TOTALIGSTAMT, 0) AS IGSTAMT, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(AGENCYSALERETURN.ASALRET_LRNO, '') AS LRNO, (CASE WHEN ISNULL(AGENCYSALERETURN.ASALRET_LRNO, '') = '' THEN NULL ELSE ASALRET_LRDATE END)  AS LRDATE, ISNULL(AGENCYSALERETURN_DESC.ASALRET_BALENO, '') AS BALENO, ISNULL(agentLEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(AGENCYSALERETURN.ASALRET_PARTYREFNO, '') AS PARTYREFNO, ISNULL(DEBITLEDGERS.Acc_cmpname, '') AS DEBITNAME, ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS DELIVERYAT, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT,ISNULL(AGENCYSALERETURN_DESC.ASALRET_PER, '') AS PER, ISNULL(AGENCYSALERETURN_DESC.ASALRET_AQTY, 0) AS AQTY, ISNULL(AGENCYSALERETURN_DESC.ASALRET_AFOLDPER, 0) AS AFOLDPER ", "", "  AGENCYSALERETURN INNER JOIN LEDGERS ON AGENCYSALERETURN.ASALRET_ledgerid = LEDGERS.Acc_id INNER JOIN AGENCYSALERETURN_DESC ON AGENCYSALERETURN.ASALRET_NO = AGENCYSALERETURN_DESC.ASALRET_NO AND AGENCYSALERETURN.ASALRET_yearid = AGENCYSALERETURN_DESC.ASALRET_YEARID LEFT OUTER JOIN  LEDGERS AS TRANSLEDGERS ON AGENCYSALERETURN.ASALRET_TRANSID = TRANSLEDGERS.Acc_id INNER JOIN ITEMMASTER ON AGENCYSALERETURN_DESC.ASALRET_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON AGENCYSALERETURN_DESC.ASALRET_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON AGENCYSALERETURN_DESC.ASALRET_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS AS agentLEDGERS ON AGENCYSALERETURN.ASALRET_AGENTID = agentLEDGERS.Acc_id LEFT OUTER JOIN  HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN  LEDGERS AS DEBITLEDGERS ON AGENCYSALERETURN.ASALRET_DEBITLEDGERID = DEBITLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON AGENCYSALERETURN.ASALRET_DELIVERYATID = PACKINGLEDGERS.Acc_id ", TEMPCONDITION)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal SALRETNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJSALRET As New AgencySaleReturn
                OBJSALRET.MdiParent = MDIMain
                OBJSALRET.EDIT = editval
                OBJSALRET.TEMPSALRETNO = SALRETNO
                OBJSALRET.Show()
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
            fillgrid(" and AGENCYSALERETURN.ASALRET_yearid=" & YearId & " order by AGENCYSALERETURN.ASALRET_no ")
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


    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Sale Return Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Sale Return Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sale Return Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Sale Return Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub AgencySaleReturnGridDetails_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If ClientName = "ABHEE" Then
            GDESIGNNO.Visible = False
            GSHADE.Visible = False
            GAQTY.Visible = True
            GFOLDPER.Visible = True
            GAQTY.VisibleIndex = 15
            GFOLDPER.VisibleIndex = 16
        End If
    End Sub
End Class