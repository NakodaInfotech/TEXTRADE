
Imports BL
Imports WAProAPI
Imports System.IO.Compression
Imports HtmlAgilityPack

Public Class MDIMain

    Sub SCROLLERS()
        Try
            Dim OBJCMN As New ClsCommon
            Dim WHERECLAUSE As String = ""

            LBLCHECKIN.Left = Me.Width
            LBLCHECKIN.Top = StatusStrip1.Top + 2


            Dim REMINDER As String = "There are No Reminders"
            Dim DT As DataTable
            'DT = OBJCMN.SEARCH("*", "", " (SELECT LEDGERS.Acc_cmpname AS NAME, INVOICE_INITIALS AS INITIALS, INVOICE_DATE AS DATE , INVOICE_BALANCE AS BALANCE , INVOICE_DUEDATE AS DUEDATE, INVOICE_NO AS BILLNO FROM INVOICEMASTER INNER JOIN LEDGERS ON Acc_id = INVOICE_LEDGERID WHERE INVOICE_YEARID = " & YearId & " AND INVOICE_BALANCE > 0 UNION ALL SELECT LEDGERS.Acc_cmpname AS NAME, BILL_INITIALS AS INITIALS, BILL_DATE AS DATE , BILL_BALANCE AS BALANCE , BILL_DUEDATE AS DUEDATE, BILL_NO AS BILLNO FROM OPENINGBILL INNER JOIN LEDGERS ON Acc_id = BILL_LEDGERID WHERE BILL_YEARID = " & YearId & " AND BILL_TYPE ='SALE' AND BILL_BALANCE > 0) AS T ", WHERECLAUSE & " AND T.NAME = 'L.S.TEXTILES' ORDER BY T.NAME, T.DUEDATE, T.BILLNO")
            If UserName = "Admin" Then
                DT = OBJCMN.SEARCH(" REMINDER.R_DATE AS DATE, ISNULL(REMINDER.R_NARRATION, '') AS REMINDER  ", "", " REMINDER LEFT OUTER JOIN USERMASTER ON REMINDER.R_ASSIGNUSERID = USERMASTER.User_id", " AND REMINDER.R_DONE = 'FALSE' AND REMINDER.R_YEARID =  " & YearId & " ORDER BY REMINDER.R_DATE ")
            Else
                DT = OBJCMN.SEARCH(" REMINDER.R_DATE AS DATE, ISNULL(REMINDER.R_NARRATION, '') AS REMINDER  ", "", " REMINDER LEFT OUTER JOIN USERMASTER ON REMINDER.R_ASSIGNUSERID = USERMASTER.User_id", " AND REMINDER.R_DONE = 'FALSE' AND REMINDER.R_YEARID =  " & YearId & " AND  ISNULL(USERMASTER.User_Name, '') ='" & UserName & "'  ORDER BY REMINDER.R_DATE ")
            End If
            If DT.Rows.Count > 0 Then
                REMINDER = ""
                For Each ROW As DataRow In DT.Rows
                    REMINDER = REMINDER & ROW("Date") & " - " & ROW("REMINDER") & "                                                     "
                Next
            End If
            LBLCHECKIN.Text = REMINDER

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        LBLCHECKIN.Left = LBLCHECKIN.Left - 1
        LBLCHECKIN.Top = StatusStrip1.Top + 2

        If LBLCHECKIN.Left < 0 - LBLCHECKIN.Width Then
            SCROLLERS()
            LBLCHECKIN.Left = Me.Width
        End If
    End Sub

    Private Sub MDIMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Text = CmpName & " (" & AccFrom & " - " & AccTo & ")                     User - " & UserName
            GETCONN()

            If ClientName = "YASHVI" Or ClientName = "NAKODAINFOTECH" Or ClientName = "AVIS" Then
                Timer1.Enabled = True
                LBLCHECKIN.Visible = True
                Timer1.Interval = 15
            End If

            'GET COMPANY'S DATA FOR VALIDATIONS OF EWB AND GST
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("ISNULL(CMP_EWBUSER,'') AS EWBUSER, ISNULL(CMP_EWBPASS,'') AS EWBPASS, ISNULL(CMP_GSTIN,'') AS CMPGSTIN, ISNULL(CMP_ZIPCODE,'') AS CMPPINCODE, ISNULL(CITY_NAME,'') AS CITYNAME, CAST(STATE_NAME AS VARCHAR(50)) AS STATENAME, CAST(STATE_REMARK AS VARCHAR(50)) AS STATECODE, ISNULL(NOOFEWAYBILLS,0) AS EWAYCOUNTER, ISNULL(CMPMASTER.CMP_BANKNAME,'') AS BANKNAME, ISNULL(CMPMASTER.CMP_BANKACNO,'') AS ACCOUNTNO, ISNULL(CMPMASTER.CMP_IFSCCODE,'') AS IFSC, ISNULL(CMPMASTER.CMP_UPI,'') AS UPI, ISNULL(CMP_ADD1,'') AS CMPADDRESS, ISNULL(CMP_TEL,'') AS CMPTEL", "", " STATEMASTER INNER JOIN CMPMASTER ON STATE_ID = CMP_STATEID LEFT OUTER JOIN CITYMASTER ON CMP_FROMCITYID = CITY_ID LEFT OUTER JOIN EWAYCOUNTER ON CMP_ID = EWAYCOUNTER.CMPID ", " AND CMP_ID = " & CmpId)
            If DT.Rows.Count > 0 Then
                CMPEWBUSER = DT.Rows(0).Item("EWBUSER")
                CMPEWBPASS = DT.Rows(0).Item("EWBPASS")
                CMPGSTIN = DT.Rows(0).Item("CMPGSTIN")
                CMPPINCODE = DT.Rows(0).Item("CMPPINCODE")
                CMPCITYNAME = DT.Rows(0).Item("CITYNAME")
                CMPSTATENAME = DT.Rows(0).Item("STATENAME")
                CMPSTATECODE = DT.Rows(0).Item("STATECODE")
                CMPBANK = DT.Rows(0).Item("BANKNAME")
                CMPACCNO = DT.Rows(0).Item("ACCOUNTNO")
                CMPIFSC = DT.Rows(0).Item("IFSC")
                CMPUPI = DT.Rows(0).Item("UPI")
                CMPADDRESS = DT.Rows(0).Item("CMPADDRESS")
                CMPTEL = DT.Rows(0).Item("CMPTEL")

                DT = OBJCMN.search("ISNULL(SUM(NOOFEWAYBILLS),0) AS EWAYCOUNTER", "", " EWAYCOUNTER ", " AND CMPID = " & CmpId)
                CMPEWAYCOUNTER = Val(DT.Rows(0).Item("EWAYCOUNTER"))
                DT = OBJCMN.search("ISNULL(MAX(DATE), GETDATE()) AS EWAYEXPDATE", "", " EWAYCOUNTER ", " AND CMPID = " & CmpId)
                EWAYEXPDATE = Convert.ToDateTime(DT.Rows(0).Item("EWAYEXPDATE")).Date.AddYears(1)

                DT = OBJCMN.search("ISNULL(SUM(NOOFEINVOICE),0) AS EINVOICECOUNTER", "", " EINVOICECOUNTER ", " AND CMPID = " & CmpId)
                CMPEINVOICECOUNTER = Val(DT.Rows(0).Item("EINVOICECOUNTER"))
                DT = OBJCMN.search("ISNULL(MAX(DATE), GETDATE()) AS EINVOICEEXPDATE", "", " EINVOICECOUNTER ", " AND CMPID = " & CmpId)
                EINVOICEEXPDATE = Convert.ToDateTime(DT.Rows(0).Item("EINVOICEEXPDATE")).Date.AddYears(1)
            End If


            'GET USERGODOWN
            DT = OBJCMN.search("ISNULL(GODOWN_NAME,'') AS USERGODOWN", "", " USERGODOWNTAGGING INNER JOIN USERMASTER ON USERGODOWNTAGGING.GODOWN_USERID = USERMASTER.[User_id]	 INNER JOIN GODOWNMASTER ON USERGODOWNTAGGING.GODOWN_GODOWNID = GODOWNMASTER.GODOWN_id  ", " AND USER_NAME ='" & UserName & "' AND  USERGODOWNTAGGING.GODOWN_CMPID = " & CmpId)
            If DT.Rows.Count > 0 Then USERGODOWN = DT.Rows(0).Item("USERGODOWN")



            'BLOCKTRANSFER VARIABLES
            BLOCKMASTERTRANSFER = False
            BLOCKOTHERTRANSFER = False
            BLOCKACCDATATRANSFER = False
            BLOCKSTOCKSTRANSFER = False


            DT = OBJCMN.SEARCH("ISNULL(BLOCK_LEDGER,0) AS BLOCKMASTER, ISNULL(BLOCK_OTHER,0) AS BLOCKOTHERMASTER, ISNULL(BLOCK_DATA,0) AS BLOCKACCDATA, ISNULL(BLOCK_STOCK,0) AS BLOCKSTOCK", "", " BLOCKDATA ", " AND BLOCK_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                BLOCKMASTERTRANSFER = Convert.ToBoolean(DT.Rows(0).Item("BLOCKMASTER"))
                BLOCKOTHERTRANSFER = Convert.ToBoolean(DT.Rows(0).Item("BLOCKOTHERMASTER"))
                BLOCKACCDATATRANSFER = Convert.ToBoolean(DT.Rows(0).Item("BLOCKACCDATA"))
                BLOCKSTOCKSTRANSFER = Convert.ToBoolean(DT.Rows(0).Item("BLOCKSTOCK"))
            End If



            Dim DT1 As DataTable = OBJCMN.SEARCH("HOME , PO , GRN , MATREC , INHOUSECHECK, CHALLAN , JOBOUT , JOBIN , ISSUEPACKING , RECPACKING, PURINVOICE , SALEINVOICE , SHOWDASHBOARD, RECOUTSTANDING , PAYOUTSTANDING , PENDINGPO , PENDINGSO, STOCKDETAILS , SALEPURMONTHLY,SALEORDER,GRNCHECKING, ISNULL(CHALLANSO,0) AS CHALLANSO, ISNULL(BILLCHECKDISPUTE,0) AS BILLCHECKDISPUTE, ISNULL(LOCKPENDING,0) AS LOCKPENDING, ISNULL(CHANGEBARCODE,0) AS CHANGEBARCODE, ISNULL(STOCKADJUSTMENT,0) AS STOCKADJUSTMENT, ISNULL(ADJMTRSDIFF,0) AS ADJMTRSDIFF, ISNULL(ALLOWINVAFTEREINV,0) AS ALLOWINVAFTEREINV, ISNULL(ALLOWPOSOCLOSE,0) AS ALLOWPOSOCLOSE, ISNULL(ALLOWMERGEPARAMETER,0) AS ALLOWMERGEPARAMETER ,ISNULL(ALLOWLRRECD,0) AS ALLOWLRRECD ,ISNULL(ALLOWWAFORUSER,0) AS ALLOWWAFORUSER, ISNULL(SHOWREMINDERREPORT,0) AS SHOWREMINDERREPORT, ISNULL(SPECIALRIGHTS.ALLOWPOSOCHECKIN, 0) AS ALLOWPOSOCHECKIN ", "", "SPECIALRIGHTS inner join usermaster on userid = user_id", " AND USER_name= '" & UserName & "'")
            If DT1.Rows.Count > 0 Then
                HOME = DT1.Rows(0).Item("HOME")
                POTOOLVISIBLE = DT1.Rows(0).Item("PO")
                GRNTOOLVISIBLE = DT1.Rows(0).Item("GRN")
                MATRECTOOLVISIBLE = DT1.Rows(0).Item("MATREC")
                INHOUSECHKTOOLVISIBLE = DT1.Rows(0).Item("INHOUSECHECK")
                GDNTOOLVISIBLE = DT1.Rows(0).Item("CHALLAN")
                JOTOOLVISIBLE = DT1.Rows(0).Item("JOBOUT")
                JITOOLVISIBLE = DT1.Rows(0).Item("JOBIN")
                ISSPACKTOOLVISIBLE = DT1.Rows(0).Item("ISSUEPACKING")
                RECPACKTOOLVISIBLE = DT1.Rows(0).Item("RECPACKING")
                PURCHASETOOLVISIBLE = DT1.Rows(0).Item("PURINVOICE")
                SALETOOLVISIBLE = DT1.Rows(0).Item("SALEINVOICE")
                DASHBOARDTOOLVISIBLE = DT1.Rows(0).Item("SHOWDASHBOARD")
                RECOUTSTANDTOOLVISIBLE = DT1.Rows(0).Item("RECOUTSTANDING")
                PAYOUTSTANDTOOLVISIBLE = DT1.Rows(0).Item("PAYOUTSTANDING")
                PENDINGPOTOOLVISIBLE = DT1.Rows(0).Item("PENDINGPO")
                PENDINGSOTOOLVISIBLE = DT1.Rows(0).Item("PENDINGSO")
                STOCKTOOLVISIBLE = DT1.Rows(0).Item("STOCKDETAILS")
                MONTHLYTOOLVISIBLE = DT1.Rows(0).Item("SALEPURMONTHLY")
                SOTOOLVISIBLE = DT1.Rows(0).Item("SALEORDER")
                GRNCHECKTOOLVISIBLE = DT1.Rows(0).Item("GRNCHECKING")
                CHALLANWITHOUTSO = DT1.Rows(0).Item("CHALLANSO")
                ALLOWBILLCHECKDISPUTE = DT1.Rows(0).Item("BILLCHECKDISPUTE")
                ALLOWLOCKPENDING = DT1.Rows(0).Item("LOCKPENDING")
                ALLOWCHANGEBARCODE = DT1.Rows(0).Item("CHANGEBARCODE")
                ALLOWSTOCKADJUSTMENT = DT1.Rows(0).Item("STOCKADJUSTMENT")
                ALLOWADJMTRSDIFF = DT1.Rows(0).Item("ADJMTRSDIFF")
                ALLOWINVAFTEREINV = DT1.Rows(0).Item("ALLOWINVAFTEREINV")
                ALLOWPOSOCLOSE = DT1.Rows(0).Item("ALLOWPOSOCLOSE")
                ALLOWMERGEPARAMETER = DT1.Rows(0).Item("ALLOWMERGEPARAMETER")
                ALLOWLRRECD = DT1.Rows(0).Item("ALLOWLRRECD")
                ALLOWWAFORUSER = DT1.Rows(0).Item("ALLOWWAFORUSER")
                SHOWREMINDERREPORT = DT1.Rows(0).Item("SHOWREMINDERREPORT")
                ALLOWPOSOCHECKIN = DT1.Rows(0).Item("ALLOWPOSOCHECKIN")

            End If

            SENDWHATSAPP_MASTER.Enabled = ALLOWWHATSAPP
            If ALLOWWAFORUSER = True And ALLOWWHATSAPP = True Then WHATSAPPREG_MASTER.Enabled = True


            'CHECKING BLOCKDATE FOR BACK DATED ENTRIES
            DT = OBJCMN.search("*", "", "BLOCKDATE", " AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                SALEBLOCKDATE = DT.Rows(0).Item("SALEDATE")
                PURBLOCKDATE = DT.Rows(0).Item("PURDATE")
                CNBLOCKDATE = DT.Rows(0).Item("CNDATE")
                DNBLOCKDATE = DT.Rows(0).Item("DNDATE")
                EXPBLOCKDATE = DT.Rows(0).Item("EXPDATE")
                GREYRTBLOCKDATE = DT.Rows(0).Item("GREYRECTRANSDATE")
                GREYRPBLOCKDATE = DT.Rows(0).Item("GREYRECPROCESSDATE")
                GRNBLOCKDATE = DT.Rows(0).Item("GRNDATE")
                DYEINGRECBLOCKDATE = DT.Rows(0).Item("DYEINGRECDATE")
                ISSUEPACKBLOCKDATE = DT.Rows(0).Item("ISSUEPAC")
                RECPACKBLOCKDATE = DT.Rows(0).Item("RECPACKDATE")
                JOBLOCKDATE = DT.Rows(0).Item("JODATE")
                JIBLOCKDATE = DT.Rows(0).Item("JIDATE")
                STOCKADJBLOCKDATE = DT.Rows(0).Item("STOCKADJDATE")
                SALERETCHBLOCKDATE = DT.Rows(0).Item("SRCHALLANDATE")
                PURRETCHBLOCKDATE = DT.Rows(0).Item("PRCHALLANDATE")
                POBLOCKDATE = DT.Rows(0).Item("PODATE")
                SOBLOCKDATE = DT.Rows(0).Item("SODATE")
                STOREPOBLOCKDATE = DT.Rows(0).Item("STOREPODATE")


            Else
                SALEBLOCKDATE = AccFrom.Date
                PURBLOCKDATE = AccFrom.Date
                CNBLOCKDATE = AccFrom.Date
                CNBLOCKDATE = AccFrom.Date
                EXPBLOCKDATE = AccFrom.Date
                GREYRTBLOCKDATE = AccFrom.Date
                GREYRPBLOCKDATE = AccFrom.Date
                GRNBLOCKDATE = AccFrom.Date
                DYEINGRECBLOCKDATE = AccFrom.Date
                ISSUEPACKBLOCKDATE = AccFrom.Date
                RECPACKBLOCKDATE = AccFrom.Date
                JOBLOCKDATE = AccFrom.Date
                JIBLOCKDATE = AccFrom.Date
                STOCKADJBLOCKDATE = AccFrom.Date
                SALERETCHBLOCKDATE = AccFrom.Date
                PURRETCHBLOCKDATE = AccFrom.Date
                POBLOCKDATE = AccFrom.Date
                SOBLOCKDATE = AccFrom.Date
                STOREPOBLOCKDATE = AccFrom.Date

            End If


            SETENABILITY()
            HEADERVISIBLE()

            If ALLOWWHATSAPP = True Then
                Dim BASEURL As String = GETWHATSAPPBASEURL()
                If BASEURL <> "" Then
                    APIMethods.BaseURL = BASEURL
                Else
                    MsgBox("Whastapp Base URL Is Missing", MsgBoxStyle.Critical)
                End If
            End If

            If ClientName = "SST" Then
                GRNGREY_MASTER.Visible = False
            End If

            If ClientName = "SKF" Then
                YARNMODULE_MENU.Visible = True
                YARNISSUE.Visible = True
                WEFTISSUE_MASTER.Visible = True
                WEFTADD.Visible = True
                WEFTEDIT.Visible = True
                MILL_MASTER.Visible = True
                YARNQUALITY_MASTER.Visible = True
                BEAMISSUE_MASTER.Visible = True
                BEAMISSTOOL.Visible = True
                BEAMISSUEADD.Visible = True
                BEAMISSUEEDIT.Visible = True
                GreyReceived_MENU.Visible = True
                GREYRECDJOBBER_MASTER.Visible = True
                YARNRECDTOOLSTRIP.Visible = True
                GREYRECDTOOLSTRIP.Visible = True
                YARNWASTAGE.Visible = True
                YARNSTOCK_JOBBER.Visible = True
                YARNSTOCKJOBBER_TOOL.Visible = True
                JOBBERYARNSTOCKFILTER_MASTER.Visible = True
            End If

            If ClientName = "KARAN" Or ClientName = "TINUMINU" Then
                EMBPRODUCTION_MASTER.Visible = True
                EMBPROD_FILTER.Visible = True
                LOCKPENDINGJO_MENU.Visible = True
            End If

            If HIDEYARN = False Then
                YARNMODULE_MENU.Visible = True
                YARNRECD.Visible = True
                MILL_MASTER.Visible = True
                YARNQUALITY_MASTER.Visible = True
                YARNISSUE.Visible = True
                YARNISSUEJOBBER_MASTER.Visible = True
                TOOLYISSUE.Visible = True
                GreyReceived_MENU.Visible = True
                GREYRECDKNITTING_MASTER.Visible = True
                YARNRETURN_MASTER.Visible = True
                YARNWASTAGE.Visible = True
                YARNREPORTS_MASTER.Visible = True
                YARNRECDTOOLSTRIP.Visible = True
                GREYRECDTOOLSTRIP.Visible = True
            End If

            If HIDEAGENCY = False Then
                AGENCYMODULE_MENU.Visible = True
            End If

            If ClientName = "NAYRA" Or ClientName = "VSTRADERS" Then
                YARNQUALITY_MASTER.Visible = True
            End If

            If DISCONTINUECLIENT = True Then
                CMPADD.Enabled = False
                CMPEDIT.Enabled = False
                YEARADD.Enabled = False
            End If



            If HIDEACCOUNTS = True Or HIDEACCOUNTSEXCEPTINVOICE = True Then
                ACCOUNTS_MASTER.Enabled = False
                PAYADD.Enabled = False
                RECADD.Enabled = False
                JVADD.Enabled = False
                CREDITADD.Enabled = False
                DEBITADD.Enabled = False
                VOUCHERADD.Enabled = False

                PURINVADD.Enabled = False
                PURINVEDIT.Enabled = False

                PURRETADD.Enabled = False
                PURRETEDIT.Enabled = False

                PURCHASE_TOOL.Enabled = False
                ACCOUNTREPORT_MAIN.Enabled = False

                PROFORMAADD.Enabled = False
                PROFORMAEDIT.Enabled = False
                RECOUT_TOOL.Enabled = False
                PAYOUT_TOOL.Enabled = False
                REGISTER_MAIN.Enabled = False

                If HIDEACCOUNTSEXCEPTINVOICE = False Then
                    SALE_TOOL.Enabled = False
                    SALE_MASTER.Enabled = False
                    SALEADD.Enabled = False
                    SALERET_MASTER.Enabled = False
                End If
            End If


            If HIDEGREY = True Then
                GREYREC_MASTER.Enabled = False
                GREYREC_MASTER.Visible = False
                GREYISSUEPROCESS_MASTER.Enabled = False
                GREYISSUEPROCESS_MASTER.Visible = False
                GRNGREY_MASTER.Enabled = False
                GRNGREY_MASTER.Visible = False
                GRNCHECKING_MASTER.Enabled = False
                GRNCHECKING_MASTER.Visible = False
                GRNCHECKING_TOOL.Visible = False
                GRNCHECKING_TOOLSTRIP.Visible = False
                MATREC_MASTER.Enabled = False
                MATREC_MASTER.Visible = False
                MATREC_TOOL.Visible = False
                MATREC_TOOLSTRIP.Visible = False
                INHOUSECHECKING_MASTER.Enabled = False
                INHOUSECHECKING_MASTER.Visible = False
                INHOUSECHECK_TOOL.Visible = False
                INHOUSECHECK_TOOLSTRIP.Visible = False
            End If

            If HIDESTORES = True Then
                STORES_MASTER.Enabled = False
                STOREITEM_MASTER.Visible = False
                STOREREPORT_MENU.Enabled = False
            End If


            If HIDESAMPLEMODULE = True Then
                SAMPLEMODULE_MASTER.Enabled = False
                SAMPLEREPORT_MENU.Enabled = False
            End If

            If HIDEPAYROLL = True Then
                EMPLOYEE_MASTER.Enabled = False
                SALARYSLIP_MASTER.Enabled = False
                SALARYSLIPADD.Enabled = False
                SALARYSLIPEDIT.Enabled = False
                SALARYREPORT_MASTER.Enabled = False
            End If


            If HIDEPOSTER = True Then
                POSTERMANAGEMENT.Enabled = False
            Else
                POSTERMANAGEMENT.Enabled = True
            End If

            If HIDEDYEINGPROGRAM = True Then
                PROGRAM_MASTER.Enabled = False
                UPDATEPROGRAMDETAILS.Enabled = False
            End If

            If ALLOWPOSOCLOSE = False Then
                POCLOSE.Enabled = False
                SOCLOSE.Enabled = False
            End If

            If FETCHITEMWISEDESIGN = True Then MILL_MASTER.Visible = True

            If ALLOWMERGEPARAMETER = True Then MERGEDETAILS_MASTER.Enabled = True
            If ALLOWBARCODEPRINT = False Then BARCODERELATEDSTOCK_MENU.Enabled = False


            If SHOWREMINDERREPORT = True Then
                Dim OBJREMINDER As New ReminderReport
                OBJREMINDER.MdiParent = Me
                OBJREMINDER.Show()
            End If

            'Dim OBJPURORDER As New PurchaseOrder
            'Dim OBJSALORDER As New SaleOrder
            'If ALLOWPOSOCHECKIN = False Then
            '    OBJPURORDER.CHKVERIFY.Enabled = False
            '    OBJSALORDER.CHKVERIFY.Enabled = False

            'End If

        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Sub SETENABILITY()
        Try

            Dim objhp As New HomePage
            'Dim OBJCMN As New ClsCommon
            'Dim DT1 As DataTable = OBJCMN.search("HOME , PO, GRN, MATREC As matrec, INHOUSECHECK As inhouse, CHALLAN As gdn, JobOut As jobout, JobIn As jobin, ISSUEPACKING As isspack, RECPACKING As recpack, PURINVOICE As purchase, SALEINVOICE As sale, SHOWDASHBOARD As dashboard, RecOutstanding As recoutstanding, PayOutstanding As payoutstanding, PENDINGPO As pendingpo, PENDINGSO As pendingso, STOCKDETAILS As stock, SALEPURMONTHLY As monthly ", "", "SPECIALRIGHTS", " And USERID= " & Userid)
            'If DT1.Rows.Count > 0 Then
            '    HOME = DT1.Rows(0).Item(0)

            'End If

            objhp.MdiParent = Me

            'OPEN THESE RIGHTS FOR ALL USERS AS PER YASHVI
            If ClientName = "YASHVI" Or ClientName = "RMANILAL" Then
                DATATRANSFER_MASTER.Enabled = True
                STOCKTRANSFER_MASTER.Enabled = True
                RECODATA_MASTER.Enabled = True
            End If


            If UserName = "Admin" Then
                CMP_MASTER.Enabled = True
                YEAR_MASTER.Enabled = True
                ADMIN_MASTER.Enabled = True
                MERGELEDGER.Enabled = True
                DATATRANSFER_MASTER.Enabled = True
                STOCKTRANSFER_MASTER.Enabled = True
                RECODATA_MASTER.Enabled = True
                BLOCKDETAILS_MASTER.Enabled = True
                USERTRANSFER.Enabled = True
                SPECIALRIGHTS_MASTER.Enabled = True
                RATETYPE_MASTER.Enabled = True
                BLOCKDATEMENU.Enabled = True
                USERGODOWN_MASTER.Enabled = True
                LOCKACCYEAR_MASTER.Enabled = True
                REPLACELOTNO_MASTER.Enabled = True
                LOGS_MASTER.Enabled = True
                RENUMBERING_MASTER.Enabled = True

                SAMPLETYPE_MASTER.Enabled = True
                DYEDTYPE_MASTER.Enabled = True
                COSTCENTER_MASTER.Enabled = True
                AUTODESIGNCREATION_MASTER.Enabled = True
            Else
                'ONLY TO CHANGE PASSWORD
                ADMIN_MASTER.Enabled = True
                USERADD.Enabled = False
                USEREDIT.Enabled = True
            End If


            If ALLOWLOCKPENDING = True Or UserName = "Admin" Then
                'LOCKPENDINGENTRIES_MENU.Enabled = True
                SHRINKAGE_MASTER.Enabled = True
                UPDATEPENDINGENTRIES_MENU.Enabled = True
            End If


            If ALLOWCHANGEBARCODE = True Or UserName = "Admin" Then
                CHANGEBARCODE_MASTER.Enabled = True
            End If

            If ALLOWSTOCKADJUSTMENT = True Then
                STOCKADJUSTMENT_MASTER.Enabled = True
            End If


            If ClientName = "MSANCHITKUMAR" Or ClientName = "SVS" Then
                PURCHASE_TOOL.Visible = False
                PURCHASE_TOOLSTRIP.Visible = False
                'WE HAVE TO GIVE THEN PERMISSION FOR INVOICE PRINTING 
                'SALE_TOOL.Visible = False
                'SALE_TOOLSTRIP.Visible = False
            End If

            If ClientName = "SAFFRON" Then
                SCHEDULE_MASTER.Visible = True
            End If

            If ClientName = "VINTAGEINDIA" Then
                BlanketManagementToolStripMenuItem.Enabled = True
            End If

            If ClientName <> "MOHAN" And ClientName <> "VAISHALI" And ClientName <> "MARKIN" And ClientName <> "KARAN" And ClientName <> "TINUMINU" Then
                YARNRECD.Visible = False
                YARNRECDTOOLSTRIP.Visible = False
                MACHINE_MASTER.Visible = False
                ToolStripSeparator108.Visible = False
            End If

            If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "KDFAB" Or ClientName = "DRDRAPES" Or ClientName = "SONAL" Or ClientName = "KCRAYON" Or ClientName = "MOMAI" Or ClientName = "KARAN" Or ClientName = "RADHA" Or ClientName = "VINTAGEINDIA" Then
                PRICELIST_MASTER.Visible = True
                ITEMPRICELIST_MASTER.Visible = False
            End If

            If ClientName = "SVS" Then DESIGNREPLACEMENT.Visible = True

            For Each DTROW As DataRow In USERRIGHTS.Rows

                'MASTERS
                If DTROW(0).ToString = "GROUP MASTER" Then
                    If DTROW(1).ToString = True Then
                        GROUP_MASTER.Enabled = True
                        GROUPADD.Enabled = True
                    Else
                        GROUPADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        GROUP_MASTER.Enabled = True
                        GROUPEDIT.Enabled = True
                    Else
                        GROUPEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "ACCOUNTS MASTER" Then
                    If DTROW(1).ToString = True Then
                        ACC_MASTER.Enabled = True
                        ACCADD.Enabled = True
                        NARRATION_MASTER.Enabled = True
                        PARTYBANK_MASTER.Enabled = True
                        NARRATIONADD.Enabled = True
                        PARTYBANKADD.Enabled = True
                        PRICELIST_MASTER.Enabled = True
                        ITEMPRICELIST_MASTER.Enabled = True
                        DESIGNPROCESSWISERATECHART.Enabled = True
                        SALESMAN_MASTER.Enabled = True
                        SALESMANADD.Enabled = True
                        CONTRACTOR_MASTER.Enabled = True
                        CONTRACTORADD.Enabled = True
                        CURRENCY_MASTER.Enabled = True
                        CURRENCYADD.Enabled = True
                        INCENTIVE_MASTER.Enabled = True
                        INCENTIVEADD.Enabled = True
                    Else
                        ACCADD.Enabled = False
                        NARRATIONADD.Enabled = False
                        PARTYBANKADD.Enabled = False
                        SALESMANEDIT.Enabled = False
                        CONTRACTORADD.Enabled = False
                        CURRENCYADD.Enabled = False
                        INCENTIVEADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        ACC_MASTER.Enabled = True
                        ACCEDIT.Enabled = True
                        NARRATION_MASTER.Enabled = True
                        PARTYBANK_MASTER.Enabled = True
                        NARRATIONEDIT.Enabled = True
                        PARTYBANKEDIT.Enabled = True
                        PRICELIST_MASTER.Enabled = True
                        DESIGNPROCESSWISERATECHART.Enabled = True
                        SALESMAN_MASTER.Enabled = True
                        SALESMANEDIT.Enabled = True
                        CONTRACTOR_MASTER.Enabled = True
                        CONTRACTOREDIT.Enabled = True
                        CURRENCY_MASTER.Enabled = True
                        CURRENCYEDIT.Enabled = True
                        INCENTIVEREPORT_MENU.Enabled = True
                        INCENTIVE_MASTER.Enabled = True
                        INCENTIVEEDIT.Enabled = True
                    Else
                        ACCEDIT.Enabled = False
                        NARRATIONEDIT.Enabled = False
                        PARTYBANKEDIT.Enabled = False
                        SALESMANEDIT.Enabled = False
                        CONTRACTOREDIT.Enabled = False
                        CURRENCYEDIT.Enabled = False
                        INCENTIVEEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "EMPLOYEE MASTER" Then
                    If DTROW(1).ToString = True Then
                        EMPLOYEE_MASTER.Enabled = True
                        EMPLOYEEADD.Enabled = True
                    Else
                        EMPLOYEEADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        EMPLOYEE_MASTER.Enabled = True
                        EMPLOYEEEDIT.Enabled = True
                    Else
                        EMPLOYEEEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "HSNMASTER" Then
                    If DTROW(1).ToString = True Then
                        HSN_MASTER.Enabled = True
                        HSNADD.Enabled = True
                    Else
                        HSNADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        HSN_MASTER.Enabled = True
                        HSNEDIT.Enabled = True
                    Else
                        HSNEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "REGISTER MASTER" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        REG_MASTER.Enabled = True
                    End If


                ElseIf DTROW(0).ToString = "ITEM MASTER" Then
                    If DTROW(1).ToString = True Then
                        MATERIAL_MASTER.Enabled = True
                        CATEGORY_MASTER.Enabled = True
                        QUALITY_MASTER.Enabled = True
                        COLOR_MASTER.Enabled = True
                        PROCESS_MASTER.Enabled = True
                        ITEM_MASTER.Enabled = True
                        PIECETYPE_MASTER.Enabled = True
                        RACK_MASTER.Enabled = True
                        SHELF_MASTER.Enabled = True
                        MILL_MASTER.Enabled = True
                        YARNQUALITY_MASTER.Enabled = True
                        MACHINE_MASTER.Enabled = True
                        REORDERLEVEL_MASTER.Enabled = True

                        MATERIALADD.Enabled = True
                        CATEGORYADD.Enabled = True
                        QUALITYADD.Enabled = True
                        COLORADD.Enabled = True
                        PROCESSADD.Enabled = True
                        ITEMADD.Enabled = True
                        PIECETYPEADD.Enabled = True
                        RACKADD.Enabled = True
                        SHELFADD.Enabled = True
                        YARNADD.Enabled = True
                        MILLADD.Enabled = True
                        MACHINEADD.Enabled = True
                        ITEMPACKINGCONFIG_MASTER.Enabled = True
                    Else
                        MATERIALADD.Enabled = False
                        CATEGORYADD.Enabled = False
                        QUALITYADD.Enabled = False
                        COLORADD.Enabled = False
                        PROCESSADD.Enabled = False
                        ITEMADD.Enabled = False
                        PIECETYPEADD.Enabled = False
                        RACKADD.Enabled = False
                        SHELFADD.Enabled = False
                        YARNADD.Enabled = False
                        MILLADD.Enabled = False
                        MACHINEADD.Enabled = False
                        ITEMPACKINGCONFIG_MASTER.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        MATERIAL_MASTER.Enabled = True
                        CATEGORY_MASTER.Enabled = True
                        QUALITY_MASTER.Enabled = True
                        COLOR_MASTER.Enabled = True
                        PROCESS_MASTER.Enabled = True
                        ITEM_MASTER.Enabled = True
                        PIECETYPE_MASTER.Enabled = True
                        RACK_MASTER.Enabled = True
                        SHELF_MASTER.Enabled = True
                        YARNQUALITY_MASTER.Enabled = True
                        MILL_MASTER.Enabled = True
                        MACHINE_MASTER.Enabled = True
                        REORDERLEVEL_MASTER.Enabled = True

                        MATERIALEDIT.Enabled = True
                        CATEGORYEDIT.Enabled = True
                        QUALITYEDIT.Enabled = True
                        COLOREDIT.Enabled = True
                        PROCESSEDIT.Enabled = True
                        ITEMEDIT.Enabled = True
                        PIECETYPEEDIT.Enabled = True
                        RACKEDIT.Enabled = True
                        SHELFEDIT.Enabled = True
                        YARNEDIT.Enabled = True
                        MILLEDIT.Enabled = True
                        MACHINEEDIT.Enabled = True
                        ITEMPACKINGCONFIG_MASTER.Enabled = True
                    Else
                        MATERIALEDIT.Enabled = False
                        CATEGORYEDIT.Enabled = False
                        QUALITYEDIT.Enabled = False
                        COLOREDIT.Enabled = False
                        PROCESSEDIT.Enabled = False
                        ITEMEDIT.Enabled = False
                        PIECETYPEEDIT.Enabled = False
                        NARRATIONEDIT.Enabled = False
                        PARTYBANKEDIT.Enabled = False
                        RACKEDIT.Enabled = False
                        SHELFEDIT.Enabled = False
                        YARNEDIT.Enabled = False
                        MILLEDIT.Enabled = False
                        MACHINEEDIT.Enabled = False
                        ITEMPACKINGCONFIG_MASTER.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "DESIGN MASTER" Then
                    If DTROW(1).ToString = True Then
                        DESIGN_MASTER.Enabled = True
                        DESIGNADD.Enabled = True
                        BLANKET_MASTER.Enabled = True
                        BLANKETADD.Enabled = True
                        DESIGNER_MASTER.Enabled = True
                        DESIGNERADD.Enabled = True
                    Else
                        DESIGNADD.Enabled = False
                        BLANKETADD.Enabled = False
                        DESIGNERADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        DESIGN_MASTER.Enabled = True
                        DESIGNEDIT.Enabled = True
                        BLANKET_MASTER.Enabled = True
                        BLANKETEDIT.Enabled = True
                        DESIGNER_MASTER.Enabled = True
                        DESIGNEREDIT.Enabled = True
                    Else
                        DESIGNEDIT.Enabled = False
                        BLANKETEDIT.Enabled = False
                        DESIGNEREDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "DEPARTMENT MASTER" Then
                    If DTROW(1).ToString = True Then
                        DEPARTMENT_MASTER.Enabled = True
                        DEPARTMENTADD.Enabled = True
                    Else
                        DEPARTMENTADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        DEPARTMENT_MASTER.Enabled = True
                        DEPARTMENTEDIT.Enabled = True
                    Else
                        DEPARTMENTEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "UNIT MASTER" Then
                    If DTROW(1).ToString = True Then
                        UNIT_MASTER.Enabled = True
                        UNITADD.Enabled = True
                    Else
                        UNITADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        UNIT_MASTER.Enabled = True
                        UNITEDIT.Enabled = True
                    Else
                        UNITEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "STORE ITEM MASTER" Then
                    If DTROW(1).ToString = True Then
                        STOREITEM_MASTER.Enabled = True
                        STOREITEMADD.Enabled = True
                    Else
                        STOREITEMADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        STOREITEM_MASTER.Enabled = True
                        STOREITEMEDIT.Enabled = True
                    Else
                        STOREITEMEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "OPENING" Then
                    If DTROW(1).ToString = True Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        OPENINGBILL_MASTER.Enabled = True
                        OPENST_MASTER.Enabled = True
                        OPENINGBANKRECO_MASTER.Enabled = True
                        INHOUSEST.Enabled = True
                        ATPARTYST.Enabled = True
                        OPENINGSTOCKVALUE.Enabled = True
                        OPENINGBALANCE.Enabled = True
                        PROVISIONALBS_MASTER.Enabled = True
                    End If

                ElseIf DTROW(0).ToString = "LOCATION MASTER" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        LOC_MASTER.Enabled = True
                    End If


                ElseIf DTROW(0).ToString = "TAX MASTER" Then
                    If DTROW(1).ToString = True Then
                        TAX_MASTER.Enabled = True
                        TAXADD.Enabled = True
                    Else
                        TAXADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        TAX_MASTER.Enabled = True
                        TAXEDIT.Enabled = True
                    Else
                        TAXEDIT.Enabled = False
                    End If

                    'PURCHASE
                ElseIf DTROW(0).ToString = "PURCHASE ORDER" Then
                    If DTROW(1).ToString = True Then
                        PO_MASTER.Enabled = True
                        PO_TOOL.Enabled = True
                        POCLOSE.Enabled = True
                        POADD.Enabled = True
                        OPPO_MASTER.Enabled = True
                        OPPOADD.Enabled = True
                        YARNPO_MASTER.Enabled = True
                        YARNPOADD.Enabled = True
                        YARNPOCLOSE.Enabled = True
                    Else
                        POADD.Enabled = False
                        OPPOADD.Enabled = False
                        YARNPOADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PO_MASTER.Enabled = True
                        PO_TOOL.Enabled = True
                        POCLOSE.Enabled = True
                        POEDIT.Enabled = True
                        OPPO_MASTER.Enabled = True
                        OPPOEDIT.Enabled = True
                        YARNPO_MASTER.Enabled = True
                        YARNPOEDIT.Enabled = True
                        YARNPOCLOSE.Enabled = True
                    Else
                        POEDIT.Enabled = False
                        OPPOEDIT.Enabled = False
                        YARNPOEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "STORES" Then
                    If DTROW(1).ToString = True Then
                        OPENING_STORESTOCK.Enabled = True
                        STORESPO_MASTER.Enabled = True
                        STORESPOCLOSE.Enabled = True
                        STOREINWARD_MASTER.Enabled = True
                        STORECONSUMPTION_MASTER.Enabled = True
                        TRANSFERSTORES_MASTER.Enabled = True
                        STORESPOADD.Enabled = True
                        STOREINWARDADD.Enabled = True
                        STORECONSUMPTIONADD.Enabled = True
                        TRANSFERSTORESADD.Enabled = True
                        STORESREORDERLEVEL_MASTER.Enabled = True
                    Else
                        STORESPOADD.Enabled = False
                        STOREINWARDADD.Enabled = False
                        STORECONSUMPTIONADD.Enabled = False
                        TRANSFERSTORESADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        OPENING_STORESTOCK.Enabled = True
                        STORESPO_MASTER.Enabled = True
                        STORESPOCLOSE.Enabled = True
                        STOREINWARD_MASTER.Enabled = True
                        STORECONSUMPTION_MASTER.Enabled = True
                        TRANSFERSTORES_MASTER.Enabled = True
                        STORESPOEDIT.Enabled = True
                        STOREINWARDEDIT.Enabled = True
                        STORECONSUMPTIONEDIT.Enabled = True
                        TRANSFERSTORESEDIT.Enabled = True
                        STORESREORDERLEVEL_MASTER.Enabled = True
                    Else
                        STORESPOEDIT.Enabled = False
                        STOREINWARDEDIT.Enabled = False
                        STORECONSUMPTIONEDIT.Enabled = False
                        TRANSFERSTORESEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "POSTER ORDER" Then
                    If DTROW(1).ToString = True Then
                        POSTERORDER_MASTER.Enabled = True
                        POSTERORDERADD.Enabled = True
                    Else
                        POSTERORDERADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        POSTERORDER_MASTER.Enabled = True
                        POSTERORDEREDIT.Enabled = True
                    Else
                        POSTERORDEREDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "GRN" Then
                    If DTROW(1).ToString = True Then
                        GRN_MASTER.Enabled = True
                        GRNGREY_MASTER.Enabled = True
                        GRNGREY_TOOL.Enabled = True
                        MATREC_MASTER.Enabled = True
                        MATREC_TOOL.Enabled = True
                        GRNADD.Enabled = True
                        GRNGREYADD.Enabled = True
                        MATRECADD.Enabled = True
                        PROGRAM_MASTER.Enabled = True
                        OPPROGRAM_MASTER.Enabled = True
                        PROGRAMADD.Enabled = True
                        OPPROGRAMADD.Enabled = True
                        DYEINGPRICELIST_MASTER.Enabled = True
                        DYEINGPRICELISTADD.Enabled = True
                        SHRINKAGE_MASTER.Enabled = True
                        GREYREC_MASTER.Enabled = True
                        GREYISSUEPROCESS_MASTER.Enabled = True

                        GREYRECDJOBBER_MASTER.Enabled = True
                        GREYRECDKNITTING_MASTER.Enabled = True
                        GREYRECDPRODUCTION_MASTER.Enabled = True
                        GREYRECDJOBBERADD.Enabled = True
                        GREYRECDKNITTINGADD.Enabled = True
                        GREYRECDPRODUCTIONADD.Enabled = True
                        GREYRECADD.Enabled = True
                        GREYISSUEPROCESSADD.Enabled = True
                    Else
                        GRNADD.Enabled = False
                        GRNGREYADD.Enabled = False
                        MATRECADD.Enabled = False
                        PROGRAMADD.Enabled = False
                        OPPROGRAMADD.Enabled = False
                        GREYRECDJOBBERADD.Enabled = False
                        GREYRECDKNITTINGADD.Enabled = False
                        GREYRECDPRODUCTIONADD.Enabled = False
                        GREYRECADD.Enabled = False
                        GREYISSUEPROCESSADD.Enabled = False
                        DYEINGPRICELISTADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        GRN_MASTER.Enabled = True
                        GRNGREY_MASTER.Enabled = True
                        GRNGREY_TOOL.Enabled = True
                        MATREC_MASTER.Enabled = True
                        MATREC_TOOL.Enabled = True
                        GRNEDIT.Enabled = True
                        GRNGREYEDIT.Enabled = True
                        MATRECEDIT.Enabled = True
                        PROGRAM_MASTER.Enabled = True
                        OPPROGRAM_MASTER.Enabled = True
                        PROGRAMEDIT.Enabled = True
                        OPPROGRAMEDIT.Enabled = True
                        SHRINKAGE_MASTER.Enabled = True
                        GREYREC_MASTER.Enabled = True
                        GREYISSUEPROCESS_MASTER.Enabled = True
                        DYEINGPRICELIST_MASTER.Enabled = True
                        DYEINGPRICELISTEDIT.Enabled = True
                        GREYRECDJOBBER_MASTER.Enabled = True
                        GREYRECDKNITTING_MASTER.Enabled = True
                        GREYRECDPRODUCTION_MASTER.Enabled = True
                        GREYRECDJOBBEREDIT.Enabled = True
                        GREYRECDKNITTINGEDIT.Enabled = True
                        GREYRECDPRODUCTIONEDIT.Enabled = True
                        GREYRECEDIT.Enabled = True
                        GREYISSUEPROCESSEDIT.Enabled = True
                    Else
                        GRNEDIT.Enabled = False
                        GRNGREYEDIT.Enabled = False
                        MATRECEDIT.Enabled = False
                        PROGRAMEDIT.Enabled = False
                        OPPROGRAMEDIT.Enabled = False
                        GREYRECDJOBBEREDIT.Enabled = False
                        GREYRECDKNITTINGEDIT.Enabled = False
                        GREYRECDPRODUCTIONEDIT.Enabled = False
                        GREYRECEDIT.Enabled = False
                        GREYISSUEPROCESSEDIT.Enabled = False
                        DYEINGPRICELISTEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "GRN CHECKING" Then
                    If DTROW(1).ToString = True Then
                        GRNCHECKING_MASTER.Enabled = True
                        GRNCHECKING_TOOL.Enabled = True
                        GRNCHECKINGADD.Enabled = True
                        INHOUSECHECKING_MASTER.Enabled = True
                        INHOUSECHECK_TOOL.Enabled = True
                        INHOUSECHECKINGADD.Enabled = True
                    Else
                        GRNCHECKINGADD.Enabled = False
                        INHOUSECHECKINGADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        GRNCHECKING_MASTER.Enabled = True
                        GRNCHECKING_TOOL.Enabled = True
                        GRNCHECKINGEDIT.Enabled = True
                        INHOUSECHECK_TOOL.Enabled = True
                        INHOUSECHECKING_MASTER.Enabled = True
                        INHOUSECHECKINGEDIT.Enabled = True
                    Else
                        GRNCHECKINGEDIT.Enabled = False
                        INHOUSECHECKINGEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "PURCHASE INVOICE" Then
                    If DTROW(1).ToString = True Then
                        PURINV_MASTER.Enabled = True
                        PURCHASE_TOOL.Enabled = True
                        PURINVADD.Enabled = True
                    Else
                        PURINVADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PURINV_MASTER.Enabled = True
                        PURCHASE_TOOL.Enabled = True
                        PURINVEDIT.Enabled = True
                    Else
                        PURINVEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "PURCHASE RETURN" Then
                    If DTROW(1).ToString = True Then
                        PURRETCHALLAN_MASTER.Enabled = True
                        PURRETCHALLANADD.Enabled = True
                        PURRET_MASTER.Enabled = True
                        PURRETADD.Enabled = True
                    Else
                        PURRETCHALLANADD.Enabled = False
                        PURRETADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PURRETCHALLAN_MASTER.Enabled = True
                        PURRETCHALLANEDIT.Enabled = True
                        PURRET_MASTER.Enabled = True
                        PURRETEDIT.Enabled = True
                    Else
                        PURRETEDIT.Enabled = False
                        PURRETCHALLANEDIT.Enabled = False
                    End If


                    'SALE
                ElseIf DTROW(0).ToString = "SALE ORDER" Then
                    If DTROW(1).ToString = True Then
                        SO_TOOL.Enabled = True
                        SO_MASTER.Enabled = True
                        SOADD.Enabled = True
                        SCHEDULE_MASTER.Enabled = True
                        SCHEDULEADD.Enabled = True
                        OPSO_MASTER.Enabled = True
                        OPSOADD.Enabled = True
                        SOCLOSE.Enabled = True
                        REASON_MASTER.Enabled = True
                        REASONADD.Enabled = True
                        YARNSO_MASTER.Enabled = True
                        YARNSOADD.Enabled = True
                        YARNSOCLOSE.Enabled = True
                    Else
                        SOADD.Enabled = False
                        SCHEDULEADD.Enabled = False
                        OPSOADD.Enabled = False
                        REASONADD.Enabled = False
                        YARNSOADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SO_MASTER.Enabled = True
                        SCHEDULE_MASTER.Enabled = True
                        SOEDIT.Enabled = True
                        SCHEDULEEDIT.Enabled = True
                        OPSO_MASTER.Enabled = True
                        OPSOEDIT.Enabled = True
                        SOCLOSE.Enabled = True
                        REASON_MASTER.Enabled = True
                        REASONEDIT.Enabled = True
                        YARNSO_MASTER.Enabled = True
                        YARNSOEDIT.Enabled = True
                        YARNSOCLOSE.Enabled = True
                    Else
                        SOEDIT.Enabled = False
                        SCHEDULEEDIT.Enabled = False
                        OPSOEDIT.Enabled = False
                        REASONEDIT.Enabled = False
                        YARNSOEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "SAMPLE MODULE" Then
                    If DTROW(1).ToString = True Then
                        SAMPLEBARCODE_MASTER.Enabled = True
                        SAMPLEBOOKLET_MASTER.Enabled = True
                        SAMPLEINWARD_MASTER.Enabled = True
                        TRANSFERSAMPLE_MASTER.Enabled = True
                        SAMPLEPRICELIST_MASTER.Enabled = True
                        SAMPLESTOCK_REPORT.Enabled = True
                        PARTYSAMPLESTOCK_REPORT.Enabled = True
                        SAMPLENOTE_MASTER.Enabled = True
                        SAMPLEORDER_MASTER.Enabled = True
                        SAMPLEORDERCLOSE.Enabled = True
                        SAMPLEBOOKLETADD.Enabled = True
                        SAMPLEINWARDADD.Enabled = True
                        TRANSFERSAMPLEADD.Enabled = True
                        SAMPLENOTEADD.Enabled = True
                        SAMPLEPRICELISTADD.Enabled = True
                        SAMPLEORDERADD.Enabled = True
                        SAMPLESTOCKTAKING_MASTER.Enabled = True
                        SAMPLESTOCKTAKINGADD.Enabled = True

                    Else
                        SAMPLEBOOKLETADD.Enabled = False
                        SAMPLEINWARDADD.Enabled = False
                        TRANSFERSAMPLEADD.Enabled = False
                        SAMPLENOTEADD.Enabled = False
                        SAMPLEPRICELISTADD.Enabled = False
                        SAMPLEORDERADD.Enabled = False
                        SAMPLESTOCKTAKINGADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SAMPLEBARCODE_MASTER.Enabled = True
                        SAMPLEBOOKLET_MASTER.Enabled = True
                        SAMPLEINWARD_MASTER.Enabled = True
                        TRANSFERSAMPLE_MASTER.Enabled = True
                        SAMPLEPRICELIST_MASTER.Enabled = True
                        PARTYSAMPLESTOCK_REPORT.Enabled = True
                        SAMPLESTOCK_REPORT.Enabled = True
                        SAMPLENOTE_MASTER.Enabled = True
                        SAMPLEORDER_MASTER.Enabled = True
                        SAMPLEORDERCLOSE.Enabled = True
                        SAMPLEBOOKLETEDIT.Enabled = True
                        SAMPLEINWARDEDIT.Enabled = True
                        TRANSFERSAMPLEEDIT.Enabled = True
                        SAMPLENOTEEDIT.Enabled = True
                        SAMPLEPRICELISTEDIT.Enabled = True
                        SAMPLEORDEREDIT.Enabled = True
                        SAMPLESTOCKTAKING_MASTER.Enabled = True
                        SAMPLESTOCKTAKINGEDIT.Enabled = True
                    Else
                        SAMPLEBOOKLETEDIT.Enabled = False
                        SAMPLEINWARDEDIT.Enabled = False
                        TRANSFERSAMPLEEDIT.Enabled = False
                        SAMPLENOTEEDIT.Enabled = False
                        SAMPLEPRICELISTEDIT.Enabled = False
                        SAMPLEORDEREDIT.Enabled = False
                        SAMPLESTOCKTAKINGEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "GDN" Then
                    If DTROW(1).ToString = True Then
                        GDN_MASTER.Enabled = True
                        GDN_TOOL.Enabled = True
                        GDNADD.Enabled = True
                        CHALLANTYPE_MASTER.Enabled = True
                        CHALLANTYPEADD.Enabled = True
                        TRANSCHALLAN_MASTER.Enabled = True
                        TRANSCHALLANADD.Enabled = True
                        OPGDN_MASTER.Enabled = True

                        If ClientName = "SBA" And UserName = "Admin" Then
                            PROFORMA_MASTER.Enabled = True
                            PROFORMAADD.Enabled = True
                        ElseIf ClientName <> "SBA" Then
                            PROFORMA_MASTER.Enabled = True
                            PROFORMAADD.Enabled = True
                        End If

                        INTERGODOWNTRANSFER_MASTER.Enabled = True
                        INTERGODOWNADD.Enabled = True
                        GP_TOOL.Enabled = True
                        GATEPASS_MASTER.Enabled = True
                        GATEPASSADD.Enabled = True
                        'If ClientName = "AVIS" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "SUPRIYA" Or ClientName = "YASHVI" Or ClientName = "RMANILAL" Or ClientName = "SONU" Or ClientName = "KOTHARI" Or ClientName = "MVIKASKUMAR" Or ClientName = "KARAN" Then
                        '    STOCKADJUSTMENT_MASTER.Enabled = True
                        '    STOCKADJUSTMENTADD.Enabled = True
                        'End If
                        STOCKTAKING_MASTER.Enabled = True
                        STOCKTAKINGADD.Enabled = True
                        YARNCHALLAN_MASTER.Enabled = True
                        YARNCHALLANADD.Enabled = True
                        OPGDNADD.Enabled = True
                    Else
                        GDNADD.Enabled = False
                        CHALLANTYPEADD.Enabled = False
                        TRANSCHALLANADD.Enabled = False
                        PROFORMAADD.Enabled = False
                        INTERGODOWNADD.Enabled = False
                        GATEPASSADD.Enabled = False
                        'If ClientName = "AVIS" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "SUPRIYA" Or ClientName = "YASHVI" Or ClientName = "RMANILAL" Or ClientName = "SONU" Or ClientName = "KOTHARI" Or ClientName = "MVIKASKUMAR" Or ClientName = "KARAN" Then STOCKADJUSTMENTADD.Enabled = False
                        STOCKTAKINGADD.Enabled = False
                        YARNCHALLANADD.Enabled = False
                        OPGDNADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        GDN_MASTER.Enabled = True
                        GDN_TOOL.Enabled = True
                        GDNEDIT.Enabled = True
                        CHALLANTYPE_MASTER.Enabled = True
                        CHALLANTYPEEDIT.Enabled = True
                        TRANSCHALLAN_MASTER.Enabled = True
                        TRANSCHALLANEDIT.Enabled = True
                        OPGDN_MASTER.Enabled = True

                        If ClientName = "SBA" And UserName = "Admin" Then
                            PROFORMA_MASTER.Enabled = True
                            PROFORMAEDIT.Enabled = True
                        ElseIf ClientName <> "SBA" Then
                            PROFORMA_MASTER.Enabled = True
                            PROFORMAEDIT.Enabled = True
                        End If

                        INTERGODOWNTRANSFER_MASTER.Enabled = True
                        INTERGODOWNEDIT.Enabled = True
                        GATEPASS_MASTER.Enabled = True
                        GATEPASSEDIT.Enabled = True
                        'If ClientName = "AVIS" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "SUPRIYA" Or ClientName = "YASHVI" Or ClientName = "RMANILAL" Or ClientName = "SONU" Or ClientName = "KOTHARI" Or ClientName = "MVIKASKUMAR" Or ClientName = "KARAN" Then
                        '    STOCKADJUSTMENT_MASTER.Enabled = True
                        '    STOCKADJUSTMENTEDIT.Enabled = True
                        'End If
                        STOCKTAKING_MASTER.Enabled = True
                        STOCKTAKINGEDIT.Enabled = True
                        YARNCHALLAN_MASTER.Enabled = True
                        YARNCHALLANEDIT.Enabled = True
                        OPGDNEDIT.Enabled = True
                    Else
                        GDNEDIT.Enabled = False
                        CHALLANTYPEEDIT.Enabled = False
                        TRANSCHALLANEDIT.Enabled = False
                        PROFORMAEDIT.Enabled = False
                        INTERGODOWNEDIT.Enabled = False
                        GATEPASSEDIT.Enabled = False
                        'If ClientName = "AVIS" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "SUPRIYA" Or ClientName = "YASHVI" Or ClientName = "RMANILAL" Or ClientName = "SONU" Or ClientName = "KOTHARI" Or ClientName = "MVIKASKUMAR" Or ClientName = "KARAN" Then STOCKADJUSTMENTEDIT.Enabled = False
                        STOCKTAKINGEDIT.Enabled = False
                        YARNCHALLANEDIT.Enabled = False
                        OPGDNEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "SALE INVOICE" Then
                    If DTROW(1).ToString = True Then
                        PROFORMASALE_MASTER.Enabled = True
                        SALE_MASTER.Enabled = True
                        SALE_TOOL.Enabled = True
                        SALEADD.Enabled = True
                        PROFORMASALEADD.Enabled = True
                        SALEAUTOPOSTADD.Enabled = True
                    Else
                        SALEADD.Enabled = False
                        PROFORMASALEADD.Enabled = False
                        SALEAUTOPOSTADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PROFORMASALE_MASTER.Enabled = True
                        SALE_MASTER.Enabled = True
                        SALE_TOOL.Enabled = True
                        SALEEDIT.Enabled = True
                        PROFORMASALEEDIT.Enabled = True
                    Else
                        SALEEDIT.Enabled = False
                        PROFORMASALEEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "SALE RETURN" Then
                    If DTROW(1).ToString = True Then
                        SALERET_MASTER.Enabled = True
                        SALERETADD.Enabled = True
                        SALERETURNCHALLAN_MASTER.Enabled = True
                        SALERETURNCHALLANADD.Enabled = True
                    Else
                        SALERETADD.Enabled = False
                        SALERETURNCHALLANADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SALERET_MASTER.Enabled = True
                        SALERETEDIT.Enabled = True
                        SALERETURNCHALLAN_MASTER.Enabled = True
                        SALERETURNCHALLANEDIT.Enabled = True
                    Else
                        SALERETEDIT.Enabled = False
                        SALERETURNCHALLANEDIT.Enabled = False
                    End If



                    'JOB WORK
                ElseIf DTROW(0).ToString = "JOB OUT" Then
                    If DTROW(1).ToString = True Then
                        JOBOUTTYPE_MASTER.Enabled = True
                        JO_MASTER.Enabled = True
                        JO_TOOL.Enabled = True
                        JOADD.Enabled = True
                        ISSUEPACKING_TOOL.Enabled = True
                        ISSUEPACKING_MASTER.Enabled = True
                        ISSUEPACKINGADD.Enabled = True
                        WEFTISSUE_MASTER.Enabled = True
                        WEFTADD.Enabled = True
                        BEAMISSUE_MASTER.Enabled = True
                        BEAMISSUEADD.Enabled = True
                        JOBOUTTYPEADD.Enabled = True
                        EMBPRODUCTION_MASTER.Enabled = True
                        EMBPRODUCTIONADD.Enabled = True
                    Else
                        JOADD.Enabled = False
                        ISSUEPACKINGADD.Enabled = False
                        WEFTADD.Enabled = False
                        BEAMISSUEADD.Enabled = False
                        JOBOUTTYPEADD.Enabled = False
                        EMBPRODUCTIONADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        JOBOUTTYPE_MASTER.Enabled = True
                        JO_MASTER.Enabled = True
                        JO_TOOL.Enabled = True
                        JOEDIT.Enabled = True
                        ISSUEPACKING_TOOL.Enabled = True
                        ISSUEPACKING_MASTER.Enabled = True
                        ISSUEPACKINGEDIT.Enabled = True
                        WEFTISSUE_MASTER.Enabled = True
                        WEFTEDIT.Enabled = True
                        BEAMISSUE_MASTER.Enabled = True
                        BEAMISSUEEDIT.Enabled = True
                        JOBOUTTYPEEDIT.Enabled = True
                        EMBPRODUCTION_MASTER.Enabled = True
                        EMBPRODUCTIONEDIT.Enabled = True
                    Else
                        JOEDIT.Enabled = False
                        ISSUEPACKINGEDIT.Enabled = False
                        WEFTEDIT.Enabled = False
                        BEAMISSUEEDIT.Enabled = False
                        JOBOUTTYPEEDIT.Enabled = False
                        EMBPRODUCTIONEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "JOB IN" Then
                    If DTROW(1).ToString = True Then
                        JI_MASTER.Enabled = True
                        JI_TOOL.Enabled = True
                        JIADD.Enabled = True
                        RECPACKING_TOOL.Enabled = True
                        RECPACKING_MASTER.Enabled = True
                        RECPACKINGADD.Enabled = True
                        YARNRECDJOBBER_MASTER.Enabled = True
                        YARNRECDJOBBERADD.Enabled = True
                    Else
                        JIADD.Enabled = False
                        RECPACKINGADD.Enabled = False
                        YARNRECDJOBBERADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        JI_MASTER.Enabled = True
                        JI_TOOL.Enabled = True
                        JIEDIT.Enabled = True
                        RECPACKING_TOOL.Enabled = True
                        RECPACKING_MASTER.Enabled = True
                        RECPACKINGEDIT.Enabled = True
                        YARNRECDJOBBER_MASTER.Enabled = True
                        YARNRECDJOBBEREDIT.Enabled = True
                    Else
                        JIEDIT.Enabled = False
                        RECPACKINGEDIT.Enabled = False
                        YARNRECDJOBBEREDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "YARN RECD" Then
                    If DTROW(1).ToString = True Then
                        YARNRECDGREY_MASTER.Enabled = True
                        YARNRECDGREYADD.Enabled = True
                        YARNRECDGODOWN_MASTER.Enabled = True
                        PRODUCTIONFLOORRETURN_MASTER.Enabled = True
                        YARNRETURNPURCHASE_MASTER.Enabled = True
                        YARNRETURNKNITTING_MASTER.Enabled = True
                        GODOWNYARNWASTAGE_MASTER.Enabled = True
                        JOBBERYARNWASTAGE_MASTER.Enabled = True
                        YARNRECDADD.Enabled = True
                        PRODRETADD.Enabled = True
                        GODOWNYARNWASTAGEADD.Enabled = True
                        JOBBERYARNWASTAGEADD.Enabled = True
                        YARNRETURNPURCHASEADD.Enabled = True
                        YARNRETURNKNITTINGADD.Enabled = True
                    Else
                        YARNRECDGREYADD.Enabled = False
                        YARNRECDADD.Enabled = False
                        PRODRETADD.Enabled = False
                        GODOWNYARNWASTAGEADD.Enabled = False
                        JOBBERYARNWASTAGEADD.Enabled = False
                        YARNRETURNPURCHASEADD.Enabled = False
                        YARNRETURNKNITTINGADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        YARNRECDGREY_MASTER.Enabled = True
                        YARNRECDGREYEDIT.Enabled = True
                        YARNRECDGODOWN_MASTER.Enabled = True
                        PRODUCTIONFLOORRETURN_MASTER.Enabled = True
                        YARNRETURNPURCHASE_MASTER.Enabled = True
                        YARNRETURNKNITTING_MASTER.Enabled = True
                        GODOWNYARNWASTAGE_MASTER.Enabled = True
                        JOBBERYARNWASTAGE_MASTER.Enabled = True
                        YARNRECDEDIT.Enabled = True
                        PRODRETURNEDIT.Enabled = True
                        GODOWNYARNWASTAGEEDIT.Enabled = True
                        JOBBERYARNWASTAGEEDIT.Enabled = True
                        YARNRETURNPURCHASEEDIT.Enabled = True
                        YARNRETURNKNITTINGEDIT.Enabled = True
                    Else
                        YARNRECDGREYEDIT.Enabled = False
                        YARNRECDEDIT.Enabled = False
                        PRODRETURNEDIT.Enabled = False
                        GODOWNYARNWASTAGEEDIT.Enabled = False
                        JOBBERYARNWASTAGEEDIT.Enabled = False
                        YARNRETURNPURCHASEEDIT.Enabled = False
                        YARNRETURNKNITTINGEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "YARN ISSUE" Then
                    If DTROW(1).ToString = True Then
                        YARNISSUEJOBBER_MASTER.Enabled = True
                        YARNISSUEKNITTING_MASTER.Enabled = True
                        PRODUCTIONFLOOR_MASTER.Enabled = True
                        YARNISSUEJOBBERADD.Enabled = True
                        YARNISSUEKNITTINGADD.Enabled = True
                        PRODISSADD.Enabled = True
                        YARNDYEINGPROGRAM_MASTER.Enabled = True
                        YARNDYEINGPROGRAMADD.Enabled = True
                        YARNPROGRAMCLOSE.Enabled = True
                        BEAMPROG_MASTER.Enabled = True
                        BEAMPROGADD.Enabled = True
                    Else
                        YARNISSUEJOBBERADD.Enabled = False
                        YARNISSUEKNITTINGADD.Enabled = False
                        PRODISSADD.Enabled = False
                        YARNDYEINGPROGRAMADD.Enabled = False
                        BEAMPROGADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        YARNISSUEJOBBER_MASTER.Enabled = True
                        YARNISSUEKNITTING_MASTER.Enabled = True
                        PRODUCTIONFLOOR_MASTER.Enabled = True
                        YARNISSUEJOBBEREDIT.Enabled = True
                        YARNISSUEKNITTINGEDIT.Enabled = True
                        PRODISSEDIT.Enabled = True
                        YARNDYEINGPROGRAM_MASTER.Enabled = True
                        YARNDYEINGPROGRAMEDIT.Enabled = True
                        YARNPROGRAMCLOSE.Enabled = True
                        BEAMPROG_MASTER.Enabled = True
                        BEAMPROGEDIT.Enabled = True
                    Else
                        YARNISSUEJOBBEREDIT.Enabled = False
                        YARNISSUEKNITTINGEDIT.Enabled = False
                        PRODISSEDIT.Enabled = False
                        YARNDYEINGPROGRAMEDIT.Enabled = False
                        BEAMPROGEDIT.Enabled = False
                    End If


                    'PRODUCTION
                ElseIf DTROW(0).ToString = "PACKING SLIP" Then
                    If DTROW(1).ToString = True Then
                        PS_MASTER.Enabled = True
                        FINALPS_MASTER.Enabled = True
                        PSADD.Enabled = True
                        FINALPSADD.Enabled = True
                    Else
                        PSADD.Enabled = False
                        FINALPSADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PS_MASTER.Enabled = True
                        FINALPS_MASTER.Enabled = True
                        PSEDIT.Enabled = True
                        FINALPSEDIT.Enabled = True
                    Else
                        PSEDIT.Enabled = False
                        FINALPSEDIT.Enabled = False
                    End If


                    'ACCOUNTS
                ElseIf DTROW(0).ToString = "PAYMENT" Then
                    If DTROW(1).ToString = True Then
                        PAY_MASTER.Enabled = True
                        PAYADD.Enabled = True
                        CHQENTRIES_MASTER.Enabled = True
                        CHQENTADD.Enabled = True
                    Else
                        PAYADD.Enabled = False
                        CHQENTADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PAY_MASTER.Enabled = True
                        PAYEDIT.Enabled = True
                        CHQENTRIES_MASTER.Enabled = True
                        CHQENTEDIT.Enabled = True
                    Else
                        PAYEDIT.Enabled = False
                        CHQENTEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "RECEIPT" Then
                    If DTROW(1).ToString = True Then
                        REC_MASTER.Enabled = True
                        RECADD.Enabled = True
                    Else
                        RECADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        REC_MASTER.Enabled = True
                        RECEDIT.Enabled = True
                    Else
                        RECEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "CONTRA VOUCHER" Then
                    If DTROW(1).ToString = True Then
                        CONTRA_MASTER.Enabled = True
                        CONTRAADD.Enabled = True
                    Else
                        CONTRAADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        CONTRA_MASTER.Enabled = True
                        CONTRAEDIT.Enabled = True
                    Else
                        CONTRAEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "JOURNAL VOUCHER" Then
                    If DTROW(1).ToString = True Then
                        JV_MASTER.Enabled = True
                        JVADD.Enabled = True
                    Else
                        JVADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        JV_MASTER.Enabled = True
                        JVEDIT.Enabled = True
                    Else
                        JVEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "DEBIT NOTE" Then
                    If DTROW(1).ToString = True Then
                        DEBIT_MASTER.Enabled = True
                        PROFORMADEBIT_MASTER.Enabled = True
                        DEBITADD.Enabled = True
                        PROFORMADEBITADD.Enabled = True
                    Else
                        DEBITADD.Enabled = False
                        PROFORMADEBITADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        DEBIT_MASTER.Enabled = True
                        PROFORMADEBIT_MASTER.Enabled = True
                        DEBITEDIT.Enabled = True
                        PROFORMADEBITEDIT.Enabled = True
                    Else
                        DEBITEDIT.Enabled = False
                        PROFORMADEBITEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "CREDIT NOTE" Then
                    If DTROW(1).ToString = True Then
                        CREDIT_MASTER.Enabled = True
                        PROFORMACREDIT_MASTER.Enabled = True
                        CREDITADD.Enabled = True
                        PROFORMACREDITADD.Enabled = True
                    Else
                        CREDITADD.Enabled = False
                        PROFORMACREDITADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        CREDIT_MASTER.Enabled = True
                        PROFORMACREDIT_MASTER.Enabled = True
                        CREDITEDIT.Enabled = True
                        PROFORMACREDITEDIT.Enabled = True
                    Else
                        CREDITEDIT.Enabled = False
                        PROFORMACREDITEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "VOUCHER ENTRY" Then
                    If DTROW(1).ToString = True Then
                        VOUCHER_MASTER.Enabled = True
                        VOUCHERADD.Enabled = True
                    Else
                        VOUCHERADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        VOUCHER_MASTER.Enabled = True
                        VOUCHEREDIT.Enabled = True
                    Else
                        VOUCHEREDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "PAYROLL" Then
                    If DTROW(1).ToString = True Then
                        SALARYSLIP_MASTER.Enabled = True
                        SALARYSLIPADD.Enabled = True
                    Else
                        SALARYSLIPADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SALARYSLIP_MASTER.Enabled = True
                        SALARYSLIPEDIT.Enabled = True
                    Else
                        SALARYSLIPEDIT.Enabled = False
                    End If

                    'REPORTS
                ElseIf DTROW(0).ToString = "PURCHASE REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PUR_REPORTS.Enabled = True
                    End If


                ElseIf DTROW(0).ToString = "SALE REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SALE_REPORTS.Enabled = True
                    End If


                ElseIf DTROW(0).ToString = "JOB REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        JOB_REPORTS.Enabled = True
                    End If


                ElseIf DTROW(0).ToString = "PRODUCTION REPORTS" Then
                    'If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                    '    PROD_REPORTS.Enabled = True
                    'End If


                ElseIf DTROW(0).ToString = "STOCK REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        STOCK_REPORTS.Enabled = True
                    End If


                ElseIf DTROW(0).ToString = "ACCOUNT REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        REGISTER_MAIN.Enabled = True
                        JOURNALREGISTER_MASTER.Enabled = True
                        EXPENSEREGISTER_MASTER.Enabled = True
                        CONTRAREGISTER_MASTER.Enabled = True
                        DNREGISTER_MASTER.Enabled = True
                        CNREGISTER_MASTER.Enabled = True
                        BANKREGISTER_MASTER.Enabled = True
                        CASHREGISTER_MASTER.Enabled = True

                        ACCOUNTREPORT_MAIN.Enabled = True
                        OTHERREPORT_MAIN.Enabled = True
                        DAYBOOK_MASTER.Enabled = True
                        LEDGERBOOK_MASTER.Enabled = True
                        TAXREGISTER_MASTER.Enabled = True
                        GSTTAXFILTER_MASTER.Enabled = True
                        UPLOADGSTR2_MASTER.Enabled = True
                        PURCHASEITC_MASTER.Enabled = True
                        GROUPTRANS_MASTER.Enabled = True
                        If ClientName <> "YASHVI" Then
                            ACCPAYABLE_MASTER.Enabled = True
                            ACCRECEIVABLE_MASTER.Enabled = True
                            RECOUT_TOOL.Enabled = True
                            PAYOUT_TOOL.Enabled = True
                            PURREGISTER_MASTER.Enabled = True
                            SALEREGISTER_MASTER.Enabled = True
                            OUTSTANDINGREPORT_MENU.Enabled = True
                            'If ClientName <> "INDRAPUJAIMPEX" Then
                            '    TB_MASTER.Enabled = True
                            '    PL_MASTER.Enabled = True
                            '    BS_MASTER.Enabled = True
                            'End If
                        End If
                        FORMENTRY_MASTER.Enabled = True

                        PAYMENTREGISTER_MENU.Enabled = True
                        RECEIPTREGISTER_MENU.Enabled = True
                    End If


                    'ONLY FOR YASHVI
                ElseIf DTROW(0).ToString = "REGISTER REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        If ClientName = "YASHVI" Or ClientName = "INDRAPUJAIMPEX" Then
                            PURREGISTER_MASTER.Enabled = True
                            SALEREGISTER_MASTER.Enabled = True
                        End If
                    End If

                    'ONLY FOR YASHVI
                ElseIf DTROW(0).ToString = "OUTSTANDING REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        ACCPAYABLE_MASTER.Enabled = True
                        ACCRECEIVABLE_MASTER.Enabled = True
                        OUTSTANDINGREPORT_MENU.Enabled = True
                        RECOUT_TOOL.Enabled = True
                        PAYOUT_TOOL.Enabled = True
                    End If


                    'ONLY FOR YASHVI
                ElseIf DTROW(0).ToString = "SPECIAL REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        TB_MASTER.Enabled = True
                        PL_MASTER.Enabled = True
                        BS_MASTER.Enabled = True

                    End If

                End If
            Next

            If DASHBOARDTOOLVISIBLE = True Then objhp.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub HEADERVISIBLE()
        Try

            TOOLHOME.Visible = HOME
            HOME_TOOLSTRIP.Visible = HOME
            PO_TOOL.Visible = POTOOLVISIBLE
            PO_TOOLSTRIP.Visible = POTOOLVISIBLE
            GRNGREY_TOOL.Visible = GRNTOOLVISIBLE
            GRNGREY_TOOLSTRIP.Visible = GRNTOOLVISIBLE
            MATREC_TOOL.Visible = MATRECTOOLVISIBLE
            MATREC_TOOLSTRIP.Visible = MATRECTOOLVISIBLE
            INHOUSECHECK_TOOL.Visible = INHOUSECHKTOOLVISIBLE
            INHOUSECHECK_TOOLSTRIP.Visible = INHOUSECHKTOOLVISIBLE
            GDN_TOOL.Visible = GDNTOOLVISIBLE
            GDN_TOOLSTRIP.Visible = GDNTOOLVISIBLE
            GP_TOOL.Visible = GDNTOOLVISIBLE
            GP_TOOLSTRIP.Visible = GDNTOOLVISIBLE
            JO_TOOL.Visible = JOTOOLVISIBLE
            JO_TOOLSTRIP.Visible = JOTOOLVISIBLE
            JI_TOOL.Visible = JITOOLVISIBLE
            JI_TOOLSTRIP.Visible = JITOOLVISIBLE
            ISSUEPACKING_TOOL.Visible = ISSPACKTOOLVISIBLE
            ISSUEPACKING_TOOLSTRIP.Visible = ISSPACKTOOLVISIBLE
            RECPACKING_TOOL.Visible = RECPACKTOOLVISIBLE
            RECPACKING_TOOLSTRIP.Visible = RECPACKTOOLVISIBLE
            PURCHASE_TOOL.Visible = PURCHASETOOLVISIBLE
            PURCHASE_TOOLSTRIP.Visible = PURCHASETOOLVISIBLE
            SALE_TOOL.Visible = SALETOOLVISIBLE
            SALE_TOOLSTRIP.Visible = SALETOOLVISIBLE
            SO_TOOL.Visible = SOTOOLVISIBLE
            SO_TOOLSTRIP.Visible = SOTOOLVISIBLE
            GRNCHECKING_TOOL.Visible = GRNCHECKTOOLVISIBLE
            GRNCHECKING_TOOLSTRIP.Visible = GRNCHECKTOOLVISIBLE

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AddNewCategoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CATEGORYADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "CATEGORY"
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewTypeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MATERIALADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "MATERIAL TYPE"
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewGroupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GROUPADD.Click
        Try
            Dim objGroupMaster As New GroupMaster
            objGroupMaster.MdiParent = Me
            objGroupMaster.Show()
            objGroupMaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewCityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewCityToolStripMenuItem.Click
        Try
            Dim objcitymaster As New citymaster
            objcitymaster.MdiParent = Me
            objcitymaster.frmstring = "CITYMASTER"
            objcitymaster.Show()
            objcitymaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewStateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewStateToolStripMenuItem.Click
        Try
            Dim objcitymaster As New citymaster
            objcitymaster.MdiParent = Me
            objcitymaster.frmstring = "STATEMASTER"
            objcitymaster.Show()
            objcitymaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewCountryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewCountryToolStripMenuItem.Click
        Try
            Dim objcitymaster As New citymaster
            objcitymaster.MdiParent = Me
            objcitymaster.frmstring = "COUNTRYMASTER"
            objcitymaster.Show()
            objcitymaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewAreaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewAreaToolStripMenuItem.Click
        Try
            Dim objcitymaster As New citymaster
            objcitymaster.MdiParent = Me
            objcitymaster.frmstring = "AREAMASTER"
            objcitymaster.Show()
            objcitymaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub AddNewItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ITEMADD.Click
        Try
            Dim objItemMaster As New ItemMaster
            objItemMaster.MdiParent = Me
            objItemMaster.frmstring = "MERCHANT"
            objItemMaster.Show()
            objItemMaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewAccountsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ACCADD.Click
        Try
            Dim objAccountMaster As New AccountsMaster
            objAccountMaster.MdiParent = Me
            objAccountMaster.frmstring = "ACCOUNTS"
            objAccountMaster.Show()
            objAccountMaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingGroupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GROUPEDIT.Click
        Try
            Dim objGroupDetails As New GroupDetails
            objGroupDetails.MdiParent = Me
            objGroupDetails.Show()
            objGroupDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingAccoutsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ACCEDIT.Click
        Try
            Dim objAccountDetails As New AccountsDetails
            objAccountDetails.MdiParent = Me
            objAccountDetails.frmstring = "ACCOUNTS"
            objAccountDetails.Show()
            objAccountDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ITEMEDIT.Click
        Try
            Dim objItemDetails As New ItemDetails
            objItemDetails.MdiParent = Me
            objItemDetails.FRMSTRING = "MERCHANT"
            objItemDetails.Show()
            objItemDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingCategoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CATEGORYEDIT.Click
        Try
            Dim objCategoryDetails As New CategoryDetails
            objCategoryDetails.MdiParent = Me
            objCategoryDetails.FRMSTRING = "CATEGORY"
            objCategoryDetails.Show()
            objCategoryDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingTypeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MATERIALEDIT.Click
        Try
            Dim objCategoryDetails As New CategoryDetails
            objCategoryDetails.MdiParent = Me
            objCategoryDetails.FRMSTRING = "MATERIAL TYPE"
            objCategoryDetails.Show()
            objCategoryDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingAreaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditExistingAreaToolStripMenuItem.Click
        Try
            Dim objCityDetails As New CityDetails
            objCityDetails.MdiParent = Me
            objCityDetails.frmstring = "AREAMASTER"
            objCityDetails.Show()
            objCityDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingCityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditExistingCityToolStripMenuItem.Click
        Try
            Dim objCityDetails As New CityDetails
            objCityDetails.MdiParent = Me
            objCityDetails.frmstring = "CITYMASTER"
            objCityDetails.Show()
            objCityDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingStateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditExistingStateToolStripMenuItem.Click
        Try
            Dim objCityDetails As New CityDetails
            objCityDetails.MdiParent = Me
            objCityDetails.frmstring = "STATEMASTER"
            objCityDetails.Show()
            objCityDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingCountryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditExistingCountryToolStripMenuItem.Click
        Try
            Dim objCityDetails As New CityDetails
            objCityDetails.MdiParent = Me
            objCityDetails.frmstring = "COUNTRYMASTER"
            objCityDetails.Show()
            objCityDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewUnitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UNITADD.Click
        Try
            Dim objunitmaster As New UnitMaster
            objunitmaster.MdiParent = Me
            objunitmaster.frmString = "UNIT"
            objunitmaster.Show()
            objunitmaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingUnitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UNITEDIT.Click
        Try
            Dim objUnitDetails As New UnitDetails
            objUnitDetails.MdiParent = Me
            objUnitDetails.frmstring = "UNIT"
            objUnitDetails.Show()
            objUnitDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub addUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles USERADD.Click
        Try
            Dim objuser As New UserMaster
            objuser.MdiParent = Me
            objuser.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub editUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles USEREDIT.Click
        Try
            Dim objuser As New UserDetails
            objuser.MdiParent = Me
            objuser.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub opencmp(ByVal CMP As String)
        Try

            Dim objcmn As New ClsCommon
            Dim DT As DataTable

            DT = objcmn.SEARCH("CMPMASTER.CMP_NAME, YearMaster.YEAR_DBNAME, Cmpmaster.CMP_ID, YearMaster.YEAR_STARTDATE, YearMaster.YEAR_ENDDATE, YearMaster.YEAR_ID", "", " CMPMASTER INNER JOIN YEARMASTER ON YEARMASTER.YEAR_CMPID = CMPMASTER.CMP_ID", " And CMPMASTER.CMP_NAME = '" & CMP & "'")
            CmpName = DT.Rows(0).Item(0).ToString
            DBName = DT.Rows(0).Item(1).ToString
            CmpId = DT.Rows(0).Item(2).ToString
            AccFrom = DT.Rows(0).Item(3)
            AccTo = DT.Rows(0).Item(4)
            YearId = DT.Rows(0).Item(5).ToString
            Cmppassword.cmdback.Visible = False
            Cmppassword.lblretype.Visible = False
            Cmppassword.txtretypepassword.Visible = False
            Cmppassword.cmdnext.Text = "&Ok"
            Cmppassword.ShowDialog()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewRegisterToolStripMenuItem.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "PURCHASE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem14.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "SALE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem17.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "JOURNAL"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem19.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "CONTRA"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem21.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "PAYMENT"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem23.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "RECEIPT"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TAXADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TAXADD.Click
        Try
            Dim OBJTAX As New Taxmaster
            OBJTAX.MdiParent = Me
            OBJTAX.Show()
            OBJTAX.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TAXEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TAXEDIT.Click
        Try
            Dim OBJTAXDETAILS As New TaxDetails
            OBJTAXDETAILS.MdiParent = Me
            OBJTAXDETAILS.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewExpenseRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewExpenseRegisterToolStripMenuItem.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "EXPENSE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewRegisterToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewRegisterToolStripMenuItem1.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "CREDITNOTE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewRegisterToolStripMenuItem2.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "DEBITNOTE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub QUALITYADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QUALITYADD.Click
        Try
            Dim objCategory As New QualityMaster
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub COLORADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COLORADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "COLOR"
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub DEPARTMENTADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DEPARTMENTADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "DEPARTMENT"
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub QUALITYEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QUALITYEDIT.Click
        Try
            Dim objCategoryDetails As New QualityDetails
            objCategoryDetails.MdiParent = Me
            objCategoryDetails.Show()
            objCategoryDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub COLOREDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COLOREDIT.Click
        Try
            Dim objCategoryDetails As New CategoryDetails
            objCategoryDetails.MdiParent = Me
            objCategoryDetails.FRMSTRING = "COLOR"
            objCategoryDetails.Show()
            objCategoryDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub DEPARTMENTEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DEPARTMENTEDIT.Click
        Try
            Dim objCategoryDetails As New CategoryDetails
            objCategoryDetails.MdiParent = Me
            objCategoryDetails.FRMSTRING = "DEPARTMENT"
            objCategoryDetails.Show()
            objCategoryDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ChangeCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeCompany.Click
        Try
            'close all child forms
            Dim frm As Form
            For Each frm In MdiChildren
                frm.Close()
            Next

            Me.Dispose()
            Cmpdetails.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ChangeUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeUserToolStripMenuItem.Click
        Try
            'close all child forms
            Dim frm As Form
            For Each frm In MdiChildren
                frm.Close()
            Next

            Me.Dispose()
            Login.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMPEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMPEDIT.Click
        Try
            Cmpmaster.EDIT = True
            Cmpmaster.TEMPCMPNAME = CmpName
            Cmpmaster.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMPADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMPADD.Click
        Try
            Dim obj As New Cmpmaster
            obj.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRNADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRNADD.Click
        Try
            Dim OBJGRN As New GRN
            OBJGRN.MdiParent = Me
            OBJGRN.FRMSTRING = "GRN FANCY"
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRNEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRNEDIT.Click
        Try
            Dim OBJGRN As New GRNDetails
            OBJGRN.MdiParent = Me
            OBJGRN.FRMSTRING = "GRN FANCY"
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PIECETYPEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PIECETYPEADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "PIECE TYPE"
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PIECETYPEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PIECETYPEEDIT.Click
        Try
            Dim objCategoryDetails As New CategoryDetails
            objCategoryDetails.MdiParent = Me
            objCategoryDetails.FRMSTRING = "PIECE TYPE"
            objCategoryDetails.Show()
            objCategoryDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GreyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INHOUSEST.Click
        Try
            Dim OBJ As New OpeningStock
            OBJ.FRMSTRING = "INHOUSE"
            OBJ.MdiParent = Me
            OBJ.Show()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub DayBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DAYBOOK_MASTER.Click
        Try
            Dim OBJDAYBOOK As New DayBook
            OBJDAYBOOK.MdiParent = Me
            OBJDAYBOOK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgerBookToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerBookToolStripMenuItem1.Click
        Try
            Dim objledgerbook As New RegisterDetails
            objledgerbook.MdiParent = Me
            objledgerbook.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgerBillWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerBillWiseToolStripMenuItem.Click
        Try
            Dim OBJBILL As New LedgerBillwise
            OBJBILL.MdiParent = Me
            OBJBILL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURREGISTER_MASTER.Click
        Try
            Dim objpurreg As New PurchaseRegister
            objpurreg.MdiParent = Me
            objpurreg.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALEREGISTER_MASTER.Click
        Try
            Dim objsalereg As New SaleRegister
            objsalereg.MdiParent = Me
            objsalereg.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JournalRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JOURNALREGISTER_MASTER.Click
        Try
            Dim OBJJVREG As New JournalRegister
            OBJJVREG.MdiParent = Me
            OBJJVREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ContraRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CONTRAREGISTER_MASTER.Click
        Try
            Dim OBJCONTRAREG As New ContraRegister
            OBJCONTRAREG.MdiParent = Me
            OBJCONTRAREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DebitNoteRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DNREGISTER_MASTER.Click
        Try
            Dim OBJDNREG As New DNRegister
            OBJDNREG.MdiParent = Me
            OBJDNREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreditNoteRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CNREGISTER_MASTER.Click
        Try
            Dim OBJCNREG As New CNRegister
            OBJCNREG.MdiParent = Me
            OBJCNREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BankBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BANKREGISTER_MASTER.Click
        Try
            Dim OBJBANKREG As New BankRegister
            OBJBANKREG.MdiParent = Me
            OBJBANKREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CashBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CASHREGISTER_MASTER.Click
        Try
            Dim OBJCASHREG As New cashregister1
            OBJCASHREG.MdiParent = Me
            OBJCASHREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GroupSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupSummaryToolStripMenuItem.Click
        Try
            Dim OBJGROUP As New GroupRegister
            OBJGROUP.MdiParent = Me
            OBJGROUP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TrialBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TB_MASTER.Click
        Try
            Dim OBJTB As New TB
            OBJTB.MdiParent = Me
            OBJTB.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ProfitLossToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PL_MASTER.Click
        Try
            Dim objpl As New PL
            objpl.MdiParent = Me
            objpl.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BalanceSheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BS_MASTER.Click
        Try
            Dim OBJBALANCESHEET As New BS
            OBJBALANCESHEET.MdiParent = Me
            OBJBALANCESHEET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FORMSUMMARY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FORMSUMMARY.Click
        Try
            Dim OBJCFORM As New CFormSummary
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FORMDETAILS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FORMDETAILS.Click
        Try
            Dim OBJCFORM As New CFormEntry
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WOEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SOEDIT.Click
        Try
            Dim OBJCFORM As New SaleOrderDetails
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub MERGEDSTOCK_REPORT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MERGEDSTOCK_REPORT.Click
    '    Try
    '        Dim OBJMERGEDSTOCK As New MergedStockReport
    '        OBJMERGEDSTOCK.MdiParent = Me
    '        OBJMERGEDSTOCK.Show()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub PSADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PSADD.Click
        Try
            Dim ObjPackingSlip As New PackingSlip
            ObjPackingSlip.MdiParent = Me
            ObjPackingSlip.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PSEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PSEDIT.Click
        Try
            Dim OBJPKGDETAIL As New PackingSlipDetails
            OBJPKGDETAIL.MdiParent = Me
            OBJPKGDETAIL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingDesignToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DESIGNEDIT.Click
        Try
            Dim OBJDESIGN As New DesignMasterDetail
            OBJDESIGN.MdiParent = Me
            OBJDESIGN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRN_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRNGREY_TOOL.Click
        Try
            Dim OBJGRN As New GRN
            OBJGRN.MdiParent = Me
            If ClientName = "CC" Or ClientName = "C3" Or ClientName = "KREEVE" Or ClientName = "PURPLE" Or ClientName = "INDRANI" Or ClientName = "GELATO" Or ClientName = "NVAHAN" Or ClientName = "SAKARIA" Or ClientName = "MANIBHADRA" Or ClientName = "TCOT" Or ClientName = "MOMAI" Or ClientName = "MNIKHIL" Or ClientName = "HRITI" Or ClientName = "MVIKASKUMAR" Or ClientName = "KRFABRICS" Or ClientName = "AMAN" Or ClientName = "BALAJI" Or ClientName = "CHINTAN" Or ClientName = "SSC" Or ClientName = "SST" Or ClientName = "MBB" Or ClientName = "VINIT" Or ClientName = "MANISH" Or ClientName = "TINUMINU" Or ClientName = "RADHA" Or ClientName = "VINTAGEINDIA" Or ClientName = "BIGAPPLE" Or HIDEGREY = True Then OBJGRN.FRMSTRING = "GRN FANCY" Else OBJGRN.FRMSTRING = "GRNJOB"
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GDN_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GDN_TOOL.Click
        Try
            Dim OBJGDN As New GDN
            OBJGDN.MdiParent = Me
            OBJGDN.Show()
            OBJGDN.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StoreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ATPARTYST.Click
        Try
            Dim OBJ As New OpeningStock
            OBJ.FRMSTRING = "JOBBERSTOCK"
            OBJ.MdiParent = Me
            OBJ.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MDIMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Dim TEMPMSG As Integer = MsgBox("Wish to Exit?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbNo Then
                Dim OBJCMN As New ClsCommon
                Dim DT2 As DataTable = OBJCMN.Execute_Any_String(" UPDATE USERMASTER SET USER_CHK = 0", " WHERE USER_NAME='" & UserName & "' and user_cmpid='" & CmpId & "' and user_locationid='" & Locationid & "' and user_yearid='" & YearId & "'", "")
                e.Cancel = True
                Exit Sub
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub AddGRNCheckingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRNCHECKINGADD.Click
        Try
            Dim OBJ As New GRNChecking
            OBJ.MdiParent = Me
            OBJ.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingCheckingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRNCHECKINGEDIT.Click
        Try
            Dim OBJ As New GRNCheckingDetails
            OBJ.MdiParent = Me
            OBJ.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRNGREYADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRNGREYADD.Click
        Try
            Dim OBJGRN As New GRN
            OBJGRN.MdiParent = Me
            OBJGRN.FRMSTRING = "GRNJOB"
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRNGREYEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRNGREYEDIT.Click
        Try
            Dim OBJGRN As New GRNDetails
            OBJGRN.MdiParent = Me
            OBJGRN.FRMSTRING = "GRNJOB"
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub POADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POADD.Click
        Try
            Dim ObjPurchaseOrder As New PurchaseOrder
            ObjPurchaseOrder.MdiParent = Me
            ObjPurchaseOrder.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub POEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POEDIT.Click
        Try
            Dim ObjPurchaseOrder As New PurchaseOrderDetails
            ObjPurchaseOrder.MdiParent = Me
            ObjPurchaseOrder.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MATRECADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MATRECADD.Click
        Try
            Dim OBJMATREC As New MaterialReceipt
            OBJMATREC.MdiParent = Me
            OBJMATREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MATRECEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MATRECEDIT.Click
        Try
            Dim OBJMATREC As New MaterialReceiptDetails
            OBJMATREC.MdiParent = Me
            OBJMATREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DESIGNADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DESIGNADD.Click
        Try
            Dim OBJDESIGN As New DesignMaster
            OBJDESIGN.MdiParent = Me
            OBJDESIGN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GDNADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GDNADD.Click
        Try
            Dim Objgdn As New GDN
            Objgdn.MdiParent = Me
            Objgdn.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GDNEDIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GDNEDIT.Click
        Try
            Dim Objgdndetails As New GDNDetails
            Objgdndetails.MdiParent = Me
            Objgdndetails.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SOADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SOADD.Click
        Try
            Dim ObjSO As New SaleOrder
            ObjSO.MdiParent = Me
            ObjSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReprintBarcodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReprintBarcodeToolStripMenuItem.Click
        Try
            Dim OBJREPRINT As New Reprint
            OBJREPRINT.MdiParent = Me
            OBJREPRINT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JOADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JOADD.Click
        Try
            Dim ObjJobout As New JobOut
            ObjJobout.MdiParent = Me
            ObjJobout.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JOEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JOEDIT.Click
        Try
            Dim ObjJobOutDetails As New JobOutDetails
            ObjJobOutDetails.MdiParent = Me
            ObjJobOutDetails.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SOEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SOEDIT.Click
        Try
            Dim ObjSO As New SaleOrderDetails
            ObjSO.MdiParent = Me
            ObjSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JIADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JIADD.Click
        Try
            Dim ObjJI As New JobIn
            ObjJI.MdiParent = Me
            ObjJI.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JIEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JIEDIT.Click
        Try
            Dim ObjJI As New JobInDetails
            ObjJI.MdiParent = Me
            ObjJI.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRNCHECKING_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRNCHECKING_TOOL.Click
        Try
            Dim OBJ As New GRNChecking
            OBJ.MdiParent = Me
            OBJ.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MATREC_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MATREC_TOOL.Click
        Try
            Dim OBJMATREC As New MaterialReceipt
            OBJMATREC.MdiParent = Me
            OBJMATREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JO_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JO_TOOL.Click
        Try
            Dim ObjJobout As New JobOut
            ObjJobout.MdiParent = Me
            ObjJobout.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JI_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JI_TOOL.Click
        Try
            Dim ObjJI As New JobIn
            ObjJI.MdiParent = Me
            ObjJI.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DesignWiseStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DesignWiseStockToolStripMenuItem.Click
        Try
            Dim OBJDESIGN As New DesignwiseStock
            OBJDESIGN.MdiParent = Me
            OBJDESIGN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ShadeWiseStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShadeWiseStockToolStripMenuItem.Click
        Try
            Dim OBJDESIGN As New ColorwiseStock
            OBJDESIGN.MdiParent = Me
            OBJDESIGN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GodownWiseStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GodownWiseStockToolStripMenuItem.Click
        Try
            Dim OBJDESIGN As New GodownwiseStock
            OBJDESIGN.MdiParent = Me
            OBJDESIGN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StockDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOCKDETAILS.Click
        Try
            Dim OBJDESIGN As New GodownwiseDetails
            OBJDESIGN.MdiParent = Me
            OBJDESIGN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ChangeBarcodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHANGEBARCODE_MASTER.Click
        Try
            Dim OBJCHANGE As New ChangeBarcode
            OBJCHANGE.MdiParent = Me
            OBJCHANGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALERETADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALERETADD.Click
        Try
            Dim ObjSaleReturn As New SaleReturn
            ObjSaleReturn.MdiParent = Me
            ObjSaleReturn.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALERETEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALERETEDIT.Click
        Try
            Dim ObjSaleReturnDetails As New SaleReturnDetails
            ObjSaleReturnDetails.MdiParent = Me
            ObjSaleReturnDetails.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURRETADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURRETADD.Click
        Try
            Dim ObjPurchaseReturn As New PurchaseReturn
            ObjPurchaseReturn.MdiParent = Me
            ObjPurchaseReturn.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURRETEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURRETEDIT.Click
        Try
            Dim ObjPRDetails As New PurchaseReturnDetails
            ObjPRDetails.MdiParent = Me
            ObjPRDetails.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OutStockDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutStockDetailsToolStripMenuItem.Click
        Try
            Dim ObjOutStockReport As New OutStockReport
            ObjOutStockReport.MdiParent = Me
            ObjOutStockReport.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALEADD.Click
        Try
            Dim OBJINVOICE As New InvoiceMaster
            OBJINVOICE.MdiParent = Me
            OBJINVOICE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURINVADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURINVADD.Click
        Try
            Dim Objpinvoice As New PurchaseMaster
            Objpinvoice.MdiParent = Me
            Objpinvoice.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURINVEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURINVEDIT.Click
        Try
            Dim ObjPurchaseDetails As New PurchaseInvoiceDetails
            ObjPurchaseDetails.MdiParent = Me
            ObjPurchaseDetails.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PAYADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAYADD.Click
        Try
            Dim OBJPAY As New PaymentMaster
            OBJPAY.MdiParent = Me
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PAYEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAYEDIT.Click
        Try
            Dim OBJPAY As New PaymentDetails
            OBJPAY.MdiParent = Me
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECADD.Click
        Try
            Dim OBJREC As New Receipt
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECEDIT.Click
        Try
            Dim OBJREC As New ReceiptDetails
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CONTRAADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CONTRAADD.Click
        Try
            Dim OBJCON As New ContraEntry
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CONTRAEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CONTRAEDIT.Click
        Try
            Dim OBJCON As New ContraDetails
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JVEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JVADD.Click
        Try
            Dim OBJJV As New journal
            OBJJV.MdiParent = Me
            OBJJV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingJournalEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JVEDIT.Click
        Try
            Dim OBJJV As New JournalDetails
            OBJJV.MdiParent = Me
            OBJJV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CREDITADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CREDITADD.Click
        Try
            Dim OBJCN As New CREDITNOTE
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CREDITEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CREDITEDIT.Click
        Try
            Dim OBJCN As New CreditNoteDetails
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DEBITADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DEBITADD.Click
        Try
            Dim OBJDN As New DebitNote
            OBJDN.MdiParent = Me
            OBJDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DEBITEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DEBITEDIT.Click
        Try
            Dim OBJDN As New DebitNoteDetails
            OBJDN.MdiParent = Me
            OBJDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub VOUCHERADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VOUCHERADD.Click
        Try
            Dim OBJEXP As New ExpenseVoucher
            OBJEXP.MdiParent = Me
            OBJEXP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub VOUCHEREDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VOUCHEREDIT.Click
        Try
            Dim OBJEXP As New ExpenseVoucherDetails
            OBJEXP.MdiParent = Me
            OBJEXP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALEEDIT.Click
        Try
            Dim OBJINVDETAIL As New InvoiceDetails
            OBJINVDETAIL.MdiParent = Me
            OBJINVDETAIL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub NARRATIONADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NARRATIONADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "NARRATION"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PARTYBANKADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PARTYBANKADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "PARTYBANK"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PARYUBANKEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PARTYBANKEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.FRMSTRING = "PARTYBANK"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub NARRATIONEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NARRATIONEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.FRMSTRING = "NARRATION"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SALE_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALE_TOOL.Click
        Try
            Dim OBJINVOICE As New InvoiceMaster
            OBJINVOICE.MdiParent = Me
            OBJINVOICE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPENINGBILL_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPENINGBILL_MASTER.Click
        Try
            Dim OBJOP As New OpeningBills
            OBJOP.FRMSTRING = "OPENINGBILLS"
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JobberStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobberStockToolStripMenuItem.Click
        Try
            Dim ObjJobberStock As New ItemWiseJobberStock
            ObjJobberStock.MdiParent = Me
            ObjJobberStock.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DyeingHouseStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DyeingHouseStockToolStripMenuItem.Click
        Try
            Dim Objdyehousestock As New DesignwiseStockatDyeingHouse
            Objdyehousestock.MdiParent = Me
            Objdyehousestock.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JobberStockDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobberStockDetailsToolStripMenuItem.Click
        Try
            Dim ObjJobberStockDetails As New ItemWiseJobberStockDetails
            ObjJobberStockDetails.MdiParent = Me
            ObjJobberStockDetails.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DyeingHouseStockDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DyeingHouseStockDetailsToolStripMenuItem.Click
        Try
            Dim Objdyestockdetails As New DesignwiseStockDyeingHouseDetails
            Objdyestockdetails.MdiParent = Me
            Objdyestockdetails.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BackupCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackupCompany.Click
        Try
            backup()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub backup()
        'TAKE BACKUP
        Dim TEMPMSG As Integer = MsgBox("Create Backup?", MsgBoxStyle.YesNo)
        If TEMPMSG = vbYes Then

            'CHECKING FOR BACKUP FOLDER
            If FileIO.FileSystem.DirectoryExists("C:\TEXTRADEBACKUP") = False Then FileIO.FileSystem.CreateDirectory("C:\TEXTRADEBACKUP")


            'COPY THE BACKUP FILE IN DIRECTIORY AND THEN CREATE ZIP
            If FileIO.FileSystem.DirectoryExists("C:\TEXTRADEBACKUP\BACKUP") = False Then FileIO.FileSystem.CreateDirectory("C:\TEXTRADEBACKUP\BACKUP")


            'IF SAME DATE'S BACKUP EXIST THEN DELETE IT THEN RECREATE IT
            If FileIO.FileSystem.FileExists("C:\TEXTRADEBACKUP\BACKUP\TEXTRADE BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".bak") Then FileIO.FileSystem.DeleteFile("C:\TEXTRADEBACKUP\BACKUP\TEXTRADE BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".bak")

            Dim OBJCMN As New ClsCommon
            On Error Resume Next
            Dim DT As DataTable = OBJCMN.Execute_Any_String(" BACKUP DATABASE TEXTRADE TO DISK='C:\TEXTRADEBACKUP\BACKUP\TEXTRADE BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".BAK'", "", "")


            ZipFile.CreateFromDirectory("C:\TEXTRADEBACKUP\BACKUP", "C:\TEXTRADEBACKUP\TEXTRADE BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".zip", CompressionLevel.Optimal, False)

            'DELETE THE BACKUP FOLDER
            If FileIO.FileSystem.DirectoryExists("C:\TEXTRADEBACKUP\BACKUP") = True Then FileIO.FileSystem.DeleteDirectory("C:\TEXTRADEBACKUP\BACKUP", FileIO.DeleteDirectoryOption.DeleteAllContents)


            MsgBox("Backup Completed")
        End If

    End Sub

    Private Sub PendingJoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PendingJoToolStripMenuItem.Click
        Try
            Dim ObjpendingJO As New JobOutDetails
            ObjpendingJO.Where = " AND (JO_TOTALMTRS-JO_RECDMTRS)>0"
            ObjpendingJO.MdiParent = Me
            ObjpendingJO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgerOnScreenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerOnScreenToolStripMenuItem.Click
        Try
            Dim objledger As New LedgerSummary
            objledger.MdiParent = Me
            objledger.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURCHASE_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURCHASE_TOOL.Click
        Try
            Dim OBJPUR As New PurchaseMaster
            OBJPUR.MdiParent = Me
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RegisterWisePurchaseSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegisterWisePurchaseSummaryToolStripMenuItem.Click
        Try
            Dim OBJFILTER As New filter
            OBJFILTER.frmstring = "REGISTERPURCHASESUMMARY"
            OBJFILTER.MdiParent = Me
            OBJFILTER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RegiserWiseSaleSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegiserWiseSaleSummaryToolStripMenuItem.Click
        Try
            Dim OBJFILTER As New filter
            OBJFILTER.frmstring = "REGISTERSALESUMMARY"
            OBJFILTER.MdiParent = Me
            OBJFILTER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MERGELEDGER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MERGELEDGER.Click
        Try
            Dim OBJMERGE As New MergeLedger
            OBJMERGE.MdiParent = Me
            OBJMERGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MaterialReceiptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaterialReceiptToolStripMenuItem.Click
        Try
            Dim OBJMATREC As New MatrecFilter
            OBJMATREC.MdiParent = Me
            OBJMATREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JobOutFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobOutFilterToolStripMenuItem.Click
        Try
            Dim OBJJO As New JobOutFilter
            OBJJO.MdiParent = Me
            OBJJO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRNCheckingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRNCheckingToolStripMenuItem.Click
        Try
            Dim OBJCHECK As New GRNCheckingFilter
            OBJCHECK.MdiParent = Me
            OBJCHECK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JobInFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobInFilterToolStripMenuItem.Click
        Try
            Dim OBJJI As New JobInFilter
            OBJJI.MdiParent = Me
            OBJJI.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleReturnFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleReturnFilterToolStripMenuItem.Click
        Try
            Dim OBJSR As New SaleReturnFilter
            OBJSR.MdiParent = Me
            OBJSR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PayableOutstandingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PayableOutstandingToolStripMenuItem.Click
        Try
            Dim OBJOUT As New OutstandingFilter
            OBJOUT.FRMSTRING = "PAYOUTSTANDING"
            OBJOUT.MdiParent = Me
            OBJOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GDNFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GDNFilterToolStripMenuItem.Click
        Try
            Dim OBJGDN As New GDNFilter
            OBJGDN.MdiParent = Me
            OBJGDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleInvoiceFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleInvoiceFilterToolStripMenuItem.Click
        Try
            Dim OBJINV As New SaleInvoiceFilter
            OBJINV.MdiParent = Me
            OBJINV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPENINGBALANCE.Click
        Try
            Dim OBJOP As New OpeningBalance
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OutstandingFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OUTSTANDINGREPORT_MENU.Click
        Try
            Dim OBJOP As New OutstandingFilter
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MergeParameterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MergeParameterToolStripMenuItem.Click
        Try
            Dim OBJmerger As New MergeParameter
            OBJmerger.MdiParent = Me
            OBJmerger.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UncheckedGRNToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UncheckedGRNToolStripMenuItem1.Click
        Try
            Dim OBJGRN As New GRNUnchekedReport
            OBJGRN.FRMSTRING = "UNCHECKED"
            OBJGRN.MdiParent = Me
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CheckedGRNToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckedGRNToolStripMenuItem1.Click
        Try
            Dim OBJGRN As New GRNUnchekedReport
            OBJGRN.FRMSTRING = "CHECKED"
            OBJGRN.MdiParent = Me
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PendingInvoiceToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PendingInvoiceToolStripMenuItem1.Click
        Try
            Dim OBJGRN As New GRNUnchekedReport
            OBJGRN.FRMSTRING = "PENDING"
            OBJGRN.MdiParent = Me
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseGRNToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseGRNToolStripMenuItem.Click
        Try
            Dim OBJGRN As New GRNFilter
            OBJGRN.MdiParent = Me
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgersToolStripMenuItem.Click
        Try
            Dim OBJLEDGER As New LedgerFilter
            OBJLEDGER.MdiParent = Me
            OBJLEDGER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ShortcutsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShortcutsToolStripMenuItem.Click
        Try
            Dim OBJSHORTCUT As New Shortcuts
            OBJSHORTCUT.MdiParent = Me
            OBJSHORTCUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleOrderFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleOrderFilterToolStripMenuItem.Click
        Try
            Dim OBJSO As New SOFilter
            OBJSO.FRMSTRING = "SO"
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LotStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LotStatusToolStripMenuItem.Click
        Try
            Dim OBJGDN As New LotFilter
            OBJGDN.MdiParent = Me
            OBJGDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseInvoiceToolStripMenuItem.Click
        Try
            Dim OBJPI As New PurchaseInvoiceFilter
            OBJPI.MdiParent = Me
            OBJPI.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddPackingMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PACKINGTYPEADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "PACKINGTYPE"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditPackingMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PACKINGTYPEEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.FRMSTRING = "PACKINGTYPE"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ManualMatchingDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualMatchingDetailToolStripMenuItem.Click
        Try
            Dim OBJMATCH As New ManualMatchingDetails
            OBJMATCH.MdiParent = Me
            OBJMATCH.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddManualMatchingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddManualMatchingToolStripMenuItem.Click
        Try
            Dim OBJMATCH As New ManualMatching
            OBJMATCH.MdiParent = Me
            OBJMATCH.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub QualityWiseStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QualityWiseStockToolStripMenuItem.Click
        Try
            Dim OBJDESIGN As New QualityWiseStock
            OBJDESIGN.MdiParent = Me
            OBJDESIGN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YEARADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YEARADD.Click
        Try
            Dim TEMPMSG As Integer = MsgBox("Create New Accounting Year?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                Dim obj As New YearMaster
                obj.cmdback.Visible = False
                obj.EDIT = False
                obj.FRMSTRING = "ADDYEAR"
                obj.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DataTransferToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DATATRANSFER_MASTER.Click
        Try
            Dim OBJYEAR As New YearTransfer
            OBJYEAR.FRMSTRING = "YEARTRANSFER"
            OBJYEAR.MdiParent = Me
            OBJYEAR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StockAdjustmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOCKADJUSTMENT_MASTER.Click
        'Try
        '    Dim OBJDSTOCK As New StockReco
        '    OBJDSTOCK.MdiParent = Me
        '    OBJDSTOCK.Show()
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub AddNewStockAdjustmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOCKADJUSTMENTADD.Click
        Try
            Dim OBJDSTOCK As New StockReco
            OBJDSTOCK.MdiParent = Me
            OBJDSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingAdjustmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOCKADJUSTMENTEDIT.Click
        Try
            Dim OBJDSTOCK As New StockRecoDetails
            OBJDSTOCK.MdiParent = Me
            OBJDSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TDSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDSToolStripMenuItem.Click
        Try
            Dim OBJTDS1 As New TDS
            OBJTDS1.MdiParent = Me
            OBJTDS1.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TDSCHALLAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDSCHALLAN_MASTER.Click
        Try
            Dim OBJTDS As New TDSChallan
            OBJTDS.MdiParent = Me
            OBJTDS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InterestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InterestToolStripMenuItem.Click
        Try
            Dim OBJINTCALC As New InterestCalc
            OBJINTCALC.MdiParent = Me
            OBJINTCALC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IntrestCalculatorSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IntrestCalculatorSummaryToolStripMenuItem.Click
        Try
            Dim OBJINTCALC As New InterestCalc_Summary
            OBJINTCALC.MdiParent = Me
            OBJINTCALC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PO_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PO_TOOL.Click
        Try
            If ClientName = "VAISHALI" Or ClientName = "AKASHDEEP" Then
                Dim OBJPO As New YarnPurchaseOrder
                OBJPO.MdiParent = Me
                OBJPO.Show()
            Else
                Dim OBJPO As New PurchaseOrder
                OBJPO.MdiParent = Me
                OBJPO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROGRAMADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PROGRAMADD.Click
        Try
            Dim OBJPROG As New ProgramMaster
            OBJPROG.MdiParent = Me
            OBJPROG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROGRAMEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PROGRAMEDIT.Click
        Try
            Dim OBJPROG As New ProgramDetails
            OBJPROG.MdiParent = Me
            OBJPROG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewPackingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FINALPSADD.Click
        Try
            Dim OBJPACKING As New FinalPacking
            OBJPACKING.MdiParent = Me
            OBJPACKING.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STOREINWARDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOREINWARDADD.Click
        Try
            Dim OBJSTORE As New StoreInward
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STOREINWARDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOREINWARDEDIT.Click
        Try
            Dim OBJSTORE As New StoreInwardDetails
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewItemToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOREITEMADD.Click
        Try
            Dim OBJSTORE As New StoreItemMaster
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingItemToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOREITEMEDIT.Click
        Try
            Dim OBJSTORE As New StoreItemDetails
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPENING_STORESTOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPENING_STORESTOCK.Click
        Try
            Dim OBJOP As New OpeningStoreStock
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub INHOUSECHECKINGADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INHOUSECHECKINGADD.Click
        Try
            Dim OBJCHECK As New InHouseChecking
            OBJCHECK.MdiParent = Me
            OBJCHECK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub INHOUSECHECKINGEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INHOUSECHECKINGEDIT.Click
        Try
            Dim OBJCHECK As New InHouseCheckingDetails
            OBJCHECK.MdiParent = Me
            OBJCHECK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub INHOUSECHECK_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INHOUSECHECK_TOOL.Click
        Try
            Dim OBJCHECK As New InHouseChecking
            OBJCHECK.MdiParent = Me
            OBJCHECK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STOCKTRANSFER_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOCKTRANSFER_MASTER.Click
        Try
            Dim OBJYEAR As New YearTransfer
            OBJYEAR.FRMSTRING = "STOCKTRANSFER"
            OBJYEAR.MdiParent = Me
            OBJYEAR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AdvancesSettlementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancesSettlementToolStripMenuItem.Click
        Try
            Dim OBJADV As New Adv_Receivable_settlement
            OBJADV.flag_adv_settlement = True
            OBJADV.MdiParent = Me
            OBJADV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub ReceivableSettlementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceivableSettlementToolStripMenuItem.Click
        Try
            Dim OBJADV As New Adv_Receivable_settlement
            OBJADV.flag_Rec_settlement = True
            OBJADV.MdiParent = Me
            OBJADV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReceivableOutstandingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceivableOutstandingToolStripMenuItem.Click
        Try
            Dim OBJOUT As New OutstandingFilter
            OBJOUT.FRMSTRING = "RECOUTSTANDING"
            OBJOUT.MdiParent = Me
            OBJOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FORMREPORTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FORMREPORTS.Click
        Try
            Dim OBJCFORM As New FormFilter
            OBJCFORM.MdiParent = Me
            OBJCFORM.frmstring = "CFORMFILTER"
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CFormApplicationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CFORMAPPLICATION.Click
        Try
            Dim OBJCFORM As New FormFilter
            OBJCFORM.frmstring = "CFORMAPPLICATION"
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPENINGSTOCKVALUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPENINGSTOCKVALUE.Click
        Try
            Dim OBJOP As New OpeningClosingStock
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseTaxRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TAXREGISTER_MASTER.Click
        Try
            Dim OBJTAX As New TaxFilter
            OBJTAX.MdiParent = Me
            OBJTAX.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DesignWiseStockSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DesignWiseStockSummaryToolStripMenuItem.Click
        Try
            Dim Objdyestockdetails As New DesignWiseStockSummary
            Objdyestockdetails.MdiParent = Me
            Objdyestockdetails.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ItemWiseStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemWiseStockToolStripMenuItem.Click
        Try
            Dim OBJITEM As New ItemWiseStock
            OBJITEM.MdiParent = Me
            OBJITEM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ItemWiseShadeWiseStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemWiseShadeWiseStockToolStripMenuItem.Click
        Try
            Dim OBJITEM As New ItemWiseColorWiseStock
            OBJITEM.MdiParent = Me
            OBJITEM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewSalesmanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALESMANADD.Click
        Try
            Dim OBJSAL As New SalesmanMaster
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingSalesmanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALESMANEDIT.Click
        Try
            Dim OBJSAL As New SalesmanDetails
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReceiptAdjustedReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim OBJREC As New filter
            OBJREC.MdiParent = Me
            OBJREC.frmstring = "RECEIPTMONTHLYADJ"
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PaymentAdjustedReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim OBJPAY As New filter
            OBJPAY.MdiParent = Me
            OBJPAY.frmstring = "PAYMENTMONTHLYADJ"
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TDSDeductedNotDedictedReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDSDeductedNotDedictedReportToolStripMenuItem.Click
        Try
            Dim OBJTDS As New TDSDeductedReport
            OBJTDS.MdiParent = Me
            OBJTDS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DESIGNREPLACEMENT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim OBJDES As New DesignReplacement
            OBJDES.MdiParent = Me
            OBJDES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PendingInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PendingInvoiceToolStripMenuItem.Click
        Try
            Dim OBJPENCHALLAN As New PendingChallanForInvoice
            OBJPENCHALLAN.MdiParent = Me
            OBJPENCHALLAN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLANTYPEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHALLANTYPEADD.Click
        Try
            Dim OBJTYPE As New ChallanTypeMaster
            OBJTYPE.MdiParent = Me
            OBJTYPE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLANTYPEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHALLANTYPEEDIT.Click
        Try
            Dim OBJTYPE As New ChallanTypeDetails
            OBJTYPE.MdiParent = Me
            OBJTYPE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROCESSADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PROCESSADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "PROCESS"
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PROCESSEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PROCESSEDIT.Click
        Try
            Dim objCategoryDetails As New CategoryDetails
            objCategoryDetails.MdiParent = Me
            objCategoryDetails.FRMSTRING = "PROCESS"
            objCategoryDetails.Show()
            objCategoryDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub UPLOADACCOUNTMENU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UPLOADACCOUNTMENU.Click
        Try
            If InputBox("Enter Master Password") <> "Infosys@123" Then Exit Sub

            '************************************ LEDGER UPLOAD ****************************
            'upload the files data
            ''Reading from Excel Woorkbook
            Dim cPart As Microsoft.Office.Interop.Excel.Range
            Dim oExcel As Microsoft.Office.Interop.Excel.Application = CreateObject("Excel.Application")
            Dim oBook As Microsoft.Office.Interop.Excel.Workbook = oExcel.Workbooks.Open("D:\" & InputBox("Enter File Name").ToString.Trim, , False)
            Dim oSheet As New Microsoft.Office.Interop.Excel.Worksheet
            oSheet = oBook.Worksheets("Sheet1")

            'GRID
            Dim ADDITEM As Boolean = True
            Dim TEMPITEMNAME As String = ""

            Dim DTSAVE As New System.Data.DataTable
            DTSAVE.Columns.Add("CODE")
            DTSAVE.Columns.Add("COMPANYNAME")
            DTSAVE.Columns.Add("ADD1")
            DTSAVE.Columns.Add("ADD2")
            DTSAVE.Columns.Add("ADDRESS")
            DTSAVE.Columns.Add("CITYNAME")
            DTSAVE.Columns.Add("PINNO")
            DTSAVE.Columns.Add("STATE")
            DTSAVE.Columns.Add("COUNTRY")
            DTSAVE.Columns.Add("PHONENO")
            DTSAVE.Columns.Add("MOBILENO")
            DTSAVE.Columns.Add("GSTIN")
            DTSAVE.Columns.Add("GROUPNAME")
            DTSAVE.Columns.Add("PANNO")
            DTSAVE.Columns.Add("BROKER")
            DTSAVE.Columns.Add("TRANSPORT")
            DTSAVE.Columns.Add("EMAIL")
            DTSAVE.Columns.Add("CRDAYS")
            DTSAVE.Columns.Add("SALESMAN")
            DTSAVE.Columns.Add("TDSPER")
            DTSAVE.Columns.Add("TDSSECTION")
            DTSAVE.Columns.Add("CMPNONCMP")
            DTSAVE.Columns.Add("DISCOUNT")
            DTSAVE.Columns.Add("CASHDISC")
            DTSAVE.Columns.Add("COMMISSION")
            DTSAVE.Columns.Add("SHIPPINGADD")
            DTSAVE.Columns.Add("RESINO")
            DTSAVE.Columns.Add("ALTNO")
            DTSAVE.Columns.Add("FAX")
            DTSAVE.Columns.Add("WEBSITE")
            DTSAVE.Columns.Add("REMARKS")
            DTSAVE.Columns.Add("INTPER")
            DTSAVE.Columns.Add("DELIVERYAT")
            DTSAVE.Columns.Add("DELIVERYPINCODE")
            DTSAVE.Columns.Add("KMS")
            DTSAVE.Columns.Add("BROKERAGECOMM")
            DTSAVE.Columns.Add("WHATSAPPNO")
            DTSAVE.Columns.Add("AREA")
            DTSAVE.Columns.Add("TYPE")

            Dim ARR As New ArrayList
            Dim COLIND As Integer = 0
            Dim DTROWSAVE As System.Data.DataRow = DTSAVE.NewRow()

            Dim FROMROWNO As Integer = Val(InputBox("Enter Start Row No"))
            Dim TOROWNO As Integer = Val(InputBox("Enter End Row No"))

            For I As Integer = FROMROWNO To TOROWNO

                If IsDBNull(oSheet.Range("A" & I.ToString).Text) = False Then
                    DTROWSAVE("CODE") = oSheet.Range("A" & I.ToString).Text
                Else
                    DTROWSAVE("CODE") = ""
                End If

                If IsDBNull(oSheet.Range("B" & I.ToString).Text) = False Then
                    DTROWSAVE("COMPANYNAME") = oSheet.Range("B" & I.ToString).Text
                Else
                    DTROWSAVE("COMPANYNAME") = ""
                End If

                If IsDBNull(oSheet.Range("C" & I.ToString).Text) = False Then
                    DTROWSAVE("ADD1") = oSheet.Range("C" & I.ToString).Text
                Else
                    DTROWSAVE("ADD1") = ""
                End If

                If IsDBNull(oSheet.Range("D" & I.ToString).Text) = False Then
                    DTROWSAVE("ADD2") = oSheet.Range("D" & I.ToString).Text
                Else
                    DTROWSAVE("ADD2") = ""
                End If

                If IsDBNull(oSheet.Range("E" & I.ToString).Text) = False Then
                    DTROWSAVE("ADDRESS") = oSheet.Range("E" & I.ToString).Text
                Else
                    DTROWSAVE("ADDRESS") = ""
                End If

                If IsDBNull(oSheet.Range("F" & I.ToString).Text) = False Then
                    DTROWSAVE("CITYNAME") = oSheet.Range("F" & I.ToString).Text
                Else
                    DTROWSAVE("CITYNAME") = ""
                End If

                If IsDBNull(oSheet.Range("G" & I.ToString).Text) = False Then
                    DTROWSAVE("PINNO") = oSheet.Range("G" & I.ToString).Text
                Else
                    DTROWSAVE("PINNO") = 0
                End If

                If IsDBNull(oSheet.Range("H" & I.ToString).Text) = False Then
                    DTROWSAVE("STATE") = oSheet.Range("H" & I.ToString).Text
                Else
                    DTROWSAVE("STATE") = ""
                End If

                If IsDBNull(oSheet.Range("I" & I.ToString).Text) = False Then
                    DTROWSAVE("COUNTRY") = oSheet.Range("I" & I.ToString).Text
                Else
                    DTROWSAVE("COUNTRY") = ""
                End If

                If IsDBNull(oSheet.Range("J" & I.ToString).Text) = False Then
                    DTROWSAVE("PHONENO") = oSheet.Range("J" & I.ToString).Text
                Else
                    DTROWSAVE("PHONENO") = ""
                End If

                If IsDBNull(oSheet.Range("K" & I.ToString).Text) = False Then
                    DTROWSAVE("MOBILENO") = oSheet.Range("K" & I.ToString).Text
                Else
                    DTROWSAVE("MOBILENO") = 0
                End If


                If IsDBNull(oSheet.Range("L" & I.ToString).Text) = False Then
                    DTROWSAVE("GSTIN") = oSheet.Range("L" & I.ToString).Text
                Else
                    DTROWSAVE("GSTIN") = ""
                End If

                If IsDBNull(oSheet.Range("M" & I.ToString).Text) = False Then
                    DTROWSAVE("GROUPNAME") = oSheet.Range("M" & I.ToString).Text
                Else
                    DTROWSAVE("GROUPNAME") = ""
                End If

                If IsDBNull(oSheet.Range("N" & I.ToString).Text) = False Then
                    DTROWSAVE("PANNO") = oSheet.Range("N" & I.ToString).Text
                Else
                    DTROWSAVE("PANNO") = ""
                End If

                If IsDBNull(oSheet.Range("O" & I.ToString).Text) = False Then
                    DTROWSAVE("BROKER") = oSheet.Range("O" & I.ToString).Text
                Else
                    DTROWSAVE("BROKER") = ""
                End If

                If IsDBNull(oSheet.Range("P" & I.ToString).Text) = False Then
                    DTROWSAVE("TRANSPORT") = oSheet.Range("P" & I.ToString).Text
                Else
                    DTROWSAVE("TRANSPORT") = ""
                End If

                If IsDBNull(oSheet.Range("Q" & I.ToString).Text) = False Then
                    DTROWSAVE("EMAIL") = oSheet.Range("Q" & I.ToString).Text
                Else
                    DTROWSAVE("EMAIL") = ""
                End If

                If IsDBNull(oSheet.Range("R" & I.ToString).Text) = False Then
                    DTROWSAVE("CRDAYS") = oSheet.Range("R" & I.ToString).Text
                Else
                    DTROWSAVE("CRDAYS") = ""
                End If

                If IsDBNull(oSheet.Range("S" & I.ToString).Text) = False Then
                    DTROWSAVE("SALESMAN") = oSheet.Range("S" & I.ToString).Text
                Else
                    DTROWSAVE("SALESMAN") = ""
                End If

                If IsDBNull(oSheet.Range("T" & I.ToString).Text) = False Then
                    DTROWSAVE("TDSPER") = oSheet.Range("T" & I.ToString).Text
                Else
                    DTROWSAVE("TDSPER") = ""
                End If

                If IsDBNull(oSheet.Range("U" & I.ToString).Text) = False Then
                    DTROWSAVE("TDSSECTION") = oSheet.Range("U" & I.ToString).Text
                Else
                    DTROWSAVE("TDSSECTION") = ""
                End If

                If IsDBNull(oSheet.Range("V" & I.ToString).Text) = False Then
                    DTROWSAVE("CMPNONCMP") = oSheet.Range("V" & I.ToString).Text
                Else
                    DTROWSAVE("CMPNONCMP") = ""
                End If

                If IsDBNull(oSheet.Range("W" & I.ToString).Text) = False Then
                    DTROWSAVE("DISCOUNT") = oSheet.Range("W" & I.ToString).Text
                Else
                    DTROWSAVE("DISCOUNT") = ""
                End If

                If IsDBNull(oSheet.Range("X" & I.ToString).Text) = False Then
                    DTROWSAVE("CASHDISC") = oSheet.Range("X" & I.ToString).Text
                Else
                    DTROWSAVE("CASHDISC") = ""
                End If

                If IsDBNull(oSheet.Range("Y" & I.ToString).Text) = False Then
                    DTROWSAVE("COMMISSION") = oSheet.Range("Y" & I.ToString).Text
                Else
                    DTROWSAVE("COMMISSION") = ""
                End If

                If IsDBNull(oSheet.Range("Z" & I.ToString).Text) = False Then
                    DTROWSAVE("SHIPPINGADD") = oSheet.Range("Z" & I.ToString).Text
                Else
                    DTROWSAVE("SHIPPINGADD") = ""
                End If

                If IsDBNull(oSheet.Range("AA" & I.ToString).Text) = False Then
                    DTROWSAVE("RESINO") = oSheet.Range("AA" & I.ToString).Text
                Else
                    DTROWSAVE("RESINO") = ""
                End If

                If IsDBNull(oSheet.Range("AB" & I.ToString).Text) = False Then
                    DTROWSAVE("ALTNO") = oSheet.Range("AB" & I.ToString).Text
                Else
                    DTROWSAVE("ALTNO") = ""
                End If

                If IsDBNull(oSheet.Range("AC" & I.ToString).Text) = False Then
                    DTROWSAVE("FAX") = oSheet.Range("AC" & I.ToString).Text
                Else
                    DTROWSAVE("FAX") = ""
                End If

                If IsDBNull(oSheet.Range("AD" & I.ToString).Text) = False Then
                    DTROWSAVE("WEBSITE") = oSheet.Range("AD" & I.ToString).Text
                Else
                    DTROWSAVE("WEBSITE") = ""
                End If

                If IsDBNull(oSheet.Range("AE" & I.ToString).Text) = False Then
                    DTROWSAVE("REMARKS") = oSheet.Range("AE" & I.ToString).Text
                Else
                    DTROWSAVE("REMARKS") = ""
                End If

                If IsDBNull(oSheet.Range("AF" & I.ToString).Text) = False Then
                    DTROWSAVE("INTPER") = Val(oSheet.Range("AF" & I.ToString).Text)
                Else
                    DTROWSAVE("INTPER") = 0
                End If

                If IsDBNull(oSheet.Range("AG" & I.ToString).Text) = False Then
                    DTROWSAVE("DELIVERYAT") = oSheet.Range("AG" & I.ToString).Text
                Else
                    DTROWSAVE("DELIVERYAT") = ""
                End If

                If IsDBNull(oSheet.Range("AH" & I.ToString).Text) = False Then
                    DTROWSAVE("DELIVERYPINCODE") = oSheet.Range("AH" & I.ToString).Text
                Else
                    DTROWSAVE("DELIVERYPINCODE") = ""
                End If

                If IsDBNull(oSheet.Range("AI" & I.ToString).Text) = False Then
                    DTROWSAVE("KMS") = Val(oSheet.Range("AI" & I.ToString).Text)
                Else
                    DTROWSAVE("KMS") = 0
                End If

                If IsDBNull(oSheet.Range("AJ" & I.ToString).Text) = False Then
                    DTROWSAVE("BROKERAGECOMM") = Val(oSheet.Range("AJ" & I.ToString).Text)
                Else
                    DTROWSAVE("BROKERAGECOMM") = 0
                End If

                If IsDBNull(oSheet.Range("AK" & I.ToString).Text) = False Then
                    DTROWSAVE("WHATSAPPNO") = oSheet.Range("AK" & I.ToString).Text
                Else
                    DTROWSAVE("WHATSAPPNO") = ""
                End If

                If IsDBNull(oSheet.Range("AL" & I.ToString).Text) = False Then
                    DTROWSAVE("AREA") = oSheet.Range("AL" & I.ToString).Text
                Else
                    DTROWSAVE("AREA") = ""
                End If

                If IsDBNull(oSheet.Range("AM" & I.ToString).Text) = False Then
                    DTROWSAVE("TYPE") = oSheet.Range("AM" & I.ToString).Text
                Else
                    DTROWSAVE("TYPE") = ""
                End If



                Dim ALPARAVAL As New ArrayList
                Dim OBJCMN As New ClsCommon
                Dim DTTABLE As DataTable = OBJCMN.SEARCH("CITY_ID AS CITYID", "", "CITYMASTER ", "AND CITY_NAME = '" & DTROWSAVE("CITYNAME") & "' AND CITY_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW CITYNAME
                    Dim objyearmaster As New ClsYearMaster
                    objyearmaster.savecity(DTROWSAVE("CITYNAME"), CmpId, Locationid, Userid, YearId, " and city_name = '" & DTROWSAVE("CITYNAME") & "' AND CITY_CMPID = " & CmpId & " AND CITY_LOCATIONID = " & Locationid & " AND CITY_YEARID = " & YearId)
                End If


                DTTABLE = OBJCMN.SEARCH("CITY_ID AS CITYID", "", "CITYMASTER ", "AND CITY_NAME = '" & DTROWSAVE("DELIVERYAT") & "' AND CITY_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW CITYNAME
                    Dim objyearmaster As New ClsYearMaster
                    objyearmaster.savecity(DTROWSAVE("DELIVERYAT"), CmpId, Locationid, Userid, YearId, " and city_name = '" & DTROWSAVE("DELIVERYAT") & "' AND CITY_CMPID = " & CmpId & " AND CITY_LOCATIONID = " & Locationid & " AND CITY_YEARID = " & YearId)
                End If


                DTTABLE = OBJCMN.SEARCH("AREA_ID AS AREAID", "", "AREAMASTER ", "AND AREA_NAME = '" & DTROWSAVE("AREA") & "' AND AREA_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW AREA
                    Dim objyearmaster As New ClsYearMaster
                    objyearmaster.savearea(DTROWSAVE("AREA"), CmpId, Locationid, Userid, YearId, " and AREA_name = '" & DTROWSAVE("AREA") & "' AND AREA_CMPID = " & CmpId & " AND AREA_LOCATIONID = " & Locationid & " AND AREA_YEARID = " & YearId)
                End If


                DTTABLE = OBJCMN.SEARCH("STATE_ID AS STATEID", "", "STATEMASTER ", "AND STATE_NAME = '" & DTROWSAVE("STATE") & "' AND STATE_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW STATE
                    Dim objyearmaster As New ClsYearMaster
                    objyearmaster.savestate(DTROWSAVE("STATE"), CmpId, Locationid, Userid, YearId, " and STATE_name = '" & DTROWSAVE("STATE") & "' AND STATE_YEARID = " & YearId)
                End If


                DTTABLE = OBJCMN.SEARCH("COUNTRY_ID AS COUNTRYID", "", "COUNTRYMASTER ", "AND COUNTRY_NAME = '" & DTROWSAVE("COUNTRY") & "' AND COUNTRY_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW COUNTRY
                    Dim objyearmaster As New ClsYearMaster
                    objyearmaster.savecountry(DTROWSAVE("COUNTRY"), CmpId, Locationid, Userid, YearId, " and COUNTRY_name = '" & DTROWSAVE("COUNTRY") & "' AND COUNTRY_YEARID = " & YearId)
                End If


                'check whether ITEMNAME is already present or not
                DTTABLE = OBJCMN.SEARCH("ACC_CMPNAME AS COMPANYNAME", "", "LEDGERS ", " AND ACC_CMPNAME = '" & DTROWSAVE("COMPANYNAME") & "' AND ACC_YEARID = " & YearId)
                If DTTABLE.Rows.Count > 0 Then GoTo SKIPLINE



                'ADD IN ACCOUNTSMASTER
                ALPARAVAL.Clear()
                Dim OBJSM As New ClsAccountsMaster

                ALPARAVAL.Add(DTROWSAVE("COMPANYNAME"))
                ALPARAVAL.Add("")   'NAME
                ALPARAVAL.Add(DTROWSAVE("GROUPNAME"))
                ALPARAVAL.Add(0)    'OPBAL
                ALPARAVAL.Add("Cr.")
                ALPARAVAL.Add(Val(DTROWSAVE("INTPER")))    'INTPER
                ALPARAVAL.Add(0)    'PROFITPER
                ALPARAVAL.Add(DTROWSAVE("ADD1"))
                ALPARAVAL.Add(DTROWSAVE("ADD2"))
                ALPARAVAL.Add(DTROWSAVE("AREA"))   'AREA
                ALPARAVAL.Add("")   'STD
                ALPARAVAL.Add(DTROWSAVE("CITYNAME"))
                ALPARAVAL.Add(DTROWSAVE("PINNO"))
                ALPARAVAL.Add(DTROWSAVE("STATE"))
                ALPARAVAL.Add(DTROWSAVE("COUNTRY"))
                ALPARAVAL.Add(Val(DTROWSAVE("CRDAYS")))
                ALPARAVAL.Add(0)    'CRLIMIT
                ALPARAVAL.Add(DTROWSAVE("RESINO"))   'RESI
                ALPARAVAL.Add(DTROWSAVE("ALTNO"))   'ALT
                ALPARAVAL.Add(DTROWSAVE("PHONENO"))
                ALPARAVAL.Add(DTROWSAVE("MOBILENO"))
                ALPARAVAL.Add(DTROWSAVE("WHATSAPPNO"))   'WHATSAPPNO
                ALPARAVAL.Add(DTROWSAVE("FAX"))   'FAX
                ALPARAVAL.Add(DTROWSAVE("WEBSITE"))   'WEBSITE
                ALPARAVAL.Add(DTROWSAVE("EMAIL"))   'EMAIL

                ALPARAVAL.Add(DTROWSAVE("TRANSPORT"))   'TRANS
                ALPARAVAL.Add(DTROWSAVE("BROKER"))   'AGENT
                ALPARAVAL.Add(Val(DTROWSAVE("COMMISSION")))    'AGENTCOM
                ALPARAVAL.Add(Val(DTROWSAVE("DISCOUNT")))    'DISC
                ALPARAVAL.Add(Val(DTROWSAVE("CASHDISC")))    'CDPER
                ALPARAVAL.Add(Val(DTROWSAVE("KMS")))    'KMS

                ALPARAVAL.Add(DTROWSAVE("PANNO"))   'PAN
                ALPARAVAL.Add("")   'EXISE
                ALPARAVAL.Add("")   'RANGE
                ALPARAVAL.Add("")   'ADDLESS
                ALPARAVAL.Add("")   'CST
                ALPARAVAL.Add("")   'TIN
                ALPARAVAL.Add("")   'ST
                ALPARAVAL.Add("")   'VAT
                ALPARAVAL.Add(DTROWSAVE("GSTIN"))
                ALPARAVAL.Add("")   'REGISTER
                ALPARAVAL.Add(DTROWSAVE("ADDRESS"))
                ALPARAVAL.Add(DTROWSAVE("SHIPPINGADD"))   'SHIPADD
                ALPARAVAL.Add(DTROWSAVE("REMARKS"))   'REMARKS
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
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                ALPARAVAL.Add(0)    'TRANSFER
                ALPARAVAL.Add(DTROWSAVE("CODE"))
                ALPARAVAL.Add("")    'PRICELIST
                ALPARAVAL.Add("")    'PACKINGTYPE
                ALPARAVAL.Add("")    'TERM
                ALPARAVAL.Add(DTROWSAVE("SALESMAN"))    'SALESMAN
                ALPARAVAL.Add(DTROWSAVE("CITYNAME"))    'DELIVERYAT (SAME AS CITY WHILE UPLOADING)



                'TDS
                '*******************************
                ALPARAVAL.Add(0)    'ISTDS
                ALPARAVAL.Add("")   'DEDUCTEETYPER
                ALPARAVAL.Add("")   'TDSFORM
                ALPARAVAL.Add("")   'TDSCOMPANY
                ALPARAVAL.Add(0)    'ISLOWER

                ALPARAVAL.Add(DTROWSAVE("TDSSECTION"))   'SECTION
                ALPARAVAL.Add(Val(0))   'TDSRATE
                ALPARAVAL.Add(Val(DTROWSAVE("TDSPER")))    'TDSPER
                ALPARAVAL.Add(0) 'SURCHARGE
                ALPARAVAL.Add(0) 'LIMIT
                '*******************************

                ALPARAVAL.Add(0)    'TDSAC
                ALPARAVAL.Add("NON SEZ")    'SEZTYPE
                ALPARAVAL.Add(DTROWSAVE("CMPNONCMP"))   'NATUREOFPAY
                If DTROWSAVE("TYPE") <> "" Then ALPARAVAL.Add(DTROWSAVE("TYPE")) Else ALPARAVAL.Add("ACCOUNTS")   'TYPE
                ALPARAVAL.Add("")   'CALC
                ALPARAVAL.Add(0)                        'POMNADTE
                ALPARAVAL.Add(DTROWSAVE("PINNO"))       'DELIVERYPINCODE (SAME AS PINCODE WHILE UPLOADING)
                ALPARAVAL.Add("")   'UPI
                ALPARAVAL.Add("")   'MSME
                ALPARAVAL.Add(0)    'TCS
                ALPARAVAL.Add("")   'TDSDEDUCTEDAC
                ALPARAVAL.Add(0)    'TDSONGTOTAL
                ALPARAVAL.Add(Val(DTROWSAVE("BROKERAGECOMM")))    'COMMISSION
                ALPARAVAL.Add("")   'DISTRICT
                ALPARAVAL.Add("")   'WARNING TEXT
                ALPARAVAL.Add(0)   'GSTINVERIFIED
                ALPARAVAL.Add(0)   'PARTYTDS
                ALPARAVAL.Add(0)   'RD
                ALPARAVAL.Add("")   'MSME TYPE
                ALPARAVAL.Add(0)   'EXMILL
                ALPARAVAL.Add(0)   'BILLTOID
                ALPARAVAL.Add(0)   'LOCKDAYS

                'CONTACT DETAILS
                '*******************************
                ALPARAVAL.Add("")   'FOR NAME
                ALPARAVAL.Add(0)   'FOR DESIGNATION
                ALPARAVAL.Add("")   'FOR MOBILE
                ALPARAVAL.Add("")   'FOR EMAIL



                OBJSM.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJSM.SAVE()

                DTROWSAVE = DTSAVE.NewRow()

SKIPLINE:
            Next

            oBook.Close()

            Exit Sub

            '************************************ END OF CODE FOR LEDGER UPLOAD ****************************



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GODOWNADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GODOWNADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "GODOWN"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GODOWNEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GODOWNEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.FRMSTRING = "GODOWN"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub RACKADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RACKADD.Click
        Try
            Dim OBJRACK As New RackMaster
            OBJRACK.MdiParent = Me
            OBJRACK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RACKEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RACKEDIT.Click
        Try
            Dim OBJRACK As New RackDetails
            OBJRACK.MdiParent = Me
            OBJRACK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SHELFADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SHELFADD.Click
        Try
            Dim OBJSHELF As New ShelfMaster
            OBJSHELF.MdiParent = Me
            OBJSHELF.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SHELFEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SHELFEDIT.Click
        Try
            Dim OBJSHELF As New ShelfDetails
            OBJSHELF.MdiParent = Me
            OBJSHELF.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UploadLotNoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadLotNoToolStripMenuItem.Click
        Try
            Dim OBJ As New UpdateLotNo
            OBJ.MdiParent = Me
            OBJ.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SCHEDULE_ADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SCHEDULEADD.Click
        Try
            Dim OBJ As New ScheduleMaster
            OBJ.MdiParent = Me
            OBJ.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SCHEDULE_EDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SCHEDULEEDIT.Click
        Try
            Dim OBJ As New ScheduleMasterDetails
            OBJ.MdiParent = Me
            OBJ.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSOADD.Click
        Try
            Dim OBJOPSO As New OpeningSaleOrder
            OBJOPSO.MdiParent = Me
            OBJOPSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingOpeningSaleOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSOEDIT.Click
        Try
            Dim OBJOPSO As New OpeningSaleOrderDetails
            OBJOPSO.MdiParent = Me
            OBJOPSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReconcileDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECODATA_MASTER.Click
        Try
            Dim OBJRECO As New ReconcileData
            OBJRECO.MdiParent = Me
            OBJRECO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TRANSCHALLANADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TRANSCHALLANADD.Click
        Try
            Dim OBJTRANS As New TransportChallan
            OBJTRANS.MdiParent = Me
            OBJTRANS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TRANSCHALLANEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TRANSCHALLANEDIT.Click
        Try
            Dim OBJTRANS As New TransportChallanDetails
            OBJTRANS.MdiParent = Me
            OBJTRANS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HSNADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HSNADD.Click
        Try
            Dim OBJHSN As New HSNMaster
            OBJHSN.MdiParent = Me
            OBJHSN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HSNEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HSNEDIT.Click
        Try
            Dim OBJHSN As New HSNDetails
            OBJHSN.MdiParent = Me
            OBJHSN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ISSUEPACKINGADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ISSUEPACKINGADD.Click
        Try
            Dim OBJISSUE As New IssueToPacking
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ISSUEPACKINGEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ISSUEPACKINGEDIT.Click
        Try
            Dim OBJISSUE As New IssueToPackingDetails
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECPACKINGADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECPACKINGADD.Click
        Try
            Dim OBJREC As New RecFromPacking
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECPACKINGEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECPACKINGEDIT.Click
        Try
            Dim OBJREC As New RecFromPackingDetails
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ISSUEPACKING_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ISSUEPACKING_TOOL.Click
        Try
            Dim OBJISSUE As New IssueToPacking
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECPACKING_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECPACKING_TOOL.Click
        Try
            Dim OBJREC As New RecFromPacking
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PRICELIST_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRICELIST_MASTER.Click
        Try
            Dim OBJPL As New PriceList
            OBJPL.MdiParent = Me
            OBJPL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GSTTAXFILTER_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GSTTAXFILTER_MASTER.Click
        Try
            Dim OBJTAX As New GSTTaxFilter
            OBJTAX.MdiParent = Me
            OBJTAX.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DefaultRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultRegisterToolStripMenuItem.Click
        Try
            Dim objCategory As New DefaultRegister
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BLOCKUSER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BLOCKUSER.Click
        Try
            Dim OBJUSER As New BlockUser
            OBJUSER.MdiParent = Me
            OBJUSER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub USERTRANSFER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles USERTRANSFER.Click
        Try
            Dim OBJYEAR As New YearTransfer
            OBJYEAR.FRMSTRING = "USERTRANSFER"
            OBJYEAR.MdiParent = Me
            OBJYEAR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DefaultTypeRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultTypeRegisterToolStripMenuItem.Click
        Try
            Dim objCategory As New DefaultScreentype
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MILLADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MILLADD.Click
        Try
            Dim objCategory As New MillMaster
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MILLEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MILLEDIT.Click
        Try
            Dim objCategory As New MillDetails
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNADD.Click
        Try
            Dim objCategory As New YarnQualityMaster
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNEDIT.Click
        Try
            Dim objCategory As New YarnQualityDetails
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WEFTADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WEFTADD.Click
        Try
            Dim objCategory As New WeftIssue
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JobOutDetailReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobOutDetailReportToolStripMenuItem.Click
        Try
            Dim objjo As New JobOutDetailReport
            objjo.MdiParent = Me
            objjo.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WEFTEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WEFTEDIT.Click
        Try
            Dim objjo As New WeftIssueDetails
            objjo.MdiParent = Me
            objjo.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRECDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRECDADD.Click
        Try
            Dim objyarnrecd As New YarnRecd
            objyarnrecd.MdiParent = Me
            objyarnrecd.FRMSTRING = "YARNRECD"
            objyarnrecd.TYPE = "FINISH"
            objyarnrecd.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BEAMISSUEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEAMISSUEADD.Click
        Try
            Dim objjo As New BeamIssue
            objjo.MdiParent = Me
            objjo.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BEAMISSUEEDIT_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEAMISSUEEDIT.Click
        Try
            Dim objjo As New BeamIssueDetails
            objjo.MdiParent = Me
            objjo.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MACHINEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MACHINEADD.Click
        Try
            Dim objMACHINE As New MachineMaster
            objMACHINE.MdiParent = Me
            objMACHINE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MACHINEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MACHINEEDIT.Click
        Try
            Dim objMACHINE As New MachineDetails
            objMACHINE.MdiParent = Me
            objMACHINE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRECDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRECDEDIT.Click
        Try
            Dim objyarnRECDTLS As New YarnRecdDetails
            objyarnRECDTLS.MdiParent = Me
            objyarnRECDTLS.FRMSTRING = "YARNRECD"
            objyarnRECDTLS.TYPE = "FINISH"
            objyarnRECDTLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STOCKREPORTS_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOCKREPORTS_TOOL.Click
        Try
            Dim OBJSTOCK As New StockFilter
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YISSUEJOBBERADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUEJOBBERADD.Click
        Try
            Dim objyarnISSUE As New YarnIssue
            objyarnISSUE.MdiParent = Me
            objyarnISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YISSUEJOBBEREDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUEJOBBEREDIT.Click
        Try
            Dim objyarnDYETLS As New YarnIssueDetails
            objyarnDYETLS.MdiParent = Me
            objyarnDYETLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YISSUEKNITTINGADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUEKNITTINGADD.Click
        Try
            Dim objyarnISSUE As New YarnIssueKnitting
            objyarnISSUE.MdiParent = Me
            objyarnISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YISSUEKNITTINGEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUEKNITTINGEDIT.Click
        Try
            Dim objyarnISSUEDTLS As New YarnIssueKnittingDetails
            objyarnISSUEDTLS.MdiParent = Me
            'objyarnISSUEDTLS.FRMSTRING = "YARNISSUETOKNITTING"
            objyarnISSUEDTLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DesignProcessWiseRateChartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DESIGNPROCESSWISERATECHART.Click
        Try
            Dim objdes As New DesignProcessWiseRateChart
            objdes.MdiParent = Me
            objdes.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MDIMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "MNARESH" Or ClientName = "INDRAPUJAFABRICS" Then
                NOUSE_MASTER.Visible = True
                NOUSE_MASTER.Enabled = True
            End If
            If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then PRICELIST_MASTER.ShortcutKeys = Keys.Alt Or Keys.Control Or Keys.L
            If ClientName = "YASHVI" Then STOCKADJUSTMENT_MASTER.Text = "Item Correction"
            If ClientName = "MANIBHADRA" Or ClientName = "GELATO" Or ClientName = "INDRANI" Or ClientName = "MNIKHIL" Or ClientName = "HRITI" Or ClientName = "KREEVE" Or ClientName = "MVIKASKUMAR" Or ClientName = "KRFABRICS" Or ClientName = "AMAN" Or ClientName = "BALAJI" Or ClientName = "CHINTAN" Or ClientName = "SSC" Or ClientName = "SST" Or ClientName = "MBB" Or ClientName = "VINIT" Or ClientName = "MANISH" Or ClientName = "TINUMINU" Or ClientName = "RADHA" Or ClientName = "VINTAGEINDIA" Or ClientName = "BIGAPPLE" Or HIDEGREY = True Then GRNGREY_TOOL.Text = "Finish Inward"
            If ClientName = "MOHAN" Then
                PRODUCTIONFLOOR_MASTER.Visible = True
                PRODUCTIONFLOORRETURN_MASTER.Visible = True
                YARNISSUEKNITTING_MASTER.Visible = True
                GREYRECDPRODUCTION_MASTER.Visible = True
            End If
            If ClientName <> "INDRANI" Then SOREPORT_MASTER.Visible = False

            If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                DESIGNPROCESSWISERATECHART.Visible = True
                DESTOOL.Visible = True
                COSTREPORT_MASTER.Visible = True
                TOOLCOSTREPORT.Visible = True
            End If

            If HIDEYARN = False Then
                YARNSTOCK_GODOWN.Visible = True
                YARNSTOCKGODOWN_TOOL.Visible = True
                YARNSTOCK_JOBBER.Visible = True
                YARNSTOCKJOBBER_TOOL.Visible = True
                YARNRECDJOBBER_MASTER.Visible = True
                YARNRECDJOBBERTOOL.Visible = True
            End If

            If HIDETALLYDATAEXPORT = False Then
                TALLYEXPORT_MAIN.Enabled = True
            End If

            If ITEMCOSTCENTRE = True Then
                STOCKONHANDSUMMCOSTMENU.Visible = True
                COSTCENTER_MASTER.Visible = True
            End If

            If ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                JOBOUTTYPE_MASTER.Visible = True
                JOBOUTTYPE_TOOL.Visible = True
            End If


            If HIDECATALOG = False Then
                CATALOGUE_MASTER.Enabled = True
                CATALOG_REPORTS.Enabled = True
                ITEMDESIGNIMAGE_MASTER.Enabled = True
            End If

            If ClientName = "AVIS" Then
                EXPORTLABEL_MASTER.Visible = True
            End If

            If ClientName = "MARKIN" Or ClientName = "KARAN" Then
                GREYRECDKNITTING_MASTER.Text = "Grey Recd From Jobber"
                YARNDYEINGPROGRAM_MASTER.Visible = True
            End If


            If ClientName = "VAISHALI" Or ClientName = "AKASHDEEP" Then
                TRANSACTION_MENU.Visible = False
                If ClientName = "VAISHALI" Then
                    GreyReceived_MENU.Visible = False
                    GREYRECDTOOLSTRIP.Visible = False
                    YARNRECDGODOWN_MASTER.Visible = True
                End If
                YARNDYEINGPROGRAM_MASTER.Visible = True
            End If

            If ClientName = "AMAN" Then
                RECOUT_TOOL.Visible = False
                RECOUT__TOOLSTRIP.Visible = False
                PAYOUT_TOOL.Visible = False
                PAYOUT_TOOLSTRIP.Visible = False
            End If

            If ClientName = "VINTAGEINDIA" Then
                GRN_MASTER.Text = "Inward"
                CONTRACTOR_MASTER.Text = "Operator Master"
                CONTRACTORADD.Text = "Add New Operator"
                CONTRACTOREDIT.Text = "Edit Existing Operator"
                GODOWNCHANGE_MASTER.Visible = True
                AUTODESIGNCREATION_MASTER.Visible = True
            End If

            If ClientName = "SNCM" Then
                GREYISSUEPROCESS_MASTER.Text = "Grey Rec In Godown"
                GRNGREY_TOOL.Text = "Grey Iss Process"
                GRNGREY_MASTER.Text = "Grey Issue To Process"
            End If

            If ClientName = "VINTAGEINDIA" Then
                GODOWNCHANGE_MASTER.Enabled = True
            Else
                GODOWNCHANGE_MASTER.Enabled = False
            End If

            If ClientName = "MASHOK" Or ClientName = "ABHEE" Then MASHOK_REPORTS.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNSTOCK_GODOWN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNSTOCK_GODOWN.Click
        Try
            Dim OBJ As New OpeningStockYarn
            OBJ.FRMSTRING = "GODOWNSTOCKYARN"
            OBJ.MdiParent = Me
            OBJ.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNSTOCK_JOBBER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNSTOCK_JOBBER.Click
        Try
            Dim OBJ As New OpeningStockYarn
            OBJ.FRMSTRING = "JOBBERSTOCKYARN"
            OBJ.MdiParent = Me
            OBJ.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PRODISSADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRODISSADD.Click
        Try
            Dim OBJPROD As New ProductionIssue
            OBJPROD.MdiParent = Me
            OBJPROD.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PRODISSEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRODISSEDIT.Click
        Try
            Dim OBJPROD As New ProductionIssueDetails
            OBJPROD.MdiParent = Me
            OBJPROD.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRECDJOBBERADD.Click
        Try
            Dim OBJYARN As New YarnRecdFromJobber
            OBJYARN.MdiParent = Me
            OBJYARN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRECDJOBBEREDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRECDJOBBEREDIT.Click
        Try
            Dim OBJYARN As New YarnRecdFromJobberDetails
            OBJYARN.MdiParent = Me
            OBJYARN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECDJOBBERADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYRECDJOBBERADD.Click
        Try
            Dim OBJGREY As New GreyRecdFromJobber
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECDJOBBEREDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYRECDJOBBEREDIT.Click
        Try
            Dim OBJGREY As New GreyRecdFromJobberDetails
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECDKNITTINGADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYRECDKNITTINGADD.Click
        Try
            Dim OBJGREY As New GreyRecdKnitting
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECDKNITTINGEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYRECDKNITTINGEDIT.Click
        Try
            Dim OBJGREY As New GreyRecdKnittingDetails
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECDPRODUCTIONADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYRECDPRODUCTIONADD.Click
        Try
            Dim OBJGREY As New GreyRecdProduction
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECDPRODUCTIONEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYRECDPRODUCTIONEDIT.Click
        Try
            Dim OBJGREY As New GreyRecdProductionDetails
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JOBOUTTYPE_ADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JOBOUTTYPEADD.Click
        Try
            Dim OBJJO As New JobOutTypeMaster
            OBJJO.MdiParent = Me
            OBJJO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JOBOUTTYPE_EDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JOBOUTTYPEEDIT.Click
        Try
            Dim OBJJO As New JobOutTypeDetails
            OBJJO.MdiParent = Me
            OBJJO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub COSTREPORT_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COSTREPORT_MASTER.Click
        Try
            Dim OBJCOST As New CCCostReport
            OBJCOST.MdiParent = Me
            OBJCOST.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub QualityWiseShadeWiseStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QualityWiseShadeWiseStockToolStripMenuItem.Click
        Try
            Dim OBJSTOCK As New QualityWiseColorWiseStock
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PRODRETADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRODRETADD.Click
        Try
            Dim OBJPRET As New ProductionFloorReturn
            OBJPRET.MdiParent = Me
            OBJPRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PRODRETURNEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRODRETURNEDIT.Click
        Try
            Dim OBJPRET As New ProductionFloorReturnDetails
            OBJPRET.MdiParent = Me
            OBJPRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GODOWNYARNWASTAGEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GODOWNYARNWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New YarnWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEGODOWN"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GODOWNYARNWASTAGEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GODOWNYARNWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New YarnWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEGODOWN"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JOBBERYARNWASTAGEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JOBBERYARNWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New YarnWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEJOBBER"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JOBBERYARNWASTAGEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JOBBERYARNWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New YarnWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEJOBBER"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNSTOCKFILTER_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNSTOCKFILTER_MASTER.Click
        Try
            Dim OBJSTOCK As New YarnStockFilter
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRETURNPURCHASEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNPURCHASEADD.Click
        Try
            Dim OBJYARNRET As New YarnReturnPurchase
            OBJYARNRET.MdiParent = Me
            OBJYARNRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRETURNPURCHASEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNPURCHASEEDIT.Click
        Try
            Dim OBJYARNRET As New YarnReturnPurchaseDetails
            OBJYARNRET.MdiParent = Me
            OBJYARNRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRETURNKNITTINGADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNKNITTINGADD.Click
        Try
            Dim OBJYARNRET As New YarnReturnKnitting
            OBJYARNRET.MdiParent = Me
            OBJYARNRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRETURNKNITTINGEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNKNITTINGEDIT.Click
        Try
            Dim OBJYARNRET As New YarnReturnKnittingDetails
            OBJYARNRET.MdiParent = Me
            OBJYARNRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JOBBERYARNSTOCKFILTER_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JOBBERYARNSTOCKFILTER_MASTER.Click
        Try
            Dim OBJSTOCK As New JobberYarnStockFilter
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InHousePackingStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InHousePackingStockToolStripMenuItem.Click
        Try
            Dim OBJSTOCK As New InHousePackingStock
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.FRMSTRING = "DETAILS"
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PRODYARNSTOCKFILTER_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRODYARNSTOCKFILTER_MASTER.Click
        Try
            Dim OBJSTOCK As New ProductionYarnStockFilter
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PARTYITEMWISECHART_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PARTYITEMWISESTAMPING.Click
        Try
            Dim objdes As New PartyItemWiseStamping
            objdes.MdiParent = Me
            objdes.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PAYOUTSTANDING_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAYOUTSTANDING_MASTER.Click
        Try
            Dim OBJPAY As New PayOutstanding
            OBJPAY.MdiParent = Me
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECOUTSTANDING_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECOUTSTANDING_MASTER.Click
        Try
            Dim OBJREC As New RecOutstanding
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DAILYGREYSTOCKADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DAILYGREYSTOCKADD.Click
        Try
            Dim OBJGSTOCK As New DailyGreyStock
            OBJGSTOCK.MdiParent = Me
            OBJGSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DAILYGREYSTOCKEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DAILYGREYSTOCKEDIT.Click
        Try
            Dim OBJGSTOCK As New DailyGreyStockDetails
            OBJGSTOCK.MdiParent = Me
            OBJGSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolstripHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLHOME.Click
        Try
            Dim objhp As New HomePage
            objhp.MdiParent = Me
            objhp.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ADDNEWTERM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ADDNEWTERM.Click
        Try
            Dim objhp As New TermMaster
            objhp.MdiParent = Me
            objhp.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ADDNEWGROUPCOMPANY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ADDNEWGROUPCOMPANY.Click
        Try
            Dim objhp As New GroupOfCompanies
            objhp.MdiParent = Me
            objhp.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EDITEXISTINGTERM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EDITEXISTINGTERM.Click
        Try
            Dim objhp As New TermDetails
            objhp.MdiParent = Me
            objhp.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EDITGROUPCOMPANY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EDITGROUPCOMPANY.Click
        Try
            Dim objhp As New GroupOfCompaniesDetails
            objhp.MdiParent = Me
            objhp.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ITEMPRICELIST_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ITEMPRICELIST_MASTER.Click
        Try
            Dim objhp As New ItemPriceList
            objhp.MdiParent = Me
            objhp.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RATETYPE_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RATETYPE_MASTER.Click
        Try
            Dim OBJRATE As New RateTypeMaster
            OBJRATE.MdiParent = Me
            OBJRATE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SOCLOSE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SOCLOSE.Click
        Try
            Dim OBJSO As New SaleOrderClose
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SPECIALRIGHTS_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPECIALRIGHTS_MASTER.Click
        Try
            Dim OBJSO As New SpecialRights
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHQENTADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHQENTADD.Click
        Try
            Dim OBJSO As New ChqEnteries
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHQENTEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHQENTEDIT.Click
        Try
            Dim OBJSO As New ChqEnteriesDetail
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UPLOADITEMMENU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UPLOADITEMMENU.Click

        Try
            If InputBox("Enter Master Password") <> "Infosys@123" Then Exit Sub
            '************************************ ITEM UPLOAD ****************************
            'upload the files data
            ''Reading from Excel Woorkbook
            Dim cPart As Microsoft.Office.Interop.Excel.Range
            Dim oExcel As Microsoft.Office.Interop.Excel.Application = CreateObject("Excel.Application")
            Dim oBook As Microsoft.Office.Interop.Excel.Workbook = oExcel.Workbooks.Open("D:\" & InputBox("Enter File Name").ToString.Trim, , False)
            Dim oSheet As New Microsoft.Office.Interop.Excel.Worksheet
            oSheet = oBook.Worksheets("Sheet1")

            'GRID
            Dim ADDITEM As Boolean = True
            Dim TEMPITEMNAME As String = ""

            Dim DTSAVE As New System.Data.DataTable
            DTSAVE.Columns.Add("ITEMNAME")
            DTSAVE.Columns.Add("SELVEDGE")
            DTSAVE.Columns.Add("REMARKS")
            DTSAVE.Columns.Add("WIDTH")
            DTSAVE.Columns.Add("HSNCODE")
            DTSAVE.Columns.Add("CATEGORY")
            DTSAVE.Columns.Add("RATE")
            DTSAVE.Columns.Add("VALUATIONRATE")
            DTSAVE.Columns.Add("GREYCATEGORY")
            DTSAVE.Columns.Add("UNIT")
            DTSAVE.Columns.Add("CGSTPER")
            DTSAVE.Columns.Add("SGSTPER")
            DTSAVE.Columns.Add("IGSTPER")


            Dim ARR As New ArrayList
            Dim COLIND As Integer = 0
            Dim DTROWSAVE As System.Data.DataRow = DTSAVE.NewRow()

            Dim FROMROWNO As Integer = Val(InputBox("Enter Start Row No"))
            Dim TOROWNO As Integer = Val(InputBox("Enter End Row No"))

            For I As Integer = FROMROWNO To TOROWNO

                If IsDBNull(oSheet.Range("A" & I.ToString).Text) = False Then
                    DTROWSAVE("ITEMNAME") = oSheet.Range("A" & I.ToString).Text
                Else
                    DTROWSAVE("ITEMNAME") = ""
                End If

                If IsDBNull(oSheet.Range("B" & I.ToString).Text) = False Then
                    DTROWSAVE("SELVEDGE") = oSheet.Range("B" & I.ToString).Text
                Else
                    DTROWSAVE("SELVEDGE") = ""
                End If

                If IsDBNull(oSheet.Range("C" & I.ToString).Text) = False Then
                    DTROWSAVE("REMARKS") = oSheet.Range("C" & I.ToString).Text
                Else
                    DTROWSAVE("REMARKS") = ""
                End If

                If IsDBNull(oSheet.Range("D" & I.ToString).Text) = False Then
                    DTROWSAVE("WIDTH") = oSheet.Range("D" & I.ToString).Text
                Else
                    DTROWSAVE("WIDTH") = ""
                End If

                If IsDBNull(oSheet.Range("E" & I.ToString).Text) = False Then
                    DTROWSAVE("HSNCODE") = oSheet.Range("E" & I.ToString).Text
                Else
                    DTROWSAVE("HSNCODE") = ""
                End If


                If IsDBNull(oSheet.Range("F" & I.ToString).Text) = False Then
                    DTROWSAVE("CATEGORY") = oSheet.Range("F" & I.ToString).Text
                Else
                    DTROWSAVE("CATEGORY") = ""
                End If


                If IsDBNull(oSheet.Range("G" & I.ToString).Text) = False Then
                    DTROWSAVE("RATE") = Val(oSheet.Range("G" & I.ToString).Text)
                Else
                    DTROWSAVE("RATE") = 0
                End If

                If IsDBNull(oSheet.Range("H" & I.ToString).Text) = False Then
                    DTROWSAVE("VALUATIONRATE") = Val(oSheet.Range("H" & I.ToString).Text)
                Else
                    DTROWSAVE("VALUATIONRATE") = 0
                End If

                If IsDBNull(oSheet.Range("I" & I.ToString).Text) = False Then
                    DTROWSAVE("GREYCATEGORY") = oSheet.Range("I" & I.ToString).Text
                Else
                    DTROWSAVE("GREYCATEGORY") = 0
                End If

                If IsDBNull(oSheet.Range("J" & I.ToString).Text) = False Then
                    DTROWSAVE("UNIT") = oSheet.Range("J" & I.ToString).Text
                Else
                    DTROWSAVE("UNIT") = 0
                End If

                If IsDBNull(oSheet.Range("K" & I.ToString).Text) = False Then
                    DTROWSAVE("CGSTPER") = Val(oSheet.Range("K" & I.ToString).Text)
                Else
                    DTROWSAVE("CGSTPER") = 0
                End If

                If IsDBNull(oSheet.Range("L" & I.ToString).Text) = False Then
                    DTROWSAVE("SGSTPER") = Val(oSheet.Range("L" & I.ToString).Text)
                Else
                    DTROWSAVE("SGSTPER") = 0
                End If

                If IsDBNull(oSheet.Range("M" & I.ToString).Text) = False Then
                    DTROWSAVE("IGSTPER") = Val(oSheet.Range("M" & I.ToString).Text)
                Else
                    DTROWSAVE("IGSTPER") = 0
                End If


                'check whether ITEMNAME is already present or not
                Dim OBJCMN As New ClsCommon
                Dim DTTABLE As DataTable = OBJCMN.SEARCH("ITEM_NAME AS ITEMNAME", "", "ITEMMASTER", " AND ITEM_NAME = '" & DTROWSAVE("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                If DTTABLE.Rows.Count > 0 Then GoTo SKIPLINE



                Dim ALPARAVAL As New ArrayList
                DTTABLE = OBJCMN.SEARCH("CATEGORY_ID AS CATEGORYID", "", "CATEGORYMASTER", "AND CATEGORY_NAME = '" & DTROWSAVE("CATEGORY") & "' AND CATEGORY_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW CATEGORY
                    Dim OBJCATEGORY As New ClsCategoryMaster
                    OBJCATEGORY.alParaval.Add(DTROWSAVE("CATEGORY"))
                    OBJCATEGORY.alParaval.Add("")
                    OBJCATEGORY.alParaval.Add(CmpId)
                    OBJCATEGORY.alParaval.Add(0)
                    OBJCATEGORY.alParaval.Add(Userid)
                    OBJCATEGORY.alParaval.Add(YearId)
                    OBJCATEGORY.alParaval.Add(0)
                    Dim INTRESCAT As Integer = OBJCATEGORY.save()
                End If


                'GREY CATEGORY
                DTTABLE = OBJCMN.SEARCH("CATEGORY_ID AS CATEGORYID", "", "CATEGORYMASTER", "AND CATEGORY_NAME = '" & DTROWSAVE("GREYCATEGORY") & "' AND CATEGORY_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW CATEGORY
                    Dim OBJCATEGORY As New ClsCategoryMaster
                    OBJCATEGORY.alParaval.Add(DTROWSAVE("GREYCATEGORY"))
                    OBJCATEGORY.alParaval.Add("")
                    OBJCATEGORY.alParaval.Add(CmpId)
                    OBJCATEGORY.alParaval.Add(0)
                    OBJCATEGORY.alParaval.Add(Userid)
                    OBJCATEGORY.alParaval.Add(YearId)
                    OBJCATEGORY.alParaval.Add(0)
                    Dim INTRESCAT As Integer = OBJCATEGORY.save()
                End If




                'HSN SAVE
                If DTROWSAVE("HSNCODE") <> "" And Val(DTROWSAVE("CGSTPER")) > 0 And Val(DTROWSAVE("SGSTPER")) > 0 And Val(DTROWSAVE("IGSTPER")) > 0 Then
                    DTTABLE = OBJCMN.SEARCH("HSN_ID AS HSNCODEID", "", "HSNMASTER", " AND HSN_CODE = '" & DTROWSAVE("HSNCODE") & "' AND HSN_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW HSN
                        Dim OBJHSN As New ClsHSNMaster
                        OBJHSN.alParaval.Add("Goods")    'HSNTYPE
                        OBJHSN.alParaval.Add(UCase(DTROWSAVE("HSNCODE")))   'CODE
                        OBJHSN.alParaval.Add(UCase(DTROWSAVE("HSNCODE")))   'ITEMDESC
                        OBJHSN.alParaval.Add("") 'DESC
                        OBJHSN.alParaval.Add(0)  'RATE
                        OBJHSN.alParaval.Add(0)  'CGST
                        OBJHSN.alParaval.Add(0)  'SGST
                        OBJHSN.alParaval.Add(0)  'IGST
                        OBJHSN.alParaval.Add(0)  'RATE1
                        OBJHSN.alParaval.Add(0)  'CGST1
                        OBJHSN.alParaval.Add(0)  'SGST1
                        OBJHSN.alParaval.Add(0)  'IGST1
                        OBJHSN.alParaval.Add(0)  'EXPCGST
                        OBJHSN.alParaval.Add(0)  'EXPSGST
                        OBJHSN.alParaval.Add(0)  'EXPIGST

                        OBJHSN.alParaval.Add(CmpId)
                        OBJHSN.alParaval.Add(Userid)
                        OBJHSN.alParaval.Add(YearId)


                        OBJHSN.alParaval.Add("2017-07-01") 'WEFDATE
                        OBJHSN.alParaval.Add(0)  'GRIDRATE
                        OBJHSN.alParaval.Add(Val(DTROWSAVE("CGSTPER")))  'GRIDCGST
                        OBJHSN.alParaval.Add(Val(DTROWSAVE("SGSTPER")))  'GRIDSGST
                        OBJHSN.alParaval.Add(Val(DTROWSAVE("IGSTPER")))  'GRIDIGST

                        OBJHSN.alParaval.Add(0)  'GRIDRATE1
                        OBJHSN.alParaval.Add(0)  'GRIDCGST1
                        OBJHSN.alParaval.Add(0)  'GRIDSGST1
                        OBJHSN.alParaval.Add(0)  'GRIDIGST1
                        OBJHSN.alParaval.Add(0)  'GRIDEXPCGST
                        OBJHSN.alParaval.Add(0)  'GRIDEXPSGST
                        OBJHSN.alParaval.Add(0)  'GRIDEXPIGST

                        Dim INTRESCAT As Integer = OBJHSN.SAVE()
                    End If
                End If




                'ADD IN ITEMMASTER
                ALPARAVAL.Clear()
                Dim OBJSM As New clsItemmaster

                ALPARAVAL.Add("Finished Goods")
                ALPARAVAL.Add(DTROWSAVE("CATEGORY"))
                ALPARAVAL.Add(DTROWSAVE("ITEMNAME"))
                ALPARAVAL.Add(UCase(DTROWSAVE("ITEMNAME")))

                ALPARAVAL.Add("")   'DEPARTMENT
                ALPARAVAL.Add(DTROWSAVE("ITEMNAME"))
                ALPARAVAL.Add(DTROWSAVE("UNIT"))   'UNIT
                ALPARAVAL.Add("")   'FOLD
                ALPARAVAL.Add(Val(DTROWSAVE("RATE")))    'RATE   
                ALPARAVAL.Add(Val(DTROWSAVE("VALUATIONRATE")))    'VALUATIONRATE   
                ALPARAVAL.Add(0)    'TRANSPORTRATE   
                ALPARAVAL.Add(0)    'CHECKINGRATE   
                ALPARAVAL.Add(0)    'PACKINGRATE   
                ALPARAVAL.Add(0)    'DESIGNRATE   
                ALPARAVAL.Add(0)    'REORDER
                ALPARAVAL.Add(0)    'UPPER
                ALPARAVAL.Add(0)    'LOWER
                ALPARAVAL.Add(DTROWSAVE("HSNCODE"))
                ALPARAVAL.Add(0)    'BLOCKED
                ALPARAVAL.Add(0)    'HIDEINDESIGN

                ALPARAVAL.Add(DTROWSAVE("WIDTH"))
                ALPARAVAL.Add("")   'GREYWIDTH
                ALPARAVAL.Add(0)    'SHIRINKFROM
                ALPARAVAL.Add(0)    'SHRINKTO
                ALPARAVAL.Add(DTROWSAVE("SELVEDGE"))

                ALPARAVAL.Add("")   'RATETYPE
                ALPARAVAL.Add("")   'GRIDRATE

                ALPARAVAL.Add("")   'YARNWUALITY
                ALPARAVAL.Add("")   'PER

                ALPARAVAL.Add("")   'GRIDSRNO
                ALPARAVAL.Add("")   'PROCESS

                ALPARAVAL.Add(DTROWSAVE("REMARKS"))
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
                ALPARAVAL.Add(DTROWSAVE("GREYCATEGORY"))   'GREYCATEGORY


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

                OBJSM.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJSM.SAVE()

                DTROWSAVE = DTSAVE.NewRow()

SKIPLINE:
            Next

            oBook.Close()

            Exit Sub

            '************************************ END OF CODE FOR ITEM UPLOAD ****************************



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UploadDesignToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UPLOADDESIGNMENU.Click
        Try

            'DELETE THE DATA IN TEMPDESIGNUPLOAD TABLE IF ANY
            'RUN THIS GETDESIGNITEMSHADE QUERY 
            'THIS QUERY WILL INSERT DATA IN TEMPDESIGNUPLOAD TABLE AND THEN CLICK ON THIS UPLOADDESIGNMENU IN THAT PARTICULAR YEAR


            Dim OBJCN As New ClsCommon
            Dim DT As DataTable = OBJCN.SEARCH("DISTINCT DESIGNNO", "", " TEMPDESIGNUPLOAD ", " AND DESIGNNO NOT IN (SELECT DESIGN_NO FROM DESIGNMASTER WHERE DESIGN_YEARID = " & YearId & ")")
            For Each DTROW As DataRow In DT.Rows
                Dim DTSHADE As DataTable = OBJCN.SEARCH("*", "", "TEMPDESIGNUPLOAD", " AND DESIGNNO = '" & DTROW("DESIGNNO") & "'")

                'ADD IN DATABASE
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Clear()
                Dim OBJSM As New ClsDesignMaster

                ALPARAVAL.Add(UCase(DTSHADE.Rows(0).Item("DESIGNNO")))
                ALPARAVAL.Add("")   'MILLNAME
                ALPARAVAL.Add("")   'CADNO

                ALPARAVAL.Add(0)    'PURRATE
                ALPARAVAL.Add(0)    'SALERATE
                ALPARAVAL.Add(0)    'WRATE
                ALPARAVAL.Add("")   'REMARKS

                ALPARAVAL.Add(0)    'FABRIC
                ALPARAVAL.Add(0)    'DYEING
                ALPARAVAL.Add(0)    'JOBWORK
                ALPARAVAL.Add(0)    'FINISHING
                ALPARAVAL.Add(0)    'EXTRA
                ALPARAVAL.Add(0)    'TOTAL
                ALPARAVAL.Add(UCase(DTSHADE.Rows(0).Item("ITEMNAME")))
                ALPARAVAL.Add(0)    'BLOCKED


                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                ALPARAVAL.Add(0)

                ALPARAVAL.Add(DBNull.Value)

                Dim GRIDSRNO As String = ""
                Dim BASE As String = ""
                Dim PRINT As String = ""
                Dim COLOR As String = ""
                Dim COLORBLOCKED As String = ""
                Dim SHADETYPE As String = ""

                Dim I As Integer = 1

                For Each DTROWSHADE As DataRow In DTSHADE.Rows
                    If DTROWSHADE("COLOR") <> "" Then
                        If GRIDSRNO = "" Then
                            GRIDSRNO = I
                            BASE = DTROWSHADE("BASE")
                            PRINT = DTROWSHADE("PRINT")
                            COLOR = DTROWSHADE("COLOR")
                            COLORBLOCKED = 0
                            SHADETYPE = DTROWSHADE("SHADETYPE")
                        Else
                            GRIDSRNO = GRIDSRNO & "|" & I
                            BASE = BASE & "|" & DTROWSHADE("BASE")
                            PRINT = PRINT & "|" & DTROWSHADE("PRINT")
                            COLOR = COLOR & "|" & DTROWSHADE("COLOR")
                            COLORBLOCKED = COLORBLOCKED & "|" & 0
                            SHADETYPE = SHADETYPE & "|" & DTROWSHADE("SHADETYPE")
                        End If
                    End If
                    I += 1
                Next

                ALPARAVAL.Add(GRIDSRNO)
                ALPARAVAL.Add(BASE)
                ALPARAVAL.Add(PRINT)
                ALPARAVAL.Add(COLOR)
                ALPARAVAL.Add(COLORBLOCKED)
                ALPARAVAL.Add(SHADETYPE)

                ALPARAVAL.Add("")   'LINE1
                ALPARAVAL.Add("")   'LINE2
                ALPARAVAL.Add("")   'PARENTDESIGNNO
                ALPARAVAL.Add("")    'DESIGNER


                OBJSM.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJSM.SAVE()

            Next
            DT = OBJCN.Execute_Any_String("DELETE * FROM TEMPDESIGNUPLOAD", "", "")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SO_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SO_TOOL.Click
        Try
            If ClientName = "VAISHALI" Then
                Dim OBJSO As New YarnSaleOrder
                OBJSO.MdiParent = Me
                OBJSO.Show()
            Else
                Dim OBJSO As New SaleOrder
                OBJSO.MdiParent = Me
                OBJSO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECOUT_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECOUT_TOOL.Click
        Try
            Dim OBJOUT As New OutstandingFilter
            OBJOUT.FRMSTRING = "RECOUTSTANDING"
            OBJOUT.MdiParent = Me
            OBJOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PAYOUT_TOOL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PAYOUT_TOOL.Click
        Try
            Dim OBJOUT As New OutstandingFilter
            OBJOUT.FRMSTRING = "PAYOUTSTANDING"
            OBJOUT.MdiParent = Me
            OBJOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UPLOADSTOCKMENU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UPLOADSTOCKMENU.Click

        'ONE TIME DATA UPLOAD FOR SAFFRON FROM CBS
        Try
            If InputBox("Enter Master Password") <> "Infosys@123" Then Exit Sub
            'upload the files data
            ''Reading from Excel Woorkbook
            Dim cPart As Microsoft.Office.Interop.Excel.Range
            Dim oExcel As Microsoft.Office.Interop.Excel.Application = CreateObject("Excel.Application")
            Dim oBook As Microsoft.Office.Interop.Excel.Workbook = oExcel.Workbooks.Open("D:\" & InputBox("Enter File Name").ToString.Trim, , False)
            Dim oSheet As New Microsoft.Office.Interop.Excel.Worksheet
            oSheet = oBook.Worksheets("Sheet1")

            'GRID
            Dim ADDITEM As Boolean = True
            Dim TEMPITEMNAME As String = ""

            Dim DTSAVE As New System.Data.DataTable
            DTSAVE.Columns.Add("ITEMNAME")
            DTSAVE.Columns.Add("DESIGN")
            DTSAVE.Columns.Add("COLOR")
            DTSAVE.Columns.Add("UNIT")
            DTSAVE.Columns.Add("BARCODE")
            DTSAVE.Columns.Add("QTY")
            DTSAVE.Columns.Add("MTRS")
            DTSAVE.Columns.Add("HSNCODE")
            DTSAVE.Columns.Add("PARTYNAME")
            DTSAVE.Columns.Add("QUALITY")
            DTSAVE.Columns.Add("LOTNO")
            DTSAVE.Columns.Add("BALENO")
            DTSAVE.Columns.Add("REMARKS")
            DTSAVE.Columns.Add("GODOWN")
            DTSAVE.Columns.Add("RACKNO")
            DTSAVE.Columns.Add("DYEINGNAME")
            DTSAVE.Columns.Add("CUT")
            DTSAVE.Columns.Add("RATE")
            DTSAVE.Columns.Add("BILLNO")
            DTSAVE.Columns.Add("WT")
            DTSAVE.Columns.Add("CATEGORY")
            DTSAVE.Columns.Add("PIECETYPE")
            DTSAVE.Columns.Add("DYEINGJOB")
            DTSAVE.Columns.Add("CGSTPER")
            DTSAVE.Columns.Add("SGSTPER")
            DTSAVE.Columns.Add("IGSTPER")



            Dim ARR As New ArrayList
            Dim COLIND As Integer = 0
            Dim DTROWSAVE As System.Data.DataRow = DTSAVE.NewRow()


            Dim FROMROWNO As Integer = Val(InputBox("Enter Start Row No"))
            Dim TOROWNO As Integer = Val(InputBox("Enter End Row No"))

            For I As Integer = FROMROWNO To TOROWNO

                If oSheet.Range("A" & I.ToString).Text = "PRODUCT TOTAL" Or oSheet.Range("A" & I.ToString).Text = "" Then
                    ADDITEM = True
                    GoTo SKIPLINE
                End If

                If ADDITEM = True Then
                    If IsDBNull(oSheet.Range("A" & I.ToString).Text) = False Then
                        DTROWSAVE("ITEMNAME") = oSheet.Range("A" & I.ToString).Text
                        TEMPITEMNAME = DTROWSAVE("ITEMNAME")
                        'FOR CBS REMOVE THE COMMENT BELOW
                        'ADDITEM = False
                    Else
                        DTROWSAVE("ITEMNAME") = ""
                    End If
                Else
                    'RETRIEVE SAME ITEMNAME
                    DTROWSAVE("ITEMNAME") = TEMPITEMNAME
                End If


                If IsDBNull(oSheet.Range("B" & I.ToString).Text) = False Then
                    DTROWSAVE("DESIGN") = oSheet.Range("B" & I.ToString).Text
                Else
                    DTROWSAVE("DESIGN") = ""
                End If


                If IsDBNull(oSheet.Range("C" & I.ToString).Text) = False Then
                    DTROWSAVE("COLOR") = oSheet.Range("C" & I.ToString).Text
                Else
                    DTROWSAVE("COLOR") = ""
                End If


                If IsDBNull(oSheet.Range("D" & I.ToString).Text) = False Then
                    DTROWSAVE("UNIT") = oSheet.Range("D" & I.ToString).Text
                Else
                    DTROWSAVE("UNIT") = ""
                End If

                If IsDBNull(oSheet.Range("E" & I.ToString).Text) = False Then
                    DTROWSAVE("BARCODE") = oSheet.Range("E" & I.ToString).Text
                Else
                    DTROWSAVE("BARCODE") = ""
                End If

                If IsDBNull(oSheet.Range("F" & I.ToString).Text) = False Then
                    DTROWSAVE("QTY") = Val(oSheet.Range("F" & I.ToString).Text)
                Else
                    DTROWSAVE("QTY") = 0
                End If

                If IsDBNull(oSheet.Range("G" & I.ToString).Text) = False Then
                    DTROWSAVE("MTRS") = Val(oSheet.Range("G" & I.ToString).Text)
                Else
                    DTROWSAVE("MTRS") = 0
                End If

                If IsDBNull(oSheet.Range("H" & I.ToString).Text) = False Then
                    DTROWSAVE("HSNCODE") = oSheet.Range("H" & I.ToString).Text
                Else
                    DTROWSAVE("HSNCODE") = ""
                End If

                If IsDBNull(oSheet.Range("I" & I.ToString).Text) = False Then
                    DTROWSAVE("PARTYNAME") = oSheet.Range("I" & I.ToString).Text
                Else
                    DTROWSAVE("PARTYNAME") = ""
                End If

                If IsDBNull(oSheet.Range("J" & I.ToString).Text) = False Then
                    DTROWSAVE("QUALITY") = oSheet.Range("J" & I.ToString).Text
                Else
                    DTROWSAVE("QUALITY") = ""
                End If

                If IsDBNull(oSheet.Range("K" & I.ToString).Text) = False Then
                    DTROWSAVE("LOTNO") = oSheet.Range("K" & I.ToString).Text
                Else
                    DTROWSAVE("LOTNO") = ""
                End If

                If IsDBNull(oSheet.Range("L" & I.ToString).Text) = False Then
                    DTROWSAVE("BALENO") = oSheet.Range("L" & I.ToString).Text
                Else
                    DTROWSAVE("BALENO") = ""
                End If

                If IsDBNull(oSheet.Range("M" & I.ToString).Text) = False Then
                    DTROWSAVE("REMARKS") = oSheet.Range("M" & I.ToString).Text
                Else
                    DTROWSAVE("REMARKS") = ""
                End If

                If IsDBNull(oSheet.Range("N" & I.ToString).Text) = False Then
                    DTROWSAVE("GODOWN") = oSheet.Range("N" & I.ToString).Text
                Else
                    DTROWSAVE("GODOWN") = ""
                End If

                If IsDBNull(oSheet.Range("O" & I.ToString).Text) = False Then
                    DTROWSAVE("RACKNO") = oSheet.Range("O" & I.ToString).Text
                Else
                    DTROWSAVE("RACKNO") = ""
                End If

                If IsDBNull(oSheet.Range("P" & I.ToString).Text) = False Then
                    DTROWSAVE("DYEINGNAME") = oSheet.Range("P" & I.ToString).Text
                Else
                    DTROWSAVE("DYEINGNAME") = ""
                End If

                If IsDBNull(oSheet.Range("Q" & I.ToString).Text) = False Then
                    DTROWSAVE("CUT") = oSheet.Range("Q" & I.ToString).Text
                Else
                    DTROWSAVE("CUT") = ""
                End If

                If IsDBNull(oSheet.Range("R" & I.ToString).Text) = False Then
                    DTROWSAVE("RATE") = Val(oSheet.Range("R" & I.ToString).Text)
                Else
                    DTROWSAVE("RATE") = 0
                End If

                If IsDBNull(oSheet.Range("S" & I.ToString).Text) = False Then
                    DTROWSAVE("BILLNO") = Val(oSheet.Range("S" & I.ToString).Text)
                Else
                    DTROWSAVE("BILLNO") = 0
                End If

                If IsDBNull(oSheet.Range("T" & I.ToString).Text) = False Then
                    DTROWSAVE("WT") = Val(oSheet.Range("T" & I.ToString).Text)
                Else
                    DTROWSAVE("WT") = 0
                End If

                If IsDBNull(oSheet.Range("U" & I.ToString).Text) = False Then
                    DTROWSAVE("CATEGORY") = oSheet.Range("U" & I.ToString).Text
                Else
                    DTROWSAVE("CATEGORY") = ""
                End If


                If IsDBNull(oSheet.Range("V" & I.ToString).Text) = False Then
                    DTROWSAVE("PIECETYPE") = oSheet.Range("V" & I.ToString).Text
                Else
                    DTROWSAVE("PIECETYPE") = ""
                End If

                If IsDBNull(oSheet.Range("W" & I.ToString).Text) = False Then
                    DTROWSAVE("DYEINGJOB") = oSheet.Range("W" & I.ToString).Text
                Else
                    DTROWSAVE("DYEINGJOB") = ""
                End If

                If IsDBNull(oSheet.Range("X" & I.ToString).Text) = False Then
                    DTROWSAVE("CGSTPER") = Val(oSheet.Range("X" & I.ToString).Text)
                Else
                    DTROWSAVE("CGSTPER") = 0
                End If

                If IsDBNull(oSheet.Range("Y" & I.ToString).Text) = False Then
                    DTROWSAVE("SGSTPER") = Val(oSheet.Range("Y" & I.ToString).Text)
                Else
                    DTROWSAVE("SGSTPER") = 0
                End If

                If IsDBNull(oSheet.Range("Z" & I.ToString).Text) = False Then
                    DTROWSAVE("IGSTPER") = Val(oSheet.Range("Z" & I.ToString).Text)
                Else
                    DTROWSAVE("IGSTPER") = 0
                End If



                If Val(DTROWSAVE("MTRS")) = 0 Then GoTo SKIPLINE

                Dim ALPARAVAL As New ArrayList
                Dim OBJCMN As New ClsCommon
                Dim DTTABLE As New DataTable





                'HSN SAVE
                If DTROWSAVE("HSNCODE") <> "" And Val(DTROWSAVE("CGSTPER")) > 0 And Val(DTROWSAVE("SGSTPER")) > 0 And Val(DTROWSAVE("IGSTPER")) > 0 Then
                    DTTABLE = OBJCMN.SEARCH("HSN_ID AS HSNCODEID", "", "HSNMASTER", " AND HSN_CODE = '" & DTROWSAVE("HSNCODE") & "' AND HSN_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW HSN
                        Dim OBJHSN As New ClsHSNMaster
                        OBJHSN.alParaval.Add("Goods")    'HSNTYPE
                        OBJHSN.alParaval.Add(UCase(DTROWSAVE("HSNCODE")))   'CODE
                        OBJHSN.alParaval.Add(UCase(DTROWSAVE("HSNCODE")))   'ITEMDESC
                        OBJHSN.alParaval.Add("") 'DESC
                        OBJHSN.alParaval.Add(0)  'RATE
                        OBJHSN.alParaval.Add(0)  'CGST
                        OBJHSN.alParaval.Add(0)  'SGST
                        OBJHSN.alParaval.Add(0)  'IGST
                        OBJHSN.alParaval.Add(0)  'RATE1
                        OBJHSN.alParaval.Add(0)  'CGST1
                        OBJHSN.alParaval.Add(0)  'SGST1
                        OBJHSN.alParaval.Add(0)  'IGST1
                        OBJHSN.alParaval.Add(0)  'EXPCGST
                        OBJHSN.alParaval.Add(0)  'EXPSGST
                        OBJHSN.alParaval.Add(0)  'EXPIGST

                        OBJHSN.alParaval.Add(CmpId)
                        OBJHSN.alParaval.Add(Userid)
                        OBJHSN.alParaval.Add(YearId)


                        OBJHSN.alParaval.Add("2017-07-01") 'WEFDATE
                        OBJHSN.alParaval.Add(0)  'GRIDRATE
                        OBJHSN.alParaval.Add(Val(DTROWSAVE("CGSTPER")))  'GRIDCGST
                        OBJHSN.alParaval.Add(Val(DTROWSAVE("SGSTPER")))  'GRIDSGST
                        OBJHSN.alParaval.Add(Val(DTROWSAVE("IGSTPER")))  'GRIDIGST

                        OBJHSN.alParaval.Add(0)  'GRIDRATE1
                        OBJHSN.alParaval.Add(0)  'GRIDCGST1
                        OBJHSN.alParaval.Add(0)  'GRIDSGST1
                        OBJHSN.alParaval.Add(0)  'GRIDIGST1
                        OBJHSN.alParaval.Add(0)  'GRIDEXPCGST
                        OBJHSN.alParaval.Add(0)  'GRIDEXPSGST
                        OBJHSN.alParaval.Add(0)  'GRIDEXPIGST

                        Dim INTRESCAT As Integer = OBJHSN.SAVE()
                    End If
                End If








                'CHECK WHETHER ITEMNAME IS PRESENT OR NOT IF NOT PRESENT THEN ADD NEW
                If DTROWSAVE("ITEMNAME") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", "ITEMMASTER ", "AND ITEM_NAME = '" & DTROWSAVE("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW ITEMNAME 
                        ALPARAVAL.Clear()


                        ALPARAVAL.Add("Finished Goods")
                        ALPARAVAL.Add(DTROWSAVE("CATEGORY"))   'CATEGORY
                        ALPARAVAL.Add(UCase(DTROWSAVE("ITEMNAME")))        'DISPLAYNAME
                        ALPARAVAL.Add(UCase(DTROWSAVE("ITEMNAME"))) 'ITEMNAME

                        ALPARAVAL.Add("")   'DEPARTMENT
                        ALPARAVAL.Add(UCase(DTROWSAVE("ITEMNAME")))        'CODE
                        ALPARAVAL.Add(DTROWSAVE("UNIT"))
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

                        Dim DTHSN As DataTable = OBJCMN.SEARCH("ISNULL(HSN_ID, 0) AS HSNCODEID", "", " HSNMASTER", " AND HSN_CODE = '" & DTROWSAVE("HSNCODE") & "' AND HSN_YEARID = " & YearId)
                        If DTHSN.Rows.Count > 0 Then ALPARAVAL.Add(DTROWSAVE("HSNCODE")) Else ALPARAVAL.Add(0) 'HSNCODEID

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
                        ALPARAVAL.Add("")   'GREY CATEGORY

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
                        ALPARAVAL.Add(0)    'SHADEsrno

                        Dim objclsItemMaster As New clsItemmaster
                        objclsItemMaster.alParaval = ALPARAVAL
                        Dim IntResult As Integer = objclsItemMaster.SAVE()

                    End If
                End If


                'DESIGN SAVE
                If DTROWSAVE("DESIGN") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("DESIGN_ID AS DESIGNID", "", "DESIGNMASTER", " AND DESIGN_NO = '" & DTROWSAVE("DESIGN") & "' AND DESIGN_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW DESIGN
                        Dim OBJDESIGN As New ClsDesignMaster
                        OBJDESIGN.alParaval.Add(UCase(DTROWSAVE("DESIGN")))
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
                If DTROWSAVE("COLOR") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("COLOR_ID AS COLORID", "", "COLORMASTER", " AND COLOR_NAME = '" & DTROWSAVE("COLOR") & "' AND COLOR_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW DESIGN
                        Dim OBJCOLOR As New ClsColorMaster
                        OBJCOLOR.alParaval.Add(UCase(DTROWSAVE("COLOR")))
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
                If DTROWSAVE("QUALITY") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("QUALITY_ID AS QUALITYID", "", "QUALITYMASTER", " AND QUALITY_NAME = '" & DTROWSAVE("QUALITY") & "' AND QUALITY_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW QUALITY
                        Dim OBJQUALITY As New ClsQualityMaster
                        OBJQUALITY.alParaval.Add(UCase(DTROWSAVE("QUALITY")))
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
                        Dim INTRESCAT As Integer = OBJQUALITY.save()
                    End If
                End If




                'RACK SAVE
                If DTROWSAVE("RACKNO") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("RACK_ID AS RACKID", "", "RACKMASTER", " AND RACK_NAME = '" & DTROWSAVE("RACKNO") & "' AND RACK_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW DESIGN
                        Dim OBJRACK As New ClsRackMaster
                        OBJRACK.alParaval.Add(UCase(DTROWSAVE("RACKNO")))
                        OBJRACK.alParaval.Add("")
                        OBJRACK.alParaval.Add(CmpId)
                        OBJRACK.alParaval.Add(Userid)
                        OBJRACK.alParaval.Add(YearId)

                        Dim INTRESCAT As Integer = OBJRACK.SAVE()
                    End If
                End If



                'PIECETYPE SAVE
                If DTROWSAVE("PIECETYPE") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("PIECETYPE_ID AS PIECETYPEID", "", "PIECETYPEMASTER", " AND PIECETYPE_NAME = '" & DTROWSAVE("PIECETYPE") & "' AND PIECETYPE_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW PIECETYPE
                        Dim OBJPIECETYPE As New ClsPieceTypeMaster
                        OBJPIECETYPE.alParaval.Add(UCase(DTROWSAVE("PIECETYPE")))
                        OBJPIECETYPE.alParaval.Add("")
                        OBJPIECETYPE.alParaval.Add(CmpId)
                        OBJPIECETYPE.alParaval.Add(0)
                        OBJPIECETYPE.alParaval.Add(Userid)
                        OBJPIECETYPE.alParaval.Add(YearId)
                        OBJPIECETYPE.alParaval.Add(0)   'TRANSFER

                        Dim INTRESCAT As Integer = OBJPIECETYPE.save()
                    End If
                End If



                'check whether ITEMNAME is already present or not
                DTTABLE = OBJCMN.SEARCH("SM_BARCODE AS BARCODE", "", "STOCKMASTER ", " AND SM_BARCODE = '" & DTROWSAVE("BARCODE") & "' AND SM_YEARID = " & YearId)
                If DTTABLE.Rows.Count > 0 Then GoTo SKIPLINE




                'IF DYEINGJONB IS '' THEN ADD DYEING
                If DTROWSAVE("DYEINGJOB") = "" Then DTROWSAVE("DYEINGJOB") = "DYEING"



                'ADD IN STOCKMASTER
                ALPARAVAL.Clear()
                Dim OBJSM As New ClsStockMaster

                ALPARAVAL.Add(AccFrom.Date)
                ALPARAVAL.Add("INHOUSE")

                ALPARAVAL.Add(I)
                ALPARAVAL.Add(DTROWSAVE("LOTNO"))               'LOTNO
                If DTROWSAVE("PIECETYPE") = "" Then ALPARAVAL.Add("FRESH") Else ALPARAVAL.Add(DTROWSAVE("PIECETYPE"))
                ALPARAVAL.Add(DTROWSAVE("ITEMNAME"))
                ALPARAVAL.Add(DTROWSAVE("QUALITY"))    'QUALITY
                ALPARAVAL.Add(DTROWSAVE("DESIGN"))               'DESIGNNO
                ALPARAVAL.Add(DTROWSAVE("COLOR"))               'COLOR    
                ALPARAVAL.Add("")               'PROCESS
                ALPARAVAL.Add(DTROWSAVE("PARTYNAME"))               'NAME
                ALPARAVAL.Add(DTROWSAVE("DYEINGNAME"))               'TONAME
                ALPARAVAL.Add(DTROWSAVE("BILLNO"))                'BILLNO
                ALPARAVAL.Add(DTROWSAVE("GODOWN"))         'GODOWN
                If Val(DTROWSAVE("CUT")) > 0 Then ALPARAVAL.Add(Val(DTROWSAVE("CUT"))) Else ALPARAVAL.Add(Val(DTROWSAVE("MTRS")))   'CUT
                ALPARAVAL.Add(Val(DTROWSAVE("WT")))                    'WT
                ALPARAVAL.Add(DTROWSAVE("UNIT"))
                ALPARAVAL.Add(Val(DTROWSAVE("QTY")))
                ALPARAVAL.Add(Val(DTROWSAVE("MTRS")))
                ALPARAVAL.Add("") 'PER
                ALPARAVAL.Add(DTROWSAVE("RACKNO"))                    'RACK
                ALPARAVAL.Add("")                    'SHELF
                ALPARAVAL.Add(Val(DTROWSAVE("RATE"))) 'RATE
                ALPARAVAL.Add(Format(Val(DTROWSAVE("RATE")) * Val(DTROWSAVE("MTRS")), "0.00"))                    'AMOUNT
                ALPARAVAL.Add("")                   'ADDLESS
                ALPARAVAL.Add(0)                   'NETTRATE
                ALPARAVAL.Add("")                   'PARTYCHALLANNO
                ALPARAVAL.Add(DTROWSAVE("BALENO"))                   'BALENO
                ALPARAVAL.Add("")                   'GRIDREMARKS

                ALPARAVAL.Add(DTROWSAVE("DYEINGJOB"))    'DYEINGJOB
                ALPARAVAL.Add("")                   'REMARKS
                ALPARAVAL.Add(DTROWSAVE("BARCODE"))

                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                ALPARAVAL.Add(0)
                ALPARAVAL.Add(0)    'PROGRAM DONE

                OBJSM.alParaval = ALPARAVAL
                DTTABLE = OBJSM.save()

                'If DTROWSAVE("BARCODE") = "1609002784" Then
                '    MsgBox("A")
                'End If
                DTROWSAVE = DTSAVE.NewRow()

SKIPLINE:
            Next

            'Dim DV As DataView = gridBANKRECO.DataSource
            'Dim DTNEW As DataTable = DV.Table
            'If DT.Rows.Count > 0 Then
            '    For Each ROW As System.Data.DataRow In DT.Rows
            '        For Each DTNEWROW As DataRow In DTNEW.Rows
            '            If ROW("CHQNO") = DTNEWROW("ChqNo") And (Val(ROW("AMOUNT")) = Val(DTNEWROW("dr")) Or Val(ROW("AMOUNT")) = Val(DTNEWROW("cr"))) Then
            '                'If Format(Convert.ToDateTime(DTNEWROW("BillDate")).Date, "dd/MM/yyyy") <= Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yyyy") Then DTNEWROW("RecoDate") = Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yyyy")
            '                If Format(Convert.ToDateTime(DTNEWROW("BillDate")).Date, "dd/MM/yyyy") <= Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yyyy") Then DTNEWROW("RecoDate") = ROW("DATE")
            '            End If
            '        Next
            '    Next
            'End If


            oBook.Close()

            Exit Sub
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UPLOADOPENINGBILL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UPLOADOPENINGBILLMENU.Click

        'ONE TIME DATA UPLOAD FOR OPENINGBILL
        Try
            If InputBox("Enter Master Password") <> "Infosys@123" Then Exit Sub
            'upload the files data
            ''Reading from Excel Woorkbook
            Dim cPart As Microsoft.Office.Interop.Excel.Range
            Dim oExcel As Microsoft.Office.Interop.Excel.Application = CreateObject("Excel.Application")
            Dim oBook As Microsoft.Office.Interop.Excel.Workbook = oExcel.Workbooks.Open("D:\" & InputBox("Enter File Name").ToString.Trim, , False)
            Dim oSheet As New Microsoft.Office.Interop.Excel.Worksheet
            oSheet = oBook.Worksheets("Sheet1")

            Dim DTSAVE As New System.Data.DataTable
            DTSAVE.Columns.Add("NAME")
            DTSAVE.Columns.Add("TYPE")
            DTSAVE.Columns.Add("REGISTER")
            DTSAVE.Columns.Add("BILLNO")
            DTSAVE.Columns.Add("YEAR")
            DTSAVE.Columns.Add("DATE")
            DTSAVE.Columns.Add("CRDAYS")
            DTSAVE.Columns.Add("DUEDATE")
            DTSAVE.Columns.Add("AGENT")
            DTSAVE.Columns.Add("BALANCE")
            DTSAVE.Columns.Add("BILLINITIALS")
            DTSAVE.Columns.Add("REMARKS")
            DTSAVE.Columns.Add("PRINTINITIALS")

            Dim ARR As New ArrayList
            Dim COLIND As Integer = 0
            Dim DTROWSAVE As System.Data.DataRow = DTSAVE.NewRow()


            Dim FROMROWNO As Integer = Val(InputBox("Enter Start Row No"))
            Dim TOROWNO As Integer = Val(InputBox("Enter End Row No"))

            For I As Integer = FROMROWNO To TOROWNO

                If IsDBNull(oSheet.Range("A" & I.ToString).Text) = False Then
                    DTROWSAVE("NAME") = oSheet.Range("A" & I.ToString).Text
                Else
                    DTROWSAVE("NAME") = ""
                End If

                If IsDBNull(oSheet.Range("B" & I.ToString).Text) = False Then
                    DTROWSAVE("TYPE") = oSheet.Range("B" & I.ToString).Text
                Else
                    DTROWSAVE("TYPE") = ""
                End If


                If IsDBNull(oSheet.Range("C" & I.ToString).Text) = False Then
                    DTROWSAVE("REGISTER") = oSheet.Range("C" & I.ToString).Text
                Else
                    DTROWSAVE("REGISTER") = ""
                End If


                If IsDBNull(oSheet.Range("D" & I.ToString).Text) = False Then
                    DTROWSAVE("BILLNO") = Val(oSheet.Range("D" & I.ToString).Text)
                Else
                    DTROWSAVE("BILLNO") = ""
                End If

                If IsDBNull(oSheet.Range("E" & I.ToString).Text) = False Then
                    DTROWSAVE("YEAR") = oSheet.Range("E" & I.ToString).Text
                Else
                    DTROWSAVE("YEAR") = ""
                End If

                If IsDBNull(oSheet.Range("F" & I.ToString).Text) = False Then
                    DTROWSAVE("DATE") = Format(Convert.ToDateTime(oSheet.Range("F" & I.ToString).Text).Date, "MM/dd/yyyy")
                Else
                    DTROWSAVE("DATE") = 0
                End If

                If IsDBNull(oSheet.Range("G" & I.ToString).Text) = False Then
                    DTROWSAVE("CRDAYS") = Val(oSheet.Range("G" & I.ToString).Text)
                Else
                    DTROWSAVE("CRDAYS") = 0
                End If

                If IsDBNull(oSheet.Range("H" & I.ToString).Text) = False Then
                    DTROWSAVE("DUEDATE") = Format(Convert.ToDateTime(oSheet.Range("H" & I.ToString).Text).Date, "MM/dd/yyyy")
                Else
                    DTROWSAVE("DUEDATE") = 0
                End If

                If IsDBNull(oSheet.Range("I" & I.ToString).Text) = False Then
                    DTROWSAVE("AGENT") = oSheet.Range("I" & I.ToString).Text
                Else
                    DTROWSAVE("AGENT") = ""
                End If

                If IsDBNull(oSheet.Range("J" & I.ToString).Text) = False Then
                    DTROWSAVE("BALANCE") = Val(oSheet.Range("J" & I.ToString).Text)
                Else
                    DTROWSAVE("BALANCE") = 0
                End If

                If IsDBNull(oSheet.Range("K" & I.ToString).Text) = False Then
                    DTROWSAVE("BILLINITIALS") = oSheet.Range("K" & I.ToString).Text
                Else
                    DTROWSAVE("BILLINITIALS") = 0
                End If

                If IsDBNull(oSheet.Range("L" & I.ToString).Text) = False Then
                    DTROWSAVE("REMARKS") = oSheet.Range("L" & I.ToString).Text
                Else
                    DTROWSAVE("REMARKS") = 0
                End If

                If IsDBNull(oSheet.Range("M" & I.ToString).Text) = False Then
                    DTROWSAVE("PRINTINITIALS") = oSheet.Range("M" & I.ToString).Text
                Else
                    DTROWSAVE("PRINTINITIALS") = 0
                End If



                Dim ALPARAVAL As New ArrayList
                Dim DTTABLE As New DataTable
                Dim OBJCMN As New ClsCommon
                'check whether ITEMNAME is already present or not
                DTTABLE = OBJCMN.SEARCH("BILL_INITIALS AS BILLINITIALS", "", "OPENINGBILL INNER JOIN LEDGERS ON BILL_LEDGERID = LEDGERS.ACC_ID", " AND LEDGERS.ACC_CMPNAME = '" & DTROWSAVE("NAME") & "' AND BILL_INITIALS = '" & DTROWSAVE("BILLINITIALS") & "' AND BILL_YEARID = " & YearId)
                If DTTABLE.Rows.Count > 0 Then GoTo SKIPLINE



                'ADD IN OPEINGBILL
                ALPARAVAL.Clear()
                Dim OBJSM As New ClsOpening

                ALPARAVAL.Add(DTROWSAVE("NAME"))
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                ALPARAVAL.Add(0)

                ALPARAVAL.Add(I)
                ALPARAVAL.Add(DTROWSAVE("TYPE"))
                ALPARAVAL.Add(DTROWSAVE("REGISTER"))
                ALPARAVAL.Add(DTROWSAVE("BILLNO"))
                ALPARAVAL.Add(DTROWSAVE("YEAR"))
                ALPARAVAL.Add(DTROWSAVE("DATE"))
                ALPARAVAL.Add(DTROWSAVE("CRDAYS"))
                ALPARAVAL.Add(DTROWSAVE("DUEDATE"))
                ALPARAVAL.Add(DTROWSAVE("AGENT"))
                ALPARAVAL.Add(DTROWSAVE("BILLINITIALS"))   'NARRATION
                ALPARAVAL.Add(DTROWSAVE("REMARKS"))   'REMARKS
                ALPARAVAL.Add(0)    'DISPUTE
                ALPARAVAL.Add(0)    'DELIVERYATID
                ALPARAVAL.Add(0)    'TOTALPCS
                ALPARAVAL.Add(0)    'TOTALMTRS
                ALPARAVAL.Add(0)    'TOTALAMT
                ALPARAVAL.Add(0)    'CHARGES
                ALPARAVAL.Add(0)    'TAXABLEAMT
                ALPARAVAL.Add(0)    'CGSTPER
                ALPARAVAL.Add(0)    'CGSTAMT
                ALPARAVAL.Add(0)    'SGSTPER
                ALPARAVAL.Add(0)    'SGSTAMT
                ALPARAVAL.Add(0)    'IGSTPER
                ALPARAVAL.Add(0)    'IGSTAMT
                ALPARAVAL.Add(0)    'GRANDTOTAL
                ALPARAVAL.Add(DTROWSAVE("BALANCE"))
                ALPARAVAL.Add(0)    'AMTPAIDREC
                ALPARAVAL.Add(0)    'EXTRAAMT
                ALPARAVAL.Add(0)    'RETURN
                ALPARAVAL.Add(DTROWSAVE("BALANCE"))
                ALPARAVAL.Add(DTROWSAVE("PRINTINITIALS"))    'PRINTINITIALS


                OBJSM.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJSM.UPLOAD()
                DTROWSAVE = DTSAVE.NewRow()

SKIPLINE:
            Next
            oBook.Close()

            Exit Sub
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StockOnHandSummarymenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOCKONHANDSUMMARYMENU.Click
        Try
            Dim OBJSTOCK As New StockOnHandSummary
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PENDINGDETAILS_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PENDINGDETAILS_MASTER.Click
        Try
            Dim OBJPENDING As New GDNDESIGN
            OBJPENDING.MdiParent = Me
            OBJPENDING.FRMSTRING = "PENDINGDETAILS"
            OBJPENDING.selfor_ss = " {PENDINGDETAILS.YEARID}=" & YearId
            OBJPENDING.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DAILYACTIVITYFILTER_MASTER_Click(sender As Object, e As EventArgs) Handles DAILYACTIVITYFILTER_MASTER.Click
        Try
            Dim OBJACT As New DailyActivityFilter
            OBJACT.MdiParent = Me
            OBJACT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AGEINGREPORT_MASTER_Click(sender As Object, e As EventArgs) Handles AGEINGREPORT_MASTER.Click
        Try
            Dim OBJAGEING As New AgeingReport
            OBJAGEING.MdiParent = Me
            OBJAGEING.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SENDSMS_MASTER_Click(sender As Object, e As EventArgs) Handles SENDSMS_MASTER.Click
        Try
            Dim OBJSMS As New SendSMS
            OBJSMS.MdiParent = Me
            OBJSMS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PAYMENTREGISTER_MENU_Click(sender As Object, e As EventArgs) Handles PAYMENTREGISTER_MENU.Click
        Try
            Dim OBJPAY As New PaymentReceiptFilter
            OBJPAY.FRMSTRING = "PAYMENT"
            OBJPAY.MdiParent = Me
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECEIPTREGISTER_MENU_Click(sender As Object, e As EventArgs) Handles RECEIPTREGISTER_MENU.Click
        Try
            Dim OBJPAY As New PaymentReceiptFilter
            OBJPAY.FRMSTRING = "RECEIPT"
            OBJPAY.MdiParent = Me
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAMPLENOTEADD_Click(sender As Object, e As EventArgs) Handles SAMPLENOTEADD.Click
        Try
            Dim OBJSMP As New SampleNote
            OBJSMP.MdiParent = Me
            OBJSMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAMPLENOTEEDIT_Click(sender As Object, e As EventArgs) Handles SAMPLENOTEEDIT.Click
        Try
            Dim OBJSMP As New SampleNoteDetails
            OBJSMP.MdiParent = Me
            OBJSMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MissingInvoiceNoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MissingInvoiceNoToolStripMenuItem.Click
        Try
            Dim OBJINV As New MissingInvoiceReport
            OBJINV.MdiParent = Me
            OBJINV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateLRNoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateLRNoToolStripMenuItem.Click
        Try
            Dim OBJLR As New PendingLRNo
            OBJLR.MdiParent = Me
            OBJLR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InterestCalculatorBillWiseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InterestCalculatorBillWiseToolStripMenuItem.Click
        Try
            Dim OBJINT As New InterestCalc_BillWise
            OBJINT.MdiParent = Me
            OBJINT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAMPLEPRICELISTADD_Click(sender As Object, e As EventArgs) Handles SAMPLEPRICELISTADD.Click
        Try
            Dim OBJSPL As New SamplePriceList
            OBJSPL.MdiParent = Me
            OBJSPL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAMPLEPRICELISTEDIT_Click(sender As Object, e As EventArgs) Handles SAMPLEPRICELISTEDIT.Click
        Try
            Dim OBJSPL As New SamplePriceListDetails
            OBJSPL.MdiParent = Me
            OBJSPL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAMPLEBARCODE_MASTER_Click(sender As Object, e As EventArgs) Handles SAMPLEBARCODE_MASTER.Click
        Try
            Dim OBJSB As New SampleBarcode
            OBJSB.MdiParent = Me
            OBJSB.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UPDATEBROKERMENU_Click(sender As Object, e As EventArgs) Handles UPDATEBROKERMENU.Click
        Try
            Dim OBJUPDATE As New UpdateBroker
            OBJUPDATE.MdiParent = Me
            OBJUPDATE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPPOADD_Click(sender As Object, e As EventArgs) Handles OPPOADD.Click
        Try
            If ClientName = "VAISHALI" Then
                Dim OBJOPPO As New OpeningYarnPurchaseOrder
                OBJOPPO.MdiParent = Me
                OBJOPPO.Show()
            Else
                Dim OBJOPPO As New OpeningPurchaseOrder
                OBJOPPO.MdiParent = Me
                OBJOPPO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPPOEDIT_Click(sender As Object, e As EventArgs) Handles OPPOEDIT.Click
        Try
            If ClientName = "VAISHALI" Then
                Dim OBJOPPO As New OpeningYarnPurchaseOrderDetails
                OBJOPPO.MdiParent = Me
                OBJOPPO.Show()
            Else
                Dim OBJOPPO As New OpeningPurchaseOrderDetails
                OBJOPPO.MdiParent = Me
                OBJOPPO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UPLOADSIGN_Click(sender As Object, e As EventArgs) Handles UPLOADSIGN.Click
        Try
            Dim OBJOPPO As New UploadSign
            OBJOPPO.MdiParent = Me
            OBJOPPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TERMSANDCONDITIONS_Click(sender As Object, e As EventArgs) Handles TERMSANDCONDITIONS.Click
        Try
            Dim OBJOPPO As New TermsAndConditions
            OBJOPPO.MdiParent = Me
            OBJOPPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateReminderDaysToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateReminderDaysToolStripMenuItem.Click
        Try
            Dim OBJUPDATE As New ChangeReminderDays
            OBJUPDATE.MdiParent = Me
            OBJUPDATE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateDefaultStockUnitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateDefaultStockUnitToolStripMenuItem.Click
        Try
            Dim OBJDEF As New DefaultUnit
            OBJDEF.MdiParent = Me
            OBJDEF.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub POCLOSE_Click(sender As Object, e As EventArgs) Handles POCLOSE.Click
        Try
            Dim OBJPO As New PurchaseOrderClose
            OBJPO.MdiParent = Me
            OBJPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LOCKACCYEAR_MASTER_Click(sender As Object, e As EventArgs) Handles LOCKACCYEAR_MASTER.Click
        Try
            Dim OBJLOCK As New LockAccYear
            OBJLOCK.MdiParent = Me
            OBJLOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BLOCKDATEMENU_Click(sender As Object, e As EventArgs) Handles BLOCKDATEMENU.Click
        Try
            Dim OBJBLOCK As New BlockDateEntry
            OBJBLOCK.MdiParent = Me
            OBJBLOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Try
            Dim OBJSTOCK As New StockOnHandWithPacking
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ADDCONTRACT_Click(sender As Object, e As EventArgs) Handles CONTRACTORADD.Click
        Try
            Dim OBJCON As New ContractMaster
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EDITCONTRACT_Click(sender As Object, e As EventArgs) Handles CONTRACTOREDIT.Click
        Try
            Dim OBJCON As New ContractMasterDetails
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROFORMAEDIT_Click(sender As Object, e As EventArgs) Handles PROFORMAEDIT.Click
        Try
            Dim OBJPRODETAILS As New ProformaDetails
            OBJPRODETAILS.MdiParent = Me
            OBJPRODETAILS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROFORMAADD_Click(sender As Object, e As EventArgs) Handles PROFORMAADD.Click
        Try
            Dim OBJPROFORMA As New Proforma
            OBJPROFORMA.MdiParent = Me
            OBJPROFORMA.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub INTERGODOWNADD_Click(sender As Object, e As EventArgs) Handles INTERGODOWNADD.Click
        Try
            Dim OBJPROFORMA As New InterGodownTransfer
            OBJPROFORMA.MdiParent = Me
            OBJPROFORMA.Show()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub INTERGODOWNEDIT_Click(sender As Object, e As EventArgs) Handles INTERGODOWNEDIT.Click
        Try
            Dim OBJPROFORMA As New InterGodownTransferDetails
            OBJPROFORMA.MdiParent = Me
            OBJPROFORMA.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EXPENSEREGISTER_MASTER_Click(sender As Object, e As EventArgs) Handles EXPENSEREGISTER_MASTER.Click
        Try
            Dim OBJEXP As New ExpenseRegister
            OBJEXP.MdiParent = Me
            OBJEXP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub USERGODOWNADD_Click(sender As Object, e As EventArgs) Handles USERGODOWNADD.Click
        Try
            Dim OBJPROFORMA As New UserGodownTagging
            OBJPROFORMA.MdiParent = Me
            OBJPROFORMA.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub USERGODOWNEDIT_Click(sender As Object, e As EventArgs) Handles USERGODOWNEDIT.Click
        Try
            Dim OBJPROFORMA As New UserGodownTaggingDetails
            OBJPROFORMA.MdiParent = Me
            OBJPROFORMA.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROVISIONALBS_MASTER_Click(sender As Object, e As EventArgs) Handles PROVISIONALBS_MASTER.Click
        Try
            Dim OBJPROVISIONAL As New ProvisionalBS
            OBJPROVISIONAL.MdiParent = Me
            OBJPROVISIONAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GROSSPROFIT_MASTER_Click(sender As Object, e As EventArgs) Handles GROSSPROFIT_MASTER.Click
        Try
            Dim OBJGROSS As New GrossProfitReport
            OBJGROSS.MdiParent = Me
            OBJGROSS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CURRENCYADD_Click(sender As Object, e As EventArgs) Handles CURRENCYADD.Click
        Try
            Dim OBJCON As New CurrencyMaster
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CURRENCYEDIT_Click(sender As Object, e As EventArgs) Handles CURRENCYEDIT.Click
        Try
            Dim OBJCON As New CurrencyMasterDetails
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GATEPASSADD_Click(sender As Object, e As EventArgs) Handles GATEPASSADD.Click
        Try
            Dim OBJCON As New SaleGatePass
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EDITGATEPASS_Click(sender As Object, e As EventArgs) Handles GATEPASSEDIT.Click
        Try
            Dim OBJCON As New SaleGatePassDetails
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UnHoldChallansToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnHoldChallansToolStripMenuItem.Click
        Try
            Dim OBJGDN As New UnHoldChallan
            OBJGDN.MdiParent = Me
            OBJGDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InHousePackingStockSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InHousePackingStockSummaryToolStripMenuItem.Click
        Try
            Dim OBJSTOCK As New InHousePackingStock
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.FRMSTRING = "SUMMARY"
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateCardRecdDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateCardRecdDateToolStripMenuItem.Click
        Try
            Dim OBJPRG As New UpdateProgCardRecdDate
            OBJPRG.MdiParent = Me
            OBJPRG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateDyeingInProgramToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateDyeingInProgramToolStripMenuItem.Click
        Try
            Dim OBJPRG As New UpdateProgDyeing
            OBJPRG.MdiParent = Me
            OBJPRG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateProgramGiveDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateProgramGiveDateToolStripMenuItem.Click
        Try
            Dim OBJPRG As New UpdateProgGivenDate
            OBJPRG.MdiParent = Me
            OBJPRG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewEntryToolStripMenuItem.Click
        Try
            Dim OBJPRG As New ShrinkageEntry
            OBJPRG.MdiParent = Me
            OBJPRG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditExistingEntryToolStripMenuItem.Click
        Try
            Dim OBJPRG As New ShrinkageEntryDetails
            OBJPRG.MdiParent = Me
            OBJPRG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateStatusOfProgramToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateStatusOfProgramToolStripMenuItem.Click
        Try
            Dim OBJPRG As New UpdateProgStatus
            OBJPRG.MdiParent = Me
            OBJPRG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ProductionCuttingRecDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductionCuttingRecDateToolStripMenuItem.Click
        Try
            Dim OBJPRG As New UpdateProdCuttingRecdDate
            OBJPRG.MdiParent = Me
            OBJPRG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FinishCuttingRecDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinishCuttingRecDateToolStripMenuItem.Click
        Try
            Dim OBJPRG As New UpdateFinishCuttingRecdDate
            OBJPRG.MdiParent = Me
            OBJPRG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GP_TOOL_Click(sender As Object, e As EventArgs) Handles GP_TOOL.Click
        Try
            Dim OBJCON As New SaleGatePass
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNPOADD_Click(sender As Object, e As EventArgs) Handles YARNPOADD.Click
        Try
            Dim OBJPO As New YarnPurchaseOrder
            OBJPO.MdiParent = Me
            OBJPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNPOEDIT_Click(sender As Object, e As EventArgs) Handles YARNPOEDIT.Click
        Try
            Dim OBJPO As New YarnPurchaseOrderDetails
            OBJPO.MdiParent = Me
            OBJPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRECDGREYADD_Click(sender As Object, e As EventArgs) Handles YARNRECDGREYADD.Click
        Try
            Dim objyarnrecd As New YarnRecd
            objyarnrecd.MdiParent = Me
            objyarnrecd.FRMSTRING = "YARNRECD"
            objyarnrecd.TYPE = "GREY"
            objyarnrecd.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRECDGREYEDIT_Click(sender As Object, e As EventArgs) Handles YARNRECDGREYEDIT.Click
        Try
            Dim objyarnRECDTLS As New YarnRecdDetails
            objyarnRECDTLS.MdiParent = Me
            objyarnRECDTLS.FRMSTRING = "YARNRECD"
            objyarnRECDTLS.TYPE = "GREY"
            objyarnRECDTLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ColorTaggingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ColorTaggingToolStripMenuItem.Click
        Try
            Dim OBJCOL As New ColorTagging
            OBJCOL.MdiParent = Me
            OBJCOL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STORECONSUMPTIONADD_Click(sender As Object, e As EventArgs) Handles STORECONSUMPTIONADD.Click
        Try
            Dim OBJSTORE As New StoreConsumption
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STORECONSUMPTIONEDIT_Click(sender As Object, e As EventArgs) Handles STORECONSUMPTIONEDIT.Click
        Try
            Dim OBJSTORE As New StoreConsumptionDetails
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STORESTOCKREPORT_MASTER_Click(sender As Object, e As EventArgs) Handles STORESTOCKREPORT_MASTER.Click
        Try
            Dim OBJSTOCK As New StoreStockFilter
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PendingInvoiceDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PendingInvoiceDetailsToolStripMenuItem.Click
        Try
            Dim OBJPENCHALLAN As New PendingChallanForInvoiceDetails
            OBJPENCHALLAN.MdiParent = Me
            OBJPENCHALLAN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseOrderToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PurchaseOrderToolStripMenuItem1.Click
        Try
            Dim OBJSO As New SOFilter
            OBJSO.FRMSTRING = "PO"
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BASEADD_Click(sender As Object, e As EventArgs) Handles BASEADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "BASE"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BASEEDIT_Click(sender As Object, e As EventArgs) Handles BASEEDIT.Click
        Try
            Dim objCategoryDetails As New CategoryDetails
            objCategoryDetails.MdiParent = Me
            objCategoryDetails.FRMSTRING = "BASE"
            objCategoryDetails.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingREgisterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditExistingREgisterToolStripMenuItem.Click
        Try
            Dim objregistermaster As New RegisterMasterDelete
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "PURCHASE"
            objregistermaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem16_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem16.Click
        Try
            Dim objregistermaster As New RegisterMasterDelete
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "SALE"
            objregistermaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem18_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem18.Click
        Try
            Dim objregistermaster As New RegisterMasterDelete
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "JOURNAL"
            objregistermaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem20_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem20.Click
        Try
            Dim objregistermaster As New RegisterMasterDelete
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "CONTRA"
            objregistermaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingExpenseRegisterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditExistingExpenseRegisterToolStripMenuItem.Click
        Try
            Dim objregistermaster As New RegisterMasterDelete
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "EXPENSE"
            objregistermaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem22_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem22.Click
        Try
            Dim objregistermaster As New RegisterMasterDelete
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "PAYMENT"
            objregistermaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem24_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem24.Click
        Try
            Dim objregistermaster As New RegisterMasterDelete
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "RECEIPT"
            objregistermaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub DeleteExistingRegisterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteExistingRegisterToolStripMenuItem.Click
        Try
            Dim objregistermaster As New RegisterMasterDelete
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "CREDITNOTE"
            objregistermaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub DeleteExistingRegisterToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteExistingRegisterToolStripMenuItem1.Click
        Try
            Dim objregistermaster As New RegisterMasterDelete
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "DEBITNOTE"
            objregistermaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub STOCKTAKINGADD_Click(sender As Object, e As EventArgs) Handles STOCKTAKINGADD.Click
        Try
            Dim OBJSTOCK As New StockTaking
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STOCKTAKINGEDIT_Click(sender As Object, e As EventArgs) Handles STOCKTAKINGEDIT.Click
        Try
            Dim OBJSTOCK As New StockTakingDetails
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UNCHECKEDSTOCK_MASTER_Click(sender As Object, e As EventArgs) Handles UNCHECKEDSTOCK_MASTER.Click
        Try
            Dim OBJSTOCK As New UnCheckedStock
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECLEDGERDIFF_MASTER_Click(sender As Object, e As EventArgs) Handles RECLEDGERDIFF_MASTER.Click
        Try
            Dim OBJDIFF As New LedgerDifference
            OBJDIFF.MdiParent = Me
            OBJDIFF.FRMSTRING = "RECEIVABLE"
            OBJDIFF.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PAYLEDGERDIFF_MASTER_Click(sender As Object, e As EventArgs) Handles PAYLEDGERDIFF_MASTER.Click
        Try
            Dim OBJDIFF As New LedgerDifference
            OBJDIFF.MdiParent = Me
            OBJDIFF.FRMSTRING = "PAYABLE"
            OBJDIFF.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub REASONADD_Click(sender As Object, e As EventArgs) Handles REASONADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "REASON"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub REASONEDIT_Click(sender As Object, e As EventArgs) Handles REASONEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.FRMSTRING = "NARRATION"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EXPORTLABEL_MASTER_Click(sender As Object, e As EventArgs) Handles EXPORTLABEL_MASTER.Click
        Try
            Dim OBJLABEL As New ExportLabel
            OBJLABEL.MdiParent = Me
            OBJLABEL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UPLOADGSTR2_MASTER_Click(sender As Object, e As EventArgs) Handles UPLOADGSTR2_MASTER.Click
        Try
            Dim OBJGSTR2 As New UploadGSTR2
            OBJGSTR2.MdiParent = Me
            OBJGSTR2.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROFORMADEBITADD_Click(sender As Object, e As EventArgs) Handles PROFORMADEBITADD.Click
        Try
            Dim OBJDN As New ProformaDebitNote
            OBJDN.MdiParent = Me
            OBJDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROFORMADEBITEDIT_Click(sender As Object, e As EventArgs) Handles PROFORMADEBITEDIT.Click
        Try
            Dim OBJDN As New ProformaDebitNoteDetails
            OBJDN.MdiParent = Me
            OBJDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROFORMACREDITADD_Click(sender As Object, e As EventArgs) Handles PROFORMACREDITADD.Click
        Try
            Dim OBJCN As New ProformaCreditNote
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROFORMACREDITEDIT_Click(sender As Object, e As EventArgs) Handles PROFORMACREDITEDIT.Click
        Try
            Dim OBJCN As New ProformaCreditNoteDetails
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROFORMASALEADD_Click(sender As Object, e As EventArgs) Handles PROFORMASALEADD.Click
        Try
            Dim OBJINVOICE As New ProformaInvoice
            OBJINVOICE.MdiParent = Me
            OBJINVOICE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROFORMASALEEDIT_Click(sender As Object, e As EventArgs) Handles PROFORMASALEEDIT.Click
        Try
            Dim OBJINVOICE As New ProformaInvoiceDetails
            OBJINVOICE.MdiParent = Me
            OBJINVOICE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNDYEINGPROGRAMADD_Click(sender As Object, e As EventArgs) Handles YARNDYEINGPROGRAMADD.Click
        Try
            Dim OBJPROG As New YarnDyeingProgram
            OBJPROG.MdiParent = Me
            OBJPROG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNDYEINGPROGRAMEDIT_Click(sender As Object, e As EventArgs) Handles YARNDYEINGPROGRAMEDIT.Click
        Try
            Dim OBJPROG As New YarnDyeingProgramDetails
            OBJPROG.MdiParent = Me
            OBJPROG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNSOADD_Click(sender As Object, e As EventArgs) Handles YARNSOADD.Click
        Try
            Dim OBJSO As New YarnSaleOrder
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNSOEDIT_Click(sender As Object, e As EventArgs) Handles YARNSOEDIT.Click
        Try
            Dim OBJSO As New YarnSaleOrderDetails
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub REORDERLEVEL_MASTER_Click(sender As Object, e As EventArgs) Handles REORDERLEVEL_MASTER.Click
        Try
            Dim OBJREORDER As New ReOrderLevel
            OBJREORDER.MdiParent = Me
            OBJREORDER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReOrderLevelReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReOrderLevelReportToolStripMenuItem.Click
        Try
            Dim OBJREORDER As New ReOrderLevelReport
            OBJREORDER.MdiParent = Me
            OBJREORDER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EMBPRODUCTIONADD_Click(sender As Object, e As EventArgs) Handles EMBPRODUCTIONADD.Click
        Try
            Dim OBJEMB As New EmbProduction
            OBJEMB.MdiParent = Me
            OBJEMB.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EMBPRODUCTIONEDIT_Click(sender As Object, e As EventArgs) Handles EMBPRODUCTIONEDIT.Click
        Try
            Dim OBJEMB As New EmbProductionDetails
            OBJEMB.MdiParent = Me
            OBJEMB.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNPROGRAMCLOSE_Click(sender As Object, e As EventArgs) Handles YARNPROGRAMCLOSE.Click
        Try
            Dim OBJPROG As New YarnDyeingProgramClose
            OBJPROG.MdiParent = Me
            OBJPROG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNPOCLOSE_Click(sender As Object, e As EventArgs) Handles YARNPOCLOSE.Click
        Try
            Dim OBJPO As New YarnPurchaseOrderClose
            OBJPO.MdiParent = Me
            OBJPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNSOCLOSE_Click(sender As Object, e As EventArgs) Handles YARNSOCLOSE.Click
        Try
            Dim OBJSO As New YarnSaleOrderClose
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNCHALLANADD_Click(sender As Object, e As EventArgs) Handles YARNCHALLANADD.Click
        Try
            Dim OBJYARN As New YarnChallan
            OBJYARN.MdiParent = Me
            OBJYARN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNCHALLANEDIT_Click(sender As Object, e As EventArgs) Handles YARNCHALLANEDIT.Click
        Try
            Dim OBJYARN As New YarnChallanDetails
            OBJYARN.MdiParent = Me
            OBJYARN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TCSCHALLAN_MASTER_Click(sender As Object, e As EventArgs) Handles TCSCHALLAN_MASTER.Click
        Try
            Dim OBJTCS As New TCSChallan
            OBJTCS.MdiParent = Me
            OBJTCS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StockRegisterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockRegisterToolStripMenuItem.Click
        Try
            Dim OBJSTOCK As New StockRegisterFilter
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LOCKPENDINGJO_MENU_Click(sender As Object, e As EventArgs) Handles LOCKPENDINGJO_MENU.Click
        Try
            Dim OBJENTRIES As New LockJobOutforEmb
            OBJENTRIES.MdiParent = Me
            OBJENTRIES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPPROGRAMADD_Click(sender As Object, e As EventArgs) Handles OPPROGRAMADD.Click
        Try
            Dim OBJPROG As New OpeningProgramMaster
            OBJPROG.MdiParent = Me
            OBJPROG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPPROGRAMEDIT_Click(sender As Object, e As EventArgs) Handles OPPROGRAMEDIT.Click
        Try
            Dim OBJPROG As New OpeningProgramDetails
            OBJPROG.MdiParent = Me
            OBJPROG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UPLOADYARNSTOCK_Click(sender As Object, e As EventArgs) Handles UPLOADYARNSTOCK.Click

        'ONE TIME DATA UPLOAD FOR SAFFRON FROM CBS
        Try
            If InputBox("Enter Master Password") <> "Infosys@123" Then Exit Sub
            'upload the files data
            ''Reading from Excel Woorkbook
            Dim cPart As Microsoft.Office.Interop.Excel.Range
            Dim oExcel As Microsoft.Office.Interop.Excel.Application = CreateObject("Excel.Application")
            Dim oBook As Microsoft.Office.Interop.Excel.Workbook = oExcel.Workbooks.Open("D:\" & InputBox("Enter File Name").ToString.Trim, , False)
            Dim oSheet As New Microsoft.Office.Interop.Excel.Worksheet
            oSheet = oBook.Worksheets("Sheet1")

            'GRID
            Dim ADDITEM As Boolean = True
            Dim TEMPITEMNAME As String = ""

            Dim DTSAVE As New System.Data.DataTable
            DTSAVE.Columns.Add("LOTNO")
            DTSAVE.Columns.Add("YARNQUALITY")
            DTSAVE.Columns.Add("MILLNAME")
            DTSAVE.Columns.Add("DESIGN")
            DTSAVE.Columns.Add("COLOR")
            DTSAVE.Columns.Add("PROCESS")
            DTSAVE.Columns.Add("PARTYNAME")
            DTSAVE.Columns.Add("GODOWN")
            DTSAVE.Columns.Add("BAGS")
            DTSAVE.Columns.Add("WT")
            DTSAVE.Columns.Add("CONES")
            DTSAVE.Columns.Add("HSNCODE")
            DTSAVE.Columns.Add("DENIER")

            Dim ARR As New ArrayList
            Dim COLIND As Integer = 0
            Dim DTROWSAVE As System.Data.DataRow = DTSAVE.NewRow()


            Dim FROMROWNO As Integer = Val(InputBox("Enter Start Row No"))
            Dim TOROWNO As Integer = Val(InputBox("Enter End Row No"))

            For I As Integer = FROMROWNO To TOROWNO

                If IsDBNull(oSheet.Range("A" & I.ToString).Text) = False Then
                    DTROWSAVE("LOTNO") = oSheet.Range("A" & I.ToString).Text
                Else
                    DTROWSAVE("LOTNO") = ""
                End If


                If IsDBNull(oSheet.Range("B" & I.ToString).Text) = False Then
                    DTROWSAVE("YARNQUALITY") = oSheet.Range("B" & I.ToString).Text
                Else
                    DTROWSAVE("YARNQUALITY") = ""
                End If


                If IsDBNull(oSheet.Range("C" & I.ToString).Text) = False Then
                    DTROWSAVE("MILLNAME") = oSheet.Range("C" & I.ToString).Text
                Else
                    DTROWSAVE("MILLNAME") = ""
                End If


                If IsDBNull(oSheet.Range("D" & I.ToString).Text) = False Then
                    DTROWSAVE("DESIGN") = oSheet.Range("D" & I.ToString).Text
                Else
                    DTROWSAVE("DESIGN") = ""
                End If


                If IsDBNull(oSheet.Range("E" & I.ToString).Text) = False Then
                    DTROWSAVE("COLOR") = oSheet.Range("E" & I.ToString).Text
                Else
                    DTROWSAVE("COLOR") = ""
                End If


                If IsDBNull(oSheet.Range("F" & I.ToString).Text) = False Then
                    DTROWSAVE("PROCESS") = oSheet.Range("F" & I.ToString).Text
                Else
                    DTROWSAVE("PROCESS") = ""
                End If

                If IsDBNull(oSheet.Range("G" & I.ToString).Text) = False Then
                    DTROWSAVE("PARTYNAME") = oSheet.Range("G" & I.ToString).Text
                Else
                    DTROWSAVE("PARTYNAME") = ""
                End If

                If IsDBNull(oSheet.Range("H" & I.ToString).Text) = False Then
                    DTROWSAVE("GODOWN") = oSheet.Range("H" & I.ToString).Text
                Else
                    DTROWSAVE("GODOWN") = ""
                End If

                If IsDBNull(oSheet.Range("I" & I.ToString).Text) = False Then
                    DTROWSAVE("BAGS") = Val(oSheet.Range("I" & I.ToString).Text)
                Else
                    DTROWSAVE("BAGS") = 0
                End If

                If IsDBNull(oSheet.Range("J" & I.ToString).Text) = False Then
                    DTROWSAVE("WT") = Val(oSheet.Range("J" & I.ToString).Text)
                Else
                    DTROWSAVE("WT") = 0
                End If

                If IsDBNull(oSheet.Range("K" & I.ToString).Text) = False Then
                    DTROWSAVE("CONES") = Val(oSheet.Range("K" & I.ToString).Text)
                Else
                    DTROWSAVE("CONES") = 0
                End If

                If IsDBNull(oSheet.Range("L" & I.ToString).Text) = False Then
                    DTROWSAVE("HSNCODE") = oSheet.Range("L" & I.ToString).Text
                Else
                    DTROWSAVE("HSNCODE") = ""
                End If

                If IsDBNull(oSheet.Range("M" & I.ToString).Text) = False Then
                    DTROWSAVE("DENIER") = Val(oSheet.Range("M" & I.ToString).Text)
                Else
                    DTROWSAVE("DENIER") = 0
                End If



                If Val(DTROWSAVE("WT")) = 0 Then GoTo SKIPLINE


                Dim ALPARAVAL As New ArrayList
                'CHECK WHETHER YARNQUALITY IS PRESENT OR NOT IF NOT PRESENT THEN ADD NEW
                Dim OBJCMN As New ClsCommon
                Dim DTTABLE As New DataTable
                If DTROWSAVE("YARNQUALITY") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("YARN_ID AS YARNID", "", "YARNQUALITYMASTER ", "AND YARN_NAME = '" & DTROWSAVE("YARNQUALITY") & "' AND YARN_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW YARNQUALITY 
                        ALPARAVAL.Clear()


                        ALPARAVAL.Add(UCase(DTROWSAVE("YARNQUALITY")))     'QUALITYNAME
                        ALPARAVAL.Add("")   'CATEGORY
                        ALPARAVAL.Add("")   'REMARKS
                        ALPARAVAL.Add(0)    'BOXWT

                        Dim DTHSN As DataTable = OBJCMN.SEARCH("ISNULL(HSN_ID, 0) AS HSNCODEID", "", " HSNMASTER", " AND HSN_CODE = '" & DTROWSAVE("HSNCODE") & "' AND HSN_YEARID = " & YearId)
                        If DTHSN.Rows.Count > 0 Then ALPARAVAL.Add(DTROWSAVE("HSNCODE")) Else ALPARAVAL.Add(0) 'HSNCODEID

                        ALPARAVAL.Add(DTROWSAVE("DENIER"))     'DENIER
                        ALPARAVAL.Add(0)    'RATE

                        ALPARAVAL.Add(UCase(DTROWSAVE("YARNQUALITY")))     'GRIDQUALITYNAME
                        ALPARAVAL.Add(100)     'GRIDPERCENTAGE

                        ALPARAVAL.Add("")   'STORESRNO
                        ALPARAVAL.Add("")   'STOREITEMNAME
                        ALPARAVAL.Add("")   'STOREQTY


                        ALPARAVAL.Add(CmpId)
                        ALPARAVAL.Add(Userid)
                        ALPARAVAL.Add(YearId)

                        Dim OBJYARN As New ClsYarnQualityMaster
                        OBJYARN.alParaval = ALPARAVAL
                        Dim IntResult As Integer = OBJYARN.SAVE()

                    End If
                End If


                'DESIGN SAVE
                If DTROWSAVE("DESIGN") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("DESIGN_ID AS DESIGNID", "", "DESIGNMASTER", " AND DESIGN_NO = '" & DTROWSAVE("DESIGN") & "' AND DESIGN_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW DESIGN
                        Dim OBJDESIGN As New ClsDesignMaster
                        OBJDESIGN.alParaval.Add(UCase(DTROWSAVE("DESIGN")))
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

                        OBJDESIGN.alParaval.Add("") 'GRIDSRNO
                        OBJDESIGN.alParaval.Add("") 'BASE
                        OBJDESIGN.alParaval.Add("") 'PRINT
                        OBJDESIGN.alParaval.Add("") 'COLOR

                        Dim INTRESCAT As Integer = OBJDESIGN.SAVE()
                    End If
                End If



                'COLOR SAVE
                If DTROWSAVE("COLOR") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("COLOR_ID AS COLORID", "", "COLORMASTER", " AND COLOR_NAME = '" & DTROWSAVE("COLOR") & "' AND COLOR_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW DESIGN
                        Dim OBJCOLOR As New ClsColorMaster
                        OBJCOLOR.alParaval.Add(UCase(DTROWSAVE("COLOR")))
                        OBJCOLOR.alParaval.Add("")
                        OBJCOLOR.alParaval.Add(CmpId)
                        OBJCOLOR.alParaval.Add(Locationid)
                        OBJCOLOR.alParaval.Add(Userid)
                        OBJCOLOR.alParaval.Add(YearId)
                        OBJCOLOR.alParaval.Add(0)

                        Dim INTRESCAT As Integer = OBJCOLOR.save()
                    End If
                End If



                'PROCESS SAVE
                If DTROWSAVE("PROCESS") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("PROCESS_ID AS PROCESSID", "", "PROCESSMASTER", " AND PROCESS_NAME = '" & DTROWSAVE("PROCESS") & "' AND PROCESS_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW QUALITY
                        Dim OBJPROCESS As New ClsProcessMaster
                        OBJPROCESS.alParaval.Add(UCase(DTROWSAVE("PROCESS")))
                        OBJPROCESS.alParaval.Add("") 'REMAKS

                        OBJPROCESS.alParaval.Add("") 'WARP
                        OBJPROCESS.alParaval.Add("") 'WEFT
                        OBJPROCESS.alParaval.Add("") 'SELVEDGE


                        OBJPROCESS.alParaval.Add(CmpId)
                        OBJPROCESS.alParaval.Add(Locationid)
                        OBJPROCESS.alParaval.Add(Userid)
                        OBJPROCESS.alParaval.Add(YearId)
                        OBJPROCESS.alParaval.Add(0)
                        Dim INTRESCAT As Integer = OBJPROCESS.save()
                    End If
                End If




                'ADD IN STOCKMASTER
                ALPARAVAL.Clear()
                Dim OBJSM As New ClsOpeningStockYarn

                ALPARAVAL.Add(AccFrom.Date)
                ALPARAVAL.Add("GODOWNSTOCKYARN")

                ALPARAVAL.Add(I)
                ALPARAVAL.Add(DTROWSAVE("LOTNO"))               'LOTNO
                ALPARAVAL.Add(DTROWSAVE("YARNQUALITY"))     'QUALITY
                ALPARAVAL.Add(DTROWSAVE("MILLNAME"))        'MILLNAME
                ALPARAVAL.Add(DTROWSAVE("DESIGN"))          'DESIGNNO
                ALPARAVAL.Add(DTROWSAVE("COLOR"))           'COLOR    
                ALPARAVAL.Add(DTROWSAVE("PROCESS"))         'PROCESS
                ALPARAVAL.Add("")                           'LRNO
                ALPARAVAL.Add(AccFrom.Date)                 'LRDATE
                ALPARAVAL.Add(DTROWSAVE("PARTYNAME"))       'NAME
                ALPARAVAL.Add(DTROWSAVE("GODOWN"))          'GODOWN
                ALPARAVAL.Add(Val(DTROWSAVE("BAGS")))       'BAGS
                ALPARAVAL.Add(Val(DTROWSAVE("WT")))         'WT
                ALPARAVAL.Add(Val(DTROWSAVE("CONES")))      'CONES

                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                ALPARAVAL.Add(0)

                OBJSM.alParaval = ALPARAVAL
                DTTABLE = OBJSM.save()


                DTROWSAVE = DTSAVE.NewRow()
SKIPLINE:
            Next
            oBook.Close()

            Exit Sub
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ContractorReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContractorReportToolStripMenuItem.Click
        Try
            Dim OBJREC As New RecFromPackFilter
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UNUSEDLEDGERS_MASTER_Click(sender As Object, e As EventArgs) Handles UNUSEDLEDGERS_MASTER.Click
        Try
            Dim OBJLEDGERS As New UnusedLedgers
            OBJLEDGERS.MdiParent = Me
            OBJLEDGERS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LEDGERBOOKWITHTDS_MASTER_Click(sender As Object, e As EventArgs) Handles LEDGERBOOKWITHTDS_MASTER.Click
        Try
            Dim objledgerbook As New LedgerBookWithTDS
            objledgerbook.MdiParent = Me
            objledgerbook.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub REPLACELOTNO_MASTER_Click(sender As Object, e As EventArgs) Handles REPLACELOTNO_MASTER.Click
        Try
            Dim OBJLOT As New ReplaceLotNo
            OBJLOT.MdiParent = Me
            OBJLOT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WHATSAPPREG_MASTER_Click(sender As Object, e As EventArgs) Handles WHATSAPPREG_MASTER.Click
        Try
            Dim OBJWHATSAPP As New WhatsappRegistration
            OBJWHATSAPP.MdiParent = Me
            OBJWHATSAPP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALERETURNCHALLANADD_Click(sender As Object, e As EventArgs) Handles SALERETURNCHALLANADD.Click
        Try
            Dim OBJSR As New SaleReturnChallan
            OBJSR.MdiParent = Me
            OBJSR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALERETURNCHALLANEDIT_Click(sender As Object, e As EventArgs) Handles SALERETURNCHALLANEDIT.Click
        Try
            Dim OBJSR As New SaleReturnChallanDetails
            OBJSR.MdiParent = Me
            OBJSR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EwayEntryDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EwayEntryDetailsToolStripMenuItem.Click
        Try
            Dim OBJEWAY As New EwayCounterReport
            OBJEWAY.MdiParent = Me
            OBJEWAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALEAUTOPOSTADD_Click(sender As Object, e As EventArgs) Handles SALEAUTOPOSTADD.Click
        Try
            Dim OBJTDS As New SaleAutoTDS
            OBJTDS.MdiParent = Me
            OBJTDS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ITEMPACKINGCONFIG_MASTER_Click(sender As Object, e As EventArgs) Handles ITEMPACKINGCONFIG_MASTER.Click
        Try
            Dim OBJITEM As New ItemPackingMaterial
            OBJITEM.MdiParent = Me
            OBJITEM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TALLYEXPORTSALES_Click(sender As Object, e As EventArgs) Handles TALLYEXPORTSALES.Click
        Try
            Dim OBJTALLY As New TallyDataExportSales
            OBJTALLY.FRMSTRING = "Sales"
            OBJTALLY.MdiParent = Me
            OBJTALLY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CATALOGUE_MASTER_Click(sender As Object, e As EventArgs) Handles CATALOGUE_MASTER.Click
        Try
            Dim OBJCATALOG As New CatalogMaster
            OBJCATALOG.MdiParent = Me
            OBJCATALOG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CATALOG_REPORTS_Click(sender As Object, e As EventArgs) Handles CATALOG_REPORTS.Click
        Try
            Dim OBJCATALOG As New CatalogFilter
            OBJCATALOG.MdiParent = Me
            OBJCATALOG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EINVOICECOUNTERREPORT_MASTER_Click(sender As Object, e As EventArgs) Handles EINVOICECOUNTERREPORT_MASTER.Click
        Try
            Dim OBJEINV As New EInvoiceCounterReport
            OBJEINV.MdiParent = Me
            OBJEINV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AddNewEntryToolStripMenuItem1.Click
        Try
            Dim OBJSHELF As New UpdateRackShelf
            OBJSHELF.MdiParent = Me
            OBJSHELF.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingEntryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditExistingEntryToolStripMenuItem1.Click
        Try
            Dim OBJSHELF As New UpdateRackShelfDetails
            OBJSHELF.MdiParent = Me
            OBJSHELF.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles BEAMPROGADD.Click
        Try
            Dim OBJBP As New BeamProgram
            OBJBP.MdiParent = Me
            OBJBP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BEAMPROGEDIT.Click
        Try
            Dim OBJPRJ As New BeamProgramDetails
            OBJPRJ.MdiParent = Me
            OBJPRJ.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPGDNADD_Click(sender As Object, e As EventArgs) Handles OPGDNADD.Click
        Try
            Dim OBJGDN As New OpeningGDN
            OBJGDN.MdiParent = Me
            OBJGDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPGDNEDIT_Click(sender As Object, e As EventArgs) Handles OPGDNEDIT.Click
        Try
            Dim OBJGDN As New OpeningGDNDetails
            OBJGDN.MdiParent = Me
            OBJGDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLANSTOCKREG_MASTER_Click(sender As Object, e As EventArgs) Handles CHALLANSTOCKREG_MASTER.Click
        Try
            Dim OBJSTOCK As New StockRegisterChallanWise
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ErrorInOpeningBillsReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ErrorInOpeningBillsReportToolStripMenuItem.Click
        Try
            Dim OBJERROR As New ErrorOpeningBills
            OBJERROR.MdiParent = Me
            OBJERROR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURCHASEITCADD_Click(sender As Object, e As EventArgs) Handles PURCHASEITCADD.Click
        Try
            Dim OBJITC As New PurchaseITCEntry
            OBJITC.MdiParent = Me
            OBJITC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURCHASEITCEDIT_Click(sender As Object, e As EventArgs) Handles PURCHASEITCEDIT.Click
        Try
            Dim OBJITC As New PurchaseITCDetails
            OBJITC.MdiParent = Me
            OBJITC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PURRETCHALLANADD.Click
        Try
            Dim OBJITC As New PurchaseReturnChallan
            OBJITC.MdiParent = Me
            OBJITC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingEntryToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PURRETCHALLANEDIT.Click
        Try
            Dim OBJITC As New PurchaseReturnChallanDetail
            OBJITC.MdiParent = Me
            OBJITC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateCardIssueDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateCardIssueDateToolStripMenuItem.Click
        Try
            Dim UPISS As New UpdateCardProgIssueDate
            UPISS.MdiParent = Me
            UPISS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UPDATELOGS_MASTER_Click(sender As Object, e As EventArgs) Handles UPDATELOGS_MASTER.Click
        Try
            Dim OBJLOGS As New LogsUpdate
            OBJLOGS.MdiParent = Me
            OBJLOGS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DELETELOGS_MASTER_Click(sender As Object, e As EventArgs) Handles DELETELOGS_MASTER.Click
        Try
            Dim OBJLOGS As New LogsDelete
            OBJLOGS.MdiParent = Me
            OBJLOGS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewDistrictToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewDistrictToolStripMenuItem.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "DISTRICT"
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingDistrictToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditExistingDistrictToolStripMenuItem.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.FRMSTRING = "DISTRICT"
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub JobberItemShadeWiseStockDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JobberItemShadeWiseStockDetailsToolStripMenuItem.Click
        Try
            Dim OBJSTOCK As New ItemShadeWiseJobberStockDetails
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateApprovedProgramToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateApprovedProgramToolStripMenuItem.Click
        Try
            Dim OBJUPDATEAPPROVEDPROGRAM As New UpdateApprovedProgram
            OBJUPDATEAPPROVEDPROGRAM.MdiParent = Me
            OBJUPDATEAPPROVEDPROGRAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURCHASERETURNREPORTS_Click(sender As Object, e As EventArgs) Handles PURCHASERETURNREPORTS.Click
        Try
            Dim OBJSR As New PurchaseReturnFilter
            OBJSR.MdiParent = Me
            OBJSR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SOREPORT_MASTER_Click(sender As Object, e As EventArgs) Handles SOREPORT_MASTER.Click
        Try
            Dim OBJSONO As New SONoWiseReport
            OBJSONO.MdiParent = Me
            OBJSONO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewBookToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SAMPLEBOOKLETADD.Click
        Try
            Dim objbooklet As New SampleBookletCreation
            objbooklet.MdiParent = Me
            objbooklet.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SampleBookletStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SAMPLESTOCK_REPORT.Click
        Try
            Dim OBJSMP As New SampleBookletStockGridReport
            OBJSMP.MdiParent = Me
            OBJSMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingBookletToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SAMPLEBOOKLETEDIT.Click
        Try
            Dim OBJSMP As New SampleBookletCreationDetails
            OBJSMP.MdiParent = Me
            OBJSMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SENDWHATSAPP_MASTER_Click(sender As Object, e As EventArgs) Handles SENDWHATSAPP_MASTER.Click
        Try
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.MdiParent = Me
            OBJWHATSAPP.FRMSTRING = "DIRECTWHATSAPP"
            OBJWHATSAPP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AUTODISCOUNT_MASTER_Click(sender As Object, e As EventArgs) Handles AUTODISCOUNT_MASTER.Click
        Try
            If InputBox("Enter Master Password") <> "Infosys@123" Then Exit Sub
            Dim OBJDISC As New AutoDiscount
            OBJDISC.MdiParent = Me
            OBJDISC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IncentiveDetails_Click(sender As Object, e As EventArgs) Handles IncentiveDetails.Click
        Try
            Dim objincentive As New EmbIncentiveDetails
            objincentive.MdiParent = Me
            objincentive.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECADD_Click(sender As Object, e As EventArgs) Handles GREYRECADD.Click
        Try
            Dim OBJGREYREC As New GreyReceivedTransport
            OBJGREYREC.MdiParent = Me
            OBJGREYREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECEDIT_Click(sender As Object, e As EventArgs) Handles GREYRECEDIT.Click
        Try
            Dim OBJGREYREC As New GreyReceivedTransportDetails
            OBJGREYREC.MdiParent = Me
            OBJGREYREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYISSUEPROCESSADD_Click(sender As Object, e As EventArgs) Handles GREYISSUEPROCESSADD.Click
        Try
            Dim OBJISSP As New GreyIssueProcess
            OBJISSP.MdiParent = Me
            OBJISSP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYISSUEPROCESSEDIT_Click(sender As Object, e As EventArgs) Handles GREYISSUEPROCESSEDIT.Click
        Try
            Dim OBJISS As New GreyIssueProcessDetails
            OBJISS.MdiParent = Me
            OBJISS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ADDNEWEMPLOYEE_Click(sender As Object, e As EventArgs) Handles EMPLOYEEADD.Click
        Try
            Dim OBJEMP As New EmployeeMaster
            OBJEMP.MdiParent = Me
            OBJEMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EDITEXISTINGEMPLOYEE_Click(sender As Object, e As EventArgs) Handles EMPLOYEEEDIT.Click
        Try
            Dim OBJEMP As New EmployeeDetails
            OBJEMP.MdiParent = Me
            OBJEMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALARYSLIPADD_Click(sender As Object, e As EventArgs) Handles SALARYSLIPADD.Click
        Try
            Dim OBJSAL As New SalaryMaster
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALARYSLIPEDIT_Click(sender As Object, e As EventArgs) Handles SALARYSLIPEDIT.Click
        Try
            Dim OBJSAL As New SalaryDetails
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ADDPENDINGENTRY_Click(sender As Object, e As EventArgs) Handles ADDPENDINGENTRY.Click
        Try
            Dim OBJPENDING As New UpdatePendingEntry
            OBJPENDING.MdiParent = Me
            OBJPENDING.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EDITPENDINGENTRY_Click(sender As Object, e As EventArgs) Handles EDITPENDINGENTRY.Click
        Try
            Dim OBJPENDING As New UpdatePendingEntryDetails
            OBJPENDING.MdiParent = Me
            OBJPENDING.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyStockTransportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GreyStockTransportToolStripMenuItem.Click
        Try
            Dim OBJGREY As New GreyStockTransportGridReport
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyStockProcessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GreyStockProcessToolStripMenuItem.Click
        Try
            Dim OBJGREY As New GreyStockProcessGridReport
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningBankRecoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OPENINGBANKRECO_MASTER.Click
        Try
            Dim OBJGREY As New OpeningBankReco
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UPLOADBANKDETAILS_Click(sender As Object, e As EventArgs) Handles UPLOADBANKDETAILS.Click
        Try
            Dim OBJUPLOADBANK As New CmpBankDetails
            OBJUPLOADBANK.MdiParent = Me
            OBJUPLOADBANK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ADDNEWIMAGE_Click(sender As Object, e As EventArgs) Handles ADDNEWIMAGE.Click
        Try
            Dim OBJIMAGE As New ItemDesignImage
            OBJIMAGE.MdiParent = Me
            OBJIMAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EDITEXISTINGIMAGE_Click(sender As Object, e As EventArgs) Handles EDITEXISTINGIMAGE.Click
        Try
            Dim OBJIMAGE As New ItemDesignImageDetails
            OBJIMAGE.MdiParent = Me
            OBJIMAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningStock_Click(sender As Object, e As EventArgs) Handles OpeningStock.Click
        Try
            Dim OBJSTOCK As New OpeningStockReport
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPENINGREYTRANSPORTSTOCK_Click(sender As Object, e As EventArgs) Handles OPENINGREYTRANSPORTSTOCK.Click
        Try
            Dim OBJSTOCK As New OpeningStockGreyTransport
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPENINGREYISSUESTOCK_Click(sender As Object, e As EventArgs) Handles OPENINGREYISSUESTOCK.Click
        Try
            Dim OBJSTOCK As New OpeningStockGreyIssueProcess
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RACKUPDATE_TOOL_Click(sender As Object, e As EventArgs) Handles RACKUPDATE_TOOL.Click
        Try
            Dim OBJRACK As New UpdateRackShelf
            OBJRACK.MdiParent = Me
            OBJRACK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub NOUSE_MASTER_Click(sender As Object, e As EventArgs) Handles NOUSE_MASTER.Click
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("CMP_ID AS CMPID", "", " CMPMASTER", " AND CMP_PASSWORD = 'wipro@123'")
            If DT.Rows.Count > 0 Then
                Dim TCMPID As Integer = Val(DT.Rows(0).Item("CMPID"))
                Dim OBJCMP As New ClsYearMaster
                OBJCMP.alParaval.Add(TCMPID)
                Dim INT As Integer = OBJCMP.DELETECMP()
                MsgBox("Company Deleted Successfully")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JOURNAL_RENUM_Click(sender As Object, e As EventArgs) Handles JOURNAL_RENUM.Click
        Try

            If InputBox("Enter Master Password", "TEXTRADE") <> "Infosys@123" Then
                MsgBox("Invalid Password")
                Exit Sub
            End If

            If MsgBox("You are about to Renumber Entries, Please Take Backup First, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If MsgBox("Wish To Renumber Jounal Entries?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then JOURNALRENUMBERING()
            MsgBox("Re-Numbering Done Successfully")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub JOURNALRENUMBERING()
        Try
            'RENUMBER JOUNRAL VOUCHER FIRST
            Dim OBJCMN As New ClsCommon
            Dim DTREGISTER As DataTable = OBJCMN.SEARCH("REGISTER_ID AS REGID, REGISTER_NAME AS REGNAME, REGISTER_ABBR AS REGABBR, REGISTER_INITIALS AS REGINITIALS", "", " REGISTERMASTER ", " AND REGISTER_TYPE = 'JOURNAL' AND REGISTER_YEARID = " & YearId)
            If DTREGISTER.Rows.Count > 0 Then
                For Each REGROW As DataRow In DTREGISTER.Rows

                    'SEARCH ENTRY IN THIS REGISTER AND ADD IN TEMP TABLE
                    'FROM THIS TEMPTABLE WE WILL UPDATE THE ENTRIES
                    'WE WILL UPDATE THE ENTRYIES FROM THEIR OLDINITIALS, AND REPLACE WITH NEW INITIALS
                    'BUT NEW INITIALS WILL CONTAIN A SPECIAL CHARACTER SO THAT WE CAN DISTINGUISH BETWEEN OLD AND NEW INITIALS\
                    'ONCE ALL ENTRIES ARE UPDATED WE WILL REMOVE THAT SPECIAL CHARACTER
                    Dim DTENTRY As DataTable = OBJCMN.SEARCH("DISTINCT  JOURNAL_NO AS OLDNO, journal_registerid AS REGID, JOURNAL_DATE AS DATE, journal_initials AS OLDINITIALS, journal_yearid AS YEARID ", "", " JOURNALMASTER ", " AND journal_registerid = " & REGROW("REGID") & " AND JOURNAL_YEARID = " & YearId & " ORDER BY journal_registerid, journal_date,journal_no")
                    Dim NEWBILLNO As Integer = 1
                    Dim TEMPDT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPRENUMBERING WHERE YEARID = " & YearId, "", "")
                    For Each DTROW As DataRow In DTENTRY.Rows
                        TEMPDT = OBJCMN.Execute_Any_String("INSERT INTO TEMPRENUMBERING VALUES (" & DTROW("REGID") & ",'" & Format(Convert.ToDateTime(DTROW("DATE")).Date, "MM/dd/yyyy") & "'," & DTROW("YEARID") & "," & DTROW("OLDNO") & ",'" & DTROW("OLDINITIALS") & "'," & NEWBILLNO & ",'" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N')", "", "")

                        'UPDATE IN JOURNALMASTER
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_NO = " & NEWBILLNO & ",JOURNAL_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE JOURNAL_INITIALS = '" & DTROW("OLDINITIALS") & "' AND JOURNAL_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_REFNO = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE JOURNAL_REFNO = '" & DTROW("OLDINITIALS") & "' AND JOURNAL_YEARID = " & YearId, "", "")

                        'UPDATE IN ACCMASTER 
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE ACCMASTER SET ACC_BILLNO = " & NEWBILLNO & ",ACC_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE ACC_INITIALS = '" & DTROW("OLDINITIALS") & "' AND ACC_YEARID = " & YearId, "", "")

                        'UPDATE IN PAYMENT MASTER
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_DESC SET PAYMENT_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE PAYMENT_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND PAYMENT_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_GRIDDESC SET PAYMENT_PAYBILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE PAYMENT_PAYBILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND PAYMENT_YEARID = " & YearId, "", "")

                        'UPDATE IN RECEIPTMASTER 
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_DESC SET RECEIPT_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE RECEIPT_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND RECEIPT_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_GRIDDESC SET RECEIPT_PAYBILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE RECEIPT_PAYBILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND RECEIPT_YEARID = " & YearId, "", "")


                        'UPDATE IN CREDITNOTEMASTER_BILLDESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE CREDITNOTEMASTER_BILLDESC SET CN_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE CN_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND CN_YEARID = " & YearId, "", "")

                        'UPDATE IN DEBITNOTEMASTER_BILLDESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE DEBITNOTEMASTER_BILLDESC SET DN_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE DN_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND DN_YEARID = " & YearId, "", "")

                        'UPDATE IN MANUALMATCHING_DESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE MANUALMATCHING_DESC SET MATCH_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE MATCH_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND MATCH_YEARID = " & YearId, "", "")


                        'UPDATE IN TDSCHALLAN
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE TDSCHALLAN SET TDSCHA_BILLNO = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE TDSCHA_BILLNO = '" & DTROW("OLDINITIALS") & "' AND TDSCHA_YEARID = " & YearId, "", "")


                        NEWBILLNO += 1
                    Next

                    'REMOVE SPECIAL CHARACTER FROM INTIIALS
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_INITIALS = LEFT(JOURNAL_INITIALS, LEN(JOURNAL_INITIALS)-1) WHERE JOURNAL_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_REFNO = LEFT(JOURNAL_REFNO, LEN(JOURNAL_REFNO)-1) FROM JOURNALMASTER INNER JOIN TEMPRENUMBERING ON NEWINITIALS = JOURNAL_REFNO AND YEARID = JOURNAL_YEARID WHERE JOURNAL_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE ACCMASTER SET ACC_INITIALS = LEFT(ACC_INITIALS, LEN(ACC_INITIALS)-1) WHERE ACC_YEARID = " & YearId & " AND ACC_TYPE = 'JOURNAL' AND ACC_REGTYPE = '" & REGROW("REGABBR") & "'", "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_DESC SET PAYMENT_BILLINITIALS = LEFT(PAYMENT_BILLINITIALS, LEN(PAYMENT_BILLINITIALS)-1) FROM PAYMENTMASTER_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = PAYMENT_BILLINITIALS AND YEARID = PAYMENT_YEARID  WHERE PAYMENT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_GRIDDESC SET PAYMENT_PAYBILLINITIALS = LEFT(PAYMENT_PAYBILLINITIALS, LEN(PAYMENT_PAYBILLINITIALS)-1) FROM PAYMENTMASTER_GRIDDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = PAYMENT_PAYBILLINITIALS AND YEARID = PAYMENT_YEARID WHERE PAYMENT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_DESC SET RECEIPT_BILLINITIALS = LEFT(RECEIPT_BILLINITIALS, LEN(RECEIPT_BILLINITIALS)-1) FROM RECEIPTMASTER_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = RECEIPT_BILLINITIALS AND YEARID = RECEIPT_YEARID  WHERE RECEIPT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_GRIDDESC SET RECEIPT_PAYBILLINITIALS = LEFT(RECEIPT_PAYBILLINITIALS, LEN(RECEIPT_PAYBILLINITIALS)-1) FROM RECEIPTMASTER_GRIDDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = RECEIPT_PAYBILLINITIALS AND YEARID = RECEIPT_YEARID WHERE RECEIPT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE CREDITNOTEMASTER_BILLDESC SET CN_BILLINITIALS = LEFT(CN_BILLINITIALS, LEN(CN_BILLINITIALS)-1) FROM CREDITNOTEMASTER_BILLDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = CN_BILLINITIALS AND YEARID = CN_YEARID  WHERE CN_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE DEBITNOTEMASTER_BILLDESC SET DN_BILLINITIALS = LEFT(DN_BILLINITIALS, LEN(DN_BILLINITIALS)-1) FROM DEBITNOTEMASTER_BILLDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = DN_BILLINITIALS AND YEARID = DN_YEARID  WHERE DN_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE MANUALMATCHING_DESC SET MATCH_BILLINITIALS = LEFT(MATCH_BILLINITIALS, LEN(MATCH_BILLINITIALS)-1) FROM MANUALMATCHING_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = MATCH_BILLINITIALS AND YEARID = MATCH_YEARID  WHERE MATCH_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE TDSCHALLAN SET TDSCHA_BILLNO = LEFT(TDSCHA_BILLNO, LEN(TDSCHA_BILLNO)-1) FROM TDSCHALLAN INNER JOIN TEMPRENUMBERING ON NEWINITIALS = TDSCHA_BILLNO AND YEARID = TDSCHA_YEARID WHERE TDSCHA_YEARID = " & YearId, "", "")
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECEIPT_RENUM_Click(sender As Object, e As EventArgs) Handles RECEIPT_RENUM.Click
        Try

            If InputBox("Enter Master Password", "TEXTRADE") <> "Infosys@123" Then
                MsgBox("Invalid Password")
                Exit Sub
            End If

            If MsgBox("You are about to Renumber Entries, Please Take Backup First, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If MsgBox("Wish To Renumber Receipt Entries?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then RECEIPTRENUMBERING()
            MsgBox("Re-Numbering Done Successfully")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub RECEIPTRENUMBERING()
        Try
            'RENUMBER JOUNRAL VOUCHER FIRST
            Dim OBJCMN As New ClsCommon
            Dim DTREGISTER As DataTable = OBJCMN.SEARCH("REGISTER_ID AS REGID, REGISTER_NAME AS REGNAME, REGISTER_ABBR AS REGABBR, REGISTER_INITIALS AS REGINITIALS", "", " REGISTERMASTER ", " AND REGISTER_TYPE = 'RECEIPT' AND REGISTER_YEARID = " & YearId)
            If DTREGISTER.Rows.Count > 0 Then
                For Each REGROW As DataRow In DTREGISTER.Rows

                    'SEARCH ENTRY IN THIS REGISTER AND ADD IN TEMP TABLE
                    'FROM THIS TEMPTABLE WE WILL UPDATE THE ENTRIES
                    'WE WILL UPDATE THE ENTRYIES FROM THEIR OLDINITIALS, AND REPLACE WITH NEW INITIALS
                    'BUT NEW INITIALS WILL CONTAIN A SPECIAL CHARACTER SO THAT WE CAN DISTINGUISH BETWEEN OLD AND NEW INITIALS\
                    'ONCE ALL ENTRIES ARE UPDATED WE WILL REMOVE THAT SPECIAL CHARACTER
                    Dim DTENTRY As DataTable = OBJCMN.SEARCH("DISTINCT  RECEIPT_NO AS OLDNO, RECEIPT_registerid AS REGID, RECEIPT_DATE AS DATE, RECEIPT_initials AS OLDINITIALS, RECEIPT_yearid AS YEARID ", "", " RECEIPTMASTER ", " AND RECEIPT_registerid = " & REGROW("REGID") & " AND RECEIPT_YEARID = " & YearId & " ORDER BY RECEIPT_registerid, RECEIPT_date, RECEIPT_no")
                    Dim NEWBILLNO As Integer = 1
                    Dim TEMPDT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPRENUMBERING WHERE YEARID = " & YearId, "", "")
                    For Each DTROW As DataRow In DTENTRY.Rows
                        TEMPDT = OBJCMN.Execute_Any_String("INSERT INTO TEMPRENUMBERING VALUES (" & DTROW("REGID") & ",'" & Format(Convert.ToDateTime(DTROW("DATE")).Date, "MM/dd/yyyy") & "'," & DTROW("YEARID") & "," & DTROW("OLDNO") & ",'" & DTROW("OLDINITIALS") & "'," & NEWBILLNO & ",'" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N')", "", "")


                        'UPDATE IN RECEIPT MASTER
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER SET RECEIPT_NO = " & NEWBILLNO & ", RECEIPT_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE RECEIPT_INITIALS = '" & DTROW("OLDINITIALS") & "' AND RECEIPT_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_DESC SET RECEIPT_NO = " & NEWBILLNO & ", RECEIPT_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE RECEIPT_INITIALS = '" & DTROW("OLDINITIALS") & "' AND RECEIPT_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_GRIDDESC SET RECEIPT_NO = " & NEWBILLNO & ", RECEIPT_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE RECEIPT_INITIALS = '" & DTROW("OLDINITIALS") & "' AND RECEIPT_YEARID = " & YearId, "", "")


                        'UPDATE IN JOURNALMASTER
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_REFNO = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE JOURNAL_REFNO = '" & DTROW("OLDINITIALS") & "' AND JOURNAL_YEARID = " & YearId, "", "")


                        'UPDATE IN ACCMASTER 
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE ACCMASTER SET ACC_BILLNO = " & NEWBILLNO & ",ACC_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE ACC_INITIALS = '" & DTROW("OLDINITIALS") & "' AND ACC_YEARID = " & YearId, "", "")


                        'UPDATE IN PAYMENTMASTER 
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_DESC SET PAYMENT_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE PAYMENT_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND PAYMENT_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_GRIDDESC SET PAYMENT_PAYBILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE PAYMENT_PAYBILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND PAYMENT_YEARID = " & YearId, "", "")

                        'UPDATE IN CREDITNOTEMASTER_BILLDESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE CREDITNOTEMASTER_BILLDESC SET CN_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE CN_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND CN_YEARID = " & YearId, "", "")

                        'UPDATE IN DEBITNOTEMASTER_BILLDESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE DEBITNOTEMASTER_BILLDESC SET DN_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE DN_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND DN_YEARID = " & YearId, "", "")

                        'UPDATE IN MANUALMATCHING_DESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE MANUALMATCHING_DESC SET MATCH_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE MATCH_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND MATCH_YEARID = " & YearId, "", "")


                        NEWBILLNO += 1
                    Next

                    'REMOVE SPECIAL CHARACTER FROM INTIIALS
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER SET RECEIPT_INITIALS = LEFT(RECEIPT_INITIALS, LEN(RECEIPT_INITIALS)-1) FROM RECEIPTMASTER INNER JOIN TEMPRENUMBERING ON NEWINITIALS = RECEIPT_INITIALS AND YEARID = RECEIPT_YEARID  WHERE RECEIPT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_DESC SET RECEIPT_INITIALS = LEFT(RECEIPT_INITIALS, LEN(RECEIPT_INITIALS)-1) FROM RECEIPTMASTER_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = RECEIPT_INITIALS AND YEARID = RECEIPT_YEARID  WHERE RECEIPT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_GRIDDESC SET RECEIPT_INITIALS = LEFT(RECEIPT_INITIALS, LEN(RECEIPT_INITIALS)-1) FROM RECEIPTMASTER_GRIDDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = RECEIPT_INITIALS AND YEARID = RECEIPT_YEARID WHERE RECEIPT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_REFNO = LEFT(JOURNAL_REFNO, LEN(JOURNAL_REFNO)-1) FROM JOURNALMASTER INNER JOIN TEMPRENUMBERING ON NEWINITIALS = JOURNAL_REFNO AND YEARID = JOURNAL_YEARID WHERE JOURNAL_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE ACCMASTER SET ACC_INITIALS = LEFT(ACC_INITIALS, LEN(ACC_INITIALS)-1) WHERE ACC_YEARID = " & YearId & " AND ACC_TYPE = 'RECEIPT' AND ACC_REGTYPE = '" & REGROW("REGABBR") & "'", "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_DESC SET PAYMENT_BILLINITIALS = LEFT(PAYMENT_BILLINITIALS, LEN(PAYMENT_BILLINITIALS)-1) FROM PAYMENTMASTER_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = PAYMENT_BILLINITIALS AND YEARID = PAYMENT_YEARID  WHERE PAYMENT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_GRIDDESC SET PAYMENT_PAYBILLINITIALS = LEFT(PAYMENT_PAYBILLINITIALS, LEN(PAYMENT_PAYBILLINITIALS)-1) FROM PAYMENTMASTER_GRIDDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = PAYMENT_PAYBILLINITIALS AND YEARID = PAYMENT_YEARID WHERE PAYMENT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE CREDITNOTEMASTER_BILLDESC SET CN_BILLINITIALS = LEFT(CN_BILLINITIALS, LEN(CN_BILLINITIALS)-1) FROM CREDITNOTEMASTER_BILLDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = CN_BILLINITIALS AND YEARID = CN_YEARID  WHERE CN_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE DEBITNOTEMASTER_BILLDESC SET DN_BILLINITIALS = LEFT(DN_BILLINITIALS, LEN(DN_BILLINITIALS)-1) FROM DEBITNOTEMASTER_BILLDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = DN_BILLINITIALS AND YEARID = DN_YEARID  WHERE DN_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE MANUALMATCHING_DESC SET MATCH_BILLINITIALS = LEFT(MATCH_BILLINITIALS, LEN(MATCH_BILLINITIALS)-1) FROM MANUALMATCHING_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = MATCH_BILLINITIALS AND YEARID = MATCH_YEARID  WHERE MATCH_YEARID = " & YearId, "", "")
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PAYMENT_RENUM_Click(sender As Object, e As EventArgs) Handles PAYMENT_RENUM.Click
        Try

            If InputBox("Enter Master Password", "TEXTRADE") <> "Infosys@123" Then
                MsgBox("Invalid Password")
                Exit Sub
            End If

            If MsgBox("You are about to Renumber Entries, Please Take Backup First, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If MsgBox("Wish To Renumber Payment Entries?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then PAYMENTRENUMBERING()
            MsgBox("Re-Numbering Done Successfully")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PAYMENTRENUMBERING()
        Try
            'RENUMBER JOUNRAL VOUCHER FIRST
            Dim OBJCMN As New ClsCommon
            Dim DTREGISTER As DataTable = OBJCMN.SEARCH("REGISTER_ID AS REGID, REGISTER_NAME AS REGNAME, REGISTER_ABBR AS REGABBR, REGISTER_INITIALS AS REGINITIALS", "", " REGISTERMASTER ", " AND REGISTER_TYPE = 'PAYMENT' AND REGISTER_YEARID = " & YearId)
            If DTREGISTER.Rows.Count > 0 Then
                For Each REGROW As DataRow In DTREGISTER.Rows

                    'SEARCH ENTRY IN THIS REGISTER AND ADD IN TEMP TABLE
                    'FROM THIS TEMPTABLE WE WILL UPDATE THE ENTRIES
                    'WE WILL UPDATE THE ENTRYIES FROM THEIR OLDINITIALS, AND REPLACE WITH NEW INITIALS
                    'BUT NEW INITIALS WILL CONTAIN A SPECIAL CHARACTER SO THAT WE CAN DISTINGUISH BETWEEN OLD AND NEW INITIALS\
                    'ONCE ALL ENTRIES ARE UPDATED WE WILL REMOVE THAT SPECIAL CHARACTER
                    Dim DTENTRY As DataTable = OBJCMN.SEARCH("DISTINCT  PAYMENT_NO AS OLDNO, PAYMENT_registerid AS REGID, PAYMENT_DATE AS DATE, PAYMENT_initials AS OLDINITIALS, PAYMENT_yearid AS YEARID ", "", " PAYMENTMASTER ", " AND PAYMENT_registerid = " & REGROW("REGID") & " AND PAYMENT_YEARID = " & YearId & " ORDER BY PAYMENT_registerid, PAYMENT_date, PAYMENT_no")
                    Dim NEWBILLNO As Integer = 1
                    Dim TEMPDT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPRENUMBERING WHERE YEARID = " & YearId, "", "")
                    For Each DTROW As DataRow In DTENTRY.Rows
                        TEMPDT = OBJCMN.Execute_Any_String("INSERT INTO TEMPRENUMBERING VALUES (" & DTROW("REGID") & ",'" & Format(Convert.ToDateTime(DTROW("DATE")).Date, "MM/dd/yyyy") & "'," & DTROW("YEARID") & "," & DTROW("OLDNO") & ",'" & DTROW("OLDINITIALS") & "'," & NEWBILLNO & ",'" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N')", "", "")

                        'UPDATE IN PAYMENT MASTER
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER SET PAYMENT_NO = " & NEWBILLNO & ", PAYMENT_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE PAYMENT_INITIALS = '" & DTROW("OLDINITIALS") & "' AND PAYMENT_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_DESC SET PAYMENT_NO = " & NEWBILLNO & ", PAYMENT_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE PAYMENT_INITIALS = '" & DTROW("OLDINITIALS") & "' AND PAYMENT_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_GRIDDESC SET PAYMENT_NO = " & NEWBILLNO & ", PAYMENT_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE PAYMENT_INITIALS = '" & DTROW("OLDINITIALS") & "' AND PAYMENT_YEARID = " & YearId, "", "")


                        'UPDATE IN JOURNALMASTER
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_REFNO = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE JOURNAL_REFNO = '" & DTROW("OLDINITIALS") & "' AND JOURNAL_YEARID = " & YearId, "", "")

                        'UPDATE IN ACCMASTER 
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE ACCMASTER SET ACC_BILLNO = " & NEWBILLNO & ",ACC_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE ACC_INITIALS = '" & DTROW("OLDINITIALS") & "' AND ACC_YEARID = " & YearId, "", "")


                        'UPDATE IN RECEIPTMASTER 
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_DESC SET RECEIPT_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE RECEIPT_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND RECEIPT_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_GRIDDESC SET RECEIPT_PAYBILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE RECEIPT_PAYBILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND RECEIPT_YEARID = " & YearId, "", "")


                        'UPDATE IN CREDITNOTEMASTER_BILLDESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE CREDITNOTEMASTER_BILLDESC SET CN_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE CN_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND CN_YEARID = " & YearId, "", "")

                        'UPDATE IN DEBITNOTEMASTER_BILLDESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE DEBITNOTEMASTER_BILLDESC SET DN_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE DN_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND DN_YEARID = " & YearId, "", "")

                        'UPDATE IN MANUALMATCHING_DESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE MANUALMATCHING_DESC SET MATCH_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE MATCH_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND MATCH_YEARID = " & YearId, "", "")


                        NEWBILLNO += 1
                    Next

                    'REMOVE SPECIAL CHARACTER FROM INTIIALS
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER SET PAYMENT_INITIALS = LEFT(PAYMENT_INITIALS, LEN(PAYMENT_INITIALS)-1) FROM PAYMENTMASTER INNER JOIN TEMPRENUMBERING ON NEWINITIALS = PAYMENT_INITIALS AND YEARID = PAYMENT_YEARID  WHERE PAYMENT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_DESC SET PAYMENT_INITIALS = LEFT(PAYMENT_INITIALS, LEN(PAYMENT_INITIALS)-1) FROM PAYMENTMASTER_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = PAYMENT_INITIALS AND YEARID = PAYMENT_YEARID  WHERE PAYMENT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_GRIDDESC SET PAYMENT_INITIALS = LEFT(PAYMENT_INITIALS, LEN(PAYMENT_INITIALS)-1) FROM PAYMENTMASTER_GRIDDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = PAYMENT_INITIALS AND YEARID = PAYMENT_YEARID WHERE PAYMENT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_REFNO = LEFT(JOURNAL_REFNO, LEN(JOURNAL_REFNO)-1) FROM JOURNALMASTER INNER JOIN TEMPRENUMBERING ON NEWINITIALS = JOURNAL_REFNO AND YEARID = JOURNAL_YEARID WHERE JOURNAL_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE ACCMASTER SET ACC_INITIALS = LEFT(ACC_INITIALS, LEN(ACC_INITIALS)-1) WHERE ACC_YEARID = " & YearId & " AND ACC_TYPE = 'PAYMENT' AND ACC_REGTYPE = '" & REGROW("REGABBR") & "'", "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_DESC SET RECEIPT_BILLINITIALS = LEFT(RECEIPT_BILLINITIALS, LEN(RECEIPT_BILLINITIALS)-1) FROM RECEIPTMASTER_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = RECEIPT_BILLINITIALS AND YEARID = RECEIPT_YEARID  WHERE RECEIPT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_GRIDDESC SET RECEIPT_PAYBILLINITIALS = LEFT(RECEIPT_PAYBILLINITIALS, LEN(RECEIPT_PAYBILLINITIALS)-1) FROM RECEIPTMASTER_GRIDDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = RECEIPT_PAYBILLINITIALS AND YEARID = RECEIPT_YEARID WHERE RECEIPT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE CREDITNOTEMASTER_BILLDESC SET CN_BILLINITIALS = LEFT(CN_BILLINITIALS, LEN(CN_BILLINITIALS)-1) FROM CREDITNOTEMASTER_BILLDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = CN_BILLINITIALS AND YEARID = CN_YEARID  WHERE CN_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE DEBITNOTEMASTER_BILLDESC SET DN_BILLINITIALS = LEFT(DN_BILLINITIALS, LEN(DN_BILLINITIALS)-1) FROM DEBITNOTEMASTER_BILLDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = DN_BILLINITIALS AND YEARID = DN_YEARID  WHERE DN_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE MANUALMATCHING_DESC SET MATCH_BILLINITIALS = LEFT(MATCH_BILLINITIALS, LEN(MATCH_BILLINITIALS)-1) FROM MANUALMATCHING_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = MATCH_BILLINITIALS AND YEARID = MATCH_YEARID  WHERE MATCH_YEARID = " & YearId, "", "")
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewInwardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SAMPLEINWARDADD.Click
        Try
            Dim objbooklet As New SampleBookletInward
            objbooklet.MdiParent = Me
            objbooklet.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingInwardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SAMPLEINWARDEDIT.Click
        Try
            Dim objbooklet As New SampleBookletInwardDetails
            objbooklet.MdiParent = Me
            objbooklet.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECDREPORTS_MASTER_Click(sender As Object, e As EventArgs) Handles GREYRECDREPORTS_MASTER.Click
        Try
            Dim OBJGREY As New GreyRecdFilter
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALARYREPORT_MASTER_Click(sender As Object, e As EventArgs) Handles SALARYREPORT_MASTER.Click
        Try
            Dim OBJSAL As New SalaryFilter
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STOCKONHANDSUMMCOSTMENU_Click(sender As Object, e As EventArgs) Handles STOCKONHANDSUMMCOSTMENU.Click
        Try
            Dim OBJSTOCK As New StockOnHandSummaryCosting
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem2_Click_1(sender As Object, e As EventArgs) Handles TRANSFERSAMPLEADD.Click
        Try
            Dim OBJPROFORMA As New InterGodownTransferSample
            OBJPROFORMA.MdiParent = Me
            OBJPROFORMA.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingEntryToolStripMenuItem2_Click_1(sender As Object, e As EventArgs) Handles TRANSFERSAMPLEEDIT.Click
        Try
            Dim OBJPROFORMA As New InterGodownTransferSampleDetails
            OBJPROFORMA.MdiParent = Me
            OBJPROFORMA.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PartySampleBookletStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PARTYSAMPLESTOCK_REPORT.Click
        Try
            Dim OBJSTOCK As New SampleBookletStockPartyGridReport
            OBJSTOCK.MdiParent = Me
            OBJSTOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STORESPOADD_Click(sender As Object, e As EventArgs) Handles STORESPOADD.Click
        Try
            Dim OBJPROFORMA As New StoresPurchaseOrder
            OBJPROFORMA.MdiParent = Me
            OBJPROFORMA.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STORESPOEDIT_Click(sender As Object, e As EventArgs) Handles STORESPOEDIT.Click
        Try
            Dim OBJPROFORMA As New StoresPurchaseOrderDetails
            OBJPROFORMA.MdiParent = Me
            OBJPROFORMA.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STORESPOCLOSE_Click(sender As Object, e As EventArgs) Handles STORESPOCLOSE.Click
        Try
            Dim OBJSTORES As New StoresPurchaseOrderClose
            OBJSTORES.MdiParent = Me
            OBJSTORES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TRANSFERSTORESADD_Click(sender As Object, e As EventArgs) Handles TRANSFERSTORESADD.Click
        Try
            Dim OBJSTORES As New InterGodownTransferStores
            OBJSTORES.MdiParent = Me
            OBJSTORES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TRANSFERSTORESEDIT_Click(sender As Object, e As EventArgs) Handles TRANSFERSTORESEDIT.Click
        Try
            Dim OBJSTORES As New InterGodownTransferStoresDetails
            OBJSTORES.MdiParent = Me
            OBJSTORES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem2_Click_2(sender As Object, e As EventArgs) Handles SAMPLEORDERADD.Click
        Try
            Dim OBJSTORES As New SampleOrder
            OBJSTORES.MdiParent = Me
            OBJSTORES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingEntryToolStripMenuItem2_Click_2(sender As Object, e As EventArgs) Handles SAMPLEORDEREDIT.Click
        Try
            Dim OBJSTORES As New SampleOrderDetails
            OBJSTORES.MdiParent = Me
            OBJSTORES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SampleOrderCloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SAMPLEORDERCLOSE.Click
        Try
            Dim OBJSTORES As New SampleOrderClose
            OBJSTORES.MdiParent = Me
            OBJSTORES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TALLYEXPORTCN_Click(sender As Object, e As EventArgs) Handles TALLYEXPORTCN.Click
        Try
            Dim OBJTALLY As New TallyDataExportSales
            OBJTALLY.FRMSTRING = "Credit Note"
            OBJTALLY.MdiParent = Me
            OBJTALLY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem2_Click_3(sender As Object, e As EventArgs) Handles DYEINGPRICELISTADD.Click
        Try
            Dim OBJSTORES As New DyeingPriceList
            OBJSTORES.MdiParent = Me
            OBJSTORES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAMPLETYPEADD_Click(sender As Object, e As EventArgs) Handles SAMPLETYPEADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "SAMPLETYPE"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SAMPLETYPEEDIT_Click(sender As Object, e As EventArgs) Handles SAMPLETYPEEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.FRMSTRING = "SAMPLETYPE"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


    Private Sub EditExistingEntryToolStripMenuItem2_Click_3(sender As Object, e As EventArgs) Handles DYEINGPRICELISTEDIT.Click
        Try
            Dim OBJSTORES As New DyeingPriceListDetails
            OBJSTORES.MdiParent = Me
            OBJSTORES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SendMailToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SendMailToolStripMenuItem1.Click
        Try
            Dim OBJMAIL As New E_Mail
            OBJMAIL.MdiParent = Me
            OBJMAIL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgersForAutoMailersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LedgersForAutoMailersToolStripMenuItem.Click
        Try
            Dim OBJMAIL As New LedgersForAutoMailers
            OBJMAIL.MdiParent = Me
            OBJMAIL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TALLYEXPORTSALESINV_Click(sender As Object, e As EventArgs) Handles TALLYEXPORTSALESINV.Click
        Try
            Dim OBJTALLY As New TallyDataExportSalesInv
            OBJTALLY.FRMSTRING = "Sales"
            OBJTALLY.MdiParent = Me
            OBJTALLY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TALLYEXPORTSRINV_Click(sender As Object, e As EventArgs) Handles TALLYEXPORTSRINV.Click
        Try
            Dim OBJTALLY As New TallyDataExportSalesReturnInv
            OBJTALLY.FRMSTRING = "Credit Note (Inventory)"
            OBJTALLY.MdiParent = Me
            OBJTALLY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAMPLEORDER_REPORTS_Click(sender As Object, e As EventArgs) Handles SAMPLEORDER_REPORTS.Click
        Try
            Dim OBJSMP As New SampleOrderFilter
            OBJSMP.MdiParent = Me
            OBJSMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STOREPURORDER_REPORTS_Click(sender As Object, e As EventArgs) Handles STOREPURORDER_REPORTS.Click
        Try
            Dim OBJSTORE As New StoreOrderFilter
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURCHASEINV_RENUM_Click(sender As Object, e As EventArgs) Handles PURCHASEINV_RENUM.Click
        Try

            If InputBox("Enter Master Password", "TEXTRADE") <> "Infosys@123" Then
                MsgBox("Invalid Password")
                Exit Sub
            End If

            If MsgBox("You are about to Renumber Entries, Please Take Backup First, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If MsgBox("Wish To Renumber Purchase Entries?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then PURCHASERENUMBERING()
            MsgBox("Re-Numbering Done Successfully")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PURCHASERENUMBERING()
        Try
            'RENUMBER JOUNRAL VOUCHER FIRST
            Dim OBJCMN As New ClsCommon
            Dim DTREGISTER As DataTable = OBJCMN.SEARCH("REGISTER_ID AS REGID, REGISTER_NAME AS REGNAME, REGISTER_ABBR AS REGABBR, REGISTER_INITIALS AS REGINITIALS", "", " REGISTERMASTER ", " AND REGISTER_TYPE = 'PURCHASE' AND REGISTER_YEARID = " & YearId)
            If DTREGISTER.Rows.Count > 0 Then
                For Each REGROW As DataRow In DTREGISTER.Rows

                    'SEARCH ENTRY IN THIS REGISTER AND ADD IN TEMP TABLE
                    'FROM THIS TEMPTABLE WE WILL UPDATE THE ENTRIES
                    'WE WILL UPDATE THE ENTRYIES FROM THEIR OLDINITIALS, AND REPLACE WITH NEW INITIALS
                    'BUT NEW INITIALS WILL CONTAIN A SPECIAL CHARACTER SO THAT WE CAN DISTINGUISH BETWEEN OLD AND NEW INITIALS\
                    'ONCE ALL ENTRIES ARE UPDATED WE WILL REMOVE THAT SPECIAL CHARACTER
                    Dim DTENTRY As DataTable = OBJCMN.SEARCH("DISTINCT  BILL_NO AS OLDNO, BILL_REGISTERID AS REGID, BILL_PARTYBILLDATE AS DATE, BILL_INITIALS AS OLDINITIALS, BILL_YEARID AS YEARID ", "", " PURCHASEMASTER ", " AND BILL_REGISTERID = " & REGROW("REGID") & " AND BILL_YEARID = " & YearId & " ORDER BY BILL_REGISTERID, BILL_PARTYBILLDATE, BILL_NO")
                    Dim NEWBILLNO As Integer = 1
                    Dim TEMPDT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPRENUMBERING WHERE YEARID = " & YearId, "", "")
                    For Each DTROW As DataRow In DTENTRY.Rows
                        TEMPDT = OBJCMN.Execute_Any_String("INSERT INTO TEMPRENUMBERING VALUES (" & DTROW("REGID") & ",'" & Format(Convert.ToDateTime(DTROW("DATE")).Date, "MM/dd/yyyy") & "'," & DTROW("YEARID") & "," & DTROW("OLDNO") & ",'" & DTROW("OLDINITIALS") & "'," & NEWBILLNO & ",'" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N')", "", "")

                        'UPDATE IN PURCHASEMASTER
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PURCHASEMASTER SET BILL_NO = " & NEWBILLNO & ",BILL_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE BILL_INITIALS = '" & DTROW("OLDINITIALS") & "' AND BILL_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PURCHASEMASTER_CHGS SET BILL_NO = " & NEWBILLNO & ",BILL_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE BILL_INITIALS = '" & DTROW("OLDINITIALS") & "' AND BILL_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PURCHASEMASTER_DESC SET BILL_NO = " & NEWBILLNO & ",BILL_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE BILL_INITIALS = '" & DTROW("OLDINITIALS") & "' AND BILL_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PURCHASEMASTER_PODETAILS SET BILL_NO = " & NEWBILLNO & " WHERE BILL_NO = " & Val(DTROW("OLDNO")) & " AND BILL_REGISTERID = " & Val(DTROW("REGID")) & " AND BILL_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PURCHASEMASTER_UPLOAD SET BILL_NO = " & NEWBILLNO & ",BILL_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE BILL_INITIALS = '" & DTROW("OLDINITIALS") & "' AND BILL_YEARID = " & YearId, "", "")

                        'UPDATE IN JOURNALMASTER
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_NO = " & NEWBILLNO & ",JOURNAL_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE JOURNAL_INITIALS = '" & DTROW("OLDINITIALS") & "' AND JOURNAL_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_REFNO = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE JOURNAL_REFNO = '" & DTROW("OLDINITIALS") & "' AND JOURNAL_YEARID = " & YearId, "", "")

                        'UPDATE IN ACCMASTER 
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE ACCMASTER SET ACC_BILLNO = " & NEWBILLNO & ",ACC_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE ACC_INITIALS = '" & DTROW("OLDINITIALS") & "' AND ACC_YEARID = " & YearId, "", "")

                        'UPDATE IN PAYMENT MASTER
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_DESC SET PAYMENT_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE PAYMENT_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND PAYMENT_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_GRIDDESC SET PAYMENT_PAYBILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE PAYMENT_PAYBILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND PAYMENT_YEARID = " & YearId, "", "")

                        'UPDATE IN RECEIPTMASTER 
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_DESC SET RECEIPT_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE RECEIPT_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND RECEIPT_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_GRIDDESC SET RECEIPT_PAYBILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE RECEIPT_PAYBILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND RECEIPT_YEARID = " & YearId, "", "")


                        'UPDATE IN CREDITNOTEMASTER_BILLDESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE CREDITNOTEMASTER_BILLDESC SET CN_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE CN_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND CN_YEARID = " & YearId, "", "")


                        'UPDATE IN DEBITNOTEMASTER_BILLDESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE DEBITNOTEMASTER_BILLDESC SET DN_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE DN_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND DN_YEARID = " & YearId, "", "")

                        'UPDATE IN PURCHASERETURN_BILLDESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PURCHASERETURN_BILLDESC SET PR_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE PR_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND PR_YEARID = " & YearId, "", "")

                        'UPDATE IN SALERETURN_BILLDESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE SALERETURN_BILLDESC SET SALRET_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE SALRET_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND SALRET_YEARID = " & YearId, "", "")


                        'UPDATE IN MANUALMATCHING_DESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE MANUALMATCHING_DESC SET MATCH_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE MATCH_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND MATCH_YEARID = " & YearId, "", "")


                        'UPDATE IN TDSCHALLAN
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE TDSCHALLAN SET TDSCHA_BILLNO = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE TDSCHA_BILLNO = '" & DTROW("OLDINITIALS") & "' AND TDSCHA_YEARID = " & YearId, "", "")


                        NEWBILLNO += 1
                    Next

                    'REMOVE SPECIAL CHARACTER FROM INTIIALS
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PURCHASEMASTER SET BILL_INITIALS = LEFT(BILL_INITIALS, LEN(BILL_INITIALS)-1) FROM PURCHASEMASTER INNER JOIN TEMPRENUMBERING ON NEWINITIALS = BILL_INITIALS AND YEARID = BILL_YEARID  WHERE BILL_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PURCHASEMASTER_CHGS SET BILL_INITIALS = LEFT(BILL_INITIALS, LEN(BILL_INITIALS)-1) FROM PURCHASEMASTER_CHGS INNER JOIN TEMPRENUMBERING ON NEWINITIALS = BILL_INITIALS AND YEARID = BILL_YEARID  WHERE BILL_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PURCHASEMASTER_DESC SET BILL_INITIALS = LEFT(BILL_INITIALS, LEN(BILL_INITIALS)-1) FROM PURCHASEMASTER_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = BILL_INITIALS AND YEARID = BILL_YEARID  WHERE BILL_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PURCHASEMASTER_UPLOAD SET BILL_INITIALS = LEFT(BILL_INITIALS, LEN(BILL_INITIALS)-1) FROM PURCHASEMASTER_UPLOAD INNER JOIN TEMPRENUMBERING ON NEWINITIALS = BILL_INITIALS AND YEARID = BILL_YEARID  WHERE BILL_YEARID = " & YearId, "", "")

                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_REFNO = LEFT(JOURNAL_REFNO, LEN(JOURNAL_REFNO)-1) FROM JOURNALMASTER INNER JOIN TEMPRENUMBERING ON NEWINITIALS = JOURNAL_REFNO AND YEARID = JOURNAL_YEARID WHERE JOURNAL_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE ACCMASTER SET ACC_INITIALS = LEFT(ACC_INITIALS, LEN(ACC_INITIALS)-1) WHERE ACC_YEARID = " & YearId & " AND ACC_TYPE = 'PURCHASE' AND ACC_REGTYPE = '" & REGROW("REGABBR") & "'", "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_DESC SET PAYMENT_BILLINITIALS = LEFT(PAYMENT_BILLINITIALS, LEN(PAYMENT_BILLINITIALS)-1) FROM PAYMENTMASTER_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = PAYMENT_BILLINITIALS AND YEARID = PAYMENT_YEARID  WHERE PAYMENT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_GRIDDESC SET PAYMENT_PAYBILLINITIALS = LEFT(PAYMENT_PAYBILLINITIALS, LEN(PAYMENT_PAYBILLINITIALS)-1) FROM PAYMENTMASTER_GRIDDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = PAYMENT_PAYBILLINITIALS AND YEARID = PAYMENT_YEARID WHERE PAYMENT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_DESC SET RECEIPT_BILLINITIALS = LEFT(RECEIPT_BILLINITIALS, LEN(RECEIPT_BILLINITIALS)-1) FROM RECEIPTMASTER_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = RECEIPT_BILLINITIALS AND YEARID = RECEIPT_YEARID  WHERE RECEIPT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_GRIDDESC SET RECEIPT_PAYBILLINITIALS = LEFT(RECEIPT_PAYBILLINITIALS, LEN(RECEIPT_PAYBILLINITIALS)-1) FROM RECEIPTMASTER_GRIDDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = RECEIPT_PAYBILLINITIALS AND YEARID = RECEIPT_YEARID WHERE RECEIPT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE CREDITNOTEMASTER_BILLDESC SET CN_BILLINITIALS = LEFT(CN_BILLINITIALS, LEN(CN_BILLINITIALS)-1) FROM CREDITNOTEMASTER_BILLDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = CN_BILLINITIALS AND YEARID = CN_YEARID  WHERE CN_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE DEBITNOTEMASTER_BILLDESC SET DN_BILLINITIALS = LEFT(DN_BILLINITIALS, LEN(DN_BILLINITIALS)-1) FROM DEBITNOTEMASTER_BILLDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = DN_BILLINITIALS AND YEARID = DN_YEARID  WHERE DN_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PURCHASERETURN_BILLDESC SET PR_BILLINITIALS = LEFT(PR_BILLINITIALS, LEN(PR_BILLINITIALS)-1) FROM PURCHASERETURN_BILLDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = PR_BILLINITIALS AND YEARID = PR_YEARID  WHERE PR_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE SALERETURN_BILLDESC SET SALRET_BILLINITIALS = LEFT(SALRET_BILLINITIALS, LEN(SALRET_BILLINITIALS)-1) FROM SALERETURN_BILLDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = SALRET_BILLINITIALS AND YEARID = SALRET_YEARID  WHERE SALRET_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE MANUALMATCHING_DESC SET MATCH_BILLINITIALS = LEFT(MATCH_BILLINITIALS, LEN(MATCH_BILLINITIALS)-1) FROM MANUALMATCHING_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = MATCH_BILLINITIALS AND YEARID = MATCH_YEARID  WHERE MATCH_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE TDSCHALLAN SET TDSCHA_BILLNO = LEFT(TDSCHA_BILLNO, LEN(TDSCHA_BILLNO)-1) FROM TDSCHALLAN INNER JOIN TEMPRENUMBERING ON NEWINITIALS = TDSCHA_BILLNO AND YEARID = TDSCHA_YEARID WHERE TDSCHA_YEARID = " & YearId, "", "")
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateItemRateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateItemRateToolStripMenuItem.Click
        Try
            Dim OBJSMP As New UpdateItemRates
            OBJSMP.MdiParent = Me
            OBJSMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BLOCKDATASTOCKTRANSFER_Click(sender As Object, e As EventArgs) Handles BLOCKDATASTOCKTRANSFER.Click
        Try
            Dim OBJBLOCK As New BlockDataTransfer
            OBJBLOCK.MdiParent = Me
            OBJBLOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewIncentiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles INCENTIVEADD.Click
        Try
            Dim OBJSTORES As New IncentiveTagging
            OBJSTORES.MdiParent = Me
            OBJSTORES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingIncentiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles INCENTIVEEDIT.Click
        Try
            Dim OBJSTORES As New IncentiveTaggingDetails
            OBJSTORES.MdiParent = Me
            OBJSTORES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewDyedTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DYEDTYPEADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "DYEDTYPE"
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub DYEDTYPEEDIT_Click(sender As Object, e As EventArgs) Handles DYEDTYPEEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.FRMSTRING = "DYEDTYPE"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub RackCapacityReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RackCapacityReportToolStripMenuItem.Click
        Try
            Dim OBJRACK As New RackCapacityGridReport
            OBJRACK.MdiParent = Me
            OBJRACK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateQualityWtToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateQualityWtToolStripMenuItem.Click
        Try
            Dim OBJWT As New UpdateQualityWt
            OBJWT.MdiParent = Me
            OBJWT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DELETEMULTIPLEIMAGE_Click(sender As Object, e As EventArgs) Handles DELETEMULTIPLEIMAGE.Click
        Try
            Dim OBJIMG As New ItemDesignImageDelete
            OBJIMG.MdiParent = Me
            OBJIMG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub POSTERORDERADD_Click(sender As Object, e As EventArgs) Handles POSTERORDERADD.Click
        Try
            Dim OBJPOSTER As New PosterOrder
            OBJPOSTER.MdiParent = Me
            OBJPOSTER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub POSTERORDEREDIT_Click(sender As Object, e As EventArgs) Handles POSTERORDEREDIT.Click
        Try
            Dim OBJPOSTER As New PosterOrderDetails
            OBJPOSTER.MdiParent = Me
            OBJPOSTER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EmbProductionFilterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EMBPROD_FILTER.Click
        Try
            Dim OBJEMB As New EmbProductionFilter
            OBJEMB.MdiParent = Me
            OBJEMB.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub COSTCENTERADD_Click(sender As Object, e As EventArgs) Handles COSTCENTERADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "COSTCENTER"
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub COSTCENTEREDIT_Click(sender As Object, e As EventArgs) Handles COSTCENTEREDIT.Click
        Try
            Dim objCategoryDetails As New CategoryDetails
            objCategoryDetails.MdiParent = Me
            objCategoryDetails.FRMSTRING = "COSTCENTER"
            objCategoryDetails.Show()
            objCategoryDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ReminderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReminderToolStripMenuItem.Click
        Try
            Dim OBJBLOCK As New Reminder
            OBJBLOCK.MdiParent = Me
            OBJBLOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub REMINDERREPORT_MASTER_Click(sender As Object, e As EventArgs) Handles REMINDERREPORT_MASTER.Click
        Try
            Dim OBJREMINDER As New ReminderReport
            OBJREMINDER.MdiParent = Me
            OBJREMINDER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STOCKAGAINSTODERREPORT_Click(sender As Object, e As EventArgs) Handles STOCKAGAINSTODERREPORT.Click
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            Dim OBJREP As New SaleOrderVsStockGridReport
            OBJREP.MdiParent = Me
            OBJREP.FRMSTRING = "STOCKVSORDER"

            If ClientName = "AVIS" Then

                'IF CHK IS TRUE THEN GET ALL YEARID UDER THAT COMPANY
                Dim WHERECLAUSE As String = ""
                'If CHKALLCMP.Checked = True Then WHERECLAUSE = WHERECLAUSE & " And BARCODESTOCK.YEARID IN (SELECT YEAR_ID FROM YEARMASTER WHERE YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "')" Else WHERECLAUSE = WHERECLAUSE & " AND BARCODESTOCK.YEARID=" & YearId
                'GET ALL YEARID FROM SELECTED COMPANY WITH SAME STARTYEAR
                WHERECLAUSE = WHERECLAUSE & " AND BARCODESTOCK.YEARID = " & YearId

                If SALEORDERONMTRS = False Then OBJREP.SOCLAUSE = OBJREP.SOCLAUSE & " AND (PENDINGPCS > 0  OR OLDPENDINGPCS > 0) "
                OBJREP.WHERECLAUSE = WHERECLAUSE

            Else
                OBJREP.SOCLAUSE = " AND ALLSALEORDER_DESC.SO_CLOSED = 'FALSE' "
            End If

            OBJREP.Show()
            Exit Sub
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Try
            Dim objCategory As New DefaultBank
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAMPLESTOCKTAKINGADD_Click_4(sender As Object, e As EventArgs) Handles SAMPLESTOCKTAKINGADD.Click
        Try
            Dim objCategory As New SampleStockTaking
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAMPLESTOCKTAKINGEDIT_Click_4(sender As Object, e As EventArgs) Handles SAMPLESTOCKTAKINGEDIT.Click
        Try
            Dim objCategory As New SampleStockTakingDetails
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewLotTaggingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LOTPROGRAMTAGADD.Click
        Try
            Dim OBJLOTTAG As New LotTagging
            OBJLOTTAG.MdiParent = Me
            OBJLOTTAG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LOTPROGRAMTAGEDIT.Click
        Try
            Dim OBJLOTTAG As New LotTaggingDetails
            OBJLOTTAG.MdiParent = Me
            OBJLOTTAG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LOCKPENDINGLOTFORPROGRAM_Click(sender As Object, e As EventArgs) Handles LOCKPENDINGLOTFORPROGRAM.Click
        Try
            Dim OBJLOCK As New LockPendingLotsForProgram
            OBJLOCK.MdiParent = Me
            OBJLOCK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub INCENTIVEREPORT_MENU_Click(sender As Object, e As EventArgs) Handles INCENTIVEREPORT_MENU.Click
        Try
            Dim OBJINCENTIVE As New IncentiveReport
            OBJINCENTIVE.MdiParent = Me
            OBJINCENTIVE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DESIGNERADD_Click(sender As Object, e As EventArgs) Handles DESIGNERADD.Click
        Try
            Dim OBJSAL As New DesignerMaster
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DESIGNEREDIT_Click_4(sender As Object, e As EventArgs) Handles DESIGNEREDIT.Click
        Try
            Dim OBJSAL As New DesignerMasterDetails
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BLANKETADD_Click(sender As Object, e As EventArgs) Handles BLANKETADD.Click
        Try
            Dim OBJSAL As New BlanketMaster
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BLANKETEDIT_Click(sender As Object, e As EventArgs) Handles BLANKETEDIT.Click
        Try
            Dim OBJSAL As New BlanketMasterDetails
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewBlanketToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AddNewBlanketToolStripMenuItem1.Click
        Try
            Dim OBJSAL As New BlanketRepresentation
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingBlanketToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditExistingBlanketToolStripMenuItem.Click
        Try
            Dim OBJSAL As New BlanketRepresentationDetails
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AUTODESIGNCREATION_MASTER_Click(sender As Object, e As EventArgs) Handles AUTODESIGNCREATION_MASTER.Click
        Try
            Dim OBJSAL As New DesignCreation
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STORESREORDERLEVEL_MASTER_Click(sender As Object, e As EventArgs) Handles STORESREORDERLEVEL_MASTER.Click
        Try
            Dim OBJREORDER As New StoresReorderLevel
            OBJREORDER.MdiParent = Me
            OBJREORDER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StoresReOrderLevelReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StoresReOrderLevelReportToolStripMenuItem.Click
        Try
            Dim OBJSTORES As New StoresReOrderLevelReport
            OBJSTORES.MdiParent = Me
            OBJSTORES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FollowupReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FollowupReportToolStripMenuItem.Click
        Try
            Dim OBJSENQUIRY As New EnquiryReminder
            OBJSENQUIRY.MdiParent = Me
            OBJSENQUIRY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        Try
            Dim OBJSENQUIRY As New SalesEnquiry
            OBJSENQUIRY.MdiParent = Me
            OBJSENQUIRY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        Try
            Dim OBJSAL As New SalesEnquiryDetails
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CloseMultipleSaleEnquiryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseMultipleSaleEnquiryToolStripMenuItem.Click
        Try
            Dim OBJSO As New SalesEnquiryClose
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub NONPURCHASEINV_RENUM_Click(sender As Object, e As EventArgs) Handles NONPURCHASEINV_RENUM.Click
        Try

            If InputBox("Enter Master Password", "TEXTRADE") <> "Infosys@123" Then
                MsgBox("Invalid Password")
                Exit Sub
            End If

            If MsgBox("You are about to Renumber Entries, Please Take Backup First, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If MsgBox("Wish To Renumber Voucher Entries?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then NONPURCHASERENUMBERING()
            MsgBox("Re-Numbering Done Successfully")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub NONPURCHASERENUMBERING()
        Try
            'RENUMBER JOUNRAL VOUCHER FIRST
            Dim OBJCMN As New ClsCommon
            Dim DTREGISTER As DataTable = OBJCMN.SEARCH("REGISTER_ID AS REGID, REGISTER_NAME AS REGNAME, REGISTER_ABBR AS REGABBR, REGISTER_INITIALS AS REGINITIALS", "", " REGISTERMASTER ", " AND REGISTER_TYPE = 'EXPENSE' AND REGISTER_YEARID = " & YearId)
            If DTREGISTER.Rows.Count > 0 Then
                For Each REGROW As DataRow In DTREGISTER.Rows

                    'SEARCH ENTRY IN THIS REGISTER AND ADD IN TEMP TABLE
                    'FROM THIS TEMPTABLE WE WILL UPDATE THE ENTRIES
                    'WE WILL UPDATE THE ENTRYIES FROM THEIR OLDINITIALS, AND REPLACE WITH NEW INITIALS
                    'BUT NEW INITIALS WILL CONTAIN A SPECIAL CHARACTER SO THAT WE CAN DISTINGUISH BETWEEN OLD AND NEW INITIALS\
                    'ONCE ALL ENTRIES ARE UPDATED WE WILL REMOVE THAT SPECIAL CHARACTER
                    Dim DTENTRY As DataTable = OBJCMN.SEARCH("DISTINCT NP_NO AS OLDNO, NP_REGISTERID AS REGID, NP_PARTYBILLDATE AS DATE, NP_INITIALS AS OLDINITIALS, NP_YEARID AS YEARID ", "", " NONPURCHASE ", " AND NP_REGISTERID = " & REGROW("REGID") & " AND NP_YEARID = " & YearId & " ORDER BY NP_REGISTERID, NP_PARTYBILLDATE, NP_NO")
                    Dim NEWBILLNO As Integer = 1
                    Dim TEMPDT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPRENUMBERING WHERE YEARID = " & YearId, "", "")
                    For Each DTROW As DataRow In DTENTRY.Rows
                        TEMPDT = OBJCMN.Execute_Any_String("INSERT INTO TEMPRENUMBERING VALUES (" & DTROW("REGID") & ",'" & Format(Convert.ToDateTime(DTROW("DATE")).Date, "MM/dd/yyyy") & "'," & DTROW("YEARID") & "," & DTROW("OLDNO") & ",'" & DTROW("OLDINITIALS") & "'," & NEWBILLNO & ",'" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N')", "", "")

                        'UPDATE IN NONPURCHASE
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE NONPURCHASE SET NP_NO = " & NEWBILLNO & ",NP_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE NP_INITIALS = '" & DTROW("OLDINITIALS") & "' AND NP_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE NONPURCHASE_DESC SET NP_NO = " & NEWBILLNO & ",NP_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE NP_INITIALS = '" & DTROW("OLDINITIALS") & "' AND NP_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE NONPURCHASE_SAMPLE SET NONPURCHASE_NO = " & NEWBILLNO & ",NONPURCHASE_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE NONPURCHASE_INITIALS = '" & DTROW("OLDINITIALS") & "' AND NONPURCHASE_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE NONPURCHASE_STORE SET NP_NO = " & NEWBILLNO & " WHERE NP_NO = " & Val(DTROW("OLDNO")) & " AND NP_REGISTERID = " & Val(DTROW("REGID")) & " AND NP_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE NONPURCHASE_UPLOAD SET NONPURCHASE_NO = " & NEWBILLNO & ",NONPURCHASE_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE NONPURCHASE_INITIALS = '" & DTROW("OLDINITIALS") & "' AND NONPURCHASE_YEARID = " & YearId, "", "")

                        'UPDATE IN JOURNALMASTER
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_NO = " & NEWBILLNO & ",JOURNAL_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE JOURNAL_INITIALS = '" & DTROW("OLDINITIALS") & "' AND JOURNAL_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_REFNO = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE JOURNAL_REFNO = '" & DTROW("OLDINITIALS") & "' AND JOURNAL_YEARID = " & YearId, "", "")

                        'UPDATE IN ACCMASTER 
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE ACCMASTER SET ACC_BILLNO = " & NEWBILLNO & ",ACC_INITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE ACC_INITIALS = '" & DTROW("OLDINITIALS") & "' AND ACC_YEARID = " & YearId, "", "")

                        'UPDATE IN PAYMENT MASTER
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_DESC SET PAYMENT_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE PAYMENT_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND PAYMENT_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_GRIDDESC SET PAYMENT_PAYBILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE PAYMENT_PAYBILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND PAYMENT_YEARID = " & YearId, "", "")

                        'UPDATE IN RECEIPTMASTER 
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_DESC SET RECEIPT_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE RECEIPT_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND RECEIPT_YEARID = " & YearId, "", "")
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_GRIDDESC SET RECEIPT_PAYBILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE RECEIPT_PAYBILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND RECEIPT_YEARID = " & YearId, "", "")


                        'UPDATE IN CREDITNOTEMASTER_BILLDESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE CREDITNOTEMASTER_BILLDESC SET CN_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE CN_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND CN_YEARID = " & YearId, "", "")

                        'UPDATE IN DEBITNOTEMASTER_BILLDESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE DEBITNOTEMASTER_BILLDESC SET DN_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE DN_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND DN_YEARID = " & YearId, "", "")

                        'UPDATE IN MANUALMATCHING_DESC
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE MANUALMATCHING_DESC SET MATCH_BILLINITIALS = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE MATCH_BILLINITIALS = '" & DTROW("OLDINITIALS") & "' AND MATCH_YEARID = " & YearId, "", "")


                        'UPDATE IN TDSCHALLAN
                        TEMPDT = OBJCMN.Execute_Any_String("UPDATE TDSCHALLAN SET TDSCHA_BILLNO = '" & REGROW("REGINITIALS") & "-" & NEWBILLNO & "N' WHERE TDSCHA_BILLNO = '" & DTROW("OLDINITIALS") & "' AND TDSCHA_YEARID = " & YearId, "", "")


                        NEWBILLNO += 1
                    Next

                    'REMOVE SPECIAL CHARACTER FROM INTIIALS
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE NONPURCHASE SET NP_INITIALS = LEFT(NP_INITIALS, LEN(NP_INITIALS)-1) FROM NONPURCHASE INNER JOIN TEMPRENUMBERING ON NEWINITIALS = NP_INITIALS AND YEARID = NP_YEARID  WHERE NP_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE NONPURCHASE_DESC SET NP_INITIALS = LEFT(NP_INITIALS, LEN(NP_INITIALS)-1) FROM NONPURCHASE_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = NP_INITIALS AND YEARID = NP_YEARID  WHERE NP_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE NONPURCHASE_SAMPLE SET NONPURCHASE_INITIALS = LEFT(NONPURCHASE_INITIALS, LEN(NONPURCHASE_INITIALS)-1) FROM NONPURCHASE_SAMPLE INNER JOIN TEMPRENUMBERING ON NEWINITIALS = NONPURCHASE_INITIALS AND YEARID = NONPURCHASE_YEARID  WHERE NONPURCHASE_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE NONPURCHASE_STORE SET NP_INITIALS = LEFT(NP_INITIALS, LEN(NP_INITIALS)-1) FROM NONPURCHASE_STORE INNER JOIN TEMPRENUMBERING ON NEWINITIALS = NP_INITIALS AND YEARID = NP_YEARID  WHERE NP_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE NONPURCHASE_UPLOAD SET NONPURCHASE_INITIALS = LEFT(NONPURCHASE_INITIALS, LEN(NONPURCHASE_INITIALS)-1) FROM NONPURCHASE_UPLOAD INNER JOIN TEMPRENUMBERING ON NEWINITIALS = NONPURCHASE_INITIALS AND YEARID = NONPURCHASE_YEARID  WHERE NONPURCHASE_YEARID = " & YearId, "", "")

                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE JOURNALMASTER SET JOURNAL_REFNO = LEFT(JOURNAL_REFNO, LEN(JOURNAL_REFNO)-1) FROM JOURNALMASTER INNER JOIN TEMPRENUMBERING ON NEWINITIALS = JOURNAL_REFNO AND YEARID = JOURNAL_YEARID WHERE JOURNAL_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE ACCMASTER SET ACC_INITIALS = LEFT(ACC_INITIALS, LEN(ACC_INITIALS)-1) WHERE ACC_YEARID = " & YearId & " AND ACC_TYPE = 'EXPENSE' AND ACC_REGTYPE = '" & REGROW("REGABBR") & "'", "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_DESC SET PAYMENT_BILLINITIALS = LEFT(PAYMENT_BILLINITIALS, LEN(PAYMENT_BILLINITIALS)-1) FROM PAYMENTMASTER_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = PAYMENT_BILLINITIALS AND YEARID = PAYMENT_YEARID  WHERE PAYMENT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE PAYMENTMASTER_GRIDDESC SET PAYMENT_PAYBILLINITIALS = LEFT(PAYMENT_PAYBILLINITIALS, LEN(PAYMENT_PAYBILLINITIALS)-1) FROM PAYMENTMASTER_GRIDDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = PAYMENT_PAYBILLINITIALS AND YEARID = PAYMENT_YEARID WHERE PAYMENT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_DESC SET RECEIPT_BILLINITIALS = LEFT(RECEIPT_BILLINITIALS, LEN(RECEIPT_BILLINITIALS)-1) FROM RECEIPTMASTER_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = RECEIPT_BILLINITIALS AND YEARID = RECEIPT_YEARID  WHERE RECEIPT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE RECEIPTMASTER_GRIDDESC SET RECEIPT_PAYBILLINITIALS = LEFT(RECEIPT_PAYBILLINITIALS, LEN(RECEIPT_PAYBILLINITIALS)-1) FROM RECEIPTMASTER_GRIDDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = RECEIPT_PAYBILLINITIALS AND YEARID = RECEIPT_YEARID WHERE RECEIPT_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE CREDITNOTEMASTER_BILLDESC SET CN_BILLINITIALS = LEFT(CN_BILLINITIALS, LEN(CN_BILLINITIALS)-1) FROM CREDITNOTEMASTER_BILLDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = CN_BILLINITIALS AND YEARID = CN_YEARID  WHERE CN_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE DEBITNOTEMASTER_BILLDESC SET DN_BILLINITIALS = LEFT(DN_BILLINITIALS, LEN(DN_BILLINITIALS)-1) FROM DEBITNOTEMASTER_BILLDESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = DN_BILLINITIALS AND YEARID = DN_YEARID  WHERE DN_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE MANUALMATCHING_DESC SET MATCH_BILLINITIALS = LEFT(MATCH_BILLINITIALS, LEN(MATCH_BILLINITIALS)-1) FROM MANUALMATCHING_DESC INNER JOIN TEMPRENUMBERING ON NEWINITIALS = MATCH_BILLINITIALS AND YEARID = MATCH_YEARID  WHERE MATCH_YEARID = " & YearId, "", "")
                    TEMPDT = OBJCMN.Execute_Any_String("UPDATE TDSCHALLAN SET TDSCHA_BILLNO = LEFT(TDSCHA_BILLNO, LEN(TDSCHA_BILLNO)-1) FROM TDSCHALLAN INNER JOIN TEMPRENUMBERING ON NEWINITIALS = TDSCHA_BILLNO AND YEARID = TDSCHA_YEARID WHERE TDSCHA_YEARID = " & YearId, "", "")
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GodownChangeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GODOWNCHANGE_MASTER.Click
        Try
            Dim OBJSO As New GodownChange
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UploadGodnameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UploadGodnameToolStripMenuItem.Click
        Try
            Dim OBJSO As New UploadGodName
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CoverNoteToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            'Dim OBJSO As New CoverNote
            'OBJSO.MdiParent = Me
            'OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub AddNewEntryToolStripMenuItem2_Click_4(sender As Object, e As EventArgs)
    '    Try
    '        Dim OBJSO As New CoverNote
    '        OBJSO.MdiParent = Me
    '        OBJSO.Show()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub EditExistingEntryToolStripMenuItem2_Click_4(sender As Object, e As EventArgs)
    '    Try
    '        Dim OBJSO As New CoverNoteDetails
    '        OBJSO.MdiParent = Me
    '        OBJSO.Show()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub MASHOK_SOFILTER_MENU_Click(sender As Object, e As EventArgs) Handles MASHOK_SOFILTER_MENU.Click
        Try
            Dim OBJSO As New SOFilter
            OBJSO.FRMSTRING = "SO"
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MASHOK_RECOUT_MENU_Click(sender As Object, e As EventArgs) Handles MASHOK_RECOUT_MENU.Click
        Try
            Dim OBJREC As New RecOutstanding
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MASHOK_PAYOUT_MENU_Click(sender As Object, e As EventArgs) Handles MASHOK_PAYOUT_MENU.Click
        Try
            Dim OBJPAY As New PayOutstanding
            OBJPAY.MdiParent = Me
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MASHOK_INVDETAILS_MENU_Click(sender As Object, e As EventArgs) Handles MASHOK_INVDETAILS_MENU.Click
        Try
            Dim OBJINVDETAIL As New InvoiceDetails
            OBJINVDETAIL.MdiParent = Me
            OBJINVDETAIL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MASHOK_AGEING_MENU_Click(sender As Object, e As EventArgs) Handles MASHOK_AGEING_MENU.Click
        Try
            Dim OBJAGEING As New AgeingReport
            OBJAGEING.MdiParent = Me
            OBJAGEING.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub COVERNOTEADD_Click(sender As Object, e As EventArgs) Handles COVERNOTEADD.Click
        Try
            Dim OBJSO As New CoverNote
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub COVERNOTEEDIT_Click(sender As Object, e As EventArgs) Handles COVERNOTEEDIT.Click
        Try
            Dim OBJSO As New CoverNoteDetails
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TransportInsuranceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransportInsuranceToolStripMenuItem.Click
        Try
            Dim OBJSO As New TransportInsuranceDetails
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MASHOK_POFILTER_MENU_Click(sender As Object, e As EventArgs) Handles MASHOK_POFILTER_MENU.Click
        Try
            Dim OBJSO As New SOFilter
            OBJSO.FRMSTRING = "PO"
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem2_Click_4(sender As Object, e As EventArgs) Handles AddNewEntryToolStripMenuItem2.Click
        Try
            Dim OBJSAL As New VehicleMaster
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingEntryToolStripMenuItem2_Click_4(sender As Object, e As EventArgs) Handles EditExistingEntryToolStripMenuItem2.Click
        Try
            Dim OBJSAL As New VehicleMasterDetails
            OBJSAL.MdiParent = Me
            OBJSAL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AutoWhatsappToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutoWhatsappToolStripMenuItem.Click
        Try
            Dim OBJAW As New AutoWhatsapp
            OBJAW.MdiParent = Me
            OBJAW.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateBillToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateBillToolStripMenuItem.Click
        Try
            Dim OBJAW As New UpdateBillDispute
            OBJAW.MdiParent = Me
            OBJAW.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub AddNewSaleOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewSaleOrderToolStripMenuItem.Click
        Try
            Dim Objpinvoice As New AgencySaleOrder
            Objpinvoice.MdiParent = Me
            Objpinvoice.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingSaleOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditExistingSaleOrderToolStripMenuItem.Click
        Try
            Dim Objpinvoice As New AgencySaleOrderDetails
            Objpinvoice.MdiParent = Me
            Objpinvoice.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem3_Click_1(sender As Object, e As EventArgs) Handles AddNewEntryToolStripMenuItem3.Click
        Try
            Dim OBJOP As New AgencyOpeningBills
            OBJOP.FRMSTRING = "AGENCYOPENINGBILLS"
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingEntryToolStripMenuItem4_Click_1(sender As Object, e As EventArgs) Handles EditExistingEntryToolStripMenuItem4.Click
        Try
            Dim OBJOP As New AgencyOpeningBillsDetails
            'OBJOP.FRMSTRING = "AGENCYOPENINGBILLS"
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CloseMultipleSaleOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseMultipleSaleOrderToolStripMenuItem.Click
        'Try
        '    Dim Objpinvoice As New AgencySaleOrderClose
        '    Objpinvoice.MdiParent = Me
        '    Objpinvoice.Show()
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles AddNewEntryToolStripMenuItem4.Click
        Try
            Dim OBJCN As New AgencyCreditNote
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingEntryToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles EditExistingEntryToolStripMenuItem3.Click
        Try
            Dim OBJCN As New AgencyCreditNoteDetails
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles AddNewEntryToolStripMenuItem5.Click
        Try
            Dim OBJCN As New OpeningAgencySaleOrder
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingEntryToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles EditExistingEntryToolStripMenuItem5.Click
        Try
            Dim OBJCN As New OpeningAgencySaleOrderDetails
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewSaleOrderToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AddNewSaleOrderToolStripMenuItem1.Click
        Try
            Dim OBJCN As New AgencyInvoice
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewSaleReturnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewSaleReturnToolStripMenuItem.Click
        Try
            Dim OBJCN As New AgencySaleReturn
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingInvoiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditExistingInvoiceToolStripMenuItem.Click
        Try
            Dim OBJCN As New AgencyInvoiceDetails
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingSaleReturnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditExistingSaleReturnToolStripMenuItem.Click
        Try
            Dim Objpinvoice As New AgencySaleReturnDetails
            Objpinvoice.MdiParent = Me
            Objpinvoice.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles AddNewEntryToolStripMenuItem6.Click
        Try
            Dim OBJOP As New AgencyReceipt
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingEntryToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles EditExistingEntryToolStripMenuItem6.Click
        Try
            Dim OBJOP As New AgencyReceiptDetails
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PartyWiseBaleRateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PartyWiseBaleRateToolStripMenuItem.Click
        Try
            Dim OBJCN As New PartyWiseBaleRate
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class

