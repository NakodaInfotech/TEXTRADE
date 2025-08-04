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
                Dim HSN As String = ""

                If row.Cells(0).Value <> Nothing Then

                    GRIDSRNO = row.Cells(gsrno.Index).Value.ToString
                    'NO = row.Cells(GNO.Index).Value.ToString
                    PARTYBILLDATE = SafeDate(row.Cells(GBILLDATE.Index).Value)
                    ENTRYDATE = SafeDate(row.Cells(GDATE.Index).Value)
                    SELLERS = row.Cells(GSELLERS.Index).Value.ToString
                    BUYERS = row.Cells(GBUYERS.Index).Value.ToString
                    PARTYBILLNO = row.Cells(GNO.Index).Value.ToString
                    CRDAYS = row.Cells(GCRDAYS.Index).Value.ToString
                    PONO = row.Cells(GPONO.Index).Value.ToString
                    POSRNO = row.Cells(GPOSRNO.Index).Value.ToString
                    POTYPE = row.Cells(GPOTYPE.Index).Value.ToString
                    MERCHANT = row.Cells(gitemname.Index).Value.ToString
                    qty = row.Cells(gQty.Index).Value.ToString
                    FOLD = row.Cells(GFOLD.Index).Value.ToString
                    DESC = row.Cells(GDESC.Index).Value.ToString
                    TRANSPORT = row.Cells(GTRANS.Index).Value.ToString
                    LRNO = row.Cells(GLRNO.Index).Value.ToString
                    LRDATE = SafeDate(row.Cells(GLRDATE.Index).Value)
                    BALENO = row.Cells(GBALENO.Index).Value.ToString
                    PCS = row.Cells(GPCS.Index).Value.ToString
                    MTRS = row.Cells(GMTRS.Index).Value.ToString
                    RATE = row.Cells(GRATE.Index).Value.ToString
                    PER = row.Cells(GPER.Index).Value.ToString
                    AMOUNT = row.Cells(GAMT.Index).Value.ToString
                    CHARGES = row.Cells(GCHARGES.Index).Value.ToString
                    SUBTOTAL = row.Cells(GSUBTOTAL.Index).Value.ToString
                    CGSTPER = row.Cells(GCGST.Index).Value.ToString
                    CGSTAMT = row.Cells(GCGSTAMT.Index).Value.ToString
                    SGSTPER = row.Cells(GSGST.Index).Value.ToString
                    SGSTAMT = row.Cells(GSGSTAMT.Index).Value.ToString
                    IGSTPER = row.Cells(GIGST.Index).Value.ToString
                    IGSTAMT = row.Cells(GIGSTAMT.Index).Value.ToString
                    ROUNDOFF = row.Cells(GROUNDOFF.Index).Value.ToString
                    GRANDTOTAL = row.Cells(GGRANDTOTAL.Index).Value.ToString
                    COMMPER = row.Cells(GCOMPER.Index).Value.ToString
                    COMM = row.Cells(GCOM.Index).Value.ToString
                    REMARKS = row.Cells(GREMARKS.Index).Value.ToString
                    HSN = row.Cells(GHSN.Index).Value.ToString


                End If
                alParaval.Add("TOTAL GST")
                alParaval.Add(GRIDSRNO)
                alParaval.Add(SELLERS)
                alParaval.Add(0)
                alParaval.Add(PONO)
                alParaval.Add(SafeDateString(PARTYBILLDATE))
                alParaval.Add("") 'TXTBALENOFROM.Text.Trim)
                alParaval.Add("") 'TXTBALENOTO.Text.Trim)
                alParaval.Add(SafeDateString(ENTRYDATE))
                alParaval.Add(TRANSPORT) 'CMBLOCALTRANSPORT.Text.Trim)
                alParaval.Add("") 'CMBHASTE.Text.Trim)
                alParaval.Add("") 'CMBAGENT.Text.Trim)
                alParaval.Add("") 'txtchallan.Text.Trim)
                alParaval.Add("")
                alParaval.Add("") 'txtrefno.Text.Trim)
                alParaval.Add("") 'CMBFORMNO.Text.Trim)
                alParaval.Add(Val(CRDAYS))
                alParaval.Add("") 'DueDate.Value.Date)
                alParaval.Add(BUYERS)

                alParaval.Add(TRANSPORT)
                alParaval.Add("") 'TXTVEHICLENO.Text.Trim)
                alParaval.Add(LRNO)
                alParaval.Add(SafeDateString(LRDATE))
                alParaval.Add("") 'CMBFROMCITY.Text.Trim)
                alParaval.Add("") 'CMBTOCITY.Text.Trim)
                alParaval.Add("") 'CMBPACKING.Text.Trim)
                alParaval.Add("") 'TXTEWAYBILLNO.Text.Trim)
                alParaval.Add("") 'TXTGATEPASSNO.Text.Trim)
                alParaval.Add("")

                alParaval.Add(0)


                'If CHKBILLDISPUTE.Checked = True Then
                alParaval.Add(0)
                'If CHKMANUAL.Checked = True Then
                alParaval.Add(0)
                'If CHKEXPORTGST.Checked = True Then
                alParaval.Add(0)
                alParaval.Add(REMARKS)
                'If CHKBARCODE.Checked = True Then
                alParaval.Add(0)

                alParaval.Add(0) 'Val(LBLTOTALBALES.Text.Trim))
                alParaval.Add(0) 'Val(lbltotalpcs.Text.Trim))
                alParaval.Add(0) 'Val(lbltotalmtrs.Text.Trim))
                alParaval.Add(0) 'Val(LBLTOTALAMT.Text.Trim))
                alParaval.Add(0) 'Val(LBLTOTALDISCAMT.Text.Trim))
                alParaval.Add(0) 'Val(LBLTOTALSPDISCAMT.Text.Trim))
                alParaval.Add(0) 'Val(LBLTOTALOTHERAMT.Text.Trim))
                alParaval.Add(0) 'Val(LBLTOTALTAXABLEAMT.Text.Trim))

                alParaval.Add(CGSTPER)
                alParaval.Add(CGSTAMT)
                alParaval.Add(SGSTPER)
                alParaval.Add(SGSTAMT)
                alParaval.Add(IGSTPER)
                alParaval.Add(IGSTAMT)



                alParaval.Add(Val(GRANDTOTAL)) 'TXTTOTALWITHGST.Text.Trim))
                'If CHKTCS.Checked = True Then
                alParaval.Add(0)
                alParaval.Add(Val(0))
                alParaval.Add(Val(0))


                alParaval.Add("") 'txtinwords.Text)

                alParaval.Add(Val(AMOUNT))
                alParaval.Add(Val(CHARGES))
                alParaval.Add(Val(SUBTOTAL))
                alParaval.Add(Val(ROUNDOFF))
                alParaval.Add(Val(GRANDTOTAL))

                alParaval.Add(Val(0)) 'TXTAMTREC.Text.Trim))
                alParaval.Add(Val(0)) 'TXTEXTRAAMT.Text.Trim))
                alParaval.Add(Val(0)) 'TXTRETURN.Text.Trim))
                alParaval.Add(Val(0)) 'TXTBAL.Text.Trim))
                alParaval.Add(Val(0)) 'TXTSONO.Text.Trim))
                alParaval.Add("") '"CMBTERM.Text.Trim)


                'EXPORT DETAILS
                alParaval.Add(Val(0)) 'TXTROE.Text.Trim))
                alParaval.Add("") '"CMBCIF.Text.Trim)
                alParaval.Add("") 'TXTEXPTERMS.Text.Trim)
                alParaval.Add("") 'TXTMARKNOS.Text.Trim)
                alParaval.Add("") 'TXTEXPINSURANCE.Text.Trim)
                alParaval.Add("") 'TXTVESSEL.Text.Trim)
                alParaval.Add("") 'TXTLOADINGPORT.Text.Trim)
                alParaval.Add("") 'TXTDISCHARGEPORT.Text.Trim)
                alParaval.Add("") 'TXTEXPHSN.Text.Trim)
                alParaval.Add("") 'CMBCURRENCY.Text.Trim)
                alParaval.Add(Val(0)) 'TXTGROSSWT.Text.Trim))
                alParaval.Add(Val(0)) 'TXTNETTWT.Text.Trim))
                alParaval.Add(Val(0)) 'TXTSQMTRS.Text.Trim))
                alParaval.Add(Val(0)) 'TXTTOTALUSDAMT.Text.Trim))
                alParaval.Add(Val(0)) 'TXTGSTINVRATE.Text.Trim))
                alParaval.Add(Val(0)) 'TXTCUSTOMINVRATE.Text.Trim))
                alParaval.Add(Val(0)) 'TXTEXPDIFF.Text.Trim))
                alParaval.Add("") 'TXTINWORDSUSD.Text.Trim)


                alParaval.Add("") 'TXTDOCKETNO.Text.Trim)
                alParaval.Add("") 'Format(CKETDATE.Value.Date, "MM/dd/yyyy"))
                alParaval.Add("") 'TXTCOURIER.Text.Trim)


                alParaval.Add(CmpId)
                alParaval.Add(Locationid)
                alParaval.Add(Userid)
                alParaval.Add(YearId)
                alParaval.Add(0)

                alParaval.Add(1)
                alParaval.Add(MERCHANT)
                alParaval.Add(HSN)
                alParaval.Add("")
                alParaval.Add("")
                alParaval.Add("") '"Color)
                alParaval.Add(qty)
                alParaval.Add(FOLD) 'FOLDPER)
                alParaval.Add("") 'PRINTDESC)
                alParaval.Add(BALENO)
                alParaval.Add(PCS)
                alParaval.Add("") 'CUT)
                alParaval.Add(MTRS)
                alParaval.Add(RATE)
                alParaval.Add(PER)
                alParaval.Add(AMOUNT)
                alParaval.Add(LRNO)
                alParaval.Add(TRANSPORT)
                alParaval.Add(0) 'DISCPER)
                alParaval.Add(0) 'DISCAMT)
                alParaval.Add(0) 'SPDISCPER)
                alParaval.Add(0) 'SPDISCAMT)
                alParaval.Add(0) 'OTHERAMT)
                alParaval.Add(0) 'TAXABLEAMT)
                alParaval.Add(CGSTPER)
                alParaval.Add(CGSTAMT)
                alParaval.Add(SGSTPER)
                alParaval.Add(SGSTAMT)
                alParaval.Add(IGSTPER)
                alParaval.Add(IGSTAMT)
                alParaval.Add(GRANDTOTAL)
                alParaval.Add("") 'BARCODE)
                alParaval.Add(PONO)
                alParaval.Add(POSRNO)
                alParaval.Add(POTYPE)
                alParaval.Add(0) 'GRIDDONE)
                alParaval.Add(0) 'GRIDPARTYPONO)
                alParaval.Add("") 'UNIT)
                alParaval.Add(0) 'GRIDSONO)
                alParaval.Add(0) 'GRIDSOSRNO)

                alParaval.Add(0) 'CSRNO)
                alParaval.Add("") 'CCHGS)
                alParaval.Add(0) 'CPER)
                alParaval.Add(0) 'CAMT)
                alParaval.Add(0) 'CTAXID)


                alParaval.Add("") 'griduploadsrno)
                alParaval.Add("") 'uploadremarks)
                alParaval.Add("") 'Name)
                alParaval.Add("") 'imgpath)
                alParaval.Add("") 'NEWIMGPATH)
                alParaval.Add("") 'FILENAME)

                alParaval.Add(ClientName)
                alParaval.Add("") 'TXTIRNNO.Text.Trim)
                alParaval.Add("") 'TXTACKNO.Text.Trim)
                alParaval.Add("") 'Format(ACKDATE.Value.Date, "MM/dd/yyyy"))
                'If PBQRCODE.Image IsNot Nothing Then
                alParaval.Add(DBNull.Value)
                alParaval.Add("") '"CMBDISPATCHFROM.Text.Trim)
                alParaval.Add(REMARKS) 'TXTSPECIALREMARKS.Text.Trim)
                'If CHKCD.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
                alParaval.Add(0)
                'If CHKCHANGEADD.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
                alParaval.Add(0)
                alParaval.Add("") 'txtDeliveryadd.Text.Trim)
                alParaval.Add("") 'CMBSALESMAN.Text.Trim)


                alParaval.Add("") 'CMBSERVICETYPE.Text.Trim)
                alParaval.Add("") 'TXTSACCODE.Text.Trim)
                alParaval.Add(Val(0)) 'TXTMATERIALVALUE.Text.Trim))
                alParaval.Add(Val(0)) 'TXTTOTALWITHMATVALUE.Text.Trim))
                alParaval.Add("") 'CMBCOSTCENTERNAME.Text.Trim)
                alParaval.Add("") 'CMBREFERREDBY.Text.Trim)
                'If CHKTRADINGACC.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
                alParaval.Add(0)
                alParaval.Add(COMMPER)
                alParaval.Add(COMM)

                alParaval.Add("") 'ORDERGRIDSRNO)
                alParaval.Add("") 'ORDERITEMNAME)
                alParaval.Add("") 'ORDERDESIGN)
                alParaval.Add("") 'ORDERCOLOR)
                alParaval.Add("") 'ORDERPCS)
                alParaval.Add("") 'ORDERMTRS)
                alParaval.Add("") 'ORDERFROMNO)
                alParaval.Add("") 'ORDERFROMSRNO)
                alParaval.Add("") 'ORDERFROMTYPE)
                alParaval.Add("") 'ORDERGDNPCS)
                alParaval.Add("") 'ORDERGDNMTRS)
                alParaval.Add("") 'ORDERRATE)
                alParaval.Add("") 'ORDERPARTYPONO)





                Dim objclsPurord As New ClsAgencyInvoiceMaster()
                objclsPurord.alParaval = alParaval
                Dim DT As DataTable = objclsPurord.SAVE()



                ''WE NEED TO CREATE THE SAME ORDER IN ABHEE FABRICS LLP COMPANY
                ''IF BUYER IS ABHEE FABRICS LLP THEN WE NEED TO CREATE PURCHASE ORDER IN THE NAME OF SELLER IN ABHEE FABRICS LLP COMPANY
                'Dim OBJCMN As New ClsCommon
                'Dim TEMPYEARID, TEMPCMPID, TEMPLEDGERID, TEMPITEMID As Integer
                'Dim DTNAME As DataTable = OBJCMN.SEARCH("ISNULL(ACC_NAME,'') AS NAME", "", " LEDGERS", " AND LEDGERS.ACC_CMPNAME = '" & row.Cells(GBUYERS.Index).Value & "' AND LEDGERS.ACC_YEARID = " & YearId)
                'If DTNAME.Rows.Count > 0 AndAlso DTNAME.Rows(0).Item("NAME") = "ABHEE FABRICS LLP" Then

                '    'CREATE PO IN ABHEE FABRICS LLP
                '    'FIRST GET THE CMPID AND YEARID OF ABHEE FABRICS LLP
                '    Dim TEMPDT As DataTable = OBJCMN.SEARCH(" TOP 1 YEAR_CMPID AS CMPID, YEAR_ID AS YEARID", "", " YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMPMASTER.CMP_DISPLAYEDNAME = 'ABHEE FABRICS LLP' ORDER BY YEAR_STARTDATE DESC")
                '    If TEMPDT.Rows.Count > 0 Then
                '        TEMPCMPID = TEMPDT.Rows(0).Item("CMPID")
                '        TEMPYEARID = TEMPDT.Rows(0).Item("YEARID")
                '    Else
                '        GoTo NEXTLINE
                '    End If

                '    'CHECK WHETHER SELLER NAME IS PRESENT OR NOT, IF NOT PRESENT THEN ADD NEW 
                '    TEMPDT = OBJCMN.SEARCH("ACC_ID AS LEDGERID ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & row.Cells(GSELLERS.Index).Value & "' AND ACC_YEARID = " & TEMPYEARID)
                '    If TEMPDT.Rows.Count > 0 Then TEMPLEDGERID = TEMPDT.Rows(0).Item("LEDGERID") Else CREATELEDGER(row.Cells(GSELLERS.Index).Value, TEMPCMPID, TEMPYEARID)


                '    'CHECKING WHETHER ITEM IS PRESENT IN CURRENT YEAR OR NOT, IF NOT PRESENT THEN ADD NEW ITEM
                '    TEMPDT = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", " ITEMMASTER ", " AND ITEM_NAME = '" & row.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & TEMPYEARID)
                '    If TEMPDT.Rows.Count > 0 Then TEMPITEMID = TEMPDT.Rows(0).Item("ITEMID") Else CREATEITEM(row.Cells(gitemname.Index).Value, TEMPCMPID, TEMPYEARID)

                '    GENERATEPO(Val(row.Index), TEMPCMPID, TEMPYEARID)
                'End If
                ''******************** END OF PO GENERATION CODE ***************************



                ''**************** GENERATE SALE ORDER ******************************
                'DTNAME = OBJCMN.SEARCH("ISNULL(ACC_NAME,'') AS NAME", "", " LEDGERS", " AND LEDGERS.ACC_CMPNAME = '" & row.Cells(GSELLERS.Index).Value & "' AND LEDGERS.ACC_YEARID = " & YearId)
                'If DTNAME.Rows.Count > 0 AndAlso DTNAME.Rows(0).Item("NAME") = "ABHEE FABRICS LLP" Then

                '    'CREATE PO IN ABHEE FABRICS LLP
                '    'FIRST GET THE CMPID AND YEARID OF ABHEE FABRICS LLP
                '    Dim TEMPDT As DataTable = OBJCMN.SEARCH(" TOP 1 YEAR_CMPID AS CMPID, YEAR_ID AS YEARID", "", " YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMPMASTER.CMP_DISPLAYEDNAME = 'ABHEE FABRICS LLP' ORDER BY YEAR_STARTDATE DESC")
                '    If TEMPDT.Rows.Count > 0 Then
                '        TEMPCMPID = TEMPDT.Rows(0).Item("CMPID")
                '        TEMPYEARID = TEMPDT.Rows(0).Item("YEARID")
                '    Else
                '        GoTo NEXTLINE
                '    End If

                '    'CHECK WHETHER SELLER NAME IS PRESENT OR NOT, IF NOT PRESENT THEN ADD NEW 
                '    TEMPDT = OBJCMN.SEARCH("ACC_ID AS LEDGERID ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & row.Cells(GBUYERS.Index).Value & "' AND ACC_YEARID = " & TEMPYEARID)
                '    If TEMPDT.Rows.Count > 0 Then TEMPLEDGERID = TEMPDT.Rows(0).Item("LEDGERID") Else CREATELEDGER(row.Cells(GBUYERS.Index).Value, TEMPCMPID, TEMPYEARID)


                '    'CHECKING WHETHER ITEM IS PRESENT IN CURRENT YEAR OR NOT, IF NOT PRESENT THEN ADD NEW ITEM
                '    TEMPDT = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", " ITEMMASTER ", " AND ITEM_NAME = '" & row.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & TEMPYEARID)
                '    If TEMPDT.Rows.Count > 0 Then TEMPITEMID = TEMPDT.Rows(0).Item("ITEMID") Else CREATEITEM(row.Cells(gitemname.Index).Value, TEMPCMPID, TEMPYEARID)

                '    GENERATESO(Val(row.Index), TEMPCMPID, TEMPYEARID)
                'End If
                '******************** END OF SO GENERATION CODE ***************************








