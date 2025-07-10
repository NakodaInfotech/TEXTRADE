﻿
Imports BL
Imports System.ComponentModel
Imports System.IO

Public Class SaleOrder

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim PARTYPONO As String
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPVERIFIED As Boolean = False  'THIS IS USED TO CHECK WHETHER THE ORDER WAS ALREADY VERIFIED OR NOT, ON EDIT MODE, USED ON ERRORVALID FOR SNCM
    Public TEMPSONO As String
    Dim tempMsg As Integer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        fillcmb()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim IntResult As Integer

            'CALL TOTAL HERE, DONE BY GULKIT
            TOTAL()

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            If TXTSONO.ReadOnly = True Then
                alParaval.Add(0)
            Else
                alParaval.Add(Val(TXTSONO.Text.Trim))
            End If

            alParaval.Add(Format(Convert.ToDateTime(SODATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(CMBHASTE.Text.Trim)
            alParaval.Add(CMBAGENT.Text.Trim)

            alParaval.Add(txtpono.Text.Trim)
            alParaval.Add(DUEDATE.Value)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(cmbtrans2.Text.Trim)
            alParaval.Add(cmbcity.Text.Trim)
            alParaval.Add(TXTREFNO.Text.Trim)

            alParaval.Add(CMBRISK.Text.Trim)
            alParaval.Add(TXTCONSIGNOR.Text.Trim)
            alParaval.Add(TXTCONSIGNEE.Text.Trim)
            alParaval.Add(CMBPACKING.Text.Trim)
            alParaval.Add(CMBCURRENCY.Text.Trim)
            alParaval.Add(lbltotalqty.Text.Trim)
            alParaval.Add(LBLTOTALMTRS.Text.Trim)
            alParaval.Add(lbltotalbale.Text.Trim)       '' *** TOTAL BALE INSTED OF TOTAL AMT.
            alParaval.Add(lbltotalamt.Text.Trim)

            alParaval.Add(txtdisper.Text.Trim)
            alParaval.Add(txtdisamt.Text.Trim)
            alParaval.Add(txtpfper.Text.Trim)
            alParaval.Add(txtpfamt.Text.Trim)
            alParaval.Add(txttestchgs.Text.Trim)
            alParaval.Add(txtnett.Text.Trim)
            alParaval.Add(Val(TXTEXCISEID.Text.Trim))
            alParaval.Add(TXTEXCISE.Text.Trim)
            alParaval.Add(txtexciseAMT.Text.Trim)
            alParaval.Add(TXTEDUCESS.Text.Trim)
            alParaval.Add(txteducessAMT.Text.Trim)
            alParaval.Add(TXTHSECESS.Text.Trim)
            alParaval.Add(txthsecessAMT.Text.Trim)
            alParaval.Add(txtsubtotal.Text.Trim)

            alParaval.Add(cmbtax.Text.Trim)
            alParaval.Add(txttax.Text.Trim)
            alParaval.Add(cmbaddtax.Text.Trim)
            alParaval.Add(txtaddtax.Text.Trim)
            alParaval.Add(txtfrper.Text.Trim)
            alParaval.Add(txtfreight.Text.Trim)
            alParaval.Add(cmboctroi.Text.Trim)
            alParaval.Add(txtoctroi.Text.Trim)
            alParaval.Add(txtinspchgs.Text.Trim)
            alParaval.Add(txtroundoff.Text.Trim)
            alParaval.Add(txtgrandtotal.Text.Trim)


            alParaval.Add(txtinwords.Text.Trim)

            alParaval.Add(TXTREMARKS.Text.Trim)
            alParaval.Add(txtnote.Text.Trim)
            alParaval.Add(txttnc.Text.Trim)


            alParaval.Add(cmbmisc.Text.Trim)
            alParaval.Add(Val(txtDiscrate.Text.Trim))
            alParaval.Add(Val(txtDiscLot.Text.Trim))
            alParaval.Add(Val(txtdd.Text.Trim))
            alParaval.Add(Val(txtkatai.Text.Trim))
            alParaval.Add(Val(txtcd.Text.Trim))
            alParaval.Add(Val(txtadat.Text.Trim))
            alParaval.Add(Val(txtdays.Text.Trim))
            alParaval.Add(Val(txtint.Text.Trim))
            alParaval.Add(Val(TXTADVANCE.Text.Trim))
            alParaval.Add(Val(TXTBALANCE.Text.Trim))
            alParaval.Add(CMBSALESMAN.Text.Trim)
            alParaval.Add(CMBPACKINGTYPE.Text.Trim)
            alParaval.Add(CMBFORWARD.Text.Trim)




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
                        GRIDSRNO = row.Cells(gsrno.Index).Value.ToString
                        MERCHANT = row.Cells(gitemname.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        gridremarks = row.Cells(gdesc.Index).Value.ToString
                        COLOR = row.Cells(gcolor.Index).Value.ToString
                        PARTYPONO = row.Cells(GPARTYPONO.Index).Value.ToString
                        qty = row.Cells(gQty.Index).Value.ToString
                        QTYUNIT = row.Cells(gqtyunit.Index).Value.ToString
                        CUT = row.Cells(gcut.Index).Value
                        MTRS = row.Cells(GMTRS.Index).Value
                        RATE = row.Cells(GRATE.Index).Value
                        PER = row.Cells(GPER.Index).Value
                        AMOUNT = row.Cells(GAMOUNT.Index).Value
                        RECDQTY = Val(row.Cells(GRECDQTY.Index).Value)
                        RECDMTRS = Val(row.Cells(GRECDMTRS.Index).Value)

                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then DONE = 1 Else DONE = 0
                        If Convert.ToBoolean(row.Cells(GSAMPLEDONE.Index).Value) = True Then SAMPLEDONE = 1 Else SAMPLEDONE = 0
                        If Convert.ToBoolean(row.Cells(GCLOSED.Index).Value) = True Then CLOSED = 1 Else CLOSED = 0

                    Else
                        GRIDSRNO = GRIDSRNO & "|" & row.Cells(gsrno.Index).Value.ToString
                        MERCHANT = MERCHANT & "|" & row.Cells(gitemname.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        gridremarks = gridremarks & "|" & row.Cells(gdesc.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        PARTYPONO = PARTYPONO & "|" & row.Cells(GPARTYPONO.Index).Value.ToString
                        qty = qty & "|" & row.Cells(gQty.Index).Value.ToString
                        QTYUNIT = QTYUNIT & "|" & row.Cells(gqtyunit.Index).Value.ToString
                        CUT = CUT & "|" & row.Cells(gcut.Index).Value
                        MTRS = MTRS & "|" & row.Cells(GMTRS.Index).Value
                        RATE = RATE & "|" & row.Cells(GRATE.Index).Value
                        PER = PER & "|" & row.Cells(GPER.Index).Value
                        AMOUNT = AMOUNT & "|" & row.Cells(GAMOUNT.Index).Value
                        RECDQTY = RECDQTY & "|" & Val(row.Cells(GRECDQTY.Index).Value)
                        RECDMTRS = RECDMTRS & "|" & Val(row.Cells(GRECDMTRS.Index).Value)

                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then DONE = DONE & "|" & "1" Else DONE = DONE & "|" & "0"
                        If Convert.ToBoolean(row.Cells(GSAMPLEDONE.Index).Value) = True Then SAMPLEDONE = SAMPLEDONE & "|" & "1" Else SAMPLEDONE = SAMPLEDONE & "|" & "0"
                        If Convert.ToBoolean(row.Cells(GCLOSED.Index).Value) = True Then CLOSED = CLOSED & "|" & "1" Else CLOSED = CLOSED & "|" & "0"

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

            alParaval.Add(CMBSAMPLE.Text.Trim)

            alParaval.Add(CMBFROMCITY.Text.Trim)
            If CHKVERIFY.CheckState = CheckState.Checked Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(CMBORDERON.Text.Trim)

            Dim objclsPurord As New ClsSaleOrder()
            objclsPurord.alParaval = alParaval


            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = objclsPurord.SAVE()
                MessageBox.Show("Details Added")
                TXTSONO.Text = DT.Rows(0).Item(0)
            Else
                alParaval.Add(TEMPSONO)
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                IntResult = objclsPurord.UPDATE()
                MessageBox.Show("Details Updated")
            End If

            If ClientName = "YASHVI" Then SMSCODE()

            'THEY DONT NEED PRINT
            If ClientName <> "SNCM" Then
                If MsgBox("Wish to Print Order?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then PRINTREPORT()
            End If


            'IF ADVANCE IS RECD FROM CLIENT THEN OPEN RECEIPT FORM AUTO
            'USER WILL SAVE IT MANUALLY
            If EDIT = False And (ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV") And Val(TXTADVANCE.Text.Trim) > 0 Then
                Dim OBJREC As New Receipt
                OBJREC.TEMPAUTOENTRY = True
                OBJREC.TEMPAMT = Val(TXTADVANCE.Text.Trim)
                OBJREC.TEMPBILLNO = "ORDER NO - " & Val(TXTSONO.Text.Trim)
                OBJREC.TEMPNAME = cmbname.Text.Trim
                OBJREC.MdiParent = MDIMain
                OBJREC.Show()
            End If

            EDIT = False
            CLEAR()
            If ClientName = "SNCM" Then TXTSONO.Focus() Else cmbname.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEXIT.Click
        Me.Close()
    End Sub

    Sub fillgrid(MATCHING As String)
        GRIDSO.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDSO.Rows.Add(Val(txtsrno.Text.Trim), cmbitemname.Text.Trim, CMBQUALITY.Text.Trim, CMBDESIGN.Text.Trim, txtgridremarks.Text.Trim, MATCHING, TXTPARTYPONO.Text.Trim, Format(Val(txtQTY.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Format(Val(TXTAMOUNT.Text.Trim), "0.00"), 0, 0, 0, 0, 0)
            getsrno(GRIDSO)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDSO.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDSO.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
            GRIDSO.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDSO.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDSO.Item(gdesc.Index, TEMPROW).Value = txtgridremarks.Text.Trim
            GRIDSO.Item(gcolor.Index, TEMPROW).Value = cmbcolor.Text.Trim
            GRIDSO.Item(GPARTYPONO.Index, TEMPROW).Value = TXTPARTYPONO.Text.Trim
            GRIDSO.Item(gQty.Index, TEMPROW).Value = Format(Val(txtQTY.Text.Trim), "0.00")
            GRIDSO.Item(gqtyunit.Index, TEMPROW).Value = cmbqtyunit.Text.Trim
            GRIDSO.Item(gcut.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
            GRIDSO.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDSO.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")
            GRIDSO.Item(GPER.Index, TEMPROW).Value = CMBPER.Text.Trim
            GRIDSO.Item(GAMOUNT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text.Trim), "0.00")

            GRIDDOUBLECLICK = False
        End If

        GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1

        txtsrno.Text = GRIDSO.RowCount + 1

        If ClientName = "AVIS" Or ClientName = "SOFTAS" Then
            If cmbcolor.SelectedIndex < cmbcolor.Items.Count - 1 Then cmbcolor.SelectedIndex = cmbcolor.SelectedIndex + 1 Else cmbcolor.Text = ""
        Else
            cmbcolor.Text = ""
        End If
        If ClientName <> "SIDDHGIRI" Then TXTPARTYPONO.Clear()
        If ClientName <> "KOTHARI" And ClientName <> "KOTHARINEW" And ClientName <> "SOFTAS" And ClientName <> "SIDDHGIRI" Then txtQTY.Clear()
        If ClientName <> "SOFTAS" And ClientName <> "SIDDHGIRI" Then TXTMTRS.Clear()

        CMBPER.Text = "Mtrs"
        TXTAMOUNT.Clear()
        If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then cmbqtyunit.Text = "Pcs"
        If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then CMBPER.Text = "Qty"



        If ClientName = "YASHVI" Or ClientName = "SUPEEMA" Then
            CMBQUALITY.Text = ""
            CMBDESIGN.Text = ""
            txtgridremarks.Clear()
            CMBGRIDREMARKS.Text = ""
            If ClientName <> "SUPEEMA" Then TXTRATE.Clear()
            cmbitemname.Focus()
        Else
            If ClientName <> "KOTHARI" And ClientName <> "KOTHARINEW" And ClientName <> "SOFTAS" And ClientName <> "SIDDHGIRI" Then TXTCUT.Clear()
            cmbcolor.Focus()
        End If
        If ClientName = "SHREEVALLABH" Or ClientName = "RAJDEEP" Or ClientName = "KRISHNA" Or ClientName = "SIDDHGIRI" Or ClientName = "SNCM" Or ClientName = "REALCORPORATION" Then CMBDESIGN.Focus()

        If ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "SUPRIYA" Or ClientName = "SMS" Or ClientName = "SONU" Or ClientName = "MNARESH" Or ClientName = "SIDDHGIRI" Or ClientName = "MASHOK" Or ClientName = "ABHEE" Then
            TXTRATE.Clear()
            If ClientName = "MASHOK" Or ClientName = "ABHEE" Then
                cmbitemname.Text = ""
                txtnote.Focus()
            Else
                cmbitemname.Focus()
            End If
        End If

    End Sub

    Sub CLEAR()
        Try
            cmbname.Enabled = True
            CMBPACKING.Enabled = True
            TEMPVERIFIED = False

            CMBCMPNAME.Text = ""
            TXTCMPSONO.Clear()

            tstxtbillno.Clear()
            TXTMOBILENO.Clear()
            CMBPACKINGTYPE.Text = ""
            LBLPACKINGTYPE.Text = ""
            LBLRATE.Text = "0.00"
            SODATE.Text = Now.Date
            cmbname.Text = ""
            cmbname.Enabled = True
            LBLGSTIN.Text = ""
            CMBHASTE.Text = ""
            CMBAGENT.Text = ""
            CMBCURRENCY.Text = ""
            CMBRISK.Text = ""
            CMBSALESMAN.Text = ""
            If ClientName = "SNCM" Then
                CMBFORWARD.Text = "READY"
            Else
                CMBFORWARD.Text = ""
            End If
            CMBORDERON.Text = ""
            CHKFETCHDESC.CheckState = CheckState.Unchecked

            txtpono.Clear()
            DUEDATE.Value = Now.Date
            deldate.Value = Now.Date
            txtcrdays.Clear()
            txtDeliveryadd.Clear()
            TXTCOPYSONO.Clear()

            CMBSAMPLE.SelectedIndex = 0
            CMBPACKING.Text = ""

            cmbtrans.Text = ""
            cmbtrans2.Text = ""
            cmbcity.Text = ""
            TXTREFNO.Clear()

            CMBFROMCITY.Text = ""

            If ClientName <> "SNCM" Then
                CHKVERIFY.CheckState = CheckState.Checked
            Else
                CHKVERIFY.CheckState = CheckState.Unchecked
            End If

            If ClientName <> "CC" And ClientName <> "C3" And ClientName <> "SHREEDEV" Then TXTCONSIGNOR.Text = CmpName
            TXTCONSIGNEE.Clear()

            cmbmisc.Text = ""
            txtDiscrate.Clear()
            txtDiscLot.Clear()
            txtadd.Clear()
            txtkatai.Clear()
            txtcd.Clear()
            txtadat.Clear()
            txtdays.Clear()
            txtint.Clear()
            txtdd.Clear()


            TXTAMOUNT.Clear()
            TXTRATE.Clear()
            lbltotalbale.Text = ""

            txtsrno.Clear()
            CMBITEMCODE.Text = ""
            cmbitemname.Text = ""
            CMBQUALITY.Text = ""
            CMBDESIGN.Text = ""
            txtgridremarks.Clear()
            CMBGRIDREMARKS.Text = ""
            cmbcolor.Text = ""
            TXTPARTYPONO.Clear()
            txtQTY.Clear()
            If ClientName = "YASHVI" Then
                cmbqtyunit.Text = "THAN"
                CHKFETCHDESC.CheckState = CheckState.Checked
            ElseIf ClientName = "AVIS" Then
                cmbqtyunit.Text = "Mtrs"
            ElseIf ClientName = "KRISHNA" Or ClientName = "SONU" Or ClientName = "SOFTAS" Or ClientName = "ANOX" Or ClientName = "MAHAVIRPOLYCOT" Then
                cmbqtyunit.Text = "ROLL"
            Else
                cmbqtyunit.Text = "PCS"
            End If

            If ClientName = "MASHOK" Then TXTCUT.Text = 110 Else TXTCUT.Clear()
            TXTMTRS.Clear()
            TXTRATE.Clear()
            CMBPER.Text = "Mtrs"
            TXTAMOUNT.Clear()

            cmbtrans.Text = ""
            cmbtrans2.Text = ""
            txtlrno.Clear()
            cmbcity.Text = ""
            txttransremarks.Clear()
            txttransref.Clear()
            cmbmisc.Text = ""


            EP.Clear()
            lbllocked.Visible = False
            PBlock.Visible = False
            LBLCLOSED.Visible = False
            LBLSMS.Visible = False
            LBLTOTALMTRS.Text = "0.00"

            TXTREMARKS.Clear()
            txtinwordedu.Clear()
            txtinwordexcise.Clear()
            txtinwordhse.Clear()
            txtinwords.Clear()

            txtadd.Clear()
            txtnote.Clear()
            txttnc.Clear()

            GRIDSO.RowCount = 0
            getmax_SO_no()
            GRIDDOUBLECLICK = False


            txtbillamt.Text = 0.0
            txtdisper.Text = 0.0
            txtdisamt.Text = 0.0
            txtpfper.Text = 0.0
            txtpfamt.Text = 0.0
            txttestchgs.Text = 0.0
            txtnett.Text = 0.0

            TXTEXCISE.Text = 0.0
            txtexciseAMT.Text = 0.0

            TXTEDUCESS.Text = 0.0
            txteducessAMT.Text = 0.0

            TXTHSECESS.Text = 0.0
            txthsecessAMT.Text = 0.0

            txtsubtotal.Text = 0.0

            cmbtax.Text = ""
            txttax.Text = 0.0

            cmbaddtax.Text = ""
            txtaddtax.Text = 0.0

            txtfrper.Text = 0.0
            txtfreight.Text = 0.0

            cmboctroi.Text = ""
            txtoctroi.Text = 0.0

            txtinspchgs.Text = 0.0
            txtgrandtotal.Text = 0.0
            txtroundoff.Text = 0.0
            TXTADVANCE.Clear()
            TXTBALANCE.Clear()
            TXTREMARKS.Clear()

            chkexcise.Checked = False

            cmbgodown1.Text = ""
            cmbgodown2.Text = ""

            TXTSTOCKITEMNAME.Clear()
            TXTPCSINHOUSE.Clear()
            TXTPCSJOBBER.Clear()
            TXTPCSPENDINGPO.Clear()
            TXTMTRSINHOUSE.Clear()
            TXTMTRSJOBBER.Clear()
            TXTMTRSPENDINGPO.Clear()
            TXTMTRSBAL.Clear()
            TXTPCSBAL.Clear()
            LBLWHATSAPP.Visible = False

            TXTSONO.ReadOnly = False

            If GRIDSO.RowCount > 0 Then
                txtsrno.Text = Val(GRIDSO.Rows(GRIDSO.RowCount - 1).Cells(0).Value) + 1
            Else
                txtsrno.Text = 1
            End If

            If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then cmbqtyunit.Text = "Pcs"
            If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then CMBPER.Text = "Qty"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getmax_SO_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(SO_no),0) + 1 ", "SALEORDER", " AND SO_cmpid=" & CmpId & " and SO_locationid=" & Locationid & " and SO_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            TXTSONO.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Sub TOTAL()

        If GRIDSO.RowCount > 0 Then

            txtbillamt.Text = 0.0
            txtdisamt.Text = 0.0
            txtpfamt.Text = 0.0
            txtnett.Text = 0.0
            lbltotalqty.Text = 0.0
            LBLTOTALMTRS.Text = 0.0
            lbltotalbale.Text = 0

            lbltotalamt.Text = 0.0
            lbltotalqty.Text = 0.0
            lbltotalbale.Text = 0.0

            txtexciseAMT.Text = 0.0
            txteducessAMT.Text = 0.0
            txthsecessAMT.Text = 0.0

            txtsubtotal.Text = 0.0
            txttax.Text = 0.0
            txtaddtax.Text = 0.0
            If Val(txtfrper.Text.Trim) > 0 Then txtfreight.Text = 0.0
            txtoctroi.Text = 0.0

            txtroundoff.Text = 0.0
            txtgrandtotal.Text = 0.0

            For Each row As DataGridViewRow In GRIDSO.Rows

                If Val(row.Cells(gcut.Index).EditedFormattedValue) > 0 Then row.Cells(GMTRS.Index).Value = Format(Val(row.Cells(gcut.Index).EditedFormattedValue) * Val(row.Cells(gQty.Index).EditedFormattedValue), "0.00")
                If Val(row.Cells(gQty.Index).EditedFormattedValue) > 0 Then lbltotalqty.Text = Format(Val(lbltotalqty.Text) + Val(row.Cells(gQty.Index).EditedFormattedValue), "0.00")
                If Val(row.Cells(GMTRS.Index).EditedFormattedValue) > 0 Then LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(row.Cells(GMTRS.Index).EditedFormattedValue), "0.00")

                If row.Cells(GPER.Index).EditedFormattedValue = "Mtrs" Then
                    If Val(row.Cells(GRATE.Index).EditedFormattedValue) > 0 Then row.Cells(GAMOUNT.Index).Value = Format(Val(row.Cells(GRATE.Index).EditedFormattedValue) * Val(row.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                ElseIf row.Cells(GPER.Index).EditedFormattedValue = "Qty" Then
                    If Val(row.Cells(GRATE.Index).EditedFormattedValue) > 0 Then row.Cells(GAMOUNT.Index).Value = Format(Val(row.Cells(GRATE.Index).EditedFormattedValue) * Val(row.Cells(gQty.Index).EditedFormattedValue), "0.00")
                End If
                If Val(row.Cells(GAMOUNT.Index).EditedFormattedValue) > 0 Then lbltotalamt.Text = Format(Val(lbltotalamt.Text) + Val(row.Cells(GAMOUNT.Index).EditedFormattedValue), "0.00")
            Next

            lbltotalbale.Text = Val(GRIDSO.RowCount)

            txtbillamt.Text = Val(lbltotalamt.Text.Trim)
            txtdisamt.Text = (Val(txtdisper.Text) * Val(txtbillamt.Text)) / 100
            txtpfamt.Text = (Val(txtpfper.Text) * Val(txtbillamt.Text)) / 100

            txtnett.Text = (txtbillamt.Text) - Val(txtdisamt.Text) + Val(txtpfamt.Text) + Val(txttestchgs.Text)

            txtexciseAMT.Text = (Val(TXTEXCISE.Text) * Val(txtnett.Text)) / 100
            txteducessAMT.Text = Val((Val(TXTEDUCESS.Text) * Val(txtexciseAMT.Text)) / 100)
            txthsecessAMT.Text = (Val(TXTHSECESS.Text) * Val(txtexciseAMT.Text)) / 100

            txtsubtotal.Text = Val(txtnett.Text) + Val(txteducessAMT.Text) + Val(txtexciseAMT.Text) + Val(txthsecessAMT.Text)

            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable = objclscommon.search("TAX_NAME,tax_tax", "", "TAXMaster", " and TAX_NAME = '" & cmbtax.Text.Trim & "' and TAX_cmpid = " & CmpId & " and TAX_Locationid = " & Locationid & " and TAX_Yearid = " & YearId)
            If dt.Rows.Count > 0 Then txttax.Text = (Val(dt.Rows(0).Item(1)) * Val(txtsubtotal.Text)) / 100

            dt.Reset()
            dt = objclscommon.search("TAX_NAME,tax_tax", "", "TAXMaster", " and TAX_NAME = '" & cmbaddtax.Text.Trim & "' and TAX_cmpid = " & CmpId & " and TAX_Locationid = " & Locationid & " and TAX_Yearid = " & YearId)
            If dt.Rows.Count > 0 Then txtaddtax.Text = (Val(dt.Rows(0).Item(1)) * Val(txtsubtotal.Text)) / 100

            If Val(txtfrper.Text.Trim) > 0 Then txtfreight.Text = (Val(txtfrper.Text) * Val(txtbillamt.Text)) / 100

            dt.Reset()
            dt = objclscommon.search("OCTROI_NAME,OCTROI_octroi", "", "OCTROIMaster", " and OCTROI_NAME = '" & cmboctroi.Text.Trim & "' and OCTROI_cmpid = " & CmpId & " and OCTROI_Locationid = " & Locationid & " and OCTROI_Yearid = " & YearId)
            If dt.Rows.Count > 0 Then txtoctroi.Text = (Val(dt.Rows(0).Item(1)) * (Val(txtsubtotal.Text) + Val(txttax.Text) + Val(txtaddtax.Text) + Val(txtfreight.Text))) / 100


            txtgrandtotal.Text = Format(Val(txtoctroi.Text) + Val(txtsubtotal.Text) + Val(txttax.Text) + Val(txtaddtax.Text) + Val(txtfreight.Text) + Val(txtinspchgs.Text), "0")
            txtroundoff.Text = Format(Val(txtgrandtotal.Text) - (Val(txtsubtotal.Text) + Val(txttax.Text) + Val(txtaddtax.Text) + Val(txtfreight.Text) + Val(txtoctroi.Text) + Val(txtinspchgs.Text)), "0.00")
            txtgrandtotal.Text = Format(Val(txtgrandtotal.Text), "0.00")

            If Val(txtgrandtotal.Text) > 0 Then txtinwords.Text = CurrencyToWord(txtgrandtotal.Text)
        Else
            lbltotalqty.Text = "0.00"
            lbltotalamt.Text = "0.00"
            LBLTOTALMTRS.Text = "0.00"
        End If
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True
        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(cmbname, " Please Fill Buyer Name ")
            bln = False
        End If

        If GRIDSO.RowCount = 0 Then
            EP.SetError(TXTAMOUNT, "Enter Item Details")
            bln = False
        End If





        Dim OBJCMN As New ClsCommon
        Dim DTTABLE As New DataTable

        If ClientName = "AVIS" Then
            If CMBFORWARD.Text.Trim = "" Then
                EP.SetError(CMBFORWARD, "Select Ready / Forward")
                bln = False
            End If

            'WE HAVE TO INTIMATE IF THE SAME DESIGN IS GOING TO SAME PARTY AGAIN 
            For Each ROW As DataGridViewRow In GRIDSO.Rows
                If ROW.Cells(GDESIGN.Index).Value <> "" Then
                    DTTABLE = OBJCMN.SEARCH("SALEORDER.SO_NO AS SONO", "", "SALEORDER_DESC INNER JOIN ITEMMASTER ON ITEM_ID = SO_ITEMID LEFT OUTER JOIN DESIGNMASTER ON SO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON SO_COLORID = COLOR_ID INNER JOIN SALEORDER ON SALEORDER.SO_NO = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID INNER JOIN LEDGERS ON SALEORDER.SO_LEDGERID = LEDGERS.ACC_ID", " AND SALEORDER.SO_NO <> " & Val(TXTSONO.Text.Trim) & " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ISNULL(DESIGN_NO,'') = '" & ROW.Cells(GDESIGN.Index).Value & "' AND ISNULL(COLOR_NAME,'') = '" & ROW.Cells(gcolor.Index).Value & "' AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND SALEORDER.SO_YEARID = " & YearId)
                    If DTTABLE.Rows.Count > 0 Then
                        If MsgBox(ROW.Cells(GDESIGN.Index).Value & " Design Already sent to Party, Wish to Continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            EP.SetError(cmbname, "Sale Order for same Design already Entered")
                            bln = False
                        End If
                    End If
                End If
            Next

        End If


        If ClientName = "KRISHNA" And cmbtrans.Text.Trim = "" Then
            EP.SetError(cmbtrans, "Enter Transport Details")
            bln = False
        End If

        If ClientName <> "CC" And ClientName <> "C3" And ClientName <> "SHREEDEV" Then
            For Each row As DataGridViewRow In GRIDSO.Rows
                If (Val(row.Cells(gQty.Index).Value) = 0 And Val(row.Cells(GMTRS.Index).Value) = 0) Then
                    EP.SetError(cmbname, "Qty and Mtrs Cannot be 0")
                    bln = False
                End If
            Next
        End If

        If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
            For Each row As DataGridViewRow In GRIDSO.Rows
                If Val(row.Cells(gQty.Index).Value) = 0 Then
                    EP.SetError(cmbname, "Qty Cannot be 0")
                    bln = False
                End If
            Next
        End If


        'we have OPENED THE LOCK
        'If lbllocked.Visible = True And LBLCLOSED.Visible = False Then
        '    EP.SetError(lbllocked, "Unable to Update, SO Locked")
        '    bln = False
        'ElseIf lbllocked.Visible = True And LBLCLOSED.Visible = True Then
        '    EP.SetError(LBLCLOSED, "Unable to Update, SO Closed")
        '    bln = False
        'End If

        If SODATE.Text = "__/__/____" Then
            EP.SetError(SODATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(SODATE.Text) Then
                EP.SetError(SODATE, "Date not in Accounting Year")
                bln = False
            End If
            If Convert.ToDateTime(SODATE.Text).Date < SOBLOCKDATE.Date Then
                EP.SetError(SODATE, "Date is Blocked, Please make entries after " & Format(SOBLOCKDATE.Date, "dd/MM/yyyy"))
                bln = False
            End If


            If ClientName = "ANOX" Then
                If Format(Convert.ToDateTime(SODATE.Text).Date, "dd/MM/yyyy") = Format(DUEDATE.Value.Date, "dd/MM/yyyy") Then
                    If MsgBox("Del Date is same as SO Date, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        EP.SetError(DUEDATE, "Del Date is same as SO Date")
                        bln = False
                    End If
                End If
                If txtpono.Text.Trim = "" Then
                    EP.SetError(txtpono, "Enter Party PO No")
                    bln = False
                End If
            End If
        End If

        If Val(TXTSONO.Text.Trim) > 0 And EDIT = False And TXTSONO.ReadOnly = False Then
            DTTABLE = OBJCMN.SEARCH("  ISNULL(so_no, 0) AS SONO ", "", " SALEORDER ", "  AND SALEORDER.so_no=" & TXTSONO.Text.Trim & " AND SALEORDER.SO_CMPID = " & CmpId & " AND SALEORDER.SO_LOCATIONID = " & Locationid & " AND SALEORDER.SO_YEARID = " & YearId)
            If DTTABLE.Rows.Count > 0 Then
                EP.SetError(TXTSONO, "Sale Order No Already Exist")
                bln = False
            End If
        End If

        If ClientName = "SNCM" Then
            If CMBAGENT.Text.Trim = "" Then
                EP.SetError(CMBAGENT, " Please Fill Agent Name ")
                bln = False
            End If
            If cmbtrans.Text.Trim = "" Then
                EP.SetError(cmbtrans, " Please Fill Transport 1 ")
                bln = False
            End If
            If cmbcity.Text.Trim = "" Then
                EP.SetError(cmbcity, " Please Fill To City ")
                bln = False
            End If

            'IF VERIFICED IS TRUE AND EDIT = TRUE THEN DONT ALLOW TO MODIFY THE ORDER FOR USERS.. ONLY ADMIN ALLOWED
            If EDIT = True And CHKVERIFY.CheckState = CheckState.Checked And ALLOWPOSOCHECKIN = False Then
                EP.SetError(CHKVERIFY, "Unable to Modify the Order, Its already Verified")
                bln = False
            End If

            For Each row As DataGridViewRow In GRIDSO.Rows
                If Val(row.Cells(GRATE.Index).Value) = 0 Then
                    EP.SetError(TXTRATE, "Rate Cannot be 0")
                    bln = False
                End If
            Next
        End If

        Return bln
    End Function

    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcolor.Validating
        Try
            If cmbcolor.Text.Trim <> "" Then COLORVALIDATE(cmbcolor, e, Me, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbCITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcity.Validating
        Try
            If cmbcity.Text.Trim <> "" Then
                pcase(cmbcity)
                Dim objclscommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objclscommon.search("city_name", "", "CityMaster", " and city_name = '" & cmbcity.Text.Trim & "' and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbcity.Text.Trim
                    Dim tempmsg As Integer = MsgBox("City not present, Add New?", MsgBoxStyle.YesNo, "TexPro_V1")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        cmbcity.Text = a
                        objyearmaster.savecity(cmbcity.Text.Trim, CmpId, Locationid, Userid, YearId, " and city_name = '" & cmbcity.Text.Trim & "' and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = cmbcity.DataSource
                        If cmbcity.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(cmbcity.Text)
                                cmbcity.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPACKING.Enter
        Try
            If ClientName = "AVIS" Then
                If CMBPACKING.Text.Trim = "" Then FILLNAME(CMBPACKING, EDIT, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')  AND GROUP_NAME = 'HASTE DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBPACKING.Text.Trim = "" Then FILLNAME(CMBPACKING, EDIT, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')   AND ACC_TYPE = 'ACCOUNTS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPACKING.Validated
        Try
            If ClientName = "YASHVI" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "MNARESH" Or ClientName = "MASHOK" Or ClientName = "ABHEE" Or ClientName = "AVIS" Or ClientName = "KRISHNA" Or ClientName = "SUPEEMA" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "AFW" Then
                If CMBPACKING.Text.Trim <> "" Then
                    'GET REGISTER , AGENCT AND TRANS
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(LEDGERSTRANS.Acc_cmpname, '') AS TRANSNAME, ISNULL(CITYMASTER.city_name, '') AS CITYNAME, ISNULL(LEDGERS.Acc_mobile, '') AS MOBILENO, ISNULL(BILLTOLEDGERS.Acc_cmpname, '') AS BILLTONAME, ISNULL(PACKINGTYPEMASTER.PACKINGTYPE_name, '') AS PACKINGTYPE ", "", " LEDGERS LEFT OUTER JOIN PACKINGTYPEMASTER ON LEDGERS.ACC_PACKINGTYPEID = PACKINGTYPEMASTER.PACKINGTYPE_id LEFT OUTER JOIN LEDGERS AS LEDGERSTRANS ON LEDGERS.ACC_TRANSID = LEDGERSTRANS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_DELIVERYATID = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS BILLTOLEDGERS ON LEDGERS.ACC_BILLTOID = BILLTOLEDGERS.Acc_id", " and LEDGERS.acc_cmpname = '" & CMBPACKING.Text.Trim & "'  and LEDGERS.acc_YEARid = " & YearId)
                    If DT.Rows.Count > 0 Then
                        If DT.Rows(0).Item("TRANSNAME") = "" Then
                            cmbtrans.Text = cmbtrans.Text.Trim
                        Else
                            If ClientName <> "MASHOK" And ClientName <> "ABHEE" Then If cmbname.Text.Trim <> CMBPACKING.Text.Trim Then cmbtrans.Text = DT.Rows(0).Item("TRANSNAME")
                        End If

                        'CHECK WHETHER NAME IS EQUAL TO BILLTONAME OR NOT, IF NOT THEN CHANGE IT
                        If ClientName = "KOTHARI" And DT.Rows(0).Item("BILLTONAME") <> "" AndAlso DT.Rows(0).Item("BILLTONAME") <> cmbname.Text.Trim Then
                            cmbname.Text = DT.Rows(0).Item("BILLTONAME")
                        End If


                        If ClientName = "MNARESH" Or ClientName = "AFW" Then
                            cmbcity.Text = DT.Rows(0).Item("CITYNAME")
                            cmbtrans.Text = DT.Rows(0).Item("TRANSNAME")
                            CMBPACKINGTYPE.Text = DT.Rows(0).Item("PACKINGTYPE")
                            TXTMOBILENO.Text = DT.Rows(0).Item("MOBILENO")
                        End If


                        If ClientName = "MASHOK" Or ClientName = "ABHEE" Then
                            cmbcity.Text = DT.Rows(0).Item("CITYNAME")
                            'cmbtrans.Text = DT.Rows(0).Item("TRANSNAME")
                            CMBPACKINGTYPE.Text = DT.Rows(0).Item("PACKINGTYPE")
                            TXTMOBILENO.Text = DT.Rows(0).Item("MOBILENO")
                        End If


                        If ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "AVIS" Or ClientName = "KRISHNA" Or ClientName = "SUPEEMA" Or ClientName = "MAHAVIRPOLYCOT" Then
                            cmbcity.Text = DT.Rows(0).Item("CITYNAME")
                            TXTMOBILENO.Text = DT.Rows(0).Item("MOBILENO")
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPACKING.Validating
        Try
            If CMBPACKING.Text.Trim <> "" Then NAMEVALIDATE(CMBPACKING, CMBCODE, e, Me, txtDeliveryadd, " AND  (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')", "Sundry Debtors", "ACCOUNTS", cmbtrans.Text, CMBAGENT.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALEORDER_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE ORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            CLEAR()
            cmbname.Enabled = True

            If EDIT = True Then
                SHOWDATA()
            End If

            If GRIDSO.RowCount > 0 Then
                txtsrno.Text = Val(GRIDSO.Rows(GRIDSO.RowCount - 1).Cells(0).Value) + 1
            Else
                txtsrno.Text = 1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub SHOWDATA()
        Try
            CLEAR()

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objclsSO As New ClsSaleOrder()
            Dim dt_po As DataTable = objclsSO.selectSo(TEMPSONO, CmpId, Locationid, YearId)

            If dt_po.Rows.Count > 0 Then
                For Each dr As DataRow In dt_po.Rows

                    TXTSONO.Text = dr("SONO")
                    TXTSONO.ReadOnly = True
                    SODATE.Text = Format(Convert.ToDateTime(dr("SODATE")), "dd/MM/yyyy")
                    cmbname.Text = Convert.ToString(dr("NAME"))
                    TXTMOBILENO.Text = Convert.ToString(dr("MOBILENO"))

                    CMBHASTE.Text = Convert.ToString(dr("HASTE"))
                    CMBAGENT.Text = Convert.ToString(dr("AGENT"))
                    CMBRISK.Text = Convert.ToString(dr("RISK"))

                    txtpono.Text = Convert.ToString(dr("PONO"))
                    DUEDATE.Value = Convert.ToDateTime(dr("DUEDATE"))
                    daysremains()

                    cmbtrans.Text = Convert.ToString(dr("TRANS"))
                    cmbtrans2.Text = Convert.ToString(dr("TRANS2"))
                    cmbcity.Text = Convert.ToString(dr("CITY"))
                    TXTREFNO.Text = Convert.ToString(dr("REFNO"))

                    TXTCONSIGNEE.Text = Convert.ToString(dr("CONSIGNEE"))
                    TXTCONSIGNOR.Text = Convert.ToString(dr("CONSIGNOR"))
                    CMBPACKING.Text = Convert.ToString(dr("PACKING"))
                    CMBCURRENCY.Text = Convert.ToString(dr("CURRENCY"))
                    txtnote.Text = Convert.ToString(dr("NOTE"))
                    txttnc.Text = Convert.ToString(dr("TNC"))
                    TXTREMARKS.Text = Convert.ToString(dr("REMARKS"))

                    CMBSALESMAN.Text = Convert.ToString(dr("SALESMAN"))
                    TXTADVANCE.Text = Convert.ToString(dr("ADVANCE"))
                    TXTBALANCE.Text = Convert.ToString(dr("BALANCE"))
                    CMBPACKINGTYPE.Text = Convert.ToString(dr("PACKINGTYPE"))

                    CMBFORWARD.Text = dr("FORWARD")

                    CMBSAMPLE.Text = dr("SAMPLE")
                    CMBFROMCITY.Text = dr("FROMCITY")
                    If Convert.ToBoolean(dr("VERIFIED")) = True Then CHKVERIFY.CheckState = CheckState.Checked Else CHKVERIFY.CheckState = CheckState.Unchecked
                    TEMPVERIFIED = Convert.ToBoolean(dr("VERIFIED"))
                    CMBORDERON.Text = dr("ORDERON")



                    GRIDSO.Rows.Add(dr("SRNO").ToString, dr("ITEM").ToString, dr("QUALITY").ToString, dr("DESIGN").ToString, dr("GRIDREMARKS").ToString, dr("COLOR").ToString, dr("PARTYPONO"), Format(Val(dr("QTY")), "0.00"), dr("UNIT").ToString, Format(Val(dr("CUT")), "0.00"), Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER"), Format(Val(dr("AMOUNT")), "0.00"), Val(dr("RECDQTY")), Val(dr("RECDMTRS")), dr("DONE"), dr("SAMPLEDONE"), dr("CLOSED"))

                    If Val(dr("RECDQTY")) > 0 Or Val(dr("RECDMTRS")) > 0 Then
                        GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If


                    If Convert.ToBoolean(dr("CLOSED")) = True Then
                        GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                        lbllocked.Visible = True
                        LBLCLOSED.Visible = True
                        PBlock.Visible = True
                        If ClientName <> "DAKSH" And ClientName <> "SHALIBHADRA" Then cmbname.Enabled = False
                    End If

                    If Convert.ToBoolean(dr("SMSSEND")) = True Then LBLSMS.Visible = True
                    If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True




                    txtdisper.Text = dr("DISPER")
                    txtdisamt.Text = dr("DISAMT")
                    txtpfper.Text = dr("PFPER")
                    'txtpfamt.Text = dr("PFAMT")
                    txttestchgs.Text = dr("TECTCHGS")
                    txtnett.Text = dr("NETT")

                    If dr("EXCISEAMT") > 0 Then
                        chkexcise.CheckState = CheckState.Checked
                        'TXTEXCISEID.Text = dr("EXCISE")
                        TXTEXCISE.Text = dr("EXCISE")
                        txtexciseAMT.Text = dr("EXCISEAMT")
                        'TXTEDUCESS.Text = dr("EDUCESSNAME")
                        txteducessAMT.Text = dr("EDUCESSAMT")
                        'TXTHSECESS.Text = dr("HSECESSNAME")
                        txthsecessAMT.Text = dr("SO_HSECESSAMT")
                    Else
                        chkexcise.CheckState = CheckState.Unchecked

                    End If

                    'txtsubtotal.Text = dr("SO_SUBTOTAL")
                    cmbtax.Text = dr("TAX")
                    txttax.Text = dr("TAXAMT")
                    cmbaddtax.Text = dr("ADDTAX")
                    txtaddtax.Text = dr("ADDTAXAMT")
                    txtfrper.Text = dr("FRPER")
                    txtfreight.Text = dr("FREIGHT")
                    cmboctroi.Text = dr("OCTROI")
                    txtoctroi.Text = dr("OCTROIAMT")
                    txtinspchgs.Text = dr("INSAMT")
                    txtroundoff.Text = dr("ROUNDOFF")
                    txtgrandtotal.Text = dr("GRANDTOTAL")

                    cmbmisc.Text = dr("MISCPER").ToString
                    txtDiscrate.Text = dr("DISCRATE")
                    txtDiscLot.Text = dr("DISCLOT")
                    txtdd.Text = dr("DISCDEALER")
                    txtkatai.Text = dr("KATAI")
                    txtcd.Text = dr("CD")
                    txtadat.Text = dr("ADAT")
                    txtdays.Text = dr("DAYS")
                    txtint.Text = dr("INT")


                    If lbllocked.Visible = True Then
                        cmbname.Enabled = False
                        If ClientName <> "MASHOK" And ClientName <> "ABHEE" Then CMBPACKING.Enabled = False
                    End If

                Next
                GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1
            Else
                EDIT = False
                CLEAR()

            End If
            chkchange.CheckState = CheckState.Checked
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            If CMBAGENT.Text.Trim = "" Then fillagentledger(CMBAGENT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND ACC_TYPE='AGENT'")
            If cmbtrans.Text.Trim = "" Then filltransname(cmbtrans, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE='TRANSPORT'")
            If cmbtrans2.Text.Trim = "" Then filltransname(cmbtrans2, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE='TRANSPORT'")
            fillCITY(cmbcity, EDIT)

            fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            fillQUALITY(CMBQUALITY, EDIT)
            FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
            FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
            fillunit(cmbqtyunit)
            fillCURRENCY(CMBCURRENCY)
            filltax(cmbtax, EDIT)
            filltax(cmbaddtax, EDIT)

            If ClientName = "AVIS" Then
                If CMBPACKING.Text.Trim = "" Then FILLNAME(CMBPACKING, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND GROUP_NAME = 'HASTE DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
                If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND GROUP_NAME <> 'HASTE DEBTORS'")
            Else
                If CMBPACKING.Text.Trim = "" Then FILLNAME(CMBPACKING, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
                If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If CMBHASTE.Text.Trim = "" Then
                DT = OBJCMN.SEARCH(" ADDRESS_alias ", "", "   ADDRESSMASTER INNER JOIN LEDGERS ON ADDRESSMASTER.ADDRESS_LEDGERID = LEDGERS.ACC_id AND ADDRESSMASTER.ADDRESS_cmpid = LEDGERS.ACC_cmpid AND ADDRESSMASTER.ADDRESS_locationid = LEDGERS.ACC_locationid AND ADDRESSMASTER.ADDRESS_yearid = LEDGERS.ACC_yearid  ", "  and  LEDGERS.ACC_cmpname ='" & cmbname.Text.Trim & "' and ADDRESS_cmpid=" & CmpId & " and ADDRESS_Locationid=" & Locationid & " and ADDRESS_Yearid=" & YearId)
                If DT.Rows.Count > 0 Then
                    DT.DefaultView.Sort = "ADDRESS_alias"
                    CMBHASTE.DataSource = DT
                    CMBHASTE.DisplayMember = "ADDRESS_alias"
                End If
                CMBHASTE.SelectAll()
            End If


            If CMBSALESMAN.Text.Trim = "" Then FILLSALESMAN(CMBSALESMAN)
            If CMBPACKINGTYPE.Text.Trim = "" Then FILLPACKINGTYPE(CMBPACKINGTYPE)

            DT = OBJCMN.SEARCH("CMP_NAME AS CMPNAME", "", "CMPMASTER", " AND CMP_ID <> " & CmpId)
            For Each ROW As DataRow In DT.Rows
                CMBCMPNAME.Items.Add(ROW("CMPNAME"))
            Next

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub daysremains()
        Dim tsTimeSpan As TimeSpan
        'tsTimeSpan = deldate.Value.Subtract(SODATE.Value)
        tsTimeSpan = deldate.Value.Subtract(Format(Convert.ToDateTime(SODATE.Text), "dd/MM/yyyy"))
        txtcrdays.Text = tsTimeSpan.Days
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        If ClientName = "SNCM" Then TXTSONO.Focus() Else cmbname.Focus()
    End Sub

    Private Sub SALEORDER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNoCancel)
                    If tempmsg = vbCancel Then Exit Sub
                    If tempmsg = vbYes Then cmdOK_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'for grid foucs
                GRIDSO.Focus()
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                ToolPREVIOUS_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                Toolnext_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call ToolStripButton3_Click(sender, e)
            ElseIf e.KeyCode = Windows.Forms.Keys.F12 Then
                Dim OBJRATE As New PurchaseItemRateReport
                If cmbname.Text <> "" Then OBJRATE.WHERECLAUSE = OBJRATE.WHERECLAUSE & " and LEDGERS.ACC_CMPNAME ='" & cmbname.Text.Trim & "'"
                If cmbitemname.Text <> "" Then OBJRATE.WHERECLAUSE = OBJRATE.WHERECLAUSE & " and ITEMMASTER.ITEM_NAME='" & cmbitemname.Text.Trim & "'"
                OBJRATE.MdiParent = MDIMain
                OBJRATE.FRMSTRING = "SALE"
                OBJRATE.Show()
                Exit Sub
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

    Sub GETPENDINGBALES(ByVal ROW As Integer)
        Try
            If SALEORDERONMTRS = False Then LBLBALQTY.Text = Val(GRIDSO.Item(gQty.Index, ROW).EditedFormattedValue) - Val(GRIDSO.Item(GRECDQTY.Index, ROW).EditedFormattedValue) Else LBLBALQTY.Text = Val(GRIDSO.Item(GMTRS.Index, ROW).EditedFormattedValue) - Val(GRIDSO.Item(GRECDMTRS.Index, ROW).EditedFormattedValue)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSO_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDSO.CellClick
        Try
            'GET BALANCE BALES AND GETSTOCK
            If e.RowIndex >= 0 Then
                GETPENDINGBALES(e.RowIndex)
                GETSTOCK(GRIDSO.CurrentRow.Cells(gitemname.Index).Value, GRIDSO.CurrentRow.Cells(GDESIGN.Index).Value, GRIDSO.CurrentRow.Cells(gcolor.Index).Value)
            End If

            'GET LAST RATE OF SELECTED ITEM FOR SELECTED PARTY
            If GRIDSO.RowCount > 0 Then
                If (ClientName = "MSANCHITKUMAR") Or (ClientName = "SNCM") Or (ClientName = "MNARESH") And GRIDSO.Item(gitemname.Index, GRIDSO.CurrentRow.Index).Value <> "" And cmbname.Text.Trim <> "" Then
                    Dim OBJCMN As New ClsCommon
                    'Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(INVOICEMASTER_DESC.INVOICE_RATE,0) AS LASTRATE", "", " INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN INVOICEMASTER ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO  AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN LEDGERS ON ACC_ID = INVOICE_LEDGERID", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & GRIDSO.Item(gitemname.Index, GRIDSO.CurrentRow.Index).Value & "' AND INVOICEMASTER.INVOICE_DATE < '" & Format(Convert.ToDateTime(SODATE.Text).Date, "MM/dd/yyyy") & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER.INVOICE_NO DESC")
                    Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(INVOICEMASTER_DESC.INVOICE_RATE,0) AS LASTRATE", "", " INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN INVOICEMASTER ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO  AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN LEDGERS ON ACC_ID = INVOICE_LEDGERID", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & GRIDSO.Item(gitemname.Index, GRIDSO.CurrentRow.Index).Value & "' AND INVOICEMASTER.INVOICE_DATE < '" & Format(Convert.ToDateTime(SODATE.Text).Date, "MM/dd/yyyy") & "' ORDER BY INVOICEMASTER.INVOICE_NO DESC")
                    If DT.Rows.Count > 0 Then LBLRATE.Text = Format(Val(DT.Rows(0).Item("LASTRATE")), "0.00")
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridSO_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDSO.CellDoubleClick
        EDITROW()
    End Sub

    Sub CALC()
        Try
            If Val(TXTCUT.Text.Trim) > 0 Then TXTMTRS.Text = Format(Val(TXTCUT.Text.Trim) * Val(txtQTY.Text.Trim), "0.00")
            If CMBPER.Text.Trim = "Mtrs" Then
                TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(TXTMTRS.Text.Trim), "0.00")
            Else
                TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(txtQTY.Text.Trim), "0.00")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRATE_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress
        numdotkeypress(e, TXTRATE, Me)
    End Sub

    Private Sub txtrate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTRATE.Validating
        Try
            CALC()
            If ClientName = "YASHVI" Or ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Or ClientName = "MASHOK" Or ClientName = "ABHEE" Then TXTAMOUNT_Validating(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTY.KeyPress
        numdot(e, txtQTY, Me)
    End Sub

    Private Sub txtrate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress
        numdotkeypress(e, TXTRATE, Me)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Unable to Delete, SO Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                tempMsg = MsgBox("Delete Sale Order ?", MsgBoxStyle.YesNo)
                If tempMsg = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTSONO.Text.Trim)
                    alParaval.Add(CmpId)
                    alParaval.Add(Locationid)
                    alParaval.Add(Userid)
                    alParaval.Add(YearId)

                    Dim clspo As New ClsSaleOrder()
                    clspo.alParaval = alParaval
                    IntResult = clspo.Delete()
                    MsgBox("sale Order Deleted")
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

    Private Sub cmbagent_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then NAMEVALIDATE(CMBAGENT, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then
                NAMEVALIDATE(cmbname, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS", cmbtrans.Text, CMBAGENT.Text)
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(ACC_CDPER,0) AS CDPER, ISNULL(ACC_CRDAYS,0) AS CRDAYS, ISNULL(ACC_GSTIN,'') AS GSTIN, ISNULL(LEDGERS.ACC_MOBILE,'') AS MOBILENO,ISNULL(PACKINGTYPEMASTER.PACKINGTYPE_name, '') AS PACKINGTYPE,ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME, ISNULL(LEDGERS.ACC_WARNING,'') AS WARNINGTEXT", "", "  LEDGERS LEFT OUTER JOIN PACKINGTYPEMASTER ON LEDGERS.ACC_PACKINGTYPEID = PACKINGTYPEMASTER.PACKINGTYPE_id LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_DELIVERYATID = CITY_ID ", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "'  AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If ClientName <> "MASHOK" And ClientName <> "ABHEE" Then
                        txtdays.Text = Val(DT.Rows(0).Item("CRDAYS"))
                    End If
                    LBLGSTIN.Text = DT.Rows(0).Item("GSTIN")
                    TXTMOBILENO.Text = DT.Rows(0).Item("MOBILENO")
                    If CMBPACKINGTYPE.Text.Trim = "" Then
                        CMBPACKINGTYPE.Text = DT.Rows(0).Item("PACKINGTYPE")
                        If ClientName <> "INDRAPUJAIMPEX" Then cmbcity.Text = DT.Rows(0).Item("CITYNAME")
                    End If
                    CMBSALESMAN.Text = DT.Rows(0).Item("SALESMAN")
                    If DT.Rows(0).Item("WARNINGTEXT") <> "" Then MsgBox(DT.Rows(0).Item("WARNINGTEXT"), MsgBoxStyle.Critical)
                    If ClientName = "MAHAVIRPOLYCOT" Or ClientName = "SNCM" And Val(DT.Rows(0).Item("CDPER")) > 0 And ClientName <> "MASHOK" And ClientName <> "ABHEE" Then txtcd.Text = Val(DT.Rows(0).Item("CDPER"))
                    If ClientName <> "CC" And ClientName <> "C3" And ClientName <> "SHREEDEV" Then TXTCONSIGNEE.Text = cmbname.Text
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridSO_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDSO.CellValidating
        ''  CODE FOR NUMERIC CHECK ONLY
        Dim colNum As Integer = GRIDSO.Columns(e.ColumnIndex).Index
        If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

        Select Case colNum

            Case GRATE.Index, gQty.Index, gcut.Index, GMTRS.Index
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                If bValid Then
                    If GRIDSO.CurrentCell.Value = Nothing Then GRIDSO.CurrentCell.Value = "0.00"
                    GRIDSO.CurrentCell.Value = Convert.ToDecimal(GRIDSO.Item(colNum, e.RowIndex).Value)
                    '' everything is good

                Else
                    MessageBox.Show("Invalid Number Entered")
                    e.Cancel = True
                    Exit Sub
                End If
                TOTAL()

        End Select
    End Sub

    Sub EDITROW()
        Try
            If GRIDSO.CurrentRow.Index >= 0 And GRIDSO.Item(gsrno.Index, GRIDSO.CurrentRow.Index).Value <> Nothing Then

                If Convert.ToBoolean(GRIDSO.Rows(GRIDSO.CurrentRow.Index).Cells(GDONE.Index).Value) = True Then 'If row.Cells(16).Value <> "0" Then 
                    MsgBox("Item Locked. First Delete from SALEORDER")
                    Exit Sub
                End If
                GRIDDOUBLECLICK = True
                txtsrno.Text = GRIDSO.Item(gsrno.Index, GRIDSO.CurrentRow.Index).Value.ToString
                cmbitemname.Text = GRIDSO.Item(gitemname.Index, GRIDSO.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDSO.Item(GQUALITY.Index, GRIDSO.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDSO.Item(GDESIGN.Index, GRIDSO.CurrentRow.Index).Value.ToString
                txtgridremarks.Text = GRIDSO.Item(gdesc.Index, GRIDSO.CurrentRow.Index).Value.ToString
                CMBGRIDREMARKS.Text = GRIDSO.Item(gdesc.Index, GRIDSO.CurrentRow.Index).Value.ToString
                cmbcolor.Text = GRIDSO.Item(gcolor.Index, GRIDSO.CurrentRow.Index).Value.ToString
                TXTPARTYPONO.Text = GRIDSO.Item(GPARTYPONO.Index, GRIDSO.CurrentRow.Index).Value.ToString
                txtQTY.Text = GRIDSO.Item(gQty.Index, GRIDSO.CurrentRow.Index).Value.ToString
                cmbqtyunit.Text = GRIDSO.Item(gqtyunit.Index, GRIDSO.CurrentRow.Index).Value.ToString
                TXTCUT.Text = GRIDSO.Item(gcut.Index, GRIDSO.CurrentRow.Index).Value.ToString
                TXTMTRS.Text = GRIDSO.Item(GMTRS.Index, GRIDSO.CurrentRow.Index).Value.ToString
                TXTRATE.Text = GRIDSO.Item(GRATE.Index, GRIDSO.CurrentRow.Index).Value.ToString
                CMBPER.Text = GRIDSO.Item(GPER.Index, GRIDSO.CurrentRow.Index).Value.ToString
                TXTAMOUNT.Text = GRIDSO.Item(GAMOUNT.Index, GRIDSO.CurrentRow.Index).Value.ToString



                'GET ITEMCODE
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ITEM_CODE AS ITEMCODE", "", " ITEMMASTER ", " AND ITEM_NAME = '" & GRIDSO.Item(gitemname.Index, GRIDSO.CurrentRow.Index).Value & "' AND ITEM_YEARID =" & YearId)
                If DT.Rows.Count > 0 Then CMBITEMCODE.Text = DT.Rows(0).Item("ITEMCODE")

                TEMPROW = GRIDSO.CurrentRow.Index
                txtsrno.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridSO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDSO.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSO.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbMERCHANT.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block


                'DONT ALLOW TO DELETE ANY ROW IF LOCKED IS VISIBLE
                If lbllocked.Visible = True Then
                    MessageBox.Show("Unable to Delete Row, Sale Order is Locked")
                    Exit Sub
                End If


                GRIDSO.Rows.RemoveAt(GRIDSO.CurrentRow.Index)
                TOTAL()
                getsrno(GRIDSO)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            ElseIf e.KeyCode = Keys.F12 And GRIDSO.RowCount > 0 Then
                If GRIDSO.CurrentRow.Cells(gitemname.Index).Value <> "" Then GRIDSO.Rows.Add(CloneWithValues(GRIDSO.CurrentRow))
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Public Function CloneWithValues(ByVal row As DataGridViewRow) As DataGridViewRow
        CloneWithValues = CType(row.Clone(), DataGridViewRow)
        For index As Int32 = 0 To row.Cells.Count - 1
            CloneWithValues.Cells(index).Value = row.Cells(index).Value
        Next
    End Function

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDSO.RowCount = 0
                TEMPSONO = Val(tstxtbillno.Text)
                If TEMPSONO > 0 Then
                    EDIT = True
                    SHOWDATA()
                    'SALEORDER_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTCRDAYS_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcrdays.Validated
        'deldate.Value = DateAdd(DateInterval.Day, CDbl(Val(txtcrdays.Text)), SODATE.Value)
        Try
            If SODATE.Text = "__/__/____" Then
                If Val(txtcrdays.Text.Trim) > 0 Then deldate.Value = Convert.ToDateTime(SODATE.Text).Date.AddDays(Val(txtcrdays.Text.Trim))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub deldate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles deldate.Validating
        daysremains()
    End Sub

    Private Sub txtdisper_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdisper.KeyPress
        Try
            numdotkeypress(e, txtdisper, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtdisper_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdisper.Validated
        TOTAL()
    End Sub

    Private Sub txtfrper_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfrper.KeyPress
        Try
            numdotkeypress(e, txtfrper, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtfrper_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfrper.Validated
        TOTAL()
    End Sub

    Private Sub txttestchgs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txttestchgs.KeyPress
        Try
            numdotkeypress(e, txttestchgs, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txttestchgs_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttestchgs.Validated
        TOTAL()
    End Sub

    Private Sub chkexcise_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkexcise.CheckedChanged
        If chkexcise.Checked = True Then
            TXTEXCISE.ReadOnly = True
            txtexciseAMT.ReadOnly = True
            TXTEDUCESS.ReadOnly = True
            txteducessAMT.ReadOnly = True
            TXTHSECESS.ReadOnly = True
            txthsecessAMT.ReadOnly = True

        Else
            TXTEXCISE.Text = 0.0
            txtexciseAMT.Text = 0.0
            TXTEDUCESS.Text = 0.0
            txteducessAMT.Text = 0.0
            TXTHSECESS.Text = 0.0
            txthsecessAMT.Text = 0.0
        End If
        TOTAL()
    End Sub

    Private Sub txtinspchgs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtinspchgs.KeyPress
        Try
            numdotkeypress(e, txtinspchgs, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtinspchgs_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtinspchgs.Validated
        TOTAL()
    End Sub

    Private Sub txtpfper_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpfper.KeyPress
        Try
            numdotkeypress(e, txtpfper, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtpfper_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpfper.Validated
        TOTAL()
    End Sub

    Private Sub txtfreight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfreight.KeyPress
        Try
            numdotkeypress(e, txtfreight, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtfreight_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfreight.Validated
        TOTAL()
    End Sub

    Private Sub txtqty_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtQTY.Validating
        CALC()
        If Val(txtQTY.Text) > 0 And Val(TXTRATE.Text) > 0 Then TXTAMOUNT.Text = Val(txtQTY.Text) * Val(TXTRATE.Text)
    End Sub

    Private Sub ToolPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolPREVIOUS.Click
        Try
            GRIDSO.RowCount = 0
LINE1:
            TEMPSONO = Val(TXTSONO.Text) - 1
            If TEMPSONO > 0 Then
                EDIT = True
                SHOWDATA()
                ' SALEORDER_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDSO.RowCount = 0 And TEMPSONO > 1 Then
                TXTSONO.Text = TEMPSONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Toolnext.Click
        Try
            GRIDSO.RowCount = 0
LINE1:
            TEMPSONO = Val(TXTSONO.Text) + 1
            getmax_SO_no()
            Dim MAXNO As Integer = TXTSONO.Text.Trim
            CLEAR()
            If Val(TXTSONO.Text) - 1 >= TEMPSONO Then
                EDIT = True
                SHOWDATA()
                'SALEORDER_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDSO.RowCount = 0 And TEMPSONO < MAXNO Then
                TXTSONO.Text = TEMPSONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            Dim OBJsaleOrder As New SaleOrderDesign
            OBJsaleOrder.MdiParent = MDIMain
            OBJsaleOrder.FRMSTRING = "SOREPORT"
            OBJsaleOrder.PARTYNAME = cmbname.Text.Trim
            OBJsaleOrder.AGENTNAME = CMBAGENT.Text.Trim
            OBJsaleOrder.SHIPTONAME = CMBPACKING.Text.Trim
            OBJsaleOrder.SONO = Val(TXTSONO.Text.Trim)
            OBJsaleOrder.FORMULA = "{saleOrder.so_no}=" & Val(TXTSONO.Text) & " and {saleOrder.SO_yearid}=" & YearId
            If ClientName = "MAHAVIRPOLYCOT" Then
                If MsgBox("Wish to Print Party SO Report?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    OBJsaleOrder.PARTYSOREPORT = True
                    GoTo LINE1
                End If
            End If
            If ClientName = "KRISHNA" Or ClientName = "KRFABRICS" Or ClientName = "AVIS" Or ClientName = "SHREENAKODA" Or ClientName = "SOFTAS" Then
                If MsgBox("Wish to Print SO Report With Rate?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJsaleOrder.RATERACK = "RATE" Else OBJsaleOrder.RATERACK = "RACK"
            End If

            If ClientName = "GELATO" Or ClientName = "ANOX" Then
                If MsgBox("Wish to Print SO Report With Image?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJsaleOrder.WITHPHOTO = True
            End If

LINE1:
            OBJsaleOrder.Show()


            If ClientName = "AVIS" Then
                tempMsg = MsgBox("Wish to Print SO cutting Report?", MsgBoxStyle.YesNo)
                If tempMsg = vbYes Then
                    Dim OBJINV As New SaleOrderDesign
                    OBJINV.MdiParent = MDIMain
                    OBJINV.FRMSTRING = "SOCAD"
                    OBJINV.PARTYNAME = cmbname.Text.Trim
                    OBJINV.AGENTNAME = CMBAGENT.Text.Trim
                    OBJINV.FORMULA = "{saleOrder.so_no}=" & Val(TXTSONO.Text) & " and {saleOrder.SO_yearid}=" & YearId
                    OBJINV.Show()
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then
                PRINTREPORT()
                If ClientName = "SOFTAS" Then PRINTBARCODE()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then filltransname(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtrans.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"

                OBJLEDGER.ShowDialog()
                'If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans.Text = OBJLEDGER.TEMPNAME
                'If OBJLEDGER.TEMPAGENT <> "" Then CMBAGENT.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then NAMEVALIDATE(cmbtrans, CMBCODE, e, Me, TXTTRANSADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS2_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtrans2.Enter
        Try
            If cmbtrans2.Text.Trim = "" Then filltransname(cmbtrans2, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS2_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans2.Validating
        Try
            If cmbtrans2.Text.Trim <> "" Then NAMEVALIDATE(cmbtrans2, CMBCODE, e, Me, TXTTRANSADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
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

            Dim objpodtls As New SaleOrderDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdOK_Click(sender, e)
    End Sub

    Private Sub CMBCONSIGNEE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBHASTE.Validating
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBHASTE.Text.Trim <> "" Then
                pcase(CMBHASTE)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("ADDRESS_full", "", " ADDRESSMASTER INNER JOIN LEDGERS ON ADDRESSMASTER.ADDRESS_LEDGERID = LEDGERS.ACC_id AND ADDRESSMASTER.ADDRESS_cmpid = LEDGERS.ACC_cmpid AND ADDRESSMASTER.ADDRESS_locationid = LEDGERS.ACC_locationid AND ADDRESSMASTER.ADDRESS_yearid = LEDGERS.ACC_yearid  ", "  and  LEDGERS.ACC_cmpname ='" & cmbname.Text.Trim & "'  and ADDRESS_alias = '" & CMBHASTE.Text.Trim & "' and ADDRESS_cmpid = " & CmpId & " and ADDRESS_Locationid = " & Locationid & " and ADDRESS_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBHASTE.Text.Trim
                    Dim tempmsg As Integer = MsgBox("ADDRESS not present, Add New?", MsgBoxStyle.YesNo, "Textrade")
                    If tempmsg = vbYes Then

                        CMBHASTE.Text = a
                        Dim objADDRESSmaster As New addressMaster
                        objADDRESSmaster.txtname.Text = CMBHASTE.Text
                        objADDRESSmaster.cmbname.Text = cmbname.Text
                        objADDRESSmaster.cmbname.Enabled = False
                        objADDRESSmaster.ShowDialog()
                        dt = objclscommon.search("ADDRESS_alias", "", "ADDRESSMaster", " and ADDRESS_alias = '" & CMBHASTE.Text.Trim & "' and ADDRESS_cmpid = " & CmpId & " and ADDRESS_Locationid = " & Locationid & " and ADDRESS_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBHASTE.DataSource
                            If CMBHASTE.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBHASTE.Text.Trim)
                                    CMBHASTE.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                Else
                    txtDeliveryadd.Text = dt.Rows(0).Item(0).ToString
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub CMBGODOWN1_ENTER(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbgodown1.Enter
        Try
            If cmbgodown1.Text.Trim = "" Then fillGODOWN(cmbgodown1, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN1_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbgodown1.Validating
        Try
            If cmbgodown1.Text.Trim <> "" Then GODOWNVALIDATE(cmbgodown1, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub CMBGODOWN2_ENTER(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbgodown2.Enter
        Try
            If cmbgodown2.Text.Trim = "" Then fillGODOWN(cmbgodown2, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN2_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbgodown2.Validating
        Try
            If cmbgodown2.Text.Trim <> "" Then GODOWNVALIDATE(cmbgodown2, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitemname.Enter
        Try
            If cmbitemname.Text.Trim = "" Then fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbitemname.Validating
        Try
            If cmbitemname.Text.Trim <> "" Then itemvalidate(cmbitemname, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtDiscrate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscrate.KeyPress
        Try
            numdotkeypress(e, txtDiscrate, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtDiscLot_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscLot.KeyPress
        Try
            numdotkeypress(e, txtDiscLot, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtdd_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdd.KeyPress
        Try
            numdotkeypress(e, txtadd, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtkatai_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtkatai.KeyPress
        Try
            numdotkeypress(e, txtkatai, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtcd_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcd.KeyPress
        Try
            numdotkeypress(e, txtcd, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtadat_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtadat.KeyPress
        Try
            numdotkeypress(e, txtadat, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtdays_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdays.KeyPress
        Try
            numkeypress(e, txtdays, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtint_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtint.KeyPress
        Try
            numkeypress(e, txtint, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLCLOSE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim OBJSO As New SaleOrderClose
            OBJSO.MdiParent = MDIMain
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleOrder_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                LBLCONSIGNOR.Text = "To"
                LBLCONSIGNEE.Text = "Mobile No."
                TXTMTRS.BackColor = Color.White
                cmbqtyunit.Text = "Pcs"
                CMBPER.Text = "Qty"
                LBLPENDINGPO.Visible = False
                LBLSTOCKATDYEING.Visible = False
                LBLSTOCKINHOUSE.Visible = False
                TXTPCSINHOUSE.Visible = False
                TXTPCSJOBBER.Visible = False
                TXTPCSPENDINGPO.Visible = False
                TXTMTRSINHOUSE.Visible = False
                TXTMTRSJOBBER.Visible = False
                TXTMTRSPENDINGPO.Visible = False
                TXTSTOCKITEMNAME.Visible = False
                txtgrandtotal.Visible = True
                LBLADVANCE.Visible = True
                LBLBALANCE.Visible = True
                TXTADVANCE.Visible = True
                TXTBALANCE.Visible = True
            End If

            If ClientName = "MNARESH" Or ClientName = "MSANCHITKUMAR" Or ClientName = "SNCM" Or ClientName = "MASHOK" Or ClientName = "ABHEE" Then LBLRATE.Visible = True

            If ClientName = "AFW" Then
                CMBRISK.TabStop = False
                cmbtrans2.TabStop = False
                TXTREFNO.TabStop = False
                CMBCURRENCY.TabStop = False
                CMBFORWARD.TabStop = False
                CMBQUALITY.TabStop = False
                CMBDESIGN.TabStop = False
                TXTPARTYPONO.TabStop = False
            End If

            If ClientName = "SAFFRON" Then
                TXTSONO.ReadOnly = False
                TXTSONO.BackColor = Color.LemonChiffon

                cmbqtyunit.TabIndex = 0
                TXTMTRS.TabStop = False
                CMBPER.TabStop = False
                TXTAMOUNT.TabStop = False
                CMBQUALITY.TabStop = False
                txtgridremarks.TabStop = False
                CMBDESIGN.TabStop = False
                CMBHASTE.TabStop = False
                CMBAGENT.TabStop = False
                CMBRISK.TabStop = False
                CMBSALESMAN.TabStop = False
                cmbtrans.TabStop = False
                cmbtrans2.TabStop = False
                cmbcity.TabStop = False
                TXTREFNO.TabStop = False
                CMBPACKING.TabStop = False
                TXTCONSIGNEE.TabStop = False
                TXTCONSIGNOR.TabStop = False
                TXTMOBILENO.TabStop = False

                'CMBITEMCODE.Visible = True
                'cmbitemname.Enabled = False
            End If
            If ClientName = "YASHVI" Then
                cmbqtyunit.TabIndex = 0
                TXTMTRS.TabStop = False
                CMBPER.TabStop = False
                TXTAMOUNT.TabStop = False
                CMBQUALITY.TabStop = False
                txtgridremarks.TabStop = False
                cmbcolor.TabStop = False
                TXTPARTYPONO.TabStop = False
                CMBHASTE.TabStop = False
                CMBAGENT.TabStop = False
                CMBRISK.TabStop = False
                CMBSALESMAN.TabStop = False
                cmbtrans.TabStop = False
                cmbtrans2.TabStop = False
                cmbcity.TabStop = False
                TXTREFNO.TabStop = False
                CMBPACKING.TabStop = False
                TXTCONSIGNEE.TabStop = False
                TXTCONSIGNOR.TabStop = False
                TXTMOBILENO.TabStop = False
                CMBGRIDREMARKS.TabStop = False
            End If
            If ClientName = "KCRAYON" Then lbltotalqty.Visible = True

            If ClientName = "AVIS" Or ClientName = "KRISHNA" Then
                CMBHASTE.Visible = False
                LBLHASTE.Visible = False
                CMBPACKINGTYPE.TabStop = True
                CMBAGENT.TabStop = False
                CMBRISK.TabStop = False
                CMBSALESMAN.TabStop = False
                txtpono.TabStop = False
                DUEDATE.TabStop = False
                cmbtrans.TabStop = False
                cmbtrans2.TabStop = False
                cmbcity.TabStop = False
                TXTREFNO.TabStop = False
                CMBCURRENCY.TabStop = False
                CMBQUALITY.TabStop = False
                txtgridremarks.TabStop = False
                CMBPER.TabStop = False

                'allow manual SONO as per kushal bhai
                TXTSONO.ReadOnly = False
                If ClientName = "AVIS" Then cmbqtyunit.Text = "MTRS"
            End If

            If ClientName = "KRISHNA" Then
                LBLCMPNAME.Visible = True
                CMBCMPNAME.Visible = True
                LBLSONO.Visible = True
                TXTCMPSONO.Visible = True
            End If

            If ClientName = "SONU" Then SODATE.TabStop = False
            If ClientName = "KEMLINO" Or ClientName = "DRDRAPES" Then TXTSONO.ReadOnly = False
            If ClientName = "SMS" Then GPARTYPONO.HeaderText = "Packing"
            If ClientName = "GELATO" Then PANELSIZE.Visible = True
            If ClientName = "RAJKRIPA" Then CHKFETCHDESC.Visible = True


            If ClientName = "SNCM" Then
                CMBFORWARD.Text = "READY"
                gcut.HeaderText = "SetQty"
                TXTCUT.Enabled = False
                CHKVERIFY.CheckState = CheckState.Unchecked
                CMBQUALITY.TabStop = False
                cmbcolor.TabStop = False
                TXTPARTYPONO.TabStop = False
                TXTSONO.TabStop = True
                TXTSONO.Focus()
            End If


            If ClientName = "MASHOK" Or ClientName = "ABHEE" Then
                If ClientName = "MASHOK" Then
                    TXTCUT.Text = 110
                    cmbqtyunit.TabStop = False
                    TXTCUT.TabStop = False
                End If
                CMBSALESMAN.TabStop = False
                CMBRISK.TabStop = False
                txtpono.TabStop = False
                DUEDATE.TabStop = False
                cmbtrans2.TabStop = False
                TXTREFNO.TabStop = False
                cmbcity.TabStop = False
                CMBCURRENCY.TabStop = False
                CMBFORWARD.TabStop = False
                CMBQUALITY.TabStop = False
                CMBDESIGN.TabStop = False
                txtgridremarks.TabStop = False
                cmbcolor.TabStop = False
                TXTPARTYPONO.TabStop = False
                CMBPER.TabStop = False
                TXTAMOUNT.TabStop = False
                TabControl1.TabStop = True

                LBLCATEGORY.Visible = True
                CMBORDERON.Visible = True
            End If


            If ALLOWPOSOCHECKIN = False Then
                CHKVERIFY.Enabled = False
            Else
                CHKVERIFY.Enabled = True
            End If

            If ClientName = "BARKHA" Then
                LBLTRANS1.Text = "Mumbai"
                LBLTRANS2.Text = "Jetpur"
            End If
            If SALEORDERONMTRS = True Then CMBORDERON.Text = "MTRS" Else CMBORDERON.Text = "PCS"


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBAGENT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND ACC_TYPE='AGENT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
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
            If ClientName = "AVIS" And CMBDESIGN.Text.Trim <> "" Then cmbitemname.Text = ""
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtrans2.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans2.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcity_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbcity.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCITY As New SelectCity
                OBJCITY.FRMSTRING = "CITY"
                OBJCITY.ShowDialog()
                If OBJCITY.TEMPNAME <> "" Then cmbcity.Text = OBJCITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbqtyunit.Enter
        Try
            If cmbqtyunit.Text.Trim = "" Then fillunit(cmbqtyunit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbqtyunit.Validating
        Try
            If cmbqtyunit.Text.Trim <> "" Then unitvalidate(cmbqtyunit, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPER_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPER.Validated
        CALC()
    End Sub

    Private Sub TXTAMOUNT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTAMOUNT.Validating
        Try
            If cmbitemname.Text.Trim <> "" And cmbqtyunit.Text.Trim <> "" And ((SALEORDERONMTRS = False And Val(txtQTY.Text.Trim) > 0) Or (SALEORDERONMTRS = True And Val(TXTMTRS.Text.Trim) > 0)) Then
                'If ClientName = "SOFTAS" And EDIT = False And Val(TXTMTRSBAL.Text.Trim) < Val(TXTMTRS.Text.Trim) Then
                '    If MsgBox("Mtrs Greater than Balance Stock, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                'End If
                'fillgrid()
                'total()

                'IF COLOR IS NOT BLANK THEN ADD ONLY THAT MATCHING
                If cmbcolor.Text.Trim = "" And GRIDDOUBLECLICK = False And ClientName = "SOFTAS" Then
                    If MsgBox("Enter Order for all Shade?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then GoTo LINESINGLE
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.Execute_Any_String(" SELECT COLORMASTER.COLOR_name AS MATCHING FROM DESIGNMASTER INNER JOIN DESIGNMASTER_COLOR ON DESIGNMASTER.DESIGN_id = DESIGNMASTER_COLOR.DESIGN_ID INNER JOIN COLORMASTER ON DESIGNMASTER_COLOR.DESIGN_COLORID = COLORMASTER.COLOR_id WHERE DESIGNMASTER.DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' AND DESIGNMASTER.DESIGN_YEARID = " & YearId, "", "")
                    For Each DTROW As DataRow In DT.Rows

                        GETSTOCK(cmbitemname.Text.Trim, CMBDESIGN.Text.Trim, DTROW("MATCHING"))
                        If ClientName = "SOFTAS" And EDIT = False And Val(TXTMTRSBAL.Text.Trim) < Val(TXTMTRS.Text.Trim) Then
                            If MsgBox("Mtrs Greater than Balance Stock, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                        End If

                        CALC()
                        fillgrid(DTROW("MATCHING"))
                        TOTAL()
                    Next
                Else
LINESINGLE:

                    If ClientName = "SOFTAS" And EDIT = False And Val(TXTMTRSBAL.Text.Trim) < Val(TXTMTRS.Text.Trim) Then
                        If MsgBox("Mtrs Greater than Balance Stock, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    End If
                    fillgrid(cmbcolor.Text.Trim)
                    TOTAL()
                End If
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTCUT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTMTRS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTMTRS.Validating, TXTCUT.Validating
        CALC()
    End Sub

    Private Sub cmbitemname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbitemname.KeyDown
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

    Private Sub CMBQUALITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJQ As New SelectQuality
                OBJQ.FRMSTRING = "QUALITY"
                OBJQ.ShowDialog()
                If OBJQ.TEMPNAME <> "" Then CMBQUALITY.Text = OBJQ.TEMPNAME
            End If
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

    Private Sub cmbcolor_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbcolor.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCOLOR As New SelectShade
                OBJCOLOR.ShowDialog()
                If OBJCOLOR.TEMPNAME <> "" Then cmbcolor.Text = OBJCOLOR.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTREMARKS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXTREMARKS.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then TXTREMARKS.Text = OBJREMARKS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBHASTE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBHASTE.Enter
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBHASTE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" ADDRESS_alias ", "", "   ADDRESSMASTER INNER JOIN LEDGERS ON ADDRESSMASTER.ADDRESS_LEDGERID = LEDGERS.ACC_id AND ADDRESSMASTER.ADDRESS_cmpid = LEDGERS.ACC_cmpid AND ADDRESSMASTER.ADDRESS_locationid = LEDGERS.ACC_locationid AND ADDRESSMASTER.ADDRESS_yearid = LEDGERS.ACC_yearid  ", "  and  LEDGERS.ACC_cmpname ='" & cmbname.Text.Trim & "' and ADDRESS_cmpid=" & CmpId & " and ADDRESS_Locationid=" & Locationid & " and ADDRESS_Yearid=" & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "ADDRESS_alias"
                    CMBHASTE.DataSource = dt
                    CMBHASTE.DisplayMember = "ADDRESS_alias"
                End If
                CMBHASTE.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub CMBITEMCODE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMCODE.Validated
        Try
            If CMBITEMCODE.Text.Trim <> "" Then
                GETSTOCK(cmbitemname.Text.Trim, CMBDESIGN.Text.Trim, cmbcolor.Text.Trim)
                Dim DTITEM As New DataTable
                If ClientName = "SAFFRON" Then
                    Dim OBJ As New SelectItemSO
                    OBJ.ITEMNAME = cmbitemname.Text.Trim
                    OBJ.ShowDialog()
                    DTITEM = OBJ.DT
                End If
                If DTITEM.Rows.Count > 0 Then
                    For Each DTROWPS As DataRow In DTITEM.Rows
                        GRIDSO.Rows.Add(0, cmbitemname.Text.Trim, "", "", "", DTROWPS("COLOR"), "", Format(Val(DTROWPS("ORDERPCS")), "0.00"), DTROWPS("UNIT"), 0, Format(Val(DTROWPS("ORDERMTRS")), "0.00"), 0, "Mtrs", 0, 0, 0, 0, 0, 0)
                        'GRIDSO.Rows.Add(Val(txtsrno.Text.Trim), cmbitemname.Text.Trim, CMBQUALITY.Text.Trim, CMBDESIGN.Text.Trim, txtgridremarks.Text.Trim, cmbcolor.Text.Trim, Format(Val(txtQTY.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Format(Val(TXTAMOUNT.Text.Trim), "0.00"), 0, 0)
                    Next
                    GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1
                    getsrno(GRIDSO)
                End If
                CMBQUALITY.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETSTOCK(ByVal ITEMNAME As String, ByVal DESIGNNO As String, ByVal COLOR As String)
        Try
            If ITEMNAME <> "" Then
                'GET STOCK OF THIS ITEM
                TXTPCSINHOUSE.Clear()
                TXTMTRSINHOUSE.Clear()
                TXTPCSJOBBER.Clear()
                TXTMTRSJOBBER.Clear()
                TXTPCSPENDINGPO.Clear()
                TXTMTRSPENDINGPO.Clear()
                TXTPCSPENDINGSO.Clear()
                TXTMTRSPENDINGSO.Clear()
                TXTPACKINGPCS.Clear()
                TXTPACKINGMTRS.Clear()
                TXTPCSBAL.Clear()
                TXTMTRSBAL.Clear()

                TXTSTOCKITEMNAME.Text = ITEMNAME
                Dim WHERECLAUSE As String = " AND YEARID = " & YearId & " AND ITEMNAME = '" & ITEMNAME & "'"
                WHERECLAUSE = WHERECLAUSE & " AND DESIGNNO = '" & DESIGNNO & "'"
                WHERECLAUSE = WHERECLAUSE & " AND COLOR = '" & COLOR & "'"


                Dim OBJCMN As New ClsCommon
                Dim DTUNIT As DataTable = OBJCMN.SEARCH("UNIT_ABBR", "", "DEFAULTSTOCKUNIT", "")
                Dim UNITCLAUSE As String = ""
                If DTUNIT.Rows.Count > 0 Then UNITCLAUSE = " AND UNIT IN (SELECT UNIT_ABBR FROM DEFAULTSTOCKUNIT)"

                'IN HOUSE STOCK
                Dim DT As New DataTable
                If ALLOWBARCODEPRINT = True Then
                    DT = OBJCMN.SEARCH("ISNULL(SUM(PCS),0) AS PCS, ISNULL(SUM(MTRS),0) AS MTRS", "", "BARCODESTOCK", WHERECLAUSE & UNITCLAUSE & " AND ROUND(MTRS,0) > 0 ")
                Else
                    DT = OBJCMN.SEARCH("ROUND(SUM(PCS) - SUM(ISSPCS),2) AS PCS, ROUND(SUM(MTRS) - SUM(ISSMTRS),2) AS MTRS", "", "STOCKREGISTER", WHERECLAUSE & " GROUP BY ITEMNAME, DESIGNNO, COLOR, YEARID HAVING  ROUND(SUM(MTRS) - SUM(ISSMTRS), 2) > 0")
                End If
                If DT.Rows.Count > 0 Then
                    TXTPCSINHOUSE.Text = Val(DT.Rows(0).Item("PCS"))
                    TXTMTRSINHOUSE.Text = Val(DT.Rows(0).Item("MTRS"))
                End If


                'ISSUE PACKING STOCK
                DT = OBJCMN.SEARCH(" ISNULL(COUNT(ISSUEPACKING_DESC.ISS_BARCODE), 0) AS PCS, ISNULL(SUM(ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2)),0) AS MTRS ", "", "  ITEMMASTER INNER JOIN ISSUEPACKING_DESC ON ITEMMASTER.item_id = ISSUEPACKING_DESC.ISS_ITEMID LEFT OUTER JOIN COLORMASTER ON ISSUEPACKING_DESC.ISS_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ISSUEPACKING_DESC.ISS_DESIGNID = DESIGNMASTER.DESIGN_id INNER JOIN ISSUEPACKING ON ISSUEPACKING.ISS_NO = ISSUEPACKING_DESC.ISS_NO AND ISSUEPACKING.ISS_YEARID = ISSUEPACKING_DESC.ISS_YEARID ", " AND ISSUEPACKING.ISS_YEARID = " & YearId & " AND ITEMMASTER.ITEM_NAME = '" & ITEMNAME & "' AND ISNULL(DESIGNMASTER.DESIGN_NO,'') = '" & DESIGNNO & "' AND ISNULL(COLORMASTER.COLOR_NAME,'') = '" & COLOR & "' AND ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISNULL(ISSUEPACKING_DESC.ISS_OUTMTRS, 0), 2) > 0 ")
                If DT.Rows.Count > 0 Then
                    TXTPACKINGPCS.Text = Val(DT.Rows(0).Item("PCS"))
                    TXTPACKINGMTRS.Text = Val(DT.Rows(0).Item("MTRS"))
                End If


                'JOBBER STOCK
                DT = OBJCMN.SEARCH("ISNULL(SUM(BALPCS),0) As PCS, ISNULL(SUM(BALMTRS),0) As MTRS", "", "LOT_VIEW", " And YEARID = " & YearId & " And ITEMNAME = '" & ITEMNAME & "' AND DESIGN = '" & DESIGNNO & "' AND SHADE = '" & COLOR & "' AND ROUND(BALMTRS,0) > 0 ")
                If DT.Rows.Count > 0 Then
                    TXTPCSJOBBER.Text = Val(DT.Rows(0).Item("PCS"))
                    TXTMTRSJOBBER.Text = Val(DT.Rows(0).Item("MTRS"))
                End If


                'PENDING PO
                DT = OBJCMN.SEARCH("ISNULL(SUM(ALLPURCHASEORDER_DESC.PO_QTY - ALLPURCHASEORDER_DESC.PO_RECDQTY),0) AS PCS, ISNULL(SUM(ALLPURCHASEORDER_DESC.PO_MTRS - ALLPURCHASEORDER_DESC.PO_RECDMTRS),0) AS MTRS", "", " ALLPURCHASEORDER_DESC INNER JOIN ITEMMASTER ON ALLPURCHASEORDER_DESC.PO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON ALLPURCHASEORDER_DESC.PO_DESIGNID = DESIGNMASTER.DESIGN_ID LEFT OUTER JOIN COLORMASTER ON ALLPURCHASEORDER_DESC.PO_COLORID = COLORMASTER.COLOR_id ", " AND ROUND(ALLPURCHASEORDER_DESC.PO_MTRS - ALLPURCHASEORDER_DESC.PO_RECDMTRS, 2) > 0 AND ALLPURCHASEORDER_DESC.PO_CLOSED = 'FALSE' AND ITEMMASTER.ITEM_NAME = '" & ITEMNAME & "' AND ISNULL(DESIGNMASTER.DESIGN_NO,'') = '" & DESIGNNO & "' AND ISNULL(COLOR_NAME,'') = '" & COLOR & "' AND ALLPURCHASEORDER_DESC.PO_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTPCSPENDINGPO.Text = Val(DT.Rows(0).Item("PCS"))
                    TXTMTRSPENDINGPO.Text = Val(DT.Rows(0).Item("MTRS"))
                End If


                'PENDINGSALEORDER
                DT = OBJCMN.SEARCH("ISNULL(SUM(ALLSALEORDER_DESC.SO_QTY - ALLSALEORDER_DESC.SO_RECDQTY),0) AS PCS, ISNULL(SUM(ALLSALEORDER_DESC.SO_MTRS - ALLSALEORDER_DESC.SO_RECDMTRS),0) AS MTRS", "", " ALLSALEORDER_DESC INNER JOIN ITEMMASTER ON ALLSALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON ALLSALEORDER_DESC.SO_DESIGNID = DESIGNMASTER.DESIGN_ID LEFT OUTER JOIN COLORMASTER ON ALLSALEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_id ", " AND ROUND(ALLSALEORDER_DESC.SO_MTRS - ALLSALEORDER_DESC.SO_RECDMTRS, 2) > 0 AND ALLSALEORDER_DESC.SO_CLOSED = 'FALSE' AND ITEMMASTER.ITEM_NAME = '" & ITEMNAME & "' AND ISNULL(DESIGNMASTER.DESIGN_NO,'') = '" & DESIGNNO & "' AND ISNULL(COLOR_NAME,'') = '" & COLOR & "' AND ALLSALEORDER_DESC.SO_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTPCSPENDINGSO.Text = Val(DT.Rows(0).Item("PCS"))
                    TXTMTRSPENDINGSO.Text = Val(DT.Rows(0).Item("MTRS"))
                End If

                TXTPCSBAL.Text = Val(TXTPCSINHOUSE.Text.Trim) + Val(TXTPACKINGPCS.Text.Trim) + Val(TXTPCSJOBBER.Text.Trim) + Val(TXTPCSPENDINGPO.Text.Trim) - Val(TXTPCSPENDINGSO.Text.Trim)
                TXTMTRSBAL.Text = Val(TXTMTRSINHOUSE.Text.Trim) + Val(TXTPACKINGMTRS.Text.Trim) + Val(TXTMTRSJOBBER.Text.Trim) + Val(TXTMTRSPENDINGPO.Text.Trim) - Val(TXTMTRSPENDINGSO.Text.Trim)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SODATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SODATE.Validating
        Try
            If SODATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(SODATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then fillagentledger(CMBAGENT, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSALESMAN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBSALESMAN.Enter
        Try
            If CMBSALESMAN.Text.Trim = "" Then FILLSALESMAN(CMBSALESMAN)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSALESMAN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSALESMAN.Validating
        Try
            If CMBSALESMAN.Text.Trim <> "" Then SALESMANVALIDATE(CMBSALESMAN, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTADVANCE_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTADVANCE.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTADVANCE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTADVANCE.Validated
        Try
            If Val(TXTADVANCE.Text.Trim) > 0 And Val(txtgrandtotal.Text.Trim) > 0 Then
                TXTBALANCE.Text = Val(txtgrandtotal.Text.Trim) - Val(TXTADVANCE.Text.Trim)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TXTSONO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTSONO.Validating
        Try

            If Val(TXTSONO.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.SEARCH("  ISNULL(so_no, 0) AS SONO ", "", " SALEORDER ", "  AND SALEORDER.so_no=" & TXTSONO.Text.Trim & " AND SALEORDER.SO_CMPID = " & CmpId & " AND SALEORDER.SO_LOCATIONID = " & Locationid & " AND SALEORDER.SO_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Sale Order No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTSONO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTSONO.KeyPress
        Try
            numkeypress(e, TXTSONO, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMCODE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMCODE.Enter
        Try
            If CMBITEMCODE.Text.Trim = "" Then fillitemcode(CMBITEMCODE, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CMBITEMCODE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMCODE.Validating
        Try
            If CMBITEMCODE.Text.Trim <> "" Then itemcodevalidate(CMBITEMCODE, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT", cmbitemname.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLSMS.Click
        If EDIT = False Then Exit Sub
        SMSCODE()
    End Sub

    Sub SMSCODE()
        If ALLOWSMS = True And TXTMOBILENO.Text.Trim <> "" Then
            If MsgBox("Send SMS?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim MSG As String = ""
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            'If ClientName = "YASHVI" Then
            '    MSG = "ORD NO-" & Val(TXTSONO.Text.Trim) & "\n"
            '    DT = OBJCMN.search("DISTINCT ITEMMASTER.ITEM_NAME AS ITEMNAME ", "", "SALEORDER_DESC INNER JOIN ITEMMASTER ON SO_ITEMID = ITEM_ID ", " AND SO_NO = " & Val(TXTSONO.Text.Trim) & " AND SO_YEARID = " & YearId)
            '    For Each DTROW As DataRow In DT.Rows
            '        MSG = MSG & DTROW("ITEMNAME") & "\n"
            '    Next
            '    MSG = MSG & Val(lbltotalqty.Text.Trim) & " THAN" & "\n"
            '    MSG = MSG & Val(LBLTOTALMTRS.Text.Trim) & " MTRS" & "\n"
            '    MSG = MSG & CmpName
            'Else
            '    MSG = "SONO-" & Val(TEMPSONO) & "\n"
            '    MSG = MSG & "PONO-" & Val(txtpono.Text.Trim) & " & INDENT-" & TXTREFNO.Text.Trim & "\n"
            '    For Each ROW As DataGridViewRow In GRIDSO.Rows
            '        If ROW.Cells(gqtyunit.Index).Value = "Mtrs" Then MSG = MSG & Val(ROW.Cells(gsrno.Index).Value) & ") " & ROW.Cells(gitemname.Index).Value & "-" & ROW.Cells(gdesc.Index).Value & " " & Format(Val(ROW.Cells(GMTRS.Index).Value), "0.00") & " " & ROW.Cells(gqtyunit.Index).Value & "\n" Else MSG = MSG & Val(ROW.Cells(gsrno.Index).Value) & ") " & ROW.Cells(gitemname.Index).Value & "-" & ROW.Cells(gdesc.Index).Value & " " & Format(Val(ROW.Cells(gQty.Index).Value), "0") & " " & ROW.Cells(gqtyunit.Index).Value & "\n"
            '        MSG = MSG & "Rs:- " & Format(Val(ROW.Cells(GRATE.Index).Value), "0.00") & "/" & ROW.Cells(gqtyunit.Index).Value & "\n"
            '    Next
            '    MSG = MSG & "Del Dt:- " & Format(DUEDATE.Value.Date, "dd/MM/yyyy") & "\n"
            '    MSG = MSG & TXTREMARKS.Text.Trim

            'End If


            If ClientName = "YASHVI" Then
                MSG = "ORD NO-" & Val(TXTSONO.Text.Trim) & "\n"
                DT = OBJCMN.SEARCH("DISTINCT ITEMMASTER.ITEM_NAME AS ITEMNAME ", "", "SALEORDER_DESC INNER JOIN ITEMMASTER ON SO_ITEMID = ITEM_ID ", " AND SO_NO = " & Val(TXTSONO.Text.Trim) & " AND SO_YEARID = " & YearId)
                For Each DTROW As DataRow In DT.Rows
                    MSG = MSG & DTROW("ITEMNAME") & "\n"
                Next
                MSG = MSG & Val(lbltotalqty.Text.Trim) & " THAN" & "\n"
                MSG = MSG & Val(LBLTOTALMTRS.Text.Trim) & " MTRS" & "\n"
                MSG = MSG & CmpName


            ElseIf ClientName = "KOTHARI" Then
                MSG = MSG & CMBPACKING.Text.Trim & " - " & cmbcity.Text.Trim & "\n"
                MSG = "SONO-" & Val(TEMPSONO) & " " & "DT." & " " & SODATE.Text.Trim & "\n"
                For Each ROW As DataGridViewRow In GRIDSO.Rows
                    If ROW.Cells(gqtyunit.Index).Value = "Mtrs" Then MSG = MSG & Val(ROW.Cells(gsrno.Index).Value) & ") " & ROW.Cells(gitemname.Index).Value & "-" & ROW.Cells(gdesc.Index).Value & " " & Format(Val(ROW.Cells(GMTRS.Index).Value), "0.00") & " " & ROW.Cells(gqtyunit.Index).Value & "\n" Else MSG = MSG & Val(ROW.Cells(gsrno.Index).Value) & ") " & ROW.Cells(gitemname.Index).Value & "-" & ROW.Cells(gdesc.Index).Value & "-" & Format(Val(ROW.Cells(gQty.Index).Value), "0") & " " & ROW.Cells(gqtyunit.Index).Value & "-" & Format(Val(ROW.Cells(GMTRS.Index).Value), "0.00") & " " & "MTRS" & " " & Format(Val(ROW.Cells(GRATE.Index).Value), "0.00") & " " & "PER" & " " & ROW.Cells(gqtyunit.Index).Value & "\n"
                Next

            Else

                MSG = "SONO-" & Val(TEMPSONO) & "\n"
                MSG = MSG & "PONO-" & Val(txtpono.Text.Trim) & " & INDENT-" & TXTREFNO.Text.Trim & "\n"
                For Each ROW As DataGridViewRow In GRIDSO.Rows
                    If ROW.Cells(gqtyunit.Index).Value = "Mtrs" Then MSG = MSG & Val(ROW.Cells(gsrno.Index).Value) & ") " & ROW.Cells(gitemname.Index).Value & "-" & ROW.Cells(gdesc.Index).Value & " " & Format(Val(ROW.Cells(GMTRS.Index).Value), "0.00") & " " & ROW.Cells(gqtyunit.Index).Value & "\n" Else MSG = MSG & Val(ROW.Cells(gsrno.Index).Value) & ") " & ROW.Cells(gitemname.Index).Value & "-" & ROW.Cells(gdesc.Index).Value & "-" & Format(Val(ROW.Cells(gQty.Index).Value), "0") & " " & ROW.Cells(gqtyunit.Index).Value & "\n"
                    MSG = MSG & "Rs:- " & Format(Val(ROW.Cells(GRATE.Index).Value), "0.00") & "/" & ROW.Cells(gqtyunit.Index).Value & "\n"
                Next
                MSG = MSG & "Del Dt:- " & Format(DUEDATE.Value.Date, "dd/MM/yyyy") & "\n"
                MSG = MSG & TXTREMARKS.Text.Trim

            End If

            If SENDMSG(MSG, TXTMOBILENO.Text.Trim) = "1701" Then
                MsgBox("Message Sent")
                DT = OBJCMN.Execute_Any_String("UPDATE SALEORDER SET SO_SMSSEND = 1 WHERE SO_NO = " & TEMPSONO & " AND SO_YEARID = " & YearId, "", "")
                LBLSMS.Visible = True
            Else
                MsgBox("Error Sending Message")
            End If
        End If
        'End If
    End Sub

    Private Sub CMBDESIGN_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBDESIGN.Validated
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If CMBDESIGN.Text.Trim <> "" And GRIDDOUBLECLICK = False Then


                'GET ITEMNAME AUTO
                If (ClientName = "AVIS" Or ClientName = "KRISHNA" Or ClientName = "NTC") And CMBDESIGN.Text.Trim <> "" Then
                    DT = OBJCMN.SEARCH("ISNULL(ITEM_NAME,'') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEM_ID", " AND DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' AND DESIGN_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then cmbitemname.Text = DT.Rows(0).Item("ITEMNAME")
                End If

                If ClientName <> "SHREENAKODA" Then GETSTOCK(cmbitemname.Text.Trim, CMBDESIGN.Text.Trim, cmbcolor.Text.Trim)
                Dim DTITEM As New DataTable

                'OPEN THIS BOX IF SHADES ARE PRESENT FOR SELECTED DESIGN
                DT = OBJCMN.SEARCH(" DESIGNMASTER_COLOR.DESIGN_SRNO", "", " DESIGNMASTER INNER JOIN DESIGNMASTER_COLOR ON DESIGNMASTER.DESIGN_id = DESIGNMASTER_COLOR.DESIGN_ID ", " AND DESIGNMASTER.DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' AND DESIGNMASTER.DESIGN_YEARID = " & YearId)
                If FETCHITEMWISEDESIGN = True And DT.Rows.Count > 0 And (ClientName = "YASHVI" Or ClientName = "MAHAVIRPOLYCOT") Then
                    Dim OBJ As New SelectItemSO
                    OBJ.ITEMNAME = cmbitemname.Text.Trim
                    OBJ.DESIGNNO = CMBDESIGN.Text.Trim
                    OBJ.UNIT = cmbqtyunit.Text.Trim
                    OBJ.ShowDialog()
                    DTITEM = OBJ.DT
                End If
                If DTITEM.Rows.Count > 0 Then
                    For Each DTROWPS As DataRow In DTITEM.Rows
                        GRIDSO.Rows.Add(0, cmbitemname.Text.Trim, "", CMBDESIGN.Text.Trim, txtgridremarks.Text.Trim, DTROWPS("COLOR"), "", Format(Val(DTROWPS("ORDERPCS")), "0.00"), cmbqtyunit.Text.Trim, Format(Val(DTROWPS("CUT")), "0.00"), Format(Val(DTROWPS("ORDERMTRS")), "0.00"), Val(TXTRATE.Text.Trim), "Mtrs", 0, 0, 0, 0, 0, 0)
                    Next
                    GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1
                    getsrno(GRIDSO)

                    TOTAL()
                    cmbitemname.Text = ""
                    CMBDESIGN.Text = ""
                    txtgridremarks.Clear()
                    TXTRATE.Clear()
                    cmbitemname.Focus()
                End If

                If ClientName = "SNCM" Then
                    If cmbitemname.Text.Trim <> "" And CMBFORWARD.Text = "READY" Then
                        DT = OBJCMN.SEARCH(" ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_SETMTRS,0) AS SETMTRS ", "", " ITEMDESIGNIMAGE LEFT OUTER JOIN DESIGNMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = DESIGNMASTER.DESIGN_id AND ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = DESIGNMASTER.DESIGN_yearid LEFT OUTER JOIN ITEMMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = ITEMMASTER.item_yearid AND ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = ITEMMASTER.item_id", " AND DESIGNMASTER.DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' AND ITEMMASTER.item_NAME  = '" & cmbitemname.Text.Trim & "' AND DESIGNMASTER.DESIGN_YEARID = " & YearId)
                        If DT.Rows.Count > 0 Then TXTCUT.Text = DT.Rows(0).Item("SETMTRS")
                    End If
                End If

            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitemname.Validated
        Try
            If cmbitemname.Text.Trim <> "" And cmbname.Text.Trim <> "" Then


                'GET LAST RATE OF SELECTED ITEM FOR SELECTED PARTY
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable

                If (ClientName = "MNARESH" Or ClientName = "MSANCHITKUMAR" Or ClientName = "SNCM" Or ClientName = "MASHOK" Or ClientName = "ABHEE") Then
                    LBLRATE.Text = 0.00
                    'DT = OBJCMN.SEARCH(" TOP 1 ISNULL(INVOICEMASTER_DESC.INVOICE_RATE,0) AS LASTRATE", "", " INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN INVOICEMASTER ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO  AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN LEDGERS ON ACC_ID = INVOICE_LEDGERID", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND INVOICEMASTER.INVOICE_DATE < '" & Format(Convert.ToDateTime(SODATE.Text).Date, "MM/dd/yyyy") & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER.INVOICE_NO DESC")
                    DT = OBJCMN.SEARCH(" TOP 1 ISNULL(INVOICEMASTER_DESC.INVOICE_RATE,0) AS LASTRATE", "", " INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN INVOICEMASTER ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO  AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN LEDGERS ON ACC_ID = INVOICE_LEDGERID", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND INVOICEMASTER.INVOICE_DATE < '" & Format(Convert.ToDateTime(SODATE.Text).Date, "MM/dd/yyyy") & "'  ORDER BY INVOICEMASTER.INVOICE_DATE DESC")
                    If DT.Rows.Count > 0 Then LBLRATE.Text = Format(Val(DT.Rows(0).Item("LASTRATE")), "0.00")
                End If

                If ClientName = "MAHAVIRPOLYCOT" Then CMBDESIGN.Text = ""
                If ClientName = "SOFTAS" Then CMBDESIGN.Text = cmbitemname.Text.Trim

                Dim DTITEM As New DataTable

                'GET STAMPING
                Dim WHERECLAUSE As String = ""
                If (ClientName = "YASHVI" Or ClientName = "SOFTAS" Or ClientName = "MSANCHITKUMAR") Then WHERECLAUSE = " AND ledgers.acc_cmpname = '" & cmbname.Text.Trim & "' "
                If ClientName = "RAJKRIPA" Or ClientName = "YASHVI" Or ClientName = "SOFTAS" Or ClientName = "MSANCHITKUMAR" Then
                    DT = OBJCMN.SEARCH(" ISNULL(PARTYITEMWISECHART.PAR_STAMPING, '') AS STAMPING, ISNULL(PAR_RATE,0) AS RATE", "", " PARTYITEMWISECHART LEFT OUTER JOIN LEDGERS ON PARTYITEMWISECHART.PAR_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON PARTYITEMWISECHART.PAR_ITEMID = ITEMMASTER.item_id ", WHERECLAUSE & " AND ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & " ' AND PARTYITEMWISECHART.PAR_YEARID = " & YearId)
                    If DT.Rows.Count > 0 AndAlso txtgridremarks.Text.Trim = "" Then
                        CMBGRIDREMARKS.Text = DT.Rows(0).Item("STAMPING")
                        txtgridremarks.Text = DT.Rows(0).Item("STAMPING")
                        TXTRATE.Text = Val(DT.Rows(0).Item("RATE"))
                    End If
                End If


                'GET RATE
                'FOR THIS WE NEED TO GET THE COLUMN NAME FROM RATETYPEMASREER 
                DT = OBJCMN.SEARCH(" ISNULL(LEDGERS.ACC_PRICELISTCOLUMN, '') AS COLNAME", "", " LEDGERS ", " AND ledgers.acc_cmpname = '" & cmbname.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("COLNAME") <> "" Then
                    Dim DTRATE As DataTable = OBJCMN.SEARCH(DT.Rows(0).Item("COLNAME") & " AS RATE", "", "ITEMPRICELIST INNER JOIN ITEMMASTER ON ITEMPRICELIST.ITEMID = ITEMMASTER.item_id ", " AND ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                    If DTRATE.Rows.Count > 0 AndAlso Val(TXTRATE.Text.Trim) = 0 Then TXTRATE.Text = Val(DTRATE.Rows(0).Item("RATE"))
                End If

                If ClientName = "KARAN" Or ClientName = "VINTAGEINDIA" Then
                    Dim DTRATE As DataTable = OBJCMN.SEARCH("PRICELISTMASTER.PL_RATE AS SALERATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME ", "", "PRICELISTMASTER INNER JOIN ITEMMASTER ON PRICELISTMASTER.PL_ITEMID = ITEMMASTER.item_id INNER JOIN LEDGERS ON PRICELISTMASTER.PL_LEDGERID = LEDGERS.ACC_ID ", " AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '" & cmbname.Text.Trim & "' AND ISNULL(ITEMMASTER.ITEM_NAME,'') = '" & cmbitemname.Text.Trim & "' AND PL_YEARID = " & YearId)
                    If DTRATE.Rows.Count > 0 AndAlso Val(TXTRATE.Text.Trim) = 0 Then TXTRATE.Text = Val(DTRATE.Rows(0).Item("RATE"))
                End If

                If (ClientName = "MAHAVIR" Or ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "SMS" Or ClientName = "RAJKRIPA" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "SIDDHPOLYCOT" Or ClientName = "SIDDHGIRI" Or ClientName = "MASHOK" Or ClientName = "ABHEE" Or ClientName = "AFW") Then
                    DT = OBJCMN.SEARCH("  ISNULL(item_reorder, 0) AS CUT, ISNULL(ITEM_RATE, 0) AS RATE,ISNULL(ITEM_FOLD, '') AS [DESC],ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(CATEGORY_NAME,'') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEM_CATEGORYID = CATEGORY_ID LEFT OUTER JOIN UNITMASTER ON ITEMMASTER.item_unitid = UNITMASTER.unit_id ", " AND ITEMMASTER.item_name = '" & cmbitemname.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID='" & YearId & "' ")
                    If DT.Rows.Count > 0 Then
                        If ClientName <> "SIDDHGIRI" And ClientName <> "MASHOK" And ClientName <> "ABHEE" And ClientName <> "AFW" Then TXTCUT.Text = DT.Rows(0).Item("CUT")
                        LBLCATEGORY.Text = DT.Rows(0).Item("CATEGORY")

                        'COZ RATE WILL BE FETCHED FROM PARTYITEMWISECHART, IF CHKFETCHDESC IS TRUE THEN 
                        If CHKFETCHDESC.Checked = False Then TXTRATE.Text = DT.Rows(0).Item("RATE")

                        If LCase(DT.Rows(0).Item("UNIT")) = "pcs" Then
                            CMBPER.Text = "Qty"
                            cmbqtyunit.Text = "Pcs"
                        Else
                            CMBPER.Text = "Mtrs"
                        End If
                        If ClientName = "SIDDHGIRI" Or ClientName = "MASHOK" Or ClientName = "ABHEE" Then cmbqtyunit.Text = DT.Rows(0).Item("UNIT")
                        If ClientName = "MAHAVIR" Then txtgridremarks.Text = DT.Rows(0).Item("DESC")
                    End If
                End If



                'OPEN THIS BOX IF SHADES ARE PRESENT FOR SELECTED DESIGN
                DT = OBJCMN.SEARCH(" ITEMMASTER_COLOR.ITEM_SRNO", "", " ITEMMASTER INNER JOIN ITEMMASTER_COLOR ON ITEMMASTER.ITEM_id = ITEMMASTER_COLOR.ITEM_ID ", " AND ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                If FETCHITEMWISESHADE = True And DT.Rows.Count > 0 Then
                    Dim OBJ As New SelectItemSO
                    OBJ.ITEMNAME = cmbitemname.Text.Trim
                    OBJ.DESIGNNO = CMBDESIGN.Text.Trim
                    OBJ.UNIT = cmbqtyunit.Text.Trim
                    OBJ.ShowDialog()
                    DTITEM = OBJ.DT
                End If
                If DTITEM.Rows.Count > 0 Then
                    For Each DTROWPS As DataRow In DTITEM.Rows
                        GRIDSO.Rows.Add(0, cmbitemname.Text.Trim, "", CMBDESIGN.Text.Trim, txtgridremarks.Text.Trim, DTROWPS("COLOR"), "", Format(Val(DTROWPS("ORDERPCS")), "0.00"), cmbqtyunit.Text.Trim, Format(Val(DTROWPS("CUT")), "0.00"), Format(Val(DTROWPS("ORDERMTRS")), "0.00"), Val(TXTRATE.Text.Trim), "Mtrs", 0, 0, 0, 0, 0, 0)
                    Next
                    GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1
                    getsrno(GRIDSO)

                    cmbitemname.Text = ""
                    CMBDESIGN.Text = ""
                    txtgridremarks.Clear()
                    TXTRATE.Clear()
                    cmbitemname.Focus()
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtgridremarks_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtgridremarks.Validated
        Try
            'MAKE THIS STAMPING DEFAULT FOR PARTY
            If (ClientName = "YASHVI" Or ClientName = "SOFTAS" Or ClientName = "MSANCHITKUMAR") And txtgridremarks.Text.Trim <> "" And cmbname.Text.Trim <> "" And cmbitemname.Text.Trim <> "" Then

                'FIRST CHECK WHETHER THIS STAMP FOR THIS PARTY AND ITEM IS PRESENT OR NOT, IF NOT THEN CREATE NEW OR ELSE UPDATE
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("PAR_STAMPING AS STAMPING, PAR_NO AS PARNO", "", "PARTYITEMWISECHART INNER JOIN LEDGERS ON ACC_ID = PAR_LEDGERID INNER JOIN ITEMMASTER ON ITEM_ID = PAR_ITEMID", " AND ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND PAR_YEARID = " & YearId)
                If DT.Rows.Count > 0 AndAlso LCase(DT.Rows(0).Item("STAMPING")) <> LCase(txtgridremarks.Text.Trim) Then
                    If MsgBox("Wish to Make this Stamp Default for this Party & Item?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    DT = OBJCMN.Execute_Any_String("UPDATE PARTYITEMWISECHART SET PAR_STAMPING = '" & txtgridremarks.Text.Trim & "' WHERE PAR_NO = " & Val(DT.Rows(0).Item("PARNO")) & " AND PAR_YEARID = " & YearId, "", "")
                ElseIf DT.Rows.Count = 0 Then
                    'ADD NEW STAMPING
                    Dim ALPARAVAL As New ArrayList
                    Dim OBJCONFIG As New ClsPartyItemWiseChart

                    ALPARAVAL.Add(0)
                    ALPARAVAL.Add(cmbname.Text.Trim)
                    ALPARAVAL.Add(cmbitemname.Text.Trim)
                    ALPARAVAL.Add(0)    'RATE
                    ALPARAVAL.Add(txtgridremarks.Text.Trim)
                    ALPARAVAL.Add(CmpId)
                    ALPARAVAL.Add(Userid)
                    ALPARAVAL.Add(YearId)

                    OBJCONFIG.alParaval = ALPARAVAL

                    Dim INT As Integer = OBJCONFIG.SAVE()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Validated
        Try
            If cmbname.Text.Trim <> "" And CMBPACKING.Text.Trim = "" And ClientName <> "INDRAPUJAIMPEX" Then CMBPACKING.Text = cmbname.Text.Trim
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKINGTYPE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBPACKINGTYPE.Enter
        Try
            If CMBPACKINGTYPE.Text.Trim = "" Then FILLPACKINGTYPE(CMBPACKINGTYPE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKINGTYPE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPACKINGTYPE.Validating
        Try
            If CMBPACKINGTYPE.Text.Trim <> "" Then PACKINGTYPEVALIDATE(CMBPACKINGTYPE, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTCOPYSONO_Validated(sender As Object, e As EventArgs) Handles TXTCOPYSONO.Validated
        Try
            If Val(TXTCOPYSONO.Text.Trim) > 0 And EDIT = False Then
                If MsgBox("Wish to Copy SO No " & Val(TXTCOPYSONO.Text.Trim) & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                Dim objclsSO As New ClsSaleOrder()
                Dim dt_po As DataTable = objclsSO.selectSo(Val(TXTCOPYSONO.Text.Trim), CmpId, Locationid, YearId)

                If dt_po.Rows.Count > 0 Then
                    For Each dr As DataRow In dt_po.Rows

                        cmbname.Text = Convert.ToString(dr("NAME"))
                        cmbname.Enabled = True
                        TXTMOBILENO.Text = Convert.ToString(dr("MOBILENO"))

                        CMBHASTE.Text = Convert.ToString(dr("HASTE"))
                        CMBAGENT.Text = Convert.ToString(dr("AGENT"))
                        CMBRISK.Text = Convert.ToString(dr("RISK"))

                        txtpono.Text = Convert.ToString(dr("PONO"))
                        DUEDATE.Value = Convert.ToDateTime(dr("DUEDATE"))
                        daysremains()

                        cmbtrans.Text = Convert.ToString(dr("TRANS"))
                        cmbtrans2.Text = Convert.ToString(dr("TRANS2"))
                        cmbcity.Text = Convert.ToString(dr("CITY"))
                        TXTREFNO.Text = Convert.ToString(dr("REFNO"))

                        TXTCONSIGNEE.Text = Convert.ToString(dr("CONSIGNEE"))
                        TXTCONSIGNOR.Text = Convert.ToString(dr("CONSIGNOR"))
                        CMBPACKING.Text = Convert.ToString(dr("PACKING"))
                        CMBCURRENCY.Text = Convert.ToString(dr("CURRENCY"))
                        txtnote.Text = Convert.ToString(dr("NOTE"))
                        txttnc.Text = Convert.ToString(dr("TNC"))
                        TXTREMARKS.Text = Convert.ToString(dr("REMARKS"))

                        CMBSALESMAN.Text = Convert.ToString(dr("SALESMAN"))
                        TXTADVANCE.Text = Convert.ToString(dr("ADVANCE"))
                        TXTBALANCE.Text = Convert.ToString(dr("BALANCE"))
                        CMBPACKINGTYPE.Text = Convert.ToString(dr("PACKINGTYPE"))

                        CMBFORWARD.Text = dr("FORWARD")
                        CMBORDERON.Text = dr("ORDERON")

                        GRIDSO.Rows.Add(dr("SRNO").ToString, dr("ITEM").ToString, dr("QUALITY").ToString, dr("DESIGN").ToString, dr("GRIDREMARKS").ToString, dr("COLOR").ToString, dr("PARTYPONO"), Format(Val(dr("QTY")), "0.00"), dr("UNIT").ToString, Format(Val(dr("CUT")), "0.00"), Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER"), Format(Val(dr("AMOUNT")), "0.00"), 0, 0, 0, 0, 0)



                        txtdisper.Text = dr("DISPER")
                        txtdisamt.Text = dr("DISAMT")
                        txtpfper.Text = dr("PFPER")
                        'txtpfamt.Text = dr("PFAMT")
                        txttestchgs.Text = dr("TECTCHGS")
                        txtnett.Text = dr("NETT")

                        If dr("EXCISEAMT") > 0 Then
                            chkexcise.CheckState = CheckState.Checked
                            'TXTEXCISEID.Text = dr("EXCISE")
                            TXTEXCISE.Text = dr("EXCISE")
                            txtexciseAMT.Text = dr("EXCISEAMT")
                            'TXTEDUCESS.Text = dr("EDUCESSNAME")
                            txteducessAMT.Text = dr("EDUCESSAMT")
                            'TXTHSECESS.Text = dr("HSECESSNAME")
                            txthsecessAMT.Text = dr("SO_HSECESSAMT")
                        Else
                            chkexcise.CheckState = CheckState.Unchecked

                        End If

                        'txtsubtotal.Text = dr("SO_SUBTOTAL")
                        cmbtax.Text = dr("TAX")
                        txttax.Text = dr("TAXAMT")
                        cmbaddtax.Text = dr("ADDTAX")
                        txtaddtax.Text = dr("ADDTAXAMT")
                        txtfrper.Text = dr("FRPER")
                        txtfreight.Text = dr("FREIGHT")
                        cmboctroi.Text = dr("OCTROI")
                        txtoctroi.Text = dr("OCTROIAMT")
                        txtinspchgs.Text = dr("INSAMT")
                        txtroundoff.Text = dr("ROUNDOFF")
                        txtgrandtotal.Text = dr("GRANDTOTAL")

                        cmbmisc.Text = dr("MISCPER").ToString
                        txtDiscrate.Text = dr("DISCRATE")
                        txtDiscLot.Text = dr("DISCLOT")
                        txtdd.Text = dr("DISCDEALER")
                        txtkatai.Text = dr("KATAI")
                        txtcd.Text = dr("CD")
                        txtadat.Text = dr("ADAT")
                        txtdays.Text = dr("DAYS")
                        txtint.Text = dr("INT")

                    Next
                    GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1

                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCURRENCY_Enter(sender As Object, e As EventArgs) Handles CMBCURRENCY.Enter
        Try
            If CMBCURRENCY.Text.Trim = "" Then fillCURRENCY(CMBCURRENCY)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SENDWHATSAPP(TEMPSONO)
            DT = OBJCMN.Execute_Any_String("UPDATE SALEORDER SET SO_SENDWHATSAPP = 1 WHERE SO_NO = " & TEMPSONO & " AND SO_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(SONO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJSO As New SaleOrderDesign
            OBJSO.MdiParent = MDIMain
            OBJSO.DIRECTPRINT = True
            OBJSO.FRMSTRING = "SOREPORT"
            OBJSO.DIRECTMAIL = True
            OBJSO.vendorname = cmbname.Text.Trim
            OBJSO.AGENTNAME = CMBAGENT.Text.Trim
            OBJSO.FORMULA = "{saleOrder.so_no}=" & Val(SONO) & " and {saleOrder.SO_yearid}=" & YearId
            OBJSO.SONO = SONO
            OBJSO.NOOFCOPIES = 1
            OBJSO.Show()
            OBJSO.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = cmbname.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBAGENT.Text.Trim
            If cmbname.Text.Trim <> CMBPACKING.Text.Trim Then OBJWHATSAPP.OTHERNAME1 = CMBPACKING.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\_SOREPORT_" & Val(SONO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add("_SOREPORT_" & Val(SONO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCURRENCY_Validating(sender As Object, e As CancelEventArgs) Handles CMBCURRENCY.Validating
        Try
            If CMBCURRENCY.Text.Trim <> "" Then CURRENCYVALIDATE(CMBCURRENCY, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_MouseWheel(sender As Object, e As MouseEventArgs) Handles cmbname.MouseWheel
        If ClientName = "AVIS" Then
            Dim HMEA As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
            HMEA.Handled = True
        End If
    End Sub

    Private Sub cmbcolor_Validated(sender As Object, e As EventArgs) Handles cmbcolor.Validated
        Try
            If (ClientName = "SAFFRON" Or ClientName = "SOFTAS" Or ClientName = "ANOX" Or ClientName = "SIDDHGIRI") And cmbitemname.Text.Trim <> "" Then
                GETSTOCK(cmbitemname.Text.Trim, CMBDESIGN.Text.Trim, cmbcolor.Text.Trim)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBPACKING.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = "  And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')   AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBPACKING.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTBARCODE()
        Try
            If ALLOWBARCODEPRINT = False Then Exit Sub


            Dim TEMPMSG As Integer = MsgBox("Wish to Print Bar Code?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbNo Then Exit Sub


            Dim TEMPHEADER As String = ""
            Dim TEMPCMPNAME As String = ""
            If ClientName = "SOFTAS" Then
                TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR ONLY D.NO" & Chr(13) & "2 FOR ONLY M.NO" & Chr(13) & "3 FOR BOTH D.NO AND M.NO")
                If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
                If MsgBox("Print Softas Name", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then TEMPCMPNAME = "SOFTAS"
                If TEMPCMPNAME = "" Then TEMPCMPNAME = InputBox("Enter Name")
            End If

            Dim dirresults As String = ""



            For Each ROW As DataGridViewRow In GRIDSO.Rows

                'TO PRINT BARCODE FROM SELECTED SRNO
                If (Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0) Then
                    If Val(ROW.Cells(gsrno.Index).Value) < Val(TXTFROM.Text.Trim) Or Val(ROW.Cells(gsrno.Index).Value) > Val(TXTTO.Text.Trim) Then GoTo NEXTLINE
                End If

                For J As Integer = 1 To Val(TXTCOPIES.Text.Trim)

                    'Writing in file
                    Dim oWrite As System.IO.StreamWriter
                    oWrite = File.CreateText(Application.StartupPath & "\Barcode.txt")

                    If ClientName = "SOFTAS" Then

                        oWrite.WriteLine("G0")
                        oWrite.WriteLine("n")
                        oWrite.WriteLine("M0533")
                        oWrite.WriteLine("O0214")
                        oWrite.WriteLine("V0")
                        oWrite.WriteLine("t1")
                        oWrite.WriteLine("Kf0070")
                        oWrite.WriteLine("L")
                        oWrite.WriteLine("D11")
                        oWrite.WriteLine("A2")
                        oWrite.WriteLine("2e4202801470000BTEXTRADE")
                        oWrite.WriteLine("ySPM")

                        If TEMPCMPNAME <> "" Then
                            oWrite.WriteLine("2911C2401540094" & TEMPCMPNAME)
                            oWrite.WriteLine("1X1100000110095L001153")
                        End If

                        If TEMPHEADER = "1" Or TEMPHEADER = "3" Then
                            oWrite.WriteLine("2911C1801890058" & ROW.Cells(GDESIGN.Index).Value)
                        End If


                        If TEMPHEADER = "2" Or TEMPHEADER = "3" Then
                            oWrite.WriteLine("2911C1801890028" & ROW.Cells(gdesc.Index).Value)
                        End If

                        oWrite.WriteLine("Q0001")
                        oWrite.WriteLine("E")
                        oWrite.Dispose()

                    End If


                    'Printing Barcode
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = "cmd.exe"
                    psi.RedirectStandardInput = False
                    psi.RedirectStandardOutput = True
                    psi.Arguments = "/c print " & Application.StartupPath & "\Barcode.txt"    ' specify your command
                    psi.UseShellExecute = False

                    Dim proc As Process
                    proc = Process.Start(psi)
                    dirresults = proc.StandardOutput.ReadToEnd() ' // read from stdout
                    '// do something with result stream
                    proc.WaitForExit()
                    proc.Dispose()

                Next


NEXTLINE:
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                'GET DATA FROM SAMPLE BARCODE
                'no need for yearid clause here as we need to fetch this barcode in all acccouting year
                DT = OBJCMN.SEARCH(" SAMPLEBARCODE.SB_NO AS SBNO, SAMPLEBARCODE.SB_GRIDSRNO AS GRIDSRNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_NAME,'') AS QUALITY, ISNULL(DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(SAMPLEBARCODE.SB_REMARKS, '') AS REMARKS, SAMPLEBARCODE.SB_BARCODE AS BARCODE, ISNULL(ITEMMASTER.ITEM_WIDTH,'') AS WIDTH, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE", "", " SAMPLEBARCODE INNER JOIN ITEMMASTER ON SAMPLEBARCODE.SB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN QUALITYMASTER ON SAMPLEBARCODE.SB_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON SAMPLEBARCODE.SB_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON SAMPLEBARCODE.SB_DESIGNID = DESIGNMASTER.DESIGN_id  ", " AND SB_BARCODE = '" & TXTBARCODE.Text.Trim & "' AND SB_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then

                    If ClientName = "MAHAVIRPOLYCOT" Then
                        txtQTY.Text = 1
                        TXTRATE.Text = Val(DT.Rows(0).Item("RATE"))
                        If DT.Rows(0).Item("WIDTH") = "57/58 INCH" Then TXTCUT.Text = 16 Else TXTCUT.Text = 20
                    End If

                    Dim RATE As Double = Val(TXTRATE.Text.Trim)

                    'GET RATE
                    Dim WHERECLAUSE As String = ""
                    Dim DTRATE As New DataTable
                    If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                        If DT.Rows(0).Item("DESIGNNO") <> "" Then WHERECLAUSE = WHERECLAUSE & " And ISNULL(DESIGNMASTER.DESIGN_NO,'') = '" & DT.Rows(0).Item("DESIGNNO") & "'"
                        If DT.Rows(0).Item("COLOR") <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(COLORMASTER.COLOR_NAME,'') = '" & DT.Rows(0).Item("COLOR") & "'"
                        DTRATE = OBJCMN.SEARCH("PRICELISTMASTER.PL_RATE AS SALERATE ", "", "PRICELISTMASTER INNER JOIN ITEMMASTER ON PRICELISTMASTER.PL_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON PRICELISTMASTER.PL_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON PRICELISTMASTER.PL_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON PRICELISTMASTER.PL_QUALITYID = QUALITYMASTER.QUALITY_id ", " AND ISNULL(ITEMMASTER.ITEM_NAME,'') = '" & DT.Rows(0).Item("ITEMNAME") & "'" & WHERECLAUSE & " AND PL_YEARID = " & YearId)
                        If DTRATE.Rows.Count > 0 Then RATE = Val(DTRATE.Rows(0).Item("SALERATE"))
                    Else
                        'FOR THIS WE NEED TO GET THE COLUMN NAME FROM RATETYPEMASREER 
                        Dim DTRATETYPE As DataTable = OBJCMN.SEARCH(" ISNULL(LEDGERS.ACC_PRICELISTCOLUMN, '') AS COLNAME", "", " LEDGERS ", " AND ledgers.acc_cmpname = '" & cmbname.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                        If DTRATETYPE.Rows.Count > 0 AndAlso DTRATETYPE.Rows(0).Item("COLNAME") <> "" Then
                            DTRATE = OBJCMN.SEARCH(DTRATETYPE.Rows(0).Item("COLNAME") & " AS RATE", "", "ITEMPRICELIST INNER JOIN ITEMMASTER ON ITEMPRICELIST.ITEMID = ITEMMASTER.item_id ", " AND ITEMMASTER.ITEM_NAME = '" & DT.Rows(0).Item("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                            If DTRATE.Rows.Count > 0 Then RATE = Val(DTRATE.Rows(0).Item("RATE"))
                        End If
                    End If



                    'FETCH RATE FROM DESIGNMASTER
                    If ClientName = "GELATO" Then
                        DTRATE = OBJCMN.SEARCH("ISNULL(DESIGN_WRATE,0) AS WRATE", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & DT.Rows(0).Item("DESIGNNO") & "' AND DESIGN_YEARID = " & YearId)
                        If DTRATE.Rows.Count > 0 Then RATE = Val(DTRATE.Rows(0).Item("WRATE"))
                    End If


                    If ClientName = "GELATO" And (CHK30.Checked = True Or CHK32.Checked = True Or CHK34.Checked = True Or CHK36.Checked = True Or CHK38.Checked = True Or CHK40.Checked = True Or CHK42.Checked = True Or CHK44.Checked = True Or CHK46.Checked = True Or CHK48.Checked = True Or CHK50.Checked = True Or CHK52.Checked = True Or CHK54.Checked = True Or CHK56.Checked = True Or CHK58.Checked = True) Then
                        Dim TEMPSIZE As String = ""


                        'WE HAVE TO INSERT THE RECORDS OF ALL THE SIZES SELECTED FOR GELATO
                        For I As Integer = 1 To 15
                            If I = 1 And CHK30.CheckState = CheckState.Checked Then
                                TEMPSIZE = "30"
                                GoTo INSERTDATA
                            End If
                            If I = 2 And CHK32.CheckState = CheckState.Checked Then
                                TEMPSIZE = "32"
                                GoTo INSERTDATA
                            End If
                            If I = 3 And CHK34.CheckState = CheckState.Checked Then
                                TEMPSIZE = "34"
                                GoTo INSERTDATA
                            End If
                            If I = 4 And CHK36.CheckState = CheckState.Checked Then
                                TEMPSIZE = "36"
                                GoTo INSERTDATA
                            End If
                            If I = 5 And CHK38.CheckState = CheckState.Checked Then
                                TEMPSIZE = "38"
                                GoTo INSERTDATA
                            End If
                            If I = 6 And CHK40.CheckState = CheckState.Checked Then
                                TEMPSIZE = "40"
                                GoTo INSERTDATA
                            End If
                            If I = 7 And CHK42.CheckState = CheckState.Checked Then
                                TEMPSIZE = "42"
                                GoTo INSERTDATA
                            End If
                            If I = 8 And CHK44.CheckState = CheckState.Checked Then
                                TEMPSIZE = "44"
                                GoTo INSERTDATA
                            End If
                            If I = 9 And CHK46.CheckState = CheckState.Checked Then
                                TEMPSIZE = "46"
                                GoTo INSERTDATA
                            End If
                            If I = 10 And CHK48.CheckState = CheckState.Checked Then
                                TEMPSIZE = "48"
                                GoTo INSERTDATA
                            End If
                            If I = 11 And CHK50.CheckState = CheckState.Checked Then
                                TEMPSIZE = "50"
                                GoTo INSERTDATA
                            End If
                            If I = 12 And CHK52.CheckState = CheckState.Checked Then
                                TEMPSIZE = "52"
                                GoTo INSERTDATA
                            End If
                            If I = 13 And CHK54.CheckState = CheckState.Checked Then
                                TEMPSIZE = "54"
                                GoTo INSERTDATA
                            End If
                            If I = 14 And CHK56.CheckState = CheckState.Checked Then
                                TEMPSIZE = "56"
                                GoTo INSERTDATA
                            End If
                            If I = 15 And CHK58.CheckState = CheckState.Checked Then
                                TEMPSIZE = "58"
                                GoTo INSERTDATA
                            End If


INSERTDATA:
                            If TEMPSIZE <> "" Then GRIDSO.Rows.Add(GRIDSO.RowCount + 1, DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("DESIGNNO"), "", TEMPSIZE, "", 1, cmbqtyunit.Text.Trim, 0, 0, Format(Val(RATE), "0.00"), CMBPER.Text.Trim, 0, 0, 0, 0, 0, 0)
                            TEMPSIZE = ""
                        Next
                        GoTo LINE1
                    End If


                    'IF SHADE IS BLANK AND DESIGNNO IS PRESENT THEN FETCH ALL SHADES FROM DESIGNMASTER_COLOR
                    If ClientName = "MAHAVIRPOLYCOT" Or ClientName = "YASHVI" Or ClientName = "SUPRIYA" Then
                        If DT.Rows(0).Item("COLOR") = "" Then
                            Dim DTDESIGN As DataTable = OBJCMN.SEARCH("COLORMASTER.COLOR_NAME AS COLOR", "", " DESIGNMASTER INNER JOIN DESIGNMASTER_COLOR ON DESIGNMASTER.DESIGN_ID = DESIGNMASTER_COLOR.DESIGN_ID INNER JOIN COLORMASTER ON DESIGNMASTER_COLOR.DESIGN_COLORID = COLORMASTER.COLOR_id ", " AND ISNULL(DESIGNMASTER_COLOR.DESIGN_BLOCKED,0) = 'FALSE' AND DESIGNMASTER.DESIGN_NO = '" & DT.Rows(0).Item("DESIGNNO") & "' AND DESIGNMASTER.DESIGN_YEARID = " & YearId)
                            For Each DTROW As DataRow In DTDESIGN.Rows

                                'CHECK WHETHER ITEM IS ALREADY PRESENT OR NOT, IF PRESENT THEN ADD NEW TAKA
                                For Each ROW As DataGridViewRow In GRIDSO.Rows
                                    If ROW.Cells(gitemname.Index).Value = DT.Rows(0).Item("ITEMNAME") And ROW.Cells(GDESIGN.Index).Value = DT.Rows(0).Item("DESIGNNO") And ROW.Cells(gcolor.Index).Value = DTROW("COLOR") Then
                                        ROW.Cells(gQty.Index).Value = Val(ROW.Cells(gQty.Index).EditedFormattedValue) + 1
                                        ROW.Cells(GMTRS.Index).Value = ROW.Cells(gQty.Index).EditedFormattedValue * ROW.Cells(gcut.Index).EditedFormattedValue
                                        GoTo NEXTLINE
                                    End If
                                Next
                                GRIDSO.Rows.Add(GRIDSO.RowCount + 1, DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("DESIGNNO"), "", DTROW("COLOR"), "", Val(txtQTY.Text.Trim), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(RATE), "0.00"), CMBPER.Text.Trim, 0, 0, 0, 0, 0, 0)
NEXTLINE:

                                If GRIDSO.RowCount = 0 Then
                                    GRIDSO.Rows.Add(GRIDSO.RowCount + 1, DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("DESIGNNO"), "", DTROW("COLOR"), "", Val(txtQTY.Text.Trim), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(RATE), "0.00"), CMBPER.Text.Trim, 0, 0, 0, 0, 0, 0)
                                End If
                            Next
                            GoTo LINE1
                        Else
                            'CHECK WHETHER ITEM IS ALREADY PRESENT OR NOT, IF PRESENT THEN ADD NEW TAKA
                            For Each ROW As DataGridViewRow In GRIDSO.Rows
                                If ROW.Cells(gitemname.Index).Value = DT.Rows(0).Item("ITEMNAME") And ROW.Cells(GDESIGN.Index).Value = DT.Rows(0).Item("DESIGNNO") And ROW.Cells(gcolor.Index).Value = DT.Rows(0).Item("COLOR") Then
                                    ROW.Cells(gQty.Index).Value = Val(ROW.Cells(gQty.Index).EditedFormattedValue) + 1
                                    ROW.Cells(GMTRS.Index).Value = ROW.Cells(gQty.Index).EditedFormattedValue * ROW.Cells(gcut.Index).EditedFormattedValue
                                    GoTo NEXTLINE1
                                End If
                            Next
                            GRIDSO.Rows.Add(GRIDSO.RowCount + 1, DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("DESIGNNO"), "", DT.Rows(0).Item("COLOR"), "", Val(txtQTY.Text.Trim), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(RATE), "0.00"), CMBPER.Text.Trim, 0, 0, 0, 0, 0, 0)
NEXTLINE1:

                            If GRIDSO.RowCount = 0 Then
                                GRIDSO.Rows.Add(GRIDSO.RowCount + 1, DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("DESIGNNO"), "", DT.Rows(0).Item("COLOR"), "", Val(txtQTY.Text.Trim), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(RATE), "0.00"), CMBPER.Text.Trim, 0, 0, 0, 0, 0, 0)
                            End If
                            GoTo LINE1

                        End If
                    End If



                    GRIDSO.Rows.Add(GRIDSO.RowCount + 1, DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("DESIGNNO"), "", DT.Rows(0).Item("COLOR"), "", Val(txtQTY.Text.Trim), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(RATE), "0.00"), CMBPER.Text.Trim, 0, 0, 0, 0, 0, 0)
                    GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1

LINE1:
                    total()
                    TXTBARCODE.Clear()
                    TXTBARCODE.Focus()
                    GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1

                Else
                    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                    TXTBARCODE.Clear()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKFETCHDESC_CheckedChanged(sender As Object, e As EventArgs) Handles CHKFETCHDESC.CheckedChanged
        Try
            CMBGRIDREMARKS.Visible = CHKFETCHDESC.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHNO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCMPSONO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTCHNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTCMPSONO.Validating
        Try
            If (ClientName = "KRISHNA") And CMBCMPNAME.Text.Trim <> "" And Val(TXTCMPSONO.Text.Trim) > 0 And EDIT = False Then
                'GET YEARID FROM SELECTED CMP 
                Dim TEMPYEARID As Integer = 0
                Dim TEMPCMPID As Integer = 0
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("YEAR_ID AS YEARID, YEAR_CMPID AS CMPID", "", "YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMP_NAME = '" & CMBCMPNAME.Text.Trim & "' AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "'")
                If DT.Rows.Count > 0 Then
                    TEMPYEARID = DT.Rows(0).Item("YEARID")
                    TEMPCMPID = DT.Rows(0).Item("CMPID")
                End If



                'NOW FETCH SALEORDER DATA
                Dim ALPARAVAL As New ArrayList
                Dim OBJSO As New ClsSaleOrder()
                Dim dttable As DataTable = OBJSO.selectSo(Val(TXTCMPSONO.Text.Trim), TEMPCMPID, 0, TEMPYEARID)
                If dttable.Rows.Count > 0 Then

                    If MsgBox("Fetch data from Entry No " & TXTCMPSONO.Text.Trim & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                    For Each dr As DataRow In dttable.Rows

                        'CHECKING WHETHER ITEM IS PRESENT IN CURRENT YEAR OR NOT, IF NOT PRESENT THEN ADD NEW ITEM
                        If dr("ITEM") <> "" Then
                            DT = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", " ITEMMASTER ", " AND ITEM_NAME = '" & dr("ITEM") & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count = 0 Then
                                'ADD NEW ITEMNAME 
                                ALPARAVAL.Clear()


                                ALPARAVAL.Add("Finished Goods")
                                ALPARAVAL.Add("")   'CATEGORY
                                ALPARAVAL.Add(UCase(dr("ITEM")))        'DISPLAYNAME
                                ALPARAVAL.Add(UCase(dr("ITEM"))) 'ITEMNAME

                                ALPARAVAL.Add("")   'DEPARTMENT
                                ALPARAVAL.Add(UCase(dr("ITEM")))        'CODE
                                ALPARAVAL.Add(dr("UNIT"))
                                ALPARAVAL.Add("")   'FOLD
                                ALPARAVAL.Add(0)    'RATE
                                ALPARAVAL.Add(0)    'VALUATIONRATE   
                                ALPARAVAL.Add(0)    'TRANSRATE
                                ALPARAVAL.Add(0)    'CHCKINGRATE
                                ALPARAVAL.Add(0)    'PACKINGRATE
                                ALPARAVAL.Add(0)    'DESIGNRATE
                                ALPARAVAL.Add(0)    'REORDER
                                ALPARAVAL.Add(0)    'UPPER
                                ALPARAVAL.Add(0)    'LOWER



                                Dim DTHSN As DataTable = OBJCMN.SEARCH("ISNULL(HSN_ID, 0) AS HSNCODEID", "", " HSNMASTER", " AND HSN_CODE = '" & dr("HSNCODE") & "' AND HSN_YEARID = " & YearId)
                                If DTHSN.Rows.Count > 0 Then ALPARAVAL.Add(dr("HSNCODE")) Else ALPARAVAL.Add(0) 'HSNCODEID

                                ALPARAVAL.Add(0)    'BLOCKED
                                ALPARAVAL.Add(0)    'HIDEINDESIGN

                                ALPARAVAL.Add("")    'WIDTH
                                ALPARAVAL.Add("")    'GREYWIDTH
                                ALPARAVAL.Add(0)    'SHRINKFROM
                                ALPARAVAL.Add(0)    'SHRINKTO
                                ALPARAVAL.Add("")   'SELVEDGE

                                ALPARAVAL.Add("")   'RATETYPE
                                ALPARAVAL.Add("")   'RATE

                                ALPARAVAL.Add("")   'YARNQUALITY
                                ALPARAVAL.Add("")   'PER


                                ALPARAVAL.Add("")   'GRIDSRNO
                                ALPARAVAL.Add("")   'PROCESS

                                ALPARAVAL.Add("")   'REMARKS
                                ALPARAVAL.Add("MERCHANT")

                                ALPARAVAL.Add(DBNull.Value)
                                ALPARAVAL.Add("")   'WARP
                                ALPARAVAL.Add("")   'WEFT

                                ALPARAVAL.Add(CmpId)
                                ALPARAVAL.Add(Locationid)
                                ALPARAVAL.Add(Userid)
                                ALPARAVAL.Add(YearId)
                                ALPARAVAL.Add(0)

                                ALPARAVAL.Add("")   'WARPSRNO
                                ALPARAVAL.Add("")   'WARPQUALITY
                                ALPARAVAL.Add("")   'WARPSHADE
                                ALPARAVAL.Add("")   'WARPENDS
                                ALPARAVAL.Add("")   'WARPWT
                                ALPARAVAL.Add("")   'WARPRATE
                                ALPARAVAL.Add("")   'WARPAMOUNT

                                ALPARAVAL.Add("")   'WEFTSRNO
                                ALPARAVAL.Add("")   'WEFTQUALITY
                                ALPARAVAL.Add("")   'WEFTSHADE
                                ALPARAVAL.Add("")   'WEFTPICK
                                ALPARAVAL.Add("")   'WEFTWT
                                ALPARAVAL.Add("")   'WEFTRATE
                                ALPARAVAL.Add("")   'WEFTAMOUNT

                                ALPARAVAL.Add(0)    'WARPTL
                                ALPARAVAL.Add(0)    'WEFTTL
                                ALPARAVAL.Add(0)    'REED
                                ALPARAVAL.Add(0)    'REEDSPACE
                                ALPARAVAL.Add(0)    'PICKS
                                ALPARAVAL.Add(0)    'TOTALWT
                                ALPARAVAL.Add(0)    'TOTALWARPWT
                                ALPARAVAL.Add(0)    'TOTALWEFTWT
                                ALPARAVAL.Add("")   'WEAVE
                                ALPARAVAL.Add("")   'GREYCATEGORY


                                ALPARAVAL.Add(0)    'ACTUALWT
                                ALPARAVAL.Add(0)    'ACTUALAMT
                                ALPARAVAL.Add(0)    'DHARAPER
                                ALPARAVAL.Add(0)    'DHARAAMT
                                ALPARAVAL.Add(0)    'WASTAGEPER
                                ALPARAVAL.Add(0)    'WASTAGEAMT
                                ALPARAVAL.Add(0)    'WEAVINGCHGS
                                ALPARAVAL.Add(0)    'WEAVINGAMT
                                ALPARAVAL.Add(0)    'GSTPER
                                ALPARAVAL.Add(0)    'GSTAMT
                                ALPARAVAL.Add(0)    'AMOUNT
                                ALPARAVAL.Add(0)    'TOTALGSTPER
                                ALPARAVAL.Add(0)    'TOTALAMT
                                ALPARAVAL.Add(0)    'WARPTOTALAMT
                                ALPARAVAL.Add(0)    'WEFTTOTALAMT


                                ALPARAVAL.Add("")   'COLORNO
                                ALPARAVAL.Add("")   'COLORSRNO
                                ALPARAVAL.Add(0)    'VALUELOSSPER
                                ALPARAVAL.Add("")   'COSTCENTERNAME
                                ALPARAVAL.Add(0)    'ITEM GSM
                                ALPARAVAL.Add(0)    'ITEM PERCENT
                                ALPARAVAL.Add(0)    'GARMENT

                                ALPARAVAL.Add(0)    'SHADESRNO
                                ALPARAVAL.Add(0)    'SHADECOLORID

                                ALPARAVAL.Add(0)    'SHADEITEMSRNO
                                ALPARAVAL.Add(0)    'SHADEITEMID
                                ALPARAVAL.Add(0)    'SHADEDESIGNID
                                ALPARAVAL.Add(0)    'SHADEITEMCOLORID
                                ALPARAVAL.Add(0)    'SHADEMTRS
                                ALPARAVAL.Add(0)    'SHADESRNO

                                Dim objclsItemMaster As New clsItemmaster
                                objclsItemMaster.alParaval = ALPARAVAL
                                Dim IntResult As Integer = objclsItemMaster.SAVE()

                            End If
                        End If


                        If dr("DESIGN") <> "" Then
                            dttable = OBJCMN.SEARCH("DESIGN_ID AS DESIGNID", "", "DESIGNMASTER", " AND DESIGN_NO = '" & dr("DESIGN") & "' AND DESIGN_YEARID = " & YearId)
                            If dttable.Rows.Count = 0 Then
                                'ADD NEW DESIGN
                                Dim OBJDESIGN As New ClsDesignMaster
                                OBJDESIGN.alParaval.Add(UCase(dr("DESIGN")))
                                OBJDESIGN.alParaval.Add("") 'MILLNAME
                                OBJDESIGN.alParaval.Add("") 'CADNO
                                OBJDESIGN.alParaval.Add(0)  'PURRATE
                                OBJDESIGN.alParaval.Add(0)  'SALERATE
                                OBJDESIGN.alParaval.Add(0)  'WRATE
                                OBJDESIGN.alParaval.Add("") 'REMARKS

                                OBJDESIGN.alParaval.Add(0)  'FABRIC
                                OBJDESIGN.alParaval.Add(0)  'DYEING
                                OBJDESIGN.alParaval.Add(0)  'JOBWORK
                                OBJDESIGN.alParaval.Add(0)  'FINISHING
                                OBJDESIGN.alParaval.Add(0)  'EXTRA
                                OBJDESIGN.alParaval.Add(0)  'TOTAL

                                'GET ITEMNAME FROM TEMPYEARID   
                                Dim DTTEMP As DataTable = OBJCMN.SEARCH("ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEMMASTER.ITEM_ID", " AND DESIGN_NO = '" & dr("DESIGN") & "' AND DESIGN_YEARID = " & TEMPYEARID)
                                If DTTEMP.Rows.Count > 0 Then OBJDESIGN.alParaval.Add(DTTEMP.Rows(0).Item("ITEMNAME")) Else OBJDESIGN.alParaval.Add("")  'ITEM

                                OBJDESIGN.alParaval.Add(0)  'BLOCKED

                                OBJDESIGN.alParaval.Add(CmpId)
                                OBJDESIGN.alParaval.Add(Locationid)
                                OBJDESIGN.alParaval.Add(Userid)
                                OBJDESIGN.alParaval.Add(YearId)
                                OBJDESIGN.alParaval.Add(0)

                                OBJDESIGN.alParaval.Add(DBNull.Value)

                                OBJDESIGN.alParaval.Add("") 'GRIDSRNO
                                OBJDESIGN.alParaval.Add("") 'BASE
                                OBJDESIGN.alParaval.Add("") 'PRINT
                                OBJDESIGN.alParaval.Add("") 'COLOR

                                Dim INTRESCAT As Integer = OBJDESIGN.SAVE()
                            End If
                        End If



                        'COLOR SAVE
                        If dr("COLOR") <> "" Then
                            dttable = OBJCMN.SEARCH("COLOR_ID AS COLORID", "", "COLORMASTER", " AND COLOR_NAME = '" & dr("COLOR") & "' AND COLOR_YEARID = " & YearId)
                            If dttable.Rows.Count = 0 Then
                                'ADD NEW DESIGN
                                Dim OBJCOLOR As New ClsColorMaster
                                OBJCOLOR.alParaval.Add(UCase(dr("COLOR")))
                                OBJCOLOR.alParaval.Add("")
                                OBJCOLOR.alParaval.Add(CmpId)
                                OBJCOLOR.alParaval.Add(Locationid)
                                OBJCOLOR.alParaval.Add(Userid)
                                OBJCOLOR.alParaval.Add(YearId)
                                OBJCOLOR.alParaval.Add(0)

                                Dim INTRESCAT As Integer = OBJCOLOR.save()
                            End If
                        End If



                        'QUALITY SAVE
                        If dr("QUALITY") <> "" Then
                            dttable = OBJCMN.SEARCH("QUALITY_ID AS QUALITYID", "", "QUALITYMASTER", " AND QUALITY_NAME = '" & dr("QUALITY") & "' AND QUALITY_YEARID = " & YearId)
                            If dttable.Rows.Count = 0 Then
                                'ADD NEW QUALITY
                                Dim OBJQUALITY As New ClsQualityMaster
                                OBJQUALITY.alParaval.Add(UCase(dr("QUALITY")))
                                OBJQUALITY.alParaval.Add("")  'PROCECSS
                                OBJQUALITY.alParaval.Add("")  'UNIT
                                OBJQUALITY.alParaval.Add("")  'ITEMNAME
                                OBJQUALITY.alParaval.Add(0) 'REED
                                OBJQUALITY.alParaval.Add(0)  'PIK
                                OBJQUALITY.alParaval.Add("")  'COUNT
                                OBJQUALITY.alParaval.Add(0)  'WIDTH
                                OBJQUALITY.alParaval.Add("") 'REMAKS

                                OBJQUALITY.alParaval.Add("") 'WARP
                                OBJQUALITY.alParaval.Add("") 'WEFT
                                OBJQUALITY.alParaval.Add("") 'SELVEDGE


                                OBJQUALITY.alParaval.Add(CmpId)
                                OBJQUALITY.alParaval.Add(Locationid)
                                OBJQUALITY.alParaval.Add(Userid)
                                OBJQUALITY.alParaval.Add(YearId)
                                OBJQUALITY.alParaval.Add(0)
                                Dim INTRESCAT As Integer = OBJQUALITY.save()
                            End If
                        End If



                        TXTMOBILENO.Text = Convert.ToString(dr("MOBILENO"))

                        CMBRISK.Text = Convert.ToString(dr("RISK"))

                        txtpono.Text = Convert.ToString(dr("PONO"))
                        DUEDATE.Value = Convert.ToDateTime(dr("DUEDATE"))
                        daysremains()

                        TXTREFNO.Text = Convert.ToString(dr("REFNO"))

                        TXTCONSIGNEE.Text = Convert.ToString(dr("CONSIGNEE"))
                        TXTCONSIGNOR.Text = Convert.ToString(dr("CONSIGNOR"))
                        txtnote.Text = Convert.ToString(dr("NOTE"))
                        txttnc.Text = Convert.ToString(dr("TNC"))
                        TXTREMARKS.Text = Convert.ToString(dr("REMARKS"))

                        CMBSALESMAN.Text = Convert.ToString(dr("SALESMAN"))
                        TXTADVANCE.Text = Convert.ToString(dr("ADVANCE"))
                        TXTBALANCE.Text = Convert.ToString(dr("BALANCE"))
                        CMBPACKINGTYPE.Text = Convert.ToString(dr("PACKINGTYPE"))

                        CMBFORWARD.Text = dr("FORWARD")
                        CMBSAMPLE.Text = dr("SAMPLE")


                        GRIDSO.Rows.Add(dr("SRNO").ToString, dr("ITEM").ToString, dr("QUALITY").ToString, dr("DESIGN").ToString, dr("GRIDREMARKS").ToString, dr("COLOR").ToString, dr("PARTYPONO"), Format(Val(dr("QTY")), "0.00"), dr("UNIT").ToString, Format(Val(dr("CUT")), "0.00"), Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER"), Format(Val(dr("AMOUNT")), "0.00"), 0, 0, 0, 0, 0)
                    Next
                    total()
                    GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Enter(sender As Object, e As EventArgs) Handles cmbcolor.Enter
        Try
            If cmbcolor.Text.Trim = "" Then FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDREMARKS_Enter(sender As Object, e As EventArgs) Handles CMBGRIDREMARKS.Enter
        Try
            If CMBGRIDREMARKS.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" PAR_STAMPING ", "", " PARTYITEMWISECHART ", "  and PAR_YEARID =" & YearId)
                If DT.Rows.Count > 0 Then
                    DT.DefaultView.Sort = "PAR_STAMPING"
                    CMBGRIDREMARKS.DataSource = DT
                    CMBGRIDREMARKS.DisplayMember = "PAR_STAMPING"
                    CMBGRIDREMARKS.Text = ""
                End If
                CMBGRIDREMARKS.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(sender As Object, e As EventArgs) Handles cmbname.Enter
        Try
            If ClientName = "AVIS" Then
                If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND GROUP_NAME <> 'HASTE DEBTORS'")
            Else
                If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcity_Enter(sender As Object, e As EventArgs) Handles cmbcity.Enter
        Try
            If cmbcity.Text.Trim = "" Then fillCITY(cmbcity, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtsrno_Enter(sender As Object, e As EventArgs) Handles txtsrno.Enter
        If GRIDDOUBLECLICK = False Then
            If GRIDSO.RowCount > 0 Then
                txtsrno.Text = Val(GRIDSO.Rows(GRIDSO.RowCount - 1).Cells(gsrno.Index).Value) + 1
            Else
                txtsrno.Text = 1
            End If
        End If
    End Sub

    Private Sub SODATE_Enter(sender As Object, e As EventArgs) Handles SODATE.Enter
        SODATE.SelectAll()
    End Sub

    Private Sub CMBGRIDREMARKS_Validated(sender As Object, e As EventArgs) Handles CMBGRIDREMARKS.Validated
        Try

            'MAKE THIS STAMPING DEFAULT FOR PARTY
            If (ClientName = "YASHVI" Or ClientName = "SOFTAS") And txtgridremarks.Text.Trim <> "" And cmbname.Text.Trim <> "" And cmbitemname.Text.Trim <> "" Then

                'FIRST CHECK WHETHER THIS STAMP FOR THIS PARTY AND ITEM IS PRESENT OR NOT, IF NOT THEN CREATE NEW OR ELSE UPDATE
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("PAR_STAMPING AS STAMPING, PAR_NO AS PARNO", "", "PARTYITEMWISECHART INNER JOIN LEDGERS ON ACC_ID = PAR_LEDGERID INNER JOIN ITEMMASTER ON ITEM_ID = PAR_ITEMID", " AND ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND PAR_YEARID = " & YearId)
                If DT.Rows.Count > 0 AndAlso LCase(DT.Rows(0).Item("STAMPING")) <> LCase(CMBGRIDREMARKS.Text.Trim) Then
                    If MsgBox("Wish to Make this Stamp Default for this Party & Item?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    DT = OBJCMN.Execute_Any_String("UPDATE PARTYITEMWISECHART SET PAR_STAMPING = '" & CMBGRIDREMARKS.Text.Trim & "' WHERE PAR_NO = " & Val(DT.Rows(0).Item("PARNO")) & " AND PAR_YEARID = " & YearId, "", "")
                ElseIf DT.Rows.Count = 0 Then
                    'ADD NEW STAMPING
                    Dim ALPARAVAL As New ArrayList
                    Dim OBJCONFIG As New ClsPartyItemWiseChart

                    ALPARAVAL.Add(0)
                    ALPARAVAL.Add(cmbname.Text.Trim)
                    ALPARAVAL.Add(cmbitemname.Text.Trim)
                    ALPARAVAL.Add(0)    'RATE
                    ALPARAVAL.Add(CMBGRIDREMARKS.Text.Trim)
                    ALPARAVAL.Add(CmpId)
                    ALPARAVAL.Add(Userid)
                    ALPARAVAL.Add(YearId)

                    OBJCONFIG.alParaval = ALPARAVAL

                    Dim INT As Integer = OBJCONFIG.SAVE()
                End If
            End If


            If CMBGRIDREMARKS.Text.Trim <> "" And ClientName = "RAJKRIPA" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME, ISNULL(PAR_RATE,0) AS RATE ", "", " PARTYITEMWISECHART INNER JOIN ITEMMASTER ON PAR_ITEMID= ITEM_ID ", " AND PAR_STAMPING = '" & CMBGRIDREMARKS.Text.Trim & "' and PAR_YEARID =" & YearId)
                If DT.Rows.Count > 0 Then
                    cmbitemname.Text = DT.Rows(0).Item("ITEMNAME")
                    TXTRATE.Text = Val(DT.Rows(0).Item("RATE"))
                End If
                CMBGRIDREMARKS.SelectAll()
            End If
            txtgridremarks.Text = CMBGRIDREMARKS.Text.Trim
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMCITY_Enter(sender As Object, e As EventArgs) Handles CMBFROMCITY.Enter
        Try
            If CMBFROMCITY.Text.Trim = "" Then fillCITY(CMBFROMCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMCITY_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBFROMCITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCITY As New SelectCity
                OBJCITY.FRMSTRING = "CITY"
                OBJCITY.ShowDialog()
                If OBJCITY.TEMPNAME <> "" Then CMBFROMCITY.Text = OBJCITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtsrno_Validating(sender As Object, e As CancelEventArgs) Handles txtsrno.Validating
        Try
            If CMBFROMCITY.Text.Trim <> "" Then CITYVALIDATE(CMBFROMCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SODATE_GotFocus(sender As Object, e As EventArgs) Handles SODATE.GotFocus
        SODATE.SelectionStart = 0
    End Sub

    Private Sub cmbtrans_Validated(sender As Object, e As EventArgs) Handles cmbtrans.Validated
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS.ACC_WARNING,'') AS WARNINGTEXT ", "", " LEDGERS ", " and LEDGERS.acc_cmpname = '" & cmbtrans.Text.Trim & "' and LEDGERS.acc_YEARid = " & YearId)
            If DT.Rows.Count > 0 Then
                If DT.Rows(0).Item("WARNINGTEXT") <> "" Then MsgBox(DT.Rows(0).Item("WARNINGTEXT"), MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtQTY_GotFocus(sender As Object, e As EventArgs) Handles txtQTY.GotFocus
        txtQTY.SelectAll()
    End Sub

    Private Sub TXTMTRS_GotFocus(sender As Object, e As EventArgs) Handles TXTMTRS.GotFocus
        TXTMTRS.SelectAll()
    End Sub

    Private Sub TXTCUT_GotFocus(sender As Object, e As EventArgs) Handles TXTCUT.GotFocus
        TXTCUT.SelectAll()
    End Sub
End Class