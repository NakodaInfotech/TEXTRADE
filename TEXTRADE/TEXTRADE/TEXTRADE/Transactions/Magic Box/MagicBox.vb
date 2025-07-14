Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms
Imports BL
Public Class MagicBox
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim IntResult As Integer

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If
            Dim alParaval As New ArrayList
            alParaval.Add(Val(TXTNO.Text.Trim))
            alParaval.Add(Format(Convert.ToDateTime(ORDERDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBBUYERS.Text.Trim)
            alParaval.Add("") ' HASTE
            alParaval.Add("") 'AGENT

            alParaval.Add(TXTORDERNO) 'PONO
            alParaval.Add(duedate) 'DueDate.Value)
            alParaval.Add("") 'cmbtrans.Text.Trim)
            alParaval.Add("") 'cmbtrans2.Text.Trim)
            alParaval.Add("") 'cmbcity.Text.Trim)
            alParaval.Add("") 'TXTREFNO.Text.Trim)

            alParaval.Add("") 'CMBRISK.Text.Trim)
            alParaval.Add("") 'TXTCONSIGNOR.Text.Trim)
            alParaval.Add("") 'TXTCONSIGNEE.Text.Trim)
            alParaval.Add(CMBSELLERS.Text.Trim)
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

            alParaval.Add(TXTREMARKS.Text.Trim)
            alParaval.Add("") 'txtnote.Text.Trim)
            alParaval.Add("") 'txttnc.Text.Trim)


            alParaval.Add("") 'cmbmisc.Text.Trim)
            alParaval.Add(Val(0)) 'txtDiscrate.Text.Trim))
            alParaval.Add(0) 'Val(txtDiscLot.Text.Trim))
            alParaval.Add(0) 'Val(txtdd.Text.Trim))
            alParaval.Add(0) 'Val(txtkatai.Text.Trim))
            alParaval.Add(Val(TXTDISCOUNT.Text.Trim))
            alParaval.Add(0) 'Val(txtadat.Text.Trim))
            alParaval.Add(Val(TXTCRDAYS.Text.Trim))
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

            Dim GRIDSRNO As String = ""
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

            For Each row As Windows.Forms.DataGridViewRow In GRIDSO.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = 1 'row.Cells(gsrno.Index).Value.ToString
                        MERCHANT = row.Cells(gitemname.Index).Value.ToString
                        QUALITY = "" 'row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        gridremarks = "" ' row.Cells(gdesc.Index).Value.ToString
                        COLOR = "" 'row.Cells(gcolor.Index).Value.ToString
                        PARTYPONO = "" ' row.Cells(GPARTYPONO.Index).Value.ToString
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

                    Else
                        GRIDSRNO = GRIDSRNO & "|" & 2 'row.Cells(gsrno.Index).Value.ToString
                        MERCHANT = MERCHANT & "|" & row.Cells(gitemname.Index).Value.ToString
                        QUALITY = QUALITY & "|" & "" 'row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        gridremarks = gridremarks & "|" & "" 'row.Cells(gdesc.Index).Value.ToString
                        COLOR = COLOR & "|" & "" 'row.Cells(gcolor.Index).Value.ToString
                        PARTYPONO = PARTYPONO & "|" & "" 'row.Cells(GPARTYPONO.Index).Value.ToString
                        qty = qty & "|" & row.Cells(gQty.Index).Value.ToString
                        QTYUNIT = QTYUNIT & "|" & row.Cells(gqtyunit.Index).Value.ToString
                        CUT = CUT & "|" & row.Cells(gcut.Index).Value
                        MTRS = MTRS & "|" & row.Cells(GMTRS.Index).Value
                        RATE = RATE & "|" & row.Cells(GRATE.Index).Value
                        PER = PER & "|" & "" 'row.Cells(GPER.Index).Value
                        AMOUNT = AMOUNT & "|" & "" 'row.Cells(GAMOUNT.Index).Value
                        RECDQTY = RECDQTY & "|" & "" 'Val(row.Cells(GRECDQTY.Index).Value)
                        RECDMTRS = RECDMTRS & "|" & "" 'Val(row.Cells(GRECDMTRS.Index).Value)

                        DONE = DONE & "|" & "0" 'If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then DONE = DONE & "|" & "1" Else DONE = DONE & "|" & "0"
                        SAMPLEDONE = SAMPLEDONE & "|" & "0" 'If Convert.ToBoolean(row.Cells(GSAMPLEDONE.Index).Value) = True Then SAMPLEDONE = SAMPLEDONE & "|" & "1" Else SAMPLEDONE = SAMPLEDONE & "|" & "0"
                        CLOSED = CLOSED & "|" & "0" 'If Convert.ToBoolean(row.Cells(GCLOSED.Index).Value) = True Then CLOSED = CLOSED & "|" & "1" Else CLOSED = CLOSED & "|" & "0"

                    End If
                End If
            Next

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


            'If EDIT = False Then
            '    If USERADD = False Then
            '        MsgBox("Insufficient Rights")
            '        Exit Sub
            '    End If
            Dim DT As DataTable = objclsPurord.SAVE()
            MessageBox.Show("Details Added")
            TXTNO.Text = DT.Rows(0).Item(0)
            'Else
            '    alParaval.Add(TEMPSONO)
            '    If USEREDIT = False Then
            '        MsgBox("Insufficient Rights")
            '        Exit Sub
            '    End If

            '    IntResult = objclsPurord.UPDATE()
            '    MessageBox.Show("Details Updated")
            'End If

            'If ClientName = "YASHVI" Then SMSCODE()

            ''THEY DONT NEED PRINT
            'If ClientName <> "SNCM" Then
            '    If MsgBox("Wish to Print Order?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then PRINTREPORT()
            'End If


            ''IF ADVANCE IS RECD FROM CLIENT THEN OPEN RECEIPT FORM AUTO
            ''USER WILL SAVE IT MANUALLY
            'If EDIT = False And (ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV") And Val(TXTADVANCE.Text.Trim) > 0 Then
            '    Dim OBJREC As New Receipt
            '    OBJREC.TEMPAUTOENTRY = True
            '    OBJREC.TEMPAMT = Val(TXTADVANCE.Text.Trim)
            '    OBJREC.TEMPBILLNO = "ORDER NO - " & Val(TXTSONO.Text.Trim)
            '    OBJREC.TEMPNAME = CMBBUYERS.Text.Trim
            '    OBJREC.MdiParent = MDIMain
            '    OBJREC.Show()
            'End If

            'EDIT = False
            CLEAR()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CLEAR()

    End Sub
    Private Function ERRORVALID() As Boolean

    End Function

    Private Sub MagicBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
    Sub fillgrid(MATCHING As String)
        GRIDSO.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDSO.Rows.Add(Val(txtsrno.Text.Trim), Val(TXTNO.Text.Trim), ORDERDATE, CMBBUYERS.Text.Trim, CMBSELLERS.Text.Trim, TXTCRDAYS.Text.Trim, TXTDISCOUNT.Text.Trim, TXTDELPERIOD.Text.Trim, duedate, TXTORDERNO.Text.Trim, cmbitemname.Text.Trim, CMBDESIGN.Text.Trim, Format(Val(txtQTY.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), TXTREMARKS.Text.Trim)
            getsrno(GRIDSO)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDSO.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDSO.Item(GNO.Index, TEMPROW).Value = Val(TXTNO.Text.Trim)
            GRIDSO.Item(GDATE.Index, TEMPROW).Value = ORDERDATE.Text.Trim
            GRIDSO.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
            GRIDSO.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDSO.Item(gQty.Index, TEMPROW).Value = Format(Val(txtQTY.Text.Trim), "0.00")
            GRIDSO.Item(gqtyunit.Index, TEMPROW).Value = cmbqtyunit.Text.Trim
            GRIDSO.Item(gcut.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
            GRIDSO.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDSO.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")

            GRIDDOUBLECLICK = False
        End If

        GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1

        txtsrno.Text = GRIDSO.RowCount + 1
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

End Class