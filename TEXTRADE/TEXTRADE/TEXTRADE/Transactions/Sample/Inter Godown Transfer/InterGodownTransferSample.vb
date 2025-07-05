
Imports BL
Imports System.ComponentModel

Public Class InterGodownTransferSample

    Dim GRIDDOUBLECLICK As Boolean
    Public TEMPGODOWNNO As Integer          'used for editing
    Public EDIT As Boolean          'used for editing
    Dim TEMPROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub clear()

        EP.Clear()
        TXTGODOWNNO.Clear()
        CMBSAMPLETYPE.Text = ""
        TXTMODEOFSHIPMENT.Text = ""
        TXTADD.Clear()
        TXTDATE.Text = Now.Date
        tstxtbillno.Clear()
        txtremarks.Clear()

        TXTISSUEBY.Clear()
        TXTBARCODE.Clear()
        GRIDTRANSFER.RowCount = 0
        GRIDDOUBLECLICK = False
        getmaxno()
        txtsrno.Text = 1

        cmbitemname.Text = ""

        CMBDESIGN.Text = ""
        cmbcolor.Text = ""
        TXTNOOFBOOKLET.Clear()
        TXTTOTALBOOKLETS.Clear()
        GRIDORDER.RowCount = 0


    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False
        CMBFROMGODOWN.Focus()
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(TRANSFER_no),0) + 1 ", " INTERGODOWNTRANSFERSAMPLE ", " and  TRANSFER_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTGODOWNNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If CMBFROMGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBFROMGODOWN, " Please Fill Godown")
                bln = False
            End If

            If CMBTOGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBTOGODOWN, " Please Fill Godown")
                bln = False
            End If

            If CMBFROMGODOWN.Text.Trim = CMBTOGODOWN.Text.Trim Then
                EP.SetError(CMBFROMGODOWN, " From && To Godown cannot be same")
                bln = False
            End If


            If GRIDTRANSFER.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If

            If TXTDATE.Text = "__/__/____" Then
                EP.SetError(TXTDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(TXTDATE.Text) Then
                    EP.SetError(TXTDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If

            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(TXTDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBFROMGODOWN.Text.Trim)
            alParaval.Add(CMBTOGODOWN.Text.Trim)
            alParaval.Add(TXTMODEOFSHIPMENT.Text.Trim)
            alParaval.Add(TXTISSUEBY.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(Val(TXTTOTALBOOKLETS.Text.Trim))
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim GRIDSRNO As String = ""
            Dim GRIDSAMPLETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim NOOFBOOKLET As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDTRANSFER.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(row.Cells(GSRNO.Index).Value)
                        GRIDSAMPLETYPE = row.Cells(GSAMPLETYPE.Index).Value
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(GCOLOR.Index).Value.ToString
                        NOOFBOOKLET = Val(row.Cells(GNOOFBOOKLET.Index).Value)

                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(GSRNO.Index).Value)
                        GRIDSAMPLETYPE = GRIDSAMPLETYPE & "|" & row.Cells(GSAMPLETYPE.Index).Value
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(GCOLOR.Index).Value.ToString
                        NOOFBOOKLET = NOOFBOOKLET & "|" & Val(row.Cells(GNOOFBOOKLET.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(GRIDSAMPLETYPE)
            alParaval.Add(ITEMNAME)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(NOOFBOOKLET)

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
                If row.Cells(0).Value <> Nothing Then

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

            alParaval.Add(ORDERGRIDSRNO)
            alParaval.Add(ORDERITEMNAME)
            alParaval.Add(ORDERDESIGNNO)
            alParaval.Add(ORDERCOLOR)
            alParaval.Add(ORDERBOOKLET)
            alParaval.Add(ORDERFROMNO)
            alParaval.Add(ORDERFROMSRNO)
            alParaval.Add(ORDERFROMTYPE)
            alParaval.Add(ORDEROUTBOOKLET)


            Dim objCUTTING As New ClsInterGodownTransferSample()
            objCUTTING.alParaval = alParaval
            If EDIT = False Then

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = objCUTTING.SAVE()
                MsgBox("Details Added")
                TXTGODOWNNO.Text = DTTABLE.Rows(0).Item(0)

            ElseIf EDIT = True Then

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                alParaval.Add(TEMPGODOWNNO)
                Dim IntResult As Integer = objCUTTING.UPDATE()
                MsgBox("Details Updated")
                EDIT = False
            End If
            PRINTREPORT(Val(TXTGODOWNNO.Text.Trim))
            clear()
            TXTDATE.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub JOBOUT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If errorvalid() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        ElseIf e.KeyCode = Keys.F5 Then
            GRIDTRANSFER.Focus()
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
        ElseIf e.KeyCode = Keys.P And e.Alt = True Then
            Call PrintToolStripButton_Click(sender, e)
        End If
    End Sub

    Private Sub InterGodownTransfer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SAMPLE MODULE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillcmb()
            clear()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objJO As New ClsInterGodownTransferSample()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPGODOWNNO)
                ALPARAVAL.Add(YearId)
                objJO.alParaval = ALPARAVAL
                Dim dttable As DataTable = objJO.SELECTGODOWN()

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTGODOWNNO.Text = TEMPGODOWNNO
                        TXTDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBFROMGODOWN.Text = dr("FROMGODOWN")
                        CMBTOGODOWN.Text = dr("TOGODOWN")
                        TXTMODEOFSHIPMENT.Text = dr("MODEOFSHIPMENT")
                        TXTISSUEBY.Text = dr("ISSUEDBY")
                        txtremarks.Text = dr("remarks")

                        GRIDTRANSFER.Rows.Add(Val(dr("GRIDSRNO")), dr("SAMPLETYPE"), dr("ITEM").ToString, dr("DESIGN").ToString, dr("SHADE").ToString, Val(dr("NOOFBOOKLET")))
                    Next
                    'Dim OBJCMN As New ClsCommon
                    Dim OBJCMN As New ClsCommon
                    dttable = OBJCMN.SEARCH(" ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ITEMMASTER.item_name AS ITEMNAME, INTERGODOWNTRANSFER_ORDERDETAILS.TRANSFER_GRIDSRNO AS GRIDSRNO, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, INTERGODOWNTRANSFER_ORDERDETAILS.TRANSFER_ORDERBOOKLET AS ORDERBOOKLET, INTERGODOWNTRANSFER_ORDERDETAILS.TRANSFER_FROMNO AS FROMNO, INTERGODOWNTRANSFER_ORDERDETAILS.TRANSFER_FROMSRNO AS FROMSRNO, INTERGODOWNTRANSFER_ORDERDETAILS.TRANSFER_FROMTYPE AS FROMTYPE, INTERGODOWNTRANSFER_ORDERDETAILS.TRANSFER_BOOKLET AS BOOKLET, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR ", "", " INTERGODOWNTRANSFER_ORDERDETAILS INNER JOIN ITEMMASTER ON INTERGODOWNTRANSFER_ORDERDETAILS.TRANSFER_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON INTERGODOWNTRANSFER_ORDERDETAILS.TRANSFER_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON INTERGODOWNTRANSFER_ORDERDETAILS.TRANSFER_DESIGNID = DESIGNMASTER.DESIGN_id", " AND INTERGODOWNTRANSFER_ORDERDETAILS.TRANSFER_NO = " & TEMPGODOWNNO & " AND INTERGODOWNTRANSFER_ORDERDETAILS.TRANSFER_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            GRIDORDER.Rows.Add(Val(DTR("GRIDSRNO")), DTR("ITEMNAME"), DTR("DESIGNNO"), DTR("COLOR"), Val(DTR("ORDERBOOKLET")), Val(DTR("FROMNO")), Val(DTR("FROMSRNO")), DTR("FROMTYPE"), Val(DTR("BOOKLET")))
                        Next
                    End If
                    getsrno(GRIDORDER)

                    TOTAL()
                    GRIDTRANSFER.FirstDisplayedScrollingRowIndex = GRIDTRANSFER.RowCount - 1
                Else
                    EDIT = False
                    clear()
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Sub TOTAL()
        Try
            TXTTOTALBOOKLETS.Clear()
            For Each ROW As DataGridViewRow In GRIDTRANSFER.Rows
                TXTTOTALBOOKLETS.Text = Val(TXTTOTALBOOKLETS.Text.Trim) + Val(ROW.Cells(GNOOFBOOKLET.Index).EditedFormattedValue)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            If CMBFROMGODOWN.Text.Trim = "" Then fillGODOWN(CMBFROMGODOWN, EDIT)
            If CMBTOGODOWN.Text.Trim = "" Then fillGODOWN(CMBTOGODOWN, EDIT)
            fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
            FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
            FILLSAMPLETYPE(CMBSAMPLETYPE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJEMB As New InterGodownTransferSampleDetails
            OBJEMB.MdiParent = MDIMain
            OBJEMB.Show()
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

    Sub EDITROW()
        Try
            If GRIDTRANSFER.CurrentRow.Index >= 0 And GRIDTRANSFER.Item(GSRNO.Index, GRIDTRANSFER.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                txtsrno.Text = Val(GRIDTRANSFER.Item(GSRNO.Index, GRIDTRANSFER.CurrentRow.Index).Value)
                CMBSAMPLETYPE.Text = GRIDTRANSFER.Item(GSAMPLETYPE.Index, GRIDTRANSFER.CurrentRow.Index).Value.ToString
                cmbitemname.Text = GRIDTRANSFER.Item(GITEMNAME.Index, GRIDTRANSFER.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDTRANSFER.Item(GDESIGN.Index, GRIDTRANSFER.CurrentRow.Index).Value.ToString
                cmbcolor.Text = GRIDTRANSFER.Item(GCOLOR.Index, GRIDTRANSFER.CurrentRow.Index).Value.ToString
                TXTNOOFBOOKLET.Text = Val(GRIDTRANSFER.Item(GNOOFBOOKLET.Index, GRIDTRANSFER.CurrentRow.Index).Value)

                TEMPROW = GRIDTRANSFER.CurrentRow.Index
                cmbitemname.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            GRIDTRANSFER.RowCount = 0
LINE1:
            TEMPGODOWNNO = Val(TXTGODOWNNO.Text) - 1
            If TEMPGODOWNNO > 0 Then
                EDIT = True
                InterGodownTransfer_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDTRANSFER.RowCount = 0 And TEMPGODOWNNO > 1 Then
                TXTGODOWNNO.Text = TEMPGODOWNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            GRIDTRANSFER.RowCount = 0

LINE1:
            TEMPGODOWNNO = Val(TXTGODOWNNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTGODOWNNO.Text.Trim
            clear()
            If Val(TXTGODOWNNO.Text) - 1 >= TEMPGODOWNNO Then
                EDIT = True
                InterGodownTransfer_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDTRANSFER.RowCount = 0 And TEMPGODOWNNO < MAXNO Then
                TXTGODOWNNO.Text = TEMPGODOWNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBFROMGODOWN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBFROMGODOWN.Enter
        Try
            If CMBFROMGODOWN.Text.Trim = "" Then fillGODOWN(CMBFROMGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMGODOWN_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBFROMGODOWN.Validating
        Try
            If CMBFROMGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBFROMGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOGODOWN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTOGODOWN.Enter
        Try
            If CMBTOGODOWN.Text.Trim = "" Then fillGODOWN(CMBTOGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOGODOWN_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTOGODOWN.Validating
        Try
            If CMBTOGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBTOGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDJO_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDTRANSFER.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPGODOWNNO)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub PRINTREPORT(ByVal GODOWNNO As Integer)
        'Try
        '    If MsgBox("Wish to Print?", MsgBoxStyle.YesNo) = vbYes Then
        '        Dim OBJGDN As New GDNDESIGN
        '        OBJGDN.MdiParent = MDIMain
        '        OBJGDN.FRMSTRING = "GODOWNTRANSFER"
        '        OBJGDN.FORMULA = "{INTERGODOWNTRANSFER.TRANSFER_NO}=" & Val(GODOWNNO) & " and {INTERGODOWNTRANSFER.TRANSFER_yearid}=" & YearId
        '        OBJGDN.Show()
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim TEMPMSG As Integer = MsgBox("Wish to Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                Dim OBJEMB As New ClsInterGodownTransferSample

                ALPARAVAL.Add(TEMPGODOWNNO)
                ALPARAVAL.Add(YearId)
                OBJEMB.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJEMB.Delete()
                MsgBox("Entry Deleted Succesfully")
                EDIT = False
                clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub txtremarks_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtremarks.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then txtremarks.Text = OBJREMARKS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JODATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTDATE.Validating
        Try
            If TXTDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(TXTDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    If Not datecheck(TXTDATE.Text) Then
                        MsgBox("Date not in Accounting Year")
                        e.Cancel = True

                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbitemname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "MERCHANT"
                OBJItem.STRSEARCH = " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then cmbitemname.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitemname.Enter
        Try
            If cmbitemname.Text.Trim = "" Then fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbitemname.Validating
        Try
            If cmbitemname.Text.Trim <> "" Then itemvalidate(cmbitemname, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
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

    Private Sub GRIDJO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDTRANSFER.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDTRANSFER.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                'end of block
                GRIDTRANSFER.Rows.RemoveAt(GRIDTRANSFER.CurrentRow.Index)
                TOTAL()
                getsrno(GRIDTRANSFER)

            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Enter(sender As Object, e As EventArgs)
        Try
            If cmbcolor.Text.Trim = "" Then FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Validating(sender As Object, e As CancelEventArgs)
        Try
            If cmbcolor.Text.Trim <> "" Then COLORVALIDATE(cmbcolor, e, Me, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            GRIDTRANSFER.Enabled = True

            If GRIDDOUBLECLICK = False Then
                GRIDTRANSFER.Rows.Add(Val(txtsrno.Text.Trim), CMBSAMPLETYPE.Text.Trim, cmbitemname.Text.Trim, CMBDESIGN.Text.Trim, cmbcolor.Text.Trim, Format(Val(TXTNOOFBOOKLET.Text.Trim), "0"))
                getsrno(GRIDTRANSFER)

            ElseIf GRIDDOUBLECLICK = True Then
                GRIDTRANSFER.Item(GSRNO.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
                GRIDTRANSFER.Item(GSAMPLETYPE.Index, TEMPROW).Value = CMBSAMPLETYPE.Text.Trim
                GRIDTRANSFER.Item(GITEMNAME.Index, TEMPROW).Value = cmbitemname.Text.Trim
                GRIDTRANSFER.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
                GRIDTRANSFER.Item(GCOLOR.Index, TEMPROW).Value = cmbcolor.Text.Trim
                GRIDTRANSFER.Item(GNOOFBOOKLET.Index, TEMPROW).Value = Format(Val(TXTNOOFBOOKLET.Text.Trim), "0")

                GRIDDOUBLECLICK = False
            End If

            GRIDTRANSFER.FirstDisplayedScrollingRowIndex = GRIDTRANSFER.RowCount - 1

            txtsrno.Text = Val(GRIDTRANSFER.RowCount) + 1
            CMBSAMPLETYPE.Text = ""
            cmbitemname.Text = ""
            CMBDESIGN.Text = ""
            cmbcolor.Text = ""
            TXTNOOFBOOKLET.Clear()
            cmbitemname.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(sender As Object, e As CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDTRANSFER.RowCount = 0
                TEMPGODOWNNO = Val(tstxtbillno.Text)
                If TEMPGODOWNNO > 0 Then
                    EDIT = True
                    InterGodownTransfer_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
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
                    GRIDTRANSFER.Rows.Add(GRIDTRANSFER.RowCount + 1, CMBSAMPLETYPE.Text.Trim, DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("DESIGN"), DT.Rows(0).Item("COLOR"), 1)
                    GRIDTRANSFER.FirstDisplayedScrollingRowIndex = GRIDTRANSFER.RowCount - 1
                    TOTAL()
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

    Private Sub TXTDATE_GotFocus(sender As Object, e As EventArgs) Handles TXTDATE.GotFocus
        TXTDATE.SelectionStart = 0
    End Sub

    Private Sub CMDSELECTSMP_Click(sender As Object, e As EventArgs) Handles CMDSELECTSMP.Click
        Try

            'If CMBPARTYNAME.Text.Trim = "" Then
            '    MsgBox("Select Party Name", MsgBoxStyle.Critical)
            '    CMBPARTYNAME.Focus()
            '    Exit Sub
            'End If


            Dim DTPO As New DataTable
            Dim OBJSMP As New SelectSampleOrder
            OBJSMP.WHERECLAUSE = " AND SELFORDER = 'TRUE'"
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

            getsrno(GRIDTRANSFER)
            CMDSELECTSMP.Enabled = True
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNOOFBOOKLET_Validated(sender As Object, e As EventArgs) Handles TXTNOOFBOOKLET.Validated
        Try
            If CMBSAMPLETYPE.Text.Trim <> "" And cmbitemname.Text.Trim <> "" And Val(TXTNOOFBOOKLET.Text.Trim) > 0 Then
                fillgrid()
                TOTAL()
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
            End If
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
End Class