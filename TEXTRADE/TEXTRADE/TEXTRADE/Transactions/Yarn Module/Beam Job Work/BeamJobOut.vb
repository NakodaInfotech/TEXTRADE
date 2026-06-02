
Imports System.ComponentModel
Imports BL
Imports DevExpress.Diagram.Core.Native


Public Class BeamJobOut

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPBEAMRECDNO, tempzalanirollno As Integer
    Dim TEMPMSG As Integer
    Dim NextBeamNo As Integer
    Dim MAXNO As Integer = 0

    Dim TEMPBEAMNO As Integer

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDSAVE_Click(sender As Object, e As EventArgs) Handles CMDSAVE.Click

    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click

    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click

    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Sub CLEAR()

        CMDSELECTYARNISSUE.Enabled = True
        TXTBEAMJONO.Clear()
        DTBEAMJODATE.Text = Mydate
        CMBGODOWN.Text = USERGODOWN
        CMBNAME.Text = ""

        LBLTAPLINE.Text = 0.0
        TXTREMARKS.Clear()
        'TXTBEAMNO.Clear()

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        TXTREMARKS.Clear()


        GRIDBEAM.RowCount = 0

        GETMAX_BEAMJO_NO()

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        TabControl1.SelectedIndex = 0

        'PBSOFTCOPY.Image = Nothing
        'TXTUPLOADSRNO.Clear()
        'txtuploadname.Clear()
        'txtuploadremarks.Clear()
        'TXTIMGPATH.Clear()
        'gridupload.RowCount = 0

        'TXTUPLOADSRNO.Text = 1

        'GetLastBeamNo()
        'TXTBEAMNO.Text = NextBeamNo

        'FILLROLLITEM(CMBROLLNO, EDIT, "AND ROLLITEM = 1 ", "HAVING SUM(QTY - ISSQTY) >0")
        'CMBROLLNO.Enabled = True

        LBLTOTALJOBMTRS.Text = 0.0
        LBLTAPLINE.Text = 0.0
        LBLTOTALBEAMMTRS.Text = 0.0

    End Sub

    Sub GETMAX_BEAMJO_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(BEAMREC_NO),0)+1", "BEAMRECEIVEDWARPER", "AND BEAMREC_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTBEAMJONO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub BeamJobOut_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDSAVE_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1) Then       'for scheduling
                TabControl1.SelectedIndex = (0)
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2) Then       'for ITEM DETAILS
                TabControl1.SelectedIndex = (1)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call toolprevious_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call toolnext_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Sub FILLCMB()
        If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If DTBEAMJODATE.Text = "__/__/____" Then
            EP.SetError(DTBEAMJODATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTBEAMJODATE.Text) Then
                EP.SetError(DTBEAMJODATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Jobber Name")
            bln = False
        End If

        If CMBGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBGODOWN, " Please Fill Godown ")
            bln = False
        End If

        For Each row As DataGridViewRow In GRIDBEAM.Rows
            If Val(row.Cells(GTOTALMTRS.Index).Value) = 0 Then
                EP.SetError(CMBGODOWN, "Beam Mtrs Cannot be 0 or Less")
                bln = False
            End If
        Next

        'For Each row As DataGridViewRow In GRIDBEAM.Rows
        '    If Val(row.Cells(GBEAMMTRS.Index).Value) > Val(row.Cells(GJOBMTRS.Index).Value) Then
        '        EP.SetError(CMBOURGODOWN, "Beam Mtrs Cannot be Greater Than Job Mtrs")
        '        row.DefaultCellStyle.BackColor = Color.LightGreen
        '        bln = False
        '    End If
        'Next

        Return bln
    End Function
End Class