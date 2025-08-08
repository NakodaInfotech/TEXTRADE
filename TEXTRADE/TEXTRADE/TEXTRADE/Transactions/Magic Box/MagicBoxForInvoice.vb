Imports System.ComponentModel
Imports BL
Public Class MagicBoxForInvoice
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDCHGSDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPCHGSROW As Integer
    Public EDIT As Boolean
    Dim DT_CHGSDETAILS As New DataTable
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
                Dim CSRNO As String = ""
                Dim CCHGS As String = ""
                Dim CPER As String = ""
                Dim CAMT As String = ""
                Dim CTAXID As String = ""
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
                    For i As Integer = 0 To DT_CHGSDETAILS.Rows.Count - 1
                        If row.Cells(gsrno.Index).Value.ToString = Val(DT_CHGSDETAILS.Rows(i).Item("EMAINSRNO")) Then
                            If CSRNO = "" Then
                                CSRNO = Val(DT_CHGSDETAILS.Rows(i).Item("ESRNO"))
                                CCHGS = DT_CHGSDETAILS.Rows(i).Item("ECHARGES")
                                CPER = DT_CHGSDETAILS.Rows(i).Item("EPER")
                                CAMT = DT_CHGSDETAILS.Rows(i).Item("EAMT")
                                CTAXID = Val(DT_CHGSDETAILS.Rows(i).Item("ETAXID"))
                            Else
                                CSRNO = CSRNO & "|" & Val(DT_CHGSDETAILS.Rows(i).Item("ESRNO"))
                                CCHGS = CCHGS & "|" & DT_CHGSDETAILS.Rows(i).Item("ECHARGES")
                                CPER = CPER & "|" & DT_CHGSDETAILS.Rows(i).Item("EPER")
                                CAMT = CAMT & "|" & Val(DT_CHGSDETAILS.Rows(i).Item("EAMT"))
                                CTAXID = CTAXID & "|" & Val(DT_CHGSDETAILS.Rows(i).Item("ETAXID"))
                            End If
                        End If
                    Next

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

                alParaval.Add(CSRNO)
                alParaval.Add(CCHGS)
                alParaval.Add(CPER)
                alParaval.Add(CAMT)
                alParaval.Add(CTAXID)


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

                    ' GENERATEPI(Val(row.Index), TEMPCMPID, TEMPYEARID)
                End If
                '******************** END OF PO GENERATION CODE ***************************



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

        DT_CHGSDETAILS.Reset()
        DT_CHGSDETAILS.Columns.Add("ESRNO")
        DT_CHGSDETAILS.Columns.Add("ECHARGES")
        DT_CHGSDETAILS.Columns.Add("EPER")
        DT_CHGSDETAILS.Columns.Add("EAMT")
        DT_CHGSDETAILS.Columns.Add("ETAXID")
        DT_CHGSDETAILS.Columns.Add("EMAINSRNO")


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


        GRIDCHGS.EndEdit() '
        ' Remove all rows for the current entry before adding new ones
        For Each MTRSROW1 As DataGridViewRow In GRIDCHGS.Rows
            Dim currentMainSrNo As Object = MTRSROW1.Cells(EMAINSRNO.Index).Value
            For i As Integer = DT_CHGSDETAILS.Rows.Count - 1 To 0 Step -1
                If DT_CHGSDETAILS.Rows(i)("EMAINSRNO") = currentMainSrNo Then
                    DT_CHGSDETAILS.Rows.RemoveAt(i)
                End If
            Next

            ' Now add new rows for this entry as usual
            For Each MTRSROW As DataGridViewRow In GRIDCHGS.Rows
                If Not MTRSROW.IsNewRow Then
                    Dim newRow As DataRow = DT_CHGSDETAILS.NewRow()
                    newRow("ESRNO") = MTRSROW.Cells(ESRNO.Index).Value
                    newRow("ECHARGES") = MTRSROW.Cells(ECHARGES.Index).Value
                    newRow("EPER") = MTRSROW.Cells(EPER.Index).Value
                    newRow("EAMT") = MTRSROW.Cells(EAMT.Index).Value
                    newRow("ETAXID") = MTRSROW.Cells(ETAXID.Index).Value
                    newRow("EMAINSRNO") = currentMainSrNo
                    DT_CHGSDETAILS.Rows.Add(newRow)
                End If
            Next
        Next

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
        GRIDCHGS.RowCount = 0
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
            OBJSELECTSO.FRMSTRING = "MAGICBOX"
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

    Private Sub CMBPER_Validated(sender As Object, e As EventArgs) Handles CMBPER.Validated
        Try
            GBMTRS.Visible = True
            CMBCHARGES.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validated(sender As Object, e As EventArgs) Handles CMBCHARGES.Validated
        Try
            If CMBCHARGES.Text.Trim <> "" Then
                'filltax()

                'GET ADDLESS DONE BY GULKIT
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS.ACC_ADDLESS,'ADD') AS ADDLESS, ISNULL(LEDGERS.ACC_DISC,0) AS DISCPER ", "", "LEDGERS", " AND ACC_CMPNAME = '" & CMBCHARGES.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If DT.Rows(0).Item("ADDLESS") = "LESS" Then
                        If Val(TXTCHGSPER.Text.Trim) = 0 Then
                            TXTCHGSPER.Text = "-"
                            If ClientName = "SOFTAS" And Val(DT.Rows(0).Item("DISCPER")) > 0 Then TXTCHGSPER.Text = Val(DT.Rows(0).Item("DISCPER")) * -1
                        End If
                        If Val(TXTCHGSAMT.Text.Trim) = 0 Then TXTCHGSAMT.Text = "-"
                        TXTCHGSPER.Select(TXTCHGSPER.Text.Length, 0)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLOSE_Click(sender As Object, e As EventArgs) Handles CMDCLOSE.Click
        GBMTRS.Visible = False
        TXTCOMMPER.Focus()
    End Sub

    Private Sub CMBCHARGES_Enter(sender As Object, e As EventArgs) Handles CMBCHARGES.Enter
        Try
            If CMBCHARGES.Text.Trim = "" Then FILLNAME(CMBCHARGES, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Sales A/C' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validating(sender As Object, e As CancelEventArgs) Handles CMBCHARGES.Validating
        'Try
        '    If CMBCHARGES.Text.Trim <> "" Then NAMEVALIDATE(CMBCHARGES, CMBCODE, e, Me " AND (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Sales A/C' )")
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub TXTCHGSAMT_Validating(sender As Object, e As CancelEventArgs) Handles TXTCHGSAMT.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" And Val(TXTCHGSAMT.Text.Trim) <> 0 Then
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(TXTCHGSAMT.Text.Trim, dDebit)
                If bValid Then
                    TXTCHGSAMT.Text = Convert.ToDecimal(Val(TXTCHGSAMT.Text))
                    ' everything is good
                    fillchgsgrid()
                    TOTAL()
                Else
                    MessageBox.Show("Invalid Number Entered")
                    'e.Cancel = True
                    TXTCHGSAMT.Clear()
                    Exit Sub
                End If
            Else
                If CMBCHARGES.Text.Trim = "" Then
                    MsgBox("Please Fill Charges Name ")
                    Exit Sub
                ElseIf Val(TXTCHGSPER.Text.Trim) = 0 And Val(TXTCHGSAMT.Text.Trim) = 0 Then
                    MsgBox("Amount can not be zero")
                    TXTCHGSAMT.Clear()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillchgsgrid()
        If GRIDCHGSDOUBLECLICK = False Then
            GRIDCHGS.Rows.Add(Val(TXTCHGSSRNO.Text.Trim), CMBCHARGES.Text.Trim, Val(TXTCHGSPER.Text.Trim), Val(TXTCHGSAMT.Text.Trim), Val(TXTTAXID.Text.Trim), txtsrno.Text.Trim)
            getsrno(GRIDCHGS)
        ElseIf GRIDCHGSDOUBLECLICK = True Then
            GRIDCHGS.Item(ESRNO.Index, TEMPCHGSROW).Value = Val(TXTCHGSSRNO.Text.Trim)
            GRIDCHGS.Item(ECHARGES.Index, TEMPCHGSROW).Value = CMBCHARGES.Text.Trim
            GRIDCHGS.Item(EPER.Index, TEMPCHGSROW).Value = Format(Val(TXTCHGSPER.Text.Trim), "0.00")
            GRIDCHGS.Item(EAMT.Index, TEMPCHGSROW).Value = Format(Val(TXTCHGSAMT.Text.Trim), "0.00")
            GRIDCHGS.Item(ETAXID.Index, TEMPCHGSROW).Value = Format(Val(TXTTAXID.Text.Trim))
            GRIDCHGS.Item(EMAINSRNO.Index, TEMPCHGSROW).Value = Format(Val(txtsrno.Text.Trim))

            GRIDCHGSDOUBLECLICK = False

        End If
        TOTAL()

        GRIDCHGS.FirstDisplayedScrollingRowIndex = GRIDCHGS.RowCount - 1

        TXTCHGSSRNO.Clear()
        CMBCHARGES.Text = ""
        TXTCHGSPER.Clear()
        TXTCHGSAMT.Clear()
        TXTTAXID.Clear()
        If TXTCHGSPER.ReadOnly = True Then TXTCHGSPER.ReadOnly = False

        If GRIDCHGS.RowCount > 0 Then
            TXTCHGSSRNO.Text = Val(GRIDCHGS.Rows(GRIDCHGS.RowCount - 1).Cells(0).Value) + 1
        Else
            TXTCHGSSRNO.Text = 1
        End If
        TXTCHGSSRNO.Focus()

    End Sub
    Sub TOTAL()
        Try
            If GRIDCHGS.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDCHGS.Rows
                    TXTCHRGS.Text = Format(Val(TXTCHRGS.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
                Next
                TXTSUBTOTAL.Text = Format(Val(TXTAMT.Text) + Val(TXTCHRGS.Text.Trim), "0.00")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDMAGICBOX_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDMAGICBOX.KeyDown
        If e.KeyCode = Keys.Delete And GRIDMAGICBOX.RowCount > 0 And GRIDMAGICBOX.CurrentRow.DefaultCellStyle.BackColor <> Color.Yellow Then
            If GRIDDOUBLECLICK = True Then
                MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                Exit Sub
            End If

            'end of block

LINE1:
            For I As Integer = 0 To DT_CHGSDETAILS.Rows.Count - 1
                If GRIDMAGICBOX.Rows(GRIDMAGICBOX.CurrentRow.Index).Cells(gsrno.Index).Value = Val(DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO")) Then
                    DT_CHGSDETAILS.Rows.RemoveAt(I)
                    GoTo LINE1
                End If
            Next
            For I As Integer = 0 To DT_CHGSDETAILS.Rows.Count - 1
                If GRIDMAGICBOX.Rows(GRIDMAGICBOX.CurrentRow.Index).Cells(gsrno.Index).Value < Val(DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO")) Then
                    DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO") = Val(DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO")) - 1
                End If
            Next

        End If
    End Sub

    Private Sub GRIDCHGS_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDCHGS.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim del As Boolean = False
                If GRIDCHGS.RowCount > 0 Then
                    Dim row As Integer = GRIDMAGICBOX.Rows(GRIDMAGICBOX.CurrentRow.Index).Cells(gsrno.Index).Value
                    For I As Integer = 0 To DT_CHGSDETAILS.Rows.Count - 1
                        'If GRIDMAGICBOX.Rows(GRIDMAGICBOX.CurrentRow.Index).Cells(gsrno.Index).Value = Val(DT_CHGSDETAILS.Rows(I).Item("MAINSRNO")) And GRIDCHGS.Rows(GRIDMTRS.CurrentRow.Index).Cells(DSRNO.Index).Value = Val(DT_CHGSDETAILS.Rows(I).Item("DSRNO")) Then
                        '    If del = False Then
                        '        DT_CHGSDETAILS.Rows.RemoveAt(I)
                        '        GRIDCHGS.Rows.RemoveAt(GRIDCHGS.CurrentRow.Index)
                        '        del = True
                        '        GoTo line1
                        '    End If
                        'End If
                    Next
line1:
                    For I As Integer = 0 To DT_CHGSDETAILS.Rows.Count - 1
                        If GRIDMAGICBOX.Rows(GRIDMAGICBOX.CurrentRow.Index).Cells(gsrno.Index).Value = Val(DT_CHGSDETAILS.Rows(I).Item("MAINSRNO")) And del = True And row < Val(DT_CHGSDETAILS.Rows(I).Item(gsrno.Index)) Then
                            DT_CHGSDETAILS.Rows(I).Item("DSRNO") = Val(DT_CHGSDETAILS.Rows(I).Item("DSRNO")) - 1
                        End If
                    Next
                    getsrno(GRIDCHGS)
                    TXTCHGSSRNO.Text = GRIDCHGS.RowCount + 1
                    CMBCHARGES.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFOLD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTFOLD.KeyPress, TXTQTY.KeyPress, TXTPCS.KeyPress, TXTMTRS.KeyPress, TXTRATES.KeyPress, TXTAMT.KeyPress, TXTCHRGS.KeyPress, TXTSUBTOTAL.KeyPress, TXTCGSTPER.KeyPress, TXTCGSTAMT.KeyPress, TXTSGSTPER.KeyPress, TXTSGSTAMT.KeyPress, TXTIGSTPER.KeyPress, TXTIGSTAMT.KeyPress, TXTROUNDOFF.KeyPress, TXTGRANDTOTAL.KeyPress, TXTCOMMPER.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub
    Sub CREATELEDGER(NAME As String, TEMPCMPID As Integer, TEMPYEARID As Integer)
        Try

            'ADD IN ACCOUNTSMASTER
            Dim ALPARAVAL As New ArrayList
            Dim OBJSM As New ClsAccountsMaster
            Dim OBJCMN As New ClsCommon
            Dim DTLEDGER As DataTable = OBJCMN.SEARCH(" GROUPMASTER.group_name AS GROUPNAME, ISNULL(LEDGERS.ACC_INTPER, 0) AS INTPER, ISNULL(LEDGERS.Acc_add1,'') AS ADD1, ISNULL(LEDGERS.Acc_add2,'') AS ADD2, ISNULL(AREAMASTER.area_name, '') AS AREA, ISNULL(CITYMASTER.city_name, '') AS CITYNAME, ISNULL(LEDGERS.Acc_zipcode, '') AS PINCODE, ISNULL(STATEMASTER.state_name, '') AS STATE, ISNULL(COUNTRYMASTER.country_name, '') AS COUNTRY, ISNULL(LEDGERS.Acc_crdays, 0) AS CRDAYS, ISNULL(LEDGERS.Acc_crlimit, 0) AS CRLIMIT, ISNULL(LEDGERS.Acc_resino, '') AS RESINO, ISNULL(LEDGERS.Acc_altno, '') AS ALTNO, ISNULL(LEDGERS.Acc_phone, '') 
                         AS PHONENO, ISNULL(LEDGERS.Acc_mobile, '') AS MOBILENO, ISNULL(LEDGERS.ACC_WHATSAPPNO, '') AS WHATSAPPNO, ISNULL(LEDGERS.Acc_fax, '') AS FAX, ISNULL(LEDGERS.Acc_website, '') AS WEBSITE, 
                         ISNULL(LEDGERS.Acc_email, '') AS EMAIL, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS BROKER, ISNULL(LEDGERS.ACC_AGENTCOMM, 0) AS COMMISSION, 
                         ISNULL(LEDGERS.ACC_DISC, 0) AS DISCOUNT, ISNULL(LEDGERS.ACC_CDPER, 0) AS CASHDISC, ISNULL(LEDGERS.ACC_KMS, 0) AS KMS, ISNULL(LEDGERS.Acc_panno, '') AS PANNO, ISNULL(LEDGERS.ACC_GSTIN, '') 
                         AS GSTIN, ISNULL(LEDGERS.Acc_add, '') AS ADDRESS, ISNULL(LEDGERS.Acc_shippingadd, '') AS SHIPPINGADDRESS, ISNULL(LEDGERS.Acc_remarks, '') AS REMARKS, LEDGERS.Acc_code AS CODE, 
                         ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN, ISNULL(DELIVERYCITYMASTER.city_name, '') AS DELIVERYAT, LEDGERS.Acc_TYPE AS TYPE, ISNULL(LEDGERS.ACC_DELIVERYPINCODE, '') 
                         AS DELIVERYPINNO, ISNULL(LEDGERS.ACC_UPI, '') AS UPI, ISNULL(LEDGERS.ACC_MSMENO, '') AS MSME, ISNULL(LEDGERS.ACC_COMMISSION, 0) AS BROKERAGECOMM, ISNULL(LEDGERS.ACC_WARNING, '') 
                         AS WARNINGTEXT, ISNULL(LEDGERS.ACC_GSTINVERIFIED, 0) AS GSTVERIFIED, ISNULL(LEDGERS.ACC_MSMETYPE, '') AS MSMETYPE, ISNULL(LEDGERS.ACC_EXMILLLESS, 0) AS EXMILLLESS, 
                         ISNULL(LEDGERS.ACC_LOCKDAYS, 0) AS LOCKDAYS ", "", " LEDGERS INNER JOIN
                         GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN
						 SALESMANMASTER ON SALESMANMASTER.SALESMAN_ID = LEDGERS.ACC_SALESMANID LEFT OUTER JOIN
                         CITYMASTER AS DELIVERYCITYMASTER ON LEDGERS.ACC_DELIVERYATID = DELIVERYCITYMASTER.city_id LEFT OUTER JOIN
                         LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN
						 LEDGERS AS TRANSLEDGERS ON TRANSLEDGERS.Acc_id = LEDGERS.ACC_TRANSID LEFT OUTER JOIN
                         COUNTRYMASTER ON LEDGERS.Acc_countryid = COUNTRYMASTER.country_id LEFT OUTER JOIN
                         STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN
                         CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN 
						 AREAMASTER ON AREAMASTER.area_id = LEDGERS.Acc_areaid ", " AND LEDGERS.ACC_CMPNAME = '" & NAME & "' AND LEDGERS.ACC_YEARID = " & YearId)



            Dim DTTABLE As DataTable = OBJCMN.SEARCH("CITY_ID AS CITYID", "", "CITYMASTER ", "AND CITY_NAME = '" & DTLEDGER.Rows(0).Item("CITYNAME") & "' AND CITY_YEARID = " & TEMPYEARID)
            If DTTABLE.Rows.Count = 0 Then
                'ADD NEW CITYNAME
                Dim objyearmaster As New ClsYearMaster
                objyearmaster.savecity(DTLEDGER.Rows(0).Item("CITYNAME"), TEMPCMPID, 0, Userid, TEMPYEARID, " and city_name = '" & DTLEDGER.Rows(0).Item("CITYNAME") & "' AND CITY_YEARID = " & TEMPYEARID)
            End If


            DTTABLE = OBJCMN.SEARCH("CITY_ID AS CITYID", "", "CITYMASTER ", "AND CITY_NAME = '" & DTLEDGER.Rows(0).Item("DELIVERYAT") & "' AND CITY_YEARID = " & TEMPYEARID)
            If DTTABLE.Rows.Count = 0 Then
                'ADD NEW CITYNAME
                Dim objyearmaster As New ClsYearMaster
                objyearmaster.savecity(DTLEDGER.Rows(0).Item("DELIVERYAT"), TEMPCMPID, Locationid, Userid, TEMPYEARID, " and city_name = '" & DTLEDGER.Rows(0).Item("DELIVERYAT") & "' AND CITY_CMPID = " & TEMPCMPID & " AND CITY_LOCATIONID = " & Locationid & " AND CITY_YEARID = " & TEMPYEARID)
            End If


            DTTABLE = OBJCMN.SEARCH("AREA_ID AS AREAID", "", "AREAMASTER ", "AND AREA_NAME = '" & DTLEDGER.Rows(0).Item("AREA") & "' AND AREA_YEARID = " & TEMPYEARID)
            If DTTABLE.Rows.Count = 0 Then
                'ADD NEW AREA
                Dim objyearmaster As New ClsYearMaster
                objyearmaster.savearea(DTLEDGER.Rows(0).Item("AREA"), TEMPCMPID, Locationid, Userid, TEMPYEARID, " and AREA_name = '" & DTLEDGER.Rows(0).Item("AREA") & "' AND AREA_CMPID = " & TEMPCMPID & " AND AREA_LOCATIONID = " & Locationid & " AND AREA_YEARID = " & TEMPYEARID)
            End If


            DTTABLE = OBJCMN.SEARCH("STATE_ID AS STATEID", "", "STATEMASTER ", "AND STATE_NAME = '" & DTLEDGER.Rows(0).Item("STATE") & "' AND STATE_YEARID = " & TEMPYEARID)
            If DTTABLE.Rows.Count = 0 Then
                'ADD NEW STATE
                Dim objyearmaster As New ClsYearMaster
                objyearmaster.savestate(DTLEDGER.Rows(0).Item("STATE"), TEMPCMPID, Locationid, Userid, TEMPYEARID, " and STATE_name = '" & DTLEDGER.Rows(0).Item("STATE") & "' AND STATE_YEARID = " & TEMPYEARID)
            End If


            DTTABLE = OBJCMN.SEARCH("COUNTRY_ID AS COUNTRYID", "", "COUNTRYMASTER ", "AND COUNTRY_NAME = '" & DTLEDGER.Rows(0).Item("COUNTRY") & "' AND COUNTRY_YEARID = " & TEMPYEARID)
            If DTTABLE.Rows.Count = 0 Then
                'ADD NEW COUNTRY
                Dim objyearmaster As New ClsYearMaster
                objyearmaster.savecountry(DTLEDGER.Rows(0).Item("COUNTRY"), TEMPCMPID, Locationid, Userid, TEMPYEARID, " and COUNTRY_name = '" & DTLEDGER.Rows(0).Item("COUNTRY") & "' AND COUNTRY_YEARID = " & TEMPYEARID)
            End If





            ALPARAVAL.Add(NAME)
            ALPARAVAL.Add("")   'NAME
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("GROUPNAME"))
            ALPARAVAL.Add(0)    'OPBAL
            ALPARAVAL.Add("Cr.")
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("INTPER")))    'INTPER
            ALPARAVAL.Add(0)    'PROFITPER
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("ADD1"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("ADD2"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("AREA"))   'AREA
            ALPARAVAL.Add("")   'STD
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("CITYNAME"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("PINCODE"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("STATE"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("COUNTRY"))
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("CRDAYS")))
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("CRLIMIT")))    'CRLIMIT
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("RESINO"))   'RESI
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("ALTNO"))   'ALT
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("PHONENO"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("MOBILENO"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("WHATSAPPNO"))   'WHATSAPPNO
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("FAX"))   'FAX
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("WEBSITE"))   'WEBSITE
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("EMAIL"))   'EMAIL

            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("TRANSPORT"))   'TRANS
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("BROKER"))   'AGENT
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("COMMISSION")))    'AGENTCOM
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("DISCOUNT")))    'DISC
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("CASHDISC")))    'CDPER
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("KMS")))    'KMS

            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("PANNO"))   'PAN
            ALPARAVAL.Add("")   'EXISE
            ALPARAVAL.Add("")   'RANGE
            ALPARAVAL.Add("")   'ADDLESS
            ALPARAVAL.Add("")   'CST
            ALPARAVAL.Add("")   'TIN
            ALPARAVAL.Add("")   'ST
            ALPARAVAL.Add("")   'VAT
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("GSTIN"))
            ALPARAVAL.Add("")   'REGISTER
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("ADDRESS"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("SHIPPINGADDRESS"))   'SHIPADD
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("REMARKS"))   'REMARKS
            ALPARAVAL.Add("")   'PARTYBANK
            ALPARAVAL.Add("")   'ACCTYPE
            ALPARAVAL.Add("")   'ACCNO
            ALPARAVAL.Add("")   'IFSCCODE
            ALPARAVAL.Add("")   'BRANCH
            ALPARAVAL.Add("")   'BANKCITY
            ALPARAVAL.Add("")   'GROUPOFCOMPANIES
            ALPARAVAL.Add(0)    'BLOCKED
            ALPARAVAL.Add(0)    'RCM
            ALPARAVAL.Add(0)    'OVERSEAS
            ALPARAVAL.Add(0)    'HOLDFORAPPROVAL
            ALPARAVAL.Add(TEMPCMPID)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(TEMPYEARID)
            ALPARAVAL.Add(0)    'TRANSFER
            ALPARAVAL.Add(NAME) 'CODE
            ALPARAVAL.Add("")    'PRICELIST
            ALPARAVAL.Add("")    'PACKINGTYPE
            ALPARAVAL.Add("")    'TERM
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("SALESMAN"))    'SALESMAN
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("DELIVERYAT"))    'DELIVERYAT (SAME AS CITY WHILE UPLOADING)



            'TDS
            '*******************************
            ALPARAVAL.Add(0)    'ISTDS
            ALPARAVAL.Add("")   'DEDUCTEETYPER
            ALPARAVAL.Add("")   'TDSFORM
            ALPARAVAL.Add("")   'TDSCOMPANY
            ALPARAVAL.Add(0)    'ISLOWER

            ALPARAVAL.Add("")   'SECTION
            ALPARAVAL.Add(Val(0))   'TDSRATE
            ALPARAVAL.Add(0)    'TDSPER
            ALPARAVAL.Add(0) 'SURCHARGE
            ALPARAVAL.Add(0) 'LIMIT
            '*******************************

            ALPARAVAL.Add(0)    'TDSAC
            ALPARAVAL.Add("NON SEZ")    'SEZTYPE
            ALPARAVAL.Add("")   'NATUREOFPAY
            If DTLEDGER.Rows(0).Item("TYPE") <> "" Then ALPARAVAL.Add(DTLEDGER.Rows(0).Item("TYPE")) Else ALPARAVAL.Add("ACCOUNTS")   'TYPE
            ALPARAVAL.Add("")   'CALC
            ALPARAVAL.Add(0)                        'POMNADTE
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("DELIVERYPINNO"))       'DELIVERYPINCODE (SAME AS PINCODE WHILE UPLOADING)
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("UPI"))   'UPI
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("MSME"))   'MSME
            ALPARAVAL.Add(0)    'TCS
            ALPARAVAL.Add("")   'TDSDEDUCTEDAC
            ALPARAVAL.Add(0)    'TDSONGTOTAL
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("BROKERAGECOMM")))    'COMMISSION
            ALPARAVAL.Add("")   'DISTRICT
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("WARNINGTEXT"))   'WARNING TEXT
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("GSTVERIFIED"))   'GSTINVERIFIED
            ALPARAVAL.Add(0)   'PARTYTDS
            ALPARAVAL.Add(0)   'RD
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("MSMETYPE"))   'MSME TYPE
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("EXMILLLESS")))   'EXMILL
            ALPARAVAL.Add(0)   'BILLTOID
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("LOCKDAYS")))   'LOCKDAYS

            'CONTACT DETAILS
            '*******************************
            ALPARAVAL.Add("")   'FOR NAME
            ALPARAVAL.Add(0)   'FOR DESIGNATION
            ALPARAVAL.Add("")   'FOR MOBILE
            ALPARAVAL.Add("")   'FOR EMAIL



            OBJSM.alParaval = ALPARAVAL
            Dim INTRES As Integer = OBJSM.SAVE()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CREATEITEM(ITEMNAME As String, TEMPCMPID As Integer, TEMPYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList
            Dim OBJCMN As New ClsCommon
            Dim DTITEM As DataTable = OBJCMN.SEARCH(" ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT, ISNULL(HSN_CODE,'') AS HSNCODE", "", " ITEMMASTER LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID LEFT OUTER JOIN UNITMASTER ON ITEMMASTER.ITEM_UNITID = UNITMASTER.UNIT_ID ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)


            ALPARAVAL.Add("Finished Goods")
            ALPARAVAL.Add("")   'CATEGORY
            ALPARAVAL.Add(UCase(ITEMNAME))       'DISPLAYNAME
            ALPARAVAL.Add(UCase(ITEMNAME)) 'ITEMNAME

            ALPARAVAL.Add("")   'DEPARTMENT
            ALPARAVAL.Add(UCase(ITEMNAME))        'CODE
            ALPARAVAL.Add(DTITEM.Rows(0).Item("UNIT"))   'UNIT
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

            Dim DTHSN As DataTable = OBJCMN.SEARCH("ISNULL(HSN_ID, 0) AS HSNCODEID", "", " HSNMASTER", " AND HSN_CODE = '" & DTITEM.Rows(0).Item("HSNCODE") & "' AND HSN_YEARID = " & YearId)
            If DTHSN.Rows.Count > 0 Then ALPARAVAL.Add(DTITEM.Rows(0).Item("HSNCODE")) Else ALPARAVAL.Add("") 'HSNCODEID

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

            ALPARAVAL.Add(DBNull.Value) 'IMGPATH
            ALPARAVAL.Add("")   'WARP
            ALPARAVAL.Add("")   'WEFT

            ALPARAVAL.Add(TEMPCMPID)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(TEMPYEARID)
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
            ALPARAVAL.Add("")    'COSTCENTERNAME
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
            ALPARAVAL.Add(0)    'SHADEsrno

            Dim objclsItemMaster As New clsItemmaster
            objclsItemMaster.alParaval = ALPARAVAL
            Dim IntResult As Integer = objclsItemMaster.SAVE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GENERATEPI(ROWNO As Integer, TEMPCMPID As Integer, TEMPYEARID As Integer)
        Try

            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gsrno.Index).Value))
            ALPARAVAL.Add("TOTAL GST")   'screentype
            ALPARAVAL.Add("PURCHASE REGISTER")   'register
            ALPARAVAL.Add("")   'servicetype
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GHSN.Index).Value))
            ALPARAVAL.Add(0)   'CHKCRM
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GBUYERS.Index).Value)
            ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GNO.Index).Value)
            ALPARAVAL.Add("")   'partybilldate
            ALPARAVAL.Add("")   'agent
            ALPARAVAL.Add("")   'challan no
            ALPARAVAL.Add("")   'challandate
            ALPARAVAL.Add("")   'refno
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GCRDAYS.Index).Value)
            ALPARAVAL.Add("")   'duedate
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GTRANS.Index).Value)
            ALPARAVAL.Add("")   'vehicleno
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GLRNO.Index).Value)
            ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add("")   'fromcity
            ALPARAVAL.Add("")   'tocity
            ALPARAVAL.Add("")   'ewaybillno
            ALPARAVAL.Add("")   'noofbales
            ALPARAVAL.Add("")   'dyeingname
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0) '
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GREMARKS.Index).Value)
            ALPARAVAL.Add(0) 'footerdisc
            ALPARAVAL.Add(0) 'footerdscamt

            ALPARAVAL.Add("") '


            'ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDUEDATE.Index).Value).Date, "MM/dd/yyyy"))
            'ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GSELLERS.Index).Value)
            'ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GCRDAYS.Index).Value))
            'ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GDELPERIOD.Index).Value))
            'ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GORDERNO.Index).Value)
            'ALPARAVAL.Add(0)
            'ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "MM/dd/yyyy"))
            'ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GDISCOUNT.Index).Value)
            'ALPARAVAL.Add("")   'TRANSPORT
            'ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GREMARKS.Index).Value)
            'ALPARAVAL.Add("FINISH")
            'ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gQty.Index).Value))
            'ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value))
            'ALPARAVAL.Add(Format(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GRATE.Index).Value) * Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value), "0.00"))  'TOTALAMT


            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(Val(0))
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add("")
            ALPARAVAL.Add(0)
            ALPARAVAL.Add("")
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add("")
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)

            ALPARAVAL.Add(0) 'PO DONE

            ALPARAVAL.Add(1)    'VERIFIED (keep verified for everyone)

            ALPARAVAL.Add(TEMPCMPID)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(TEMPYEARID)
            ALPARAVAL.Add(0)

            Dim gridsrno As String = "1"
            Dim ITEMNAME As String = GRIDMAGICBOX.Rows(ROWNO).Cells(gitemname.Index).Value
            Dim gridremarks As String = ""
            Dim QUALITY As String = ""
            Dim COUNT As String = ""
            Dim REED As String = ""
            Dim PICK As String = ""
            Dim WIDTH As String = ""
            Dim WT As String = ""
            Dim CUT As String = "" 'Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gcut.Index).Value)
            Dim DESIGN As String = "" 'GRIDMAGICBOX.Rows(ROWNO).Cells(GDESIGN.Index).Value
            Dim COLOR As String = ""
            Dim PDESNO As String = ""
            Dim PSHADE As String = ""
            Dim qty As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gQty.Index).Value)
            Dim qtyunit As String = "" 'GRIDMAGICBOX.Rows(ROWNO).Cells(gqtyunit.Index).Value
            Dim MTRS As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value)
            Dim rate As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GRATE.Index).Value)
            Dim PER As String = "Mtrs"
            Dim amount As String = Format(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GRATE.Index).Value) * Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value), "0.00")
            Dim QUOTNO As String = 0         'value of QUOTNO
            Dim QUOTgridsrno As String = 0   'value of QUOTGRIDSRNO
            Dim recdqty As String = 0      'Qty recd in GRN
            Dim GRNDONE As String = 0      'WHETHER GRN IS DONE FOR THIS LINE
            Dim TONAME As String = ""      'WHETHER GRN IS DONE FOR THIS LINE
            Dim CLOSED As String = 0



            ALPARAVAL.Add(gridsrno)
            ALPARAVAL.Add(ITEMNAME)
            ALPARAVAL.Add(gridremarks)
            ALPARAVAL.Add(QUALITY)
            ALPARAVAL.Add(COUNT)
            ALPARAVAL.Add(REED)
            ALPARAVAL.Add(PICK)
            ALPARAVAL.Add(WIDTH)
            ALPARAVAL.Add(WT)
            ALPARAVAL.Add(CUT)
            ALPARAVAL.Add(PER)
            ALPARAVAL.Add(DESIGN)
            ALPARAVAL.Add(COLOR)
            ALPARAVAL.Add(PDESNO)
            ALPARAVAL.Add(PSHADE)
            ALPARAVAL.Add(qty)
            ALPARAVAL.Add(qtyunit)
            ALPARAVAL.Add(MTRS)
            ALPARAVAL.Add(rate)
            ALPARAVAL.Add(amount)
            ALPARAVAL.Add(QUOTNO)
            ALPARAVAL.Add(QUOTgridsrno)
            ALPARAVAL.Add(recdqty)
            ALPARAVAL.Add(GRNDONE)
            ALPARAVAL.Add(TONAME)
            ALPARAVAL.Add(CLOSED)

            ALPARAVAL.Add("")
            ALPARAVAL.Add("")
            ALPARAVAL.Add("PCS")

            Dim OBJPO As New ClsPurchaseOrder()
            OBJPO.alParaval = ALPARAVAL
            Dim DT As DataTable = OBJPO.SAVE()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Sub GENERATESO(ROWNO As Integer, TEMPCMPID As Integer, TEMPYEARID As Integer)
    '    Try

    '        Dim ALPARAVAL As New ArrayList
    '        ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GNO.Index).Value))
    '        ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "MM/dd/yyyy"))
    '        ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GBUYERS.Index).Value)
    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add("")

    '        ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GORDERNO.Index).Value)
    '        ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDUEDATE.Index).Value).Date, "MM/dd/yyyy"))
    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add("")   'REFNO

    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GBUYERS.Index).Value)
    '        ALPARAVAL.Add("")   'CURRENCY
    '        ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gQty.Index).Value))
    '        ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value))
    '        ALPARAVAL.Add(0)       '' *** TOTAL BALE INSTED OF TOTAL AMT.
    '        ALPARAVAL.Add(Format(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GRATE.Index).Value) * Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value), "0.00"))

    '        ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GDISCOUNT.Index).Value))
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)

    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)


    '        ALPARAVAL.Add("")   'INWORDS

    '        ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GREMARKS.Index).Value)
    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add("")


    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GDISCOUNT.Index).Value))
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GCRDAYS.Index).Value))
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add("")




    '        ALPARAVAL.Add(TEMPCMPID)
    '        ALPARAVAL.Add(0)
    '        ALPARAVAL.Add(Userid)
    '        ALPARAVAL.Add(TEMPYEARID)
    '        ALPARAVAL.Add(0)

    '        Dim GRIDSRNO As String = "1"
    '        Dim MERCHANT As String = GRIDMAGICBOX.Rows(ROWNO).Cells(gitemname.Index).Value
    '        Dim QUALITY As String = ""
    '        Dim DESIGN As String = GRIDMAGICBOX.Rows(ROWNO).Cells(GDESIGN.Index).Value
    '        Dim gridremarks As String = ""
    '        Dim COLOR As String = ""
    '        Dim PARTYPONO As String = ""
    '        Dim qty As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gQty.Index).Value)
    '        Dim QTYUNIT As String = GRIDMAGICBOX.Rows(ROWNO).Cells(gqtyunit.Index).Value
    '        Dim CUT As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gcut.Index).Value)
    '        Dim MTRS As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value)
    '        Dim RATE As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GRATE.Index).Value)
    '        Dim PER As String = "Mtrs"
    '        Dim AMOUNT As String = Format(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GRATE.Index).Value) * Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value), "0.00")
    '        Dim RECDQTY As String = 0
    '        Dim RECDMTRS As String = 0
    '        Dim DONE As String = 0
    '        Dim SAMPLEDONE As String = 0
    '        Dim CLOSED As String = 0

    '        ALPARAVAL.Add(GRIDSRNO)
    '        ALPARAVAL.Add(MERCHANT)
    '        ALPARAVAL.Add(QUALITY)
    '        ALPARAVAL.Add(DESIGN)
    '        ALPARAVAL.Add(gridremarks)
    '        ALPARAVAL.Add(COLOR)
    '        ALPARAVAL.Add(PARTYPONO)
    '        ALPARAVAL.Add(qty)
    '        ALPARAVAL.Add(QTYUNIT)
    '        ALPARAVAL.Add(CUT)
    '        ALPARAVAL.Add(MTRS)
    '        ALPARAVAL.Add(RATE)
    '        ALPARAVAL.Add(PER)
    '        ALPARAVAL.Add(AMOUNT)
    '        ALPARAVAL.Add(RECDQTY)
    '        ALPARAVAL.Add(RECDMTRS)
    '        ALPARAVAL.Add(DONE)
    '        ALPARAVAL.Add(SAMPLEDONE)
    '        ALPARAVAL.Add(CLOSED)

    '        ALPARAVAL.Add("")

    '        ALPARAVAL.Add("")
    '        ALPARAVAL.Add(1)    'VERIFIED
    '        ALPARAVAL.Add("PCS")    'ORDERON

    '        Dim OBJSO As New ClsSaleOrder()
    '        OBJSO.alParaval = ALPARAVAL
    '        Dim DT As DataTable = OBJSO.SAVE()

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

End Class