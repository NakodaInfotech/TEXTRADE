Imports System.ComponentModel
Imports BL


Public Class UpdateBillDispute
    Public EDIT As Boolean

    Dim saleregabbr, salereginitial, TEMPSALEREG As String, saleregid As Integer
    Dim PURREGABBR, PURREGINITIAL As String, PURREGID As Integer
    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            EDIT = False
            cmbregister.Enabled = True
            TXTINVNO.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CLEAR()
        Try
            EP.Clear()
            TXTINVNO.Clear()
            cmbregister.Text = ""
            TXTSPECIALREMARKS.Clear()
            CHKBILLDISPUTE.Checked = False
            CHKBILLCHECKED.Checked = False
            CMBTYPE.SelectedIndex = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub UpdateBillDispute_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CLEAR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPDATE_Click(sender As Object, e As EventArgs) Handles CMDUPDATE.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim DTREGISTER As DataTable = OBJCMN.SEARCH("ISNULL(register_id,0) AS REGID", "", " REGISTERMASTER", " AND register_name = '" & cmbregister.Text.Trim & "' AND REGISTER_YEARID = " & YearId)
            Dim DT As New DataTable
            If CMBTYPE.Text = "SALE INVOICE" Then
                DT = OBJCMN.Execute_Any_String(" UPDATE INVOICEMASTER SET INVOICEMASTER.INVOICE_CHECKED = " & CHKBILLCHECKED.CheckState & ", INVOICE_DISPUTE = " & CHKBILLDISPUTE.CheckState & ", INVOICE_SPECIALREMARKS = '" & TXTSPECIALREMARKS.Text.Trim & "' WHERE INVOICEMASTER.INVOICE_NO = " & Val(TXTINVNO.Text.Trim) & " And INVOICEMASTER.INVOICE_REGISTERID = " & DTREGISTER.Rows(0).Item("REGID") & " And INVOICEMASTER.INVOICE_YEARID = " & YearId, "", "")
            ElseIf CMBTYPE.Text = "PURCHASE INVOICE" Then
                DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASEMASTER SET PURCHASEMASTER.BILL_CHECKED = " & CHKBILLCHECKED.CheckState & ", BILL_DISPUTE = " & CHKBILLDISPUTE.CheckState & ", BILL_SPLREMARKS = '" & TXTSPECIALREMARKS.Text.Trim & "' WHERE PURCHASEMASTER.BILL_NO = " & Val(TXTINVNO.Text.Trim) & " And PURCHASEMASTER.BILL_REGISTERID = " & DTREGISTER.Rows(0).Item("REGID") & " And PURCHASEMASTER.BILL_YEARID = " & YearId, "", "")
            End If
            MsgBox("Bill Dispute Updated Successfully")
            CLEAR()
            TXTINVNO.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If TXTINVNO.Text.Trim.Length = 0 Then
            EP.SetError(TXTINVNO, "Enter Invoice No")
            bln = False
        End If

        Dim OBJCMN As New ClsCommon
        Dim DTTABLE As New DataTable

        Return bln
    End Function

    Private Sub UpdateBillDispute_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbregister_Enter(sender As Object, e As EventArgs) Handles cmbregister.Enter
        Try
            If CMBTYPE.Text.Trim = "SALE INVOICE" Then
                If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'SALE'")

                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.SEARCH(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'SALE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    cmbregister.Text = dt.Rows(0).Item(0).ToString
                End If
            Else
                If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'PURCHASE'")

                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.SEARCH(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'PURCHASE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    cmbregister.Text = dt.Rows(0).Item(0).ToString
                End If
            End If
            'getmax_INVOICE_no()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub cmbregister_Validating(sender As Object, e As CancelEventArgs) Handles cmbregister.Validating
        Try
            If CMBTYPE.Text.Trim = "SALE INVOICE" Then
                If cmbregister.Text.Trim.Length > 0 And EDIT = False Then
                    cmbregister.Text = UCase(cmbregister.Text)
                    Dim clscommon As New ClsCommon
                    Dim dt As DataTable
                    dt = clscommon.SEARCH(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        saleregabbr = dt.Rows(0).Item(0).ToString
                        salereginitial = dt.Rows(0).Item(1).ToString
                        saleregid = dt.Rows(0).Item(2)
                        'getmax_INVOICE_no()
                        cmbregister.Enabled = False
                    Else
                        MsgBox("Register Not Present, Add New from Master Module")
                        e.Cancel = True
                    End If
                End If
            Else
                If cmbregister.Text.Trim.Length > 0 And EDIT = False Then
                    'clear()
                    cmbregister.Text = UCase(cmbregister.Text)
                    Dim clscommon As New ClsCommon
                    Dim dt As DataTable
                    dt = clscommon.SEARCH(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'PURCHASE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        PURREGABBR = dt.Rows(0).Item(0).ToString
                        PURREGINITIAL = dt.Rows(0).Item(1).ToString
                        PURREGID = dt.Rows(0).Item(2)
                        'getmax_BILL_no()
                        cmbregister.Enabled = False
                    Else
                        MsgBox("Register Not Present, Add New from Master Module")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    'Private Sub cmbregister_Validated(sender As Object, e As EventArgs) Handles cmbregister.Validated
    '    Try
    '        'GET DEFAUKLT SCREENTYPE
    '        If EDIT = False And cmbregister.Text.Trim <> "" Then
    '            Dim OBJCMN As New ClsCommon
    '            Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(DTYPE_PURSCREENTYPE,'GOODS PURCHASE') AS SERVICETYPE", "", " DEFAULTPURSCREENTYPE INNER JOIN REGISTERMASTER ON DTYPE_PURREGID = REGISTER_ID ", " AND REGISTER_NAME = '" & cmbregister.Text & "' AND DTYPE_YEARID = " & YearId)
    '            If DT.Rows.Count > 0 Then
    '                CMBSERVICETYPE.Text = DT.Rows(0).Item("SERVICETYPE")
    '                If DT.Rows(0).Item("SERVICETYPE") = "JOB CHARGES" Then TXTSACCODE.Text = "998821" Else TXTSACCODE.Clear()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub TXTINVNO_Validated(sender As Object, e As EventArgs) Handles TXTINVNO.Validated
        Try
            If TXTINVNO.Text.Trim.Length > 0 Then

                If CMBTYPE.Text = "SALE INVOICE" Then

                    'GET DYEING NAME
                    'Dim OBJCMN1 As New ClsCommon
                    'Dim DTREGISTER As DataTable = OBJCMN1.SEARCH("ISNULL(register_id,0) AS REGID", "", " REGISTERMASTER", " AND register_name = '" & cmbregister.Text.Trim & "' AND REGISTER_YEARID = " & YearId)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH("  ISNULL(INVOICEMASTER.INVOICE_CHECKED, 0) AS BILLCHECKED, ISNULL(INVOICEMASTER.INVOICE_DISPUTE, 0) AS BILLDISPUTE, ISNULL(INVOICEMASTER.INVOICE_SPECIALREMARKS, '') AS SPECIALREMARKS, ISNULL(REGISTERMASTER.register_name, '') AS REGISTERNAME", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id AND INVOICEMASTER.INVOICE_YEARID = REGISTERMASTER.register_yearid", " AND INVOICEMASTER.INVOICE_NO = " & Val(TXTINVNO.Text.Trim) & " AND REGISTERMASTER.register_name = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        If DT.Rows(0).Item("BILLCHECKED") = 0 Then CHKBILLCHECKED.Checked = False Else CHKBILLCHECKED.Checked = True
                        If DT.Rows(0).Item("BILLDISPUTE") = 0 Then CHKBILLDISPUTE.Checked = False Else CHKBILLDISPUTE.Checked = True
                        'CHKBILLCHECKED = DT.Rows(0).Item("BILLCHECKED")
                        'CHKBILLDISPUTE = DT.Rows(0).Item("BILLDISPUTE")
                        TXTSPECIALREMARKS.Text = DT.Rows(0).Item("SPECIALREMARKS")

                    End If

                ElseIf CMBTYPE.Text.Trim = "PURCHASE INVOICE" Then

                    'GET JOBBER NAME
                    Dim OBJCMN1 As New ClsCommon
                    Dim DTREGISTER As DataTable = OBJCMN1.SEARCH("ISNULL(register_id,0) AS REGID", "", " REGISTERMASTER", " AND register_name = '" & cmbregister.Text.Trim & "' AND REGISTER_YEARID = " & YearId)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH("  ISNULL(PURCHASEMASTER.BILL_CHECKED, 0) AS BILLCHECKED, ISNULL(PURCHASEMASTER.BILL_DISPUTE, 0) AS BILLDISPUTE, ISNULL(PURCHASEMASTER.BILL_SPLREMARKS, '') AS SPECIALREMARKS, ISNULL(REGISTERMASTER.register_name, '') AS REGISTERNAME ", "", "PURCHASEMASTER INNER JOIN REGISTERMASTER ON PURCHASEMASTER.BILL_REGISTERID = REGISTERMASTER.register_id AND PURCHASEMASTER.BILL_YEARID = REGISTERMASTER.register_yearid", " AND PURCHASEMASTER.BILL_NO = " & Val(TXTINVNO.Text.Trim) & " AND REGISTERMASTER.register_name = '" & cmbregister.Text.Trim & "' AND PURCHASEMASTER.BILL_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        If DT.Rows(0).Item("BILLCHECKED") = 0 Then CHKBILLCHECKED.Checked = False Else CHKBILLCHECKED.Checked = True
                        If DT.Rows(0).Item("BILLDISPUTE") = 0 Then CHKBILLDISPUTE.Checked = False Else CHKBILLDISPUTE.Checked = True
                        TXTSPECIALREMARKS.Text = DT.Rows(0).Item("SPECIALREMARKS")

                    End If

                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class