
Imports BL

Public Class SelectLotNo

    Public JOBBERNAME As String = ""
    Public WCLAUSE As String = ""
    Public DT As New DataTable

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectHSN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim WHERECLAUSE As String = " AND JOBBERNAME = '" & JOBBERNAME & "'"
            Dim OBJCMN As New ClsCommon
            Dim DTTABLE As New DataTable
            If LOTSTATUSONMTRS = False Then
                DTTABLE = OBJCMN.SEARCH("*, (CASE WHEN ACCEPTEDMTRS >0 THEN (BALMTRS/ACCEPTEDMTRS) * 100 ELSE (BALMTRS/TOTALMTRS) * 100 END) AS SHRINKAGEPER", "", " LOT_VIEW ", " AND LOT_VIEW.YEARID=" & YearId & WHERECLAUSE & WCLAUSE & " AND LOT_VIEW.BALPCS > 0 AND LOT_VIEW.LOTCOMPLETED = 'FALSE' ORDER BY GRNNO")
            Else
                DTTABLE = OBJCMN.SEARCH("*, (CASE WHEN ACCEPTEDMTRS >0 THEN (BALMTRS/ACCEPTEDMTRS) * 100 ELSE (BALMTRS/TOTALMTRS) * 100 END) AS SHRINKAGEPER", "", " LOT_VIEW ", " AND LOT_VIEW.YEARID=" & YearId & WHERECLAUSE & WCLAUSE & " AND (LOT_VIEW.LOTCOMPLETED = 'FALSE') ORDER BY GRNNO")
            End If
            gridbilldetails.DataSource = DTTABLE
            If DTTABLE.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon

            DT.Columns.Add("LOTNO")
            DT.Columns.Add("DATE")
            DT.Columns.Add("ITEMNAME")
            DT.Columns.Add("ACCEPTEDPCS")
            DT.Columns.Add("ACCEPTEDMTRS")
            DT.Columns.Add("RECDPCS")
            DT.Columns.Add("RECDMTRS")
            DT.Columns.Add("BALPCS")
            DT.Columns.Add("BALMTRS")
            DT.Columns.Add("SHRINKAGE")
            DT.Columns.Add("GRNNO")
            DT.Columns.Add("GRNTYPE")
            DT.Columns.Add("CHALLANNO")
            DT.Columns.Add("DYEINGJOB")

            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                DT.Rows.Add(DTROW("LOTNO"), DTROW("DATE"), DTROW("ITEMNAME"), Val(DTROW("ACCEPTEDPCS")), Val(DTROW("ACCEPTEDMTRS")), Val(DTROW("RECDPCS")), Val(DTROW("RECDMTRS")), Val(DTROW("BALPCS")), Val(DTROW("BALMTRS")), Val(DTROW("SHRINKAGE")), Val(DTROW("GRNNO")), DTROW("GRNTYPE"), DTROW("JONO").ToString, DTROW("DYEINGJOB"))
            Next

            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Private Sub SelectHSN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectLotNo_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If LOTSTATUSONMTRS = True And ClientName <> "KRISHNA" Then
                GPCS.Visible = False
                GRECDPCS.Visible = False
                GBALPCS.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXCEL_Click(sender As Object, e As EventArgs) Handles CMDEXCEL.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Select Lot Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Select Lot Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Select Lot Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Select Lot Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub
End Class