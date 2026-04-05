
Imports BL

Public Class SelectStockGDNGrid

    Public FRMSTRING As String = ""
    Public WHERECLAUSE As String = ""
    Public DTBARCODE As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectStockGDNGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then
                gridbill.Focus()
                gridbill.FocusedColumn = gridbill.Columns("BALENO")
                gridbill.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
                gridbill.ShowEditor()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectStockGDNGrid_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try

            WHERECLAUSE = WHERECLAUSE & " AND ROUND(MTRS,2) > 0 "

            Dim OBJCMN As New ClsCommon
            Dim DTUNIT As DataTable = OBJCMN.SEARCH("UNIT_ABBR", "", "DEFAULTSTOCKUNIT", "")
            If DTUNIT.Rows.Count > 0 Then WHERECLAUSE = WHERECLAUSE & " AND UNIT IN (SELECT UNIT_ABBR FROM DEFAULTSTOCKUNIT)"

            Dim DT As New DataTable
            If FRMSTRING = "GREY" Then
                DT = OBJCMN.Execute_Any_String(" SELECT CAST(0 AS BIT) AS CHK, SUM(PCS) AS TOTALPCS, SUM(MTRS) AS TOTALMTRS, DESIGNNO, ITEMNAME, QUALITY, COLOR ,GODOWN, LOTNO, BALENO, CHALLANNO, PIECETYPE, BARCODE, UNIT, ITEMCODE, CATEGORY,PURRATE, SALERATE, DESIGNRATE,RACK,SHELF, MILLNAME, DATE, JOBBERNAME, TYPE, GRIDREMARKS, PURNAME, ROUND(PURRATE*SUM(MTRS),2) AS AMOUNT, DISPLAYNAME, SUM(WT) AS WT FROM  GREYBARCODESTOCK WHERE YEARID = " & YearId & WHERECLAUSE & " GROUP BY DESIGNNO, ITEMNAME, QUALITY, LOTNO, BALENO, CHALLANNO, COLOR ,GODOWN, PIECETYPE, BARCODE, UNIT, ITEMCODE, CATEGORY, PURRATE,RACK,SHELF, SALERATE, DESIGNRATE, MILLNAME, DATE, JOBBERNAME, TYPE, GRIDREMARKS, PURNAME, DISPLAYNAME ORDER BY DESIGNNO, QUALITY, COLOR", "", "")
            Else
                DT = OBJCMN.Execute_Any_String(" SELECT CAST(0 AS BIT) AS CHK, PCS AS TOTALPCS, MTRS AS TOTALMTRS, DESIGNNO, ITEMNAME, QUALITY, COLOR ,GODOWN, LOTNO, BALENO, CHALLANNO, PIECETYPE, BARCODE, UNIT, ITEMCODE, CATEGORY,PURRATE, SALERATE, DESIGNRATE,RACK,SHELF, MILLNAME, DATE, JOBBERNAME, TYPE, GRIDREMARKS, PURNAME, ROUND(PURRATE*MTRS,2) AS AMOUNT, DISPLAYNAME, WT AS WT FROM  BARCODESTOCK WHERE YEARID = " & YearId & WHERECLAUSE & " ORDER BY ITEMNAME, DESIGNNO, COLOR", "", "")
            End If
            gridbilldetails.DataSource = DT
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try
            DTBARCODE.Columns.Add("BARCODE")

            gridbill.ClearColumnsFilter()

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DTBARCODE.Rows.Add(dtrow("BARCODE"))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectStockGDNGrid_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            gridbill.Focus()
            gridbill.FocusedColumn = gridbill.Columns("BALENO")
            gridbill.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
            gridbill.ShowEditor()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbill.KeyDown
        Try
            If gridbill.FocusedRowHandle >= 0 AndAlso (e.KeyCode = Keys.Space Or e.KeyCode = Keys.Enter) Then
                Dim DTROW As DataRow = gridbill.GetFocusedDataRow
                DTROW("CHK") = 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class