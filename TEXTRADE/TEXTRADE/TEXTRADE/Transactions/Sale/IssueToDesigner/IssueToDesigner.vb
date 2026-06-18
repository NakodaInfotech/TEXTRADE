
Imports System.ComponentModel
Imports BL
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid


Public Class IssueToDesigner

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE, GRIDDOUBLECLICK As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean          'used for editing
    Public TEMPISSNO As String
    Dim TEMPROW, TEMPCHGSROW As Integer

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
    End Sub

    Sub CLEAR()

        TXTNO.Clear()
        DTDATE.Text = Now.Date
        GRIDISSUE.RowCount = 0
        CMBDESIGNERNAME.Text = ""
        TXTREMARKS.Clear()
        LBLTOTALMTRS.Text = 0.00
        GETMAX_ISSENTRY()
        tstxtbillno.Clear()
        CMBDESIGNERNAME.Enabled = True

    End Sub

    Sub GETMAX_ISSENTRY()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(ISS_NO),0) + 1 ", "ISSUETODESIGNER", "  AND ISS_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub


    Private Sub IssueToDesigner_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDOK_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolprevious_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.F5 Then
                GRIDISSUE.Focus()
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBDESIGNERNAME.Text.Trim = "" Then fillDESIGNER(CMBDESIGNERNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub IssueToDesigner_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Dim DTROW() As DataRow

            DTROW = USERRIGHTS.Select("FormName = 'SALE ORDER'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            CLEAR()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim OBJCMN As New ClsCommon
                Dim OBJCLSPROFORMA As New ClsIssueToDesigner
                OBJCLSPROFORMA.alParaval.Add(TEMPISSNO)
                OBJCLSPROFORMA.alParaval.Add(CmpId)
                OBJCLSPROFORMA.alParaval.Add(YearId)
                Dim dttable As DataTable = OBJCLSPROFORMA.SELECTISSNO()
                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTNO.Text = TEMPISSNO
                        'TXTPRONO.ReadOnly = True
                        DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")
                        CMBDESIGNERNAME.Text = dr("DESIGNERNAME")
                        TXTREMARKS.Text = dr("REMARKS")
                        LBLTOTALMTRS.Text = dr("TOTALMTRS")

                        GRIDISSUE.Rows.Add(Val(dr("GRIDSRNO")), dr("ORDERNO"), dr("NAME"), dr("ITEMNAME"), dr("DESIGN"), dr("COLOR"), Format(Val(dr("MTRS")), "0.00"), dr("ORDERSRNO"), dr("ORDERTYPE"))
                    Next

                    GRIDISSUE.FirstDisplayedScrollingRowIndex = GRIDISSUE.RowCount - 1
                    TOTAL()
                    DTDATE.Focus()
                    CMBDESIGNERNAME.Enabled = False
                Else
                    EDIT = False
                    CLEAR()
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try


    End Sub

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click

        Try
            If ISLOCKYEAR = True Then
                MsgBox("Unable to Make changes, Year is Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If
            Dim IntResult As Integer

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            alParaval.Add(TXTNO.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(DTDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBDESIGNERNAME.Text.Trim)
            alParaval.Add(LBLTOTALMTRS.Text.Trim)
            alParaval.Add(TXTREMARKS.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim GRIDSRNO As String = ""
            Dim ORDERNO As String = ""
            Dim NAME As String = ""
            Dim ITEMNAME As String = ""
            Dim DESIGN As String = ""
            Dim SHADE As String = ""
            Dim MTRS As String = ""
            Dim ORDERSRNO As String = ""
            Dim ORDERTYPE As String = ""



            For Each row As Windows.Forms.DataGridViewRow In GRIDISSUE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(row.Cells(GSRNO.Index).Value)
                        ORDERNO = row.Cells(GORDERNO.Index).Value
                        NAME = row.Cells(GPARTYNAME.Index).Value.ToString
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        SHADE = row.Cells(GSHADE.Index).Value.ToString
                        MTRS = row.Cells(GMTRS.Index).Value
                        ORDERSRNO = row.Cells(GORDERSRNO.Index).Value
                        ORDERTYPE = row.Cells(GORDERTYPE.Index).Value.ToString
                    Else

                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(GSRNO.Index).Value)
                        ORDERNO = ORDERNO & "|" & row.Cells(GORDERNO.Index).Value
                        NAME = NAME & "|" & row.Cells(GPARTYNAME.Index).Value.ToString
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        SHADE = SHADE & "|" & row.Cells(GSHADE.Index).Value.ToString
                        MTRS = MTRS & "|" & row.Cells(GMTRS.Index).Value
                        ORDERSRNO = ORDERSRNO & "|" & row.Cells(GORDERSRNO.Index).Value
                        ORDERTYPE = ORDERTYPE & "|" & row.Cells(GORDERTYPE.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(ORDERNO)
            alParaval.Add(NAME)
            alParaval.Add(ITEMNAME)
            alParaval.Add(DESIGN)
            alParaval.Add(SHADE)
            alParaval.Add(MTRS)
            alParaval.Add(ORDERSRNO)
            alParaval.Add(ORDERTYPE)

            Dim objclsPurord As New ClsIssueToDesigner()
            objclsPurord.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = objclsPurord.SAVE()
                MessageBox.Show("Details Added")
                TXTNO.Text = DT.Rows(0).Item(0)
            Else


                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPISSNO)
                IntResult = objclsPurord.UPDATE()
                MessageBox.Show("Details Updated")
                EDIT = False
            End If

            CLEAR()
            CMBDESIGNERNAME.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub


    Private Function ERRORVALID() As Boolean

        Dim bln As Boolean = True



        If DTDATE.Text = "__/__/____" Then
            EP.SetError(DTDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTDATE.Text) Then
                EP.SetError(DTDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If CMBDESIGNERNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBDESIGNERNAME, " Please Fill Designer Name ")
            bln = False
        End If

        If GRIDISSUE.RowCount = 0 Then
            EP.SetError(CMBDESIGNERNAME, " Please Enter Proper Details ")
            bln = False
        End If

        Return bln

    End Function

    Private Sub CMDDELETE_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Try
            If ISLOCKYEAR = True Then
                MsgBox("Unable to Make changes, Year is Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete Issue To Designer?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(Val(TEMPISSNO))
                    alParaval.Add(CmpId)
                    alParaval.Add(YearId)

                    Dim ClsINCTAG As New ClsIssueToDesigner()
                    ClsINCTAG.alParaval = alParaval
                    Dim IntResult As Integer = ClsINCTAG.DELETE()
                    MsgBox("Entry Deleted")
                    CLEAR()
                    EDIT = False

                End If
            Else
                MsgBox("Delete Entry")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub



    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            GRIDISSUE.RowCount = 0
LINE1:
            TEMPISSNO = Val(TXTNO.Text) - 1
            If TEMPISSNO > 0 Then
                EDIT = True
                IssueToDesigner_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDISSUE.RowCount = 0 And TEMPISSNO > 1 Then
                TXTNO.Text = TEMPISSNO
                GoTo LINE1
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            GRIDISSUE.RowCount = 0
LINE1:
            TEMPISSNO = Val(TXTNO.Text) + 1
            GETMAX_ISSENTRY()
            Dim MAXNO As Integer = TXTNO.Text.Trim
            CLEAR()
            If Val(TXTNO.Text) - 1 >= TEMPISSNO Then
                EDIT = True
                IssueToDesigner_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDISSUE.RowCount = 0 And TEMPISSNO < MAXNO Then
                TXTNO.Text = TEMPISSNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub



    Sub TOTAL()
        Try
            LBLTOTALMTRS.Text = 0.0

            Dim dt As New DataTable
            Dim OBJCMN As New ClsCommon

            For Each ROW As DataGridViewRow In GRIDISSUE.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objINVDTLS As New IssueToDesignerDetails
            objINVDTLS.MdiParent = MDIMain
            objINVDTLS.Show()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TOOLDELETE_Click(sender As Object, e As EventArgs) Handles TOOLDELETE.Click
        Try
            Call CMDDELETE_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Try
            CMDOK_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTORDER_Click(sender As Object, e As EventArgs) Handles CMDSELECTORDER.Click
        Try
            If CMBDESIGNERNAME.Text.Trim = "" Then
                MsgBox("Select Designer Name", MsgBoxStyle.Critical)
                CMBDESIGNERNAME.Focus()
                Exit Sub
            End If

            Dim DTSO As New DataTable
            Dim OBJSELECTSO As New SelectSO
            OBJSELECTSO.PARTYNAME = CMBDESIGNERNAME.Text.Trim
            OBJSELECTSO.ShowDialog()
            DTSO = OBJSELECTSO.DT

            If DTSO.Rows.Count > 0 Then

                'BEFORE ADDING THE ROW IN ORDERDER GRID CHECK WHETHER SAME ORDERNO AN SRNO IS PRESENT IN GRID OR NOT
                For Each DTROW As DataRow In DTSO.Rows
                    For Each ROW As DataGridViewRow In GRIDISSUE.Rows
                        If Val(ROW.Cells(GORDERNO.Index).Value) = Val(DTROW("SONO")) And Val(ROW.Cells(GORDERSRNO.Index).Value) = Val(DTROW("GRIDSRNO")) And ROW.Cells(GORDERTYPE.Index).Value = DTROW("TYPE") Then GoTo NEXTLINE
                    Next

                    GRIDISSUE.Rows.Add(0, DTROW("SONO"), DTROW("NAME"), DTROW("ITEMNAME"), DTROW("DESIGN"), DTROW("COLOR"), DTROW("MTRS"), DTROW("GRIDSRNO"), DTROW("TYPE"))
NEXTLINE:
                Next
                getsrno(GRIDISSUE)

            End If

            CMBDESIGNERNAME.Enabled = False
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GRIDMANUALENTRY_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDISSUE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDISSUE.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDISSUE.Rows.RemoveAt(GRIDISSUE.CurrentRow.Index)
                TOTAL()
                getsrno(GRIDISSUE)

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub



    Private Sub tstxtbillno_Validated(sender As Object, e As EventArgs) Handles tstxtbillno.Validated
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDISSUE.RowCount = 0
                TEMPISSNO = Val(tstxtbillno.Text)
                If TEMPISSNO > 0 Then
                    EDIT = True
                    IssueToDesigner_Load(sender, e)
                Else
                    EDIT = False
                    CLEAR()
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub





    Private Sub TXTSQUARE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstxtbillno.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub CMBDESIGNERNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBDESIGNERNAME.Validating
        Try
            If CMBDESIGNERNAME.Text.Trim <> "" Then DESIGNERVALIDATE(CMBDESIGNERNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNERNAME_Enter(sender As Object, e As EventArgs) Handles CMBDESIGNERNAME.Enter
        Try
            If CMBDESIGNERNAME.Text.Trim = "" Then fillDESIGNER(CMBDESIGNERNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class

