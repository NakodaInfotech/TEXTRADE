
Imports BL

Public Class SelectYarnStock

    Public DT As New DataTable
    Public GODOWN As String = ""


    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectYarnStock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Opening_Stock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim WHERECLAUSE As String = "  AND YEARID=" & YearId
            If GODOWN <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GODOWN ='" & GODOWN & "'"
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If ALLOWYARNBARCODEPRINT = True Then
                'DT = OBJCMN.SEARCH(" CAST(0 AS BIT) AS CHK, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO, LRNO, CONES, BAGS, WT, BARCODE, FROMNO, FROMSRNO, FROMTYPE , RACK ", "", "  YARNBARCODESTOCK ", WHERECLAUSE & " GROUP BY GODOWN, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO, LRNO, BARCODE, ISNULL(RACK, '') HAVING SUM(WT) > 0 ")
                DT = OBJCMN.SEARCH(" CAST(0 AS BIT) AS CHK, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO, LRNO, SUM(ISNULL(CONES, 0)) AS CONES, SUM(BAGS) AS BAGS, SUM(WT) AS WT,  BARCODE,  FROMNO,  FROMSRNO,  FROMTYPE, ISNULL(RACK, '') AS RACK ", "", "  YARNBARCODESTOCK ", WHERECLAUSE & " GROUP BY GODOWN, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO, LRNO, BARCODE,FROMNO,  FROMSRNO, FROMTYPE, ISNULL(RACK, '') HAVING SUM(WT) > 0 ")
            Else
                DT = OBJCMN.SEARCH(" CAST(0 AS BIT) AS CHK, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO, LRNO, SUM(ISNULL(CONES, 0)) AS CONES, SUM(BAGS) AS BAGS, SUM(WT) AS WT, '' AS BARCODE, 0 AS FROMNO, 0 AS FROMSRNO, '' AS FROMTYPE, ISNULL(RACK, '') AS RACK ", "", "  YARNSTOCKVIEW ", WHERECLAUSE & " GROUP BY GODOWN, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO, LRNO, ISNULL(RACK, '') HAVING SUM(WT) > 0 ")
            End If
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            gridbill.ClearColumnsFilter()


            DT.Columns.Add("YARNQUALITY")
            DT.Columns.Add("MILLNAME")
            DT.Columns.Add("DESIGNNO")
            DT.Columns.Add("COLOR")
            DT.Columns.Add("LOTNO")
            DT.Columns.Add("LRNO")
            DT.Columns.Add("BAGS")
            DT.Columns.Add("WT")
            DT.Columns.Add("CONES")
            DT.Columns.Add("BARCODE")
            DT.Columns.Add("FROMNO")
            DT.Columns.Add("FROMSRNO")
            DT.Columns.Add("FROMTYPE")
            DT.Columns.Add("RACK")


            For I As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(I)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("YARNQUALITY"), dtrow("MILLNAME"), dtrow("DESIGNNO"), dtrow("COLOR"), dtrow("LOTNO"), dtrow("LRNO"), Val(dtrow("BAGS")), Val(dtrow("WT")), Val(dtrow("CONES")), dtrow("BARCODE"), Val(dtrow("FROMNO")), Val(dtrow("FROMSRNO")), dtrow("FROMTYPE"), dtrow("RACK"))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkall_CheckedChanged(sender As Object, e As EventArgs) Handles chkall.CheckedChanged
        Try
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                dtrow("CHK") = chkall.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class