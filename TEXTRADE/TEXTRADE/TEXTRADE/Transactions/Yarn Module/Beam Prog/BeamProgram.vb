Imports System.ComponentModel
Imports BL

Public Class BeamProgram

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Public TEMPBEAMPROGNO As Integer
    Dim TEMPROW As Integer
    Dim GRIDDOUBLECLICK As Boolean

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()
        Ep.Clear()
        tstxtbillno.Clear()
        CMBNAME.Text = ""
        PROGDATE.Text = Now.Date
        txtremarks.Clear()
        LBLTOTALWT.Text = 0

        TXTSRNO.Text = 1
        CMBYARNQUALITY.Text = ""
        CMBMILL.Text = ""
        TXTLOTNO.Clear()
        TXTDENIER.Clear()
        TXTREED.Clear()
        TXTTL.Clear()
        TXTREEDSPACE.Clear()
        TXTWT.Clear()
        TXTGRIDREMARKS.Clear()

        LBLCLOSED.Visible = False
        PBlock.Visible = False
        lbllocked.Visible = False

        GET_MAX_NO()
        GRIDPROG.RowCount = 0
        GRIDDOUBLECLICK = False
    End Sub

    Sub GET_MAX_NO()
        Dim DT As New DataTable
        DT = getmax(" isnull(max(BEAMPRM_NO),0) + 1 ", " BEAMPROGRAM ", " AND BEAMPRM_YEARID = " & YearId)
        If DT.Rows.Count > 0 Then TXTPROGNO.Text = DT.Rows(0).Item(0)
    End Sub

    Sub GETSRNO(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each ROW As DataGridViewRow In GRIDPROG.Rows
                ROW.Cells(0).Value = ROW.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim BLN As Boolean = True
        If CMBNAME.Text.Trim = "" Then
            Ep.SetError(CMBNAME, "Please Enter Party Name")
            BLN = False
        End If

        If GRIDPROG.RowCount = 0 Then
            Ep.SetError(CMBNAME, "Please Enter Yarn Details")
            BLN = False
        End If

        If LBLCLOSED.Visible = True Then
            Ep.SetError(LBLCLOSED, "Unable to Modify, Entry Cloed")
            BLN = False
        End If

        If lbllocked.Visible = True Then
            Ep.SetError(lbllocked, "Unable to Modify, Entry Locked")
            BLN = False
        End If

        Return BLN
    End Function

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Dim IntResult As New Integer
        Try
            Cursor.Current = Cursors.WaitCursor
            Ep.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(CMBNAME.Text.Trim)
            ALPARAVAL.Add(Format(Convert.ToDateTime(PROGDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(Val(LBLTOTALWT.Text.Trim))
            ALPARAVAL.Add(txtremarks.Text.Trim)

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            ''grid parameter
            Dim SRNO As String = ""
            Dim QUALITYNAME As String = ""
            Dim MILLNAME As String = ""
            Dim LOTNO As String = ""
            Dim DENIER As String = ""
            Dim REED As String = ""
            Dim TL As String = ""
            Dim REEDSPACE As String = ""
            Dim WT As String = ""
            Dim GRIDREMARKS As String = ""

            For Each ROW As Windows.Forms.DataGridViewRow In GRIDPROG.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = ROW.Cells(gsrno.Index).Value
                        QUALITYNAME = ROW.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = ROW.Cells(GMILLNAME.Index).Value.ToString
                        LOTNO = ROW.Cells(GLOTNO.Index).Value.ToString
                        DENIER = ROW.Cells(GDENIER.Index).Value.ToString
                        REED = Val(ROW.Cells(GREED.Index).Value)
                        TL = Val(ROW.Cells(GTL.Index).Value)
                        REEDSPACE = Val(ROW.Cells(GREEDSPACE.Index).Value)
                        WT = Val(ROW.Cells(GWT.Index).Value)
                        GRIDREMARKS = ROW.Cells(GGRIDREMARKS.Index).Value.ToString

                    Else
                        SRNO = SRNO & "|" & ROW.Cells(gsrno.Index).Value
                        QUALITYNAME = QUALITYNAME & "|" & ROW.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & ROW.Cells(GMILLNAME.Index).Value.ToString
                        LOTNO = LOTNO & "|" & ROW.Cells(GLOTNO.Index).Value.ToString
                        DENIER = DENIER & "|" & ROW.Cells(GDENIER.Index).Value.ToString
                        REED = REED & "|" & Val(ROW.Cells(GREED.Index).Value)
                        TL = TL & "|" & Val(ROW.Cells(GTL.Index).Value)
                        REEDSPACE = REEDSPACE & "|" & Val(ROW.Cells(GREEDSPACE.Index).Value)
                        WT = WT & "|" & Val(ROW.Cells(GWT.Index).Value)
                        GRIDREMARKS = GRIDREMARKS & "|" & ROW.Cells(GGRIDREMARKS.Index).Value.ToString
                    End If
                End If
            Next
            ALPARAVAL.Add(SRNO)
            ALPARAVAL.Add(QUALITYNAME)
            ALPARAVAL.Add(MILLNAME)
            ALPARAVAL.Add(LOTNO)
            ALPARAVAL.Add(DENIER)
            ALPARAVAL.Add(REED)
            ALPARAVAL.Add(TL)
            ALPARAVAL.Add(REEDSPACE)
            ALPARAVAL.Add(WT)
            ALPARAVAL.Add(GRIDREMARKS)



            Dim OBJBP As New ClsBeamProgram
            OBJBP.alParaval = ALPARAVAL

            If EDIT = False Then
                Dim DT As DataTable = OBJBP.SAVE()
                MsgBox("Detail Added!!")
                TXTPROGNO.Text = DT.Rows(0).Item(0)
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                ALPARAVAL.Add(TEMPBEAMPROGNO)
                IntResult = OBJBP.UPDATE()
                MsgBox("Detail Updated")
                EDIT = False
            End If
            CLEAR()
            CMBNAME.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        If GRIDDOUBLECLICK = False Then
            GRIDPROG.Rows.Add(Val(TXTSRNO.Text.Trim), CMBYARNQUALITY.Text.Trim, CMBMILL.Text.Trim, TXTLOTNO.Text.Trim, Val(TXTDENIER.Text.Trim), Val(TXTREED.Text.Trim), Val(TXTTL.Text.Trim), Val(TXTREEDSPACE.Text.Trim), Format(Val(TXTWT.Text.Trim), "0.000"), TXTGRIDREMARKS.Text.Trim)
            GETSRNO(GRIDPROG)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDPROG.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text)
            GRIDPROG.Item(GYARNQUALITY.Index, TEMPROW).Value = CMBYARNQUALITY.Text.Trim
            GRIDPROG.Item(GMILLNAME.Index, TEMPROW).Value = CMBMILL.Text.Trim
            GRIDPROG.Item(GLOTNO.Index, TEMPROW).Value = TXTLOTNO.Text.Trim
            GRIDPROG.Item(GDENIER.Index, TEMPROW).Value = Val(TXTDENIER.Text.Trim)
            GRIDPROG.Item(GREED.Index, TEMPROW).Value = Val(TXTREED.Text.Trim)
            GRIDPROG.Item(GTL.Index, TEMPROW).Value = Val(TXTTL.Text.Trim)
            GRIDPROG.Item(GREEDSPACE.Index, TEMPROW).Value = Val(TXTREEDSPACE.Text.Trim)
            GRIDPROG.Item(GWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.000")
            GRIDPROG.Item(GGRIDREMARKS.Index, TEMPROW).Value = TXTGRIDREMARKS.Text.Trim
            GRIDDOUBLECLICK = False
        End If
        TOTAL()
        GRIDPROG.FirstDisplayedScrollingRowIndex = GRIDPROG.RowCount - 1

        TXTSRNO.Text = Val(GRIDPROG.RowCount) + 1
        CMBYARNQUALITY.Text = ""
        CMBMILL.Text = ""
        TXTLOTNO.Clear()
        TXTDENIER.Clear()
        TXTREED.Clear()
        TXTTL.Clear()
        TXTREEDSPACE.Clear()
        TXTWT.Clear()
        TXTGRIDREMARKS.Clear()
        CMBYARNQUALITY.Focus()

    End Sub

    Private Sub GRIDPROG_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDPROG.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            fillname(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            fillYARNQUALITY(CMBYARNQUALITY, EDIT)
            FILLMILL(CMBMILL, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_Enter(sender As Object, e As EventArgs) Handles CMBYARNQUALITY.Enter
        Try
            If CMBYARNQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBYARNQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamProgram_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'YARN ISSUE'")
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


                Dim DT As DataTable
                Dim OBJ As New ClsBeamProgram
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPBEAMPROGNO)
                ALPARAVAL.Add(YearId)
                OBJ.alParaval = ALPARAVAL
                DT = OBJ.SELECTNO()

                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows
                        TXTPROGNO.Text = TEMPBEAMPROGNO
                        PROGDATE.Text = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(DR("CMPNAME"))
                        txtremarks.Text = Convert.ToString(DR("REMARKS"))

                        ''For GRID
                        GRIDPROG.Rows.Add(Val(DR("GRIDSRNO")), DR("YARNNAME").ToString, DR("MILLNAME").ToString, DR("LOTNO").ToString, Val(DR("DENIER")), Val(DR("REED")), Val(DR("TL")), Val(DR("REEDSPACE")), Format(Val(DR("WT")), "0.000"), DR("GRIDREMARKS").ToString)

                        If Convert.ToBoolean(DR("CLOSED")) = True Then
                            PBlock.Visible = True
                            LBLCLOSED.Visible = True
                        End If

                    Next
                    GRIDPROG.FirstDisplayedScrollingRowIndex = GRIDPROG.RowCount - 1
                End If
                TOTAL()
                GETSRNO(GRIDPROG)
                CMBNAME.Focus()

            Else
                EDIT = False
                CLEAR()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBYARNQUALITY.Validating
        Try
            If CMBYARNQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBYARNQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_Validated(sender As Object, e As EventArgs) Handles CMBYARNQUALITY.Validated
        Try
            If CMBYARNQUALITY.Text.Trim <> "" Then
                Dim OBJ As New ClsCommon
                Dim DT As DataTable = OBJ.Execute_Any_String("SELECT ISNULL(YARN_DENIER,0) AS DENIER FROM YARNQUALITYMASTER WHERE YARN_NAME = '" & CMBYARNQUALITY.Text.Trim & "' AND YARN_YEARID = " & YearId, "", "")
                If DT.Rows.Count > 0 Then
                    TXTDENIER.Text = Val(DT.Rows(0).Item("DENIER"))
                    CALC()
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            TXTWT.Text = 0
            If CMBYARNQUALITY.Text.Trim <> "" And Val(TXTDENIER.Text.Trim) > 0 And Val(TXTTL.Text.Trim) > 0 And Val(TXTREEDSPACE.Text.Trim) > 0 And Val(TXTREED.Text.Trim) > 0 Then
                TXTWT.Text = Format((Val(TXTDENIER.Text.Trim) * Val(TXTREED.Text.Trim) * Val(TXTREEDSPACE.Text.Trim) * Val(TXTTL.Text.Trim)) / 9000000, "0.000")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGRIDREMARKS_Validated(sender As Object, e As EventArgs) Handles TXTGRIDREMARKS.Validated
        Try
            If CMBYARNQUALITY.Text.Trim <> "" And Val(TXTDENIER.Text.Trim) > 0 And Val(TXTREED.Text.Trim) > 0 And Val(TXTREEDSPACE.Text.Trim) > 0 And Val(TXTTL.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 Then FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(sender As Object, e As CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then MILLVALIDATE(CMBMILL, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS'", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            GRIDPROG.RowCount = 0
LINE1:
            TEMPBEAMPROGNO = Val(TXTPROGNO.Text) - 1
            If TEMPBEAMPROGNO > 0 Then
                EDIT = True
                BeamProgram_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDPROG.RowCount = 0 And TEMPBEAMPROGNO > 1 Then
                TXTPROGNO.Text = TEMPBEAMPROGNO
                GoTo LINE1
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            GRIDPROG.RowCount = 0
LINE1:
            TEMPBEAMPROGNO = Val(TXTPROGNO.Text) + 1
            GET_MAX_NO()
            Dim MAXNO As Integer = TXTPROGNO.Text.Trim
            CLEAR()
            If Val(TXTPROGNO.Text) - 1 >= TEMPBEAMPROGNO Then
                EDIT = True
                BeamProgram_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDPROG.RowCount = 0 And TEMPBEAMPROGNO < MAXNO Then
                TXTPROGNO.Text = TEMPBEAMPROGNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTREEDSPACE_Validating(sender As Object, e As CancelEventArgs) Handles TXTREEDSPACE.Validating, TXTTL.Validating
        Try
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If EDIT = True Then
                Dim OBJBEAM As New ClsBeamProgram
                If MsgBox("Wish to Delete?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                Dim alparaval As New ArrayList
                OBJBEAM.alParaval.Add(TXTPROGNO.Text.Trim)
                OBJBEAM.alParaval.Add(Userid)
                OBJBEAM.alParaval.Add(YearId)
                Dim intresult As Integer = OBJBEAM.DELETE
                MsgBox("Entry Deleted!!!")
                CLEAR()
                EDIT = False
            Else
                MsgBox("Delete is only in Edit Mode")
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJJOB As New BeamProgramDetails
            OBJJOB.MdiParent = MDIMain
            OBJJOB.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDPROG.CurrentRow.Index >= 0 And GRIDPROG.Item(gsrno.Index, GRIDPROG.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDPROG.Item(gsrno.Index, GRIDPROG.CurrentRow.Index).Value
                CMBYARNQUALITY.Text = GRIDPROG.Item(GYARNQUALITY.Index, GRIDPROG.CurrentRow.Index).Value.ToString
                CMBMILL.Text = GRIDPROG.Item(GMILLNAME.Index, GRIDPROG.CurrentRow.Index).Value.ToString
                TXTLOTNO.Text = GRIDPROG.Item(GLOTNO.Index, GRIDPROG.CurrentRow.Index).Value.ToString
                TXTDENIER.Text = Val(GRIDPROG.Item(GDENIER.Index, GRIDPROG.CurrentRow.Index).Value)
                TXTREED.Text = Val(GRIDPROG.Item(GREED.Index, GRIDPROG.CurrentRow.Index).Value)
                TXTTL.Text = Val(GRIDPROG.Item(GTL.Index, GRIDPROG.CurrentRow.Index).Value)
                TXTREEDSPACE.Text = Val(GRIDPROG.Item(GREEDSPACE.Index, GRIDPROG.CurrentRow.Index).Value)
                TXTWT.Text = Val(GRIDPROG.Item(GWT.Index, GRIDPROG.CurrentRow.Index).Value)
                TXTGRIDREMARKS.Text = GRIDPROG.Item(GGRIDREMARKS.Index, GRIDPROG.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDPROG.CurrentRow.Index
                CMBYARNQUALITY.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPBEAMPROGNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(ByVal WEFTISSNO As Integer)
        Try
            'If MsgBox("Wish to Print Weft Issue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            'Dim OBJPUR As New WeftIssueDesign
            'OBJPUR.MdiParent = MDIMain
            'OBJPUR.WHERECLAUSE = "{WEFTISSUEMASTER.WEFTISS_NO}=" & Val(WEFTISSNO) & " and {WEFTISSUEMASTER.WEFTISS_YEARID}=" & YearId
            'OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALWT.Text = 0
            For Each row As DataGridViewRow In GRIDPROG.Rows
                LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(row.Cells(GWT.Index).EditedFormattedValue), "0.000")
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(sender As Object, e As CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) = 0 Then Exit Sub
            GRIDPROG.RowCount = 0
            TEMPBEAMPROGNO = Val(tstxtbillno.Text)
            If TEMPBEAMPROGNO > 0 Then
                EDIT = True
                BeamProgram_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(sender As Object, e As EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub CMCLOSE_Click(sender As Object, e As EventArgs) Handles CMCLOSE.Click
        Try
            If EDIT = True Then
                If MsgBox("Wish To Close Program?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE BEAMPROGRAM SET BEAMPRM_CLOSED = 1 WHERE BEAMPROGRAM.BEAMPRM_NO = " & Val(TXTPROGNO.Text.Trim) & " AND BEAMPROGRAM.BEAMPRM_YEARID = " & YearId, "", "")
                CLEAR()
                EDIT = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamProgram_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call toolprevious_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call toolnext_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.F5 Then
                GRIDPROG.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(sender As Object, e As EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then FILLMILL(CMBMILL, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class