Imports BL

Public Class SelectYarnGodownTransfer
    Public DT As New DataTable
    Public TEMPGODOWNNAME As String
    Public GODOWN As String

    Sub fillgrid(ByVal WHERE As String)
        'Try
        '    Cursor.Current = Cursors.WaitCursor
        '    Dim OBJCMN As New ClsCommon()
        '    'Dim DT As DataTable = OBJCMN.SEARCH(" CAST (0 AS BIT) AS CHK , NO, SRNO, TYPE, MILLNAME,YARNQUALITY, ISNULL(ENDS, 0) AS ENDS, ROLLS, WT, PROGRAMNO,PROGRAMSRNO, TOTALENDS, LENGTH", "", "ROLLSTOCK", " AND ROLLSTOCK.GODOWN='" & TEMPGODOWNNAME & "' AND ROLLSTOCK.YEARID = " & YearId)
        '    Dim DT As DataTable = OBJCMN.SEARCH("CAST (0 AS BIT) AS CHK,YARNQUALITY, MILLNAME, DESIGNNO, COLOR, LOTNO, SUM(BAGS) AS BAGS, SUM (WT) AS WT,SUM(CONES) AS CONES,    CAST(GETDATE() AS DATE) AS LIFTINGDATE,GODOWN  ", "", " YARNSTOCKVIEW ", " AND YEARID = " & YearId & WHERE & "   GROUP BY YARNQUALITY, MILLNAME, DESIGNNO, COLOR, GODOWN, LOTNO,CATEGORY HAVING SUM(WT) > 0")
        '    gridbilldetails.DataSource = DT
        '    If DT.Rows.Count > 0 Then
        '        gridbill.FocusedRowHandle = gridbill.RowCount - 1
        '        gridbill.TopRowIndex = gridbill.RowCount - 15
        '    End If

        'Catch ex As Exception
        '    If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        'Finally
        '    Cursor.Current = Cursors.Default
        'End Try
        Try
            Dim WHERECLAUSE As String = "  AND YEARID=" & YearId
            If GODOWN <> "" Then WHERECLAUSE = WHERECLAUSE & " AND GODOWN ='" & GODOWN & "'"
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If ALLOWYARNBARCODEPRINT = True Then
                DT = OBJCMN.SEARCH(" CAST(0 AS BIT) AS CHK, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO, LRNO, CONES, BAGS, WT, BARCODE, FROMNO, FROMSRNO, FROMTYPE,CAST(GETDATE() AS DATE) AS LIFTINGDATE ", "", "  YARNBARCODESTOCK ", WHERECLAUSE)
            Else
                DT = OBJCMN.SEARCH(" CAST(0 AS BIT) AS CHK, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO, LRNO, SUM(ISNULL(CONES,0)) AS CONES, SUM(BAGS) AS BAGS, SUM(WT) AS WT, '' AS BARCODE, 0 AS FROMNO, 0 AS FROMSRNO, '' AS FROMTYPE, CAST(GETDATE() AS DATE) AS LIFTINGDATE ", "", "  YARNSTOCKVIEW ", WHERECLAUSE & " GROUP BY GODOWN, YARNQUALITY, CATEGORY, MILLNAME, DESIGNNO, COLOR, LOTNO, LRNO HAVING SUM(WT) > 0 ")
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
    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Dim COUNT As Integer = 0
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    COUNT = COUNT + 1
                End If
            Next
            'If COUNT > 1 Then
            '    MsgBox("You Can Select Only One Entry")
            '    Exit Sub
            'End If


            DT.Columns.Add("YARNQUALITY")
            DT.Columns.Add("MILLNAME")
            DT.Columns.Add("DESIGNNO")
            DT.Columns.Add("COLOR")
            DT.Columns.Add("LOTNO")
            DT.Columns.Add("BAGS")
            DT.Columns.Add("WT")
            DT.Columns.Add("CONES")
            DT.Columns.Add("BARCODE")
            'DT.Columns.Add("LRNO")
            DT.Columns.Add("LIFTINGDATE")
            DT.Columns.Add("GODOWN")
            'DT.Columns.Add("BARCODE")

            'DT.Columns.Add("TOTALENDS")
            'DT.Columns.Add("LENGTH")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("YARNQUALITY"), dtrow("MILLNAME"), dtrow("DESIGNNO"), dtrow("COLOR"), dtrow("LOTNO"), dtrow("BAGS"), Val(dtrow("WT")), Val(dtrow("CONES")), dtrow("BARCODE"), dtrow("LIFTINGDATE"), dtrow("BARCODE"))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectRolls_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectRolls_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid("")
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
End Class