
Imports BL

Public Class JobOutTypeDetails

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub ChallanTypeDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt = True And e.KeyCode = Windows.Forms.Keys.E Then       'for Saving
            Call cmdedit_Click(sender, e)
        ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf (e.Control = True And e.KeyCode = Windows.Forms.Keys.N) Or (e.KeyCode = Windows.Forms.Keys.A) Then   'for Exit
            showform(False, "", 0)
        End If
    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.Click
        Try
            showform(True, GRIDTYPE.Item(0, GRIDTYPE.CurrentRow.Index).Value.ToString, GRIDTYPE.Item(1, GRIDTYPE.CurrentRow.Index).Value.ToString)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ChallanTypeDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillgrid()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Dim dttable As New DataTable
        Dim objClsCommon As New ClsCommonMaster

        dttable = objClsCommon.search(" JOTYPE_name, JOTYPE_id", "", "JOBOUTTYPEMASTER", " and JOTYPE_Yearid = " & YearId)

        GRIDTYPE.DataSource = dttable
        GRIDTYPE.Columns(0).HeaderText = "Name"

        GRIDTYPE.Columns(0).Width = 250
        GRIDTYPE.Columns(1).Visible = False
        GRIDTYPE.Columns(0).SortMode = Windows.Forms.DataGridViewColumnSortMode.Automatic
        If GRIDTYPE.RowCount > 0 Then GRIDTYPE.FirstDisplayedScrollingRowIndex = GRIDTYPE.RowCount - 1

    End Sub

    Private Sub gridgroup_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDTYPE.CellDoubleClick
        Try
            showform(True, GRIDTYPE.Item(0, GRIDTYPE.CurrentRow.Index).Value.ToString, GRIDTYPE.Item(1, GRIDTYPE.CurrentRow.Index).Value.ToString)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal name As String, ByVal id As Integer)
        Try
            Dim objCategorymaster As New JobOutTypeMaster
            objCategorymaster.EDIT = editval
            objCategorymaster.MdiParent = MDIMain
            objCategorymaster.TEMPJOBOUTTYPE = name
            objCategorymaster.TEMPTYPEID = id
            objCategorymaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        Try
            showform(False, "", 0)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtcmp_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcmp.Validated
        Dim rowno, b As Integer

        fillgrid()
        rowno = 0
        For b = 1 To GRIDTYPE.RowCount
            txttempname.Text = GRIDTYPE.Item(0, rowno).Value.ToString()
            txttempname.SelectionStart = 0
            txttempname.SelectionLength = txtcmp.TextLength
            If LCase(txtcmp.Text.Trim) <> LCase(txttempname.SelectedText.Trim) Then
                GRIDTYPE.Rows.RemoveAt(rowno)
            Else
                rowno = rowno + 1
            End If
        Next
    End Sub
End Class