NEXTLINE:
            Next

            MessageBox.Show("Details Added")


            CLEAR()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function SafeDate(cellValue As Object) As String
        If IsDate(cellValue) Then
            Return Format(CDate(cellValue), "MM/dd/yyyy")
        Else
            Return ""
        End If
    End Function
    Private Function SafeDateString(val As String) As String
        If IsDate(val) Then
            Return Format(CDate(val), "MM/dd/yyyy")
        Else
            Return ""
        End If
    End Function


    Sub CLEAR()
        TXTGRANDTOTAL.Clear()
        txtsrno.Text = 1
        ENTRYDATE.Value = Now.Date
        BILLDATE.Value = Now.Date
        CMBBUYERS.Text = ""
        CMBSELLERS.Text = ""
        txtcrdays.Clear()
        TXTPARTYBILLNO.Clear()
        TXTPONO.Clear()
        TXTPOSRNO.Clear()
        TXTPOTYPE.Clear()
        cmbitemname.Text = ""
        CMBTRANS.Text = ""
        TXTQTY.Clear()
        TXTFOLD.Clear()
        TXTDESC.Clear()
        TXTLR.Clear()
        LRDATE.Value = Now.Date
        TXTBALENO.Clear()
        TXTPCS.Clear()
        TXTMTRS.Clear()
        TXTRATES.Clear()
        TXTAMT.Clear()
        CMBPER.Text = ""
        TXTREMARKS.Clear()
        TXTCGSTAMT.Clear()
        TXTSGSTAMT.Clear()
        TXTIGSTAMT.Clear()
        TXTSUBTOTAL.Clear()
        TXTCGSTPER.Clear()
        TXTSGSTPER.Clear()
        TXTIGSTPER.Clear()
        TXTCOMMPER.Clear()
        TXTROUNDOFF.Clear()
        TXTCHRGS.Clear()
        CMBCOMM.Text = ""
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
            'txtsrno.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub getmax_SO_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(AINVOICE_no),0) + 1 ", "AGENCYINVOICEMASTER", " AND AINVOICE_cmpid=" & CmpId & " and AINVOICE_locationid=" & Locationid & " and AINVOICE_yearid=" & YearId)
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
            GRIDMAGICBOX.Rows.Add(Val(txtsrno.Text.Trim), BILLDATE.Value.ToString("MM/dd/yyyy"), ENTRYDATE.Value.ToString("MM/dd/yyyy"), CMBSELLERS.Text.Trim, CMBBUYERS.Text.Trim, TXTPARTYBILLNO.Text.Trim, txtcrdays.Text.Trim, TXTPONO.Text.Trim, TXTPOSRNO.Text.Trim, TXTPOTYPE.Text.Trim, cmbitemname.Text.Trim, Format(Val(TXTQTY.Text.Trim), "0.00"), Val(TXTFOLD.Text.Trim), TXTDESC.Text.Trim, CMBTRANS.Text.Trim, TXTLR.Text.Trim, LRDATE.Value.ToString("MM/dd/yyyy"), TXTBALENO.Text.Trim, TXTPCS.Text.Trim, Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATES.Text.Trim), "0.00"), CMBPER.Text.Trim, Format(Val(TXTAMT.Text.Trim), "0.00"), Format(Val(TXTCHRGS.Text.Trim), "0.00"), Format(Val(TXTSUBTOTAL.Text.Trim), "0.00"), Format(Val(TXTCGSTPER.Text.Trim), "0.00"), Format(Val(TXTCGSTAMT.Text.Trim), "0.00"), Format(Val(TXTSGSTPER.Text.Trim), "0.00"), Format(Val(TXTSGSTAMT.Text.Trim), "0.00"), Format(Val(TXTIGSTPER.Text.Trim), "0.00"), Format(Val(TXTIGSTAMT.Text.Trim), "0.00"), Val(TXTROUNDOFF.Text.Trim), Format(Val(TXTGRANDTOTAL.Text.Trim), "0.00"), Format(Val(TXTCOMMPER.Text.Trim), "0.00"), CMBCOMM.Text.Trim, TXTREMARKS.Text.Trim, TXTHSN.Text.Trim)

            'getsrno(GRIDMAGICBOX)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDMAGICBOX.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDMAGICBOX.Item(GBILLDATE.Index, TEMPROW).Value = BILLDATE.Value.Date
            GRIDMAGICBOX.Item(GDATE.Index, TEMPROW).Value = ENTRYDATE.Value.Date
            GRIDMAGICBOX.Item(GSELLERS.Index, TEMPROW).Value = CMBSELLERS.Text.Trim
            GRIDMAGICBOX.Item(GBUYERS.Index, TEMPROW).Value = CMBBUYERS.Text.Trim
            GRIDMAGICBOX.Item(GNO.Index, TEMPROW).Value = TXTPARTYBILLNO.Text.Trim
            GRIDMAGICBOX.Item(GCRDAYS.Index, TEMPROW).Value = txtcrdays.Text.Trim
            GRIDMAGICBOX.Item(GPONO.Index, TEMPROW).Value = TXTPONO.Text.Trim
            GRIDMAGICBOX.Item(GPOSRNO.Index, TEMPROW).Value = TXTPOSRNO.Text.Trim
            GRIDMAGICBOX.Item(GPOTYPE.Index, TEMPROW).Value = TXTPOTYPE.Text.Trim
            GRIDMAGICBOX.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
            GRIDMAGICBOX.Item(gQty.Index, TEMPROW).Value = Format(Val(TXTQTY.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GFOLD.Index, TEMPROW).Value = Val(TXTFOLD.Text.Trim)
            GRIDMAGICBOX.Item(GDESC.Index, TEMPROW).Value = TXTDESC.Text.Trim
            GRIDMAGICBOX.Item(GTRANS.Index, TEMPROW).Value = CMBTRANS.Text.Trim
            GRIDMAGICBOX.Item(GLRNO.Index, TEMPROW).Value = TXTLR.Text.Trim
            GRIDMAGICBOX.Item(GLRDATE.Index, TEMPROW).Value = LRDATE.Value.Date
            GRIDMAGICBOX.Item(GBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim
            GRIDMAGICBOX.Item(GPCS.Index, TEMPROW).Value = TXTPCS.Text.Trim
            GRIDMAGICBOX.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATES.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GPER.Index, TEMPROW).Value = CMBPER.Text.Trim
            GRIDMAGICBOX.Item(GAMT.Index, TEMPROW).Value = Format(Val(TXTAMT.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GCHARGES.Index, TEMPROW).Value = Format(Val(TXTCHRGS.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GSUBTOTAL.Index, TEMPROW).Value = Format(Val(TXTSUBTOTAL.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GCGST.Index, TEMPROW).Value = Format(Val(TXTCGSTPER.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GCGSTAMT.Index, TEMPROW).Value = Format(Val(TXTCGSTAMT.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GSGST.Index, TEMPROW).Value = Format(Val(TXTSGSTPER.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GSGSTAMT.Index, TEMPROW).Value = Format(Val(TXTSGSTAMT.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GIGST.Index, TEMPROW).Value = Format(Val(TXTIGSTPER.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GIGSTAMT.Index, TEMPROW).Value = Format(Val(TXTIGSTAMT.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GROUNDOFF.Index, TEMPROW).Value = Val(TXTROUNDOFF.Text.Trim)
            GRIDMAGICBOX.Item(GGRANDTOTAL.Index, TEMPROW).Value = Format(Val(TXTGRANDTOTAL.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GCOMPER.Index, TEMPROW).Value = Format(Val(TXTCOMMPER.Text.Trim), "0")
            GRIDMAGICBOX.Item(GCOM.Index, TEMPROW).Value = CMBCOMM.Text.Trim
            GRIDMAGICBOX.Item(GREMARKS.Index, TEMPROW).Value = TXTREMARKS.Text.Trim
            GRIDMAGICBOX.Item(GHSN.Index, TEMPROW).Value = TXTHSN.Text.Trim



            GRIDDOUBLECLICK = False
        End If

        GRIDMAGICBOX.FirstDisplayedScrollingRowIndex = GRIDMAGICBOX.RowCount - 1
        txtsrno.Text = txtsrno.Text.Trim + 1
        ' Clear all relevant input fields used in grid entry
        BILLDATE.Value = Date.Today
        ENTRYDATE.Value = Date.Today
        CMBSELLERS.Text = ""
        CMBBUYERS.Text = ""
        TXTPARTYBILLNO.Clear()
        txtcrdays.Clear()
        TXTPONO.Clear()
        TXTPOSRNO.Clear()
        TXTPOTYPE.Clear()
        cmbitemname.Text = ""
        TXTQTY.Clear()
        TXTFOLD.Clear()
        TXTDESC.Clear()
        CMBTRANS.Text = ""
        TXTLR.Clear()
        LRDATE.Value = Date.Today
        TXTBALENO.Clear()
        TXTPCS.Clear()
        TXTMTRS.Clear()
        TXTRATES.Clear()
        CMBPER.Text = ""
        TXTAMT.Clear()
        TXTCHRGS.Clear()
        TXTSUBTOTAL.Clear()
        TXTCGSTPER.Clear()
        TXTCGSTAMT.Clear()
        TXTSGSTPER.Clear()
        TXTSGSTAMT.Clear()
        TXTIGSTPER.Clear()
        TXTIGSTAMT.Clear()
        TXTROUNDOFF.Clear()
        TXTGRANDTOTAL.Clear()
        TXTCOMMPER.Clear()
        CMBCOMM.Text = ""
        TXTREMARKS.Clear()
        TXTHSN.Clear()
        'getmax_SO_no()
        ' Set focus to the first input control
        BILLDATE.Focus()
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBSELLERS.Focus()
    End Sub

    Private Sub cmdEXIT_Click(sender As Object, e As EventArgs) Handles cmdEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDSELECTPO_Click(sender As Object, e As EventArgs) Handles CMDSELECTPO.Click
        Try
            If CMBBUYERS.Text.Trim = "" Then
                MsgBox("Select Party Name", MsgBoxStyle.Critical)
                CMBBUYERS.Focus()
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim DTSO As New DataTable
            Dim OBJSELECTSO As New SelectAgencySO
            OBJSELECTSO.PARTYNAME = CMBBUYERS.Text.Trim
            OBJSELECTSO.ShowDialog()
            DTSO = OBJSELECTSO.DT

            If DTSO.Rows.Count > 0 Then
                Dim DTROW As DataRow = DTSO.Rows(0) ' Use first row

                ' Fill controls with values from selected Sales Order row
                TXTPONO.Text = DTROW("SONO").ToString()
                TXTPOSRNO.Text = DTROW("GRIDSRNO").ToString()
                TXTPOTYPE.Text = DTROW("TYPE").ToString()
                cmbitemname.Text = DTROW("ITEMNAME").ToString()
                TXTQTY.Text = Val(DTROW("QTY").ToString())
                TXTFOLD.Text = "0" ' or fill if available
                TXTDESC.Text = ""     ' or fill if available
                TXTLR.Text = ""       ' or fill if available
                TXTBALENO.Text = "" ' DTROW("BALENO").ToString()
                TXTPCS.Text = DTROW("QTY").ToString()
                TXTMTRS.Text = Format(Val(DTROW("MTRS").ToString()), "0.00")
                TXTRATES.Text = Format(Val(DTROW("RATE").ToString()), "0.00")
                CMBPER.Text = "Mtrs"
                TXTAMT.Text = "0.00"
                TXTCHRGS.Text = "0.00"
                TXTSUBTOTAL.Text = "0.00"
                TXTCGSTPER.Text = "0.00"
                TXTCGSTAMT.Text = "0.00"
                TXTSGSTPER.Text = "0.00"
                TXTSGSTAMT.Text = "0.00"
                TXTIGSTPER.Text = "0.00"
                TXTIGSTAMT.Text = "0.00"
                TXTROUNDOFF.Text = "0.00"
                TXTGRANDTOTAL.Text = "0.00"
                TXTCOMMPER.Text = "0"
                CMBCOMM.Text = ""
                TXTREMARKS.Text = DTROW("REMARKS").ToString()

                ' Optional: Set the BILLDATE if present
                If DTROW.Table.Columns.Contains("DATE") Then
                    BILLDATE.Value = Convert.ToDateTime(DTROW("DATE"))
                End If
                GETHSNCODE()
                ' getsrno(GRIDMAGICBOX)
                TXTPARTYBILLNO.Focus()


            End If

        Catch ex As Exception
            Throw ex
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

    Private Sub TXTREMARKS_Validated(sender As Object, e As EventArgs) Handles TXTREMARKS.Validated
        Try
            If CMBBUYERS.Text <> "" And CMBSELLERS.Text <> "" And cmbitemname.Text <> "" Then
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

    Private Sub CMBBUYERS_Validated(sender As Object, e As EventArgs) Handles CMBBUYERS.Validated
        Try
            If CMBBUYERS.Text.Trim <> "" Then
                CMDSELECTPO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTPO_Validated(sender As Object, e As EventArgs) Handles CMDSELECTPO.Validated
        TXTPARTYBILLNO.Focus()
    End Sub

    Private Sub CMBPER_Validating(sender As Object, e As CancelEventArgs) Handles CMBPER.Validating
        Try
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CALC()
        Try
            If Val(TXTRATES.Text.Trim) > 0 Then TXTAMT.Text = 0.0

            If Val(TXTRATES.Text.Trim) > 0 Then
                If CMBPER.Text = "Mtrs" Or CMBPER.Text = "Yards" Or CMBPER.Text = "Rolls" Then
                    TXTAMT.Text = Format(Val(TXTMTRS.Text) * Val(TXTRATES.Text), "0.00")
                Else
                    TXTAMT.Text = Format(Val(TXTPCS.Text) * Val(TXTRATES.Text), "0.00")
                End If
            End If
            'If Val(TXTDISCPER.Text.Trim) > 0 And Val(TXTDISCAMT.Text.Trim) = 0 Then TXTDISCAMT.Text = Format(Val(TXTAMT.Text.Trim) * (Val(TXTDISCPER.Text.Trim) / 100), "0.00")
            'If Val(TXTSPDISCPER.Text.Trim) > 0 And Val(TXTSPDISCAMT.Text.Trim) = 0 Then TXTSPDISCAMT.Text = Format((Val(TXTAMT.Text.Trim) - Val(TXTDISCAMT.Text.Trim)) * (Val(TXTSPDISCPER.Text.Trim) / 100), "0.00")
            'TXTTAXABLEAMT.Text = Format((Val(TXTAMT.Text.Trim) - Val(TXTDISCAMT.Text.Trim) - Val(TXTSPDISCPER.Text.Trim) + Val(TXTOTHERAMT.Text.Trim)), "0.00")
            TXTCGSTAMT.Text = Format(Val(TXTCGSTPER.Text) / 100 * Val(TXTCHRGS.Text), "0.00")
            TXTSGSTAMT.Text = Format(Val(TXTSGSTPER.Text) / 100 * Val(TXTCHRGS.Text), "0.00")
            TXTSUBTOTAL.Text = Format(Val(TXTAMT.Text) + Val(TXTCHRGS.Text), "0.00")
            TXTIGSTAMT.Text = Format(Val(TXTIGSTPER.Text) / 100 * Val(TXTSUBTOTAL.Text), "0.00")
            TXTGRANDTOTAL.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT.Text) + Val(TXTSGSTAMT.Text) + Val(TXTIGSTAMT.Text) + Val(TXTROUNDOFF.Text), "0.00")
            If ClientName = "ABHEE" Then
                If Val(TXTQTY.Text.Trim) > 0 Then
                    TXTMTRS.Text = Format(Val(TXTQTY.Text.Trim) * (Val(TXTFOLD.Text.Trim) / 100), "0.00")
                End If
            End If

            ' TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub GETHSNCODE()
        Try
            If Convert.ToDateTime(ENTRYDATE.Text).Date >= "01/07/2017" Then

                Dim OBJCMN As New ClsCommon
                'Dim DT As DataTable = OBJCMN.search("  ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND ITEMMASTER.ITEM_NAME= '" & CMBITEM.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
                Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER_DESC.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER_DESC.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER_DESC.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER_DESC.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN HSNMASTER_DESC ON HSNMASTER.HSN_ID = HSNMASTER_DESC.HSN_ID INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND HSNMASTER_DESC.HSN_WEFDATE <= '" & Format(Convert.ToDateTime(ENTRYDATE.Text).Date, "MM/dd/yyyy") & "' AND ITEMMASTER.ITEM_NAME= '" & cmbitemname.Text.Trim & "' AND HSNMASTER.HSN_YEARID=" & YearId & " ORDER BY HSNMASTER_DESC.HSN_WEFDATE DESC")
                If DT.Rows.Count > 0 Then


                    ' TXTHSNCODE.Clear()
                    TXTCGSTPER.Clear()
                    TXTCGSTAMT.Clear()
                    TXTSGSTPER.Clear()
                    TXTSGSTAMT.Clear()
                    TXTIGSTPER.Clear()
                    TXTIGSTAMT.Clear()

                    TXTIGSTPER.Text = 0
                    TXTCGSTPER.Text = Val(DT.Rows(0).Item("CGSTPER"))
                    TXTSGSTPER.Text = Val(DT.Rows(0).Item("SGSTPER"))
                    TXTIGSTPER.Text = Val(DT.Rows(0).Item("IGSTPER"))
                End If
                CALC()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS_Enter(sender As Object, e As EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then FILLNAME(CMBTRANS, EDIT, "  AND ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBTRANS.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = "  AND ACC_TYPE = 'TRANSPORT' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBTRANS.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS_Validating(sender As Object, e As CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then NAMEVALIDATE(CMBTRANS, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validated(sender As Object, e As EventArgs) Handles cmbitemname.Validated
        Try
            Dim OBJCMN As New ClsCommon
            'Dim DT As DataTable = OBJCMN.search("  ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND ITEMMASTER.ITEM_NAME= '" & CMBITEM.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
            Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER_DESC.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER_DESC.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER_DESC.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER_DESC.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN HSNMASTER_DESC ON HSNMASTER.HSN_ID = HSNMASTER_DESC.HSN_ID INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND HSNMASTER_DESC.HSN_WEFDATE <= '" & Format(Convert.ToDateTime(ENTRYDATE.Text).Date, "MM/dd/yyyy") & "' AND ITEMMASTER.ITEM_NAME= '" & cmbitemname.Text.Trim & "' AND HSNMASTER.HSN_YEARID=" & YearId & " ORDER BY HSNMASTER_DESC.HSN_WEFDATE DESC")
            If DT.Rows.Count > 0 Then
                TXTHSN.Text = DT.Rows(0).Item("HSNCODE").ToString()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class