
Imports BL
Imports System.Windows.Forms

Public Class SelectSampleStock

    Public DT As New DataTable
    Public GODOWN As String = ""
    Public EDIT As Boolean          'used for editing
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectSampleStock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectSampleStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FILLGRID("")
    End Sub

    Sub FILLGRID(ByVal WHERE As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" CAST(0 AS BIT) AS CHK, BOOKLETSTOCK.SAMPLETYPE, BOOKLETSTOCK.ITEMNAME, BOOKLETSTOCK.DESIGNNO, ISNULL(BOOKLETSTOCK.SHADE, '') AS COLOR, ISNULL(BOOKLETSTOCK.NOOFBOOKLETS, 0) AS NOOFBOOKLET, SB_BARCODE AS BARCODE ", "", " SAMPLEBARCODE INNER JOIN ITEMMASTER ON ITEM_ID = SB_ITEMID LEFT OUTER JOIN DESIGNMASTER ON SB_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON SB_COLORID = COLOR_ID INNER JOIN BOOKLETSTOCK ON ITEMMASTER.ITEM_NAME = BOOKLETSTOCK.ITEMNAME AND ISNULL(DESIGN_NO,'') = BOOKLETSTOCK.DESIGNNO AND ISNULL(COLOR_NAME,'') = BOOKLETSTOCK.SHADE AND BOOKLETSTOCK.YEARID = " & YearId, " AND GODOWN = '" & GODOWN & "' AND NOOFBOOKLETS <> 0 ORDER BY ITEMNAME, DESIGNNO, SHADE, GODOWN")
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
            DT.Columns.Add("SAMPLETYPE")
            DT.Columns.Add("ITEMNAME")
            DT.Columns.Add("DESIGNNO")
            DT.Columns.Add("COLOR")
            DT.Columns.Add("NOOFBOOKLET")
            DT.Columns.Add("BARCODE")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("SAMPLETYPE"), dtrow("ITEMNAME"), dtrow("DESIGNNO"), dtrow("COLOR"), dtrow("NOOFBOOKLET"), dtrow("BARCODE"))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

End Class