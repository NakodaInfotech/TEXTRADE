Imports System.ComponentModel
Imports BL

Public Class YarnStockReco

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


    Private Sub YarnStockReco_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        fillYARNQUALITY(CMBYARNQUALITY, EDIT)
        FILLMILL(CMBMILL, EDIT)
        FILLDESIGN(CMBDESIGN, "")
        FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, "")
    End Sub


    Private Sub YarnStockReco_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'YARNSTOCKRECO'")
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


                Dim objSTOCK As New ClsYarnStockAdjustment()
                Dim dttable As DataTable = objSTOCK.SELECTYARNSTOCKADJUSTMENT(TEMPRECONO, CmpId, Locationid, YearId)
                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows
                        TXTRECONO.Text = TEMPRECONO
                        TXTRECONO.ReadOnly = True
                        DTRECODATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                        CMBNAME.Text = dr("NAME")
                        CMBTRANS.Text = dr("TRANSNAME")
                        LBLTOTALOUTBAGS.Text = Convert.ToString(dr("TOTALOUTBAGS").ToString)
                        LBLTOTALOUTWT.Text = Convert.ToString(dr("TOTALOUTWT").ToString)
                        LBLTOTALOUTCONES.Text = Convert.ToString(dr("TOTALOUTCONES").ToString)
                        LBLTOTALINBAGS.Text = Convert.ToString(dr("TOTALINBAGS").ToString)
                        LBLTOTALINWT.Text = Convert.ToString(dr("TOTALINWT").ToString)
                        LBLTOTALINCONES.Text = Convert.ToString(dr("TOTALINCONES").ToString)
                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)


                        'Item Grid
                        If Val(dr("GRIDSRNO")) > 0 Then GRIDSTOCKOUT.Rows.Add(dr("GRIDSRNO").ToString, dr("YARNITEMNAME").ToString, dr("MILL").ToString, dr("DESIGN").ToString, dr("PARTYLOTNO").ToString, dr("PARTYCOLOR").ToString, dr("COLOR").ToString, dr("LOTNO").ToString, dr("DESC").ToString, Val(dr("BAGS")), Format(Val(dr("WT")), "0.00"), Val(dr("CONES")), dr("LRNO"), dr("RACK").ToString, Format(Val(dr("RATE")), "0.00"), dr("PER").ToString, Format(Val(dr("AMOUNT")), "0.00"), dr("BARCODE").ToString, Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("FROMTYPE").ToString)

                    Next



                    'GET DATA FROM YARNSTOCKADJUSTMENT_INDESC
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(YARNSTOCKADJUSTMENT.YSA_NO, 0) AS SANO, YARNSTOCKADJUSTMENT.YSA_DATE AS DATE, GODOWNMASTER.GODOWN_name AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS YARNITEM,  ISNULL(MILLMASTER.MILL_NAME, '') AS MILL, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_PARTYLOTNO, '') AS PARTYLOTNO,  ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_PARTYCOLOR, '') AS PARTYCOLOR, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_LOTNO, '') AS LOTNO, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_DESC, '') AS [DESC], ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_BAGS, 0) AS BAGS, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_WT, 0) AS WT, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_CONES, '') AS CONES, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_LRNO, '') AS LRNO, ISNULL(RACKMASTER.RACK_NAME, '') AS RACK, ISNULL(YARNSTOCKADJUSTMENT_INDESC.SA_RATE, 0) AS RATE, ISNULL(YARNSTOCKADJUSTMENT_INDESC.SA_PER, '') AS PER, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_AMOUNT, 0) AS AMOUNT, ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_BARCODE, '') AS BARCODE, ISNULL(YARNSTOCKADJUSTMENT.YSA_REMARKS, '') AS REMARKS , ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_DONE, '') AS DONE ,ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_OUTBAGS, '') AS OUTBAGS ,ISNULL(YARNSTOCKADJUSTMENT_INDESC.YSA_OUTWT, '') AS OUTWT ", "", " YARNSTOCKADJUSTMENT INNER JOIN YARNSTOCKADJUSTMENT_INDESC ON YARNSTOCKADJUSTMENT.YSA_NO = YARNSTOCKADJUSTMENT_INDESC.YSA_NO AND  YARNSTOCKADJUSTMENT.YSA_yearid = YARNSTOCKADJUSTMENT_INDESC.YSA_YEARID LEFT OUTER JOIN RACKMASTER ON YARNSTOCKADJUSTMENT_INDESC.YSA_RACKID = RACKMASTER.RACK_ID LEFT OUTER JOIN COLORMASTER ON YARNSTOCKADJUSTMENT_INDESC.YSA_SHADEID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON YARNSTOCKADJUSTMENT_INDESC.YSA_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN MILLMASTER ON YARNSTOCKADJUSTMENT_INDESC.YSA_MILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN YARNQUALITYMASTER ON YARNSTOCKADJUSTMENT_INDESC.YSA_ITEMID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON YARNSTOCKADJUSTMENT.YSA_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON YARNSTOCKADJUSTMENT.YSA_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON YARNSTOCKADJUSTMENT.YSA_GODOWNID = GODOWNMASTER.GODOWN_id     ", " AND YARNSTOCKADJUSTMENT_INDESC.YSA_NO = " & TEMPRECONO & " AND YARNSTOCKADJUSTMENT_INDESC.YSA_YEARID = " & YearId & " ORDER BY YARNSTOCKADJUSTMENT_INDESC.YSA_GRIDSRNO")

                    For Each DR As DataRow In DT.Rows
                        'Item Grid
                        GRIDSTOCKIN.Rows.Add(DR("GRIDSRNO").ToString, DR("YARNITEM").ToString, DR("MILL").ToString, DR("DESIGN").ToString, DR("PARTYLOTNO").ToString, DR("PARTYCOLOR").ToString, DR("COLOR").ToString, DR("LOTNO").ToString, DR("DESC").ToString, Val(DR("BAGS")), Format(Val(DR("WT")), "0.00"), Val(DR("CONES")), DR("LRNO"), DR("RACK").ToString, Format(Val(DR("RATE")), "0.00"), DR("PER").ToString, Format(Val(DR("AMOUNT")), "0.00"), DR("BARCODE").ToString, Val(DR("DONE")), Val(DR("OUTBAGS")), Val(DR("OUTWT")))


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
        If USERGODOWN <> "" Then CMBGODOWN.Text = USERGODOWN Else CMBGODOWN.Text = ""
        CMBNAME.Text = ""
        CMBTRANS.Text = ""
        DTRECODATE.Text = Now.Date
        tstxtbillno.Clear()
        txtremarks.Clear()
        txtsrno.Text = 1
        CMBYARNQUALITY.Text = ""
        CMBMILL.Text = ""
        CMBDESIGN.Text = ""
        cmbcolor.Text = ""
        TXTLOTNO.Clear()
        TXTDESC.Clear()
        TXTBAGS.Clear()
        TXTWT.Clear()
        TXTCONES.Clear()
        TXTLRNO.Clear()
        CMBRACK.Text = ""
        TXTRATE.Clear()
        CMBPER.Text = ""
        TXTAMT.Clear()
        TXTINBARCODE.Clear()




        TXTBAGSDIFF.Clear()
        LBLTOTALOUTBAGS.Text = 0.0
        LBLTOTALOUTWT.Text = 0.0
        LBLTOTALOUTCONES.Text = 0.0

        LBLTOTALINBAGS.Text = 0.0
        LBLTOTALINWT.Text = 0.0
        LBLTOTALINCONES.Text = 0.0

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
                    Dim dttable As DataTable = OBJCMNn.SEARCH(" ISNULL(YARNSTOCKADJUSTMENT.YSA_NO,0)  AS RECONO", "", " YARNSTOCKADJUSTMENT ", "  AND YARNSTOCKADJUSTMENT.YSA_NO=" & Val(TXTRECONO.Text.Trim) & " AND YARNSTOCKADJUSTMENT.YSA_YEARID = " & YearId)
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

            'If CMBNAME.Text.Trim.Length = 0 Then
            '    EP.SetError(CMBNAME, " Please Fill Party Name")
            '    bln = False
            'End If



            If GRIDSTOCKOUT.RowCount = 0 And GRIDSTOCKIN.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If
            'CHEKC BARCODE IS PRESENT IN DATABASE OR NOT

            If Not datecheck(DTRECODATE.Text) Then
                EP.SetError(DTRECODATE, "Date not in Accounting Year")
                bln = False
            End If



            'If Convert.ToDateTime(DTRECODATE.Text).Date < STOCKADJBLOCKDATE.Date Then
            '        EP.SetError(DTRECODATE, "Date is Blocked, Please make entries after " & Format(STOCKADJBLOCKDATE.Date, "dd/MM/yyyy"))    'UNCOMMENT AFTER ADDING BLOCKDATE
            '        bln = False
            '    End If


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
        DTTABLE = getmax(" isnull(max(YSA_no),0) + 1 ", " YARNSTOCKADJUSTMENT ", " AND YSA_yearid=" & YearId)
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
            alParaval.Add(LBLTOTALINBAGS.Text.Trim)
            alParaval.Add(LBLTOTALINWT.Text.Trim)
            alParaval.Add(LBLTOTALINCONES.Text.Trim)
            alParaval.Add(LBLTOTALOUTBAGS.Text.Trim)
            alParaval.Add(LBLTOTALOUTWT.Text.Trim)
            alParaval.Add(LBLTOTALOUTCONES.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim gridsrno As String = ""
            Dim ITEMNAME As String = ""
            Dim MILLNAME As String = ""
            Dim DESIGN As String = ""
            Dim PARTYLOTNO As String = ""
            Dim PARTYCOLOR As String = ""
            Dim SHADE As String = ""
            Dim LOTNO As String = ""
            Dim DESC As String = ""
            Dim BAGS As String = ""
            Dim WT As String = ""
            Dim CONES As String = ""
            Dim LRNO As String = ""
            Dim RACK As String = ""
            Dim RATE As String = ""
            Dim PER As String = ""
            Dim AMOUNT As String = ""
            Dim BARCODE As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDSTOCKOUT.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(OSRNO.Index).Value
                        ITEMNAME = row.Cells(OYARNQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(OMILLNAME.Index).Value.ToString
                        DESIGN = row.Cells(ODESIGN.Index).Value.ToString
                        PARTYLOTNO = row.Cells(OPARTYLOTNO.Index).Value.ToString
                        PARTYCOLOR = row.Cells(OPARTYCOLOR.Index).Value.ToString
                        SHADE = row.Cells(OSHADE.Index).Value.ToString
                        LOTNO = row.Cells(OLOTNO.Index).Value.ToString
                        DESC = row.Cells(ODESC.Index).Value.ToString
                        BAGS = row.Cells(OBAGS.Index).Value
                        WT = row.Cells(OWT.Index).Value
                        CONES = row.Cells(OCONES.Index).Value
                        LRNO = row.Cells(OLRNO.Index).Value.ToString
                        RACK = row.Cells(ORACK.Index).Value.ToString
                        RATE = row.Cells(ORATE.Index).Value
                        PER = row.Cells(OPER.Index).Value.ToString
                        AMOUNT = row.Cells(OAMOUNT.Index).Value
                        BARCODE = row.Cells(OBARCODE.Index).Value.ToString
                        FROMNO = row.Cells(OFROMNO.Index).Value
                        FROMSRNO = row.Cells(OFROMSRNO.Index).Value
                        FROMTYPE = row.Cells(OFROMTYPE.Index).Value.ToString


                    Else
                        gridsrno = gridsrno & "|" & row.Cells(OSRNO.Index).Value
                        ITEMNAME = ITEMNAME & "|" & row.Cells(OYARNQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(OMILLNAME.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(ODESIGN.Index).Value.ToString
                        PARTYLOTNO = PARTYLOTNO & "|" & row.Cells(OPARTYLOTNO.Index).Value.ToString
                        PARTYCOLOR = PARTYCOLOR & "|" & row.Cells(OPARTYCOLOR.Index).Value.ToString
                        SHADE = SHADE & "|" & row.Cells(OSHADE.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(OLOTNO.Index).Value.ToString
                        DESC = DESC & "|" & row.Cells(ODESC.Index).Value.ToString
                        BAGS = BAGS & "|" & row.Cells(OBAGS.Index).Value
                        WT = WT & "|" & row.Cells(OWT.Index).Value
                        CONES = CONES & "|" & row.Cells(OCONES.Index).Value
                        LRNO = LRNO & "|" & row.Cells(OLRNO.Index).Value.ToString
                        RACK = RACK & "|" & row.Cells(ORACK.Index).Value.ToString
                        RATE = RATE & "|" & row.Cells(ORATE.Index).Value
                        PER = PER & "|" & row.Cells(OPER.Index).Value.ToString
                        AMOUNT = AMOUNT & "|" & row.Cells(OAMOUNT.Index).Value
                        BARCODE = BARCODE & "|" & row.Cells(OBARCODE.Index).Value.ToString
                        FROMNO = FROMNO & "|" & row.Cells(OFROMNO.Index).Value
                        FROMSRNO = FROMSRNO & "|" & row.Cells(OFROMSRNO.Index).Value
                        FROMTYPE = FROMTYPE & "|" & row.Cells(OFROMTYPE.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(ITEMNAME)
            alParaval.Add(MILLNAME)
            alParaval.Add(DESIGN)
            alParaval.Add(PARTYLOTNO)
            alParaval.Add(PARTYCOLOR)
            alParaval.Add(SHADE)
            alParaval.Add(LOTNO)
            alParaval.Add(DESC)
            alParaval.Add(BAGS)
            alParaval.Add(WT)
            alParaval.Add(CONES)
            alParaval.Add(LRNO)
            alParaval.Add(RACK)
            alParaval.Add(RATE)
            alParaval.Add(PER)
            alParaval.Add(AMOUNT)
            alParaval.Add(BARCODE)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)




            Dim INgridsrno As String = ""
            Dim INITEMNAME As String = ""
            Dim INMILLNAME As String = ""
            Dim INDESIGN As String = ""
            Dim INPARTYLOTNO As String = ""
            Dim INPARTYCOLOR As String = ""
            Dim INSHADE As String = ""
            Dim INLOTNO As String = ""
            Dim INDESC As String = ""
            Dim INBAGS As String = ""
            Dim INWT As String = ""
            Dim INCONES As String = ""
            Dim INLRNO As String = ""
            Dim INRACK As String = ""
            Dim INRATE As String = ""
            Dim INPER As String = ""
            Dim INAMOUNT As String = ""
            Dim INBARCODE As String = ""
            Dim DONE As String = ""
            Dim OUTBAGS As String = ""
            Dim OUTWT As String = ""



            For Each row As Windows.Forms.DataGridViewRow In GRIDSTOCKIN.Rows
                If row.Cells(0).Value <> Nothing Then
                    If INgridsrno = "" Then
                        INgridsrno = row.Cells(gsrno.Index).Value
                        INITEMNAME = row.Cells(GYARNQUALITY.Index).Value.ToString
                        INMILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        INDESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        INPARTYLOTNO = row.Cells(GPARTYLOTNO.Index).Value.ToString
                        INPARTYCOLOR = row.Cells(GPARTYCOLOR.Index).Value.ToString
                        INSHADE = row.Cells(GCOLOR.Index).Value.ToString
                        INLOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        INDESC = row.Cells(GDESC.Index).Value.ToString
                        INBAGS = row.Cells(GBAGS.Index).Value
                        INWT = row.Cells(GWT.Index).Value
                        INCONES = row.Cells(GCONES.Index).Value
                        INLRNO = row.Cells(GLRNO.Index).Value.ToString
                        INRACK = row.Cells(GRACK.Index).Value.ToString
                        INRATE = row.Cells(GRATE.Index).Value
                        INPER = row.Cells(GPER.Index).Value.ToString
                        INAMOUNT = row.Cells(GAMOUNT.Index).Value
                        INBARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        If row.Cells(GDONE.Index).Value = True Then DONE = 1 Else DONE = 0
                        OUTBAGS = row.Cells(GOUTBAGS.Index).Value
                        OUTWT = row.Cells(GOUTWT.Index).Value



                    Else
                        INgridsrno = INgridsrno & "|" & row.Cells(gsrno.Index).Value
                        INITEMNAME = INITEMNAME & "|" & row.Cells(GYARNQUALITY.Index).Value.ToString
                        INMILLNAME = INMILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        INDESIGN = INDESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        INPARTYLOTNO = INPARTYLOTNO & "|" & row.Cells(GPARTYLOTNO.Index).Value.ToString
                        INPARTYCOLOR = INPARTYCOLOR & "|" & row.Cells(GPARTYCOLOR.Index).Value.ToString
                        INSHADE = INSHADE & "|" & row.Cells(GCOLOR.Index).Value.ToString
                        INLOTNO = INLOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        INDESC = INDESC & "|" & row.Cells(GDESC.Index).Value.ToString
                        INBAGS = INBAGS & "|" & row.Cells(GBAGS.Index).Value
                        INWT = INWT & "|" & row.Cells(GWT.Index).Value
                        INCONES = INCONES & "|" & row.Cells(GCONES.Index).Value
                        INLRNO = INLRNO & "|" & row.Cells(GLRNO.Index).Value.ToString
                        INRACK = INRACK & "|" & row.Cells(GRACK.Index).Value.ToString
                        INRATE = INRATE & "|" & row.Cells(GRATE.Index).Value
                        INPER = INPER & "|" & row.Cells(GPER.Index).Value.ToString
                        INAMOUNT = INAMOUNT & "|" & row.Cells(GAMOUNT.Index).Value
                        INBARCODE = INBARCODE & "|" & row.Cells(GBARCODE.Index).Value
                        If row.Cells(GDONE.Index).Value = True Then DONE = DONE & "|" & "1" Else DONE = DONE & "|" & "0"
                        OUTBAGS = OUTBAGS & "|" & row.Cells(GOUTBAGS.Index).Value
                        OUTWT = OUTWT & "|" & row.Cells(GOUTWT.Index).Value



                    End If
                End If
            Next

            alParaval.Add(INgridsrno)
            alParaval.Add(INITEMNAME)
            alParaval.Add(INMILLNAME)
            alParaval.Add(INDESIGN)
            alParaval.Add(INPARTYLOTNO)
            alParaval.Add(INPARTYCOLOR)
            alParaval.Add(INSHADE)
            alParaval.Add(INLOTNO)
            alParaval.Add(INDESC)
            alParaval.Add(INBAGS)
            alParaval.Add(INWT)
            alParaval.Add(INCONES)
            alParaval.Add(INLRNO)
            alParaval.Add(INRACK)
            alParaval.Add(INRATE)
            alParaval.Add(INPER)
            alParaval.Add(INAMOUNT)
            alParaval.Add(INBARCODE)
            alParaval.Add(DONE)
            alParaval.Add(OUTBAGS)
            alParaval.Add(OUTWT)



            Dim objSTOCK As New ClsYarnStockAdjustment()
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
            LBLTOTALOUTBAGS.Text = 0.0
            LBLTOTALOUTWT.Text = 0.0
            LBLTOTALOUTCONES.Text = 0
            LBLTOTALINBAGS.Text = 0.0
            LBLTOTALINWT.Text = 0.0
            LBLTOTALINCONES.Text = 0

            TXTBAGSDIFF.Text = 0
            TXTWTDIFF.Text = 0.00

            For Each ROW As DataGridViewRow In GRIDSTOCKOUT.Rows
                If ROW.Cells(OSRNO.Index).Value <> Nothing Then
                    LBLTOTALOUTBAGS.Text = Format(Val(LBLTOTALOUTBAGS.Text) + Val(ROW.Cells(OBAGS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALOUTWT.Text = Format(Val(LBLTOTALOUTWT.Text) + Val(ROW.Cells(OWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALOUTCONES.Text = Format(Val(LBLTOTALOUTCONES.Text) + Val(ROW.Cells(OCONES.Index).EditedFormattedValue), "0")

                End If
            Next

            For Each ROW As DataGridViewRow In GRIDSTOCKIN.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALINBAGS.Text = Format(Val(LBLTOTALINBAGS.Text) + Val(ROW.Cells(GBAGS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALINWT.Text = Format(Val(LBLTOTALINWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALINCONES.Text = Format(Val(LBLTOTALINCONES.Text) + Val(ROW.Cells(GCONES.Index).EditedFormattedValue), "0")
                End If
            Next
            TXTBAGSDIFF.Text = Format(Val(LBLTOTALOUTBAGS.Text) - Val(LBLTOTALINBAGS.Text), "0")
            TXTWTDIFF.Text = Format(Val(LBLTOTALOUTWT.Text) - Val(LBLTOTALOUTWT.Text), "0.00")

            If CMBPER.Text = "Bags" Then
                TXTAMT.Text = Val(TXTBAGS.Text) * Val(TXTRATE.Text)
            Else
                TXTAMT.Text = Val(TXTWT.Text) * Val(TXTRATE.Text)
            End If


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
                If CMBTRANS.Text.Trim = "" Then FILLNAME(CMBTRANS, EDIT, " And GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
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
            Dim OBJSELECTGDN As New SelectYarnStock
            OBJSELECTGDN.GODOWN = CMBGODOWN.Text.Trim
                OBJSELECTGDN.ShowDialog()
                DTTABLE = OBJSELECTGDN.DT

                If DTTABLE.Rows.Count > 0 Then
                    For Each dr As DataRow In DTTABLE.Rows
                    GRIDSTOCKOUT.Rows.Add(0, dr("YARNQUALITY").ToString, dr("MILLNAME").ToString, dr("DESIGNNO").ToString, "", "", dr("COLOR").ToString, dr("LOTNO").ToString, "", Val(dr("BAGS")), Format(Val(dr("WT")), "0.00"), Val(dr("CONES")), dr("LRNO"), dr("RACK"), 0, "", 0, dr("BARCODE").ToString, Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("FROMTYPE").ToString)
                    If CHKCOPY.Checked = True Then GRIDSTOCKIN.Rows.Add(0, dr("YARNQUALITY").ToString, dr("MILLNAME").ToString, dr("DESIGNNO").ToString, "", "", dr("COLOR").ToString, dr("LOTNO").ToString, "", Val(dr("BAGS")), Format(Val(dr("WT")), "0.00"), Val(dr("CONES")), dr("LRNO"), dr("RACK"), 0, "", 0, dr("BARCODE").ToString, 0, 0, 0)
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
                    YarnStockReco_Load(sender, e)
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
                GRIDSTOCKIN.Rows.Add(Val(txtsrno.Text.Trim), CMBYARNQUALITY.Text.Trim, CMBMILL.Text.Trim, CMBDESIGN.Text.Trim, TXTPARTYLOTNO.Text.Trim, TXTPARTYCOLOR.Text.Trim, cmbcolor.Text.Trim, TXTLOTNO.Text.Trim, TXTDESC.Text.Trim, Format(Val(TXTBAGS.Text.Trim), "0"), Format(Val(TXTWT.Text.Trim), "0.00"), Val(TXTCONES.Text.Trim), TXTLRNO.Text.Trim, CMBRACK.Text.Trim, Val(TXTRATE.Text.Trim), CMBPER.Text.Trim, Format(Val(TXTAMT.Text.Trim), "0.00"), TXTINBARCODE.Text.Trim, 0, 0, 0)
                GETSRNO(GRIDSTOCKIN)

            ElseIf GRIDDOUBLECLICK = True Then

                GRIDSTOCKIN.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
                GRIDSTOCKIN.Item(GYARNQUALITY.Index, TEMPROW).Value = CMBYARNQUALITY.Text.Trim
                GRIDSTOCKIN.Item(GMILLNAME.Index, TEMPROW).Value = CMBMILL.Text.Trim
                GRIDSTOCKIN.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
                GRIDSTOCKIN.Item(GPARTYLOTNO.Index, TEMPROW).Value = TXTPARTYLOTNO.Text.Trim
                GRIDSTOCKIN.Item(GPARTYCOLOR.Index, TEMPROW).Value = TXTPARTYCOLOR.Text.Trim
                GRIDSTOCKIN.Item(GCOLOR.Index, TEMPROW).Value = cmbcolor.Text.Trim
                GRIDSTOCKIN.Item(GLOTNO.Index, TEMPROW).Value = TXTLOTNO.Text.Trim
                GRIDSTOCKIN.Item(GDESC.Index, TEMPROW).Value = TXTDESC.Text.Trim
                GRIDSTOCKIN.Item(GBAGS.Index, TEMPROW).Value = Val(TXTBAGS.Text.Trim)
                GRIDSTOCKIN.Item(GWT.Index, TEMPROW).Value = Format(TXTWT.Text.Trim, "0.00")
                GRIDSTOCKIN.Item(GCONES.Index, TEMPROW).Value = TXTCONES.Text.Trim
                GRIDSTOCKIN.Item(GRACK.Index, TEMPROW).Value = CMBRACK.Text.Trim
                GRIDSTOCKIN.Item(GRATE.Index, TEMPROW).Value = TXTRATE.Text.Trim
                GRIDSTOCKIN.Item(GPER.Index, TEMPROW).Value = CMBPER.Text.Trim
                GRIDSTOCKIN.Item(GAMOUNT.Index, TEMPROW).Value = TXTAMT.Text.Trim

                GRIDDOUBLECLICK = False
            End If

            TOTAL()

            GRIDSTOCKIN.FirstDisplayedScrollingRowIndex = GRIDSTOCKIN.RowCount - 1

            txtsrno.Text = GRIDSTOCKIN.RowCount + 1
            CMBYARNQUALITY.Text = ""
            CMBMILL.Text = ""
            CMBDESIGN.Text = ""
            TXTPARTYCOLOR.Clear()
            TXTPARTYLOTNO.Clear()
            cmbcolor.Text = ""
            TXTLOTNO.Clear()
            TXTDESC.Clear()
            TXTWT.Clear()
            TXTBAGS.Clear()
            TXTCONES.Clear()
            TXTLRNO.Clear()
            CMBRACK.Text = ""
            TXTRATE.Clear()
            CMBPER.Focus()
            TXTAMT.Clear()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDJOBIN_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDSTOCKIN.CellDoubleClick
        EDITROW()
    End Sub

    Sub EDITROW()
        Try
            If GRIDSTOCKIN.CurrentRow.Index >= 0 And GRIDSTOCKIN.Item(gsrno.Index, GRIDSTOCKIN.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                txtsrno.Text = GRIDSTOCKIN.Item(gsrno.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBYARNQUALITY.Text = GRIDSTOCKIN.Item(GYARNQUALITY.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBMILL.Text = GRIDSTOCKIN.Item(GMILLNAME.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDSTOCKIN.Item(GDESIGN.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTPARTYLOTNO.Text = GRIDSTOCKIN.Item(GPARTYLOTNO.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTPARTYCOLOR.Text = GRIDSTOCKIN.Item(GPARTYCOLOR.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                cmbcolor.Text = GRIDSTOCKIN.Item(GCOLOR.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTLOTNO.Text = GRIDSTOCKIN.Item(GLOTNO.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTDESC.Text = GRIDSTOCKIN.Item(GDESC.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTBAGS.Text = GRIDSTOCKIN.Item(GBAGS.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTWT.Text = GRIDSTOCKIN.Item(GWT.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTCONES.Text = GRIDSTOCKIN.Item(GCONES.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTLRNO.Text = GRIDSTOCKIN.Item(GLRNO.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBRACK.Text = GRIDSTOCKIN.Item(GRACK.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTRATE.Text = GRIDSTOCKIN.Item(GRATE.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                CMBPER.Text = GRIDSTOCKIN.Item(GPER.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString
                TXTAMT.Text = GRIDSTOCKIN.Item(GAMOUNT.Index, GRIDSTOCKIN.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDSTOCKIN.CurrentRow.Index
                CMBYARNQUALITY.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCONES.KeyPress, TXTBAGS.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub


    Private Sub GRIDSTOCKOUT_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDSTOCKOUT.CellValidating
        Try
            Dim colNum As Integer = GRIDSTOCKOUT.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GBAGS.Index, OWT.Index
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
                Dim OBSTOCK As New ClsYarnStockAdjustment

                ALPARAVAL.Add(TEMPRECONO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                OBSTOCK.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBSTOCK.DELETE()
                MsgBox("Yarn Stock Adjustment Deleted Succesfully")
                CLEAR()
                EDIT = False
                DTRECODATE.Focus()
            End If
        Catch ex As Exception
            Throw ex
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
        Try
            If CMBPER.Text = "Bags" Then TXTAMT.Text = Format(Val(TXTRATE.Text.Trim) * Val(TXTBAGS.Text.Trim), "0.00") Else TXTAMT.Text = Format(Val(TXTRATE.Text.Trim) * Val(TXTWT.Text.Trim), "0.00")
        Catch ex As Exception
            Throw ex
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






    '    Sub PRINTREPORT()
    '            'Try
    '            '    If MsgBox("Wish to Print Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
    '            '    Dim OBJSA As New SaleOrderDesign
    '            '    OBJSA.MdiParent = MDIMain
    '            '    OBJSA.FORMULA = "{STORESTOCKADJUSTMENT.SA_NO} = " & Val(TXTRECONO.Text.Trim) & " AND {STORESTOCKADJUSTMENT.SA_YEARID} = " & YearId
    '            '    OBJSA.FRMSTRING = "STOCKRECO"
    '            '    OBJSA.Show()
    '            'Catch ex As Exception
    '            '    Throw ex
    '            'End Try
    '        End Sub



    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstxtbillno.KeyPress
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
                YarnStockReco_Load(sender, e)
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
                YarnStockReco_Load(sender, e)
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
                Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(YARNSTOCKADJUSTMENT.YSA_NO,0)  AS RECONO", "", " YARNSTOCKADJUSTMENT ", "  AND YARNSTOCKADJUSTMENT.YSA_NO=" & Val(TXTRECONO.Text.Trim) & " AND YARNSTOCKADJUSTMENT.YSA_YEARID = " & YearId)
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

                Case GBAGS.Index, GWT.Index, GCONES.Index
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

            Dim OBJstock As New YarnStockRecoDetails
            OBJstock.MdiParent = MDIMain
            OBJstock.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub



    Private Sub TXTAMT_Validated(sender As Object, e As EventArgs) Handles TXTAMT.Validated
        Try
            If CMBYARNQUALITY.Text <> "" And Val(TXTBAGS.Text) <> 0 And Val(TXTWT.Text) <> 0 Then


                If GRIDDOUBLECLICK = False Then
                    If EDIT = True Then
                        'GET LAST BARCODE SRNO
                        Dim LSRNO As Integer = 0
                        Dim RSRNO As Integer = 0
                        Dim SNO As Integer = 0
                        If GRIDSTOCKIN.RowCount > 0 Then
                            LSRNO = InStr(GRIDSTOCKIN.Rows(GRIDSTOCKIN.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                            RSRNO = InStr(LSRNO + 1, GRIDSTOCKIN.Rows(GRIDSTOCKIN.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                            SNO = GRIDSTOCKIN.Rows(GRIDSTOCKIN.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)
                        End If

                        TXTINBARCODE.Text = "YA-" & Val(TXTRECONO.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                    Else
                        TXTINBARCODE.Text = "YA-" & Val(TXTRECONO.Text.Trim) & "/" & GRIDSTOCKIN.RowCount + 1 & "/" & YearId
                    End If
                End If
                FILLGRID()

                CMBYARNQUALITY.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(sender As Object, e As EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then FILLMILL(CMBMILL, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub CMBUNIT_Validating(sender As Object, e As CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then MILLVALIDATE(CMBMILL, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMBYARNQUALITY_Enter(sender As Object, e As EventArgs) Handles CMBYARNQUALITY.Enter
        Try
            If CMBYARNQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBYARNQUALITY, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBYARNQUALITY.Validating
        Try
            If CMBYARNQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBYARNQUALITY, e, Me)
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
                    YarnStockReco_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


    Private Sub CMBDESIGN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Enter(sender As Object, e As EventArgs) Handles cmbcolor.Enter
        Try
            FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, "")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcolor.Validating
        Try
            If cmbcolor.Text.Trim <> "" Then COLORVALIDATE(cmbcolor, e, Me, CMBDESIGN.Text.Trim, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBRACK_Validating(sender As Object, e As CancelEventArgs) Handles CMBRACK.Validating
        Try
            If CMBRACK.Text.Trim <> "" Then RACKVALIDATE(CMBRACK, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBRACK_Enter(sender As Object, e As EventArgs) Handles CMBRACK.Enter
        Try
            If CMBRACK.Text.Trim = "" Then FILLRACK(CMBRACK)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPER_Validating(sender As Object, e As CancelEventArgs) Handles CMBPER.Validating
        TOTAL()
    End Sub
End Class





