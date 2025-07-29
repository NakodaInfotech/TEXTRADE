
Imports System.ComponentModel
Imports BL

Public Class MagicBox
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Try
            For Each row As Windows.Forms.DataGridViewRow In GRIDMAGICBOX.Rows
                Dim alParaval As New ArrayList
                Dim NO As String = ""
                Dim GRIDSRNO As String = ""
                Dim ORDERDATE As String = ""
                Dim BUYERS As String = ""
                Dim SELLERS As String = ""
                Dim DISCOUNT As String = ""
                Dim DELPERIOD As String = ""
                Dim DUEDATE As String = ""
                Dim ORDERNO As String = ""
                Dim MERCHANT As String = ""
                Dim QUALITY As String = ""
                Dim DESIGN As String = ""
                Dim gridremarks As String = ""
                Dim COLOR As String = ""
                Dim PARTYPONO As String = ""
                Dim qty As String = ""
                Dim QTYUNIT As String = ""
                Dim CUT As String = ""
                Dim MTRS As String = ""
                Dim RATE As String = ""
                Dim PER As String = ""
                Dim AMOUNT As String = ""
                Dim RECDQTY As String = ""
                Dim RECDMTRS As String = ""
                Dim DONE As String = ""
                Dim SAMPLEDONE As String = ""
                Dim CLOSED As String = ""
                Dim REMARKS As String = ""
                Dim CRDAYS As String = ""

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

    Sub GENERATEPO(ROWNO As Integer, TEMPCMPID As Integer, TEMPYEARID As Integer)
        Try

            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GNO.Index).Value))
            ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDUEDATE.Index).Value).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GSELLERS.Index).Value)
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GCRDAYS.Index).Value))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GDELPERIOD.Index).Value))
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GORDERNO.Index).Value)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GDISCOUNT.Index).Value)
            ALPARAVAL.Add("")   'TRANSPORT
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GREMARKS.Index).Value)
            ALPARAVAL.Add("FINISH")
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gQty.Index).Value))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value))
            ALPARAVAL.Add(Format(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GRATE.Index).Value) * Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value), "0.00"))  'TOTALAMT


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
            Dim CUT As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gcut.Index).Value)
            Dim DESIGN As String = GRIDMAGICBOX.Rows(ROWNO).Cells(GDESIGN.Index).Value
            Dim COLOR As String = ""
            Dim PDESNO As String = ""
            Dim PSHADE As String = ""
            Dim qty As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gQty.Index).Value)
            Dim qtyunit As String = GRIDMAGICBOX.Rows(ROWNO).Cells(gqtyunit.Index).Value
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

    Sub GENERATESO(ROWNO As Integer, TEMPCMPID As Integer, TEMPYEARID As Integer)
        Try

            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GNO.Index).Value))
            ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDATE.Index).Value).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GBUYERS.Index).Value)
            ALPARAVAL.Add("")
            ALPARAVAL.Add("")

            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GORDERNO.Index).Value)
            ALPARAVAL.Add(Format(Convert.ToDateTime(GRIDMAGICBOX.Rows(ROWNO).Cells(GDUEDATE.Index).Value).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add("")
            ALPARAVAL.Add("")
            ALPARAVAL.Add("")
            ALPARAVAL.Add("")   'REFNO

            ALPARAVAL.Add("")
            ALPARAVAL.Add("")
            ALPARAVAL.Add("")
            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GBUYERS.Index).Value)
            ALPARAVAL.Add("")   'CURRENCY
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gQty.Index).Value))
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value))
            ALPARAVAL.Add(0)       '' *** TOTAL BALE INSTED OF TOTAL AMT.
            ALPARAVAL.Add(Format(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GRATE.Index).Value) * Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value), "0.00"))

            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GDISCOUNT.Index).Value))
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
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


            ALPARAVAL.Add("")   'INWORDS

            ALPARAVAL.Add(GRIDMAGICBOX.Rows(ROWNO).Cells(GREMARKS.Index).Value)
            ALPARAVAL.Add("")
            ALPARAVAL.Add("")


            ALPARAVAL.Add("")
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GDISCOUNT.Index).Value))
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GCRDAYS.Index).Value))
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add("")
            ALPARAVAL.Add("")
            ALPARAVAL.Add("")




            ALPARAVAL.Add(TEMPCMPID)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(TEMPYEARID)
            ALPARAVAL.Add(0)

            Dim GRIDSRNO As String = "1"
            Dim MERCHANT As String = GRIDMAGICBOX.Rows(ROWNO).Cells(gitemname.Index).Value
            Dim QUALITY As String = ""
            Dim DESIGN As String = GRIDMAGICBOX.Rows(ROWNO).Cells(GDESIGN.Index).Value
            Dim gridremarks As String = ""
            Dim COLOR As String = ""
            Dim PARTYPONO As String = ""
            Dim qty As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gQty.Index).Value)
            Dim QTYUNIT As String = GRIDMAGICBOX.Rows(ROWNO).Cells(gqtyunit.Index).Value
            Dim CUT As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(gcut.Index).Value)
            Dim MTRS As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value)
            Dim RATE As String = Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GRATE.Index).Value)
            Dim PER As String = "Mtrs"
            Dim AMOUNT As String = Format(Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GRATE.Index).Value) * Val(GRIDMAGICBOX.Rows(ROWNO).Cells(GMTRS.Index).Value), "0.00")
            Dim RECDQTY As String = 0
            Dim RECDMTRS As String = 0
            Dim DONE As String = 0
            Dim SAMPLEDONE As String = 0
            Dim CLOSED As String = 0

            ALPARAVAL.Add(GRIDSRNO)
            ALPARAVAL.Add(MERCHANT)
            ALPARAVAL.Add(QUALITY)
            ALPARAVAL.Add(DESIGN)
            ALPARAVAL.Add(gridremarks)
            ALPARAVAL.Add(COLOR)
            ALPARAVAL.Add(PARTYPONO)
            ALPARAVAL.Add(qty)
            ALPARAVAL.Add(QTYUNIT)
            ALPARAVAL.Add(CUT)
            ALPARAVAL.Add(MTRS)
            ALPARAVAL.Add(RATE)
            ALPARAVAL.Add(PER)
            ALPARAVAL.Add(AMOUNT)
            ALPARAVAL.Add(RECDQTY)
            ALPARAVAL.Add(RECDMTRS)
            ALPARAVAL.Add(DONE)
            ALPARAVAL.Add(SAMPLEDONE)
            ALPARAVAL.Add(CLOSED)

            ALPARAVAL.Add("")

            ALPARAVAL.Add("")
            ALPARAVAL.Add(1)    'VERIFIED
            ALPARAVAL.Add("PCS")    'ORDERON

            Dim OBJSO As New ClsSaleOrder()
            OBJSO.alParaval = ALPARAVAL
            Dim DT As DataTable = OBJSO.SAVE()

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
        duedate.Value = Now.Date
        TXTORDERNO.Clear()
        cmbitemname.Text = ""
        CMBDESIGN.Text = ""
        txtQTY.Clear()
        cmbqtyunit.Text = "Pcs"
        TXTCUT.Clear()
        TXTMTRS.Clear()
        TXTRATE.Clear()
        TXTREMARKS.Clear()
        GRIDMAGICBOX.RowCount = 0
        getmax_SO_no()
    End Sub

    Private Sub MagicBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            TXTNO.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub TXTDELPERIOD_Validated(sender As Object, e As EventArgs) Handles TXTDELPERIOD.Validated
        Try
            If ORDERDATE.Text <> "__/__/____" Then
                If Val(TXTDELPERIOD.Text.Trim) > 0 Then duedate.Value = Convert.ToDateTime(ORDERDATE.Text).Date.AddDays(Val(TXTDELPERIOD.Text.Trim))
            End If
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

    Private Sub CMBDESIGN_Enter(sender As Object, e As EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBDESIGN.KeyDown
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

    Private Sub CMBDESIGN_Validating(sender As Object, e As CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Validating(sender As Object, e As CancelEventArgs) Handles cmbqtyunit.Validating
        Try
            If cmbqtyunit.Text.Trim <> "" Then unitvalidate(cmbqtyunit, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Enter(sender As Object, e As EventArgs) Handles cmbqtyunit.Enter
        Try
            If cmbqtyunit.Text.Trim = "" Then fillunit(cmbqtyunit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLCMB()
        Try
            fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
            fillunit(cmbqtyunit)
            If CMBSELLERS.Text.Trim = "" Then FILLNAME(CMBSELLERS, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            If CMBBUYERS.Text.Trim = "" Then FILLNAME(CMBBUYERS, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillgrid()
        GRIDMAGICBOX.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDMAGICBOX.Rows.Add(Val(txtsrno.Text.Trim), Val(TXTNO.Text.Trim), ORDERDATE.Text.Trim, CMBBUYERS.Text.Trim, CMBSELLERS.Text.Trim, TXTCRDAYS.Text.Trim, TXTDISCOUNT.Text.Trim, TXTDELPERIOD.Text.Trim, duedate.Text.Trim, TXTORDERNO.Text.Trim, cmbitemname.Text.Trim, CMBDESIGN.Text.Trim, Format(Val(txtQTY.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), TXTREMARKS.Text.Trim)
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
            GRIDMAGICBOX.Item(GDUEDATE.Index, TEMPROW).Value = duedate.Text.Trim
            GRIDMAGICBOX.Item(GORDERNO.Index, TEMPROW).Value = TXTORDERNO.Text.Trim
            GRIDMAGICBOX.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
            GRIDMAGICBOX.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDMAGICBOX.Item(gQty.Index, TEMPROW).Value = Format(Val(txtQTY.Text.Trim), "0.00")
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
        txtQTY.Clear()
        TXTRATE.Clear()
        TXTREMARKS.Clear()
        ORDERDATE.Focus()
    End Sub

    Private Sub cmdEXIT_Click(sender As Object, e As EventArgs) Handles cmdEXIT.Click
        Me.Close()
    End Sub
    'Sub getmax_no()
    '    Dim DTTABLE As New DataTable
    '    DTTABLE = getmax("  isnull(max(ASO_no),0) + 1 ", "AGENCYSALEORDER", " AND ASO_cmpid=" & CmpId & " and ASO_locationid=" & Locationid & " and ASO_yearid=" & YearId)
    '    If DTTABLE.Rows.Count > 0 Then
    '        TXTNO.Text = DTTABLE.Rows(0).Item(0)
    '    End If
    'End Sub

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

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBBUYERS.Focus()
    End Sub

    Private Sub TXTCUT_Validated(sender As Object, e As EventArgs) Handles TXTCUT.Validated
        Try
            If TXTCUT.Text.Trim > 0 Then
                If txtQTY.Text.Trim > 0 Then
                    TXTMTRS.Text = Format(Val(TXTCUT.Text.Trim) * Val(txtQTY.Text.Trim), "0.00")
                End If
            Else
                TXTMTRS.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MagicBox_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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