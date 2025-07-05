Imports System.ComponentModel
Imports BL
Public Class StoreOrderFilter

    Public FRMSTRING As String
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String
    Dim edit As Boolean
    Private Sub StoreOrderFilter_Enter(sender As Object, e As EventArgs) Handles Me.Enter
        Try
            If FRMSTRING = "SO" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, dtfrom.Value)
        a2 = DatePart(DateInterval.Month, dtfrom.Value)
        a3 = DatePart(DateInterval.Year, dtfrom.Value)
        fromD = "(" & a3 & "," & a2 & "," & a1 & ")"

        a11 = DatePart(DateInterval.Day, dtto.Value)
        a12 = DatePart(DateInterval.Month, dtto.Value)
        a13 = DatePart(DateInterval.Year, dtto.Value)
        toD = "(" & a13 & "," & a12 & "," & a11 & ")"
    End Sub
    Private Sub StoreOrderFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            fillcmb()
            FILLGRID()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillcmb()
        Try
            If FRMSTRING = "SO" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdshow_Click(sender As Object, e As EventArgs) Handles cmdshow.Click
        Try
            Dim NAMECLAUSE As String = ""
            Dim ITEMCLAUSE As String = ""
            Dim DESIGNCLAUSE As String = ""
            Dim COLORCLAUSE As String = ""
            Dim ORDERCLAUSE As String = ""

            Dim OBJGRN As New SaleInvoiceDesign
            OBJGRN.MdiParent = MDIMain
            If FRMSTRING = "SO" Then
                OBJGRN.WHERECLAUSE = " {ALLSALEORDER.SO_yearid}=" & YearId
            Else
                OBJGRN.WHERECLAUSE = " {ALLPURCHASEORDER.PO_yearid}=" & YearId
            End If

            If chkdate.Checked = True Then
                getFromToDate()
                OBJGRN.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
            Else
                OBJGRN.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If

            If FRMSTRING = "SO" Then
                If CMBNAME.Text <> "" Then OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {LEDGERS.ACC_CMPNAME}='" & CMBNAME.Text.Trim & "'"
                If RDBPARTY.Checked = True Then
                    OBJGRN.FRMSTRING = "SOSTATUS"
                ElseIf RDBPARTYDTLS.Checked = True Then
                    OBJGRN.FRMSTRING = "SOSTATUSDTLS"


                    'OPEN ORDERVSSTOCK REPORT
                    Dim OBJORDER As New SaleOrderVsStockGridReport
                    OBJORDER.MdiParent = MDIMain
                    OBJORDER.SOCLAUSE = " AND ALLSALEORDER_DESC.SO_CLOSED = 'FALSE' "

                    'FOR NAME
                    gridbill.ClearColumnsFilter()
                    For i As Integer = 0 To gridbill.RowCount - 1
                        Dim dtrow As DataRow = gridbill.GetDataRow(i)
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
                        OBJORDER.SOCLAUSE = OBJORDER.SOCLAUSE & NAMECLAUSE
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
                        OBJORDER.SOCLAUSE = OBJORDER.SOCLAUSE & ITEMCLAUSE
                    End If

                    OBJORDER.Show()
                    Exit Sub

                End If


            Else
                If CMBNAME.Text <> "" Then OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {LEDGERS.ACC_CMPNAME}='" & CMBNAME.Text.Trim & "'"
                If RDBPARTY.Checked = True Then
                    OBJGRN.FRMSTRING = "POSTATUS"
                ElseIf RDBPARTYDTLS.Checked = True Then
                    OBJGRN.FRMSTRING = "POSTATUSDTLS"

                End If

            End If

            If FRMSTRING = "SO" Then
                If RDBPENDING.Checked = True Then
                    OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {@BALANCE} > 0 and {ALLSALEORDER_DESC.SO_Closed}=FALSE "
                    OBJGRN.PENDINGSO = "PENDING"
                End If
                If RDBCOMPLETE.Checked = True Then
                    OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {@BALANCE} <= 0 and {ALLSALEORDER_DESC.SO_Closed}=FALSE "
                    OBJGRN.PENDINGSO = "COMPLETED"
                End If
                If RDBCLOSED.Checked = True Then OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {ALLSALEORDER_DESC.SO_Closed}=true "


                'FOR ORDERNO
                GRIDBILLORDER.ClearColumnsFilter()
                For i As Integer = 0 To GRIDBILLORDER.RowCount - 1
                    Dim dtrow As DataRow = GRIDBILLORDER.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If ORDERCLAUSE = "" Then
                            ORDERCLAUSE = " AND ({ALLSALEORDER.SO_NO} = " & Val(dtrow("ORDERNO"))
                        Else
                            ORDERCLAUSE = ORDERCLAUSE & " OR {ALLSALEORDER.SO_NO} = " & Val(dtrow("ORDERNO"))
                        End If
                    End If
                Next
                If ORDERCLAUSE <> "" Then
                    ORDERCLAUSE = ORDERCLAUSE & ")"
                    OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & ORDERCLAUSE
                End If


            Else
                If RDBPENDING.Checked = True Then
                    OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {@BALANCE} > 0 and {ALLPURCHASEORDER_DESC.PO_Closed}=FALSE "
                    OBJGRN.PENDINGSO = "PENDING"
                End If
                If RDBCOMPLETE.Checked = True Then
                    OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {@BALANCE} <= 0 and {ALLPURCHASEORDER_DESC.PO_Closed}=FALSE "
                    OBJGRN.PENDINGSO = "COMPLETED"
                End If
                If RDBCLOSED.Checked = True Then OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & " and {ALLPURCHASEORDER_DESC.PO_Closed}=true "


                'FOR ORDERNO
                GRIDBILLORDER.ClearColumnsFilter()
                For i As Integer = 0 To GRIDBILLORDER.RowCount - 1
                    Dim dtrow As DataRow = GRIDBILLORDER.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If ORDERCLAUSE = "" Then
                            ORDERCLAUSE = " AND ({ALLPURCHASEORDER.PO_NO} = " & Val(dtrow("ORDERNO"))
                        Else
                            ORDERCLAUSE = ORDERCLAUSE & " OR {ALLPURCHASEORDER.PO_NO} = " & Val(dtrow("ORDERNO"))
                        End If
                    End If
                Next
                If ORDERCLAUSE <> "" Then
                    ORDERCLAUSE = ORDERCLAUSE & ")"
                    OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & ORDERCLAUSE
                End If

            End If

            'FOR PARTYNAME
            gridbill.ClearColumnsFilter()
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If NAMECLAUSE = "" Then
                        NAMECLAUSE = " AND ({LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                    Else
                        NAMECLAUSE = NAMECLAUSE & " OR {LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                    End If
                End If
            Next
            If NAMECLAUSE <> "" Then
                NAMECLAUSE = NAMECLAUSE & ")"
                OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & NAMECLAUSE
            End If


            'FOR ITEMNAME
            GRIDBILLITEM.ClearColumnsFilter()
            For i As Integer = 0 To GRIDBILLITEM.RowCount - 1
                Dim dtrow As DataRow = GRIDBILLITEM.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If ITEMCLAUSE = "" Then
                        ITEMCLAUSE = " AND ({ITEMMASTER.ITEM_NAME} = '" & dtrow("ITEMNAME") & "'"
                    Else
                        ITEMCLAUSE = ITEMCLAUSE & " OR {ITEMMASTER.ITEM_NAME} = '" & dtrow("ITEMNAME") & "'"
                    End If
                End If
            Next
            If ITEMCLAUSE <> "" Then
                ITEMCLAUSE = ITEMCLAUSE & ")"
                OBJGRN.WHERECLAUSE = OBJGRN.WHERECLAUSE & ITEMCLAUSE
            End If


            OBJGRN.POSOFRMSTRING = FRMSTRING
            OBJGRN.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If FRMSTRING = "SO" Then DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, GROUPMASTER.GROUP_NAME AS GROUPNAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA, ISNULL(SALESMANMASTER.SALESMAN_NAME,'') AS SALESMAN ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.ACC_AREAID = AREAMASTER.AREA_ID LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID ", " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname") Else DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, GROUPMASTER.GROUP_NAME AS GROUPNAME, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(STATEMASTER.STATE_NAME,'') AS STATENAME, ISNULL(AREA_NAME,'') AS AREA, ISNULL(SALESMANMASTER.SALESMAN_NAME,'') AS SALESMAN ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.ACC_AREAID = AREAMASTER.AREA_ID LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID ", " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If

            Dim DTITEM As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, STOREITEMMASTER.STOREITEM_NAME AS ITEMNAME ", " ", " STOREITEMMASTER ", " AND STOREITEMMASTER.STOREITEM_YEARID = '" & YearId & "' ORDER BY STOREITEMMASTER.STOREITEM_NAME")
            GRIDBILLDETAILSITEM.DataSource = DTITEM
            If DTITEM.Rows.Count > 0 Then
                GRIDBILLITEM.FocusedRowHandle = GRIDBILLITEM.RowCount - 1
                GRIDBILLITEM.TopRowIndex = GRIDBILLITEM.RowCount - 15
            End If


            If FRMSTRING = "SO" Then DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ALLSALEORDER.SO_NO AS ORDERNO ", " ", " ALLSALEORDER ", " AND ALLSALEORDER.SO_YEARID = " & YearId & " ORDER BY ALLSALEORDER.SO_NO ") Else DT = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK, ALLPURCHASEORDER.PO_NO AS ORDERNO ", " ", " ALLPURCHASEORDER ", " AND ALLPURCHASEORDER.PO_YEARID = " & YearId & " ORDER BY ALLPURCHASEORDER.PO_NO ")
            GRIDBILLDETAILSORDER.DataSource = DT
            If DT.Rows.Count > 0 Then
                GRIDBILLORDER.FocusedRowHandle = GRIDBILLORDER.RowCount - 1
                GRIDBILLORDER.TopRowIndex = GRIDBILLORDER.RowCount - 15
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                dtrow("CHK") = CHKSELECTALL.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CHKSELECTITEM_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTITEM.CheckedChanged
        Try
            For i As Integer = 0 To GRIDBILLITEM.RowCount - 1
                Dim dtrow As DataRow = GRIDBILLITEM.GetDataRow(i)
                dtrow("CHK") = CHKSELECTITEM.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTORDER_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTORDER.CheckedChanged
        Try
            For i As Integer = 0 To GRIDBILLORDER.RowCount - 1
                Dim dtrow As DataRow = GRIDBILLORDER.GetDataRow(i)
                dtrow("CHK") = CHKSELECTORDER.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StoreOrderFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class