
Imports System.ComponentModel
Imports BL

Public Class MagicBoxForInvoice
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDCHGSDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPCHGSROW As Integer
    Public EDIT As Boolean
    Dim DT_CHGSDETAILS As New DataTable
    Dim BUYERSTATECODE, SELLERSTATECODE As String

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Try
            For Each row As Windows.Forms.DataGridViewRow In GRIDMAGICBOX.Rows
                Dim SRNO As Integer
                Dim DTTABLE As New DataTable
                DTTABLE = getmax(" isnull(max(AINVOICE_no),0) + 1 ", "AGENCYINVOICEMASTER", " AND AINVOICE_cmpid=" & CmpId & " and AINVOICE_locationid=" & Locationid & " and AINVOICE_yearid=" & YearId)
                If DTTABLE.Rows.Count > 0 Then SRNO = DTTABLE.Rows(0).Item(0)
                row.Cells(gsrno.Index).Value = Val(SRNO)

                Dim alParaval As New ArrayList


                'CHECKING BILLNO DUPLICATION 
                Dim OBJCMN As New ClsCommon
                If row.Cells(GNO.Index).Value <> "" And row.Cells(GSELLERS.Index).Value <> "" Then
                    Dim DTP As DataTable = OBJCMN.SEARCH(" AINVOICE_NO AS BILLNO", "", " AGENCYINVOICEMASTER INNER JOIN LEDGERS ON AGENCYINVOICEMASTER.AINVOICE_PURLEDGERID = LEDGERS.Acc_id", " AND LEDGERS.ACC_CMPNAME = '" & row.Cells(GSELLERS.Index).Value & "' AND AGENCYINVOICEMASTER.AINVOICE_PARTYPONO = '" & row.Cells(GNO.Index).Value & "' AND AINVOICE_YEARID = " & YearId)
                    If DTP.Rows.Count > 0 Then
                        MsgBox("Party Bill " & row.Cells(GNO.Index).Value & " Already Exists in Entry No " & DTP.Rows(0).Item("BILLNO"))
                        GoTo NEXTLINE
                    End If
                End If

                'CHECKING LRNO DUPLICATION 
                If row.Cells(GLRNO.Index).Value <> "" And row.Cells(GTRANS.Index).Value <> "" Then
                    Dim DTP As DataTable = OBJCMN.SEARCH(" AINVOICE_NO AS BILLNO", "", " AGENCYINVOICEMASTER INNER JOIN LEDGERS ON AGENCYINVOICEMASTER.AINVOICE_TRANSID = LEDGERS.Acc_id", " AND LEDGERS.ACC_CMPNAME = '" & row.Cells(GTRANS.Index).Value & "' AND AGENCYINVOICEMASTER.AINVOICE_LRNO = '" & row.Cells(GLRNO.Index).Value & "' AND AINVOICE_YEARID = " & YearId)
                    If DTP.Rows.Count > 0 Then
                        MsgBox("LR No " & row.Cells(GLRNO.Index).Value & " Already Exists In Entry No " & DTP.Rows(0).Item("BILLNO"))
                        GoTo NEXTLINE
                    End If
                End If


                alParaval.Add("TOTAL GST")
                alParaval.Add(SRNO)
                alParaval.Add(row.Cells(GBUYERS.Index).Value)
                alParaval.Add(0)
                alParaval.Add(row.Cells(GNO.Index).Value)
                alParaval.Add(Format(Convert.ToDateTime(row.Cells(GBILLDATE.Index).Value).Date, "MM/dd/yyyy"))    'PODATE
                alParaval.Add("") 'TXTBALENOFROM.Text.Trim)
                alParaval.Add("") 'TXTBALENOTO.Text.Trim)
                alParaval.Add(Format(Convert.ToDateTime(row.Cells(GDATE.Index).Value).Date, "MM/dd/yyyy"))  'entrydate
                alParaval.Add(row.Cells(GTRANS.Index).Value) 'CMBLOCALTRANSPORT.Text.Trim)
                alParaval.Add("") 'CMBHASTE.Text.Trim)
                alParaval.Add("") 'CMBAGENT.Text.Trim)
                alParaval.Add("") 'txtchallan.Text.Trim)
                alParaval.Add(Format(Convert.ToDateTime(row.Cells(GBILLDATE.Index).Value).Date, "MM/dd/yyyy"))  'chdate
                alParaval.Add("") 'txtrefno.Text.Trim)
                alParaval.Add("") 'CMBFORMNO.Text.Trim)
                alParaval.Add(Val(row.Cells(GCRDAYS.Index).Value))
                alParaval.Add(Format(Convert.ToDateTime(row.Cells(GBILLDATE.Index).Value).Date.AddDays(Val(row.Cells(GCRDAYS.Index).Value)).Date, "MM/dd/yyyy")) 'DueDate.Value.Date)
                alParaval.Add(row.Cells(GSELLERS.Index).Value)

                alParaval.Add(row.Cells(GTRANS.Index).Value)
                alParaval.Add("") 'TXTVEHICLENO.Text.Trim)
                alParaval.Add(row.Cells(GLRNO.Index).Value)
                alParaval.Add(Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy"))
                alParaval.Add("") 'CMBFROMCITY.Text.Trim)
                alParaval.Add("") 'CMBTOCITY.Text.Trim)
                alParaval.Add("") 'CMBPACKING.Text.Trim)
                alParaval.Add("") 'TXTEWAYBILLNO.Text.Trim)
                alParaval.Add("") 'TXTGATEPASSNO.Text.Trim)
                alParaval.Add(Format(Convert.ToDateTime(row.Cells(GBILLDATE.Index).Value).Date, "MM/dd/yyyy")) 'GPDATE

                alParaval.Add(0)    'BILLCHECKED
                alParaval.Add(0) 'If CHKBILLDISPUTE.Checked = True Then
                alParaval.Add(Convert.ToBoolean(row.Cells(GMANUALGST.Index).Value))    'CHKMANUAL GST
                alParaval.Add(0) 'If CHKEXPORTGST.Checked = True Then

                alParaval.Add(row.Cells(GREMARKS.Index).Value)
                'If CHKBARCODE.Checked = True Then
                alParaval.Add(0)

                alParaval.Add(0) 'Val(LBLTOTALBALES.Text.Trim))
                alParaval.Add(Val(row.Cells(GPCS.Index).Value)) 'Val(lbltotalpcs.Text.Trim))
                alParaval.Add(Val(row.Cells(GMTRS.Index).Value)) 'Val(lbltotalmtrs.Text.Trim))
                alParaval.Add(Val(row.Cells(GAMT.Index).Value)) 'Val(LBLTOTALAMT.Text.Trim))
                alParaval.Add(0) 'Val(LBLTOTALDISCAMT.Text.Trim))
                alParaval.Add(0) 'Val(LBLTOTALSPDISCAMT.Text.Trim))
                alParaval.Add(0) 'Val(LBLTOTALOTHERAMT.Text.Trim))
                alParaval.Add(0) 'Val(LBLTOTALTAXABLEAMT.Text.Trim))

                alParaval.Add(Val(row.Cells(GCGST.Index).Value))
                alParaval.Add(Val(row.Cells(GCGSTAMT.Index).Value))
                alParaval.Add(Val(row.Cells(GSGST.Index).Value))
                alParaval.Add(Val(row.Cells(GSGSTAMT.Index).Value))
                alParaval.Add(Val(row.Cells(GIGST.Index).Value))
                alParaval.Add(Val(row.Cells(GIGSTAMT.Index).Value))



                alParaval.Add(Val(row.Cells(GSUBTOTAL.Index).Value) + Val(row.Cells(GCGSTAMT.Index).Value) + Val(row.Cells(GSGSTAMT.Index).Value) + Val(row.Cells(GIGSTAMT.Index).Value)) 'TXTTOTALWITHGST.Text.Trim))
                alParaval.Add(0)    'APPLYTCS
                alParaval.Add(Val(0)) 'TCSPER
                alParaval.Add(Val(0)) 'TCSAMT


                alParaval.Add("") 'txtinwords.Text)

                alParaval.Add(Val(row.Cells(GAMT.Index).Value))
                alParaval.Add(Val(row.Cells(GCHARGES.Index).Value))
                alParaval.Add(Val(row.Cells(GSUBTOTAL.Index).Value))
                alParaval.Add(Val(row.Cells(GROUNDOFF.Index).Value))
                alParaval.Add(Val(row.Cells(GGRANDTOTAL.Index).Value))

                alParaval.Add(Val(0)) 'TXTAMTREC.Text.Trim))
                alParaval.Add(Val(0)) 'TXTEXTRAAMT.Text.Trim))
                alParaval.Add(Val(0)) 'TXTRETURN.Text.Trim))
                alParaval.Add(Val(row.Cells(GGRANDTOTAL.Index).Value)) 'TXTBAL.Text.Trim))
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
                alParaval.Add(row.Cells(gitemname.Index).Value)
                alParaval.Add(row.Cells(GHSN.Index).Value)
                alParaval.Add("")   'QUALITY
                alParaval.Add("")   'DESIGN
                alParaval.Add("") '"Color)
                alParaval.Add(Val(row.Cells(gQty.Index).Value))
                alParaval.Add(Val(row.Cells(GFOLD.Index).Value)) 'FOLDPER)
                alParaval.Add(row.Cells(GDESC.Index).Value) 'PRINTDESC)
                alParaval.Add(row.Cells(GBALENO.Index).Value)
                alParaval.Add(Val(row.Cells(GPCS.Index).Value))
                alParaval.Add(Val(row.Cells(GCUT.Index).Value)) 'CUT)
                alParaval.Add(Val(row.Cells(GMTRS.Index).Value))
                alParaval.Add(Val(row.Cells(GRATE.Index).Value))
                alParaval.Add(row.Cells(GPER.Index).Value)
                alParaval.Add(Val(row.Cells(GAMT.Index).Value))
                alParaval.Add(row.Cells(GLRNO.Index).Value)
                alParaval.Add(row.Cells(GTRANS.Index).Value)
                alParaval.Add(0)    'DISCPER
                alParaval.Add(0)    'DISCAMT
                alParaval.Add(0)    'SPDISCPER
                alParaval.Add(0)    'SPDISCAMT
                alParaval.Add(0)    'OTHERAMT

                alParaval.Add(0)    'TAXABLEAMT
                alParaval.Add(0)    'CGSTPER (GRID)
                alParaval.Add(0)    'CGSTAMT (GRID)
                alParaval.Add(0)    'SGSTPER (GRID)
                alParaval.Add(0)    'SGSTAMT (GRID)
                alParaval.Add(0)    'IGSTPER (GRID)
                alParaval.Add(0)    'IGSTAMT (GRID)
                alParaval.Add(Val(row.Cells(GGRANDTOTAL.Index).Value))

                alParaval.Add("") 'BARCODE)
                alParaval.Add(0)    'FROMNO
                alParaval.Add(0)    'FROMSRNO
                alParaval.Add("")   'FROMTYPE
                alParaval.Add(0) 'GRIDDONE)
                alParaval.Add(0) 'GRIDPARTYPONO)
                alParaval.Add("Mtrs") 'UNIT)
                alParaval.Add(Val(row.Cells(GPONO.Index).Value)) 'GRIDSONO)
                alParaval.Add(Val(row.Cells(GPOSRNO.Index).Value)) 'GRIDSOSRNO)

                Dim CSRNO As String = ""
                Dim CCHGS As String = ""
                Dim CPER As String = ""
                Dim CAMT As String = ""
                Dim CTAXID As String = ""

                For Each DTROW As DataRow In DT_CHGSDETAILS.Rows
                    If Val(DTROW("EMAINSRNO")) = Val(row.Index + 1) Then
                        'If Val(DTROW("EMAINSRNO")) = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gsrno.Index).Value) Then
                        If CSRNO = "" Then
                            CSRNO = Val(DTROW("ESRNO"))
                            CCHGS = DTROW("ECHARGES")
                            CPER = Val(DTROW("EPER"))
                            CAMT = Val(DTROW("EAMT"))
                            CTAXID = Val(DTROW("ETAXID"))
                        Else
                            CSRNO = CSRNO & "|" & Val(DTROW("ESRNO"))
                            CCHGS = CCHGS & "|" & DTROW("ECHARGES")
                            CPER = CPER & "|" & Val(DTROW("EPER"))
                            CAMT = CAMT & "|" & Val(DTROW("EAMT"))
                            CTAXID = CTAXID & "|" & Val(DTROW("ETAXID"))
                        End If
                    End If
                Next
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
                alParaval.Add("") 'TXTSPECIALREMARKS.Text.Trim)
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
                alParaval.Add(Val(row.Cells(GCOMPER.Index).Value))
                alParaval.Add(Val(row.Cells(GCOM.Index).Value))


                Dim DTPO As DataTable = OBJCMN.SEARCH("  (CASE WHEN ASO_ORDERON = 'PCS' THEN ROUND(ASO_MTRS - ASO_RECDQTY, 2) ELSE ROUND(ASO_QTY - ASO_RECDQTY, 2) END) AS BALPCS, (CASE WHEN ASO_ORDERON = 'PCS' THEN ROUND(ASO_MTRS - ASO_RECDQTY, 2) ELSE ROUND(ALLAGENCYSALEORDER_DESC.ASO_MTRS - ALLAGENCYSALEORDER_DESC.ASO_RECDMTRS, 2) END) AS BALMTRS, ALLAGENCYSALEORDER_DESC.ASO_RATE AS RATE  ", "", " ALLAGENCYSALEORDER_DESC INNER JOIN ALLAGENCYSALEORDER ON ALLAGENCYSALEORDER_DESC.ASO_NO = ALLAGENCYSALEORDER.ASO_no AND ALLAGENCYSALEORDER_DESC.TYPE = ALLAGENCYSALEORDER.TYPE AND ALLAGENCYSALEORDER_DESC.ASO_YEARID = ALLAGENCYSALEORDER.ASO_YEARID  ", " AND ALLAGENCYSALEORDER_DESC.ASO_NO = " & Val(row.Cells(GPONO.Index).Value) & " AND ALLAGENCYSALEORDER_DESC.ASO_GRIDSRNO = " & Val(row.Cells(GPOSRNO.Index).Value) & " AND ALLAGENCYSALEORDER_DESC.TYPE = '" & row.Cells(GPOTYPE.Index).Value & "' AND ALLAGENCYSALEORDER_DESC.ASO_YEARID = " & YearId)

                alParaval.Add("1")  'ORDERGRIDSRNO
                alParaval.Add(row.Cells(gitemname.Index).Value)    'ORDERITEMNAME
                alParaval.Add("")   'ORDERDESIGN
                alParaval.Add("")   'ORDERCOLOR
                alParaval.Add(Val(DTPO.Rows(0).Item("BALPCS")))
                alParaval.Add(Val(DTPO.Rows(0).Item("BALMTRS")))
                alParaval.Add(Val(row.Cells(GPONO.Index).Value))   'FROMNO
                alParaval.Add(Val(row.Cells(GPOSRNO.Index).Value))   'ORDERFROMSRNO
                alParaval.Add(row.Cells(GPOTYPE.Index).Value)    'ORDERFROMTYPE
                alParaval.Add(Val(row.Cells(GPCS.Index).Value))    'GRNPCS
                alParaval.Add(Val(row.Cells(GMTRS.Index).Value)) 'ORDERGRNMTRS
                alParaval.Add(Val(DTPO.Rows(0).Item("RATE")))   'ORDERRATE
                alParaval.Add("")   'ORDERPARTYPONO


                'alParaval.Add("") 'ORDERGRIDSRNO)
                'alParaval.Add("") 'ORDERITEMNAME)
                'alParaval.Add("") 'ORDERDESIGN)
                'alParaval.Add("") 'ORDERCOLOR)
                'alParaval.Add("") 'ORDERPCS)
                'alParaval.Add("") 'ORDERMTRS)
                'alParaval.Add("") 'ORDERFROMNO)
                'alParaval.Add("") 'ORDERFROMSRNO)
                'alParaval.Add("") 'ORDERFROMTYPE)
                'alParaval.Add("") 'ORDERGDNPCS)
                'alParaval.Add("") 'ORDERGDNMTRS)
                'alParaval.Add("") 'ORDERRATE)
                'alParaval.Add("") 'ORDERPARTYPONO)

                alParaval.Add("")   'COMPLAINT
                alParaval.Add("")   'COMPLAINTBY
                alParaval.Add("")   'COMPLAINTDATE


                alParaval.Add(Convert.ToBoolean(row.Cells(GMANUALROUNDOFF.Index).Value)) 'MANUALROUNDOFF

                Dim objclsPurord As New ClsAgencyInvoiceMaster()
                objclsPurord.alParaval = alParaval
                Dim DT As DataTable = objclsPurord.SAVE()



                'WE WILL HAVE TO CREATE CREDIT NOTE IF TDS IS APPLICABLE IN AGENCY
                If Convert.ToBoolean(row.Cells(GTDS.Index).Value) = True And Val(row.Cells(GTDSAMT.Index).Value) > 0 And row.Cells(GTDSNAME.Index).Value <> "" Then GENERATEAGENCYCN(Val(row.Index))





                'WE NEED TO CREATE THE SAME ORDER IN ABHEE FABRICS LLP COMPANY
                'IF BUYER IS ABHEE FABRICS LLP THEN WE NEED TO CREATE PURCHASE INVOICE IN THE NAME OF SELLER IN ABHEE FABRICS LLP COMPANY
                Dim TEMPYEARID, TEMPCMPID, TEMPLEDGERID, TEMPITEMID As Integer
                Dim DTNAME As DataTable = OBJCMN.SEARCH("ISNULL(ACC_NAME,'') AS NAME", "", " LEDGERS", " AND LEDGERS.ACC_CMPNAME = '" & row.Cells(GBUYERS.Index).Value & "' AND LEDGERS.ACC_YEARID = " & YearId)
                If DTNAME.Rows.Count > 0 AndAlso DTNAME.Rows(0).Item("NAME") = "ABHEE FABRICS LLP" Then

                    'CREATE PURCHASE INVOICE IN ABHEE FABRICS LLP
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

                    GENERATEPI(Val(row.Index), TEMPCMPID, TEMPYEARID)

                    'WE WILL HAVE TO CREATE JOURNAL IF TDS IS APPLICABLE IN LLP
                    If Convert.ToBoolean(row.Cells(GTDS.Index).Value) = True And Val(row.Cells(GTDSAMT.Index).Value) > 0 And row.Cells(GTDSNAME.Index).Value <> "" Then GENERATETDSJOURNAL(Val(row.Index), TEMPCMPID, TEMPYEARID)

                End If
                '******************** END OF PO GENERATION CODE ***************************

