
Imports BL

Public Class SelectItemSampleOrder

    Public DT As New DataTable
    Dim CLEAR As Boolean = False
    Public EDIT As Boolean
    Public tempMsg As Integer
    Public ITEMNAME As String
    Public ITEMWIDTH As String

    Private Sub SelectItemSampleOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJCMN As New ClsCommon
            Dim GROUPBY As String = " GROUP BY DESIGN_NO, DESIGN_ID, DESIGN_yearid ORDER BY DESIGN_NO"
            Dim WHERECLAUSE As String = " AND ITEMMASTER.ITEM_NAME = '" & ITEMNAME & "' AND DESIGN_YEARID = " & YearId
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_NO,'') AS DESIGNNO,  SUM(ISNULL(B.NOOFBOOKLETS, 0)) AS TOTALBOOKLET, 0 AS ORDERBOOKLET, ISNULL((SELECT SUM(SO_NOOFBOOKLET-SO_OUTQTY) FROM SAMPLEORDER_DESC WHERE SO_CLOSED = 'FALSE' AND SO_NOOFBOOKLET-SO_OUTQTY > 0 AND SO_YEARID = DESIGN_YEARID AND SO_DESIGNID = DESIGN_ID),0) AS PENDINGBOOKLET ", "", " ITEMMASTER INNER JOIN  DESIGNMASTER ON ITEMMASTER.ITEM_ID = DESIGN_ITEMID LEFT OUTER JOIN BOOKLETSTOCK AS B ON DESIGN_NO = B.DESIGNNO AND DESIGN_YEARID = B.YEARID ", WHERECLAUSE & GROUPBY)
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = 0
                gridbill.FocusedColumn = GORDERBOOKLET
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try

            DT.Columns.Add("DESIGNNO")
            DT.Columns.Add("ORDERBOOKLET")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Val(dtrow("ORDERBOOKLET")) > 0 Then
                    DT.Rows.Add(dtrow("DESIGNNO"), Val(dtrow("ORDERBOOKLET")))
                End If
            Next

            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridbilldetails.KeyDown
        Try
            If e.KeyCode = Keys.Space And gridbill.FocusedRowHandle > 0 Then
                Dim DTROW As DataRow = gridbill.GetFocusedDataRow()
                Dim DTROW1 As DataRow
                For I As Integer = gridbill.FocusedRowHandle - 1 To 0 Step -1
                    DTROW1 = gridbill.GetDataRow(I)
                    If Val(DTROW1("ORDERBOOKLET")) > 0 Then Exit For
                Next
                DTROW("ORDERBOOKLET") = Val(DTROW1("ORDERBOOKLET"))
            End If

            If e.KeyCode = Keys.Delete And gridbill.FocusedRowHandle >= 0 Then
                Dim DTROW As DataRow = gridbill.GetFocusedDataRow()
                DTROW("ORDERBOOKLET") = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSHOWSTOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSHOWSTOCK.Click
        Try
            'Dim OBJSTOCK As New SampleBookletStockGridReport
            'OBJSTOCK.TEMPITEMNAME = ITEMNAME
            'OBJSTOCK.TEMPDESIGNNO = gridbill.GetFocusedRowCellDisplayText("DESIGNNO")
            'OBJSTOCK.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class