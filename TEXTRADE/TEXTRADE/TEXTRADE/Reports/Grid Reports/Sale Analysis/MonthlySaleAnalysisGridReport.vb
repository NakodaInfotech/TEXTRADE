
Imports BL

Public Class MonthlySaleAnalysisGridReport

    Dim FILLDONE As Boolean = True

    Public Sub New()
        InitializeComponent()
        FILLCMB()
    End Sub

    Sub FILLCMB()
        Try
            FILLNAME(CMBNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
            FILLNAME(CMBBROKERNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'AGENT'")

            fillCITY(CMBCITY, False)
            fillregister(CMBREGISTER, " and register_type = 'SALE'")
            fillitemname(CMBITEMNAME, " AND ITEM_FRMSTRING = 'MERCHANT'")


            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.ACC_AREAID = AREAMASTER.AREA_ID LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.ACC_ID", " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_YEARID = " & YearId & " ORDER BY LEDGERS.Acc_cmpname")
            gridpartydetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                Gridparty.FocusedRowHandle = Gridparty.RowCount - 1
                Gridparty.TopRowIndex = Gridparty.RowCount - 15
            End If


            Dim DT1 As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME", " ", " LEDGERS ", " AND LEDGERS.ACC_TYPE = 'AGENT' AND LEDGERS.ACC_YEARID = " & YearId & " ORDER BY LEDGERS.Acc_cmpname")
            GRIDAGENTDETAILS.DataSource = DT1
            If DT1.Rows.Count > 0 Then
                GRIDAGENT.FocusedRowHandle = GRIDAGENT.RowCount - 1
                GRIDAGENT.TopRowIndex = GRIDAGENT.RowCount - 15
            End If

            Dim DTITEM As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ITEMMASTER.ITEM_NAME AS NAME", " ", " ITEMMASTER ", " AND ITEMMASTER.ITEM_YEARID = " & YearId & " ORDER BY ITEMMASTER.ITEM_NAME")
            GRIDITEMDETAILS.DataSource = DTITEM
            If DTITEM.Rows.Count > 0 Then
                GRIDITEM.FocusedRowHandle = GRIDITEM.RowCount - 1
                GRIDITEM.TopRowIndex = GRIDITEM.RowCount - 15
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MonthlySaleAnalysisGridReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MonthlySaleAnalysisGridReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            'DONE BY GULKIT, COZ IF FILLDONE IS NOT MENTIONED HERE THEN EVERYTIME IT GOES TO SELECTEDINDEXCHANGE EVENT ON FILLING THE COMBO
            FILLDONE = True
            CMBREPORTTYPE.SelectedIndex = 0
            CMBPRINTVALUE.SelectedIndex = 0

            dtfrom.Value = AccFrom.Date
            dtto.Value = AccTo.Date

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            GRIDREPORT.RowCount = 0
            Dim WHERECLAUSE As String = " AND INVOICEMASTER.INVOICE_YEARID = " & YearId
            If chkdate.Checked = True Then WHERECLAUSE = WHERECLAUSE & " AND INVOICEMASTER.INVOICE_DATE >='" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "' AND INVOICEMASTER.INVOICE_DATE <= '" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
            If CMBREGISTER.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND REGISTERMASTER.REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "'"
            If CMBNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "'"
            If CMBBROKERNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND AGENTLEDGERS.ACC_CMPNAME = '" & CMBBROKERNAME.Text.Trim & "'"
            If CMBITEMNAME.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND INVDESC.ITEMNAME = '" & CMBITEMNAME.Text.Trim & "'"
            If CMBCITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND CITYMASTER.CITY_NAME = '" & CMBCITY.Text.Trim & "'"


            'FOR PARTYNAME
            Dim NAMECLAUSE As String = ""
            Dim ITEMCLAUSE As String = ""
            Dim AGENTCLAUSE As String = ""

            Gridparty.ClearColumnsFilter()
            For i As Integer = 0 To Gridparty.RowCount - 1
                Dim dtrow As DataRow = Gridparty.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If NAMECLAUSE = "" Then
                        NAMECLAUSE = " AND (LEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                    Else
                        NAMECLAUSE = NAMECLAUSE & " OR LEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                    End If
                End If
            Next
            If NAMECLAUSE <> "" Then
                NAMECLAUSE = NAMECLAUSE & ")"
                WHERECLAUSE = WHERECLAUSE & NAMECLAUSE
            End If


            'FOR ITEMNAME
            GRIDITEM.ClearColumnsFilter()
            For i As Integer = 0 To GRIDITEM.RowCount - 1
                Dim dtrow As DataRow = GRIDITEM.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If ITEMCLAUSE = "" Then
                        ITEMCLAUSE = " AND (INVDESC.ITEMNAME = '" & dtrow("NAME") & "'"
                    Else
                        ITEMCLAUSE = ITEMCLAUSE & " OR INVDESC.ITEMNAME = '" & dtrow("NAME") & "'"
                    End If
                End If
            Next
            If ITEMCLAUSE <> "" Then
                ITEMCLAUSE = ITEMCLAUSE & ")"
                WHERECLAUSE = WHERECLAUSE & ITEMCLAUSE
            End If



            GRIDAGENT.ClearColumnsFilter()
            For i As Integer = 0 To GRIDAGENT.RowCount - 1
                Dim dtrow As DataRow = GRIDAGENT.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If AGENTCLAUSE = "" Then
                        AGENTCLAUSE = " AND (AGENTLEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                    Else
                        AGENTCLAUSE = AGENTCLAUSE & " OR AGENTLEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                    End If
                End If
            Next
            If AGENTCLAUSE <> "" Then
                AGENTCLAUSE = AGENTCLAUSE & ")"
                WHERECLAUSE = WHERECLAUSE & AGENTCLAUSE
            End If



            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable


            Dim GROUPBYCLAUSE As String = ""
            Dim QTYMTRS As String = "INVOICE_TOTALMTRS"
            If CMBPRINTVALUE.Text = "Amt Only" Then QTYMTRS = "INVOICE_SUBTOTAL"
            If CMBPRINTVALUE.Text = "Taka Only" Then QTYMTRS = "INVOICE_TOTALPCS"
            Dim TOTALCLAUSE As String = " ROUND(SUM(INVOICEMASTER." & QTYMTRS & ") ,2)"



            If CMBPRINTVALUE.Text = "Avg Mtrs" Or CMBPRINTVALUE.Text = "Avg Rate" Then
                If CMBPRINTVALUE.Text = "Avg Mtrs" Then
                    QTYMTRS = " CASE WHEN SUM(INVOICE_TOTALPCS) > 0 THEN ROUND((SUM(INVOICE_TOTALMTRS)/ SUM(INVOICE_TOTALPCS)),2) ELSE 0 END "
                    TOTALCLAUSE = " CASE WHEN SUM(INVOICE_TOTALPCS) > 0 THEN ROUND((SUM(INVOICE_TOTALMTRS)/ SUM(INVOICE_TOTALPCS)),2) ELSE 0 END "
                ElseIf CMBPRINTVALUE.Text = "Avg Rate" Then
                    QTYMTRS = " CASE WHEN SUM(INVOICE_TOTALMTRS) > 0 THEN ROUND((SUM(INVOICE_TOTALAMT)/ SUM(INVOICE_TOTALMTRS)),2) ELSE 0 END "
                    TOTALCLAUSE = " CASE WHEN SUM(INVOICE_TOTALMTRS) > 0 THEN ROUND((SUM(INVOICE_TOTALAMT)/ SUM(INVOICE_TOTALMTRS)),2) ELSE 0 END "
                End If

                If CMBREPORTTYPE.Text = "PARTYWISE" Then
                    GROUPBYCLAUSE = " GROUP BY LEDGERS.ACC_CMPNAME,UPPER(DATENAME(MONTH,INVOICE_DATE))"
                ElseIf CMBREPORTTYPE.Text = "AGENTWISE" Then
                    GROUPBYCLAUSE = " GROUP BY AGENTLEDGER.ACC_CMPNAME,UPPER(DATENAME(MONTH,INVOICE_DATE))"
                ElseIf CMBREPORTTYPE.Text = "CITYWISE" Then
                    GROUPBYCLAUSE = " GROUP BY CITYMASTER.CITY_NAME,UPPER(DATENAME(MONTH,INVOICE_DATE))"
                ElseIf CMBREPORTTYPE.Text = "ITEMWISE" Then
                    GROUPBYCLAUSE = " GROUP BY INVDESC.ITEMNAME,UPPER(DATENAME(MONTH,INVOICE_DATE))"
                ElseIf CMBREPORTTYPE.Text = "CATEGORYWISE" Then
                    GROUPBYCLAUSE = " GROUP BY INVDESC.CATEGORY,UPPER(DATENAME(MONTH,INVOICE_DATE))"
                End If
            End If


            If CMBREPORTTYPE.Text = "PARTYWISE" Then
                DT = OBJCMN.SEARCH(" NAME, ISNULL(APRIL,0) AS APR, ISNULL(MAY,0) AS MAY, ISNULL(JUNE,0) AS JUN, ISNULL(JULY,0) AS JUL, ISNULL(AUGUST,0) AS AUG, ISNULL(SEPTEMBER,0) AS SEP, ISNULL(OCTOBER,0) AS OCT, ISNULL(NOVEMBER,0) AS NOV, ISNULL(DECEMBER,0) AS DEC, ISNULL(JANUARY,0) AS JAN, ISNULL(FEBRUARY,0) AS FEB, ISNULL(MARCH,0) AS MAR, ROUND((ISNULL(APRIL,0) + ISNULL(MAY,0)+ ISNULL(JUNE,0)+ISNULL(JULY,0)+ISNULL(AUGUST,0)+ISNULL(SEPTEMBER,0)+ISNULL(OCTOBER,0)+ISNULL(NOVEMBER,0)+ISNULL(DECEMBER,0)+ISNULL(JANUARY,0)+ISNULL(FEBRUARY,0)+ISNULL(MARCH,0)),2) AS TOTALMTRS ", "", " (SELECT LEDGERS.Acc_cmpname AS NAME, UPPER(DATENAME(MONTH,INVOICE_DATE)) AS [MONTHNAME] , " & QTYMTRS & " AS TOTALMTRS FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID CROSS APPLY (SELECT TOP 1 ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORY_NAME,'') AS CATEGORY FROM INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON INVOICEMASTER_DESC.INVOICE_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORY_ID WHERE INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID ) AS INVDESC WHERE 1=1 " & WHERECLAUSE & GROUPBYCLAUSE & " UNION ALL SELECT 'PARTY TOTAL' AS NAME, UPPER(DATENAME(MONTH,INVOICE_DATE)) AS [MONTHNAME], " & TOTALCLAUSE & " AS TOTALMTRS FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID CROSS APPLY (SELECT TOP 1 ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORY_NAME,'') AS CATEGORY FROM INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON INVOICEMASTER_DESC.INVOICE_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORY_ID WHERE INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID ) AS INVDESC WHERE 1=1 " & WHERECLAUSE & " GROUP BY UPPER(DATENAME(MONTH,INVOICE_DATE))) AS INVOICEDATA PIVOT (SUM(TOTALMTRS) FOR  [MONTHNAME] IN (APRIL, MAY, JUNE, JULY, AUGUST, SEPTEMBER, OCTOBER, NOVEMBER, DECEMBER, JANUARY, FEBRUARY, MARCH)) AS PIV  ", " ORDER BY (CASE NAME WHEN 'PARTY TOTAL' THEN 2 ELSE 1 END), NAME")
            ElseIf CMBREPORTTYPE.Text = "AGENTWISE" Then
                DT = OBJCMN.SEARCH(" NAME, ISNULL(APRIL,0) AS APR, ISNULL(MAY,0) AS MAY, ISNULL(JUNE,0) AS JUN, ISNULL(JULY,0) AS JUL, ISNULL(AUGUST,0) AS AUG, ISNULL(SEPTEMBER,0) AS SEP, ISNULL(OCTOBER,0) AS OCT, ISNULL(NOVEMBER,0) AS NOV, ISNULL(DECEMBER,0) AS DEC, ISNULL(JANUARY,0) AS JAN, ISNULL(FEBRUARY,0) AS FEB, ISNULL(MARCH,0) AS MAR, ROUND((ISNULL(APRIL,0) + ISNULL(MAY,0)+ ISNULL(JUNE,0)+ISNULL(JULY,0)+ISNULL(AUGUST,0)+ISNULL(SEPTEMBER,0)+ISNULL(OCTOBER,0)+ISNULL(NOVEMBER,0)+ISNULL(DECEMBER,0)+ISNULL(JANUARY,0)+ISNULL(FEBRUARY,0)+ISNULL(MARCH,0)),2) AS TOTALMTRS ", "", " (SELECT AGENTLEDGERS.Acc_cmpname AS NAME, UPPER(DATENAME(MONTH,INVOICE_DATE)) AS [MONTHNAME] , " & QTYMTRS & " AS TOTALMTRS FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID CROSS APPLY (SELECT TOP 1 ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORY_NAME,'') AS CATEGORY FROM INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON INVOICEMASTER_DESC.INVOICE_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORY_ID WHERE INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID ) AS INVDESC WHERE 1=1 " & WHERECLAUSE & GROUPBYCLAUSE & " UNION ALL SELECT 'AGENT TOTAL' AS NAME, UPPER(DATENAME(MONTH,INVOICE_DATE)) AS [MONTHNAME], " & TOTALCLAUSE & " AS TOTALMTRS FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID CROSS APPLY (SELECT TOP 1 ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORY_NAME,'') AS CATEGORY FROM INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON INVOICEMASTER_DESC.INVOICE_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORY_ID WHERE INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID ) AS INVDESC WHERE 1=1 " & WHERECLAUSE & " GROUP BY UPPER(DATENAME(MONTH,INVOICE_DATE))) AS INVOICEDATA PIVOT (SUM(TOTALMTRS) FOR  [MONTHNAME] IN (APRIL, MAY, JUNE, JULY, AUGUST, SEPTEMBER, OCTOBER, NOVEMBER, DECEMBER, JANUARY, FEBRUARY, MARCH)) AS PIV  ", " ORDER BY (CASE NAME WHEN 'AGENT TOTAL' THEN 2 ELSE 1 END), NAME")
            ElseIf CMBREPORTTYPE.Text = "CITYWISE" Then
                DT = OBJCMN.SEARCH(" NAME, ISNULL(APRIL,0) AS APR, ISNULL(MAY,0) AS MAY, ISNULL(JUNE,0) AS JUN, ISNULL(JULY,0) AS JUL, ISNULL(AUGUST,0) AS AUG, ISNULL(SEPTEMBER,0) AS SEP, ISNULL(OCTOBER,0) AS OCT, ISNULL(NOVEMBER,0) AS NOV, ISNULL(DECEMBER,0) AS DEC, ISNULL(JANUARY,0) AS JAN, ISNULL(FEBRUARY,0) AS FEB, ISNULL(MARCH,0) AS MAR, ROUND((ISNULL(APRIL,0) + ISNULL(MAY,0)+ ISNULL(JUNE,0)+ISNULL(JULY,0)+ISNULL(AUGUST,0)+ISNULL(SEPTEMBER,0)+ISNULL(OCTOBER,0)+ISNULL(NOVEMBER,0)+ISNULL(DECEMBER,0)+ISNULL(JANUARY,0)+ISNULL(FEBRUARY,0)+ISNULL(MARCH,0)),2) AS TOTALMTRS ", "", " (SELECT ISNULL(CITYMASTER.CITY_NAME,'') AS NAME, UPPER(DATENAME(MONTH,INVOICE_DATE)) AS [MONTHNAME] ,  " & QTYMTRS & " AS TOTALMTRS FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID CROSS APPLY (SELECT TOP 1 ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORY_NAME,'') AS CATEGORY FROM INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON INVOICEMASTER_DESC.INVOICE_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORY_ID WHERE INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID ) AS INVDESC WHERE 1=1 " & WHERECLAUSE & GROUPBYCLAUSE & " UNION ALL SELECT 'CITY TOTAL' AS NAME, UPPER(DATENAME(MONTH,INVOICE_DATE)) AS [MONTHNAME], " & TOTALCLAUSE & " AS TOTALMTRS FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID CROSS APPLY (SELECT TOP 1 ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORY_NAME,'') AS CATEGORY FROM INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON INVOICEMASTER_DESC.INVOICE_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORY_ID WHERE INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID ) AS INVDESC  WHERE 1=1 " & WHERECLAUSE & " GROUP BY UPPER(DATENAME(MONTH,INVOICE_DATE))) AS INVOICEDATA PIVOT (SUM(TOTALMTRS) FOR  [MONTHNAME] IN (APRIL, MAY, JUNE, JULY, AUGUST, SEPTEMBER, OCTOBER, NOVEMBER, DECEMBER, JANUARY, FEBRUARY, MARCH)) AS PIV  ", " ORDER BY (CASE NAME WHEN 'CITY TOTAL' THEN 2 ELSE 1 END), NAME")
            ElseIf CMBREPORTTYPE.Text = "ITEMWISE" Then
                DT = OBJCMN.SEARCH(" NAME, ISNULL(APRIL,0) AS APR, ISNULL(MAY,0) AS MAY, ISNULL(JUNE,0) AS JUN, ISNULL(JULY,0) AS JUL, ISNULL(AUGUST,0) AS AUG, ISNULL(SEPTEMBER,0) AS SEP, ISNULL(OCTOBER,0) AS OCT, ISNULL(NOVEMBER,0) AS NOV, ISNULL(DECEMBER,0) AS DEC, ISNULL(JANUARY,0) AS JAN, ISNULL(FEBRUARY,0) AS FEB, ISNULL(MARCH,0) AS MAR, ROUND((ISNULL(APRIL,0) + ISNULL(MAY,0)+ ISNULL(JUNE,0)+ISNULL(JULY,0)+ISNULL(AUGUST,0)+ISNULL(SEPTEMBER,0)+ISNULL(OCTOBER,0)+ISNULL(NOVEMBER,0)+ISNULL(DECEMBER,0)+ISNULL(JANUARY,0)+ISNULL(FEBRUARY,0)+ISNULL(MARCH,0)),2) AS TOTALMTRS ", "", " (SELECT INVDESC.ITEMNAME AS NAME, UPPER(DATENAME(MONTH,INVOICE_DATE)) AS [MONTHNAME] , " & QTYMTRS & " AS TOTALMTRS FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID CROSS APPLY (SELECT TOP 1 ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORY_NAME,'') AS CATEGORY FROM INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON INVOICEMASTER_DESC.INVOICE_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORY_ID WHERE INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID ) AS INVDESC WHERE 1=1 " & WHERECLAUSE & GROUPBYCLAUSE & " UNION ALL SELECT 'ITEM TOTAL' AS NAME, UPPER(DATENAME(MONTH,INVOICE_DATE)) AS [MONTHNAME], " & TOTALCLAUSE & " AS TOTALMTRS FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID CROSS APPLY (SELECT TOP 1 ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORY_NAME,'') AS CATEGORY FROM INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON INVOICEMASTER_DESC.INVOICE_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORY_ID WHERE INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID ) AS INVDESC  WHERE 1=1 " & WHERECLAUSE & " GROUP BY UPPER(DATENAME(MONTH,INVOICE_DATE))) AS INVOICEDATA PIVOT (SUM(TOTALMTRS) FOR  [MONTHNAME] IN (APRIL, MAY, JUNE, JULY, AUGUST, SEPTEMBER, OCTOBER, NOVEMBER, DECEMBER, JANUARY, FEBRUARY, MARCH)) AS PIV  ", " ORDER BY (CASE NAME WHEN 'ITEM TOTAL' THEN 2 ELSE 1 END), NAME")
            ElseIf CMBREPORTTYPE.Text = "CATEGORYWISE" Then
                DT = OBJCMN.SEARCH(" NAME, ISNULL(APRIL,0) AS APR, ISNULL(MAY,0) AS MAY, ISNULL(JUNE,0) AS JUN, ISNULL(JULY,0) AS JUL, ISNULL(AUGUST,0) AS AUG, ISNULL(SEPTEMBER,0) AS SEP, ISNULL(OCTOBER,0) AS OCT, ISNULL(NOVEMBER,0) AS NOV, ISNULL(DECEMBER,0) AS DEC, ISNULL(JANUARY,0) AS JAN, ISNULL(FEBRUARY,0) AS FEB, ISNULL(MARCH,0) AS MAR, ROUND((ISNULL(APRIL,0) + ISNULL(MAY,0)+ ISNULL(JUNE,0)+ISNULL(JULY,0)+ISNULL(AUGUST,0)+ISNULL(SEPTEMBER,0)+ISNULL(OCTOBER,0)+ISNULL(NOVEMBER,0)+ISNULL(DECEMBER,0)+ISNULL(JANUARY,0)+ISNULL(FEBRUARY,0)+ISNULL(MARCH,0)),2) AS TOTALMTRS ", "", " (SELECT INVDESC.CATEGORY AS NAME, UPPER(DATENAME(MONTH,INVOICE_DATE)) AS [MONTHNAME] , " & QTYMTRS & " AS TOTALMTRS FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID CROSS APPLY (SELECT TOP 1 ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORY_NAME,'') AS CATEGORY FROM INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON INVOICEMASTER_DESC.INVOICE_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORY_ID WHERE INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID ) AS INVDESC WHERE 1=1 " & WHERECLAUSE & GROUPBYCLAUSE & " UNION ALL SELECT 'CATEGORY TOTAL' AS NAME, UPPER(DATENAME(MONTH,INVOICE_DATE)) AS [MONTHNAME], " & TOTALCLAUSE & " AS TOTALMTRS FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID CROSS APPLY (SELECT TOP 1 ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORY_NAME,'') AS CATEGORY FROM INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON INVOICEMASTER_DESC.INVOICE_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORY_ID WHERE INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID ) AS INVDESC  WHERE 1=1 " & WHERECLAUSE & " GROUP BY UPPER(DATENAME(MONTH,INVOICE_DATE))) AS INVOICEDATA PIVOT (SUM(TOTALMTRS) FOR  [MONTHNAME] IN (APRIL, MAY, JUNE, JULY, AUGUST, SEPTEMBER, OCTOBER, NOVEMBER, DECEMBER, JANUARY, FEBRUARY, MARCH)) AS PIV  ", " ORDER BY (CASE NAME WHEN 'CATEGORY TOTAL' THEN 2 ELSE 1 END), NAME")
            End If
            For Each ROW As DataRow In DT.Rows
                GRIDREPORT.Rows.Add(ROW("NAME"), Val(ROW("APR")), Val(ROW("MAY")), Val(ROW("JUN")), Val(ROW("JUL")), Val(ROW("AUG")), Val(ROW("SEP")), Val(ROW("OCT")), Val(ROW("NOV")), Val(ROW("DEC")), Val(ROW("JAN")), Val(ROW("FEB")), Val(ROW("MAR")), Val(ROW("TOTALMTRS")))
            Next

            If GRIDREPORT.RowCount > 1 Then
                GRIDREPORT.Rows(GRIDREPORT.RowCount - 1).DefaultCellStyle.ForeColor = Color.Maroon
                TabControl1.SelectedIndex = 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDSEARCH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSEARCH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors'", "Sundry Debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub CMBAGENT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBROKERNAME.Validating
        Try
            If CMBBROKERNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBBROKERNAME, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBROKERNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT' "
                OBJLEDGER.ShowDialog()
                'If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBBROKERNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then itemvalidate(CMBITEMNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCITY As New SelectCity
                OBJCITY.FRMSTRING = "CITY"
                OBJCITY.ShowDialog()
                If OBJCITY.TEMPNAME <> "" Then CMBCITY.Text = OBJCITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSCITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCITY.Validating
        Try
            If CMBCITY.Text.Trim <> "" Then CITYVALIDATE(CMBCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKERNAME_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.SelectedValueChanged, CMBBROKERNAME.SelectedValueChanged, CMBCITY.SelectedValueChanged, CMBREGISTER.SelectedValueChanged, CMBITEMNAME.SelectedValueChanged
        Try
            If FILLDONE = False Then Exit Sub
            CMDSEARCH_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            CMBNAME.Text = ""
            CMBBROKERNAME.Text = ""
            CMBCITY.Text = ""
            CMBREGISTER.Text = ""
            CMBITEMNAME.Text = ""
            chkdate.CheckState = CheckState.Unchecked

            'FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPRINT_Click(sender As Object, e As EventArgs) Handles CMDPRINT.Click
        Try
            If GRIDREPORT.RowCount = 0 Then Exit Sub
            Dim PRINT As Boolean = True
            Dim WHATSAPP As Boolean = True

            If MsgBox("Wish to Print?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            TEMPOUTSTANDING()


            If MsgBox("Wish to Print in Excel?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim OBJRPT As New clsReportDesigner("Outstanding Report", System.AppDomain.CurrentDomain.BaseDirectory & "Outstanding Report.xlsx", 2)
                OBJRPT.OUTSTANDIGEXCEL(ClientName, CmpId, YearId)
                Exit Sub
            End If

            Dim OBJPL As New PLDesign
            OBJPL.frmstring = "OUTSTANDING"
            OBJPL.MdiParent = MDIMain
            OBJPL.strsearch = "{TEMPOUTSTANDING.YEARID} = " & YearId
            OBJPL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TEMPOUTSTANDING()
        Try
            'Dim OBJCMN As New ClsCommon
            'Dim DT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPOUTSTANDING WHERE YEARID = " & YearId, "", "")

            'Dim I As Integer = 1

            'If TabControl1.SelectedIndex = 0 Then
            '    For Each ROW As DataGridViewRow In GRIDOUTSTANDING.Rows
            '        Dim ALPARAVAL As New ArrayList
            '        ALPARAVAL.Add(I)
            '        If ROW.Cells(GNAME.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GNAME.Index).Value)
            '        If ROW.Cells(GINVNO.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GINVNO.Index).Value)
            '        Dim TEMP As Date
            '        If Not DateTime.TryParse(ROW.Cells(GDATE.Index).Value, TEMP) Then
            '            ALPARAVAL.Add(DBNull.Value)
            '        Else
            '            ALPARAVAL.Add(TEMP)
            '        End If

            '        If Not DateTime.TryParse(ROW.Cells(GDUEDATE.Index).Value, TEMP) Then
            '            ALPARAVAL.Add(DBNull.Value)
            '        Else
            '            ALPARAVAL.Add(TEMP)
            '        End If

            '        If ROW.Cells(GBILLAMT.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(GBILLAMT.Index).Value))

            '        'WE WILL ADD PARTYCONTACT NO IN LR NO COLUMN
            '        If ROW.Cells(GDATE.Index).Value = "CONTACT" Then
            '            If ROW.Cells(GDATE.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GDATE.Index).Value)
            '        Else
            '            If ROW.Cells(GLRNO.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GLRNO.Index).Value)
            '        End If

            '        If ROW.Cells(GITEMNAME.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GITEMNAME.Index).Value)
            '        If ROW.Cells(GRECDAMT.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(GRECDAMT.Index).Value))
            '        If ROW.Cells(GBALANCE.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(GBALANCE.Index).Value))
            '        'If ROW.Cells(GDAYS.Index).Value = Nothing Then ALPARAVAL.Add(0) Else ALPARAVAL.Add(Val(ROW.Cells(GDAYS.Index).Value))
            '        If ROW.Cells(GOVERDUEDAYS.Index).Value = Nothing Then ALPARAVAL.Add(0) Else ALPARAVAL.Add(Val(ROW.Cells(GOVERDUEDAYS.Index).Value))
            '        If ROW.Cells(GCMPNAME.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(GCMPNAME.Index).Value)
            '        ALPARAVAL.Add(CmpId)
            '        ALPARAVAL.Add(YearId)
            '        If ROW.Cells(GCHARGES.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(GCHARGES.Index).Value))

            '        Dim OBJTB As New ClsTrialBalance
            '        OBJTB.alParaval = ALPARAVAL
            '        Dim INT As Integer = OBJTB.SAVEOUTSTANDING()

            '        I += 1
            '    Next

            'Else
            '    For Each ROW As DataGridViewRow In GRIDSUMM.Rows
            '        Dim ALPARAVAL As New ArrayList
            '        ALPARAVAL.Add(I)
            '        If ROW.Cells(SNAME.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(ROW.Cells(SNAME.Index).Value)
            '        ALPARAVAL.Add("")
            '        ALPARAVAL.Add("")
            '        ALPARAVAL.Add("")
            '        ALPARAVAL.Add("")
            '        ALPARAVAL.Add("")
            '        ALPARAVAL.Add("")
            '        ALPARAVAL.Add("")
            '        If ROW.Cells(SBALANCE.Index).Value = Nothing Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Val(ROW.Cells(SBALANCE.Index).Value))
            '        '  ALPARAVAL.Add(0)
            '        ALPARAVAL.Add("")
            '        ALPARAVAL.Add(CmpId)
            '        ALPARAVAL.Add(YearId)
            '        ALPARAVAL.Add("")

            '        Dim OBJTB As New ClsTrialBalance
            '        OBJTB.alParaval = ALPARAVAL
            '        Dim INT As Integer = OBJTB.SAVEOUTSTANDING()

            '        I += 1
            '    Next
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDWHATSAPP_Click(sender As Object, e As EventArgs) Handles CMDWHATSAPP.Click
        'Try
        '    If ALLOWWHATSAPP = False Then Exit Sub
        '    If Not CHECKWHASTAPPEXP() Then
        '        MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
        '        Exit Sub
        '    End If

        '    If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        '    TEMPOUTSTANDING()
        '    Dim WHATSAPPNO As String = ""
        '    Dim OBJPL As New PLDesign
        '    OBJPL.frmstring = "OUTSTANDING"
        '    OBJPL.MdiParent = MDIMain
        '    OBJPL.strsearch = "{TEMPOUTSTANDING.YEARID} = " & YearId
        '    OBJPL.DIRECTPRINT = True
        '    OBJPL.PARTYNAME = CMBNAME.Text.Trim
        '    OBJPL.Show()
        '    OBJPL.Close()

        '    Dim OBJWHATSAPP As New SendWhatsapp
        '    OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
        '    OBJWHATSAPP.PATH.Add(Application.StartupPath & "\Outstanding_" & CMBNAME.Text.Trim & ".pdf")
        '    OBJWHATSAPP.FILENAME.Add("Outstanding" & PARTYNAME & ".pdf")
        '    OBJWHATSAPP.ShowDialog()
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub GRIDREPORT_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles GRIDREPORT.CellFormatting
        Try
            If e.ColumnIndex > GNAME.Index AndAlso e.Value = 0 Then
                e.Value = String.Empty
                e.FormattingApplied = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDREPORT_SortCompare(sender As Object, e As DataGridViewSortCompareEventArgs) Handles GRIDREPORT.SortCompare
        Try
            e.SortResult = CDbl(e.CellValue1).CompareTo(CDbl(e.CellValue2))
            e.Handled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTITEM_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTITEM.CheckedChanged
        Try
            If GRIDITEMDETAILS.Visible = True Then
                For i As Integer = 0 To GRIDITEM.RowCount - 1
                    Dim dtrow As DataRow = GRIDITEM.GetDataRow(i)
                    dtrow("CHK") = CHKSELECTITEM.Checked
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTPARTY_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTPARTY.CheckedChanged
        Try
            If gridpartydetails.Visible = True Then
                For i As Integer = 0 To Gridparty.RowCount - 1
                    Dim dtrow As DataRow = Gridparty.GetDataRow(i)
                    dtrow("CHK") = CHKSELECTPARTY.Checked
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTAGENT_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTAGENT.CheckedChanged
        Try
            If GRIDAGENTDETAILS.Visible = True Then
                For i As Integer = 0 To GRIDAGENT.RowCount - 1
                    Dim dtrow As DataRow = GRIDAGENT.GetDataRow(i)
                    dtrow("CHK") = CHKSELECTAGENT.Checked
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class