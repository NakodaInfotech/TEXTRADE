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
                    'NO = row.Cells(GNO.Index).Value.ToString
                    PARTYBILLDATE = Format(Convert.ToDateTime(row.Cells(GBILLDATE.Index).Value), "MM/dd/yyyy")
                    ENTRYDATE = Format(Convert.ToDateTime(row.Cells(GDATE.Index).Value), "MM/dd/yyyy")
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
                    LRDATE = Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value), "MM/dd/yyyy")
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


                End If

                alParaval.Add(GRIDSRNO)
                alParaval.Add(SELLERS)
                alParaval.Add(0)
                alParaval.Add(PONO)
                alParaval.Add(Format(Convert.ToDateTime(PARTYBILLDATE).Date, "MM/dd/yyyy"))
                alParaval.Add("") 'TXTBALENOFROM.Text.Trim)
                alParaval.Add("") 'TXTBALENOTO.Text.Trim)
                alParaval.Add(Format(Convert.ToDateTime(ENTRYDATE).Date, "MM/dd/yyyy"))
                alParaval.Add(TRANSPORT) 'CMBLOCALTRANSPORT.Text.Trim)
                alParaval.Add("") 'CMBHASTE.Text.Trim)
                alParaval.Add("") 'CMBAGENT.Text.Trim)
                alParaval.Add("") 'txtchallan.Text.Trim)
                alParaval.Add(Format(Convert.ToDateTime("").Date, "MM/dd/yyyy"))
                alParaval.Add("") 'txtrefno.Text.Trim)
                alParaval.Add("") 'CMBFORMNO.Text.Trim)
                alParaval.Add(Val(CRDAYS))
                alParaval.Add("") 'DueDate.Value.Date)
                alParaval.Add(BUYERS)

                alParaval.Add(TRANSPORT)
                alParaval.Add("") 'TXTVEHICLENO.Text.Trim)
                alParaval.Add(LRNO)
                alParaval.Add(Format(Convert.ToDateTime(LRDATE).Date, "MM/dd/yyyy"))
                alParaval.Add("") 'CMBFROMCITY.Text.Trim)
                alParaval.Add("") 'CMBTOCITY.Text.Trim)
                alParaval.Add("") 'CMBPACKING.Text.Trim)
                alParaval.Add("") 'TXTEWAYBILLNO.Text.Trim)
                alParaval.Add("") 'TXTGATEPASSNO.Text.Trim)
                alParaval.Add(Format(Convert.ToDateTime("").Date, "MM/dd/yyyy"))

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

                alParaval.Add(GRIDSRNO)
                alParaval.Add(MERCHANT)
                alParaval.Add("")
                alParaval.Add("")
                alParaval.Add("")
                alParaval.Add("") '"Color)
                alParaval.Add(qty)
                alParaval.Add("") 'FOLDPER)
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


                alParaval.Add(0) 'griduploadsrno)
                alParaval.Add("") 'uploadremarks)
                alParaval.Add("") 'Name)
                alParaval.Add("") 'imgpath)
                alParaval.Add("") 'NEWIMGPATH)
                alParaval.Add("") 'FILENAME)

                'alParaval.Add(ClientName)
                'alParaval.Add(TXTIRNNO.Text.Trim)
                'alParaval.Add(TXTACKNO.Text.Trim)
                'alParaval.Add(Format(ACKDATE.Value.Date, "MM/dd/yyyy"))
                'If PBQRCODE.Image IsNot Nothing Then
                '    Dim MS As New IO.MemoryStream
                '    PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                '    alParaval.Add(MS.ToArray)
                'Else
                '    alParaval.Add(DBNull.Value)
                'End If
                'alParaval.Add(CMBDISPATCHFROM.Text.Trim)
                'alParaval.Add(TXTSPECIALREMARKS.Text.Trim)
                'If CHKCD.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
                'If CHKCHANGEADD.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
                'alParaval.Add(txtDeliveryadd.Text.Trim)
                'alParaval.Add(CMBSALESMAN.Text.Trim)


                'alParaval.Add(CMBSERVICETYPE.Text.Trim)
                'alParaval.Add(TXTSACCODE.Text.Trim)
                'alParaval.Add(Val(TXTMATERIALVALUE.Text.Trim))
                'alParaval.Add(Val(TXTTOTALWITHMATVALUE.Text.Trim))
                'alParaval.Add(CMBCOSTCENTERNAME.Text.Trim)
                'alParaval.Add(CMBREFERREDBY.Text.Trim)
                'If CHKTRADINGACC.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
                'alParaval.Add(TXTCOMM.Text.Trim)
                'alParaval.Add(CMBCOMMTPYE.Text.Trim)

                'alParaval.Add(ORDERGRIDSRNO)
                'alParaval.Add(ORDERITEMNAME)
                'alParaval.Add(ORDERDESIGN)
                'alParaval.Add(ORDERCOLOR)
                'alParaval.Add(ORDERPCS)
                'alParaval.Add(ORDERMTRS)
                'alParaval.Add(ORDERFROMNO)
                'alParaval.Add(ORDERFROMSRNO)
                'alParaval.Add(ORDERFROMTYPE)
                'alParaval.Add(ORDERGDNPCS)
                'alParaval.Add(ORDERGDNMTRS)
                'alParaval.Add(ORDERRATE)
                'alParaval.Add(ORDERPARTYPONO)





                'Dim objclsPurord As New ClsAgencyInvoiceMaster()
                'objclsPurord.alParaval = alParaval
                'Dim DT As DataTable = objclsPurord.SAVE()



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
    Sub CLEAR()
        txtsrno.Text = 1
        'ORDERDATE.Value = Now.Date
        'CMBBUYERS.Text = ""
        'CMBSELLERS.Text = ""
        'TXTCRDAYS.Clear()
        'TXTDISCOUNT.Clear()
        'TXTDELPERIOD.Clear()
        'DueDate.Value = Now.Date
        'TXTORDERNO.Clear()
        'cmbitemname.Text = ""
        'CMBDESIGN.Text = ""
        'TXTQTY.Clear()
        'cmbqtyunit.Text = "Pcs"
        'TXTCUT.Clear()
        'TXTMTRS.Clear()
        'TXTRATE.Clear()
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

        'If GRIDDOUBLECLICK = False Then
        '    GRIDMAGICBOX.Rows.Add(Val(txtsrno.Text.Trim), Val(TXTNO.Text.Trim), ORDERDATE.Text.Trim, CMBBUYERS.Text.Trim, CMBSELLERS.Text.Trim, TXTCRDAYS.Text.Trim, TXTDISCOUNT.Text.Trim, TXTDELPERIOD.Text.Trim, DueDate.Text.Trim, TXTORDERNO.Text.Trim, cmbitemname.Text.Trim, CMBDESIGN.Text.Trim, Format(Val(TXTQTY.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), TXTREMARKS.Text.Trim)
        '    getsrno(GRIDMAGICBOX)
        'ElseIf GRIDDOUBLECLICK = True Then
        '    GRIDMAGICBOX.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
        '    GRIDMAGICBOX.Item(GNO.Index, TEMPROW).Value = Val(TXTNO.Text.Trim)
        '    GRIDMAGICBOX.Item(GDATE.Index, TEMPROW).Value = ORDERDATE.Text.Trim
        '    GRIDMAGICBOX.Item(GBUYERS.Index, TEMPROW).Value = CMBBUYERS.Text.Trim
        '    GRIDMAGICBOX.Item(GSELLERS.Index, TEMPROW).Value = CMBSELLERS.Text.Trim
        '    GRIDMAGICBOX.Item(GCRDAYS.Index, TEMPROW).Value = TXTCRDAYS.Text.Trim
        '    GRIDMAGICBOX.Item(GDISCOUNT.Index, TEMPROW).Value = TXTDISCOUNT.Text.Trim
        '    GRIDMAGICBOX.Item(GDELPERIOD.Index, TEMPROW).Value = TXTDELPERIOD.Text.Trim
        '    GRIDMAGICBOX.Item(GDUEDATE.Index, TEMPROW).Value = DueDate.Text.Trim
        '    GRIDMAGICBOX.Item(GORDERNO.Index, TEMPROW).Value = TXTORDERNO.Text.Trim
        '    GRIDMAGICBOX.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
        '    GRIDMAGICBOX.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
        '    GRIDMAGICBOX.Item(gQty.Index, TEMPROW).Value = Format(Val(TXTQTY.Text.Trim), "0.00")
        '    GRIDMAGICBOX.Item(gqtyunit.Index, TEMPROW).Value = cmbqtyunit.Text.Trim
        '    GRIDMAGICBOX.Item(gcut.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
        '    GRIDMAGICBOX.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
        '    GRIDMAGICBOX.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")
        '    GRIDMAGICBOX.Item(GREMARKS.Index, TEMPROW).Value = TXTREMARKS.Text.Trim

        '    GRIDDOUBLECLICK = False
        'End If

        'GRIDMAGICBOX.FirstDisplayedScrollingRowIndex = GRIDMAGICBOX.RowCount - 1
        'txtsrno.Text = GRIDMAGICBOX.RowCount + 1
        'TXTNO.Text = Val(TXTNO.Text) + 1
        ''CLEAR()
        'CMBBUYERS.Text = ""
        'CMBSELLERS.Text = ""
        'CMBDESIGN.Text = ""
        'cmbitemname.Text = ""
        'cmbqtyunit.Text = ""
        'TXTCRDAYS.Clear()
        'TXTCUT.Clear()
        'TXTDELPERIOD.Clear()
        'TXTDISCOUNT.Clear()
        'TXTMTRS.Clear()
        'TXTORDERNO.Clear()
        'TXTQTY.Clear()
        'TXTRATE.Clear()
        'TXTREMARKS.Clear()
        'ORDERDATE.Focus()
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
End Class