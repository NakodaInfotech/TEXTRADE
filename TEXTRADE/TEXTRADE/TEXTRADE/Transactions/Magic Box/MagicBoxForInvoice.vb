Imports System.ComponentModel
Imports BL
Public Class MagicBoxForInvoice
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Try
            For Each row As Windows.Forms.DataGridViewRow In GRIDMAGICBOX.Rows
                Dim alParaval As New ArrayList
                Dim GRIDSRNO As String = ""
                Dim PARTYBILLDATE As String = ""
                Dim ENTRYDATE As String = ""
                Dim SELLERS As String = ""
                Dim BUYERS As String = ""
                Dim PARTYBILLNO As String = ""
                Dim CRDAYS As String = ""
                Dim PONO As String = ""
                Dim POSRNO As String = ""
                Dim POTYPE As String = ""
                Dim MERCHANT As String = ""
                Dim qty As String = ""
                Dim FOLD As String = ""
                Dim DESC As String = ""
                Dim TRANSPORT As String = ""
                Dim LRNO As String = ""
                Dim LRDATE As String = ""
                Dim BALENO As String = ""
                Dim PCS As String = ""
                Dim MTRS As String = ""
                Dim RATE As String = ""
                Dim PER As String = ""
                Dim AMOUNT As String = ""
                Dim CHARGES As String = ""
                Dim SUBTOTAL As String = ""
                Dim CGSTPER As String = ""
                Dim CGSTAMT As String = ""
                Dim SGSTPER As String = ""
                Dim SGSTAMT As String = ""
                Dim IGSTPER As String = ""
                Dim IGSTAMT As String = ""
                Dim ROUNDOFF As String = ""
                Dim GRANDTOTAL As String = ""
                Dim COMMPER As String = ""
                Dim COMM As String = ""
                Dim REMARKS As String = ""

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

                Dim OBJSO As New ClsAgencySaleOrder()
                OBJSO.alParaval = alParaval
                Dim DT As DataTable = OBJSO.SAVE()



                'WE NEED TO CREATE THE SAME ORDER IN ABHEE FABRICS LLP COMPANY
                'IF BUYER IS ABHEE FABRICS LLP THEN WE NEED TO CREATE PURCHASE ORDER IN THE NAME OF SELLER IN ABHEE FABRICS LLP COMPANY
                Dim OBJCMN As New ClsCommon
                Dim TEMPYEARID, TEMPCMPID, TEMPLEDGERID, TEMPITEMID As Integer
                Dim DTNAME As DataTable = OBJCMN.SEARCH("ISNULL(ACC_NAME,'') AS NAME", "", " LEDGERS", " AND LEDGERS.ACC_CMPNAME = '" & row.Cells(GBUYERS.Index).Value & "' AND LEDGERS.ACC_YEARID = " & YearId)
                If DTNAME.Rows.Count > 0 AndAlso DTNAME.Rows(0).Item("NAME") = "ABHEE FABRICS LLP" Then

                    'CREATE PO IN ABHEE FABRICS LLP
                    'FIRST GET THE CMPID AND YEARID OF ABHEE FABRICS LLP
                    Dim TEMPDT As DataTable = OBJCMN.SEARCH(" TOP 1 YEAR_CMPID AS CMPID, YEAR_ID AS YEARID", "", " YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMPMASTER.CMP_DISPLAYEDNAME = 'ABHEE FABRICS LLP' ORDER BY YEAR_STARTDATE DESC")
                    If TEMPDT.Rows.Count > 0 Then
                        TEMPCMPID = TEMPDT.Rows(0).Item("CMPID")
                        TEMPYEARID = TEMPDT.Rows(0).Item("YEARID")
                    Else
                        GoTo NEXTLINE
                    End If

                    'CHECK WHETHER SELLER NAME IS PRESENT OR NOT, IF NOT PRESENT THEN ADD NEW 
                    TEMPDT = OBJCMN.SEARCH("ACC_ID AS LEDGERID ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & row.Cells(GSELLERS.Index).Value & "' AND ACC_YEARID = " & TEMPYEARID)
                    If TEMPDT.Rows.Count > 0 Then TEMPLEDGERID = TEMPDT.Rows(0).Item("LEDGERID") Else CREATELEDGER(row.Cells(GSELLERS.Index).Value, TEMPCMPID, TEMPYEARID)


                    'CHECKING WHETHER ITEM IS PRESENT IN CURRENT YEAR OR NOT, IF NOT PRESENT THEN ADD NEW ITEM
                    TEMPDT = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", " ITEMMASTER ", " AND ITEM_NAME = '" & row.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & TEMPYEARID)
                    If TEMPDT.Rows.Count > 0 Then TEMPITEMID = TEMPDT.Rows(0).Item("ITEMID") Else CREATEITEM(row.Cells(gitemname.Index).Value, TEMPCMPID, TEMPYEARID)

                    GENERATEPO(Val(row.Index), TEMPCMPID, TEMPYEARID)
                End If
                '******************** END OF PO GENERATION CODE ***************************



                '**************** GENERATE SALE ORDER ******************************
                DTNAME = OBJCMN.SEARCH("ISNULL(ACC_NAME,'') AS NAME", "", " LEDGERS", " AND LEDGERS.ACC_CMPNAME = '" & row.Cells(GSELLERS.Index).Value & "' AND LEDGERS.ACC_YEARID = " & YearId)
                If DTNAME.Rows.Count > 0 AndAlso DTNAME.Rows(0).Item("NAME") = "ABHEE FABRICS LLP" Then

                    'CREATE PO IN ABHEE FABRICS LLP
                    'FIRST GET THE CMPID AND YEARID OF ABHEE FABRICS LLP
                    Dim TEMPDT As DataTable = OBJCMN.SEARCH(" TOP 1 YEAR_CMPID AS CMPID, YEAR_ID AS YEARID", "", " YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMPMASTER.CMP_DISPLAYEDNAME = 'ABHEE FABRICS LLP' ORDER BY YEAR_STARTDATE DESC")
                    If TEMPDT.Rows.Count > 0 Then
                        TEMPCMPID = TEMPDT.Rows(0).Item("CMPID")
                        TEMPYEARID = TEMPDT.Rows(0).Item("YEARID")
                    Else
                        GoTo NEXTLINE
                    End If

                    'CHECK WHETHER SELLER NAME IS PRESENT OR NOT, IF NOT PRESENT THEN ADD NEW 
                    TEMPDT = OBJCMN.SEARCH("ACC_ID AS LEDGERID ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & row.Cells(GBUYERS.Index).Value & "' AND ACC_YEARID = " & TEMPYEARID)
                    If TEMPDT.Rows.Count > 0 Then TEMPLEDGERID = TEMPDT.Rows(0).Item("LEDGERID") Else CREATELEDGER(row.Cells(GBUYERS.Index).Value, TEMPCMPID, TEMPYEARID)


                    'CHECKING WHETHER ITEM IS PRESENT IN CURRENT YEAR OR NOT, IF NOT PRESENT THEN ADD NEW ITEM
                    TEMPDT = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", " ITEMMASTER ", " AND ITEM_NAME = '" & row.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & TEMPYEARID)
                    If TEMPDT.Rows.Count > 0 Then TEMPITEMID = TEMPDT.Rows(0).Item("ITEMID") Else CREATEITEM(row.Cells(gitemname.Index).Value, TEMPCMPID, TEMPYEARID)

                    GENERATESO(Val(row.Index), TEMPCMPID, TEMPYEARID)
                End If
                '******************** END OF SO GENERATION CODE ***************************








NEXTLINE:
            Next

            MessageBox.Show("Details Added")


            CLEAR()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CLEAR()
        txtsrno.Text = 1
        ORDERDATE.Value = Now.Date
        CMBBUYERS.Text = ""
        CMBSELLERS.Text = ""
        TXTCRDAYS.Clear()
        TXTDISCOUNT.Clear()
        TXTDELPERIOD.Clear()
        DueDate.Value = Now.Date
        TXTORDERNO.Clear()
        cmbitemname.Text = ""
        CMBDESIGN.Text = ""
        TXTQTY.Clear()
        cmbqtyunit.Text = "Pcs"
        TXTCUT.Clear()
        TXTMTRS.Clear()
        TXTRATE.Clear()
        TXTREMARKS.Clear()
        GRIDMAGICBOX.RowCount = 0
        getmax_SO_no()
    End Sub

    Private Sub MagicBoxForInvoice_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE ORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            CLEAR()
            txtsrno.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub getmax_SO_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(ASO_no),0) + 1 ", "AGENCYSALEORDER", " AND ASO_cmpid=" & CmpId & " and ASO_locationid=" & Locationid & " and ASO_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            txtsrno.Text = DTTABLE.Rows(0).Item(0)
        End If
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
    Sub FILLCMB()
        Try
            fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            If CMBSELLERS.Text.Trim = "" Then FILLNAME(CMBSELLERS, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            If CMBBUYERS.Text.Trim = "" Then FILLNAME(CMBBUYERS, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillgrid()
        GRIDMAGICBOX.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDMAGICBOX.Rows.Add(Val(txtsrno.Text.Trim), Val(TXTNO.Text.Trim), ORDERDATE.Text.Trim, CMBBUYERS.Text.Trim, CMBSELLERS.Text.Trim, TXTCRDAYS.Text.Trim, TXTDISCOUNT.Text.Trim, TXTDELPERIOD.Text.Trim, DueDate.Text.Trim, TXTORDERNO.Text.Trim, cmbitemname.Text.Trim, CMBDESIGN.Text.Trim, Format(Val(TXTQTY.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), TXTREMARKS.Text.Trim)
            getsrno(GRIDMAGICBOX)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDMAGICBOX.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDMAGICBOX.Item(GNO.Index, TEMPROW).Value = Val(TXTNO.Text.Trim)
            GRIDMAGICBOX.Item(GDATE.Index, TEMPROW).Value = ORDERDATE.Text.Trim
            GRIDMAGICBOX.Item(GBUYERS.Index, TEMPROW).Value = CMBBUYERS.Text.Trim
            GRIDMAGICBOX.Item(GSELLERS.Index, TEMPROW).Value = CMBSELLERS.Text.Trim
            GRIDMAGICBOX.Item(GCRDAYS.Index, TEMPROW).Value = TXTCRDAYS.Text.Trim
            GRIDMAGICBOX.Item(GDISCOUNT.Index, TEMPROW).Value = TXTDISCOUNT.Text.Trim
            GRIDMAGICBOX.Item(GDELPERIOD.Index, TEMPROW).Value = TXTDELPERIOD.Text.Trim
            GRIDMAGICBOX.Item(GDUEDATE.Index, TEMPROW).Value = DueDate.Text.Trim
            GRIDMAGICBOX.Item(GORDERNO.Index, TEMPROW).Value = TXTORDERNO.Text.Trim
            GRIDMAGICBOX.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
            GRIDMAGICBOX.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDMAGICBOX.Item(gQty.Index, TEMPROW).Value = Format(Val(TXTQTY.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(gqtyunit.Index, TEMPROW).Value = cmbqtyunit.Text.Trim
            GRIDMAGICBOX.Item(gcut.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GREMARKS.Index, TEMPROW).Value = TXTREMARKS.Text.Trim

            GRIDDOUBLECLICK = False
        End If

        GRIDMAGICBOX.FirstDisplayedScrollingRowIndex = GRIDMAGICBOX.RowCount - 1
        txtsrno.Text = GRIDMAGICBOX.RowCount + 1
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
        TXTQTY.Clear()
        TXTRATE.Clear()
        TXTREMARKS.Clear()
        ORDERDATE.Focus()
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBSELLERS.Focus()
    End Sub

    Private Sub cmdEXIT_Click(sender As Object, e As EventArgs) Handles cmdEXIT.Click
        Me.Close()
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

    Private Sub MagicBoxForInvoice_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class