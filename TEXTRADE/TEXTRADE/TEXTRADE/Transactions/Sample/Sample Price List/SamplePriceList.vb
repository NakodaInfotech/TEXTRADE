
Imports System.ComponentModel
Imports BL

Public Class SamplePriceList

    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPSPLNO As String
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdEXIT_Click(sender As Object, e As EventArgs) Handles cmdEXIT.Click
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If
            Dim OBJSPL As New ClsSamplePriceList()

            OBJSPL.ALPARAVAL.Add(Format(Convert.ToDateTime(SPLDATE.Text).Date, "MM/dd/yyyy"))
            OBJSPL.ALPARAVAL.Add(CMBPARTYNAME.Text.Trim)
            OBJSPL.ALPARAVAL.Add(TXTMODEOFSHIPMENT.Text.Trim)
            OBJSPL.ALPARAVAL.Add(Val(LBLTOTALMTRS.Text.Trim))
            OBJSPL.ALPARAVAL.Add(Val(LBLTOTALAMT.Text.Trim))
            OBJSPL.ALPARAVAL.Add(txtremarks.Text.Trim)
            OBJSPL.ALPARAVAL.Add(txtrefno.Text.Trim)

            OBJSPL.ALPARAVAL.Add(CmpId)
            OBJSPL.ALPARAVAL.Add(Userid)
            OBJSPL.ALPARAVAL.Add(YearId)

            Dim GRIDSRNO As String = ""
            Dim GRIDSAMPLETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim RATE As String = ""
            Dim MTRS As String = ""
            Dim AMOUNT As String = ""
            Dim NARRATION As String = ""
            Dim NOOFBOOKLET As String = ""



            For Each ROW As DataGridViewRow In GRIDSPL.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(ROW.Cells(gsrno.Index).Value)

                        GRIDSAMPLETYPE = ROW.Cells(GSAMPLETYPE.Index).Value
                        ITEMNAME = ROW.Cells(gitemname.Index).Value
                        QUALITY = ROW.Cells(GQUALITY.Index).Value
                        DESIGN = ROW.Cells(GDESIGN.Index).Value
                        COLOR = ROW.Cells(GCOLOR.Index).Value
                        RATE = Val(ROW.Cells(GRATE.Index).Value)
                        MTRS = Val(ROW.Cells(GMTRS.Index).Value)
                        AMOUNT = Val(ROW.Cells(GAMOUNT.Index).Value)
                        NARRATION = ROW.Cells(GNARRATION.Index).Value
                        NOOFBOOKLET = Val(ROW.Cells(GNOOFBOOKLET.Index).Value)
                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(ROW.Cells(gsrno.Index).Value)
                        GRIDSAMPLETYPE = GRIDSAMPLETYPE & "|" & ROW.Cells(GSAMPLETYPE.Index).Value
                        ITEMNAME = ITEMNAME & "|" & ROW.Cells(gitemname.Index).Value
                        QUALITY = QUALITY & "|" & ROW.Cells(GQUALITY.Index).Value
                        DESIGN = DESIGN & "|" & ROW.Cells(GDESIGN.Index).Value
                        COLOR = COLOR & "|" & ROW.Cells(GCOLOR.Index).Value
                        RATE = RATE & "|" & Val(ROW.Cells(GRATE.Index).Value)
                        MTRS = MTRS & "|" & Val(ROW.Cells(GMTRS.Index).Value)
                        AMOUNT = AMOUNT & "|" & Val(ROW.Cells(GAMOUNT.Index).Value)
                        NARRATION = NARRATION & "|" & ROW.Cells(GNARRATION.Index).Value
                        NOOFBOOKLET = NOOFBOOKLET & "|" & Val(ROW.Cells(GNOOFBOOKLET.Index).Value)

                    End If
                End If
            Next

            OBJSPL.ALPARAVAL.Add(GRIDSRNO)
            OBJSPL.ALPARAVAL.Add(GRIDSAMPLETYPE)
            OBJSPL.ALPARAVAL.Add(ITEMNAME)
            OBJSPL.ALPARAVAL.Add(QUALITY)
            OBJSPL.ALPARAVAL.Add(DESIGN)
            OBJSPL.ALPARAVAL.Add(COLOR)
            OBJSPL.ALPARAVAL.Add(RATE)
            OBJSPL.ALPARAVAL.Add(MTRS)
            OBJSPL.ALPARAVAL.Add(AMOUNT)
            OBJSPL.ALPARAVAL.Add(NARRATION)
            OBJSPL.ALPARAVAL.Add(NOOFBOOKLET)
            OBJSPL.ALPARAVAL.Add(Val(TOTALNOOFBOOKLET.Text.Trim))

            OBJSPL.ALPARAVAL.Add(CMBAGENT.Text.Trim)
            OBJSPL.ALPARAVAL.Add(CMBGODOWN.Text.Trim)


            Dim ORDERGRIDSRNO As String = ""
            Dim ORDERITEMNAME As String = ""
            Dim ORDERDESIGNNO As String = ""
            Dim ORDERCOLOR As String = ""
            Dim ORDERBOOKLET As String = ""
            Dim ORDERFROMNO As String = ""
            Dim ORDERFROMSRNO As String = ""
            Dim ORDERFROMTYPE As String = ""
            Dim ORDEROUTBOOKLET As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDORDER.Rows
                If row.Cells(0).Value <> Nothing AndAlso Val(row.Cells(OOUTBOOKLET.Index).Value) > 0 Then

                    If ORDERGRIDSRNO = "" Then
                        ORDERGRIDSRNO = Val(row.Cells(OSRNO.Index).Value)
                        ORDERITEMNAME = row.Cells(OITEMNAME.Index).Value.ToString
                        ORDERDESIGNNO = row.Cells(ODESIGNNO.Index).Value.ToString
                        ORDERCOLOR = row.Cells(OCOLOR.Index).Value.ToString
                        ORDERBOOKLET = Val(row.Cells(OBOOKLET.Index).Value)
                        ORDERFROMNO = Val(row.Cells(OFROMNO.Index).Value)
                        ORDERFROMSRNO = Val(row.Cells(OFROMSRNO.Index).Value)
                        ORDERFROMTYPE = row.Cells(OFROMTYPE.Index).Value.ToString
                        ORDEROUTBOOKLET = Val(row.Cells(OOUTBOOKLET.Index).Value)
                    Else
                        ORDERGRIDSRNO = ORDERGRIDSRNO & "|" & Val(row.Cells(OSRNO.Index).Value)
                        ORDERITEMNAME = ORDERITEMNAME & "|" & row.Cells(OITEMNAME.Index).Value.ToString
                        ORDERDESIGNNO = ORDERDESIGNNO & "|" & row.Cells(ODESIGNNO.Index).Value.ToString
                        ORDERCOLOR = ORDERCOLOR & "|" & row.Cells(OCOLOR.Index).Value.ToString
                        ORDERBOOKLET = ORDERBOOKLET & "|" & Val(row.Cells(OBOOKLET.Index).Value)
                        ORDERFROMNO = ORDERFROMNO & "|" & Val(row.Cells(OFROMNO.Index).Value)
                        ORDERFROMSRNO = ORDERFROMSRNO & "|" & Val(row.Cells(OFROMSRNO.Index).Value)
                        ORDERFROMTYPE = ORDERFROMTYPE & "|" & row.Cells(OFROMTYPE.Index).Value.ToString
                        ORDEROUTBOOKLET = ORDEROUTBOOKLET & "|" & Val(row.Cells(OOUTBOOKLET.Index).Value)
                    End If
                End If
            Next

            OBJSPL.ALPARAVAL.Add(ORDERGRIDSRNO)
            OBJSPL.ALPARAVAL.Add(ORDERITEMNAME)
            OBJSPL.ALPARAVAL.Add(ORDERDESIGNNO)
            OBJSPL.ALPARAVAL.Add(ORDERCOLOR)
            OBJSPL.ALPARAVAL.Add(ORDERBOOKLET)
            OBJSPL.ALPARAVAL.Add(ORDERFROMNO)
            OBJSPL.ALPARAVAL.Add(ORDERFROMSRNO)
            OBJSPL.ALPARAVAL.Add(ORDERFROMTYPE)
            OBJSPL.ALPARAVAL.Add(ORDEROUTBOOKLET)


            If EDIT = False Then

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = OBJSPL.SAVE()
                MessageBox.Show("Details Added")
                TXTSPLNO.Text = DT.Rows(0).Item(0)
            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                OBJSPL.ALPARAVAL.Add(TEMPSPLNO)
                Dim IntResult As Integer = OBJSPL.UPDATE()
                MessageBox.Show("Details Updated")
            End If

            PRINTREPORT()

            EDIT = False
            CLEAR()
            CMBPARTYNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True
        If CMBPARTYNAME.Text.Trim = "" Then
            EP.SetError(CMBPARTYNAME, " Please Fill Party Name ")
            bln = False
        End If
        If CMBGODOWN.Text.Trim = "" Then
            EP.SetError(CMBGODOWN, " Please Fill GODOWN ")
            bln = False
        End If
        If GRIDSPL.RowCount = 0 Then
            EP.SetError(TXTNARRATION, " Please Enter Data in grid")
            bln = False
        End If

        If SPLDATE.Text = "__/__/____" Then
            EP.SetError(SPLDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(SPLDATE.Text) Then
                EP.SetError(SPLDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        'FOR ORDER CHECKING, FIRST REMOVE GDNQTY
        Dim TEMPORDERROWNO As Integer = -1
        Dim TEMPORDERMATCH As Boolean = False
        If GRIDORDER.RowCount > 0 Then

            For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                ORDROW.Cells(OOUTBOOKLET.Index).Value = 0
            Next

            For Each ROW As DataGridViewRow In GRIDSPL.Rows
                For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                    If ROW.Cells(gitemname.Index).Value = ORDROW.Cells(OITEMNAME.Index).Value And ROW.Cells(GDESIGN.Index).Value = ORDROW.Cells(ODESIGNNO.Index).Value And ROW.Cells(GCOLOR.Index).Value = ORDROW.Cells(OCOLOR.Index).Value Then
                        TEMPORDERMATCH = True
                        'IF ITEM IS MATCHED BUT THE QTY IS FULL THEN WE NEED TO KEEP THIS ROWNO IN TEMP AND NEED TO CHECK FURTHER ALSO
                        'IF WE GET ANY NEW MATHING THEN WE NEED TO INSERT THERE
                        'IF NO MATCHING IS FOUND IN FURTHER ROWS THEN WE NEED TO ADD QTY IN THIS TEMPROW
                        If Val(ORDROW.Cells(OOUTBOOKLET.Index).Value) >= Val(ORDROW.Cells(OBOOKLET.Index).Value) Then
                            TEMPORDERROWNO = ORDROW.Index
                            GoTo CHECKNEXTLINE
                        End If
                        ORDROW.Cells(OOUTBOOKLET.Index).Value = Val(ORDROW.Cells(OOUTBOOKLET.Index).Value) + Val(ROW.Cells(GNOOFBOOKLET.Index).Value)
                        TEMPORDERROWNO = -1
                        Exit For
CHECKNEXTLINE:
                    End If
                Next
                'IF NO FURTHER MACHING IS FOUND BUT WE HAVE TEMPORDERROWNO THEN ADD VALUE IN THAT ROW
                If TEMPORDERROWNO >= 0 Then
                    GRIDORDER.Rows(TEMPORDERROWNO).Cells(OOUTBOOKLET.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OOUTBOOKLET.Index).Value) + Val(ROW.Cells(GNOOFBOOKLET.Index).Value)
                    TEMPORDERROWNO = -1
                End If
                If TEMPORDERMATCH = False Then
                    ROW.DefaultCellStyle.BackColor = Color.LightGreen
                    If MsgBox("There are Items which are not Present in Selected Order, Wish to Proceed", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        EP.SetError(CMBPARTYNAME, "There are Items which are not Present in Selected Order")
                        bln = False
                    End If
                End If
                TEMPORDERMATCH = False
            Next
        End If



        Return bln
    End Function

    Sub fillgrid()

        If GRIDDOUBLECLICK = False Then
            GRIDSPL.Rows.Add(Val(TXTSRNO.Text.Trim), CMBSAMPLETYPE.Text.Trim, CMBITEMNAME.Text.Trim, CMBQUALITY.Text.Trim, CMBDESIGN.Text.Trim, CMBCOLOR.Text.Trim, Val(TXTNOOFBOOKLET.Text.Trim), Val(TXTRATE.Text.Trim), Val(TXTMTRS.Text.Trim), Val(TXTAMOUNT.Text.Trim), TXTNARRATION.Text.Trim)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDSPL.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDSPL.Item(GSAMPLETYPE.Index, TEMPROW).Value = CMBSAMPLETYPE.Text.Trim
            GRIDSPL.Item(gitemname.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
            GRIDSPL.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDSPL.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDSPL.Item(GCOLOR.Index, TEMPROW).Value = CMBCOLOR.Text.Trim
            GRIDSPL.Item(GNOOFBOOKLET.Index, TEMPROW).Value = Val(TXTNOOFBOOKLET.Text.Trim)
            GRIDSPL.Item(GRATE.Index, TEMPROW).Value = Val(TXTRATE.Text.Trim)
            GRIDSPL.Item(GMTRS.Index, TEMPROW).Value = Val(TXTMTRS.Text.Trim)
            GRIDSPL.Item(GAMOUNT.Index, TEMPROW).Value = Val(TXTAMOUNT.Text.Trim)
            GRIDSPL.Item(GNARRATION.Index, TEMPROW).Value = TXTNARRATION.Text.Trim
            GRIDDOUBLECLICK = False
        End If

        GRIDSPL.FirstDisplayedScrollingRowIndex = GRIDSPL.RowCount - 1

        TXTSRNO.Text = GRIDSPL.RowCount + 1
        CMBSAMPLETYPE.Text = ""
        If ClientName <> "YASHVI" Then
            CMBITEMNAME.Text = ""
            TXTNOOFBOOKLET.Clear()
        End If
        CMBQUALITY.Text = ""
        CMBDESIGN.Text = ""
        CMBCOLOR.Text = ""
        TXTBARCODE.Clear()
        TXTRATE.Clear()
        TXTMTRS.Clear()
        TXTAMOUNT.Clear()
        TXTNARRATION.Clear()

        CMBSAMPLETYPE.Focus()
        TOTAL()

    End Sub

    Sub TOTAL()
        Try
            TOTALNOOFBOOKLET.Text = 0

            For Each ROW As DataGridViewRow In GRIDSPL.Rows
                TOTALNOOFBOOKLET.Text = Format(Val(TOTALNOOFBOOKLET.Text.Trim) + Val(ROW.Cells(GNOOFBOOKLET.Index).Value), "0")
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            CMBPARTYNAME.Text = ""
            SPLDATE.Text = Now.Date
            TXTMODEOFSHIPMENT.Clear()
            TXTBARCODE.Clear()
            txtremarks.Clear()
            txtrefno.Clear()
            CMBITEMNAME.Text = ""
            CMBQUALITY.Text = ""
            CMBDESIGN.Text = ""
            CMBCOLOR.Text = ""
            TXTNOOFBOOKLET.Clear()
            TXTRATE.Clear()
            TXTMTRS.Clear()
            TXTAMOUNT.Clear()
            TXTNARRATION.Clear()
            GRIDSPL.RowCount = 0

            LBLTOTALMTRS.Text = 0.0
            LBLTOTALAMT.Text = 0.0
            txtbillno.Clear()
            EP.Clear()
            GETMAX_SPLNO()
            TOTALNOOFBOOKLET.Text = 0.0
            TXTSRNO.Text = 1
            CMBAGENT.Text = ""
            GRIDORDER.RowCount = 0
            If USERGODOWN <> "" Then CMBGODOWN.Text = USERGODOWN Else CMBGODOWN.Text = ""
            If ClientName = "MAHAVIRPOLYCOT" Then CMBSAMPLETYPE.Text = "BOOKLET" Else CMBSAMPLETYPE.Text = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_SPLNO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(SPL_no),0) + 1 ", "SAMPLEPRICELIST", " AND SPL_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSPLNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub SamplePricelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SAMPLE MODULE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            fillcmb()
            CLEAR()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objclsSMP As New ClsSamplePriceList()
                Dim DTTABLE As DataTable = objclsSMP.selectSMP(TEMPSPLNO, CmpId, Locationid, YearId)

                If DTTABLE.Rows.Count > 0 Then
                    For Each dr As DataRow In DTTABLE.Rows
                        TXTSPLNO.Text = TEMPSPLNO
                        SPLDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")
                        CMBPARTYNAME.Text = Convert.ToString(dr("NAME"))
                        TXTMODEOFSHIPMENT.Text = Convert.ToString(dr("MODE"))
                        txtremarks.Text = Convert.ToString(dr("REMARKS"))
                        txtrefno.Text = Convert.ToString(dr("REFNO"))
                        TOTALNOOFBOOKLET.Text = dr("TOTALNOOFBOOKLET")

                        GRIDSPL.Rows.Add(Val(dr("GRIDSRNO")), dr("SAMPLETYPE"), dr("ITEMNAME"), dr("QUALITYNAME"), dr("DESIGN").ToString, dr("COLOR").ToString, Val(dr("NOOFBOOKLET")), Val(dr("RATE")), Val(dr("MTRS")), Val(dr("AMOUNT")), dr("NARRATION").ToString)
                        CMBGODOWN.Text = dr("GODOWN")
                    Next


                    'ORDER GRID
                    'Dim OBJCMN As New ClsCommon
                    Dim OBJCMN As New ClsCommon
                    DTTABLE = OBJCMN.SEARCH(" SAMPLEPRICELIST_ORDERDETAILS.SPL_GRIDSRNO AS GRIDSRNO, ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR, SAMPLEPRICELIST_ORDERDETAILS.SPL_ORDERBOOKLET AS ORDERBOOKLET, SAMPLEPRICELIST_ORDERDETAILS.SPL_FROMNO AS FROMNO, SAMPLEPRICELIST_ORDERDETAILS.SPL_FROMSRNO AS FROMSRNO, SAMPLEPRICELIST_ORDERDETAILS.SPL_FROMTYPE AS FROMTYPE, SAMPLEPRICELIST_ORDERDETAILS.SPL_BOOKLET AS BOOKLET ", "", " SAMPLEPRICELIST_ORDERDETAILS INNER JOIN  ITEMMASTER ON SAMPLEPRICELIST_ORDERDETAILS.SPL_ITEMID = ITEMMASTER.ITEM_id LEFT OUTER JOIN DESIGNMASTER ON SAMPLEPRICELIST_ORDERDETAILS.SPL_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON SAMPLEPRICELIST_ORDERDETAILS.SPL_COLORID = COLORMASTER.COLOR_id", " AND SAMPLEPRICELIST_ORDERDETAILS.SPL_NO = " & TEMPSPLNO & " AND SAMPLEPRICELIST_ORDERDETAILS.SPL_YEARID = " & YearId)
                    If DTTABLE.Rows.Count > 0 Then
                        For Each DTR As DataRow In DTTABLE.Rows
                            GRIDORDER.Rows.Add(Val(DTR("GRIDSRNO")), DTR("ITEMNAME"), DTR("DESIGNNO"), DTR("COLOR"), Val(DTR("ORDERBOOKLET")), Val(DTR("FROMNO")), Val(DTR("FROMSRNO")), DTR("FROMTYPE"), Val(DTR("BOOKLET")))
                        Next
                    End If
                    getsrno(GRIDORDER)

                    GRIDSPL.FirstDisplayedScrollingRowIndex = GRIDSPL.RowCount - 1
                    TOTAL()
                Else
                    EDIT = False
                    CLEAR()
                End If
            End If

            TXTSRNO.Text = Val(GRIDSPL.RowCount) + 1

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBPARTYNAME.Text.Trim = "" Then FILLNAME(CMBPARTYNAME, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' and LEDGERS.ACC_TYPE = 'AGENT'))")
            fillQUALITY(CMBQUALITY, EDIT)
            FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
            FILLCOLOR(CMBCOLOR, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
            FILLSAMPLETYPE(CMBSAMPLETYPE)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBPARTYNAME.Focus()
    End Sub

    Private Sub SamplePricelist_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdOK_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1 Then
                TabControl1.SelectedIndex = 0
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2 Then
                TabControl1.SelectedIndex = 1
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                txtbillno.Focus()
                txtbillno.SelectAll()
            ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'for grid foucs
                GRIDSPL.Focus()
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                ToolPREVIOUS_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                Toolnext_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)
            End If
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

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete Sample barcode ?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPSPLNO)
                    alParaval.Add(YearId)

                    Dim clspo As New ClsSamplePriceList()
                    clspo.ALPARAVAL = alParaval
                    Dim IntResult As Integer = clspo.Delete()
                    MsgBox("sample barcode Deleted")
                    CLEAR()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDSPL.CurrentRow.Index >= 0 And GRIDSPL.Item(gsrno.Index, GRIDSPL.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TEMPROW = GRIDSPL.CurrentRow.Index
                TXTSRNO.Text = GRIDSPL.Item(gsrno.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                CMBSAMPLETYPE.Text = GRIDSPL.Item(GSAMPLETYPE.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Text = GRIDSPL.Item(gitemname.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDSPL.Item(GQUALITY.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDSPL.Item(GDESIGN.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                CMBCOLOR.Text = GRIDSPL.Item(GCOLOR.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                TXTNOOFBOOKLET.Text = GRIDSPL.Item(GNOOFBOOKLET.Index, GRIDSPL.CurrentRow.Index).Value
                TXTRATE.Text = Val(GRIDSPL.Item(GRATE.Index, GRIDSPL.CurrentRow.Index).Value)
                TXTMTRS.Text = Val(GRIDSPL.Item(GMTRS.Index, GRIDSPL.CurrentRow.Index).Value)
                TXTAMOUNT.Text = Val(GRIDSPL.Item(GAMOUNT.Index, GRIDSPL.CurrentRow.Index).Value)
                TXTNARRATION.Text = GRIDSPL.Item(GNARRATION.Index, GRIDSPL.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSPL_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDSPL.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSPL.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDSPL.Rows.RemoveAt(GRIDSPL.CurrentRow.Index)
                getsrno(GRIDSPL)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If


            Dim objpodtls As New SamplePriceListDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdOK_Click(sender, e)
    End Sub

    Private Sub ToolPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolPREVIOUS.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            GRIDSPL.RowCount = 0
LINE1:
            TEMPSPLNO = Val(TXTSPLNO.Text) - 1
            If TEMPSPLNO > 0 Then
                EDIT = True
                SamplePricelist_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDSPL.RowCount = 0 And TEMPSPLNO > 1 Then
                TXTSPLNO.Text = TEMPSPLNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            GRIDSPL.RowCount = 0
LINE1:
            TEMPSPLNO = Val(TXTSPLNO.Text) + 1
            GETMAX_SPLNO()
            Dim MAXNO As Integer = TXTSPLNO.Text.Trim
            CLEAR()
            If Val(TXTSPLNO.Text) - 1 >= TEMPSPLNO Then
                EDIT = True
                SamplePricelist_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDSPL.RowCount = 0 And TEMPSPLNO < MAXNO Then
                TXTSPLNO.Text = TEMPSPLNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If MsgBox("Wish to Print Price List?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub
            Dim OBJSMP As New SampleOrderDesign
            OBJSMP.MdiParent = MDIMain
            OBJSMP.FRMSTRING = "SAMPLEPRICELIST"
            OBJSMP.FORMULA = "{SAMPLEPRICELIST.SPL_no}=" & Val(TXTSPLNO.Text.Trim) & " and {SAMPLEPRICELIST.SPL_yearid}=" & YearId
            OBJSMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then itemvalidate(CMBITEMNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDESIGN.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJD As New SelectDesign
                OBJD.ShowDialog()
                If OBJD.TEMPNAME <> "" Then CMBDESIGN.Text = OBJD.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCOLOR.Enter
        Try
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCOLOR.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCOLOR As New SelectShade
                OBJCOLOR.ShowDialog()
                If OBJCOLOR.TEMPNAME <> "" Then CMBCOLOR.Text = OBJCOLOR.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCOLOR.Validating
        Try
            If CMBCOLOR.Text.Trim <> "" Then COLORVALIDATE(CMBCOLOR, e, Me, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SPLDATE_Validating(sender As Object, e As CancelEventArgs) Handles SPLDATE.Validating
        Try
            If SPLDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(SPLDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    If Not datecheck(SPLDATE.Text) Then
                        MsgBox("Date not in Accounting Year")
                        e.Cancel = True

                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then
                Dim WHERECLAUSE As String = ""
                If ClientName <> "KOTHARI" Then WHERECLAUSE = " and LEDGERS.ACC_TYPE = 'AGENT'"
                NAMEVALIDATE(CMBPARTYNAME, CMBCODE, e, Me, TXTADD, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' " & WHERECLAUSE & ")) ", "SUNDRY DEBTORS", "ACCOUNTS")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSPL_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSPL.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRATE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTRATE.KeyPress, TXTMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTRATE_Validated(sender As Object, e As EventArgs) Handles TXTRATE.Validated, TXTMTRS.Validated
        TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(TXTMTRS.Text.Trim), "0.00")
    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 And CMBSAMPLETYPE.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                'no need for yearid clause here as we need to fetch this barcode in all acccouting year
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITYNAME, ISNULL(ITEMMASTER.ITEM_RATE,0) AS ITEMRATE ", "", "  SAMPLEBARCODE INNER JOIN ITEMMASTER ON SAMPLEBARCODE.SB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN QUALITYMASTER ON SAMPLEBARCODE.SB_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON SAMPLEBARCODE.SB_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON SAMPLEBARCODE.SB_DESIGNID = DESIGNMASTER.DESIGN_id ", " AND SB_BARCODE = '" & TXTBARCODE.Text.Trim & "'")
                If DT.Rows.Count > 0 Then

                    Dim RATE As Double = Val(DT.Rows(0).Item("ITEMRATE"))

                    'GET RATE
                    Dim WHERECLAUSE As String = ""
                    If DT.Rows(0).Item("DESIGN") <> "" Then WHERECLAUSE = WHERECLAUSE & " And ISNULL(DESIGNMASTER.DESIGN_NO,'') = '" & DT.Rows(0).Item("DESIGN") & "'"
                    If DT.Rows(0).Item("COLOR") <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(COLORMASTER.COLOR_NAME,'') = '" & DT.Rows(0).Item("COLOR") & "'"
                    Dim DTRATE As New DataTable

                    If ClientName = "YASHVI" Then

                        'FOR THIS WE NEED TO GET THE COLUMN NAME FROM RATETYPEMASREER 
                        If CMBPARTYNAME.Text.Trim <> "" Then
                            Dim DTRATETYPE As DataTable = OBJCMN.SEARCH(" ISNULL(LEDGERS.ACC_PRICELISTCOLUMN, '') AS COLNAME", "", " LEDGERS ", " AND ledgers.acc_cmpname = '" & CMBPARTYNAME.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                            If DTRATETYPE.Rows.Count > 0 AndAlso DTRATETYPE.Rows(0).Item("COLNAME") <> "" Then
                                DTRATE = OBJCMN.SEARCH(DTRATETYPE.Rows(0).Item("COLNAME") & " AS SALERATE", "", "ITEMPRICELIST INNER JOIN ITEMMASTER ON ITEMPRICELIST.ITEMID = ITEMMASTER.item_id ", " AND ITEMMASTER.ITEM_NAME = '" & DT.Rows(0).Item("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                            End If
                        End If

                    Else
                        DTRATE = OBJCMN.SEARCH("PRICELISTMASTER.PL_RATE AS SALERATE ", "", "PRICELISTMASTER INNER JOIN ITEMMASTER ON PRICELISTMASTER.PL_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON PRICELISTMASTER.PL_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON PRICELISTMASTER.PL_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON PRICELISTMASTER.PL_QUALITYID = QUALITYMASTER.QUALITY_id ", " AND ISNULL(ITEMMASTER.ITEM_NAME,'') = '" & DT.Rows(0).Item("ITEMNAME") & "'" & WHERECLAUSE & " AND PL_YEARID = " & YearId)
                    End If

                    If DTRATE.Rows.Count > 0 Then RATE = Val(DTRATE.Rows(0).Item("SALERATE"))

                    If ClientName = "KOTHARI" Then RATE = 0.0

                    GRIDSPL.Rows.Add(GRIDSPL.RowCount + 1, CMBSAMPLETYPE.Text.Trim, DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITYNAME"), DT.Rows(0).Item("DESIGN"), DT.Rows(0).Item("COLOR"), 1, Format(Val(RATE), "0.00"), 0, 0, "")
                    GRIDSPL.FirstDisplayedScrollingRowIndex = GRIDSPL.RowCount - 1

                    TXTBARCODE.Clear()
                    TXTBARCODE.Focus()
                    TOTAL()
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

    Private Sub CMBQUALITY_Enter(sender As Object, e As EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtbillno_Validating(sender As Object, e As CancelEventArgs) Handles txtbillno.Validating
        Try
            If Val(txtbillno.Text.Trim) > 0 Then
                GRIDSPL.RowCount = 0
                TEMPSPLNO = Val(txtbillno.Text)
                If TEMPSPLNO > 0 Then
                    EDIT = True
                    SamplePricelist_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNOOFBOOKLET_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTNOOFBOOKLET.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub CMBPARTYNAME_Validated(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Validated
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENT ", "", " LEDGERS LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.Acc_id  ", " and LEDGERS.acc_cmpname = '" & CMBPARTYNAME.Text.Trim & "' and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    If CMBAGENT.Text.Trim = "" Then CMBAGENT.Text = DT.Rows(0).Item("AGENT")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(sender As Object, e As EventArgs) Handles CMBAGENT.Enter
        If CMBAGENT.Text.Trim = "" Then fillledger(CMBAGENT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
    End Sub

    Private Sub CMBAGENT_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBAGENT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBAGENT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Validating(sender As Object, e As CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then namevalidate(CMBAGENT, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Enter(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then
                Dim WHERECLAUSE As String = ""
                If ClientName <> "KOTHARI" Then WHERECLAUSE = " and LEDGERS.ACC_TYPE = 'AGENT'"
                FILLNAME(CMBPARTYNAME, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' " & WHERECLAUSE & "))")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSMP_Click(sender As Object, e As EventArgs) Handles CMDSELECTSMP.Click
        Try

            If CMBPARTYNAME.Text.Trim = "" Then
                MsgBox("Select Party Name", MsgBoxStyle.Critical)
                CMBPARTYNAME.Focus()
                Exit Sub
            End If


            Dim DTPO As New DataTable
            Dim OBJSMP As New SelectSampleOrder
            OBJSMP.PARTYNAME = CMBPARTYNAME.Text.Trim
            OBJSMP.WHERECLAUSE = " And SELFORDER = 'FALSE'"
            OBJSMP.ShowDialog()
            DTPO = OBJSMP.DT
            If DTPO.Rows.Count > 0 Then

                'BEFORE ADDING THE ROW IN ORDERDER GRID CHECK WHETHER SAME ORDERNO AN SRNO IS PRESENT IN GRID OR NOT
                For Each DTROW As DataRow In DTPO.Rows
                    For Each ROW As DataGridViewRow In GRIDORDER.Rows
                        If Val(ROW.Cells(OFROMNO.Index).Value) = Val(DTROW("SRNO")) And Val(ROW.Cells(OFROMSRNO.Index).Value) = Val(DTROW("GRIDSRNO")) And ROW.Cells(OFROMTYPE.Index).Value = DTROW("TYPE") Then GoTo NEXTLINE
                    Next

                    GRIDORDER.Rows.Add(0, DTROW("ITEMNAME"), DTROW("DESIGNNO"), DTROW("COLOR"), Val(DTROW("QTY")), DTROW("SRNO"), DTROW("GRIDSRNO"), DTROW("TYPE"), 0)


                    'DONT ADD IN ITEM GRID.. IF USER WANTS THEN THEY WILL ADD MANUALLY FROM BARCODE SCANNING OR MANUAL ENTRY
                    ''FILL SAME DATA IN GRNGRID
                    'CMBITEMNAME.Text = DTROW("ITEMNAME")
                    'CMBDESIGN.Text = DTROW("DESIGNNO")
                    'CMBCOLOR.Text = DTROW("COLOR")
                    'CMBITEMNAME_Validated(sender, e)
                    'TXTNOOFBOOKLET.Text = 1
                    'TXTNARRATION_Validated(sender, e)

NEXTLINE:
                Next
                getsrno(GRIDORDER)

            End If

            getsrno(GRIDSPL)
            CMDSELECTSMP.Enabled = True
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validated(sender As Object, e As EventArgs) Handles CMBITEMNAME.Validated
        Try
            Dim OBJCMN As New ClsCommon
            Dim DTRATE As New DataTable
            If ClientName = "MAHAVIRPOLYCOT" Then
                DTRATE = OBJCMN.SEARCH("ISNULL(ITEMMASTER.ITEM_RATE, 0) AS RATE ", "", "  ITEMMASTER", "AND ITEMMASTER.item_name = '" & CMBITEMNAME.Text.Trim & "' AND ITEMMASTER.item_yearid = " & YearId)
            ElseIf ClientName = "YASHVI" Then
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(LEDGERS.ACC_PRICELISTCOLUMN, '') AS COLNAME", "", " LEDGERS ", " AND ledgers.acc_cmpname = '" & CMBPARTYNAME.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("COLNAME") <> "" Then DTRATE = OBJCMN.SEARCH(DT.Rows(0).Item("COLNAME") & " AS RATE", "", "ITEMPRICELIST INNER JOIN ITEMMASTER ON ITEMPRICELIST.ITEMID = ITEMMASTER.item_id ", " AND ITEMMASTER.ITEM_NAME = '" & CMBITEMNAME.Text.Trim & "' AND ITEM_YEARID = " & YearId)
            End If
            If DTRATE.Rows.Count > 0 Then TXTRATE.Text = Val(DTRATE.Rows(0).Item("RATE"))
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

    Private Sub TXTNARRATION_Validated(sender As Object, e As EventArgs) Handles TXTNARRATION.Validated
        Try
            If CMBSAMPLETYPE.Text.Trim <> "" Then
                fillgrid()
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SPLDATE_GotFocus(sender As Object, e As EventArgs) Handles SPLDATE.GotFocus
        SPLDATE.SelectionStart = 0
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If EDIT = False Then Exit Sub
            SENDWHATSAPP(TEMPSPLNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(SPLNO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJSO As New SampleOrderDesign
            OBJSO.MdiParent = MDIMain
            OBJSO.DIRECTPRINT = True
            OBJSO.FRMSTRING = "SAMPLEPRICELIST"
            OBJSO.DIRECTMAIL = True
            OBJSO.PARTYNAME = CMBPARTYNAME.Text.Trim
            OBJSO.AGENTNAME = CMBAGENT.Text.Trim
            OBJSO.FORMULA = "{SAMPLEPRICELIST.SPL_NO}=" & Val(SPLNO) & " and {SAMPLEPRICELIST.SPL_yearid}=" & YearId
            OBJSO.TEMPNO = SPLNO
            OBJSO.NOOFCOPIES = 1
            OBJSO.Show()
            OBJSO.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBPARTYNAME.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBAGENT.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\SAMPLEPRICELIST_" & Val(SPLNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add("SAMPLEPRICELIST_" & Val(SPLNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
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

    Private Sub SamplePriceList_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "YASHVI" Then
                CMBQUALITY.TabStop = False
                CMBCOLOR.TabStop = False
                TXTMTRS.TabStop = False
                TXTAMOUNT.TabStop = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbillno.KeyPress, TXTCOPY.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTCOPY_Validated(sender As Object, e As EventArgs) Handles TXTCOPY.Validated
        Try
            If Val(TXTCOPY.Text.Trim) = 0 Or EDIT = True Then Exit Sub
            If MsgBox("Wish To Copy PL No" & Val(TXTCOPY.Text.Trim) & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim objclsSMP As New ClsSamplePriceList()
            Dim DTTABLE As DataTable = objclsSMP.selectSMP(Val(TXTCOPY.Text.Trim), CmpId, Locationid, YearId)

            If DTTABLE.Rows.Count > 0 Then
                For Each dr As DataRow In DTTABLE.Rows
                    CMBPARTYNAME.Text = Convert.ToString(dr("NAME"))
                    TXTMODEOFSHIPMENT.Text = Convert.ToString(dr("MODE"))
                    txtremarks.Text = Convert.ToString(dr("REMARKS"))
                    txtrefno.Text = Convert.ToString(dr("REFNO"))
                    TOTALNOOFBOOKLET.Text = dr("TOTALNOOFBOOKLET")

                    GRIDSPL.Rows.Add(Val(dr("GRIDSRNO")), dr("SAMPLETYPE"), dr("ITEMNAME"), dr("QUALITYNAME"), dr("DESIGN").ToString, dr("COLOR").ToString, Val(dr("NOOFBOOKLET")), Val(dr("RATE")), Val(dr("MTRS")), Val(dr("AMOUNT")), dr("NARRATION").ToString)
                    CMBGODOWN.Text = dr("GODOWN")
                Next

                GRIDSPL.FirstDisplayedScrollingRowIndex = GRIDSPL.RowCount - 1
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class