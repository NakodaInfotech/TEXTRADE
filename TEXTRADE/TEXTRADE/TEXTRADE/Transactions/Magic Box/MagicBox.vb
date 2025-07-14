Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms
Imports BL
Public Class MagicBox
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim IntResult As Integer

            EP.Clear()

            For Each row As Windows.Forms.DataGridViewRow In GRIDSO.Rows
                Dim alParaval As New ArrayList
                Dim NO As String = ""
                Dim GRIDSRNO As String = ""
                Dim ORDERDATE As String = ""
                Dim BUYERS As String = ""
                Dim SELLERS As String = ""
                Dim DISCOUNT As String = ""
                Dim DELPERIOD As String = ""
                Dim DUEDATE As String = ""
                Dim ORDERNO As String = ""
                Dim MERCHANT As String = ""
                Dim QUALITY As String = ""
                Dim DESIGN As String = ""
                Dim gridremarks As String = ""
                Dim COLOR As String = ""
                Dim PARTYPONO As String = ""
                Dim qty As String = ""
                Dim QTYUNIT As String = ""
                Dim CUT As String = ""
                Dim MTRS As String = ""
                Dim RATE As String = ""
                Dim PER As String = ""
                Dim AMOUNT As String = ""
                Dim RECDQTY As String = ""
                Dim RECDMTRS As String = ""
                Dim DONE As String = ""
                Dim SAMPLEDONE As String = ""
                Dim CLOSED As String = ""
                Dim REMARKS As String = ""
                Dim CRDAYS As String = ""

                If row.Cells(0).Value <> Nothing Then



                    GRIDSRNO = 1 'row.Cells(gsrno.Index).Value.ToString
                    NO = row.Cells(GNO.Index).Value.ToString
                    ORDERDATE = Format(Convert.ToDateTime(row.Cells(GDATE.Index).Value), "MM/dd/yyyy")
                    BUYERS = row.Cells(GBUYERS.Index).Value.ToString
                    SELLERS = row.Cells(GSELLERS.Index).Value.ToString
                    CRDAYS = row.Cells(GCRDAYS.Index).Value.ToString
                    DISCOUNT = row.Cells(GDISCOUNT.Index).Value.ToString
                    DELPERIOD = row.Cells(GDELPERIOD.Index).Value.ToString
                    DUEDATE = Format(Convert.ToDateTime(row.Cells(GDUEDATE.Index).Value), "MM/dd/yyyy")
                    ORDERNO = row.Cells(GORDERNO.Index).Value.ToString
                    MERCHANT = row.Cells(gitemname.Index).Value.ToString
                    QUALITY = "" 'row.Cells(GQUALITY.Index).Value.ToString
                    DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                    gridremarks = "" ' row.Cells(gdesc.Index).Value.ToString
                    COLOR = "" 'row.Cells(gcolor.Index).Value.ToString
                    PARTYPONO = "" ' row.Cells(GORDERNO.Index).Value.ToString
                    qty = row.Cells(gQty.Index).Value.ToString
                    QTYUNIT = row.Cells(gqtyunit.Index).Value.ToString
                    CUT = row.Cells(gcut.Index).Value
                    MTRS = row.Cells(GMTRS.Index).Value
                    RATE = row.Cells(GRATE.Index).Value
                    PER = "" 'row.Cells(GPER.Index).Value
                    AMOUNT = "" 'row.Cells(GAMOUNT.Index).Value
                    RECDQTY = "" 'Val(row.Cells(GRECDQTY.Index).Value)
                    RECDMTRS = "" 'Val(row.Cells(GRECDMTRS.Index).Value)

                    DONE = 0 'If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then DONE = 1 Else DONE = 0
                    SAMPLEDONE = 0 'If Convert.ToBoolean(row.Cells(GSAMPLEDONE.Index).Value) = True Then SAMPLEDONE = 1 Else SAMPLEDONE = 0
                    CLOSED = 0 'If Convert.ToBoolean(row.Cells(GCLOSED.Index).Value) = True Then CLOSED = 1 Else CLOSED = 0
                    REMARKS = row.Cells(GREMARKS.Index).Value



                End If

                alParaval.Add(0)
                alParaval.Add(ORDERDATE)
                alParaval.Add(BUYERS)
                alParaval.Add("") ' HASTE
                alParaval.Add("") 'AGENT

                alParaval.Add(ORDERNO) 'PONO
                alParaval.Add(DUEDATE) 'DueDate.Value)
                alParaval.Add("") 'cmbtrans.Text.Trim)
                alParaval.Add("") 'cmbtrans2.Text.Trim)
                alParaval.Add("") 'cmbcity.Text.Trim)
                alParaval.Add("") 'TXTREFNO.Text.Trim)

                alParaval.Add("") 'CMBRISK.Text.Trim)
                alParaval.Add("") 'TXTCONSIGNOR.Text.Trim)
                alParaval.Add("") 'TXTCONSIGNEE.Text.Trim)
                alParaval.Add(SELLERS)
                alParaval.Add("") 'CMBCURRENCY.Text.Trim)
                alParaval.Add(0) 'lbltotalqty.Text.Trim)
                alParaval.Add(0) 'LBLTOTALMTRS.Text.Trim)
                alParaval.Add(0) 'lbltotalbale.Text.Trim)       '' *** TOTAL BALE INSTED OF TOTAL AMT.
                alParaval.Add(0) 'lbltotalamt.Text.Trim)

                alParaval.Add(0) 'txtdisper.Text.Trim)
                alParaval.Add(0) 'txtdisamt.Text.Trim)
                alParaval.Add(0) 'txtpfper.Text.Trim)
                alParaval.Add(0) 'txtpfamt.Text.Trim)
                alParaval.Add(0) 'txttestchgs.Text.Trim)
                alParaval.Add(0) 'txtnett.Text.Trim)
                alParaval.Add(0) 'Val(TXTEXCISEID.Text.Trim))
                alParaval.Add(0) 'TXTEXCISE.Text.Trim)
                alParaval.Add(0) 'txtexciseAMT.Text.Trim)
                alParaval.Add(0) 'TXTEDUCESS.Text.Trim)
                alParaval.Add(0) 'txteducessAMT.Text.Trim)
                alParaval.Add(0) 'TXTHSECESS.Text.Trim)
                alParaval.Add(0) 'txthsecessAMT.Text.Trim)
                alParaval.Add(0) 'txtsubtotal.Text.Trim)

                alParaval.Add(0) 'cmbtax.Text.Trim)
                alParaval.Add(0) 'txttax.Text.Trim)
                alParaval.Add(0) 'cmbaddtax.Text.Trim)
                alParaval.Add(0) 'txtaddtax.Text.Trim)
                alParaval.Add(0) 'txtfrper.Text.Trim)
                alParaval.Add(0) 'txtfreight.Text.Trim)
                alParaval.Add(0) 'cmboctroi.Text.Trim)
                alParaval.Add(0) 'txtoctroi.Text.Trim)
                alParaval.Add(0) 'txtinspchgs.Text.Trim)
                alParaval.Add(0) 'txtroundoff.Text.Trim)
                alParaval.Add(0) 'txtgrandtotal.Text.Trim)


                alParaval.Add("") 'txtinwords.Text.Trim)

                alParaval.Add(REMARKS)
                alParaval.Add("") 'txtnote.Text.Trim)
                alParaval.Add("") 'txttnc.Text.Trim)


                alParaval.Add("") 'cmbmisc.Text.Trim)
                alParaval.Add(Val(0)) 'txtDiscrate.Text.Trim))
                alParaval.Add(0) 'Val(txtDiscLot.Text.Trim))
                alParaval.Add(0) 'Val(txtdd.Text.Trim))
                alParaval.Add(0) 'Val(txtkatai.Text.Trim))
                alParaval.Add(DISCOUNT)
                alParaval.Add(0) 'Val(txtadat.Text.Trim))
                alParaval.Add(CRDAYS)
                alParaval.Add(0) 'Val(txtint.Text.Trim))
                alParaval.Add(0) 'Val(TXTADVANCE.Text.Trim))
                alParaval.Add(0) 'Val(TXTBALANCE.Text.Trim))
                alParaval.Add(0) 'CMBSALESMAN.Text.Trim)
                alParaval.Add(0) 'CMBPACKINGTYPE.Text.Trim)
                alParaval.Add("") 'CMBFORWARD.Text.Trim)



                alParaval.Add(CmpId)
                alParaval.Add(Locationid)
                alParaval.Add(Userid)
                alParaval.Add(YearId)
                alParaval.Add(0)

                alParaval.Add(GRIDSRNO)
                alParaval.Add(MERCHANT)
                alParaval.Add(QUALITY)
                alParaval.Add(DESIGN)
                alParaval.Add(gridremarks)
                alParaval.Add(COLOR)
                alParaval.Add(PARTYPONO)
                alParaval.Add(qty)
                alParaval.Add(QTYUNIT)
                alParaval.Add(CUT)
                alParaval.Add(MTRS)
                alParaval.Add(RATE)
                alParaval.Add(PER)
                alParaval.Add(AMOUNT)
                alParaval.Add(RECDQTY)
                alParaval.Add(RECDMTRS)
                alParaval.Add(DONE)
                alParaval.Add(SAMPLEDONE)
                alParaval.Add(CLOSED)

                alParaval.Add("") 'CMBSAMPLE.Text.Trim)

                alParaval.Add("") 'CMBFROMCITY.Text.Trim)

                alParaval.Add(0)


                Dim objclsPurord As New ClsAgencySaleOrder()
                objclsPurord.alParaval = alParaval

                Dim DT As DataTable = objclsPurord.SAVE()


            Next
            MessageBox.Show("Details Added")


            CLEAR()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CLEAR()
        txtsrno.Text = 1
        CMBBUYERS.Text = ""
        CMBSELLERS.Text = ""
        CMBDESIGN.Text = ""
        cmbitemname.Text = ""
        cmbqtyunit.Text = ""
        TXTCRDAYS.Clear()
        TXTCUT.Clear()
        TXTDELPERIOD.Clear()
        TXTDISCOUNT.Clear()
        TXTMTRS.Clear()
        TXTORDERNO.Clear()
        txtQTY.Clear()
        TXTRATE.Clear()
        TXTREMARKS.Clear()
        GRIDSO.RowCount = 0
        CMBBUYERS.Focus()
    End Sub
    Private Function ERRORVALID() As Boolean

    End Function

    Private Sub MagicBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE ORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            CLEAR()
            If GRIDSO.RowCount > 0 Then
                txtsrno.Text = Val(GRIDSO.Rows(GRIDSO.RowCount - 1).Cells(0).Value) + 1
            Else
                txtsrno.Text = 1
            End If
            'getmax_no()
            CMBBUYERS.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTDELPERIOD_Validated(sender As Object, e As EventArgs) Handles TXTDELPERIOD.Validated
        Try
            If ORDERDATE.Text <> "__/__/____" Then
                If Val(TXTDELPERIOD.Text.Trim) > 0 Then duedate.Value = Convert.ToDateTime(ORDERDATE.Text).Date.AddDays(Val(TXTDELPERIOD.Text.Trim))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELLERS_Enter(sender As Object, e As EventArgs) Handles CMBSELLERS.Enter
        Try
            If CMBSELLERS.Text.Trim = "" Then FILLNAME(CMBSELLERS, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELLERS_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBSELLERS.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = "  And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')   AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBSELLERS.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELLERS_Validating(sender As Object, e As CancelEventArgs) Handles CMBSELLERS.Validating
        Try
            If CMBSELLERS.Text.Trim <> "" Then NAMEVALIDATE(CMBSELLERS, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex

        End Try
    End Sub
    Private Sub CMBBUYERS_Validating(sender As Object, e As CancelEventArgs) Handles CMBBUYERS.Validating
        Try
            If CMBBUYERS.Text.Trim <> "" Then
                NAMEVALIDATE(CMBBUYERS, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBBUYERS_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBBUYERS.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBBUYERS.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBUYERS_Enter(sender As Object, e As EventArgs) Handles CMBBUYERS.Enter
        Try
            If CMBBUYERS.Text.Trim = "" Then FILLNAME(CMBBUYERS, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Enter(sender As Object, e As EventArgs) Handles cmbitemname.Enter
        Try
            If cmbitemname.Text.Trim = "" Then fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validating(sender As Object, e As CancelEventArgs) Handles cmbitemname.Validating
        Try
            If cmbitemname.Text.Trim <> "" Then itemvalidate(cmbitemname, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
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

    Private Sub CMBDESIGN_Enter(sender As Object, e As EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBDESIGN.KeyDown
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

    Private Sub CMBDESIGN_Validating(sender As Object, e As CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Validating(sender As Object, e As CancelEventArgs) Handles cmbqtyunit.Validating
        Try
            If cmbqtyunit.Text.Trim <> "" Then unitvalidate(cmbqtyunit, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Enter(sender As Object, e As EventArgs) Handles cmbqtyunit.Enter
        Try
            If cmbqtyunit.Text.Trim = "" Then fillunit(cmbqtyunit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLCMB()
        Try
            fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
            fillunit(cmbqtyunit)
            If CMBSELLERS.Text.Trim = "" Then FILLNAME(CMBSELLERS, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            If CMBBUYERS.Text.Trim = "" Then FILLNAME(CMBBUYERS, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillgrid()
        GRIDSO.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDSO.Rows.Add(Val(txtsrno.Text.Trim), Val(TXTNO.Text.Trim), ORDERDATE.Text.Trim, CMBBUYERS.Text.Trim, CMBSELLERS.Text.Trim, TXTCRDAYS.Text.Trim, TXTDISCOUNT.Text.Trim, TXTDELPERIOD.Text.Trim, duedate.Text.Trim, TXTORDERNO.Text.Trim, cmbitemname.Text.Trim, CMBDESIGN.Text.Trim, Format(Val(txtQTY.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), TXTREMARKS.Text.Trim)
            getsrno(GRIDSO)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDSO.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDSO.Item(GNO.Index, TEMPROW).Value = Val(TXTNO.Text.Trim)
            GRIDSO.Item(GDATE.Index, TEMPROW).Value = ORDERDATE.Text.Trim
            GRIDSO.Item(GBUYERS.Index, TEMPROW).Value = CMBBUYERS.Text.Trim
            GRIDSO.Item(GSELLERS.Index, TEMPROW).Value = CMBSELLERS.Text.Trim
            GRIDSO.Item(GCRDAYS.Index, TEMPROW).Value = TXTCRDAYS.Text.Trim
            GRIDSO.Item(GDISCOUNT.Index, TEMPROW).Value = TXTDISCOUNT.Text.Trim
            GRIDSO.Item(GDELPERIOD.Index, TEMPROW).Value = TXTDELPERIOD.Text.Trim
            GRIDSO.Item(GDUEDATE.Index, TEMPROW).Value = duedate.Text.Trim
            GRIDSO.Item(GORDERNO.Index, TEMPROW).Value = TXTORDERNO.Text.Trim
            GRIDSO.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
            GRIDSO.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDSO.Item(gQty.Index, TEMPROW).Value = Format(Val(txtQTY.Text.Trim), "0.00")
            GRIDSO.Item(gqtyunit.Index, TEMPROW).Value = cmbqtyunit.Text.Trim
            GRIDSO.Item(gcut.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
            GRIDSO.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDSO.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")
            GRIDSO.Item(GREMARKS.Index, TEMPROW).Value = TXTREMARKS.Text.Trim

            GRIDDOUBLECLICK = False
        End If

        GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1
        txtsrno.Text = GRIDSO.RowCount + 1
        TXTNO.Text = Val(TXTNO.Text) + 1
        'CLEAR()
        CMBBUYERS.Text = ""
        CMBSELLERS.Text = ""
        CMBDESIGN.Text = ""
        cmbitemname.Text = ""
        cmbqtyunit.Text = ""
        TXTCRDAYS.Clear()
        TXTCUT.Clear()
        TXTDELPERIOD.Clear()
        TXTDISCOUNT.Clear()
        TXTMTRS.Clear()
        TXTORDERNO.Clear()
        txtQTY.Clear()
        TXTRATE.Clear()
        TXTREMARKS.Clear()
        ORDERDATE.Focus()
    End Sub

    Private Sub cmdEXIT_Click(sender As Object, e As EventArgs) Handles cmdEXIT.Click
        Me.Close()
    End Sub
    'Sub getmax_no()
    '    Dim DTTABLE As New DataTable
    '    DTTABLE = getmax("  isnull(max(ASO_no),0) + 1 ", "AGENCYSALEORDER", " AND ASO_cmpid=" & CmpId & " and ASO_locationid=" & Locationid & " and ASO_yearid=" & YearId)
    '    If DTTABLE.Rows.Count > 0 Then
    '        TXTNO.Text = DTTABLE.Rows(0).Item(0)
    '    End If
    'End Sub

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTREMARKS_Validated(sender As Object, e As EventArgs) Handles TXTREMARKS.Validated
        Try
            If CMBBUYERS.Text <> "" And CMBSELLERS.Text <> "" And cmbitemname.Text <> "" And cmbqtyunit.Text <> "" Then
                If TXTMTRS.Text.Trim <> "" Then
                    fillgrid()
                Else
                    MsgBox("Please Enter Detail Properly.", MsgBoxStyle.Critical)

                End If


            Else
                MsgBox("Please Enter Details Properly.", MsgBoxStyle.Critical)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
    End Sub
End Class