
Imports BL

Public Class RCMSelfInvoiceGSTDetails

    Public WHERECLAUSE As String = ""

    Private Sub NonPurchaseDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" NONPURCHASE.NP_NO AS NPNO, REGISTERMASTER.register_name AS REGNAME, LEDGERS.Acc_cmpname AS NAME, LEDGERS.ACC_GSTIN AS GSTIN, NONPURCHASE.NP_REFNO AS PARTYBILLNO, NONPURCHASE.NP_PARTYBILLDATE AS PARTYBILLDATE, ISNULL(STATEMASTER.state_name, '') AS STATENAME, ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR(20)), '') AS STATECODE, NONPURCHASE.NP_TOTALTAXABLEAMT AS TOTALTAXABLEAMT, NONPURCHASE.NP_TOTALCGSTAMT AS TOTALCGSTAMT, NONPURCHASE.NP_TOTALSGSTAMT AS TOTALSGSTAMT, NONPURCHASE.NP_TOTALIGSTAMT AS TOTALIGSTAMT, NONPURCHASE.NP_GRANDTOTAL AS GRANDTOTAL, NONPURCHASE.NP_SELFINVNO AS SELFINVNO, NONPURCHASE.NP_SELFINVPRINTINITIALS AS SELFINVPRINTINITIALS ", "", " NONPURCHASE INNER JOIN LEDGERS ON NONPURCHASE.NP_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON NONPURCHASE.NP_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id ", " AND NONPURCHASE.NP_RCM = 'TRUE' AND NONPURCHASE.NP_SELFINVNO > 0 AND NONPURCHASE.NP_YEARID = " & YearId & WHERECLAUSE & " ORDER BY NONPURCHASE.NP_PARTYBILLDATE, NONPURCHASE.NP_SELFINVNO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDPRINT_Click(sender As Object, e As EventArgs) Handles CMDPRINT.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\RCM Self Invoice Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = ""
            PERIOD = AccFrom & " - " & AccTo

            opti.SheetName = "RCM Self Invoice Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "RCM Self Invoice Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("RCM Self Invoice Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub RCMSelfInvoiceGSTDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class