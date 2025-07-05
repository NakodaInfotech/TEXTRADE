
Imports System.ComponentModel
Imports BL

Public Class UpdatePendingEntry

    Public TEMPPENDINGNO As Integer
    Public EDIT As Boolean          'used for editing

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(UPDATE_NO),0) + 1 ", " UPDATELOCKPENDINGENTRY ", " AND UPDATE_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTPENDINGNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub CMDSAVE_Click(sender As Object, e As EventArgs) Handles CMDSAVE.Click
        Try

            If gridbill.FilterPanelText <> "" Then gridbill.ActiveFilterEnabled = False
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(Format(PENDINGDATE.Value.Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)

            Dim GRIDSRNO As String = ""
            Dim NO As String = ""
            Dim INITIALS As String = ""
            Dim TYPE As String = ""


            For I As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(I)
                If GRIDSRNO = "" Then
                    GRIDSRNO = dtrow("GRIDSRNO")
                    NO = dtrow("NO")
                    INITIALS = dtrow("INITIALS")
                    TYPE = dtrow("TYPE")
                Else
                    GRIDSRNO = GRIDSRNO & "|" & dtrow("GRIDSRNO")
                    NO = NO & "|" & dtrow("NO")
                    INITIALS = INITIALS & "|" & dtrow("INITIALS")
                    TYPE = TYPE & "|" & dtrow("TYPE")
                End If
            Next
            ALPARAVAL.Add(GRIDSRNO)
            ALPARAVAL.Add(NO)
            ALPARAVAL.Add(INITIALS)
            ALPARAVAL.Add(TYPE)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            Dim OBJPENDING As New ClsUpdatePendingEntry
            OBJPENDING.ALPARAVAL = ALPARAVAL
            If EDIT = False Then

                Dim DTTABLE As DataTable = OBJPENDING.SAVE()
                MsgBox("Details Added")
                TXTPENDINGNO.Text = DTTABLE.Rows(0).Item(0)
                TEMPPENDINGNO = DTTABLE.Rows(0).Item(0)

            ElseIf EDIT = True Then


                ALPARAVAL.Add(TEMPPENDINGNO)
                Dim IntResult As Integer = OBJPENDING.UPDATE()
                MsgBox("Details Updated")
                EDIT = False
            End If
            CLEAR()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try

            Cursor.Current = Cursors.WaitCursor
            gridbilldetails.DataSource = Nothing
LINE1:
            TEMPPENDINGNO = Val(TXTPENDINGNO.Text) - 1
            If TEMPPENDINGNO > 0 Then
                EDIT = True
                UpdatePendingEntry_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If gridbill.RowCount = 0 And TEMPPENDINGNO > 1 Then
                TXTPENDINGNO.Text = TEMPPENDINGNO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try

LINE1:
            TEMPPENDINGNO = Val(TXTPENDINGNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTPENDINGNO.Text.Trim
            CLEAR()
            If Val(TXTPENDINGNO.Text) - 1 >= TEMPPENDINGNO Then
                EDIT = True
                UpdatePendingEntry_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If gridbill.RowCount = 0 And TEMPPENDINGNO < MAXNO Then
                TXTPENDINGNO.Text = TEMPPENDINGNO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        PENDINGDATE.Focus()
    End Sub

    Sub CLEAR()

        PENDINGDATE.Value = Now.Date
        tstxtbillno.Clear()
        TXTREMARKS.Clear()
        gridbilldetails.DataSource = Nothing
        getmaxno()

    End Sub

    Private Sub CMDDELETE_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Try
            If EDIT = True Then
                If MsgBox("Wish to Delete Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                Dim OBJPENDING As New ClsUpdatePendingEntry

                ALPARAVAL.Add(TEMPPENDINGNO)
                ALPARAVAL.Add(YearId)
                OBJPENDING.ALPARAVAL = ALPARAVAL
                Dim INTRES As Integer = OBJPENDING.DELETE()
                MsgBox("Entry Deleted Succesfully")
                CLEAR()
                EDIT = False
                PENDINGDATE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJENTRY As New UpdatePendingEntryDetails
            OBJENTRY.MdiParent = MDIMain
            OBJENTRY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Call CMDSAVE_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(sender As Object, e As EventArgs) Handles tooldelete.Click
        Call CMDDELETE_Click(sender, e)
    End Sub

    Private Sub UpdatePendingEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CLEAR()
            If EDIT = True Then

                Dim OBJPENDING As New ClsUpdatePendingEntry()
                Dim dttable As DataTable = OBJPENDING.SELECTPENDINGENTRY(TEMPPENDINGNO, CmpId, YearId)
                If dttable.Rows.Count > 0 Then
                    TXTPENDINGNO.Text = TEMPPENDINGNO
                    PENDINGDATE.Value = Format(Convert.ToDateTime(dttable.Rows(0).Item("DATE")).Date, "dd/MM/yyyy")
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS")
                    gridbilldetails.DataSource = dttable
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(sender As Object, e As CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                gridbilldetails.DataSource = Nothing
                TEMPPENDINGNO = Val(tstxtbillno.Text)
                If TEMPPENDINGNO > 0 Then
                    EDIT = True
                    UpdatePendingEntry_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SELECTPENDINGENTRY_Click(sender As Object, e As EventArgs) Handles SELECTPENDINGENTRY.Click
        Try
            Dim OBJSELECT As New SelectPendingEntries
            OBJSELECT.ShowDialog()
            If OBJSELECT.DT1.Rows.Count > 0 Then gridbilldetails.DataSource = OBJSELECT.DT1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class