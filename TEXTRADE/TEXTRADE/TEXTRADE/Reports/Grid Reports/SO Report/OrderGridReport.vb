
Imports System.ComponentModel
Imports System.IO
Imports BL
Imports iTextSharp.text.pdf

Public Class OrderGridReport

    Public NAME As String
    Public SOCLAUSE As String
    Public ORDERTYPE As String
    Public FRMSTRING As String


    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OrderGridReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FILLCMB()
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If NAME <> "" Then
                CMBNAME.Text = NAME
            End If
            If FRMSTRING = "SO" Then
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, False, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If
            'If CMBAGENT.Text.Trim = "" Then FILLNAME(CMBAGENT, False, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND ACC_TYPE='AGENT'")


            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If RBACCOUNT.Checked = True Then
                gridbill.Columns("NAME").Visible = True
                If FRMSTRING = "SO" Then
                    DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, GROUPMASTER.GROUP_NAME AS GROUPNAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA, ISNULL(SALESMANMASTER.SALESMAN_NAME,'') AS SALESMAN ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.ACC_AREAID = AREAMASTER.AREA_ID LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.ACC_ID  ", " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
                ElseIf FRMSTRING <> Nothing Then
                    DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, GROUPMASTER.GROUP_NAME AS GROUPNAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA, ISNULL(SALESMANMASTER.SALESMAN_NAME,'') AS SALESMAN ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.ACC_AREAID = AREAMASTER.AREA_ID LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.ACC_ID  ", " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
                End If
            ElseIf RBAGENT.Checked = True Then
                gridbill.Columns("NAME").Visible = False
                DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, GROUPMASTER.GROUP_NAME AS GROUPNAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA ", " ", " LEDGERS AS AGENTLEDGERS INNER JOIN GROUPMASTER ON AGENTLEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON AGENTLEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON AGENTLEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON AGENTLEDGERS.ACC_AREAID = AREAMASTER.AREA_ID ", " AND AGENTLEDGERS.ACC_TYPE = 'AGENT' AND (AGENTLEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY AGENTLEDGERS.Acc_cmpname")
            End If

            If DT.Rows.Count > 0 Then gridbill.FocusedRowHandle = gridbill.RowCount - 1
            gridbilldetails.DataSource = DT


            Dim DTITEM As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY ", " ", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.ITEM_CATEGORYID = CATEGORYMASTER.CATEGORY_ID", " AND ITEMMASTER.ITEM_YEARID = '" & YearId & "' ORDER BY ITEMMASTER.ITEM_NAME")
            If DTITEM.Rows.Count > 0 Then GRIDBILLITEM.FocusedRowHandle = GRIDBILLITEM.RowCount - 1
            GRIDBILLDETAILSITEM.DataSource = DTITEM



            If FRMSTRING = "SO" Then
                DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ALLSALEORDER.SO_NO AS ORDERNO ", " ", " ALLSALEORDER ", " AND ALLSALEORDER.SO_YEARID = " & YearId & " ORDER BY ALLSALEORDER.SO_NO ")
            Else
                DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ALLPURCHASEORDER.PO_NO AS ORDERNO ", " ", " ALLPURCHASEORDER ", " AND ALLPURCHASEORDER.PO_YEARID = " & YearId & " ORDER BY ALLPURCHASEORDER.PO_NO ")
            End If
            If DT.Rows.Count > 0 Then GRIDBILLORDER.FocusedRowHandle = GRIDBILLORDER.RowCount - 1
            GRIDBILLDETAILSORDER.DataSource = DT

            If ClientName = "MASHOK" Then DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, MILLMASTER.MILL_NAME AS MILLNAME ", " ", " MILLMASTER ", " AND MILLMASTER.MILL_YEARID = " & YearId & " ORDER BY MILLMASTER.MILL_NAME ")
            If DT.Rows.Count > 0 Then GRIDMILL.FocusedRowHandle = GRIDMILL.RowCount - 1
            GRIDMILLDETAILS.DataSource = DT


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub FILLGRID()
        Try

            Dim MILLNAMECLAUSE As String = ""
            Dim NAMECLAUSE As String = ""
            Dim ITEMCLAUSE As String = ""
            Dim ORDERCLAUSE As String = ""
            SOCLAUSE = " AND 1=1 "

            If CMBORDERTYPE.Text = "YARN" Then ORDERTYPE = "YARN" & FRMSTRING Else ORDERTYPE = FRMSTRING
            If ORDERTYPE = "YARNSO" Or ORDERTYPE = "YARNPO" Then GMILLNAME.Visible = True Else GMILLNAME.Visible = False


            If CMBNAME.Text <> "" Then SOCLAUSE = SOCLAUSE & " and LEDGERS.ACC_CMPNAME='" & CMBNAME.Text.Trim & "'"
            If CMBAGENT.Text <> "" Then SOCLAUSE = SOCLAUSE & " and agentledgers.ACC_CMPNAME='" & CMBAGENT.Text.Trim & "'"
            If CMBCATEGORY.Text <> "" Then SOCLAUSE = SOCLAUSE & " AND ITEMMASTER.ITEM_CATEGORYID = (SELECT CATEGORY_ID FROM CATEGORYMASTER WHERE CATEGORY_NAME = '" & CMBCATEGORY.Text.Trim & "'AND category_yearid=" & YearId & ")"
            If chkdate.Checked = True Then
                If ORDERTYPE = "SO" Then
                    SOCLAUSE &= " AND ALLSALEORDER.so_date BETWEEN '" & Format(dtfrom.Value.Date, "YYYY-MM-dd") & "' AND '" & Format(dtto.Value.Date, "YYYY-MM-dd") & "'"
                ElseIf ORDERTYPE = "PO" Then
                    SOCLAUSE &= " AND ALLPURCHASEORDER.Po_date BETWEEN '" & Format(dtfrom.Value.Date, "YYYY-MM-dd") & "' AND '" & Format(dtto.Value.Date, "YYYY-MM-dd") & "'"
                ElseIf ORDERTYPE = "YARNSO" Then
                    SOCLAUSE &= " AND ALLYARNSALEORDER.Yso_date BETWEEN '" & Format(dtfrom.Value.Date, "YYYY-MM-dd") & "' AND '" & Format(dtto.Value.Date, "YYYY-MM-dd") & "'"
                ElseIf ORDERTYPE = "YARNPO" Then
                    SOCLAUSE &= " AND ALLYARNPURCHASEORDER.YPo_date BETWEEN '" & Format(dtfrom.Value.Date, "YYYY-MM-dd") & "' AND '" & Format(dtto.Value.Date, "YYYY-MM-dd") & "'"
                End If
            End If




            'FOR NAME
            gridbill.ClearColumnsFilter()
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If NAMECLAUSE = "" Then
                        If RBACCOUNT.Checked = True Then
                            NAMECLAUSE = " AND (LEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                        ElseIf RBAGENT.Checked = True Then
                            NAMECLAUSE = " AND (AGENTLEDGERS.ACC_CMPNAME = '" & dtrow("AGENTNAME") & "'"
                        End If
                    Else
                        If RBACCOUNT.Checked = True Then
                            NAMECLAUSE = NAMECLAUSE & " OR LEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                        ElseIf RBAGENT.Checked = True Then
                            NAMECLAUSE = NAMECLAUSE & " OR AGENTLEDGERS.ACC_CMPNAME = '" & dtrow("AGENTNAME") & "'"
                        End If
                    End If
                End If
            Next
            If NAMECLAUSE <> "" Then
                NAMECLAUSE = NAMECLAUSE & ")"
                SOCLAUSE = SOCLAUSE & NAMECLAUSE
            End If


            'FOR ITEMNAME
            GRIDBILLITEM.ClearColumnsFilter()
            For i As Integer = 0 To GRIDBILLITEM.RowCount - 1
                Dim dtrow As DataRow = GRIDBILLITEM.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If ITEMCLAUSE = "" Then
                        ITEMCLAUSE = " AND (ITEMMASTER.ITEM_NAME = '" & dtrow("ITEMNAME") & "'"
                    Else
                        ITEMCLAUSE = ITEMCLAUSE & " OR ITEMMASTER.ITEM_NAME = '" & dtrow("ITEMNAME") & "'"
                    End If
                End If
            Next
            If ITEMCLAUSE <> "" Then
                ITEMCLAUSE = ITEMCLAUSE & ")"
                SOCLAUSE = SOCLAUSE & ITEMCLAUSE
            End If




            If CMBORDERTYPE.Text = "YARN" Then
                'FOR MILLNAME
                GRIDMILL.ClearColumnsFilter()
                For i As Integer = 0 To GRIDMILL.RowCount - 1
                    Dim dtrow As DataRow = GRIDMILL.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If MILLNAMECLAUSE = "" Then
                            MILLNAMECLAUSE = " AND (MILLMASTER.MILL_NAME = '" & dtrow("MILLNAME") & "'"
                        Else
                            MILLNAMECLAUSE = MILLNAMECLAUSE & " OR MILLMASTER.MILL_NAME = '" & dtrow("MILLNAME") & "'"
                        End If
                    End If
                Next
                If MILLNAMECLAUSE <> "" Then
                    MILLNAMECLAUSE = MILLNAMECLAUSE & ")"
                    SOCLAUSE = SOCLAUSE & MILLNAMECLAUSE
                End If
            End If




            If FRMSTRING = "SO" Then
                If CMBORDERTYPE.Text = "YARN" Then
                    If RDBPENDING.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLYARNSALEORDER_DESC.BALANCE > 0 AND ALLYARNSALEORDER_DESC.YSO_CLOSED='FALSE' "
                    If RDBCOMPLETE.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLYARNSALEORDER_DESC.BALANCE <= 0 AND ALLYARNSALEORDER_DESC.YSO_CLOSED='FALSE'"
                    If RDBCLOSED.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLYARNSALEORDER_DESC.YSO_CLOSED='TRUE' "
                Else
                    If RDBPENDING.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLSALEORDER_DESC.BALANCE > 0 AND ALLSALEORDER_DESC.SO_CLOSED='FALSE' "
                    If RDBCOMPLETE.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLSALEORDER_DESC.BALANCE <= 0 AND ALLSALEORDER_DESC.SO_CLOSED='FALSE'"
                    If RDBCLOSED.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLSALEORDER_DESC.SO_CLOSED='TRUE' "
                End If

            ElseIf FRMSTRING = "PO" Then
                If CMBORDERTYPE.Text = "YARN" Then
                    If RDBPENDING.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLYARNPURCHASEORDER_DESC.BALANCE > 0 AND ALLYARNPURCHASEORDER_DESC.YPO_CLOSED='FALSE' "
                    If RDBCOMPLETE.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLYARNPURCHASEORDER_DESC.BALANCE <= 0 AND ALLYARNPURCHASEORDER_DESC.YPO_CLOSED='FALSE'"
                    If RDBCLOSED.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLYARNPURCHASEORDER_DESC.YPO_CLOSED='TRUE' "
                Else
                    If RDBPENDING.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLPURCHASEORDER_DESC.BALANCE > 0 AND ALLPURCHASEORDER_DESC.PO_CLOSED='FALSE' "
                    If RDBCOMPLETE.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLPURCHASEORDER_DESC.BALANCE <= 0 AND ALLPURCHASEORDER_DESC.PO_CLOSED='FALSE'"
                    If RDBCLOSED.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLPURCHASEORDER_DESC.PO_CLOSED='TRUE' "
                End If
            End If



            GRIDSO.RowCount = 0
            Dim OBJCMN As New ClsCommon
            Dim LASTITEMNAME As String = ""
            Dim TOTALPCS, TOTALDELPCS, TOTALBALPCS As Double
            Dim GTOTALPCS, GTOTALDELPCS, GTOTALBALPCS As Double
            Dim COMPLETIONDAYS As Integer = 0
            Dim DT As New DataTable


            If SALEORDERONMTRS = True Then
                If ORDERTYPE = "SO" Then
                    DT = OBJCMN.SEARCH(" ITEMMASTER.item_name AS ITEMNAME, '' AS MILLNAME, ALLSALEORDER.so_no AS SONO, ALLSALEORDER.so_date AS SODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname,'') AS AGENTNAME, ALLSALEORDER.so_NOTE AS NOTE, ALLSALEORDER_DESC.SO_MTRS AS PCS, (CASE WHEN '" & ClientName & "' = 'ABHEE' AND ALLSALEORDER.SO_ORDERON = 'PCS' THEN ALLSALEORDER_DESC.SO_RECDQTY ELSE ALLSALEORDER_DESC.SO_RECDMTRS END) AS OUTPCS, ALLSALEORDER_DESC.BALANCE AS BALPCS, ALLSALEORDER_DESC.SO_RATE AS RATE, SO_DAYS AS [DAYS], ISNULL(ITEMMASTER.ITEM_REORDER,0) AS PERDAYPROD ", "", " ALLSALEORDER INNER JOIN ALLSALEORDER_DESC ON ALLSALEORDER.so_no = ALLSALEORDER_DESC.SO_NO AND ALLSALEORDER.TYPE = ALLSALEORDER_DESC.TYPE AND ALLSALEORDER.SO_YEARID = ALLSALEORDER_DESC.SO_YEARID INNER JOIN ITEMMASTER ON ALLSALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id INNER JOIN LEDGERS ON ALLSALEORDER.so_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON ALLSALEORDER.so_Agentid = AGENTLEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON ALLSALEORDER_DESC.SO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON ALLSALEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_ID", " AND ALLSALEORDER.SO_YEARID =" & YearId & SOCLAUSE & " ORDER BY ITEMMASTER.item_name, ALLSALEORDER.SO_DATE, ALLSALEORDER.SO_NO")
                ElseIf ORDERTYPE = "YARNSO" Then
                    DT = OBJCMN.SEARCH(" YARNQUALITYMASTER.YARN_NAME AS ITEMNAME, ISNULL(MILLMASTER.MILL_NAME,'') AS MILLNAME, ALLYARNSALEORDER.YSO_NO AS SONO, ALLYARNSALEORDER.YSO_DATE AS SODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, ALLYARNSALEORDER.YSO_NOTE AS NOTE, ALLYARNSALEORDER_DESC.YSO_WT AS PCS, ALLYARNSALEORDER_DESC.YSO_RECDWT AS OUTPCS, ALLYARNSALEORDER_DESC.BALANCE AS BALPCS, ALLYARNSALEORDER_DESC.YSO_RATE AS RATE, ALLYARNSALEORDER.YSO_CRDAYS AS DAYS, 0 AS PERDAYPROD   ", "", " ALLYARNSALEORDER INNER JOIN ALLYARNSALEORDER_DESC ON ALLYARNSALEORDER.YSO_NO = ALLYARNSALEORDER_DESC.YSO_NO AND ALLYARNSALEORDER.YSO_YEARID = ALLYARNSALEORDER_DESC.YSO_YEARID INNER JOIN YARNQUALITYMASTER ON ALLYARNSALEORDER_DESC.YSO_YARNQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN MILLMASTER ON ALLYARNSALEORDER_DESC.YSO_MILLID = MILLMASTER.MILL_ID INNER JOIN LEDGERS ON ALLYARNSALEORDER.YSO_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON ALLYARNSALEORDER.YSO_BROKERID = AGENTLEDGERS.ACC_ID ", " AND ALLYARNSALEORDER.YSO_YEARID =" & YearId & SOCLAUSE & " ORDER BY YARNQUALITYMASTER.YARN_NAME, ALLYARNSALEORDER.YSO_DATE, ALLYARNSALEORDER.YSO_NO")
                ElseIf ORDERTYPE = "PO" Then
                    DT = OBJCMN.SEARCH(" ITEMMASTER.item_name AS ITEMNAME, '' AS MILLNAME, ALLPURCHASEORDER.PO_no AS SONO, ALLPURCHASEORDER.PO_date AS SODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname,'') AS AGENTNAME, ALLPURCHASEORDER.PO_REMARKS AS NOTE, ALLPURCHASEORDER_DESC.PO_MTRS AS PCS, (CASE WHEN '" & ClientName & "' = 'ABHEE' AND ALLPURCHASEORDER.PO_ORDERON = 'PCS' THEN ALLPURCHASEORDER_DESC.PO_RECDQTY ELSE ALLPURCHASEORDER_DESC.PO_RECDMTRS END) AS OUTPCS, ALLPURCHASEORDER_DESC.BALANCE AS BALPCS, ALLPURCHASEORDER_DESC.PO_RATE AS RATE, PO_CRDAYS AS [DAYS], ISNULL(ITEMMASTER.ITEM_REORDER,0) AS PERDAYPROD ", "", " ALLPURCHASEORDER INNER JOIN ALLPURCHASEORDER_DESC ON ALLPURCHASEORDER.PO_no = ALLPURCHASEORDER_DESC.PO_NO AND ALLPURCHASEORDER.TYPE = ALLPURCHASEORDER_DESC.TYPE AND ALLPURCHASEORDER.PO_YEARID = ALLPURCHASEORDER_DESC.PO_YEARID INNER JOIN ITEMMASTER ON ALLPURCHASEORDER_DESC.PO_ITEMID = ITEMMASTER.item_id INNER JOIN LEDGERS ON ALLPURCHASEORDER.PO_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON ALLPURCHASEORDER.PO_BROKERID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON ALLPURCHASEORDER_DESC.PO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON ALLPURCHASEORDER_DESC.PO_COLORID = COLORMASTER.COLOR_ID", " AND ALLPURCHASEORDER.PO_YEARID =" & YearId & SOCLAUSE & " ORDER BY ITEMMASTER.item_name, ALLPURCHASEORDER.PO_DATE, ALLPURCHASEORDER.PO_NO")
                ElseIf ORDERTYPE = "YARNPO" Then
                    DT = OBJCMN.SEARCH(" YARNQUALITYMASTER.YARN_NAME AS ITEMNAME, ISNULL(MILLMASTER.MILL_NAME,'') AS MILLNAME, ALLYARNPURCHASEORDER.YPO_NO AS SONO, ALLYARNPURCHASEORDER.YPO_DATE AS SODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, ALLYARNPURCHASEORDER.YPO_REMARKS AS NOTE, ALLYARNPURCHASEORDER_DESC.YPO_WT AS PCS, ALLYARNPURCHASEORDER_DESC.YPO_RECDWT AS OUTPCS, ALLYARNPURCHASEORDER_DESC.BALANCE AS BALPCS, ALLYARNPURCHASEORDER_DESC.YPO_RATE AS RATE, ALLYARNPURCHASEORDER.YPO_CRDAYS AS DAYS, 0 AS PERDAYPROD ", "", " ALLYARNPURCHASEORDER INNER JOIN ALLYARNPURCHASEORDER_DESC ON ALLYARNPURCHASEORDER.YPO_NO = ALLYARNPURCHASEORDER_DESC.YPO_NO AND ALLYARNPURCHASEORDER.YPO_YEARID = ALLYARNPURCHASEORDER_DESC.YPO_YEARID INNER JOIN YARNQUALITYMASTER ON ALLYARNPURCHASEORDER_DESC.YPO_YARNQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN MILLMASTER ON ALLYARNPURCHASEORDER_DESC.YPO_MILLID = MILLMASTER.MILL_ID INNER JOIN LEDGERS ON ALLYARNPURCHASEORDER.YPO_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON ALLYARNPURCHASEORDER.YPO_BROKERID = AGENTLEDGERS.ACC_ID  ", " AND ALLYARNPURCHASEORDER.YPO_YEARID =" & YearId & SOCLAUSE & " ORDER BY YARNQUALITYMASTER.YARN_NAME, ALLYARNPURCHASEORDER.YPO_DATE, ALLYARNPURCHASEORDER.YPO_NO")
                End If
            Else
                If ORDERTYPE = "SO" Then
                    DT = OBJCMN.SEARCH(" ITEMMASTER.item_name AS ITEMNAME, '' AS MILLNAME, ALLSALEORDER.so_no AS SONO, ALLSALEORDER.so_date AS SODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname,'') AS AGENTNAME, ALLSALEORDER.so_NOTE AS NOTE, ALLSALEORDER_DESC.SO_QTY AS PCS, ALLSALEORDER_DESC.SO_RECDQTY AS OUTPCS, ALLSALEORDER_DESC.BALANCE AS BALPCS, ALLSALEORDER_DESC.SO_RATE AS RATE, SO_DAYS AS [DAYS], ISNULL(ITEMMASTER.ITEM_REORDER,0) AS PERDAYPROD ", "", " ALLSALEORDER INNER JOIN ALLSALEORDER_DESC ON ALLSALEORDER.so_no = ALLSALEORDER_DESC.SO_NO AND ALLSALEORDER.TYPE = ALLSALEORDER_DESC.TYPE AND ALLSALEORDER.SO_YEARID = ALLSALEORDER_DESC.SO_YEARID INNER JOIN ITEMMASTER ON ALLSALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id INNER JOIN LEDGERS ON ALLSALEORDER.so_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON ALLSALEORDER.so_Agentid = AGENTLEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON ALLSALEORDER_DESC.SO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON ALLSALEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_ID", " AND ALLSALEORDER.SO_YEARID =" & YearId & SOCLAUSE & " ORDER BY ITEMMASTER.item_name, ALLSALEORDER.SO_DATE, ALLSALEORDER.SO_NO")
                ElseIf ORDERTYPE = "YARNSO" Then
                    DT = OBJCMN.SEARCH(" YARNQUALITYMASTER.YARN_NAME AS ITEMNAME, ISNULL(MILLMASTER.MILL_NAME,'') AS MILLNAME, ALLYARNSALEORDER.YSO_NO AS SONO, ALLYARNSALEORDER.YSO_DATE AS SODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, ALLYARNSALEORDER.YSO_NOTE AS NOTE, ALLYARNSALEORDER_DESC.YSO_BAGS AS PCS, ALLYARNSALEORDER_DESC.YSO_RECDBAGS AS OUTPCS, ALLYARNSALEORDER_DESC.BALANCE AS BALPCS, ALLYARNSALEORDER_DESC.YSO_RATE AS RATE, ALLYARNSALEORDER.YSO_CRDAYS AS DAYS, 0 AS PERDAYPROD   ", "", " ALLYARNSALEORDER INNER JOIN ALLYARNSALEORDER_DESC ON ALLYARNSALEORDER.YSO_NO = ALLYARNSALEORDER_DESC.YSO_NO AND ALLYARNSALEORDER.YSO_YEARID = ALLYARNSALEORDER_DESC.YSO_YEARID INNER JOIN YARNQUALITYMASTER ON ALLYARNSALEORDER_DESC.YSO_YARNQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN MILLMASTER ON ALLYARNSALEORDER_DESC.YSO_MILLID = MILLMASTER.MILL_ID INNER JOIN LEDGERS ON ALLYARNSALEORDER.YSO_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON ALLYARNSALEORDER.YSO_BROKERID = AGENTLEDGERS.ACC_ID ", " AND ALLYARNSALEORDER.YSO_YEARID =" & YearId & SOCLAUSE & " ORDER BY YARNQUALITYMASTER.YARN_NAME, ALLYARNSALEORDER.YSO_DATE, ALLYARNSALEORDER.YSO_NO")
                ElseIf ORDERTYPE = "PO" Then
                    DT = OBJCMN.SEARCH(" ITEMMASTER.item_name AS ITEMNAME, '' AS MILLNAME, ALLPURCHASEORDER.PO_no AS SONO, ALLPURCHASEORDER.PO_date AS SODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname,'') AS AGENTNAME, ALLPURCHASEORDER.PO_REMARKS AS NOTE, ALLPURCHASEORDER_DESC.PO_QTY AS PCS, ALLPURCHASEORDER_DESC.PO_RECDQTY AS OUTPCS, ALLPURCHASEORDER_DESC.BALANCE AS BALPCS, ALLPURCHASEORDER_DESC.PO_RATE AS RATE, PO_CRDAYS AS [DAYS], ISNULL(ITEMMASTER.ITEM_REORDER,0) AS PERDAYPROD ", "", " ALLPURCHASEORDER INNER JOIN ALLPURCHASEORDER_DESC ON ALLPURCHASEORDER.PO_no = ALLPURCHASEORDER_DESC.PO_NO AND ALLPURCHASEORDER.TYPE = ALLPURCHASEORDER_DESC.TYPE AND ALLPURCHASEORDER.PO_YEARID = ALLPURCHASEORDER_DESC.PO_YEARID INNER JOIN ITEMMASTER ON ALLPURCHASEORDER_DESC.PO_ITEMID = ITEMMASTER.item_id INNER JOIN LEDGERS ON ALLPURCHASEORDER.PO_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON ALLPURCHASEORDER.PO_BROKERID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON ALLPURCHASEORDER_DESC.PO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON ALLPURCHASEORDER_DESC.PO_COLORID = COLORMASTER.COLOR_ID", " AND ALLPURCHASEORDER.PO_YEARID =" & YearId & SOCLAUSE & " ORDER BY ITEMMASTER.item_name, ALLPURCHASEORDER.PO_DATE, ALLPURCHASEORDER.PO_NO")
                ElseIf ORDERTYPE = "YARNPO" Then
                    DT = OBJCMN.SEARCH(" YARNQUALITYMASTER.YARN_NAME AS ITEMNAME, ISNULL(MILLMASTER.MILL_NAME,'') AS MILLNAME, ALLYARNPURCHASEORDER.YPO_NO AS SONO, ALLYARNPURCHASEORDER.YPO_DATE AS SODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, ALLYARNPURCHASEORDER.YPO_REMARKS AS NOTE, ALLYARNPURCHASEORDER_DESC.YPO_BAGS AS PCS, ALLYARNPURCHASEORDER_DESC.YPO_RECDBAGS AS OUTPCS, ALLYARNPURCHASEORDER_DESC.BALANCE AS BALPCS, ALLYARNPURCHASEORDER_DESC.YPO_RATE AS RATE, ALLYARNPURCHASEORDER.YPO_CRDAYS AS DAYS, 0 AS PERDAYPROD ", "", " ALLYARNPURCHASEORDER INNER JOIN ALLYARNPURCHASEORDER_DESC ON ALLYARNPURCHASEORDER.YPO_NO = ALLYARNPURCHASEORDER_DESC.YPO_NO AND ALLYARNPURCHASEORDER.YPO_YEARID = ALLYARNPURCHASEORDER_DESC.YPO_YEARID INNER JOIN YARNQUALITYMASTER ON ALLYARNPURCHASEORDER_DESC.YPO_YARNQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN MILLMASTER ON ALLYARNPURCHASEORDER_DESC.YPO_MILLID = MILLMASTER.MILL_ID INNER JOIN LEDGERS ON ALLYARNPURCHASEORDER.YPO_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON ALLYARNPURCHASEORDER.YPO_BROKERID = AGENTLEDGERS.ACC_ID  ", " AND ALLYARNPURCHASEORDER.YPO_YEARID =" & YearId & SOCLAUSE & " ORDER BY YARNQUALITYMASTER.YARN_NAME, ALLYARNPURCHASEORDER.YPO_DATE, ALLYARNPURCHASEORDER.YPO_NO")
                End If
            End If

            For Each DTROW As DataRow In DT.Rows
                If LASTITEMNAME <> DTROW("ITEMNAME") Then
                    LASTITEMNAME = DTROW("ITEMNAME")
                    If GRIDSO.RowCount > 0 Then
                        GRIDSO.Rows.Add("", "", "", "COMPLETION DAYS - " & Val(COMPLETIONDAYS), "", "TOTAL", "", Val(TOTALPCS), Val(TOTALDELPCS), Val(TOTALBALPCS), "", "")
                        GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.ForeColor = Color.Maroon
                        GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.Font = New Font("Calibri", 10, FontStyle.Bold)
                        GRIDSO.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "")

                        TOTALPCS = 0
                        TOTALDELPCS = 0
                        TOTALBALPCS = 0
                        COMPLETIONDAYS = 0
                    End If
                    GRIDSO.Rows.Add(DTROW("ITEMNAME"), "", "", "PER DAY PROD - " & Val(DTROW("PERDAYPROD")), "", "", "", "", "", "", "", "")
                    GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.Font = New Font("Calibri", 10, FontStyle.Bold)
                End If
                GRIDSO.Rows.Add("", Val(DTROW("SONO")), Format(DTROW("SODATE"), "dd/MM/yyyy"), DTROW("NAME"), DTROW("AGENTNAME"), DTROW("NOTE"), DTROW("MILLNAME"), Val(DTROW("PCS")), Val(DTROW("OUTPCS")), Val(DTROW("BALPCS")), Format(Val(DTROW("RATE")), "0.00"), Val(DTROW("DAYS")))
                TOTALPCS += Val(DTROW("PCS"))
                GTOTALPCS += Val(DTROW("PCS"))
                TOTALDELPCS += Val(DTROW("OUTPCS"))
                GTOTALDELPCS += Val(DTROW("OUTPCS"))
                TOTALBALPCS += Val(DTROW("BALPCS"))
                GTOTALBALPCS += Val(DTROW("BALPCS"))
                If Val(TOTALBALPCS) > 0 And Val(DTROW("PERDAYPROD")) > 0 Then COMPLETIONDAYS = Format(Val(TOTALBALPCS) / Val(DTROW("PERDAYPROD")), "0")
            Next

            'FOR TOTAL AND GRANDTOTAL ON LAST LINE
            If GRIDSO.RowCount > 0 Then
                GRIDSO.Rows.Add("", "", "", "COMPLETION DAYS - " & Val(COMPLETIONDAYS), "", "TOTAL", "", Val(TOTALPCS), Val(TOTALDELPCS), Val(TOTALBALPCS), "", "")
                GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.ForeColor = Color.Maroon
                GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.Font = New Font("Calibri", 10, FontStyle.Bold)


                GRIDSO.Rows.Add("", "", "", "", "", "GRAND TOTAL", "", Val(GTOTALPCS), Val(GTOTALDELPCS), Val(GTOTALBALPCS), "", "")
                GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.ForeColor = Color.DarkGreen
                GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.Font = New Font("Calibri", 10, FontStyle.Bold)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OrderGridReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXPORT_Click(sender As Object, e As EventArgs) Handles CMDEXPORT.Click
        Try
            Dim xlapp As Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim i As Integer
            Dim j As Integer

            xlapp = New Excel.Application
            xlWorkBook = xlapp.Workbooks.Add(misValue)
            xlWorkSheet = CType(xlWorkBook.Sheets("Sheet1"), Excel.Worksheet)

            For k = 0 To GRIDSO.ColumnCount - 1
                xlWorkSheet.Cells(1, k + 1).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(1, k + 1) = GRIDSO.Columns(k).HeaderText
                xlWorkSheet.Rows.Item(1).EntireColumn.AutoFit()
            Next
            For i = 0 To GRIDSO.RowCount - 1
                For j = 0 To GRIDSO.ColumnCount - 1
                    xlWorkSheet.Cells(i + 2, j + 1).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(i + 2, j + 1) = GRIDSO(j, i).Value.ToString()
                Next
                xlWorkSheet.Rows.Item(i + 2).EntireColumn.AutoFit()
            Next


            Dim SaveFileDialog1 As New SaveFileDialog()
            SaveFileDialog1.Filter = "Execl files (*.xlsx)|*.xlsx"
            SaveFileDialog1.FilterIndex = 2
            SaveFileDialog1.RestoreDirectory = True
            If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                xlWorkSheet.SaveAs(SaveFileDialog1.FileName)
                MsgBox("Save file success")
            Else
                Return
            End If
            xlWorkBook.Close()
            xlapp.Quit()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OrderGridReport_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "ABHEE" Then GAGENTNAME.Visible = False
            If ClientName = "MASHOK" Then
                CMBORDERTYPE.Items.Clear()
                CMBORDERTYPE.Items.Add("GREY")
                CMBORDERTYPE.Items.Add("YARN")
                GPMILLNAME.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If FRMSTRING = "SO" Then
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(sender As Object, e As EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then FILLNAME(CMBAGENT, False, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBAGENT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBAGENT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If FRMSTRING = "SO" Then OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' " Else OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCATEGORY_Enter(sender As Object, e As EventArgs) Handles CMBCATEGORY.Enter
        Try
            If CMBCATEGORY.Text.Trim = "" Then fillCATEGORY(CMBCATEGORY, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCATEGORY_Validating(sender As Object, e As CancelEventArgs) Handles CMBCATEGORY.Validating
        Try
            If CMBCATEGORY.Text.Trim <> "" Then CATEGORYVALIDATE(CMBCATEGORY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBAGENT_CheckedChanged(sender As Object, e As EventArgs) Handles RBAGENT.CheckedChanged
        Try
            FILLCMB()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                dtrow("CHK") = CHKSELECTALL.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RDBALL_CheckedChanged(sender As Object, e As EventArgs) Handles RDBALL.CheckedChanged, RDBCLOSED.CheckedChanged, RDBCOMPLETE.CheckedChanged, RDBPENDING.CheckedChanged
        If sender IsNot Nothing AndAlso CType(sender, RadioButton).Checked Then
            Try
                FILLCMB()
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub
    Private Sub GRIDSO_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSO.CellDoubleClick
        Try
            ' Ensure valid row index
            If e.RowIndex >= 0 Then
                ' Get gsono from clicked row
                Dim gsono As String = GRIDSO.Rows(e.RowIndex).Cells("GSONO").Value.ToString()

                ' Call ShowReport with gsono
                ShowReport(gsono)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub




    Private Sub ShowReport(orderNo As Integer)
        Try
            Dim rpt As New SaleInvoiceDesign() ' or another CrystalReport
            rpt.MdiParent = MDIMain

            ' ✅ Set WHERECLAUSE
            If ORDERTYPE = "SO" Or ORDERTYPE = "YARNSO" Then
                rpt.WHERECLAUSE = " {ALLSALEORDER.SO_NO}=" & orderNo & " AND {ALLSALEORDER.SO_yearid}=" & YearId
                If ORDERTYPE = "YARNSO" Then
                    rpt.WHERECLAUSE = rpt.WHERECLAUSE.Replace("ALLSALEORDER", "ALLYARNSALEORDER").Replace("SO_", "YSO_")
                End If
            Else
                rpt.WHERECLAUSE = " {ALLPURCHASEORDER.PO_NO}=" & orderNo & " AND {ALLPURCHASEORDER.PO_yearid}=" & YearId
                If ORDERTYPE = "YARNPO" Then
                    rpt.WHERECLAUSE = rpt.WHERECLAUSE.Replace("ALLPURCHASEORDER", "ALLYARNPURCHASEORDER").Replace("PO_", "YPO_")
                End If
            End If

            ' ✅ Set FRMSTRING based on type
            Select Case ORDERTYPE
                Case "SO"
                    rpt.FRMSTRING = "SOSTATUSDTLS"
                Case "YARNSO"
                    rpt.FRMSTRING = "YARNSOSTATUSDTLS"
                Case "PO"
                    rpt.FRMSTRING = "POSTATUSDTLS"
                Case "YARNPO"
                    rpt.FRMSTRING = "YARNPOSTATUSDTLS"
            End Select

            rpt.Show()
        Catch ex As Exception
            MessageBox.Show("Error loading report: " & ex.Message)
        End Try
    End Sub

    Private Sub CMDWHATSAPP_Click(sender As Object, e As EventArgs) Handles CMDWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub

            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            ' Prepare data for grid
            ' TEMPOUTSTANDING()

            ' Generate the PDF from DataGridView
            Dim filePath As String = Application.StartupPath & "\Pending Order_" & CMBNAME.Text.Trim & ".pdf"

            ' ✅ Replace "YourDataGridView" with the actual DataGridView object from your form
            ExportDataGridViewToPdfForWP(GRIDSO, filePath)

            ' Prepare WhatsApp sending form
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.PATH.Add(filePath)
            OBJWHATSAPP.FILENAME.Add("Pending Order" & CMBNAME.Text.Trim & ".pdf")
            OBJWHATSAPP.ShowDialog()

            ' Delete PDF if client is SNCM
            If ClientName = "SNCM" Then
                For Each path As String In OBJWHATSAPP.PATH
                    If File.Exists(path) Then
                        File.Delete(path)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExportDataGridViewToPdfForWP(dgv As DataGridView, filePath As String)
        ' 👉 Changed to A3 for bigger page size
        Dim doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A3.Rotate(), 20, 20, 20, 20)

        Try
            PdfWriter.GetInstance(doc, New FileStream(filePath, FileMode.Create))
            doc.Open()

            ' Load Verdana font
            Dim verdanaBaseFont As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\verdana.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim verdana10 As New iTextSharp.text.Font(verdanaBaseFont, 10)
            Dim verdana10Bold As New iTextSharp.text.Font(verdanaBaseFont, 10, iTextSharp.text.Font.BOLD)
            Dim verdana16Bold As New iTextSharp.text.Font(verdanaBaseFont, 16, iTextSharp.text.Font.BOLD)

            ' Title & Date
            doc.Add(New iTextSharp.text.Paragraph("Order Grid Report", verdana16Bold))
            doc.Add(New iTextSharp.text.Paragraph("Generated on: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm"), verdana10))
            doc.Add(New iTextSharp.text.Paragraph(" "))

            ' Collect visible columns
            Dim visibleColumns As New List(Of DataGridViewColumn)
            For Each col As DataGridViewColumn In dgv.Columns
                If col.Visible Then visibleColumns.Add(col)
            Next

            Dim table As New PdfPTable(visibleColumns.Count)
            table.WidthPercentage = 100
            table.HeaderRows = 1

            ' 👉 Custom width logic: NAME & BILL AMT are wider
            Dim columnWidths(visibleColumns.Count - 1) As Single
            Dim totalWeight As Single = 0.0F

            For i As Integer = 0 To visibleColumns.Count - 1
                Dim header As String = visibleColumns(i).HeaderText.Trim().ToUpper()
                Select Case header
                    Case "NAME", "AGENT NAME"
                        columnWidths(i) = 2.5F  ' 👈 Increased
                    Case "BILL AMT"
                        columnWidths(i) = 2.0F
                    Case "RECD AMT", "BALANCE", "RUNNING BAL"
                        columnWidths(i) = 1.5F
                    Case "NOTE"
                        columnWidths(i) = 5.0F  ' 👈 Increased
                    Case Else
                        columnWidths(i) = 1.0F  ' 👈 Increased
                End Select
                totalWeight += columnWidths(i)
            Next

            ' Normalize widths to make total = 100%
            For i As Integer = 0 To columnWidths.Length - 1
                columnWidths(i) = columnWidths(i) / totalWeight * 100.0F
            Next

            table.SetWidths(columnWidths)

            ' Headers
            For Each col As DataGridViewColumn In visibleColumns
                Dim headerCell As New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(col.HeaderText, verdana10Bold)) With {
                 .BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY,
                 .HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
                 .VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,
                 .Padding = 5,
                 .NoWrap = False
              }

                table.AddCell(headerCell)

            Next


            ' Data rows
            For Each row As DataGridViewRow In dgv.Rows
                If Not row.IsNewRow Then
                    Dim isGrandTotalRow As Boolean = False

                    For Each cell As DataGridViewCell In row.Cells
                        If cell.Value IsNot Nothing AndAlso cell.Value.ToString().Trim().ToUpper() = "GRANDTOTAL" Then
                            isGrandTotalRow = True
                            Exit For
                        End If
                    Next

                    For Each col As DataGridViewColumn In visibleColumns
                        Dim cell As DataGridViewCell = row.Cells(col.Index)
                        Dim value As String = ""

                        If cell.Value IsNot Nothing Then
                            If TypeOf cell.Value Is DateTime Then
                                value = CType(cell.Value, DateTime).ToString("dd/MM/yyyy")
                            Else
                                value = cell.Value.ToString()
                            End If
                        End If

                        Dim pdfCell As New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(value, If(isGrandTotalRow, verdana10Bold, verdana10))) With {
                        .VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,
                        .Padding = 4
                    }

                        ' Color logic
                        If isGrandTotalRow Then
                            pdfCell.BackgroundColor = New iTextSharp.text.BaseColor(250, 240, 230)

                        ElseIf row.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow Then
                            pdfCell.BackgroundColor = iTextSharp.text.BaseColor.YELLOW

                        ElseIf row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen Then
                            pdfCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY

                        End If

                        ' Wrapping for specific columns
                        Dim colName As String = col.HeaderText.Trim().ToUpper()
                        Select Case colName
                            Case "NAME", "INV NO", "ITEM NAME", "MILL NAME", "PCS/BAGS", "REMARKS", "BROKER", "JOBBERNAME", "TRANSNAME", "GODOWN"
                                pdfCell.NoWrap = False
                            Case Else
                                pdfCell.NoWrap = True
                        End Select

                        ' Alignment

                        If IsNumeric(value) Then
                            pdfCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT
                        Else
                            pdfCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                        End If

                        table.AddCell(pdfCell)
                    Next
                End If
            Next

            doc.Add(table)

        Catch ex As Exception
            MessageBox.Show("Failed to export PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            doc.Close()
        End Try
    End Sub

End Class