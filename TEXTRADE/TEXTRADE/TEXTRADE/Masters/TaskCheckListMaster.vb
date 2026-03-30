Imports System.ComponentModel
Imports BL


Public Class TaskCheckListMaster

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Public TEMPTASKNO As Integer          'used for editing
    Public EDIT As Boolean          'used for editing
    Dim TEMPROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub TaskCheckListMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If ERRORVALID() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1 Then       'for Delete
            TabControl1.SelectedIndex = (0)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2 Then       'for Delete
            TabControl1.SelectedIndex = (1)
        ElseIf e.KeyCode = Keys.OemPipe Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
            toolprevious_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
            toolnext_Click(sender, e)
        ElseIf e.KeyCode = Keys.F5 Then     'grid focus
            GRIDTASK.Focus()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        End If
    End Sub

    'Sub FILLCMB()
    '    If CMBTASKTYPE.Text.Trim = "" Then FILLTASKTYPE(CMBTASKTYPE, EDIT, "")
    'End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True

            If CMBTASKTYPE.Text.Trim.Length = 0 Then
                EP.SetError(CMBTASKTYPE, " Please Fill Task Type")
                bln = False
            End If

            If GRIDTASK.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If
            'CHEKC BARCODE IS PRESENT IN DATABASE OR NOT

            If Not datecheck(DTTASKDATE.Text) Then
                EP.SetError(DTTASKDATE, "Date not in Accounting Year")
                bln = False
            End If

            If Convert.ToDateTime(DTTASKDATE.Text).Date < STOCKADJBLOCKDATE.Date Then
                EP.SetError(DTTASKDATE, "Date is Blocked, Please make entries after " & Format(STOCKADJBLOCKDATE.Date, "dd/MM/yyyy"))    'UNCOMMENT AFTER ADDING BLOCKDATE
                bln = False
            End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDTASK.RowCount = 0
LINE1:
            TEMPTASKNO = Val(TXTTASKNO.Text) - 1
            If TEMPTASKNO > 0 Then
                EDIT = True
                TaskCheckListMaster_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDTASK.RowCount = 0 And TEMPTASKNO > 1 Then
                TXTTASKNO.Text = TEMPTASKNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        DTTASKDATE.Focus()
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
LINE1:
            TEMPTASKNO = Val(TXTTASKNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTTASKNO.Text.Trim
            CLEAR()
            If Val(TXTTASKNO.Text) - 1 >= TEMPTASKNO Then
                EDIT = True
                TaskCheckListMaster_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDTASK.RowCount = 0 And TEMPTASKNO < MAXNO Then
                TXTTASKNO.Text = TEMPTASKNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try

            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Val(TXTTASKNO.Text.Trim))
            alParaval.Add(Format(Convert.ToDateTime(DTTASKDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBTASKTYPE.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(LBLTOTALTASK.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim gridsrno As String = ""
            Dim CHECKTASK As String = ""
            Dim TASK As String = ""
            Dim REMARKS As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDTASK.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(GSRNO.Index).Value.ToString
                        CHECKTASK = row.Cells(GCHKTASK.Index).Value
                        TASK = row.Cells(GTASK.Index).Value
                        REMARKS = row.Cells(GREMARKS.Index).Value.ToString
                    Else
                        gridsrno = gridsrno & "|" & row.Cells(GSRNO.Index).Value.ToString
                        CHECKTASK = CHECKTASK & "|" & row.Cells(GCHKTASK.Index).Value
                        TASK = TASK & "|" & row.Cells(GTASK.Index).Value
                        REMARKS = REMARKS & "|" & row.Cells(GREMARKS.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(CHECKTASK)
            alParaval.Add(TASK)
            alParaval.Add(REMARKS)

            Dim objSTOCK As New ClsTaskCheckListMaster()
            objSTOCK.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = objSTOCK.SAVE()
                MsgBox("Details Added")
                TXTTASKNO.Text = DTTABLE.Rows(0).Item(0)
                TEMPTASKNO = DTTABLE.Rows(0).Item(0)
                'PRINTREPORT(DTTABLE.Rows(0).Item(0))

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                alParaval.Add(TEMPTASKNO)
                IntResult = objSTOCK.UPDATE()
                MsgBox("Details Updated")
                'PRINTREPORT(TEMPTASKNO)
                EDIT = False
            End If


            CLEAR()
            DTTASKDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then
                If MsgBox("Wish to Delete Task Check List?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                Dim OBSTOCK As New ClsTaskCheckListMaster

                ALPARAVAL.Add(TEMPTASKNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                OBSTOCK.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBSTOCK.DELETE()
                MsgBox("Task Check List Deleted Succesfully")
                CLEAR()
                EDIT = False
                DTTASKDATE.Focus()
            End If
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

            Dim OBJstock As New TaskCheckListDetails
            OBJstock.MdiParent = MDIMain
            OBJstock.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(TASKCHECK_NO),0) + 1 ", " TASKCHECKMASTER ", " AND TASKCHECK_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTTASKNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Sub CLEAR()
        CMBTASKTYPE.Text = ""
        DTTASKDATE.Text = Now.Date
        txtremarks.Clear()
        EP.Clear()

        GRIDTASK.RowCount = 0
        GRIDDOUBLECLICK = False
        TabControl1.SelectedIndex = 0
        getmaxno()

        LBLTOTALTASK.Text = 0.0
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then
                PRINTREPORT()
                If GRIDTASK.RowCount > 0 Then
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If MsgBox("Wish to Print Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJSA As New SaleOrderDesign
            OBJSA.MdiParent = MDIMain
            OBJSA.FORMULA = "{TASKCHECKMASTER.TASKCHECK_NO} = " & Val(TXTTASKNO.Text.Trim) & " AND {TASKCHECKMASTER.TASKCHECK_YEARID} = " & YearId
            OBJSA.FRMSTRING = "STORESTOCKRECO"
            OBJSA.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub tooldelete_Click(sender As Object, e As EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Try
            Call cmdok_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TaskCheckListMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'STORESTOCKRECO'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            'FILLCMB()
            CLEAR()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If


                Dim objSTOCK As New ClsTaskCheckListMaster()
                Dim dttable As DataTable = objSTOCK.SELECTTASKCHECKLIST(TEMPTASKNO, CmpId, Locationid, YearId)
                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows
                        TXTTASKNO.Text = TEMPTASKNO
                        TXTTASKNO.ReadOnly = True
                        DTTASKDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBTASKTYPE.Text = Convert.ToString(dr("TASKTYPE").ToString)
                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        LBLTOTALTASK.Text = Convert.ToString(dr("TOTALTASK").ToString)

                        'Task Grid
                        If Val(dr("GRIDSRNO")) > 0 Then GRIDTASK.Rows.Add(dr("GRIDSRNO").ToString, dr("CHECKTASK"), dr("TASK").ToString, dr("REMARKS").ToString)
                    Next
                Else
                    EDIT = False
                    CLEAR()
                End If
                TOTAL()
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALTASK.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDTASK.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALTASK.Text = Format(Val(LBLTOTALTASK.Text) + Val(ROW.Cells(GTASK.Index).EditedFormattedValue), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETSRNO(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    'Private Sub CMBTASKTYPE_Enter(sender As Object, e As EventArgs)
    '    Try
    '        If CMBTASKTYPE.Text.Trim = "" Then FILLTASKTYPE(CMBTASKTYPE, EDIT, "")
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
End Class