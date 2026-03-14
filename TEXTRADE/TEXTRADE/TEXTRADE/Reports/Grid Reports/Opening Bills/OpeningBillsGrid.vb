Imports System.ComponentModel
Imports BL
Imports DevExpress.XtraEditors.Filtering

Public Class OpeningBillsGrid
    Public EDIT As Boolean

    Sub FILLGRIDOPENING()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" OPENINGBILL.BILL_GRIDSRNO AS GRIDSRNO, OPENINGBILL.BILL_TYPE AS BILLTYPE, OPENINGBILL.BILL_NO AS BILLNO, OPENINGBILL.BILL_YEAR AS YEAR, OPENINGBILL.BILL_DATE AS BILLDATE, OPENINGBILL.BILL_CRDAYS AS CRDAYS, OPENINGBILL.BILL_DUEDATE AS DUEDATE, ISNULL(AGENTLEDGERS.ACC_CMPNAME, '') AS AGENT, OPENINGBILL.BILL_NARRATION AS NARRATION, OPENINGBILL.BILL_DISPUTE AS DISPUTE, OPENINGBILL.BILL_AMT AS AMT, OPENINGBILL.BILL_AMTPAIDREC AS AMTPAIDREC, OPENINGBILL.BILL_EXTRAAMT AS EXTRAAMT, OPENINGBILL.BILL_RETURN AS [RETURN], OPENINGBILL.BILL_BALANCE AS BALANCE, ISNULL(REGISTER_NAME,'') AS REGNAME, ISNULL(BILL_PRINTINITIALS,'') AS PRINTINITIALS, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '') AS DELIVERYAT, ISNULL(OPENINGBILL.BILL_PCS, 0) AS PCS, ISNULL(OPENINGBILL.BILL_MTRS, 0) AS MTRS, ISNULL(OPENINGBILL.BILL_TOTALAMT, 0) AS TOTALAMT, ISNULL(OPENINGBILL.BILL_CHARGES, 0) AS CHARGES, ISNULL(OPENINGBILL.BILL_TAXABLEAMT, 0) AS TAXABLEAMT, ISNULL(OPENINGBILL.BILL_CGSTPER, 0) AS CGSTPER, ISNULL(OPENINGBILL.BILL_CGSTAMT, 0) AS CGSTAMT, ISNULL(OPENINGBILL.BILL_SGSTPER, 0) AS SGSTPER, ISNULL(OPENINGBILL.BILL_SGSTAMT, 0) AS SGSTAMT, ISNULL(OPENINGBILL.BILL_IGSTPER, 0) AS IGSTPER, ISNULL(OPENINGBILL.BILL_IGSTAMT, 0) AS IGSTAMT, ISNULL(OPENINGBILL.BILL_GRANDTOTAL, 0) AS GRANDTOTAL, ISNULL(OPENINGBILL.BILL_CD, 0) AS CD, ISNULL(OPENINGBILL.BILL_HOLDINTCALC, 0) AS HOLDINTCALC, ISNULL(OPENINGBILL.BILL_COMPLAINT,'') AS COMPLAINT, ISNULL(OPENINGBILL.BILL_COMPLAINTBY,'') AS COMPLAINTBY, ISNULL(OPENINGBILL.BILL_COMPLAINTDATE,'') AS COMPLAINTDATE, ISNULL(OPENINGBILL.BILL_ORDERNO,'') AS ORDERNO, ISNULL(OPENINGBILL.BILL_CHANGEDATE,OPENINGBILL.BILL_DATE) AS CHANGEDATE  ", "", " OPENINGBILL INNER JOIN LEDGERS ON OPENINGBILL.BILL_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON OPENINGBILL.BILL_AGENTID = AGENTLEDGERS.Acc_id INNER JOIN REGISTERMASTER ON REGISTER_ID = BILL_REGISTERID LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON OPENINGBILL.BILL_DELIVERYATID = DELIVERYLEDGERS.Acc_id", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "'  AND BILL_YEARID = " & YearId & "  ORDER BY OPENINGBILL.BILL_GRIDSRNO ")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningBillsGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningBillsGrid_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'FILLGRIDOPENING()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRIDOPENING()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Opening Bills Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Opening Bills Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Opening Bills Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Eway Entry Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            'OPEN ALL LEDGERS
            If CMBNAME.Text.Trim = "" Then fillledger(CMBNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS')and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            'If cmbname.Text.Trim <> "" Then ledgervalidate(cmbname, CMBACCCODE, e, Me, txtadd, " and (groupmaster.group_SECONDARY = 'Sundry Debtors' or groupmaster.group_SECONDARY = 'Indirect Income' or groupmaster.group_SECONDARY = 'Direct Income') and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
            If CMBNAME.Text.Trim <> "" Then CHECKLEDGER(CMBNAME, CMBACCCODE, e, Me, TXTADD, " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS')and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
            'If TXTBILLNO.Text.Trim = "" And CMBNAME.Text.Trim <> "" Then
            FILLGRIDOPENING()
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CHECKLEDGER(ByRef cmbname As ComboBox, ByVal CMBACCCODE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, ByRef txtadd As System.Windows.Forms.TextBox, ByVal WHERECLAUSE As String, Optional ByVal GROUPNAME As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbname.Text.Trim <> "" Then
                uppercase(cmbname)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("acc_add, isnull( ACC_CODE,''), REGISTER_NAME AS REGISTERNAME", "", " LEDGERS INNER JOIN GROUPMASTER ON GROUPMASTER.group_id = LEDGERS.Acc_groupid AND GROUPMASTER.group_cmpid = LEDGERS.Acc_cmpid AND GROUPMASTER.group_locationid = LEDGERS.Acc_locationid AND GROUPMASTER.group_yearid = LEDGERS.Acc_yearid LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id AND LEDGERS.Acc_cmpid = REGISTERMASTER.register_cmpid AND LEDGERS.Acc_locationid = REGISTERMASTER.register_locationid AND LEDGERS.Acc_yearid = REGISTERMASTER.register_yearid ", " and acc_cmpname = '" & cmbname.Text.Trim & "' AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId & WHERECLAUSE)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbname.Text.Trim
                    Dim tempmsg As Integer = MessageBox.Show("Account not present, Please take another name?", "TEXTRADE", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    e.Cancel = True
                Else
                    txtadd.Text = dt.Rows(0).Item(0).ToString
                    CMBACCCODE.Text = dt.Rows(0).Item(1)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub CMBNAME_Validated(sender As Object, e As EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" GROUP_SECONDARY AS SECONDARY, LEDGERS.ACC_OPBAL AS OPBAL, LEDGERS.ACC_DRCR AS DRCR", "", " LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID  ", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then

                    txtopening.Text = Format(Val(DT.Rows(0).Item("OPBAL")), "0.00")
                    lbldrcropening.Text = DT.Rows(0).Item("DRCR")

                    'If DT.Rows(0).Item(0) = "Sundry Creditors" Then
                    '    CMBTYPE.Text = "PURCHASE"
                    'Else
                    '    CMBTYPE.Text = "SALE"
                    'End If
                    'EDIT = True
                End If

                CMBNAME.Enabled = False
                CMBACCCODE.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class