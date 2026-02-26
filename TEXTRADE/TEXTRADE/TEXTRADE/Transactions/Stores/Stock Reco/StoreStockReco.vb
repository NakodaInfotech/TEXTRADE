

Imports System.ComponentModel
Imports BL



Public Class StoreStockReco

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Public TEMPRECONO As Integer          'used for editing
    Public EDIT As Boolean          'used for editing
    Dim TEMPROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer
    Dim ALLOWMANUALRECNO As Boolean = False

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub StoreStockReco_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            GRIDSTOCKOUT.Focus()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        End If
    End Sub

    Sub FILLCMB()
        If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
        If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        If CMBTRANS.Text.Trim = "" Then FILLNAME(CMBTRANS, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        If CMBSTOREITEMNAME.Text.Trim = "" Then FILLSTOREITEMNAME(CMBSTOREITEMNAME)
        If CMBUNIT.Text.Trim = "" Then fillunit(CMBUNIT)
    End Sub


    Private Sub StoreStockReco_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'STORESTOCKRECO'")
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


                Dim objSTOCK As New ClsStoreStockAdjustment()
                Dim dttable As DataTable = objSTOCK.SELECTSTORESTOCKADJUSTMENT(TEMPRECONO, CmpId, Locationid, YearId)
                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows
                        TXTRECONO.Text = TEMPRECONO
                        TXTRECONO.ReadOnly = True
                        DTRECODATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                        CMBTRANS.Text = dr("TRANSNAME")
                        CMBNAME.Text = dr("NAME")
                        TXTCHALLANNO.Text = dr("CHALLANNO")
                        DTCHALLANDATE.Text = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")
                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        LBLTOTALINQTY.Text = Convert.ToString(dr("TOTALINQTY").ToString)
                        LBLTOTALOUTQTY.Text = Convert.ToString(dr("TOTALOUTQTY").ToString)

                        'Item Grid
                        If Val(dr("GRIDSRNO")) > 0 Then GRIDSTOCKOUT.Rows.Add(dr("GRIDSRNO").ToString, dr("ITEMNAME").ToString, dr("DESC").ToString, Format(Val(dr("QTY")), "0.00"), dr("UNIT"), Format(Val(dr("RATE")), "0.00"))

                    Next



                    'GET DATA FROM STOCKADJUSTMENT_INDESC
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_INGRIDSRNO, 0) AS GRIDSRNO,  ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_INDESC, '') AS INDESC, ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_INQTY, 0) AS INQTY, ISNULL(UNITMASTER.unit_abbr, '') AS INUNIT,  ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_INRATE, 0) AS INRATE,  ISNULL(STOREITEMMASTER.STOREITEM_NAME, '') AS INITEMNAME ", "", " STORESTOCKADJUSTMENT LEFT OUTER JOIN STORESTOCKADJUSTMENT_INDESC ON STORESTOCKADJUSTMENT.SA_no = STORESTOCKADJUSTMENT_INDESC.SA_NO AND STORESTOCKADJUSTMENT.SA_yearid = STORESTOCKADJUSTMENT_INDESC.SA_YEARID LEFT OUTER JOIN UNITMASTER ON STORESTOCKADJUSTMENT_INDESC.SA_INUNITID = UNITMASTER.unit_id LEFT OUTER JOIN STOREITEMMASTER ON STORESTOCKADJUSTMENT_INDESC.SA_INITEMID = STOREITEMMASTER.STOREITEM_ID  ", " AND STORESTOCKADJUSTMENT.SA_NO = " & TEMPRECONO & " AND STORESTOCKADJUSTMENT_INDESC.SA_YEARID = " & YearId & " ORDER BY STORESTOCKADJUSTMENT_INDESC.SA_INGRIDSRNO")

                    'Dim DT As DataTable = OBJCMN.search(" ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(PIECETYPEMASTER.PIECETYPE_name,'') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_BALENO, '') AS BALENO, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_CUT, 0) AS CUT, ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_QTY, 0) AS QTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_MTRS, 0) AS MTRS,ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_RACKID, 0) AS RACK,ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_SHELFID, 0) AS SHELF, ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_BARCODE, '') AS BARCODE, ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_OUTPCS, 0) AS OUTPCS, ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_OUTMTRS, 0) AS OUTMTRS, STORESTOCKADJUSTMENT_INDESC.SA_GRIDDONE AS GRIDDONE ", "", " STORESTOCKADJUSTMENT_INDESC INNER JOIN PIECETYPEMASTER ON STORESTOCKADJUSTMENT_INDESC.SA_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN UNITMASTER ON STORESTOCKADJUSTMENT_INDESC.SA_QTYUNITID = UNITMASTER.unit_id LEFT OUTER JOIN DESIGNMASTER AS DESIGNMASTER ON STORESTOCKADJUSTMENT_INDESC.SA_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON STORESTOCKADJUSTMENT_INDESC.SA_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON STORESTOCKADJUSTMENT_INDESC.SA_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN ITEMMASTER AS ITEMMASTER ON STORESTOCKADJUSTMENT_INDESC.SA_ITEMID = ITEMMASTER.item_id ", " AND SA_NO = " & TEMPRECONO & " AND SA_YEARID = " & YearId)
                    For Each DR As DataRow In DT.Rows
                        'Item Grid
                        GRIDSTOCKIN.Rows.Add(DR("GRIDSRNO").ToString, DR("INITEMNAME").ToString, DR("INDESC").ToString, Format(Val(DR("INQTY")), "0.00"), DR("INUNIT"), Format(Val(DR("INRATE")), "0.00"))


                        TabControl1.SelectedIndex = 1
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

    Sub CLEAR()

        EP.Clear()
        CHKCOPY.CheckState = CheckState.Unchecked
        CMBGODOWN.Text = ""
        CMBNAME.Text = ""
        CMBTRANS.Text = ""
        TXTCHALLANNO.Clear()
        DTRECODATE.Text = Now.Date
        DTCHALLANDATE.Text = Now.Date
        tstxtbillno.Clear()
        txtremarks.Clear()

        TXTSRNO.Text = 1
        CMBSTOREITEMNAME.Text = ""
        TXTDESC.Clear()
        TXTQTY.Clear()
        CMBUNIT.Text = ""
        TXTRATE.Clear()


        GRIDSTOCKIN.RowCount = 0
        GRIDSTOCKOUT.RowCount = 0
        GRIDDOUBLECLICK = False
        TabControl1.SelectedIndex = 0
        getmaxno()

        If ALLOWMANUALRECNO = True Then
            TXTRECONO.ReadOnly = False
            TXTRECONO.BackColor = Color.LemonChiffon
        Else
            TXTRECONO.ReadOnly = True
            TXTRECONO.BackColor = Color.Linen

            TXTMTRSDIFF.Clear()
            LBLTOTALOUTQTY.Text = 0.0
            LBLTOTALINQTY.Text = 0.0

        End If

        CMDSELECTSTOCK.Enabled = True
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True
            'If ALLOWADJQTYDIFF = False And Val(LBLTOTALINQTY.Text.Trim) < Val(LBLTOTALOUTQTY.Text.Trim) Then
            '    EP.SetError(TXTMTRSDIFF, " In Qty Cannot be Less than Out Qty")
            '    bln = False
            'End If
            If ALLOWMANUALRECNO = True Then
                If Val(TXTRECONO.Text.Trim) <> 0 And EDIT = False Then
                    Dim OBJCMNn As New ClsCommon
                    Dim dttable As DataTable = OBJCMNn.SEARCH(" ISNULL(STORESTOCKADJUSTMENT.SA_NO,0)  AS RECONO", "", " STORESTOCKADJUSTMENT ", "  AND STORESTOCKADJUSTMENT.SA_NO=" & Val(TXTRECONO.Text.Trim) & " AND STORESTOCKADJUSTMENT.SA_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        MsgBox("Rec No Already Exist")
                        bln = False
                    End If
                End If
            End If
            If CMBGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBGODOWN, " Please Fill Godown")
                bln = False
            End If



            If GRIDSTOCKOUT.RowCount = 0 And GRIDSTOCKIN.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If
            'CHEKC BARCODE IS PRESENT IN DATABASE OR NOT

            If Not datecheck(DTRECODATE.Text) Then
                EP.SetError(DTRECODATE, "Date not in Accounting Year")
                bln = False
            End If

            If Not datecheck(DTCHALLANDATE.Text) Then
                EP.SetError(DTRECODATE, "Date not in Accounting Year")
                bln = False
            End If

            'If CMBUNIT.Text = "" And CMBSTOREITEMNAME.Text = "" Then
            '    EP.SetError(TabControl1, "Fill Details Properly")
            '    bln = False
            'End If


            If Convert.ToDateTime(DTRECODATE.Text).Date < STOCKADJBLOCKDATE.Date Then
                EP.SetError(DTRECODATE, "Date is Blocked, Please make entries after " & Format(STOCKADJBLOCKDATE.Date, "dd/MM/yyyy"))    'UNCOMMENT AFTER ADDING BLOCKDATE
                bln = False
            End If



            'If Convert.ToDateTime(DTRECODATE.Text).Date < STORESTOCKADJBLOCKDATE.Date Then
            '    EP.SetError(DTRECODATE, "Date is Blocked, Please make entries after " & Format(STORESTOCKADJBLOCKDATE.Date, "dd/MM/yyyy"))
            '    bln = False
            'End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function
    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        DTRECODATE.Focus()

    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(SA_no),0) + 1 ", " STORESTOCKADJUSTMENT ", " AND SA_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTRECONO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            If TXTRECONO.ReadOnly = False Then
                alParaval.Add(Val(TXTRECONO.Text.Trim))
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(Format(Convert.ToDateTime(DTRECODATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBGODOWN.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBTRANS.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(DTCHALLANDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(LBLTOTALINQTY.Text.Trim)
            alParaval.Add(LBLTOTALOUTQTY.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim gridsrno As String = ""
            Dim ITEMNAME As String = ""
            Dim DESC As String = ""
            Dim QTY As String = ""
            Dim UNIT As String = ""
            Dim RATE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDSTOCKOUT.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(GSRNO.Index).Value.ToString
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        DESC = row.Cells(GDESC.Index).Value
                        QTY = row.Cells(GQTY.Index).Value.ToString
                        UNIT = row.Cells(GUNIT.Index).Value.ToString
                        RATE = row.Cells(GRATE.Index).Value.ToString
                    Else
                        gridsrno = gridsrno & "|" & row.Cells(GSRNO.Index).Value.ToString
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value.ToString
                        DESC = DESC & "|" & row.Cells(GDESC.Index).Value
                        QTY = QTY & "|" & row.Cells(GQTY.Index).Value.ToString
                        UNIT = UNIT & "|" & row.Cells(GUNIT.Index).Value.ToString
                        RATE = RATE & "|" & row.Cells(GRATE.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(ITEMNAME)
            alParaval.Add(DESC)
            alParaval.Add(QTY)
            alParaval.Add(UNIT)
            alParaval.Add(RATE)

            Dim INGRIDSRNO As String = ""
            Dim INITEMNAME As String = ""
            Dim INGDESC As String = ""
            Dim INQTY As String = ""
            Dim INUNIT As String = ""
            Dim INRATE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDSTOCKIN.Rows
                If row.Cells(0).Value <> Nothing Then
                    If INGRIDSRNO = "" Then
                        INGRIDSRNO = row.Cells(ESRNO.Index).Value.ToString
                        INITEMNAME = row.Cells(EITEMNAME.Index).Value.ToString
                        INGDESC = row.Cells(EGDESC.Index).Value
                        INQTY = row.Cells(EQTY.Index).Value.ToString
                        INUNIT = row.Cells(EUNIT.Index).Value.ToString
                        INRATE = row.Cells(ERATE.Index).Value.ToString
                    Else
                        INGRIDSRNO = INGRIDSRNO & "|" & row.Cells(ESRNO.Index).Value.ToString
                        INITEMNAME = INITEMNAME & "|" & row.Cells(EITEMNAME.Index).Value.ToString
                        INGDESC = INGDESC & "|" & row.Cells(EGDESC.Index).Value
                        INQTY = INQTY & "|" & row.Cells(EQTY.Index).Value.ToString
                        INUNIT = INUNIT & "|" & row.Cells(EUNIT.Index).Value.ToString
                        INRATE = INRATE & "|" & row.Cells(ERATE.Index).Value.ToString
                    End If
                End If
            Next

            alParaval.Add(INGRIDSRNO)
            alParaval.Add(INITEMNAME)
            alParaval.Add(INGDESC)
            alParaval.Add(INQTY)
            alParaval.Add(INUNIT)
            alParaval.Add(INRATE)

            'alParaval.Add(CMBNAME.Text.Trim)

            Dim objSTOCK As New ClsStoreStockAdjustment()
            objSTOCK.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = objSTOCK.SAVE()
                MsgBox("Details Added")
                TXTRECONO.Text = DTTABLE.Rows(0).Item(0)
                TEMPRECONO = DTTABLE.Rows(0).Item(0)
                'PRINTREPORT(DTTABLE.Rows(0).Item(0))

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                alParaval.Add(TEMPRECONO)
                IntResult = objSTOCK.UPDATE()
                MsgBox("Details Updated")
                'PRINTREPORT(TEMPRECONO)
                EDIT = False
            End If


            CLEAR()
            DTRECODATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
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

    Sub TOTAL()
        Try
            LBLTOTALOUTQTY.Text = 0.0
            LBLTOTALINQTY.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDSTOCKOUT.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALOUTQTY.Text = Format(Val(LBLTOTALOUTQTY.Text) + Val(ROW.Cells(GQTY.Index).EditedFormattedValue), "0.00")
                End If
            Next

            For Each ROW As DataGridViewRow In GRIDSTOCKIN.Rows
                If ROW.Cells(ESRNO.Index).Value <> Nothing Then
                    LBLTOTALINQTY.Text = Format(Val(LBLTOTALINQTY.Text) + Val(ROW.Cells(EQTY.Index).EditedFormattedValue), "0.00")
                End If
            Next
            'TXTMTRSDIFF.Text = Format(Val(LBLTOTALINQTY.Text) - Val(LBLTOTALINSHEETS.Text), "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then FILLNAME(CMBTRANS, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then NAMEVALIDATE(CMBTRANS, cmbcode, e, Me, TXTTRANSADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSTOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTSTOCK.Click
        Try
            If CMBGODOWN.Text.Trim = "" And GRIDSTOCKOUT.RowCount = 0 Then
                MsgBox("Please Select Godown First", MsgBoxStyle.Critical)
                CMBGODOWN.Focus()
                Exit Sub
            End If

            Dim DTTABLE As New DataTable
            Dim OBJSELECTGDN As New SelectStock
            OBJSELECTGDN.GODOWN = CMBGODOWN.Text.Trim
            OBJSELECTGDN.ShowDialog()
            DTTABLE = OBJSELECTGDN.DTNEW

            If DTTABLE.Rows.Count > 0 Then
                TXTCHALLANNO.Text = DTTABLE.Rows(0).Item("CHALLANNO").ToString

                For Each dr As DataRow In DTTABLE.Rows
                    GRIDSTOCKOUT.Rows.Add(0, dr("ITEMNAME"), dr("LOTNO"), 0, Format(Val(dr("WT")), "0.00"), Format(Val(dr("QTY")), "0.00"), dr("UNIT"), dr("RATE"), 0, 0, "")
                    If CHKCOPY.Checked = True Then GRIDSTOCKIN.Rows.Add(0, dr("ITEMNAME"), dr("LOTNO"), 0, Format(Val(dr("WT")), "0.00"), Format(Val(dr("QTY")), "0.00"), dr("UNIT"), dr("RATE"), 0, 0, "")
                Next
                GRIDSTOCKOUT.FirstDisplayedScrollingRowIndex = GRIDSTOCKOUT.RowCount - 1
                GETSRNO(GRIDSTOCKOUT)
                GETSRNO(GRIDSTOCKIN)

                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDSTOCKOUT.RowCount = 0
                GRIDSTOCKIN.RowCount = 0
                TEMPRECONO = Val(tstxtbillno.Text)
                If TEMPRECONO > 0 Then
                    EDIT = True
                    StoreStockReco_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            GRIDSTOCKIN.Enabled = True

            If GRIDDOUBLECLICK = False Then
                GRIDSTOCKIN.Rows.Add(Val(TXTSRNO.Text.Trim), CMBSTOREITEMNAME.Text.Trim, TXTDESC.Text.Trim, Format(Val(TXTQTY.Text.Trim), "0.00"), CMBUNIT.Text.Trim, Format(Val(TXTRATE.Text.Trim), "0.00"))
                GETSRNO(GRIDSTOCKIN)

            ElseIf GRIDDOUBLECLICK = True Then

                GRIDSTOCKIN.Item(ESRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
                GRIDSTOCKIN.Item(EITEMNAME.Index, TEMPROW).Value = CMBSTOREITEMNAME.Text.Trim
                GRIDSTOCKIN.Item(EGDESC.Index, TEMPROW).Value = TXTDESC.Text.Trim
                GRIDSTOCKIN.Item(EQTY.Index, TEMPROW).Value = Format(Val(TXTQTY.Text.Trim), "0.00")
                GRIDSTOCKIN.Item(EUNIT.Index, TEMPROW).Value = CMBUNIT.Text.Trim
                GRIDSTOCKIN.Item(ERATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")


                GRIDDOUBLECLICK = False
            End If

            TOTAL()

            GRIDSTOCKIN.FirstDisplayedScrollingRowIndex = GRIDSTOCKIN.RowCount - 1

            TXTSRNO.Text = GRIDSTOCKIN.RowCount + 1
            CMBSTOREITEMNAME.Text = ""
            TXTDESC.Clear()
            TXTQTY.Clear()
            CMBUNIT.Text = ""
            TXTRATE.Clear()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDJOBIN_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDSTOCKIN.CellDoubleClick
        EDITROW()
    End Sub

    Sub EDITROW()
        Try
            If GRIDSTOCKIN.CurrentRow.Index >= 0 And GRIDSTOCKIN.Item(GSRNO.Index, GRIDSTOCKIN.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDSTOCKIN.Item(ESRNO.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBSTOREITEMNAME.Text = GRIDSTOCKIN.Item(EITEMNAME.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTQTY.Text = GRIDSTOCKIN.Item(EQTY.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBUNIT.Text = GRIDSTOCKIN.Item(EUNIT.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTRATE.Text = GRIDSTOCKIN.Item(ERATE.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTDESC.Text = GRIDSTOCKIN.Item(EGDESC.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDSTOCKIN.CurrentRow.Index
                TXTSRNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub


    Private Sub GRIDSTOCKOUT_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDSTOCKOUT.CellValidating
        Try
            Dim colNum As Integer = GRIDSTOCKOUT.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GQTY.Index, GUNIT.Index, GRATE.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDSTOCKOUT.CurrentCell.Value = Nothing Then GRIDSTOCKOUT.CurrentCell.Value = "0.00"
                        GRIDSTOCKOUT.CurrentCell.Value = Convert.ToDecimal(GRIDSTOCKOUT.Item(colNum, e.RowIndex).Value)
                        '' everything is good
                        TOTAL()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If

            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSTOCKOUT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDSTOCKOUT.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSTOCKOUT.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDSTOCKOUT.Rows.RemoveAt(GRIDSTOCKOUT.CurrentRow.Index)
                GETSRNO(GRIDSTOCKOUT)
                TOTAL()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then
                If MsgBox("Wish to Delete Stock Adjustment?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub



                Dim ALPARAVAL As New ArrayList
                Dim OBSTOCK As New ClsStoreStockAdjustment

                ALPARAVAL.Add(TEMPRECONO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                OBSTOCK.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBSTOCK.DELETE()
                MsgBox("Store Stock Adjustment Deleted Succesfully")
                CLEAR()
                EDIT = False
                DTRECODATE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If CMBUNIT.Text.Trim <> "" Then unitvalidate(CMBUNIT, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDJOBIN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDSTOCKIN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSTOCKIN.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                'end of block
                GRIDSTOCKIN.Rows.RemoveAt(GRIDSTOCKIN.CurrentRow.Index)
                GETSRNO(GRIDSTOCKIN)
                TOTAL()
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub CALC()
        'Try
        '    If Val(TXTQTY.Text.Trim) > 0 And Val(TXTQTY.Text.Trim) > 0 Then TXTMTRS.Text = Format(Val(TXTQTY.Text.Trim) * Val(TXTQTY.Text.Trim), "0.00")
        '    If CMBPER.Text = "Mtrs" Then TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(TXTMTRS.Text.Trim), "0.00") Else TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(TXTQTY.Text.Trim), "0.00")
        'Catch ex As Exception
        '    Throw ex
        'End Try
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


    'Private Sub cmbitemname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Try
    '        If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
    '        If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

    '        If e.KeyCode = Keys.F1 Then
    '            Dim OBJItem As New SelectItem
    '            OBJItem.FRMSTRING = "MERCHANT"
    '            OBJItem.STRSEARCH = " and ITEM_YEARid = " & YearId
    '            OBJItem.ShowDialog()
    '            If OBJItem.TEMPNAME <> "" Then CMBITEMNAME.Text = OBJItem.TEMPNAME
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try

    '        If USEREDIT = False And USERVIEW = False Then
    '            MsgBox("Insufficient Rights")
    '            Exit Sub
    '        End If

    '        Dim OBJstock As New StoreStockRecoDetails
    '        OBJstock.MdiParent = MDIMain
    '        OBJstock.Show()
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub

    Sub PRINTREPORT()
        'Try
        '    If MsgBox("Wish to Print Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        '    Dim OBJSA As New SaleOrderDesign
        '    OBJSA.MdiParent = MDIMain
        '    OBJSA.FORMULA = "{STORESTOCKADJUSTMENT.SA_NO} = " & Val(TXTRECONO.Text.Trim) & " AND {STORESTOCKADJUSTMENT.SA_YEARID} = " & YearId
        '    OBJSA.FRMSTRING = "STOCKRECO"
        '    OBJSA.Show()
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub StoreStockReco_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        TXTQTY.ReadOnly = False

    End Sub
    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs)
        numkeypress(e, sender, Me)
    End Sub
    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE='ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, cmbcode, e, Me, TXTADD, " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE='ACCOUNTS'", "", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDSTOCKOUT.RowCount = 0
            GRIDSTOCKIN.RowCount = 0
LINE1:
            TEMPRECONO = Val(TXTRECONO.Text) - 1
            If TEMPRECONO > 0 Then
                EDIT = True
                StoreStockReco_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDSTOCKOUT.RowCount = 0 And GRIDSTOCKIN.RowCount = 0 And TEMPRECONO > 1 Then
                TXTRECONO.Text = TEMPRECONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
LINE1:
            TEMPRECONO = Val(TXTRECONO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTRECONO.Text.Trim
            CLEAR()
            If Val(TXTRECONO.Text) - 1 >= TEMPRECONO Then
                EDIT = True
                StoreStockReco_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDSTOCKOUT.RowCount = 0 And GRIDSTOCKIN.RowCount = 0 And TEMPRECONO < MAXNO Then
                TXTRECONO.Text = TEMPRECONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTRECONO_Validating(sender As Object, e As CancelEventArgs) Handles TXTRECONO.Validating
        Try
            If Val(TXTRECONO.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(STORESTOCKADJUSTMENT.SA_NO,0)  AS RECONO", "", " STORESTOCKADJUSTMENT ", "  AND STORESTOCKADJUSTMENT.SA_NO=" & Val(TXTRECONO.Text.Trim) & " AND STORESTOCKADJUSTMENT.SA_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Rec No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRECONO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTRECONO.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub


    Private Sub GRIDSTOCKIN_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDSTOCKIN.CellValidating
        Try
            Dim colNum As Integer = GRIDSTOCKIN.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case EQTY.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDSTOCKIN.CurrentCell.Value = Nothing Then GRIDSTOCKIN.CurrentCell.Value = "0.00"
                        GRIDSTOCKIN.CurrentCell.Value = Convert.ToDecimal(GRIDSTOCKIN.Item(colNum, e.RowIndex).Value)
                        '' everything is good
                        TOTAL()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If

            End Select
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

            Dim OBJstock As New StoreStockRecoDetails
            OBJstock.MdiParent = MDIMain
            OBJstock.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTDESC_Validated(sender As Object, e As EventArgs) Handles TXTRATE.Validated
        Try
            If CMBSTOREITEMNAME.Text <> "" And CMBUNIT.Text <> "" Then
                FILLGRID()
                If CMBUNIT.Text = "" Then
                    cmdok.Focus()
                Else
                    CMBSTOREITEMNAME.Focus()
                End If
            Else
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBUNIT_Enter(sender As Object, e As EventArgs) Handles CMBUNIT.Enter
        Try
            If CMBUNIT.Text.Trim = "" Then fillunit(CMBUNIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub CMBUNIT_Validating(sender As Object, e As CancelEventArgs) Handles CMBUNIT.Validating
        Try
            If CMBUNIT.Text.Trim <> "" Then unitvalidate(CMBUNIT, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSTOREITEMNAME_Enter(sender As Object, e As EventArgs) Handles CMBSTOREITEMNAME.Enter
        Try
            If CMBSTOREITEMNAME.Text.Trim = "" Then FILLSTOREITEMNAME(CMBSTOREITEMNAME)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSTOREITEMNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBSTOREITEMNAME.Validating
        Try
            If CMBSTOREITEMNAME.Text.Trim <> "" Then STOREITEMVALIDATE(CMBSTOREITEMNAME, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


    Private Sub tstxtbillno_Validated(sender As Object, e As EventArgs) Handles tstxtbillno.Validated
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDSTOCKOUT.RowCount = 0
                GRIDSTOCKIN.RowCount = 0
                TEMPRECONO = Val(tstxtbillno.Text)
                If TEMPRECONO > 0 Then
                    EDIT = True
                    StoreStockReco_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTCHALLANNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTCHALLANNO.Validating
        Try
            If TXTCHALLANNO.Text.Trim.Length > 0 Then
                If EDIT = False Then
                    'for search
                    Dim objclscommon As New ClsCommon()
                    Dim dt As New DataTable
                    dt = objclscommon.SEARCH(" ISNULL(STORESTOCKADJUSTMENT.SA_CHALLANNO, '') AS CHALLANNO, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME", "", " STORESTOCKADJUSTMENT INNER JOIN LEDGERS ON STORESTOCKADJUSTMENT.SA_LEDGERID = LEDGERS.Acc_id AND STORESTOCKADJUSTMENT.SA_yearid = LEDGERS.Acc_yearid AND STORESTOCKADJUSTMENT.SA_cmpid = LEDGERS.Acc_cmpid ", " and STORESTOCKADJUSTMENT.SA_CHALLANNO = '" & TXTCHALLANNO.Text.Trim & "' AND STORESTOCKADJUSTMENT.SA_CMPID =" & CmpId & " AND STORESTOCKADJUSTMENT.SA_LOCATIONID =" & Locationid & " AND STORESTOCKADJUSTMENT.SA_YEARID =" & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Challan No. Already Exists", MsgBoxStyle.Critical, "PRINTPRO")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class



