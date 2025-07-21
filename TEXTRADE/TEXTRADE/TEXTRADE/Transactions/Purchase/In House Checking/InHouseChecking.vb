
Imports BL
Imports System.IO
Imports System.Speech.Recognition

Public Class InHouseChecking

    Dim GRIDDOUBLECLICK As Boolean
    Public EDIT As Boolean          'used for editing
    Public TEMPCHECKINGNO As Integer     'used for poation no while editing
    Dim TEMPROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        fillcmb()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            EP.Clear()
            If ClientName = "VINTAGEINDIA" Then TXTCHECKEDBY.Text = UserName Else TXTCHECKEDBY.Clear()
            CMDSELECTLOT.Enabled = True

            tstxtbillno.Clear()
            TXTFROM.Clear()
            TXTTO.Clear()
            GRIDCHECKING.RowCount = 0

            TXTCHECKINGNO.Clear()
            CHECKINGDATE.Text = Now.Date
            TXTNAME.Clear()
            TXTGODOWN.Clear()
            TXTWEAVERNAME.Clear()
            TXTWEAVERCHNO.Clear()
            TXTLOTNO.Clear()
            TXTMATRECNO.Clear()
            TXTTYPE.Clear()

            TXTTOTALGREYMTRS.Text = 0.0
            TXTTOTALRECDMTRS.Text = 0.0
            TXTTOTALCHECKEDMTRS.Text = 0.0
            LBLSHORTAGE.Text = "Shortage"
            TXTTOTALDIFF.Text = 0.0
            TXTTOTALPCS.Text = 0
            TXTTOTALCHECKEDPCS.Text = 0
            TXTTOTALRECDWT.Text = 0.0
            TXTTOTALWT.Text = 0.0
            TXTTOTALWTDIFF.Text = 0.0
            TXTSHRINKAGEPER.Text = 0.0
            TXTAVGWT.Text = 0.0
            TXTTOTALAMT.Text = 0.0
            txtremarks.Clear()

            lbllocked.Visible = False
            PBlock.Visible = False
            TXTLRNO.Clear()
            TXTWIDTH.Clear()
            TXTDECLPCS.Clear()
            TXTDECLMTRS.Clear()

            Gdesc.ReadOnly = False
            CMDSELECTLOT.Enabled = True
            GRIDDOUBLECLICK = False
            getmaxno()
            If ClientName <> "SHASHWAT" Then CHKCHECKMTRS.CheckState = CheckState.Unchecked
            CHALLANDATE.Text = Now.Date
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            TXTTOTALGREYMTRS.Text = 0.0
            TXTTOTALRECDMTRS.Text = 0.0
            TXTTOTALCHECKEDMTRS.Text = 0.0
            TXTTOTALDIFF.Text = 0.0
            TXTTOTALPCS.Text = 0
            TXTTOTALCHECKEDPCS.Text = 0
            TXTTOTALRECDWT.Text = 0.0
            TXTTOTALWT.Text = 0.0
            TXTTOTALWTDIFF.Text = 0.0
            TXTAVGWT.Text = 0.0
            TXTSHRINKAGEPER.Text = 0.0
            TXTTOTALAMT.Text = 0.0

            Dim TEMPWTMTRS As Double = 0.0

            For Each ROW As DataGridViewRow In GRIDCHECKING.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    ROW.Cells(GDIFF.Index).Value = Format(Val(ROW.Cells(GRECDMTRS.Index).EditedFormattedValue) - Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    ROW.Cells(GWTDIFF.Index).Value = Format(Val(ROW.Cells(GRECDWT.Index).EditedFormattedValue) - Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.00")


                    'IF WTDIFF PERCENT IS GREATER THEN GIVEN WTPERCENT THEN MARK THE LINE IN GREEN COLOR
                    If Val(ROW.Cells(GWTPER.Index).EditedFormattedValue) > 0 And Val(ROW.Cells(GWTDIFF.Index).EditedFormattedValue) <> 0 And Val(ROW.Cells(GRECDWT.Index).EditedFormattedValue) > 0 And Val(ROW.Cells(GWT.Index).EditedFormattedValue) > 0 AndAlso (Val(ROW.Cells(GWTDIFF.Index).EditedFormattedValue) / Val(ROW.Cells(GRECDWT.Index).EditedFormattedValue) * 100) > Val(ROW.Cells(GWTPER.Index).EditedFormattedValue) Then ROW.DefaultCellStyle.BackColor = Color.LightGreen

                    If Val(ROW.Cells(GWT.Index).EditedFormattedValue) > 0 Then TEMPWTMTRS = Val(TEMPWTMTRS) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue)


                    TXTTOTALGREYMTRS.Text = Format(Val(TXTTOTALGREYMTRS.Text) + Val(ROW.Cells(GGREYMTRS.Index).EditedFormattedValue), "0.00")
                    TXTTOTALRECDMTRS.Text = Format(Val(TXTTOTALRECDMTRS.Text) + Val(ROW.Cells(GRECDMTRS.Index).EditedFormattedValue), "0.00")
                    TXTTOTALCHECKEDMTRS.Text = Format(Val(TXTTOTALCHECKEDMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    TXTTOTALDIFF.Text = Format(Val(TXTTOTALDIFF.Text) + Val(ROW.Cells(GDIFF.Index).EditedFormattedValue), "0.00")

                    If Val(ROW.Cells(GRECDMTRS.Index).EditedFormattedValue) > 0 Then TXTTOTALPCS.Text = Val(TXTTOTALPCS.Text.Trim) + 1

                    TXTTOTALRECDWT.Text = Format(Val(TXTTOTALRECDWT.Text) + Val(ROW.Cells(GRECDWT.Index).EditedFormattedValue), "0.00")
                    TXTTOTALWT.Text = Format(Val(TXTTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.00")
                    TXTTOTALWTDIFF.Text = Format(Val(TXTTOTALWTDIFF.Text) + Val(ROW.Cells(GWTDIFF.Index).EditedFormattedValue), "0.00")
                    TXTTOTALAMT.Text = Format(Val(TXTTOTALAMT.Text) + Val(ROW.Cells(GAMOUNT.Index).EditedFormattedValue), "0.00")
                End If
            Next
            TXTTOTALCHECKEDPCS.Text = Val(GRIDCHECKING.RowCount)
            TXTAVGWT.Text = Format(Val(TXTTOTALWT.Text.Trim) / Val(TEMPWTMTRS), "0.000")

            If Val(TXTTOTALDIFF.Text.Trim) < 0 Then LBLSHORTAGE.Text = "Longation" Else LBLSHORTAGE.Text = "Shortage"
            If Val(TXTTOTALGREYMTRS.Text.Trim) > 0 Then TXTSHRINKAGEPER.Text = Format(((Val(TXTTOTALGREYMTRS.Text.Trim) - Val(TXTTOTALCHECKEDMTRS.Text.Trim)) / Val(TXTTOTALGREYMTRS.Text.Trim)) * 100, "0.00") Else TXTSHRINKAGEPER.Text = Format((Val(TXTTOTALDIFF.Text.Trim) / Val(TXTTOTALRECDMTRS.Text.Trim)) * 100, "0.00")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Try
            CLEAR()
            EDIT = False
            CHECKINGDATE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHECKINGDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHECKINGDATE.GotFocus
        CHECKINGDATE.SelectionStart = 0
    End Sub
    Private Sub CHALLANDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHALLANDATE.GotFocus
        CHALLANDATE.SelectionStart = 0
    End Sub

    Private Sub CHECKINGDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CHECKINGDATE.Validating
        Try
            If CHECKINGDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(CHECKINGDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getmaxno()
        Try
            Dim DTTABLE As New DataTable
            DTTABLE = getmax(" isnull(max(CHECK_NO),0) + 1 ", " INHOUSECHECKING", " AND CHECK_yearid=" & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTCHECKINGNO.Text = DTTABLE.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If TXTNAME.Text.Trim.Length = 0 Then
                EP.SetError(TXTNAME, " Please Select Data ")
                bln = False
            End If

            'If TXTLOTNO.Text.Trim.Length = 0 Then
            '    EP.SetError(TXTLOTNO, "Please Select Lot No")
            '    bln = False
            'End If

            If TXTCHECKEDBY.Text.Trim.Length = 0 Then
                EP.SetError(TXTCHECKEDBY, "Please Enter Checked By")
                bln = False
            End If


            'WE DONT HAVE TO LOCK FULL ENTRY, WE WILL LOCK ONLY OUTMTRS ENTRY
            'If lbllocked.Visible = True Then
            '    EP.SetError(lbllocked, "Item Used in Mfg, Delete Mfg First")
            '    bln = False
            'End If

            If GRIDCHECKING.RowCount = 0 Then
                EP.SetError(TXTLOTNO, "Fill Item Details")
                bln = False
            End If



            For Each row As DataGridViewRow In GRIDCHECKING.Rows
                If Val(row.Cells(GMTRS.Index).Value) = 0 And ClientName <> "PARAS" And ClientName <> "RAJKRIPA" And ClientName <> "SNCM" Then
                    EP.SetError(TXTLOTNO, "Recd Mtrs Cannot be kept Blank")
                    bln = False
                End If
            Next





            If CHECKINGDATE.Text = "__/__/____" Then
                EP.SetError(CHECKINGDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(CHECKINGDATE.Text) Then
                    EP.SetError(CHECKINGDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If


            'check WHETHER SAME LOT NO AND NAME IS SAVED BEFORE OR NOT
            If EDIT = False And TXTLOTNO.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("CHECK_NO AS CHECKNO", "", " INHOUSECHECKING INNER JOIN LEDGERS ON CHECK_LEDGERID = LEDGERS.ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & TXTNAME.Text.Trim & "' AND CHECK_LOTNO = '" & TXTLOTNO.Text.Trim & "'  AND CHECK_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If MsgBox("Lot No already saved before, wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        EP.SetError(TXTNAME, "Lot No already saved before")
                        bln = False
                    End If
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
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            alParaval.Add(Format(Convert.ToDateTime(CHECKINGDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(TXTNAME.Text.Trim)
            alParaval.Add(TXTGODOWN.Text.Trim)
            alParaval.Add(TXTWEAVERNAME.Text.Trim)
            alParaval.Add(TXTWEAVERCHNO.Text.Trim)
            alParaval.Add(TXTLOTNO.Text.Trim)
            alParaval.Add(Val(TXTMATRECNO.Text.Trim))
            alParaval.Add(TXTTYPE.Text.Trim)
            alParaval.Add(TXTCHECKEDBY.Text.Trim)


            alParaval.Add(Val(TXTTOTALGREYMTRS.Text))
            alParaval.Add(Val(TXTTOTALRECDMTRS.Text))
            alParaval.Add(Val(TXTTOTALCHECKEDMTRS.Text.Trim))
            alParaval.Add(Val(TXTTOTALDIFF.Text))
            alParaval.Add(Val(TXTSHRINKAGEPER.Text.Trim))
            alParaval.Add(Val(TXTTOTALPCS.Text))
            alParaval.Add(Val(TXTTOTALCHECKEDPCS.Text))
            alParaval.Add(Val(TXTTOTALRECDWT.Text))
            alParaval.Add(Val(TXTTOTALWT.Text))
            alParaval.Add(Val(TXTTOTALWTDIFF.Text))
            alParaval.Add(Val(TXTTOTALAMT.Text.Trim))
            alParaval.Add(Val(TXTAVGWT.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim GRIDSRNO As String = ""
            Dim GREYMTRS As String = ""
            Dim RECDMTRS As String = ""
            Dim CHECKEDMTRS As String = ""
            Dim GRIDREMARKS As String = ""
            Dim PIECETYPE As String = ""
            Dim DIFF As String = ""
            Dim UNIT As String = ""
            Dim CHKWIDTH As String = ""
            Dim RECDWT As String = ""
            Dim WT As String = ""
            Dim WTDIFF As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim BALENO As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim BARCODE As String = ""
            Dim RACK As String = ""
            Dim SHELF As String = ""
            Dim DONE As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""
            Dim RATE As String = ""         'value of RATE
            Dim PER As String = ""
            Dim AMT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCHECKING.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(row.Cells(gsrno.Index).Value)
                        GREYMTRS = Val(row.Cells(GGREYMTRS.Index).Value)
                        RECDMTRS = Val(row.Cells(GRECDMTRS.Index).Value)
                        CHECKEDMTRS = Val(row.Cells(GMTRS.Index).Value)
                        If row.Cells(Gdesc.Index).Value <> Nothing Then GRIDREMARKS = row.Cells(Gdesc.Index).Value.ToString Else GRIDREMARKS = ""
                        PIECETYPE = row.Cells(GPIECETYPE.Index).Value.ToString
                        DIFF = Val(row.Cells(GDIFF.Index).Value)
                        UNIT = row.Cells(GUNIT.Index).Value
                        CHKWIDTH = Val(row.Cells(GCHKWIDTH.Index).Value)
                        RECDWT = Val(row.Cells(GRECDWT.Index).Value)
                        WT = Val(row.Cells(GWT.Index).Value)
                        WTDIFF = Val(row.Cells(GWTDIFF.Index).Value)
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        BALENO = row.Cells(GBALENO.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(GCOLOR.Index).Value.ToString
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        RACK = row.Cells(GRACK.Index).Value.ToString
                        SHELF = row.Cells(GSHELF.Index).Value.ToString
                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then
                            DONE = 1
                        Else
                            DONE = 0
                        End If
                        OUTPCS = Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = Val(row.Cells(GOUTMTRS.Index).Value)
                        RATE = row.Cells(GRATE.Index).Value
                        PER = row.Cells(GPER.Index).Value.ToString
                        If row.Cells(GAMOUNT.Index).Value <> Nothing Then AMT = row.Cells(GAMOUNT.Index).Value Else AMT = 0
                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(gsrno.Index).Value)
                        GREYMTRS = GREYMTRS & "|" & Val(row.Cells(GGREYMTRS.Index).Value)
                        RECDMTRS = RECDMTRS & "|" & Val(row.Cells(GRECDMTRS.Index).Value)
                        CHECKEDMTRS = CHECKEDMTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        If row.Cells(Gdesc.Index).Value <> Nothing Then GRIDREMARKS = GRIDREMARKS & "|" & row.Cells(Gdesc.Index).Value.ToString Else GRIDREMARKS = GRIDREMARKS & "|" & ""
                        PIECETYPE = PIECETYPE & "|" & row.Cells(GPIECETYPE.Index).Value.ToString
                        DIFF = DIFF & "|" & Val(row.Cells(GDIFF.Index).Value)
                        UNIT = UNIT & "|" & row.Cells(GUNIT.Index).Value
                        CHKWIDTH = CHKWIDTH & "|" & Val(row.Cells(GCHKWIDTH.Index).Value)
                        RECDWT = RECDWT & "|" & Val(row.Cells(GRECDWT.Index).Value)
                        WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                        WTDIFF = WTDIFF & "|" & Val(row.Cells(GWTDIFF.Index).Value)
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(GCOLOR.Index).Value.ToString
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        RACK = RACK & "|" & row.Cells(GRACK.Index).Value.ToString
                        SHELF = SHELF & "|" & row.Cells(GSHELF.Index).Value.ToString
                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then
                            DONE = DONE & "|" & "1"
                        Else
                            DONE = DONE & "|" & "0"
                        End If
                        OUTPCS = OUTPCS & "|" & Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = OUTMTRS & "|" & Val(row.Cells(GOUTMTRS.Index).Value)
                        RATE = RATE & "|" & row.Cells(GRATE.Index).Value
                        PER = PER & "|" & row.Cells(GPER.Index).Value
                        If row.Cells(GAMOUNT.Index).Value <> Nothing Then AMT = AMT & "|" & row.Cells(GAMOUNT.Index).Value Else AMT = AMT & "|" & "0"
                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(GREYMTRS)
            alParaval.Add(RECDMTRS)
            alParaval.Add(CHECKEDMTRS)
            alParaval.Add(GRIDREMARKS)
            alParaval.Add(PIECETYPE)
            alParaval.Add(DIFF)
            alParaval.Add(UNIT)
            alParaval.Add(CHKWIDTH)
            alParaval.Add(RECDWT)
            alParaval.Add(WT)
            alParaval.Add(WTDIFF)
            alParaval.Add(ITEMNAME)
            alParaval.Add(QUALITY)
            alParaval.Add(BALENO)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(BARCODE)
            alParaval.Add(RACK)
            alParaval.Add(SHELF)
            alParaval.Add(DONE)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)
            alParaval.Add(RATE)
            alParaval.Add(PER)
            alParaval.Add(AMT)
            alParaval.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(TXTLRNO.Text.Trim)
            alParaval.Add(TXTWIDTH.Text.Trim)
            alParaval.Add(TXTDECLMTRS.Text.Trim)
            alParaval.Add(TXTDECLPCS.Text.Trim)
            Dim OBJCHECKING As New ClsInHouseChecking()
            OBJCHECKING.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJCHECKING.SAVE()
                TEMPCHECKINGNO = DT.Rows(0).Item(0)
                MsgBox("Details Added")
            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPCHECKINGNO)
                Dim IntResult As Integer = OBJCHECKING.UPDATE()
                MsgBox("Details Updated")
            End If
            PRINTREPORT()

            EDIT = False
            CLEAR()
            CHECKINGDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PRINTBARCODE()
        Try
            If ALLOWBARCODEPRINT Then

                'PRINT BARCODE
                Dim TEMPMSG As Integer = MsgBox("Wish to Print Barcode?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                Dim WHOLESALEBARCODE As Integer = 0
                If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then WHOLESALEBARCODE = MsgBox("Wish to Print Wholesale Barcode?", MsgBoxStyle.YesNo)

                Dim TEMPHEADER As String = ""
                If ClientName = "YASHVI" Then
                    TEMPHEADER = InputBox("Enter Sticker Type (M/N/Y/B)")
                    If TEMPHEADER <> "M" And TEMPHEADER <> "N" And TEMPHEADER <> "Y" And TEMPHEADER <> "B" Then Exit Sub
                    If TEMPHEADER = "M" Then TEMPHEADER = "MAFATLAL"
                    If TEMPHEADER = "N" Then TEMPHEADER = ""
                End If

                If ClientName = "GELATO" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR MRP" & Chr(13) & "3 FOR WSP")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
                End If

                If ClientName = "DAKSH" Or ClientName = "KUNAL" Or ClientName = "VALIANT" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR PRE PRINTED")
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
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR LUMP" & Chr(13) & "2 FOR CUTPACK")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If

                If ClientName = "MANS" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR SALVATROE" & Chr(13) & "2 FOR DONBION" & Chr(13) & "2 FOR OCM")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
                End If

                If ClientName = "AXIS" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR PCS" & Chr(13) & "2 FOR MTRS")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If

                If ClientName = "KRISHNA" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "SIMPLEX" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR BOX STICKER")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
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

                Dim NARR As String = ""
                Dim OBJCHECKING As New ClsInHouseChecking()
                Dim dttable As DataTable = OBJCHECKING.SELECTCHECKING(TEMPCHECKINGNO, YearId)
                If dttable.Rows.Count > 0 Then
                    For Each dr As DataRow In dttable.Rows

                        'TO PRINT BARCODE FROM SELECTED SRNO
                        If (Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0) Then
                            If Val(dr("GRIDSRNO")) < Val(TXTFROM.Text.Trim) Or Val(dr("GRIDSRNO")) > Val(TXTTO.Text.Trim) Then GoTo NEXTLINE
                        End If
                        If ClientName = "YASHVI" Then NARR = "" Else NARR = dr("NARR")
                        Dim DYEINGNAME As String = ""
                        If ClientName = "RAJKRIPA" Then DYEINGNAME = TXTNAME.Text.Trim

                        'IF barcode is used the BARCODE printING WILL BE BLOCKED
                        If Val(dr("OUTMTRS")) > 0 Then GoTo NEXTLINE

                        BARCODEPRINTING(dr("BARCODE"), dr("PIECETYPE"), dr("ITEMNAME"), dr("QUALITY"), dr("DESIGN"), dr("COLOR"), dr("UNIT"), dr("LOTNO"), dr("NARR"), NARR, Val(dr("CHECKEDMTRS")), 1, 0, dr("RACK"), TEMPHEADER, SUPRIYAHEADER, WHOLESALEBARCODE, "", DYEINGNAME)
NEXTLINE:
                    Next
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InHouseChecking_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If errorvalid() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNoCancel)
                If tempmsg = vbCancel Then Exit Sub
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'for Delete
            GRIDCHECKING.Focus()
        ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
            toolprevious_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
            toolnext_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for CLEAR
            tstxtbillno.Focus()
        ElseIf e.KeyCode = Keys.P And e.Alt = True Then
            Call PrintToolStripButton_Click(sender, e)
        End If
    End Sub

    Private Sub InHouseChecking_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GRN CHECKING'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            CLEAR()

            If ClientName = "VINTAGEINDIA" Then TXTCHECKEDBY.ReadOnly = True


            If EDIT = True Then
                SHOWDATA()
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

            Dim OBJCHECKING As New ClsInHouseChecking()
            Dim dttable As DataTable = OBJCHECKING.SELECTCHECKING(TEMPCHECKINGNO, YearId)
            If dttable.Rows.Count > 0 Then
                CMDSELECTLOT.Enabled = False

                For Each dr As DataRow In dttable.Rows
                    TXTCHECKINGNO.Text = TEMPCHECKINGNO
                    CHECKINGDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                    TXTNAME.Text = Convert.ToString(dr("NAME").ToString)
                    TXTGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                    TXTWEAVERNAME.Text = dr("WEAVERNAME")
                    TXTWEAVERCHNO.Text = dr("WEAVERCHNO")
                    TXTLOTNO.Text = dr("LOTNO")
                    TXTMATRECNO.Text = Val(dr("MATRECNO"))
                    TXTTYPE.Text = dr("TYPE")
                    TXTCHECKEDBY.Text = Convert.ToString(dr("CHECKEDBY").ToString)
                    txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                    CHALLANDATE.Text = dr("CHALLANDATE")
                    TXTLRNO.Text = dr("LRNO")
                    TXTWIDTH.Text = dr("WIDTH")
                    TXTDECLMTRS.Text = Format(Val(dr("DECLMTRS")), "0.00")
                    TXTDECLPCS.Text = Format(Val(dr("DECLPCS")), "0.00")

                    GRIDCHECKING.Rows.Add(dr("GRIDSRNO").ToString, Format(Val(dr("GREYMTRS")), "0.00"), Format(Val(dr("RECDMTRS")), "0.00"), Format(Val(dr("CHECKEDMTRS")), "0.00"), dr("NARR").ToString, dr("PIECETYPE").ToString, Format(Val(dr("DIFF")), "0.00"), dr("UNIT").ToString, Format(Val(dr("CHKWIDTH")), "0.00"), Format(Val(dr("RECDWT")), "0.00"), Format(Val(dr("WT")), "0.00"), Format(Val(dr("WTDIFF")), "0.00"), dr("ITEMNAME"), dr("QUALITY"), dr("BALENO"), dr("DESIGN"), dr("COLOR").ToString, dr("BARCODE"), dr("RACK"), dr("SHELF"), dr("DONE"), Val(dr("OUTPCS")), Val(dr("OUTMTRS")), Val(dr("RATE")), dr("PER"), Val(dr("AMT")), Val(dr("WTPER")))
                    If dr("NARR") <> "" And ClientName = "VINTAGEINDIA" Then Gdesc.ReadOnly = True

                    If Val(dr("OUTMTRS")) > 0 Then
                        GRIDCHECKING.Rows(GRIDCHECKING.RowCount - 1).DefaultCellStyle.BackColor = Drawing.Color.Yellow
                        GRIDCHECKING.Rows(GRIDCHECKING.RowCount - 1).ReadOnly = True
                    End If

                Next
                TOTAL()
                GRIDCHECKING.FirstDisplayedScrollingRowIndex = GRIDCHECKING.RowCount - 1
            Else
                EDIT = False
                CLEAR()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            'FILL PIECETYPE
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(PIECETYPE_NAME,'') AS PIECETYPE", "", " PIECETYPEMASTER ", " AND PIECETYPE_YEARID =" & YearId)
            If DT.Rows.Count > 0 Then
                GPIECETYPE.Items.Clear()
                For Each ROW As DataRow In DT.Rows
                    GPIECETYPE.Items.Add(ROW("PIECETYPE"))
                Next
            End If

            DT = OBJCMN.SEARCH("ISNULL(UNIT_ABBR,'') AS UNIT", "", " UNITMASTER ", " AND UNIT_YEARID =" & YearId)
            If DT.Rows.Count > 0 Then
                GUNIT.Items.Clear()
                For Each ROW As DataRow In DT.Rows
                    GUNIT.Items.Add(ROW("UNIT"))
                Next
            End If

            DT = OBJCMN.SEARCH("ISNULL(COLOR_NAME,'') AS COLOR", "", " COLORMASTER ", " AND COLOR_YEARID =" & YearId)
            If DT.Rows.Count > 0 Then
                GCOLOR.Items.Clear()
                For Each ROW As DataRow In DT.Rows
                    GCOLOR.Items.Add(ROW("COLOR"))
                Next
            End If

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
            Dim OBJCHECKING As New InHouseCheckingDetails
            OBJCHECKING.MdiParent = MDIMain
            OBJCHECKING.Show()
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

    Private Sub CMDSELECTLOT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDSELECTLOT.Click
        Try

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJSELECTLOT As New SelectLot
            Dim DT As DataTable = OBJSELECTLOT.DT
            OBJSELECTLOT.ShowDialog()

            If DT.Rows.Count > 0 Then

                TXTNAME.Text = DT.Rows(0).Item("NAME")
                TXTGODOWN.Text = DT.Rows(0).Item("GODOWN")
                TXTMATRECNO.Text = Val(DT.Rows(0).Item("MATRECNO"))
                If DT.Rows(0).Item("LOTNO") <> "0" Then TXTLOTNO.Text = DT.Rows(0).Item("LOTNO")
                TXTTYPE.Text = DT.Rows(0).Item("TYPE")
                TXTLRNO.Text = DT.Rows(0).Item("LRNO")
                Dim OBJCMN As New ClsCommon
                Dim DTTABLE As New DataTable
                If TXTTYPE.Text = "MATREC" Then
                    'DTTABLE = OBJCMN.SEARCH(" MATERIALRECEIPT_DESC.MATREC_MTRS AS GREYMTRS, MATERIALRECEIPT_DESC.MATREC_RECDMTRS AS RECDMTRS, ITEMMASTER.item_name AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR , MATREC_GRIDSRNO AS FROMSRNO, ISNULL(UNIT_ABBR,'') AS UNIT, ISNULL(MATERIALRECEIPT_DESC.MATREC_GRIDLOTNO,'') AS LOTNO, ISNULL(MATERIALRECEIPT_DESC.MATREC_RATE,0) AS RATE, ISNULL(MATERIALRECEIPT_DESC.MATREC_BALENO, '') AS BALENO, ISNULL(MATERIALRECEIPT_DESC.MATREC_WT, 0) AS RECDWT, ISNULL(ITEMMASTER.item_PERCENT,0) AS WTPER ", "", " MATERIALRECEIPT_DESC INNER JOIN ITEMMASTER ON MATERIALRECEIPT_DESC.MATREC_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON MATERIALRECEIPT_DESC.MATREC_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON MATERIALRECEIPT_DESC.MATREC_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON MATERIALRECEIPT_DESC.MATREC_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN UNITMASTER ON MATERIALRECEIPT_DESC.MATREC_QTYUNITID = UNITMASTER.UNIT_id ", " AND MATREC_NO = " & Val(DT.Rows(0).Item("MATRECNO")) & " AND MATREC_YEARID = " & YearId)
                    DTTABLE = OBJCMN.SEARCH(" MATERIALRECEIPT_DESC.MATREC_MTRS AS GREYMTRS, MATERIALRECEIPT_DESC.MATREC_RECDMTRS AS RECDMTRS, ITEMMASTER.item_name AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, MATERIALRECEIPT_DESC.MATREC_GRIDSRNO AS FROMSRNO, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(MATERIALRECEIPT_DESC.MATREC_GRIDLOTNO, '') AS LOTNO, ISNULL(MATERIALRECEIPT_DESC.MATREC_RATE, 0) AS RATE, ISNULL(MATERIALRECEIPT_DESC.MATREC_BALENO, '') AS BALENO, ISNULL(MATERIALRECEIPT_DESC.MATREC_WT, 0) AS RECDWT, ISNULL(ITEMMASTER.ITEM_PERCENT, 0) AS WTPER, ISNULL(MATERIALRECEIPT.MATREC_CHALLANNO,'') AS CHALLANNO, MATERIALRECEIPT.MATREC_CHALLANDATE AS CHALLANDATE ", "", " MATERIALRECEIPT_DESC INNER JOIN ITEMMASTER ON MATERIALRECEIPT_DESC.MATREC_ITEMID = ITEMMASTER.item_id INNER JOIN MATERIALRECEIPT ON MATERIALRECEIPT_DESC.MATREC_NO = MATERIALRECEIPT.MATREC_NO AND MATERIALRECEIPT_DESC.MATREC_YEARID = MATERIALRECEIPT.MATREC_yearid LEFT OUTER JOIN COLORMASTER ON MATERIALRECEIPT_DESC.MATREC_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON MATERIALRECEIPT_DESC.MATREC_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON MATERIALRECEIPT_DESC.MATREC_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN UNITMASTER ON MATERIALRECEIPT_DESC.MATREC_QTYUNITID = UNITMASTER.unit_id ", " AND MATERIALRECEIPT.MATREC_NO = " & Val(DT.Rows(0).Item("MATRECNO")) & " AND MATERIALRECEIPT.MATREC_YEARID = " & YearId)
                ElseIf TXTTYPE.Text = "JOBIN" Then
                    'DTTABLE = OBJCMN.SEARCH("  JOBIN_DESC.JI_MTRS AS GREYMTRS, JOBIN_DESC.JI_MTRS AS RECDMTRS, ITEMMASTER.item_name AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, JOBIN_DESC.JI_GRIDSRNO AS FROMSRNO, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(JOBIN.JI_LOTNO, '') AS LOTNO, ISNULL(JOBIN_DESC.JI_RATE, 0) AS RATE, ISNULL(JOBIN_DESC.JI_BALENO, '') AS BALENO, ISNULL(JOBIN_DESC.JI_WT, '') AS RECDWT, ISNULL(ITEMMASTER.item_PERCENT,0) AS WTPER ", "", " JOBIN_DESC INNER JOIN ITEMMASTER ON JOBIN_DESC.JI_ITEMID = ITEMMASTER.item_id INNER JOIN JOBIN ON JOBIN_DESC.JI_NO = JOBIN.JI_no AND JOBIN_DESC.JI_YEARID = JOBIN.JI_yearid LEFT OUTER JOIN COLORMASTER ON JOBIN_DESC.JI_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON JOBIN_DESC.JI_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON JOBIN_DESC.JI_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN UNITMASTER ON JOBIN_DESC.JI_QTYUNITID = UNITMASTER.unit_id ", " AND JOBIN.JI_NO = " & Val(DT.Rows(0).Item("MATRECNO")) & " AND JOBIN.JI_YEARID = " & YearId)
                    DTTABLE = OBJCMN.SEARCH(" JOBIN_DESC.JI_MTRS AS GREYMTRS, JOBIN_DESC.JI_MTRS AS RECDMTRS, ITEMMASTER.item_name AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, JOBIN_DESC.JI_GRIDSRNO AS FROMSRNO, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(JOBIN.JI_LOTNO, '') AS LOTNO, ISNULL(JOBIN_DESC.JI_RATE, 0) AS RATE, ISNULL(JOBIN_DESC.JI_BALENO, '') AS BALENO, ISNULL(JOBIN_DESC.JI_WT, '') AS RECDWT, ISNULL(ITEMMASTER.ITEM_PERCENT, 0) AS WTPER, ISNULL(JOBIN.JI_CHALLANNO, '') AS CHALLANNO, JOBIN.JI_CHALLANDATE AS CHALLANDATE ", "", " JOBIN_DESC INNER JOIN ITEMMASTER ON JOBIN_DESC.JI_ITEMID = ITEMMASTER.item_id INNER JOIN JOBIN ON JOBIN_DESC.JI_NO = JOBIN.JI_no AND JOBIN_DESC.JI_YEARID = JOBIN.JI_yearid LEFT OUTER JOIN COLORMASTER ON JOBIN_DESC.JI_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON JOBIN_DESC.JI_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON JOBIN_DESC.JI_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN UNITMASTER ON JOBIN_DESC.JI_QTYUNITID = UNITMASTER.unit_id ", " AND JOBIN.JI_NO = " & Val(DT.Rows(0).Item("MATRECNO")) & " AND JOBIN.JI_YEARID = " & YearId)
                Else
                    'DTTABLE = OBJCMN.SEARCH(" GRN_DESC.GRN_MTRS AS GREYMTRS, GRN_DESC.GRN_MTRS AS RECDMTRS, ITEMMASTER.item_name AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR , GRN_GRIDSRNO AS FROMSRNO, ISNULL(UNIT_ABBR,'') AS UNIT, '' AS LOTNO, ISNULL(GRN_DESC.GRN_PURRATE,0) AS RATE, ISNULL(GRN_DESC.GRN_BALENO, '') AS BALENO, ISNULL(GRN_DESC.GRN_WT, '') AS RECDWT, ISNULL(ITEMMASTER.item_PERCENT,0) AS WTPER", "", " GRN_DESC INNER JOIN ITEMMASTER ON GRN_DESC.GRN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON GRN_DESC.GRN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GRN_DESC.GRN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON GRN_DESC.GRN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN UNITMASTER ON GRN_DESC.GRN_QTYUNITID = UNITMASTER.UNIT_id ", " AND GRN_GRIDTYPE = 'FANCY MATERIAL' AND GRN_NO = " & Val(DT.Rows(0).Item("MATRECNO")) & " AND GRN_YEARID = " & YearId)
                    DTTABLE = OBJCMN.SEARCH(" GRN_DESC.GRN_MTRS AS GREYMTRS, GRN_DESC.GRN_MTRS AS RECDMTRS, ITEMMASTER.item_name AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, GRN_DESC.GRN_GRIDSRNO AS FROMSRNO, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, '' AS LOTNO, ISNULL(GRN_DESC.GRN_PURRATE, 0) AS RATE, ISNULL(GRN_DESC.GRN_BALENO, '') AS BALENO, ISNULL(GRN_DESC.GRN_WT, '') AS RECDWT, ISNULL(ITEMMASTER.ITEM_PERCENT, 0) AS WTPER, ISNULL(GRN.grn_challanno, '') AS CHALLANNO, GRN.grn_challandt AS CHALLANDATE", "", " GRN_DESC INNER JOIN ITEMMASTER ON GRN_DESC.GRN_ITEMID = ITEMMASTER.item_id INNER JOIN GRN ON GRN_DESC.GRN_NO = GRN.grn_no AND GRN_DESC.GRN_YEARID = GRN.grn_yearid AND GRN_DESC.GRN_GRIDTYPE = GRN.GRN_TYPE LEFT OUTER JOIN COLORMASTER ON GRN_DESC.GRN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GRN_DESC.GRN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON GRN_DESC.GRN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN UNITMASTER ON GRN_DESC.GRN_QTYUNITID = UNITMASTER.unit_id  ", " AND GRN_GRIDTYPE = 'FANCY MATERIAL' AND GRN.GRN_NO = " & Val(DT.Rows(0).Item("MATRECNO")) & " AND GRN.GRN_YEARID = " & YearId)

                End If
                For Each ROW As DataRow In DTTABLE.Rows
                    Dim CHECKEDMTRS As Decimal = 0.0
                    If CHKCHECKMTRS.CheckState = 1 Then CHECKEDMTRS = Val(ROW("RECDMTRS"))
                    'If ClientName = "SHASHWAT" Then CHECKEDMTRS = 0
                    If TXTLOTNO.Text.Trim = "" And ROW("LOTNO") <> "0" And ROW("LOTNO") <> "" Then TXTLOTNO.Text = ROW("LOTNO")

                    'FETCH WEAVERNAME AND WEAVERCHNO FROM LOTVIEW
                    If TXTLOTNO.Text.Trim <> "" Then
                        Dim DTWEAVER As DataTable = OBJCMN.SEARCH(" PARTYNAME AS WEAVERNAME, CHALLANNO AS WEAVERCHNO", "", "LOT_VIEW", " AND LOTNO = '" & TXTLOTNO.Text.Trim & "' AND JOBBERNAME = '" & TXTNAME.Text.Trim & "' AND YEARID = " & YearId)
                        If DTWEAVER.Rows.Count > 0 Then
                            TXTWEAVERNAME.Text = DTWEAVER.Rows(0).Item("WEAVERNAME")
                            TXTWEAVERCHNO.Text = DTWEAVER.Rows(0).Item("WEAVERCHNO")
                        End If
                    End If

                    If TXTTYPE.Text.Trim <> "MATREC" Then TXTWEAVERCHNO.Text = ROW("CHALLANNO")
                    If TXTTYPE.Text.Trim = "GRN" Then TXTWEAVERNAME.Text = DT.Rows(0).Item("NAME")

                    CHALLANDATE.Text = ROW("CHALLANDATE")
                    TXTWEAVERCHNO.Text = ROW("CHALLANNO")

                    GRIDCHECKING.Rows.Add(0, Val(ROW("GREYMTRS")), Val(ROW("RECDMTRS")), Val(CHECKEDMTRS), "", "FRESH", 0, ROW("UNIT"), 0, Format(Val(ROW("RECDWT")), "0.00"), 0, 0, ROW("ITEMNAME"), ROW("QUALITY"), ROW("BALENO"), ROW("DESIGN"), ROW("COLOR"), "", "", "", 0, 0, 0, ROW("RATE"), "Mtrs", 0, Val(ROW("WTPER")))
                Next
                CMDSELECTLOT.Enabled = False
                getsrno(GRIDCHECKING)
                TOTAL()
                'CHECKINGDATE.Focus()
                If ClientName = "VINTAGEINDIA" Then GRIDCHECKING.Focus()
            Else
                CHECKINGDATE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDCHECKING.RowCount = 0
                TEMPCHECKINGNO = Val(tstxtbillno.Text)
                If TEMPCHECKINGNO > 0 Then
                    EDIT = True
                    SHOWDATA()
                    'InHouseChecking_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            GRIDCHECKING.RowCount = 0
LINE1:
            TEMPCHECKINGNO = Val(TXTCHECKINGNO.Text) - 1
            If TEMPCHECKINGNO > 0 Then
                EDIT = True
                SHOWDATA()
                'InHouseChecking_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDCHECKING.RowCount = 0 And TEMPCHECKINGNO > 1 Then
                TXTCHECKINGNO.Text = TEMPCHECKINGNO
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
            GRIDCHECKING.RowCount = 0
LINE1:
            TEMPCHECKINGNO = Val(TXTCHECKINGNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTCHECKINGNO.Text.Trim
            clear()
            If Val(TXTCHECKINGNO.Text) - 1 >= TEMPCHECKINGNO Then
                EDIT = True
                SHOWDATA()
                'InHouseChecking_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDCHECKING.RowCount = 0 And TEMPCHECKINGNO < MAXNO Then
                TXTCHECKINGNO.Text = TEMPCHECKINGNO
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
        Dim IntResult As Integer
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Unable to Delete, Checking Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete Checking?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim alParaval As New ArrayList
                alParaval.Add(Val(TXTCHECKINGNO.Text.Trim))
                alParaval.Add(TXTTYPE.Text.Trim)
                alParaval.Add(YearId)

                Dim ClsInHouseChecking As New ClsInHouseChecking()
                ClsInHouseChecking.alParaval = alParaval
                IntResult = ClsInHouseChecking.DELETE()
                MsgBox("Checking Deleted")
                clear()
                EDIT = False
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDCHECKING_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDCHECKING.CellValidating
        Try
            Dim colNum As Integer = GRIDCHECKING.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GMTRS.Index, GWT.Index, GCHKWIDTH.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDCHECKING.CurrentCell.Value = Nothing Then GRIDCHECKING.CurrentCell.Value = "0.00"
                        GRIDCHECKING.CurrentCell.Value = Format(Convert.ToDecimal(GRIDCHECKING.Item(colNum, e.RowIndex).Value), "0.00")
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

    Private Sub GRIDCHECKING_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCHECKING.KeyDown
        Try
            If e.KeyCode = Keys.F12 And GRIDCHECKING.RowCount > 0 Then
                If Val(GRIDCHECKING.CurrentRow.Cells(GOUTMTRS.Index).EditedFormattedValue) > 0 Then Exit Sub
                If GRIDCHECKING.CurrentRow.Cells(GITEMNAME.Index).Value <> "" Then
                    If ClientName = "SNCM" Or ClientName = "VINTAGEINDIA" Then
                        If EDIT = False Then
                            GRIDCHECKING.Rows.Insert(GRIDCHECKING.CurrentRow.Index, CloneWithValues(GRIDCHECKING.CurrentRow))
                            GRIDCHECKING.Item(GRECDMTRS.Index, GRIDCHECKING.CurrentRow.Index).Value = 0
                            GRIDCHECKING.Item(GGREYMTRS.Index, GRIDCHECKING.CurrentRow.Index).Value = 0
                        Else
                            GRIDCHECKING.Rows.Add(CloneWithValues(GRIDCHECKING.CurrentRow))

                            'GET LAST BARCODE SRNO
                            Dim LSRNO As Integer = 0
                            Dim RSRNO As Integer = 0
                            Dim SNO As Integer = 0
                            Dim TEMPBARCODE As String = ""
                            LSRNO = InStr(GRIDCHECKING.Rows(GRIDCHECKING.RowCount - 2).Cells(GBARCODE.Index).Value, "/")
                            RSRNO = InStr(LSRNO + 1, GRIDCHECKING.Rows(GRIDCHECKING.RowCount - 2).Cells(GBARCODE.Index).Value, "/")
                            SNO = GRIDCHECKING.Rows(GRIDCHECKING.RowCount - 2).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                            TEMPBARCODE = "C-" & Val(TXTCHECKINGNO.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                            GRIDCHECKING.Item(GBARCODE.Index, GRIDCHECKING.RowCount - 1).Value = TEMPBARCODE

                            GRIDCHECKING.Item(GRECDMTRS.Index, GRIDCHECKING.RowCount - 1).Value = 0
                            GRIDCHECKING.Item(GGREYMTRS.Index, GRIDCHECKING.RowCount - 1).Value = 0

                        End If
                        getsrno(GRIDCHECKING)
                        TOTAL()
                    End If
                End If
            ElseIf e.KeyCode = Keys.Delete And GRIDCHECKING.Item(GRECDMTRS.Index, GRIDCHECKING.CurrentRow.Index).Value = 0 Then
                If GRIDCHECKING.Item(GOUTMTRS.Index, GRIDCHECKING.CurrentRow.Index).Value > 0 Then
                    MsgBox("Unable To Delete, Entry Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                GRIDCHECKING.Rows.RemoveAt(GRIDCHECKING.CurrentRow.Index)
                getsrno(GRIDCHECKING)
                TOTAL()
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

    Sub PRINTREPORT()
        Try
            If MsgBox("Print Checking Report?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim OBJPRINT As New InHouseDesign
                OBJPRINT.MdiParent = MDIMain
                OBJPRINT.WHERECLAUSE = "{INHOUSECHECKING.CHECK_NO} = " & TEMPCHECKINGNO & " AND {INHOUSECHECKING.CHECK_YEARID} = " & YearId
                OBJPRINT.Show()
            End If
            PRINTBARCODE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            'speech to text
            Dim recoganizer As New SpeechRecognitionEngine
            recoganizer.LoadGrammar(New DictationGrammar)
            recoganizer.SetInputToDefaultAudioDevice()
            recoganizer.RecognizeAsync(RecognizeMode.Multiple)
            AddHandler recoganizer.SpeechRecognized, AddressOf Recoganizer_SpeechRecognizer

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Recoganizer_SpeechRecognizer(sender As Object, e As SpeechRecognizedEventArgs)
        'For Each row As DataGridViewRow In GRIDCHECKING.Rows
        '    row.Cells(Gdesc.Index).Value &= e.Result.Text & Environment.NewLine
        'Next
        txtremarks.Text &= e.Result.Text & Environment.NewLine
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        If EDIT = True Then PRINTREPORT()
    End Sub

    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstxtbillno.KeyPress, TXTFROM.KeyPress, TXTTO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub InHouseChecking_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "RAJKRIPA" Then Gdesc.HeaderText = "Grade"

            If ClientName = "SHASHWAT" Then CHKCHECKMTRS.CheckState = CheckState.Checked

            If ClientName = "VINTAGEINDIA" Then
                TXTCHECKEDBY.Text = UserName
                GBALENO.HeaderText = "Pcs No"
                GQUALITY.Visible = False
            End If
            If ClientName = "SNCM" Then
                GPIECETYPE.ReadOnly = False
                LBLWIDTH.Visible = True
                TXTWIDTH.Visible = True
            End If

            If ClientName = "YASHVI" Then
                GCHKWIDTH.Visible = False
                GRECDWT.Visible = False
                GWT.Visible = False
                GWTDIFF.Visible = False
                GQUALITY.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTDECLMTRS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTDECLMTRS.KeyPress, TXTDECLPCS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub
End Class