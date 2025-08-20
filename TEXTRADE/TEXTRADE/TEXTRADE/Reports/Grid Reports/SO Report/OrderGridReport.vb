
Imports System.ComponentModel
Imports BL
Imports DevExpress.XtraGrid.Views.Base

Public Class OrderGridReport
    Dim edit As Boolean
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String
    Public SOCLAUSE As String = ""
    Public ORDERTYPE As String = ""
    Public FRMSTRING As String


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        FILLCMB()
        FILLDETAILGRID()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub OrderGridReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If FRMSTRING = "SO" Then
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If

            If CMBAGENT.Text.Trim = "" Then FILLNAME(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry CREDITORS' AND ACC_TYPE='AGENT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            OrderGridReport_Load(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim NAMECLAUSE As String = ""
            Dim ITEMCLAUSE As String = ""
            Dim ORDERCLAUSE As String = ""
            Dim WHERECLAUSE As String = ""
            Dim PERIOD As String = ""
            Dim SOCLAUSE As String = ""


            If FRMSTRING = "SO" Then
                WHERECLAUSE = " ALLSALEORDER.SO_yearid=" & YearId
            Else
                WHERECLAUSE = " ALLPURCHASEORDER.PO_yearid=" & YearId
            End If
            ' Apply date filter
            If chkdate.Checked = True Then
                getFromToDate() ' This should set fromD and toD

                If FRMSTRING = "SO" Then WHERECLAUSE &= " AND ALLSALEORDER.so_date BETWEEN " & fromD & " AND " & toD & "" Else WHERECLAUSE &= " AND ALLPURCHASEORDER.so_date BETWEEN " & fromD & " AND " & toD & ""
            Else
                If FRMSTRING = "SO" Then PERIOD = " AND ALLSALEORDER.so_date BETWEEN '" & Format(AccFrom, "yyyy-MM-dd") & "' AND '" & Format(AccTo, "yyyy-MM-dd") & "'" Else PERIOD = " AND ALLPURCHASEORDER.so_date BETWEEN '" & Format(AccFrom, "yyyy-MM-dd") & "' AND '" & Format(AccTo, "yyyy-MM-dd") & "'"
            End If
            If FRMSTRING = "SO" Then
                If CMBNAME.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " and LEDGERS.ACC_CMPNAME='" & CMBNAME.Text.Trim & "'"
                If CMBAGENT.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " and agent.ACC_CMPNAME='" & CMBAGENT.Text.Trim & "'"
                If CMBCATEGORY.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMMASTER.ITEM_CATEGORYID = (SELECT CATEGORY_ID FROM CATEGORYMASTER WHERE CATEGORY_NAME = '" & CMBCATEGORY.Text.Trim & "'AND category_yearid=" & YearId & ")"
                If WHERECLAUSE <> "" Then
                    SOCLAUSE = SOCLAUSE & WHERECLAUSE
                End If
                If PERIOD <> "" Then
                    SOCLAUSE = SOCLAUSE & PERIOD
                End If
                'OPEN ORDERVSSTOCK REPORT

                If RDBPENDING.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLSALEORDER_DESC.BALANCE > 0 AND ALLSALEORDER_DESC.SO_CLOSED='FALSE' "
                If RDBCOMPLETE.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLSALEORDER_DESC.BALANCE <= 0 AND ALLSALEORDER_DESC.SO_CLOSED='FALSE'"
                If RDBCLOSED.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLSALEORDER_DESC.SO_CLOSED='TRUE' "


                'FOR NAME
                'gridbill.ClearColumnsFilter()
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

                'FOR ORDERNO
                GRIDBILLORDER.ClearColumnsFilter()
                For i As Integer = 0 To GRIDBILLORDER.RowCount - 1
                    Dim dtrow As DataRow = GRIDBILLORDER.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If ORDERCLAUSE = "" Then
                            ORDERCLAUSE = " AND (ALLSALEORDER.SO_NO = " & Val(dtrow("ORDERNO"))
                        Else
                            ORDERCLAUSE = ORDERCLAUSE & " OR ALLSALEORDER.SO_NO = " & Val(dtrow("ORDERNO"))
                        End If
                    End If
                Next
                If ORDERCLAUSE <> "" Then
                    ORDERCLAUSE = ORDERCLAUSE & ")"
                    SOCLAUSE = SOCLAUSE & ORDERCLAUSE
                End If

                'FOR ITEMNAME
                'GRIDBILLITEM.ClearColumnsFilter()
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

            ElseIf FRMSTRING = "PO" Then

                If CMBNAME.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " and LEDGERS.ACC_CMPNAME='" & CMBNAME.Text.Trim & "'"
                If CMBAGENT.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " and agent.ACC_CMPNAME='" & CMBAGENT.Text.Trim & "'"
                If CMBCATEGORY.Text <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMMASTER.ITEM_CATEGORYID = (SELECT CATEGORY_ID FROM CATEGORYMASTER WHERE CATEGORY_NAME = '" & CMBCATEGORY.Text.Trim & "'AND category_yearid=" & YearId & ")"
                If WHERECLAUSE <> "" Then
                    SOCLAUSE = SOCLAUSE & WHERECLAUSE
                End If
                If PERIOD <> "" Then
                    SOCLAUSE = SOCLAUSE & PERIOD
                End If
                'OPEN ORDERVSSTOCK REPORT

                If RDBPENDING.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLPURCHASEORDER_DESC.BALANCE > 0 AND ALLPURCHASEORDER_DESC.PO_CLOSED='FALSE' "
                If RDBPENDING.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLPURCHASEORDER_DESC.BALANCE > 0 AND ALLPURCHASEORDER_DESC.PO_CLOSED='FALSE' "
                If RDBCOMPLETE.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLPURCHASEORDER_DESC.BALANCE <= 0 AND ALLPURCHASEORDER_DESC.PO_CLOSED='FALSE'"
                If RDBCLOSED.Checked = True Then SOCLAUSE = SOCLAUSE & " AND ALLPURCHASEORDER_DESC.PO_CLOSED='TRUE' "


                'FOR NAME
                'gridbill.ClearColumnsFilter()
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

                'FOR ORDERNO
                GRIDBILLORDER.ClearColumnsFilter()
                For i As Integer = 0 To GRIDBILLORDER.RowCount - 1
                    Dim dtrow As DataRow = GRIDBILLORDER.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If ORDERCLAUSE = "" Then
                            ORDERCLAUSE = " AND (ALLPURCHASEORDER.PO_NO = " & Val(dtrow("ORDERNO"))
                        Else
                            ORDERCLAUSE = ORDERCLAUSE & " OR ALLPURCHASEORDER.PO_NO = " & Val(dtrow("ORDERNO"))
                        End If
                    End If
                Next
                If ORDERCLAUSE <> "" Then
                    ORDERCLAUSE = ORDERCLAUSE & ")"
                    SOCLAUSE = SOCLAUSE & ORDERCLAUSE
                End If

                'FOR ITEMNAME
                'GRIDBILLITEM.ClearColumnsFilter()
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

            End If








            GRIDSO.RowCount = 0
            Dim OBJCMN As New ClsCommon
            Dim LASTITEMNAME As String = ""
            Dim TOTALPCS, TOTALDELPCS, TOTALBALPCS As Double
            Dim GTOTALPCS, GTOTALDELPCS, GTOTALBALPCS As Double
            Dim COMPLETIONDAYS As Integer = 0
            Dim DT As New DataTable

            If ORDERTYPE = "SO" Then
                DT = OBJCMN.SEARCH(" ITEMMASTER.item_name AS ITEMNAME, '' AS MILLNAME, ALLSALEORDER.so_no AS SONO, ALLSALEORDER.so_date AS SODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname,'') AS AGENTNAME, ALLSALEORDER.so_NOTE AS NOTE, ALLSALEORDER_DESC.SO_QTY AS PCS, ALLSALEORDER_DESC.SO_RECDQTY AS OUTPCS, ALLSALEORDER_DESC.BALANCE AS BALPCS, ALLSALEORDER_DESC.SO_RATE AS RATE, SO_DAYS AS [DAYS], ISNULL(ITEMMASTER.ITEM_REORDER,0) AS PERDAYPROD ", "", " ALLSALEORDER INNER JOIN ALLSALEORDER_DESC ON ALLSALEORDER.so_no = ALLSALEORDER_DESC.SO_NO AND ALLSALEORDER.TYPE = ALLSALEORDER_DESC.TYPE AND ALLSALEORDER.SO_YEARID = ALLSALEORDER_DESC.SO_YEARID INNER JOIN ITEMMASTER ON ALLSALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id INNER JOIN LEDGERS ON ALLSALEORDER.so_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON ALLSALEORDER.so_Agentid = AGENTLEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON ALLSALEORDER_DESC.SO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON ALLSALEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_ID", SOCLAUSE & " ORDER BY ITEMMASTER.item_name, ALLSALEORDER.SO_DATE, ALLSALEORDER.SO_NO")
            ElseIf ORDERTYPE = "PO" Then
                DT = OBJCMN.SEARCH(" ITEMMASTER.item_name AS ITEMNAME, '' AS MILLNAME, ALLPURCHASEORDER.PO_no AS SONO, ALLPURCHASEORDER.PO_date AS SODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname,'') AS AGENTNAME, ALLPURCHASEORDER.PO_NOTE AS NOTE, ALLPURCHASEORDER_DESC.PO_QTY AS PCS, ALLPURCHASEORDER_DESC.PO_RECDQTY AS OUTPCS, ALLPURCHASEORDER_DESC.BALANCE AS BALPCS, ALLPURCHASEORDER_DESC.PO_RATE AS RATE, PO_DAYS AS [DAYS], ISNULL(ITEMMASTER.ITEM_REORDER,0) AS PERDAYPROD ", "", " ALLPURCHASEORDER INNER JOIN ALLPURCHASEORDER_DESC ON ALLPURCHASEORDER.PO_no = ALLPURCHASEORDER_DESC.PO_NO AND ALLPURCHASEORDER.TYPE = ALLPURCHASEORDER_DESC.TYPE AND ALLPURCHASEORDER.PO_YEARID = ALLPURCHASEORDER_DESC.PO_YEARID INNER JOIN ITEMMASTER ON ALLPURCHASEORDER_DESC.PO_ITEMID = ITEMMASTER.item_id INNER JOIN LEDGERS ON ALLPURCHASEORDER.PO_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON ALLPURCHASEORDER.PO_Agentid = AGENTLEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON ALLPURCHASEORDER_DESC.PO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON ALLPURCHASEORDER_DESC.PO_COLORID = COLORMASTER.COLOR_ID", SOCLAUSE & " ORDER BY ITEMMASTER.item_name, ALLPURCHASEORDER.PO_DATE, ALLPURCHASEORDER.PO_NO")
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
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If FRMSTRING = "SO" Then
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(sender As Object, e As EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then FILLNAME(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND ACC_TYPE='AGENT'")
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
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCATEGORY_Enter(sender As Object, e As EventArgs) Handles CMBCATEGORY.Enter
        Try
            If CMBCATEGORY.Text.Trim = "" Then fillCATEGORY(CMBCATEGORY, edit)
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
            OrderGridReport_Load(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBACCOUNT_CheckedChanged(sender As Object, e As EventArgs) Handles RBACCOUNT.CheckedChanged
        Try

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
    Sub FILLDETAILGRID()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If RBACCOUNT.Checked = True Then
                gridbill.Columns("NAME").Visible = True
                DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, GROUPMASTER.GROUP_NAME AS GROUPNAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA, ISNULL(SALESMANMASTER.SALESMAN_NAME,'') AS SALESMAN ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.ACC_AREAID = AREAMASTER.AREA_ID LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.ACC_ID  ", " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            ElseIf RBAGENT.Checked = True Then
                gridbill.Columns("NAME").Visible = False
                DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, GROUPMASTER.GROUP_NAME AS GROUPNAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA ", " ", " LEDGERS AS AGENTLEDGERS INNER JOIN GROUPMASTER ON AGENTLEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON AGENTLEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON AGENTLEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON AGENTLEDGERS.ACC_AREAID = AREAMASTER.AREA_ID ", " AND AGENTLEDGERS.ACC_TYPE = 'AGENT' AND (AGENTLEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY AGENTLEDGERS.Acc_cmpname")
            End If

            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
            End If
            gridbilldetails.DataSource = DT

            Dim DTITEM As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY ", " ", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.ITEM_CATEGORYID = CATEGORYMASTER.CATEGORY_ID", " AND ITEMMASTER.ITEM_YEARID = '" & YearId & "' ORDER BY ITEMMASTER.ITEM_NAME")
            If DTITEM.Rows.Count > 0 Then
                GRIDBILLITEM.FocusedRowHandle = GRIDBILLITEM.RowCount - 1
            End If
            GRIDBILLDETAILSITEM.DataSource = DTITEM



            If FRMSTRING = "SO" Then
                DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ALLSALEORDER.SO_NO AS ORDERNO ", " ", " ALLSALEORDER ", " AND ALLSALEORDER.SO_YEARID = " & YearId & " ORDER BY ALLSALEORDER.SO_NO ")
            ElseIf FRMSTRING = "PO" Then
                DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ALLSALEORDER.SO_NO AS ORDERNO ", " ", " ALLSALEORDER ", " AND ALLSALEORDER.SO_YEARID = " & YearId & " ORDER BY ALLSALEORDER.SO_NO ")
            End If
            If DT.Rows.Count > 0 Then
                GRIDBILLORDER.FocusedRowHandle = GRIDBILLORDER.RowCount - 1
            End If
            GRIDBILLDETAILSORDER.DataSource = DT

            'If ClientName = "MASHOK" Then DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, MILLMASTER.MILL_NAME AS MILLNAME ", " ", " MILLMASTER ", " AND MILLMASTER.MILL_YEARID = " & YearId & " ORDER BY MILLMASTER.MILL_NAME ")
            'If DT.Rows.Count > 0 Then
            '    GRIDMILL.FocusedRowHandle = GRIDMILL.RowCount - 1
            'End If
            'GRIDMILLDETAILS.DataSource = DT


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RDBALL_CheckedChanged(sender As Object, e As EventArgs) Handles RDBALL.CheckedChanged, RDBCLOSED.CheckedChanged, RDBCOMPLETE.CheckedChanged, RDBPENDING.CheckedChanged
        If sender IsNot Nothing AndAlso CType(sender, RadioButton).Checked Then
            Try
                OrderGridReport_Load(sender, e)
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub
    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, dtfrom.Value)
        a2 = DatePart(DateInterval.Month, dtfrom.Value)
        a3 = DatePart(DateInterval.Year, dtfrom.Value)
        fromD = "'" & a3 & "-" & a2 & "-" & a1 & "'"

        a11 = DatePart(DateInterval.Day, dtto.Value)
        a12 = DatePart(DateInterval.Month, dtto.Value)
        a13 = DatePart(DateInterval.Year, dtto.Value)
        toD = "'" & a13 & "-" & a12 & "-" & a11 & "'"
    End Sub
End Class