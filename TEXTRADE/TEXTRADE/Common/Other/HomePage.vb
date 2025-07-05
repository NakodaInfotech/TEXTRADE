﻿
Imports BL

Public Class HomePage

    Dim OBJCMN As New ClsCommon
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Private Sub HomePage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If HIDEACCOUNTS = True Then
                RECOUTSTANDTOOLVISIBLE = False
                PAYOUTSTANDTOOLVISIBLE = False
                MONTHLYTOOLVISIBLE = False
            End If

            GBRECOUTSTANDING.Visible = RECOUTSTANDTOOLVISIBLE
            GBPAYOUTSTANDING.Visible = PAYOUTSTANDTOOLVISIBLE
            GBSALEORDER.Visible = PENDINGSOTOOLVISIBLE
            GBPURORDER.Visible = PENDINGPOTOOLVISIBLE
            GBSTOCK.Visible = STOCKTOOLVISIBLE
            GBSALE.Visible = MONTHLYTOOLVISIBLE
            GBOVERDUE.Visible = MONTHLYTOOLVISIBLE

            If RECOUTSTANDTOOLVISIBLE = True Then FILLRECGRID()
            If PAYOUTSTANDTOOLVISIBLE = True Then FILLPAYGRID()
            If STOCKTOOLVISIBLE = True Then FILLSTOCK()
            If PENDINGSOTOOLVISIBLE = True Then FILLSO()
            If PENDINGPOTOOLVISIBLE = True Then FILLPO()
            If MONTHLYTOOLVISIBLE = True Then FILLOVERDUE()

            If SALEORDERONMTRS = True Then GSOPCS.Caption = "Mtrs"


            GRIDSO.OptionsView.ShowFooter = True
            GRIDPO.OptionsView.ShowFooter = True
            GRIDSTOCK.OptionsView.ShowFooter = True


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLRECGRID()
        Try
            Dim dt As DataTable = OBJCMN.search(" NAME, (CASE WHEN ROUND(DR,0) > 0 THEN DR ELSE ROUND(CR,0)*-1 END) AS BALANCE ", "", " TRIALBALANCE ", " AND SECONDARY = 'Sundry Debtors' AND (ROUND(DR,0) > 0 OR ROUND(CR,0) > 0) AND YEARID = " & YearId & " ORDER BY NAME")
            GRIDRECDETAILS.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLPAYGRID()
        Try
            Dim dt As DataTable = OBJCMN.search(" NAME, (CASE WHEN ROUND(CR,0) > 0 THEN CR ELSE ROUND(DR,0)*-1 END) AS BALANCE ", "", " TRIALBALANCE ", " AND SECONDARY = 'Sundry Creditors' AND (ROUND(DR,0) > 0 OR ROUND(CR,0) > 0) AND YEARID = " & YearId & " ORDER BY NAME")
            GRIDPAYDETAILS.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSTOCK()
        Try
            Dim dt As New DataTable
            If ALLOWBARCODEPRINT = True Then
                'FETCH FROM BARCODESTOCK
                'IF WE HAVE DEFAULT STOCK UNIT THEN FETCH STOCK OF THAT UNIT ONLY AND FRESH
                Dim WHERECLAUSE As String = ""
                If ClientName <> "PARAS" Then WHERECLAUSE = "  AND PIECETYPE ='FRESH' "
                Dim OBJCMN As New ClsCommon
                Dim DTUNIT As DataTable = OBJCMN.search("UNIT_ABBR", "", "DEFAULTSTOCKUNIT", "")
                If DTUNIT.Rows.Count > 0 Then WHERECLAUSE = WHERECLAUSE & " AND UNIT IN (SELECT UNIT_ABBR FROM DEFAULTSTOCKUNIT)"
                If ClientName = "AVIS" Or ClientName = "SUPRIYA" Or ClientName = "KRISHNA" Or ClientName = "INDRANI" Or ClientName = "NTC" Or ClientName = "INDRAPUJAIMPEX" Then
                    GDESIGN.Visible = True
                    GDESIGN.VisibleIndex = 0
                    dt = OBJCMN.search(" ITEMNAME, DESIGNNO, SUM(PCS) AS PCS, SUM(MTRS) AS MTRS ", "", " BARCODESTOCK ", WHERECLAUSE & " AND YEARID = " & YearId & " GROUP BY ITEMNAME, DESIGNNO")
                ElseIf ClientName = "PARAS" Then
                    GCATEGORY.Visible = True
                    GCATEGORY.VisibleIndex = 0
                    dt = OBJCMN.search(" ITEMNAME, CATEGORY, SUM(PCS) AS PCS, SUM(MTRS) AS MTRS ", "", " BARCODESTOCK ", WHERECLAUSE & " AND YEARID = " & YearId & " GROUP BY ITEMNAME, CATEGORY")

                    'FETCH STOCK IN HAND AND INHOUSE CHECKING
                ElseIf ClientName = "SOFTAS" Then
                    dt = OBJCMN.Execute_Any_String("SELECT SUM(T.PCS) AS PCS, SUM(T.MTRS) AS MTRS, T.ITEMNAME FROM (SELECT SUM(PCS) AS PCS, SUM(MTRS) AS MTRS, ITEMNAME FROM BARCODESTOCK WHERE 1 = 1 " & WHERECLAUSE & " AND YEARID = " & YearId & " GROUP BY ITEMNAME UNION ALL SELECT ROUND(ISSUEPACKING_DESC.ISS_PCS - ISNULL(ISSUEPACKING_DESC.ISS_OUTPCS, 0), 2) AS BALPCS, ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2)  AS BALMTRS, ITEMMASTER.item_name AS ITEMNAME FROM ITEMMASTER INNER JOIN ISSUEPACKING_DESC ON ITEMMASTER.item_id = ISSUEPACKING_DESC.ISS_ITEMID WHERE (ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2) > 0) AND ISSUEPACKING_DESC.ISS_YEARID = " & YearId & ") AS T GROUP BY ITEMNAME", "", "")
                Else
                    dt = OBJCMN.search(" ITEMNAME, SUM(PCS) AS PCS, SUM(MTRS) AS MTRS ", "", " BARCODESTOCK ", WHERECLAUSE & " AND YEARID = " & YearId & " GROUP BY ITEMNAME")
                End If
            Else
                'FETCH FROM STOCKREGISTER
                If ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                    GGODOWN.Visible = True
                    GGODOWN.VisibleIndex = 0
                    dt = OBJCMN.search(" GODOWN, ITEMNAME, (SUM(PCS)-SUM(ISSPCS)) AS PCS, (SUM(MTRS)-SUM(ISSMTRS)) AS MTRS", "", " STOCKREGISTER ", " AND YEARID = " & YearId & " GROUP BY GODOWN, ITEMNAME HAVING (SUM(MTRS)-SUM(ISSMTRS)) > 0")
                Else
                    Dim WHERECLAUSE As String = ""
                    If ClientName = "KREEVE" Or ClientName = "GELATO" Or ClientName = "AXIS" Then WHERECLAUSE = " (SUM(PCS)-SUM(ISSPCS)) > 0 " Else WHERECLAUSE = " (SUM(MTRS)-SUM(ISSMTRS)) > 0"
                    dt = OBJCMN.search(" ITEMNAME, (SUM(PCS)-SUM(ISSPCS)) AS PCS, (SUM(MTRS)-SUM(ISSMTRS)) AS MTRS", "", " STOCKREGISTER ", " AND YEARID = " & YearId & " GROUP BY ITEMNAME HAVING " & WHERECLAUSE)
                End If
            End If
            GRIDSTOCKDETAILS.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSO()
        Try
            Dim DT As New DataTable
            If SALEORDERONMTRS = False Then
                If ClientName = "KRISHNA" Then
                    GSODESIGN.Visible = True
                    GSOCOLOR.Visible = True
                    GSODESIGN.VisibleIndex = 0
                    GSOCOLOR.VisibleIndex = 1
                    DT = OBJCMN.search("*", "", " (SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(ITEMMASTER.item_name,'') AS [ITEMNAME], ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR, SUM(SALEORDER_DESC.SO_QTY - SALEORDER_DESC.SO_RECDQTY) AS [PCS] FROM SALEORDER INNER JOIN LEDGERS ON SALEORDER.so_ledgerid = LEDGERS.Acc_id INNER JOIN SALEORDER_DESC ON SALEORDER.so_no = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID INNER JOIN ITEMMASTER ON SALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON SALEORDER_DESC.SO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON SALEORDER_DESC.SO_COLORID = COLOR_ID WHERE ROUND((SALEORDER_DESC.SO_MTRS - SALEORDER_DESC.SO_RECDMTRS),0) > 0 AND SALEORDER_DESC.SO_CLOSED=0  AND SALEORDER.so_YEARID = " & YearId & " GROUP BY ISNULL(LEDGERS.ACC_CMPNAME,''), ISNULL(ITEMMASTER.ITEM_NAME,''), ISNULL(DESIGNMASTER.DESIGN_NO,''), ISNULL(COLORMASTER.COLOR_NAME,'') UNION ALL  SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(ITEMMASTER.item_name,'') AS [ITEMNAME], ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR, SUM(OPENINGSALEORDER_DESC.OPSO_MTRS - OPENINGSALEORDER_DESC.OPSO_RECDMTRS) AS [PCS] FROM OPENINGSALEORDER INNER JOIN LEDGERS ON OPENINGSALEORDER.OPSO_ledgerid = LEDGERS.Acc_id INNER JOIN OPENINGSALEORDER_DESC ON OPENINGSALEORDER.OPSO_no = OPENINGSALEORDER_DESC.OPSO_NO AND OPENINGSALEORDER.OPSO_YEARID = OPENINGSALEORDER_DESC.OPSO_YEARID INNER JOIN ITEMMASTER ON OPENINGSALEORDER_DESC.OPSO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGSALEORDER_DESC.OPSO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON OPENINGSALEORDER_DESC.OPSO_COLORID = COLOR_ID WHERE (OPENINGSALEORDER_DESC.OPSO_QTY - OPENINGSALEORDER_DESC.OPSO_RECDQTY) > 0 AND OPENINGSALEORDER_DESC.OPSO_CLOSED=0 AND OPENINGSALEORDER.OPSO_YEARID = " & YearId & " GROUP BY ISNULL(LEDGERS.ACC_CMPNAME,''), ISNULL(ITEMMASTER.ITEM_NAME,''), ISNULL(DESIGNMASTER.DESIGN_NO,''), ISNULL(COLORMASTER.COLOR_NAME,'')   ) AS T", "")
                Else
                    DT = OBJCMN.search("*", "", " (SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(ITEMMASTER.item_name,'') AS [ITEMNAME], SUM(SALEORDER_DESC.SO_QTY - SALEORDER_DESC.SO_RECDQTY) AS [PCS] FROM SALEORDER INNER JOIN LEDGERS ON SALEORDER.so_ledgerid = LEDGERS.Acc_id INNER JOIN SALEORDER_DESC ON SALEORDER.so_no = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID INNER JOIN ITEMMASTER ON SALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON SALEORDER_DESC.SO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON SALEORDER_DESC.SO_COLORID = COLOR_ID WHERE (SALEORDER_DESC.SO_QTY - SALEORDER_DESC.SO_RECDQTY) > 0 AND SALEORDER_DESC.SO_CLOSED=0  AND SALEORDER.so_YEARID = " & YearId & " GROUP BY ISNULL(LEDGERS.ACC_CMPNAME,''), ISNULL(ITEMMASTER.ITEM_NAME,'') UNION ALL  SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(ITEMMASTER.item_name,'') AS [ITEMNAME], SUM(OPENINGSALEORDER_DESC.OPSO_QTY - OPENINGSALEORDER_DESC.OPSO_RECDQTY) AS [PCS] FROM OPENINGSALEORDER INNER JOIN LEDGERS ON OPENINGSALEORDER.OPSO_ledgerid = LEDGERS.Acc_id INNER JOIN OPENINGSALEORDER_DESC ON OPENINGSALEORDER.OPSO_no = OPENINGSALEORDER_DESC.OPSO_NO AND OPENINGSALEORDER.OPSO_YEARID = OPENINGSALEORDER_DESC.OPSO_YEARID INNER JOIN ITEMMASTER ON OPENINGSALEORDER_DESC.OPSO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGSALEORDER_DESC.OPSO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON OPENINGSALEORDER_DESC.OPSO_COLORID = COLOR_ID WHERE (OPENINGSALEORDER_DESC.OPSO_QTY - OPENINGSALEORDER_DESC.OPSO_RECDQTY) > 0 AND OPENINGSALEORDER_DESC.OPSO_CLOSED=0  AND OPENINGSALEORDER.OPSO_YEARID = " & YearId & " GROUP BY ISNULL(LEDGERS.ACC_CMPNAME,''), ISNULL(ITEMMASTER.ITEM_NAME,'')) AS T", "")
                End If

            Else
                If ClientName = "AVIS" Or ClientName = "YASHVI" Or ClientName = "SUPRIYA" Then
                    GSODESIGN.Visible = True
                    GSOCOLOR.Visible = True
                    GSODESIGN.VisibleIndex = 0
                    GSOCOLOR.VisibleIndex = 1
                    DT = OBJCMN.search("*", "", " (SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(ITEMMASTER.item_name,'') AS [ITEMNAME], ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR, SUM(SALEORDER_DESC.SO_MTRS - SALEORDER_DESC.SO_RECDMTRS) AS [PCS] FROM SALEORDER INNER JOIN LEDGERS ON SALEORDER.so_ledgerid = LEDGERS.Acc_id INNER JOIN SALEORDER_DESC ON SALEORDER.so_no = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID INNER JOIN ITEMMASTER ON SALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON SALEORDER_DESC.SO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON SALEORDER_DESC.SO_COLORID = COLOR_ID WHERE ROUND((SALEORDER_DESC.SO_MTRS - SALEORDER_DESC.SO_RECDMTRS),0) > 0 AND SALEORDER_DESC.SO_CLOSED=0  AND SALEORDER.so_YEARID = " & YearId & " GROUP BY ISNULL(LEDGERS.ACC_CMPNAME,''), ISNULL(ITEMMASTER.ITEM_NAME,''), ISNULL(DESIGNMASTER.DESIGN_NO,''), ISNULL(COLORMASTER.COLOR_NAME,'') UNION ALL  SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(ITEMMASTER.item_name,'') AS [ITEMNAME], ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR, SUM(OPENINGSALEORDER_DESC.OPSO_MTRS - OPENINGSALEORDER_DESC.OPSO_RECDMTRS) AS [PCS] FROM OPENINGSALEORDER INNER JOIN LEDGERS ON OPENINGSALEORDER.OPSO_ledgerid = LEDGERS.Acc_id INNER JOIN OPENINGSALEORDER_DESC ON OPENINGSALEORDER.OPSO_no = OPENINGSALEORDER_DESC.OPSO_NO AND OPENINGSALEORDER.OPSO_YEARID = OPENINGSALEORDER_DESC.OPSO_YEARID INNER JOIN ITEMMASTER ON OPENINGSALEORDER_DESC.OPSO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGSALEORDER_DESC.OPSO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON OPENINGSALEORDER_DESC.OPSO_COLORID = COLOR_ID WHERE ROUND((OPENINGSALEORDER_DESC.OPSO_MTRS - OPENINGSALEORDER_DESC.OPSO_RECDMTRS),0) > 0 AND OPENINGSALEORDER_DESC.OPSO_CLOSED=0  AND OPENINGSALEORDER.OPSO_YEARID = " & YearId & " GROUP BY ISNULL(LEDGERS.ACC_CMPNAME,''), ISNULL(ITEMMASTER.ITEM_NAME,''), ISNULL(DESIGNMASTER.DESIGN_NO,''), ISNULL(COLORMASTER.COLOR_NAME,'')   ) AS T", "")
                Else
                    DT = OBJCMN.search("*", "", " (SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(ITEMMASTER.item_name,'') AS [ITEMNAME], SUM(SALEORDER_DESC.SO_MTRS - SALEORDER_DESC.SO_RECDMTRS) AS [PCS] FROM SALEORDER INNER JOIN LEDGERS ON SALEORDER.so_ledgerid = LEDGERS.Acc_id INNER JOIN SALEORDER_DESC ON SALEORDER.so_no = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID INNER JOIN ITEMMASTER ON SALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON SALEORDER_DESC.SO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON SALEORDER_DESC.SO_COLORID = COLOR_ID WHERE ROUND((SALEORDER_DESC.SO_MTRS - SALEORDER_DESC.SO_RECDMTRS),0) > 0 AND SALEORDER_DESC.SO_CLOSED=0  AND SALEORDER.so_YEARID = " & YearId & " GROUP BY ISNULL(LEDGERS.ACC_CMPNAME,''), ISNULL(ITEMMASTER.ITEM_NAME,'') UNION ALL  SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(ITEMMASTER.item_name,'') AS [ITEMNAME], SUM(OPENINGSALEORDER_DESC.OPSO_MTRS - OPENINGSALEORDER_DESC.OPSO_RECDMTRS) AS [PCS] FROM OPENINGSALEORDER INNER JOIN LEDGERS ON OPENINGSALEORDER.OPSO_ledgerid = LEDGERS.Acc_id INNER JOIN OPENINGSALEORDER_DESC ON OPENINGSALEORDER.OPSO_no = OPENINGSALEORDER_DESC.OPSO_NO AND OPENINGSALEORDER.OPSO_YEARID = OPENINGSALEORDER_DESC.OPSO_YEARID INNER JOIN ITEMMASTER ON OPENINGSALEORDER_DESC.OPSO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGSALEORDER_DESC.OPSO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON OPENINGSALEORDER_DESC.OPSO_COLORID = COLOR_ID WHERE ROUND((OPENINGSALEORDER_DESC.OPSO_MTRS - OPENINGSALEORDER_DESC.OPSO_RECDMTRS),0) > 0 AND OPENINGSALEORDER_DESC.OPSO_CLOSED=0  AND OPENINGSALEORDER.OPSO_YEARID = " & YearId & " GROUP BY ISNULL(LEDGERS.ACC_CMPNAME,''), ISNULL(ITEMMASTER.ITEM_NAME,'')) AS T", "")
                End If
            End If
            GRIDSODETAILS.DataSource = DT
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLPO()
        Try
            Dim DT As DataTable = OBJCMN.search("*", "", " (SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(ITEMMASTER.item_name,'') AS [ITEMNAME], SUM(PURCHASEORDER_DESC.PO_MTRS - PURCHASEORDER_DESC.PO_RECDMTRS) AS [MTRS] FROM PURCHASEORDER INNER JOIN LEDGERS ON PURCHASEORDER.PO_ledgerid = LEDGERS.Acc_id INNER JOIN PURCHASEORDER_DESC ON PURCHASEORDER.PO_no = PURCHASEORDER_DESC.PO_NO AND PURCHASEORDER.PO_YEARID = PURCHASEORDER_DESC.PO_YEARID INNER JOIN ITEMMASTER ON PURCHASEORDER_DESC.PO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON PURCHASEORDER_DESC.PO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON PURCHASEORDER_DESC.PO_COLORID = COLOR_ID WHERE (PURCHASEORDER_DESC.PO_MTRS - PURCHASEORDER_DESC.PO_RECDMTRS) > 0 AND PURCHASEORDER_DESC.PO_CLOSED=0  AND PURCHASEORDER.PO_YEARID = " & YearId & " GROUP BY ISNULL(LEDGERS.ACC_CMPNAME,''), ISNULL(ITEMMASTER.ITEM_NAME,'') UNION ALL  SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(ITEMMASTER.item_name,'') AS [ITEMNAME], SUM(OPENINGPURCHASEORDER_DESC.OPO_MTRS - OPENINGPURCHASEORDER_DESC.OPO_RECDMTRS) AS [MTRS] FROM OPENINGPURCHASEORDER INNER JOIN LEDGERS ON OPENINGPURCHASEORDER.OPO_ledgerid = LEDGERS.Acc_id INNER JOIN OPENINGPURCHASEORDER_DESC ON OPENINGPURCHASEORDER.OPO_no = OPENINGPURCHASEORDER_DESC.OPO_NO AND OPENINGPURCHASEORDER.OPO_YEARID = OPENINGPURCHASEORDER_DESC.OPO_YEARID INNER JOIN ITEMMASTER ON OPENINGPURCHASEORDER_DESC.OPO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGPURCHASEORDER_DESC.OPO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON OPENINGPURCHASEORDER_DESC.OPO_COLORID = COLOR_ID WHERE ROUND(OPENINGPURCHASEORDER_DESC.OPO_MTRS - OPENINGPURCHASEORDER_DESC.OPO_RECDMTRS,2) > 0 AND OPENINGPURCHASEORDER_DESC.OPO_CLOSED=0  AND OPENINGPURCHASEORDER.OPO_YEARID = " & YearId & " GROUP BY ISNULL(LEDGERS.ACC_CMPNAME,''), ISNULL(ITEMMASTER.ITEM_NAME,'')) AS T", "")
            GRIDPODETAILS.DataSource = DT
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLOVERDUE()
        Try
            Dim WHERECLAUSE As String = ""
            Dim DT As DataTable = OBJCMN.search("ISNULL(REM_DAYS,0) AS DAYS", "", "REMINDERDAYS ", WHERECLAUSE & " AND REMINDERDAYS.REM_CMPID =" & CmpId)
            If DT.Rows.Count > 0 Then WHERECLAUSE = " AND T.DUEDATE <= '" & Format(DateAdd(DateInterval.Day, Val(DT.Rows(0).Item("DAYS")), Mydate), "MM/dd/yyyy") & "' " Else WHERECLAUSE = " AND T.DUEDATE <= '" & Format(DateAdd(DateInterval.Day, 1, Mydate), "MM/dd/yyyy") & "' "
            DT = OBJCMN.search("*", "", " (SELECT LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME, INVOICE_INITIALS AS INITIALS, INVOICE_DATE AS DATE , INVOICE_BALANCE AS BALANCE , INVOICE_DUEDATE AS DUEDATE, INVOICE_NO AS BILLNO, REGISTERMASTER.REGISTER_NAME AS REGNAME, 'SALE' AS TYPE, ISNULL(TOCITYMASTER.CITY_NAME,'') AS TOCITY, DATEDIFF(DAY,INVOICEMASTER.INVOICE_DUEDATE, GETDATE()) AS OVERDUEDAYS FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID INNER JOIN LEDGERS ON LEDGERS.Acc_id = INVOICE_LEDGERID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON AGENTLEDGERS.ACC_ID = INVOICEMASTER.INVOICE_AGENTID LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON INVOICEMASTER.INVOICE_TOCITYID = TOCITYMASTER.CITY_ID WHERE INVOICE_YEARID = " & YearId & " AND INVOICE_BALANCE > 0 UNION ALL SELECT LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME , ISNULL(CITY_NAME,'') AS CITYNAME, BILL_INITIALS AS INITIALS, BILL_DATE AS DATE , BILL_BALANCE AS BALANCE , BILL_DUEDATE AS DUEDATE, BILL_NO AS BILLNO, REGISTERMASTER.REGISTER_NAME AS REGNAME, 'OPENING' AS TYPE, '' AS TOCITY, DATEDIFF(DAY,BILL_DUEDATE , GETDATE()) AS OVERDUEDAYS FROM OPENINGBILL INNER JOIN REGISTERMASTER ON REGISTER_ID = OPENINGBILL.BILL_REGISTERID INNER JOIN LEDGERS ON LEDGERS.Acc_id = BILL_LEDGERID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON AGENTLEDGERS.ACC_ID = OPENINGBILL.BILL_AGENTID LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITY_ID WHERE BILL_YEARID = " & YearId & " AND BILL_TYPE ='SALE' AND BILL_BALANCE > 0) AS T ", WHERECLAUSE & " ORDER BY T.NAME, T.DUEDATE, T.BILLNO")
            GRIDOVERDUEDETAILS.DataSource = DT
            If ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                GOCITY.Visible = False
                GTOCITY.Visible = True
                GTOCITY.VisibleIndex = GAGENTNAME.VisibleIndex + 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SHOWPAYFORM()
        Try
            If GRIDPAY.RowCount > 0 Then
                Dim dtrow As DataRow = GRIDPAY.GetFocusedDataRow
                Dim NAME As String = dtrow("NAME")
                If NAME <> "" Then
                    If ClientName = "YASHVI" Then
                        Dim OBJOUTSTAND As New OutstandingDesign
                        OBJOUTSTAND.MdiParent = MDIMain
                        OBJOUTSTAND.selfor_ss = " {@YEARID}=" & YearId
                        getFromToDate()
                        OBJOUTSTAND.PERIOD = Format(AccFrom.Date, "dd/MM/yyyy") & " - " & Format(Now.Date, "dd/MM/yyyy")
                        OBJOUTSTAND.TODATE = Format(Now.Date, "dd/MM/yyyy")
                        OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DATE} <= #" & Format(Now.Date, "MM/dd/yyyy") & "# and {OUTSTANDINGREPORT_DETAILS.BALANCE}>0 and {@NAME}='" & NAME & "'"
                        OBJOUTSTAND.FRMSTRING = "OUTSTANDINGPAYDTLS"
                        OBJOUTSTAND.DAYS = "DUEDATE"
                        OBJOUTSTAND.ADDRESS = 0
                        OBJOUTSTAND.SHOWPRINTDATE = 1
                        OBJOUTSTAND.SHOWREMARKS = 0
                        OBJOUTSTAND.Show()
                    Else
                        Dim objlb As New RegisterDetails
                        objlb.TEMPNAME = NAME
                        objlb.MdiParent = MDIMain
                        objlb.Show()
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, AccFrom.Date)
        a2 = DatePart(DateInterval.Month, AccFrom.Date)
        a3 = DatePart(DateInterval.Year, AccFrom.Date)
        fromD = "(" & a3 & "," & a2 & "," & a1 & ")"

        a11 = DatePart(DateInterval.Day, Now.Date)
        a12 = DatePart(DateInterval.Month, Now.Date)
        a13 = DatePart(DateInterval.Year, Now.Date)
        toD = "(" & a13 & "," & a12 & "," & a11 & ")"
    End Sub

    Sub SHOWRECFORM()
        Try
            If GRIDREC.RowCount > 0 Then
                Dim dtrow As DataRow = GRIDREC.GetFocusedDataRow
                Dim NAME As String = dtrow("NAME")
                If NAME <> "" Then
                    'OPEN OUTSTANDING
                    If ClientName = "YASHVI" Then
                        Dim OBJOUTSTAND As New OutstandingDesign
                        OBJOUTSTAND.MdiParent = MDIMain
                        OBJOUTSTAND.selfor_ss = " {@YEARID}=" & YearId
                        getFromToDate()
                        OBJOUTSTAND.PERIOD = Format(AccFrom.Date, "dd/MM/yyyy") & " - " & Format(Now.Date, "dd/MM/yyyy")
                        OBJOUTSTAND.TODATE = Format(Now.Date, "dd/MM/yyyy")
                        OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DATE} <= #" & Format(Now.Date, "MM/dd/yyyy") & "# and {OUTSTANDINGREPORT_DETAILS.BALANCE}>0 and {@NAME}='" & NAME & "'"
                        OBJOUTSTAND.FRMSTRING = "OUTSTANDINGRECDTLS"
                        OBJOUTSTAND.DAYS = "DUEDATE"
                        OBJOUTSTAND.ADDRESS = 0
                        OBJOUTSTAND.SHOWPRINTDATE = 1
                        OBJOUTSTAND.SHOWREMARKS = 0
                        OBJOUTSTAND.Show()

                    ElseIf ClientName = "AVIS" Then
                        Dim OBJOUTSTAND As New RecOutstanding
                        OBJOUTSTAND.MdiParent = MDIMain
                        OBJOUTSTAND.PARTYNAME = NAME
                        OBJOUTSTAND.Show()

                    Else
                        Dim objlb As New RegisterDetails
                        objlb.TEMPNAME = NAME
                        objlb.MdiParent = MDIMain
                        objlb.Show()
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SHOWSTOCKDETAILS()
        Try
            If GRIDSTOCK.RowCount > 0 Then
                Dim dtrow As DataRow = GRIDSTOCK.GetFocusedDataRow
                Dim ITEMNAME As String = dtrow("ITEMNAME")
                Dim DESIGNNO As String = ""
                If GDESIGN.Visible = True Then DESIGNNO = dtrow("DESIGNNO")
                If ITEMNAME <> "" Then
                    If ALLOWBARCODEPRINT = True Then

                        If ClientName = "SUPEEMA" Then
                            Dim OBJSTOCK As New StockDesign
                            OBJSTOCK.WHERECLAUSE = " {BARCODESTOCK.YEARID}=" & YearId & " AND {BARCODESTOCK.ITEMNAME} = '" & ITEMNAME & "'"

                            Dim TEMPUNIT As String = ""
                            Dim DTUNIT As DataTable = OBJCMN.search("UNIT_ABBR", "", "DEFAULTSTOCKUNIT", "")
                            For Each DRUNIT As DataRow In DTUNIT.Rows
                                If TEMPUNIT = "" Then
                                    TEMPUNIT = " AND ({BARCODESTOCK.UNIT} = '" & DRUNIT("UNIT_ABBR") & "'"
                                Else
                                    TEMPUNIT = TEMPUNIT & " OR {BARCODESTOCK.UNIT} = '" & DRUNIT("UNIT_ABBR") & "'"
                                End If
                            Next
                            If TEMPUNIT <> "" Then TEMPUNIT = TEMPUNIT & ")"

                            OBJSTOCK.WHERECLAUSE = OBJSTOCK.WHERECLAUSE & TEMPUNIT

                            OBJSTOCK.FRMSTRING = "ITEMDESIGNSHADESTOCKNOUNITSMALLSUMM"
                            OBJSTOCK.MdiParent = MDIMain
                            OBJSTOCK.Show()
                        Else

                            If ClientName = "SOFTAS" Then
                                Dim OBJSTOCK As New StockOnHandWithPacking
                                OBJSTOCK.TEMPITEMNAME = ITEMNAME
                                If GDESIGN.Visible = True Then OBJSTOCK.TEMPDESIGNNO = DESIGNNO
                                OBJSTOCK.MdiParent = MDIMain
                                OBJSTOCK.Show()
                            Else
                                Dim OBJSTOCK As New GodownwiseDetails
                                OBJSTOCK.TEMPITEMNAME = ITEMNAME
                                If GDESIGN.Visible = True Then OBJSTOCK.TEMPDESIGNNO = DESIGNNO
                                OBJSTOCK.MdiParent = MDIMain
                                OBJSTOCK.Show()
                            End If

                        End If

                    Else
                        Dim OBJSTOCK As New StockDesign
                        OBJSTOCK.MdiParent = MDIMain
                        OBJSTOCK.WHERECLAUSE = " {STOCKREGISTER.YEARID}=" & YearId & " And {STOCKREGISTER.ITEMNAME}='" & ITEMNAME & "'"
                        OBJSTOCK.FRMSTRING = "ITEMSTOCKSUMM"
                        OBJSTOCK.FROMDATE = AccFrom.Date
                        OBJSTOCK.TODATE = AccTo.Date
                        OBJSTOCK.Show()
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDREC_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRIDREC.DoubleClick
        Try
            SHOWRECFORM()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDPAY_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRIDPAY.DoubleClick
        Try
            SHOWPAYFORM()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSTOCK_DoubleClick(sender As Object, e As EventArgs) Handles GRIDSTOCK.DoubleClick
        Try
            SHOWSTOCKDETAILS()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDOVERDUE_DoubleClick(sender As Object, e As EventArgs) Handles GRIDOVERDUE.DoubleClick
        Try
            If GRIDOVERDUE.GetFocusedRowCellValue("TYPE") = "OPENING" Then Exit Sub
            Dim OBJBILL As New InvoiceMaster
            OBJBILL.MdiParent = MDIMain
            OBJBILL.EDIT = True
            OBJBILL.TEMPINVOICENO = GRIDOVERDUE.GetFocusedRowCellValue("BILLNO")
            OBJBILL.TEMPREGNAME = GRIDOVERDUE.GetFocusedRowCellValue("REGNAME")
            OBJBILL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSO_DoubleClick(sender As Object, e As EventArgs) Handles GRIDSO.DoubleClick
        Try
            SHOWSOPO("SO")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDPO_DoubleClick(sender As Object, e As EventArgs) Handles GRIDPO.DoubleClick
        Try
            SHOWSOPO("PO")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SHOWSOPO(FRMSTRING As String)
        Try
            Dim OBJGRN As New SaleInvoiceDesign
            OBJGRN.MdiParent = MDIMain
            OBJGRN.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            OBJGRN.PENDINGSO = "PENDING"
            OBJGRN.POSOFRMSTRING = FRMSTRING

            If FRMSTRING = "SO" Then
                OBJGRN.WHERECLAUSE = " {ALLSALEORDER.SO_yearid}=" & YearId
                OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {LEDGERS.ACC_CMPNAME}='" & GRIDSO.GetFocusedRowCellValue("NAME") & "'"
                OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {ITEMMASTER.ITEM_NAME}='" & GRIDSO.GetFocusedRowCellValue("ITEMNAME") & "'"
                OBJGRN.FRMSTRING = "SOSTATUS"
                OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {@BALANCE} > 0 and {ALLSALEORDER_DESC.SO_Closed}=FALSE "
            Else
                OBJGRN.WHERECLAUSE = " {ALLPURCHASEORDER.PO_yearid}=" & YearId
                OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {LEDGERS.ACC_CMPNAME}='" & GRIDPO.GetFocusedRowCellValue("NAME") & "'"
                OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {ITEMMASTER.ITEM_NAME}='" & GRIDPO.GetFocusedRowCellValue("ITEMNAME") & "'"
                OBJGRN.FRMSTRING = "POSTATUS"
                OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {@BALANCE} > 0 and {ALLPURCHASEORDER_DESC.PO_Closed}=FALSE "
            End If

            OBJGRN.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HomePage_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "SONU" Then
                GRIDREC.OptionsView.ShowFooter = True
                GRIDPAY.OptionsView.ShowFooter = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class