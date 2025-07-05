Imports BL

Public Class SONoWiseReport

    Private Sub cmdcancel_Click(sender As Object, e As EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDEXPORT_Click(sender As Object, e As EventArgs) Handles CMDEXPORT.Click
        Try
            Dim PATH As String = Application.StartupPath & "\So Report.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "So Report"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "So Report", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("So Report Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" DISTINCT SALEORDER.SO_REFNO AS SONO, SALEORDER.so_date AS SODATE, SALEORDER.SO_DUEDATE AS DUEDATE, LEDGERS.Acc_cmpname AS NAME, SALEORDER.SO_REMARKS AS SOREMARKS, JOBOUT.JO_date AS JOBOUTDATE, JOBOUT.JO_NO AS JONO, JOBBERLEDGERS.Acc_cmpname AS KARIGARNAME, CAST(JOBOUT.JO_REMARKS AS VARCHAR(2000)) AS JOREMARKS, ISNULL(ITEM_NAME,'') AS ITEMNAME, ISNULL(QUALITY_name,'') AS QUALITY, ISNULL(DESIGN_NO,'') AS DESIGNNO, ISNULL(COLOR_NAME,'') AS SHADE, JO_PCS AS PCS, ISNULL(unit_abbr,'Pcs') AS UNIT, JOBIN.JI_DATE AS JIDATE, JOBIN.JI_NO AS JINO, CAST(JOBIN.JI_REMARKS AS VARCHAR(2000)) AS JIREMARKS, GDN_BALENO AS CHALLANNO, GDN_NO AS GDNNO ", "", " SALEORDER INNER JOIN LEDGERS ON SALEORDER.so_ledgerid = LEDGERS.Acc_id  LEFT OUTER JOIN JOBOUT_DESC ON JOBOUT_DESC.JO_BALENO = CAST(SALEORDER.SO_REFNO  AS VARCHAR(50)) AND JOBOUT_DESC.JO_YEARID = SALEORDER.SO_YEARID LEFT OUTER JOIN JOBOUT ON JOBOUT.JO_no = JOBOUT_DESC.JO_NO AND JOBOUT.JO_yearid = JOBOUT_DESC.JO_YEARID LEFT OUTER JOIN LEDGERS AS JOBBERLEDGERS ON JOBOUT.JO_ledgerid = JOBBERLEDGERS.Acc_id LEFT OUTER JOIN ITEMMASTER ON JO_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN QUALITYMASTER ON JO_QUALITYID = QUALITY_id LEFT OUTER JOIN DESIGNMASTER ON JO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON JO_COLORID = COLOR_ID LEFT OUTER JOIN GODOWNMASTER ON JO_GODOWNID = GODOWN_ID LEFT OUTER JOIN UNITMASTER ON JO_UNITID = UNIT_ID  LEFT OUTER JOIN JOBIN_DESC ON JOBIN_DESC.JI_BALENO = CAST(SALEORDER.SO_REFNO  AS VARCHAR(50)) AND JOBIN_DESC.JI_YEARID = SALEORDER.SO_YEARID LEFT OUTER JOIN JOBIN ON JOBIN.JI_no = JOBIN_DESC.JI_NO AND JOBIN.JI_yearid = JOBIN_DESC.JI_YEARID LEFT OUTER JOIN GDN_DESC ON SO_REFNO = GDN_DESC.GDN_BALENO AND SALEORDER.SO_YEARID = GDN_DESC.GDN_YEARID ", " AND SALEORDER.SO_YEARID = " & YearId & " ORDER BY SO_DATE, SALEORDER.SO_REFNO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SONoWiseReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SONoWiseReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class