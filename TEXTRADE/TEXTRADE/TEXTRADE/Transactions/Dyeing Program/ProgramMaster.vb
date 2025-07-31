
Imports System.ComponentModel
Imports System.IO
Imports BL

Public Class ProgramMaster

    Dim GRIDDOUBLECLICK As Boolean
    Public EDIT As Boolean          'used for editing
    Public PROGRAMNO, TEMPCHGSROW As Integer     'used for poation no while editing
    Dim TEMPROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE, GRIDCHGSDOUBLECLICK As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim ALLOWMANUALPROGNO As Boolean = False

    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstxtbillno.KeyPress, TXTFROM.KeyPress, TXTTO.KeyPress, TXTCOPIES.KeyPress, TXTPROGRAMNO.KeyPress, TXTSONO.KeyPress, TXTFETCHGRNNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        EP.Clear()
        tstxtbillno.Clear()
        PROGRAMDATE.Value = Now.Date
        CMBNAME.Text = ""
        CMBNAME.Enabled = True
        CARDRECDATE.Clear()
        CARDISSUEDATE.Clear()

        TXTSRNO.Text = 1
        CMBLOTNO.Items.Clear()
        CMBLOTNO.Text = ""
        CMBLOTNO.Enabled = True
        CMBITEMNAME.Text = ""
        CMBDESIGNNO.Text = ""
        TXTTOTALPCS.Clear()
        TXTGRNNO.Clear()
        TXTGRNTYPE.Clear()
        CMBCOLOR.Text = ""
        TXTPCS.Clear()
        GRIDLOT.RowCount = 0
        LBLTOTALPCS.Text = 0
        TXTBARCODE.Clear()
        CHKURGENT.CheckState = CheckState.Unchecked
        TXTCHGSAMT.Clear()
        CMBCHARGES.Text = ""
        txtremarks.Clear()
        TXTCHGSSRNO.Clear()
        GRIDDOUBLECLICK = False
        GETMAXNO()
        CHKAPPROVED.CheckState = CheckState.Unchecked
        CHKKHADI.CheckState = CheckState.Unchecked
        CHKABOVETOSCREEN.CheckState = CheckState.Unchecked
        CHKMISCRATE.CheckState = CheckState.Unchecked
        CMBRATETYPE.Text = ""

        If ALLOWMANUALPROGNO = True Then
            TXTPROGRAMNO.ReadOnly = False
            TXTPROGRAMNO.BackColor = Color.LemonChiffon
        Else
            TXTPROGRAMNO.ReadOnly = True
            TXTPROGRAMNO.BackColor = Color.Linen
        End If
        TXTPROCESSNAME.Clear()
        TXTFOLD.Clear()
        TXTFINISHTYPE.Clear()
        TXTSONO.Clear()
        GRIDSUMM.RowCount = 0

        GRIDCHGS.RowCount = 0
        GRIDCHGSDOUBLECLICK = False

        CLB_PROCESS.SelectedItem = CheckState.Unchecked
        CHKPROCESS.CheckState = CheckState.Unchecked

    End Sub

    Sub TOTAL()
        Try
            LBLTOTALPCS.Text = 0.0
            GRIDSUMM.RowCount = 0
            Dim DONE As Boolean = False
            For Each ROW As DataGridViewRow In GRIDLOT.Rows
                LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(ROW.Cells(GPCS.Index).Value), "0.00")

                DONE = False
                If Val(ROW.Cells(GPCS.Index).EditedFormattedValue) > 0 Then
                    If GRIDSUMM.RowCount = 0 Then
                        GRIDSUMM.Rows.Add(ROW.Cells(GLOTNO.Index).Value, Format(Val(ROW.Cells(GTOTALPCS.Index).EditedFormattedValue), "0.00"), Format(Val(ROW.Cells(GPCS.Index).EditedFormattedValue), "0.00"), Format(Val(ROW.Cells(GTOTALPCS.Index).EditedFormattedValue) - Val(ROW.Cells(GPCS.Index).EditedFormattedValue), "0.00"))
                    Else
                        For Each SUMMROW As DataGridViewRow In GRIDSUMM.Rows
                            If SUMMROW.Cells(SLOTNO.Index).Value = ROW.Cells(GLOTNO.Index).Value Then
                                SUMMROW.Cells(SPRGMTRS.Index).Value = Format(Val(SUMMROW.Cells(SPRGMTRS.Index).EditedFormattedValue) + Val(ROW.Cells(GPCS.Index).EditedFormattedValue), "0.00")
                                SUMMROW.Cells(SBALMTRS.Index).Value = Format(Val(SUMMROW.Cells(SMTRS.Index).EditedFormattedValue) - Val(SUMMROW.Cells(SPRGMTRS.Index).EditedFormattedValue), "0.00")
                                DONE = True
                            End If
                        Next
                        If DONE = False Then GRIDSUMM.Rows.Add(ROW.Cells(GLOTNO.Index).Value, Format(Val(ROW.Cells(GTOTALPCS.Index).EditedFormattedValue), "0.00"), Format(Val(ROW.Cells(GPCS.Index).EditedFormattedValue), "0.00"), Format(Val(ROW.Cells(GTOTALPCS.Index).EditedFormattedValue) - Val(ROW.Cells(GPCS.Index).EditedFormattedValue), "0.00"))
                    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        clear()
        EDIT = False
        PROGRAMDATE.Focus()
    End Sub

    Private Sub PROGRAMDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PROGRAMDATE.Validating
        If Not datecheck(PROGRAMDATE.Value) Then
            MsgBox("Date Not in Current Accounting Year")
            e.Cancel = True
        End If
    End Sub

    Sub GETMAXNO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(PROGRAM_no),0) + 1 ", " PROGRAMMASTER ", " AND PROGRAM_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTPROGRAMNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True

            getsrno(GRIDLOT)

            If ClientName <> "AVIS" And CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, " Please Select Name ")
                bln = False
            End If

            If ClientName = "AVIS" Then
                If CMBNAME.Text.Trim <> "" And CARDRECDATE.Text = "__/__/____" Then CARDRECDATE.Text = PROGRAMDATE.Text
                If CARDRECDATE.Text = " Then__/__/____" And CMBNAME.Text.Trim <> "" Then
                    EP.SetError(CMBNAME, " Please Enter Card Rec Date First")
                    bln = False
                End If
            End If


            'ADD ALL SELECTED PROCESSNAME IN TXTPROCESS ALSO FETCH RATES FROM PROCESSES AND ADD IN GRID
            Dim TEMPRATE As Double = 0.0
            If ClientName = "VINTAGEINDIA" Then
                Dim CHECKED_PROCESS As CheckedListBox.CheckedItemCollection = CLB_PROCESS.CheckedItems
                If CHECKED_PROCESS.Count > 0 Then TXTPROCESSNAME.Clear()
                For Each ITEM As Object In CHECKED_PROCESS

                    'GET RATE AND SAVE THAT IN TEMPRATE
                    Dim OBJCMN As New ClsCommon
                    Dim DTRATE As DataTable = OBJCMN.SEARCH(" ISNULL(DYEINGPRICELIST_DESC.PL_MISCRATE,0) AS RATE", "", " DYEINGPRICELIST INNER JOIN DYEINGPRICELIST_DESC ON DYEINGPRICELIST.PL_NO = DYEINGPRICELIST_DESC.PL_NO AND DYEINGPRICELIST.PL_YEARID = DYEINGPRICELIST_DESC.PL_YEARID INNER JOIN PROCESSMASTER ON DYEINGPRICELIST_DESC.PL_PROCESSID = PROCESSMASTER.PROCESS_ID INNER JOIN LEDGERS ON DYEINGPRICELIST.PL_LEDGERID = LEDGERS.Acc_id ", " AND PROCESSMASTER.PROCESS_NAME = '" & ITEM.ToString() & "' AND DYEINGPRICELIST.PL_YEARID = " & YearId)
                    If DTRATE.Rows.Count > 0 Then TEMPRATE += Val(DTRATE.Rows(0).Item("RATE"))

                    If TXTPROCESSNAME.Text.Trim = "" Then
                        TXTPROCESSNAME.Text = ITEM.ToString()
                    Else
                        TXTPROCESSNAME.Text = TXTPROCESSNAME.Text & vbCrLf & ITEM.ToString()
                    End If
                Next ITEM
            End If


            If Val(LBLTOTALPCS.Text.Trim) = 0 Then
                EP.SetError(CMBLOTNO, " Please Select Lot No")
                bln = False
            End If

            If Val(GRIDLOT.RowCount) = 0 Then
                EP.SetError(GRIDLOT, " Please Select Lot")
                bln = False
            End If

            If Not datecheck(PROGRAMDATE.Value) Then
                EP.SetError(PROGRAMDATE, "Date Not In Current Accounting Year")
                bln = False
            End If


            If ALLOWMANUALPROGNO = True Then
                If Val(TXTPROGRAMNO.Text.Trim) <> 0 And EDIT = False Then
                    Dim OBJCMNn As New ClsCommon
                    Dim dttable As DataTable = OBJCMNn.search(" ISNULL(PROGRAM_NO, 0)  As PROGRAM", "", " PROGRAMMASTER ", "  And PROGRAM_NO=" & Val(TXTPROGRAMNO.Text.Trim) & " And PROGRAM_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        MsgBox("Program No Already Exist")
                        bln = False
                    End If
                End If
            End If


            'CHECKING WHETHER PRGMTRS GREATER THAN LOTMTRS
            If ClientName = "YASHVI" Then
                For Each ROW As DataGridViewRow In GRIDSUMM.Rows
                    If ClientName = "YASHVI" And Val(ROW.Cells(SBALMTRS.Index).Value) < 0 Then
                        EP.SetError(CMBNAME, "Program Mtrs Cannot be Greater than Lot Mtrs")
                        bln = False
                    End If
                Next
            End If

            If ClientName = "VINTAGEINDIA" Then
                For Each ROW As DataGridViewRow In GRIDLOT.Rows
                    ROW.Cells(GRATE.Index).Value = Val(TEMPRATE)
                Next
            End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            Cursor.Current = Cursors.WaitCursor

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            If TXTPROGRAMNO.ReadOnly = False Then
                alParaval.Add(Val(TXTPROGRAMNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(PROGRAMDATE.Value.Date)
            alParaval.Add(CMBNAME.Text.Trim)
            If CARDRECDATE.Text = "__/__/____" Then alParaval.Add("") Else alParaval.Add(CARDRECDATE.Text)
            If CARDISSUEDATE.Text = "__/__/____" Then alParaval.Add(PROGRAMDATE.Text) Else alParaval.Add(CARDISSUEDATE.Text)
            alParaval.Add(Val(LBLTOTALPCS.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)


            Dim GRIDSRNO As String = ""
            Dim LOTNO As String = ""
            Dim ITEMNAME As String = ""
            Dim DESIGNNO As String = ""
            Dim TOTALPCS As String = ""
            Dim COLOR As String = ""
            Dim URGENT As String = ""
            Dim PCS As String = ""
            Dim KHADI As String = ""
            Dim ABOVETOSCREEN As String = ""
            Dim MISCRATE As String = ""
            Dim RATETYPE As String = ""
            Dim PROGISSDATE As String = ""
            Dim STATUS As String = ""
            Dim PRODCUTTING As String = ""
            Dim FINISHCUTTING As String = ""
            Dim INWARDDATE As String = ""
            Dim GRNNO As String = ""
            Dim GRNTYPE As String = ""
            Dim RECDPCS As String = ""
            Dim BARCODE As String = ""
            Dim RATE As String = ""
            Dim APPROVED As String = ""
            Dim ADJMTRS As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDLOT.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(row.Cells(GSRNO.Index).Value)
                        LOTNO = row.Cells(GLOTNO.Index).Value
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value
                        DESIGNNO = row.Cells(GDESIGNNO.Index).Value
                        TOTALPCS = Val(row.Cells(GTOTALPCS.Index).Value)
                        COLOR = row.Cells(GCOLOR.Index).Value
                        URGENT = row.Cells(GURGENT.Index).Value
                        PCS = Val(row.Cells(GPCS.Index).Value)
                        KHADI = row.Cells(GKHADI.Index).Value
                        ABOVETOSCREEN = row.Cells(GABOVETOSCREEN.Index).Value
                        MISCRATE = row.Cells(GMISCRATE.Index).Value
                        RATETYPE = row.Cells(GRATETYPE.Index).Value
                        If CMBNAME.Text.Trim <> "" And row.Cells(GPROGISSDATE.Index).Value = "" Then PROGISSDATE = PROGRAMDATE.Text Else PROGISSDATE = row.Cells(GPROGISSDATE.Index).Value
                        STATUS = row.Cells(GSTATUS.Index).Value
                        PRODCUTTING = row.Cells(GCUTRECDDATE.Index).Value
                        FINISHCUTTING = row.Cells(GFINISHCUTTING.Index).Value
                        INWARDDATE = row.Cells(GINWARDDATE.Index).Value
                        GRNNO = Val(row.Cells(GGRNNO.Index).Value)
                        GRNTYPE = row.Cells(GGRNTYPE.Index).Value
                        RECDPCS = Val(row.Cells(GRECDPCS.Index).Value)
                        BARCODE = row.Cells(GBARCODE.Index).Value
                        RATE = Val(row.Cells(GRATE.Index).Value)
                        'APPROVED = row.Cells(GAPPROVED.Index).Value
                        If Val(row.Cells(GRATE.Index).Value) >= 10 Then APPROVED = "1" Else APPROVED = row.Cells(GAPPROVED.Index).Value
                        ADJMTRS = Val(row.Cells(GADJMTRS.Index).Value)

                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(GSRNO.Index).Value)
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value
                        DESIGNNO = DESIGNNO & "|" & row.Cells(GDESIGNNO.Index).Value
                        TOTALPCS = TOTALPCS & "|" & Val(row.Cells(GTOTALPCS.Index).Value)
                        COLOR = COLOR & "|" & row.Cells(GCOLOR.Index).Value.ToString
                        URGENT = URGENT & "|" & row.Cells(GURGENT.Index).Value
                        PCS = PCS & "|" & Val(row.Cells(GPCS.Index).Value)
                        KHADI = KHADI & "|" & row.Cells(GKHADI.Index).Value
                        ABOVETOSCREEN = ABOVETOSCREEN & "|" & row.Cells(GABOVETOSCREEN.Index).Value
                        MISCRATE = MISCRATE & "|" & row.Cells(GMISCRATE.Index).Value
                        RATETYPE = RATETYPE & "|" & row.Cells(GRATETYPE.Index).Value
                        If CMBNAME.Text.Trim <> "" And row.Cells(GPROGISSDATE.Index).Value = "" Then PROGISSDATE = PROGISSDATE & "|" & PROGRAMDATE.Text Else PROGISSDATE = PROGISSDATE & "|" & row.Cells(GPROGISSDATE.Index).Value
                        STATUS = STATUS & "|" & row.Cells(GSTATUS.Index).Value
                        PRODCUTTING = PRODCUTTING & "|" & row.Cells(GCUTRECDDATE.Index).Value
                        FINISHCUTTING = FINISHCUTTING & "|" & row.Cells(GFINISHCUTTING.Index).Value
                        INWARDDATE = INWARDDATE & "|" & row.Cells(GINWARDDATE.Index).Value
                        GRNNO = GRNNO & "|" & Val(row.Cells(GGRNNO.Index).Value)
                        GRNTYPE = GRNTYPE & "|" & row.Cells(GGRNTYPE.Index).Value
                        RECDPCS = RECDPCS & "|" & Val(row.Cells(GRECDPCS.Index).Value)
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value
                        RATE = RATE & "|" & Val(row.Cells(GRATE.Index).Value)
                        ''   APPROVED = APPROVED & " | " & row.Cells(GAPPROVED.Index).Value
                        If Val(row.Cells(GRATE.Index).Value) >= 10 Then APPROVED = APPROVED & "|" & "1" Else APPROVED = APPROVED & "|" & row.Cells(GAPPROVED.Index).Value
                        ADJMTRS = ADJMTRS & "|" & Val(row.Cells(GADJMTRS.Index).Value)

                    End If
                End If
            Next
            alParaval.Add(GRIDSRNO)
            alParaval.Add(LOTNO)
            alParaval.Add(ITEMNAME)
            alParaval.Add(DESIGNNO)
            alParaval.Add(TOTALPCS)
            alParaval.Add(COLOR)
            alParaval.Add(URGENT)
            alParaval.Add(PCS)
            alParaval.Add(PROGISSDATE)
            alParaval.Add(STATUS)
            alParaval.Add(PRODCUTTING)
            alParaval.Add(FINISHCUTTING)
            alParaval.Add(INWARDDATE)
            alParaval.Add(GRNNO)
            alParaval.Add(GRNTYPE)
            alParaval.Add(RECDPCS)
            alParaval.Add(BARCODE)
            alParaval.Add(RATE)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(APPROVED)
            alParaval.Add(TXTPROCESSNAME.Text.Trim)
            alParaval.Add(TXTFINISHTYPE.Text.Trim)
            alParaval.Add(TXTFOLD.Text.Trim)
            alParaval.Add(KHADI)
            alParaval.Add(ABOVETOSCREEN)
            alParaval.Add(MISCRATE)
            alParaval.Add(RATETYPE)
            alParaval.Add(ADJMTRS)


            Dim CSRNO As String = ""
            Dim CCHGS As String = ""
            Dim CAMT As String = ""
            Dim CTAXID As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCHGS.Rows
                If row.Cells(0).Value <> Nothing Then
                    If CSRNO = "" Then
                        CSRNO = row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = row.Cells(ECHARGES.Index).Value.ToString
                        CAMT = row.Cells(EAMT.Index).Value.ToString
                        CTAXID = Val(row.Cells(ETAXID.Index).Value)

                    Else
                        CSRNO = CSRNO & "|" & row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = CCHGS & "|" & row.Cells(ECHARGES.Index).Value.ToString
                        CAMT = CAMT & "|" & row.Cells(EAMT.Index).Value.ToString
                        CTAXID = CTAXID & "|" & Val(row.Cells(ETAXID.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(CSRNO)
            alParaval.Add(CCHGS)
            alParaval.Add(CAMT)
            alParaval.Add(CTAXID)


            Dim OBJPROGRAM As New ClsProgramMaster()
            OBJPROGRAM.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = OBJPROGRAM.SAVE()
                If TXTSONO.Text.Trim = "" Then MsgBox("Details Added")
                TXTPROGRAMNO.Text = DTTABLE.Rows(0).Item(0)
            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(PROGRAMNO)
                Dim IntResult As Integer = OBJPROGRAM.UPDATE()
                MsgBox("Details Updated")
            End If

            If TXTSONO.Text.Trim = "" Then  PRINTREPORT()

            EDIT = False
            clear()
            PROGRAMDATE.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If MsgBox("Wish To Print Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim OBJPROG As New ProgramDesign
                OBJPROG.MdiParent = MDIMain
                OBJPROG.WHERECLAUSE = "{PROGRAMMASTER.PROGRAM_YEARID} = " & YearId & " And {PROGRAMMASTER.PROGRAM_no} = " & Val(TXTPROGRAMNO.Text.Trim)
                OBJPROG.Show()
            End If

            If ClientName = "AVIS" AndAlso MsgBox("Wish To Print Barcode?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                PRINTBARCODE()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTBARCODE()
        Try
            If ALLOWBARCODEPRINT = True Then

                Dim dirresults As String = ""
                'GET FRESH DATA FROM DATABASE (ONLY GRID)
                'THIS IS DONE COZ FOR MULTIUSER THE NOS WILL BE SAME
                'SO WE WILL ADD BARCODE IN SP AND THEN FETCH THAT DATA HERE AFTER THAT WE WILL PRINT BARCODES
                GRIDLOT.RowCount = 0
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" PROGRAMMASTER.PROGRAM_NO As PROGRAMNO, PROGRAMMASTER.PROGRAM_DATE As Date, ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(PROGRAMMASTER.PROGRAM_CARDRECDATE,'') AS CARDRECDATE, PROGRAMMASTER.PROGRAM_LBLTOTALPCS AS TOTALPCS, PROGRAMMASTER.PROGRAM_REMARKS AS REMARKS, PROGRAMMASTER.PROGRAM_DONE AS DONE, PROGRAMMASTER_DESC.PROGRAM_GRIDSRNO AS GRIDSRNO, PROGRAMMASTER_DESC.PROGRAM_LOTNO AS LOTNO, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, PROGRAMMASTER_DESC.PROGRAM_TOTALPCS AS GRIDTOTALPCS, COLORMASTER.COLOR_name AS COLOR, ISNULL(PROGRAMMASTER_DESC.PROGRAM_URGENT,0) AS URGENT, PROGRAMMASTER_DESC.PROGRAM_PCS AS PCS, ISNULL(PROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,'') AS PROGISSDATE, ISNULL(PROGRAMMASTER_DESC.PROGRAM_STATUS,'') AS STATUS, ISNULL(PROGRAMMASTER_DESC.PROGRAM_PRODCUTTING,'') AS PRODCUTTING, ISNULL(PROGRAMMASTER_DESC.PROGRAM_FINISHCUTTING,'') AS FINISHCUTTING, ISNULL(PROGRAMMASTER_DESC.PROGRAM_INWARDDATE,'') AS INWARDDATE, PROGRAMMASTER_DESC.PROGRAM_GRNNO AS GRNNO, PROGRAMMASTER_DESC.PROGRAM_GRNTYPE AS GRNTYPE, PROGRAMMASTER_DESC.PROGRAM_RECDPCS as RECDPCS, PROGRAMMASTER_DESC.PROGRAM_BARCODE as BARCODE ", "", " PROGRAMMASTER INNER JOIN PROGRAMMASTER_DESC ON PROGRAMMASTER.PROGRAM_NO = PROGRAMMASTER_DESC.PROGRAM_NO AND PROGRAMMASTER.PROGRAM_YEARID = PROGRAMMASTER_DESC.PROGRAM_YEARID LEFT OUTER JOIN LEDGERS ON PROGRAMMASTER.PROGRAM_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON PROGRAMMASTER_DESC.PROGRAM_ITEMID = ITEMMASTER.item_id INNER JOIN COLORMASTER ON PROGRAMMASTER_DESC.PROGRAM_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON PROGRAM_DESIGNID = DESIGN_ID", " AND PROGRAMMASTER.PROGRAM_NO = " & PROGRAMNO & " AND PROGRAMMASTER.PROGRAM_YEARID = " & YearId & " ORDER BY PROGRAMMASTER_DESC.PROGRAM_GRIDSRNO")
                For Each dr As DataRow In dttable.Rows
                        GRIDLOT.Rows.Add(Val(dr("GRIDSRNO")), dr("LOTNO"), dr("ITEMNAME"), dr("DESIGNNO"), Val(dr("GRIDTOTALPCS")), dr("COLOR"), dr("URGENT"), Val(dr("PCS")), dr("PROGISSDATE"), dr("STATUS"), dr("PRODCUTTING"), dr("FINISHCUTTING"), dr("INWARDDATE"), Val(dr("GRNNO")), dr("GRNTYPE"), Val(dr("RECDPCS")), dr("BARCODE"))
                    Next

                    If Val(TXTCOPIES.Text.Trim) = 0 Then TXTCOPIES.Text = 3

                    For Each ROW As DataGridViewRow In GRIDLOT.Rows
                        For COPIES As Integer = 1 To Val(TXTCOPIES.Text.Trim)

                            Dim oWrite As System.IO.StreamWriter
                            oWrite = File.CreateText("D:\Barcode.txt")

                            'TO PRINT BARCODE FROM SELECTED SRNO
                            If (Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0) Then
                                If Val(ROW.Cells(GSRNO.Index).Value) < Val(TXTFROM.Text.Trim) Or Val(ROW.Cells(GSRNO.Index).Value) > Val(TXTTO.Text.Trim) Then GoTo NEXTLINE
                            End If

                            If ClientName = "AVIS" Then
                                'Writing in file

                                oWrite.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>G0")
                                oWrite.WriteLine("n")
                                oWrite.WriteLine("M0500")
                                oWrite.WriteLine("O0214")
                                oWrite.WriteLine("V0")
                                oWrite.WriteLine("t1")
                                oWrite.WriteLine("Kf0070")
                                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.0 mm'></xpml>L")
                                oWrite.WriteLine("D11")
                                oWrite.WriteLine("A2")
                                oWrite.WriteLine("1W1D44000001200152,LA," & ROW.Cells(GBARCODE.Index).Value)
                                oWrite.WriteLine("ySPM")
                                oWrite.WriteLine("1911A0600020005" & ROW.Cells(GBARCODE.Index).Value)
                                oWrite.WriteLine("1911A0800340067" & ROW.Cells(GDESIGNNO.Index).Value)
                                oWrite.WriteLine("1911A0800170067" & ROW.Cells(GCOLOR.Index).Value)
                                oWrite.WriteLine("Q0001")
                                oWrite.WriteLine("E")
                                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                oWrite.Dispose()
                            End If


                            'Printing Barcode
                            Dim psi As New ProcessStartInfo()
                            psi.FileName = "cmd.exe"
                            psi.RedirectStandardInput = False
                            psi.RedirectStandardOutput = True
                            'psi.Arguments = "/c print " & Application.StartupPath & "\Barcode.txt"    ' specify your command
                            psi.Arguments = "/c print D:\Barcode.txt"    ' specify your command
                            psi.UseShellExecute = False

                            Dim proc As Process
                            proc = Process.Start(psi)
                            dirresults = proc.StandardOutput.ReadToEnd() ' // read from stdout
                            '// do something with result stream
                            proc.WaitForExit()
NEXTLINE:
                            'THIS LINE IS WRITTEN TO DISPOSE THE BARCODE NOTEPAD OBJECT, WHEN CURSOR COMES DIRECTLY ON NEXTLINE CODE
                            oWrite.Dispose()
                        Next
                    Next
                End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROGRAMMASTER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If ERRORVALID() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.KeyCode = Keys.OemQuotes Or e.KeyCode = Keys.OemPipe Then
            e.SuppressKeyPress = True
        ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
            TOOLPREVIOUS_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
            toolnext_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub PROGRAMMASTER_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            If ClientName = "SUPRIYA" Then ALLOWMANUALPROGNO = True
            fillcmb()
            fillcheckboxlist()
            CLEAR()

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

            Dim OBJCMN As New ClsCommon()
            Dim dttable As DataTable = OBJCMN.SEARCH(" PROGRAMMASTER.PROGRAM_NO AS PROGRAMNO, PROGRAMMASTER.PROGRAM_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(PROGRAMMASTER.PROGRAM_CARDRECDATE, '') AS CARDRECDATE, ISNULL(PROGRAMMASTER.PROGRAM_CARDISSUEDATE, '') AS CARDISSUEDATE, PROGRAMMASTER.PROGRAM_LBLTOTALPCS AS TOTALPCS, PROGRAMMASTER.PROGRAM_REMARKS AS REMARKS,  PROGRAMMASTER.PROGRAM_DONE AS DONE, PROGRAMMASTER_DESC.PROGRAM_GRIDSRNO AS GRIDSRNO, PROGRAMMASTER_DESC.PROGRAM_LOTNO AS LOTNO, ITEMMASTER.item_name AS ITEMNAME,  ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, PROGRAMMASTER_DESC.PROGRAM_TOTALPCS AS GRIDTOTALPCS, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,  ISNULL(PROGRAMMASTER_DESC.PROGRAM_URGENT, 0) AS URGENT, PROGRAMMASTER_DESC.PROGRAM_PCS AS PCS, ISNULL(PROGRAMMASTER_DESC.PROGRAM_KHADI, 0) AS KHADI, ISNULL(PROGRAMMASTER_DESC.PROGRAM_ABOVETOSCREEN, 0)  AS ABOVETOSCREEN, ISNULL(PROGRAMMASTER_DESC.PROGRAM_MISCRATE, 0) AS MISCRATE, ISNULL(PROGRAMMASTER_DESC.PROGRAM_RATETYPE, '') AS RATETYPE, ISNULL(PROGRAMMASTER_DESC.PROGRAM_PROGISSDATE, '') AS PROGISSDATE,  ISNULL(PROGRAMMASTER_DESC.PROGRAM_STATUS, '') AS STATUS, ISNULL(PROGRAMMASTER_DESC.PROGRAM_PRODCUTTING, '') AS PRODCUTTING, ISNULL(PROGRAMMASTER_DESC.PROGRAM_FINISHCUTTING, '')  AS FINISHCUTTING, ISNULL(PROGRAMMASTER_DESC.PROGRAM_INWARDDATE, '') AS INWARDDATE, PROGRAMMASTER_DESC.PROGRAM_GRNNO AS GRNNO, PROGRAMMASTER_DESC.PROGRAM_GRNTYPE AS GRNTYPE,  PROGRAMMASTER_DESC.PROGRAM_RECDPCS AS RECDPCS, PROGRAMMASTER_DESC.PROGRAM_BARCODE AS BARCODE, ISNULL(PROGRAMMASTER_DESC.PROGRAM_RATE, '') AS RATE,  ISNULL(PROGRAMMASTER_DESC.PROGRAM_APPROVED, 0) AS APPROVED, ISNULL(PROGRAMMASTER.PROGRAM_PROCESSNAME, '') AS PROCESSNAME, ISNULL(PROGRAMMASTER.PROGRAM_FINISHTYPE, '')  AS FINISHTYPE, ISNULL(PROGRAMMASTER.PROGRAM_FOLD, '') AS FOLD , ISNULL(PROGRAMMASTER_DESC.PROGRAM_ADJMTRS,0) AS ADJMTRS ", "", " PROGRAMMASTER INNER JOIN PROGRAMMASTER_DESC ON PROGRAMMASTER.PROGRAM_NO = PROGRAMMASTER_DESC.PROGRAM_NO AND PROGRAMMASTER.PROGRAM_YEARID = PROGRAMMASTER_DESC.PROGRAM_YEARID LEFT OUTER JOIN LEDGERS ON PROGRAMMASTER.PROGRAM_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON PROGRAMMASTER_DESC.PROGRAM_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON PROGRAMMASTER_DESC.PROGRAM_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON PROGRAMMASTER_DESC.PROGRAM_DESIGNID = DESIGNMASTER.DESIGN_id", " AND PROGRAMMASTER.PROGRAM_NO = " & PROGRAMNO & " AND PROGRAMMASTER.PROGRAM_YEARID = " & YearId & " ORDER BY PROGRAMMASTER_DESC.PROGRAM_GRIDSRNO")
            If dttable.Rows.Count > 0 Then

                For Each dr As DataRow In dttable.Rows
                    TXTPROGRAMNO.ReadOnly = True
                    TXTPROGRAMNO.Text = Val(dr("PROGRAMNO"))
                    PROGRAMDATE.Value = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                    CARDRECDATE.Text = dr("CARDRECDATE")
                    CARDISSUEDATE.Text = dr("CARDISSUEDATE")
                    CMBNAME.Text = dr("NAME")
                    If CMBNAME.Text.Trim <> "" Then CMBNAME.Enabled = False
                    txtremarks.Text = dr("REMARKS")
                    TXTPROCESSNAME.Text = dr("PROCESSNAME")
                    TXTFINISHTYPE.Text = dr("FINISHTYPE")
                    TXTFOLD.Text = dr("FOLD")

                    GRIDLOT.Rows.Add(Val(dr("GRIDSRNO")), dr("LOTNO"), dr("ITEMNAME"), dr("DESIGNNO"), Val(dr("GRIDTOTALPCS")), dr("COLOR"), dr("URGENT"), Val(dr("PCS")), dr("KHADI"), dr("ABOVETOSCREEN"), dr("MISCRATE"), dr("RATETYPE"), dr("PROGISSDATE"), dr("STATUS"), dr("PRODCUTTING"), dr("FINISHCUTTING"), dr("INWARDDATE"), Val(dr("GRNNO")), dr("GRNTYPE"), Val(dr("RECDPCS")), dr("BARCODE"), Val(dr("RATE")), dr("APPROVED"), 0)

                    If Val(dr("RECDPCS")) > 0 Then GRIDLOT.Rows(GRIDLOT.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow

                Next
                'CHARGES GRID
                Dim OBJCM2 As New ClsCommon
                Dim dt2 As DataTable = OBJCM2.SEARCH(" PROGRAMMASTER_CHGS.PROGRAM_CHGSGRIDSRNO AS CHGSGRIDSRNO, ISNULL(LEDGERS.Acc_cmpname, '') AS CHARGES, ISNULL(PROGRAMMASTER_CHGS.PROGRAM_AMT, 0) AS AMT, ISNULL(TAXMASTER.tax_id, 0) AS TAXID", "", " PROGRAMMASTER INNER JOIN PROGRAMMASTER_CHGS ON PROGRAMMASTER.PROGRAM_NO = PROGRAMMASTER_CHGS.PROGRAM_no AND PROGRAMMASTER.PROGRAM_YEARID = PROGRAMMASTER_CHGS.PROGRAM_yearid LEFT OUTER JOIN TAXMASTER ON PROGRAMMASTER_CHGS.PROGRAM_TAXID = TAXMASTER.tax_id AND PROGRAMMASTER_CHGS.PROGRAM_yearid = TAXMASTER.tax_yearid LEFT OUTER JOIN LEDGERS ON PROGRAMMASTER_CHGS.PROGRAM_CHARGESID = LEDGERS.Acc_id AND PROGRAMMASTER_CHGS.PROGRAM_yearid = LEDGERS.Acc_yearid", "AND PROGRAMMASTER_CHGS.PROGRAM_NO = " & PROGRAMNO & " AND PROGRAMMASTER_CHGS.PROGRAM_YEARID = " & YearId)
                If dt2.Rows.Count > 0 Then
                    For Each DTR As DataRow In dt2.Rows
                        GRIDCHGS.Rows.Add(DTR("CHGSGRIDSRNO"), DTR("CHARGES"), DTR("AMT"), DTR("TAXID"))
                    Next
                End If
                TOTAL()
            Else
                EDIT = False
                CLEAR()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBNAME.Text.Trim = "" Then
                If ClientName = "VINTAGEINDIA" Then FILLNAME(CMBNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') and ACC_TYPE = 'ACCOUNTS'") Else FILLNAME(CMBNAME, EDIT, " And GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If
            fillitemname(CMBITEMNAME, "")
            FILLDESIGN(CMBDESIGNNO, CMBITEMNAME.Text)
            FILLCOLOR(CMBCOLOR, CMBDESIGNNO.Text, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        If GRIDDOUBLECLICK = False Then

            If EDIT = True Then
                'GET LAST BARCODE SRNO
                Dim LSRNO As Integer = 0
                Dim RSRNO As Integer = 0
                Dim SNO As Integer = 0
                LSRNO = InStr(GRIDLOT.Rows(GRIDLOT.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                RSRNO = InStr(LSRNO + 1, GRIDLOT.Rows(GRIDLOT.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                SNO = GRIDLOT.Rows(GRIDLOT.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                TXTBARCODE.Text = "PG-" & Val(TXTPROGRAMNO.Text.Trim) & "/" & SNO + 1 & "/" & YearId
            Else
                TXTBARCODE.Text = "PG-" & Val(TXTPROGRAMNO.Text.Trim) & "/" & GRIDLOT.RowCount + 1 & "/" & YearId
            End If


            'GETRATE FROM DYEINGPRICELIST
            Dim RATE As Double = 0.0
            Dim OBJCMN As New ClsCommon

            Dim TWHERE As String = ""
            'FOR YASHVI FETCH RATES WITH RESPECT TO DYEDTYPE
            If ClientName = "YASHVI" Then
                Dim DTT As New DataTable
                If TXTGRNTYPE.Text.Trim = "OPENING" Then DTT = OBJCMN.SEARCH(" ISNULL(DYEDTYPEMASTER.DYEDTYPE_NAME,'') AS DYEDTYPE ", "", " STOCKMASTER_DYEING LEFT OUTER JOIN DYEDTYPEMASTER ON STOCKMASTER_DYEING.DYEDTYPEID = DYEDTYPEMASTER.DYEDTYPE_ID INNER JOIN LEDGERS ON STOCKMASTER_DYEING.JOBBERID = LEDGERS.ACC_ID", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND STOCKMASTER_DYEING.LOTNO = '" & CMBLOTNO.Text.Trim & "' AND STOCKMASTER_DYEING.YEARID = " & YearId) Else DTT = OBJCMN.SEARCH(" ISNULL(DYEDTYPEMASTER.DYEDTYPE_NAME,'') AS DYEDTYPE ", "", " GRN LEFT OUTER JOIN DYEDTYPEMASTER ON GRN.GRN_DYEINGTYPEID = DYEDTYPEMASTER.DYEDTYPE_ID INNER JOIN LEDGERS ON GRN.GRN_TOLEDGERID = LEDGERS.ACC_ID", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND GRN.GRN_PLOTNO = '" & CMBLOTNO.Text.Trim & "' AND GRN.GRN_YEARID = " & YearId)
                If DTT.Rows.Count > 0 Then TWHERE = " AND ISNULL(DYEDTYPEMASTER.DYEDTYPE_NAME,'') = '" & DTT.Rows(0).Item("DYEDTYPE") & "'"
            End If

            'If ClientName = "VINTAGEINDIA" Then
            '    Dim DTT As New DataTable
            '    If TXTGRNTYPE.Text.Trim = "OPENING" Then DTT = OBJCMN.SEARCH(" ISNULL(DYEDTYPEMASTER.DYEDTYPE_NAME,'') AS DYEDTYPE ", "", " STOCKMASTER_DYEING LEFT OUTER JOIN DYEDTYPEMASTER ON STOCKMASTER_DYEING.DYEDTYPEID = DYEDTYPEMASTER.DYEDTYPE_ID INNER JOIN LEDGERS ON STOCKMASTER_DYEING.JOBBERID = LEDGERS.ACC_ID", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND STOCKMASTER_DYEING.LOTNO = '" & CMBLOTNO.Text.Trim & "' AND STOCKMASTER_DYEING.YEARID = " & YearId) Else DTT = OBJCMN.SEARCH(" ISNULL(DYEDTYPEMASTER.DYEDTYPE_NAME,'') AS DYEDTYPE ", "", " GRN LEFT OUTER JOIN DYEDTYPEMASTER ON GRN.GRN_DYEINGTYPEID = DYEDTYPEMASTER.DYEDTYPE_ID INNER JOIN LEDGERS ON GRN.GRN_TOLEDGERID = LEDGERS.ACC_ID", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND GRN.GRN_PLOTNO = '" & CMBLOTNO.Text.Trim & "' AND GRN.GRN_YEARID = " & YearId)
            '    If DTT.Rows.Count > 0 Then TWHERE = " AND ISNULL(DYEDTYPEMASTER.DYEDTYPE_NAME,'') = '" & DTT.Rows(0).Item("DYEDTYPE") & "'"
            'End If

            Dim DTRATE As DataTable = OBJCMN.SEARCH("(CASE WHEN '" & CMBRATETYPE.Text.Trim & "' = 'White' THEN PL_WHITE ELSE (CASE WHEN '" & CMBRATETYPE.Text.Trim & "' = 'Cream' THEN PL_CREAM ELSE (CASE WHEN '" & CMBRATETYPE.Text.Trim & "' = 'Light' THEN PL_LIGHT ELSE (CASE WHEN '" & CMBRATETYPE.Text.Trim & "' = 'Medium' THEN PL_MEDIUM ELSE (CASE WHEN '" & CMBRATETYPE.Text.Trim & "' = 'Dark' THEN PL_DARK ELSE (CASE WHEN '" & CMBRATETYPE.Text.Trim & "' = 'Extra Dark' THEN PL_EXTRADARK ELSE (CASE WHEN '" & CMBRATETYPE.Text.Trim & "' = 'Rainbow' THEN PL_RAINBOW ELSE (CASE WHEN '" & CMBRATETYPE.Text.Trim & "' = 'Procian White' THEN PL_PROCWHITE ELSE (CASE WHEN '" & CMBRATETYPE.Text.Trim & "' = 'Procian Dyed' THEN PL_PROCDYED ELSE (CASE WHEN '" & CMBRATETYPE.Text.Trim & "' = 'Discharge' THEN PL_DISCHARGE ELSE 0 END) END) END) END) END) END) END) END) END) END) AS RATE, PL_KHADI AS KHADI, PL_ABOVETWOSCREEN AS ABOVE2SCREEN, PL_MISCRATE AS MISCRATE", "", " DYEINGPRICELIST INNER JOIN DYEINGPRICELIST_DESC ON DYEINGPRICELIST.PL_NO = DYEINGPRICELIST_DESC.PL_NO And DYEINGPRICELIST.PL_YEARID = DYEINGPRICELIST_DESC.PL_YEARID INNER JOIN LEDGERS ON DYEINGPRICELIST.PL_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN DYEDTYPEMASTER ON DYEINGPRICELIST_DESC.PL_TYPEID = DYEDTYPEMASTER.DYEDTYPE_ID", " And LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND DYEINGPRICELIST.PL_YEARID = " & YearId & TWHERE)

            If DTRATE.Rows.Count > 0 Then RATE = Val(DTRATE.Rows(0).Item("RATE"))
            If CHKKHADI.Checked = True Then RATE += Val(DTRATE.Rows(0).Item("KHADI"))
            If CHKABOVETOSCREEN.Checked = True Then RATE += Val(DTRATE.Rows(0).Item("ABOVE2SCREEN"))
            If CHKMISCRATE.Checked = True Then RATE += Val(DTRATE.Rows(0).Item("MISCRATE"))


            GRIDLOT.Rows.Add(Val(TXTSRNO.Text.Trim), CMBLOTNO.Text.Trim, CMBITEMNAME.Text.Trim, CMBDESIGNNO.Text.Trim, Val(TXTTOTALPCS.Text.Trim), CMBCOLOR.Text.Trim, CHKURGENT.Checked, Val(TXTPCS.Text.Trim), CHKKHADI.Checked, CHKABOVETOSCREEN.Checked, CHKMISCRATE.Checked, CMBRATETYPE.Text.Trim, "", "", "", "", "", Val(TXTGRNNO.Text.Trim), TXTGRNTYPE.Text.Trim, 0, TXTBARCODE.Text.Trim, Val(RATE), CHKAPPROVED.Checked, 0)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDLOT.Item(GLOTNO.Index, TEMPROW).Value = CMBLOTNO.Text.Trim
            GRIDLOT.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
            GRIDLOT.Item(GDESIGNNO.Index, TEMPROW).Value = CMBDESIGNNO.Text.Trim
            GRIDLOT.Item(GTOTALPCS.Index, TEMPROW).Value = Val(TXTTOTALPCS.Text.Trim)
            GRIDLOT.Item(GCOLOR.Index, TEMPROW).Value = CMBCOLOR.Text.Trim
            GRIDLOT.Item(GURGENT.Index, TEMPROW).Value = CHKURGENT.Checked
            GRIDLOT.Item(GPCS.Index, TEMPROW).Value = Val(TXTPCS.Text.Trim)
            GRIDLOT.Item(GKHADI.Index, TEMPROW).Value = CHKKHADI.Checked
            GRIDLOT.Item(GABOVETOSCREEN.Index, TEMPROW).Value = CHKABOVETOSCREEN.Checked
            GRIDLOT.Item(GMISCRATE.Index, TEMPROW).Value = CHKMISCRATE.Checked
            GRIDLOT.Item(GRATETYPE.Index, TEMPROW).Value = CMBRATETYPE.Text.Trim
            GRIDLOT.Item(GGRNNO.Index, TEMPROW).Value = Val(TXTGRNNO.Text.Trim)
            GRIDLOT.Item(GGRNTYPE.Index, TEMPROW).Value = TXTGRNTYPE.Text.Trim
            GRIDLOT.Item(GAPPROVED.Index, TEMPROW).Value = CHKAPPROVED.Checked

            GRIDDOUBLECLICK = False
        End If

        TOTAL()
        getsrno(GRIDLOT)
        GRIDLOT.FirstDisplayedScrollingRowIndex = GRIDLOT.RowCount - 1

        If ClientName <> "AVIS" Then
            CMBITEMNAME.Text = ""
            CMBDESIGNNO.Text = ""
        End If
        CMBCOLOR.Text = ""
        TXTTOTALPCS.Clear()
        CHKURGENT.CheckState = CheckState.Unchecked
        TXTPCS.Clear()
        TXTGRNNO.Clear()
        TXTGRNTYPE.Clear()
        TXTBARCODE.Clear()
        TXTSRNO.Text = GRIDLOT.RowCount + 1
        CMBLOTNO.Focus()
        CHKKHADI.CheckState = CheckState.Unchecked
        CHKABOVETOSCREEN.CheckState = CheckState.Unchecked
        CHKMISCRATE.CheckState = CheckState.Unchecked
        CMBRATETYPE.Text = ""

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

    Private Sub GRIDLOT_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDLOT.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub TXTPCS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPCS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Dim IntResult As Integer
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                For Each ROW As DataGridViewRow In GRIDLOT.Rows
                    If Val(ROW.Cells(GRECDPCS.Index).Value) > 0 Then
                        MsgBox("Unable To Delete Entry Locked", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                Next

                If MsgBox("Delete Program?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim alParaval As New ArrayList
                alParaval.Add(PROGRAMNO)
                alParaval.Add(YearId)

                Dim OBJPROGRAM As New ClsProgramMaster()
                OBJPROGRAM.alParaval = alParaval
                IntResult = OBJPROGRAM.DELETE()
                MsgBox("Program Deleted")
                EDIT = False
                clear()

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDLOT.CurrentRow.Index >= 0 And GRIDLOT.Item(GITEMNAME.Index, GRIDLOT.CurrentRow.Index).Value <> Nothing Then

                If Val(GRIDLOT.Item(GRECDPCS.Index, GRIDLOT.CurrentRow.Index).Value) > 0 Then
                    MsgBox("Unable To Modify Entry Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = Val(GRIDLOT.Item(GSRNO.Index, GRIDLOT.CurrentRow.Index).Value)
                CMBLOTNO.Text = GRIDLOT.Item(GLOTNO.Index, GRIDLOT.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Text = GRIDLOT.Item(GITEMNAME.Index, GRIDLOT.CurrentRow.Index).Value
                CMBDESIGNNO.Text = GRIDLOT.Item(GDESIGNNO.Index, GRIDLOT.CurrentRow.Index).Value
                TXTTOTALPCS.Text = Val(GRIDLOT.Item(GTOTALPCS.Index, GRIDLOT.CurrentRow.Index).Value)
                CMBCOLOR.Text = GRIDLOT.Item(GCOLOR.Index, GRIDLOT.CurrentRow.Index).Value
                CHKURGENT.Checked = Convert.ToBoolean(GRIDLOT.Item(GURGENT.Index, GRIDLOT.CurrentRow.Index).Value)
                TXTPCS.Text = Val(GRIDLOT.Item(GPCS.Index, GRIDLOT.CurrentRow.Index).Value)
                CHKKHADI.Checked = Convert.ToBoolean(GRIDLOT.Item(GKHADI.Index, GRIDLOT.CurrentRow.Index).Value)
                CHKABOVETOSCREEN.Checked = Convert.ToBoolean(GRIDLOT.Item(GABOVETOSCREEN.Index, GRIDLOT.CurrentRow.Index).Value)
                CHKMISCRATE.Checked = Convert.ToBoolean(GRIDLOT.Item(GMISCRATE.Index, GRIDLOT.CurrentRow.Index).Value)
                CMBRATETYPE.Text = GRIDLOT.Item(GRATETYPE.Index, GRIDLOT.CurrentRow.Index).Value
                TXTGRNNO.Text = Val(GRIDLOT.Item(GGRNNO.Index, GRIDLOT.CurrentRow.Index).Value)
                TXTGRNTYPE.Text = GRIDLOT.Item(GGRNTYPE.Index, GRIDLOT.CurrentRow.Index).Value
                CHKAPPROVED.Checked = Convert.ToBoolean(GRIDLOT.Item(GAPPROVED.Index, GRIDLOT.CurrentRow.Index).Value)

                TEMPROW = GRIDLOT.CurrentRow.Index
                CMBLOTNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDLOT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDLOT.KeyDown

        Try
            If e.KeyCode = Keys.Delete And GRIDLOT.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row Is In Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                If Val(GRIDLOT.Item(GRECDPCS.Index, GRIDLOT.CurrentRow.Index).Value) > 0 Then
                    MsgBox("Unable to Delete Entry Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                'end of block
                GRIDLOT.Rows.RemoveAt(GRIDLOT.CurrentRow.Index)
                TOTAL()
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCOLOR.Enter
        Try
            FILLCOLOR(CMBCOLOR, CMBDESIGNNO.Text, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCOLOR.Validating
        Try
            If CMBCOLOR.Text.Trim <> "" Then COLORVALIDATE(CMBCOLOR, e, Me, CMBDESIGNNO.Text, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then
                If ClientName = "VINTAGEINDIA" Then FILLNAME(CMBNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE = 'ACCOUNTS'") Else FILLNAME(CMBNAME, EDIT, " And GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then
                If ClientName = "VINTAGEINDIA" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS')", "Sundry Creditors", "ACCOUNTS") Else NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If ClientName = "VINTAGEINDIA" Then
                    OBJLEDGER.STRSEARCH = " and (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                Else
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                End If
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" And EDIT = False Then
                FILLLOTNO()

                'GET CHARGHES GRID FROM DYEINGPROCELIST
                Dim OBJCM2 As New ClsCommon
                Dim dt2 As DataTable = OBJCM2.SEARCH(" DYEINGPRICELIST_CHGS.PL_CHGSGRIDSRNO AS CHGSGRIDSRNO, ISNULL(CHGSLEDGERS.Acc_cmpname, '') AS CHARGES, ISNULL(DYEINGPRICELIST_CHGS.PL_AMT, 0) AS AMT, ISNULL(TAXMASTER.tax_id, 0) AS TAXID", "", " DYEINGPRICELIST INNER JOIN DYEINGPRICELIST_CHGS ON DYEINGPRICELIST.PL_NO = DYEINGPRICELIST_CHGS.PL_no AND DYEINGPRICELIST.PL_YEARID = DYEINGPRICELIST_CHGS.PL_yearid LEFT OUTER JOIN TAXMASTER ON DYEINGPRICELIST_CHGS.PL_TAXID = TAXMASTER.tax_id LEFT OUTER JOIN LEDGERS AS CHGSLEDGERS ON DYEINGPRICELIST_CHGS.PL_CHARGESID = CHGSLEDGERS.Acc_id INNER JOIN LEDGERS ON DYEINGPRICELIST.PL_LEDGERID = LEDGERS.ACC_ID ", "AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND DYEINGPRICELIST.PL_YEARID = " & YearId)
                If dt2.Rows.Count > 0 Then
                    GRIDCHGS.RowCount = 0
                    For Each DTR As DataRow In dt2.Rows
                        GRIDCHGS.Rows.Add(DTR("CHGSGRIDSRNO"), DTR("CHARGES"), DTR("AMT"), DTR("TAXID"))
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLLOTNO()
        Try
            CMBLOTNO.Items.Clear()
            If CMBNAME.Text.Trim <> "" Then

                ''FILLLOTNO
                Dim OBJCMN As New ClsCommon
                'WE HAVE CHANGED THE WORKING, IT WAS ON PROGRAM DONE WE HAVE CHANGED TO MTRS - PROGRAMMTRS 
                'Dim DT As DataTable = OBJCMN.SEARCH("*", "", " (SELECT CHECKINGMASTER.CHECK_LOTNO AS LOTNO FROM CHECKINGMASTER INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id INNER JOIN GRN ON GRN.GRN_NO = CHECKINGMASTER.CHECK_GRNNO AND GRN.GRN_TYPE = CHECKINGMASTER.CHECK_TYPE AND GRN.GRN_YEARID = CHECKINGMASTER.CHECK_YEARID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = CHECKINGMASTER.CHECK_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = CHECKINGMASTER.CHECK_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(GRN.GRN_PROGRAMDONE,0) = 0 AND GRN.GRN_PLOTNO <> '' AND GRN.GRN_YEARID = " & YearId & " UNION ALL SELECT STOCKMASTER.SM_LOTNO AS LOTNO FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = STOCKMASTER.SM_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = STOCKMASTER.SM_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTCOMPLETED = 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(STOCKMASTER.SM_PROGRAMDONE,0) = 0 AND SM_TYPE = 'JOBBERSTOCK' AND STOCKMASTER.SM_LOTNO <> '' AND STOCKMASTER.SM_YEARID = " & YearId & ") AS T", "")

                Dim DT As New DataTable

                'FOR VINTAGE GET DATA FROM FINISHED GRN
                If ClientName = "VINTAGEINDIA" Then
                    DT = OBJCMN.SEARCH(" GRN.GRN_PLOTNO AS LOTNO ", "", " GRN INNER JOIN LEDGERS ON GRN.GRN_LEDGERID = LEDGERS.ACC_ID", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(GRN.GRN_PROGRAMDONE,0) = 0 AND (GRN.GRN_TOTALQTY - ISNULL(GRN.GRN_PROGRAMMTRS,0)) > 0 AND GRN.GRN_PLOTNO <> '' AND GRN.GRN_YEARID = " & YearId & " ORDER BY GRN.GRN_NO")

                    'KCRAYON WORKS ON PCS
                ElseIf LOTSTATUSONMTRS = False And ClientName <> "KARAN" Then
                    'DT = OBJCMN.SEARCH("*", "", " (SELECT CHECKINGMASTER.CHECK_LOTNO AS LOTNO FROM CHECKINGMASTER INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id INNER JOIN GRN ON GRN.GRN_NO = CHECKINGMASTER.CHECK_GRNNO AND GRN.GRN_TYPE = CHECKINGMASTER.CHECK_TYPE AND GRN.GRN_YEARID = CHECKINGMASTER.CHECK_YEARID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = CHECKINGMASTER.CHECK_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = CHECKINGMASTER.CHECK_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(GRN.GRN_PROGRAMDONE,0) = 0 AND (GRN.GRN_TOTALQTY - ISNULL(GRN.GRN_PROGRAMMTRS,0)) > 0 AND GRN.GRN_PLOTNO <> '' AND GRN.GRN_YEARID = " & YearId & " UNION ALL SELECT STOCKMASTER.SM_LOTNO AS LOTNO FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = STOCKMASTER.SM_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = STOCKMASTER.SM_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTCOMPLETED = 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(STOCKMASTER.SM_PROGRAMDONE,0) = 0 AND (STOCKMASTER.SM_PCS - ISNULL(STOCKMASTER.SM_PROGRAMMTRS,0)) > 0 AND SM_TYPE = 'JOBBERSTOCK' AND STOCKMASTER.SM_LOTNO <> '' AND STOCKMASTER.SM_LOTNO <> '0' AND STOCKMASTER.SM_YEARID = " & YearId & ") AS T", "")
                    DT = OBJCMN.SEARCH("*", "", " (SELECT LOT_VIEW.LOTNO FROM GRN INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = GRN.GRN_PLOTNO AND LOT_VIEW.JOBBERLEDGERID = GRN.GRN_TOLEDGERID AND LOT_VIEW.YEARID = GRN.GRN_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 WHERE JOBBERNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(GRN.GRN_PROGRAMDONE,0) = 0 AND (GRN.GRN_TOTALQTY - ISNULL(GRN.GRN_PROGRAMMTRS,0)) > 0 AND GRN.GRN_PLOTNO <> '' AND GRN.GRN_YEARID = " & YearId & " UNION ALL SELECT LOT_VIEW.LOTNO FROM STOCKMASTER INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = STOCKMASTER.SM_LOTNO AND LOT_VIEW.JOBBERLEDGERID = STOCKMASTER.SM_LEDGERIDTO AND LOT_VIEW.YEARID = STOCKMASTER.SM_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 WHERE JOBBERNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(STOCKMASTER.SM_PROGRAMDONE,0) = 0 AND (STOCKMASTER.SM_PCS - ISNULL(STOCKMASTER.SM_PROGRAMMTRS,0)) > 0 AND SM_TYPE = 'JOBBERSTOCK' AND STOCKMASTER.SM_LOTNO <> '' AND STOCKMASTER.SM_LOTNO <> '0' AND STOCKMASTER.SM_YEARID = " & YearId & ") AS T", "")
                ElseIf ClientName = "KARAN" Then
                    DT = OBJCMN.SEARCH("*", "", " (SELECT CHECKINGMASTER.CHECK_LOTNO AS LOTNO FROM CHECKINGMASTER INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id INNER JOIN GRN ON GRN.GRN_NO = CHECKINGMASTER.CHECK_GRNNO AND GRN.GRN_TYPE = CHECKINGMASTER.CHECK_TYPE AND GRN.GRN_YEARID = CHECKINGMASTER.CHECK_YEARID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = CHECKINGMASTER.CHECK_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = CHECKINGMASTER.CHECK_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(GRN.GRN_PROGRAMDONE,0) = 0 AND (GRN.GRN_TOTALMTRS - ISNULL(GRN.GRN_PROGRAMMTRS,0)) > 0 AND GRN.GRN_PLOTNO <> '' AND GRN.GRN_YEARID = " & YearId & " UNION SELECT JOBOUT.JO_LOTNO AS LOTNO FROM JOBOUT INNER JOIN LEDGERS ON JOBOUT.JO_ledgerid = LEDGERS.Acc_id INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = JOBOUT.JO_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.Acc_cmpname AND LOT_VIEW.YEARID = JOBOUT.JO_yearid AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(JOBOUT.JO_PROGRAMDONE,0) = 0 AND (JOBOUT.JO_TOTALMTRS - ISNULL(JOBOUT.JO_PROGRAMMTRS,0)) > 0 AND JOBOUT.JO_LOTNO <> '' AND JOBOUT.JO_YEARID = " & YearId & " UNION ALL SELECT STOCKMASTER.SM_LOTNO AS LOTNO FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = STOCKMASTER.SM_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = STOCKMASTER.SM_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTCOMPLETED = 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(STOCKMASTER.SM_PROGRAMDONE,0) = 0 AND (STOCKMASTER.SM_MTRS - ISNULL(STOCKMASTER.SM_PROGRAMMTRS,0)) > 0 AND SM_TYPE = 'JOBBERSTOCK' AND STOCKMASTER.SM_LOTNO <> '' AND STOCKMASTER.SM_LOTNO <> '0' AND STOCKMASTER.SM_YEARID = " & YearId & ") AS T", "")
                Else
                    DT = OBJCMN.SEARCH("*", "", " (SELECT CHECKINGMASTER.CHECK_LOTNO AS LOTNO FROM CHECKINGMASTER INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id INNER JOIN GRN ON GRN.GRN_NO = CHECKINGMASTER.CHECK_GRNNO AND GRN.GRN_TYPE = CHECKINGMASTER.CHECK_TYPE AND GRN.GRN_YEARID = CHECKINGMASTER.CHECK_YEARID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = CHECKINGMASTER.CHECK_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = CHECKINGMASTER.CHECK_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(GRN.GRN_PROGRAMDONE,0) = 0 AND (GRN.GRN_TOTALMTRS - ISNULL(GRN.GRN_PROGRAMMTRS,0)) > 0 AND GRN.GRN_PLOTNO <> '' AND GRN.GRN_YEARID = " & YearId & " UNION ALL SELECT STOCKMASTER.SM_LOTNO AS LOTNO FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = STOCKMASTER.SM_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = STOCKMASTER.SM_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTCOMPLETED = 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(STOCKMASTER.SM_PROGRAMDONE,0) = 0 AND (STOCKMASTER.SM_MTRS - ISNULL(STOCKMASTER.SM_PROGRAMMTRS,0)) > 0 AND SM_TYPE = 'JOBBERSTOCK' AND STOCKMASTER.SM_LOTNO <> '' AND STOCKMASTER.SM_LOTNO <> '0' AND STOCKMASTER.SM_YEARID = " & YearId & ") AS T", "")
                End If

                If DT.Rows.Count > 0 Then
                    For Each DTROW As DataRow In DT.Rows
                        CMBLOTNO.Items.Add(DTROW("LOTNO"))
                    Next
                    CMBNAME.Enabled = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOTNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBLOTNO.Validating
        Try
            If CMBLOTNO.Text.Trim <> "" And CMBNAME.Text.Trim <> "" And GRIDDOUBLECLICK = False Then
                'GET LOT DETAILS
                Dim OBJCMN As New ClsCommon

                'WE HAVE CHANGED THE WORKING, IT WAS ON PROGRAM DONE WE HAVE CHANGED TO MTRS - PROGRAMMTRS 
                'Dim DT As DataTable = OBJCMN.SEARCH("*", "", "(SELECT CHECKINGMASTER.CHECK_BALPCS As LOTPCS, CHECKINGMASTER.CHECK_BALMTRS As LOTMTRS, ItemMaster.ITEM_name As ITEMNAME, ISNULL(DesignMaster.DESIGN_NO,'') AS DESIGNNO, CHECKINGMASTER.CHECK_GRNNO AS GRNNO, CHECKINGMASTER.CHECK_TYPE AS GRNTYPE, LOT_VIEW.TOTALWT AS QUALITYWT FROM CHECKINGMASTER INNER JOIN ITEMMASTER ON CHECKINGMASTER.CHECK_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON CHECK_DESIGNID = DESIGN_ID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = CHECKINGMASTER.CHECK_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = CHECKINGMASTER.CHECK_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "'  AND CHECK_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND CHECK_YEARID = " & YearId & " UNION ALL SELECT STOCKMASTER.SM_PCS As LOTPCS, STOCKMASTER.SM_MTRS As LOTMTRS, ItemMaster.ITEM_name As ITEMNAME, ISNULL(DesignMaster.DESIGN_NO,'') AS DESIGNNO, STOCKMASTER.SM_NO AS GRNNO, 'OPENING' AS GRNTYPE, LOT_VIEW.TOTALWT AS QUALITYWT FROM STOCKMASTER INNER JOIN ITEMMASTER ON STOCKMASTER.SM_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON STOCKMASTER.SM_DESIGNID = DESIGN_ID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = STOCKMASTER.SM_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = STOCKMASTER.SM_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTCOMPLETED = 0  WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND STOCKMASTER.SM_TYPE = 'JOBBERSTOCK' AND STOCKMASTER.SM_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND STOCKMASTER.SM_YEARID = " & YearId & ") AS T", "")
                Dim DT As New DataTable


                If ClientName = "VINTAGEINDIA" Then
                    DT = OBJCMN.SEARCH(" TOP 1 0 As LOTPCS, GRN.GRN_TOTALMTRS As LOTMTRS, ItemMaster.ITEM_name As ITEMNAME, ISNULL(DesignMaster.DESIGN_NO,'') AS DESIGNNO, GRN.GRN_NO AS GRNNO, GRN.GRN_TYPE AS GRNTYPE, GRN.GRN_TOTALWT AS QUALITYWT  ", "", " GRN INNER JOIN GRN_DESC ON GRN.GRN_NO = GRN_DESC.GRN_NO AND GRN.GRN_TYPE = GRN_DESC.GRN_GRIDTYPE AND GRN.GRN_YEARID = GRN_DESC.GRN_YEARID INNER JOIN ITEMMASTER ON GRN_DESC.GRN_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON GRN.GRN_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON GRN_DESC.GRN_DESIGNID = DESIGN_ID AND GRN.GRN_PROGRAMDONE = 0 AND (GRN_TOTALMTRS - GRN_PROGRAMMTRS) > 0 ", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND GRN.GRN_PLOTNO = '" & CMBLOTNO.Text.Trim & "' AND GRN.GRN_YEARID = " & YearId)
                ElseIf LOTSTATUSONMTRS = False And ClientName <> "KARAN" Then
                    DT = OBJCMN.SEARCH("*", "", "(SELECT CHECKINGMASTER.CHECK_BALPCS As LOTPCS, CHECKINGMASTER.CHECK_BALMTRS As LOTMTRS, ItemMaster.ITEM_name As ITEMNAME, ISNULL(DesignMaster.DESIGN_NO,'') AS DESIGNNO, CHECKINGMASTER.CHECK_GRNNO AS GRNNO, CHECKINGMASTER.CHECK_TYPE AS GRNTYPE, LOT_VIEW.TOTALWT AS QUALITYWT FROM CHECKINGMASTER INNER JOIN ITEMMASTER ON CHECKINGMASTER.CHECK_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON CHECK_DESIGNID = DESIGN_ID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = CHECKINGMASTER.CHECK_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = CHECKINGMASTER.CHECK_YEARID AND LOT_VIEW.BALPCS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 AND LOT_VIEW.PROGRAMDONE = 0 AND (TOTALPCS - PROGRAMMTRS) > 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "'  AND CHECK_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND CHECK_YEARID = " & YearId & " UNION ALL SELECT STOCKMASTER.SM_PCS As LOTPCS, STOCKMASTER.SM_MTRS As LOTMTRS, ItemMaster.ITEM_name As ITEMNAME, ISNULL(DesignMaster.DESIGN_NO,'') AS DESIGNNO, STOCKMASTER.SM_NO AS GRNNO, 'OPENING' AS GRNTYPE, LOT_VIEW.TOTALWT AS QUALITYWT FROM STOCKMASTER INNER JOIN ITEMMASTER ON STOCKMASTER.SM_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON STOCKMASTER.SM_DESIGNID = DESIGN_ID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = STOCKMASTER.SM_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = STOCKMASTER.SM_YEARID AND LOT_VIEW.BALPCS > 0 AND LOT_VIEW.LOTCOMPLETED = 0 AND LOT_VIEW.PROGRAMDONE = 0 AND (TOTALPCS - PROGRAMMTRS) > 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND STOCKMASTER.SM_TYPE = 'JOBBERSTOCK' AND STOCKMASTER.SM_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND STOCKMASTER.SM_YEARID = " & YearId & ") AS T", "")
                ElseIf ClientName = "KARAN" Then
                    DT = OBJCMN.SEARCH("*", "", "(SELECT CHECKINGMASTER.CHECK_BALPCS As LOTPCS, CHECKINGMASTER.CHECK_BALMTRS As LOTMTRS, ItemMaster.ITEM_name As ITEMNAME, ISNULL(DesignMaster.DESIGN_NO,'') AS DESIGNNO, CHECKINGMASTER.CHECK_GRNNO AS GRNNO, CHECKINGMASTER.CHECK_TYPE AS GRNTYPE, LOT_VIEW.TOTALWT AS QUALITYWT FROM CHECKINGMASTER INNER JOIN ITEMMASTER ON CHECKINGMASTER.CHECK_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON CHECK_DESIGNID = DESIGN_ID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = CHECKINGMASTER.CHECK_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = CHECKINGMASTER.CHECK_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 AND LOT_VIEW.PROGRAMDONE = 0 AND (TOTALMTRS - PROGRAMMTRS) > 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "'  AND CHECK_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND CHECK_YEARID = " & YearId & " UNION ALL SELECT JOBOUT_DESC.JO_PCS AS LOTPCS, JOBOUT_DESC.JO_MTRS AS LOTMTRS, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, JOBOUT_DESC.JO_FROMTYPE AS GRNNO, JOBOUT_DESC.JO_FROMTYPE AS GRNTYPE, LOT_VIEW.TOTALWT AS QUALITYWT FROM JOBOUT INNER JOIN LEDGERS ON JOBOUT.JO_ledgerid = LEDGERS.Acc_id INNER JOIN LOT_VIEW ON LOT_VIEW.JOBBERNAME = LEDGERS.Acc_cmpname AND LOT_VIEW.YEARID = JOBOUT.JO_yearid AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 AND  LOT_VIEW.PROGRAMDONE = 0 AND LOT_VIEW.TOTALMTRS - LOT_VIEW.PROGRAMMTRS > 0 INNER JOIN JOBOUT_DESC ON JOBOUT.JO_no = JOBOUT_DESC.JO_NO AND JOBOUT.JO_yearid = JOBOUT_DESC.JO_YEARID AND LOT_VIEW.LOTNO = JOBOUT_DESC.JO_GRIDLOTNO INNER JOIN ITEMMASTER ON JOBOUT_DESC.JO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON JOBOUT_DESC.JO_QUALITYID = DESIGNMASTER.DESIGN_id  WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "'  AND JO_GRIDLOTNO = '" & CMBLOTNO.Text.Trim & "' AND JO_YEARID = " & YearId & " UNION ALL SELECT STOCKMASTER.SM_PCS As LOTPCS, STOCKMASTER.SM_MTRS As LOTMTRS, ItemMaster.ITEM_name As ITEMNAME, ISNULL(DesignMaster.DESIGN_NO,'') AS DESIGNNO, STOCKMASTER.SM_NO AS GRNNO, 'OPENING' AS GRNTYPE, LOT_VIEW.TOTALWT AS QUALITYWT FROM STOCKMASTER INNER JOIN ITEMMASTER ON STOCKMASTER.SM_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON STOCKMASTER.SM_DESIGNID = DESIGN_ID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = STOCKMASTER.SM_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = STOCKMASTER.SM_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTCOMPLETED = 0 AND LOT_VIEW.PROGRAMDONE = 0 AND (TOTALMTRS - PROGRAMMTRS) > 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND STOCKMASTER.SM_TYPE = 'JOBBERSTOCK' AND STOCKMASTER.SM_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND STOCKMASTER.SM_YEARID = " & YearId & ") AS T", "")
                Else
                    DT = OBJCMN.SEARCH("*", "", "(SELECT CHECKINGMASTER.CHECK_BALPCS As LOTPCS, CHECKINGMASTER.CHECK_BALMTRS As LOTMTRS, ItemMaster.ITEM_name As ITEMNAME, ISNULL(DesignMaster.DESIGN_NO,'') AS DESIGNNO, CHECKINGMASTER.CHECK_GRNNO AS GRNNO, CHECKINGMASTER.CHECK_TYPE AS GRNTYPE, LOT_VIEW.TOTALWT AS QUALITYWT FROM CHECKINGMASTER INNER JOIN ITEMMASTER ON CHECKINGMASTER.CHECK_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON CHECK_DESIGNID = DESIGN_ID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = CHECKINGMASTER.CHECK_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = CHECKINGMASTER.CHECK_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 AND LOT_VIEW.PROGRAMDONE = 0 AND (TOTALMTRS - PROGRAMMTRS) > 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "'  AND CHECK_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND CHECK_YEARID = " & YearId & " UNION ALL SELECT STOCKMASTER.SM_PCS As LOTPCS, STOCKMASTER.SM_MTRS As LOTMTRS, ItemMaster.ITEM_name As ITEMNAME, ISNULL(DesignMaster.DESIGN_NO,'') AS DESIGNNO, STOCKMASTER.SM_NO AS GRNNO, 'OPENING' AS GRNTYPE, LOT_VIEW.TOTALWT AS QUALITYWT FROM STOCKMASTER INNER JOIN ITEMMASTER ON STOCKMASTER.SM_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON STOCKMASTER.SM_DESIGNID = DESIGN_ID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = STOCKMASTER.SM_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = STOCKMASTER.SM_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTCOMPLETED = 0 AND LOT_VIEW.PROGRAMDONE = 0 AND (TOTALMTRS - PROGRAMMTRS) > 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND STOCKMASTER.SM_TYPE = 'JOBBERSTOCK' AND STOCKMASTER.SM_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND STOCKMASTER.SM_YEARID = " & YearId & ") AS T", "")
                End If

                If DT.Rows.Count > 0 Then
                    If LOTSTATUSONMTRS = True Then TXTTOTALPCS.Text = Val(DT.Rows(0).Item("LOTMTRS")) Else TXTTOTALPCS.Text = Val(DT.Rows(0).Item("LOTPCS"))
                    If ClientName = "KRISHNA" Then TXTTOTALPCS.Text = Val(DT.Rows(0).Item("LOTPCS"))
                    TXTQUALITYWT.Text = Val(DT.Rows(0).Item("QUALITYWT"))

                    CMBITEMNAME.Text = DT.Rows(0).Item("ITEMNAME")
                    CMBDESIGNNO.Text = DT.Rows(0).Item("DESIGNNO")
                    TXTGRNNO.Text = Val(DT.Rows(0).Item("GRNNO"))
                    TXTGRNTYPE.Text = DT.Rows(0).Item("GRNTYPE")

                    'FETCH PROCESS NAMES FROM ITEMMASTER_PROCESS
                    DT = OBJCMN.SEARCH("ISNULL(PROCESS_NAME,'') AS PROCESSNAME ", "", " ITEMMASTER_PROCESS INNER JOIN ITEMMASTER ON ITEMMASTER.ITEM_ID = ITEMMASTER_PROCESS.ITEM_ID INNER JOIN PROCESSMASTER ON ITEMMASTER_PROCESS.ITEM_PROCESSID = PROCESS_ID ", " AND ITEMMASTER.ITEM_NAME = '" & CMBITEMNAME.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then TXTPROCESSNAME.Clear()
                    For Each DTROW As DataRow In DT.Rows
                        TXTPROCESSNAME.Text = TXTPROCESSNAME.Text & DTROW("PROCESSNAME") & vbCrLf
                    Next

                Else
                    MsgBox("Invalid Lot No", MsgBoxStyle.Critical)
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCOLOR.KeyDown
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

    Private Sub TOOLPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLPREVIOUS.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDLOT.RowCount = 0
            PROGRAMNO = Val(TXTPROGRAMNO.Text) - 1
            If PROGRAMNO > 0 Then
                EDIT = True
                'PROGRAMMASTER_Load(sender, e)
                SHOWDATA()
            Else
                CLEAR()
                EDIT = False
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TOOLNEXT.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            PROGRAMNO = Val(TXTPROGRAMNO.Text) + 1
            GETMAXNO()
            CLEAR()
            If Val(TXTPROGRAMNO.Text) - 1 >= PROGRAMNO Then
                EDIT = True
                'PROGRAMMASTER_Load(sender, e)
                SHOWDATA()
            Else
                CLEAR()
                EDIT = False
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

            Dim OBJPROGRAM As New ProgramDetails
            OBJPROGRAM.MdiParent = MDIMain
            OBJPROGRAM.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Call cmdok_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub PRINTTOOLSTRIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRINTTOOLSTRIP.Click
        Try
            If EDIT = True Then PRINTREPORT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPCS_Validated(sender As Object, e As EventArgs) Handles TXTPCS.Validated
        'Try
        '    If ClientName = "AVIS" Then
        '        If CMBNAME.Text.Trim = "" And CMBLOTNO.Text.Trim <> "" Then CMBLOTNO.Text = ""
        '        If CMBITEMNAME.Text.Trim <> "" And CMBDESIGNNO.Text.Trim <> "" And CMBCOLOR.Text.Trim <> "" And Val(TXTPCS.Text.Trim) > 0 Then fillgrid()
        '    Else
        '        'COLOR NOT MANDATE
        '        If ClientName = "SUPEEMA" Or ClientName = "YASHVI" Then
        '            If CMBLOTNO.Text.Trim <> "" And Val(TXTTOTALPCS.Text) > 0 And CMBITEMNAME.Text.Trim <> "" And Val(TXTPCS.Text.Trim) > 0 Then fillgrid()

        '            'LOTNO NOT MANDATE
        '        ElseIf ClientName = "OWAIS" Then
        '            If CMBITEMNAME.Text.Trim <> "" And CMBCOLOR.Text.Trim <> "" And Val(TXTPCS.Text.Trim) > 0 Then fillgrid()
        '        Else
        '            If CMBLOTNO.Text.Trim <> "" And Val(TXTTOTALPCS.Text) > 0 And CMBITEMNAME.Text.Trim <> "" And CMBCOLOR.Text.Trim <> "" And Val(TXTPCS.Text.Trim) > 0 Then fillgrid()
        '        End If
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub ProgramMaster_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "SUPEEMA" Or ClientName = "AVIS" Or ClientName = "NAYRA" Or ClientName = "YASHVI" Then
                GPCS.HeaderText = "Mtrs"
                GTOTALPCS.HeaderText = "Mtrs"
            End If

            If ClientName = "YASHVI" Then
                LBLQUALITYWT.Visible = True
                TXTQUALITYWT.Visible = True
            End If

            If ClientName = "AVIS" Or ClientName = "SUPEEMA" Then
                LBLSRNO.Text = "Card No"
                CMBNAME.BackColor = Color.White
                CMBLOTNO.BackColor = Color.White
                CMBDESIGNNO.BackColor = Color.LemonChiffon
                CMBITEMNAME.Enabled = True
                CMBDESIGNNO.Enabled = True
                GPROGISSDATE.Visible = True
                GSTATUS.Visible = True
                'GCUTRECDDATE.Visible = True
                'GFINISHCUTTING.Visible = True
                'GINWARDDATE.Visible = True
                CARDRECDATE.Visible = True
                LBLCARDREC.Visible = True
            End If


            If ClientName = "SUPRIYA" Or ClientName = "SNCM" Or ClientName = "REALCORPORATION" Then
                CMBLOTNO.BackColor = Color.White
                CMBITEMNAME.Enabled = True
                CMBDESIGNNO.Enabled = True
            End If

            If ClientName = "VINTAGEINDIA" Then
                LBLDYEING.Text = "Party Name"
                CHKPROCESS.Visible = True
                CLB_PROCESS.Visible = True
            End If

            If ClientName = "KARAN" Then
                CMBCOLOR.BackColor = Color.White
                CMBITEMNAME.Enabled = True
                CMBDESIGNNO.Enabled = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CARDRECDATE_Validating(sender As Object, e As CancelEventArgs) Handles CARDRECDATE.Validating
        Try
            If CARDRECDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(CARDRECDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(sender As Object, e As EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then itemvalidate(CMBITEMNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROGRAMDATE_Validated(sender As Object, e As EventArgs) Handles PROGRAMDATE.Validated
        If ClientName = "AVIS" Then CMBDESIGNNO.Focus()
    End Sub

    Private Sub CMBDESIGNNO_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBDESIGNNO.Enter
        Try
            If CMBDESIGNNO.Text.Trim = "" Then FILLDESIGN(CMBDESIGNNO, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNNO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGNNO.Validating
        Try
            If CMBDESIGNNO.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGNNO, e, Me, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNNO_Validated(sender As Object, e As EventArgs) Handles CMBDESIGNNO.Validated
        Try
            'GET ITEMNAME AUTO
            If ClientName = "AVIS" And CMBDESIGNNO.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(ITEM_NAME,'') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEM_ID", " AND DESIGN_NO = '" & CMBDESIGNNO.Text.Trim & "' AND DESIGN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then CMBITEMNAME.Text = DT.Rows(0).Item("ITEMNAME")
                CMBCOLOR.Text = ""
                CMBCOLOR.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtremarks_KeyDown(sender As Object, e As KeyEventArgs) Handles txtremarks.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then
                    If txtremarks.Text = "" Then txtremarks.Text = OBJREMARKS.TEMPNAME Else txtremarks.Text = txtremarks.Text & vbCrLf & OBJREMARKS.TEMPNAME
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CARDISSUEDATE_Validating(sender As Object, e As CancelEventArgs) Handles CARDISSUEDATE.Validating
        Try
            If CARDISSUEDATE.Text.Trim <> "__/__/____" Then
                Dim temp1 As DateTime
                If Not DateTime.TryParse(CARDISSUEDATE.Text, temp1) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validated(sender As Object, e As EventArgs) Handles tstxtbillno.Validated
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDLOT.RowCount = 0
                PROGRAMNO = Val(tstxtbillno.Text)
                If PROGRAMNO > 0 Then
                    EDIT = True
                    'PROGRAMMASTER_Load(sender, e)
                    SHOWDATA()
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTPROGRAMNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTPROGRAMNO.Validating
        Try
            If Val(TXTPROGRAMNO.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(PROGRAM_NO,0)  AS PROGRAM", "", " PROGRAMMASTER ", "  AND PROGRAM_NO=" & Val(TXTPROGRAMNO.Text.Trim) & " AND PROGRAM_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Program No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOTNO_Enter(sender As Object, e As EventArgs) Handles CMBLOTNO.Enter
        Try
            FILLLOTNO()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOTNO_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBLOTNO.KeyDown
        Try
            If CMBNAME.Text.Trim <> "" And EDIT = False Then
                If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
                If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

                If e.KeyCode = Keys.F1 Then
                    Dim OBJLEDGER As New SelectLotNo
                    OBJLEDGER.JOBBERNAME = CMBNAME.Text.Trim
                    OBJLEDGER.WCLAUSE = " AND LOT_VIEW.ACCEPTEDMTRS > 0 AND LOT_VIEW.DYEINGJOB = 'DYEING' AND LOT_VIEW.PROGRAMDONE = 'FALSE' AND LOT_VIEW.LOTCOMPLETED = 0"
                    OBJLEDGER.ShowDialog()
                    If OBJLEDGER.DT.Rows.Count > 0 Then
                        CMBLOTNO.Text = OBJLEDGER.DT.Rows(0).Item("LOTNO")
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTSONO_Validated(sender As Object, e As EventArgs) Handles TXTSONO.Validated
        Try
            If Val(TXTSONO.Text.Trim) > 0 And EDIT = False Then
                If MsgBox("Fetch Data From SO No " & Val(TXTSONO.Text.Trim) & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                If MsgBox("Wish to Get All Design in Single Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    DT = OBJCMN.SEARCH("ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(SALEORDER_DESC.SO_MTRS, 0) AS MTRS ", "", " SALEORDER INNER JOIN SALEORDER_DESC ON SALEORDER.so_no = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID LEFT OUTER JOIN DESIGNMASTER ON DESIGNMASTER.DESIGN_id = SALEORDER_DESC.SO_DESIGNID LEFT OUTER JOIN ITEMMASTER AS ITEMMASTER ON SALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER AS COLORMASTER ON SALEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_id ", " AND SALEORDER.SO_NO = " & Val(TXTSONO.Text.Trim) & " AND SALEORDER.SO_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        For Each DR As DataRow In DT.Rows
                            GRIDLOT.Rows.Add(0, "", DR("ITEM"), DR("DESIGN"), 0, DR("COLOR"), 0, Val(DR("MTRS")), 0, 0, 0, "", "", "", "", "", "", 0, "", 0, "", 0, 0, 0)
                        Next
                        getsrno(GRIDLOT)
                        TOTAL()
                    Else
                        MsgBox("Invalid SO No", MsgBoxStyle.Critical)
                        TXTSONO.Clear()
                        Exit Sub
                    End If
                Else
                    If MsgBox("Create Seperate Program Card for Each Design", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    If MsgBox("Are You Sure Create Seperate Program Card for Each Design", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    DT = OBJCMN.SEARCH("SALEORDER.SO_NO AS SONO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(SUM(SALEORDER_DESC.SO_MTRS), 0) AS MTRS ", "", " SALEORDER INNER JOIN SALEORDER_DESC ON SALEORDER.so_no = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID LEFT OUTER JOIN DESIGNMASTER ON DESIGNMASTER.DESIGN_id = SALEORDER_DESC.SO_DESIGNID LEFT OUTER JOIN ITEMMASTER AS ITEMMASTER ON SALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id ", " AND SALEORDER.SO_NO = " & Val(TXTSONO.Text.Trim) & " AND SALEORDER.SO_YEARID = " & YearId & " GROUP BY SALEORDER.SO_NO, ISNULL(ITEMMASTER.item_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, '')")
                    If DT.Rows.Count > 0 Then
                        For Each DR As DataRow In DT.Rows
                            TXTSONO.Text = Val(DR("SONO"))

                            If MsgBox("Create Program Card for " + DR("DESIGN"), MsgBoxStyle.YesNo) = MsgBoxResult.No Then GoTo NEXTLINE
                            Dim TEMPMTRS As Double = InputBox("Enter Program Mtrs")

                            Dim DTDESIGN As DataTable = OBJCMN.SEARCH("ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(SALEORDER_DESC.SO_MTRS, 0) AS MTRS ", "", " SALEORDER INNER JOIN SALEORDER_DESC ON SALEORDER.so_no = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID LEFT OUTER JOIN DESIGNMASTER ON DESIGNMASTER.DESIGN_id = SALEORDER_DESC.SO_DESIGNID LEFT OUTER JOIN ITEMMASTER AS ITEMMASTER ON SALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER AS COLORMASTER ON SALEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_id ", " AND SALEORDER.SO_NO = " & Val(DR("SONO")) & " AND ISNULL(ITEMMASTER.ITEM_NAME, '') = '" & DR("ITEM") & "' AND ISNULL(DESIGNMASTER.DESIGN_NO, '') = '" & DR("DESIGN") & "' AND SALEORDER.SO_YEARID = " & YearId)
                            For Each DRDESIGN As DataRow In DTDESIGN.Rows
                                If TEMPMTRS = 0 Then TEMPMTRS = Val(DRDESIGN("MTRS"))
                                GRIDLOT.Rows.Add(0, "", DRDESIGN("ITEM"), DRDESIGN("DESIGN"), 0, DRDESIGN("COLOR"), 0, TEMPMTRS, 0, 0, 0, "", "", "", "", "", "", 0, "", 0, "", 0, 0, 0)
                            Next
                            getsrno(GRIDLOT)
                            TOTAL()

                            Call cmdok_Click(sender, e)

NEXTLINE:
                        Next

                    Else
                        MsgBox("Invalid SO No", MsgBoxStyle.Critical)
                        TXTSONO.Clear()
                        Exit Sub
                    End If
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDLOT_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDLOT.CellValidating
        Try
            If EDIT = False And ClientName = "AVIS" Then
                Dim TEMPITEM As String = GRIDLOT.CurrentRow.Cells(GITEMNAME.Index).Value
                For I As Integer = GRIDLOT.CurrentRow.Index + 1 To GRIDLOT.RowCount - 1
                    If TEMPITEM = GRIDLOT.Rows(I).Cells(GITEMNAME.Index).Value Then
                        GRIDLOT.Item(GRATE.Index, I).Value = GRIDLOT.Item(GRATE.Index, I - 1).EditedFormattedValue
                    End If
                Next
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFETCHGRNNO_Validated(sender As Object, e As EventArgs) Handles TXTFETCHGRNNO.Validated
        Try
            If Val(TXTFETCHGRNNO.Text.Trim) > 0 And EDIT = False Then
                If MsgBox("Fetch Data From GRN No " & Val(TXTFETCHGRNNO.Text.Trim) & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                Dim RATETYPE As String = ""
                Dim RATE As Double = 0.0
                Dim TWHERE As String = ""

                If MsgBox("Wish to Get All Design in Single Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    DT = OBJCMN.SEARCH(" GRN.GRN_NO AS GRNNO, GRN.GRN_PLOTNO AS LOTNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(SUM(GRN_DESC.GRN_MTRS), 0) AS MTRS, ISNULL(GRN.GRN_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(DYEDTYPEMASTER.DYEDTYPE_NAME,'') AS DYEDTYPE ", "", " GRN INNER JOIN GRN_DESC ON GRN.GRN_no = GRN_DESC.GRN_NO AND GRN.GRN_YEARID = GRN_DESC.GRN_YEARID AND GRN.GRN_TYPE = GRN_DESC.GRN_GRIDTYPE LEFT OUTER JOIN DESIGNMASTER ON DESIGNMASTER.DESIGN_id = GRN_DESC.GRN_DESIGNID LEFT OUTER JOIN ITEMMASTER AS ITEMMASTER ON GRN_DESC.GRN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER AS COLORMASTER ON GRN_DESC.GRN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DYEDTYPEMASTER ON GRN.GRN_DYEINGTYPEID = DYEDTYPEMASTER.DYEDTYPE_ID", " AND GRN.GRN_NO = " & Val(TXTFETCHGRNNO.Text.Trim) & " AND GRN.GRN_TYPE = 'Job Work' AND GRN.GRN_YEARID = " & YearId & " GROUP BY GRN.GRN_NO, GRN.GRN_PLOTNO, ISNULL(ITEMMASTER.item_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, ''), ISNULL(GRN.GRN_TOTALMTRS, 0), ISNULL(DYEDTYPEMASTER.DYEDTYPE_NAME,'') ")
                    If DT.Rows.Count > 0 Then

                        If ClientName = "YASHVI" And DT.Rows(0).Item("DYEDTYPE") = "TOP DYED" Then RATETYPE = "White"

                        'GETRATE FROM DYEINGPRICELIST
                        'FOR YASHVI FETCH RATES WITH RESPECT TO DYEDTYPE
                        If ClientName = "YASHVI" Then TWHERE = " AND ISNULL(DYEDTYPEMASTER.DYEDTYPE_NAME,'') = '" & DT.Rows(0).Item("DYEDTYPE") & "'"

                        Dim DTRATE As DataTable = OBJCMN.SEARCH("(CASE WHEN '" & RATETYPE & "' = 'White' THEN PL_WHITE ELSE (CASE WHEN '" & RATETYPE & "' = 'Cream' THEN PL_CREAM ELSE (CASE WHEN '" & RATETYPE & "' = 'Light' THEN PL_LIGHT ELSE (CASE WHEN '" & RATETYPE & "' = 'Medium' THEN PL_MEDIUM ELSE (CASE WHEN '" & RATETYPE & "' = 'Dark' THEN PL_DARK ELSE (CASE WHEN '" & RATETYPE & "' = 'Extra Dark' THEN PL_EXTRADARK ELSE (CASE WHEN '" & RATETYPE & "' = 'Rainbow' THEN PL_RAINBOW ELSE (CASE WHEN '" & RATETYPE & "' = 'Procian White' THEN PL_PROCWHITE ELSE (CASE WHEN '" & RATETYPE & "' = 'Procian Dyed' THEN PL_PROCDYED ELSE (CASE WHEN '" & RATETYPE & "' = 'Discharge' THEN PL_DISCHARGE ELSE 0 END) END) END) END) END) END) END) END) END) END) AS RATE, PL_KHADI AS KHADI, PL_ABOVETWOSCREEN AS ABOVE2SCREEN, PL_MISCRATE AS MISCRATE", "", " DYEINGPRICELIST INNER JOIN DYEINGPRICELIST_DESC ON DYEINGPRICELIST.PL_NO = DYEINGPRICELIST_DESC.PL_NO And DYEINGPRICELIST.PL_YEARID = DYEINGPRICELIST_DESC.PL_YEARID INNER JOIN LEDGERS ON DYEINGPRICELIST.PL_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN DYEDTYPEMASTER ON DYEINGPRICELIST_DESC.PL_TYPEID = DYEDTYPEMASTER.DYEDTYPE_ID", " And LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND DYEINGPRICELIST.PL_YEARID = " & YearId & TWHERE)

                        If DTRATE.Rows.Count > 0 Then RATE = Val(DTRATE.Rows(0).Item("RATE"))
                        If CHKKHADI.Checked = True Then RATE += Val(DTRATE.Rows(0).Item("KHADI"))
                        If CHKABOVETOSCREEN.Checked = True Then RATE += Val(DTRATE.Rows(0).Item("ABOVE2SCREEN"))
                        If CHKMISCRATE.Checked = True Then RATE += Val(DTRATE.Rows(0).Item("MISCRATE"))


                        For Each DR As DataRow In DT.Rows
                            GRIDLOT.Rows.Add(0, DR("LOTNO"), DR("ITEM"), DR("DESIGN"), Val(DR("TOTALMTRS")), DR("COLOR"), 0, Val(DR("MTRS")), 0, 0, 0, RATETYPE, "", "", "", "", "", Val(DR("GRNNO")), "Job Work", 0, "", Val(RATE), 0, 0)
                        Next
                        getsrno(GRIDLOT)
                        TOTAL()
                    Else
                        MsgBox("Invalid GRN No", MsgBoxStyle.Critical)
                        TXTFETCHGRNNO.Clear()
                        Exit Sub
                    End If
                Else
                    If MsgBox("Create Seperate Program Card for Each Design", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    If MsgBox("Are You Sure Create Seperate Program Card for Each Design", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    DT = OBJCMN.SEARCH("GRN.GRN_NO AS GRNNO, GRN.GRN_PLOTNO AS LOTNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(SUM(GRN_DESC.GRN_MTRS), 0) AS MTRS, ISNULL(GRN.GRN_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(DYEDTYPEMASTER.DYEDTYPE_NAME,'') AS DYEDTYPE ", "", " GRN INNER JOIN GRN_DESC ON GRN.GRN_no = GRN_DESC.GRN_NO AND GRN.GRN_YEARID = GRN_DESC.GRN_YEARID AND GRN.GRN_TYPE = GRN_DESC.GRN_GRIDTYPE LEFT OUTER JOIN DESIGNMASTER ON DESIGNMASTER.DESIGN_id = GRN_DESC.GRN_DESIGNID LEFT OUTER JOIN ITEMMASTER AS ITEMMASTER ON GRN_DESC.GRN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DYEDTYPEMASTER ON GRN.GRN_DYEINGTYPEID = DYEDTYPEMASTER.DYEDTYPE_ID", " AND GRN.GRN_NO = " & Val(TXTFETCHGRNNO.Text.Trim) & " AND GRN.GRN_TYPE = 'Job Work' AND GRN.GRN_YEARID = " & YearId & " GROUP BY GRN.GRN_NO, GRN.GRN_PLOTNO, ISNULL(ITEMMASTER.item_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(GRN.GRN_TOTALMTRS, 0), ISNULL(DYEDTYPEMASTER.DYEDTYPE_NAME,'')")
                    If DT.Rows.Count > 0 Then
                        If ClientName = "YASHVI" And DT.Rows(0).Item("DYEDTYPE") = "TOP DYED" Then RATETYPE = "White"

                        'GETRATE FROM DYEINGPRICELIST
                        'FOR YASHVI FETCH RATES WITH RESPECT TO DYEDTYPE
                        If ClientName = "YASHVI" Then TWHERE = " AND ISNULL(DYEDTYPEMASTER.DYEDTYPE_NAME,'') = '" & DT.Rows(0).Item("DYEDTYPE") & "'"

                        Dim DTRATE As DataTable = OBJCMN.SEARCH("(CASE WHEN '" & RATETYPE & "' = 'White' THEN PL_WHITE ELSE (CASE WHEN '" & RATETYPE & "' = 'Cream' THEN PL_CREAM ELSE (CASE WHEN '" & RATETYPE & "' = 'Light' THEN PL_LIGHT ELSE (CASE WHEN '" & RATETYPE & "' = 'Medium' THEN PL_MEDIUM ELSE (CASE WHEN '" & RATETYPE & "' = 'Dark' THEN PL_DARK ELSE (CASE WHEN '" & RATETYPE & "' = 'Extra Dark' THEN PL_EXTRADARK ELSE (CASE WHEN '" & RATETYPE & "' = 'Rainbow' THEN PL_RAINBOW ELSE (CASE WHEN '" & RATETYPE & "' = 'Procian White' THEN PL_PROCWHITE ELSE (CASE WHEN '" & RATETYPE & "' = 'Procian Dyed' THEN PL_PROCDYED ELSE (CASE WHEN '" & RATETYPE & "' = 'Discharge' THEN PL_DISCHARGE ELSE 0 END) END) END) END) END) END) END) END) END) END) AS RATE, PL_KHADI AS KHADI, PL_ABOVETWOSCREEN AS ABOVE2SCREEN, PL_MISCRATE AS MISCRATE", "", " DYEINGPRICELIST INNER JOIN DYEINGPRICELIST_DESC ON DYEINGPRICELIST.PL_NO = DYEINGPRICELIST_DESC.PL_NO And DYEINGPRICELIST.PL_YEARID = DYEINGPRICELIST_DESC.PL_YEARID INNER JOIN LEDGERS ON DYEINGPRICELIST.PL_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN DYEDTYPEMASTER ON DYEINGPRICELIST_DESC.PL_TYPEID = DYEDTYPEMASTER.DYEDTYPE_ID", " And LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND DYEINGPRICELIST.PL_YEARID = " & YearId & TWHERE)

                        If DTRATE.Rows.Count > 0 Then RATE = Val(DTRATE.Rows(0).Item("RATE"))
                        If CHKKHADI.Checked = True Then RATE += Val(DTRATE.Rows(0).Item("KHADI"))
                        If CHKABOVETOSCREEN.Checked = True Then RATE += Val(DTRATE.Rows(0).Item("ABOVE2SCREEN"))
                        If CHKMISCRATE.Checked = True Then RATE += Val(DTRATE.Rows(0).Item("MISCRATE"))


                        For Each DR As DataRow In DT.Rows
                            TXTFETCHGRNNO.Text = Val(DR("GRNNO"))

                            If MsgBox("Create Program Card for " + DR("DESIGN"), MsgBoxStyle.YesNo) = MsgBoxResult.No Then GoTo NEXTLINE
                            Dim TEMPMTRS As Double = InputBox("Enter Program Mtrs")

                            Dim DTDESIGN As DataTable = OBJCMN.SEARCH("ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(GRN_DESC.GRN_MTRS, 0) AS MTRS ", "", " GRN INNER JOIN GRN_DESC ON GRN.GRN_no = GRN_DESC.GRN_NO AND GRN.GRN_YEARID = GRN_DESC.GRN_YEARID  AND GRN.GRN_TYPE = GRN_DESC.GRN_GRIDTYPE LEFT OUTER JOIN DESIGNMASTER ON DESIGNMASTER.DESIGN_id = GRN_DESC.GRN_DESIGNID LEFT OUTER JOIN ITEMMASTER AS ITEMMASTER ON GRN_DESC.GRN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER AS COLORMASTER ON GRN_DESC.GRN_COLORID = COLORMASTER.COLOR_id ", " AND GRN.GRN_NO = " & Val(DR("GRNNO")) & " AND ISNULL(ITEMMASTER.ITEM_NAME, '') = '" & DR("ITEM") & "' AND ISNULL(DESIGNMASTER.DESIGN_NO, '') = '" & DR("DESIGN") & "'  AND GRN.GRN_TYPE = 'Job Work' AND GRN.GRN_YEARID = " & YearId)
                            For Each DRDESIGN As DataRow In DTDESIGN.Rows
                                If TEMPMTRS = 0 Then TEMPMTRS = Val(DRDESIGN("MTRS"))
                                GRIDLOT.Rows.Add(0, DR("LOTNO"), DRDESIGN("ITEM"), DRDESIGN("DESIGN"), Val(DR("TOTALMTRS")), DRDESIGN("COLOR"), 0, TEMPMTRS, 0, 0, 0, RATETYPE, "", "", "", "", "", Val(DR("GRNNO")), "Job Work", 0, "", Val(RATE), 0)
                            Next
                            getsrno(GRIDLOT)
                            TOTAL()

                            Call cmdok_Click(sender, e)
NEXTLINE:
                        Next

                    Else
                        MsgBox("Invalid GRN No", MsgBoxStyle.Critical)
                        TXTFETCHGRNNO.Clear()
                        Exit Sub
                    End If
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBRATETYPE_Validated(sender As Object, e As EventArgs) Handles CMBRATETYPE.Validated
        Try
            If ClientName = "AVIS" Or ClientName = "SUPRIYA" Then
                If CMBNAME.Text.Trim = "" And CMBLOTNO.Text.Trim <> "" Then CMBLOTNO.Text = ""
                If CMBITEMNAME.Text.Trim <> "" And CMBDESIGNNO.Text.Trim <> "" And CMBCOLOR.Text.Trim <> "" And Val(TXTPCS.Text.Trim) > 0 Then FILLGRID()
            Else
                'COLOR NOT MANDATE
                If ClientName = "SUPEEMA" Or ClientName = "YASHVI" Or ClientName = "SNCM" Or ClientName = "VINTAGEINDIA" Or ClientName = "REALCORPORATION" Or ClientName = "KARAN" Then
                    If CMBLOTNO.Text.Trim <> "" And Val(TXTTOTALPCS.Text) > 0 And CMBITEMNAME.Text.Trim <> "" And Val(TXTPCS.Text.Trim) > 0 Then FILLGRID()
                Else
                    If CMBLOTNO.Text.Trim <> "" And Val(TXTTOTALPCS.Text) > 0 And CMBITEMNAME.Text.Trim <> "" And CMBCOLOR.Text.Trim <> "" And Val(TXTPCS.Text.Trim) > 0 Then fillgrid()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CHECKVALIDATE() As Boolean
        Try
            Dim BLN As Boolean = True
            For Each ROW As DataGridViewRow In GRIDCHGS.Rows
                If GRIDCHGSDOUBLECLICK = False Or (GRIDCHGSDOUBLECLICK = True And TEMPCHGSROW <> ROW.Index) Then
                    If CMBCHARGES.Text.Trim = ROW.Cells(ECHARGES.Index).Value Then
                        BLN = False
                    End If
                End If
            Next
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub EDITCHGSROW()
        Try
            TXTCHGSAMT.ReadOnly = False
            If GRIDCHGS.CurrentRow.Index >= 0 And GRIDCHGS.Item(ESRNO.Index, GRIDCHGS.CurrentRow.Index).Value <> Nothing Then
                GRIDCHGSDOUBLECLICK = True
                TXTCHGSSRNO.Text = GRIDCHGS.Item(ESRNO.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                CMBCHARGES.Text = GRIDCHGS.Item(ECHARGES.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSAMT.Text = GRIDCHGS.Item(EAMT.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTTAXID.Text = GRIDCHGS.Item(ETAXID.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TEMPCHGSROW = GRIDCHGS.CurrentRow.Index
                CMBCHARGES.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHGS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCHGS.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCHGS.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbMERCHANT.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDCHGSDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDCHGS.Rows.RemoveAt(GRIDCHGS.CurrentRow.Index)
                'TOTAL()
                getsrno(GRIDCHGS)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITCHGSROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLCHGSGRID()
        Try
            If GRIDCHGSDOUBLECLICK = False Then
                GRIDCHGS.Rows.Add(Val(TXTCHGSSRNO.Text.Trim), CMBCHARGES.Text.Trim, Val(TXTCHGSAMT.Text.Trim), Val(TXTTAXID.Text.Trim))
                getsrno(GRIDCHGS)
            ElseIf GRIDCHGSDOUBLECLICK = True Then
                GRIDCHGS.Item(ESRNO.Index, TEMPCHGSROW).Value = Val(TXTCHGSSRNO.Text.Trim)
                GRIDCHGS.Item(ECHARGES.Index, TEMPCHGSROW).Value = CMBCHARGES.Text.Trim
                GRIDCHGS.Item(EAMT.Index, TEMPCHGSROW).Value = Format(Val(TXTCHGSAMT.Text.Trim), "0.00")
                GRIDCHGS.Item(ETAXID.Index, TEMPCHGSROW).Value = Format(Val(TXTTAXID.Text.Trim))
                GRIDCHGSDOUBLECLICK = False
            End If

            GRIDCHGS.FirstDisplayedScrollingRowIndex = GRIDCHGS.RowCount - 1

            TXTCHGSSRNO.Text = GRIDCHGS.RowCount + 1
            CMBCHARGES.Text = ""
            TXTCHGSAMT.Clear()
            CMBCHARGES.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHGS_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDCHGS.CellDoubleClick
        EDITCHGSROW()
    End Sub

    Private Sub TXTCHGSAMT_Validating(sender As Object, e As CancelEventArgs) Handles TXTCHGSAMT.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" And (Val(TXTCHGSAMT.Text.Trim) <> 0) Then
                FILLCHGSGRID()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMBCHARGES_Enter(sender As Object, e As EventArgs) Handles CMBCHARGES.Enter
        Try
            If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' or GROUPMASTER.GROUP_SECONDARY = 'Sales A/C')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCHARGES.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' or GROUPMASTER.GROUP_SECONDARY = 'Sales A/C')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBCHARGES.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Validated
        Try
            If CMBCHARGES.Text.Trim <> "" Then
                filltax()

                'GET ADDLESS DONE BY GULKIT
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS.ACC_ADDLESS,'ADD') AS ADDLESS ", "", "LEDGERS", " AND ACC_CMPNAME = '" & CMBCHARGES.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If UCase(DT.Rows(0).Item("ADDLESS")) = "LESS" Then

                        If Val(TXTCHGSAMT.Text.Trim) = 0 Then TXTCHGSAMT.Text = "-"

                    End If
                End If
            Else
                TOTAL()
                TXTCHGSAMT.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub filltax()
        Try

            TXTCHGSAMT.ReadOnly = False
            TXTTAXID.Text = 0
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable = objclscommon.search(" ISNULL(tax_tax, 0) as TAX, TAX_ID AS TAXID ", "", " TAXMASTER", " AND tax_name = '" & CMBCHARGES.Text & "'  AND tax_cmpid=" & CmpId & " AND tax_LOCATIONID = " & Locationid & " AND tax_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                TXTTAXID.Text = Val(dt.Rows(0).Item("TAXID"))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCHARGES.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" Then namevalidate(CMBCHARGES, CMBCODE, e, Me, TXTTAXID, "  and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' or GROUPMASTER.GROUP_SECONDARY = 'Sales A/C')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSAMT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTCHGSAMT.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AMOUNTNUMDOTKYEPRESS(ByVal han As KeyPressEventArgs, ByVal sen As Control, ByVal frm As System.Windows.Forms.Form)
        Try
            Dim mypos As Integer

            If AscW(han.KeyChar) >= 48 And AscW(han.KeyChar) <= 57 Or AscW(han.KeyChar) = 8 Or AscW(han.KeyChar) = 45 Then
                han.KeyChar = han.KeyChar
            ElseIf AscW(han.KeyChar) = 46 Or AscW(han.KeyChar) = 45 Then
                mypos = InStr(1, sen.Text, ".")
                If mypos = 0 Then
                    han.KeyChar = han.KeyChar
                Else
                    han.KeyChar = ""
                End If
            Else
                han.KeyChar = ""
            End If

            If AscW(han.KeyChar) = Keys.Escape Then
                frm.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillcheckboxlist()

        Dim objclscomm As New ClsCommon()

        'Fill PROCESS
        Dim dt As DataTable = objclscomm.Execute_Any_String("SELECT DISTINCT PROCESS_NAME FROM PROCESSMASTER ", " ", " WHERE PROCESSMASTER.PROCESS_Yearid=" & YearId & " ORDER BY PROCESSMASTER.PROCESS_NAME ")
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                CLB_PROCESS.Items.Add(Convert.ToString(dr(0)), False)
            Next
        End If

    End Sub

    Private Sub CHKPROCESS_CheckedChanged(sender As Object, e As EventArgs) Handles CHKPROCESS.CheckedChanged
        If CHKPROCESS.Checked = True Then
            For i As Integer = 0 To CLB_PROCESS.Items.Count - 1
                CLB_PROCESS.SetItemChecked(i, True)
            Next
        Else
            For i As Integer = 0 To CLB_PROCESS.Items.Count - 1
                CLB_PROCESS.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Private Sub CMBCOLOR_Validated(sender As Object, e As EventArgs) Handles CMBCOLOR.Validated
        Try
            'GET RATETYPE FROM DESIGNMASTER_COLOR
            If CMBCOLOR.Text.Trim <> "" And CMBDESIGNNO.Text.Trim <> "" And FETCHITEMWISEDESIGN = True Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(DESIGNMASTER_COLOR.DESIGN_SHADETYPE,'') AS RATETYPE ", "", "DESIGNMASTER_COLOR INNER JOIN DESIGNMASTER ON DESIGNMASTER_COLOR.DESIGN_ID = DESIGNMASTER.DESIGN_ID INNER JOIN COLORMASTER ON DESIGNMASTER_COLOR.DESIGN_COLORID = COLORMASTER.COLOR_ID ", " AND DESIGNMASTER.DESIGN_NO = '" & CMBDESIGNNO.Text.Trim & "' AND COLORMASTER.COLOR_NAME = '" & CMBCOLOR.Text.Trim & "' AND DESIGNMASTER.DESIGN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then CMBRATETYPE.Text = DT.Rows(0).Item("RATETYPE")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class