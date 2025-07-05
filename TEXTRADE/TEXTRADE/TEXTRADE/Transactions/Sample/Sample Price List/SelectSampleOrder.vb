
Imports BL

Public Class SelectSampleOrder

    Public PARTYNAME As String = ""
    Public SONO As Integer = 0
    Public FRMSTRING As String = ""
    Public WHERECLAUSE As String = ""
    Public DT As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectSampleOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectSampleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid()
    End Sub

    Sub fillgrid()
        Try
            Cursor.Current = Cursors.WaitCursor

            Dim where As String = ""
            If SONO <> 0 Then where = where & " AND SRNO = " & SONO
            If PARTYNAME <> "" Then where = where & " AND NAME = '" & PARTYNAME & "'"
            If WHERECLAUSE <> "" Then where = where & WHERECLAUSE

            Dim OBJCMN As New ClsCommon()
            Dim dt As DataTable = OBJCMN.SEARCH("*", "", " (SELECT SAMPLEORDER.SO_NO AS SRNO, SAMPLEORDER.SO_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(SAMPLEORDER.SO_REMARKS, '') AS REMARKS, SAMPLEORDER_DESC.SO_GRIDSRNO AS GRIDSRNO, ISNULL(ITEMMASTER.ITEM_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR, (SAMPLEORDER_DESC.SO_NOOFBOOKLET - SAMPLEORDER_DESC.SO_OUTQTY) AS QTY, 'SAMPLEORDER' AS TYPE, ISNULL(SO_SELF,0) AS SELFORDER FROM  SAMPLEORDER INNER JOIN SAMPLEORDER_DESC ON SAMPLEORDER.SO_NO = SAMPLEORDER_DESC.SO_NO AND SAMPLEORDER.SO_YEARID = SAMPLEORDER_DESC.SO_YEARID INNER JOIN ITEMMASTER ON SAMPLEORDER_DESC.SO_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON SAMPLEORDER.SO_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON SAMPLEORDER_DESC.SO_DESIGNID = DESIGNMASTER.DESIGN_id  LEFT OUTER JOIN COLORMASTER ON SAMPLEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_ID WHERE SAMPLEORDER_DESC.SO_CLOSED = 'FALSE' and (SAMPLEORDER_DESC.SO_NOOFBOOKLET-SAMPLEORDER_DESC.SO_OUTQTY)>0 AND dbo.SAMPLEORDER.SO_yearid=" & YearId & " UNION ALL SELECT  OPENINGSAMPLEORDER.OSO_NO AS SRNO, OPENINGSAMPLEORDER.OSO_DATE AS PODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(OPENINGSAMPLEORDER.OSO_REMARKS, '') AS REMARKS, OPENINGSAMPLEORDER_DESC.OSO_GRIDSRNO AS POGRIDSRNO, ISNULL(ITEMMASTER.ITEM_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR, (OPENINGSAMPLEORDER_DESC.OSO_NOOFBOOKLET - OPENINGSAMPLEORDER_DESC.OSO_OUTQTY) AS QTY, 'OPENING' AS TYPE, ISNULL(OSO_SELF,0) AS SELFORDER FROM OPENINGSAMPLEORDER INNER JOIN OPENINGSAMPLEORDER_DESC ON OPENINGSAMPLEORDER.OSO_NO = OPENINGSAMPLEORDER_DESC.OSO_NO AND OPENINGSAMPLEORDER.OSO_YEARID = OPENINGSAMPLEORDER_DESC.OSO_YEARID INNER JOIN ITEMMASTER ON OPENINGSAMPLEORDER_DESC.OSO_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON OPENINGSAMPLEORDER.OSO_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGSAMPLEORDER_DESC.OSO_DESIGNID = DESIGNMASTER.DESIGN_id  LEFT OUTER JOIN COLORMASTER ON OPENINGSAMPLEORDER_DESC.OSO_COLORID = COLORMASTER.COLOR_ID WHERE OPENINGSAMPLEORDER_DESC.OSO_CLOSED = 'FALSE' and (OPENINGSAMPLEORDER_DESC.OSO_NOOFBOOKLET-OPENINGSAMPLEORDER_DESC.OSO_OUTQTY)>0 AND dbo.OPENINGSAMPLEORDER.OSO_yearid=" & YearId & ") AS T", where & " ORDER BY SRNO, GRIDSRNO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default

        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            DT.Columns.Add("SRNO")
            DT.Columns.Add("NAME")
            DT.Columns.Add("DATE")
            DT.Columns.Add("GRIDSRNO")
            DT.Columns.Add("ITEMNAME")
            DT.Columns.Add("DESIGNNO")
            DT.Columns.Add("COLOR")
            DT.Columns.Add("QTY")
            DT.Columns.Add("TYPE")
            DT.Columns.Add("REMARKS")


            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                Dim dtrow As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                DT.Rows.Add(dtrow("SRNO"), dtrow("NAME"), dtrow("DATE"), Val(dtrow("GRIDSRNO")), dtrow("ITEMNAME"), dtrow("DESIGNNO"), dtrow("COLOR"), Val(dtrow("QTY")), dtrow("TYPE"), dtrow("REMARKS"))
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


End Class