NEXTLINE:
            Next
            MessageBox.Show("Details Added")
            CLEAR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GENERATEAGENCYCN(ROWNO As Integer)
        Try

            Dim alParaval As New ArrayList

            alParaval.Add(0)    'CNNO
            alParaval.Add("")   'TYPE
            alParaval.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "MM/dd/yyyy")) 'CNDATE
            alParaval.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "MM/dd/yyyy")) 'ACTUALINVDATE

            alParaval.Add("")   'BILLNO
            alParaval.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GNO.Index).Value)  'PARTYBILLNO
            alParaval.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GBUYERS.Index).Value)  'NAME
            alParaval.Add("")   'AGENT
            alParaval.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GHSN.Index).Value) 'HSNCODE
            alParaval.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GSELLERS.Index).Value) 'DEBITLEDGER
            alParaval.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSNAME.Index).Value)   'PACKING (add debit to)

            alParaval.Add("")   'INVPRINTINITIALS
            alParaval.Add(0)    'PCS
            alParaval.Add(0)    'MTRS
            alParaval.Add(0)    'ACTUALINVAMT
            alParaval.Add(0)    'DISCPER


            alParaval.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSAMT.Index).Value))
            alParaval.Add(0)    'TOTALTAXAMT
            alParaval.Add(0)    'OTHERCHGS
            alParaval.Add(0)    'CHARGES

            alParaval.Add(0)    'RCM
            alParaval.Add(1)    'MANUALGST (KEEP IT TRUE), AS WE NEED 0 GSTAMT
            alParaval.Add(0)    'MANUALROUNDOFF
            alParaval.Add(1)    'NOGSTR1

            alParaval.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSAMT.Index).Value))

            alParaval.Add(0)    'CGSTPER
            alParaval.Add(0)    'CGSTAMT
            alParaval.Add(0)    'SGSTPER
            alParaval.Add(0)    'SGSTAMT
            alParaval.Add(0)    'IGSTPER
            alParaval.Add(0)    'IGSTAMT

            alParaval.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSAMT.Index).Value)) 'TOTALWITHGST
            alParaval.Add(0)    'APPLYTCS
            alParaval.Add(0)    'TCSPER
            alParaval.Add(0)    'TCSAMT

            alParaval.Add(0)    'ROUNDOFF
            alParaval.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSAMT.Index).Value)) 'GTOTAL

            alParaval.Add(0)    'RECAMT
            alParaval.Add(0)    'EXTRAAMT
            alParaval.Add(0)    'RETURN
            alParaval.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSAMT.Index).Value)) 'BAL

            alParaval.Add("")   'REMARKS
            alParaval.Add("")   'BILLREMARKS
            alParaval.Add("")   'INWORDS

            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            alParaval.Add("")   'CSRNO)
            alParaval.Add("")   'CCHGS)
            alParaval.Add("")   'CPER)
            alParaval.Add("")   'CAMT)
            alParaval.Add("")   'CTAXID)

            alParaval.Add("1")  'GRIDSRNO
            alParaval.Add("Against Bill")   'PAYTYPE
            alParaval.Add("S-" & GRIDMAGICBOX.Rows(ROWNO).Cells(GNO.Index).Value)   'BILLINITIALS
            alParaval.Add("")   'NARR
            alParaval.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSAMT.Index).Value)) 'ADJAMT
            alParaval.Add(0)    'RECAMT
            alParaval.Add(0)    'EXTRAAMT
            alParaval.Add(0)    'RETURN
            alParaval.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSAMT.Index).Value)) 'BALANCE

            alParaval.Add("")   'IRN
            alParaval.Add("")   'ACKNO
            alParaval.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "MM/dd/yyyy")) 'ACKDATE
            alParaval.Add(DBNull.Value) 'QRCODE
            alParaval.Add("")   'SPREMARKS
            alParaval.Add(0)    'CD
            alParaval.Add("")   'COSTCENTRE

            alParaval.Add("")   'COMPLAINT
            alParaval.Add("")   'COMPLAINTBY
            alParaval.Add("")   'COMPLAINTDATE

            Dim objclsCNmaster As New ClsAgencyCreditNote()
            objclsCNmaster.alParaval = alParaval
            Dim DTTABLE As DataTable = objclsCNmaster.SAVE()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GENERATETDSJOURNAL(ROWNO As Integer, TEMPCMPID As Integer, TEMPYEARID As Integer)
        Try

            'save entry in journal
            Dim alParaval As New ArrayList
            alParaval.Add(0)
            alParaval.Add("JOURNAL REGISTER")
            alParaval.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "MM/dd/yyyy"))
            alParaval.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSAMT.Index).Value))
            alParaval.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSAMT.Index).Value))
            alParaval.Add("Against Bill No " & GRIDMAGICBOX.Rows(ROWNO).Cells(gsrno.Index).Value & "/" & GRIDMAGICBOX.Rows(ROWNO).Cells(GNO.Index).Value & " Bill Dt. " & Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "dd/MM/yyy"))   'FOR REMARKS
            If GRIDMAGICBOX.Rows(ROWNO).Cells(GNO.Index).Value <> "" Then
                alParaval.Add("Against Bill No " & GRIDMAGICBOX.Rows(ROWNO).Cells(GNO.Index).Value & " Bill Dt. " & Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "dd/MM/yyy"))
            Else
                alParaval.Add("Against Bill No " & GRIDMAGICBOX.Rows(ROWNO).Cells(gsrno.Index).Value & " Bill Dt. " & Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "dd/MM/yyy"))
            End If
            alParaval.Add(TEMPCMPID)
            alParaval.Add(0)
            alParaval.Add(Userid)
            alParaval.Add(TEMPYEARID)
            alParaval.Add(0)

            Dim type As String = ""
            Dim name As String = ""
            Dim paytype As String = ""
            Dim refno As String = ""
            Dim debit As String = ""
            Dim credit As String = ""
            Dim gridsrno As String = ""

            For I As Integer = 0 To 1
                If type = "" Then
                    type = "Dr"
                    name = GRIDMAGICBOX.Rows(ROWNO).Cells(GSELLERS.Index).Value
                    paytype = "Against Bill"
                    refno = "P-" & GRIDMAGICBOX.Rows(ROWNO).Cells(gsrno.Index).Value
                    debit = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSAMT.Index).Value)
                    credit = 0
                    gridsrno = 1
                Else
                    type = type & "|" & "Cr"
                    name = name & "|" & GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSNAME.Index).Value
                    paytype = paytype & "|" & "On Account"
                    refno = refno & "|" & "P-" & GRIDMAGICBOX.Rows(ROWNO).Cells(gsrno.Index).Value
                    debit = debit & "|" & 0
                    credit = credit & "|" & Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSAMT.Index).Value)
                    gridsrno = gridsrno & "|" & 2
                End If
            Next

            alParaval.Add(type)
            alParaval.Add(name)
            alParaval.Add(paytype)
            alParaval.Add(refno)
            alParaval.Add(debit)
            alParaval.Add(credit)
            alParaval.Add(gridsrno)
            alParaval.Add("")   'SPECIAL REMARKS
            alParaval.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GNO.Index).Value)   'PARTYBILLNO
            alParaval.Add("")  'COSTCENTERNAME

            Dim objclsjvmaster As New ClsJournalMaster()
            objclsjvmaster.alParaval = alParaval
            Dim DT As DataTable = objclsjvmaster.save()


            'ACCOUNTS ENTRY TO BE DONE HERE COZ LOOP IS NOT POSSIBLE IN SP
            Dim TEMPJVNO As Integer = DT.Rows(0).Item(0)



            'ACCOUNTSENTRY
            Dim OBJJV As New ClsJournalMaster
            Dim INTRESULT As Integer
            alParaval.Clear()

            alParaval.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSNAME.Index).Value)    'ADDING NAME TOID
            alParaval.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GTDSAMT.Index).Value))    'ADDING NAME TOID
            alParaval.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GSELLERS.Index).Value)    'ADDING NAME TOID

            alParaval.Add(Val(TEMPJVNO))            'JOURNAL NO
            alParaval.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "MM/dd/yyyy"))            'JOURNAL DATE
            alParaval.Add("")        'REMARKS
            alParaval.Add("JOURNAL REGISTER")        'REGISTER
            alParaval.Add(TEMPCMPID)
            alParaval.Add(0)
            alParaval.Add(Userid)
            alParaval.Add(TEMPYEARID)
            alParaval.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GNO.Index).Value)   'partybillno
            alParaval.Add("") 'COSTCENTERNAME

            OBJJV.alParaval = alParaval
            INTRESULT = OBJJV.ACCOUNTS()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        TXTGRANDTOTAL.Clear()
        'txtsrno.Text = 1
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
        TXTFOLD.Text = 100
        TXTDESC.Clear()
        TXTLR.Clear()
        LRDATE.Value = Now.Date
        TXTBALENO.Clear()
        TXTPCS.Clear()
        TXTCUT.Clear()
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
        GRIDDOUBLECLICK = False
        GRIDCHGS.RowCount = 0

        CHKMANUAL.Checked = False
        CHKMANUALROUND.Checked = False

        DT_CHGSDETAILS.Reset()
        DT_CHGSDETAILS.Columns.Add("ESRNO")
        DT_CHGSDETAILS.Columns.Add("ECHARGES")
        DT_CHGSDETAILS.Columns.Add("EPER")
        DT_CHGSDETAILS.Columns.Add("EAMT")
        DT_CHGSDETAILS.Columns.Add("ETAXID")
        DT_CHGSDETAILS.Columns.Add("EMAINSRNO")
        getmax_SO_no()
        getsrno(GRIDCHGS)
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
            If CMBBUYERS.Text.Trim <> "" Then NAMEVALIDATE(CMBBUYERS, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS")
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

    Sub FILLGRID()

        GRIDMAGICBOX.Enabled = True
        Dim currentMainSrNo As Integer = GRIDMAGICBOX.RowCount + 1

        If GRIDDOUBLECLICK = False Then
            GRIDMAGICBOX.Rows.Add(Val(txtsrno.Text.Trim), TXTPARTYBILLNO.Text.Trim, TXTLR.Text.Trim, TXTPONO.Text.Trim, Format(BILLDATE.Value.Date, "dd/MM/yyyy"), Format(ENTRYDATE.Value.Date, "dd/MM/yyyy"), CMBSELLERS.Text.Trim, CMBBUYERS.Text.Trim, Val(txtcrdays.Text), TXTPOSRNO.Text.Trim, TXTPOTYPE.Text.Trim, cmbitemname.Text.Trim, TXTDESC.Text.Trim, Format(Val(TXTPCS.Text.Trim), "0.00"), Format(Val(TXTQTY.Text.Trim), "0.00"), Format(Val(TXTFOLD.Text.Trim), "0.00"), Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATES.Text.Trim), "0.00"), CMBPER.Text.Trim, Format(Val(TXTAMT.Text.Trim), "0.00"), Format(Val(TXTCHRGS.Text.Trim), "0.00"), Format(Val(TXTSUBTOTAL.Text.Trim), "0.00"), Format(Val(TXTCGSTPER.Text.Trim), "0.00"), Format(Val(TXTCGSTAMT.Text.Trim), "0.00"), Format(Val(TXTSGSTPER.Text.Trim), "0.00"), Format(Val(TXTSGSTAMT.Text.Trim), "0.00"), Format(Val(TXTIGSTPER.Text.Trim), "0.00"), Format(Val(TXTIGSTAMT.Text.Trim), "0.00"), Val(TXTROUNDOFF.Text.Trim), Format(Val(TXTGRANDTOTAL.Text.Trim), "0.00"), Format(Val(TXTCOMMPER.Text.Trim), "0.00"), CMBCOMM.Text.Trim, CMBTRANS.Text.Trim, Format(LRDATE.Value.Date, "dd/MM/yyyy"), TXTBALENO.Text.Trim, TXTREMARKS.Text.Trim, TXTHSN.Text.Trim, CHKMANUAL.CheckState, CHKMANUALROUND.CheckState, CHKTDS.CheckState, CMBTDS.Text.Trim, Val(TXTTDSPER.Text.Trim), Val(TXTTDSAMT.Text.Trim))

            'getsrno(GRIDMAGICBOX)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDMAGICBOX.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDMAGICBOX.Item(GNO.Index, TEMPROW).Value = TXTPARTYBILLNO.Text.Trim
            GRIDMAGICBOX.Item(GLRNO.Index, TEMPROW).Value = TXTLR.Text.Trim
            GRIDMAGICBOX.Item(GPONO.Index, TEMPROW).Value = TXTPONO.Text.Trim
            GRIDMAGICBOX.Item(GBILLDATE.Index, TEMPROW).Value = BILLDATE.Value.Date
            GRIDMAGICBOX.Item(GDATE.Index, TEMPROW).Value = ENTRYDATE.Value.Date
            GRIDMAGICBOX.Item(GSELLERS.Index, TEMPROW).Value = CMBSELLERS.Text.Trim
            GRIDMAGICBOX.Item(GBUYERS.Index, TEMPROW).Value = CMBBUYERS.Text.Trim
            GRIDMAGICBOX.Item(GCRDAYS.Index, TEMPROW).Value = Val(txtcrdays.Text.Trim)
            GRIDMAGICBOX.Item(GPOSRNO.Index, TEMPROW).Value = TXTPOSRNO.Text.Trim
            GRIDMAGICBOX.Item(GPOTYPE.Index, TEMPROW).Value = TXTPOTYPE.Text.Trim
            GRIDMAGICBOX.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
            GRIDMAGICBOX.Item(GDESC.Index, TEMPROW).Value = TXTDESC.Text.Trim
            GRIDMAGICBOX.Item(GPCS.Index, TEMPROW).Value = Val(TXTPCS.Text.Trim)
            GRIDMAGICBOX.Item(gQty.Index, TEMPROW).Value = Format(Val(TXTQTY.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GFOLD.Index, TEMPROW).Value = Val(TXTFOLD.Text.Trim)
            GRIDMAGICBOX.Item(GCUT.Index, TEMPROW).Value = Val(TXTCUT.Text.Trim)
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

            GRIDMAGICBOX.Item(GTRANS.Index, TEMPROW).Value = CMBTRANS.Text.Trim
            GRIDMAGICBOX.Item(GLRDATE.Index, TEMPROW).Value = LRDATE.Value.Date
            GRIDMAGICBOX.Item(GBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim

            GRIDMAGICBOX.Item(GREMARKS.Index, TEMPROW).Value = TXTREMARKS.Text.Trim
            GRIDMAGICBOX.Item(GHSN.Index, TEMPROW).Value = TXTHSN.Text.Trim

            GRIDMAGICBOX.Item(GMANUALGST.Index, TEMPROW).Value = CHKMANUAL.Checked
            GRIDMAGICBOX.Item(GMANUALROUNDOFF.Index, TEMPROW).Value = CHKMANUALROUND.Checked
            GRIDMAGICBOX.Item(GTDS.Index, TEMPROW).Value = CHKTDS.Checked
            GRIDMAGICBOX.Item(GTDSNAME.Index, TEMPROW).Value = CMBTDS.Text.Trim
            GRIDMAGICBOX.Item(GTDSPER.Index, TEMPROW).Value = Format(Val(TXTTDSPER.Text.Trim), "0.00")
            GRIDMAGICBOX.Item(GTDSAMT.Index, TEMPROW).Value = Format(Val(TXTTDSAMT.Text.Trim), "0.00")


            currentMainSrNo = TEMPROW
            GRIDDOUBLECLICK = False
        End If
        '        If EDIT = False Then
        'LINE1:
        '            For I As Integer = 0 To DT_CHGSDETAILS.Rows.Count - 1
        '                If GRIDMAGICBOX.Rows(GRIDMAGICBOX.CurrentRow.Index).Cells(gsrno.Index).Value = Val(DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO")) Then
        '                    DT_CHGSDETAILS.Rows.RemoveAt(I)
        '                    GoTo LINE1
        '                End If
        '            Next
        '        End If

        GRIDCHGS.EndEdit() '
        ' Remove all rows for the current entry before adding new ones
        For Each MTRSROW1 As DataGridViewRow In GRIDCHGS.Rows
            'Dim currentMainSrNo As Object = MTRSROW1.Cells(EMAINSRNO.Index).Value
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
        'txtsrno.Text = txtsrno.Text.Trim + 1
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
        TXTDESC.Clear()
        TXTPCS.Clear()
        TXTQTY.Clear()
        TXTFOLD.Text = 100
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
        CMBTRANS.Text = ""
        TXTLR.Clear()
        LRDATE.Value = Date.Today
        TXTBALENO.Clear()
        TXTREMARKS.Clear()
        TXTHSN.Clear()

        getsrno(GRIDMAGICBOX)
        BILLDATE.Focus()
        GRIDCHGS.RowCount = 0
        GBMTRS.Visible = False
        CHKMANUAL.Checked = False
        CHKMANUALROUND.Checked = False
        CHKTDS.Checked = False
        CMBTDS.Text = ""
        TXTTDSPER.Clear()
        TXTTDSAMT.Clear()
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
            OBJSELECTSO.BUYERNAME = CMBBUYERS.Text.Trim
            OBJSELECTSO.SELLERNAME = CMBSELLERS.Text.Trim
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
                GETHSNCODE()
                'TXTQTY.Text = Val(DTROW("QTY").ToString())
                'TXTFOLD.Text = "0" ' or fill if available
                TXTDESC.Text = ""     ' or fill if available
                TXTLR.Text = ""       ' or fill if available
                TXTBALENO.Text = "" ' DTROW("BALENO").ToString()
                TXTPCS.Text = DTROW("PERQTY").ToString()
                'TXTCUT.Text = DTROW("CUT").ToString()
                'TXTMTRS.Text = Format(Val(DTROW("MTRS").ToString()), "0.00")
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

                TXTPARTYBILLNO.Focus()

            End If

            'FETCH DISCOUNT WITH RESPECT TO SALE ORDER 
            If ClientName = "ABHEE" And EDIT = False And Val(TXTPONO.Text.Trim) > 0 Then

                GRIDCHGS.RowCount = 0
                Dim DT As New DataTable
                DT = OBJCMN.SEARCH(" ISNULL(ASO_DISCDEALER, 0) AS DISCPER, ISNULL(ASO_CD, 0) AS CDPER, ISNULL(ASO_DISCRATE,0) AS RATEDIFF, ISNULL(ASO_INT,0) AS AGENTCOMM, ISNULL(ASO_DAYS,0) AS CRDAYS", "", " ALLAGENCYSALEORDER ", " and ALLAGENCYSALEORDER.ASO_NO IN (" & TXTPONO.Text.Trim & ") and ALLAGENCYSALEORDER.ASO_YEARid = " & YearId)

                If DT.Rows.Count > 0 Then

                    txtcrdays.Text = Val(DT.Rows(0).Item("CRDAYS"))


                    'IN CHARGES GRID ADD DISCOUNT GIVEN / BROKERAGE
                    'If (ClientName = "YASHVI" Or ClientName = "SBA" Or ClientName = "DEVEN" Or ClientName = "SOFTAS" Or ClientName = "BARKHA" Or ClientName = "AVIS" Or ClientName = "MOMAI" Or ClientName = "SHREEVALLABH") Then
                    'INITIALLY IT WAS WITH RESPECT TO THE ABOVE MENTIONED CLIENT, THEN CHANGED WITH RESPECT TO SALEAUTODISCOUNT
                    If SALEAUTODISCOUNT = True And EDIT = False Then
                        For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                            If DTROW.Cells(ECHARGES.Index).Value = "DISCOUNT GIVEN" Then GoTo LINE2
                        Next
                        If Val(DT.Rows(0).Item("DISCPER")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "DISCOUNT GIVEN", Val(DT.Rows(0).Item("DISCPER")) * -1, 0, 0, Val(txtsrno.Text.Trim))
                    End If

LINE2:
                End If
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

    Sub getmax_SO_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(AINVOICE_no),0) + 1 ", "AGENCYINVOICEMASTER", " AND AINVOICE_cmpid=" & CmpId & " and AINVOICE_locationid=" & Locationid & " and AINVOICE_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            txtsrno.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub TXTREMARKS_Validated(sender As Object, e As EventArgs) Handles TXTREMARKS.Validated
        Try
            If CMBBUYERS.Text.Trim <> "" And CMBSELLERS.Text.Trim <> "" And cmbitemname.Text.Trim <> "" And TXTPARTYBILLNO.Text.Trim <> "" And Val(TXTQTY.Text.Trim) > 0 And Val(TXTFOLD.Text.Trim) > 0 And Val(TXTMTRS.Text.Trim) > 0 And Val(TXTRATES.Text.Trim) > 0 And CMBTRANS.Text.Trim <> "" And TXTLR.Text.Trim <> "" Then
                FILLGRID()
            Else
                MsgBox("Please Enter Detail Properly.", MsgBoxStyle.Critical)
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
                'GET BUYERSTATECODE
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(CAST(STATEMASTER.STATE_REMARK AS VARCHAR(50)),'') AS STATECODE", "", " LEDGERS INNER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID ", " AND LEDGERS.ACC_CMPNAME = '" & CMBBUYERS.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then BUYERSTATECODE = DT.Rows(0).Item("STATECODE")

                GETHSNCODE()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            If CHKMANUALROUND.CheckState = CheckState.Unchecked Then TXTROUNDOFF.Text = 0

            If ClientName = "ABHEE" AndAlso Val(TXTQTY.Text.Trim) > 0 And Val(TXTFOLD.Text.Trim) > 0 Then TXTMTRS.Text = Format(Val(TXTQTY.Text.Trim) * (Val(TXTFOLD.Text.Trim) / 100), "0.00")
            If CMBPER.Text = "Qty" Then
                TXTAMT.Text = Format(Val(TXTQTY.Text) * Val(TXTRATES.Text), "0.00")
            Else
                TXTAMT.Text = Format(Val(TXTMTRS.Text) * Val(TXTRATES.Text), "0.00")
            End If
            If Val(TXTPCS.Text.Trim) > 0 And Val(TXTCUT.Text.Trim) > 0 Then TXTMTRS.Text = Val(TXTPCS.Text.Trim) * (TXTCUT.Text.Trim)
            TXTSUBTOTAL.Text = Format(Val(TXTAMT.Text) + Val(TXTCHRGS.Text), "0.00")

            If CHKMANUAL.CheckState = CheckState.Unchecked Then
                TXTCGSTAMT.Text = Format(Val(TXTCGSTPER.Text) / 100 * Val(TXTSUBTOTAL.Text), "0.00")
                TXTSGSTAMT.Text = Format(Val(TXTSGSTPER.Text) / 100 * Val(TXTSUBTOTAL.Text), "0.00")
                TXTIGSTAMT.Text = Format(Val(TXTIGSTPER.Text) / 100 * Val(TXTSUBTOTAL.Text), "0.00")
            End If

            If CHKMANUALROUND.Checked = False Then
                TXTGRANDTOTAL.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT.Text) + Val(TXTSGSTAMT.Text) + Val(TXTIGSTAMT.Text), "0")
                TXTROUNDOFF.Text = Format(Val(TXTGRANDTOTAL.Text) - (Val(TXTSUBTOTAL.Text.Trim) + Val(TXTCGSTAMT.Text) + Val(TXTSGSTAMT.Text) + Val(TXTIGSTAMT.Text)), "0.00")
            Else
                TXTGRANDTOTAL.Text = Val(TXTSUBTOTAL.Text.Trim) + Val(TXTCGSTAMT.Text) + Val(TXTSGSTAMT.Text) + Val(TXTIGSTAMT.Text) + Val(TXTROUNDOFF.Text.Trim)
            End If
            TXTGRANDTOTAL.Text = Format(Val(TXTGRANDTOTAL.Text.Trim), "0.00")

            'TDS CALC
            If Val(TXTTDSPER.Text.Trim) > 0 And CHKTDS.Checked = True Then TXTTDSAMT.Text = Format((Val(TXTTDSPER.Text.Trim) * Val(TXTSUBTOTAL.Text.Trim) / 100), "0")

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


                    TXTHSN.Clear()
                    TXTCGSTPER.Clear()
                    TXTCGSTAMT.Clear()
                    TXTSGSTPER.Clear()
                    TXTSGSTAMT.Clear()
                    TXTIGSTPER.Clear()
                    TXTIGSTAMT.Clear()

                    If BUYERSTATECODE = SELLERSTATECODE Then
                        TXTCGSTPER.Text = Val(DT.Rows(0).Item("CGSTPER"))
                        TXTSGSTPER.Text = Val(DT.Rows(0).Item("SGSTPER"))
                        TXTIGSTPER.Text = 0
                    Else
                        TXTCGSTPER.Text = 0
                        TXTSGSTPER.Text = 0
                        TXTIGSTPER.Text = Val(DT.Rows(0).Item("IGSTPER"))
                    End If
                    TXTHSN.Text = DT.Rows(0).Item("HSNCODE")
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
            GETHSNCODE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPER_Validated(sender As Object, e As EventArgs) Handles CMBPER.Validated
        Try
            If cmbitemname.Text.Trim = "" Then
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
                Exit Sub
            Else
                GBMTRS.Visible = True


                If GRIDDOUBLECLICK = False Then
                    'TEMPDTMTRS.Clear()
                    'GRIDCHGS.RowCount = 0
                    GRIDDOUBLECLICK = False
                    'Dim i As Integer = 0
                    'While i < TEMPDTMTRS.Rows.Count
                    '    If TEMPDTMTRS.Rows(i).Item("MAINSRNO") = Val(txtsrno.Text.Trim) Then
                    '        TEMPDTMTRS.Rows.RemoveAt(i)
                    '        'GRIDMTRS.Rows.RemoveAt(GRIDMTRS.CurrentRow.Index)
                    '    Else
                    '        i += 1 ' Only increment if no row is removed
                    '    End If
                    'End While
                    TOTAL()
                Else
                    If GRIDMAGICBOX.Rows.Count > 0 Then
                        GRIDCHGS.RowCount = 0
                        GRIDCHGSDOUBLECLICK = False
                        For i As Integer = 0 To DT_CHGSDETAILS.Rows.Count - 1
                            If DT_CHGSDETAILS.Rows(i).Item("EMAINSRNO") = Val(GRIDMAGICBOX.CurrentRow.Cells(gsrno.Index).Value) Then
                                GRIDCHGS.Rows.Add(DT_CHGSDETAILS.Rows(i).Item("ESRNO"), DT_CHGSDETAILS.Rows(i).Item("ECHARGES"), DT_CHGSDETAILS.Rows(i).Item("EPER"), DT_CHGSDETAILS.Rows(i).Item("EAMT"), DT_CHGSDETAILS.Rows(i).Item("ETAXID"), DT_CHGSDETAILS.Rows(i).Item("EMAINSRNO"))
                            End If
                        Next
                        TOTAL()
                    End If
                End If
                TXTCHGSSRNO.Text = GRIDCHGS.RowCount + 1
                CMBCHARGES.Focus()
            End If
            GBMTRS.Visible = True
            TOTAL()
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
                        If Val(TXTCHGSPER.Text.Trim) = 0 Then TXTCHGSPER.Text = "-"
                        If Val(TXTCHGSAMT.Text.Trim) = 0 Then TXTCHGSAMT.Text = "-"
                        TXTCHGSPER.Select(TXTCHGSPER.Text.Length, 0)
                    End If
                End If
            Else
                CMDCLOSE.Focus()
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
        Try
            If CMBCHARGES.Text.Trim <> "" Then NAMEVALIDATE(CMBCHARGES, CMBCODE, e, Me, TXTADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Sales A/C' )", "Indirect Expenses", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
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
                    ' TOTAL()
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
            'If GRIDCHGS.RowCount > 0 Then
            '    For Each row As DataGridViewRow In GRIDCHGS.Rows
            '        TXTCHRGS.Text = Format(Val(TXTCHRGS.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
            '    Next
            '    TXTSUBTOTAL.Text = Format(Val(TXTAMT.Text) + Val(TXTCHRGS.Text.Trim), "0.00")
            'End If
            TXTCHRGS.Text = 0.0

            If GRIDCHGS.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDCHGS.Rows
                    If SALEAUTODISCOUNT = True Then
                        'IF PERCENT IS > 0 THEN GETAUTO CHARGES
                        Dim OBJCMN As New ClsCommon
                        Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(ACC_CALC,'GROSS') AS CALC", "", "LEDGERS", "AND ACC_CMPNAME = '" & row.Cells(ECHARGES.Index).Value & "' AND ACC_YEARID = " & YearId)
                        If DT.Rows.Count > 0 Then
                            If DT.Rows(0).Item("CALC") = "GROSS" And Val(row.Cells(EPER.Index).Value) <> 0 Then
                                row.Cells(EAMT.Index).Value = Format((Val(row.Cells(EPER.Index).Value) * Val(TXTAMT.Text.Trim)) / 100, "0.00")
                            ElseIf DT.Rows(0).Item("CALC") = "NETT" And Val(row.Cells(EPER.Index).Value) <> 0 Then
                                TXTNETTAMT.Text = Val(TXTAMT.Text.Trim)
                                For I As Integer = 0 To row.Index - 1
                                    TXTNETTAMT.Text = Format(Val(TXTNETTAMT.Text) + Val(GRIDCHGS.Rows(I).Cells(EAMT.Index).Value), "0.00")
                                Next
                                row.Cells(EAMT.Index).Value = Format((Val(row.Cells(EPER.Index).Value) * Val(TXTNETTAMT.Text.Trim)) / 100, "0.00")
                                'TXTCHGSAMT.Text = Format((Val(TXTNETT.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
                            ElseIf DT.Rows(0).Item("CALC") = "MTRS" And Val(row.Cells(EPER.Index).Value) <> 0 Then
                                row.Cells(EAMT.Index).Value = Format(Val(row.Cells(EPER.Index).Value) * Val(TXTMTRS.Text.Trim), "0.00")
                            ElseIf DT.Rows(0).Item("CALC") = "PCS" And Val(row.Cells(EPER.Index).Value) <> 0 Then
                                row.Cells(EAMT.Index).Value = Format(Val(row.Cells(EPER.Index).Value) * Val(TXTPCS.Text.Trim), "0.00")
                            End If
                        End If
                    End If
                    TXTCHRGS.Text = Format(Val(TXTCHRGS.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
                Next
            End If
            CALC()

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

LINE1:
            For I As Integer = 0 To DT_CHGSDETAILS.Rows.Count - 1
                If Val(DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO")) = GRIDMAGICBOX.CurrentRow.Index + 1 Then
                    DT_CHGSDETAILS.Rows.RemoveAt(I)
                    GoTo LINE1
                End If
            Next
            For I As Integer = 0 To DT_CHGSDETAILS.Rows.Count - 1
                If Val(DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO")) > (GRIDMAGICBOX.CurrentRow.Index + 1) Then
                    DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO") = Val(DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO")) - 1
                End If
            Next
            GRIDMAGICBOX.Rows.RemoveAt(GRIDMAGICBOX.CurrentRow.Index)
            getmax_SO_no()
            TOTAL()
        End If
    End Sub

    Private Sub GRIDCHGS_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDCHGS.KeyDown
        Try
            '            If e.KeyCode = Keys.Delete Then
            '                Dim del As Boolean = False
            '                If GRIDCHGS.RowCount > 0 Then
            '                    Dim row As Integer = GRIDMAGICBOX.Rows(GRIDMAGICBOX.CurrentRow.Index).Cells(gsrno.Index).Value


            'line1:
            '                    For I As Integer = 0 To DT_CHGSDETAILS.Rows.Count - 1
            '                        If GRIDMAGICBOX.Rows(GRIDMAGICBOX.CurrentRow.Index).Cells(gsrno.Index).Value = Val(DT_CHGSDETAILS.Rows(I).Item("MAINSRNO")) And del = True And row < Val(DT_CHGSDETAILS.Rows(I).Item(gsrno.Index)) Then
            '                            DT_CHGSDETAILS.Rows(I).Item("DSRNO") = Val(DT_CHGSDETAILS.Rows(I).Item("DSRNO")) - 1
            '                        End If
            '                    Next
            '                    GRIDCHGS.Rows.RemoveAt(GRIDCHGS.CurrentRow.Index)
            '                    TOTAL()
            '                    getsrno(GRIDCHGS)
            '                    TXTCHGSSRNO.Text = GRIDCHGS.RowCount + 1
            '                    CMBCHARGES.Focus()
            '                End If
            '            End If

            If e.KeyCode = Keys.Delete Then
                Dim del As Boolean = False
                If GRIDCHGS.RowCount > 0 Then
                    'Dim row As Integer = GRIDMAGICBOX.Rows(GRIDMAGICBOX.CurrentRow.Index).Cells(gsrno.Index).Value
                    For I As Integer = 0 To DT_CHGSDETAILS.Rows.Count - 1
                        If Val(txtsrno.Text.Trim) = Val(DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO")) And GRIDCHGS.Rows(GRIDCHGS.CurrentRow.Index).Cells(ESRNO.Index).Value = Val(DT_CHGSDETAILS.Rows(I).Item("ESRNO")) Then
                            If del = False Then
                                DT_CHGSDETAILS.Rows.RemoveAt(I)
                                del = True
                                GoTo line1
                            End If
                        End If
                    Next
line1:
                    For I As Integer = 0 To DT_CHGSDETAILS.Rows.Count - 1
                        If Val(txtsrno.Text.Trim) = Val(DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO")) And del = True And Val(txtsrno.Text.Trim) < Val(DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO")) Then
                            DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO") = Val(DT_CHGSDETAILS.Rows(I).Item("EMAINSRNO")) - 1
                        End If
                    Next
                    GRIDCHGS.Rows.RemoveAt(GRIDCHGS.CurrentRow.Index)
                    TOTAL()
                    getsrno(GRIDCHGS)
                    TXTCHGSSRNO.Text = GRIDCHGS.RowCount + 1
                    CMBCHARGES.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFOLD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTFOLD.KeyPress, TXTQTY.KeyPress, TXTPCS.KeyPress, TXTCUT.KeyPress, TXTMTRS.KeyPress, TXTRATES.KeyPress, TXTAMT.KeyPress, TXTCHRGS.KeyPress, TXTCHGSPER.KeyPress, TXTCHGSAMT.KeyPress, TXTSUBTOTAL.KeyPress, TXTCGSTPER.KeyPress, TXTCGSTAMT.KeyPress, TXTSGSTPER.KeyPress, TXTSGSTAMT.KeyPress, TXTIGSTPER.KeyPress, TXTIGSTAMT.KeyPress, TXTROUNDOFF.KeyPress, TXTGRANDTOTAL.KeyPress, TXTCOMMPER.KeyPress, TXTCGSTAMT.KeyPress, TXTSGSTAMT.KeyPress, TXTIGSTAMT.KeyPress
        AMOUNTNUMDOTKYEPRESS(e, sender, Me)
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
            ALPARAVAL.Add("GOODS PURCHASE")   'servicetype
            ALPARAVAL.Add("")   'SACCODE
            ALPARAVAL.Add(0)   'CHKCRM
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GSELLERS.Index).Value)
            ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "MM/dd/yyyy"))

            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GNO.Index).Value)
            ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GBILLDATE.Index).Value).Date, "MM/dd/yyyy"))   'partybilldate

            ALPARAVAL.Add("")   'agent
            ALPARAVAL.Add("")   'challan no
            ALPARAVAL.Add("")   'challandate
            ALPARAVAL.Add("")   'refno

            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GCRDAYS.Index).Value)
            ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GBILLDATE.Index).Value).Date.AddDays(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GCRDAYS.Index).Value)), "MM/dd/yyyy"))   'duedate

            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GTRANS.Index).Value)
            ALPARAVAL.Add("")   'vehicleno
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GLRNO.Index).Value)
            ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add("")   'fromcity
            ALPARAVAL.Add("")   'tocity
            ALPARAVAL.Add("")   'ewaybillno
            ALPARAVAL.Add(1)   'noofbales
            ALPARAVAL.Add("")   'dyeingname
            ALPARAVAL.Add(0)    'BILLCHECKED
            ALPARAVAL.Add(0)    'DISPUTE
            ALPARAVAL.Add(Convert.ToBoolean(GRIDMAGICBOX.Rows(ROWNO).Cells(GMANUALGST.Index).Value))    'MANUALGST  
            ALPARAVAL.Add(Convert.ToBoolean(GRIDMAGICBOX.Rows(ROWNO).Cells(GMANUALROUNDOFF.Index).Value))    'MANUALROUNDOFF  

            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GREMARKS.Index).Value) 'REMARKS

            ALPARAVAL.Add(0)    'footerdisc
            ALPARAVAL.Add(0)    'footerdscamt

            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GPCS.Index).Value)) 'TOTALQTY
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value)) 'TOTALMTRS
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GAMT.Index).Value)) 'TOTALAMT
            ALPARAVAL.Add(0) 'Val(LBLTOTALDISCAMT.Text.Trim))
            ALPARAVAL.Add(0) 'Val(LBLTOTALSPDISCAMT.Text.Trim))
            ALPARAVAL.Add(0) 'Val(LBLTOTALOTHERAMT.Text.Trim))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GSUBTOTAL.Index).Value)) 'TOTALTAXABLEAMT

            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GCGST.Index).Value))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GCGSTAMT.Index).Value))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GSGST.Index).Value)) 'SGSTPER.Text.Trim))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GSGSTAMT.Index).Value)) '(Val(TXTSGSTAMT.Text.Trim))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GIGST.Index).Value)) '(Val(TXTIGSTPER.Text.Trim))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GIGSTAMT.Index).Value)) '(Val(TXTIGSTAMT.Text.Trim))

            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GSUBTOTAL.Index).Value) + Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GCGSTAMT.Index).Value) + Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GSGSTAMT.Index).Value) + Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GIGSTAMT.Index).Value)) 'TXTTOTALWITHGST.Text.Trim))
            ALPARAVAL.Add(0) 'If CHKMANUALTCS.Checked = True Then ALPARAVAL.Add(1) Else
            ALPARAVAL.Add(0) 'If CHKTCS.Checked = True Then ALPARAVAL.Add(1) Else 
            ALPARAVAL.Add(0) 'Val(TXTTCSPER.Text.Trim))
            ALPARAVAL.Add(0) 'Val(TXTTCSAMT.Text.Trim))

            ALPARAVAL.Add(CurrencyToWord(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GGRANDTOTAL.Index).Value))) 'txtinwords.Text)


            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GAMT.Index).Value)) 'BILLAMT
            ALPARAVAL.Add(Val(0)) 'TXTTOTALTAXAMT.Text.Trim), "0.00"))
            ALPARAVAL.Add(Val(0)) 'TXTTOTALOTHERCHGSAMT.Text.Trim), "0.00"))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GCHARGES.Index).Value)) 'TXTCHARGES.Text.Trim), "0.00"))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GSUBTOTAL.Index).Value)) '(Format(Val(TXTSUBTOTAL.Text.Trim), "0.00"))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GROUNDOFF.Index).Value)) '(Format(Val(TXTROUNDOFF.Text.Trim), "0.00"))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GGRANDTOTAL.Index).Value)) '(Format(Val(TXTGRANDTOTAL.Text.Trim), "0.00"))

            ALPARAVAL.Add(Val(0)) 'TXTAMTPAID.Text.Trim))
            ALPARAVAL.Add(Val(0)) 'TXTEXTRAAMT.Text.Trim))
            ALPARAVAL.Add(Val(0)) 'TXTRETURN.Text.Trim))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GGRANDTOTAL.Index).Value)) 'TXTBAL.Text.Trim))
            ALPARAVAL.Add(Val(0)) 'TXTCHADTI.Text.Trim))

            ALPARAVAL.Add(TEMPCMPID)
            ALPARAVAL.Add(0)    'LOCATIONID
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(TEMPYEARID)
            ALPARAVAL.Add(0)    'TRANSFER

            ALPARAVAL.Add("")   'FORMTYPE

            ALPARAVAL.Add("1")  'GRIDSRNO
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(gitemname.Index).Value)    'ITEMNAME

            'GETHSNCODE FROM ITEMMASTER
            Dim OBJCMN As New ClsCommon
            Dim DTHSN As DataTable = OBJCMN.SEARCH("ISNULL(HSN_CODE,'') AS HSNCODE", "", "ITEMMASTER LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID", " AND ITEMMASTER.ITEM_NAME = '" & GRIDMAGICBOX.Rows(ROWNO).Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            ALPARAVAL.Add(DTHSN.Rows(0).Item("HSNCODE"))   'HSNCODE

            ALPARAVAL.Add("")   'QUALITY
            ALPARAVAL.Add("")   'DESIGN
            ALPARAVAL.Add("")   'SHADE

            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gQty.Index).Value))    'AQTY
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GFOLD.Index).Value))    'FOLDPER
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GDESC.Index).Value)   'DESC
            ALPARAVAL.Add("")   'LOTNO
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GBALENO.Index).Value)   'BALENO

            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GPCS.Index).Value))    'PCS
            ALPARAVAL.Add("Mtrs")    'UNIT
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GCUT.Index).Value))    'CUT
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value))    'MTRS

            ALPARAVAL.Add(0)    'WT
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GRATE.Index).Value))    'RATE
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GPER.Index).Value)    'PER
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GAMT.Index).Value))    'AMT

            ALPARAVAL.Add(0)    'DISCPER
            ALPARAVAL.Add(0)    'DISCAMT
            ALPARAVAL.Add(0)    'SPDISCPER
            ALPARAVAL.Add(0)    'SPDISCAMT
            ALPARAVAL.Add(0)    'OTHERAMT

            ALPARAVAL.Add(0)    'TAXABLEAMT
            ALPARAVAL.Add(0)    'CGSTPER (GRID)
            ALPARAVAL.Add(0)    'CGSTAMT (GRID)
            ALPARAVAL.Add(0)    'SGSTPER (GRID)
            ALPARAVAL.Add(0)    'SGSTAMT (GRID)
            ALPARAVAL.Add(0)    'IGSTPER (GRID)
            ALPARAVAL.Add(0)    'IGSTAMT (GRID)
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GAMT.Index).Value))    'GRIDTOTAL

            ALPARAVAL.Add("")   'GRNNO
            ALPARAVAL.Add("")   'GRNGRIDSRNO
            ALPARAVAL.Add("")   'GRNGRIDTYPE
            ALPARAVAL.Add(0)    'GRIDDONE
            ALPARAVAL.Add(0)    'OUTPCS
            ALPARAVAL.Add(0)    'OUTMTRS

            Dim CSRNO As String = ""
            Dim CCHGS As String = ""
            Dim CPER As String = ""
            Dim CAMT As String = ""
            Dim CTAXID As String = ""

            For Each DTROW As DataRow In DT_CHGSDETAILS.Rows
                If Val(DTROW("EMAINSRNO")) = Val(ROWNO + 1) Then
                    'If Val(DTROW("EMAINSRNO")) = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gsrno.Index).Value) Then
                    If CSRNO = "" Then
                        CSRNO = Val(DTROW("ESRNO"))
                        CCHGS = DTROW("ECHARGES")
                        CPER = Val(DTROW("EPER"))
                        CAMT = Val(DTROW("EAMT"))
                        CTAXID = Val(DTROW("ETAXID"))
                    Else
                        CSRNO = CSRNO & "|" & Val(DTROW("ESRNO"))
                        CCHGS = CCHGS & "|" & DTROW("ECHARGES")
                        CPER = CPER & "|" & Val(DTROW("EPER"))
                        CAMT = CAMT & "|" & Val(DTROW("EAMT"))
                        CTAXID = CTAXID & "|" & Val(DTROW("ETAXID"))
                    End If
                End If
            Next

            ALPARAVAL.Add(CSRNO)
            ALPARAVAL.Add(CCHGS)
            ALPARAVAL.Add(CPER)
            ALPARAVAL.Add(CAMT)
            ALPARAVAL.Add(CTAXID)

            ALPARAVAL.Add(ClientName)



            'GET DETAILS FROM PURCHASE ORDER
            If Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GPONO.Index).Value) > 0 Then
                Dim DTPO As DataTable = OBJCMN.SEARCH(" (CASE WHEN PO_ORDERON = 'PCS' THEN ROUND(PO_MTRS - PO_RECDQTY,2) ELSE ROUND(PO_QTY - PO_RECDQTY,2) END) AS BALPCS, (CASE WHEN PO_ORDERON = 'PCS' THEN ROUND(PO_MTRS - PO_RECDQTY,2) ELSE ROUND(PO_MTRS - PO_RECDMTRS,2) END) AS BALMTRS, PO_RATE AS RATE, PO_ORDERON AS ORDERON ", "", " ALLPURCHASEORDER_DESC ", " AND PO_NO = " & Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GPONO.Index).Value) & " AND PO_GRIDSRNO = " & Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GPOSRNO.Index).Value) & " AND TYPE = '" & GRIDMAGICBOX.Rows(ROWNO).Cells(GPOTYPE.Index).Value.ToString.Replace("AGENCYSALE", "PURCHASE") & "' AND PO_YEARID = " & TEMPYEARID)
                ALPARAVAL.Add("1")  'ORDERGRIDSRNO
                ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(gitemname.Index).Value)    'ORDERITEMNAME
                ALPARAVAL.Add("")   'ORDERDESIGN
                ALPARAVAL.Add("")   'ORDERCOLOR
                ALPARAVAL.Add(Val(DTPO.Rows(0).Item("BALPCS")))
                ALPARAVAL.Add(Val(DTPO.Rows(0).Item("BALMTRS")))
                ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GPONO.Index).Value))   'FROMNO
                ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GPOSRNO.Index).Value))   'ORDERFROMSRNO
                ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GPOTYPE.Index).Value.ToString.Replace("AGENCYSALE", "PURCHASE"))    'ORDERFROMTYPE
                ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GPCS.Index).Value))    'GRNPCS
                ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value)) 'ORDERGRNMTRS
                ALPARAVAL.Add(Val(DTPO.Rows(0).Item("RATE")))   'ORDERRATE
                ALPARAVAL.Add(DTPO.Rows(0).Item("ORDERON"))    'ORDERON

            Else
                ALPARAVAL.Add("")   'ORDERGRIDSRNO
                ALPARAVAL.Add("")   'ORDERITEMNAME
                ALPARAVAL.Add("")   'ORDERDESIGN
                ALPARAVAL.Add("")   'ORDERCOLOR
                ALPARAVAL.Add("")   'ORDEPCS
                ALPARAVAL.Add("")   'ORDERMTRS
                ALPARAVAL.Add("")   'ORDERFROMNO
                ALPARAVAL.Add("")   'ORDERFROMSRNO
                ALPARAVAL.Add("")   'ORDERFROMTYPE
                ALPARAVAL.Add("")   'ORDERGRNPCS
                ALPARAVAL.Add("")   'ORDERGRNMTRS
                ALPARAVAL.Add("")   'ORDERRATE
                ALPARAVAL.Add("")   'ORDERON
            End If



            ALPARAVAL.Add("")   'SPECIALREMARKS
            ALPARAVAL.Add(0)    'CD
            ALPARAVAL.Add(0)    'COSTCENTRE
            ALPARAVAL.Add("")   'SHIPTO
            ALPARAVAL.Add(0)    'HOLDFORINT

            ALPARAVAL.Add("")   'COMPLAINT
            ALPARAVAL.Add("")   'COMPLAINTBY
            ALPARAVAL.Add("")   'COMPLAINTDATE

            Dim OBJPI As New ClsPurchaseMaster()
            OBJPI.alParaval = ALPARAVAL
            Dim DT As DataTable = OBJPI.SAVE()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHGS_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDCHGS.CellDoubleClick
        Try
            If GRIDCHGS.CurrentRow.Index >= 0 And GRIDCHGS.Item(ECHARGES.Index, GRIDCHGS.CurrentRow.Index).Value <> Nothing Then
                GRIDCHGSDOUBLECLICK = True
                TXTCHGSSRNO.Text = GRIDCHGS.Item(ESRNO.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                CMBCHARGES.Text = GRIDCHGS.Item(ECHARGES.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSPER.Text = GRIDCHGS.Item(EPER.Index, GRIDCHGS.CurrentRow.Index).Value
                TXTCHGSAMT.Text = GRIDCHGS.Item(EAMT.Index, GRIDCHGS.CurrentRow.Index).Value
                TXTTAXID.Text = GRIDCHGS.Item(ETAXID.Index, GRIDCHGS.CurrentRow.Index).Value
                TEMPCHGSROW = GRIDCHGS.CurrentRow.Index
                CMBCHARGES.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSPER_Validating(sender As Object, e As CancelEventArgs) Handles TXTCHGSPER.Validating
        Try
            Dim dDebit As Decimal
            Dim bValid As Boolean = Decimal.TryParse(TXTCHGSPER.Text.Trim, dDebit)
            If bValid Then
                If Val(TXTCHGSPER.Text) = 0 Then TXTCHGSPER.Text = ""
                TXTCHGSPER.Text = Convert.ToDecimal(Val(TXTCHGSPER.Text))
                '' everything is good
                CALCHGS()
            ElseIf Val(TXTCHGSPER.Text.Trim) = 0 Then
                TXTCHGSAMT.ReadOnly = False
            Else
                MessageBox.Show("Invalid Number Entered")
                'e.Cancel = True
                TXTCHGSPER.Clear()
                TXTCHGSPER.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALCHGS()
        Try
            If Val(TXTCHGSPER.Text) <> 0 Then
                'before CALC CHECK HOW TO CALC CHARGES
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" (CASE WHEN ISNULL(ACC_CALC,'') = '' THEN 'GROSS' ELSE ACC_CALC END) AS CALC", "", "LEDGERS", " AND ACC_CMPNAME = '" & CMBCHARGES.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("CALC") = "GROSS" Then
                    TXTCHGSAMT.Text = Format((Val(TXTAMT.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
                ElseIf DT.Rows(0).Item("CALC") = "NETT" Then
                    TXTNETTAMT.Text = Val(TXTAMT.Text.Trim)
                    'FIRST CALC NETT THEN ADD CHARGES ON THAT NETT TOTAL
                    For Each ROW As DataGridViewRow In GRIDCHGS.Rows
                        If GRIDCHGSDOUBLECLICK = True And ROW.Index >= TEMPCHGSROW Then Exit For
                        TXTNETTAMT.Text = Format(Val(TXTNETTAMT.Text) + Val(ROW.Cells(EAMT.Index).Value), "0.00")
                    Next
                    TXTCHGSAMT.Text = Format((Val(TXTNETTAMT.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
                ElseIf DT.Rows(0).Item("CALC") = "QTY" Then
                    TXTCHGSAMT.Text = Format((Val(TXTPCS.Text) * Val(TXTCHGSPER.Text)), "0.00")
                ElseIf DT.Rows(0).Item("CALC") = "MTRS" Then
                    TXTCHGSAMT.Text = Format((Val(TXTMTRS.Text) * Val(TXTCHGSPER.Text)), "0.00")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQTY_Validated(sender As Object, e As EventArgs) Handles TXTQTY.Validated, TXTCUT.Validated, TXTFOLD.Validated, TXTPCS.Validated, TXTMTRS.Validated, TXTRATES.Validated, CMBPER.Validated, TXTROUNDOFF.Validated, TXTCGSTAMT.Validated, TXTSGSTAMT.Validated, TXTIGSTAMT.Validated
        CALC()
        TOTAL()
    End Sub

    Private Sub CHKMANUAL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKMANUAL.CheckedChanged
        Try
            If CHKMANUAL.Checked = True Then
                TXTCGSTAMT.ReadOnly = False
                TXTCGSTAMT.TabStop = True
                TXTCGSTAMT.BackColor = Color.LemonChiffon
                TXTSGSTAMT.ReadOnly = False
                TXTSGSTAMT.TabStop = True
                TXTSGSTAMT.BackColor = Color.LemonChiffon
                TXTIGSTAMT.ReadOnly = False
                TXTIGSTAMT.TabStop = True
                TXTIGSTAMT.BackColor = Color.LemonChiffon
            Else
                TXTCGSTAMT.ReadOnly = True
                TXTCGSTAMT.TabStop = False
                TXTCGSTAMT.BackColor = Color.Linen
                TXTSGSTAMT.ReadOnly = True
                TXTSGSTAMT.TabStop = False
                TXTSGSTAMT.BackColor = Color.Linen
                TXTIGSTAMT.ReadOnly = True
                TXTIGSTAMT.TabStop = False
                TXTIGSTAMT.BackColor = Color.Linen
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKMANUALROUND_CheckedChanged(sender As Object, e As EventArgs) Handles CHKMANUALROUND.CheckedChanged
        Try
            If CHKMANUALROUND.Checked = True Then
                TXTROUNDOFF.ReadOnly = False
                TXTROUNDOFF.TabStop = True
            Else
                TXTROUNDOFF.ReadOnly = True
                TXTROUNDOFF.TabStop = False
                CALC()
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLR_Validating(sender As Object, e As CancelEventArgs) Handles TXTLR.Validating
        Try
            If TXTLR.Text.Trim <> "" And CMBTRANS.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" AINVOICE_NO AS BILLNO", "", " AGENCYINVOICEMASTER INNER JOIN LEDGERS ON AGENCYINVOICEMASTER.AINVOICE_TRANSID = LEDGERS.Acc_id", " AND LEDGERS.ACC_CMPNAME = '" & CMBTRANS.Text.Trim & "' AND AGENCYINVOICEMASTER.AINVOICE_LRNO = '" & TXTLR.Text.Trim & "' AND AINVOICE_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    MsgBox("LR No Already Exists In Entry No " & DT.Rows(0).Item("BILLNO"))
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDMAGICBOX_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDMAGICBOX.CellDoubleClick
        If e.RowIndex >= 0 Then
            TEMPROW = e.RowIndex
            EDITROW()
        End If
    End Sub

    Sub EDITROW()
        Try
            If GRIDMAGICBOX.CurrentRow.Index >= 0 And GRIDMAGICBOX.Item(gsrno.Index, GRIDMAGICBOX.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TEMPROW = GRIDMAGICBOX.CurrentRow.Index

                txtsrno.Text = GRIDMAGICBOX.Item(gsrno.Index, TEMPROW).Value.ToString
                BILLDATE.Text = Format(Convert.ToDateTime(GRIDMAGICBOX.Item(GBILLDATE.Index, TEMPROW).Value).Date, "dd/MM/yyyy")
                ENTRYDATE.Text = Format(Convert.ToDateTime(GRIDMAGICBOX.Item(GDATE.Index, TEMPROW).Value).Date, "dd/MM/yyyy")
                CMBSELLERS.Text = GRIDMAGICBOX.Item(GSELLERS.Index, TEMPROW).Value.ToString
                CMBBUYERS.Text = GRIDMAGICBOX.Item(GBUYERS.Index, TEMPROW).Value.ToString
                TXTPARTYBILLNO.Text = GRIDMAGICBOX.Item(GNO.Index, TEMPROW).Value.ToString
                txtcrdays.Text = GRIDMAGICBOX.Item(GCRDAYS.Index, TEMPROW).Value.ToString
                TXTPONO.Text = GRIDMAGICBOX.Item(GPONO.Index, TEMPROW).Value.ToString
                TXTPOSRNO.Text = GRIDMAGICBOX.Item(GPOSRNO.Index, TEMPROW).Value.ToString
                TXTPOTYPE.Text = GRIDMAGICBOX.Item(GPOTYPE.Index, TEMPROW).Value.ToString
                cmbitemname.Text = GRIDMAGICBOX.Item(gitemname.Index, TEMPROW).Value.ToString
                TXTQTY.Text = GRIDMAGICBOX.Item(gQty.Index, TEMPROW).Value.ToString
                TXTFOLD.Text = GRIDMAGICBOX.Item(GFOLD.Index, TEMPROW).Value.ToString
                TXTDESC.Text = GRIDMAGICBOX.Item(GDESC.Index, TEMPROW).Value.ToString
                CMBTRANS.Text = GRIDMAGICBOX.Item(GTRANS.Index, TEMPROW).Value.ToString
                TXTLR.Text = GRIDMAGICBOX.Item(GLRNO.Index, TEMPROW).Value.ToString
                LRDATE.Text = Format(Convert.ToDateTime(GRIDMAGICBOX.Item(GLRDATE.Index, TEMPROW).Value).Date, "dd/MM/yyyy")
                TXTBALENO.Text = GRIDMAGICBOX.Item(GBALENO.Index, TEMPROW).Value.ToString
                TXTPCS.Text = GRIDMAGICBOX.Item(GPCS.Index, TEMPROW).Value.ToString
                TXTCUT.Text = GRIDMAGICBOX.Item(GCUT.Index, TEMPROW).Value.ToString
                TXTMTRS.Text = GRIDMAGICBOX.Item(GMTRS.Index, TEMPROW).Value.ToString
                TXTRATES.Text = GRIDMAGICBOX.Item(GRATE.Index, TEMPROW).Value.ToString
                CMBPER.Text = GRIDMAGICBOX.Item(GPER.Index, TEMPROW).Value.ToString
                TXTAMT.Text = GRIDMAGICBOX.Item(GAMT.Index, TEMPROW).Value.ToString
                TXTCHRGS.Text = GRIDMAGICBOX.Item(GCHARGES.Index, TEMPROW).Value.ToString
                TXTSUBTOTAL.Text = GRIDMAGICBOX.Item(GSUBTOTAL.Index, TEMPROW).Value.ToString
                TXTCGSTPER.Text = GRIDMAGICBOX.Item(GCGST.Index, TEMPROW).Value.ToString
                TXTCGSTAMT.Text = GRIDMAGICBOX.Item(GCGSTAMT.Index, TEMPROW).Value.ToString
                TXTSGSTPER.Text = GRIDMAGICBOX.Item(GSGST.Index, TEMPROW).Value.ToString
                TXTSGSTAMT.Text = GRIDMAGICBOX.Item(GSGSTAMT.Index, TEMPROW).Value.ToString
                TXTIGSTPER.Text = GRIDMAGICBOX.Item(GIGST.Index, TEMPROW).Value.ToString
                TXTIGSTAMT.Text = GRIDMAGICBOX.Item(GIGSTAMT.Index, TEMPROW).Value.ToString

                TXTROUNDOFF.Text = GRIDMAGICBOX.Item(GROUNDOFF.Index, TEMPROW).Value.ToString
                TXTGRANDTOTAL.Text = GRIDMAGICBOX.Item(GGRANDTOTAL.Index, TEMPROW).Value.ToString
                TXTCOMMPER.Text = GRIDMAGICBOX.Item(GCOMPER.Index, TEMPROW).Value.ToString
                CMBCOMM.Text = GRIDMAGICBOX.Item(GCOM.Index, TEMPROW).Value.ToString
                TXTREMARKS.Text = GRIDMAGICBOX.Item(GREMARKS.Index, TEMPROW).Value.ToString
                TXTHSN.Text = GRIDMAGICBOX.Item(GHSN.Index, TEMPROW).Value.ToString

                CHKMANUAL.Checked = Convert.ToBoolean(GRIDMAGICBOX.Item(GMANUALGST.Index, TEMPROW).Value)
                CHKMANUALROUND.Checked = Convert.ToBoolean(GRIDMAGICBOX.Item(GMANUALROUNDOFF.Index, TEMPROW).Value)
                CHKTDS.Checked = Convert.ToBoolean(GRIDMAGICBOX.Item(GTDS.Index, TEMPROW).Value)
                CMBTDS.Text = GRIDMAGICBOX.Item(GTDSNAME.Index, TEMPROW).Value.ToString
                TXTTDSPER.Text = Val(GRIDMAGICBOX.Item(GTDSPER.Index, TEMPROW).Value)
                TXTTDSAMT.Text = Val(GRIDMAGICBOX.Item(GTDSAMT.Index, TEMPROW).Value)


                txtsrno.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtcrdays_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcrdays.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub CMBSELLERS_Validated(sender As Object, e As EventArgs) Handles CMBSELLERS.Validated
        Try
            If CMBSELLERS.Text.Trim <> "" Then
                'GET SELLERSTATECODE
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS_1.ACC_CMPNAME,'') AS TRANSNAME, ISNULL(LEDGERS_2.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(REGISTER_NAME,'') AS REGISTERNAME, ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN, ISNULL(LEDGERS.ACC_EXMILLLESS,0) AS EXMILLLESS,  ISNULL(LEDGERS.ACC_DISC,0) AS DISCPER,  ISNULL(LEDGERS.ACC_CDPER,0) AS CDPER, isnull(LEDGERS.ACC_CRDAYS,0) AS CRDAYS, ISNULL(LEDGERS.ACC_MOBILE,'') AS MOBILENO, ISNULL(TERMMASTER.TERM_NAME,'') AS TERM, ISNULL(LEDGERS.ACC_AGENTCOMM,'') AS AGENTCOMM, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME, ISNULL(LEDGERS.ACC_OVERSEAS,0) AS OVERSEAS, ISNULL(LEDGERS.ACC_TCS,0) AS TCS, ISNULL(LEDGERS.ACC_PARTYTDS,0) AS PARTYTDS, ISNULL(LEDGERS.ACC_WARNING,'') AS WARNINGTEXT, ISNULL(LEDGERS.ACC_RD,0) AS RATEDIFF, ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN TERMMASTER ON LEDGERS.ACC_TERMID = TERM_ID  LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_DELIVERYATID = CITY_ID ", " and LEDGERS.acc_cmpname = '" & CMBSELLERS.Text.Trim & "' and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and LEDGERS.acc_YEARid = " & YearId)
                    SELLERSTATECODE = DT.Rows(0).Item("STATECODE")

                If CMBTRANS.Text = "" Then CMBTRANS.Text = DT.Rows(0).Item("TRANSNAME")


                'IN CHARGES GRID ADD DISCOUNT GIVEN / BROKERAGE
                'If (ClientName = "YASHVI" Or ClientName = "SBA" Or ClientName = "DEVEN" Or ClientName = "SOFTAS" Or ClientName = "BARKHA" Or ClientName = "AVIS" Or ClientName = "MOMAI" Or ClientName = "SHREEVALLABH") Then
                'INITIALLY IT WAS WITH RESPECT TO THE ABOVE MENTIONED CLIENT, THEN CHANGED WITH RESPECT TO SALEAUTODISCOUNT
                If SALEAUTODISCOUNT = True And EDIT = False Then

                    For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                        If DTROW.Cells(ECHARGES.Index).Value = "DISCOUNT GIVEN" Then GoTo LINE1
                    Next
                    If Val(DT.Rows(0).Item("DISCPER")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "DISCOUNT GIVEN", Val(DT.Rows(0).Item("DISCPER")) * -1, 0, 0)

                End If

LINE1:

                'GET TDSAPPLICABLE
                DT = OBJCMN.SEARCH("ISNULL(ACC_TDSPER,0) AS TDSPER, ISNULL(LEDGERS.ACC_TDSDEDUCTEDAC,'') AS TDSDEDUCTEDAC ", "", " LEDGERS INNER JOIN ACCOUNTSMASTER_TDS ON LEDGERS.ACC_ID = ACCOUNTSMASTER_TDS.ACC_ID", " and LEDGERS.acc_cmpname = '" & CMBSELLERS.Text.Trim & "' and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    If Val(DT.Rows(0).Item("TDSPER")) > 0 Then
                        CHKTDS.CheckState = CheckState.Checked
                        TXTTDSPER.Text = Val(DT.Rows(0).Item("TDSPER"))
                        CMBTDS.Text = DT.Rows(0).Item("TDSDEDUCTEDAC")
                        CALC()
                    End If
                End If

                GETHSNCODE()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPARTYBILLNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTPARTYBILLNO.Validating
        Try
            If TXTPARTYBILLNO.Text.Trim <> "" And CMBSELLERS.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" AINVOICE_NO AS BILLNO", "", " AGENCYINVOICEMASTER INNER JOIN LEDGERS ON AGENCYINVOICEMASTER.AINVOICE_PURLEDGERID = LEDGERS.Acc_id", " AND LEDGERS.ACC_CMPNAME = '" & CMBSELLERS.Text.Trim & "' AND AGENCYINVOICEMASTER.AINVOICE_PARTYPONO = '" & TXTPARTYBILLNO.Text.Trim & "' AND AINVOICE_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    MsgBox("Party Bill No Already Exists in Entry No " & DT.Rows(0).Item("BILLNO"))
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BILLDATE_Validated(sender As Object, e As EventArgs) Handles BILLDATE.Validated
        Try
            ENTRYDATE.Value = BILLDATE.Value
            LRDATE.Value = BILLDATE.Value
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