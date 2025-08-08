
Imports System.ComponentModel
Imports BL

Public Class StockReco

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Public TEMPRECONO As Integer          'used for editing
    Public EDIT As Boolean          'used for editing
    Dim TEMPROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer
    Public TEMPPROFORMANO As Integer = 0
    Public UNCHECKEDSTOCK As Boolean = False
    Public BARCODE As String
    Dim ALLOWMANUALRECNO As Boolean = False


    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub StockReco_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            GRIDSTOCK.Focus()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        End If
    End Sub

    Sub FILLCMB()
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
            If CMBPIECETYPE.Text.Trim = "" Then fillPIECETYPE(CMBPIECETYPE)
            If cmbitemname.Text = "" Then fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
            If cmbqtyunit.Text.Trim = "" Then fillunit(cmbqtyunit)
            If cmbcolor.Text.Trim = "" Then FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE='ACCOUNTS'")
            If CMBCONTRACTOR.Text.Trim = "" Then FILLCONTRACT(CMBCONTRACTOR)

            FILLRACK(CMBRACK)
            FILLSHELF(CMBSHELF)

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("CMP_NAME AS CMPNAME", "", "CMPMASTER", " AND CMP_ID <> " & CmpId)
            For Each ROW As DataRow In DT.Rows
                CMBCMPNAME.Items.Add(ROW("CMPNAME"))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETDATA()
        Try
            Dim OBJCLSPROFORMA As New ClsProforma()
            Dim dttable As DataTable = OBJCLSPROFORMA.SELECTPROFORMA(TEMPPROFORMANO, CmpId, Locationid, YearId)
            If dttable.Rows.Count > 0 Then
                For Each dr As DataRow In dttable.Rows
                    GRIDSTOCK.Rows.Add(GRIDSTOCK.RowCount + 1, dr("PIECETYPE"), dr("ITEMNAME").ToString, dr("QUALITY"), dr("DESIGN"), dr("COLOR"), "", Format(Val(dr("PCS")), "0"), "", Format(Val(dr("MTRS")), "0.00"), 0, "Mtrs", 0, dr("BARCODE"), dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"))
                Next
                txtremarks.Text = "Proforma No - " & Val(TEMPPROFORMANO)
                TOTAL()
                GRIDSTOCK.FirstDisplayedScrollingRowIndex = GRIDSTOCK.RowCount - 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETUNCHECKEDDATA()
        Try
            ' GRIDSTOCK.DataSource = DT
            Dim TEMPCONDITION As String = ""
            If ClientName = "YAMUNESH" Or ClientName = "MOMAI" Or ClientName = "AXIS" Then
                TEMPCONDITION = TEMPCONDITION & " AND ROUND(PCS,0) > 0 "
            ElseIf ClientName = "MOHAN" Then
                TEMPCONDITION = TEMPCONDITION & " AND ROUND(MTRS,2) > 0 "
            End If

            Dim OBJCMN As New ClsCommon()
            Dim dttable As DataTable = OBJCMN.SEARCH("BARCODESTOCK.*", "", " BARCODESTOCK ", " AND BARCODESTOCK.BARCODE NOT IN (SELECT BARCODE FROM STOCKTAKING_DESC WHERE YEARID = " & YearId & ") AND BARCODESTOCK.YEARID = " & YearId & TEMPCONDITION & " AND BARCODESTOCK.BARCODE IN (" & BARCODE & ")")
            If dttable.Rows.Count > 0 Then
                For Each dr As DataRow In dttable.Rows
                    GRIDSTOCK.Rows.Add(GRIDSTOCK.RowCount + 1, dr("PIECETYPE"), dr("ITEMNAME").ToString, dr("QUALITY"), dr("DESIGNNO"), dr("COLOR"), dr("LOTNO"), Format(Val(dr("PCS")), "0"), dr("UNIT"), Format(Val(dr("MTRS")), "0.00"), 0, "Mtrs", 0, dr("BARCODE"), dr("FROMNO"), dr("FROMSRNO"), dr("TYPE"))
                Next
                TOTAL()
                GRIDSTOCK.FirstDisplayedScrollingRowIndex = GRIDSTOCK.RowCount - 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StockReco_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GDN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            'If ClientName = "AVIS" Then ALLOWMANUALRECNO = True

            FILLCMB()
            CLEAR()

            If TEMPPROFORMANO > 0 Then GETDATA()
            If UNCHECKEDSTOCK = True Then GETUNCHECKEDDATA()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If


                Dim objSTOCK As New ClsStockAdjustment()
                Dim dttable As DataTable = objSTOCK.SELECTSTOCKADJUSTMENT(TEMPRECONO, CmpId, Locationid, YearId)
                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows
                        TXTRECONO.Text = TEMPRECONO
                        TXTRECONO.ReadOnly = True
                        DTRECODATE.Value = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                        cmbtrans.Text = dr("TRANSNAME")
                        CMBNAME.Text = dr("NAME")
                        txtchallan.Text = dr("CHALLANNO")
                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        CMBCONTRACTOR.Text = Convert.ToString(dr("CONTRACTOR").ToString)


                        'Item Grid
                        If Val(dr("GRIDSRNO")) > 0 Then GRIDSTOCK.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE").ToString, dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("DESIGNNO").ToString, dr("COLOR"), dr("LOTNO"), Format(dr("PCS"), "0.00"), dr("UNIT"), Format(dr("MTRS"), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER").ToString, Format(Val(dr("AMOUNT")), "0.00"), dr("BALENO").ToString, dr("BARCODE").ToString, dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"))

                        CHKMANUALRATE.Checked = Convert.ToBoolean(dr("MANUALRATE"))

                        If UserName = "Admin" Then
                            If CHKMANUALRATE.CheckState = CheckState.Checked Then
                                TXTRATE.ReadOnly = False
                                GINRATE.ReadOnly = False
                            End If
                        End If

                    Next



                    'GET DATA FROM STOCKADJUSTMENT_INDESC
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STOCKADJUSTMENT_INDESC.SA_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(STOCKADJUSTMENT_INDESC.SA_BALENO, '')  AS BALENO, ISNULL(STOCKADJUSTMENT_INDESC.SA_GRIDDESC, '') AS GRIDDESC, ISNULL(STOCKADJUSTMENT_INDESC.SA_LOTNO, '') AS LOTNO, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(STOCKADJUSTMENT_INDESC.SA_CUT, 0) AS CUT, ISNULL(STOCKADJUSTMENT_INDESC.SA_QTY, 0) AS QTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT,  ISNULL(STOCKADJUSTMENT_INDESC.SA_MTRS, 0) AS MTRS, ISNULL(STOCKADJUSTMENT_INDESC.SA_BARCODE, '') AS BARCODE, ISNULL(STOCKADJUSTMENT_INDESC.SA_OUTPCS, 0) AS OUTPCS, ISNULL(STOCKADJUSTMENT_INDESC.SA_OUTMTRS, 0) AS OUTMTRS, STOCKADJUSTMENT_INDESC.SA_GRIDDONE AS GRIDDONE, ISNULL(SHELFMASTER.SHELF_NAME, '') AS INSHELF, ISNULL(RACKMASTER.RACK_NAME, '') AS INRACK, ISNULL(STOCKADJUSTMENT_INDESC.SA_RATE, 0) AS RATE, ISNULL(STOCKADJUSTMENT_INDESC.SA_PER, 'Mtrs') AS PER, ISNULL(STOCKADJUSTMENT_INDESC.SA_AMOUNT, 0) AS AMOUNT ", "", " STOCKADJUSTMENT_INDESC INNER JOIN PIECETYPEMASTER ON STOCKADJUSTMENT_INDESC.SA_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN RACKMASTER ON STOCKADJUSTMENT_INDESC.SA_RACKID = RACKMASTER.RACK_ID LEFT OUTER JOIN SHELFMASTER ON STOCKADJUSTMENT_INDESC.SA_SHELFID = SHELFMASTER.SHELF_ID LEFT OUTER JOIN UNITMASTER ON STOCKADJUSTMENT_INDESC.SA_QTYUNITID = UNITMASTER.unit_id LEFT OUTER JOIN DESIGNMASTER AS DESIGNMASTER ON STOCKADJUSTMENT_INDESC.SA_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON STOCKADJUSTMENT_INDESC.SA_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON STOCKADJUSTMENT_INDESC.SA_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN  ITEMMASTER AS ITEMMASTER ON STOCKADJUSTMENT_INDESC.SA_ITEMID = ITEMMASTER.item_id ", " AND SA_NO = " & TEMPRECONO & " AND SA_YEARID = " & YearId & " ORDER BY STOCKADJUSTMENT_INDESC.SA_GRIDSRNO")

                    'Dim DT As DataTable = OBJCMN.search(" ISNULL(STOCKADJUSTMENT_INDESC.SA_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(PIECETYPEMASTER.PIECETYPE_name,'') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(STOCKADJUSTMENT_INDESC.SA_BALENO, '') AS BALENO, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(STOCKADJUSTMENT_INDESC.SA_CUT, 0) AS CUT, ISNULL(STOCKADJUSTMENT_INDESC.SA_QTY, 0) AS QTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(STOCKADJUSTMENT_INDESC.SA_MTRS, 0) AS MTRS,ISNULL(STOCKADJUSTMENT_INDESC.SA_RACKID, 0) AS RACK,ISNULL(STOCKADJUSTMENT_INDESC.SA_SHELFID, 0) AS SHELF, ISNULL(STOCKADJUSTMENT_INDESC.SA_BARCODE, '') AS BARCODE, ISNULL(STOCKADJUSTMENT_INDESC.SA_OUTPCS, 0) AS OUTPCS, ISNULL(STOCKADJUSTMENT_INDESC.SA_OUTMTRS, 0) AS OUTMTRS, STOCKADJUSTMENT_INDESC.SA_GRIDDONE AS GRIDDONE ", "", " STOCKADJUSTMENT_INDESC INNER JOIN PIECETYPEMASTER ON STOCKADJUSTMENT_INDESC.SA_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN UNITMASTER ON STOCKADJUSTMENT_INDESC.SA_QTYUNITID = UNITMASTER.unit_id LEFT OUTER JOIN DESIGNMASTER AS DESIGNMASTER ON STOCKADJUSTMENT_INDESC.SA_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON STOCKADJUSTMENT_INDESC.SA_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON STOCKADJUSTMENT_INDESC.SA_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN ITEMMASTER AS ITEMMASTER ON STOCKADJUSTMENT_INDESC.SA_ITEMID = ITEMMASTER.item_id ", " AND SA_NO = " & TEMPRECONO & " AND SA_YEARID = " & YearId)
                    For Each DR As DataRow In DT.Rows
                        'Item Grid
                        GRIDSTOCKIN.Rows.Add(DR("GRIDSRNO").ToString, DR("PIECETYPE"), DR("ITEM").ToString, DR("QUALITY").ToString, DR("BALENO").ToString, DR("GRIDDESC"), DR("LOTNO"), DR("DESIGN").ToString, DR("COLOR"), Format(Val(DR("CUT")), "0.00"), Format(Val(DR("qty")), "0.00"), DR("UNIT").ToString, Format(Val(DR("MTRS")), "0.00"), Format(Val(DR("RATE")), "0.00"), DR("PER").ToString, Format(Val(DR("AMOUNT")), "0.00"), DR("INRACK").ToString, DR("INSHELF").ToString, DR("BARCODE"), 0, DR("OUTPCS"), DR("OUTMTRS"))

                        If Convert.ToBoolean(DR("GRIDDONE")) = True Or Val(DR("OUTPCS")) > 0 Or Val(DR("OUTMTRS")) > 0 Then
                            GRIDSTOCKIN.Rows(GRIDSTOCKIN.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If
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
        LBLCATEGORY.Text = ""
        txtchallan.Clear()

        CMBCMPNAME.Text = ""
        TXTCHNO.Clear()

        DTRECODATE.Value = Now.Date
        tstxtbillno.Clear()
        TXTFROM.Clear()
        TXTTO.Clear()

        txtremarks.Clear()

        CMDSELECTSTOCK.Enabled = True

        lbllocked.Visible = False
        PBlock.Visible = False

        LBLTOTALOUTMTRS.Text = 0.0
        LBLTOTALOUTPCS.Text = 0.0
        LBLTOTALINMTRS.Text = 0.0
        LBLTOTALINPCS.Text = 0.0
        cmbtrans.Text = ""

        TXTBARCODE.Clear()

        GRIDSTOCK.RowCount = 0


        txtsrno.Text = 1
        CMBPIECETYPE.Text = "FRESH"
        cmbitemname.Text = ""
        CMBQUALITY.Text = ""
        TXTBALENO.Clear()
        TXTGRIDDESC.Clear()
        TXTLOTNO.Clear()
        CMBDESIGN.Text = ""
        cmbcolor.Text = ""
        TXTCUT.Clear()
        txtqty.Text = 1
        TXTNOOFENTRIES.Clear()
        If ClientName = "SOFTAS" Or ClientName = "SHREENAKODA" Then cmbqtyunit.Text = "LUMP" Else cmbqtyunit.Text = ""
        If USERGODOWN <> "" Then CMBGODOWN.Text = USERGODOWN Else CMBGODOWN.Text = ""
        TXTMTRS.Clear()
        TXTRATE.Clear()
        CMBPER.Text = "Mtrs"
        TXTAMOUNT.Clear()
        CMBRACK.Text = ""
        CMBSHELF.Text = ""
        CMBNAME.Text = ""
        CMBCONTRACTOR.Text = ""


        TXTINBARCODE.Clear()
        GRIDSTOCKIN.RowCount = 0


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
            LBLTOTALAMOUNT.Text = 0.0
            LBLTOTALINAMOUNT.Text = 0.0
        End If
        CHKMANUALRATE.CheckState = CheckState.Unchecked
        LBLAVGRATE.Text = 0.0
        TXTRATE.ReadOnly = True
        GRATE.ReadOnly = True
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True


            If ALLOWADJMTRSDIFF = False And Val(LBLTOTALINMTRS.Text.Trim) < Val(LBLTOTALOUTMTRS.Text.Trim) Or ((ClientName = "RMANILAL" Or ClientName = "SARAYU") And ALLOWADJMTRSDIFF = False And Val(LBLTOTALINMTRS.Text.Trim) <> Val(LBLTOTALOUTMTRS.Text.Trim)) Then
                EP.SetError(TXTMTRSDIFF, " In Mtrs Cannot be Less than Out Mtrs")
                bln = False
            End If

            If ALLOWMANUALRECNO = True Then
                If Val(TXTRECONO.Text.Trim) <> 0 And EDIT = False Then
                    Dim OBJCMNn As New ClsCommon
                    Dim dttable As DataTable = OBJCMNn.SEARCH(" ISNULL(STOCKADJUSTMENT.SA_NO,0)  AS RECONO", "", " STOCKADJUSTMENT ", "  AND STOCKADJUSTMENT.SA_NO=" & Val(TXTRECONO.Text.Trim) & " AND STOCKADJUSTMENT.SA_YEARID = " & YearId)
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

            If txtchallan.Text.Trim = "" And (ClientName = "AMAN" Or ClientName = "AARYA") Then
                EP.SetError(txtchallan, " Please Fill Challan No")
                bln = False
            End If

            If CMBGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBGODOWN, " Please Fill Godown")
                bln = False
            End If

            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, " Inward Done, Delete Inward First")
                bln = False
            End If

            If GRIDSTOCK.RowCount = 0 And GRIDSTOCKIN.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If

            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If ClientName <> "PARAS" And ClientName <> "AMAN" And ClientName <> "DILIP" And ClientName <> "DILIPNEW" Then
                    If Val(ROW.Cells(GPCS.Index).Value) = 0 Then
                        EP.SetError(LBLTOTALOUTPCS, "Pcs Cannot be 0")
                        bln = False
                    End If
                End If

                If ClientName <> "AXIS" And ClientName <> "GELATO" And ClientName <> "MOMAI" And ClientName <> "KREEVE" And ClientName <> "DILIP" And ClientName <> "DILIPNEW" Then
                    If Val(ROW.Cells(GMTRS.Index).Value) = 0 Then
                        EP.SetError(LBLTOTALOUTMTRS, "Mtrs Cannot be 0")
                        bln = False
                    End If
                End If

                'If ITEMCOSTCENTRE = True And Val(ROW.Cells(GRATE.Index).Value) = 0 Then
                '    EP.SetError(CMBNAME, "Rate Cannot be 0")
                '    bln = False
                'End If

                'CHECK WHETHER BARCODE IS USED OR NOT
                If EDIT = False And ROW.Cells(GBARCODE.Index).Value <> "" And ALLOWBARCODEPRINT = True And CHECKBARCODEERRORVALID = True Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH("*", "", "OUTBARCODESTOCK", " AND BARCODE = '" & ROW.Cells(GBARCODE.Index).Value & "' AND YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        EP.SetError(CMBNAME, "Barcode Already Used")
                        bln = False
                    End If
                End If
            Next

            'If ITEMCOSTCENTRE = True Then
            '    For Each ROW As DataGridViewRow In GRIDSTOCKIN.Rows
            '        If Val(ROW.Cells(GINRATE.Index).Value) = 0 Then
            '            EP.SetError(TXTBARCODE, "Rate Cannot be 0")
            '            bln = False
            '        End If
            '    Next
            'End If


            'CHEKC BARCODE IS PRESENT IN DATABASE OR NOT
            If Not CHECKBARCODE() Then
                bln = False
                EP.SetError(TabControl1, "Barcode already present, Please re-enter data")
            End If

            If Not datecheck(DTRECODATE.Text) Then
                EP.SetError(DTRECODATE, "Date not in Accounting Year")
                bln = False
            End If

            If Convert.ToDateTime(DTRECODATE.Text).Date < STOCKADJBLOCKDATE.Date Then
                EP.SetError(DTRECODATE, "Date is Blocked, Please make entries after " & Format(STOCKADJBLOCKDATE.Date, "dd/MM/yyyy"))
                bln = False
            End If

            If ClientName = "ANOX" And txtremarks.Text.Trim = "" Then
                EP.SetError(txtremarks, "Enter Remarks")
                bln = False
            End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Function CHECKBARCODE() As Boolean
        Try
            Dim BLN As Boolean = True
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(SA_BARCODE,'') AS BARCODE ", "", " STOCKADJUSTMENT_INDESC ", " AND SA_YEARID =  " & YearId)
            If DT.Rows.Count > 0 Then
                For Each DTR As DataRow In DT.Rows
                    For Each ROW As Windows.Forms.DataGridViewRow In GRIDSTOCKIN.Rows
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

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        DTRECODATE.Focus()
        TEMPPROFORMANO = 0
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(SA_no),0) + 1 ", " STOCKADJUSTMENT ", " AND SA_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTRECONO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            'WHILE ADDING COLUMN IN JOBOUT DONT FORGET TO ADD SAME COLUMNS IN FORMS GIVEN BELOW
            '1) JOBOUT -- RECOSAVE
            '2) OPENINGGDN --- RECOSAVE
            '3) PACKINGSLIP --- RECOSAVE
            '4) GDN --- RECOSAVE
            '5) PROFORMA --- RECOSAVE
            '6) UNCHECKED STOCK REPORT

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


            alParaval.Add(Format(DTRECODATE.Value.Date, "MM/dd/yyyy"))
            alParaval.Add(CMBGODOWN.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(txtchallan.Text.Trim)
            alParaval.Add(Val(LBLTOTALOUTPCS.Text))
            alParaval.Add(Val(LBLTOTALOUTMTRS.Text))
            alParaval.Add(Val(LBLTOTALINPCS.Text))
            alParaval.Add(Val(LBLTOTALINMTRS.Text))

            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)


            Dim gridsrno As String = ""
            Dim PIECETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim LOTNO As String = ""
            Dim PCS As String = ""
            Dim UNIT As String = ""
            Dim MTRS As String = ""

            Dim BARCODE As String = "" 'BARCODE ADDED
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""
            Dim RATE As String = ""
            Dim PER As String = ""
            Dim AMOUNT As String = ""
            Dim BALENO As String = ""



            For Each row As Windows.Forms.DataGridViewRow In GRIDSTOCK.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(GSRNO.Index).Value.ToString

                        PIECETYPE = row.Cells(GPIECETYPE.Index).Value.ToString
                        ITEMNAME = row.Cells(GMERCHANT.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(GCOLOR.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        PCS = row.Cells(GPCS.Index).Value.ToString
                        UNIT = row.Cells(GUNIT.Index).Value.ToString
                        MTRS = row.Cells(GMTRS.Index).Value.ToString
                        RATE = row.Cells(GRATE.Index).Value
                        PER = row.Cells(GPER.Index).Value
                        AMOUNT = row.Cells(GAMOUNT.Index).Value
                        BALENO = row.Cells(GBALENO.Index).Value.ToString
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        FROMNO = row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = row.Cells(GFROMSRNO.Index).Value.ToString
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString

                    Else
                        gridsrno = gridsrno & "|" & row.Cells(GSRNO.Index).Value.ToString

                        PIECETYPE = PIECETYPE & "|" & row.Cells(GPIECETYPE.Index).Value.ToString
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GMERCHANT.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(GCOLOR.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        PCS = PCS & "|" & row.Cells(GPCS.Index).Value.ToString
                        UNIT = UNIT & "|" & row.Cells(GUNIT.Index).Value.ToString
                        MTRS = MTRS & "|" & row.Cells(GMTRS.Index).Value.ToString
                        RATE = RATE & "|" & row.Cells(GRATE.Index).Value
                        PER = PER & "|" & row.Cells(GPER.Index).Value
                        AMOUNT = AMOUNT & "|" & row.Cells(GAMOUNT.Index).Value
                        BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = FROMSRNO & "|" & row.Cells(GFROMSRNO.Index).Value.ToString
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(PIECETYPE)
            alParaval.Add(ITEMNAME)
            alParaval.Add(QUALITY)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(LOTNO)
            alParaval.Add(PCS)
            alParaval.Add(UNIT)
            alParaval.Add(MTRS)
            alParaval.Add(RATE)
            alParaval.Add(PER)
            alParaval.Add(AMOUNT)
            alParaval.Add(BALENO)
            alParaval.Add(BARCODE)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)



            Dim INGRIDSRNO As String = ""
            Dim INPIECETYPE As String = ""
            Dim INITEMNAME As String = ""
            Dim INQUALITY As String = ""
            Dim INBALENO As String = ""
            Dim INGRIDDESC As String = ""
            Dim INLOTNO As String = ""
            Dim INDESIGN As String = ""
            Dim INCOLOR As String = ""
            Dim INCUT As String = ""
            Dim INPCS As String = ""
            Dim INQTYUNIT As String = ""
            Dim INMTRS As String = ""
            Dim INRACK As String = ""
            Dim INSHELF As String = ""
            Dim INBARCODE As String = ""
            Dim INDONE As String = ""
            Dim INOUTPCS As String = ""
            Dim INOUTMTRS As String = ""
            Dim INRATE As String = ""
            Dim INPER As String = ""
            Dim INAMOUNT As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDSTOCKIN.Rows
                If row.Cells(0).Value <> Nothing Then
                    If INGRIDSRNO = "" Then
                        INGRIDSRNO = row.Cells(GINSRNO.Index).Value.ToString
                        INPIECETYPE = row.Cells(GINPIECETYPE.Index).Value.ToString
                        INITEMNAME = row.Cells(GINITEMNAME.Index).Value.ToString
                        INQUALITY = row.Cells(GINQUALITY.Index).Value.ToString
                        INBALENO = row.Cells(GINBALENO.Index).Value.ToString
                        INGRIDDESC = row.Cells(GINDESC.Index).Value.ToString
                        INLOTNO = row.Cells(GINLOTNO.Index).Value.ToString
                        INDESIGN = row.Cells(GINDESIGN.Index).Value.ToString
                        INCOLOR = row.Cells(GINCOLOR.Index).Value.ToString
                        INCUT = row.Cells(GINCUT.Index).Value.ToString
                        INPCS = row.Cells(GINPCS.Index).Value.ToString
                        INQTYUNIT = row.Cells(GINUNIT.Index).Value.ToString
                        INMTRS = row.Cells(GINMTRS.Index).Value
                        INRATE = row.Cells(GINRATE.Index).Value
                        INPER = row.Cells(GINPER.Index).Value
                        INAMOUNT = row.Cells(GINAMOUNT.Index).Value
                        INRACK = row.Cells(GRACK.Index).Value.ToString
                        INSHELF = row.Cells(GSHELF.Index).Value.ToString
                        INBARCODE = row.Cells(GINBARCODE.Index).Value
                        If row.Cells(GINDONE.Index).Value = True Then
                            INDONE = 1
                        Else
                            INDONE = 0
                        End If
                        INOUTPCS = row.Cells(GINOUTPCS.Index).Value
                        INOUTMTRS = row.Cells(GINOUTMTRS.Index).Value

                    Else

                        INGRIDSRNO = INGRIDSRNO & "|" & row.Cells(GINSRNO.Index).Value
                        INPIECETYPE = INPIECETYPE & "|" & row.Cells(GINPIECETYPE.Index).Value
                        INITEMNAME = INITEMNAME & "|" & row.Cells(GINITEMNAME.Index).Value
                        INQUALITY = INQUALITY & "|" & row.Cells(GINQUALITY.Index).Value.ToString
                        INBALENO = INBALENO & "|" & row.Cells(GINBALENO.Index).Value.ToString
                        INGRIDDESC = INGRIDDESC & "|" & row.Cells(GINDESC.Index).Value.ToString
                        INLOTNO = INLOTNO & "|" & row.Cells(GINLOTNO.Index).Value.ToString
                        INDESIGN = INDESIGN & "|" & row.Cells(GINDESIGN.Index).Value.ToString
                        INCOLOR = INCOLOR & "|" & row.Cells(GINCOLOR.Index).Value.ToString
                        INCUT = INCUT & "|" & row.Cells(GINCUT.Index).Value
                        INPCS = INPCS & "|" & row.Cells(GINPCS.Index).Value
                        INQTYUNIT = INQTYUNIT & "|" & row.Cells(GINUNIT.Index).Value
                        INMTRS = INMTRS & "|" & row.Cells(GINMTRS.Index).Value
                        INRATE = INRATE & "|" & row.Cells(GINRATE.Index).Value
                        INPER = INPER & "|" & row.Cells(GINPER.Index).Value
                        INAMOUNT = INAMOUNT & "|" & row.Cells(GINAMOUNT.Index).Value
                        INRACK = INRACK & "," & row.Cells(GRACK.Index).Value.ToString
                        INSHELF = INSHELF & "," & row.Cells(GSHELF.Index).Value.ToString
                        INBARCODE = INBARCODE & "|" & row.Cells(GINBARCODE.Index).Value
                        If row.Cells(GINDONE.Index).Value = True Then
                            INDONE = INDONE & "|" & "1"
                        Else
                            INDONE = INDONE & "|" & "0"
                        End If
                        INOUTPCS = INOUTPCS & "|" & row.Cells(GINOUTPCS.Index).Value
                        INOUTMTRS = INOUTMTRS & "|" & row.Cells(GINOUTMTRS.Index).Value

                    End If
                End If
            Next

            alParaval.Add(INGRIDSRNO)
            alParaval.Add(INPIECETYPE)
            alParaval.Add(INITEMNAME)
            alParaval.Add(INQUALITY)
            alParaval.Add(INBALENO)
            alParaval.Add(INGRIDDESC)
            alParaval.Add(INLOTNO)
            alParaval.Add(INDESIGN)
            alParaval.Add(INCOLOR)
            alParaval.Add(INCUT)
            alParaval.Add(INPCS)
            alParaval.Add(INQTYUNIT)
            alParaval.Add(INMTRS)
            alParaval.Add(INRATE)
            alParaval.Add(INPER)
            alParaval.Add(INAMOUNT)
            alParaval.Add(INRACK)
            alParaval.Add(INSHELF)
            alParaval.Add(INBARCODE)
            alParaval.Add(INDONE)
            alParaval.Add(INOUTPCS)
            alParaval.Add(INOUTMTRS)

            alParaval.Add(CMBNAME.Text.Trim)


            alParaval.Add(Val(LBLTOTALAMOUNT.Text.Trim))
            alParaval.Add(Val(LBLTOTALINAMOUNT.Text.Trim))

            If CHKMANUALRATE.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(Val(LBLAVGRATE.Text.Trim))
            alParaval.Add(CMBCONTRACTOR.Text.Trim)

            Dim objSTOCK As New ClsStockAdjustment()
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
            If GRIDSTOCKIN.RowCount > 0 Then PRINTBARCODE()


            TEMPPROFORMANO = 0
            CLEAR()
            DTRECODATE.Focus()

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

                'GET FRESH DATA FROM DATABASE (ONLY GRID)
                'THIS IS DONE COZ FOR MULTIUSER THE NOS WILL BE SAME
                'SO WE WILL ADD BARCODE IN SP AND THEN FETCH THAT DATA HERE AFTER THAT WE WILL PRINT BARCODES
                GRIDSTOCKIN.RowCount = 0
                Dim OBJCMN1 As New ClsCommon
                Dim DT1 As DataTable = OBJCMN1.SEARCH("ISNULL(STOCKADJUSTMENT_INDESC.SA_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(STOCKADJUSTMENT_INDESC.SA_BALENO, '')  AS BALENO, ISNULL(STOCKADJUSTMENT_INDESC.SA_GRIDDESC, '') AS GRIDDESC, ISNULL(STOCKADJUSTMENT_INDESC.SA_LOTNO, '') AS LOTNO, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(STOCKADJUSTMENT_INDESC.SA_CUT, 0) AS CUT, ISNULL(STOCKADJUSTMENT_INDESC.SA_QTY, 0) AS QTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT,  ISNULL(STOCKADJUSTMENT_INDESC.SA_MTRS, 0) AS MTRS, ISNULL(STOCKADJUSTMENT_INDESC.SA_RATE, 0) AS RATE, ISNULL(STOCKADJUSTMENT_INDESC.SA_PER, 'Mtrs') AS PER, ISNULL(STOCKADJUSTMENT_INDESC.SA_AMOUNT, 0) AS AMOUNT, ISNULL(STOCKADJUSTMENT_INDESC.SA_BARCODE, '') AS BARCODE, ISNULL(STOCKADJUSTMENT_INDESC.SA_OUTPCS, 0) AS OUTPCS, ISNULL(STOCKADJUSTMENT_INDESC.SA_OUTMTRS, 0) AS OUTMTRS, STOCKADJUSTMENT_INDESC.SA_GRIDDONE AS GRIDDONE, ISNULL(SHELFMASTER.SHELF_NAME, '') AS INSHELF, ISNULL(RACKMASTER.RACK_NAME, '') AS INRACK ", "", " STOCKADJUSTMENT_INDESC INNER JOIN PIECETYPEMASTER ON STOCKADJUSTMENT_INDESC.SA_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN RACKMASTER ON STOCKADJUSTMENT_INDESC.SA_RACKID = RACKMASTER.RACK_ID LEFT OUTER JOIN SHELFMASTER ON STOCKADJUSTMENT_INDESC.SA_SHELFID = SHELFMASTER.SHELF_ID LEFT OUTER JOIN UNITMASTER ON STOCKADJUSTMENT_INDESC.SA_QTYUNITID = UNITMASTER.unit_id LEFT OUTER JOIN DESIGNMASTER AS DESIGNMASTER ON STOCKADJUSTMENT_INDESC.SA_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON STOCKADJUSTMENT_INDESC.SA_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON STOCKADJUSTMENT_INDESC.SA_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN  ITEMMASTER AS ITEMMASTER ON STOCKADJUSTMENT_INDESC.SA_ITEMID = ITEMMASTER.item_id ", " AND SA_NO = " & TEMPRECONO & " AND SA_YEARID = " & YearId & " ORDER BY STOCKADJUSTMENT_INDESC.SA_GRIDSRNO")
                For Each DR As DataRow In DT1.Rows
                    GRIDSTOCKIN.Rows.Add(DR("GRIDSRNO").ToString, DR("PIECETYPE"), DR("ITEM").ToString, DR("QUALITY").ToString, DR("BALENO").ToString, DR("GRIDDESC"), DR("LOTNO"), DR("DESIGN").ToString, DR("COLOR"), Format(Val(DR("CUT")), "0.00"), Format(Val(DR("qty")), "0.00"), DR("UNIT").ToString, Format(Val(DR("MTRS")), "0.00"), Format(Val(DR("RATE")), "0.00"), DR("PER").ToString, Format(Val(DR("AMOUNT")), "0.00"), DR("INRACK").ToString, DR("INSHELF").ToString, DR("BARCODE"), 0, DR("OUTPCS"), DR("OUTMTRS"))
                Next


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

                For Each ROW As DataGridViewRow In GRIDSTOCKIN.Rows
                    'TO PRINT BARCODE FROM SELECTED SRNO
                    If (Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0) Then
                        If Val(ROW.Cells(GSRNO.Index).Value) < Val(TXTFROM.Text.Trim) Or Val(ROW.Cells(GSRNO.Index).Value) > Val(TXTTO.Text.Trim) Then GoTo NEXTLINE
                    End If

                    'IF barcode is used the BARCODE printING WILL BE BLOCKED
                    If Val(ROW.Cells(GINOUTMTRS.Index).Value) > 0 Then GoTo NEXTLINE

                    BARCODEPRINTING(ROW.Cells(GINBARCODE.Index).Value, ROW.Cells(GINPIECETYPE.Index).Value, ROW.Cells(GINITEMNAME.Index).Value, ROW.Cells(GINQUALITY.Index).Value, ROW.Cells(GINDESIGN.Index).Value, ROW.Cells(GINCOLOR.Index).Value, ROW.Cells(GINUNIT.Index).Value, ROW.Cells(GINLOTNO.Index).Value, ROW.Cells(GINBALENO.Index).Value, ROW.Cells(GINDESC.Index).Value, Val(ROW.Cells(GINMTRS.Index).Value), Val(ROW.Cells(GINPCS.Index).Value), Val(ROW.Cells(GINCUT.Index).Value), ROW.Cells(GRACK.Index).Value, TEMPHEADER, SUPRIYAHEADER, WHOLESALEBARCODE, "", "", ROW.Cells(GSHELF.Index).Value, DTRECODATE.Text)
NEXTLINE:

                Next
            End If

        Catch ex As Exception
            Throw ex
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
            LBLTOTALOUTMTRS.Text = 0.0
            LBLTOTALOUTPCS.Text = 0.0
            LBLTOTALINMTRS.Text = 0.0
            LBLTOTALINPCS.Text = 0.0
            LBLTOTALAMOUNT.Text = 0.0
            LBLTOTALINAMOUNT.Text = 0.00
            LBLAVGRATE.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALOUTPCS.Text = Format(Val(LBLTOTALOUTPCS.Text) + Val(ROW.Cells(GPCS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALOUTMTRS.Text = Format(Val(LBLTOTALOUTMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    If ROW.Cells(GPER.Index).EditedFormattedValue = "Mtrs" Then
                        ROW.Cells(GAMOUNT.Index).Value = Format(Val(ROW.Cells(GRATE.Index).EditedFormattedValue) * Val(ROW.Cells(GMTRS.Index).EditedFormattedValue))
                    Else
                        ROW.Cells(GAMOUNT.Index).Value = Format(Val(ROW.Cells(GRATE.Index).EditedFormattedValue) * Val(ROW.Cells(GPCS.Index).EditedFormattedValue))
                    End If
                    LBLTOTALAMOUNT.Text = Format(Val(LBLTOTALAMOUNT.Text) + Val(ROW.Cells(GAMOUNT.Index).EditedFormattedValue), "0.00")
                    LBLAVGRATE.Text = Format(Val(LBLAVGRATE.Text) + Val(ROW.Cells(GRATE.Index).EditedFormattedValue) / GRIDSTOCK.Rows.Count, "0.00")
                End If
            Next

            For Each ROW As DataGridViewRow In GRIDSTOCKIN.Rows
                If ROW.Cells(GINSRNO.Index).Value <> Nothing Then
                    If ROW.Cells(GINCUT.Index).EditedFormattedValue > 0 Then ROW.Cells(GINMTRS.Index).Value = Val(ROW.Cells(GINPCS.Index).EditedFormattedValue) * Val(ROW.Cells(GINCUT.Index).EditedFormattedValue)
                    LBLTOTALINPCS.Text = Format(Val(LBLTOTALINPCS.Text) + Val(ROW.Cells(GINPCS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALINMTRS.Text = Format(Val(LBLTOTALINMTRS.Text) + Val(ROW.Cells(GINMTRS.Index).EditedFormattedValue), "0.00")
                    If Val(ROW.Cells(GINRATE.Index).EditedFormattedValue) = 0 And Val(LBLAVGRATE.Text) > 0 And CHKMANUALRATE.Checked = False Then ROW.Cells(GINRATE.Index).Value = Format(Val(LBLAVGRATE.Text), "0.00")
                    If ROW.Cells(GINPER.Index).EditedFormattedValue = "Mtrs" Then
                        ROW.Cells(GINAMOUNT.Index).Value = Format(Val(ROW.Cells(GINRATE.Index).EditedFormattedValue) * Val(ROW.Cells(GINMTRS.Index).EditedFormattedValue))
                    Else
                        ROW.Cells(GINAMOUNT.Index).Value = Format(Val(ROW.Cells(GINRATE.Index).EditedFormattedValue) * Val(ROW.Cells(GINPCS.Index).EditedFormattedValue))
                    End If
                    LBLTOTALINAMOUNT.Text = Format(Val(LBLTOTALINAMOUNT.Text) + Val(ROW.Cells(GINAMOUNT.Index).EditedFormattedValue), "0.00")
                End If
            Next
            TXTMTRSDIFF.Text = Format(Val(LBLTOTALOUTMTRS.Text) - Val(LBLTOTALINMTRS.Text), "0.00")
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

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then NAMEVALIDATE(cmbtrans, CMBCODE, e, Me, TXTTRANSADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSTOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTSTOCK.Click
        Try
            Dim DTJO As New DataTable
            Dim OBJSELECTGDN As New SelectStockGDN
            OBJSELECTGDN.GODOWN = CMBGODOWN.Text.Trim
            OBJSELECTGDN.ShowDialog()
            DTJO = OBJSELECTGDN.DT
            If DTJO.Rows.Count > 0 Then


                For Each DTROWPS As DataRow In DTJO.Rows

                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    If DTROWPS("BARCODE") <> "" Then
                        For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                            If LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(DTROWPS("BARCODE")) Then GoTo LINE1
                        Next
                    End If

                    TXTLOTNO.Text = DTROWPS("LOTNO")

                    If ClientName = "AMAN" Or ClientName = "AARYA" Then
                        CMBNAME.Text = DTROWPS("JOBBERNAME")
                        txtchallan.Text = DTROWPS("BALENO")
                    End If

                    If ClientName = "SAKARIA" Or ClientName = "CC" Or ClientName = "C3" Then CMBNAME.Text = DTROWPS("PURNAME")

                    GRIDSTOCK.Rows.Add(0, DTROWPS("PIECETYPE"), DTROWPS("ITEMNAME"), DTROWPS("QUALITY"), DTROWPS("DESIGNNO"), DTROWPS("COLOR"), DTROWPS("LOTNO"), Format(Val(DTROWPS("PCS")), "0.00"), DTROWPS("UNIT"), Format(Val(DTROWPS("MTRS")), "0.00"), Val(DTROWPS("PURRATE")), "Mtrs", 0, DTROWPS("BALENO"), DTROWPS("BARCODE"), DTROWPS("FROMNO"), DTROWPS("FROMSRNO"), DTROWPS("TYPE"))
                    If CHKYARDMTR.Checked = True And CHKCOPY.Checked = True Then
                        DTROWPS("MTRS") = Format(Val(DTROWPS("MTRS")) * 0.9144, "0.00")
                        DTROWPS("UNIT") = "Mtrs"
                    End If
                    If CHKCOPY.Checked = True Then GRIDSTOCKIN.Rows.Add(0, DTROWPS("PIECETYPE"), DTROWPS("ITEMNAME"), DTROWPS("QUALITY"), DTROWPS("BALENO"), "", DTROWPS("LOTNO"), DTROWPS("DESIGNNO"), DTROWPS("COLOR"), 0, Format(Val(DTROWPS("PCS")), "0.00"), DTROWPS("UNIT"), Format(Val(DTROWPS("MTRS")), "0.00"), Val(DTROWPS("PURRATE")), "Mtrs", 0, "", "", "", 0, 0, 0)

LINE1:
                Next
                'CMDSELECTSTOCK.Enabled = False
                GETSRNO(GRIDSTOCK)
                GETSRNO(GRIDSTOCKIN)
                TOTAL()
            End If


            'CHANGES AS PER REQUOREMENT
            If ClientName = "AVIS" And GRIDSTOCK.RowCount > 0 Then
                CMBPIECETYPE.Text = GRIDSTOCK.Rows(0).Cells(GPIECETYPE.Index).Value
                cmbitemname.Text = GRIDSTOCK.Rows(0).Cells(GMERCHANT.Index).Value
                CMBQUALITY.Text = GRIDSTOCK.Rows(0).Cells(GQUALITY.Index).Value
                CMBDESIGN.Text = GRIDSTOCK.Rows(0).Cells(GDESIGN.Index).Value
                cmbcolor.Text = GRIDSTOCK.Rows(0).Cells(GCOLOR.Index).Value

                TXTMTRS.Text = Val(LBLTOTALOUTMTRS.Text.Trim)
                TXTRATE.Text = Val(LBLAVGRATE.Text.Trim)
                TXTGRIDDESC.Clear()
                For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                    If TXTGRIDDESC.Text = "" Then
                        TXTGRIDDESC.Text = "(" & Val(ROW.Cells(GMTRS.Index).Value)
                    Else
                        TXTGRIDDESC.Text = TXTGRIDDESC.Text & " + " & Val(ROW.Cells(GMTRS.Index).Value)
                    End If
                Next
                If TXTGRIDDESC.Text.Trim <> "" Then TXTGRIDDESC.Text = TXTGRIDDESC.Text & ")"
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDSTOCK.RowCount = 0
            GRIDSTOCKIN.RowCount = 0
LINE1:
            TEMPRECONO = Val(TXTRECONO.Text) - 1
            If TEMPRECONO > 0 Then
                EDIT = True
                StockReco_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
                TEMPPROFORMANO = 0
            End If
            If GRIDSTOCK.RowCount = 0 And GRIDSTOCKIN.RowCount = 0 And TEMPRECONO > 1 Then
                TXTRECONO.Text = TEMPRECONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
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
                StockReco_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
                TEMPPROFORMANO = 0
            End If
            If GRIDSTOCK.RowCount = 0 And GRIDSTOCKIN.RowCount = 0 And TEMPRECONO < MAXNO Then
                TXTRECONO.Text = TEMPRECONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDSTOCK.RowCount = 0
                GRIDSTOCKIN.RowCount = 0
                TEMPRECONO = Val(tstxtbillno.Text)
                If TEMPRECONO > 0 Then
                    EDIT = True
                    StockReco_Load(sender, e)
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
                GRIDSTOCKIN.Rows.Add(Val(txtsrno.Text.Trim), CMBPIECETYPE.Text.Trim, cmbitemname.Text.Trim, CMBQUALITY.Text.Trim, TXTBALENO.Text.Trim, TXTGRIDDESC.Text.Trim, TXTLOTNO.Text.Trim, CMBDESIGN.Text.Trim, cmbcolor.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(txtqty.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Format(Val(TXTAMOUNT.Text.Trim), "0.00"), CMBRACK.Text.Trim, CMBSHELF.Text.Trim, TXTINBARCODE.Text.Trim, 0, 0, 0)
                GETSRNO(GRIDSTOCKIN)

            ElseIf GRIDDOUBLECLICK = True Then

                If ClientName = "SONU" Then
                    Dim TEMPITEM As String = ""
                    Dim TEMPSHADE As String = ""
                    For I As Integer = TEMPROW + 1 To GRIDSTOCKIN.RowCount - 1
                        If I = GRIDSTOCKIN.CurrentRow.Index + 1 Then
                            TEMPITEM = GRIDSTOCKIN.Item(GINITEMNAME.Index, I - 1).Value
                            TEMPSHADE = GRIDSTOCKIN.Item(GINCOLOR.Index, I - 1).Value
                        End If
                        If GRIDSTOCKIN.Item(GINITEMNAME.Index, I).Value = TEMPITEM Then GRIDSTOCKIN.Item(GINITEMNAME.Index, I).Value = cmbitemname.Text.Trim
                        If cmbcolor.Text.Trim <> "" And GRIDSTOCKIN.Item(GINCOLOR.Index, I).Value = TEMPSHADE Then GRIDSTOCKIN.Item(GINCOLOR.Index, I).Value = cmbcolor.Text.Trim
                    Next
                End If


                GRIDSTOCKIN.Item(GINSRNO.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
                GRIDSTOCKIN.Item(GINPIECETYPE.Index, TEMPROW).Value = CMBPIECETYPE.Text.Trim
                GRIDSTOCKIN.Item(GINITEMNAME.Index, TEMPROW).Value = cmbitemname.Text.Trim
                GRIDSTOCKIN.Item(GINQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
                GRIDSTOCKIN.Item(GINBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim
                GRIDSTOCKIN.Item(GINDESC.Index, TEMPROW).Value = TXTGRIDDESC.Text.Trim
                GRIDSTOCKIN.Item(GINLOTNO.Index, TEMPROW).Value = TXTLOTNO.Text.Trim
                GRIDSTOCKIN.Item(GINDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
                GRIDSTOCKIN.Item(GINCOLOR.Index, TEMPROW).Value = cmbcolor.Text.Trim
                GRIDSTOCKIN.Item(GINCUT.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
                GRIDSTOCKIN.Item(GINPCS.Index, TEMPROW).Value = Val(txtqty.Text.Trim)
                GRIDSTOCKIN.Item(GINUNIT.Index, TEMPROW).Value = cmbqtyunit.Text.Trim
                GRIDSTOCKIN.Item(GINMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
                GRIDSTOCKIN.Item(GINRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")
                GRIDSTOCKIN.Item(GINPER.Index, TEMPROW).Value = CMBPER.Text.Trim
                GRIDSTOCKIN.Item(GINAMOUNT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text.Trim), "0.00")
                GRIDSTOCKIN.Item(GRACK.Index, TEMPROW).Value = CMBRACK.Text.Trim
                GRIDSTOCKIN.Item(GSHELF.Index, TEMPROW).Value = CMBSHELF.Text.Trim



                GRIDDOUBLECLICK = False
            End If

            TOTAL()

            If ClientName <> "SNCM" Then GRIDSTOCKIN.FirstDisplayedScrollingRowIndex = GRIDSTOCKIN.RowCount - 1

            txtsrno.Text = GRIDSTOCKIN.RowCount + 1
            If ClientName = "SOFTAS" Then CMBQUALITY.Text = ""
            TXTBALENO.Clear()
            TXTGRIDDESC.Clear()
            If ClientName <> "AVIS" And ClientName <> "TINUMINU" And ClientName <> "SIDDHGIRI" Then TXTLOTNO.Clear()
            TXTMTRS.Clear()
            'TXTRATE.Clear()
            TXTAMOUNT.Clear()
            If ClientName = "SOFTAS" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then TXTMTRS.Focus() Else CMBPIECETYPE.Focus()
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
                txtsrno.Text = GRIDSTOCKIN.Item(GINSRNO.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBPIECETYPE.Text = GRIDSTOCKIN.Item(GINPIECETYPE.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                cmbitemname.Text = GRIDSTOCKIN.Item(GINITEMNAME.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDSTOCKIN.Item(GINQUALITY.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTBALENO.Text = GRIDSTOCKIN.Item(GINBALENO.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTGRIDDESC.Text = GRIDSTOCKIN.Item(GINDESC.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTLOTNO.Text = GRIDSTOCKIN.Item(GINLOTNO.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDSTOCKIN.Item(GINDESIGN.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                cmbcolor.Text = GRIDSTOCKIN.Item(GINCOLOR.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTCUT.Text = GRIDSTOCKIN.Item(GINCUT.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                txtqty.Text = GRIDSTOCKIN.Item(GINPCS.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                cmbqtyunit.Text = GRIDSTOCKIN.Item(GINUNIT.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTMTRS.Text = GRIDSTOCKIN.Item(GINMTRS.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTRATE.Text = GRIDSTOCKIN.Item(GINRATE.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBPER.Text = GRIDSTOCKIN.Item(GINPER.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTAMOUNT.Text = GRIDSTOCKIN.Item(GINAMOUNT.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBRACK.Text = GRIDSTOCKIN.Item(GRACK.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBSHELF.Text = GRIDSTOCKIN.Item(GSHELF.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDSTOCKIN.CurrentRow.Index
                txtsrno.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBRACK_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBRACK.Enter
        Try
            If CMBRACK.Text.Trim = "" Then FILLRACK(CMBRACK)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBRACK_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBRACK.Validating
        Try
            If CMBRACK.Text.Trim <> "" Then RACKVALIDATE(CMBRACK, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHELF_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSHELF.Enter
        Try
            If CMBSHELF.Text.Trim = "" Then FILLSHELF(CMBSHELF)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHELF_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSHELF.Validated
        Try
            If CMBPIECETYPE.Text.Trim <> "" And cmbitemname.Text.Trim <> "" And Val(txtqty.Text.Trim) > 0 And cmbqtyunit.Text.Trim <> "" Then
                If ClientName <> "MOMAI" And ClientName <> "AXIS" And ClientName <> "GELATO" And Val(TXTMTRS.Text.Trim) = 0 Then Exit Sub
                If ClientName = "SBA" Or ClientName = "KARAN" Or ClientName = "RMANILAL" Or ClientName = "SST" Or ClientName = "MBB" Or (ClientName = "AXIS" And Val(TXTMTRS.Text.Trim) = 0) Then
                    Dim TEMPQTY As Integer = Val(txtqty.Text.Trim)
                    If Val(TXTNOOFENTRIES.Text.Trim) = 0 Then txtqty.Text = 1 Else txtqty.Text = Val(TXTNOOFENTRIES.Text.Trim)
                    If Val(TXTCUT.Text.Trim) > 0 Then TXTMTRS.Text = Val(TXTCUT.Text.Trim) * Val(txtqty.Text.Trim)
                    For I As Integer = 1 To Val(TEMPQTY)
                        If GRIDDOUBLECLICK = False Then
                            If EDIT = True Then
                                'GET LAST BARCODE SRNO
                                Dim LSRNO As Integer = 0
                                Dim RSRNO As Integer = 0
                                Dim SNO As Integer = 0
                                If GRIDSTOCKIN.RowCount > 0 Then
                                    LSRNO = InStr(GRIDSTOCKIN.Rows(GRIDSTOCKIN.RowCount - 1).Cells(GINBARCODE.Index).Value, "/")
                                    RSRNO = InStr(LSRNO + 1, GRIDSTOCKIN.Rows(GRIDSTOCKIN.RowCount - 1).Cells(GINBARCODE.Index).Value, "/")
                                    SNO = GRIDSTOCKIN.Rows(GRIDSTOCKIN.RowCount - 1).Cells(GINBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)
                                End If

                                TXTINBARCODE.Text = "A-" & Val(TXTRECONO.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                            Else
                                TXTINBARCODE.Text = "A-" & Val(TXTRECONO.Text.Trim) & "/" & GRIDSTOCKIN.RowCount + 1 & "/" & YearId
                            End If
                        End If
                        FILLGRID()
                    Next
                Else
                    If GRIDDOUBLECLICK = False Then
                        If EDIT = True Then
                            'GET LAST BARCODE SRNO
                            Dim LSRNO As Integer = 0
                            Dim RSRNO As Integer = 0
                            Dim SNO As Integer = 0
                            If GRIDSTOCKIN.RowCount > 0 Then
                                LSRNO = InStr(GRIDSTOCKIN.Rows(GRIDSTOCKIN.RowCount - 1).Cells(GINBARCODE.Index).Value, "/")
                                RSRNO = InStr(LSRNO + 1, GRIDSTOCKIN.Rows(GRIDSTOCKIN.RowCount - 1).Cells(GINBARCODE.Index).Value, "/")
                                SNO = GRIDSTOCKIN.Rows(GRIDSTOCKIN.RowCount - 1).Cells(GINBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)
                            End If

                            TXTINBARCODE.Text = "A-" & Val(TXTRECONO.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                        Else
                            TXTINBARCODE.Text = "A-" & Val(TXTRECONO.Text.Trim) & "/" & GRIDSTOCKIN.RowCount + 1 & "/" & YearId
                        End If
                    End If
                    FILLGRID()
                End If
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHELF_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSHELF.Validating
        Try
            If CMBSHELF.Text.Trim <> "" Then SHELFVALIDATE(CMBSHELF, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtqty.KeyPress, TXTNOOFENTRIES.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTCUT.KeyPress, TXTRATE.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTBARCODE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTBARCODE.Validating
        Try
            If TXTBARCODE.Text.Trim <> "" And CHECKBARCODEERRORVALID = True Then
                'CHECKING WHETHER IS IS GONE OUT OR NOT
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("TOP 1 TYPE, FROMNO", "", " OUTBARCODESTOCK ", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "' AND CMPID = " & CmpId & " AND LOCATIONID = " & Locationid & " AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    MsgBox("Barcode Already Used in " & DT.Rows(0).Item("TYPE") & " Sr No " & DT.Rows(0).Item("FROMNO"))
                    TXTBARCODE.Clear()
                    e.Cancel = True
                    'Else
                    '    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSTOCK_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDSTOCK.CellValidating
        Try
            Dim colNum As Integer = GRIDSTOCK.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GPCS.Index, GMTRS.Index, GRATE.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDSTOCK.CurrentCell.Value = Nothing Then GRIDSTOCK.CurrentCell.Value = "0.00"
                        GRIDSTOCK.CurrentCell.Value = Convert.ToDecimal(GRIDSTOCK.Item(colNum, e.RowIndex).Value)
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

    Private Sub GRIDSTOCK_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDSTOCK.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSTOCK.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDSTOCK.Rows.RemoveAt(GRIDSTOCK.CurrentRow.Index)
                GETSRNO(GRIDSTOCK)
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

                If lbllocked.Visible = True Then
                    MsgBox("Entry Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If


                Dim ALPARAVAL As New ArrayList
                Dim OBSTOCK As New ClsStockAdjustment

                ALPARAVAL.Add(TEMPRECONO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                OBSTOCK.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBSTOCK.DELETE()
                MsgBox("Stock Adjustment Deleted Succesfully")
                CLEAR()
                EDIT = False
                DTRECODATE.Focus()
                TEMPPROFORMANO = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbqtyunit.Validating
        Try
            If cmbqtyunit.Text.Trim <> "" Then unitvalidate(cmbqtyunit, e, Me)
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

    Private Sub CMBCOLOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcolor.Enter
        Try
            If cmbcolor.Text.Trim = "" Then FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcolor.Validating
        Try
            If cmbcolor.Text.Trim <> "" Then COLORVALIDATE(cmbcolor, e, Me, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
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

    Private Sub TXTCUT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTCUT.Validated, txtqty.Validated, TXTRATE.Validated, CMBPER.Validated
        CALC()
    End Sub

    Sub CALC()
        Try
            If Val(txtqty.Text.Trim) > 0 And Val(TXTCUT.Text.Trim) > 0 Then TXTMTRS.Text = Format(Val(txtqty.Text.Trim) * Val(TXTCUT.Text.Trim), "0.00")
            If CMBPER.Text = "Mtrs" Then TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(TXTMTRS.Text.Trim), "0.00") Else TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(txtqty.Text.Trim), "0.00")
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
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbPIECETYPE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPIECETYPE.Enter
        Try
            If CMBPIECETYPE.Text.Trim = "" Then fillPIECETYPE(CMBPIECETYPE)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbPIECETYPE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPIECETYPE.Validating
        Try
            If CMBPIECETYPE.Text.Trim <> "" Then PIECETYPEvalidate(CMBPIECETYPE, e, Me)
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

    Private Sub CMBPIECETYPE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBPIECETYPE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJPieceType As New SelectPieceType
                OBJPieceType.ShowDialog()
                If OBJPieceType.TEMPNAME <> "" Then CMBPIECETYPE.Text = OBJPieceType.TEMPNAME
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

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJstock As New StockRecoDetails
            OBJstock.MdiParent = MDIMain
            OBJstock.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then
                PRINTREPORT()
                If GRIDSTOCKIN.RowCount > 0 Then PRINTBARCODE()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If MsgBox("Wish to Print Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJSA As New SaleOrderDesign
            OBJSA.MdiParent = MDIMain
            OBJSA.FORMULA = "{STOCKADJUSTMENT.SA_NO} = " & Val(TXTRECONO.Text.Trim) & " AND {STOCKADJUSTMENT.SA_YEARID} = " & YearId
            OBJSA.FRMSTRING = "STOCKRECO"
            OBJSA.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Try
            Call cmdok_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StockReco_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "KDFAB" Or ClientName = "KCRAYON" Then GBARCODE.HeaderText = "Desc"
        txtqty.ReadOnly = False

        If ClientName = "SBA" Then TXTNOOFENTRIES.Visible = True
        If ClientName = "SNCM" Then GINMTRS.ReadOnly = False

        If ClientName = "SOFTAS" Then
            CMBPIECETYPE.Text = "FRESH"
            cmbqtyunit.Text = "PCS"
            TXTBALENO.TabStop = False
            CMBQUALITY.TabStop = False
            TXTGRIDDESC.TabStop = False
            TXTLOTNO.TabStop = False
            TXTCUT.TabStop = False
        End If

        If ClientName = "VINIT" Then
            CHKCOPY.Checked = True
            CHKYARDMTR.Checked = True
        End If

        If ITEMCOSTCENTRE = True And UserName = "Admin" Then CHKMANUALRATE.Visible = True

        If ClientName = "PARAS" Then LBLCATEGORY.Visible = True
        'If ClientName = "GELATO" Then
        '    GDESIGN.HeaderText = "Size"
        '    GINDESIGN.HeaderText = "Size"
        'End If

        If ClientName = "SONU" Or ClientName = "KCRAYON" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "MNARESH" Then
            LBLCMPNAME.Visible = True
            CMBCMPNAME.Visible = True
            LBLCHNO.Visible = True
            TXTCHNO.Visible = True
        End If

        If ClientName = "AMAN" Or ClientName = "AARYA" Or ClientName = "VALIANT" Then
            LBLNAME.Visible = True
            CMBNAME.Visible = True
        End If

        If ClientName = "SST" Then
            TXTRECONO.ReadOnly = False
        End If
    End Sub


    Private Sub cmbitemname_Validated(sender As Object, e As EventArgs) Handles cmbitemname.Validated
        Try
            'GET CATEGORY
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("ISNULL(CATEGORY_NAME,'') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEM_CATEGORYID = CATEGORY_ID", " AND ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEM_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                LBLCATEGORY.Text = DT.Rows(0).Item("CATEGORY")
            End If

            If cmbitemname.Text.Trim <> "" And EDIT = False Then
                'GET DISPLAY NAME IN GRIDREMARKS
                If ClientName = "RAJKRIPA" Then
                    DT = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS DISPLAYNAME", "", " ITEMMASTER ", " AND ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        For Each DTROW As DataRow In DT.Rows
                            TXTBALENO.Text = (DT.Rows(0).Item("DISPLAYNAME"))
                        Next
                    End If
                End If
            End If

            If ClientName = "MAHAVIRPOLYCOT" Or ClientName = "YASHVI" Then
                CMBDESIGN.Text = ""
                cmbcolor.Text = ""
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstxtbillno.KeyPress, TXTFROM.KeyPress, TXTTO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable


                If CMBGODOWN.Text.Trim = "" Then
                    MsgBox("Select Godown First", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                TXTBARCODE.Text = TXTBARCODE.Text.Replace(" TRIAL", "")

                'GET DATA FROM BARCODE
                DT = OBJCMN.search("TOP 1 *", "", "BARCODESTOCK", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "' AND DONE = 0 AND CMPID = " & CmpId & " AND LOCATIONID  = " & Locationid & " AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then

                    'VALIDATE GODOWN
                    If DT.Rows(0).Item("GODOWN") <> CMBGODOWN.Text.Trim Then
                        MsgBox("Item Not in Selected Godown", MsgBoxStyle.Critical)
                        TXTBARCODE.Clear()
                        Exit Sub
                    End If

                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                        If LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(TXTBARCODE.Text.Trim) Then GoTo LINE1
                    Next

                    Dim PCS As Double = 0
                    If ClientName = "TCOT" Or ClientName = "VALIANT" Then PCS = Val(DT.Rows(0).Item("PCS")) Else PCS = 1
                    If ClientName = "SAKARIA" Or ClientName = "CC"  Or ClientName = "C3" Then CMBNAME.Text = DT.Rows(0).Item("PURNAME")

                    GRIDSTOCK.Rows.Add(GRIDSTOCK.RowCount + 1, DT.Rows(0).Item("PIECETYPE"), DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("DESIGNNO"), DT.Rows(0).Item("COLOR"), DT.Rows(0).Item("LOTNO"), PCS, DT.Rows(0).Item("UNIT"), Format(Val(DT.Rows(0).Item("MTRS")), "0.00"), Format(Val(DT.Rows(0).Item("PURRATE")), "0.00"), "Mtrs", 0, DT.Rows(0).Item("BALENO"), DT.Rows(0).Item("BARCODE"), DT.Rows(0).Item("FROMNO"), DT.Rows(0).Item("FROMSRNO"), DT.Rows(0).Item("TYPE"))
                    If CHKYARDMTR.Checked = True And CHKCOPY.Checked = True Then
                        DT.Rows(0).Item("MTRS") = Format(Val(DT.Rows(0).Item("MTRS")) * 0.9144, "0.00")
                        DT.Rows(0).Item("UNIT") = "Mtrs"
                    End If
                    If CHKCOPY.Checked = True Then GRIDSTOCKIN.Rows.Add(GRIDSTOCKIN.RowCount + 1, DT.Rows(0).Item("PIECETYPE"), DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("BALENO"), "", DT.Rows(0).Item("LOTNO"), DT.Rows(0).Item("DESIGNNO"), DT.Rows(0).Item("COLOR"), 0, PCS, DT.Rows(0).Item("UNIT"), Format(Val(DT.Rows(0).Item("MTRS")), "0.00"), Format(Val(DT.Rows(0).Item("PURRATE")), "0.00"), "Mtrs", 0, "", "", "", 0, 0, 0)
                    TOTAL()
LINE1:
                    TXTBARCODE.Clear()
                    TXTBARCODE.Focus()
                    'Else
                    '    MsgBox("Invalid Barcode / Barcode already Used", MsgBoxStyle.Critical)
                    '    GoTo LINE1
                    '    Exit Sub
                    GRIDSTOCK.FirstDisplayedScrollingRowIndex = GRIDSTOCK.RowCount - 1


                    'CHANGES AS PER REQUOREMENT
                    If ClientName = "AVIS" Or ClientName = "RMANILAL" Or ClientName = "SUPRIYA" And GRIDSTOCK.RowCount > 0 Then
                        CMBPIECETYPE.Text = GRIDSTOCK.Rows(0).Cells(GPIECETYPE.Index).Value
                        cmbitemname.Text = GRIDSTOCK.Rows(0).Cells(GMERCHANT.Index).Value
                        CMBQUALITY.Text = GRIDSTOCK.Rows(0).Cells(GQUALITY.Index).Value
                        CMBDESIGN.Text = GRIDSTOCK.Rows(0).Cells(GDESIGN.Index).Value
                        cmbcolor.Text = GRIDSTOCK.Rows(0).Cells(GCOLOR.Index).Value

                        TXTMTRS.Text = Val(LBLTOTALOUTMTRS.Text.Trim)
                        TXTRATE.Text = Val(LBLAVGRATE.Text.Trim)
                        TXTGRIDDESC.Clear()
                        For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                            If TXTGRIDDESC.Text = "" Then
                                TXTGRIDDESC.Text = "(" & Val(ROW.Cells(GMTRS.Index).Value)
                            Else
                                TXTGRIDDESC.Text = TXTGRIDDESC.Text & " + " & Val(ROW.Cells(GMTRS.Index).Value)
                            End If
                        Next
                        If TXTGRIDDESC.Text.Trim <> "" Then TXTGRIDDESC.Text = TXTGRIDDESC.Text & ")"
                    End If

                Else
                    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                    TXTBARCODE.Clear()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validated(sender As Object, e As EventArgs) Handles CMBDESIGN.Validated
        Try
            'GET ITEMNAME AUTO
            If (ClientName = "AVIS" Or ClientName = "KRISHNA" Or ClientName = "NTC") And CMBDESIGN.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(ITEM_NAME,'') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEM_ID", " AND DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' AND DESIGN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then cmbitemname.Text = DT.Rows(0).Item("ITEMNAME")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHNO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTCHNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTCHNO_Validated(sender As Object, e As EventArgs) Handles TXTCHNO.Validated
        Try
            If (ClientName = "SONU" Or ClientName = "KCRAYON" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "MNARESH") And CMBCMPNAME.Text.Trim <> "" And Val(TXTCHNO.Text.Trim) > 0 And EDIT = False Then
                'GET YEARID FROM SELECTED CMP 
                Dim TEMPYEARID As Integer = 0
                Dim TEMPCMPID As Integer = 0
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("YEAR_ID AS YEARID, YEAR_CMPID AS CMPID", "", "YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMP_NAME = '" & CMBCMPNAME.Text.Trim & "' AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "'")
                If DT.Rows.Count > 0 Then
                    TEMPYEARID = DT.Rows(0).Item("YEARID")
                    TEMPCMPID = DT.Rows(0).Item("CMPID")
                End If



                'NOW FETCH CHALLAN DATA
                Dim ALPARAVAL As New ArrayList
                Dim objclsGDN As New ClsStockAdjustment()
                Dim dttable As DataTable = objclsGDN.SELECTSTOCKADJUSTMENT(Val(TXTCHNO.Text.Trim), TEMPCMPID, Locationid, TEMPYEARID)
                If dttable.Rows.Count > 0 Then

                    If MsgBox("Fetch data from Entry No " & TXTCHNO.Text.Trim & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                    For Each dr As DataRow In dttable.Rows

                        'CHECKING WHETHER ITEM IS PRESENT IN CURRENT YEAR OR NOT, IF NOT PRESENT THEN ADD NEW ITEM
                        If dr("ITEMNAME") <> "" Then
                            DT = OBJCMN.search("ITEM_ID AS ITEMID", "", " ITEMMASTER ", " AND ITEM_NAME = '" & dr("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count = 0 Then
                                'ADD NEW ITEMNAME 
                                ALPARAVAL.Clear()


                                ALPARAVAL.Add("Finished Goods")
                                ALPARAVAL.Add("")   'CATEGORY
                                ALPARAVAL.Add(UCase(dr("ITEMNAME")))        'DISPLAYNAME
                                ALPARAVAL.Add(UCase(dr("ITEMNAME"))) 'ITEMNAME

                                ALPARAVAL.Add("")   'DEPARTMENT
                                ALPARAVAL.Add(UCase(dr("ITEMNAME")))        'CODE
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

                                Dim DTHSN As DataTable = OBJCMN.search("ISNULL(HSN_ID, 0) AS HSNCODEID", "", " HSNMASTER", " AND HSN_CODE = '" & dr("HSNCODE") & "' AND HSN_YEARID = " & YearId)
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
                                ALPARAVAL.Add("")   'GREYCATGORY

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


                        If dr("DESIGNNO") <> "" Then
                            dttable = OBJCMN.search("DESIGN_ID AS DESIGNID", "", "DESIGNMASTER", " AND DESIGN_NO = '" & dr("DESIGNNO") & "' AND DESIGN_YEARID = " & YearId)
                            If dttable.Rows.Count = 0 Then
                                'ADD NEW DESIGN
                                Dim OBJDESIGN As New ClsDesignMaster
                                OBJDESIGN.alParaval.Add(UCase(dr("DESIGNNO")))
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

                                OBJDESIGN.alParaval.Add("") 'ITEM
                                OBJDESIGN.alParaval.Add(0)  'BLOCKED

                                OBJDESIGN.alParaval.Add(CmpId)
                                OBJDESIGN.alParaval.Add(Locationid)
                                OBJDESIGN.alParaval.Add(Userid)
                                OBJDESIGN.alParaval.Add(YearId)
                                OBJDESIGN.alParaval.Add(0)

                                OBJDESIGN.alParaval.Add(DBNull.Value)
                                OBJDESIGN.alParaval.Add("")   'LINE1
                                OBJDESIGN.alParaval.Add("")   'LINE2
                                OBJDESIGN.alParaval.Add("")   'PARENTDESIGNNO
                                OBJDESIGN.alParaval.Add("")    'DESIGNER

                                OBJDESIGN.alParaval.Add("") 'GRIDSRNO
                                OBJDESIGN.alParaval.Add("") 'BASE
                                OBJDESIGN.alParaval.Add("") 'PRINT
                                OBJDESIGN.alParaval.Add("") 'COLOR
                                OBJDESIGN.alParaval.Add(0) 'COLORBLOCKED
                                OBJDESIGN.alParaval.Add("") 'SHADETYPE

                                Dim INTRESCAT As Integer = OBJDESIGN.SAVE()
                            End If
                        End If



                        'COLOR SAVE
                        If dr("COLOR") <> "" Then
                            dttable = OBJCMN.search("COLOR_ID AS COLORID", "", "COLORMASTER", " AND COLOR_NAME = '" & dr("COLOR") & "' AND COLOR_YEARID = " & YearId)
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
                            dttable = OBJCMN.search("QUALITY_ID AS QUALITYID", "", "QUALITYMASTER", " AND QUALITY_NAME = '" & dr("QUALITY") & "' AND QUALITY_YEARID = " & YearId)
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
                                OBJQUALITY.alParaval.Add(DBNull.Value)



                                Dim INTRESCAT As Integer = OBJQUALITY.save()
                            End If
                        End If

                        GRIDSTOCKIN.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE"), dr("ITEMNAME").ToString, dr("QUALITY"), "", "", "", dr("DESIGNNO"), dr("COLOR"), 0, Format(Val(dr("PCS")), "0"), dr("UNIT"), Format(Val(dr("MTRS")), "0.00"), 0, "Mtrs", 0, "", "", "", 0, 0, 0)
                    Next
                    TOTAL()
                    GRIDSTOCKIN.FirstDisplayedScrollingRowIndex = GRIDSTOCKIN.RowCount - 1
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtchallan_Validating(sender As Object, e As CancelEventArgs) Handles txtchallan.Validating
        Try
            'FOR AMAN WE NEED TO CHECK WHETHER THIS PARTYCHALLAN IS CORRECT OR NOT, IF CORRECT THEN CHECK WHETHER WE HAVE STOCK FOR THIS CHALLAN OR NOT
            If (ClientName = "AMAN" Or ClientName = "AARYA") And txtchallan.Text.Trim <> "" Then
                Dim BALPCS As Double = 0
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable = OBJCMN.SEARCH(" CHALLANNO, SUM(PCS) AS INPCS, SUM(MTRS) AS INMTRS, SUM(PCS)-SUM(ISSPCS) AS BALPCS, SUM(MTRS)-SUM(ISSMTRS) AS BALMTRS ", "", " STOCKREGISTER ", " AND NAME = '" & CMBNAME.Text.Trim & "' AND CHALLANNO = '" & txtchallan.Text.Trim & "' AND YEARID = " & YearId & " GROUP BY NAME, CHALLANNO ")
                If dt.Rows.Count > 0 Then

                    'CHECK THE STOCK
                    BALPCS = Val(dt.Rows(0).Item("BALPCS"))

                    If Val(BALPCS) <= 0 Then
                        MsgBox("Challan No not in Stock", MsgBoxStyle.Critical, "TEXTRADE")
                        e.Cancel = True
                    End If

                    'IF PCS ARE IN STOCK AND WE HAVE LOCKED CHALLAN THEN ALSO VALIDATE
                    'WE HAVE GIVE AN OPTION IN GRN TO LOCK THIS CHALLAN MANUALLY IF WE WANT TO CLOSE THIS
                    'IF WE CLOSE THIS CHALLAN NO IN GRN THEN CHECK IT AND DONT ALLOW TO ENTER THAT CHALLANNO
                    'WE HAVE USED LOTREADY CHECK BOX IN GRN FOR THIS PURPOSE
                    Dim DTGRN As DataTable = OBJCMN.SEARCH("ISNULL(GRN_LOTREADY,0) AS CHALLANLOCK", "", " GRN INNER JOIN LEDGERS ON GRN_LEDGERID = LEDGERS.ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND GRN_CHALLANNO = '" & txtchallan.Text.Trim & "' AND GRN.GRN_YEARID = " & YearId)
                    If DTGRN.Rows.Count > 0 Then
                        If Convert.ToBoolean(DTGRN.Rows(0).Item("CHALLANLOCK")) = True Then
                            MsgBox("Invalid Challan No., Challan has been Locked ", MsgBoxStyle.Critical, "TEXTRADE")
                            e.Cancel = True
                        End If
                    End If
                Else
                    MsgBox("Invalid Challan No. ", MsgBoxStyle.Critical, "TEXTRADE")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE='ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, TXTTRANSADD, " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE='ACCOUNTS'", "", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Enter(sender As Object, e As EventArgs) Handles cmbqtyunit.Enter
        Try
            If cmbqtyunit.Text.Trim = "" Then fillunit(cmbqtyunit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTRECONO_Validating(sender As Object, e As CancelEventArgs) Handles TXTRECONO.Validating
        Try
            If Val(TXTRECONO.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(STOCKADJUSTMENT.SA_NO,0)  AS RECONO", "", " STOCKADJUSTMENT ", "  AND STOCKADJUSTMENT.SA_NO=" & Val(TXTRECONO.Text.Trim) & " AND STOCKADJUSTMENT.SA_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Rec No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRECONO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTRECONO.KeyPress, tstxtbillno.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTMTRS_Validated(sender As Object, e As EventArgs) Handles TXTMTRS.Validated
        Try
            ' CALC()
            If ClientName = "AVIS" Or ClientName = "SOFTAS" Then Call CMBSHELF_Validated(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSTOCKIN_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDSTOCKIN.CellValidating
        Try
            Dim colNum As Integer = GRIDSTOCKIN.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GINRATE.Index, GMTRS.Index
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

    Private Sub CHKMANUALRATE_CheckedChanged(sender As Object, e As EventArgs) Handles CHKMANUALRATE.CheckedChanged
        Try
            If UserName <> "Admin" Then Exit Sub
            TXTRATE.ReadOnly = Not CHKMANUALRATE.Checked
            GINRATE.ReadOnly = Not CHKMANUALRATE.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_GotFocus(sender As Object, e As EventArgs) Handles TXTCUT.GotFocus
        TXTCUT.SelectAll()
    End Sub

    Private Sub CMBCONTRACTOR_Validating(sender As Object, e As CancelEventArgs) Handles CMBCONTRACTOR.Validating
        Try
            If CMBCONTRACTOR.Text.Trim <> "" Then CONTRACTVALIDATE(CMBCONTRACTOR, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCONTRACTOR_Enter(sender As Object, e As EventArgs) Handles CMBCONTRACTOR.Enter
        Try
            If CMBCONTRACTOR.Text.Trim = "" Then FILLCONTRACT(CMBCONTRACTOR)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class