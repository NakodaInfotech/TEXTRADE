

Imports BL
Imports System.IO
Imports System.Net
Imports System.ComponentModel
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports RestSharp
Imports Newtonsoft.Json
Imports TaxProEInvoice.API


Public Class InvoiceMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public Shared selectsotable As New DataTable
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK, GRIDCHGSDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPINVOICENO, TEMPREGNAME, TEMPACCCODE, TEMPREFNO As String
    Public tempMsg As Integer
    Dim TEMPUPLOADROW, TEMPCHGSROW, saleregid As Integer
    Dim saleregabbr, salereginitial, TEMPSALEREG As String
    Dim SALERATE, WRATE As Double
    Public DIRECTINVOICE As Boolean = False
    Public DIRECTPARTYNAME As String = ""
    Public TEMPPURNO As Integer, TEMPPURREGNAME As String, TEMPPARTYNAME As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        FILLCMB()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEXIT.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        cmbregister.Enabled = True
        cmbregister.Focus()
    End Sub

    Sub CLEAR()

        LBLRATE.Text = 0.00
        LBLCATEGORY.Text = ""
        GCUT.HeaderText = "Cut"
        GCUT.ReadOnly = True
        INVOICEDATE.Text = Now.Date
        sodate.Text = Now.Date
        GPDATE.Text = Now.Date
        TabControl2.SelectedIndex = 0

        Gmtrs.ReadOnly = False
        TXTMTRS.ReadOnly = False
        GCUT.ReadOnly = False
        TXTCUT.ReadOnly = False
        Gpcs.ReadOnly = False
        TXTPCS.ReadOnly = False

        TXTDOCKETNO.Clear()
        DTDOCKETDATE.Value = Now.Date
        TXTCOURIER.Clear()

        If ALLOWMANUALINVNO = True Then
            TXTINVOICENO.ReadOnly = False
            TXTINVOICENO.BackColor = Color.LemonChiffon
        Else
            TXTINVOICENO.ReadOnly = True
            TXTINVOICENO.BackColor = Color.Linen
        End If
        CMBSCREENTYPE.Text = INVOICESCREENTYPE
        HIDEVIEW()

        If ClientName = "AVIS" Then
            GPARTYPONO.Visible = True
            GRIDINVOICE.Width = 1330
        End If


        CMBSERVICETYPE.SelectedIndex = 0
        TXTSACCODE.Clear()

        GRIDMILLSUMM.RowCount = 0

        tstxtbillno.Clear()
        cmbname.Text = ""
        TXTSTATECODE.Clear()
        TXTGSTIN.Clear()
        TXTMULTISONO.Clear()
        txtpartypono.Clear()
        If ClientName = "SOFTAS" Then TXTBALENOFROM.Text = 1 Else TXTBALENOFROM.Clear()
        TXTBALENOTO.Clear()
        CMBHASTE.Text = ""
        CMBTERM.Text = ""
        CMBAGENT.Text = ""
        CMBLOCALTRANSPORT.Text = ""
        CMBHASTE.Enabled = True
        CMBAGENT.Enabled = True
        TXTGATEPASSNO.Clear()
        txtchallan.Clear()
        CHALLANDATE.Text = Now.Date
        txtrefno.Clear()
        CMBFORMNO.Text = ""
        TXTCRDAYS.Clear()
        duedate.Value = Now.Date
        If DIRECTINVOICE = False Then CMBSUPPLIERNAME.Text = ""
        txtDeliveryadd.Clear()
        cmbtrans.Text = ""
        txtlrno.Clear()
        LRDATE.Text = Now.Date
        TXTVEHICLENO.Clear()
        If CMPCITYNAME <> "" Then CMBFROMCITY.Text = CMPCITYNAME Else CMBFROMCITY.Text = ""
        CMBTOCITY.Text = ""
        CMBPACKING.Text = ""

        TXTADD.Clear()


        CHKBILLCHECKED.Checked = False
        CHKBILLDISPUTE.Checked = False
        CHKMANUAL.CheckState = CheckState.Unchecked
        CHKEXPORTGST.CheckState = CheckState.Unchecked
        chkchange.Checked = False
        CHKCD.CheckState = CheckState.Unchecked
        CHKPARTYTDS.CheckState = CheckState.Unchecked

        'LBLBARCODE.Visible = False
        'TXTBARCODE.Visible = False
        'CMDSELECTGDN.Enabled = False

        EP.Clear()
        PBCN.Visible = False
        PBRECD.Visible = False
        lbllocked.Visible = False
        PBlock.Visible = False
        cmdshowdetails.Visible = False
        LBLEINVGENERATED.Visible = False

        If ClientName = "RATAN" Then txtremarks.Text = "NETT CASH RATE PAYMENT 20 DAYS ONLY" Else txtremarks.Clear()
        txtinwords.Clear()

        TXTSRNO.Text = 1
        CMBITEM.Text = ""
        TXTHSNCODE.Clear()
        CMBQUALITY.Text = ""
        CMBDESIGN.Text = ""
        CMBSHADE.Text = ""
        TXTDESCRIPTION.Clear()
        TXTBALENO.Clear()
        TXTPCS.Clear()
        TXTCUT.Clear()
        TXTMTRS.Clear()
        TXTRATE.Clear()
        If ClientName = "CC" Or ClientName = "C3" Or ClientName = "GELATO" Or ClientName = "JITUBHAI" Or ClientName = "MAHAVIR" Or ClientName = "ARIHANTGARMENTS" Then
            CMBPER.Text = "Pcs"
        Else
            CMBPER.Text = "Mtrs"
        End If

        TXTAMT.Clear()
        TXTDISCPER.Clear()
        TXTDISCAMT.Clear()
        TXTSPDISCPER.Clear()
        TXTSPDISCAMT.Clear()
        TXTOTHERAMT.Clear()
        TXTTAXABLEAMT.Clear()
        TXTCGSTPER.Clear()
        TXTCGSTAMT.Clear()
        TXTSGSTPER.Clear()
        TXTSGSTAMT.Clear()
        TXTIGSTPER.Clear()
        TXTIGSTAMT.Clear()
        txtgrandtotal.Clear()

        txtdescbarcode.Clear()
        GRIDINVOICE.RowCount = 0

        TXTCHGSSRNO.Text = 1
        CMBCHARGES.Text = ""
        TXTCHGSPER.Clear()
        TXTCHGSAMT.Clear()
        GRIDCHGS.RowCount = 0

        TXTROE.Clear()
        CHKOVERSEAS.Checked = False
        CMBCIF.Text = ""
        TXTEXPTERMS.Clear()
        TXTMARKNOS.Clear()
        TXTEXPINSURANCE.Clear()
        TXTVESSEL.Clear()
        TXTLOADINGPORT.Clear()
        TXTDISCHARGEPORT.Clear()
        TXTEXPHSN.Text = "52083290"
        CMBCURRENCY.Text = ""
        TXTGROSSWT.Clear()
        TXTNETTWT.Clear()
        TXTSQMTRS.Clear()
        TXTTOTALUSDAMT.Clear()
        TXTGSTINVRATE.Clear()
        TXTCUSTOMINVRATE.Clear()
        TXTEXPDIFF.Clear()
        TXTINWORDSUSD.Clear()

        txtuploadsrno.Text = 1
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        txtimgpath.Clear()
        TXTFILENAME.Clear()
        TXTNEWIMGPATH.Clear()
        PBSoftCopy.ImageLocation = ""
        gridupload.RowCount = 0

        GRIDDOUBLECLICK = False
        GRIDCHGSDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        txtbillamt.Text = 0.0
        TXTSUBTOTAL.Text = 0.0
        TXTCHARGES.Text = 0.0
        txtgrandtotal.Text = 0.0
        txtroundoff.Text = 0.0
        txtremarks.Clear()

        TXTAMTREC.Clear()
        TXTRETURN.Clear()
        TXTEXTRAAMT.Clear()
        TXTBAL.Clear()

        lbltotalpcs.Text = 0.0
        lbltotalmtrs.Text = 0.0
        LBLTOTALAMT.Text = 0.0
        LBLTOTALDISCAMT.Text = 0.0
        LBLTOTALSPDISCAMT.Text = 0.0
        LBLTOTALOTHERAMT.Text = 0.0
        LBLTOTALTAXABLEAMT.Text = 0.0
        LBLTOTALCGSTAMT.Text = 0.0
        LBLTOTALSGSTAMT.Text = 0.0
        LBLTOTALIGSTAMT.Text = 0.0
        LBLTOTALBALES.Text = 0.0
        TXTMATERIALVALUE.Text = 0.0
        TXTTOTALWITHMATVALUE.Text = 0.0

        TXTTOTALPARTYINVAMT.Clear()
        LBLTOTALWT.Text = 0.0

        TXTCGSTPER1.Clear()
        TXTCGSTAMT1.Clear()
        TXTSGSTPER1.Clear()
        TXTSGSTAMT1.Clear()
        TXTIGSTPER1.Clear()
        TXTIGSTAMT1.Clear()

        CHKTCS.Checked = False
        TXTTOTALWITHGST.Clear()
        TXTTCSPER.Clear()
        TXTTCSAMT.Clear()

        CMBLEDGERCODE.Text = ""
        cmbname.Text = ""
        CMBLEDGERCODE.Enabled = True
        cmbname.Enabled = True
        getmax_INVOICE_no()

        TXTSOADVANCE.Clear()
        CMDSELECTGDN.Enabled = True
        LBLSMS.Visible = False
        LBLPRINT.Visible = False

        TXTMOBILENO.Clear()
        TXTEWAYBILLNO.Clear()
        TXTIRNNO.Clear()
        TXTACKNO.Clear()
        ACKDATE.Value = Now.Date
        PBQRCODE.Image = Nothing
        CMBDISPATCHFROM.Text = ""
        TXTSPECIALREMARKS.Clear()
        LBLWHATSAPP.Visible = False
        PBDESIGNIMAGE.Image = Nothing
        CMBSALESMAN.Text = ""

        'IT WAS DONE FOR EINVOICE LOCKING
        GRATE.ReadOnly = False
        TXTRATE.ReadOnly = False
        CMBCHARGES.Enabled = True
        TXTCHGSPER.ReadOnly = False
        TXTCHARGES.ReadOnly = False
        cmbname.Enabled = True
        INVOICEDATE.ReadOnly = False
        cmddelete.Enabled = True
        TOOLDELETE.Enabled = True
        GRIDCHGS.Enabled = True
        GRIDINVOICE.Enabled = True

        CMBCOSTCENTERNAME.Text = ""
        CMBREFERREDBY.Text = ""
        CHKTRADINGACC.CheckState = CheckState.Unchecked
        GRIDORDER.RowCount = 0
        LBLCOVERNOTEDONE.Visible = False
        CMDSELECTSO.Enabled = True
    End Sub

    Sub getmax_INVOICE_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(INVOICE_no),0) + 1 ", "INVOICEMASTER INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICE_REGISTERID AND REGISTER_CMPID = INVOICE_CMPID AND REGISTER_LOCATIONID = INVOICE_LOCATIONID AND REGISTER_YEARID = INVOICE_YEARID ", " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_cmpid=" & CmpId & " and INVOICE_locationid=" & Locationid & " and INVOICE_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTINVOICENO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub INVOICEMASTER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNoCancel)
                    If tempmsg = vbCancel Then Exit Sub
                    If tempmsg = vbYes Then cmdOK_Click(sender, e)
                End If
                Me.Close()

            ElseIf e.Alt = True And e.KeyCode = Keys.D1 Then
                TabControl2.SelectedIndex = 0
            ElseIf e.Alt = True And e.KeyCode = Keys.D2 Then
                TabControl2.SelectedIndex = 1
            ElseIf e.Alt = True And e.KeyCode = Keys.D3 Then
                TabControl2.SelectedIndex = 2
            ElseIf e.Alt = True And e.KeyCode = Keys.D4 Then
                TabControl2.SelectedIndex = 3
            ElseIf e.Alt = True And e.KeyCode = Keys.D5 Then
                TabControl2.SelectedIndex = 4
            ElseIf e.Alt = True And e.KeyCode = Keys.D6 Then
                TabControl2.SelectedIndex = 5
            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                Call PrintToolStripButton_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.E Then
                Call TOOLEINV_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.W Then
                Call TOOLEWB_Click(sender, e)
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolprevious_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.F5 Then
                GRIDINVOICE.Focus()
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            fillregister(cmbregister, " and register_type = 'SALE'")
            If CMBAGENT.Text.Trim = "" Then FILLNAME(CMBAGENT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
            If CMBSALESMAN.Text.Trim = "" Then FILLSALESMAN(CMBSALESMAN)

            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
            If CMBFROMCITY.Text.Trim = "" Then fillCITY(CMBFROMCITY, EDIT)
            If CMBTOCITY.Text.Trim = "" Then fillCITY(CMBTOCITY, EDIT)
            If CMBTERM.Text.Trim = "" Then fillTERM(CMBTERM, EDIT)
            If CMBITEM.Text.Trim = "" Then fillitemname(CMBITEM, " AND ITEM_FRMSTRING = 'MERCHANT'")
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
            'If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, CMBITEM.Text.Trim)
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE, CMBDESIGN.Text.Trim, CMBITEM.Text.Trim)
            If CMBCHARGES.Text.Trim = "" Then FILLNAME(CMBCHARGES, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY ='Indirect Income' OR GROUPMASTER.GROUP_SECONDARY ='Sales A/C' OR GROUPMASTER.GROUP_SECONDARY ='Indirect Expenses' or GROUPMASTER.GROUP_SECONDARY ='Direct Income' OR GROUPMASTER.GROUP_SECONDARY ='Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY ='Duties & Taxes')")
            If ClientName = "AVIS" Then
                If CMBPACKING.Text.Trim = "" Then FILLNAME(CMBPACKING, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND GROUP_NAME = 'HASTE DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
                If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND GROUP_NAME <> 'HASTE DEBTORS'")
                If CMBCHARGES.Text.Trim = "" Then FILLNAME(CMBCHARGES, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY ='Sales A/C')")
            Else
                If CMBPACKING.Text.Trim = "" Then FILLNAME(CMBPACKING, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
                If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
                If CMBCHARGES.Text.Trim = "" Then FILLNAME(CMBCHARGES, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY ='Indirect Income' OR GROUPMASTER.GROUP_SECONDARY ='Sales A/C' OR GROUPMASTER.GROUP_SECONDARY ='Indirect Expenses' or GROUPMASTER.GROUP_SECONDARY ='Direct Income' OR GROUPMASTER.GROUP_SECONDARY ='Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY ='Duties & Taxes')")
            End If
            If CMBDISPATCHFROM.Text.Trim = "" Then FILLNAME(CMBDISPATCHFROM, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
            'If CMBREFERREDBY.Text.Trim = "" Then FILLNAME(CMBREFERREDBY, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")

            If CMBLOCALTRANSPORT.Text.Trim = "" Then FILLNAME(CMBLOCALTRANSPORT, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
            FILLNAME(CMBHASTE, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            If CMBCURRENCY.Text.Trim <> "" Then fillCURRENCY(CMBCURRENCY)
            FILLNAME(CMBSUPPLIERNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            If CMBGRIDTRANS.Text.Trim = "" Then FILLNAME(CMBGRIDTRANS, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")



            'Dim objclscommon As New ClsCommonMaster
            'Dim dt As DataTable
            'dt = objclscommon.search("acc_CODE", "", " ACCOUNTSMaster ", " and acc_cmpid = " & CmpId & " and acc_locationid = " & Locationid & " and acc_yearid = " & YearId)
            'If dt.Rows.Count > 0 Then
            '    dt.DefaultView.Sort = "acc_CODE"
            '    CMBLEDGERCODE.DataSource = dt
            '    CMBLEDGERCODE.DisplayMember = "acc_CODE"
            '    CMBLEDGERCODE.Text = TEMPACCCODE
            'End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub GETDATAFROMPUR()
        Try
            Dim objclsINV As New ClsPurchaseMaster
            objclsINV.alParaval.Add(TEMPPURNO)
            objclsINV.alParaval.Add(TEMPPURREGNAME)
            objclsINV.alParaval.Add(CmpId)
            objclsINV.alParaval.Add(Locationid)
            objclsINV.alParaval.Add(YearId)

            Dim TEMPNOOFBALES As Integer = 0

            Dim DT As DataTable = objclsINV.selectpurchase()
            If DT.Rows.Count > 0 Then

                For Each dr As DataRow In DT.Rows

                    cmbname.Text = TEMPPARTYNAME
                    Dim E As System.EventArgs
                    cmbname_Validated(cmbname, E)
                    INVOICEDATE.Text = Now.Date
                    txtrefno.Text = Convert.ToString(dr("BILLINITIALS"))

                    CMBSUPPLIERNAME.Text = dr("NAME")
                    cmbtrans.Text = dr("TRANSNAME")
                    txtlrno.Text = dr("LRNO")
                    LRDATE.Text = Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy")
                    CMBFROMCITY.Text = dr("FROMCITY")
                    CMBTOCITY.Text = dr("TOCITY")

                    txtremarks.Text = Convert.ToString(dr("REMARKS"))
                    txtinwords.Text = Convert.ToString(dr("INWORDS"))

                    TEMPNOOFBALES = Val(dr("NOOFBALES"))
                    TXTBALENOFROM.Text = Val(dr("NOOFBALES"))

                    txtbillamt.Text = Val(dr("BILLAMT"))
                    TXTCHARGES.Text = Val(dr("CHARGES"))
                    txtroundoff.Text = Val(dr("ROUNDOFF"))
                    txtgrandtotal.Text = Val(dr("GRANDTOTAL"))


                    'Item Grid
                    Dim PER As String
                    If dr("PER") = "Qty" Then PER = "Pcs" Else PER = "Mtrs"
                    GRIDINVOICE.Rows.Add(dr("GRIDSRNO").ToString, dr("ITEMNAME").ToString, dr("HSNCODE").ToString, dr("QUALITY").ToString, "", dr("COLOR"), Val(dr("AQTY")), Val(dr("FOLDPER")), "", dr("BALENO").ToString, Val(dr("QTY")), Val(dr("CUT")), Val(dr("MTRS")), Val(dr("RATE")), PER, Val(dr("AMT")), dr("LRNO"), dr("TRANSNAME"), Format(Val(TXTDISCPER.Text.Trim), "0.00"), Format(Val(TXTDISCAMT.Text.Trim), "0.00"), Format(Val(TXTSPDISCPER.Text.Trim), "0.00"), Format(Val(TXTSPDISCAMT.Text.Trim), "0.00"), Format(Val(TXTOTHERAMT.Text.Trim), "0.00"), Format(Val(TXTTAXABLEAMT.Text.Trim), "0.00"), Val(TXTCGSTPER.Text.Trim), Format(Val(TXTCGSTAMT.Text.Trim), "0.00"), Val(TXTSGSTPER.Text.Trim), Format(Val(TXTSGSTAMT.Text.Trim), "0.00"), Val(TXTIGSTPER.Text.Trim), Format(Val(TXTIGSTAMT.Text.Trim), "0.00"), Format(Val(TXTGRIDTOTAL.Text.Trim), "0.00"), txtdescbarcode.Text.Trim, 0, 0, "", 0, "", dr("UNIT"), 0, 0, Val(dr("CHADTI")), dr("NAME"), dr("PARTYBILLNO"))
                    CMBITEM.Text = dr("ITEMNAME")

                    TabControl2.SelectedIndex = (0)

                Next


                'NO NEED TO ADD THIS GRID AS PER NVAHAN
                ''CHARGES GRID
                'Dim OBJCMN As New ClsCommon
                'DT = OBJCMN.search(" PURCHASEMASTER_CHGS.BILL_gridsrno AS GRIDSRNO, ISNULL(LEDGERS.Acc_cmpname, '') AS CHARGES, ISNULL(PURCHASEMASTER_CHGS.BILL_PER, 0) AS PER, ISNULL(PURCHASEMASTER_CHGS.BILL_AMT, 0) AS AMT, ISNULL(TAXMASTER.TAX_ID, 0) AS TAXID ", "", " PURCHASEMASTER INNER JOIN REGISTERMASTER ON PURCHASEMASTER.BILL_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN PURCHASEMASTER_CHGS LEFT OUTER JOIN TAXMASTER ON PURCHASEMASTER_CHGS.BILL_TAXID = TAXMASTER.tax_id ON PURCHASEMASTER.BILL_NO = PURCHASEMASTER_CHGS.BILL_no AND PURCHASEMASTER.BILL_REGISTERID = PURCHASEMASTER_CHGS.BILL_REGISTERID LEFT OUTER JOIN LEDGERS ON PURCHASEMASTER_CHGS.BILL_CHARGESID = LEDGERS.Acc_id", " AND REGISTERMASTER.REGISTER_NAME = '" & TEMPPURREGNAME & "' AND REGISTER_TYPE = 'PURCHASE' AND PURCHASEMASTER_CHGS.BILL_NO = " & TEMPPURNO & " AND PURCHASEMASTER_CHGS.BILL_YEARID = " & YearId)
                'If DT.Rows.Count > 0 Then
                '    For Each DTR As DataRow In DT.Rows
                '        GRIDCHGS.Rows.Add(DTR("GRIDSRNO"), DTR("CHARGES"), DTR("PER"), DTR("AMT"), DTR("TAXID"))
                '    Next
                'End If

            End If
            GETHSNCODE()
            'IN CHARGES GRID ADD PKG ON SALE AS 21 RS PER BALE
            If ClientName = "NVAHAN" Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "PKG ON SALE", 0, 21 * Val(TEMPNOOFBALES), 0)

            TOTAL()


            cmbname.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub INVOICEMASTER_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE INVOICE'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            CLEAR()
            cmbname.Enabled = True


            If DIRECTINVOICE = True Then
                cmbname.Text = TEMPPARTYNAME
                cmbname_Validated(sender, e)
                If ClientName = "AFW" Then Call CMDSELECTGDN_Click(sender, e) Else GETDATAFROMPUR()
            End If


            If EDIT = True Then SHOWDATA()


            If TXTIRNNO.Text <> "" And TXTACKNO.Text <> "" Then
                LBLEINVGENERATED.Visible = True
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
            DIRECTINVOICE = False

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim OBJINVOICE As New ClsInvoiceMaster()
            Dim DT As DataTable = OBJINVOICE.selectINVOICE(TEMPINVOICENO, TEMPREGNAME, CmpId, Locationid, YearId)

            If DT.Rows.Count > 0 Then
                For Each dr As DataRow In DT.Rows

                    CMBSCREENTYPE.Text = dr("SCREENTYPE")
                    HIDEVIEW()

                    If ClientName = "AVIS" Then
                        GPARTYPONO.Visible = True
                        GRIDINVOICE.Width = 1330
                    End If

                    TXTINVOICENO.Text = TEMPINVOICENO

                    TXTINVOICENO.ReadOnly = True

                    cmbregister.Text = Convert.ToString(dr("REGNAME"))
                    INVOICEDATE.Text = Format(Convert.ToDateTime(dr("INVOICEDATE")), "dd/MM/yyyy")
                    cmbname.Text = Convert.ToString(dr("NAME"))
                    CMBPACKING.Text = Convert.ToString(dr("PACKING"))
                    txtDeliveryadd.Text = dr("PACKINGADDRESS")
                    CMBSALESMAN.Text = dr("SALESMAN")
                    CMBREFERREDBY.Text = dr("REFERREDBY")

                    CMBSERVICETYPE.Text = dr("SALETYPE")
                    TXTSACCODE.Text = dr("SACCODE")
                    TXTMATERIALVALUE.Text = Val(dr("MATERIALVALUE"))
                    TXTTOTALWITHMATVALUE.Text = Val(dr("TOTALWITHMATVALUE"))
                    CMBCOSTCENTERNAME.Text = Convert.ToString(dr("COSTCENTERNAME"))

                    If ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                        DT = OBJCMN.SEARCH("ISNULL(LEDGERS.ACC_MOBILE,'') AS MOBILENO ", "", " LEDGERS ", " and LEDGERS.acc_cmpname = '" & CMBPACKING.Text.Trim & "'  and LEDGERS.acc_YEARid = " & YearId)
                        If DT.Rows.Count > 0 Then
                            TXTMOBILENO.Text = DT.Rows(0).Item("MOBILENO")
                        End If
                    Else
                        TXTMOBILENO.Text = Convert.ToString(dr("MOBILENO"))
                    End If
                    TXTSTATECODE.Text = dr("STATECODE")
                    TXTGSTIN.Text = dr("GSTIN")

                    'EXPORT SECTION
                    CHKOVERSEAS.Checked = Convert.ToBoolean(dr("OVERSEAS"))
                    If CHKOVERSEAS.Checked = True And ClientName = "AVIS" Then GCUT.HeaderText = "USD" Else GCUT.HeaderText = "Cut"
                    TXTROE.Text = Val(dr("ROE"))
                    CMBCIF.Text = dr("CIF")
                    TXTEXPTERMS.Text = dr("EXPTERMS")
                    TXTMARKNOS.Text = dr("MARKNOS")
                    TXTEXPINSURANCE.Text = dr("EXPINSURANCE")
                    TXTVESSEL.Text = dr("VESSEL")
                    TXTLOADINGPORT.Text = dr("LOADINGPORT")
                    TXTDISCHARGEPORT.Text = dr("DISCHARGEPORT")
                    TXTEXPHSN.Text = dr("EXPHSN")
                    CMBCURRENCY.Text = dr("CURRENCY")
                    TXTGROSSWT.Text = Val(dr("GROSSWT"))
                    TXTNETTWT.Text = Val(dr("NETTWT"))
                    TXTSQMTRS.Text = Val(dr("SQMTRS"))
                    TXTTOTALUSDAMT.Text = Val(dr("TOTALUSDAMT"))
                    TXTGSTINVRATE.Text = Val(dr("GSTINVRATE"))
                    TXTCUSTOMINVRATE.Text = Val(dr("CUSTOMINVRATE"))
                    TXTEXPDIFF.Text = Val(dr("EXPDIFF"))
                    TXTINWORDSUSD.Text = dr("INWORDSUSD")





                    TXTMULTISONO.Text = Convert.ToString(dr("PONO"))
                    txtpartypono.Text = Convert.ToString(dr("PARTYPONO"))
                    sodate.Text = Format(Convert.ToDateTime(dr("PODATE")), "dd/MM/yyyy")
                    TXTBALENOFROM.Text = Val(dr("BALENOFROM"))
                    TXTBALENOTO.Text = Val(dr("BALENOTO"))

                    CMBLOCALTRANSPORT.Text = Convert.ToString(dr("LOCALTRANSNAME"))
                    CMBHASTE.Text = Convert.ToString(dr("HASTE"))
                    CMBAGENT.Text = Convert.ToString(dr("AGENT"))
                    txtchallan.Text = Convert.ToString(dr("CHALLANNO"))
                    CHALLANDATE.Text = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")

                    If ClientName <> "DAKSH" And ClientName <> "SHALIBHADRA" And ClientName <> "SANGHVI" And ClientName <> "TINUMINU" And ClientName <> "KCRAYON" And ClientName <> "SBA" And ClientName <> "AVIS" And ClientName <> "REALCORPORATION" Then
                        If txtchallan.Text <> "" Then
                            cmbname.Enabled = False
                            CMBLEDGERCODE.Enabled = False
                        End If
                    End If
                    If ClientName = "KDFAB" Then cmbname.Enabled = False

                    'GET PARTYTOTALINVOICE AMT
                    Dim DTINV As DataTable = OBJCMN.SEARCH("ISNULL(SUM(INVOICEMASTER.INVOICE_GRANDTOTAL),0) AS GRANDTOTAL", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                    If DTINV.Rows.Count > 0 Then TXTTOTALPARTYINVAMT.Text = Format(Val(DTINV.Rows(0).Item("GRANDTOTAL")), "0.00")


                    txtrefno.Text = Convert.ToString(dr("REFNO"))
                    TEMPREFNO = Convert.ToString(dr("REFNO"))
                    CMBFORMNO.Text = Convert.ToString(dr("FORM"))
                    TXTCRDAYS.Text = Convert.ToString(dr("CRDAYS"))
                    duedate.Value = Convert.ToDateTime(dr("DUEDATE"))
                    CMBSUPPLIERNAME.Text = dr("PURNAME")


                    cmbtrans.Text = dr("TRANSNAME")
                    TXTVEHICLENO.Text = dr("VEHICLENO")
                    txtlrno.Text = dr("LRNO")
                    'LRDATE.Text = Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy")
                    LRDATE.Text = Format(Convert.ToDateTime(dr("LRDATE")), "dd/MM/yyyy")
                    CMBFROMCITY.Text = Convert.ToString(dr("FROMCITY"))
                    CMBTOCITY.Text = Convert.ToString(dr("TOCITY"))
                    CMBTERM.Text = Convert.ToString(dr("TERMNAME"))

                    TXTDOCKETNO.Text = dr("DOCKETNO")
                    DTDOCKETDATE.Value = dr("DOCKETDATE")
                    TXTCOURIER.Text = dr("COURIER")

                    TXTEWAYBILLNO.Text = dr("EWAYBILLNO")
                    TXTGATEPASSNO.Text = dr("GATEPASSNO")
                    GPDATE.Text = Format(Convert.ToDateTime(dr("GPDATE")).Date, "dd/MM/yyyy")

                    If dr("BILLCHECKED") = 0 Then CHKBILLCHECKED.Checked = False Else CHKBILLCHECKED.Checked = True
                    If dr("BILLDISPUTE") = 0 Then CHKBILLDISPUTE.Checked = False Else CHKBILLDISPUTE.Checked = True
                    If Convert.ToBoolean(dr("MANUALGST")) = False Then CHKMANUAL.Checked = False Else CHKMANUAL.Checked = True
                    If Convert.ToBoolean(dr("EXPORTGST")) = False Then CHKEXPORTGST.Checked = False Else CHKEXPORTGST.Checked = True

                    TXTCGSTPER1.Text = Val(dr("TOTALCGSTPER"))
                    TXTSGSTPER1.Text = Val(dr("TOTALSGSTPER"))
                    TXTIGSTPER1.Text = Val(dr("TOTALIGSTPER"))

                    If CMBSCREENTYPE.Text = "TOTAL GST" And CHKMANUAL.Checked = True Then
                        TXTCGSTAMT1.Text = Format(Val(dr("TOTALCGSTAMT")), "0.00")
                        TXTSGSTAMT1.Text = Format(Val(dr("TOTALSGSTAMT")), "0.00")
                        TXTIGSTAMT1.Text = Format(Val(dr("TOTALIGSTAMT")), "0.00")
                    End If


                    If dr("APPLYTCS") = 0 Then CHKTCS.Checked = False Else CHKTCS.Checked = True
                    If dr("PARTYTDS") = 0 Then CHKPARTYTDS.Checked = False Else CHKPARTYTDS.Checked = True
                    TXTTOTALWITHGST.Text = Val(dr("TOTALWITHGST"))
                    TXTTCSPER.Text = Val(dr("TCSPER"))
                    TXTTCSAMT.Text = Val(dr("TCSAMT"))


                    txtbillamt.Text = Val(dr("AMOUNT"))
                    TXTCHARGES.Text = Val(dr("CHARGES"))
                    TXTSUBTOTAL.Text = Val(dr("SUBTOTAL"))
                    txtroundoff.Text = Val(dr("ROUNDOFF"))
                    txtgrandtotal.Text = Val(dr("GRANDTOTAL"))
                    LBLTOTALWT.Text = Val(dr("TOTALWT"))  'TOTALWT

                    TXTAMTREC.Text = Val(dr("AMTREC"))
                    TXTEXTRAAMT.Text = Val(dr("EXTRAAMT"))
                    TXTRETURN.Text = Val(dr("RETURN"))
                    TXTBAL.Text = Val(dr("BALANCE"))


                    TXTSONO.Text = dr("SONO")

                    If Val(dr("AMTREC")) > 0 Or Val(dr("EXTRAAMT")) > 0 Then
                        cmdshowdetails.Visible = True
                        PBRECD.Visible = True
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If

                    If Val(dr("RETURN")) > 0 Then
                        cmdshowdetails.Visible = True
                        PBCN.Visible = True
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If

                    txtremarks.Text = Convert.ToString(dr("REMARKS"))
                    If dr("CHKBARCODE") = 0 Then
                        CHKBARCODE.Checked = False
                    Else
                        CHKBARCODE.Checked = True
                    End If
                    TXTSPECIALREMARKS.Text = dr("SPECIALREMARKS")
                    If Convert.ToBoolean(dr("CD")) = False Then CHKCD.Checked = False Else CHKCD.Checked = True
                    If Convert.ToBoolean(dr("CHANGEADD")) = False Then CHKCHANGEADD.Checked = False Else CHKCHANGEADD.Checked = True
                    txtDeliveryadd.Text = dr("DELIVERYADD")
                    If Convert.ToBoolean(dr("TRADINGACC")) = False Then CHKTRADINGACC.Checked = False Else CHKTRADINGACC.Checked = True



                    'Item Grid
                    'GRIDINVOICE.Rows.Add(dr("SRNO"), Convert.ToString(dr("ITEMNAME")), Convert.ToString(dr("QUALITY")), Convert.ToString(dr("DESIGN")), Convert.ToString(dr("COLOR")), Format(Val(dr("PCS")), "0.00"), Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER"), Format(Val(dr("AMOUNT")), "0.00"), dr("BARCODE").ToString, dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"), dr("GRIDDONE"))
                    GRIDINVOICE.Rows.Add(dr("SRNO"), Convert.ToString(dr("ITEMNAME")), Convert.ToString(dr("HSNCODE")), Convert.ToString(dr("QUALITY")), Convert.ToString(dr("DESIGN")), Convert.ToString(dr("COLOR")), Format(Val(dr("QTY")), "0.00"), Val(dr("FOLDPER")), dr("PRINTDESC"), Convert.ToString(dr("BALENO")), Format(Val(dr("PCS")), "0.00"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER"), Format(Val(dr("AMT")), "0.00"), dr("GRIDLRNO"), dr("GRIDTRANS"), Format(Val(dr("DISCPER")), "0.00"), Format(Val(dr("DISCAMT")), "0.00"), Format(Val(dr("SPDISCPER")), "0.00"), Format(Val(dr("SPDISCAMT")), "0.00"), Format(Val(dr("OTHERAMT")), "0.00"), Format(Val(dr("TAXABLEAMT")), "0.00"), Format(Val(dr("CGSTPER")), "0.00"), Format(Val(dr("CGSTAMT")), "0.00"), Format(Val(dr("SGSTPER")), "0.00"), Format(Val(dr("SGSTAMT")), "0.00"), Format(Val(dr("IGSTPER")), "0.00"), Format(Val(dr("IGSTAMT")), "0.00"), Format(Val(dr("GRIDTOTAL")), "0.00"), dr("BARCODE").ToString, dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"), dr("GRIDDONE"), dr("GRIDPARTYPONO"), dr("UNIT"), Val(dr("GRIDSONO")), Val(dr("GRIDSOSRNO")), Val(dr("WT")), dr("GRIDPURNAME"), dr("PARTYBILLNO"))

                    If Convert.ToBoolean(dr("GRIDDONE")) = True Then
                        lbllocked.Visible = True
                        PBlock.Visible = True
                        GRIDINVOICE.Rows(GRIDINVOICE.RowCount - 1).DefaultCellStyle.BackColor = Drawing.Color.Yellow
                    End If

                    If Convert.ToBoolean(dr("SMSSEND")) = True Then LBLSMS.Visible = True
                    If Convert.ToBoolean(dr("PRINT")) = True Then LBLPRINT.Visible = True
                    If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True
                    If Convert.ToBoolean(dr("COVERNOTEDONE")) = True Then LBLCOVERNOTEDONE.Visible = True

                    TXTIRNNO.Text = dr("IRNNO")
                    TXTACKNO.Text = dr("ACKNO")
                    ACKDATE.Value = dr("ACKDATE")
                    If IsDBNull(dr("QRCODE")) = False Then
                        PBQRCODE.Image = Image.FromStream(New IO.MemoryStream(DirectCast(dr("QRCODE"), Byte())))
                    Else
                        PBQRCODE.Image = Nothing
                    End If
                    CMBDISPATCHFROM.Text = dr("DISPATCHFROM")


                    If ClientName = "YASHVI" And txtchallan.Text.Trim <> "" Then
                        Gmtrs.ReadOnly = True
                        TXTMTRS.ReadOnly = True
                        GCUT.ReadOnly = True
                        TXTCUT.ReadOnly = True
                        Gpcs.ReadOnly = True
                        TXTPCS.ReadOnly = True
                    End If

                Next
                GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1

                'CHARGES GRID

                Dim dttable As DataTable = OBJCMN.SEARCH(" INVOICEMASTER_CHGS.INVOICE_gridsrno AS GRIDSRNO, LEDGERS.Acc_cmpname AS CHARGES, INVOICEMASTER_CHGS.INVOICE_PER AS PER, INVOICEMASTER_CHGS.INVOICE_AMT AS AMT, INVOICEMASTER_CHGS.INVOICE_TAXID AS TAXID ", "", " INVOICEMASTER_CHGS INNER JOIN LEDGERS ON INVOICEMASTER_CHGS.INVOICE_CHARGESID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER_CHGS.INVOICE_REGISTERID = REGISTERMASTER.register_id ", " AND REGISTERMASTER.REGISTER_NAME = '" & TEMPREGNAME & "' AND INVOICEMASTER_CHGS.INVOICE_NO = " & TEMPINVOICENO & " AND INVOICEMASTER_CHGS.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER_CHGS.INVOICE_gridsrno")
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        GRIDCHGS.Rows.Add(DTR("GRIDSRNO"), DTR("CHARGES"), DTR("PER"), DTR("AMT"), DTR("TAXID"))
                    Next
                End If

                'UPLOAD GRID
                Dim OBJ As New ClsCommon
                Dim dt2 As DataTable = OBJCMN.SEARCH(" INVOICEMASTER_UPLOAD.INVOICE_GRIDSRNO AS GRIDSRNO, INVOICEMASTER_UPLOAD.INVOICE_REMARKS AS REMARKS, INVOICEMASTER_UPLOAD.INVOICE_NAME AS NAME, INVOICEMASTER_UPLOAD.INVOICE_IMGPATH AS IMGPATH, INVOICEMASTER_UPLOAD.INVOICE_NEWIMGPATH AS NEWIMGPATH", "", " INVOICEMASTER INNER JOIN INVOICEMASTER_UPLOAD ON INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_UPLOAD.INVOICE_YEARID AND INVOICEMASTER.INVOICE_LOCATIONID = INVOICEMASTER_UPLOAD.INVOICE_LOCATIONID AND INVOICEMASTER.INVOICE_CMPID = INVOICEMASTER_UPLOAD.INVOICE_CMPID AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_UPLOAD.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_NO = INVOICEMASTER_UPLOAD.INVOICE_NO LEFT OUTER JOIN REGISTERMASTER ON INVOICEMASTER_UPLOAD.INVOICE_REGISTERID = REGISTERMASTER.register_id AND INVOICEMASTER_UPLOAD.INVOICE_CMPID = REGISTERMASTER.register_cmpid AND INVOICEMASTER_UPLOAD.INVOICE_LOCATIONID = REGISTERMASTER.register_locationid AND INVOICEMASTER_UPLOAD.INVOICE_YEARID = REGISTERMASTER.register_yearid ", " AND INVOICEMASTER.INVOICE_NO = " & TEMPINVOICENO & " AND REGISTER_NAME ='" & TEMPREGNAME & "' AND REGISTERMASTER.REGISTER_TYPE='SALE' AND INVOICEMASTER.INVOICE_CMPID = " & CmpId & " AND INVOICEMASTER.INVOICE_LOCATIONID = " & Locationid & " AND INVOICEMASTER.INVOICE_YEARID = " & YearId)

                If dt2.Rows.Count > 0 Then
                    For Each DTR3 As DataRow In dt2.Rows
                        gridupload.Rows.Add(DTR3("GRIDSRNO"), DTR3("REMARKS"), DTR3("NAME"), DTR3("IMGPATH"), DTR3("NEWIMGPATH"))
                    Next
                End If


                'ORDER GRID
                'Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.SEARCH(" INVOICEMASTER_SODETAILS.INVOICE_GRIDSRNO AS GRIDSRNO, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, INVOICEMASTER_SODETAILS.INVOICE_ORDERPCS AS ORDERQTY, ISNULL(INVOICEMASTER_SODETAILS.INVOICE_ORDERMTRS,0) AS ORDERMTRS, INVOICEMASTER_SODETAILS.INVOICE_FROMNO AS FROMNO, INVOICEMASTER_SODETAILS.INVOICE_FROMSRNO AS FROMSRNO, INVOICEMASTER_SODETAILS.INVOICE_FROMTYPE AS FROMTYPE, INVOICEMASTER_SODETAILS.INVOICE_PCS AS GDNQTY, ISNULL(INVOICEMASTER_SODETAILS.INVOICE_MTRS,0) AS GDNMTRS, ISNULL(INVOICEMASTER_SODETAILS.INVOICE_RATE,0) AS RATE, ISNULL(INVOICEMASTER_SODETAILS.INVOICE_PARTYPONO,'') AS PARTYPONO, ISNULL(INVOICEMASTER_SODETAILS.INVOICE_ORDERON,'MTRS') AS ORDERON ", "", " INVOICEMASTER_SODETAILS INNER JOIN ITEMMASTER ON INVOICEMASTER_SODETAILS.INVOICE_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON INVOICEMASTER_SODETAILS.INVOICE_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON INVOICEMASTER_SODETAILS.INVOICE_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN REGISTERMASTER ON INVOICEMASTER_SODETAILS.INVOICE_REGISTERID = REGISTERMASTER.register_id ", " AND INVOICEMASTER_SODETAILS.INVOICE_NO = " & TEMPINVOICENO & " AND REGISTER_NAME ='" & TEMPREGNAME & "' AND INVOICEMASTER_SODETAILS.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER_SODETAILS.INVOICE_GRIDSRNO")
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        GRIDORDER.Rows.Add(Val(DTR("GRIDSRNO")), DTR("ITEMNAME"), DTR("DESIGNNO"), DTR("COLOR"), Val(DTR("ORDERQTY")), Val(DTR("ORDERMTRS")), Val(DTR("FROMNO")), Val(DTR("FROMSRNO")), DTR("FROMTYPE"), Val(DTR("GDNQTY")), Val(DTR("GDNMTRS")), Val(DTR("RATE")), DTR("PARTYPONO"), DTR("ORDERON"))
                    Next
                End If
                getsrno(GRIDORDER)


                Dim clscommon As New ClsCommon
                Dim dtID As DataTable
                dtID = clscommon.SEARCH(" register_abbr, register_initials, register_id ", "", " RegisterMaster ", " And register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
                If dtID.Rows.Count > 0 Then
                    saleregabbr = dtID.Rows(0).Item(0).ToString
                    salereginitial = dtID.Rows(0).Item(1).ToString
                    saleregid = dtID.Rows(0).Item(2)
                End If
                GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1
                CHKBARCODE.Enabled = False



                If ALLOWINVAFTEREINV = False And TXTACKNO.Text.Trim <> "" Then
                    GRATE.ReadOnly = True
                    TXTRATE.ReadOnly = True
                    CMBCHARGES.Enabled = False
                    TXTCHGSPER.ReadOnly = True
                    TXTCHARGES.ReadOnly = True
                    cmbname.Enabled = False
                    INVOICEDATE.ReadOnly = True
                    cmddelete.Enabled = False
                    TOOLDELETE.Enabled = False
                    GRIDCHGS.Enabled = False
                    GRIDINVOICE.Enabled = False
                End If




            Else
                EDIT = False
                CLEAR()

            End If
            cmbregister.Enabled = False
            If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then CMDSELECTGDN.Enabled = False
            INVOICEDATE.Focus()
            chkchange.CheckState = CheckState.Checked
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        If ISLOCKYEAR = True Then
            MsgBox("Unable to Make changes, Year is Locked", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Try
            Cursor.Current = Cursors.WaitCursor
            Dim IntResult As Integer

            Call CMBTERM_Validated(sender, e)

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList


            If CMBTERM.Text.Trim <> "" Then CMBTERM_Validated(sender, e)

            alParaval.Add(CMBSCREENTYPE.Text.Trim)

            If ALLOWMANUALINVNO = True Then
                alParaval.Add(Val(TXTINVOICENO.Text.Trim))
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(cmbregister.Text.Trim)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(TXTMULTISONO.Text.Trim)
            alParaval.Add(txtpartypono.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(sodate.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(TXTBALENOFROM.Text.Trim)
            alParaval.Add(TXTBALENOTO.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBLOCALTRANSPORT.Text.Trim)
            alParaval.Add(CMBHASTE.Text.Trim)
            alParaval.Add(CMBAGENT.Text.Trim)
            alParaval.Add(txtchallan.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(txtrefno.Text.Trim)
            alParaval.Add(CMBFORMNO.Text.Trim)
            alParaval.Add(Val(TXTCRDAYS.Text.Trim))
            alParaval.Add(duedate.Value.Date)
            alParaval.Add(CMBSUPPLIERNAME.Text.Trim)

            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(TXTVEHICLENO.Text.Trim)
            alParaval.Add(txtlrno.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(LRDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBFROMCITY.Text.Trim)
            alParaval.Add(CMBTOCITY.Text.Trim)
            alParaval.Add(CMBPACKING.Text.Trim)
            alParaval.Add(TXTEWAYBILLNO.Text.Trim)
            alParaval.Add(TXTGATEPASSNO.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(GPDATE.Text).Date, "MM/dd/yyyy"))

            If CHKBILLCHECKED.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If

            If CHKBILLDISPUTE.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If

            If CHKMANUAL.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If

            If CHKEXPORTGST.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(txtremarks.Text.Trim)

            If CHKBARCODE.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(Val(LBLTOTALBALES.Text.Trim))
            alParaval.Add(Val(lbltotalpcs.Text.Trim))
            alParaval.Add(Val(lbltotalmtrs.Text.Trim))
            alParaval.Add(Val(LBLTOTALAMT.Text.Trim))
            alParaval.Add(Val(LBLTOTALDISCAMT.Text.Trim))
            alParaval.Add(Val(LBLTOTALSPDISCAMT.Text.Trim))
            alParaval.Add(Val(LBLTOTALOTHERAMT.Text.Trim))
            alParaval.Add(Val(LBLTOTALTAXABLEAMT.Text.Trim))

            If CMBSCREENTYPE.Text = "TOTAL GST" Then
                alParaval.Add(Val(TXTCGSTPER1.Text.Trim))
                alParaval.Add(Val(TXTCGSTAMT1.Text.Trim))
                alParaval.Add(Val(TXTSGSTPER1.Text.Trim))
                alParaval.Add(Val(TXTSGSTAMT1.Text.Trim))
                alParaval.Add(Val(TXTIGSTPER1.Text.Trim))
                alParaval.Add(Val(TXTIGSTAMT1.Text.Trim))
            Else
                alParaval.Add(Val(TXTCGSTPER1.Text.Trim))
                alParaval.Add(Val(LBLTOTALCGSTAMT.Text.Trim))
                alParaval.Add(Val(TXTSGSTPER1.Text.Trim))
                alParaval.Add(Val(LBLTOTALSGSTAMT.Text.Trim))
                alParaval.Add(Val(TXTIGSTPER1.Text.Trim))
                alParaval.Add(Val(LBLTOTALIGSTAMT.Text.Trim))
            End If

            alParaval.Add(Val(TXTTOTALWITHGST.Text.Trim))
            If CHKTCS.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            alParaval.Add(Val(TXTTCSPER.Text.Trim))
            alParaval.Add(Val(TXTTCSAMT.Text.Trim))


            alParaval.Add(txtinwords.Text)

            alParaval.Add(Val(txtbillamt.Text.Trim))
            alParaval.Add(Val(TXTCHARGES.Text.Trim))
            alParaval.Add(Val(TXTSUBTOTAL.Text.Trim))
            alParaval.Add(Val(txtroundoff.Text.Trim))
            alParaval.Add(Val(txtgrandtotal.Text.Trim))

            alParaval.Add(Val(TXTAMTREC.Text.Trim))
            alParaval.Add(Val(TXTEXTRAAMT.Text.Trim))
            alParaval.Add(Val(TXTRETURN.Text.Trim))
            alParaval.Add(Val(TXTBAL.Text.Trim))
            alParaval.Add(Val(TXTSONO.Text.Trim))
            alParaval.Add(CMBTERM.Text.Trim)


            'EXPORT DETAILS
            alParaval.Add(Val(TXTROE.Text.Trim))
            alParaval.Add(CMBCIF.Text.Trim)
            alParaval.Add(TXTEXPTERMS.Text.Trim)
            alParaval.Add(TXTMARKNOS.Text.Trim)
            alParaval.Add(TXTEXPINSURANCE.Text.Trim)
            alParaval.Add(TXTVESSEL.Text.Trim)
            alParaval.Add(TXTLOADINGPORT.Text.Trim)
            alParaval.Add(TXTDISCHARGEPORT.Text.Trim)
            alParaval.Add(TXTEXPHSN.Text.Trim)
            alParaval.Add(CMBCURRENCY.Text.Trim)
            alParaval.Add(Val(TXTGROSSWT.Text.Trim))
            alParaval.Add(Val(TXTNETTWT.Text.Trim))
            alParaval.Add(Val(TXTSQMTRS.Text.Trim))
            alParaval.Add(Val(TXTTOTALUSDAMT.Text.Trim))
            alParaval.Add(Val(TXTGSTINVRATE.Text.Trim))
            alParaval.Add(Val(TXTCUSTOMINVRATE.Text.Trim))
            alParaval.Add(Val(TXTEXPDIFF.Text.Trim))
            alParaval.Add(TXTINWORDSUSD.Text.Trim)


            alParaval.Add(TXTDOCKETNO.Text.Trim)
            alParaval.Add(Format(DTDOCKETDATE.Value.Date, "MM/dd/yyyy"))
            alParaval.Add(TXTCOURIER.Text.Trim)


            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim gridsrno As String = ""
            Dim ITEMNAME As String = ""
            Dim HSNCODE As String = ""

            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim QTY As String = ""
            Dim FOLDPER As String = ""
            Dim PRINTDESC As String = ""
            Dim BALENO As String = ""
            Dim PCS As String = ""
            Dim CUT As String = ""

            Dim MTRS As String = ""
            Dim RATE As String = ""         'value of RATE
            Dim PER As String = ""
            Dim AMT As String = ""         'value of AMT
            Dim GRIDLRNO As String = ""
            Dim GRIDTRANSPORT As String = ""
            Dim DISCPER As String = ""
            Dim DISCAMT As String = ""
            Dim SPDISCPER As String = ""
            Dim SPDISCAMT As String = ""
            Dim OTHERAMT As String = ""
            Dim TAXABLEAMT As String = ""
            Dim CGSTPER As String = ""
            Dim CGSTAMT As String = ""
            Dim SGSTPER As String = ""
            Dim SGSTAMT As String = ""
            Dim IGSTPER As String = ""
            Dim IGSTAMT As String = ""
            Dim GRIDTOTAL As String = ""
            Dim BARCODE As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""
            Dim GRIDDONE As String = ""
            Dim GRIDPARTYPONO As String = ""
            Dim UNIT As String = ""
            Dim GRIDSONO As String = ""
            Dim GRIDSOSRNO As String = ""
            Dim WT As String = ""
            Dim GRIDPURPARTY As String = ""
            Dim GRIDPURPARTYBILLNO As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDINVOICE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(GSRNO.Index).Value.ToString
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        HSNCODE = row.Cells(GHSNCODE.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(GSHADE.Index).Value.ToString
                        QTY = row.Cells(GQTY.Index).Value.ToString
                        FOLDPER = row.Cells(GFOLDPER.Index).Value.ToString
                        If row.Cells(GDESCRIPTION.Index).Value <> Nothing Then PRINTDESC = row.Cells(GDESCRIPTION.Index).Value.ToString Else PRINTDESC = ""
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = row.Cells(GBALENO.Index).Value.ToString
                        PCS = Val(row.Cells(Gpcs.Index).Value.ToString)
                        CUT = Val(row.Cells(GCUT.Index).Value)

                        MTRS = Val(row.Cells(Gmtrs.Index).Value)
                        RATE = Val(row.Cells(GRATE.Index).Value)
                        PER = row.Cells(GPER.Index).Value.ToString
                        AMT = Val(row.Cells(GAMT.Index).Value)
                        GRIDLRNO = row.Cells(GLRNO.Index).Value.ToString
                        GRIDTRANSPORT = row.Cells(GTRANS.Index).Value.ToString
                        DISCPER = Val(row.Cells(GDISCPER.Index).Value)
                        DISCAMT = Val(row.Cells(GDISCAMT.Index).Value)
                        SPDISCPER = Val(row.Cells(GSPDISCPER.Index).Value)
                        SPDISCAMT = Val(row.Cells(GSPDISCAMT.Index).Value)
                        OTHERAMT = Val(row.Cells(GOTHERAMT.Index).Value)
                        TAXABLEAMT = Val(row.Cells(GTAXABLEAMT.Index).Value)
                        CGSTPER = row.Cells(GCGSTPER.Index).Value.ToString
                        CGSTAMT = Val(row.Cells(GCGSTAMT.Index).Value)
                        SGSTPER = row.Cells(GSGSTPER.Index).Value.ToString
                        SGSTAMT = Val(row.Cells(GSGSTAMT.Index).Value)
                        IGSTPER = row.Cells(GIGSTPER.Index).Value.ToString
                        IGSTAMT = Val(row.Cells(GIGSTAMT.Index).Value)
                        GRIDTOTAL = Val(row.Cells(GGRIDTOTAL.Index).Value)
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        FROMNO = row.Cells(GFROMNO.Index).Value
                        FROMSRNO = row.Cells(GFROMSRNO.Index).Value
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value
                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then GRIDDONE = "1" Else GRIDDONE = "0"
                        GRIDPARTYPONO = row.Cells(GPARTYPONO.Index).Value
                        UNIT = row.Cells(GUNIT.Index).Value
                        GRIDSONO = Val(row.Cells(GSONO.Index).Value)
                        GRIDSOSRNO = Val(row.Cells(GSOSRNO.Index).Value)
                        WT = Val(row.Cells(GWT.Index).Value)
                        GRIDPURPARTY = row.Cells(GGRIDPURPARTY.Index).Value
                        GRIDPURPARTYBILLNO = row.Cells(GPURPARTYBILLNO.Index).Value


                    Else

                        gridsrno = gridsrno & "|" & row.Cells(GSRNO.Index).Value
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value
                        HSNCODE = HSNCODE & "|" & row.Cells(GHSNCODE.Index).Value.ToString

                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(GSHADE.Index).Value.ToString
                        QTY = QTY & "|" & row.Cells(GQTY.Index).Value
                        FOLDPER = FOLDPER & "|" & row.Cells(GFOLDPER.Index).Value
                        If row.Cells(GDESCRIPTION.Index).Value <> Nothing Then PRINTDESC = PRINTDESC & "|" & row.Cells(GDESCRIPTION.Index).Value.ToString Else PRINTDESC = PRINTDESC & "|" & ""
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString
                        PCS = PCS & "|" & Val(row.Cells(Gpcs.Index).Value)
                        CUT = CUT & "|" & Val(row.Cells(GCUT.Index).Value)

                        MTRS = MTRS & "|" & Val(row.Cells(Gmtrs.Index).Value)
                        RATE = RATE & "|" & Val(row.Cells(GRATE.Index).Value)
                        PER = PER & "|" & row.Cells(GPER.Index).Value
                        AMT = AMT & "|" & Val(row.Cells(GAMT.Index).Value)
                        GRIDLRNO = GRIDLRNO & "|" & row.Cells(GLRNO.Index).Value
                        GRIDTRANSPORT = GRIDTRANSPORT & "|" & row.Cells(GTRANS.Index).Value
                        DISCPER = DISCPER & "|" & Val(row.Cells(GDISCPER.Index).Value)
                        DISCAMT = DISCAMT & "|" & Val(row.Cells(GDISCAMT.Index).Value)
                        SPDISCPER = SPDISCPER & "|" & Val(row.Cells(GSPDISCPER.Index).Value)
                        SPDISCAMT = SPDISCAMT & "|" & Val(row.Cells(GSPDISCAMT.Index).Value)
                        OTHERAMT = OTHERAMT & "|" & Val(row.Cells(GOTHERAMT.Index).Value)
                        TAXABLEAMT = TAXABLEAMT & "|" & Val(row.Cells(GTAXABLEAMT.Index).Value)
                        CGSTPER = CGSTPER & "|" & row.Cells(GCGSTPER.Index).Value
                        CGSTAMT = CGSTAMT & "|" & Val(row.Cells(GCGSTAMT.Index).Value)
                        SGSTPER = SGSTPER & "|" & row.Cells(GSGSTPER.Index).Value
                        SGSTAMT = SGSTAMT & "|" & Val(row.Cells(GSGSTAMT.Index).Value)
                        IGSTPER = IGSTPER & "|" & row.Cells(GIGSTPER.Index).Value
                        IGSTAMT = IGSTAMT & "|" & Val(row.Cells(GIGSTAMT.Index).Value)
                        GRIDTOTAL = GRIDTOTAL & "|" & Val(row.Cells(GGRIDTOTAL.Index).Value)

                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value
                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then GRIDDONE = GRIDDONE & "|" & "1" Else GRIDDONE = GRIDDONE & "|" & "0"
                        GRIDPARTYPONO = GRIDPARTYPONO & "|" & row.Cells(GPARTYPONO.Index).Value
                        UNIT = UNIT & "|" & row.Cells(GUNIT.Index).Value
                        GRIDSONO = GRIDSONO & "|" & Val(row.Cells(GSONO.Index).Value)
                        GRIDSOSRNO = GRIDSOSRNO & "|" & Val(row.Cells(GSOSRNO.Index).Value)
                        WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                        GRIDPURPARTY = GRIDPURPARTY & "|" & row.Cells(GGRIDPURPARTY.Index).Value
                        GRIDPURPARTYBILLNO = GRIDPURPARTYBILLNO & "|" & row.Cells(GPURPARTYBILLNO.Index).Value


                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(ITEMNAME)
            alParaval.Add(HSNCODE)
            alParaval.Add(QUALITY)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(QTY)
            alParaval.Add(FOLDPER)
            alParaval.Add(PRINTDESC)
            alParaval.Add(BALENO)
            alParaval.Add(PCS)
            alParaval.Add(CUT)
            alParaval.Add(MTRS)
            alParaval.Add(RATE)
            alParaval.Add(PER)
            alParaval.Add(AMT)
            alParaval.Add(GRIDLRNO)
            alParaval.Add(GRIDTRANSPORT)
            alParaval.Add(DISCPER)
            alParaval.Add(DISCAMT)
            alParaval.Add(SPDISCPER)
            alParaval.Add(SPDISCAMT)
            alParaval.Add(OTHERAMT)
            alParaval.Add(TAXABLEAMT)
            alParaval.Add(CGSTPER)
            alParaval.Add(CGSTAMT)
            alParaval.Add(SGSTPER)
            alParaval.Add(SGSTAMT)
            alParaval.Add(IGSTPER)
            alParaval.Add(IGSTAMT)
            alParaval.Add(GRIDTOTAL)
            alParaval.Add(BARCODE)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)
            alParaval.Add(GRIDDONE)
            alParaval.Add(GRIDPARTYPONO)
            alParaval.Add(UNIT)
            alParaval.Add(GRIDSONO)
            alParaval.Add(GRIDSOSRNO)
            alParaval.Add(WT)
            alParaval.Add(GRIDPURPARTY)
            alParaval.Add(GRIDPURPARTYBILLNO)


            Dim CSRNO As String = ""
            Dim CCHGS As String = ""
            Dim CPER As String = ""
            Dim CAMT As String = ""
            Dim CTAXID As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCHGS.Rows
                If row.Cells(0).Value <> Nothing Then
                    If CSRNO = "" Then
                        CSRNO = row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = row.Cells(ECHARGES.Index).Value.ToString
                        CPER = row.Cells(EPER.Index).Value.ToString
                        CAMT = Val(row.Cells(EAMT.Index).Value)
                        CTAXID = Val(row.Cells(ETAXID.Index).Value)
                    Else
                        CSRNO = CSRNO & "|" & row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = CCHGS & "|" & row.Cells(ECHARGES.Index).Value.ToString
                        CPER = CPER & "|" & row.Cells(EPER.Index).Value.ToString
                        CAMT = CAMT & "|" & Val(row.Cells(EAMT.Index).Value)
                        CTAXID = CTAXID & "|" & Val(row.Cells(ETAXID.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(CSRNO)
            alParaval.Add(CCHGS)
            alParaval.Add(CPER)
            alParaval.Add(CAMT)
            alParaval.Add(CTAXID)

            Dim griduploadsrno As String = ""
            Dim imgpath As String = ""
            Dim uploadremarks As String = ""
            Dim name As String = ""
            Dim NEWIMGPATH As String = ""
            Dim FILENAME As String = ""

            'Saving Upload Grid
            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                If row.Cells(0).Value <> Nothing Then
                    If griduploadsrno = "" Then
                        griduploadsrno = row.Cells(0).Value.ToString
                        uploadremarks = row.Cells(1).Value.ToString
                        name = row.Cells(2).Value.ToString
                        imgpath = row.Cells(3).Value.ToString
                        NEWIMGPATH = row.Cells(GNEWIMGPATH.Index).Value.ToString

                    Else
                        griduploadsrno = griduploadsrno & "|" & row.Cells(0).Value.ToString
                        uploadremarks = uploadremarks & "|" & row.Cells(1).Value.ToString
                        name = name & "|" & row.Cells(2).Value.ToString
                        imgpath = imgpath & "|" & row.Cells(3).Value.ToString
                        NEWIMGPATH = NEWIMGPATH & "|" & row.Cells(GNEWIMGPATH.Index).Value.ToString

                    End If
                End If
            Next


            alParaval.Add(griduploadsrno)
            alParaval.Add(uploadremarks)
            alParaval.Add(name)
            alParaval.Add(imgpath)
            alParaval.Add(NEWIMGPATH)
            alParaval.Add(FILENAME)

            alParaval.Add(ClientName)
            alParaval.Add(TXTIRNNO.Text.Trim)
            alParaval.Add(TXTACKNO.Text.Trim)
            alParaval.Add(Format(ACKDATE.Value.Date, "MM/dd/yyyy"))
            If PBQRCODE.Image IsNot Nothing Then
                Dim MS As New IO.MemoryStream
                PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                alParaval.Add(MS.ToArray)
            Else
                alParaval.Add(DBNull.Value)
            End If
            alParaval.Add(CMBDISPATCHFROM.Text.Trim)
            alParaval.Add(TXTSPECIALREMARKS.Text.Trim)
            If CHKCD.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            If CHKCHANGEADD.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            alParaval.Add(txtDeliveryadd.Text.Trim)
            alParaval.Add(CMBSALESMAN.Text.Trim)


            alParaval.Add(CMBSERVICETYPE.Text.Trim)
            alParaval.Add(TXTSACCODE.Text.Trim)
            alParaval.Add(Val(TXTMATERIALVALUE.Text.Trim))
            alParaval.Add(Val(TXTTOTALWITHMATVALUE.Text.Trim))
            alParaval.Add(CMBCOSTCENTERNAME.Text.Trim)
            alParaval.Add(CMBREFERREDBY.Text.Trim)
            If CHKTRADINGACC.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)




            Dim ORDERGRIDSRNO As String = ""
            Dim ORDERITEMNAME As String = ""
            Dim ORDERDESIGN As String = ""
            Dim ORDERCOLOR As String = ""
            Dim ORDERPCS As String = ""
            Dim ORDERMTRS As String = ""
            Dim ORDERFROMNO As String = ""
            Dim ORDERFROMSRNO As String = ""
            Dim ORDERFROMTYPE As String = ""
            Dim ORDERGDNPCS As String = ""
            Dim ORDERGDNMTRS As String = ""
            Dim ORDERRATE As String = ""
            Dim ORDERPARTYPONO As String = ""
            Dim ORDERON As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDORDER.Rows
                If row.Cells(0).Value <> Nothing AndAlso (Val(row.Cells(OGDNQTY.Index).Value) > 0 Or Val(row.Cells(OGDNMTRS.Index).Value) > 0) Then

                    If ORDERGRIDSRNO = "" Then
                        ORDERGRIDSRNO = Val(row.Cells(OSRNO.Index).Value)
                        ORDERITEMNAME = row.Cells(OITEMNAME.Index).Value.ToString
                        ORDERDESIGN = row.Cells(ODESIGN.Index).Value.ToString
                        ORDERCOLOR = row.Cells(OCOLOR.Index).Value.ToString
                        ORDERPCS = Val(row.Cells(OPCS.Index).Value)
                        ORDERMTRS = Val(row.Cells(OMTRS.Index).Value)
                        ORDERFROMNO = Val(row.Cells(OFROMNO.Index).Value)
                        ORDERFROMSRNO = Val(row.Cells(OFROMSRNO.Index).Value)
                        ORDERFROMTYPE = row.Cells(OFROMTYPE.Index).Value.ToString
                        ORDERGDNPCS = Val(row.Cells(OGDNQTY.Index).Value)
                        ORDERGDNMTRS = Val(row.Cells(OGDNMTRS.Index).Value)
                        ORDERRATE = Val(row.Cells(ORATE.Index).Value)
                        ORDERPARTYPONO = row.Cells(OPARTYPONO.Index).Value
                        ORDERON = row.Cells(OORDERON.Index).Value
                    Else
                        ORDERGRIDSRNO = ORDERGRIDSRNO & "|" & Val(row.Cells(OSRNO.Index).Value)
                        ORDERITEMNAME = ORDERITEMNAME & "|" & row.Cells(OITEMNAME.Index).Value.ToString
                        ORDERDESIGN = ORDERDESIGN & "|" & row.Cells(ODESIGN.Index).Value.ToString
                        ORDERCOLOR = ORDERCOLOR & "|" & row.Cells(OCOLOR.Index).Value.ToString
                        ORDERPCS = ORDERPCS & "|" & Val(row.Cells(OPCS.Index).Value)
                        ORDERMTRS = ORDERMTRS & "|" & Val(row.Cells(OMTRS.Index).Value)
                        ORDERFROMNO = ORDERFROMNO & "|" & Val(row.Cells(OFROMNO.Index).Value)
                        ORDERFROMSRNO = ORDERFROMSRNO & "|" & Val(row.Cells(OFROMSRNO.Index).Value)
                        ORDERFROMTYPE = ORDERFROMTYPE & "|" & row.Cells(OFROMTYPE.Index).Value.ToString
                        ORDERGDNPCS = ORDERGDNPCS & "|" & Val(row.Cells(OGDNQTY.Index).Value)
                        ORDERGDNMTRS = ORDERGDNMTRS & "|" & Val(row.Cells(OGDNMTRS.Index).Value)
                        ORDERRATE = ORDERRATE & "|" & Val(row.Cells(ORATE.Index).Value)
                        ORDERPARTYPONO = ORDERPARTYPONO & "|" & row.Cells(OPARTYPONO.Index).Value
                        ORDERON = ORDERON & "|" & row.Cells(OORDERON.Index).Value
                    End If
                End If
            Next

            alParaval.Add(ORDERGRIDSRNO)
            alParaval.Add(ORDERITEMNAME)
            alParaval.Add(ORDERDESIGN)
            alParaval.Add(ORDERCOLOR)
            alParaval.Add(ORDERPCS)
            alParaval.Add(ORDERMTRS)
            alParaval.Add(ORDERFROMNO)
            alParaval.Add(ORDERFROMSRNO)
            alParaval.Add(ORDERFROMTYPE)
            alParaval.Add(ORDERGDNPCS)
            alParaval.Add(ORDERGDNMTRS)
            alParaval.Add(ORDERRATE)
            alParaval.Add(ORDERPARTYPONO)
            alParaval.Add(ORDERON)

            alParaval.Add(Val(LBLTOTALWT.Text))

            Dim objclsPurord As New ClsInvoiceMaster()
            objclsPurord.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = objclsPurord.SAVE()
                TXTINVOICENO.Text = DT.Rows(0).Item(0)
                MessageBox.Show("Details Added")
                DIRECTINVOICE = False
                If ClientName = "AVIS" Or ClientName = "SUPRIYA" Or ClientName = "RMANILAL" Then
                    GENERATEEWB()
                    PRINTEWB()
                End If
                SMSCODE()
                If ClientName = "SAKARIA" Or ClientName = "NVAHAN" Or ClientName = "RMANILAL" Or ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHASHWAT" Or ClientName = "SHAILESHTRADING" Then SENDDIRECTMAIL()


                'IF ADVANCE IS RECD FROM CLIENT THEN OPEN RECEIPT FORM AUTO
                'USER WILL SAVE IT MANUALLY
                If EDIT = False And (ClientName = "CC" Or ClientName = "C3") And Val(TXTSOADVANCE.Text.Trim) > 0 Then
                    Dim OBJREC As New Receipt
                    OBJREC.TEMPAUTOENTRY = True
                    OBJREC.TEMPAMT = Val(TXTSOADVANCE.Text.Trim)
                    OBJREC.TEMPBILLNO = "S-" & Val(TXTINVOICENO.Text.Trim)
                    OBJREC.TEMPNAME = cmbname.Text.Trim
                    OBJREC.MdiParent = MDIMain
                    OBJREC.Show()
                End If


            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPINVOICENO)
                IntResult = objclsPurord.UPDATE()
                MessageBox.Show("Details Updated")

                'WE NEED TO UPDATE LRNO IN PURCHASE
                If ClientName = "SAKARIA" And txtlrno.Text.Trim <> "" And txtrefno.Text.Trim <> "" Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE PURCHASEMASTER SET BILL_LRNO = '" & txtlrno.Text.Trim & "', BILL_LRDATE = '" & LRDATE.Text & "' WHERE BILL_INITIALS = '" & txtrefno.Text.Trim & "' AND BILL_YEARID = " & YearId, "", "")
                End If

                If ClientName = "CC" Or ClientName = "C3" Then SENDDIRECTMAIL()
                If ClientName = "AVIS" And txtlrno.Text.Trim <> "" Then SENDDIRECTMAIL()

                SMSCODE()
                EDIT = False
            End If

            If ClientName <> "SOFTAS" And ClientName <> "SNCM" Then PRINTREPORT(TXTINVOICENO.Text.Trim)

            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            'clear()

            'TEMPORARILY DONE FOR ALENCOT   
            If ClientName = "ALENCOT" Or ClientName = "RMANILAL" Or ClientName = "SUPEEMA" Or ClientName = "RAJKRIPA" Then
                CLEAR()
            Else
                Call toolnext_Click(sender, e)
            End If
            If ClientName = "DAKSH" Then TXTINVOICENO.Focus() Else INVOICEDATE.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub SENDDIRECTMAIL()
        Try

            If MsgBox("Wish To Mail Invoice?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            'CHECK WHETHER EMAILID IS PRESENT IN LEDGER OR NOT, IF NOT THEN EXIT SUB WITH MESSAGE
            Dim OBJCMN As New ClsCommon
            Dim DTEMAIL As DataTable = OBJCMN.SEARCH("ACC_EMAIL AS EMAILID", "", "LEDGERS", "AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If DTEMAIL.Rows.Count > 0 AndAlso DTEMAIL.Rows(0).Item("EMAILID") <> "" Then

                Dim ALATTACHMENT As New ArrayList
                Dim OBJINVOICE As New saledesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.DIRECTPRINT = True
                OBJINVOICE.FRMSTRING = "INVOICE"
                OBJINVOICE.DIRECTMAIL = True
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STATE_REMARK,'') AS STATECODE", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID ", " AND INVOICEMASTER.INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STATECODE") <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                OBJINVOICE.registername = cmbregister.Text.Trim
                OBJINVOICE.PRINTSETTING = PRINTDIALOG
                OBJINVOICE.INVNO = Val(TXTINVOICENO.Text.Trim)
                OBJINVOICE.NOOFCOPIES = 1
                OBJINVOICE.Show()
                OBJINVOICE.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\INVOICE_" & Val(TXTINVOICENO.Text.Trim) & ".pdf")

                sendemail(DTEMAIL.Rows(0).Item("EMAILID"), ALATTACHMENT(0), "", "Invoice", ALATTACHMENT, ALATTACHMENT.Count, "", "", "", "", "")
                MsgBox("Mail Sent")
            Else
                MsgBox("Add E-Mail id in Ledger")
                Exit Sub
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROP(ByVal INVOICENO As Integer)
        Dim OBJ As New Object

        If MsgBox("Wish to Print Invoice?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

        If ClientName = "SUPRIYA" Then
            OBJ = New InvoiceReport_SUPRIYA
        End If

        '**************** SET SERVER ************************
        Dim crtableLogonInfo As New TableLogOnInfo
        Dim crConnecttionInfo As New ConnectionInfo
        Dim crTables As Tables
        Dim crTable As Table

        With crConnecttionInfo
            .ServerName = SERVERNAME
            .DatabaseName = DatabaseName
            .UserID = DBUSERNAME
            .Password = Dbpassword
            .IntegratedSecurity = Dbsecurity
        End With

        crTables = OBJ.Database.Tables
        For Each crTable In crTables
            crtableLogonInfo = crTable.LogOnInfo
            crtableLogonInfo.ConnectionInfo = crConnecttionInfo
            crTable.ApplyLogOnInfo(crtableLogonInfo)
        Next


        Dim OBJCMN As New ClsCommon
        Dim DT As New DataTable

        OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
        OBJ.RecordSelectionFormula = "{INVOICEMASTER.INVOICE_no}=" & Val(INVOICENO) & " and {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "' and {INVOICEMASTER.INVOICE_yearid}=" & YearId
        OBJ.PrintToPrinter(1, True, 0, 0)
    End Sub

    Sub PRINTREPORT(ByVal INVOICENO As Integer)
        Try

            If ClientName = "SNCM" AndAlso MsgBox("Wish to Print Challan?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJGDN As New GDNDESIGN
                OBJGDN.MdiParent = MDIMain
                OBJGDN.FRMSTRING = "GDN"
                OBJGDN.FORMULA = "{GDN.GDN_no}=" & Val(txtchallan.Text.Trim) & " and {GDN.GDN_yearid}=" & YearId
                If MsgBox("Wish to Print Rate?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJGDN.PRINTRATE = True
                OBJGDN.vendorname = cmbname.Text.Trim
                OBJGDN.agentname = CMBAGENT.Text.Trim
                OBJGDN.WHITELABEL = CHKBLANKPAPER.Checked
                OBJGDN.Show()
            End If

            If CHKOVERSEAS.CheckState = CheckState.Checked And ClientName = "AVIS" Then
                If MsgBox("Wish to Print Buyer Invoice?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim OBJINVOICE As New saledesign
                    OBJINVOICE.MdiParent = MDIMain
                    OBJINVOICE.BLANKPAPER = CHKBLANKPAPER.Checked
                    OBJINVOICE.IGSTFORMAT = False
                    OBJINVOICE.INVOICECOPYNAME = "CUSTOMER COPY"
                    If TXTSTATECODE.Text.Trim <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                    OBJINVOICE.strsearch = "{INVOICEMASTER.INVOICE_no}=" & Val(INVOICENO) & " and {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "' and {INVOICEMASTER.INVOICE_yearid}=" & YearId
                    OBJINVOICE.PARTYNAME = cmbname.Text.Trim
                    OBJINVOICE.AGENTNAME = CMBAGENT.Text.Trim
                    OBJINVOICE.FRMSTRING = "EXPBUYER"
                    OBJINVOICE.Show()
                End If

                If MsgBox("Wish to Print Custom Invoice?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim OBJINVOICE As New saledesign
                    OBJINVOICE.MdiParent = MDIMain
                    OBJINVOICE.BLANKPAPER = CHKBLANKPAPER.Checked
                    OBJINVOICE.IGSTFORMAT = False
                    OBJINVOICE.INVOICECOPYNAME = "CUSTOMER COPY"
                    If TXTSTATECODE.Text.Trim <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                    OBJINVOICE.strsearch = "{INVOICEMASTER.INVOICE_no}=" & Val(INVOICENO) & " and {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "' and {INVOICEMASTER.INVOICE_yearid}=" & YearId
                    OBJINVOICE.PARTYNAME = cmbname.Text.Trim
                    OBJINVOICE.AGENTNAME = CMBAGENT.Text.Trim
                    OBJINVOICE.FRMSTRING = "EXPCUSTOM"
                    OBJINVOICE.Show()
                End If

                If MsgBox("Wish to Print GST Invoice?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim OBJINVOICE As New saledesign
                    OBJINVOICE.MdiParent = MDIMain
                    OBJINVOICE.BLANKPAPER = CHKBLANKPAPER.Checked
                    OBJINVOICE.IGSTFORMAT = False
                    OBJINVOICE.INVOICECOPYNAME = "CUSTOMER COPY"
                    If TXTSTATECODE.Text.Trim <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                    OBJINVOICE.strsearch = "{INVOICEMASTER.INVOICE_no}=" & Val(INVOICENO) & " and {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "' and {INVOICEMASTER.INVOICE_yearid}=" & YearId
                    OBJINVOICE.PARTYNAME = cmbname.Text.Trim
                    OBJINVOICE.AGENTNAME = CMBAGENT.Text.Trim
                    OBJINVOICE.FRMSTRING = "EXPGST"
                    OBJINVOICE.Show()
                End If

                Exit Sub
            End If



            If ClientName = "MASHOK" And (GRIDINVOICE.Rows(0).Cells(GFROMTYPE.Index).Value = "YARNSALEORDER" Or GRIDINVOICE.Rows(0).Cells(GFROMTYPE.Index).Value = "OPENINGYARNSALEORDER") AndAlso MsgBox("Wish to Print Yarn DO?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJINVOICE As New saledesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.BLANKPAPER = CHKBLANKPAPER.Checked
                If TXTSTATECODE.Text.Trim <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                OBJINVOICE.strsearch = "{INVOICEMASTER.INVOICE_no}=" & Val(INVOICENO) & " and {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "' and {INVOICEMASTER.INVOICE_yearid}=" & YearId
                OBJINVOICE.PARTYNAME = cmbname.Text.Trim
                OBJINVOICE.AGENTNAME = CMBAGENT.Text.Trim
                OBJINVOICE.FRMSTRING = "YARNDO"
                OBJINVOICE.Show()
            End If



            If MsgBox("Wish to Print Invoice?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub
            Dim OBJPRINT As New PrintInvoiceFilter
            OBJPRINT.INVOICENO = INVOICENO
            OBJPRINT.REGISTERNAME = cmbregister.Text.Trim
            If CHKCHANGEADD.CheckState = CheckState.Checked Then OBJPRINT.PARTYCHANGEADD = txtDeliveryadd.Text.Trim
            OBJPRINT.PARTYNAME = cmbname.Text.Trim
            OBJPRINT.AGENTNAME = CMBAGENT.Text.Trim
            OBJPRINT.TEMPSTATECODE = TXTSTATECODE.Text.Trim
            OBJPRINT.BLANKPAPER = CHKBLANKPAPER.Checked

            OBJPRINT.ShowDialog()




            If ClientName = "DAKSH" AndAlso MsgBox("Wish to Print Envelope?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJENV As New payment_advice
                OBJENV.WHERECLAUSE = " {LEDGERS.Acc_cmpname} = '" & cmbname.Text.Trim & "' and {LEDGERS.ACC_YEARID} = " & YearId
                OBJENV.LEDGERSNAME = cmbname.Text.Trim
                OBJENV.FRMSTRING = "ENVELOPE"
                OBJENV.MdiParent = MDIMain
                OBJENV.Show()
            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True

        If (ClientName = "SBA" Or ClientName = "SOFTAS" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "MASHOK") And txtchallan.Text.Trim <> TXTINVOICENO.Text.Trim And txtchallan.Text.Trim <> "" Then
            If MsgBox("Invoice No and Challan No does not match, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                EP.SetError(txtchallan, "Invoice No and Challan No does not match")
                bln = False
            End If
        End If

        If ClientName = "MASHOK" Or ClientName = "ABHEE" Then txtchallan.Text = Val(TXTINVOICENO.Text)


        If (ClientName = "REALCORPORATION") And txtchallan.Text.Trim <> TXTINVOICENO.Text.Trim And txtchallan.Text.Trim <> "" Then
            'If MsgBox("Invoice No and Challan No does not match, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            EP.SetError(txtchallan, "Invoice No and Challan No does not match")
            bln = False
        End If
        'End If


        If Val(TXTBALENOFROM.Text.Trim) > 0 And Val(TXTBALENOTO.Text.Trim) > 0 Then
            If Val(TXTBALENOFROM.Text.Trim) > Val(TXTBALENOTO.Text.Trim) Then
                EP.SetError(TXTBALENOTO, " From Bale No Cannot Be Greater Than To Bale No ")
                bln = False
            End If
        End If

        If CMBSERVICETYPE.Text = "" Or CMBSERVICETYPE.Text = "GOODS PURCHASE" Then
            CMBSERVICETYPE.Text = "GOODS PURCHASE"
            TXTSACCODE.Clear()
        ElseIf CMBSERVICETYPE.Text = "JOB CHARGES" Then
            TXTSACCODE.Text = "998821"
        End If


        If cmbregister.Text.Trim.Length = 0 Then
            EP.SetError(cmbregister, "Enter Register Name")
            bln = False
        End If


        If ClientName = "ALENCOT" And (TXTEWAYBILLNO.Text.Trim = "" Or txtlrno.Text.Trim = "") Then
            If MsgBox("Eway Bill no / LR No Not Entered, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                EP.SetError(cmbname, "Enter Eway Bill no / LR No")
                bln = False
            End If
        End If

        If (ClientName = "NVAHAN" Or ClientName = "SAKARIA") And CMBTERM.Text.Trim = "" Then
            EP.SetError(cmbname, "Enter Party Terms")
            bln = False
        End If

        If CMBPACKING.Text.Trim = "" And cmbname.Text.Trim <> "" Then CMBPACKING.Text = cmbname.Text.Trim

        Dim OBJCMN As New ClsCommon
        Dim DT As New DataTable
        If TXTEWAYBILLNO.Text.Trim <> "" And EDIT = True And ClientName = "YASHVI" Then
            'NEED TO CHECK FROM DATABASE WHETHER THERE IS EWAYBILL NO IN DATABASE OR NOT, IF NOT PRESENT IN DATABASE THEN DONT VALIDATE
            'COZ IF THERE IS NO EWAYBILL NO IN DATABASE THEN USER CAN UPDATE THE EWAYBILLNO
            DT = OBJCMN.SEARCH("INVOICE_EWAYBILLNO AS EWAYBILLNO", "", " INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_NO = " & Val(TEMPINVOICENO) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId)
            If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("EWAYBILLNO") <> "" Then
                EP.SetError(TXTINVOICENO, "Unable To Modify Invoice, Eway Bill already Generated")
                bln = False
            End If
        End If


        'GET DEFAUKLT SCREENTYPE
        If cmbregister.Text.Trim <> "" Then
            DT = OBJCMN.SEARCH("ISNULL(DTYPE_PURSCREENTYPE,'GOODS PURCHASE') AS SERVICETYPE", "", " DEFAULTPURSCREENTYPE INNER JOIN REGISTERMASTER ON DTYPE_PURREGID = REGISTER_ID ", " AND REGISTER_NAME = '" & cmbregister.Text & "' AND DTYPE_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                CMBSERVICETYPE.Text = DT.Rows(0).Item("SERVICETYPE")
                If DT.Rows(0).Item("SERVICETYPE") = "JOB CHARGES" Then TXTSACCODE.Text = "998821" Else TXTSACCODE.Clear()
            End If
        End If


        If Val(TXTINVOICENO.Text.Trim) = 0 Then
            EP.SetError(TXTINVOICENO, "Enter Invoice No")
            bln = False
        End If

        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(cmbname, " Please Fill Company Name ")
            bln = False
        End If


        If CMBAGENT.Text.Trim.Length = 0 And ClientName = "PARAS" Then
            EP.SetError(CMBAGENT, " Please Fill Agent Name ")
            bln = False
        End If


        'If lbllocked.Visible = True Then
        '    EP.SetError(lbllocked, "Sale Return Raised, Delete Sale Return First")
        '    bln = False
        'End If

        If GRIDINVOICE.RowCount = 0 Then
            EP.SetError(cmbname, "Enter Bill Details")
            bln = False
        End If

        If ALLOWMANUALINVNO = True Then
            If TXTINVOICENO.Text <> "" And cmbname.Text.Trim <> "" And EDIT = False Then
                Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(INVOICEMASTER.INVOICE_NO, '') AS INVOICENO, REGISTERMASTER.register_name AS REGNAME ", "", " INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id AND INVOICEMASTER.INVOICE_CMPID = REGISTERMASTER.register_cmpid AND INVOICEMASTER.INVOICE_LOCATIONID = REGISTERMASTER.register_locationid AND INVOICEMASTER.INVOICE_YEARID = REGISTERMASTER.register_yearid ", "  AND INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_CMPID = " & CmpId & " AND INVOICEMASTER.INVOICE_LOCATIONID = " & Locationid & " AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    EP.SetError(TXTINVOICENO, "Invoice No Already Exist")
                    bln = False
                End If
            End If
        End If

        If Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then
            'If (txtgrandtotal.Text > 50000 And CMPSTATECODE <> TXTSTATECODE.Text.Trim) AndAlso TXTEWAYBILLNO.Text.Trim.Length = 0 Then
            '    If MsgBox("E-Way No. Not Entered, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            '        EP.SetError(TXTEWAYBILLNO, " Please Enter E-Way No..... ")
            '        bln = False
            '    End If
            'End If

            If CHKOVERSEAS.Checked = False And TXTSTATECODE.Text.Trim.Length = 0 Then
                EP.SetError(TXTSTATECODE, "Please enter the state code")
                bln = False
            End If

            If TXTGSTIN.Text.Trim.Length = 0 Then
                If MsgBox("GSTIN Not Entered, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    EP.SetError(TXTSTATECODE, "Enter GSTIN in Party Master")
                    bln = False
                End If
            End If

            If CMPSTATECODE <> TXTSTATECODE.Text.Trim And (Val(LBLTOTALCGSTAMT.Text) > 0 Or Val(LBLTOTALSGSTAMT.Text.Trim) > 0) Then
                EP.SetError(TXTSTATECODE, "Invaid Entry Done in CGST/SGST")
                bln = False
            End If

            If CMPSTATECODE = TXTSTATECODE.Text.Trim And Val(LBLTOTALIGSTAMT.Text) > 0 Then
                EP.SetError(TXTSTATECODE, "Invaid Entry Done in IGST")
                bln = False
            End If
        End If

        For Each row As DataGridViewRow In GRIDINVOICE.Rows
            If Val(row.Cells(Gmtrs.Index).Value) = 0 And Val(row.Cells(Gpcs.Index).Value) = 0 And ClientName <> "MANSI" Then
                EP.SetError(cmbname, "Mtrs & Pcs Cannot be 0")
                bln = False
            End If
            If Val(row.Cells(GAMT.Index).Value) = 0 And ClientName <> "MOMAI" Then
                EP.SetError(cmbname, "Amt Cannot be 0")
                bln = False
            End If
            If row.Cells(GHSNCODE.Index).Value = "" And ClientName <> "CC" Then
                EP.SetError(cmbname, "HSN Cannot be Blank")
                bln = False
            End If
        Next

        If Val(txtgrandtotal.Text.Trim) = 0 Then
            EP.SetError(txtgrandtotal, "Amt Cannot be 0")
            bln = False
        End If


        'SET CREDIT LIMIT
        'IF EDIT IS TRUE THEN WE NEED TO SUBTRACT THAT GRANDTOTAL FROM THEN TRIALBALANCE AMONT
        Dim OLDBAL As Double = 0
        If EDIT = True Then
            Dim DTOLD As DataTable = OBJCMN.SEARCH("INVOICE_GRANDTOTAL AS OLDGTOTAL ", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_NO = " & TEMPINVOICENO & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' And INVOICE_YEARID = " & YearId)
            If DTOLD.Rows.Count > 0 Then OLDBAL = Val(DTOLD.Rows(0).Item("OLDGTOTAL"))
        End If


        'TAKE CREDIT LIMIT FROM ALL THE COMPANIES
        If ClientName = "SUPRIYA" Then
            DT = OBJCMN.SEARCH("ACC_CRLIMIT As CRLIMIT, (CASE WHEN DR > 0 THEN DR ELSE CR END) AS BALANCE", "", "LEDGERS INNER JOIN TRIALBALANCE ON ACC_CMPNAME = NAME AND ACC_YEARID = YEARID", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND LEDGERS.ACC_YEARID IN (SELECT YEAR_ID FROM YEARMASTER WHERE YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "')")
        Else
            DT = OBJCMN.SEARCH("ACC_CRLIMIT As CRLIMIT, (CASE WHEN DR > 0 THEN DR ELSE CR END) As BALANCE", "", "LEDGERS INNER JOIN TRIALBALANCE On ACC_CMPNAME = NAME And ACC_YEARID = YEARID", " And ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)
        End If
        If DT.Rows.Count > 0 AndAlso Val(DT.Rows(0).Item("CRLIMIT")) > 0 Then
            If Format(Val(DT.Rows(0).Item("BALANCE")) - Val(OLDBAL) + Val(txtgrandtotal.Text.Trim), "0.00") > Val(DT.Rows(0).Item("CRLIMIT")) Then
                EP.SetError(cmbname, "Amount Greater then Credit Limit, Only " & Format(Val(DT.Rows(0).Item("BALANCE")) - Val(OLDBAL) - Val(DT.Rows(0).Item("CRLIMIT")), "0.00") & " allowed")
                bln = False
            End If
        End If


        'SET CRLIMIT FOR AGENTS ALSO
        'TAKE CREDIT LIMIT FROM ALL THE COMPANIES FOR AGENTS
        If ClientName = "SUPRIYA" Then
            DT = OBJCMN.SEARCH("ACC_CRLIMIT As CRLIMIT, (CASE WHEN SUM(DR)-SUM(CR) > 0 THEN SUM(DR)-SUM(CR) ELSE SUM(CR)-SUM(DR) END) AS BALANCE", "", "LEDGERS INNER JOIN TRIALBALANCE ON ACC_ID = AGENTID ", " AND ACC_CMPNAME = '" & CMBAGENT.Text.Trim & "' AND LEDGERS.ACC_YEARID IN (SELECT YEAR_ID FROM YEARMASTER WHERE YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "') GROUP BY ACC_CRLIMIT")
            If DT.Rows.Count > 0 AndAlso Val(DT.Rows(0).Item("CRLIMIT")) > 0 Then
                If Format(Val(DT.Rows(0).Item("BALANCE")) - Val(OLDBAL) + Val(txtgrandtotal.Text.Trim), "0.00") > Val(DT.Rows(0).Item("CRLIMIT")) Then
                    EP.SetError(cmbname, "Amount Greater then Credit Limit Set for Agent, Only " & Format(Val(DT.Rows(0).Item("BALANCE")) - Val(OLDBAL) - Val(DT.Rows(0).Item("CRLIMIT")), "0.00") & " allowed")
                    bln = False
                End If
            End If
        End If

        'WE HAVE TO GIVE POPUP OF REMARKS
        If ClientName = "SNCM" And txtremarks.Text.Trim <> "" Then MsgBox(txtremarks.Text.Trim, MsgBoxStyle.Critical)


        If ClientName = "YASHVI" And Val(TXTCRDAYS.Text.Trim) = 0 Then
            If MsgBox("Cr Days is 0, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                EP.SetError(cmbname, "Enter Cr Days")
                bln = False
            End If
        End If

        If UserName <> "Admin" Then
            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, "Rec/Return Made , Delete Rec/Return First")
                bln = False
            End If
        End If

        If INVOICEDATE.Text = "__/__/____" Then
            EP.SetError(INVOICEDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(INVOICEDATE.Text) Then
                EP.SetError(INVOICEDATE, "Date not in Accounting Year")
                bln = False
            End If

            If Convert.ToDateTime(INVOICEDATE.Text).Date < SALEBLOCKDATE.Date Then
                EP.SetError(INVOICEDATE, "Date is Blocked, Please make entries after " & Format(SALEBLOCKDATE.Date, "dd/MM/yyyy"))
                bln = False
            End If
        End If

        'CHECK WHETHER SALES LEDGER HAS CROSSED 50LAKHS OR NOT
        'Dim DTB As New DataTable
        'If CHKTCS.CheckState = CheckState.Unchecked And CHKPARTYTDS.CheckState = CheckState.Unchecked Then
        '    Dim TEMPTCSTOTAL As Double = Val(txtgrandtotal.Text.Trim)
        '    DT = OBJCMN.Execute_Any_String("SELECT ISNULL(SUM(INVOICE_GRANDTOTAL),0) AS GTOTAL FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID WHERE INVOICE_YEARID = " & YearId & " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "'", "", "")
        '    If DT.Rows.Count > 0 Then TEMPTCSTOTAL += Val(DT.Rows(0).Item("GTOTAL"))
        '    If TEMPTCSTOTAL > 5000000 Then
        '        If MsgBox("Amount Exceeds 5000000, and TCS is not Applied, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
        '            EP.SetError(cmbname, "Apply TCS")
        '            bln = False
        '        End If
        '    End If
        'End If




        'FOR ABHEE WE HAVE WRITTEN SEPERATE CODE
        'FOR ORDER CHECKING, FIRST REMOVE GDNQTY
        If ClientName = "ABHEE" Then
            Dim TEMPORDERROWNO As Integer = -1
            Dim TEMPORDERMATCH As Boolean = False
            If GRIDORDER.RowCount > 0 Then

                For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                    ORDROW.Cells(OGDNQTY.Index).Value = 0
                    ORDROW.Cells(OGDNMTRS.Index).Value = 0
                Next

                For Each CHROW As DataGridViewRow In GRIDINVOICE.Rows
                    CHROW.Cells(GSONO.Index).Value = 0
                    CHROW.Cells(GSOSRNO.Index).Value = 0
                Next

                'GET MULTISONO
                Dim MULTISONO() As String = (From row As DataGridViewRow In GRIDORDER.Rows.Cast(Of DataGridViewRow)() Where Not row.IsNewRow Select CStr(row.Cells(OFROMNO.Index).Value)).Distinct.ToArray
                TXTMULTISONO.Clear()
                For Each a As String In MULTISONO
                    If TXTMULTISONO.Text = "" Then
                        TXTMULTISONO.Text = a
                    Else
                        TXTMULTISONO.Text = TXTMULTISONO.Text & "," & a
                    End If
                Next

                Dim ALLOWEDQTY, BALQTY As Double
                ALLOWEDQTY = 0
                BALQTY = 0

                'ORDERON PCS
                If GRIDORDER.Rows(0).Cells(OORDERON.Index).Value = "PCS" Then

                    For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                        For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                            ' Check for matching item, design, and color (shade for PURCHASE)
                            If ROW.Cells(GITEMNAME.Index).Value = ORDROW.Cells(OITEMNAME.Index).Value And ROW.Cells(GDESIGN.Index).Value = ORDROW.Cells(ODESIGN.Index).Value And ROW.Cells(GSHADE.Index).Value = ORDROW.Cells(OCOLOR.Index).Value Then

                                TEMPORDERMATCH = True

                                BALQTY = Val(ROW.Cells(GQTY.Index).Value) - ALLOWEDQTY
                                ALLOWEDQTY = Val(ORDROW.Cells(OMTRS.Index).Value) - Val(ORDROW.Cells(OGDNMTRS.Index).Value)


                                If (Val(ORDROW.Cells(OGDNMTRS.Index).Value) = 0 And Val(ORDROW.Cells(OMTRS.Index).Value) < Val(BALQTY)) Or (Val(ORDROW.Cells(OGDNMTRS.Index).Value) >= Val(ORDROW.Cells(OMTRS.Index).Value)) Then
                                    TEMPORDERROWNO = ORDROW.Index

                                    ORDROW.Cells(OGDNMTRS.Index).Value = ALLOWEDQTY
                                    BALQTY = Val(ROW.Cells(GQTY.Index).Value) - ALLOWEDQTY

                                    ROW.Cells(GRATE.Index).Value = Val(ORDROW.Cells(ORATE.Index).Value)

                                    GoTo CHECKNEXTLINEABHEEPCS
                                End If

                                'NO NEED OF PCS
                                'ORDROW.Cells(OGRNQTY.Index).Value = Val(ORDROW.Cells(OGRNQTY.Index).Value) + Val(ROW.Cells(gQty.Index).Value)
                                ORDROW.Cells(OGDNMTRS.Index).Value = Val(ORDROW.Cells(OGDNMTRS.Index).Value) + Val(ROW.Cells(GQTY.Index).Value)
                                ROW.Cells(GRATE.Index).Value = Val(ORDROW.Cells(ORATE.Index).Value)
                                TEMPORDERROWNO = -1
                                Exit For
CHECKNEXTLINEABHEEPCS:
                            End If
                        Next

                        ' If no further matching is found but we have TEMPORDERROWNO, add value in that row
                        If TEMPORDERROWNO >= 0 Then
                            'NO NEED OF PCS
                            'GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNQTY.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNQTY.Index).Value) + Val(ROW.Cells(gQty.Index).Value)
                            GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGDNMTRS.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGDNMTRS.Index).Value) + Val(ROW.Cells(GQTY.Index).Value)
                            ROW.Cells(GRATE.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(ORATE.Index).Value)
                            TEMPORDERROWNO = -1
                        End If

                        ' If no matching was found, change row color and ask for user confirmation
                        If TEMPORDERMATCH = False Then
                            ROW.DefaultCellStyle.BackColor = Color.LightGreen

                            If MsgBox("There are Items which are not Present in Selected Order, Wish to Proceed", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                EP.SetError(cmbname, "There are Items which are not Present in Selected Order")
                                bln = False
                            End If
                        End If

                        TEMPORDERMATCH = False
                    Next

                Else
                    'ORDERON MTRS
                    For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                        For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                            ' Check for matching item, design, and color (shade for PURCHASE)
                            If ROW.Cells(GITEMNAME.Index).Value = ORDROW.Cells(OITEMNAME.Index).Value And ROW.Cells(GDESIGN.Index).Value = ORDROW.Cells(ODESIGN.Index).Value And ROW.Cells(GSHADE.Index).Value = ORDROW.Cells(OCOLOR.Index).Value Then

                                TEMPORDERMATCH = True

                                BALQTY = Val(ROW.Cells(Gmtrs.Index).Value) - ALLOWEDQTY
                                ALLOWEDQTY = Val(ORDROW.Cells(OMTRS.Index).Value) - Val(ORDROW.Cells(OGDNMTRS.Index).Value)


                                If (Val(ORDROW.Cells(OGDNMTRS.Index).Value) = 0 And Val(ORDROW.Cells(OMTRS.Index).Value) < Val(BALQTY)) Or (Val(ORDROW.Cells(OGDNMTRS.Index).Value) >= Val(ORDROW.Cells(OMTRS.Index).Value)) Then
                                    TEMPORDERROWNO = ORDROW.Index

                                    ORDROW.Cells(OGDNMTRS.Index).Value = ALLOWEDQTY
                                    BALQTY = Val(ROW.Cells(Gmtrs.Index).Value) - ALLOWEDQTY

                                    ROW.Cells(GRATE.Index).Value = Val(ORDROW.Cells(ORATE.Index).Value)

                                    GoTo CHECKNEXTLINEABHEEMTRS
                                End If

                                'NO NEED TO UPDATE PCS 
                                'ORDROW.Cells(OGRNQTY.Index).Value = Val(ORDROW.Cells(OGRNQTY.Index).Value) + Val(ROW.Cells(gQty.Index).Value)
                                ORDROW.Cells(OGDNMTRS.Index).Value = Val(ORDROW.Cells(OGDNMTRS.Index).Value) + Val(ROW.Cells(Gmtrs.Index).Value)
                                ROW.Cells(GRATE.Index).Value = Val(ORDROW.Cells(ORATE.Index).Value)
                                TEMPORDERROWNO = -1
                                Exit For
CHECKNEXTLINEABHEEMTRS:
                            End If
                        Next

                        ' If no further matching is found but we have TEMPORDERROWNO, add value in that row
                        If TEMPORDERROWNO >= 0 Then
                            'NO NEED TO UPDATE PCS
                            'GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNQTY.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNQTY.Index).Value) + Val(ROW.Cells(gQty.Index).Value)
                            GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGDNMTRS.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGDNMTRS.Index).Value) + Val(ROW.Cells(Gmtrs.Index).Value)
                            ROW.Cells(GRATE.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(ORATE.Index).Value)
                            TEMPORDERROWNO = -1
                        End If

                        ' If no matching was found, change row color and ask for user confirmation
                        If TEMPORDERMATCH = False Then
                            ROW.DefaultCellStyle.BackColor = Color.LightGreen

                            If MsgBox("There are Items which are not Present in Selected Order, Wish to Proceed", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                EP.SetError(cmbname, "There are Items which are not Present in Selected Order")
                                bln = False
                            End If
                        End If

                        TEMPORDERMATCH = False
                    Next
                End If

            End If
            TOTAL()
        End If


        If ClientName = "MASHOK" Then
            Dim TEMPORDERROWNO As Integer = -1
            Dim TEMPORDERMATCH As Boolean = False
            If GRIDORDER.RowCount > 0 Then

                For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                    ORDROW.Cells(OGDNQTY.Index).Value = 0
                    ORDROW.Cells(OGDNMTRS.Index).Value = 0
                Next


                For Each CHROW As DataGridViewRow In GRIDINVOICE.Rows
                    CHROW.Cells(GSONO.Index).Value = 0
                    CHROW.Cells(GSOSRNO.Index).Value = 0
                Next

                'GET MULTISONO
                Dim MULTISONO() As String = (From row As DataGridViewRow In GRIDORDER.Rows.Cast(Of DataGridViewRow)() Where Not row.IsNewRow Select CStr(row.Cells(OFROMNO.Index).Value)).Distinct.ToArray
                TXTMULTISONO.Clear()
                For Each a As String In MULTISONO
                    If TXTMULTISONO.Text = "" Then
                        TXTMULTISONO.Text = a
                    Else
                        TXTMULTISONO.Text = TXTMULTISONO.Text & "," & a
                    End If
                Next

                Dim ALLOWEDQTY, BALQTY As Double
                ALLOWEDQTY = 0
                BALQTY = 0
                For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                    For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                        If ROW.Cells(GITEMNAME.Index).Value = ORDROW.Cells(OITEMNAME.Index).Value And ROW.Cells(GDESIGN.Index).Value = ORDROW.Cells(ODESIGN.Index).Value And ROW.Cells(GSHADE.Index).Value = ORDROW.Cells(OCOLOR.Index).Value Then
                            TEMPORDERMATCH = True
                            'IF ITEM / DESIGN / SHADE IS MATCHED BUT THE QTY IS FULL THEN WE NEED TO KEEP THIS ROWNO IN TEMP AND NEED TO CHECK FURTHER ALSO
                            'IF WE GET ANY NEW MATHING THEN WE NEED TO INSERT THERE
                            'IF NO MATCHING IS FOUND IN FURTHER ROWS THEN WE NEED TO ADD QTY IN THIS TEMPROW
                            BALQTY = Val(ROW.Cells(Gpcs.Index).Value) - ALLOWEDQTY
                            ALLOWEDQTY = Val(ORDROW.Cells(OPCS.Index).Value) - Val(ORDROW.Cells(OGDNQTY.Index).Value)
                            If (Val(ORDROW.Cells(OGDNQTY.Index).Value) = 0 And Val(ORDROW.Cells(OPCS.Index).Value) < Val(BALQTY)) Or (Val(ORDROW.Cells(OGDNQTY.Index).Value) >= Val(ORDROW.Cells(OPCS.Index).Value)) Then
                                TEMPORDERROWNO = ORDROW.Index

                                ORDROW.Cells(OGDNQTY.Index).Value = ALLOWEDQTY
                                BALQTY = Val(ROW.Cells(Gpcs.Index).Value) - ALLOWEDQTY

                                ROW.Cells(GRATE.Index).Value = Val(ORDROW.Cells(ORATE.Index).Value)
                                ROW.Cells(GSONO.Index).Value = Val(ORDROW.Cells(OFROMNO.Index).Value)
                                ROW.Cells(GSOSRNO.Index).Value = Val(ORDROW.Cells(OFROMSRNO.Index).Value)
                                ROW.Cells(GPARTYPONO.Index).Value = ORDROW.Cells(OPARTYPONO.Index).Value

                                'DONT ALLOW EXCESS PCS
                                'If ClientName = "MASHOK" Then
                                '    TEMPORDERROWNO = -1
                                '    TEMPORDERMATCH = False
                                '    ROW.DefaultCellStyle.BackColor = Color.LightGreen
                                'End If

                                GoTo CHECKNEXTLINE
                            End If
                            ORDROW.Cells(OGDNQTY.Index).Value = Val(ORDROW.Cells(OGDNQTY.Index).Value) + Val(BALQTY)
                            ROW.Cells(GRATE.Index).Value = Val(ORDROW.Cells(ORATE.Index).Value)
                            ROW.Cells(GSONO.Index).Value = Val(ORDROW.Cells(OFROMNO.Index).Value)
                            ROW.Cells(GSOSRNO.Index).Value = Val(ORDROW.Cells(OFROMSRNO.Index).Value)
                            ROW.Cells(GPARTYPONO.Index).Value = ORDROW.Cells(OPARTYPONO.Index).Value
                            TEMPORDERROWNO = -1
                            Exit For
CHECKNEXTLINE:
                        End If
                    Next
                    'IF NO FURTHER MACHING IS FOUND BUT WE HAVE TEMPORDERROWNO THEN ADD VALUE IN THAT ROW
                    If TEMPORDERROWNO >= 0 And BALQTY > 0 Then
                        GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGDNQTY.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGDNQTY.Index).Value) + Val(BALQTY)
                        ROW.Cells(GRATE.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(ORATE.Index).Value)
                        ROW.Cells(GSONO.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OFROMNO.Index).Value)
                        ROW.Cells(GSOSRNO.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OFROMSRNO.Index).Value)
                        ROW.Cells(GPARTYPONO.Index).Value = GRIDORDER.Rows(TEMPORDERROWNO).Cells(OPARTYPONO.Index).Value
                        TEMPORDERROWNO = -1
                    End If
                    If TEMPORDERMATCH = False Then
                        ROW.DefaultCellStyle.BackColor = Color.LightGreen

                        'SALEORDER MANDATORY 
                        If ClientName = "MASHOK" Or ClientName = "ABHEE" Then
                            EP.SetError(cmbname, "There are Items which are not Present in Selected Order")
                            bln = False
                        Else
                            If MsgBox("There are Items which are not Present in Selected Order, Wish to Proceed", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                EP.SetError(cmbname, "There are Items which are not Present in Selected Order")
                                bln = False
                            End If
                        End If
                    End If
                    TEMPORDERMATCH = False
                Next
            End If
        End If



        If (ClientName = "MASHOK" Or ClientName = "ABHEE") And GRIDORDER.RowCount = 0 And CHALLANWITHOUTSO = False Then
            EP.SetError(cmbname, "Please Select Sale Order")
            bln = False
        End If

        'TO BLOCK EXCESS QTY
        If ClientName = "ABHEE" Then
            For Each ROW As DataGridViewRow In GRIDORDER.Rows
                If (ROW.Cells(OGDNMTRS.Index).Value) > Val(ROW.Cells(OMTRS.Index).Value) Then
                    EP.SetError(cmbname, "Excess Qty Not Allowed")
                    bln = False
                End If
            Next
        End If
        If ClientName = "MASHOK" Then
            For Each ROW As DataGridViewRow In GRIDORDER.Rows
                If (ROW.Cells(OGDNQTY.Index).Value) > Val(ROW.Cells(OPCS.Index).Value) Then
                    EP.SetError(cmbname, "Excess Qty Not Allowed")
                    bln = False
                End If
            Next
        End If

        'If Not datecheck(duedate.Value) Then bln = False

        Return bln
    End Function

    Sub CALCEXPORT()
        Try
            'CAL RATE IF CLIENTNAME = AVIS AND EXPORTINV IS MADE
            If GRIDINVOICE.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDINVOICE.Rows
                    If ClientName = "AVIS" And CHKOVERSEAS.Checked = True And Val(TXTROE.Text.Trim) > 0 Then
                        TXTSQMTRS.Text = Format(Val(TXTSQMTRS.Text.Trim) + Val(row.Cells(Gmtrs.Index).EditedFormattedValue) * 1.47, "0.00")
                        TXTTOTALUSDAMT.Text = Format(Val(TXTTOTALUSDAMT.Text.Trim) + (Val(row.Cells(GCUT.Index).EditedFormattedValue) * Val(row.Cells(Gmtrs.Index).EditedFormattedValue)), "0.00")
                        TXTCUSTOMINVRATE.Text = Format(Val(TXTTOTALUSDAMT.Text.Trim) / Val(TXTSQMTRS.Text.Trim), "0.000")
                        TXTGSTINVRATE.Text = Format((Val(TXTCUSTOMINVRATE.Text.Trim) * Val(TXTROE.Text.Trim)), "0.000")
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try

            TXTTCSPER.Text = 0
            TXTTCSAMT.Text = 0

            'FETCH TCSPERCENT WITH RESPECT TO DATE
            Dim dt As New DataTable
            Dim OBJCMN As New ClsCommon
            Dim DTTCS As DataTable = OBJCMN.SEARCH("TOP 1 ISNULL(TCSPER,0) AS TCSPER", "", "TCSPERCENT", " AND TCSDATE <= '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' ORDER BY TCSDATE DESC")
            If DTTCS.Rows.Count > 0 Then TXTTCSPER.Text = Val(DTTCS.Rows(0).Item("TCSPER"))



            LBLTOTALBALES.Text = "0.0"
            lbltotalpcs.Text = "0.0"
            lbltotalmtrs.Text = "0.0"

            LBLTOTALAMT.Text = "0.0"
            LBLTOTALDISCAMT.Text = 0.0
            LBLTOTALSPDISCAMT.Text = 0.0
            LBLTOTALOTHERAMT.Text = "0.0"
            LBLTOTALTAXABLEAMT.Text = "0.0"
            LBLTOTALCGSTAMT.Text = "0.0"
            LBLTOTALSGSTAMT.Text = "0.0"
            LBLTOTALIGSTAMT.Text = "0.0"

            LBLTOTALWT.Text = "0.0"

            txtbillamt.Text = 0.0
            TXTCHARGES.Text = 0.0
            TXTSUBTOTAL.Text = 0
            txtroundoff.Text = 0
            txtgrandtotal.Text = 0

            TXTTOTALWITHMATVALUE.Text = 0.0

            TXTSQMTRS.Text = 0
            TXTTOTALUSDAMT.Text = 0
            TXTGSTINVRATE.Text = 0
            TXTCUSTOMINVRATE.Text = 0

            GRIDMILLSUMM.RowCount = 0

            Dim DONE As Boolean = False

            If GRIDINVOICE.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDINVOICE.Rows

                    'CAL RATE IF CLIENTNAME = AVIS AND EXPORTINV IS MADE
                    If ClientName = "AVIS" And CHKOVERSEAS.Checked = True And Val(TXTROE.Text.Trim) > 0 And row.Cells(GCUT.Index).EditedFormattedValue > 0 Then
                        row.Cells(GRATE.Index).Value = Format(Val(row.Cells(GCUT.Index).EditedFormattedValue) * Val(TXTROE.Text.Trim), "0.00")
                    End If

                    'REVERSE CALC WHEN THEY PUNCH IN DESC AND GET RATE AUTO WITH REVERSE CALC
                    If ClientName = "MBB" And CHKREVERSECALC.Checked = True And Val(row.Cells(GDESCRIPTION.Index).EditedFormattedValue) > 0 Then
                        If Val(TXTIGSTPER1.Text.Trim) > 0 Then row.Cells(GRATE.Index).Value = Format((Val(row.Cells(GDESCRIPTION.Index).EditedFormattedValue) / (Val(TXTIGSTPER1.Text.Trim) + 100)) * 100, "0.00") Else row.Cells(GRATE.Index).Value = Format((Val(row.Cells(GDESCRIPTION.Index).EditedFormattedValue) / (Val(TXTCGSTPER1.Text.Trim) + Val(TXTSGSTPER1.Text.Trim) + 100)) * 100, "0.00")
                    End If


                    If Val(row.Cells(GRATE.Index).EditedFormattedValue) > 0 Then
                        If row.Cells(GPER.Index).EditedFormattedValue = "Pcs" Then
                            row.Cells(GAMT.Index).Value = Format(Val(row.Cells(Gpcs.Index).EditedFormattedValue) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        Else
                            row.Cells(GAMT.Index).Value = Format(Val(row.Cells(Gmtrs.Index).EditedFormattedValue) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        End If
                    End If

                    If ClientName = "REALCORPORATION" Then
                        DONE = False
                        Dim TEMPMILLNAME As String = ""
                        Dim DTMILL As DataTable = OBJCMN.SEARCH("ISNULL(MILLMASTER.MILL_NAME,'') AS MILLNAME", "", " DESIGNMASTER LEFT OUTER JOIN MILLMASTER ON DESIGN_MILLID = MILL_ID", " AND DESIGN_NO = '" & row.Cells(GDESIGN.Index).Value & "' AND DESIGN_YEARID = " & YearId)
                        If DTMILL.Rows.Count > 0 Then TEMPMILLNAME = DTMILL.Rows(0).Item("MILLNAME")
                        If GRIDMILLSUMM.RowCount = 0 Then
                            GRIDMILLSUMM.Rows.Add(TEMPMILLNAME, Format(Val(row.Cells(Gpcs.Index).EditedFormattedValue), "0.00"), Format(Val(row.Cells(Gmtrs.Index).EditedFormattedValue), "0.00"), Format(Val(row.Cells(GAMT.Index).EditedFormattedValue), "0.00"))
                        Else
                            For Each SUMMROW As DataGridViewRow In GRIDMILLSUMM.Rows
                                If SUMMROW.Cells(SMILLNAME.Index).Value = TEMPMILLNAME Then
                                    SUMMROW.Cells(SPCS.Index).Value = Format(Val(SUMMROW.Cells(SPCS.Index).EditedFormattedValue) + Val(row.Cells(Gpcs.Index).EditedFormattedValue), "0.00")
                                    SUMMROW.Cells(SMTRS.Index).Value = Format(Val(SUMMROW.Cells(SMTRS.Index).EditedFormattedValue) + Val(row.Cells(Gmtrs.Index).EditedFormattedValue), "0.00")
                                    SUMMROW.Cells(SAMT.Index).Value = Format(Val(SUMMROW.Cells(SAMT.Index).EditedFormattedValue) + Val(row.Cells(GAMT.Index).EditedFormattedValue), "0.00")
                                    DONE = True
                                End If
                            Next
                            If DONE = False Then GRIDMILLSUMM.Rows.Add(TEMPMILLNAME, Format(Val(row.Cells(Gpcs.Index).EditedFormattedValue), "0.00"), Format(Val(row.Cells(Gmtrs.Index).EditedFormattedValue), "0.00"), Format(Val(row.Cells(GAMT.Index).EditedFormattedValue), "0.00"))
                        End If
                    End If

                    If CMBSCREENTYPE.Text = "LINE GST" Then
                        If Val(row.Cells(GDISCPER.Index).EditedFormattedValue) <> 0 Then row.Cells(GDISCAMT.Index).Value = Format(Val(row.Cells(GAMT.Index).Value) * (Val(row.Cells(GDISCPER.Index).EditedFormattedValue) / 100), "0.00")
                        If Val(row.Cells(GSPDISCPER.Index).EditedFormattedValue) <> 0 Then
                            'CALC ON RUNNING TOTAL FOR EVERYONE
                            row.Cells(GSPDISCAMT.Index).Value = Format((Val(row.Cells(GAMT.Index).Value) - Val(row.Cells(GDISCAMT.Index).Value)) * (Val(row.Cells(GSPDISCPER.Index).EditedFormattedValue) / 100), "0.00")

                            'IF ANY CLIENT DONT WANT ABOVE MENTIONED CODE ENABLE BELOW CODE
                            If ClientName = "" Then
                                row.Cells(GSPDISCAMT.Index).Value = Format(Val(row.Cells(GAMT.Index).Value) * (Val(row.Cells(GSPDISCPER.Index).EditedFormattedValue) / 100), "0.00")
                            End If
                        End If
                        row.Cells(GTAXABLEAMT.Index).Value = Format(Val(row.Cells(GAMT.Index).EditedFormattedValue) - Val(row.Cells(GDISCAMT.Index).EditedFormattedValue) - Val(row.Cells(GSPDISCAMT.Index).EditedFormattedValue) + Val(row.Cells(GOTHERAMT.Index).EditedFormattedValue), "0.00")


                        If ClientName = "GELATO" Or ClientName = "KREEVE" Then
                            'CHANGE GST% WITH RESPECT TO RATE (TAXABLEAMT / QTY)
                            'dt = OBJCMN.search("ISNULL(HSN_CGST,0) AS CGST, ISNULL(HSN_SGST,0) AS SGST, ISNULL(HSN_IGST,0) AS IGST, ISNULL(HSN_RATE1,0) AS RATE, ISNULL(HSN_CGST1,0) AS CGST1, ISNULL(HSN_SGST1,0) AS SGST1, ISNULL(HSN_IGST1,0) AS IGST1", "", "ITEMMASTER INNER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID", " AND ITEMMASTER.ITEM_NAME = '" & row.Cells(GITEMNAME.Index).Value & "' AND HSN_YEARID = " & YearId)
                            dt = OBJCMN.SEARCH(" TOP 1 ISNULL(HSNMASTER_DESC.HSN_CGST,0) AS CGST, ISNULL(HSNMASTER_DESC.HSN_SGST,0) AS SGST, ISNULL(HSNMASTER_DESC.HSN_IGST,0) AS IGST, ISNULL(HSNMASTER_DESC.HSN_RATE1,0) AS RATE, ISNULL(HSNMASTER_DESC.HSN_CGST1,0) AS CGST1, ISNULL(HSNMASTER_DESC.HSN_SGST1,0) AS SGST1, ISNULL(HSNMASTER_DESC.HSN_IGST1,0) AS IGST1, ISNULL(HSNMASTER_DESC.HSN_EXPCGST,0) AS EXPCGST, ISNULL(HSNMASTER_DESC.HSN_EXPSGST,0) AS EXPSGST, ISNULL(HSNMASTER_DESC.HSN_EXPIGST,0) AS EXPIGST", "", "ITEMMASTER INNER JOIN HSNMASTER ON ITEM_HSNCODEID = HSNMASTER.HSN_ID INNER JOIN HSNMASTER_DESC ON HSNMASTER.HSN_ID = HSNMASTER_DESC.HSN_ID", " AND HSNMASTER_DESC.HSN_WEFDATE <= '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND ITEMMASTER.ITEM_NAME = '" & row.Cells(GITEMNAME.Index).Value & "' AND HSNMASTER.HSN_YEARID = " & YearId & " ORDER BY HSNMASTER_DESC.HSN_WEFDATE DESC")
                            If dt.Rows.Count > 0 Then
                                'IF WE HAVE NOT MENTIONED RATE SECTION IN HSNCODE THEN APPLY NORMAL GST RATES AND NOT GST1 RATES, OR ELSE IF EXPORT IS CHECKED THEN ALSO GOTO NORATE SECTION
                                If Val(dt.Rows(0).Item("RATE")) = 0 Or CHKEXPORTGST.Checked = True Then GoTo NORATE

                                'THIS CODE WAS REMOVED AS PER CLIENTS REQUIREMENT
                                'If Val(dt.Rows(0).Item("RATE")) <= Format((Val(row.Cells(GAMT.Index).EditedFormattedValue) + Val(row.Cells(GOTHERAMT.Index).EditedFormattedValue)) / Val(row.Cells(Gpcs.Index).EditedFormattedValue), "0.00") Then

                                'IN NEW CHANGES CLIENT NEEDS RATE-DISCOUNT-SPECIALDISCOUT, AND THEN WHETEVER THE RATES COMES, WE HAVE TO CALC GSTRATE IN THAT RATE
                                'If Val(dt.Rows(0).Item("RATE")) <= Format(Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00") Then
                                Dim CALCON As Double = 0.0
                                If ClientName = "GELATO" Then
                                    CALCON = Format((Val(row.Cells(GAMT.Index).EditedFormattedValue) - Val(row.Cells(GDISCAMT.Index).EditedFormattedValue) - Val(row.Cells(GSPDISCAMT.Index).EditedFormattedValue)) / Val(row.Cells(Gpcs.Index).EditedFormattedValue), "0.00")
                                Else
                                    CALCON = Format(Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                                End If

                                If Val(dt.Rows(0).Item("RATE")) <= CALCON Then
                                    If Val(row.Cells(GCGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GCGSTPER.Index).Value = Val(dt.Rows(0).Item("CGST1"))
                                    If Val(row.Cells(GSGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GSGSTPER.Index).Value = Val(dt.Rows(0).Item("SGST1"))
                                    If Val(row.Cells(GIGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GIGSTPER.Index).Value = Val(dt.Rows(0).Item("IGST1"))
                                Else
NORATE:

                                    If CHKEXPORTGST.Checked = True Then
                                        If Val(row.Cells(GCGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GCGSTPER.Index).Value = Val(dt.Rows(0).Item("EXPCGST"))
                                        If Val(row.Cells(GSGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GSGSTPER.Index).Value = Val(dt.Rows(0).Item("EXPSGST"))
                                        If Val(row.Cells(GIGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GIGSTPER.Index).Value = Val(dt.Rows(0).Item("EXPIGST"))
                                    Else
                                        If Val(row.Cells(GCGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GCGSTPER.Index).Value = Val(dt.Rows(0).Item("CGST"))
                                        If Val(row.Cells(GSGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GSGSTPER.Index).Value = Val(dt.Rows(0).Item("SGST"))
                                        If Val(row.Cells(GIGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GIGSTPER.Index).Value = Val(dt.Rows(0).Item("IGST"))
                                    End If

                                End If
                            End If
                        End If



                        If CHKMANUAL.CheckState = CheckState.Unchecked Then
                            row.Cells(GCGSTAMT.Index).Value = Format((Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) * Val(row.Cells(GCGSTPER.Index).EditedFormattedValue) / 100), "0.00")
                            row.Cells(GSGSTAMT.Index).Value = Format((Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) * Val(row.Cells(GSGSTPER.Index).EditedFormattedValue) / 100), "0.00")
                            row.Cells(GIGSTAMT.Index).Value = Format((Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) * Val(row.Cells(GIGSTPER.Index).EditedFormattedValue) / 100), "0.00")
                        End If
                        row.Cells(GGRIDTOTAL.Index).Value = Format(Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) + Val(row.Cells(GCGSTAMT.Index).EditedFormattedValue) + Val(row.Cells(GSGSTAMT.Index).EditedFormattedValue) + Val(row.Cells(GIGSTAMT.Index).EditedFormattedValue), "0.00")
                    Else
                        row.Cells(GTAXABLEAMT.Index).Value = Format(Val(row.Cells(GAMT.Index).EditedFormattedValue) - Val(row.Cells(GDISCAMT.Index).EditedFormattedValue) - Val(row.Cells(GSPDISCAMT.Index).EditedFormattedValue) + Val(row.Cells(GOTHERAMT.Index).EditedFormattedValue), "0.00")
                        row.Cells(GGRIDTOTAL.Index).Value = Format(Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) + Val(row.Cells(GCGSTAMT.Index).EditedFormattedValue) + Val(row.Cells(GSGSTAMT.Index).EditedFormattedValue) + Val(row.Cells(GIGSTAMT.Index).EditedFormattedValue), "0.00")
                    End If

                    'If row.Cells(GBALENO.Index).Value <> "" Then LBLTOTALBALES.Text = Format(Val(LBLTOTALBALES.Text) + Val(row.Cells(GBALENO.Index).EditedFormattedValue), "0")
                    If Val(row.Cells(Gpcs.Index).Value) <> 0 Then lbltotalpcs.Text = Format(Val(lbltotalpcs.Text) + Val(row.Cells(Gpcs.Index).EditedFormattedValue), "0")
                    If Val(row.Cells(Gmtrs.Index).Value) <> 0 Then lbltotalmtrs.Text = Format(Val(lbltotalmtrs.Text) + Val(row.Cells(Gmtrs.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GAMT.Index).Value) <> 0 Then LBLTOTALAMT.Text = Format(Val(LBLTOTALAMT.Text) + Val(row.Cells(GAMT.Index).EditedFormattedValue), "0.00")

                    If Val(row.Cells(GDISCAMT.Index).Value) <> 0 Then LBLTOTALDISCAMT.Text = Format(Val(LBLTOTALDISCAMT.Text) + Val(row.Cells(GDISCAMT.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GSPDISCAMT.Index).Value) <> 0 Then LBLTOTALSPDISCAMT.Text = Format(Val(LBLTOTALSPDISCAMT.Text) + Val(row.Cells(GSPDISCAMT.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GOTHERAMT.Index).Value) <> 0 Then LBLTOTALOTHERAMT.Text = Format(Val(LBLTOTALOTHERAMT.Text) + Val(row.Cells(GOTHERAMT.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GTAXABLEAMT.Index).Value) <> 0 Then LBLTOTALTAXABLEAMT.Text = Format(Val(LBLTOTALTAXABLEAMT.Text) + Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue), "0.00")

                    If Val(row.Cells(GCGSTAMT.Index).Value) <> 0 Then LBLTOTALCGSTAMT.Text = Format(Val(LBLTOTALCGSTAMT.Text) + Val(row.Cells(GCGSTAMT.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GSGSTAMT.Index).Value) <> 0 Then LBLTOTALSGSTAMT.Text = Format(Val(LBLTOTALSGSTAMT.Text) + Val(row.Cells(GSGSTAMT.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GIGSTAMT.Index).Value) <> 0 Then LBLTOTALIGSTAMT.Text = Format(Val(LBLTOTALIGSTAMT.Text) + Val(row.Cells(GIGSTAMT.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GGRIDTOTAL.Index).Value) <> 0 Then txtbillamt.Text = Format(Val(txtbillamt.Text) + Val(row.Cells(GGRIDTOTAL.Index).EditedFormattedValue), "0.00")

                    If Val(row.Cells(GWT.Index).Value) <> 0 Then LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(row.Cells(GWT.Index).EditedFormattedValue), "0.00")
                Next
            End If

            If CHKOVERSEAS.Checked = True Then txtbillamt.Text = Format(Val(txtbillamt.Text) + Val(TXTEXPDIFF.Text.Trim), "0.00")


            If GRIDCHGS.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDCHGS.Rows
                    If SALEAUTODISCOUNT = True And ClientName <> "YASHVI" Then
                        'IF PERCENT IS > 0 THEN GETAUTO CHARGES
                        dt = OBJCMN.SEARCH("ISNULL(ACC_CALC,'GROSS') AS CALC", "", "LEDGERS", "AND ACC_CMPNAME = '" & row.Cells(ECHARGES.Index).Value & "' AND ACC_YEARID = " & YearId)
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item("CALC") = "GROSS" And Val(row.Cells(EPER.Index).Value) <> 0 Then
                                row.Cells(EAMT.Index).Value = Format((Val(row.Cells(EPER.Index).Value) * Val(txtbillamt.Text.Trim)) / 100, "0.00")
                            ElseIf dt.Rows(0).Item("CALC") = "NETT" And Val(row.Cells(EPER.Index).Value) <> 0 Then
                                TXTNETTAMT.Text = Val(txtbillamt.Text.Trim)
                                For I As Integer = 0 To row.Index - 1
                                    TXTNETTAMT.Text = Format(Val(TXTNETTAMT.Text) + Val(GRIDCHGS.Rows(I).Cells(EAMT.Index).Value), "0.00")
                                Next
                                row.Cells(EAMT.Index).Value = Format((Val(row.Cells(EPER.Index).Value) * Val(TXTNETTAMT.Text.Trim)) / 100, "0.00")
                                'TXTCHGSAMT.Text = Format((Val(TXTNETT.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
                            ElseIf dt.Rows(0).Item("CALC") = "MTRS" And Val(row.Cells(EPER.Index).Value) <> 0 Then
                                row.Cells(EAMT.Index).Value = Format(Val(row.Cells(EPER.Index).Value) * Val(lbltotalmtrs.Text.Trim), "0.00")
                            ElseIf dt.Rows(0).Item("CALC") = "PCS" And Val(row.Cells(EPER.Index).Value) <> 0 Then
                                row.Cells(EAMT.Index).Value = Format(Val(row.Cells(EPER.Index).Value) * Val(lbltotalpcs.Text.Trim), "0.00")
                            End If
                        End If
                    End If
                    TXTCHARGES.Text = Format(Val(TXTCHARGES.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
                Next
            End If

            TXTSUBTOTAL.Text = Format(Val(txtbillamt.Text) + Val(TXTCHARGES.Text.Trim), "0.00")

            If CMBSCREENTYPE.Text = "TOTAL GST" Then
                If CHKMANUAL.CheckState = CheckState.Unchecked Then
                    TXTCGSTAMT1.Text = Format((Val(TXTSUBTOTAL.Text.Trim) * Val(TXTCGSTPER1.Text.Trim)) / 100, "0.00")
                    LBLTOTALCGSTAMT.Text = Val(TXTCGSTAMT1.Text.Trim)
                    TXTSGSTAMT1.Text = Format((Val(TXTSUBTOTAL.Text.Trim) * Val(TXTSGSTPER1.Text.Trim)) / 100, "0.00")
                    LBLTOTALSGSTAMT.Text = Val(TXTSGSTAMT1.Text.Trim)
                    TXTIGSTAMT1.Text = Format((Val(TXTSUBTOTAL.Text.Trim) * Val(TXTIGSTPER1.Text.Trim)) / 100, "0.00")
                    LBLTOTALIGSTAMT.Text = Val(TXTIGSTAMT1.Text.Trim)
                End If

                TXTTOTALWITHGST.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT1.Text.Trim) + Val(TXTSGSTAMT1.Text.Trim) + Val(TXTIGSTAMT1.Text.Trim), "0.00")
                If CHKTCS.CheckState = CheckState.Checked Then
                    If ClientName = "MNIKHIL" Then
                        TXTTCSAMT.Text = Format((Val(TXTSUBTOTAL.Text.Trim) * Val(TXTTCSPER.Text.Trim)) / 100, "0")
                    Else
                        TXTTCSAMT.Text = Format((Val(TXTTOTALWITHGST.Text.Trim) * Val(TXTTCSPER.Text.Trim)) / 100, "0")
                    End If
                End If


                txtgrandtotal.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT1.Text.Trim) + Val(TXTSGSTAMT1.Text.Trim) + Val(TXTIGSTAMT1.Text.Trim) + Val(TXTTCSAMT.Text.Trim), "0")
                txtroundoff.Text = Format(Val(txtgrandtotal.Text) - (Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT1.Text.Trim) + Val(TXTSGSTAMT1.Text.Trim) + Val(TXTIGSTAMT1.Text.Trim) + Val(TXTTCSAMT.Text.Trim)), "0.00")
            Else

                TXTTOTALWITHGST.Text = Format(Val(TXTSUBTOTAL.Text.Trim), "0.00")
                If CHKTCS.CheckState = CheckState.Checked Then TXTTCSAMT.Text = Format((Val(TXTTOTALWITHGST.Text.Trim) * Val(TXTTCSPER.Text.Trim)) / 100, "0")

                txtgrandtotal.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTTCSAMT.Text.Trim), "0")
                txtroundoff.Text = Format(Val(txtgrandtotal.Text) - (Val(TXTSUBTOTAL.Text) + Val(TXTTCSAMT.Text.Trim)), "0.00")
            End If


            txtgrandtotal.Text = Format(Val(txtgrandtotal.Text), "0.00")


            'THIS COL IS USED FOR JOBWORK EWAY BILL
            TXTTOTALWITHMATVALUE.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTMATERIALVALUE.Text.Trim), "0.00")


            CALCEXPORT()


            If Val(txtgrandtotal.Text) > 0 Then txtinwords.Text = CurrencyToWord(txtgrandtotal.Text)
            If Val(TXTTOTALUSDAMT.Text) > 0 Then TXTINWORDSUSD.Text = Replace(Replace(CurrencyToWord(TXTTOTALUSDAMT.Text).ToString, "Rupees", "USD").ToString, "Paise", "Cents")

            BALECOUNT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub BALECOUNT()
        Try
            LBLTOTALBALES.Text = 0
            Dim dic As New Dictionary(Of String, Integer)()
            Dim cellValue As String
            For i = 0 To GRIDINVOICE.Rows.Count - 1
                If Not GRIDINVOICE.Rows(i).IsNewRow Then
                    cellValue = GRIDINVOICE(GBALENO.Index, i).EditedFormattedValue.ToString()
                    If cellValue <> "" Then
                        If Not dic.ContainsKey(cellValue) Then
                            dic.Add(cellValue, 1)
                        Else
                            dic(cellValue) += 1
                        End If
                    End If
                End If
            Next
            LBLTOTALBALES.Text = Val(dic.Count)
            If ClientName = "CHINTAN" Or ClientName = "SNCM" Then TXTBALENOFROM.Text = Val(LBLTOTALBALES.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try
            If ISLOCKYEAR = True Then
                MsgBox("Unable to Make changes, Year is Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If TXTIRNNO.Text.Trim <> "" And TXTACKNO.Text.Trim <> "" And ALLOWINVAFTEREINV = False Then
                EP.SetError(TXTINVOICENO, "You are not allowed to make changes as E-Invoice is Generated")
                Exit Sub
            End If

            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Rec / Return Made, Delete Rec First", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If Convert.ToDateTime(INVOICEDATE.Text).Date < SALEBLOCKDATE.Date Then
                    MsgBox("Date is Blocked, Please Delete entries after " & Format(SALEBLOCKDATE.Date, "dd/MM/yyyy"), MsgBoxStyle.Critical)
                    Exit Sub
                End If

                'IF TCS CHALLAN IS GENERATED THEN LOCK THE ENTRY
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("TCSCHA_BILLNO AS BILLNO", "", " TCSCHALLAN INNER JOIN REGISTERMASTER ON TCSCHA_REGISTERID = REGISTER_ID", " AND TCSCHA_YEARID = " & YearId & " AND TCSCHA_BILLNO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "'")
                If DT.Rows.Count > 0 Then
                    MsgBox("TCS Challan Raised, Please Remove Entry from TCS Challan First", MsgBoxStyle.Critical)
                    Exit Sub
                End If


                tempMsg = MsgBox("Delete Invoice ?", MsgBoxStyle.YesNo)
                If tempMsg = vbYes Then


                    'FIRST MAIL THE INVOICE 
                    If ClientName = "AVIS" Or ClientName = "SNCM" Then SENDMAILBEFOREDELETE()



                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTINVOICENO.Text.Trim)
                    alParaval.Add(TEMPREGNAME)
                    If CHKBARCODE.Checked = True Then
                        alParaval.Add(1)
                    Else
                        alParaval.Add(0)
                    End If
                    alParaval.Add(Userid)
                    alParaval.Add(CmpId)
                    alParaval.Add(Locationid)
                    alParaval.Add(YearId)

                    Dim clspo As New ClsInvoiceMaster()
                    clspo.alParaval = alParaval
                    IntResult = clspo.Delete()
                    MsgBox("Invoice Deleted")
                    CLEAR()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub SENDMAILBEFOREDELETE()
        Try

            Dim ALATTACHMENT As New ArrayList
            '**************** SET SERVER ************************
            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table

            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions

            Dim TOMAIL As String = ""
            Dim SUBJECT As String = ""
            Dim OBJ As New Object
            If ClientName = "AVIS" Then
                OBJ = New InvoiceReport_AVIS
                TOMAIL = "infoavisindustries@gmail.com"
                SUBJECT = "Deleted Invoice"

            ElseIf ClientName = "SNCM" Then
                OBJ = New InvoiceReport_TOTALLEFT
                TOMAIL = "nakoda8020@gmail.com"
                SUBJECT = "Deleted Invoice"
            End If

            crTables = OBJ.Database.Tables
            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = "{INVOICEMASTER.INVOICE_no}=" & Val(TEMPINVOICENO) & " and {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "' and {INVOICEMASTER.INVOICE_yearid}=" & YearId

            oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE_" & TEMPINVOICENO & ".pdf"
            expo = OBJ.ExportOptions
            expo.ExportDestinationType = ExportDestinationType.DiskFile
            expo.ExportFormatType = ExportFormatType.PortableDocFormat
            expo.DestinationOptions = oDfDopt
            OBJ.Export()
            ALATTACHMENT.Add(oDfDopt.DiskFileName)

            sendemail(TOMAIL, ALATTACHMENT(0), "", SUBJECT, ALATTACHMENT, ALATTACHMENT.Count, "", "", "", "", "")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Toolprevious.Click
        Try
            GRIDINVOICE.RowCount = 0
LINE1:
            TEMPINVOICENO = Val(TXTINVOICENO.Text) - 1
            TEMPREGNAME = cmbregister.Text.Trim
            If TEMPINVOICENO > 0 Then
                EDIT = True
                'INVOICEMASTER_Load(sender, e)
                SHOWDATA()
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDINVOICE.RowCount = 0 And TEMPINVOICENO > 1 Then
                TXTINVOICENO.Text = TEMPINVOICENO
                GoTo LINE1
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDINVOICE.RowCount = 0
LINE1:
            TEMPINVOICENO = Val(TXTINVOICENO.Text) + 1
            TEMPREGNAME = cmbregister.Text.Trim
            getmax_INVOICE_no()
            Dim MAXNO As Integer = TXTINVOICENO.Text.Trim
            CLEAR()
            If Val(TXTINVOICENO.Text) - 1 >= TEMPINVOICENO Then
                EDIT = True
                'INVOICEMASTER_Load(sender, e)
                SHOWDATA()
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDINVOICE.RowCount = 0 And TEMPINVOICENO < MAXNO Then
                TXTINVOICENO.Text = TEMPINVOICENO
                GoTo LINE1
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

            Dim objINVDTLS As New InvoiceDetails
            objINVDTLS.MdiParent = MDIMain
            objINVDTLS.Show()
            objINVDTLS.BringToFront()
            ' Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            NAMEVALIDATE(cmbname, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Try
            cmdOK_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPINVOICENO)
            If ClientName <> "SUPRIYA" Then PRINTEWB()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDINVOICE.RowCount = 0
                TEMPINVOICENO = Val(tstxtbillno.Text)
                TEMPREGNAME = cmbregister.Text.Trim
                If TEMPINVOICENO > 0 Then
                    EDIT = True
                    'INVOICEMASTER_Load(sender, e)
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

    Private Sub CMDSELECTGDN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTGDN.Click
        Try
            Dim OBJCMN As New ClsCommon
            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If ClientName <> "KCRAYON" And ClientName <> "YASHVI" And ClientName <> "SUPRIYA" Then
                If cmbname.Text = "" Then
                    MsgBox("Select Party Name First !", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If


            Dim DTTABLE As New DataTable
            Dim OBJSELECTPO As New SelectGdn


            If ClientName = "AFW" And DIRECTINVOICE = True Then

                DTTABLE.Columns.Add("GDNNO")
                DTTABLE.Columns.Add("GDNDATE")
                DTTABLE.Columns.Add("CHALLANNO")
                DTTABLE.Columns.Add("NAME")
                DTTABLE.Columns.Add("PCS")
                DTTABLE.Columns.Add("MTRS")
                DTTABLE.Columns.Add("GODOWN")
                DTTABLE.Columns.Add("TOTALBALES")
                DTTABLE.Columns.Add("TOTALAMT")
                DTTABLE.Columns.Add("GPNO")
                DTTABLE.Columns.Add("GPDATE")
                DTTABLE.Columns.Add("FROMTYPE")


                DTTABLE.Rows.Add(Val(TEMPPURNO), Now.Date, "", TEMPPARTYNAME, 0, 0, "", 0, 0, 0, Now.Date, "GDN")

                GoTo AFWSKIPLINE
            End If


            OBJSELECTPO.PARTYNAME = cmbname.Text.Trim
            If ClientName = "AVIS" Then OBJSELECTPO.FRMSTRING = "GATEPASS"
            OBJSELECTPO.ShowDialog()

            DTTABLE = OBJSELECTPO.DT1


AFWSKIPLINE:    'SKIP DIRECTLY HERE FOR AFW (AHWAMEGH)

            Dim i As Integer = 0
            If DTTABLE.Rows.Count > 0 Then
                chkchange.Checked = True
                If ClientName = "AVIS" Then
                    GRIDINVOICE.RowCount = 0
                    ''  GETTING DISTINCT GATEPASS NO IN TEXTBOX
                    Dim DVGP As DataView = DTTABLE.DefaultView
                    Dim NEWDTGP As DataTable = DVGP.ToTable(True, "GPNO")
                    For Each DTR As DataRow In NEWDTGP.Rows
                        If TXTGATEPASSNO.Text.Trim = "" Then
                            TXTGATEPASSNO.Text = DTR("GPNO").ToString
                        Else
                            TXTGATEPASSNO.Text = TXTGATEPASSNO.Text & "," & DTR("GPNO").ToString
                        End If
                    Next
                End If

                GPDATE.Text = DTTABLE.Rows(0).Item("GPDATE")

                If (ClientName = "DAKSH" Or ClientName = "SHALIBHADRA" Or ClientName = "MAHAVIRPOLYCOT") Then INVOICEDATE.Text = DTTABLE.Rows(0).Item("GDNDATE")
                TabControl2.SelectedIndex = 0

                ''  GETTING DISTINCT CHALLAN NO IN TEXTBOX
                Dim DV As DataView = DTTABLE.DefaultView
                Dim NEWDT As DataTable = DV.ToTable(True, "GDNNO")
                For Each DTR As DataRow In NEWDT.Rows
                    If txtchallan.Text.Trim = "" Then
                        txtchallan.Text = DTR("GDNNO").ToString
                    Else
                        txtchallan.Text = txtchallan.Text & "," & DTR("GDNNO").ToString
                    End If
                    If ClientName = "RMANILAL" Or ClientName = "YASHVI" Or ClientName = "SUPEEMA" Or ClientName = "TCOT" Or ClientName = "SIMPLEX" Or ClientName = "ANMOL" Or ClientName = "JAGDAMBA" Then TXTBALENOFROM.Text = Val(TXTBALENOFROM.Text) + 1
                    If ClientName = "MANSI" Or ClientName = "VALIANT" Or ClientName = "SARAYU" Then
                        Dim TEMPROW() As DataRow = DTTABLE.Select("GDNNO=" & Val(DTR("GDNNO")))
                        If TEMPROW.Length > 0 Then TXTBALENOFROM.Text = Val(TXTBALENOFROM.Text.Trim) + Val(TEMPROW(0).Item("TOTALBALES"))
                    End If
                Next

                For i = 0 To DTTABLE.Rows.Count - 1
                    Dim objclspreq As New ClsCommon()
                    Dim DT As New DataTable
                    If DTTABLE.Rows(0).Item("FROMTYPE") = "GDN" Then
                        DT = objclspreq.SEARCH(" ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(GDN.GDN_TYPENO, 0) AS TYPECHALLANNO, GDN.GDN_date AS CHALLANDATE, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN,ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(HASTELEDGERS.ACC_CMPNAME,'') AS HASTE, ISNULL(AGENTMASTER.Acc_cmpname, '') AS AGENT, ISNULL(GDN.GDN_MULTISONO, '') AS MULTISONO,ISNULL(GDN.GDN_PONO, '') AS PARTYPONO, GDN.GDN_SODATE AS SODATE, ISNULL(GDN.GDN_SONO, 0) AS SONO, ISNULL(GDN.GDN_SOGRIDSRNO, 0) AS SOGRIDSRNO,GDN.GDN_SODATE AS SODATE, ISNULL(TRANSLEDGER.Acc_cmpname, '') AS TRANS, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(GDN.GDN_LRNO, '') AS LRNO, GDN.GDN_LRDATE AS LRDATE, ISNULL(GDN.GDN_TRANSREFNO, '') AS TRANSREFF, ISNULL(GDN.GDN_TRANSREMARKS, '') AS TRANSREMARKS, ISNULL(GDN.GDN_TOTALBALES, 0) AS TOTALBALES, ISNULL(GDN.GDN_TOTALPCS, 0) AS TOTALPCS, ISNULL(GDN.GDN_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(GDN.GDN_TOTALAMT, 0) AS TOTALAMT, ISNULL(GDN_DESC.GDN_SRNO, 0) AS SRNO, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO, ISNULL(GDN_DESC.GDN_PCS, 0) AS PCS, ISNULL(GDN_DESC.GDN_MTRS, 0) AS MTRS, ISNULL(GDN_DESC.GDN_RATE, 0) AS RATE, ISNULL(GDN_DESC.GDN_PER, '') AS PER, ISNULL(GDN_DESC.GDN_AMOUNT, 0) AS AMOUNT, ISNULL(GDN_DESC.GDN_BARCODE, '') AS BARCODE, ISNULL(GDN_DESC.GDN_FROMNO, 0) AS FROMNO, ISNULL(GDN_DESC.GDN_FROMSRNO, 0) AS FROMSRNO, ISNULL(GDN_DESC.GDN_FROMTYPE, '') AS FROMTYPE, ISNULL(GDN.GDN_BALENOFROM, 0) AS BALENOFROM, ISNULL(GDN.GDN_BALENOTO, 0) AS BALENOTO, ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS PACKING,ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR), '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(GDN.GDN_REMARKS,'') AS REMARKS, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO,'') AS GRIDPARTYPONO ", "", "   GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON GDN.GDN_DISPATCHTOID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN LEDGERS AS HASTELEDGERS ON GDN.GDN_JOBBERID = HASTELEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id  ", "  and GDN.GDN_NO='" & DTTABLE.Rows(i).Item("GDNNO") & "'  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " ORDER BY GDN_DESC.GDN_SRNO")

                        If ClientName = "SNCM" Then
                            Dim DT2 As DataTable = OBJCMN.SEARCH(" ISNULL(so_no, 0) AS SONO, ISNULL(so_remarks, '') AS REMARKS", "", " SALEORDER", " AND SO_NO = " & Val(DT.Rows(0).Item("SONO")) & "And SO_YEARID = " & YearId)
                            If DT2.Rows.Count > 0 Then
                                If DT2.Rows(0).Item("REMARKS") <> "" Then MsgBox(DT2.Rows(0).Item("REMARKS"), MsgBoxStyle.Critical)
                            End If
                        End If

                    ElseIf DTTABLE.Rows(0).Item("FROMTYPE") = "OPENINGGDN" Then
                        DT = objclspreq.SEARCH(" ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(OPENINGGDN.OPENINGGDN_TYPENO, 0) AS TYPECHALLANNO, OPENINGGDN.OPENINGGDN_date AS CHALLANDATE, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN,ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(HASTELEDGERS.ACC_CMPNAME,'') AS HASTE, ISNULL(AGENTMASTER.Acc_cmpname, '') AS AGENT, ISNULL(OPENINGGDN.OPENINGGDN_MULTISONO, '') AS MULTISONO,ISNULL(OPENINGGDN.OPENINGGDN_PONO, '') AS PARTYPONO, OPENINGGDN.OPENINGGDN_SODATE AS SODATE, ISNULL(OPENINGGDN.OPENINGGDN_SONO, 0) AS SONO, ISNULL(OPENINGGDN.OPENINGGDN_SOGRIDSRNO, 0) AS SOGRIDSRNO,OPENINGGDN.OPENINGGDN_SODATE AS SODATE, ISNULL(TRANSLEDGER.Acc_cmpname, '') AS TRANS, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(OPENINGGDN.OPENINGGDN_LRNO, '') AS LRNO, OPENINGGDN.OPENINGGDN_LRDATE AS LRDATE, ISNULL(OPENINGGDN.OPENINGGDN_TRANSREFNO, '') AS TRANSREFF, ISNULL(OPENINGGDN.OPENINGGDN_TRANSREMARKS, '') AS TRANSREMARKS, ISNULL(OPENINGGDN.OPENINGGDN_TOTALBALES, 0) AS TOTALBALES, ISNULL(OPENINGGDN.OPENINGGDN_TOTALPCS, 0) AS TOTALPCS, ISNULL(OPENINGGDN.OPENINGGDN_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(OPENINGGDN.OPENINGGDN_TOTALAMT, 0) AS TOTALAMT, ISNULL(OPENINGGDN_DESC.OPENINGGDN_SRNO, 0) AS SRNO, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO, ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0) AS PCS, ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0) AS MTRS, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_PER, '') AS PER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_AMOUNT, 0) AS AMOUNT, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BARCODE, '') AS BARCODE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_FROMNO, 0) AS FROMNO, ISNULL(OPENINGGDN_DESC.OPENINGGDN_FROMSRNO, 0) AS FROMSRNO, ISNULL(OPENINGGDN_DESC.OPENINGGDN_FROMTYPE, '') AS FROMTYPE, ISNULL(OPENINGGDN.OPENINGGDN_BALENOFROM, 0) AS BALENOFROM, ISNULL(OPENINGGDN.OPENINGGDN_BALENOTO, 0) AS BALENOTO, ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS PACKING,ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR), '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(OPENINGGDN.OPENINGGDN_REMARKS,'') AS REMARKS, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO,'') AS GRIDPARTYPONO  ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON OPENINGGDN.OPENINGGDN_DISPATCHTOID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON OPENINGGDN_DESC.OPENINGGDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN LEDGERS AS HASTELEDGERS ON OPENINGGDN.OPENINGGDN_JOBBERID = HASTELEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id ", "  and OPENINGGDN.OPENINGGDN_NO='" & DTTABLE.Rows(i).Item("GDNNO") & "'  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " ORDER BY OPENINGGDN_DESC.OPENINGGDN_SRNO")

                    Else
                        DT = objclspreq.SEARCH(" ISNULL(YARNCHALLAN.YARN_NO, 0) AS YARNCHALLANNO, 0 AS TYPECHALLANNO, YARNCHALLAN.YARN_date AS CHALLANDATE, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN,ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, '' AS HASTE, '' AS AGENT, ISNULL(YARNCHALLAN.YARN_SONO, '') AS MULTISONO, '' AS PARTYPONO, YARNCHALLAN.YARN_SODATE AS SODATE, ISNULL(YARNCHALLAN.YARN_SONO, 0) AS SONO, 0 AS SOGRIDSRNO,YARNCHALLAN.YARN_SODATE AS SODATE, ISNULL(TRANSLEDGER.Acc_cmpname, '') AS TRANS, '' AS CITY, '' AS LRNO, YARNCHALLAN.YARN_DATE AS LRDATE, ISNULL(YARNCHALLAN.YARN_REFNO, '') AS TRANSREFF, '' AS TRANSREMARKS, 0 AS TOTALBALES, ISNULL(YARNCHALLAN.YARN_TOTALQTY, 0) AS TOTALPCS, ISNULL(YARNCHALLAN.YARN_TOTALWT, 0) AS TOTALMTRS, 0 AS TOTALAMT, ISNULL(YARNCHALLAN_DESC.YARN_GRIDSRNO, 0) AS SRNO, 'FRESH' AS PIECETYPE, ISNULL(YARNQUALITYMASTER.YARN_NAME,'') AS ITEM, '' AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS PRINTDESC, '' AS BALENO, ISNULL(YARNCHALLAN_DESC.YARN_QTY, 0) AS PCS, ISNULL(YARNCHALLAN_DESC.YARN_WT, 0) AS MTRS, 0 AS RATE, 'Mtrs' AS PER, 0 AS AMOUNT, '' AS BARCODE, ISNULL(YARNCHALLAN_DESC.YARN_NO, 0) AS FROMNO, ISNULL(YARNCHALLAN_DESC.YARN_GRIDSRNO, 0) AS FROMSRNO, 'YARN' AS FROMTYPE, 0 AS BALENOFROM, 0 AS BALENOTO, ISNULL(LEDGERS.Acc_cmpname, '') AS PACKING,ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR), '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(YARNCHALLAN.YARN_REMARKS,'') AS REMARKS, '' AS GRIDPARTYPONO ", "", " YARNCHALLAN INNER JOIN LEDGERS ON YARNCHALLAN.YARN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN YARNCHALLAN_DESC ON YARNCHALLAN.YARN_NO = YARNCHALLAN_DESC.YARN_NO AND YARNCHALLAN.YARN_YEARID = YARNCHALLAN_DESC.YARN_YEARID LEFT OUTER JOIN DESIGNMASTER ON YARNCHALLAN_DESC.YARN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON YARNCHALLAN_DESC.YARN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN YARNQUALITYMASTER ON YARNCHALLAN_DESC.YARN_YARNQUALITYID = YARNQUALITYMASTER.YARN_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON YARNCHALLAN.YARN_TRANSLEDGERID = TRANSLEDGER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON YARNCHALLAN.YARN_GODOWNID = GODOWNMASTER.GODOWN_id ", " AND YARNCHALLAN.YARN_NO='" & DTTABLE.Rows(i).Item("GDNNO") & "' AND YARNCHALLAN.YARN_YEARID = " & YearId & " ORDER BY YARNCHALLAN_DESC.YARN_GRIDSRNO")
                    End If

                    If DT.Rows.Count > 0 Then
                        For Each dr As DataRow In DT.Rows

                            TXTSTATECODE.Text = dr("STATECODE")
                            TXTGSTIN.Text = dr("GSTIN")
                            cmbname.Text = dr("NAME")
                            If cmbname.Text.Trim <> "" And (ClientName = "YASHVI" Or ClientName = "KCRAYON") Then cmbname_Validated(sender, e)
                            CMBAGENT.Text = dr("AGENT")
                            CMBHASTE.Text = dr("HASTE")
                            If dr("PACKING") <> "" Then CMBPACKING.Text = dr("PACKING")
                            cmbtrans.Text = dr("TRANS")
                            LRDATE.Text = Format(Convert.ToDateTime(dr("LRDATE")), "dd/MM/yyyy")
                            txtlrno.Text = dr("LRNO")

                            If ClientName = "SAFFRONOFF" Then
                                txtrefno.Text = dr("TYPECHALLANNO")
                            ElseIf ClientName <> "GELATO" Then
                                txtrefno.Text = DTTABLE.Rows(0).Item("CHALLANNO")
                            End If

                            If ClientName <> "AVIS" And ClientName <> "ANMOL" Then
                                If dr("MULTISONO") <> "" Then TXTMULTISONO.Text = dr("MULTISONO") Else TXTMULTISONO.Text = dr("SONO")
                            End If



                            txtpartypono.Text = dr("TRANSREMARKS")
                            sodate.Text = Format(Convert.ToDateTime(dr("SODATE")), "dd/MM/yyyy")
                            If ClientName <> "RMANILAL" And ClientName <> "YASHVI" And ClientName <> "TCOT" And ClientName <> "SUPEEMA" And ClientName <> "MANSI" And ClientName <> "SIMPLEX" And ClientName <> "ANMOL" And ClientName <> "VALIANT" And ClientName <> "SARAYU" And ClientName <> "JAGDAMBA" Then TXTBALENOFROM.Text = Val(dr("BALENOFROM"))


                            TXTBALENOTO.Text = Val(dr("BALENOTO"))
                            CHALLANDATE.Text = Convert.ToDateTime(dr("CHALLANDATE")).Date
                            If ClientName = "SBA" Or ClientName = "SOFTAS" Then INVOICEDATE.Text = Convert.ToDateTime(dr("CHALLANDATE")).Date
                            If dr("CITY") <> "" Then CMBTOCITY.Text = dr("CITY")
                            If ClientName = "AVIS" Then CMBTOCITY.Text = dr("CITY")
                            If ClientName <> "TCOT" And ClientName <> "REALCORPORATION" Then cmbname.Enabled = False
                            If ClientName = "ALENCOT" Or ClientName = "SOFTAS" Or ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "KEMLINO" Or ClientName = "KCRAYON" Or ClientName = "VSTRADERS" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "SIDDHGIRI" Or ClientName = "SNCM" Then txtremarks.Text = dr("REMARKS")
                        Next
                    End If
                Next




                'FETCH DISCOUNT WITH RESPECT TO SHIPTO LEDGERS
                If ClientName = "KOTHARI" And EDIT = False Then

                    GRIDCHGS.RowCount = 0

                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(LEDGERS.ACC_EXMILLLESS,0) AS EXMILLLESS,  ISNULL(LEDGERS.ACC_DISC,0) AS DISCPER,  ISNULL(LEDGERS.ACC_CDPER,0) AS CDPER, ISNULL(LEDGERS.ACC_AGENTCOMM,'') AS AGENTCOMM, ISNULL(LEDGERS.ACC_RD,0) AS RATEDIFF", "", " LEDGERS ", " and LEDGERS.acc_cmpname = '" & CMBPACKING.Text.Trim & "' and LEDGERS.acc_YEARid = " & YearId)
                    If DT.Rows.Count > 0 Then

                        'IN CHARGES GRID ADD DISCOUNT GIVEN / BROKERAGE
                        'If (ClientName = "YASHVI" Or ClientName = "SBA" Or ClientName = "DEVEN" Or ClientName = "SOFTAS" Or ClientName = "BARKHA" Or ClientName = "AVIS" Or ClientName = "MOMAI" Or ClientName = "SHREEVALLABH") Then
                        'INITIALLY IT WAS WITH RESPECT TO THE ABOVE MENTIONED CLIENT, THEN CHANGED WITH RESPECT TO SALEAUTODISCOUNT
                        If SALEAUTODISCOUNT = True And CMBSCREENTYPE.Text <> "LINE GST" And EDIT = False Then
                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "RATE DIFFERENCE" Then GoTo LINE1
                            Next
                            If Val(DT.Rows(0).Item("RATEDIFF")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "RATE DIFFERENCE", Val(DT.Rows(0).Item("RATEDIFF")) * -1, 0, 0)

                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "EXMILL LESS" Then GoTo LINE1
                            Next
                            If Val(DT.Rows(0).Item("EXMILLLESS")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "EXMILL LESS", Val(DT.Rows(0).Item("EXMILLLESS")) * -1, 0, 0)

                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "DISCOUNT GIVEN" Then GoTo LINE1
                            Next
                            If Val(DT.Rows(0).Item("DISCPER")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "DISCOUNT GIVEN", Val(DT.Rows(0).Item("DISCPER")) * -1, 0, 0)


                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "CASH DISCOUNT" Then GoTo LINE1
                            Next
                            If Val(DT.Rows(0).Item("CDPER")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "CASH DISCOUNT", Val(DT.Rows(0).Item("CDPER")) * -1, 0, 0)

                            'INITIALLY IT WAS WITH RESPECT TO THE ABOVE MENTIONED CLIENT, THEN CHANGED WITH RESPECT TO AUTOBROKERAGE
                            If AUTOBROKERAGE = True Then
                                For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                    If DTROW.Cells(ECHARGES.Index).Value = "BROKERAGE" Then GoTo LINE1
                                Next
                                If Val(DT.Rows(0).Item("AGENTCOMM")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "BROKERAGE", Val(DT.Rows(0).Item("AGENTCOMM")) * -1, 0, 0)
                            End If
                        End If

LINE1:
                    End If
                End If



                'FETCH DISCOUNT WITH RESPECT TO SALE ORDER IN SNCM
                If ClientName = "SNCM" And EDIT = False Then

                    GRIDCHGS.RowCount = 0
                    Dim SONO As Integer
                    For i = 0 To DTTABLE.Rows.Count - 1
                        Dim objclspreq As New ClsCommon()
                        Dim DT2 As New DataTable
                        DT2 = objclspreq.SEARCH("ISNULL(GDN.GDN_SONO, 0) AS SONO", "", "   GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON GDN.GDN_DISPATCHTOID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN LEDGERS AS HASTELEDGERS ON GDN.GDN_JOBBERID = HASTELEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id  ", "  and GDN.GDN_NO='" & DTTABLE.Rows(i).Item("GDNNO") & "'  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " ORDER BY GDN_DESC.GDN_SRNO")
                        If DT2.Rows.Count > 0 Then
                            If SONO = 0 Then
                                SONO = DT2.Rows(0).Item("SONO")
                            Else
                                SONO = SONO + "," + DT2.Rows(0).Item("SONO")
                            End If
                        End If
                    Next

                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(SO_DISCDEALER, 0) AS DISCPER, ISNULL(SO_CD, 0) AS CDPER, ISNULL(SO_DISCRATE,0) AS RATEDIFF, ISNULL(SO_INT,0) AS AGENTCOMM", "", " SALEORDER ", " and SALEORDER.SO_NO IN (" & SONO & ") and SALEORDER.SO_YEARid = " & YearId)

                    If DT.Rows.Count > 0 Then

                        'IN CHARGES GRID ADD DISCOUNT GIVEN / BROKERAGE
                        'If (ClientName = "YASHVI" Or ClientName = "SBA" Or ClientName = "DEVEN" Or ClientName = "SOFTAS" Or ClientName = "BARKHA" Or ClientName = "AVIS" Or ClientName = "MOMAI" Or ClientName = "SHREEVALLABH") Then
                        'INITIALLY IT WAS WITH RESPECT TO THE ABOVE MENTIONED CLIENT, THEN CHANGED WITH RESPECT TO SALEAUTODISCOUNT
                        If SALEAUTODISCOUNT = True And CMBSCREENTYPE.Text <> "LINE GST" And EDIT = False Then
                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "RATE DIFFERENCE" Then GoTo LINE2
                            Next
                            If Val(DT.Rows(0).Item("RATEDIFF")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "RATE DIFFERENCE", Val(DT.Rows(0).Item("RATEDIFF")) * -1, 0, 0)

                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "DISCOUNT GIVEN" Then GoTo LINE2
                            Next
                            If Val(DT.Rows(0).Item("DISCPER")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "DISCOUNT GIVEN", Val(DT.Rows(0).Item("DISCPER")) * -1, 0, 0)


                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "CASH DISCOUNT" Then GoTo LINE2
                            Next
                            If Val(DT.Rows(0).Item("CDPER")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "CASH DISCOUNT", Val(DT.Rows(0).Item("CDPER")) * -1, 0, 0)

                            'INITIALLY IT WAS WITH RESPECT TO THE ABOVE MENTIONED CLIENT, THEN CHANGED WITH RESPECT TO AUTOBROKERAGE
                            If AUTOBROKERAGE = True Then
                                For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                    If DTROW.Cells(ECHARGES.Index).Value = "BROKERAGE" Then GoTo LINE2
                                Next
                                If Val(DT.Rows(0).Item("AGENTCOMM")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "BROKERAGE", Val(DT.Rows(0).Item("AGENTCOMM")) * -1, 0, 0)
                            End If
                        End If

LINE2:
                    End If
                End If





                If ClientName = "AVIS" Or ClientName = "ANMOL" Then
                    Dim DTSO As New DataTable
                    If DTTABLE.Rows(0).Item("FROMTYPE") = "GDN" Then
                        If ClientName = "AVIS" Then DTSO = OBJCMN.SEARCH("DISTINCT CAST(GDN_SODETAILS.GDN_FROMNO AS INT) AS SONO", "", " GDN_SODETAILS", " AND GDN_YEARID = " & YearId & " AND GDN_NO IN (SELECT CAST(GP_BALENO AS INT) FROM SALEGATEPASS_DESC WHERE GP_YEARID = " & YearId & " AND GP_NO IN (" & TXTGATEPASSNO.Text.Trim & "))") Else DTSO = OBJCMN.SEARCH("DISTINCT CAST(GDN_SODETAILS.GDN_FROMNO AS INT) AS SONO", "", " GDN_SODETAILS", " AND GDN_YEARID = " & YearId & " AND GDN_NO IN (" & txtchallan.Text.Trim & ")")
                    ElseIf DTTABLE.Rows(0).Item("FROMTYPE") = "OPENINGGDN" Then
                        DTSO = OBJCMN.SEARCH("DISTINCT OPENINGGDN.OPENINGGDN_MULTISONO AS SONO", "", " OPENINGGDN", " AND OPENINGGDN_YEARID = " & YearId & " AND OPENINGGDN_NO IN (" & txtchallan.Text.Trim & ")")
                    End If
                    For Each SOROW As DataRow In DTSO.Rows
                        If TXTMULTISONO.Text.Trim = "" Then TXTMULTISONO.Text = Val(SOROW("SONO")) Else TXTMULTISONO.Text = TXTMULTISONO.Text & "," & Val(SOROW("SONO"))
                    Next
                End If

                Dim DT1 As New DataTable


                'THIS IS WRITTEN FOR OUR REFERENCE
                'WHILE MAKING CHANGES IN THIS CODE, DONT FORGET TO MAKE THE CHANGES IN ELSE PART ALSO
                'DONT FORGET TO MAKE CHANGES IN ELSE PART ALSO
                'CHECK ELSE PART ALSO

                ''  GETTING DISTINCT FROMTYPE IN DATATABLE
                ' WE HAVE ADDED THIS LOOP COZ WE WANT TO ADD BOTH OPENING AND GDN IN SAME INVOICE
                Dim DVFROMTYPE As DataView = DTTABLE.DefaultView
                Dim DTFROMTYPE As DataTable = DVFROMTYPE.ToTable(True, "FROMTYPE")
                For Each DRFROMTYPE As DataRow In DTFROMTYPE.Rows


                    If DRFROMTYPE("FROMTYPE") = "GDN" Then
                        If ClientName = "SVS" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT  ISNULL(GDN.GDN_NO, 0) As GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER, ISNULL(ITEMMASTER.ITEM_RATE, 0) AS RATE, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO ,ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON QUALITY_COUNT = HSN_CODE AND QUALITY_YEARID = HSN_YEARID", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, '') , ISNULL(GDN_DESC.GDN_PRINTDESC, ''), ISNULL(GDN_DESC.GDN_BALENO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(ITEMMASTER.ITEM_RATE,0), ISNULL(GDN_BALENOFROM,0), ISNULL(GDN_BALENOTO,0)  ")
                        ElseIf ClientName = "MAHAVIR" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE, ISNULL(GDN_DESC.GDN_SRNO,0) AS GDNSRNO, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO,ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_CMPID = GDN_DESC.GDN_CMPID AND GDN.GDN_LOCATIONID = GDN_DESC.GDN_LOCATIONID AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, '') , ISNULL(GDN_DESC.GDN_PRINTDESC, ''), ISNULL(GDN_DESC.GDN_BALENO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(ITEMMASTER.ITEM_RATE,0), ISNULL(GDN_BALENOFROM,0), ISNULL(GDN_BALENOTO,0), ISNULL(GDN_SRNO,0)  ")
                        ElseIf ClientName = "AXIS" Or ClientName = "SUCCESS" Or ClientName = "MANIBHADRA" Or ClientName = "DRDRAPES" Or ClientName = "KARAN" Or ClientName = "VINAYAK" Or ClientName = "POOJA" Or ClientName = "DEVEN" Or ClientName = "KEMLINO" Or ClientName = "MSANCHITKUMAR" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, '' AS DESIGN, '' AS COLOR, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE, 0) AS RATE, 0 AS GDNSRNO, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO,ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''),ISNULL(GDN_DESC.GDN_PRINTDESC, ''), ISNULL(GDN_DESC.GDN_BALENO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(GDN_DESC.GDN_RATE, 0)")
                            'ONLY ITEMNAME
                        ElseIf ClientName = "NAYRA" Or ClientName = "PARTOBA" Or ClientName = "KUNAL" Or ClientName = "VINIT" Or ClientName = "SARAYU" Or ClientName = "SIDDHPOLYCOT" Or ClientName = "MNARESH" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, '' AS DESIGN, '' AS COLOR, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE, 0) AS RATE, 0 AS GDNSRNO, '' AS BALENO, '' AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(GDN_DESC.GDN_RATE, 0)")
                        ElseIf ClientName = "KCRAYON" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGN, '' AS COLOR, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, 0 AS RATE, 0 AS GDNSRNO, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO,ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO,''), ISNULL(GDN_DESC.GDN_PRINTDESC, ''), ISNULL(GDN_DESC.GDN_BALENO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0)")

                            'ITEM | DESIGN | BALENO
                        ElseIf ClientName = "YASHVI" Or ClientName = "DETLINE" Or ClientName = "SBA" Or ClientName = "SOFTAS" Or ClientName = "BRILLANTO" Or ClientName = "MYCOT" Or ClientName = "SUPEEMA" Or ClientName = "MAFATLAL" Or ClientName = "REALCORPORATION" Then
                            DT1 = OBJCMN.SEARCH(" ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, ISNULL(DESIGN_NO,'') AS DESIGN, '' AS COLOR, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE, 0) AS RATE, 0 AS GDNSRNO, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO,ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(ITEMMASTER.item_name, ''), ISNULL(DESIGN_NO,''), ISNULL(GDN_DESC.GDN_PRINTDESC, ''), ISNULL(GDN_DESC.GDN_RATE, 0), ISNULL(GDN_DESC.GDN_BALENO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(GDN.GDN_NO, 0) ORDER BY ISNULL(ITEMMASTER.item_name, '')")

                            'ITEMNAME AND BALENO
                        ElseIf ClientName = "CHINTAN" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, '' AS DESIGN, '' AS COLOR, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE, 0) AS RATE, 0 AS GDNSRNO, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO, '' AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(GDN_DESC.GDN_BALENO, ''), ISNULL(GDN_DESC.GDN_RATE, 0)")

                            'THIS IS FOR UNIT WISE ENTRY FROM CHALLAN
                        ElseIf ClientName = "VALIANT" Then
                            DT1 = OBJCMN.SEARCH("  ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, '' AS DESIGN, '' AS COLOR, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE, 0) AS RATE, 0 AS GDNSRNO, '' AS BALENO,ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, '' AS GRIDPARTYPONO, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID LEFT OUTER JOIN UNITMASTER ON GDN_DESC.GDN_UNITID = UNITMASTER.UNIT_ID", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''),ISNULL(GDN_DESC.GDN_PRINTDESC, ''),  ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(GDN_DESC.GDN_RATE, 0), ISNULL(UNITMASTER.UNIT_ABBR,'')")


                            'THIS IS FOR SINGLE SINGLE ENTRY
                        ElseIf ClientName = "CC" Or ClientName = "SNCM" Or ClientName = "AFW" Or ClientName = "MAHAJAN" Then
                            DT1 = OBJCMN.SEARCH(" ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO, ISNULL(GDN_DESC.GDN_PCS, 0) AS PCS, ISNULL(GDN_DESC.GDN_MTRS, 0) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE, 0) AS RATE, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", " and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " ORDER BY GDN_DESC.GDN_SRNO")

                            'THIS IS FOR SINGLE SINGLE ENTRY WITHOUT COLOR
                            'ElseIf ClientName = "SNCM" Then '(NOW THEY WANT WITH COLOR)
                            '    DT1 = OBJCMN.SEARCH(" ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, '' AS COLOR, ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO, ISNULL(GDN_DESC.GDN_PCS, 0) AS PCS, ISNULL(GDN_DESC.GDN_MTRS, 0) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE,0) AS RATE, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " ORDER BY GDN_DESC.GDN_SRNO ")

                        ElseIf ClientName = "AVIS" Or ClientName = "MOMAI" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO, ISNULL(GDN_DESC.GDN_CUT, 0) AS CUT, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE, 0) AS RATE, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_CMPID = GDN_DESC.GDN_CMPID AND GDN.GDN_LOCATIONID = GDN_DESC.GDN_LOCATIONID AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, '') , ISNULL(GDN_DESC.GDN_PRINTDESC, ''), ISNULL(GDN_DESC.GDN_BALENO, ''), ISNULL(GDN_DESC.GDN_CUT, 0), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(GDN_DESC.GDN_RATE, 0), ISNULL(GDN_BALENOFROM,0), ISNULL(GDN_BALENOTO,0), ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '')   ")
                        ElseIf ClientName = "SUPRIYA" Or ClientName = "KREEVE" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO, 0 AS CUT, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE, 0) AS RATE, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_CMPID = GDN_DESC.GDN_CMPID AND GDN.GDN_LOCATIONID = GDN_DESC.GDN_LOCATIONID AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, '') , ISNULL(GDN_DESC.GDN_PRINTDESC, ''), ISNULL(GDN_DESC.GDN_BALENO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(GDN_DESC.GDN_RATE, 0), ISNULL(GDN_BALENOFROM,0), ISNULL(GDN_BALENOTO,0), ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '')   ")
                        ElseIf ClientName = "SANGHVI" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, '' AS BALENO, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, ''), ISNULL(GDN_DESC.GDN_PRINTDESC, ''), ISNULL(GDN_DESC.GDN_BALENO, ''),ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(ITEMMASTER.ITEM_RATE,0), ISNULL(GDN_BALENOFROM,0), ISNULL(GDN_BALENOTO,0), ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '')    ")
                        ElseIf ClientName = "PARAS" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "TINUMINU" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, '' AS DESIGN, '' AS COLOR, ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE,0) AS RATE, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''),  ISNULL(GDN_DESC.GDN_PRINTDESC, ''), ISNULL(GDN_DESC.GDN_BALENO, ''),ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(ITEMMASTER.ITEM_RATE,0), ISNULL(GDN_BALENOFROM,0), ISNULL(GDN_BALENOTO,0), ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, ''), ISNULL(GDN_DESC.GDN_RATE,0) ")

                            'WITHOUT BALENO
                        ElseIf ClientName = "NTC" Or ClientName = "KRFABRICS" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, '' AS COLOR, '' AS PRINTDESC, '' AS BALENO, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE,0) AS RATE, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(GDN_DESC.GDN_RATE,0), ISNULL(GDN_BALENOFROM,0), ISNULL(GDN_BALENOTO,0), ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '')    ")

                            'WITHOUT BALENO, WITH ITEM | DESIGN | SHADE
                        ElseIf ClientName = "C3" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, '' AS PRINTDESC, '' AS BALENO, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE,0) AS RATE, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(GDN_DESC.GDN_RATE,0), ISNULL(GDN_BALENOFROM,0), ISNULL(GDN_BALENOTO,0), ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '')    ")

                            'WITHOUT BALE AND GET TRANSREFNO IN PRINTDESC
                        ElseIf ClientName = "AMAN" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, '' AS COLOR, ISNULL(GDN.GDN_TRANSREFNO, '') AS PRINTDESC, '' AS BALENO, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE,0) AS RATE, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(GDN_DESC.GDN_RATE,0), ISNULL(GDN_BALENOFROM,0), ISNULL(GDN_BALENOTO,0), ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, ''), ISNULL(GDN.GDN_TRANSREFNO, '')    ")
                        Else
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(GDN.GDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, '' AS COLOR,ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS PRINTDESC, ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO, SUM(ISNULL(GDN_DESC.GDN_PCS, 0)) AS PCS, SUM(ISNULL(GDN_DESC.GDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(GDN_DESC.GDN_RATE,0) AS RATE, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON GDN.GDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON GDN.GDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON GDN.GDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and GDN.GDN_NO IN(" & txtchallan.Text.Trim & ")  and GDN_DESC.GDN_SALEDONE=0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY ISNULL(GDN.GDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(GDN_DESC.GDN_PRINTDESC, ''), ISNULL(GDN_DESC.GDN_BALENO, ''),ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(GDN_DESC.GDN_RATE,0), ISNULL(GDN_BALENOFROM,0), ISNULL(GDN_BALENOTO,0), ISNULL(GDN_DESC.GDN_GRIDPARTYPONO, '')    ")
                        End If

                    ElseIf DRFROMTYPE("FROMTYPE") = "OPENINGGDN" Then

                        If ClientName = "SVS" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT  ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER, ISNULL(ITEMMASTER.ITEM_RATE, 0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO ,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON OPENINGGDN_DESC.OPENINGGDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON QUALITY_COUNT = HSN_CODE AND QUALITY_YEARID = HSN_YEARID", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, '') , ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(ITEMMASTER.ITEM_RATE,0), ISNULL(OPENINGGDN_BALENOFROM,0), ISNULL(OPENINGGDN_BALENOTO,0)  ")
                        ElseIf ClientName = "MAHAVIR" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_SRNO,0) AS OPENINGGDNSRNO, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_CMPID = OPENINGGDN_DESC.OPENINGGDN_CMPID AND OPENINGGDN.OPENINGGDN_LOCATIONID = OPENINGGDN_DESC.OPENINGGDN_LOCATIONID AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON OPENINGGDN_DESC.OPENINGGDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, '') , ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(ITEMMASTER.ITEM_RATE,0), ISNULL(OPENINGGDN_BALENOFROM,0), ISNULL(OPENINGGDN_BALENOTO,0), ISNULL(OPENINGGDN_SRNO,0)  ")
                        ElseIf ClientName = "AXIS" Or ClientName = "SUCCESS" Or ClientName = "MANIBHADRA" Or ClientName = "DRDRAPES" Or ClientName = "KARAN" Or ClientName = "VINAYAK" Or ClientName = "SUPEEMA" Or ClientName = "POOJA" Or ClientName = "DEVEN" Or ClientName = "KEMLINO" Or ClientName = "MSANCHITKUMAR" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, '' AS DESIGN, '' AS COLOR, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0) AS RATE, 0 AS OPENINGGDNSRNO, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''),ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0)")
                            'ONLY ITEMNAME
                        ElseIf ClientName = "NAYRA" Or ClientName = "PARTOBA" Or ClientName = "KUNAL" Or ClientName = "VINIT" Or ClientName = "SARAYU" Or ClientName = "SIDDHPOLYCOT" Or ClientName = "MNARESH" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, '' AS DESIGN, '' AS COLOR, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0) AS RATE, 0 AS OPENINGGDNSRNO, '' AS BALENO, '' AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''),ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0)")
                        ElseIf ClientName = "KCRAYON" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGN, '' AS COLOR, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, 0 AS RATE, 0 AS OPENINGGDNSRNO, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO,''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0)")

                            'ITEM | DESIGN | BALENO
                        ElseIf ClientName = "YASHVI" Or ClientName = "DETLINE" Or ClientName = "SBA" Or ClientName = "SOFTAS" Or ClientName = "BRILLANTO" Or ClientName = "MYCOT" Or ClientName = "SUPEEMA" Or ClientName = "MAFATLAL" Or ClientName = "REALCORPORATION" Then
                            DT1 = OBJCMN.SEARCH(" ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, ISNULL(DESIGN_NO,'') AS DESIGN, '' AS COLOR, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0) AS RATE, 0 AS OPENINGGDNSRNO, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(ITEMMASTER.item_name, ''), ISNULL(DESIGN_NO,''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) ORDER BY ISNULL(ITEMMASTER.item_name, '')")

                            'ITEMNAME AND BALENO
                        ElseIf ClientName = "CHINTAN" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, '' AS DESIGN, '' AS COLOR, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0) AS RATE, 0 AS OPENINGGDNSRNO, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO, '' AS PRINTDESC, '' AS GRIDPARTYPONO, '' AS UNIT ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''),ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0)")


                        ElseIf ClientName = "VALIANT" Then
                            DT1 = OBJCMN.SEARCH("  ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, '' AS DESIGN, '' AS COLOR, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0) AS RATE, 0 AS OPENINGGDNSRNO, '' AS BALENO,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, '' AS GRIDPARTYPONO, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID LEFT OUTER JOIN UNITMASTER ON OPENINGGDN_DESC.OPENINGGDN_UNITID = UNITMASTER.UNIT_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''),ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''),  ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0), ISNULL(UNITMASTER.UNIT_ABBR,'')")

                            'THIS IS FOR SINGLE SINGLE ENTRY
                        ElseIf ClientName = "CC" Or ClientName = "SNCM" Or ClientName = "AFW" Then
                            DT1 = OBJCMN.SEARCH(" ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO, ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0) AS PCS, ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON OPENINGGDN_DESC.OPENINGGDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", " and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " ORDER BY OPENINGGDN_DESC.OPENINGGDN_SRNO")

                            'THIS IS FOR SINGLE SINGLE ENTRY WITHOUT COLOR
                            'ElseIf ClientName = "SNCM" Then
                            '    DT1 = OBJCMN.SEARCH(" ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, '' AS COLOR, ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO, ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0) AS PCS, ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE,0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " ORDER BY OPENINGGDN_DESC.OPENINGGDN_SRNO")

                        ElseIf ClientName = "AVIS" Or ClientName = "MOMAI" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO, ISNULL(OPENINGGDN_DESC.OPENINGGDN_CUT, 0) AS CUT, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_CMPID = OPENINGGDN_DESC.OPENINGGDN_CMPID AND OPENINGGDN.OPENINGGDN_LOCATIONID = OPENINGGDN_DESC.OPENINGGDN_LOCATIONID AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON OPENINGGDN_DESC.OPENINGGDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, '') , ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_CUT, 0), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0), ISNULL(OPENINGGDN_BALENOFROM,0), ISNULL(OPENINGGDN_BALENOTO,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '')   ")
                        ElseIf ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "SUPRIYA" Or ClientName = "KREEVE" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO, 0 AS CUT, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_CMPID = OPENINGGDN_DESC.OPENINGGDN_CMPID AND OPENINGGDN.OPENINGGDN_LOCATIONID = OPENINGGDN_DESC.OPENINGGDN_LOCATIONID AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON OPENINGGDN_DESC.OPENINGGDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, '') , ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE, 0), ISNULL(OPENINGGDN_BALENOFROM,0), ISNULL(OPENINGGDN_BALENOTO,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '')   ")
                        ElseIf ClientName = "SANGHVI" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, '' AS BALENO, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN COLORMASTER ON OPENINGGDN_DESC.OPENINGGDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''),ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(ITEMMASTER.ITEM_RATE,0), ISNULL(OPENINGGDN_BALENOFROM,0), ISNULL(OPENINGGDN_BALENOTO,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '')    ")
                        ElseIf ClientName = "PARAS" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "TINUMINU" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, '' AS DESIGN, '' AS COLOR, ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE,0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''),  ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''),ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(ITEMMASTER.ITEM_RATE,0), ISNULL(OPENINGGDN_BALENOFROM,0), ISNULL(OPENINGGDN_BALENOTO,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE,0) ")

                            'WITHOUT BALENO
                        ElseIf ClientName = "NTC" Or ClientName = "KRFABRICS" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, '' AS COLOR,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE,0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''),ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE,0), ISNULL(OPENINGGDN_BALENOFROM,0), ISNULL(OPENINGGDN_BALENOTO,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '')    ")

                            'WITHOUT BALENO, WITH ITEM | DESIGN | SHADE
                        ElseIf ClientName = "C3" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, '' AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, '' AS PRINTDESC, '' AS BALENO, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE,0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON OPENINGGDN_DESC.OPENINGGDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE,0), ISNULL(OPENINGGDN_BALENOFROM,0), ISNULL(OPENINGGDN_BALENOTO,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '')    ")

                            'WITHOUT BALE AND GET TRANSREFNO IN PRINTDESC
                        ElseIf ClientName = "AMAN" Then
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, '' AS COLOR, ISNULL(OPENINGGDN.OPENINGGDN_TRANSREFNO, '') AS PRINTDESC, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE,0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''),ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE,0), ISNULL(OPENINGGDN_BALENOFROM,0), ISNULL(OPENINGGDN_BALENOTO,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, ''), ISNULL(OPENINGGDN.OPENINGGDN_TRANSREFNO, '')  ")
                        Else
                            DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(OPENINGGDN.OPENINGGDN_NO, 0) AS GDNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, '' AS COLOR,ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, '') AS PRINTDESC, ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, '') AS BALENO, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_PCS, 0)) AS PCS, SUM(ISNULL(OPENINGGDN_DESC.OPENINGGDN_MTRS, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE,0) AS RATE, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '') AS GRIDPARTYPONO, '' AS UNIT  ", "", " OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN OPENINGGDN_DESC ON OPENINGGDN.OPENINGGDN_NO = OPENINGGDN_DESC.OPENINGGDN_NO AND OPENINGGDN.OPENINGGDN_YEARID = OPENINGGDN_DESC.OPENINGGDN_YEARID LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON OPENINGGDN_DESC.OPENINGGDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN CITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = CITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON OPENINGGDN.OPENINGGDN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & txtchallan.Text.Trim & ")  and OPENINGGDN_DESC.OPENINGGDN_SALEDONE=0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY ISNULL(OPENINGGDN.OPENINGGDN_NO, 0), ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_PRINTDESC, ''), ISNULL(OPENINGGDN_DESC.OPENINGGDN_BALENO, ''),ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_RATE,0), ISNULL(OPENINGGDN_BALENOFROM,0), ISNULL(OPENINGGDN_BALENOTO,0), ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO, '')    ")
                        End If

                    Else
                        'FETCH DATA FROM YARNCHALLAN
                        DT1 = OBJCMN.SEARCH(" DISTINCT ISNULL(YARNCHALLAN.YARN_NO, 0) AS GDNNO, ISNULL(YARNQUALITYMASTER.YARN_NAME,'') AS ITEM, '' AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, '' AS COLOR,ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS PRINTDESC, '' AS BALENO, SUM(ISNULL(YARNCHALLAN_DESC.YARN_QTY, 0)) AS PCS, SUM(ISNULL(YARNCHALLAN_DESC.YARN_WT, 0)) AS MTRS , ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, 0 AS RATE, '' AS GRIDPARTYPONO, '' AS UNIT  ", "", " YARNCHALLAN INNER JOIN LEDGERS ON YARNCHALLAN.YARN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN YARNCHALLAN_DESC ON YARNCHALLAN.YARN_NO = YARNCHALLAN_DESC.YARN_NO AND YARNCHALLAN.YARN_YEARID = YARNCHALLAN_DESC.YARN_YEARID LEFT OUTER JOIN DESIGNMASTER ON YARNCHALLAN_DESC.YARN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN YARNQUALITYMASTER ON YARNCHALLAN_DESC.YARN_YARNQUALITYID = YARNQUALITYMASTER.YARN_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGER ON YARNCHALLAN.YARN_TRANSLEDGERID = TRANSLEDGER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON YARNCHALLAN.YARN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN HSNMASTER ON YARN_HSNCODEID = HSN_ID ", "  and YARNCHALLAN.YARN_NO IN(" & txtchallan.Text.Trim & ")  and YARNCHALLAN.YARN_YEARID = " & YearId & " GROUP BY ISNULL(YARNCHALLAN.YARN_NO, 0), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(YARNQUALITYMASTER.YARN_NAME, ''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0)  ")
                    End If

                    If DT1.Rows.Count > 0 Then

                        'CHECKING WHETHER SAME CHALLAN IS ALREADY PRESENT IN GRID OR NOT
                        For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                            If Val(ROW.Cells(GFROMNO.Index).Value) > 0 And Val(ROW.Cells(GFROMNO.Index).Value) = Val(DT1.Rows(0).Item("GDNNO")) Then GoTo SKIPLINE
                        Next


                        For Each dr As DataRow In DT1.Rows


                            'ADD YARNQUALITY IN ITEMMASTER IF NOT PRESENT
                            'CODE DONE BY GULKIT
                            If DRFROMTYPE("FROMTYPE") = "YARN" Then
                                Dim DTITEM = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", " ITEMMASTER ", " AND ITEM_NAME = '" & dr("ITEM") & "' AND ITEM_YEARID = " & YearId)
                                If DTITEM.Rows.Count = 0 Then

                                    'ADD NEW ITEMNAME 
                                    Dim ALPARAVAL As New ArrayList
                                    ALPARAVAL.Clear()


                                    ALPARAVAL.Add("Finished Goods")

                                    Dim DTCATEGORY As DataTable = OBJCMN.SEARCH("ISNULL(CATEGORY_NAME, '') AS CATEGORY", "", " CATEGORYMASTER INNER JOIN YARNQUALITYMASTER ON YARNQUALITYMASTER.YARN_CATEGORYID = CATEGORYMASTER.CATEGORY_ID", " AND YARNQUALITYMASTER.YARN_NAME = '" & dr("ITEM") & "' AND CATEGORY_YEARID = " & YearId)
                                    If DTCATEGORY.Rows.Count > 0 Then ALPARAVAL.Add(DTCATEGORY.Rows(0).Item("CATEGORY")) Else ALPARAVAL.Add("") 'CATEGORY

                                    ALPARAVAL.Add(UCase(dr("ITEM")))        'DISPLAYNAME
                                    ALPARAVAL.Add(UCase(dr("ITEM")))        'ITEMNAME

                                    ALPARAVAL.Add("")   'DEPARTMENT
                                    ALPARAVAL.Add(UCase(dr("ITEM")))        'CODE
                                    ALPARAVAL.Add("")   'UNIT
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

                                    Dim DTHSN As DataTable = OBJCMN.SEARCH("ISNULL(HSN_CODE, '') AS HSNCODE", "", " HSNMASTER INNER JOIN YARNQUALITYMASTER ON YARNQUALITYMASTER.YARN_HSNCODEID = HSNMASTER.HSN_ID", " AND YARNQUALITYMASTER.YARN_NAME = '" & dr("ITEM") & "' AND HSN_YEARID = " & YearId)
                                    If DTHSN.Rows.Count > 0 Then ALPARAVAL.Add(DTHSN.Rows(0).Item("HSNCODE")) Else ALPARAVAL.Add("") 'HSNCODE

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

                                    ALPARAVAL.Add("")   'COLORSRNO
                                    ALPARAVAL.Add("")   'COLOR
                                    ALPARAVAL.Add(0)   'VALUELOSSPER
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



                            Dim BALENO As String = dr("BALENO")
                            If ClientName = "ARIHANT" Then BALENO = Val(txtchallan.Text.Trim) & "*" & Val(TXTBALENOFROM.Text)
                            If ClientName = "SUPRIYA" Or ClientName = "BARKHA" Then BALENO = txtchallan.Text.Trim
                            If ClientName = "KRISHNA" Or ClientName = "AMAN" Or ClientName = "SIMPLEX" Or ClientName = "SURYODAYA" Or ClientName = "VALIANT" Or ClientName = "SARAYU" Or ClientName = "JAGDAMBA" Or (ClientName = "SUPEEMA" And dr("BALENO") = "") Then BALENO = dr("GDNNO")
                            Dim PER As String = "Mtrs"
                            Dim INVRATE As Double = 0
                            Dim CUT As Double = 0
                            Dim GDNSRNO As Integer = 0
                            Dim DISCPER As Double = Val(TXTDISCPER.Text.Trim)

                            If ClientName = "MAHAVIR" Then GDNSRNO = Val(dr("GDNSRNO"))
                            If ClientName = "AVIS" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then CUT = Val(dr("CUT"))
                            If ClientName = "PURPLE" Or ClientName = "GELATO" Or ClientName = "MAHAVIR" Or ClientName = "MOMAI" Then PER = "Pcs"

                            'getting per/RATE from itemmaster
                            If ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "AXIS" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "SMS" Or ClientName = "DEVEN" Or ClientName = "KREEVE" Or ClientName = "NAYRA" Or ClientName = "MVIKASKUMAR" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "KDFAB" Or ClientName = "KUNAL" Or ClientName = "SIDDHGIRI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "CHINTAN" Or ClientName = "MAFATLAL" Or ClientName = "SUPEEMA" Then
                                Dim DTPER As DataTable = OBJCMN.SEARCH("ISNULL(UNIT_ABBR,'Mtrs') AS PER, ISNULL(ITEM_REORDER,0) AS CUT, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE, ISNULL(ITEMMASTER.ITEM_HIDEINDESIGN,0) AS NETTRATEKDFAB, ISNULL(ITEMMASTER.ITEM_REMARKS,'') AS REMARKS ", "", " ITEMMASTER LEFT OUTER JOIN UNITMASTER ON item_unitid = UNIT_ID ", " AND ITEMMASTER.ITEM_NAME = '" & dr("ITEM") & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                                If DTPER.Rows.Count > 0 Then
                                    If LCase(DTPER.Rows(0).Item("PER")) = "pcs" Then PER = "Pcs"
                                    If LCase(DTPER.Rows(0).Item("PER")) = "yards" Then PER = "Yards"
                                    If (ClientName = "DEVEN" Or ClientName = "NAYRA") And DTPER.Rows(0).Item("PER") = "Kgs" Then PER = "Kgs"
                                    If ClientName = "SMS" And Val(DTPER.Rows(0).Item("CUT")) > 0 Then CUT = Val(DTPER.Rows(0).Item("CUT"))
                                    If ClientName = "KDFAB" And DTPER.Rows(0).Item("NETTRATEKDFAB") = True Then DISCPER = 0
                                    If ClientName = "KREEVE" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "KUNAL" Or ClientName = "CHINTAN" Then INVRATE = Val(DTPER.Rows(0).Item("RATE"))
                                    If ClientName = "KUNAL" Then dr("PRINTDESC") = DTPER.Rows(0).Item("REMARKS")
                                End If
                            End If


                            If FETCHRATEFROMCHALLAN = True Then  'ClientName = "CC" Or ClientName = "KREEVE" Or ClientName = "PURVITEX" Or ClientName = "YASHVI" Or ClientName = "KOTHARI" Or ClientName = "SBA" Or ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Or ClientName = "SOFTAS" Or ClientName = "MJFABRIC" Or ClientName = "AVIS" Or ClientName = "PARAS" Or ClientName = "MOMAI" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "SHREEVALLABH" Or ClientName = "KEMLINO" Or ClientName = "MOOLTEX" Or ClientName = "SUPRIYA" Or ClientName = "MYCOT" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI"  Or ClientName = "SMS" Or ClientName = "GELATO" Or ClientName = "KRISHNA" Or ClientName = "VINAYAK" Then
                                'WE NEED TO FETCH RATES FROM CHALLAN
                                If Val(dr("RATE")) > 0 Then INVRATE = Val(dr("RATE"))

                                'GIVE 35% LESS FIXED
                                If ClientName = "MOMAI" Then INVRATE = Format(INVRATE - (INVRATE * 0.35), "0")
                                If ClientName = "CC" Or ClientName = "C3" Or ClientName = "KREEVE" Then PER = "Pcs"

                            ElseIf ClientName = "DEVEN" Or ClientName = "KEMLINO" Or ClientName = "RMANILAL" Or ClientName = "VALIANT" Then

                                Dim DTPRICELIST As DataTable = OBJCMN.SEARCH(" ISNULL(LEDGERS.ACC_PRICELISTCOLUMN, '') AS COLNAME", "", " LEDGERS ", " AND ledgers.acc_cmpname = '" & cmbname.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                                If DTPRICELIST.Rows.Count > 0 AndAlso DTPRICELIST.Rows(0).Item("COLNAME") <> "" Then
                                    Dim DTRATE As DataTable = OBJCMN.SEARCH(DTPRICELIST.Rows(0).Item("COLNAME") & " AS RATE", "", "ITEMPRICELIST INNER JOIN ITEMMASTER ON ITEMPRICELIST.ITEMID = ITEMMASTER.item_id ", " AND ITEMMASTER.ITEM_NAME = '" & dr("ITEM") & "' AND ITEM_YEARID = " & YearId)
                                    If DTRATE.Rows.Count > 0 AndAlso Val(TXTRATE.Text.Trim) = 0 Then INVRATE = Val(DTRATE.Rows(0).Item("RATE"))
                                End If

                            ElseIf ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "KCRAYON" Then
                                Dim WHERECLAUSE As String = ""
                                If ClientName = "KCRAYON" AndAlso cmbname.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '" & cmbname.Text.Trim & "'"
                                If (ClientName = "SANGHVI" Or ClientName = "TINUMINU") AndAlso dr("QUALITY") <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(QUALITYMASTER.QUALITY_NAME,'') = '" & dr("QUALITY") & "'"
                                If dr("DESIGN") <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(DESIGNMASTER.DESIGN_NO,'') = '" & dr("DESIGN") & "'"
                                If (ClientName = "SANGHVI" Or ClientName = "TINUMINU") AndAlso dr("COLOR") <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(COLORMASTER.COLOR_NAME,'') = '" & dr("COLOR") & "'"
                                Dim DTRATE As DataTable = OBJCMN.SEARCH("PRICELISTMASTER.PL_RATE AS SALERATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR ", "", "PRICELISTMASTER INNER JOIN ITEMMASTER ON PRICELISTMASTER.PL_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON PRICELISTMASTER.PL_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON PRICELISTMASTER.PL_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON PRICELISTMASTER.PL_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN LEDGERS ON PRICELISTMASTER.PL_LEDGERID = LEDGERS.ACC_ID ", " AND ISNULL(ITEMMASTER.ITEM_NAME,'') = '" & dr("ITEM") & "'" & WHERECLAUSE & " AND PL_YEARID = " & YearId)
                                If DTRATE.Rows.Count > 0 Then INVRATE = DTRATE.Rows(0).Item("SALERATE")
                            ElseIf ClientName = "KDFAB" Or ClientName = "DRDRAPES" Or ClientName = "RADHA" Or ClientName = "KARAN" Then
                                Dim DTRATE As New DataTable
                                If ClientName = "DRDRAPES" Or ClientName = "RADHA" Or ClientName = "KCRAYON" Or ClientName = "KARAN" Then
                                    DTRATE = OBJCMN.SEARCH("PRICELISTMASTER.PL_RATE AS SALERATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME ", "", "PRICELISTMASTER INNER JOIN ITEMMASTER ON PRICELISTMASTER.PL_ITEMID = ITEMMASTER.item_id INNER JOIN LEDGERS ON PRICELISTMASTER.PL_LEDGERID = LEDGERS.ACC_ID ", " AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '" & cmbname.Text.Trim & "' AND ISNULL(ITEMMASTER.ITEM_NAME,'') = '" & dr("ITEM") & "' AND PL_YEARID = " & YearId)
                                Else
                                    DTRATE = OBJCMN.SEARCH("PRICELISTMASTER.PL_RATE AS SALERATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME ", "", "PRICELISTMASTER INNER JOIN ITEMMASTER ON PRICELISTMASTER.PL_ITEMID = ITEMMASTER.item_id  ", " AND ISNULL(ITEMMASTER.ITEM_NAME,'') = '" & dr("ITEM") & "' AND PL_YEARID = " & YearId)
                                End If
                                If DTRATE.Rows.Count > 0 Then INVRATE = DTRATE.Rows(0).Item("SALERATE")
                            End If

                            'FOR EXPORT
                            If ClientName = "AVIS" And CHKOVERSEAS.Checked = True Then
                                CUT = INVRATE
                                INVRATE = 0
                            End If

                            If ClientName = "GELATO" And Val(txtrefno.Text.Trim) > 0 Then DISCPER = Val(txtrefno.Text.Trim)
                            If ClientName = "YASHVI" Then
                                Gmtrs.ReadOnly = True
                                TXTMTRS.ReadOnly = True
                            End If


                            If dr("ITEM").ToString <> "" And Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then

                                Dim DTHSN As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER_DESC.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER_DESC.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER_DESC.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER_DESC.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN HSNMASTER_DESC ON HSNMASTER.HSN_ID = HSNMASTER_DESC.HSN_ID INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND HSNMASTER_DESC.HSN_WEFDATE <= '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND ITEMMASTER.ITEM_NAME= '" & dr("ITEM") & "' AND HSNMASTER.HSN_YEARID=" & YearId & " ORDER BY HSNMASTER_DESC.HSN_WEFDATE DESC")
                                Dim CGSTPER, SGSTPER, IGSTPER As Double

                                CGSTPER = Val(DTHSN.Rows(0).Item("CGSTPER"))
                                SGSTPER = Val(DTHSN.Rows(0).Item("SGSTPER"))
                                IGSTPER = Val(DTHSN.Rows(0).Item("IGSTPER"))

                                If CHKEXPORTGST.Checked = True Then
                                    CGSTPER = Val(DTHSN.Rows(0).Item("EXPCGSTPER"))
                                    SGSTPER = Val(DTHSN.Rows(0).Item("EXPSGSTPER"))
                                    IGSTPER = Val(DTHSN.Rows(0).Item("EXPIGSTPER"))
                                End If

                                If CHKREVERSECALC.Checked = True Then INVRATE = Format((INVRATE / (Val(IGSTPER) + 100)) * 100, "0.00")


                                If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                                    GRIDINVOICE.Rows.Add(0, dr("ITEM"), DTHSN.Rows(0).Item("HSNCODE"), dr("QUALITY"), dr("DESIGN"), dr("COLOR"), 0, 0, dr("PRINTDESC"), BALENO, Format(Val(dr("PCS")), "0.00"), Format(Val(CUT), "0.00"), Format(Val(dr("MTRS")), "0.00"), INVRATE, PER, "0.00", "", "", Val(DISCPER), 0, Val(TXTSPDISCPER.Text.Trim), 0, "0.00", "0.00", Val(CGSTPER), "0.00", Val(SGSTPER), "0.00", "0.00", "0.00", "0.00", "", dr("GDNNO"), GDNSRNO, DRFROMTYPE("FROMTYPE"), 0, dr("GRIDPARTYPONO"), dr("UNIT"), 0, 0, 0, "", "")
                                    TXTCGSTPER1.Text = Val(CGSTPER)
                                    TXTSGSTPER1.Text = Val(SGSTPER)
                                    TXTIGSTPER1.Text = 0
                                Else
                                    GRIDINVOICE.Rows.Add(0, dr("ITEM"), DTHSN.Rows(0).Item("HSNCODE"), dr("QUALITY"), dr("DESIGN"), dr("COLOR"), 0, 0, dr("PRINTDESC"), BALENO, Format(Val(dr("PCS")), "0.00"), Format(Val(CUT), "0.00"), Format(Val(dr("MTRS")), "0.00"), INVRATE, PER, "0.00", "", "", Val(DISCPER), 0, Val(TXTSPDISCPER.Text.Trim), 0, "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", Val(IGSTPER), "0.00", "0.00", "", dr("GDNNO"), GDNSRNO, DRFROMTYPE("FROMTYPE"), 0, dr("GRIDPARTYPONO"), dr("UNIT"), 0, 0, 0, "", "")
                                    TXTCGSTPER1.Text = 0
                                    TXTSGSTPER1.Text = 0
                                    TXTIGSTPER1.Text = Val(IGSTPER)
                                End If

                            Else
                                GRIDINVOICE.Rows.Add(0, dr("ITEM"), " ", dr("QUALITY"), dr("DESIGN"), dr("COLOR"), 0, 0, dr("PRINTDESC"), BALENO, Format(Val(dr("PCS")), "0.00"), "0.00", Format(Val(dr("MTRS")), "0.00"), INVRATE, PER, "0.00", "", "", Val(DISCPER), 0, 0, 0, "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "", dr("GDNNO"), GDNSRNO, DRFROMTYPE("FROMTYPE"), 0, dr("GRIDPARTYPONO"), dr("UNIT"), 0, 0, 0, "", "")
                            End If
                        Next


SKIPLINE:
                    End If


                Next





                GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1
                getsrno(GRIDINVOICE)
                If GRIDINVOICE.RowCount > 0 Then
                    GRIDINVOICE.Focus()
                    GRIDINVOICE.CurrentCell = GRIDINVOICE.Rows(0).Cells(GRATE.Index)
                End If
                CHKBARCODE.Enabled = False
            End If
            ' End If
            TOTAL()
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

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'SALE'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.SEARCH(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'SALE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If
            getmax_INVOICE_no()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If cmbregister.Text.Trim.Length > 0 And EDIT = False Then
                If DIRECTINVOICE = False Then CLEAR()
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.SEARCH(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    saleregabbr = dt.Rows(0).Item(0).ToString
                    salereginitial = dt.Rows(0).Item(1).ToString
                    saleregid = dt.Rows(0).Item(2)
                    getmax_INVOICE_no()
                    cmbregister.Enabled = False
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDINVOICE.CurrentRow.Index >= 0 And GRIDINVOICE.Item(GSRNO.Index, GRIDINVOICE.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDINVOICE.Item(GSRNO.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                CMBITEM.Text = GRIDINVOICE.Item(GITEMNAME.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTHSNCODE.Text = GRIDINVOICE.Item(GHSNCODE.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDINVOICE.Item(GQUALITY.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDINVOICE.Item(GDESIGN.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                CMBSHADE.Text = GRIDINVOICE.Item(GSHADE.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTQTY.Text = GRIDINVOICE.Item(GQTY.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTFOLDPER.Text = GRIDINVOICE.Item(GFOLDPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTDESCRIPTION.Text = GRIDINVOICE.Item(GDESCRIPTION.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTBALENO.Text = GRIDINVOICE.Item(GBALENO.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTPCS.Text = GRIDINVOICE.Item(Gpcs.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTCUT.Text = GRIDINVOICE.Item(GCUT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString

                TXTMTRS.Text = GRIDINVOICE.Item(Gmtrs.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTRATE.Text = GRIDINVOICE.Item(GRATE.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                CMBPER.Text = GRIDINVOICE.Item(GPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTAMT.Text = GRIDINVOICE.Item(GAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTGRIDLRNO.Text = GRIDINVOICE.Item(GLRNO.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                CMBGRIDTRANS.Text = GRIDINVOICE.Item(GTRANS.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTDISCPER.Text = GRIDINVOICE.Item(GDISCPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTDISCAMT.Text = GRIDINVOICE.Item(GDISCAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTSPDISCPER.Text = GRIDINVOICE.Item(GSPDISCPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTSPDISCAMT.Text = GRIDINVOICE.Item(GSPDISCAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTOTHERAMT.Text = GRIDINVOICE.Item(GOTHERAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTTAXABLEAMT.Text = GRIDINVOICE.Item(GTAXABLEAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTCGSTPER.Text = GRIDINVOICE.Item(GCGSTPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTCGSTAMT.Text = GRIDINVOICE.Item(GCGSTAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTSGSTPER.Text = GRIDINVOICE.Item(GSGSTPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTSGSTAMT.Text = GRIDINVOICE.Item(GSGSTAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTIGSTPER.Text = GRIDINVOICE.Item(GIGSTPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTIGSTAMT.Text = GRIDINVOICE.Item(GIGSTAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString


                TXTGRIDTOTAL.Text = GRIDINVOICE.Item(GGRIDTOTAL.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString

                txtdescbarcode.Text = GRIDINVOICE.Item(GBARCODE.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDINVOICE.CurrentRow.Index
                TXTSRNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDINVOICE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDINVOICE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDINVOICE.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbMERCHANT.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                If ClientName = "YASHVI" And txtchallan.Text.Trim <> "" Then
                    MessageBox.Show("Row Fetched From Challan, You Cannot Delete This Row")
                    Exit Sub
                End If

                GRIDINVOICE.Rows.RemoveAt(GRIDINVOICE.CurrentRow.Index)
                TOTAL()
                getsrno(GRIDINVOICE)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            ElseIf e.KeyCode = Keys.F12 And GRIDINVOICE.RowCount > 0 Then
                If GRIDINVOICE.CurrentRow.Cells(GITEMNAME.Index).Value <> "" Then GRIDINVOICE.Rows.Add(CloneWithValues(GRIDINVOICE.CurrentRow))
            ElseIf e.KeyCode = Keys.F11 And GRIDINVOICE.RowCount > 0 Then
                If GRIDINVOICE.CurrentRow.Cells(GITEMNAME.Index).Value <> "" And GRIDINVOICE.CurrentRow.Index > 0 Then
                    GRIDINVOICE.Item(GPARTYPONO.Index, GRIDINVOICE.CurrentRow.Index).Value = GRIDINVOICE.Item(GPARTYPONO.Index, GRIDINVOICE.CurrentRow.Index - 1).Value
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Public Function CloneWithValues(ByVal row As DataGridViewRow) As DataGridViewRow
        CloneWithValues = CType(row.Clone(), DataGridViewRow)
        For index As Int32 = 0 To row.Cells.Count - 1
            CloneWithValues.Cells(index).Value = row.Cells(index).Value
        Next
    End Function

    Private Sub CNNOTE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CNNOTE.Click
        Try
            If Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then
                MsgBox("Credit Note Cannot be Raised for This Invoice, Create Sale Return", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If PBRECD.Visible = True Then
                MsgBox("Rec made, Delete Rec First", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If lbllocked.Visible = True Or PBlock.Visible = True Then
                MsgBox("Booking Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If EDIT = True Then
                Dim TEMPMSG As Integer = MsgBox("Wish to create Credit Note?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim OBJdN As New CREDITNOTE
                    OBJdN.MdiParent = MDIMain
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" REGISTER_INITIALS AS INITIALS", "", " REGISTERMASTER ", " AND REGISTER_NAME  = '" & cmbregister.Text.Trim & "' AND REGISTER_CMPID = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                    OBJdN.BILLNO = DT.Rows(0).Item("INITIALS") & "-" & Val(TXTINVOICENO.Text.Trim)
                    OBJdN.Show()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETKMS()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If cmbname.Text.Trim <> "" Then
                DT = OBJCMN.SEARCH("isnull(LEDGERS.ACC_KMS,0) AS KMS ", "", " LEDGERS ", " and LEDGERS.acc_cmpname = '" & cmbname.Text.Trim & "' And LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then

                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbname.Validated
        Try
            If cmbname.Text.Trim <> "" Then
                'GET REGISTER , AGENCT AND TRANS
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS_1.ACC_CMPNAME,'') AS TRANSNAME, ISNULL(LEDGERS_2.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(REGISTER_NAME,'') AS REGISTERNAME, ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN, ISNULL(LEDGERS.ACC_EXMILLLESS,0) AS EXMILLLESS,  ISNULL(LEDGERS.ACC_DISC,0) AS DISCPER,  ISNULL(LEDGERS.ACC_CDPER,0) AS CDPER, isnull(LEDGERS.ACC_CRDAYS,0) AS CRDAYS, ISNULL(LEDGERS.ACC_MOBILE,'') AS MOBILENO, ISNULL(TERMMASTER.TERM_NAME,'') AS TERM, ISNULL(LEDGERS.ACC_AGENTCOMM,'') AS AGENTCOMM, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME, ISNULL(LEDGERS.ACC_OVERSEAS,0) AS OVERSEAS, ISNULL(LEDGERS.ACC_TCS,0) AS TCS, ISNULL(LEDGERS.ACC_PARTYTDS,0) AS PARTYTDS, ISNULL(LEDGERS.ACC_WARNING,'') AS WARNINGTEXT, ISNULL(LEDGERS.ACC_RD,0) AS RATEDIFF, ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN TERMMASTER ON LEDGERS.ACC_TERMID = TERM_ID  LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_DELIVERYATID = CITY_ID ", " and LEDGERS.acc_cmpname = '" & cmbname.Text.Trim & "' and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then

                    If cmbtrans.Text.Trim = "" Then cmbtrans.Text = DT.Rows(0).Item("TRANSNAME")
                    If CMBTOCITY.Text.Trim = "" Then CMBTOCITY.Text = DT.Rows(0).Item("CITYNAME")
                    If CMBSALESMAN.Text.Trim = "" Then CMBSALESMAN.Text = DT.Rows(0).Item("SALESMAN")

                    If ClientName = "SBA" Then
                        CMBAGENT.Text = DT.Rows(0).Item("AGENTNAME")
                    Else
                        If CMBAGENT.Text.Trim = "" Then CMBAGENT.Text = DT.Rows(0).Item("AGENTNAME")
                    End If
                    TXTSTATECODE.Text = DT.Rows(0).Item("STATECODE")
                    TXTGSTIN.Text = DT.Rows(0).Item("GSTIN")
                    If CMBSCREENTYPE.Text = "LINE GST" Then
                        TXTDISCPER.Text = Val(DT.Rows(0).Item("DISCPER"))
                        TXTSPDISCPER.Text = Val(DT.Rows(0).Item("CDPER"))
                    End If

                    If DT.Rows(0).Item("WARNINGTEXT") <> "" Then MsgBox(DT.Rows(0).Item("WARNINGTEXT"), MsgBoxStyle.Critical)

                    If (ClientName <> "KOTHARI" And ClientName <> "KOTHARINEW") Then TXTMOBILENO.Text = DT.Rows(0).Item("MOBILENO")
                    CHKOVERSEAS.Checked = Convert.ToBoolean(DT.Rows(0).Item("OVERSEAS"))
                    CHKTCS.Checked = Convert.ToBoolean(DT.Rows(0).Item("TCS"))
                    CHKPARTYTDS.Checked = Convert.ToBoolean(DT.Rows(0).Item("PARTYTDS"))

                    If CHKOVERSEAS.Checked = True And ClientName = "AVIS" Then
                        GCUT.HeaderText = "USD"
                        GCUT.ReadOnly = False
                    Else
                        GCUT.HeaderText = "Cut"
                        GCUT.ReadOnly = True
                    End If


                    'IN CHARGES GRID ADD DISCOUNT GIVEN / BROKERAGE
                    'If (ClientName = "YASHVI" Or ClientName = "SBA" Or ClientName = "DEVEN" Or ClientName = "SOFTAS" Or ClientName = "BARKHA" Or ClientName = "AVIS" Or ClientName = "MOMAI" Or ClientName = "SHREEVALLABH") Then
                    'INITIALLY IT WAS WITH RESPECT TO THE ABOVE MENTIONED CLIENT, THEN CHANGED WITH RESPECT TO SALEAUTODISCOUNT
                    If SALEAUTODISCOUNT = True And CMBSCREENTYPE.Text <> "LINE GST" And EDIT = False Then
                        For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                            If DTROW.Cells(ECHARGES.Index).Value = "RATE DIFFERENCE" Then GoTo LINE1
                        Next
                        If Val(DT.Rows(0).Item("RATEDIFF")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "RATE DIFFERENCE", Val(DT.Rows(0).Item("RATEDIFF")) * -1, 0, 0)

                        For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                            If DTROW.Cells(ECHARGES.Index).Value = "EXMILL LESS" Then GoTo LINE1
                        Next
                        If Val(DT.Rows(0).Item("EXMILLLESS")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "EXMILL LESS", Val(DT.Rows(0).Item("EXMILLLESS")) * -1, 0, 0)

                        For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                            If DTROW.Cells(ECHARGES.Index).Value = "DISCOUNT GIVEN" Then GoTo LINE1
                        Next
                        If Val(DT.Rows(0).Item("DISCPER")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "DISCOUNT GIVEN", Val(DT.Rows(0).Item("DISCPER")) * -1, 0, 0)


                        For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                            If DTROW.Cells(ECHARGES.Index).Value = "CASH DISCOUNT" Then GoTo LINE1
                        Next
                        If Val(DT.Rows(0).Item("CDPER")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "CASH DISCOUNT", Val(DT.Rows(0).Item("CDPER")) * -1, 0, 0)


                        If ClientName = "AVIS" Then
                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "SPECIAL DISCOUNT" Then GoTo LINE1
                            Next
                            If Val(DT.Rows(0).Item("AGENTCOMM")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "SPECIAL DISCOUNT", Val(DT.Rows(0).Item("AGENTCOMM")) * -1, 0, 0)
                        End If


                        'If ClientName = "SBA" Or ClientName = "SOFTAS" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "MOOLTEX" Or ClientName = "SUPRIYA" Or ClientName = "SANGHVI" Or ClientName = "YASHVI" Or ClientName = "KRISHNA" Or ClientName = "SONU" Then
                        'INITIALLY IT WAS WITH RESPECT TO THE ABOVE MENTIONED CLIENT, THEN CHANGED WITH RESPECT TO AUTOBROKERAGE
                        If AUTOBROKERAGE = True Then
                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "BROKERAGE" Then GoTo LINE1
                            Next
                            If Val(DT.Rows(0).Item("AGENTCOMM")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "BROKERAGE", Val(DT.Rows(0).Item("AGENTCOMM")) * -1, 0, 0)
                        End If



                        'GET TRAVEL INSURANCE
                        Dim DTINSURANCE As DataTable = OBJCMN.SEARCH("ISNULL(TI_PERCENT,0) AS INSURANCE", "", "TRANSPORTINSURANCE", " AND TI_DATE <= '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND TI_YEARID = " & YearId & " ORDER BY TI_DATE DESC ")
                        If DTINSURANCE.Rows.Count > 0 Then
                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "INSURANCE CHARGES" Then GoTo LINE1
                            Next
                            If Val(DTINSURANCE.Rows(0).Item("INSURANCE")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "INSURANCE CHARGES", Val(DTINSURANCE.Rows(0).Item("INSURANCE")), 0, 0)
                        End If


                    End If

LINE1:


                    If ClientName = "KOTHARI" Then cmbname.Enabled = False

                    If CMBPACKING.Text.Trim = "" Then CMBPACKING.Text = cmbname.Text.Trim

                    If ClientName <> "NVAHAN" And ClientName <> "SAKARIA" Then
                        If Val(TXTCRDAYS.Text.Trim) = 0 Then
                            If Val(DT.Rows(0).Item("CRDAYS")) > 0 Then TXTCRDAYS.Text = Val(DT.Rows(0).Item("CRDAYS"))
                            If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then TXTCRDAYS.Text = 30
                            If Val(TXTCRDAYS.Text) > 0 And INVOICEDATE.Text <> "__/__/____" Then Call TXTCRDAYS_Validated(sender, e)
                        End If
                    End If

                    'GET PARTYTOTALINVOICE AMT
                    Dim DTINV As DataTable = OBJCMN.SEARCH("ISNULL(SUM(INVOICEMASTER.INVOICE_GRANDTOTAL),0) AS GRANDTOTAL", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                    If DTINV.Rows.Count > 0 Then TXTTOTALPARTYINVAMT.Text = Format(Val(DTINV.Rows(0).Item("GRANDTOTAL")), "0.00")


                    If (ClientName = "NVAHAN" Or ClientName = "SAKARIA") And EDIT = False Then CMBTERM.Text = DT.Rows(0).Item("TERM")

                    If DT.Rows(0).Item("REGISTERNAME") <> cmbregister.Text.Trim And DT.Rows(0).Item("REGISTERNAME") <> "" Then
                        Dim TEMPMSG As Integer = MsgBox("Register is Different Change to Default?", MsgBoxStyle.YesNo)
                        If TEMPMSG = vbYes Then
                            cmbregister.Text = DT.Rows(0).Item("REGISTERNAME")
                            getmax_BILL_no()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBHASTE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBHASTE.Enter
        Try
            If CMBHASTE.Text.Trim = "" Then FILLNAME(CMBHASTE, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            Throw ex
        End Try

        '    Cursor.Current = Cursors.WaitCursor
        '    If CMBHASTE.Text.Trim = "" Then
        '        Dim objclscommon As New ClsCommonMaster
        '        Dim dt As DataTable = objclscommon.search(" ADDRESS_alias ", "", "   ADDRESSMASTER INNER JOIN LEDGERS ON ADDRESSMASTER.ADDRESS_LEDGERID = LEDGERS.ACC_id AND ADDRESSMASTER.ADDRESS_cmpid = LEDGERS.ACC_cmpid AND ADDRESSMASTER.ADDRESS_locationid = LEDGERS.ACC_locationid AND ADDRESSMASTER.ADDRESS_yearid = LEDGERS.ACC_yearid  ", "  and  LEDGERS.ACC_cmpname ='" & cmbname.Text.Trim & "' and ADDRESS_cmpid=" & CmpId & " and ADDRESS_Locationid=" & Locationid & " and ADDRESS_Yearid=" & YearId)
        '        If dt.Rows.Count > 0 Then
        '            dt.DefaultView.Sort = "ADDRESS_alias"
        '            CMBHASTE.DataSource = dt
        '            CMBHASTE.DisplayMember = "ADDRESS_alias"
        '        End If
        '        CMBHASTE.SelectAll()
        '    End If
        'Catch ex As Exception
        '    If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        'Finally
        '    Cursor.Current = Cursors.Default
        'End Try
    End Sub

    Private Sub CMBHASTE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBHASTE.Validating
        Try
            NAMEVALIDATE(CMBHASTE, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
        '        Try
        '            Cursor.Current = Cursors.WaitCursor
        '            If CMBHASTE.Text.Trim <> "" Then
        '                pcase(CMBHASTE)
        '                Dim objclscommon As New ClsCommonMaster
        '                Dim dt As DataTable
        '                dt = objclscommon.search("ADDRESS_full", "", " ADDRESSMASTER INNER JOIN LEDGERS ON ADDRESSMASTER.ADDRESS_LEDGERID = LEDGERS.ACC_id AND ADDRESSMASTER.ADDRESS_cmpid = LEDGERS.ACC_cmpid AND ADDRESSMASTER.ADDRESS_locationid = LEDGERS.ACC_locationid AND ADDRESSMASTER.ADDRESS_yearid = LEDGERS.ACC_yearid  ", "  and  LEDGERS.ACC_cmpname ='" & cmbname.Text.Trim & "'  and ADDRESS_alias = '" & CMBHASTE.Text.Trim & "' and ADDRESS_cmpid = " & CmpId & " and ADDRESS_Locationid = " & Locationid & " and ADDRESS_Yearid = " & YearId)
        '                If dt.Rows.Count = 0 Then
        '                    Dim a As String = CMBHASTE.Text.Trim
        '                    Dim tempmsg As Integer = MsgBox("ADDRESS not present, Add New?", MsgBoxStyle.YesNo, "Textrade")
        '                    If tempmsg = vbYes Then

        '                        CMBHASTE.Text = a
        '                        Dim objADDRESSmaster As New addressMaster
        '                        objADDRESSmaster.txtname.Text = CMBHASTE.Text
        '                        objADDRESSmaster.cmbname.Text = cmbname.Text
        '                        objADDRESSmaster.cmbname.Enabled = False
        '                        objADDRESSmaster.ShowDialog()
        '                        dt = objclscommon.search("ADDRESS_alias", "", "ADDRESSMaster", " and ADDRESS_alias = '" & CMBHASTE.Text.Trim & "' and ADDRESS_cmpid = " & CmpId & " and ADDRESS_Locationid = " & Locationid & " and ADDRESS_Yearid = " & YearId)
        '                        If dt.Rows.Count > 0 Then
        '                            Dim dt1 As New DataTable
        '                            dt1 = CMBHASTE.DataSource
        '                            If CMBHASTE.DataSource <> Nothing Then
        'line1:
        '                                If dt1.Rows.Count > 0 Then
        '                                    dt1.Rows.Add(CMBHASTE.Text.Trim)
        '                                    CMBHASTE.Text = a
        '                                End If
        '                            End If
        '                        End If
        '                        e.Cancel = True
        '                    Else
        '                        e.Cancel = True
        '                    End If
        '                Else
        '                    txtDeliveryadd.Text = dt.Rows(0).Item(0).ToString
        '                End If
        '            End If
        '        Catch ex As Exception
        '            GoTo line1
        '            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        '        Finally
        '            Cursor.Current = Cursors.Default
        '        End Try
    End Sub

    Private Sub CMBAGENT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        If CMBAGENT.Text.Trim = "" Then fillledger(CMBAGENT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
    End Sub

    Private Sub CMBAGENT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBAGENT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBAGENT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then NAMEVALIDATE(CMBAGENT, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBFORMNO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBFORMNO.Validating
        Try
            If CMBFORMNO.Text.Trim <> "" Then FORMvalidate(CMBFORMNO, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTERM_ENTER(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTERM.Enter
        Try
            If CMBTERM.Text.Trim = "" Then fillTERM(CMBTERM, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTERM_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTERM.Validated
        Try
            If CMBTERM.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(TERM_CRDAYS, 0) AS CRDAYS, ISNULL(TERM_OTHERPER,0) AS OTHERPER", "", "TERMMASTER", " AND TERMMASTER.TERM_NAME='" & CMBTERM.Text.Trim & "' AND TERM_YEARID=" & YearId)
                If DT.Rows.Count > 0 Then
                    TXTCRDAYS.Text = DT.Rows(0).Item("CRDAYS")
                    If Val(DT.Rows(0).Item("CRDAYS")) > 0 Then TXTCRDAYS.Text = Val(DT.Rows(0).Item("CRDAYS"))
                    If Val(TXTCRDAYS.Text) > 0 And INVOICEDATE.Text <> "__/__/____" Then Call TXTCRDAYS_Validated(sender, e)

                    'FIRST CHECK WHETHER OTHERCHGS IS ALREADY ADDED IN GRIDCHGS OR NOT
                    'IF NOT THEN ADD
                    For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                        If DTROW.Cells(ECHARGES.Index).Value = "OTHER CHGS" Then Exit Sub
                    Next
                    If Val(DT.Rows(0).Item("OTHERPER")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "OTHER CHGS", Val(DT.Rows(0).Item("OTHERPER")), Format(Val(DT.Rows(0).Item("OTHERPER")) * Val(txtbillamt.Text.Trim) / 100, "0.00"), 0)
                    TOTAL()

                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTERM_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTERM.Validating
        Try
            If CMBTERM.Text.Trim <> "" Then TERMVALIDATE(CMBTERM, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub getmax_BILL_no()
        Dim DTTABLE As New DataTable
        If cmbregister.Text <> "" Then
            DTTABLE = getmax(" isnull(max(INVOICEMASTER_no),0) + 1 ", " INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID AND INVOICE_CMPID = REGISTER_CMPID AND INVOICE_LOCATIONID = REGISTER_LOCATIONID AND INVOICE_YEARID = REGISTER_YEARID ", " and REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER_cmpid=" & CmpId & " and INVOICEMASTER_locationid=" & Locationid & " and INVOICEMASTER_yearid=" & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTINVOICENO.Text = DTTABLE.Rows(0).Item(0)
        End If
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

    Private Sub CMBLOCALTRANSPORT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBLOCALTRANSPORT.Enter
        Try
            If CMBLOCALTRANSPORT.Text.Trim = "" Then FILLNAME(CMBLOCALTRANSPORT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBLOCALTRANSPORT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBLOCALTRANSPORT.Validating
        Try
            If CMBLOCALTRANSPORT.Text.Trim <> "" Then NAMEVALIDATE(CMBLOCALTRANSPORT, CMBCODE, e, Me, TXTTRANSADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSCITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBFROMCITY.Validating
        Try
            If CMBFROMCITY.Text.Trim <> "" Then CITYVALIDATE(CMBFROMCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTOCITY.Validating
        Try
            If CMBTOCITY.Text.Trim <> "" Then CITYVALIDATE(CMBTOCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEM_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBITEM.Enter
        Try
            If CMBITEM.Text.Trim = "" Then fillitemname(CMBITEM, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEM_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEM.Validating
        Try
            If CMBITEM.Text.Trim <> "" Then itemvalidate(CMBITEM, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, CMBITEM.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me, CMBITEM.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBSHADE.Enter
        Try
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE, CMBDESIGN.Text.Trim, CMBITEM.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSHADE.Validating
        Try
            If CMBSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBSHADE, e, Me, CMBDESIGN.Text.Trim, CMBITEM.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTPCS.KeyPress
        AMOUNTNUMDOTKYEPRESS(e, sender, Me)
    End Sub

    Private Sub TXTPCS_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTPCS.Validated, TXTMTRS.Validated, TXTRATE.Validated
        CALC()
        TOTAL()
    End Sub

    Private Sub TXTRATE_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress, TXTROE.KeyPress, TXTGROSSWT.KeyPress, TXTNETTWT.KeyPress, TXTSQMTRS.KeyPress, TXTCUT.KeyPress, TXTAMT.KeyPress, TXTSOADVANCE.KeyPress, TXTMATERIALVALUE.KeyPress, TXTQTY.KeyPress, TXTFOLDPER.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Sub CALC()
        Try
            If Val(TXTRATE.Text.Trim) > 0 Then TXTAMT.Text = 0.0
            TXTGRIDTOTAL.Text = 0.0
            If Val(TXTDISCPER.Text.Trim) > 0 Then TXTDISCAMT.Clear()
            If Val(TXTSPDISCPER.Text.Trim) > 0 Then TXTSPDISCAMT.Clear()
            If CHKMANUAL.CheckState = CheckState.Unchecked Then
                TXTCGSTAMT.Text = 0.0
                TXTSGSTAMT.Text = 0.0
                TXTIGSTAMT.Text = 0.0
            End If

            'TXTCGSTAMT1.Text = 0.0
            'TXTSGSTAMT1.Text = 0.0
            'TXTIGSTAMT1.Text = 0.0

            If ClientName = "AVIS" And CHKOVERSEAS.Checked = True And Val(TXTROE.Text.Trim) > 0 And Val(TXTCUT.Text.Trim) > 0 Then
                TXTRATE.Text = Format(Val(TXTROE.Text.Trim) * Val(TXTCUT.Text.Trim), "0.00")
            End If

            If Val(TXTRATE.Text.Trim) > 0 Then
                If CMBPER.Text = "Mtrs" Or CMBPER.Text = "Yards" Or CMBPER.Text = "Rolls" Then
                    TXTAMT.Text = Format(Val(TXTMTRS.Text) * Val(TXTRATE.Text), "0.00")
                Else
                    TXTAMT.Text = Format(Val(TXTPCS.Text) * Val(TXTRATE.Text), "0.00")
                End If
            End If
            If Val(TXTDISCPER.Text.Trim) > 0 And Val(TXTDISCAMT.Text.Trim) = 0 Then TXTDISCAMT.Text = Format(Val(TXTAMT.Text.Trim) * (Val(TXTDISCPER.Text.Trim) / 100), "0.00")
            If Val(TXTSPDISCPER.Text.Trim) > 0 And Val(TXTSPDISCAMT.Text.Trim) = 0 Then TXTSPDISCAMT.Text = Format((Val(TXTAMT.Text.Trim) - Val(TXTDISCAMT.Text.Trim)) * (Val(TXTSPDISCPER.Text.Trim) / 100), "0.00")
            TXTTAXABLEAMT.Text = Format((Val(TXTAMT.Text.Trim) - Val(TXTDISCAMT.Text.Trim) - Val(TXTSPDISCPER.Text.Trim) + Val(TXTOTHERAMT.Text.Trim)), "0.00")
            If CMBSCREENTYPE.Text = "LINE GST" And CHKMANUAL.CheckState = CheckState.Unchecked Then
                TXTCGSTAMT.Text = Format(Val(TXTCGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
                TXTSGSTAMT.Text = Format(Val(TXTSGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
                TXTIGSTAMT.Text = Format(Val(TXTIGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
            End If
            TXTGRIDTOTAL.Text = Format(Val(TXTTAXABLEAMT.Text) + Val(TXTCGSTAMT.Text) + Val(TXTSGSTAMT.Text) + Val(TXTIGSTAMT.Text), "0.00")
            If ClientName = "ABHEE" Then
                If Val(TXTQTY.Text.Trim) > 0 Then
                    TXTMTRS.Text = Format(Val(TXTQTY.Text.Trim) * (Val(TXTFOLDPER.Text.Trim) / 100), "0.00")
                End If
            End If

            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub calchgs()
        Try
            If Val(TXTCHGSPER.Text) <> 0 Then
                'before CALC CHECK HOW TO CALC CHARGES
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" (CASE WHEN ISNULL(ACC_CALC,'') = '' THEN 'GROSS' ELSE ACC_CALC END) AS CALC", "", "LEDGERS", " AND ACC_CMPNAME = '" & CMBCHARGES.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("CALC") = "GROSS" Then
                    TXTCHGSAMT.Text = Format((Val(txtbillamt.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
                ElseIf DT.Rows(0).Item("CALC") = "NETT" Then
                    'FIRST CALC NETT THEN ADD CHARGES ON THAT NETT TOTAL
                    TXTNETTAMT.Text = Val(txtbillamt.Text.Trim)
                    For Each ROW As DataGridViewRow In GRIDCHGS.Rows
                        If GRIDCHGSDOUBLECLICK = True And ROW.Index >= TEMPCHGSROW Then Exit For
                        TXTNETTAMT.Text = Format(Val(TXTNETTAMT.Text) + Val(ROW.Cells(EAMT.Index).Value), "0.00")
                    Next
                    TXTCHGSAMT.Text = Format((Val(TXTNETTAMT.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
                ElseIf DT.Rows(0).Item("CALC") = "QTY" Then
                    TXTCHGSAMT.Text = Format((Val(lbltotalpcs.Text) * Val(TXTCHGSPER.Text)), "0.00")
                ElseIf DT.Rows(0).Item("CALC") = "MTRS" Then
                    TXTCHGSAMT.Text = Format((Val(lbltotalmtrs.Text) * Val(TXTCHGSPER.Text)), "0.00")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPER.Validating
        Try
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTAMT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTAMT.Validating
        Try
            If CMBSCREENTYPE.Text = "TOTAL GST" Then
                CALC()
                If ClientName <> "ABHEE" Then Call TXTGRIDTOTAL_Validating(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        'If Val(TXTRATE.Text.Trim) = 0 Then
        '    If CMBPER.Text = "Pcs" Then
        '        TXTRATE.Text = Val(TXTAMT.Text) / Val(TXTPCS.Text)
        '    Else
        '        TXTRATE.Text = Val(TXTAMT.Text) / Val(TXTMTRS.Text)
        '    End If
        'End If

        'If CMBITEM.Text.Trim <> "" And Val(TXTMTRS.Text.Trim) <> 0 And CMBPER.Text.Trim <> "" And Val(TXTAMT.Text.Trim) <> 0 Then
        '    fillgrid()
        'Else
        '    If CMBITEM.Text.Trim = "" Then
        '        MsgBox("Please Fill Item Name ")
        '        CMBITEM.Focus()
        '        Exit Sub
        '    End If
        '    If TXTPCS.Text.Trim = "" Then
        '        MsgBox("Please Fill Pcs ")
        '        TXTPCS.Focus()
        '        Exit Sub
        '    End If
        '    If TXTMTRS.Text.Trim = "" Then
        '        MsgBox("Please Fill Mtrs ")
        '        TXTMTRS.Focus()
        '        Exit Sub
        '    End If
        '    If Val(TXTRATE.Text.Trim) <= 0 Then
        '        MsgBox("Please Fill Rate")
        '        TXTRATE.Focus()
        '        Exit Sub
        '    End If
        '    If CMBPER.Text.Trim = "" Then
        '        MsgBox("Please Fill Per ")
        '        CMBPER.Focus()
        '        Exit Sub
        '    End If
        'End If
    End Sub

    Sub fillgrid()

        If GRIDDOUBLECLICK = False Then
            GRIDINVOICE.Rows.Add(Val(TXTSRNO.Text.Trim), CMBITEM.Text.Trim, TXTHSNCODE.Text.Trim, CMBQUALITY.Text.Trim, CMBDESIGN.Text.Trim, CMBSHADE.Text.Trim, Format(Val(TXTQTY.Text.Trim), "0.00"), TXTFOLDPER.Text.Trim, TXTDESCRIPTION.Text.Trim, TXTBALENO.Text.Trim, Format(Val(TXTPCS.Text.Trim), "0.00"), Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Format(Val(TXTAMT.Text.Trim), "0.00"), TXTGRIDLRNO.Text.Trim, CMBGRIDTRANS.Text.Trim, Format(Val(TXTDISCPER.Text.Trim), "0.00"), Format(Val(TXTDISCAMT.Text.Trim), "0.00"), Format(Val(TXTSPDISCPER.Text.Trim), "0.00"), Format(Val(TXTSPDISCAMT.Text.Trim), "0.00"), Format(Val(TXTOTHERAMT.Text.Trim), "0.00"), Format(Val(TXTTAXABLEAMT.Text.Trim), "0.00"), Val(TXTCGSTPER.Text.Trim), Format(Val(TXTCGSTAMT.Text.Trim), "0.00"), Val(TXTSGSTPER.Text.Trim), Format(Val(TXTSGSTAMT.Text.Trim), "0.00"), Val(TXTIGSTPER.Text.Trim), Format(Val(TXTIGSTAMT.Text.Trim), "0.00"), Format(Val(TXTGRIDTOTAL.Text.Trim), "0.00"), txtdescbarcode.Text.Trim, 0, 0, "", 0, "", "Mtrs", 0, 0, 0, "", "")
            getsrno(GRIDINVOICE)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDINVOICE.Item(GSRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDINVOICE.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEM.Text.Trim
            GRIDINVOICE.Item(GHSNCODE.Index, TEMPROW).Value = TXTHSNCODE.Text.Trim
            GRIDINVOICE.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDINVOICE.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDINVOICE.Item(GSHADE.Index, TEMPROW).Value = CMBSHADE.Text.Trim
            GRIDINVOICE.Item(GQTY.Index, TEMPROW).Value = Format(Val(TXTQTY.Text.Trim), "0.00")
            GRIDINVOICE.Item(GFOLDPER.Index, TEMPROW).Value = TXTFOLDPER.Text.Trim
            GRIDINVOICE.Item(GDESCRIPTION.Index, TEMPROW).Value = TXTDESCRIPTION.Text.Trim
            GRIDINVOICE.Item(GBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim
            GRIDINVOICE.Item(Gpcs.Index, TEMPROW).Value = Format(Val(TXTPCS.Text.Trim), "0.00")
            GRIDINVOICE.Item(GCUT.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")

            GRIDINVOICE.Item(Gmtrs.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDINVOICE.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")
            GRIDINVOICE.Item(GPER.Index, TEMPROW).Value = CMBPER.Text.Trim
            GRIDINVOICE.Item(GAMT.Index, TEMPROW).Value = Format(Val(TXTAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GDISCPER.Index, TEMPROW).Value = Format(Val(TXTDISCPER.Text.Trim), "0.00")
            GRIDINVOICE.Item(GDISCAMT.Index, TEMPROW).Value = Format(Val(TXTDISCAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GSPDISCPER.Index, TEMPROW).Value = Format(Val(TXTSPDISCPER.Text.Trim), "0.00")
            GRIDINVOICE.Item(GSPDISCAMT.Index, TEMPROW).Value = Format(Val(TXTSPDISCAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GOTHERAMT.Index, TEMPROW).Value = Format(Val(TXTOTHERAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GTAXABLEAMT.Index, TEMPROW).Value = Format(Val(TXTTAXABLEAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GCGSTPER.Index, TEMPROW).Value = Val(TXTCGSTPER.Text.Trim)
            GRIDINVOICE.Item(GCGSTAMT.Index, TEMPROW).Value = Format(Val(TXTCGSTAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GSGSTPER.Index, TEMPROW).Value = Val(TXTSGSTPER.Text.Trim)
            GRIDINVOICE.Item(GSGSTAMT.Index, TEMPROW).Value = Format(Val(TXTSGSTAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GIGSTPER.Index, TEMPROW).Value = Val(TXTIGSTPER.Text.Trim)
            GRIDINVOICE.Item(GIGSTAMT.Index, TEMPROW).Value = Format(Val(TXTIGSTAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GGRIDTOTAL.Index, TEMPROW).Value = Format(Val(TXTGRIDTOTAL.Text.Trim), "0.00")
            GRIDINVOICE.Item(GLRNO.Index, TEMPROW).Value = TXTGRIDLRNO.Text.Trim
            GRIDINVOICE.Item(GTRANS.Index, TEMPROW).Value = CMBGRIDTRANS.Text.Trim


            GRIDINVOICE.Item(GBARCODE.Index, TEMPROW).Value = txtdescbarcode.Text.Trim

            GRIDDOUBLECLICK = False

        End If
        TOTAL()
        GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1

        TXTSRNO.Clear()
        If ClientName = "SONAL" Then CMBITEM.Text = ""
        TXTHSNCODE.Clear()
        TXTDESCRIPTION.Clear()
        TXTBALENO.Clear()
        TXTPCS.Clear()
        TXTCUT.Clear()

        TXTMTRS.Clear()
        TXTRATE.Clear()
        If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Or ClientName = "JITUBHAI" Or ClientName = "MAHAVIR" Or ClientName = "ARIHANTGARMENTS" Then
            CMBPER.Text = "Pcs"
        Else
            CMBPER.Text = "Mtrs"
        End If

        TXTAMT.Clear()
        TXTDISCPER.Clear()
        TXTDISCAMT.Clear()
        TXTSPDISCPER.Clear()
        TXTSPDISCAMT.Clear()


        TXTOTHERAMT.Clear()
        TXTTAXABLEAMT.Clear()
        TXTCGSTPER.Clear()
        TXTCGSTAMT.Clear()
        TXTSGSTPER.Clear()
        TXTSGSTAMT.Clear()
        TXTIGSTPER.Clear()
        TXTIGSTAMT.Clear()
        TXTGRIDTOTAL.Clear()
        txtdescbarcode.Clear()
        TXTQTY.Clear()
        TXTFOLDPER.Clear()
        TXTGRIDLRNO.Clear()
        CMBGRIDTRANS.Text = ""




        If GRIDINVOICE.RowCount > 0 Then
            TXTSRNO.Text = Val(GRIDINVOICE.Rows(GRIDINVOICE.RowCount - 1).Cells(0).Value) + 1
            ' TXTSRNO.Text = Val(GRIDINVOICE.RowCount) + 1
        Else
            TXTSRNO.Text = 1
        End If
        TXTSRNO.Focus()

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

    Private Sub CMBCHARGES_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Enter
        Try
            If ClientName = "AVIS" Then
                If CMBCHARGES.Text.Trim = "" Then FILLNAME(CMBCHARGES, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY ='Sales A/C')")
            Else
                If CMBCHARGES.Text.Trim = "" Then FILLNAME(CMBCHARGES, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Sales A/C' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses')")
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

    Private Sub CMBCHARGES_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCHARGES.Validating
        Try
            If ClientName = "AVIS" Then
                If CMBCHARGES.Text.Trim <> "" Then NAMEVALIDATE(CMBCHARGES, CMBCODE, e, Me, TXTTRANSADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'Sales A/C' )")
            Else
                If CMBCHARGES.Text.Trim <> "" Then NAMEVALIDATE(CMBCHARGES, CMBCODE, e, Me, TXTTRANSADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Sales A/C' )")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSAMT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHGSAMT.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, TXTCHGSAMT, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSAMT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHGSAMT.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" And Val(TXTCHGSAMT.Text.Trim) <> 0 Then
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(TXTCHGSAMT.Text.Trim, dDebit)
                If bValid Then
                    TXTCHGSAMT.Text = Convert.ToDecimal(Val(TXTCHGSAMT.Text))

                    'WHEN USER ENTERS DISCOUNT NAME IN CHARGES THEN ADD THIS DISCOUNT PERCENT IN THE GRID DISCOUNT ALSO
                    If (ClientName = "MASHOK" Or ClientName = "ABHEE") And CMBCHARGES.Text.Trim = "DISCOUNT" Then
                        For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                            ROW.Cells(GDISCPER.Index).Value = Val(TXTCHGSPER.Text.Trim) * -1
                        Next
                    End If

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
            GRIDCHGS.Rows.Add(Val(TXTCHGSSRNO.Text.Trim), CMBCHARGES.Text.Trim, Val(TXTCHGSPER.Text.Trim), Val(TXTCHGSAMT.Text.Trim), Val(TXTTAXID.Text.Trim))
            getsrno(GRIDCHGS)
        ElseIf GRIDCHGSDOUBLECLICK = True Then
            GRIDCHGS.Item(ESRNO.Index, TEMPCHGSROW).Value = Val(TXTCHGSSRNO.Text.Trim)
            GRIDCHGS.Item(ECHARGES.Index, TEMPCHGSROW).Value = CMBCHARGES.Text.Trim
            GRIDCHGS.Item(EPER.Index, TEMPCHGSROW).Value = Format(Val(TXTCHGSPER.Text.Trim), "0.00")
            GRIDCHGS.Item(EAMT.Index, TEMPCHGSROW).Value = Format(Val(TXTCHGSAMT.Text.Trim), "0.00")
            GRIDCHGS.Item(ETAXID.Index, TEMPCHGSROW).Value = Format(Val(TXTTAXID.Text.Trim))

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

    Private Sub txtuploadname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
        Try
            If txtimgpath.Text.Trim <> "" And txtuploadname.Text.Trim <> "" Then
                fillgridscan()
                txtuploadremarks.Clear()
                txtuploadname.Clear()
                txtimgpath.Clear()
                PBSoftCopy.ImageLocation = ""
                txtuploadsrno.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridupload_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
        Try
            If gridupload.Rows(e.RowIndex).Cells(GGRIDUPLOADSRNO.Index).Value <> Nothing Then
                GRIDUPLOADDOUBLECLICK = True
                TEMPUPLOADROW = e.RowIndex
                txtuploadsrno.Text = gridupload.Rows(e.RowIndex).Cells(GGRIDUPLOADSRNO.Index).Value
                txtuploadremarks.Text = gridupload.Rows(e.RowIndex).Cells(GREMARKS.Index).Value
                txtuploadname.Text = gridupload.Rows(e.RowIndex).Cells(GNAME.Index).Value
                txtimgpath.Text = gridupload.Rows(e.RowIndex).Cells(GIMGPATH.Index).Value
                TXTNEWIMGPATH.Text = gridupload.Rows(e.RowIndex).Cells(GNEWIMGPATH.Index).Value
                TXTFILENAME.Text = gridupload.Rows(e.RowIndex).Cells(GFILENAME.Index).Value
                txtuploadsrno.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub uploadgetsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            'If edit = False Then
            Dim i As Integer = 0
            For Each row As DataGridViewRow In grid.Rows
                If row.Visible = True Then
                    row.Cells(GGRIDUPLOADSRNO.Index).Value = i + 1
                    i = i + 1
                End If
            Next
            'End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridupload_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
        If e.KeyCode = Keys.Delete And gridupload.RowCount > 0 Then
            Dim TEMPMSG As Integer = MsgBox("This Will Delete File, Wish to Proceed?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                If FileIO.FileSystem.FileExists(gridupload.Rows(gridupload.CurrentRow.Index).Cells(GNEWIMGPATH.Index).Value) Then FileIO.FileSystem.DeleteFile(gridupload.Rows(gridupload.CurrentRow.Index).Cells(GNEWIMGPATH.Index).Value)
                gridupload.Rows.RemoveAt(gridupload.CurrentRow.Index)
                uploadgetsrno(gridupload)
            End If
        End If
    End Sub

    Sub fillgridscan()
        Try
            If GRIDUPLOADDOUBLECLICK = False Then

                gridupload.Rows.Add(Val(txtuploadsrno.Text.Trim), txtuploadremarks.Text.Trim, txtuploadname.Text.Trim, txtimgpath.Text.Trim, TXTNEWIMGPATH.Text.Trim, TXTFILENAME.Text.Trim)
                uploadgetsrno(gridupload)

            ElseIf GRIDUPLOADDOUBLECLICK = True Then

                gridupload.Item(0, TEMPUPLOADROW).Value = txtuploadsrno.Text.Trim
                gridupload.Item(1, TEMPUPLOADROW).Value = txtuploadremarks.Text.Trim
                gridupload.Item(2, TEMPUPLOADROW).Value = txtuploadname.Text.Trim
                gridupload.Item(3, TEMPUPLOADROW).Value = txtimgpath.Text.Trim
                gridupload.Item(GNEWIMGPATH.Index, TEMPUPLOADROW).Value = TXTNEWIMGPATH.Text.Trim
                gridupload.Item(GFILENAME.Index, TEMPUPLOADROW).Value = TXTFILENAME.Text.Trim

                GRIDUPLOADDOUBLECLICK = False

            End If
            gridupload.FirstDisplayedScrollingRowIndex = gridupload.RowCount - 1
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridupload_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If gridupload.RowCount > 0 Then
                If Not FileIO.FileSystem.FileExists(gridupload.Rows(e.RowIndex).Cells(GNEWIMGPATH.Index).Value) Then
                    PBSoftCopy.ImageLocation = gridupload.Rows(e.RowIndex).Cells(GIMGPATH.Index).Value
                Else
                    PBSoftCopy.ImageLocation = gridupload.Rows(e.RowIndex).Cells(GNEWIMGPATH.Index).Value
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdupload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdupload.Click
        If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png;*.pdf)|*.bmp;*.jpeg;*.png;*.pdf"
        OpenFileDialog1.ShowDialog()

        OpenFileDialog1.AddExtension = True
        TXTFILENAME.Text = OpenFileDialog1.SafeFileName
        txtimgpath.Text = OpenFileDialog1.FileName
        TXTNEWIMGPATH.Text = Application.StartupPath & "\UPLOADDOCS\" & TXTINVOICENO.Text.Trim & txtuploadsrno.Text.Trim & TXTFILENAME.Text.Trim
        On Error Resume Next

        If txtimgpath.Text.Trim.Length <> 0 Then
            PBSoftCopy.ImageLocation = txtimgpath.Text.Trim
            PBSoftCopy.Load(txtimgpath.Text.Trim)
            txtuploadsrno.Focus()
        End If
    End Sub

    Private Sub txtuploadsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuploadsrno.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(GGRIDUPLOADSRNO.Index).Value) + 1
            Else
                txtuploadsrno.Text = 1
            End If
        End If
    End Sub

    Private Sub CMDVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDVIEW.Click
        Try
            If txtimgpath.Text.Trim <> "" Then
                If Path.GetExtension(txtimgpath.Text.Trim) = ".pdf" Then
                    System.Diagnostics.Process.Start(txtimgpath.Text.Trim)
                Else
                    Dim objVIEW As New ViewImage
                    objVIEW.pbsoftcopy.ImageLocation = PBSoftCopy.ImageLocation
                    objVIEW.ShowDialog()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHGS_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCHGS.CellDoubleClick
        EDITCHGSROW()
    End Sub

    Private Sub GRIDCHGS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCHGS.KeyDown
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
                TOTAL()
                getsrno(GRIDCHGS)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITCHGSROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub EDITCHGSROW()
        Try
            TXTCHGSPER.ReadOnly = False
            TXTCHGSAMT.ReadOnly = False
            If GRIDCHGS.CurrentRow.Index >= 0 And GRIDCHGS.Item(GSRNO.Index, GRIDCHGS.CurrentRow.Index).Value <> Nothing Then
                GRIDCHGSDOUBLECLICK = True
                TXTCHGSSRNO.Text = GRIDCHGS.Item(GSRNO.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                CMBCHARGES.Text = GRIDCHGS.Item(ECHARGES.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSPER.Text = GRIDCHGS.Item(EPER.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSAMT.Text = GRIDCHGS.Item(EAMT.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTTAXID.Text = GRIDCHGS.Item(ETAXID.Index, GRIDCHGS.CurrentRow.Index).Value.ToString

                If Val(TXTCHGSPER.Text.Trim) > 0 Then TXTCHGSAMT.ReadOnly = True

                TEMPCHGSROW = GRIDCHGS.CurrentRow.Index
                TXTCHGSSRNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDINVOICE_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDINVOICE.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub CMBTRANSCITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBFROMCITY.Enter
        Try
            If CMBFROMCITY.Text.Trim = "" Then fillCITY(CMBFROMCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTOCITY.Enter
        Try
            If CMBTOCITY.Text.Trim = "" Then fillCITY(CMBTOCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCRDAYS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCRDAYS.KeyPress
        numdot(e, TXTCRDAYS, Me)
    End Sub

    Private Sub TXTCRDAYS_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTCRDAYS.Validated
        Try
            If INVOICEDATE.Text <> "__/__/____" Then
                If Val(TXTCRDAYS.Text.Trim) > 0 Then duedate.Value = Convert.ToDateTime(INVOICEDATE.Text).Date.AddDays(Val(TXTCRDAYS.Text.Trim))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtrans.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'"
                OBJLEDGER.ShowDialog()
                'If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMCITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBFROMCITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCITY As New SelectCity
                OBJCITY.FRMSTRING = "CITY"
                OBJCITY.ShowDialog()
                If OBJCITY.TEMPNAME <> "" Then CMBFROMCITY.Text = OBJCITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTOCITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCITY As New SelectCity
                OBJCITY.FRMSTRING = "CITY"
                OBJCITY.ShowDialog()
                If OBJCITY.TEMPNAME <> "" Then CMBTOCITY.Text = OBJCITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEM_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBITEM.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "MERCHANT"
                OBJItem.STRSEARCH = " and ITEM_cmpid = " & CmpId & " and ITEM_LOCATIONid = " & Locationid & " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then CMBITEM.Text = OBJItem.TEMPNAME
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
                Dim OBJQ As New SelectQuality
                OBJQ.FRMSTRING = "QUALITY"
                OBJQ.ShowDialog()
                If OBJQ.TEMPNAME <> "" Then CMBQUALITY.Text = OBJQ.TEMPNAME
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

    Private Sub CMBSHADE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBSHADE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCOLOR As New SelectShade
                OBJCOLOR.ShowDialog()
                If OBJCOLOR.TEMPNAME <> "" Then CMBSHADE.Text = OBJCOLOR.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSPER_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHGSPER.KeyPress, TXTEXPDIFF.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSPER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHGSPER.Validating
        Try
            Dim dDebit As Decimal
            Dim bValid As Boolean = Decimal.TryParse(TXTCHGSPER.Text.Trim, dDebit)
            If bValid Then
                If Val(TXTCHGSPER.Text) = 0 Then TXTCHGSPER.Text = ""
                TXTCHGSPER.Text = Convert.ToDecimal(Val(TXTCHGSPER.Text))
                '' everything is good
                calchgs()
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

    Private Sub GRIDINVOICE_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDINVOICE.CellValidating
        Dim colNum As Integer = GRIDINVOICE.Columns(e.ColumnIndex).Index
        If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return
        Select Case colNum

            Case GRATE.Index, Gpcs.Index, GOTHERAMT.Index, GDISCPER.Index, GSPDISCPER.Index, GCUT.Index, GGRIDTOTAL.Index
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                If bValid Then
                    If GRIDINVOICE.CurrentCell.Value = Nothing Then GRIDINVOICE.CurrentCell.Value = "0.00"
                    GRIDINVOICE.CurrentCell.Value = Convert.ToDecimal(GRIDINVOICE.Item(colNum, e.RowIndex).Value)
                    '' everything is good


                    'FOR REVERSE CALC
                    If (ClientName = "CC" Or ClientName = "C3") And colNum = GGRIDTOTAL.Index And CHKRETAIL.CheckState = CheckState.Checked And CHKREVERSECALC.CheckState = CheckState.Checked Then
                        Dim OBJCMN As New ClsCommon
                        Dim DTHSN As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER_DESC.HSN_CGST, 0) AS CGST, ISNULL(HSNMASTER_DESC.HSN_SGST, 0) AS SGST, ISNULL(HSNMASTER_DESC.HSN_IGST, 0) AS IGST ", "", "HSNMASTER INNER JOIN HSNMASTER_DESC ON HSNMASTER.HSN_ID = HSNMASTER_DESC.HSN_ID INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND HSNMASTER_DESC.HSN_WEFDATE <= '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND ITEMMASTER.ITEM_NAME= '" & GRIDINVOICE.Rows(GRIDINVOICE.CurrentRow.Index).Cells(GITEMNAME.Index).EditedFormattedValue & "' AND HSNMASTER.HSN_YEARID=" & YearId & " ORDER BY HSNMASTER_DESC.HSN_WEFDATE DESC")
                        GRIDINVOICE.Rows(GRIDINVOICE.CurrentRow.Index).Cells(GRATE.Index).Value = Format((Val(GRIDINVOICE.Rows(GRIDINVOICE.CurrentRow.Index).Cells(GGRIDTOTAL.Index).EditedFormattedValue) / (Val(DTHSN.Rows(0).Item("IGST")) + 100)) * 100, "0.00")
                    End If


                    TOTAL()
                Else
                    MessageBox.Show("Invalid Number Entered")
                    e.Cancel = True
                    Exit Sub
                End If
            Case GPER.Index, GDESCRIPTION.Index
                TOTAL()
        End Select

        If EDIT = False Then
            Dim TEMPITEM As String = ""
            For I As Integer = GRIDINVOICE.CurrentRow.Index + 1 To GRIDINVOICE.RowCount - 1
                If I = GRIDINVOICE.CurrentRow.Index + 1 Then TEMPITEM = GRIDINVOICE.Item(GITEMNAME.Index, I - 1).Value
                If GRIDINVOICE.Item(GITEMNAME.Index, I).Value = TEMPITEM Then

                    If ClientName = "KDFAB" Then GRIDINVOICE.Item(GRATE.Index, I).Value = GRIDINVOICE.Item(GRATE.Index, I - 1).EditedFormattedValue

                    GRIDINVOICE.Item(GDISCPER.Index, I).Value = GRIDINVOICE.Item(GDISCPER.Index, I - 1).EditedFormattedValue
                    GRIDINVOICE.Item(GSPDISCPER.Index, I).Value = GRIDINVOICE.Item(GSPDISCPER.Index, I - 1).EditedFormattedValue
                End If
                If ClientName = "SBA" Then GRIDINVOICE.Item(GBALENO.Index, I).Value = GRIDINVOICE.Item(GBALENO.Index, I - 1).EditedFormattedValue

                If ClientName = "PURPLE" Then
                    GRIDINVOICE.Item(GDISCPER.Index, I).Value = GRIDINVOICE.Item(GDISCPER.Index, I - 1).EditedFormattedValue
                    GRIDINVOICE.Item(GSPDISCPER.Index, I).Value = GRIDINVOICE.Item(GSPDISCPER.Index, I - 1).EditedFormattedValue
                End If
            Next
        End If
        TOTAL()

    End Sub

    Private Sub txtrefno_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtrefno.Validated
        Try
            If (ClientName = "SAKARIA" Or ClientName = "NVAHAN") And txtrefno.Text <> "" And EDIT = False And GRIDINVOICE.Rows.Count = 0 Then
                'GET PURNO AND PURREGISTER TO FETCH THE DATA FROM PURCHASE
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("BILL_NO AS BILLNO, REGISTER_NAME AS REGNAME", "", "PURCHASEMASTER INNER JOIN REGISTERMASTER ON BILL_REGISTERID = REGISTER_ID ", " AND BILL_INITIALS = '" & txtrefno.Text.Trim & "' AND BILL_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPPURNO = Val(DT.Rows(0).Item("BILLNO"))
                    TEMPPURREGNAME = DT.Rows(0).Item("REGNAME")
                    GETDATAFROMPUR()
                End If
            End If
            If txtrefno.Text.Trim = "" Then CMBSUPPLIERNAME.Text = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtrefno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtrefno.Validating
        Try
            If (ClientName = "SAKARIA" Or ClientName = "NVAHAN") And txtrefno.Text <> "" Then
                If EDIT = False Or (EDIT = True And TEMPREFNO <> txtrefno.Text.Trim) Then
                    Dim OBJCMN As New ClsCommon
                    Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(INVOICEMASTER.INVOICE_REFNO,'') AS REFNO", "", " INVOICEMASTER ", "  AND INVOICEMASTER.INVOICE_REFNO='" & txtrefno.Text.Trim & "' AND INVOICEMASTER.INVOICE_CMPID = " & CmpId & " AND INVOICEMASTER.INVOICE_LOCATIONID = " & Locationid & " AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        MsgBox("Ref No Already Exist")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTINVOICENO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTINVOICENO.KeyPress
        numkeypress(e, TXTINVOICENO, Me)
    End Sub

    Private Sub TXTINVOICENO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTINVOICENO.Validating
        Try
            If ALLOWMANUALINVNO = True Then
                If (Val(TXTINVOICENO.Text.Trim) <> 0 And cmbregister.Text.Trim <> "" And EDIT = False) Or (EDIT = True And TEMPINVOICENO <> Val(TXTINVOICENO.Text.Trim)) Then
                    Dim OBJCMN As New ClsCommon
                    Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(INVOICEMASTER.INVOICE_NO,0)  AS INVNO", "", " INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID AND INVOICE_CMPID = REGISTER_CMPID AND INVOICE_LOCATIONID = REGISTER_LOCATIONID AND INVOICE_YEARID = REGISTER_YEARID ", "  AND INVOICEMASTER.INVOICE_NO=" & TXTINVOICENO.Text.Trim & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_CMPID = " & CmpId & " AND INVOICEMASTER.INVOICE_LOCATIONID = " & Locationid & " AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        MsgBox("Invoice No Already Exist")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub filltax()
        Try
            TXTCHGSPER.ReadOnly = False
            TXTCHGSAMT.ReadOnly = False
            TXTTAXID.Text = 0
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable = objclscommon.search(" ISNULL(tax_tax, 0) as TAX, TAX_ID AS TAXID ", "", " TAXMASTER", " AND tax_name = '" & CMBCHARGES.Text & "'  AND tax_cmpid=" & CmpId & " AND tax_LOCATIONID = " & Locationid & " AND tax_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                TXTCHGSPER.Text = dt.Rows(0).Item("TAX")
                TXTTAXID.Text = Val(dt.Rows(0).Item("TAXID"))
                If Val(TXTCHGSPER.Text.Trim) > 0 Then TXTCHGSAMT.ReadOnly = True
                TXTCHGSPER.ReadOnly = True
            End If
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

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If ClientName = "AVIS" Then
                If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND GROUP_NAME <> 'HASTE DEBTORS'")
            Else
                If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdshowdetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshowdetails.Click
        Try
            Dim OBJRECPAY As New ShowRecPay
            OBJRECPAY.MdiParent = MDIMain
            OBJRECPAY.PURBILLINITIALS = salereginitial & "-" & TEMPINVOICENO
            OBJRECPAY.SALEBILLINITIALS = salereginitial & "-" & TEMPINVOICENO

            OBJRECPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTAMT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTAMT.KeyPress, TXTCGSTAMT.KeyPress, TXTSGSTAMT.KeyPress, TXTIGSTAMT.KeyPress, TXTCGSTAMT1.KeyPress, TXTSGSTAMT1.KeyPress, TXTIGSTAMT1.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub CMBLEDGERCODE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBLEDGERCODE.Enter
        Dim objclscommon As New ClsCommonMaster
        Dim dt As DataTable
        dt = objclscommon.search("acc_CODE", "", " ACCOUNTSMaster ", " and acc_cmpid = " & CmpId & " and acc_locationid = " & Locationid & " and acc_yearid = " & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "acc_CODE"
            CMBLEDGERCODE.DataSource = dt
            CMBLEDGERCODE.DisplayMember = "acc_CODE"
            CMBLEDGERCODE.Text = TEMPACCCODE
        End If
    End Sub

    Private Sub CMBLEDGERCODE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBLEDGERCODE.Validated
        Try
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclscommon.search("acc_CMPNAME AS NAME", "", " ACCOUNTSMaster ", " AND ACC_CODE='" & CMBLEDGERCODE.Text.Trim & "' AND acc_cmpid = " & CmpId & " and acc_locationid = " & Locationid & " and acc_yearid = " & YearId)

            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "NAME"
                cmbname.DataSource = dt
                cmbname.DisplayMember = "NAME"
                CMBLEDGERCODE.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLEDGERCODE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBLEDGERCODE.KeyDown
        'Try
        '    If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
        '    If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

        '    If e.KeyCode = Keys.F1 Then
        '        Dim OBJLEDGER As New SelectLedger
        '        OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'"
        '        OBJLEDGER.ShowDialog()
        '        If OBJLEDGER.TEMPCODE <> "" Then CMBLEDGERCODE.Text = OBJLEDGER.TEMPCODE
        '        If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Sub GETHSNCODE()
        Try
            If Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then

                Dim OBJCMN As New ClsCommon
                'Dim DT As DataTable = OBJCMN.search("  ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND ITEMMASTER.ITEM_NAME= '" & CMBITEM.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
                Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER_DESC.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER_DESC.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER_DESC.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER_DESC.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN HSNMASTER_DESC ON HSNMASTER.HSN_ID = HSNMASTER_DESC.HSN_ID INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND HSNMASTER_DESC.HSN_WEFDATE <= '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND ITEMMASTER.ITEM_NAME= '" & CMBITEM.Text.Trim & "' AND HSNMASTER.HSN_YEARID=" & YearId & " ORDER BY HSNMASTER_DESC.HSN_WEFDATE DESC")
                If DT.Rows.Count > 0 Then


                    TXTHSNCODE.Clear()
                    TXTCGSTPER.Clear()
                    TXTCGSTAMT.Clear()
                    TXTSGSTPER.Clear()
                    TXTSGSTAMT.Clear()
                    TXTIGSTPER.Clear()
                    TXTIGSTAMT.Clear()


                    If CHKMANUAL.CheckState = CheckState.Unchecked Then
                        TXTCGSTPER1.Clear()
                        TXTCGSTAMT1.Clear()
                        TXTSGSTPER1.Clear()
                        TXTSGSTAMT1.Clear()
                        TXTIGSTPER1.Clear()
                        TXTIGSTAMT1.Clear()
                    End If

                    TXTHSNCODE.Text = DT.Rows(0).Item("HSNCODE")
                    If TXTSTATECODE.Text.Trim = CMPSTATECODE Then

                        TXTIGSTPER.Text = 0
                        TXTIGSTPER1.Text = 0
                        If CHKEXPORTGST.CheckState = CheckState.Unchecked Then
                            TXTCGSTPER.Text = Val(DT.Rows(0).Item("CGSTPER"))
                            TXTSGSTPER.Text = Val(DT.Rows(0).Item("SGSTPER"))
                            TXTCGSTPER1.Text = Val(DT.Rows(0).Item("CGSTPER"))
                            TXTSGSTPER1.Text = Val(DT.Rows(0).Item("SGSTPER"))
                        Else
                            TXTCGSTPER.Text = Val(DT.Rows(0).Item("EXPCGSTPER"))
                            TXTSGSTPER.Text = Val(DT.Rows(0).Item("EXPSGSTPER"))
                            TXTCGSTPER1.Text = Val(DT.Rows(0).Item("EXPCGSTPER"))
                            TXTSGSTPER1.Text = Val(DT.Rows(0).Item("EXPSGSTPER"))
                        End If

                    Else
                        TXTCGSTPER.Text = 0
                        TXTSGSTPER.Text = 0
                        TXTCGSTPER1.Text = 0
                        TXTSGSTPER1.Text = 0
                        If CHKEXPORTGST.CheckState = CheckState.Unchecked Then
                            TXTIGSTPER1.Text = Val(DT.Rows(0).Item("IGSTPER"))
                            TXTIGSTPER.Text = Val(DT.Rows(0).Item("IGSTPER"))
                        Else
                            TXTIGSTPER1.Text = Val(DT.Rows(0).Item("EXPIGSTPER"))
                            TXTIGSTPER.Text = Val(DT.Rows(0).Item("EXPIGSTPER"))
                        End If

                    End If
                End If
                CALC()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBITEM.Validated

        Try

            If CMBITEM.Text.Trim <> "" And Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then GETHSNCODE()


            'GET LAST RATE OF SELECTED ITEM FOR SELECTED PARTY
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If (ClientName = "NIRAJ" Or ClientName = "DETLINE" Or ClientName = "MVIKASKUMAR" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "MSANCHITKUMAR") And CMBITEM.Text.Trim <> "" And cmbname.Text.Trim <> "" Then
                DT = OBJCMN.SEARCH(" TOP 1 ISNULL(INVOICEMASTER_DESC.INVOICE_RATE,0) AS LASTRATE", "", " INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN INVOICEMASTER ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO  AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN LEDGERS ON ACC_ID = INVOICE_LEDGERID", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & CMBITEM.Text.Trim & "' AND INVOICEMASTER.INVOICE_DATE < '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER.INVOICE_NO DESC")
                If DT.Rows.Count > 0 Then LBLRATE.Text = Format(Val(DT.Rows(0).Item("LASTRATE")), "0.00")
            End If

            If (ClientName = "MAHAVIR" Or ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "MVIKASKUMAR" Or ClientName = "MINALFAB" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "CHINTAN" Or ClientName = "MAFATLAL" Or ClientName = "MASHOK" Or ClientName = "ABHEE" Or ClientName = "SUPEEMA") And CMBITEM.Text.Trim <> "" And EDIT = False Then
                Dim OBJCMN1 As New ClsCommon
                Dim DT1 As DataTable = OBJCMN1.SEARCH("  ISNULL(item_reorder, 0) AS CUT, ISNULL(ITEM_RATE, 0) AS RATE,ISNULL(ITEM_FOLD, '') AS [DESC],ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(ITEMMASTER.ITEM_REMARKS,'') AS ITEMREMARKS", "", " ITEMMASTER LEFT OUTER JOIN UNITMASTER ON ITEMMASTER.item_unitid = UNITMASTER.unit_id ", " AND ITEMMASTER.item_name = '" & CMBITEM.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID='" & YearId & "' ")
                If DT1.Rows.Count > 0 Then
                    TXTCUT.Text = DT1.Rows(0).Item("CUT")
                    TXTRATE.Text = DT1.Rows(0).Item("RATE")
                    If DT1.Rows(0).Item("UNIT") = "Pcs" Then
                        CMBPER.Text = "Pcs"
                    ElseIf LCase(DT1.Rows(0).Item("UNIT")) = "yards" Then
                        CMBPER.Text = "Yards"
                    ElseIf LCase(DT1.Rows(0).Item("UNIT")) = "kgs" Then
                        CMBPER.Text = "Kgs"
                    Else
                        CMBPER.Text = "Mtrs"
                    End If

                    If ClientName = "MAHAVIR" Then TXTDESCRIPTION.Text = DT1.Rows(0).Item("DESC")
                    If ClientName = "MASHOK" Or ClientName = "ABHEE" Then TXTDESCRIPTION.Text = DT1.Rows(0).Item("ITEMREMARKS")
                End If
            End If

            'FETCH ITEMRATE FROM PRICELIST
            If ClientName = "SONAL" And cmbname.Text.Trim <> "" And CMBITEM.Text.Trim <> "" And Val(TXTRATE.Text.Trim) = 0 Then
                Dim DTRATE As DataTable = OBJCMN.SEARCH("PRICELISTMASTER.PL_RATE AS SALERATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME ", "", "PRICELISTMASTER INNER JOIN ITEMMASTER ON PRICELISTMASTER.PL_ITEMID = ITEMMASTER.item_id INNER JOIN LEDGERS ON PRICELISTMASTER.PL_LEDGERID = LEDGERS.ACC_ID ", " AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '" & cmbname.Text.Trim & "' AND ISNULL(ITEMMASTER.ITEM_NAME,'') = '" & CMBITEM.Text.Trim & "' AND PL_YEARID = " & YearId)
                If DTRATE.Rows.Count > 0 Then TXTRATE.Text = Val(DTRATE.Rows(0).Item("SALERATE"))
            End If

            If (ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI") And CMBITEM.Text.Trim <> "" And EDIT = False Then
                DT = OBJCMN.SEARCH(" ISNULL(PARTYITEMWISECHART.PAR_STAMPING, '') AS STAMPING", "", " PARTYITEMWISECHART INNER JOIN LEDGERS ON PARTYITEMWISECHART.PAR_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON PARTYITEMWISECHART.PAR_ITEMID = ITEMMASTER.item_id ", " AND ledgers.acc_cmpname = '" & cmbname.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & CMBITEM.Text.Trim & " ' AND PARTYITEMWISECHART.PAR_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    For Each DTROW As DataRow In DT.Rows
                        TXTDESCRIPTION.Text = (DT.Rows(0).Item("STAMPING"))
                    Next
                End If
            End If

            'GET CATEGORY
            DT = OBJCMN.SEARCH("isnull(CATEGORY_NAME,'') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEM_CATEGORYID = CATEGORY_ID", " AND ITEM_NAME = '" & CMBITEM.Text.Trim & "' AND ITEM_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                LBLCATEGORY.Text = DT.Rows(0).Item("CATEGORY")
            End If


            'GET RATE FROM ORDERGRID
            If (ClientName = "MASHOK" Or ClientName = "ABHEE") And CMBITEM.Text.Trim <> "" And GRIDORDER.RowCount > 0 Then
                For Each ROW As DataGridViewRow In GRIDORDER.Rows
                    If ROW.Cells(OITEMNAME.Index).Value = CMBITEM.Text.Trim Then
                        TXTRATE.Text = Val(ROW.Cells(ORATE.Index).Value)
                        Exit For
                    End If
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTBARCODE.Validating
        Try
            If TXTBARCODE.Text.Trim <> "" Then
                'CHECKING WHETHER IS IS GONE OUT OR NOT
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("TOP 1 TYPE, FROMNO", "", " OUTBARCODESTOCK ", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "' AND CMPID = " & CmpId & " AND LOCATIONID = " & Locationid & " AND YEARID = " & YearId)
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

    Private Sub CHKBARCODE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKBARCODE.CheckedChanged
        Try
            If CHKBARCODE.Checked = True Then
                LBLBARCODE.Visible = True
                TXTBARCODE.Visible = True
                CMDSELECTGDN.Enabled = False
                'TXTBARCODE.Focus()
            Else
                CMDSELECTGDN.Enabled = True
                LBLBARCODE.Visible = False
                TXTBARCODE.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InvoiceMaster_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'FOR HIDEVIEW
            'If ClientName = "DAKSH" Or ClientName = "JITUBHAI" Or ClientName = "SHALIBHADRA" Or ClientName = "MAHAVIR" Or ClientName = "MANINATH" Or ClientName = "KCRAYON" Then CMBSCREENTYPE.Text = "TOTAL GST" Else CMBSCREENTYPE.Text = "LINE GST"
            CMBSCREENTYPE.Text = INVOICESCREENTYPE
            HIDEVIEW()

            If ALLOWEINVOICE = False Then TOOLEINV.Visible = False

            If ALLOWBILLCHECKDISPUTE = False Then
                CHKBILLCHECKED.Enabled = False
                CHKBILLDISPUTE.Enabled = False
            End If

            If ClientName = "INDRAPUJAIMPEX" Or ClientName = "SOFTAS" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "PARTOBA" Then CHKBLANKPAPER.CheckState = CheckState.Checked

            If ClientName = "MASHOK" Or ClientName = "ABHEE" Then
                CMDSELECTGDN.Visible = False
                CMDSELECTSO.Visible = True
                Gpcs.ReadOnly = False
                Gmtrs.ReadOnly = False
                txtchallan.ReadOnly = False
                CHKTRADINGACC.Visible = True

                If ClientName = "ABHEE" Then
                    LBLTRANS.Visible = False
                    cmbtrans.Visible = False
                    LBLLRNO.Visible = False
                    txtlrno.Visible = False
                    LBLLRDATE.Visible = False
                    LRDATE.Visible = False
                    TXTSRNO.Visible = False
                    CMBITEM.Visible = False
                    TXTHSNCODE.Visible = False
                    TXTQTY.Visible = False
                    TXTFOLDPER.Visible = False
                    TXTGRIDLRNO.Visible = False
                    CMBGRIDTRANS.Visible = False
                    CMBQUALITY.Visible = False
                    CMBDESIGN.Visible = False
                    CMBSHADE.Visible = False
                    TXTDESCRIPTION.Visible = False
                    TXTBALENO.Visible = False
                    TXTPCS.Visible = False
                    TXTCUT.Visible = False
                    TXTMTRS.Visible = False
                    TXTRATE.Visible = False
                    CMBPER.Visible = False
                    TXTAMT.Visible = False
                    GGRIDPURPARTY.Visible = True
                    GPURPARTYBILLNO.Visible = True

                    GLRNO.Visible = True
                    GTRANS.Visible = True

                    GWT.Visible = True
                    LBLTOTALWT.Visible = True
                    CMDSELECTSTOCK.Visible = True
                End If
            Else
                CMDSELECTGDN.Visible = True
                CMDSELECTSO.Visible = False
            End If


            If ClientName = "PARAS" Then LBLCATEGORY.Visible = True
            TXTINVOICENO.TabStop = ALLOWMANUALINVNO

            If ClientName = "NVAHAN" Or ClientName = "SAKARIA" Then
                txtrefno.Width = 132
                LBLPURNAME.Visible = True
                CMBSUPPLIERNAME.Visible = True
                If ClientName = "NVAHAN" Then
                    CMBQUALITY.TabStop = False
                    CMBDESIGN.TabStop = False
                    CMBSHADE.TabStop = False
                    TXTDESCRIPTION.TabStop = False
                End If
            End If

            If ClientName = "SKF" Or ClientName = "MASHOK" Or ClientName = "ABHEE" Or ClientName = "NIRAJ" Or ClientName = "SOFTAS" Or ClientName = "SONAL" Or ClientName = "MINALFAB" Or ClientName = "ARPITA" Then
                If ClientName <> "NIRAJ" And ClientName <> "MASHOK" And ClientName <> "ABHEE" Then LBLPACKING.Text = "Dyeing Name"
                txtchallan.ReadOnly = False
                txtchallan.TabStop = True
                CHALLANDATE.TabStop = True
            End If

            If ClientName = "NIRAJ" Or ClientName = "DETLINE" Or ClientName = "MVIKASKUMAR" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "MSANCHITKUMAR" Then
                LBLRATE.Visible = True
            End If



            If ClientName = "MBB" Or ClientName = "MSANCHITKUMAR" Then GDESCRIPTION.ReadOnly = False

            If ClientName = "SOFTAS" Then
                CMBQUALITY.TabStop = False
                CMBDESIGN.TabStop = False
                CMBSHADE.TabStop = False
                TXTBALENO.TabStop = False
            End If

            If ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                GDESCRIPTION.HeaderText = "Stamping"
                LBLBALEFROM.Text = "Weight"
                LBLBALETO.Visible = True
                TXTBALENOTO.Visible = True
                LBLBALETO.Text = "Freight"
                LBLREFNO.Text = "Block Sticker"

            End If


            If ClientName = "KDFAB" Or ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "SAKARIA" Or ClientName = "NVAHAN" Or ClientName = "SUCCESS" Or ClientName = "NIRAJ" Or ClientName = "TCOT" Or ClientName = "SOFTAS" Or ClientName = "RMANILAL" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "YASHVI" Or ClientName = "MINALFAB" Then
                LBLBALEFROM.Text = "Bls/Bundle"
                LBLBALETO.Visible = False
                TXTBALENOTO.Visible = False
                If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                    GSPDISCPER.HeaderText = "Brk%"
                    GSPDISCAMT.HeaderText = "Brk Amt"
                End If
            End If

            If ClientName = "MANINATH" Then
                GSPDISCPER.HeaderText = "C.D.%"
                GSPDISCAMT.HeaderText = "C.D. Amt"
                GSPDISCPER.HeaderText = "Brk%"
                GSPDISCAMT.HeaderText = "Brk Amt"
            End If

            If ClientName = "PURPLE" Or ClientName = "CC" Or ClientName = "C3" Then
                CHKBARCODE.Visible = True
                CHKBARCODE.Enabled = True
                CHKBARCODE.Checked = True
                CHKRETAIL.Visible = True
                CHKRETAIL.CheckState = CheckState.Checked
                CMDSELECTGDN.Enabled = True
                CHKREVERSECALC.Visible = True
                If ClientName = "PURPLE" Then CHKREVERSECALC.Checked = True
                GGRIDTOTAL.ReadOnly = False
                txtrefno.Width = TXTCRDAYS.Width
                LBLSOADVANCE.Visible = True
                TXTSOADVANCE.Visible = True
                TXTSOADVANCE.ReadOnly = False
            Else
                CHKBARCODE.Visible = False
                CMDSELECTGDN.Enabled = True
                CHKRETAIL.CheckState = CheckState.Unchecked
            End If

            If ClientName = "MBB" Then
                CHKREVERSECALC.Visible = True
                CHKREVERSECALC.CheckState = CheckState.Checked
            End If


            If ClientName = "DRDRAPES" And UserName <> "Admin" Then
                GRATE.ReadOnly = True
                TXTRATE.ReadOnly = True
            End If

            If ClientName = "SONAL" Then
                CMBQUALITY.TabStop = False
                CMBDESIGN.TabStop = False
                CMBSHADE.TabStop = False
                TXTDESCRIPTION.TabStop = False
                TXTBALENO.TabStop = False
                TXTCUT.TabStop = False
            End If

            If ClientName = "AVIS" Then
                LBLCITY.Text = "Delivery To"
                TXTINVOICENO.TabStop = False
                INVOICEDATE.TabStop = False
                LBLGATEPASS.Visible = True
                TXTGATEPASSNO.Visible = True
                CMDSELECTGDN.Text = "S&elect GP"
                GPARTYPONO.ReadOnly = False
                GFROMNO.Visible = True
                GRIDINVOICE.Width = GRIDINVOICE.Width + 100
            End If

            If ITEMCOSTCENTRE = True Then
                LBLCOSTCENTER.Visible = True
                CMBCOSTCENTERNAME.Visible = True
            End If

            If ClientName = "SUPRIYA" Or ClientName = "VINAYAK" Or ClientName = "KUNAL" Or ClientName = "SIDDHGIRI" Or ClientName = "SANGHVI" Or ClientName = "VSTRADERS" Or ClientName = "SNCM" Then
                LBLHASTE.Visible = True
                CMBHASTE.Visible = True
            End If

            If ClientName = "GELATO" Then
                GSHADE.HeaderText = "Size"
                LBLREFNO.Text = "Disc %"
                txtrefno.TextAlign = HorizontalAlignment.Right
            End If

            If ClientName = "MANSI" Then
                LBLCHDATE.Visible = False
                CHALLANDATE.Visible = False
                LRDATE.Visible = False
                LBLLRDATE.Visible = False
                LBLSODATE.Visible = False
                sodate.Visible = False
                TXTAMT.ReadOnly = False
                TXTAMT.TabStop = True
            End If

            If ClientName = "REALCORPORATION" Then
                GRIDMILLSUMM.Visible = True
                CHKCD.Text = "Dhara Bill"
            End If


            If ClientName = "ARIHANTGARMENTS" Then
                txtchallan.ReadOnly = False
                txtchallan.TabStop = True
                CMBQUALITY.TabStop = False
                CMBDESIGN.TabStop = False
                CMBSHADE.TabStop = False
                TXTBALENO.TabStop = False
                CMBPER.Text = "Pcs"
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub INVOICEDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles INVOICEDATE.GotFocus
        INVOICEDATE.SelectionStart = 0
    End Sub

    Private Sub INVOICEDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INVOICEDATE.Validating
        Try
            If INVOICEDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(INVOICEDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    If Val(TXTCRDAYS.Text) = 0 Then duedate.Text = INVOICEDATE.Text
                    'SAME DATE FOR CHALLANDATE / LRDATE 
                    If ClientName = "SOFTAS" Then
                        CHALLANDATE.Text = Convert.ToDateTime(INVOICEDATE.Text).Date
                    End If
                    If ClientName = "CC" Or ClientName = "C3" Then
                        CHALLANDATE.Text = Convert.ToDateTime(INVOICEDATE.Text).Date
                        sodate.Text = Convert.ToDateTime(INVOICEDATE.Text).Date
                        LRDATE.Text = Convert.ToDateTime(INVOICEDATE.Text).Date
                        GPDATE.Text = Convert.ToDateTime(INVOICEDATE.Text).Date
                    End If
                    GETHSNCODE()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLANDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CHALLANDATE.Validating
        Try
            If CHALLANDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(CHALLANDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LRDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles LRDATE.GotFocus
        LRDATE.SelectAll()
    End Sub

    Private Sub LRDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles LRDATE.Validating
        Try
            If LRDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(LRDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub sodate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles sodate.GotFocus
        sodate.SelectAll()
    End Sub

    Private Sub sodate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles sodate.Validating
        Try
            If sodate.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(sodate.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPACKING.Enter
        Try
            If ClientName = "AVIS" Then
                If CMBPACKING.Text.Trim = "" Then FILLNAME(CMBPACKING, EDIT, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')  AND GROUP_NAME = 'HASTE DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBPACKING.Text.Trim = "" Then FILLNAME(CMBPACKING, EDIT, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')   AND ACC_TYPE = 'ACCOUNTS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPACKING.Validated
        Try
            If CMBPACKING.Text.Trim <> "" And ClientName <> "BARKHA" And ClientName <> "MAHAJAN" And ClientName <> "SHUBHI" And ClientName <> "SUBHLAXMI" Then
                'GET REGISTER , AGENCT AND TRANS
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS_1.ACC_CMPNAME,'') AS TRANSNAME, ISNULL(LEDGERS_2.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(REGISTER_NAME,'') AS REGISTERNAME, ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN, ISNULL(LEDGERS.ACC_DISC,0) AS DISCPER, isnull(LEDGERS.ACC_CRDAYS,0) AS CRDAYS, ISNULL(LEDGERS.ACC_MOBILE,'') AS MOBILENO, ISNULL(TERMMASTER.TERM_NAME,'') AS TERM, ISNULL(LEDGERS.ACC_AGENTCOMM,'') AS AGENTCOMM, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN TERMMASTER ON LEDGERS.ACC_TERMID = TERM_ID  LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITY_ID ", " and LEDGERS.acc_cmpname = '" & CMBPACKING.Text.Trim & "' and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' and LEDGERS.acc_YEARid = " & YearId)
                'Dim DT As DataTable = OBJCMN.search("ISNULL(LEDGERS.ACC_MOBILE,'') AS MOBILENO ", "", " LEDGERS ", " and LEDGERS.acc_cmpname = '" & CMBPACKING.Text.Trim & "'  and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTMOBILENO.Text = DT.Rows(0).Item("MOBILENO")
                    If DT.Rows(0).Item("TRANSNAME") <> "" And EDIT = False Then cmbtrans.Text = DT.Rows(0).Item("TRANSNAME")
                    If DT.Rows(0).Item("CITYNAME") <> "" And EDIT = False Then CMBTOCITY.Text = DT.Rows(0).Item("CITYNAME")
                End If
            End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPACKING.Validating
        Try
            If CHKCHANGEADD.Checked = True Then
                If CMBPACKING.Text.Trim <> "" Then NAMEVALIDATE(CMBPACKING, CMBCODE, e, Me, TXTADD, " AND  (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')", "Sundry Creditors", "ACCOUNTS")
            Else
                If CMBPACKING.Text.Trim <> "" Then NAMEVALIDATE(CMBPACKING, CMBCODE, e, Me, txtDeliveryadd, " AND  (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')", "Sundry Creditors", "ACCOUNTS")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGRIDTOTAL_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTGRIDTOTAL.Validating

        Try
            If Val(TXTRATE.Text.Trim) = 0 Then
                If CMBPER.Text = "Pcs" Then
                    TXTRATE.Text = Val(TXTAMT.Text) / Val(TXTPCS.Text)
                Else
                    TXTRATE.Text = Val(TXTAMT.Text) / Val(TXTMTRS.Text)
                End If
            End If

            If CMBITEM.Text.Trim <> "" And CMBPER.Text.Trim <> "" And Val(TXTAMT.Text.Trim) <> 0 And Val(TXTTAXABLEAMT.Text.Trim) <> 0 And Val(TXTGRIDTOTAL.Text.Trim) <> 0 Then
                If ClientName <> "MANSI" And Val(TXTMTRS.Text.Trim) = 0 And Val(TXTPCS.Text.Trim) = 0 Then
                    MsgBox("Please Mtrs/Pcs")
                    TXTMTRS.Focus()
                    Exit Sub
                Else
                    fillgrid()
                End If
            Else
                If CMBITEM.Text.Trim = "" Then
                    MsgBox("Please Fill Item Name ")
                    CMBITEM.Focus()
                    Exit Sub
                End If
                If TXTPCS.Text.Trim = "" Then
                    MsgBox("Please Fill Pcs ")
                    TXTPCS.Focus()
                    Exit Sub
                End If
                If Val(TXTRATE.Text.Trim) <= 0 Then
                    MsgBox("Please Fill Rate")
                    TXTRATE.Focus()
                    Exit Sub
                End If
                If CMBPER.Text.Trim = "" Then
                    MsgBox("Please Fill Per ")
                    CMBPER.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTOTHERAMT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTOTHERAMT.KeyPress
        AMOUNTNUMDOTKYEPRESS(e, sender, Me)
    End Sub

    Private Sub TXTOTHERAMT_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTOTHERAMT.Validated, TXTDISCPER.Validated, TXTDISCAMT.Validated, TXTSPDISCPER.Validated, TXTSPDISCAMT.Validated
        Try
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKEXPORTGST_CheckedChanged(sender As Object, e As EventArgs) Handles CHKEXPORTGST.CheckedChanged
        GETHSNCODE()
    End Sub

    Private Sub TXTDISCAMT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTDISCAMT.KeyPress, TXTDISCPER.KeyPress, TXTSPDISCAMT.KeyPress, TXTSPDISCPER.KeyPress
        Try
            numdotkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTDISCPER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTDISCPER.Validating, TXTSPDISCPER.Validating
        Try
            If Val(TXTDISCPER.Text.Trim) > 100 Or Val(TXTSPDISCPER.Text.Trim) > 100 Then
                MsgBox("Percent Cannot be greater then 100", MsgBoxStyle.Critical)
                e.Cancel = True
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBALENOFROM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTBALENOFROM.KeyPress
        Try
            If ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then numdotkeypress(e, sender, Me) Else numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBALENOTO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTBALENOTO.KeyPress
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKTCS_CheckedChanged(sender As Object, e As EventArgs) Handles CHKTCS.CheckedChanged
        TOTAL()
    End Sub

    Sub HIDEVIEW()
        Try
            If CMBSCREENTYPE.Text = "LINE GST" Then
                TXTDISCPER.Visible = True
                GDISCPER.Visible = True
                TXTDISCAMT.Visible = True
                GDISCAMT.Visible = True
                LBLTOTALDISCAMT.Visible = True

                TXTSPDISCPER.Visible = True
                GSPDISCPER.Visible = True
                TXTSPDISCAMT.Visible = True
                GSPDISCAMT.Visible = True
                LBLTOTALSPDISCAMT.Visible = True

                TXTOTHERAMT.Visible = True
                GOTHERAMT.Visible = True
                LBLTOTALOTHERAMT.Visible = True

                TXTTAXABLEAMT.Visible = True
                GTAXABLEAMT.Visible = True
                LBLTOTALTAXABLEAMT.Visible = True

                TXTCGSTPER.Visible = True
                GCGSTPER.Visible = True
                TXTCGSTAMT.Visible = True
                GCGSTAMT.Visible = True
                LBLTOTALCGSTAMT.Visible = True

                TXTSGSTPER.Visible = True
                GSGSTPER.Visible = True
                TXTSGSTAMT.Visible = True
                GSGSTAMT.Visible = True
                LBLTOTALSGSTAMT.Visible = True

                TXTIGSTPER.Visible = True
                GIGSTPER.Visible = True
                TXTIGSTAMT.Visible = True
                GIGSTAMT.Visible = True
                LBLTOTALIGSTAMT.Visible = True

                TXTGRIDTOTAL.Visible = True
                GGRIDTOTAL.Visible = True

                GRIDINVOICE.Width = 2094


                LBLCGST.Visible = False
                TXTCGSTPER1.Visible = False
                TXTCGSTAMT1.Visible = False
                LBLSGST.Visible = False
                TXTSGSTPER1.Visible = False
                TXTSGSTAMT1.Visible = False
                LBLIGST.Visible = False
                TXTIGSTPER1.Visible = False
                TXTIGSTAMT1.Visible = False

                TXTAMT.TabStop = False
            Else

                TXTDISCPER.Visible = False
                GDISCPER.Visible = False
                TXTDISCAMT.Visible = False
                GDISCAMT.Visible = False
                LBLTOTALDISCAMT.Visible = False

                TXTSPDISCPER.Visible = False
                GSPDISCPER.Visible = False
                TXTSPDISCAMT.Visible = False
                GSPDISCAMT.Visible = False
                LBLTOTALSPDISCAMT.Visible = False

                TXTOTHERAMT.Visible = False
                GOTHERAMT.Visible = False
                LBLTOTALOTHERAMT.Visible = False

                TXTTAXABLEAMT.Visible = False
                GTAXABLEAMT.Visible = False
                LBLTOTALTAXABLEAMT.Visible = False

                TXTCGSTPER.Visible = False
                GCGSTPER.Visible = False
                TXTCGSTAMT.Visible = False
                GCGSTAMT.Visible = False
                LBLTOTALCGSTAMT.Visible = False

                TXTSGSTPER.Visible = False
                GSGSTPER.Visible = False
                TXTSGSTAMT.Visible = False
                GSGSTAMT.Visible = False
                LBLTOTALSGSTAMT.Visible = False

                TXTIGSTPER.Visible = False
                GIGSTPER.Visible = False
                TXTIGSTAMT.Visible = False
                GIGSTAMT.Visible = False
                LBLTOTALIGSTAMT.Visible = False

                TXTGRIDTOTAL.Visible = False
                GGRIDTOTAL.Visible = False

                If ClientName = "ABHEE" Then
                    GRIDINVOICE.Width = 1810
                Else
                    GRIDINVOICE.Width = 1230
                End If



                LBLCGST.Visible = True
                TXTCGSTPER1.Visible = True
                TXTCGSTAMT1.Visible = True
                LBLSGST.Visible = True
                TXTSGSTPER1.Visible = True
                TXTSGSTAMT1.Visible = True
                LBLIGST.Visible = True
                TXTIGSTPER1.Visible = True
                TXTIGSTAMT1.Visible = True

                TXTAMT.TabStop = True

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKCHANGEADD_CheckedChanged(sender As Object, e As EventArgs) Handles CHKCHANGEADD.CheckedChanged
        Try
            txtDeliveryadd.Enabled = CHKCHANGEADD.Checked
            txtDeliveryadd.ReadOnly = Not CHKCHANGEADD.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEINV_Click(sender As Object, e As EventArgs) Handles TOOLEINV.Click
        Try
            If EDIT = False Then Exit Sub
            If TXTSACCODE.Text.Trim = "" Then GENERATEEINV() Else GENERATEJOBEINV()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub GENERATEEINV()
        Try
            If ALLOWEINVOICE = False Then Exit Sub
            If cmbname.Text.Trim = "" Then Exit Sub

            If Val(LBLTOTALCGSTAMT.Text.Trim) = 0 And Val(TXTCGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALSGSTAMT.Text.Trim) = 0 And Val(TXTSGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALIGSTAMT.Text.Trim) = 0 And Val(TXTIGSTAMT1.Text.Trim) = 0 And CHKOVERSEAS.CheckState = CheckState.Unchecked Then Exit Sub


            'If txtlrno.Text.Trim <> "" AndAlso LRDATE.Text <> "__/__/____" Then
            '    If Convert.ToDateTime(LRDATE.Text).Date < Convert.ToDateTime(INVOICEDATE.Text).Date Then
            '        MsgBox("LR Date cannot be Before Invoice Date", MsgBoxStyle.Critical)
            '        Exit Sub
            '    End If
            'End If

            If CMBFROMCITY.Text.Trim = "" Then
                MsgBox("Enter From City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If CMBTOCITY.Text.Trim = "" Then
                MsgBox("Enter to City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Generate E-Invoice?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If TXTIRNNO.Text.Trim <> "" Then
                MsgBox("E-Invoice Already Generated", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'BEFORE GENERATING EWAY BILL WE NEED TO VALIDATE WHETHER ALL THE DATA ARE PRESENT OR NOT
            'IF DATA IS NOT PRESENT THEN VALIDATE
            'DATA TO BE CHECKED 
            '   1)CMPEWBUSER | CMPEWBPASS | CMPGSTIN | CMPPINCODE | CMPCITY | CMPSTATE | 
            '   2)PARTYGSTIN | PARTYCITY | PARTYPINCODE | PARTYSTATE | PARTYSTATECODE | PARTYKMS
            '   3)CGST OR SGST OR IGST (ALWAYS USE MTR IN QTYUNIT)
            If CMPEWBUSER = "" Or CMPEWBPASS = "" Or CMPGSTIN = "" Or CMPPINCODE = "" Or CMPCITYNAME = "" Or CMPSTATENAME = "" Then
                MsgBox(" Company Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim TEMPCMPADD1 As String = ""
            Dim TEMPCMPADD2 As String = ""
            Dim TEMPCMPDISPATCHADD1 As String = ""
            Dim PARTYGSTIN As String = ""
            Dim PARTYPINCODE As String = ""
            Dim PARTYSTATECODE As String = ""
            Dim PARTYSTATENAME As String = ""
            Dim SHIPTOGSTIN As String = ""
            Dim SHIPTOSTATECODE As String = ""
            Dim SHIPTOSTATENAME As String = ""
            Dim SHIPTOPINCODE As String = ""
            Dim PARTYKMS As Double = 0
            Dim PARTYADD1 As String = ""
            Dim PARTYADD2 As String = ""
            Dim PARTYCITY As String = ""
            Dim SHIPTOADD1 As String = ""
            Dim SHIPTOADD2 As String = ""
            Dim TRANSGSTIN As String = ""
            'Dim KOTHARIPLACE As String = ""  'THIS VARIABLE IS USED TO FETCH RANGE COLUMN ONLY FOR KOTHARI, THEY WILL ENTER FULL SHIPTO ADDRESS THERE
            Dim DISPATCHFROM As String = ""
            Dim DISPATCHFROMGSTIN As String = ""
            Dim DISPATCHFROMPINCODE As String = ""
            Dim DISPATCHFROMSTATECODE As String = ""
            Dim DISPATCHFROMSTATENAME As String = ""
            Dim DISPATCHFROMKMS As Double = 0
            Dim DISPATCHFROMADD1 As String = ""
            Dim DISPATCHFROMADD2 As String = ""


            Dim OBJCMN As New ClsCommon
            'CMP ADDRESS DETAILS
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(CMP_ADD1, '') AS ADD1, ISNULL(CMP_ADD2,'') AS ADD2, ISNULL(CMP_DISPATCHFROM, '') AS DISPATCHADD ", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
            TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
            TEMPCMPADD2 = DT.Rows(0).Item("ADD2")
            TEMPCMPDISPATCHADD1 = DT.Rows(0).Item("DISPATCHADD")
            DISPATCHFROM = CmpName
            DISPATCHFROMGSTIN = CMPGSTIN
            DISPATCHFROMPINCODE = CMPPINCODE
            DISPATCHFROMSTATECODE = CMPSTATECODE
            DISPATCHFROMSTATENAME = CMPSTATENAME

            'PARTY GST DETAILS 

            DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN, (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME ", "", " LEDGERS LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If (DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "") And CHKEXPORTGST.Checked = False Then
                MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            Else
                PARTYGSTIN = DT.Rows(0).Item("GSTIN")
                SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                PARTYSTATENAME = DT.Rows(0).Item("STATENAME")
                PARTYSTATECODE = DT.Rows(0).Item("STATECODE")
                SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                PARTYCITY = DT.Rows(0).Item("CITYNAME")
                'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                PARTYADD1 = DT.Rows(0).Item("ADD1")
                PARTYADD2 = DT.Rows(0).Item("ADD2")
            End If

            If CHKEXPORTGST.Checked = True And CHKOVERSEAS.Checked = True Then
                PARTYGSTIN = "URP"
                SHIPTOGSTIN = "URP"
                PARTYSTATECODE = "96"
                SHIPTOSTATECODE = "96"
                PARTYPINCODE = "999999"
            End If



            'FETCH PINCODE / KMS / ADD1 / ADD2 OF SHIPTO IF IT IS NOT SAME AS CMBNAME
            If CMBPACKING.Text.Trim <> "" AndAlso cmbname.Text.Trim <> CMBPACKING.Text.Trim Then
                DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN,  (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_RANGE,'') AS KOTHARIPLACE ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBPACKING.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("PINCODE") = "" Then
                    MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                    SHIPTOPINCODE = DT.Rows(0).Item("PINCODE")
                    'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                    SHIPTOADD1 = DT.Rows(0).Item("ADD1")
                    SHIPTOADD2 = DT.Rows(0).Item("ADD2")
                    SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                    SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                    'KOTHARIPLACE = DT.Rows(0).Item("KOTHARIPLACE")
                End If
            End If


            If CHKEXPORTGST.Checked = True And CHKOVERSEAS.Checked = True Then
                SHIPTOGSTIN = "URP"
                SHIPTOPINCODE = "999999"
            End If



            'DISPATCHFROM GST DETAILS AND KMS WILL BE FETCHED FROM TXTKMS
            If CMBDISPATCHFROM.Text.Trim <> "" AndAlso cmbname.Text.Trim <> CMBDISPATCHFROM.Text.Trim Then
                DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN, (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBDISPATCHFROM.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Then
                    MsgBox(" Dispatch From Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    DISPATCHFROM = CMBDISPATCHFROM.Text.Trim
                    DISPATCHFROMGSTIN = DT.Rows(0).Item("GSTIN")
                    DISPATCHFROMSTATENAME = DT.Rows(0).Item("STATENAME")
                    DISPATCHFROMSTATECODE = DT.Rows(0).Item("STATECODE")
                    DISPATCHFROMPINCODE = DT.Rows(0).Item("PINCODE")
                    'DISPATCHFROMKMS = Val(TXTKMS.Text.Trim)
                    TEMPCMPDISPATCHADD1 = DT.Rows(0).Item("ADD1")
                    TEMPCMPADD2 = "ADD2" 'DT.Rows(0).Item("ADD2")
                End If
            End If



            'TRANSPORT GSTING IS NOT MANDATORY
            'FOR LOCAL TRANSPORT THERE IS NO GSTIN
            'TRANSPORT GSTIN IF TRANSPORT IS PRESENT
            If cmbtrans.Text.Trim <> "" Then
                DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TRANSGSTIN = DT.Rows(0).Item("GSTIN")
                'FOR LOCAL TRANSPORT THERE IS NO GSTIN
                'If TRANSGSTIN = "" Then
                '    MsgBox("Enter Transport GSTIN", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If
            End If



            'CHECKING COUNTER AND VALIDATE WHETHER EINVOICE WILL BE ALLOWED OR NOT, FOR EACH EINVOICE BILL WE NEED TO 2 API COUNTS (1 FOR TOKEN AND ANOTHER FOR EINVOICE)
            If CMPEINVOICECOUNTER = 0 Then
                MsgBox("E-Invoice Bill Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'GET USED EINVOICECOUNTER
            Dim USEDEINVOICECOUNTER As Integer = 0
            DT = OBJCMN.SEARCH("COUNT(COUNTERID) AS EINVOICECOUNT", "", "EINVOICEENTRY", " AND CMPID =" & CmpId)
            If DT.Rows.Count > 0 Then USEDEINVOICECOUNTER = Val(DT.Rows(0).Item("EINVOICECOUNT"))

            'IF COUNTERS ARE FINISJED
            If CMPEINVOICECOUNTER - USEDEINVOICECOUNTER <= 0 Then
                MsgBox("E-Invoice Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF DATE HAS EXPIRED
            If Now.Date > EINVOICEEXPDATE Then
                MsgBox("E-Invoice Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF BALANCECOUNTERS ARE .10 THEN INTIMATE
            If CMPEINVOICECOUNTER - USEDEINVOICECOUNTER < Format((CMPEINVOICECOUNTER * 0.1), "0") Then
                MsgBox("Only " & (CMPEINVOICECOUNTER - USEDEINVOICECOUNTER) & " API's Left, Kindly contact Nakoda Infotech for Renewal of E-Invoice Package", MsgBoxStyle.Critical)
            End If


            'FOR GENERATING EINVOICE BILL WE NEED TO FIRST GENERATE THE TOKEN
            'THIS IS FOR SANDBOX TEST
            'Dim URL As New Uri("http://gstsandbox.charteredinfo.com/eivital/dec/v1.04/auth?aspid=1602611918&password=infosys123&Gstin=34AACCC1596Q002&user_name=TaxProEnvPON&eInvPwd=abc34*")
            Dim URL As New Uri("https://einvapi.charteredinfo.com/eivital/dec/v1.04/auth?aspid=1602611918&password=infosys123&Gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&eInvPwd=" & CMPEWBPASS)

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim REQUEST As WebRequest
            Dim RESPONSE As WebResponse
            REQUEST = WebRequest.CreateDefault(URL)

            REQUEST.Method = "GET"
            Try
                RESPONSE = REQUEST.GetResponse()
            Catch ex As WebException
                RESPONSE = ex.Response
            End Try
            Dim READER As StreamReader = New StreamReader(RESPONSE.GetResponseStream())
            Dim REQUESTEDTEXT As String = READER.ReadToEnd()

            'IF STATUS IS NOT 1 THEN TOKEN IS NOT GENERATED
            Dim STARTPOS As Integer = 0
            Dim TEMPSTATUS As String = ""
            Dim TOKEN As String = ""
            Dim ENDPOS As Integer = 0

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 2
            TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
            If TEMPSTATUS = "1" Then TEMPSTATUS = "SUCCESS" Else TEMPSTATUS = "FAILED"




            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("authtoken") + Len("AUTHTOKEN") + 3
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS) - 1
            TOKEN = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            'ADD DATA IN EINVOICEENTRY
            'DONT ADD IN EINVOICEENTRY, DONE BY GULKIT, IF FAILED THEN ADD
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
            'IF STATUS IS FAILED THEN ERROR MESSAGE
            If TEMPSTATUS = "FAILED" Then
                MsgBox("Unable to create E-Invoice", MsgBoxStyle.Critical)
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End If

            Dim j As String = ""
            Dim PRINTINITIALS As String = ""

            'GENERATING EINVOICE
            'FOR SANBOX TEST
            'Dim FURL As New Uri("http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice?aspid=1602611918&password=infosys123&Gstin=34AACCC1596Q002&AuthToken=" & TOKEN & "&user_name=TaxProEnvPON&QrCodeSize=250")
            Dim FURL As New Uri("https://einvapi.charteredinfo.com/eicore/dec/v1.03/Invoice?aspid=1602611918&password=infosys123&Gstin=" & CMPGSTIN & "&AuthToken=" & TOKEN & "&user_name=" & CMPEWBUSER & "&QrCodeSize=250")
            REQUEST = WebRequest.CreateDefault(FURL)
            REQUEST.Method = "POST"
            Try
                REQUEST.ContentType = "application/json"



                j = "{"
                j = j & """Version"": ""1.1"","
                j = j & """TranDtls"": {"
                j = j & """TaxSch"":""GST"","
                If CHKEXPORTGST.Checked = True And CHKOVERSEAS.Checked = True Then j = j & """SupTyp"":""EXPWP""," Else j = j & """SupTyp"":""B2B"","
                j = j & """RegRev"":""N"","
                j = j & """IgstOnIntra"":""N""},"



                'WE NEED TO FETCH INITIALS INSTEAD OF BILLNO
                Dim DTINI As DataTable = OBJCMN.SEARCH("INVOICE_PRINTINITIALS AS PRINTINITIALS", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId)
                PRINTINITIALS = DTINI.Rows(0).Item("PRINTINITIALS")

                j = j & """DocDtls"": {"
                j = j & """Typ"":""INV"","
                j = j & """No"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """Dt"":""" & INVOICEDATE.Text & """" & "},"


                'For WORKING ON SANDBOX
                'CMPGSTIN = "34AACCC1596Q002"
                'CMPPINCODE = "605001"
                'CMPSTATECODE = "34"


                j = j & """SellerDtls"": {"
                j = j & """Gstin"":""" & CMPGSTIN & """" & ","
                j = j & """LglNm"":""" & CmpName & """" & ","
                j = j & """TrdNm"":""" & CmpName & """" & ","
                j = j & """Addr1"":""" & TEMPCMPADD1.Trim.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & TEMPCMPADD2.Trim.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & CMPCITYNAME & """" & "," 'CMBFROMCITY.Text.Trim & """" & ","
                j = j & """Pin"":" & CMPPINCODE & "" & ","
                j = j & """Stcd"":""" & CMPSTATECODE & """" & "},"

                If PARTYADD1 = "" Then PARTYADD1 = PARTYSTATENAME
                If PARTYADD2 = "" Then PARTYADD2 = PARTYSTATENAME

                j = j & """BuyerDtls"": {"
                j = j & """Gstin"":""" & PARTYGSTIN & """" & ","
                j = j & """LglNm"":""" & cmbname.Text.Trim & """" & ","
                j = j & """TrdNm"":""" & cmbname.Text.Trim & """" & ","
                j = j & """Pos"":""" & PARTYSTATECODE & """" & ","
                j = j & """Addr1"":""" & PARTYADD1.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & PARTYADD2.Replace(vbCrLf, " ") & """" & ","
                'If ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then j = j & """Loc"":""" & KOTHARIPLACE & """" & "," Else j = j & """Loc"":""" & CMBTOCITY.Text.Trim & """" & ","
                'j = j & """Loc"":""" & CMBTOCITY.Text.Trim & """" & ","

                If PARTYCITY = "" Then PARTYCITY = CMBTOCITY.Text.Trim

                j = j & """Loc"":""" & PARTYCITY & """" & ","
                j = j & """Pin"":" & PARTYPINCODE & "" & ","
                j = j & """Stcd"":""" & PARTYSTATECODE & """" & "},"


                j = j & """DispDtls"": {"
                j = j & """Nm"":""" & DISPATCHFROM & """" & ","
                j = j & """Addr1"":""" & TEMPCMPDISPATCHADD1.Trim.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & TEMPCMPADD2.Trim.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & CMBFROMCITY.Text.Trim & """" & ","
                j = j & """Pin"":" & DISPATCHFROMPINCODE & "" & ","
                j = j & """Stcd"":""" & DISPATCHFROMSTATECODE & """" & "},"

                j = j & """ShipDtls"": {"
                If SHIPTOGSTIN <> "" Then j = j & """Gstin"":""" & SHIPTOGSTIN & """" & ","
                j = j & """LglNm"":""" & CMBPACKING.Text.Trim & """" & ","
                j = j & """TrdNm"":""" & CMBPACKING.Text.Trim & """" & ","
                If SHIPTOADD1 = "" Then j = j & """Addr1"":""" & PARTYADD1.Replace(vbCrLf, " ") & """" & "," Else j = j & """Addr1"":""" & SHIPTOADD1.Replace(vbCrLf, " ") & """" & ","
                If SHIPTOADD2 = "" Then SHIPTOADD2 = " ADDRESS2 "
                j = j & """Addr2"":""" & SHIPTOADD2 & """" & ","
                j = j & """Loc"":""" & CMBTOCITY.Text.Trim & """" & ","
                If SHIPTOPINCODE = "" Then j = j & """Pin"":" & PARTYPINCODE & "" & "," Else j = j & """Pin"":" & SHIPTOPINCODE & "" & ","
                j = j & """Stcd"":""" & SHIPTOSTATECODE & """" & "},"


                j = j & """ItemList"":[{"



                Dim TEMPLINEDISC As Double = 0
                Dim TEMPLINETAXABLEAMT As Double = 0
                Dim TEMPLINECGSTAMT As Double = 0
                Dim TEMPLINESGSTAMT As Double = 0
                Dim TEMPLINEIGSTAMT As Double = 0
                Dim TEMPLINEGRIDTOTALAMT As Double = 0
                Dim TEMPLINECHARGES As Double = 0
                Dim TEMPRATE As Double = 0
                If Val(TXTCHARGES.Text.Trim) < 0 Then
                    If GRIDINVOICE.Rows(0).Cells(GPER.Index).Value = "Pcs" Then TEMPLINEDISC = Val(TXTCHARGES.Text.Trim) / Val(lbltotalpcs.Text.Trim) Else TEMPLINEDISC = Val(TXTCHARGES.Text.Trim) / Val(txtbillamt.Text.Trim)
                Else
                    If GRIDINVOICE.Rows(0).Cells(GPER.Index).Value = "Pcs" Then TEMPRATE = Val(TXTCHARGES.Text.Trim) / Val(lbltotalpcs.Text.Trim) Else TEMPRATE = Val(TXTCHARGES.Text.Trim) / Val(lbltotalmtrs.Text.Trim)
                End If


                Dim TEMPTOTALAMT As Double = 0
                Dim TEMPTOTALDISC As Double = 0
                Dim TEMPTOTALTAXABLEAMT As Double = 0
                Dim TEMPTOTALCGSTAMT As Double = 0
                Dim TEMPTOTALSGSTAMT As Double = 0
                Dim TEMPTOTALIGSTAMT As Double = 0
                Dim TEMPGTOTALAMT As Double = 0


                'WE NEED TO FETCH ALL ITEMS HERE
                'FETCH FROM DESC TABLE 
                DT = OBJCMN.Execute_Any_String(" SELECT ISNULL(INVOICEMASTER_DESC.INVOICE_SRNO,0) AS SRNO, ISNULL(ITEMMASTER.item_name,'') AS ITEMNAME, ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGST, ISNULL(HSN_SGST,0) AS SGST, ISNULL(HSN_IGST,0) AS IGST, ISNULL(INVOICEMASTER_DESC.INVOICE_PCS,0) AS PCS, ISNULL(INVOICEMASTER_DESC.INVOICE_MTRS,0) AS MTRS, ISNULL(INVOICEMASTER_DESC.INVOICE_PER,'Mtrs') AS PER, ISNULL(INVOICEMASTER_DESC.INVOICE_RATE,0) AS RATE, ISNULL(INVOICEMASTER_DESC.INVOICE_AMOUNT,0) AS TOTALAMT, (ISNULL(INVOICEMASTER_DESC.INVOICE_DISCAMT,0) + ISNULL(INVOICEMASTER_DESC.INVOICE_SPDISCAMT,0)) AS LINEDISC, ISNULL(INVOICEMASTER_DESC.INVOICE_OTHERAMT,0) AS LINEOTHERAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_TAXABLEAMT,0) AS LINETAXABLEAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_CGSTAMT,0) AS LINECGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_SGSTAMT,0) AS LINESGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_IGSTAMT,0) AS LINEIGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_GRIDTOTAL,0) AS LINEGRIDTOTAL, ISNULL(HSN_TYPE,'Goods') HSNTYPE, ISNULL(INVOICEMASTER_DESC.INVOICE_CGSTPER,0) AS LINECGSTPER, ISNULL(INVOICEMASTER_DESC.INVOICE_SGSTPER,0) AS LINESGSTPER, ISNULL(INVOICEMASTER_DESC.INVOICE_IGSTPER,0) AS LINEIGSTPER FROM INVOICEMASTER INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN HSNMASTER ON HSN_ID = INVOICE_HSNCODEID INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTER_ID WHERE INVOICEMASTER.INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' and INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER_DESC.INVOICE_SRNO", "", "")
                Dim CURRROW As Integer = 0
                For Each DTROW As DataRow In DT.Rows

                    If GRIDINVOICE.Rows(0).Cells(GPER.Index).Value = "Pcs" Then TEMPLINECHARGES = Format(Val(TEMPLINEDISC) * Val(DTROW("PCS")), "0.0000") Else TEMPLINECHARGES = Format(Val(TEMPLINEDISC) * Val(DTROW("TOTALAMT")), "0.00")
                    TEMPLINETAXABLEAMT = Format(Val(DTROW("TOTALAMT")) + Val(TEMPLINECHARGES), "0.00")
                    TEMPLINECGSTAMT = Format(Val(TXTCGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    TEMPLINESGSTAMT = Format(Val(TXTSGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    TEMPLINEIGSTAMT = Format(Val(TXTIGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    TEMPLINEGRIDTOTALAMT = Format(Val(TEMPLINETAXABLEAMT + TEMPLINECGSTAMT + TEMPLINESGSTAMT + TEMPLINEIGSTAMT), "0.00")

                    If CURRROW > 0 Then j = j & ", {"
                    j = j & """SlNo"": """ & DTROW("SRNO") & """" & ","
                    j = j & """PrdDesc"":""" & DTROW("ITEMNAME") & """" & ","
                    If DTROW("HSNTYPE") = "Goods" Then j = j & """IsServc"":""" & "N" & """" & "," Else j = j & """IsServc"":""" & "Y" & """" & ","
                    j = j & """HsnCd"":""" & DTROW("HSNCODE") & """" & ","
                    j = j & """Barcde"":""REC9999"","
                    If DTROW("PER") = "Pcs" Then j = j & """Qty"":" & Val(DTROW("PCS")) & "" & "," Else j = j & """Qty"":" & Val(DTROW("MTRS")) & "" & ","
                    j = j & """FreeQty"":" & "0" & "" & ","
                    j = j & """Unit"":""" & "MTR" & """" & ","


                    Dim DTHSN As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER_DESC.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER_DESC.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER_DESC.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER_DESC.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN HSNMASTER_DESC ON HSNMASTER.HSN_ID = HSNMASTER_DESC.HSN_ID INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND HSNMASTER_DESC.HSN_WEFDATE <= '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND ITEMMASTER.ITEM_NAME= '" & DTROW("ITEMNAME") & "' AND HSNMASTER.HSN_YEARID=" & YearId & " ORDER BY HSNMASTER_DESC.HSN_WEFDATE DESC")


                    If (Val(TXTCHARGES.Text.Trim) <= 0 And INVOICESCREENTYPE = "TOTAL GST") Or (Val(DTROW("LINEOTHERAMT")) - Val(DTROW("LINEDISC")) <= 0 And INVOICESCREENTYPE = "LINE GST") Then

                        j = j & """UnitPrice"":" & Val(DTROW("RATE")) & "" & ","
                        j = j & """TotAmt"":" & Format(Val(DTROW("TOTALAMT")), "0.00") & "" & ","

                        If INVOICESCREENTYPE = "LINE GST" Then
                            'If Val(DTROW("LINEDISC")) < 0 Then j = j & """Discount"":" & Val(DTROW("LINEDISC")) * -1 & "" & "," Else j = j & """Discount"":" & Val(DTROW("LINEDISC")) & "" & ","
                            If Val(DTROW("LINEOTHERAMT")) - Val(DTROW("LINEDISC")) < 0 Then j = j & """Discount"":" & Format(Val(DTROW("LINEOTHERAMT")) - Val(DTROW("LINEDISC")), "0.00") * -1 & "" & "," Else j = j & """Discount"":" & Format(Val(DTROW("LINEOTHERAMT")) - Val(DTROW("LINEDISC")), "0.00") & "" & ","
                        Else
                            If Val(TEMPLINECHARGES) < 0 Then j = j & """Discount"":" & Format(Val(TEMPLINECHARGES), "0.00") * -1 & "" & "," Else j = j & """Discount"":" & Format(Val(TEMPLINECHARGES), "0.00") & "" & ","
                        End If
                        j = j & """PreTaxVal"":" & "1" & "" & ","
                        If INVOICESCREENTYPE = "LINE GST" Then
                            j = j & """AssAmt"":" & Val(DTROW("LINETAXABLEAMT")) & "" & ","
                            j = j & """GstRt"":" & Val(DTROW("LINECGSTPER")) + Val(DTROW("LINESGSTPER")) + Val(DTROW("LINEIGSTPER")) & "" & ","
                        Else
                            j = j & """AssAmt"":" & Val(TEMPLINETAXABLEAMT) & "" & ","
                            If CHKEXPORTGST.CheckState = CheckState.Unchecked Then j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("IGSTPER")) & "" & "," Else j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("EXPIGSTPER")) & "" & ","
                        End If

                        If INVOICESCREENTYPE = "LINE GST" Then
                            j = j & """IgstAmt"":" & Val(DTROW("LINEIGSTAMT")) & "" & ","
                            j = j & """CgstAmt"":" & Val(DTROW("LINECGSTAMT")) & "" & ","
                            j = j & """SgstAmt"":" & Val(DTROW("LINESGSTAMT")) & "" & ","
                        Else
                            j = j & """IgstAmt"":" & Val(TEMPLINEIGSTAMT) & "" & ","
                            j = j & """CgstAmt"":" & Val(TEMPLINECGSTAMT) & "" & ","
                            j = j & """SgstAmt"":" & Val(TEMPLINESGSTAMT) & "" & ","
                        End If

                        j = j & """CesRt"":" & "0" & "" & ","
                        j = j & """CesAmt"":" & "0" & "" & ","
                        j = j & """CesNonAdvlAmt"":" & "0" & "" & ","
                        j = j & """StateCesRt"":" & "0" & "" & ","
                        j = j & """StateCesAmt"":" & "0" & "" & ","
                        j = j & """StateCesNonAdvlAmt"":" & "0" & "" & ","
                        j = j & """OthChrg"":" & "0" & "" & ","

                        If INVOICESCREENTYPE = "LINE GST" Then j = j & """TotItemVal"":" & Val(DTROW("LINEGRIDTOTAL")) & "" & "," Else j = j & """TotItemVal"":" & Val(TEMPLINEGRIDTOTALAMT) & "" & ","
                        j = j & """OrdLineRef"":"" "","
                        j = j & """OrgCntry"":""IN"","
                        j = j & """PrdSlNo"":""123"","

                        j = j & """BchDtls"": {"
                        j = j & """Nm"":""123"","
                        j = j & """Expdt"":""" & INVOICEDATE.Text & """" & ","
                        j = j & """wrDt"":""" & INVOICEDATE.Text & """" & "},"

                        j = j & """AttribDtls"": [{"
                        j = j & """Nm"":""" & DTROW("ITEMNAME") & """" & ","
                        j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"

                    Else

                        If INVOICESCREENTYPE = "LINE GST" Then
                            If DTROW("PER") = "Pcs" Then TEMPRATE = (Val(DTROW("LINEOTHERAMT")) - Val(DTROW("LINEDISC"))) / Val(DTROW("PCS")) Else TEMPRATE = (Val(DTROW("LINEOTHERAMT")) - Val(DTROW("LINEDISC"))) / Val(DTROW("MTRS"))
                        End If

                        j = j & """UnitPrice"":" & Format(Val(DTROW("RATE")) + TEMPRATE, "0.00") & "" & ","
                        If DTROW("PER") = "Pcs" Then TEMPLINETAXABLEAMT = Format(Val(Val(DTROW("RATE")) + TEMPRATE) * Val(DTROW("PCS")), "0.00") Else TEMPLINETAXABLEAMT = Format(Val(Val(DTROW("RATE")) + TEMPRATE) * Val(DTROW("MTRS")), "0.00")

                        TEMPLINECGSTAMT = Format(Val(TXTCGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                        TEMPLINESGSTAMT = Format(Val(TXTSGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                        TEMPLINEIGSTAMT = Format(Val(TXTIGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                        TEMPLINEGRIDTOTALAMT = Format(Val(TEMPLINETAXABLEAMT + TEMPLINECGSTAMT + TEMPLINESGSTAMT + TEMPLINEIGSTAMT), "0.00")

                        j = j & """TotAmt"":" & Val(TEMPLINETAXABLEAMT) & "" & ","
                        j = j & """Discount"":" & "0" & "" & ","
                        j = j & """PreTaxVal"":" & "1" & "" & ","

                        If INVOICESCREENTYPE = "LINE GST" Then
                            j = j & """AssAmt"":" & Val(DTROW("LINETAXABLEAMT")) & "" & ","
                            j = j & """GstRt"":" & Val(DTROW("LINECGSTPER")) + Val(DTROW("LINESGSTPER")) + Val(DTROW("LINEIGSTPER")) & "" & ","
                            j = j & """IgstAmt"":" & Val(DTROW("LINEIGSTAMT")) & "" & ","
                            j = j & """CgstAmt"":" & Val(DTROW("LINECGSTAMT")) & "" & ","
                            j = j & """SgstAmt"":" & Val(DTROW("LINESGSTAMT")) & "" & ","
                        Else
                            j = j & """AssAmt"":" & Val(TEMPLINETAXABLEAMT) & "" & ","
                            If CHKEXPORTGST.CheckState = CheckState.Unchecked Then j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("IGSTPER")) & "" & "," Else j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("EXPIGSTPER")) & "" & ","
                            j = j & """IgstAmt"":" & Val(TEMPLINEIGSTAMT) & "" & ","
                            j = j & """CgstAmt"":" & Val(TEMPLINECGSTAMT) & "" & ","
                            j = j & """SgstAmt"":" & Val(TEMPLINESGSTAMT) & "" & ","
                        End If


                        j = j & """CesRt"":" & "0" & "" & ","
                        j = j & """CesAmt"":" & "0" & "" & ","
                        j = j & """CesNonAdvlAmt"":" & "0" & "" & ","
                        j = j & """StateCesRt"":" & "0" & "" & ","
                        j = j & """StateCesAmt"":" & "0" & "" & ","
                        j = j & """StateCesNonAdvlAmt"":" & "0" & "" & ","
                        j = j & """OthChrg"":" & "0" & "" & ","
                        If INVOICESCREENTYPE = "LINE GST" Then j = j & """TotItemVal"":" & Val(DTROW("LINEGRIDTOTAL")) & "" & "," Else j = j & """TotItemVal"":" & Val(TEMPLINEGRIDTOTALAMT) & "" & ","
                        j = j & """OrdLineRef"":"" "","
                        j = j & """OrgCntry"":""IN"","
                        j = j & """PrdSlNo"":""123"","

                        j = j & """BchDtls"": {"
                        j = j & """Nm"":""123"","
                        j = j & """Expdt"":""" & INVOICEDATE.Text & """" & ","
                        j = j & """wrDt"":""" & INVOICEDATE.Text & """" & "},"

                        j = j & """AttribDtls"": [{"
                        j = j & """Nm"":""" & DTROW("ITEMNAME") & """" & ","
                        If INVOICESCREENTYPE = "LINE GST" Then j = j & """Val"":""" & Val(DTROW("LINEGRIDTOTAL")) & """" & "}]" Else j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"
                        'j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"
                    End If



                    j = j & " }"
                    CURRROW += 1


                    'THESE VARIABLES ARE JUST FOR TESTING PURPOSE
                    TEMPTOTALAMT += Val(DTROW("TOTALAMT"))
                    TEMPTOTALDISC += Val(TEMPLINECHARGES)
                    TEMPTOTALTAXABLEAMT += Val(TEMPLINETAXABLEAMT)
                    TEMPTOTALCGSTAMT += Val(TEMPLINECGSTAMT)
                    TEMPTOTALSGSTAMT += Val(TEMPLINESGSTAMT)
                    TEMPTOTALIGSTAMT += Val(TEMPLINEIGSTAMT)
                    TEMPGTOTALAMT += Val(TEMPLINEGRIDTOTALAMT)


                Next

                j = j & " ],"



                j = j & """ValDtls"": {"
                If INVOICESCREENTYPE = "TOTAL GST" Then
                    j = j & """AssVal"":" & Val(TXTSUBTOTAL.Text.Trim) & "" & ","
                    j = j & """CgstVal"":" & Val(TXTCGSTAMT1.Text.Trim) & "" & ","
                    j = j & """SgstVal"":" & Val(TXTSGSTAMT1.Text.Trim) & "" & ","
                    j = j & """IgstVal"":" & Val(TXTIGSTAMT1.Text.Trim) & "" & ","
                Else
                    j = j & """AssVal"":" & Val(LBLTOTALTAXABLEAMT.Text.Trim) & "" & ","
                    j = j & """CgstVal"":" & Val(LBLTOTALCGSTAMT.Text.Trim) & "" & ","
                    j = j & """SgstVal"":" & Val(LBLTOTALSGSTAMT.Text.Trim) & "" & ","
                    j = j & """IgstVal"":" & Val(LBLTOTALIGSTAMT.Text.Trim) & "" & ","
                End If

                j = j & """CesVal"":" & "0" & "" & ","
                j = j & """StCesVal"":" & "0" & "" & ","
                j = j & """Discount"":" & "0" & "" & ","
                j = j & """OthChrg"":" & Val(TXTTCSAMT.Text.Trim) & "" & ","
                j = j & """RndOffAmt"":" & Val(txtroundoff.Text.Trim) & "" & ","
                j = j & """TotInvVal"":" & Val(txtgrandtotal.Text.Trim) & "" & ","
                j = j & """TotInvValFc"":" & "0" & "" & "},"


                j = j & """PayDtls"": {"
                j = j & """Nm"":"" "","
                j = j & """Accdet"":"" "","
                j = j & """Mode"":""Credit"","
                j = j & """Fininsbr"":"" "","
                j = j & """Payterm"":"" "","
                j = j & """Payinstr"":"" "","
                j = j & """Crtrn"":"" "","
                j = j & """Dirdr"":"" "","
                j = j & """Crday"":" & Val(TXTCRDAYS.Text.Trim) & "" & ","
                j = j & """Paidamt"":" & "0" & "" & ","
                j = j & """Paymtdue"":" & Val(txtgrandtotal.Text.Trim) & "" & "},"


                j = j & """RefDtls"": {"
                j = j & """InvRm"":""TEST"","
                j = j & """DocPerdDtls"": {"
                j = j & """InvStDt"":""" & INVOICEDATE.Text & """" & ","
                j = j & """InvEndDt"":""" & INVOICEDATE.Text & """" & "},"

                j = j & """PrecDocDtls"": [{"
                j = j & """InvNo"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """InvDt"":""" & INVOICEDATE.Text & """" & ","
                j = j & """OthRefNo"":"" ""}],"

                j = j & """ContrDtls"": [{"
                j = j & """RecAdvRefr"":"" "","
                j = j & """RecAdvDt"":""" & INVOICEDATE.Text & """" & ","
                j = j & """Tendrefr"":"" "","
                j = j & """Contrrefr"":"" "","
                j = j & """Extrefr"":"" "","
                j = j & """Projrefr"":"" "","
                j = j & """Porefr"":"" "","
                j = j & """PoRefDt"":""" & INVOICEDATE.Text & """" & "}]"
                j = j & "},"




                j = j & """AddlDocDtls"": [{"
                j = j & """Url"":""https://einv-apisandbox.nic.in"","
                j = j & """Docs"":""INVOICE"","
                j = j & """Info"":""INVOICE""}],"

                j = j & """TransDocNo"":""" & txtlrno.Text.Trim & """" & ","



                j = j & """ExpDtls"": {"
                j = j & """ShipBNo"":"" "","
                j = j & """ShipBDt"":""" & INVOICEDATE.Text & """" & ","
                j = j & """Port"":""INBOM1"","
                j = j & """RefClm"":""N"","
                j = j & """ForCur"":""AED"","
                j = j & """CntCode"":""AE""}"



                'THIS CODE IS WRITTEN COZ WHEN BILLTO AND SHIPTO ARE IN THE SAME PINCODE THEN WE HAVE TO PASS MINIMUM 10 KMS
                'OR ELSE IT WILL GIVE ERROR
                If DISPATCHFROMPINCODE = PARTYPINCODE Then PARTYKMS = 10

                'WE HAVE REMOVED CREATING EWAY DIRECTLY FORM EINVOICE
                'USER HAVE TO MANUALLY CREATE EWAY SEPERATELY
                'If TXTVEHICLENO.Text.Trim <> "" Then
                '    j = j & ","
                '    j = j & """EwbDtls"": {"
                '    j = j & """TransId"":""" & TRANSGSTIN & """" & ","
                '    j = j & """TransName"":""" & cmbtrans.Text.Trim & """" & ","
                '    j = j & """Distance"":""" & PARTYKMS & """" & ","
                '    If LRDATE.Text <> "__/__/____" Then j = j & """TransDocDt"":""" & LRDATE.Text & """" & "," Else j = j & """TransDocDt"":"""","
                '    j = j & """VehNo"":""" & TXTVEHICLENO.Text.Trim & """" & ","
                '    j = j & """VehType"":""" & "R" & """" & ","
                '    j = j & """TransMode"":""1""" & "}"
                'End If

                j = j & "}"


                Dim stream As Stream = REQUEST.GetRequestStream()
                Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(j)
                stream.Write(buffer, 0, buffer.Length)

                'POST request absenden
                RESPONSE = REQUEST.GetResponse()



            Catch ex As WebException
                'RESPONSE = ex.Response
                'MsgBox("Error While Generating EWB, Please check the Data Properly")
                ''ADD DATA IN EINVOICEENTRY
                'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

                RESPONSE = ex.Response
                READER = New StreamReader(RESPONSE.GetResponseStream())
                REQUESTEDTEXT = READER.ReadToEnd()
                GoTo ERRORMESSAGE
            End Try

            READER = New StreamReader(RESPONSE.GetResponseStream())
            REQUESTEDTEXT = READER.ReadToEnd()


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 3
            TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
            If TEMPSTATUS = "1" Then
                TEMPSTATUS = "SUCCESS"
                MsgBox("E-Invoice Generated Successfully ")

            Else

ERRORMESSAGE:
                TEMPSTATUS = "FAILED"

                Dim ERRORMSG As String = ""
                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ErrorMessage") + Len("ErrorMessage") + 5
                ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("""", STARTPOS) - 2
                ERRORMSG = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

                'ADD DATA IN EINVOICEENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

                MsgBox("Error While Generating E-Invoice, " & REQUESTEDTEXT)

                Exit Sub
            End If


            Dim IRNNO As String = ""
            Dim ACKNO As String = ""
            Dim ADATE As String = ""


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ackno") + Len("ACKNO") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            ACKNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            TXTACKNO.Text = ACKNO


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ackdt") + Len("ACKDT") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            ADATE = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            ACKDATE.Value = ADATE

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("irn") + Len("IRN") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            IRNNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            TXTIRNNO.Text = IRNNO


            'WE NEED TO UPDATE THIS IRNNO IN DATABASE ALSO
            DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_IRNNO = '" & TXTIRNNO.Text.Trim & "', INVOICE_ACKNO = '" & TXTACKNO.Text.Trim & "', INVOICE_ACKDATE = '" & Format(ACKDATE.Value.Date, "MM/dd/yyyy") & "' FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

            'ADD DATA IN EINVOICEENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','" & TEMPSTATUS & "', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ADD DATA IN EINVOICEENTRY FOR QRCODE
            If TEMPSTATUS = "SUCCESS" Then

                ''GET SIGNED QRCODE
                Dim req As New RestRequest
                req.AddParameter("application/json", j, RestSharp.ParameterType.RequestBody)
                'Dim client As New RestClient("http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=34AACCC1596Q002&user_name=TaxProEnvPON&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim client As New RestClient("https://einvapi.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim res As IRestResponse = Await client.ExecuteTaskAsync(req)
                Dim respPl = New RespPl()
                respPl = JsonConvert.DeserializeObject(Of RespPl)(res.Content)
                Dim respPlGenIRNDec As New RespPlGenIRNDec()
                respPlGenIRNDec = JsonConvert.DeserializeObject(Of RespPlGenIRNDec)(respPl.Data)
                'MsgBox(respPlGenIRNDec.Irn)
                Dim qrImg As Byte() = Convert.FromBase64String(respPlGenIRNDec.QrCodeImage)
                Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(Bitmap))
                Dim bitmap1 As Bitmap = CType(tc.ConvertFrom(qrImg), Bitmap)

                'GET REGINITIALS AS SAVE WITH IT
                Dim TEMPREG As DataTable = OBJCMN.Execute_Any_String("SELECT REGISTER_INITIALS AS INITIALS FROM REGISTERMASTER WHERE REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTER_TYPE ='SALE' AND REGISTER_YEARID = " & YearId, "", "")
                bitmap1.Save(Application.StartupPath & "\" & TEMPREG.Rows(0).Item("INITIALS") & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png")
                PBQRCODE.ImageLocation = Application.StartupPath & "\" & TEMPREG.Rows(0).Item("INITIALS") & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png"
                PBQRCODE.Refresh()

                If PBQRCODE.Image IsNot Nothing Then
                    Dim OBJINVOICE As New ClsInvoiceMaster
                    Dim MS As New IO.MemoryStream
                    PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    OBJINVOICE.alParaval.Add(TXTINVOICENO.Text.Trim)
                    OBJINVOICE.alParaval.Add(cmbregister.Text.Trim)
                    OBJINVOICE.alParaval.Add(MS.ToArray)
                    OBJINVOICE.alParaval.Add(YearId)
                    Dim INTRES As Integer = OBJINVOICE.SAVEQRCODE()
                End If

                'DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_QRCODE = (SELECT * FROM OPENROWSET(BULK '" & Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png',SINGLE_BLOB) AS IMG) FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")


                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','QRCODE SUCCESS', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','QRCODE SUCCESS1', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

            End If




            'STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("QrCodeImage\", 0) + Len("QrCodeImage\") + 5
            'ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("""", STARTPOS)
            ''Dim QRSTREAM As New MemoryStream
            ''Dim bmp As New Bitmap(QRSTREAM)
            ''bmp.Save(QRSTREAM, Drawing.Imaging.ImageFormat.Bmp)
            ''QRSTREAM.Position = STARTPOS
            ''Dim data As Byte()
            ''QRSTREAM.Read(data, STARTPOS, STARTPOS - ENDPOS)

            'Dim bytes() As Byte
            'Dim ImageInStringFormat As String = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            'Dim MS As System.IO.MemoryStream
            'Dim NewImage As Bitmap

            'Dim nbyte() As Byte = System.Text.Encoding.UTF8.GetBytes(ImageInStringFormat)
            'Dim BASE64STRING As String = Convert.ToBase64String(nbyte)

            'bytes = Convert.FromBase64String(BASE64STRING)
            'NewImage = BytesToBitmap(bytes)
            'MS = New System.IO.MemoryStream(bytes)
            'MS.Write(bytes, 0, bytes.Length)
            'NewImage.Save(MS, Drawing.Imaging.ImageFormat.Bmp)    ' = System.Drawing.Image.FromStream(MS, True)
            'NewImage.Save("d:\qrcode.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

            'IRNNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            ''ADD data IN EINVOICEENTRY
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','" & TEMPSTATUS & "', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub GENERATEJOBEINV()
        Try
            If ALLOWEINVOICE = False Then Exit Sub
            If cmbname.Text.Trim = "" Then Exit Sub

            If Val(LBLTOTALCGSTAMT.Text.Trim) = 0 And Val(TXTCGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALSGSTAMT.Text.Trim) = 0 And Val(TXTSGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALIGSTAMT.Text.Trim) = 0 And Val(TXTIGSTAMT1.Text.Trim) = 0 And CHKOVERSEAS.CheckState = CheckState.Unchecked Then Exit Sub


            'If txtlrno.Text.Trim <> "" AndAlso LRDATE.Text <> "__/__/____" Then
            '    If Convert.ToDateTime(LRDATE.Text).Date < Convert.ToDateTime(INVOICEDATE.Text).Date Then
            '        MsgBox("LR Date cannot be Before Invoice Date", MsgBoxStyle.Critical)
            '        Exit Sub
            '    End If
            'End If

            If CMBFROMCITY.Text.Trim = "" Then
                MsgBox("Enter From City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If CMBTOCITY.Text.Trim = "" Then
                MsgBox("Enter to City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Generate E-Invoice?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If TXTIRNNO.Text.Trim <> "" Then
                MsgBox("E-Invoice Already Generated", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'BEFORE GENERATING EWAY BILL WE NEED TO VALIDATE WHETHER ALL THE DATA ARE PRESENT OR NOT
            'IF DATA IS NOT PRESENT THEN VALIDATE
            'DATA TO BE CHECKED 
            '   1)CMPEWBUSER | CMPEWBPASS | CMPGSTIN | CMPPINCODE | CMPCITY | CMPSTATE | 
            '   2)PARTYGSTIN | PARTYCITY | PARTYPINCODE | PARTYSTATE | PARTYSTATECODE | PARTYKMS
            '   3)CGST OR SGST OR IGST (ALWAYS USE MTR IN QTYUNIT)
            If CMPEWBUSER = "" Or CMPEWBPASS = "" Or CMPGSTIN = "" Or CMPPINCODE = "" Or CMPCITYNAME = "" Or CMPSTATENAME = "" Then
                MsgBox(" Company Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim TEMPCMPADD1 As String = ""
            Dim TEMPCMPADD2 As String = ""
            Dim TEMPCMPDISPATCHADD1 As String = ""
            Dim PARTYGSTIN As String = ""
            Dim PARTYPINCODE As String = ""
            Dim PARTYSTATECODE As String = ""
            Dim PARTYSTATENAME As String = ""
            Dim SHIPTOGSTIN As String = ""
            Dim SHIPTOSTATECODE As String = ""
            Dim SHIPTOSTATENAME As String = ""
            Dim SHIPTOPINCODE As String = ""
            Dim PARTYKMS As Double = 0
            Dim PARTYADD1 As String = ""
            Dim PARTYADD2 As String = ""
            Dim SHIPTOADD1 As String = ""
            Dim SHIPTOADD2 As String = ""
            Dim TRANSGSTIN As String = ""
            'Dim KOTHARIPLACE As String = ""  'THIS VARIABLE IS USED TO FETCH RANGE COLUMN ONLY FOR KOTHARI, THEY WILL ENTER FULL SHIPTO ADDRESS THERE
            Dim DISPATCHFROM As String = ""
            Dim DISPATCHFROMGSTIN As String = ""
            Dim DISPATCHFROMPINCODE As String = ""
            Dim DISPATCHFROMSTATECODE As String = ""
            Dim DISPATCHFROMSTATENAME As String = ""
            Dim DISPATCHFROMKMS As Double = 0
            Dim DISPATCHFROMADD1 As String = ""
            Dim DISPATCHFROMADD2 As String = ""


            Dim OBJCMN As New ClsCommon
            'CMP ADDRESS DETAILS
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(CMP_ADD1, '') AS ADD1, ISNULL(CMP_ADD2,'') AS ADD2, ISNULL(CMP_DISPATCHFROM, '') AS DISPATCHADD ", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
            TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
            TEMPCMPADD2 = DT.Rows(0).Item("ADD2")
            TEMPCMPDISPATCHADD1 = DT.Rows(0).Item("DISPATCHADD")
            DISPATCHFROM = CmpName
            DISPATCHFROMGSTIN = CMPGSTIN
            DISPATCHFROMPINCODE = CMPPINCODE
            DISPATCHFROMSTATECODE = CMPSTATECODE
            DISPATCHFROMSTATENAME = CMPSTATENAME

            'PARTY GST DETAILS 

            DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN, (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If (DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "") And CHKEXPORTGST.Checked = False Then
                MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            Else
                PARTYGSTIN = DT.Rows(0).Item("GSTIN")
                SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                PARTYSTATENAME = DT.Rows(0).Item("STATENAME")
                PARTYSTATECODE = DT.Rows(0).Item("STATECODE")
                SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                PARTYADD1 = DT.Rows(0).Item("ADD1")
                PARTYADD2 = DT.Rows(0).Item("ADD2")
            End If

            If CHKEXPORTGST.Checked = True And CHKOVERSEAS.Checked = True Then
                PARTYGSTIN = "URP"
                SHIPTOGSTIN = "URP"
                PARTYSTATECODE = "96"
                SHIPTOSTATECODE = "96"
                PARTYPINCODE = "999999"
            End If



            'FETCH PINCODE / KMS / ADD1 / ADD2 OF SHIPTO IF IT IS NOT SAME AS CMBNAME
            If CMBPACKING.Text.Trim <> "" AndAlso cmbname.Text.Trim <> CMBPACKING.Text.Trim Then
                DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN,  (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_RANGE,'') AS KOTHARIPLACE ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBPACKING.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("PINCODE") = "" Then
                    MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                    SHIPTOPINCODE = DT.Rows(0).Item("PINCODE")
                    'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                    SHIPTOADD1 = DT.Rows(0).Item("ADD1")
                    SHIPTOADD2 = DT.Rows(0).Item("ADD2")
                    SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                    SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                    'KOTHARIPLACE = DT.Rows(0).Item("KOTHARIPLACE")
                End If
            End If


            If CHKEXPORTGST.Checked = True And CHKOVERSEAS.Checked = True Then
                SHIPTOGSTIN = "URP"
                SHIPTOPINCODE = "999999"
            End If



            'DISPATCHFROM GST DETAILS AND KMS WILL BE FETCHED FROM TXTKMS
            If CMBDISPATCHFROM.Text.Trim <> "" AndAlso cmbname.Text.Trim <> CMBDISPATCHFROM.Text.Trim Then
                DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN, (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBDISPATCHFROM.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Then
                    MsgBox(" Dispatch From Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    DISPATCHFROM = CMBDISPATCHFROM.Text.Trim
                    DISPATCHFROMGSTIN = DT.Rows(0).Item("GSTIN")
                    DISPATCHFROMSTATENAME = DT.Rows(0).Item("STATENAME")
                    DISPATCHFROMSTATECODE = DT.Rows(0).Item("STATECODE")
                    DISPATCHFROMPINCODE = DT.Rows(0).Item("PINCODE")
                    'DISPATCHFROMKMS = Val(TXTKMS.Text.Trim)
                    TEMPCMPDISPATCHADD1 = DT.Rows(0).Item("ADD1")
                    TEMPCMPADD2 = "ADD2" 'DT.Rows(0).Item("ADD2")
                End If
            End If



            'TRANSPORT GSTING IS NOT MANDATORY
            'FOR LOCAL TRANSPORT THERE IS NO GSTIN
            'TRANSPORT GSTIN IF TRANSPORT IS PRESENT
            If cmbtrans.Text.Trim <> "" Then
                DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TRANSGSTIN = DT.Rows(0).Item("GSTIN")
                'FOR LOCAL TRANSPORT THERE IS NO GSTIN
                'If TRANSGSTIN = "" Then
                '    MsgBox("Enter Transport GSTIN", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If
            End If



            'CHECKING COUNTER AND VALIDATE WHETHER EINVOICE WILL BE ALLOWED OR NOT, FOR EACH EINVOICE BILL WE NEED TO 2 API COUNTS (1 FOR TOKEN AND ANOTHER FOR EINVOICE)
            If CMPEINVOICECOUNTER = 0 Then
                MsgBox("E-Invoice Bill Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'GET USED EINVOICECOUNTER
            Dim USEDEINVOICECOUNTER As Integer = 0
            DT = OBJCMN.SEARCH("COUNT(COUNTERID) AS EINVOICECOUNT", "", "EINVOICEENTRY", " AND CMPID =" & CmpId)
            If DT.Rows.Count > 0 Then USEDEINVOICECOUNTER = Val(DT.Rows(0).Item("EINVOICECOUNT"))

            'IF COUNTERS ARE FINISJED
            If CMPEINVOICECOUNTER - USEDEINVOICECOUNTER <= 0 Then
                MsgBox("E-Invoice Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF DATE HAS EXPIRED
            If Now.Date > EINVOICEEXPDATE Then
                MsgBox("E-Invoice Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF BALANCECOUNTERS ARE .10 THEN INTIMATE
            If CMPEINVOICECOUNTER - USEDEINVOICECOUNTER < Format((CMPEINVOICECOUNTER * 0.1), "0") Then
                MsgBox("Only " & (CMPEINVOICECOUNTER - USEDEINVOICECOUNTER) & " API's Left, Kindly contact Nakoda Infotech for Renewal of E-Invoice Package", MsgBoxStyle.Critical)
            End If


            'FOR GENERATING EINVOICE BILL WE NEED TO FIRST GENERATE THE TOKEN
            'THIS IS FOR SANDBOX TEST
            'Dim URL As New Uri("http://gstsandbox.charteredinfo.com/eivital/dec/v1.04/auth?aspid=1602611918&password=infosys123&Gstin=34AACCC1596Q002&user_name=TaxProEnvPON&eInvPwd=abc34*")
            Dim URL As New Uri("https://einvapi.charteredinfo.com/eivital/dec/v1.04/auth?aspid=1602611918&password=infosys123&Gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&eInvPwd=" & CMPEWBPASS)

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim REQUEST As WebRequest
            Dim RESPONSE As WebResponse
            REQUEST = WebRequest.CreateDefault(URL)

            REQUEST.Method = "GET"
            Try
                RESPONSE = REQUEST.GetResponse()
            Catch ex As WebException
                RESPONSE = ex.Response
            End Try
            Dim READER As StreamReader = New StreamReader(RESPONSE.GetResponseStream())
            Dim REQUESTEDTEXT As String = READER.ReadToEnd()

            'IF STATUS IS NOT 1 THEN TOKEN IS NOT GENERATED
            Dim STARTPOS As Integer = 0
            Dim TEMPSTATUS As String = ""
            Dim TOKEN As String = ""
            Dim ENDPOS As Integer = 0

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 2
            TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
            If TEMPSTATUS = "1" Then TEMPSTATUS = "SUCCESS" Else TEMPSTATUS = "FAILED"




            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("authtoken") + Len("AUTHTOKEN") + 3
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS) - 1
            TOKEN = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            'ADD DATA IN EINVOICEENTRY
            'DONT ADD IN EINVOICEENTRY, DONE BY GULKIT, IF FAILED THEN ADD
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
            'IF STATUS IS FAILED THEN ERROR MESSAGE
            If TEMPSTATUS = "FAILED" Then
                MsgBox("Unable to create E-Invoice", MsgBoxStyle.Critical)
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End If

            Dim j As String = ""
            Dim PRINTINITIALS As String = ""

            'GENERATING EINVOICE
            'FOR SANBOX TEST
            'Dim FURL As New Uri("http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice?aspid=1602611918&password=infosys123&Gstin=34AACCC1596Q002&AuthToken=" & TOKEN & "&user_name=TaxProEnvPON&QrCodeSize=250")
            Dim FURL As New Uri("https://einvapi.charteredinfo.com/eicore/dec/v1.03/Invoice?aspid=1602611918&password=infosys123&Gstin=" & CMPGSTIN & "&AuthToken=" & TOKEN & "&user_name=" & CMPEWBUSER & "&QrCodeSize=250")
            REQUEST = WebRequest.CreateDefault(FURL)
            REQUEST.Method = "POST"
            Try
                REQUEST.ContentType = "application/json"



                j = "{"
                j = j & """Version"": ""1.1"","
                j = j & """TranDtls"": {"
                j = j & """TaxSch"":""GST"","
                If CHKEXPORTGST.Checked = True And CHKOVERSEAS.Checked = True Then j = j & """SupTyp"":""EXPWP""," Else j = j & """SupTyp"":""B2B"","
                j = j & """RegRev"":""N"","
                j = j & """IgstOnIntra"":""N""},"



                'WE NEED TO FETCH INITIALS INSTEAD OF BILLNO
                Dim DTINI As DataTable = OBJCMN.SEARCH("INVOICE_PRINTINITIALS AS PRINTINITIALS", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId)
                PRINTINITIALS = DTINI.Rows(0).Item("PRINTINITIALS")

                j = j & """DocDtls"": {"
                j = j & """Typ"":""INV"","
                j = j & """No"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """Dt"":""" & INVOICEDATE.Text & """" & "},"


                'For WORKING ON SANDBOX
                'CMPGSTIN = "34AACCC1596Q002"
                'CMPPINCODE = "605001"
                'CMPSTATECODE = "34"


                j = j & """SellerDtls"": {"
                j = j & """Gstin"":""" & CMPGSTIN & """" & ","
                j = j & """LglNm"":""" & CmpName & """" & ","
                j = j & """TrdNm"":""" & CmpName & """" & ","
                j = j & """Addr1"":""" & TEMPCMPADD1.Trim.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & TEMPCMPADD2.Trim.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & CMPCITYNAME & """" & "," 'CMBFROMCITY.Text.Trim & """" & ","
                j = j & """Pin"":" & CMPPINCODE & "" & ","
                j = j & """Stcd"":""" & CMPSTATECODE & """" & "},"

                If PARTYADD1 = "" Then PARTYADD1 = PARTYSTATENAME
                If PARTYADD2 = "" Then PARTYADD2 = PARTYSTATENAME

                j = j & """BuyerDtls"": {"
                j = j & """Gstin"":""" & PARTYGSTIN & """" & ","
                j = j & """LglNm"":""" & cmbname.Text.Trim & """" & ","
                j = j & """TrdNm"":""" & cmbname.Text.Trim & """" & ","
                j = j & """Pos"":""" & PARTYSTATECODE & """" & ","
                j = j & """Addr1"":""" & PARTYADD1.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & PARTYADD2.Replace(vbCrLf, " ") & """" & ","
                'If ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then j = j & """Loc"":""" & KOTHARIPLACE & """" & "," Else j = j & """Loc"":""" & CMBTOCITY.Text.Trim & """" & ","
                j = j & """Loc"":""" & CMBTOCITY.Text.Trim & """" & ","
                j = j & """Pin"":" & PARTYPINCODE & "" & ","
                j = j & """Stcd"":""" & PARTYSTATECODE & """" & "},"


                j = j & """DispDtls"": {"
                j = j & """Nm"":""" & DISPATCHFROM & """" & ","
                j = j & """Addr1"":""" & TEMPCMPDISPATCHADD1.Trim.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & TEMPCMPADD2.Trim.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & CMBFROMCITY.Text.Trim & """" & ","
                j = j & """Pin"":" & DISPATCHFROMPINCODE & "" & ","
                j = j & """Stcd"":""" & DISPATCHFROMSTATECODE & """" & "},"

                j = j & """ShipDtls"": {"
                If SHIPTOGSTIN <> "" Then j = j & """Gstin"":""" & SHIPTOGSTIN & """" & ","
                j = j & """LglNm"":""" & CMBPACKING.Text.Trim & """" & ","
                j = j & """TrdNm"":""" & CMBPACKING.Text.Trim & """" & ","
                If SHIPTOADD1 = "" Then j = j & """Addr1"":""" & PARTYADD1.Replace(vbCrLf, " ") & """" & "," Else j = j & """Addr1"":""" & SHIPTOADD1.Replace(vbCrLf, " ") & """" & ","
                If SHIPTOADD2 = "" Then SHIPTOADD2 = " ADDRESS2 "
                j = j & """Addr2"":""" & SHIPTOADD2 & """" & ","
                j = j & """Loc"":""" & CMBTOCITY.Text.Trim & """" & ","
                If SHIPTOPINCODE = "" Then j = j & """Pin"":" & PARTYPINCODE & "" & "," Else j = j & """Pin"":" & SHIPTOPINCODE & "" & ","
                j = j & """Stcd"":""" & SHIPTOSTATECODE & """" & "},"


                j = j & """ItemList"":[{"



                Dim TEMPLINEDISC As Double = 0
                Dim TEMPLINETAXABLEAMT As Double = 0
                Dim TEMPLINECGSTAMT As Double = 0
                Dim TEMPLINESGSTAMT As Double = 0
                Dim TEMPLINEIGSTAMT As Double = 0
                Dim TEMPLINEGRIDTOTALAMT As Double = 0
                Dim TEMPLINECHARGES As Double = 0
                Dim TEMPRATE As Double = 0
                If Val(TXTCHARGES.Text.Trim) < 0 Then
                    If GRIDINVOICE.Rows(0).Cells(GPER.Index).Value = "Pcs" Then TEMPLINEDISC = Val(TXTCHARGES.Text.Trim) / Val(lbltotalpcs.Text.Trim) Else TEMPLINEDISC = Val(TXTCHARGES.Text.Trim) / Val(lbltotalmtrs.Text.Trim)
                Else
                    If GRIDINVOICE.Rows(0).Cells(GPER.Index).Value = "Pcs" Then TEMPRATE = Val(TXTCHARGES.Text.Trim) / Val(lbltotalpcs.Text.Trim) Else TEMPRATE = Val(TXTCHARGES.Text.Trim) / Val(lbltotalmtrs.Text.Trim)
                End If


                Dim TEMPTOTALAMT As Double = 0
                Dim TEMPTOTALDISC As Double = 0
                Dim TEMPTOTALTAXABLEAMT As Double = 0
                Dim TEMPTOTALCGSTAMT As Double = 0
                Dim TEMPTOTALSGSTAMT As Double = 0
                Dim TEMPTOTALIGSTAMT As Double = 0
                Dim TEMPGTOTALAMT As Double = 0


                'WE NEED TO FETCH ALL ITEMS HERE
                'FETCH FROM DESC TABLE 
                DT = OBJCMN.Execute_Any_String(" SELECT ISNULL(INVOICEMASTER_DESC.INVOICE_SRNO,0) AS SRNO, ISNULL(ITEMMASTER.item_name,'') AS ITEMNAME, ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGST, ISNULL(HSN_SGST,0) AS SGST, ISNULL(HSN_IGST,0) AS IGST, ISNULL(INVOICEMASTER_DESC.INVOICE_PCS,0) AS PCS, ISNULL(INVOICEMASTER_DESC.INVOICE_MTRS,0) AS MTRS, ISNULL(INVOICEMASTER_DESC.INVOICE_PER,'Mtrs') AS PER, ISNULL(INVOICEMASTER_DESC.INVOICE_RATE,0) AS RATE, ISNULL(INVOICEMASTER_DESC.INVOICE_AMOUNT,0) AS TOTALAMT, (ISNULL(INVOICEMASTER_DESC.INVOICE_DISCAMT,0) + ISNULL(INVOICEMASTER_DESC.INVOICE_SPDISCAMT,0)) AS LINEDISC, ISNULL(INVOICEMASTER_DESC.INVOICE_OTHERAMT,0) AS LINEOTHERAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_TAXABLEAMT,0) AS LINETAXABLEAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_CGSTAMT,0) AS LINECGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_SGSTAMT,0) AS LINESGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_IGSTAMT,0) AS LINEIGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_GRIDTOTAL,0) AS LINEGRIDTOTAL, ISNULL(HSN_TYPE,'Goods') HSNTYPE, ISNULL(INVOICEMASTER_DESC.INVOICE_CGSTPER,0) AS LINECGSTPER, ISNULL(INVOICEMASTER_DESC.INVOICE_SGSTPER,0) AS LINESGSTPER, ISNULL(INVOICEMASTER_DESC.INVOICE_IGSTPER,0) AS LINEIGSTPER FROM INVOICEMASTER INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN HSNMASTER ON HSN_ID = INVOICE_HSNCODEID INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTER_ID WHERE INVOICEMASTER.INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' and INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER_DESC.INVOICE_SRNO", "", "")
                Dim CURRROW As Integer = 0
                For Each DTROW As DataRow In DT.Rows

                    If GRIDINVOICE.Rows(0).Cells(GPER.Index).Value = "Pcs" Then TEMPLINECHARGES = Format(Val(TEMPLINEDISC) * Val(DTROW("PCS")), "0.0000") Else TEMPLINECHARGES = Format(Val(TEMPLINEDISC) * Val(DTROW("MTRS")), "0.00")
                    TEMPLINETAXABLEAMT = Format(Val(DTROW("TOTALAMT")) + Val(TEMPLINECHARGES), "0.00")
                    TEMPLINECGSTAMT = Format(Val(TXTCGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    TEMPLINESGSTAMT = Format(Val(TXTSGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    TEMPLINEIGSTAMT = Format(Val(TXTIGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    TEMPLINEGRIDTOTALAMT = Format(Val(TEMPLINETAXABLEAMT + TEMPLINECGSTAMT + TEMPLINESGSTAMT + TEMPLINEIGSTAMT), "0.00")

                    If CURRROW > 0 Then j = j & ", {"
                    j = j & """SlNo"": """ & DTROW("SRNO") & """" & ","
                    j = j & """PrdDesc"":""" & DTROW("ITEMNAME") & """" & ","
                    j = j & """IsServc"":""" & "Y" & """" & ","
                    j = j & """HsnCd"":""" & TXTSACCODE.Text.Trim & """" & ","
                    j = j & """Barcde"":""REC9999"","
                    If DTROW("PER") = "Pcs" Then j = j & """Qty"":" & Val(DTROW("PCS")) & "" & "," Else j = j & """Qty"":" & Val(DTROW("MTRS")) & "" & ","
                    j = j & """FreeQty"":" & "0" & "" & ","
                    j = j & """Unit"":""" & "MTR" & """" & ","


                    Dim DTHSN As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER_DESC.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER_DESC.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER_DESC.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER_DESC.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN HSNMASTER_DESC ON HSNMASTER.HSN_ID = HSNMASTER_DESC.HSN_ID ", " AND HSNMASTER_DESC.HSN_WEFDATE <= '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND HSNMASTER.HSN_CODE= '" & TXTSACCODE.Text.Trim & "' AND HSNMASTER.HSN_YEARID=" & YearId & " ORDER BY HSNMASTER_DESC.HSN_WEFDATE DESC")


                    If (Val(TXTCHARGES.Text.Trim) <= 0 And INVOICESCREENTYPE = "TOTAL GST") Or (Val(DTROW("LINEOTHERAMT")) - Val(DTROW("LINEDISC")) <= 0 And INVOICESCREENTYPE = "LINE GST") Then

                        j = j & """UnitPrice"":" & Val(DTROW("RATE")) & "" & ","
                        j = j & """TotAmt"":" & Format(Val(DTROW("TOTALAMT")), "0.00") & "" & ","

                        If INVOICESCREENTYPE = "LINE GST" Then
                            'If Val(DTROW("LINEDISC")) < 0 Then j = j & """Discount"":" & Val(DTROW("LINEDISC")) * -1 & "" & "," Else j = j & """Discount"":" & Val(DTROW("LINEDISC")) & "" & ","
                            If Val(DTROW("LINEOTHERAMT")) - Val(DTROW("LINEDISC")) < 0 Then j = j & """Discount"":" & (Val(DTROW("LINEOTHERAMT")) - Val(DTROW("LINEDISC"))) * -1 & "" & "," Else j = j & """Discount"":" & (Val(DTROW("LINEOTHERAMT")) - Val(DTROW("LINEDISC"))) & "" & ","
                        Else
                            If Val(TEMPLINECHARGES) < 0 Then j = j & """Discount"":" & Format(Val(TEMPLINECHARGES), "0.00") * -1 & "" & "," Else j = j & """Discount"":" & Format(Val(TEMPLINECHARGES), "0.00") & "" & ","
                        End If
                        j = j & """PreTaxVal"":" & "1" & "" & ","
                        If INVOICESCREENTYPE = "LINE GST" Then
                            j = j & """AssAmt"":" & Val(DTROW("LINETAXABLEAMT")) & "" & ","
                            j = j & """GstRt"":" & Val(DTROW("LINECGSTPER")) + Val(DTROW("LINESGSTPER")) + Val(DTROW("LINEIGSTPER")) & "" & ","
                        Else
                            j = j & """AssAmt"":" & Val(TEMPLINETAXABLEAMT) & "" & ","
                            If CHKEXPORTGST.CheckState = CheckState.Unchecked Then j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("IGSTPER")) & "" & "," Else j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("EXPIGSTPER")) & "" & ","
                        End If

                        If INVOICESCREENTYPE = "LINE GST" Then
                            j = j & """IgstAmt"":" & Val(DTROW("LINEIGSTAMT")) & "" & ","
                            j = j & """CgstAmt"":" & Val(DTROW("LINECGSTAMT")) & "" & ","
                            j = j & """SgstAmt"":" & Val(DTROW("LINESGSTAMT")) & "" & ","
                        Else
                            j = j & """IgstAmt"":" & Val(TEMPLINEIGSTAMT) & "" & ","
                            j = j & """CgstAmt"":" & Val(TEMPLINECGSTAMT) & "" & ","
                            j = j & """SgstAmt"":" & Val(TEMPLINESGSTAMT) & "" & ","
                        End If

                        j = j & """CesRt"":" & "0" & "" & ","
                        j = j & """CesAmt"":" & "0" & "" & ","
                        j = j & """CesNonAdvlAmt"":" & "0" & "" & ","
                        j = j & """StateCesRt"":" & "0" & "" & ","
                        j = j & """StateCesAmt"":" & "0" & "" & ","
                        j = j & """StateCesNonAdvlAmt"":" & "0" & "" & ","
                        j = j & """OthChrg"":" & "0" & "" & ","

                        If INVOICESCREENTYPE = "LINE GST" Then j = j & """TotItemVal"":" & Val(DTROW("LINEGRIDTOTAL")) & "" & "," Else j = j & """TotItemVal"":" & Val(TEMPLINEGRIDTOTALAMT) & "" & ","
                        j = j & """OrdLineRef"":"" "","
                        j = j & """OrgCntry"":""IN"","
                        j = j & """PrdSlNo"":""123"","

                        j = j & """BchDtls"": {"
                        j = j & """Nm"":""123"","
                        j = j & """Expdt"":""" & INVOICEDATE.Text & """" & ","
                        j = j & """wrDt"":""" & INVOICEDATE.Text & """" & "},"

                        j = j & """AttribDtls"": [{"
                        j = j & """Nm"":""" & DTROW("ITEMNAME") & """" & ","
                        j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"

                    Else

                        If INVOICESCREENTYPE = "LINE GST" Then
                            If DTROW("PER") = "Pcs" Then TEMPRATE = (Val(DTROW("LINEOTHERAMT")) - Val(DTROW("LINEDISC"))) / Val(DTROW("PCS")) Else TEMPRATE = (Val(DTROW("LINEOTHERAMT")) - Val(DTROW("LINEDISC"))) / Val(DTROW("MTRS"))
                        End If

                        j = j & """UnitPrice"":" & Format(Val(DTROW("RATE")) + TEMPRATE, "0.00") & "" & ","
                        If DTROW("PER") = "Pcs" Then TEMPLINETAXABLEAMT = Format(Val(Val(DTROW("RATE")) + TEMPRATE) * Val(DTROW("PCS")), "0.00") Else TEMPLINETAXABLEAMT = Format(Val(Val(DTROW("RATE")) + TEMPRATE) * Val(DTROW("MTRS")), "0.00")

                        TEMPLINECGSTAMT = Format(Val(TXTCGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                        TEMPLINESGSTAMT = Format(Val(TXTSGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                        TEMPLINEIGSTAMT = Format(Val(TXTIGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                        TEMPLINEGRIDTOTALAMT = Format(Val(TEMPLINETAXABLEAMT + TEMPLINECGSTAMT + TEMPLINESGSTAMT + TEMPLINEIGSTAMT), "0.00")

                        j = j & """TotAmt"":" & Val(TEMPLINETAXABLEAMT) & "" & ","
                        j = j & """Discount"":" & "0" & "" & ","
                        j = j & """PreTaxVal"":" & "1" & "" & ","

                        If INVOICESCREENTYPE = "LINE GST" Then
                            j = j & """AssAmt"":" & Val(DTROW("LINETAXABLEAMT")) & "" & ","
                            j = j & """GstRt"":" & Val(DTROW("LINECGSTPER")) + Val(DTROW("LINESGSTPER")) + Val(DTROW("LINEIGSTPER")) & "" & ","
                            j = j & """IgstAmt"":" & Val(DTROW("LINEIGSTAMT")) & "" & ","
                            j = j & """CgstAmt"":" & Val(DTROW("LINECGSTAMT")) & "" & ","
                            j = j & """SgstAmt"":" & Val(DTROW("LINESGSTAMT")) & "" & ","
                        Else
                            j = j & """AssAmt"":" & Val(TEMPLINETAXABLEAMT) & "" & ","
                            If CHKEXPORTGST.CheckState = CheckState.Unchecked Then j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("IGSTPER")) & "" & "," Else j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("EXPIGSTPER")) & "" & ","
                            j = j & """IgstAmt"":" & Val(TEMPLINEIGSTAMT) & "" & ","
                            j = j & """CgstAmt"":" & Val(TEMPLINECGSTAMT) & "" & ","
                            j = j & """SgstAmt"":" & Val(TEMPLINESGSTAMT) & "" & ","
                        End If


                        j = j & """CesRt"":" & "0" & "" & ","
                        j = j & """CesAmt"":" & "0" & "" & ","
                        j = j & """CesNonAdvlAmt"":" & "0" & "" & ","
                        j = j & """StateCesRt"":" & "0" & "" & ","
                        j = j & """StateCesAmt"":" & "0" & "" & ","
                        j = j & """StateCesNonAdvlAmt"":" & "0" & "" & ","
                        j = j & """OthChrg"":" & "0" & "" & ","
                        If INVOICESCREENTYPE = "LINE GST" Then j = j & """TotItemVal"":" & Val(DTROW("LINEGRIDTOTAL")) & "" & "," Else j = j & """TotItemVal"":" & Val(TEMPLINEGRIDTOTALAMT) & "" & ","
                        j = j & """OrdLineRef"":"" "","
                        j = j & """OrgCntry"":""IN"","
                        j = j & """PrdSlNo"":""123"","

                        j = j & """BchDtls"": {"
                        j = j & """Nm"":""123"","
                        j = j & """Expdt"":""" & INVOICEDATE.Text & """" & ","
                        j = j & """wrDt"":""" & INVOICEDATE.Text & """" & "},"

                        j = j & """AttribDtls"": [{"
                        j = j & """Nm"":""" & DTROW("ITEMNAME") & """" & ","
                        If INVOICESCREENTYPE = "LINE GST" Then j = j & """Val"":""" & Val(DTROW("LINEGRIDTOTAL")) & """" & "}]" Else j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"
                        'j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"
                    End If



                    j = j & " }"
                    CURRROW += 1


                    'THESE VARIABLES ARE JUST FOR TESTING PURPOSE
                    TEMPTOTALAMT += Val(DTROW("TOTALAMT"))
                    TEMPTOTALDISC += Val(TEMPLINECHARGES)
                    TEMPTOTALTAXABLEAMT += Val(TEMPLINETAXABLEAMT)
                    TEMPTOTALCGSTAMT += Val(TEMPLINECGSTAMT)
                    TEMPTOTALSGSTAMT += Val(TEMPLINESGSTAMT)
                    TEMPTOTALIGSTAMT += Val(TEMPLINEIGSTAMT)
                    TEMPGTOTALAMT += Val(TEMPLINEGRIDTOTALAMT)


                Next

                j = j & " ],"



                j = j & """ValDtls"": {"
                If INVOICESCREENTYPE = "TOTAL GST" Then
                    j = j & """AssVal"":" & Val(TXTSUBTOTAL.Text.Trim) & "" & ","
                    j = j & """CgstVal"":" & Val(TXTCGSTAMT1.Text.Trim) & "" & ","
                    j = j & """SgstVal"":" & Val(TXTSGSTAMT1.Text.Trim) & "" & ","
                    j = j & """IgstVal"":" & Val(TXTIGSTAMT1.Text.Trim) & "" & ","
                Else
                    j = j & """AssVal"":" & Val(LBLTOTALTAXABLEAMT.Text.Trim) & "" & ","
                    j = j & """CgstVal"":" & Val(LBLTOTALCGSTAMT.Text.Trim) & "" & ","
                    j = j & """SgstVal"":" & Val(LBLTOTALSGSTAMT.Text.Trim) & "" & ","
                    j = j & """IgstVal"":" & Val(LBLTOTALIGSTAMT.Text.Trim) & "" & ","
                End If

                j = j & """CesVal"":" & "0" & "" & ","
                j = j & """StCesVal"":" & "0" & "" & ","
                j = j & """Discount"":" & "0" & "" & ","
                j = j & """OthChrg"":" & Val(TXTTCSAMT.Text.Trim) & "" & ","
                j = j & """RndOffAmt"":" & Val(txtroundoff.Text.Trim) & "" & ","
                j = j & """TotInvVal"":" & Val(txtgrandtotal.Text.Trim) & "" & ","
                j = j & """TotInvValFc"":" & "0" & "" & "},"


                j = j & """PayDtls"": {"
                j = j & """Nm"":"" "","
                j = j & """Accdet"":"" "","
                j = j & """Mode"":""Credit"","
                j = j & """Fininsbr"":"" "","
                j = j & """Payterm"":"" "","
                j = j & """Payinstr"":"" "","
                j = j & """Crtrn"":"" "","
                j = j & """Dirdr"":"" "","
                j = j & """Crday"":" & Val(TXTCRDAYS.Text.Trim) & "" & ","
                j = j & """Paidamt"":" & "0" & "" & ","
                j = j & """Paymtdue"":" & Val(txtgrandtotal.Text.Trim) & "" & "},"


                j = j & """RefDtls"": {"
                j = j & """InvRm"":""TEST"","
                j = j & """DocPerdDtls"": {"
                j = j & """InvStDt"":""" & INVOICEDATE.Text & """" & ","
                j = j & """InvEndDt"":""" & INVOICEDATE.Text & """" & "},"

                j = j & """PrecDocDtls"": [{"
                j = j & """InvNo"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """InvDt"":""" & INVOICEDATE.Text & """" & ","
                j = j & """OthRefNo"":"" ""}],"

                j = j & """ContrDtls"": [{"
                j = j & """RecAdvRefr"":"" "","
                j = j & """RecAdvDt"":""" & INVOICEDATE.Text & """" & ","
                j = j & """Tendrefr"":"" "","
                j = j & """Contrrefr"":"" "","
                j = j & """Extrefr"":"" "","
                j = j & """Projrefr"":"" "","
                j = j & """Porefr"":"" "","
                j = j & """PoRefDt"":""" & INVOICEDATE.Text & """" & "}]"
                j = j & "},"




                j = j & """AddlDocDtls"": [{"
                j = j & """Url"":""https://einv-apisandbox.nic.in"","
                j = j & """Docs"":""INVOICE"","
                j = j & """Info"":""INVOICE""}],"

                j = j & """TransDocNo"":""" & txtlrno.Text.Trim & """" & ","



                j = j & """ExpDtls"": {"
                j = j & """ShipBNo"":"" "","
                j = j & """ShipBDt"":""" & INVOICEDATE.Text & """" & ","
                j = j & """Port"":""INBOM1"","
                j = j & """RefClm"":""N"","
                j = j & """ForCur"":""AED"","
                j = j & """CntCode"":""AE""}"



                'THIS CODE IS WRITTEN COZ WHEN BILLTO AND SHIPTO ARE IN THE SAME PINCODE THEN WE HAVE TO PASS MINIMUM 10 KMS
                'OR ELSE IT WILL GIVE ERROR
                If DISPATCHFROMPINCODE = PARTYPINCODE Then PARTYKMS = 10

                'WE HAVE REMOVED CREATING EWAY DIRECTLY FORM EINVOICE
                'USER HAVE TO MANUALLY CREATE EWAY SEPERATELY
                'If TXTVEHICLENO.Text.Trim <> "" Then
                '    j = j & ","
                '    j = j & """EwbDtls"": {"
                '    j = j & """TransId"":""" & TRANSGSTIN & """" & ","
                '    j = j & """TransName"":""" & cmbtrans.Text.Trim & """" & ","
                '    j = j & """Distance"":""" & PARTYKMS & """" & ","
                '    If LRDATE.Text <> "__/__/____" Then j = j & """TransDocDt"":""" & LRDATE.Text & """" & "," Else j = j & """TransDocDt"":"""","
                '    j = j & """VehNo"":""" & TXTVEHICLENO.Text.Trim & """" & ","
                '    j = j & """VehType"":""" & "R" & """" & ","
                '    j = j & """TransMode"":""1""" & "}"
                'End If

                j = j & "}"


                Dim stream As Stream = REQUEST.GetRequestStream()
                Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(j)
                stream.Write(buffer, 0, buffer.Length)

                'POST request absenden
                RESPONSE = REQUEST.GetResponse()



            Catch ex As WebException
                'RESPONSE = ex.Response
                'MsgBox("Error While Generating EWB, Please check the Data Properly")
                ''ADD DATA IN EINVOICEENTRY
                'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

                RESPONSE = ex.Response
                READER = New StreamReader(RESPONSE.GetResponseStream())
                REQUESTEDTEXT = READER.ReadToEnd()
                GoTo ERRORMESSAGE
            End Try

            READER = New StreamReader(RESPONSE.GetResponseStream())
            REQUESTEDTEXT = READER.ReadToEnd()


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 3
            TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
            If TEMPSTATUS = "1" Then
                TEMPSTATUS = "SUCCESS"
                MsgBox("E-Invoice Generated Successfully ")

            Else

ERRORMESSAGE:
                TEMPSTATUS = "FAILED"

                Dim ERRORMSG As String = ""
                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ErrorMessage") + Len("ErrorMessage") + 5
                ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("""", STARTPOS) - 2
                ERRORMSG = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

                'ADD DATA IN EINVOICEENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

                MsgBox("Error While Generating E-Invoice, " & REQUESTEDTEXT)

                Exit Sub
            End If


            Dim IRNNO As String = ""
            Dim ACKNO As String = ""
            Dim ADATE As String = ""


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ackno") + Len("ACKNO") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            ACKNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            TXTACKNO.Text = ACKNO


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ackdt") + Len("ACKDT") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            ADATE = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            ACKDATE.Value = ADATE

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("irn") + Len("IRN") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            IRNNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            TXTIRNNO.Text = IRNNO


            'WE NEED TO UPDATE THIS IRNNO IN DATABASE ALSO
            DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_IRNNO = '" & TXTIRNNO.Text.Trim & "', INVOICE_ACKNO = '" & TXTACKNO.Text.Trim & "', INVOICE_ACKDATE = '" & Format(ACKDATE.Value.Date, "MM/dd/yyyy") & "' FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

            'ADD DATA IN EINVOICEENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','" & TEMPSTATUS & "', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ADD DATA IN EINVOICEENTRY FOR QRCODE
            If TEMPSTATUS = "SUCCESS" Then

                ''GET SIGNED QRCODE
                Dim req As New RestRequest
                req.AddParameter("application/json", j, RestSharp.ParameterType.RequestBody)
                'Dim client As New RestClient("http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=34AACCC1596Q002&user_name=TaxProEnvPON&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim client As New RestClient("https://einvapi.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim res As IRestResponse = Await client.ExecuteTaskAsync(req)
                Dim respPl = New RespPl()
                respPl = JsonConvert.DeserializeObject(Of RespPl)(res.Content)
                Dim respPlGenIRNDec As New RespPlGenIRNDec()
                respPlGenIRNDec = JsonConvert.DeserializeObject(Of RespPlGenIRNDec)(respPl.Data)
                'MsgBox(respPlGenIRNDec.Irn)
                Dim qrImg As Byte() = Convert.FromBase64String(respPlGenIRNDec.QrCodeImage)
                Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(Bitmap))
                Dim bitmap1 As Bitmap = CType(tc.ConvertFrom(qrImg), Bitmap)

                'GET REGINITIALS AS SAVE WITH IT
                Dim TEMPREG As DataTable = OBJCMN.Execute_Any_String("SELECT REGISTER_INITIALS AS INITIALS FROM REGISTERMASTER WHERE REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTER_TYPE ='SALE' AND REGISTER_YEARID = " & YearId, "", "")
                bitmap1.Save(Application.StartupPath & "\" & TEMPREG.Rows(0).Item("INITIALS") & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png")
                PBQRCODE.ImageLocation = Application.StartupPath & "\" & TEMPREG.Rows(0).Item("INITIALS") & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png"
                PBQRCODE.Refresh()

                If PBQRCODE.Image IsNot Nothing Then
                    Dim OBJINVOICE As New ClsInvoiceMaster
                    Dim MS As New IO.MemoryStream
                    PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    OBJINVOICE.alParaval.Add(TXTINVOICENO.Text.Trim)
                    OBJINVOICE.alParaval.Add(cmbregister.Text.Trim)
                    OBJINVOICE.alParaval.Add(MS.ToArray)
                    OBJINVOICE.alParaval.Add(YearId)
                    Dim INTRES As Integer = OBJINVOICE.SAVEQRCODE()
                End If

                'DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_QRCODE = (SELECT * FROM OPENROWSET(BULK '" & Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png',SINGLE_BLOB) AS IMG) FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")


                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','QRCODE SUCCESS', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','QRCODE SUCCESS1', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

            End If




            'STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("QrCodeImage\", 0) + Len("QrCodeImage\") + 5
            'ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("""", STARTPOS)
            ''Dim QRSTREAM As New MemoryStream
            ''Dim bmp As New Bitmap(QRSTREAM)
            ''bmp.Save(QRSTREAM, Drawing.Imaging.ImageFormat.Bmp)
            ''QRSTREAM.Position = STARTPOS
            ''Dim data As Byte()
            ''QRSTREAM.Read(data, STARTPOS, STARTPOS - ENDPOS)

            'Dim bytes() As Byte
            'Dim ImageInStringFormat As String = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            'Dim MS As System.IO.MemoryStream
            'Dim NewImage As Bitmap

            'Dim nbyte() As Byte = System.Text.Encoding.UTF8.GetBytes(ImageInStringFormat)
            'Dim BASE64STRING As String = Convert.ToBase64String(nbyte)

            'bytes = Convert.FromBase64String(BASE64STRING)
            'NewImage = BytesToBitmap(bytes)
            'MS = New System.IO.MemoryStream(bytes)
            'MS.Write(bytes, 0, bytes.Length)
            'NewImage.Save(MS, Drawing.Imaging.ImageFormat.Bmp)    ' = System.Drawing.Image.FromStream(MS, True)
            'NewImage.Save("d:\qrcode.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

            'IRNNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            ''ADD data IN EINVOICEENTRY
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','" & TEMPSTATUS & "', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function BytesToBitmap(ByVal Bytes As Byte()) As Bitmap
        Dim stream As MemoryStream = Nothing

        Try
            stream = New MemoryStream(Bytes)
            Return New Bitmap(CType(New Bitmap(stream), Image))
        Catch ex As ArgumentNullException
            Throw ex
        Catch ex As ArgumentException
            Throw ex
        Finally
            stream.Close()
        End Try
    End Function

    Private Sub CMBSCREENTYPE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBSCREENTYPE.Validated
        Try
            HIDEVIEW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPLOADIRN_Click(sender As Object, e As EventArgs) Handles CMDUPLOADIRN.Click
        If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        OpenFileDialog1.Filter = "Pictures (*.png)|*.png"
        OpenFileDialog1.ShowDialog()

        OpenFileDialog1.AddExtension = True
        TXTFILENAME.Text = OpenFileDialog1.SafeFileName
        txtimgpath.Text = OpenFileDialog1.FileName
        TXTNEWIMGPATH.Text = Application.StartupPath & "\UPLOADDOCS\" & TXTINVOICENO.Text.Trim & txtuploadsrno.Text.Trim & TXTFILENAME.Text.Trim
        On Error Resume Next

        If txtimgpath.Text.Trim.Length <> 0 Then
            PBQRCODE.ImageLocation = txtimgpath.Text.Trim
            PBQRCODE.Load(txtimgpath.Text.Trim)
        End If
    End Sub

    Private Async Sub CMDGETQRCODE_Click(sender As Object, e As EventArgs) Handles CMDGETQRCODE.Click
        Try
            If EDIT = True And TXTIRNNO.Text.Trim <> "" And IsNothing(PBQRCODE.Image) = True Then

                'FIRST GETTOKEN AND THEN GET QRCODE
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable

                Dim URL As New Uri("https://einvapi.charteredinfo.com/eivital/dec/v1.04/auth?aspid=1602611918&password=infosys123&Gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&eInvPwd=" & CMPEWBPASS)

                ServicePointManager.Expect100Continue = True
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim REQUEST As WebRequest
                Dim RESPONSE As WebResponse
                REQUEST = WebRequest.CreateDefault(URL)

                REQUEST.Method = "GET"
                Try
                    RESPONSE = REQUEST.GetResponse()
                Catch ex As WebException
                    RESPONSE = ex.Response
                End Try
                Dim READER As StreamReader = New StreamReader(RESPONSE.GetResponseStream())
                Dim REQUESTEDTEXT As String = READER.ReadToEnd()

                'IF STATUS IS NOT 1 THEN TOKEN IS NOT GENERATED
                Dim STARTPOS As Integer = 0
                Dim TEMPSTATUS As String = ""
                Dim TOKEN As String = ""
                Dim ENDPOS As Integer = 0

                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 2
                TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
                If TEMPSTATUS = "1" Then TEMPSTATUS = "SUCCESS" Else TEMPSTATUS = "FAILED"




                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("authtoken") + Len("AUTHTOKEN") + 3
                ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS) - 1
                TOKEN = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

                'ADD DATA IN EINVOICEENTRY
                'DONT ADD IN EINVOICEENTRY, DONE BY GULKIT, IF FAILED THEN ADD
                'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


                'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
                'IF STATUS IS FAILED THEN ERROR MESSAGE
                If TEMPSTATUS = "FAILED" Then
                    MsgBox("Unable to create Eway Bill", MsgBoxStyle.Critical)
                    DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                    Exit Sub
                End If


                ''GET SIGNED QRCODE
                Dim req As New RestRequest
                req.AddParameter("application/json", "", RestSharp.ParameterType.RequestBody)
                'Dim client As New RestClient("http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=34AACCC1596Q002&user_name=TaxProEnvPON&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim client As New RestClient("https://einvapi.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim res As IRestResponse = Await client.ExecuteTaskAsync(req)
                Dim respPl = New RespPl()
                respPl = JsonConvert.DeserializeObject(Of RespPl)(res.Content)
                Dim respPlGenIRNDec As New RespPlGenIRNDec()
                respPlGenIRNDec = JsonConvert.DeserializeObject(Of RespPlGenIRNDec)(respPl.Data)
                'MsgBox(respPlGenIRNDec.Irn)
                Dim qrImg As Byte() = Convert.FromBase64String(respPlGenIRNDec.QrCodeImage)
                Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(Bitmap))
                Dim bitmap1 As Bitmap = CType(tc.ConvertFrom(qrImg), Bitmap)



                'bitmap1.Save(Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png")
                'PBQRCODE.ImageLocation = Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png"
                'PBQRCODE.Refresh()


                'GET REGINITIALS AS SAVE WITH IT
                Dim TEMPREG As DataTable = OBJCMN.Execute_Any_String("SELECT REGISTER_INITIALS AS INITIALS FROM REGISTERMASTER WHERE REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTER_TYPE ='SALE' AND REGISTER_YEARID = " & YearId, "", "")
                bitmap1.Save(Application.StartupPath & "\" & TEMPREG.Rows(0).Item("INITIALS") & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png")
                PBQRCODE.ImageLocation = Application.StartupPath & "\" & TEMPREG.Rows(0).Item("INITIALS") & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png"
                PBQRCODE.Refresh()



                'If PBQRCODE.Image IsNot Nothing Then
                '    Dim OBJINVOICE As New ClsInvoiceMaster
                '    Dim MS As New IO.MemoryStream
                '    PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                '    OBJINVOICE.alParaval.Add(TXTINVOICENO.Text.Trim)
                '    OBJINVOICE.alParaval.Add(cmbregister.Text.Trim)
                '    OBJINVOICE.alParaval.Add(MS.ToArray)
                '    OBJINVOICE.alParaval.Add(YearId)
                '    Dim INTRES As Integer = OBJINVOICE.SAVEQRCODE()
                'End If

                'DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_QRCODE = (SELECT * FROM OPENROWSET(BULK '" & Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png',SINGLE_BLOB) AS IMG) FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & TXTIRNNO.Text.Trim & "','QRCODE SUCCESS', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & TXTIRNNO.Text.Trim & "','QRCODE SUCCESS1', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                cmdOK_Click(sender, e)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon

            If EDIT = True Then SENDWHATSAPP(TEMPINVOICENO)
            DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_SENDWHATSAPP = 1 FROM InvoiceMaster INNER JOIN REGISTERMASTER On INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id WHERE INVOICE_NO = " & TEMPINVOICENO & " AND INVOICE_REGISTERID " & TEMPREGNAME & "  AND INVOICE_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(INVOICENO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech On 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub





            Dim WHATSAPPNO As String = ""
            Dim OBJINVOICE As New saledesign
            Dim PDFWITHDIGITALSIGN As Boolean = False
            If ALLOWDIGITALSIGN = True AndAlso MsgBox("Send Pdf with Digital Signature?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                OBJINVOICE.PDFWITHDIGITALSIGN = True
                PDFWITHDIGITALSIGN = True
            End If
            OBJINVOICE.MdiParent = MDIMain
            OBJINVOICE.DIRECTPRINT = True
            OBJINVOICE.FRMSTRING = "INVOICE"
            OBJINVOICE.DIRECTMAIL = True
            OBJINVOICE.PARTYNAME = cmbname.Text.Trim
            OBJINVOICE.INVOICECOPYNAME = "CUSTOMER COPY"
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STATE_REMARK,'') AS STATECODE, ISNULL(LEDGERS.ACC_WHATSAPPNO,'') AS WHATSAPPNO", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID ", " AND INVOICEMASTER.INVOICE_NO = " & Val(INVOICENO) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
            If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STATECODE") <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
            If DT.Rows.Count > 0 Then WHATSAPPNO = DT.Rows(0).Item("WHATSAPPNO")
            OBJINVOICE.registername = cmbregister.Text.Trim
            OBJINVOICE.PRINTSETTING = PRINTDIALOG
            OBJINVOICE.INVNO = Val(INVOICENO)
            OBJINVOICE.NOOFCOPIES = 1
            OBJINVOICE.Show()
            OBJINVOICE.Close()


            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = cmbname.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBAGENT.Text.Trim
            If cmbname.Text.Trim <> CMBPACKING.Text.Trim Then OBJWHATSAPP.OTHERNAME1 = CMBPACKING.Text.Trim

            If PDFWITHDIGITALSIGN = True Then
                OBJWHATSAPP.PATH.Add(Application.StartupPath & "\INVOICEPDF\" & cmbname.Text.Trim & "INVOICE_" & Val(INVOICENO) & "-" & Val(AccFrom.Year) & ".pdf")
                OBJWHATSAPP.FILENAME.Add(cmbname.Text.Trim & "INVOICE_" & Val(INVOICENO) & "-" & Val(AccFrom.Year) & ".pdf")
            Else
                OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & cmbname.Text.Trim & "INVOICE_" & Val(INVOICENO) & ".pdf")
                OBJWHATSAPP.FILENAME.Add(cmbname.Text.Trim & "INVOICE_" & Val(INVOICENO) & ".pdf")
            End If




            ''SEND CHALLAN | INVOICE | EWAYBILL 
            If MsgBox("Send Challan | Invoice | Eway ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim DTDESC As DataTable = OBJCMN.Execute_Any_String("select DISTINCT INVOICE_FROMNO AS GDNNO FROM INVOICEMASTER_DESC INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(INVOICENO) & " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId & " ORDER BY INVOICE_FROMNO ", "", "")
                For Each DTROW As DataRow In DTDESC.Rows
                    Dim OBJGDN As New GDNDESIGN
                    OBJGDN.MdiParent = MDIMain
                    OBJGDN.DIRECTPRINT = True
                    OBJGDN.FRMSTRING = "GDN"
                    OBJGDN.DIRECTMAIL = True
                    OBJGDN.vendorname = cmbname.Text.Trim
                    OBJGDN.agentname = CMBAGENT.Text.Trim
                    OBJGDN.FORMULA = "{GDN.GDN_no}=" & Val(DTROW("GDNNO")) & " and {GDN.GDN_yearid}=" & YearId
                    OBJGDN.JONO = Val(DTROW("GDNNO"))
                    OBJGDN.NOOFCOPIES = 1
                    OBJGDN.WHITELABEL = 0
                    OBJGDN.HIDEPCSDETAILS = 0
                    OBJGDN.PRINTINYARDS = 0
                    OBJGDN.Show()
                    OBJGDN.Close()
                    OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & cmbname.Text.Trim & "GDN_" & Val(DTROW("GDNNO")) & ".pdf")
                    OBJWHATSAPP.FILENAME.Add(cmbname.Text.Trim & "GDN_" & Val(DTROW("GDNNO")) & ".pdf")
                Next


                'ADDING EWAYBILL IF PRESENT
                If ClientName <> "RMANILAL" Then
                    If File.Exists(Application.StartupPath & "\EWB_" & TXTEWAYBILLNO.Text.Trim & ".Pdf") Then
                        OBJWHATSAPP.PATH.Add(Application.StartupPath & "\EWB_" & TXTEWAYBILLNO.Text.Trim & ".Pdf")
                        OBJWHATSAPP.FILENAME.Add("\EWB_" & TXTEWAYBILLNO.Text.Trim & ".Pdf")
                    End If
                End If
            End If

            OBJWHATSAPP.ShowDialog()



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDAUTOPOST_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDAUTOPOST.Click
        Try
            'GET INVOICENOS FROM INVOICEMASTER
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("MAX(INVOICE_NO) As INVOICENO", "", " INVOICEMASTER INNER JOIN REGISTERMASTER On REGISTER_ID = INVOICE_REGISTERID", " And REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId)
            For I As Integer = 3001 To Val(DT.Rows(0).Item("INVOICENO"))
                GRIDINVOICE.RowCount = 0
                TEMPINVOICENO = Val(I)
                TEMPREGNAME = cmbregister.Text.Trim
                EDIT = True
                'INVOICEMASTER_Load(sender, e)
                SHOWDATA()
                If GRIDINVOICE.RowCount = 0 Then GoTo NEXTLINE
                cmdOK_Click(sender, e)
NEXTLINE:
                CLEAR()
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTCUT.Validated
        Try
            CALC()
            TOTAL()
            If Val(TXTCUT.Text.Trim) > 0 And CHKOVERSEAS.Checked = False Then TXTMTRS.Text = Val(TXTCUT.Text.Trim) * Val(TXTPCS.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLSMS.Click
        If EDIT = False Then Exit Sub
        SMSCODE()
    End Sub

    Sub SMSCODE()
        If ALLOWSMS = True Then
            If ClientName <> "KOTHARI" And TXTMOBILENO.Text.Trim = "" Then Exit Sub
            If ClientName = "KOTHARI" And CMBPACKING.Text.Trim = "" Then Exit Sub

            If ClientName = "NVAHAN" And txtlrno.Text.Trim = "" Then
                If MsgBox("LR No not entered, Wish to send SMS?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If

            If MsgBox("Send SMS?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim MSG As String
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            'If ClientName = "KOTHARI" Then
            '    DT = OBJCMN.search("ACC_CODE AS LEDGERCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & CMBPACKING.Text.Trim & "' AND ACC_YEARID = " & YearId)

            '    If DT.Rows.Count > 0 Then MSG = DT.Rows(0).Item("LEDGERCODE") & "\n"
            '    MSG = MSG & txtchallan.Text.Trim & "\n"
            '    MSG = MSG & GRIDINVOICE.Rows(0).Cells(GITEMNAME.Index).Value & "-" & GRIDINVOICE.Rows(0).Cells(GDESCRIPTION.Index).Value & "\n"
            '    DT = OBJCMN.search("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
            '    If DT.Rows.Count > 0 Then MSG = MSG & DT.Rows(0).Item("TRANSCODE") & "\n"
            '    MSG = MSG & txtlrno.Text.Trim & "\n"
            '    MSG = MSG & LRDATE.Text

            If ClientName = "KOTHARI" Then
                MSG = MSG & CMBPACKING.Text.Trim & " - " & CMBTOCITY.Text.Trim & "\n"
                MSG = MSG & "GOODS DISPATCHED" & "\n"
                MSG = MSG & "BALE NO." & TXTINVOICENO.Text.Trim & " X " & TXTBALENOFROM.Text.Trim & "\n"
                MSG = MSG & "L.R.NO" & txtlrno.Text.Trim & " DT. " & LRDATE.Text.Trim & "\n"
                MSG = MSG & TXTEWAYBILLNO.Text

            ElseIf ClientName = "KCRAYON" Then
                MSG = "INV NO " & Val(TEMPINVOICENO) & "\n"
                DT = OBJCMN.SEARCH("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then MSG = MSG & "TRANSPORT NAME-" & DT.Rows(0).Item("TRANSCODE") & " & LRNO-" & txtlrno.Text.Trim & "\n"
                For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                    MSG = MSG & ROW.Cells(GITEMNAME.Index).Value & "-" & Format(Val(ROW.Cells(Gmtrs.Index).Value), "0.00") & "\n"
                Next
                MSG = MSG & "THANK YOU"

            ElseIf ClientName = "NVAHAN" Then
                MSG = "GOODS DESP" & vbCrLf
                MSG = MSG & "INV-" & Val(TEMPINVOICENO) & vbCrLf
                MSG = MSG & "LRNO-" & txtlrno.Text.Trim & vbCrLf
                MSG = MSG & "DT-" & LRDATE.Text & vbCrLf
                DT = OBJCMN.SEARCH("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then MSG = MSG & "TRANS-" & DT.Rows(0).Item("TRANSCODE") & vbCrLf
                MSG = MSG & "ITEM-" & GRIDINVOICE.Rows(0).Cells(GITEMNAME.Index).Value & vbCrLf & "PCS-" & Val(GRIDINVOICE.Rows(0).Cells(Gpcs.Index).Value) & vbCrLf & "MTRS-" & Val(GRIDINVOICE.Rows(0).Cells(Gmtrs.Index).Value) & vbCrLf & "BALE-" & GRIDINVOICE.Rows(0).Cells(GBALENO.Index).Value

            ElseIf ClientName = "SAKARIA" Then
                For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                    MSG = MSG & "ITEM-" & ROW.Cells(GITEMNAME.Index).Value & "\n"
                    MSG = MSG & "PCS-" & Val(ROW.Cells(Gpcs.Index).Value) & " MTRS-" & Val(ROW.Cells(Gmtrs.Index).Value) & "\n"
                Next
                DT = OBJCMN.SEARCH("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then MSG = MSG & DT.Rows(0).Item("TRANSCODE") & "\n"
                MSG = MSG & "LRNO-" & txtlrno.Text.Trim & ", DT-" & LRDATE.Text & "\n"
                MSG = MSG & "INV-" & Val(TXTINVOICENO.Text.Trim) & ", DT-" & INVOICEDATE.Text & "\n"
                MSG = MSG & "AMT-" & Val(txtgrandtotal.Text.Trim) & "\n"
                MSG = MSG & CmpName

            ElseIf ClientName = "YASHVI" Or ClientName = "ALENCOT" Then
                MSG = cmbname.Text.Trim & "\n"
                If ClientName = "ALENCOT" Then MSG = MSG & "BALENO-" & txtchallan.Text.Trim & "X" & Val(TXTBALENOFROM.Text.Trim) & "\n" Else MSG = MSG & "BALENO-" & txtchallan.Text.Trim & "\n"
                DT = OBJCMN.SEARCH("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then MSG = MSG & DT.Rows(0).Item("TRANSCODE") & "\n"
                MSG = MSG & "LRNO-" & txtlrno.Text.Trim & "\n"
                MSG = MSG & "DT-" & LRDATE.Text & "\n"
                MSG = MSG & "QTY-" & Val(lbltotalmtrs.Text.Trim) & "\n"
                MSG = MSG & CmpName

            ElseIf ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                MSG = "INV NO" & Val(TEMPINVOICENO) & "\n"
                MSG = MSG & "DT-" & INVOICEDATE.Text & "\n"
                DT = OBJCMN.SEARCH("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then MSG = MSG & "TRANSPORT NAME-" & DT.Rows(0).Item("TRANSCODE") & " & LRNO-" & txtlrno.Text.Trim & "\n"
                MSG = MSG & "LRDT-" & LRDATE.Text & "\n"
                MSG = MSG & " & BUNDLES-" & TXTBALENOFROM.Text.Trim & "\n"
                MSG = MSG & "THANK YOU"
            Else
                MSG = "INV-" & Val(TEMPINVOICENO) & ", DT-" & INVOICEDATE.Text & "\n"
                MSG = MSG & "LRNO-" & txtlrno.Text.Trim & ", DT-" & LRDATE.Text & "\n"
                DT = OBJCMN.SEARCH("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then MSG = MSG & "TRANS-" & DT.Rows(0).Item("TRANSCODE") & " & LRNO-" & txtlrno.Text.Trim & "(" & LRDATE.Text.Trim & ")" & "\n"
                For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                    MSG = MSG & ROW.Cells(GITEMNAME.Index).Value & "-" & Format(Val(ROW.Cells(Gmtrs.Index).Value), "0.00") & "\n"
                Next
                MSG = MSG & "AMT-" & Val(txtgrandtotal.Text.Trim)
            End If

            If SENDMSG(MSG, TXTMOBILENO.Text.Trim) = "1701" Then
                MsgBox("Message Sent")
                DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_SMSSEND = 1 WHERE INVOICE_NO = " & TEMPINVOICENO & " AND INVOICE_YEARID = " & YearId, "", "")
                LBLSMS.Visible = True
            Else
                MsgBox("Error Sending Message")
            End If
        End If
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

                TXTCGSTAMT1.ReadOnly = False
                TXTCGSTAMT1.TabStop = True
                TXTCGSTAMT1.BackColor = Color.LemonChiffon
                TXTSGSTAMT1.ReadOnly = False
                TXTSGSTAMT1.TabStop = True
                TXTSGSTAMT1.BackColor = Color.LemonChiffon
                TXTIGSTAMT1.ReadOnly = False
                TXTIGSTAMT1.TabStop = True
                TXTIGSTAMT1.BackColor = Color.LemonChiffon
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

                TXTCGSTAMT1.ReadOnly = True
                TXTCGSTAMT1.TabStop = False
                TXTCGSTAMT1.BackColor = Color.Linen
                TXTSGSTAMT1.ReadOnly = True
                TXTSGSTAMT1.TabStop = False
                TXTSGSTAMT1.BackColor = Color.Linen
                TXTIGSTAMT1.ReadOnly = True
                TXTIGSTAMT1.TabStop = False
                TXTIGSTAMT1.BackColor = Color.Linen
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCGSTAMT1_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTCGSTAMT1.Validated, TXTSGSTAMT1.Validated, TXTIGSTAMT1.Validated, TXTMATERIALVALUE.Validated
        Try
            CALC()
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEWB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEWB.Click
        Try
            If EDIT = False Then Exit Sub
            If ALLOWEINVOICE = True And TXTIRNNO.Text.Trim = "" Then
                If MsgBox("IRN not generated, First Generate IRN, Wish to Proceed Without IRN?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If
            If TXTSACCODE.Text.Trim = "" Then GENERATEEWB() Else GENERATEJOBEWB()
            PRINTEWB()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GENERATEEWB()
        Try
            If ALLOWEWAYBILL = False Then Exit Sub
            If cmbname.Text.Trim = "" Then Exit Sub

            If Val(LBLTOTALCGSTAMT.Text.Trim) = 0 And Val(TXTCGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALSGSTAMT.Text.Trim) = 0 And Val(TXTSGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALIGSTAMT.Text.Trim) = 0 And Val(TXTIGSTAMT1.Text.Trim) = 0 And CHKOVERSEAS.CheckState = CheckState.Unchecked Then Exit Sub


            If txtlrno.Text.Trim <> "" AndAlso LRDATE.Text <> "__/__/____" Then
                If Convert.ToDateTime(LRDATE.Text).Date < Convert.ToDateTime(INVOICEDATE.Text).Date Then
                    MsgBox("LR Date cannot be Before Invoice Date", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            If CMBFROMCITY.Text.Trim = "" Then
                MsgBox("Enter From City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If CMBTOCITY.Text.Trim = "" Then
                MsgBox("Enter to City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Generate E-Way Bill?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If TXTEWAYBILLNO.Text.Trim <> "" Then
                MsgBox("E-Way Bill No Already Generated", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'BEFORE GENERATING EWAY BILL WE NEED TO VALIDATE WHETHER ALL THE DATA ARE PRESENT OR NOT
            'IF DATA IS NOT PRESENT THEN VALIDATE
            'DATA TO BE CHECKED 
            '   1)CMPEWBUSER | CMPEWBPASS | CMPGSTIN | CMPPINCODE | CMPCITY | CMPSTATE | 
            '   2)PARTYGSTIN | PARTYCITY | PARTYPINCODE | PARTYSTATE | PARTYSTATECODE | PARTYKMS
            '   3)CGST OR SGST OR IGST (ALWAYS USE MTR IN QTYUNIT)
            If CMPEWBUSER = "" Or CMPEWBPASS = "" Or CMPGSTIN = "" Or CMPPINCODE = "" Or CMPCITYNAME = "" Or CMPSTATENAME = "" Then
                MsgBox(" Company Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim TEMPCMPADD1 As String = ""
            Dim TEMPCMPADD2 As String = ""
            Dim PARTYGSTIN As String = ""
            Dim PARTYPINCODE As String = ""
            Dim PARTYSTATECODE As String = ""
            Dim PARTYSTATENAME As String = ""
            Dim SHIPTOGSTIN As String = ""
            Dim SHIPTOSTATECODE As String = ""
            Dim SHIPTOSTATENAME As String = ""
            Dim PARTYKMS As Double = 0
            Dim PARTYADD1 As String = ""
            Dim PARTYADD2 As String = ""
            Dim TRANSGSTIN As String = ""
            Dim KOTHARIPLACE As String = ""  'THIS VARIABLE IS USED TO FETCH RANGE COLUMN ONLY FOR KOTHARI, THEY WILL ENTER FULL SHIPTO ADDRESS THERE
            Dim DISPATCHFROM As String = ""
            Dim DISPATCHFROMGSTIN As String = ""
            Dim DISPATCHFROMPINCODE As String = ""
            Dim DISPATCHFROMSTATECODE As String = ""
            Dim DISPATCHFROMSTATENAME As String = ""
            Dim DISPATCHFROMKMS As Double = 0
            Dim DISPATCHFROMADD1 As String = ""
            Dim DISPATCHFROMADD2 As String = ""

            Dim OBJCMN As New ClsCommon
            'CMP ADDRESS DETAILS
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(CMP_DISPATCHFROM, '') AS ADD1, ISNULL(CMP_ADD2,'') AS ADD2 ", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
            TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
            TEMPCMPADD2 = "" 'DT.Rows(0).Item("ADD2")
            DISPATCHFROM = CmpName
            DISPATCHFROMGSTIN = CMPGSTIN
            DISPATCHFROMPINCODE = CMPPINCODE
            DISPATCHFROMSTATECODE = CMPSTATECODE
            DISPATCHFROMSTATENAME = CMPSTATENAME


            'PARTY GST DETAILS 
            DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN, (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN ISNULL(ACC_SHIPPINGADD,'') ELSE ISNULL(ACC_ADD1,'') END) AS ADD1, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN '' ELSE ISNULL(ACC_ADD2,'') END) AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)

            If DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Then
                MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            Else
                If DT.Rows(0).Item("GSTIN") = "" Then
                    PARTYGSTIN = "URP"
                    SHIPTOGSTIN = "URP"
                Else
                    PARTYGSTIN = DT.Rows(0).Item("GSTIN")
                    SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                End If

                PARTYSTATENAME = DT.Rows(0).Item("STATENAME")
                PARTYSTATECODE = DT.Rows(0).Item("STATECODE")
                SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                PARTYADD1 = DT.Rows(0).Item("ADD1")
                PARTYADD2 = DT.Rows(0).Item("ADD2")
            End If


            'FETCH PINCODE / KMS / ADD1 / ADD2 OF SHIPTO IF IT IS NOT SAME AS CMBNAME
            If CMBPACKING.Text.Trim <> "" AndAlso cmbname.Text.Trim <> CMBPACKING.Text.Trim Then
                DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN,  (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(ACC_KMS,0) AS KMS, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN ISNULL(ACC_SHIPPINGADD,'') ELSE ISNULL(ACC_ADD1,'') END) AS ADD1, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN '' ELSE ISNULL(ACC_ADD2,'') END) AS ADD2, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_RANGE,'') AS KOTHARIPLACE ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBPACKING.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("PINCODE") = "" Then
                    MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else

                    If DT.Rows(0).Item("GSTIN") = "" Then
                        SHIPTOGSTIN = "URP"
                    Else
                        SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                    End If

                    PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                    'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                    PARTYADD1 = DT.Rows(0).Item("ADD1")
                    PARTYADD2 = DT.Rows(0).Item("ADD2")
                    SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                    SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                    KOTHARIPLACE = DT.Rows(0).Item("KOTHARIPLACE")
                End If
            End If




            'DISPATCHFROM GST DETAILS AND KMS WILL BE FETCHED FROM TXTKMS
            If CMBDISPATCHFROM.Text.Trim <> "" AndAlso cmbname.Text.Trim <> CMBDISPATCHFROM.Text.Trim Then
                DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN, (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN ISNULL(ACC_SHIPPINGADD,'') ELSE ISNULL(ACC_ADD1,'') END) AS ADD1, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN '' ELSE ISNULL(ACC_ADD2,'') END) AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBDISPATCHFROM.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Then
                    MsgBox(" Dispatch From Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    DISPATCHFROMGSTIN = DT.Rows(0).Item("GSTIN")
                    DISPATCHFROMSTATENAME = DT.Rows(0).Item("STATENAME")
                    DISPATCHFROMSTATECODE = DT.Rows(0).Item("STATECODE")
                    DISPATCHFROMPINCODE = DT.Rows(0).Item("PINCODE")
                    'DISPATCHFROMKMS = 0
                    TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
                    DISPATCHFROMADD1 = DT.Rows(0).Item("ADD1")
                    TEMPCMPADD2 = DT.Rows(0).Item("ADD2")
                    DISPATCHFROMADD2 = DT.Rows(0).Item("ADD2")
                End If
            End If


            'TRANSPORT GSTING IS NOT MANDATORY
            'FOR LOCAL TRANSPORT THERE IS NO GSTIN
            'TRANSPORT GSTIN IF TRANSPORT IS PRESENT
            If cmbtrans.Text.Trim <> "" Then
                DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TRANSGSTIN = DT.Rows(0).Item("GSTIN")
                'FOR LOCAL TRANSPORT THERE IS NO GSTIN
                'If TRANSGSTIN = "" Then
                '    MsgBox("Enter Transport GSTIN", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If
            End If



            'CHECKING COUNTER AND VALIDATE WHETHER EWAY BILL WILL BE ALLOWED OR NOT, FOR EACH EWAY BILL WE NEED TO 2 API COUNTS (1 FOR TOKEN AND ANOTHER FOR EWB)
            If CMPEWAYCOUNTER = 0 Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'GET USED EWAYCOUNTER
            Dim USEDEWAYCOUNTER As Integer = 0
            DT = OBJCMN.SEARCH("COUNT(COUNTERID) AS EWAYCOUNT", "", "EWAYENTRY", " AND CMPID =" & CmpId)
            If DT.Rows.Count > 0 Then USEDEWAYCOUNTER = Val(DT.Rows(0).Item("EWAYCOUNT"))

            'IF COUNTERS ARE FINISJED
            If CMPEWAYCOUNTER - USEDEWAYCOUNTER <= 0 Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF DATE HAS EXPIRED
            If Now.Date > EWAYEXPDATE Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF BALANCECOUNTERS ARE .10 THEN INTIMATE
            If CMPEWAYCOUNTER - USEDEWAYCOUNTER < Format((CMPEWAYCOUNTER * 0.1), "0") Then
                MsgBox("Only " & (CMPEWAYCOUNTER - USEDEWAYCOUNTER) & " API's Left, Kindly contact Nakoda Infotech for Renewal of EWB Package", MsgBoxStyle.Critical)
            End If


            'FOR GENERATING EWAY BILL WE NEED TO FIRST GENERATE THE TOKEN
            'THIS IS FOR SANDBOX TEST
            'Dim URL As New Uri("https://gstsandbox.charteredinfo.com/ewaybillapi/dec/v1.03/auth?action=ACCESSTOKEN&aspid=1602611918&password=infosys123&gstin=34AACCC1596Q002&username=TaxProEnvPON&ewbpwd=abc34*")
            Dim URL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/auth?action=ACCESSTOKEN&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&ewbpwd=" & CMPEWBPASS)

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim REQUEST As WebRequest
            Dim RESPONSE As WebResponse
            REQUEST = WebRequest.CreateDefault(URL)

            REQUEST.Method = "GET"
            Try
                RESPONSE = REQUEST.GetResponse()
            Catch ex As WebException
                RESPONSE = ex.Response
            End Try
            Dim READER As StreamReader = New StreamReader(RESPONSE.GetResponseStream())
            Dim REQUESTEDTEXT As String = READER.ReadToEnd()

            'IF STATUS IS NOT 1 THEN TOKEN IS NOT GENERATED
            Dim STARTPOS As Integer = 0
            Dim TEMPSTATUS As String = ""
            Dim TOKEN As String = ""
            Dim ENDPOS As Integer = 0

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 3
            TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
            If TEMPSTATUS = "1" Then TEMPSTATUS = "SUCCESS" Else TEMPSTATUS = "FAILED"




            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("authtoken") + Len("AUTHTOKEN") + 3
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS) - 1
            TOKEN = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            'ADD DATA IN EWAYENTRY
            'DONT ADD IN EWAYENTRY, DONE BY GULKIT, IF FAILED THEN ADD
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
            'IF STATUS IS FAILED THEN ERROR MESSAGE
            If TEMPSTATUS = "FAILED" Then
                MsgBox("Unable to create Eway Bill", MsgBoxStyle.Critical)
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End If



            'GENERATING EWAY BILL 
            'FOR SANBOX TEST
            'Dim FURL As New Uri("https://gstsandbox.charteredinfo.com/ewaybillapi/dec/v1.03/ewayapi?action=GENEWAYBILL&aspid=1602611918&password=infosys123&gstin=34AACCC1596Q002&username=TaxProEnvPON&authtoken=" & TOKEN)
            Dim FURL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/ewayapi?action=GENEWAYBILL&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&authtoken=" & TOKEN)
            REQUEST = WebRequest.CreateDefault(FURL)
            REQUEST.Method = "POST"
            Try
                REQUEST.ContentType = "application/json"


                'For WORKING ON SANDBOX
                'CMPGSTIN = "34AACCC1596Q002"
                'CMPPINCODE = "605001"
                'CMPSTATECODE = "34"


                Dim j As String = ""

                j = "{"
                j = j & """supplyType"":""O"","
                j = j & """subSupplyType"":""1"","
                j = j & """subSupplyDesc"":"""","
                j = j & """docType"":""INV"","

                'WE NEED TO FETCH INITIALS INSTEAD OF BILLNO
                Dim DTINI As DataTable = OBJCMN.SEARCH("INVOICE_PRINTINITIALS AS PRINTINITIALS", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId)

                j = j & """docNo"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """docDate"":""" & INVOICEDATE.Text & """" & ","
                j = j & """fromGstin"":""" & CMPGSTIN & """" & ","
                j = j & """fromTrdName"":""" & CmpName & """" & ","
                j = j & """fromAddr1"":""" & TEMPCMPADD1.Trim & """" & ","
                j = j & """fromAddr2"":""" & TEMPCMPADD2.Trim & """" & ","
                j = j & """fromPlace"":""" & CMBFROMCITY.Text.Trim & " - " & """" & ","
                j = j & """fromPincode"":""" & DISPATCHFROMPINCODE & """" & ","
                j = j & """actFromStateCode"":""" & DISPATCHFROMSTATECODE & """" & ","
                j = j & """fromStateCode"":""" & CMPSTATECODE & """" & ","
                j = j & """toGstin"":""" & PARTYGSTIN & """" & ","
                j = j & """toTrdName"":""" & cmbname.Text.Trim & """" & ","
                j = j & """toAddr1"":""" & PARTYADD1 & """" & ","
                j = j & """toAddr2"":""" & PARTYADD2 & """" & ","
                If ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then j = j & """toPlace"":""" & KOTHARIPLACE & """" & "," Else j = j & """toPlace"":""" & CMBTOCITY.Text.Trim & "-" & SHIPTOGSTIN & "-" & CMBPACKING.Text.Trim & """" & ","
                j = j & """toPincode"":""" & PARTYPINCODE & """" & ","
                j = j & """actToStateCode"":""" & SHIPTOSTATECODE & """" & ","
                j = j & """toStateCode"":""" & PARTYSTATECODE & """" & ","

                If ClientName = "RMANILAL" Then j = j & """transactionType"":""1""," Else j = j & """transactionType"":""4"","
                j = j & """dispatchFromGSTIN"":""" & DISPATCHFROMGSTIN & """" & ","
                j = j & """dispatchFromTradeName"":""" & DISPATCHFROM & """" & ","
                j = j & """shipToGSTIN"":""" & SHIPTOGSTIN & """" & ","
                j = j & """shipToTradeName"":""" & CMBPACKING.Text.Trim & """" & ","
                j = j & """otherValue"":""0"","



                If INVOICESCREENTYPE = "TOTAL GST" Then
                    j = j & """totalValue"":""" & Val(TXTSUBTOTAL.Text.Trim) & """" & ","
                    j = j & """cgstValue"":""" & Val(TXTCGSTAMT1.Text.Trim) & """" & ","
                    j = j & """sgstValue"":""" & Val(TXTSGSTAMT1.Text.Trim) & """" & ","
                    j = j & """igstValue"":""" & Val(TXTIGSTAMT1.Text.Trim) & """" & ","
                Else
                    j = j & """totalValue"":""" & Val(LBLTOTALTAXABLEAMT.Text.Trim) & """" & ","
                    j = j & """cgstValue"":""" & Val(LBLTOTALCGSTAMT.Text.Trim) & """" & ","
                    j = j & """sgstValue"":""" & Val(LBLTOTALSGSTAMT.Text.Trim) & """" & ","
                    j = j & """igstValue"":""" & Val(LBLTOTALIGSTAMT.Text.Trim) & """" & ","
                End If
                j = j & """cessValue"":""" & "0" & """" & ","
                j = j & """cessNonAdvolValue"":""" & "0" & """" & ","
                j = j & """totInvValue"":""" & Val(txtgrandtotal.Text.Trim) & """" & ","
                j = j & """transporterId"":""" & TRANSGSTIN & """" & ","
                j = j & """transporterName"":""" & cmbtrans.Text.Trim & """" & ","


                'THIS CODE IS WRITTEN COZ WHEN BILLTO AND SHIPTO ARE IN THE SAME PINCODE THEN WE HAVE TO PASS MINIMUM 10 KMS
                'OR ELSE IT WILL GIVE ERROR
                If DISPATCHFROMPINCODE = PARTYPINCODE Then PARTYKMS = 10

                If TXTVEHICLENO.Text.Trim = "" Then
                    j = j & """transDocNo"":"""","
                    j = j & """transMode"":"""","
                    j = j & """transDistance"":""" & PARTYKMS & """" & ","
                    j = j & """transDocDate"":"""","
                    j = j & """vehicleNo"":"""","
                    j = j & """vehicleType"":"""","
                Else
                    j = j & """transDocNo"":""" & txtlrno.Text.Trim & """" & ","
                    j = j & """transMode"":""" & "1" & """" & ","
                    j = j & """transDistance"":""" & PARTYKMS & """" & ","
                    If LRDATE.Text <> "__/__/____" Then j = j & """transDocDate"":""" & LRDATE.Text & """" & "," Else j = j & """transDocDate"":"""","
                    j = j & """vehicleNo"":""" & TXTVEHICLENO.Text.Trim & """" & ","
                    j = j & """vehicleType"":""" & "R" & """" & ","
                End If


                j = j & """itemList"":[{"


                'OLD CODE WITH SINGLE HSN
                ''j = j & """itemList"":["
                ''j = j & """{"
                'j = j & """productName"":""" & GRIDINVOICE.Item(GITEMNAME.Index, 0).Value & """" & ","
                'j = j & """productDesc"":""" & GRIDINVOICE.Item(GITEMNAME.Index, 0).Value & """" & ","
                'j = j & """hsnCode"":""" & GRIDINVOICE.Item(GHSNCODE.Index, 0).Value & """" & ","
                'j = j & """quantity"":""" & Val(lbltotalmtrs.Text.Trim) & """" & ","
                'j = j & """qtyUnit"":""" & "MTR" & """" & ","
                'If INVOICESCREENTYPE = "TOTAL GST" Then
                '    j = j & """cgstRate"":""" & Val(TXTCGSTPER1.Text.Trim) & """" & ","
                '    j = j & """sgstRate"":""" & Val(TXTSGSTPER1.Text.Trim) & """" & ","
                '    j = j & """igstRate"":""" & Val(TXTIGSTPER1.Text.Trim) & """" & ","
                'Else
                '    j = j & """cgstRate"":""" & Val(GRIDINVOICE.Item(GCGSTPER.Index, 0).Value) & """" & ","
                '    j = j & """sgstRate"":""" & Val(GRIDINVOICE.Item(GSGSTPER.Index, 0).Value) & """" & ","
                '    j = j & """igstRate"":""" & Val(GRIDINVOICE.Item(GIGSTPER.Index, 0).Value) & """" & ","
                'End If
                'j = j & """cessRate"":""" & "0" & """" & ","
                'j = j & """cessAdvol"":""" & "0" & """" & ","
                'If INVOICESCREENTYPE = "TOTAL GST" Then
                '    j = j & """taxableAmount"":""" & Val(TXTSUBTOTAL.Text.Trim) & """"
                'Else
                '    j = j & """taxableAmount"":""" & Val(LBLTOTALTAXABLEAMT.Text.Trim) & """"
                'End If
                'j = j & " }"

                'WE NEED TO FETCH SUMMARY OF ITEMS AND HSN TO PASS HERE
                'FETCH FROM DESC TABLE 
                DT = OBJCMN.Execute_Any_String(" SELECT ITEM_NAME AS ITEMNAME, ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGST, ISNULL(HSN_SGST,0) AS SGST, ISNULL(HSN_IGST,0) AS IGST, SUM(INVOICE_MTRS) AS MTRS, (CASE WHEN ISNULL(INVOICE_SCREENTYPE,'TOTAL GST') = 'TOTAL GST' THEN SUM(INVOICEMASTER_DESC.INVOICE_AMOUNT) ELSE SUM(INVOICE_TAXABLEAMT) END) AS TAXABLEAMT FROM INVOICEMASTER INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN HSNMASTER ON HSN_ID = INVOICE_HSNCODEID INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTER_ID WHERE INVOICEMASTER.INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' and INVOICEMASTER.INVOICE_YEARID = " & YearId & " GROUP BY item_name, ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), INVOICE_SCREENTYPE  ", "", "")
                Dim CURRROW As Integer = 0
                For Each DTROW As DataRow In DT.Rows

                    If ClientName = "MAHAVIRPOLYCOT" Then DTROW("ITEMNAME") = "FANCY SHIRTING"

                    If CURRROW > 0 Then j = j & ",{"
                    j = j & """productName"":""" & DTROW("ITEMNAME") & """" & ","
                    j = j & """productDesc"":""" & DTROW("ITEMNAME") & """" & ","
                    j = j & """hsnCode"":""" & DTROW("HSNCODE") & """" & ","
                    j = j & """quantity"":""" & Val(DTROW("MTRS")) & """" & ","
                    j = j & """qtyUnit"":""" & "MTR" & """" & ","
                    If INVOICESCREENTYPE = "TOTAL GST" Then
                        j = j & """cgstRate"":""" & Val(TXTCGSTPER1.Text.Trim) & """" & ","
                        j = j & """sgstRate"":""" & Val(TXTSGSTPER1.Text.Trim) & """" & ","
                        j = j & """igstRate"":""" & Val(TXTIGSTPER1.Text.Trim) & """" & ","
                    Else
                        j = j & """cgstRate"":""" & Val(GRIDINVOICE.Item(GCGSTPER.Index, CURRROW).Value) & """" & ","
                        j = j & """sgstRate"":""" & Val(GRIDINVOICE.Item(GSGSTPER.Index, CURRROW).Value) & """" & ","
                        j = j & """igstRate"":""" & Val(GRIDINVOICE.Item(GIGSTPER.Index, CURRROW).Value) & """" & ","
                    End If
                    j = j & """cessRate"":""" & "0" & """" & ","
                    'THIS CODE WAS IN V1.02
                    'j = j & """cessAdvol"":""" & "0" & """" & ","
                    j = j & """cessNonAdvol"":""" & "0" & """" & ","
                    j = j & """taxableAmount"":""" & Val(DTROW("TAXABLEAMT")) & """"
                    j = j & " }"
                    CURRROW += 1
                Next

                j = j & " ]}"

                Dim stream As Stream = REQUEST.GetRequestStream()
                Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(j)
                stream.Write(buffer, 0, buffer.Length)

                'POST request absenden
                RESPONSE = REQUEST.GetResponse()

            Catch ex As WebException
                'RESPONSE = ex.Response
                'MsgBox("Error While Generating EWB, Please check the Data Properly")
                ''ADD DATA IN EWAYENTRY
                'DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


                RESPONSE = ex.Response
                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                READER = New StreamReader(RESPONSE.GetResponseStream())
                REQUESTEDTEXT = READER.ReadToEnd()
                Dim ERRORMSG As String = ""
                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("message") + Len("message") + 5
                ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("}", STARTPOS) - 2
                ERRORMSG = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
                MsgBox("Error While Generating EWB, " & ERRORMSG)

                Exit Sub
            End Try

            READER = New StreamReader(RESPONSE.GetResponseStream())
            REQUESTEDTEXT = READER.ReadToEnd()




            Dim EWBNO As String = ""

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ewayBillNo") + Len("ewayBillNo") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS)
            EWBNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            TXTEWAYBILLNO.Text = EWBNO

            'WE NEED TO UPDATE THIS EWBNO IN DATABASE ALSO
            DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_EWAYBILLNO = '" & TXTEWAYBILLNO.Text.Trim & "' FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

            'ADD DATA IN EWAYENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & EWBNO & "','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GENERATEJOBEWB()
        Try
            If ALLOWEWAYBILL = False Then Exit Sub
            If cmbname.Text.Trim = "" Then Exit Sub
            If EDIT = False Then Exit Sub

            'If Val(LBLTOTALCGSTAMT.Text.Trim) = 0 And Val(TXTCGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALSGSTAMT.Text.Trim) = 0 And Val(TXTSGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALIGSTAMT.Text.Trim) = 0 And Val(TXTIGSTAMT1.Text.Trim) = 0 Then Exit Sub


            If txtlrno.Text.Trim <> "" AndAlso LRDATE.Text <> "__/__/____" Then
                If Convert.ToDateTime(LRDATE.Text).Date < Convert.ToDateTime(INVOICEDATE.Text).Date Then
                    MsgBox("LR Date cannot be Before Invoice Date", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            If CMBFROMCITY.Text.Trim = "" Then
                MsgBox("Enter From City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If CMBTOCITY.Text.Trim = "" Then
                MsgBox("Enter to City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Generate E-Way Bill?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If TXTEWAYBILLNO.Text.Trim <> "" Then
                MsgBox("E-Way Bill No Already Generated", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'BEFORE GENERATING EWAY BILL WE NEED TO VALIDATE WHETHER ALL THE DATA ARE PRESENT OR NOT
            'IF DATA IS NOT PRESENT THEN VALIDATE
            'DATA TO BE CHECKED 
            '   1)CMPEWBUSER | CMPEWBPASS | CMPGSTIN | CMPPINCODE | CMPCITY | CMPSTATE | 
            '   2)PARTYGSTIN | PARTYCITY | PARTYPINCODE | PARTYSTATE | PARTYSTATECODE | PARTYKMS
            '   3)CGST OR SGST OR IGST (ALWAYS USE MTR IN QTYUNIT)
            If CMPEWBUSER = "" Or CMPEWBPASS = "" Or CMPGSTIN = "" Or CMPPINCODE = "" Or CMPCITYNAME = "" Or CMPSTATENAME = "" Then
                MsgBox(" Company Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim TEMPCMPADD1 As String = ""
            Dim TEMPCMPADD2 As String = ""
            Dim PARTYGSTIN As String = ""
            Dim PARTYPINCODE As String = ""
            Dim PARTYSTATECODE As String = ""
            Dim PARTYSTATENAME As String = ""
            Dim SHIPTOGSTIN As String = ""
            Dim SHIPTOSTATECODE As String = ""
            Dim SHIPTOSTATENAME As String = ""
            Dim PARTYKMS As Double = 0
            Dim PARTYADD1 As String = ""
            Dim PARTYADD2 As String = ""
            Dim TRANSGSTIN As String = ""
            Dim KOTHARIPLACE As String = ""  'THIS VARIABLE IS USED TO FETCH RANGE COLUMN ONLY FOR KOTHARI, THEY WILL ENTER FULL SHIPTO ADDRESS THERE
            Dim DISPATCHFROM As String = ""
            Dim DISPATCHFROMGSTIN As String = ""
            Dim DISPATCHFROMPINCODE As String = ""
            Dim DISPATCHFROMSTATECODE As String = ""
            Dim DISPATCHFROMSTATENAME As String = ""
            Dim DISPATCHFROMKMS As Double = 0
            Dim DISPATCHFROMADD1 As String = ""
            Dim DISPATCHFROMADD2 As String = ""

            Dim OBJCMN As New ClsCommon
            'CMP ADDRESS DETAILS
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(CMP_DISPATCHFROM, '') AS ADD1, ISNULL(CMP_ADD2,'') AS ADD2 ", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
            TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
            TEMPCMPADD2 = "" 'DT.Rows(0).Item("ADD2")
            DISPATCHFROM = CmpName
            DISPATCHFROMGSTIN = CMPGSTIN
            DISPATCHFROMPINCODE = CMPPINCODE
            DISPATCHFROMSTATECODE = CMPSTATECODE
            DISPATCHFROMSTATENAME = CMPSTATENAME


            'PARTY GST DETAILS 
            DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN, (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN ISNULL(ACC_SHIPPINGADD,'') ELSE ISNULL(ACC_ADD1,'') END) AS ADD1, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN '' ELSE ISNULL(ACC_ADD2,'') END) AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)

            If DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Then
                MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            Else
                If DT.Rows(0).Item("GSTIN") = "" Then
                    PARTYGSTIN = "URP"
                    SHIPTOGSTIN = "URP"
                Else
                    PARTYGSTIN = DT.Rows(0).Item("GSTIN")
                    SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                End If

                PARTYSTATENAME = DT.Rows(0).Item("STATENAME")
                PARTYSTATECODE = DT.Rows(0).Item("STATECODE")
                SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                PARTYADD1 = DT.Rows(0).Item("ADD1")
                PARTYADD2 = DT.Rows(0).Item("ADD2")
            End If


            'FETCH PINCODE / KMS / ADD1 / ADD2 OF SHIPTO IF IT IS NOT SAME AS CMBNAME
            If CMBPACKING.Text.Trim <> "" AndAlso cmbname.Text.Trim <> CMBPACKING.Text.Trim Then
                DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN,  (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(ACC_KMS,0) AS KMS, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN ISNULL(ACC_SHIPPINGADD,'') ELSE ISNULL(ACC_ADD1,'') END) AS ADD1, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN '' ELSE ISNULL(ACC_ADD2,'') END) AS ADD2, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_RANGE,'') AS KOTHARIPLACE ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBPACKING.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("PINCODE") = "" Then
                    MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else

                    If DT.Rows(0).Item("GSTIN") = "" Then
                        SHIPTOGSTIN = "URP"
                    Else
                        SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                    End If

                    PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                    'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                    PARTYADD1 = DT.Rows(0).Item("ADD1")
                    PARTYADD2 = DT.Rows(0).Item("ADD2")
                    SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                    SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                    KOTHARIPLACE = DT.Rows(0).Item("KOTHARIPLACE")
                End If
            End If




            'DISPATCHFROM GST DETAILS AND KMS WILL BE FETCHED FROM TXTKMS
            If CMBDISPATCHFROM.Text.Trim <> "" AndAlso cmbname.Text.Trim <> CMBDISPATCHFROM.Text.Trim Then
                DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN, (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN ISNULL(ACC_SHIPPINGADD,'') ELSE ISNULL(ACC_ADD1,'') END) AS ADD1, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN '' ELSE ISNULL(ACC_ADD2,'') END) AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBDISPATCHFROM.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Then
                    MsgBox(" Dispatch From Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    DISPATCHFROMGSTIN = DT.Rows(0).Item("GSTIN")
                    DISPATCHFROMSTATENAME = DT.Rows(0).Item("STATENAME")
                    DISPATCHFROMSTATECODE = DT.Rows(0).Item("STATECODE")
                    DISPATCHFROMPINCODE = DT.Rows(0).Item("PINCODE")
                    'DISPATCHFROMKMS = 0
                    TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
                    DISPATCHFROMADD1 = DT.Rows(0).Item("ADD1")
                    TEMPCMPADD2 = DT.Rows(0).Item("ADD2")
                    DISPATCHFROMADD2 = DT.Rows(0).Item("ADD2")
                End If
            End If


            'TRANSPORT GSTING IS NOT MANDATORY
            'FOR LOCAL TRANSPORT THERE IS NO GSTIN
            'TRANSPORT GSTIN IF TRANSPORT IS PRESENT
            If cmbtrans.Text.Trim <> "" Then
                DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TRANSGSTIN = DT.Rows(0).Item("GSTIN")
                'FOR LOCAL TRANSPORT THERE IS NO GSTIN
                'If TRANSGSTIN = "" Then
                '    MsgBox("Enter Transport GSTIN", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If
            End If



            'CHECKING COUNTER AND VALIDATE WHETHER EWAY BILL WILL BE ALLOWED OR NOT, FOR EACH EWAY BILL WE NEED TO 2 API COUNTS (1 FOR TOKEN AND ANOTHER FOR EWB)
            If CMPEWAYCOUNTER = 0 Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'GET USED EWAYCOUNTER
            Dim USEDEWAYCOUNTER As Integer = 0
            DT = OBJCMN.SEARCH("COUNT(COUNTERID) AS EWAYCOUNT", "", "EWAYENTRY", " AND CMPID =" & CmpId)
            If DT.Rows.Count > 0 Then USEDEWAYCOUNTER = Val(DT.Rows(0).Item("EWAYCOUNT"))

            'IF COUNTERS ARE FINISJED
            If CMPEWAYCOUNTER - USEDEWAYCOUNTER <= 0 Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF DATE HAS EXPIRED
            If Now.Date > EWAYEXPDATE Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF BALANCECOUNTERS ARE .10 THEN INTIMATE
            If CMPEWAYCOUNTER - USEDEWAYCOUNTER < Format((CMPEWAYCOUNTER * 0.1), "0") Then
                MsgBox("Only " & (CMPEWAYCOUNTER - USEDEWAYCOUNTER) & " API's Left, Kindly contact Nakoda Infotech for Renewal of EWB Package", MsgBoxStyle.Critical)
            End If


            'For WORKING ON SANDBOX
            'CMPGSTIN = "34AACCC1596Q002"
            'CMPPINCODE = "605001"
            'CMPSTATECODE = "34"


            'FOR GENERATING EWAY BILL WE NEED TO FIRST GENERATE THE TOKEN
            'THIS IS FOR SANDBOX TEST
            'Dim URL As New Uri("https://gstsandbox.charteredinfo.com/ewaybillapi/dec/v1.03/auth?action=ACCESSTOKEN&aspid=1602611918&password=infosys123&gstin=34AACCC1596Q002&username=TaxProEnvPON&ewbpwd=abc34*")
            Dim URL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/auth?action=ACCESSTOKEN&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&ewbpwd=" & CMPEWBPASS)

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim REQUEST As WebRequest
            Dim RESPONSE As WebResponse
            REQUEST = WebRequest.CreateDefault(URL)

            REQUEST.Method = "GET"
            Try
                RESPONSE = REQUEST.GetResponse()
            Catch ex As WebException
                RESPONSE = ex.Response
            End Try
            Dim READER As StreamReader = New StreamReader(RESPONSE.GetResponseStream())
            Dim REQUESTEDTEXT As String = READER.ReadToEnd()

            'IF STATUS IS NOT 1 THEN TOKEN IS NOT GENERATED
            Dim STARTPOS As Integer = 0
            Dim TEMPSTATUS As String = ""
            Dim TOKEN As String = ""
            Dim ENDPOS As Integer = 0

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 3
            TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
            If TEMPSTATUS = "1" Then TEMPSTATUS = "SUCCESS" Else TEMPSTATUS = "FAILED"




            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("authtoken") + Len("AUTHTOKEN") + 3
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS) - 1
            TOKEN = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            'ADD DATA IN EWAYENTRY
            'DONT ADD IN EWAYENTRY, DONE BY GULKIT, IF FAILED THEN ADD
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
            'IF STATUS IS FAILED THEN ERROR MESSAGE
            If TEMPSTATUS = "FAILED" Then
                MsgBox("Unable to create Eway Bill", MsgBoxStyle.Critical)
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End If



            'GENERATING EWAY BILL 
            'FOR SANBOX TEST
            'Dim FURL As New Uri("https://gstsandbox.charteredinfo.com/ewaybillapi/dec/v1.03/ewayapi?action=GENEWAYBILL&aspid=1602611918&password=infosys123&gstin=34AACCC1596Q002&username=TaxProEnvPON&authtoken=" & TOKEN)
            Dim FURL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/ewayapi?action=GENEWAYBILL&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&authtoken=" & TOKEN)
            REQUEST = WebRequest.CreateDefault(FURL)
            REQUEST.Method = "POST"
            Try
                REQUEST.ContentType = "application/json"






                Dim j As String = ""

                j = "{"
                j = j & """supplyType"":""O"","
                j = j & """subSupplyType"":""4"","
                j = j & """subSupplyDesc"":"""","
                j = j & """docType"":""CHL"","

                'WE NEED TO FETCH INITIALS INSTEAD OF BILLNO
                Dim DTINI As DataTable = OBJCMN.SEARCH("INVOICE_PRINTINITIALS AS PRINTINITIALS", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId)

                j = j & """docNo"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """docDate"":""" & INVOICEDATE.Text & """" & ","
                j = j & """fromGstin"":""" & CMPGSTIN & """" & ","
                j = j & """fromTrdName"":""" & CmpName & """" & ","
                j = j & """fromAddr1"":""" & TEMPCMPADD1.Trim & """" & ","
                j = j & """fromAddr2"":""" & TEMPCMPADD2.Trim & """" & ","
                j = j & """fromPlace"":""" & CMBFROMCITY.Text.Trim & " - " & """" & ","
                j = j & """fromPincode"":""" & DISPATCHFROMPINCODE & """" & ","
                j = j & """actFromStateCode"":""" & DISPATCHFROMSTATECODE & """" & ","
                j = j & """fromStateCode"":""" & CMPSTATECODE & """" & ","
                j = j & """toGstin"":""" & PARTYGSTIN & """" & ","
                j = j & """toTrdName"":""" & cmbname.Text.Trim & """" & ","
                j = j & """toAddr1"":""" & PARTYADD1 & """" & ","
                j = j & """toAddr2"":""" & PARTYADD2 & """" & ","
                If ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then j = j & """toPlace"":""" & KOTHARIPLACE & """" & "," Else j = j & """toPlace"":""" & CMBTOCITY.Text.Trim & "-" & SHIPTOGSTIN & "-" & CMBPACKING.Text.Trim & """" & ","
                j = j & """toPincode"":""" & PARTYPINCODE & """" & ","
                j = j & """actToStateCode"":""" & SHIPTOSTATECODE & """" & ","
                j = j & """toStateCode"":""" & PARTYSTATECODE & """" & ","

                If ClientName = "RMANILAL" Then j = j & """transactionType"":""1""," Else j = j & """transactionType"":""4"","
                j = j & """dispatchFromGSTIN"":""" & DISPATCHFROMGSTIN & """" & ","
                j = j & """dispatchFromTradeName"":""" & DISPATCHFROM & """" & ","
                j = j & """shipToGSTIN"":""" & SHIPTOGSTIN & """" & ","
                j = j & """shipToTradeName"":""" & CMBPACKING.Text.Trim & """" & ","
                j = j & """otherValue"":""0"","


                Dim CGSTPER, SGSTPER, IGSTPER As Double
                Dim CGSTAMT, SGSTAMT, IGSTAMT As Double
                Dim HSNCODE As String = ""
                Dim DTHSN As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER_DESC.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER_DESC.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER_DESC.HSN_IGST, 0) AS IGSTPER", "", "HSNMASTER INNER JOIN HSNMASTER_DESC ON HSNMASTER.HSN_ID = HSNMASTER_DESC.HSN_ID ", " AND HSNMASTER_DESC.HSN_WEFDATE <= '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND HSNMASTER.HSN_CODE= '" & TXTSACCODE.Text.Trim & "' AND HSNMASTER.HSN_YEARID=" & YearId & " ORDER BY HSNMASTER_DESC.HSN_WEFDATE DESC")
                If DTHSN.Rows.Count <= 0 Then
                    MsgBox("Check HSN Properly", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    HSNCODE = Val(DTHSN.Rows(0).Item("HSNCODE"))

                    'IF PARTY STATE IS LOCAL THEN CGST AND SGST ELSE IGST
                    If PARTYSTATECODE = CMPSTATECODE Then
                        CGSTPER = Val(DTHSN.Rows(0).Item("CGSTPER"))
                        CGSTAMT = Format(Val(DTHSN.Rows(0).Item("CGSTPER")) * Val(TXTTOTALWITHMATVALUE.Text.Trim) / 100, "0.00")
                        SGSTPER = Val(DTHSN.Rows(0).Item("SGSTPER"))
                        SGSTAMT = Format(Val(DTHSN.Rows(0).Item("SGSTPER")) * Val(TXTTOTALWITHMATVALUE.Text.Trim) / 100, "0.00")
                        IGSTPER = 0
                        IGSTAMT = 0
                    Else
                        CGSTPER = 0
                        SGSTPER = 0
                        IGSTPER = Val(DTHSN.Rows(0).Item("IGSTPER"))
                        IGSTAMT = Format(Val(DTHSN.Rows(0).Item("IGSTPER")) * Val(TXTTOTALWITHMATVALUE.Text.Trim) / 100, "0.00")
                    End If


                End If


                j = j & """totalValue"":""" & Val(TXTTOTALWITHMATVALUE.Text.Trim) & """" & ","
                j = j & """cgstValue"":""" & Val(CGSTAMT) & """" & ","
                j = j & """sgstValue"":""" & Val(SGSTAMT) & """" & ","
                j = j & """igstValue"":""" & Val(IGSTAMT) & """" & ","
                j = j & """cessValue"":""" & "0" & """" & ","
                j = j & """cessNonAdvolValue"":""" & "0" & """" & ","
                j = j & """totInvValue"":""" & Format(Val(TXTTOTALWITHMATVALUE.Text.Trim) + Val(CGSTAMT) + Val(SGSTAMT) + Val(IGSTAMT), "0") & """" & ","
                j = j & """transporterId"":""" & TRANSGSTIN & """" & ","
                j = j & """transporterName"":""" & cmbtrans.Text.Trim & """" & ","


                'THIS CODE IS WRITTEN COZ WHEN BILLTO AND SHIPTO ARE IN THE SAME PINCODE THEN WE HAVE TO PASS MINIMUM 10 KMS
                'OR ELSE IT WILL GIVE ERROR
                If DISPATCHFROMPINCODE = PARTYPINCODE Then PARTYKMS = 10


                If TXTVEHICLENO.Text.Trim = "" Then
                    j = j & """transDocNo"":"""","
                    j = j & """transMode"":"""","
                    j = j & """transDistance"":""" & PARTYKMS & """" & ","
                    j = j & """transDocDate"":"""","
                    j = j & """vehicleNo"":"""","
                    j = j & """vehicleType"":"""","
                Else
                    j = j & """transDocNo"":""" & txtlrno.Text.Trim & """" & ","
                    j = j & """transMode"":""" & "1" & """" & ","
                    j = j & """transDistance"":""" & PARTYKMS & """" & ","
                    If LRDATE.Text <> "__/__/____" Then j = j & """transDocDate"":""" & LRDATE.Text & """" & "," Else j = j & """transDocDate"":"""","
                    j = j & """vehicleNo"":""" & TXTVEHICLENO.Text.Trim & """" & ","
                    j = j & """vehicleType"":""" & "R" & """" & ","
                End If



                j = j & """itemList"":[{"
                j = j & """productName"":""" & GRIDINVOICE.Item(GITEMNAME.Index, 0).Value & """" & ","
                j = j & """productDesc"":""" & GRIDINVOICE.Item(GITEMNAME.Index, 0).Value & """" & ","
                j = j & """hsnCode"":""" & GRIDINVOICE.Item(GHSNCODE.Index, 0).Value & """" & ","
                j = j & """quantity"":""" & Val(lbltotalmtrs.Text.Trim) & """" & ","
                j = j & """qtyUnit"":""" & "MTR" & """" & ","
                j = j & """cgstRate"":""" & Val(CGSTPER) & """" & ","
                j = j & """sgstRate"":""" & Val(SGSTPER) & """" & ","
                j = j & """igstRate"":""" & Val(IGSTPER) & """" & ","
                j = j & """cessRate"":""" & "0" & """" & ","
                j = j & """cessNonAdvol"":""" & "0" & """" & ","
                j = j & """taxableAmount"":""" & Val(TXTTOTALWITHMATVALUE.Text.Trim) & """"

                j = j & " }]}"

                Dim stream As Stream = REQUEST.GetRequestStream()
                Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(j)
                stream.Write(buffer, 0, buffer.Length)

                'POST request absenden
                RESPONSE = REQUEST.GetResponse()

            Catch ex As WebException
                'RESPONSE = ex.Response
                'MsgBox("Error While Generating EWB, Please check the Data Properly")
                ''ADD DATA IN EWAYENTRY
                'DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


                RESPONSE = ex.Response
                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                READER = New StreamReader(RESPONSE.GetResponseStream())
                REQUESTEDTEXT = READER.ReadToEnd()
                Dim ERRORMSG As String = ""
                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("message") + Len("message") + 5
                ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("}", STARTPOS) - 2
                ERRORMSG = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
                MsgBox("Error While Generating EWB, " & ERRORMSG)

                Exit Sub
            End Try

            READER = New StreamReader(RESPONSE.GetResponseStream())
            REQUESTEDTEXT = READER.ReadToEnd()




            Dim EWBNO As String = ""

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ewayBillNo") + Len("ewayBillNo") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS)
            EWBNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            TXTEWAYBILLNO.Text = EWBNO

            'WE NEED TO UPDATE THIS EWBNO IN DATABASE ALSO
            DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_EWAYBILLNO = '" & TXTEWAYBILLNO.Text.Trim & "' FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

            'ADD DATA IN EWAYENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & EWBNO & "','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTEWB()
        Try

            If PRINTEWAYBILL = False Then Exit Sub
            If TXTEWAYBILLNO.Text.Trim = "" Then Exit Sub


            If MsgBox("Print EWB?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim TOKENNO As String = ""
            Dim EWBNO As String = ""

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(TOKENNO, '') AS TOKENNO, ISNULL(EWBNO, '') AS EWBNO ", "", " EWAYENTRY ", " AND EWBNO = '" & TXTEWAYBILLNO.Text.Trim & "' And YearId = " & YearId)
            If DT.Rows.Count = 0 Then Exit Sub
            TOKENNO = DT.Rows(0).Item("TOKENNO")
            EWBNO = DT.Rows(0).Item("EWBNO")

            'Dim URL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/authenticate?action=ACCESSTOKEN&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&ewbpwd=" & CMPEWBPASS)
            Dim URL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/ewayapi?action=GetEwayBill&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&authtoken=" & TOKENNO & "&ewbNo=" & EWBNO)


            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim REQUEST As WebRequest
            Dim RESPONSE As WebResponse
            REQUEST = WebRequest.CreateDefault(URL)
            REQUEST.Method = "Get"
            Try
                RESPONSE = REQUEST.GetResponse()
            Catch ex As WebException
                RESPONSE = ex.Response
            End Try
            Dim READER As StreamReader = New StreamReader(RESPONSE.GetResponseStream())
            Dim REQUESTEDTEXT As String = READER.ReadToEnd()
            Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(REQUESTEDTEXT)

            Dim FURL As New Uri("https://einvapi.charteredinfo.com/aspapi/v1.0/printewb?aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN)
            REQUEST = WebRequest.CreateDefault(FURL)
            REQUEST.Method = "POST"
            Try
                REQUEST.ContentType = "application/x-www-form-urlencoded"
                REQUEST.ContentLength = buffer.Length

                Dim stream As Stream = REQUEST.GetRequestStream()
                stream.Write(buffer, 0, buffer.Length)

                'POST request absenden
                RESPONSE = REQUEST.GetResponse()
                Dim STRREADER As Stream = RESPONSE.GetResponseStream()
                Dim BINREADER As New BinaryReader(STRREADER)
                Dim BFFER As Byte() = BINREADER.ReadBytes(CInt(RESPONSE.ContentLength))
                File.WriteAllBytes(Application.StartupPath & "\EWB_" & TXTEWAYBILLNO.Text.Trim & ".pdf", BFFER)
                Process.Start(Application.StartupPath & "\EWB_" & TXTEWAYBILLNO.Text.Trim & ".pdf")

                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKENNO & "','" & EWBNO & "','PRINT SUCCESS1', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKENNO & "','" & EWBNO & "','PRINT SUCCESS2', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

            Catch ex As WebException
                RESPONSE = ex.Response
                MsgBox("Error While Printing EWB, Please check the Data Properly")
                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKENNO & "','" & EWBNO & "','PRINT FAILED1', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKENNO & "','" & EWBNO & "','PRINT FAILED2', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTROE_Validated(sender As Object, e As EventArgs) Handles TXTROE.Validated, TXTFOLDPER.Validated, TXTQTY.Validated
        CALC()
        TOTAL()
    End Sub

    Private Sub CMBCURRENCY_Enter(sender As Object, e As EventArgs) Handles CMBCURRENCY.Enter
        Try
            If CMBCURRENCY.Text.Trim = "" Then fillCURRENCY(CMBCURRENCY)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCURRENCY_Validating(sender As Object, e As CancelEventArgs) Handles CMBCURRENCY.Validating
        Try
            If CMBCURRENCY.Text.Trim <> "" Then CURRENCYVALIDATE(CMBCURRENCY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validated(sender As Object, e As EventArgs) Handles CMBDESIGN.Validated
        Try
            If CMBDESIGN.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEM_ID ", " AND DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' AND DESIGN_YEARID =  " & YearId)
                If DT.Rows.Count > 0 Then
                    If ClientName = "AVIS" Then CMBITEM.Text = DT.Rows(0).Item("ITEMNAME")
                End If

                If ClientName = "KCRAYON" Or ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                    Dim WHERECLAUSE As String = ""
                    If ClientName = "KCRAYON" AndAlso cmbname.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '" & cmbname.Text.Trim & "'"
                    If (ClientName = "SANGHVI" Or ClientName = "TINUMINU") AndAlso CMBQUALITY.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(QUALITYMASTER.QUALITY_NAME,'') = '" & CMBQUALITY.Text.Trim & "'"
                    If CMBDESIGN.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(DESIGNMASTER.DESIGN_NO,'') = '" & CMBDESIGN.Text.Trim & "'"
                    If (ClientName = "SANGHVI" Or ClientName = "TINUMINU") AndAlso CMBSHADE.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(COLORMASTER.COLOR_NAME,'') = '" & CMBSHADE.Text.Trim & "'"
                    Dim DTRATE As DataTable = OBJCMN.SEARCH("PRICELISTMASTER.PL_RATE AS SALERATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR ", "", "PRICELISTMASTER INNER JOIN ITEMMASTER ON PRICELISTMASTER.PL_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON PRICELISTMASTER.PL_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON PRICELISTMASTER.PL_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON PRICELISTMASTER.PL_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN LEDGERS ON PRICELISTMASTER.PL_LEDGERID = LEDGERS.ACC_ID ", " AND ISNULL(ITEMMASTER.ITEM_NAME,'') = '" & CMBITEM.Text.Trim & "'" & WHERECLAUSE & " AND PL_YEARID = " & YearId)
                    If DTRATE.Rows.Count > 0 Then TXTRATE.Text = DTRATE.Rows(0).Item("SALERATE")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_MouseWheel(sender As Object, e As MouseEventArgs) Handles cmbname.MouseWheel
        If ClientName = "AVIS" Then
            Dim HMEA As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
            HMEA.Handled = True
        End If
    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 Then

                Dim WHERECLAUSE As String = ""
                If ClientName <> "YAMUNESH" Then WHERECLAUSE = WHERECLAUSE & " AND DONE = 0"

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("TOP 1 *", "", "BARCODESTOCK", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "'" & WHERECLAUSE & "  AND YEARID =" & YearId)
                If DT.Rows.Count > 0 Then

                    If cmbname.Text.Trim = "" Then
                        MsgBox("Select Party Name", MsgBoxStyle.Critical)
                        Exit Sub
                    End If


                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    'FOR YAMUNESH MULTIPLE PCS MAY HAVE SAME BARCODE NOS
                    If ClientName <> "YAMUNESH" Then
                        For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                            If ROW.Cells(GBARCODE.Index).Value = TXTBARCODE.Text.Trim Then GoTo LINE1
                        Next
                    End If

                    'GET WRATE AND SALERATE FROM DESIGN
                    Dim RATE As Double = 0
                    Dim DESC As String = ""
                    Dim DTRATE As New DataTable
                    If ClientName = "YAMUNESH" Then
                        DTRATE = OBJCMN.SEARCH("GRN_BALENO AS RATE", "", "GRN_DESC", " AND GRN_BARCODE = '" & TXTBARCODE.Text.Trim & "' AND GRN_YEARID = " & YearId)
                        DESC = DTRATE.Rows(0).Item("RATE").ToString
                        RATE = Val(DTRATE.Rows(0).Item("RATE").ToString.Substring(0, Len(DTRATE.Rows(0).Item("RATE")) - 2))
                        RATE = Format(Val(RATE) / 2, "0.00")
                    Else
                        DTRATE = OBJCMN.SEARCH("DESIGN_WRATE AS WRATE, DESIGN_SALERATE AS SALERATE", "", "DESIGNMASTER", " AND DESIGN_NO = '" & DT.Rows(0).Item("DESIGNNO") & "' AND DESIGN_YEARID = " & YearId)
                        If DTRATE.Rows.Count > 0 Then
                            If CHKRETAIL.CheckState = CheckState.Checked Then RATE = Val(DTRATE.Rows(0).Item("SALERATE")) Else RATE = Val(DTRATE.Rows(0).Item("WRATE"))
                        End If
                    End If

                    'GET HSNCODE AND PERCENTAGES
                    Dim DTHSN As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER_DESC.HSN_CGST, 0) AS CGST, ISNULL(HSNMASTER_DESC.HSN_SGST, 0) AS SGST, ISNULL(HSNMASTER_DESC.HSN_IGST, 0) AS IGST,  ISNULL(HSNMASTER_DESC.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN HSNMASTER_DESC ON HSNMASTER.HSN_ID = HSNMASTER_DESC.HSN_ID INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND HSNMASTER_DESC.HSN_WEFDATE <= '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND ITEMMASTER.ITEM_NAME= '" & DT.Rows(0).Item("ITEMNAME") & "' AND HSNMASTER.HSN_YEARID=" & YearId & " ORDER BY HSNMASTER_DESC.HSN_WEFDATE DESC")

                    'REVERSE CALC
                    'WE HAVE TAKEN IGST COZ WE NEED TO REVERSE WITH RESPECT TO TOTALGST PERCENT
                    If CHKRETAIL.CheckState = CheckState.Checked And CHKREVERSECALC.Checked = True Then RATE = Format((RATE / (Val(DTHSN.Rows(0).Item("IGST")) + 100)) * 100, "0.00")

                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                        If LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(TXTBARCODE.Text.Trim) Then
                            ROW.Cells(Gpcs.Index).Value = Val(ROW.Cells(Gpcs.Index).Value) + 1
                            TOTAL()
                            GoTo LINE1
                        End If
                    Next


                    If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                        TXTCGSTPER1.Text = Val(DTHSN.Rows(0).Item("CGST"))
                        TXTSGSTPER1.Text = Val(DTHSN.Rows(0).Item("SGST"))
                        TXTIGSTPER1.Text = 0
                        GRIDINVOICE.Rows.Add(GRIDINVOICE.RowCount + 1, DT.Rows(0).Item("ITEMNAME"), DTHSN.Rows(0).Item("HSNCODE"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("DESIGNNO"), DT.Rows(0).Item("COLOR"), 0, 0, DESC, "", 1, Format(Val(DT.Rows(0).Item("CUT")), "0.00"), Format(Val(DT.Rows(0).Item("MTRS")), "0.00"), RATE, "Pcs", 0, "", "", Val(TXTDISCPER.Text.Trim), 0, 0, 0, 0, 0, Val(DTHSN.Rows(0).Item("CGST")), 0, Val(DTHSN.Rows(0).Item("SGST")), 0, 0, 0, 0, DT.Rows(0).Item("BARCODE"), DT.Rows(0).Item("FROMNO"), DT.Rows(0).Item("FROMSRNO"), DT.Rows(0).Item("TYPE"), 0, "", DT.Rows(0).Item("UNIT"), 0, 0, 0, "", "")
                    Else
                        TXTCGSTPER1.Text = 0
                        TXTSGSTPER1.Text = 0
                        TXTIGSTPER1.Text = Val(DTHSN.Rows(0).Item("IGST"))
                        GRIDINVOICE.Rows.Add(GRIDINVOICE.RowCount + 1, DT.Rows(0).Item("ITEMNAME"), DTHSN.Rows(0).Item("HSNCODE"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("DESIGNNO"), DT.Rows(0).Item("COLOR"), 0, 0, DESC, "", 1, Format(Val(DT.Rows(0).Item("CUT")), "0.00"), Format(Val(DT.Rows(0).Item("MTRS")), "0.00"), RATE, "Pcs", 0, "", "", Val(TXTDISCPER.Text.Trim), 0, 0, 0, 0, 0, 0, 0, 0, 0, Val(DTHSN.Rows(0).Item("IGST")), 0, 0, DT.Rows(0).Item("BARCODE"), DT.Rows(0).Item("FROMNO"), DT.Rows(0).Item("FROMSRNO"), DT.Rows(0).Item("TYPE"), 0, "", DT.Rows(0).Item("UNIT"), 0, 0, 0, "", "")
                    End If
                    TOTAL()
LINE1:
                    TXTBARCODE.Clear()
                    TXTBARCODE.Focus()
                Else
                    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                    TXTBARCODE.Clear()
                End If
            End If

            'OLDCODE
            '            If Len(TXTBARCODE.Text.Trim) > 7 Then

            '                'GET DATA FROM BARCODE
            '                Dim OBJCMN As New ClsCommon
            '                Dim DT As DataTable = OBJCMN.search("*", "", "BARCODESTOCK", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "' AND DONE = 0 AND CMPID = " & CmpId & " AND LOCATIONID  = " & Locationid & " AND YEARID = " & YearId)
            '                If DT.Rows.Count > 0 Then
            '                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
            '                    For Each ROW As DataGridViewRow In GRIDGDN.Rows
            '                        If ROW.Cells(GBARCODE.Index).Value = TXTBARCODE.Text.Trim Then GoTo LINE1
            '                    Next
            '                    GRIDGDN.Rows.Add(GRIDGDN.RowCount + 1, DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("DESIGNNO"), DT.Rows(0).Item("COLOR"), "", 1, Format(Val(DT.Rows(0).Item("MTRS")), "0.00"), DT.Rows(0).Item("BARCODE"), DT.Rows(0).Item("FROMNO"), DT.Rows(0).Item("FROMSRNO"), DT.Rows(0).Item("TYPE"))
            '                    total()
            '                    GRIDGDN.FirstDisplayedScrollingRowIndex = GRIDGDN.RowCount - 1

            'LINE1:
            '                    TXTBARCODE.Clear()
            '                Else
            '                    MsgBox("Invalid Barcode / Barcode already Used", MsgBoxStyle.Critical)
            '                    GoTo LINE1
            '                    Exit Sub
            '                End If
            '            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtrefno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtrefno.KeyPress
        If ClientName = "GELATO" Then numdotkeypress(e, sender, Me)
    End Sub

    Private Sub CMBPACKING_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBPACKING.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " And (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS')   AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBPACKING.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDISPATCHFROM_Enter(sender As Object, e As EventArgs) Handles CMBDISPATCHFROM.Enter
        Try
            If CMBDISPATCHFROM.Text.Trim = "" Then FILLNAME(CMBDISPATCHFROM, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDISPATCHFROM_Validating(sender As Object, e As CancelEventArgs) Handles CMBDISPATCHFROM.Validating
        Try
            NAMEVALIDATE(CMBDISPATCHFROM, CMBCODE, e, Me, TXTADD, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors')", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBDISPATCHFROM_Validated(sender As Object, e As EventArgs) Handles CMBDISPATCHFROM.Validated
        Try
            If cmbname.Text.Trim <> CMBDISPATCHFROM.Text.Trim Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS.ACC_KMS,0) AS KMS, ISNULL(CITYMASTER.CITY_NAME,'') AS FROMCITY ", "", " LEDGERS LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_DELIVERYATID = CITYMASTER.CITY_ID", " and LEDGERS.acc_cmpname = '" & CMBDISPATCHFROM.Text.Trim & "' AND LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBFROMCITY.Text = DT.Rows(0).Item("FROMCITY")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMOBILENO_Validated(sender As Object, e As EventArgs) Handles TXTMOBILENO.Validated
        Try
            If (ClientName = "CC" Or ClientName = "C3") And cmbname.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(ACC_CMPNAME,'') AS NAME", "", "LEDGERS ", " AND LEDGERS.ACC_MOBILE = '" & TXTMOBILENO.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    cmbname.Text = DT.Rows(0).Item("NAME")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSTOCK_Click(sender As Object, e As EventArgs) Handles CMDSELECTSTOCK.Click
        Try
            If GRIDORDER.RowCount = 0 Then
                MsgBox("Select Order First", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim OBJSTOCK As New SelectPurLRStock
            OBJSTOCK.ITEMNAME = GRIDORDER.Item(GITEMNAME.Index, 0).Value
            Dim DTST As DataTable = OBJSTOCK.DT
            OBJSTOCK.ShowDialog()

            If DTST.Rows.Count > 0 Then
                For Each DTROWPS As DataRow In DTST.Rows
                    GRIDINVOICE.Rows.Add(GRIDINVOICE.RowCount + 1, DTROWPS("ITEMNAME"), DTROWPS("HSNCODE"), "", "", "", Val(DTROWPS("AQTY")), Val(DTROWPS("FOLDPER")), "", "", Val(DTROWPS("QTY")), 0, Format(Val(DTROWPS("MTRS")), "0.00"), 0, "Mtrs", 0, DTROWPS("LRNO"), DTROWPS("TRANSPORT"), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", Val(DTROWPS("FROMNO")), 0, DTROWPS("TYPE"), 0, "", DTROWPS("UNIT"), 0, 0, Val(DTROWPS("WT")), DTROWPS("PURNAME"), DTROWPS("PARTYBILLNO"))
                Next
                getsrno(GRIDINVOICE)
                TOTAL()
                GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDINVOICE_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDINVOICE.CellClick
        Try
            'GET IMAGES FROM DESIGNMASTER
            If ClientName = "CC" Or ClientName = "C3" Then
                PBDESIGNIMAGE.Image = Nothing
                Dim OBJCMN As New ClsCommon
                Dim DTIMG As DataTable = OBJCMN.SEARCH("DESIGN_PHOTO AS PHOTO", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & GRIDINVOICE.Item(GDESIGN.Index, GRIDINVOICE.CurrentRow.Index).Value & "' AND DESIGN_YEARID = " & YearId)
                If DTIMG.Rows.Count > 0 Then
                    If IsDBNull(DTIMG.Rows(0).Item("PHOTO")) = False Then
                        PBDESIGNIMAGE.Image = Image.FromStream(New IO.MemoryStream(DirectCast(DTIMG.Rows(0).Item("PHOTO"), Byte())))
                    Else
                        PBDESIGNIMAGE.Image = Nothing
                    End If
                End If
            End If

            'GET LAST RATE OF SELECTED ITEM FOR SELECTED PARTY

            If (ClientName = "NIRAJ" Or ClientName = "DETLINE" Or ClientName = "MVIKASKUMAR" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "MSANCHITKUMAR") And GRIDINVOICE.Item(GITEMNAME.Index, GRIDINVOICE.CurrentRow.Index).Value <> "" And cmbname.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(INVOICEMASTER_DESC.INVOICE_RATE,0) AS LASTRATE", "", " INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN INVOICEMASTER ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO  AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN LEDGERS ON ACC_ID = INVOICE_LEDGERID", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & GRIDINVOICE.Item(GITEMNAME.Index, GRIDINVOICE.CurrentRow.Index).Value & "' AND INVOICEMASTER.INVOICE_DATE < '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER.INVOICE_NO DESC")
                If DT.Rows.Count > 0 Then LBLRATE.Text = Format(Val(DT.Rows(0).Item("LASTRATE")), "0.00")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSALESMAN_Enter(sender As Object, e As EventArgs) Handles CMBSALESMAN.Enter
        Try
            If CMBSALESMAN.Text.Trim = "" Then FILLSALESMAN(CMBSALESMAN)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSALESMAN_Validating(sender As Object, e As CancelEventArgs) Handles CMBSALESMAN.Validating
        Try
            If CMBSALESMAN.Text.Trim <> "" Then SALESMANVALIDATE(CMBSALESMAN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validated(sender As Object, e As EventArgs) Handles cmbtrans.Validated
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS.ACC_WARNING,'') AS WARNINGTEXT ", "", " LEDGERS ", " and LEDGERS.acc_cmpname = '" & cmbtrans.Text.Trim & "' and LEDGERS.acc_YEARid = " & YearId)
            If DT.Rows.Count > 0 Then
                If DT.Rows(0).Item("WARNINGTEXT") <> "" Then MsgBox(DT.Rows(0).Item("WARNINGTEXT"), MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validated(sender As Object, e As EventArgs) Handles cmbregister.Validated
        Try
            'GET DEFAUKLT SCREENTYPE
            If EDIT = False And cmbregister.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(DTYPE_PURSCREENTYPE,'GOODS PURCHASE') AS SERVICETYPE", "", " DEFAULTPURSCREENTYPE INNER JOIN REGISTERMASTER ON DTYPE_PURREGID = REGISTER_ID ", " AND REGISTER_NAME = '" & cmbregister.Text & "' AND DTYPE_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBSERVICETYPE.Text = DT.Rows(0).Item("SERVICETYPE")
                    If DT.Rows(0).Item("SERVICETYPE") = "JOB CHARGES" Then TXTSACCODE.Text = "998821" Else TXTSACCODE.Clear()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOSTCENTERNAME_Enter(sender As Object, e As EventArgs) Handles CMBCOSTCENTERNAME.Enter
        Try
            If CMBCOSTCENTERNAME.Text.Trim = "" Then FILLCOSTCENTER(CMBCOSTCENTERNAME)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOSTCENTERNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBCOSTCENTERNAME.Validating
        Try
            If CMBCOSTCENTERNAME.Text.Trim <> "" Then COSTCENTERVALIDATE(CMBCOSTCENTERNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSUPPLIERNAME_Enter(sender As Object, e As EventArgs) Handles CMBSUPPLIERNAME.Enter
        Try
            If CMBSUPPLIERNAME.Text.Trim <> "" Then FILLNAME(CMBSUPPLIERNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSUPPLIERNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBSUPPLIERNAME.Validating
        Try
            If CMBSUPPLIERNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBSUPPLIERNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBREFERREDBY_Enter(sender As Object, e As EventArgs) Handles CMBREFERREDBY.Enter
        Try
            If CMBREFERREDBY.Text.Trim = "" Then FILLNAME(CMBREFERREDBY, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBREFERREDBY_Validating(sender As Object, e As CancelEventArgs) Handles CMBREFERREDBY.Validating
        Try
            NAMEVALIDATE(CMBREFERREDBY, CMBCODE, e, Me, TXTADD, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors')", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSO_Click(sender As Object, e As EventArgs) Handles CMDSELECTSO.Click
        Try
            If cmbname.Text.Trim = "" Then
                MsgBox("Select Party Name", MsgBoxStyle.Critical)
                cmbname.Focus()
                Exit Sub
            End If
            Dim OBJCMN As New ClsCommon
            'Dim DT1 As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(INVOICEMASTER_DESC.INVOICE_RATE,0) AS LASTRATE", "", " INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN INVOICEMASTER ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO  AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN LEDGERS ON ACC_ID = INVOICE_LEDGERID", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & GRIDINVOICE.Item(GITEMNAME.Index, GRIDINVOICE.CurrentRow.Index).Value & "' AND INVOICEMASTER.INVOICE_DATE < '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER.INVOICE_NO DESC")
            'If DT1.Rows.Count > 0 Then LBLRATE.Text = Format(Val(DT1.Rows(0).Item("LASTRATE")), "0.00")

            Dim DTSO As New DataTable
            Dim OBJSELECTSO As New SelectSO
            OBJSELECTSO.PARTYNAME = cmbname.Text.Trim
            OBJSELECTSO.ShowDialog()
            DTSO = OBJSELECTSO.DT

            If DTSO.Rows.Count > 0 Then

                ''  GETTING DISTINCT SONO NO IN TEXTBOX
                Dim DV As DataView = DTSO.DefaultView
                Dim NEWDT As DataTable = DV.ToTable(True, "SONO")
                For Each DTR As DataRow In NEWDT.Rows
                    If TXTMULTISONO.Text.Trim = "" Then
                        TXTMULTISONO.Text = DTR("SONO").ToString
                    Else
                        TXTMULTISONO.Text = TXTMULTISONO.Text & "," & DTR("SONO").ToString
                    End If
                Next

                TXTSONO.Text = DTSO.Rows(0).Item("SONO")
                sodate.Text = DTSO.Rows(0).Item("DATE")


                ''  GETTING DISTINCT PONO NO IN TEXTBOX
                DV = DTSO.DefaultView
                NEWDT = DV.ToTable(True, "PONO")
                For Each DTR As DataRow In NEWDT.Rows
                    If txtpartypono.Text.Trim = "" Then
                        txtpartypono.Text = DTR("PONO").ToString
                    Else
                        txtpartypono.Text = txtpartypono.Text & "," & DTR("PONO").ToString
                    End If
                Next



                CMBAGENT.Text = DTSO.Rows(0).Item("AGENTNAME")
                If DTSO.Rows(0).Item("TRANSNAME") <> "" Then cmbtrans.Text = DTSO.Rows(0).Item("TRANSNAME")
                CMBTOCITY.Text = DTSO.Rows(0).Item("CITYNAME")
                CMBPACKING.Text = DTSO.Rows(0).Item("DELIVERYAT")
                txtremarks.Text = DTSO.Rows(0).Item("REMARKS")




                'FETCH DISCOUNT WITH RESPECT TO SALE ORDER 
                If (ClientName = "MASHOK" Or ClientName = "ABHEE") And EDIT = False Then

                    GRIDCHGS.RowCount = 0
                    Dim DT As New DataTable
                    If DTSO.Rows(0).Item("TYPE") = "YARNSALEORDER" Or DTSO.Rows(0).Item("TYPE") = "OPENINGYARNSALEORDER" Then
                        DT = OBJCMN.SEARCH(" ISNULL(YSO_DISCOUNT, 0) AS DISCPER, ISNULL(YSO_CD, 0) AS CDPER, 0 AS RATEDIFF, ISNULL(YSO_INT,0) AS AGENTCOMM, ISNULL(YSO_CRDAYS,0) AS CRDAYS", "", " ALLYARNSALEORDER ", " and ALLYARNSALEORDER.YSO_NO IN (" & TXTMULTISONO.Text.Trim & ") and ALLYARNSALEORDER.YSO_YEARid = " & YearId)
                    Else
                        DT = OBJCMN.SEARCH(" ISNULL(SO_DISCDEALER, 0) AS DISCPER, ISNULL(SO_CD, 0) AS CDPER, ISNULL(SO_DISCRATE,0) AS RATEDIFF, ISNULL(SO_INT,0) AS AGENTCOMM, ISNULL(SO_DAYS,0) AS CRDAYS", "", " ALLSALEORDER ", " and ALLSALEORDER.SO_NO IN (" & TXTMULTISONO.Text.Trim & ") and ALLSALEORDER.SO_YEARid = " & YearId)
                    End If

                    If DT.Rows.Count > 0 Then

                        TXTCRDAYS.Text = Val(DT.Rows(0).Item("CRDAYS"))
                        duedate.Value = Convert.ToDateTime(INVOICEDATE.Text).Date.AddDays(Val(TXTCRDAYS.Text.Trim))


                        'IN CHARGES GRID ADD DISCOUNT GIVEN / BROKERAGE
                        'If (ClientName = "YASHVI" Or ClientName = "SBA" Or ClientName = "DEVEN" Or ClientName = "SOFTAS" Or ClientName = "BARKHA" Or ClientName = "AVIS" Or ClientName = "MOMAI" Or ClientName = "SHREEVALLABH") Then
                        'INITIALLY IT WAS WITH RESPECT TO THE ABOVE MENTIONED CLIENT, THEN CHANGED WITH RESPECT TO SALEAUTODISCOUNT
                        If SALEAUTODISCOUNT = True And CMBSCREENTYPE.Text <> "LINE GST" And EDIT = False Then
                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "RATE DIFFERENCE" Then GoTo LINE2
                            Next
                            If Val(DT.Rows(0).Item("RATEDIFF")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "RATE DIFFERENCE", Val(DT.Rows(0).Item("RATEDIFF")) * -1, 0, 0)

                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "DISCOUNT GIVEN" Then GoTo LINE2
                            Next
                            If Val(DT.Rows(0).Item("DISCPER")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "DISCOUNT GIVEN", Val(DT.Rows(0).Item("DISCPER")) * -1, 0, 0)


                            For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                If DTROW.Cells(ECHARGES.Index).Value = "CASH DISCOUNT" Then GoTo LINE2
                            Next
                            If Val(DT.Rows(0).Item("CDPER")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "CASH DISCOUNT", Val(DT.Rows(0).Item("CDPER")) * -1, 0, 0)

                            'INITIALLY IT WAS WITH RESPECT TO THE ABOVE MENTIONED CLIENT, THEN CHANGED WITH RESPECT TO AUTOBROKERAGE
                            If AUTOBROKERAGE = True Then
                                For Each DTROW As DataGridViewRow In GRIDCHGS.Rows
                                    If DTROW.Cells(ECHARGES.Index).Value = "BROKERAGE" Then GoTo LINE2
                                Next
                                If Val(DT.Rows(0).Item("AGENTCOMM")) > 0 Then GRIDCHGS.Rows.Add(GRIDCHGS.RowCount + 1, "BROKERAGE", Val(DT.Rows(0).Item("AGENTCOMM")) * -1, 0, 0)
                            End If
                        End If

LINE2:
                    End If
                End If


                Dim CUT As Double = 0.00
                Dim PER As String = "Mtrs"

                'BEFORE ADDING THE ROW IN ORDERDER GRID CHECK WHETHER SAME ORDERNO AN SRNO IS PRESENT IN GRID OR NOT
                For Each DTROW As DataRow In DTSO.Rows
                    For Each ROW As DataGridViewRow In GRIDORDER.Rows
                        If Val(ROW.Cells(OFROMNO.Index).Value) = Val(DTROW("SONO")) And Val(ROW.Cells(OFROMSRNO.Index).Value) = Val(DTROW("GRIDSRNO")) And ROW.Cells(OFROMTYPE.Index).Value = DTROW("TYPE") Then GoTo NEXTLINE
                    Next


                    'ADD YARNQUALITY IN ITEMMASTER
                    If DTROW("TYPE") = "YARNSALEORDER" Or DTROW("TYPE") = "OPENINGYARNSALEORDER" Then
                        Dim DTITEM = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", " ITEMMASTER ", " AND ITEM_NAME = '" & DTROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                        If DTITEM.Rows.Count = 0 Then

                            'ADD NEW ITEMNAME 
                            Dim ALPARAVAL As New ArrayList
                            ALPARAVAL.Clear()


                            ALPARAVAL.Add("Finished Goods")

                            Dim DTCATEGORY As DataTable = OBJCMN.SEARCH("ISNULL(CATEGORY_NAME, '') AS CATEGORY", "", " CATEGORYMASTER INNER JOIN YARNQUALITYMASTER ON YARNQUALITYMASTER.YARN_CATEGORYID = CATEGORYMASTER.CATEGORY_ID", " AND YARNQUALITYMASTER.YARN_NAME = '" & DTROW("ITEMNAME") & "' AND CATEGORY_YEARID = " & YearId)
                            If DTCATEGORY.Rows.Count > 0 Then ALPARAVAL.Add(DTCATEGORY.Rows(0).Item("CATEGORY")) Else ALPARAVAL.Add("") 'CATEGORY

                            ALPARAVAL.Add(UCase(DTROW("ITEMNAME")))        'DISPLAYNAME
                            ALPARAVAL.Add(UCase(DTROW("ITEMNAME")))        'ITEMNAME

                            ALPARAVAL.Add("")   'DEPARTMENT
                            ALPARAVAL.Add(UCase(DTROW("ITEMNAME")))        'CODE
                            ALPARAVAL.Add("Kgs")   'UNIT
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

                            Dim DTHSN As DataTable = OBJCMN.SEARCH("ISNULL(HSN_CODE, '') AS HSNCODE", "", " HSNMASTER INNER JOIN YARNQUALITYMASTER ON YARNQUALITYMASTER.YARN_HSNCODEID = HSNMASTER.HSN_ID", " AND YARNQUALITYMASTER.YARN_NAME = '" & DTROW("ITEMNAME") & "' AND HSN_YEARID = " & YearId)
                            If DTHSN.Rows.Count > 0 Then ALPARAVAL.Add(DTHSN.Rows(0).Item("HSNCODE")) Else ALPARAVAL.Add("") 'HSNCODE

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

                            ALPARAVAL.Add("")   'COLORSRNO
                            ALPARAVAL.Add("")   'COLOR
                            ALPARAVAL.Add(0)   'VALUELOSSPER
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
                            ALPARAVAL.Add(0)    'SHADEGRIDSRNO

                            Dim objclsItemMaster As New clsItemmaster
                            objclsItemMaster.alParaval = ALPARAVAL
                            Dim IntResult As Integer = objclsItemMaster.SAVE()

                        End If
                    End If



                    GRIDORDER.Rows.Add(0, DTROW("ITEMNAME"), DTROW("DESIGN"), DTROW("COLOR"), DTROW("QTY"), DTROW("MTRS"), DTROW("SONO"), DTROW("GRIDSRNO"), DTROW("TYPE"), 0, 0, Val(DTROW("RATE")), DTROW("GRIDPARTYPONO"), DTROW("ORDERON"))
                    'FETCH HSNCODE FROM ITEMMASTER
                    Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(HSN_CODE,'') AS HSNCODE, ITEMMASTER.ITEM_REMARKS AS ITEMREMARKS, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT", "", " ITEMMASTER INNER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN UNITMASTER ON ITEMMASTER.ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEMMASTER.ITEM_NAME = '" & DTROW("ITEMNAME") & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                    If ClientName = "MASHOK" Or ClientName = "ABHEE" Then
                        CMBITEM.Text = DTROW("ITEMNAME")
                        If DT.Rows(0).Item("UNIT") <> "" Then PER = DT.Rows(0).Item("UNIT") Else PER = "Mtrs"
                        GETHSNCODE()
                        If DTROW("TYPE") = "YARNSALEORDER" Or DTROW("TYPE") = "OPENINGYARNSALEORDER" Then DT.Rows(0).Item("ITEMREMARKS") = DTROW("GRIDDESC")
                        GRIDINVOICE.Rows.Add(0, DTROW("ITEMNAME"), DT.Rows(0).Item("HSNCODE"), "", DTROW("DESIGN"), DTROW("COLOR"), 0, 0, DT.Rows(0).Item("ITEMREMARKS"), "", 0, 0, 0, Format(Val(DTROW("RATE")), "0.00"), PER, 0, "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, DTROW("TYPE"), 0, "", "", Val(DTROW("SONO")), Val(DTROW("GRIDSRNO")), 0, "", "")
                    End If
NEXTLINE:
                Next
                getsrno(GRIDORDER)
                getsrno(GRIDINVOICE)
                CMDSELECTSO.Enabled = False

                TOTAL()
                GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1
                If GRIDINVOICE.RowCount > 0 Then
                    GRIDINVOICE.Focus()
                    GRIDINVOICE.CurrentCell = GRIDINVOICE.Rows(0).Cells(Gpcs.Index)
                End If
                If ClientName = "ABHEE" Then
                    GRIDINVOICE.RowCount = 0
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDTRANS_Validated(sender As Object, e As EventArgs) Handles CMBGRIDTRANS.Validated
        Try
            Dim TEMPE As New CancelEventArgs
            If ClientName = "ABHEE" Then
                Call TXTGRIDTOTAL_Validating(sender, TEMPE)
                CALC()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDTRANS_Validating(sender As Object, e As CancelEventArgs) Handles CMBGRIDTRANS.Validating
        Try
            If CMBGRIDTRANS.Text.Trim <> "" Then NAMEVALIDATE(CMBGRIDTRANS, CMBCODE, e, Me, TXTTRANSADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDTRANS_Enter(sender As Object, e As EventArgs) Handles CMBGRIDTRANS.Enter
        Try
            If CMBGRIDTRANS.Text.Trim = "" Then FILLNAME(CMBGRIDTRANS, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


End Class