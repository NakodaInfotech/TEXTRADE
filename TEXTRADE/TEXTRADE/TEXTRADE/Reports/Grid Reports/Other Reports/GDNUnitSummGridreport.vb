
Imports BL

Public Class GDNUnitSummGridreport

    Public WHERECLAUSE As String = ""

    Private Sub GDNUnitSummGridreport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable = OBJCMN.Execute_Any_String(" SELECT ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT, ROUND(SUM(GDN_DESC.GDN_PCS ),0) AS TOTALPCS, ROUND(SUM(GDN_DESC.GDN_MTRS),2) AS TOTALMTRS FROM GDN_DESC INNER JOIN GDN ON GDN_DESC.GDN_NO = GDN.GDN_NO AND GDN_DESC.GDN_YEARID = GDN.GDN_YEARID INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.ACC_ID LEFT OUTER JOIN UNITMASTER ON GDN_UNITID = UNIT_ID WHERE GDN.GDN_YEARID = " & YearId & WHERECLAUSE & " GROUP BY ISNULL(UNITMASTER.UNIT_ABBR,'') ", "", "")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub TOOLEXCEL_Click(sender As Object, e As EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Missing Invoice No.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Missing Invoice No"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Missing Invoice No", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Missing Invoice No Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub GDNUnitSummGridreport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class