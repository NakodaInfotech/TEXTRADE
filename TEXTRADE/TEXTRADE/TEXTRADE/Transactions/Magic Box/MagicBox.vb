'Imports System.ComponentModel
'Imports System.IO
'Imports System.Windows.Forms
'Imports BL
Public Class MagicBox
    '    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
    '        Try
    '            Cursor.Current = Cursors.WaitCursor
    '            Dim IntResult As Integer

    '            EP.Clear()
    '            If Not ERRORVALID() Then
    '                Exit Sub
    '            End If
    '            Dim alParaval As New ArrayList
    '            alParaval.Add(Val(TXTNO.Text.Trim))
    '            alParaval.Add(Format(Convert.ToDateTime(ORDERDATE.Text).Date, "MM/dd/yyyy"))
    '            alParaval.Add(CMBBUYERS.Text.Trim)
    '            alParaval.Add("") ' HASTE
    '            alParaval.Add("") 'AGENT

    '            alParaval.Add(TXTORDERNO) 'PONO
    '            alParaval.Add(duedate) 'DueDate.Value)
    '            alParaval.Add("") 'cmbtrans.Text.Trim)
    '            alParaval.Add("") 'cmbtrans2.Text.Trim)
    '            alParaval.Add("") 'cmbcity.Text.Trim)
    '            alParaval.Add("") 'TXTREFNO.Text.Trim)

    '            alParaval.Add("") 'CMBRISK.Text.Trim)
    '            alParaval.Add("") 'TXTCONSIGNOR.Text.Trim)
    '            alParaval.Add("") 'TXTCONSIGNEE.Text.Trim)
    '            alParaval.Add(CMBSELLERS.Text.Trim)
    '            alParaval.Add("") 'CMBCURRENCY.Text.Trim)
    '            alParaval.Add(0) 'lbltotalqty.Text.Trim)
    '            alParaval.Add(0) 'LBLTOTALMTRS.Text.Trim)
    '            alParaval.Add(0) 'lbltotalbale.Text.Trim)       '' *** TOTAL BALE INSTED OF TOTAL AMT.
    '            alParaval.Add(0) 'lbltotalamt.Text.Trim)

    '            alParaval.Add(0) 'txtdisper.Text.Trim)
    '            alParaval.Add(0) 'txtdisamt.Text.Trim)
    '            alParaval.Add(0) 'txtpfper.Text.Trim)
    '            alParaval.Add(0) 'txtpfamt.Text.Trim)
    '            alParaval.Add(0) 'txttestchgs.Text.Trim)
    '            alParaval.Add(0) 'txtnett.Text.Trim)
    '            alParaval.Add(0) 'Val(TXTEXCISEID.Text.Trim))
    '            alParaval.Add(0) 'TXTEXCISE.Text.Trim)
    '            alParaval.Add(0) 'txtexciseAMT.Text.Trim)
    '            alParaval.Add(0) 'TXTEDUCESS.Text.Trim)
    '            alParaval.Add(0) 'txteducessAMT.Text.Trim)
    '            alParaval.Add(0) 'TXTHSECESS.Text.Trim)
    '            alParaval.Add(0) 'txthsecessAMT.Text.Trim)
    '            alParaval.Add(0) 'txtsubtotal.Text.Trim)

    '            alParaval.Add(0) 'cmbtax.Text.Trim)
    '            alParaval.Add(0) 'txttax.Text.Trim)
    '            alParaval.Add(0) 'cmbaddtax.Text.Trim)
    '            alParaval.Add(0) 'txtaddtax.Text.Trim)
    '            alParaval.Add(0) 'txtfrper.Text.Trim)
    '            alParaval.Add(0) 'txtfreight.Text.Trim)
    '            alParaval.Add(0) 'cmboctroi.Text.Trim)
    '            alParaval.Add(0) 'txtoctroi.Text.Trim)
    '            alParaval.Add(0) 'txtinspchgs.Text.Trim)
    '            alParaval.Add(0) 'txtroundoff.Text.Trim)
    '            alParaval.Add(0) 'txtgrandtotal.Text.Trim)


    '            alParaval.Add("") 'txtinwords.Text.Trim)

    '            alParaval.Add(TXTREMARKS.Text.Trim)
    '            alParaval.Add("") 'txtnote.Text.Trim)
    '            alParaval.Add("") 'txttnc.Text.Trim)


    '            alParaval.Add("") 'cmbmisc.Text.Trim)
    '            alParaval.Add(Val(0)) 'txtDiscrate.Text.Trim))
    '            alParaval.Add(0) 'Val(txtDiscLot.Text.Trim))
    '            alParaval.Add(0) 'Val(txtdd.Text.Trim))
    '            alParaval.Add(0) 'Val(txtkatai.Text.Trim))
    '            alParaval.Add(Val(TXTDISCOUNT.Text.Trim))
    '            alParaval.Add(0) 'Val(txtadat.Text.Trim))
    '            alParaval.Add(Val(TXTCRDAYS.Text.Trim))
    '            alParaval.Add(0) 'Val(txtint.Text.Trim))
    '            alParaval.Add(0) 'Val(TXTADVANCE.Text.Trim))
    '            alParaval.Add(0) 'Val(TXTBALANCE.Text.Trim))
    '            alParaval.Add(0) 'CMBSALESMAN.Text.Trim)
    '            alParaval.Add(0) 'CMBPACKINGTYPE.Text.Trim)
    '            alParaval.Add("") 'CMBFORWARD.Text.Trim)



    '            alParaval.Add(CmpId)
    '            alParaval.Add(Locationid)
    '            alParaval.Add(Userid)
    '            alParaval.Add(YearId)
    '            alParaval.Add(0)

    '            Dim GRIDSRNO As String = ""
    '            Dim MERCHANT As String = ""
    '            Dim QUALITY As String = ""
    '            Dim DESIGN As String = ""
    '            Dim gridremarks As String = ""
    '            Dim COLOR As String = ""
    '            Dim PARTYPONO As String = ""
    '            Dim qty As String = ""
    '            Dim QTYUNIT As String = ""
    '            Dim CUT As String = ""
    '            Dim MTRS As String = ""
    '            Dim RATE As String = ""
    '            Dim PER As String = ""
    '            Dim AMOUNT As String = ""
    '            Dim RECDQTY As String = ""
    '            Dim RECDMTRS As String = ""
    '            Dim DONE As String = ""
    '            Dim SAMPLEDONE As String = ""
    '            Dim CLOSED As String = ""

    '            For Each row As Windows.Forms.DataGridViewRow In GRIDSO.Rows
    '                If row.Cells(0).Value <> Nothing Then
    '                    If GRIDSRNO = "" Then
    '                        GRIDSRNO = row.Cells(gsrno.Index).Value.ToString
    '                        MERCHANT = row.Cells(gitemname.Index).Value.ToString
    '                        QUALITY = "" 'row.Cells(GQUALITY.Index).Value.ToString
    '                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
    '                        gridremarks = "" ' row.Cells(gdesc.Index).Value.ToString
    '                        COLOR = "" 'row.Cells(gcolor.Index).Value.ToString
    '                        PARTYPONO = "" ' row.Cells(GPARTYPONO.Index).Value.ToString
    '                        qty = row.Cells(gQty.Index).Value.ToString
    '                        QTYUNIT = row.Cells(gqtyunit.Index).Value.ToString
    '                        CUT = row.Cells(gcut.Index).Value
    '                        MTRS = row.Cells(GMTRS.Index).Value
    '                        RATE = row.Cells(GRATE.Index).Value
    '                        PER = "" 'row.Cells(GPER.Index).Value
    '                        AMOUNT = "" 'row.Cells(GAMOUNT.Index).Value
    '                        RECDQTY = "" 'Val(row.Cells(GRECDQTY.Index).Value)
    '                        RECDMTRS = "" 'Val(row.Cells(GRECDMTRS.Index).Value)

    '                        DONE = 0 'If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then DONE = 1 Else DONE = 0
    '                        SAMPLEDONE = 0 'If Convert.ToBoolean(row.Cells(GSAMPLEDONE.Index).Value) = True Then SAMPLEDONE = 1 Else SAMPLEDONE = 0
    '                        CLOSED = 0 'If Convert.ToBoolean(row.Cells(GCLOSED.Index).Value) = True Then CLOSED = 1 Else CLOSED = 0

    '                    Else
    '                        GRIDSRNO = GRIDSRNO & "|" & row.Cells(gsrno.Index).Value.ToString
    '                        MERCHANT = MERCHANT & "|" & row.Cells(gitemname.Index).Value.ToString
    '                        QUALITY = QUALITY & "|" & "" 'row.Cells(GQUALITY.Index).Value.ToString
    '                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
    '                        gridremarks = gridremarks & "|" & "" 'row.Cells(gdesc.Index).Value.ToString
    '                        COLOR = COLOR & "|" & "" 'row.Cells(gcolor.Index).Value.ToString
    '                        PARTYPONO = PARTYPONO & "|" & "" 'row.Cells(GPARTYPONO.Index).Value.ToString
    '                        qty = qty & "|" & row.Cells(gQty.Index).Value.ToString
    '                        QTYUNIT = QTYUNIT & "|" & row.Cells(gqtyunit.Index).Value.ToString
    '                        CUT = CUT & "|" & row.Cells(gcut.Index).Value
    '                        MTRS = MTRS & "|" & row.Cells(GMTRS.Index).Value
    '                        RATE = RATE & "|" & row.Cells(GRATE.Index).Value
    '                        PER = PER & "|" & "" 'row.Cells(GPER.Index).Value
    '                        AMOUNT = AMOUNT & "|" & "" 'row.Cells(GAMOUNT.Index).Value
    '                        RECDQTY = RECDQTY & "|" & "" 'Val(row.Cells(GRECDQTY.Index).Value)
    '                        RECDMTRS = RECDMTRS & "|" & "" 'Val(row.Cells(GRECDMTRS.Index).Value)

    '                        DONE = DONE & "|" & "0" 'If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then DONE = DONE & "|" & "1" Else DONE = DONE & "|" & "0"
    '                        SAMPLEDONE = SAMPLEDONE & "|" & "0" 'If Convert.ToBoolean(row.Cells(GSAMPLEDONE.Index).Value) = True Then SAMPLEDONE = SAMPLEDONE & "|" & "1" Else SAMPLEDONE = SAMPLEDONE & "|" & "0"
    '                        CLOSED = CLOSED & "|" & "0" 'If Convert.ToBoolean(row.Cells(GCLOSED.Index).Value) = True Then CLOSED = CLOSED & "|" & "1" Else CLOSED = CLOSED & "|" & "0"

    '                    End If
    '                End If
    '            Next

    '            alParaval.Add(GRIDSRNO)
    '            alParaval.Add(MERCHANT)
    '            alParaval.Add(QUALITY)
    '            alParaval.Add(DESIGN)
    '            alParaval.Add(gridremarks)
    '            alParaval.Add(COLOR)
    '            alParaval.Add(PARTYPONO)
    '            alParaval.Add(qty)
    '            alParaval.Add(QTYUNIT)
    '            alParaval.Add(CUT)
    '            alParaval.Add(MTRS)
    '            alParaval.Add(RATE)
    '            alParaval.Add(PER)
    '            alParaval.Add(AMOUNT)
    '            alParaval.Add(RECDQTY)
    '            alParaval.Add(RECDMTRS)
    '            alParaval.Add(DONE)
    '            alParaval.Add(SAMPLEDONE)
    '            alParaval.Add(CLOSED)

    '            alParaval.Add("") 'CMBSAMPLE.Text.Trim)

    '            alParaval.Add("") 'CMBFROMCITY.Text.Trim)

    '            alParaval.Add(0)


    '            Dim objclsPurord As New ClsAgencySaleOrder()
    '            objclsPurord.alParaval = alParaval


    '            If EDIT = False Then
    '                If USERADD = False Then
    '                    MsgBox("Insufficient Rights")
    '                    Exit Sub
    '                End If
    '                Dim DT As DataTable = objclsPurord.SAVE()
    '                MessageBox.Show("Details Added")
    '                TXTSONO.Text = DT.Rows(0).Item(0)
    '            Else
    '                alParaval.Add(TEMPSONO)
    '                If USEREDIT = False Then
    '                    MsgBox("Insufficient Rights")
    '                    Exit Sub
    '                End If

    '                IntResult = objclsPurord.UPDATE()
    '                MessageBox.Show("Details Updated")
    '            End If

    '            If ClientName = "YASHVI" Then SMSCODE()

    '            'THEY DONT NEED PRINT
    '            If ClientName <> "SNCM" Then
    '                If MsgBox("Wish to Print Order?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then PRINTREPORT()
    '            End If


    '            'IF ADVANCE IS RECD FROM CLIENT THEN OPEN RECEIPT FORM AUTO
    '            'USER WILL SAVE IT MANUALLY
    '            If EDIT = False And (ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV") And Val(TXTADVANCE.Text.Trim) > 0 Then
    '                Dim OBJREC As New Receipt
    '                OBJREC.TEMPAUTOENTRY = True
    '                OBJREC.TEMPAMT = Val(TXTADVANCE.Text.Trim)
    '                OBJREC.TEMPBILLNO = "ORDER NO - " & Val(TXTSONO.Text.Trim)
    '                OBJREC.TEMPNAME = cmbname.Text.Trim
    '                OBJREC.MdiParent = MDIMain
    '                OBJREC.Show()
    '            End If

    '            EDIT = False
    '            CLEAR()


    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Sub
    '    Private Function ERRORVALID() As Boolean

    '    End Function

    '    Private Sub MagicBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    '    End Sub

    '    Private Sub TXTDELPERIOD_Validated(sender As Object, e As EventArgs) Handles TXTDELPERIOD.Validated
    '        Try
    '            If ORDERDATE.Text <> "__/__/____" Then
    '                If Val(TXTDELPERIOD.Text.Trim) > 0 Then duedate.Value = Convert.ToDateTime(ORDERDATE.Text).Date.AddDays(Val(TXTDELPERIOD.Text.Trim))
    '            End If
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Sub
End Class