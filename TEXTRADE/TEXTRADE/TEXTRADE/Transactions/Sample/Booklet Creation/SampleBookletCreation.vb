
Imports System.ComponentModel
Imports BL

Public Class SampleBookletCreation

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public TEMPNO As Integer

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()

        SMDATE.Text = Now.Date
        CMBSAMPLETYPE.Text = ""
        cmbitemname.Text = ""
        CMBDESIGN.Text = ""
        CMBSHADE.Text = ""
        TXTNOOFBOOKLET.Clear()
        TXTNARRATION.Clear()
        GRIDSMP.RowCount = 0

        LBLTOTAL.Text = 0
        CMBPARTYNAME.Text = ""
        TXTSAMPLECREATED.Clear()
        TXTBARCODE.Clear()
        If USERGODOWN <> "" Then CMBGODOWN.Text = USERGODOWN Else CMBGODOWN.Text = ""
        txtsrno.Text = 1
        GETMAXNO()
        EP.Clear()
        tstxtbillno.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False
        SMDATE.Focus()
    End Sub

    Sub GETMAXNO()
        Try
            Dim DTTABLE As New DataTable
            DTTABLE = getmax("  isnull(max(sm_no),0)+1 ", " SAMPLEBOOKLETCREATION ", " AND SM_YEARID =" & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTNO.Text = DTTABLE.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True

            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, "Invoice Raised, Delete Invoice First")
                bln = False
            End If

            If GRIDSMP.RowCount = 0 Then
                EP.SetError(cmbitemname, "Enter Grid Detail")
                bln = False
            End If
            If CMBGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBGODOWN, " Please Select Godown")
                bln = False
            End If

            If SMDATE.Text = "__/__/____" Then
                EP.SetError(SMDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(SMDATE.Text) Then
                    EP.SetError(SMDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If

            If CMBPARTYNAME.Text.Trim = "" And TXTSAMPLECREATED.Text.Trim = "" And ClientName = "YASHVI" Then
                EP.SetError(CMBGODOWN, " Please Select Created By")
                bln = False
            End If
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            alParaval.Add(Format(Convert.ToDateTime(SMDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(Val(LBLTOTAL.Text.Trim))

            Dim GRIDSRNO As String = ""
            Dim GRIDSAMPLETYPE As String = ""
            Dim GRIDITEM As String = ""
            Dim GRIDDESIGN As String = ""
            Dim GRIDSHADE As String = ""
            Dim GRIDNOOFBOOKLATE As String = ""
            Dim GRIDNARRATION As String = ""

            For Each ROW As Windows.Forms.DataGridViewRow In GRIDSMP.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(ROW.Cells(gsrno.Index).Value)
                        GRIDSAMPLETYPE = ROW.Cells(GSAMPLETYPE.Index).Value
                        GRIDITEM = ROW.Cells(gitemname.Index).Value
                        GRIDDESIGN = ROW.Cells(GDESIGN.Index).Value
                        GRIDSHADE = ROW.Cells(gcolor.Index).Value
                        GRIDNOOFBOOKLATE = Val(ROW.Cells(GNOOFBOOKLET.Index).Value)
                        GRIDNARRATION = ROW.Cells(GNARRATION.Index).Value
                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(ROW.Cells(gsrno.Index).Value)
                        GRIDSAMPLETYPE = GRIDSAMPLETYPE & "|" & ROW.Cells(GSAMPLETYPE.Index).Value
                        GRIDITEM = GRIDITEM & "|" & ROW.Cells(gitemname.Index).Value
                        GRIDDESIGN = GRIDDESIGN & "|" & ROW.Cells(GDESIGN.Index).Value
                        GRIDSHADE = GRIDSHADE & "|" & ROW.Cells(gcolor.Index).Value
                        GRIDNOOFBOOKLATE = GRIDNOOFBOOKLATE & "|" & Val(ROW.Cells(GNOOFBOOKLET.Index).Value)
                        GRIDNARRATION = GRIDNARRATION & "|" & ROW.Cells(GNARRATION.Index).Value
                    End If
                End If
            Next
            alParaval.Add(GRIDSRNO)
            alParaval.Add(GRIDSAMPLETYPE)
            alParaval.Add(GRIDITEM)
            alParaval.Add(GRIDDESIGN)
            alParaval.Add(GRIDSHADE)
            alParaval.Add(GRIDNOOFBOOKLATE)
            alParaval.Add(GRIDNARRATION)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            alParaval.Add(CMBPARTYNAME.Text.Trim)
            alParaval.Add(TXTSAMPLECREATED.Text.Trim)
            alParaval.Add(CMBGODOWN.Text.Trim)

            Dim OBJSMAPLE As New ClsSampleBookletCreation
            OBJSMAPLE.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = OBJSMAPLE.SAVE()
                MsgBox("Details Added")
                TXTNO.Text = DTTABLE.Rows(0).Item(0)
            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPNO)
                Dim IntResult As Integer = OBJSMAPLE.UPDATE()
                MsgBox("Details Updated")
                EDIT = False
            End If
            clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SampleBookletCreation_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemQuotes Or e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolprevious_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillname(CMBPARTYNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' ")
            FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
            FILLCOLOR(CMBSHADE, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
            FILLSAMPLETYPE(CMBSAMPLETYPE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SampleBookletCreation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SAMPLE MODULE'")
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
                Dim OBJCLS As New ClsSampleBookletCreation
                Dim ALPARAVEL As New ArrayList
                ALPARAVEL.Add(TEMPNO)
                ALPARAVEL.Add(CmpId)
                ALPARAVEL.Add(YearId)
                OBJCLS.alParaval = ALPARAVEL

                Dim DT As DataTable = OBJCLS.SELECTSM()
                If DT.Rows.Count > 0 Then
                    For Each dr As DataRow In DT.Rows
                        TXTNO.Text = TEMPNO
                        SMDATE.Text = Format(Convert.ToDateTime(dr("SMDATE")).Date, "dd/MM/yyyy")
                        LBLTOTAL.Text = dr("TOTAL")

                        GRIDSMP.Rows.Add(Val(dr("SRNO")), dr("SAMPLETYPE"), dr("ITEMNAME").ToString, dr("DESIGN"), dr("COLOR"), Val(dr("NOOFBOOKLET")), dr("NARRATION").ToString)

                        CMBPARTYNAME.Text = dr("PARTYNAME")
                        TXTSAMPLECREATED.Text = dr("SAMPLECREATED")
                        CMBGODOWN.Text = dr("GODOWN")


                        If Convert.ToBoolean(dr("DONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If
                    Next
                End If
                total()

            End If
        Catch ex As Exception
            Throw ex
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

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDSMP.Rows.Add(Val(txtsrno.Text.Trim), CMBSAMPLETYPE.Text.Trim, cmbitemname.Text.Trim, CMBDESIGN.Text.Trim, CMBSHADE.Text.Trim, Val(TXTNOOFBOOKLET.Text.Trim), TXTNARRATION.Text.Trim)
                getsrno(GRIDSMP)
            ElseIf GRIDDOUBLECLICK = True Then
                GRIDSMP.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
                GRIDSMP.Item(GSAMPLETYPE.Index, TEMPROW).Value = CMBSAMPLETYPE.Text.Trim
                GRIDSMP.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
                GRIDSMP.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
                GRIDSMP.Item(gcolor.Index, TEMPROW).Value = CMBSHADE.Text.Trim
                GRIDSMP.Item(GNOOFBOOKLET.Index, TEMPROW).Value = Val(TXTNOOFBOOKLET.Text.Trim)
                GRIDSMP.Item(GNARRATION.Index, TEMPROW).Value = TXTNARRATION.Text.Trim
                GRIDDOUBLECLICK = False
                txtsrno.Text = GRIDSMP.RowCount + 1
            End If

            txtsrno.Text = Val(GRIDSMP.RowCount) + 1
            CMBSAMPLETYPE.Text = ""
            cmbitemname.Text = ""
            CMBSHADE.Text = ""
            CMBDESIGN.Text = ""
            TXTNOOFBOOKLET.Clear()
            TXTNARRATION.Clear()
            total()
            cmbitemname.Focus()

            GRIDSMP.FirstDisplayedScrollingRowIndex = GRIDSMP.RowCount - 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridsample_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSMP.CellDoubleClick
        Try

            If GRIDSMP.CurrentRow.Index >= 0 And GRIDSMP.Item(gsrno.Index, GRIDSMP.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                txtsrno.Text = GRIDSMP.Item(gsrno.Index, e.RowIndex).Value.ToString
                CMBSAMPLETYPE.Text = GRIDSMP.Item(GSAMPLETYPE.Index, GRIDSMP.CurrentRow.Index).Value.ToString
                cmbitemname.Text = GRIDSMP.Item(gitemname.Index, e.RowIndex).Value.ToString
                CMBDESIGN.Text = GRIDSMP.Item(GDESIGN.Index, e.RowIndex).Value.ToString
                CMBSHADE.Text = GRIDSMP.Item(gcolor.Index, e.RowIndex).Value.ToString
                TXTNOOFBOOKLET.Text = GRIDSMP.Item(GNOOFBOOKLET.Index, e.RowIndex).Value.ToString
                TXTNARRATION.Text = GRIDSMP.Item(GNARRATION.Index, e.RowIndex).Value.ToString

                TEMPROW = GRIDSMP.CurrentRow.Index
                cmbitemname.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Enter(sender As Object, e As EventArgs) Handles cmbitemname.Enter
        Try
            If cmbitemname.Text.Trim = "" Then fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbitemname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "MERCHANT"
                OBJItem.STRSEARCH = " and ITEM_cmpid = " & CmpId & " and ITEM_LOCATIONid = " & Locationid & " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then cmbitemname.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validating(sender As Object, e As CancelEventArgs) Handles cmbitemname.Validating
        Try
            If cmbitemname.Text.Trim <> "" Then itemvalidate(cmbitemname, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validated(sender As Object, e As EventArgs) Handles cmbitemname.Validated
        If cmbitemname.Text.Trim = "" Then cmdok.Focus()
    End Sub

    Private Sub CMBDESIGN_Validating(sender As Object, e As CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBSHADE.Validating
        Try
            If CMBSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBSHADE, e, Me, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor

            GRIDSMP.RowCount = 0
LINE1:
            TEMPNO = Val(TXTNO.Text) - 1
            If TEMPNO > 0 Then
                EDIT = True
                SampleBookletCreation_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDSMP.RowCount = 0 And TEMPNO > 0 Then
                TXTNO.Text = TEMPNO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            GRIDSMP.RowCount = 0
LINE1:
            TEMPNO = Val(TXTNO.Text) + 1
            GETMAXNO()
            Dim MAXNO As Integer
            MAXNO = TXTNO.Text.Trim
            clear()
            If Val(TXTNO.Text) - 1 >= TEMPNO Then
                EDIT = True
                SampleBookletCreation_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDSMP.RowCount = 0 And TEMPNO < MAXNO Then
                TXTNO.Text = TEMPNO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            cmdok_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(sender As Object, e As EventArgs) Handles tooldelete.Click
        Try
            cmddelete_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then


                If MsgBox("Delete Sample barcode ?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPNO)
                    alParaval.Add(YearId)


                    Dim OBJISSUE As New ClsSampleBookletCreation
                    ALPARAVAL.Add(TEMPNO)
                    ALPARAVAL.Add(YearId)
                    OBJISSUE.alParaval = ALPARAVAL
                    Dim INTRESULT As Integer = OBJISSUE.Delete()
                    MsgBox("Sample Booklet Deleted")
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
            clear()
            cmbitemname.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNARRATION_Validated(sender As Object, e As EventArgs) Handles TXTNARRATION.Validated
        Try
            If CMBSAMPLETYPE.Text.Trim <> "" And cmbitemname.Text.Trim <> "" And Val(TXTNOOFBOOKLET.Text.Trim) > 0 Then
                FILLGRID()
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim obj As New SampleBookletCreationDetails
            obj.MdiParent = MDIMain
            obj.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Try
            cmdok_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub total()
        Try
            LBLTOTAL.Text = 0
            For Each dr As DataGridViewRow In GRIDSMP.Rows
                If dr.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTAL.Text = Format(Val(LBLTOTAL.Text) + Val(dr.Cells(GNOOFBOOKLET.Index).EditedFormattedValue), "0")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridsample_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDSMP.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSMP.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDSMP.Rows.RemoveAt(GRIDSMP.CurrentRow.Index)
                getsrno(GRIDSMP)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(sender As Object, e As CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDSMP.RowCount = 0
                TEMPNO = Val(tstxtbillno.Text)
                If TEMPNO > 0 Then
                    EDIT = True
                    SampleBookletCreation_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(sender As Object, e As EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNOOFBOOKLET_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTNOOFBOOKLET.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub CMBSHADE_Enter(sender As Object, e As EventArgs) Handles CMBSHADE.Enter
        Try
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 And CMBSAMPLETYPE.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                'no need for yearid clause here as we need to fetch this barcode in all acccouting year
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR ", "", "   SAMPLEBARCODE INNER JOIN ITEMMASTER ON SAMPLEBARCODE.SB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON SAMPLEBARCODE.SB_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON SAMPLEBARCODE.SB_DESIGNID = DESIGNMASTER.DESIGN_id ", " AND SB_BARCODE = '" & TXTBARCODE.Text.Trim & "'")
                If DT.Rows.Count > 0 Then
                    GRIDSMP.Rows.Add(GRIDSMP.RowCount + 1, CMBSAMPLETYPE.Text.Trim, DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("DESIGN"), DT.Rows(0).Item("COLOR"), 1, "")
                    GRIDSMP.FirstDisplayedScrollingRowIndex = GRIDSMP.RowCount - 1

                    TXTBARCODE.Clear()
                    TXTBARCODE.Focus()
                Else
                    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                    TXTBARCODE.Clear()
                End If
            Else
                If CMBSAMPLETYPE.Text.Trim = "" And TXTBARCODE.Text.Trim <> "" Then MsgBox("Enter Sample Type", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Enter(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillname(CMBPARTYNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then namevalidate(CMBPARTYNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' ", "SUNDRY DEBTORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(sender As Object, e As EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(sender As Object, e As CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SMDATE_Validating(sender As Object, e As CancelEventArgs) Handles SMDATE.Validating
        Try
            If SMDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(SMDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    If Not datecheck(SMDATE.Text) Then
                        MsgBox("Date not in Accounting Year")
                        e.Cancel = True

                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SMDATE_GotFocus(sender As Object, e As EventArgs) Handles SMDATE.GotFocus
        SMDATE.SelectionStart = 0
    End Sub

    Private Sub CMBSAMPLETYPE_Enter(sender As Object, e As EventArgs) Handles CMBSAMPLETYPE.Enter
        Try
            If CMBSAMPLETYPE.Text.Trim = "" Then FILLSAMPLETYPE(CMBSAMPLETYPE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSAMPLETYPE_Validating(sender As Object, e As CancelEventArgs) Handles CMBSAMPLETYPE.Validating
        Try
            If CMBSAMPLETYPE.Text.Trim <> "" Then SAMPLETYPEVALIDATE(CMBSAMPLETYPE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class