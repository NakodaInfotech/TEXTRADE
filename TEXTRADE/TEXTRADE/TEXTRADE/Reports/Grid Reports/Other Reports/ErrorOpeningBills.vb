
Imports BL

Public Class ErrorOpeningBills

    Private Sub ErrorOpeningBills_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ErrorOpeningBills_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" LEDGERS.ACC_CMPNAME AS NAME, OPENINGBILL.BILL_GRIDSRNO AS GRIDSRNO, OPENINGBILL.BILL_TYPE AS [TYPE], OPENINGBILL.BILL_NO AS BILLNO, OPENINGBILL.BILL_YEAR AS YEAR, OPENINGBILL.BILL_DATE AS BILLDATE, OPENINGBILL.BILL_CRDAYS AS CRDAYS, OPENINGBILL.BILL_DUEDATE AS DUEDATE, ISNULL(AGENTLEDGERS.ACC_CMPNAME, '') AS AGENTNAME, OPENINGBILL.BILL_NARRATION AS REMARKS, OPENINGBILL.BILL_DISPUTE AS DISPUTE, OPENINGBILL.BILL_AMT AS AMT, OPENINGBILL.BILL_AMTPAIDREC AS AMTPAIDREC, OPENINGBILL.BILL_EXTRAAMT AS EXTRAAMT, OPENINGBILL.BILL_RETURN AS [RETURN], OPENINGBILL.BILL_BALANCE AS BALANCE, ISNULL(REGISTER_NAME,'') AS REGNAME, ISNULL(BILL_PRINTINITIALS,'') AS PRINTINITIALS, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '') AS DELIVERYAT, ISNULL(OPENINGBILL.BILL_PCS, 0) AS PCS, ISNULL(OPENINGBILL.BILL_MTRS, 0) AS MTRS, ISNULL(OPENINGBILL.BILL_TOTALAMT, 0) AS TOTALAMT, ISNULL(OPENINGBILL.BILL_CHARGES, 0) AS CHARGES, ISNULL(OPENINGBILL.BILL_TAXABLEAMT, 0) AS TAXABLEAMT, ISNULL(OPENINGBILL.BILL_CGSTPER, 0) AS CGSTPER, ISNULL(OPENINGBILL.BILL_CGSTAMT, 0) AS CGSTAMT, ISNULL(OPENINGBILL.BILL_SGSTPER, 0) AS SGSTPER, ISNULL(OPENINGBILL.BILL_SGSTAMT, 0) AS SGSTAMT, ISNULL(OPENINGBILL.BILL_IGSTPER, 0) AS IGSTPER, ISNULL(OPENINGBILL.BILL_IGSTAMT, 0) AS IGSTAMT, ISNULL(OPENINGBILL.BILL_GRANDTOTAL, 0) AS GRANDTOTAL, ISNULL(OPENINGBILL.BILL_INITIALS, '') AS BILLINITIALS ", "", " OPENINGBILL INNER JOIN LEDGERS ON OPENINGBILL.BILL_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON OPENINGBILL.BILL_AGENTID = AGENTLEDGERS.Acc_id INNER JOIN REGISTERMASTER ON REGISTER_ID = BILL_REGISTERID LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON OPENINGBILL.BILL_DELIVERYATID = DELIVERYLEDGERS.Acc_id", " AND BILL_YEARID = " & YearId & " AND ((BILL_BALANCE < 0) OR ROUND((BILL_AMT - (BILL_AMTPAIDREC + BILL_EXTRAAMT + BILL_RETURN)),2) <> ROUND(BILL_BALANCE,2)) ORDER BY OPENINGBILL.BILL_GRIDSRNO ")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
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

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXPORT_Click(sender As Object, e As EventArgs) Handles CMDEXPORT.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Eway Entry Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Eway Entry Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Eway Entry Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Eway Entry Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

End Class