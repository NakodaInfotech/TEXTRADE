
Imports System.ComponentModel
Imports BL

Public Class RecFromPacking
    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Public EDIT As Boolean          'used for editing
    Public TEMPRECNO As Integer     'used for poation no while editing
    Dim TEMPROW As Integer
    Public ISSUEBARCODE As String = ""  'IT IS USED TO OPEN THE ENTRY DIRECTLY FROM INHOUSEPACKINGSTOFK REPORT
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim HIDEALLISSUE As Boolean = True
    Dim ALLOWMANUALRECNO As Boolean = False
    Dim TEMPDATE As Date

    Public Sub New()
        InitializeComponent()
        FILLCMB()

    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub clear()

        TXTFROM.Clear()
        TXTTO.Clear()

        EP.Clear()
        RECDATE.Text = Now.Date
        RECDATE.ReadOnly = False
        TXTLOTNO.Clear()
        TXTREFNO.Clear()
        cmbname.Text = ""
        cmbname.Enabled = True
        tstxtbillno.Clear()
        If USERGODOWN <> "" Then cmbGodown.Text = USERGODOWN Else cmbGodown.Text = ""
        CMBBARCODE.Text = ""
        CMBBARCODE.Enabled = True

        CMDSHORTAGE.Enabled = True

        TXTISSUEMTRS.Clear()
        TXTPENDINGMTRS.Clear()
        TXTRUNNINGBAL.Clear()
        TXTLONGATIONPER.Clear()

        txtgridremarks.Clear()
        TXTBARCODE.Clear()
        TXTFROMNO.Clear()
        TXTFROMSRNO.Clear()
        TXTFROMTYPE.Clear()

        txtsrno.Text = 1
        CMBPIECETYPE.Text = ""
        cmbitemname.Text = ""
        CMBQUALITY.Text = ""
        CMBDESIGN.Text = ""
        cmbcolor.Text = ""
        TXTCUT.Clear()
        txtqty.Text = 1
        If ClientName = "YASHVI" Then
            cmbqtyunit.Text = "THAN"
        ElseIf ClientName = "YUMILONE" Or ClientName = "REVAANT" Then
            cmbqtyunit.Text = "PCS"
        ElseIf ClientName = "VALIANT" Then
            cmbqtyunit.Text = "TAKA"
        ElseIf ClientName = "SONU" Or ClientName = "MNIKHIL" Or ClientName = "HRITI" Or ClientName = "SOFTAS" Or ClientName = "MYCOT" Then
            cmbqtyunit.Text = "ROLL"
        ElseIf ClientName = "SHREENAKODA" Then
            cmbqtyunit.Text = "LUMP"
        Else
            cmbqtyunit.Text = ""
        End If
        TXTMTRS.Clear()
        TXTRATE.Clear()
        CMBPER.Text = "Mtrs"
        TXTAMOUNT.Clear()
        CMBRACK.Text = ""
        CMBSHELF.Text = ""
        GRIDREC.RowCount = 0

        txtremarks.Clear()

        LBLBARCODEPRINTED.Visible = False
        lbllocked.Visible = False
        PBlock.Visible = False

        GRIDDOUBLECLICK = False

        If ClientName = "SUPRIYA" Or ClientName = "SHREENAKODA" Or ClientName = "SOFTAS" Then
            TXTFROMNO.ReadOnly = False
            TXTFROMSRNO.ReadOnly = False
            TXTFROMNO.TabStop = True
            TXTFROMSRNO.TabStop = True
            txtqty.ReadOnly = False
        End If

        getmaxno()
        lbltotalqty.Text = 0
        LBLTOTALMTRS.Text = 0
        LBLTOTALAMOUNT.Text = 0.0


        GRIDALLREC.RowCount = 0
        GRIDAREC.RowCount = 0
        LBLATOTALQTY.Text = 0
        LBLATOTALMTRS.Text = 0
        GRIDISSUE.RowCount = 0
        GRIDISSUE.ReadOnly = False
        GRIDISSUE.Enabled = True
        TXTFROMNO.ReadOnly = False

        TXTASRNO.Text = 1
        CMBAPIECETYPE.Text = ""
        CMBAITEMNAME.Text = ""
        CMBADESIGN.Text = ""
        TXTAGRIDREMARKS.Clear()
        CMBACOLOR.Text = ""
        TXTACUT.Clear()
        TXTAQTY.Text = 1
        CMBAQTYUNIT.Text = ""
        TXTAMTRS.Clear()
        CMBARACK.Text = ""
        CMBASHELF.Text = ""


        If ALLOWMANUALRECNO = True Then
            TXTRECNO.ReadOnly = False
            TXTRECNO.BackColor = Color.LemonChiffon
        Else
            TXTRECNO.ReadOnly = True
            TXTRECNO.BackColor = Color.Linen
        End If
        CHKMANUALRATE.CheckState = CheckState.Unchecked
        TXTRATE.ReadOnly = True
        GRATE.ReadOnly = True



    End Sub

    Sub TOTAL()
        Try
            LBLTOTALMTRS.Text = 0.0
            lbltotalqty.Text = 0
            LBLATOTALMTRS.Text = 0.0
            LBLATOTALQTY.Text = 0
            TXTRUNNINGBAL.Text = 0
            LBLTOTALAMOUNT.Text = 0

            For Each ROW As DataGridViewRow In GRIDREC.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    If ROW.Cells(gcut.Index).EditedFormattedValue > 0 Then ROW.Cells(GMTRS.Index).Value = ROW.Cells(gQty.Index).EditedFormattedValue * ROW.Cells(gcut.Index).EditedFormattedValue
                    lbltotalqty.Text = Format(Val(lbltotalqty.Text) + Val(ROW.Cells(gQty.Index).EditedFormattedValue), "0")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    If ROW.Cells(GPER.Index).EditedFormattedValue = "Mtrs" Then
                        ROW.Cells(GAMOUNT.Index).Value = Format(Val(ROW.Cells(GRATE.Index).EditedFormattedValue) * Val(ROW.Cells(GMTRS.Index).EditedFormattedValue))
                    Else
                        ROW.Cells(GAMOUNT.Index).Value = Format(Val(ROW.Cells(GRATE.Index).EditedFormattedValue) * Val(ROW.Cells(gQty.Index).EditedFormattedValue))
                    End If
                    LBLTOTALAMOUNT.Text = Format(Val(LBLTOTALAMOUNT.Text) + Val(ROW.Cells(GAMOUNT.Index).EditedFormattedValue), "0.00")
                End If
            Next


            For Each ROW As DataGridViewRow In GRIDAREC.Rows
                If ROW.Cells(ASRNO.Index).Value <> Nothing Then
                    If ROW.Cells(ACUT.Index).EditedFormattedValue > 0 Then ROW.Cells(AMTRS.Index).Value = ROW.Cells(AQTY.Index).EditedFormattedValue * ROW.Cells(ACUT.Index).EditedFormattedValue
                    LBLATOTALQTY.Text = Format(Val(LBLATOTALQTY.Text) + Val(ROW.Cells(AQTY.Index).EditedFormattedValue), "0")
                    LBLATOTALMTRS.Text = Format(Val(LBLATOTALMTRS.Text) + Val(ROW.Cells(AMTRS.Index).EditedFormattedValue), "0.00")
                End If
            Next

            If HIDEALLISSUE = False Then
                TXTRUNNINGBAL.Text = Format(Val(TXTPENDINGMTRS.Text.Trim) - Val(LBLATOTALMTRS.Text.Trim), "0.00")
            Else
                TXTRUNNINGBAL.Text = Format(Val(TXTPENDINGMTRS.Text.Trim) - Val(LBLTOTALMTRS.Text.Trim), "0.00")
            End If

            If Val(TXTRUNNINGBAL.Text.Trim) < 0 Then TXTLONGATIONPER.Text = Format((Val(TXTRUNNINGBAL.Text.Trim) * -1) / Val(TXTISSUEMTRS.Text.Trim) * 100, "0.00")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        ISSUEBARCODE = ""
        EDIT = False
        cmbGodown.Focus()
    End Sub

    Sub getmaxno()
        Try
            Dim DTTABLE As New DataTable
            DTTABLE = getmax(" isnull(max(REC_no),0) + 1 ", " RECPACKING ", " and REC_yearid=" & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTRECNO.Text = DTTABLE.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ERRORVALID() As Boolean
        Try

            Dim bln As Boolean = True

            'WE WERE ALLOWING TO SAVE ENTRY IF RUNNINGBAL IS NOT 0, NOW WE HAVE LOCKED FOR EVERYONE
            'If (ClientName = "AVIS" Or ClientName = "SHREENAKODA" Or ClientName = "SOFTAS" Or ClientName = "SUPEEMA") And Val(TXTRUNNINGBAL.Text.Trim) > 0 Then
            '    EP.SetError(TXTRUNNINGBAL, " Mtrs Cannot be Geater than 0")
            '    bln = False
            'ElseIf Val(TXTRUNNINGBAL.Text.Trim) > 0 Then
            '    If MsgBox("Mtrs will be shown in Packing Stock, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            '        EP.SetError(TXTRUNNINGBAL, " Mtrs Cannot be Geater than 0")
            '        bln = False
            '    End If
            'End If


            If ALLOWMANUALRECNO = True Then
                If Val(TXTRECNO.Text.Trim) <> 0 And EDIT = False Then
                    Dim OBJCMNn As New ClsCommon
                    Dim dttable As DataTable = OBJCMNn.search(" ISNULL(RECPACKING.REC_NO,0)  AS RECNO", "", " RECPACKING ", "  AND RECPACKING.REC_NO=" & Val(TXTRECNO.Text.Trim) & " AND RECPACKING.REC_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        MsgBox("Rec No Already Exist")
                        bln = False
                    End If
                End If
            End If

            If UserName <> "Admin" Then
                If CHKMANUALRATE.Checked = True Then
                    TXTRATE.ReadOnly = False
                    EP.SetError(CHKMANUALRATE, " ALLOW MANUAL RATE")
                    CHKMANUALRATE.Checked = False
                End If
            End If

            If Val(TXTRUNNINGBAL.Text.Trim) > 0 And ClientName <> "DEVEN" And ClientName <> "KARAN" And ClientName <> "DJIMPEX" And HIDEALLISSUE = True Then
                EP.SetError(TXTRUNNINGBAL, " Mtrs Cannot be Geater than 0")
                bln = False
            End If



            If cmbGodown.Text.Trim.Length = 0 Then
                EP.SetError(cmbGodown, " Please Select Godown")
                bln = False
            End If

            If CMBBARCODE.Text.Trim.Length = 0 And HIDEALLISSUE = True Then
                EP.SetError(CMBBARCODE, " Select Entry")
                bln = False
            End If

            If ClientName <> "AVIS" Or (ClientName = "AVIS" And UserName <> "Admin") Then
                If lbllocked.Visible = True Then
                    EP.SetError(lbllocked, "Item Used, Item Locked")
                    bln = False
                End If
            End If

            If GRIDREC.RowCount = 0 And HIDEALLISSUE = True Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If

            If GRIDALLREC.RowCount = 0 And HIDEALLISSUE = False Then
                EP.SetError(GRIDAREC, "Fill Item Details")
                bln = False
            End If

            For Each ROW As DataGridViewRow In GRIDREC.Rows
                If ROW.Cells(GMTRS.Index).Value = 0 Then
                    EP.SetError(TXTMTRS, "Mtrs Cannot be 0")
                    bln = False
                End If
                If ITEMCOSTCENTRE = True And Val(ROW.Cells(GRATE.Index).Value) = 0 Then
                    EP.SetError(cmbname, "Rate Cannot be 0")
                    bln = False
                End If
            Next

            If RECDATE.Text = "__/__/____" Then
                EP.SetError(RECDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(RECDATE.Text) Then
                    EP.SetError(RECDATE, "Date not in Accounting Year")
                    bln = False
                End If

                If Convert.ToDateTime(RECDATE.Text).Date < RECPACKBLOCKDATE.Date Then
                    EP.SetError(RECDATE, "Date is Blocked, Please make entries after " & Format(RECPACKBLOCKDATE.Date, "dd/MM/yyyy"))
                    bln = False
                End If
            End If


            'CHEKC WHETHER ISSUE BARCODE ENTRY IS ALRADY DONE OR NOT
            If EDIT = False And ClientName <> "AVIS" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("REC_NO AS SRNO", "", " RECPACKING ", " AND REC_OUTBARCODE = '" & CMBBARCODE.Text.Trim & "' And REC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If MsgBox("Barcode Already Used In Entry No " & Val(DT.Rows(0).Item("SRNO")) & ", Wish To Proceed?", MsgBoxStyle.YesNo) = vbNo Then
                        EP.SetError(CMBBARCODE, "Barcode Already Used In Entry No " & Val(DT.Rows(0).Item("SRNO")))
                        bln = False
                    End If
                End If
            End If

            If ClientName = "MNARESH" And UserName <> "Admin" Then
                Dim TEMPDATE As DateTime = RECDATE.Text
                If TEMPDATE <> DateTime.Today Then
                    EP.SetError(RECDATE, "You cannot Modify And Create previous Date Entry. ")
                    bln = False
                End If
            End If



            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            'DONE BY GULKIT
            GRIDALLREC.Sort(GRIDALLREC.Columns(ALLMAINSRNO.Index), System.ComponentModel.ListSortDirection.Ascending)
            getsrno(GRIDALLREC)


            Dim alParaval As New ArrayList

            If TXTRECNO.ReadOnly = False Then
                alParaval.Add(Val(TXTRECNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(Format(Convert.ToDateTime(RECDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(cmbGodown.Text.Trim)
            alParaval.Add(TXTLOTNO.Text.Trim)
            alParaval.Add(TXTREFNO.Text.Trim)
            alParaval.Add(CMBBARCODE.Text.Trim)

            alParaval.Add(Val(TXTISSUEMTRS.Text))
            alParaval.Add(Val(TXTPENDINGMTRS.Text))
            alParaval.Add(Val(TXTFROMNO.Text))
            alParaval.Add(Val(TXTFROMSRNO.Text))
            alParaval.Add(TXTFROMTYPE.Text)

            alParaval.Add(Val(lbltotalqty.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))

            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim GRIDSRNO As String = ""
            Dim PIECETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim gridremarks As String = ""
            Dim COLOR As String = ""
            Dim CUT As String = ""
            Dim QTY As String = ""
            Dim QTYUNIT As String = ""
            Dim MTRS As String = ""
            Dim RACK As String = ""
            Dim SHELF As String = ""
            Dim BARCODE As String = ""
            Dim DONE As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""
            Dim RATE As String = ""
            Dim PER As String = ""
            Dim AMOUNT As String = ""

            If HIDEALLISSUE = True Then
                For Each row As Windows.Forms.DataGridViewRow In GRIDREC.Rows
                    If row.Cells(0).Value <> Nothing Then
                        If GRIDSRNO = "" Then
                            GRIDSRNO = row.Cells(gsrno.Index).Value.ToString
                            PIECETYPE = row.Cells(GPIECETYPE.Index).Value.ToString
                            ITEMNAME = row.Cells(gitemname.Index).Value.ToString
                            QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                            DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                            gridremarks = row.Cells(gdesc.Index).Value.ToString
                            COLOR = row.Cells(gcolor.Index).Value.ToString
                            CUT = row.Cells(gcut.Index).Value.ToString
                            QTY = row.Cells(gQty.Index).Value.ToString
                            QTYUNIT = row.Cells(gqtyunit.Index).Value.ToString
                            MTRS = row.Cells(GMTRS.Index).Value
                            RATE = row.Cells(GRATE.Index).Value
                            PER = row.Cells(GPER.Index).Value
                            AMOUNT = row.Cells(GAMOUNT.Index).Value
                            RACK = row.Cells(GRACK.Index).Value.ToString
                            SHELF = row.Cells(GSHELF.Index).Value.ToString
                            BARCODE = row.Cells(GBARCODE.Index).Value
                            If row.Cells(GDONE.Index).Value = True Then
                                DONE = 1
                            Else
                                DONE = 0
                            End If
                            OUTPCS = row.Cells(GOUTPCS.Index).Value
                            OUTMTRS = row.Cells(GOUTMTRS.Index).Value
                            FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                            FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                            FROMTYPE = row.Cells(GFROMTYPE.Index).Value


                        Else

                            GRIDSRNO = GRIDSRNO & "|" & row.Cells(gsrno.Index).Value
                            PIECETYPE = PIECETYPE & "|" & row.Cells(GPIECETYPE.Index).Value
                            ITEMNAME = ITEMNAME & "|" & row.Cells(gitemname.Index).Value
                            QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                            DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                            gridremarks = gridremarks & "|" & row.Cells(gdesc.Index).Value.ToString
                            COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                            CUT = CUT & "|" & row.Cells(gcut.Index).Value
                            QTY = QTY & "|" & row.Cells(gQty.Index).Value
                            QTYUNIT = QTYUNIT & "|" & row.Cells(gqtyunit.Index).Value
                            MTRS = MTRS & "|" & row.Cells(GMTRS.Index).Value
                            RATE = RATE & "|" & row.Cells(GRATE.Index).Value
                            PER = PER & "|" & row.Cells(GPER.Index).Value
                            AMOUNT = AMOUNT & "|" & row.Cells(GAMOUNT.Index).Value
                            RACK = RACK & "|" & row.Cells(GRACK.Index).Value.ToString
                            SHELF = SHELF & "|" & row.Cells(GSHELF.Index).Value.ToString
                            BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value
                            If row.Cells(GDONE.Index).Value = True Then
                                DONE = DONE & "|" & "1"
                            Else
                                DONE = DONE & "|" & "0"
                            End If
                            OUTPCS = OUTPCS & "|" & row.Cells(GOUTPCS.Index).Value
                            OUTMTRS = OUTMTRS & "|" & row.Cells(GOUTMTRS.Index).Value
                            FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                            FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                            FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value

                        End If
                    End If
                Next

            Else

                For Each row As Windows.Forms.DataGridViewRow In GRIDALLREC.Rows
                    If row.Cells(0).Value <> Nothing Then
                        If GRIDSRNO = "" Then
                            GRIDSRNO = row.Cells(ALLSRNO.Index).Value.ToString
                            PIECETYPE = row.Cells(ALLPIECETYPE.Index).Value.ToString
                            ITEMNAME = row.Cells(ALLITEMNAME.Index).Value.ToString
                            QUALITY = row.Cells(ALLQUALITY.Index).Value.ToString
                            DESIGN = row.Cells(ALLDESIGN.Index).Value.ToString
                            gridremarks = row.Cells(ALLDESC.Index).Value.ToString
                            COLOR = row.Cells(ALLCOLOR.Index).Value.ToString
                            CUT = row.Cells(ALLCUT.Index).Value.ToString
                            QTY = row.Cells(ALLQTY.Index).Value.ToString
                            QTYUNIT = row.Cells(ALLQTYUNIT.Index).Value.ToString
                            MTRS = row.Cells(ALLMTRS.Index).Value
                            RATE = 0
                            PER = "Mtrs"
                            AMOUNT = 0
                            RACK = row.Cells(ALLRACK.Index).Value.ToString
                            SHELF = row.Cells(ALLSHELF.Index).Value.ToString
                            BARCODE = row.Cells(ALLBARCODE.Index).Value
                            If row.Cells(ALLDONE.Index).Value = True Then
                                DONE = 1
                            Else
                                DONE = 0
                            End If
                            OUTPCS = row.Cells(ALLOUTPCS.Index).Value
                            OUTMTRS = row.Cells(ALLOUTMTRS.Index).Value
                            FROMNO = Val(row.Cells(ALLFROMNO.Index).Value)
                            FROMSRNO = Val(row.Cells(ALLFROMSRNO.Index).Value)
                            FROMTYPE = row.Cells(ALLFROMTYPE.Index).Value

                        Else

                            GRIDSRNO = GRIDSRNO & "|" & row.Cells(ALLSRNO.Index).Value
                            PIECETYPE = PIECETYPE & "|" & row.Cells(ALLPIECETYPE.Index).Value
                            ITEMNAME = ITEMNAME & "|" & row.Cells(ALLITEMNAME.Index).Value
                            QUALITY = QUALITY & "|" & row.Cells(ALLQUALITY.Index).Value.ToString
                            DESIGN = DESIGN & "|" & row.Cells(ALLDESIGN.Index).Value.ToString
                            gridremarks = gridremarks & "|" & row.Cells(ALLDESC.Index).Value.ToString
                            COLOR = COLOR & "|" & row.Cells(ALLCOLOR.Index).Value.ToString
                            CUT = CUT & "|" & row.Cells(ALLCUT.Index).Value
                            QTY = QTY & "|" & row.Cells(ALLQTY.Index).Value
                            QTYUNIT = QTYUNIT & "|" & row.Cells(ALLQTYUNIT.Index).Value
                            MTRS = MTRS & "|" & row.Cells(ALLMTRS.Index).Value
                            RATE = RATE & "|" & 0
                            PER = PER & "|Mtrs"
                            AMOUNT = AMOUNT & "|" & 0
                            RACK = RACK & "|" & row.Cells(ALLRACK.Index).Value.ToString
                            SHELF = SHELF & "|" & row.Cells(ALLSHELF.Index).Value.ToString
                            BARCODE = BARCODE & "|" & row.Cells(ALLBARCODE.Index).Value
                            If row.Cells(ALLDONE.Index).Value = True Then
                                DONE = DONE & "|" & "1"
                            Else
                                DONE = DONE & "|" & "0"
                            End If
                            OUTPCS = OUTPCS & "|" & row.Cells(ALLOUTPCS.Index).Value
                            OUTMTRS = OUTMTRS & "|" & row.Cells(ALLOUTMTRS.Index).Value
                            FROMNO = FROMNO & "|" & Val(row.Cells(ALLFROMNO.Index).Value)
                            FROMSRNO = FROMSRNO & "|" & Val(row.Cells(ALLFROMSRNO.Index).Value)
                            FROMTYPE = FROMTYPE & "|" & row.Cells(ALLFROMTYPE.Index).Value

                        End If
                    End If
                Next

            End If

            alParaval.Add(GRIDSRNO)
            alParaval.Add(PIECETYPE)
            alParaval.Add(ITEMNAME)
            alParaval.Add(QUALITY)
            alParaval.Add(DESIGN)
            alParaval.Add(gridremarks)
            alParaval.Add(COLOR)
            alParaval.Add(CUT)
            alParaval.Add(QTY)
            alParaval.Add(QTYUNIT)
            alParaval.Add(MTRS)
            alParaval.Add(RATE)
            alParaval.Add(PER)
            alParaval.Add(AMOUNT)
            alParaval.Add(RACK)
            alParaval.Add(SHELF)
            alParaval.Add(BARCODE)
            alParaval.Add(DONE)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)

            alParaval.Add(Val(LBLTOTALAMOUNT.Text.Trim))


            If CHKMANUALRATE.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If

            Dim OBJJobIn As New ClsRecFromPacking()
            OBJJobIn.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = OBJJobIn.SAVE()
                MsgBox("Details Added")
                TXTRECNO.Text = Val(DTTABLE.Rows(0).Item(0))
                TEMPRECNO = Val(DTTABLE.Rows(0).Item(0))

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPRECNO)
                IntResult = OBJJobIn.UPDATE()
                MsgBox("Details Updated")
            End If

            PRINTBARCODE()
            EDIT = False
            clear()
            FILLBARCODE()
            If ClientName = "SOFTAS" Then CMBBARCODE.Focus() Else cmbGodown.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If ClientName = "RAJKRIPA" Then Exit Sub
            If MsgBox("Wish to Print Rec Report...?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJMATREC As New MATRECDesign
                OBJMATREC.MdiParent = MDIMain
                OBJMATREC.FRMSTRING = "RECPACK"
                OBJMATREC.WHERECLAUSE = "{RECPACKING.REC_no}=" & Val(TXTRECNO.Text.Trim) & " AND {RECPACKING.REC_yearid}=" & YearId
                OBJMATREC.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTBARCODE()
        Try
            If ALLOWBARCODEPRINT Then

                'PRINT BARCODE
                Dim TEMPMSG As Integer = MsgBox("Wish To Print Barcode?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                'GET FRESH DATA FROM DATABASE (ONLY GRID)
                'THIS IS DONE COZ FOR MULTIUSER THE NOS WILL BE SAME
                'SO WE WILL ADD BARCODE IN SP AND THEN FETCH THAT DATA HERE AFTER THAT WE WILL PRINT BARCODES
                GRIDREC.RowCount = 0
                Dim OBJREC As New ClsRecFromPacking()
                OBJREC.alParaval.Add(TEMPRECNO)
                OBJREC.alParaval.Add(YearId)
                Dim dttable As DataTable = OBJREC.SELECTRECPACKING()
                For Each dr As DataRow In dttable.Rows
                    GRIDREC.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE"), dr("ITEM").ToString, dr("QUALITY").ToString, dr("DESIGN").ToString, dr("GRIDREMARKS").ToString, dr("COLOR"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("qty")), "0.00"), dr("UNIT").ToString, Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER").ToString, Format(Val(dr("AMOUNT")), "0.00"), dr("RACK"), dr("SHELF"), dr("BARCODE"), 0, dr("OUTPCS"), dr("OUTMTRS"), Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("FROMTYPE"))
                Next


                Dim WHOLESALEBARCODE As Integer = 0
                If ClientName = "CC"  Or ClientName = "C3" Or ClientName = "SHREEDEV" Then WHOLESALEBARCODE = MsgBox("Wish To Print Wholesale Barcode?", MsgBoxStyle.YesNo)


                Dim TEMPHEADER As String = ""
                'If ClientName = "YASHVI" Then
                '    TEMPHEADER = InputBox("Enter Sticker Type (M/N/O/P/Y)", "", "Y")
                '    If TEMPHEADER <> "M" And TEMPHEADER <> "N" And TEMPHEADER <> "O" And TEMPHEADER <> "P" And TEMPHEADER <> "Y" Then Exit Sub
                '    If TEMPHEADER = "M" Then TEMPHEADER = "MAFATLAL"
                '    If TEMPHEADER = "N" Or TEMPHEADER = "P" Then TEMPHEADER = ""
                '    If TEMPHEADER = "O" Then TEMPHEADER = "ORGALIN"
                '    If TEMPHEADER = "Y" Then TEMPHEADER = "PREPRINTED"
                'End If
                If ClientName = "YASHVI" Then
                    TEMPHEADER = InputBox("Enter Sticker Type (M/N/Y/B)")
                    If TEMPHEADER <> "M" And TEMPHEADER <> "N" And TEMPHEADER <> "Y" And TEMPHEADER <> "B" Then Exit Sub
                    If TEMPHEADER = "M" Then TEMPHEADER = "MAFATLAL"
                    If TEMPHEADER = "N" Then TEMPHEADER = ""
                End If




                If ClientName = "GELATO" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 For NORMAL" & Chr(13) & "2 For MRP" & Chr(13) & "3 For WSP")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
                End If

                If ClientName = "DAKSH" Or ClientName = "KUNAL" Or ClientName = "VALIANT" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 For NORMAL" & Chr(13) & "2 For PRE PRINTED")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If

                If ClientName = "SST" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR PRE PRINTED" & Chr(13) & "3 FOR MRP")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
                End If

                If ClientName = "MANSI" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR PRE PRINTED" & Chr(13) & "3 FOR MRP" & Chr(13) & "4 FOR 100 X 50")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" And TEMPHEADER <> "4" Then Exit Sub
                End If

                If ClientName = "RAJKRIPA" Or ClientName = "MOHATUL" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 For LUMP" & Chr(13) & "2 For CUTPACK")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If

                If ClientName = "MANS" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 For SALVATROE" & Chr(13) & "2 For DONBION")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If


                If ClientName = "KRISHNA" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "SIMPLEX" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 For NORMAL" & Chr(13) & "2 For BOX STICKER")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If


                If ClientName = "SOFTAS" And CHKPRINTSERIES.Checked = True Then
                    TEMPHEADER = "PRINTSERIES"
                End If

                Dim SUPRIYAHEADER As String = ""
                If ClientName = "SUPRIYA" Then
                    TEMPHEADER = InputBox("Enter Sticker Type (1/2/3/4/5/6/7)")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" And TEMPHEADER <> "4" And TEMPHEADER <> "5" And TEMPHEADER <> "6" And TEMPHEADER <> "7" Then Exit Sub
                    If TEMPHEADER = "1" Or TEMPHEADER = "6" Then SUPRIYAHEADER = "ROYAL TEX"
                    If TEMPHEADER = "2" Or TEMPHEADER = "7" Then SUPRIYAHEADER = "DEEP BLUE"
                    If TEMPHEADER = "3" Then SUPRIYAHEADER = ""
                    If TEMPHEADER = "4" Then SUPRIYAHEADER = "KAMDHENU"
                    If TEMPHEADER = "5" Then SUPRIYAHEADER = "5"
                End If

                For Each ROW As DataGridViewRow In GRIDREC.Rows
                    'TO PRINT BARCODE FROM SELECTED SRNO
                    If (Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0) Then
                        If Val(ROW.Cells(gsrno.Index).Value) < Val(TXTFROM.Text.Trim) Or Val(ROW.Cells(gsrno.Index).Value) > Val(TXTTO.Text.Trim) Then GoTo NEXTLINE
                    End If

                    'IF barcode is used the BARCODE printING WILL BE BLOCKED
                    If Val(ROW.Cells(GOUTMTRS.Index).Value) > 0 Then GoTo NEXTLINE
                    Dim BALENO As String = ""
                    If ClientName = "SSC" Or ClientName = "REALCORPORATION" Then BALENO = ROW.Cells(gdesc.Index).Value

                    'FOR AVIS GET LOTNO FROM ISSUE TO PACK FOR EACH ENTRY NO
                    Dim OBJCMN As New ClsCommon
                    If HIDEALLISSUE = False Then
                        Dim DTLOT As DataTable = OBJCMN.SEARCH("ISS_LOTNO AS LOTNO", "", " ISSUEPACKING_DESC", " AND ISS_NO = " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND ISS_GRIDSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND ISS_YEARID = " & YearId)
                        If DTLOT.Rows.Count > 0 Then TXTLOTNO.Text = DTLOT.Rows(0).Item("LOTNO")
                    End If

                    BARCODEPRINTING(ROW.Cells(GBARCODE.Index).Value, ROW.Cells(GPIECETYPE.Index).Value, ROW.Cells(gitemname.Index).Value, ROW.Cells(GQUALITY.Index).Value, ROW.Cells(GDESIGN.Index).Value, ROW.Cells(gcolor.Index).Value, ROW.Cells(gqtyunit.Index).Value, TXTLOTNO.Text.Trim, BALENO, ROW.Cells(gdesc.Index).Value, Val(ROW.Cells(GMTRS.Index).Value), Val(ROW.Cells(gQty.Index).Value), Val(ROW.Cells(gcut.Index).Value), ROW.Cells(GRACK.Index).Value, TEMPHEADER, SUPRIYAHEADER, WHOLESALEBARCODE, "", cmbname.Text.Trim, ROW.Cells(GSHELF.Index).Value)
                    dttable = OBJCMN.Execute_Any_String("UPDATE RECPACKING SET REC_BARCODEPRINTED = 1 WHERE REC_NO = " & TEMPRECNO & " And REC_YEARID = " & YearId, "", "")
                    LBLBARCODEPRINTED.Visible = True

NEXTLINE:

                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JobIn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'Grid Focus
                GRIDREC.Focus()
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1 Then       'for Delete
                TabControl1.SelectedIndex = (0)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2 Then       'for Delete
                TabControl1.SelectedIndex = (1)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolprevious_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_Click(sender, e)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)

            ElseIf e.KeyCode = Keys.F12 And GPALL.Enabled = True And HIDEALLISSUE = False Then

                If Val(TXTRUNNINGBAL.Text.Trim) > 0 Then
                    MsgBox("Pending Mtrs Not Allowed", MsgBoxStyle.Critical)
                    Exit Sub
                End If


                'REMOVING ROWS FROM GRIDALLREC
1:
                For Each ROW As DataGridViewRow In GRIDALLREC.Rows
                    If ROW.Cells(ALLFROMSRNO.Index).Value = GRIDISSUE.CurrentRow.Cells(ISRNO.Index).Value Then
                        GRIDALLREC.Rows.RemoveAt(ROW.Index)
                        GoTo 1
                    End If
                Next
                getsrno(GRIDAREC)



                'INSERT THE COMPLETE GRIDAREC IN GRIDALLREC
                For Each ROW As DataGridViewRow In GRIDAREC.Rows
                    GRIDALLREC.Rows.Add(ROW.Cells(ASRNO.Index).Value, ROW.Cells(APIECETYPE.Index).Value, ROW.Cells(AITEMNAME.Index).Value, "", ROW.Cells(ADESIGN.Index).Value, ROW.Cells(ADESC.Index).Value, ROW.Cells(ACOLOR.Index).Value, Format(Val(ROW.Cells(ACUT.Index).Value), "0.00"), Format(Val(ROW.Cells(AQTY.Index).Value), "0.00"), ROW.Cells(AQTYUNIT.Index).Value, Format(Val(ROW.Cells(AMTRS.Index).Value), "0.00"), ROW.Cells(ARACK.Index).Value, ROW.Cells(ASHELF.Index).Value, ROW.Cells(ABARCODE.Index).Value, ROW.Cells(ADONE.Index).Value, Val(ROW.Cells(AOUTPCS.Index).Value), Val(ROW.Cells(AOUTMTRS.Index).Value), Val(ROW.Cells(AFROMNO.Index).Value), Val(ROW.Cells(AFROMSRNO.Index).Value), ROW.Cells(AFROMTYPE.Index).Value, Val(ROW.Cells(AMAINSRNO.Index).Value))
                Next

                GRIDALLREC.Sort(GRIDALLREC.Columns(ALLMAINSRNO.Index), System.ComponentModel.ListSortDirection.Ascending)


                GRIDISSUE.CurrentRow.DefaultCellStyle.BackColor = Color.Yellow
                GRIDISSUE.CurrentRow.ReadOnly = True

                GRIDISSUE.Enabled = True
                GPALL.Enabled = False
                GRIDAREC.RowCount = 0
                TXTISSUEMTRS.Clear()
                TXTPENDINGMTRS.Clear()
                TXTRUNNINGBAL.Clear()
                TXTFROMSRNO.Clear()
                TXTFROMTYPE.Clear()

                LBLATOTALMTRS.Text = 0
                LBLATOTALQTY.Text = 0

                MsgBox("Details Saved")
                If GRIDISSUE.CurrentRow.Index < GRIDISSUE.RowCount - 1 Then GRIDISSUE.CurrentCell = GRIDISSUE.Rows(GRIDISSUE.CurrentRow.Index + 1).Cells(IMTRS.Index)
                GRIDISSUE.Select()



            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RecFromPacking_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'JOB IN'")
                        USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            If ClientName = "SUPEEMA" Or ClientName = "SURYODAYA" Or ClientName = "SARAYU" Or ClientName = "AFW" Then HIDEALLISSUE = False


            clear()



            cmbname.Enabled = True

            If ClientName = "SVS" Then
                gQty.ReadOnly = True
                txtqty.ReadOnly = True
                txtqty.Text = 1
                txtqty.BackColor = Color.Linen
            End If

            If ISSUEBARCODE <> "" And EDIT = False Then
                CMBBARCODE.Text = ISSUEBARCODE
                CMBBARCODE_Validated(sender, e)
            End If

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJREC As New ClsRecFromPacking()
                OBJREC.alParaval.Add(TEMPRECNO)
                OBJREC.alParaval.Add(YearId)
                Dim dttable As DataTable = OBJREC.SELECTRECPACKING()
                If dttable.Rows.Count > 0 Then


                    TXTFROMNO.Text = Val(dttable.Rows(0).Item("ISSUENO"))
                    TXTFROMSRNO.Text = Val(dttable.Rows(0).Item("ISSUESRNO"))
                    TXTFROMTYPE.Text = dttable.Rows(0).Item("ISSUETYPE")
                    If HIDEALLISSUE = False Then TXTFROMNO_Validated(sender, e)

                    For Each dr As DataRow In dttable.Rows

                        TXTRECNO.Text = TEMPRECNO
                        TXTRECNO.ReadOnly = True

                        CMDSHORTAGE.Enabled = False

                        RECDATE.Text = Format(Convert.ToDateTime(dr("RECDATE")).Date, "dd/MM/yyyy")
                        cmbname.Text = Convert.ToString(dr("NAME"))
                        cmbGodown.Text = dr("GODOWN")
                        TXTLOTNO.Text = dr("LOTNO")
                        TXTREFNO.Text = dr("REFNO")
                        CMBBARCODE.Text = dr("OUTBARCODE")
                        If ClientName <> "AVIS" Or (ClientName = "AVIS" And UserName <> "Admin") Then CMBBARCODE.Enabled = False


                        TXTISSUEMTRS.Text = Val(dr("ISSUEMTRS"))
                        TXTPENDINGMTRS.Text = Val(dr("PENDINGMTRS"))


                        lbltotalqty.Text = Format(Val(dr("TOTALQTY")), "0.00")
                        LBLTOTALMTRS.Text = Format(Val(dr("TOTALMTRS")), "0.00")

                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                        If HIDEALLISSUE = True Then
                            GRIDREC.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE"), dr("ITEM").ToString, dr("QUALITY").ToString, dr("DESIGN").ToString, dr("GRIDREMARKS").ToString, dr("COLOR"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("qty")), "0.00"), dr("UNIT").ToString, Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER"), Format(Val(dr("AMOUNT")), "0.00"), dr("RACK"), dr("SHELF"), dr("BARCODE"), 0, dr("OUTPCS"), dr("OUTMTRS"), Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("FROMTYPE"))
                        Else
                            GRIDALLREC.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE"), dr("ITEM").ToString, dr("QUALITY").ToString, dr("DESIGN").ToString, dr("GRIDREMARKS").ToString, dr("COLOR"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("qty")), "0.00"), dr("UNIT").ToString, Format(Val(dr("MTRS")), "0.00"), dr("RACK"), dr("SHELF"), dr("BARCODE"), 0, dr("OUTPCS"), dr("OUTMTRS"), Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("FROMTYPE"), Val(dr("GRIDSRNO")))

                            'LOCK ISSUEGRID
                            GRIDISSUE.Rows(Val(dr("FROMSRNO")) - 1).DefaultCellStyle.BackColor = Color.Yellow
                            GRIDISSUE.Rows(Val(dr("FROMSRNO")) - 1).ReadOnly = True
                        End If

                        If Val(dr("OUTMTRS")) > 0 Then
                            If HIDEALLISSUE = True Then GRIDREC.Rows(GRIDREC.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        CHKMANUALRATE.Checked = Convert.ToBoolean(dr("MANUALRATE"))

                        If UserName = "Admin" Then
                            If CHKMANUALRATE.CheckState = CheckState.Checked Then
                                TXTRATE.ReadOnly = False
                                GRATE.ReadOnly = False
                            End If
                        End If
                        If Convert.ToBoolean(dr("BARCODEPRINTED")) = True Then LBLBARCODEPRINTED.Visible = True
                    Next


                    TOTAL()
                    If HIDEALLISSUE = True Then GRIDREC.FirstDisplayedScrollingRowIndex = GRIDREC.RowCount - 1
                Else
                    EDIT = False
                    clear()
                End If

            End If
            If ClientName = "MNARESH" AndAlso EDIT = True AndAlso UserName <> "Admin" Then
                RECDATE.ReadOnly = True
            Else
                RECDATE.ReadOnly = False
            End If
            txtsrno.Text = GRIDREC.RowCount

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Sub FILLBARCODE()
        Try
            CMBBARCODE.Items.Clear()
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" ISS_BARCODE AS BARCODE ", "", " ISSUEPACKING_DESC ", " AND ROUND(ISS_MTRS-ISS_OUTMTRS,2) > 0  AND ISS_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                For Each DTROW As DataRow In DT.Rows
                    CMBBARCODE.Items.Add(DTROW("BARCODE"))
                Next
            End If
            If ClientName = "YASHVI" And CMBBARCODE.Items.Count > 0 Then CMBBARCODE.SelectedIndex = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            FILLNAME(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors'")
            fillGODOWN(cmbGodown, EDIT)

            If HIDEALLISSUE = True Then
                fillPIECETYPE(CMBPIECETYPE)
                fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
                fillQUALITY(CMBQUALITY, EDIT)
                FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
                fillunit(cmbqtyunit)
                FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
                FILLRACK(CMBRACK)
                FILLSHELF(CMBSHELF)
                FILLBARCODE()
            Else
                fillPIECETYPE(CMBAPIECETYPE)
                fillitemname(CMBAITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
                FILLDESIGN(CMBADESIGN, CMBAITEMNAME.Text.Trim)
                fillunit(CMBAQTYUNIT)
                FILLCOLOR(CMBACOLOR, CMBADESIGN.Text.Trim, CMBAITEMNAME.Text.Trim)
                FILLRACK(CMBARACK)
                FILLSHELF(CMBASHELF)
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_ENTER(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGodown.Enter
        Try
            If cmbGodown.Text.Trim = "" Then fillGODOWN(cmbGodown, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbGodown.Validating
        Try
            If cmbGodown.Text.Trim <> "" Then GODOWNVALIDATE(cmbGodown, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJREC As New RecFromPackingDetails
            OBJREC.MdiParent = MDIMain
            OBJREC.Show()
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

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDREC.RowCount = 0
                GRIDALLREC.RowCount = 0
                TEMPRECNO = Val(tstxtbillno.Text)
                If TEMPRECNO > 0 Then
                    EDIT = True
                    RecFromPacking_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            GRIDREC.Enabled = True

            If GRIDDOUBLECLICK = False Then

                If Val(TXTCUT.Text.Trim) > 0 And (ClientName <> "AVIS") Then
                    Dim TEMPQTY As Integer = Val(txtqty.Text.Trim)
                    txtqty.Text = 1
                    For I As Integer = 1 To TEMPQTY
                        If GRIDDOUBLECLICK = False Then
                            If EDIT = True Then
                                'GET LAST BARCODE SRNO
                                Dim LSRNO As Integer = 0
                                Dim RSRNO As Integer = 0
                                Dim SNO As Integer = 0
                                LSRNO = InStr(GRIDREC.Rows(GRIDREC.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                                RSRNO = InStr(LSRNO + 1, GRIDREC.Rows(GRIDREC.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                                SNO = GRIDREC.Rows(GRIDREC.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                                TXTBARCODE.Text = "P-" & Val(TXTRECNO.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                            Else
                                TXTBARCODE.Text = "P-" & Val(TXTRECNO.Text.Trim) & "/" & GRIDREC.RowCount + 1 & "/" & YearId
                            End If
                        End If
                        GRIDREC.Rows.Add(Val(txtsrno.Text.Trim), CMBPIECETYPE.Text.Trim, cmbitemname.Text.Trim, CMBQUALITY.Text.Trim, CMBDESIGN.Text.Trim, txtgridremarks.Text.Trim, cmbcolor.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(txtqty.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Format(Val(TXTAMOUNT.Text.Trim), "0.00"), CMBRACK.Text.Trim, CMBSHELF.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTFROMTYPE.Text.Trim)
                    Next
                Else
                    GRIDREC.Rows.Add(Val(txtsrno.Text.Trim), CMBPIECETYPE.Text.Trim, cmbitemname.Text.Trim, CMBQUALITY.Text.Trim, CMBDESIGN.Text.Trim, txtgridremarks.Text.Trim, cmbcolor.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(txtqty.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Format(Val(TXTAMOUNT.Text.Trim), "0.00"), CMBRACK.Text.Trim, CMBSHELF.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTFROMTYPE.Text.Trim)
                End If
                getsrno(GRIDREC)

            ElseIf GRIDDOUBLECLICK = True Then
                GRIDREC.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
                GRIDREC.Item(GPIECETYPE.Index, TEMPROW).Value = CMBPIECETYPE.Text.Trim
                GRIDREC.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
                GRIDREC.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
                GRIDREC.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
                GRIDREC.Item(gdesc.Index, TEMPROW).Value = txtgridremarks.Text.Trim
                GRIDREC.Item(gcolor.Index, TEMPROW).Value = cmbcolor.Text.Trim
                GRIDREC.Item(gcut.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
                GRIDREC.Item(gQty.Index, TEMPROW).Value = Val(txtqty.Text.Trim)
                GRIDREC.Item(gqtyunit.Index, TEMPROW).Value = cmbqtyunit.Text.Trim
                GRIDREC.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
                GRIDREC.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")
                GRIDREC.Item(GPER.Index, TEMPROW).Value = CMBPER.Text.Trim
                GRIDREC.Item(GAMOUNT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text.Trim), "0.00")
                GRIDREC.Item(GRACK.Index, TEMPROW).Value = CMBRACK.Text.Trim
                GRIDREC.Item(GSHELF.Index, TEMPROW).Value = CMBSHELF.Text.Trim
                GRIDDOUBLECLICK = False
            End If

            TOTAL()

            GRIDREC.FirstDisplayedScrollingRowIndex = GRIDREC.RowCount - 1

            If ClientName = "SOFTAS" Then CMBQUALITY.Text = ""

            If ClientName = "YASHVI" Then TXTMTRS.Text = Val(TXTRUNNINGBAL.Text.Trim) Else TXTMTRS.Clear()
            If ClientName <> "SHREENAKODA" And ClientName <> "SOFTAS" And ClientName <> "MOHATUL" Then CMBRACK.Text = ""
            CMBSHELF.Text = ""
            txtsrno.Text = GRIDREC.RowCount + 1
            If ClientName = "YASHVI" Or ClientName = "SHREENAKODA" Then TXTCUT.Focus() Else CMBPIECETYPE.Focus()
            If ClientName = "SOFTAS" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then TXTMTRS.Focus()
            If ClientName = "KCRAYON" Then txtgridremarks.Clear()
            If ClientName = "SUPRIYA" Or ClientName = "YASHVI" Then TXTCUT.Clear()
            'TXTRATE.Clear()
            TXTAMOUNT.Clear()
            If ClientName = "SUPRIYA" Or ClientName = "MNIKHIL" Or ClientName = "HRITI" Or ClientName = "AVIS" Then
                txtgridremarks.Clear()
                TXTMTRS.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLAGRID()
        Try
            GRIDAREC.Enabled = True

            If GRIDDOUBLECLICK = False Then

                If Val(TXTACUT.Text.Trim) > 0 Then 'And (ClientName = "SBA" Or ClientName = "MIDAS") Then
                    Dim TEMPQTY As Integer = Val(TXTAQTY.Text.Trim)
                    TXTAQTY.Text = 1
                    For I As Integer = 1 To TEMPQTY
                        If GRIDDOUBLECLICK = False Then
                            If EDIT = True Then

                                'FIRST SORT THE GRID WITH RESPECT TO BARCODE NO
                                GRIDALLREC.Sort(GRIDALLREC.Columns(ALLMAINSRNO.Index), System.ComponentModel.ListSortDirection.Ascending)

                                'GET LAST BARCODE SRNO
                                Dim LSRNO As Integer = 0
                                Dim RSRNO As Integer = 0
                                Dim SNO As Integer = 0
                                LSRNO = InStr(GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLBARCODE.Index).Value, "/")
                                RSRNO = InStr(LSRNO + 1, GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLBARCODE.Index).Value, "/")
                                SNO = GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                                TXTBARCODE.Text = "P-" & Val(TXTRECNO.Text.Trim) & "/" & SNO + 1 & "/" & YearId

                            Else
                                'TXTBARCODE.Text = "P-" & Val(TXTRECNO.Text.Trim) & "/" & Val(GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLMAINSRNO.Index).Value) + 1 & "/" & YearId
                                If GRIDALLREC.RowCount > 0 Then TXTBARCODE.Text = "P-" & Val(TXTRECNO.Text.Trim) & "/" & Val(GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLMAINSRNO.Index).Value) + 1 & "/" & YearId Else TXTBARCODE.Text = "P-" & Val(TXTRECNO.Text.Trim) & "/" & Val(GRIDAREC.RowCount) + 1 & "/" & YearId
                            End If
                        End If
                        If GRIDALLREC.RowCount > 0 Then
                            GRIDAREC.Rows.Add(Val(TXTASRNO.Text.Trim), CMBAPIECETYPE.Text.Trim, CMBAITEMNAME.Text.Trim, "", CMBADESIGN.Text.Trim, TXTAGRIDREMARKS.Text.Trim, CMBACOLOR.Text.Trim, Format(Val(TXTACUT.Text.Trim), "0.00"), Format(Val(TXTAQTY.Text.Trim), "0.00"), CMBAQTYUNIT.Text.Trim, Format(Val(TXTAMTRS.Text.Trim), "0.00"), CMBARACK.Text.Trim, CMBASHELF.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTFROMTYPE.Text.Trim, Val(GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLMAINSRNO.Index).Value) + 1)
                            GRIDALLREC.Rows.Add(Val(TXTASRNO.Text.Trim), CMBAPIECETYPE.Text.Trim, CMBAITEMNAME.Text.Trim, "", CMBADESIGN.Text.Trim, TXTAGRIDREMARKS.Text.Trim, CMBACOLOR.Text.Trim, Format(Val(TXTACUT.Text.Trim), "0.00"), Format(Val(TXTAQTY.Text.Trim), "0.00"), CMBAQTYUNIT.Text.Trim, Format(Val(TXTAMTRS.Text.Trim), "0.00"), CMBARACK.Text.Trim, CMBASHELF.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTFROMTYPE.Text.Trim, Val(GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLMAINSRNO.Index).Value) + 1)
                        Else
                            GRIDAREC.Rows.Add(Val(TXTASRNO.Text.Trim), CMBAPIECETYPE.Text.Trim, CMBAITEMNAME.Text.Trim, "", CMBADESIGN.Text.Trim, TXTAGRIDREMARKS.Text.Trim, CMBACOLOR.Text.Trim, Format(Val(TXTACUT.Text.Trim), "0.00"), Format(Val(TXTAQTY.Text.Trim), "0.00"), CMBAQTYUNIT.Text.Trim, Format(Val(TXTAMTRS.Text.Trim), "0.00"), CMBARACK.Text.Trim, CMBASHELF.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTFROMTYPE.Text.Trim, Val(GRIDAREC.RowCount) + 1)
                            GRIDALLREC.Rows.Add(Val(TXTASRNO.Text.Trim), CMBAPIECETYPE.Text.Trim, CMBAITEMNAME.Text.Trim, "", CMBADESIGN.Text.Trim, TXTAGRIDREMARKS.Text.Trim, CMBACOLOR.Text.Trim, Format(Val(TXTACUT.Text.Trim), "0.00"), Format(Val(TXTAQTY.Text.Trim), "0.00"), CMBAQTYUNIT.Text.Trim, Format(Val(TXTAMTRS.Text.Trim), "0.00"), CMBARACK.Text.Trim, CMBASHELF.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTFROMTYPE.Text.Trim, Val(GRIDALLREC.RowCount) + 1)
                        End If


                    Next
                Else
                    If GRIDALLREC.RowCount > 0 Then
                        GRIDAREC.Rows.Add(Val(TXTASRNO.Text.Trim), CMBAPIECETYPE.Text.Trim, CMBAITEMNAME.Text.Trim, "", CMBADESIGN.Text.Trim, TXTAGRIDREMARKS.Text.Trim, CMBACOLOR.Text.Trim, Format(Val(TXTACUT.Text.Trim), "0.00"), Format(Val(TXTAQTY.Text.Trim), "0.00"), CMBAQTYUNIT.Text.Trim, Format(Val(TXTAMTRS.Text.Trim), "0.00"), CMBARACK.Text.Trim, CMBASHELF.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTFROMTYPE.Text.Trim, Val(GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLMAINSRNO.Index).Value) + 1)
                        GRIDALLREC.Rows.Add(Val(TXTASRNO.Text.Trim), CMBAPIECETYPE.Text.Trim, CMBAITEMNAME.Text.Trim, "", CMBADESIGN.Text.Trim, TXTAGRIDREMARKS.Text.Trim, CMBACOLOR.Text.Trim, Format(Val(TXTACUT.Text.Trim), "0.00"), Format(Val(TXTAQTY.Text.Trim), "0.00"), CMBAQTYUNIT.Text.Trim, Format(Val(TXTAMTRS.Text.Trim), "0.00"), CMBARACK.Text.Trim, CMBASHELF.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTFROMTYPE.Text.Trim, Val(GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLMAINSRNO.Index).Value) + 1)
                    Else
                        GRIDAREC.Rows.Add(Val(TXTASRNO.Text.Trim), CMBAPIECETYPE.Text.Trim, CMBAITEMNAME.Text.Trim, "", CMBADESIGN.Text.Trim, TXTAGRIDREMARKS.Text.Trim, CMBACOLOR.Text.Trim, Format(Val(TXTACUT.Text.Trim), "0.00"), Format(Val(TXTAQTY.Text.Trim), "0.00"), CMBAQTYUNIT.Text.Trim, Format(Val(TXTAMTRS.Text.Trim), "0.00"), CMBARACK.Text.Trim, CMBASHELF.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTFROMTYPE.Text.Trim, Val(GRIDAREC.RowCount) + 1)
                        GRIDALLREC.Rows.Add(Val(TXTASRNO.Text.Trim), CMBAPIECETYPE.Text.Trim, CMBAITEMNAME.Text.Trim, "", CMBADESIGN.Text.Trim, TXTAGRIDREMARKS.Text.Trim, CMBACOLOR.Text.Trim, Format(Val(TXTACUT.Text.Trim), "0.00"), Format(Val(TXTAQTY.Text.Trim), "0.00"), CMBAQTYUNIT.Text.Trim, Format(Val(TXTAMTRS.Text.Trim), "0.00"), CMBARACK.Text.Trim, CMBASHELF.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTFROMTYPE.Text.Trim, Val(GRIDALLREC.RowCount) + 1)
                    End If

                End If
                    getsrno(GRIDAREC)

            ElseIf GRIDDOUBLECLICK = True Then
                GRIDAREC.Item(ASRNO.Index, TEMPROW).Value = Val(TXTASRNO.Text.Trim)
                GRIDAREC.Item(APIECETYPE.Index, TEMPROW).Value = CMBAPIECETYPE.Text.Trim
                GRIDAREC.Item(AITEMNAME.Index, TEMPROW).Value = CMBAITEMNAME.Text.Trim

                GRIDAREC.Item(ADESIGN.Index, TEMPROW).Value = CMBADESIGN.Text.Trim
                GRIDAREC.Item(ADESC.Index, TEMPROW).Value = TXTAGRIDREMARKS.Text.Trim
                GRIDAREC.Item(ACOLOR.Index, TEMPROW).Value = CMBACOLOR.Text.Trim
                GRIDAREC.Item(ACUT.Index, TEMPROW).Value = Format(Val(TXTACUT.Text.Trim), "0.00")
                GRIDAREC.Item(AQTY.Index, TEMPROW).Value = Val(TXTAQTY.Text.Trim)
                GRIDAREC.Item(AQTYUNIT.Index, TEMPROW).Value = CMBAQTYUNIT.Text.Trim
                GRIDAREC.Item(AMTRS.Index, TEMPROW).Value = Format(Val(TXTAMTRS.Text.Trim), "0.00")
                GRIDAREC.Item(ARACK.Index, TEMPROW).Value = CMBARACK.Text.Trim
                GRIDAREC.Item(ASHELF.Index, TEMPROW).Value = CMBASHELF.Text.Trim
                GRIDDOUBLECLICK = False
            End If

            TOTAL()

            GRIDAREC.FirstDisplayedScrollingRowIndex = GRIDAREC.RowCount - 1
            TXTRATE.Clear()
            TXTAMOUNT.Clear()
            TXTAMTRS.Clear()
            If ClientName <> "SHREENAKODA" Then CMBARACK.Text = ""
            CMBASHELF.Text = ""
            TXTASRNO.Text = GRIDAREC.RowCount + 1
            If ClientName = "YASHVI" Or ClientName = "SHREENAKODA" Then TXTACUT.Focus() Else CMBAPIECETYPE.Focus()
            If ClientName = "SOFTAS" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then TXTAMTRS.Focus()
            If ClientName = "KCRAYON" Then TXTAGRIDREMARKS.Clear()
            If ClientName = "SUPRIYA" Then TXTACUT.Clear()
            If ClientName = "AFW" Then CMBACOLOR.Focus()

            If ClientName = "SUPRIYA" Or ClientName = "MNIKHIL" Or ClientName = "HRITI" Then
                TXTAGRIDREMARKS.Clear()
                TXTAMTRS.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDJOBIN_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDREC.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDREC.RowCount = 0
LINE1:
            TEMPRECNO = Val(TXTRECNO.Text) - 1
            If TEMPRECNO > 0 Then
                EDIT = True
                RecFromPacking_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDREC.RowCount = 0 And GRIDALLREC.RowCount = 0 And TEMPRECNO > 1 Then
                TXTRECNO.Text = TEMPRECNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            GRIDREC.RowCount = 0
LINE1:
            TEMPRECNO = Val(TXTRECNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTRECNO.Text.Trim
            clear()
            If Val(TXTRECNO.Text) - 1 >= TEMPRECNO Then
                EDIT = True
                RecFromPacking_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDREC.RowCount = 0 And GRIDALLREC.RowCount = 0 And TEMPRECNO < MAXNO Then
                TXTRECNO.Text = TEMPRECNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            Dim IntResult As Integer
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Entry Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                Dim alParaval As New ArrayList
                alParaval.Add(Val(TXTRECNO.Text.Trim))
                alParaval.Add(Userid)
                alParaval.Add(YearId)

                Dim OBJREC As New ClsRecFromPacking()
                OBJREC.alParaval = alParaval
                IntResult = OBJREC.Delete()
                MsgBox("Entry Deleted")
                clear()
                EDIT = False

            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbqtyunit.Enter, CMBAQTYUNIT.Enter
        Try
            If sender.Text.Trim = "" Then fillunit(sender)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbqtyunit.Validating, CMBAQTYUNIT.Validating
        Try
            If sender.Text.Trim <> "" Then unitvalidate(sender, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDJOBIN_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDREC.CellValidating
        Try
            Dim colNum As Integer = GRIDREC.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GMTRS.Index, gcut.Index, GRATE.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDREC.CurrentCell.Value = Nothing Then GRIDREC.CurrentCell.Value = "0.00"
                        GRIDREC.CurrentCell.Value = Convert.ToDecimal(GRIDREC.Item(colNum, e.RowIndex).Value)
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

    Sub EDITROW()
        Try
            If GRIDREC.CurrentRow.Index >= 0 And GRIDREC.Item(gsrno.Index, GRIDREC.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                txtsrno.Text = GRIDREC.Item(gsrno.Index, GRIDREC.CurrentRow.Index).Value.ToString
                CMBPIECETYPE.Text = GRIDREC.Item(GPIECETYPE.Index, GRIDREC.CurrentRow.Index).Value.ToString
                cmbitemname.Text = GRIDREC.Item(gitemname.Index, GRIDREC.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDREC.Item(GQUALITY.Index, GRIDREC.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDREC.Item(GDESIGN.Index, GRIDREC.CurrentRow.Index).Value.ToString
                txtgridremarks.Text = GRIDREC.Item(gdesc.Index, GRIDREC.CurrentRow.Index).Value.ToString
                cmbcolor.Text = GRIDREC.Item(gcolor.Index, GRIDREC.CurrentRow.Index).Value.ToString
                TXTCUT.Text = GRIDREC.Item(gcut.Index, GRIDREC.CurrentRow.Index).Value.ToString
                txtqty.Text = GRIDREC.Item(gQty.Index, GRIDREC.CurrentRow.Index).Value.ToString
                cmbqtyunit.Text = GRIDREC.Item(gqtyunit.Index, GRIDREC.CurrentRow.Index).Value.ToString
                TXTMTRS.Text = GRIDREC.Item(GMTRS.Index, GRIDREC.CurrentRow.Index).Value.ToString
                TXTRATE.Text = GRIDREC.Item(GRATE.Index, GRIDREC.CurrentRow.Index).Value.ToString
                CMBPER.Text = GRIDREC.Item(GPER.Index, GRIDREC.CurrentRow.Index).Value.ToString
                TXTAMOUNT.Text = GRIDREC.Item(GAMOUNT.Index, GRIDREC.CurrentRow.Index).Value.ToString
                CMBRACK.Text = GRIDREC.Item(GRACK.Index, GRIDREC.CurrentRow.Index).Value.ToString
                CMBSHELF.Text = GRIDREC.Item(GSHELF.Index, GRIDREC.CurrentRow.Index).Value.ToString
                TEMPROW = GRIDREC.CurrentRow.Index
                cmbitemname.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDJOBIN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDREC.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDREC.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                'end of block
                GRIDREC.Rows.RemoveAt(GRIDREC.CurrentRow.Index)
                getsrno(GRIDREC)
                TOTAL()
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcolor.Enter, CMBACOLOR.Enter
        Try
            Dim ITEMOBJ As New Object
            If HIDEALLISSUE = False Then ITEMOBJ = CMBAITEMNAME Else ITEMOBJ = cmbitemname

            Dim DESIGNOBJ As New Object
            If HIDEALLISSUE = False Then DESIGNOBJ = CMBADESIGN Else DESIGNOBJ = CMBDESIGN

            If sender.Text.Trim = "" Then FILLCOLOR(sender, DESIGNOBJ.Text.Trim, ITEMOBJ.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcolor.Validating, CMBACOLOR.Validating
        Try
            Dim ITEMOBJ As New Object
            If HIDEALLISSUE = False Then ITEMOBJ = CMBAITEMNAME Else ITEMOBJ = cmbitemname

            Dim DESIGNOBJ As New Object
            If HIDEALLISSUE = False Then DESIGNOBJ = CMBADESIGN Else DESIGNOBJ = CMBDESIGN

            If sender.Text.Trim <> "" Then COLORVALIDATE(sender, e, Me, DESIGNOBJ.Text.Trim, ITEMOBJ.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitemname.Enter, CMBAITEMNAME.Enter
        Try
            If sender.Text.Trim = "" Then fillitemname(sender, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbitemname.Validating, CMBAITEMNAME.Validating
        Try
            If sender.Text.Trim <> "" Then itemvalidate(sender, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitemname.Validated, CMBAITEMNAME.Validated
        Try
            If sender.Text.Trim <> "" And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable


                'GET DISPLAY NAME IN GRIDREMARKS
                If ClientName = "RAJKRIPA" Then
                    DT = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS DISPLAYNAME", "", " ITEMMASTER ", " AND ITEMMASTER.ITEM_NAME = '" & sender.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        For Each DTROW As DataRow In DT.Rows
                            txtgridremarks.Text = (DT.Rows(0).Item("DISPLAYNAME"))
                        Next
                    End If
                End If


                Dim WHERECLAUSE As String = ""
                If (ClientName = "YASHVI" Or ClientName = "SOFTAS") Then WHERECLAUSE = " AND ledgers.acc_cmpname = '" & cmbname.Text.Trim & "' "
                If (ClientName = "RAJKRIPA" And CHKPRINTSERIES.CheckState = CheckState.Checked) Or ClientName = "YASHVI" Or ClientName = "SOFTAS" Then
                    DT = OBJCMN.search(" ISNULL(PARTYITEMWISECHART.PAR_STAMPING, '') AS STAMPING", "", " PARTYITEMWISECHART LEFT OUTER JOIN LEDGERS ON PARTYITEMWISECHART.PAR_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON PARTYITEMWISECHART.PAR_ITEMID = ITEMMASTER.item_id ", WHERECLAUSE & " AND ITEMMASTER.ITEM_NAME = '" & sender.Text.Trim & "' AND PARTYITEMWISECHART.PAR_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        For Each DTROW As DataRow In DT.Rows
                            txtgridremarks.Text = (DT.Rows(0).Item("STAMPING"))
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTCUT.GotFocus
        TXTCUT.SelectAll()
    End Sub

    Private Sub TXTCUT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTCUT.Validated, txtqty.Validated, TXTACUT.Validated, TXTAQTY.Validated, TXTRATE.Validated, CMBPER.Validated
        CALC()
    End Sub

    Sub CALC()
        Try
            If Val(txtqty.Text.Trim) > 0 And Val(TXTCUT.Text.Trim) > 0 Then TXTMTRS.Text = Format(Val(txtqty.Text.Trim) * Val(TXTCUT.Text.Trim), "0.00")
            If Val(TXTAQTY.Text.Trim) > 0 And Val(TXTACUT.Text.Trim) > 0 Then TXTAMTRS.Text = Format(Val(TXTAQTY.Text.Trim) * Val(TXTACUT.Text.Trim), "0.00")
            If CMBPER.Text = "Mtrs" Then TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(TXTMTRS.Text.Trim), "0.00") Else TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(txtqty.Text.Trim), "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDESIGN.Enter, CMBADESIGN.Enter
        Try
            Dim ITEMOBJ As New Object
            If HIDEALLISSUE = False Then ITEMOBJ = CMBAITEMNAME Else ITEMOBJ = cmbitemname

            If sender.Text.Trim = "" Then FILLDESIGN(sender, ITEMOBJ.TEXT.TRIM)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGN.Validating, CMBADESIGN.Validating
        Try
            Dim ITEMOBJ As New Object
            If HIDEALLISSUE = False Then ITEMOBJ = CMBAITEMNAME Else ITEMOBJ = cmbitemname

            If sender.Text.Trim <> "" Then
                If ClientName = "AVIS" Then DESIGNVALIDATE(sender, e, Me) Else DESIGNVALIDATE(sender, e, Me, ITEMOBJ.Text.Trim)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbGodown_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbGodown.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then cmbGodown.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbPIECETYPE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPIECETYPE.Enter, CMBAPIECETYPE.Enter
        Try
            If sender.Text.Trim = "" Then fillPIECETYPE(sender)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbPIECETYPE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPIECETYPE.Validating, CMBAPIECETYPE.Validating
        Try
            If sender.Text.Trim <> "" Then PIECETYPEvalidate(sender, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
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

    Private Sub cmbitemname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbitemname.KeyDown, CMBAITEMNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "MERCHANT"
                OBJItem.STRSEARCH = " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then sender.Text = OBJItem.TEMPNAME
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
                Dim OBJQUALITY As New SelectQuality
                OBJQUALITY.ShowDialog()
                If OBJQUALITY.TEMPNAME <> "" Then CMBQUALITY.Text = OBJQUALITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JOBINDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles RECDATE.GotFocus
        RECDATE.SelectionStart = 0
    End Sub

    Private Sub JOBINDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles RECDATE.Validating
        Try
            If RECDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(RECDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CHECKBARCODE() As Boolean
        Try
            Dim BLN As Boolean = True
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" ISNULL(REC_BARCODE,'') AS BARCODE ", "", " RECPACKING_DESC ", " AND RECPACKING_DESC.REC_YEARID =  " & YearId)
            If DT.Rows.Count > 0 Then
                For Each DTR As DataRow In DT.Rows
                    For Each ROW As Windows.Forms.DataGridViewRow In GRIDREC.Rows
                        If ((EDIT = False) And Convert.ToString(DTR("BARCODE")) = ROW.Cells(GBARCODE.Index).Value.ToString) Then
                            BLN = False
                            Exit Function
                        End If
                    Next
                Next
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub CMBRACK_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBRACK.Enter, CMBARACK.Enter
        Try
            If sender.Text.Trim = "" Then FILLRACK(sender)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBRACK_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBRACK.Validating, CMBARACK.Validating
        Try
            If sender.Text.Trim <> "" Then RACKVALIDATE(sender, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHELF_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSHELF.Enter, CMBASHELF.Enter
        Try
            If sender.Text.Trim = "" Then FILLSHELF(sender)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHELF_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSHELF.Validated
        Try
            If CMBPIECETYPE.Text.Trim <> "" And cmbitemname.Text.Trim <> "" And Val(txtqty.Text.Trim) > 0 And cmbqtyunit.Text.Trim <> "" And Val(TXTMTRS.Text.Trim) > 0 Then
                Dim TEMPQTY As Integer = Val(txtqty.Text.Trim)

                'THIS CODE IS DONE BY GULKIT
                'If Val(TXTCUT.Text.Trim) = 0 Then TEMPQTY = 1 Else txtqty.Text = 1
                If Val(TXTCUT.Text.Trim) > 0 Then TXTMTRS.Text = Val(TXTCUT.Text.Trim)
                For I As Integer = 1 To Val(TEMPQTY)
                    If GRIDDOUBLECLICK = False Then
                        If EDIT = True Then
                            'GET LAST BARCODE SRNO
                            Dim LSRNO As Integer = 0
                            Dim RSRNO As Integer = 0
                            Dim SNO As Integer = 0
                            LSRNO = InStr(GRIDREC.Rows(GRIDREC.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                            RSRNO = InStr(LSRNO + 1, GRIDREC.Rows(GRIDREC.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                            SNO = GRIDREC.Rows(GRIDREC.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                            TXTBARCODE.Text = "P-" & Val(TXTRECNO.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                        Else
                            TXTBARCODE.Text = "P-" & Val(TXTRECNO.Text.Trim) & "/" & GRIDREC.RowCount + 1 & "/" & YearId
                        End If
                    End If
                Next
                fillgrid()
            Else
                If ClientName <> "AVIS" And ClientName <> "SOFTAS" Then
                    If CMBPIECETYPE.Text.Trim = "" Then
                        MsgBox("Enter Piece Type", MsgBoxStyle.Critical)
                        CMBPIECETYPE.Focus()
                    ElseIf cmbitemname.Text.Trim = "" Then
                        MsgBox("Enter Item Name", MsgBoxStyle.Critical)
                        cmbitemname.Focus()
                    ElseIf Val(txtqty.Text.Trim) = 0 Then
                        MsgBox("Enter Quantity", MsgBoxStyle.Critical)
                        txtqty.Focus()
                    ElseIf cmbqtyunit.Text.Trim = "" Then
                        MsgBox("Enter Unit", MsgBoxStyle.Critical)
                        cmbqtyunit.Focus()
                    ElseIf Val(TXTMTRS.Text.Trim) = 0 Then
                        MsgBox("Enter Mtrs", MsgBoxStyle.Critical)
                        TXTMTRS.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHELF_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSHELF.Validating, CMBASHELF.Validating
        Try
            If sender.Text.Trim <> "" Then SHELFVALIDATE(sender, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBARCODE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBARCODE.Validated
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("ISSUEPACKING_DESC.ISS_NO AS ISSNO, ISSUEPACKING_DESC.ISS_NO AS FROMNO, ISSUEPACKING_DESC.ISS_GRIDSRNO AS FROMSRNO, 'ISSUEPACKING' AS GRIDTYPE, ISNULL(ISSUEPACKING_DESC.ISS_LOTNO,'') AS LOTNO, ISSUEPACKING_DESC.ISS_MTRS AS ISSUEMTRS, ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISSUEPACKING_DESC.ISS_OUTMTRS,2) AS PENDINGMTRS, ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(ISSUEPACKING_DESC.ISS_RATE,0) AS RATE, ISNULL(ISSUEPACKING.ISS_CHALLANNO,'') AS CHALLANNO ", "", " ISSUEPACKING_DESC INNER JOIN ISSUEPACKING ON ISSUEPACKING_DESC.ISS_NO = ISSUEPACKING.ISS_no AND ISSUEPACKING_DESC.ISS_YEARID = ISSUEPACKING.ISS_yearid LEFT OUTER JOIN LEDGERS ON ISSUEPACKING.ISS_WEAVERID = LEDGERS.Acc_id ", " AND ISSUEPACKING_DESC.ISS_BARCODE='" & CMBBARCODE.Text.Trim & "' AND ROUND(ISSUEPACKING_DESC.ISS_MTRS-ISSUEPACKING_DESC.ISS_OUTMTRS,2) > 0 AND ISSUEPACKING.ISS_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                TXTFROMNO.Text = Val(DT.Rows(0).Item("FROMNO"))
                TXTFROMSRNO.Text = Val(DT.Rows(0).Item("FROMSRNO"))
                TXTFROMTYPE.Text = DT.Rows(0).Item("GRIDTYPE")
                TXTISSUEMTRS.Text = Val(DT.Rows(0).Item("ISSUEMTRS"))
                TXTPENDINGMTRS.Text = Val(DT.Rows(0).Item("PENDINGMTRS"))
                TXTLOTNO.Text = DT.Rows(0).Item("LOTNO")
                If ITEMCOSTCENTRE = True Then TXTRATE.Text = Format(Val(DT.Rows(0).Item("RATE")), "0.00")
                If cmbname.Text.Trim = "" Then cmbname.Text = DT.Rows(0).Item("NAME")
                If ClientName = "RADHA" Then TXTREFNO.Text = DT.Rows(0).Item("CHALLANNO")

                If ClientName <> "AVIS" Or (ClientName = "AVIS" And UserName <> "Admin") Then CMBBARCODE.Enabled = False


                DT.Clear()
                DT = OBJCMN.search("*", "", "OUTBARCODESTOCK", " AND BARCODE = '" & CMBBARCODE.Text.Trim & "' AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBPIECETYPE.Text = DT.Rows(0).Item("PIECETYPE")
                    If ClientName <> "KRISHNA" Then cmbitemname.Text = DT.Rows(0).Item("ITEMNAME")
                    CMBQUALITY.Text = DT.Rows(0).Item("QUALITY")
                    CMBDESIGN.Text = DT.Rows(0).Item("DESIGNNO")
                    cmbcolor.Text = DT.Rows(0).Item("COLOR")
                End If


                If ClientName <> "AVIS" And ClientName <> "MAHAVIRPOLYCOT" And ClientName <> "SAFFRON" Then
                    'FETCH RACK
                    DT.Clear()
                    DT = OBJCMN.SEARCH("ISNULL(RACK,'') AS RACK", "", "ALLBARCODESTOCK", " AND BARCODE = '" & CMBBARCODE.Text.Trim & "' AND YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then CMBRACK.Text = DT.Rows(0).Item("RACK")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtgridremarks_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtgridremarks.Validated
        Try
            'MAKE THIS STAMPING DEFAULT FOR PARTY
            If (ClientName = "YASHVI" Or ClientName = "SOFTAS") And txtgridremarks.Text.Trim <> "" And cmbname.Text.Trim <> "" And cmbitemname.Text.Trim <> "" Then

                'FIRST CHECK WHETHER THIS STAMP FOR THIS PARTY AND ITEM IS PRESENT OR NOT, IF NOT THEN CREATE NEW OR ELSE UPDATE
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("PAR_STAMPING AS STAMPING, PAR_NO AS PARNO", "", "PARTYITEMWISECHART INNER JOIN LEDGERS ON ACC_ID = PAR_LEDGERID INNER JOIN ITEMMASTER ON ITEM_ID = PAR_ITEMID", " AND ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND PAR_YEARID = " & YearId)
                If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STAMPING") <> txtgridremarks.Text.Trim Then
                    If MsgBox("Wish to Make this Stamp Default for this Party & Item?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    DT = OBJCMN.Execute_Any_String("UPDATE PARTYITEMWISECHART SET PAR_STAMPING = '" & txtgridremarks.Text.Trim & "' WHERE PAR_NO = " & Val(DT.Rows(0).Item("PARNO")) & " AND PAR_YEARID = " & YearId, "", "")
                ElseIf DT.Rows.Count = 0 Then
                    'ADD NEW STAMPING
                    Dim ALPARAVAL As New ArrayList
                    Dim OBJCONFIG As New ClsPartyItemWiseChart

                    ALPARAVAL.Add(0)
                    ALPARAVAL.Add(cmbname.Text.Trim)
                    ALPARAVAL.Add(cmbitemname.Text.Trim)
                    ALPARAVAL.Add(0)
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

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        If EDIT = True Then
            PRINTREPORT()
            PRINTBARCODE()
        End If
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            'THEY CAN WRITE THE NAME MANUALLY FOR PRINTING BARCODE PURPOSE
            If ClientName <> "SHREENAKODA" And cmbname.Text.Trim <> "" Then
                If ClientName = "KARAN" Or ClientName = "CC"  Or ClientName = "C3" Or ClientName = "SAKARIA" Then namevalidate(cmbname, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "ACCOUNTS") Else namevalidate(cmbname, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_Validated(sender As Object, e As EventArgs) Handles TXTMTRS.Validated
        Try
            'CALC()  this will give error done by gulkit dont open
            If ClientName = "AVIS" Then CMBSHELF_Validated(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSHORTAGE_Click(sender As Object, e As EventArgs) Handles CMDSHORTAGE.Click
        Try
            If GRIDREC.RowCount > 0 And Val(TXTRUNNINGBAL.Text.Trim) > 0 Then
                If ClientName = "AVIS" Then CMBPIECETYPE.Text = "SHORTAGE"
                cmbitemname.Text = GRIDREC.Rows(GRIDREC.RowCount - 1).Cells(gitemname.Index).Value
                CMBDESIGN.Text = GRIDREC.Rows(GRIDREC.RowCount - 1).Cells(GDESIGN.Index).Value
                cmbcolor.Text = GRIDREC.Rows(GRIDREC.RowCount - 1).Cells(gcolor.Index).Value
                txtqty.Text = 1
                cmbqtyunit.Text = "SHORTAGE"
                TXTMTRS.Text = Val(TXTRUNNINGBAL.Text.Trim)
                CMBSHELF_Validating(sender, e)
                TOTAL()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function CloneWithValues(ByVal row As DataGridViewRow) As DataGridViewRow
        CloneWithValues = CType(row.Clone(), DataGridViewRow)
        For index As Int32 = 0 To row.Cells.Count - 1
            CloneWithValues.Cells(index).Value = row.Cells(index).Value
        Next
    End Function

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then
                If ClientName = "KARAN" Or ClientName = "CC"  Or ClientName = "C3" Or ClientName = "SAKARIA" Then fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'") Else fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors'")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub RecFromPacking_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'If ClientName = "YASHVI" Or ClientName = "KCRAYON" Or ClientName = "SBA" Or ClientName = "AVIS" Or ClientName = "KARAN" Or ClientName = "SMS" Or ClientName = "KRISHNA" Or ClientName = "SONU" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "SHREENAKODA" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "MANSI" Or ClientName = "SSC" Or ClientName = "SST" Or ClientName = "VALIANT" Or ClientName = "MYCOT" Or ClientName = "MANSI" Or ClientName = "INDRAPUJAIMPEX" Then txtqty.ReadOnly = False
            If MANUALRECPACKQTY = True Then
                txtqty.ReadOnly = False
                TXTAQTY.ReadOnly = False
            End If

            If ClientName = "AFW" Then
                CMBADESIGN.TabStop = False
            End If



            If ClientName = "SURYODAYA" Or ClientName = "SARAYU" Or ClientName = "AFW" Then TXTAQTY.ReadOnly = False
            If ClientName = "YASHVI" Then
                cmbname.TabStop = False
                TXTREFNO.TabStop = False
                CMBPIECETYPE.TabStop = False
                cmbitemname.TabStop = False
                CMBQUALITY.TabStop = False
                CMBDESIGN.TabStop = False
                txtgridremarks.TabStop = False
                cmbcolor.TabStop = False
            End If

            If ClientName = "KCRAYON" Then
                cmbname.TabStop = False
                TXTREFNO.TabIndex = False
            End If

            If ClientName = "MNIKHIL" Or ClientName = "HRITI" Then
                cmbname.TabStop = False
                TXTREFNO.TabStop = False
                CMBQUALITY.TabStop = False
                txtgridremarks.TabStop = False
                TXTCUT.TabStop = False
                CMBRACK.TabStop = False
            End If

            If ClientName = "AVIS" Or ClientName = "KRISHNA" Then
                LBLPARTYNAME.Visible = False
                cmbname.Visible = False
                cmbitemname.TabStop = False
                CMBQUALITY.TabStop = False
                txtgridremarks.TabStop = False
                txtqty.ReadOnly = False
                cmbqtyunit.Text = "LUMP"
                CMBRACK.TabStop = False
                CMDSHORTAGE.Visible = True
                If ClientName <> "KRISHNA" Then CMBPIECETYPE.TabStop = False
                cmbGodown.TabStop = False
                If UserName = "Admin" Then CMBBARCODE.Enabled = True
            End If
            If ITEMCOSTCENTRE = True And UserName = "Admin" Then CHKMANUALRATE.Visible = True
            If ClientName = "DJIMPEX" Then TXTYARDS.Visible = True



            If ClientName = "PARAS" Then
                LBLLONGATION.Visible = True
                TXTLONGATIONPER.Visible = True
            End If

            If ClientName = "SUPRIYA" Or ClientName = "SHREENAKODA" Or ClientName = "SOFTAS" Then
                TXTFROMNO.ReadOnly = False
                TXTFROMSRNO.ReadOnly = False
                TXTFROMNO.TabStop = True
                TXTFROMSRNO.TabStop = True
                txtqty.ReadOnly = False
                CMBQUALITY.TabStop = False
                If ClientName = "SOFTAS" Then
                    CHKPRINTSERIES.Visible = True
                    If CmpName = "SIDDHIM COTFAB LLP" Then CHKPRINTSERIES.CheckState = True
                    TXTFROMNO.Focus()
                End If
                TXTRATE.TabStop = False
                CMBPER.TabStop = False
            End If

            If ClientName = "REALCORPORATION" Then gdesc.HeaderText = "Bale No"

            If ClientName = "RAJKRIPA" Then
                CHKPRINTSERIES.Visible = True
                CHKPRINTSERIES.Text = "Fetch Description"
            End If
            If ClientName = "MYCOT" Then cmbname.TabStop = True

            If ClientName = "SUPEEMA" Or ClientName = "SURYODAYA" Or ClientName = "SARAYU" Or ClientName = "AFW" Then HIDEALLISSUE = False
            HIDEVIEW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub HIDEVIEW()
        Try
            If HIDEALLISSUE = True Then
                TabControl1.Visible = True
                TBALL.Visible = False
            Else
                TabControl1.Visible = False
                TBALL.Visible = True
                LBLBARCODE.Visible = False
                CMBBARCODE.Visible = False
                LBLREFNO.Visible = False
                TXTREFNO.Visible = False

                TXTFROMNO.ReadOnly = False
                TXTFROMNO.TabStop = True
                LBLF11.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validated(sender As Object, e As EventArgs) Handles CMBDESIGN.Validated, CMBADESIGN.Validated
        Try
            Dim ITEMOBJ As New Object
            If HIDEALLISSUE = False Then ITEMOBJ = CMBAITEMNAME Else ITEMOBJ = cmbitemname

            'GET ITEMNAME AUTO
            If (ClientName = "AVIS" Or ClientName = "KRISHNA" Or ClientName = "NTC") And sender.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(ITEM_NAME,'') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEM_ID", " AND DESIGN_NO = '" & sender.Text.Trim & "' AND DESIGN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then ITEMOBJ.Text = DT.Rows(0).Item("ITEMNAME")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Validated(sender As Object, e As EventArgs) Handles cmbqtyunit.Validated, CMBAQTYUNIT.Validated
        Try
            Dim PIECETYPEOBJ As New Object
            If HIDEALLISSUE = False Then PIECETYPEOBJ = CMBAPIECETYPE Else PIECETYPEOBJ = CMBPIECETYPE


            If ClientName = "AVIS" Then
                If UCase(sender.Text.Trim) = "FENT" Then
                    PIECETYPEOBJ.Text = "FENT"
                ElseIf UCase(sender.Text.Trim) = "2ND" Then
                    PIECETYPEOBJ.Text = "SECOND"
                ElseIf UCase(sender.Text.Trim) = "2ND TP" Then
                    PIECETYPEOBJ.Text = "SECOND"
                ElseIf UCase(sender.Text.Trim) = "SHORTAGE" Then
                    PIECETYPEOBJ.Text = "SHORTAGE"
                ElseIf UCase(sender.Text.Trim) = "TP" Then
                    PIECETYPEOBJ.Text = "TWOPART"
                ElseIf UCase(sender.Text.Trim) = "PCS" Then
                    PIECETYPEOBJ.Text = "PIECES"
                Else
                    PIECETYPEOBJ.Text = "FRESH"
                End If
            End If
            If ClientName = "DJIMPEX" Then TXTYARDS.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTYARDS_Validated(sender As Object, e As EventArgs) Handles TXTYARDS.Validated
        Try
            If Val(TXTYARDS.Text.Trim) > 0 Then TXTMTRS.Text = Format(Val(TXTYARDS.Text.Trim) * 0.914, "0.00")
            TXTMTRS.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstxtbillno.KeyPress, TXTFROM.KeyPress, TXTTO.KeyPress, txtqty.KeyPress, TXTFROMNO.KeyPress, TXTFROMSRNO.KeyPress, TXTRECNO.KeyPress, TXTAQTY.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTMTRS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTCUT.KeyPress, TXTACUT.KeyPress, TXTAMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTFROMSRNO_Validated(sender As Object, e As EventArgs) Handles TXTFROMSRNO.Validated
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("ISSUEPACKING_DESC.ISS_NO AS FROMNO, ISSUEPACKING_DESC.ISS_GRIDSRNO AS FROMSRNO, ISSUEPACKING_DESC.ISS_BARCODE AS BARCODE, 'ISSUEPACKING' AS GRIDTYPE, ISNULL(ISSUEPACKING_DESC.ISS_LOTNO,'') AS LOTNO, ISSUEPACKING_DESC.ISS_MTRS AS ISSUEMTRS, ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISSUEPACKING_DESC.ISS_OUTMTRS,2) AS PENDINGMTRS, ISNULL(LEDGERS.ACC_CMPNAME,'') AS NAME, ISNULL(ISSUEPACKING_DESC.ISS_RATE,0) AS RATE", "", " ISSUEPACKING_DESC INNER JOIN ISSUEPACKING ON ISSUEPACKING_DESC.ISS_NO = ISSUEPACKING.ISS_no AND ISSUEPACKING_DESC.ISS_YEARID = ISSUEPACKING.ISS_yearid LEFT OUTER JOIN LEDGERS ON ISSUEPACKING.ISS_WEAVERID = LEDGERS.Acc_id ", " AND ISSUEPACKING.ISS_NO=" & Val(TXTFROMNO.Text.Trim) & " AND ISSUEPACKING_DESC.ISS_GRIDSRNO = " & Val(TXTFROMSRNO.Text.Trim) & " AND ROUND(ISSUEPACKING_DESC.ISS_MTRS - ISSUEPACKING_DESC.ISS_OUTMTRS,2) > 0 AND ISSUEPACKING.ISS_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                CMBBARCODE.Text = DT.Rows(0).Item("BARCODE")
                TXTFROMTYPE.Text = DT.Rows(0).Item("GRIDTYPE")
                TXTISSUEMTRS.Text = Val(DT.Rows(0).Item("ISSUEMTRS"))
                TXTPENDINGMTRS.Text = Val(DT.Rows(0).Item("PENDINGMTRS"))
                TXTLOTNO.Text = DT.Rows(0).Item("LOTNO")
                If ITEMCOSTCENTRE = True Then TXTRATE.Text = Format(Val(DT.Rows(0).Item("RATE")), "0.00")
                If cmbname.Text.Trim = "" Then cmbname.Text = DT.Rows(0).Item("NAME")

                CMBBARCODE.Enabled = False
                TXTFROMNO.ReadOnly = True
                TXTFROMSRNO.ReadOnly = True

                DT.Clear()
                DT = OBJCMN.search("*", "", "OUTBARCODESTOCK", " AND BARCODE = '" & CMBBARCODE.Text.Trim & "' AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBPIECETYPE.Text = DT.Rows(0).Item("PIECETYPE")
                    cmbitemname.Text = DT.Rows(0).Item("ITEMNAME")
                    CMBQUALITY.Text = DT.Rows(0).Item("QUALITY")
                    CMBDESIGN.Text = DT.Rows(0).Item("DESIGNNO")
                    cmbcolor.Text = DT.Rows(0).Item("COLOR")
                End If

                'FETCH RACK
                DT.Clear()
                DT = OBJCMN.search("ISNULL(RACK,'') AS RACK", "", "ALLBARCODESTOCK", " AND BARCODE = '" & CMBBARCODE.Text.Trim & "' AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then CMBRACK.Text = DT.Rows(0).Item("RACK")

                cmbitemname.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROMNO_Validated(sender As Object, e As EventArgs) Handles TXTFROMNO.Validated
        Try
            If HIDEALLISSUE = False And Val(TXTFROMNO.Text.Trim) > 0 Then

                'CHECK WHETHER IT IS LOCKED OR NOT, IF LOCKED THEN DONT FETCH NEW ENTRY AGAIN, GIVE MESSAGE (ONLY FOR FRESH 
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.Execute_Any_String(" SELECT ISS_NO FROM ISSUEPACKING_DESC WHERE ISS_OUTMTRS > 0 AND ISS_NO = " & Val(TXTFROMNO.Text.Trim) & " AND ISS_YEARID = " & YearId, "", "")
                If DT.Rows.Count > 0 And EDIT = False Then
                    MsgBox("Entry for this Card No is Already Done", MsgBoxStyle.Critical)
                    TXTFROMNO.Clear()
                    Exit Sub
                End If

                'GET ALL ENTRIES FROM ISSUE TO PACK
                GRIDISSUE.RowCount = 0
                Dim OBJISSUE As New ClsIssueToPacking()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(Val(TXTFROMNO.Text.Trim))
                ALPARAVAL.Add(YearId)
                OBJISSUE.alParaval = ALPARAVAL
                Dim dttable As DataTable = OBJISSUE.SELECTJO()
                For Each dr As DataRow In dttable.Rows
                    'If Val(dr("OUTMTRS")) > 0 Then
                    '    MsgBox("Entry Locked", MsgBoxStyle.Critical)
                    '    clear()
                    '    Exit Sub
                    'End If
                    GRIDISSUE.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE").ToString, dr("BALENO").ToString, dr("ITEM").ToString, dr("LOTNO").ToString, dr("QUALITY").ToString, dr("DESIGN").ToString, dr("COLOR").ToString, Format(dr("PCS"), "0.00"), dr("UNIT"), Format(dr("MTRS"), "0.00"), dr("BARCODE").ToString)
                Next
                GRIDISSUE.ClearSelection()
                'GRIDALLREC.Visible = True
                'GRIDALLREC.BringToFront()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDISSUE_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDISSUE.KeyDown
        Try
            If e.KeyCode = Keys.F11 And GRIDISSUE.CurrentRow.Cells(ISRNO.Index).Value <> "" Then
                GPALL.Enabled = True
                CMBAPIECETYPE.Text = GRIDISSUE.CurrentRow.Cells(IPIECETYPE.Index).Value
                CMBAITEMNAME.Text = GRIDISSUE.CurrentRow.Cells(IITEMNAME.Index).Value
                CMBADESIGN.Text = GRIDISSUE.CurrentRow.Cells(IDESIGNNO.Index).Value
                CMBACOLOR.Text = GRIDISSUE.CurrentRow.Cells(ISHADE.Index).Value

                TXTISSUEMTRS.Text = Val(GRIDISSUE.CurrentRow.Cells(IMTRS.Index).Value)
                TXTPENDINGMTRS.Text = Val(GRIDISSUE.CurrentRow.Cells(IMTRS.Index).Value)
                TXTFROMSRNO.Text = GRIDISSUE.CurrentRow.Cells(ISRNO.Index).Value
                TXTFROMTYPE.Text = "ISSUEPACKING"

                GRIDISSUE.Enabled = False

                CMBAITEMNAME.Focus()
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBASHELF_Validated(sender As Object, e As EventArgs) Handles CMBASHELF.Validated
        Try
            If CMBAPIECETYPE.Text.Trim <> "" And CMBAITEMNAME.Text.Trim <> "" And Val(TXTAQTY.Text.Trim) > 0 And CMBAQTYUNIT.Text.Trim <> "" And Val(TXTAMTRS.Text.Trim) > 0 Then

                Dim TEMPQTY As Integer = Val(TXTAQTY.Text.Trim)

                'THIS CODE IS DONE BY GULKIT
                'If Val(TXTCUT.Text.Trim) = 0 Then TEMPQTY = 1 Else txtqty.Text = 1
                If Val(TXTACUT.Text.Trim) > 0 Then TXTAMTRS.Text = Val(TXTACUT.Text.Trim)
                For I As Integer = 1 To Val(TEMPQTY)
                    If GRIDDOUBLECLICK = False Then
                        If EDIT = True Then

                            'FIRST SORT THE GRID WITH RESPECT TO BARCODE NO
                            GRIDALLREC.Sort(GRIDALLREC.Columns(ALLMAINSRNO.Index), System.ComponentModel.ListSortDirection.Ascending)

                            'GET LAST BARCODE SRNO
                            Dim LSRNO As Integer = 0
                            Dim RSRNO As Integer = 0
                            Dim SNO As Integer = 0
                            LSRNO = InStr(GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLBARCODE.Index).Value, "/")
                            RSRNO = InStr(LSRNO + 1, GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLBARCODE.Index).Value, "/")
                            SNO = GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                            TXTBARCODE.Text = "P-" & Val(TXTRECNO.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                        Else
                            If GRIDALLREC.RowCount > 0 Then TXTBARCODE.Text = "P-" & Val(TXTRECNO.Text.Trim) & "/" & Val(GRIDALLREC.Rows(GRIDALLREC.RowCount - 1).Cells(ALLMAINSRNO.Index).Value) + 1 & "/" & YearId Else TXTBARCODE.Text = "P-" & Val(TXTRECNO.Text.Trim) & "/" & Val(GRIDAREC.RowCount) + 1 & "/" & YearId
                        End If
                    End If
                Next

                FILLAGRID()
            Else
                If ClientName <> "AVIS" And ClientName <> "SOFTAS" Then
                    If CMBAPIECETYPE.Text.Trim = "" Then
                        MsgBox("Enter Piece Type", MsgBoxStyle.Critical)
                        CMBAPIECETYPE.Focus()
                    ElseIf CMBAITEMNAME.Text.Trim = "" Then
                        MsgBox("Enter Item Name", MsgBoxStyle.Critical)
                        CMBAITEMNAME.Focus()
                    ElseIf Val(TXTAQTY.Text.Trim) = 0 Then
                        MsgBox("Enter Quantity", MsgBoxStyle.Critical)
                        TXTAQTY.Focus()
                    ElseIf CMBAQTYUNIT.Text.Trim = "" Then
                        MsgBox("Enter Unit", MsgBoxStyle.Critical)
                        CMBAQTYUNIT.Focus()
                    ElseIf Val(TXTAMTRS.Text.Trim) = 0 Then
                        MsgBox("Enter Mtrs", MsgBoxStyle.Critical)
                        TXTAMTRS.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDAREC_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDAREC.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then

                'if LINE IS IN EDIT MODE (GRIDDESCDOUBLECLICK = TRUE) THEN DONT ALLOW TO DELETE
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                'REMOVING ROWS FROM GRIDALLREC
1:
                For Each ROW As DataGridViewRow In GRIDALLREC.Rows
                    If ROW.Cells(ALLFROMSRNO.Index).Value = GRIDISSUE.CurrentRow.Cells(ISRNO.Index).Value Then
                        GRIDALLREC.Rows.RemoveAt(ROW.Index)
                        GoTo 1
                    End If
                Next

                GRIDAREC.Rows.RemoveAt(GRIDAREC.CurrentRow.Index)
                TOTAL()
                getsrno(GRIDAREC)
                CMBAITEMNAME.Focus()

                'AGAIN INSERT THE COMPLETE GRIDAREC IN GRIDALLREC
                For Each ROW As DataGridViewRow In GRIDAREC.Rows
                    GRIDALLREC.Rows.Add(ROW.Cells(ASRNO.Index).Value, ROW.Cells(APIECETYPE.Index).Value, ROW.Cells(AITEMNAME.Index).Value, "", ROW.Cells(ADESIGN.Index).Value, ROW.Cells(ADESC.Index).Value, ROW.Cells(ACOLOR.Index).Value, Format(Val(ROW.Cells(ACUT.Index).Value), "0.00"), Format(Val(ROW.Cells(AQTY.Index).Value), "0.00"), ROW.Cells(AQTYUNIT.Index).Value, Format(Val(ROW.Cells(AMTRS.Index).Value), "0.00"), ROW.Cells(ARACK.Index).Value, ROW.Cells(ASHELF.Index).Value, ROW.Cells(ABARCODE.Index).Value, ROW.Cells(ADONE.Index).Value, Val(ROW.Cells(AOUTPCS.Index).Value), Val(ROW.Cells(AOUTMTRS.Index).Value), Val(ROW.Cells(AFROMNO.Index).Value), Val(ROW.Cells(AFROMSRNO.Index).Value), ROW.Cells(AFROMTYPE.Index).Value, Val(ROW.Cells(AMAINSRNO.Index).Value))
                Next

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRECNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTRECNO.Validating
        Try
            If Val(TXTRECNO.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(RECPACKING.REC_NO,0)  AS RECNO", "", " RECPACKING ", "  AND RECPACKING.REC_NO=" & Val(TXTRECNO.Text.Trim) & " AND RECPACKING.REC_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Rec No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDISSUE_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDISSUE.RowEnter
        Try
            GRIDAREC.RowCount = 0
            'SHOW DATA IN GRIDAREC WITH RESPECT TO ISSUE SRNO
            For Each ROW As DataGridViewRow In GRIDALLREC.Rows
                If Val(ROW.Cells(ALLFROMSRNO.Index).Value) = Val(GRIDISSUE.Rows(e.RowIndex).Cells(ISRNO.Index).Value) Then
                    GRIDAREC.Rows.Add(ROW.Cells(ALLSRNO.Index).Value, ROW.Cells(ALLPIECETYPE.Index).Value, ROW.Cells(ALLITEMNAME.Index).Value, "", ROW.Cells(ALLDESIGN.Index).Value, ROW.Cells(ALLDESC.Index).Value, ROW.Cells(ALLCOLOR.Index).Value, Format(Val(ROW.Cells(ALLCUT.Index).Value), "0.00"), Format(Val(ROW.Cells(ALLQTY.Index).Value), "0.00"), ROW.Cells(ALLQTYUNIT.Index).Value, Format(Val(ROW.Cells(ALLMTRS.Index).Value), "0.00"), ROW.Cells(ALLRACK.Index).Value, ROW.Cells(ALLSHELF.Index).Value, ROW.Cells(ALLBARCODE.Index).Value, ROW.Cells(ALLDONE.Index).Value, Val(ROW.Cells(ALLOUTPCS.Index).Value), Val(ROW.Cells(ALLOUTMTRS.Index).Value), Val(ROW.Cells(ALLFROMNO.Index).Value), Val(ROW.Cells(ALLFROMSRNO.Index).Value), ROW.Cells(ALLFROMTYPE.Index).Value, Val(ROW.Cells(ALLMAINSRNO.Index).Value))

                    If Val(ROW.Cells(ALLOUTMTRS.Index).Value) > 0 Then
                        GRIDAREC.Rows(GRIDAREC.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If
                End If

            Next
            getsrno(GRIDAREC)
            TXTISSUEMTRS.Text = Val(GRIDISSUE.Rows(e.RowIndex).Cells(IMTRS.Index).Value)
            TXTPENDINGMTRS.Text = Val(GRIDISSUE.Rows(e.RowIndex).Cells(IMTRS.Index).Value)
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDAREC_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDAREC.CellDoubleClick
        Try
            If GRIDAREC.CurrentRow.Index >= 0 And GRIDAREC.Item(ASRNO.Index, GRIDAREC.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTASRNO.Text = GRIDAREC.Item(ASRNO.Index, GRIDAREC.CurrentRow.Index).Value.ToString
                CMBAPIECETYPE.Text = GRIDAREC.Item(APIECETYPE.Index, GRIDAREC.CurrentRow.Index).Value.ToString
                CMBAITEMNAME.Text = GRIDAREC.Item(AITEMNAME.Index, GRIDAREC.CurrentRow.Index).Value.ToString
                CMBADESIGN.Text = GRIDAREC.Item(ADESIGN.Index, GRIDAREC.CurrentRow.Index).Value.ToString
                TXTAGRIDREMARKS.Text = GRIDAREC.Item(ADESC.Index, GRIDAREC.CurrentRow.Index).Value.ToString
                CMBACOLOR.Text = GRIDAREC.Item(ACOLOR.Index, GRIDAREC.CurrentRow.Index).Value.ToString
                TXTACUT.Text = GRIDAREC.Item(ACUT.Index, GRIDAREC.CurrentRow.Index).Value.ToString
                TXTAQTY.Text = GRIDAREC.Item(AQTY.Index, GRIDAREC.CurrentRow.Index).Value.ToString
                CMBAQTYUNIT.Text = GRIDAREC.Item(AQTYUNIT.Index, GRIDAREC.CurrentRow.Index).Value.ToString
                TXTAMTRS.Text = GRIDAREC.Item(AMTRS.Index, GRIDAREC.CurrentRow.Index).Value.ToString
                CMBARACK.Text = GRIDAREC.Item(ARACK.Index, GRIDAREC.CurrentRow.Index).Value.ToString
                CMBASHELF.Text = GRIDAREC.Item(ASHELF.Index, GRIDAREC.CurrentRow.Index).Value.ToString
                TEMPROW = GRIDAREC.CurrentRow.Index
                CMBAITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtgridremarks_KeyDown(sender As Object, e As KeyEventArgs) Handles txtgridremarks.KeyDown
        Try
            If ClientName = "MANSI" Then
                If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
                If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

                If e.KeyCode = Keys.F1 Then
                    Dim OBJREMARKS As New SelectRemarks
                    OBJREMARKS.FRMSTRING = "NARRATION"
                    OBJREMARKS.ShowDialog()
                    If OBJREMARKS.TEMPNAME <> "" Then txtgridremarks.Text = OBJREMARKS.TEMPNAME
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKMANUALRATE_CheckedChanged(sender As Object, e As EventArgs) Handles CHKMANUALRATE.CheckedChanged
        Try
            If UserName <> "Admin" Then Exit Sub
            TXTRATE.ReadOnly = Not CHKMANUALRATE.Checked
            GRATE.ReadOnly = Not CHKMANUALRATE.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class